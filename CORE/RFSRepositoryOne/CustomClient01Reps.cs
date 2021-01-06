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
using OfficeOpenXml.Drawing;
using System.Data.OleDb;using RFSRepository;


namespace RFSRepositoryOne
{
    public class CustomClient01Reps
    {
        Host _host = new Host();

        //Model UnitRegistry
        private class HistoricalTransactionPerClient
        {
            public string ClientName { get; set; }
            public string CIF { get; set; }
            public string SID { get; set; }
            public string Email { get; set; }
            public string Period { get; set; }
            public DateTime TransactionDate { get; set; }
            public string Fund { get; set; }
            public string Transaction { get; set; }
            public decimal Subscription { get; set; }
            public decimal Fee { get; set; }
            public decimal NetSubs { get; set; }
            public decimal NAV { get; set; }
            public decimal Unit { get; set; }
            public string Remark { get; set; }
            public string SalesName { get; set; }
            public string Branch { get; set; }
        }
        private class ClientReport
        {
            public string ClientName { get; set; }
            public string CIF { get; set; }
            public string SID { get; set; }
            public string Email { get; set; }
            public string Period { get; set; }
            public DateTime TransactionDate { get; set; }
            public string Fund { get; set; }
            public string Transaction { get; set; }
            public decimal Subscription { get; set; }
            public decimal Fee { get; set; }
            public decimal NetSubs { get; set; }
            public decimal NAV { get; set; }
            public decimal Unit { get; set; }
            public decimal UnitBalance { get; set; }
            public decimal AvgNAV { get; set; }
            public string Remark { get; set; }
            public decimal LastNAV { get; set; }
            public decimal BeginingBalance { get; set; }
        }

        private class SummaryClient
        {
            public string ClientName { get; set; }
            public string Fund { get; set; }
            public string CIF { get; set; }
            public string Type { get; set; }
            public decimal CurrentNAV { get; set; }
            public decimal UnitBalance { get; set; }
            public decimal NAV { get; set; }

        }

        private class ManagementFeePerClient
        {
            public string ClientName { get; set; }
            public string Fund { get; set; }
            public DateTime Date { get; set; }
            public DateTime DateFrom { get; set; }
            public DateTime DateTo { get; set; }
            public decimal TotalNAV { get; set; }
            public decimal MIFee { get; set; }
            public decimal MIFeeAccrual { get; set; }
            public string AgentName { get; set; }
            public decimal TrailingFee { get; set; }

        }

        private class DailyTransactionReport
        {
            public string ClientName { get; set; }
            public string CIF { get; set; }
            public string Fund { get; set; }
            public string Type { get; set; }
            public string SalesName { get; set; }
            public string Branch { get; set; }
            public DateTime ValueDate { get; set; }
            public DateTime PaymentDate { get; set; }
            public decimal Fee { get; set; }
            public decimal NetSubs { get; set; }
            public decimal NAV { get; set; }
            public decimal Unit { get; set; }
            public string Remark { get; set; }
            public decimal Amount { get; set; }
            public string Currency { get; set; }
            public string Description { get; set; }
        }


        //Fund Admin
        private class CounterpartTransaction
        {
            public string Custodian { get; set; }
            public string FundPK { get; set; }
            public string FundName { get; set; }
            public string FundType { get; set; }
            public DateTime Date { get; set; }
            public string TransactionType { get; set; }
            public string BrokerCode { get; set; }
            public decimal TotalYTD { get; set; }
            public decimal TotalCommission { get; set; }
            public decimal TotalWHT { get; set; }
            public decimal PercentageNAV { get; set; }

        }
        private class PerhitunganFeeMIdanFeeBK
        {
            public string Custodian { get; set; }
            public string FundPK { get; set; }
            public string FundName { get; set; }
            public string FundType { get; set; }
            public DateTime Date { get; set; }
            public string Currency { get; set; }
            public string Perhitungan { get; set; }
            public decimal TotalDanaKelolaan { get; set; }
            public decimal MIFEE { get; set; }
            public decimal MIFEEAccrual { get; set; }
            public decimal PPN { get; set; }
            public decimal TotalFee { get; set; }
        }

        public class CashMovement
        {
            public string Custodian { get; set; }
            public string FundName { get; set; }
            public string CustodianName { get; set; }
            public DateTime Date { get; set; }
            public string IDTrans { get; set; }
            public string Type { get; set; }
            public string InstrumentName { get; set; }
            public string Description { get; set; }
            public decimal Debit { get; set; }
            public decimal Credit { get; set; }
            public decimal StartBalance { get; set; }
            public decimal EndBalance { get; set; }
        }

        public class IncomeStatement
        {
            public string DateFrom { get; set; }
            public string DateTo { get; set; }
            public string ID { get; set; }
            public string Name { get; set; }
            public decimal PreviousBaseBalance { get; set; }
            public decimal BaseDebitMutasi { get; set; }
            public decimal BaseCreditMutasi { get; set; }
            public decimal CurrentBaseBalance { get; set; }
            public decimal Movement { get; set; }
            public string CurrencyID { get; set; }
            public int Groups { get; set; }
            public int ParentPK { get; set; }
            public int No { get; set; }
            public string FundID { get; set; }
            public string FundName { get; set; }
            public string InstrumentID { get; set; }
            public string DepartmentID { get; set; }
            public string OfficeID { get; set; }
            public string AgentID { get; set; }


        }

        private class TrxSummary
        {
            public string InstrumentID { get; set; }
            public string InstrumentTypeName { get; set; }
            public string TrxTypeID { get; set; }
            public DateTime ValueDate { get; set; }
            public DateTime SettlementDate { get; set; }
            public string MaturityDate { get; set; }
            public string CounterpartCode { get; set; }
            public string InstrumentName { get; set; }
            public decimal DoneVolume { get; set; }
            public decimal DonePrice { get; set; }
            public decimal DoneAmount { get; set; }
            public decimal TotalBrokerFee { get; set; }
            public decimal WHTAmount { get; set; }
            public decimal TotalAmount { get; set; }
            public decimal AcqPrice { get; set; }
            public decimal Realised { get; set; }
            public decimal DoneAccruedInterest { get; set; }
            public decimal InterestPercent { get; set; }
            public string BankName { get; set; }
            public int Tenor { get; set; }
            public string FundPK { get; set; }
            public string FundName { get; set; }
            public string FundType { get; set; }
            public string InstrumentType { get; set; }

        }

        private class PortfolioValuationReport
        {
            public string SecurityCode { get; set; }
            public string SecurityDescription { get; set; }
            public string TimeDeposit { get; set; }
            public string BICode { get; set; }
            public string Branch { get; set; }
            public string InstrumentTypeName { get; set; }
            public decimal QtyOfUnit { get; set; }
            public decimal Lot { get; set; }
            public decimal AverageCost { get; set; }
            public decimal BookValue { get; set; }
            public decimal MarketPrice { get; set; }
            public decimal MarketValue { get; set; }
            public decimal UnrealizedProfitLoss { get; set; }
            public decimal PercentFR { get; set; }
            public decimal Nominal { get; set; }
            public decimal RateGross { get; set; }
            public decimal AccIntTD { get; set; }
            public string TradeDate { get; set; }
            public string MaturityDate { get; set; }
            public string FundID { get; set; }
            public string FundName { get; set; }
            public int InstrumentTypePK { get; set; }
            public string Date { get; set; }
            public string FundType { get; set; }
            public string CurrencyID { get; set; }
            public decimal PercentOfNav { get; set; }
            public string PeriodeActual { get; set; }
            public decimal Accrual { get; set; }
            public decimal AccruedDays { get; set; }
            public string BankCustodian { get; set; }
        }




        public Boolean GenerateReportUnitRegistry(string _userID, UnitRegistryRpt _unitRegistryRpt)
        {
            #region Historical Transaction Per Client
            if (_unitRegistryRpt.ReportName.Equals("Historical Transaction Per Client"))
            {
                try
                {
                    using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                    {
                        DbCon.Open();
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            string _status = "";
                            string _paramFund = "";
                            string _paramFundTo = "";
                            string _paramFundFrom = "";
                            string _paramFundClient = "";

                            if (!_host.findString(_unitRegistryRpt.FundFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_unitRegistryRpt.FundFrom))
                            {
                                _paramFund = "And A.FundPK in ( " + _unitRegistryRpt.FundFrom + " ) ";
                            }
                            else
                            {
                                _paramFund = "";
                            }
                            if (!_host.findString(_unitRegistryRpt.FundFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_unitRegistryRpt.FundFrom))
                            {
                                _paramFundFrom = "And A.FundPKFrom in ( " + _unitRegistryRpt.FundFrom + " ) ";
                            }
                            else
                            {
                                _paramFundFrom = "";
                            }

                            if (!_host.findString(_unitRegistryRpt.FundFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_unitRegistryRpt.FundFrom))
                            {
                                _paramFundTo = "And A.FundPKTo in ( " + _unitRegistryRpt.FundFrom + " ) ";
                            }
                            else
                            {
                                _paramFundTo = "";
                            }
                            if (!_host.findString(_unitRegistryRpt.FundClientFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_unitRegistryRpt.FundClientFrom))
                            {
                                _paramFundClient = "And A.FundClientPK in ( " + _unitRegistryRpt.FundClientFrom + " ) ";
                            }
                            else
                            {
                                _paramFundClient = "";
                            }


                            if (_unitRegistryRpt.Status == 1)
                            {
                                _status = " A.Status = 2 and A.Posted = 1 and A.Revised = 0 ";

                            }
                            else if (_unitRegistryRpt.Status == 2)
                            {
                                _status = " A.Status = 2 and A.Posted = 1 and A.Revised = 1 ";

                            }
                            else if (_unitRegistryRpt.Status == 3)
                            {
                                _status = " A.Status = 2 and A.Posted = 0 and A.Revised = 0 ";

                            }
                            else if (_unitRegistryRpt.Status == 4)
                            {
                                _status = " A.Status = 1  ";

                            }
                            else if (_unitRegistryRpt.Status == 5)
                            {
                                _status = " A.Status = 3  ";

                            }
                            else if (_unitRegistryRpt.Status == 6)
                            {
                                _status = " (A.Status = 2 or A.Posted = 1) and A.Revised = 0 and A.status not in (3,4)  ";

                            }
                            else if (_unitRegistryRpt.Status == 7)
                            {
                                _status = " (A.Status = 1 Or A.Status = 2 or A.Posted = 1) and  A.Revised = 0 and A.status not in (3,4)  ";

                            }

                            cmd.CommandText =

                            @"
                            Select 
	                        A.* from (
	                        Select isnull(C.Name,'') FundName,isnull(B.Name,'') Name,B.ID CIF,B.SID SID,case when B.ClientCategory = 1 then isnull(B.Email,'') else isnull(B.CompanyMail,'') end [Email]
	                        ,A.ValueDate, 'SUB' [Transaction]
	                        ,A.TotalUnitAmount Unit
	                        ,A.NAV 
	                        ,A.TotalCashAmount Amount
	                        ,A.Description Remark
	                        ,A.CashAmount NetSubs
	                        ,A.FundClientPK,A.FundPK
                            ,isnull(D.Name,'') SalesName
                            ,isnull(F.Name,'') BranchName
	                        from ClientSubscription A
	                        left join FundClient B on A.FundClientPK = B.FundClientPK and B.status in (1,2)
	                        left join Fund C on A.FundPK = C.FundPK and C.status in (1,2)
left join Agent D on B.SellingAgentPK = D.AgentPK and D.status in (1,2)
Left join Department F on D.DepartmentPK = F.DepartmentPK and F.Status in (1,2)
	                        where " + _status + _paramFund + _paramFundClient + @" and A.ValueDate between @DateFrom and @Dateto

	                        UNION ALL

	                        Select isnull(C.Name,'') FundName,isnull(B.Name,'') Name,B.ID CIF,B.SID SID,case when B.ClientCategory = 1 then isnull(B.Email,'') else isnull(B.CompanyMail,'') end [Email]
	                        ,A.ValueDate, 'RED' [Transaction]
	                        ,A.UnitAmount Unit
	                        ,A.NAV 
	                        ,A.TotalCashAmount Amount
	                        ,A.Description Remark
	                        ,A.CashAmount NetSubs
	                        ,A.FundClientPK,A.FundPK
                            ,isnull(D.Name,'') SalesName
                            ,isnull(F.Name,'') BranchName
	                        from ClientRedemption A
	                        left join FundClient B on A.FundClientPK = B.FundClientPK and B.status in (1,2)
	                        left join Fund C on A.FundPK = C.FundPK and C.status in (1,2)
left join Agent D on B.SellingAgentPK = D.AgentPK and D.status in (1,2)
Left join Department F on D.DepartmentPK = F.DepartmentPK and F.Status in (1,2)
	                        where " + _status + _paramFund + _paramFundClient + @" and A.ValueDate between @DateFrom and @Dateto

	                        UNION ALL

	                        Select isnull(C.Name,'') FundName,isnull(B.Name,'') Name,B.ID CIF,B.SID SID,case when B.ClientCategory = 1 then isnull(B.Email,'') else isnull(B.CompanyMail,'') end [Email]
	                        ,A.ValueDate, 'SWITCH IN' [Transaction]
	                        ,A.TotalUnitAmountFundTo Unit
	                        ,A.NAVFundTo NAV 
	                        ,A.TotalCashAmountFundTo Amount
	                        ,A.Description Remark
	                        ,A.CashAmount NetSubs
	                        ,A.FundClientPK,A.FundPKTo FundPK
                            ,isnull(D.Name,'') SalesName
                            ,isnull(F.Name,'') BranchName
	                        from ClientSwitching A
	                        left join FundClient B on A.FundClientPK = B.FundClientPK and B.status in (1,2)
	                        left join Fund C on A.FundPKTo = C.FundPK and C.status in (1,2)
left join Agent D on B.SellingAgentPK = D.AgentPK and D.status in (1,2)
Left join Department F on D.DepartmentPK = F.DepartmentPK and F.Status in (1,2)
	                        where " + _status + _paramFundTo + _paramFundClient + @" and A.ValueDate between @DateFrom and @Dateto


	                        UNION ALL

	                        Select isnull(C.Name,'') FundName,isnull(B.Name,'') Name,B.ID CIF,B.SID SID,case when B.ClientCategory = 1 then isnull(B.Email,'') else isnull(B.CompanyMail,'') end [Email]
	                        ,A.ValueDate, 'SWITCH OUT' [Transaction]
	                        ,A.TotalUnitAmountFundFrom Unit
	                        ,A.NAVFundFrom NAV 
	                        ,A.TotalCashAmountFundFrom Amount
	                        ,A.Description Remark
	                        ,A.CashAmount NetSubs
	                        ,A.FundClientPK,A.FundPKFrom FundPK
                            ,isnull(D.Name,'') SalesName
                            ,isnull(F.Name,'') BranchName
	                        from ClientSwitching A
	                        left join FundClient B on A.FundClientPK = B.FundClientPK and B.status in (1,2)
	                        left join Fund C on A.FundPKFrom = C.FundPK and C.status in (1,2)
left join Agent D on B.SellingAgentPK = D.AgentPK and D.status in (1,2)
Left join Department F on D.DepartmentPK = F.DepartmentPK and F.Status in (1,2)
	                        where " + _status + _paramFundFrom + _paramFundClient + @" and A.ValueDate between @DateFrom and @Dateto
	                        )A"
                            ;

                            cmd.CommandTimeout = 0;


                            cmd.Parameters.AddWithValue("@DateFrom", _unitRegistryRpt.ValueDateFrom);
                            cmd.Parameters.AddWithValue("@Dateto", _unitRegistryRpt.ValueDateTo);
                            cmd.ExecuteNonQuery();

                            using (SqlDataReader dr0 = cmd.ExecuteReader())
                            {
                                if (!dr0.HasRows)
                                {
                                    return false;
                                }
                                else
                                {
                                    string filePath = Tools.ReportsPath + "HistoricalTransactionPerClient" + "_" + _userID + ".xlsx";
                                    string pdfPath = Tools.ReportsPath + "HistoricalTransactionPerClient" + "_" + _userID + ".pdf";
                                    FileInfo excelFile = new FileInfo(filePath);
                                    if (excelFile.Exists)
                                    {
                                        excelFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                        excelFile = new FileInfo(filePath);
                                    }


                                    // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                    using (ExcelPackage package = new ExcelPackage(excelFile))
                                    {
                                        package.Workbook.Properties.Title = "Historical Transaction Per Client";
                                        package.Workbook.Properties.Author = Tools.DefaultReportAuthor();
                                        package.Workbook.Properties.Comments = Tools.DefaultReportComments();
                                        package.Workbook.Properties.Company = Tools.DefaultReportCompany();
                                        package.Workbook.Properties.SetCustomPropertyValue("Checked by", _userID);
                                        package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", Tools.DefaultReportAssemblyName());

                                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Historical Transaction Per Client");


                                        //ATUR DATA GROUPINGNYA DULU
                                        List<HistoricalTransactionPerClient> rList = new List<HistoricalTransactionPerClient>();
                                        while (dr0.Read())
                                        {
                                            HistoricalTransactionPerClient rSingle = new HistoricalTransactionPerClient();
                                            rSingle.ClientName = Convert.ToString(dr0["Name"]);
                                            rSingle.CIF = Convert.ToString(dr0["CIF"]);
                                            rSingle.SID = Convert.ToString(dr0["SID"]);
                                            rSingle.Email = Convert.ToString(dr0["Email"]);
                                            //rSingle.Period = Convert.ToString(dr0["Period"]);
                                            rSingle.TransactionDate = Convert.ToDateTime(dr0["ValueDate"]);
                                            rSingle.Fund = Convert.ToString(dr0["FundName"]);
                                            rSingle.Transaction = Convert.ToString(dr0["Transaction"]);
                                            rSingle.Subscription = Convert.ToDecimal(dr0["Amount"]);
                                            //rSingle.Fee = Convert.ToDecimal(dr0["Fee"]);
                                            rSingle.NetSubs = Convert.ToDecimal(dr0["NetSubs"]);
                                            rSingle.NAV = Convert.ToDecimal(dr0["NAV"]);
                                            rSingle.Unit = Convert.ToDecimal(dr0["Unit"]);
                                            rSingle.Remark = Convert.ToString(dr0["Remark"]);
                                            rSingle.SalesName = Convert.ToString(dr0["SalesName"]);
                                            rSingle.Branch = Convert.ToString(dr0["BranchName"]);

                                            rList.Add(rSingle);

                                        }

                                        var GroupByCategory =
                                         from r in rList
                                         orderby r.ClientName ascending
                                         group r by new { r.ClientName, r.CIF, r.SID, r.Email, r.Period } into rGroup
                                         select rGroup;

                                        int incRowExcel = 0;
                                        //int _rowEndBalance = 0;

                                        foreach (var rsHeader in GroupByCategory)
                                        {
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Size = 20;
                                            worksheet.Cells["A" + incRowExcel + ":L" + incRowExcel].Merge = true;
                                            worksheet.Cells["A" + incRowExcel + ":L" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 1].Value = "Client Name ";
                                            worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 2].Value = " : ";
                                            worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 3].Value = rsHeader.Key.ClientName;
                                            worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 1].Value = "CIF ";
                                            worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 2].Value = " : ";
                                            worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 3].Value = rsHeader.Key.CIF;
                                            worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 1].Value = "SID ";
                                            worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 2].Value = " : ";
                                            worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 3].Value = rsHeader.Key.SID;
                                            worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 1].Value = "Email ";
                                            worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 2].Value = " : ";
                                            worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 3].Value = rsHeader.Key.Email;
                                            worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 1].Value = "Period ";
                                            worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 2].Value = " : ";

                                            worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 3].Value = Convert.ToDateTime(_unitRegistryRpt.ValueDateFrom).ToString("dd/MMM/yyyy") + " - " + Convert.ToDateTime(_unitRegistryRpt.ValueDateTo).ToString("dd/MMM/yyyy");
                                            incRowExcel = incRowExcel + 2;
                                            int RowA = incRowExcel;
                                            int RowB = incRowExcel + 1;

                                            worksheet.Cells["A" + RowA + ":L" + RowA].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + RowB + ":L" + RowB].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells["A" + RowA + ":A" + RowB].Value = "Transaction Date";
                                            worksheet.Cells["A" + RowA + ":A" + RowB].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells["A" + RowA + ":A" + RowB].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["A" + RowA + ":A" + RowB].Merge = true;

                                            worksheet.Cells["C" + RowA + ":C" + RowB].Value = "Fund";
                                            worksheet.Cells["C" + RowA + ":C" + RowB].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells["C" + RowA + ":C" + RowB].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["C" + RowA + ":C" + RowB].Merge = true;

                                            worksheet.Cells["D" + RowA + ":D" + RowB].Value = "Transaction";
                                            worksheet.Cells["D" + RowA + ":D" + RowB].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells["D" + RowA + ":D" + RowB].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["D" + RowA + ":D" + RowB].Merge = true;

                                            worksheet.Cells["E" + RowA + ":E" + RowB].Value = "Subscription ";
                                            worksheet.Cells["E" + RowA + ":E" + RowB].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells["E" + RowA + ":E" + RowB].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["E" + RowA + ":E" + RowB].Merge = true;

                                            worksheet.Cells["F" + RowA + ":F" + RowB].Value = "Fee";
                                            worksheet.Cells["F" + RowA + ":F" + RowB].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells["F" + RowA + ":F" + RowB].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["F" + RowA + ":F" + RowB].Merge = true;

                                            worksheet.Cells["G" + RowA + ":G" + RowB].Value = "Net Subs.";
                                            worksheet.Cells["G" + RowA + ":G" + RowB].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells["G" + RowA + ":G" + RowB].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["G" + RowA + ":G" + RowB].Merge = true;

                                            worksheet.Cells["H" + RowA + ":H" + RowB].Value = "NAV";
                                            worksheet.Cells["H" + RowA + ":H" + RowB].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells["H" + RowA + ":H" + RowB].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["H" + RowA + ":H" + RowB].Merge = true;

                                            worksheet.Cells["I" + RowA + ":I" + RowB].Value = "Unit ";
                                            worksheet.Cells["I" + RowA + ":I" + RowB].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells["I" + RowA + ":I" + RowB].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["I" + RowA + ":I" + RowB].Merge = true;

                                            worksheet.Cells["J" + RowA + ":J" + RowB].Value = "Remark";
                                            worksheet.Cells["J" + RowA + ":J" + RowB].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells["J" + RowA + ":J" + RowB].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["J" + RowA + ":J" + RowB].Merge = true;

                                            worksheet.Cells["K" + RowA + ":K" + RowB].Value = "Sales Name";
                                            worksheet.Cells["K" + RowA + ":K" + RowB].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells["K" + RowA + ":K" + RowB].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["K" + RowA + ":K" + RowB].Merge = true;

                                            worksheet.Cells["L" + RowA + ":L" + RowB].Value = "Branch";
                                            worksheet.Cells["L" + RowA + ":L" + RowB].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells["L" + RowA + ":L" + RowB].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["L" + RowA + ":L" + RowB].Merge = true;


                                            incRowExcel = incRowExcel + 2;
                                            int _no = 1;

                                            int _startRowDetail = incRowExcel;
                                            int _endRowDetail = incRowExcel;
                                            //end area header

                                            incRowExcel++;
                                            foreach (var rsDetail in rsHeader)
                                            {

                                                //area detail
                                                worksheet.Cells[incRowExcel, 1].Value = Convert.ToDateTime(rsDetail.TransactionDate).ToString("dd-MMM-yyyy");
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.Fund;
                                                worksheet.Cells[incRowExcel, 3].Style.WrapText = true;
                                                worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 4].Value = rsDetail.Transaction;
                                                worksheet.Cells[incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 5].Value = rsDetail.Subscription;
                                                worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.0000";
                                                worksheet.Cells[incRowExcel, 6].Value = rsDetail.Fee;
                                                worksheet.Cells[incRowExcel, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.0000";
                                                worksheet.Cells[incRowExcel, 7].Value = rsDetail.NetSubs;
                                                worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0.0000";
                                                worksheet.Cells[incRowExcel, 8].Value = rsDetail.NAV;
                                                worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0.0000";
                                                worksheet.Cells[incRowExcel, 9].Value = rsDetail.Unit;
                                                worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0.0000";
                                                worksheet.Cells[incRowExcel, 10].Value = rsDetail.Remark;
                                                worksheet.Cells[incRowExcel, 10].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                worksheet.Cells[incRowExcel, 10].Style.WrapText = true;
                                                worksheet.Cells[incRowExcel, 11].Value = rsDetail.SalesName;
                                                worksheet.Cells[incRowExcel, 12].Value = rsDetail.Branch;
                                                _endRowDetail = incRowExcel;
                                                _no++;
                                                incRowExcel++;

                                            }
                                            worksheet.Cells["A" + incRowExcel + ":L" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                            incRowExcel = incRowExcel + 7;
                                            worksheet.Cells[incRowExcel, 1].Value = "Disclaimer   : ";
                                            incRowExcel++;
                                            worksheet.Row(incRowExcel).Height = 50;
                                            worksheet.Cells[incRowExcel, 1].Value = Tools.DefaultDisclaimerReportFooterLeftText();
                                            worksheet.Cells["A" + incRowExcel + ":I" + incRowExcel].Style.WrapText = true;
                                            worksheet.Cells["A" + incRowExcel + ":I" + incRowExcel].Merge = true;
                                            worksheet.Cells["A" + incRowExcel + ":I" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            worksheet.Cells[incRowExcel, 1].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                                            incRowExcel = incRowExcel + 2;
                                            worksheet.Row(incRowExcel).PageBreak = _unitRegistryRpt.PageBreak;

                                        }


                                        // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                        worksheet.PrinterSettings.FitToPage = true;
                                        worksheet.PrinterSettings.FitToWidth = 1;
                                        worksheet.PrinterSettings.FitToHeight = 0;
                                        worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel, 12];
                                        worksheet.Column(1).Width = 15;
                                        worksheet.Column(2).Width = 5;
                                        worksheet.Column(3).Width = 35;
                                        worksheet.Column(4).Width = 20;
                                        worksheet.Column(5).Width = 20;
                                        worksheet.Column(6).Width = 20;
                                        worksheet.Column(7).Width = 20;
                                        worksheet.Column(8).Width = 20;
                                        worksheet.Column(9).Width = 20;
                                        worksheet.Column(10).Width = 20;
                                        worksheet.Column(11).Width = 20;
                                        worksheet.Column(12).Width = 20;

                                        worksheet.PrinterSettings.Orientation = eOrientation.Landscape; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                        // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                        worksheet.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
                                        worksheet.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
                                        worksheet.View.ShowGridLines = Tools.DefaultReportShowGridLines();
                                        Image img = Image.FromFile(Tools.ReportImage);
                                        worksheet.HeaderFooter.OddHeader.InsertPicture(img, PictureAlignment.Left);
                                        worksheet.HeaderFooter.OddHeader.CenteredText = "&14 Historical Transaction Report";


                                        //worksheet.HeaderFooter.OddHeader.LeftAlignedText = Tools.DefaultReportHeaderLeftText();

                                        worksheet.HeaderFooter.OddFooter.RightAlignedText =
                                        string.Format("Page {0} of {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);



                                        //worksheet.HeaderFooter.OddFooter.CenteredText = Tools.DefaultReportFooterCenterText();
                                        worksheet.HeaderFooter.OddFooter.LeftAlignedText = Tools.DefaultReportFooterLeftText();

                                        package.Save();
                                        if (_unitRegistryRpt.DownloadMode == "PDF")
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
            #endregion

            #region Client Report
            if (_unitRegistryRpt.ReportName.Equals("Client Report"))
            {
                try
                {
                    using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                    {
                        DbCon.Open();
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            string _status = "";
                            string _paramFund = "";
                            string _paramFundTo = "";
                            string _paramFundFrom = "";
                            string _paramFundClient = "";

                            if (!_host.findString(_unitRegistryRpt.FundFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_unitRegistryRpt.FundFrom))
                            {
                                _paramFund = "And A.FundPK in ( " + _unitRegistryRpt.FundFrom + " ) ";
                            }
                            else
                            {
                                _paramFund = "";
                            }
                            if (!_host.findString(_unitRegistryRpt.FundFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_unitRegistryRpt.FundFrom))
                            {
                                _paramFundFrom = "And A.FundPKFrom in ( " + _unitRegistryRpt.FundFrom + " ) ";
                            }
                            else
                            {
                                _paramFundFrom = "";
                            }

                            if (!_host.findString(_unitRegistryRpt.FundFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_unitRegistryRpt.FundFrom))
                            {
                                _paramFundTo = "And A.FundPKTo in ( " + _unitRegistryRpt.FundFrom + " ) ";
                            }
                            else
                            {
                                _paramFundTo = "";
                            }
                            if (!_host.findString(_unitRegistryRpt.FundClientFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_unitRegistryRpt.FundClientFrom))
                            {
                                _paramFundClient = "And A.FundClientPK in ( " + _unitRegistryRpt.FundClientFrom + " ) ";
                            }
                            else
                            {
                                _paramFundClient = "";
                            }


                            if (_unitRegistryRpt.Status == 1)
                            {
                                _status = " A.Status = 2 and A.Posted = 1 and A.Revised = 0 ";

                            }
                            else if (_unitRegistryRpt.Status == 2)
                            {
                                _status = " A.Status = 2 and A.Posted = 1 and A.Revised = 1 ";

                            }
                            else if (_unitRegistryRpt.Status == 3)
                            {
                                _status = " A.Status = 2 and A.Posted = 0 and A.Revised = 0 ";

                            }
                            else if (_unitRegistryRpt.Status == 4)
                            {
                                _status = " A.Status = 1  ";

                            }
                            else if (_unitRegistryRpt.Status == 5)
                            {
                                _status = " A.Status = 3  ";

                            }
                            else if (_unitRegistryRpt.Status == 6)
                            {
                                _status = " (A.Status = 2 or A.Posted = 1) and A.Revised = 0 and A.status not in (3,4)  ";

                            }
                            else if (_unitRegistryRpt.Status == 7)
                            {
                                _status = " (A.Status = 1 Or A.Status = 2 or A.Posted = 1) and  A.Revised = 0 and A.status not in (3,4)  ";

                            }

                            cmd.CommandText =

                            @"
                                CREATE TABLE #A
                                (FundName nvarchar(100),Name nvarchar(100),CIF nvarchar(100),SID nvarchar(100),Email nvarchar(100),ValueDate datetime,Trx nvarchar(10),Unit numeric(22,8),NAV numeric(22,8),Amount numeric(22,2),Remark nvarchar(100),NetSubs numeric(22,2),FundClientPK int,FundPK int,AVGNav numeric(22,8),BeginningBalance numeric(22,2),LastNAV numeric(22,8))

                                insert into #A (FundName,Name,CIF,SID,Email,ValueDate,Trx,Unit,NAV,Amount,Remark,NetSubs,FundClientPK,FundPK,AVGNav,BeginningBalance,LastNAV)


                                select A.*, [dbo].[Get_UnitAmountByFundPKandFundClientPK](dateadd(day,-1,@dateFrom),A.FundClientPK,A.FundPK) BeginningBalance 
                                ,[dbo].[FgetLastCloseNav] (@DateTo,A.FundPK) LastNAV
                                from (
                                Select isnull(C.Name,'') FundName,isnull(B.Name,'') Name,B.ID CIF,B.SID SID,case when B.ClientCategory = 1 then isnull(B.Email,'') else isnull(B.CompanyMail,'') end [Email]
                                ,A.ValueDate, 'SUB' [Transaction]
                                ,A.TotalUnitAmount Unit
                                ,A.NAV 
                                ,A.TotalCashAmount Amount
                                ,A.Description Remark
                                ,A.CashAmount NetSubs
                                ,A.FundClientPK,A.FundPK, [dbo].[FGetAVGForFundClientPosition] (A.ValueDate,A.FundClientPK,A.fundPK) AVGNav
                                from ClientSubscription A
                                left join FundClient B on A.FundClientPK = B.FundClientPK and B.status in (1,2)
                                left join Fund C on A.FundPK = C.FundPK and C.status in (1,2)
                                where " + _status + _paramFund + _paramFundClient + @" and A.ValueDate between @DateFrom and @Dateto

                                UNION ALL

                                Select isnull(C.Name,'') FundName,isnull(B.Name,'') Name,B.ID CIF,B.SID SID,case when B.ClientCategory = 1 then isnull(B.Email,'') else isnull(B.CompanyMail,'') end [Email]
                                ,A.ValueDate, 'RED' [Transaction]
                                ,A.UnitAmount Unit
                                ,A.NAV 
                                ,A.TotalCashAmount Amount
                                ,A.Description Remark
                                ,A.CashAmount NetSubs
                                ,A.FundClientPK,A.FundPK, [dbo].[FGetAVGForFundClientPosition] (A.ValueDate,A.FundClientPK,A.fundPK) AVGNav
                                from ClientRedemption A
                                left join FundClient B on A.FundClientPK = B.FundClientPK and B.status in (1,2)
                                left join Fund C on A.FundPK = C.FundPK and C.status in (1,2)
                                where " + _status + _paramFund + _paramFundClient + @" and A.ValueDate between @DateFrom and @Dateto

                                UNION ALL

                                Select isnull(C.Name,'') FundName,isnull(B.Name,'') Name,B.ID CIF,B.SID SID,case when B.ClientCategory = 1 then isnull(B.Email,'') else isnull(B.CompanyMail,'') end [Email]
                                ,A.ValueDate, 'SWITCH IN' [Transaction]
                                ,A.TotalUnitAmountFundTo Unit
                                ,A.NAVFundTo NAV 
                                ,A.TotalCashAmountFundTo Amount
                                ,A.Description Remark
                                ,A.CashAmount NetSubs
                                ,A.FundClientPK,A.FundPKTo FundPK, [dbo].[FGetAVGForFundClientPosition] (A.ValueDate,A.FundClientPK,A.fundPKTo) AVGNav
                                from ClientSwitching A
                                left join FundClient B on A.FundClientPK = B.FundClientPK and B.status in (1,2)
                                left join Fund C on A.FundPKTo = C.FundPK and C.status in (1,2)
                                WHERE " + _status + _paramFundTo + _paramFundClient + @" and A.ValueDate between @DateFrom and @Dateto


                                UNION ALL

                                Select isnull(C.Name,'') FundName,isnull(B.Name,'') Name,B.ID CIF,B.SID SID,case when B.ClientCategory = 1 then isnull(B.Email,'') else isnull(B.CompanyMail,'') end [Email]
                                ,A.ValueDate, 'SWITCH OUT' [Transaction]
                                ,A.TotalUnitAmountFundFrom Unit
                                ,A.NAVFundFrom NAV 
                                ,A.TotalCashAmountFundFrom Amount
                                ,A.Description Remark
                                ,A.CashAmount NetSubs
                                ,A.FundClientPK,A.FundPKFrom FundPK, [dbo].[FGetAVGForFundClientPosition] (A.ValueDate,A.FundClientPK,A.fundPKFrom) AVGNav
                                from ClientSwitching A
                                left join FundClient B on A.FundClientPK = B.FundClientPK and B.status in (1,2)
                                left join Fund C on A.FundPKFrom = C.FundPK and C.status in (1,2)
                                where " + _status + _paramFundFrom + _paramFundClient + @" and A.ValueDate between @DateFrom and @Dateto
                                )A


                                select FundName,Name,CIF,SID,Email,ValueDate,Trx [Transaction],Unit,NAV,Amount,Remark,NetSubs,FundClientPK,FundPK,AVGNav,BeginningBalance,LastNAV from #A
                                union all
                                select distinct B.Name FundName,C.Name,C.ID,C.SID,C.Email,@DateTo,'',0,0,0,'',0,A.FundClientPK,B.FundPK,0 ,[dbo].[Get_UnitAmountByFundPKandFundClientPK](dateadd(day,-1,@dateTo),A.FundClientPK,A.FundPK) BeginningBalance 
                                ,dbo.FgetLastCloseNav(@DateTo,A.FundPK)  LastNAV from FundClientPosition A
                                left join Fund B on A.FundPK = B.FundPK and B.Status = 2
                                left join FundClient C on A.FundClientPK = C.FundClientPK and C.Status = 2
                                where  NOT EXISTS 
                                (  
                                SELECT * FROM #A D WHERE A.FundClientPK = D.FundClientPK AND A.FundPK = D.FundPK	
                                )  " + _paramFund + _paramFundClient + @"
                                and Date = 
                                (
                                Select Max(date) From FundClientPosition Where  Date < @DateTo
                                ) 
                                order by Amount desc 

"
                            ;

                            cmd.CommandTimeout = 0;


                            cmd.Parameters.AddWithValue("@DateFrom", _unitRegistryRpt.ValueDateFrom);
                            cmd.Parameters.AddWithValue("@Dateto", _unitRegistryRpt.ValueDateTo);
                            cmd.ExecuteNonQuery();

                            using (SqlDataReader dr0 = cmd.ExecuteReader())
                            {
                                if (!dr0.HasRows)
                                {
                                    return false;
                                }
                                else
                                {
                                    string filePath = Tools.ReportsPath + "ClientReport" + "_" + _userID + ".xlsx";
                                    string pdfPath = Tools.ReportsPath + "ClientReport" + "_" + _userID + ".pdf";
                                    FileInfo excelFile = new FileInfo(filePath);
                                    if (excelFile.Exists)
                                    {
                                        excelFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                        excelFile = new FileInfo(filePath);
                                    }


                                    // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                    using (ExcelPackage package = new ExcelPackage(excelFile))
                                    {
                                        package.Workbook.Properties.Title = "Client Report";
                                        package.Workbook.Properties.Author = Tools.DefaultReportAuthor();
                                        package.Workbook.Properties.Comments = Tools.DefaultReportComments();
                                        package.Workbook.Properties.Company = Tools.DefaultReportCompany();
                                        package.Workbook.Properties.SetCustomPropertyValue("Checked by", _userID);
                                        package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", Tools.DefaultReportAssemblyName());

                                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Client Report");


                                        //ATUR DATA GROUPINGNYA DULU
                                        List<ClientReport> rList = new List<ClientReport>();
                                        while (dr0.Read())
                                        {
                                            ClientReport rSingle = new ClientReport();
                                            rSingle.ClientName = Convert.ToString(dr0["Name"]);
                                            rSingle.CIF = Convert.ToString(dr0["CIF"]);
                                            rSingle.SID = Convert.ToString(dr0["SID"]);
                                            rSingle.Email = Convert.ToString(dr0["Email"]);
                                            //rSingle.Period = Convert.ToString(dr0["Period"]);
                                            rSingle.TransactionDate = Convert.ToDateTime(dr0["ValueDate"]);
                                            rSingle.Fund = Convert.ToString(dr0["FundName"]);
                                            rSingle.Transaction = Convert.ToString(dr0["Transaction"]);
                                            rSingle.Subscription = Convert.ToDecimal(dr0["Amount"]);
                                            //rSingle.Fee = Convert.ToDecimal(dr0["Fee"]);
                                            rSingle.NetSubs = Convert.ToDecimal(dr0["NetSubs"]);
                                            rSingle.NAV = Convert.ToDecimal(dr0["NAV"]);
                                            rSingle.Unit = Convert.ToDecimal(dr0["Unit"]);
                                            //rSingle.UnitBalance = Convert.ToDecimal(dr0["UnitBalance"]);
                                            rSingle.AvgNAV = Convert.ToDecimal(dr0["AvgNAV"]);
                                            rSingle.Remark = Convert.ToString(dr0["Remark"]);
                                            rSingle.BeginingBalance = Convert.ToDecimal(dr0["BeginningBalance"]);
                                            rSingle.LastNAV = Convert.ToDecimal(dr0["LastNAV"]);

                                            rList.Add(rSingle);

                                        }

                                        var GroupByCategory =
                                         from r in rList
                                         orderby r.ClientName ascending
                                         group r by new { r.ClientName, r.CIF, r.SID, r.Email, r.Fund, r.BeginingBalance, r.LastNAV } into rGroup
                                         select rGroup;

                                        int incRowExcel = 0;
                                        //int _rowEndBalance = 0;
                                        int _startRowDetail = 0;
                                        int _endRowDetail = 0;

                                        foreach (var rsHeader in GroupByCategory)
                                        {
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Size = 20;
                                            worksheet.Cells[incRowExcel, 1].Value = "Client Report";
                                            worksheet.Cells["A" + incRowExcel + ":L" + incRowExcel].Merge = true;
                                            worksheet.Cells["A" + incRowExcel + ":L" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 1].Value = "Client Name ";
                                            worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 2].Value = " : ";
                                            worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 3].Value = rsHeader.Key.ClientName;
                                            worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 1].Value = "CIF ";
                                            worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 2].Value = " : ";
                                            worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 3].Value = rsHeader.Key.CIF;
                                            worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 1].Value = "SID ";
                                            worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 2].Value = " : ";
                                            worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 3].Value = rsHeader.Key.SID;
                                            worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 1].Value = "Email ";
                                            worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 2].Value = " : ";
                                            worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 3].Value = rsHeader.Key.Email;
                                            worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 1].Value = "Period ";
                                            worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 2].Value = " : ";
                                            worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 3].Value = Convert.ToDateTime(_unitRegistryRpt.ValueDateFrom).ToString("dd/MMM/yyyy") + " - " + Convert.ToDateTime(_unitRegistryRpt.ValueDateTo).ToString("dd/MMM/yyyy");
                                            worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 1].Value = "Fund ";
                                            worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 2].Value = " : ";
                                            worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 3].Value = rsHeader.Key.Fund;
                                            worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;


                                            incRowExcel++;

                                            worksheet.Cells["A" + incRowExcel + ":L" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + incRowExcel + ":L" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells[incRowExcel, 1].Value = "Transaction Date";
                                            worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 3].Value = "Transaction";
                                            worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 4].Value = "Amount";
                                            worksheet.Cells[incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 5].Value = "Fee";
                                            worksheet.Cells[incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 6].Value = "Net Amount";
                                            worksheet.Cells[incRowExcel, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 7].Value = "NAV/Unit";
                                            worksheet.Cells[incRowExcel, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 8].Value = "Unit";
                                            worksheet.Cells[incRowExcel, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 9].Value = "Unit Balance";
                                            worksheet.Cells[incRowExcel, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 10].Value = "Avg. NAV/unit";
                                            worksheet.Cells[incRowExcel, 10].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 11].Value = "Realized";
                                            worksheet.Cells[incRowExcel, 11].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 12].Value = "Unrealized";
                                            worksheet.Cells[incRowExcel, 12].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            incRowExcel++;
                                            //_startRowDetail = incRowExcel;
                                            worksheet.Cells[incRowExcel, 3].Value = "Beginning";
                                            worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 9].Value = rsHeader.Key.BeginingBalance;
                                            worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0.0000";

                                            worksheet.Cells[incRowExcel, 10].Value = "0";
                                            worksheet.Cells[incRowExcel, 10].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0.0000";

                                            worksheet.Cells[incRowExcel, 12].Style.Numberformat.Format = "#,##0.0000";
                                            worksheet.Cells[incRowExcel, 12].Formula = "(G" + incRowExcel + "-J" + incRowExcel + ")" + "*I" + incRowExcel;
                                            worksheet.Cells[incRowExcel, 12].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                            worksheet.Cells[incRowExcel, 12].Calculate();
                                            int _no = 1;

                                            //end area header

                                            foreach (var rsDetail in rsHeader)
                                            {


                                                _startRowDetail = incRowExcel;

                                                //area detail

                                                if (rsDetail.Transaction != "")
                                                {
                                                    incRowExcel++;
                                                    worksheet.Cells[incRowExcel, 1].Value = Convert.ToDateTime(rsDetail.TransactionDate).ToString("dd-MMM-yyyy");

                                                    worksheet.Cells[incRowExcel, 3].Value = rsDetail.Transaction;
                                                    worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                    worksheet.Cells[incRowExcel, 4].Value = rsDetail.Subscription;
                                                    worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##0.00";
                                                    worksheet.Cells[incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                                                    worksheet.Cells[incRowExcel, 5].Value = rsDetail.Fee;
                                                    worksheet.Cells[incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                    worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.00";

                                                    worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.00";
                                                    worksheet.Cells[incRowExcel, 6].Value = rsDetail.NetSubs;
                                                    worksheet.Cells[incRowExcel, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                                                    worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0.0000";
                                                    worksheet.Cells[incRowExcel, 7].Value = rsDetail.NAV;
                                                    worksheet.Cells[incRowExcel, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                                                    worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0.0000";
                                                    worksheet.Cells[incRowExcel, 8].Value = rsDetail.Unit;
                                                    worksheet.Cells[incRowExcel, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;


                                                    if (rsDetail.Transaction == "SUB")
                                                    {
                                                        worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0.0000";
                                                        worksheet.Cells[incRowExcel, 9].Formula = "SUM(I" + _startRowDetail + "+H" + incRowExcel + ")";
                                                        worksheet.Cells[incRowExcel, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                        worksheet.Cells[incRowExcel, 9].Calculate();
                                                        worksheet.Cells[incRowExcel, 11].Value = "";
                                                        worksheet.Cells[incRowExcel, 12].Style.Numberformat.Format = "#,##0.00";
                                                        worksheet.Cells[incRowExcel, 12].Formula = "(G" + incRowExcel + "-J" + incRowExcel + ")" + "*I" + incRowExcel;
                                                        worksheet.Cells[incRowExcel, 12].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                        worksheet.Cells[incRowExcel, 12].Calculate();
                                                    }
                                                    else if (rsDetail.Transaction == "RED")
                                                    {
                                                        worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0.0000";
                                                        worksheet.Cells[incRowExcel, 9].Formula = "SUM(I" + _startRowDetail + "-H" + incRowExcel + ")";
                                                        worksheet.Cells[incRowExcel, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                        worksheet.Cells[incRowExcel, 9].Calculate();
                                                        worksheet.Cells[incRowExcel, 11].Style.Numberformat.Format = "#,##0.00";
                                                        worksheet.Cells[incRowExcel, 11].Formula = "(G" + incRowExcel + "-J" + incRowExcel + ")" + "*H" + incRowExcel;
                                                        worksheet.Cells[incRowExcel, 11].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                        worksheet.Cells[incRowExcel, 11].Calculate();
                                                        worksheet.Cells[incRowExcel, 12].Value = "";
                                                    }
                                                    else
                                                    {
                                                        worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0.0000";
                                                        worksheet.Cells[incRowExcel, 9].Formula = "SUM(I" + _startRowDetail + "+H" + incRowExcel + ")";
                                                        worksheet.Cells[incRowExcel, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                        worksheet.Cells[incRowExcel, 9].Calculate();
                                                        worksheet.Cells[incRowExcel, 11].Value = "";
                                                        worksheet.Cells[incRowExcel, 12].Style.Numberformat.Format = "#,##0.00";
                                                        worksheet.Cells[incRowExcel, 12].Formula = "(G" + incRowExcel + "-J" + incRowExcel + ")" + "*I" + incRowExcel;
                                                        worksheet.Cells[incRowExcel, 12].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                        worksheet.Cells[incRowExcel, 12].Calculate();
                                                    }

                                                    worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0.0000";
                                                    worksheet.Cells[incRowExcel, 10].Value = rsDetail.AvgNAV;
                                                    worksheet.Cells[incRowExcel, 10].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                    worksheet.Cells[incRowExcel, 10].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;


                                                }
                                                _endRowDetail = incRowExcel;

                                                _no++;




                                            }


                                            incRowExcel++;
                                            worksheet.Cells["A" + incRowExcel + ":L" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells[incRowExcel, 3].Value = "Ending";
                                            worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0.0000";
                                            worksheet.Cells[incRowExcel, 9].Formula = "I" + _endRowDetail;
                                            worksheet.Cells[incRowExcel, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                            worksheet.Cells[incRowExcel, 9].Calculate();



                                            worksheet.Cells[incRowExcel, 11].Value = "-";
                                            worksheet.Cells[incRowExcel, 11].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            incRowExcel = incRowExcel + 2;
                                            int _count = incRowExcel;
                                            worksheet.Cells[incRowExcel, 3].Value = "Ending Balance Unit";
                                            worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                                            worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##0.0000";
                                            worksheet.Cells[incRowExcel, 4].Formula = "I" + _endRowDetail;
                                            worksheet.Cells[incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                            worksheet.Cells[incRowExcel, 4].Calculate();
                                            incRowExcel++;
                                            int _countx = incRowExcel;
                                            worksheet.Cells[incRowExcel, 3].Value = "NAV/Unit as of " + Convert.ToDateTime(_unitRegistryRpt.ValueDateTo).ToString("dd/MMM/yyyy");
                                            worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            worksheet.Cells[incRowExcel, 4].Value = rsHeader.Key.LastNAV;
                                            worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##0.0000";
                                            worksheet.Cells[incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                            incRowExcel++;

                                            worksheet.Cells[incRowExcel, 3].Value = "Total NAV";
                                            worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##0.0000";
                                            worksheet.Cells[incRowExcel, 4].Formula = "SUM(D" + _count + "*D" + _countx + ")";
                                            worksheet.Cells[incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                            worksheet.Cells[incRowExcel, 4].Calculate();
                                            incRowExcel++;

                                            incRowExcel = incRowExcel + 7;
                                            worksheet.Cells[incRowExcel, 1].Value = "Disclaimer   : ";
                                            incRowExcel++;
                                            worksheet.Row(incRowExcel).Height = 50;
                                            worksheet.Cells[incRowExcel, 1].Value = Tools.DefaultDisclaimerReportFooterLeftText();
                                            worksheet.Cells["A" + incRowExcel + ":I" + incRowExcel].Style.WrapText = true;
                                            worksheet.Cells["A" + incRowExcel + ":I" + incRowExcel].Merge = true;
                                            worksheet.Cells["A" + incRowExcel + ":I" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            worksheet.Cells[incRowExcel, 1].Style.VerticalAlignment = ExcelVerticalAlignment.Top;

                                            worksheet.Row(incRowExcel).PageBreak = _unitRegistryRpt.PageBreak;
                                            incRowExcel = incRowExcel + 2;


                                        }


                                        // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                        worksheet.PrinterSettings.FitToPage = true;
                                        worksheet.PrinterSettings.FitToWidth = 1;
                                        worksheet.PrinterSettings.FitToHeight = 0;
                                        worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel - 2, 12];
                                        worksheet.Column(1).Width = 20;
                                        worksheet.Column(2).Width = 5;
                                        worksheet.Column(3).Width = 35;
                                        worksheet.Column(4).Width = 20;
                                        worksheet.Column(5).Width = 20;
                                        worksheet.Column(6).Width = 20;
                                        worksheet.Column(7).Width = 20;
                                        worksheet.Column(8).Width = 20;
                                        worksheet.Column(9).Width = 20;
                                        worksheet.Column(10).Width = 20;
                                        worksheet.Column(11).Width = 20;
                                        worksheet.Column(12).Width = 20;

                                        worksheet.PrinterSettings.Orientation = eOrientation.Landscape; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                        // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                        worksheet.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
                                        worksheet.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
                                        worksheet.View.ShowGridLines = Tools.DefaultReportShowGridLines();
                                        Image img = Image.FromFile(Tools.ReportImage);
                                        worksheet.HeaderFooter.OddHeader.InsertPicture(img, PictureAlignment.Left);
                                        worksheet.HeaderFooter.OddHeader.CenteredText = "&14 Unit Registry Report";


                                        // worksheet.HeaderFooter.OddHeader.LeftAlignedText = Tools.DefaultReportHeaderLeftText();

                                        worksheet.HeaderFooter.OddFooter.RightAlignedText =
                                        string.Format("Page {0} of {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);

                                        //worksheet.HeaderFooter.OddFooter.CenteredText = Tools.DefaultReportFooterCenterText();
                                        worksheet.HeaderFooter.OddFooter.LeftAlignedText = Tools.DefaultReportFooterLeftText();

                                        package.Save();
                                        if (_unitRegistryRpt.DownloadMode == "PDF")
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
            #endregion

            #region Summary Client
            if (_unitRegistryRpt.ReportName.Equals("Summary Client"))
            {
                try
                {
                    using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                    {
                        DbCon.Open();
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            string _status = "";
                            string _paramFund = "";
                            string _paramFundClient = "";

                            if (!_host.findString(_unitRegistryRpt.FundFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_unitRegistryRpt.FundFrom))
                            {
                                _paramFund = " And A.FundPK in ( " + _unitRegistryRpt.FundFrom + " ) ";
                            }
                            else
                            {
                                _paramFund = "";
                            }
                            if (!_host.findString(_unitRegistryRpt.FundClientFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_unitRegistryRpt.FundClientFrom))
                            {
                                _paramFundClient = "And A.FundClientPK in ( " + _unitRegistryRpt.FundClientFrom + " ) ";
                            }
                            else
                            {
                                _paramFundClient = "";
                            }


                            if (_unitRegistryRpt.Status == 1)
                            {
                                _status = " A.Status = 2 and A.Posted = 1 and A.Revised = 0 ";

                            }
                            else if (_unitRegistryRpt.Status == 2)
                            {
                                _status = " A.Status = 2 and A.Posted = 1 and A.Revised = 1 ";

                            }
                            else if (_unitRegistryRpt.Status == 3)
                            {
                                _status = " A.Status = 2 and A.Posted = 0 and A.Revised = 0 ";

                            }
                            else if (_unitRegistryRpt.Status == 4)
                            {
                                _status = " A.Status = 1  ";

                            }
                            else if (_unitRegistryRpt.Status == 5)
                            {
                                _status = " A.Status = 3  ";

                            }
                            else if (_unitRegistryRpt.Status == 6)
                            {
                                _status = " (A.Status = 2 or A.Posted = 1) and A.Revised = 0 and A.status not in (3,4)  ";

                            }
                            else if (_unitRegistryRpt.Status == 7)
                            {
                                _status = " (A.Status = 1 Or A.Status = 2 or A.Posted = 1) and  A.Revised = 0 and A.status not in (3,4)  ";

                            }

                            cmd.CommandText =

                            @"

                        Declare @Date datetime
                        set @date = @dateFrom

                        Select B.Name, B.ID CIF,C.Name fundName
                        , cast(D.NAV as numeric(18,4)) Nav, A.UnitAmount * cast(D.NAV as numeric(18,4)) CurrentNAV,A.UnitAmount UnitBalance,
                        E.DescOne ClientCategory
                        FROM dbo.FundClientPosition A
                        LEFT JOIN fundClient B ON A.FundclientPK = b.FundClientPK AND B.status IN (1,2)
                        LEFT JOIN Fund C ON A.FundPK = C.FundPK AND C.status IN (1,2)
                        LEFT JOIN CloseNAV D ON A.FundPK = D.FundPK AND D.Date = @date AND D.status IN (1,2)
                        LEFT JOIN dbo.MasterValue E ON B.ClientCategory = E.Code AND E.ID = 'ClientCategory'

                        WHERE A.Date = dbo.FWorkingDay(@date,-1) and A.UnitAmount > 0.1 " + _paramFund + _paramFundClient + @" ";

                            cmd.CommandTimeout = 0;


                            cmd.Parameters.AddWithValue("@DateFrom", _unitRegistryRpt.ValueDateFrom);
                            cmd.ExecuteNonQuery();

                            using (SqlDataReader dr0 = cmd.ExecuteReader())
                            {
                                if (!dr0.HasRows)
                                {
                                    return false;
                                }
                                else
                                {
                                    string filePath = Tools.ReportsPath + "SummaryClient" + "_" + _userID + ".xlsx";
                                    string pdfPath = Tools.ReportsPath + "SummaryClient" + "_" + _userID + ".pdf";
                                    FileInfo excelFile = new FileInfo(filePath);
                                    if (excelFile.Exists)
                                    {
                                        excelFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                        excelFile = new FileInfo(filePath);
                                    }


                                    // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                    using (ExcelPackage package = new ExcelPackage(excelFile))
                                    {
                                        package.Workbook.Properties.Title = "Summary Client";
                                        package.Workbook.Properties.Author = Tools.DefaultReportAuthor();
                                        package.Workbook.Properties.Comments = Tools.DefaultReportComments();
                                        package.Workbook.Properties.Company = Tools.DefaultReportCompany();
                                        package.Workbook.Properties.SetCustomPropertyValue("Checked by", _userID);
                                        package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", Tools.DefaultReportAssemblyName());

                                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Summary Client");


                                        //ATUR DATA GROUPINGNYA DULU
                                        List<SummaryClient> rList = new List<SummaryClient>();
                                        while (dr0.Read())
                                        {
                                            SummaryClient rSingle = new SummaryClient();
                                            rSingle.ClientName = Convert.ToString(dr0["Name"]);
                                            rSingle.CIF = Convert.ToString(dr0["CIF"]);
                                            rSingle.Fund = Convert.ToString(dr0["FundName"]);
                                            rSingle.Type = Convert.ToString(dr0["ClientCategory"]);
                                            rSingle.UnitBalance = Convert.ToDecimal(dr0["UnitBalance"]);
                                            rSingle.CurrentNAV = Convert.ToDecimal(dr0["CurrentNAV"]);
                                            rSingle.NAV = Convert.ToDecimal(dr0["NAV"]);

                                            rList.Add(rSingle);

                                        }

                                        var GroupByCategory =
                                         from r in rList
                                         orderby r.ClientName ascending
                                         group r by new { r.Fund, r.NAV } into rGroup
                                         select rGroup;

                                        int incRowExcel = 1;
                                        //int _rowEndBalance = 0;

                                        incRowExcel++;

                                        worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                        foreach (var rsHeader in GroupByCategory)
                                        {

                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Size = 20;
                                            worksheet.Cells["A" + incRowExcel + ":E" + incRowExcel].Merge = true;
                                            worksheet.Cells["A" + incRowExcel + ":E" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            incRowExcel = incRowExcel + 2;

                                            worksheet.Cells[incRowExcel, 1].Value = "Fund Manager";
                                            worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                                            worksheet.Cells[incRowExcel, 2].Value = " : " + _host.Get_CompanyName();
                                            incRowExcel++;

                                            int RowX = incRowExcel;
                                            worksheet.Cells[incRowExcel, 1].Value = "As of";
                                            worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;

                                            worksheet.Cells[incRowExcel, 2].Value = " : " + Convert.ToDateTime(_unitRegistryRpt.ValueDateFrom).ToString("dd/MMM/yyyy");
                                            incRowExcel++;

                                            int RowA = incRowExcel;
                                            int RowB = incRowExcel + 1;

                                            worksheet.Cells["A" + RowA + ":E" + RowA].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + RowB + ":E" + RowB].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 1].Value = "Fund";
                                            worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                                            worksheet.Cells[incRowExcel, 2].Value = " : " + rsHeader.Key.Fund;
                                            worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            incRowExcel++;

                                            worksheet.Cells[incRowExcel, 1].Value = "NAV/Unit";
                                            worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                                            worksheet.Cells[incRowExcel, 2].Value = " : " + rsHeader.Key.NAV;
                                            worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            worksheet.Cells[incRowExcel, 2].Style.Numberformat.Format = "#,##0.0000";
                                            incRowExcel++;

                                            worksheet.Cells["A" + incRowExcel + ":E" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + incRowExcel + ":E" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 1].Value = "CIF";
                                            worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 2].Value = "Client Name";
                                            worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 3].Value = "Type";
                                            worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 4].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 4].Value = "Unit Balance";
                                            worksheet.Cells[incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 5].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 5].Value = "Current NAV";
                                            worksheet.Cells[incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            int _startRowDetail = incRowExcel;
                                            int _endRowDetail = 0;
                                            //end area header

                                            incRowExcel++;
                                            foreach (var rsDetail in rsHeader)
                                            {

                                                worksheet.Cells[incRowExcel, 1].Value = rsDetail.CIF;
                                                worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 2].Value = rsDetail.ClientName;
                                                worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.Type;
                                                worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 4].Value = rsDetail.UnitBalance;
                                                worksheet.Cells[incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##0.0000";
                                                worksheet.Cells[incRowExcel, 5].Value = rsDetail.CurrentNAV;
                                                worksheet.Cells[incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.00";
                                                _endRowDetail = incRowExcel;
                                                incRowExcel++;

                                            }

                                            worksheet.Cells["A" + incRowExcel + ":E" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + incRowExcel + ":E" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells[incRowExcel, 2].Value = "Total";
                                            worksheet.Cells[incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                            worksheet.Cells[incRowExcel, 4].Formula = "SUM(D" + _startRowDetail + ":D" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 4].Calculate();
                                            worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##0.0000";
                                            worksheet.Cells[incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                            worksheet.Cells[incRowExcel, 5].Formula = "SUM(E" + _startRowDetail + ":E" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 5].Calculate();
                                            worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.0000";

                                            worksheet.Cells["A" + RowA + ":A" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["E" + RowA + ":E" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Medium;

                                            incRowExcel++;

                                            incRowExcel = incRowExcel + 7;
                                            worksheet.Cells[incRowExcel, 1].Value = "Disclaimer   : ";
                                            incRowExcel++;
                                            worksheet.Row(incRowExcel).Height = 50;
                                            worksheet.Cells[incRowExcel, 1].Value = Tools.DefaultDisclaimerReportFooterLeftText();
                                            worksheet.Cells["A" + incRowExcel + ":E" + incRowExcel].Style.WrapText = true;
                                            worksheet.Cells["A" + incRowExcel + ":E" + incRowExcel].Merge = true;
                                            worksheet.Cells["A" + incRowExcel + ":E" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            worksheet.Cells[incRowExcel, 1].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                                            incRowExcel = incRowExcel + 2;
                                            worksheet.Row(incRowExcel).PageBreak = _unitRegistryRpt.PageBreak;

                                        }


                                        // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                        worksheet.PrinterSettings.FitToPage = true;
                                        worksheet.PrinterSettings.FitToWidth = 1;
                                        worksheet.PrinterSettings.FitToHeight = 0;
                                        worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel - 2, 5];
                                        worksheet.Column(1).Width = 20;
                                        worksheet.Column(2).Width = 40;
                                        worksheet.Column(3).Width = 20;
                                        worksheet.Column(4).Width = 20;
                                        worksheet.Column(5).Width = 20;

                                        worksheet.PrinterSettings.Orientation = eOrientation.Portrait; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                        // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                        worksheet.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
                                        worksheet.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
                                        worksheet.View.ShowGridLines = Tools.DefaultReportShowGridLines();
                                        Image img = Image.FromFile(Tools.ReportImage);
                                        worksheet.HeaderFooter.OddHeader.InsertPicture(img, PictureAlignment.Left);
                                        worksheet.HeaderFooter.OddHeader.CenteredText = "&14 Client Summary Report";


                                        //worksheet.HeaderFooter.OddHeader.LeftAlignedText = Tools.DefaultReportHeaderLeftText();

                                        worksheet.HeaderFooter.OddFooter.RightAlignedText =
                                        string.Format("Page {0} of {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);

                                        //worksheet.HeaderFooter.OddFooter.CenteredText = Tools.DefaultReportFooterCenterText();
                                        worksheet.HeaderFooter.OddFooter.LeftAlignedText = Tools.DefaultReportFooterLeftText();

                                        package.Save();
                                        if (_unitRegistryRpt.DownloadMode == "PDF")
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
            #endregion

            #region Daily Transaction Report
            else if (_unitRegistryRpt.ReportName.Equals("Daily Transaction Report"))
            {
                try
                {
                    using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                    {
                        DbCon.Open();
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            string _statusSubs = "";
                            string _statusRedemp = "";
                            string _statusSwitch = "";
                            string _paramFund = "";
                            string _paramFundFrom = "";
                            string _paramFundTo = "";
                            string _paramAgent = "";
                            string _paramFundClient = "";


                            if (!_host.findString(_unitRegistryRpt.FundFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_unitRegistryRpt.FundFrom))
                            {
                                _paramFund = " And A.FundPK in ( " + _unitRegistryRpt.FundFrom + " ) ";
                                _paramFundFrom = " And A.FundPKFrom in ( " + _unitRegistryRpt.FundFrom + " ) ";
                                _paramFundTo = " And A.FundPKTo in ( " + _unitRegistryRpt.FundFrom + " ) ";
                            }
                            else
                            {
                                _paramFund = "";
                            }
                            if (!_host.findString(_unitRegistryRpt.AgentFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_unitRegistryRpt.AgentFrom))
                            {
                                _paramAgent = " And A.AgentPK in ( " + _unitRegistryRpt.AgentFrom + " ) ";
                            }
                            else
                            {
                                _paramAgent = "";
                            }
                            if (!_host.findString(_unitRegistryRpt.FundClientFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_unitRegistryRpt.FundClientFrom))
                            {
                                _paramFundClient = " And A.FundClientPK in ( " + _unitRegistryRpt.FundClientFrom + " ) ";
                            }
                            else
                            {
                                _paramFundClient = "";
                            }



                            if (_unitRegistryRpt.Status == 1)
                            {
                                _statusSubs = "  A.Status = 2 and A.Posted = 1 and A.Revised = 0 ";
                                _statusRedemp = "  A.Status = 2 and A.Posted = 1 and A.Revised = 0 ";
                                _statusSwitch = "  A.Status = 2 and A.Posted = 1 and A.Revised = 0 ";
                            }
                            else if (_unitRegistryRpt.Status == 2)
                            {
                                _statusSubs = "  A.Status = 2 and A.Posted = 1 and A.Revised = 1 ";
                                _statusRedemp = "  A.Status = 2 and A.Posted = 1 and A.Revised = 1 ";
                                _statusSwitch = "  A.Status = 2 and A.Posted = 1 and A.Revised = 1 ";
                            }
                            else if (_unitRegistryRpt.Status == 3)
                            {
                                _statusSubs = "  A.Status = 2 and A.Posted = 0 and A.Revised = 0 ";
                                _statusRedemp = "  A.Status = 2 and A.Posted = 0 and A.Revised = 0 ";
                                _statusSwitch = "  A.Status = 2 and A.Posted = 0 and A.Revised = 0 ";
                            }
                            else if (_unitRegistryRpt.Status == 4)
                            {
                                _statusSubs = "  A.Status = 1  ";
                                _statusRedemp = "  A.Status = 1  ";
                                _statusSwitch = "  A.Status = 1  ";
                            }
                            else if (_unitRegistryRpt.Status == 5)
                            {
                                _statusSubs = "  A.Status = 3  ";
                                _statusRedemp = "  A.Status = 3  ";
                                _statusSwitch = "  A.Status = 3  ";
                            }
                            else if (_unitRegistryRpt.Status == 6)
                            {
                                _statusSubs = "  (A.Status = 2 or A.Posted = 1) and A.Revised = 0 and A.status not in (3,4) ";
                                _statusRedemp = "  (A.Status = 2 or A.Posted = 1) and A.Revised = 0 and A.status not in (3,4) ";
                                _statusSwitch = "  (A.Status = 2 or A.Posted = 1) and A.Revised = 0 and A.status not in (3,4) ";
                            }
                            else if (_unitRegistryRpt.Status == 7)
                            {
                                _statusSubs = "  (A.Status = 1 Or A.Status = 2 or A.Posted = 1) and  A.Revised = 0 and A.status not in (3,4)";
                                _statusRedemp = "  (A.Status = 1 Or A.Status = 2 or A.Posted = 1) and A.Revised = 0  and A.status not in (3,4)";
                                _statusSwitch = "  (A.Status = 1 Or A.Status = 2 or A.Posted = 1) and A.Revised = 0  and A.status not in (3,4)";
                            }
                            cmd.CommandText =

                             @"
                            Select 
                            A.* from (
                            Select isnull(C.Name,'') FundName,isnull(E.Name,'') SalesName,isnull(F.Name,'') Branch,B.ID CIF,isnull(B.Name,'') Name
                            ,case when B.ClientCategory = 1 then 'Individual' else 'Institution' end [ClientCategory],D.ID Currency
                            ,A.ValueDate ValueDate
                            ,A.NAVDate Date
                            ,'Subscription' Remark
                            ,A.TotalUnitAmount NetSubs
                            ,A.NAV 
                            ,A.UnitAmount Unit
                            ,A.SubscriptionFeeAmount Fee
                            ,A.CashAmount Amount
                            ,A.FundClientPK,A.FundPK,A.Description,B.Name ClientName, B.ID ClientID,C.ID FundID,'' PaymentDate
                            from ClientSubscription A
                            left join FundClient B on A.FundClientPK = B.FundClientPK and B.status in (1,2)
                            left join Fund C on A.FundPK = C.FundPK and C.status in (1,2)
                            left join Currency D on A.CurrencyPK = D.CurrencyPK and D.Status in (1,2) 
                            left join Agent E on B.SellingAgentPK = E.AgentPK and E.status in (1,2)
                            left join Department F on E.DepartmentPK = F.DepartmentPK and F.status in (1,2)
                            where 
                            " + _statusSubs + _paramFund + _paramFundClient + @" and 
                            A.ValueDate between @DateFrom and @DateTo

                            UNION ALL

                            Select isnull(C.Name,'') FundName,isnull(E.Name,'') SalesName,isnull(F.Name,'') Branch,B.ID CIF,isnull(B.Name,'') Name
                            ,case when B.ClientCategory = 1 then 'Individual' else 'Institution' end [ClientCategory],D.ID Currency
                            ,A.ValueDate ValueDate
                            ,A.PaymentDate Date
                            ,'Redemption' Remark
                            ,A.TotalUnitAmount NetSubs
                            ,A.NAV 
                            ,A.UnitAmount Unit
                            ,A.RedemptionFeeAmount Fee
                            ,A.CashAmount Amount
                            ,A.FundClientPK,A.FundPK,A.Description,B.Name ClientName, B.ID ClientID,C.ID FundID,A.PaymentDate PaymentDate
                            from ClientRedemption A
                            left join FundClient B on A.FundClientPK = B.FundClientPK and B.status in (1,2)
                            left join Fund C on A.FundPK = C.FundPK and C.status in (1,2)
                            left join Currency D on A.CurrencyPK = D.CurrencyPK and D.Status in (1,2)
                            left join Agent E on B.SellingAgentPK = E.AgentPK and E.status in (1,2)
                            left join Department F on E.DepartmentPK = F.DepartmentPK and F.status in (1,2)
                            where 
                            " + _statusRedemp + _paramFund + _paramFundClient + @" and 
                            A.ValueDate between @DateFrom and @DateTo

                            UNION ALL

                            Select isnull(C.Name,'') FundName,isnull(E.Name,'') SalesName,isnull(F.Name,'') Branch,B.ID CIF,isnull(B.Name,'') Name
                            ,case when B.ClientCategory = 1 then 'Individual' else 'Institution' end [ClientCategory],D.ID Currency
                            ,A.ValueDate ValueDate
                            ,A.PaymentDate Date
                            ,'Switching From' Remark
                            ,A.TotalUnitAmountFundFrom NetSubs
                            ,A.NAVFundFrom NAV 
                            ,A.UnitAmount Unit
                            ,A.SwitchingFeeAmount Fee
                            ,A.CashAmount Amount
                            ,A.FundClientPK,A.FundPKFrom,A.Description,B.Name ClientName, B.ID ClientID,C.ID FundID,A.PaymentDate PaymentDate
                            from ClientSwitching A
                            left join FundClient B on A.FundClientPK = B.FundClientPK and B.status in (1,2)
                            left join Fund C on A.FundPKFrom = C.FundPK and C.status in (1,2)
                            left join Currency D on A.CurrencyPK = D.CurrencyPK and D.Status in (1,2)
                            left join Agent E on B.SellingAgentPK = E.AgentPK and E.status in (1,2)
                            left join Department F on E.DepartmentPK = F.DepartmentPK and F.status in (1,2)
                            where 
                            " + _statusSwitch + _paramFundFrom + _paramFundClient + @" and 
                            A.ValueDate between @DateFrom and @DateTo

                            UNION ALL

                            Select isnull(C.Name,'') FundName,isnull(E.Name,'') SalesName,isnull(F.Name,'') Branch,B.ID CIF,isnull(B.Name,'') Name
                            ,case when B.ClientCategory = 1 then 'Individual' else 'Institution' end [ClientCategory],D.ID Currency
                            ,A.ValueDate ValueDate
                            ,A.PaymentDate Date
                            ,'Switching In' Remark
                            ,A.TotalUnitAmountFundTo NetSubs
                            ,A.NAVFundTo NAV 
                            ,A.UnitAmount Unit
                            ,A.SwitchingFeeAmount Fee
                            ,A.CashAmount Amount
                            ,A.FundClientPK,A.FundPKTo,A.Description,B.Name ClientName, B.ID ClientID,C.ID FundID,A.PaymentDate PaymentDate
                            from ClientSwitching A
                            left join FundClient B on A.FundClientPK = B.FundClientPK and B.status in (1,2)
                            left join Fund C on A.FundPKTo = C.FundPK and C.status in (1,2)
                            left join Currency D on A.CurrencyPK = D.CurrencyPK and D.Status in (1,2)
                            left join Agent E on B.SellingAgentPK = E.AgentPK and E.status in (1,2)
                            left join Department F on E.DepartmentPK = F.DepartmentPK and F.status in (1,2)
                            where 
                            " + _statusSwitch + _paramFundTo + _paramFundClient + @" and 
                            A.ValueDate between @DateFrom and @DateTo

                            )A 
                             ";

                            cmd.CommandTimeout = 0;
                            cmd.Parameters.AddWithValue("@DateFrom", _unitRegistryRpt.ValueDateFrom);
                            cmd.Parameters.AddWithValue("@DateTo", _unitRegistryRpt.ValueDateTo);

                            using (SqlDataReader dr0 = cmd.ExecuteReader())
                            {
                                if (!dr0.HasRows)
                                {
                                    return false;
                                }
                                else
                                {
                                    string filePath = Tools.ReportsPath + "DailyTransactionReport" + "_" + _userID + ".xlsx";
                                    string pdfPath = Tools.ReportsPath + "DailyTransactionReport" + "_" + _userID + ".pdf";
                                    FileInfo excelFile = new FileInfo(filePath);
                                    if (excelFile.Exists)
                                    {
                                        excelFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                        excelFile = new FileInfo(filePath);
                                    }

                                    // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                    using (ExcelPackage package = new ExcelPackage(excelFile))
                                    {
                                        package.Workbook.Properties.Title = "UnitRegistryReport";
                                        package.Workbook.Properties.Author = Tools.DefaultReportAuthor();
                                        package.Workbook.Properties.Comments = Tools.DefaultReportComments();
                                        package.Workbook.Properties.Company = Tools.DefaultReportCompany();
                                        package.Workbook.Properties.SetCustomPropertyValue("Checked by", _userID);
                                        package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", Tools.DefaultReportAssemblyName());

                                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Daily Transaction Report");


                                        //ATUR DATA GROUPINGNYA DULU
                                        List<DailyTransactionReport> rList = new List<DailyTransactionReport>();
                                        while (dr0.Read())
                                        {
                                            DailyTransactionReport rSingle = new DailyTransactionReport();
                                            rSingle.ClientName = Convert.ToString(dr0["Name"]);
                                            rSingle.CIF = Convert.ToString(dr0["CIF"]);
                                            rSingle.Fund = Convert.ToString(dr0["FundID"]);
                                            rSingle.Type = Convert.ToString(dr0["Remark"]);
                                            rSingle.SalesName = Convert.ToString(dr0["SalesName"]);
                                            rSingle.Branch = Convert.ToString(dr0["Branch"]);
                                            rSingle.ValueDate = Convert.ToDateTime(dr0["ValueDate"]);
                                            rSingle.PaymentDate = Convert.ToDateTime(dr0["Date"]);
                                            rSingle.Fee = Convert.ToDecimal(dr0["Fee"]);
                                            rSingle.NetSubs = Convert.ToDecimal(dr0["NetSubs"]);
                                            rSingle.NAV = Convert.ToDecimal(dr0["NAV"]);
                                            rSingle.Unit = Convert.ToDecimal(dr0["Unit"]);
                                            rSingle.Remark = Convert.ToString(dr0["Description"]);
                                            rSingle.Amount = Convert.ToDecimal(dr0["Amount"]);
                                            rSingle.Currency = Convert.ToString(dr0["Currency"]);
                                            rSingle.Description = Convert.ToString(dr0["Description"]);
                                            rList.Add(rSingle);

                                        }


                                        var QueryByFundID =
                                            from r in rList
                                            group r by new { r.Type } into rGroup
                                            select rGroup;

                                        int incRowExcel = 1;

                                        foreach (var rsHeader in QueryByFundID)
                                        {
                                            incRowExcel = incRowExcel + 2;
                                            worksheet.Cells[incRowExcel, 1].Value = rsHeader.Key.Type;
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 2].Value = "Period";
                                            worksheet.Cells[incRowExcel, 3].Value = ": ";
                                            worksheet.Cells[incRowExcel, 4].Value = _unitRegistryRpt.ValueDateFrom + " - " + _unitRegistryRpt.ValueDateTo;
                                            worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "dd-MMM-yyyy";

                                            incRowExcel = incRowExcel + 2;
                                            //Row B = 3
                                            int RowB = incRowExcel;
                                            int RowG = incRowExcel + 1;


                                            worksheet.Cells[incRowExcel, 1].Value = "CIF No";
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells["A" + RowB + ":A" + RowG].Merge = true;
                                            worksheet.Cells["A" + RowB + ":A" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["A" + RowB + ":A" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 2].Value = "Fund";
                                            worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                            worksheet.Cells["B" + RowB + ":B" + RowG].Merge = true;
                                            worksheet.Cells["B" + RowB + ":B" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["B" + RowB + ":B" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;


                                            worksheet.Cells[incRowExcel, 3].Value = "Client Name";
                                            worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;
                                            worksheet.Cells["C" + RowB + ":D" + RowG].Merge = true;
                                            worksheet.Cells["C" + RowB + ":D" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["C" + RowB + ":D" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 5].Value = "Investor Type";
                                            worksheet.Cells[incRowExcel, 5].Style.Font.Bold = true;
                                            worksheet.Cells["E" + RowB + ":E" + RowG].Merge = true;
                                            worksheet.Cells["E" + RowB + ":E" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["E" + RowB + ":E" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;


                                            worksheet.Cells[incRowExcel, 6].Value = "Sales Name";
                                            worksheet.Cells[incRowExcel, 6].Style.Font.Bold = true;
                                            worksheet.Cells["F" + RowB + ":F" + RowG].Merge = true;
                                            worksheet.Cells["F" + RowB + ":F" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["F" + RowB + ":F" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;



                                            worksheet.Cells[incRowExcel, 7].Value = "Sales Branch";
                                            worksheet.Cells[incRowExcel, 7].Style.Font.Bold = true;
                                            worksheet.Cells["G" + RowB + ":G" + RowG].Merge = true;
                                            worksheet.Cells["G" + RowB + ":G" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["G" + RowB + ":G" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            if (rsHeader.Key.Type == "Subscription")
                                            {
                                                worksheet.Cells[incRowExcel, 8].Value = "Subscription Date";
                                                worksheet.Cells[incRowExcel, 8].Style.Font.Bold = true;
                                                worksheet.Cells["H" + RowB + ":H" + RowG].Merge = true;
                                                worksheet.Cells["H" + RowB + ":H" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells["H" + RowB + ":H" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 9].Value = "Subs. Amount";
                                                worksheet.Cells[incRowExcel, 9].Style.Font.Bold = true;
                                                worksheet.Cells["I" + RowB + ":I" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells["I" + RowB + ":I" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 10].Value = "Fee";
                                                worksheet.Cells[incRowExcel, 10].Style.Font.Bold = true;
                                                worksheet.Cells["J" + RowB + ":J" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells["J" + RowB + ":J" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 11].Value = "Net Subs.";
                                                worksheet.Cells[incRowExcel, 11].Style.Font.Bold = true;
                                                worksheet.Cells["K" + RowB + ":K" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells["K" + RowB + ":K" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 12].Value = "Unit";
                                                worksheet.Cells[incRowExcel, 12].Style.Font.Bold = true;
                                                worksheet.Cells["L" + RowB + ":L" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells["L" + RowB + ":L" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 13].Value = "NAV";
                                                worksheet.Cells[incRowExcel, 13].Style.Font.Bold = true;
                                                worksheet.Cells["M" + RowB + ":M" + RowG].Merge = true;
                                                worksheet.Cells["M" + RowB + ":M" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells["M" + RowB + ":M" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 14].Value = "Remark";
                                                worksheet.Cells[incRowExcel, 14].Style.Font.Bold = true;
                                                worksheet.Cells["N" + RowB + ":N" + RowG].Merge = true;
                                                worksheet.Cells["N" + RowB + ":N" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells["N" + RowB + ":N" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            }
                                            else if (rsHeader.Key.Type == "Redemption")
                                            {
                                                worksheet.Cells[incRowExcel, 8].Value = "Redemption Date";
                                                worksheet.Cells[incRowExcel, 8].Style.Font.Bold = true;
                                                worksheet.Cells["H" + RowB + ":H" + RowG].Merge = true;
                                                worksheet.Cells["H" + RowB + ":H" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells["H" + RowB + ":H" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 9].Value = "Payment Date";
                                                worksheet.Cells[incRowExcel, 9].Style.Font.Bold = true;
                                                worksheet.Cells["I" + RowB + ":I" + RowG].Merge = true;
                                                worksheet.Cells["I" + RowB + ":I" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells["I" + RowB + ":I" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 10].Value = "Red Amount";
                                                worksheet.Cells[incRowExcel, 10].Style.Font.Bold = true;
                                                worksheet.Cells["J" + RowB + ":J" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells["J" + RowB + ":J" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 11].Value = "Fee";
                                                worksheet.Cells[incRowExcel, 11].Style.Font.Bold = true;
                                                worksheet.Cells["K" + RowB + ":K" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells["K" + RowB + ":K" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 12].Value = "Net Red.";
                                                worksheet.Cells[incRowExcel, 12].Style.Font.Bold = true;
                                                worksheet.Cells["L" + RowB + ":L" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells["L" + RowB + ":L" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;



                                                worksheet.Cells[incRowExcel, 13].Value = "Unit";
                                                worksheet.Cells[incRowExcel, 13].Style.Font.Bold = true;
                                                worksheet.Cells["M" + RowB + ":M" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells["M" + RowB + ":M" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 14].Value = "NAV";
                                                worksheet.Cells[incRowExcel, 14].Style.Font.Bold = true;
                                                worksheet.Cells["N" + RowB + ":N" + RowG].Merge = true;
                                                worksheet.Cells["N" + RowB + ":N" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells["N" + RowB + ":N" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 15].Value = "Remark";
                                                worksheet.Cells[incRowExcel, 15].Style.Font.Bold = true;
                                                worksheet.Cells["O" + RowB + ":O" + RowG].Merge = true;
                                                worksheet.Cells["O" + RowB + ":O" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells["O" + RowB + ":O" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            }

                                            else if (rsHeader.Key.Type == "Switching In")
                                            {
                                                worksheet.Cells[incRowExcel, 8].Value = "Switching Date";
                                                worksheet.Cells[incRowExcel, 8].Style.Font.Bold = true;
                                                worksheet.Cells["H" + RowB + ":H" + RowG].Merge = true;
                                                worksheet.Cells["H" + RowB + ":H" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells["H" + RowB + ":H" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 9].Value = "Switch. Amount";
                                                worksheet.Cells[incRowExcel, 9].Style.Font.Bold = true;
                                                worksheet.Cells["I" + RowB + ":I" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells["I" + RowB + ":I" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 10].Value = "Fee";
                                                worksheet.Cells[incRowExcel, 10].Style.Font.Bold = true;
                                                worksheet.Cells["J" + RowB + ":J" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells["J" + RowB + ":J" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 11].Value = "Net Switch.";
                                                worksheet.Cells[incRowExcel, 11].Style.Font.Bold = true;
                                                worksheet.Cells["K" + RowB + ":K" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells["K" + RowB + ":K" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 12].Value = "Unit";
                                                worksheet.Cells[incRowExcel, 12].Style.Font.Bold = true;
                                                worksheet.Cells["L" + RowB + ":L" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells["L" + RowB + ":L" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 13].Value = "NAV";
                                                worksheet.Cells[incRowExcel, 13].Style.Font.Bold = true;
                                                worksheet.Cells["M" + RowB + ":M" + RowG].Merge = true;
                                                worksheet.Cells["M" + RowB + ":M" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells["M" + RowB + ":M" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 14].Value = "Remark";
                                                worksheet.Cells[incRowExcel, 14].Style.Font.Bold = true;
                                                worksheet.Cells["N" + RowB + ":N" + RowG].Merge = true;
                                                worksheet.Cells["N" + RowB + ":N" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells["N" + RowB + ":N" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            }
                                            else
                                            {
                                                worksheet.Cells[incRowExcel, 8].Value = "Switching Date";
                                                worksheet.Cells[incRowExcel, 8].Style.Font.Bold = true;
                                                worksheet.Cells["H" + RowB + ":H" + RowG].Merge = true;
                                                worksheet.Cells["H" + RowB + ":H" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells["H" + RowB + ":H" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 9].Value = "Payment Date";
                                                worksheet.Cells[incRowExcel, 9].Style.Font.Bold = true;
                                                worksheet.Cells["I" + RowB + ":I" + RowG].Merge = true;
                                                worksheet.Cells["I" + RowB + ":I" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells["I" + RowB + ":I" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 10].Value = "Switch Amount";
                                                worksheet.Cells[incRowExcel, 10].Style.Font.Bold = true;
                                                worksheet.Cells["J" + RowB + ":J" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells["J" + RowB + ":J" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 11].Value = "Fee";
                                                worksheet.Cells[incRowExcel, 11].Style.Font.Bold = true;
                                                worksheet.Cells["K" + RowB + ":K" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells["K" + RowB + ":K" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 12].Value = "Net Switch.";
                                                worksheet.Cells[incRowExcel, 12].Style.Font.Bold = true;
                                                worksheet.Cells["L" + RowB + ":L" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells["L" + RowB + ":L" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;



                                                worksheet.Cells[incRowExcel, 13].Value = "Unit";
                                                worksheet.Cells[incRowExcel, 13].Style.Font.Bold = true;
                                                worksheet.Cells["M" + RowB + ":M" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells["M" + RowB + ":M" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 14].Value = "NAV";
                                                worksheet.Cells[incRowExcel, 14].Style.Font.Bold = true;
                                                worksheet.Cells["N" + RowB + ":N" + RowG].Merge = true;
                                                worksheet.Cells["N" + RowB + ":N" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells["N" + RowB + ":N" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 15].Value = "Remark";
                                                worksheet.Cells[incRowExcel, 15].Style.Font.Bold = true;
                                                worksheet.Cells["O" + RowB + ":O" + RowG].Merge = true;
                                                worksheet.Cells["O" + RowB + ":O" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells["O" + RowB + ":O" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            }








                                            incRowExcel++;

                                            // Row C = 4
                                            int RowC = incRowExcel;

                                            incRowExcel++;
                                            //area header

                                            int _no = 1;
                                            int _startRowDetail = incRowExcel;
                                            int _endRowDetail = 0;
                                            var _fundID = "";
                                            foreach (var rsDetail in rsHeader)
                                            {
                                                //Row D = 5
                                                int RowD = incRowExcel;
                                                int RowE = incRowExcel + 1;

                                                if (rsHeader.Key.Type == "Subscription")
                                                {
                                                    worksheet.Cells["A" + RowB + ":N" + RowG].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                    worksheet.Cells["A" + RowB + ":N" + RowG].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                }
                                                else if (rsHeader.Key.Type == "Redemption")
                                                {
                                                    worksheet.Cells["A" + RowB + ":O" + RowG].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                    worksheet.Cells["A" + RowB + ":O" + RowG].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                }
                                                else if (rsHeader.Key.Type == "Switching In")
                                                {
                                                    worksheet.Cells["A" + RowB + ":N" + RowG].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                    worksheet.Cells["A" + RowB + ":N" + RowG].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                }
                                                else
                                                {
                                                    worksheet.Cells["A" + RowB + ":O" + RowG].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                    worksheet.Cells["A" + RowB + ":O" + RowG].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                }


                                                //area detail
                                                worksheet.Cells[incRowExcel, 1].Value = rsDetail.CIF;
                                                worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 1].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                                                worksheet.Cells[incRowExcel, 2].Value = rsDetail.Fund;
                                                worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 2].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.ClientName;
                                                worksheet.Cells["C" + incRowExcel + ":D" + incRowExcel].Merge = true;
                                                worksheet.Cells["C" + incRowExcel + ":D" + incRowExcel].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                                                worksheet.Cells[incRowExcel, 5].Value = rsDetail.Type;
                                                worksheet.Cells[incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                worksheet.Cells[incRowExcel, 5].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                                                worksheet.Cells[incRowExcel, 6].Value = rsDetail.SalesName;
                                                worksheet.Cells[incRowExcel, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                worksheet.Cells[incRowExcel, 6].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                                                worksheet.Cells[incRowExcel, 7].Value = rsDetail.Branch;
                                                worksheet.Cells[incRowExcel, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                worksheet.Cells[incRowExcel, 7].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                                                if (rsDetail.Type == "Subscription")
                                                {
                                                    worksheet.Cells[incRowExcel, 8].Value = rsDetail.ValueDate;
                                                    worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "dd-MMM-yyyy";

                                                    worksheet.Cells[incRowExcel, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                    worksheet.Cells[incRowExcel, 9].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                                                    worksheet.Cells[incRowExcel, 9].Value = rsDetail.Amount;
                                                    worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0";
                                                    worksheet.Cells[incRowExcel, 9].Style.VerticalAlignment = ExcelVerticalAlignment.Top;

                                                    worksheet.Cells[incRowExcel, 10].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                    worksheet.Cells[incRowExcel, 10].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                                                    worksheet.Cells[incRowExcel, 10].Value = rsDetail.Fee;
                                                    worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0";
                                                    worksheet.Cells[incRowExcel, 10].Style.VerticalAlignment = ExcelVerticalAlignment.Top;

                                                    worksheet.Cells[incRowExcel, 11].Value = rsDetail.NetSubs;
                                                    worksheet.Cells[incRowExcel, 11].Style.Numberformat.Format = "#,##0";

                                                    worksheet.Cells[incRowExcel, 12].Value = rsDetail.Unit;
                                                    worksheet.Cells[incRowExcel, 12].Style.Numberformat.Format = "#,##0.0000";

                                                    worksheet.Cells[incRowExcel, 13].Value = rsDetail.NAV;
                                                    worksheet.Cells[incRowExcel, 13].Style.Numberformat.Format = "#,##0.0000";

                                                    worksheet.Cells[incRowExcel, 14].Value = rsDetail.Description;
                                                    worksheet.Cells[incRowExcel, 14].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                    worksheet.Cells[incRowExcel, 14].Style.WrapText = true;
                                                    worksheet.Cells[incRowExcel, 14].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                                                }
                                                else if (rsHeader.Key.Type == "Redemption")
                                                {
                                                    worksheet.Cells[incRowExcel, 8].Value = rsDetail.ValueDate;
                                                    worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "dd-MMM-yyyy";

                                                    worksheet.Cells[incRowExcel, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                    worksheet.Cells[incRowExcel, 9].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                                                    worksheet.Cells[incRowExcel, 9].Value = rsDetail.PaymentDate;
                                                    worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "dd-MMM-yyyy";
                                                    worksheet.Cells[incRowExcel, 9].Style.VerticalAlignment = ExcelVerticalAlignment.Top;

                                                    worksheet.Cells[incRowExcel, 10].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                    worksheet.Cells[incRowExcel, 10].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                                                    worksheet.Cells[incRowExcel, 10].Value = rsDetail.Amount;
                                                    worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0";
                                                    worksheet.Cells[incRowExcel, 10].Style.VerticalAlignment = ExcelVerticalAlignment.Top;

                                                    worksheet.Cells[incRowExcel, 11].Value = rsDetail.Fee;
                                                    worksheet.Cells[incRowExcel, 11].Style.Numberformat.Format = "#,##0";

                                                    worksheet.Cells[incRowExcel, 12].Value = rsDetail.NetSubs;
                                                    worksheet.Cells[incRowExcel, 12].Style.Numberformat.Format = "#,##0.0000";

                                                    worksheet.Cells[incRowExcel, 13].Value = rsDetail.Unit;
                                                    worksheet.Cells[incRowExcel, 13].Style.Numberformat.Format = "#,##0.0000";

                                                    worksheet.Cells[incRowExcel, 14].Value = rsDetail.NAV;
                                                    worksheet.Cells[incRowExcel, 14].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                    worksheet.Cells[incRowExcel, 14].Style.WrapText = true;
                                                    worksheet.Cells[incRowExcel, 14].Style.VerticalAlignment = ExcelVerticalAlignment.Top;

                                                    worksheet.Cells[incRowExcel, 15].Value = rsDetail.Remark;
                                                    worksheet.Cells[incRowExcel, 15].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                    worksheet.Cells[incRowExcel, 15].Style.WrapText = true;
                                                    worksheet.Cells[incRowExcel, 15].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                                                }
                                                else if (rsHeader.Key.Type == "Switching In")
                                                {
                                                    worksheet.Cells[incRowExcel, 8].Value = rsDetail.ValueDate;
                                                    worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "dd-MMM-yyyy";

                                                    worksheet.Cells[incRowExcel, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                    worksheet.Cells[incRowExcel, 9].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                                                    worksheet.Cells[incRowExcel, 9].Value = rsDetail.Amount;
                                                    worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0";
                                                    worksheet.Cells[incRowExcel, 9].Style.VerticalAlignment = ExcelVerticalAlignment.Top;

                                                    worksheet.Cells[incRowExcel, 10].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                    worksheet.Cells[incRowExcel, 10].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                                                    worksheet.Cells[incRowExcel, 10].Value = rsDetail.Fee;
                                                    worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0";
                                                    worksheet.Cells[incRowExcel, 10].Style.VerticalAlignment = ExcelVerticalAlignment.Top;

                                                    worksheet.Cells[incRowExcel, 11].Value = rsDetail.NetSubs;
                                                    worksheet.Cells[incRowExcel, 11].Style.Numberformat.Format = "#,##0";

                                                    worksheet.Cells[incRowExcel, 12].Value = rsDetail.Unit;
                                                    worksheet.Cells[incRowExcel, 12].Style.Numberformat.Format = "#,##0.0000";

                                                    worksheet.Cells[incRowExcel, 13].Value = rsDetail.NAV;
                                                    worksheet.Cells[incRowExcel, 13].Style.Numberformat.Format = "#,##0.0000";

                                                    worksheet.Cells[incRowExcel, 14].Value = rsDetail.Description;
                                                    worksheet.Cells[incRowExcel, 14].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                    worksheet.Cells[incRowExcel, 14].Style.WrapText = true;
                                                    worksheet.Cells[incRowExcel, 14].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                                                }
                                                else
                                                {
                                                    worksheet.Cells[incRowExcel, 8].Value = rsDetail.ValueDate;
                                                    worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "dd-MMM-yyyy";

                                                    worksheet.Cells[incRowExcel, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                    worksheet.Cells[incRowExcel, 9].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                                                    worksheet.Cells[incRowExcel, 9].Value = rsDetail.PaymentDate;
                                                    worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "dd-MMM-yyyy";
                                                    worksheet.Cells[incRowExcel, 9].Style.VerticalAlignment = ExcelVerticalAlignment.Top;

                                                    worksheet.Cells[incRowExcel, 10].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                    worksheet.Cells[incRowExcel, 10].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                                                    worksheet.Cells[incRowExcel, 10].Value = rsDetail.Amount;
                                                    worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0";
                                                    worksheet.Cells[incRowExcel, 10].Style.VerticalAlignment = ExcelVerticalAlignment.Top;

                                                    worksheet.Cells[incRowExcel, 11].Value = rsDetail.Fee;
                                                    worksheet.Cells[incRowExcel, 11].Style.Numberformat.Format = "#,##0";

                                                    worksheet.Cells[incRowExcel, 12].Value = rsDetail.NetSubs;
                                                    worksheet.Cells[incRowExcel, 12].Style.Numberformat.Format = "#,##0.0000";

                                                    worksheet.Cells[incRowExcel, 13].Value = rsDetail.Unit;
                                                    worksheet.Cells[incRowExcel, 13].Style.Numberformat.Format = "#,##0.0000";

                                                    worksheet.Cells[incRowExcel, 14].Value = rsDetail.NAV;
                                                    worksheet.Cells[incRowExcel, 14].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                    worksheet.Cells[incRowExcel, 14].Style.WrapText = true;
                                                    worksheet.Cells[incRowExcel, 14].Style.VerticalAlignment = ExcelVerticalAlignment.Top;

                                                    worksheet.Cells[incRowExcel, 15].Value = rsDetail.Remark;
                                                    worksheet.Cells[incRowExcel, 15].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                    worksheet.Cells[incRowExcel, 15].Style.WrapText = true;
                                                    worksheet.Cells[incRowExcel, 15].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                                                }




                                                _endRowDetail = incRowExcel;
                                                _no++;
                                                incRowExcel++;
                                                _fundID = rsDetail.Fund;
                                            }
                                            if (rsHeader.Key.Type == "Subscription")
                                            {
                                                worksheet.Cells["A" + _endRowDetail + ":N" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                            }
                                            else if (rsHeader.Key.Type == "Redemption")
                                            {
                                                worksheet.Cells["A" + _endRowDetail + ":O" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                            }
                                            else if (rsHeader.Key.Type == "Switching In")
                                            {
                                                worksheet.Cells["A" + _endRowDetail + ":N" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                            }
                                            else
                                            {
                                                worksheet.Cells["A" + _endRowDetail + ":O" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                            }

                                        }




                                        incRowExcel++;

                                        //-----------------------------------
                                        using (SqlConnection DbCon1 = new SqlConnection(Tools.conString))
                                        {
                                            DbCon1.Open();
                                            using (SqlCommand cmd1 = DbCon1.CreateCommand())
                                            {
                                                cmd1.CommandText =

                                                @"
                                                Select A.NoRow,A.CurrencyID,A.Type,A.FundID,sum(A.TotalCashAmount)TotalCashAmount,sum(A.TotalUnitAmount)TotalUnitAmount from ( 
                                                Select  '1' NoRow,CU.ID CurrencyID,'Subscription' Type ,F.ID FundID,sum (TotalCashAmount)TotalCashAmount,sum (TotalUnitAmount)TotalUnitAmount 
                                                from ClientSubscription A left join Fund F on A.FundPK = F.fundPK and f.Status in (1,2)    
                                                left join Currency CU on A.CurrencyPK = CU.CurrencyPK and CU.Status in (1,2)   
                                                left join FundClient FC on A.FundClientPK = FC.FundClientPK and fc.Status in (1,2)    
                                                left join Agent AG on A.AgentPK = AG.AgentPK and AG.Status in (1,2)  
                                                left join Department D on D.DepartmentPK = AG.DepartmentPK and D.Status in (1,2) 
                                                where " + _statusSubs + _paramFund + _paramAgent + _paramFundClient + @"and 
                                                NAVDate Between @ValueDateFrom and @ValueDateTo  
                                                Group By F.ID,CU.ID 
                                                UNION ALL   
                                                Select '2' NoRow,CU.ID CurrencyID,'Redemption' Type,F.ID FundID,sum (TotalCashAmount)TotalCashAmount,sum (TotalUnitAmount)TotalUnitAmount   
                                                from ClientRedemption A left join Fund F on A.FundPK = F.fundPK and f.Status in (1,2)    
                                                left join Currency CU on A.CurrencyPK = CU.CurrencyPK and CU.Status in (1,2)    
                                                left join FundClient FC on A.FundClientPK = FC.FundClientPK and fc.Status in (1,2)    
                                                left join Agent AG on A.AgentPK = AG.AgentPK and AG.Status in (1,2)  
                                                left join Department D on D.DepartmentPK = AG.DepartmentPK and D.Status in (1,2)
                                                where  " + _statusRedemp + _paramFund + _paramAgent + _paramFundClient + @"and 
                                                NAVDate Between @ValueDateFrom and @ValueDateTo  
                                                Group By F.ID,CU.ID
                                                UNION ALL   
                                                Select '3' NoRow,CU.ID CurrencyID,'Switching From' Type,F.ID FundID,sum (TotalCashAmountFundFrom)TotalCashAmount,sum (TotalUnitAmountFundFrom)TotalUnitAmount   
                                                from ClientSwitching A left join Fund F on A.FundPKFrom = F.fundPK and f.Status in (1,2)    
                                                left join Currency CU on A.CurrencyPK = CU.CurrencyPK and CU.Status in (1,2)    
                                                left join FundClient FC on A.FundClientPK = FC.FundClientPK and fc.Status in (1,2)    
                                                left join Agent AG on A.AgentPK = AG.AgentPK and AG.Status in (1,2)  
                                                left join Department D on D.DepartmentPK = AG.DepartmentPK and D.Status in (1,2)
                                                where  " + _statusSwitch + _paramFundFrom + _paramAgent + _paramFundClient + @"and 
                                                NAVDate Between @ValueDateFrom and @ValueDateTo  
                                                Group By F.ID,CU.ID
                                                UNION ALL   
                                                Select '4' NoRow,CU.ID CurrencyID,'Switching In' Type,F.ID FundID,sum (TotalCashAmountFundTo)TotalCashAmount,sum (TotalUnitAmountFundTo)TotalUnitAmount   
                                                from ClientSwitching A left join Fund F on A.FundPKTo = F.fundPK and f.Status in (1,2)    
                                                left join Currency CU on A.CurrencyPK = CU.CurrencyPK and CU.Status in (1,2)    
                                                left join FundClient FC on A.FundClientPK = FC.FundClientPK and fc.Status in (1,2)    
                                                left join Agent AG on A.AgentPK = AG.AgentPK and AG.Status in (1,2)  
                                                left join Department D on D.DepartmentPK = AG.DepartmentPK and D.Status in (1,2)
                                                where  " + _statusSwitch + _paramFundTo + _paramAgent + _paramFundClient + @"and 
                                                NAVDate Between @ValueDateFrom and @ValueDateTo  
                                                Group By F.ID,CU.ID

                                                )A   
                                                Group by A.NoRow,A.Type,A.FundID,A.CurrencyID
                                                order by A.NoRow,A.Type,A.FundID Asc 
                                                ";
                                                cmd1.CommandTimeout = 0;
                                                cmd1.Parameters.AddWithValue("@ValueDateFrom", _unitRegistryRpt.ValueDateFrom);
                                                cmd1.Parameters.AddWithValue("@ValueDateTo", _unitRegistryRpt.ValueDateTo);
                                                cmd1.Parameters.AddWithValue("@FundFrom", _unitRegistryRpt.FundFrom);
                                                cmd1.Parameters.AddWithValue("@AgentFrom", _unitRegistryRpt.AgentFrom);
                                                cmd1.Parameters.AddWithValue("@FundClientFrom", _unitRegistryRpt.FundClientFrom);
                                                cmd1.Parameters.AddWithValue("@DepartmentFrom", _unitRegistryRpt.DepartmentFrom);

                                                cmd1.ExecuteNonQuery();


                                                using (SqlDataReader dr1 = cmd1.ExecuteReader())
                                                {
                                                    if (!dr1.HasRows)
                                                    {
                                                        return false;
                                                    }
                                                    else
                                                    {


                                                        // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                                        using (ExcelPackage package1 = new ExcelPackage(excelFile))
                                                        {

                                                            //ATUR DATA GROUPINGNYA DULU
                                                            List<UnitRegistryRpt> rList1 = new List<UnitRegistryRpt>();
                                                            while (dr1.Read())
                                                            {
                                                                UnitRegistryRpt rSingle1 = new UnitRegistryRpt();
                                                                rSingle1.FundID = Convert.ToString(dr1["FundID"]);
                                                                rSingle1.CashBalance = Convert.ToDecimal(dr1["TotalCashAmount"]);
                                                                rSingle1.UnitBalance = Convert.ToDecimal(dr1["TotalUnitAmount"]);
                                                                rSingle1.Type = Convert.ToString(dr1["Type"]);
                                                                rSingle1.CurrencyID = Convert.ToString(dr1["CurrencyID"]);
                                                                rList1.Add(rSingle1);

                                                            }


                                                            var QueryByFundID1 =
                                                                from r1 in rList1
                                                                group r1 by new { r1.Type, r1.NAVDate } into rGroup1
                                                                select rGroup1;

                                                            incRowExcel = incRowExcel + 6;
                                                            int _endRowDetailZ = 0;


                                                            foreach (var rsHeader1 in QueryByFundID1)
                                                            {
                                                                worksheet.Cells[incRowExcel, 1].Value = "Total " + rsHeader1.Key.Type + " : ";
                                                                worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                                                worksheet.Cells["A" + incRowExcel + ":B" + incRowExcel].Merge = true;
                                                                worksheet.Cells["A" + incRowExcel + ":B" + incRowExcel].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                                worksheet.Cells["A" + incRowExcel + ":B" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                                incRowExcel = incRowExcel + 2;
                                                                //Row B = 3
                                                                int RowBZ = incRowExcel;
                                                                int RowGZ = incRowExcel + 1;


                                                                worksheet.Cells[incRowExcel, 1].Value = "No";
                                                                worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                                                worksheet.Cells["A" + RowBZ + ":A" + RowGZ].Merge = true;
                                                                worksheet.Cells["A" + RowBZ + ":A" + RowGZ].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                                worksheet.Cells["A" + RowBZ + ":A" + RowGZ].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                                worksheet.Cells[incRowExcel, 2].Value = "Fund";
                                                                worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                                                worksheet.Cells["B" + RowBZ + ":B" + RowGZ].Merge = true;
                                                                worksheet.Cells["B" + RowBZ + ":B" + RowGZ].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                                worksheet.Cells["B" + RowBZ + ":B" + RowGZ].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                                if (rsHeader1.Key.Type == "Subscription")
                                                                {
                                                                    worksheet.Cells[incRowExcel, 3].Value = "Amount";
                                                                    worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;
                                                                    worksheet.Cells["C" + RowBZ + ":D" + RowGZ].Merge = true;
                                                                    worksheet.Cells["C" + RowBZ + ":D" + RowGZ].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                                    worksheet.Cells["C" + RowBZ + ":D" + RowGZ].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                                    worksheet.Cells[incRowExcel, 5].Value = "Currency";
                                                                    worksheet.Cells[incRowExcel, 5].Style.Font.Bold = true;
                                                                    worksheet.Cells["E" + RowBZ + ":E" + RowGZ].Merge = true;
                                                                    worksheet.Cells["E" + RowBZ + ":E" + RowGZ].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                                    worksheet.Cells["E" + RowBZ + ":E" + RowGZ].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                                }
                                                                else if (rsHeader1.Key.Type == "Redemption")
                                                                {
                                                                    worksheet.Cells[incRowExcel, 3].Value = "Unit";
                                                                    worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;
                                                                    worksheet.Cells["C" + RowBZ + ":D" + RowGZ].Merge = true;
                                                                    worksheet.Cells["C" + RowBZ + ":D" + RowGZ].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                                    worksheet.Cells["C" + RowBZ + ":D" + RowGZ].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                                    worksheet.Cells[incRowExcel, 5].Value = "Amount";
                                                                    worksheet.Cells[incRowExcel, 5].Style.Font.Bold = true;
                                                                    worksheet.Cells["E" + RowBZ + ":E" + RowGZ].Merge = true;
                                                                    worksheet.Cells["E" + RowBZ + ":E" + RowGZ].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                                    worksheet.Cells["E" + RowBZ + ":E" + RowGZ].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                                    worksheet.Cells[incRowExcel, 6].Value = "Currency";
                                                                    worksheet.Cells[incRowExcel, 6].Style.Font.Bold = true;
                                                                    worksheet.Cells["F" + RowBZ + ":F" + RowGZ].Merge = true;
                                                                    worksheet.Cells["F" + RowBZ + ":F" + RowGZ].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                                    worksheet.Cells["F" + RowBZ + ":F" + RowGZ].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                                }
                                                                else if (rsHeader1.Key.Type == "Switching In")
                                                                {
                                                                    worksheet.Cells[incRowExcel, 3].Value = "Amount";
                                                                    worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;
                                                                    worksheet.Cells["C" + RowBZ + ":D" + RowGZ].Merge = true;
                                                                    worksheet.Cells["C" + RowBZ + ":D" + RowGZ].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                                    worksheet.Cells["C" + RowBZ + ":D" + RowGZ].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                                    worksheet.Cells[incRowExcel, 5].Value = "Currency";
                                                                    worksheet.Cells[incRowExcel, 5].Style.Font.Bold = true;
                                                                    worksheet.Cells["E" + RowBZ + ":E" + RowGZ].Merge = true;
                                                                    worksheet.Cells["E" + RowBZ + ":E" + RowGZ].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                                    worksheet.Cells["E" + RowBZ + ":E" + RowGZ].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                                }
                                                                else
                                                                {
                                                                    worksheet.Cells[incRowExcel, 3].Value = "Unit";
                                                                    worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;
                                                                    worksheet.Cells["C" + RowBZ + ":D" + RowGZ].Merge = true;
                                                                    worksheet.Cells["C" + RowBZ + ":D" + RowGZ].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                                    worksheet.Cells["C" + RowBZ + ":D" + RowGZ].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                                    worksheet.Cells[incRowExcel, 5].Value = "Amount";
                                                                    worksheet.Cells[incRowExcel, 5].Style.Font.Bold = true;
                                                                    worksheet.Cells["E" + RowBZ + ":E" + RowGZ].Merge = true;
                                                                    worksheet.Cells["E" + RowBZ + ":E" + RowGZ].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                                    worksheet.Cells["E" + RowBZ + ":E" + RowGZ].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                                    worksheet.Cells[incRowExcel, 6].Value = "Currency";
                                                                    worksheet.Cells[incRowExcel, 6].Style.Font.Bold = true;
                                                                    worksheet.Cells["F" + RowBZ + ":F" + RowGZ].Merge = true;
                                                                    worksheet.Cells["F" + RowBZ + ":F" + RowGZ].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                                    worksheet.Cells["F" + RowBZ + ":F" + RowGZ].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                                }


                                                                incRowExcel++;

                                                                // Row C = 4
                                                                int RowCZ = incRowExcel;

                                                                incRowExcel++;
                                                                //area header

                                                                int _noZ = 1;
                                                                int _startRowDetailZ = incRowExcel;
                                                                foreach (var rsDetail1 in rsHeader1)
                                                                {
                                                                    //Row D = 5
                                                                    int RowDZ = incRowExcel;
                                                                    int RowEZ = incRowExcel + 1;


                                                                    //ThickBox Border

                                                                    if (rsDetail1.Type == "Subscription")
                                                                    {
                                                                        worksheet.Cells["A" + RowBZ + ":E" + RowGZ].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                                        worksheet.Cells["A" + RowBZ + ":E" + RowGZ].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                                    }
                                                                    else if (rsDetail1.Type == "Redemption")
                                                                    {
                                                                        worksheet.Cells["A" + RowBZ + ":F" + RowGZ].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                                        worksheet.Cells["A" + RowBZ + ":F" + RowGZ].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                                    }
                                                                    else if (rsDetail1.Type == "Switching In")
                                                                    {
                                                                        worksheet.Cells["A" + RowBZ + ":E" + RowGZ].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                                        worksheet.Cells["A" + RowBZ + ":E" + RowGZ].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                                    }
                                                                    else
                                                                    {
                                                                        worksheet.Cells["A" + RowBZ + ":F" + RowGZ].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                                        worksheet.Cells["A" + RowBZ + ":F" + RowGZ].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                                    }


                                                                    //area detail
                                                                    worksheet.Cells[incRowExcel, 1].Value = _noZ;
                                                                    worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                                    worksheet.Cells[incRowExcel, 2].Value = rsDetail1.FundID;
                                                                    worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                                    if (rsDetail1.Type == "Subscription")
                                                                    {
                                                                        worksheet.Cells[incRowExcel, 3].Value = rsDetail1.CashBalance;
                                                                        worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "#,##0.00";
                                                                        worksheet.Cells["C" + incRowExcel + ":D" + incRowExcel].Merge = true;
                                                                        worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                                        worksheet.Cells[incRowExcel, 5].Value = rsDetail1.CurrencyID;
                                                                        worksheet.Cells[incRowExcel, 5].Merge = true;
                                                                        worksheet.Cells[incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                                    }
                                                                    else if (rsDetail1.Type == "Redemption")
                                                                    {
                                                                        worksheet.Cells[incRowExcel, 3].Value = rsDetail1.UnitBalance;
                                                                        worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "#,##0.000000";
                                                                        worksheet.Cells["C" + incRowExcel + ":D" + incRowExcel].Merge = true;
                                                                        worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                                        worksheet.Cells[incRowExcel, 5].Value = rsDetail1.CashBalance;
                                                                        worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.00";
                                                                        worksheet.Cells[incRowExcel, 5].Merge = true;
                                                                        worksheet.Cells[incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                                        worksheet.Cells[incRowExcel, 6].Value = rsDetail1.CurrencyID;
                                                                        worksheet.Cells[incRowExcel, 6].Merge = true;
                                                                        worksheet.Cells[incRowExcel, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                                    }
                                                                    else if (rsDetail1.Type == "Switching In")
                                                                    {
                                                                        worksheet.Cells[incRowExcel, 3].Value = rsDetail1.CashBalance;
                                                                        worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "#,##0.00";
                                                                        worksheet.Cells["C" + incRowExcel + ":D" + incRowExcel].Merge = true;
                                                                        worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                                        worksheet.Cells[incRowExcel, 5].Value = rsDetail1.CurrencyID;
                                                                        worksheet.Cells[incRowExcel, 5].Merge = true;
                                                                        worksheet.Cells[incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                                    }
                                                                    else
                                                                    {
                                                                        worksheet.Cells[incRowExcel, 3].Value = rsDetail1.UnitBalance;
                                                                        worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "#,##0.000000";
                                                                        worksheet.Cells["C" + incRowExcel + ":D" + incRowExcel].Merge = true;
                                                                        worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                                        worksheet.Cells[incRowExcel, 5].Value = rsDetail1.CashBalance;
                                                                        worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.00";
                                                                        worksheet.Cells[incRowExcel, 5].Merge = true;
                                                                        worksheet.Cells[incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                                        worksheet.Cells[incRowExcel, 6].Value = rsDetail1.CurrencyID;
                                                                        worksheet.Cells[incRowExcel, 6].Merge = true;
                                                                        worksheet.Cells[incRowExcel, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                                    }


                                                                    _endRowDetailZ = incRowExcel;
                                                                    _noZ++;
                                                                    incRowExcel++;

                                                                }


                                                                if (rsHeader1.Key.Type == "Subscription")
                                                                {
                                                                    worksheet.Cells["A" + _endRowDetailZ + ":E" + _endRowDetailZ].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                                }
                                                                else if (rsHeader1.Key.Type == "Redemption")
                                                                {
                                                                    worksheet.Cells["A" + _endRowDetailZ + ":F" + _endRowDetailZ].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                                }
                                                                else if (rsHeader1.Key.Type == "Switching In")
                                                                {
                                                                    worksheet.Cells["A" + _endRowDetailZ + ":E" + _endRowDetailZ].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                                }
                                                                else
                                                                {
                                                                    worksheet.Cells["A" + _endRowDetailZ + ":F" + _endRowDetailZ].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                                }


                                                                worksheet.Cells[_endRowDetailZ + 1, 4].Style.Numberformat.Format = "#,##0";
                                                                worksheet.Cells[_endRowDetailZ + 1, 4].Formula = "SUM(C" + _startRowDetailZ + ":D" + _endRowDetailZ + ")";
                                                                worksheet.Cells[_endRowDetailZ + 1, 4].Calculate();
                                                                worksheet.Cells[_endRowDetailZ + 1, 4].Style.Font.Bold = true;



                                                                if (rsHeader1.Key.Type == "Subscription")
                                                                {
                                                                    worksheet.Cells[_endRowDetailZ + 1, 4].Style.Numberformat.Format = "#,##0.00";
                                                                }
                                                                else if (rsHeader1.Key.Type == "Redemption")
                                                                {
                                                                    worksheet.Cells[_endRowDetailZ + 1, 4].Style.Numberformat.Format = "#,##0.000000";
                                                                    worksheet.Cells[_endRowDetailZ + 1, 5].Style.Numberformat.Format = "#,##0.00";
                                                                    worksheet.Cells[_endRowDetailZ + 1, 5].Formula = "SUM(E" + _startRowDetailZ + ":E" + _endRowDetailZ + ")";
                                                                    worksheet.Cells[_endRowDetailZ + 1, 5].Calculate();
                                                                    worksheet.Cells[_endRowDetailZ + 1, 5].Style.Font.Bold = true;
                                                                }
                                                                else if (rsHeader1.Key.Type == "Switching In")
                                                                {
                                                                    worksheet.Cells[_endRowDetailZ + 1, 4].Style.Numberformat.Format = "#,##0.00";
                                                                }
                                                                else
                                                                {
                                                                    worksheet.Cells[_endRowDetailZ + 1, 4].Style.Numberformat.Format = "#,##0.000000";
                                                                    worksheet.Cells[_endRowDetailZ + 1, 5].Style.Numberformat.Format = "#,##0.00";
                                                                    worksheet.Cells[_endRowDetailZ + 1, 5].Formula = "SUM(E" + _startRowDetailZ + ":E" + _endRowDetailZ + ")";
                                                                    worksheet.Cells[_endRowDetailZ + 1, 5].Calculate();
                                                                    worksheet.Cells[_endRowDetailZ + 1, 5].Style.Font.Bold = true;
                                                                }



                                                                incRowExcel++;
                                                            }




                                                            //string _rangeA1 = "A:M" + incRowExcel;
                                                            //using (ExcelRange r = worksheet.Cells[_rangeA1]) // KALO  KOLOM 1 SAMPE 9 A-I
                                                            //{
                                                            //    r.Style.Font.Size = 22;
                                                            //}
                                                        }

                                                    }
                                                }
                                            }
                                        }



                                        int _lastRow = incRowExcel;

                                        incRowExcel = incRowExcel + 7;
                                        worksheet.Cells[incRowExcel, 1].Value = "Disclaimer   : ";
                                        incRowExcel++;
                                        worksheet.Row(incRowExcel).Height = 50;
                                        worksheet.Cells[incRowExcel, 1].Value = Tools.DefaultDisclaimerReportFooterLeftText();
                                        worksheet.Cells["A" + incRowExcel + ":I" + incRowExcel].Style.WrapText = true;
                                        worksheet.Cells["A" + incRowExcel + ":I" + incRowExcel].Merge = true;
                                        worksheet.Cells["A" + incRowExcel + ":I" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                        worksheet.Cells[incRowExcel, 1].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                                        incRowExcel++;
                                        worksheet.Row(incRowExcel).PageBreak = _unitRegistryRpt.PageBreak;

                                        string _rangeA = "A:O" + incRowExcel;
                                        using (ExcelRange r = worksheet.Cells[_rangeA]) // KALO  KOLOM 1 SAMPE 9 A-I
                                        {
                                            r.Style.Font.Size = 12;
                                        }

                                        //worksheet.DeleteRow(_lastRow);

                                        worksheet.PrinterSettings.FitToPage = true;
                                        worksheet.PrinterSettings.FitToWidth = 1;
                                        worksheet.PrinterSettings.FitToHeight = 0;
                                        worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel - 1, 15];
                                        worksheet.Column(1).Width = 15;
                                        worksheet.Column(2).Width = 10;
                                        worksheet.Column(3).Width = 2;
                                        worksheet.Column(4).Width = 60;
                                        worksheet.Column(5).Width = 20;
                                        worksheet.Column(6).Width = 40;
                                        worksheet.Column(7).Width = 20;
                                        worksheet.Column(8).Width = 20;
                                        worksheet.Column(9).Width = 20;
                                        worksheet.Column(10).Width = 20;
                                        worksheet.Column(11).Width = 20;
                                        worksheet.Column(12).Width = 20;
                                        worksheet.Column(13).Width = 15;
                                        worksheet.Column(14).Width = 15;
                                        worksheet.Column(15).Width = 30;



                                        // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                        //worksheet.Cells["A3:I14"].AutoFitColumns(); // UNTUK AUTO FIT UKURAN TULISAN DENGAN KOLOM, KEBETULAN INI BUAT HEADER AJA DIPAKENYA
                                        //worksheetApproved.Cells["A3:E3"].AutoFitColumns();
                                        worksheet.PrinterSettings.Orientation = eOrientation.Landscape; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                        worksheet.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN

                                        worksheet.HeaderFooter.OddHeader.CenteredText = "\n \n \n \n \n \n \n &18&B Daily Total Transaction Report ";



                                        // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                        worksheet.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
                                        worksheet.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
                                        worksheet.View.PageLayoutView = Tools.DefaultReportPageLayoutView();
                                        worksheet.View.ShowGridLines = Tools.DefaultReportShowGridLines();
                                        Image img = Image.FromFile(Tools.ReportImage);
                                        worksheet.HeaderFooter.OddHeader.InsertPicture(img, PictureAlignment.Left);
                                        //worksheet.HeaderFooter.OddHeader.CenteredText = Tools.DefaultReportHeaderCenterText();
                                        //worksheet.HeaderFooter.OddHeader.LeftAlignedText = Tools.DefaultReportHeaderLeftText();

                                        worksheet.HeaderFooter.OddFooter.RightAlignedText =
                                        string.Format("Page {0} of {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);

                                        ////worksheet.HeaderFooter.OddFooter.CenteredText = Tools.DefaultReportFooterCenterText();
                                        worksheet.HeaderFooter.OddFooter.LeftAlignedText = Tools.DefaultReportFooterLeftText();



                                        package.Save();
                                        if (_unitRegistryRpt.DownloadMode == "PDF")
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
            #endregion

            #region Management Fee Per Client
            if (_unitRegistryRpt.ReportName.Equals("Management Fee Per Client"))
            {
                try
                {
                    using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                    {
                        DbCon.Open();
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            string _paramFund = "";
                            string _paramFundClient = "";
                            string _paramAgent = "";

                            if (!_host.findString(_unitRegistryRpt.FundFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_unitRegistryRpt.FundFrom))
                            {
                                _paramFund = "And A.FundPK in ( " + _unitRegistryRpt.FundFrom + " ) ";
                            }
                            else
                            {
                                _paramFund = "";
                            }

                            if (!_host.findString(_unitRegistryRpt.FundClientFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_unitRegistryRpt.FundClientFrom))
                            {
                                _paramFundClient = "And A.FundClientPK in ( " + _unitRegistryRpt.FundClientFrom + " ) ";
                            }
                            else
                            {
                                _paramFundClient = "";
                            }

                            if (!_host.findString(_unitRegistryRpt.AgentFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_unitRegistryRpt.AgentFrom))
                            {
                                _paramAgent = "And E.AgentPK in ( " + _unitRegistryRpt.AgentFrom + " ) ";
                            }
                            else
                            {
                                _paramAgent = "";
                            }

                            cmd.CommandText =
                            @"
                                Declare @StartDate datetime
                                Declare @EndDate datetime

                                set @StartDate =  dbo.fworkingday(@dateFrom,-2)
                                set @EndDate = dbo.fworkingday(@DateTo,-2)

                                Create table #FCP
                                (
                                Date datetime,
                                UnitAmount Numeric(20,4),
                                ManagementFeePercent Numeric(18,8),
                                RebateFeePercent Numeric(18,8),
                                FundPK int,
                                FundClientPK int,
                                NAVDate datetime,
                                SeriesDay int
                                )
                                insert into #FCP
                                Select Date,UnitAmount,[dbo].[FgetManagementFeePercentByDate] (A.Date,A.FundPK) ManagementFeePercent,
                                [dbo].[FgetRebateFeePercentByDate] (A.Date,A.FundPK,A.FundClientPK) RebateFeePercent,
                                A.FundPK,A.FundClientPK,dbo.fworkingday(A.Date,1) NAVDate 
                                , Case when dbo.fworkingday(A.Date,1) < @DateFrom and dbo.CheckTodayIsHoliday(@DateFrom) = 1 
                                then datediff(day,@Datefrom,dbo.fworkingday(@DateFrom,1)) + 1
                                when dbo.fworkingday(A.Date,1) < @DateFrom 
                                then datediff(day,@Datefrom,dbo.fworkingday(@DateFrom,1))  
                                when dbo.fworkingday(A.Date,1) = dbo.fworkingday(@DateTo,-1) and dbo.CheckTodayIsHoliday(dateadd(day,-1,@DateTo)) = 1
                                then datediff(day,dbo.fworkingday(A.Date,1),dateadd(day,-1,@DateTo))  + 1
                                when dbo.fworkingday(A.Date,1) = dbo.fworkingday(@DateTo,-1) 
                                then 1
                                else case when [dbo].CheckIsTommorowHoliday(dbo.fworkingday(A.Date,1)) = 1 
                                then dbo.CheckTotalSeriesHoliday(
                                case when dbo.fworkingday(A.Date,1) < @DateFrom then @DateFrom 	
                                else case when dbo.fworkingday(A.Date,1) < @DateTo then  dbo.fworkingday(A.Date,1) 
                                else @DateTo end end
                                ) else 1 end 
                                end SeriesDay
                                from
                                FundClientPosition A  
                                where	A.Date between @StartDate and @EndDate 
                                " + _paramFundClient + _paramFund + @"


                                create table #tempMFee
                                (
                                Date datetime,
                                AUM decimal (22,8),
                                UnitAmount decimal(22,8),
                                NAV decimal (22,8),
                                MGTFee decimal (22,8),
                                RebateFee decimal (22,8),
                                FundClientPK int,
                                FundPK int
                                )

                                insert into #tempMFee
                                Select A.NAVDate,isnull(A.UnitAmount,0) * isnull(B.NAV,0),A.UnitAmount,B.NAV
                                ,A.UnitAmount * B.NAV * A.ManagementFeePercent /365 /100 * SeriesDay
                                ,isnull(A.UnitAmount * B.NAV * A.ManagementFeePercent /365 /100 * SeriesDay * (A.RebateFeePercent/100),A.UnitAmount * B.NAV * A.ManagementFeePercent /365 /100 * SeriesDay * 0)
                                ,A.FundClientPK, A.FundPK
                                from #FCP A
                                left join CloseNAV B on A.NAVDate = B.Date and B.status in (1,2) and A.FundPK = B.FundPK	
                                order by A.date	

                                select @DateFrom DateFrom, @DateTo DateTo,A.Date,
                                ISNULL(MF.NAV,dbo.FgetLastCloseNav(A.Date,A.fundPK)) NAV
                                ,isnull(MFee.MGTFee,0) + (case when dbo.CheckTodayIsHoliday(@DateTo) = 1 and A.Date = dbo.fworkingday(@DateTo,-1) then  
                                isnull(MF.AUM,dbo.FgetLastAUM(A.Date,A.fundPK)) * [dbo].[FgetManagementFeePercentByDate] (A.Date,A.FundPK) / 100 / 365 * datediff(day,A.Date,@DateTo) else 0 end)  MGTFee 
                                ,isnull(MFee.MGTFee,0),isnull(MF.AUM,dbo.FgetLastAUM(A.Date,A.fundPK)) AUM
                                ,C.Name FundName,[dbo].[FgetManagementFeePercentByDate] (A.Date,A.FundPK) /100 ManagementFeePercent,
                                CRY.ID Currency,
                                D.Name FundClientName
                                ,A.UnitAmount,E.Name AgentName, isnull((isnull(MFee.MGTFee,0) + (case when dbo.CheckTodayIsHoliday(@DateTo) = 1 and A.Date = dbo.fworkingday(@DateTo,-1) then  
                                isnull(MF.AUM,dbo.FgetLastAUM(A.Date,A.fundPK)) * [dbo].[FgetManagementFeePercentByDate] (A.Date,A.FundPK) / 100 / 365 * datediff(day,A.Date,@DateTo) else 0 end)) * [dbo].[FgetRebateFeePercentByDate] (A.Date,A.FundPK,A.FundClientPK) /100,0)  TrailingFee
                                from FundClientPosition A 


                                left join Fund C on A.FundPK = C.FundPK and C.status in (1,2)
                                left join Currency CRY on C.CurrencyPK = CRY.CurrencyPK and CRY.Status in (1,2)
                                left join FundClient D on A.FundClientPK = D.FundClientPK and D.status in (1,2)
                                left join Agent E on D.SellingAgentPK = E.AgentPK and E.status in (1,2)
                                left join #tempMFee MF on A.Date = MF.Date and A.FundPK = MF.FundPK and A.FundClientPK = MF.FundClientPK
                                left join #tempMFee MFee on dbo.fworkingday(A.Date,-1) = MFee.Date and A.FundPK = MFee.FundPK and A.FundClientPK = MFee.FundClientPK
                                where A.date between @DateFrom and @DateTo  
                                 
                                " + _paramFundClient + _paramFund + _paramAgent + @"
                                group by MF.NAV,A.Date,A.fundPK,A.FundClientPK,C.Name,CRY.ID,D.Name,A.UnitAmount,MF.AUM,MFee.MGTFee,E.Name,MFee.RebateFee 

                                order by A.Date ASC
                            "
                            ;

                            cmd.CommandTimeout = 0;


                            cmd.Parameters.AddWithValue("@DateFrom", _unitRegistryRpt.ValueDateFrom);
                            cmd.Parameters.AddWithValue("@DateTo", _unitRegistryRpt.ValueDateTo);

                            cmd.ExecuteNonQuery();

                            using (SqlDataReader dr0 = cmd.ExecuteReader())
                            {
                                if (!dr0.HasRows)
                                {
                                    return false;
                                }
                                else
                                {
                                    string filePath = Tools.ReportsPath + "ManagementFeePerClient" + "_" + _userID + ".xlsx";
                                    string pdfPath = Tools.ReportsPath + "ManagementFeePerClient" + "_" + _userID + ".pdf";
                                    FileInfo excelFile = new FileInfo(filePath);
                                    if (excelFile.Exists)
                                    {
                                        excelFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                        excelFile = new FileInfo(filePath);
                                    }


                                    // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                    using (ExcelPackage package = new ExcelPackage(excelFile))
                                    {
                                        package.Workbook.Properties.Title = "Management Fee Per Client";
                                        package.Workbook.Properties.Author = Tools.DefaultReportAuthor();
                                        package.Workbook.Properties.Comments = Tools.DefaultReportComments();
                                        package.Workbook.Properties.Company = Tools.DefaultReportCompany();
                                        package.Workbook.Properties.SetCustomPropertyValue("Checked by", _userID);
                                        package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", Tools.DefaultReportAssemblyName());

                                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Management Fee Per Client");


                                        //ATUR DATA GROUPINGNYA DULU
                                        List<ManagementFeePerClient> rList = new List<ManagementFeePerClient>();
                                        while (dr0.Read())
                                        {
                                            ManagementFeePerClient rSingle = new ManagementFeePerClient();
                                            rSingle.ClientName = Convert.ToString(dr0["FundClientName"]);
                                            rSingle.Fund = Convert.ToString(dr0["FundName"]);
                                            rSingle.Date = Convert.ToDateTime(dr0["Date"]);
                                            rSingle.TotalNAV = Convert.ToDecimal(dr0["AUM"]);
                                            rSingle.MIFee = Convert.ToDecimal(dr0["ManagementFeePercent"]);
                                            rSingle.MIFeeAccrual = Convert.ToDecimal(dr0["MGTFee"]);
                                            rSingle.DateFrom = Convert.ToDateTime(dr0["DateFrom"]);
                                            rSingle.DateTo = Convert.ToDateTime(dr0["DateTo"]);
                                            rSingle.AgentName = Convert.ToString(dr0["AgentName"]);
                                            rSingle.TrailingFee = Convert.ToDecimal(dr0["TrailingFee"]);

                                            rList.Add(rSingle);

                                        }

                                        var GroupByCategory =
                                         from r in rList
                                         orderby r.ClientName ascending
                                         group r by new { r.Fund, r.ClientName, r.DateFrom, r.DateTo, r.AgentName } into rGroup
                                         select rGroup;

                                        int incRowExcel = 1;
                                        //int _rowEndBalance = 0;

                                        foreach (var rsHeader in GroupByCategory)
                                        {


                                            incRowExcel = incRowExcel + 2;

                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 1].Value = "Client";
                                            worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                                            worksheet.Cells[incRowExcel, 2].Value = " : ";
                                            worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 3].Value = rsHeader.Key.ClientName;
                                            worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            incRowExcel++;

                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 1].Value = "Agent";
                                            worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                                            worksheet.Cells[incRowExcel, 2].Value = " : ";
                                            worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 3].Value = rsHeader.Key.AgentName;
                                            worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            incRowExcel++;

                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 1].Value = "Fund Name";
                                            worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                                            worksheet.Cells[incRowExcel, 2].Value = " : ";
                                            worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 3].Value = rsHeader.Key.Fund;
                                            worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            incRowExcel++;

                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 1].Value = "Date From";
                                            worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                                            worksheet.Cells[incRowExcel, 2].Value = " : ";
                                            worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 3].Value = Convert.ToDateTime(rsHeader.Key.DateFrom).ToString("dd-MMM-yyyy");
                                            worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            incRowExcel++;

                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 1].Value = "To";
                                            worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                                            worksheet.Cells[incRowExcel, 2].Value = " : ";
                                            worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 3].Value = Convert.ToDateTime(rsHeader.Key.DateTo).ToString("dd-MMM-yyyy");
                                            worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            incRowExcel++;

                                            int RowA = incRowExcel;
                                            int RowB = incRowExcel + 2;

                                            worksheet.Cells["A" + RowA + ":H" + RowA].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + RowA + ":A" + RowB].Value = "Date";
                                            worksheet.Cells["A" + RowA + ":A" + RowB].Merge = true;
                                            worksheet.Cells["A" + RowA + ":A" + RowB].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            worksheet.Cells["A" + RowA + ":A" + RowB].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                            worksheet.Cells["C" + RowA + ":C" + RowB].Value = "Total NAV";
                                            worksheet.Cells["C" + RowA + ":C" + RowB].Merge = true;
                                            worksheet.Cells["C" + RowA + ":C" + RowB].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            worksheet.Cells["C" + RowA + ":C" + RowB].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                            worksheet.Cells["D" + RowA + ":D" + RowB].Value = "MI Fee";
                                            worksheet.Cells["D" + RowA + ":D" + RowB].Merge = true;
                                            worksheet.Cells["D" + RowA + ":D" + RowB].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            worksheet.Cells["D" + RowA + ":D" + RowB].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                            worksheet.Cells["E" + RowA + ":E" + RowB].Value = "MI Fee Accrual";
                                            worksheet.Cells["E" + RowA + ":E" + RowB].Merge = true;
                                            worksheet.Cells["E" + RowA + ":E" + RowB].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["E" + RowA + ":E" + RowB].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                                            worksheet.Cells["F" + RowA + ":F" + RowB].Value = "PPN 10%";
                                            worksheet.Cells["F" + RowA + ":F" + RowB].Merge = true;
                                            worksheet.Cells["F" + RowA + ":F" + RowB].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            worksheet.Cells["F" + RowA + ":F" + RowB].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                            worksheet.Cells["G" + RowA + ":G" + RowB].Value = "Total";
                                            worksheet.Cells["G" + RowA + ":G" + RowB].Merge = true;
                                            worksheet.Cells["G" + RowA + ":G" + RowB].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            worksheet.Cells["G" + RowA + ":G" + RowB].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                            worksheet.Cells["H" + RowA + ":H" + RowB].Value = "Trailing Fee";
                                            worksheet.Cells["H" + RowA + ":H" + RowB].Merge = true;
                                            worksheet.Cells["H" + RowA + ":H" + RowB].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            worksheet.Cells["H" + RowA + ":H" + RowB].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                            int _startRowDetail = incRowExcel;
                                            int _startRowDetails = RowB;
                                            int _endRowDetail = 0;
                                            //end area header

                                            incRowExcel = incRowExcel + 3;
                                            foreach (var rsDetail in rsHeader)
                                            {

                                                worksheet.Cells[incRowExcel, 1].Value = Convert.ToDateTime(rsDetail.Date).ToString("dd/MMM/yyyy");
                                                worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.TotalNAV;
                                                worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                worksheet.Cells[incRowExcel, 4].Value = rsDetail.MIFee;
                                                worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##0.00%";
                                                worksheet.Cells[incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 5].Value = rsDetail.MIFeeAccrual;
                                                worksheet.Cells[incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 6].Formula = "SUM(E" + incRowExcel + "*10%" + ")";
                                                worksheet.Cells[incRowExcel, 6].Calculate();
                                                worksheet.Cells[incRowExcel, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                worksheet.Cells[incRowExcel, 7].Formula = "SUM(E" + incRowExcel + ":F" + incRowExcel + ")";
                                                worksheet.Cells[incRowExcel, 7].Calculate();
                                                worksheet.Cells[incRowExcel, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 8].Value = rsDetail.TrailingFee;
                                                worksheet.Cells[incRowExcel, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0.00";
                                                _endRowDetail = incRowExcel;
                                                incRowExcel++;

                                            }


                                            worksheet.Cells["A" + _startRowDetail + ":A" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["C" + _startRowDetail + ":C" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["C" + _startRowDetail + ":H" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            //worksheet.Cells["A" + _startRowDetails + ":G" + _endRowDetail].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + _startRowDetails + ":H" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            //worksheet.Cells["A" + incRowExcel + ":G" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + incRowExcel + ":H" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells[incRowExcel, 4].Value = "Total";
                                            worksheet.Cells[incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                            worksheet.Cells[incRowExcel, 5].Formula = "SUM(E" + _startRowDetail + ":E" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 5].Calculate();
                                            worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.0000";
                                            worksheet.Cells[incRowExcel, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                            worksheet.Cells[incRowExcel, 6].Formula = "SUM(F" + _startRowDetail + ":F" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 6].Calculate();
                                            worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.0000";
                                            worksheet.Cells[incRowExcel, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                            worksheet.Cells[incRowExcel, 7].Formula = "SUM(G" + _startRowDetail + ":G" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 7].Calculate();
                                            worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0.0000";

                                            incRowExcel = incRowExcel + 7;
                                            worksheet.Cells[incRowExcel, 1].Value = "Disclaimer   : ";
                                            incRowExcel++;
                                            worksheet.Row(incRowExcel).Height = 50;
                                            worksheet.Cells[incRowExcel, 1].Value = Tools.DefaultDisclaimerReportFooterLeftText();
                                            worksheet.Cells["A" + incRowExcel + ":F" + incRowExcel].Style.WrapText = true;
                                            worksheet.Cells["A" + incRowExcel + ":F" + incRowExcel].Merge = true;
                                            worksheet.Cells["A" + incRowExcel + ":F" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            worksheet.Cells[incRowExcel, 1].Style.VerticalAlignment = ExcelVerticalAlignment.Top;

                                            incRowExcel = incRowExcel + 2;
                                            worksheet.Row(incRowExcel).PageBreak = _unitRegistryRpt.PageBreak;
                                            incRowExcel++;


                                        }


                                        // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                        worksheet.PrinterSettings.FitToPage = true;
                                        worksheet.PrinterSettings.FitToWidth = 1;
                                        worksheet.PrinterSettings.FitToHeight = 0;
                                        worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel - 1, 8];
                                        worksheet.Column(1).Width = 20;
                                        worksheet.Column(2).Width = 5;
                                        worksheet.Column(3).Width = 35;
                                        worksheet.Column(4).Width = 20;
                                        worksheet.Column(5).Width = 20;
                                        worksheet.Column(6).Width = 20;
                                        worksheet.Column(7).Width = 20;
                                        worksheet.Column(8).Width = 20;

                                        worksheet.PrinterSettings.Orientation = eOrientation.Landscape; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                        // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                        worksheet.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
                                        worksheet.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
                                        worksheet.View.ShowGridLines = Tools.DefaultReportShowGridLines();
                                        worksheet.HeaderFooter.OddHeader.CenteredText = "&14 Management Fee Per Client";


                                        Image img = Image.FromFile(Tools.ReportImage);
                                        worksheet.HeaderFooter.OddHeader.InsertPicture(img, PictureAlignment.Left);
                                        //worksheet.HeaderFooter.OddHeader.LeftAlignedText = Tools.DefaultReportHeaderLeftText();

                                        worksheet.HeaderFooter.OddFooter.RightAlignedText =
                                        string.Format("Page {0} of {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);

                                        //worksheet.HeaderFooter.OddFooter.CenteredText = Tools.DefaultReportFooterCenterText();
                                        worksheet.HeaderFooter.OddFooter.LeftAlignedText = Tools.DefaultReportFooterLeftText();

                                        package.Save();
                                        if (_unitRegistryRpt.DownloadMode == "PDF")
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
            #endregion
            else
            {
                return false;
            }
        }

        public Boolean GenerateReportFundAccounting(string _userID, FundAccountingRpt _FundAccountingRpt)
        {

            #region Trial Balance
            if (_FundAccountingRpt.ReportName.Equals("Trial Balance"))
            {
                try
                {
                    using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                    {
                        DbCon.Open();
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            string _status = "";
                            string _paramFund = "";

                            if (!_host.findString(_FundAccountingRpt.FundFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_FundAccountingRpt.FundFrom))
                            {
                                _paramFund = "And A.FundPK in ( " + _FundAccountingRpt.FundFrom + " ) ";
                            }
                            else
                            {
                                _paramFund = "";
                            }

                            if (_FundAccountingRpt.Status == 1)
                            {
                                _status = " and B.Status = 2 and B.Posted = 1 and B.Reversed = 0 ";
                            }
                            else if (_FundAccountingRpt.Status == 2)
                            {
                                _status = " and B.Status = 2 and B.Posted = 1 and B.Reversed = 1 ";
                            }
                            else if (_FundAccountingRpt.Status == 3)
                            {
                                _status = " and B.Status = 2 and B.Posted = 0 and B.Reversed = 0 ";
                            }
                            else if (_FundAccountingRpt.Status == 4)
                            {
                                _status = " and B.Status = 1  ";
                            }
                            else if (_FundAccountingRpt.Status == 5)
                            {
                                _status = " and (B.Status = 2 or B.Posted = 1) and B.Reversed = 0 and B.status not in (3,4) ";
                            }
                            else if (_FundAccountingRpt.Status == 6)
                            {
                                _status = " and (B.Status = 1 Or B.Status = 2 or B.Posted = 1) and B.Reversed = 0 and B.status not in (3,4) ";
                            }



                            cmd.CommandText = @"
	                            
                                Declare @BeginDate datetime
                                select @BeginDate = [dbo].[FgetMaxDatePeriodClosingByFundPK](@ValueDateFrom,@FundPK) 

                                SELECT case when C.ID like '1%' then 1 
                                when C.ID like '201%' then 2 
                                when C.ID like '202%' then 3 
                                when C.ID like '3%' then 4 
                                when C.ID like '4%' then 5 
                                when C.ID like '5%' then 6 
                                when C.ID like '6%' then 7 
                                end No,
                                C.ID, C.Name, C.[Groups],C.[ParentPK],    
                                D.ID CurrencyID,       
                                CAST(ISNULL(B.PreviousBaseBalance, 0) AS NUMERIC(19,4)) AS PreviousBaseBalance,      
                                CAST(A.CurrentBaseDebit - ISNULL(B.PreviousBaseDebit, 0) AS NUMERIC(19,4)) AS BaseDebitMutasi,       
                                CAST(A.CurrentBaseCredit - ISNULL(B.PreviousBaseCredit, 0) AS NUMERIC(19,4)) AS BaseCreditMutasi,      
                                CAST(A.CurrentBaseBalance AS NUMERIC(19,4)) AS CurrentBaseBalance      
                                FROM (      
                                SELECT  A.FundJournalAccountPK,       
                                SUM(B.Balance) AS CurrentBalance,       
                                SUM(B.BaseBalance) AS CurrentBaseBalance,      
                                SUM(B.SumDebit) AS CurrentDebit,       
                                SUM(B.SumCredit) AS CurrentCredit,       
                                SUM(B.SumBaseDebit) AS CurrentBaseDebit,       
                                SUM(B.SumBaseCredit) AS CurrentBaseCredit      
                                FROM [FundJournalAccount] A INNER JOIN [Currency] C ON A.CurrencyPK = C.CurrencyPK, (      
                                SELECT A.FundJournalAccountPK, SUM(A.Debit-A.Credit) AS Balance,       
                                SUM(A.BaseDebit-A.BaseCredit) AS BaseBalance,      
                                SUM(A.Debit) AS SumDebit,      
                                SUM(A.Credit) AS SumCredit,      
                                SUM(A.BaseDebit) AS SumBaseDebit,      
                                SUM(A.BaseCredit) AS SumBaseCredit,      
                                C.ParentPK1, C.ParentPK2, C.ParentPK3, C.ParentPK4, C.ParentPK5,      
                                C.ParentPK6, C.ParentPK7, C.ParentPK8, C.ParentPK9     
                                FROM [FundJournalDetail] A INNER JOIN [FundJournal] B ON A.FundJournalPK = B.FundJournalPK      
                                INNER JOIN FundJournalAccount C ON A.FundJournalAccountPK = C.FundJournalAccountPK   And C.Status in (1,2)    
                                INNER JOIN Currency D ON A.CurrencyPK = D.CurrencyPK And D.Status in (1,2)      
                                WHERE B.ValueDate between @BeginDate and @ValueDateTo and C.Show = 1 " + _status + _paramFund + @"
                                Group BY A.FundJournalAccountPK, C.ParentPK1, C.ParentPK2,        
                                C.ParentPK3, C.ParentPK4, C.ParentPK5, C.ParentPK6,       
                                C.ParentPK7, C.ParentPK8, C.ParentPK9      
                                ) AS B        
                                WHERE
	                            (B.FundJournalAccountPK = A.FundJournalAccountPK OR B.ParentPK1 = A.FundJournalAccountPK OR B.ParentPK2 = A.FundJournalAccountPK        
                                OR B.ParentPK3 = A.FundJournalAccountPK OR B.ParentPK4 = A.FundJournalAccountPK OR B.ParentPK5 = A.FundJournalAccountPK        
                                OR B.ParentPK6 = A.FundJournalAccountPK OR B.ParentPK7 = A.FundJournalAccountPK OR B.ParentPK8 = A.FundJournalAccountPK        
                                OR B.ParentPK9 = A.FundJournalAccountPK)       and A.Status = 2 and A.Show = 1
                                Group BY A.FundJournalAccountPK       
                                ) AS A LEFT JOIN (       
                                SELECT A.FundJournalAccountPK,        
                                SUM(B.Balance) AS PreviousBalance,        
                                SUM(B.BaseBalance) AS PreviousBaseBalance,       
                                SUM(B.SumDebit) AS PreviousDebit,        
                                SUM(B.SumCredit) AS PreviousCredit,        
                                SUM(B.SumBaseDebit) AS PreviousBaseDebit,        
                                SUM(B.SumBaseCredit) AS PreviousBaseCredit       
                                FROM [FundJournalAccount] A INNER JOIN [Currency] C ON A.CurrencyPK = C.CurrencyPK, (       
                                SELECT A.FundJournalAccountPK, SUM(A.Debit-A.Credit) AS Balance,        
                                SUM(A.BaseDebit-A.BaseCredit) AS BaseBalance,        
                                SUM(A.Debit) AS SumDebit,        
                                SUM(A.Credit) AS SumCredit,        
                                SUM(A.BaseDebit) AS SumBaseDebit,        
                                SUM(A.BaseCredit) AS SumBaseCredit,        
                                C.ParentPK1, C.ParentPK2, C.ParentPK3, C.ParentPK4, C.ParentPK5,        
                                C.ParentPK6, C.ParentPK7, C.ParentPK8, C.ParentPK9       
                                FROM [FundJournalDetail] A INNER JOIN [FundJournal] B ON A.FundJournalPK = B.FundJournalPK        
                                INNER JOIN FundJournalAccount C ON A.FundJournalAccountPK = C.FundJournalAccountPK   And C.Status in (1,2)     
                                INNER JOIN Currency D ON A.CurrencyPK = D.CurrencyPK    And D.Status in (1,2)
                                WHERE  B.ValueDate < @ValueDateFrom and B.ValueDate >= @BeginDate and C.Show = 1   " + _status + _paramFund + @" 
                                Group BY A.FundJournalAccountPK, C.ParentPK1, C.ParentPK2,        
                                C.ParentPK3, C.ParentPK4, C.ParentPK5, C.ParentPK6,       
                                C.ParentPK7, C.ParentPK8, C.ParentPK9        
                                ) AS B        
                                WHERE  (B.FundJournalAccountPK = A.FundJournalAccountPK OR B.ParentPK1 = A.FundJournalAccountPK OR B.ParentPK2 = A.FundJournalAccountPK        
                                OR B.ParentPK3 = A.FundJournalAccountPK OR B.ParentPK4 = A.FundJournalAccountPK OR B.ParentPK5 = A.FundJournalAccountPK        
                                OR B.ParentPK6 = A.FundJournalAccountPK OR B.ParentPK7 = A.FundJournalAccountPK OR B.ParentPK8 = A.FundJournalAccountPK        
                                OR B.ParentPK9 = A.FundJournalAccountPK)  and A.Status = 2
                                Group BY A.FundJournalAccountPK       
                                ) AS B ON A.FundJournalAccountPK = B.FundJournalAccountPK        
                                INNER JOIN FundJournalAccount C ON A.FundJournalAccountPK = C.FundJournalAccountPK   And C.Status in (1,2)     
                                INNER JOIN Currency D ON C.CurrencyPK = D.CurrencyPK   And D.Status in (1,2)
                                WHERE ((A.CurrentBalance <> 0)        
                                OR (A.CurrentDebit - ISNULL(B.PreviousDebit, 0) <> 0)        
                                OR (A.CurrentCredit - ISNULL(B.PreviousCredit, 0) <> 0)        
                                OR (A.CurrentBaseBalance <> 0)        
                                OR (A.CurrentBaseDebit - ISNULL(B.PreviousBaseDebit, 0) <> 0)        
                                OR (A.CurrentBaseCredit - ISNULL(B.PreviousBaseCredit, 0) <> 0)) and C.Show = 1  and C.Groups = 0
                                Order BY C.ID  ";
                            cmd.CommandTimeout = 0;
                            cmd.Parameters.AddWithValue("@ValueDateFrom", _FundAccountingRpt.ValueDateFrom);
                            cmd.Parameters.AddWithValue("@ValueDateTo", _FundAccountingRpt.ValueDateTo);
                            cmd.Parameters.AddWithValue("@FundPK", _FundAccountingRpt.FundPK);

                            using (SqlDataReader dr0 = cmd.ExecuteReader())
                            {
                                if (!dr0.HasRows)
                                {
                                    return false;
                                }
                                else
                                {
                                    string filePath = Tools.ReportsPath + "TrialBalance" + "_" + _userID + ".xlsx";
                                    string pdfPath = Tools.ReportsPath + "TrialBalance" + "_" + _userID + ".pdf";
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

                                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Trial Balance");


                                        //ATUR DATA GROUPINGNYA DULU
                                        List<FundTrialBalance> rList = new List<FundTrialBalance>();
                                        while (dr0.Read())
                                        {


                                            FundTrialBalance rSingle = new FundTrialBalance();
                                            rSingle.No = Convert.ToInt32(dr0["No"]);
                                            rSingle.ID = Convert.ToString(dr0["ID"]);
                                            rSingle.Name = Convert.ToString(dr0["Name"]);
                                            rSingle.CurrencyID = Convert.ToString(dr0["CurrencyID"]);
                                            rSingle.Groups = Convert.ToInt32(dr0["Groups"]);
                                            rSingle.ParentPK = Convert.ToInt32(dr0["ParentPK"]);
                                            //rSingle.FundID = Convert.ToString(dr0["FundID"]);
                                            //rSingle.FundName = Convert.ToString(dr0["FundName"]);
                                            rSingle.PreviousBaseBalance = Convert.ToDecimal(dr0["PreviousBaseBalance"]);
                                            rSingle.BaseDebitMutasi = Convert.ToDecimal(dr0["BaseDebitMutasi"]);
                                            rSingle.BaseCreditMutasi = Convert.ToDecimal(dr0["BaseCreditMutasi"]);
                                            rSingle.CurrentBaseBalance = Convert.ToDecimal(dr0["CurrentBaseBalance"]);


                                            rList.Add(rSingle);

                                        }

                                        var GroupByReference =
                                            from r in rList
                                            orderby r.CurrencyID ascending
                                            group r by new { r.CurrencyID, r.No } into rGroup
                                            select rGroup;

                                        int incRowExcel = 0;

                                        incRowExcel = incRowExcel + 1;
                                        worksheet.Cells[incRowExcel, 1].Value = "Fund Manager";
                                        worksheet.Cells[incRowExcel, 2].Value = _host.Get_CompanyName();
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "Custodian Bank";
                                        worksheet.Cells[incRowExcel, 2].Value = _host.Get_BankCustodianName(_FundAccountingRpt.FundFrom);
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "Fund Name";
                                        worksheet.Cells[incRowExcel, 2].Value = _host.Get_FundName(_FundAccountingRpt.FundFrom);
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "Fund Type";
                                        worksheet.Cells[incRowExcel, 2].Value = _host.Get_FundType(_FundAccountingRpt.FundFrom);
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "Date From";
                                        worksheet.Cells[incRowExcel, 2].Value = _FundAccountingRpt.ValueDateFrom;
                                        worksheet.Cells[incRowExcel, 2].Style.Numberformat.Format = "dd/MMM/yyyy";
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "Date To";
                                        worksheet.Cells[incRowExcel, 2].Value = _FundAccountingRpt.ValueDateTo;
                                        worksheet.Cells[incRowExcel, 2].Style.Numberformat.Format = "dd/MMM/yyyy";
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "Currency";
                                        worksheet.Cells[incRowExcel, 2].Value = _host.Get_CurrencyID(_FundAccountingRpt.FundFrom);
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                        incRowExcel = incRowExcel + 2;




                                        //Row B = 3
                                        int RowB = incRowExcel;
                                        int RowG = incRowExcel + 1;

                                        worksheet.Cells[incRowExcel, 1].Value = "ID";
                                        worksheet.Cells[incRowExcel, 2].Value = "NAME";
                                        worksheet.Cells[incRowExcel, 3].Value = "PREVIOUS BALANCE";
                                        worksheet.Cells[incRowExcel, 4].Value = "DEBIT";
                                        worksheet.Cells[incRowExcel, 5].Value = "CREDIT";
                                        worksheet.Cells[incRowExcel, 6].Value = "CURRENT BALANCE";
                                        string _range = "A" + incRowExcel + ":F" + incRowExcel;


                                        foreach (var rsHeader in GroupByReference)
                                        {
                                            incRowExcel++;
                                            if (rsHeader.Key.No == 1)
                                            {
                                                worksheet.Cells[incRowExcel, 1].Value = "Asset";
                                            }
                                            else if (rsHeader.Key.No == 2)
                                            {
                                                worksheet.Cells[incRowExcel, 1].Value = "Liabilities";
                                            }
                                            else if (rsHeader.Key.No == 3)
                                            {
                                                worksheet.Cells[incRowExcel, 1].Value = "Capital";
                                            }
                                            else if (rsHeader.Key.No == 4)
                                            {
                                                worksheet.Cells[incRowExcel, 1].Value = "Revenue";
                                            }
                                            else if (rsHeader.Key.No == 5)
                                            {
                                                worksheet.Cells[incRowExcel, 1].Value = "Expenses";
                                            }
                                            else if (rsHeader.Key.No == 6)
                                            {
                                                worksheet.Cells[incRowExcel, 1].Value = "Profit/Loss";
                                            }
                                            else if (rsHeader.Key.No == 7)
                                            {
                                                worksheet.Cells[incRowExcel, 1].Value = "Tax";
                                            }
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            //worksheet.Cells["A" + incRowExcel + ":A" + incRowExcel].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                            //worksheet.Cells["A" + incRowExcel + ":A" + incRowExcel].Style.Fill.BackgroundColor.SetColor(Color.Orange);

                                            //worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            //worksheet.Cells[incRowExcel, 1].Value = "FUND : ";
                                            //worksheet.Cells[incRowExcel, 2].Value = _host.Get_FundName(_FundAccountingRpt.FundFrom);
                                            //worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                            //incRowExcel++;
                                            //worksheet.Cells[incRowExcel, 1].Value = "DATE FROM : ";
                                            //worksheet.Cells[incRowExcel, 2].Value = _FundAccountingRpt.ValueDateFrom;
                                            //worksheet.Cells[incRowExcel, 2].Style.Numberformat.Format = "dd/MMM/yyyy";
                                            //incRowExcel++;
                                            //worksheet.Cells[incRowExcel, 1].Value = "DATE TO : ";
                                            //worksheet.Cells[incRowExcel, 2].Value = _FundAccountingRpt.ValueDateTo;
                                            //worksheet.Cells[incRowExcel, 2].Style.Numberformat.Format = "dd/MMM/yyyy";
                                            //incRowExcel++;
                                            //worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;

                                            //incRowExcel++;

                                            ////Row B = 3
                                            //int RowB = incRowExcel;
                                            //int RowG = incRowExcel + 1;

                                            //worksheet.Cells[incRowExcel, 1].Value = "ID";
                                            //worksheet.Cells[incRowExcel, 2].Value = "NAME";
                                            //worksheet.Cells[incRowExcel, 3].Value = "PREVIOUS BALANCE";
                                            //worksheet.Cells[incRowExcel, 4].Value = "DEBIT";
                                            //worksheet.Cells[incRowExcel, 5].Value = "CREDIT";
                                            //worksheet.Cells[incRowExcel, 6].Value = "CURRENT BALANCE";
                                            //string _range = "A" + incRowExcel + ":F" + incRowExcel;

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

                                                worksheet.Cells["A" + RowB + ":F" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + RowB + ":F" + RowB].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + RowB + ":F" + RowB].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + RowB + ":F" + RowB].Style.Border.Right.Style = ExcelBorderStyle.Medium;


                                                //area detail




                                                worksheet.Cells[incRowExcel, 1].Value = rsDetail.ID;
                                                worksheet.Cells[incRowExcel, 2].Value = rsDetail.Name;
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.PreviousBaseBalance;
                                                worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 4].Value = rsDetail.BaseDebitMutasi;
                                                worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 5].Value = rsDetail.BaseCreditMutasi;
                                                worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 6].Value = rsDetail.CurrentBaseBalance;
                                                worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.00";
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

                                            //incRowExcel = incRowExcel + 2;
                                            if (rsHeader.Key.No == 1)
                                            {
                                                worksheet.Cells[incRowExcel, 1].Value = "Total Asset";
                                            }
                                            else if (rsHeader.Key.No == 2)
                                            {
                                                worksheet.Cells[incRowExcel, 1].Value = "Total Liabilities";
                                            }
                                            else if (rsHeader.Key.No == 3)
                                            {
                                                worksheet.Cells[incRowExcel, 1].Value = "Total Capital";
                                            }
                                            else if (rsHeader.Key.No == 4)
                                            {
                                                worksheet.Cells[incRowExcel, 1].Value = "Total Revenue";
                                            }
                                            else if (rsHeader.Key.No == 5)
                                            {
                                                worksheet.Cells[incRowExcel, 1].Value = "Total Expenses";
                                            }
                                            else if (rsHeader.Key.No == 6)
                                            {
                                                worksheet.Cells[incRowExcel, 1].Value = "Total Profit/Loss";
                                            }
                                            else if (rsHeader.Key.No == 7)
                                            {
                                                worksheet.Cells[incRowExcel, 1].Value = "Total Tax";
                                            }

                                            worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "#,##0.00";
                                            worksheet.Cells[incRowExcel, 3].Formula = "SUM(C" + _startRowDetail + ":C" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 3].Calculate();
                                            worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##0.00";
                                            worksheet.Cells[incRowExcel, 4].Formula = "SUM(D" + _startRowDetail + ":D" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 4].Calculate();
                                            worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.00";
                                            worksheet.Cells[incRowExcel, 5].Formula = "SUM(E" + _startRowDetail + ":E" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 5].Calculate();
                                            worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.00";
                                            worksheet.Cells[incRowExcel, 6].Formula = "SUM(F" + _startRowDetail + ":F" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 6].Calculate();
                                            //worksheet.Cells["A" + incRowExcel + ":F" + incRowExcel].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                            //worksheet.Cells["A" + incRowExcel + ":F" + incRowExcel].Style.Fill.BackgroundColor.SetColor(Color.Orange);
                                            worksheet.Cells["A" + incRowExcel + ":F" + incRowExcel].Style.Font.Bold = true;
                                            incRowExcel++;

                                        }




                                        worksheet.Row(incRowExcel).PageBreak = _FundAccountingRpt.PageBreak;

                                        string _rangeDetail = "A:F";

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
                                        worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel, 6];
                                        worksheet.Column(3).Width = 20;
                                        worksheet.Column(4).Width = 20;
                                        worksheet.Column(1).Width = 15;
                                        worksheet.Column(2).AutoFit();
                                        worksheet.Column(5).Width = 20;
                                        worksheet.Column(6).Width = 20;
                                        worksheet.PrinterSettings.Orientation = eOrientation.Portrait; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                        // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                        worksheet.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
                                        worksheet.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
                                        worksheet.View.ShowGridLines = Tools.DefaultReportShowGridLines();
                                        worksheet.HeaderFooter.OddHeader.CenteredText = "&14 TRIAL BALANCE";

                                        Image img = Image.FromFile(Tools.ReportImage);
                                        worksheet.HeaderFooter.OddHeader.InsertPicture(img, PictureAlignment.Left);
                                        worksheet.HeaderFooter.OddHeader.RightAlignedText = Tools.DefaultReportHeaderRightText();

                                        worksheet.HeaderFooter.OddFooter.RightAlignedText =
                                        string.Format("Page {0} of {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);

                                        //worksheet.HeaderFooter.OddFooter.CenteredText = Tools.DefaultReportFooterCenterText();
                                        worksheet.HeaderFooter.OddFooter.LeftAlignedText = Tools.DefaultReportFooterLeftText();

                                        package.Save();
                                        if (_FundAccountingRpt.DownloadMode == "PDF")
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
            #endregion

            #region Income Statement
            else if (_FundAccountingRpt.ReportName.Equals("Income Statement"))
            {
                try
                {
                    using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                    {
                        DbCon.Open();
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            string _status = "";
                            string _paramFund = "";

                            if (!_host.findString(_FundAccountingRpt.FundFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_FundAccountingRpt.FundFrom))
                            {
                                _paramFund = "And A.FundPK in ( " + _FundAccountingRpt.FundFrom + " ) ";
                            }
                            else
                            {
                                _paramFund = "";
                            }

                            if (_FundAccountingRpt.Status == 1)
                            {
                                _status = " and B.Status = 2 and B.Posted = 1 and B.Reversed = 0 ";
                            }
                            else if (_FundAccountingRpt.Status == 2)
                            {
                                _status = " and B.Status = 2 and B.Posted = 1 and B.Reversed = 1 ";
                            }
                            else if (_FundAccountingRpt.Status == 3)
                            {
                                _status = " and B.Status = 2 and B.Posted = 0 and B.Reversed = 0 ";
                            }
                            else if (_FundAccountingRpt.Status == 4)
                            {
                                _status = " and B.Status = 1  ";
                            }
                            else if (_FundAccountingRpt.Status == 5)
                            {
                                _status = " and (B.Status = 2 or B.Posted = 1) and B.Reversed = 0 and B.status not in (3,4) ";
                            }
                            else if (_FundAccountingRpt.Status == 6)
                            {
                                _status = " and (B.Status = 1 Or B.Status = 2 or B.Posted = 1) and B.Reversed = 0 and B.status not in (3,4) ";
                            }



                            cmd.CommandText = @"

                            Declare @BeginDate datetime
                            select @BeginDate = [dbo].[FgetMaxDatePeriodClosingByFundPK](@ValueDateFrom,@FundPK) 


                            Create Table #A
                            (No int,ID nvarchar(50), Movement numeric(22,4),Balance numeric(22,4))

                            insert into #A(No,ID,Movement,Balance)

                            SELECT 7 No,'500.00.00.000' ID,abs(sum(
                            CAST(A.CurrentBaseDebit - ISNULL(B.PreviousBaseDebit, 0) AS NUMERIC(19,4)) - CAST(A.CurrentBaseCredit - ISNULL(B.PreviousBaseCredit, 0) AS NUMERIC(19,4)))) AS Movement,            
                            abs(sum(CAST(A.CurrentBaseBalance AS NUMERIC(19,4)))) AS CurrentBaseBalance      
                            FROM (      
                            SELECT  A.FundJournalAccountPK,       
                            SUM(B.Balance) AS CurrentBalance,       
                            SUM(B.BaseBalance) AS CurrentBaseBalance,      
                            SUM(B.SumDebit) AS CurrentDebit,       
                            SUM(B.SumCredit) AS CurrentCredit,       
                            SUM(B.SumBaseDebit) AS CurrentBaseDebit,       
                            SUM(B.SumBaseCredit) AS CurrentBaseCredit      
                            FROM [FundJournalAccount] A INNER JOIN [Currency] C ON A.CurrencyPK = C.CurrencyPK, (      
                            SELECT A.FundJournalAccountPK, SUM(A.Debit-A.Credit) AS Balance,       
                            SUM(A.BaseDebit-A.BaseCredit) AS BaseBalance,      
                            SUM(A.Debit) AS SumDebit,      
                            SUM(A.Credit) AS SumCredit,      
                            SUM(A.BaseDebit) AS SumBaseDebit,      
                            SUM(A.BaseCredit) AS SumBaseCredit,      
                            C.ParentPK1, C.ParentPK2, C.ParentPK3, C.ParentPK4, C.ParentPK5,      
                            C.ParentPK6, C.ParentPK7, C.ParentPK8, C.ParentPK9     
                            FROM [FundJournalDetail] A INNER JOIN [FundJournal] B ON A.FundJournalPK = B.FundJournalPK      
                            INNER JOIN FundJournalAccount C ON A.FundJournalAccountPK = C.FundJournalAccountPK   And C.Status in (1,2)    
                            INNER JOIN Currency D ON A.CurrencyPK = D.CurrencyPK And D.Status in (1,2)      
                            WHERE B.ValueDate between @BeginDate and @ValueDateTo and C.Show = 1 " + _status + _paramFund + @"
                            Group BY A.FundJournalAccountPK, C.ParentPK1, C.ParentPK2,        
                            C.ParentPK3, C.ParentPK4, C.ParentPK5, C.ParentPK6,       
                            C.ParentPK7, C.ParentPK8, C.ParentPK9      
                            ) AS B        
                            WHERE
                            (B.FundJournalAccountPK = A.FundJournalAccountPK OR B.ParentPK1 = A.FundJournalAccountPK OR B.ParentPK2 = A.FundJournalAccountPK        
                            OR B.ParentPK3 = A.FundJournalAccountPK OR B.ParentPK4 = A.FundJournalAccountPK OR B.ParentPK5 = A.FundJournalAccountPK        
                            OR B.ParentPK6 = A.FundJournalAccountPK OR B.ParentPK7 = A.FundJournalAccountPK OR B.ParentPK8 = A.FundJournalAccountPK        
                            OR B.ParentPK9 = A.FundJournalAccountPK)       and A.Status = 2 and A.Show = 1
                            Group BY A.FundJournalAccountPK       
                            ) AS A LEFT JOIN (       
                            SELECT A.FundJournalAccountPK,        
                            SUM(B.Balance) AS PreviousBalance,        
                            SUM(B.BaseBalance) AS PreviousBaseBalance,       
                            SUM(B.SumDebit) AS PreviousDebit,        
                            SUM(B.SumCredit) AS PreviousCredit,        
                            SUM(B.SumBaseDebit) AS PreviousBaseDebit,        
                            SUM(B.SumBaseCredit) AS PreviousBaseCredit       
                            FROM [FundJournalAccount] A INNER JOIN [Currency] C ON A.CurrencyPK = C.CurrencyPK, (       
                            SELECT A.FundJournalAccountPK, SUM(A.Debit-A.Credit) AS Balance,        
                            SUM(A.BaseDebit-A.BaseCredit) AS BaseBalance,        
                            SUM(A.Debit) AS SumDebit,        
                            SUM(A.Credit) AS SumCredit,        
                            SUM(A.BaseDebit) AS SumBaseDebit,        
                            SUM(A.BaseCredit) AS SumBaseCredit,        
                            C.ParentPK1, C.ParentPK2, C.ParentPK3, C.ParentPK4, C.ParentPK5,        
                            C.ParentPK6, C.ParentPK7, C.ParentPK8, C.ParentPK9       
                            FROM [FundJournalDetail] A INNER JOIN [FundJournal] B ON A.FundJournalPK = B.FundJournalPK        
                            INNER JOIN FundJournalAccount C ON A.FundJournalAccountPK = C.FundJournalAccountPK   And C.Status in (1,2)     
                            INNER JOIN Currency D ON A.CurrencyPK = D.CurrencyPK    And D.Status in (1,2)
                            WHERE  B.ValueDate < @ValueDateFrom and B.ValueDate >= @BeginDate and C.Show = 1   and C.Type >=3 and C.ID not like '5%' " + _status + _paramFund + @"
                            Group BY A.FundJournalAccountPK, C.ParentPK1, C.ParentPK2,        
                            C.ParentPK3, C.ParentPK4, C.ParentPK5, C.ParentPK6,       
                            C.ParentPK7, C.ParentPK8, C.ParentPK9        
                            ) AS B        
                            WHERE  (B.FundJournalAccountPK = A.FundJournalAccountPK OR B.ParentPK1 = A.FundJournalAccountPK OR B.ParentPK2 = A.FundJournalAccountPK        
                            OR B.ParentPK3 = A.FundJournalAccountPK OR B.ParentPK4 = A.FundJournalAccountPK OR B.ParentPK5 = A.FundJournalAccountPK        
                            OR B.ParentPK6 = A.FundJournalAccountPK OR B.ParentPK7 = A.FundJournalAccountPK OR B.ParentPK8 = A.FundJournalAccountPK        
                            OR B.ParentPK9 = A.FundJournalAccountPK)  and A.Status = 2
                            Group BY A.FundJournalAccountPK       
                            ) AS B ON A.FundJournalAccountPK = B.FundJournalAccountPK        
                            INNER JOIN FundJournalAccount C ON A.FundJournalAccountPK = C.FundJournalAccountPK   And C.Status in (1,2)     
                            INNER JOIN Currency D ON C.CurrencyPK = D.CurrencyPK   And D.Status in (1,2)
                            WHERE ((A.CurrentBalance <> 0)        
                            OR (A.CurrentDebit - ISNULL(B.PreviousDebit, 0) <> 0)        
                            OR (A.CurrentCredit - ISNULL(B.PreviousCredit, 0) <> 0)        
                            OR (A.CurrentBaseBalance <> 0)        
                            OR (A.CurrentBaseDebit - ISNULL(B.PreviousBaseDebit, 0) <> 0)        
                            OR (A.CurrentBaseCredit - ISNULL(B.PreviousBaseCredit, 0) <> 0)) and C.Show = 1  and C.Type >=3  and C.Groups = 0 and C.ID not like '5%'




                            SELECT case 
                            when C.ID like '3%' then 4 
                            when C.ID like '4%' then 5 
                            when C.ID like '5%' then 7 
                            when C.ID like '6%' then 6 
                            end No,
                            C.ID, C.Name, C.[Groups],C.[ParentPK],    
                            D.ID CurrencyID,       
                            CAST(ISNULL(B.PreviousBaseBalance, 0) AS NUMERIC(19,4)) AS PreviousBaseBalance,      
                            CAST(A.CurrentBaseDebit - ISNULL(B.PreviousBaseDebit, 0) AS NUMERIC(19,4)) AS BaseDebitMutasi,       
                            CAST(A.CurrentBaseCredit - ISNULL(B.PreviousBaseCredit, 0) AS NUMERIC(19,4)) AS BaseCreditMutasi, 
                            abs(CAST(A.CurrentBaseDebit - ISNULL(B.PreviousBaseDebit, 0) AS NUMERIC(19,4)) - CAST(A.CurrentBaseCredit - ISNULL(B.PreviousBaseCredit, 0) AS NUMERIC(19,4))) AS Movement,            
                            abs(CAST(A.CurrentBaseBalance AS NUMERIC(19,4))) AS CurrentBaseBalance      
                            FROM (      
                            SELECT  A.FundJournalAccountPK,       
                            SUM(B.Balance) AS CurrentBalance,       
                            SUM(B.BaseBalance) AS CurrentBaseBalance,      
                            SUM(B.SumDebit) AS CurrentDebit,       
                            SUM(B.SumCredit) AS CurrentCredit,       
                            SUM(B.SumBaseDebit) AS CurrentBaseDebit,       
                            SUM(B.SumBaseCredit) AS CurrentBaseCredit      
                            FROM [FundJournalAccount] A INNER JOIN [Currency] C ON A.CurrencyPK = C.CurrencyPK, (      
                            SELECT A.FundJournalAccountPK, SUM(A.Debit-A.Credit) AS Balance,       
                            SUM(A.BaseDebit-A.BaseCredit) AS BaseBalance,      
                            SUM(A.Debit) AS SumDebit,      
                            SUM(A.Credit) AS SumCredit,      
                            SUM(A.BaseDebit) AS SumBaseDebit,      
                            SUM(A.BaseCredit) AS SumBaseCredit,      
                            C.ParentPK1, C.ParentPK2, C.ParentPK3, C.ParentPK4, C.ParentPK5,      
                            C.ParentPK6, C.ParentPK7, C.ParentPK8, C.ParentPK9     
                            FROM [FundJournalDetail] A INNER JOIN [FundJournal] B ON A.FundJournalPK = B.FundJournalPK      
                            INNER JOIN FundJournalAccount C ON A.FundJournalAccountPK = C.FundJournalAccountPK   And C.Status in (1,2)    
                            INNER JOIN Currency D ON A.CurrencyPK = D.CurrencyPK And D.Status in (1,2)      
                            WHERE B.ValueDate between @BeginDate and @ValueDateTo and C.Show = 1 " + _status + _paramFund + @"
                            Group BY A.FundJournalAccountPK, C.ParentPK1, C.ParentPK2,        
                            C.ParentPK3, C.ParentPK4, C.ParentPK5, C.ParentPK6,       
                            C.ParentPK7, C.ParentPK8, C.ParentPK9      
                            ) AS B        
                            WHERE
                            (B.FundJournalAccountPK = A.FundJournalAccountPK OR B.ParentPK1 = A.FundJournalAccountPK OR B.ParentPK2 = A.FundJournalAccountPK        
                            OR B.ParentPK3 = A.FundJournalAccountPK OR B.ParentPK4 = A.FundJournalAccountPK OR B.ParentPK5 = A.FundJournalAccountPK        
                            OR B.ParentPK6 = A.FundJournalAccountPK OR B.ParentPK7 = A.FundJournalAccountPK OR B.ParentPK8 = A.FundJournalAccountPK        
                            OR B.ParentPK9 = A.FundJournalAccountPK)       and A.Status = 2 and A.Show = 1
                            Group BY A.FundJournalAccountPK       
                            ) AS A LEFT JOIN (       
                            SELECT A.FundJournalAccountPK,        
                            SUM(B.Balance) AS PreviousBalance,        
                            SUM(B.BaseBalance) AS PreviousBaseBalance,       
                            SUM(B.SumDebit) AS PreviousDebit,        
                            SUM(B.SumCredit) AS PreviousCredit,        
                            SUM(B.SumBaseDebit) AS PreviousBaseDebit,        
                            SUM(B.SumBaseCredit) AS PreviousBaseCredit       
                            FROM [FundJournalAccount] A INNER JOIN [Currency] C ON A.CurrencyPK = C.CurrencyPK, (       
                            SELECT A.FundJournalAccountPK, SUM(A.Debit-A.Credit) AS Balance,        
                            SUM(A.BaseDebit-A.BaseCredit) AS BaseBalance,        
                            SUM(A.Debit) AS SumDebit,        
                            SUM(A.Credit) AS SumCredit,        
                            SUM(A.BaseDebit) AS SumBaseDebit,        
                            SUM(A.BaseCredit) AS SumBaseCredit,        
                            C.ParentPK1, C.ParentPK2, C.ParentPK3, C.ParentPK4, C.ParentPK5,        
                            C.ParentPK6, C.ParentPK7, C.ParentPK8, C.ParentPK9       
                            FROM [FundJournalDetail] A INNER JOIN [FundJournal] B ON A.FundJournalPK = B.FundJournalPK        
                            INNER JOIN FundJournalAccount C ON A.FundJournalAccountPK = C.FundJournalAccountPK   And C.Status in (1,2)     
                            INNER JOIN Currency D ON A.CurrencyPK = D.CurrencyPK    And D.Status in (1,2)
                            WHERE  B.ValueDate < @ValueDateFrom and B.ValueDate >= @BeginDate and C.Show = 1   and C.Type >=3 " + _status + _paramFund + @"
                            Group BY A.FundJournalAccountPK, C.ParentPK1, C.ParentPK2,        
                            C.ParentPK3, C.ParentPK4, C.ParentPK5, C.ParentPK6,       
                            C.ParentPK7, C.ParentPK8, C.ParentPK9        
                            ) AS B        
                            WHERE  (B.FundJournalAccountPK = A.FundJournalAccountPK OR B.ParentPK1 = A.FundJournalAccountPK OR B.ParentPK2 = A.FundJournalAccountPK        
                            OR B.ParentPK3 = A.FundJournalAccountPK OR B.ParentPK4 = A.FundJournalAccountPK OR B.ParentPK5 = A.FundJournalAccountPK        
                            OR B.ParentPK6 = A.FundJournalAccountPK OR B.ParentPK7 = A.FundJournalAccountPK OR B.ParentPK8 = A.FundJournalAccountPK        
                            OR B.ParentPK9 = A.FundJournalAccountPK)  and A.Status = 2
                            Group BY A.FundJournalAccountPK       
                            ) AS B ON A.FundJournalAccountPK = B.FundJournalAccountPK        
                            INNER JOIN FundJournalAccount C ON A.FundJournalAccountPK = C.FundJournalAccountPK   And C.Status in (1,2)     
                            INNER JOIN Currency D ON C.CurrencyPK = D.CurrencyPK   And D.Status in (1,2)
                            WHERE ((A.CurrentBalance <> 0)        
                            OR (A.CurrentDebit - ISNULL(B.PreviousDebit, 0) <> 0)        
                            OR (A.CurrentCredit - ISNULL(B.PreviousCredit, 0) <> 0)        
                            OR (A.CurrentBaseBalance <> 0)        
                            OR (A.CurrentBaseDebit - ISNULL(B.PreviousBaseDebit, 0) <> 0)        
                            OR (A.CurrentBaseCredit - ISNULL(B.PreviousBaseCredit, 0) <> 0)) and C.Show = 1  and C.Type >=3  and C.Groups = 0

                            union all
                            select No,ID, 'PENDAPATAN INVESTASI BERSIH', 0,0,'IDR',0,0,0,Movement,Balance from #A
                            order by No";

                            cmd.CommandTimeout = 0;
                            cmd.Parameters.AddWithValue("@ValueDateFrom", _FundAccountingRpt.ValueDateFrom);
                            cmd.Parameters.AddWithValue("@ValueDateTo", _FundAccountingRpt.ValueDateTo);
                            cmd.Parameters.AddWithValue("@FundPK", _FundAccountingRpt.FundFrom);

                            using (SqlDataReader dr0 = cmd.ExecuteReader())
                            {
                                if (!dr0.HasRows)
                                {
                                    return false;
                                }
                                else
                                {
                                    string filePath = Tools.ReportsPath + "IncomeStatement" + "_" + _userID + ".xlsx";
                                    string pdfPath = Tools.ReportsPath + "IncomeStatement" + "_" + _userID + ".pdf";
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

                                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Income Statement");


                                        //ATUR DATA GROUPINGNYA DULU
                                        List<IncomeStatement> rList = new List<IncomeStatement>();
                                        while (dr0.Read())
                                        {


                                            IncomeStatement rSingle = new IncomeStatement();
                                            rSingle.No = Convert.ToInt32(dr0["No"]);
                                            rSingle.ID = Convert.ToString(dr0["ID"]);
                                            rSingle.Name = Convert.ToString(dr0["Name"]);
                                            rSingle.CurrencyID = Convert.ToString(dr0["CurrencyID"]);
                                            rSingle.Groups = Convert.ToInt32(dr0["Groups"]);
                                            rSingle.ParentPK = Convert.ToInt32(dr0["ParentPK"]);
                                            //rSingle.FundID = Convert.ToString(dr0["FundID"]);
                                            //rSingle.FundName = Convert.ToString(dr0["FundName"]);
                                            rSingle.PreviousBaseBalance = Convert.ToDecimal(dr0["PreviousBaseBalance"]);
                                            rSingle.BaseDebitMutasi = Convert.ToDecimal(dr0["BaseDebitMutasi"]);
                                            rSingle.BaseCreditMutasi = Convert.ToDecimal(dr0["BaseCreditMutasi"]);
                                            rSingle.Movement = Convert.ToDecimal(dr0["Movement"]);
                                            rSingle.CurrentBaseBalance = Convert.ToDecimal(dr0["CurrentBaseBalance"]);


                                            rList.Add(rSingle);

                                        }

                                        var GroupByReference =
                                            from r in rList
                                            orderby r.CurrencyID, r.No, r.ID ascending
                                            group r by new { r.CurrencyID, r.No } into rGroup
                                            select rGroup;

                                        int incRowExcel = 0;

                                        incRowExcel = incRowExcel + 1;
                                        worksheet.Cells[incRowExcel, 1].Value = "Fund Manager";
                                        worksheet.Cells[incRowExcel, 2].Value = _host.Get_CompanyName();
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "Custodian Bank";
                                        worksheet.Cells[incRowExcel, 2].Value = _host.Get_BankCustodianName(_FundAccountingRpt.FundFrom);
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "Fund Name";
                                        worksheet.Cells[incRowExcel, 2].Value = _host.Get_FundName(_FundAccountingRpt.FundFrom);
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "Fund Type";
                                        worksheet.Cells[incRowExcel, 2].Value = _host.Get_FundType(_FundAccountingRpt.FundFrom);
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "Date";
                                        worksheet.Cells[incRowExcel, 2].Value = _FundAccountingRpt.ValueDateTo;
                                        worksheet.Cells[incRowExcel, 2].Style.Numberformat.Format = "dd/MMM/yyyy";
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "Currency";
                                        worksheet.Cells[incRowExcel, 2].Value = _host.Get_CurrencyID(_FundAccountingRpt.FundFrom);
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                        incRowExcel = incRowExcel + 2;

                                        worksheet.Cells[incRowExcel, 1].Value = "Laporan Operasi Reksadana";
                                        worksheet.Cells["A" + incRowExcel + ":D" + incRowExcel].Merge = true;
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                        incRowExcel = incRowExcel + 2;


                                        //Row B = 3
                                        int RowB = incRowExcel;
                                        int RowG = incRowExcel + 1;

                                        worksheet.Cells[incRowExcel, 1].Value = "Laporan Operasi";
                                        worksheet.Cells["A" + incRowExcel + ":B" + incRowExcel].Merge = true;
                                        worksheet.Cells[incRowExcel, 3].Value = "Hari Ini";
                                        worksheet.Cells[incRowExcel, 4].Value = "S/d Hari Ini";

                                        string _range = "A" + incRowExcel + ":D" + incRowExcel;


                                        foreach (var rsHeader in GroupByReference)
                                        {
                                            incRowExcel++;

                                            if (rsHeader.Key.No == 4)
                                            {
                                                worksheet.Cells[incRowExcel, 1].Value = "Pendapatan Investasi";
                                            }
                                            else if (rsHeader.Key.No == 5)
                                            {
                                                worksheet.Cells[incRowExcel, 1].Value = "Biaya-biaya";
                                            }
                                            else if (rsHeader.Key.No == 7)
                                            {
                                                worksheet.Cells[incRowExcel, 1].Value = "Laba/Rugi Operasi";
                                            }
                                            else if (rsHeader.Key.No == 6)
                                            {
                                                worksheet.Cells[incRowExcel, 1].Value = "Tax";
                                            }
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            //worksheet.Cells["A" + incRowExcel + ":A" + incRowExcel].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                            //worksheet.Cells["A" + incRowExcel + ":A" + incRowExcel].Style.Fill.BackgroundColor.SetColor(Color.Orange);

                                            //worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            //worksheet.Cells[incRowExcel, 1].Value = "FUND : ";
                                            //worksheet.Cells[incRowExcel, 2].Value = _host.Get_FundName(_FundAccountingRpt.FundFrom);
                                            //worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                            //incRowExcel++;
                                            //worksheet.Cells[incRowExcel, 1].Value = "DATE FROM : ";
                                            //worksheet.Cells[incRowExcel, 2].Value = _FundAccountingRpt.ValueDateFrom;
                                            //worksheet.Cells[incRowExcel, 2].Style.Numberformat.Format = "dd/MMM/yyyy";
                                            //incRowExcel++;
                                            //worksheet.Cells[incRowExcel, 1].Value = "DATE TO : ";
                                            //worksheet.Cells[incRowExcel, 2].Value = _FundAccountingRpt.ValueDateTo;
                                            //worksheet.Cells[incRowExcel, 2].Style.Numberformat.Format = "dd/MMM/yyyy";
                                            //incRowExcel++;
                                            //worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;

                                            //incRowExcel++;

                                            ////Row B = 3
                                            //int RowB = incRowExcel;
                                            //int RowG = incRowExcel + 1;

                                            //worksheet.Cells[incRowExcel, 1].Value = "ID";
                                            //worksheet.Cells[incRowExcel, 2].Value = "NAME";
                                            //worksheet.Cells[incRowExcel, 3].Value = "PREVIOUS BALANCE";
                                            //worksheet.Cells[incRowExcel, 4].Value = "DEBIT";
                                            //worksheet.Cells[incRowExcel, 5].Value = "CREDIT";
                                            //worksheet.Cells[incRowExcel, 6].Value = "CURRENT BALANCE";
                                            //string _range = "A" + incRowExcel + ":F" + incRowExcel;

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

                                                worksheet.Cells["A" + RowB + ":D" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + RowB + ":D" + RowB].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + RowB + ":D" + RowB].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + RowB + ":D" + RowB].Style.Border.Right.Style = ExcelBorderStyle.Medium;






                                                worksheet.Cells[incRowExcel, 2].Value = rsDetail.Name;
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.Movement;
                                                worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 4].Value = rsDetail.CurrentBaseBalance;
                                                worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##0.00";
                                                _endRowDetail = incRowExcel;

                                                _no++;
                                                incRowExcel++;





                                            }

                                            int RowF = incRowExcel - 1;
                                            worksheet.Cells["A" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + RowB + ":A" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            //worksheet.Cells["A" + RowB + ":A" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells["B" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            //worksheet.Cells["B" + RowB + ":B" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            //worksheet.Cells["B" + RowB + ":B" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["B" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells["C" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            //worksheet.Cells["C" + RowB + ":C" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            //worksheet.Cells["C" + RowB + ":C" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["C" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;


                                            worksheet.Cells["D" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            //worksheet.Cells["D" + RowB + ":D" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["D" + RowB + ":D" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["D" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;



                                            //incRowExcel = incRowExcel + 2;
                                            if (rsHeader.Key.No == 4)
                                            {
                                                worksheet.Cells[incRowExcel, 1].Value = "Total Pendapatan Investasi";
                                            }
                                            else if (rsHeader.Key.No == 5)
                                            {
                                                worksheet.Cells[incRowExcel, 1].Value = "Total Biaya-Biaya  ";
                                            }
                                            else if (rsHeader.Key.No == 6)
                                            {
                                                worksheet.Cells[incRowExcel, 1].Value = "Total Tax";
                                            }
                                            else if (rsHeader.Key.No == 7)
                                            {
                                                worksheet.Cells[incRowExcel, 1].Value = "Pendapatan Operasi Bersih";
                                            }


                                            worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "#,##0.00";
                                            worksheet.Cells[incRowExcel, 3].Formula = "SUM(C" + _startRowDetail + ":C" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 3].Calculate();
                                            worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##0.00";
                                            worksheet.Cells[incRowExcel, 4].Formula = "SUM(D" + _startRowDetail + ":D" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 4].Calculate();
                                            //worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.00";
                                            //worksheet.Cells[incRowExcel, 5].Formula = "SUM(E" + _startRowDetail + ":E" + _endRowDetail + ")";
                                            //worksheet.Cells[incRowExcel, 5].Calculate();
                                            //worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.00";
                                            //worksheet.Cells[incRowExcel, 6].Formula = "SUM(F" + _startRowDetail + ":F" + _endRowDetail + ")";
                                            //worksheet.Cells[incRowExcel, 6].Calculate();
                                            //worksheet.Cells["A" + incRowExcel + ":F" + incRowExcel].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                            //worksheet.Cells["A" + incRowExcel + ":F" + incRowExcel].Style.Fill.BackgroundColor.SetColor(Color.Orange);
                                            worksheet.Cells["A" + incRowExcel + ":D" + incRowExcel].Style.Font.Bold = true;
                                            incRowExcel++;

                                        }




                                        worksheet.Row(incRowExcel).PageBreak = _FundAccountingRpt.PageBreak;

                                        string _rangeDetail = "A:D";

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
                                        worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel, 5];
                                        worksheet.Column(3).Width = 20;
                                        worksheet.Column(4).Width = 20;
                                        worksheet.Column(1).Width = 15;
                                        worksheet.Column(2).AutoFit();
                                        worksheet.Column(5).Width = 20;
                                        worksheet.PrinterSettings.Orientation = eOrientation.Portrait; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                        // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                        worksheet.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
                                        worksheet.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
                                        worksheet.View.ShowGridLines = Tools.DefaultReportShowGridLines();
                                        worksheet.HeaderFooter.OddHeader.CenteredText = "&14 INCOME STATEMENT";

                                        Image img = Image.FromFile(Tools.ReportImage);
                                        worksheet.HeaderFooter.OddHeader.InsertPicture(img, PictureAlignment.Left);
                                        worksheet.HeaderFooter.OddHeader.RightAlignedText = Tools.DefaultReportHeaderRightText();

                                        worksheet.HeaderFooter.OddFooter.RightAlignedText =
                                        string.Format("Page {0} of {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);

                                        //worksheet.HeaderFooter.OddFooter.CenteredText = Tools.DefaultReportFooterCenterText();
                                        worksheet.HeaderFooter.OddFooter.LeftAlignedText = Tools.DefaultReportFooterLeftText();

                                        package.Save();
                                        if (_FundAccountingRpt.DownloadMode == "PDF")
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
            #endregion

            #region Cash Movement
            else if (_FundAccountingRpt.ReportName.Equals("Cash Movement"))
            {
                try
                {
                    using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                    {
                        DbCon.Open();
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {

                            string _paramFundFrom = "";

                            if (!_host.findString(_FundAccountingRpt.FundFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_FundAccountingRpt.FundFrom))
                            {
                                _paramFundFrom = "And B.FundPK  in ( " + _FundAccountingRpt.FundFrom + " ) ";
                            }
                            else
                            {
                                _paramFundFrom = "";
                            }

                            cmd.CommandText = @"
                            Declare @BeginDate datetime

                            select @BeginDate = [dbo].[FgetMaxDatePeriodClosingByFundPK](@ValueDateFrom,@FundFrom) 

                            Select A.ValueDate,A.Reference,A.TrxName,B.DetailDescription,B.BaseDebit,B.BaseCredit,C.Name InstrumentName,[dbo].FGetStartFundJournalAccountBalance(@ValueDateFrom,3,@FundFrom) StartBalance,
                            [dbo].[FGetStartFundJournalAccountBalance](@ValuedateFrom,3,@FundFrom) + SUM(B.BaseDebit - B.BaseCredit ) OVER(ORDER BY ValueDate 
                            ROWS BETWEEN UNBOUNDED PRECEDING AND CURRENT ROW) EndBalance 
                            from FundJournal A
                            left join FundJournalDetail B on A.FundJournalPK = B.FundJournalPK and B.status in (1,2)
                            left join Instrument C on B.InstrumentPK = C.InstrumentPK and C.status in (1,2)
                            where B.FundJournalAccountPK = 3 and A.status not in (3,4)and A.Posted = 1 and A.Reversed = 0
                            and A.valueDate between @BeginDate and @ValueDateTo " + _paramFundFrom + @"
                            order by A.ValueDate asc";

                            cmd.CommandTimeout = 0;
                            cmd.Parameters.AddWithValue("@ValueDateFrom", _FundAccountingRpt.ValueDateFrom);
                            cmd.Parameters.AddWithValue("@ValueDateTo", _FundAccountingRpt.ValueDateTo);
                            cmd.Parameters.AddWithValue("@FundFrom", _FundAccountingRpt.FundFrom);


                            using (SqlDataReader dr0 = cmd.ExecuteReader())
                            {
                                if (!dr0.HasRows)
                                {
                                    return false;
                                }
                                else
                                {
                                    string filePath = Tools.ReportsPath + "CashMovement" + "_" + _userID + ".xlsx";
                                    string pdfPath = Tools.ReportsPath + "CashMovement" + "_" + _userID + ".pdf";
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

                                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Cash Movement");




                                        //ATUR DATA GROUPINGNYA DULU
                                        List<CashMovement> rList = new List<CashMovement>();
                                        while (dr0.Read())
                                        {
                                            CashMovement rSingle = new CashMovement();
                                            rSingle.Date = Convert.ToDateTime(dr0["ValueDate"]);
                                            rSingle.InstrumentName = Convert.ToString(dr0["InstrumentName"]);
                                            rSingle.IDTrans = Convert.ToString(dr0["Reference"]);
                                            rSingle.Type = Convert.ToString(dr0["TrxName"]);
                                            rSingle.Description = Convert.ToString(dr0["DetailDescription"]);
                                            rSingle.Debit = Convert.ToDecimal(dr0["BaseDebit"]);
                                            rSingle.Credit = Convert.ToDecimal(dr0["BaseCredit"]);
                                            rSingle.StartBalance = Convert.ToDecimal(dr0["StartBalance"]);
                                            rSingle.EndBalance = Convert.ToDecimal(dr0["EndBalance"]);

                                            rList.Add(rSingle);

                                        }

                                        var GroupByReference =
                                            from r in rList
                                            //orderby  r.ValueDate, r.RefNo, r.FundClientID, r.InstrumentID ascending
                                            group r by new { r.Custodian, r.FundName, r.StartBalance } into rGroup
                                            select rGroup;

                                        int incRowExcel = 0;

                                        foreach (var rsHeader in GroupByReference)
                                        {

                                            incRowExcel++;
                                            worksheet.Cells["A" + incRowExcel + ":H" + incRowExcel].Merge = true;
                                            worksheet.Cells["A" + incRowExcel + ":H" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Value = _host.Get_FundName(_FundAccountingRpt.FundFrom);
                                            worksheet.Cells["A" + incRowExcel + ":H" + incRowExcel].Merge = true;
                                            worksheet.Cells["A" + incRowExcel + ":H" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Value = _host.Get_BankCustodianName(_FundAccountingRpt.FundFrom);
                                            worksheet.Cells["A" + incRowExcel + ":H" + incRowExcel].Merge = true;
                                            worksheet.Cells["A" + incRowExcel + ":H" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Value = "Date From  " + Convert.ToDateTime(_FundAccountingRpt.ValueDateFrom).ToString("dd-MMM-yyyy") + "  To  " + Convert.ToDateTime(_FundAccountingRpt.ValueDateTo).ToString("dd-MMM-yyyy");
                                            worksheet.Cells["A" + incRowExcel + ":H" + incRowExcel].Merge = true;
                                            worksheet.Cells["A" + incRowExcel + ":H" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            incRowExcel = incRowExcel + 2;

                                            int RowB = incRowExcel;
                                            worksheet.Cells[incRowExcel, 1].Value = "Date";
                                            worksheet.Cells[incRowExcel, 2].Value = "ID Trans.";
                                            worksheet.Cells[incRowExcel, 3].Value = "Type";
                                            worksheet.Cells[incRowExcel, 4].Value = "Instrument Name";
                                            worksheet.Cells[incRowExcel, 5].Value = "Description";
                                            worksheet.Cells[incRowExcel, 6].Value = "Debit";
                                            worksheet.Cells[incRowExcel, 7].Value = "Credit";
                                            worksheet.Cells[incRowExcel, 8].Value = "Balance";

                                            string _range = "A" + incRowExcel + ":H" + incRowExcel;

                                            //worksheet.Cells["A" + incRowExcel + ":H" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            //worksheet.Cells["A" + incRowExcel + ":H" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                            //worksheet.Cells["A" + incRowExcel + ":A" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            //worksheet.Cells["H" + incRowExcel + ":H" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Medium;

                                            incRowExcel++;

                                            worksheet.Cells[incRowExcel, 4].Value = "BEGINNING BALANCE";
                                            worksheet.Cells[incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0.00";
                                            worksheet.Cells[incRowExcel, 8].Value = rsHeader.Key.StartBalance;
                                            worksheet.Cells[incRowExcel, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

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


                                            int _startRowDetail = incRowExcel;
                                            int _endRowDetail = 0;
                                            //end area header
                                            incRowExcel++;
                                            foreach (var rsDetail in rsHeader)
                                            {


                                                //ThickBox Border HEADER

                                                worksheet.Cells["A" + RowB + ":H" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + RowB + ":H" + RowB].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + RowB + ":H" + RowB].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + RowB + ":H" + RowB].Style.Border.Right.Style = ExcelBorderStyle.Medium;



                                                worksheet.Cells[incRowExcel, 1].Value = Convert.ToDateTime(rsDetail.Date).ToString("dd/MMM/yyyy");
                                                worksheet.Cells[incRowExcel, 2].Value = rsDetail.IDTrans;
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.Type;
                                                worksheet.Cells[incRowExcel, 3].Style.WrapText = true;
                                                worksheet.Cells[incRowExcel, 4].Value = rsDetail.InstrumentName;
                                                worksheet.Cells[incRowExcel, 4].Style.WrapText = true;
                                                worksheet.Cells[incRowExcel, 5].Value = rsDetail.Description;
                                                worksheet.Cells[incRowExcel, 5].Style.WrapText = true;
                                                worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 6].Value = rsDetail.Debit;
                                                worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 7].Value = rsDetail.Credit;
                                                worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 8].Value = rsDetail.EndBalance;


                                                incRowExcel++;

                                            }
                                            int _RowA = incRowExcel - 1;

                                            worksheet.Cells[incRowExcel, 4].Value = "ENDING BALANCE";
                                            //worksheet.Cells[incRowExcel, 5].Merge = true;
                                            worksheet.Cells[incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0.00";
                                            worksheet.Cells[incRowExcel, 8].Formula = "SUM(H" + _RowA + ")";

                                            worksheet.Cells[incRowExcel, 8].Calculate();
                                            worksheet.Cells[incRowExcel, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                                            _endRowDetail = incRowExcel;

                                            worksheet.Cells["A" + _startRowDetail + ":H" + _startRowDetail].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + _endRowDetail + ":H" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + _startRowDetail + ":H" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["H" + _startRowDetail + ":H" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            incRowExcel++;

                                            worksheet.Row(incRowExcel).PageBreak = _FundAccountingRpt.PageBreak;

                                        }

                                        string _rangeDetail = "A:H";

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
                                        worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel, 8];
                                        worksheet.Column(1).Width = 15;
                                        worksheet.Column(2).Width = 15;
                                        worksheet.Column(3).Width = 20;
                                        worksheet.Column(4).Width = 30;
                                        worksheet.Column(5).Width = 40;
                                        worksheet.Column(6).Width = 20;
                                        worksheet.Column(7).Width = 20;
                                        worksheet.Column(8).Width = 20;
                                        worksheet.PrinterSettings.Orientation = eOrientation.Portrait; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                        // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                        worksheet.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
                                        worksheet.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
                                        worksheet.View.ShowGridLines = Tools.DefaultReportShowGridLines();
                                        Image img = Image.FromFile(Tools.ReportImage);
                                        worksheet.HeaderFooter.OddHeader.InsertPicture(img, PictureAlignment.Left);
                                        worksheet.HeaderFooter.OddHeader.CenteredText = "&30 CASH MOVEMENT";


                                        worksheet.HeaderFooter.OddHeader.RightAlignedText = Tools.DefaultReportHeaderRightText();

                                        worksheet.HeaderFooter.OddFooter.RightAlignedText =
                                        string.Format("Page {0} of {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);

                                        //worksheet.HeaderFooter.OddFooter.CenteredText = Tools.DefaultReportFooterCenterText();
                                        worksheet.HeaderFooter.OddFooter.LeftAlignedText = Tools.DefaultReportFooterLeftText();

                                        package.Save();
                                        if (_FundAccountingRpt.DownloadMode == "PDF")
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
            #endregion

            #region Counterpart Transaction
            if (_FundAccountingRpt.ReportName.Equals("Counterpart Transaction"))
            {
                try
                {
                    using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                    {
                        DbCon.Open();
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            string _paramCounterpart = "";
                            string _paramFundFrom = "";


                            if (!_host.findString(_FundAccountingRpt.FundFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_FundAccountingRpt.FundFrom))
                            {
                                _paramFundFrom = "And A.FundPK  in ( " + _FundAccountingRpt.FundFrom + " ) ";
                            }
                            else
                            {
                                _paramFundFrom = "";
                            }

                            if (!_host.findString(_FundAccountingRpt.CounterpartFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_FundAccountingRpt.CounterpartFrom))
                            {
                                _paramCounterpart = "And A.CounterpartPK  in ( " + _FundAccountingRpt.CounterpartFrom + " ) ";
                            }
                            else
                            {
                                _paramCounterpart = "";
                            }

                            cmd.CommandText = @"
                           
                            --Declare @BeginDate datetime

                            --select @BeginDate = [dbo].[FgetMaxDatePeriodClosingByFundPK](@ValueDateFrom,@FundPK) 


                            select A.FundPK,A.FundName,A.FundType,A.TransactionType,A.BrokerCode,sum(TotalYTD) TotalYTD, sum(TotalCommission) TotalCommission, sum(TotalWHT) TotalWHT, sum(PercentageNav) PercentageNav
                            from (
                            select C.FundPK FundPK,C.Name FundName, F.DescOne FundType,
                            CASE when A.InstrumentTypePK in (2,3) then 'Bond' else case when A.InstrumentTypePK = 1 then 'Equity' else D.Name end end TransactionType,
                            ISNULL(B.ID,'') BrokerCode,sum(A.DoneAmount) TotalYTD, CASE when A.InstrumentTypePK in (2,3) AND A.TrxType = 1 THEN   
                            SUM(ISNULL(100 / CASE WHEN A.IncomeTaxGainPercent = 0 THEN 1 ELSE A.incomeTaxGainPercent END * ISNULL(A.IncomeTaxGainAmount,0),0) )
                            ELSE  SUM(ISNULL(A.CommissionAmount,0)) END TotalCommission,
                            CASE when A.InstrumentTypePK in (2,3) THEN SUM(ISNULL(A.incomeTaxInterestAmount,0) + ISNULL(A.incomeTaxGainAmount,0)) ELSE  SUM(ISNULL(A.WHTAmount,0)) END TotalWHT,
                            SUM(A.DoneAmount)/[dbo].[FGetAmountFromInvestmentByInstrumentTypeYTD](@ValueDateTo,A.FundPK,A.InstrumentTypePK) PercentageNav from Investment A
                            left join Counterpart B on A.CounterpartPK = B.CounterpartPK and B.status in (1,2) 
                            left join Fund C on A.FundPK = C.FundPK and C.Status  in (1,2) 
                            left join InstrumentType D on A.InstrumentTypePK = D.InstrumentTypePK and D.Status in (1,2)
                            left join MasterValue F on C.Type = F.Code and F.ID = 'FundType'
                            Where A.ValueDate between @ValueDateFrom and @ValueDateTo and A.InstrumentTypePK <> 5 
                            " + _paramFundFrom + _paramCounterpart + @" 
                            and A.StatusSettlement = 2
                             group by C.FundPK,C.Name,F.DescOne,A.InstrumentTypePK,D.Name,B.ID,A.FundPK,A.TrxType
                            )A
                            group by A.FundPK,A.FundName,A.FundType,A.TransactionType,A.BrokerCode

                            ";

                            cmd.CommandTimeout = 0;
                            cmd.Parameters.AddWithValue("@ValueDateFrom", _FundAccountingRpt.ValueDateFrom);
                            cmd.Parameters.AddWithValue("@ValueDateTo", _FundAccountingRpt.ValueDateTo);
                            cmd.Parameters.AddWithValue("@FundPK", _FundAccountingRpt.FundFrom);


                            using (SqlDataReader dr0 = cmd.ExecuteReader())
                            {
                                if (!dr0.HasRows)
                                {
                                    return false;
                                }
                                else
                                {
                                    string filePath = Tools.ReportsPath + "CounterpartTransaction" + "_" + _userID + ".xlsx";
                                    string pdfPath = Tools.ReportsPath + "CounterpartTransaction" + "_" + _userID + ".pdf";
                                    FileInfo excelFile = new FileInfo(filePath);
                                    if (excelFile.Exists)
                                    {
                                        excelFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                        excelFile = new FileInfo(filePath);
                                    }

                                    // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                    using (ExcelPackage package = new ExcelPackage(excelFile))
                                    {
                                        package.Workbook.Properties.Title = "CounterpartTransaction";
                                        package.Workbook.Properties.Author = Tools.DefaultReportAuthor();
                                        package.Workbook.Properties.Comments = Tools.DefaultReportComments();
                                        package.Workbook.Properties.Company = Tools.DefaultReportCompany();
                                        package.Workbook.Properties.SetCustomPropertyValue("Checked by", _userID);
                                        package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", Tools.DefaultReportAssemblyName());

                                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Counterpart Transaction");




                                        //ATUR DATA GROUPINGNYA DULU
                                        List<CounterpartTransaction> rList = new List<CounterpartTransaction>();
                                        while (dr0.Read())
                                        {
                                            CounterpartTransaction rSingle = new CounterpartTransaction();
                                            rSingle.FundPK = Convert.ToString(dr0["FundPK"]);
                                            rSingle.FundName = Convert.ToString(dr0["FundName"]);
                                            rSingle.FundType = Convert.ToString(dr0["FundType"]);
                                            rSingle.TransactionType = Convert.ToString(dr0["TransactionType"]);
                                            rSingle.BrokerCode = Convert.ToString(dr0["BrokerCode"]);
                                            rSingle.TotalYTD = Convert.ToDecimal(dr0["TotalYTD"]);
                                            rSingle.TotalCommission = Convert.ToDecimal(dr0["TotalCommission"]);
                                            rSingle.TotalWHT = Convert.ToDecimal(dr0["TotalWHT"]);
                                            rSingle.PercentageNAV = Convert.ToDecimal(dr0["PercentageNAV"]);
                                            rList.Add(rSingle);

                                        }

                                        var GroupByReference =
                                            from r in rList
                                            ////orderby r.FundJournalAccountID, r.ValueDate, r.RefNo, r.FundClientID, r.InstrumentID ascending
                                            group r by new { r.TransactionType, r.FundName, r.FundPK, r.FundType } into rGroup
                                            select rGroup;

                                        int incRowExcel = 0;

                                        incRowExcel = incRowExcel + 1;
                                        



                                        foreach (var rsHeader in GroupByReference)
                                        {
                                            worksheet.Cells[incRowExcel, 1].Value = "Fund Manager";
                                            worksheet.Cells[incRowExcel, 2].Value = _host.Get_CompanyName();
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Value = "Custodian Bank";
                                            worksheet.Cells[incRowExcel, 2].Value = _host.Get_BankCustodianName(rsHeader.Key.FundPK);
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Value = "Fund Name";
                                            worksheet.Cells[incRowExcel, 2].Value = rsHeader.Key.FundName;
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Value = "Fund Type";
                                            worksheet.Cells[incRowExcel, 2].Value = rsHeader.Key.FundType;
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Value = "Date From";
                                            worksheet.Cells[incRowExcel, 2].Value = _FundAccountingRpt.ValueDateFrom;
                                            worksheet.Cells[incRowExcel, 2].Style.Numberformat.Format = "dd/MMM/yyyy";
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Value = "Date To";
                                            worksheet.Cells[incRowExcel, 2].Value = _FundAccountingRpt.ValueDateTo;
                                            worksheet.Cells[incRowExcel, 2].Style.Numberformat.Format = "dd/MMM/yyyy";
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            incRowExcel = incRowExcel + 2;

                                            worksheet.Cells[incRowExcel, 1].Value = "Counterpart Transaction";
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells["A" + incRowExcel + ":E" + incRowExcel].Style.Font.Size = 25;
                                            worksheet.Cells["A" + incRowExcel + ":E" + incRowExcel].Merge = true;
                                            worksheet.Cells["A" + incRowExcel + ":E" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            incRowExcel++;

                                            worksheet.Cells[incRowExcel, 1].Value = "TRANSACTION";
                                            worksheet.Cells[incRowExcel, 2].Value = rsHeader.Key.TransactionType;
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                            incRowExcel++;

                                            int RowB = incRowExcel;
                                            int RowG = incRowExcel + 1;

                                            int _startRowDetailBorder = incRowExcel;
                                            worksheet.Cells[incRowExcel, 1].Value = "BROKER CODE";
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 1].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 2].Value = "TOTAL VALUE YTD";
                                            worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 2].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 3].Value = "TOTAL COMMISSION";
                                            worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 3].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 4].Value = "TOTAL WHT";
                                            worksheet.Cells[incRowExcel, 4].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 4].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 5].Value = "% FROM NAV";
                                            worksheet.Cells[incRowExcel, 5].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 5].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            //ThickBox Border

                                            worksheet.Cells["A" + RowB + ":E" + RowG].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + RowB + ":E" + RowG].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + RowB + ":E" + RowG].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + RowB + ":E" + RowG].Style.Border.Right.Style = ExcelBorderStyle.Medium;


                                            incRowExcel++;

                                            int _no = 1;
                                            int _startRowDetail = incRowExcel;
                                            int _endRowDetail = 0;
                                            foreach (var rsDetail in rsHeader)
                                            {
                                                //ThickBox Border

                                                int RowD = incRowExcel;
                                                int RowE = incRowExcel + 1;
                                                worksheet.Cells["A" + RowD + ":E" + RowE].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + RowD + ":E" + RowE].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + RowD + ":E" + RowE].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + RowD + ":E" + RowE].Style.Border.Right.Style = ExcelBorderStyle.Medium;



                                                //area detail
                                                worksheet.Cells[incRowExcel, 1].Value = rsDetail.BrokerCode;
                                                worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 2].Value = rsDetail.TotalYTD;
                                                worksheet.Cells[incRowExcel, 2].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.TotalCommission;
                                                worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                worksheet.Cells[incRowExcel, 4].Value = rsDetail.TotalWHT;
                                                worksheet.Cells[incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 5].Value = rsDetail.PercentageNAV;
                                                worksheet.Cells[incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.00%";
                                                _endRowDetail = incRowExcel;
                                                _no++;
                                                incRowExcel++;

                                            }


                                            //worksheet.Cells["A" + _startRowDetailBorder + ":E" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            //worksheet.Cells["A" + _startRowDetailBorder + ":E" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                            //worksheet.Cells["A" + _startRowDetailBorder + ":E" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            //worksheet.Cells["A" + _startRowDetailBorder + ":E" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Medium;


                                            //worksheet.Cells["A" + _endRowDetail + ":G" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells[incRowExcel, 1].Value = "TOTAL : ";
                                            worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 2].Style.Numberformat.Format = "#,##0.00";
                                            worksheet.Cells[incRowExcel, 2].Formula = "SUM(B" + _startRowDetail + ":B" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 2].Calculate();
                                            worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "#,##0.00";
                                            worksheet.Cells[incRowExcel, 3].Formula = "SUM(C" + _startRowDetail + ":C" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 3].Calculate();
                                            worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##0.00";
                                            worksheet.Cells[incRowExcel, 4].Formula = "SUM(D" + _startRowDetail + ":D" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 4].Calculate();
                                            worksheet.Cells[incRowExcel, 5].Formula = "SUM(E" + _startRowDetail + ":E" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.00%";
                                            worksheet.Cells[incRowExcel, 5].Calculate();
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 4].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 5].Style.Font.Bold = true;


                                            incRowExcel = incRowExcel + 2;



                                            worksheet.Row(incRowExcel).PageBreak = _FundAccountingRpt.PageBreak;
                                        }

                                        int _lastRow = incRowExcel;

                                        string _rangeA = "A:E" + incRowExcel;
                                        using (ExcelRange r = worksheet.Cells[_rangeA]) // KALO  KOLOM 1 SAMPE 9 A-I
                                        {
                                            r.Style.Font.Size = 14;
                                        }

                                        worksheet.DeleteRow(_lastRow);

                                        worksheet.PrinterSettings.FitToPage = true;
                                        worksheet.PrinterSettings.FitToWidth = 1;
                                        worksheet.PrinterSettings.FitToHeight = 0;
                                        worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel - 1, 5];
                                        worksheet.Column(1).Width = 20;
                                        worksheet.Column(2).Width = 30;
                                        worksheet.Column(3).Width = 25;
                                        worksheet.Column(4).Width = 20;
                                        worksheet.Column(5).Width = 20;

                                        // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                        //worksheet.Cells["A3:I14"].AutoFitColumns(); // UNTUK AUTO FIT UKURAN TULISAN DENGAN KOLOM, KEBETULAN INI BUAT HEADER AJA DIPAKENYA
                                        //worksheetApproved.Cells["A3:E3"].AutoFitColumns();
                                        worksheet.PrinterSettings.Orientation = eOrientation.Portrait; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                        worksheet.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN

                                        //worksheet.HeaderFooter.OddHeader.CenteredText = "\n \n \n \n \n \n \n \n \n &30&B Transaction Report Laporan Akun Bulanan Client";

                                        Image img = Image.FromFile(Tools.ReportImage);
                                        worksheet.HeaderFooter.OddHeader.InsertPicture(img, PictureAlignment.Left);
                                        worksheet.HeaderFooter.OddHeader.RightAlignedText = Tools.DefaultReportHeaderRightText();
                                        worksheet.HeaderFooter.OddHeader.CenteredText = "&12 COUNTERPART TRANSACTION";
                                        // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                        worksheet.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
                                        worksheet.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
                                        worksheet.View.PageLayoutView = Tools.DefaultReportPageLayoutView();
                                        worksheet.View.ShowGridLines = Tools.DefaultReportShowGridLines();
                                        //worksheet.HeaderFooter.OddHeader.CenteredText = Tools.DefaultReportHeaderCenterText();
                                        //worksheet.HeaderFooter.OddHeader.LeftAlignedText = Tools.DefaultReportHeaderLeftText();
                                        //worksheet.HeaderFooter.OddFooter.LeftAlignedText = Tools.DefaultReportFooterLeftTextDisclaimer();
                                        //
                                        worksheet.HeaderFooter.OddFooter.RightAlignedText =
                                        string.Format("Page {0} of {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);

                                        ////worksheet.HeaderFooter.OddFooter.CenteredText = Tools.DefaultReportFooterCenterText();
                                        //worksheet.HeaderFooter.OddFooter.LeftAlignedText = Tools.DefaultReportFooterLeftText();



                                        package.Save();
                                        if (_FundAccountingRpt.DownloadMode == "PDF")
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
            #endregion

            #region Perhitungan Fee MI dan Fee BK
            else if (_FundAccountingRpt.ReportName.Equals("Perhitungan Fee MI dan Fee BK"))
            {
                try
                {
                    using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                    {
                        DbCon.Open();
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {

                            string _paramFundFrom = "";

                            if (!_host.findString(_FundAccountingRpt.FundFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_FundAccountingRpt.FundFrom))
                            {
                                _paramFundFrom = "And A.FundPK  in ( " + _FundAccountingRpt.FundFrom + " ) ";
                            }
                            else
                            {
                                _paramFundFrom = "";
                            }

                            cmd.CommandText = @"
            
                          
	                            SELECT  A.Date ,
                                        B.Name FundName,
		                                A.FundPK,
                                        isnull(D.AUM,0) AUM ,
                                        E.DescOne FundType ,
                                       [dbo].[FgetManagementFeePercentByDate](A.Date,A.FundPK) / 100 FeePercent ,
                                        SUM(isnull(D.AUM * [dbo].[FgetManagementFeePercentByDate](A.Date,A.FundPK) / 100 / 365 * 0.9,0)) NetFeeAmount ,
                                        SUM(isnull(D.AUM * [dbo].[FgetManagementFeePercentByDate](A.Date,A.FundPK) / 100 / 365 * 0.1,0)) TaxFeeAmount ,
                                        SUM(isnull(D.AUM * [dbo].[FgetManagementFeePercentByDate](A.Date,A.FundPK) / 100 / 365,0)) TotalFeeAmount ,
                                        'Management Fee' Perhitungan
                                FROM    CloseNAV A
                                        LEFT JOIN Fund B ON A.FundPK = B.FundPK
                                                            AND B.Status in (1,2)
		                                LEFT JOIN CloseNAV D ON dbo.fworkingday(A.Date,-1) = D.Date AND D.status = 2 and A.fundPK = D.FundPK
		                                LEFT JOIN MasterValue E on B.Type = E.Code and E.ID = 'FundType' AND E.status = 2
                                WHERE   A.Status = 2
                                        AND A.Date BETWEEN @ValueDateFrom AND @ValueDateTo 
		                               " + _paramFundFrom + @"
                                      
                                GROUP BY A.Date ,
		                                B.Name ,
		                                A.FundPK,
                                        D.AUM ,
                                      
		                                E.DescOne
                                UNION ALL
                                SELECT  A.Date ,
		                                B.Name FundName,
		                                A.FundPK,
                                       isnull(D.AUM,0) AUM ,
                                        E.DescOne FundType ,
                                        [dbo].[FgetCustodiFeePercentByDate](A.Date,A.FundPK)  / 100 FeePercent ,
                                        SUM(isnull(D.AUM * [dbo].[FgetCustodiFeePercentByDate](A.Date,A.FundPK)  / 100 / 365 * 0.9,0)) NetFeeAmount ,
                                        SUM(isnull(D.AUM * [dbo].[FgetCustodiFeePercentByDate](A.Date,A.FundPK)  / 100 / 365 * 0.1,0)) TaxFeeAmount ,
                                        SUM(isnull(D.AUM * [dbo].[FgetCustodiFeePercentByDate](A.Date,A.FundPK)  / 100 / 365,0)) TotalFeeAmount ,
                                        'Custodi Fee' Perhitungan
                                FROM    CloseNAV A
                                        LEFT JOIN Fund B ON A.FundPK = B.FundPK
                                                            AND B.Status in (1,2)
                                     
		                                 LEFT JOIN CloseNAV D ON dbo.fworkingday(A.Date,-1) = D.Date AND D.status = 2 and A.fundPK = D.FundPK
		                                LEFT JOIN MasterValue E on B.Type = E.Code and E.ID = 'FundType' AND E.status = 2
                                WHERE   A.Status = 2
                                        AND A.Date BETWEEN @ValueDateFrom AND @ValueDateTo 
		                              " + _paramFundFrom + @"
                                     
                                GROUP BY A.Date ,
		                                B.Name,
		                                A.FundPK,
                                        D.AUM ,
                                      
		                                E.DescOne
                                UNION ALL
                                SELECT  A.Date ,
		                                B.Name FundName,
		                                A.FundPK,
                                       isnull(D.AUM,0) AUM ,
                                        E.DescOne FundType ,
                                        [dbo].[FgetSInvestFeePercentByDate](A.Date,A.FundPK)  / 100 FeePercent ,
                                        SUM(isnull(D.AUM * [dbo].[FgetSInvestFeePercentByDate](A.Date,A.FundPK)  / 100 / 365 * 0.9,0)) NetFeeAmount ,
                                        SUM(isnull(D.AUM * [dbo].[FgetSInvestFeePercentByDate](A.Date,A.FundPK)  / 100 / 365 * 0.1,0)) TaxFeeAmount ,
                                        SUM(isnull(D.AUM * [dbo].[FgetSInvestFeePercentByDate](A.Date,A.FundPK)  / 100 / 365,0)) TotalFeeAmount ,
                                        'SInvest Fee' Perhitungan
                                FROM    CloseNAV A
                                        LEFT JOIN Fund B ON A.FundPK = B.FundPK
                                                            AND B.Status in (1,2)
                                     
		                                 LEFT JOIN CloseNAV D ON dbo.fworkingday(A.Date,-1) = D.Date AND D.status = 2 and A.fundPK = D.FundPK
		                                LEFT JOIN MasterValue E on B.Type = E.Code and E.ID = 'FundType' AND E.status = 2
                                WHERE   A.Status = 2
                                        AND A.Date BETWEEN @ValueDateFrom AND @ValueDateTo 
		                              " + _paramFundFrom + @"
                                     
                                GROUP BY A.Date ,
		                                B.Name,
		                                A.FundPK,
                                        D.AUM ,
                                      
		                                E.DescOne
                                ORDER BY A.Date

                            ";

                            cmd.CommandTimeout = 0;
                            cmd.Parameters.AddWithValue("@ValueDateFrom", _FundAccountingRpt.ValueDateFrom);
                            cmd.Parameters.AddWithValue("@ValueDateTo", _FundAccountingRpt.ValueDateTo);

                            using (SqlDataReader dr0 = cmd.ExecuteReader())
                            {
                                if (!dr0.HasRows)
                                {
                                    return false;
                                }
                                else
                                {
                                    string filePath = Tools.ReportsPath + "PerhitunganFeeMIdanFeeBK" + "_" + _userID + ".xlsx";
                                    string pdfPath = Tools.ReportsPath + "PerhitunganFeeMIdanFeeBK" + "_" + _userID + ".pdf";
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

                                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Perhitungan Fee MI dan Fee BK");




                                        //ATUR DATA GROUPINGNYA DULU
                                        List<PerhitunganFeeMIdanFeeBK> rList = new List<PerhitunganFeeMIdanFeeBK>();
                                        while (dr0.Read())
                                        {
                                            PerhitunganFeeMIdanFeeBK rSingle = new PerhitunganFeeMIdanFeeBK();
                                            rSingle.FundPK = Convert.ToString(dr0["FundPK"]);
                                            rSingle.FundName = Convert.ToString(dr0["FundName"]);
                                            rSingle.FundType = Convert.ToString(dr0["FundType"]);
                                            rSingle.Perhitungan = Convert.ToString(dr0["Perhitungan"]);
                                            rSingle.Date = Convert.ToDateTime(dr0["Date"]);
                                            rSingle.TotalDanaKelolaan = Convert.ToDecimal(dr0["AUM"]);
                                            rSingle.MIFEE = Convert.ToDecimal(dr0["FeePercent"]);
                                            rSingle.MIFEEAccrual = Convert.ToDecimal(dr0["NetFeeAmount"]);
                                            rSingle.PPN = Convert.ToDecimal(dr0["TaxFeeAmount"]);
                                            rSingle.TotalFee = Convert.ToDecimal(dr0["TotalFeeAmount"]);
                                            
                                            rList.Add(rSingle);

                                        }

                                        var GroupByReference =
                                            from r in rList
                                            //orderby  r.ValueDate, r.RefNo, r.FundClientID, r.InstrumentID ascending
                                            group r by new { r.Perhitungan, r.FundPK, r.FundName, r.FundType } into rGroup
                                            select rGroup;

                                        int incRowExcel = 0;

                                        incRowExcel = incRowExcel + 1;

                                        foreach (var rsHeader in GroupByReference)
                                        {

                                            worksheet.Cells[incRowExcel, 1].Value = "Fund Manager";
                                            worksheet.Cells[incRowExcel, 2].Value = " : ";
                                            worksheet.Cells[incRowExcel, 3].Value = _host.Get_CompanyName();
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Value = "Custodian Bank";
                                            worksheet.Cells[incRowExcel, 2].Value = " : ";
                                            worksheet.Cells[incRowExcel, 3].Value = _host.Get_BankCustodianName(rsHeader.Key.FundPK);
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Value = "Fund Name";
                                            worksheet.Cells[incRowExcel, 2].Value = " : ";
                                            worksheet.Cells[incRowExcel, 3].Value = rsHeader.Key.FundName;
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Value = "Fund Type";
                                            worksheet.Cells[incRowExcel, 2].Value = " : ";
                                            worksheet.Cells[incRowExcel, 3].Value = rsHeader.Key.FundType;
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Value = "Date From";
                                            worksheet.Cells[incRowExcel, 2].Value = " : ";
                                            worksheet.Cells[incRowExcel, 3].Value = _FundAccountingRpt.ValueDateFrom;
                                            worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "dd/MMM/yyyy";
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Value = "Date To";
                                            worksheet.Cells[incRowExcel, 2].Value = " : ";
                                            worksheet.Cells[incRowExcel, 3].Value = _FundAccountingRpt.ValueDateTo;
                                            worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "dd/MMM/yyyy";
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Value = "Currency";
                                            worksheet.Cells[incRowExcel, 2].Value = " : ";
                                            worksheet.Cells[incRowExcel, 3].Value = _host.Get_CurrencyID(rsHeader.Key.FundPK);
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;
                                            incRowExcel = incRowExcel + 2;

                                            worksheet.Cells[incRowExcel, 1].Value = "Perhitungan";
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 2].Value = " : ";
                                            worksheet.Cells[incRowExcel, 3].Value = rsHeader.Key.Perhitungan;
                                            worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            incRowExcel = incRowExcel + 2;

                                            int RowQ = incRowExcel;
                                            int RowZ = incRowExcel + 2;


                                            //ThickBox Border HEADER
                                            int RowB = incRowExcel;
                                            int RowG = incRowExcel + 2;
                                            worksheet.Cells["A" + RowB + ":G" + RowG].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + RowB + ":G" + RowG].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + RowB + ":G" + RowG].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + RowB + ":G" + RowG].Style.Border.Right.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 1].Value = "TANGGAL";
                                            worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells["A" + RowB + ":B" + RowG].Style.WrapText = true;
                                            worksheet.Cells["A" + RowB + ":B" + RowG].Merge = true;

                                            worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;
                                            worksheet.Cells["C" + RowB + ":C" + RowG].Style.WrapText = true;
                                            worksheet.Cells[incRowExcel, 3].Value = "Total Dana Kelolaan";
                                            worksheet.Cells["C" + RowB + ":C" + RowG].Merge = true;
                                            worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 4].Style.Font.Bold = true;
                                            worksheet.Cells["D" + RowB + ":D" + RowG].Style.WrapText = true;
                                            if (rsHeader.Key.Perhitungan == "Management Fee")
                                            {
                                                worksheet.Cells[incRowExcel, 4].Value = "MI FEE";
                                            }
                                            else if (rsHeader.Key.Perhitungan == "Custodi Fee")
                                            {
                                                worksheet.Cells[incRowExcel, 4].Value = "BK FEE";
                                            }
                                            else
                                            {
                                                worksheet.Cells[incRowExcel, 4].Value = "SInvest FEE";
                                            }

                                            worksheet.Cells["D" + RowB + ":D" + RowG].Merge = true;
                                            worksheet.Cells[incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 5].Style.Font.Bold = true;
                                            worksheet.Cells["E" + RowB + ":E" + RowG].Style.WrapText = true;

                                            if (rsHeader.Key.Perhitungan == "Management Fee")
                                            {
                                                worksheet.Cells[incRowExcel, 5].Value = "MI FEE Accrual";
                                            }
                                            else if (rsHeader.Key.Perhitungan == "Custodi Fee")
                                            {
                                                worksheet.Cells[incRowExcel, 5].Value = "BK FEE Accrual";
                                            }
                                            else
                                            {
                                                worksheet.Cells[incRowExcel, 5].Value = "SInvest FEE Accrual";
                                            }

                                            worksheet.Cells["E" + RowB + ":E" + RowG].Merge = true;
                                            worksheet.Cells[incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 6].Style.Font.Bold = true;
                                            worksheet.Cells["F" + RowB + ":F" + RowG].Style.WrapText = true;
                                            worksheet.Cells[incRowExcel, 6].Value = "PPN 10%";
                                            worksheet.Cells["F" + RowB + ":F" + RowG].Merge = true;
                                            worksheet.Cells[incRowExcel, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 7].Style.Font.Bold = true;
                                            worksheet.Cells["G" + RowB + ":G" + RowG].Style.WrapText = true;

                                            if (rsHeader.Key.Perhitungan == "Management Fee")
                                            {
                                                worksheet.Cells[incRowExcel, 7].Value = "TOTAL Fee MI & PPN ";
                                            }
                                            else if (rsHeader.Key.Perhitungan == "Custodi Fee")
                                            {
                                                worksheet.Cells[incRowExcel, 7].Value = "TOTAL Fee BK & PPN ";
                                            }
                                            else
                                            {
                                                worksheet.Cells[incRowExcel, 7].Value = "TOTAL Fee SInvest & PPN ";
                                            }

                                            worksheet.Cells["G" + RowB + ":G" + RowG].Merge = true;
                                            worksheet.Cells[incRowExcel, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;


                                            string _range = "A" + incRowExcel + ":G" + incRowExcel;

                                            using (ExcelRange r = worksheet.Cells[_range]) // KALO  KOLOM 1 SAMPE 9 A-I


                                                incRowExcel++;
                                            int _no = 1;

                                            int _startRowDetail = incRowExcel;
                                            int _endRowDetail = 0;

                                            incRowExcel = incRowExcel + 2;
                                            //end area header
                                            foreach (var rsDetail in rsHeader)
                                            {

                                                //ThickBox Border HEADER
                                                int RowD = incRowExcel;
                                                int RowE = incRowExcel + 1;
                                                worksheet.Cells["A" + RowD + ":G" + RowE].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + RowD + ":G" + RowE].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + RowD + ":G" + RowE].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + RowD + ":G" + RowE].Style.Border.Right.Style = ExcelBorderStyle.Medium;

                                                //area detail
                                                worksheet.Cells[incRowExcel, 1].Value = rsDetail.Date;
                                                worksheet.Cells[incRowExcel, 1].Style.Numberformat.Format = "dd/MMM/yyyy";
                                                //worksheet.Cells[incRowExcel, 1].Value = Convert.ToDateTime(rsDetail.Date).ToShortDateString();
                                                worksheet.Cells["A" + incRowExcel + ":B" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells["A" + incRowExcel + ":B" + incRowExcel].Merge = true;
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.TotalDanaKelolaan;
                                                worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 4].Value = rsDetail.MIFEE;
                                                worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##0.0000%";
                                                worksheet.Cells[incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 5].Value = rsDetail.MIFEEAccrual;
                                                worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 6].Value = rsDetail.PPN;
                                                worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 7].Value = rsDetail.TotalFee;
                                                worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0.00";
                                                _endRowDetail = incRowExcel;

                                                _no++;
                                                incRowExcel++;

                                            }

                                            int _RowA = incRowExcel;
                                            worksheet.Cells[incRowExcel, 1].Value = "TOTAL :";
                                            worksheet.Cells["A" + incRowExcel + ":B" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "#,##0.00";
                                            worksheet.Cells[incRowExcel, 3].Formula = "SUM(C" + _startRowDetail + ":C" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 3].Calculate();
                                            worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.00";
                                            worksheet.Cells[incRowExcel, 5].Formula = "SUM(E" + _startRowDetail + ":E" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 5].Calculate();
                                            worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.00";
                                            worksheet.Cells[incRowExcel, 6].Formula = "SUM(F" + _startRowDetail + ":F" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 6].Calculate();
                                            worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0.00";
                                            worksheet.Cells[incRowExcel, 7].Formula = "SUM(G" + _startRowDetail + ":G" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 7].Calculate();

                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 5].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 6].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 7].Style.Font.Bold = true;

                                            incRowExcel = incRowExcel + 2;

                                            incRowExcel = incRowExcel + 1;
                                            worksheet.Cells[incRowExcel, 6].Value = "TOTAL Fee & PPN";
                                            worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0.00";
                                            worksheet.Cells[incRowExcel, 7].Formula = "SUM(G" + _startRowDetail + ":G" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 7].Calculate();
                                            worksheet.Cells["C" + incRowExcel + ":C" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 6].Value = "PPN 10%";
                                            worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0.00";
                                            worksheet.Cells[incRowExcel, 7].Formula = "SUM(F" + _startRowDetail + ":F" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 7].Calculate();
                                            worksheet.Cells["D" + incRowExcel + ":D" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 6].Value = "PPH 23";
                                            worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0.00";
                                            //worksheet.Cells[incRowExcel, 7].Value = "0";
                                            worksheet.Cells[incRowExcel, 7].Formula = "SUM(E" + _RowA + "*2%"+ ")";
                                            worksheet.Cells[incRowExcel, 7].Calculate();

                                            incRowExcel++;
                                            worksheet.Row(incRowExcel).PageBreak = _FundAccountingRpt.PageBreak;
                                            incRowExcel++;

                                        }

                                        string _rangeDetail = "A:G";

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
                                        worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel-2, 8];
                                        worksheet.Column(1).Width = 25;
                                        worksheet.Column(2).Width = 5;
                                        worksheet.Column(3).Width = 20;
                                        worksheet.Column(4).Width = 20;
                                        worksheet.Column(5).Width = 20;
                                        worksheet.Column(6).Width = 20;
                                        worksheet.Column(7).Width = 20;
                                        worksheet.PrinterSettings.Orientation = eOrientation.Portrait; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                        // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                        worksheet.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
                                        worksheet.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
                                        worksheet.View.ShowGridLines = Tools.DefaultReportShowGridLines();
                                        Image img = Image.FromFile(Tools.ReportImage);
                                        worksheet.HeaderFooter.OddHeader.InsertPicture(img, PictureAlignment.Left);
                                        worksheet.HeaderFooter.OddHeader.CenteredText = "&14 Perhitungan Fee MI dan Fee BK";
                                        worksheet.HeaderFooter.OddHeader.RightAlignedText = Tools.DefaultReportHeaderRightText();



                                        worksheet.HeaderFooter.OddFooter.RightAlignedText =
                                        string.Format("Page {0} of {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);

                                        //worksheet.HeaderFooter.OddFooter.CenteredText = Tools.DefaultReportFooterCenterText();
                                        worksheet.HeaderFooter.OddFooter.LeftAlignedText = Tools.DefaultReportFooterLeftText();

                                        package.Save();
                                        if (_FundAccountingRpt.DownloadMode == "PDF")
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
            #endregion

            #region Transaction Summary
            else if (_FundAccountingRpt.ReportName.Equals("Transaction Summary"))
            {
                try
                {
                    using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                    {
                        DbCon.Open();
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {

                            string _paramFundFrom = "";
                            string _paramInstrumentType = "";



                            if (_FundAccountingRpt.InstrumentTypeFrom == "1")
                            {
                                _paramInstrumentType = "and B.InstrumentTypePK in (1,4,16) ";
                            }
                            else if (_FundAccountingRpt.InstrumentTypeFrom == "2")
                            {
                                _paramInstrumentType = "and B.InstrumentTypePK in (2,3,9,13,15) ";
                            }
                            else if (_FundAccountingRpt.InstrumentTypeFrom == "3")
                            {
                                _paramInstrumentType = "and B.InstrumentTypePK in (5) ";
                            }
                            else
                            {
                                _paramInstrumentType = "";
                            }


                            if (!_host.findString(_FundAccountingRpt.FundFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_FundAccountingRpt.FundFrom))
                            {
                                _paramFundFrom = " And A.FundPK  in ( " + _FundAccountingRpt.FundFrom + " ) ";
                            }
                            else
                            {
                                _paramFundFrom = "";
                            }



                            cmd.CommandText =

                            @"
                            select E.FundPK FundPK, E.Name, case when A.InstrumentTypePK in (2,3,8,9,11,13,14,15) then 'BOND' when A.InstrumentTypePK = 1 then 'EQUITY' else 'TIME DEPOSIT' end InstrumentTypeName
                            ,A.TrxTypeID,B.ID InstrumentID,
                            CASE WHEN A.InstrumentTypePK = 5 AND A.TrxType = 2 THEN A.Acqdate else A.ValueDate end ValueDate
                            ,SettlementDate,isnull(C.ID,'') CounterpartCode,ISNULL(B.Name,'') InstrumentName,
                            ISNULL(DoneVolume,0) DoneVolume,ISNULL(DonePrice,0) DonePrice, ISNULL(DoneAmount,0) DoneAmount,
                            sum(ISNULL(TotalAmount ,0)  -  ISNULL(DoneAmount  ,0) + ISNULL(WHTAmount  ,0)) TotalBrokerFee,case when A.InstrumentTypePK in (2,3,8,9,11,13,14,15) 
                            THEN sum(IncomeTaxGainAmount+IncomeTaxInterestAmount) when A.InstrumentTypePK = 1 then WHTAmount else 0 end WHTAmount, 
                            TotalAmount,dbo.FGetLastAvgFromInvestment(A.ValueDate,A.InstrumentPK,A.FundPK) AcqPrice,
                            SUM(DonePrice - dbo.FGetLastAvgFromInvestment(A.ValueDate,A.InstrumentPK,A.FundPK)) * DoneVolume Realised,
                            CASE WHEN A.InstrumentTypePK = 5 AND A.TrxType in (1,3) THEN A.DoneVolume /365 * A.InterestPercent/100 * DATEDIFF(DAY,A.ValueDate,A.maturitydate) 
                            ELSE CASE WHEN A.InstrumentTypePK = 5 AND A.TrxType = 2 THEN A.DoneVolume /365 * A.InterestPercent/100 * DATEDIFF(DAY,A.AcqDate,A.ValueDate) 
                            ELSE DoneAccruedInterest END END	DoneAccruedInterest
                            ,CASE WHEN A.InstrumentTypePK = 5 AND A.TrxType = 2 THEN isnull(A.BreakInterestPercent,0) ELSE  isnull(A.InterestPercent,0) END InterestPercent,isnull(D.Name,'') BankName,
                            CASE when A.InstrumentTypePK = 5 AND A.TrxType in (1,3) THEN  isnull(DATEDIFF(day,A.ValueDate ,A.MaturityDate),0) else 
                            CASE when A.InstrumentTypePK = 5 AND A.TrxType = 2 THEN DATEDIFF(DAY,A.AcqDate,A.ValueDate)  ELSE 0 end END Tenor,
                            A.MaturityDate, E.Name FundName, F.DescOne FundType, G.Name InstrumentType from Investment A  
                            left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status in (1,2)
                            left join Counterpart C on A.CounterpartPK = C.CounterpartPK and C.Status in (1,2)
                            left join Bank D on A.BankPK = D.BankPK and D.Status in (1,2)
                            left join Fund E on A.FundPK = E.FundPK and E.status in (1,2)
                            left join MasterValue F on E.Type = F.Code and F.ID = 'FundType'
                            left join InstrumentType G on A.InstrumentTypePK = G.InstrumentTypePK and G.status = 2
                            where 
                            StatusSettlement = 2 and
                            ValueDate between @ValueDateFrom and @ValueDateTo " + _paramFundFrom + _paramInstrumentType + @" 
                            group by E.FundPK, E.Name,A.TrxTypeID,B.ID,ValueDate,SettlementDate,C.ID,B.Name,DoneVolume,DonePrice,DoneAmount,
                            WHTAmount, TotalAmount,AcqPrice,DoneAccruedInterest,A.InterestPercent,D.Name,A.MaturityDate,A.InstrumentTypePK,A.MaturityDate,A.InstrumentPK,A.FundPK, F.DescOne
	                        ,A.TrxType,A.AcqDate,A.BreakInterestPercent, G.Name

                            ";


                            cmd.CommandTimeout = 0;
                            cmd.Parameters.AddWithValue("@ValueDateFrom", _FundAccountingRpt.ValueDateFrom);
                            cmd.Parameters.AddWithValue("@ValueDateTo", _FundAccountingRpt.ValueDateTo);

                            using (SqlDataReader dr0 = cmd.ExecuteReader())
                            {
                                if (!dr0.HasRows)
                                {
                                    return false;
                                }
                                else
                                {
                                    string filePath = Tools.ReportsPath + "TransactionSummary" + "_" + _userID + ".xlsx";
                                    string pdfPath = Tools.ReportsPath + "TransactionSummary" + "_" + _userID + ".pdf";
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

                                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Transaction Summary");

                                        //ATUR DATA GROUPINGNYA DULU
                                        List<TrxSummary> rList = new List<TrxSummary>();
                                        while (dr0.Read())
                                        {
                                            TrxSummary rSingle = new TrxSummary();
                                            rSingle.FundPK = Convert.ToString(dr0["FundPK"]);
                                            rSingle.FundName = Convert.ToString(dr0["Name"]);
                                            rSingle.InstrumentTypeName = Convert.ToString(dr0["InstrumentTypeName"]);
                                            rSingle.TrxTypeID = Convert.ToString(dr0["TrxTypeID"]);
                                            rSingle.InstrumentID = Convert.ToString(dr0["InstrumentID"]);
                                            rSingle.ValueDate = Convert.ToDateTime(dr0["ValueDate"]);
                                            rSingle.SettlementDate = Convert.ToDateTime(dr0["SettlementDate"]);
                                            rSingle.CounterpartCode = Convert.ToString(dr0["CounterpartCode"]);
                                            rSingle.InstrumentName = Convert.ToString(dr0["InstrumentName"]);
                                            rSingle.DoneVolume = Convert.ToDecimal(dr0["DoneVolume"]);
                                            rSingle.DonePrice = Convert.ToDecimal(dr0["DonePrice"]);
                                            rSingle.DoneAmount = Convert.ToDecimal(dr0["DoneAmount"]);
                                            rSingle.TotalBrokerFee = Convert.ToDecimal(dr0["TotalBrokerFee"]);
                                            rSingle.WHTAmount = Convert.ToDecimal(dr0["WHTAmount"]);
                                            rSingle.TotalAmount = Convert.ToDecimal(dr0["TotalAmount"]);
                                            rSingle.AcqPrice = Convert.ToDecimal(dr0["AcqPrice"]);
                                            rSingle.Realised = Convert.ToDecimal(dr0["Realised"]);
                                            rSingle.DoneAccruedInterest = Convert.ToDecimal(dr0["DoneAccruedInterest"]);
                                            rSingle.InterestPercent = Convert.ToDecimal(dr0["InterestPercent"]);
                                            rSingle.BankName = Convert.ToString(dr0["BankName"]);
                                            rSingle.FundType = Convert.ToString(dr0["FundType"]);
                                            rSingle.Tenor = Convert.ToInt32(dr0["Tenor"]);
                                            rSingle.MaturityDate = dr0["MaturityDate"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["MaturityDate"]);
                                            rSingle.InstrumentType = Convert.ToString(dr0["InstrumentType"]);
                                            rList.Add(rSingle);

                                        }

                                        var QueryByFundID =
                                                     from r in rList
                                                     group r by new { r.FundName, r.InstrumentTypeName, r.TrxTypeID, r.FundType, r.FundPK } into rGroup
                                                     select rGroup;

                                        int incRowExcel = 0;

                                        

                                        //int incRowExcel = 1;
                                        int RowA = incRowExcel;
                                        foreach (var rsHeader in QueryByFundID)
                                        {
                                            incRowExcel = incRowExcel + 1;
                                            worksheet.Cells[incRowExcel, 1].Value = "Fund Manager";
                                            worksheet.Cells[incRowExcel, 2].Value = " : ";
                                            worksheet.Cells[incRowExcel, 3].Value = _host.Get_CompanyName();
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Value = "Custodian Bank";
                                            worksheet.Cells[incRowExcel, 2].Value = " : ";
                                            worksheet.Cells[incRowExcel, 3].Value = _host.Get_BankCustodianName(rsHeader.Key.FundPK);
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Value = "Fund Name";
                                            worksheet.Cells[incRowExcel, 2].Value = " : ";
                                            worksheet.Cells[incRowExcel, 3].Value = rsHeader.Key.FundName;
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Value = "Fund Type";
                                            worksheet.Cells[incRowExcel, 2].Value = " : ";
                                            worksheet.Cells[incRowExcel, 3].Value = rsHeader.Key.FundType;
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Value = "Date From";
                                            worksheet.Cells[incRowExcel, 2].Value = " : ";
                                            worksheet.Cells[incRowExcel, 3].Value = _FundAccountingRpt.ValueDateFrom;
                                            worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "dd/MMM/yyyy";
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Value = "Date To";
                                            worksheet.Cells[incRowExcel, 2].Value = " : ";
                                            worksheet.Cells[incRowExcel, 3].Value = _FundAccountingRpt.ValueDateTo;
                                            worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "dd/MMM/yyyy";
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            incRowExcel = incRowExcel + 2;


                                            incRowExcel = incRowExcel + 1;

                                            worksheet.Cells[incRowExcel, 1].Value = "Instrument Type";
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 2].Value = " : ";
                                            worksheet.Cells[incRowExcel, 3].Value = rsHeader.Key.InstrumentTypeName;
                                            worksheet.Cells["C" + incRowExcel + ":D" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;

                                            incRowExcel++;

                                            worksheet.Cells[incRowExcel, 1].Value = "Transaction";
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 2].Value = " : ";
                                            worksheet.Cells[incRowExcel, 3].Value = rsHeader.Key.TrxTypeID;
                                            worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;
                                            incRowExcel++;

                                            int RowB = incRowExcel;
                                            int RowG = incRowExcel + 1;


                                            if (rsHeader.Key.InstrumentTypeName == "EQUITY")
                                            {
                                                worksheet.Cells[incRowExcel, 1].Value = "No.";
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 1].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 2].Value = "INS ID";
                                                worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                                worksheet.Cells["B" + incRowExcel + ":C" + incRowExcel].Merge = true;
                                                worksheet.Cells[incRowExcel, 2].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 4].Value = "Transaction Date";
                                                worksheet.Cells[incRowExcel, 4].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 4].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 5].Value = "Settlement Date";
                                                worksheet.Cells[incRowExcel, 5].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 5].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 6].Value = "Counterpart Code";
                                                worksheet.Cells[incRowExcel, 6].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 6].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 7].Value = "Securities Name/Code";
                                                worksheet.Cells[incRowExcel, 7].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 7].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 8].Value = "Quantity";
                                                worksheet.Cells[incRowExcel, 8].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 8].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 9].Value = "Price";
                                                worksheet.Cells[incRowExcel, 9].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 9].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 10].Value = "Total Amount";
                                                worksheet.Cells[incRowExcel, 10].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 10].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 10].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 11].Value = "Tot. Counterpart Fee";
                                                worksheet.Cells[incRowExcel, 11].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 11].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 11].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 12].Value = "WHT";
                                                worksheet.Cells[incRowExcel, 12].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 12].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 12].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 13].Value = "Total Settlement";
                                                worksheet.Cells[incRowExcel, 13].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 13].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 13].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                if (rsHeader.Key.TrxTypeID == "SELL")
                                                {
                                                    worksheet.Cells[incRowExcel, 14].Value = "Book Cost";
                                                    worksheet.Cells[incRowExcel, 14].Style.Font.Bold = true;
                                                    worksheet.Cells[incRowExcel, 14].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                    worksheet.Cells[incRowExcel, 14].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                    worksheet.Cells[incRowExcel, 15].Value = "Realized Gain/Loss";
                                                    worksheet.Cells[incRowExcel, 15].Style.Font.Bold = true;
                                                    worksheet.Cells[incRowExcel, 15].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                    worksheet.Cells[incRowExcel, 15].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                    worksheet.Cells["A" + RowB + ":O" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                    worksheet.Cells["A" + RowB + ":O" + RowB].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                    worksheet.Cells["A" + RowB + ":O" + RowB].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                    worksheet.Cells["A" + RowB + ":O" + RowB].Style.Border.Right.Style = ExcelBorderStyle.Medium;

                                                }

                                                worksheet.Cells["A" + RowB + ":M" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + RowB + ":M" + RowB].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + RowB + ":M" + RowB].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + RowB + ":M" + RowB].Style.Border.Right.Style = ExcelBorderStyle.Medium;

                                            }
                                            else if (rsHeader.Key.InstrumentTypeName == "BOND")
                                            {
                                                worksheet.Cells[incRowExcel, 1].Value = "No.";
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 1].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 2].Value = "INS ID";
                                                worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                                worksheet.Cells["B" + incRowExcel + ":C" + incRowExcel].Merge = true;
                                                worksheet.Cells[incRowExcel, 2].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 4].Value = "Transaction Date";
                                                worksheet.Cells[incRowExcel, 4].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 4].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 5].Value = "Settlement Date";
                                                worksheet.Cells[incRowExcel, 5].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 5].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 6].Value = "Counterpart Code";
                                                worksheet.Cells[incRowExcel, 6].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 6].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 7].Value = "Securities Name";
                                                worksheet.Cells[incRowExcel, 7].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 7].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 8].Value = "Nominal";
                                                worksheet.Cells[incRowExcel, 8].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 8].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 9].Value = "Price";
                                                worksheet.Cells[incRowExcel, 9].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 9].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 10].Value = "Total Amount";
                                                worksheet.Cells[incRowExcel, 10].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 10].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 10].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 11].Value = "Accrued Interest";
                                                worksheet.Cells[incRowExcel, 11].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 11].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 11].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 12].Value = "WHT";
                                                worksheet.Cells[incRowExcel, 12].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 12].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 12].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 13].Value = "Total Settlement";
                                                worksheet.Cells[incRowExcel, 13].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 13].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 13].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                if (rsHeader.Key.TrxTypeID == "SELL")
                                                {
                                                    worksheet.Cells[incRowExcel, 14].Value = "Book Cost";
                                                    worksheet.Cells[incRowExcel, 14].Style.Font.Bold = true;
                                                    worksheet.Cells[incRowExcel, 14].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                    worksheet.Cells[incRowExcel, 14].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                    worksheet.Cells[incRowExcel, 15].Value = "Realized Gain/Loss";
                                                    worksheet.Cells[incRowExcel, 15].Style.Font.Bold = true;
                                                    worksheet.Cells[incRowExcel, 15].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                    worksheet.Cells[incRowExcel, 15].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                    worksheet.Cells["A" + RowB + ":O" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                    worksheet.Cells["A" + RowB + ":O" + RowB].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                    worksheet.Cells["A" + RowB + ":O" + RowB].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                    worksheet.Cells["A" + RowB + ":O" + RowB].Style.Border.Right.Style = ExcelBorderStyle.Medium;

                                                }
                                                worksheet.Cells["A" + RowB + ":M" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + RowB + ":M" + RowB].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + RowB + ":M" + RowB].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + RowB + ":M" + RowB].Style.Border.Right.Style = ExcelBorderStyle.Medium;

                                            }

                                            else
                                            {
                                                worksheet.Cells[incRowExcel, 1].Value = "No.";
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 1].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 2].Value = "INS ID";
                                                worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                                worksheet.Cells["B" + incRowExcel + ":C" + incRowExcel].Merge = true;
                                                worksheet.Cells[incRowExcel, 2].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 4].Value = "Bank Placement";
                                                worksheet.Cells[incRowExcel, 4].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 4].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 5].Value = "Nominal";
                                                worksheet.Cells[incRowExcel, 5].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 5].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 6].Value = "Placement Date";
                                                worksheet.Cells[incRowExcel, 6].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 6].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 7].Value = "Maturity Date";
                                                worksheet.Cells[incRowExcel, 7].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 7].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 8].Value = "Interest %";
                                                worksheet.Cells[incRowExcel, 8].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 8].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 9].Value = "Tenor";
                                                worksheet.Cells[incRowExcel, 9].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 9].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 10].Value = "Total Interest";
                                                worksheet.Cells[incRowExcel, 10].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 10].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 10].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 11].Value = "Net Amount";
                                                worksheet.Cells[incRowExcel, 11].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 11].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 11].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells["A" + RowB + ":K" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + RowB + ":K" + RowB].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + RowB + ":K" + RowB].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + RowB + ":K" + RowB].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            }


                                            incRowExcel++;

                                            // Row C = 4
                                            int RowC = incRowExcel;

                                            //area header

                                            int _no = 1;
                                            int _noA = 1;
                                            int _noB = 1;


                                            int _startRowDetail = incRowExcel;
                                            int _endRowDetail = 0;

                                            //end area header
                                            foreach (var rsDetail in rsHeader)
                                            {

                                                //ThickBox Border HEADER
                                                //int RowD = incRowExcel;
                                                //int RowE = incRowExcel + 1;

                                                //area detail
                                                if (rsHeader.Key.InstrumentTypeName == "EQUITY")
                                                {
                                                    worksheet.Cells[incRowExcel, 1].Value = _no;
                                                    worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                    worksheet.Cells[incRowExcel, 3].Value = rsDetail.InstrumentID;
                                                    worksheet.Cells[incRowExcel, 4].Value = rsDetail.ValueDate;
                                                    worksheet.Cells[incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                    worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "dd/MMM/yyyy";
                                                    worksheet.Cells[incRowExcel, 5].Value = rsDetail.SettlementDate;
                                                    worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "dd/MMM/yyyy";
                                                    worksheet.Cells[incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                    worksheet.Cells[incRowExcel, 6].Value = rsDetail.CounterpartCode;
                                                    worksheet.Cells[incRowExcel, 7].Value = rsDetail.InstrumentName;
                                                    worksheet.Cells[incRowExcel, 8].Value = rsDetail.DoneVolume;
                                                    worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0.00";
                                                    worksheet.Cells[incRowExcel, 9].Value = rsDetail.DonePrice;
                                                    worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0.00";
                                                    worksheet.Cells[incRowExcel, 10].Value = rsDetail.DoneAmount;
                                                    worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0.00";
                                                    worksheet.Cells[incRowExcel, 11].Value = rsDetail.TotalBrokerFee;
                                                    worksheet.Cells[incRowExcel, 11].Style.Numberformat.Format = "#,##0.00";
                                                    worksheet.Cells[incRowExcel, 12].Value = rsDetail.WHTAmount;
                                                    worksheet.Cells[incRowExcel, 12].Style.Numberformat.Format = "#,##0.00";
                                                    worksheet.Cells[incRowExcel, 13].Value = rsDetail.TotalAmount;
                                                    worksheet.Cells[incRowExcel, 13].Style.Numberformat.Format = "#,##0.00";

                                                    if (rsHeader.Key.TrxTypeID == "SELL")
                                                    {
                                                        worksheet.Cells[incRowExcel, 14].Value = rsDetail.AcqPrice;
                                                        worksheet.Cells[incRowExcel, 14].Style.Numberformat.Format = "#,##0.00";
                                                        worksheet.Cells[incRowExcel, 15].Value = rsDetail.Realised;
                                                        worksheet.Cells[incRowExcel, 15].Style.Numberformat.Format = "#,##0.00";
                                                    }

                                                }

                                                else if (rsHeader.Key.InstrumentTypeName == "BOND")
                                                {
                                                    worksheet.Cells[incRowExcel, 1].Value = _noA;
                                                    worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                    worksheet.Cells[incRowExcel, 3].Value = rsDetail.InstrumentID;
                                                    worksheet.Cells[incRowExcel, 4].Value = rsDetail.ValueDate;
                                                    worksheet.Cells[incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                    worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "dd/MMM/yyyy";
                                                    worksheet.Cells[incRowExcel, 5].Value = rsDetail.SettlementDate;
                                                    worksheet.Cells[incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                    worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "dd/MMM/yyyy";
                                                    worksheet.Cells[incRowExcel, 6].Value = rsDetail.CounterpartCode;
                                                    worksheet.Cells[incRowExcel, 7].Value = rsDetail.InstrumentName;
                                                    worksheet.Cells[incRowExcel, 8].Value = rsDetail.DoneVolume;
                                                    worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0.00";
                                                    worksheet.Cells[incRowExcel, 9].Value = rsDetail.DonePrice;
                                                    worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0.00";
                                                    worksheet.Cells[incRowExcel, 10].Value = rsDetail.DoneAmount;
                                                    worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0.00";
                                                    worksheet.Cells[incRowExcel, 11].Value = rsDetail.DoneAccruedInterest;
                                                    worksheet.Cells[incRowExcel, 11].Style.Numberformat.Format = "#,##0.00";
                                                    worksheet.Cells[incRowExcel, 12].Value = rsDetail.WHTAmount;
                                                    worksheet.Cells[incRowExcel, 12].Style.Numberformat.Format = "#,##0.00";
                                                    worksheet.Cells[incRowExcel, 13].Value = rsDetail.TotalAmount;
                                                    worksheet.Cells[incRowExcel, 13].Style.Numberformat.Format = "#,##0.00";

                                                    if (rsHeader.Key.TrxTypeID == "SELL")
                                                    {
                                                        worksheet.Cells[incRowExcel, 14].Value = rsDetail.AcqPrice;
                                                        worksheet.Cells[incRowExcel, 14].Style.Numberformat.Format = "#,##0.00";
                                                        worksheet.Cells[incRowExcel, 15].Value = rsDetail.Realised;
                                                        worksheet.Cells[incRowExcel, 15].Style.Numberformat.Format = "#,##0.00";
                                                    }

                                                }
                                                else
                                                {
                                                    worksheet.Cells[incRowExcel, 1].Value = _noB;
                                                    worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                    worksheet.Cells[incRowExcel, 3].Value = rsDetail.InstrumentID;
                                                    worksheet.Cells[incRowExcel, 4].Value = rsDetail.BankName;
                                                    worksheet.Cells[incRowExcel, 5].Value = rsDetail.DoneVolume;
                                                    worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.00";
                                                    worksheet.Cells[incRowExcel, 6].Value = rsDetail.ValueDate;
                                                    worksheet.Cells[incRowExcel, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                    worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "dd/MMM/yyyy";
                                                    worksheet.Cells[incRowExcel, 7].Value = Convert.ToDateTime(rsDetail.MaturityDate).ToString("dd-MMM-yyyy");
                                                    worksheet.Cells[incRowExcel, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                    worksheet.Cells[incRowExcel, 8].Value = rsDetail.InterestPercent;
                                                    worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0.00";
                                                    worksheet.Cells[incRowExcel, 9].Value = rsDetail.Tenor;
                                                    worksheet.Cells[incRowExcel, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                    worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0";
                                                    worksheet.Cells[incRowExcel, 10].Value = rsDetail.DoneAccruedInterest;
                                                    worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0.00";
                                                    worksheet.Cells[incRowExcel, 11].Value = rsDetail.TotalAmount;
                                                    worksheet.Cells[incRowExcel, 11].Style.Numberformat.Format = "#,##0.00";

                                                }

                                                _endRowDetail = incRowExcel;

                                                _no++;
                                                _noA++;
                                                _noB++;
                                                if (rsHeader.Key.InstrumentTypeName == "EQUITY")
                                                {
                                                    if (rsHeader.Key.TrxTypeID == "SELL")
                                                    {
                                                        worksheet.Cells["A" + _startRowDetail + ":A" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                        worksheet.Cells["A" + _startRowDetail + ":A" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Medium;

                                                        worksheet.Cells["D" + _startRowDetail + ":O" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                        worksheet.Cells["D" + _startRowDetail + ":O" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                                    }
                                                    else
                                                    {
                                                        worksheet.Cells["A" + _startRowDetail + ":A" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                        worksheet.Cells["A" + _startRowDetail + ":A" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Medium;

                                                        worksheet.Cells["D" + _startRowDetail + ":M" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                        worksheet.Cells["D" + _startRowDetail + ":M" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                                    }

                                                }
                                                else if (rsHeader.Key.InstrumentTypeName == "BOND")
                                                {
                                                    if (rsHeader.Key.TrxTypeID == "SELL")
                                                    {
                                                        worksheet.Cells["A" + _startRowDetail + ":A" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                        worksheet.Cells["A" + _startRowDetail + ":A" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Medium;

                                                        worksheet.Cells["D" + _startRowDetail + ":O" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                        worksheet.Cells["D" + _startRowDetail + ":O" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                                    }
                                                    else
                                                    {
                                                        worksheet.Cells["A" + _startRowDetail + ":A" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                        worksheet.Cells["A" + _startRowDetail + ":A" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Medium;

                                                        worksheet.Cells["D" + _startRowDetail + ":M" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                        worksheet.Cells["D" + _startRowDetail + ":M" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                                    }

                                                }
                                                else
                                                {
                                                    worksheet.Cells["A" + _startRowDetail + ":A" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                    worksheet.Cells["A" + _startRowDetail + ":A" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Medium;

                                                    worksheet.Cells["D" + _startRowDetail + ":K" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                    worksheet.Cells["D" + _startRowDetail + ":K" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                                }


                                                incRowExcel++;

                                            }

                                            if (rsHeader.Key.InstrumentTypeName == "EQUITY")
                                            {
                                                if (rsHeader.Key.TrxTypeID == "SELL")
                                                {
                                                    worksheet.Cells["A" + _endRowDetail + ":O" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                    worksheet.Cells[incRowExcel, 3].Value = "TOTAL";
                                                    worksheet.Cells["C" + incRowExcel + ":O" + incRowExcel].Style.Font.Bold = true;
                                                    worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0.00";
                                                    worksheet.Cells[incRowExcel, 8].Formula = "SUM(H" + _startRowDetail + ":H" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 8].Calculate();

                                                    worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0.00";
                                                    worksheet.Cells[incRowExcel, 10].Formula = "SUM(J" + _startRowDetail + ":J" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 10].Calculate();

                                                    worksheet.Cells[incRowExcel, 11].Style.Numberformat.Format = "#,##0.00";
                                                    worksheet.Cells[incRowExcel, 11].Formula = "SUM(K" + _startRowDetail + ":K" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 11].Calculate();

                                                    worksheet.Cells[incRowExcel, 12].Style.Numberformat.Format = "#,##0.00";
                                                    worksheet.Cells[incRowExcel, 12].Formula = "SUM(L" + _startRowDetail + ":L" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 12].Calculate();

                                                    worksheet.Cells[incRowExcel, 13].Style.Numberformat.Format = "#,##0.00";
                                                    worksheet.Cells[incRowExcel, 13].Formula = "SUM(M" + _startRowDetail + ":M" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 13].Calculate();

                                                    worksheet.Cells[incRowExcel, 15].Style.Numberformat.Format = "#,##0.00";
                                                    worksheet.Cells[incRowExcel, 15].Formula = "SUM(O" + _startRowDetail + ":O" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 15].Calculate();
                                                }
                                                else
                                                {
                                                    worksheet.Cells["A" + _endRowDetail + ":M" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                    worksheet.Cells[incRowExcel, 3].Value = "TOTAL";
                                                    worksheet.Cells["C" + incRowExcel + ":O" + incRowExcel].Style.Font.Bold = true;
                                                    worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0.00";
                                                    worksheet.Cells[incRowExcel, 8].Formula = "SUM(H" + _startRowDetail + ":H" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 8].Calculate();

                                                    worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0.00";
                                                    worksheet.Cells[incRowExcel, 10].Formula = "SUM(J" + _startRowDetail + ":J" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 10].Calculate();

                                                    worksheet.Cells[incRowExcel, 11].Style.Numberformat.Format = "#,##0.00";
                                                    worksheet.Cells[incRowExcel, 11].Formula = "SUM(K" + _startRowDetail + ":K" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 11].Calculate();

                                                    worksheet.Cells[incRowExcel, 12].Style.Numberformat.Format = "#,##0.00";
                                                    worksheet.Cells[incRowExcel, 12].Formula = "SUM(L" + _startRowDetail + ":L" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 12].Calculate();

                                                    worksheet.Cells[incRowExcel, 13].Style.Numberformat.Format = "#,##0.00";
                                                    worksheet.Cells[incRowExcel, 13].Formula = "SUM(M" + _startRowDetail + ":M" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 13].Calculate();
                                                }

                                            }
                                            else if (rsHeader.Key.InstrumentTypeName == "BOND")
                                            {
                                                if (rsHeader.Key.TrxTypeID == "SELL")
                                                {
                                                    worksheet.Cells["A" + _endRowDetail + ":O" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                    worksheet.Cells[incRowExcel, 3].Value = "TOTAL";
                                                    worksheet.Cells["C" + incRowExcel + ":O" + incRowExcel].Style.Font.Bold = true;
                                                    worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0.00";
                                                    worksheet.Cells[incRowExcel, 8].Formula = "SUM(H" + _startRowDetail + ":H" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 8].Calculate();

                                                    worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0.00";
                                                    worksheet.Cells[incRowExcel, 10].Formula = "SUM(J" + _startRowDetail + ":J" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 10].Calculate();

                                                    worksheet.Cells[incRowExcel, 11].Style.Numberformat.Format = "#,##0.00";
                                                    worksheet.Cells[incRowExcel, 11].Formula = "SUM(K" + _startRowDetail + ":K" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 11].Calculate();

                                                    worksheet.Cells[incRowExcel, 12].Style.Numberformat.Format = "#,##0.00";
                                                    worksheet.Cells[incRowExcel, 12].Formula = "SUM(L" + _startRowDetail + ":L" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 12].Calculate();

                                                    worksheet.Cells[incRowExcel, 13].Style.Numberformat.Format = "#,##0.00";
                                                    worksheet.Cells[incRowExcel, 13].Formula = "SUM(M" + _startRowDetail + ":M" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 13].Calculate();

                                                    worksheet.Cells[incRowExcel, 15].Style.Numberformat.Format = "#,##0.00";
                                                    worksheet.Cells[incRowExcel, 15].Formula = "SUM(O" + _startRowDetail + ":O" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 15].Calculate();

                                                }
                                                else
                                                {
                                                    worksheet.Cells["A" + _endRowDetail + ":M" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                    worksheet.Cells[incRowExcel, 3].Value = "TOTAL";
                                                    worksheet.Cells["C" + incRowExcel + ":O" + incRowExcel].Style.Font.Bold = true;
                                                    worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0.00";
                                                    worksheet.Cells[incRowExcel, 8].Formula = "SUM(H" + _startRowDetail + ":H" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 8].Calculate();

                                                    worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0.00";
                                                    worksheet.Cells[incRowExcel, 10].Formula = "SUM(J" + _startRowDetail + ":J" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 10].Calculate();

                                                    worksheet.Cells[incRowExcel, 11].Style.Numberformat.Format = "#,##0.00";
                                                    worksheet.Cells[incRowExcel, 11].Formula = "SUM(K" + _startRowDetail + ":K" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 11].Calculate();

                                                    worksheet.Cells[incRowExcel, 12].Style.Numberformat.Format = "#,##0.00";
                                                    worksheet.Cells[incRowExcel, 12].Formula = "SUM(L" + _startRowDetail + ":L" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 12].Calculate();

                                                    worksheet.Cells[incRowExcel, 13].Style.Numberformat.Format = "#,##0.00";
                                                    worksheet.Cells[incRowExcel, 13].Formula = "SUM(M" + _startRowDetail + ":M" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 13].Calculate();
                                                }


                                            }
                                            else
                                            {
                                                worksheet.Cells["A" + _endRowDetail + ":K" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells[incRowExcel, 3].Value = "TOTAL";
                                                worksheet.Cells["C" + incRowExcel + ":K" + incRowExcel].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 5].Formula = "SUM(E" + _startRowDetail + ":E" + _endRowDetail + ")";
                                                worksheet.Cells[incRowExcel, 5].Calculate();

                                                worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 10].Formula = "SUM(J" + _startRowDetail + ":J" + _endRowDetail + ")";
                                                worksheet.Cells[incRowExcel, 10].Calculate();

                                                worksheet.Cells[incRowExcel, 11].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 11].Formula = "SUM(K" + _startRowDetail + ":K" + _endRowDetail + ")";
                                                worksheet.Cells[incRowExcel, 11].Calculate();


                                            }


                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 5].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 6].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 7].Style.Font.Bold = true;

                                            incRowExcel = incRowExcel + 2;



                                            incRowExcel++;
                                            worksheet.Row(incRowExcel).PageBreak = _FundAccountingRpt.PageBreak;

                                        }

                                        string _rangeDetail = "A:M";

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
                                        worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel, 15];
                                        worksheet.Column(1).Width = 15;
                                        worksheet.Column(2).Width = 5;
                                        worksheet.Column(3).Width = 15;
                                        worksheet.Column(4).Width = 40;
                                        worksheet.Column(5).Width = 20;
                                        worksheet.Column(6).AutoFit();
                                        worksheet.Column(7).AutoFit();
                                        worksheet.Column(8).AutoFit();
                                        worksheet.Column(9).Width = 15;
                                        worksheet.Column(10).AutoFit();
                                        worksheet.Column(11).AutoFit();
                                        worksheet.Column(12).AutoFit();
                                        worksheet.Column(13).Width = 15;
                                        worksheet.Column(14).Width = 15;
                                        worksheet.Column(15).Width = 25;
                                        worksheet.PrinterSettings.Orientation = eOrientation.Portrait; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                        // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                        worksheet.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
                                        worksheet.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
                                        worksheet.View.ShowGridLines = Tools.DefaultReportShowGridLines();
                                        Image img = Image.FromFile(Tools.ReportImage);
                                        worksheet.HeaderFooter.OddHeader.InsertPicture(img, PictureAlignment.Left);
                                        worksheet.HeaderFooter.OddHeader.CenteredText = "&14 Transaction Summary";



                                        worksheet.HeaderFooter.OddFooter.RightAlignedText =
                                        string.Format("Page {0} of {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);

                                        //worksheet.HeaderFooter.OddFooter.CenteredText = Tools.DefaultReportFooterCenterText();
                                        worksheet.HeaderFooter.OddFooter.LeftAlignedText = Tools.DefaultReportFooterLeftText();


                                        package.Save();
                                        if (_FundAccountingRpt.DownloadMode == "PDF")
                                        {
                                            Tools.ExportFromExcelToPDF(filePath, pdfPath);
                                        }
                                        //Tools.ExportFromExcelToPDF(filePath, pdfPath);
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
            #endregion

            #region Fund Portfolio
            if (_FundAccountingRpt.ReportName.Equals("Fund Portfolio"))
            {
                try
                {
                    using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                    {
                        DbCon.Open();
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {

                            string _paramFund = "";
                            string _paramInstrumentType = "";



                            if (_FundAccountingRpt.InstrumentTypeFrom == "1")
                            {
                                _paramInstrumentType = "and I.InstrumentTypePK in (1,4,16) ";
                            }
                            else if (_FundAccountingRpt.InstrumentTypeFrom == "2")
                            {
                                _paramInstrumentType = "and I.InstrumentTypePK in (2,3,9,13,15) ";
                            }
                            else if (_FundAccountingRpt.InstrumentTypeFrom == "3")
                            {
                                _paramInstrumentType = "and I.InstrumentTypePK in (5) ";
                            }
                            else
                            {
                                _paramInstrumentType = "";
                            }

                            if (!_host.findString(_FundAccountingRpt.FundFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_FundAccountingRpt.FundFrom))
                            {
                                _paramFund = "And F.FundPK in ( " + _FundAccountingRpt.FundFrom + " ) ";
                            }
                            else
                            {
                                _paramFund = "";
                            }


                            cmd.CommandText =
                                @"

                                select Q.ID CurrencyID,isnull(P.DescOne,'') FundType,FP.AvgPrice AvgPrice,FP.Date Date,I.InstrumentTypePK InstrumentTypePK,IT.Name InstrumentTypeName,
                                isnull(F.ID,'') FundID,isnull(F.Name,'') FundName,I.ID InstrumentID,I.Name InstrumentName,FP.MaturityDate MaturityDate,FP.Balance Balance,FP.CostValue CostValue,  
                                FP.ClosePrice ClosePrice
                                ,Case when IT.Type =3 then M.DescOne else '' end PeriodeActual
                                ,Case when IT.Type =3 then  dbo.[FGetDepositoInterestAccrued](@ValueDate,FP.InstrumentPK,Balance,Fp.InterestDaysType,Fp.InterestPercent,AcqDate) 
                                else 0 end AccrualHarian
                                ,Case when IT.Type =3 then  dbo.[FGetDepositoInterestAccrued](@ValueDate,FP.InstrumentPK,Balance,Fp.InterestDaysType,Fp.InterestPercent,AcqDate)
                                * datediff(day,DATEADD(month, DATEDIFF(month, 0, @ValueDate), 0),@ValueDate )
                                else FP.InterestPercent end Accrual
                                ,FP.InterestPercent 
                                ,FP.MarketValue MarketValue,
                                sum(FP.MarketValue - FP.CostValue)Unrealised,case when isnull(CN.AUM,0) = 0 then 0 else isnull(sum((FP.MarketValue / 
                                case when CN.AUM = 0 then 1 else isnull(CN.AUM,1) End) * 100),0) End PercentOfNav , sum(FP.Balance/100) Lot,case when FP.CostValue = 0 then 0 else sum(((FP.MarketValue - FP.CostValue)/case when FP.CostValue = 0 then 1 else FP.CostValue end )*100) end PercentFR 
                                ,O.SInvestID,O.Name BankName,N.ID BranchID,FP.AcqDate, DATEDIFF (day ,FP.AcqDate,@ValueDate ) AccruedDays, S.Name BankCustodian
                                from fundposition FP   
                                left join Instrument I on FP.InstrumentPK = I.InstrumentPK and I.status in (1,2)   
                                left join Fund F on FP.FundPK = F.FundPK and F.status in (1,2)   
                                left join InstrumentType IT on I.InstrumentTypePK = IT.InstrumentTypePK and IT.status in (1,2)  
                                left join CloseNav CN on CN.Date = dbo.FWorkingDay(@ValueDate ,-1) and FP.FundPK = CN.FundPK and CN.status in (1,2)  
                                left join MasterValue  M on FP.InterestPaymentType = M.Code and M.ID = 'InterestPaymentType'
                                left join BankBranch N on FP.BankBranchPK = N.BankBranchPK and N.status in (1,2)
                                left join Bank O on N.BankPK = O.BankPK and O.status in (1,2)
                                left join MasterValue P on F.Type = P.Code and P.status in (1,2) and P.ID = 'FundType'
                                left join Currency Q on FP.CurrencyPK = Q.CurrencyPK and Q.status in (1,2)
                                left join BankBranch R on F.BankBranchPK = R.BankBranchPK and R.status in (1,2)
                                left join Bank S on R.BankPK = S.BankPK and S.status in (1,2)
                                where FP.status in (1,2)  and FP.Date = @ValueDate  " + _paramFund + _paramInstrumentType + @"  
                                group by Q.ID,P.DescOne, Fp.AvgPrice,FP.Date,I.InstrumentTypePK, FP.AcqDate,I.ID ,I.InstrumentPK,IT.Name,F.ID,F.Name,I.Name ,FP.MaturityDate ,FP.Balance ,FP.CostValue ,  
                                FP.ClosePrice ,FP.InterestPercent ,FP.MarketValue,CN.AUM,IT.Type,FP.InstrumentPK,Fp.InterestDaysType,Fp.InterestPaymentType,Fp.MaturityDate,M.DescOne,O.SInvestID,O.Name,N.ID,FP.AcqDate, R.ID, S.Name
                                order by I.ID
                                ";

                            cmd.CommandTimeout = 0;
                            cmd.Parameters.AddWithValue("@ValueDate", _FundAccountingRpt.ValueDateFrom);


                            using (SqlDataReader dr0 = cmd.ExecuteReader())
                            {
                                if (!dr0.HasRows)
                                {
                                    return false;
                                }
                                else
                                {
                                    string filePath = Tools.ReportsPath + "FundPortfolio" + "_" + _userID + ".xlsx";
                                    string pdfPath = Tools.ReportsPath + "FundPortfolio" + "_" + _userID + ".pdf";
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

                                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Portfolio Valuation Report");



                                        //ATUR DATA GROUPINGNYA DULU
                                        List<PortfolioValuationReport> rList = new List<PortfolioValuationReport>();
                                        while (dr0.Read())
                                        {
                                            PortfolioValuationReport rSingle = new PortfolioValuationReport();
                                            rSingle.SecurityCode = Convert.ToString(dr0["InstrumentID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["InstrumentID"]));
                                            rSingle.SecurityDescription = Convert.ToString(dr0["InstrumentName"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["InstrumentName"]));
                                            rSingle.InstrumentTypeName = Convert.ToString(dr0["InstrumentTypeName"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["InstrumentTypeName"]));
                                            rSingle.QtyOfUnit = Convert.ToDecimal(dr0["Balance"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["Balance"]));
                                            rSingle.Lot = Convert.ToDecimal(dr0["Lot"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["Lot"]));
                                            rSingle.AverageCost = Convert.ToDecimal(dr0["AvgPrice"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["AvgPrice"]));
                                            rSingle.BookValue = Convert.ToDecimal(dr0["CostValue"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["CostValue"]));
                                            rSingle.MarketPrice = Convert.ToDecimal(dr0["ClosePrice"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["ClosePrice"]));
                                            rSingle.MarketValue = Convert.ToDecimal(dr0["MarketValue"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["MarketValue"]));
                                            rSingle.UnrealizedProfitLoss = Convert.ToDecimal(dr0["Unrealised"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["Unrealised"]));
                                            rSingle.PercentFR = Convert.ToDecimal(dr0["PercentFR"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["PercentFR"]));
                                            rSingle.BICode = Convert.ToString(dr0["SInvestID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["SInvestID"]));
                                            rSingle.Branch = Convert.ToString(dr0["BranchID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["BranchID"]));
                                            rSingle.Nominal = Convert.ToDecimal(dr0["Balance"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["Balance"]));
                                            rSingle.TradeDate = Convert.ToString(dr0["AcqDate"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["AcqDate"]));
                                            rSingle.MaturityDate = Convert.ToString(dr0["MaturityDate"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["MaturityDate"]));
                                            rSingle.RateGross = Convert.ToDecimal(dr0["InterestPercent"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["InterestPercent"]));
                                            rSingle.AccIntTD = Convert.ToDecimal(dr0["AccrualHarian"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["AccrualHarian"]));
                                            rSingle.PercentOfNav = Convert.ToDecimal(dr0["PercentOfNav"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["PercentOfNav"]));
                                            rSingle.FundID = Convert.ToString(dr0["FundID"]);
                                            rSingle.FundName = Convert.ToString(dr0["FundName"]);
                                            rSingle.FundType = Convert.ToString(dr0["FundType"]);
                                            rSingle.CurrencyID = Convert.ToString(dr0["CurrencyID"]);
                                            rSingle.InstrumentTypePK = Convert.ToInt32(dr0["InstrumentTypePK"]);
                                            rSingle.Date = Convert.ToString(dr0["Date"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["Date"]));
                                            rSingle.PeriodeActual = Convert.ToString(dr0["PeriodeActual"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["PeriodeActual"]));
                                            rSingle.Accrual = Convert.ToDecimal(dr0["Accrual"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["Accrual"]));
                                            rSingle.AccruedDays = Convert.ToDecimal(dr0["AccruedDays"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["AccruedDays"]));
                                            rSingle.BankCustodian = Convert.ToString(dr0["BankCustodian"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["BankCustodian"]));
                                            rList.Add(rSingle);

                                        }

                                        var QueryBySales =
                                         from r in rList
                                         orderby r.FundName, r.InstrumentTypePK, r.Date ascending
                                         group r by new { r.FundName, r.BankCustodian, r.InstrumentTypeName, r.Date, r.InstrumentTypePK, r.FundType, r.CurrencyID } into rGroup
                                         select rGroup;


                                        int incRowExcel = 0;

                                        incRowExcel = incRowExcel + 3;

                                        foreach (var rsHeader in QueryBySales)
                                        {
                                            incRowExcel = incRowExcel + 1;
                                            worksheet.Cells[incRowExcel, 2].Value = "Fund Manager";
                                            worksheet.Cells[incRowExcel, 3].Value = ":";
                                            worksheet.Cells[incRowExcel, 4].Value = _host.Get_CompanyName();
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 2].Value = "Bank Kustodian";
                                            worksheet.Cells[incRowExcel, 3].Value = ":";
                                            worksheet.Cells[incRowExcel, 4].Value = rsHeader.Key.BankCustodian;
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 2].Value = "Fund Name: ";
                                            worksheet.Cells[incRowExcel, 3].Value = ":";
                                            worksheet.Cells[incRowExcel, 4].Value = rsHeader.Key.FundName;
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 2].Value = "Fund Type: ";
                                            worksheet.Cells[incRowExcel, 3].Value = ":";
                                            worksheet.Cells[incRowExcel, 4].Value = rsHeader.Key.FundType;
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 2].Value = "Date";
                                            worksheet.Cells[incRowExcel, 3].Value = ":";
                                            worksheet.Cells[incRowExcel, 4].Value = Convert.ToDateTime(rsHeader.Key.Date).ToString("dd/MMM/yyyy");
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 2].Value = "Currency";
                                            worksheet.Cells[incRowExcel, 3].Value = ":";
                                            worksheet.Cells[incRowExcel, 4].Value = rsHeader.Key.CurrencyID;
                                            incRowExcel = incRowExcel + 2;
                                            worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 2].Value = "INSTRUMENT TYPE";
                                            worksheet.Cells[incRowExcel, 3].Value = ":";
                                            worksheet.Cells[incRowExcel, 4].Value = rsHeader.Key.InstrumentTypeName;
                                            worksheet.Cells[incRowExcel, 4].Style.Font.Bold = true;
                                            incRowExcel = incRowExcel + 2;

                                            if (rsHeader.Key.InstrumentTypePK == 1 || rsHeader.Key.InstrumentTypePK == 4 || rsHeader.Key.InstrumentTypePK == 16)
                                            {
                                                worksheet.Cells[incRowExcel, 1].Value = "NO";
                                                worksheet.Cells[incRowExcel, 2].Value = "CODE";
                                                worksheet.Cells["B" + incRowExcel + ":C" + incRowExcel].Merge = true;
                                                worksheet.Cells[incRowExcel, 4].Value = "Securities Name";
                                                worksheet.Cells[incRowExcel, 5].Value = "Quantity";
                                                worksheet.Cells[incRowExcel, 6].Value = "Avg. Book Cost";
                                                worksheet.Cells[incRowExcel, 7].Value = "Total Cost";
                                                worksheet.Cells[incRowExcel, 8].Value = "Market Price";
                                                worksheet.Cells[incRowExcel, 9].Value = "Market Value";
                                                worksheet.Cells[incRowExcel, 10].Value = "Unrealized";
                                                worksheet.Cells[incRowExcel, 11].Value = "(%) of NAV";


                                                worksheet.Cells["A" + incRowExcel + ":K" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells["A" + incRowExcel + ":K" + incRowExcel].Style.Font.Bold = true;
                                                worksheet.Cells["A" + incRowExcel + ":K" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + incRowExcel + ":K" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + incRowExcel + ":K" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + incRowExcel + ":K" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Medium;

                                            }

                                            else if (rsHeader.Key.InstrumentTypePK == 5)
                                            {
                                                worksheet.Cells["A" + incRowExcel + ":J" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 1].Value = "NO";
                                                worksheet.Cells[incRowExcel, 2].Value = "INS. ID";
                                                worksheet.Cells["B" + incRowExcel + ":C" + incRowExcel].Merge = true;
                                                worksheet.Cells[incRowExcel, 4].Value = "Bank Placement";
                                                worksheet.Cells[incRowExcel, 5].Value = "Placement Date";
                                                worksheet.Cells[incRowExcel, 6].Value = "MATURITY DATE";
                                                worksheet.Cells[incRowExcel, 7].Value = "TOTAL AMOUNT";
                                                worksheet.Cells[incRowExcel, 8].Value = "INTEREST %";
                                                worksheet.Cells[incRowExcel, 9].Value = "PERIODE ACCRUAL";
                                                worksheet.Cells[incRowExcel, 10].Value = "Accrued Days";
                                                worksheet.Cells[incRowExcel, 11].Value = "Daily Accrual";
                                                worksheet.Cells[incRowExcel, 12].Value = "Accrued Interest";
                                                worksheet.Cells[incRowExcel, 13].Value = "Accrued Tax";
                                                worksheet.Cells[incRowExcel, 14].Value = "Net Accrued Interest";
                                                worksheet.Cells[incRowExcel, 15].Value = "(%) of NAV";

                                                worksheet.Cells["A" + incRowExcel + ":O" + incRowExcel].Style.Font.Bold = true;
                                                worksheet.Cells["A" + incRowExcel + ":O" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + incRowExcel + ":O" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + incRowExcel + ":O" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + incRowExcel + ":O" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + incRowExcel + ":O" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;


                                            }

                                            else
                                            {

                                                worksheet.Cells["A" + incRowExcel + ":K" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 1].Value = "NO";
                                                worksheet.Cells[incRowExcel, 2].Value = "INS. ID";
                                                worksheet.Cells["B" + incRowExcel + ":C" + incRowExcel].Merge = true;
                                                worksheet.Cells[incRowExcel, 4].Value = "Securities Name";
                                                worksheet.Cells[incRowExcel, 5].Value = "Maturity Date";
                                                worksheet.Cells[incRowExcel, 6].Value = "NOMINAL FACE VALUE";
                                                worksheet.Cells[incRowExcel, 7].Value = "AVG COST";
                                                worksheet.Cells[incRowExcel, 8].Value = "COST(IDR)";
                                                worksheet.Cells[incRowExcel, 9].Value = "TERM OF INTEREST";
                                                worksheet.Cells[incRowExcel, 10].Value = "Market Price";
                                                worksheet.Cells[incRowExcel, 11].Value = "MARKET VALUE";
                                                worksheet.Cells[incRowExcel, 12].Value = "UNREALISED";
                                                worksheet.Cells[incRowExcel, 13].Value = "(%) of NAV";

                                                worksheet.Cells["A" + incRowExcel + ":M" + incRowExcel].Style.Font.Bold = true;
                                                worksheet.Cells["A" + incRowExcel + ":M" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + incRowExcel + ":M" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + incRowExcel + ":M" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + incRowExcel + ":M" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + incRowExcel + ":M" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;




                                            }

                                            incRowExcel++;
                                            int _startRowDetail = incRowExcel;
                                            int _endRowDetail = 0;
                                            int _no = 1;
                                            foreach (var rsDetail in rsHeader)
                                            {
                                                if (rsHeader.Key.InstrumentTypePK == 1 || rsHeader.Key.InstrumentTypePK == 4 || rsHeader.Key.InstrumentTypePK == 16)
                                                {
                                                    worksheet.Cells[incRowExcel, 1].Value = _no;
                                                    worksheet.Cells[incRowExcel, 2].Value = rsDetail.SecurityCode;
                                                    worksheet.Cells["B" + incRowExcel + ":C" + incRowExcel].Merge = true;
                                                    worksheet.Cells[incRowExcel, 4].Value = rsDetail.SecurityDescription;
                                                    worksheet.Cells[incRowExcel, 5].Value = rsDetail.QtyOfUnit;
                                                    worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.0000";
                                                    worksheet.Cells[incRowExcel, 6].Value = rsDetail.BookValue;
                                                    worksheet.Cells[incRowExcel, 7].Value = rsDetail.RateGross;
                                                    worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0.00";
                                                    worksheet.Cells[incRowExcel, 8].Value = rsDetail.MarketPrice;
                                                    worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0.00";
                                                    worksheet.Cells[incRowExcel, 9].Value = rsDetail.MarketValue;
                                                    worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0.0000";
                                                    worksheet.Cells[incRowExcel, 10].Value = rsDetail.UnrealizedProfitLoss;
                                                    worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0.00";
                                                    worksheet.Cells[incRowExcel, 11].Value = rsDetail.PercentOfNav;
                                                    worksheet.Cells[incRowExcel, 11].Style.Numberformat.Format = "#,##0.00";

                                                    worksheet.Cells["A" + incRowExcel + ":K" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                    worksheet.Cells["A" + incRowExcel + ":K" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                    worksheet.Cells["A" + incRowExcel + ":K" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                    worksheet.Cells["A" + incRowExcel + ":K" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Medium;



                                                }

                                                else if (rsHeader.Key.InstrumentTypePK == 5)
                                                {

                                                    worksheet.Cells[incRowExcel, 1].Value = _no;
                                                    worksheet.Cells[incRowExcel, 2].Value = rsDetail.SecurityCode;
                                                    worksheet.Cells["B" + incRowExcel + ":C" + incRowExcel].Merge = true;
                                                    //worksheet.Cells[incRowExcel, 3].Value = rsDetail.BICode;
                                                    worksheet.Cells[incRowExcel, 4].Value = rsDetail.SecurityDescription;
                                                    worksheet.Cells[incRowExcel, 4].Style.WrapText = true;
                                                    worksheet.Cells[incRowExcel, 5].Value = Convert.ToDateTime(rsDetail.TradeDate).ToString("dd-MMM-yyyy");
                                                    worksheet.Cells[incRowExcel, 6].Value = Convert.ToDateTime(rsDetail.MaturityDate).ToString("dd-MMM-yyyy");
                                                    worksheet.Cells[incRowExcel, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                    worksheet.Cells[incRowExcel, 7].Value = rsDetail.BookValue;
                                                    worksheet.Cells[incRowExcel, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                    worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0.00";
                                                    worksheet.Cells[incRowExcel, 8].Value = rsDetail.RateGross;
                                                    worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0.00";
                                                    worksheet.Cells[incRowExcel, 9].Value = rsDetail.PeriodeActual;
                                                    worksheet.Cells[incRowExcel, 10].Value = rsDetail.AccruedDays;
                                                    worksheet.Cells[incRowExcel, 11].Value = rsDetail.AccIntTD;
                                                    worksheet.Cells[incRowExcel, 11].Style.Numberformat.Format = "#,##0.00";
                                                    worksheet.Cells[incRowExcel, 12].Formula = "SUM(K" + incRowExcel + "*J" + incRowExcel + ")";
                                                    worksheet.Cells[incRowExcel, 12].Style.Numberformat.Format = "#,##0.00";
                                                    worksheet.Cells[incRowExcel, 13].Formula = "SUM(L" + incRowExcel + "*0.2" + ")";
                                                    worksheet.Cells[incRowExcel, 13].Style.Numberformat.Format = "#,##0.00";
                                                    worksheet.Cells[incRowExcel, 13].Calculate();
                                                    worksheet.Cells[incRowExcel, 14].Formula = "SUM(L" + incRowExcel + "-M" + incRowExcel + ")";
                                                    worksheet.Cells[incRowExcel, 14].Style.Numberformat.Format = "#,##0.00";
                                                    worksheet.Cells[incRowExcel, 14].Calculate();
                                                    worksheet.Cells[incRowExcel, 15].Value = rsDetail.PercentOfNav;
                                                    worksheet.Cells[incRowExcel, 15].Style.Numberformat.Format = "#,##0.00";

                                                    worksheet.Cells["A" + incRowExcel + ":O" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                    worksheet.Cells["A" + incRowExcel + ":O" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                    worksheet.Cells["A" + incRowExcel + ":O" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                    worksheet.Cells["A" + incRowExcel + ":O" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                                }
                                                else
                                                {
                                                    worksheet.Cells[incRowExcel, 1].Value = _no;
                                                    worksheet.Cells[incRowExcel, 2].Value = rsDetail.SecurityCode;
                                                    worksheet.Cells["B" + incRowExcel + ":C" + incRowExcel].Merge = true;
                                                    //worksheet.Cells[incRowExcel, 3].Value = rsDetail.SecurityDescription;
                                                    worksheet.Cells[incRowExcel, 4].Value = rsDetail.SecurityDescription;
                                                    worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##0";
                                                    worksheet.Cells[incRowExcel, 5].Value = Convert.ToDateTime(rsDetail.MaturityDate).ToString("dd-MMM-yyyy");
                                                    worksheet.Cells[incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                    worksheet.Cells[incRowExcel, 6].Value = rsDetail.QtyOfUnit;
                                                    worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.00";
                                                    worksheet.Cells[incRowExcel, 7].Value = rsDetail.AverageCost;
                                                    worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0.00";
                                                    worksheet.Cells[incRowExcel, 8].Value = rsDetail.BookValue;
                                                    worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0.00";
                                                    worksheet.Cells[incRowExcel, 9].Value = rsDetail.RateGross;
                                                    worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0.00";
                                                    worksheet.Cells[incRowExcel, 10].Value = rsDetail.MarketPrice;
                                                    worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0.0000";
                                                    worksheet.Cells[incRowExcel, 11].Value = rsDetail.MarketValue;
                                                    worksheet.Cells[incRowExcel, 11].Style.Numberformat.Format = "#,##0.00";
                                                    worksheet.Cells[incRowExcel, 12].Value = rsDetail.UnrealizedProfitLoss;
                                                    worksheet.Cells[incRowExcel, 12].Style.Numberformat.Format = "#,##0.00";
                                                    worksheet.Cells[incRowExcel, 13].Value = rsDetail.PercentOfNav;
                                                    worksheet.Cells[incRowExcel, 13].Style.Numberformat.Format = "#,##0.00";

                                                    worksheet.Cells["A" + incRowExcel + ":M" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                    worksheet.Cells["A" + incRowExcel + ":M" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                    worksheet.Cells["A" + incRowExcel + ":M" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                    worksheet.Cells["A" + incRowExcel + ":M" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                                }

                                                _endRowDetail = incRowExcel;
                                                _no++;
                                                incRowExcel++;
                                            }

                                            if (rsHeader.Key.InstrumentTypePK == 1 || rsHeader.Key.InstrumentTypePK == 4 || rsHeader.Key.InstrumentTypePK == 16)
                                            {
                                                worksheet.Cells["A" + incRowExcel + ":K" + incRowExcel].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 2].Value = "TOTAL";
                                                worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 11].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 7].Formula = "SUM(G" + _startRowDetail + ":G" + _endRowDetail + ")";
                                                worksheet.Cells[incRowExcel, 9].Formula = "SUM(I" + _startRowDetail + ":I" + _endRowDetail + ")";
                                                worksheet.Cells[incRowExcel, 10].Formula = "SUM(J" + _startRowDetail + ":J" + _endRowDetail + ")";
                                                worksheet.Cells[incRowExcel, 11].Formula = "SUM(K" + _startRowDetail + ":K" + _endRowDetail + ")";
                                                worksheet.Cells[incRowExcel, 4].Calculate();
                                                worksheet.Cells[incRowExcel, 7].Calculate();
                                                worksheet.Cells[incRowExcel, 9].Calculate();
                                                worksheet.Cells[incRowExcel, 10].Calculate();
                                                worksheet.Cells[incRowExcel, 11].Calculate();


                                            }
                                            else if (rsHeader.Key.InstrumentTypePK == 5)
                                            {
                                                worksheet.Cells["A" + incRowExcel + ":O" + incRowExcel].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 2].Value = "TOTAL";
                                                //worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 11].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 12].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 13].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 14].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 15].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 7].Formula = "SUM(G" + _startRowDetail + ":G" + _endRowDetail + ")";
                                                //worksheet.Cells[incRowExcel, 8].Formula = "SUM(H" + _startRowDetail + ":H" + _endRowDetail + ")";
                                                //worksheet.Cells[incRowExcel, 10].Formula = "SUM(J" + _startRowDetail + ":J" + _endRowDetail + ")";
                                                worksheet.Cells[incRowExcel, 11].Formula = "SUM(K" + _startRowDetail + ":K" + _endRowDetail + ")";
                                                worksheet.Cells[incRowExcel, 12].Formula = "SUM(L" + _startRowDetail + ":L" + _endRowDetail + ")";
                                                worksheet.Cells[incRowExcel, 13].Formula = "SUM(M" + _startRowDetail + ":M" + _endRowDetail + ")";
                                                worksheet.Cells[incRowExcel, 14].Formula = "SUM(N" + _startRowDetail + ":N" + _endRowDetail + ")";
                                                worksheet.Cells[incRowExcel, 15].Formula = "SUM(O" + _startRowDetail + ":O" + _endRowDetail + ")";
                                                worksheet.Cells[incRowExcel, 4].Calculate();
                                                worksheet.Cells[incRowExcel, 7].Calculate();
                                                worksheet.Cells[incRowExcel, 10].Calculate();
                                                worksheet.Cells[incRowExcel, 11].Calculate();
                                                worksheet.Cells[incRowExcel, 12].Calculate();
                                                worksheet.Cells[incRowExcel, 13].Calculate();
                                                worksheet.Cells[incRowExcel, 14].Calculate();
                                                worksheet.Cells[incRowExcel, 15].Calculate();

                                            }
                                            else
                                            {

                                                worksheet.Cells["A" + incRowExcel + ":M" + incRowExcel].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 2].Value = "TOTAL";
                                                worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 11].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 12].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 13].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 6].Formula = "SUM(F" + _startRowDetail + ":F" + _endRowDetail + ")";
                                                worksheet.Cells[incRowExcel, 8].Formula = "SUM(H" + _startRowDetail + ":H" + _endRowDetail + ")";
                                                worksheet.Cells[incRowExcel, 11].Formula = "SUM(K" + _startRowDetail + ":K" + _endRowDetail + ")";
                                                worksheet.Cells[incRowExcel, 12].Formula = "SUM(L" + _startRowDetail + ":L" + _endRowDetail + ")";
                                                worksheet.Cells[incRowExcel, 13].Formula = "SUM(M" + _startRowDetail + ":M" + _endRowDetail + ")";
                                                worksheet.Cells[incRowExcel, 8].Calculate();
                                                worksheet.Cells[incRowExcel, 11].Calculate();
                                                worksheet.Cells[incRowExcel, 12].Calculate();
                                                worksheet.Cells[incRowExcel, 13].Calculate();
                                            }

                                            incRowExcel = incRowExcel + 2;
                                            worksheet.Row(incRowExcel).PageBreak = _FundAccountingRpt.PageBreak;

                                            incRowExcel++;

                                        }


                                        worksheet.PrinterSettings.FitToPage = true;
                                        worksheet.PrinterSettings.FitToWidth = 1;
                                        worksheet.PrinterSettings.FitToHeight = 0;
                                        worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel - 1, 15];
                                        worksheet.Column(1).Width = 5;
                                        worksheet.Column(2).Width = 20;
                                        worksheet.Column(3).Width = 2;
                                        worksheet.Column(4).Width = 45;
                                        worksheet.Column(5).Width = 15;
                                        worksheet.Column(6).Width = 25;
                                        worksheet.Column(7).Width = 22;
                                        worksheet.Column(8).Width = 20;
                                        worksheet.Column(9).Width = 22;
                                        worksheet.Column(10).Width = 20;
                                        worksheet.Column(11).Width = 20;
                                        worksheet.Column(12).Width = 20;
                                        worksheet.Column(13).Width = 15;
                                        worksheet.Column(14).Width = 20;
                                        worksheet.Column(15).Width = 15;


                                        // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                        //worksheet.Cells.AutoFitColumns(); // UNTUK AUTO FIT UKURAN TULISAN DENGAN KOLOM, KEBETULAN INI BUAT HEADER AJA DIPAKENYA
                                        worksheet.PrinterSettings.Orientation = eOrientation.Landscape; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                        worksheet.HeaderFooter.OddHeader.CenteredText = "&30 PORTFOLIO VALUATION REPORT";

                                        // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                        worksheet.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
                                        worksheet.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
                                        worksheet.View.PageLayoutView = Tools.DefaultReportPageLayoutView();
                                        worksheet.View.ShowGridLines = Tools.DefaultReportShowGridLines();
                                        //worksheet.HeaderFooter.OddHeader.CenteredText = Tools.DefaultReportHeaderCenterText();
                                        //worksheet.HeaderFooter.OddHeader.LeftAlignedText = Tools.DefaultReportHeaderLeftText();

                                        Image img = Image.FromFile(Tools.ReportImage);
                                        worksheet.HeaderFooter.OddHeader.InsertPicture(img, PictureAlignment.Left);
                                        worksheet.HeaderFooter.OddHeader.RightAlignedText = Tools.DefaultReportHeaderRightText();
                                        worksheet.HeaderFooter.OddFooter.RightAlignedText =
                                        string.Format("Page {0} of {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);

                                        //worksheet.HeaderFooter.OddFooter.CenteredText = Tools.DefaultReportFooterCenterText();
                                        worksheet.HeaderFooter.OddFooter.LeftAlignedText = Tools.DefaultReportFooterLeftText();

                                        package.Save();
                                        if (_FundAccountingRpt.DownloadMode == "PDF")
                                        {
                                            Tools.ExportFromExcelToPDF(filePath, pdfPath);
                                        }
                                        //Tools.ExportFromExcelToPDF(filePath, pdfPath);
                                        return true;
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

            #endregion

            #region Counterpart Transaction Grouping By Broker Code
            if (_FundAccountingRpt.ReportName.Equals("Counterpart Transaction Grouping By Broker Code"))
            {
                try
                {
                    using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                    {
                        DbCon.Open();
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            string _paramCounterpart = "";
                            string _instrumentType = "";

                            if (_FundAccountingRpt.InstrumentTypeFrom != "0")
                            {
                                _instrumentType = "And D.GroupType  = @GroupType ";
                            }
                            else
                            {
                                _instrumentType = "";
                            }


                            if (!_host.findString(_FundAccountingRpt.CounterpartFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_FundAccountingRpt.CounterpartFrom))
                            {
                                _paramCounterpart = "And A.CounterpartPK  in ( " + _FundAccountingRpt.CounterpartFrom + " ) ";
                            }
                            else
                            {
                                _paramCounterpart = "";
                            }

                            cmd.CommandText = @"
                           
                            --declare @ValuedateFrom datetime
                            --declare @valuedateto datetime

                            --set @ValuedateFrom = '01/01/2020'
                            --set @valuedateto = '08/24/2020'

                            select A.TransactionType,A.BrokerCode,sum(TotalYTD) TotalYTD, sum(TotalCommission) TotalCommission, sum(TotalWHT) TotalWHT, sum(PercentageNav) PercentageNav
                            from (
                            select C.FundPK FundPK,C.Name FundName, F.DescOne FundType,
                            CASE when A.InstrumentTypePK in (2,3,9,13,15) then 'Bond' else case when A.InstrumentTypePK in (1,16) then 'Equity' else D.Name end end TransactionType,
                            ISNULL(B.ID,'') BrokerCode,sum(A.DoneAmount) TotalYTD, CASE when A.InstrumentTypePK in (2,3,9,13,15) AND A.TrxType = 1 THEN   
                            SUM(ISNULL(100 / CASE WHEN A.IncomeTaxGainPercent = 0 THEN 1 ELSE A.incomeTaxGainPercent END * ISNULL(A.IncomeTaxGainAmount,0),0) )
                            ELSE  SUM(ISNULL(A.CommissionAmount,0)) END TotalCommission,
                            CASE when A.InstrumentTypePK in (2,3,9,13,15) THEN SUM(ISNULL(A.incomeTaxInterestAmount,0) + ISNULL(A.incomeTaxGainAmount,0)) ELSE  SUM(ISNULL(A.WHTAmount,0)) END TotalWHT,
                            SUM(A.DoneAmount)/[dbo].[FGetAmountFromInvestmentByInstrumentTypeGroupingByBrokerCodeYTD](@ValueDateTo,A.InstrumentTypePK) PercentageNav from Investment A
                            left join Counterpart B on A.CounterpartPK = B.CounterpartPK and B.status in (1,2) 
                            left join Fund C on A.FundPK = C.FundPK and C.Status  in (1,2) 
                            left join InstrumentType D on A.InstrumentTypePK = D.InstrumentTypePK and D.Status in (1,2)
                            left join MasterValue F on C.Type = F.Code and F.ID = 'FundType'
                            Where A.ValueDate between @ValueDateFrom and @ValueDateTo and A.InstrumentTypePK <> 5  and C.Type <> 10
                            " + _instrumentType + _paramCounterpart + @" 
                            and A.StatusSettlement = 2
                            group by C.FundPK,C.Name,F.DescOne,A.InstrumentTypePK,D.Name,B.ID,A.FundPK,A.TrxType
                            )A
                            group by A.TransactionType,A.BrokerCode

                            ";

                            cmd.CommandTimeout = 0;
                            cmd.Parameters.AddWithValue("@ValueDateFrom", _FundAccountingRpt.ValueDateFrom);
                            cmd.Parameters.AddWithValue("@ValueDateTo", _FundAccountingRpt.ValueDateTo);
                            cmd.Parameters.AddWithValue("@GroupType", _FundAccountingRpt.InstrumentTypeFrom);

                            using (SqlDataReader dr0 = cmd.ExecuteReader())
                            {
                                if (!dr0.HasRows)
                                {
                                    return false;
                                }
                                else
                                {
                                    string filePath = Tools.ReportsPath + "CounterpartTransactionGroupingByBrokerCode" + "_" + _userID + ".xlsx";
                                    string pdfPath = Tools.ReportsPath + "CounterpartTransactionGroupingByBrokerCode" + "_" + _userID + ".pdf";
                                    FileInfo excelFile = new FileInfo(filePath);
                                    if (excelFile.Exists)
                                    {
                                        excelFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                        excelFile = new FileInfo(filePath);
                                    }

                                    // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                    using (ExcelPackage package = new ExcelPackage(excelFile))
                                    {
                                        package.Workbook.Properties.Title = "CounterpartTransactionGroupingByBrokerCode";
                                        package.Workbook.Properties.Author = Tools.DefaultReportAuthor();
                                        package.Workbook.Properties.Comments = Tools.DefaultReportComments();
                                        package.Workbook.Properties.Company = Tools.DefaultReportCompany();
                                        package.Workbook.Properties.SetCustomPropertyValue("Checked by", _userID);
                                        package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", Tools.DefaultReportAssemblyName());

                                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Counterpart Transaction Grouping By Broker Code");




                                        //ATUR DATA GROUPINGNYA DULU
                                        List<CounterpartTransaction> rList = new List<CounterpartTransaction>();
                                        while (dr0.Read())
                                        {
                                            CounterpartTransaction rSingle = new CounterpartTransaction();
                                            rSingle.TransactionType = Convert.ToString(dr0["TransactionType"]);
                                            rSingle.BrokerCode = Convert.ToString(dr0["BrokerCode"]);
                                            rSingle.TotalYTD = Convert.ToDecimal(dr0["TotalYTD"]);
                                            rSingle.TotalCommission = Convert.ToDecimal(dr0["TotalCommission"]);
                                            rSingle.TotalWHT = Convert.ToDecimal(dr0["TotalWHT"]);
                                            rSingle.PercentageNAV = Convert.ToDecimal(dr0["PercentageNAV"]);
                                            rList.Add(rSingle);

                                        }

                                        var GroupByReference =
                                            from r in rList
                                                ////orderby r.FundJournalAccountID, r.ValueDate, r.RefNo, r.FundClientID, r.InstrumentID ascending
                                            group r by new { r.TransactionType } into rGroup
                                            select rGroup;

                                        int incRowExcel = 0;

                                        incRowExcel = incRowExcel + 1;




                                        foreach (var rsHeader in GroupByReference)
                                        {
                                            worksheet.Cells[incRowExcel, 1].Value = "Fund Manager";
                                            worksheet.Cells[incRowExcel, 2].Value = _host.Get_CompanyName();
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Value = "Date From";
                                            worksheet.Cells[incRowExcel, 2].Value = _FundAccountingRpt.ValueDateFrom;
                                            worksheet.Cells[incRowExcel, 2].Style.Numberformat.Format = "dd/MMM/yyyy";
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Value = "Date To";
                                            worksheet.Cells[incRowExcel, 2].Value = _FundAccountingRpt.ValueDateTo;
                                            worksheet.Cells[incRowExcel, 2].Style.Numberformat.Format = "dd/MMM/yyyy";
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            incRowExcel = incRowExcel + 2;

                                            worksheet.Cells[incRowExcel, 1].Value = "Counterpart Transaction";
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells["A" + incRowExcel + ":E" + incRowExcel].Style.Font.Size = 25;
                                            worksheet.Cells["A" + incRowExcel + ":E" + incRowExcel].Merge = true;
                                            worksheet.Cells["A" + incRowExcel + ":E" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            incRowExcel++;

                                            worksheet.Cells[incRowExcel, 1].Value = "TRANSACTION";
                                            worksheet.Cells[incRowExcel, 2].Value = rsHeader.Key.TransactionType;
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                            incRowExcel++;

                                            int RowB = incRowExcel;
                                            int RowG = incRowExcel + 1;

                                            int _startRowDetailBorder = incRowExcel;
                                            worksheet.Cells[incRowExcel, 1].Value = "BROKER CODE";
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 1].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 2].Value = "TOTAL VALUE YTD";
                                            worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 2].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 3].Value = "TOTAL COMMISSION";
                                            worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 3].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 4].Value = "TOTAL WHT";
                                            worksheet.Cells[incRowExcel, 4].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 4].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 5].Value = "% FROM NAV";
                                            worksheet.Cells[incRowExcel, 5].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 5].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            //ThickBox Border

                                            worksheet.Cells["A" + RowB + ":E" + RowG].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + RowB + ":E" + RowG].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + RowB + ":E" + RowG].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + RowB + ":E" + RowG].Style.Border.Right.Style = ExcelBorderStyle.Medium;


                                            incRowExcel++;

                                            int _no = 1;
                                            int _startRowDetail = incRowExcel;
                                            int _endRowDetail = 0;
                                            foreach (var rsDetail in rsHeader)
                                            {
                                                //ThickBox Border

                                                int RowD = incRowExcel;
                                                int RowE = incRowExcel + 1;
                                                worksheet.Cells["A" + RowD + ":E" + RowE].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + RowD + ":E" + RowE].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + RowD + ":E" + RowE].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + RowD + ":E" + RowE].Style.Border.Right.Style = ExcelBorderStyle.Medium;



                                                //area detail
                                                worksheet.Cells[incRowExcel, 1].Value = rsDetail.BrokerCode;
                                                worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 2].Value = rsDetail.TotalYTD;
                                                worksheet.Cells[incRowExcel, 2].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.TotalCommission;
                                                worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                worksheet.Cells[incRowExcel, 4].Value = rsDetail.TotalWHT;
                                                worksheet.Cells[incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 5].Value = rsDetail.PercentageNAV;
                                                worksheet.Cells[incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.00%";
                                                _endRowDetail = incRowExcel;
                                                _no++;
                                                incRowExcel++;

                                            }


                                            //worksheet.Cells["A" + _startRowDetailBorder + ":E" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            //worksheet.Cells["A" + _startRowDetailBorder + ":E" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                            //worksheet.Cells["A" + _startRowDetailBorder + ":E" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            //worksheet.Cells["A" + _startRowDetailBorder + ":E" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Medium;


                                            //worksheet.Cells["A" + _endRowDetail + ":G" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells[incRowExcel, 1].Value = "TOTAL : ";
                                            worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 2].Style.Numberformat.Format = "#,##0.00";
                                            worksheet.Cells[incRowExcel, 2].Formula = "SUM(B" + _startRowDetail + ":B" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 2].Calculate();
                                            worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "#,##0.00";
                                            worksheet.Cells[incRowExcel, 3].Formula = "SUM(C" + _startRowDetail + ":C" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 3].Calculate();
                                            worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##0.00";
                                            worksheet.Cells[incRowExcel, 4].Formula = "SUM(D" + _startRowDetail + ":D" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 4].Calculate();
                                            worksheet.Cells[incRowExcel, 5].Formula = "SUM(E" + _startRowDetail + ":E" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.00%";
                                            worksheet.Cells[incRowExcel, 5].Calculate();
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 4].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 5].Style.Font.Bold = true;


                                            incRowExcel = incRowExcel + 2;



                                            worksheet.Row(incRowExcel).PageBreak = _FundAccountingRpt.PageBreak;
                                        }

                                        int _lastRow = incRowExcel;

                                        string _rangeA = "A:E" + incRowExcel;
                                        using (ExcelRange r = worksheet.Cells[_rangeA]) // KALO  KOLOM 1 SAMPE 9 A-I
                                        {
                                            r.Style.Font.Size = 14;
                                        }

                                        worksheet.DeleteRow(_lastRow);

                                        worksheet.PrinterSettings.FitToPage = true;
                                        worksheet.PrinterSettings.FitToWidth = 1;
                                        worksheet.PrinterSettings.FitToHeight = 0;
                                        worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel - 1, 5];
                                        worksheet.Column(1).Width = 20;
                                        worksheet.Column(2).Width = 30;
                                        worksheet.Column(3).Width = 25;
                                        worksheet.Column(4).Width = 20;
                                        worksheet.Column(5).Width = 20;

                                        // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                        //worksheet.Cells["A3:I14"].AutoFitColumns(); // UNTUK AUTO FIT UKURAN TULISAN DENGAN KOLOM, KEBETULAN INI BUAT HEADER AJA DIPAKENYA
                                        //worksheetApproved.Cells["A3:E3"].AutoFitColumns();
                                        worksheet.PrinterSettings.Orientation = eOrientation.Portrait; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                        worksheet.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN

                                        //worksheet.HeaderFooter.OddHeader.CenteredText = "\n \n \n \n \n \n \n \n \n &30&B Transaction Report Laporan Akun Bulanan Client";

                                        Image img = Image.FromFile(Tools.ReportImage);
                                        worksheet.HeaderFooter.OddHeader.InsertPicture(img, PictureAlignment.Left);
                                        worksheet.HeaderFooter.OddHeader.RightAlignedText = Tools.DefaultReportHeaderRightText();
                                        worksheet.HeaderFooter.OddHeader.CenteredText = "&12 COUNTERPART TRANSACTION BY GROUPING BROKER CODE";
                                        // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                        worksheet.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
                                        worksheet.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
                                        worksheet.View.PageLayoutView = Tools.DefaultReportPageLayoutView();
                                        worksheet.View.ShowGridLines = Tools.DefaultReportShowGridLines();
                                        //worksheet.HeaderFooter.OddHeader.CenteredText = Tools.DefaultReportHeaderCenterText();
                                        //worksheet.HeaderFooter.OddHeader.LeftAlignedText = Tools.DefaultReportHeaderLeftText();
                                        //worksheet.HeaderFooter.OddFooter.LeftAlignedText = Tools.DefaultReportFooterLeftTextDisclaimer();
                                        //
                                        worksheet.HeaderFooter.OddFooter.RightAlignedText =
                                        string.Format("Page {0} of {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);

                                        ////worksheet.HeaderFooter.OddFooter.CenteredText = Tools.DefaultReportFooterCenterText();
                                        //worksheet.HeaderFooter.OddFooter.LeftAlignedText = Tools.DefaultReportFooterLeftText();



                                        package.Save();
                                        if (_FundAccountingRpt.DownloadMode == "PDF")
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
            #endregion


            //            #region Daily Compliance Report
            //            else if (_FundAccountingRpt.ReportName.Equals("Daily Compliance Report"))
            //            {
            //                try
            //                {
            //                    using (SqlConnection DbCon = new SqlConnection(Tools.conString))
            //                    {
            //                        DbCon.Open();
            //                        using (SqlCommand cmd = DbCon.CreateCommand())
            //                        {

            //                            string _paramFund = "";

            //                            if (!_host.findString(_FundAccountingRpt.FundFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_FundAccountingRpt.FundFrom))
            //                            {
            //                                _paramFund = " And A.FundPK in ( " + _FundAccountingRpt.FundFrom + " ) ";

            //                            }
            //                            else
            //                            {
            //                                _paramFund = "";
            //                            }

            //                            cmd.CommandText =

            //                             @"
            //                            
            //                                create table #A (FundID nvarchar(50),FundName nvarchar(100),DepositoAmount numeric(22,4),DEPPercentOfNav numeric(18,4),BondAmount numeric(22,4),BondPercentOfNav numeric(18,4),EquityAmount numeric(22,4),EQPercentOfNav numeric(18,4))
            //                                insert into #A (FundID,FundName,DepositoAmount,DEPPercentOfNav,BondAmount,BondPercentOfNav,EquityAmount,EQPercentOfNav)
            //                                select B.ID,B.Name,0,0,0,0,isnull(sum(A.MarketValue),0) MarketValue,case when isnull(D.AUM,0) = 0 then 0 else isnull(sum((A.MarketValue / 
            //                                case when D.AUM = 0 then 1 else isnull(D.AUM,1) End) * 100),0) End PercentOfNav   from FundPosition A 
            //                                left join Fund B on A.FundPK = B.FundPK and B.Status = 2
            //                                left join Instrument C on A.InstrumentPK = C.InstrumentPK and C.Status = 2
            //                                left join CloseNav D on D.Date = A.Date and A.FundPK = D.FundPK and D.status = 2  
            //                                where A.Date = @valuedate and A.status = 2 and InstrumentTypePK in (1,4,16) " + _paramFund + @"
            //                                group by B.ID,B.Name,D.AUM
            //
            //                                union all
            //                                select B.ID,B.Name,0,0,isnull(sum(A.MarketValue),0) MarketValue,case when isnull(D.AUM,0) = 0 then 0 else isnull(sum((A.MarketValue / 
            //                                case when D.AUM = 0 then 1 else isnull(D.AUM,1) End) * 100),0) End PercentOfNav,0,0   from FundPosition A 
            //                                left join Fund B on A.FundPK = B.FundPK and B.Status = 2
            //                                left join Instrument C on A.InstrumentPK = C.InstrumentPK and C.Status = 2
            //                                left join CloseNav D on D.Date = A.Date and A.FundPK = D.FundPK and D.status = 2  
            //                                where A.Date = @valuedate and A.status = 2 and InstrumentTypePK not in (1,4,5,6,16) " + _paramFund + @"
            //                                group by B.ID,B.Name,D.AUM
            //
            //                                union all
            //                                select B.ID,B.Name,isnull(sum(A.MarketValue),0) MarketValue,case when isnull(D.AUM,0) = 0 then 0 else isnull(sum((A.MarketValue / 
            //                                case when D.AUM = 0 then 1 else isnull(D.AUM,1) End) * 100),0) End PercentOfNav,0,0,0,0   from FundPosition A 
            //                                left join Fund B on A.FundPK = B.FundPK and B.Status = 2
            //                                left join Instrument C on A.InstrumentPK = C.InstrumentPK and C.Status = 2
            //                                left join CloseNav D on D.Date = A.Date and A.FundPK = D.FundPK and D.status = 2  
            //                                where A.Date = @valuedate and A.status = 2 and InstrumentTypePK in (5) " + _paramFund + @"
            //                                group by B.ID,B.Name,D.AUM
            //
            //                                select @ValueDate Date,FundID,FundName,sum(DepositoAmount) DepositoAmount,sum(DEPPercentOfNav) DEPPercentOfNav,sum(BondAmount) BondAmount,sum(BondPercentOfNav) BondPercentOfNav,sum(EquityAmount) EquityAmount,sum(EQPercentOfNav) EQPercentOfNav,sum(DEPPercentOfNav + BondPercentOfNav + EQPercentOfNav ) TotalPercent  from #A
            //                                group By FundID,FundName
            //                                ";

            //                            cmd.CommandTimeout = 0;
            //                            cmd.Parameters.AddWithValue("@ValueDate", _FundAccountingRpt.ValueDateTo);

            //                            using (SqlDataReader dr0 = cmd.ExecuteReader())
            //                            {
            //                                if (!dr0.HasRows)
            //                                {
            //                                    return false;
            //                                }
            //                                else
            //                                {
            //                                    string filePath = Tools.ReportsPath + "DailyComplianceReport" + "_" + _userID + ".xlsx";
            //                                    string pdfPath = Tools.ReportsPath + "DailyComplianceReport" + "_" + _userID + ".pdf";
            //                                    FileInfo excelFile = new FileInfo(filePath);
            //                                    if (excelFile.Exists)
            //                                    {
            //                                        excelFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
            //                                        excelFile = new FileInfo(filePath);
            //                                    }

            //                                    // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
            //                                    using (ExcelPackage package = new ExcelPackage(excelFile))
            //                                    {
            //                                        package.Workbook.Properties.Title = "FundAccountingReport";
            //                                        package.Workbook.Properties.Author = Tools.DefaultReportAuthor();
            //                                        package.Workbook.Properties.Comments = Tools.DefaultReportComments();
            //                                        package.Workbook.Properties.Company = Tools.DefaultReportCompany();
            //                                        package.Workbook.Properties.SetCustomPropertyValue("Checked by", _userID);
            //                                        package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", Tools.DefaultReportAssemblyName());

            //                                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Daily Transaction Report");


            //                                        //ATUR DATA GROUPINGNYA DULU
            //                                        List<DailyComplianceReport> rList = new List<DailyComplianceReport>();
            //                                        while (dr0.Read())
            //                                        {
            //                                            DailyComplianceReport rSingle = new DailyComplianceReport();
            //                                            rSingle.Date = Convert.ToString(dr0["Date"]);
            //                                            rSingle.FundID = Convert.ToString(dr0["FundID"]);
            //                                            rSingle.FundName = Convert.ToString(dr0["FundName"]);
            //                                            rSingle.DepositoAmount = Convert.ToDecimal(dr0["DepositoAmount"]);
            //                                            rSingle.DEPPercentOfNav = Convert.ToDecimal(dr0["DEPPercentOfNav"]);
            //                                            rSingle.BondAmount = Convert.ToDecimal(dr0["BondAmount"]);
            //                                            rSingle.BondPercentOfNav = Convert.ToDecimal(dr0["BondPercentOfNav"]);
            //                                            rSingle.EquityAmount = Convert.ToDecimal(dr0["EquityAmount"]);
            //                                            rSingle.EQPercentOfNav = Convert.ToDecimal(dr0["EQPercentOfNav"]);
            //                                            rSingle.TotalPercent = Convert.ToDecimal(dr0["TotalPercent"]);
            //                                            rList.Add(rSingle);

            //                                        }


            //                                        var QueryByFundID =
            //                                            from r in rList
            //                                            group r by new { r.Date } into rGroup
            //                                            select rGroup;

            //                                        int incRowExcel = 1;

            //                                        foreach (var rsHeader in QueryByFundID)
            //                                        {
            //                                            incRowExcel = incRowExcel + 2;
            //                                            worksheet.Cells[incRowExcel, 1].Value = "Daily Compliance Report";
            //                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
            //                                            worksheet.Cells["A" + incRowExcel + ":I" + incRowExcel].Merge = true;
            //                                            worksheet.Cells["A" + incRowExcel + ":I" + incRowExcel].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            //                                            worksheet.Cells["A" + incRowExcel + ":I" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            //                                            incRowExcel++;
            //                                            worksheet.Cells[incRowExcel, 1].Value = "Date   :  ";
            //                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
            //                                            worksheet.Cells[incRowExcel, 2].Value = _FundAccountingRpt.ValueDateTo;
            //                                            worksheet.Cells[incRowExcel, 2].Style.Numberformat.Format = "dd-MMM-yyyy";
            //                                            worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
            //                                            worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;

            //                                            incRowExcel = incRowExcel + 2;
            //                                            //Row B = 3
            //                                            //int rowA = incRowExcel;
            //                                            int RowB = incRowExcel;
            //                                            int RowG = incRowExcel + 1;

            //                                            worksheet.Cells[incRowExcel, 1].Value = "ID";
            //                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
            //                                            worksheet.Cells["A" + RowB + ":A" + RowG].Merge = true;
            //                                            worksheet.Cells["A" + RowB + ":A" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            //                                            worksheet.Cells["A" + RowB + ":A" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

            //                                            worksheet.Cells[incRowExcel, 2].Value = "FUND NAME";
            //                                            worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
            //                                            worksheet.Cells["B" + RowB + ":B" + RowG].Merge = true;
            //                                            worksheet.Cells["B" + RowB + ":B" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            //                                            worksheet.Cells["B" + RowB + ":B" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

            //                                            worksheet.Cells[incRowExcel, 3].Value = "DEPOSITO";
            //                                            worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;
            //                                            worksheet.Cells["C" + RowB + ":D" + RowB].Merge = true;
            //                                            worksheet.Cells["C" + RowB + ":D" + RowB].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            //                                            worksheet.Cells["C" + RowB + ":D" + RowB].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

            //                                            worksheet.Cells[incRowExcel, 5].Value = "BOND";
            //                                            worksheet.Cells[incRowExcel, 5].Style.Font.Bold = true;
            //                                            worksheet.Cells["E" + RowB + ":F" + RowB].Merge = true;
            //                                            worksheet.Cells["E" + RowB + ":F" + RowB].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            //                                            worksheet.Cells["E" + RowB + ":F" + RowB].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

            //                                            worksheet.Cells[incRowExcel, 7].Value = "EQUITY";
            //                                            worksheet.Cells[incRowExcel, 7].Style.Font.Bold = true;
            //                                            worksheet.Cells["G" + RowB + ":H" + RowB].Merge = true;
            //                                            worksheet.Cells["G" + RowB + ":H" + RowB].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            //                                            worksheet.Cells["G" + RowB + ":H" + RowB].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

            //                                            worksheet.Cells[incRowExcel, 9].Value = "Total Investment (% NAV)";
            //                                            worksheet.Cells[incRowExcel, 9].Style.Font.Bold = true;
            //                                            worksheet.Cells["I" + RowB + ":I" + RowG].Merge = true;
            //                                            worksheet.Cells["I" + RowB + ":I" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            //                                            worksheet.Cells["I" + RowB + ":I" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

            //                                            incRowExcel++;

            //                                            worksheet.Cells[incRowExcel, 3].Value = "Amount";
            //                                            worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;
            //                                            //worksheet.Cells["C" + RowB + ":C" + RowG].Merge = true;
            //                                            worksheet.Cells["C" + RowB + ":C" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            //                                            worksheet.Cells["C" + RowB + ":C" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

            //                                            worksheet.Cells[incRowExcel, 4].Value = "%NAV";
            //                                            worksheet.Cells[incRowExcel, 4].Style.Font.Bold = true;
            //                                            //worksheet.Cells["D" + RowB + ":D" + RowG].Merge = true;
            //                                            worksheet.Cells["D" + RowB + ":D" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            //                                            worksheet.Cells["D" + RowB + ":D" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;


            //                                            worksheet.Cells[incRowExcel, 5].Value = "Amount";
            //                                            worksheet.Cells[incRowExcel, 5].Style.Font.Bold = true;
            //                                            //worksheet.Cells["E" + RowB + ":E" + RowG].Merge = true;
            //                                            worksheet.Cells["E" + RowB + ":E" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            //                                            worksheet.Cells["E" + RowB + ":E" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

            //                                            worksheet.Cells[incRowExcel, 6].Value = "%NAV";
            //                                            worksheet.Cells[incRowExcel, 6].Style.Font.Bold = true;
            //                                            //worksheet.Cells["F" + RowB + ":F" + RowG].Merge = true;
            //                                            worksheet.Cells["F" + RowB + ":F" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            //                                            worksheet.Cells["F" + RowB + ":F" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

            //                                            worksheet.Cells[incRowExcel, 7].Value = "Amount";
            //                                            worksheet.Cells[incRowExcel, 7].Style.Font.Bold = true;
            //                                            //worksheet.Cells["G" + RowB + ":G" + RowG].Merge = true;
            //                                            worksheet.Cells["G" + RowB + ":G" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            //                                            worksheet.Cells["G" + RowB + ":G" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

            //                                            worksheet.Cells[incRowExcel, 8].Value = "%NAV";
            //                                            worksheet.Cells[incRowExcel, 8].Style.Font.Bold = true;
            //                                            //worksheet.Cells["H" + RowB + ":H" + RowG].Merge = true;
            //                                            worksheet.Cells["H" + RowB + ":H" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            //                                            worksheet.Cells["H" + RowB + ":H" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;


            //                                            incRowExcel++;

            //                                            // Row C = 4
            //                                            int RowC = incRowExcel;

            //                                            //incRowExcel++;
            //                                            //area header

            //                                            int _no = 1;
            //                                            int _startRowDetail = incRowExcel;
            //                                            int _endRowDetail = 0;

            //                                            //var _fundID = "";
            //                                            foreach (var rsDetail in rsHeader)
            //                                            {
            //                                                //Row D = 5
            //                                                //int RowD = incRowExcel;
            //                                                //int RowE = incRowExcel + 1;

            //                                                worksheet.Cells["A" + RowB + ":I" + RowG].Style.Border.Top.Style = ExcelBorderStyle.Medium;
            //                                                worksheet.Cells["A" + RowB + ":I" + RowG].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
            //                                                worksheet.Cells["A" + RowB + ":I" + RowG].Style.Border.Left.Style = ExcelBorderStyle.Medium;
            //                                                worksheet.Cells["A" + RowB + ":I" + RowG].Style.Border.Right.Style = ExcelBorderStyle.Medium;



            //                                                //area detail
            //                                                worksheet.Cells[incRowExcel, 1].Value = rsDetail.FundID;
            //                                                worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
            //                                                worksheet.Cells[incRowExcel, 2].Value = rsDetail.FundName;
            //                                                worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
            //                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.DepositoAmount;
            //                                                worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "#,##0.00";
            //                                                worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
            //                                                worksheet.Cells[incRowExcel, 4].Value = rsDetail.DEPPercentOfNav;
            //                                                worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##0.0000";
            //                                                worksheet.Cells[incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
            //                                                worksheet.Cells[incRowExcel, 5].Value = rsDetail.BondAmount;
            //                                                worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.00";
            //                                                worksheet.Cells[incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
            //                                                worksheet.Cells[incRowExcel, 6].Value = rsDetail.BondPercentOfNav;
            //                                                worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.0000";
            //                                                worksheet.Cells[incRowExcel, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
            //                                                worksheet.Cells[incRowExcel, 7].Value = rsDetail.EquityAmount;
            //                                                worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0.00";
            //                                                worksheet.Cells[incRowExcel, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
            //                                                worksheet.Cells[incRowExcel, 8].Value = rsDetail.EQPercentOfNav;
            //                                                worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0.0000";
            //                                                worksheet.Cells[incRowExcel, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
            //                                                worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0.0000";
            //                                                worksheet.Cells[incRowExcel, 9].Value = rsDetail.TotalPercent;
            //                                                worksheet.Cells[incRowExcel, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

            //                                                _endRowDetail = incRowExcel;
            //                                                _no++;
            //                                                incRowExcel++;
            //                                                //_fundID = rsDetail.Fund;
            //                                            }

            //                                            worksheet.Cells["A" + _endRowDetail + ":I" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
            //                                            worksheet.Cells["A" + _startRowDetail + ":I" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Medium;
            //                                            worksheet.Cells["A" + _startRowDetail + ":I" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Medium;

            //                                        }




            //                                        incRowExcel++;

            //                                        //-----------------------------------
            //                                        using (SqlConnection DbCon1 = new SqlConnection(Tools.conString))
            //                                        {
            //                                            DbCon1.Open();
            //                                            using (SqlCommand cmd1 = DbCon1.CreateCommand())
            //                                            {
            //                                                cmd1.CommandText =

            //                                                @"
            //                                           	Create Table #Exposure
            //            (
            //            InstrumentTypePK int,
            //            InstrumentPK int,
            //            FundPK int,
            //            Amount numeric(22,2),
            //            NAVPercent numeric(18,4),
            //			ExposureType nvarchar(100)
            //            )
            //
            //            Declare @InstrumentTypePK int
            //            Declare @InstrumentPK int
            //            Declare @FundPK int
            //            Declare @Amount numeric(22,2)
            //            Declare @NAVPercent numeric(18,4)
            //            Declare @WarningMaxExposurePercent numeric(18,4)
            //
            //            SET ANSI_WARNINGS OFF
            //
            //            DECLARE A CURSOR FOR 
            //            select C.InstrumentTypePK,A.InstrumentPK,A.FundPK,isnull(sum(A.MarketValue),0),case when isnull(D.AUM,0) = 0 then 0 else isnull(sum((A.MarketValue / 
            //            case when D.AUM = 0 then 1 else isnull(D.AUM,1) End) * 100),0) End NAVPercent   from FundPosition A 
            //            left join Instrument C on A.InstrumentPK = C.InstrumentPK and C.Status = 2
            //            left join CloseNav D on D.Date = A.Date and A.FundPK = D.FundPK and D.status = 2 
            //            left join InstrumentType E on C.InstrumentTypePK = E.InstrumentTypePK and E.Status = 2 
            //            where A.Date = @ValueDate and A.status = 2 
            //			--" + _paramFund + @"
            //            group by C.InstrumentTypePK,A.InstrumentPK,A.FundPK,D.AUM
            //            having case when isnull(D.AUM,0) = 0 then 0 else isnull(sum((A.MarketValue / 
            //            case when D.AUM = 0 then 1 else isnull(D.AUM,1) End) * 100),0) End > 0
            //        	
            //            Open A
            //            Fetch Next From A
            //            Into @InstrumentTypePK,@InstrumentPK,@FundPK,@Amount,@NAVPercent
            //        
            //            While @@FETCH_STATUS = 0
            //            BEGIN
            //            set @WarningMaxExposurePercent = 0
            //
            //            IF (@InstrumentTypePK in (1,4,16))
            //            BEGIN
            //	            IF EXISTS(select * from FundExposure where FundPK = @FundPK and Type = 5 and status = 2 and Parameter = 0)
            //	            BEGIN
            //		            select @WarningMaxExposurePercent = isnull(WarningMaxExposurePercent,0) from FundExposure where FundPK = @FundPK and Type = 5 and status = 2 and Parameter = 0
            //		            IF (@NAVPercent >= @WarningMaxExposurePercent)
            //		            BEGIN
            //			            insert into #Exposure(InstrumentTypePK,InstrumentPK,FundPK,Amount,NAVPercent,ExposureType)
            //			            select @InstrumentTypePK,@InstrumentPK,@FundPK,@Amount,@NAVPercent,'EQUITY ALL'
            //		            END
            //	            END
            //
            //            END
            //            ELSE IF (@InstrumentTypePK = 5)
            //            BEGIN  
            //	            IF EXISTS(select * from FundExposure where FundPK = @FundPK and Type = 10 and status = 2 and Parameter = 0)
            //	            BEGIN
            //		            select @WarningMaxExposurePercent = isnull(WarningMaxExposurePercent,0) from FundExposure where FundPK = @FundPK and Type = 10 and status = 2  and Parameter = 0
            //		            IF (@NAVPercent >= @WarningMaxExposurePercent)
            //		            BEGIN
            //			            insert into #Exposure(InstrumentTypePK,InstrumentPK,FundPK,Amount,NAVPercent,ExposureType)
            //			            select @InstrumentTypePK,@InstrumentPK,@FundPK,@Amount,@NAVPercent,'BOND ALL'
            //		            END
            //	            END
            //            END
            //            ELSE
            //            BEGIN  
            //	            IF EXISTS(select * from FundExposure where FundPK = @FundPK and Type = 13 and status = 2 and Parameter = 0)
            //	            BEGIN
            //		            select @WarningMaxExposurePercent = isnull(WarningMaxExposurePercent,0) from FundExposure where FundPK = @FundPK and Type = 13 and status = 2  and Parameter = 0
            //		            IF (@NAVPercent >= @WarningMaxExposurePercent)
            //		            BEGIN
            //			            insert into #Exposure(InstrumentTypePK,InstrumentPK,FundPK,Amount,NAVPercent,ExposureType)
            //			            select @InstrumentTypePK,@InstrumentPK,@FundPK,@Amount,@NAVPercent,'DEPOSITO ALL'
            //		            END
            //	            END
            //            END
            //            Fetch next From A Into @InstrumentTypePK,@InstrumentPK,@FundPK,@Amount,@NAVPercent
            //            END
            //            Close A
            //            Deallocate A 
            //			
            //
            //			DECLARE A CURSOR FOR 
            //          		select 0,A.IssuerPK,A.FundPK,sum(isnull(A.MarketValue,0)),sum(isnull(A.NAVPercent,0)) from
            //				(
            //				select C.IssuerPK,A.FundPK,isnull(sum(A.MarketValue),0) MarketValue,case when isnull(D.AUM,0) = 0 then 0 else isnull(sum((A.MarketValue / 
            //				case when D.AUM = 0 then 1 else isnull(D.AUM,1) End) * 100),0) End NAVPercent   from FundPosition A 
            //				left join Instrument C on A.InstrumentPK = C.InstrumentPK and C.Status = 2
            //				left join CloseNav D on D.Date = A.Date and A.FundPK = D.FundPK and D.status = 2 
            //				left join InstrumentType E on C.InstrumentTypePK = E.InstrumentTypePK and E.Status = 2 
            //				where A.Date = @ValueDate and A.status = 2 and C.InstrumentTypePK <> 5 and isnull(C.IssuerPK,0) <> 0
            //				--" + _paramFund + @"
            //				group by C.IssuerPK,A.InstrumentPK,A.FundPK,D.AUM
            //				having case when isnull(D.AUM,0) = 0 then 0 else isnull(sum((A.MarketValue / 
            //				case when D.AUM = 0 then 1 else isnull(D.AUM,1) End) * 100),0) End > 0
            //				UNION ALL
            //				select H.IssuerPK,A.FundPK,isnull(sum(A.MarketValue),0) MarketValue,case when isnull(D.AUM,0) = 0 then 0 else isnull(sum((A.MarketValue / 
            //				case when D.AUM = 0 then 1 else isnull(D.AUM,1) End) * 100),0) End NAVPercent   from FundPosition A 
            //				left join Instrument C on A.InstrumentPK = C.InstrumentPK and C.Status = 2
            //				left join CloseNav D on D.Date = A.Date and A.FundPK = D.FundPK and D.status = 2 
            //				left join InstrumentType E on C.InstrumentTypePK = E.InstrumentTypePK and E.Status = 2 
            //				left join BankBranch G on A.BankbranchPK = G.BankBranchPK and G.status in (1,2)
            //				left join Bank H on G.BankPK = H.BankPK and H.status in (1,2)
            //				where A.Date = @ValueDate and A.status = 2 and C.InstrumentTypePK = 5 and isnull(H.IssuerPK,0) <> 0
            //				--" + _paramFund + @"
            //				group by H.IssuerPK,A.InstrumentPK,A.FundPK,D.AUM
            //				having case when isnull(D.AUM,0) = 0 then 0 else isnull(sum((A.MarketValue / 
            //				case when D.AUM = 0 then 1 else isnull(D.AUM,1) End) * 100),0) End > 0
            //				) A 
            //				group by A.IssuerPK,A.FundPK
            //
            //        	
            //            Open A
            //            Fetch Next From A
            //            Into @InstrumentTypePK,@InstrumentPK,@FundPK,@Amount,@NAVPercent
            //        
            //            While @@FETCH_STATUS = 0
            //            BEGIN
            //            set @WarningMaxExposurePercent = 0
            //			IF EXISTS(select * from FundExposure where FundPK = @FundPK and Type = 2 and status = 2 and Parameter = 0)
            //	            BEGIN
            //		            select @WarningMaxExposurePercent = isnull(WarningMaxExposurePercent,0) from FundExposure where FundPK = @FundPK and Type = 2 and status = 2 and Parameter = 0
            //		            IF (@NAVPercent >= @WarningMaxExposurePercent)
            //		            BEGIN
            //			            insert into #Exposure(InstrumentTypePK,InstrumentPK,FundPK,Amount,NAVPercent, ExposureType)
            //			            select @InstrumentTypePK,@InstrumentPK,@FundPK,@Amount,@NAVPercent, 'ISSUER ALL'
            //		            END
            //	            END
            //
            //			  Fetch next From A Into @InstrumentTypePK,@InstrumentPK,@FundPK,@Amount,@NAVPercent
            //            END
            //            Close A
            //            Deallocate A 
            //
            //
            //
            //            select @ValueDate Date,E.Name Type,C.ID InstrumentID,B.ID FundID,B.Name FundName,Amount,NAVPercent   from #Exposure A 
            //            left join Fund B on A.FundPK = B.FundPK and B.Status in (1,2)
            //            left join Instrument C on A.InstrumentPK = C.InstrumentPK and C.Status in (1,2)
            //            left join InstrumentType E on C.InstrumentTypePK = E.InstrumentTypePK and E.Status in (1,2)
            //			where A.ExposureType in
            //			(
            //			'DEPOSITO ALL','EQUITY ALL','BOND ALL'
            //			)
            //
            //			UNION ALL
            //
            //			 select @ValueDate Date,'ISSUER ALL' Type,C.Name InstrumentID,B.ID FundID,B.Name FundName,Amount,NAVPercent   from #Exposure A 
            //            left join Fund B on A.FundPK = B.FundPK and B.Status in (1,2)
            //            left join Issuer C on A.InstrumentPK = C.IssuerPK and C.Status in (1,2) 
            //			where A.ExposureType in
            //			(
            //			'ISSUER ALL'
            //			)
            //		
            //			
            //
            //                                                 ";

            //                                                cmd1.CommandTimeout = 0;
            //                                                cmd1.Parameters.AddWithValue("@valuedate", _FundAccountingRpt.ValueDateTo);
            //                                                //cmd1.Parameters.AddWithValue("@FundFrom", _FundAccountingRpt.FundFrom);



            //                                                cmd1.ExecuteNonQuery();


            //                                                using (SqlDataReader dr1 = cmd1.ExecuteReader())
            //                                                {
            //                                                    //if (!dr1.HasRows)
            //                                                    //{
            //                                                    //    return false;
            //                                                    //}
            //                                                    //else
            //                                                    //{


            //                                                        // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
            //                                                        using (ExcelPackage package1 = new ExcelPackage(excelFile))
            //                                                        {

            //                                                            //ATUR DATA GROUPINGNYA DULU
            //                                                            List<DailyComplianceReport> rList1 = new List<DailyComplianceReport>();
            //                                                            while (dr1.Read())
            //                                                            {
            //                                                                DailyComplianceReport rSingle1 = new DailyComplianceReport();
            //                                                                rSingle1.Date = Convert.ToString(dr1["Date"]);
            //                                                                rSingle1.FundID = Convert.ToString(dr1["FundID"]);
            //                                                                rSingle1.FundName = Convert.ToString(dr1["FundName"]);
            //                                                                rSingle1.Amount = Convert.ToDecimal(dr1["Amount"]);
            //                                                                rSingle1.NAVPercent = Convert.ToDecimal(dr1["NAVPercent"]);
            //                                                                rSingle1.Type = Convert.ToString(dr1["Type"]);
            //                                                                rSingle1.InstrumentID = Convert.ToString(dr1["InstrumentID"]);
            //                                                                rList1.Add(rSingle1);

            //                                                            }


            //                                                            var QueryByFundID1 =
            //                                                                from r1 in rList1
            //                                                                group r1 by new { r1.Date } into rGroup1
            //                                                                select rGroup1;

            //                                                            incRowExcel = incRowExcel + 2;
            //                                                            int _endRowDetailZ = 0;


            //                                                            foreach (var rsHeader1 in QueryByFundID1)
            //                                                            {
            //                                                                //worksheet.Cells[incRowExcel, 1].Value = "Total " + rsHeader1.Key.FundID + " : ";
            //                                                                worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
            //                                                                worksheet.Cells["A" + incRowExcel + ":B" + incRowExcel].Merge = true;
            //                                                                worksheet.Cells["A" + incRowExcel + ":B" + incRowExcel].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            //                                                                worksheet.Cells["A" + incRowExcel + ":B" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

            //                                                                incRowExcel = incRowExcel + 2;
            //                                                                //Row B = 3
            //                                                                int RowBZ = incRowExcel;
            //                                                                int RowGZ = incRowExcel + 1;

            //                                                                worksheet.Cells[incRowExcel, 1].Value = "ID";
            //                                                                worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
            //                                                                worksheet.Cells["A" + RowBZ + ":A" + RowGZ].Merge = true;
            //                                                                worksheet.Cells["A" + RowBZ + ":A" + RowGZ].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            //                                                                worksheet.Cells["A" + RowBZ + ":A" + RowGZ].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

            //                                                                worksheet.Cells[incRowExcel, 2].Value = "Fund";
            //                                                                worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
            //                                                                worksheet.Cells["B" + RowBZ + ":B" + RowGZ].Merge = true;
            //                                                                worksheet.Cells["B" + RowBZ + ":B" + RowGZ].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            //                                                                worksheet.Cells["B" + RowBZ + ":B" + RowGZ].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

            //                                                                worksheet.Cells[incRowExcel, 3].Value = "Exposure Monitoring";
            //                                                                worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;
            //                                                                worksheet.Cells["C" + RowBZ + ":E" + RowBZ].Merge = true;
            //                                                                worksheet.Cells["C" + RowBZ + ":E" + RowBZ].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            //                                                                worksheet.Cells["C" + RowBZ + ":E" + RowBZ].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

            //                                                                worksheet.Cells[incRowExcel, 6].Value = "%NAV";
            //                                                                worksheet.Cells[incRowExcel, 6].Style.Font.Bold = true;
            //                                                                worksheet.Cells["F" + RowBZ + ":F" + RowGZ].Merge = true;
            //                                                                worksheet.Cells["F" + RowBZ + ":F" + RowGZ].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            //                                                                worksheet.Cells["F" + RowBZ + ":F" + RowGZ].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

            //                                                                incRowExcel++;


            //                                                                worksheet.Cells[incRowExcel, 3].Value = "Type";
            //                                                                worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;
            //                                                                //worksheet.Cells["C" + RowBZ + ":C" + RowGZ].Merge = true;
            //                                                                worksheet.Cells["C" + RowBZ + ":C" + RowGZ].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            //                                                                worksheet.Cells["C" + RowBZ + ":C" + RowGZ].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

            //                                                                worksheet.Cells[incRowExcel, 4].Value = "Instrument";
            //                                                                worksheet.Cells[incRowExcel, 4].Style.Font.Bold = true;
            //                                                                //worksheet.Cells["D" + RowBZ + ":D" + RowGZ].Merge = true;
            //                                                                worksheet.Cells["D" + RowBZ + ":D" + RowGZ].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            //                                                                worksheet.Cells["D" + RowBZ + ":D" + RowGZ].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

            //                                                                worksheet.Cells[incRowExcel, 5].Value = "Amount";
            //                                                                worksheet.Cells[incRowExcel, 5].Style.Font.Bold = true;
            //                                                                //worksheet.Cells["E" + RowBZ + ":E" + RowGZ].Merge = true;
            //                                                                worksheet.Cells["E" + RowBZ + ":E" + RowGZ].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            //                                                                worksheet.Cells["E" + RowBZ + ":E" + RowGZ].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;


            //                                                                incRowExcel++;

            //                                                                // Row C = 4
            //                                                                int RowCZ = incRowExcel;

            //                                                                //incRowExcel++;
            //                                                                //area header

            //                                                                int _noZ = 1;
            //                                                                int _startRowDetailZ = incRowExcel;
            //                                                                foreach (var rsDetail1 in rsHeader1)
            //                                                                {
            //                                                                    //Row D = 5
            //                                                                    int RowDZ = incRowExcel;
            //                                                                    int RowEZ = incRowExcel + 1;


            //                                                                    //ThickBox Border

            //                                                                    worksheet.Cells["A" + RowBZ + ":F" + RowGZ].Style.Border.Top.Style = ExcelBorderStyle.Medium;
            //                                                                    worksheet.Cells["A" + RowBZ + ":F" + RowGZ].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
            //                                                                    worksheet.Cells["A" + RowBZ + ":F" + RowGZ].Style.Border.Left.Style = ExcelBorderStyle.Medium;
            //                                                                    worksheet.Cells["A" + RowBZ + ":F" + RowGZ].Style.Border.Right.Style = ExcelBorderStyle.Medium;


            //                                                                    //area detail
            //                                                                    worksheet.Cells[incRowExcel, 1].Value = rsDetail1.FundID;
            //                                                                    worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
            //                                                                    worksheet.Cells[incRowExcel, 2].Value = rsDetail1.FundName;
            //                                                                    worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
            //                                                                    worksheet.Cells[incRowExcel, 3].Value = rsDetail1.Type;
            //                                                                    worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
            //                                                                    worksheet.Cells[incRowExcel, 4].Value = rsDetail1.InstrumentID;
            //                                                                    worksheet.Cells[incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
            //                                                                    worksheet.Cells[incRowExcel, 5].Value = rsDetail1.Amount;
            //                                                                    worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.00";
            //                                                                    worksheet.Cells[incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
            //                                                                    worksheet.Cells[incRowExcel, 6].Value = rsDetail1.NAVPercent;
            //                                                                    worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.0000";
            //                                                                    worksheet.Cells[incRowExcel, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;



            //                                                                    _endRowDetailZ = incRowExcel;
            //                                                                    _noZ++;
            //                                                                    incRowExcel++;

            //                                                                }

            //                                                                worksheet.Cells["A" + _endRowDetailZ + ":F" + _endRowDetailZ].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
            //                                                                worksheet.Cells["A" + _startRowDetailZ + ":F" + _endRowDetailZ].Style.Border.Left.Style = ExcelBorderStyle.Medium;
            //                                                                worksheet.Cells["A" + _startRowDetailZ + ":F" + _endRowDetailZ].Style.Border.Right.Style = ExcelBorderStyle.Medium;

            //                                                                //incRowExcel++;
            //                                                            }
            //                                                            //disini
            //                                                            incRowExcel++;

            //                                                            //-----------------------------------
            //                                                            using (SqlConnection DbCon2 = new SqlConnection(Tools.conString))
            //                                                            {
            //                                                                DbCon2.Open();
            //                                                                using (SqlCommand cmd2 = DbCon2.CreateCommand())
            //                                                                {
            //                                                                    cmd2.CommandText =

            //                                                                    @"
            //                                                                    select @ValueDate Date,B.ID FundID,B.Name FundName,isnull(A.AUM,0) TotalAUM from CloseNAV A
            //                                                                    left join Fund B on A.FundPK = B.FundPK and B.status in (1,2)
            //                                                                    where A.date = @ValueDate and A.status = 2 " + _paramFund;

            //                                                                    cmd2.CommandTimeout = 0;
            //                                                                    cmd2.Parameters.AddWithValue("@ValueDate", _FundAccountingRpt.ValueDateTo);

            //                                                                    cmd2.ExecuteNonQuery();


            //                                                                    using (SqlDataReader dr2 = cmd2.ExecuteReader())
            //                                                                    {
            //                                                                        //if (!dr2.HasRows)
            //                                                                        //{
            //                                                                        //    return false;
            //                                                                        //}
            //                                                                        //else
            //                                                                        //{


            //                                                                            // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
            //                                                                            using (ExcelPackage package2 = new ExcelPackage(excelFile))
            //                                                                            {

            //                                                                                //ATUR DATA GROUPINGNYA DULU
            //                                                                                List<DailyComplianceReport> rList2 = new List<DailyComplianceReport>();
            //                                                                                while (dr2.Read())
            //                                                                                {
            //                                                                                    DailyComplianceReport rSingle2 = new DailyComplianceReport();
            //                                                                                    rSingle2.Date = Convert.ToString(dr2["Date"]);
            //                                                                                    rSingle2.FundID = Convert.ToString(dr2["FundID"]);
            //                                                                                    rSingle2.FundName = Convert.ToString(dr2["FundName"]);
            //                                                                                    rSingle2.TotalAUM = Convert.ToDecimal(dr2["TotalAUM"]);
            //                                                                                    rList2.Add(rSingle2);

            //                                                                                }


            //                                                                                var QueryByFundID2 =
            //                                                                                    from r2 in rList2
            //                                                                                    group r2 by new { r2.Date } into rGroup2
            //                                                                                    select rGroup2;

            //                                                                                incRowExcel = incRowExcel + 2;
            //                                                                                int _endRowDetailZZ = 0;


            //                                                                                foreach (var rsHeader2 in QueryByFundID2)
            //                                                                                {
            //                                                                                    //worksheet.Cells[incRowExcel, 1].Value = "Total " + rsHeader1.Key.FundID + " : ";
            //                                                                                    worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
            //                                                                                    worksheet.Cells["A" + incRowExcel + ":C" + incRowExcel].Merge = true;
            //                                                                                    worksheet.Cells["A" + incRowExcel + ":C" + incRowExcel].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            //                                                                                    worksheet.Cells["A" + incRowExcel + ":C" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

            //                                                                                    incRowExcel = incRowExcel + 2;
            //                                                                                    //Row B = 3
            //                                                                                    int RowBZ = incRowExcel;
            //                                                                                    int RowGZ = incRowExcel + 1;

            //                                                                                    worksheet.Cells[incRowExcel, 1].Value = "AUM monitoring";
            //                                                                                    worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
            //                                                                                    worksheet.Cells["A" + RowBZ + ":C" + RowBZ].Merge = true;
            //                                                                                    worksheet.Cells["A" + RowBZ + ":C" + RowBZ].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            //                                                                                    worksheet.Cells["A" + RowBZ + ":C" + RowBZ].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

            //                                                                                    incRowExcel++;

            //                                                                                    worksheet.Cells[incRowExcel, 1].Value = "ID";
            //                                                                                    worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
            //                                                                                    //worksheet.Cells["A" + RowBZ + ":A" + RowGZ].Merge = true;
            //                                                                                    worksheet.Cells["A" + RowBZ + ":A" + RowGZ].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            //                                                                                    worksheet.Cells["A" + RowBZ + ":A" + RowGZ].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

            //                                                                                    worksheet.Cells[incRowExcel, 2].Value = "Fund";
            //                                                                                    worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
            //                                                                                    //worksheet.Cells["B" + RowBZ + ":B" + RowGZ].Merge = true;
            //                                                                                    worksheet.Cells["B" + RowBZ + ":B" + RowGZ].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            //                                                                                    worksheet.Cells["B" + RowBZ + ":B" + RowGZ].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

            //                                                                                    worksheet.Cells[incRowExcel, 3].Value = "Total AUM";
            //                                                                                    worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;
            //                                                                                    //worksheet.Cells["C" + RowBZ + ":C" + RowGZ].Merge = true;
            //                                                                                    worksheet.Cells["C" + RowBZ + ":C" + RowGZ].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            //                                                                                    worksheet.Cells["C" + RowBZ + ":C" + RowGZ].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;






            //                                                                                    incRowExcel++;

            //                                                                                    // Row C = 4
            //                                                                                    int RowCZ = incRowExcel;

            //                                                                                    //incRowExcel++;
            //                                                                                    //area header

            //                                                                                    int _noZ = 1;
            //                                                                                    int _startRowDetailZ = incRowExcel;
            //                                                                                    foreach (var rsDetail2 in rsHeader2)
            //                                                                                    {
            //                                                                                        //Row D = 5
            //                                                                                        int RowDZ = incRowExcel;
            //                                                                                        int RowEZ = incRowExcel + 1;


            //                                                                                        //ThickBox Border

            //                                                                                        //if (rsDetail1.Type == "Subscription")
            //                                                                                        //{
            //                                                                                        worksheet.Cells["A" + RowBZ + ":C" + RowGZ].Style.Border.Top.Style = ExcelBorderStyle.Medium;
            //                                                                                        worksheet.Cells["A" + RowBZ + ":C" + RowGZ].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
            //                                                                                        worksheet.Cells["A" + RowBZ + ":C" + RowGZ].Style.Border.Left.Style = ExcelBorderStyle.Medium;
            //                                                                                        worksheet.Cells["A" + RowBZ + ":C" + RowGZ].Style.Border.Right.Style = ExcelBorderStyle.Medium;

            //                                                                                        //area detail
            //                                                                                        worksheet.Cells[incRowExcel, 1].Value = rsDetail2.FundID;
            //                                                                                        worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
            //                                                                                        worksheet.Cells[incRowExcel, 2].Value = rsDetail2.FundName;
            //                                                                                        worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
            //                                                                                        worksheet.Cells[incRowExcel, 3].Value = rsDetail2.TotalAUM;
            //                                                                                        worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "#,##0.00";
            //                                                                                        worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;




            //                                                                                        _endRowDetailZZ = incRowExcel;
            //                                                                                        _noZ++;
            //                                                                                        incRowExcel++;

            //                                                                                    }

            //                                                                                    worksheet.Cells["A" + _endRowDetailZZ + ":C" + _endRowDetailZZ].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
            //                                                                                    worksheet.Cells["A" + _startRowDetailZ + ":C" + _endRowDetailZZ].Style.Border.Left.Style = ExcelBorderStyle.Medium;
            //                                                                                    worksheet.Cells["A" + _startRowDetailZ + ":C" + _endRowDetailZZ].Style.Border.Right.Style = ExcelBorderStyle.Medium;
            //                                                                                    incRowExcel++;
            //                                                                                }





            //                                                                                //string _rangeA1 = "A:M" + incRowExcel;
            //                                                                                //using (ExcelRange r = worksheet.Cells[_rangeA1]) // KALO  KOLOM 1 SAMPE 9 A-I
            //                                                                                //{
            //                                                                                //    r.Style.Font.Size = 22;
            //                                                                                //}
            //                                                                            //}

            //                                                                        }
            //                                                                    }
            //                                                                }
            //                                                            }



            //                                                            //string _rangeA1 = "A:M" + incRowExcel;
            //                                                            //using (ExcelRange r = worksheet.Cells[_rangeA1]) // KALO  KOLOM 1 SAMPE 9 A-I
            //                                                            //{
            //                                                            //    r.Style.Font.Size = 22;
            //                                                            //}
            //                                                        }

            //                                                    //}
            //                                                }
            //                                            }
            //                                        }



            //                                        int _lastRow = incRowExcel;

            //                                        //incRowExcel = incRowExcel + 7;
            //                                        //worksheet.Cells[incRowExcel, 1].Value = "Disclaimer   : ";
            //                                        //incRowExcel++;
            //                                        //worksheet.Row(incRowExcel).Height = 50;
            //                                        //worksheet.Cells[incRowExcel, 1].Value = Tools.DefaultDisclaimerReportFooterLeftText();
            //                                        //worksheet.Cells["A" + incRowExcel + ":I" + incRowExcel].Style.WrapText = true;
            //                                        //worksheet.Cells["A" + incRowExcel + ":I" + incRowExcel].Merge = true;
            //                                        //worksheet.Cells["A" + incRowExcel + ":I" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
            //                                        //worksheet.Cells[incRowExcel, 1].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
            //                                        incRowExcel++;
            //                                        worksheet.Row(incRowExcel).PageBreak = _FundAccountingRpt.PageBreak;

            //                                        string _rangeA = "A:I" + incRowExcel;
            //                                        using (ExcelRange r = worksheet.Cells[_rangeA]) // KALO  KOLOM 1 SAMPE 9 A-I
            //                                        {
            //                                            r.Style.Font.Size = 12;
            //                                        }

            //                                        //worksheet.DeleteRow(_lastRow);

            //                                        worksheet.PrinterSettings.FitToPage = true;
            //                                        worksheet.PrinterSettings.FitToWidth = 1;
            //                                        worksheet.PrinterSettings.FitToHeight = 0;
            //                                        worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel - 1, 9];
            //                                        worksheet.Column(1).Width = 15;
            //                                        worksheet.Column(2).Width = 50;
            //                                        worksheet.Column(3).Width = 30;
            //                                        worksheet.Column(4).Width = 20;
            //                                        worksheet.Column(5).Width = 30;
            //                                        worksheet.Column(6).Width = 20;
            //                                        worksheet.Column(7).Width = 30;
            //                                        worksheet.Column(8).Width = 20;
            //                                        worksheet.Column(9).Width = 25;
            //                                        //worksheet.Column(10).Width = 20;
            //                                        //worksheet.Column(11).Width = 20;
            //                                        //worksheet.Column(12).Width = 20;
            //                                        //worksheet.Column(13).Width = 15;
            //                                        //worksheet.Column(14).Width = 15;
            //                                        //worksheet.Column(15).Width = 30;



            //                                        // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
            //                                        //worksheet.Cells["A3:I14"].AutoFitColumns(); // UNTUK AUTO FIT UKURAN TULISAN DENGAN KOLOM, KEBETULAN INI BUAT HEADER AJA DIPAKENYA
            //                                        //worksheetApproved.Cells["A3:E3"].AutoFitColumns();
            //                                        worksheet.PrinterSettings.Orientation = eOrientation.Landscape; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
            //                                        worksheet.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN

            //                                        worksheet.HeaderFooter.OddHeader.CenteredText = "\n \n \n \n \n \n \n &18&B Daily Total Transaction Report ";



            //                                        // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
            //                                        worksheet.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
            //                                        worksheet.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
            //                                        worksheet.View.PageLayoutView = Tools.DefaultReportPageLayoutView();
            //                                        worksheet.View.ShowGridLines = Tools.DefaultReportShowGridLines();
            //                                        Image img = Image.FromFile(Tools.ReportImage);
            //                                        worksheet.HeaderFooter.OddHeader.InsertPicture(img, PictureAlignment.Left);
            //                                        //worksheet.HeaderFooter.OddHeader.CenteredText = Tools.DefaultReportHeaderCenterText();
            //                                        //worksheet.HeaderFooter.OddHeader.LeftAlignedText = Tools.DefaultReportHeaderLeftText();

            //                                        worksheet.HeaderFooter.OddFooter.RightAlignedText =
            //                                        string.Format("Page {0} of {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);

            //                                        ////worksheet.HeaderFooter.OddFooter.CenteredText = Tools.DefaultReportFooterCenterText();
            //                                        worksheet.HeaderFooter.OddFooter.LeftAlignedText = Tools.DefaultReportFooterLeftText();



            //                                        package.Save();
            //                                        if (_FundAccountingRpt.DownloadMode == "PDF")
            //                                        {
            //                                            Tools.ExportFromExcelToPDF(filePath, pdfPath);
            //                                        }
            //                                        return true;
            //                                    }
            //                                }
            //                            }

            //                        }

            //                    }
            //                }
            //                catch (Exception err)
            //                {
            //                    return false;
            //                    throw err;
            //                }

            //            }
            //            #endregion

            else
            {
                return false;
            }
        }

        public string Retrieve_ManagementFee(DateTime _date, string _usersID)
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

                        declare @FundPK int
                        declare @InstrumentPK int 
    
                        Declare @ARManagementFee int 
                        Declare @TaxARManagementFee int 
                        Declare @ManagementFeeExpense int 


                        declare @ManagementFeeAmount numeric(22,6)

                        declare @ARManagementFeeAmount numeric(22,6)
                        declare @TaxARManagementFeeAmount numeric(22,6)
                        declare @ManagementFeeExpenseAmount numeric(22,6)

                        create table #ReferenceTemp
                        (Reference nvarchar(50))

                        Declare @JourHeader int  
                        set @JourHeader = 0    
                        Declare @JournalPK int 
                        Declare @PeriodPK int 
                        Declare @Reference nvarchar(50)    
                        Select @PeriodPK = PeriodPK From Period Where DateFrom <= @Date and Dateto >= @Date and Status = 2  
                        Select @JournalPK = isnull(Max(JournalPK),0) from Journal   

                        insert into #ReferenceTemp (Reference)
                        exec getJournalReference @Date,'ADJ',@Reference out  
 
                        set @JournalPK = @JournalPK + 1   
                        INSERT INTO [Journal]  
                        ([JournalPK],[HistoryPK],[Status],[Notes],[PeriodPK],[ValueDate]  
                        ,[TrxNo],[TrxName],[Reference],[Type],[Description],[Posted],[PostedBy]  
                        ,[PostedTime],[EntryUsersID],[EntryTime]  
                        ,[ApprovedUsersID],[ApprovedTime],[DBUserID],LastUpdate)     
                        SELECT @JournalPK,1,2,'Management Fee',@PeriodPK, @Date
                        ,0,'',@Reference,1,'Management Fee',1,@UsersID
                        ,@TimeNow,@UsersID,@TimeNow,@UsersID,@TimeNow,@UsersID,@TimeNow 


                        Declare @AutoNo int 
                        set @AutoNo = 0   
                        DECLARE A CURSOR FOR 
                        select FundPK,ManagementFeeAmount from FundDailyFee where Date = @Date
                        OPEN A 
                        FETCH NEXT FROM A 
                        INTO @FundPK,@ManagementFeeAmount   
                        WHILE @@FETCH_STATUS = 0  
                        BEGIN 


                        select @InstrumentPK = InstrumentPK From Instrument A 
                        left join Fund B on A.ID = B.ID and B.Status = 2 where A.Status = 2 and B.FundPK = @FundPK         
 
                        Select @ARManagementFee = ARManagementFee,@TaxARManagementFee =  TaxARManagementFee,
                        @ManagementFeeExpense = ManagementFeeExpense 
                        From AccountingSetup Where Status = 2


                        IF (@ManagementFeeAmount) > 0
                        BEGIN

                        set @AutoNo = @AutoNo + 1   


                        set @ManagementFeeExpenseAmount = cast(@ManagementFeeAmount as numeric(22,4))
                        set @TaxARManagementFeeAmount = cast(0.02 * @ManagementFeeAmount as numeric(22,4))
                        set @ARManagementFeeAmount = @ManagementFeeAmount - @TaxARManagementFeeAmount


                        INSERT INTO [JournalDetail]  
                        ([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[OfficePK]  
                        ,[DepartmentPK],[AgentPK],[CounterpartPK],[InstrumentPK],[ConsigneePK],[DetailDescription]  
                        ,[DocRef],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit]  
                        ,[BaseCredit],[LastUsersID],LastUpdate) 
                        Select @JournalPK,@AutoNo,1,2,@ARManagementFee,1,0,0,0,0,@InstrumentPK,0,'','','D',abs(@ARManagementFeeAmount), abs(@ARManagementFeeAmount),0,1,abs(@ARManagementFeeAmount),0,@UsersID,@TimeNow 
               
			            
                        set @AutoNo = @AutoNo + 1   


                        INSERT INTO [JournalDetail]  
                        ([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[OfficePK]  
                        ,[DepartmentPK],[AgentPK],[CounterpartPK],[InstrumentPK],[ConsigneePK],[DetailDescription]  
                        ,[DocRef],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit]  
                        ,[BaseCredit],[LastUsersID],LastUpdate) 
                        Select @JournalPK,@AutoNo,1,2,@TaxARManagementFee,1,0,0,0,0,@InstrumentPK,0,'','','D',abs(@TaxARManagementFeeAmount), abs(@TaxARManagementFeeAmount),0,1,abs(@TaxARManagementFeeAmount),0,@UsersID,@TimeNow 
               
			            
                        set @AutoNo = @AutoNo + 1   

                        INSERT INTO [JournalDetail]  
                        ([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[OfficePK]  
                        ,[DepartmentPK],[AgentPK],[CounterpartPK],[InstrumentPK],[ConsigneePK],[DetailDescription]  
                        ,[DocRef],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit]  
                        ,[BaseCredit],[LastUsersID],LastUpdate) 
                        Select @JournalPK,@AutoNo,1,2,@ManagementFeeExpense,1,0,0,0,0,@InstrumentPK,0,'','','C',abs(@ManagementFeeExpenseAmount), 0,abs(@ManagementFeeExpenseAmount),1,0,abs(@ManagementFeeExpenseAmount),@UsersID,@TimeNow 
                        END

                        ELSE

                        BEGIN
                        set @AutoNo = @AutoNo + 1   


                        set @ManagementFeeExpenseAmount = cast(@ManagementFeeAmount as numeric(22,4))
                        set @TaxARManagementFeeAmount = cast(0.02 * @ManagementFeeAmount as numeric(22,4))
                        set @ARManagementFeeAmount = @ManagementFeeAmount - @TaxARManagementFeeAmount


                        INSERT INTO [JournalDetail]  
                        ([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[OfficePK]  
                        ,[DepartmentPK],[AgentPK],[CounterpartPK],[InstrumentPK],[ConsigneePK],[DetailDescription]  
                        ,[DocRef],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit]  
                        ,[BaseCredit],[LastUsersID],LastUpdate) 
                        Select @JournalPK,@AutoNo,1,2,@ARManagementFee,1,0,0,0,0,@InstrumentPK,0,'','','C',abs(@ARManagementFeeAmount), 0,abs(@ARManagementFeeAmount),1,0,abs(@ARManagementFeeAmount),@UsersID,@TimeNow 
               
			            
                        set @AutoNo = @AutoNo + 1   


                        INSERT INTO [JournalDetail]  
                        ([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[OfficePK]  
                        ,[DepartmentPK],[AgentPK],[CounterpartPK],[InstrumentPK],[ConsigneePK],[DetailDescription]  
                        ,[DocRef],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit]  
                        ,[BaseCredit],[LastUsersID],LastUpdate) 
                        Select @JournalPK,@AutoNo,1,2,@TaxARManagementFee,1,0,0,0,0,@InstrumentPK,0,'','','C',abs(@TaxARManagementFeeAmount) ,0,abs(@TaxARManagementFeeAmount),1,0,abs(@TaxARManagementFeeAmount),@UsersID,@TimeNow 
               
			            
                        set @AutoNo = @AutoNo + 1   

                        INSERT INTO [JournalDetail]  
                        ([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[OfficePK]  
                        ,[DepartmentPK],[AgentPK],[CounterpartPK],[InstrumentPK],[ConsigneePK],[DetailDescription]  
                        ,[DocRef],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit]  
                        ,[BaseCredit],[LastUsersID],LastUpdate) 
                        Select @JournalPK,@AutoNo,1,2,@ManagementFeeExpense,1,0,0,0,0,@InstrumentPK,0,'','','D',abs(@ManagementFeeExpenseAmount),abs(@ManagementFeeExpenseAmount), 0,1,abs(@ManagementFeeExpenseAmount), 0,@UsersID,@TimeNow 
                        END
                        
                        FETCH NEXT FROM A 
                        INTO @FundPK,@ManagementFeeAmount 
                        END 
                        CLOSE A  
                        DEALLOCATE A

                        DECLARE @combinedString NVARCHAR(500)
                        SELECT @combinedString = COALESCE(@combinedString + ', ', '') + Reference
                        FROM #ReferenceTemp
                        IF (@combinedString is null)
                        BEGIN
                            SELECT 'No Data Retrieve, Please Check Fund Daily Fee' as Result
                        END
                        ELSE
                        BEGIN
                        SELECT 'Retrieve Management Fee Success ! Reference is : ' + @combinedString as Result
                        END
                        
                        ";

                        cmd.Parameters.AddWithValue("@Date", _date);
                        cmd.Parameters.AddWithValue("@UsersID", _usersID);
                        cmd.Parameters.AddWithValue("@TimeNow", _dateTimeNow);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return Convert.ToString(dr["Result"]);

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

        public Boolean Payment_Voucher(string _userID, Cashier _cashier)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText =
                         @" Select lower(C.EntryUsersID) CheckedBy,lower(C.EntryUsersID) ApprovedBy,       
                            reference , valuedate,A.ID AccountID, A.Name AccountName, C.Description,     
                            DebitCredit,(Case When DebitCredit ='D' then BaseDebit else 0 end) Debit,(Case When DebitCredit ='D' then 0 else BaseCredit end) Credit,F.ID DepartmentID       
                            from Cashier C       
                            left join Account A on C.Debitaccountpk =A.Accountpk and A.status = 2       
                            left join Office E on C.OfficePK = E.OfficePK and E.status = 2       
                            left join Department F on C.DepartmentPK = F.DepartmentPK and F.status = 2       
                            left join Agent G on C.AgentPK = G.AgentPK and G.status = 2       
                            left join Consignee H on C.ConsigneePK = H.ConsigneePK and H.status = 2       
                            left join Instrument I on C.InstrumentPK = I.InstrumentPK and I.status = 2       
                            Where C.Reference = @Reference  and C.Status in (1,2)         
                            UNION ALL       
                            Select lower(C.EntryUsersID) CheckedBy,lower(C.EntryUsersID) ApprovedBy,      
                            reference , valuedate,A.ID AccountID, A.Name AccountName, '' Description,         
                            'C', 0 Debit,SUM(Case When DebitCredit ='D' then BaseDebit else BaseCredit * -1 end)Credit,'' DepartmentID           
                            from Cashier C       
                            left join Account A on C.Creditaccountpk =A.Accountpk and A.status = 2       
                            left join Office E on C.OfficePK = E.OfficePK and E.status = 2       
                            left join Department F on C.DepartmentPK = F.DepartmentPK and F.status = 2       
                            left join Agent G on C.AgentPK = G.AgentPK and G.status = 2       
                            left join Consignee H on C.ConsigneePK = H.ConsigneePK and H.status = 2       
                            left join Instrument I on C.InstrumentPK = I.InstrumentPK and I.status = 2       
                            Where C.Reference = @Reference and C.Status in (1,2)    
                            group by C.EntryUsersID,Reference , Valuedate,A.ID, A.Name    
                            Order By DebitCredit Desc ";

                        cmd.Parameters.AddWithValue("@Reference", _cashier.Reference);
                        //cmd.Parameters.AddWithValue("@Status", _cashier.Status);

                        using (SqlDataReader dr0 = cmd.ExecuteReader())
                        {
                            if (!dr0.HasRows)
                            {
                                return false;
                            }
                            else
                            {
                                string filePath = Tools.ReportsPath + "PaymentVoucher" + "_" + _userID + ".xlsx";
                                string pdfPath = Tools.ReportsPath + "PaymentVoucher" + "_" + _userID + ".pdf";
                                FileInfo excelFile = new FileInfo(filePath);
                                if (excelFile.Exists)
                                {
                                    excelFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                    excelFile = new FileInfo(filePath);
                                }

                                // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                using (ExcelPackage package = new ExcelPackage(excelFile))
                                {
                                    package.Workbook.Properties.Title = "FinanceReport";
                                    package.Workbook.Properties.Author = Tools.DefaultReportAuthor();
                                    package.Workbook.Properties.Comments = Tools.DefaultReportComments();
                                    package.Workbook.Properties.Company = Tools.DefaultReportCompany();
                                    package.Workbook.Properties.SetCustomPropertyValue("Checked by", _userID);
                                    package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", Tools.DefaultReportAssemblyName());

                                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Payment Voucher");


                                    //ATUR DATA GROUPINGNYA DULU
                                    List<CashierVoucher> rList = new List<CashierVoucher>();
                                    while (dr0.Read())
                                    {
                                        CashierVoucher rSingle = new CashierVoucher();
                                        rSingle.ValueDate = Convert.ToDateTime(dr0["ValueDate"]);
                                        rSingle.Reference = Convert.ToString(dr0["Reference"]);
                                        rSingle.Description = Convert.ToString(dr0["Description"]);
                                        rSingle.AccountID = Convert.ToString(dr0["AccountID"]);
                                        rSingle.AccountName = Convert.ToString(dr0["AccountName"]);
                                        rSingle.DebitCredit = Convert.ToString(dr0["DebitCredit"]);
                                        rSingle.Debit = Convert.ToDecimal(dr0["Debit"]);
                                        rSingle.Credit = Convert.ToDecimal(dr0["Credit"]);
                                        rSingle.DepartmentID = Convert.ToString(dr0["DepartmentID"]);
                                        rSingle.CheckedBy = Convert.ToString(dr0["CheckedBy"]);
                                        rSingle.ApprovedBy = Convert.ToString(dr0["ApprovedBy"]);
                                        rList.Add(rSingle);

                                    }

                                    var GroupByReference =
                                                 from r in rList
                                                 orderby r.DebitCredit descending
                                                 group r by new { r.Reference, r.ValueDate, r.CheckedBy, } into rGroup
                                                 select rGroup;

                                    int incRowExcel = 0;

                                    foreach (var rsHeader in GroupByReference)
                                    {
                                        incRowExcel = incRowExcel + 2;
                                        worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 2].Value = "REFERENCE : ";
                                        worksheet.Cells[incRowExcel, 3].Value = rsHeader.Key.Reference;
                                        worksheet.Cells[incRowExcel, 5].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 5].Value = "DATE : ";
                                        //worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "dd-MM-yyyy";
                                        worksheet.Cells[incRowExcel, 6].Value = Convert.ToDateTime(rsHeader.Key.ValueDate).ToShortDateString();
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                        //worksheet.Cells[incRowExcel, 1].Value = "BANK";
                                        //worksheet.Cells["B" + incRowExcel + ":C" + incRowExcel].Merge = true;
                                        //worksheet.Cells[incRowExcel, 2].Value = rsHeader.Key.BankID + "-" + rsHeader.Key.BankName;
                                        incRowExcel++;

                                        //Row B = 3
                                        int RowB = incRowExcel;
                                        int RowG = incRowExcel + 1;

                                        worksheet.Cells[incRowExcel, 1].Value = "NO";
                                        worksheet.Cells[incRowExcel, 2].Value = "ACCOUNT ID";
                                        worksheet.Cells[incRowExcel, 3].Value = "NAME";
                                        worksheet.Cells[incRowExcel, 4].Value = "DESCRIPTION";
                                        worksheet.Cells[incRowExcel, 5].Value = "DEBIT";
                                        worksheet.Cells[incRowExcel, 6].Value = "CREDIT";
                                        worksheet.Cells[incRowExcel, 7].Value = "DEPT";
                                        string _range = "A" + incRowExcel + ":G" + incRowExcel;

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

                                            worksheet.Cells["A" + RowB + ":G" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + RowB + ":G" + RowB].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + RowB + ":G" + RowB].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + RowB + ":G" + RowB].Style.Border.Right.Style = ExcelBorderStyle.Medium;




                                            //area detail
                                            worksheet.Cells[incRowExcel, 1].Value = _no;
                                            worksheet.Cells[incRowExcel, 2].Value = rsDetail.AccountID;
                                            worksheet.Cells[incRowExcel, 3].Value = rsDetail.AccountName;
                                            worksheet.Cells[incRowExcel, 3].Style.WrapText = true;
                                            worksheet.Cells[incRowExcel, 4].Value = rsDetail.Description;
                                            worksheet.Cells[incRowExcel, 4].Style.WrapText = true;
                                            //worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.00";;
                                            //worksheet.Cells[incRowExcel, 6].Value = rsDetail.Amount;
                                            worksheet.Cells[incRowExcel, 5].Value = rsDetail.Debit;
                                            worksheet.Cells[incRowExcel, 6].Value = rsDetail.Credit;

                                            if (_cashier.DecimalPlaces == 0)
                                            {
                                                worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0";
                                            }
                                            else if(_cashier.DecimalPlaces == 2)
                                            {
                                                worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.00";
                                            }
                                            else if(_cashier.DecimalPlaces == 4)
                                            {
                                                worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.0000";
                                                worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.0000";
                                            }
                                            else if (_cashier.DecimalPlaces == 6)
                                            {
                                                worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.000000";
                                                worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.000000";
                                            }
                                            else
                                            {
                                                worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.00000000";
                                                worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.00000000";
                                            }
                    
                                            //worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0.0000";
                                            //worksheet.Cells[incRowExcel, 10].Value = rsDetail.Rate;
                                            //worksheet.Cells[incRowExcel, 11].Style.Numberformat.Format = "#,##0.00";
                                            //worksheet.Cells[incRowExcel, 11].Value = rsDetail.BaseDebit;
                                            //worksheet.Cells[incRowExcel, 12].Style.Numberformat.Format = "#,##0.00";
                                            //worksheet.Cells[incRowExcel, 12].Value = rsDetail.BaseCredit;
                                            worksheet.Cells[incRowExcel, 7].Value = rsDetail.DepartmentID;
                                            worksheet.Cells[incRowExcel, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
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

                                        //worksheet.Cells["I" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        //worksheet.Cells["I" + RowB + ":I" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                        //worksheet.Cells["I" + RowB + ":I" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                        //worksheet.Cells["I" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                        //worksheet.Cells["J" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        //worksheet.Cells["J" + RowB + ":J" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                        //worksheet.Cells["J" + RowB + ":J" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                        //worksheet.Cells["J" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                        //worksheet.Cells["K" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        //worksheet.Cells["K" + RowB + ":K" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                        //worksheet.Cells["K" + RowB + ":K" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                        //worksheet.Cells["K" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                        //worksheet.Cells["L" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        //worksheet.Cells["L" + RowB + ":L" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                        //worksheet.Cells["L" + RowB + ":L" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                        //worksheet.Cells["L" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                        //worksheet.Cells["M" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        //worksheet.Cells["M" + RowB + ":M" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                        //worksheet.Cells["M" + RowB + ":M" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                        //worksheet.Cells["M" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;



                                        //worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.00";;
                                        //worksheet.Cells[incRowExcel, 5].Formula = "SUM(E" + _startRowDetail + ":F" + _endRowDetail + ")";
                                        worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.00"; ;
                                        worksheet.Cells[incRowExcel, 5].Formula = "SUM(E" + _startRowDetail + ":E" + _endRowDetail + ")";
                                        worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.00"; ;
                                        worksheet.Cells[incRowExcel, 6].Formula = "SUM(F" + _startRowDetail + ":F" + _endRowDetail + ")";
                                        worksheet.Cells[incRowExcel, 5].Calculate();
                                        worksheet.Cells[incRowExcel, 6].Calculate();
                                        incRowExcel = incRowExcel + 1;
                                        worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.00"; ;
                                        worksheet.Cells[incRowExcel, 5].Value = worksheet.Cells[incRowExcel - 1, 5].Value;
                                        worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.00"; ;
                                        worksheet.Cells[incRowExcel, 6].Value = worksheet.Cells[incRowExcel - 1, 6].Value;
                                        worksheet.Cells[incRowExcel - 1, 5].Value = "";
                                        worksheet.Cells[incRowExcel - 1, 6].Value = "";
                                        incRowExcel = incRowExcel + 2;
                                        worksheet.Cells[incRowExcel, 2].Value = "Prepared By";
                                        worksheet.Cells[incRowExcel, 4].Value = "Checked By";
                                        worksheet.Cells[incRowExcel, 6].Value = "Approved By";
                                        incRowExcel = incRowExcel + 5;
                                        worksheet.Cells[incRowExcel, 2].Value = "(     " + rsHeader.Key.CheckedBy;
                                        worksheet.Cells[incRowExcel, 3].Value = "      )";
                                        worksheet.Cells[incRowExcel, 4].Value = "(                                         )";
                                        worksheet.Cells[incRowExcel, 6].Value = "(                                 ";
                                        worksheet.Cells[incRowExcel, 7].Value = "      )";
                                        worksheet.Row(incRowExcel).PageBreak = true;

                                    }

                                    string _rangeDetail = "A:G";

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
                                    worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel, 8];
                                    worksheet.Column(3).Width = 30;
                                    worksheet.Column(4).Width = 30;
                                    worksheet.Column(1).Width = 5;
                                    worksheet.Column(8).Width = 1;
                                    worksheet.Column(2).AutoFit();
                                    worksheet.Column(5).AutoFit();
                                    worksheet.Column(6).AutoFit();
                                    worksheet.Column(7).AutoFit();
                                    worksheet.PrinterSettings.Orientation = eOrientation.Portrait; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                    // worksheet.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN
                                    // worksheet.PrinterSettings.FitToPage = true;
                                    //worksheet.HeaderFooter.OddHeader.RightAlignedText = "&14 PAYMENT VOUCHER";
                                    // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                    worksheet.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
                                    worksheet.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
                                    //worksheet.View.PageLayoutView = Tools.DefaultReportPageLayoutView();
                                    worksheet.View.ShowGridLines = Tools.DefaultReportShowGridLines();
                                    worksheet.HeaderFooter.OddHeader.CenteredText = "&14 PAYMENT / JOURNAL VOUCHER";
                                    worksheet.HeaderFooter.OddHeader.LeftAlignedText = Tools.DefaultReportHeaderLeftText();

                                    worksheet.HeaderFooter.OddFooter.RightAlignedText =
                                    string.Format("Page {0} of {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);

                                    //worksheet.HeaderFooter.OddFooter.CenteredText = Tools.DefaultReportFooterCenterText();
                                    worksheet.HeaderFooter.OddFooter.LeftAlignedText = Tools.DefaultReportFooterLeftText();

                                    package.Save();

                                }
                                Tools.ExportFromExcelToPDF(filePath, pdfPath);
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

        public Boolean Receipt_Voucher(string _userID, Cashier _cashier)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText =
                             @" Select lower(C.EntryUsersID) CheckedBy,lower(C.EntryUsersID) ApprovedBy,          
                              reference , valuedate,A.ID AccountID, A.Name AccountName, C.Description,       
                              DebitCredit,(Case When DebitCredit ='C' then 0 else BaseCredit end) Debit,(Case When DebitCredit ='C' then BaseDebit else 0 end) Credit,F.ID DepartmentID       
                              from Cashier C         
                              left join Account A on C.Creditaccountpk =A.Accountpk and A.status = 2         
                              left join Office E on C.OfficePK = E.OfficePK and E.status = 2         
                              left join Department F on C.DepartmentPK = F.DepartmentPK and F.status = 2         
                              left join Agent G on C.AgentPK = G.AgentPK and G.status = 2         
                              left join Consignee H on C.ConsigneePK = H.ConsigneePK and H.status = 2         
                              left join Instrument I on C.InstrumentPK = I.InstrumentPK and I.status = 2         
                              Where C.Reference = @Reference  and C.Status in (1,2)         
                              UNION ALL         
                              Select lower(C.EntryUsersID) CheckedBy,lower(C.EntryUsersID) ApprovedBy,         
                              reference , valuedate,A.ID AccountID, A.Name AccountName, '' Description,       
                              'D', SUM(Case When DebitCredit ='C' then BaseDebit else BaseCredit * -1 end) Debit,0 Credit,'' DepartmentID             
                              from Cashier C         
                              left join Account A on C.DebitAccountPK =A.Accountpk and A.status = 2         
                              left join Office E on C.OfficePK = E.OfficePK and E.status = 2         
                              left join Department F on C.DepartmentPK = F.DepartmentPK and F.status = 2         
                              left join Agent G on C.AgentPK = G.AgentPK and G.status = 2         
                              left join Consignee H on C.ConsigneePK = H.ConsigneePK and H.status = 2         
                              left join Instrument I on C.InstrumentPK = I.InstrumentPK and I.status = 2        
                              Where C.Reference = @Reference  and C.Status in (1,2)       
                              group by C.EntryUsersID,Reference , Valuedate,A.ID, A.Name    
                              Order By DebitCredit Desc ";

                        cmd.Parameters.AddWithValue("@Reference", _cashier.Reference);
                        //cmd.Parameters.AddWithValue("@Status", _cashier.Status);
                        using (SqlDataReader dr0 = cmd.ExecuteReader())
                        {
                            if (!dr0.HasRows)
                            {
                                return false;
                            }
                            else
                            {
                                string filePath = Tools.ReportsPath + "ReceiptVoucher" + "_" + _userID + ".xlsx";
                                string pdfPath = Tools.ReportsPath + "ReceiptVoucher" + "_" + _userID + ".pdf";
                                FileInfo excelFile = new FileInfo(filePath);
                                if (excelFile.Exists)
                                {
                                    excelFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                    excelFile = new FileInfo(filePath);
                                }

                                // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                using (ExcelPackage package = new ExcelPackage(excelFile))
                                {
                                    package.Workbook.Properties.Title = "FinanceReport";
                                    package.Workbook.Properties.Author = Tools.DefaultReportAuthor();
                                    package.Workbook.Properties.Comments = Tools.DefaultReportComments();
                                    package.Workbook.Properties.Company = Tools.DefaultReportCompany();
                                    package.Workbook.Properties.SetCustomPropertyValue("Checked by", _userID);
                                    package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", Tools.DefaultReportAssemblyName());

                                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Receipt Voucher");


                                    //ATUR DATA GROUPINGNYA DULU
                                    List<CashierVoucher> rList = new List<CashierVoucher>();
                                    while (dr0.Read())
                                    {
                                        CashierVoucher rSingle = new CashierVoucher();
                                        rSingle.ValueDate = Convert.ToDateTime(dr0["ValueDate"]);
                                        rSingle.Reference = Convert.ToString(dr0["Reference"]);
                                        rSingle.Description = Convert.ToString(dr0["Description"]);
                                        rSingle.AccountID = Convert.ToString(dr0["AccountID"]);
                                        rSingle.AccountName = Convert.ToString(dr0["AccountName"]);
                                        rSingle.DebitCredit = Convert.ToString(dr0["DebitCredit"]);
                                        rSingle.Debit = Convert.ToDecimal(dr0["Debit"]);
                                        rSingle.Credit = Convert.ToDecimal(dr0["Credit"]);
                                        rSingle.DepartmentID = Convert.ToString(dr0["DepartmentID"]);
                                        rSingle.CheckedBy = Convert.ToString(dr0["CheckedBy"]);
                                        rSingle.ApprovedBy = Convert.ToString(dr0["ApprovedBy"]);
                                        rList.Add(rSingle);

                                    }

                                    var GroupByReference =
                                                 from r in rList
                                                 orderby r.DebitCredit descending
                                                 group r by new { r.Reference, r.ValueDate, r.CheckedBy, } into rGroup
                                                 select rGroup;

                                    int incRowExcel = 0;

                                    foreach (var rsHeader in GroupByReference)
                                    {
                                        incRowExcel = incRowExcel + 2;
                                        worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 2].Value = "REFERENCE : ";
                                        worksheet.Cells[incRowExcel, 3].Value = rsHeader.Key.Reference;
                                        worksheet.Cells[incRowExcel, 5].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 5].Value = "DATE : ";
                                        //worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "dd-MM-yyyy";
                                        worksheet.Cells[incRowExcel, 6].Value = Convert.ToDateTime(rsHeader.Key.ValueDate).ToShortDateString();
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                        //worksheet.Cells[incRowExcel, 1].Value = "BANK";
                                        //worksheet.Cells["B" + incRowExcel + ":C" + incRowExcel].Merge = true;
                                        //worksheet.Cells[incRowExcel, 2].Value = rsHeader.Key.BankID + "-" + rsHeader.Key.BankName;
                                        incRowExcel++;

                                        //Row B = 3
                                        int RowB = incRowExcel;
                                        int RowG = incRowExcel + 1;

                                        worksheet.Cells[incRowExcel, 1].Value = "NO";
                                        worksheet.Cells[incRowExcel, 2].Value = "ACCOUNT ID";
                                        worksheet.Cells[incRowExcel, 3].Value = "NAME";
                                        worksheet.Cells[incRowExcel, 4].Value = "DESCRIPTION";
                                        worksheet.Cells[incRowExcel, 5].Value = "DEBIT";
                                        worksheet.Cells[incRowExcel, 6].Value = "CREDIT";
                                        worksheet.Cells[incRowExcel, 7].Value = "DEPT";
                                        string _range = "A" + incRowExcel + ":G" + incRowExcel;

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

                                            worksheet.Cells["A" + RowB + ":G" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + RowB + ":G" + RowB].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + RowB + ":G" + RowB].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + RowB + ":G" + RowB].Style.Border.Right.Style = ExcelBorderStyle.Medium;




                                            //area detail
                                            worksheet.Cells[incRowExcel, 1].Value = _no;
                                            worksheet.Cells[incRowExcel, 2].Value = rsDetail.AccountID;
                                            worksheet.Cells[incRowExcel, 3].Value = rsDetail.AccountName;
                                            worksheet.Cells[incRowExcel, 3].Style.WrapText = true;
                                            worksheet.Cells[incRowExcel, 4].Value = rsDetail.Description;
                                            worksheet.Cells[incRowExcel, 4].Style.WrapText = true;
                                            //worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.00";;
                                            //worksheet.Cells[incRowExcel, 6].Value = rsDetail.Amount;
                                            worksheet.Cells[incRowExcel, 5].Value = rsDetail.Debit;
                                            worksheet.Cells[incRowExcel, 6].Value = rsDetail.Credit;
                                            if (_cashier.DecimalPlaces == 0)
                                            {
                                                worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0";
                                            }
                                            else if (_cashier.DecimalPlaces == 2)
                                            {
                                                worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.00";
                                            }
                                            else if (_cashier.DecimalPlaces == 4)
                                            {
                                                worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.0000";
                                                worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.0000";
                                            }
                                            else if (_cashier.DecimalPlaces == 6)
                                            {
                                                worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.000000";
                                                worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.000000";
                                            }
                                            else
                                            {
                                                worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.00000000";
                                                worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.00000000";
                                            }

                                            worksheet.Cells[incRowExcel, 7].Value = rsDetail.DepartmentID;
                                            worksheet.Cells[incRowExcel, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
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





                                        //worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.00";;
                                        //worksheet.Cells[incRowExcel, 5].Formula = "SUM(E" + _startRowDetail + ":F" + _endRowDetail + ")";
                                        worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.00"; ;
                                        worksheet.Cells[incRowExcel, 5].Formula = "SUM(E" + _startRowDetail + ":E" + _endRowDetail + ")";
                                        worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.00"; ;
                                        worksheet.Cells[incRowExcel, 6].Formula = "SUM(F" + _startRowDetail + ":F" + _endRowDetail + ")";
                                        worksheet.Cells[incRowExcel, 5].Calculate();
                                        worksheet.Cells[incRowExcel, 6].Calculate();
                                        incRowExcel = incRowExcel + 1;
                                        worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.00"; ;
                                        worksheet.Cells[incRowExcel, 5].Value = worksheet.Cells[incRowExcel - 1, 5].Value;
                                        worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.00"; ;
                                        worksheet.Cells[incRowExcel, 6].Value = worksheet.Cells[incRowExcel - 1, 6].Value;
                                        worksheet.Cells[incRowExcel - 1, 5].Value = "";
                                        worksheet.Cells[incRowExcel - 1, 6].Value = "";
                                        incRowExcel = incRowExcel + 2;
                                        worksheet.Cells[incRowExcel, 2].Value = "Prepared By";
                                        worksheet.Cells[incRowExcel, 4].Value = "Checked By";
                                        worksheet.Cells[incRowExcel, 6].Value = "Approved By";
                                        incRowExcel = incRowExcel + 5;
                                        worksheet.Cells[incRowExcel, 2].Value = "(     " + rsHeader.Key.CheckedBy;
                                        worksheet.Cells[incRowExcel, 3].Value = "      )";
                                        worksheet.Cells[incRowExcel, 4].Value = "(                                         )";
                                        worksheet.Cells[incRowExcel, 6].Value = "(                                 ";
                                        worksheet.Cells[incRowExcel, 7].Value = "      )";
                                        worksheet.Row(incRowExcel).PageBreak = true;


                                    }

                                    string _rangeDetail = "A:G";

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
                                    worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel, 8];
                                    worksheet.Column(3).Width = 30;
                                    worksheet.Column(4).Width = 30;
                                    worksheet.Column(1).Width = 5;
                                    worksheet.Column(8).Width = 1;
                                    worksheet.Column(2).AutoFit();
                                    worksheet.Column(5).AutoFit();
                                    worksheet.Column(6).AutoFit();
                                    worksheet.Column(7).AutoFit();
                                    worksheet.PrinterSettings.Orientation = eOrientation.Portrait; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                    // worksheet.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN
                                    // worksheet.PrinterSettings.FitToPage = true;
                                    //worksheet.HeaderFooter.OddHeader.RightAlignedText = "&14 Receipt VOUCHER";
                                    // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                    worksheet.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
                                    worksheet.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
                                    //worksheet.View.PageLayoutView = Tools.DefaultReportPageLayoutView();
                                    worksheet.View.ShowGridLines = Tools.DefaultReportShowGridLines();
                                    worksheet.HeaderFooter.OddHeader.CenteredText = "&14 RECEIPT / JOURNAL VOUCHER";
                                    worksheet.HeaderFooter.OddHeader.LeftAlignedText = Tools.DefaultReportHeaderLeftText();

                                    worksheet.HeaderFooter.OddFooter.RightAlignedText =
                                    string.Format("Page {0} of {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);

                                    //worksheet.HeaderFooter.OddFooter.CenteredText = Tools.DefaultReportFooterCenterText();
                                    worksheet.HeaderFooter.OddFooter.LeftAlignedText = Tools.DefaultReportFooterLeftText();

                                    package.Save();

                                }
                                Tools.ExportFromExcelToPDF(filePath, pdfPath);
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


        string _insertCommand = "INSERT INTO [dbo].[Cashier] " +
                           "([CashierPK],[HistoryPK],[Status],[PeriodPK],[ValueDate],[Type],[Reference],[Description],[DebitCredit],[DebitCurrencyPK], " +
                           " [CreditCurrencyPK],[DebitCashRefPK],[CreditCashRefPK],[DebitAccountPK],[CreditAccountPK],[Debit], " +
                           " [Credit],[DebitCurrencyRate],[CreditCurrencyRate],[BaseDebit],[BaseCredit],[PercentAmount],[FinalAmount],[OfficePK],[DepartmentPK],[AgentPK],[CounterpartPK],[ConsigneePK],[InstrumentPK],[JournalNo], ";

        string _paramaterCommand = "@PeriodPK,@ValueDate,@Type,@Reference,@Description,@DebitCredit,@DebitCurrencyPK,@CreditCurrencyPK,@DebitCashRefPK," +
                            "@CreditCashRefPK,@DebitAccountPK,@CreditAccountPK,@Debit,@Credit,@DebitCurrencyRate,@CreditCurrencyRate,@BaseDebit,@BaseCredit,@PercentAmount,@FinalAmount,@OfficePK,@DepartmentPK,@AgentPK,@CounterpartPK,@ConsigneePK,@InstrumentPK,@JournalNo, ";


        private Cashier setCashier(SqlDataReader dr)
        {
            Cashier M_Cashier = new Cashier();
            M_Cashier.CashierPK = Convert.ToInt32(dr["CashierPK"]);
            M_Cashier.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_Cashier.Selected = dr["Selected"].Equals(DBNull.Value) == true ? false : Convert.ToBoolean(dr["Selected"]);
            M_Cashier.Status = Convert.ToInt32(dr["Status"]);
            M_Cashier.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_Cashier.Notes = Convert.ToString(dr["Notes"]);
            M_Cashier.PeriodPK = Convert.ToInt32(dr["PeriodPK"]);
            M_Cashier.PeriodID = Convert.ToString(dr["PeriodID"]);
            M_Cashier.ValueDate = dr["ValueDate"].ToString();
            M_Cashier.Type = Convert.ToString(dr["Type"]);
            M_Cashier.RefNo = Convert.ToInt32(dr["RefNo"]);
            M_Cashier.Reference = Convert.ToString(dr["Reference"]);
            M_Cashier.DebitCredit = Convert.ToString(dr["DebitCredit"]);
            M_Cashier.Description = Convert.ToString(dr["Description"]);
            M_Cashier.DebitCurrencyPK = Convert.ToInt32(dr["DebitCurrencyPK"]);
            M_Cashier.DebitCurrencyID = Convert.ToString(dr["DebitCurrencyID"]);
            M_Cashier.CreditCurrencyPK = Convert.ToInt32(dr["CreditCurrencyPK"]);
            M_Cashier.CreditCurrencyID = Convert.ToString(dr["CreditCurrencyID"]);
            M_Cashier.DebitCashRefPK = Convert.ToInt32(dr["DebitCashRefPK"]);
            M_Cashier.DebitCashRefID = Convert.ToString(dr["DebitCashRefID"]);
            M_Cashier.DebitCashRefName = Convert.ToString(dr["DebitCashRefName"]);
            M_Cashier.CreditCashRefPK = Convert.ToInt32(dr["CreditCashRefPK"]);
            M_Cashier.CreditCashRefID = Convert.ToString(dr["CreditCashRefID"]);
            M_Cashier.CreditCashRefName = Convert.ToString(dr["CreditCashRefName"]);
            M_Cashier.DebitAccountPK = Convert.ToInt32(dr["DebitAccountPK"]);
            M_Cashier.DebitAccountID = Convert.ToString(dr["DebitAccountID"]);
            M_Cashier.DebitAccountName = Convert.ToString(dr["DebitAccountName"]);
            M_Cashier.CreditAccountPK = Convert.ToInt32(dr["CreditAccountPK"]);
            M_Cashier.CreditAccountID = Convert.ToString(dr["CreditAccountID"]);
            M_Cashier.CreditAccountName = Convert.ToString(dr["CreditAccountName"]);
            M_Cashier.Debit = Convert.ToDecimal(dr["Debit"]);
            M_Cashier.Credit = Convert.ToDecimal(dr["Credit"]);
            M_Cashier.DebitCurrencyRate = Convert.ToDecimal(dr["DebitCurrencyRate"]);
            M_Cashier.CreditCurrencyRate = Convert.ToDecimal(dr["CreditCurrencyRate"]);
            M_Cashier.BaseDebit = Convert.ToDecimal(dr["BaseDebit"]);
            M_Cashier.BaseCredit = Convert.ToDecimal(dr["BaseCredit"]);
            M_Cashier.PercentAmount = Convert.ToDecimal(dr["PercentAmount"]);
            M_Cashier.FinalAmount = Convert.ToDecimal(dr["FinalAmount"]);
            M_Cashier.OfficePK = Convert.ToInt32(dr["OfficePK"]);
            M_Cashier.OfficeID = Convert.ToString(dr["OfficeID"]);
            M_Cashier.DepartmentPK = Convert.ToInt32(dr["DepartmentPK"]);
            M_Cashier.DepartmentID = Convert.ToString(dr["DepartmentID"]);
            M_Cashier.AgentPK = Convert.ToInt32(dr["AgentPK"]);
            M_Cashier.AgentID = Convert.ToString(dr["AgentID"]);
            M_Cashier.CounterpartPK = Convert.ToInt32(dr["CounterpartPK"]);
            M_Cashier.CounterpartID = Convert.ToString(dr["CounterpartID"]);
            M_Cashier.ConsigneePK = Convert.ToInt32(dr["ConsigneePK"]);
            M_Cashier.ConsigneeID = Convert.ToString(dr["ConsigneeID"]);
            M_Cashier.InstrumentPK = Convert.ToInt32(dr["InstrumentPK"]);
            M_Cashier.InstrumentID = Convert.ToString(dr["InstrumentID"]);
            M_Cashier.InstrumentName = Convert.ToString(dr["InstrumentName"]);
            M_Cashier.JournalNo = Convert.ToInt32(dr["JournalNo"]);
            M_Cashier.Posted = Convert.ToBoolean(dr["Posted"]);
            M_Cashier.PostedBy = Convert.ToString(dr["PostedBy"]);
            M_Cashier.PostedTime = Convert.ToString(dr["PostedTime"]);
            M_Cashier.Revised = Convert.ToBoolean(dr["Revised"]);
            M_Cashier.RevisedBy = Convert.ToString(dr["RevisedBy"]);
            M_Cashier.RevisedTime = Convert.ToString(dr["RevisedTime"]);
            M_Cashier.EntryUsersID = dr["EntryUsersID"].ToString();
            M_Cashier.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_Cashier.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_Cashier.VoidUsersID = dr["VoidUsersID"].ToString();
            M_Cashier.EntryTime = dr["EntryTime"].ToString();
            M_Cashier.UpdateTime = dr["UpdateTime"].ToString();
            M_Cashier.ApprovedTime = dr["ApprovedTime"].ToString();
            M_Cashier.VoidTime = dr["VoidTime"].ToString();
            M_Cashier.DBUserID = dr["DBUserID"].ToString();
            M_Cashier.DBTerminalID = dr["DBTerminalID"].ToString();
            M_Cashier.LastUpdate = dr["LastUpdate"].ToString();
            M_Cashier.LastUpdateDB = dr["LastUpdateDB"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["LastUpdateDB"]);
            return M_Cashier;
        }

        public CashierAddNew Cashier_Add(Cashier _cashier, bool _havePrivillege)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_havePrivillege)
                        {
                            cmd.CommandText = "Declare @newCashierPK int \n " +
                                 "Select @newCashierPK = isnull(max(CashierPk),0) + 1 from Cashier \n " +
                                _insertCommand + "[EntryUsersID],[EntryTime],[ApprovedUsersID],[ApprovedTime],LastUpdate)" +
                                 "Select @newCashierPK ,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@LastUpdate \n " +
                                 "Select @newCashierPK newCashierPK, 1 newHistoryPK ";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _cashier.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        }
                        else
                        {
                            cmd.CommandText = "Declare @newCashierPK int \n " +
                                 "Select @newCashierPK = isnull(max(CashierPk),0) + 1 from Cashier \n " +
                                _insertCommand + "[EntryUsersID],[EntryTime],LastUpdate)" +
                                "Select @newCashierPK ,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@LastUpdate \n " +
                                 "Select @newCashierPK newCashierPK, 1 newHistoryPK ";
                        }

                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@PeriodPK", _cashier.PeriodPK);
                        cmd.Parameters.AddWithValue("@ValueDate", _cashier.ValueDate);
                        cmd.Parameters.AddWithValue("@Type", _cashier.Type);
                        cmd.Parameters.AddWithValue("@Reference", _cashier.Reference);
                        cmd.Parameters.AddWithValue("@Description", _cashier.Description);
                        cmd.Parameters.AddWithValue("@DebitCredit", _cashier.DebitCredit);
                        cmd.Parameters.AddWithValue("@DebitCurrencyPK", _cashier.DebitCurrencyPK);
                        cmd.Parameters.AddWithValue("@CreditCurrencyPK", _cashier.CreditCurrencyPK);
                        cmd.Parameters.AddWithValue("@DebitCashRefPK", _cashier.DebitCashRefPK);
                        cmd.Parameters.AddWithValue("@CreditCashRefPK", _cashier.CreditCashRefPK);
                        cmd.Parameters.AddWithValue("@DebitAccountPK", _cashier.DebitAccountPK);
                        cmd.Parameters.AddWithValue("@CreditAccountPK", _cashier.CreditAccountPK);
                        cmd.Parameters.AddWithValue("@Debit", _cashier.Debit);
                        cmd.Parameters.AddWithValue("@Credit", _cashier.Credit);
                        cmd.Parameters.AddWithValue("@DebitCurrencyRate", _cashier.DebitCurrencyRate);
                        cmd.Parameters.AddWithValue("@CreditCurrencyRate", _cashier.CreditCurrencyRate);
                        cmd.Parameters.AddWithValue("@BaseDebit", _cashier.BaseDebit);
                        cmd.Parameters.AddWithValue("@BaseCredit", _cashier.BaseCredit);
                        cmd.Parameters.AddWithValue("@PercentAmount", _cashier.PercentAmount);
                        cmd.Parameters.AddWithValue("@FinalAmount", _cashier.FinalAmount);
                        cmd.Parameters.AddWithValue("@OfficePK", _cashier.OfficePK);
                        cmd.Parameters.AddWithValue("@DepartmentPK", _cashier.DepartmentPK);
                        cmd.Parameters.AddWithValue("@AgentPK", _cashier.AgentPK);
                        cmd.Parameters.AddWithValue("@CounterpartPK", _cashier.CounterpartPK);
                        cmd.Parameters.AddWithValue("@ConsigneePK", _cashier.ConsigneePK);
                        cmd.Parameters.AddWithValue("@InstrumentPK", _cashier.InstrumentPK);
                        cmd.Parameters.AddWithValue("@JournalNo", _cashier.JournalNo);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _cashier.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return new CashierAddNew()
                                {
                                    CashierPK = Convert.ToInt32(dr["newCashierPK"]),
                                    HistoryPK = Convert.ToInt32(dr["newHistoryPK"]),
                                    Message = "Insert Cashier Success"
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

        public void Cashier_PostingBySelected(string _userID, string _permissionID, DateTime _dateFrom, DateTime _dateTo, string _type, ParamCashierBySelected _paramCashierBySelected)
        {
            try
            {
                string paramCashierPK = "";
                if (!_host.findString(_paramCashierBySelected.stringCashierPaymentSelected.ToLower(), "0", ",") && !string.IsNullOrEmpty(_paramCashierBySelected.stringCashierPaymentSelected))
                {
                    paramCashierPK = " And CashierPK in (" + _paramCashierBySelected.stringCashierPaymentSelected + ") ";
                }
                else
                {
                    paramCashierPK = " And CashierPK in (0) ";
                }

                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        string _bankParamCr = "";
                        string _bankParamCp = "";
                        string _journalDetailCr = "";
                        string _journalDetailCp = "";

                        //CP
                        _bankParamCp =
                        @" 
                        
                        SET @AutoNo = 1    
                        Select @BankAmount = sum(case when DebitCredit = 'D' then Credit else credit * -1 end)     
                        ,@BaseBankAmount =   sum(case when DebitCredit = 'D' then BaseCredit else Basecredit * -1 end)  From Cashier WHERE Reference = @Reference and Posted = 0 and Revised = 0 and status = 2       
                        INSERT INTO [dbo].[JournalDetail]    
                                ([JournalPK]    
                                ,[AutoNo]    
                                ,[HistoryPK]    
                                ,[Status]    
                                ,[AccountPK]    
                                ,[CurrencyPK]    
                                ,[OfficePK]    
                                ,[DepartmentPK]    
                                ,[AgentPK]    
                                ,[CounterpartPK]    
                                ,[InstrumentPK]    
                                ,[ConsigneePK]    
                                ,[DetailDescription]    
                                ,[DocRef]    
                                ,[DebitCredit]    
                                ,[Amount]    
                                ,[Debit]    
                                ,[Credit]    
                                ,[CurrencyRate]    
                                ,[BaseDebit]    
                                ,[BaseCredit]    
                                ,[LastUsersID]    
                                ,[LastUpdate])    
                            SELECT top 1 @JournalPK,@AutoNo,1,2,CreditAccountPK,case when CreditCurrencyPK = 0 then 1 else CreditCurrencyPK end,0,0,    
                            0,0,0,0,@BankDescription,@Reference,'C',@BankAmount,0,@BankAmount,    
                            CreditCurrencyRate,0,@BaseBankAmount,@UserID,@DateTimeNow from cashier where reference = @Reference and posted = 0 and status = 2      
                            SET @AutoNo = 2         ";

                        _journalDetailCp =

                         @"  
                        INSERT INTO [dbo].[JournalDetail]    
                                ([JournalPK]    
                                ,[AutoNo]    
                                ,[HistoryPK]    
                                ,[Status]    
                                ,[AccountPK]    
                                ,[CurrencyPK]    
                                ,[OfficePK]    
                                ,[DepartmentPK]    
                                ,[AgentPK]    
                                ,[CounterpartPK]    
                                ,[InstrumentPK]    
                                ,[ConsigneePK]    
                                ,[DetailDescription]    
                                ,[DocRef]    
                                ,[DebitCredit]    
                                ,[Amount]    
                                ,[Debit]    
                                ,[Credit]    
                                ,[CurrencyRate]    
                                ,[BaseDebit]    
                                ,[BaseCredit]    
                                ,[LastUsersID]    
                                ,[LastUpdate])    
                         SELECT @JournalPK,@AutoNo,1,2,@DebitAccountPK,@DebitCurrencyPK,@OfficePK,@DepartmentPK,    
                         @AgentPK,@CounterpartPK,@InstrumentPK,@ConsigneePK,@Description,@Reference,
		                 Case when @DebitCredit = 'D' then 'D' Else 'C' END,@Debit,Case when @DebitCredit = 'D' then @Debit Else 0 END,    
                         Case when @DebitCredit = 'D' then 0 Else @Debit END,@DebitCurrencyRate,
		                 Case when @DebitCredit = 'D' then @BaseDebit Else 0 END,Case when @DebitCredit = 'D' then 0 Else @BaseDebit END,
		                 @UserID,@DateTimeNow    
                         SET @AutoNo = ISNULL(@AutoNo,0) +  1
                          ";


                        //CR
                        _bankParamCr =
                        @"      
                        SET @AutoNo = 1    
                           Select @BankAmount = sum(case when DebitCredit = 'C' then debit else Debit * -1 end)     
                        ,@BaseBankAmount =   sum(case when DebitCredit = 'C' then BaseDebit * -1 else BaseDebit  end)  From Cashier WHERE Reference = @Reference and Posted = 0 and Revised = 0 and status = 2       
                        INSERT INTO [dbo].[JournalDetail]    
                                ([JournalPK]    
                                ,[AutoNo]    
                                ,[HistoryPK]    
                                ,[Status]    
                                ,[AccountPK]    
                                ,[CurrencyPK]    
                                ,[OfficePK]    
                                ,[DepartmentPK]    
                                ,[AgentPK]    
                                ,[CounterpartPK]    
                                ,[InstrumentPK]    
                                ,[ConsigneePK]    
                                ,[DetailDescription]    
                                ,[DocRef]    
                                ,[DebitCredit]    
                                ,[Amount]    
                                ,[Debit]    
                                ,[Credit]    
                                ,[CurrencyRate]    
                                ,[BaseDebit]    
                                ,[BaseCredit]    
                                ,[LastUsersID]    
                                ,[LastUpdate])    
                  SELECT  top 1 @JournalPK,@AutoNo,1,2,DebitAccountPK,case when DebitCurrencyPK = 0 then 1 else DebitCurrencyPK end,0,0,    
                         0,0,0,0,@BankDescription,@Reference,'D',abs(@BankAmount),abs(@BankAmount),0,DebitCurrencyRate,abs(@BaseBankAmount),0,@UserID,@DateTimeNow    
                         FROM Cashier WHERE Reference = @Reference and Posted = 0 and Revised = 0 and status = 2       
                         SET @AutoNo = 2 
                        ";

                        _journalDetailCr =
                         @" 
                                INSERT INTO [dbo].[JournalDetail]    
                                        ([JournalPK]    
                                        ,[AutoNo]    
                                        ,[HistoryPK]    
                                        ,[Status]    
                                        ,[AccountPK]    
                                        ,[CurrencyPK]    
                                        ,[OfficePK]    
                                        ,[DepartmentPK]    
                                        ,[AgentPK]    
                                        ,[CounterpartPK]    
                                        ,[InstrumentPK]    
                                        ,[ConsigneePK]    
                                        ,[DetailDescription]    
                                        ,[DocRef]    
                                        ,[DebitCredit]    
                                        ,[Amount]    
                                        ,[Debit]    
                                        ,[Credit]    
                                        ,[CurrencyRate]    
                                        ,[BaseDebit]    
                                        ,[BaseCredit]    
                                        ,[LastUsersID]    
                                        ,[LastUpdate])    
                                 SELECT @JournalPK,@AutoNo,1,2,@CreditAccountPK,@CreditCurrencyPK,@OfficePK,@DepartmentPK,    
                                 @AgentPK,@CounterpartPK,@InstrumentPK,@ConsigneePK,@Description,@Reference,Case when @DebitCredit = 'D' then 'D' Else 'C' END,@Credit,    
                                 Case when @DebitCredit = 'D' then @Credit Else 0 END,Case when @DebitCredit = 'D' then 0 Else @Credit END,@CreditCurrencyRate,Case when @DebitCredit = 'D' then @BaseCredit Else 0 END,Case when @DebitCredit = 'D' then 0 Else @BaseCredit END,@UserID,@DateTimeNow    
                                 SET @AutoNo = ISNULL(@AutoNo,0) +  1

                            ";



                        cmd.CommandText =
                            @"
                            Declare @IPAddress nvarchar(50) select @IPAddress = IPAddress from Users where ID = @UserID and Status = 2      
                            Insert into Activity(Time,PermissionID,ObjectTable,ObjectTablePK,Status,Message,UsersID,IPAddress,LastUpdate)      
                            Select @DatetimeNow,@PermissionID,'Cashier',CashierPK,1,'Posting by Selected Data',
                            @UserID,@IPAddress,@DatetimeNow  from Cashier 
                            where reference in (Select Reference From Cashier 
                            Where ValueDate between @DateFrom and @DateTo and Status = 2 and Posted = 0 and Revised = 0 " + paramCashierPK + @" and type = @Paramtype ) 
                            and Posted = 0 and Revised = 0 and status = 2      

                            --ADA BUG DI FINAL AMOUNT
                            update Cashier set FinalAmount = BaseDebit

                             Declare @Reference Nvarchar(30)    
       
                             Declare @periodPK int    
                             Declare @ValueDate Datetime    
                             Declare @Type Nvarchar(10)    
                             Declare @Description Nvarchar(Max)    
                             Declare @DebitCurrencyPK int    
                             Declare @CreditCurrencyPK int    
                             Declare @DebitCashRefPK int    
                             Declare @CreditCashRefPK int    
                             Declare @DebitAccountPK int    
                             Declare @CreditAccountPK int    
                             Declare @Debit numeric(19,4)    
                             Declare @Credit numeric(19,4)    
                             Declare @DebitCurrencyRate numeric(19,4)    
                             Declare @CreditCurrencyRate numeric(19,4)    
                             Declare @BaseDebit numeric(19,4)    
                             Declare @BaseCredit numeric(19,4)    
                             Declare @OfficePK int    
                             Declare @DepartmentPK int    
                             Declare @AgentPK int    
                             Declare @CounterpartPK int    
                             Declare @InstrumentPK int    
                             Declare @ConsigneePK int    
                             Declare @DebitCredit nvarchar(1)    
                             Declare @JournalPK int    
                             Declare @BankAmount Numeric(19,4) Declare @BaseBankAmount Numeric(19,4)    
                             Declare @BankDescription nvarchar(Max)    

       
                              DECLARE Header CURSOR FOR             
                                 SELECT distinct Reference FROM Cashier    
                                 WHERE status = 2 and Posted = 0 and revised = 0 " + paramCashierPK + @" and ValueDate between @DateFrom and @DateTo and type = @Paramtype     
                              OPEN Header             
                                 FETCH NEXT FROM Header INTO @Reference    
                              WHILE @@FETCH_STATUS = 0              
                                BEGIN       

                                     Select @JournalPK = isnull(max(JournalPK),0) +  1 From Journal     
       
                                     Declare @AutoNo Int    
       
                                    SET @BankDescription = ''    
                                    SELECT @BankDescription =  @BankDescription +   ' - ' +  Description , @Type = Type    
                                    FROM Cashier WHERE Reference = @Reference and Posted = 0 and Revised = 0 and status = 2    
                                     INSERT INTO [dbo].[Journal]    
                                                ([JournalPK]    
                                                ,[HistoryPK]    
                                                ,[Status]    
                                                ,[Notes]    
                                                ,[PeriodPK]    
                                                ,[ValueDate]    
                                                ,[TrxNo]    
                                                ,[TrxName]    
                                                ,[Reference]    
                                                ,[Type]    
                                                ,[Description]    
                                                ,[Posted]    
                                                ,[PostedBy]    
                                                ,[PostedTime]    
                                                ,[EntryUsersID]    
                                                ,[EntryTime]    
                                                ,[ApprovedUsersID]    
                                                ,[ApprovedTime]    
                                                ,[LastUpdate])    
                                     SELECT Top 1 @JournalPK,1,2,'Posting From Cashier',PeriodPK,    
                                     ValueDate,CashierPK,Type,@Reference,3,@BankDescription,1,@UserID,@DatetimeNow,    
                                     EntryUsersID,EntryTime,@UserID,@DatetimeNow,@DatetimeNow    
                                     FROM Cashier    
                                     WHERE Reference = @Reference And Status = 2 and Posted = 0 and Revised = 0        
                              if @Type = 'CP' Begin       
                            " + _bankParamCp +
                              @"
                            end else Begin  
                            " + _bankParamCr +
                              @"
                            end
                            DECLARE Detail CURSOR FOR     
                                     SELECT PeriodPK,ValueDate,Type,Description,DebitCredit,DebitCurrencyPK,CreditCurrencyPK,DebitCashRefPK    
                                     ,CreditCashRefPK,DebitAccountPK,CreditAccountPK,Debit,Credit,DebitCurrencyRate,CreditCurrencyRate,    
                                     BaseDebit,BaseCredit,OfficePK,DepartmentPK,AgentPK,CounterpartPK,InstrumentPK,ConsigneePK FROM Cashier    
                                     WHERE Reference = @Reference and Posted = 0 and Revised = 0 and status = 2    
       
                                     OPEN Detail              
                                     FETCH NEXT FROM Detail INTO     
                                     @periodPK,@ValueDate,@Type,@Description,@DebitCredit,@DebitCurrencyPK,@CreditCurrencyPK,@DebitCashRefPK    
                                     ,@CreditCashRefPK,@DebitAccountPK,@CreditAccountPK,@Debit,@Credit,@DebitCurrencyRate,@CreditCurrencyRate,    
                                     @BaseDebit,@BaseCredit,@OfficePK,@DepartmentPK,@AgentPK,@CounterpartPK,@InstrumentPK,@ConsigneePK    
       
                                    WHILE @@FETCH_STATUS = 0               
                              BEGIN      
                              if @Type = 'CP' Begin
                            "
                              + _journalDetailCp +
                              @"
                            end else Begin 
                            " + _journalDetailCr +
                              @"
                             END FETCH NEXT FROM Detail INTO    
                                    @periodPK,@ValueDate,@Type,@Description,@DebitCredit,@DebitCurrencyPK,@CreditCurrencyPK,@DebitCashRefPK    
                                    ,@CreditCashRefPK,@DebitAccountPK,@CreditAccountPK,@Debit,@Credit,@DebitCurrencyRate,@CreditCurrencyRate,    
                                    @BaseDebit,@BaseCredit,@OfficePK,@DepartmentPK,@AgentPK,@CounterpartPK,@InstrumentPK,@ConsigneePK      
                                    END       
       
                                    CLOSE Detail        
                                    DEALLOCATE Detail       
       
                                    UPDATE CASHIER SET Posted = 1,PostedBy = @UserID,PostedTime = @DatetimeNow, JournalNo = @JournalPK , LastUpdate = @DatetimeNow   
                                    WHERE Reference = @Reference and Posted = 0 and Revised = 0 and status = 2 and valueDate Between @DateFrom and @DateTo     
       
                                 FETCH NEXT FROM Header INTO @Reference    
                                END          
                                CLOSE Header       
                                DEALLOCATE Header  
                            "
                        ;
                        cmd.Parameters.AddWithValue("@PermissionID", _permissionID);
                        cmd.Parameters.AddWithValue("@UserID", _userID);
                        cmd.Parameters.AddWithValue("@DatetimeNow", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _dateTo);
                        cmd.Parameters.AddWithValue("@Paramtype", _type);

                        cmd.ExecuteNonQuery();
                    }

                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int Cashier_Update(Cashier _cashier, bool _havePrivillege)
        {
            int _newHisPK;
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                int status = _host.Get_Status(_cashier.CashierPK, _cashier.HistoryPK, "Cashier");
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update Cashier set status=2, Notes=@Notes,PeriodPK=@PeriodPK,ValueDate=@ValueDate,Type=@Type," +
                                "Reference=@Reference,Description=@Description,DebitCredit=@DebitCredit,DebitCurrencyPK=@DebitCurrencyPK,CreditCurrencyPK=@CreditCurrencyPK, " +
                                "DebitCashRefPK=@DebitCashRefPK,CreditCashRefPK=@CreditCashRefPK,DebitAccountPK=@DebitAccountPK,CreditAccountPK=@CreditAccountPK, " +
                                "Debit=@Debit,Credit=@Credit,DebitCurrencyRate=@DebitCurrencyRate,CreditCurrencyRate=@CreditCurrencyRate, " +
                                "BaseDebit=@BaseDebit,BaseCredit=@BaseCredit,PercentAmount=@PercentAmount,FinalAmount=@FinalAmount,OfficePK=@OfficePK,DepartmentPK=@DepartmentPK, " +
                                "AgentPK=@AgentPK,CounterpartPK=@CounterpartPK,ConsigneePK=@ConsigneePK,InstrumentPK=@InstrumentPK,JournalNo=@JournalNo, " +
                                "ApprovedUsersID=@ApprovedUsersID, " +
                                "ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime ,LastUpdate=@lastupdate " +
                                "where CashierPK = @PK and historyPK = @HistoryPK";

                            cmd.Parameters.AddWithValue("@HistoryPK", _cashier.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _cashier.CashierPK);
                            cmd.Parameters.AddWithValue("@Notes", _cashier.Notes);
                            cmd.Parameters.AddWithValue("@PeriodPK", _cashier.PeriodPK);
                            cmd.Parameters.AddWithValue("@ValueDate", _cashier.ValueDate);
                            cmd.Parameters.AddWithValue("@Type", _cashier.Type);
                            cmd.Parameters.AddWithValue("@Reference", _cashier.Reference);
                            cmd.Parameters.AddWithValue("@Description", _cashier.Description);
                            cmd.Parameters.AddWithValue("@DebitCredit", _cashier.DebitCredit);
                            cmd.Parameters.AddWithValue("@DebitCurrencyPK", _cashier.DebitCurrencyPK);
                            cmd.Parameters.AddWithValue("@CreditCurrencyPK", _cashier.CreditCurrencyPK);
                            cmd.Parameters.AddWithValue("@DebitCashRefPK", _cashier.DebitCashRefPK);
                            cmd.Parameters.AddWithValue("@CreditCashRefPK", _cashier.CreditCashRefPK);
                            cmd.Parameters.AddWithValue("@DebitAccountPK", _cashier.DebitAccountPK);
                            cmd.Parameters.AddWithValue("@CreditAccountPK", _cashier.CreditAccountPK);
                            cmd.Parameters.AddWithValue("@Debit", _cashier.Debit);
                            cmd.Parameters.AddWithValue("@Credit", _cashier.Credit);
                            cmd.Parameters.AddWithValue("@DebitCurrencyRate", _cashier.DebitCurrencyRate);
                            cmd.Parameters.AddWithValue("@CreditCurrencyRate", _cashier.CreditCurrencyRate);
                            cmd.Parameters.AddWithValue("@BaseDebit", _cashier.BaseDebit);
                            cmd.Parameters.AddWithValue("@BaseCredit", _cashier.BaseCredit);
                            cmd.Parameters.AddWithValue("@PercentAmount", _cashier.PercentAmount);
                            cmd.Parameters.AddWithValue("@FinalAmount", _cashier.FinalAmount);
                            cmd.Parameters.AddWithValue("@OfficePK", _cashier.OfficePK);
                            cmd.Parameters.AddWithValue("@DepartmentPK", _cashier.DepartmentPK);
                            cmd.Parameters.AddWithValue("@AgentPK", _cashier.AgentPK);
                            cmd.Parameters.AddWithValue("@CounterpartPK", _cashier.CounterpartPK);
                            cmd.Parameters.AddWithValue("@ConsigneePK", _cashier.ConsigneePK);
                            cmd.Parameters.AddWithValue("@InstrumentPK", _cashier.InstrumentPK);
                            cmd.Parameters.AddWithValue("@JournalNo", _cashier.JournalNo);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _cashier.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _cashier.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                            cmd.ExecuteNonQuery();
                        }

                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update Cashier set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@lastupdate where CashierPK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _cashier.CashierPK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _cashier.EntryUsersID);
                            cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
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
                                cmd.CommandText = "Update Cashier set Notes=@Notes,PeriodPK=@PeriodPK,ValueDate=@ValueDate,Type=@Type," +
                                    "Reference=@Reference,Description=@Description,DebitCredit=@DebitCredit,DebitCurrencyPK=@DebitCurrencyPK,CreditCurrencyPK=@CreditCurrencyPK, " +
                                    "DebitCashRefPK=@DebitCashRefPK,CreditCashRefPK=@CreditCashRefPK,DebitAccountPK=@DebitAccountPK,CreditAccountPK=@CreditAccountPK, " +
                                    "Debit=@Debit,Credit=@Credit,DebitCurrencyRate=@DebitCurrencyRate,CreditCurrencyRate=@CreditCurrencyRate, " +
                                    "BaseDebit=@BaseDebit,BaseCredit=@BaseCredit,PercentAmount=@PercentAmount,FinalAmount=@FinalAmount,OfficePK=@OfficePK,DepartmentPK=@DepartmentPK, " +
                                    "AgentPK=@AgentPK,CounterpartPK=@CounterpartPK,ConsigneePK=@ConsigneePK,InstrumentPK=@InstrumentPK,JournalNo=@JournalNo, " +
                                    "UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@lastupdate " +
                                    "where CashierPK = @PK and historyPK = @HistoryPK";

                                cmd.Parameters.AddWithValue("@HistoryPK", _cashier.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _cashier.CashierPK);
                                cmd.Parameters.AddWithValue("@Notes", _cashier.Notes);
                                cmd.Parameters.AddWithValue("@PeriodPK", _cashier.PeriodPK);
                                cmd.Parameters.AddWithValue("@ValueDate", _cashier.ValueDate);
                                cmd.Parameters.AddWithValue("@Type", _cashier.Type);
                                cmd.Parameters.AddWithValue("@Reference", _cashier.Reference);
                                cmd.Parameters.AddWithValue("@Description", _cashier.Description);
                                cmd.Parameters.AddWithValue("@DebitCredit", _cashier.DebitCredit);
                                cmd.Parameters.AddWithValue("@DebitCurrencyPK", _cashier.DebitCurrencyPK);
                                cmd.Parameters.AddWithValue("@CreditCurrencyPK", _cashier.CreditCurrencyPK);
                                cmd.Parameters.AddWithValue("@DebitCashRefPK", _cashier.DebitCashRefPK);
                                cmd.Parameters.AddWithValue("@CreditCashRefPK", _cashier.CreditCashRefPK);
                                cmd.Parameters.AddWithValue("@DebitAccountPK", _cashier.DebitAccountPK);
                                cmd.Parameters.AddWithValue("@CreditAccountPK", _cashier.CreditAccountPK);
                                cmd.Parameters.AddWithValue("@Debit", _cashier.Debit);
                                cmd.Parameters.AddWithValue("@Credit", _cashier.Credit);
                                cmd.Parameters.AddWithValue("@DebitCurrencyRate", _cashier.DebitCurrencyRate);
                                cmd.Parameters.AddWithValue("@CreditCurrencyRate", _cashier.CreditCurrencyRate);
                                cmd.Parameters.AddWithValue("@BaseDebit", _cashier.BaseDebit);
                                cmd.Parameters.AddWithValue("@BaseCredit", _cashier.BaseCredit);
                                cmd.Parameters.AddWithValue("@PercentAmount", _cashier.PercentAmount);
                                cmd.Parameters.AddWithValue("@FinalAmount", _cashier.FinalAmount);
                                cmd.Parameters.AddWithValue("@OfficePK", _cashier.OfficePK);
                                cmd.Parameters.AddWithValue("@DepartmentPK", _cashier.DepartmentPK);
                                cmd.Parameters.AddWithValue("@AgentPK", _cashier.AgentPK);
                                cmd.Parameters.AddWithValue("@CounterpartPK", _cashier.CounterpartPK);
                                cmd.Parameters.AddWithValue("@ConsigneePK", _cashier.ConsigneePK);
                                cmd.Parameters.AddWithValue("@InstrumentPK", _cashier.InstrumentPK);
                                cmd.Parameters.AddWithValue("@JournalNo", _cashier.JournalNo);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _cashier.EntryUsersID);
                                cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                                cmd.ExecuteNonQuery();
                            }
                            return 0;
                        }
                        else
                        {
                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                _newHisPK = _host.Get_NewHistoryPK(_cashier.CashierPK, "Cashier");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                 "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                              "From Cashier where CashierPK =@PK and historyPK = @HistoryPK ";
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _cashier.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _cashier.CashierPK);
                                cmd.Parameters.AddWithValue("@Notes", _cashier.Notes);
                                cmd.Parameters.AddWithValue("@PeriodPK", _cashier.PeriodPK);
                                cmd.Parameters.AddWithValue("@ValueDate", _cashier.ValueDate);
                                cmd.Parameters.AddWithValue("@Type", _cashier.Type);
                                cmd.Parameters.AddWithValue("@Reference", _cashier.Reference);
                                cmd.Parameters.AddWithValue("@Description", _cashier.Description);
                                cmd.Parameters.AddWithValue("@DebitCredit", _cashier.DebitCredit);
                                cmd.Parameters.AddWithValue("@DebitCurrencyPK", _cashier.DebitCurrencyPK);
                                cmd.Parameters.AddWithValue("@CreditCurrencyPK", _cashier.CreditCurrencyPK);
                                cmd.Parameters.AddWithValue("@DebitCashRefPK", _cashier.DebitCashRefPK);
                                cmd.Parameters.AddWithValue("@CreditCashRefPK", _cashier.CreditCashRefPK);
                                cmd.Parameters.AddWithValue("@DebitAccountPK", _cashier.DebitAccountPK);
                                cmd.Parameters.AddWithValue("@CreditAccountPK", _cashier.CreditAccountPK);
                                cmd.Parameters.AddWithValue("@Debit", _cashier.Debit);
                                cmd.Parameters.AddWithValue("@Credit", _cashier.Credit);
                                cmd.Parameters.AddWithValue("@DebitCurrencyRate", _cashier.DebitCurrencyRate);
                                cmd.Parameters.AddWithValue("@CreditCurrencyRate", _cashier.CreditCurrencyRate);
                                cmd.Parameters.AddWithValue("@BaseDebit", _cashier.BaseDebit);
                                cmd.Parameters.AddWithValue("@BaseCredit", _cashier.BaseCredit);
                                cmd.Parameters.AddWithValue("@PercentAmount", _cashier.PercentAmount);
                                cmd.Parameters.AddWithValue("@FinalAmount", _cashier.FinalAmount);
                                cmd.Parameters.AddWithValue("@OfficePK", _cashier.OfficePK);
                                cmd.Parameters.AddWithValue("@DepartmentPK", _cashier.DepartmentPK);
                                cmd.Parameters.AddWithValue("@AgentPK", _cashier.AgentPK);
                                cmd.Parameters.AddWithValue("@CounterpartPK", _cashier.CounterpartPK);
                                cmd.Parameters.AddWithValue("@ConsigneePK", _cashier.ConsigneePK);
                                cmd.Parameters.AddWithValue("@InstrumentPK", _cashier.InstrumentPK);
                                cmd.Parameters.AddWithValue("@JournalNo", _cashier.JournalNo);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _cashier.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                                cmd.ExecuteNonQuery();

                            }
                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update Cashier set status= 4,Notes=@Notes," +
                                    "LastUpdate=@lastupdate where CashierPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _cashier.Notes);
                                cmd.Parameters.AddWithValue("@PK", _cashier.CashierPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _cashier.HistoryPK);
                                cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
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

        public List<Cashier> Cashier_SelectFromTo(int _status, string _type, DateTime _dateFrom, DateTime _dateTo)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<Cashier> L_Cashier = new List<Cashier>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {

                            cmd.CommandText = " Select case when A.status=1 then 'PENDING' else Case When A.status = 2 then 'APPROVED' else Case when A.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,cast(substring(reference,1,charindex('/',reference,1) - 1) as integer) RefNo,A.Reference,B.ID PeriodID,C.ID OfficeID,D.ID DepartmentID,E.ID AgentID,F.ID CounterpartID, " +
                            " G.ID DebitCurrencyID,H.ID CreditCurrencyID,I.ID DebitCashRefID,I.Name DebitCashRefName, J.ID CreditCashRefID,J.Name CreditCashRefName,    " +
                            " K.ID DebitAccountID,K.Name DebitAccountName,L.ID CreditAccountID,L.Name CreditAccountName, M.ID ConsigneeID, N.ID InstrumentID,N.Name InstrumentName,   " +
                            " A.* From Cashier A Left join     " +
                            " Period B on A.PeriodPK = B.PeriodPK and B.Status = 2 Left join     " +
                            " Office C on A.OfficePK = C.OfficePK and C.Status = 2 Left join     " +
                            " Department D on A.DepartmentPK = D.DepartmentPK and D.Status = 2 Left join     " +
                            " Agent E on A.AgentPK = E.AgentPK and E.Status = 2 Left join     " +
                            " Counterpart F on A.CounterpartPK = F.CounterpartPK and F.Status = 2 Left join    " +
                            " Currency G on A.DebitCurrencyPK = G.CurrencyPK and G.Status = 2 Left join     " +
                            " Currency H on A.CreditCurrencyPK = H.CurrencyPK and H.Status = 2 Left join     " +
                            " CashRef I on A.DebitCashRefPK = I.CashRefPK and I.Status = 2 Left join     " +
                            " Cashref J on A.CreditCashRefPK = J.CashRefPK and J.Status = 2 Left join     " +
                            " Account K on A.DebitAccountPK = K.AccountPK and K.Status = 2 Left join     " +
                            " Account L on A.CreditAccountPK = L.AccountPK and L.Status = 2 Left join    " +
                            " Consignee M on A.ConsigneePK = M.ConsigneePK and M.Status = 2 Left join   " +
                            " Instrument N on A.InstrumentPK = N.InstrumentPK and N.Status = 2  " +
                            " Where  A.Status = @Status and A.Type= @Type and ValueDate between @DateFrom and @DateTo order By RefNo  ";
                            cmd.Parameters.AddWithValue("@Status", _status);
                            cmd.Parameters.AddWithValue("@Type", _type);
                            cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                            cmd.Parameters.AddWithValue("@DateTo", _dateTo);

                        }
                        else
                        {
                            cmd.CommandText = " Select case when A.status=1 then 'PENDING' else Case When A.status = 2 then 'APPROVED' else Case when A.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,cast(substring(reference,1,charindex('/',reference,1) - 1) as integer) RefNo,A.Reference,B.ID PeriodID,C.ID OfficeID,D.ID DepartmentID,E.ID AgentID,F.ID CounterpartID, " +
                            " G.ID DebitCurrencyID,H.ID CreditCurrencyID,I.ID DebitCashRefID,I.Name DebitCashRefName, J.ID CreditCashRefID,J.Name CreditCashRefName,    " +
                            " K.ID DebitAccountID,K.Name DebitAccountName,L.ID CreditAccountID,L.Name CreditAccountName, M.ID ConsigneeID, N.ID InstrumentID,N.Name InstrumentName,   " +
                            " A.* From Cashier A Left join     " +
                            " Period B on A.PeriodPK = B.PeriodPK and B.Status = 2 Left join     " +
                            " Office C on A.OfficePK = C.OfficePK and C.Status = 2 Left join     " +
                            " Department D on A.DepartmentPK = D.DepartmentPK and D.Status = 2 Left join     " +
                            " Agent E on A.AgentPK = E.AgentPK and E.Status = 2 Left join     " +
                            " Counterpart F on A.CounterpartPK = F.CounterpartPK and F.Status = 2 Left join    " +
                            " Currency G on A.DebitCurrencyPK = G.CurrencyPK and G.Status = 2 Left join     " +
                            " Currency H on A.CreditCurrencyPK = H.CurrencyPK and H.Status = 2 Left join     " +
                            " CashRef I on A.DebitCashRefPK = I.CashRefPK and I.Status = 2 Left join     " +
                            " Cashref J on A.CreditCashRefPK = J.CashRefPK and J.Status = 2 Left join     " +
                            " Account K on A.DebitAccountPK = K.AccountPK and K.Status = 2 Left join     " +
                            " Account L on A.CreditAccountPK = L.AccountPK and L.Status = 2 Left join    " +
                            " Consignee M on A.ConsigneePK = M.ConsigneePK and M.Status = 2 Left join   " +
                            " Instrument N on A.InstrumentPK = N.InstrumentPK and N.Status = 2  " +
                            " Where   A.Type= @Type and ValueDate between @DateFrom and @DateTo order By RefNo  ";
                            cmd.Parameters.AddWithValue("@Type", _type);
                            cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                            cmd.Parameters.AddWithValue("@DateTo", _dateTo);
                        }

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_Cashier.Add(setCashier(dr));
                                }
                            }
                            return L_Cashier;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }


        public void Cashier_ApproveBySelected(string _usersID, string _permissionID, DateTime _dateFrom, DateTime _dateTo, string _type, ParamCashierBySelected _paramCashierBySelected)
        {
            try
            {
                string paramCashierPK = "";
                if (!_host.findString(_paramCashierBySelected.stringCashierPaymentSelected.ToLower(), "0", ",") && !string.IsNullOrEmpty(_paramCashierBySelected.stringCashierPaymentSelected))
                {
                    paramCashierPK = " And CashierPK in (" + _paramCashierBySelected.stringCashierPaymentSelected + ") ";
                }
                else
                {
                    paramCashierPK = " And CashierPK in (0) ";
                }

                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"
                    Declare @IPAddress nvarchar(50) select @IPAddress = IPAddress from Users where ID = @UsersID and Status = 2      
                        Insert into Activity(Time,PermissionID,ObjectTable,ObjectTablePK,Status,Message,UsersID,IPAddress,LastUpdate)      
                     Select @Time,@PermissionID,'Cashier',CashierPK,1,'Approve by Selected Data',@UsersID,@IPAddress,@Time  from Cashier where ValueDate between @DateFrom and @DateTo and Status = 1 " + paramCashierPK + @" and type = @type    
                     update Cashier set status = 2,ApprovedUsersID = @UsersID,ApprovedTime = @Time,LastUpdate=@Time 
                     where CashierPK in ( Select CashierPK from Cashier where ValueDate between @DateFrom and @DateTo and Status = 1 " + paramCashierPK + @" and type = @type)   and Status = 1 " + paramCashierPK + @" and type = @type   
                     Update Cashier set status= 3,VoidUsersID=@UsersID,VoidTime=@Time,LastUpdate=@Time  where CashierPK in (Select CashierPK from Cashier where ValueDate between @DateFrom and @DateTo and Status = 4 " + paramCashierPK + @" and type = @type)    
                     and Status = 4 " + paramCashierPK + @" and type = @type
     
 

                        ";
                        cmd.Parameters.AddWithValue("@PermissionID", _permissionID);
                        cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _dateTo);
                        cmd.Parameters.AddWithValue("@UsersID", _usersID);
                        cmd.Parameters.AddWithValue("@Time", _datetimeNow);
                        cmd.Parameters.AddWithValue("@type", _type);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public bool Add_CashierValidate(string _valueDate, string _cashRef, string _ref, string _type)
        {

            try
            {


                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmdR = DbCon.CreateCommand())
                    {
                        string _cashRefColumn = "";
                        if (_type == "CP" || _type == "CBT")
                        {
                            _cashRefColumn = "CreditCashRefPK";
                        }
                        if (_type == "CR")
                        {
                            _cashRefColumn = "DebitCashRefPK";
                        }
                        if (_type == "CAR" || _type == "CAP")
                        {
                            _cashRefColumn = "DebitAccountPK";
                        }


                        cmdR.CommandText =


                            @"
                            if exists (Select distinct(Reference) Reference From Cashier where status = 1 and Posted = 0 and Reference = @Reference and Type = @Type)    
                            Begin    
                   
	                            if exists (select * from Cashier where Reference = @Reference and ValueDate = @ValueDate and " + _cashRefColumn + @"= @CashRef)    
		                            BEGIN 
                                     SELECT 1 Result 
                                    END 
                                    ELSE 
                                    BEGIN 
                                      SELECT 0 Result
                                    END   
                            End    
                            Else    
                            Begin    
	                              SELECT 1 Result 
                            End ";




                        cmdR.Parameters.AddWithValue("@Reference", _ref);
                        cmdR.Parameters.AddWithValue("@Type", _type);
                        cmdR.Parameters.AddWithValue("@ValueDate", _valueDate);
                        cmdR.Parameters.AddWithValue("@CashRef", _cashRef);
                        using (SqlDataReader drR = cmdR.ExecuteReader())
                        {

                            if (drR.HasRows)
                            {
                                drR.Read();
                                return Convert.ToBoolean(drR["Result"]);
                            }
                            else
                            {
                                return false;
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


        public void TrxPortfolio_PostingBySelected(string _usersID, DateTime _dateFrom, DateTime _dateTo)
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
                         	declare @PValueDate datetime
                            declare @PSettledDate datetime
                            declare @PMaturityDate datetime
                            declare @PPeriodPK int
                            declare @PInstrumentPK int
                            declare @PCounterpartPK int
                            declare @PTrxType int
                            declare @PReference nvarchar(50)
                            declare @InstrumentID nvarchar(50)
                            declare @PInstrumentTypePK int
                            declare @InstrumentType int
                            declare @PCashRefPK int
                            declare @PVolume numeric (22,4)
                            declare @PBrokerageFeeAmount numeric (22,4)
                            declare @PLevyAmount numeric (22,4)
                            declare @PKPEIAmount numeric (22,4)                 
                            declare @PVATAmount numeric (22,4)
                            declare @PWHTAmount numeric (22,4)
                            declare @POTCAmount numeric (22,4)
                            declare @PIncomeTaxSellAmount numeric (22,4)
                            declare @PIncomeTaxInterestAmount numeric (22,4)
                            declare @PIncomeTaxGainAmount numeric (22,4)
                            declare @PNetAmount numeric (22,4)
                            declare @PRealisedAmount numeric (22,4)
                            declare @PInterestAmount numeric (22,4)

                            declare @InvestmentAcc int
                            declare @PayablePurchaseAcc int
                            declare @ReceivableSaleAcc int
                            declare @CommissionAcc int
                            declare @LevyAcc int
                            declare @VatAcc int
                            declare @WhtAcc int
                            declare @CashAtBankAcc int
                            declare @RealisedAcc int
                            declare @IncomeSaleTaxAcc int
                            declare @InterestRecAcc int
                            declare @journalPK int
                            declare @TrxPortfolioPK int
                            declare @TotalFeeAcc int
                            declare @RealisedAccReksadana int
 
 
                            declare @PAmount numeric(22,4)
                            declare @FPayablePurchaseAmount numeric (22,4)
                        
   
                            declare @CashierPK int
                            declare @CashierID int
	                        Declare @CurReference nvarchar(100)  

                            declare @AccountBalance table
                            (
	                            Balance numeric(19,4)
                            )

                             declare @RealisedInstrument table
                            (
	                            TrxPortfolioPK int,
	                            InstrumentPK int,
	                            ValueDate datetime,
	                            SettledDate datetime,
	                            Realised numeric(19,4)
                            )

                            declare @LastRealisedInstrument table
                            (
	                            TrxPortfolioPK int,
	                            InstrumentPK int,
	                            ValueDate datetime,
		                        SettledDate datetime,
	                            LastRealised numeric(19,4)
                            )

                            declare @FlagRealisedInstrument table
                            (
	                            InstrumentPK int,
	                            ValueDate datetime,
		                        SettledDate datetime,
	                            Realised numeric(19,4),
	                            Amount numeric(19,4),
	                            Flag int
                            )


                            declare @CValuedate datetime
                            declare @BalancePriorYear numeric(19,4)
                            declare @Realised numeric(19,4)
                            declare @LastUnrealised numeric(19,4)
                            declare @UnrealisedMutualFund int
                            declare @RealisedMutualFund int
                            declare @RevalMutualFund int
                            declare @CostAmount numeric(19,4)	
                            declare @LastRealised numeric(19,4)
                            declare @CSettledDate datetime

                            declare @CTrxPortfolioPK int
                            declare @CInstrumentPK int

                            declare @UnrealisedEquity int
                            declare @RevalEquity int
                            declare @RealisedEquity int

                            declare @BalanceCurrYear numeric(19,4)
                            declare @RealisedAmount numeric(19,4)

                        Declare A Cursor For                  
	                        Select TrxPortfolioPK,ValueDate,PeriodPK,Reference,TrxType,CounterpartPK,InstrumentPK,                   
	                        CashRefPK,InterestAmount,SettledDate,Volume,Amount,                  
	                        BrokerageFeeAmount,LevyAmount,KPEIAmount,VATAmount,WHTAmount,OTCAmount,IncomeTaxSellAmount,                  
	                        IncomeTaxInterestAmount,IncomeTaxGainAmount,NetAmount,InstrumentTypePK,RealisedAmount,MaturityDate            
	                        From TrxPortfolio         
	                        Where status = 2 and Posted = 0 and Revised = 0 and ValueDate between @DateFrom and @DateTo and Selected = 1              
	                        Open A                  
	                        Fetch Next From A                  
	                        Into @TrxPortfolioPK,@PValueDate,@PPeriodPK,@PReference,@PTrxType,@PCounterpartPK,@PInstrumentPK,
	                        @PCashRefPK,@PInterestAmount,@PSettledDate,@PVolume,@PAmount,@PBrokerageFeeAmount,@PLevyAmount,@PKPEIAmount,                  
	                        @PVATAmount,@PWHTAmount,@POTCAmount,@PIncomeTaxSellAmount,
	                        @PIncomeTaxInterestAmount,@PIncomeTaxGainAmount,@PNetAmount,@PInstrumentTypePK,@PRealisedAmount,@PMaturityDate                 
                        While @@FETCH_STATUS = 0                  
                        Begin                  
	                        Select @InstrumentID = ID From Instrument where Status = 2 and InstrumentPK = @PInstrumentPK 
		      				               
	                        -- A1. BUY EQUITY --                 
	                        if @PTrxType = 1 and @PInstrumentTypePK = 1                  
	                        BEGIN     

                                exec getJournalReference @PSettledDate,'CP',@CurReference out
             
		                        Select @InstrumentType =  InstrumentTypePK From Instrument where InstrumentPK = @PInstrumentPK and Status = 2                  
		                        IF @InstrumentType in (1,4,16)                  
		                        BEGIN                  
			                        Select @InvestmentAcc = InvInEquity,@PayablePurchaseAcc = APPurchaseEquity From AccountingSetup where Status in (1,2)   
		                        END                                
		                        Select @CommissionAcc = BrokerageFee,@LevyAcc = JSXLevyFee,@VatAcc = VATFee,@WhtAcc = WHTFee 
		                        From AccountingSetup where Status in (1,2)                  
		                        Select @CashAtBankAcc = AccountPK From CashRef where Status in (1,2) and CashRefPK = @PCashRefPK                  

		                        -- Setup Account kelar diatas, Next masukin ke Fund Journal                  
		                        select @JournalPK = isnull(max(JournalPK),0) + 1 From Journal                   

		                        -- T0                  
		                        INSERT INTO [Journal]([JournalPK],[HistoryPK],[Status],[Notes],[PeriodPK],[ValueDate],[Type],[TrxNo],[TrxName],[Reference]                  
		                        ,[Description],[Posted],[EntryUsersID],[EntryTime],[ApprovedUsersID],[ApprovedTime],[PostedBy],[PostedTime],[LastUpdate])                  

			                        Select  @JournalPK, 1,2,'',@PPeriodPK,@PValueDate,4,TrxPortfolioPK,'TRANSACTION',                  
			                        @CurReference,'T0 EQUITY BUY: ' + @InstrumentID ,1,EntryusersID,EntryTime,@UsersID,@LastUpdate,@UsersID,@LastUpdate,@LastUpdate from TrxPortfolio where  TrxPortfolioPK = @TrxPortfolioPK                 

		                        INSERT INTO [JournalDetail]([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[InstrumentPK],                
		                        [DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])                  

			                        Select  @JournalPK,1,1,2,@InvestmentAcc,1,@PInstrumentPK,'T0 EQUITY BUY: ' + @InstrumentID,'D',@PAmount,                   
			                        @PAmount,0,1,@PAmount,0,@LastUpdate From Account Where AccountPK = @InvestmentAcc and Status = 2                  


		                        INSERT INTO [JournalDetail]([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[InstrumentPK],                
		                        [DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])                   

			                        Select @JournalPK,2,1,2,@PayablePurchaseAcc,1,@PInstrumentPK,'T0 EQUITY BUY: ' + @InstrumentID,'C',@PNetAmount,                   
			                        0,@PNetAmount,1,0,@PNetAmount,@LastUpdate From Account Where AccountPK = @PayablePurchaseAcc and Status = 2                  

		                        INSERT INTO [JournalDetail]([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[InstrumentPK],                
		                        [DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])                    

			                        Select @JournalPK,3,1,2,@CommissionAcc,1,@PInstrumentPK,'T0 EQUITY BUY: ' + @InstrumentID,'D',@PBrokerageFeeAmount,                   
			                        @PBrokerageFeeAmount,0,1,@PBrokerageFeeAmount,0,@LastUpdate From Account Where AccountPK = @CommissionAcc and Status = 2                  

		                        set @PLevyAmount = @PLevyAmount + @PKPeIAmount                

		                        INSERT INTO [JournalDetail]([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[InstrumentPK],                
		                        [DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])                   

			                        Select @JournalPK,4,1,2,@LevyAcc,1,@PInstrumentPK,'T0 EQUITY BUY: ' + @InstrumentID,'D',@PLevyAmount,                   
			                        @PLevyAmount,0,1,@PLevyAmount,0,@LastUpdate From Account Where AccountPK = @LevyAcc and Status = 2                  

		                        INSERT INTO [JournalDetail]([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[InstrumentPK],                
		                        [DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])  

			                        Select @JournalPK,5,1,2,@VatAcc,1,@PInstrumentPK,'T0 EQUITY BUY: ' + @InstrumentID,'D',@PVATAmount,                   
			                        @PVATAmount,0,1,@PVATAmount,0,@LastUpdate From Account Where AccountPK = @VatAcc and Status = 2                  

		                        INSERT INTO [JournalDetail]([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[InstrumentPK],                
		                        [DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])                  

			                        Select @JournalPK,6,1,2,@WhtAcc,1,@PInstrumentPK,'T0 EQUITY BUY: ' + @InstrumentID,'C',@PWHTAmount,                   
			                        0,@PWHTAmount,1,0,@PWHTAmount,@LastUpdate From Account Where AccountPK = @WhtAcc and Status = 2                  

		                        -- T Settled  
		                        select @CashierPK = isnull(max(CashierPK),0) + 1 From Cashier                   
          
		                        

		                        INSERT INTO [dbo].[Cashier]  
		                        ([CashierPK],[HistoryPK],[Status],[PeriodPK],[ValueDate],[Type],[Reference],[Description],[DebitCredit],[DebitCurrencyPK],  
		                         [CreditCurrencyPK],[DebitCashRefPK],[CreditCashRefPK],[DebitAccountPK],[CreditAccountPK],[Debit],  
		                         [Credit],[DebitCurrencyRate],[CreditCurrencyRate],[BaseDebit],[BaseCredit],[PercentAmount],[FinalAmount],[OfficePK],[DepartmentPK],[AgentPK],[CounterpartPK],[ConsigneePK],[InstrumentPK],[JournalNo],[TrxNo],
		                         [EntryUsersID],[EntryTime],[ApprovedUsersID],[ApprovedTime],LastUpdate)

		                        select @CashierPK,1,1,dbo.FgetPeriod(@PSettledDate), @PSettledDate,'CP',@CurReference,'T-SETTLED EQUITY BUY: ' + @InstrumentID,'D',1,
		                        1,0,@PCashRefPK,@PayablePurchaseAcc,@CashAtBankAcc,@PNetAmount,@PNetAmount,1,1,@PNetAmount,@PNetAmount,100,@PNetAmount,1,1,0,@PCounterpartPK,0,@PInstrumentPK,0,@TrxPortfolioPK,
		                        EntryusersID,EntryTime,@UsersID,@LastUpdate,@LastUpdate from TrxPortfolio where  TrxPortfolioPK = @TrxPortfolioPK     
		    
		                        --INSERT INTO [JournalDetail]([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[InstrumentPK],                
		                        --[DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])                 

		                        --	Select  @JournalPK,1,1,2,@PayablePurchaseAcc,1,@PInstrumentPK,'T0 EQUITY BUY: ' + @InstrumentID,'D',@PNetAmount,@PNetAmount,0,1,@PNetAmount,0,@LastUpdate   
		                        --	From Account Where AccountPK = @PayablePurchaseAcc and Status = 2                  

		                        --INSERT INTO [JournalDetail]([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[InstrumentPK],                
		                        --[DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])                 

		                        --	Select @JournalPK,2,1,2,@CashAtBankAcc,1,@PInstrumentPK,'T0 EQUITY BUY: ' + @InstrumentID,'C',@PNetAmount,                   
		                        --	0,@PNetAmount,1,0,@PNetAmount,@LastUpdate From Account Where AccountPK = @CashAtBankAcc and Status = 2                  
	                        END 
               
	                       -- A2. SELL EQUITY --              
	                        if @PTrxType = 2 and @PInstrumentTypePK = 1                  
	                        BEGIN      
                                exec getJournalReference @PSettledDate,'CR',@CurReference out

								Select @CommissionAcc = BrokerageFee,@LevyAcc = JSXLevyFee,@VatAcc = VATFee,@WhtAcc = WHTFee ,@IncomeSaleTaxAcc = IncomeTaxArt23, @TotalFeeAcc = CadanganEquity,
								 @UnrealisedEquity = UnrealisedEquity, @RealisedEquity = RealisedEquity,@RevalEquity = CadanganEquity
		                        From AccountingSetup where Status in (1,2)       
		                        Select @CashAtBankAcc = AccountPK From CashRef where Status in (1,2) and CashRefPK = @PCashRefPK                  
		                        Select @InstrumentID = ID From Instrument where Status = 2 and InstrumentPK = @PInstrumentPK    

								delete from @AccountBalance

								Declare B Cursor For
								select distinct (SELECT DATEADD (dd, -1, DATEADD(yy, DATEDIFF(yy, 0, ValueDate) +1, 0))) ValueDate from journal 
								where Posted = 1 and Reversed = 0 and Description <> 'PERIOD CLOSING' and ValueDate < DATEADD(yy, DATEDIFF(yy, 0, @PSettledDate), 0)
								order by ValueDate asc
								Open B
								Fetch Next From B
								INTO @CValueDate

								While @@FETCH_STATUS = 0  
								Begin

				
								insert into @AccountBalance
								select dbo.FGetAccountBalanceByDateByParentByInstrumentPK(@CValueDate,@UnrealisedEquity,@PInstrumentPK)

								Fetch Next From B
								into @CValueDate
								End	
								Close B
								Deallocate B


								select @BalancePriorYear = sum(Balance) from @AccountBalance

                                select @BalanceCurrYear = dbo.FGetAccountBalanceByDateByParentByInstrumentPK(DATEADD(yy, DATEDIFF(yy, 0, @CValuedate), 0),@UnrealisedEquity,@PInstrumentPK)

								select @RealisedAmount = (Price - dbo.FGetLastAvgFromInvestment_Acc(ValueDate,@PInstrumentPK)) * Volume from TrxPortfolio 
								where instrumentPk = @PInstrumentPK and ValueDate = @PValueDate
								and status = 2  and Revised = 0


								delete from @RealisedInstrument

								insert into @RealisedInstrument
								select TrxPortfolioPK,InstrumentPK,ValueDate,SettledDate,(Price - dbo.FGetLastAvgFromInvestment_Acc(ValueDate,@PInstrumentPK)) * Volume from TrxPortfolio 
								where instrumentPk = @PInstrumentPK and SettledDate  between DATEADD(yy, DATEDIFF(yy, 0, @PSettledDate), 0) and @PSettledDate 
								and status = 2  and Revised = 0 and TrxType = 2
								order by SettledDate 


								delete from @LastRealisedInstrument

								Declare C Cursor For
								select TrxPortfolioPK,InstrumentPK,ValueDate,SettledDate from @RealisedInstrument
								order by ValueDate,SettledDate 
								Open C
								Fetch Next From C
								INTO @CTrxPortfolioPK,@CInstrumentPK,@CValueDate,@CSettledDate

								While @@FETCH_STATUS = 0  
								Begin

                               

								insert into @LastRealisedInstrument
								select @CTrxPortfolioPK,@CInstrumentPK,@CValueDate,@CSettledDate,sum(isnull(Realised,0)) from @RealisedInstrument
								where  InstrumentPK = @CInstrumentPK and SettledDate <= @CSettledDate

	
								Fetch Next From C
								into @CTrxPortfolioPK,@CInstrumentPK,@CValueDate,@CSettledDate
								End	
								Close C
								Deallocate C


								delete from @FlagRealisedInstrument

								insert into @FlagRealisedInstrument
								select A.InstrumentPK,A.ValueDate,A.SettledDate,Realised,@BalancePriorYear - LastRealised,case when @BalancePriorYear - LastRealised < 0 then 1 else 0 end from @LastRealisedInstrument A
								left join @RealisedInstrument B on A.InstrumentPK = B.InstrumentPK and A.SettledDate = B.SettledDate and A.TrxPortfolioPK = B.TrxPortfolioPK

								select @Realised = Realised, @LastUnrealised = Amount from @FlagRealisedInstrument where ValueDate = @PValueDate
								select @CostAmount = dbo.FGetLastAvgFromInvestment_Acc(@PSettledDate,@PInstrumentPK) * @PVolume


            
		                        Select @InstrumentType =  InstrumentTypePK From Instrument where InstrumentPK = @PInstrumentPK and Status = 2                  
		                        IF @InstrumentType in (1,4,16)                  
		                        BEGIN                 
			                        Select @InvestmentAcc = InvInEquity,@ReceivableSaleAcc = ARSellEquity,@RealisedAcc = RealisedEquity From AccountingSetup where Status in (1,2)                  
		                        END                  
 
		                                   

		                        -- Setup Account kelar diatas, Next masukin ke Fund Journal                  
		                        select @JournalPK = isnull(max(JournalPK),0) + 1 From Journal                   

		                        -- T0                  
		                        INSERT INTO [Journal]([JournalPK],[HistoryPK],[Status],[Notes],[PeriodPK],[ValueDate],[Type],[TrxNo],[TrxName],[Reference]                  
		                        ,[Description],[Posted],[EntryUsersID],[EntryTime],[ApprovedUsersID],[ApprovedTime],[PostedBy],[PostedTime],[LastUpdate])                   

			                        Select  @JournalPK, 1,2,'',@PPeriodPK,@PValueDate,4,TrxPortfolioPK,'TRANSACTION',                    
			                        @CurReference,'T0 EQUITY SELL: ' + @InstrumentID ,1,EntryusersID,EntryTime,@UsersID,@LastUpdate,@UsersID,@LastUpdate,@LastUpdate from TrxPortfolio where  TrxPortfolioPK = @TrxPortfolioPK                 

		                        INSERT INTO [JournalDetail]([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[InstrumentPK],                
		                        [DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])                  

			                        Select  @JournalPK,1,1,2,@CommissionAcc,1,@PInstrumentPK,'T0 EQUITY SELL: ' + @InstrumentID,'D',@PBrokerageFeeAmount,                   
			                        @PBrokerageFeeAmount,0,1,@PBrokerageFeeAmount,0,@LastUpdate From Account Where AccountPK = @CommissionAcc and Status = 2                  

		                        INSERT INTO [JournalDetail]([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[InstrumentPK],                
		                        [DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])                  

			                        Select  @JournalPK,2,1,2,@LevyAcc,1,@PInstrumentPK,'T0 EQUITY SELL: ' + @InstrumentID,'D',@PLevyAmount + @PKPEIAmount,                   
			                        @PLevyAmount + @PKPEIAmount,0,1,@PLevyAmount + @PKPEIAmount,0,@LastUpdate From Account Where AccountPK = @LevyAcc and Status = 2                  

		                        INSERT INTO [JournalDetail]([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[InstrumentPK],                
		                        [DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])                

			                        Select  @JournalPK,3,1,2,@VatAcc,1,@PInstrumentPK,'T0 EQUITY SELL: ' + @InstrumentID,'D',@PVATAmount,                   
			                        @PVATAmount,0,1,@PVATAmount,0,@LastUpdate From Account Where AccountPK = @VatAcc and Status = 2                  

		                        INSERT INTO [JournalDetail]([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[InstrumentPK],                
		                        [DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])                 

			                        Select  @JournalPK,4,1,2,@IncomeSaleTaxAcc,1,@PInstrumentPK,'T0 EQUITY SELL: ' + @InstrumentID,'D',@PIncomeTaxSellAmount,                   
			                        @PIncomeTaxSellAmount,0,1,@PIncomeTaxSellAmount,0,@LastUpdate From Account Where AccountPK = @IncomeSaleTaxAcc and Status = 2                  

		                        INSERT INTO [JournalDetail]([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[InstrumentPK],                
		                        [DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])                   

			                        Select @JournalPK,5,1,2,@WhtAcc,1,@PInstrumentPK,'T0 EQUITY SELL: ' + @InstrumentID,'C',@PWHTAmount,                   
			                        0,@PWHTAmount,1,0,@PWHTAmount,@LastUpdate From Account Where AccountPK = @WhtAcc and Status = 2                  

			
				                INSERT INTO [JournalDetail]([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[InstrumentPK],                
				                [DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])                              

				                    Select @JournalPK,6,1,2,@ReceivableSaleAcc,1,@PInstrumentPK,'T0 EQUITY SELL: ' + @InstrumentID,'D',isnull(@PNetAmount,0),                   
                                    isnull(@PNetAmount,0),0,1,isnull(@PNetAmount,0),0,@LastUpdate From Account Where AccountPK = @ReceivableSaleAcc and Status = 2   


		                        Declare @SellArAmount numeric(22,6)   
                                Declare @TotalFeeAmount numeric(22,6)            
		                        Set @SellArAmount = @PAmount - isnull(@PBrokerageFeeAmount,0) - isnull(@PLevyAmount,0) - isnull(@PKPEIAmount,0) - isnull(@PVATAmount,0) - isnull(@PIncomeTaxSellAmount,0) + isnull(@PWHTAmount,0)                
                                Set @TotalFeeAmount = isnull(@PBrokerageFeeAmount,0) + isnull(@PLevyAmount,0) + isnull(@PKPEIAmount,0) + isnull(@PVATAmount,0) + isnull(@PIncomeTaxSellAmount,0)       

		                        Declare @InvestmentShareAmount numeric(22,6)                          

		                        -- Gain Realised
		                        if @PAmount - @CostAmount >= 0              
		                        Begin            
                                        INSERT INTO [JournalDetail]([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[InstrumentPK],                
		                                [DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])                   

										  Select @JournalPK,7,1,2,@RevalEquity,1,@PInstrumentPK,'T0 EQUITY SELL: ' + @InstrumentID,'C',abs(@PAmount - @CostAmount),                   
				                            0,abs(@PAmount - @CostAmount),1,0,abs(@PAmount - @CostAmount),@LastUpdate From Account Where AccountPK = @RevalEquity and Status = 2                  

            
				                        INSERT INTO [JournalDetail]([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[InstrumentPK],                
				                        [DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate]) 

				                         	Select  @JournalPK,8,1,2,@InvestmentAcc,1,@PInstrumentPK,'T0 EQUITY SELL: ' + @InstrumentID,'C',@PAmount - abs(@PAmount - @CostAmount),                   
			                                0,@PAmount- abs(@PAmount - @CostAmount),1,0,@PAmount - abs(@PAmount - @CostAmount),@LastUpdate From Account Where AccountPK = @InvestmentAcc and Status = 2        
         
		                        End       
		                        ELSE            
		                        begin     

    
                                        INSERT INTO [JournalDetail]([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[InstrumentPK],                
		                                [DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])                   

			                                Select  @JournalPK,7,1,2,@RevalEquity,1,@PInstrumentPK,'T0 EQUITY SELL: ' + @InstrumentID,'D',abs(@PAmount - @CostAmount),                   
			                                abs(@PAmount - @CostAmount),0,1,abs(@PAmount - @CostAmount),0,@LastUpdate From Account Where AccountPK = @RevalEquity and Status = 2                   
     

				                        INSERT INTO [JournalDetail]([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[InstrumentPK],                
				                        [DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])               

				                        Select  @JournalPK,8,1,2,@InvestmentAcc,1,@PInstrumentPK,'T0 EQUITY SELL: ' + @InstrumentID,'C',@PAmount + abs(@PAmount - @CostAmount),                   
				                        0,@PAmount + abs(@PAmount - @CostAmount),1,0,@PAmount + abs(@PAmount - @CostAmount),@LastUpdate From Account Where AccountPK = @InvestmentAcc and Status = 2                 
			 
       
		                        end  

                                IF(@RealisedAmount < 0)
                                BEGIN
                                    INSERT INTO [JournalDetail]([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[InstrumentPK],                
	                                [DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])                   

	                                Select  @JournalPK,9,1,2,@RealisedEquity,1,@PInstrumentPK,'T0 EQUITY SELL: ' + @InstrumentID,'D',abs(@RealisedAmount),                   
	                                abs(@RealisedAmount),0,1,abs(@RealisedAmount),0,@LastUpdate From Account Where AccountPK = @RealisedEquity and Status = 2                   

  
	                                INSERT INTO [JournalDetail]([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[InstrumentPK],                
	                                [DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate]) 

	                                Select @JournalPK,10,1,2,@UnrealisedEquity,1,@PInstrumentPK,'T0 EQUITY SELL: ' + @InstrumentID,'C',abs(@RealisedAmount),                   
	                                0,abs(@RealisedAmount),1,0,abs(@RealisedAmount),@LastUpdate From Account Where AccountPK = @UnrealisedEquity and Status = 2   
                                END
                                ELSE IF EXISTS(select * from @FlagRealisedInstrument where SettledDate < @PSettledDate and Flag = 1) and @Lastunrealised < 0
                                BEGIN
			                        --select @Realised,'Realised'
	                                INSERT INTO [JournalDetail]([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[InstrumentPK],                
	                                [DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])                   

	                                Select  @JournalPK,9,1,2,@UnrealisedEquity,1,@PInstrumentPK,'T0 EQUITY SELL: ' + @InstrumentID,'D',abs(@Realised),                   
	                                abs(@Realised),0,1,abs(@Realised),0,@LastUpdate From Account Where AccountPK = @UnrealisedEquity and Status = 2                   

  
	                                INSERT INTO [JournalDetail]([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[InstrumentPK],                
	                                [DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate]) 

	                                Select @JournalPK,10,1,2,@RealisedEquity,1,@PInstrumentPK,'T0 EQUITY SELL: ' + @InstrumentID,'C',abs(@Realised),                   
	                                0,abs(@Realised),1,0,abs(@Realised),@LastUpdate From Account Where AccountPK = @RealisedEquity and Status = 2   
                                END 
                                ELSE IF EXISTS(select * from @FlagRealisedInstrument where SettledDate <= @PSettledDate and Flag = 1) and @Lastunrealised < 0
                                BEGIN

			
			                        --select @LastUnrealised,'UnRealised'
	                                INSERT INTO [JournalDetail]([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[InstrumentPK],                
	                                [DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])                   

	                                Select  @JournalPK,9,1,2,@UnrealisedEquity,1,@PInstrumentPK,'T0 EQUITY SELL: ' + @InstrumentID,'D',abs(@LastUnrealised),                   
	                                abs(@LastUnrealised),0,1,abs(@LastUnrealised),0,@LastUpdate From Account Where AccountPK = @UnrealisedEquity and Status = 2                   

  
	                                INSERT INTO [JournalDetail]([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[InstrumentPK],                
	                                [DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate]) 

	                                Select @JournalPK,10,1,2,@RealisedEquity,1,@PInstrumentPK,'T0 EQUITY SELL: ' + @InstrumentID,'C',abs(@LastUnrealised),                   
	                                0,abs(@LastUnrealised),1,0,abs(@LastUnrealised),@LastUpdate From Account Where AccountPK = @RealisedEquity and Status = 2     


                                END
            

		                        -- T SETTLED         
                                select @CashierPK = isnull(max(CashierPK),0) + 1 From Cashier                   
          
                     

                                INSERT INTO [dbo].[Cashier]  
                                ([CashierPK],[HistoryPK],[Status],[PeriodPK],[ValueDate],[Type],[Reference],[Description],[DebitCredit],[DebitCurrencyPK],  
                                [CreditCurrencyPK],[DebitCashRefPK],[CreditCashRefPK],[DebitAccountPK],[CreditAccountPK],[Debit],  
                                [Credit],[DebitCurrencyRate],[CreditCurrencyRate],[BaseDebit],[BaseCredit],[PercentAmount],[FinalAmount],[OfficePK],[DepartmentPK],[AgentPK],[CounterpartPK],[ConsigneePK],[InstrumentPK],[JournalNo],[TrxNo],
                                [EntryUsersID],[EntryTime],[ApprovedUsersID],[ApprovedTime],LastUpdate)


                                select @CashierPK,1,1,dbo.FgetPeriod(@PSettledDate), @PSettledDate,'CR',@CurReference,'T-SETTLED EQUITY SELL: ' + @InstrumentID,'C',1,
                                1,@PCashRefPK,0,@CashAtBankAcc,@ReceivableSaleAcc,@SellArAmount,@PNetAmount,1,1,@PNetAmount,@PNetAmount,100,@PNetAmount,1,1,0,@PCounterpartPK,0,@PInstrumentPK,0,@TrxPortfolioPK,
                                EntryusersID,EntryTime,@UsersID,@LastUpdate,@LastUpdate from TrxPortfolio where  TrxPortfolioPK = @TrxPortfolioPK  
     
		                      
	                        END  
	
	                        -- BUY BOND
	
	                        if @PTrxType = 1 and @PInstrumentTypePK = 2                 
	                        BEGIN  
                                exec getJournalReference @PSettledDate,'CP',@CurReference out

		                        declare @BondDayAccrued int                 
		                        -- 2 = G-BOND                  
		                        -- 3 = C-BOND                  
		                        Select @InstrumentType =  InstrumentTypePK From Instrument where InstrumentPK = @PInstrumentPK and Status = 2                                           

		                        if @InstrumentType not in (1,4,5,6,16)          
		                        BEGIN            	      
			                        Select @InvestmentAcc = InvInBond,@InterestRecAcc = InterestPurchaseBond,@PayablePurchaseAcc = APPurchaseBond, 
			                        @BondDayAccrued = InterestReceivableBond,@WHTAcc = IncomeTaxArt23
			                        From AccountingSetup where Status in (1,2)      
		                        END                  
                  
		                        Select @CashAtBankAcc = AccountPK From CashRef where Status in (1,2) and CashRefPK = @PCashRefPK                  
	
		                        -- Setup Account kelar diatas, Next masukin ke Fund Journal 
		                        select @JournalPK = isnull(max(JournalPK),0) + 1 From Journal                

		                        -- T0                  
		                        INSERT INTO [Journal]([JournalPK],[HistoryPK],[Status],[Notes],[PeriodPK],[ValueDate],[Type],[TrxNo],[TrxName],[Reference]                  
		                        ,[Description],[Posted],[EntryUsersID],[EntryTime],[ApprovedUsersID],[ApprovedTime],[PostedBy],[PostedTime],[LastUpdate])                     

			                        Select  @JournalPK, 1,2,'',@PPeriodPK,@PValueDate,4,TrxPortfolioPK,'TRANSACTION',                    
			                        @CurReference,'T0 BOND BUY: ' + @InstrumentID ,1,EntryusersID,EntryTime,@UsersID,@LastUpdate,@UsersID,@LastUpdate,@LastUpdate from TrxPortfolio where  TrxPortfolioPK = @TrxPortfolioPK                 

				                        INSERT INTO [JournalDetail]([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[InstrumentPK],                
				                        [DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])               

				                        Select  @JournalPK,1,1,2,@InvestmentAcc,1,@PInstrumentPK,'T0 BOND BUY: ' + @InstrumentID,'D',@PAmount,                   
				                        @PAmount,0,1,@PAmount,0,@LastUpdate From Account Where AccountPK = @InvestmentAcc and Status = 2                   

				                        INSERT INTO [JournalDetail]([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[InstrumentPK],                
				                        [DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])               

				                        Select  @JournalPK,2,1,2,@InterestRecAcc,1,@PInstrumentPK,'T0 BOND BUY: ' + @InstrumentID,'D',@PInterestAmount,                   
				                        @PInterestAmount,0,1,@PInterestAmount,0,@LastUpdate From Account Where AccountPK = @InterestRecAcc and Status = 2                   

		                                set @FPayablePurchaseAmount = @PAmount + isnull(@PInterestAmount,0) -  isnull(@PIncomeTaxInterestAmount,0) - isnull(@PIncomeTaxGainAmount,0)               

				                        INSERT INTO [JournalDetail]([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[InstrumentPK],                
				                        [DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])                     

				                        Select @JournalPK,3,1,2,@PayablePurchaseAcc,1,@PInstrumentPK,'T0 BOND BUY: ' + @InstrumentID,'C',@FPayablePurchaseAmount,                   
				                        0,@FPayablePurchaseAmount,1,0,@FPayablePurchaseAmount,@LastUpdate From Account Where AccountPK = @PayablePurchaseAcc and Status = 2                  

				                        INSERT INTO [JournalDetail]([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[InstrumentPK],                
				                        [DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])                     

				                        Select @JournalPK,4,1,2,@WhtAcc,1,@PInstrumentPK,'T0 BOND BUY: ' + @InstrumentID,'C',isnull(@PIncomeTaxInterestAmount,0) + isnull(@PIncomeTaxGainAmount,0),                   
				                        0,isnull(@PIncomeTaxInterestAmount,0) + isnull(@PIncomeTaxGainAmount,0) ,1,0,isnull(@PIncomeTaxInterestAmount,0) + isnull(@PIncomeTaxGainAmount,0),@LastUpdate From Account Where AccountPK = @WhtAcc and Status = 2                  


		                        -- T Settled  
                                select @CashierPK = isnull(max(CashierPK),0) + 1 From Cashier                   
          


                                INSERT INTO [dbo].[Cashier]  
                                ([CashierPK],[HistoryPK],[Status],[PeriodPK],[ValueDate],[Type],[Reference],[Description],[DebitCredit],[DebitCurrencyPK],  
                                [CreditCurrencyPK],[DebitCashRefPK],[CreditCashRefPK],[DebitAccountPK],[CreditAccountPK],[Debit],  
                                [Credit],[DebitCurrencyRate],[CreditCurrencyRate],[BaseDebit],[BaseCredit],[PercentAmount],[FinalAmount],[OfficePK],[DepartmentPK],[AgentPK],[CounterpartPK],[ConsigneePK],[InstrumentPK],[JournalNo],[TrxNo],
                                [EntryUsersID],[EntryTime],[ApprovedUsersID],[ApprovedTime],LastUpdate)

                                select @CashierPK,1,1,dbo.FgetPeriod(@PSettledDate), @PSettledDate,'CP',@CurReference,'T-SETTLED BOND BUY: ' + @InstrumentID,'D',1,
                                1,0,@PCashRefPK,@PayablePurchaseAcc,@CashAtBankAcc,@FPayablePurchaseAmount,@FPayablePurchaseAmount,1,1,@FPayablePurchaseAmount,@FPayablePurchaseAmount,100,@FPayablePurchaseAmount,1,1,0,@PCounterpartPK,0,@PInstrumentPK,0,@TrxPortfolioPK,
                                EntryusersID,EntryTime,@UsersID,@LastUpdate,@LastUpdate from TrxPortfolio where  TrxPortfolioPK = @TrxPortfolioPK    

              
		                        --select @JournalPK = isnull(max(JournalPK),0) + 1 From Journal 
	
		                        --INSERT INTO [Journal]([JournalPK],[HistoryPK],[Status],[Notes],[PeriodPK],[ValueDate],[Type],[TrxNo],[TrxName],[Reference]                  
		                        --,[Description],[Posted],[EntryUsersID],[EntryTime],[ApprovedUsersID],[ApprovedTime],[PostedBy],[PostedTime],[LastUpdate])                

			                    --    Select  @JournalPK, 1,2,'',@PPeriodPK,@PSettledDate,4,TrxPortfolioPK,'TRANSACTION',                  
			                    --    @PReference,'T-Settled BOND BUY: ' + @InstrumentID ,1,EntryusersID,EntryTime,@UsersID,@LastUpdate,@UsersID,@LastUpdate,@LastUpdate from TrxPortfolio where  TrxPortfolioPK = @TrxPortfolioPK                 

				                        --INSERT INTO [JournalDetail]([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[InstrumentPK],                
				                        --[DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])                

				                        --Select  @JournalPK,1,1,2,@PayablePurchaseAcc,1,@PInstrumentPK,'T0 BOND BUY: ' + @InstrumentID,'D',@FPayablePurchaseAmount,                   
				                        --@FPayablePurchaseAmount,0,1,@FPayablePurchaseAmount,0,@LastUpdate From Account Where AccountPK = @PayablePurchaseAcc and Status = 2                  

				                        --INSERT INTO [JournalDetail]([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[InstrumentPK],                
				                        --[DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])                 

				                        --Select @JournalPK,2,1,2,@CashAtBankAcc,1,@PInstrumentPK,'T0 BOND BUY: ' + @InstrumentID,'C',@FPayablePurchaseAmount,                   
				                        --0,@FPayablePurchaseAmount,1,0,@FPayablePurchaseAmount,@LastUpdate From Account Where AccountPK = @CashAtBankAcc and Status = 2                

				                        --INSERT INTO [JournalDetail]([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[InstrumentPK],                
				                        --[DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])                

				                        --Select  @JournalPK,3,1,2,@BondDayAccrued,1,@PInstrumentPK,'T-Settled BOND BUY: ' + @InstrumentID,'D',@PInterestAmount,                   
				                        --@PInterestAmount,0,1,@PInterestAmount,0,@LastUpdate From Account Where AccountPK = @BondDayAccrued and Status = 2                  

				                        --INSERT INTO [JournalDetail]([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[InstrumentPK],                
				                        --[DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])               

				                        --Select @JournalPK,4,1,2,@InterestRecAcc,1,@PInstrumentPK,'T-Settled BOND BUY: ' + @InstrumentID,'C',@PInterestAmount,                   
				                        --0,@PInterestAmount,1,0,@PInterestAmount,@LastUpdate From Account Where AccountPK = @InterestRecAcc and Status = 2                  
	                        END   
	  
	                        --SELL BOND
           
	                        if @PTrxType = 2 and @PInstrumentTypePK = 2                  
	                        BEGIN        
                                exec getJournalReference @PSettledDate,'CR',@CurReference out   
		                        -- 2 = G-BOND                  
		                        -- 3 = C-BOND                  
		                        Select @InstrumentType =  InstrumentTypePK From Instrument where InstrumentPK = @PInstrumentPK and Status = 2                                           

		                        if @InstrumentType not in (1,4,5,6,16)             
		                        BEGIN            	      
			                        Select @InvestmentAcc = InvInBond,@InterestRecAcc = InterestPurchaseBond,@ReceivableSaleAcc = ARSellBond, 
			                        @BondDayAccrued = InterestReceivableBond,@WHTAcc = IncomeTaxArt23,@RealisedAcc=RealisedBond
			                        From AccountingSetup where Status in (1,2)      
		                        END                  
    
		                        Select @CashAtBankAcc = AccountPK From CashRef where Status in (1,2)    and CashRefPK = @PCashRefPK                  
		                        Select @InstrumentID = ID From Instrument where Status = 2 and InstrumentPK = @PInstrumentPK                  

		                        -- Setup Account kelar diatas, Next masukin ke Fund Journal                  
		                        select @JournalPK = isnull(max(JournalPK),0) + 1 From Journal                   

		                        -- T0                  
		                        INSERT INTO [Journal]([JournalPK],[HistoryPK],[Status],[Notes],[PeriodPK],[ValueDate],[Type],[TrxNo],[TrxName],[Reference]                  
		                        ,[Description],[Posted],[EntryUsersID],[EntryTime],[ApprovedUsersID],[ApprovedTime],[PostedBy],[PostedTime],[LastUpdate])                   

			                        Select  @JournalPK, 1,2,'',@PPeriodPK,@PValueDate,4,TrxPortfolioPK,'TRANSACTION',                   
			                        @CurReference,'T0 BOND SELL: ' + @InstrumentID ,1,EntryusersID,EntryTime,@UsersID,@LastUpdate,@UsersID,@LastUpdate,@LastUpdate from TrxPortfolio where  TrxPortfolioPK = @TrxPortfolioPK                 

		                        INSERT INTO [JournalDetail]([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[InstrumentPK],                
		                        [DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])                   

			                        Select  @JournalPK,1,1,2,@ReceivableSaleAcc,1,@PInstrumentPK,'T0 BOND SELL: ' + @InstrumentID,'D',@PNetAmount,                   
			                        @PNetAmount,0,1,@PNetAmount,0,@LastUpdate From Account Where AccountPK = @ReceivableSaleAcc and Status = 2          
         
                                INSERT INTO [JournalDetail]([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[InstrumentPK],                
                                [DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])                              

				                    Select @JournalPK,2,1,2,@InterestRecAcc,1,@PInstrumentPK,'T0 BOND SELL: ' + @InstrumentID,'C',@PInterestAmount,                   
				                    0,@PInterestAmount,1,0,@PInterestAmount,@LastUpdate From Account Where AccountPK = @InterestRecAcc and Status = 2   

                                INSERT INTO [JournalDetail]([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[InstrumentPK],                
                                [DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])                              

				                    Select @JournalPK,3,1,2,@WHTAcc,1,@PInstrumentPK,'T0 BOND SELL: ' + @InstrumentID,'D',isnull(@PIncomeTaxInterestAmount,0) + isnull(@PIncomeTaxGainAmount,0),                   
				                    isnull(@PIncomeTaxInterestAmount,0) + isnull(@PIncomeTaxGainAmount,0),0,1,isnull(@PIncomeTaxInterestAmount,0) + isnull(@PIncomeTaxGainAmount,0),0,@LastUpdate From Account Where AccountPK = @WHTAcc and Status = 2  
         

		                        -- Gain Realised
		                        if @PRealisedAmount > 0              
		                        Begin              
				                        INSERT INTO [JournalDetail]([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[InstrumentPK],                
				                        [DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate]) 

				                        Select @JournalPK,4,1,2,@RealisedAcc,1,@PInstrumentPK,'T0 BOND SELL: ' + @InstrumentID,'C',abs(@PRealisedAmount),                   
				                        0,abs(@PRealisedAmount),1,0,abs(@PRealisedAmount),@LastUpdate From Account Where AccountPK = @RealisedAcc and Status = 2                  

			                        set  @InvestmentShareAmount = @PAmount - abs(@PRealisedAmount)              
		                        End       
     
		                        -- Loss Realised
		                        if @PRealisedAmount <= 0              
		                        begin              
				                        INSERT INTO [JournalDetail]([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[InstrumentPK],                
				                        [DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])               

				                        Select  @JournalPK,4,1,2,@RealisedAcc,1,@PInstrumentPK,'T0 BOND SELL: ' + @InstrumentID,'D',abs(@PRealisedAmount),                   
				                        abs(@PRealisedAmount),0,1,abs(@PRealisedAmount),0,@LastUpdate From Account Where AccountPK = @RealisedAcc and Status = 2                 
			 
			                        set  @InvestmentShareAmount = @PAmount + abs(@PRealisedAmount)              
		                        end       

				                                    

				                        INSERT INTO [JournalDetail]([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[InstrumentPK],                
				                        [DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])                              

				                        Select @JournalPK,5,1,2,@InvestmentAcc,1,@PInstrumentPK,'T0 BOND SELL: ' + @InstrumentID,'C',@InvestmentShareAmount,                   
				                        0,@InvestmentShareAmount,1,0,@InvestmentShareAmount,@LastUpdate From Account Where AccountPK = @InvestmentAcc and Status = 2   


		                        -- T SETTLED      
                                select @CashierPK = isnull(max(CashierPK),0) + 1 From Cashier                   
          
                

                                INSERT INTO [dbo].[Cashier]  
                                ([CashierPK],[HistoryPK],[Status],[PeriodPK],[ValueDate],[Type],[Reference],[Description],[DebitCredit],[DebitCurrencyPK],  
                                [CreditCurrencyPK],[DebitCashRefPK],[CreditCashRefPK],[DebitAccountPK],[CreditAccountPK],[Debit],  
                                [Credit],[DebitCurrencyRate],[CreditCurrencyRate],[BaseDebit],[BaseCredit],[PercentAmount],[FinalAmount],[OfficePK],[DepartmentPK],[AgentPK],[CounterpartPK],[ConsigneePK],[InstrumentPK],[JournalNo],[TrxNo],
                                [EntryUsersID],[EntryTime],[ApprovedUsersID],[ApprovedTime],LastUpdate)

                                select @CashierPK,1,1,dbo.FgetPeriod(@PSettledDate), @PSettledDate,'CR',@CurReference,'T-SETTLED BOND SELL: ' + @InstrumentID,'C',1,
                                1,@PCashRefPK,0,@CashAtBankAcc,@ReceivableSaleAcc,@PNetAmount,@PNetAmount,1,1,@PNetAmount,@PNetAmount,100,@PNetAmount,1,1,0,@PCounterpartPK,0,@PInstrumentPK,0,@TrxPortfolioPK,
                                EntryusersID,EntryTime,@UsersID,@LastUpdate,@LastUpdate from TrxPortfolio where  TrxPortfolioPK = @TrxPortfolioPK  

        
		                       -- select @JournalPK = isnull(max(JournalPK),0) + 1 From Journal                   

		                       -- INSERT INTO [Journal]([JournalPK],[HistoryPK],[Status],[Notes],[PeriodPK],[ValueDate],[Type],[TrxNo],[TrxName],[Reference]                  
		                       -- ,[Description],[Posted],[EntryUsersID],[EntryTime],[ApprovedUsersID],[ApprovedTime],[PostedBy],[PostedTime],[LastUpdate])                   

			                  --      Select  @JournalPK, 1,2,'',@PPeriodPK,@PSettledDate,4,TrxPortfolioPK,'TRANSACTION',                    
			                  --     @PReference,'T-SETTLED BOND SELL: ' + @InstrumentID ,1,EntryusersID,EntryTime,@UsersID,@LastUpdate,@UsersID,@LastUpdate,@LastUpdate from TrxPortfolio where  TrxPortfolioPK = @TrxPortfolioPK                 

				              --          INSERT INTO [JournalDetail]([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[InstrumentPK],                
				              --          [DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])                              

				              --          Select  @JournalPK,1,1,2,@CashAtBankAcc,1,@PInstrumentPK,'T0 BOND SELL: ' + @InstrumentID,'D',@SellArAmount,                   
				              --          @SellArAmount,0,1,@SellArAmount,0,@LastUpdate From Account Where AccountPK = @CashAtBankAcc and Status = 2                

				              --          INSERT INTO [JournalDetail]([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[InstrumentPK],                
				              --          [DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])                              

				              --          Select @JournalPK,2,1,2,@ReceivableSaleAcc,1,@PInstrumentPK,'T0 BOND SELL: ' + @InstrumentID,'C',@SellArAmount,                   
				              --          0,@SellArAmount,1,0,@SellArAmount,@LastUpdate From Account Where AccountPK = @ReceivableSaleAcc and Status = 2                      
	                        END   

	                    -- A5.PLACEMENT DEPOSITO --                 
                            if (@PTrxType = 1 and @PInstrumentTypePK = 3) or (@PTrxType = 3 and @PInstrumentTypePK = 3)  

                            BEGIN                  
                                Select @InstrumentType =  InstrumentTypePK From Instrument where InstrumentPK = @PInstrumentPK and Status = 2                           

                                if @InstrumentType = 5                  
                                BEGIN                  
                                    Select @InvestmentAcc = InvInTD From AccountingSetup where Status in (1,2)                   
                                    Select @CashAtBankAcc = AccountPK From CashRef where Status in (1,2)    and CashRefPK = @PCashRefPK                  
                                END    


                                select @CashierPK = isnull(max(CashierPK),0) + 1 From Cashier   

		                        exec getJournalReference @PSettledDate,'CP',@CurReference out

		                        INSERT INTO [dbo].[Cashier]  
		                        ([CashierPK],[HistoryPK],[Status],[PeriodPK],[ValueDate],[Type],[Reference],[Description],[DebitCredit],[DebitCurrencyPK],  
		                         [CreditCurrencyPK],[DebitCashRefPK],[CreditCashRefPK],[DebitAccountPK],[CreditAccountPK],[Debit],  
		                         [Credit],[DebitCurrencyRate],[CreditCurrencyRate],[BaseDebit],[BaseCredit],[PercentAmount],[FinalAmount],[OfficePK],[DepartmentPK],[AgentPK],[CounterpartPK],[ConsigneePK],[InstrumentPK],[JournalNo],[TrxNo],
		                         [EntryUsersID],[EntryTime],[ApprovedUsersID],[ApprovedTime],LastUpdate)

		                        select @CashierPK,1,1,@PPeriodPK, @PValueDate,'CP',@CurReference,'PLACEMENT DEPOSITO : ' + @InstrumentID,'D',1,
		                        1,0,@PCashRefPK,@InvestmentAcc,@CashAtBankAcc,@PAmount,@PAmount,1,1,@PAmount,@PAmount,100,@PAmount,1,1,0,@PCounterpartPK,0,@PInstrumentPK,0,@TrxPortfolioPK,
		                        EntryusersID,EntryTime,@UsersID,@LastUpdate,@LastUpdate from TrxPortfolio where  TrxPortfolioPK = @TrxPortfolioPK     
		     
                                --MATURE DEPOSITO
                                
                                select @CashierPK = isnull(max(CashierPK),0) + 1 From Cashier   

                                exec getJournalReference @PSettledDate,'CR',@CurReference out

                                INSERT INTO [dbo].[Cashier]  
                                ([CashierPK],[HistoryPK],[Status],[PeriodPK],[ValueDate],[Type],[Reference],[Description],[DebitCredit],[DebitCurrencyPK],  
                                [CreditCurrencyPK],[DebitCashRefPK],[CreditCashRefPK],[DebitAccountPK],[CreditAccountPK],[Debit],  
                                [Credit],[DebitCurrencyRate],[CreditCurrencyRate],[BaseDebit],[BaseCredit],[PercentAmount],[FinalAmount],[OfficePK],[DepartmentPK],[AgentPK],[CounterpartPK],[ConsigneePK],[InstrumentPK],[JournalNo],[TrxNo],
                                [EntryUsersID],[EntryTime],[ApprovedUsersID],[ApprovedTime],LastUpdate)

                                select @CashierPK,1,1,@PPeriodPK, @PMaturityDate,'CR',@CurReference,'DEPOSIT MATURE: ' + @InstrumentID,'C',1,
                                1,@PCashRefPK,0,@CashAtBankAcc,@InvestmentAcc,@PAmount,@PAmount,1,1,@PAmount,@PAmount,100,@PAmount,1,1,0,@PCounterpartPK,0,@PInstrumentPK,0,@TrxPortfolioPK,
                                EntryusersID,EntryTime,@UsersID,@LastUpdate,@LastUpdate from TrxPortfolio where  TrxPortfolioPK = @TrxPortfolioPK  
       
                               

                                ---- Setup Account kelar diatas, Next masukin ke Fund Journal 
                                --Select @JournalPK = ISNULL(JournalPK,0) + 1 from Journal                  

                                ---- T0                
		                        --INSERT INTO [Journal]([JournalPK],[HistoryPK],[Status],[Notes],[PeriodPK],[ValueDate],[Type],[TrxNo],[TrxName],[Reference]                  
		                        --,[Description],[Posted],[EntryUsersID],[EntryTime],[ApprovedUsersID],[ApprovedTime],[PostedBy],[PostedTime],[LastUpdate])                          

                                --    Select  @JournalPK, 1,2,'',@PPeriodPK,@PValueDate,4,TrxPortfolioPK,'TRANSACTION',                  
                                --    @PReference,'T0 DEPOSIT BUY: ' + @InstrumentID ,1,EntryusersID,EntryTime,@UsersID,@LastUpdate,@UsersID,@LastUpdate,@LastUpdate from TrxPortfolio where  TrxPortfolioPK = @TrxPortfolioPK                 

                                --        INSERT INTO [JournalDetail]([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[InstrumentPK],                
                                --        [DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])                           

                                --        Select  @JournalPK,1,1,2,@InvestmentAcc,1,@PInstrumentPK,'T0 DEPOSIT BUY: ' + @InstrumentID,'D',@PAmount,                   
                                --        @PAmount,0,1,@PAmount,0,@LastUpdate From Account Where AccountPK = @InvestmentAcc and Status = 2                   

                                --        INSERT INTO [JournalDetail]([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[InstrumentPK],                
                                --        [DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])                         

                                --        Select @JournalPK,2,1,2,@CashAtBankAcc,1,@PInstrumentPK,'T0 DEPOSIT BUY: ' + @InstrumentID,'C',@PAmount,                   
                                --        0,@PAmount,1,0,@PAmount,@LastUpdate From Account Where AccountPK = @CashAtBankAcc and Status = 2                  
                            END                  

                            -- A6. LIQUIDATE DEPOSITO --            
                            if @PTrxType = 2 and @PInstrumentTypePK = 3                  
                            BEGIN                  
                                Select @InstrumentType =  InstrumentTypePK From Instrument where InstrumentPK = @PInstrumentPK and Status = 2                  

                                if @InstrumentType = 5                  
                                BEGIN                  
                                    Select @InvestmentAcc = InvInTD From AccountingSetup where Status in (1,2)                  
                                    Select @CashAtBankAcc = AccountPK From CashRef where Status in (1,2)    and CashRefPK = @PCashRefPK                  
                                END                  

                                select @CashierPK = isnull(max(CashierPK),0) + 1 From Cashier                   

                                exec getJournalReference @PSettledDate,'CR',@CurReference out

                                INSERT INTO [dbo].[Cashier]  
                                ([CashierPK],[HistoryPK],[Status],[PeriodPK],[ValueDate],[Type],[Reference],[Description],[DebitCredit],[DebitCurrencyPK],  
                                [CreditCurrencyPK],[DebitCashRefPK],[CreditCashRefPK],[DebitAccountPK],[CreditAccountPK],[Debit],  
                                [Credit],[DebitCurrencyRate],[CreditCurrencyRate],[BaseDebit],[BaseCredit],[PercentAmount],[FinalAmount],[OfficePK],[DepartmentPK],[AgentPK],[CounterpartPK],[ConsigneePK],[InstrumentPK],[JournalNo],[TrxNo],
                                [EntryUsersID],[EntryTime],[ApprovedUsersID],[ApprovedTime],LastUpdate)

                                select @CashierPK,1,1,@PPeriodPK, @PValueDate,'CR',@CurReference,'DEPOSIT LIQUIDATE: ' + @InstrumentID,'C',1,
                                1,@PCashRefPK,0,@CashAtBankAcc,@InvestmentAcc,@PAmount,@PAmount,1,1,@PAmount,@PAmount,100,@PAmount,1,1,0,@PCounterpartPK,0,@PInstrumentPK,0,@TrxPortfolioPK,
                                EntryusersID,EntryTime,@UsersID,@LastUpdate,@LastUpdate from TrxPortfolio where  TrxPortfolioPK = @TrxPortfolioPK  


                                select @CashierPK = isnull(max(CashierPK),0) + 1 From Cashier                   

		                        exec getJournalReference @PSettledDate,'CP',@CurReference out

		                        INSERT INTO [dbo].[Cashier]  
		                        ([CashierPK],[HistoryPK],[Status],[PeriodPK],[ValueDate],[Type],[Reference],[Description],[DebitCredit],[DebitCurrencyPK],  
		                         [CreditCurrencyPK],[DebitCashRefPK],[CreditCashRefPK],[DebitAccountPK],[CreditAccountPK],[Debit],  
		                         [Credit],[DebitCurrencyRate],[CreditCurrencyRate],[BaseDebit],[BaseCredit],[PercentAmount],[FinalAmount],[OfficePK],[DepartmentPK],[AgentPK],[CounterpartPK],[ConsigneePK],[InstrumentPK],[JournalNo],[TrxNo],
		                         [EntryUsersID],[EntryTime],[ApprovedUsersID],[ApprovedTime],LastUpdate)

		                        select @CashierPK,1,1,@PPeriodPK, @PMaturityDate,'CP',@CurReference,'DEPOSIT MATURE : ' + @InstrumentID,'D',1,
		                        1,0,@PCashRefPK,@InvestmentAcc,@CashAtBankAcc,@PAmount,@PAmount,1,1,@PAmount,@PAmount,100,@PAmount,1,1,0,@PCounterpartPK,0,@PInstrumentPK,0,@TrxPortfolioPK,
		                        EntryusersID,EntryTime,@UsersID,@LastUpdate,@LastUpdate from TrxPortfolio where  TrxPortfolioPK = @TrxPortfolioPK     
		     
                                ---- Setup Account kelar diatas, Next masukin ke Fund Journal 
                                --Select @JournalPK = ISNULL(JournalPK,0) + 1 from Journal                  

                                ---- T0              
		                        --INSERT INTO [Journal]([JournalPK],[HistoryPK],[Status],[Notes],[PeriodPK],[ValueDate],[Type],[TrxNo],[TrxName],[Reference]                  
		                        --,[Description],[Posted],[EntryUsersID],[EntryTime],[ApprovedUsersID],[ApprovedTime],[PostedBy],[PostedTime],[LastUpdate])                                

                                --    Select  @JournalPK, 1,2,'',@PPeriodPK,@PValueDate,4,TrxPortfolioPK,'TRANSACTION',                  
                                --    @PReference,'T0 DEPOSIT LIQUIDATE: ' + @InstrumentID ,1,EntryusersID,EntryTime,@UsersID,@LastUpdate,@UsersID,@LastUpdate,@LastUpdate from TrxPortfolio where  TrxPortfolioPK = @TrxPortfolioPK                 

                                --        INSERT INTO [JournalDetail]([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[InstrumentPK],                
                                --        [DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])                            

                                --        Select  @JournalPK,1,1,2,@CashAtBankAcc,1,@PInstrumentPK,'T0 DEPOSIT LIQUIDATE: ' + @InstrumentID,'D',@PAmount,                   
                                --        @PAmount,0,1,@PAmount,0,@LastUpdate From Account Where AccountPK = @InvestmentAcc and Status = 2                   

                                --        INSERT INTO [JournalDetail]([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[InstrumentPK],                
                                --        [DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])                       

                                --        Select @JournalPK,2,1,2,@InvestmentAcc,1,@PInstrumentPK,'T0 DEPOSIT LIQUIDATE: ' + @InstrumentID,'C',@PAmount,                   
                                --        0,@PAmount,1,0,@PAmount,@LastUpdate From Account Where AccountPK = @CashAtBankAcc and Status = 2                  
                            END    

                            -- A7. ROLLOVER DEPOSITO --         
                            --if @PTrxType = 3 and @PInstrumentTypePK = 3                  
                            --BEGIN                  
                                --Select @InstrumentType =  InstrumentTypePK From Instrument where InstrumentPK = @PInstrumentPK and Status = 2                  

                                --if @InstrumentType = 5                  
                                --BEGIN                  
                                --    Select @InvestmentAcc = InvInTD From AccountingSetup where Status in (1,2)                   
                                --    Select @CashAtBankAcc = AccountPK From CashRef where Status in (1,2)    and CashRefPK = @PCashRefPK                  
                                --END                  

                                -- Setup Account kelar diatas, Next masukin ke Fund Journal 
                                --Select @JournalPK = ISNULL(JournalPK,0) + 1 from Journal                  

                                -- T0              
		                        --INSERT INTO [Journal]([JournalPK],[HistoryPK],[Status],[Notes],[PeriodPK],[ValueDate],[Type],[TrxNo],[TrxName],[Reference]                  
		                        --,[Description],[Posted],[EntryUsersID],[EntryTime],[ApprovedUsersID],[ApprovedTime],[PostedBy],[PostedTime],[LastUpdate])                                

                                --    Select  @JournalPK, 1,2,'',@PPeriodPK,@PValueDate,4,TrxPortfolioPK,'TRANSACTION',                   
                                --    @PReference,'T0 DEPOSIT ROLLOVER: ' + @InstrumentID ,1,EntryusersID,EntryTime,@UsersID,@LastUpdate,@UsersID,@LastUpdate,@LastUpdate from TrxPortfolio where  TrxPortfolioPK = @TrxPortfolioPK                 

                                --        INSERT INTO [JournalDetail]([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[InstrumentPK],                
                                --        [DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])                            

                                --        Select  @JournalPK,1,1,2,@CashAtBankAcc,1,@PInstrumentPK,'T0 DEPOSIT ROLLOVER: ' + @InstrumentID,'D',@PAmount,                   
                                --        @PAmount,0,1,@PAmount,0,@LastUpdate From Account Where AccountPK = @InvestmentAcc and Status = 2                   

                                --        INSERT INTO [JournalDetail]([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[InstrumentPK],                
                                --        [DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])                       

                                --        Select @JournalPK,2,1,2,@InvestmentAcc,1,@PInstrumentPK,'T0 DEPOSIT ROLLOVER: ' + @InstrumentID,'C',@PAmount,                   
                                --        0,@PAmount,1,0,@PAmount,@LastUpdate From Account Where AccountPK = @CashAtBankAcc and Status = 2  


                                --        INSERT INTO [JournalDetail]([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[InstrumentPK],                
                                --        [DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])                           

                                --        Select  @JournalPK,3,1,2,@InvestmentAcc,1,@PInstrumentPK,'T0 DEPOSIT ROLLOVER: ' + @InstrumentID,'D',@PAmount,                   
                                --        @PAmount,0,1,@PAmount,0,@LastUpdate From Account Where AccountPK = @InvestmentAcc and Status = 2                   

                                --        INSERT INTO [JournalDetail]([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[InstrumentPK],                
                                --        [DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])                         

                                --        Select @JournalPK,4,1,2,@CashAtBankAcc,1,@PInstrumentPK,'T0 DEPOSIT ROLLOVER: ' + @InstrumentID,'C',@PAmount,                   
                                --        0,@PAmount,1,0,@PAmount,@LastUpdate From Account Where AccountPK = @CashAtBankAcc and Status = 2  
        
                            --END       


                            -- BUY REKSADANA
    
                            if @PTrxType = 1 and @PInstrumentTypePK = 4                 
                            BEGIN                
         	      	            exec getJournalReference @PSettledDate,'CP',@CurReference out

                                Select @InvestmentAcc = InvInReksadana,@PayablePurchaseAcc=APPurchaseReksadana,@ReceivableSaleAcc=ARSellReksadana 
                                From AccountingSetup where Status in (1,2)                     

                                Select @CashAtBankAcc = AccountPK From CashRef where Status in (1,2)    and CashRefPK = @PCashRefPK                  

                                -- Setup Account kelar diatas, Next masukin ke Fund Journal 
                                select @JournalPK = isnull(max(JournalPK),0) + 1 From Journal  

                                -- T0                  
		                        INSERT INTO [Journal]([JournalPK],[HistoryPK],[Status],[Notes],[PeriodPK],[ValueDate],[Type],[TrxNo],[TrxName],[Reference]                  
		                        ,[Description],[Posted],[EntryUsersID],[EntryTime],[ApprovedUsersID],[ApprovedTime],[PostedBy],[PostedTime],[LastUpdate])                     

                                    Select  @JournalPK, 1,2,'',@PPeriodPK,@PValueDate,4,TrxPortfolioPK,'TRANSACTION',                
                                    @CurReference,'T0 REKSADANA BUY: ' + @InstrumentID ,1,EntryusersID,EntryTime,@UsersID,@LastUpdate,@UsersID,@LastUpdate,@LastUpdate from TrxPortfolio where  TrxPortfolioPK = @TrxPortfolioPK                 

                                        INSERT INTO [JournalDetail]([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[InstrumentPK],                
                                        [DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])               

                                        Select  @JournalPK,1,1,2,@InvestmentAcc,1,@PInstrumentPK,'T0 REKSADANA BUY: ' + @InstrumentID,'D',@PAmount,                   
                                        @PAmount,0,1,@PAmount,0,@LastUpdate From Account Where AccountPK = @InvestmentAcc and Status = 2                   

                                        INSERT INTO [JournalDetail]([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[InstrumentPK],                
                                        [DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])                 

                                        Select @JournalPK,2,1,2,@PayablePurchaseAcc,1,@PInstrumentPK,'T0 REKSADANA BUY: ' + @InstrumentID,'C',@PAmount,                   
                                        0,@PAmount,1,0,@PAmount,@LastUpdate From Account Where AccountPK = @PayablePurchaseAcc and Status = 2                



                                select @CashierPK = isnull(max(CashierPK),0) + 1 From Cashier                   
          
	

		                        INSERT INTO [dbo].[Cashier]  
		                        ([CashierPK],[HistoryPK],[Status],[PeriodPK],[ValueDate],[Type],[Reference],[Description],[DebitCredit],[DebitCurrencyPK],  
		                         [CreditCurrencyPK],[DebitCashRefPK],[CreditCashRefPK],[DebitAccountPK],[CreditAccountPK],[Debit],  
		                         [Credit],[DebitCurrencyRate],[CreditCurrencyRate],[BaseDebit],[BaseCredit],[PercentAmount],[FinalAmount],[OfficePK],[DepartmentPK],[AgentPK],[CounterpartPK],[ConsigneePK],[InstrumentPK],[JournalNo],[TrxNo],
		                         [EntryUsersID],[EntryTime],[ApprovedUsersID],[ApprovedTime],LastUpdate)

		                        select @CashierPK,1,1,dbo.FgetPeriod(@PSettledDate), @PSettledDate,'CP',@CurReference,'SUBSCRIPTION REKSADANA : ' + @InstrumentID,'D',1,
		                        1,0,@PCashRefPK,@PayablePurchaseAcc,@CashAtBankAcc,@PAmount,@PAmount,1,1,@PAmount,@PAmount,100,@PAmount,1,1,0,@PCounterpartPK,0,@PInstrumentPK,0,@TrxPortfolioPK,
		                        EntryusersID,EntryTime,@UsersID,@LastUpdate,@LastUpdate from TrxPortfolio where  TrxPortfolioPK = @TrxPortfolioPK     
		                                    


              


                            END   

                            --SELL REKSADANA

                            if @PTrxType = 2 and @PInstrumentTypePK = 4                  
                            BEGIN           
        	      
                                Select @InvestmentAcc = InvInReksadana,@RealisedAccReksadana = RealisedReksadana,@PayablePurchaseAcc=APPurchaseReksadana,@ReceivableSaleAcc=ARSellReksadana,
                                @UnrealisedMutualFund = UnrealisedReksadana, @RealisedMutualFund = RealisedReksadana,@RevalMutualFund = CadanganReksadana
                                From AccountingSetup where Status in (1,2)                

                                Select @CashAtBankAcc = AccountPK From CashRef where Status in (1,2)    and CashRefPK = @PCashRefPK                     
                                Select @InstrumentID = ID From Instrument where Status = 2 and InstrumentPK = @PInstrumentPK                  


                                select @CashierPK = isnull(max(CashierPK),0) + 1 From Cashier                   
          
                                exec getJournalReference @PSettledDate,'CR',@CurReference out

                                
                                delete from @AccountBalance

                                Declare B Cursor For
                                select distinct (SELECT DATEADD (dd, -1, DATEADD(yy, DATEDIFF(yy, 0, ValueDate) +1, 0))) ValueDate from journal 
                                where Posted = 1 and Reversed = 0 and Description <> 'PERIOD CLOSING' and ValueDate < DATEADD(yy, DATEDIFF(yy, 0, @PSettledDate), 0)
                                order by ValueDate asc
                                Open B
                                Fetch Next From B
                                INTO @CValueDate

                                While @@FETCH_STATUS = 0  
                                Begin

                                insert into @AccountBalance
                                select dbo.FGetAccountBalanceByDateByParent(@CValueDate,@UnrealisedMutualFund)

                                Fetch Next From B
                                into @CValueDate
                                End	
                                Close B
                                Deallocate B




                                select @BalancePriorYear = sum(Balance) from @AccountBalance


                                delete from @RealisedInstrument

                                insert into @RealisedInstrument
                                select TrxPortfolioPK,InstrumentPK,ValueDate,SettledDate,(Price - dbo.FGetLastAvgFromInvestment_Acc(ValueDate,@PInstrumentPK)) * Volume from TrxPortfolio 
                                where instrumentPk = @PInstrumentPK and SettledDate  between DATEADD(yy, DATEDIFF(yy, 0, @PSettledDate), 0) and @PSettledDate 
                                and status = 2  and Revised = 0 and TrxType = 2
                                order by SettledDate 


                                delete from @LastRealisedInstrument

                                Declare C Cursor For
                                select TrxPortfolioPK,InstrumentPK,ValueDate,SettledDate from @RealisedInstrument
                                order by ValueDate,SettledDate 
                                Open C
                                Fetch Next From C
                                INTO @CTrxPortfolioPK,@CInstrumentPK,@CValueDate,@CSettledDate

                                While @@FETCH_STATUS = 0  
                                Begin

                               

                                insert into @LastRealisedInstrument
                                select @CTrxPortfolioPK,@CInstrumentPK,@CValueDate,@CSettledDate,sum(isnull(Realised,0)) from @RealisedInstrument
                                where  InstrumentPK = @CInstrumentPK and SettledDate <= @CSettledDate

	
                                Fetch Next From C
                                into @CTrxPortfolioPK,@CInstrumentPK,@CValueDate,@CSettledDate
                                End	
                                Close C
                                Deallocate C



                                delete from @FlagRealisedInstrument

                                insert into @FlagRealisedInstrument
                                select A.InstrumentPK,A.ValueDate,A.SettledDate,Realised,@BalancePriorYear - LastRealised,case when @BalancePriorYear - LastRealised < 0 then 1 else 0 end from @LastRealisedInstrument A
                                left join @RealisedInstrument B on A.InstrumentPK = B.InstrumentPK and A.SettledDate = B.SettledDate and A.TrxPortfolioPK = B.TrxPortfolioPK

                                select @Realised = Realised, @LastUnrealised = Amount from @FlagRealisedInstrument where ValueDate = @PValueDate
                                select @CostAmount = dbo.FGetLastAvgFromInvestment_Acc(@PSettledDate,@PInstrumentPK) * @PVolume


		                        --select * from @FlagRealisedInstrument
                                INSERT INTO [dbo].[Cashier]  
                                ([CashierPK],[HistoryPK],[Status],[PeriodPK],[ValueDate],[Type],[Reference],[Description],[DebitCredit],[DebitCurrencyPK],  
                                [CreditCurrencyPK],[DebitCashRefPK],[CreditCashRefPK],[DebitAccountPK],[CreditAccountPK],[Debit],  
                                [Credit],[DebitCurrencyRate],[CreditCurrencyRate],[BaseDebit],[BaseCredit],[PercentAmount],[FinalAmount],[OfficePK],[DepartmentPK],[AgentPK],[CounterpartPK],[ConsigneePK],[InstrumentPK],[JournalNo],[TrxNo],
                                [EntryUsersID],[EntryTime],[ApprovedUsersID],[ApprovedTime],LastUpdate)

                                select @CashierPK,1,1,dbo.FgetPeriod(@PSettledDate), @PSettledDate,'CR',@CurReference,'REDEMPTION REKSADANA: ' + @InstrumentID,'C',1,
                                1,@PCashRefPK,0,@CashAtBankAcc,@ReceivableSaleAcc,@PAmount,@PAmount,1,1,@PAmount,@PAmount,100,@PAmount,1,1,0,@PCounterpartPK,0,@PInstrumentPK,0,@TrxPortfolioPK,
                                EntryusersID,EntryTime,@UsersID,@LastUpdate,@LastUpdate from TrxPortfolio where  TrxPortfolioPK = @TrxPortfolioPK  


                                -- Setup Account kelar diatas, Next masukin ke Fund Journal                  
                                select @JournalPK = isnull(max(JournalPK),0) + 1 From Journal                   

                                -- T0                  
		                        INSERT INTO [Journal]([JournalPK],[HistoryPK],[Status],[Notes],[PeriodPK],[ValueDate],[Type],[TrxNo],[TrxName],[Reference]                  
		                        ,[Description],[Posted],[EntryUsersID],[EntryTime],[ApprovedUsersID],[ApprovedTime],[PostedBy],[PostedTime],[LastUpdate])                   

                                    Select  @JournalPK, 1,2,'',@PPeriodPK,@PValueDate,4,TrxPortfolioPK,'TRANSACTION',                  
                                    @CurReference,'T0 REKSADANA SELL: ' + @InstrumentID ,1,EntryusersID,EntryTime,@UsersID,@LastUpdate,@UsersID,@LastUpdate,@LastUpdate from TrxPortfolio where  TrxPortfolioPK = @TrxPortfolioPK                 

                               
                   
                                    INSERT INTO [JournalDetail]([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[InstrumentPK],                
                                    [DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])                              

				                        Select @JournalPK,1,1,2,@InvestmentAcc,1,@PInstrumentPK,'T0 REKSADANA SELL: ' + @InstrumentID,'C',abs(@CostAmount),                   
				                        0,abs(@CostAmount),1,0,abs(@CostAmount),@LastUpdate From Account Where AccountPK = @InvestmentAcc and Status = 2               


                                    INSERT INTO [JournalDetail]([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[InstrumentPK],                
                                    [DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])                              

				                        Select  @JournalPK,2,1,2,@RevalMutualFund,1,@PInstrumentPK,'T0 REKSADANA SELL: ' + @InstrumentID,'C',isnull(@PAmount - @CostAmount,0),                   
				                        0,isnull(@PAmount - @CostAmount,0),1,0,isnull(@PAmount - @CostAmount,0),@LastUpdate From Account Where AccountPK = @RevalMutualFund and Status = 2                


                                    INSERT INTO [JournalDetail]([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[InstrumentPK],                
                                    [DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])                              

				                        Select  @JournalPK,3,1,2,@ReceivableSaleAcc,1,@PInstrumentPK,'T0 REKSADANA SELL: ' + @InstrumentID,'D',isnull(@PAmount,0),                   
				                        isnull(@PAmount,0),0,1,isnull(@PAmount,0),0,@LastUpdate From Account Where AccountPK = @ReceivableSaleAcc and Status = 2                

               

		
		                        --select * from @FlagRealisedInstrument
		                        --select @PSettledDate

		                        IF EXISTS(select * from @FlagRealisedInstrument where SettledDate < @PSettledDate and Flag = 1) and @Lastunrealised < 0
                                BEGIN
			                        --select @Realised,'Realised'
	                                INSERT INTO [JournalDetail]([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[InstrumentPK],                
	                                [DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])                   

	                                Select  @JournalPK,4,1,2,@UnrealisedMutualFund,1,@PInstrumentPK,'T0 EQUITY SELL: ' + @InstrumentID,'D',abs(@Realised),                   
	                                abs(@Realised),0,1,abs(@Realised),0,@LastUpdate From Account Where AccountPK = @UnrealisedMutualFund and Status = 2                   

  
	                                INSERT INTO [JournalDetail]([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[InstrumentPK],                
	                                [DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate]) 

	                                Select @JournalPK,5,1,2,@RealisedMutualFund,1,@PInstrumentPK,'T0 EQUITY SELL: ' + @InstrumentID,'C',abs(@Realised),                   
	                                0,abs(@Realised),1,0,abs(@Realised),@LastUpdate From Account Where AccountPK = @RealisedMutualFund and Status = 2   
                                END 
                                ELSE IF EXISTS(select * from @FlagRealisedInstrument where SettledDate <= @PSettledDate and Flag = 1) and @Lastunrealised < 0
                                BEGIN

			
			                        --select @LastUnrealised,'UnRealised'
	                                INSERT INTO [JournalDetail]([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[InstrumentPK],                
	                                [DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])                   

	                                Select  @JournalPK,4,1,2,@UnrealisedMutualFund,1,@PInstrumentPK,'T0 EQUITY SELL: ' + @InstrumentID,'D',abs(@LastUnrealised),                   
	                                abs(@LastUnrealised),0,1,abs(@LastUnrealised),0,@LastUpdate From Account Where AccountPK = @UnrealisedMutualFund and Status = 2                   

  
	                                INSERT INTO [JournalDetail]([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[InstrumentPK],                
	                                [DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate]) 

	                                Select @JournalPK,5,1,2,@RealisedMutualFund,1,@PInstrumentPK,'T0 EQUITY SELL: ' + @InstrumentID,'C',abs(@LastUnrealised),                   
	                                0,abs(@LastUnrealised),1,0,abs(@LastUnrealised),@LastUpdate From Account Where AccountPK = @RealisedMutualFund and Status = 2     


                                END
                                
                            END


	                        update TrxPortfolio    
	                        set PostedBy = @UsersID,PostedTime = @LastUpdate,Posted = 1,Lastupdate = @LastUpdate    
	                        where TrxPortfolioPK = @TrxPortfolioPK and Status = 2 
	             
	          
                        Fetch next From A                   
	                        Into @TrxPortfolioPK,@PValueDate,@PPeriodPK,@PReference,@PTrxType,@PCounterpartPK,@PInstrumentPK,
	                        @PCashRefPK,@PInterestAmount,@PSettledDate,@PVolume,@PAmount,@PBrokerageFeeAmount,@PLevyAmount,@PKPEIAmount,                  
	                        @PVATAmount,@PWHTAmount,@POTCAmount,@PIncomeTaxSellAmount,
	                        @PIncomeTaxInterestAmount,@PIncomeTaxGainAmount,@PNetAmount,@PInstrumentTypePK,@PRealisedAmount,@PMaturityDate 
                        END                  
                        Close A                  
                        Deallocate A

                        ";
                        cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _dateTo);
                        cmd.Parameters.AddWithValue("@UsersID", _usersID);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }


        public string PTPDeposito_BySelectedData(string _userID, DateTime _dateFrom, DateTime _dateTo, bool _param5)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        string _mature = "";
                        if (_param5 == true)
                        {
                            _mature = @"union all

                            select CONVERT(varchar(15), [identity]) + '/FP/'  
                            + REPLACE(RIGHT(CONVERT(VARCHAR(8), A.MaturityDate, 3), 5) ,'/','') Reference,A.MaturityDate valuedate, A.MaturityDate settlementdate, '' BrokerCode, C.Sinvestcode FundCode, D.ID Instrument, 1 DonePrice, A.Balance Quantity, 
                            A.Balance TradeAmount,0 CommissionAmount, 0 IncomeTaxSellAmount, 0 LevyAmount, 0 VatAmount, 0 OtherCharges, 2 TransactionType,
                            A.Balance TotalAmount, 0 WHTAmount, '' InvestmentNotes, 2 TrxType,2 SettlementMode,'' LastCouponDate,'' NextCouponDate,0 AccruedDays,
                            0 CapitalGainAmount,0 TaxInterestAmount,F.PTPCode BankCode,G.PTPCode BankBranchCode,F.Name BankName,G.BankAccountNo,
                            A.InterestPercent,A.MaturityDate,F.BitSyariah,G.ContactPerson,G.Phone1,G.Fax1,G.BankAccountName,A.Balance OldTradeAmount,A.InterestPercent OldInterestPercent,A.MaturityDate OldMaturityDate,H.ID CurrencyID,'' InterestPaymentType,A.Balance * 1 AmountTrf, A.InterestPercent BreakInterestPercent,AcqDate, 
                            round(A.Balance * (A.InterestPercent/100)/365 * datediff(day,A.AcqDate,A.MaturityDate),0) InterestAmount,1 Mature
                            from FundPosition A
                            left join Fund C on A.fundpk = C.fundpk and C.status = 2
                            left join instrument D on A.instrumentpk = D.instrumentpk and D.status = 2  
                            left join Bank F on A.BankPK = F.BankPK and F.status = 2
                            left join BankBranch G on A.BankBranchPK = G.BankBranchPK and G.status = 2
                            left join Currency H on D.CurrencyPK = H.CurrencyPK and H.status = 2
                            where A.MaturityDate = @ValueDate and A.TrailsPK = @TrailsPK ";
                        }
                        else
                        {
                            _mature = "";
                        }
                        cmd.CommandText = @"
                        Declare @TrailsPK int
                        Declare @MaxDateEndDayFP datetime

                        select @TrailsPK = EndDayTrailsFundPortfolioPK, @MaxDateEndDayFP = ValueDate from EndDayTrailsFundPortfolio 
                        where ValueDate = 
                        (
                        select max(valueDate) from EndDayTrailsFundPortfolio where status = 2 and valueDate < @ValueDate
                        )
                        and status = 2

                        BEGIN  
                                SET NOCOUNT ON         
          
                            create table #Text(      
                            [ResultText] [nvarchar](1000)  NULL          
                            )                        
        
                        truncate table #Text  
                        insert into #Text     
                        select ''     
                        insert into #Text
                        Select  
                        'NEWM' -- 1.Transaction Status
                        + '|' + cast(isnull(A.TrxType,'') as nvarchar) -- 2.Investment.TrxType
                        + '|' + @CompanyID -- 3.IM Code
                        + '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(A.FundCode,'')))) -- 4.Fund.SInvestCode
                        + '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(A.BankCode,'')))) -- 5.Bank.PTPCode
                        + '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(A.BankBranchCode,'')))) -- 6.BankBranch.PTPCode
                        + '|' + RTRIM(LTRIM((isnull(A.BankAccountName,'')))) -- 7.BankBranch.BankAccountName
                        + '|' + RTRIM(LTRIM((isnull(A.BankAccountNo,'')))) -- 8.BankBranch.BankAccountNo
                        + '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(A.CurrencyID,'')))) -- 9.Instrument.CurrencyPK
                        + '|' + case when A.TrxType = 1  then case when A.TradeAmount = 0 then '0' else cast(isnull(cast(A.TradeAmount as decimal(30,2)),'')as nvarchar) end 
                        else case when A.TrxType = 3 then case when A.OldTradeAmount = 0 then '0' else cast(isnull(cast(A.OldTradeAmount as decimal(30,2)),'')as nvarchar) end else cast(isnull(cast(A.TradeAmount as decimal(30,2)),'')as nvarchar) end  end -- 10.Investment.DoneAmount
                        + '|' + case when A.TrxType = 1  then case when A.InterestPercent = 0 then '0' else cast(isnull(cast(A.InterestPercent as decimal(6,4)),'')as nvarchar) end 
                        else case when A.TrxType = 3 then case when A.OldInterestPercent = 0 then '0' else cast(isnull(cast(A.OldInterestPercent as decimal(6,4)),'')as nvarchar) end else cast(isnull(cast(A.InterestPercent as decimal(6,4)),'')as nvarchar) end  end -- 11.Investment.InterestPercent
                        + '|' + case when A.TrxType = 2  then RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(CONVERT(VARCHAR(10), A.AcqDate, 112),'')))) else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(CONVERT(VARCHAR(10), ValueDate, 112),'')))) end -- 12.Investment.ValueDate
                        + '|' + case when A.TrxType = 1  then RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(CONVERT(VARCHAR(10), A.MaturityDate, 112),'')))) 
                        else case when A.TrxType = 3 then RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(CONVERT(VARCHAR(10), A.OldMaturityDate, 112),'')))) else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(CONVERT(VARCHAR(10), A.OldMaturityDate, 112),'')))) end  end  -- 13.Investment.MaturityDate
                        + '|' + case when A.TrxType = 1  then cast(isnull(A.InterestPaymentType,'') as nvarchar) else case when A.TrxType = 3 then '1' else '' end  end -- 14.Investment.InterestPaymentType           
                        + '|' + case when A.TrxType = 1  then '1' else  '' end  -- 15. Hardcode InterestType
                        + '|' + case when A.TrxType = 1  then case when A.BitSyariah = 1  then 'Y' else 'N' end  else '' end -- 16.Bank.BitSyariah
                        + '|' + case when A.TrxType = 2  then RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(CONVERT(VARCHAR(10), A.ValueDate, 112),'')))) else '' end -- 17.WithdrawalDate
                        + '|' + case when A.TrxType = 2  then cast(isnull(cast(A.BreakInterestPercent as decimal(6,4)),'')as nvarchar) else '' end -- 18.Investment.BreakInteresPercent
                        + '|' + case when A.TrxType = 2  then cast(isnull(cast(A.TradeAmount as decimal(30,2)),'')as nvarchar)  else '' end -- 19.Withdrawal Principle
                        + '|' + '' -- 20.Withdrawal Interest
                        + '|' + '' -- 21.Total Withdrawal Amount
                        + '|' + -- 22.Rollover Type
                        + '|' + case when A.TrxType = 3 then cast(isnull(cast(A.TradeAmount as decimal(30,2)),'')as nvarchar) else '' end -- 23.Investment.DoneAmount 
                        + '|' + case when A.TrxType = 3 then cast(isnull(cast(A.InterestPercent as decimal(6,4)),'')as nvarchar) else '' end -- 24.Investment.InterestPercent 
                        + '|' + case when A.TrxType = 3 then RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(CONVERT(VARCHAR(10), MaturityDate, 112),'')))) else '' end -- 25.Investment.MaturityDate
                        + '|' + case when A.Mature = 1 then cast(isnull(cast((A.TradeAmount) as decimal(30,2)),'')as nvarchar) else case when A.TrxType = 2  then cast(isnull(cast((A.TradeAmount) as decimal(30,2)),'')as nvarchar) else cast(isnull(A.AmountTrf,0) as nvarchar) end end-- 26.Amount to be Transfer
                        + '|' + -- 27.Statutory Type
                        + '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(A.ContactPerson,'')))) -- 28.BankBranch.ContactPerson
                        + '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(A.Phone1,'')))) -- 29.BankBranch.Phone1
                        + '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(A.Fax1,'')))) -- 30.BankBranch.Fax1
                        + '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(A.Reference,'')))) -- 31.Investment.Reference
                        + '|' + case when A.TrxType = 2 then RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(A.Reference,'')))) else case when A.TrxType = 3 then RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(A.Reference,'')))) else '' end end -- 32.Investment.Reference
                        + '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(A.InvestmentNotes,'')))) -- 33.Investment.InvestmentNotes
                        + '|' + '' 
                        from (      
                        select A.Reference,A.valuedate valuedate, A.settlementdate settlementdate, B.SInvestCode BrokerCode, C.Sinvestcode FundCode, D.ID Instrument, A.DonePrice, A.DoneVolume Quantity, 
                        A.DoneAmount TradeAmount,A.CommissionAmount, A.IncomeTaxSellAmount, A.LevyAmount, A.VatAmount, A.OTCamount OtherCharges, A.TrxType TransactionType,
                        A.TotalAmount, A.WHTAmount, A.InvestmentNotes, A.TrxType,A.SettlementMode,A.LastCouponDate,A.NextCouponDate,datediff(day,A.LastCouponDate,A.SettlementDate) AccruedDays,
                        A.IncomeTaxGainAmount CapitalGainAmount,A.IncomeTaxInterestAmount TaxInterestAmount,F.PTPCode BankCode,G.PTPCode BankBranchCode,F.Name BankName,G.BankAccountNo,
                        A.InterestPercent,A.MaturityDate,F.BitSyariah,G.ContactPerson,G.Phone1,G.Fax1,G.BankAccountName,I.Balance OldTradeAmount,I.InterestPercent OldInterestPercent,I.MaturityDate OldMaturityDate,H.ID CurrencyID,A.InterestPaymentType,
                        case when A.DoneAmount = I.Balance then 0 else cast(sum(isnull(A.DoneAmount,0) - isnull(I.Balance,0)) as decimal(30,2)) end AmountTrf,A.BreakInterestPercent,A.AcqDate,
                        round(A.DoneAmount * (A.BreakInterestPercent/100)/365 * datediff(day,A.AcqDate,A.SettlementDate),0) InterestAmount,0 Mature
                        from investment A
                        left join counterpart B on A.counterpartpk = B.counterpartpk and b.status = 2
                        left join Fund C on A.fundpk = C.fundpk and C.status = 2
                        left join instrument D on A.instrumentpk = D.instrumentpk and D.status = 2
                        left join MasterValue E on E.Code = A.SettlementMode and E.status =2  and E.ID ='SettlementMode'   
                        left join Bank F on A.BankPK = F.BankPK and F.status = 2
                        left join BankBranch G on A.BankBranchPK = G.BankBranchPK and G.status = 2
                        left join Currency H on D.CurrencyPK = H.CurrencyPK and H.status = 2
                        left join FundPosition I on A.TrxBuy = I.[Identity] and I.status = 2
                        where    
                        A.ValueDate =  @ValueDate and A.InstrumentTypePK in (5)
                        and A.SelectedSettlement = 1
                        and A.statusdealing = 2

                        Group by A.Reference,A.valuedate, A.settlementdate, B.SInvestCode, C.Sinvestcode, D.ID, A.DonePrice, A.DoneVolume, 
                        A.DoneAmount,A.CommissionAmount, A.IncomeTaxSellAmount, A.LevyAmount, A.VatAmount, A.OTCamount, A.TrxType,
                        A.TotalAmount, A.WHTAmount, A.InvestmentNotes, A.TrxType,A.SettlementMode,A.LastCouponDate,A.NextCouponDate,
                        A.DoneAccruedInterest,A.IncomeTaxGainAmount,A.IncomeTaxInterestAmount,F.PTPCode,G.PTPCode,F.Name,G.BankAccountNo,
                        A.InterestPercent,A.MaturityDate,F.BitSyariah,G.ContactPerson,G.Phone1,G.Fax1,G.BankAccountName,I.Balance,I.InterestPercent,I.MaturityDate,H.ID,A.InterestPaymentType,A.BreakInterestPercent,A.AcqDate


                            " + _mature + @"

                        )A    
                        Group by A.Reference,A.ValueDate,A.settlementdate,A.BrokerCode,A.FundCode,A.Instrument,A.DonePrice,A.Quantity,A.TradeAmount,A.CommissionAmount,A.IncomeTaxSellAmount,
                        A.TransactionType, A.LevyAmount, A.VatAmount, A.OtherCharges,A.TotalAmount, A.WHTAmount, A.InvestmentNotes, A.TrxType,A.SettlementMode,A.LastCouponDate,A.NextCouponDate,A.AccruedDays,
                        A.InterestAmount,A.CapitalGainAmount,A.TaxInterestAmount,A.BankCode,A.BankBranchCode,A.BankName,A.BankAccountNo,A.InterestPercent,A.MaturityDate,A.BitSyariah,A.ContactPerson,A.Phone1,A.Fax1,
                        A.BankAccountName,A.OldTradeAmount,A.OldInterestPercent,A.OldMaturityDate,A.CurrencyID,A.InterestPaymentType,A.AmountTrf,A.BreakInterestPercent,A.AcqDate,A.Mature 
                        order by A.ValueDate Asc
                        select * from #text 
                        END


                                    ";
                        cmd.Parameters.AddWithValue("@CompanyID", _host.Get_CompanyID());
                        cmd.Parameters.AddWithValue("@ValueDate", _dateFrom);



                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                string filePath = Tools.SInvestTextPath + "PTP_Deposito.txt";
                                FileInfo txtFile = new FileInfo(filePath);
                                if (txtFile.Exists)
                                {
                                    txtFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                }

                                using (System.IO.StreamWriter file = new System.IO.StreamWriter(filePath))
                                {
                                    while (dr.Read())
                                    {
                                        file.WriteLine(Convert.ToString(dr["ResultText"]));
                                    }
                                    return Tools.HtmlSinvestTextPath + "PTP_Deposito.txt";
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

        public Boolean GenerateReportAccounting(string _userID, AccountingRpt _accountingRpt)
        {

            #region Laporan Keuangan
            if (_accountingRpt.ReportName.Equals("Laporan Keuangan"))
            {
                try
                {
                    using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                    {
                        DbCon.Open();
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {

                            string filePath = Tools.ReportsPath + "LKAscend" + "_" + _userID + ".xlsx";
                            File.Copy(Tools.ReportsTemplatePath + "\\01\\" + "01_TemplateLKReportAscend.xlsx", filePath, true);
                            FileInfo excelFile = new FileInfo(filePath);
                            using (ExcelPackage package = new ExcelPackage(excelFile))
                            {

                                cmd.CommandText =
                                @"
                                          DECLARE @valueDateFromLastMonth DATETIME

SET @valueDateFromLastMonth = DATEADD(mm, DATEDIFF(mm, 0, @ValueDateFrom) - 1, 0)


DECLARE @ValueDateLastYear datetime
set @ValueDateLastYear = dateadd(year,-1,@ValueDateTo)


Declare @PeriodPK int,@PeriodPKLastYear int
Select @PeriodPK = PeriodPK from Period where @ValueDateTo between DateFrom and DateTo and status = 2
Select @PeriodPKLastYear = PeriodPK from Period where @ValueDateLastYear between DateFrom and DateTo and status = 2

Select A.Row,A.AccountPK,isnull(E.PreviousBaseBalance,0) * case when D.Type in (2,3,4) then -1 else 1 end PreviousBaseBalance,
isnull(B.CurrentBaseBalance,0) * case when D.Type in (2,3,4) then -1 else 1 end CurrentBaseBalance, 
isnull(C.CurrentBalance,0) * case when D.Type in (2,3,4) then 1 else 1 end CurrentBaseBalanceLastYear
from TemplateReport A
left join
(
  SELECT A.AccountPK,       
    SUM(B.Balance) AS CurrentBalance,       
    SUM(B.BaseBalance) AS CurrentBaseBalance,      
    SUM(B.SumDebit) AS CurrentDebit,       
    SUM(B.SumCredit) AS CurrentCredit,       
    SUM(B.SumBaseDebit) AS CurrentBaseDebit,       
    SUM(B.SumBaseCredit) AS CurrentBaseCredit      
    FROM [Account] A INNER JOIN [Currency] C ON A.CurrencyPK = C.CurrencyPK, (      
    SELECT A.AccountPK, SUM(A.Debit-A.Credit) AS Balance,       
    SUM(A.BaseDebit-A.BaseCredit) AS BaseBalance,      
    SUM(A.Debit) AS SumDebit,      
    SUM(A.Credit) AS SumCredit,      
    SUM(A.BaseDebit) AS SumBaseDebit,      
    SUM(A.BaseCredit) AS SumBaseCredit,      
    C.ParentPK1, C.ParentPK2, C.ParentPK3, C.ParentPK4, C.ParentPK5,      
    C.ParentPK6, C.ParentPK7, C.ParentPK8, C.ParentPK9     
    FROM [JournalDetail] A INNER JOIN [Journal] B ON A.JournalPK = B.JournalPK      
    INNER JOIN Account C ON A.AccountPK = C.AccountPK   And C.Status in (1,2)  
    INNER JOIN Currency D ON A.CurrencyPK = D.CurrencyPK And D.Status in (1,2)   
    WHERE  B.ValueDate >= @ValueDateFrom AND B.ValueDate <= @ValueDateTo and  B.PeriodPK = @PeriodPK
    
	and B.Status <> 3

    Group BY A.AccountPK, C.ParentPK1, C.ParentPK2,        
    C.ParentPK3, C.ParentPK4, C.ParentPK5, C.ParentPK6,       
    C.ParentPK7, C.ParentPK8, C.ParentPK9        
    ) AS B        
    WHERE 
(B.AccountPK = A.AccountPK OR B.ParentPK1 = A.AccountPK OR B.ParentPK2 = A.AccountPK        
    OR B.ParentPK3 = A.AccountPK OR B.ParentPK4 = A.AccountPK OR B.ParentPK5 = A.AccountPK        
    OR B.ParentPK6 = A.AccountPK OR B.ParentPK7 = A.AccountPK OR B.ParentPK8 = A.AccountPK        
    OR B.ParentPK9 = A.AccountPK)       and A.Status = 2 
    Group BY A.AccountPK      
     
        
   
)B on A.AccountPK = B.AccountPK
Left join
(
SELECT A.AccountPK, A.ID, A.Name, A.[Type], A.[Groups],    
SUM(B.Balance) AS CurrentBalance FROM Account A, (     
SELECT A.AccountPK, B.Posted, B.Reversed, SUM(CASE C.[Type] WHEN 1 THEN A.BaseDebit-A.BaseCredit      
ELSE A.BaseCredit-A.BaseDebit END) AS Balance,      
C.ParentPK1, C.ParentPK2, C.ParentPK3, C.ParentPK4, C.ParentPK5, C.ParentPK6,     
C.ParentPK7, C.ParentPK8, C.ParentPK9     
FROM [JournalDetail] A      
INNER JOIN [Journal] B ON A.JournalPK = B.JournalPK     
INNER JOIN Account C ON A.AccountPK = C.AccountPK  and C.Status in (1,2)   
WHERE  B.ValueDate <= @ValueDateTo and B.PeriodPK = @PeriodPK   

And B.status <> 3


GROUP BY A.AccountPK, B.Posted, B.Reversed,C.ParentPK1, C.ParentPK2, C.ParentPK3, C.ParentPK4,      
C.ParentPK5, C.ParentPK6, C.ParentPK7, C.ParentPK8, C.ParentPK9     
) AS B     
    WHERE A.[Type] > 2 AND A.Show = 1 AND (B.AccountPK = A.AccountPK          
OR B.ParentPK3 = A.AccountPK OR B.ParentPK4 = A.AccountPK      
OR B.ParentPK5 = A.AccountPK OR B.ParentPK6 = A.AccountPK      
OR B.ParentPK7 = A.AccountPK OR B.ParentPK8 = A.AccountPK       
OR B.ParentPK9 = A.AccountPK) and A.Status = 2    

GROUP BY A.AccountPK, A.ID, A.Name, A.[Type], A.[Levels], A.[Groups]    


)C on A.AccountPK = C.AccountPK
left join Account D on A.AccountPK = D.AccountPK and D.status in (1,2)
left join
(
    SELECT A.AccountPK,        
    SUM(B.Balance) AS PreviousBalance,        
    SUM(B.BaseBalance) AS PreviousBaseBalance,       
    SUM(B.SumDebit) AS PreviousDebit,        
    SUM(B.SumCredit) AS PreviousCredit,        
    SUM(B.SumBaseDebit) AS PreviousBaseDebit,        
    SUM(B.SumBaseCredit) AS PreviousBaseCredit       
    FROM [Account] A INNER JOIN [Currency] C ON A.CurrencyPK = C.CurrencyPK, (       
    SELECT A.AccountPK, SUM(A.Debit-A.Credit) AS Balance,        
    SUM(A.BaseDebit-A.BaseCredit) AS BaseBalance,        
    SUM(A.Debit) AS SumDebit,        
    SUM(A.Credit) AS SumCredit,        
    SUM(A.BaseDebit) AS SumBaseDebit,        
    SUM(A.BaseCredit) AS SumBaseCredit,        
    C.ParentPK1, C.ParentPK2, C.ParentPK3, C.ParentPK4, C.ParentPK5,        
    C.ParentPK6, C.ParentPK7, C.ParentPK8, C.ParentPK9       
    FROM [JournalDetail] A INNER JOIN [Journal] B ON A.JournalPK = B.JournalPK        
    INNER JOIN Account C ON A.AccountPK = C.AccountPK   And C.Status in (1,2)   
    INNER JOIN Currency D ON A.CurrencyPK = D.CurrencyPK    And D.Status in (1,2)   
    WHERE   B.ValueDate >= @valueDateFromLastMonth AND B.ValueDate < @ValueDateFrom  and  B.PeriodPK = @PeriodPK 

	and B.Status <> 3

    Group BY A.AccountPK, C.ParentPK1, C.ParentPK2,        
    C.ParentPK3, C.ParentPK4, C.ParentPK5, C.ParentPK6,       
    C.ParentPK7, C.ParentPK8, C.ParentPK9        
    ) AS B        
    WHERE   (B.AccountPK = A.AccountPK OR B.ParentPK1 = A.AccountPK OR B.ParentPK2 = A.AccountPK        
    OR B.ParentPK3 = A.AccountPK OR B.ParentPK4 = A.AccountPK OR B.ParentPK5 = A.AccountPK        
    OR B.ParentPK6 = A.AccountPK OR B.ParentPK7 = A.AccountPK OR B.ParentPK8 = A.AccountPK        
    OR B.ParentPK9 = A.AccountPK)       and A.Status = 2 
    Group BY A.AccountPK  
)E on A.AccountPK = E.AccountPK

where ReportName = 'ASCEND_LK' and D.Type > 2
order by A.Row asc

                                            ";
                                cmd.CommandTimeout = 0;
                                cmd.Parameters.AddWithValue("@ValueDateTo", _accountingRpt.ValueDateTo);
                                cmd.Parameters.AddWithValue("@ValueDateFrom", _accountingRpt.ValueDateFrom);

                                using (SqlDataReader dr0 = cmd.ExecuteReader())
                                {
                                    if (!dr0.HasRows)
                                    {
                                        return false;
                                    }
                                    else
                                    {
                                        ExcelWorksheet worksheet = package.Workbook.Worksheets[1];
                                        ExcelWorksheet worksheet4 = package.Workbook.Worksheets[4];
                                        ExcelWorksheet worksheet5 = package.Workbook.Worksheets[5];

                                        worksheet.Cells[2, 1].Value = _accountingRpt.ValueDateFrom;
                                        worksheet.Cells["A" + 2 + ":F" + 2].Merge = true;
                                        worksheet.Cells["A" + 2 + ":F" + 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        worksheet.Cells[2, 1].Style.Numberformat.Format = "MMMM yyyy";

                                        worksheet4.Cells[2, 1].Value = _accountingRpt.ValueDateFrom;
                                        worksheet4.Cells["A" + 2 + ":F" + 2].Merge = true;
                                        worksheet4.Cells["A" + 2 + ":F" + 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        worksheet4.Cells[2, 1].Style.Numberformat.Format = "MMMM yyyy";
                                        //worksheet4.Cells["A" + 2 + ":H" + 2].Merge = true;
                                        worksheet5.Cells[2, 1].Value = _accountingRpt.ValueDateFrom;
                                        worksheet5.Cells["A" + 2 + ":F" + 2].Merge = true;
                                        worksheet5.Cells["A" + 2 + ":F" + 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        //worksheet5.Cells["A" + 2 + ":F" + 2].Merge = true;
                                        worksheet5.Cells[2, 1].Style.Numberformat.Format = "MMMM yyyy";
                                        while (dr0.Read())
                                        {
                                            worksheet.Cells[Convert.ToInt32(dr0["Row"]), 4].Value = Convert.ToDecimal(dr0["PreviousBaseBalance"]);
                                            worksheet.Cells[Convert.ToInt32(dr0["Row"]), 5].Value = Convert.ToDecimal(dr0["CurrentBaseBalance"]);
                                            worksheet.Cells[Convert.ToInt32(dr0["Row"]), 6].Value = Convert.ToDecimal(dr0["CurrentBaseBalanceLastYear"]);



                                            if (_accountingRpt.DecimalPlaces == 0)
                                            {
                                                worksheet.Cells[Convert.ToInt32(dr0["Row"]), 4].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[Convert.ToInt32(dr0["Row"]), 5].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[Convert.ToInt32(dr0["Row"]), 6].Style.Numberformat.Format = "#,##0";
                                            }
                                            else if (_accountingRpt.DecimalPlaces == 2)
                                            {
                                                worksheet.Cells[Convert.ToInt32(dr0["Row"]), 4].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[Convert.ToInt32(dr0["Row"]), 5].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[Convert.ToInt32(dr0["Row"]), 6].Style.Numberformat.Format = "#,##0.00";
                                            }
                                            else if (_accountingRpt.DecimalPlaces == 4)
                                            {
                                                worksheet.Cells[Convert.ToInt32(dr0["Row"]), 4].Style.Numberformat.Format = "#,##0.0000";
                                                worksheet.Cells[Convert.ToInt32(dr0["Row"]), 5].Style.Numberformat.Format = "#,##0.0000";
                                                worksheet.Cells[Convert.ToInt32(dr0["Row"]), 6].Style.Numberformat.Format = "#,##0.0000";
                                            }
                                            else if (_accountingRpt.DecimalPlaces == 6)
                                            {
                                                worksheet.Cells[Convert.ToInt32(dr0["Row"]), 4].Style.Numberformat.Format = "#,##0.000000";
                                                worksheet.Cells[Convert.ToInt32(dr0["Row"]), 5].Style.Numberformat.Format = "#,##0.000000";
                                                worksheet.Cells[Convert.ToInt32(dr0["Row"]), 6].Style.Numberformat.Format = "#,##0.000000";
                                            }
                                            else
                                            {
                                                worksheet.Cells[Convert.ToInt32(dr0["Row"]), 4].Style.Numberformat.Format = "#,##0.00000000";
                                                worksheet.Cells[Convert.ToInt32(dr0["Row"]), 5].Style.Numberformat.Format = "#,##0.00000000";
                                                worksheet.Cells[Convert.ToInt32(dr0["Row"]), 6].Style.Numberformat.Format = "#,##0.00000000";
                                            }

                                        }
                                        worksheet.Calculate();
                                    }
                                }

                                ExcelWorksheet worksheet2 = package.Workbook.Worksheets[2];

                                cmd.CommandText =
                                @"
                                        Declare @ValueDateLastYear datetime
                                        set @ValueDateLastYear = dateadd(year,-1,@ValueDateTo2)




                                        Declare @PeriodPK int,@PeriodPKLastYear int
                                        Select @PeriodPK = PeriodPK from Period where @ValueDateTo2 between DateFrom and DateTo and status = 2
                                        Select @PeriodPKLastYear = PeriodPK from Period where @ValueDateLastYear between DateFrom and DateTo and status = 2

                                      Select A.Row,A.AccountPK,isnull(B.PreviousBaseBalance,0) * case when D.Type in (2,3,4) then -1 else 1 end PreviousBaseBalance,
                                            isnull(B.BaseDebitMutasi,0) * case when D.Type in (2,3,4) then -1 else 1 end BaseDebitMutasi,
                                            isnull(B.BaseCreditMutasi,0) * case when D.Type in (2,3,4) then -1 else 1 end BaseCreditMutasi,
                                            isnull(B.CurrentBaseBalance,0) * case when D.Type in (2,3,4) then -1 else 1 end CurrentBaseBalance, 
                                            isnull(C.CurrentBalance,0) * case when D.Type in (2,3,4) then 1 else 1 end CurrentBaseBalanceLastYear
                                        from TemplateReport A
                                        left join
                                        (
                                        SELECT A.AccountPK, C.Name, C.[Groups],C.[ParentPK],    
    
                                            CAST(ISNULL(B.PreviousBaseBalance, 0) AS NUMERIC(19,4)) AS PreviousBaseBalance,      
                                            CAST(A.CurrentBaseDebit - ISNULL(B.PreviousBaseDebit, 0) AS NUMERIC(19,4)) AS BaseDebitMutasi,       
                                            CAST(A.CurrentBaseCredit - ISNULL(B.PreviousBaseCredit, 0) AS NUMERIC(19,4)) AS BaseCreditMutasi,      
                                            CAST(A.CurrentBaseBalance AS NUMERIC(19,4)) AS CurrentBaseBalance      
                                            FROM (      
                                            SELECT A.AccountPK,       
                                            SUM(B.Balance) AS CurrentBalance,       
                                            SUM(B.BaseBalance) AS CurrentBaseBalance,      
                                            SUM(B.SumDebit) AS CurrentDebit,       
                                            SUM(B.SumCredit) AS CurrentCredit,       
                                            SUM(B.SumBaseDebit) AS CurrentBaseDebit,       
                                            SUM(B.SumBaseCredit) AS CurrentBaseCredit      
                                            FROM [Account] A INNER JOIN [Currency] C ON A.CurrencyPK = C.CurrencyPK, (      
                                            SELECT A.AccountPK, SUM(A.Debit-A.Credit) AS Balance,       
                                            SUM(A.BaseDebit-A.BaseCredit) AS BaseBalance,      
                                            SUM(A.Debit) AS SumDebit,      
                                            SUM(A.Credit) AS SumCredit,      
                                            SUM(A.BaseDebit) AS SumBaseDebit,      
                                            SUM(A.BaseCredit) AS SumBaseCredit,      
                                            C.ParentPK1, C.ParentPK2, C.ParentPK3, C.ParentPK4, C.ParentPK5,      
                                            C.ParentPK6, C.ParentPK7, C.ParentPK8, C.ParentPK9     
                                            FROM [JournalDetail] A INNER JOIN [Journal] B ON A.JournalPK = B.JournalPK      
                                            INNER JOIN Account C ON A.AccountPK = C.AccountPK   And C.Status in (1,2)  
                                            INNER JOIN Currency D ON A.CurrencyPK = D.CurrencyPK And D.Status in (1,2)   
                                            WHERE  B.ValueDate <= @ValueDateTo2 and  B.PeriodPK = @PeriodPK
    
	                                        and B.Status <> 3

                                            Group BY A.AccountPK, C.ParentPK1, C.ParentPK2,        
                                            C.ParentPK3, C.ParentPK4, C.ParentPK5, C.ParentPK6,       
                                            C.ParentPK7, C.ParentPK8, C.ParentPK9        
                                            ) AS B        
                                            WHERE 
                                        (B.AccountPK = A.AccountPK OR B.ParentPK1 = A.AccountPK OR B.ParentPK2 = A.AccountPK        
                                            OR B.ParentPK3 = A.AccountPK OR B.ParentPK4 = A.AccountPK OR B.ParentPK5 = A.AccountPK        
                                            OR B.ParentPK6 = A.AccountPK OR B.ParentPK7 = A.AccountPK OR B.ParentPK8 = A.AccountPK        
                                            OR B.ParentPK9 = A.AccountPK)       and A.Status = 2 
                                            Group BY A.AccountPK       
                                            ) AS A LEFT JOIN (       
                                            SELECT A.AccountPK,        
                                            SUM(B.Balance) AS PreviousBalance,        
                                            SUM(B.BaseBalance) AS PreviousBaseBalance,       
                                            SUM(B.SumDebit) AS PreviousDebit,        
                                            SUM(B.SumCredit) AS PreviousCredit,        
                                            SUM(B.SumBaseDebit) AS PreviousBaseDebit,        
                                            SUM(B.SumBaseCredit) AS PreviousBaseCredit       
                                            FROM [Account] A INNER JOIN [Currency] C ON A.CurrencyPK = C.CurrencyPK, (       
                                            SELECT A.AccountPK, SUM(A.Debit-A.Credit) AS Balance,        
                                            SUM(A.BaseDebit-A.BaseCredit) AS BaseBalance,        
                                            SUM(A.Debit) AS SumDebit,        
                                            SUM(A.Credit) AS SumCredit,        
                                            SUM(A.BaseDebit) AS SumBaseDebit,        
                                            SUM(A.BaseCredit) AS SumBaseCredit,        
                                            C.ParentPK1, C.ParentPK2, C.ParentPK3, C.ParentPK4, C.ParentPK5,        
                                            C.ParentPK6, C.ParentPK7, C.ParentPK8, C.ParentPK9       
                                            FROM [JournalDetail] A INNER JOIN [Journal] B ON A.JournalPK = B.JournalPK        
                                            INNER JOIN Account C ON A.AccountPK = C.AccountPK   And C.Status in (1,2)   
                                            INNER JOIN Currency D ON A.CurrencyPK = D.CurrencyPK    And D.Status in (1,2)   
                                            WHERE  B.ValueDate < @ValueDateFrom2  and  B.PeriodPK = @PeriodPK 

	                                        and B.Status <> 3

                                            Group BY A.AccountPK, C.ParentPK1, C.ParentPK2,        
                                            C.ParentPK3, C.ParentPK4, C.ParentPK5, C.ParentPK6,       
                                            C.ParentPK7, C.ParentPK8, C.ParentPK9        
                                            ) AS B        
                                            WHERE   (B.AccountPK = A.AccountPK OR B.ParentPK1 = A.AccountPK OR B.ParentPK2 = A.AccountPK        
                                            OR B.ParentPK3 = A.AccountPK OR B.ParentPK4 = A.AccountPK OR B.ParentPK5 = A.AccountPK        
                                            OR B.ParentPK6 = A.AccountPK OR B.ParentPK7 = A.AccountPK OR B.ParentPK8 = A.AccountPK        
                                            OR B.ParentPK9 = A.AccountPK)       and A.Status = 2 
                                            Group BY A.AccountPK       
                                            ) AS B ON A.AccountPK = B.AccountPK        
                                            INNER JOIN Account C ON A.AccountPK = C.AccountPK   And C.Status in (1,2)    
                                            INNER JOIN Currency D ON C.CurrencyPK = D.CurrencyPK   And D.Status in (1,2)   
                                            WHERE (A.CurrentBalance <> 0)        
                                            OR (A.CurrentDebit - ISNULL(B.PreviousDebit, 0) <> 0)        
                                            OR (A.CurrentCredit - ISNULL(B.PreviousCredit, 0) <> 0)        
                                            OR (A.CurrentBaseBalance <> 0)        
                                            OR (A.CurrentBaseDebit - ISNULL(B.PreviousBaseDebit, 0) <> 0)        
                                            OR (A.CurrentBaseCredit - ISNULL(B.PreviousBaseCredit, 0) <> 0)     
   
                                        )B on A.AccountPK = B.AccountPK
                                        Left join
                                        (
                                        SELECT A.AccountPK, A.ID, A.Name, A.[Type], A.[Groups],    
                                        SUM(B.Balance) AS CurrentBalance FROM Account A, (     
                                        SELECT A.AccountPK, B.Posted, B.Reversed, SUM(CASE C.[Type] WHEN 1 THEN A.BaseDebit-A.BaseCredit      
                                        ELSE A.BaseCredit-A.BaseDebit END) AS Balance,      
                                        C.ParentPK1, C.ParentPK2, C.ParentPK3, C.ParentPK4, C.ParentPK5, C.ParentPK6,     
                                        C.ParentPK7, C.ParentPK8, C.ParentPK9     
                                        FROM [JournalDetail] A      
                                        INNER JOIN [Journal] B ON A.JournalPK = B.JournalPK     
                                        INNER JOIN Account C ON A.AccountPK = C.AccountPK  and C.Status in (1,2)   
                                        WHERE  B.ValueDate <= @ValueDateLastYear and B.PeriodPK = @PeriodPKLastYear 

                                        And B.status <> 3


                                        GROUP BY A.AccountPK, B.Posted, B.Reversed,C.ParentPK1, C.ParentPK2, C.ParentPK3, C.ParentPK4,      
                                        C.ParentPK5, C.ParentPK6, C.ParentPK7, C.ParentPK8, C.ParentPK9     
                                        ) AS B     
                                        WHERE A.[Type] <= 2 AND A.Show = 1 AND (B.AccountPK = A.AccountPK      
                                        OR B.ParentPK1 = A.AccountPK OR B.ParentPK2 = A.AccountPK       
                                        OR B.ParentPK3 = A.AccountPK OR B.ParentPK4 = A.AccountPK      
                                        OR B.ParentPK5 = A.AccountPK OR B.ParentPK6 = A.AccountPK      
                                        OR B.ParentPK7 = A.AccountPK OR B.ParentPK8 = A.AccountPK       
                                        OR B.ParentPK9 = A.AccountPK) and A.Status = 2    

                                        GROUP BY A.AccountPK, A.ID, A.Name, A.[Type], A.[Levels], A.[Groups]    


                                        )C on A.AccountPK = C.AccountPK
                                        left join Account D on A.AccountPK = D.AccountPK and D.status in (1,2)

                                        where ReportName = 'ASCEND_LK' and D.Type <= 2
                                        order by A.Row asc
                                        ";
                                cmd.CommandTimeout = 0;
                                cmd.Parameters.AddWithValue("@ValueDateTo2", _accountingRpt.ValueDateTo);
                                cmd.Parameters.AddWithValue("@ValueDateFrom2", _accountingRpt.ValueDateFrom);

                                using (SqlDataReader dr1 = cmd.ExecuteReader())
                                {
                                    if (!dr1.HasRows)
                                    {
                                        return false;
                                    }
                                    else
                                    {
                                        worksheet2.Cells[2, 1].Value = _accountingRpt.ValueDateFrom;
                                        worksheet2.Cells["A" + 2 + ":G" + 2].Merge = true;
                                        worksheet2.Cells["A" + 2 + ":G" + 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        worksheet2.Cells[2, 1].Style.Numberformat.Format = "MMMM yyyy";
                                        while (dr1.Read())

                                            while (dr1.Read())
                                            {
                                                worksheet2.Cells[Convert.ToInt32(dr1["Row"]), 5].Value = Convert.ToDecimal(dr1["PreviousBaseBalance"]);
                                                worksheet2.Cells[Convert.ToInt32(dr1["Row"]), 6].Value = Convert.ToDecimal(dr1["CurrentBaseBalance"]);


                                                if (_accountingRpt.DecimalPlaces == 0)
                                                {
                                                    worksheet2.Cells[Convert.ToInt32(dr1["Row"]), 5].Style.Numberformat.Format = "#,##0";
                                                    worksheet2.Cells[Convert.ToInt32(dr1["Row"]), 6].Style.Numberformat.Format = "#,##0";
                                                    worksheet2.Cells[Convert.ToInt32(dr1["Row"]), 7].Style.Numberformat.Format = "#,##0";
                                                }
                                                else if (_accountingRpt.DecimalPlaces == 2)
                                                {
                                                    worksheet2.Cells[Convert.ToInt32(dr1["Row"]), 5].Style.Numberformat.Format = "#,##0.00";
                                                    worksheet2.Cells[Convert.ToInt32(dr1["Row"]), 6].Style.Numberformat.Format = "#,##0.00";
                                                    worksheet2.Cells[Convert.ToInt32(dr1["Row"]), 7].Style.Numberformat.Format = "#,##0.00";
                                                }
                                                else if (_accountingRpt.DecimalPlaces == 4)
                                                {
                                                    worksheet2.Cells[Convert.ToInt32(dr1["Row"]), 5].Style.Numberformat.Format = "#,##0.0000";
                                                    worksheet2.Cells[Convert.ToInt32(dr1["Row"]), 6].Style.Numberformat.Format = "#,##0.0000";
                                                    worksheet2.Cells[Convert.ToInt32(dr1["Row"]), 7].Style.Numberformat.Format = "#,##0.0000";
                                                }
                                                else if (_accountingRpt.DecimalPlaces == 6)
                                                {
                                                    worksheet2.Cells[Convert.ToInt32(dr1["Row"]), 5].Style.Numberformat.Format = "#,##0.000000";
                                                    worksheet2.Cells[Convert.ToInt32(dr1["Row"]), 6].Style.Numberformat.Format = "#,##0.000000";
                                                    worksheet2.Cells[Convert.ToInt32(dr1["Row"]), 7].Style.Numberformat.Format = "#,##0.000000";
                                                }
                                                else
                                                {
                                                    worksheet2.Cells[Convert.ToInt32(dr1["Row"]), 6].Style.Numberformat.Format = "#,##0.00000000";
                                                    worksheet2.Cells[Convert.ToInt32(dr1["Row"]), 6].Style.Numberformat.Format = "#,##0.00000000";
                                                    worksheet2.Cells[Convert.ToInt32(dr1["Row"]), 7].Style.Numberformat.Format = "#,##0.00000000";
                                                }
                                            }
                                        worksheet2.Calculate();
                                    }
                                }

                                //ExcelWorksheet worksheet3 = package.Workbook.Worksheets[3];
                                //while (dr0.Read())
                                //{

                                //}

                                //ExcelWorksheet worksheet4 = package.Workbook.Worksheets[4];
                                //while (dr0.Read())
                                //{

                                //}

                                //ExcelWorksheet worksheet5 = package.Workbook.Worksheets[5];
                                //while (dr0.Read())
                                //{

                                //}
                                package.Save();
                                return true;
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
            #endregion
            
            else
            {
                return false;
            }
        }

        public string PTPBond_BySelectedData(string _userID, DateTime _dateFrom, DateTime _dateTo, Investment _investment)
        {

            try
            {
                string _paramSettlementPK = "";

                if (!_host.findString(_investment.stringInvestmentFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_investment.stringInvestmentFrom))
                {
                    _paramSettlementPK = " And SettlementPK in (" + _investment.stringInvestmentFrom + ") ";
                }
                else
                {
                    _paramSettlementPK = " And SettlementPK in (0) ";
                }
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = @"
                                BEGIN  
                                SET NOCOUNT ON         
          
                                create table #Text(      
                                [ResultText] [nvarchar](1000)  NULL          
                                )                        
        
                                truncate table #Text      
                                insert into #Text     
                                select 'Transaction Status|TA Reference ID|Data Type|TA Reference No.|Trade Date|Settlement Date|IM Code|BR Code|Fund Code|Security Code|Buy/Sell|Price|Face Value|Proceeds|Last Coupon Date|Next Coupon Date|Accrued Days|Accrued Interest Amount|Other Fee|Capital Gain Tax|Interest Income Tax|Withholding Tax|Net Proceeds|Settlement Type|Sellers Tax ID|Purpose of Transaction|Statutory Type|Remarks|Cancellation Reason|Data Type|TA Reference No.|Face Value|Acquisition Date|Acquisition Price(%)|Acquisition Amount|Capital Gain|Days of Holding Interest|Holding Interest Amount|Total Taxable Income|Tax Rate in %|Tax Amount'      
        
                                insert into #Text
                                Select  
                                'NEWM' --1
                                + '|' + ''
                                + '|' + '1'
                                + '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(A.Reference,''))))
                                + '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(CONVERT(VARCHAR(10), ValueDate, 112),''))))
                                + '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(CONVERT(VARCHAR(10), settlementdate, 112),''))))
                                + '|' + @CompanyID
                                + '|' + isnull(A.BrokerCode,'')
                                + '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(A.FundCode,''))))
                                + '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(A.Instrument,''))))
                                + '|' + cast(isnull(A.TrxType,'') as nvarchar)
                                + '|' + case when A.DonePrice = 0 then '0' else cast(isnull(cast(A.DonePrice as decimal(30,6)),'')as nvarchar) end 
                                + '|' + case when A.Quantity = 0 then '0' else cast(isnull(cast(A.Quantity as decimal(30,0)), '')as nvarchar) end
                                + '|' + case when A.DonePrice = 0 then '0' else cast(isnull(cast(sum(A.DonePrice * A.Quantity)/100 as decimal(30,2)), '')as nvarchar) end
                                + '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(CONVERT(VARCHAR(10), LastCouponDate, 112),''))))
                                + '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(CONVERT(VARCHAR(10), NextCouponDate, 112),''))))
                                + '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(A.AccruedDays,'')))) 
                                + '|' + case when A.InterestAmount = 0 then '0' else cast(isnull(cast(isnull(A.InterestAmount,0) as decimal(30,2)),'')as nvarchar) end 
                                + '|' + ''
                                + '|' + case when A.CapitalGainAmount = 0 then '0' else cast(isnull(cast(isnull(A.CapitalGainAmount,0) as decimal(30,2)),'')as nvarchar) end 
                                + '|' + case when A.TaxInterestAmount = 0 then '0' else cast(isnull(cast(isnull(A.TaxInterestAmount,0) as decimal(30,2)),'')as nvarchar) end 
                                + '|' +  cast(cast(isnull(sum(A.CapitalGainAmount + A.TaxInterestAmount),0) as decimal (30,2)) as nvarchar)
                                + '|' + case when A.TotalAmount = 0 then '0' else cast(isnull(cast(isnull(A.TotalAmount,0) as decimal(30,2)),'')as nvarchar) end 
                                + '|' + case when A.TrxType = 1 then '2' else case when A.TrxType = 2  then '1' else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(A.SettlementMode,'')))) end  end             
                                + '|' + case when A.TrxType = 1 then '' else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(A.BankAccountNo,'')))) end              
                                + '|' + case when A.InvestmentTrType = 0 then '3' else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(A.InvestmentTrType,'3'))) ) end 
                                + '|' + case when A.StatutoryType = 0 then '2' else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(A.StatutoryType,'2')))) end
                                + '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(A.Notes,''))))
                                + '|' + '' 
                                + case when A.TrxType = 1 then '' else  
                                + '|' + case when A.TrxType = 1 then '' else '2' end  
                                + '|' + case when A.TrxType = 1 then '' else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(A.Reference,'')))) end 
                                + '|' + case when A.TrxType = 1 then '' else case when A.Quantity = 0 then '0' else cast(isnull(cast(A.Quantity as decimal(30,0)), '')as nvarchar) end end  
                                + '|' + case when A.TrxType = 1 then '' else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(CONVERT(VARCHAR(10), AcqDate, 112),'')))) end    
                                + '|' + case when A.TrxType = 1 then '' else case when A.AcqPrice = 0 then '0' else cast(isnull(cast(A.AcqPrice as decimal(30,6)), '')as nvarchar) end end     
                                + '|' + case when A.TrxType = 1 then '' else case when A.AcqPrice = 0 then '0' else cast(isnull(cast(sum(A.AcqPrice * A.AcqVolume)/100 as decimal(30,2)), '')as nvarchar) end end     
                                + '|' + case when A.TrxType = 1 then '' else cast(isnull(cast(isnull(sum(A.DonePrice* A.Quantity)/100 - sum(A.AcqPrice * A.AcqVolume)/100,0) as decimal(30,2)),'')as nvarchar)  end 
                                + '|' + case when A.TrxType = 1 then '' else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(CONVERT(VARCHAR(10), case when A.InstrumentTypePK = 3 then dbo.FgetDateDiffCorporateBond(A.LastCouponDate,A.SettlementDate) else  dbo.FGetDateDIffGovermentBond(A.LastCouponDate,A.SettlementDate) end, 112),''))))   end 
                                + '|' + case when A.TrxType = 1 then '' else  cast(isnull(cast(isnull(A.InterestAmount,0) as decimal(30,2)),'')as nvarchar) end 
                                + '|' + case when A.TrxType = 1 then '' else case when A.TaxInterestAmount = 0 then '0' else cast(isnull(cast(sum(A.CapitalGainAmount + A.TaxInterestAmount) as decimal(30,2)),'')as nvarchar) end  end 
                                + '|' + case when A.TrxType = 1 then '' else case when A.TaxExpensePercent = 0 then '0' else cast(isnull(cast(isnull(A.TaxExpensePercent,0) as decimal(30,2)),'')as nvarchar) end  end 
                                + '|' + case when A.TrxType = 1 then '' else case when A.TaxInterestAmount = 0 then '0' else cast(isnull(cast(sum(A.CapitalGainAmount + A.TaxInterestAmount) as decimal(30,2)),'')as nvarchar) end  end 
                                End
                                from (      
                                select A.InstrumentTypePK,A.Reference,A.valuedate valuedate, A.settlementdate settlementdate, B.SInvestCode BrokerCode, C.Sinvestcode FundCode, D.ID Instrument, A.DonePrice, A.DoneVolume Quantity, 
                                round(A.DoneAmount,0) TradeAmount,A.CommissionAmount, A.IncomeTaxSellAmount, A.LevyAmount, A.VatAmount, A.OTCamount OtherCharges, A.TrxType TransactionType,
                                round(A.TotalAmount,0) TotalAmount, A.WHTAmount, A.Notes, A.TrxType,A.SettlementMode,A.LastCouponDate,A.NextCouponDate,case when A.InstrumentTypePK = 3 then dbo.FgetDateDiffCorporateBond(A.LastCouponDate,A.SettlementDate) else datediff(day,A.LastCouponDate,A.SettlementDate) end AccruedDays,
                                round(A.DoneAccruedInterest,0) InterestAmount,round(A.IncomeTaxGainAmount,0) CapitalGainAmount,round(A.IncomeTaxInterestAmount,0) TaxInterestAmount,A.AcqDate,isnull(A.AcqPrice,0) AcqPrice,isnull(A.AcqVolume,0) AcqVolume,A.TaxExpensePercent,F.BankAccountNo,A.PurposeOfTransaction,A.StatutoryType,A.InterestPercent,A.InvestmentTrType from investment A
                                left join counterpart B on A.counterpartpk = B.counterpartpk and b.status = 2
                                left join Fund C on A.fundpk = C.fundpk and C.status = 2
                                left join instrument D on A.instrumentpk = D.instrumentpk and D.status = 2
                                left join MasterValue E on E.Code = A.SettlementMode and E.status =2  and E.ID ='SettlementMode'
                                left join FundCashRef F on A.fundcashrefpk = F.fundcashrefpk and F.status = 2 and F.Type = 3
                                where    
                                A.ValueDate =  @valuedate and A.InstrumentTypePK in (2,3,9,13,15)
                                " + _paramSettlementPK + @" and A.statusdealing = 2

                                )A    
                                Group by A.InstrumentTypePK,A.Reference,A.ValueDate,A.settlementdate,A.BrokerCode,A.FundCode,A.Instrument,A.DonePrice,A.Quantity,A.TradeAmount,A.CommissionAmount,A.IncomeTaxSellAmount,
                                A.TransactionType, A.LevyAmount, A.VatAmount, A.OtherCharges,A.TotalAmount, A.WHTAmount, A.Notes, A.TrxType,A.SettlementMode,A.LastCouponDate,A.NextCouponDate,A.AccruedDays,
                                A.InterestAmount,A.CapitalGainAmount,A.TaxInterestAmount,AcqDate,AcqPrice,AcqVolume,TaxExpensePercent,BankAccountNo,A.PurposeOfTransaction,A.StatutoryType,A.InvestmentTrType
                                order by A.ValueDate Asc


                                select * from #text 
                                END";
                        cmd.Parameters.AddWithValue("@CompanyID", _host.Get_CompanyID());
                        cmd.Parameters.AddWithValue("@ValueDate", _dateFrom);



                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                string filePath = Tools.SInvestTextPath + "PTP_Bond.txt";
                                FileInfo txtFile = new FileInfo(filePath);
                                if (txtFile.Exists)
                                {
                                    txtFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                }

                                using (System.IO.StreamWriter file = new System.IO.StreamWriter(filePath))
                                {
                                    while (dr.Read())
                                    {
                                        file.WriteLine(Convert.ToString(dr["ResultText"]));
                                    }
                                    return Tools.HtmlSinvestTextPath + "PTP_Bond.txt";
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


        public Boolean GenerateReportCompliance(string _userID, OjkRpt _OjkRpt)
        {
            #region KPD
            if (_OjkRpt.ReportName.Equals("5"))
            {
                #region Txt

                if (_OjkRpt.DownloadMode == "Txt")
                {
                    try
                    {
                        DateTime _datetimeNow = DateTime.Now;
                        using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                        {
                            DbCon.Open();
                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                string _companyCode = "";

                                cmd.CommandTimeout = 0;
                                cmd.CommandText = @"

                                declare @FundPK int

                                --drop table #Text--
                                create table #Text(      
                                [ResultText] [nvarchar](1000)  NULL          
                                )                        
        
                                truncate table #Text --         

                                --drop Table #KPD--
                                Create Table #KPD
                                (AUM nvarchar(50),CashAmount nvarchar(50),InstrumentTypePK int,PK int,KodeNasabah nvarchar(50),NamaNasabah nvarchar(50) ,NomorKontrak nvarchar(50),TanggalKontrak nvarchar(50),TanggalJatuhTempo nvarchar(50),
                                NomorAdendum nvarchar(50), TanggalAdendum nvarchar(50),NilaiInvestasiAwalIDR nvarchar(50), NilaiInvestasiAwalNonIDR nvarchar(50),NilaiInvestasiAkhir nvarchar(50),
                                NilaiInvestasiAkhirNonIDR nvarchar(50), JenisEfek nvarchar(50), DNatauLN int,JumlahEfek nvarchar(50),NilaiPembelian nvarchar(50), NilaiNominal nvarchar(50),
                                HPW nvarchar(50), Deposito nvarchar(50), TotalNilai nvarchar(50),KodeBK  nvarchar(50), type int,KodeSaham nvarchar(50),MarketValue nvarchar(50),SID nvarchar(50)
                                )

                                DECLARE A CURSOR FOR 
                                select FundPK
                                from Fund
                                where [Status] = 2 and FundTypeInternal = 2
                                Open A
                                Fetch Next From A
                                Into @FundPK

                                While @@FETCH_STATUS = 0
                                Begin


                                Insert into #KPD (AUM,CashAmount,InstrumentTypePK,PK,KodeNasabah ,NamaNasabah ,NomorKontrak,TanggalKontrak,TanggalJatuhTempo,
								NomorAdendum, TanggalAdendum,NilaiInvestasiAwalIDR, NilaiInvestasiAwalNonIDR,NilaiInvestasiAkhir,
								NilaiInvestasiAkhirNonIDR, JenisEfek, DNatauLN,JumlahEfek,NilaiPembelian, NilaiNominal,
								HPW, Deposito, TotalNilai,KodeBK,type,KodeSaham,MarketValue,SID)
								select F.AUM,E.CashAmount,InstrumentTypePK,ROW_NUMBER() OVER(ORDER BY C.ID ASC) AS PK,H.ClientCategory KodeNasabah ,H.Name NamaNasabah ,A.KPDNoContract NomorKontrak,isnull(CONVERT(VARCHAR(8), A.EffectiveDate, 112),0) TanggalKontrak,isnull(CONVERT(VARCHAR(8), A.MaturityDate, 112),0) TanggalJatuhTempo,
								0 NomorAdendum,0 TanggalAdendum,cast(isnull(TotalUnits,CAST(TotalUnits AS DECIMAL(22, 0))) * isnull(A.Nav,CAST(A.Nav AS DECIMAL(22, 0)))as DECIMAL(22, 0)) NilaiInvestasiAwalIDR,0 NilaiInvestasiAwalNonIDR,F.AUM NilaiInvestasiAkhir,
								0 NilaiInvestasiAkhirNonIDR, C.ID JenisEfek,1 DNatauLN,Case When C.InstrumentTypePK not in (5,10) then B.Balance else 0 end as JumlahEfek,CAST(B.CostValue AS DECIMAL(22, 0)) NilaiPembelian,CAST(0 AS DECIMAL(22, 2)) NilaiNominal,
								case when C.InstrumentTypePK not in (5,10) then   CAST(B.ClosePrice AS DECIMAL(22, 6))  else CAST(B.ClosePrice AS DECIMAL(22, 0)) end HPW,CASE WHEN C.InstrumentTypePK = 5 THEN B.Balance ELSE  0 END AS Deposito,0 TotalNilai,isnull(I.NKPDCode,0) KodeBK,C.InstrumentTypePK type, '1' KodeSaham,
								B.Balance * case when C.InstrumentTypePK not in (5,10) then   CAST(B.ClosePrice AS DECIMAL(22, 6))  else CAST(B.ClosePrice AS DECIMAL(22, 0)) end,isnull(H.SID,'') SID from Fund A
								left join FundPosition B on A.FundPK = B.FundPK and B.Status = 2
								left join Instrument C on B.InstrumentPK = C.InstrumentPK and C.Status = 2 
								left join BankBranch D on A.BankBranchPK = D.BankBranchPK and D.status  = 2
								left join DistributedIncome E on A.FundPK = E.FundPK and E.status  = 2 and E.ValueDate <= @Date and Posted = 1
								left join CloseNAV F on A.FundPK = F.FundPK and F.Status = 2 and F.Date = @Date
								left join FundClientposition G on A.FundPK = G.FundPK
								left join FundClient H on G.FundClientPK = H.FundClientPK and H.status = 2
								left join Bank I on D.BankPK = I.BankPK and I.Status in (1,2)
								--left join ClosePrice I on B.InstrumentPK = I.instrumentPK and I.status = 2 and B.Date = @Date
								where A.FundPK = @FundPK and B.Date = @Date and A.status = 2
								Group By  F.AUM,E.CashAmount,C.ID,H.ClientCategory,H.Name,KPDNoContract,A.EffectiveDate,A.MaturityDate,A.TotalUnits,A.Nav,E.CashAmount,B.Balance,B.CostValue,B.ClosePrice,I.NKPDCode,C.InstrumentTypePK,B.MarketValue,A.FundPK,H.SID
								order By C.ID asc

                                Fetch next From A Into @FundPK
                                end
                                Close A
                                Deallocate A

                                update #KPD set 
                                NomorAdendum = 0, TanggalAdendum = 0, NilaiInvestasiAwalNonIDR = 0, --NilaiInvestasiAkhir = CAST(0 AS DECIMAL(22, 2)),
                                NilaiInvestasiAkhirNonIDR = 0 where PK <> 1



                                insert into #Text 

                                select isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(KodeNasabah,'')))),'') + --1
                                '|' + isnull(RTRIM(LTRIM(isnull(NamaNasabah,''))),'')  +    --2
                                '|' + isnull(RTRIM(LTRIM(isnull(NomorKontrak,''))),'')  +  --3
                                '|' + isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(TanggalKontrak,'')))),'')  +  --4
                                '|' + isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(TanggalJatuhTempo,'')))),'')  +  --5
                                '|' + isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(NomorAdendum,'')))),'')  +  --6
                                '|' + isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(TanggalAdendum,'')))),'')  +  --7
                                '|' + isnull(RTRIM(LTRIM(isnull(NilaiInvestasiAwalIDR,''))),'')  +  --8
                                '|' + isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(NilaiInvestasiAwalNonIDR,'')))),'')  + --9
                                '|' + isnull(RTRIM(LTRIM(isnull(NilaiInvestasiAkhir,''))),'')  + --10
                                '|' + isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(NilaiInvestasiAkhirNonIDR,'')))),'')  +  --11
                                '|' + isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(JenisEfek,'')))),'')  +  --12
                                '|' + isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(DNatauLN,'')))),'')  + --13
                                '|' + isnull(RTRIM(LTRIM(isnull(JumlahEfek,0))),0) + --14
                                --'|' + isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(JumlahEfek,'')))),'')  +  --14
                                '|' + isnull(RTRIM(LTRIM(isnull(NilaiPembelian,0))),0) + --15
                                --'|' +  isnull(RTRIM(LTRIM(isnull(NilaiPembelian,''))),'')  +  --15
                                --'|' + case when type = 1 then isnull(RTRIM(LTRIM(isnull(MarketValue,''))),'') else case when type not in (1,5) then isnull(RTRIM(LTRIM(isnull(MarketValue,''))),'') else '' end end + --16
                                '|' + isnull(RTRIM(LTRIM(isnull(0,''))),'')  +  --16
                                --'|' + case when type = 1 then isnull(RTRIM(LTRIM(isnull(HPW,''))),'') else case when type not in (1,5) then isnull(RTRIM(LTRIM(isnull(PriceBond,''))),'') else '' end end +
                                '|' + isnull(RTRIM(LTRIM(isnull(HPW,''))),'')  +  --17
                                '|' + isnull(RTRIM(LTRIM(isnull(Deposito,''))),'')  +  --18
                                '|' + isnull(RTRIM(LTRIM(isnull(MarketValue,0))),0) + --19
                                --'|' + isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(TotalNilai,'')))),'')  +   --19
                                '|' + isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(KodeBK,'')))),'') + --20
                                '|' + '0' +--21
                                '|' + isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(SID,'')))),'') --22
                                from #KPD

if exists(select top 1 * from #Text)
BEGIN
     
select * from #text

END
ELSE
BEGIN
    select '' ResultText 
END


                                     ";
                                cmd.Parameters.AddWithValue("@Date", _OjkRpt.Date);
                                //cmd.Parameters.AddWithValue("@FundPK", _OjkRpt.Fund);

                                using (SqlDataReader dr = cmd.ExecuteReader())
                                {
                                    if (dr.HasRows)
                                    {


                                        _companyCode = _host.Get_CompanyID();
                                        string filePath = Tools.ARIATextPath + _companyCode + "KPD.txt";
                                        FileInfo txtFile = new FileInfo(filePath);
                                        if (txtFile.Exists)
                                        {
                                            txtFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                        }

                                        using (System.IO.StreamWriter file = new System.IO.StreamWriter(filePath))
                                        {
                                            while (dr.Read())
                                            {
                                                file.WriteLine(Convert.ToString(dr["ResultText"]));
                                            }
                                            return true;
                                            //return Tools.HtmlARIATextPath + _companyCode + "KPD.txt";
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

                #region Excel

                if (_OjkRpt.DownloadMode == "Excel")
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
declare @FundPK int

--                             create table #Text(      
--[ResultText] [nvarchar](1000)  NULL          
--)                        
        
        

--drop Table #KPD
Create Table #KPD
(AUM nvarchar(50),CashAmount nvarchar(50),InstrumentTypePK int,PK int,KodeNasabah nvarchar(50),NamaNasabah nvarchar(50) ,NomorKontrak nvarchar(50),TanggalKontrak nvarchar(50),TanggalJatuhTempo nvarchar(50),
NomorAdendum nvarchar(50), TanggalAdendum nvarchar(50),NilaiInvestasiAwalIDR nvarchar(50), NilaiInvestasiAwalNonIDR nvarchar(50),NilaiInvestasiAkhir nvarchar(50),
NilaiInvestasiAkhirNonIDR nvarchar(50), JenisEfek nvarchar(50), DNatauLN int,JumlahEfek nvarchar(50),NilaiPembelian nvarchar(50), NilaiNominal nvarchar(50),
HPW nvarchar(50), Deposito nvarchar(50), TotalNilai nvarchar(50),KodeBK  nvarchar(50), type int,KodeSaham nvarchar(50),MarketValue nvarchar(50),SID nvarchar(50)
)

DECLARE A CURSOR FOR 
select FundPK
from Fund
where [Status] = 2 and FundTypeInternal = 2
Open A
Fetch Next From A
Into @FundPK

While @@FETCH_STATUS = 0
Begin

Insert into #KPD (AUM,CashAmount,InstrumentTypePK,PK,KodeNasabah ,NamaNasabah ,NomorKontrak,TanggalKontrak,TanggalJatuhTempo,
NomorAdendum, TanggalAdendum,NilaiInvestasiAwalIDR, NilaiInvestasiAwalNonIDR,NilaiInvestasiAkhir,
NilaiInvestasiAkhirNonIDR, JenisEfek, DNatauLN,JumlahEfek,NilaiPembelian, NilaiNominal,
HPW, Deposito, TotalNilai,KodeBK,type,KodeSaham,MarketValue,SID)
select F.AUM,E.CashAmount,InstrumentTypePK,ROW_NUMBER() OVER(ORDER BY C.ID ASC) AS PK,H.ClientCategory KodeNasabah ,H.Name NamaNasabah ,A.KPDNoContract NomorKontrak,isnull(CONVERT(VARCHAR(8), A.EffectiveDate, 112),0) TanggalKontrak,isnull(CONVERT(VARCHAR(8), A.MaturityDate, 112),0) TanggalJatuhTempo,
0 NomorAdendum,0 TanggalAdendum,cast(isnull(TotalUnits,CAST(TotalUnits AS DECIMAL(22, 0))) * isnull(A.Nav,CAST(A.Nav AS DECIMAL(22, 0)))as DECIMAL(22, 0)) NilaiInvestasiAwalIDR,0 NilaiInvestasiAwalNonIDR,F.AUM NilaiInvestasiAkhir,
0 NilaiInvestasiAkhirNonIDR, C.ID JenisEfek,1 DNatauLN,Case When C.InstrumentTypePK not in (5,10) then B.Balance else 0 end as JumlahEfek,CAST(B.CostValue AS DECIMAL(22, 0)) NilaiPembelian,CAST(0 AS DECIMAL(22, 2)) NilaiNominal,
case when C.InstrumentTypePK not in (5,10) then   CAST(B.ClosePrice AS DECIMAL(22, 6))  else CAST(B.ClosePrice AS DECIMAL(22, 0)) end HPW,CASE WHEN C.InstrumentTypePK = 5 THEN B.Balance ELSE  0 END AS Deposito,0 TotalNilai,isnull(I.NKPDCode,0) KodeBK,C.InstrumentTypePK type, '1' KodeSaham,CAST(B.MarketValue AS DECIMAL(22, 0)) MarketValue,isnull(H.SID,'') SID from Fund A
left join FundPosition B on A.FundPK = B.FundPK and B.Status = 2
left join Instrument C on B.InstrumentPK = C.InstrumentPK and C.Status = 2 
left join BankBranch D on A.BankBranchPK = D.BankBranchPK and D.status  = 2
left join DistributedIncome E on A.FundPK = E.FundPK and E.status  = 2 and E.ValueDate <= @Date and Posted = 1
left join CloseNAV F on A.FundPK = F.FundPK and F.Status = 2 and F.Date = @Date
left join FundClientposition G on A.FundPK = G.FundPK
left join FundClient H on G.FundClientPK = H.FundClientPK and H.status = 2
left join Bank I on D.BankPK = I.BankPK and I.Status in (1,2)
--left join ClosePrice I on B.InstrumentPK = I.instrumentPK and I.status = 2 and B.Date = @Date
where A.FundPK = @FundPK and B.Date = @Date and A.status = 2
Group By  F.AUM,E.CashAmount,C.ID,H.ClientCategory,H.Name,KPDNoContract,A.EffectiveDate,A.MaturityDate,A.TotalUnits,A.Nav,E.CashAmount,B.Balance,B.CostValue,B.ClosePrice,I.NKPDCode,C.InstrumentTypePK,B.MarketValue,A.FundPK,H.SID
order By C.ID asc

Fetch next From A Into @FundPK
end
Close A
Deallocate A

update #KPD set 
NomorAdendum = 0, TanggalAdendum = 0, NilaiInvestasiAwalNonIDR = 0, --NilaiInvestasiAkhir = CAST(0 AS DECIMAL(22, 2)),
NilaiInvestasiAkhirNonIDR = 0 where PK <> 1


if exists(select top 1 * from #KPD)
BEGIN


select isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(KodeNasabah,'')))),'') KodeNasabah --1
, isnull(RTRIM(LTRIM(isnull(NamaNasabah,''))),'') NamaNasabah     --2
, isnull(RTRIM(LTRIM(isnull(NomorKontrak,''))),'')  NomorKontrak  --3
, isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(TanggalKontrak,'')))),'')  TanggalKontrak  --4
, isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(TanggalJatuhTempo,'')))),'')  TanggalJatuhTempo  --5
, isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(NomorAdendum,'')))),'')  NomorAdendum  --6
, isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(TanggalAdendum,'')))),'')  TanggalAdendum  --7
, isnull(RTRIM(LTRIM(isnull(NilaiInvestasiAwalIDR,''))),'') NilaiInvestasiAwalIDR   --8
, isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(NilaiInvestasiAwalNonIDR,'')))),'') NilaiInvestasiAwalNonIDR  --9
, isnull(RTRIM(LTRIM(isnull(NilaiInvestasiAkhir,''))),'') NilaiInvestasiAkhir  --10
, isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(NilaiInvestasiAkhirNonIDR,'')))),'') NilaiInvestasiAkhirNonIDR   --11
, isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(JenisEfek,'')))),'')  JenisEfek  --12
, isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(DNatauLN,'')))),'') KodeKategoriEfek  --13
,isnull(RTRIM(LTRIM(isnull(JumlahEfek,0))),0) JumlahEfek --14
--'|' + isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(JumlahEfek,'')))),'')  +  --14
,isnull(RTRIM(LTRIM(isnull(NilaiPembelian,0))),0) NilaiPembelian --15
--'|' + isnull(RTRIM(LTRIM(isnull(NilaiPembelian,''))),'')  +  --15
--'|' + case when type = 1 then isnull(RTRIM(LTRIM(isnull(MarketValue,''))),'') else case when type not in (1,5) then isnull(RTRIM(LTRIM(isnull(MarketValue,''))),'') else '' end end + --16
, isnull(RTRIM(LTRIM(isnull(0,''))),'')  NilaiNominal  --16
--'|' + case when type = 1 then isnull(RTRIM(LTRIM(isnull(HPW,''))),'') else case when type not in (1,5) then isnull(RTRIM(LTRIM(isnull(PriceBond,''))),'') else '' end end +
, isnull(RTRIM(LTRIM(isnull(HPW,''))),'') HPW   --17
, isnull(RTRIM(LTRIM(isnull(Deposito,''))),'')  Deposito  --18
, 0 TotalInvestasi --19
--'|' + isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(TotalNilai,'')))),'')    --19
, isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(KodeBK,'')))),'') KodeBK --20
, '999' Keterangan --21
, isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(SID,'')))),'') SID --22
from #KPD

END
ELSE
BEGIN
    select '' KodeNasabah ,'' NamaNasabah ,'' NomorKontrak ,'' TanggalKontrak ,'' TanggalJatuhTempo ,'' NomorAdendum ,'' TanggalAdendum ,'' NilaiInvestasiAwalIDR ,'' NilaiInvestasiAwalNonIDR ,'' NilaiInvestasiAkhir ,'' NilaiInvestasiAkhirNonIDR ,
'' JenisEfek ,0 KodeKategoriEfek ,'' JumlahEfek ,'' NilaiPembelian ,'' NilaiNominal ,'' HPW ,'' Deposito ,'' TotalInvestasi ,'' KodeBK ,'' Keterangan ,'' SID 
END
                            ";


                                //cmd.Parameters.AddWithValue("@date", _sInvestRpt.ParamDate);

                                cmd.Parameters.AddWithValue("@Date", _OjkRpt.Date);
                                //cmd.Parameters.AddWithValue("@FundPK", _OjkRpt.Fund);

                                using (SqlDataReader dr0 = cmd.ExecuteReader())
                                {
                                    if (!dr0.HasRows)
                                    {
                                        return false;
                                    }
                                    else
                                    {
                                        string filePath = Tools.ReportsPath + "KPD" + "_" + _userID + ".xlsx";
                                        string pdfPath = Tools.ReportsPath + "KPD" + "_" + _userID + ".pdf";
                                        FileInfo excelFile = new FileInfo(filePath);
                                        if (excelFile.Exists)
                                        {
                                            excelFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                            excelFile = new FileInfo(filePath);
                                        }


                                        // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                        using (ExcelPackage package = new ExcelPackage(excelFile))
                                        {
                                            package.Workbook.Properties.Title = "KPDReport";
                                            package.Workbook.Properties.Author = Tools.DefaultReportAuthor();
                                            package.Workbook.Properties.Comments = Tools.DefaultReportComments();
                                            package.Workbook.Properties.Company = Tools.DefaultReportCompany();
                                            package.Workbook.Properties.SetCustomPropertyValue("Checked by", _userID);
                                            package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", Tools.DefaultReportAssemblyName());

                                            ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("KPD Report");


                                            //ATUR DATA GROUPINGNYA DULU
                                            List<KPD> rList = new List<KPD>();
                                            while (dr0.Read())
                                            {
                                                KPD rSingle = new KPD();
                                                rSingle.KodeNasabah = Convert.ToString(dr0["KodeNasabah"]);
                                                rSingle.NamaNasabah = Convert.ToString(dr0["NamaNasabah"]);
                                                rSingle.NomorKontrak = Convert.ToString(dr0["NomorKontrak"]);
                                                rSingle.TanggalKontrak = Convert.ToString(dr0["TanggalKontrak"]);
                                                rSingle.TanggalJatuhTempo = Convert.ToString(dr0["TanggalJatuhTempo"]);
                                                rSingle.NomorAdendum = Convert.ToString(dr0["NomorAdendum"]);
                                                rSingle.TanggalAdendum = Convert.ToString(dr0["TanggalAdendum"]);
                                                rSingle.NilaiInvestasiAwalIDR = Convert.ToString(dr0["NilaiInvestasiAwalIDR"]);
                                                rSingle.NilaiInvestasiAwalNonIDR = Convert.ToString(dr0["NilaiInvestasiAwalNonIDR"]);
                                                rSingle.NilaiInvestasiAkhir = Convert.ToString(dr0["NilaiInvestasiAkhir"]);
                                                rSingle.NilaiInvestasiAkhirNonIDR = Convert.ToString(dr0["NilaiInvestasiAkhirNonIDR"]);
                                                rSingle.JenisEfek = Convert.ToString(dr0["JenisEfek"]);
                                                rSingle.KodeKategoriEfek = Convert.ToInt32(dr0["KodeKategoriEfek"]);
                                                rSingle.JumlahEfek = Convert.ToString(dr0["JumlahEfek"]);
                                                rSingle.NilaiPembelian = Convert.ToString(dr0["NilaiPembelian"]);
                                                rSingle.NilaiNominal = Convert.ToString(dr0["NilaiNominal"]);
                                                rSingle.HPW = Convert.ToString(dr0["HPW"]);
                                                rSingle.Deposito = Convert.ToString(dr0["Deposito"]);
                                                rSingle.TotalInvestasi = Convert.ToString(dr0["TotalInvestasi"]);
                                                rSingle.KodeBK = Convert.ToString(dr0["KodeBK"]);
                                                rSingle.Keterangan = Convert.ToString(dr0["Keterangan"]);
                                                rSingle.SID = Convert.ToString(dr0["SID"]);
                                                rList.Add(rSingle);

                                            }

                                            var QueryByClientID =
                                             from r in rList
                                             group r by new { } into rGroup
                                             select rGroup;

                                            int incRowExcel = 0;
                                            int _startRowDetail = 0;
                                            foreach (var rsHeader in QueryByClientID)
                                            {

                                                incRowExcel++;
                                                //Row A = 2
                                                int RowA = incRowExcel;
                                                int RowB = incRowExcel + 1;
                                                worksheet.Cells["A" + incRowExcel + ":V" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + incRowExcel + ":V" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + incRowExcel + ":V" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + incRowExcel + ":V" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Dotted;

                                                worksheet.Cells["A" + incRowExcel + ":V" + incRowExcel].Style.WrapText = true;

                                                worksheet.Cells["A" + incRowExcel + ":V" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells["A" + incRowExcel + ":V" + incRowExcel].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells["A" + incRowExcel + ":V" + incRowExcel].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                                worksheet.Cells["A" + incRowExcel + ":V" + incRowExcel].Style.Fill.BackgroundColor.SetColor(Color.LightSteelBlue);

                                                worksheet.Cells[incRowExcel, 1].Value = "Kode Nasabah";
                                                worksheet.Cells[incRowExcel, 2].Value = "Nama Nasabah";
                                                worksheet.Cells[incRowExcel, 3].Value = "Nomer Kontrak";
                                                worksheet.Cells[incRowExcel, 4].Value = "Tanggal Kontrak";
                                                worksheet.Cells[incRowExcel, 5].Value = "Tanggal Jatuh Tempo";
                                                worksheet.Cells[incRowExcel, 6].Value = "Nomer Adendum";
                                                worksheet.Cells[incRowExcel, 7].Value = "Tanggal Adendum";
                                                worksheet.Cells[incRowExcel, 8].Value = "Nilai Investasi Awal IDR";
                                                worksheet.Cells[incRowExcel, 9].Value = "Nilai Investasi Awal Non IDR";
                                                worksheet.Cells[incRowExcel, 10].Value = "Nilai investtasi Akhir IDR";
                                                worksheet.Cells[incRowExcel, 11].Value = "Nilai investasi Akhir Non IDR";
                                                worksheet.Cells[incRowExcel, 12].Value = "Kode Efek";
                                                worksheet.Cells[incRowExcel, 13].Value = "Kode Kategori Efek";
                                                worksheet.Cells[incRowExcel, 14].Value = "Jumlah Efek";
                                                worksheet.Cells[incRowExcel, 15].Value = "Nilai Pembelian";
                                                worksheet.Cells[incRowExcel, 16].Value = "Nilai Nominal";
                                                worksheet.Cells[incRowExcel, 17].Value = "HPW";
                                                worksheet.Cells[incRowExcel, 18].Value = "Deposito";
                                                worksheet.Cells[incRowExcel, 19].Value = "Total Investtasi";
                                                worksheet.Cells[incRowExcel, 20].Value = "Kode BK";
                                                worksheet.Cells[incRowExcel, 21].Value = "Keterangan";
                                                worksheet.Cells[incRowExcel, 22].Value = "SID";

                                                //area header
                                                int _endRowDetail = 0;
                                                int _startRow = incRowExcel;
                                                incRowExcel++;
                                                _startRowDetail = incRowExcel;
                                                foreach (var rsDetail in rsHeader)
                                                {
                                                    worksheet.Cells[incRowExcel, 1].Value = rsDetail.KodeNasabah;
                                                    worksheet.Cells[incRowExcel, 1].Style.WrapText = true;
                                                    worksheet.Cells[incRowExcel, 2].Value = rsDetail.NamaNasabah;
                                                    worksheet.Cells[incRowExcel, 3].Value = rsDetail.NomorKontrak;
                                                    worksheet.Cells[incRowExcel, 4].Value = rsDetail.TanggalKontrak;
                                                    worksheet.Cells[incRowExcel, 5].Value = rsDetail.TanggalJatuhTempo;
                                                    worksheet.Cells[incRowExcel, 6].Value = rsDetail.NomorAdendum;
                                                    worksheet.Cells[incRowExcel, 7].Value = rsDetail.TanggalAdendum;
                                                    worksheet.Cells[incRowExcel, 8].Value = rsDetail.NilaiInvestasiAwalIDR;
                                                    worksheet.Cells[incRowExcel, 9].Value = rsDetail.NilaiInvestasiAwalNonIDR;
                                                    worksheet.Cells[incRowExcel, 10].Value = rsDetail.NilaiInvestasiAkhir;
                                                    worksheet.Cells[incRowExcel, 11].Value = rsDetail.NilaiInvestasiAkhirNonIDR;
                                                    worksheet.Cells[incRowExcel, 12].Value = rsDetail.JenisEfek;
                                                    worksheet.Cells[incRowExcel, 13].Value = rsDetail.KodeKategoriEfek;
                                                    worksheet.Cells[incRowExcel, 14].Value = rsDetail.JumlahEfek;
                                                    worksheet.Cells[incRowExcel, 15].Value = rsDetail.NilaiPembelian;
                                                    worksheet.Cells[incRowExcel, 16].Value = rsDetail.NilaiNominal;
                                                    worksheet.Cells[incRowExcel, 17].Value = rsDetail.HPW;
                                                    worksheet.Cells[incRowExcel, 18].Value = rsDetail.Deposito;
                                                    worksheet.Cells[incRowExcel, 19].Formula = "(N" + incRowExcel + "+R" + incRowExcel + ")" + "*Q" + incRowExcel;
                                                    worksheet.Cells[incRowExcel, 20].Value = rsDetail.KodeBK;
                                                    worksheet.Cells[incRowExcel, 21].Value = rsDetail.Keterangan;
                                                    worksheet.Cells[incRowExcel, 22].Value = rsDetail.SID;

                                                    _endRowDetail = incRowExcel;

                                                    incRowExcel++;


                                                }

                                                worksheet.Cells["A" + incRowExcel + ":V" + incRowExcel].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                                worksheet.Cells["A" + incRowExcel + ":V" + incRowExcel].Style.Fill.BackgroundColor.SetColor(Color.LightSeaGreen);


                                                worksheet.Cells["A" + _startRow + ":V" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + _startRow + ":V" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + _startRow + ":V" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + incRowExcel + ":V" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                incRowExcel++;
                                            }



                                            worksheet.PrinterSettings.FitToPage = true;
                                            worksheet.PrinterSettings.FitToWidth = 1;
                                            worksheet.PrinterSettings.FitToHeight = 1;
                                            worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel - 1, 22];
                                            worksheet.Column(1).Width = 9;
                                            worksheet.Column(2).Width = 35;
                                            worksheet.Column(3).Width = 20;
                                            worksheet.Column(4).Width = 20;
                                            worksheet.Column(5).Width = 20;
                                            worksheet.Column(6).Width = 20;
                                            worksheet.Column(7).Width = 20;
                                            worksheet.Column(8).Width = 20;
                                            worksheet.Column(9).Width = 20;
                                            worksheet.Column(10).Width = 20;
                                            worksheet.Column(11).Width = 20;
                                            worksheet.Column(12).Width = 20;
                                            worksheet.Column(13).Width = 20;
                                            worksheet.Column(14).Width = 20;
                                            worksheet.Column(15).Width = 20;
                                            worksheet.Column(16).Width = 20;
                                            worksheet.Column(17).Width = 20;
                                            worksheet.Column(18).Width = 20;
                                            worksheet.Column(19).Width = 20;
                                            worksheet.Column(20).Width = 20;
                                            worksheet.Column(21).Width = 20;
                                            worksheet.Column(22).Width = 20;



                                            // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                            //worksheet.Cells["A3:O14"].AutoFitColumns();  // UNTUK AUTO FIT UKURAN TULISAN DENGAN KOLOM, KEBETULAN INI BUAT HEADER AJA DIPAKENYA

                                            worksheet.PrinterSettings.Orientation = eOrientation.Landscape; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                            worksheet.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN
                                            worksheet.HeaderFooter.OddHeader.RightAlignedText = "&12 KPD REPORT";

                                            // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                            worksheet.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
                                            worksheet.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
                                            worksheet.View.PageLayoutView = Tools.DefaultReportPageLayoutView();
                                            worksheet.View.ShowGridLines = Tools.DefaultReportShowGridLines();
                                            //Image img = Image.FromFile(Tools.ReportImage);
                                            //worksheet.HeaderFooter.OddHeader.InsertPicture(img, PictureAlignment.Left);
                                            //worksheet.HeaderFooter.OddHeader.CenteredText = Tools.DefaultReportHeaderCenterText();
                                            //worksheet.HeaderFooter.OddHeader.LeftAlignedText = Tools.DefaultReportHeaderLeftText();

                                            worksheet.HeaderFooter.OddFooter.RightAlignedText =
                                            string.Format("Page {0} of {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);

                                            ////worksheet.HeaderFooter.OddFooter.CenteredText = Tools.DefaultReportFooterCenterText();
                                            //worksheet.HeaderFooter.OddFooter.LeftAlignedText = Tools.DefaultReportFooterLeftText();



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



                #endregion

                return true;
            }//else if
            #endregion

            else
            {
                return false;
            }
        }


    }
}