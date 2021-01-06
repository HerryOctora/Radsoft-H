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
using System.Data.OleDb;
using RFSRepository;


namespace RFSRepositoryTwo
{
    public class CustomClient19Reps
    {
        Host _host = new Host();

        public class PVRRpt
        {
            public int Row { get; set; }
            public decimal Balance { get; set; }
            public string FundName { get; set; }
            public string InstrumentID { get; set; }
            public string InstrumentName { get; set; }

            public decimal UnitQuantity { get; set; }
            public decimal AverageCost { get; set; }
            public decimal BookValue { get; set; }
            public decimal MarketPrice { get; set; }
            public decimal MarketValue { get; set; }
            public decimal UnrealizedProfitLoss { get; set; }
            public decimal PercentProfilLoss { get; set; }
            public decimal Lot { get; set; }
            public string MarketCap { get; set; }
            public string Sector { get; set; }
            public string MaturityDate { get; set; }
            public string BondType { get; set; }
            public string TimeDeposit { get; set; }
            public string BICode { get; set; }
            public string Branch { get; set; }
            public string TradeDate { get; set; }
            public decimal Nominal { get; set; }
            public decimal Rate { get; set; }
            public decimal AccTD { get; set; }
        }
        private class PencairanDeposito
        {
            public string BankCustodian { get; set; }
            public string Address { get; set; }
            public string Attn1 { get; set; }
            public string PhoneAttn1 { get; set; }
            public string FaxAttn1 { get; set; }
            public string Attn2 { get; set; }
            public string PhoneAttn2 { get; set; }
            public string CC1 { get; set; }
            public string PhoneCC1 { get; set; }
            public string CC2 { get; set; }
            public string PhoneCC2 { get; set; }
            public string Remark { get; set; }
            public string Periode { get; set; }
            public string NamaBank { get; set; }
            public decimal Jumlah { get; set; }
            public decimal Rate { get; set; }
            public string Kota { get; set; }
            public string Reference { get; set; }
            public string Instruksi { get; set; }
            public string Currency { get; set; }
            public string NoRek { get; set; }
            public string Fund { get; set; }
            public string BankPlacement { get; set; }
        }

        private class PlacementDeposito
        {
            public string PlacementDate { get; set; }
            public string BankCustodian { get; set; }
            public string Address { get; set; }
            public string Attn1 { get; set; }
            public string PhoneAttn1 { get; set; }
            public string FaxAttn1 { get; set; }
            public string Attn2 { get; set; }
            public string PhoneAttn2 { get; set; }
            public string CC1 { get; set; }
            public string PhoneCC1 { get; set; }
            public string PhoneCC2 { get; set; }
            public string FaxCC1 { get; set; }
            //public string Periode { get; set; }
            public string NamaBank { get; set; }
            public decimal Jumlah { get; set; }
            public decimal Rate { get; set; }
            public string Kota { get; set; }
            public string Reference { get; set; }
            public string Instruksi { get; set; }
            public string Currency { get; set; }
            public string NomorRekeningInstrument { get; set; }
            public string NamaRekeningInstrument { get; set; }
            public string NomorRekeningFund { get; set; }
            public string NamaRekeningFund { get; set; }
            public string Fund { get; set; }
            public string FundID { get; set; }
            public string BankPlacement { get; set; }
            public string BICCode { get; set; }
            public string Cabang { get; set; }
            public string RefNo { get; set; }
            public string PeriodID { get; set; }
            public string Month { get; set; }
            public string ValueDate { get; set; }
            public string MaturityDate { get; set; }
            public decimal NewRate { get; set; }
        }

        private class PerpanjanganDeposito
        {
            public string BankCustodian { get; set; }
            public string Address { get; set; }
            public string Address2 { get; set; }
            public string Address3 { get; set; }
            public string Attn1 { get; set; }
            public string PhoneAttn1 { get; set; }
            public string ext { get; set; }
            public string ext1 { get; set; }
            public string Fax { get; set; }
            public string Attn2 { get; set; }
            public string PhoneAttn2 { get; set; }
            public string CC1 { get; set; }
            public string PhoneCC1 { get; set; }
            public string CC2 { get; set; }
            public string PhoneCC2 { get; set; }
            public string Remark { get; set; }
            public string Periode { get; set; }
            public string NamaBank { get; set; }
            public decimal Jumlah { get; set; }
            public decimal Rate { get; set; }
            public string Kota { get; set; }
            public string Reference { get; set; }
            public string Instruksi { get; set; }
            public string Currency { get; set; }
            public string NoRek { get; set; }
            public string Fund { get; set; }
            public string BankPlacement { get; set; }
            public string KodeBIC { get; set; }
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
            public string FundName { get; set; }

        }

        private class BrokerCommisionSummary
        {
            public string BrokerID { get; set; }
            public string BrokerName { get; set; }
            public decimal Fee { get; set; }
            public decimal NetFee { get; set; }
            public decimal TotalTrading { get; set; }
            public decimal Trading { get; set; }
            public decimal Commission { get; set; }
            public decimal NetCommission { get; set; }
            public decimal Fee1 { get; set; }
            public string Date { get; set; }

        }

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

        public Boolean GenerateReportFundAccounting(string _userID, FundAccountingRpt _FundAccountingRpt)
        {



            #region Transaction Summary
            if (_FundAccountingRpt.ReportName.Equals("Transaction Summary"))
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
                                _paramFundFrom = " And A.FundPK  in ( " + _FundAccountingRpt.FundFrom + " ) ";
                            }
                            else
                            {
                                _paramFundFrom = "";
                            }


                            cmd.CommandText =

                            @"
                            select E.Name, case when A.InstrumentTypePK in (2,3,8,9,11,13,14,15) then 'BOND' when A.InstrumentTypePK = 1 then 'EQUITY' else 'TIME DEPOSIT' end InstrumentTypeName
                            ,A.TrxTypeID,B.ID InstrumentID,ValueDate,SettlementDate,isnull(C.ID,'') CounterpartCode,B.Name InstrumentName,DoneVolume,DonePrice,DoneAmount,
                            sum(TotalAmount - DoneAmount + WHTAmount) TotalBrokerFee,case when A.InstrumentTypePK in (2,3,8,9,11,13,14,15) then sum(IncomeTaxGainAmount+IncomeTaxInterestAmount) when A.InstrumentTypePK = 1 then WHTAmount else 0 end WHTAmount, TotalAmount,dbo.FGetLastAvgFromInvestment(A.ValueDate,A.InstrumentPK,A.FundPK) AcqPrice,sum(DonePrice - dbo.FGetLastAvgFromInvestment(A.ValueDate,A.InstrumentPK,A.FundPK)) * DoneVolume Realised,
                            isnull(DoneAccruedInterest,0) DoneAccruedInterest,A.InterestPercent,isnull(D.Name,'') BankName,case when A.InstrumentTypePK = 5 then isnull(DATEDIFF(day,A.ValueDate ,A.MaturityDate),0) else 0 end Tenor,A.MaturityDate from Investment A 
                            left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2
                            left join Counterpart C on A.CounterpartPK = C.CounterpartPK and C.Status = 2
                            left join Bank D on A.BankPK = D.BankPK and D.Status = 2
                            left join Fund E on A.FundPK = E.FundPK and E.status = 2
                            where StatusSettlement = 2 and valuedate between @ValueDateFrom and @ValueDateTo 
                            group by E.Name,A.TrxTypeID,B.ID,ValueDate,SettlementDate,C.ID,B.Name,DoneVolume,DonePrice,DoneAmount,
                            WHTAmount, TotalAmount,AcqPrice,DoneAccruedInterest,A.InterestPercent,D.Name,A.MaturityDate,A.InstrumentTypePK,A.MaturityDate,A.InstrumentPK,A.FundPK,A.InvestmentPK

                            ";


                            cmd.CommandTimeout = 0;
                            cmd.Parameters.AddWithValue("@ValueDateTo", _FundAccountingRpt.ValueDateTo);
                            cmd.Parameters.AddWithValue("@ValueDateFrom", _FundAccountingRpt.ValueDateFrom);

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
                                            rSingle.Tenor = Convert.ToInt32(dr0["Tenor"]);
                                            rSingle.MaturityDate = dr0["MaturityDate"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["MaturityDate"]);
                                            rList.Add(rSingle);

                                        }

                                        var QueryByFundID =
                                                     from r in rList
                                                     group r by new { r.FundName, r.InstrumentTypeName, r.TrxTypeID } into rGroup
                                                     select rGroup;

                                        int incRowExcel = 1;

                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = _host.Get_CompanyName();
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                        worksheet.Cells["A" + incRowExcel + ":C" + incRowExcel].Merge = true;

                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "Investment Transaction";
                                        incRowExcel = incRowExcel + 2;
                                        worksheet.Cells[incRowExcel, 1].Value = "Period";
                                        worksheet.Cells[incRowExcel, 2].Value = " : " + Convert.ToDateTime(_FundAccountingRpt.ValueDateFrom).ToString("dd-MMMM-yyyy") + "-" + Convert.ToDateTime(_FundAccountingRpt.ValueDateTo).ToString("dd-MMMM-yyyy");
                                        incRowExcel = incRowExcel + 2;

                                        //int incRowExcel = 1;
                                        int RowA = incRowExcel;
                                        incRowExcel = incRowExcel + 1;
                                        foreach (var rsHeader in QueryByFundID)
                                        {

                                            worksheet.Cells[incRowExcel, 1].Value = "Fund Name";
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 2].Value = " : ";
                                            worksheet.Cells[incRowExcel, 3].Value = rsHeader.Key.FundName;
                                            worksheet.Cells["C" + incRowExcel + ":D" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;
                                            incRowExcel++;
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
                                                    worksheet.Cells["C" + RowB + ":O" + RowB].Style.Font.Bold = true;
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

                                            incRowExcel++;
                                            worksheet.Row(incRowExcel).PageBreak = _FundAccountingRpt.PageBreak;
                                            incRowExcel++;

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
                                        worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel - 1, 15];
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
                                        worksheet.Column(13).AutoFit();
                                        worksheet.Column(14).AutoFit();
                                        worksheet.Column(15).AutoFit();
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

            #region Pencairan Deposito
            if (_FundAccountingRpt.ReportName.Equals("Pencairan Deposito"))
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
                                _paramFund = "And F.FundPK in ( " + _FundAccountingRpt.FundFrom + " ) ";
                            }
                            else
                            {
                                _paramFund = "";

                            }

                            //                            cmd.CommandText =
                            //                                @"
                            //                                select 'Jakarta' Kota, 'No. 042/STL-FA-ARO/I/2018' Reference,'PT Bank Mega Tbk' BankCustodian,'Sunter Hijau Raya' Address, 'Euis Nurjanah' Attn1, '021-79175000' PhoneAttn1
                            //                                , '021-79187100' FaxAttn1,'Ita' Attn2, '0822 0822 5466' PhoneAttn2, 'Budi Winoto' CC1, '081-808187519' PhoneCC1, '021 290-2740' PhoneCC2, '021 290-27241' FaxCC1,'Penempatan Deposito a/n  Reksa Dana Aurora Likuid di ' Remarks, 
                            //                                'No. 027/STL-FA-ARO/XII/2017' Instruksi, D.ID Currency, A.NAV Jumlah, '29 Desember 2017 s/d 29 Januari 2018' Periode, A.UnitAmount Rate, '01.074.0011.241076' NomorRekening,
                            //                                'MANDIRI' NamaBank, 'Thamrin' Cabang, 'GNESIDJA' BICCode, 'PT BANK GANESHA MEGA KUNINGAN' BankPlacement, B.Name Fund from ClientSubscription A 
                            //                                left join Fund B on A.FundPK = B.FundPK and B.Status in (1,2) 
                            //                                left join BankBranch C on B.BankBranchPK = C.BankBranchPK and C.Status in (1,2)
                            //                                left join Currency D on B.CurrencyPK = D.CurrencyPK and D.Status in (1,2)
                            //                                ";

                            cmd.CommandText =
                            @"Declare @EndDayTrailsPK int
                            Declare @MaxDateEndDayFP datetime
                            Declare @PeriodPK int

                            select @PeriodPK = PeriodPK from Period where status = 2 and @valuedate between DateFrom and DateTo

                            select @EndDayTrailsPK = EndDayTrailsFundPortfolioPK, @MaxDateEndDayFP = ValueDate from EndDayTrailsFundPortfolio 
                            where ValueDate = 
                            (
                            select max(valueDate) from EndDayTrailsFundPortfolio where status = 2 and valueDate < @valuedate 
                            and status = 2) and status = 2

                            select 'Jakarta' Kota,Reference,C.ID BankCustodian,C.Address,C.ContactPerson Attn1,C.Phone1 PhoneAttn1,C.Fax1 FaxAttn1,C.Phone2 PhoneAttn2,A.BreakInterestPercent/100 NewRate,
                            D.ContactPerson CC1,D.Phone1 PhoneCC1, D.Phone2 PhoneCC2,D.Fax1 FaxCC1,A.InvestmentNotes Remarks,'' Instruksi, A.AcqDate PlacementDate,
                            E.ID Currency,A.DoneAmount Jumlah,A.ValueDate ValueDate,A.MaturityDate MaturityDate,A.InterestPercent/100 Rate,
                            D.BankAccountNo NomorRekeningInstrument,
                            D.BankAccountName NamaRekeningInstrument,
                            '' Cabang,
                            G.BICode BICCode,
                            G.Name BankPlacement,
                            F.BankAccountNo NomorRekeningFund,
                            F.BankAccountName NamaRekeningFund,
                            I.Name Fund,cast(substring(reference,1,charindex('/',reference,1) - 1) as integer) RefNo,H.ID PeriodID,dbo.ConvertIntToRoman(month(@ValueDate)) Month from investment A
                            left join Fund B on A.FundPK = B.FundPK and B.status in (1,2)
                            left join BankBranch C on B.BankBranchPK = C.BankBranchPK and C.Status in (1,2)
                            left join BankBranch D on A.BankBranchPK = D.BankBranchPK and D.Status in (1,2)
                            left join Currency E on B.CurrencyPK = E.CurrencyPK and E.Status in (1,2)
                            left join FundCashRef F on A.FundCashRefPK = F.FundCashRefPK and F.Status in (1,2)
                            left join Bank G on A.BankPK = G.BankPK and G.Status in (1,2)
                            left join Period H on A.PeriodPK = H.PeriodPK and H.Status in (1,2)
                            left join Fund I on A.FundPK = I.FundPK and I.Status in (1,2)
                            where valuedate = @valuedate and InstrumentTypePK = 5 and StatusSettlement = 2 and A.trxType = 2 
                            union all


                            select 'Jakarta' Kota,A.Reference, 
                            BC.ID BankCustodian,BC.Address,BC.ContactPerson Attn1,BC.Phone1 PhoneAttn1,BC.Fax1 FaxAttn1,BC.Phone2 PhoneAttn2,A.BreakInterestPercent/100 NewRate,
                            B.ContactPerson CC1,B.Phone1 PhoneCC1, B.Phone2 PhoneCC2,B.Fax1 FaxCC1,A.InvestmentNotes Remarks,'' Instruksi, A.AcqDate PlacementDate,
                            E.ID Currency,A.DoneAmount Jumlah,A.ValueDate ValueDate,A.MaturityDate MaturityDate,A.InterestPercent/100 Rate,
                            B.BankAccountNo NomorRekeningInstrument,
                            B.BankAccountName NamaRekeningInstrument,
                            '' Cabang,
                            C.BICode BICCode,
                            C.Name BankPlacement,
                            G.BankAccountNo NomorRekeningFund,
                            G.BankAccountName NamaRekeningFund,
                            F.Name Fund,cast(substring(reference,1,charindex('/',reference,1) - 1) as integer) RefNo,H.ID PeriodID,dbo.ConvertIntToRoman(month(@ValueDate)) Month

                            from Investment A
                            left join Instrument I on A.InstrumentPK = I.InstrumentPK and I.status = 2
                            left join InstrumentType D on I.InstrumentTypePK = D.InstrumentTypePK and D.status = 2
                            left join Fund F on A.FundPK = F.FundPK and F.status = 2  
                            left join BankBranch BC on BC.BankBranchPK = F.BankBranchPK and BC.status = 2  
                            left join Bank BB on BB.BankPK = BC.BankPK and BB.status = 2 
                            left join BankBranch B on A.BankBranchPK = B.BankBranchPK and B.status = 2 
                            left join Bank C on B.BankPK = C.BankPK and C.status = 2 
                            left join Currency E on F.CurrencyPK = E.CurrencyPK and E.Status in (1,2)
                            left join Period H on A.PeriodPK = H.PeriodPK and H.Status in (1,2)
                            left join FundCashRef G on A.FundCashRefPK = G.FundCashRefPK and G.Status in (1,2)
                            where A.InstrumentTypePK = 5 and A.MaturityDate = @valuedate  and StatusSettlement in (2)
                            and A.InstrumentPK not in  (
                            select InstrumentPK	from Investment where StatusSettlement = 2 and TrxType in (2) and MaturityDate = @valuedate
                            ) and A.PeriodPK = @PeriodPK



                             union all
                       
                            select 'Jakarta' Kota,CONVERT(varchar(10), A.FundEndYearPortfolioPK) + '/FP/' + REPLACE(RIGHT(CONVERT(VARCHAR(8), A.MaturityDate, 3), 5) ,'/','') Reference, 
                            BC.ID BankCustodian,BC.Address,BC.ContactPerson Attn1,BC.Phone1 PhoneAttn1,BC.Fax1 FaxAttn1,BC.Phone2 PhoneAttn2,0 NewRate,
                            B.ContactPerson CC1,B.Phone1 PhoneCC1, B.Phone2 PhoneCC2,B.Fax1 FaxCC1,'' Remarks,'' Instruksi,  A.AcqDate PlacementDate,
                            E.ID Currency,A.Volume Jumlah,@valuedate ValueDate,A.MaturityDate MaturityDate,A.InterestPercent/100 Rate,
                            B.BankAccountNo NomorRekeningInstrument,
                            B.BankAccountName NamaRekeningInstrument,
                            '' Cabang,
                            C.BICode BICCode,
                            C.Name BankPlacement,
                            G.BankAccountNo NomorRekeningFund,
                            G.BankAccountName NamaRekeningFund,
                            F.Name Fund,'' RefNo,H.ID PeriodID,dbo.ConvertIntToRoman(month(@ValueDate)) Month

                            from FundEndYearPortfolio A
                            left join Instrument I on A.InstrumentPK = I.InstrumentPK and I.status = 2
                            left join InstrumentType D on I.InstrumentTypePK = D.InstrumentTypePK and D.status = 2
                            left join Fund F on A.FundPK = F.FundPK and F.status = 2  
                            left join BankBranch BC on BC.BankBranchPK = F.BankBranchPK and BC.status = 2  
                            left join Bank BB on BB.BankPK = BC.BankPK and BB.status = 2 
                            left join BankBranch B on A.BankBranchPK = B.BankBranchPK and B.status = 2 
                            left join Bank C on B.BankPK = C.BankPK and C.status = 2 
                            left join Currency E on F.CurrencyPK = E.CurrencyPK and E.Status in (1,2)
                            left join Period H on A.PeriodPK = H.PeriodPK and H.Status in (1,2)
	                         left join FundCashRef G on F.FundPK = G.FundPK and G.Status in (1,2)
                            where A.MaturityDate = @valuedate and I.InstrumentTypePK = 5 and A.PeriodPK = @PeriodPK ";

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
                                    string filePath = Tools.ReportsPath + "PencairanDeposito" + "_" + _userID + ".xlsx";
                                    string pdfPath = Tools.ReportsPath + "PencairanDeposito" + "_" + _userID + ".pdf";
                                    FileInfo excelFile = new FileInfo(filePath);
                                    if (excelFile.Exists)
                                    {
                                        excelFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                        excelFile = new FileInfo(filePath);
                                    }


                                    // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                    using (ExcelPackage package = new ExcelPackage(excelFile))
                                    {
                                        package.Workbook.Properties.Title = "PencairanDeposito";
                                        package.Workbook.Properties.Author = Tools.DefaultReportAuthor();
                                        package.Workbook.Properties.Comments = Tools.DefaultReportComments();
                                        package.Workbook.Properties.Company = Tools.DefaultReportCompany();
                                        package.Workbook.Properties.SetCustomPropertyValue("Checked by", _userID);
                                        package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", Tools.DefaultReportAssemblyName());

                                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Pencairan Deposito");



                                        //ATUR DATA GROUPINGNYA DULU
                                        List<PlacementDeposito> rList = new List<PlacementDeposito>();
                                        while (dr0.Read())
                                        {
                                            PlacementDeposito rSingle = new PlacementDeposito();
                                            rSingle.Kota = Convert.ToString(dr0["Kota"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["Kota"]));
                                            rSingle.Reference = Convert.ToString(dr0["Reference"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["Reference"]));
                                            rSingle.BankCustodian = Convert.ToString(dr0["BankCustodian"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["BankCustodian"]));
                                            rSingle.Address = Convert.ToString(dr0["Address"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["Address"]));
                                            rSingle.Attn1 = Convert.ToString(dr0["Attn1"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["Attn1"]));
                                            rSingle.PhoneAttn1 = Convert.ToString(dr0["PhoneAttn1"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["PhoneAttn1"]));
                                            rSingle.FaxAttn1 = Convert.ToString(dr0["FaxAttn1"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["FaxAttn1"]));
                                            rSingle.BankPlacement = Convert.ToString(dr0["BankPlacement"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["BankPlacement"]));
                                            //rSingle.Attn2 = Convert.ToString(dr0["Attn2"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["Attn2"]));
                                            rSingle.PhoneAttn2 = Convert.ToString(dr0["PhoneAttn2"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["PhoneAttn2"]));
                                            rSingle.CC1 = Convert.ToString(dr0["CC1"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["CC1"]));
                                            rSingle.PhoneCC1 = Convert.ToString(dr0["PhoneCC1"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["PhoneCC1"]));
                                            rSingle.PhoneCC2 = Convert.ToString(dr0["PhoneCC2"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["PhoneCC2"]));
                                            rSingle.FaxCC1 = Convert.ToString(dr0["FaxCC1"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["FaxCC1"]));
                                            rSingle.Instruksi = Convert.ToString(dr0["Instruksi"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["Instruksi"]));
                                            rSingle.PlacementDate = Convert.ToString(dr0["PlacementDate"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["PlacementDate"]));
                                            rSingle.Currency = Convert.ToString(dr0["Currency"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["Currency"]));
                                            rSingle.Jumlah = Convert.ToDecimal(dr0["Jumlah"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["Jumlah"]));
                                            //rSingle.Periode = Convert.ToString(dr0["Periode"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["Periode"]));
                                            rSingle.Rate = Convert.ToDecimal(dr0["Rate"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["Rate"]));
                                            rSingle.Fund = Convert.ToString(dr0["Fund"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["Fund"]));
                                            rSingle.NomorRekeningInstrument = Convert.ToString(dr0["NomorRekeningInstrument"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["NomorRekeningInstrument"]));
                                            rSingle.NamaRekeningInstrument = Convert.ToString(dr0["NamaRekeningInstrument"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["NamaRekeningInstrument"]));
                                            rSingle.NomorRekeningFund = Convert.ToString(dr0["NomorRekeningFund"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["NomorRekeningFund"]));
                                            rSingle.NamaRekeningFund = Convert.ToString(dr0["NamaRekeningFund"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["NamaRekeningFund"]));
                                            rSingle.BICCode = Convert.ToString(dr0["BICCode"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["BICCode"]));
                                            rSingle.Cabang = Convert.ToString(dr0["Cabang"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["Cabang"]));
                                            rSingle.RefNo = Convert.ToString(dr0["RefNo"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["RefNo"]));
                                            rSingle.PeriodID = Convert.ToString(dr0["PeriodID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["PeriodID"]));
                                            rSingle.Month = Convert.ToString(dr0["Month"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["Month"]));
                                            rSingle.ValueDate = Convert.ToString(dr0["ValueDate"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["ValueDate"]));
                                            rSingle.MaturityDate = Convert.ToString(dr0["MaturityDate"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["MaturityDate"]));
                                            rSingle.NewRate = Convert.ToDecimal(dr0["NewRate"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["NewRate"]));
                                            rList.Add(rSingle);

                                        }

                                        var QueryBySales =
                                         from r in rList
                                         orderby r.BankCustodian ascending
                                         group r by new { } into rGroup
                                         select rGroup;


                                        int incRowExcel = 0;

                                        incRowExcel++;

                                        foreach (var rsHeader in QueryBySales)
                                        {

                                            incRowExcel++;
                                            foreach (var rsDetail in rsHeader)
                                            {
                                                incRowExcel = incRowExcel + 5;
                                                worksheet.Cells[incRowExcel, 4].Value = rsDetail.Kota + "," + Convert.ToDateTime(_FundAccountingRpt.ValueDateFrom).ToString("dd MMMM yyyy");
                                                worksheet.Cells[incRowExcel, 4].Style.Font.Size = 15;
                                                worksheet.Cells[incRowExcel, 1].Value = "No. MM" + rsDetail.RefNo + "/" + rsDetail.FundID + "-FA/IAM /" + rsDetail.Month + "/" + rsDetail.PeriodID;
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Size = 15;
                                                incRowExcel = incRowExcel + 3;
                                                worksheet.Cells[incRowExcel, 1].Value = "Kepada Yth,";
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Size = 16;
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = rsDetail.BankCustodian;
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Size = 16;
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                                incRowExcel = incRowExcel + 2;
                                                worksheet.Cells[incRowExcel, 1].Value = rsDetail.Address;
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Size = 16;
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                                incRowExcel = incRowExcel + 2;
                                                worksheet.Cells[incRowExcel, 1].Value = "Attn.";
                                                worksheet.Cells[incRowExcel, 1].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                                                worksheet.Row(incRowExcel).Height = 30;
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Size = 16;
                                                worksheet.Cells["B" + incRowExcel + ":C" + incRowExcel].Merge = true;
                                                worksheet.Cells[incRowExcel, 2].Value = " : " + rsDetail.Attn1 + " ( Telp. " + rsDetail.PhoneAttn1 + " / " + "Telp. " + rsDetail.PhoneAttn2 + " ) ";
                                                worksheet.Cells[incRowExcel, 2].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                                                worksheet.Cells[incRowExcel, 2].Style.Font.Size = 16;
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = "Cc.";
                                                worksheet.Cells[incRowExcel, 1].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                                                worksheet.Row(incRowExcel).Height = 30;
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Size = 16;
                                                worksheet.Cells["B" + incRowExcel + ":D" + incRowExcel].Merge = true;
                                                worksheet.Cells[incRowExcel, 2].Value = " : " + rsDetail.BankPlacement + " - " + rsDetail.CC1 + "/" + "Telp. " + rsDetail.PhoneCC1 + ")";
                                                worksheet.Cells[incRowExcel, 2].Style.Font.Size = 16;
                                                incRowExcel++;
                                                incRowExcel = incRowExcel + 2;
                                                worksheet.Cells[incRowExcel, 1].Value = "Perihal : Pencairan Deposito Breakable a/n." + rsDetail.Fund ;
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Size = 16;
                                                worksheet.Cells[incRowExcel, 1].Style.Font.UnderLine = true;
                                                incRowExcel = incRowExcel + 2;
                                                worksheet.Cells[incRowExcel, 1].Value = "Dengan hormat";
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Size = 16;
                                                incRowExcel = incRowExcel + 2;
                                                worksheet.Cells[incRowExcel, 1].Value = "Sesuai dengan pembicaraan via telepon, dengan ini kami konfirmasikan bahwa kami akan mencairkan deposito dengan";
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Size = 16;
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = "perincian sebagai berikut: ";
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Size = 16;
                                                incRowExcel = incRowExcel + 2;
                                                worksheet.Cells[incRowExcel, 1].Value = "Nominal";
                                                worksheet.Cells[incRowExcel, 2].Value = " : " + rsDetail.Currency + " " + Convert.ToDecimal(rsDetail.Jumlah).ToString("#,##0.00");
                                                //worksheet.Cells[incRowExcel, 2].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Size = 16;
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 2].Style.Font.Size = 16;
                                                worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = "Periode";
                                                worksheet.Cells[incRowExcel, 2].Value = " : " + Convert.ToDateTime(rsDetail.PlacementDate).ToString("dd MMMM yyyy") + " s/d " + Convert.ToDateTime(rsDetail.MaturityDate).ToString("dd MMMM yyyy");
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Size = 16;
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 2].Style.Font.Size = 16;
                                                worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = "Rate";
                                                worksheet.Cells[incRowExcel, 2].Value = " : " + Convert.ToDecimal(rsDetail.Rate).ToString("#,##0.00%") + " p.a ";
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Size = 16;
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 2].Style.Font.Size = 16;
                                                worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = "Tanggal Pencairan";
                                                worksheet.Cells[incRowExcel, 2].Value = " : " + Convert.ToDateTime(rsDetail.ValueDate).ToString("dd MMMM yyyy");
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Size = 16;
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 2].Style.Font.Size = 16;
                                                worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = "Rate Pencairan";
                                                worksheet.Cells[incRowExcel, 2].Value = " : " + Convert.ToDecimal(rsDetail.NewRate).ToString("#,##0.00%") + " p.a ";
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Size = 16;
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 2].Style.Font.Size = 16;
                                                worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = "Atas Nama";
                                                worksheet.Cells[incRowExcel, 2].Value = " : " + rsDetail.Fund;
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Size = 16;
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 2].Style.Font.Size = 16;
                                                worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                                incRowExcel = incRowExcel + 2;
                                                worksheet.Cells[incRowExcel, 1].Value = "Mohon pokok dan bunga deposito ditransfer ke rekening sebagai berikut: ";
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Size = 16;
                                                incRowExcel = incRowExcel + 2;
                                                worksheet.Cells[incRowExcel, 1].Value = "Nama Rekening";
                                                worksheet.Cells[incRowExcel, 2].Value = " : " + rsDetail.NamaRekeningFund;
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Size = 16;
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 2].Style.Font.Size = 16;
                                                worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = "Nomor Rekening";
                                                worksheet.Cells[incRowExcel, 2].Value = " : " + rsDetail.NomorRekeningFund;
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Size = 16;
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 2].Style.Font.Size = 16;
                                                worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = "Nama Bank";
                                                worksheet.Cells[incRowExcel, 2].Value = " : " + rsDetail.BankCustodian;
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Size = 16;
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 2].Style.Font.Size = 16;
                                                worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                                incRowExcel = incRowExcel + 2;
                                                worksheet.Cells[incRowExcel, 1].Value = "Untuk informasi lebih lanjut dapat menghubungi Bank Kustodian kami: ";
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Size = 16;
                                                incRowExcel = incRowExcel + 2;
                                                worksheet.Cells[incRowExcel, 1].Value = rsDetail.BankCustodian;
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Size = 16;
                                              
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = rsDetail.Address;
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Size = 16;
                                                incRowExcel = incRowExcel + 2;
                                                worksheet.Cells[incRowExcel, 1].Value = "Demikian kami sampaikan, atas perhatian dan kerjasamanya kami ucapkan terima kasih.";
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Size = 16;
                                                incRowExcel = incRowExcel + 2;
                                                worksheet.Cells[incRowExcel, 1].Value = "Hormat kami,";
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Size = 16;
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = rsDetail.Fund;
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Size = 16;
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                                incRowExcel = incRowExcel + 10;
                                                worksheet.Cells[incRowExcel, 1].Value = _host.Get_SignatureName(_FundAccountingRpt.Signature1);
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Size = 16;
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 3].Value = _host.Get_SignatureName(_FundAccountingRpt.Signature2);
                                                worksheet.Cells[incRowExcel, 3].Style.Font.Size = 16;
                                                worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = _host.Get_PositionSignature(_FundAccountingRpt.Signature1);
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Size = 16;
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Italic = true;
                                                worksheet.Cells[incRowExcel, 3].Value = _host.Get_PositionSignature(_FundAccountingRpt.Signature2);
                                                worksheet.Cells[incRowExcel, 3].Style.Font.Size = 16;
                                                worksheet.Cells[incRowExcel, 3].Style.Font.Italic = true;





                                                incRowExcel = incRowExcel + 2;
                                                worksheet.Row(incRowExcel).PageBreak = _FundAccountingRpt.PageBreak;


                                            }
                                        }


                                        worksheet.PrinterSettings.FitToPage = true;
                                        worksheet.PrinterSettings.FitToWidth = 1;
                                        worksheet.PrinterSettings.FitToHeight = 0;
                                        worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel - 1, 4];
                                        worksheet.Column(1).Width = 25;
                                        worksheet.Column(2).Width = 50;
                                        worksheet.Column(3).Width = 50;
                                        worksheet.Column(4).Width = 30;


                                        // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                        //worksheet.Cells.AutoFitColumns(); // UNTUK AUTO FIT UKURAN TULISAN DENGAN KOLOM, KEBETULAN INI BUAT HEADER AJA DIPAKENYA
                                        worksheet.PrinterSettings.Orientation = eOrientation.Portrait; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                        //worksheet.HeaderFooter.OddHeader.RightAlignedText = "&12 Placement Deposito";

                                        // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                        worksheet.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
                                        worksheet.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
                                        worksheet.View.PageLayoutView = Tools.DefaultReportPageLayoutView();
                                        worksheet.View.ShowGridLines = Tools.DefaultReportShowGridLines();
                                        //worksheet.HeaderFooter.OddHeader.CenteredText = Tools.DefaultReportHeaderCenterText();
                                        //worksheet.HeaderFooter.OddHeader.LeftAlignedText = Tools.DefaultReportHeaderLeftText();

                                        Image img = Image.FromFile(Tools.ReportImage);
                                        worksheet.HeaderFooter.OddHeader.InsertPicture(img, PictureAlignment.Left);
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
            #region Placement Deposito
            if (_FundAccountingRpt.ReportName.Equals("Placement Deposito"))
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
                                _paramFund = "And F.FundPK in ( " + _FundAccountingRpt.FundFrom + " ) ";
                            }
                            else
                            {
                                _paramFund = "";
                            }

                            //                            cmd.CommandText =
                            //                                @"
                            //                                select 'Jakarta' Kota, 'No. 042/STL-FA-ARO/I/2018' Reference,'PT Bank Mega Tbk' BankCustodian,'Sunter Hijau Raya' Address, 'Euis Nurjanah' Attn1, '021-79175000' PhoneAttn1
                            //                                , '021-79187100' FaxAttn1,'Ita' Attn2, '0822 0822 5466' PhoneAttn2, 'Budi Winoto' CC1, '081-808187519' PhoneCC1, '021 290-2740' PhoneCC2, '021 290-27241' FaxCC1,'Penempatan Deposito a/n  Reksa Dana Aurora Likuid di ' Remarks, 
                            //                                'No. 027/STL-FA-ARO/XII/2017' Instruksi, D.ID Currency, A.NAV Jumlah, '29 Desember 2017 s/d 29 Januari 2018' Periode, A.UnitAmount Rate, '01.074.0011.241076' NomorRekening,
                            //                                'MANDIRI' NamaBank, 'Thamrin' Cabang, 'GNESIDJA' BICCode, 'PT BANK GANESHA MEGA KUNINGAN' BankPlacement, B.Name Fund from ClientSubscription A 
                            //                                left join Fund B on A.FundPK = B.FundPK and B.Status in (1,2) 
                            //                                left join BankBranch C on B.BankBranchPK = C.BankBranchPK and C.Status in (1,2)
                            //                                left join Currency D on B.CurrencyPK = D.CurrencyPK and D.Status in (1,2)
                            //                                ";

                            cmd.CommandText =
                            @"select 'Jakarta' Kota,Reference,C.ID BankCustodian,C.Address,C.ContactPerson Attn1,C.Phone1 PhoneAttn1,C.Fax1 FaxAttn1,C.Phone2 PhoneAttn2,
                            D.ContactPerson CC1,D.Phone1 PhoneCC1, D.Phone2 PhoneCC2,D.Fax1 FaxCC1,A.InvestmentNotes Remarks,'' Instruksi,
                            E.ID Currency,A.DoneAmount Jumlah,A.ValueDate ValueDate,A.MaturityDate MaturityDate,A.InterestPercent/100 Rate,
                            D.BankAccountNo NomorRekeningInstrument,
                            D.BankAccountName NamaRekeningInstrument,
                            '' Cabang,
                            G.BICode BICCode,
                            G.Name BankPlacement,
                            F.BankAccountNo NomorRekeningFund,
                            F.BankAccountName NamaRekeningFund,
                            I.Name Fund, I.ID FundID,cast(substring(reference,1,charindex('/',reference,1) - 1) as integer) RefNo,H.ID PeriodID,dbo.ConvertIntToRoman(month(@ValueDate)) Month,* from investment A
                            left join Fund B on A.FundPK = B.FundPK and B.status in (1,2)
                            left join BankBranch C on B.BankBranchPK = C.BankBranchPK and C.Status in (1,2)
                            left join BankBranch D on A.BankBranchPK = D.BankBranchPK and D.Status in (1,2)
                            left join Currency E on B.CurrencyPK = E.CurrencyPK and E.Status in (1,2)
                            left join FundCashRef F on A.FundCashRefPK = F.FundCashRefPK and F.Status in (1,2)
                            left join Bank G on A.BankPK = G.BankPK and G.Status in (1,2)
                            left join Period H on A.PeriodPK = H.PeriodPK and H.Status in (1,2)
                            left join Fund I on A.FundPK = I.FundPK and I.Status in (1,2)
                            where valuedate = @valuedate and InstrumentTypePK = 5 and StatusSettlement = 2 ";

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
                                    string filePath = Tools.ReportsPath + "PlacementDeposito" + "_" + _userID + ".xlsx";
                                    string pdfPath = Tools.ReportsPath + "PlacementDeposito" + "_" + _userID + ".pdf";
                                    FileInfo excelFile = new FileInfo(filePath);
                                    if (excelFile.Exists)
                                    {
                                        excelFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                        excelFile = new FileInfo(filePath);
                                    }


                                    // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                    using (ExcelPackage package = new ExcelPackage(excelFile))
                                    {
                                        package.Workbook.Properties.Title = "PlacementDeposito";
                                        package.Workbook.Properties.Author = Tools.DefaultReportAuthor();
                                        package.Workbook.Properties.Comments = Tools.DefaultReportComments();
                                        package.Workbook.Properties.Company = Tools.DefaultReportCompany();
                                        package.Workbook.Properties.SetCustomPropertyValue("Checked by", _userID);
                                        package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", Tools.DefaultReportAssemblyName());

                                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Placement Deposito");



                                        //ATUR DATA GROUPINGNYA DULU
                                        List<PlacementDeposito> rList = new List<PlacementDeposito>();
                                        while (dr0.Read())
                                        {
                                            PlacementDeposito rSingle = new PlacementDeposito();
                                            rSingle.Kota = Convert.ToString(dr0["Kota"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["Kota"]));
                                            rSingle.Reference = Convert.ToString(dr0["Reference"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["Reference"]));
                                            rSingle.BankCustodian = Convert.ToString(dr0["BankCustodian"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["BankCustodian"]));
                                            rSingle.Address = Convert.ToString(dr0["Address"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["Address"]));
                                            rSingle.Attn1 = Convert.ToString(dr0["Attn1"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["Attn1"]));
                                            rSingle.PhoneAttn1 = Convert.ToString(dr0["PhoneAttn1"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["PhoneAttn1"]));
                                            rSingle.FaxAttn1 = Convert.ToString(dr0["FaxAttn1"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["FaxAttn1"]));
                                            rSingle.BankPlacement = Convert.ToString(dr0["BankPlacement"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["BankPlacement"]));
                                            rSingle.Attn2 = Convert.ToString(dr0["Attn2"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["Attn2"]));
                                            rSingle.PhoneAttn2 = Convert.ToString(dr0["PhoneAttn2"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["PhoneAttn2"]));
                                            rSingle.CC1 = Convert.ToString(dr0["CC1"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["CC1"]));
                                            rSingle.PhoneCC1 = Convert.ToString(dr0["PhoneCC1"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["PhoneCC1"]));
                                            rSingle.PhoneCC2 = Convert.ToString(dr0["PhoneCC2"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["PhoneCC2"]));
                                            rSingle.FaxCC1 = Convert.ToString(dr0["FaxCC1"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["FaxCC1"]));
                                            rSingle.Instruksi = Convert.ToString(dr0["Instruksi"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["Instruksi"]));
                                            rSingle.Currency = Convert.ToString(dr0["Currency"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["Currency"]));
                                            rSingle.Jumlah = Convert.ToDecimal(dr0["Jumlah"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["Jumlah"]));
                                            //rSingle.Periode = Convert.ToString(dr0["Periode"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["Periode"]));
                                            rSingle.Rate = Convert.ToDecimal(dr0["Rate"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["Rate"]));
                                            rSingle.Fund = Convert.ToString(dr0["Fund"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["Fund"]));
                                            rSingle.FundID = Convert.ToString(dr0["FundID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["FundID"]));
                                            rSingle.NomorRekeningInstrument = Convert.ToString(dr0["NomorRekeningInstrument"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["NomorRekeningInstrument"]));
                                            rSingle.NamaRekeningInstrument = Convert.ToString(dr0["NamaRekeningInstrument"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["NamaRekeningInstrument"]));
                                            rSingle.NomorRekeningFund = Convert.ToString(dr0["NomorRekeningFund"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["NomorRekeningFund"]));
                                            rSingle.NamaRekeningFund = Convert.ToString(dr0["NamaRekeningFund"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["NamaRekeningFund"]));
                                            rSingle.BICCode = Convert.ToString(dr0["BICCode"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["BICCode"]));
                                            rSingle.Cabang = Convert.ToString(dr0["Cabang"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["Cabang"]));
                                            rSingle.RefNo = Convert.ToString(dr0["RefNo"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["RefNo"]));
                                            rSingle.PeriodID = Convert.ToString(dr0["PeriodID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["PeriodID"]));
                                            rSingle.Month = Convert.ToString(dr0["Month"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["Month"]));
                                            rSingle.ValueDate = Convert.ToString(dr0["ValueDate"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["ValueDate"]));
                                            rSingle.MaturityDate = Convert.ToString(dr0["MaturityDate"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["MaturityDate"]));
                                            rList.Add(rSingle);

                                        }

                                        var QueryBySales =
                                         from r in rList
                                         orderby r.BankCustodian ascending
                                         group r by new { } into rGroup
                                         select rGroup;


                                        int incRowExcel = 0;

                                        incRowExcel++;

                                        foreach (var rsHeader in QueryBySales)
                                        {

                                            incRowExcel++;
                                            foreach (var rsDetail in rsHeader)
                                            {
                                                incRowExcel = incRowExcel + 5;
                                                worksheet.Cells[incRowExcel, 4].Value = rsDetail.Kota + "," + Convert.ToDateTime(_FundAccountingRpt.ValueDateFrom).ToString("dd MMMM yyyy");
                                              
                                                worksheet.Cells[incRowExcel, 4].Style.Font.Size = 15;
                                                worksheet.Cells[incRowExcel, 1].Value = "No. MM" + rsDetail.RefNo + "/" + "-FA/IAM /" + rsDetail.Month + "/" + rsDetail.PeriodID;
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Size = 15;
                                                incRowExcel = incRowExcel + 3;
                                                worksheet.Cells[incRowExcel, 1].Value = "Kepada Yth.";
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Size = 16;
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = rsDetail.BankCustodian;
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Size = 16;
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = rsDetail.Address;
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Size = 16;
                                                incRowExcel = incRowExcel + 2;
                                                worksheet.Cells[incRowExcel, 1].Value = "Attn.";
                                                worksheet.Cells[incRowExcel, 1].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                                                worksheet.Row(incRowExcel).Height = 30;
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Size = 16;
                                                worksheet.Cells["B" + incRowExcel + ":C" + incRowExcel].Merge = true;
                                                worksheet.Cells[incRowExcel, 2].Value = " : " + rsDetail.Attn1 + "/" + "Telp. " + rsDetail.PhoneAttn1 + " / " + "Telp. " + rsDetail.PhoneAttn2 + " ) ";
                                                worksheet.Cells[incRowExcel, 2].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                                                worksheet.Cells[incRowExcel, 2].Style.Font.Size = 16;
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = "Cc.";
                                                worksheet.Cells[incRowExcel, 1].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                                                worksheet.Row(incRowExcel).Height = 30;
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Size = 16;
                                                worksheet.Cells["B" + incRowExcel + ":D" + incRowExcel].Merge = true;
                                                worksheet.Cells[incRowExcel, 2].Value = " : " + rsDetail.BankPlacement + " - " + rsDetail.CC1 + "/" + "Telp. " + rsDetail.PhoneCC1 + ")" ;
                                                worksheet.Cells[incRowExcel, 2].Style.Font.Size = 16;                                           
                                                incRowExcel++;
                                                incRowExcel = incRowExcel + 2;
                                                worksheet.Cells[incRowExcel, 1].Value = "Perihal : Penempatan Deposito a/n " + rsDetail.Fund;
                                                worksheet.Cells[incRowExcel, 1].Style.Font.UnderLine = true;
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Size = 16;
                                                incRowExcel = incRowExcel + 2;
                                                worksheet.Cells[incRowExcel, 1].Value = "Dengan hormat,";
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Size = 16;
                                                incRowExcel = incRowExcel + 2;
                                                worksheet.Cells[incRowExcel, 1].Value = "Sesuai dengan pembicaraan via telepon, dengan ini kami konfirmasikan penempatan deposito (breakable) dengan  ";
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Size = 16;
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = "perincian sebagai berikut: ";
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Size = 16;
                                                incRowExcel = incRowExcel + 2;
                                                worksheet.Cells[incRowExcel, 1].Value = "Jumlah";
                                                worksheet.Cells[incRowExcel, 2].Value = " : " + rsDetail.Currency + " " + Convert.ToDecimal(rsDetail.Jumlah).ToString("#,##0.00");
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Size = 16;
                                                worksheet.Cells[incRowExcel, 2].Style.Font.Size = 16;
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                                //worksheet.Cells[incRowExcel, 2].Style.Numberformat.Format = "#,##0.00";
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = "Periode";
                                                worksheet.Cells[incRowExcel, 2].Value = " : " + Convert.ToDateTime(rsDetail.ValueDate).ToString("dd MMMM yyyy") + " s/d " + Convert.ToDateTime(rsDetail.MaturityDate).ToString("dd MMMM yyyy");
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Size = 16;
                                                worksheet.Cells[incRowExcel, 2].Style.Font.Size = 16;
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = "Rate";
                                                worksheet.Cells[incRowExcel, 2].Value = " : " + Convert.ToDecimal(rsDetail.Rate).ToString("#,##0.00%") + " p.a ";
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Size = 16;
                                                worksheet.Cells[incRowExcel, 2].Style.Font.Size = 16;
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = "Nama Rekening";
                                                worksheet.Cells[incRowExcel, 2].Value = " : " + Convert.ToString(rsDetail.NamaRekeningFund);
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Size = 16;
                                                worksheet.Cells[incRowExcel, 2].Style.Font.Size = 16;
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                                incRowExcel = incRowExcel + 2;
                                                int RowA = incRowExcel;
                                                int RowB = incRowExcel + 1;
                                                worksheet.Cells[incRowExcel, 1].Value = "Dana tersebut akan ditransfer (RTGS) dari BCA" + " pada tanggal " + Convert.ToDateTime(_FundAccountingRpt.ValueDateFrom).ToString("dd MMMM yyyy") + " ke rekening sebagai berikut:";
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Size = 16;
                                                incRowExcel++;
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = "Bank";
                                                worksheet.Cells[incRowExcel, 2].Value = " : " + rsDetail.BankPlacement;
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Size = 16;
                                                worksheet.Cells[incRowExcel, 2].Style.Font.Size = 16;
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = "No. Rekening";
                                                worksheet.Cells[incRowExcel, 2].Value = " : " + rsDetail.NomorRekeningInstrument;
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Size = 16;
                                                worksheet.Cells[incRowExcel, 2].Style.Font.Size = 16;
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = "Nama Rekening";
                                                worksheet.Cells[incRowExcel, 2].Value = " : " + rsDetail.NamaRekeningInstrument;
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Size = 16;
                                                worksheet.Cells[incRowExcel, 2].Style.Font.Size = 16;
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                                incRowExcel = incRowExcel + 2;
                                                worksheet.Cells[incRowExcel, 1].Value = "Pada saat jatuh tempo, pokok dan bunga deposito mohon ditransfer ke rekening sebagai berikut:";
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Size = 16;
                                                incRowExcel = incRowExcel + 2;
                                                worksheet.Cells[incRowExcel, 1].Value = "Nama Rekening";
                                                worksheet.Cells[incRowExcel, 2].Value = " : " + rsDetail.NamaRekeningFund;
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Size = 16;
                                                worksheet.Cells[incRowExcel, 2].Style.Font.Size = 16;
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = "No. Rekening";
                                                worksheet.Cells[incRowExcel, 2].Value = " : " + rsDetail.NomorRekeningFund;
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Size = 16;
                                                worksheet.Cells[incRowExcel, 2].Style.Font.Size = 16;
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = "Nama Bank";
                                                worksheet.Cells[incRowExcel, 2].Value = " : " + rsDetail.BankCustodian;
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Size = 16;
                                                worksheet.Cells[incRowExcel, 2].Style.Font.Size = 16;
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                                incRowExcel = incRowExcel + 2;
                                                worksheet.Cells[incRowExcel, 1].Value = "Bilyet deposito mohon dikirimkan kepada Bank Kustodian kami: ";
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Size = 16;
                                                incRowExcel = incRowExcel + 2;
                                                worksheet.Cells[incRowExcel, 1].Value = rsDetail.BankCustodian;
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Size = 16;
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = rsDetail.Address;
                                                worksheet.Cells[incRowExcel, 1].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                                                worksheet.Row(incRowExcel).Height = 30;
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Size = 16;
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                                incRowExcel = incRowExcel + 2;
                                                worksheet.Cells[incRowExcel, 1].Value = "Demikian kami sampaikan, atas perhatian dan kerjasamanya kami ucapkan terima kasih. ";
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Size = 16;
                                                incRowExcel = incRowExcel + 4;

                                                worksheet.Cells[incRowExcel, 1].Value = "Hormat kami,";
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Size = 16;
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = rsDetail.Fund;
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Size = 16;
                                                incRowExcel = incRowExcel + 10;
                                                worksheet.Cells[incRowExcel, 1].Value = _host.Get_SignatureName(_FundAccountingRpt.Signature1);
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Size = 16;
                                                worksheet.Cells[incRowExcel, 3].Value = _host.Get_SignatureName(_FundAccountingRpt.Signature2);
                                                worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 3].Style.Font.Size = 16;
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = _host.Get_PositionSignature(_FundAccountingRpt.Signature1);
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Size = 16;
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Italic = true;
                                                worksheet.Cells[incRowExcel, 3].Value = _host.Get_PositionSignature(_FundAccountingRpt.Signature2);
                                                worksheet.Cells[incRowExcel, 3].Style.Font.Size = 16;
                                                worksheet.Cells[incRowExcel, 3].Style.Font.Italic = true;





                                                incRowExcel = incRowExcel + 2;
                                                worksheet.Row(incRowExcel).PageBreak = _FundAccountingRpt.PageBreak;


                                            }

                                        }


                                        worksheet.PrinterSettings.FitToPage = true;
                                        worksheet.PrinterSettings.FitToWidth = 1;
                                        worksheet.PrinterSettings.FitToHeight = 0;
                                        worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel - 1, 4];
                                        worksheet.Column(1).Width = 25;
                                        worksheet.Column(2).Width = 50;
                                        worksheet.Column(3).Width = 50;
                                        worksheet.Column(4).Width = 30;


                                        // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                        //worksheet.Cells.AutoFitColumns(); // UNTUK AUTO FIT UKURAN TULISAN DENGAN KOLOM, KEBETULAN INI BUAT HEADER AJA DIPAKENYA
                                        worksheet.PrinterSettings.Orientation = eOrientation.Portrait; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                        //worksheet.HeaderFooter.OddHeader.RightAlignedText = "&12 Placement Deposito";

                                        // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                        worksheet.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
                                        worksheet.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
                                        worksheet.View.PageLayoutView = Tools.DefaultReportPageLayoutView();
                                        worksheet.View.ShowGridLines = Tools.DefaultReportShowGridLines();
                                        //worksheet.HeaderFooter.OddHeader.CenteredText = Tools.DefaultReportHeaderCenterText();
                                        //worksheet.HeaderFooter.OddHeader.LeftAlignedText = Tools.DefaultReportHeaderLeftText();

                                        Image img = Image.FromFile(Tools.ReportImage);
                                        worksheet.HeaderFooter.OddHeader.InsertPicture(img, PictureAlignment.Left);
                                        worksheet.HeaderFooter.OddFooter.RightAlignedText =
                                        string.Format("Page {0} of {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);

                                        //worksheet.HeaderFooter.OddFooter.CenteredText = Tools.DefaultReportFooterCenterText();




                                        package.Save();
                                        if (_FundAccountingRpt.DownloadMode == "PDF")
                                        {
                                            Tools.ExportFromExcelToPDF(filePath, pdfPath);
                                        }

                                        worksheet.HeaderFooter.OddFooter.CenteredText = Tools.DefaultReportFooterCenterText();
                                        worksheet.HeaderFooter.OddFooter.LeftAlignedText = Tools.DefaultReportFooterLeftText();

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
            #region Perpanjangan Deposito
            if (_FundAccountingRpt.ReportName.Equals("Perpanjangan Deposito"))
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
                                _paramFund = "And F.FundPK in ( " + _FundAccountingRpt.FundFrom + " ) ";
                            }
                            else
                            {
                                _paramFund = "";
                            }

                            //                            cmd.CommandText =
                            //                                @"
                            //                                select 'Jakarta' Kota, 'No. 042/STL-FA-ARO/I/2018' Reference,'PT Bank Mega Tbk' BankCustodian,'Sunter Hijau Raya' Address, 'Euis Nurjanah' Attn1, '021-79175000' PhoneAttn1
                            //                                , '021-79187100' FaxAttn1,'Ita' Attn2, '0822 0822 5466' PhoneAttn2, 'Budi Winoto' CC1, '081-808187519' PhoneCC1, '021 290-2740' PhoneCC2, '021 290-27241' FaxCC1,'Penempatan Deposito a/n  Reksa Dana Aurora Likuid di ' Remarks, 
                            //                                'No. 027/STL-FA-ARO/XII/2017' Instruksi, D.ID Currency, A.NAV Jumlah, '29 Desember 2017 s/d 29 Januari 2018' Periode, A.UnitAmount Rate, '01.074.0011.241076' NomorRekening,
                            //                                'MANDIRI' NamaBank, 'Thamrin' Cabang, 'GNESIDJA' BICCode, 'PT BANK GANESHA MEGA KUNINGAN' BankPlacement, B.Name Fund from ClientSubscription A 
                            //                                left join Fund B on A.FundPK = B.FundPK and B.Status in (1,2) 
                            //                                left join BankBranch C on B.BankBranchPK = C.BankBranchPK and C.Status in (1,2)
                            //                                left join Currency D on B.CurrencyPK = D.CurrencyPK and D.Status in (1,2)
                            //                                ";

                            cmd.CommandText =
                            @"select 'Jakarta' Kota,Reference,C.ID BankCustodian,C.Address,C.ContactPerson Attn1,C.Phone1 PhoneAttn1,C.Fax1 FaxAttn1,C.Phone2 PhoneAttn2,
                            D.ContactPerson CC1,D.Phone1 PhoneCC1, D.Phone2 PhoneCC2,D.Fax1 FaxCC1,A.InvestmentNotes Remarks,'' Instruksi,
                            E.ID Currency,A.DoneAmount Jumlah,A.ValueDate ValueDate,A.MaturityDate MaturityDate,A.InterestPercent/100 Rate,
                            D.BankAccountNo NomorRekeningInstrument,
                            D.BankAccountName NamaRekeningInstrument,
                            '' Cabang,
                            G.BICode BICCode,
                            G.Name BankPlacement,
                            F.BankAccountNo NomorRekeningFund,
                            F.BankAccountName NamaRekeningFund,
                            I.Name Fund,I.ID FundID,cast(substring(reference,1,charindex('/',reference,1) - 1) as integer) RefNo,H.ID PeriodID,dbo.ConvertIntToRoman(month(@ValueDate)) Month,* from investment A
                            left join Fund B on A.FundPK = B.FundPK and B.status in (1,2)
                            left join BankBranch C on B.BankBranchPK = C.BankBranchPK and C.Status in (1,2)
                            left join BankBranch D on A.BankBranchPK = D.BankBranchPK and D.Status in (1,2)
                            left join Currency E on B.CurrencyPK = E.CurrencyPK and E.Status in (1,2)
                            left join FundCashRef F on A.FundCashRefPK = F.FundCashRefPK and F.Status in (1,2)
                            left join Bank G on A.BankPK = G.BankPK and G.Status in (1,2)
                            left join Period H on A.PeriodPK = H.PeriodPK and H.Status in (1,2)
                            left join Fund I on A.FundPK = I.FundPK and I.Status in (1,2)
                            where valuedate = @valuedate and InstrumentTypePK = 5 and StatusSettlement = 2 and A.TrxType = 3 ";

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
                                    string filePath = Tools.ReportsPath + "PerpanjanganDeposito" + "_" + _userID + ".xlsx";
                                    string pdfPath = Tools.ReportsPath + "PerpanjanganDeposito" + "_" + _userID + ".pdf";
                                    FileInfo excelFile = new FileInfo(filePath);
                                    if (excelFile.Exists)
                                    {
                                        excelFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                        excelFile = new FileInfo(filePath);
                                    }


                                    // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                    using (ExcelPackage package = new ExcelPackage(excelFile))
                                    {
                                        package.Workbook.Properties.Title = "PerpanjanganDeposito";
                                        package.Workbook.Properties.Author = Tools.DefaultReportAuthor();
                                        package.Workbook.Properties.Comments = Tools.DefaultReportComments();
                                        package.Workbook.Properties.Company = Tools.DefaultReportCompany();
                                        package.Workbook.Properties.SetCustomPropertyValue("Checked by", _userID);
                                        package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", Tools.DefaultReportAssemblyName());

                                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Perpanjangan Deposito");



                                        //ATUR DATA GROUPINGNYA DULU
                                        List<PlacementDeposito> rList = new List<PlacementDeposito>();
                                        while (dr0.Read())
                                        {
                                            PlacementDeposito rSingle = new PlacementDeposito();
                                            rSingle.Kota = Convert.ToString(dr0["Kota"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["Kota"]));
                                            rSingle.Reference = Convert.ToString(dr0["Reference"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["Reference"]));
                                            rSingle.BankCustodian = Convert.ToString(dr0["BankCustodian"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["BankCustodian"]));
                                            rSingle.Address = Convert.ToString(dr0["Address"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["Address"]));
                                            rSingle.Attn1 = Convert.ToString(dr0["Attn1"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["Attn1"]));
                                            rSingle.PhoneAttn1 = Convert.ToString(dr0["PhoneAttn1"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["PhoneAttn1"]));
                                            rSingle.FaxAttn1 = Convert.ToString(dr0["FaxAttn1"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["FaxAttn1"]));
                                            rSingle.BankPlacement = Convert.ToString(dr0["BankPlacement"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["BankPlacement"]));
                                            rSingle.Attn2 = Convert.ToString(dr0["Attn2"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["Attn2"]));
                                            rSingle.PhoneAttn2 = Convert.ToString(dr0["PhoneAttn2"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["PhoneAttn2"]));
                                            rSingle.CC1 = Convert.ToString(dr0["CC1"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["CC1"]));
                                            rSingle.PhoneCC1 = Convert.ToString(dr0["PhoneCC1"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["PhoneCC1"]));
                                            rSingle.PhoneCC2 = Convert.ToString(dr0["PhoneCC2"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["PhoneCC2"]));
                                            rSingle.FaxCC1 = Convert.ToString(dr0["FaxCC1"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["FaxCC1"]));
                                            rSingle.Instruksi = Convert.ToString(dr0["Instruksi"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["Instruksi"]));
                                            rSingle.Currency = Convert.ToString(dr0["Currency"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["Currency"]));
                                            rSingle.Jumlah = Convert.ToDecimal(dr0["Jumlah"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["Jumlah"]));
                                            //rSingle.Periode = Convert.ToString(dr0["Periode"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["Periode"]));
                                            rSingle.Rate = Convert.ToDecimal(dr0["Rate"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["Rate"]));
                                            rSingle.Fund = Convert.ToString(dr0["Fund"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["Fund"]));
                                            rSingle.NomorRekeningInstrument = Convert.ToString(dr0["NomorRekeningInstrument"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["NomorRekeningInstrument"]));
                                            rSingle.NamaRekeningInstrument = Convert.ToString(dr0["NamaRekeningInstrument"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["NamaRekeningInstrument"]));
                                            rSingle.NomorRekeningFund = Convert.ToString(dr0["NomorRekeningFund"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["NomorRekeningFund"]));
                                            rSingle.NamaRekeningFund = Convert.ToString(dr0["NamaRekeningFund"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["NamaRekeningFund"]));
                                            rSingle.BICCode = Convert.ToString(dr0["BICCode"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["BICCode"]));
                                            rSingle.Cabang = Convert.ToString(dr0["Cabang"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["Cabang"]));
                                            rSingle.RefNo = Convert.ToString(dr0["RefNo"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["RefNo"]));
                                            rSingle.PeriodID = Convert.ToString(dr0["PeriodID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["PeriodID"]));
                                            rSingle.Month = Convert.ToString(dr0["Month"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["Month"]));
                                            rSingle.ValueDate = Convert.ToString(dr0["ValueDate"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["ValueDate"]));
                                            rSingle.MaturityDate = Convert.ToString(dr0["MaturityDate"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["MaturityDate"]));
                                            rList.Add(rSingle);

                                        }

                                        var QueryBySales =
                                         from r in rList
                                         orderby r.BankCustodian ascending
                                         group r by new { } into rGroup
                                         select rGroup;


                                        int incRowExcel = 0;

                                        incRowExcel++;

                                        foreach (var rsHeader in QueryBySales)
                                        {

                                            incRowExcel++;
                                            foreach (var rsDetail in rsHeader)
                                            {
                                                incRowExcel = incRowExcel + 5;
                                                worksheet.Cells[incRowExcel, 4].Value = rsDetail.Kota + "," + Convert.ToDateTime(_FundAccountingRpt.ValueDateFrom).ToString("dd MMMM yyyy");
                                                worksheet.Cells[incRowExcel, 4].Style.Font.Size = 15;
                                                worksheet.Cells[incRowExcel, 1].Value = "No. MM" + rsDetail.RefNo + "/" + rsDetail.FundID + "-FA/IAM /" + rsDetail.Month + "/" + rsDetail.PeriodID;
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Size = 15;
                                                incRowExcel = incRowExcel + 3;
                                                worksheet.Cells[incRowExcel, 1].Value = "Kepada Yth.";
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Size = 16;
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = rsDetail.BankCustodian;
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Size = 16;
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = rsDetail.Address;
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Size = 16;
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                                incRowExcel = incRowExcel + 2;
                                                worksheet.Cells[incRowExcel, 1].Value = "Attn.";
                                                worksheet.Cells[incRowExcel, 1].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                                                worksheet.Row(incRowExcel).Height = 30;
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Size = 16;
                                                worksheet.Cells["B" + incRowExcel + ":C" + incRowExcel].Merge = true;
                                                worksheet.Cells[incRowExcel, 2].Value = " : " + rsDetail.Attn1 + " / " + "Telp. " + rsDetail.PhoneAttn1 + " / " + "Telp. " + rsDetail.PhoneAttn2 + " ) ";
                                                worksheet.Cells[incRowExcel, 2].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                                                worksheet.Cells[incRowExcel, 2].Style.Font.Size = 16;
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = "Cc.";
                                                worksheet.Cells[incRowExcel, 1].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                                                worksheet.Row(incRowExcel).Height = 30;
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Size = 16;
                                                worksheet.Cells["B" + incRowExcel + ":D" + incRowExcel].Merge = true;
                                                worksheet.Cells[incRowExcel, 2].Value = " : " + rsDetail.BankPlacement + " - " + rsDetail.CC1 + "/" + "Telp. " + rsDetail.PhoneCC1 + ")";
                                                worksheet.Cells[incRowExcel, 2].Style.Font.Size = 16;
                                                incRowExcel++;
                                                incRowExcel = incRowExcel + 2;
                                                worksheet.Cells[incRowExcel, 1].Value = "Perihal : Perpanjangan Deposito Breakable a/n " + rsDetail.Fund;
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Size = 16;
                                                worksheet.Cells[incRowExcel, 1].Style.Font.UnderLine = true;
                                                incRowExcel = incRowExcel + 2;
                                                worksheet.Cells[incRowExcel, 1].Value = "Dengan hormat,";
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Size = 16;
                                                incRowExcel = incRowExcel + 2;
                                                worksheet.Cells[incRowExcel, 1].Value = "Sesuai dengan pembicaraan via telepon, dengan ini kami konfirmasikan bahwa kami akan memperpanjang deposito dengan";
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Size = 16;
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = "perincian sebagai berikut:";
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Size = 16;
                                                //worksheet.Cells[incRowExcel, 1].Value = "Sesuai dengan pembicaraan via telepon, maka kami bersama ini konfirmasikan bahwa deposito atas nama " + rsDetail.Fund + " yang jatuh tempo " + Convert.ToDateTime(rsDetail.ValueDate).ToString("dd MMMM yyyy");
                                                incRowExcel = incRowExcel + 2;
                                                worksheet.Cells[incRowExcel, 1].Value = "Jumlah";
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Size = 16;
                                                worksheet.Cells[incRowExcel, 2].Style.Font.Size = 16;
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 2].Value = " : " + rsDetail.Currency + " " + Convert.ToDecimal(rsDetail.Jumlah).ToString("#,##0.00");
                                                worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                                //worksheet.Cells[incRowExcel, 2].Style.Numberformat.Format = "#,##0.00";

                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = "Periode";
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 2].Value = " : " + Convert.ToDateTime(rsDetail.ValueDate).ToString("dd MMMM yyyy") + " s/d " + Convert.ToDateTime(rsDetail.MaturityDate).ToString("dd MMMM yyyy");
                                                worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Size = 16;
                                                worksheet.Cells[incRowExcel, 2].Style.Font.Size = 16;
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = "Rate";
                                                worksheet.Cells[incRowExcel, 2].Value = " : " + Convert.ToDecimal(rsDetail.Rate).ToString("#,##0.00%") + " p.a ";
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Size = 16;
                                                worksheet.Cells[incRowExcel, 2].Style.Font.Size = 16;
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = "Nama Rekening";
                                                worksheet.Cells[incRowExcel, 2].Value = " : " + Convert.ToString(rsDetail.NamaRekeningFund);
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Size = 16;
                                                worksheet.Cells[incRowExcel, 2].Style.Font.Size = 16;
                                                incRowExcel = incRowExcel + 2;
                                                worksheet.Cells[incRowExcel, 1].Value = "Pada saat jatuh tempo, bunga deposito mohon ditransfer ke rekening sebagai berikut: ";
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Size = 16;
                                                incRowExcel = incRowExcel + 2;
                                                worksheet.Cells[incRowExcel, 1].Value = "Nama Rekening";
                                                worksheet.Cells[incRowExcel, 2].Value = " : " + rsDetail.NamaRekeningFund;
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Size = 16;
                                                worksheet.Cells[incRowExcel, 2].Style.Font.Size = 16;
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = "No. Rekening";
                                                worksheet.Cells[incRowExcel, 2].Value = " : " + rsDetail.NomorRekeningFund;
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Size = 16;
                                                worksheet.Cells[incRowExcel, 2].Style.Font.Size = 16;
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = "Nama Bank";
                                                worksheet.Cells[incRowExcel, 2].Value = " : " + rsDetail.BankCustodian;
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Size = 16;
                                                worksheet.Cells[incRowExcel, 2].Style.Font.Size = 16;
                                                incRowExcel++;
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = "Bilyet deposito mohon dikirimkan kepada Bank Kustodian kami: ";
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Size = 16;
                                                incRowExcel = incRowExcel + 2;
                                                worksheet.Cells[incRowExcel, 1].Value = rsDetail.BankCustodian;
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Size = 16;
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = rsDetail.Address;
                                                worksheet.Cells[incRowExcel, 1].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                                                worksheet.Row(incRowExcel).Height = 30;
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Size = 16;
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                                incRowExcel = incRowExcel + 4;
                                                worksheet.Cells[incRowExcel, 1].Value = "Demikian kami sampaikan, atas perhatian dan kerjasamanya kami ucapkan terima kasih. ";
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Size = 16;
                                                incRowExcel = incRowExcel + 2;

                                                worksheet.Cells[incRowExcel, 1].Value = "Hormat kami,";
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Size = 16;
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = rsDetail.Fund;
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Size = 16;
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                                incRowExcel = incRowExcel + 10;
                                                worksheet.Cells[incRowExcel, 1].Value = _host.Get_SignatureName(_FundAccountingRpt.Signature1);
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Size = 16;
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 3].Value = _host.Get_SignatureName(_FundAccountingRpt.Signature2);
                                                worksheet.Cells[incRowExcel, 3].Style.Font.Size = 16;
                                                worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = _host.Get_PositionSignature(_FundAccountingRpt.Signature1);
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Italic = true;
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Size = 16;
                                                worksheet.Cells[incRowExcel, 3].Value = _host.Get_PositionSignature(_FundAccountingRpt.Signature2);
                                                worksheet.Cells[incRowExcel, 3].Style.Font.Italic = true;
                                                worksheet.Cells[incRowExcel, 3].Style.Font.Size = 16;



                                                incRowExcel = incRowExcel + 2;
                                                worksheet.Row(incRowExcel).PageBreak = _FundAccountingRpt.PageBreak;


                                            }
                                        }


                                        worksheet.PrinterSettings.FitToPage = true;
                                        worksheet.PrinterSettings.FitToWidth = 1;
                                        worksheet.PrinterSettings.FitToHeight = 0;
                                        worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel - 1, 4];
                                        worksheet.Column(1).Width = 25;
                                        worksheet.Column(2).Width = 50;
                                        worksheet.Column(3).Width = 50;
                                        worksheet.Column(4).Width = 30;


                                        // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                        //worksheet.Cells.AutoFitColumns(); // UNTUK AUTO FIT UKURAN TULISAN DENGAN KOLOM, KEBETULAN INI BUAT HEADER AJA DIPAKENYA
                                        worksheet.PrinterSettings.Orientation = eOrientation.Portrait; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                        //worksheet.HeaderFooter.OddHeader.RightAlignedText = "&12 Placement Deposito";

                                        // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                        worksheet.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
                                        worksheet.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
                                        worksheet.View.PageLayoutView = Tools.DefaultReportPageLayoutView();
                                        worksheet.View.ShowGridLines = Tools.DefaultReportShowGridLines();
                                        //worksheet.HeaderFooter.OddHeader.CenteredText = Tools.DefaultReportHeaderCenterText();
                                        //worksheet.HeaderFooter.OddHeader.LeftAlignedText = Tools.DefaultReportHeaderLeftText();

                                        Image img = Image.FromFile(Tools.ReportImage);
                                        worksheet.HeaderFooter.OddHeader.InsertPicture(img, PictureAlignment.Left);
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

            #region Portfolio Valuation Report
            else if (_FundAccountingRpt.ReportName.Equals("Portfolio Valuation Report"))
            {
                try
                {

                    string filePath = Tools.ReportsPath + "PortfolioValuationReport" + "_" + _userID + ".xlsx";
                    string pdfPath = Tools.ReportsPath + "PortfolioValuationReport" + "_" + _userID + ".pdf";
                    FileInfo excelFile = new FileInfo(filePath);
                    if (excelFile.Exists)
                    {
                        excelFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                        excelFile = new FileInfo(filePath);
                    }
                    ExcelPackage package = new ExcelPackage(excelFile);
                    package.Workbook.Properties.Title = "UnitRegistryReport";
                    package.Workbook.Properties.Author = Tools.DefaultReportAuthor();
                    package.Workbook.Properties.Comments = Tools.DefaultReportComments();
                    package.Workbook.Properties.Company = Tools.DefaultReportCompany();
                    package.Workbook.Properties.SetCustomPropertyValue("Checked by", _userID);
                    package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", Tools.DefaultReportAssemblyName());

                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Portfolio Valuation Report");

                    int _startAsset = 0;
                    int _RowNominal = 0;
                    int _rowMarketValue1 = 0;
                    int _rowMarketValue2 = 0;
                    int _this = 0;
                    bool _row1 = false;
                    bool _row2 = false;
                    bool _row3 = false;
                    bool _row4 = false;
                    //ATUR DATA GROUPINGNYA DULU
                    int incRowExcel = 2;
                    using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                    {
                        DbCon.Open();
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {

                            cmd.CommandText =
                                @"
                                
                                    Select '2' Row ,  [dbo].[FGetGroupAccountFundJournalBalanceByFundPK] (@Date,2,@FundPK) Balance -- CASH AT BANK
                                    UNION ALL
                                    Select '3' Row ,  [dbo].[FGetGroupAccountFundJournalBalanceByFundPK] (@Date,37,@FundPK) Balance -- INTEREST RECEIVABLE (ACCRUAL) - CURRENT ACCOUNT
                                    UNION ALL
                                    Select '4' Row ,  [dbo].[FGetGroupAccountFundJournalBalanceByFundPK] (@Date,38,@FundPK) Balance -- 	INTEREST RECEIVABLE (ACCRUAL) - TIME DEPOSIT
                                    UNION ALL
                                    Select '5' Row ,  [dbo].[FGetGroupAccountFundJournalBalanceByFundPK] (@Date,34,@FundPK) Balance -- INTEREST RECEIVABLE (ACCRUAL) - BOND
                                    UNION ALL
                                    Select '6' Row ,  [dbo].[FGetGroupAccountFundJournalBalanceByFundPK] (@Date,26,@FundPK) Balance -- INTEREST RECEIVABLE (BUY/SELL) - BOND
                                    UNION ALL
                                    Select '7' Row ,  [dbo].[FGetGroupAccountFundJournalBalanceByFundPK] (@Date,33,@FundPK) Balance -- 	INTEREST RECEIVABLE (BUY/SELL) - TIME DEPOSIT
                                    UNION ALL
                                    Select '8' Row ,  [dbo].[FGetGroupAccountFundJournalBalanceByFundPK] (@Date,52,@FundPK) Balance -- RECEIVABLE - DIVIDEND FROM EQUITY
                                    UNION ALL
                                    Select '9' Row ,  [dbo].[FGetGroupAccountFundJournalBalanceByFundPK] (@Date,53,@FundPK) Balance -- RECEIVABLE - CONSENT FEE FROM BONDS
                                    UNION ALL
                                    Select '10' Row ,  [dbo].[FGetGroupAccountFundJournalBalanceByFundPK] (@Date,60,@FundPK) Balance -- RECEIVABLE - OTHER	
                                    UNION ALL
                                    Select '11' Row ,  [dbo].[FGetGroupAccountFundJournalBalanceByFundPK] (@Date,59,@FundPK) Balance -- RECEIVABLE - REFUND
                                    UNION ALL
                                    Select '12' Row ,  [dbo].[FGetGroupAccountFundJournalBalanceByFundPK] (@Date,61,@FundPK) Balance -- RECEIVABLE - CLAIMS
                                    UNION ALL
                                    Select '13' Row ,  [dbo].[FGetGroupAccountFundJournalBalanceByFundPK] (@Date,195,@FundPK) Balance -- OTHER ASSETS	
                                    UNION ALL
                                    Select '14' Row ,  [dbo].[FGetGroupAccountFundJournalBalanceByFundPK] (@Date,42,@FundPK) Balance -- RECEIVABLE SALE - EQUITY
                                    UNION ALL
                                    Select '15' Row ,  [dbo].[FGetGroupAccountFundJournalBalanceByFundPK] (@Date,45,@FundPK) Balance -- RECEIVABLE SALE - NEGOTIABLE CERTIFICATE OF DEPOSIT
                                    UNION ALL
                                    Select '16' Row ,  [dbo].[FGetGroupAccountFundJournalBalanceByFundPK] (@Date,49,@FundPK) Balance -- RECEIVABLE SALE - TIME DEPOSIT
                                    UNION ALL
                                    Select '17' Row ,  [dbo].[FGetGroupAccountFundJournalBalanceByFundPK] (@Date,50,@FundPK) Balance -- RECEIVABLE SALE - BOND
                                    UNION ALL
                                    Select '18' Row ,  [dbo].[FGetGroupAccountFundJournalBalanceByFundPK] (@Date,56,@FundPK) Balance -- PREPAID TAX - PPH 23 (DIVIDEND)
                                    UNION ALL
                                    Select '19' Row ,  [dbo].[FGetGroupAccountFundJournalBalanceByFundPK] (@Date,57,@FundPK) Balance   -- PREPAID TAX - PPH 25
                                    UNION ALL
                                    Select '20' Row ,  [dbo].[FGetGroupAccountFundJournalBalanceByFundPK] (@Date,197,@FundPK) Balance -- PREPAID TAX - OTHER
                                    UNION ALL
                                    Select '23' Row ,  [dbo].[FGetGroupAccountFundJournalBalanceByFundPK] (@Date,79,@FundPK) * -1 Balance -- PAYABLE - MANAGEMENT FEE
                                    UNION ALL
                                    Select '24' Row ,  [dbo].[FGetGroupAccountFundJournalBalanceByFundPK] (@Date,80,@FundPK) * -1 Balance -- PAYABLE - CUSTODIAN FEE
                                    UNION ALL
                                    Select '25' Row ,  [dbo].[FGetGroupAccountFundJournalBalanceByFundPK] (@Date,202,@FundPK) * -1  Balance -- PAYABLE - SINVEST FEE
                                    UNION ALL
                                    Select '26' Row ,  [dbo].[FGetGroupAccountFundJournalBalanceByFundPK] (@Date,78,@FundPK) * -1 Balance -- PAYABLE - MOVEMENT FEE (CUST)
                                    UNION ALL
                                    Select '27' Row ,  [dbo].[FGetGroupAccountFundJournalBalanceByFundPK] (@Date,77,@FundPK) * -1 Balance -- PAYABLE - BROKER FEE (C-BEST)
                                    UNION ALL
                                    Select '28' Row ,  [dbo].[FGetGroupAccountFundJournalBalanceByFundPK] (@Date,81,@FundPK) * -1 Balance -- PAYABLE - AUDIT FEE
                                    UNION ALL
                                    Select '29' Row ,  [dbo].[FGetGroupAccountFundJournalBalanceByFundPK] (@Date,82,@FundPK) * -1 Balance -- PAYABLE - SELLING FEE
                                    UNION ALL
                                    Select '30' Row ,  [dbo].[FGetGroupAccountFundJournalBalanceByFundPK] (@Date,104,@FundPK) * -1 Balance -- PAYABLE - REPORTING FEE
                                    UNION ALL
                                    Select '31' Row ,  [dbo].[FGetGroupAccountFundJournalBalanceByFundPK] (@Date,100,@FundPK) * -1 Balance -- PAYABLE - BANK TRANSFER CHARGE
                                    UNION ALL
                                    Select '32' Row ,  [dbo].[FGetGroupAccountFundJournalBalanceByFundPK] (@Date,101,@FundPK) * -1 Balance -- PAYABLE - PROSPEKTUS FEE
                                    UNION ALL
                                    Select '33' Row ,  [dbo].[FGetGroupAccountFundJournalBalanceByFundPK] (@Date,102,@FundPK) * -1 Balance -- PAYABLE - CLAIMS
                                    UNION ALL
                                    Select '34' Row ,  [dbo].[FGetGroupAccountFundJournalBalanceByFundPK] (@Date,105,@FundPK) * -1 Balance -- PAYABLE - OTHER FEE
                                    UNION ALL
                                    Select '35' Row ,  [dbo].[FGetGroupAccountFundJournalBalanceByFundPK] (@Date,196,@FundPK) * -1 Balance -- OTHER LIABILITIES
                                    UNION ALL
                                    Select '36' Row ,  [dbo].[FGetGroupAccountFundJournalBalanceByFundPK]  (@Date,94,@FundPK)* -1 Balance -- WITHHOLDING TAX PAYABLE - ACCRUED INTEREST
                                    UNION ALL
                                    Select '37' Row ,  [dbo].[FGetGroupAccountFundJournalBalanceByFundPK]  (@Date,93,@FundPK)* -1 Balance -- WITHHOLDING TAX PAYABLE - CAPITAL GAIN
                                    UNION ALL
                                    Select '38' Row ,  [dbo].[FGetGroupAccountFundJournalBalanceByFundPK]  (@Date,95,@FundPK)* -1 Balance -- WITHHOLDING TAX PAYABLE - PPH23 COMM. FROM BROKER	
                                    UNION ALL
                                    Select '39' Row ,  [dbo].[FGetGroupAccountFundJournalBalanceByFundPK]  (@Date,96,@FundPK)* -1 Balance -- PAYABLE - INCOME TAX
                                    UNION ALL
                                    Select '40' Row ,  [dbo].[FGetGroupAccountFundJournalBalanceByFundPK]  (@Date,97,@FundPK)* -1 Balance -- PAYABLE - ESTIMATION TAX
                                    UNION ALL
                                    Select '41' Row ,  [dbo].[FGetGroupAccountFundJournalBalanceByFundPK]  (@Date,98,@FundPK)* -1 Balance -- WITHHOLDING TAX PAYABLE - AUDIT FEE
                                    UNION ALL
                                    Select '42' Row ,  [dbo].[FGetGroupAccountFundJournalBalanceByFundPK]  (@Date,194,@FundPK)* -1 Balance -- WITHHOLDING TAX PAYABLE
                                    UNION ALL
                                    Select '43' Row ,  [dbo].[FGetGroupAccountFundJournalBalanceByFundPK] (@Date,66,@FundPK) * -1 Balance -- PAYABLE PURCHASE - EQUITY
                                    UNION ALL
                                    Select '44' Row ,  [dbo].[FGetGroupAccountFundJournalBalanceByFundPK] (@Date,67,@FundPK) * -1 Balance -- PAYABLE PURCHASE - BOND
                                    UNION ALL
                                    Select '45' Row ,  [dbo].[FGetGroupAccountFundJournalBalanceByFundPK] (@Date,71,@FundPK) * -1 Balance -- PAYABLE PURCHASE - NEGOTIABLE CERTIFICATE OF DEPOSIT
                                    UNION ALL
                                    Select '46' Row ,  [dbo].[FGetGroupAccountFundJournalBalanceByFundPK] (@Date,75,@FundPK) * -1 Balance -- PAYABLE PURCHASE - TIME DEPOSIT
                                    UNION ALL
                                    Select '47' Row ,  [dbo].[FGetGroupAccountFundJournalBalanceByFundPK] (@Date,89,@FundPK) * -1 Balance -- PENDING SUBSCRIPTION
                                    UNION ALL
                                    Select '48' Row ,  [dbo].[FGetGroupAccountFundJournalBalanceByFundPK] (@Date,85,@FundPK) * -1 Balance -- PAYABLE - SUBSCRIPTION FEE
                                    UNION ALL
                                    Select '49' Row ,  [dbo].[FGetGroupAccountFundJournalBalanceByFundPK] (@Date,84,@FundPK) * -1 Balance --PAYABLE - REDEMPTION
                                    UNION ALL
                                    Select '50' Row ,  [dbo].[FGetGroupAccountFundJournalBalanceByFundPK] (@Date,86,@FundPK) * -1 Balance -- PAYABLE - REDEMPTION FEE
                                    UNION ALL
                                    Select '51' Row ,  [dbo].[FGetGroupAccountFundJournalBalanceByFundPK] (@Date,87,@FundPK) * -1 Balance -- PAYABLE - DISTRIBUTED INCOME
                                    UNION ALL
                                    Select '52' Row ,  [dbo].[FGetGroupAccountFundJournalBalanceByFundPK] (@Date,88,@FundPK) * -1 Balance -- PAYABLE - FROM SWITCH OUT
                                    UNION ALL
                                    Select '53' Row ,  [dbo].[FGetGroupAccountFundJournalBalanceByFundPK] (@Date,90,@FundPK) * -1 Balance -- DISTRIBUTION INCOME PAYABLE
                                    UNION ALL
                                    Select '54' Row ,  [dbo].[FGetGroupAccountFundJournalBalanceByFundPK] (@Date,103,@FundPK) * -1 Balance --PayableSInvestFee
                                 ";
                            cmd.CommandTimeout = 0;
                            cmd.Parameters.AddWithValue("@Date", _FundAccountingRpt.ValueDateFrom);
                            cmd.Parameters.AddWithValue("@FundPK", _FundAccountingRpt.FundFrom);

                            using (SqlDataReader dr0 = cmd.ExecuteReader())
                            {

                                if (dr0.HasRows)
                                {
                                    _row1 = dr0.HasRows;
                                    List<PVRRpt> rList = new List<PVRRpt>();
                                    while (dr0.Read())
                                    {
                                        PVRRpt rSingle = new PVRRpt();
                                        rSingle.Balance = dr0["Balance"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["Balance"]);
                                        rSingle.Row = dr0["Row"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr0["Row"]);

                                        rList.Add(rSingle);

                                    }
                                    var QueryByFundID =
                                            from r in rList
                                            group r by new { } into rGroup
                                            select rGroup;

                                    foreach (var rsHeader in QueryByFundID)
                                    {

                                        worksheet.Cells[incRowExcel, 1].Value = "ASSET";
                                        worksheet.Cells["A" + incRowExcel + ":C" + incRowExcel].Merge = true;
                                        worksheet.Cells["A" + incRowExcel + ":C" + incRowExcel].Style.Font.Size = 13;
                                        worksheet.Cells["A" + incRowExcel + ":C" + incRowExcel].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                        worksheet.Cells["A" + incRowExcel + ":C" + incRowExcel].Style.Fill.BackgroundColor.SetColor(Color.Gray);
                                        incRowExcel++;

                                        _startAsset = incRowExcel;
                                        worksheet.Cells[incRowExcel, 1].Value = "1";
                                        worksheet.Cells[incRowExcel, 2].Value = "CASH AT BANK";
                                        worksheet.Cells[incRowExcel, 6].Value = "TOTAL NET ASSET VALUE";
                                        worksheet.Cells[incRowExcel, 7].Value = _host.Get_FundAUM(_FundAccountingRpt.FundFrom, _FundAccountingRpt.ValueDateFrom);
                                        worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0.00";
                                        worksheet.Cells[incRowExcel, 8].Value = "(Last Day NAV)";
                                        worksheet.Cells[incRowExcel, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        worksheet.Cells[incRowExcel, 8].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                        worksheet.Cells[incRowExcel, 8].Style.Fill.BackgroundColor.SetColor(Color.LightSalmon);

                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "2";
                                        worksheet.Cells[incRowExcel, 2].Value = "INTEREST RECEIVABLE (ACCRUAL) - CURRENT ACCOUNT";
                                        worksheet.Cells[incRowExcel, 6].Value = "TOTAL OUTSTANDING UNIT";
                                        worksheet.Cells[incRowExcel, 7].Value = _host.Get_FundUnitPosition(_FundAccountingRpt.FundFrom, _FundAccountingRpt.ValueDateFrom);
                                        worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0.0000";
                                        worksheet.Cells[incRowExcel, 8].Value = _host.Get_LastNavYesterday(_FundAccountingRpt.FundFrom, _FundAccountingRpt.ValueDateFrom);
                                        worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0.0000";
                                        worksheet.Cells[incRowExcel, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        worksheet.Cells[incRowExcel, 8].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                        worksheet.Cells[incRowExcel, 8].Style.Fill.BackgroundColor.SetColor(Color.LightSalmon);

                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "3";
                                        worksheet.Cells[incRowExcel, 2].Value = "INTEREST RECEIVABLE (ACCRUAL) - TIME DEPOSIT";
                                        worksheet.Cells[incRowExcel, 6].Value = "NET ASSET VALUE PER UNIT";
                                        worksheet.Cells[incRowExcel, 7].Value = _host.Get_FundAUM(_FundAccountingRpt.FundFrom, _FundAccountingRpt.ValueDateFrom) / _host.Get_FundUnitPosition(_FundAccountingRpt.FundFrom, _FundAccountingRpt.ValueDateFrom);
                                        worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0.0000";
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "4";
                                        worksheet.Cells[incRowExcel, 2].Value = "INTEREST RECEIVABLE (ACCRUAL) - BOND";
                                        worksheet.Cells[incRowExcel, 6].Value = "CHANGE / DAY";
                                        worksheet.Cells[incRowExcel, 7].Value = _host.Get_ChangeNavPerDay(_FundAccountingRpt.FundFrom, _FundAccountingRpt.ValueDateFrom);
                                        worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0.0000";
                                        worksheet.Cells[incRowExcel, 8].Value = "(Last Year NAV)";
                                        worksheet.Cells[incRowExcel, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        worksheet.Cells[incRowExcel, 8].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                        worksheet.Cells[incRowExcel, 8].Style.Fill.BackgroundColor.SetColor(Color.LightSalmon);
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "5";
                                        worksheet.Cells[incRowExcel, 2].Value = "INTEREST RECEIVABLE (BUY/SELL) - BOND";
                                        worksheet.Cells[incRowExcel, 6].Value = "YIELD IN THE LAST 30 DAYS";
                                        worksheet.Cells[incRowExcel, 8].Value = _host.Get_NavLastYear(_FundAccountingRpt.FundFrom, _FundAccountingRpt.ValueDateFrom);
                                        worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0.0000";
                                        worksheet.Cells[incRowExcel, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        worksheet.Cells[incRowExcel, 8].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                        worksheet.Cells[incRowExcel, 8].Style.Fill.BackgroundColor.SetColor(Color.LightSalmon);
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "6";
                                        worksheet.Cells[incRowExcel, 2].Value = "INTEREST RECEIVABLE (BUY/SELL) - TIME DEPOSIT";
                                        worksheet.Cells[incRowExcel, 6].Value = "YIELD IN THE LAST 1 YEARS";
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "7";
                                        worksheet.Cells[incRowExcel, 2].Value = "RECEIVABLE - DIVIDEND FROM EQUITY";
                                        worksheet.Cells[incRowExcel, 6].Value = "YIELD YEAR to DATE";
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "8";
                                        worksheet.Cells[incRowExcel, 2].Value = "RECEIVABLE - CONSENT FEE FROM BONDS";
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "9";
                                        worksheet.Cells[incRowExcel, 2].Value = "RECEIVABLE - OTHER";
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "10";
                                        worksheet.Cells[incRowExcel, 2].Value = "RECEIVABLE - REFUND";
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "11";
                                        worksheet.Cells[incRowExcel, 2].Value = "RECEIVABLE - CLAIMS";
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "12";
                                        worksheet.Cells[incRowExcel, 2].Value = "OTHER ASSETS";
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "13";
                                        worksheet.Cells[incRowExcel, 2].Value = "RECEIVABLE SALE - EQUITY";
                                        worksheet.Cells[incRowExcel, 3].Value = "0";
                                        worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "14";
                                        worksheet.Cells[incRowExcel, 2].Value = "RECEIVABLE SALE - NEGOTIABLE CERTIFICATE OF DEPOSIT";
                                        worksheet.Cells[incRowExcel, 3].Value = "0";
                                        worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "15";
                                        worksheet.Cells[incRowExcel, 2].Value = "RECEIVABLE SALE - TIME DEPOSIT";
                                        worksheet.Cells[incRowExcel, 3].Value = "0";
                                        worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "16";
                                        worksheet.Cells[incRowExcel, 2].Value = "RECEIVABLE SALE - BOND";
                                        worksheet.Cells[incRowExcel, 3].Value = "0";
                                        worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "17";
                                        worksheet.Cells[incRowExcel, 2].Value = "PREPAID TAX - PPH 23 (DIVIDEND)";
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "18";
                                        worksheet.Cells[incRowExcel, 2].Value = "PREPAID TAX - PPH 25";
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "19";
                                        worksheet.Cells[incRowExcel, 2].Value = "PREPAID TAX - OTHER";
                                        int _endAsset = incRowExcel;
                                        incRowExcel++;

                                        int _end = incRowExcel - 13;
                                        worksheet.Cells["A" + _startAsset + ":C" + _endAsset].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                        worksheet.Cells["A" + _startAsset + ":C" + _endAsset].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
                                        worksheet.Cells["A" + _startAsset + ":C" + _endAsset].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["A" + _startAsset + ":C" + _endAsset].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["A" + _startAsset + ":C" + _endAsset].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["A" + _startAsset + ":C" + _endAsset].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                        worksheet.Cells["F" + _startAsset + ":G" + _end].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                        worksheet.Cells["F" + _startAsset + ":G" + _end].Style.Fill.BackgroundColor.SetColor(Color.MediumPurple);
                                        worksheet.Cells["F" + _startAsset + ":G" + _end].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["F" + _startAsset + ":G" + _end].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["F" + _startAsset + ":G" + _end].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["F" + _startAsset + ":G" + _end].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "LIABILITIES";
                                        worksheet.Cells["A" + incRowExcel + ":C" + incRowExcel].Merge = true;
                                        worksheet.Cells["A" + incRowExcel + ":C" + incRowExcel].Style.Font.Size = 13;
                                        worksheet.Cells["A" + incRowExcel + ":C" + incRowExcel].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                        worksheet.Cells["A" + incRowExcel + ":C" + incRowExcel].Style.Fill.BackgroundColor.SetColor(Color.Gray);
                                        incRowExcel++;
                                        int _startLiabilities = incRowExcel;
                                        worksheet.Cells[incRowExcel, 1].Value = "20";
                                        worksheet.Cells[incRowExcel, 2].Value = "PAYABLE - MANAGEMENT FEE";

                                        int _startCrossCheck = incRowExcel;
                                        worksheet.Cells[incRowExcel, 6].Value = "Crosscheck";

                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "21";
                                        worksheet.Cells[incRowExcel, 2].Value = "PAYABLE - CUSTODIAN FEE";

                                        worksheet.Cells[incRowExcel, 6].Value = "NAV";
                                        worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0.00";

                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "22";
                                        worksheet.Cells[incRowExcel, 2].Value = "PAYABLE - SINVEST FEE";

                                        worksheet.Cells[incRowExcel, 6].Value = "Selisih";
                                        worksheet.Cells[incRowExcel, 7].Formula = "SUM(G2" + "-G18" + ")";
                                        worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0.00";

                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "23";
                                        worksheet.Cells[incRowExcel, 2].Value = "A/P MOVEMENT FEE (CUST)";

                                        worksheet.Cells[incRowExcel, 6].Value = "Unrealized";

                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "24";
                                        worksheet.Cells[incRowExcel, 2].Value = "PAYABLE - BROKER FEE (C-BEST)";

                                        int _endCrossCheck = incRowExcel;
                                        worksheet.Cells[incRowExcel, 6].Value = "Selisih";

                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "25";
                                        worksheet.Cells[incRowExcel, 2].Value = "PAYABLE - AUDIT FEE";

                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "26";
                                        worksheet.Cells[incRowExcel, 2].Value = "PAYABLE - SELLING FEE";

                                        int _startcheckBy = incRowExcel;
                                        worksheet.Cells[incRowExcel, 6].Value = "Prepared by";

                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "27";
                                        worksheet.Cells[incRowExcel, 2].Value = "PAYABLE - REPORTING FEE";

                                        worksheet.Cells[incRowExcel, 6].Value = "Checked by";

                                        worksheet.Cells[incRowExcel, 1].Value = "38";
                                        worksheet.Cells[incRowExcel, 2].Value = "PAYABLE - BANK TRANSFER CHARGE";
                                        incRowExcel++;

                                        int _endcheckBy = incRowExcel;
                                        worksheet.Cells[incRowExcel, 6].Value = "Investment Manager";

                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "29";
                                        worksheet.Cells[incRowExcel, 2].Value = "PAYABLE - PROSPEKTUS FEE";

                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "30";
                                        worksheet.Cells[incRowExcel, 2].Value = "PAYABLE - CLAIMS";
                                        incRowExcel++;

                                        worksheet.Cells[incRowExcel, 1].Value = "31";
                                        worksheet.Cells[incRowExcel, 2].Value = "PAYABLE - OTHER FEE";
                                        incRowExcel++;

                                        worksheet.Cells[incRowExcel, 1].Value = "32";
                                        worksheet.Cells[incRowExcel, 2].Value = "OTHER LIABILITIES";
                                        incRowExcel++;

                                        worksheet.Cells[incRowExcel, 1].Value = "33";
                                        worksheet.Cells[incRowExcel, 2].Value = "WITHHOLDING TAX PAYABLE - ACCRUED INTEREST";
                                        incRowExcel++;

                                        worksheet.Cells[incRowExcel, 1].Value = "34";
                                        worksheet.Cells[incRowExcel, 2].Value = "WITHHOLDING TAX PAYABLE - CAPITAL GAIN";
                                        incRowExcel++;

                                        worksheet.Cells[incRowExcel, 1].Value = "35";
                                        worksheet.Cells[incRowExcel, 2].Value = "WITHHOLDING TAX PAYABLE - PPH23 COMM. FROM BROKER";
                                        incRowExcel++;

                                        worksheet.Cells[incRowExcel, 1].Value = "36";
                                        worksheet.Cells[incRowExcel, 2].Value = "PAYABLE - INCOME TAX";
                                        incRowExcel++;

                                        worksheet.Cells[incRowExcel, 1].Value = "37";
                                        worksheet.Cells[incRowExcel, 2].Value = "PAYABLE - ESTIMATION TAX";
                                        incRowExcel++;

                                        worksheet.Cells[incRowExcel, 1].Value = "38";
                                        worksheet.Cells[incRowExcel, 2].Value = "WITHHOLDING TAX PAYABLE - AUDIT FEE";
                                        incRowExcel++;

                                        worksheet.Cells[incRowExcel, 1].Value = "39";
                                        worksheet.Cells[incRowExcel, 2].Value = "WITHHOLDING TAX PAYABLE";
                                        incRowExcel++;

                                        worksheet.Cells[incRowExcel, 1].Value = "40";
                                        worksheet.Cells[incRowExcel, 2].Value = "PAYABLE PURCHASE - EQUITY";
                                        incRowExcel++;

                                        worksheet.Cells[incRowExcel, 1].Value = "41";
                                        worksheet.Cells[incRowExcel, 2].Value = "PAYABLE PURCHASE - BOND";
                                        incRowExcel++;

                                        worksheet.Cells[incRowExcel, 1].Value = "42";
                                        worksheet.Cells[incRowExcel, 2].Value = "PAYABLE PURCHASE - NEGOTIABLE CERTIFICATE OF DEPOSIT";
                                        incRowExcel++;

                                        worksheet.Cells[incRowExcel, 1].Value = "43";
                                        worksheet.Cells[incRowExcel, 2].Value = "PAYABLE PURCHASE - TIME DEPOSIT";
                                        incRowExcel++;

                                        worksheet.Cells[incRowExcel, 1].Value = "44";
                                        worksheet.Cells[incRowExcel, 2].Value = "PENDING SUBSCRIPTION";
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "45";
                                        worksheet.Cells[incRowExcel, 2].Value = "PAYABLE - SUBSCRIPTION FEE";
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "46";
                                        worksheet.Cells[incRowExcel, 2].Value = "PAYABLE - REDEMPTION";
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "47";
                                        worksheet.Cells[incRowExcel, 2].Value = "PAYABLE - REDEMPTION FEE";
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "48";
                                        worksheet.Cells[incRowExcel, 2].Value = "PAYABLE - DISTRIBUTED INCOME";
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "49";
                                        worksheet.Cells[incRowExcel, 2].Value = "PAYABLE - FROM SWITCH OUT";
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "50";
                                        worksheet.Cells[incRowExcel, 2].Value = "DISTRIBUTION INCOME PAYABLE";
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "51";
                                        worksheet.Cells[incRowExcel, 2].Value = "PAYABLE - OTHER";
                                        int _endLiabilities = incRowExcel;

                                        worksheet.Cells["A" + _startLiabilities + ":C" + _endLiabilities].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                        worksheet.Cells["A" + _startLiabilities + ":C" + _endLiabilities].Style.Fill.BackgroundColor.SetColor(Color.Yellow);
                                        worksheet.Cells["C" + 23 + ":C" + 54].Style.Font.Color.SetColor(Color.Red);
                                        worksheet.Cells["A" + _startLiabilities + ":C" + _endLiabilities].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["A" + _startLiabilities + ":C" + _endLiabilities].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["A" + _startLiabilities + ":C" + _endLiabilities].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["A" + _startLiabilities + ":C" + _endLiabilities].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;


                                        worksheet.Cells["F" + _startCrossCheck + ":G" + _endCrossCheck].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                        worksheet.Cells["F" + _startCrossCheck + ":G" + _endCrossCheck].Style.Fill.BackgroundColor.SetColor(Color.Gray);
                                        worksheet.Cells["F" + _startCrossCheck + ":G" + _endCrossCheck].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["F" + _startCrossCheck + ":G" + _endCrossCheck].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["F" + _startCrossCheck + ":G" + _endCrossCheck].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["F" + _startCrossCheck + ":G" + _endCrossCheck].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                        worksheet.Cells["F" + _startcheckBy + ":G" + _endcheckBy].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                        worksheet.Cells["F" + _startcheckBy + ":G" + _endcheckBy].Style.Fill.BackgroundColor.SetColor(Color.Gray);
                                        worksheet.Cells["F" + _startcheckBy + ":G" + _endcheckBy].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["F" + _startcheckBy + ":G" + _endcheckBy].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["F" + _startcheckBy + ":G" + _endcheckBy].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["F" + _startcheckBy + ":G" + _endcheckBy].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;



                                        int _startRowDetail = incRowExcel;
                                        int _endRowDetail = 0;
                                        foreach (var rsDetail in rsHeader)
                                        {
                                            worksheet.Cells[rsDetail.Row, 3].Value = rsDetail.Balance;
                                            worksheet.Cells[rsDetail.Row, 3].Style.Numberformat.Format = "#,##0.00";
                                            worksheet.Cells[rsDetail.Row, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                            _endRowDetail = incRowExcel;
                                            incRowExcel++;

                                        }

                                        incRowExcel++;
                                        worksheet.Cells[55, 2, 55, 3].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                        worksheet.Cells[55, 2, 55, 3].Style.Fill.BackgroundColor.SetColor(Color.Gray);
                                        worksheet.Cells[55, 2, 55, 3].Style.Font.Bold = true;
                                        worksheet.Cells[55, 2].Value = "Total Cash & Equivalent";

                                        worksheet.Cells[55, 3].Style.Numberformat.Format = "#,##0.00";
                                        worksheet.Cells[55, 3].Formula = "SUM(C2" + ":C54" + ")";
                                        worksheet.Cells[55, 3].Calculate();
                                        incRowExcel++;

                                    }
                                    //incRowExcel++;
                                    //worksheet.Row(incRowExcel).PageBreak = _FundAccountingRpt.PageBreak;
                                    //incRowExcel++;
                                }


                                //-----------------------------------
                                using (SqlConnection DbCon1 = new SqlConnection(Tools.conString))
                                {
                                    DbCon1.Open();
                                    using (SqlCommand cmd1 = DbCon1.CreateCommand())
                                    {
                                        //                                              
                                        cmd1.CommandText =
                                        @"
                                                select isnull(B.Name,'') FundName,isnull(C.ID,'') SecuritiesCode,isnull(C.Name,'') SecuritiesDescription, 
                                                isnull(A.Balance,0) QtyOfUnit, isnull(A.Balance,0) / 100 Lot, 
                                                isnull(A.AvgPrice,0) AvgCost,
                                                isnull(A.Balance,0) * isnull(A.AvgPrice,0) BookValue,
                                                isnull(A.ClosePrice,0) MarketPrice,
                                                isnull(A.Balance,0) * isnull(A.ClosePrice,0) MarketValue,
                                                (isnull(A.Balance,0) * isnull(A.ClosePrice,0)) - (isnull(A.Balance,0) * isnull(A.AvgPrice,0))  Unrealised,
                                                case when (isnull(A.Balance,0) * isnull(A.AvgPrice,0)) > 0 then
                                                ((isnull(A.Balance,0) * isnull(A.ClosePrice,0)) - (isnull(A.Balance,0) * isnull(A.AvgPrice,0))) /  (isnull(A.Balance,0) * isnull(A.AvgPrice,0)) * 100 
                                                else 0 end UnrealisedPercent,
                                                '' ByMarketCap,
                                                isnull(D.ID,'') SubSector,
                                                isnull(E.Name,'') Sector
                                                from fundPosition A
                                                left join Fund B on A.FundPK = B.FundPK and B.status in (1,2)
                                                left join Instrument C on A.InstrumentPK = C.InstrumentPK and C.status in (1,2)
                                                left join SubSector D on C.SectorPK = D.SubSectorPK and D.status in (1,2)
                                                left join Sector E on D.SectorPK = E.SectorPK and E.status in (1,2)
                                                where A.status = 2 and A.Date = @Date
                                                and A.FundPK= @FundPK
                                                and C.InstrumentTypePK in (1,4,16)
                                                ";
                                        cmd1.CommandTimeout = 0;

                                        cmd1.Parameters.AddWithValue("@Date", _FundAccountingRpt.ValueDateFrom);
                                        cmd1.Parameters.AddWithValue("@FundPK", _FundAccountingRpt.FundFrom);

                                        cmd1.ExecuteNonQuery();


                                        using (SqlDataReader dr1 = cmd1.ExecuteReader())
                                        {
                                            if (dr1.HasRows)
                                                _row2 = dr1.HasRows;
                                            {
                                                using (ExcelPackage package1 = new ExcelPackage(excelFile))
                                                {

                                                    //ATUR DATA GROUPINGNYA DULU
                                                    List<PVRRpt> rList1 = new List<PVRRpt>();
                                                    while (dr1.Read())
                                                    {
                                                        PVRRpt rSingle1 = new PVRRpt();
                                                        rSingle1.FundName = Convert.ToString(dr1["FundName"]);
                                                        rSingle1.InstrumentID = Convert.ToString(dr1["SecuritiesCode"]);
                                                        rSingle1.InstrumentName = dr1["SecuritiesDescription"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr1["SecuritiesDescription"]);
                                                        rSingle1.UnitQuantity = dr1["QtyOfUnit"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr1["QtyOfUnit"]);
                                                        rSingle1.AverageCost = dr1["AvgCost"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr1["AvgCost"]);
                                                        rSingle1.BookValue = dr1["BookValue"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr1["BookValue"]);
                                                        rSingle1.MarketPrice = Convert.ToDecimal(dr1["MarketPrice"]);
                                                        rSingle1.MarketValue = Convert.ToDecimal(dr1["MarketValue"]);
                                                        rSingle1.UnrealizedProfitLoss = Convert.ToDecimal(dr1["Unrealised"]);
                                                        rSingle1.PercentProfilLoss = Convert.ToDecimal(dr1["UnrealisedPercent"]);
                                                        rSingle1.Lot = Convert.ToDecimal(dr1["Lot"]);
                                                        rSingle1.MarketCap = Convert.ToString(dr1["ByMarketCap"]);
                                                        rSingle1.Sector = Convert.ToString(dr1["Sector"]);
                                                        //rSingle1.PercentTA = Convert.ToDecimal(dr1["PercentTA"]);
                                                        //rSingle1.PercentYTM = Convert.ToDecimal(dr0["PercentYTM"]);
                                                        //rSingle1.MDur = Convert.ToDecimal(dr0["MDur"]);
                                                        //rSingle1.CouponRate = Convert.ToDecimal(dr0["CouponRate"]);
                                                        //rSingle1.Compliance = Convert.ToDecimal(dr1["Compliance"]);
                                                        //rSingle1.RatingObligasi = Convert.ToDecimal(dr0["RatingObligasi"]);
                                                        //rSingle1.ComplianceIBPA = Convert.ToDecimal(dr0["ComplianceIBPA"]);
                                                        //rSingle1.BondType = Convert.ToDecimal(dr0["BondType"]);

                                                        rList1.Add(rSingle1);

                                                    }


                                                    var QueryByFundID1 =
                                                        from r1 in rList1
                                                        group r1 by new { r1.FundName } into rGroup1
                                                        select rGroup1;
                                                    incRowExcel = incRowExcel + 10;
                                                    int _StartRow = incRowExcel + 1;
                                                    foreach (var rsHeader1 in QueryByFundID1)
                                                    {
                                                        _this = incRowExcel;
                                                        worksheet.Cells[incRowExcel, 1].Value = Convert.ToDateTime(_FundAccountingRpt.ValueDateFrom).ToString("dd-MMM-yy");
                                                        worksheet.Cells["A" + incRowExcel + ":B" + incRowExcel].Merge = true;
                                                        worksheet.Cells["A" + incRowExcel + ":B" + incRowExcel].Style.Font.Size = 14;

                                                        incRowExcel++;
                                                        worksheet.Cells[incRowExcel, 1].Style.Font.Size = 16;
                                                        worksheet.Cells[incRowExcel, 1].Value = rsHeader1.Key.FundName;
                                                        worksheet.Cells["A" + incRowExcel + ":E" + incRowExcel].Style.Font.Bold = true;
                                                        worksheet.Cells["A" + incRowExcel + ":E" + incRowExcel].Merge = true;
                                                        worksheet.Cells["A" + incRowExcel + ":E" + incRowExcel].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                                        worksheet.Cells["A" + incRowExcel + ":E" + incRowExcel].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
                                                        worksheet.Cells["A" + incRowExcel + ":E" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                        incRowExcel = incRowExcel + 2;
                                                        worksheet.Cells["A" + incRowExcel + ":Q" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                        worksheet.Cells["A" + incRowExcel + ":Q" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                        worksheet.Cells["A" + incRowExcel + ":Q" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                                        worksheet.Cells["A" + incRowExcel + ":Q" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                                        worksheet.Cells["A" + incRowExcel + ":Q" + incRowExcel].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                                        worksheet.Cells["A" + incRowExcel + ":Q" + incRowExcel].Style.Fill.BackgroundColor.SetColor(Color.LightGray);

                                                        worksheet.Cells[incRowExcel, 1].Value = "No.";
                                                        worksheet.Cells[incRowExcel, 2].Value = "Securities CODE";
                                                        worksheet.Cells[incRowExcel, 3].Value = "Securities Description";
                                                        worksheet.Cells[incRowExcel, 4].Value = "Qty Of Unit";
                                                        worksheet.Cells[incRowExcel, 5].Value = "Lot";
                                                        worksheet.Cells[incRowExcel, 6].Value = "Average Cost";
                                                        worksheet.Cells[incRowExcel, 7].Value = "Book Value";
                                                        worksheet.Cells[incRowExcel, 8].Value = "Market Price";
                                                        worksheet.Cells[incRowExcel, 9].Value = "Market Value";
                                                        worksheet.Cells[incRowExcel, 10].Value = "Unrealized Profit/(Loss)";
                                                        worksheet.Cells[incRowExcel, 11].Value = "% fr P/L";
                                                        //worksheet.Cells[incRowExcel, 12].Value = "BY MARKET CAP";
                                                        worksheet.Cells[incRowExcel, 13].Value = "BY SECTOR";
                                                        worksheet.Cells[incRowExcel, 14].Value = "% fr TA";
                                                        worksheet.Cells[incRowExcel, 15].Value = "Beta";
                                                        worksheet.Cells[incRowExcel, 16].Value = "% Segment";
                                                        worksheet.Cells[incRowExcel, 17].Value = "Compliance";
                                                        worksheet.Cells["A" + incRowExcel + ":Q" + incRowExcel].Style.Font.Bold = true;

                                                        worksheet.Cells["A" + incRowExcel + ":Q" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                        incRowExcel = incRowExcel + 2;

                                                        worksheet.Cells[incRowExcel, 2].Value = "STOCKS";
                                                        worksheet.Cells[incRowExcel, 2].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                                        worksheet.Cells[incRowExcel, 2].Style.Fill.BackgroundColor.SetColor(Color.LightGray);

                                                        incRowExcel++;
                                                        //area header

                                                        int _no = 1;
                                                        int _startRowDetail = incRowExcel;
                                                        int _endRowDetail = 0;
                                                        var _fundID = "";
                                                        foreach (var rsDetail1 in rsHeader1)
                                                        {
                                                            worksheet.Cells["A" + incRowExcel + ":A" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                            worksheet.Cells["B" + incRowExcel + ":B" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                            worksheet.Cells["C" + incRowExcel + ":J" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                            worksheet.Cells["K" + incRowExcel + ":M" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                            worksheet.Cells["N" + incRowExcel + ":Q" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                            worksheet.Cells[incRowExcel, 1].Value = _no;
                                                            worksheet.Cells[incRowExcel, 2].Value = rsDetail1.InstrumentID;
                                                            worksheet.Cells[incRowExcel, 3].Value = rsDetail1.InstrumentName;
                                                            worksheet.Cells[incRowExcel, 3].Style.WrapText = true;
                                                            worksheet.Cells[incRowExcel, 4].Value = rsDetail1.UnitQuantity;
                                                            worksheet.Cells[incRowExcel, 5].Value = rsDetail1.Lot;
                                                            worksheet.Cells[incRowExcel, 6].Value = rsDetail1.AverageCost;
                                                            worksheet.Cells[incRowExcel, 7].Value = rsDetail1.BookValue;
                                                            worksheet.Cells[incRowExcel, 8].Value = rsDetail1.MarketPrice;
                                                            worksheet.Cells[incRowExcel, 9].Value = rsDetail1.MarketValue;
                                                            worksheet.Cells[incRowExcel, 10].Value = rsDetail1.UnrealizedProfitLoss;
                                                            worksheet.Cells[incRowExcel, 11].Value = rsDetail1.PercentProfilLoss / 100;
                                                            worksheet.Cells[incRowExcel, 11].Style.Numberformat.Format = "0.00 %";

                                                            //worksheet.Cells[incRowExcel, 12].Value = rsDetail1.MarketCap;
                                                            worksheet.Cells[incRowExcel, 13].Value = rsDetail1.Sector;

                                                            worksheet.Cells[incRowExcel, 14].Formula = "I" + incRowExcel + "/G2" + "";
                                                            worksheet.Cells[incRowExcel, 14].Calculate();
                                                            worksheet.Cells[incRowExcel, 14].Style.Numberformat.Format = "0.00 %";
                                                            worksheet.Cells["A" + incRowExcel + ":J" + incRowExcel].Style.Numberformat.Format = "#,##";
                                                            //ThickBox Border
                                                            _endRowDetail = incRowExcel;
                                                            incRowExcel++;
                                                            _no++;
                                                        }

                                                        incRowExcel++;
                                                        worksheet.Cells["A" + incRowExcel + ":Q" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                        worksheet.Cells["A" + incRowExcel + ":Q" + incRowExcel].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                                        worksheet.Cells["A" + incRowExcel + ":Q" + incRowExcel].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
                                                        worksheet.Cells[incRowExcel, 2].Value = "TOTAL";

                                                        worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##0";
                                                        worksheet.Cells[incRowExcel, 4].Formula = "SUM(D" + _startRowDetail + ":D" + _endRowDetail + ")";
                                                        worksheet.Cells[incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                        worksheet.Cells[incRowExcel, 4].Calculate();

                                                        worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0";
                                                        worksheet.Cells[incRowExcel, 5].Formula = "SUM(E" + _startRowDetail + ":E" + _endRowDetail + ")";
                                                        worksheet.Cells[incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                        worksheet.Cells[incRowExcel, 5].Calculate();

                                                        worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0.00";
                                                        worksheet.Cells[incRowExcel, 7].Formula = "SUM(G" + _startRowDetail + ":G" + _endRowDetail + ")";
                                                        worksheet.Cells[incRowExcel, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                        worksheet.Cells[incRowExcel, 7].Calculate();

                                                        _rowMarketValue1 = incRowExcel;
                                                        worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0.00";
                                                        worksheet.Cells[incRowExcel, 9].Formula = "SUM(I" + _startRowDetail + ":I" + _endRowDetail + ")";
                                                        worksheet.Cells[incRowExcel, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                        worksheet.Cells[incRowExcel, 9].Calculate();

                                                        worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0.00";
                                                        worksheet.Cells[incRowExcel, 10].Formula = "SUM(J" + _startRowDetail + ":J" + _endRowDetail + ")";
                                                        worksheet.Cells[incRowExcel, 10].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                        worksheet.Cells[incRowExcel, 10].Calculate();

                                                        worksheet.Cells[incRowExcel, 11].Style.Numberformat.Format = "0.00%";
                                                        worksheet.Cells[incRowExcel, 11].Formula = "SUM(J" + incRowExcel + "/G" + incRowExcel + ")";
                                                        worksheet.Cells[incRowExcel, 11].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                        worksheet.Cells[incRowExcel, 11].Calculate();

                                                        worksheet.Cells[incRowExcel, 14].Style.Numberformat.Format = "0.00%";
                                                        worksheet.Cells[incRowExcel, 14].Formula = "SUM(I" + incRowExcel + "/G" + 2 + ")";
                                                        worksheet.Cells[incRowExcel, 14].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                        worksheet.Cells[incRowExcel, 14].Calculate();

                                                        worksheet.Cells[incRowExcel, 15].Style.Numberformat.Format = "0.00%";
                                                        worksheet.Cells[incRowExcel, 15].Formula = "SUM(O" + _startRowDetail + ":O" + _endRowDetail + ")";
                                                        worksheet.Cells[incRowExcel, 15].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                        worksheet.Cells[incRowExcel, 15].Calculate();

                                                        worksheet.Cells[incRowExcel, 16].Style.Numberformat.Format = "#,##0.00";
                                                        worksheet.Cells[incRowExcel, 16].Formula = "SUM(P" + _startRowDetail + ":P" + _endRowDetail + ")";
                                                        worksheet.Cells[incRowExcel, 16].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                        worksheet.Cells[incRowExcel, 16].Calculate();

                                                        worksheet.Cells[incRowExcel, 17].Style.Numberformat.Format = "0.00%";
                                                        worksheet.Cells[incRowExcel, 17].Formula = "SUM(Q" + _startRowDetail + ":Q" + _endRowDetail + ")";
                                                        worksheet.Cells[incRowExcel, 17].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                        worksheet.Cells[incRowExcel, 17].Calculate();

                                                        worksheet.Cells["A" + incRowExcel + ":Q" + incRowExcel].Style.Font.Bold = true;
                                                        worksheet.Cells["A" + _startRowDetail + ":Q" + _endRowDetail].Style.Border.Top.Style = ExcelBorderStyle.Dotted;
                                                        worksheet.Cells["A" + _startRowDetail + ":Q" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Dotted;
                                                        worksheet.Cells["A" + _startRowDetail + ":Q" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Dotted;
                                                        worksheet.Cells["A" + _startRowDetail + ":Q" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Dotted;
                                                    }


                                                }
                                            }
                                        }
                                    }
                                }

                                incRowExcel = incRowExcel + 2;

                                //-----------------------------------
                                using (SqlConnection DbCon2 = new SqlConnection(Tools.conString))
                                {
                                    DbCon2.Open();
                                    using (SqlCommand cmd2 = DbCon2.CreateCommand())
                                    {
                                        //                                              
                                        cmd2.CommandText =
                                        @"
                                                select isnull(C.ID,'') SecuritiesCode,isnull(C.Name,'') SecuritiesDescription, 
                                                isnull(A.Balance,0) QtyOfUnit, C.MaturityDate, 
                                                isnull(A.AvgPrice,0) AvgCost,
                                                isnull(A.Balance,0) * isnull(A.AvgPrice,0) BookValue,
                                                isnull(A.ClosePrice,0) MarketPrice,
                                                isnull(A.Balance,0) * isnull(A.ClosePrice,0) MarketValue,
                                                (isnull(A.Balance,0) * isnull(A.ClosePrice,0)) - (isnull(A.Balance,0) * isnull(A.AvgPrice,0))  Unrealised,
                                                case when (isnull(A.Balance,0) * isnull(A.AvgPrice,0)) > 0 then
                                                ((isnull(A.Balance,0) * isnull(A.ClosePrice,0)) - (isnull(A.Balance,0) * isnull(A.AvgPrice,0))) /  (isnull(A.Balance,0) * isnull(A.AvgPrice,0)) * 100 else 0 end UnrealisedPercent,
                                                 isnull(D.Name,'') BondType
                                                from fundPosition A
                                                left join Fund B on A.FundPK = B.FundPK and B.status in (1,2)
                                                left join Instrument C on A.InstrumentPK = C.InstrumentPK and C.status in (1,2)
                                                left join instrumentType D on C.InstrumentTypePK = D.InstrumentTypePK and D.status in (1,2)
                                                where A.status = 2 
                                                and A.Date = @Date
                                                and A.FundPK= @FundPK
						                        and C.InstrumentTypePK in (2,3,8,9,11,12,13,14,15)
                                                ";
                                        cmd2.CommandTimeout = 0;

                                        cmd2.Parameters.AddWithValue("@Date", _FundAccountingRpt.ValueDateFrom);
                                        cmd2.Parameters.AddWithValue("@FundPK", _FundAccountingRpt.FundFrom);

                                        cmd2.ExecuteNonQuery();


                                        using (SqlDataReader dr2 = cmd2.ExecuteReader())
                                        {
                                            if (dr2.HasRows)
                                            {
                                                _row3 = dr2.HasRows;
                                                // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                                using (ExcelPackage package1 = new ExcelPackage(excelFile))
                                                {

                                                    //ATUR DATA GROUPINGNYA DULU
                                                    List<PVRRpt> rList1 = new List<PVRRpt>();
                                                    while (dr2.Read())
                                                    {
                                                        PVRRpt rSingle1 = new PVRRpt();
                                                        rSingle1.InstrumentID = Convert.ToString(dr2["SecuritiesCode"]);
                                                        rSingle1.InstrumentName = dr2["SecuritiesDescription"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr2["SecuritiesDescription"]);
                                                        rSingle1.UnitQuantity = dr2["QtyOfUnit"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr2["QtyOfUnit"]);
                                                        rSingle1.MaturityDate = Convert.ToString(dr2["MaturityDate"]);
                                                        rSingle1.AverageCost = dr2["AvgCost"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr2["AvgCost"]);
                                                        rSingle1.BookValue = dr2["BookValue"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr2["BookValue"]);
                                                        rSingle1.MarketPrice = Convert.ToDecimal(dr2["MarketPrice"]);
                                                        rSingle1.MarketValue = Convert.ToDecimal(dr2["MarketValue"]);
                                                        rSingle1.UnrealizedProfitLoss = Convert.ToDecimal(dr2["Unrealised"]);
                                                        rSingle1.PercentProfilLoss = Convert.ToDecimal(dr2["UnrealisedPercent"]);
                                                        //rSingle1.PercentTA = Convert.ToDecimal(dr1["PercentTA"]);
                                                        //rSingle1.PercentYTM = Convert.ToDecimal(dr0["PercentYTM"]);
                                                        //rSingle1.MDur = Convert.ToDecimal(dr0["MDur"]);
                                                        //rSingle1.CouponRate = Convert.ToDecimal(dr0["CouponRate"]);
                                                        //rSingle1.Compliance = Convert.ToDecimal(dr1["Compliance"]);
                                                        //rSingle1.RatingObligasi = Convert.ToDecimal(dr0["RatingObligasi"]);
                                                        //rSingle1.ComplianceIBPA = Convert.ToDecimal(dr0["ComplianceIBPA"]);
                                                        rSingle1.BondType = Convert.ToString(dr2["BondType"]);

                                                        rList1.Add(rSingle1);

                                                    }

                                                    int _lastRow1 = incRowExcel;

                                                    worksheet.Row(incRowExcel).PageBreak = _FundAccountingRpt.PageBreak;


                                                    var QueryByFundID1 =
                                                        from r1 in rList1
                                                        group r1 by new { } into rGroup1
                                                        select rGroup1;

                                                    incRowExcel = incRowExcel + 6;
                                                    int _StartRow = incRowExcel + 1;
                                                    foreach (var rsHeader1 in QueryByFundID1)
                                                    {

                                                        worksheet.Cells["A" + incRowExcel + ":S" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                        worksheet.Cells["A" + incRowExcel + ":S" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                        worksheet.Cells["A" + incRowExcel + ":S" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                                        worksheet.Cells["A" + incRowExcel + ":S" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                        worksheet.Cells[incRowExcel, 1].Value = "No.";
                                                        worksheet.Cells[incRowExcel, 2].Value = "Securities CODE";
                                                        worksheet.Cells[incRowExcel, 3].Value = "Securities Description";
                                                        worksheet.Cells[incRowExcel, 4].Value = "Qty Of Unit";
                                                        worksheet.Cells[incRowExcel, 5].Value = "Maturity Date";
                                                        worksheet.Cells[incRowExcel, 6].Value = "Average Cost";
                                                        worksheet.Cells[incRowExcel, 7].Value = "Book Value";
                                                        worksheet.Cells[incRowExcel, 8].Value = "Market Price";
                                                        worksheet.Cells[incRowExcel, 9].Value = "Market Value";
                                                        worksheet.Cells[incRowExcel, 10].Value = "Unrealized Profit/(Loss)";
                                                        worksheet.Cells[incRowExcel, 11].Value = "% fr P/L";
                                                        worksheet.Cells[incRowExcel, 12].Value = "% fr TA";
                                                        worksheet.Cells[incRowExcel, 13].Value = "% YTM";
                                                        worksheet.Cells[incRowExcel, 14].Value = "MDur";
                                                        worksheet.Cells[incRowExcel, 15].Value = "Coupon Rate";
                                                        worksheet.Cells[incRowExcel, 16].Value = "Compliance Max. 10%";
                                                        worksheet.Cells[incRowExcel, 17].Value = "Rating Obligasi";
                                                        worksheet.Cells[incRowExcel, 18].Value = "Compliance IBPA";
                                                        worksheet.Cells[incRowExcel, 19].Value = "BONDS TYPE";

                                                        worksheet.Cells["A" + incRowExcel + ":S" + incRowExcel].Style.Font.Bold = true;
                                                        worksheet.Cells["A" + incRowExcel + ":S" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                        worksheet.Cells["A" + incRowExcel + ":S" + incRowExcel].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                                        worksheet.Cells["A" + incRowExcel + ":S" + incRowExcel].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                                                        // Row C = 4

                                                        incRowExcel = incRowExcel + 2;
                                                        //area header
                                                        int _no = 1;
                                                        int _startRowDetail1 = incRowExcel;
                                                        int _endRowDetail1 = 0;
                                                        foreach (var rsDetail1 in rsHeader1)
                                                        {
                                                            worksheet.Cells["A" + incRowExcel + ":A" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                            worksheet.Cells["B" + incRowExcel + ":B" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                            worksheet.Cells["C" + incRowExcel + ":D" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                            worksheet.Cells["E" + incRowExcel + ":S" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                            worksheet.Cells[incRowExcel, 1].Value = _no;
                                                            worksheet.Cells[incRowExcel, 2].Value = rsDetail1.InstrumentID;
                                                            worksheet.Cells[incRowExcel, 3].Value = rsDetail1.InstrumentName;
                                                            worksheet.Cells[incRowExcel, 3].Style.WrapText = true;
                                                            worksheet.Cells[incRowExcel, 4].Value = rsDetail1.UnitQuantity;
                                                            worksheet.Cells[incRowExcel, 5].Value = Convert.ToDateTime(rsDetail1.MaturityDate).ToString("dd-MMM-yy");
                                                            worksheet.Cells[incRowExcel, 6].Value = rsDetail1.AverageCost;
                                                            worksheet.Cells[incRowExcel, 7].Value = rsDetail1.BookValue / 100;
                                                            worksheet.Cells[incRowExcel, 8].Value = rsDetail1.MarketPrice;
                                                            worksheet.Cells[incRowExcel, 9].Value = rsDetail1.MarketValue / 100;
                                                            worksheet.Cells[incRowExcel, 10].Value = rsDetail1.UnrealizedProfitLoss / 100;
                                                            worksheet.Cells[incRowExcel, 11].Value = rsDetail1.PercentProfilLoss;
                                                            worksheet.Cells[incRowExcel, 12].Formula = "I" + incRowExcel + "/C31";
                                                            worksheet.Cells[incRowExcel, 12].Style.Numberformat.Format = "0.00 %";
                                                            worksheet.Cells[incRowExcel, 12].Calculate();
                                                            worksheet.Cells[incRowExcel, 13].Value = "-";
                                                            worksheet.Cells[incRowExcel, 14].Value = "-";
                                                            worksheet.Cells[incRowExcel, 15].Value = "-";
                                                            worksheet.Cells[incRowExcel, 16].Value = "-";
                                                            worksheet.Cells[incRowExcel, 17].Value = "-";
                                                            worksheet.Cells[incRowExcel, 18].Value = "-";
                                                            worksheet.Cells[incRowExcel, 19].Value = rsDetail1.BondType;
                                                            worksheet.Cells["A" + incRowExcel + ":K" + incRowExcel].Style.Numberformat.Format = "#,##";
                                                            worksheet.Cells["M" + incRowExcel + ":S" + incRowExcel].Style.Numberformat.Format = "#,##";
                                                            incRowExcel++;
                                                            _endRowDetail1 = incRowExcel;
                                                            _no++;
                                                        }
                                                        int _EndRow = incRowExcel;
                                                        incRowExcel++;

                                                        worksheet.Cells["A" + incRowExcel + ":S" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                        worksheet.Cells["A" + incRowExcel + ":S" + incRowExcel].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                                        worksheet.Cells["A" + incRowExcel + ":S" + incRowExcel].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
                                                        worksheet.Cells["A" + incRowExcel + ":S" + incRowExcel].Style.Font.Bold = true;
                                                        worksheet.Cells[incRowExcel, 2].Value = "TOTAL";

                                                        worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##0.00";
                                                        worksheet.Cells[incRowExcel, 4].Formula = "SUM(D" + _startRowDetail1 + ":D" + _endRowDetail1 + ")";
                                                        worksheet.Cells[incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                        worksheet.Cells[incRowExcel, 4].Calculate();

                                                        worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0.00";
                                                        worksheet.Cells[incRowExcel, 7].Formula = "SUM(G" + _startRowDetail1 + ":G" + _endRowDetail1 + ")";
                                                        worksheet.Cells[incRowExcel, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                        worksheet.Cells[incRowExcel, 7].Calculate();

                                                        _rowMarketValue2 = incRowExcel;
                                                        worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0.00";
                                                        worksheet.Cells[incRowExcel, 9].Formula = "SUM(I" + _startRowDetail1 + ":I" + _endRowDetail1 + ")";
                                                        worksheet.Cells[incRowExcel, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                        worksheet.Cells[incRowExcel, 9].Calculate();

                                                        worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0.00";
                                                        worksheet.Cells[incRowExcel, 10].Formula = "SUM(J" + _startRowDetail1 + ":J" + _endRowDetail1 + ")";
                                                        worksheet.Cells[incRowExcel, 10].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                        worksheet.Cells[incRowExcel, 10].Calculate();

                                                        worksheet.Cells[incRowExcel, 11].Style.Numberformat.Format = "0.00%";
                                                        worksheet.Cells[incRowExcel, 11].Formula = "SUM(J" + incRowExcel + "/G" + incRowExcel + ")";
                                                        worksheet.Cells[incRowExcel, 11].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                        worksheet.Cells[incRowExcel, 11].Calculate();

                                                        //worksheet.Cells[incRowExcel, 12].Style.Numberformat.Format = "#,##0.0000";
                                                        //worksheet.Cells[incRowExcel, 12].Formula = "SUM(O" + _startRowDetail + ":O" + _endRowDetail + ")";
                                                        //worksheet.Cells[incRowExcel, 12].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                        //worksheet.Cells[incRowExcel, 12].Calculate();

                                                        //worksheet.Cells[incRowExcel, 13].Style.Numberformat.Format = "#,##0.0000";
                                                        //worksheet.Cells[incRowExcel, 13].Formula = "SUM(P" + _startRowDetail + ":P" + _endRowDetail + ")";
                                                        //worksheet.Cells[incRowExcel, 13].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                        //worksheet.Cells[incRowExcel, 13].Calculate();

                                                        //worksheet.Cells[incRowExcel, 14].Style.Numberformat.Format = "#,##0.0000";
                                                        //worksheet.Cells[incRowExcel, 14].Formula = "SUM(N" + _startRowDetail + ":N" + _endRowDetail + ")";
                                                        //worksheet.Cells[incRowExcel, 14].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                        //worksheet.Cells[incRowExcel, 14].Calculate();

                                                        worksheet.Cells["A" + _startRowDetail1 + ":S" + _EndRow].Style.Border.Top.Style = ExcelBorderStyle.Dotted;
                                                        worksheet.Cells["A" + _StartRow + ":S" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Dotted;
                                                        worksheet.Cells["A" + _StartRow + ":S" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Dotted;
                                                        worksheet.Cells["A" + _startRowDetail1 + ":S" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Dotted;

                                                    }


                                                }
                                            }
                                        }
                                    }
                                }

                                incRowExcel = incRowExcel + 2;

                                //-----------------------------------
                                using (SqlConnection DbCon2 = new SqlConnection(Tools.conString))
                                {
                                    DbCon2.Open();
                                    using (SqlCommand cmd2 = DbCon2.CreateCommand())
                                    {
                                        //                                              
                                        cmd2.CommandText =
                                        @"
                                                select 
                                                isnull(F.Name,'') SecuritiesCode,
                                                isnull(F.SInvestID,'') BiCode,
                                                isnull(E.ID,'') Branch,
                                                isnull(A.Balance,0) Nominal,
                                                A.AcqDate TradeDate,
                                                A.MaturityDate,
                                                A.InterestPercent Rate,
                                                [dbo].[Fgetdepositointerestaccrued] (@date,A.InstrumentPK,A.Balance,A.InterestDaysType,A.InterestPercent,A.AcqDate) AccruedInterest
                                                from fundPosition A
                                                left join Fund B on A.FundPK = B.FundPK and B.status in (1,2)
                                                left join Instrument C on A.InstrumentPK = C.InstrumentPK and C.status in (1,2)
                                                left join instrumentType D on C.InstrumentTypePK = D.InstrumentTypePK and D.status in (1,2)
                                                left join BankBranch E on A.BankBranchPK = E.BankBranchPK and E.status in (1,2)
                                                left join Bank F on E.BankPK = F.BankPK and F.status in (1,2)
                                                where A.status = 2 and A.Date = @Date
                                                and A.FundPK= @FundPK and C.InstrumentTypePK in (5)
                                                ";
                                        cmd2.CommandTimeout = 0;

                                        cmd2.Parameters.AddWithValue("@date", _FundAccountingRpt.ValueDateFrom);
                                        cmd2.Parameters.AddWithValue("@FundPK", _FundAccountingRpt.FundFrom);

                                        cmd2.ExecuteNonQuery();


                                        using (SqlDataReader dr3 = cmd2.ExecuteReader())
                                        {
                                            if (dr3.HasRows)
                                            {
                                                _row4 = dr3.HasRows;
                                                // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                                using (ExcelPackage package1 = new ExcelPackage(excelFile))
                                                {

                                                    //ATUR DATA GROUPINGNYA DULU
                                                    List<PVRRpt> rList2 = new List<PVRRpt>();
                                                    while (dr3.Read())
                                                    {
                                                        PVRRpt rSingle2 = new PVRRpt();
                                                        rSingle2.TimeDeposit = Convert.ToString(dr3["SecuritiesCode"]);
                                                        rSingle2.BICode = Convert.ToString(dr3["BICode"]);
                                                        rSingle2.Branch = Convert.ToString(dr3["Branch"]);
                                                        rSingle2.Nominal = Convert.ToDecimal(dr3["Nominal"]);
                                                        rSingle2.TradeDate = Convert.ToString(dr3["TradeDate"]);
                                                        rSingle2.MaturityDate = Convert.ToString(dr3["MaturityDate"]);
                                                        rSingle2.Rate = Convert.ToDecimal(dr3["Rate"]);
                                                        rSingle2.AccTD = Convert.ToDecimal(dr3["AccruedInterest"]);
                                                        //rSingle2.PercentTA = Convert.ToDecimal(dr2["PercentTA"]);
                                                        //rSingle2.MaturityAlert = Convert.ToString(dr2["MaturityAlert"]);
                                                        rList2.Add(rSingle2);

                                                    }


                                                    var QueryByFundID2 =
                                                        from r2 in rList2
                                                        group r2 by new { } into rGroup2
                                                        select rGroup2;

                                                    incRowExcel = incRowExcel + 6;

                                                    int _startRow = incRowExcel;
                                                    foreach (var rsHeader2 in QueryByFundID2)
                                                    {
                                                        worksheet.Cells["A" + incRowExcel + ":K" + incRowExcel].Style.Font.Bold = true;
                                                        worksheet.Cells["A" + incRowExcel + ":K" + incRowExcel].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                                        worksheet.Cells["A" + incRowExcel + ":K" + incRowExcel].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                                                        worksheet.Cells["A" + incRowExcel + ":K" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                        worksheet.Cells[incRowExcel, 1].Value = "No";
                                                        worksheet.Cells[incRowExcel, 2].Value = "TIME DEPOSITS";
                                                        worksheet.Cells[incRowExcel, 3].Value = "BI CODE";
                                                        worksheet.Cells[incRowExcel, 4].Value = "BRANCH";
                                                        worksheet.Cells[incRowExcel, 5].Value = "Nominal";
                                                        worksheet.Cells[incRowExcel, 6].Value = "Trade Date";
                                                        worksheet.Cells[incRowExcel, 7].Value = "Maturity Date";
                                                        worksheet.Cells[incRowExcel, 8].Value = "Rate(Gross)";
                                                        worksheet.Cells[incRowExcel, 9].Value = "Acc Int. TD(Net)";
                                                        worksheet.Cells[incRowExcel, 10].Value = "%fr TA";
                                                        worksheet.Cells[incRowExcel, 11].Value = "Mature Alert";
                                                        incRowExcel++;

                                                        // Row C = 4
                                                        int RowCZ = incRowExcel;

                                                        //area header
                                                        int _no = 1;
                                                        int _startRowDetail = incRowExcel;
                                                        int _endRowDetail = 0;
                                                        foreach (var rsDetail2 in rsHeader2)
                                                        {
                                                            worksheet.Cells["A" + incRowExcel + ":A" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                            worksheet.Cells["B" + incRowExcel + ":I" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                            worksheet.Cells["J" + incRowExcel + ":K" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                            worksheet.Cells[incRowExcel, 1].Value = _no;
                                                            worksheet.Cells[incRowExcel, 2].Value = rsDetail2.TimeDeposit;
                                                            worksheet.Cells[incRowExcel, 3].Value = rsDetail2.BICode;
                                                            worksheet.Cells[incRowExcel, 4].Value = rsDetail2.Branch;
                                                            worksheet.Cells[incRowExcel, 5].Value = rsDetail2.Nominal;
                                                            worksheet.Cells[incRowExcel, 6].Value = Convert.ToDateTime(rsDetail2.TradeDate).ToString("dd-MMM-yy");
                                                            worksheet.Cells[incRowExcel, 7].Value = Convert.ToDateTime(rsDetail2.MaturityDate).ToString("dd-MMM-yy");
                                                            worksheet.Cells[incRowExcel, 8].Value = Convert.ToDecimal(rsDetail2.Rate / 100).ToString("0.00 %");
                                                            worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "0.00 %";
                                                            worksheet.Cells[incRowExcel, 9].Formula = "((E" + incRowExcel + "*H" + incRowExcel + "*(A" + _this + "-F" + incRowExcel + "))/365)*0.8/100";
                                                            worksheet.Cells[incRowExcel, 9].Calculate();
                                                            worksheet.Cells[incRowExcel, 10].Formula = "E" + incRowExcel + "/G2";
                                                            worksheet.Cells[incRowExcel, 10].Calculate();
                                                            worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "0.00 %";
                                                            _endRowDetail = incRowExcel;
                                                            worksheet.Cells["A" + incRowExcel + ":I" + incRowExcel].Style.Numberformat.Format = "#,##";
                                                            worksheet.Cells["K" + incRowExcel + ":K" + incRowExcel].Style.Numberformat.Format = "#,##";
                                                            incRowExcel++;
                                                            _no++;

                                                        }

                                                        worksheet.Cells["A" + _startRow + ":K" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                        worksheet.Cells["A" + _startRow + ":K" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                        worksheet.Cells["A" + _startRow + ":K" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                                        worksheet.Cells["A" + _startRow + ":K" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                        worksheet.Cells["A" + incRowExcel + ":K" + incRowExcel].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                                        worksheet.Cells["A" + incRowExcel + ":K" + incRowExcel].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);


                                                        _RowNominal = incRowExcel;
                                                        worksheet.Cells["A" + incRowExcel + ":K" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                        worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.00";
                                                        worksheet.Cells[incRowExcel, 5].Formula = "SUM(E" + _startRowDetail + ":E" + _endRowDetail + ")";
                                                        worksheet.Cells[incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                        worksheet.Cells[incRowExcel, 5].Calculate();

                                                        worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0.00";
                                                        worksheet.Cells[incRowExcel, 9].Formula = "SUM(I" + _startRowDetail + ":I" + _endRowDetail + ")";
                                                        worksheet.Cells[incRowExcel, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                        worksheet.Cells[incRowExcel, 9].Calculate();

                                                        worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "0.00%";
                                                        worksheet.Cells[incRowExcel, 10].Formula = "SUM(J" + _startRowDetail + ":J" + _endRowDetail + ")";
                                                        worksheet.Cells[incRowExcel, 10].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                        worksheet.Cells[incRowExcel, 10].Calculate();

                                                        worksheet.Cells["A" + incRowExcel + ":K" + incRowExcel].Style.Font.Bold = true;

                                                        incRowExcel++;
                                                    }


                                                }
                                            }
                                        }
                                    }
                                }

                                //if (_row2 == false)
                                //{
                                //    worksheet.Cells[_startAsset, 7].Style.Numberformat.Format = "#,##0.00";
                                //    worksheet.Cells[_startAsset, 7].Formula = "SUM(C31" + "+E" + _RowNominal + "+I" + _rowMarketValue2 + ")";
                                //    worksheet.Cells[_startAsset, 7].Calculate();
                                //}
                                //else if (_row3 == false)
                                //{
                                //    worksheet.Cells[_startAsset, 7].Style.Numberformat.Format = "#,##0.00";
                                //    worksheet.Cells[_startAsset, 7].Formula = "SUM(C31" + "+E" + _RowNominal + "+I" + _rowMarketValue1 + ")";
                                //    worksheet.Cells[_startAsset, 7].Calculate();
                                //}

                                //else if (_row4 == false)
                                //{
                                //    worksheet.Cells[_startAsset, 7].Style.Numberformat.Format = "#,##0.00";
                                //    worksheet.Cells[_startAsset, 7].Formula = "SUM(C31" + "+I" + _rowMarketValue1 + "+I" + _rowMarketValue2 + ")";
                                //    worksheet.Cells[_startAsset, 7].Calculate();
                                //}
                                //else
                                //{
                                //    worksheet.Cells[_startAsset, 7].Style.Numberformat.Format = "#,##0.00";
                                //    worksheet.Cells[_startAsset, 7].Formula = "SUM(C31" + "+E" + _RowNominal + "+I" + _rowMarketValue1 + "+I" + _rowMarketValue2 + ")";
                                //    worksheet.Cells[_startAsset, 7].Calculate();
                                //}


                                int _lastRow = incRowExcel;

                                worksheet.Row(incRowExcel).PageBreak = _FundAccountingRpt.PageBreak;

                                worksheet.PrinterSettings.FitToPage = true;
                                worksheet.PrinterSettings.FitToWidth = 1;
                                worksheet.PrinterSettings.FitToHeight = 0;
                                worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel - 1, 19];
                                worksheet.Column(1).Width = 5;
                                worksheet.Column(2).Width = 30;
                                worksheet.Column(3).Width = 25;
                                worksheet.Column(4).Width = 25;
                                worksheet.Column(5).Width = 25;
                                worksheet.Column(6).Width = 25;
                                worksheet.Column(7).Width = 25;
                                worksheet.Column(8).Width = 25;
                                worksheet.Column(9).Width = 25;
                                worksheet.Column(10).Width = 25;
                                worksheet.Column(11).Width = 15;
                                worksheet.Column(12).Width = 25;
                                worksheet.Column(13).Width = 55;
                                worksheet.Column(14).Width = 15;
                                worksheet.Column(15).Width = 25;
                                worksheet.Column(16).Width = 25;
                                worksheet.Column(17).Width = 20;
                                worksheet.Column(18).Width = 20;
                                worksheet.Column(19).Width = 20;



                                // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                //worksheet.Cells["A3:I14"].AutoFitColumns(); // UNTUK AUTO FIT UKURAN TULISAN DENGAN KOLOM, KEBETULAN INI BUAT HEADER AJA DIPAKENYA
                                //worksheetApproved.Cells["A3:E3"].AutoFitColumns();
                                worksheet.PrinterSettings.Orientation = eOrientation.Landscape; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                worksheet.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN

                                worksheet.HeaderFooter.OddHeader.CenteredText = "\n \n \n \n \n \n \n &30&B Portfolio Valuation Report";


                                worksheet.PrinterSettings.TopMargin = (decimal).3 / 2.54M; // narrow border
                                worksheet.PrinterSettings.RightMargin = (decimal).3 / 2.54M; //narrow borderExcelWorksheet ws = pck.Workbook.Worksheets.Add("Demo");
                                worksheet.PrinterSettings.LeftMargin = (decimal).3 / 2.54M; // narrow border

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

                                worksheet.HeaderFooter.OddFooter.LeftAlignedText = Tools.DefaultReportFooterLeftText();


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
                catch (Exception err)
                {
                    return false;
                    throw err;
                }

            }
            #endregion



            #region Daily Compliance Report
            else if (_FundAccountingRpt.ReportName.Equals("Daily Compliance Report"))
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
                                _paramFund = " And A.FundPK in ( " + _FundAccountingRpt.FundFrom + " ) ";

                            }
                            else
                            {
                                _paramFund = "";
                            }

                            cmd.CommandText =

                             @"
                            
                                create table #A (FundID nvarchar(50),FundName nvarchar(100),DepositoAmount numeric(22,4),DEPPercentOfNav numeric(18,4),BondAmount numeric(22,4),BondPercentOfNav numeric(18,4),EquityAmount numeric(22,4),EQPercentOfNav numeric(18,4))
                                insert into #A (FundID,FundName,DepositoAmount,DEPPercentOfNav,BondAmount,BondPercentOfNav,EquityAmount,EQPercentOfNav)
                                select B.ID,B.Name,0,0,0,0,isnull(sum(A.MarketValue),0) MarketValue,case when isnull(D.AUM,0) = 0 then 0 else isnull(sum((A.MarketValue / 
                                case when D.AUM = 0 then 1 else isnull(D.AUM,1) End) * 100),0) End PercentOfNav   from FundPosition A 
                                left join Fund B on A.FundPK = B.FundPK and B.Status = 2
                                left join Instrument C on A.InstrumentPK = C.InstrumentPK and C.Status = 2
                                left join CloseNav D on D.Date = A.Date and A.FundPK = D.FundPK and D.status = 2  
                                where A.Date = @valuedate and A.status = 2 and InstrumentTypePK in (1,4,16) " + _paramFund + @"
                                group by B.ID,B.Name,D.AUM

                                union all
                                select B.ID,B.Name,0,0,isnull(sum(A.MarketValue),0) MarketValue,case when isnull(D.AUM,0) = 0 then 0 else isnull(sum((A.MarketValue / 
                                case when D.AUM = 0 then 1 else isnull(D.AUM,1) End) * 100),0) End PercentOfNav,0,0   from FundPosition A 
                                left join Fund B on A.FundPK = B.FundPK and B.Status = 2
                                left join Instrument C on A.InstrumentPK = C.InstrumentPK and C.Status = 2
                                left join CloseNav D on D.Date = A.Date and A.FundPK = D.FundPK and D.status = 2  
                                where A.Date = @valuedate and A.status = 2 and InstrumentTypePK not in (1,4,5,6,16) " + _paramFund + @"
                                group by B.ID,B.Name,D.AUM

                                union all
                                select B.ID,B.Name,isnull(sum(A.MarketValue),0) MarketValue,case when isnull(D.AUM,0) = 0 then 0 else isnull(sum((A.MarketValue / 
                                case when D.AUM = 0 then 1 else isnull(D.AUM,1) End) * 100),0) End PercentOfNav,0,0,0,0   from FundPosition A 
                                left join Fund B on A.FundPK = B.FundPK and B.Status = 2
                                left join Instrument C on A.InstrumentPK = C.InstrumentPK and C.Status = 2
                                left join CloseNav D on D.Date = A.Date and A.FundPK = D.FundPK and D.status = 2  
                                where A.Date = @valuedate and A.status = 2 and InstrumentTypePK in (5) " + _paramFund + @"
                                group by B.ID,B.Name,D.AUM

                                select @ValueDate Date,FundID,FundName,sum(DepositoAmount) DepositoAmount,sum(DEPPercentOfNav) DEPPercentOfNav,sum(BondAmount) BondAmount,sum(BondPercentOfNav) BondPercentOfNav,sum(EquityAmount) EquityAmount,sum(EQPercentOfNav) EQPercentOfNav,sum(DEPPercentOfNav + BondPercentOfNav + EQPercentOfNav ) TotalPercent  from #A
                                group By FundID,FundName
                                ";

                            cmd.CommandTimeout = 0;
                            cmd.Parameters.AddWithValue("@ValueDate", _FundAccountingRpt.ValueDateTo);

                            using (SqlDataReader dr0 = cmd.ExecuteReader())
                            {
                                if (!dr0.HasRows)
                                {
                                    return false;
                                }
                                else
                                {
                                    string filePath = Tools.ReportsPath + "DailyComplianceReport" + "_" + _userID + ".xlsx";
                                    string pdfPath = Tools.ReportsPath + "DailyComplianceReport" + "_" + _userID + ".pdf";
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

                                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Daily Transaction Report");


                                        //ATUR DATA GROUPINGNYA DULU
                                        List<DailyComplianceReport> rList = new List<DailyComplianceReport>();
                                        while (dr0.Read())
                                        {
                                            DailyComplianceReport rSingle = new DailyComplianceReport();
                                            rSingle.Date = Convert.ToString(dr0["Date"]);
                                            rSingle.FundID = Convert.ToString(dr0["FundID"]);
                                            rSingle.FundName = Convert.ToString(dr0["FundName"]);
                                            rSingle.DepositoAmount = Convert.ToDecimal(dr0["DepositoAmount"]);
                                            rSingle.DEPPercentOfNav = Convert.ToDecimal(dr0["DEPPercentOfNav"]);
                                            rSingle.BondAmount = Convert.ToDecimal(dr0["BondAmount"]);
                                            rSingle.BondPercentOfNav = Convert.ToDecimal(dr0["BondPercentOfNav"]);
                                            rSingle.EquityAmount = Convert.ToDecimal(dr0["EquityAmount"]);
                                            rSingle.EQPercentOfNav = Convert.ToDecimal(dr0["EQPercentOfNav"]);
                                            rSingle.TotalPercent = Convert.ToDecimal(dr0["TotalPercent"]);
                                            rList.Add(rSingle);

                                        }


                                        var QueryByFundID =
                                            from r in rList
                                            group r by new { r.Date } into rGroup
                                            select rGroup;

                                        int incRowExcel = 1;

                                        foreach (var rsHeader in QueryByFundID)
                                        {
                                            incRowExcel = incRowExcel + 2;
                                            worksheet.Cells[incRowExcel, 1].Value = "Daily Compliance Report";
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells["A" + incRowExcel + ":I" + incRowExcel].Merge = true;
                                            worksheet.Cells["A" + incRowExcel + ":I" + incRowExcel].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["A" + incRowExcel + ":I" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Value = "Date   :  ";
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 2].Value = _FundAccountingRpt.ValueDateTo;
                                            worksheet.Cells[incRowExcel, 2].Style.Numberformat.Format = "dd-MMM-yyyy";
                                            worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;

                                            incRowExcel = incRowExcel + 2;
                                            //Row B = 3
                                            //int rowA = incRowExcel;
                                            int RowB = incRowExcel;
                                            int RowG = incRowExcel + 1;

                                            worksheet.Cells[incRowExcel, 1].Value = "ID";
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells["A" + RowB + ":A" + RowG].Merge = true;
                                            worksheet.Cells["A" + RowB + ":A" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["A" + RowB + ":A" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 2].Value = "FUND NAME";
                                            worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                            worksheet.Cells["B" + RowB + ":B" + RowG].Merge = true;
                                            worksheet.Cells["B" + RowB + ":B" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["B" + RowB + ":B" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 3].Value = "DEPOSITO";
                                            worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;
                                            worksheet.Cells["C" + RowB + ":D" + RowB].Merge = true;
                                            worksheet.Cells["C" + RowB + ":D" + RowB].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["C" + RowB + ":D" + RowB].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 5].Value = "BOND";
                                            worksheet.Cells[incRowExcel, 5].Style.Font.Bold = true;
                                            worksheet.Cells["E" + RowB + ":F" + RowB].Merge = true;
                                            worksheet.Cells["E" + RowB + ":F" + RowB].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["E" + RowB + ":F" + RowB].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 7].Value = "EQUITY";
                                            worksheet.Cells[incRowExcel, 7].Style.Font.Bold = true;
                                            worksheet.Cells["G" + RowB + ":H" + RowB].Merge = true;
                                            worksheet.Cells["G" + RowB + ":H" + RowB].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["G" + RowB + ":H" + RowB].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 9].Value = "Total Investment (% NAV)";
                                            worksheet.Cells[incRowExcel, 9].Style.Font.Bold = true;
                                            worksheet.Cells["I" + RowB + ":I" + RowG].Merge = true;
                                            worksheet.Cells["I" + RowB + ":I" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["I" + RowB + ":I" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            incRowExcel++;

                                            worksheet.Cells[incRowExcel, 3].Value = "Amount";
                                            worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;
                                            //worksheet.Cells["C" + RowB + ":C" + RowG].Merge = true;
                                            worksheet.Cells["C" + RowB + ":C" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["C" + RowB + ":C" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 4].Value = "%NAV";
                                            worksheet.Cells[incRowExcel, 4].Style.Font.Bold = true;
                                            //worksheet.Cells["D" + RowB + ":D" + RowG].Merge = true;
                                            worksheet.Cells["D" + RowB + ":D" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["D" + RowB + ":D" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;


                                            worksheet.Cells[incRowExcel, 5].Value = "Amount";
                                            worksheet.Cells[incRowExcel, 5].Style.Font.Bold = true;
                                            //worksheet.Cells["E" + RowB + ":E" + RowG].Merge = true;
                                            worksheet.Cells["E" + RowB + ":E" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["E" + RowB + ":E" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 6].Value = "%NAV";
                                            worksheet.Cells[incRowExcel, 6].Style.Font.Bold = true;
                                            //worksheet.Cells["F" + RowB + ":F" + RowG].Merge = true;
                                            worksheet.Cells["F" + RowB + ":F" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["F" + RowB + ":F" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 7].Value = "Amount";
                                            worksheet.Cells[incRowExcel, 7].Style.Font.Bold = true;
                                            //worksheet.Cells["G" + RowB + ":G" + RowG].Merge = true;
                                            worksheet.Cells["G" + RowB + ":G" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["G" + RowB + ":G" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 8].Value = "%NAV";
                                            worksheet.Cells[incRowExcel, 8].Style.Font.Bold = true;
                                            //worksheet.Cells["H" + RowB + ":H" + RowG].Merge = true;
                                            worksheet.Cells["H" + RowB + ":H" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["H" + RowB + ":H" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;


                                            incRowExcel++;

                                            // Row C = 4
                                            int RowC = incRowExcel;

                                            //incRowExcel++;
                                            //area header

                                            int _no = 1;
                                            int _startRowDetail = incRowExcel;
                                            int _endRowDetail = 0;

                                            //var _fundID = "";
                                            foreach (var rsDetail in rsHeader)
                                            {
                                                //Row D = 5
                                                //int RowD = incRowExcel;
                                                //int RowE = incRowExcel + 1;

                                                worksheet.Cells["A" + RowB + ":I" + RowG].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + RowB + ":I" + RowG].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + RowB + ":I" + RowG].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + RowB + ":I" + RowG].Style.Border.Right.Style = ExcelBorderStyle.Medium;



                                                //area detail
                                                worksheet.Cells[incRowExcel, 1].Value = rsDetail.FundID;
                                                worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                worksheet.Cells[incRowExcel, 2].Value = rsDetail.FundName;
                                                worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.DepositoAmount;
                                                worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                worksheet.Cells[incRowExcel, 4].Value = rsDetail.DEPPercentOfNav;
                                                worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##0.0000";
                                                worksheet.Cells[incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                worksheet.Cells[incRowExcel, 5].Value = rsDetail.BondAmount;
                                                worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                worksheet.Cells[incRowExcel, 6].Value = rsDetail.BondPercentOfNav;
                                                worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.0000";
                                                worksheet.Cells[incRowExcel, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                worksheet.Cells[incRowExcel, 7].Value = rsDetail.EquityAmount;
                                                worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                worksheet.Cells[incRowExcel, 8].Value = rsDetail.EQPercentOfNav;
                                                worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0.0000";
                                                worksheet.Cells[incRowExcel, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0.0000";
                                                worksheet.Cells[incRowExcel, 9].Value = rsDetail.TotalPercent;
                                                worksheet.Cells[incRowExcel, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                                                _endRowDetail = incRowExcel;
                                                _no++;
                                                incRowExcel++;
                                                //_fundID = rsDetail.Fund;
                                            }

                                            worksheet.Cells["A" + _endRowDetail + ":I" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + _startRowDetail + ":I" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + _startRowDetail + ":I" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Medium;

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
                                           	Create Table #Exposure
            (
            InstrumentTypePK int,
            InstrumentPK int,
            FundPK int,
            Amount numeric(22,2),
            NAVPercent numeric(18,4),
			ExposureType nvarchar(100)
            )

            Declare @InstrumentTypePK int
            Declare @InstrumentPK int
            Declare @FundPK int
            Declare @Amount numeric(22,2)
            Declare @NAVPercent numeric(18,4)
            Declare @WarningMaxExposurePercent numeric(18,4)

            SET ANSI_WARNINGS OFF

            DECLARE A CURSOR FOR 
            select C.InstrumentTypePK,A.InstrumentPK,A.FundPK,isnull(sum(A.MarketValue),0),case when isnull(D.AUM,0) = 0 then 0 else isnull(sum((A.MarketValue / 
            case when D.AUM = 0 then 1 else isnull(D.AUM,1) End) * 100),0) End NAVPercent   from FundPosition A 
            left join Instrument C on A.InstrumentPK = C.InstrumentPK and C.Status = 2
            left join CloseNav D on D.Date = A.Date and A.FundPK = D.FundPK and D.status = 2 
            left join InstrumentType E on C.InstrumentTypePK = E.InstrumentTypePK and E.Status = 2 
            where A.Date = @ValueDate and A.status = 2 
			--" + _paramFund + @"
            group by C.InstrumentTypePK,A.InstrumentPK,A.FundPK,D.AUM
            having case when isnull(D.AUM,0) = 0 then 0 else isnull(sum((A.MarketValue / 
            case when D.AUM = 0 then 1 else isnull(D.AUM,1) End) * 100),0) End > 0
        	
            Open A
            Fetch Next From A
            Into @InstrumentTypePK,@InstrumentPK,@FundPK,@Amount,@NAVPercent
        
            While @@FETCH_STATUS = 0
            BEGIN
            set @WarningMaxExposurePercent = 0

            IF (@InstrumentTypePK in (1,4,16))
            BEGIN
	            IF EXISTS(select * from FundExposure where FundPK = @FundPK and Type = 5 and status = 2 and Parameter = 0)
	            BEGIN
		            select @WarningMaxExposurePercent = isnull(WarningMaxExposurePercent,0) from FundExposure where FundPK = @FundPK and Type = 5 and status = 2 and Parameter = 0
		            IF (@NAVPercent >= @WarningMaxExposurePercent)
		            BEGIN
			            insert into #Exposure(InstrumentTypePK,InstrumentPK,FundPK,Amount,NAVPercent,ExposureType)
			            select @InstrumentTypePK,@InstrumentPK,@FundPK,@Amount,@NAVPercent,'EQUITY ALL'
		            END
	            END

            END
            ELSE IF (@InstrumentTypePK = 5)
            BEGIN  
	            IF EXISTS(select * from FundExposure where FundPK = @FundPK and Type = 10 and status = 2 and Parameter = 0)
	            BEGIN
		            select @WarningMaxExposurePercent = isnull(WarningMaxExposurePercent,0) from FundExposure where FundPK = @FundPK and Type = 10 and status = 2  and Parameter = 0
		            IF (@NAVPercent >= @WarningMaxExposurePercent)
		            BEGIN
			            insert into #Exposure(InstrumentTypePK,InstrumentPK,FundPK,Amount,NAVPercent,ExposureType)
			            select @InstrumentTypePK,@InstrumentPK,@FundPK,@Amount,@NAVPercent,'BOND ALL'
		            END
	            END
            END
            ELSE
            BEGIN  
	            IF EXISTS(select * from FundExposure where FundPK = @FundPK and Type = 13 and status = 2 and Parameter = 0)
	            BEGIN
		            select @WarningMaxExposurePercent = isnull(WarningMaxExposurePercent,0) from FundExposure where FundPK = @FundPK and Type = 13 and status = 2  and Parameter = 0
		            IF (@NAVPercent >= @WarningMaxExposurePercent)
		            BEGIN
			            insert into #Exposure(InstrumentTypePK,InstrumentPK,FundPK,Amount,NAVPercent,ExposureType)
			            select @InstrumentTypePK,@InstrumentPK,@FundPK,@Amount,@NAVPercent,'DEPOSITO ALL'
		            END
	            END
            END
            Fetch next From A Into @InstrumentTypePK,@InstrumentPK,@FundPK,@Amount,@NAVPercent
            END
            Close A
            Deallocate A 
			

			DECLARE A CURSOR FOR 
          		select 0,A.IssuerPK,A.FundPK,sum(isnull(A.MarketValue,0)),sum(isnull(A.NAVPercent,0)) from
				(
				select C.IssuerPK,A.FundPK,isnull(sum(A.MarketValue),0) MarketValue,case when isnull(D.AUM,0) = 0 then 0 else isnull(sum((A.MarketValue / 
				case when D.AUM = 0 then 1 else isnull(D.AUM,1) End) * 100),0) End NAVPercent   from FundPosition A 
				left join Instrument C on A.InstrumentPK = C.InstrumentPK and C.Status = 2
				left join CloseNav D on D.Date = A.Date and A.FundPK = D.FundPK and D.status = 2 
				left join InstrumentType E on C.InstrumentTypePK = E.InstrumentTypePK and E.Status = 2 
				where A.Date = @ValueDate and A.status = 2 and C.InstrumentTypePK <> 5 and isnull(C.IssuerPK,0) <> 0
				--" + _paramFund + @"
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
				where A.Date = @ValueDate and A.status = 2 and C.InstrumentTypePK = 5 and isnull(H.IssuerPK,0) <> 0
				--" + _paramFund + @"
				group by H.IssuerPK,A.InstrumentPK,A.FundPK,D.AUM
				having case when isnull(D.AUM,0) = 0 then 0 else isnull(sum((A.MarketValue / 
				case when D.AUM = 0 then 1 else isnull(D.AUM,1) End) * 100),0) End > 0
				) A 
				group by A.IssuerPK,A.FundPK

        	
            Open A
            Fetch Next From A
            Into @InstrumentTypePK,@InstrumentPK,@FundPK,@Amount,@NAVPercent
        
            While @@FETCH_STATUS = 0
            BEGIN
            set @WarningMaxExposurePercent = 0
			IF EXISTS(select * from FundExposure where FundPK = @FundPK and Type = 2 and status = 2 and Parameter = 0)
	            BEGIN
		            select @WarningMaxExposurePercent = isnull(WarningMaxExposurePercent,0) from FundExposure where FundPK = @FundPK and Type = 2 and status = 2 and Parameter = 0
		            IF (@NAVPercent >= @WarningMaxExposurePercent)
		            BEGIN
			            insert into #Exposure(InstrumentTypePK,InstrumentPK,FundPK,Amount,NAVPercent, ExposureType)
			            select @InstrumentTypePK,@InstrumentPK,@FundPK,@Amount,@NAVPercent, 'ISSUER ALL'
		            END
	            END

			  Fetch next From A Into @InstrumentTypePK,@InstrumentPK,@FundPK,@Amount,@NAVPercent
            END
            Close A
            Deallocate A 



            select @ValueDate Date,E.Name Type,C.ID InstrumentID,B.ID FundID,B.Name FundName,Amount,NAVPercent   from #Exposure A 
            left join Fund B on A.FundPK = B.FundPK and B.Status in (1,2)
            left join Instrument C on A.InstrumentPK = C.InstrumentPK and C.Status in (1,2)
            left join InstrumentType E on C.InstrumentTypePK = E.InstrumentTypePK and E.Status in (1,2)
			where A.ExposureType in
			(
			'DEPOSITO ALL','EQUITY ALL','BOND ALL'
			)

			UNION ALL

			 select @ValueDate Date,'ISSUER ALL' Type,C.Name InstrumentID,B.ID FundID,B.Name FundName,Amount,NAVPercent   from #Exposure A 
            left join Fund B on A.FundPK = B.FundPK and B.Status in (1,2)
            left join Issuer C on A.InstrumentPK = C.IssuerPK and C.Status in (1,2) 
			where A.ExposureType in
			(
			'ISSUER ALL'
			)
		
			

                                                 ";

                                                cmd1.CommandTimeout = 0;
                                                cmd1.Parameters.AddWithValue("@valuedate", _FundAccountingRpt.ValueDateTo);
                                                //cmd1.Parameters.AddWithValue("@FundFrom", _FundAccountingRpt.FundFrom);



                                                cmd1.ExecuteNonQuery();


                                                using (SqlDataReader dr1 = cmd1.ExecuteReader())
                                                {
                                                    //if (!dr1.HasRows)
                                                    //{
                                                    //    return false;
                                                    //}
                                                    //else
                                                    //{


                                                    // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                                    using (ExcelPackage package1 = new ExcelPackage(excelFile))
                                                    {

                                                        //ATUR DATA GROUPINGNYA DULU
                                                        List<DailyComplianceReport> rList1 = new List<DailyComplianceReport>();
                                                        while (dr1.Read())
                                                        {
                                                            DailyComplianceReport rSingle1 = new DailyComplianceReport();
                                                            rSingle1.Date = Convert.ToString(dr1["Date"]);
                                                            rSingle1.FundID = Convert.ToString(dr1["FundID"]);
                                                            rSingle1.FundName = Convert.ToString(dr1["FundName"]);
                                                            rSingle1.Amount = Convert.ToDecimal(dr1["Amount"]);
                                                            rSingle1.NAVPercent = Convert.ToDecimal(dr1["NAVPercent"]);
                                                            rSingle1.Type = Convert.ToString(dr1["Type"]);
                                                            rSingle1.InstrumentID = Convert.ToString(dr1["InstrumentID"]);
                                                            rList1.Add(rSingle1);

                                                        }


                                                        var QueryByFundID1 =
                                                            from r1 in rList1
                                                            group r1 by new { r1.Date } into rGroup1
                                                            select rGroup1;

                                                        incRowExcel = incRowExcel + 2;
                                                        int _endRowDetailZ = 0;


                                                        foreach (var rsHeader1 in QueryByFundID1)
                                                        {
                                                            //worksheet.Cells[incRowExcel, 1].Value = "Total " + rsHeader1.Key.FundID + " : ";
                                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                                            worksheet.Cells["A" + incRowExcel + ":B" + incRowExcel].Merge = true;
                                                            worksheet.Cells["A" + incRowExcel + ":B" + incRowExcel].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                            worksheet.Cells["A" + incRowExcel + ":B" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                            incRowExcel = incRowExcel + 2;
                                                            //Row B = 3
                                                            int RowBZ = incRowExcel;
                                                            int RowGZ = incRowExcel + 1;

                                                            worksheet.Cells[incRowExcel, 1].Value = "ID";
                                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                                            worksheet.Cells["A" + RowBZ + ":A" + RowGZ].Merge = true;
                                                            worksheet.Cells["A" + RowBZ + ":A" + RowGZ].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                            worksheet.Cells["A" + RowBZ + ":A" + RowGZ].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                            worksheet.Cells[incRowExcel, 2].Value = "Fund";
                                                            worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                                            worksheet.Cells["B" + RowBZ + ":B" + RowGZ].Merge = true;
                                                            worksheet.Cells["B" + RowBZ + ":B" + RowGZ].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                            worksheet.Cells["B" + RowBZ + ":B" + RowGZ].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                            worksheet.Cells[incRowExcel, 3].Value = "Exposure Monitoring";
                                                            worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;
                                                            worksheet.Cells["C" + RowBZ + ":E" + RowBZ].Merge = true;
                                                            worksheet.Cells["C" + RowBZ + ":E" + RowBZ].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                            worksheet.Cells["C" + RowBZ + ":E" + RowBZ].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                            worksheet.Cells[incRowExcel, 6].Value = "%NAV";
                                                            worksheet.Cells[incRowExcel, 6].Style.Font.Bold = true;
                                                            worksheet.Cells["F" + RowBZ + ":F" + RowGZ].Merge = true;
                                                            worksheet.Cells["F" + RowBZ + ":F" + RowGZ].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                            worksheet.Cells["F" + RowBZ + ":F" + RowGZ].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                            incRowExcel++;


                                                            worksheet.Cells[incRowExcel, 3].Value = "Type";
                                                            worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;
                                                            //worksheet.Cells["C" + RowBZ + ":C" + RowGZ].Merge = true;
                                                            worksheet.Cells["C" + RowBZ + ":C" + RowGZ].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                            worksheet.Cells["C" + RowBZ + ":C" + RowGZ].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                            worksheet.Cells[incRowExcel, 4].Value = "Instrument";
                                                            worksheet.Cells[incRowExcel, 4].Style.Font.Bold = true;
                                                            //worksheet.Cells["D" + RowBZ + ":D" + RowGZ].Merge = true;
                                                            worksheet.Cells["D" + RowBZ + ":D" + RowGZ].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                            worksheet.Cells["D" + RowBZ + ":D" + RowGZ].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                            worksheet.Cells[incRowExcel, 5].Value = "Amount";
                                                            worksheet.Cells[incRowExcel, 5].Style.Font.Bold = true;
                                                            //worksheet.Cells["E" + RowBZ + ":E" + RowGZ].Merge = true;
                                                            worksheet.Cells["E" + RowBZ + ":E" + RowGZ].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                            worksheet.Cells["E" + RowBZ + ":E" + RowGZ].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;


                                                            incRowExcel++;

                                                            // Row C = 4
                                                            int RowCZ = incRowExcel;

                                                            //incRowExcel++;
                                                            //area header

                                                            int _noZ = 1;
                                                            int _startRowDetailZ = incRowExcel;
                                                            foreach (var rsDetail1 in rsHeader1)
                                                            {
                                                                //Row D = 5
                                                                int RowDZ = incRowExcel;
                                                                int RowEZ = incRowExcel + 1;


                                                                //ThickBox Border

                                                                worksheet.Cells["A" + RowBZ + ":F" + RowGZ].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                                worksheet.Cells["A" + RowBZ + ":F" + RowGZ].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                                worksheet.Cells["A" + RowBZ + ":F" + RowGZ].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                                worksheet.Cells["A" + RowBZ + ":F" + RowGZ].Style.Border.Right.Style = ExcelBorderStyle.Medium;


                                                                //area detail
                                                                worksheet.Cells[incRowExcel, 1].Value = rsDetail1.FundID;
                                                                worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                                worksheet.Cells[incRowExcel, 2].Value = rsDetail1.FundName;
                                                                worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail1.Type;
                                                                worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                                worksheet.Cells[incRowExcel, 4].Value = rsDetail1.InstrumentID;
                                                                worksheet.Cells[incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                                worksheet.Cells[incRowExcel, 5].Value = rsDetail1.Amount;
                                                                worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.00";
                                                                worksheet.Cells[incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                                worksheet.Cells[incRowExcel, 6].Value = rsDetail1.NAVPercent;
                                                                worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.0000";
                                                                worksheet.Cells[incRowExcel, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;



                                                                _endRowDetailZ = incRowExcel;
                                                                _noZ++;
                                                                incRowExcel++;

                                                            }

                                                            worksheet.Cells["A" + _endRowDetailZ + ":F" + _endRowDetailZ].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                            worksheet.Cells["A" + _startRowDetailZ + ":F" + _endRowDetailZ].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                            worksheet.Cells["A" + _startRowDetailZ + ":F" + _endRowDetailZ].Style.Border.Right.Style = ExcelBorderStyle.Medium;

                                                            //incRowExcel++;
                                                        }
                                                        //disini
                                                        incRowExcel++;

                                                        //-----------------------------------
                                                        using (SqlConnection DbCon2 = new SqlConnection(Tools.conString))
                                                        {
                                                            DbCon2.Open();
                                                            using (SqlCommand cmd2 = DbCon2.CreateCommand())
                                                            {
                                                                cmd2.CommandText =

                                                                @"
                                                                    select @ValueDate Date,B.ID FundID,B.Name FundName,isnull(A.AUM,0) TotalAUM from CloseNAV A
                                                                    left join Fund B on A.FundPK = B.FundPK and B.status in (1,2)
                                                                    where A.date = @ValueDate and A.status = 2 " + _paramFund;

                                                                cmd2.CommandTimeout = 0;
                                                                cmd2.Parameters.AddWithValue("@ValueDate", _FundAccountingRpt.ValueDateTo);

                                                                cmd2.ExecuteNonQuery();


                                                                using (SqlDataReader dr2 = cmd2.ExecuteReader())
                                                                {
                                                                    //if (!dr2.HasRows)
                                                                    //{
                                                                    //    return false;
                                                                    //}
                                                                    //else
                                                                    //{


                                                                    // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                                                    using (ExcelPackage package2 = new ExcelPackage(excelFile))
                                                                    {

                                                                        //ATUR DATA GROUPINGNYA DULU
                                                                        List<DailyComplianceReport> rList2 = new List<DailyComplianceReport>();
                                                                        while (dr2.Read())
                                                                        {
                                                                            DailyComplianceReport rSingle2 = new DailyComplianceReport();
                                                                            rSingle2.Date = Convert.ToString(dr2["Date"]);
                                                                            rSingle2.FundID = Convert.ToString(dr2["FundID"]);
                                                                            rSingle2.FundName = Convert.ToString(dr2["FundName"]);
                                                                            rSingle2.TotalAUM = Convert.ToDecimal(dr2["TotalAUM"]);
                                                                            rList2.Add(rSingle2);

                                                                        }


                                                                        var QueryByFundID2 =
                                                                            from r2 in rList2
                                                                            group r2 by new { r2.Date } into rGroup2
                                                                            select rGroup2;

                                                                        incRowExcel = incRowExcel + 2;
                                                                        int _endRowDetailZZ = 0;


                                                                        foreach (var rsHeader2 in QueryByFundID2)
                                                                        {
                                                                            //worksheet.Cells[incRowExcel, 1].Value = "Total " + rsHeader1.Key.FundID + " : ";
                                                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                                                            worksheet.Cells["A" + incRowExcel + ":C" + incRowExcel].Merge = true;
                                                                            worksheet.Cells["A" + incRowExcel + ":C" + incRowExcel].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                                            worksheet.Cells["A" + incRowExcel + ":C" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                                            incRowExcel = incRowExcel + 2;
                                                                            //Row B = 3
                                                                            int RowBZ = incRowExcel;
                                                                            int RowGZ = incRowExcel + 1;

                                                                            worksheet.Cells[incRowExcel, 1].Value = "AUM monitoring";
                                                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                                                            worksheet.Cells["A" + RowBZ + ":C" + RowBZ].Merge = true;
                                                                            worksheet.Cells["A" + RowBZ + ":C" + RowBZ].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                                            worksheet.Cells["A" + RowBZ + ":C" + RowBZ].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                                            incRowExcel++;

                                                                            worksheet.Cells[incRowExcel, 1].Value = "ID";
                                                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                                                            //worksheet.Cells["A" + RowBZ + ":A" + RowGZ].Merge = true;
                                                                            worksheet.Cells["A" + RowBZ + ":A" + RowGZ].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                                            worksheet.Cells["A" + RowBZ + ":A" + RowGZ].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                                            worksheet.Cells[incRowExcel, 2].Value = "Fund";
                                                                            worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                                                            //worksheet.Cells["B" + RowBZ + ":B" + RowGZ].Merge = true;
                                                                            worksheet.Cells["B" + RowBZ + ":B" + RowGZ].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                                            worksheet.Cells["B" + RowBZ + ":B" + RowGZ].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                                            worksheet.Cells[incRowExcel, 3].Value = "Total AUM";
                                                                            worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;
                                                                            //worksheet.Cells["C" + RowBZ + ":C" + RowGZ].Merge = true;
                                                                            worksheet.Cells["C" + RowBZ + ":C" + RowGZ].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                                            worksheet.Cells["C" + RowBZ + ":C" + RowGZ].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;






                                                                            incRowExcel++;

                                                                            // Row C = 4
                                                                            int RowCZ = incRowExcel;

                                                                            //incRowExcel++;
                                                                            //area header

                                                                            int _noZ = 1;
                                                                            int _startRowDetailZ = incRowExcel;
                                                                            foreach (var rsDetail2 in rsHeader2)
                                                                            {
                                                                                //Row D = 5
                                                                                int RowDZ = incRowExcel;
                                                                                int RowEZ = incRowExcel + 1;


                                                                                //ThickBox Border

                                                                                //if (rsDetail1.Type == "Subscription")
                                                                                //{
                                                                                worksheet.Cells["A" + RowBZ + ":C" + RowGZ].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                                                worksheet.Cells["A" + RowBZ + ":C" + RowGZ].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                                                worksheet.Cells["A" + RowBZ + ":C" + RowGZ].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                                                worksheet.Cells["A" + RowBZ + ":C" + RowGZ].Style.Border.Right.Style = ExcelBorderStyle.Medium;

                                                                                //area detail
                                                                                worksheet.Cells[incRowExcel, 1].Value = rsDetail2.FundID;
                                                                                worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                                                worksheet.Cells[incRowExcel, 2].Value = rsDetail2.FundName;
                                                                                worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail2.TotalAUM;
                                                                                worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "#,##0.00";
                                                                                worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;




                                                                                _endRowDetailZZ = incRowExcel;
                                                                                _noZ++;
                                                                                incRowExcel++;

                                                                            }

                                                                            worksheet.Cells["A" + _endRowDetailZZ + ":C" + _endRowDetailZZ].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                                            worksheet.Cells["A" + _startRowDetailZ + ":C" + _endRowDetailZZ].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                                            worksheet.Cells["A" + _startRowDetailZ + ":C" + _endRowDetailZZ].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                                                            incRowExcel++;
                                                                        }





                                                                        //string _rangeA1 = "A:M" + incRowExcel;
                                                                        //using (ExcelRange r = worksheet.Cells[_rangeA1]) // KALO  KOLOM 1 SAMPE 9 A-I
                                                                        //{
                                                                        //    r.Style.Font.Size = 22;
                                                                        //}
                                                                        //}

                                                                    }
                                                                }
                                                            }
                                                        }



                                                        //string _rangeA1 = "A:M" + incRowExcel;
                                                        //using (ExcelRange r = worksheet.Cells[_rangeA1]) // KALO  KOLOM 1 SAMPE 9 A-I
                                                        //{
                                                        //    r.Style.Font.Size = 22;
                                                        //}
                                                    }

                                                    //}
                                                }
                                            }
                                        }



                                        int _lastRow = incRowExcel;

                                        //incRowExcel = incRowExcel + 7;
                                        //worksheet.Cells[incRowExcel, 1].Value = "Disclaimer   : ";
                                        //incRowExcel++;
                                        //worksheet.Row(incRowExcel).Height = 50;
                                        //worksheet.Cells[incRowExcel, 1].Value = Tools.DefaultDisclaimerReportFooterLeftText();
                                        //worksheet.Cells["A" + incRowExcel + ":I" + incRowExcel].Style.WrapText = true;
                                        //worksheet.Cells["A" + incRowExcel + ":I" + incRowExcel].Merge = true;
                                        //worksheet.Cells["A" + incRowExcel + ":I" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                        //worksheet.Cells[incRowExcel, 1].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                                        incRowExcel++;
                                        worksheet.Row(incRowExcel).PageBreak = _FundAccountingRpt.PageBreak;

                                        string _rangeA = "A:I" + incRowExcel;
                                        using (ExcelRange r = worksheet.Cells[_rangeA]) // KALO  KOLOM 1 SAMPE 9 A-I
                                        {
                                            r.Style.Font.Size = 12;
                                        }

                                        //worksheet.DeleteRow(_lastRow);

                                        worksheet.PrinterSettings.FitToPage = true;
                                        worksheet.PrinterSettings.FitToWidth = 1;
                                        worksheet.PrinterSettings.FitToHeight = 0;
                                        worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel - 1, 9];
                                        worksheet.Column(1).Width = 15;
                                        worksheet.Column(2).Width = 50;
                                        worksheet.Column(3).Width = 30;
                                        worksheet.Column(4).Width = 20;
                                        worksheet.Column(5).Width = 30;
                                        worksheet.Column(6).Width = 20;
                                        worksheet.Column(7).Width = 30;
                                        worksheet.Column(8).Width = 20;
                                        worksheet.Column(9).Width = 25;
                                        //worksheet.Column(10).Width = 20;
                                        //worksheet.Column(11).Width = 20;
                                        //worksheet.Column(12).Width = 20;
                                        //worksheet.Column(13).Width = 15;
                                        //worksheet.Column(14).Width = 15;
                                        //worksheet.Column(15).Width = 30;



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

            #region Broker Commission Summary
            else if (_FundAccountingRpt.ReportName.Equals("Broker Commission Summary"))
            {
                try
                {
                    using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                    {
                        DbCon.Open();
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {

                            string _paramCounterpart = "";

                            if (!_host.findString(_FundAccountingRpt.CounterpartFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_FundAccountingRpt.CounterpartFrom))
                            {
                                _paramCounterpart = " And A.CounterpartPK  in ( " + _FundAccountingRpt.CounterpartFrom + " ) ";
                            }
                            else
                            {
                                _paramCounterpart = "";
                            }


                            cmd.CommandText =

                            @"
                                    DECLARE @CCounterpartPK int

                                    DECLARE @CP TABLE
                                    (
	                                    CounterpartPK INT,
	                                    Fee NUMERIC(18,8),
	                                    NetFee NUMERIC(18,8)
                                    )

                                    Declare A Cursor For
	                                    SELECT DISTINCT A.CounterpartPK FROM dbo.CounterpartCommission A WHERE A.status = 2
	                                      " + _paramCounterpart + @"
                                    Open A
                                    Fetch Next From A
                                    INTO @CCounterpartPK
                                    While @@FETCH_STATUS = 0  
                                    Begin
	                                    INSERT INTO @CP
	                                            ( CounterpartPK ,
			                                        NetFee ,
				                                     Fee
	                                            )
	                                    SELECT CounterpartPK,CommissionPercent,ISNULL(CommissionPercent,0) 
	                                    + ISNULL(LevyPercent,0) + ISNULL(KPEIPercent,0) + ISNULL(VATPercent,0) 
	                                    FROM dbo.CounterpartCommission WHERE CounterpartPK = @CCounterpartPK
	                                    AND Date = 
	                                    (
		                                    SELECT MAX(date) FROM dbo.CounterpartCommission WHERE CounterpartPK = @CCounterpartPK AND
                                            Date  <= @DateTo AND status = 2 	AND BoardType = 1
	                                    ) AND status = 2 AND BoardType = 1
	                                    Fetch Next From A 
	                                    into @CCounterpartPK
                                    End	
                                    Close A
                                    Deallocate A

                                    DECLARE @AumTotal NUMERIC(26,4)

                                    SET @AumTotal = 0

                                    SELECT @AumTotal = SUM(ISNULL(A.DoneAmount,0)) FROM dbo.Investment A
                                    LEFT JOIN dbo.Instrument B ON A.InstrumentPK = B.InstrumentPK AND B.Status IN (1,2)
                                    WHERE ValueDate BETWEEN @DateFrom AND @DateTo AND StatusSettlement = 2
                                    AND  B.InstrumentTypePK IN 
                                    (
                                    1,4,16
                                    )


                                    SELECT 
                                    ISNULL(C.ID,'') BrokerID
                                    ,ISNULL(C.Name,'') BrokerName
                                    ,ISNULL(D.Fee,0) Fee
                                    ,ISNULL(D.NetFee,0) NetFee
                                    ,SUM(ISNULL(A.DoneAmount,0)) TotalDoneAmount
                                    ,CASE WHEN @AumTotal > 0 THEN SUM(ISNULL(A.DoneAmount,0)) / @AumTotal ELSE 0 END PercentTrading
                                    ,SUM(ISNULL(A.CommissionAmount,0)) + SUM(ISNULL(A.LevyAmount,0)) + SUM(ISNULL(A.VATAmount,0)) + SUM(ISNULL(A.KPEIAmount,0)) Commission
                                    ,SUM(ISNULL(A.CommissionAmount,0)) NetCommission
                                    ,CASE WHEN @AumTotal > 0 THEN SUM(ISNULL(A.CommissionAmount,0)) / @AumTotal ELSE 0 END PercentNetCommission
                                    FROM dbo.Investment A
                                    LEFT JOIN dbo.Instrument B ON A.InstrumentPK = B.InstrumentPK AND B.status IN (1,2)
                                    LEFT JOIN dbo.Counterpart C ON A.CounterpartPK = C.CounterpartPK AND C.status IN (1,2)
                                    LEFT JOIN @CP D ON A.CounterpartPK = D.CounterpartPK

                                    WHERE StatusSettlement = 2 AND B.InstrumentTypePK IN 
                                    (
                                    1,4,16
                                    )AND A.ValueDate BETWEEN @DateFrom AND @DateTo  " + _paramCounterpart + @"

                                    GROUP BY ISNULL(C.ID,''),ISNULL(C.Name,''),ISNULL(D.Fee,0) ,ISNULL(D.NetFee,0)
                             ";


                            cmd.CommandTimeout = 0;
                            cmd.Parameters.AddWithValue("@DateFrom", _FundAccountingRpt.ValueDateFrom);
                            cmd.Parameters.AddWithValue("@DateTo", _FundAccountingRpt.ValueDateTo);

                            using (SqlDataReader dr0 = cmd.ExecuteReader())
                            {
                                if (!dr0.HasRows)
                                {
                                    return false;
                                }
                                else
                                {
                                    string filePath = Tools.ReportsPath + "BrokerCommissionSummary" + "_" + _userID + ".xlsx";
                                    string pdfPath = Tools.ReportsPath + "BrokerCommissionSummary" + "_" + _userID + ".pdf";
                                    FileInfo excelFile = new FileInfo(filePath);
                                    if (excelFile.Exists)
                                    {
                                        excelFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                        excelFile = new FileInfo(filePath);
                                    }

                                    // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                    using (ExcelPackage package = new ExcelPackage(excelFile))
                                    {
                                        package.Workbook.Properties.Title = "BrokerCommisionSummary";
                                        package.Workbook.Properties.Author = Tools.DefaultReportAuthor();
                                        package.Workbook.Properties.Comments = Tools.DefaultReportComments();
                                        package.Workbook.Properties.Company = Tools.DefaultReportCompany();
                                        package.Workbook.Properties.SetCustomPropertyValue("Checked by", _userID);
                                        package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", Tools.DefaultReportAssemblyName());

                                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("BrokerCommisionSummary");

                                        int incRowExcel = 1;

                                        //worksheet.Cells[incRowExcel, 1].Value = "Periode: " + Convert.ToDateTime(_FundAccountingRpt.ValueDateTo).ToShortDateString(); // + " s/d " + Convert.ToDateTime(_unitRegistryRpt.ValueDateTo).ToShortDateString();                                                                 
                                        //worksheet.Cells[incRowExcel, 1].Style.Font.UnderLine = true;
                                        //worksheet.Cells[incRowExcel, 1, incRowExcel, 4].Merge = true;
                                        //worksheet.Cells[incRowExcel, 1, incRowExcel, 4].Style.Font.Size = 12;
                                        //worksheet.Cells[incRowExcel, 1, incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        //worksheet.Cells[incRowExcel, 1, incRowExcel, 4].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                        //incRowExcel = incRowExcel + 2;


                                        //ATUR DATA GROUPINGNYA DULU
                                        List<BrokerCommisionSummary> rList = new List<BrokerCommisionSummary>();
                                        while (dr0.Read())
                                        {
                                            BrokerCommisionSummary rSingle = new BrokerCommisionSummary();
                                            rSingle.BrokerID = Convert.ToString(dr0["BrokerID"]);
                                            rSingle.BrokerName = Convert.ToString(dr0["BrokerName"]);
                                            rSingle.TotalTrading = Convert.ToDecimal(dr0["TotalDoneAmount"]);
                                            rSingle.Trading = Convert.ToDecimal(dr0["PercentTrading"]);
                                            rSingle.Commission = Convert.ToDecimal(dr0["Commission"]);
                                            rSingle.NetCommission = Convert.ToDecimal(dr0["NetCommission"]);
                                            //rSingle.Date = Convert.ToString(dr0["Date"]);
                                            rList.Add(rSingle);

                                        }

                                        var QueryByFundID =
                                                     from r in rList
                                                     group r by new { } into rGroup
                                                     select rGroup;


                                        foreach (var rsHeader in QueryByFundID)
                                        {

                                            //worksheet.Cells[incRowExcel, 1].Value = "Bersama ini kami sampaikan data NAB dan dana kelolaan. ";
                                            //incRowExcel++;
                                            worksheet.Cells[incRowExcel, 2].Value = "Broker Commission Summary";
                                            worksheet.Cells["B" + incRowExcel + ":C" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 2].Value = "PT. Indosterling Aset Manajemen";
                                            worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                            worksheet.Cells["B" + incRowExcel + ":C" + incRowExcel].Merge = true;
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 2].Value = "Date From";
                                            worksheet.Cells[incRowExcel, 3].Value = Convert.ToDateTime(_FundAccountingRpt.ValueDateFrom).ToString("dd/MMM/yyyy");
                                            worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "dd/MMM/yyyy";
                                            worksheet.Cells[incRowExcel, 3, incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 2].Value = "Date To";
                                            worksheet.Cells[incRowExcel, 3].Value = Convert.ToDateTime(_FundAccountingRpt.ValueDateTo).ToString("dd/MMM/yyyy");
                                            worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "dd/MMM/yyyy";
                                            worksheet.Cells[incRowExcel, 3, incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            incRowExcel = incRowExcel + 3;
                                            worksheet.Cells[incRowExcel, 2, incRowExcel, 7].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells[incRowExcel, 2, incRowExcel, 7].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells[incRowExcel, 2, incRowExcel, 7].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells[incRowExcel, 2, incRowExcel, 7].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                                            worksheet.Cells[incRowExcel, 2].Value = "Broker ID";
                                            worksheet.Cells[incRowExcel, 3].Value = "Broker Name";
                                            worksheet.Cells[incRowExcel, 4].Value = "Total Trading"; //5
                                            worksheet.Cells[incRowExcel, 5].Value = "% Trading";//6
                                            worksheet.Cells[incRowExcel, 6].Value = "Commission"; //7
                                            worksheet.Cells[incRowExcel, 7].Value = "Net Commission";//8


                                            worksheet.Cells[incRowExcel, 2, incRowExcel, 7].Style.Font.Size = 11;
                                            worksheet.Cells[incRowExcel, 2, incRowExcel, 7].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 2, incRowExcel, 7].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                            worksheet.Cells[incRowExcel, 2, incRowExcel, 7].Style.Fill.BackgroundColor.SetColor(Color.White);
                                            worksheet.Cells[incRowExcel, 2, incRowExcel, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 2, incRowExcel, 7].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 2, incRowExcel, 7].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells[incRowExcel, 2, incRowExcel, 7].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells["B" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells["C" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells["D" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells["E" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells["F" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells["G" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                                            incRowExcel = incRowExcel + 1;

                                            int first = incRowExcel;

                                            int no = 1;

                                            foreach (var rsDetail in rsHeader)
                                            {
                                                worksheet.Cells[incRowExcel, 2, incRowExcel, 7].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells[incRowExcel, 2, incRowExcel, 7].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                                                worksheet.Cells[incRowExcel, 2].Value = rsDetail.BrokerID;
                                                worksheet.Cells[incRowExcel, 2].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells[incRowExcel, 2, incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 2, incRowExcel, 2].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.BrokerName;
                                                worksheet.Cells[incRowExcel, 3].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells[incRowExcel, 3, incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                worksheet.Cells[incRowExcel, 3, incRowExcel, 3].Style.VerticalAlignment = ExcelVerticalAlignment.Center;


                                                worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 4].Value = rsDetail.TotalTrading;
                                                worksheet.Cells[incRowExcel, 4, incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                worksheet.Cells[incRowExcel, 4, incRowExcel, 4].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.00%";
                                                worksheet.Cells[incRowExcel, 5].Value = rsDetail.Trading;
                                                worksheet.Cells[incRowExcel, 5].Style.WrapText = true;
                                                worksheet.Cells[incRowExcel, 5, incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                worksheet.Cells[incRowExcel, 5, incRowExcel, 5].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 6].Value = rsDetail.Commission;
                                                worksheet.Cells[incRowExcel, 6].Style.WrapText = true;
                                                worksheet.Cells[incRowExcel, 6, incRowExcel, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                worksheet.Cells[incRowExcel, 6, incRowExcel, 6].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 7].Value = rsDetail.NetCommission;
                                                worksheet.Cells[incRowExcel, 7].Style.WrapText = true;
                                                worksheet.Cells[incRowExcel, 7, incRowExcel, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                worksheet.Cells[incRowExcel, 7, incRowExcel, 7].Style.VerticalAlignment = ExcelVerticalAlignment.Center;


                                                worksheet.Cells[incRowExcel, 2, incRowExcel, 7].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells[incRowExcel, 2, incRowExcel, 7].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells[incRowExcel, 2, incRowExcel, 7].Style.Font.Size = 11;


                                                incRowExcel++;
                                                no++;
                                            }

                                            int last = incRowExcel - 1;
                                            worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.00";
                                            worksheet.Cells[incRowExcel, 6].Formula = "SUM(F" + first.ToString() + ":F" + last.ToString() + ")";
                                            worksheet.Cells[incRowExcel, 6].Calculate();
                                            worksheet.Cells[incRowExcel, 6, incRowExcel, 6].Style.Font.Size = 11;

                                        }
                                        worksheet.PrinterSettings.FitToPage = true;
                                        worksheet.PrinterSettings.FitToWidth = 1;
                                        worksheet.PrinterSettings.FitToHeight = 0;
                                        worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel - 1, 9];
                                        worksheet.Column(2).Width = 10;
                                        worksheet.Column(3).Width = 40;
                                        worksheet.Column(4).Width = 20;
                                        worksheet.Column(5).Width = 30;
                                        worksheet.Column(6).Width = 25;
                                        worksheet.Column(7).Width = 20;



                                        // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                        //worksheet.Cells.AutoFitColumns(); // UNTUK AUTO FIT UKURAN TULISAN DENGAN KOLOM, KEBETULAN INI BUAT HEADER AJA DIPAKENYA
                                        //worksheet.PrinterSettings.FitToPage = true;
                                        worksheet.PrinterSettings.Orientation = eOrientation.Portrait; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                        worksheet.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN


                                        Image img = Image.FromFile(Tools.ReportImage);
                                        worksheet.HeaderFooter.OddHeader.InsertPicture(img, PictureAlignment.Left);

                                        worksheet.HeaderFooter.OddHeader.CenteredText = "&12 BROKER COMMISSION SUMMARY ";

                                        // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                        //ExcelWorksheet ws = package.Workbook.Worksheets.Add("Demo");

                                        worksheet.PrinterSettings.TopMargin = (decimal).5 / 2.54M; // narrow border
                                        worksheet.PrinterSettings.RightMargin = (decimal).5 / 2.54M; //narrow borderExcelWorksheet ws = pck.Workbook.Worksheets.Add("Demo");
                                        worksheet.PrinterSettings.LeftMargin = (decimal).5 / 2.54M; // narrow border

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

            else
            {
                return false;
            }
        }

        //ACCOUNTING
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

                            cmd.CommandText = @"  
Select isnull(Row,0) Row,isnull(Col,0) Col,sum(isnull(dbo.FGetAccountBalanceByDateByParent(@Date,AccountPK),0) * Case when Operator = 1 then 1 else -1 end) Balance 
from TemplateReport
where status = 2 and ReportName = @ReportName
group by Row,Col
                            ";
                            cmd.CommandTimeout = 0;
                            cmd.Parameters.AddWithValue("@ReportName", _accountingRpt.ReportName);
                            cmd.Parameters.AddWithValue("@Date", _accountingRpt.ValueDateTo);

                            using (SqlDataReader dr0 = cmd.ExecuteReader())
                            {
                                if (!dr0.HasRows)
                                {
                                    return false;
                                }
                                else
                                {

                                    string filePath = Tools.ReportsPath + "19_LaporanKeuangan" + "_" + _userID + ".xlsx";
                                    File.Copy(Tools.ReportsTemplatePath + "\\19\\" + "19_LapKeu.xlsx", filePath, true);
                                    FileInfo excelFile = new FileInfo(filePath);


                                    // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                    using (ExcelPackage package = new ExcelPackage(excelFile))
                                    {
                                        ExcelWorksheet worksheet = package.Workbook.Worksheets[1];

                                        while (dr0.Read())
                                        {
                                            worksheet.Cells[Convert.ToInt16(dr0["Row"]), Convert.ToInt16(dr0["Col"])].Value = Convert.ToDecimal(dr0["Balance"]);
                                        }


                                        using (SqlConnection subCon1 = new SqlConnection(Tools.conString))
                                        {
                                            subCon1.Open();
                                            using (SqlCommand subCmd1 = subCon1.CreateCommand())
                                            {
                                                subCmd1.CommandText = @"
                                                 
	Declare @PeriodPK int
    Select @PeriodPK = PeriodPK from Period where @ValueDateFrom between DateFrom and DateTo and status = 2

SELECT ISNULL(E.Row,0) Row,       
    CAST(ISNULL(B.PreviousBaseBalance, 0) AS NUMERIC(19,4)) AS PreviousBaseBalance,      
    (CAST(A.CurrentBaseDebit - ISNULL(B.PreviousBaseDebit, 0) AS NUMERIC(19,4))) - (CAST(A.CurrentBaseCredit - ISNULL(B.PreviousBaseCredit, 0) AS NUMERIC(19,4)))   AS Transaksi,       
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
    WHERE  B.ValueDate <= @ValueDateTo and  B.PeriodPK = @PeriodPK
	
	AND B.status = 2 AND B.Posted = 1 AND B.Reversed = 0

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
    WHERE  B.ValueDate < @ValueDateFrom  and  B.PeriodPK = @PeriodPK 
	AND B.status = 2 AND B.Posted = 1 AND B.Reversed = 0
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
    ) AS B ON A.AccountPK = B.AccountPK        
    INNER JOIN Account C ON A.AccountPK = C.AccountPK   And C.Status in (1,2)    
    INNER JOIN Currency D ON C.CurrencyPK = D.CurrencyPK   And D.Status in (1,2)   
	LEFT JOIN TemplateReport E ON C.AccountPK = E.AccountPK AND E.Status = 2 AND E.ReportName = 'Laporan Keuangan'
    WHERE ((A.CurrentBalance <> 0)        
    OR (A.CurrentDebit - ISNULL(B.PreviousDebit, 0) <> 0)        
    OR (A.CurrentCredit - ISNULL(B.PreviousCredit, 0) <> 0)        
    OR (A.CurrentBaseBalance <> 0)        
    OR (A.CurrentBaseDebit - ISNULL(B.PreviousBaseDebit, 0) <> 0)        
    OR (A.CurrentBaseCredit - ISNULL(B.PreviousBaseCredit, 0) <> 0))     

	AND C.AccountPK IN 
	(
		SELECT AccountPK from TemplateReport where status = 2 and ReportName = 'Laporan Keuangan'
	)
    Order BY E.Row            
                                                ";
                                                subCmd1.CommandTimeout = 0;
                                                subCmd1.Parameters.AddWithValue("@ValueDateFrom", _accountingRpt.ValueDateFrom);
                                                subCmd1.Parameters.AddWithValue("@ValueDateTo", _accountingRpt.ValueDateTo);
                                                using (SqlDataReader dr1 = subCmd1.ExecuteReader())
                                                {
                                                    if (!dr1.HasRows)
                                                    {
                                                        return false;
                                                    }
                                                    else
                                                    {
                                                        ExcelWorksheet worksheet1 = package.Workbook.Worksheets[1];
                                                        while (dr1.Read())
                                                        {

                                                        }

                                                    }
                                                }
                                            }
                                        }

                                        using (SqlConnection subCon2 = new SqlConnection(Tools.conString))
                                        {
                                            subCon2.Open();
                                            using (SqlCommand subCmd2 = subCon2.CreateCommand())
                                            {
                                                subCmd2.CommandText = @"
                                                 
	Declare @PeriodPK int
    Select @PeriodPK = PeriodPK from Period where @ValueDateFrom between DateFrom and DateTo and status = 2

SELECT ISNULL(E.Row,0) Row,       
    CAST(ISNULL(B.PreviousBaseBalance, 0) AS NUMERIC(19,4)) AS PreviousBaseBalance,      
    (CAST(A.CurrentBaseDebit - ISNULL(B.PreviousBaseDebit, 0) AS NUMERIC(19,4))) - (CAST(A.CurrentBaseCredit - ISNULL(B.PreviousBaseCredit, 0) AS NUMERIC(19,4)))   AS Transaksi,       
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
    WHERE  B.ValueDate <= @ValueDateTo and  B.PeriodPK = @PeriodPK
	
	AND B.status = 2 AND B.Posted = 1 AND B.Reversed = 0

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
    WHERE  B.ValueDate < @ValueDateFrom  and  B.PeriodPK = @PeriodPK 
	AND B.status = 2 AND B.Posted = 1 AND B.Reversed = 0
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
    ) AS B ON A.AccountPK = B.AccountPK        
    INNER JOIN Account C ON A.AccountPK = C.AccountPK   And C.Status in (1,2)    
    INNER JOIN Currency D ON C.CurrencyPK = D.CurrencyPK   And D.Status in (1,2)   
	LEFT JOIN TemplateReport E ON C.AccountPK = E.AccountPK AND E.Status = 2 AND E.ReportName = '19_IS'
    WHERE ((A.CurrentBalance <> 0)        
    OR (A.CurrentDebit - ISNULL(B.PreviousDebit, 0) <> 0)        
    OR (A.CurrentCredit - ISNULL(B.PreviousCredit, 0) <> 0)        
    OR (A.CurrentBaseBalance <> 0)        
    OR (A.CurrentBaseDebit - ISNULL(B.PreviousBaseDebit, 0) <> 0)        
    OR (A.CurrentBaseCredit - ISNULL(B.PreviousBaseCredit, 0) <> 0))     

	AND C.AccountPK IN 
	(
		SELECT AccountPK from TemplateReport where status = 2 and ReportName = '19_IS'
	)
    Order BY E.Row            
                                                ";
                                                subCmd2.CommandTimeout = 0;
                                                subCmd2.Parameters.AddWithValue("@ValueDateFrom", _accountingRpt.ValueDateFrom);
                                                subCmd2.Parameters.AddWithValue("@ValueDateTo", _accountingRpt.ValueDateTo);
                                                using (SqlDataReader dr2 = subCmd2.ExecuteReader())
                                                {
                                                    if (!dr2.HasRows)
                                                    {
                                                        return false;
                                                    }
                                                    else
                                                    {
                                                        ExcelWorksheet worksheet2 = package.Workbook.Worksheets[2];
                                                        while (dr2.Read())
                                                        {
                                                            worksheet2.Cells[Convert.ToInt16(dr2["Row"]), 4].Value = Convert.ToDecimal(dr2["PreviousBaseBalance"]);
                                                            worksheet2.Cells[Convert.ToInt16(dr2["Row"]), 5].Value = Convert.ToDecimal(dr2["Transaksi"]);
                                                            worksheet2.Cells[Convert.ToInt16(dr2["Row"]), 6].Value = Convert.ToDecimal(dr2["CurrentBaseBalance"]);
                                                        }

                                                    }
                                                }
                                            }
                                        }


                                        using (SqlConnection subCon3 = new SqlConnection(Tools.conString))
                                        {
                                            subCon3.Open();
                                            using (SqlCommand subCmd3 = subCon3.CreateCommand())
                                            {
                                                subCmd3.CommandText = @"
                                                 
	Declare @PeriodPK int
    Select @PeriodPK = PeriodPK from Period where @ValueDateFrom between DateFrom and DateTo and status = 2

SELECT ISNULL(E.Row,0) Row,       
    CAST(ISNULL(B.PreviousBaseBalance, 0) AS NUMERIC(19,4)) AS PreviousBaseBalance,      
    (CAST(A.CurrentBaseDebit - ISNULL(B.PreviousBaseDebit, 0) AS NUMERIC(19,4))) - (CAST(A.CurrentBaseCredit - ISNULL(B.PreviousBaseCredit, 0) AS NUMERIC(19,4)))   AS Transaksi,       
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
    WHERE  B.ValueDate <= @ValueDateTo and  B.PeriodPK = @PeriodPK
	
	AND B.status = 2 AND B.Posted = 1 AND B.Reversed = 0

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
    WHERE  B.ValueDate < @ValueDateFrom  and  B.PeriodPK = @PeriodPK 
	AND B.status = 2 AND B.Posted = 1 AND B.Reversed = 0
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
    ) AS B ON A.AccountPK = B.AccountPK        
    INNER JOIN Account C ON A.AccountPK = C.AccountPK   And C.Status in (1,2)    
    INNER JOIN Currency D ON C.CurrencyPK = D.CurrencyPK   And D.Status in (1,2)   
	LEFT JOIN TemplateReport E ON C.AccountPK = E.AccountPK AND E.Status = 2 AND E.ReportName = '19_RL1'
    WHERE ((A.CurrentBalance <> 0)        
    OR (A.CurrentDebit - ISNULL(B.PreviousDebit, 0) <> 0)        
    OR (A.CurrentCredit - ISNULL(B.PreviousCredit, 0) <> 0)        
    OR (A.CurrentBaseBalance <> 0)        
    OR (A.CurrentBaseDebit - ISNULL(B.PreviousBaseDebit, 0) <> 0)        
    OR (A.CurrentBaseCredit - ISNULL(B.PreviousBaseCredit, 0) <> 0))     

	AND C.AccountPK IN 
	(
		SELECT AccountPK from TemplateReport where status = 2 and ReportName = '19_RL1'
	)
    Order BY E.Row            
                                                ";
                                                subCmd3.CommandTimeout = 0;
                                                subCmd3.Parameters.AddWithValue("@ValueDateFrom", _accountingRpt.ValueDateFrom);
                                                subCmd3.Parameters.AddWithValue("@ValueDateTo", _accountingRpt.ValueDateTo);
                                                using (SqlDataReader dr3 = subCmd3.ExecuteReader())
                                                {
                                                    if (!dr3.HasRows)
                                                    {
                                                        return false;
                                                    }
                                                    else
                                                    {
                                                        ExcelWorksheet worksheet3 = package.Workbook.Worksheets[3];
                                                        while (dr3.Read())
                                                        {
                                                        }

                                                    }
                                                }
                                            }
                                        }


                                        //                                        using (SqlConnection subCon4 = new SqlConnection(Tools.conString))
                                        //                                        {
                                        //                                            subCon4.Open();
                                        //                                            using (SqlCommand subCmd4 = subCon4.CreateCommand())
                                        //                                            {
                                        //                                                subCmd4.CommandText = @"
                                        //                                                 
                                        //	Declare @PeriodPK int
                                        //    Select @PeriodPK = PeriodPK from Period where @ValueDateFrom between DateFrom and DateTo and status = 2
                                        //
                                        //SELECT ISNULL(E.Row,0) Row,       
                                        //    CAST(ISNULL(B.PreviousBaseBalance, 0) AS NUMERIC(19,4)) AS PreviousBaseBalance,      
                                        //    (CAST(A.CurrentBaseDebit - ISNULL(B.PreviousBaseDebit, 0) AS NUMERIC(19,4))) - (CAST(A.CurrentBaseCredit - ISNULL(B.PreviousBaseCredit, 0) AS NUMERIC(19,4)))   AS Transaksi,       
                                        //    CAST(A.CurrentBaseBalance AS NUMERIC(19,4)) AS CurrentBaseBalance      
                                        //    FROM (      
                                        //    SELECT A.AccountPK,       
                                        //    SUM(B.Balance) AS CurrentBalance,       
                                        //    SUM(B.BaseBalance) AS CurrentBaseBalance,      
                                        //    SUM(B.SumDebit) AS CurrentDebit,       
                                        //    SUM(B.SumCredit) AS CurrentCredit,       
                                        //    SUM(B.SumBaseDebit) AS CurrentBaseDebit,       
                                        //    SUM(B.SumBaseCredit) AS CurrentBaseCredit      
                                        //    FROM [Account] A INNER JOIN [Currency] C ON A.CurrencyPK = C.CurrencyPK, (      
                                        //    SELECT A.AccountPK, SUM(A.Debit-A.Credit) AS Balance,       
                                        //    SUM(A.BaseDebit-A.BaseCredit) AS BaseBalance,      
                                        //    SUM(A.Debit) AS SumDebit,      
                                        //    SUM(A.Credit) AS SumCredit,      
                                        //    SUM(A.BaseDebit) AS SumBaseDebit,      
                                        //    SUM(A.BaseCredit) AS SumBaseCredit,      
                                        //    C.ParentPK1, C.ParentPK2, C.ParentPK3, C.ParentPK4, C.ParentPK5,      
                                        //    C.ParentPK6, C.ParentPK7, C.ParentPK8, C.ParentPK9     
                                        //    FROM [JournalDetail] A INNER JOIN [Journal] B ON A.JournalPK = B.JournalPK      
                                        //    INNER JOIN Account C ON A.AccountPK = C.AccountPK   And C.Status in (1,2)  
                                        //    INNER JOIN Currency D ON A.CurrencyPK = D.CurrencyPK And D.Status in (1,2)   
                                        //    WHERE  B.ValueDate <= @ValueDateTo and  B.PeriodPK = @PeriodPK
                                        //	
                                        //	AND B.status = 2 AND B.Posted = 1 AND B.Reversed = 0
                                        //
                                        //    Group BY A.AccountPK, C.ParentPK1, C.ParentPK2,        
                                        //    C.ParentPK3, C.ParentPK4, C.ParentPK5, C.ParentPK6,       
                                        //    C.ParentPK7, C.ParentPK8, C.ParentPK9        
                                        //    ) AS B        
                                        //    WHERE
                                        //(B.AccountPK = A.AccountPK OR B.ParentPK1 = A.AccountPK OR B.ParentPK2 = A.AccountPK        
                                        //    OR B.ParentPK3 = A.AccountPK OR B.ParentPK4 = A.AccountPK OR B.ParentPK5 = A.AccountPK        
                                        //    OR B.ParentPK6 = A.AccountPK OR B.ParentPK7 = A.AccountPK OR B.ParentPK8 = A.AccountPK        
                                        //    OR B.ParentPK9 = A.AccountPK)       and A.Status = 2 
                                        //    Group BY A.AccountPK       
                                        //    ) AS A LEFT JOIN (       
                                        //    SELECT A.AccountPK,        
                                        //    SUM(B.Balance) AS PreviousBalance,        
                                        //    SUM(B.BaseBalance) AS PreviousBaseBalance,       
                                        //    SUM(B.SumDebit) AS PreviousDebit,        
                                        //    SUM(B.SumCredit) AS PreviousCredit,        
                                        //    SUM(B.SumBaseDebit) AS PreviousBaseDebit,        
                                        //    SUM(B.SumBaseCredit) AS PreviousBaseCredit       
                                        //    FROM [Account] A INNER JOIN [Currency] C ON A.CurrencyPK = C.CurrencyPK, (       
                                        //    SELECT A.AccountPK, SUM(A.Debit-A.Credit) AS Balance,        
                                        //    SUM(A.BaseDebit-A.BaseCredit) AS BaseBalance,        
                                        //    SUM(A.Debit) AS SumDebit,        
                                        //    SUM(A.Credit) AS SumCredit,        
                                        //    SUM(A.BaseDebit) AS SumBaseDebit,        
                                        //    SUM(A.BaseCredit) AS SumBaseCredit,        
                                        //    C.ParentPK1, C.ParentPK2, C.ParentPK3, C.ParentPK4, C.ParentPK5,        
                                        //    C.ParentPK6, C.ParentPK7, C.ParentPK8, C.ParentPK9       
                                        //    FROM [JournalDetail] A INNER JOIN [Journal] B ON A.JournalPK = B.JournalPK        
                                        //    INNER JOIN Account C ON A.AccountPK = C.AccountPK   And C.Status in (1,2)   
                                        //    INNER JOIN Currency D ON A.CurrencyPK = D.CurrencyPK    And D.Status in (1,2)   
                                        //    WHERE  B.ValueDate < @ValueDateFrom  and  B.PeriodPK = @PeriodPK 
                                        //	AND B.status = 2 AND B.Posted = 1 AND B.Reversed = 0
                                        //    Group BY A.AccountPK, C.ParentPK1, C.ParentPK2,        
                                        //    C.ParentPK3, C.ParentPK4, C.ParentPK5, C.ParentPK6,       
                                        //    C.ParentPK7, C.ParentPK8, C.ParentPK9        
                                        //    ) AS B        
                                        //    WHERE 
                                        //	
                                        //	(B.AccountPK = A.AccountPK OR B.ParentPK1 = A.AccountPK OR B.ParentPK2 = A.AccountPK        
                                        //    OR B.ParentPK3 = A.AccountPK OR B.ParentPK4 = A.AccountPK OR B.ParentPK5 = A.AccountPK        
                                        //    OR B.ParentPK6 = A.AccountPK OR B.ParentPK7 = A.AccountPK OR B.ParentPK8 = A.AccountPK        
                                        //    OR B.ParentPK9 = A.AccountPK)       and A.Status = 2 
                                        //    Group BY A.AccountPK       
                                        //    ) AS B ON A.AccountPK = B.AccountPK        
                                        //    INNER JOIN Account C ON A.AccountPK = C.AccountPK   And C.Status in (1,2)    
                                        //    INNER JOIN Currency D ON C.CurrencyPK = D.CurrencyPK   And D.Status in (1,2)   
                                        //	LEFT JOIN TemplateReport E ON C.AccountPK = E.AccountPK AND E.Status = 2 AND E.ReportName = '19_RL2'
                                        //    WHERE ((A.CurrentBalance <> 0)        
                                        //    OR (A.CurrentDebit - ISNULL(B.PreviousDebit, 0) <> 0)        
                                        //    OR (A.CurrentCredit - ISNULL(B.PreviousCredit, 0) <> 0)        
                                        //    OR (A.CurrentBaseBalance <> 0)        
                                        //    OR (A.CurrentBaseDebit - ISNULL(B.PreviousBaseDebit, 0) <> 0)        
                                        //    OR (A.CurrentBaseCredit - ISNULL(B.PreviousBaseCredit, 0) <> 0))     
                                        //
                                        //	AND C.AccountPK IN 
                                        //	(
                                        //		SELECT AccountPK from TemplateReport where status = 2 and ReportName = '19_RL2'
                                        //	)
                                        //    Order BY E.Row            
                                        //                                                ";
                                        //                                                subCmd4.CommandTimeout = 0;
                                        //                                                subCmd4.Parameters.AddWithValue("@ValueDateFrom", _accountingRpt.ValueDateFrom);
                                        //                                                subCmd4.Parameters.AddWithValue("@ValueDateTo", _accountingRpt.ValueDateTo);
                                        //                                                using (SqlDataReader dr4 = subCmd4.ExecuteReader())
                                        //                                                {
                                        //                                                    if (!dr4.HasRows)
                                        //                                                    {
                                        //                                                        return false;
                                        //                                                    }
                                        //                                                    else
                                        //                                                    {
                                        //                                                        ExcelWorksheet worksheet4 = package.Workbook.Worksheets[4];
                                        //                                                        while (dr4.Read())
                                        //                                                        {
                                        //                                                            worksheet4.Cells[Convert.ToInt16(dr4["Row"]), 4].Value = Convert.ToDecimal(dr4["PreviousBaseBalance"]);
                                        //                                                            worksheet4.Cells[Convert.ToInt16(dr4["Row"]), 5].Value = Convert.ToDecimal(dr4["Transaksi"]);
                                        //                                                            worksheet4.Cells[Convert.ToInt16(dr4["Row"]), 6].Value = Convert.ToDecimal(dr4["CurrentBaseBalance"]);
                                        //                                                        }

                                        //                                                    }
                                        //                                                }
                                        //                                            }
                                        //                                        }

                                        //                                        using (SqlConnection subCon5 = new SqlConnection(Tools.conString))
                                        //                                        {
                                        //                                            subCon5.Open();
                                        //                                            using (SqlCommand subCmd5 = subCon5.CreateCommand())
                                        //                                            {
                                        //                                                subCmd5.CommandText = @"
                                        //                                                 
                                        //	Declare @PeriodPK int
                                        //    Select @PeriodPK = PeriodPK from Period where @ValueDateFrom between DateFrom and DateTo and status = 2
                                        //
                                        //SELECT ISNULL(E.Row,0) Row,       
                                        //    CAST(ISNULL(B.PreviousBaseBalance, 0) AS NUMERIC(19,4)) AS PreviousBaseBalance,      
                                        //    (CAST(A.CurrentBaseDebit - ISNULL(B.PreviousBaseDebit, 0) AS NUMERIC(19,4))) - (CAST(A.CurrentBaseCredit - ISNULL(B.PreviousBaseCredit, 0) AS NUMERIC(19,4)))   AS Transaksi,       
                                        //    CAST(A.CurrentBaseBalance AS NUMERIC(19,4)) AS CurrentBaseBalance      
                                        //    FROM (      
                                        //    SELECT A.AccountPK,       
                                        //    SUM(B.Balance) AS CurrentBalance,       
                                        //    SUM(B.BaseBalance) AS CurrentBaseBalance,      
                                        //    SUM(B.SumDebit) AS CurrentDebit,       
                                        //    SUM(B.SumCredit) AS CurrentCredit,       
                                        //    SUM(B.SumBaseDebit) AS CurrentBaseDebit,       
                                        //    SUM(B.SumBaseCredit) AS CurrentBaseCredit      
                                        //    FROM [Account] A INNER JOIN [Currency] C ON A.CurrencyPK = C.CurrencyPK, (      
                                        //    SELECT A.AccountPK, SUM(A.Debit-A.Credit) AS Balance,       
                                        //    SUM(A.BaseDebit-A.BaseCredit) AS BaseBalance,      
                                        //    SUM(A.Debit) AS SumDebit,      
                                        //    SUM(A.Credit) AS SumCredit,      
                                        //    SUM(A.BaseDebit) AS SumBaseDebit,      
                                        //    SUM(A.BaseCredit) AS SumBaseCredit,      
                                        //    C.ParentPK1, C.ParentPK2, C.ParentPK3, C.ParentPK4, C.ParentPK5,      
                                        //    C.ParentPK6, C.ParentPK7, C.ParentPK8, C.ParentPK9     
                                        //    FROM [JournalDetail] A INNER JOIN [Journal] B ON A.JournalPK = B.JournalPK      
                                        //    INNER JOIN Account C ON A.AccountPK = C.AccountPK   And C.Status in (1,2)  
                                        //    INNER JOIN Currency D ON A.CurrencyPK = D.CurrencyPK And D.Status in (1,2)   
                                        //    WHERE  B.ValueDate <= @ValueDateTo and  B.PeriodPK = @PeriodPK
                                        //	
                                        //	AND B.status = 2 AND B.Posted = 1 AND B.Reversed = 0
                                        //
                                        //    Group BY A.AccountPK, C.ParentPK1, C.ParentPK2,        
                                        //    C.ParentPK3, C.ParentPK4, C.ParentPK5, C.ParentPK6,       
                                        //    C.ParentPK7, C.ParentPK8, C.ParentPK9        
                                        //    ) AS B        
                                        //    WHERE
                                        //(B.AccountPK = A.AccountPK OR B.ParentPK1 = A.AccountPK OR B.ParentPK2 = A.AccountPK        
                                        //    OR B.ParentPK3 = A.AccountPK OR B.ParentPK4 = A.AccountPK OR B.ParentPK5 = A.AccountPK        
                                        //    OR B.ParentPK6 = A.AccountPK OR B.ParentPK7 = A.AccountPK OR B.ParentPK8 = A.AccountPK        
                                        //    OR B.ParentPK9 = A.AccountPK)       and A.Status = 2 
                                        //    Group BY A.AccountPK       
                                        //    ) AS A LEFT JOIN (       
                                        //    SELECT A.AccountPK,        
                                        //    SUM(B.Balance) AS PreviousBalance,        
                                        //    SUM(B.BaseBalance) AS PreviousBaseBalance,       
                                        //    SUM(B.SumDebit) AS PreviousDebit,        
                                        //    SUM(B.SumCredit) AS PreviousCredit,        
                                        //    SUM(B.SumBaseDebit) AS PreviousBaseDebit,        
                                        //    SUM(B.SumBaseCredit) AS PreviousBaseCredit       
                                        //    FROM [Account] A INNER JOIN [Currency] C ON A.CurrencyPK = C.CurrencyPK, (       
                                        //    SELECT A.AccountPK, SUM(A.Debit-A.Credit) AS Balance,        
                                        //    SUM(A.BaseDebit-A.BaseCredit) AS BaseBalance,        
                                        //    SUM(A.Debit) AS SumDebit,        
                                        //    SUM(A.Credit) AS SumCredit,        
                                        //    SUM(A.BaseDebit) AS SumBaseDebit,        
                                        //    SUM(A.BaseCredit) AS SumBaseCredit,        
                                        //    C.ParentPK1, C.ParentPK2, C.ParentPK3, C.ParentPK4, C.ParentPK5,        
                                        //    C.ParentPK6, C.ParentPK7, C.ParentPK8, C.ParentPK9       
                                        //    FROM [JournalDetail] A INNER JOIN [Journal] B ON A.JournalPK = B.JournalPK        
                                        //    INNER JOIN Account C ON A.AccountPK = C.AccountPK   And C.Status in (1,2)   
                                        //    INNER JOIN Currency D ON A.CurrencyPK = D.CurrencyPK    And D.Status in (1,2)   
                                        //    WHERE  B.ValueDate < @ValueDateFrom  and  B.PeriodPK = @PeriodPK 
                                        //	AND B.status = 2 AND B.Posted = 1 AND B.Reversed = 0
                                        //    Group BY A.AccountPK, C.ParentPK1, C.ParentPK2,        
                                        //    C.ParentPK3, C.ParentPK4, C.ParentPK5, C.ParentPK6,       
                                        //    C.ParentPK7, C.ParentPK8, C.ParentPK9        
                                        //    ) AS B        
                                        //    WHERE 
                                        //	
                                        //	(B.AccountPK = A.AccountPK OR B.ParentPK1 = A.AccountPK OR B.ParentPK2 = A.AccountPK        
                                        //    OR B.ParentPK3 = A.AccountPK OR B.ParentPK4 = A.AccountPK OR B.ParentPK5 = A.AccountPK        
                                        //    OR B.ParentPK6 = A.AccountPK OR B.ParentPK7 = A.AccountPK OR B.ParentPK8 = A.AccountPK        
                                        //    OR B.ParentPK9 = A.AccountPK)       and A.Status = 2 
                                        //    Group BY A.AccountPK       
                                        //    ) AS B ON A.AccountPK = B.AccountPK        
                                        //    INNER JOIN Account C ON A.AccountPK = C.AccountPK   And C.Status in (1,2)    
                                        //    INNER JOIN Currency D ON C.CurrencyPK = D.CurrencyPK   And D.Status in (1,2)   
                                        //	LEFT JOIN TemplateReport E ON C.AccountPK = E.AccountPK AND E.Status = 2 AND E.ReportName = '19_PK1'
                                        //    WHERE ((A.CurrentBalance <> 0)        
                                        //    OR (A.CurrentDebit - ISNULL(B.PreviousDebit, 0) <> 0)        
                                        //    OR (A.CurrentCredit - ISNULL(B.PreviousCredit, 0) <> 0)        
                                        //    OR (A.CurrentBaseBalance <> 0)        
                                        //    OR (A.CurrentBaseDebit - ISNULL(B.PreviousBaseDebit, 0) <> 0)        
                                        //    OR (A.CurrentBaseCredit - ISNULL(B.PreviousBaseCredit, 0) <> 0))     
                                        //
                                        //	AND C.AccountPK IN 
                                        //	(
                                        //		SELECT AccountPK from TemplateReport where status = 2 and ReportName = '19_PK1'
                                        //	)
                                        //    Order BY E.Row            
                                        //                                                ";
                                        //                                                subCmd5.CommandTimeout = 0;
                                        //                                                subCmd5.Parameters.AddWithValue("@ValueDateFrom", _accountingRpt.ValueDateFrom);
                                        //                                                subCmd5.Parameters.AddWithValue("@ValueDateTo", _accountingRpt.ValueDateTo);
                                        //                                                using (SqlDataReader dr5 = subCmd5.ExecuteReader())
                                        //                                                {
                                        //                                                    if (!dr5.HasRows)
                                        //                                                    {
                                        //                                                        return false;
                                        //                                                    }
                                        //                                                    else
                                        //                                                    {
                                        //                                                        ExcelWorksheet worksheet5 = package.Workbook.Worksheets[5];
                                        //                                                        while (dr5.Read())
                                        //                                                        {
                                        //                                                            worksheet5.Cells[Convert.ToInt16(dr5["Row"]), 2].Value = Convert.ToDecimal(dr5["PreviousBaseBalance"]);
                                        //                                                            worksheet5.Cells[Convert.ToInt16(dr5["Row"]), 3].Value = Convert.ToDecimal(dr5["CurrentBaseBalance"]);
                                        //                                                        }

                                        //                                                    }
                                        //                                                }
                                        //                                            }
                                        //                                        }


                                        //                                        using (SqlConnection subCon6 = new SqlConnection(Tools.conString))
                                        //                                        {
                                        //                                            subCon6.Open();
                                        //                                            using (SqlCommand subCmd6 = subCon6.CreateCommand())
                                        //                                            {
                                        //                                                subCmd6.CommandText = @"
                                        //                                                 
                                        //	Declare @PeriodPK int
                                        //    Select @PeriodPK = PeriodPK from Period where @ValueDateFrom between DateFrom and DateTo and status = 2
                                        //
                                        //SELECT ISNULL(E.Row,0) Row,       
                                        //    CAST(ISNULL(B.PreviousBaseBalance, 0) AS NUMERIC(19,4)) AS PreviousBaseBalance,      
                                        //    (CAST(A.CurrentBaseDebit - ISNULL(B.PreviousBaseDebit, 0) AS NUMERIC(19,4))) - (CAST(A.CurrentBaseCredit - ISNULL(B.PreviousBaseCredit, 0) AS NUMERIC(19,4)))   AS Transaksi,       
                                        //    CAST(A.CurrentBaseBalance AS NUMERIC(19,4)) AS CurrentBaseBalance      
                                        //    FROM (      
                                        //    SELECT A.AccountPK,       
                                        //    SUM(B.Balance) AS CurrentBalance,       
                                        //    SUM(B.BaseBalance) AS CurrentBaseBalance,      
                                        //    SUM(B.SumDebit) AS CurrentDebit,       
                                        //    SUM(B.SumCredit) AS CurrentCredit,       
                                        //    SUM(B.SumBaseDebit) AS CurrentBaseDebit,       
                                        //    SUM(B.SumBaseCredit) AS CurrentBaseCredit      
                                        //    FROM [Account] A INNER JOIN [Currency] C ON A.CurrencyPK = C.CurrencyPK, (      
                                        //    SELECT A.AccountPK, SUM(A.Debit-A.Credit) AS Balance,       
                                        //    SUM(A.BaseDebit-A.BaseCredit) AS BaseBalance,      
                                        //    SUM(A.Debit) AS SumDebit,      
                                        //    SUM(A.Credit) AS SumCredit,      
                                        //    SUM(A.BaseDebit) AS SumBaseDebit,      
                                        //    SUM(A.BaseCredit) AS SumBaseCredit,      
                                        //    C.ParentPK1, C.ParentPK2, C.ParentPK3, C.ParentPK4, C.ParentPK5,      
                                        //    C.ParentPK6, C.ParentPK7, C.ParentPK8, C.ParentPK9     
                                        //    FROM [JournalDetail] A INNER JOIN [Journal] B ON A.JournalPK = B.JournalPK      
                                        //    INNER JOIN Account C ON A.AccountPK = C.AccountPK   And C.Status in (1,2)  
                                        //    INNER JOIN Currency D ON A.CurrencyPK = D.CurrencyPK And D.Status in (1,2)   
                                        //    WHERE  B.ValueDate <= @ValueDateTo and  B.PeriodPK = @PeriodPK
                                        //	
                                        //	AND B.status = 2 AND B.Posted = 1 AND B.Reversed = 0
                                        //
                                        //    Group BY A.AccountPK, C.ParentPK1, C.ParentPK2,        
                                        //    C.ParentPK3, C.ParentPK4, C.ParentPK5, C.ParentPK6,       
                                        //    C.ParentPK7, C.ParentPK8, C.ParentPK9        
                                        //    ) AS B        
                                        //    WHERE
                                        //(B.AccountPK = A.AccountPK OR B.ParentPK1 = A.AccountPK OR B.ParentPK2 = A.AccountPK        
                                        //    OR B.ParentPK3 = A.AccountPK OR B.ParentPK4 = A.AccountPK OR B.ParentPK5 = A.AccountPK        
                                        //    OR B.ParentPK6 = A.AccountPK OR B.ParentPK7 = A.AccountPK OR B.ParentPK8 = A.AccountPK        
                                        //    OR B.ParentPK9 = A.AccountPK)       and A.Status = 2 
                                        //    Group BY A.AccountPK       
                                        //    ) AS A LEFT JOIN (       
                                        //    SELECT A.AccountPK,        
                                        //    SUM(B.Balance) AS PreviousBalance,        
                                        //    SUM(B.BaseBalance) AS PreviousBaseBalance,       
                                        //    SUM(B.SumDebit) AS PreviousDebit,        
                                        //    SUM(B.SumCredit) AS PreviousCredit,        
                                        //    SUM(B.SumBaseDebit) AS PreviousBaseDebit,        
                                        //    SUM(B.SumBaseCredit) AS PreviousBaseCredit       
                                        //    FROM [Account] A INNER JOIN [Currency] C ON A.CurrencyPK = C.CurrencyPK, (       
                                        //    SELECT A.AccountPK, SUM(A.Debit-A.Credit) AS Balance,        
                                        //    SUM(A.BaseDebit-A.BaseCredit) AS BaseBalance,        
                                        //    SUM(A.Debit) AS SumDebit,        
                                        //    SUM(A.Credit) AS SumCredit,        
                                        //    SUM(A.BaseDebit) AS SumBaseDebit,        
                                        //    SUM(A.BaseCredit) AS SumBaseCredit,        
                                        //    C.ParentPK1, C.ParentPK2, C.ParentPK3, C.ParentPK4, C.ParentPK5,        
                                        //    C.ParentPK6, C.ParentPK7, C.ParentPK8, C.ParentPK9       
                                        //    FROM [JournalDetail] A INNER JOIN [Journal] B ON A.JournalPK = B.JournalPK        
                                        //    INNER JOIN Account C ON A.AccountPK = C.AccountPK   And C.Status in (1,2)   
                                        //    INNER JOIN Currency D ON A.CurrencyPK = D.CurrencyPK    And D.Status in (1,2)   
                                        //    WHERE  B.ValueDate < @ValueDateFrom  and  B.PeriodPK = @PeriodPK 
                                        //	AND B.status = 2 AND B.Posted = 1 AND B.Reversed = 0
                                        //    Group BY A.AccountPK, C.ParentPK1, C.ParentPK2,        
                                        //    C.ParentPK3, C.ParentPK4, C.ParentPK5, C.ParentPK6,       
                                        //    C.ParentPK7, C.ParentPK8, C.ParentPK9        
                                        //    ) AS B        
                                        //    WHERE 
                                        //	
                                        //	(B.AccountPK = A.AccountPK OR B.ParentPK1 = A.AccountPK OR B.ParentPK2 = A.AccountPK        
                                        //    OR B.ParentPK3 = A.AccountPK OR B.ParentPK4 = A.AccountPK OR B.ParentPK5 = A.AccountPK        
                                        //    OR B.ParentPK6 = A.AccountPK OR B.ParentPK7 = A.AccountPK OR B.ParentPK8 = A.AccountPK        
                                        //    OR B.ParentPK9 = A.AccountPK)       and A.Status = 2 
                                        //    Group BY A.AccountPK       
                                        //    ) AS B ON A.AccountPK = B.AccountPK        
                                        //    INNER JOIN Account C ON A.AccountPK = C.AccountPK   And C.Status in (1,2)    
                                        //    INNER JOIN Currency D ON C.CurrencyPK = D.CurrencyPK   And D.Status in (1,2)   
                                        //	LEFT JOIN TemplateReport E ON C.AccountPK = E.AccountPK AND E.Status = 2 AND E.ReportName = '19_PK2'
                                        //    WHERE ((A.CurrentBalance <> 0)        
                                        //    OR (A.CurrentDebit - ISNULL(B.PreviousDebit, 0) <> 0)        
                                        //    OR (A.CurrentCredit - ISNULL(B.PreviousCredit, 0) <> 0)        
                                        //    OR (A.CurrentBaseBalance <> 0)        
                                        //    OR (A.CurrentBaseDebit - ISNULL(B.PreviousBaseDebit, 0) <> 0)        
                                        //    OR (A.CurrentBaseCredit - ISNULL(B.PreviousBaseCredit, 0) <> 0))     
                                        //
                                        //	AND C.AccountPK IN 
                                        //	(
                                        //		SELECT AccountPK from TemplateReport where status = 2 and ReportName = '19_PK2'
                                        //	)
                                        //    Order BY E.Row            
                                        //                                                ";
                                        //                                                subCmd6.CommandTimeout = 0;
                                        //                                                subCmd6.Parameters.AddWithValue("@ValueDateFrom", _accountingRpt.ValueDateFrom);
                                        //                                                subCmd6.Parameters.AddWithValue("@ValueDateTo", _accountingRpt.ValueDateTo);
                                        //                                                using (SqlDataReader dr6 = subCmd6.ExecuteReader())
                                        //                                                {
                                        //                                                    if (!dr6.HasRows)
                                        //                                                    {
                                        //                                                        return false;
                                        //                                                    }
                                        //                                                    else
                                        //                                                    {
                                        //                                                        ExcelWorksheet worksheet6 = package.Workbook.Worksheets[6];
                                        //                                                        while (dr6.Read())
                                        //                                                        {
                                        //                                                            worksheet6.Cells[Convert.ToInt16(dr6["Row"]), 2].Value = Convert.ToDecimal(dr6["CurrentBaseBalance"]);
                                        //                                                        }

                                        //                                                    }
                                        //                                                }
                                        //                                            }
                                        //                                        }

                                        using (SqlConnection subCon7 = new SqlConnection(Tools.conString))
                                        {
                                            subCon7.Open();
                                            using (SqlCommand subCmd7 = subCon7.CreateCommand())
                                            {
                                                subCmd7.CommandText = @"
                                                 
	                                        DECLARE @LK TABLE
                                            (
                                            ID nvarchar(100),
                                            Name nvarchar(200),
                                            Groups bit,
                                            ParentPK int,
                                            CCY nvarchar(5),
                                            Bulan nvarchar(100),
                                            StartBalance numeric(22,4),
                                            Debit numeric(22,4),
                                            Credit numeric(22,4),
                                            EndBalance numeric(22,4)
                                            )

                                            Declare @PeriodPK int
                                            Select @PeriodPK = PeriodPK from Period where @ValueDateFrom between DateFrom and DateTo and status = 2

                                            Declare @DateCounter datetime
                                            Declare @DateCounterTo datetime
                                            Declare @StartDate datetime
                                            Declare @EndDate datetime

                                            set @DateCounter = EOMONTH(DATEADD(yy, DATEDIFF(yy, 0, @valuedateFrom), -1)) -- 31 jan
                                            set @StartDate = DATEADD(yy, DATEDIFF(yy, 0, @valuedateFrom), 0) -- 01 jan
                                            set @EndDate = EOMONTH(@valuedateto) -- 31 dec
                                            SET @DateCounterTo = EOMONTH(DATEADD(MONTH,1,@DateCounter),0) 

                                            while (@DateCounter<= @EndDate)
                                            BEGIN

                                            --select @DateCounter,@DateCounterTo
                                            INSERT INTO  @LK
                                            SELECT C.ID, C.Name, C.[Groups],C.[ParentPK],    
                                                D.ID, DATENAME(Month,@DateCounterTo) Bulan,      
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
                                                WHERE  B.ValueDate <= @DateCounterTo and  B.PeriodPK = @PeriodPK

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
                                                WHERE  B.ValueDate < @DateCounter  and  B.PeriodPK = @PeriodPK 
                                               
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
                                                ) AS B ON A.AccountPK = B.AccountPK        
                                                INNER JOIN Account C ON A.AccountPK = C.AccountPK   And C.Status in (1,2)    
                                                INNER JOIN Currency D ON C.CurrencyPK = D.CurrencyPK   And D.Status in (1,2)   
                                                WHERE (A.CurrentBalance <> 0)        
                                                OR (A.CurrentDebit - ISNULL(B.PreviousDebit, 0) <> 0)        
                                                OR (A.CurrentCredit - ISNULL(B.PreviousCredit, 0) <> 0)        
                                                OR (A.CurrentBaseBalance <> 0)        
                                                OR (A.CurrentBaseDebit - ISNULL(B.PreviousBaseDebit, 0) <> 0)        
                                                OR (A.CurrentBaseCredit - ISNULL(B.PreviousBaseCredit, 0) <> 0)     
                                                Order BY C.ID 
						  
									        
                                            SET @StartDate = DATEADD(MONTH,1,@StartDate)
                                            SET @DateCounter = EOMONTH(DATEADD(MONTH,1,@DateCounter),0)
                                            SET @DateCounterTo = EOMONTH(DATEADD(MONTH,1,@DateCounter),0)

                                            END



                                            select A.ID,A.Name,A.Groups,A.ParentPK,A.CCY,A.Bulan,isnull(A.StartBalance,0) StartBalance,sum(isnull(A.Debit,0) - isnull(A.Credit,0)) Movement,isnull(A.EndBalance,0) EndBalance,
                                            isnull(B.Bulan,'February') Bulan,isnull(B.StartBalance,0) StartBalance,sum(isnull(B.Debit,0) - isnull(B.Credit,0)) Movement,isnull(B.EndBalance,0) EndBalance,
                                            isnull(C.Bulan,'March') Bulan,isnull(C.StartBalance,0) StartBalance,sum(isnull(C.Debit,0) - isnull(C.Credit,0)) Movement,isnull(C.EndBalance,0) EndBalance,
                                            isnull(D.Bulan,'April') Bulan,isnull(D.StartBalance,0) StartBalance,sum(isnull(D.Debit,0) - isnull(D.Credit,0)) Movement,isnull(D.EndBalance,0) EndBalance,
                                            isnull(E.Bulan,'May') Bulan,isnull(E.StartBalance,0) StartBalance,sum(isnull(E.Debit,0) - isnull(E.Credit,0)) Movement,isnull(E.EndBalance,0) EndBalance,
                                            isnull(F.Bulan,'June') Bulan,isnull(F.StartBalance,0) StartBalance,sum(isnull(F.Debit,0) - isnull(F.Credit,0)) Movement,isnull(F.EndBalance,0) EndBalance,
                                            isnull(G.Bulan,'July') Bulan,isnull(G.StartBalance,0) StartBalance,sum(isnull(G.Debit,0) - isnull(G.Credit,0)) Movement,isnull(G.EndBalance,0) EndBalance,
                                            isnull(H.Bulan,'August') Bulan,isnull(H.StartBalance,0) StartBalance,sum(isnull(H.Debit,0) - isnull(H.Credit,0)) Movement,isnull(H.EndBalance,0) EndBalance,
                                            isnull(I.Bulan,'September') Bulan,isnull(I.StartBalance,0) StartBalance,sum(isnull(I.Debit,0) - isnull(I.Credit,0)) Movement,isnull(I.EndBalance,0) EndBalance,
                                            isnull(J.Bulan,'October') Bulan,isnull(J.StartBalance,0) StartBalance,sum(isnull(J.Debit,0) - isnull(J.Credit,0)) Movement,isnull(J.EndBalance,0) EndBalance,
                                            isnull(K.Bulan,'November') Bulan,isnull(K.StartBalance,0) StartBalance,sum(isnull(K.Debit,0) - isnull(K.Credit,0)) Movement,isnull(K.EndBalance,0) EndBalance,
                                            isnull(L.Bulan,'December') Bulan,isnull(L.StartBalance,0) StartBalance,sum(isnull(L.Debit,0) - isnull(L.Credit,0)) Movement,isnull(L.EndBalance,0) EndBalance from @LK A
                                            LEFT JOIN             
                                            (            
                                            select ID,Name,Groups,ParentPK,CCY,Bulan,StartBalance,Debit,Credit,EndBalance from @LK  
                                            where Bulan = 'February'        
                                            ) B On A.ID = B.ID and A.Name = B.Name and A.Groups = B.Groups and A.ParentPK = B.ParentPK and A.CCY = B.CCY
                                            LEFT JOIN             
                                            (            
                                            select ID,Name,Groups,ParentPK,CCY,Bulan,StartBalance,Debit,Credit,EndBalance from @LK  
                                            where Bulan = 'March'        
                                            ) C On A.ID = C.ID and A.Name = C.Name and A.Groups = C.Groups and A.ParentPK = C.ParentPK and A.CCY = C.CCY
                                            LEFT JOIN             
                                            (            
                                            select ID,Name,Groups,ParentPK,CCY,Bulan,StartBalance,Debit,Credit,EndBalance from @LK  
                                            where Bulan = 'April'        
                                            ) D On A.ID = D.ID and A.Name = D.Name and A.Groups = D.Groups and A.ParentPK = D.ParentPK and A.CCY = D.CCY
                                            LEFT JOIN             
                                            (            
                                            select ID,Name,Groups,ParentPK,CCY,Bulan,StartBalance,Debit,Credit,EndBalance from @LK  
                                            where Bulan = 'May'        
                                            ) E On A.ID = E.ID and A.Name = E.Name and A.Groups = E.Groups and A.ParentPK = E.ParentPK and A.CCY = E.CCY
                                            LEFT JOIN             
                                            (            
                                            select ID,Name,Groups,ParentPK,CCY,Bulan,StartBalance,Debit,Credit,EndBalance from @LK  
                                            where Bulan = 'June'        
                                            ) F On A.ID = F.ID and A.Name = F.Name and A.Groups = F.Groups and A.ParentPK = F.ParentPK and A.CCY = F.CCY
                                            LEFT JOIN             
                                            (            
                                            select ID,Name,Groups,ParentPK,CCY,Bulan,StartBalance,Debit,Credit,EndBalance from @LK  
                                            where Bulan = 'July'        
                                            ) G On A.ID = G.ID and A.Name = G.Name and A.Groups = G.Groups and A.ParentPK = G.ParentPK and A.CCY = G.CCY
                                            LEFT JOIN             
                                            (            
                                            select ID,Name,Groups,ParentPK,CCY,Bulan,StartBalance,Debit,Credit,EndBalance from @LK  
                                            where Bulan = 'August'        
                                            ) H On A.ID = H.ID and A.Name = H.Name and A.Groups = H.Groups and A.ParentPK = H.ParentPK and A.CCY = H.CCY
                                            LEFT JOIN             
                                            (            
                                            select ID,Name,Groups,ParentPK,CCY,Bulan,StartBalance,Debit,Credit,EndBalance from @LK  
                                            where Bulan = 'September'        
                                            ) I On A.ID = I.ID and A.Name = I.Name and A.Groups = I.Groups and A.ParentPK = I.ParentPK and A.CCY = I.CCY
                                            LEFT JOIN             
                                            (            
                                            select ID,Name,Groups,ParentPK,CCY,Bulan,StartBalance,Debit,Credit,EndBalance from @LK  
                                            where Bulan = 'October'        
                                            ) J On A.ID = J.ID and A.Name = J.Name and A.Groups = J.Groups and A.ParentPK = J.ParentPK and A.CCY = J.CCY
                                            LEFT JOIN             
                                            (            
                                            select ID,Name,Groups,ParentPK,CCY,Bulan,StartBalance,Debit,Credit,EndBalance from @LK  
                                            where Bulan = 'November'        
                                            ) K On A.ID = K.ID and A.Name = K.Name and A.Groups = K.Groups and A.ParentPK = K.ParentPK and A.CCY = K.CCY
                                            LEFT JOIN             
                                            (            
                                            select ID,Name,Groups,ParentPK,CCY,Bulan,StartBalance,Debit,Credit,EndBalance from @LK  
                                            where Bulan = 'December'        
                                            ) L On A.ID = L.ID and A.Name = L.Name and A.Groups = L.Groups and A.ParentPK = L.ParentPK and A.CCY = L.CCY

                                            where A.Bulan = 'January'

                                            group by A.ID,A.Name,A.Groups,A.ParentPK,A.CCY,A.Bulan,A.StartBalance,A.EndBalance,B.Bulan,B.StartBalance,B.EndBalance,
                                            C.Bulan,C.StartBalance,C.EndBalance,D.Bulan,D.StartBalance,D.EndBalance,E.Bulan,E.StartBalance,E.EndBalance,
                                            F.Bulan,F.StartBalance,F.EndBalance,G.Bulan,G.StartBalance,G.EndBalance,H.Bulan,H.StartBalance,H.EndBalance,
                                            I.Bulan,I.StartBalance,I.EndBalance,J.Bulan,J.StartBalance,J.EndBalance,K.Bulan,K.StartBalance,K.EndBalance,
                                            L.Bulan,L.StartBalance,L.EndBalance          
                                                ";
                                                subCmd7.CommandTimeout = 0;
                                                subCmd7.Parameters.AddWithValue("@ValueDateFrom", _accountingRpt.ValueDateFrom);
                                                subCmd7.Parameters.AddWithValue("@ValueDateTo", _accountingRpt.ValueDateTo);
                                                using (SqlDataReader dr7 = subCmd7.ExecuteReader())
                                                {
                                                    if (!dr7.HasRows)
                                                    {
                                                        return false;
                                                    }
                                                    else
                                                    {
                                                        ExcelWorksheet worksheet7 = package.Workbook.Worksheets[7];

                                                        int incRowExcel = 2;
                                                        int _startRowDetail = incRowExcel;
                                                        int _endRowDetail = 0;
                                                        int _endColDetail = 0;

                                                        while (dr7.Read())
                                                        {
                                                            int incColExcel = 1;
                                                            for (int inc1 = 0; inc1 < dr7.FieldCount; inc1++)
                                                            {
                                                                worksheet7.Cells[1, incColExcel].Value = dr7.GetName(inc1);
                                                                worksheet7.Cells[1, incColExcel].Style.Font.Bold = true;
                                                                worksheet7.Cells[1, incColExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                                worksheet7.Cells[incRowExcel, incColExcel].Style.Font.Size = Tools.DefaultReportFontSize();


                                                                worksheet7.Cells[incRowExcel, incColExcel].Value = (dr7.GetValue(inc1));
                                                                worksheet7.Column(incColExcel).Width = 15;

                                                                incColExcel++;
                                                            }
                                                            _endColDetail = incColExcel - 1;
                                                            _endRowDetail = incRowExcel - 1;


                                                            incRowExcel++;
                                                        }

                                                    }
                                                }
                                            }
                                        }

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

            #region Account Activity
            else if (_accountingRpt.ReportName.Equals("Account Activity"))
            {
                try
                {
                    using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                    {
                        DbCon.Open();
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            string _status = "";
                            string _paramAccount = "";
                            string _paramOffice = "";
                            string _paramDepartment = "";
                            string _paramAgent = "";
                            string _paramConsignee = "";
                            string _paramInstrument = "";
                            DateTime _compareDate = Convert.ToDateTime("10/28/2015");

                            if (!_host.findString(_accountingRpt.AccountFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_accountingRpt.AccountFrom))
                            {
                                _paramAccount = "And B.AccountPK  in ( " + _accountingRpt.AccountFrom + " ) ";
                            }
                            else
                            {
                                _paramAccount = "";
                            }

                            if (!_host.findString(_accountingRpt.OfficeFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_accountingRpt.OfficeFrom))
                            {
                                _paramOffice = "And B.OfficePK  in ( " + _accountingRpt.OfficeFrom + " ) ";
                            }
                            else
                            {
                                _paramOffice = "";
                            }

                            if (!_host.findString(_accountingRpt.DepartmentFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_accountingRpt.DepartmentFrom))
                            {
                                _paramDepartment = "And B.DepartmentPK  in ( " + _accountingRpt.DepartmentFrom + " ) ";
                            }
                            else
                            {
                                _paramDepartment = "";
                            }

                            if (!_host.findString(_accountingRpt.AgentFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_accountingRpt.AgentFrom))
                            {
                                _paramAgent = "And B.AgentPK  in ( " + _accountingRpt.AgentFrom + " ) ";
                            }
                            else
                            {
                                _paramAgent = "";
                            }


                            if (!_host.findString(_accountingRpt.ConsigneeFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_accountingRpt.ConsigneeFrom))
                            {
                                _paramConsignee = "And B.ConsigneePK  in ( " + _accountingRpt.ConsigneeFrom + " ) ";
                            }
                            else
                            {
                                _paramConsignee = "";
                            }


                            if (!_host.findString(_accountingRpt.InstrumentFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_accountingRpt.InstrumentFrom))
                            {
                                _paramInstrument = "And B.InstrumentPK  in ( " + _accountingRpt.InstrumentFrom + " ) ";
                            }
                            else
                            {
                                _paramInstrument = "";
                            }

                            if (_accountingRpt.Status == 1)
                            {
                                _status = " and A.Status = 2 and A.Posted = 1 and A.Reversed = 0 ";
                            }
                            else if (_accountingRpt.Status == 2)
                            {
                                _status = " and A.Status = 2 and A.Posted = 1 and A.Reversed = 1 ";
                            }
                            else if (_accountingRpt.Status == 3)
                            {
                                _status = " and A.Status = 2 and A.Posted = 0 and A.Reversed = 0 ";
                            }
                            else if (_accountingRpt.Status == 4)
                            {
                                _status = " and A.Status = 1  ";
                            }
                            else if (_accountingRpt.Status == 5)
                            {
                                _status = " and (A.Status = 2 or A.Posted = 1) and A.Reversed = 0 and A.status not in (3,4)";
                            }
                            else if (_accountingRpt.Status == 6)
                            {
                                _status = " and (A.Status = 1 Or A.Status = 2 or A.Posted = 1) and A.Reversed = 0 and A.status not in (3,4) ";
                            }
                            cmd.CommandText = @"
	                        Declare @PeriodPK int
                            Select @PeriodPK = PeriodPK from Period where @ValueDateFrom between DateFrom and DateTo and status = 2

                            select C.Type AccountType,A.JournalPK,A.ValueDate,A.Reference,C.ID AccountID,C.Name AccountName,    
                              isnull(D.ID,'') CurrencyID,isnull(E.ID,'') OfficeID,isnull(F.ID,'') DepartmentID,isnull(G.ID,'') AgentID,isnull(H.Name,'') ConsigneeName,    
                              isnull(I.ID,'') InstrumentID,B.DetailDescription,B.DebitCredit,B.Amount,B.Debit,B.Credit,B.CurrencyRate Rate,    
                              B.BaseDebit,B.BaseCredit,[dbo].[FGetStartAccountBalance](@ValueDateFrom,B.AccountPK) StartBalance ,    
                              case when Reference = '' or Reference is null then 0 else cast(substring(reference,1,charindex('/',reference,1) - 1) as integer) end RefNo       
                              from journal A    
                              left join journalDetail B on A.JournalPK = B.JournalPK    
                              left join Account C on B.AccountPK = C.AccountPK and C.status in (1,2)    
                              left join Currency D on B.CurrencyPK = D.CurrencyPK and D.status in (1,2)    
                              left join Office E on B.OfficePK = E.OfficePK and E.status in (1,2)    
                              left join Department F on B.DepartmentPK = F.DepartmentPK and F.status in (1,2)    
                              left join Agent G on B.AgentPK = G.AgentPK and G.status in (1,2)    
                              left join Consignee H on B.consigneePK = H.ConsigneePK and H.status in (1,2)    
                              left join Instrument I on B.InstrumentPK = I.InstrumentPK and I.status in (1,2)    
                              Where A.ValueDate Between @ValueDateFrom and @ValueDateTo 
                              and A.PeriodPK = @PeriodPK 
                             " + _status + _paramAccount + _paramOffice + _paramDepartment + _paramAgent + _paramConsignee + _paramInstrument;
                            cmd.CommandTimeout = 0;
                            cmd.Parameters.AddWithValue("@ValueDateFrom", _accountingRpt.ValueDateFrom);
                            cmd.Parameters.AddWithValue("@ValueDateTo", _accountingRpt.ValueDateTo);
                            cmd.Parameters.AddWithValue("@AccountFrom", _accountingRpt.AccountFrom);
                            cmd.Parameters.AddWithValue("@OfficeFrom", _accountingRpt.OfficeFrom);
                            cmd.Parameters.AddWithValue("@DepartmentFrom", _accountingRpt.DepartmentFrom);
                            cmd.Parameters.AddWithValue("@AgentFrom", _accountingRpt.AgentFrom);
                            cmd.Parameters.AddWithValue("@ConsigneeFrom", _accountingRpt.ConsigneeFrom);
                            cmd.Parameters.AddWithValue("@InstrumentFrom", _accountingRpt.InstrumentFrom);

                            using (SqlDataReader dr0 = cmd.ExecuteReader())
                            {
                                if (!dr0.HasRows)
                                {
                                    return false;
                                }
                                else
                                {
                                    string filePath = Tools.ReportsPath + "AccountActivity" + "_" + _userID + ".xlsx";
                                    string pdfPath = Tools.ReportsPath + "AccountActivity" + "_" + _userID + ".pdf";
                                    FileInfo excelFile = new FileInfo(filePath);
                                    if (excelFile.Exists)
                                    {
                                        excelFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                        excelFile = new FileInfo(filePath);
                                    }

                                    // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                    using (ExcelPackage package = new ExcelPackage(excelFile))
                                    {
                                        package.Workbook.Properties.Title = "AccountingReport";
                                        package.Workbook.Properties.Author = Tools.DefaultReportAuthor();
                                        package.Workbook.Properties.Comments = Tools.DefaultReportComments();
                                        package.Workbook.Properties.Company = Tools.DefaultReportCompany();
                                        package.Workbook.Properties.SetCustomPropertyValue("Checked by", _userID);
                                        package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", Tools.DefaultReportAssemblyName());

                                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Account Activity");


                                        //ATUR DATA GROUPINGNYA DULU
                                        List<AccountActivity> rList = new List<AccountActivity>();
                                        while (dr0.Read())
                                        {
                                            AccountActivity rSingle = new AccountActivity();
                                            rSingle.AccountType = dr0["AccountType"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt16(dr0["AccountType"]);
                                            rSingle.StartBalance = dr0["StartBalance"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["StartBalance"]);
                                            rSingle.journalPK = dr0["journalPK"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr0["journalPK"]);
                                            rSingle.ValueDate = Convert.ToDateTime(dr0["ValueDate"]);
                                            rSingle.Reference = dr0["Reference"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["Reference"]);
                                            rSingle.RefNo = dr0["RefNo"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr0["RefNo"]);
                                            rSingle.DetailDescription = dr0["DetailDescription"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["DetailDescription"]);
                                            rSingle.AccountID = dr0["AccountID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["AccountID"]);
                                            rSingle.AccountName = dr0["AccountName"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["AccountName"]);
                                            rSingle.CurrencyID = dr0["CurrencyID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["CurrencyID"]);
                                            rSingle.DebitCredit = dr0["DebitCredit"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["DebitCredit"]);
                                            rSingle.Amount = dr0["Amount"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["Amount"]);
                                            rSingle.Debit = dr0["Debit"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["Debit"]);
                                            rSingle.Credit = dr0["Credit"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["Credit"]);
                                            rSingle.Rate = dr0["Rate"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["Rate"]);
                                            rSingle.BaseDebit = dr0["BaseDebit"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["BaseDebit"]);
                                            rSingle.BaseCredit = dr0["BaseCredit"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["BaseCredit"]);
                                            rSingle.OfficeID = dr0["OfficeID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["OfficeID"]);
                                            rSingle.DepartmentID = dr0["DepartmentID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["DepartmentID"]);
                                            rSingle.AgentID = dr0["AgentID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["AgentID"]);
                                            rSingle.ConsigneeName = dr0["ConsigneeName"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["ConsigneeName"]);
                                            rSingle.InstrumentID = dr0["InstrumentID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["InstrumentID"]);
                                            rList.Add(rSingle);

                                        }

                                        var GroupByAccountID =
                                            from r in rList
                                            orderby r.AccountID, r.ValueDate, r.RefNo ascending
                                            group r by new { r.AccountID, r.AccountName, r.CurrencyID, r.StartBalance } into rGroup
                                            select rGroup;

                                        int incRowExcel = 0;
                                        int _rowEndBalance = 0;
                                        foreach (var rsHeader in GroupByAccountID)
                                        {
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 2].Value = "ACC : ";
                                            worksheet.Cells["C" + incRowExcel + ":E" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 3].Style.WrapText = true;
                                            worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 3].Value = rsHeader.Key.AccountID + "-" + rsHeader.Key.AccountName;
                                            worksheet.Cells[incRowExcel, 6].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 6].Value = "DATEFROM :";
                                            worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "dd/MMM/yyyy";
                                            worksheet.Cells[incRowExcel, 7].Value = _accountingRpt.ValueDateFrom;
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 2].Value = "CURR ID : ";
                                            worksheet.Cells[incRowExcel, 3].Value = rsHeader.Key.CurrencyID;
                                            worksheet.Cells[incRowExcel, 6].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 6].Value = "DATETO :";
                                            worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "dd/MMM/yyyy";
                                            worksheet.Cells[incRowExcel, 7].Value = _accountingRpt.ValueDateTo;
                                            worksheet.Cells[incRowExcel, 9].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 9].Value = "BEG BALANCE :";
                                            worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0.00";
                                            worksheet.Cells[incRowExcel, 10].Value = rsHeader.Key.StartBalance;
                                            _rowEndBalance = incRowExcel;

                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Value = "NO";
                                            worksheet.Cells[incRowExcel, 2].Value = "DATE";
                                            worksheet.Cells[incRowExcel, 3].Value = "REF";
                                            worksheet.Cells[incRowExcel, 4].Value = "DESC";
                                            worksheet.Cells[incRowExcel, 5].Value = "DEPT";
                                            worksheet.Cells[incRowExcel, 6].Value = "INST";
                                            worksheet.Cells[incRowExcel, 7].Value = "PROJECT";//10
                                            worksheet.Cells[incRowExcel, 8].Value = "BASE DEBIT"; //7 
                                            worksheet.Cells[incRowExcel, 9].Value = "BASE CREDIT"; //8
                                            worksheet.Cells[incRowExcel, 10].Value = "BALANCE"; //9
                                        
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

                                            _range = "A" + incRowExcel + ":J" + incRowExcel;
                                            using (ExcelRange r = worksheet.Cells[_range]) // KALO  KOLOM 1 SAMPE 9 A-I
                                            {
                                                //NILAINYA NGAMBIL DARI DEFAULT DI TOOLS, KLO MAU BEDA SENDIRI BOLEH2 AJA.
                                                r.Style.Font.Color.SetColor(Tools.DefaultReportColumnHeaderFontColor());
                                                r.Style.HorizontalAlignment = Tools.DefaultReportColumnHeaderHorizontalAlignment();
                                                r.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                                r.Style.Fill.BackgroundColor.SetColor(Tools.DefaultReportColumnHeaderBackgroundColor());
                                                r.Style.Font.Size = Tools.DefaultReportColumnHeaderFontSize();
                                                r.Style.Font.Bold = Tools.DefaultReportColumnHeaderFontBold();
                                                //r.Style.Border.Top.Style = Tools.DefaultReportColumnHeaderBorderTop();
                                                r.Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                r.Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            }
                                            incRowExcel++;
                                            int _no = 1;

                                            int _startRowDetail = incRowExcel;
                                            //int _endRowDetail = 0;
                                            //end area header

                                            foreach (var rsDetail in rsHeader)
                                            {

                                                _range = "A" + incRowExcel + ":J" + incRowExcel;
                                                using (ExcelRange r = worksheet.Cells[_range]) // KALO  KOLOM 1 SAMPE 9 A-I
                                                {
                                                    r.Style.Font.Size = 11;
                                                    r.Style.Border.Bottom.Style = ExcelBorderStyle.Dashed;
                                                }
                                                //area detail

                                                worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 1].Value = _no;
                                                worksheet.Cells[incRowExcel, 2].Style.Numberformat.Format = "dd/MMM/yyyy";
                                                worksheet.Cells[incRowExcel, 2].Value = rsDetail.ValueDate;
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.Reference;
                                                worksheet.Cells[incRowExcel, 4].Value = rsDetail.DetailDescription;
                                                worksheet.Cells[incRowExcel, 4].Style.WrapText = true;
                                                worksheet.Cells[incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 5].Value = rsDetail.DepartmentID;
                                                worksheet.Cells[incRowExcel, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 6].Value = rsDetail.InstrumentID;
                                                worksheet.Cells[incRowExcel, 8].Value = rsDetail.BaseDebit;
                                                worksheet.Cells[incRowExcel, 9].Value = rsDetail.BaseCredit;
                                                if (rsDetail.AccountType == 1 || rsDetail.AccountType == 4)
                                                {
                                                    worksheet.Cells[incRowExcel, 10].Formula = "J" + _rowEndBalance + "+H" + incRowExcel + "-I" + incRowExcel;
                                                }
                                                else
                                                {
                                                    worksheet.Cells[incRowExcel, 10].Formula = "J" + _rowEndBalance + "-H" + incRowExcel + "+I" + incRowExcel;
                                                }
                                                if (_accountingRpt.DecimalPlaces == 0)
                                                {
                                                    worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0";
                                                    worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0";
                                                    worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0";
                                                }
                                                else if (_accountingRpt.DecimalPlaces == 2)
                                                {
                                                    worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0.00";
                                                    worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0.00";
                                                    worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0.00";
                                                }
                                                else if (_accountingRpt.DecimalPlaces == 4)
                                                {
                                                    worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0.0000";
                                                    worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0.0000";
                                                    worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0.0000";
                                                }
                                                else if (_accountingRpt.DecimalPlaces == 6)
                                                {
                                                    worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0.000000";
                                                    worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0.000000";
                                                    worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0.000000";
                                                }
                                                else
                                                {
                                                    worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0.00000000";
                                                    worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0.00000000";
                                                    worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0.00000000";
                                                }
                                                worksheet.Cells[incRowExcel, 10].Calculate();
                                                worksheet.Cells[incRowExcel, 7].Value = rsDetail.ConsigneeName;
                                                _rowEndBalance = incRowExcel;

                                                incRowExcel++;
                                                _range = "A" + incRowExcel + ":J" + incRowExcel;
                                                using (ExcelRange r = worksheet.Cells[_range]) // KALO  KOLOM 1 SAMPE 9 A-I
                                                {
                                                    r.Style.Font.Size = 11;
                                                }
                                                _no++;


                                            }
                                            incRowExcel = incRowExcel + 2;
                                            worksheet.Row(incRowExcel).PageBreak = _accountingRpt.PageBreak;

                                        }
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
                                        worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel, 10];
                                        worksheet.Column(3).Width = 15;
                                        worksheet.Column(4).Width = 30;
                                        worksheet.Column(1).Width = 5;
                                        worksheet.Column(10).Width = 1;
                                        worksheet.Column(2).AutoFit();
                                        worksheet.Column(3).AutoFit();
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
                                        worksheet.HeaderFooter.OddHeader.CenteredText = "&14 ACCOUNT ACTIVITY";

                                        Image img = Image.FromFile(Tools.ReportImage);
                                        worksheet.HeaderFooter.OddHeader.InsertPicture(img, PictureAlignment.Left);

                                        if (_accountingRpt.ValueDateTo <= _compareDate)
                                        {
                                            worksheet.HeaderFooter.OddHeader.LeftAlignedText = Tools.DefaultReportHeaderLeftBatchReport();
                                        }
                                        else
                                        {
                                            //worksheet.HeaderFooter.OddHeader.LeftAlignedText = Tools.DefaultReportHeaderLeftText();
                                        }

                                        worksheet.HeaderFooter.OddFooter.RightAlignedText =
                                        string.Format("Page {0} of {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);

                                        worksheet.HeaderFooter.OddFooter.CenteredText = Tools.DefaultReportFooterCenterText();
                                        worksheet.HeaderFooter.OddFooter.LeftAlignedText = Tools.DefaultReportFooterLeftText();

                                        package.Save();
                                        if (_accountingRpt.DownloadMode == "PDF")
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

        public Boolean Settlement_ListingRpt(string _userID, InvestmentListing _listing)
        {
            if (_listing.ParamInstType == "2")
            {
                try
                {
                    using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                    {
                        DbCon.Open();
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {

                            string _paramFund = "";
                            string _ParamTrxType = "";

                            if (_listing.ParamFundID != "All")
                            {
                                _paramFund = "and F.ID = left(@ParamFundIDFrom,charindex('-',@ParamFundIDFrom) - 1)";
                            }
                            else
                            {
                                _paramFund = "";
                            }
                            if (_listing.ParamTrxType == "1")
                            {
                                _ParamTrxType = "1";
                            }
                            else
                            {
                                _ParamTrxType = "2";
                            }

                            cmd.CommandText = @"Select Reference, cast(substring(reference,1,charindex('/',reference,1) - 1) as integer) RefNo,MV.DescOne SettlementModeDesc,MV.DescTwo SettlementModeDescTwo,BB.Name BankCustodianName,BC.ContactPerson BankCustodianContactPerson,BC.Fax1 BankCustodianFaxNo,BC.Phone1 BankCustodianPhone,C.ContactPerson,C.Fax FaxNo,C.Name CounterpartName,I.Name InstrumentName,F.Name FundName,IV.EntryUsersID CheckedBy,IV.ApprovedUsersID ApprovedBy,IV.valueDate,I.ID InstrumentID,   
                                            F.ID FundID,IT.Name InstrumentType,isnull( AcqDate1,'') AcqDate1,isnull(AcqDate2,'') AcqDate2,isnull(AcqDate3,'') AcqDate3,isnull(AcqDate4,'') AcqDate4,isnull(AcqDate5,'') AcqDate5,IV.*  
                                            from Investment IV   
                                            left join Counterpart C on IV.CounterpartPK = C.CounterpartPK and C.status = 2  
                                            left join Fund F on IV.FundPK = F.FundPK and F.status = 2  
                                            left join FundCashRef FC on IV.FundCashRefPK = FC.FundCashRefPK and FC.status = 2 
                                            left join BankBranch BC on BC.BankBranchPK = F.BankBranchPK and BC.status = 2  
                                            left join Bank BB on BB.BankPK = BC.BankPK and BB.status = 2 
                                            left join Instrument I on IV.InstrumentPK = I.InstrumentPK and I.status = 2  
                                            left join MasterValue MV on IV.SettlementMode = MV.Code and MV.ID ='SettlementMode' and MV.status = 2  
                                            left join InstrumentType IT on IV.InstrumentTypePK = IT.InstrumentTypePK and IT.status = 2   
                                            Where  IV.ValueDate = @ParamListDate and IV.InstrumentTypePK in (2,3,9,13,15)  and IV.statusSettlement = 2 and IV.TrxType =
                                            " + _ParamTrxType + _paramFund + @" order by RefNo ";

                            cmd.Parameters.AddWithValue("@ParamListDate", _listing.ParamListDate);
                            if (_listing.ParamFundID != "All")
                            {
                                cmd.Parameters.AddWithValue("@ParamFundIDFrom", _listing.ParamFundID);
                            }
                            cmd.Parameters.AddWithValue("@ParamInstType", _listing.ParamInstType);
                            if (_listing.ParamTrxType == "1")
                            {
                                cmd.Parameters.AddWithValue("@ParamTrxType", _listing.ParamTrxType);
                            }
                            using (SqlDataReader dr0 = cmd.ExecuteReader())
                            {
                                if (!dr0.HasRows)
                                {
                                    return false;
                                }
                                else
                                {
                                    string filePath = Tools.ReportsPath + "SettlementListingBond" + "_" + _listing.ParamListDate.ToString().Replace("/", "-") + "_" + _userID + ".xlsx";
                                    string pdfPath = Tools.ReportsPath + "SettlementListingBond" + "_" + _listing.ParamListDate.ToString().Replace("/", "-") + "_" + _userID + ".pdf";
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

                                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Settlement Listing Bond");


                                        //ATUR DATA GROUPINGNYA DULU
                                        List<InvestmentListing> rList = new List<InvestmentListing>();
                                        while (dr0.Read())
                                        {
                                            InvestmentListing rSingle = new InvestmentListing();
                                            rSingle.FundName = dr0["FundName"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["FundName"]);
                                            rSingle.TrxTypeID = dr0["TrxTypeID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["TrxTypeID"]);
                                            rSingle.InstructionDate = Convert.ToDateTime(dr0["InstructionDate"]);
                                            rSingle.InstrumentID = dr0["InstrumentID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["InstrumentID"]);
                                            rSingle.InstrumentName = dr0["InstrumentName"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["InstrumentName"]);
                                            rSingle.MaturityDate = Convert.ToDateTime(dr0["MaturityDate"]);
                                            rSingle.Amount = dr0["Amount"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["Amount"]);
                                            rSingle.DoneVolume = dr0["DoneVolume"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["DoneVolume"]);
                                            rSingle.DonePrice = dr0["DonePrice"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["DonePrice"]);
                                            rSingle.DoneAmount = dr0["DoneAmount"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["DoneAmount"]);
                                            rSingle.InterestPercent = dr0["InterestPercent"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["InterestPercent"]);
                                            rSingle.CounterpartName = dr0["CounterpartName"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["CounterpartName"]);
                                            rSingle.BankCustodianName = dr0["BankCustodianName"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["BankCustodianName"]);
                                            rSingle.BankCustodianContactPerson = dr0["BankCustodianContactPerson"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["BankCustodianContactPerson"]);
                                            rSingle.BankCustodianFaxNo = dr0["BankCustodianFaxNo"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["BankCustodianFaxNo"]);
                                            rSingle.BankCustodianPhone = dr0["BankCustodianPhone"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["BankCustodianPhone"]);
                                            rSingle.ValueDate = Convert.ToDateTime(dr0["ValueDate"]);
                                            rSingle.LastCouponDate = Convert.ToDateTime(dr0["LastCouponDate"]);
                                            rSingle.SettlementDate = Convert.ToDateTime(dr0["SettlementDate"]);
                                            rSingle.ContactPerson = Convert.ToString(dr0["ContactPerson"]);
                                            rSingle.Reference = Convert.ToString(dr0["Reference"]);
                                            rSingle.FaxNo = Convert.ToString(dr0["FaxNo"]);
                                            rSingle.AcqPrice = dr0["AcqPrice"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["AcqPrice"]);
                                            rSingle.AcqDate = Convert.ToDateTime(dr0["AcqDate"]);
                                            rSingle.AcqPrice1 = dr0["AcqPrice1"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["AcqPrice1"]);
                                            rSingle.AcqDate1 = Convert.ToDateTime(dr0["AcqDate1"]);
                                            rSingle.AcqPrice2 = dr0["AcqPrice2"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["AcqPrice2"]);
                                            rSingle.AcqDate2 = Convert.ToDateTime(dr0["AcqDate2"]);
                                            rSingle.AcqPrice3 = dr0["AcqPrice3"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["AcqPrice3"]);
                                            rSingle.AcqDate3 = Convert.ToDateTime(dr0["AcqDate3"]);
                                            rSingle.AcqPrice4 = dr0["AcqPrice4"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["AcqPrice4"]);
                                            rSingle.AcqDate4 = Convert.ToDateTime(dr0["AcqDate4"]);
                                            rSingle.AcqPrice5 = dr0["AcqPrice5"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["AcqPrice5"]);
                                            rSingle.AcqDate5 = Convert.ToDateTime(dr0["AcqDate5"]);
                                            rSingle.IncomeTaxGainAmount = Convert.ToDecimal(dr0["IncomeTaxGainAmount"]);
                                            rSingle.TotalAmount = Convert.ToDecimal(dr0["TotalAmount"]);
                                            rSingle.AccruedInterest = dr0["DoneAccruedInterest"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["DoneAccruedInterest"]);
                                            rSingle.SettlementModeDesc = dr0["SettlementModeDesc"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["SettlementModeDesc"]);
                                            rSingle.SettlementModeDescTwo = dr0["SettlementModeDescTwo"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["SettlementModeDescTwo"]);
                                            //rSingle.TaxCapitalGainLoss = Convert.ToDecimal(dr0["TaxCapitalGainLoss"]);
                                            //rSingle.NetAmount = Convert.ToDecimal(dr0["NetAmount"]);
                                            rList.Add(rSingle);

                                        }

                                        var QueryByFundID =
                                             from r in rList
                                             group r by new { r.FundName, r.InstrumentID, r.TrxTypeID } into rGroup
                                             select rGroup;

                                        int incRowExcel = 0;

                                        foreach (var rsHeader in QueryByFundID)
                                        {

                                            //int _rowHeader = incRowExcel;
                                            int _rowLine1 = 0;
                                            int _rowLine2 = 0;

                                            int _endRowDetail = incRowExcel;

                                            //incRowExcel++;

                                            //area header

                                            foreach (var rsDetail in rsHeader)
                                            {
                                                //Row D = 5
                                                incRowExcel = incRowExcel + 5;
                                                int RowE = incRowExcel + 1;
                                                decimal _purchaseAmount = rsDetail.Amount;
                                                decimal _recalNetProceeds = rsDetail.TotalAmount;
                                                incRowExcel++;

                                                //area detail
                                                worksheet.Cells[incRowExcel, 1].Value = "Date ";
                                                worksheet.Cells[incRowExcel, 2].Value = ":";
                                                worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "dddd" + ",dd-MMM-yyyy";
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.InstructionDate;
                                                worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                worksheet.Cells["C" + incRowExcel + ":G" + incRowExcel].Merge = true;
                                                _rowLine1 = incRowExcel;
                                                incRowExcel++;

                                                worksheet.Cells[incRowExcel, 1].Value = "To ";
                                                worksheet.Cells[incRowExcel, 2].Value = ":";
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.BankCustodianName;
                                                worksheet.Cells["C" + incRowExcel + ":G" + incRowExcel].Merge = true;
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = "Attention ";
                                                worksheet.Cells[incRowExcel, 2].Value = ":";
                                                worksheet.Cells["C" + incRowExcel + ":G" + incRowExcel].Merge = true;
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.BankCustodianContactPerson;
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = "Fax No ";
                                                worksheet.Cells[incRowExcel, 2].Value = ":";
                                                worksheet.Cells["C" + incRowExcel + ":G" + incRowExcel].Merge = true;
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.BankCustodianFaxNo;
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = "From ";
                                                worksheet.Cells[incRowExcel, 2].Value = ":";
                                                worksheet.Cells["C" + incRowExcel + ":G" + incRowExcel].Merge = true;
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.FundName;
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 3].Value = _host.Get_CompanyName();
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = "Reference ";
                                                worksheet.Cells[incRowExcel, 2].Value = ":";
                                                worksheet.Cells["C" + incRowExcel + ":G" + incRowExcel].Merge = true;
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.Reference;
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = "Re ";
                                                worksheet.Cells[incRowExcel, 2].Value = ":";
                                                worksheet.Cells["C" + incRowExcel + ":G" + incRowExcel].Merge = true;
                                                worksheet.Cells[incRowExcel, 3].Value = "Bond Transaction";
                                                _rowLine2 = incRowExcel;

                                                incRowExcel = incRowExcel + 2;
                                                worksheet.Cells[incRowExcel, 1].Value = "Dear Sir,";
                                                incRowExcel = incRowExcel + 2;
                                                worksheet.Cells["A" + incRowExcel + ":G" + incRowExcel].Merge = true;
                                                worksheet.Cells[incRowExcel, 1].Value = "Here with we would like to confirm having " + rsDetail.TrxTypeID +" bond transaction with following details :";
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = "Bond Name ";
                                                worksheet.Cells[incRowExcel, 2].Value = ":";
                                                worksheet.Cells["C" + incRowExcel + ":G" + incRowExcel].Merge = true;
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.InstrumentName;
                                                worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = "Bond Code ";
                                                worksheet.Cells[incRowExcel, 2].Value = ":";
                                                worksheet.Cells["C" + incRowExcel + ":G" + incRowExcel].Merge = true;
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.InstrumentID;
                                                worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = "Type of Transaction ";
                                                worksheet.Cells[incRowExcel, 2].Value = ":";
                                                worksheet.Cells["C" + incRowExcel + ":G" + incRowExcel].Merge = true;
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.SettlementModeDesc + "(" + rsDetail.SettlementModeDescTwo + ")";
                                                worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = "Maturity Date ";
                                                worksheet.Cells[incRowExcel, 2].Value = ":";
                                                worksheet.Cells["C" + incRowExcel + ":G" + incRowExcel].Merge = true;
                                                worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "dddd" + ",dd-MMM-yyyy";
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.MaturityDate;
                                                worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = "Nominal (IDR) ";
                                                worksheet.Cells[incRowExcel, 2].Value = ":";
                                                worksheet.Cells["C" + incRowExcel + ":G" + incRowExcel].Merge = true;
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.DoneVolume;
                                                worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = "Price (%) ";
                                                worksheet.Cells[incRowExcel, 2].Value = ":";
                                                worksheet.Cells["C" + incRowExcel + ":G" + incRowExcel].Merge = true;
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.DonePrice;
                                                worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "#,##0.0000";
                                                worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = "Current Coupon (%) ";
                                                worksheet.Cells[incRowExcel, 2].Value = ":";
                                                worksheet.Cells["C" + incRowExcel + ":G" + incRowExcel].Merge = true;
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.InterestPercent;
                                                worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "#,##0.0000";
                                                worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = "CounterParty ";
                                                worksheet.Cells[incRowExcel, 2].Value = ":";
                                                worksheet.Cells["C" + incRowExcel + ":G" + incRowExcel].Merge = true;
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.CounterpartName;
                                                worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                incRowExcel++;
                                                //worksheet.Cells[incRowExcel, 1].Value = "care of ";
                                                //worksheet.Cells[incRowExcel, 2].Value = ":";
                                                //worksheet.Cells["C" + incRowExcel + ":G" + incRowExcel].Merge = true;
                                                //worksheet.Cells[incRowExcel, 3].Value = rsDetail.CounterpartName;
                                                //worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                //incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = "Trade Date ";
                                                worksheet.Cells[incRowExcel, 2].Value = ":";
                                                worksheet.Cells["C" + incRowExcel + ":G" + incRowExcel].Merge = true;
                                                worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "dddd" + ",dd-MMM-yyyy";
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.ValueDate;
                                                worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = "Settlement Date ";
                                                worksheet.Cells[incRowExcel, 2].Value = ":";
                                                worksheet.Cells["C" + incRowExcel + ":G" + incRowExcel].Merge = true;
                                                worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "dddd" + ",dd-MMM-yyyy";
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.SettlementDate;
                                                worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                incRowExcel++;
                                                int RowF = incRowExcel;
                                                worksheet.Cells[incRowExcel, 1].Value = "Acq Price (%) ";
                                                worksheet.Cells[incRowExcel, 2].Value = ":";
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.AcqPrice;
                                                worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "#,##0.0000";
                                                worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = "Acq Date ";
                                                worksheet.Cells[incRowExcel, 2].Value = ":";
                                                worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "dddd" + ",dd-MMM-yyyy";
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.AcqDate;
                                                worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = "Last Coupon ";
                                                worksheet.Cells[incRowExcel, 2].Value = ":";
                                                worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "dddd" + ",dd-MMM-yyyy";
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.LastCouponDate;
                                                worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                incRowExcel++;

                                                if (rsDetail.AcqPrice1 != 0)
                                                {

                                                    worksheet.Cells[incRowExcel, 1].Value = "Acq Price 1 (%) ";
                                                    worksheet.Cells[incRowExcel, 2].Value = ":";
                                                    worksheet.Cells[incRowExcel, 3].Value = rsDetail.AcqPrice1;
                                                    worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "#,##0.0000";
                                                    worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                    incRowExcel++;
                                                    worksheet.Cells[incRowExcel, 1].Value = "Acq Date 1 ";
                                                    worksheet.Cells[incRowExcel, 2].Value = ":";
                                                    worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "dddd" + ",dd-MMM-yyyy";
                                                    worksheet.Cells[incRowExcel, 3].Value = rsDetail.AcqDate1;
                                                    worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                    incRowExcel++;
                                                }

                                                if (rsDetail.AcqPrice2 != 0)
                                                {

                                                    worksheet.Cells[incRowExcel, 1].Value = "Acq Price 2 (%) ";
                                                    worksheet.Cells[incRowExcel, 2].Value = ":";
                                                    worksheet.Cells[incRowExcel, 3].Value = rsDetail.AcqPrice2;
                                                    worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "#,##0.0000";
                                                    worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                    incRowExcel++;
                                                    worksheet.Cells[incRowExcel, 1].Value = "Acq Date 2 ";
                                                    worksheet.Cells[incRowExcel, 2].Value = ":";
                                                    worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "dddd" + ",dd-MMM-yyyy";
                                                    worksheet.Cells[incRowExcel, 3].Value = rsDetail.AcqDate2;
                                                    worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                    incRowExcel++;
                                                }
                                                if (rsDetail.AcqPrice3 != 0)
                                                {
                                                    worksheet.Cells["E" + RowF + ":E" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                    worksheet.Cells[RowF, 5].Value = "Acq Price 3 (%) ";
                                                    worksheet.Cells[RowF, 6].Value = ":";
                                                    worksheet.Cells[RowF, 7].Value = rsDetail.AcqPrice3;
                                                    worksheet.Cells[RowF, 7].Style.Numberformat.Format = "#,##0.0000";
                                                    worksheet.Cells[RowF, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                    RowF++;
                                                    worksheet.Cells["E" + RowF + ":E" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                    worksheet.Cells[RowF, 5].Value = "Acq Date 3 ";
                                                    worksheet.Cells[RowF, 6].Value = ":";
                                                    worksheet.Cells[RowF, 7].Style.Numberformat.Format = "dddd" + ",dd-MMM-yyyy";
                                                    worksheet.Cells[RowF, 7].Value = rsDetail.AcqDate3;
                                                    worksheet.Cells[RowF, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                    RowF++;

                                                }
                                                if (rsDetail.AcqPrice4 != 0)
                                                {
                                                    worksheet.Cells["E" + RowF + ":E" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                    worksheet.Cells[RowF, 5].Value = "Acq Price 4 (%) ";
                                                    worksheet.Cells[RowF, 6].Value = ":";
                                                    worksheet.Cells[RowF, 7].Value = rsDetail.AcqPrice4;
                                                    worksheet.Cells[RowF, 7].Style.Numberformat.Format = "#,##0.0000";
                                                    worksheet.Cells[RowF, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                    RowF++;
                                                    worksheet.Cells["E" + RowF + ":E" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                    worksheet.Cells[RowF, 5].Value = "Acq Date 4 ";
                                                    worksheet.Cells[RowF, 6].Value = ":";
                                                    worksheet.Cells[RowF, 7].Style.Numberformat.Format = "dddd" + ",dd-MMM-yyyy";
                                                    worksheet.Cells[RowF, 7].Value = rsDetail.AcqDate4;
                                                    worksheet.Cells[RowF, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                    RowF++;
                                                }
                                                if (rsDetail.AcqPrice5 != 0)
                                                {
                                                    worksheet.Cells["E" + RowF + ":E" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                    worksheet.Cells[RowF, 5].Value = "Acq Price 5 (%) ";
                                                    worksheet.Cells[RowF, 6].Value = ":";
                                                    worksheet.Cells[RowF, 7].Value = rsDetail.AcqPrice5;
                                                    worksheet.Cells[RowF, 7].Style.Numberformat.Format = "#,##0.0000";
                                                    worksheet.Cells[RowF, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                    RowF++;
                                                    worksheet.Cells["E" + RowF + ":E" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                    worksheet.Cells[RowF, 5].Value = "Acq Date 5 ";
                                                    worksheet.Cells[RowF, 6].Value = ":";
                                                    worksheet.Cells[RowF, 7].Style.Numberformat.Format = "dddd" + ",dd-MMM-yyyy";
                                                    worksheet.Cells[RowF, 7].Value = rsDetail.AcqDate5;
                                                    worksheet.Cells[RowF, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                    RowF++;
                                                }

                                                incRowExcel = incRowExcel + 2;
                                                worksheet.Cells[incRowExcel, 1].Value = "Purchase Amount (IDR) ";
                                                worksheet.Cells[incRowExcel, 2].Value = ":";
                                                worksheet.Cells[incRowExcel, 3].Value = _purchaseAmount;
                                                worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "#,##0";
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = "Interest Amount (IDR) ";
                                                worksheet.Cells[incRowExcel, 2].Value = ":";
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.AccruedInterest;
                                                worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "#,##0";
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = "Tax. Accru interest ";
                                                worksheet.Cells[incRowExcel, 2].Value = ":";
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.IncomeTaxInterestAmount;
                                                worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "#,##0";
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = "Tax. Capital Gain (IDR) ";
                                                worksheet.Cells[incRowExcel, 2].Value = ":";
                                                if (rsDetail.SettlementModeDesc == "RVP")
                                                {
                                                    worksheet.Cells[incRowExcel, 3].Value = rsDetail.IncomeTaxGainAmount * -1;
                                                }
                                                else
                                                {
                                                    worksheet.Cells[incRowExcel, 3].Value = rsDetail.IncomeTaxGainAmount;
                                                }

                                                worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells["A" + incRowExcel + ":C" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = "Net Proceeds (IDR) ";
                                                worksheet.Cells[incRowExcel, 2].Value = ":";
                                                worksheet.Cells[incRowExcel, 3].Value = _recalNetProceeds;
                                                worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "#,##0";
                                                incRowExcel = incRowExcel + 15;
                                                worksheet.Cells[incRowExcel, 1].Value = _host.Get_SignatureName(_listing.Signature1);
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Size = 16;
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 3].Value = _host.Get_SignatureName(_listing.Signature2);
                                                worksheet.Cells[incRowExcel, 3].Style.Font.Size = 16;
                                                worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 5].Value = _host.Get_SignatureName(_listing.Signature3);
                                                worksheet.Cells[incRowExcel, 5].Style.Font.Size = 16;
                                                worksheet.Cells[incRowExcel, 5].Style.Font.Bold = true;
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = _host.Get_PositionSignature(_listing.Signature1);
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Size = 16;
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Italic = true;
                                                worksheet.Cells[incRowExcel, 3].Value = _host.Get_PositionSignature(_listing.Signature2);
                                                worksheet.Cells[incRowExcel, 3].Style.Font.Size = 16;
                                                worksheet.Cells[incRowExcel, 3].Style.Font.Italic = true;
                                                worksheet.Cells[incRowExcel, 5].Value = _host.Get_PositionSignature(_listing.Signature3);
                                                worksheet.Cells[incRowExcel, 5].Style.Font.Size = 16;
                                                worksheet.Cells[incRowExcel, 5].Style.Font.Italic = true;
                                                incRowExcel = incRowExcel + 13;
                                                worksheet.Row(incRowExcel).PageBreak = true;
                                            }

                                            //incRowExcel++;


                                        }



                                        string _rangeA = "A1:G" + incRowExcel;
                                        using (ExcelRange r = worksheet.Cells[_rangeA]) // KALO  KOLOM 1 SAMPE 9 A-I
                                        {
                                            r.Style.Font.Size = 18;
                                            r.Style.Font.Bold = Tools.DefaultReportColumnHeaderFontBold();
                                        }


                                        // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                        worksheet.PrinterSettings.FitToPage = true;
                                        worksheet.PrinterSettings.FitToWidth = 1;
                                        worksheet.PrinterSettings.FitToHeight = 0;
                                        worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel, 7];
                                        worksheet.Column(1).Width = 35;
                                        worksheet.Column(2).Width = 4;
                                        worksheet.Column(3).Width = 35;
                                        worksheet.Column(4).Width = 4;
                                        worksheet.Column(5).Width = 35;
                                        worksheet.Column(6).Width = 4;
                                        worksheet.Column(7).Width = 35;
                                        worksheet.PrinterSettings.Orientation = eOrientation.Portrait; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                        // worksheet.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN
                                        // worksheet.PrinterSettings.FitToPage = true;
                                        //worksheet.HeaderFooter.OddHeader.RightAlignedText = "&14 PAYMENT VOUCHER";
                                        // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                        worksheet.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
                                        worksheet.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
                                        //worksheet.View.PageLayoutView = Tools.DefaultReportPageLayoutView();
                                        worksheet.View.ShowGridLines = Tools.DefaultReportShowGridLines();
                                        worksheet.HeaderFooter.OddHeader.CenteredText = "&27 SETTLEMENT LISTING BOND";

                                        Image img = Image.FromFile(Tools.ReportImage);
                                        worksheet.HeaderFooter.OddHeader.InsertPicture(img, PictureAlignment.Left);
                                        //worksheet.HeaderFooter.OddHeader.LeftAlignedText = Tools.DefaultReportHeaderLeftText();

                                        //worksheet.HeaderFooter.OddFooter.RightAlignedText =
                                        //string.Format("Page {0} of {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);

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

            else if (_listing.ParamInstType == "1")
            {
                try
                {
                    using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                    {
                        DbCon.Open();
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            string _paramFund = "";
                            string _ParamTrxType = "";

                            if (_listing.ParamFundID != "All")
                            {
                                _paramFund = "and F.ID = left(@ParamFundIDFrom,charindex('-',@ParamFundIDFrom) - 1)";
                            }
                            else
                            {
                                _paramFund = "";
                            }

                            if (_listing.ParamTrxType == "1")
                            {
                                _ParamTrxType = "1";
                            }
                            else
                            {
                                _ParamTrxType = "2";
                            }

                            cmd.CommandText = @" Select Reference, cast(substring(reference,1,charindex('/',reference,1) - 1) as integer) RefNo,C.Name CounterpartName,I.Name InstrumentName,F.Name FundName,IV.valueDate,I.ID InstrumentID,I.Name InstrumentName, 
                                            F.ID FundID,IT.Name InstrumentType,C.ID CounterpartID,BB.Name BankCustodianName,BC.ContactPerson ContactPerson,BC.Fax1 FaxNo,FC.BankAccountNo,FC.BankAccountName,IV.* 
                                            from Investment IV  
                                            left join Counterpart C on IV.CounterpartPK = C.CounterpartPK and C.status = 2  
                                            left join Fund F on IV.FundPK = F.FundPK and F.status = 2  
                                            left join FundCashRef FC on IV.FundCashRefPK = FC.FundCashRefPK and FC.status = 2 
                                            left join BankBranch BC on BC.BankBranchPK = F.BankBranchPK and BC.status = 2  
                                            left join Bank BB on BB.BankPK = BC.BankPK and BB.status = 2 
                                            left join Instrument I on IV.InstrumentPK = I.InstrumentPK and I.status = 2  
                                            left join MasterValue MV on IV.SettlementMode = MV.Code and MV.ID ='SettlementMode' and MV.status = 2  
                                            left join InstrumentType IT on IV.InstrumentTypePK = IT.InstrumentTypePK and IT.status = 2  
                                            Where  IV.ValueDate = @ParamListDate and IV.InstrumentTypePK = 1 and IV.statusSettlement = 2  and IV.TrxType = " + _ParamTrxType + _paramFund + @" order by Refno ";

                            cmd.Parameters.AddWithValue("@ParamListDate", _listing.ParamListDate);
                            if (_listing.ParamFundID != "All")
                            {
                                cmd.Parameters.AddWithValue("@ParamFundIDFrom", _listing.ParamFundID);
                            }
                            cmd.Parameters.AddWithValue("@ParamInstType", _listing.ParamInstType);
                            if (_listing.ParamTrxType == "1")
                            {
                                cmd.Parameters.AddWithValue("@ParamTrxType", _listing.ParamTrxType);
                            }

                            using (SqlDataReader dr0 = cmd.ExecuteReader())
                            {
                                if (!dr0.HasRows)
                                {
                                    return false;
                                }
                                else
                                {
                                    string filePath = Tools.ReportsPath + "SettlementListingEquity" + "_" + _listing.ParamListDate.ToString().Replace("/", "-") + "_" + _userID + ".xlsx";
                                    string pdfPath = Tools.ReportsPath + "SettlementListingEquity" + "_" + _listing.ParamListDate.ToString().Replace("/", "-") + "_" + _userID + ".pdf";
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

                                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Settlement Listing Equity");


                                        //ATUR DATA GROUPINGNYA DULU
                                        List<InvestmentListing> rList = new List<InvestmentListing>();
                                        while (dr0.Read())
                                        {
                                            InvestmentListing rSingle = new InvestmentListing();
                                            rSingle.FundName = dr0["FundName"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["FundName"]);
                                            rSingle.TrxTypeID = dr0["TrxTypeID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["TrxTypeID"]);
                                            rSingle.InstructionDate = Convert.ToDateTime(dr0["InstructionDate"]);
                                            rSingle.Reference = Convert.ToString(dr0["Reference"]);
                                            rSingle.FundID = dr0["FundID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["FundID"]);
                                            rSingle.InstrumentType = dr0["InstrumentType"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["InstrumentType"]);
                                            rSingle.CounterpartID = dr0["CounterpartID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["CounterpartID"]);
                                            rSingle.ContactPerson = dr0["ContactPerson"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["ContactPerson"]);
                                            rSingle.BankCustodianName = dr0["BankCustodianName"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["BankCustodianName"]);
                                            rSingle.FaxNo = dr0["FaxNo"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["FaxNo"]);
                                            rSingle.BankAccountName = dr0["BankAccountName"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["BankAccountName"]);
                                            rSingle.BankAccountNo = dr0["BankAccountNo"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["BankAccountNo"]);
                                            rSingle.InstrumentID = dr0["InstrumentID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["InstrumentID"]);
                                            rSingle.InstrumentName = dr0["InstrumentName"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["InstrumentName"]);
                                            rSingle.DoneVolume = dr0["DoneVolume"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["DoneVolume"]);
                                            rSingle.DoneAmount = dr0["DoneAmount"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["DoneAmount"]);
                                            rSingle.DonePrice = dr0["DonePrice"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["DonePrice"]);
                                            rSingle.InterestPercent = dr0["InterestPercent"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["InterestPercent"]);
                                            rSingle.CounterpartName = dr0["CounterpartName"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["CounterpartName"]);
                                            rSingle.ValueDate = Convert.ToDateTime(dr0["ValueDate"]);
                                            rSingle.SettlementDate = Convert.ToDateTime(dr0["SettlementDate"]);
                                            //rSingle.PurchaseAmount = Convert.ToDecimal(dr0["PurchaseAmount"]);
                                            rSingle.AccruedInterest = dr0["AccruedInterest"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["AccruedInterest"]);
                                            //rSingle.TaxCapitalGainLoss = Convert.ToDecimal(dr0["TaxCapitalGainLoss"]);
                                            rSingle.TotalAmount = Convert.ToDecimal(dr0["TotalAmount"]);
                                            rSingle.CommissionAmount = dr0["CommissionAmount"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["CommissionAmount"]);
                                            rSingle.LevyAmount = dr0["LevyAmount"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["LevyAmount"]);
                                            rSingle.KPEIAmount = dr0["KPEIAmount"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["KPEIAmount"]);
                                            rSingle.VATAmount = dr0["VATAmount"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["VATAmount"]);
                                            rSingle.WHTAmount = dr0["WHTAmount"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["WHTAmount"]);
                                            rSingle.IncomeTaxSellAmount = dr0["IncomeTaxSellAmount"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["IncomeTaxSellAmount"]);
                                            rList.Add(rSingle);

                                        }


                                        var GroupByValueDate =
                                               from r in rList
                                               group r by new { r.ValueDate, r.FundID, r.CounterpartID, r.InstrumentType, r.TrxTypeID, r.SettlementDate, r.ContactPerson, r.FaxNo, r.BankAccountNo, r.BankAccountName, r.BankCustodianName } into rGroup
                                               select rGroup;

                                        int incRowExcel = 0;


                                        foreach (var rsHeader in GroupByValueDate)
                                        {
                                            incRowExcel = incRowExcel + 7;
                                            //worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            //worksheet.Cells[incRowExcel, 1].Value = "REFERENCE";
                                            //worksheet.Cells[incRowExcel, 2].Value = rsHeader.Key.Reference;
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 1].Value = "Date ";
                                            worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 2].Value = ":";
                                            worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "dddd" + ",dd-MMM-yyyy";
                                            worksheet.Cells[incRowExcel, 3].Value = (rsHeader.Key.ValueDate);
                                            worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            worksheet.Cells["K" + incRowExcel + ":L" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 11].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 11].Value = "Securities Acc No ";
                                            worksheet.Cells[incRowExcel, 13].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 13].Value = ":";
                                            worksheet.Cells[incRowExcel, 13].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 14].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 14].Value = rsHeader.Key.BankAccountName;
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 1].Value = "To";
                                            worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 2].Value = ":";
                                            worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 3].Value = rsHeader.Key.BankCustodianName;
                                            worksheet.Cells["K" + incRowExcel + ":L" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 11].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 11].Value = "Cash Acc No ";
                                            worksheet.Cells[incRowExcel, 13].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 13].Value = ":";
                                            worksheet.Cells[incRowExcel, 13].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 14].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 14].Value = rsHeader.Key.BankAccountNo;
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 1].Value = "Attention ";
                                            worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 2].Value = ":";
                                            worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 3].Value = rsHeader.Key.ContactPerson;
                                            worksheet.Cells["K" + incRowExcel + ":L" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 11].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 11].Value = "Instrument Type ";
                                            worksheet.Cells[incRowExcel, 13].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 13].Value = ":";
                                            worksheet.Cells[incRowExcel, 13].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 14].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 14].Value = rsHeader.Key.InstrumentType;
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 1].Value = "Fax no ";
                                            worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 2].Value = ":";
                                            worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 3].Value = rsHeader.Key.FaxNo;
                                            worksheet.Cells["K" + incRowExcel + ":L" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 11].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 11].Value = "Trade Date ";
                                            worksheet.Cells[incRowExcel, 13].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 13].Value = ":";
                                            worksheet.Cells[incRowExcel, 13].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 14].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 14].Style.Numberformat.Format = "dddd" + ",dd-MMM-yyyy";
                                            worksheet.Cells[incRowExcel, 14].Value = (rsHeader.Key.ValueDate);
                                            worksheet.Cells[incRowExcel, 14].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 1].Value = "From ";
                                            worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 2].Value = ":";
                                            worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 3].Value = rsHeader.Key.FundID;
                                            worksheet.Cells["K" + incRowExcel + ":L" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 11].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 11].Value = "Settlement Date ";
                                            worksheet.Cells[incRowExcel, 13].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 13].Value = ":";
                                            worksheet.Cells[incRowExcel, 13].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 14].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 14].Style.Numberformat.Format = "dddd" + ",dd-MMM-yyyy";
                                            worksheet.Cells[incRowExcel, 14].Value = (rsHeader.Key.SettlementDate);
                                            worksheet.Cells[incRowExcel, 14].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 1].Value = "Re ";
                                            worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 2].Value = ":";
                                            worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 3].Value = "Settlement Instruction";
                                            incRowExcel++;




                                            incRowExcel = incRowExcel + 2;
                                            worksheet.Cells[incRowExcel, 1].Value = "Counterpart";
                                            worksheet.Cells[incRowExcel, 2].Value = "B / S";
                                            worksheet.Cells[incRowExcel, 3].Value = "Instrument";
                                            worksheet.Cells[incRowExcel, 4].Value = "Reference";
                                            worksheet.Cells[incRowExcel, 5].Value = "Quantity";
                                            worksheet.Cells[incRowExcel, 6].Value = "Price";
                                            worksheet.Cells[incRowExcel, 7].Value = "Total Price";
                                            worksheet.Cells[incRowExcel, 8].Value = "Comm.";
                                            worksheet.Cells[incRowExcel, 9].Value = "Levy";
                                            worksheet.Cells[incRowExcel, 10].Value = "VAT";
                                            worksheet.Cells[incRowExcel, 11].Value = "KPEI";
                                            if (rsHeader.Key.TrxTypeID == "BUY")
                                            {
                                                worksheet.Cells[incRowExcel, 12].Value = "Total Settle";
                                            }
                                            else
                                            {
                                                worksheet.Cells[incRowExcel, 12].Value = "Income Tax";
                                            }

                                            worksheet.Cells[incRowExcel, 13].Value = "WHT";
                                            worksheet.Cells[incRowExcel, 14].Value = "Total Payment";

                                            //THICK BOX HEADER
                                            worksheet.Cells["A" + incRowExcel + ":N" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + incRowExcel + ":N" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + incRowExcel + ":N" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + incRowExcel + ":N" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Medium;


                                            string _range = "A" + incRowExcel + ":N" + incRowExcel;
                                            using (ExcelRange r = worksheet.Cells[_range]) // KALO  KOLOM 1 SAMPE 9 A-I
                                            {
                                                //NILAINYA NGAMBIL DARI DEFAULT DI TOOLS, KLO MAU BEDA SENDIRI BOLEH2 AJA.
                                                r.Style.Font.Color.SetColor(Tools.DefaultReportColumnHeaderFontColor());
                                                r.Style.HorizontalAlignment = Tools.DefaultReportColumnHeaderHorizontalAlignment();
                                                r.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                                r.Style.Fill.BackgroundColor.SetColor(Tools.DefaultReportColumnHeaderBackgroundColor());
                                                r.Style.Font.Size = 18;
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
                                                decimal _totalSettleForBuy = 0;
                                                //decimal _totalSettleForSell = 0;
                                                _totalSettleForBuy = rsDetail.DoneAmount + rsDetail.CommissionAmount + rsDetail.LevyAmount + rsDetail.VATAmount + rsDetail.KPEIAmount;
                                                //_totalSettleForSell = rsDetail.DoneAmount + rsDetail.WHTAmount - rsDetail.CommissionAmount - rsDetail.LevyAmount - rsDetail.VATAmount - rsDetail.KPEIAmount - rsDetail.IncomeTaxSellAmount;
                                                _range = "A" + incRowExcel + ":N" + incRowExcel;
                                                using (ExcelRange r = worksheet.Cells[_range]) // KALO  KOLOM 1 SAMPE 9 A-I
                                                {
                                                    r.Style.Font.Size = 18;
                                                    r.Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                }





                                                //area detail

                                                worksheet.Cells[incRowExcel, 1].Value = rsDetail.CounterpartID;
                                                worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                if (rsDetail.TrxTypeID == "BUY")
                                                {
                                                    worksheet.Cells[incRowExcel, 2].Value = "B";

                                                }
                                                else
                                                {
                                                    worksheet.Cells[incRowExcel, 2].Value = "S";

                                                }
                                                worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.InstrumentID;
                                                worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 4].Value = rsDetail.Reference;
                                                worksheet.Cells[incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 5].Value = rsDetail.DoneVolume;
                                                worksheet.Cells[incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.0000";
                                                worksheet.Cells[incRowExcel, 6].Value = rsDetail.DonePrice;
                                                worksheet.Cells[incRowExcel, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 7].Value = rsDetail.DoneAmount;
                                                worksheet.Cells[incRowExcel, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 8].Value = rsDetail.CommissionAmount;
                                                worksheet.Cells[incRowExcel, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 9].Value = rsDetail.LevyAmount;
                                                worksheet.Cells[incRowExcel, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 10].Value = rsDetail.VATAmount;
                                                worksheet.Cells[incRowExcel, 10].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 11].Style.Numberformat.Format ="#,##0";
                                                worksheet.Cells[incRowExcel, 11].Value = rsDetail.KPEIAmount;
                                                worksheet.Cells[incRowExcel, 11].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                if (rsDetail.TrxTypeID == "BUY")
                                                {
                                                    worksheet.Cells[incRowExcel, 12].Style.Numberformat.Format = "#,##0";
                                                    //worksheet.Cells[incRowExcel, 12].Value = rsDetail.TotalAmount;
                                                    worksheet.Cells[incRowExcel, 12].Value = _totalSettleForBuy;
                                                    worksheet.Cells[incRowExcel, 12].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                }
                                                else
                                                {
                                                    worksheet.Cells[incRowExcel, 12].Style.Numberformat.Format = "#,##0";
                                                    worksheet.Cells[incRowExcel, 12].Value = rsDetail.IncomeTaxSellAmount;
                                                    worksheet.Cells[incRowExcel, 12].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                }

                                                worksheet.Cells[incRowExcel, 13].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 13].Value = rsDetail.WHTAmount;
                                                worksheet.Cells[incRowExcel, 13].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                if (rsDetail.TrxTypeID == "BUY")
                                                {
                                                    worksheet.Cells[incRowExcel, 14].Style.Numberformat.Format = "#,##0";
                                                    worksheet.Cells[incRowExcel, 14].Value = rsDetail.TotalAmount;
                                                    worksheet.Cells[incRowExcel, 14].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                    //worksheet.Cells[incRowExcel, 14].Value = (_totalSettleForBuy - rsDetail.WHTAmount);
                                                }
                                                else
                                                {
                                                    worksheet.Cells[incRowExcel, 14].Style.Numberformat.Format = "#,##0";
                                                    worksheet.Cells[incRowExcel, 14].Value = rsDetail.TotalAmount;
                                                    worksheet.Cells[incRowExcel, 14].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                    //worksheet.Cells[incRowExcel, 14].Value = _totalSettleForSell;
                                                }

                                                _endRowDetail = incRowExcel;
                                                _no++;
                                                incRowExcel++;




                                            }
                                            //THICK BOX DETAIL
                                            _endRowDetail = incRowExcel - 1;
                                            worksheet.Cells["A" + _startRowDetail].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + _startRowDetail + ":A" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + _startRowDetail + ":A" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells["B" + _startRowDetail].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["B" + _startRowDetail + ":B" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["B" + _startRowDetail + ":B" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["B" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells["C" + _startRowDetail].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["C" + _startRowDetail + ":C" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["C" + _startRowDetail + ":C" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["C" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells["D" + _startRowDetail].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["D" + _startRowDetail + ":D" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["D" + _startRowDetail + ":D" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["D" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells["E" + _startRowDetail].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["E" + _startRowDetail + ":E" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["E" + _startRowDetail + ":E" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["E" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells["F" + _startRowDetail].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["F" + _startRowDetail + ":F" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["F" + _startRowDetail + ":F" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["F" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells["G" + _startRowDetail].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["G" + _startRowDetail + ":G" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["G" + _startRowDetail + ":G" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["G" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells["H" + _startRowDetail].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["H" + _startRowDetail + ":H" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["H" + _startRowDetail + ":H" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["H" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells["I" + _startRowDetail].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["I" + _startRowDetail + ":I" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["I" + _startRowDetail + ":I" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["I" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells["J" + _startRowDetail].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["J" + _startRowDetail + ":J" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["J" + _startRowDetail + ":J" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["J" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells["K" + _startRowDetail].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["K" + _startRowDetail + ":K" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["K" + _startRowDetail + ":K" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["K" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells["L" + _startRowDetail].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["L" + _startRowDetail + ":L" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["L" + _startRowDetail + ":L" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["L" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells["M" + _startRowDetail].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["M" + _startRowDetail + ":M" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["M" + _startRowDetail + ":M" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["M" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells["N" + _startRowDetail].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["N" + _startRowDetail + ":N" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["N" + _startRowDetail + ":N" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["N" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;


                                            worksheet.Cells[incRowExcel, 3].Value = "Total " + rsHeader.Key.TrxTypeID + " (IDR) :";
                                            worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0";
                                            worksheet.Cells[incRowExcel, 5].Formula = "SUM(E" + _startRowDetail + ":E" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0";
                                            worksheet.Cells[incRowExcel, 7].Formula = "SUM(G" + _startRowDetail + ":G" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0";
                                            worksheet.Cells[incRowExcel, 8].Formula = "SUM(H" + _startRowDetail + ":H" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0";
                                            worksheet.Cells[incRowExcel, 9].Formula = "SUM(I" + _startRowDetail + ":I" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0";
                                            worksheet.Cells[incRowExcel, 10].Formula = "SUM(J" + _startRowDetail + ":J" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 10].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 11].Style.Numberformat.Format = "#,##0";
                                            worksheet.Cells[incRowExcel, 11].Formula = "SUM(K" + _startRowDetail + ":K" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 11].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 12].Style.Numberformat.Format = "#,##0";
                                            worksheet.Cells[incRowExcel, 12].Formula = "SUM(L" + _startRowDetail + ":L" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 12].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 13].Style.Numberformat.Format = "#,##0";
                                            worksheet.Cells[incRowExcel, 13].Formula = "SUM(M" + _startRowDetail + ":M" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 13].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 14].Style.Numberformat.Format = "#,##0";
                                            worksheet.Cells[incRowExcel, 14].Formula = "SUM(N" + _startRowDetail + ":N" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 14].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 5].Calculate();
                                            worksheet.Cells[incRowExcel, 7].Calculate();
                                            worksheet.Cells[incRowExcel, 8].Calculate();
                                            worksheet.Cells[incRowExcel, 9].Calculate();
                                            worksheet.Cells[incRowExcel, 10].Calculate();
                                            worksheet.Cells[incRowExcel, 11].Calculate();
                                            worksheet.Cells[incRowExcel, 12].Calculate();
                                            worksheet.Cells[incRowExcel, 13].Calculate();
                                            worksheet.Cells[incRowExcel, 14].Calculate();
                                            incRowExcel = incRowExcel + 2;
                                            worksheet.Cells[incRowExcel, 12].Value = "Net Proceeds " + rsHeader.Key.TrxTypeID + " (IDR) :";
                                            worksheet.Cells[incRowExcel, 12].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 14].Style.Numberformat.Format = "#,##0";
                                            worksheet.Cells[incRowExcel, 14].Formula = "SUM(N" + _startRowDetail + ":N" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 14].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 14].Calculate();
                                            incRowExcel = incRowExcel + 13;
                                            worksheet.Cells[incRowExcel, 3].Value = _host.Get_SignatureName(_listing.Signature1);
                                            worksheet.Cells[incRowExcel, 3].Style.Font.Size = 16;
                                            worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 7].Value = _host.Get_SignatureName(_listing.Signature2);
                                            worksheet.Cells[incRowExcel, 7].Style.Font.Size = 16;
                                            worksheet.Cells[incRowExcel, 7].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 12].Value = _host.Get_SignatureName(_listing.Signature3);
                                            worksheet.Cells[incRowExcel, 12].Style.Font.Size = 16;
                                            worksheet.Cells[incRowExcel, 12].Style.Font.Bold = true;
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 3].Value = _host.Get_PositionSignature(_listing.Signature1);
                                            worksheet.Cells[incRowExcel, 3].Style.Font.Size = 16;
                                            worksheet.Cells[incRowExcel, 3].Style.Font.Italic = true;
                                            worksheet.Cells[incRowExcel, 7].Value = _host.Get_PositionSignature(_listing.Signature2);
                                            worksheet.Cells[incRowExcel, 7].Style.Font.Size = 16;
                                            worksheet.Cells[incRowExcel, 7].Style.Font.Italic = true;
                                            worksheet.Cells[incRowExcel, 12].Value = _host.Get_PositionSignature(_listing.Signature3);
                                            worksheet.Cells[incRowExcel, 12].Style.Font.Size = 16;
                                            worksheet.Cells[incRowExcel, 12].Style.Font.Italic = true;

                                            incRowExcel = incRowExcel + 13;
                                            worksheet.Row(incRowExcel).PageBreak = true;





                                        }


                                        string _rangeA = "A1:N" + incRowExcel;
                                        using (ExcelRange r = worksheet.Cells[_rangeA]) // KALO  KOLOM 1 SAMPE 9 A-I
                                        {
                                            r.Style.Font.Size = 18;
                                            r.Style.Font.Bold = Tools.DefaultReportColumnHeaderFontBold();
                                        }


                                        // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                        worksheet.PrinterSettings.FitToPage = true;
                                        worksheet.PrinterSettings.FitToWidth = 1;
                                        worksheet.PrinterSettings.FitToHeight = 0;
                                        worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel, 15];
                                        worksheet.Column(1).Width = 20;
                                        worksheet.Column(2).Width = 15;
                                        worksheet.Column(3).Width = 20;
                                        worksheet.Column(4).Width = 20;
                                        worksheet.Column(5).Width = 20;
                                        worksheet.Column(6).Width = 20;
                                        worksheet.Column(7).Width = 25;
                                        worksheet.Column(8).Width = 15;
                                        worksheet.Column(9).Width = 15;
                                        worksheet.Column(10).Width = 15;
                                        worksheet.Column(11).Width = 15;
                                        worksheet.Column(12).Width = 25;
                                        worksheet.Column(13).Width = 15;
                                        worksheet.Column(14).Width = 30;
                                        worksheet.PrinterSettings.Orientation = eOrientation.Landscape; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                        // worksheet.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN
                                        // worksheet.PrinterSettings.FitToPage = true;
                                        //worksheet.HeaderFooter.OddHeader.RightAlignedText = "&14 PAYMENT VOUCHER";
                                        // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                        worksheet.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
                                        worksheet.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
                                        //worksheet.View.PageLayoutView = Tools.DefaultReportPageLayoutView();
                                        worksheet.View.ShowGridLines = Tools.DefaultReportShowGridLines();
                                        worksheet.HeaderFooter.OddHeader.CenteredText = "&34 SETTLEMENT LISTING EQUITY";

                                        Image img = Image.FromFile(Tools.ReportImage);
                                        worksheet.HeaderFooter.OddHeader.InsertPicture(img, PictureAlignment.Left);

                                        worksheet.HeaderFooter.OddFooter.RightAlignedText =
                                        string.Format("Page {0} of {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);

                                        ////worksheet.HeaderFooter.OddFooter.CenteredText = Tools.DefaultReportFooterCenterText();
                                        worksheet.HeaderFooter.OddFooter.LeftAlignedText = Tools.DefaultReportFooterLeftText();

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


            else if (_listing.ParamInstType == "3")
            {
                try
                {
                    using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                    {
                        DbCon.Open();
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            string _bitIsMature = "";
                            string _paramFund = "";
                            string _paramSettlementPK = "";

                            if (!_host.findString(_listing.stringInvestmentFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_listing.stringInvestmentFrom))
                            {
                                _paramSettlementPK = " And IV.SettlementPK in (" + _listing.stringInvestmentFrom + ") ";
                            }
                            else
                            {
                                _paramSettlementPK = " And IV.SettlementPK in (0) ";
                            }

                            if (_listing.ParamFundID != "All")
                            {
                                _paramFund = "and F.ID = left(@ParamFundIDFrom,charindex('-',@ParamFundIDFrom) - 1)";
                            }
                            else
                            {
                                _paramFund = "";
                            }
                            if (_listing.BitIsMature == true)
                            {
                                _bitIsMature = @"union all

                                Select  Reference,Tenor,ClearingCode,BankAccountNo,BankCustodianName,BankCustodianContactPerson,BankCustodianFaxNo,BankCustodianPhone,ContactPerson,FaxNo,Phone,InstrumentName,FundName,valueDate,InstrumentID,  
                                FundID,InstrumentType,BankBranchName ,TrxTypeID,InstructionDate,MaturityDate,Amount,DoneAmount,OrderPrice,InterestPercent,ValueDate,AcqDate,AccruedInterest,SettlementDate 
                                from InvestmentMature where Selected = 1 ";
                            }
                            else
                            {
                                _bitIsMature = "";
                            }
                            //                            cmd.CommandText = @"Select Reference, cast(substring(reference,1,charindex('/',reference,1) - 1) as integer) RefNo,IV.Tenor,BB.ClearingCode,BC.BankAccountNo,BB.Name BankCustodianName,BC.ContactPerson BankCustodianContactPerson,BC.Fax1 BankCustodianFaxNo,BC.Phone1 BankCustodianPhone,C.ContactPerson,C.Fax FaxNo,C.Phone,C.Name CounterpartName,I.Name InstrumentName,F.Name FundName,IV.valueDate,I.ID InstrumentID,I.Name InstrumentName,   
                            //                                             F.ID FundID,IT.Name InstrumentType,C.ID CounterpartID,IV.*  
                            //                                             from Investment IV   
                            //                                             left join Counterpart C on IV.CounterpartPK = C.CounterpartPK and C.status = 2  
                            //                                             left join Fund F on IV.FundPK = F.FundPK and F.status = 2  
                            //                                             left join BankBranch BC on BC.BankBranchPK = F.BankBranchPK and BC.status = 2  
                            //                                             left join Bank BB on BB.BankPK = BC.BankPK and BB.status = 2 
                            //                                             left join Instrument I on IV.InstrumentPK = I.InstrumentPK and I.status = 2  
                            //                                             left join MasterValue MV on IV.SettlementMode = MV.Code and MV.ID ='SettlementMode' and MV.status = 2  
                            //                                             left join InstrumentType IT on IV.InstrumentTypePK = IT.InstrumentTypePK and IT.status = 2   
                            //                                             Where  IV.ValueDate = @ParamListDate and IV.InstrumentTypePK = 5 and IV.statusSettlement = 2  and IV.selectedSettlement = 1 "
                            //                                             + _paramFund + @" order by Refno ";


                            cmd.CommandText = @"
                            Declare @TrailsPK int
                            Declare @MaxDateEndDayFP datetime

                            select @TrailsPK = EndDayTrailsFundPortfolioPK, @MaxDateEndDayFP = ValueDate from EndDayTrailsFundPortfolio 
                            where ValueDate = 
                            (
                            select max(valueDate) from EndDayTrailsFundPortfolio where status = 2 and valueDate < @ParamListDate
                            )
                            and status = 2

                            Select Reference, DATEDIFF (day,IV.ValueDate ,IV.MaturityDate ) Tenor,BB.ClearingCode,BC.BankAccountNo,BB.Name BankCustodianName,BC.ContactPerson BankCustodianContactPerson,BC.Fax1 BankCustodianFaxNo,BC.Phone1 BankCustodianPhone,B.ContactPerson,B.Fax1 FaxNo,B.Phone1 Phone,I.Name InstrumentName,F.Name FundName,IV.valueDate,I.ID InstrumentID,  
                            F.ID FundID,IT.Name InstrumentType,C.Name BankBranchName,IV.TrxTypeID,IV.InstructionDate,IV.MaturityDate,IV.Amount,IV.DoneAmount,IV.OrderPrice,IV.InterestPercent,IV.ValueDate,IV.AcqDate,IV.AccruedInterest ,IV.SettlementDate 
                            from Investment IV   
                            left join BankBranch B on IV.BankBranchPK = B.BankBranchPK and B.status = 2   
                            left join Fund F on IV.FundPK = F.FundPK and F.status = 2  
                            left join BankBranch BC on BC.BankBranchPK = F.BankBranchPK and BC.status = 2  
                            left join Bank BB on BB.BankPK = BC.BankPK and BB.status = 2 
                            left join Instrument I on IV.InstrumentPK = I.InstrumentPK and I.status = 2  
                            left join MasterValue MV on IV.SettlementMode = MV.Code and MV.ID ='SettlementMode' and MV.status = 2  
                            left join InstrumentType IT on IV.InstrumentTypePK = IT.InstrumentTypePK and IT.status = 2
                            left join Bank C on B.BankPK = C.BankPK and C.status = 2    
                            Where  IV.ValueDate = @ParamListDate and IV.InstrumentTypePK = 5 and IV.statusSettlement = 2  
                            " + _paramFund + _paramSettlementPK + _bitIsMature;

                            cmd.Parameters.AddWithValue("@ParamListDate", _listing.ParamListDate);
                            if (_listing.ParamFundID != "All")
                            {
                                cmd.Parameters.AddWithValue("@ParamFundIDFrom", _listing.ParamFundID);
                            }
                            cmd.Parameters.AddWithValue("@ParamInstType", _listing.ParamInstType);
                            using (SqlDataReader dr0 = cmd.ExecuteReader())
                            {
                                if (!dr0.HasRows)
                                {
                                    return false;
                                }
                                else
                                {
                                    string filePath = Tools.ReportsPath + "SettlementListingDeposito" + "_" + _listing.ParamListDate.ToString().Replace("/", "-") + "_" + _userID + ".xlsx";
                                    string pdfPath = Tools.ReportsPath + "SettlementListingDeposito" + "_" + _listing.ParamListDate.ToString().Replace("/", "-") + "_" + _userID + ".pdf";
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

                                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Settlement Listing Deposito");


                                        //ATUR DATA GROUPINGNYA DULU
                                        List<InvestmentListing> rList = new List<InvestmentListing>();
                                        while (dr0.Read())
                                        {
                                            InvestmentListing rSingle = new InvestmentListing();
                                            rSingle.FundName = dr0["FundName"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["FundName"]);
                                            rSingle.TrxTypeID = dr0["TrxTypeID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["TrxTypeID"]);
                                            rSingle.InstructionDate = Convert.ToDateTime(dr0["InstructionDate"]);
                                            rSingle.Reference = dr0["Reference"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["Reference"]);
                                            rSingle.InstrumentID = dr0["InstrumentID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["InstrumentID"]);
                                            rSingle.InstrumentName = dr0["InstrumentName"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["InstrumentName"]);
                                            rSingle.InstrumentType = dr0["InstrumentType"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["InstrumentType"]);
                                            rSingle.MaturityDate = Convert.ToDateTime(dr0["MaturityDate"]);
                                            rSingle.Amount = dr0["Amount"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["Amount"]);
                                            rSingle.DoneAmount = dr0["Amount"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["DoneAmount"]);
                                            rSingle.OrderPrice = dr0["OrderPrice"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["OrderPrice"]);
                                            rSingle.InterestPercent = dr0["InterestPercent"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["InterestPercent"]);
                                            rSingle.BankBranchName = dr0["BankBranchName"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["BankBranchName"]);
                                            rSingle.ContactPerson = dr0["ContactPerson"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["ContactPerson"]);
                                            rSingle.FaxNo = dr0["FaxNo"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["FaxNo"]);
                                            rSingle.Phone = dr0["Phone"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["Phone"]);
                                            rSingle.BankCustodianName = dr0["BankCustodianName"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["BankCustodianName"]);
                                            rSingle.BankCustodianContactPerson = dr0["BankCustodianContactPerson"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["BankCustodianContactPerson"]);
                                            rSingle.BankCustodianFaxNo = dr0["BankCustodianFaxNo"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["BankCustodianFaxNo"]);
                                            rSingle.BankCustodianPhone = dr0["BankCustodianPhone"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["BankCustodianPhone"]);
                                            rSingle.BankAccountNo = dr0["BankAccountNo"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["BankAccountNo"]);
                                            rSingle.ClearingCode = dr0["ClearingCode"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["ClearingCode"]);
                                            rSingle.Tenor = dr0["Tenor"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr0["Tenor"]);
                                            rSingle.ValueDate = Convert.ToDateTime(dr0["ValueDate"]);
                                            rSingle.SettlementDate = Convert.ToDateTime(dr0["SettlementDate"]);
                                            if (rSingle.TrxTypeID == "LIQUIDATE")
                                            {
                                                rSingle.AcqDateDeposito = Convert.ToDateTime(dr0["AcqDate"]);
                                            }
                                            else if (rSingle.TrxTypeID == "ROLLOVER")
                                            {
                                                rSingle.AcqDateDeposito = Convert.ToDateTime(dr0["ValueDate"]);
                                            }
                                            //rSingle.PurchaseAmount = Convert.ToDecimal(dr0["PurchaseAmount"]);
                                            rSingle.AccruedInterest = dr0["AccruedInterest"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["AccruedInterest"]);
                                            //rSingle.TaxCapitalGainLoss = Convert.ToDecimal(dr0["TaxCapitalGainLoss"]);
                                            //rSingle.NetAmount = Convert.ToDecimal(dr0["NetAmount"]);
                                            rList.Add(rSingle);

                                        }


                                        var QueryByFundID =
                                            from r in rList
                                            group r by new { r.FundName, r.InstrumentID } into rGroup
                                            select rGroup;

                                        int incRowExcel = 1;

                                        foreach (var rsHeader in QueryByFundID)
                                        {


                                            int _rowLine1 = 0;
                                            int _rowLine2 = 0;
                                            int _rowLineA = 0;
                                            int _rowLineB = 0;
                                            int _rowLineC = 0;
                                            int _rowLineD = 0;
                                            int _rowLineE = 0;
                                            int _rowLineF = 0;
                                            int _rowLineG = 0;
                                            int _endRowDetail = incRowExcel;

                                            //incRowExcel++;



                                            // Untuk Cetak Tebal
                                            //string _range = "A" + incRowExcel + ":F" + incRowExcel;
                                            //using (ExcelRange r = worksheet.Cells[_range]) // KALO  KOLOM 1 SAMPE 9 A-I
                                            //{
                                            //    //NILAINYA NGAMBIL DARI DEFAULT DI TOOLS, KLO MAU BEDA SENDIRI BOLEH2 AJA.
                                            //    r.Style.Font.Color.SetColor(Tools.DefaultReportColumnHeaderFontColor());
                                            //    r.Style.HorizontalAlignment = Tools.DefaultReportColumnHeaderHorizontalAlignment();
                                            //    r.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                            //    r.Style.Fill.BackgroundColor.SetColor(Tools.DefaultReportColumnHeaderBackgroundColor());
                                            //    //r.Style.Font.Size = Tools.DefaultReportColumnHeaderFontSize();
                                            //    r.Style.Font.Bold = Tools.DefaultReportColumnHeaderFontBold();
                                            //    r.Style.Border.Top.Style = Tools.DefaultReportColumnHeaderBorderTop();
                                            //    r.Style.Border.Bottom.Style = Tools.DefaultReportColumnHeaderBorderBottom();
                                            //}
                                            //incRowExcel++;

                                            //area header

                                            foreach (var rsDetail in rsHeader)
                                            {
                                                incRowExcel = incRowExcel + 2;
                                                int _rowHeader = incRowExcel;
                                                //Row D = 5
                                                int RowD = incRowExcel;
                                                int RowE = incRowExcel + 1;
                                                decimal _purchaseAmount = (rsDetail.Amount * rsDetail.OrderPrice);
                                                decimal _recalNetProceeds = (_purchaseAmount + rsDetail.AccruedInterest);

                                                //worksheet.Cells["A" + _rowHeader + ":F" + _rowHeader].Merge = true;               
                                                worksheet.Cells["A" + incRowExcel + ":F" + incRowExcel].Merge = true;
                                                worksheet.Cells[incRowExcel, 1].Value = rsHeader.Key.FundName;
                                                worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 1].Style.Font.Size = 16;

                                                worksheet.Cells["A" + _rowHeader + ":F" + _rowHeader].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;


                                                incRowExcel++;
                                                //area detail
                                                worksheet.Cells[incRowExcel, 1].Value = "To ";
                                                worksheet.Cells["C" + incRowExcel + ":D" + incRowExcel].Merge = true;
                                                worksheet.Cells[incRowExcel, 2].Value = ":";
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.BankCustodianName;
                                                incRowExcel++;
                                                worksheet.Cells["C" + incRowExcel + ":D" + incRowExcel].Merge = true;
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.BankCustodianContactPerson;
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = "Fax / Telp ";
                                                worksheet.Cells[incRowExcel, 2].Value = ":";
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.BankCustodianFaxNo;
                                                worksheet.Cells[incRowExcel, 4].Value = rsDetail.BankCustodianPhone;
                                                _rowLine1 = incRowExcel;
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = "To ";
                                                worksheet.Cells["C" + incRowExcel + ":D" + incRowExcel].Merge = true;
                                                worksheet.Cells[incRowExcel, 2].Value = ":";
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.BankBranchName;
                                                incRowExcel++;
                                                worksheet.Cells["C" + incRowExcel + ":D" + incRowExcel].Merge = true;
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.ContactPerson;
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = "Fax / Telp ";
                                                worksheet.Cells[incRowExcel, 2].Value = ":";
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.FaxNo;
                                                worksheet.Cells[incRowExcel, 4].Value = rsDetail.Phone;
                                                _rowLine2 = incRowExcel;
                                                incRowExcel = incRowExcel + 1;
                                                worksheet.Cells[incRowExcel, 1].Value = "From ";
                                                worksheet.Cells["C" + incRowExcel + ":D" + incRowExcel].Merge = true;
                                                worksheet.Cells[incRowExcel, 2].Value = ":";
                                                worksheet.Cells[incRowExcel, 3].Value = _host.Get_CompanyName();
                                                worksheet.Cells[incRowExcel, 5].Value = "Reference : ";
                                                worksheet.Cells[incRowExcel, 6].Value = rsDetail.Reference;
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = "Date ";
                                                worksheet.Cells["C" + incRowExcel + ":D" + incRowExcel].Merge = true;
                                                worksheet.Cells[incRowExcel, 2].Value = ":";
                                                worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "dddd" + ",dd-MMM-yyyy";
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.ValueDate;
                                                worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                incRowExcel++;
                                                worksheet.Cells["A" + incRowExcel + ":F" + incRowExcel].Merge = true;
                                                worksheet.Cells[incRowExcel, 1].Value = "TIME DEPOSIT";
                                                //worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                                //worksheet.Cells[incRowExcel, 1].Style.Font.Size = 16;
                                                incRowExcel++;
                                                worksheet.Cells["A" + incRowExcel + ":F" + incRowExcel].Merge = true;
                                                worksheet.Cells[incRowExcel, 1].Value = "Please execute the following instruction :";
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = "Benefary Bank ";
                                                worksheet.Cells[incRowExcel, 2].Value = ":";
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.BankBranchName;
                                                worksheet.Cells[incRowExcel, 5].Value = "Clearing Code";
                                                worksheet.Cells[incRowExcel, 6].Value = rsDetail.ClearingCode;
                                                worksheet.Cells[incRowExcel, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                _rowLineA = incRowExcel;
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = "In Favoring of ";
                                                worksheet.Cells[incRowExcel, 2].Value = ":";
                                                worksheet.Cells["C" + incRowExcel + ":F" + incRowExcel].Merge = true;
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.FundName;
                                                _rowLineB = incRowExcel;
                                                incRowExcel++;
                                                worksheet.Cells["C" + incRowExcel + ":F" + incRowExcel].Merge = true;
                                                worksheet.Cells[incRowExcel, 1].Value = "Amount (IDR) ";
                                                worksheet.Cells[incRowExcel, 2].Value = ":";
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.DoneAmount;
                                                worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                _rowLineC = incRowExcel;
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = "Value Date ";
                                                worksheet.Cells[incRowExcel, 2].Value = ":";
                                                worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "dddd" + ",dd-MMM-yyyy";
                                                if (rsDetail.TrxTypeID == "PLACEMENT")
                                                {
                                                    worksheet.Cells[incRowExcel, 3].Value = rsDetail.SettlementDate;
                                                }
                                                else if (rsDetail.TrxTypeID == "LIQUIDATE")
                                                {
                                                    worksheet.Cells[incRowExcel, 3].Value = rsDetail.AcqDateDeposito;
                                                }
                                                else
                                                {
                                                    worksheet.Cells[incRowExcel, 3].Value = rsDetail.AcqDateDeposito;
                                                }

                                                worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                worksheet.Cells[incRowExcel, 5].Value = "Interest (%) ";
                                                worksheet.Cells[incRowExcel, 6].Value = rsDetail.InterestPercent;
                                                worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.0000";
                                                worksheet.Cells[incRowExcel, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                _rowLineD = incRowExcel;
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = "Maturity Date ";
                                                worksheet.Cells[incRowExcel, 2].Value = ":";
                                                worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "dddd" + ",dd-MMM-yyyy";
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.MaturityDate;
                                                worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                worksheet.Cells[incRowExcel, 5].Value = "Tenor Days ";
                                                worksheet.Cells[incRowExcel, 6].Value = rsDetail.Tenor;
                                                worksheet.Cells[incRowExcel, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                _rowLineE = incRowExcel;
                                                incRowExcel = incRowExcel + 2;
                                                worksheet.Cells["A" + incRowExcel + ":F" + incRowExcel].Merge = true;
                                                if (rsDetail.TrxTypeID == "PLACEMENT")
                                                {
                                                    worksheet.Cells[incRowExcel, 1].Value = "NEW PLACEMENT";
                                                }
                                                else if (rsDetail.TrxTypeID == "LIQUIDATE")
                                                {
                                                    worksheet.Cells[incRowExcel, 1].Value = "LIQUIDATE";
                                                }
                                                else
                                                {
                                                    worksheet.Cells[incRowExcel, 1].Value = "ROLLOVER";
                                                }

                                                //worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                                //worksheet.Cells[incRowExcel, 1].Style.Font.Size = 16;
                                                incRowExcel = incRowExcel + 2;
                                                worksheet.Cells[incRowExcel, 1].Value = "Notes ";
                                                worksheet.Cells[incRowExcel, 2].Value = ":";
                                                worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                worksheet.Cells[incRowExcel, 3].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                                                worksheet.Cells["C" + incRowExcel + ":F" + (incRowExcel + 4)].Merge = true;
                                                worksheet.Cells[incRowExcel, 3].Value = _listing.Message;
                                                worksheet.Cells[incRowExcel, 3].Style.WrapText = true;
                                                incRowExcel = incRowExcel + 6;
                                                var _principalText = "";
                                                if (rsDetail.TrxTypeID == "LIQUIDATE")
                                                {
                                                    _principalText = "Principal and Interest";
                                                }
                                                else
                                                {
                                                    _principalText = "Interest";
                                                }
                                                worksheet.Cells["A" + incRowExcel + ":F" + incRowExcel].Merge = true;
                                                worksheet.Cells[incRowExcel, 1].Value = "Upon maturity, please transfer the " + _principalText + " to : ";
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = "Bank Name ";
                                                worksheet.Cells[incRowExcel, 2].Value = ":";
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.BankCustodianName;
                                                _rowLineF = incRowExcel;
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = "Account Name ";
                                                worksheet.Cells[incRowExcel, 2].Value = ":";
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.FundName;
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = "Account Number ";
                                                worksheet.Cells[incRowExcel, 2].Value = ":";
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.BankAccountNo;
                                                _rowLineG = incRowExcel;
                                                //incRowExcel = incRowExcel + 2;
                                                //worksheet.Cells["A" + incRowExcel + ":F" + incRowExcel].Merge = true;
                                                //worksheet.Cells[incRowExcel, 1].Value = "and interest to  ";
                                                //incRowExcel++;
                                                //worksheet.Cells[incRowExcel, 1].Value = "Bank Name ";
                                                //worksheet.Cells[incRowExcel, 2].Value = ":";
                                                //worksheet.Cells[incRowExcel, 3].Value = rsDetail.BankCustodianName;
                                                //_rowLineH = incRowExcel;
                                                //incRowExcel++;
                                                //worksheet.Cells[incRowExcel, 1].Value = "Account Name ";
                                                //worksheet.Cells[incRowExcel, 2].Value = ":";
                                                //worksheet.Cells[incRowExcel, 3].Value = rsDetail.FundName;
                                                //incRowExcel++;
                                                //worksheet.Cells[incRowExcel, 1].Value = "Account Number ";
                                                //worksheet.Cells[incRowExcel, 2].Value = ":";
                                                //worksheet.Cells[incRowExcel, 3].Value = rsDetail.BankAccountNo;
                                                //_rowLineI = incRowExcel;
                                                incRowExcel++;
                                                incRowExcel = incRowExcel + 4;

                                                int _RowA = incRowExcel;
                                                int _RowB = incRowExcel + 11;
                                                worksheet.Cells["A" + _RowB + ":H" + _RowB].Style.Font.Bold = true;
                                                worksheet.Cells["A" + _RowB + ":H" + _RowB].Style.Font.Size = 15;
                                                if (Tools.ClientCode == "12")
                                                {
                                                    if (_listing.Signature1 != 0)
                                                    {
                                                        worksheet.Cells[_RowA, 3].Value = _host.Get_PositionSignature(_listing.Signature1);
                                                        worksheet.Cells[_RowA, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                        worksheet.Cells[_RowB, 3].Value = "( " + _host.Get_SignatureName(_listing.Signature1) + " )";
                                                        worksheet.Cells[_RowB, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;


                                                    }
                                                    else
                                                    {
                                                        worksheet.Cells[_RowA, 3].Value = "";
                                                        worksheet.Cells[_RowA, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                        worksheet.Cells[_RowB, 3].Value = "";
                                                        worksheet.Cells[_RowB, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

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
                                                        worksheet.Cells[_RowA, 5].Value = _host.Get_PositionSignature(_listing.Signature3);
                                                        worksheet.Cells[_RowA, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                        worksheet.Cells[_RowB, 5].Value = "( " + _host.Get_SignatureName(_listing.Signature3) + " )";
                                                        worksheet.Cells[_RowB, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                    }
                                                    else
                                                    {
                                                        worksheet.Cells[_RowA, 5].Value = "";
                                                        worksheet.Cells[_RowA, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                        worksheet.Cells[_RowB, 5].Value = "";
                                                        worksheet.Cells[_RowB, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                    }

                                                    if (_listing.Signature4 != 0)
                                                    {
                                                        worksheet.Cells[_RowA, 6].Value = _host.Get_PositionSignature(_listing.Signature4);
                                                        worksheet.Cells[_RowA, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                        worksheet.Cells[_RowB, 6].Value = "( " + _host.Get_SignatureName(_listing.Signature4) + " )";
                                                        worksheet.Cells[_RowB, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;


                                                    }
                                                    else
                                                    {
                                                        worksheet.Cells[_RowA, 6].Value = "";
                                                        worksheet.Cells[_RowA, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                        worksheet.Cells[_RowB, 6].Value = "";
                                                        worksheet.Cells[_RowB, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                    }
                                                }
                                                else
                                                {
                                                    worksheet.Cells[incRowExcel, 1].Value = "         Sincerely Yours                             Acknowledged by                   Confirmed by                        Verified by";
                                                    worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                    incRowExcel = incRowExcel + 7;
                                                    worksheet.Cells[incRowExcel, 1].Value = "      (                                 )                       (                                 )            (                                 )          (                                 )";
                                                    worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                }

                                                //worksheet.Cells[incRowExcel, 1].Value = "Sincerely Yours";
                                                //worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                //worksheet.Cells[incRowExcel, 2].Value = "Acknowledged by";
                                                //worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                //worksheet.Cells[incRowExcel, 4].Value = "Confirmed by";
                                                //worksheet.Cells[incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                //worksheet.Cells[incRowExcel, 6].Value = "Verified by";
                                                //worksheet.Cells[incRowExcel, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                //worksheet.Cells[incRowExcel, 2].Value = "(                          )";
                                                //worksheet.Cells[incRowExcel, 4].Value = "(                          )";
                                                //worksheet.Cells[incRowExcel, 6].Value = "(                          )";

                                                worksheet.Cells["A" + _rowLine1 + ":F" + _rowLine1].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + _rowLine2 + ":F" + _rowLine2].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                                //BORDER 
                                                worksheet.Cells["A" + _rowLineA + ":F" + _rowLineA].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + _rowLineA + ":F" + _rowLineA].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + _rowLineB + ":F" + _rowLineB].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + _rowLineC + ":F" + _rowLineC].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + _rowLineD + ":F" + _rowLineD].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + _rowLineE + ":F" + _rowLineE].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                                worksheet.Cells["A" + _rowLineA + ":A" + _rowLineE].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["C" + _rowLineA + ":C" + _rowLineE].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                //worksheet.Cells["D" + _rowLineA + ":D" + _rowLineE].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["E" + _rowLineA + ":E" + _rowLineE].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["F" + _rowLineA + ":F" + _rowLineE].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["G" + _rowLineA + ":G" + _rowLineE].Style.Border.Left.Style = ExcelBorderStyle.Medium;

                                                worksheet.Cells["C" + _rowLineF + ":F" + _rowLineF].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["C" + _rowLineF + ":C" + _rowLineG].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["F" + _rowLineF + ":F" + _rowLineG].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["C" + _rowLineG + ":F" + _rowLineG].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                                //worksheet.Cells["C" + _rowLineH + ":F" + _rowLineH].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                //worksheet.Cells["C" + _rowLineH + ":C" + _rowLineI].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                //worksheet.Cells["F" + _rowLineH + ":F" + _rowLineI].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                                //worksheet.Cells["C" + _rowLineI + ":F" + _rowLineI].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;


                                                incRowExcel = incRowExcel + 14;
                                                worksheet.Row(incRowExcel).PageBreak = true;

                                                //incRowExcel++;
                                            }






                                            string _rangeA = "A1:F" + incRowExcel;
                                            using (ExcelRange r = worksheet.Cells[_rangeA]) // KALO  KOLOM 1 SAMPE 9 A-I
                                            {
                                                r.Style.Font.Size = 16;
                                                r.Style.Font.Bold = Tools.DefaultReportColumnHeaderFontBold();
                                            }
                                        }




                                        // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                        worksheet.PrinterSettings.FitToPage = true;
                                        worksheet.PrinterSettings.FitToWidth = 1;
                                        worksheet.PrinterSettings.FitToHeight = 0;
                                        worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel, 6];
                                        worksheet.Column(1).Width = 35;
                                        worksheet.Column(2).Width = 4;
                                        worksheet.Column(3).Width = 35;
                                        worksheet.Column(4).Width = 35;
                                        worksheet.Column(5).Width = 35;
                                        worksheet.Column(6).Width = 35;
                                        worksheet.PrinterSettings.Orientation = eOrientation.Portrait; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                        // worksheet.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN
                                        // worksheet.PrinterSettings.FitToPage = true;
                                        //worksheet.HeaderFooter.OddHeader.RightAlignedText = "&14 PAYMENT VOUCHER";
                                        // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                        worksheet.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
                                        worksheet.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
                                        //worksheet.View.PageLayoutView = Tools.DefaultReportPageLayoutView();
                                        worksheet.View.ShowGridLines = Tools.DefaultReportShowGridLines();
                                        //worksheet.HeaderFooter.OddHeader.CenteredText = "&34 SETTLEMENT LISTING BOND";

                                        Image img = Image.FromFile(Tools.ReportImage);
                                        worksheet.HeaderFooter.OddHeader.InsertPicture(img, PictureAlignment.Left);
                                        worksheet.HeaderFooter.OddFooter.RightAlignedText =
                                       string.Format("Page {0} of {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);

                                        ////worksheet.HeaderFooter.OddFooter.CenteredText = Tools.DefaultReportFooterCenterText();
                                        worksheet.HeaderFooter.OddFooter.LeftAlignedText = Tools.DefaultReportFooterLeftText();
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

            else
            {
                return false;
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
                         @" 
                            
                        Select lower(C.ApprovedUsersID) CheckedBy,lower(C.ApprovedUsersID) ApprovedBy, lower(C.EntryUsersID) PreparedBy, C.Reference ,      
                        valuedate,A.ID AccountID, A.Name AccountName, C.Description,C.DocRef,     
                        DebitCredit,(Case When DebitCredit ='D' then BaseDebit else 0 end) Debit,(Case When DebitCredit ='D' then 0 else BaseCredit end) Credit,F.ID DepartmentID,Case When DebitCredit ='D' then 1 else 2 end Row       
                        from Cashier C       
                        left join Account A on C.Debitaccountpk =A.Accountpk and A.status = 2       
                        left join Office E on C.OfficePK = E.OfficePK and E.status = 2       
                        left join Department F on C.DepartmentPK = F.DepartmentPK and F.status = 2       
                        left join Agent G on C.AgentPK = G.AgentPK and G.status = 2       
                        left join Consignee H on C.ConsigneePK = H.ConsigneePK and H.status = 2       
                        left join Instrument I on C.InstrumentPK = I.InstrumentPK and I.status = 2       
                        Where C.CashierID = @CashierID and C.Type = 'CP' and C.PeriodPK = @PeriodPK and C.Status in (1,2)         
                        UNION ALL       
                        Select lower(C.ApprovedUsersID) CheckedBy,lower(C.ApprovedUsersID) ApprovedBy, lower(C.EntryUsersID) PreparedBy, C.Reference ,      
                        valuedate,A.ID AccountID, A.Name AccountName, '' Description,C.DocRef,         
                        'C', 0 Debit,SUM(Case When DebitCredit ='D' then BaseDebit else BaseCredit * -1 end)Credit,'' DepartmentID ,3 Row          
                        from Cashier C       
                        left join Account A on C.Creditaccountpk =A.Accountpk and A.status = 2       
                        left join Office E on C.OfficePK = E.OfficePK and E.status = 2       
                        left join Department F on C.DepartmentPK = F.DepartmentPK and F.status = 2       
                        left join Agent G on C.AgentPK = G.AgentPK and G.status = 2       
                        left join Consignee H on C.ConsigneePK = H.ConsigneePK and H.status = 2       
                        left join Instrument I on C.InstrumentPK = I.InstrumentPK and I.status = 2       
                        Where C.CashierID = @CashierID  and C.Type = 'CP' and C.PeriodPK = @PeriodPK and C.Status in (1,2)    
                        group by C.EntryUsersID, C.ApprovedUsersID, Valuedate,A.ID, A.Name, C.Reference ,C.DocRef       
                        Order By row,AccountID asc ";

                        cmd.Parameters.AddWithValue("@CashierID", _cashier.CashierID);
                        cmd.Parameters.AddWithValue("@PeriodPK", _cashier.PeriodPK);
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
                                        rSingle.DocRef = Convert.ToString(dr0["DocRef"]);
                                        rSingle.AccountID = Convert.ToString(dr0["AccountID"]);
                                        rSingle.AccountName = Convert.ToString(dr0["AccountName"]);
                                        rSingle.DebitCredit = Convert.ToString(dr0["DebitCredit"]);
                                        rSingle.Debit = Convert.ToDecimal(dr0["Debit"]);
                                        rSingle.Credit = Convert.ToDecimal(dr0["Credit"]);
                                        rSingle.DepartmentID = Convert.ToString(dr0["DepartmentID"]);
                                        rSingle.CheckedBy = Convert.ToString(dr0["CheckedBy"]);
                                        rSingle.ApprovedBy = Convert.ToString(dr0["ApprovedBy"]);
                                        rSingle.PreparedBy = Convert.ToString(dr0["PreparedBy"]);
                                        rList.Add(rSingle);

                                    }

                                    var GroupByReference =
                                                 from r in rList
                                                 orderby r.DebitCredit descending
                                                 group r by new { r.Reference, r.ValueDate, r.CheckedBy, r.PreparedBy, r.ApprovedBy, r.DocRef } into rGroup
                                                 select rGroup;

                                    int incRowExcel = 0;

                                    foreach (var rsHeader in GroupByReference)
                                    {
                                        incRowExcel = incRowExcel + 2;

                                        worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 2].Value = "Kepada        : ";
                                        worksheet.Cells[incRowExcel, 3].Value = rsHeader.Key.DocRef;
                                        worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;
                                        incRowExcel++;
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
                                            //worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0.0000";
                                            //worksheet.Cells[incRowExcel, 10].Value = rsDetail.Rate;
                                            //worksheet.Cells[incRowExcel, 11].Style.Numberformat.Format = "#,##0.00";;
                                            //worksheet.Cells[incRowExcel, 11].Value = rsDetail.BaseDebit;
                                            //worksheet.Cells[incRowExcel, 12].Style.Numberformat.Format = "#,##0.00";;
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


                                        worksheet.Cells[incRowExcel, 5].Formula = "SUM(E" + _startRowDetail + ":E" + _endRowDetail + ")";
                                        worksheet.Cells[incRowExcel, 6].Formula = "SUM(F" + _startRowDetail + ":F" + _endRowDetail + ")";
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
                                        worksheet.Cells[incRowExcel, 5].Calculate();
                                        worksheet.Cells[incRowExcel, 6].Calculate();

                                        incRowExcel = incRowExcel + 1;

                                        worksheet.Cells[incRowExcel, 5].Value = worksheet.Cells[incRowExcel - 1, 5].Value;
                                        worksheet.Cells[incRowExcel, 6].Value = worksheet.Cells[incRowExcel - 1, 6].Value;
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

                                        worksheet.Cells[incRowExcel - 1, 5].Value = "";
                                        worksheet.Cells[incRowExcel - 1, 6].Value = "";
                                        incRowExcel = incRowExcel + 2;
                                        worksheet.Cells[incRowExcel, 2].Value = "Prepared By";
                                        worksheet.Cells[incRowExcel, 4].Value = "Checked By";
                                        worksheet.Cells[incRowExcel, 6].Value = "Approved By";
                                        worksheet.Cells["A" + incRowExcel + ":J" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                        incRowExcel = incRowExcel + 5;
                                        worksheet.Cells["A" + incRowExcel + ":J" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                        worksheet.Cells[incRowExcel, 2].Value = "(     Sulikana     )";
                                        worksheet.Cells["B" + incRowExcel + ":C" + incRowExcel].Merge = true;

                                        worksheet.Cells[incRowExcel, 4].Value = "(    Ferina Tanzil    )";
                                        worksheet.Cells["D" + incRowExcel + ":E" + incRowExcel].Merge = true;

                                        worksheet.Cells[incRowExcel, 6].Value = "( Fitzgerald Stevan Purba )";
                                        worksheet.Cells["F" + incRowExcel + ":G" + incRowExcel].Merge = true;
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
                                    worksheet.HeaderFooter.OddHeader.CenteredText = "&14 BUKTI KAS KELUAR";
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
                            @" 
                            Select lower(C.ApprovedUsersID) CheckedBy,lower(C.ApprovedUsersID) ApprovedBy, lower(C.EntryUsersID) PreparedBy, C.Reference ,      
                            valuedate,A.ID AccountID, A.Name AccountName, C.Description,C.DocRef,       
                            DebitCredit,(Case When DebitCredit ='C' then 0 else BaseCredit end) Debit,(Case When DebitCredit ='C' then BaseDebit else 0 end) Credit,F.ID DepartmentID , case when DebitCredit ='D' then 2 else 3 end Row    
                            from Cashier C         
                            left join Account A on C.Creditaccountpk =A.Accountpk and A.status = 2         
                            left join Office E on C.OfficePK = E.OfficePK and E.status = 2         
                            left join Department F on C.DepartmentPK = F.DepartmentPK and F.status = 2         
                            left join Agent G on C.AgentPK = G.AgentPK and G.status = 2         
                            left join Consignee H on C.ConsigneePK = H.ConsigneePK and H.status = 2         
                            left join Instrument I on C.InstrumentPK = I.InstrumentPK and I.status = 2         
                            Where 
							C.CashierID = @CashierID  and 
							C.Type = 'CR' and 
							C.PeriodPK = @PeriodPK and 
							C.Status in (1,2)         
                            UNION ALL         
                            Select lower(C.ApprovedUsersID) CheckedBy,lower(C.ApprovedUsersID) ApprovedBy, lower(C.EntryUsersID) PreparedBy, C.Reference ,        
                            valuedate,A.ID AccountID, A.Name AccountName, '' Description,C.DocRef,      
                            'D', SUM(Case When DebitCredit ='C' then BaseDebit else BaseCredit * -1 end) Debit,0 Credit,'' DepartmentID, 1 Row             
                            from Cashier C         
                            left join Account A on C.DebitAccountPK =A.Accountpk and A.status = 2         
                            left join Office E on C.OfficePK = E.OfficePK and E.status = 2         
                            left join Department F on C.DepartmentPK = F.DepartmentPK and F.status = 2         
                            left join Agent G on C.AgentPK = G.AgentPK and G.status = 2         
                            left join Consignee H on C.ConsigneePK = H.ConsigneePK and H.status = 2         
                            left join Instrument I on C.InstrumentPK = I.InstrumentPK and I.status = 2        
                            Where 
							C.CashierID = @CashierID  and 
							C.Type = 'CR' and 
							C.PeriodPK = @PeriodPK and 
							C.Status in (1,2)       
                            group by C.EntryUsersID , C.ApprovedUsersID, Valuedate,A.ID, A.Name,C.Reference ,C.DocRef  
                            Order By Row,AccountID asc ";

                        cmd.Parameters.AddWithValue("@CashierID", _cashier.CashierID);
                        cmd.Parameters.AddWithValue("@PeriodPK", _cashier.PeriodPK);
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
                                        rSingle.DocRef = Convert.ToString(dr0["DocRef"]);
                                        rSingle.AccountID = Convert.ToString(dr0["AccountID"]);
                                        rSingle.AccountName = Convert.ToString(dr0["AccountName"]);
                                        rSingle.DebitCredit = Convert.ToString(dr0["DebitCredit"]);
                                        rSingle.Debit = Convert.ToDecimal(dr0["Debit"]);
                                        rSingle.Credit = Convert.ToDecimal(dr0["Credit"]);
                                        rSingle.DepartmentID = Convert.ToString(dr0["DepartmentID"]);
                                        rSingle.CheckedBy = Convert.ToString(dr0["CheckedBy"]);
                                        rSingle.ApprovedBy = Convert.ToString(dr0["ApprovedBy"]);
                                        rSingle.PreparedBy = Convert.ToString(dr0["PreparedBy"]);
                                        rList.Add(rSingle);

                                    }

                                    var GroupByReference =
                                                 from r in rList
                                                 orderby r.DebitCredit descending
                                                 group r by new { r.Reference, r.ValueDate, r.CheckedBy, r.ApprovedBy, r.PreparedBy, r.DocRef } into rGroup
                                                 select rGroup;

                                    int incRowExcel = 0;

                                    foreach (var rsHeader in GroupByReference)
                                    {
                                        incRowExcel = incRowExcel + 2;
                                        worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 2].Value = "Kepada        : ";
                                        worksheet.Cells[incRowExcel, 3].Value = rsHeader.Key.DocRef;
                                        worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;
                                        incRowExcel++;
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

                                        worksheet.Cells[incRowExcel, 5].Formula = "SUM(E" + _startRowDetail + ":E" + _endRowDetail + ")";
                                        worksheet.Cells[incRowExcel, 6].Formula = "SUM(F" + _startRowDetail + ":F" + _endRowDetail + ")";
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

                                        worksheet.Cells[incRowExcel, 5].Calculate();
                                        worksheet.Cells[incRowExcel, 6].Calculate();
                                        incRowExcel = incRowExcel + 1;
                                        worksheet.Cells[incRowExcel, 5].Value = worksheet.Cells[incRowExcel - 1, 5].Value;
                                        worksheet.Cells[incRowExcel, 6].Value = worksheet.Cells[incRowExcel - 1, 6].Value;
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

                                        worksheet.Cells[incRowExcel - 1, 5].Value = "";
                                        worksheet.Cells[incRowExcel - 1, 6].Value = "";
                                        incRowExcel = incRowExcel + 2;
                                        worksheet.Cells[incRowExcel, 2].Value = "Prepared By";
                                        worksheet.Cells[incRowExcel, 4].Value = "Checked By";
                                        worksheet.Cells[incRowExcel, 6].Value = "Approved By";
                                        worksheet.Cells["A" + incRowExcel + ":J" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                        incRowExcel = incRowExcel + 5;
                                        worksheet.Cells["A" + incRowExcel + ":J" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                        worksheet.Cells[incRowExcel, 2].Value = "(     Sulikana     )";
                                        worksheet.Cells["B" + incRowExcel + ":C" + incRowExcel].Merge = true;

                                        worksheet.Cells[incRowExcel, 4].Value = "(    Ferina Tanzil    )";
                                        worksheet.Cells["D" + incRowExcel + ":E" + incRowExcel].Merge = true;

                                        worksheet.Cells[incRowExcel, 6].Value = "( Fitzgerald Stevan Purba )";
                                        worksheet.Cells["F" + incRowExcel + ":G" + incRowExcel].Merge = true;
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
                                    worksheet.HeaderFooter.OddHeader.CenteredText = "&14 BUKTI KAS MASUK";
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

        public Boolean GenerateReportUnitRegistry(string _userID, UnitRegistryRpt _unitRegistryRpt)
        {

           #region Client Unit Position
            if (_unitRegistryRpt.ReportName.Equals("Client Unit Position"))
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

                            if (!_host.findString(_unitRegistryRpt.FundFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_unitRegistryRpt.FundFrom))
                            {
                                _paramFund = "And A.FundPk in ( " + _unitRegistryRpt.FundFrom + " ) ";
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


                            cmd.CommandText =
                                    @" Select B.Name ClientName,B.ClientCategory clientcategory1,mv.DescOne ClientCategory2, C.Name FundName,@Date Date ,isnull(CNA.Nav,0) NavAmount,A.UnitAmount,A.CashAmount, isnull(sum(CNA.NAv * A.UnitAmount),0) EndBalance ,
case when B.ClientCategory = 2 then mv1.DescOne else 'INDIVIDUAL' end ClientCategory
From FundClientPosition A   
                                     Left join FundClient B on A.FundClientPK = B.FundClientPK and B.Status = 2   
                                     Left join Fund C on A.FundPK = C.FundPK and C.Status = 2
									 left join MasterValue mv on b.ClientCategory = mv.Code  and mv.ID = 'ClientCategory'  
									  left join MasterValue mv1 on b.Tipe = mv1.Code  and mv1.ID = 'CompanyType' 
                                     left Join    
                                    (   
                                         select B.ID FundID, MAX(CN.Nav) NAV    
                                         from CloseNAV CN   
                                         Left join Fund B on B.FundPK = CN.FundPK and B.Status =2  
                                         where CN.Date = @Date   
                                         group by B.ID   
                                     ) CNA on CNA.FundID = C.ID   

                                     left Join    
                                     (   
                                         select B.ID FundID, MAX(A.Date) MaxDate    
                                         from FundClientPosition A   
                                         Left join Fund B on B.FundPK = A.FundPK and B.Status =2  
                                         where A.Date <= @Date  
                                         group by B.ID   
                                     ) MFCP on MFCP.FundID = C.ID   
                                     where MFCP.MaxDate = A.Date   and A.UnitAmount <> 0         
									  " + _paramFund + _paramFundClient + @"
                                     group by B.Name,B.Clientcategory ,mv.DescOne , C.Name ,CNA.Nav,A.UnitAmount,A.CashAmount,mv1.DescOne";

                            cmd.Parameters.AddWithValue("@Date", _unitRegistryRpt.ValueDateFrom);

                            using (SqlDataReader dr0 = cmd.ExecuteReader())
                            {
                                if (!dr0.HasRows)
                                {
                                    return false;
                                }
                                else
                                {
                                    string filePath = Tools.ReportsPath + "ClientUnitPosition" + "_" + _userID + ".xlsx";
                                    string pdfPath = Tools.ReportsPath + "ClientUnitPosition" + "_" + _userID + ".pdf";
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

                                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Client Unit Position");


                                        //ATUR DATA GROUPINGNYA DULU
                                        List<UnitRegistryRpt> rList = new List<UnitRegistryRpt>();
                                        while (dr0.Read())
                                        {
                                            UnitRegistryRpt rSingle = new UnitRegistryRpt();
                                            rSingle.ClientName = Convert.ToString(dr0["ClientName"]);
                                            rSingle.Date = Convert.ToDateTime(dr0["Date"]);
                                            rSingle.Nav = Convert.ToDecimal(dr0["NavAmount"]);
                                            rSingle.FundName = Convert.ToString(dr0["FundName"]);
                                            rSingle.ClientCategory = Convert.ToString(dr0["ClientCategory"]);
                                            rSingle.CashBalance = Convert.ToDecimal(dr0["CashAmount"]);
                                            rSingle.UnitBalance = Convert.ToDecimal(dr0["UnitAmount"]);
                                            rSingle.EndBalance = Convert.ToDecimal(dr0["EndBalance"]);

                                            rList.Add(rSingle);

                                        }
                                        //mekel
                                        var QueryByClientID =
                                         from r in rList
                                         group r by new { r.ClientName } into rGroup
                                         select rGroup;

                                        int incRowExcel = 6;

                                        worksheet.Cells[incRowExcel, 1].Value = "Date :";

                                        worksheet.Cells[incRowExcel, 2].Value = Convert.ToDateTime(_unitRegistryRpt.ValueDateFrom).ToString("dd/MMM/yyyy");
                                        //worksheet.Cells[incRowExcel, 2].Style.Numberformat.Format = "dd/MMM/yyyy";
                                        incRowExcel++;
                                        foreach (var rsHeader in QueryByClientID)
                                        {
                                            worksheet.Cells[incRowExcel, 1].Value = "Client :";
                                            worksheet.Cells[incRowExcel, 2].Value = rsHeader.Key.ClientName;
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Value = "No.";
                                            worksheet.Cells[incRowExcel, 3].Value = "Client Category";
                                            worksheet.Cells[incRowExcel, 2].Value = "Fund";
                                            worksheet.Cells[incRowExcel, 4].Value = "NAV/Unit";
                                            worksheet.Cells[incRowExcel, 5].Value = "Unit";
                                            worksheet.Cells[incRowExcel, 6].Value = "Cash";
                                            string _range = "A" + incRowExcel + ":F" + incRowExcel;

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

                                            _range = "A" + incRowExcel + ":F" + incRowExcel;
                                            using (ExcelRange r = worksheet.Cells[_range]) // KALO  KOLOM 1 SAMPE 9 A-I
                                            {
                                                //NILAINYA NGAMBIL DARI DEFAULT DI TOOLS, KLO MAU BEDA SENDIRI BOLEH2 AJA.
                                                r.Style.Font.Color.SetColor(Tools.DefaultReportColumnHeaderFontColor());
                                                r.Style.HorizontalAlignment = Tools.DefaultReportColumnHeaderHorizontalAlignment();
                                                r.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                                r.Style.Fill.BackgroundColor.SetColor(Tools.DefaultReportColumnHeaderBackgroundColor());
                                                r.Style.Font.Size = Tools.DefaultReportColumnHeaderFontSize();
                                                r.Style.Font.Bold = Tools.DefaultReportColumnHeaderFontBold();
                                                r.Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                r.Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            }

                                            incRowExcel++;
                                            int _no = 1;

                                            int _startRowDetail = incRowExcel;
                                            //int _endRowDetail = 0;
                                            //end area header

                                            foreach (var rsDetail in rsHeader)
                                            {

                                                _range = "A" + incRowExcel + ":F" + incRowExcel;
                                                using (ExcelRange r = worksheet.Cells[_range]) // KALO  KOLOM 1 SAMPE 9 A-I
                                                {
                                                    r.Style.Font.Size = 11;
                                                    r.Style.Border.Bottom.Style = ExcelBorderStyle.Dashed;
                                                }
                                                //area detail
                                                worksheet.Cells[incRowExcel, 1].Value = _no;
                                                worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.ClientCategory;
                                                worksheet.Cells[incRowExcel, 2].Value = rsDetail.FundName;
                                                worksheet.Cells[incRowExcel, 4].Value = rsDetail.Nav;
                                                worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##0.0000";
                                                worksheet.Cells[incRowExcel, 5].Value = rsDetail.UnitBalance;
                                                worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.000000";
                                                worksheet.Cells[incRowExcel, 6].Value = rsDetail.Nav * rsDetail.UnitBalance;
                                                worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.00";


                                                incRowExcel++;
                                                _range = "A" + incRowExcel + ":F" + incRowExcel;
                                                using (ExcelRange r = worksheet.Cells[_range]) // KALO  KOLOM 1 SAMPE 9 A-I
                                                {
                                                    r.Style.Font.Size = 11;
                                                }

                                                //_endRowDetail = incRowExcel;
                                                _no++;
                                                //incRowExcel++;

                                            }
                                            incRowExcel = incRowExcel + 2;
                                            worksheet.Row(incRowExcel).PageBreak = _unitRegistryRpt.PageBreak;
                                            incRowExcel = incRowExcel + 6;

                                        }




                                        // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                        worksheet.Cells["A3:F14"].AutoFitColumns();  // UNTUK AUTO FIT UKURAN TULISAN DENGAN KOLOM, KEBETULAN INI BUAT HEADER AJA DIPAKENYA

                                        worksheet.PrinterSettings.Orientation = eOrientation.Portrait; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                        worksheet.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN
                                        worksheet.HeaderFooter.OddHeader.CenteredText = "&12 CLIENT UNIT POSITION";

                                        // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                        worksheet.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
                                        worksheet.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
                                        worksheet.View.PageLayoutView = Tools.DefaultReportPageLayoutView();
                                        worksheet.View.ShowGridLines = Tools.DefaultReportShowGridLines();
                                        Image img = Image.FromFile(Tools.ReportImage);
                                        Image thumb = img.GetThumbnailImage(Tools.imgWidth, Tools.imgHeight, null, IntPtr.Zero);
                                        worksheet.HeaderFooter.OddHeader.InsertPicture(thumb, PictureAlignment.Left);

                                        //worksheet.HeaderFooter.OddHeader.CenteredText = Tools.DefaultReportHeaderCenterText();
                                        //worksheet.HeaderFooter.OddHeader.LeftAlignedText = Tools.DefaultReportHeaderLeftText();

                                        worksheet.HeaderFooter.OddFooter.RightAlignedText =
                                        string.Format("Page {0} of {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);

                                        //worksheet.HeaderFooter.OddFooter.CenteredText = Tools.DefaultReportFooterCenterText();
                                        worksheet.HeaderFooter.OddFooter.LeftAlignedText = Tools.DefaultReportFooterLeftText();





                                        package.Save();
                                        //Tools.ExportFromExcelToPDF(filePath, pdfPath);
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
                            _mature = @"
                            union all

                            select CONVERT(varchar(15), [identity]) + '/FP/'  
                            + REPLACE(RIGHT(CONVERT(VARCHAR(8), A.MaturityDate, 3), 5) ,'/','') Reference,A.MaturityDate valuedate, A.MaturityDate settlementdate, '' BrokerCode, C.Sinvestcode FundCode, D.ID Instrument, 1 DonePrice, A.Balance Quantity, 
                            A.Balance TradeAmount,0 CommissionAmount, 0 IncomeTaxSellAmount, 0 LevyAmount, 0 VatAmount, 0 OtherCharges, 2 TransactionType,
                            A.Balance TotalAmount, 0 WHTAmount, '' InvestmentNotes, 2 TrxType,2 SettlementMode,'' LastCouponDate,'' NextCouponDate,0 AccruedDays,
                            0 CapitalGainAmount,0 TaxInterestAmount,F.PTPCode BankCode,G.PTPCode BankBranchCode,F.Name BankName,G.BankAccountNo,
                            A.InterestPercent,A.MaturityDate,F.BitSyariah,G.ContactPerson,G.Phone1,G.Fax1,G.BankAccountName,A.Balance OldTradeAmount,A.InterestPercent OldInterestPercent,A.MaturityDate OldMaturityDate,H.ID CurrencyID,'' InterestPaymentType,A.Balance * 1 AmountTrf, A.InterestPercent BreakInterestPercent,AcqDate,AcqDate , 
                            round(A.Balance * (A.InterestPercent/100)/365 * datediff(day,A.AcqDate,A.MaturityDate),0) InterestAmount
                            from FundPosition A
                            left join Fund C on A.fundpk = C.fundpk and C.status = 2
                            left join instrument D on A.instrumentpk = D.instrumentpk and D.status = 2  
                            left join Bank F on A.BankPK = F.BankPK and F.status = 2
                            left join BankBranch G on A.BankBranchPK = G.BankBranchPK and G.status = 2
                            left join Currency H on D.CurrencyPK = H.CurrencyPK and H.status = 2
                            where A.MaturityDate = @ValueDate and A.TrailsPK = @TrailsPK  ";
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
                        + '|' + case when A.TrxType = 1  then case when A.TradeAmount = 0 then '0' else cast(isnull(cast(A.TradeAmount as decimal(30,2)),0)as nvarchar) end 
                        else case when A.TrxType = 3 then case when A.OldTradeAmount = 0 then '0' else cast(isnull(cast(A.OldTradeAmount as decimal(30,2)),0)as nvarchar) end else cast(isnull(cast(A.TradeAmount as decimal(30,2)),'')as nvarchar) end  end -- 10.Investment.DoneAmount
                         + '|' + case when A.TrxType = 1  then case when A.InterestPercent = 0 then '0' else cast(isnull(cast(A.InterestPercent as decimal(6,4)),0)as nvarchar) end 
                        else case when A.TrxType = 3 then case when A.OldInterestPercent = 0 then '0' else cast(isnull(cast(A.OldInterestPercent as decimal(6,4)),0)as nvarchar) end else cast(isnull(cast(A.InterestPercent as decimal(6,4)),'')as nvarchar) end  end -- 11.Investment.InterestPercent
                        + '|' + case when A.TrxType = 2  then RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(CONVERT(VARCHAR(10), A.AcqDate, 112),'')))) else case when A.TrxType = 1 then RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(CONVERT(VARCHAR(10), ValueDate, 112),'')))) else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(CONVERT(VARCHAR(10), PrevDate, 112),'')))) end end -- 12.Investment.ValueDate
                        + '|' + case when A.TrxType = 1  then RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(CONVERT(VARCHAR(10), A.MaturityDate, 112),'')))) 
                        else case when A.TrxType = 3 then RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(CONVERT(VARCHAR(10), A.OldMaturityDate, 112),'')))) else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(CONVERT(VARCHAR(10), A.OldMaturityDate, 112),'')))) end  end  -- 13.Investment.MaturityDate
                        + '|' + case when A.TrxType = 1  then cast(isnull(A.InterestPaymentType,'') as nvarchar) else case when A.TrxType = 3 then '1' else '' end  end -- 14.Investment.InterestPaymentType           
                        + '|' + case when A.TrxType = 1  then '1' else  '' end  -- 15. Hardcode InterestType
                        + '|' + case when A.TrxType = 1  then case when A.BitSyariah = 1  then 'Y' else 'N' end  else '' end -- 16.Bank.BitSyariah
                        + '|' + case when A.TrxType = 2  then RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(CONVERT(VARCHAR(10), A.ValueDate, 112),''))))   else ''  end -- 17.WithdrawalDate
                        + '|' + case when A.TrxType = 2  then cast(isnull(cast(A.BreakInterestPercent as decimal(6,4)),'')as nvarchar) else '' end -- 18.Investment.BreakInteresPercent
                        + '|' + case when A.TrxType = 2  then cast(isnull(cast(A.TradeAmount as decimal(30,2)),'')as nvarchar)  else '' end -- 19.Withdrawal Principle
                        + '|' + case when A.TrxType = 2  then case when A.InterestAmount = 0 then '0' else cast(isnull(cast(A.InterestAmount as decimal(30,2)),'')as nvarchar) end else '' end -- 20.Withdrawal Interest
                        + '|' + case when A.TrxType = 2  then cast(isnull(cast(A.TradeAmount + A.InterestAmount as decimal(30,2)),'')as nvarchar) else '' end -- 21.Total Withdrawal Amount
                        + '|' + -- 22.Rollover Type
                        + '|' + case when A.TrxType = 3 then cast(isnull(cast(A.TradeAmount as decimal(30,2)),'')as nvarchar) else '' end -- 23.Investment.DoneAmount 
                        + '|' + case when A.TrxType = 3 then cast(isnull(cast(A.InterestPercent as decimal(6,4)),'')as nvarchar) else '' end -- 24.Investment.InterestPercent 
                        + '|' + case when A.TrxType = 3 then RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(CONVERT(VARCHAR(10), MaturityDate, 112),'')))) else '' end -- 25.Investment.MaturityDate
                        + '|' + case when A.TrxType = 2  then cast(isnull(cast((A.TradeAmount + A.InterestAmount) as decimal(30,2)),'')as nvarchar) else case when A.TrxType = 1  then cast(isnull(A.AmountTrf,0) as nvarchar) else '0' end end-- 26.Amount to be Transfer
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
                        case when A.DoneAmount = I.Balance then cast(isnull(A.DoneAmount,0) as decimal(30,2)) else cast(sum(isnull(A.DoneAmount,0) - isnull(I.Balance,0)) as decimal(30,2)) end AmountTrf,A.BreakInterestPercent,A.AcqDate,I.AcqDate PrevDate,
                        round(A.DoneAmount * (A.BreakInterestPercent/100)/365 * datediff(day,A.AcqDate,A.SettlementDate),0) InterestAmount
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
                        A.InterestPercent,A.MaturityDate,F.BitSyariah,G.ContactPerson,G.Phone1,G.Fax1,G.BankAccountName,I.Balance,I.InterestPercent,I.MaturityDate,H.ID,A.InterestPaymentType,A.BreakInterestPercent,A.AcqDate,I.AcqDate

                            " + _mature + @"

                        )A    
                        Group by A.Reference,A.ValueDate,A.settlementdate,A.BrokerCode,A.FundCode,A.Instrument,A.DonePrice,A.Quantity,A.TradeAmount,A.CommissionAmount,A.IncomeTaxSellAmount,
                        A.TransactionType, A.LevyAmount, A.VatAmount, A.OtherCharges,A.TotalAmount, A.WHTAmount, A.InvestmentNotes, A.TrxType,A.SettlementMode,A.LastCouponDate,A.NextCouponDate,A.AccruedDays,
                        A.InterestAmount,A.CapitalGainAmount,A.TaxInterestAmount,A.BankCode,A.BankBranchCode,A.BankName,A.BankAccountNo,A.InterestPercent,A.MaturityDate,A.BitSyariah,A.ContactPerson,A.Phone1,A.Fax1,
                        A.BankAccountName,A.OldTradeAmount,A.OldInterestPercent,A.OldMaturityDate,A.CurrencyID,A.InterestPaymentType,A.AmountTrf,A.BreakInterestPercent,A.AcqDate,A.PrevDate 
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

        public Boolean GenerateReportCompliance(string _userID, OjkRpt _OjkRpt)
        {

            #region SiPesat
            if (_OjkRpt.ReportName.Equals("4"))
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

                                cmd.CommandTimeout = 0;
                                cmd.CommandText = @"
                        
                        DECLARE @Name nvarchar(100),@ID nvarchar(50)
                        DECLARE @NoIDPJK nvarchar(30)


                        select @NoIDPJK = NoIDPJK  from Company where Status in(1,2)
                        set @NoIDPJK = isnull(@NoIDPJK,'')

                        create table #Text(
                        [ResultText] [nvarchar] (1000) NULL
                        )

                        DECLARE @Table TABLE
                        (
	                        FundClientPK INT,
	                        Date Datetime
                        )

                        DECLARE @CFundClientPK INT

                        Declare A Cursor FOR
	                        SELECT DISTINCT fundclientpK FROM dbo.ClientSubscription WHERE status <> 3 AND Posted = 1 AND Revised = 0
                        Open A
                        Fetch Next From A
                        INTO @CFundClientPK

                        While @@FETCH_STATUS = 0  
                        Begin
	
	                        INSERT INTO @Table
                                ( FundClientPK, Date )
                            SELECT TOP 1 FundClientPK,ValueDate FROM dbo.ClientSubscription 
                            WHERE FundClientPK = @CFundClientPK AND status <> 3 AND posted = 1 AND Revised = 0
                            ORDER BY ValueDate asc

	                        Fetch Next From A 
	                        into @CFundClientPK
                        End	
                        Close A
                        Deallocate A

                        Declare @A Table
                        (
                        IDPJK nvarchar(30),InvestorType int,TempatLahir nvarchar(50),
                        Name nvarchar(100),TanggalLahir nvarchar(100), Alamat nvarchar(500),
                        NoKTP nvarchar(50),NoIDlain nvarchar(50),ID nvarchar(50),NPWP nvarchar(50)
                        )


                        Insert Into @A
                        select @NoIDPJK IDPJK ,isnull(InvestorType,'') InvestorType,ISNULL(TempatLahir,'') TempatLahir,
                        ISNULL(Name,'') Name,
                        Case when InvestorType = 1 then convert(nvarchar(15),isnull(TanggalLahir,''),105 ) else '' end  TanggalLahir,
                        Case when InvestorType = 1 then ISNULL(AlamatInd1,'') else AlamatPerusahaan end Alamat,
                        CASE when InvestorType = 1 and IdentitasInd1 in (1,7) then NoIdentitasInd1 else  ' ' end NoKTP,
                        case when InvestorType = 1 and IdentitasInd1 in (2,3,4,5,6) then NoIdentitasInd1 else ' ' end NoIDLain,
                        ISNULL(SID,'') ID,
                        ISNULL(NPWP,'') NPWP
                        from FundClient A
                        LEFT JOIN @Table B ON A.FundClientPK = B.FundClientPK
                        WHERE B.Date between @ValueDateFrom and @ValueDateTo  and A.SACode = ''  and status in (1,2) 
                        AND B.FundClientPK IS NOT null



                        Declare Z Cursor For 

                        select Name,ID from @A
                        group by Name,ID 
                        order by Name asc

                        Open Z                  
                        Fetch Next From Z                  
                        Into @Name,@ID
                        While @@FETCH_STATUS = 0                  
                        Begin 

                        Update @A set Name = @Name where ID = @ID

                        Fetch next From Z                   
                        Into @Name,@ID
                        END                  
                        Close Z                  
                        Deallocate z


                        insert into #Text

                        select distinct IDPJK + '|' + RTRIM(LTRIM(isnull(InvestorType,''))) + '|' +
                        RTRIM(LTRIM(ISNULL(TempatLahir,''))) + '|' +
                        RTRIM(LTRIM(ISNULL(Name,''))) + '|' +
                        isnull(TanggalLahir,'') + '|' + 
                        convert(nvarchar(15),ISNULL(Alamat,'')) + '|' +
                        CAST(NoKTP as nvarchar(30)) + '|' +
                        CAST(NoIDLain as nvarchar(30)) + '|' +
                        RTRIM(LTRIM(ISNULL(ID,''))) + '|' +
                        RTRIM(LTRIM(ISNULL(NPWP,'')))
                        from @A


                        select * from #Text ";
                                cmd.Parameters.AddWithValue("@ValueDateFrom", _OjkRpt.ValueDateFrom);
                                cmd.Parameters.AddWithValue("@ValueDateTo", _OjkRpt.ValueDateTo);

                                using (SqlDataReader dr = cmd.ExecuteReader())
                                {
                                    if (dr.HasRows)
                                    {

                                        string filePath = Tools.ARIATextPath + "Sipesat.txt";
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
                                        }

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


                #endregion

                #region Excel
                else
                {
                    if (_OjkRpt.ReportName.Equals("4"))
                    {
                        int rowcell = 0;
                        try
                        {
                            using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                            {
                                DbCon.Open();
                                using (SqlCommand cmd = DbCon.CreateCommand())
                                {
                                    cmd.CommandText = @"
                        
                        DECLARE @Name nvarchar(100),@ID nvarchar(50)
                        DECLARE @NoIDPJK nvarchar(30)


                        select @NoIDPJK = NoIDPJK  from Company where Status in(1,2)
                        set @NoIDPJK = isnull(@NoIDPJK,'')

                        create table #Text(
                        [ResultText] [nvarchar] (1000) NULL
                        )

                        DECLARE @Table TABLE
                        (
	                        FundClientPK INT,
	                        Date Datetime
                        )

                        DECLARE @CFundClientPK INT

                        Declare A Cursor FOR
	                        SELECT DISTINCT fundclientpK FROM dbo.ClientSubscription WHERE status <> 3 AND Posted = 1 AND Revised = 0
                        Open A
                        Fetch Next From A
                        INTO @CFundClientPK

                        While @@FETCH_STATUS = 0  
                        Begin
	
	                        INSERT INTO @Table
                                ( FundClientPK, Date )
                            SELECT TOP 1 FundClientPK,ValueDate FROM dbo.ClientSubscription 
                            WHERE FundClientPK = @CFundClientPK AND status <> 3 AND posted = 1 AND Revised = 0
                            ORDER BY ValueDate asc

	                        Fetch Next From A 
	                        into @CFundClientPK
                        End	
                        Close A
                        Deallocate A

                        Declare @A Table
                        (
                        IDPJK nvarchar(30),InvestorType int,TempatLahir nvarchar(50),
                        Name nvarchar(100),TanggalLahir nvarchar(100), Alamat nvarchar(500),
                        NoKTP nvarchar(50),NoIDlain nvarchar(50),ID nvarchar(50),NPWP nvarchar(50)
                        )


                        Insert Into @A
                        select @NoIDPJK IDPJK ,isnull(InvestorType,'') InvestorType,ISNULL(TempatLahir,'') TempatLahir,
                        ISNULL(Name,'') Name,
                        Case when InvestorType = 1 then convert(nvarchar(15),isnull(TanggalLahir,''),106 ) else '' end  TanggalLahir,
                        Case when InvestorType = 1 then ISNULL(AlamatInd1,'') else AlamatPerusahaan end Alamat,
                        CASE when InvestorType = 1 and IdentitasInd1 in (1,7) then NoIdentitasInd1 else  ' ' end NoKTP,
                        case when InvestorType = 1 and IdentitasInd1 in (2,3,4,5,6) then NoIdentitasInd1 else ' ' end NoIDLain,
                        ISNULL(SID,'') ID,
                        ISNULL(NPWP,'') NPWP
                        from FundClient A
                        LEFT JOIN @Table B ON A.FundClientPK = B.FundClientPK
                        WHERE B.Date between @ValueDateFrom and @ValueDateTo  and A.SACode = ''  and status in (1,2) 
                        AND B.FundClientPK IS NOT null



                        Declare Z Cursor For 

                        select Name,ID from @A
                        group by Name,ID 
                        order by Name asc

                        Open Z                  
                        Fetch Next From Z                  
                        Into @Name,@ID
                        While @@FETCH_STATUS = 0                  
                        Begin 

                        Update @A set Name = @Name where ID = @ID

                        Fetch next From Z                   
                        Into @Name,@ID
                        END                  
                        Close Z                  
                        Deallocate z

                        select distinct IDPJK,InvestorType,TempatLahir,Name,TanggalLahir,Alamat,NoKTP,NoIDLain,ID,NPWP from @A 

                        ";
                                    cmd.CommandTimeout = 0;
                                    cmd.Parameters.AddWithValue("@ValueDateFrom", _OjkRpt.ValueDateFrom);
                                    cmd.Parameters.AddWithValue("@ValueDateTo", _OjkRpt.ValueDateTo);
                                    using (SqlDataReader dr0 = cmd.ExecuteReader())
                                    {
                                        if (!dr0.HasRows)
                                        {
                                            return false;
                                        }
                                        else
                                        {
                                            string filePath = Tools.ReportsPath + "Sipesat_" + _userID + ".xlsx";
                                            int incRowExcel = 0;
                                            int _startRowDetail, _endRowDetail;
                                            FileInfo excelFile = new FileInfo(filePath);
                                            if (excelFile.Exists)
                                            {
                                                excelFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                                excelFile = new FileInfo(filePath);
                                            }

                                            // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                            using (ExcelPackage package = new ExcelPackage(excelFile))
                                            {
                                                package.Workbook.Properties.Title = "Sipesat";
                                                package.Workbook.Properties.Author = Tools.DefaultReportAuthor();
                                                package.Workbook.Properties.Comments = Tools.DefaultReportComments();
                                                package.Workbook.Properties.Company = Tools.DefaultReportCompany();
                                                package.Workbook.Properties.SetCustomPropertyValue("Checked by", _userID);
                                                package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", Tools.DefaultReportAssemblyName());

                                                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("SiPesatReport");

                                                List<SipesatRpt> rList = new List<SipesatRpt>();
                                                while (dr0.Read())
                                                {
                                                    SipesatRpt rSingle = new SipesatRpt();

                                                    rSingle.InvestorType = Convert.ToInt32(dr0["InvestorType"]);
                                                    rSingle.ClientName = Convert.ToString(dr0["Name"]);
                                                    rSingle.TempatLahir = Convert.ToString(dr0["TempatLahir"]);
                                                    rSingle.TanggalLahir = Convert.ToString(dr0["TanggalLahir"]);
                                                    rSingle.Alamat = Convert.ToString(dr0["Alamat"]);
                                                    rSingle.NoKTP = Convert.ToString(dr0["NoKTP"]);
                                                    rSingle.NoIDLain = Convert.ToString(dr0["NoIDLain"]);
                                                    rSingle.ID = Convert.ToString(dr0["ID"]);
                                                    rSingle.NPWP = Convert.ToString(dr0["NPWP"]);
                                                    rSingle.IDPJK = Convert.ToString(dr0["IDPJK"]);

                                                    rList.Add(rSingle);
                                                }

                                                var GroupByReference =
                                                    from r in rList
                                                    group r by new { } into rGroup
                                                    select rGroup;
                                                foreach (var rsHeader in GroupByReference)
                                                {

                                                    incRowExcel++;
                                                    _startRowDetail = incRowExcel;
                                                    worksheet.Cells[incRowExcel, 1].Value = "ID PJK";
                                                    worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                                    worksheet.Cells[incRowExcel, 2].Value = "Kode Nasabah";
                                                    worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                                    worksheet.Cells[incRowExcel, 3].Value = "Nama Nasabah";
                                                    worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;
                                                    worksheet.Cells[incRowExcel, 4].Value = "Tempat Lahir";
                                                    worksheet.Cells[incRowExcel, 4].Style.Font.Bold = true;
                                                    worksheet.Cells[incRowExcel, 5].Value = "Tanggal Lahir";
                                                    worksheet.Cells[incRowExcel, 5].Style.Font.Bold = true;
                                                    worksheet.Cells[incRowExcel, 6].Value = "Alamat";
                                                    worksheet.Cells[incRowExcel, 6].Style.Font.Bold = true;
                                                    worksheet.Cells[incRowExcel, 7].Value = "KTP";
                                                    worksheet.Cells[incRowExcel, 7].Style.Font.Bold = true;
                                                    worksheet.Cells[incRowExcel, 8].Value = "Identitas Lain";
                                                    worksheet.Cells[incRowExcel, 8].Style.Font.Bold = true;
                                                    worksheet.Cells[incRowExcel, 9].Value = "CIF/Kepesertaan";
                                                    worksheet.Cells[incRowExcel, 9].Style.Font.Bold = true;
                                                    worksheet.Cells[incRowExcel, 10].Value = "NPWP";
                                                    worksheet.Cells[incRowExcel, 10].Style.Font.Bold = true;
                                                    worksheet.Cells[incRowExcel, 1, incRowExcel, 10].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                    incRowExcel++;

                                                    //end area header
                                                    foreach (var rsDetail in rsHeader)
                                                    {

                                                        worksheet.Cells[incRowExcel, 1].Value = rsDetail.IDPJK;
                                                        worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                        worksheet.Cells[incRowExcel, 2].Value = rsDetail.InvestorType;
                                                        worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                        worksheet.Cells[incRowExcel, 3].Value = rsDetail.ClientName;
                                                        worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                        worksheet.Cells[incRowExcel, 3].Style.WrapText = true;

                                                        worksheet.Cells[incRowExcel, 4].Value = rsDetail.TempatLahir;
                                                        worksheet.Cells[incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                        worksheet.Cells[incRowExcel, 5].Value = rsDetail.TanggalLahir;
                                                        worksheet.Cells[incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                        worksheet.Cells[incRowExcel, 6].Value = rsDetail.Alamat;
                                                        worksheet.Cells[incRowExcel, 6].Style.WrapText = true;
                                                        worksheet.Cells[incRowExcel, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                        //worksheet.Cells[incRowExcel, 6].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                                        worksheet.Cells[incRowExcel, 7].Value = rsDetail.NoKTP;
                                                        worksheet.Cells[incRowExcel, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                        worksheet.Cells[incRowExcel, 8].Value = rsDetail.NoIDLain;
                                                        worksheet.Cells[incRowExcel, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                        worksheet.Cells[incRowExcel, 9].Value = rsDetail.ID;
                                                        worksheet.Cells[incRowExcel, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                        worksheet.Cells[incRowExcel, 10].Value = rsDetail.NPWP;
                                                        worksheet.Cells[incRowExcel, 10].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 10].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                                        _endRowDetail = incRowExcel;
                                                        incRowExcel++;

                                                        worksheet.Cells["A" + _startRowDetail + ":J" + _startRowDetail].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                        worksheet.Cells["A" + _startRowDetail + ":J" + _startRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                        worksheet.Cells["A" + _startRowDetail + ":J" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                        worksheet.Cells["A" + _startRowDetail + ":J" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Medium;

                                                    }
                                                    int _endRow = incRowExcel - 1;
                                                    worksheet.Cells["A" + _endRow + ":J" + _endRow].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                    incRowExcel++;
                                                    worksheet.Row(incRowExcel).PageBreak = true;
                                                }


                                                // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                                worksheet.PrinterSettings.FitToPage = true;
                                                worksheet.PrinterSettings.FitToWidth = 1;
                                                worksheet.PrinterSettings.FitToHeight = 0;
                                                worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel, 10];
                                                worksheet.Cells.AutoFitColumns(0);
                                                worksheet.Column(1).Width = 20;
                                                worksheet.Column(2).Width = 20;
                                                worksheet.Column(3).Width = 40;
                                                worksheet.Column(4).Width = 20;
                                                worksheet.Column(5).Width = 20;
                                                worksheet.Column(6).Width = 40;
                                                worksheet.Column(7).Width = 20;
                                                worksheet.Column(8).Width = 20;
                                                worksheet.Column(9).Width = 20;
                                                worksheet.Column(10).Width = 20;
                                                worksheet.PrinterSettings.Orientation = eOrientation.Portrait; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                                // worksheet.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN
                                                // worksheet.PrinterSettings.FitToPage = true;
                                                //worksheet.HeaderFooter.OddHeader.RightAlignedText = "&14 PAYMENT VOUCHER";
                                                // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                                worksheet.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
                                                worksheet.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
                                                //worksheet.View.PageLayoutView = Tools.DefaultReportPageLayoutView();
                                                worksheet.View.ShowGridLines = Tools.DefaultReportShowGridLines();
                                                worksheet.HeaderFooter.OddHeader.CenteredText = "&14 SiPesat Report";
                                                worksheet.HeaderFooter.OddHeader.RightAlignedText = Tools.DefaultReportHeaderLeftText();

                                                Image img = Image.FromFile(Tools.ReportImage);
                                                worksheet.HeaderFooter.OddHeader.InsertPicture(img, PictureAlignment.Left);
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

                }

                #endregion

                return true;
            }//else if
            #endregion

            #region KPD
            else if (_OjkRpt.ReportName.Equals("5"))
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


                                string _paramFund = "";

                                if (!_host.findString(_OjkRpt.Fund.ToLower(), "0", ",") && !string.IsNullOrEmpty(_OjkRpt.Fund))
                                {
                                    _paramFund = "And A.FundPK in ( " + _OjkRpt.Fund + " ) ";
                                }
                                else
                                {
                                    _paramFund = "";
                                }

                                cmd.CommandTimeout = 0;
                                cmd.CommandText = @"

                        create table #Text(      
                        [ResultText] [nvarchar](1000)  NULL          
                        )                        
        
                        truncate table #Text --     

          DECLARE @CFundPK INT
DECLARE @CFundClientPK INT

DECLARE @NAV TABLE
(
	FundPK INT,
	NAV NUMERIC(22,4)
)

INSERT INTO @NAV
        ( FundPK, NAV )
SELECT FundPK,ISNULL(A.Nav,0) FROM dbo.CloseNAV A
WHERE Date = (
SELECT MAX(date) FROM closeNAV WHERE status = 2 AND Date <= @Date
) " + _paramFund + @"


DECLARE @Text table (      
[ResultText] [nvarchar](1000)  NULL          
)                        

--drop Table @KPD--
DECLARE @KPD Table 
(
	KodeNasabah NVARCHAR(5),
	NamaNasabah NVARCHAR(200),
	NoKontrak NVARCHAR(100),
	TglKontrakFrom NVARCHAR(50),
	TglKontrakTo NVARCHAR(50),
	NoAdendum NVARCHAR(100),
	TglAdendum NVARCHAR(100),
	NilaiInvestasiAwalIDR NUMERIC(22,4),
	NilaiInvestasiAwalNonIDR NUMERIC(22,4),
	NilaiInvestasiAkhirIDR NUMERIC(22,4),
	NilaiInvestasiAkhirNonIDR NUMERIC(22,4),
	JenisEfek NVARCHAR(100),
	DnLn NVARCHAR(2),
	JumlahEfek NUMERIC(22,4),
	NilaiPembelian NUMERIC(22,4),
	NilaiNominal NUMERIC(22,4),
	HPW NUMERIC(22,4),
	Deposito NUMERIC(22,4),
	TotalNilai NUMERIC(22,4),
	KodeBK NVARCHAR(10),
	Keterangan NVARCHAR(50),
	SID NVARCHAR(100)
)

DECLARE A CURSOR FOR 
SELECT distinct A.FundPK,A.FundClientPK 
from dbo.FundClientPosition A
LEFT JOIN Fund B ON A.FundPK = B.FundPK AND B.status IN (1,2)
where A.Date = @Date and B.FundTypeInternal = 2
AND A.UnitAmount > 1  " + _paramFund + @"
Open A
Fetch Next From A
Into @CFundPK,@CFundClientPK
While @@FETCH_STATUS = 0
Begin
	
	INSERT INTO @KPD
	        ( KodeNasabah ,
	          NamaNasabah ,
	          NoKontrak ,
	          TglKontrakFrom ,
	          TglKontrakTo ,
	          NoAdendum ,
	          TglAdendum ,
	          NilaiInvestasiAwalIDR ,
	          NilaiInvestasiAwalNonIDR ,
	          NilaiInvestasiAkhirIDR ,
	          NilaiInvestasiAkhirNonIDR ,
	          JenisEfek ,
	          DnLn ,
	          JumlahEfek ,
	          NilaiPembelian ,
	          NilaiNominal ,
	          HPW ,
	          Deposito ,
	          TotalNilai ,
	          KodeBK ,
	          Keterangan ,
	          SID
	        )
	SELECT ISNULL(B.InvestorType,1) 
	,ISNULL(B.Name,'') 
	,ISNULL(C.KPDNoContract,'') 
	,isnull(CONVERT(VARCHAR(8), C.KPDDateFromContract, 112),0)
	,isnull(CONVERT(VARCHAR(8), C.KPDDateToContract, 112),0)
	,ISNULL(C.KPDNoAdendum,'') 
	,isnull(CONVERT(VARCHAR(8), C.KPDDateAdendum, 112),0)
	,ISNULL(D.TotalInvestasiAwalIDR,0)
	,ISNULL(E.TotalInvestasiAwalNonIDR,0)
	,ISNULL(D.TotalUnitIDR,0) * ISNULL(F.NAV,0)
	,ISNULL(E.TotalUnitNonIDR,0) * ISNULL(F.NAV,0)
	,ISNULL(G.InstrumentID,'') JenisEfek
	,1 DnLn
	,ISNULL(G.Balance,0) JumlahEfek
	,ISNULL(G.CostValue,0) NilaiPembelian
	,0 NilaiNominal
	,ISNULL(G.ClosePrice,0) HPW
	,ISNULL(H.BalanceDeposito,0) Deposito
	,ISNULL(G.Balance,0) * ISNULL(G.ClosePrice,0) TotalNilai
	,ISNULL(J.NKPDCode,'') KodeBK
	,ISNULL(G.InstrumentID,'') Keterangan
	,ISNULL(B.SID,'') SID
	FROM dbo.FundClientPosition A
	LEFT JOIN FundClient B ON A.FundClientPK = B.FundClientPK AND B.status  IN (1,2)
	LEFT JOIN Fund C ON A.FundPK = C.FundPK AND C.status IN (1,2)
	LEFT JOIN
    (
		SELECT FundClientPK, SUM(ISNULL(A.CashAmount,0)) TotalInvestasiAwalIDR,SUM(ISNULL(A.TotalUnitAmount,0)) TotalUnitIDR FROM dbo.ClientSubscription A
		WHERE A.FundPK = @CFundPK AND A.status <> 3 AND A.Posted = 1 AND A.Revised =0
		AND A.CurrencyPK = 1 AND A.FundclientPK = @CFundClientPK
		GROUP BY A.FundClientPK
	)D ON A.FundClientPK = D.FundClientPK
	LEFT JOIN
    (
		SELECT FundClientPK, SUM(ISNULL(A.CashAmount,0)) TotalInvestasiAwalNonIDR 
		,SUM(ISNULL(A.TotalUnitAmount,0)) TotalUnitNonIDR
		FROM dbo.ClientSubscription A
		WHERE A.FundPK = @CFundPK AND A.status <> 3 AND A.Posted = 1 AND A.Revised =0
		AND A.CurrencyPK <> 1 AND A.FundclientPK = @CFundClientPK
		GROUP BY A.FundClientPK
	)E ON A.FundClientPK = D.FundClientPK
	LEFT JOIN @NAV F ON A.FundPK = F.FundPK
	LEFT JOIN
    (
		SELECT A.FundPK,A.InstrumentID,ISNULL(A.Balance,0) Balance 
		,ISNULL(A.CostValue,0) CostValue
		,ISNULL(A.ClosePrice,0) ClosePrice
		FROM dbo.FundPosition A
		WHERE A.Date = @Date
	)G ON A.FundPK = G.FundPK
	LEFT JOIN 
	(
		SELECT FundPK, SUM(ISNULL(A.Balance,0)) BalanceDeposito
		FROM dbo.FundPosition A
		LEFT JOIN dbo.Instrument B ON A.InstrumentPK = B.InstrumentPK AND B.status IN (1,2)
		WHERE A.Date = @Date AND B.InstrumentTypePK = 5
		GROUP BY A.FundPK
	)H ON A.FundPK = H.FundPK
	LEFT JOIN dbo.BankBranch I ON C.BankBranchPK = I.BankBranchPK AND I.status IN (1,2)
	LEFT JOIN Bank J ON I.BankPK = J.BankPK AND J.status IN (1,2)
	WHERE A.Date = @Date AND A.FundPK = @CFundPK AND A.FundclientPK = @CFundClientPK

	
	UNION ALL

	SELECT  ISNULL(B.InvestorType,1) 
	,'0'
	,ISNULL(C.KPDNoContract,'') 
	,isnull(CONVERT(VARCHAR(8), C.KPDDateFromContract, 112),0)
	,'0'
	,'0'
	,'0'
	,'0'
	,'0'
	,'0'
	,'0'
	,'998'
	,'1'
	,'0'
	,'0'
	,'0'
	,'0'
	,'0'
	,[dbo].[FGetGroupAccountFundJournalBalanceByFundPK](@Date,3,@CFundPK)
	,ISNULL(J.NKPDCode,'') KodeBK
	,'0'
	,ISNULL(B.SID,'') SID
	FROM dbo.FundClientPosition A
	LEFT JOIN FundClient B ON A.FundClientPK = B.FundClientPK AND B.status  IN (1,2)
	LEFT JOIN Fund C ON A.FundPK = C.FundPK AND C.status IN (1,2)
	LEFT JOIN dbo.BankBranch I ON C.BankBranchPK = I.BankBranchPK AND I.status IN (1,2)
	LEFT JOIN Bank J ON I.BankPK = J.BankPK AND J.status IN (1,2)
	WHERE A.Date = @Date AND A.FundPK = @CFundPK AND A.FundclientPK = @CFundClientPK
	

	UNION ALL

	SELECT  ISNULL(B.InvestorType,1) 
	,'0'
	,ISNULL(C.KPDNoContract,'') 
	,isnull(CONVERT(VARCHAR(8), C.KPDDateFromContract, 112),0)
	,'0'
	,'0'
	,'0'
	,'0'
	,'0'
	,'0'
	,'0'
	,'997'
	,'1'
	,'0'
	,'0'
	,'0'
	,'0'
	,'0'
	,[dbo].[FGetGroupAccountFundJournalBalanceByFundPK](@Date,40,@CFundPK)
	,ISNULL(J.NKPDCode,'') KodeBK
	,'0'
	,ISNULL(B.SID,'') SID
	FROM dbo.FundClientPosition A
	LEFT JOIN FundClient B ON A.FundClientPK = B.FundClientPK AND B.status  IN (1,2)
	LEFT JOIN Fund C ON A.FundPK = C.FundPK AND C.status IN (1,2)
	LEFT JOIN dbo.BankBranch I ON C.BankBranchPK = I.BankBranchPK AND I.status IN (1,2)
	LEFT JOIN Bank J ON I.BankPK = J.BankPK AND J.status IN (1,2)
	WHERE A.Date = @Date AND A.FundPK = @CFundPK AND A.FundclientPK = @CFundClientPK

	UNION ALL

	SELECT  ISNULL(B.InvestorType,1) 
	,'0'
	,ISNULL(C.KPDNoContract,'') 
	,isnull(CONVERT(VARCHAR(8), C.KPDDateFromContract, 112),0)
	,'0'
	,'0'
	,'0'
	,'0'
	,'0'
	,'0'
	,'0'
	,'996'
	,'1'
	,'0'
	,'0'
	,'0'
	,'0'
	,'0'
	,[dbo].[FGetGroupAccountFundJournalBalanceByFundPK](@Date,64,@CFundPK)
	,ISNULL(J.NKPDCode,'') KodeBK
	,'0'
	,ISNULL(B.SID,'') SID
	FROM dbo.FundClientPosition A
	LEFT JOIN FundClient B ON A.FundClientPK = B.FundClientPK AND B.status  IN (1,2)
	LEFT JOIN Fund C ON A.FundPK = C.FundPK AND C.status IN (1,2)
	LEFT JOIN dbo.BankBranch I ON C.BankBranchPK = I.BankBranchPK AND I.status IN (1,2)
	LEFT JOIN Bank J ON I.BankPK = J.BankPK AND J.status IN (1,2)
	WHERE A.Date = @Date AND A.FundPK = @CFundPK AND A.FundclientPK = @CFundClientPK



Fetch next From A Into @CFundPK,@CFundClientPK
end
Close A
Deallocate A

insert into #Text
 
select 
isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(KodeNasabah,'')))),'') + --1
'|' + isnull(RTRIM(LTRIM(isnull(NamaNasabah,''))),'')  +    --2
'|' + isnull(RTRIM(LTRIM(isnull(NoKontrak,''))),'')  +  --3
'|' + isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(TglKontrakFrom,'')))),'')  +  --4
'|' + isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(TglKontrakTo,'')))),'')  +  --5
'|' + isnull(RTRIM(LTRIM(isnull(NoAdendum,''))),'')  +  --6
'|' + isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(TglAdendum,'')))),'')  +  --7
'|' + isnull(RTRIM(LTRIM(isnull(NilaiInvestasiAwalIDR,0))),'')  +  --8
'|' + isnull(RTRIM(LTRIM(isnull(NilaiInvestasiAwalNonIDR,0))),'')  + --9
'|' + isnull(RTRIM(LTRIM(isnull(NilaiInvestasiAkhirIDR,0))),'')  + --10
'|' + isnull(RTRIM(LTRIM(isnull(NilaiInvestasiAkhirNonIDR,0))),'')  +  --11
'|' + isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(JenisEfek,'')))),'')  +  --12
'|' + isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(DnLn,'')))),'')  + --13
'|' + isnull(RTRIM(LTRIM(isnull(JumlahEfek,0))),'')  + --14
'|' + isnull(RTRIM(LTRIM(isnull(NilaiPembelian,0))),'')  +  --15
'|' + isnull(RTRIM(LTRIM(isnull(NilaiNominal,0))),'')  +  --16
'|' + isnull(RTRIM(LTRIM(isnull(HPW,0))),'')  +  --17
'|' + isnull(RTRIM(LTRIM(isnull(Deposito,0))),'')  +  --18
'|' + isnull(RTRIM(LTRIM(isnull(TotalNilai,0))),'')  +  --19
'|' + isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(KodeBK,'')))),'') + --20
'|' + isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(Keterangan,'')))),'') + --21
'|' + isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(SID,'')))),'') --22
from @KPD

select * from #text

                         ";
                                cmd.Parameters.AddWithValue("@Date", _OjkRpt.Date);
                                //cmd.Parameters.AddWithValue("@FundPK", _Fund);

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
                                            //return Tools.HtmlARIATextPath + _companyCode + "KPD.txt";
                                            return true;
                                        }

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

                                string _paramFund = "";

                                if (!_host.findString(_OjkRpt.Fund.ToLower(), "0", ",") && !string.IsNullOrEmpty(_OjkRpt.Fund))
                                {
                                    _paramFund = "And A.FundPK in ( " + _OjkRpt.Fund + " ) ";
                                }
                                else
                                {
                                    _paramFund = "";
                                }

                                cmd.CommandTimeout = 0;
                                cmd.CommandText =
                                @"

DECLARE @CFundPK INT
DECLARE @CFundClientPK INT

DECLARE @NAV TABLE
(
	FundPK INT,
	NAV NUMERIC(22,4)
)

INSERT INTO @NAV
        ( FundPK, NAV )
SELECT FundPK,ISNULL(A.Nav,0) FROM dbo.CloseNAV A
WHERE Date = (
SELECT MAX(date) FROM closeNAV WHERE status = 2 AND Date <= @Date
) " + _paramFund + @"


DECLARE @Text table (      
[ResultText] [nvarchar](1000)  NULL          
)                        

--drop Table @KPD--
DECLARE @KPD Table 
(
	KodeNasabah NVARCHAR(5),
	NamaNasabah NVARCHAR(200),
	NoKontrak NVARCHAR(100),
	TglKontrakFrom NVARCHAR(50),
	TglKontrakTo NVARCHAR(50),
	NoAdendum NVARCHAR(100),
	TglAdendum NVARCHAR(100),
	NilaiInvestasiAwalIDR NUMERIC(22,4),
	NilaiInvestasiAwalNonIDR NUMERIC(22,4),
	NilaiInvestasiAkhirIDR NUMERIC(22,4),
	NilaiInvestasiAkhirNonIDR NUMERIC(22,4),
	JenisEfek NVARCHAR(100),
	DnLn NVARCHAR(2),
	JumlahEfek NUMERIC(22,4),
	NilaiPembelian NUMERIC(22,4),
	NilaiNominal NUMERIC(22,4),
	HPW NUMERIC(22,4),
	Deposito NUMERIC(22,4),
	TotalNilai NUMERIC(22,4),
	KodeBK NVARCHAR(10),
	Keterangan NVARCHAR(50),
	SID NVARCHAR(100)
)

DECLARE A CURSOR FOR 
SELECT distinct A.FundPK,A.FundClientPK 
from dbo.FundClientPosition A
LEFT JOIN Fund B ON A.FundPK = B.FundPK AND B.status IN (1,2)
where A.Date = @Date and B.FundTypeInternal = 2 
AND A.UnitAmount > 1 " + _paramFund + @"
Open A
Fetch Next From A
Into @CFundPK,@CFundClientPK
While @@FETCH_STATUS = 0
Begin
	
	INSERT INTO @KPD
	        ( KodeNasabah ,
	          NamaNasabah ,
	          NoKontrak ,
	          TglKontrakFrom ,
	          TglKontrakTo ,
	          NoAdendum ,
	          TglAdendum ,
	          NilaiInvestasiAwalIDR ,
	          NilaiInvestasiAwalNonIDR ,
	          NilaiInvestasiAkhirIDR ,
	          NilaiInvestasiAkhirNonIDR ,
	          JenisEfek ,
	          DnLn ,
	          JumlahEfek ,
	          NilaiPembelian ,
	          NilaiNominal ,
	          HPW ,
	          Deposito ,
	          TotalNilai ,
	          KodeBK ,
	          Keterangan ,
	          SID
	        )
	SELECT ISNULL(B.InvestorType,1) 
	,ISNULL(B.Name,'') 
	,ISNULL(C.KPDNoContract,'') 
	,isnull(CONVERT(VARCHAR(8), C.KPDDateFromContract, 112),0)
	,isnull(CONVERT(VARCHAR(8), C.KPDDateToContract, 112),0)
	,ISNULL(C.KPDNoAdendum,'') 
	,isnull(CONVERT(VARCHAR(8), C.KPDDateAdendum, 112),0)
	,ISNULL(D.TotalInvestasiAwalIDR,0)
	,ISNULL(E.TotalInvestasiAwalNonIDR,0)
	,ISNULL(D.TotalUnitIDR,0) * ISNULL(F.NAV,0)
	,ISNULL(E.TotalUnitNonIDR,0) * ISNULL(F.NAV,0)
	,ISNULL(G.InstrumentID,'') JenisEfek
	,1 DnLn
	,ISNULL(G.Balance,0) JumlahEfek
	,ISNULL(G.CostValue,0) NilaiPembelian
	,0 NilaiNominal
	,ISNULL(G.ClosePrice,0) HPW
	,ISNULL(H.BalanceDeposito,0) Deposito
	,ISNULL(G.Balance,0) * ISNULL(G.ClosePrice,0) TotalNilai
	,ISNULL(J.NKPDCode,'') KodeBK
	,ISNULL(G.InstrumentID,'') Keterangan
	,ISNULL(B.SID,'') SID
	FROM dbo.FundClientPosition A
	LEFT JOIN FundClient B ON A.FundClientPK = B.FundClientPK AND B.status  IN (1,2)
	LEFT JOIN Fund C ON A.FundPK = C.FundPK AND C.status IN (1,2)
	LEFT JOIN
    (
		SELECT FundClientPK, SUM(ISNULL(A.CashAmount,0)) TotalInvestasiAwalIDR,SUM(ISNULL(A.TotalUnitAmount,0)) TotalUnitIDR FROM dbo.ClientSubscription A
		WHERE A.FundPK = @CFundPK AND A.status <> 3 AND A.Posted = 1 AND A.Revised =0
		AND A.CurrencyPK = 1 AND A.FundclientPK = @CFundClientPK
		GROUP BY A.FundClientPK
	)D ON A.FundClientPK = D.FundClientPK
	LEFT JOIN
    (
		SELECT FundClientPK, SUM(ISNULL(A.CashAmount,0)) TotalInvestasiAwalNonIDR 
		,SUM(ISNULL(A.TotalUnitAmount,0)) TotalUnitNonIDR
		FROM dbo.ClientSubscription A
		WHERE A.FundPK = @CFundPK AND A.status <> 3 AND A.Posted = 1 AND A.Revised =0
		AND A.CurrencyPK <> 1 AND A.FundclientPK = @CFundClientPK
		GROUP BY A.FundClientPK
	)E ON A.FundClientPK = D.FundClientPK
	LEFT JOIN @NAV F ON A.FundPK = F.FundPK
	LEFT JOIN
    (
		SELECT A.FundPK,A.InstrumentID,ISNULL(A.Balance,0) Balance 
		,ISNULL(A.CostValue,0) CostValue
		,ISNULL(A.ClosePrice,0) ClosePrice
		FROM dbo.FundPosition A
		WHERE A.Date = @Date
	)G ON A.FundPK = G.FundPK
	LEFT JOIN 
	(
		SELECT FundPK, SUM(ISNULL(A.Balance,0)) BalanceDeposito
		FROM dbo.FundPosition A
		LEFT JOIN dbo.Instrument B ON A.InstrumentPK = B.InstrumentPK AND B.status IN (1,2)
		WHERE A.Date = @Date AND B.InstrumentTypePK = 5
		GROUP BY A.FundPK
	)H ON A.FundPK = H.FundPK
	LEFT JOIN dbo.BankBranch I ON C.BankBranchPK = I.BankBranchPK AND I.status IN (1,2)
	LEFT JOIN Bank J ON I.BankPK = J.BankPK AND J.status IN (1,2)
	WHERE A.Date = @Date AND A.FundPK = @CFundPK AND A.FundclientPK = @CFundClientPK

	
	UNION ALL

	SELECT  ISNULL(B.InvestorType,1) 
	,'0'
	,ISNULL(C.KPDNoContract,'') 
	,isnull(CONVERT(VARCHAR(8), C.KPDDateFromContract, 112),0)
	,'0'
	,'0'
	,'0'
	,'0'
	,'0'
	,'0'
	,'0'
	,'998'
	,'1'
	,'0'
	,'0'
	,'0'
	,'0'
	,'0'
	,[dbo].[FGetGroupAccountFundJournalBalanceByFundPK](@Date,3,@CFundPK)
	,ISNULL(J.NKPDCode,'') KodeBK
	,'0'
	,ISNULL(B.SID,'') SID
	FROM dbo.FundClientPosition A
	LEFT JOIN FundClient B ON A.FundClientPK = B.FundClientPK AND B.status  IN (1,2)
	LEFT JOIN Fund C ON A.FundPK = C.FundPK AND C.status IN (1,2)
	LEFT JOIN dbo.BankBranch I ON C.BankBranchPK = I.BankBranchPK AND I.status IN (1,2)
	LEFT JOIN Bank J ON I.BankPK = J.BankPK AND J.status IN (1,2)
	WHERE A.Date = @Date AND A.FundPK = @CFundPK AND A.FundclientPK = @CFundClientPK
	

	UNION ALL

	SELECT  ISNULL(B.InvestorType,1) 
	,'0'
	,ISNULL(C.KPDNoContract,'') 
	,isnull(CONVERT(VARCHAR(8), C.KPDDateFromContract, 112),0)
	,'0'
	,'0'
	,'0'
	,'0'
	,'0'
	,'0'
	,'0'
	,'997'
	,'1'
	,'0'
	,'0'
	,'0'
	,'0'
	,'0'
	,[dbo].[FGetGroupAccountFundJournalBalanceByFundPK](@Date,40,@CFundPK)
	,ISNULL(J.NKPDCode,'') KodeBK
	,'0'
	,ISNULL(B.SID,'') SID
	FROM dbo.FundClientPosition A
	LEFT JOIN FundClient B ON A.FundClientPK = B.FundClientPK AND B.status  IN (1,2)
	LEFT JOIN Fund C ON A.FundPK = C.FundPK AND C.status IN (1,2)
	LEFT JOIN dbo.BankBranch I ON C.BankBranchPK = I.BankBranchPK AND I.status IN (1,2)
	LEFT JOIN Bank J ON I.BankPK = J.BankPK AND J.status IN (1,2)
	WHERE A.Date = @Date AND A.FundPK = @CFundPK AND A.FundclientPK = @CFundClientPK

	UNION ALL

	SELECT  ISNULL(B.InvestorType,1) 
	,'0'
	,ISNULL(C.KPDNoContract,'') 
	,isnull(CONVERT(VARCHAR(8), C.KPDDateFromContract, 112),0)
	,'0'
	,'0'
	,'0'
	,'0'
	,'0'
	,'0'
	,'0'
	,'996'
	,'1'
	,'0'
	,'0'
	,'0'
	,'0'
	,'0'
	,[dbo].[FGetGroupAccountFundJournalBalanceByFundPK](@Date,64,@CFundPK)
	,ISNULL(J.NKPDCode,'') KodeBK
	,'0'
	,ISNULL(B.SID,'') SID
	FROM dbo.FundClientPosition A
	LEFT JOIN FundClient B ON A.FundClientPK = B.FundClientPK AND B.status  IN (1,2)
	LEFT JOIN Fund C ON A.FundPK = C.FundPK AND C.status IN (1,2)
	LEFT JOIN dbo.BankBranch I ON C.BankBranchPK = I.BankBranchPK AND I.status IN (1,2)
	LEFT JOIN Bank J ON I.BankPK = J.BankPK AND J.status IN (1,2)
	WHERE A.Date = @Date AND A.FundPK = @CFundPK AND A.FundclientPK = @CFundClientPK



Fetch next From A Into @CFundPK,@CFundClientPK
end
Close A
Deallocate A

SELECT * FROM @KPD
                            ";


                                //cmd.Parameters.AddWithValue("@date", _sInvestRpt.ParamDate);

                                cmd.Parameters.AddWithValue("@Date", _OjkRpt.Date);
                                //cmd.Parameters.AddWithValue("@FundPK", _sInvestRpt.FundFrom);

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
                                                rSingle.NomorKontrak = Convert.ToString(dr0["NoKontrak"]);
                                                rSingle.TanggalKontrak = Convert.ToString(dr0["TglKontrakFrom"]);
                                                rSingle.TanggalJatuhTempo = Convert.ToString(dr0["TglKontrakTo"]);
                                                rSingle.NomorAdendum = Convert.ToString(dr0["NoAdendum"]);
                                                rSingle.TanggalAdendum = Convert.ToString(dr0["TglAdendum"]);
                                                rSingle.NilaiInvestasiAwalIDR = Convert.ToString(dr0["NilaiInvestasiAwalIDR"]);
                                                rSingle.NilaiInvestasiAwalNonIDR = Convert.ToString(dr0["NilaiInvestasiAwalNonIDR"]);
                                                rSingle.NilaiInvestasiAkhir = Convert.ToString(dr0["NilaiInvestasiAkhirIDR"]);
                                                rSingle.NilaiInvestasiAkhirNonIDR = Convert.ToString(dr0["NilaiInvestasiAkhirNonIDR"]);
                                                rSingle.JenisEfek = Convert.ToString(dr0["JenisEfek"]);
                                                rSingle.KodeKategoriEfek = Convert.ToInt32(dr0["DnLn"]);
                                                rSingle.JumlahEfek = Convert.ToString(dr0["JumlahEfek"]);
                                                rSingle.NilaiPembelian = Convert.ToString(dr0["NilaiPembelian"]);
                                                rSingle.NilaiNominal = Convert.ToString(dr0["NilaiNominal"]);
                                                rSingle.HPW = Convert.ToString(dr0["HPW"]);
                                                rSingle.Deposito = Convert.ToString(dr0["Deposito"]);
                                                rSingle.TotalInvestasi = Convert.ToString(dr0["TotalNilai"]);
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
                                                worksheet.Cells[incRowExcel, 10].Value = "Nilai investasi Akhir IDR";
                                                worksheet.Cells[incRowExcel, 11].Value = "Nilai investasi Akhir Non IDR";
                                                worksheet.Cells[incRowExcel, 12].Value = "Kode Efek";
                                                worksheet.Cells[incRowExcel, 13].Value = "Kode Kategori Efek";
                                                worksheet.Cells[incRowExcel, 14].Value = "Jumlah Efek";
                                                worksheet.Cells[incRowExcel, 15].Value = "Nilai Pembelian";
                                                worksheet.Cells[incRowExcel, 16].Value = "Nilai Nominal";
                                                worksheet.Cells[incRowExcel, 17].Value = "HPW";
                                                worksheet.Cells[incRowExcel, 18].Value = "Deposito";
                                                worksheet.Cells[incRowExcel, 19].Value = "Total Investasi";
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
                                                    worksheet.Cells[incRowExcel, 19].Value = rsDetail.TotalInvestasi;
                                                    worksheet.Cells[incRowExcel, 20].Value = rsDetail.KodeBK;
                                                    worksheet.Cells[incRowExcel, 21].Value = rsDetail.Keterangan;
                                                    worksheet.Cells[incRowExcel, 22].Value = rsDetail.SID;

                                                    _endRowDetail = incRowExcel;

                                                    incRowExcel++;

                                                }

                                                worksheet.Cells["A" + incRowExcel + ":V" + incRowExcel].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                                worksheet.Cells["A" + incRowExcel + ":V" + incRowExcel].Style.Fill.BackgroundColor.SetColor(Color.LightSeaGreen);
                                                //worksheet.Cells[incRowExcel, 2].Value = "Total :";
                                                //worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                //worksheet.Cells[incRowExcel, 4].Formula = "SUM(D" + _startRowDetail + ":D" + _endRowDetail + ")";
                                                //worksheet.Cells[incRowExcel, 5].Formula = "SUM(E" + _startRowDetail + ":E" + _endRowDetail + ")";
                                                //worksheet.Cells[incRowExcel, 6].Formula = "SUM(F" + _startRowDetail + ":F" + _endRowDetail + ")";
                                                //worksheet.Cells[incRowExcel, 7].Formula = "SUM(G" + _startRowDetail + ":G" + _endRowDetail + ")";
                                                //worksheet.Cells[incRowExcel, 8].Formula = "SUM(H" + _startRowDetail + ":H" + _endRowDetail + ")";
                                                //worksheet.Cells[incRowExcel, 9].Formula = "SUM(I" + _startRowDetail + ":I" + _endRowDetail + ")";
                                                //worksheet.Cells[incRowExcel, 10].Formula = "SUM(J" + _startRowDetail + ":J" + _endRowDetail + ")";
                                                //worksheet.Cells[incRowExcel, 11].Formula = "SUM(K" + _startRowDetail + ":K" + _endRowDetail + ")";
                                                //worksheet.Cells[incRowExcel, 12].Formula = "SUM(L" + _startRowDetail + ":L" + _endRowDetail + ")";
                                                //worksheet.Cells[incRowExcel, 13].Formula = "SUM(M" + _startRowDetail + ":M" + _endRowDetail + ")";
                                                //worksheet.Cells[incRowExcel, 14].Formula = "SUM(N" + _startRowDetail + ":N" + _endRowDetail + ")";
                                                //worksheet.Cells[incRowExcel, 15].Formula = "SUM(O" + _startRowDetail + ":O" + _endRowDetail + ")";
                                                //worksheet.Cells[incRowExcel, 16].Formula = "SUM(P" + _startRowDetail + ":P" + _endRowDetail + ")";
                                                //worksheet.Cells[incRowExcel, 17].Formula = "SUM(Q" + _startRowDetail + ":Q" + _endRowDetail + ")";
                                                //worksheet.Cells[incRowExcel, 18].Formula = "SUM(R" + _startRowDetail + ":R" + _endRowDetail + ")";
                                                //worksheet.Cells[incRowExcel, 19].Formula = "SUM(S" + _startRowDetail + ":S" + _endRowDetail + ")";
                                                //worksheet.Cells[incRowExcel, 20].Formula = "SUM(T" + _startRowDetail + ":T" + _endRowDetail + ")";
                                                //worksheet.Cells[incRowExcel, 21].Formula = "SUM(U" + _startRowDetail + ":U" + _endRowDetail + ")";
                                                //worksheet.Cells[incRowExcel, 22].Formula = "SUM(V" + _startRowDetail + ":V" + _endRowDetail + ")";
                                                //worksheet.Cells["A" + incRowExcel + ":A" + incRowExcel].Calculate();
                                                //worksheet.Cells["B" + incRowExcel + ":B" + incRowExcel].Calculate();
                                                //worksheet.Cells["C" + incRowExcel + ":C" + incRowExcel].Calculate();
                                                //worksheet.Cells["D" + incRowExcel + ":D" + incRowExcel].Calculate();
                                                //worksheet.Cells["E" + incRowExcel + ":E" + incRowExcel].Calculate();
                                                //worksheet.Cells["F" + incRowExcel + ":F" + incRowExcel].Calculate();
                                                //worksheet.Cells["G" + incRowExcel + ":G" + incRowExcel].Calculate();
                                                //worksheet.Cells["H" + incRowExcel + ":H" + incRowExcel].Calculate();
                                                //worksheet.Cells["I" + incRowExcel + ":J" + incRowExcel].Calculate();
                                                //worksheet.Cells["J" + incRowExcel + ":K" + incRowExcel].Calculate();
                                                //worksheet.Cells["K" + incRowExcel + ":L" + incRowExcel].Calculate();
                                                //worksheet.Cells["L" + incRowExcel + ":M" + incRowExcel].Calculate();
                                                //worksheet.Cells["M" + incRowExcel + ":N" + incRowExcel].Calculate();
                                                //worksheet.Cells["N" + incRowExcel + ":O" + incRowExcel].Calculate();
                                                //worksheet.Cells["O" + incRowExcel + ":P" + incRowExcel].Calculate();
                                                //worksheet.Cells["P" + incRowExcel + ":Q" + incRowExcel].Calculate();
                                                //worksheet.Cells["Q" + incRowExcel + ":R" + incRowExcel].Calculate();
                                                //worksheet.Cells["R" + incRowExcel + ":S" + incRowExcel].Calculate();
                                                //worksheet.Cells["S" + incRowExcel + ":T" + incRowExcel].Calculate();
                                                //worksheet.Cells["T" + incRowExcel + ":U" + incRowExcel].Calculate();
                                                //worksheet.Cells["U" + incRowExcel + ":V" + incRowExcel].Calculate();
                                                //worksheet.Cells["A" + incRowExcel + ":V" + incRowExcel].Style.Numberformat.Format = "#,##0";
                                                //worksheet.Cells["A" + incRowExcel + ":V" + incRowExcel].Style.Font.Bold = true;

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

            #region NKPD
            else if (_OjkRpt.ReportName.Equals("3"))
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

                                cmd.CommandTimeout = 0;
                                cmd.CommandText = @"
                         declare @FinalDate datetime
                         set @FinalDate = dbo.FWorkingDay(@Date ,-1)

            --drop table #Text --
            create table #Text(                    
            [ResultText] [nvarchar](1000)  NULL                    
            )                     
                
            insert into #Text   
                 

            SELECT  RTRIM(LTRIM(isnull(FU.NKPDName,'')))             
            + '|' + RTRIM(LTRIM(isnull(AAA.NKPDCode,'')))         
            + '|' + RTRIM(LTRIM(isnull(A.jumlahPerorangan,0)))
            + '|' + CAST(CAST(isnull(B.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30))                  
            + '|' + RTRIM(LTRIM(isnull(C.jumlahPerusahaanEfek,0)))    
            + '|' + CAST(CAST(isnull(D.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30))                     
            + '|' + RTRIM(LTRIM(isnull(E.jumlahDanaPensiun,0)))    
            + '|' + CAST(CAST(isnull(F.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30))                  
            + '|' + RTRIM(LTRIM(isnull(G.jumlahAsuransi,0)))    
            + '|' + CAST(CAST(isnull(H.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30))                   
            + '|' + RTRIM(LTRIM(isnull(I.jumlahBank,0)))        
            + '|' + CAST(CAST(isnull(J.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30))              
            + '|' + RTRIM(LTRIM(isnull(K.jumlahPT,0)))     
            + '|' + CAST(CAST(isnull(L.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30))                    
            + '|' + RTRIM(LTRIM(isnull(M.jumlahBUMN,0)))     
            + '|' + CAST(CAST(isnull(N.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30))                  
            + '|' + RTRIM(LTRIM(isnull(O.jumlahBUMD,0)))        
            + '|' + CAST(CAST(isnull(P.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30))              
            + '|' + RTRIM(LTRIM(isnull(Q.jumlahYayasan,0)))  
            + '|' + CAST(CAST(isnull(R.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30))                     
            + '|' + RTRIM(LTRIM(isnull(S.jumlahKoperasi,0)))     
            + '|' + CAST(CAST(isnull(T.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30))                 
            + '|' + RTRIM(LTRIM(isnull(U.jumlahLembagaNasionalLainnya,0)))
            + '|' + CAST(CAST(isnull(V.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30))             
            
            ------ASING            
            + '|' + RTRIM(LTRIM(isnull(AA.jumlahPeroranganAsing,0)))      
            + '|' + CAST(CAST(isnull(AB.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30))                   
            + '|' + RTRIM(LTRIM(isnull(AC.jumlahPerusahaanEfekAsing,0)))     
            + '|' + CAST(CAST(isnull(AD.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30))                     
            + '|' + RTRIM(LTRIM(isnull(AE.jumlahDanaPensiunAsing,0)))  
            + '|' + CAST(CAST(isnull(AF.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30))                   
            + '|' + RTRIM(LTRIM(isnull(AG.jumlahAsuransiAsing,0)))        
            + '|' + CAST(CAST(isnull(AH.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30))                 
            + '|' + RTRIM(LTRIM(isnull(AI.jumlahBankAsing,0)))   
            + '|' + CAST(CAST(isnull(AJ.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30))                        
            + '|' + RTRIM(LTRIM(isnull(AK.jumlahPTAsing,0))) 
            + '|' + CAST(CAST(isnull(AL.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30))                         
            + '|' + RTRIM(LTRIM(isnull(AM.jumlahBUMNAsing,0)))    
            + '|' + CAST(CAST(isnull(AN.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30))                     
            + '|' + RTRIM(LTRIM(isnull(AO.jumlahBUMDAsing,0)))   
            + '|' + CAST(CAST(isnull(AP.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30))                       
            + '|' + RTRIM(LTRIM(isnull(AQ.jumlahYayasanAsing,0)))  
            + '|' + CAST(CAST(isnull(AR.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30))                       
            + '|' + RTRIM(LTRIM(isnull(SS.jumlahKoperasiAsing,0)))       
            + '|' + CAST(CAST(isnull(AT.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30))                   
            + '|' + RTRIM(LTRIM(isnull(AU.jumlahLembagaAsingLainnya,0)))           
            + '|' + CAST(CAST(isnull(AV.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30)) 
            + '|' +   CAST( CASE WHEN (ISNULL(B.UnitAmount,0) + isnull(D.UnitAmount,0) + isnull(F.UnitAmount,0) + isnull(H.UnitAmount,0) 
            + isnull(J.UnitAmount,0) + isnull(L.UnitAmount,0) + isnull(N.UnitAmount,0) + isnull(P.UnitAmount,0) + isnull(R.UnitAmount,0) 
            + isnull(T.UnitAmount,0) + isnull(V.UnitAmount,0) + 
            isnull(AB.UnitAmount,0) + isnull(AD.UnitAmount,0) + isnull(AF.UnitAmount,0) + isnull(AH.UnitAmount,0) 
            + isnull(AJ.UnitAmount,0) + isnull(AL.UnitAmount,0) + isnull(AN.UnitAmount,0) + isnull(AP.UnitAmount,0) + isnull(AR.UnitAmount,0) 
            + isnull(AT.UnitAmount,0) + isnull(AV.UnitAmount,0)) = 0 THEN 0 ELSE 
             ((ISNULL(B.UnitAmount,0) + isnull(D.UnitAmount,0) + isnull(F.UnitAmount,0) + isnull(H.UnitAmount,0) 
            + isnull(J.UnitAmount,0) + isnull(L.UnitAmount,0) + isnull(N.UnitAmount,0) + isnull(P.UnitAmount,0) + isnull(R.UnitAmount,0) 
            + isnull(T.UnitAmount,0) + isnull(V.UnitAmount,0)) 
            / (ISNULL(B.UnitAmount,0) + isnull(D.UnitAmount,0) + isnull(F.UnitAmount,0) + isnull(H.UnitAmount,0) 
            + isnull(J.UnitAmount,0) + isnull(L.UnitAmount,0) + isnull(N.UnitAmount,0) + isnull(P.UnitAmount,0) + isnull(R.UnitAmount,0) 
            + isnull(T.UnitAmount,0) + isnull(V.UnitAmount,0) + 
            isnull(AB.UnitAmount,0) + isnull(AD.UnitAmount,0) + isnull(AF.UnitAmount,0) + isnull(AH.UnitAmount,0) 
            + isnull(AJ.UnitAmount,0) + isnull(AL.UnitAmount,0) + isnull(AN.UnitAmount,0) + isnull(AP.UnitAmount,0) + isnull(AR.UnitAmount,0) 
            + isnull(AT.UnitAmount,0) + isnull(AV.UnitAmount,0)
             )) * 100 END AS NVARCHAR(30))    
	       
            + '|' + CAST( CASE WHEN (ISNULL(B.UnitAmount,0) + isnull(D.UnitAmount,0) + isnull(F.UnitAmount,0) + isnull(H.UnitAmount,0) 
            + isnull(J.UnitAmount,0) + isnull(L.UnitAmount,0) + isnull(N.UnitAmount,0) + isnull(P.UnitAmount,0) + isnull(R.UnitAmount,0) 
            + isnull(T.UnitAmount,0) + isnull(V.UnitAmount,0) + 
            isnull(AB.UnitAmount,0) + isnull(AD.UnitAmount,0) + isnull(AF.UnitAmount,0) + isnull(AH.UnitAmount,0) 
            + isnull(AJ.UnitAmount,0) + isnull(AL.UnitAmount,0) + isnull(AN.UnitAmount,0) + isnull(AP.UnitAmount,0) + isnull(AR.UnitAmount,0) 
            + isnull(AT.UnitAmount,0) + isnull(AV.UnitAmount,0)) = 0 THEN 0 ELSE 
             ((isnull(AB.UnitAmount,0) + isnull(AD.UnitAmount,0) + isnull(AF.UnitAmount,0) + isnull(AH.UnitAmount,0) 
            + isnull(AJ.UnitAmount,0) + isnull(AL.UnitAmount,0) + isnull(AN.UnitAmount,0) + isnull(AP.UnitAmount,0) + isnull(AR.UnitAmount,0) 
            + isnull(AT.UnitAmount,0) + isnull(AV.UnitAmount,0)) 
            / (ISNULL(B.UnitAmount,0) + isnull(D.UnitAmount,0) + isnull(F.UnitAmount,0) + isnull(H.UnitAmount,0) 
            + isnull(J.UnitAmount,0) + isnull(L.UnitAmount,0) + isnull(N.UnitAmount,0) + isnull(P.UnitAmount,0) + isnull(R.UnitAmount,0) 
            + isnull(T.UnitAmount,0) + isnull(V.UnitAmount,0) + 
            isnull(AB.UnitAmount,0) + isnull(AD.UnitAmount,0) + isnull(AF.UnitAmount,0) + isnull(AH.UnitAmount,0) 
            + isnull(AJ.UnitAmount,0) + isnull(AL.UnitAmount,0) + isnull(AN.UnitAmount,0) + isnull(AP.UnitAmount,0) + isnull(AR.UnitAmount,0) 
            + isnull(AT.UnitAmount,0) + isnull(AV.UnitAmount,0)
             )) * 100 END AS NVARCHAR(30))  
             
            FROM FundClientPosition FCP (NOLOCK)                      
            LEFT JOIN FundClient FC (NOLOCK) ON FCP.FundClientPK = FC.FundClientPK and FC.Status in (1,2)         
            LEFT JOIN Fund FU on FCP.FundPK = FU.FundPK and FU.Status = 2 
            LEFT JOIN FundCashRef ZZZ on FU.FundPK = ZZZ.FundPK and ZZZ.Status = 2
            LEFT JOIN BankBranch ZZ on FU.BankBranchPK = ZZ.BankBranchPK and ZZ.Status in (1,2)
            LEFT JOIN Bank AAA on ZZ.BankPK = AAA.BankPK and AAA.Status in (1,2)
            ----LEFT JOIN FundCashRef FCR on FU.FundPK = FCR.FundPK and FCR.Status in (1,2)       
            LEFT JOIN             
            (            
            select COUNT (*) jumlahperorangan, CS.FundPK from FundClientPosition CS            
            left join FundClient g            
            on CS.FundClientPK = g.FundClientPK   and g.Status in (1,2)       
            where g.InvestorType = 1 and g.nationality= 'ID'
            and CS.Date = @FinalDate and CS.UnitAmount > 1          
            and g.SACode = ''            
            group by CS.FundPK            
            ) A On FCP.FundPK = A.FundPK            
             
            LEFT JOIN             
            (            
            select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
            left join FundClient CG        
            on CS.FundClientPK=CG.FundClientPK    and CG.Status in (1,2)      
            where CG.InvestorType = 1 and CG.nationality= 'ID'
            and CS.Date = @FinalDate and CS.UnitAmount > 0   
            and CG.SACode = ''            
            
            group by CS.FundPK            
            ) B On FCP.FundPK = B.FundPK            
             
            LEFT JOIN             
            (            
            ----------EFEK----------------        
            select COUNT(*) jumlahPerusahaanEfek, CS.FundPK from FundClientPosition CS            
            left join FundClient CG            
            on CS.FundClientPK = CG.FundClientPK and CG.Status in (1,2)          
            where CG.InvestorType = 2 and CG.Negara= 'ID'            
            and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe IN (7)  
            and CG.SACode = ''                 
            
            group by CS.FundPK            
            ) C On FCP.FundPK = C.FundPK            
             
             
            LEFT JOIN             
            (            
            select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
            left join FundClient CG            
            on CS.FundClientPK=CG.FundClientPK and CG.Status in (1,2)          
            where CG.InvestorType = 2 and CG.Negara= 'ID'            
            and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe IN (7)   
            and CG.SACode = ''                
            
            group by CS.FundPK            
            ) D On FCP.FundPK = D.FundPK            
             
             
            LEFT JOIN             
            (            
            ---------DANA PENSIUN-------------        
            select COUNT(*) jumlahDanaPensiun, CS.FundPK from FundClientPosition CS            
            left join FundClient CG            
            on CS.FundClientPK = CG.FundClientPK and CG.Status in (1,2)          
            where CG.InvestorType = 2 and CG.Negara= 'ID'            
            and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=6  
            and CG.SACode = ''              
            
            group by CS.FundPK            
            ) E On FCP.FundPK = E.FundPK            
             
             
            LEFT JOIN             
            (            
            select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
            left join FundClient CG            
            on CS.FundClientPK=CG.FundClientPK  and CG.Status in (1,2)         
            where CG.InvestorType = 2 and CG.Negara= 'ID'            
            and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=6    
            and CG.SACode = ''                  
            
            group by CS.FundPK            
            ) F On FCP.FundPK = F.FundPK            
             
            LEFT JOIN             
            (            
            ----------ASURANSI-----------        
            select COUNT(*) jumlahAsuransi, CS.FundPK from FundClientPosition CS            
            left join FundClient CG            
            on CS.FundClientPK = CG.FundClientPK and CG.Status in (1,2)          
            where CG.InvestorType = 2 and CG.Negara= 'ID'            
            and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe=4     
            and CG.SACode = ''                  
            
            group by CS.FundPK            
            ) G On FCP.FundPK = G.FundPK            
             
            LEFT JOIN             
            (            
            select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
            left join FundClient CG            
            on CS.FundClientPK=CG.FundClientPK  and CG.Status in (1,2)    
            where CG.InvestorType = 2 and CG.Negara= 'ID'            
            and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=4          
            and CG.SACode = ''              
            
            group by CS.FundPK            
            ) H On FCP.FundPK = H.FundPK            
             
            LEFT JOIN             
            (            
            ------------BANK-----------        
            select COUNT(*) jumlahBank, CS.FundPK from FundClientPosition CS            
            left join FundClient CG            
            on CS.FundClientPK = CG.FundClientPK  and CG.Status in (1,2)         
            where CG.InvestorType = 2 and CG.Negara= 'ID'            
            and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe=3 and CG.Karakteristik IN (1,2,3,4,5,6,7)           
            and CG.SACode = ''              
            
            group by CS.FundPK            
            ) I On FCP.FundPK = I.FundPK            
             
            LEFT JOIN             
            (            
            select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
            left join FundClient CG            
            on CS.FundClientPK=CG.FundClientPK  and CG.Status in (1,2)         
            where CG.InvestorType = 2 and CG.Negara= 'ID'            
            and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=3 and CG.Karakteristik IN (1,2,3,4,5,6,7)          
            and CG.SACode = ''              
            
            group by CS.FundPK            
            ) J On FCP.FundPK = J.FundPK            
             
            LEFT JOIN             
            (            
            --------PEURSAHAAN SWASTA-----------        
            select COUNT(*) jumlahPT, CS.FundPK from FundClientPosition CS            
            left join FundClient CG            
            on CS.FundClientPK = CG.FundClientPK  and CG.Status in (1,2)         
            where CG.InvestorType = 2 and CG.Negara= 'ID'            
            and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe=1 and CG.Karakteristik IN (2,3,4,5,6,7)          
            and CG.SACode = ''            
            
            group by CS.FundPK            
            ) K On FCP.FundPK = K.FundPK            
             
            LEFT JOIN             
            (            
            select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
            left join FundClient CG            
            on CS.FundClientPK=CG.FundClientPK  and CG.Status in (1,2)         
            where CG.InvestorType = 2 and CG.Negara= 'ID'            
            and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=1 and CG.Karakteristik IN (2,3,4,5,6,7)       
            and CG.SACode = ''             
            
            group by CS.FundPK            
            ) L On FCP.FundPK = L.FundPK            
             
            LEFT JOIN             
            (            
            ---------------BUMN----------------        
            select COUNT(*) jumlahBUMN, CS.FundPK from FundClientPosition CS            
            left join FundClient CG            
            on CS.FundClientPK = CG.FundClientPK  and CG.Status in (1,2)         
            where CG.InvestorType = 2 and CG.Negara= 'ID'            
            and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe=1 and CG.Karakteristik = 1           
            and CG.SACode = ''            
            
            group by CS.FundPK            
            ) M On FCP.FundPK = M.FundPK            
             
            LEFT JOIN             
            (            
            select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
            left join FundClient CG            
            on CS.FundClientPK=CG.FundClientPK  and CG.Status in (1,2)         
            where CG.InvestorType = 2 and CG.Negara= 'ID'            
            and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe=1 and CG.Karakteristik = 1 
            and CG.SACode = ''                         
            
            group by CS.FundPK            
            ) N On FCP.FundPK = N.FundPK            
             
            LEFT JOIN             
            (            
            -------------BUMD-------------        
            select COUNT(*) jumlahBUMD, CS.FundPK from FundClientPosition CS            
            left join FundClient CG            
            on CS.FundClientPK = CG.FundClientPK and CG.Status in (1,2)          
            where CG.InvestorType = 2 and CG.Negara= 'ID'            
            and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe=1 and CG.Karakteristik = 8      
            and CG.SACode = ''                   
            
            group by CS.FundPK            
            ) O On FCP.FundPK = O.FundPK            
             
            LEFT JOIN             
            (            
            select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
            left join FundClient CG            
            on CS.FundClientPK=CG.FundClientPK  and CG.Status in (1,2)         
            where CG.InvestorType = 2 and CG.Negara= 'ID'            
            and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe=1 and CG.Karakteristik = 8    
            and CG.SACode = ''                     
            
            group by CS.FundPK            
            ) P On FCP.FundPK = P.FundPK            
             
            LEFT JOIN             
            (            
            -----YAYASAN-----------        
            select COUNT(*) jumlahYayasan, CS.FundPK from FundClientPosition CS            
            left join FundClient CG            
            on CS.FundClientPK = CG.FundClientPK and CG.Status in (1,2)          
            where CG.InvestorType = 2 and CG.Negara= 'ID'            
            and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe=2            
            and CG.SACode = ''            
            
            group by CS.FundPK            
            ) Q On FCP.FundPK = Q.FundPK            
             
            LEFT JOIN             
            (            
            select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
            left join FundClient CG            
            on CS.FundClientPK=CG.FundClientPK and CG.Status in (1,2)          
            where CG.InvestorType = 2 and CG.Negara= 'ID'            
            and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe=2       
            and CG.SACode = ''                
            
            group by CS.FundPK            
            ) R On FCP.FundPK = R.FundPK            
             
            LEFT JOIN             
            (            
            ------------KOPERASI--------------        
            select COUNT(*) jumlahKoperasi, CS.FundPK from FundClientPosition CS            
            left join FundClient CG            
            on CS.FundClientPK = CG.FundClientPK and CG.Status in (1,2)          
            where CG.InvestorType = 2 and CG.Negara= 'ID'            
            and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe= 3 and CG.Karakteristik = 8  
            and CG.SACode = ''                     
            
            group by CS.FundPK            
            ) S On FCP.FundPK = S.FundPK            
             
            LEFT JOIN             
            (            
            select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
            left join FundClient CG            
            on CS.FundClientPK=CG.FundClientPK  and CG.Status in (1,2)         
            where CG.InvestorType = 2 and CG.Negara= 'ID'            
            and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe= 3 and CG.Karakteristik = 8  
            and CG.SACode = ''                   
            
            group by CS.FundPK            
            ) T On FCP.FundPK = T.FundPK    

					
            ------------LEMBAGA LAINNYA--------------            
             
            LEFT JOIN             
            (            
            select COUNT(*) jumlahLembagaNasionalLainnya, CS.FundPK from FundClientPosition CS            
            left join FundClient CG            
            on CS.FundClientPK = CG.FundClientPK and CG.Status in (1,2)          
            where CG.InvestorType = 2 and CG.Negara= 'ID'            
            and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe=8           
            and CG.SACode = ''             
            
            group by CS.FundPK            
            ) U On FCP.FundPK = U.FundPK            
             
            LEFT JOIN             
            (            
            select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
            left join FundClient CG            
            on CS.FundClientPK=CG.FundClientPK  and CG.Status in (1,2)         
            where CG.InvestorType = 2 and CG.Negara= 'ID'            
            and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe=8         
            and CG.SACode = ''               
            
            group by CS.FundPK            
            ) V On FCP.FundPK = V.FundPK            
             
            ----ASING            
             
            LEFT JOIN             
            (            
            select COUNT(*) jumlahPeroranganAsing, CS.FundPK from FundClientPosition CS            
            left join FundClient CG            
            on CS.FundClientPK = CG.FundClientPK and CG.Status in (1,2)          
            where CG.InvestorType = 1 and CG.nationality <> 'ID'
            and CS.Date = @FinalDate and CS.UnitAmount > 0            
            and CG.SACode = ''            
            
            group by CS.FundPK            
            ) AA On FCP.FundPK = AA.FundPK            
             
            LEFT JOIN             
            (            
            select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
            left join FundClient CG            
            on CS.FundClientPK=CG.FundClientPK    and CG.Status in (1,2)       
            where CG.InvestorType = 1 and CG.nationality <> 'ID'
            and CS.Date = @FinalDate and CS.UnitAmount > 0             
            and CG.SACode = ''            
            
            group by CS.FundPK            
            ) AB On FCP.FundPK = AB.FundPK            
             
            LEFT JOIN             
            (            
            select COUNT(*) jumlahPerusahaanEfekAsing, CS.FundPK from FundClientPosition CS            
            left join FundClient CG            
            on CS.FundClientPK = CG.FundClientPK  and CG.Status in (1,2)        
            where CG.InvestorType = 2 and CG.Negara<> 'ID'            
            and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe IN (3,7)   
            and CG.SACode = ''                    
            
            group by CS.FundPK          
            ) AC On FCP.FundPK = AC.FundPK            
             
             
            LEFT JOIN             
            (            
            select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
            left join FundClient CG            
            on CS.FundClientPK=CG.FundClientPK  and CG.Status in (1,2)        
            where CG.InvestorType = 2 and CG.Negara<> 'ID'            
            and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe IN (3,7)    
            and CG.SACode = ''                  
            
            group by CS.FundPK            
            ) AD On FCP.FundPK = AD.FundPK            
             
             
            LEFT JOIN             
            (            
            select COUNT(*) jumlahDanaPensiunAsing, CS.FundPK from FundClientPosition CS            
            left join FundClient CG            
            on CS.FundClientPK = CG.FundClientPK and CG.Status in (1,2)    
            where CG.InvestorType = 2 and CG.Negara<> 'ID'            
            and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=6           
            and CG.SACode = ''            
            
            group by CS.FundPK            
            ) AE On FCP.FundPK = AE.FundPK            
             
             
            LEFT JOIN             
            (            
            select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
            left join FundClient CG            
            on CS.FundClientPK=CG.FundClientPK and CG.Status in (1,2)         
            where CG.InvestorType = 2 and CG.Negara<> 'ID'            
            and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=6     
            and CG.SACode = ''                  
            
            group by CS.FundPK            
            ) AF On FCP.FundPK = AF.FundPK            
             
            LEFT JOIN             
            (            
            select COUNT(*) jumlahAsuransiAsing, CS.FundPK from FundClientPosition CS            
            left join FundClient CG            
            on CS.FundClientPK = CG.FundClientPK and CG.Status in (1,2)         
            where CG.InvestorType = 2 and CG.Negara<> 'ID'            
            and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe=4    
            and CG.SACode = ''                   
            
            group by CS.FundPK            
            ) AG On FCP.FundPK = AG.FundPK            
             
            LEFT JOIN             
            (            
            select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
            left join FundClient CG            
            on CS.FundClientPK=CG.FundClientPK and CG.Status in (1,2)         
            where CG.InvestorType = 2 and CG.Negara<> 'ID'            
            and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=4       
            and CG.SACode = ''                 
            
            group by CS.FundPK            
            ) AH On FCP.FundPK = AH.FundPK            
             
            LEFT JOIN             
            (            
            select COUNT(*) jumlahBankAsing, CS.FundPK from FundClientPosition CS            
            left join FundClient CG            
            on CS.FundClientPK = CG.FundClientPK and CG.Status in (1,2)         
            where CG.InvestorType = 2 and CG.Negara<> 'ID'            
            and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe=3 and CG.Karakteristik IN (1,2,3,4,5,6,7)         
            and CG.SACode = ''             
            
            group by CS.FundPK            
            ) AI On FCP.FundPK = AI.FundPK            
             
            LEFT JOIN             
            (            
            select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
            left join FundClient CG            
            on CS.FundClientPK=CG.FundClientPK  and CG.Status in (1,2)        
            where CG.InvestorType = 2 and CG.Negara<> 'ID'            
            and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=3 and CG.Karakteristik IN (1,2,3,4,5,6,7)
            and CG.SACode = ''                        
            
            group by CS.FundPK            
            ) AJ On FCP.FundPK = AJ.FundPK            
             
            LEFT JOIN             
            (            
            select COUNT(*) jumlahPTAsing, CS.FundPK from FundClientPosition CS            
            left join FundClient CG            
            on CS.FundClientPK = CG.FundClientPK  and CG.Status in (1,2)        
            where CG.InvestorType = 2 and CG.Negara<> 'ID'            
            and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe=1 and CG.Karakteristik IN (2,3,4,5,6,7) 
            and CG.SACode = ''                      
            
            group by CS.FundPK            
            ) AK On FCP.FundPK = AK.FundPK            
             
            LEFT JOIN             
            (            
            select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
            left join FundClient CG            
            on CS.FundClientPK=CG.FundClientPK  and CG.Status in (1,2)        
            where CG.InvestorType = 2 and CG.Negara<> 'ID'            
            and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=1 and CG.Karakteristik IN (2,3,4,5,6,7)  
            and CG.SACode = ''                       
            
            group by CS.FundPK            
            ) AL On FCP.FundPK = AL.FundPK            
             
            LEFT JOIN             
            (            
            select COUNT(*) jumlahBUMNAsing, CS.FundPK from FundClientPosition CS            
            left join FundClient CG            
            on CS.FundClientPK = CG.FundClientPK   and CG.Status in (1,2)       
            where CG.InvestorType = 2 and CG.Negara<> 'ID'            
            and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=1 and CG.Karakteristik =1            
            and CG.SACode = ''             
            
            group by CS.FundPK            
            ) AM On FCP.FundPK = AM.FundPK            
             
            LEFT JOIN             
            (            
            select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
            left join FundClient CG            
            on CS.FundClientPK=CG.FundClientPK  and CG.Status in (1,2)        
            where CG.InvestorType = 2 and CG.Negara<> 'ID'            
            and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=1 and CG.Karakteristik =1           
            and CG.SACode = ''            
            
            group by CS.FundPK            
            ) AN On FCP.FundPK = AN.FundPK            
             
            LEFT JOIN             
            (            
            select COUNT(*) jumlahBUMDAsing, CS.FundPK from FundClientPosition CS            
            left join FundClient CG            
            on CS.FundClientPK = CG.FundClientPK and CG.Status in (1,2)         
            where CG.InvestorType = 2 and CG.Negara<>'ID'            
            and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=1 and CG.Karakteristik = 8  
            and CG.SACode = ''                   
            
            group by CS.FundPK            
            ) AO On FCP.FundPK = AO.FundPK            
             
            LEFT JOIN             
            (            
            select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
            left join FundClient CG            
            on CS.FundClientPK=CG.FundClientPK and CG.Status in (1,2)         
            where CG.InvestorType = 2 and CG.Negara<> 'ID'            
            and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=1 and CG.Karakteristik = 8      
            and CG.SACode = ''                  
            
            group by CS.FundPK            
            ) AP On FCP.FundPK = AP.FundPK            
             
            LEFT JOIN             
            (            
            select COUNT(*) jumlahYayasanAsing, CS.FundPK from FundClientPosition CS            
            left join FundClient CG            
            on CS.FundClientPK = CG.FundClientPK and CG.Status in (1,2)         
            where CG.InvestorType = 2 and CG.Negara<> 'ID'          
            and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=2      
            and CG.SACode = ''                 
            
            group by CS.FundPK            
            ) AQ On FCP.FundPK = AQ.FundPK            
             
            LEFT JOIN             
            (            
            select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
            left join FundClient CG            
            on CS.FundClientPK=CG.FundClientPK  and CG.Status in (1,2)        
            where CG.InvestorType = 2 and CG.Negara<> 'ID'            
            and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=2       
            and CG.SACode = ''                 
            
            group by CS.FundPK            
            ) AR On FCP.FundPK = AR.FundPK            
             
            LEFT JOIN             
            (            
            select COUNT(*) jumlahKoperasiAsing, CS.FundPK from FundClientPosition CS            
            left join FundClient CG            
            on CS.FundClientPK = CG.FundClientPK  and CG.Status in (1,2)        
            where CG.InvestorType = 2 and CG.Negara<> 'ID'            
            and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe= 3 and CG.Karakteristik = 8 
            and CG.SACode = ''                        
            
            group by CS.FundPK            
            ) SS On FCP.FundPK = SS.FundPK            
             
            LEFT JOIN             
            (            
            select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
            left join FundClient CG            
            on CS.FundClientPK=CG.FundClientPK and CG.Status in (1,2)         
            where CG.InvestorType = 2 and CG.Negara<> 'ID'            
            and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe= 3 and CG.Karakteristik = 8  
            and CG.SACode = ''                      
            
            group by CS.FundPK            
            ) AT On FCP.FundPK = AT.FundPK            
             
            LEFT JOIN             
            (            
            select COUNT(*) jumlahLembagaAsingLainnya, CS.FundPK from FundClientPosition CS            
            left join FundClient CG            
            on CS.FundClientPK = CG.FundClientPK and CG.Status in (1,2)         
            where CG.InvestorType = 2 and CG.Negara<> 'ID'            
            and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=8            
            and CG.SACode = ''            
            
            group by CS.FundPK            
            ) AU On FCP.FundPK = AU.FundPK            
             
            LEFT JOIN             
            (            
            select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
            left join FundClient CG            
            on CS.FundClientPK=CG.FundClientPK  and CG.Status in (1,2)        
            where CG.InvestorType = 2 and CG.Negara<> 'ID'            
            and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=8         
            and CG.SACode = ''               
            
            group by CS.FundPK            
            ) AV On FCP.FundPK = AV.FundPK            
            left Join Fund Z on FCp.FundPK = Z.FundPK and Z.Status in (1,2)       
            WHERE FCP.Date =@date 
            and Z.FundTypeInternal <> 2   -- BUKAN KPD        
            GROUP BY FU.NKPDName,AAA.NKPDCode,A.jumlahPerorangan,            
            B.UnitAmount           
            ,C.jumlahPerusahaanEfek,            
            D.UnitAmount ,E.jumlahDanaPensiun,            
            F.UnitAmount ,G.jumlahAsuransi,            
            H.UnitAmount ,I.jumlahBank,           
            J.UnitAmount ,K.jumlahPT,            
            L.UnitAmount ,M.jumlahBUMN,            
            N.UnitAmount ,O.jumlahBUMD,            
            P.UnitAmount ,Q.jumlahYayasan,            
            R.UnitAmount ,S.jumlahKoperasi,            
            T.UnitAmount ,U.jumlahLembagaNasionalLainnya,            
            V.UnitAmount ,            
            ----asing            
            AA.jumlahPeroranganAsing,            
            AB.UnitAmount ,AC.jumlahPerusahaanEfekAsing,            
            AD.UnitAmount ,AE.jumlahDanaPensiunAsing,            
            AF.UnitAmount ,AG.jumlahAsuransiAsing,            
            AH.UnitAmount ,AI.jumlahBankAsing,            
            AJ.UnitAmount ,AK.jumlahPTAsing,            
            AL.UnitAmount ,AM.jumlahBUMNAsing,            
            AN.UnitAmount ,AO.jumlahBUMDAsing,            
            AP.UnitAmount ,AQ.jumlahYayasanAsing,            
            AR.UnitAmount ,SS.jumlahKoperasiAsing,            
            AT.UnitAmount ,AU.jumlahLembagaAsingLainnya,            
            AV.UnitAmount , FCP.FundPK
             
            select * from #text

                                 ";
                                cmd.Parameters.AddWithValue("@Date", _OjkRpt.Date);

                                using (SqlDataReader dr = cmd.ExecuteReader())
                                {
                                    if (dr.HasRows)
                                    {

                                        string filePath = Tools.ARIATextPath + "NKPD01.txt";
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
                                            //return Tools.HtmlARIATextPath + "NKPD01.txt";
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

                } // bates

                #endregion

                #region Excel
                else
                {

                    if (_OjkRpt.ReportName.Equals("3"))
                    {
                        try
                        {
                            using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                            {
                                DbCon.Open();
                                using (SqlCommand cmd = DbCon.CreateCommand())
                                {

                                    string _paramFund = "";
                                    _paramFund = "left(@FundFrom,charindex('-',@FundFrom) - 1) ";


                                    cmd.CommandText =
                                    @"
                             Declare @FinalDate datetime
   set @FinalDate =dbo.FWorkingDay(@Date ,-1)

                    SELECT  RTRIM(LTRIM(isnull(FU.Name,''))) FundName,  
					RTRIM(LTRIM(isnull(FU.NKPDName,''))) KodeProduk            
, RTRIM(LTRIM(isnull(AAA.NKPDCode,''))) KodeBK         
, RTRIM(LTRIM(isnull(A.jumlahPerorangan,0))) JmlNasabahPerorangan
, CAST(CAST(isnull(B.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30)) DanaNasabahPerorangan                   
, RTRIM(LTRIM(isnull(C.jumlahPerusahaanEfek,0)))    JmlNasabahLembagaPE
, CAST(CAST(isnull(D.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30))  DanaNasabahLembagaPE                   
, RTRIM(LTRIM(isnull(E.jumlahDanaPensiun,0)))    JmlNasabahLembagaDAPEN
, CAST(CAST(isnull(F.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30)) DanaNasabahLembagaDAPEN                 
, RTRIM(LTRIM(isnull(G.jumlahAsuransi,0)))    JmlNasabahLembagaAsuransi
, CAST(CAST(isnull(H.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30)) DanaNasabahLembagaAsuransi                  
, RTRIM(LTRIM(isnull(I.jumlahBank,0)))        JmlNasabahLembagaBank
, CAST(CAST(isnull(J.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30)) DanaNasabahLembagaBank             
, RTRIM(LTRIM(isnull(K.jumlahPT,0)))     JmlNasabahLembagaSwasta
, CAST(CAST(isnull(L.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30)) DanaNasabahLembagaSwasta                   
, RTRIM(LTRIM(isnull(M.jumlahBUMN,0)))     JmlNasabahLembagaBUMN
, CAST(CAST(isnull(N.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30)) DanaNasabahLembagaBUMN                 
, RTRIM(LTRIM(isnull(O.jumlahBUMD,0)))        JmlNasabahLembagaBUMD
, CAST(CAST(isnull(P.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30)) DanaNasabahLembagaBUMD             
, RTRIM(LTRIM(isnull(Q.jumlahYayasan,0)))  JmlNasabahLembagaYayasan
, CAST(CAST(isnull(R.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30)) DanaNasabahLembagaYayasan                    
, RTRIM(LTRIM(isnull(S.jumlahKoperasi,0)))     JmlNasabahLembagaKoperasi
, CAST(CAST(isnull(T.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30)) DanaNasabahLembagaKoperasi                
, RTRIM(LTRIM(isnull(U.jumlahLembagaNasionalLainnya,0))) JmlNasabahLembagaLainnya
, CAST(CAST(isnull(V.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30)) DanaNasabahLembagaLainnya            
            
------ASING            
, RTRIM(LTRIM(isnull(AA.jumlahPeroranganAsing,0))) JmlAsingPerorangan     
, CAST(CAST(isnull(AB.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30)) DanaAsingPerorangan                  
, RTRIM(LTRIM(isnull(AC.jumlahPerusahaanEfekAsing,0)))     JmlAsingLembagaPE
, CAST(CAST(isnull(AD.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30)) DanaAsingLembagaPE                    
, RTRIM(LTRIM(isnull(AE.jumlahDanaPensiunAsing,0))) JmlAsingLembagaDAPEN 
, CAST(CAST(isnull(AF.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30)) DanaAsingLembagaDAPEN                  
, RTRIM(LTRIM(isnull(AG.jumlahAsuransiAsing,0)))        JmlAsingLembagaAsuransi
, CAST(CAST(isnull(AH.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30)) DanaAsingLembagaAsuransi                
, RTRIM(LTRIM(isnull(AI.jumlahBankAsing,0)))   JmlAsingLembagaBank
, CAST(CAST(isnull(AJ.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30)) DanaAsingLembagaBank                       
, RTRIM(LTRIM(isnull(AK.jumlahPTAsing,0))) JmlAsingLembagaSwasta
, CAST(CAST(isnull(AL.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30)) DanaAsingLembagaSwasta                        
, RTRIM(LTRIM(isnull(AM.jumlahBUMNAsing,0)))    JmlAsingLembagaBUMN
, CAST(CAST(isnull(AN.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30)) DanaAsingLembagaBUMN                     
, RTRIM(LTRIM(isnull(AO.jumlahBUMDAsing,0)))   JmlAsingLembagaBUMD
, CAST(CAST(isnull(AP.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30)) DanaAsingLembagaBUMD                      
, RTRIM(LTRIM(isnull(AQ.jumlahYayasanAsing,0)))  JmlAsingLembagaYayasan
, CAST(CAST(isnull(AR.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30)) DanaAsingLembagaYayasan                      
, RTRIM(LTRIM(isnull(SS.jumlahKoperasiAsing,0)))       JmlAsingLembagaKoperasi
, CAST(CAST(isnull(AT.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30)) DanaAsingLembagaKoperasi                  
, RTRIM(LTRIM(isnull(AU.jumlahLembagaAsingLainnya,0)))           JmlAsingLembagaLainnya
, CAST(CAST(isnull(AV.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,6)) as NVARCHAR(30)) DanaAsingLembagaLainnya


, CAST( CASE WHEN (ISNULL(B.UnitAmount,0) + isnull(D.UnitAmount,0) + isnull(F.UnitAmount,0) + isnull(H.UnitAmount,0) 
+ isnull(J.UnitAmount,0) + isnull(L.UnitAmount,0) + isnull(N.UnitAmount,0) + isnull(P.UnitAmount,0) + isnull(R.UnitAmount,0) 
+ isnull(T.UnitAmount,0) + isnull(V.UnitAmount,0) + 
isnull(AB.UnitAmount,0) + isnull(AD.UnitAmount,0) + isnull(AF.UnitAmount,0) + isnull(AH.UnitAmount,0) 
+ isnull(AJ.UnitAmount,0) + isnull(AL.UnitAmount,0) + isnull(AN.UnitAmount,0) + isnull(AP.UnitAmount,0) + isnull(AR.UnitAmount,0) 
+ isnull(AT.UnitAmount,0) + isnull(AV.UnitAmount,0)) = 0 THEN 0 ELSE 
 ((ISNULL(B.UnitAmount,0) + isnull(D.UnitAmount,0) + isnull(F.UnitAmount,0) + isnull(H.UnitAmount,0) 
+ isnull(J.UnitAmount,0) + isnull(L.UnitAmount,0) + isnull(N.UnitAmount,0) + isnull(P.UnitAmount,0) + isnull(R.UnitAmount,0) 
+ isnull(T.UnitAmount,0) + isnull(V.UnitAmount,0)) 
/ (ISNULL(B.UnitAmount,0) + isnull(D.UnitAmount,0) + isnull(F.UnitAmount,0) + isnull(H.UnitAmount,0) 
+ isnull(J.UnitAmount,0) + isnull(L.UnitAmount,0) + isnull(N.UnitAmount,0) + isnull(P.UnitAmount,0) + isnull(R.UnitAmount,0) 
+ isnull(T.UnitAmount,0) + isnull(V.UnitAmount,0) + 
isnull(AB.UnitAmount,0) + isnull(AD.UnitAmount,0) + isnull(AF.UnitAmount,0) + isnull(AH.UnitAmount,0) 
+ isnull(AJ.UnitAmount,0) + isnull(AL.UnitAmount,0) + isnull(AN.UnitAmount,0) + isnull(AP.UnitAmount,0) + isnull(AR.UnitAmount,0) 
+ isnull(AT.UnitAmount,0) + isnull(AV.UnitAmount,0)
 )) * 100 END AS NVARCHAR(30)) InvestasiDN



, CAST( CASE WHEN (ISNULL(B.UnitAmount,0) + isnull(D.UnitAmount,0) + isnull(F.UnitAmount,0) + isnull(H.UnitAmount,0) 
+ isnull(J.UnitAmount,0) + isnull(L.UnitAmount,0) + isnull(N.UnitAmount,0) + isnull(P.UnitAmount,0) + isnull(R.UnitAmount,0) 
+ isnull(T.UnitAmount,0) + isnull(V.UnitAmount,0) + 
isnull(AB.UnitAmount,0) + isnull(AD.UnitAmount,0) + isnull(AF.UnitAmount,0) + isnull(AH.UnitAmount,0) 
+ isnull(AJ.UnitAmount,0) + isnull(AL.UnitAmount,0) + isnull(AN.UnitAmount,0) + isnull(AP.UnitAmount,0) + isnull(AR.UnitAmount,0) 
+ isnull(AT.UnitAmount,0) + isnull(AV.UnitAmount,0)) = 0 THEN 0 ELSE 
 ((isnull(AB.UnitAmount,0) + isnull(AD.UnitAmount,0) + isnull(AF.UnitAmount,0) + isnull(AH.UnitAmount,0) 
+ isnull(AJ.UnitAmount,0) + isnull(AL.UnitAmount,0) + isnull(AN.UnitAmount,0) + isnull(AP.UnitAmount,0) + isnull(AR.UnitAmount,0) 
+ isnull(AT.UnitAmount,0) + isnull(AV.UnitAmount,0)) 
/ (ISNULL(B.UnitAmount,0) + isnull(D.UnitAmount,0) + isnull(F.UnitAmount,0) + isnull(H.UnitAmount,0) 
+ isnull(J.UnitAmount,0) + isnull(L.UnitAmount,0) + isnull(N.UnitAmount,0) + isnull(P.UnitAmount,0) + isnull(R.UnitAmount,0) 
+ isnull(T.UnitAmount,0) + isnull(V.UnitAmount,0) + 
isnull(AB.UnitAmount,0) + isnull(AD.UnitAmount,0) + isnull(AF.UnitAmount,0) + isnull(AH.UnitAmount,0) 
+ isnull(AJ.UnitAmount,0) + isnull(AL.UnitAmount,0) + isnull(AN.UnitAmount,0) + isnull(AP.UnitAmount,0) + isnull(AR.UnitAmount,0) 
+ isnull(AT.UnitAmount,0) + isnull(AV.UnitAmount,0)
 )) * 100 END AS NVARCHAR(30)) InvestasiLN
	         
             
FROM FundClientPosition FCP (NOLOCK)                      
LEFT JOIN FundClient FC (NOLOCK) ON FCP.FundClientPK = FC.FundClientPK and FC.Status in  (1,2)           
LEFT JOIN Fund FU on FCP.FundPK = FU.FundPK and FU.Status = 2 
LEFT JOIN FundCashRef ZZZ on FU.FundPK = ZZZ.FundPK and ZZZ.Status = 2
LEFT JOIN BankBranch ZZ on FU.BankBranchPK = ZZ.BankBranchPK and ZZ.Status = 2  
LEFT JOIN Bank AAA on ZZ.BankPK = AAA.BankPK and AAA.Status = 2  
----LEFT JOIN FundCashRef FCR on FU.FundPK = FCR.FundPK and FCR.Status = 2         
LEFT JOIN             
(            
select COUNT (*) jumlahperorangan, CS.FundPK from FundClientPosition CS            
left join FundClient g            
on CS.FundClientPK = g.FundClientPK   and g.Status in  (1,2)         
where g.InvestorType = 1 and g.nationality= 'ID'
and CS.Date = @FinalDate and CS.UnitAmount > 1     
and g.SACode = ''      
            
group by CS.FundPK            
) A On FCP.FundPK = A.FundPK            
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG        
on CS.FundClientPK=CG.FundClientPK    and CG.Status in  (1,2)       
where CG.InvestorType = 1 and CG.nationality= 'ID'
and CS.Date = @FinalDate and CS.UnitAmount > 0   
and CG.SACode = ''           
            
group by CS.FundPK            
) B On FCP.FundPK = B.FundPK            
             
LEFT JOIN             
(            
----------EFEK----------------        
select COUNT(*) jumlahPerusahaanEfek, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK = CG.FundClientPK and CG.Status in  (1,2)           
where CG.InvestorType = 2 and CG.Negara= 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe IN (7)   
and CG.SACode = ''               
            
group by CS.FundPK            
) C On FCP.FundPK = C.FundPK            
             
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK=CG.FundClientPK and CG.Status in  (1,2)          
where CG.InvestorType = 2 and CG.Negara= 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe IN (7)  
and CG.SACode = ''                
            
group by CS.FundPK            
) D On FCP.FundPK = D.FundPK            
             
             
LEFT JOIN             
(            
---------DANA PENSIUN-------------        
select COUNT(*) jumlahDanaPensiun, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK = CG.FundClientPK and CG.Status in  (1,2)            
where CG.InvestorType = 2 and CG.Negara= 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=6   
and CG.SACode = ''            
            
group by CS.FundPK            
) E On FCP.FundPK = E.FundPK            
             
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK=CG.FundClientPK  and CG.Status in  (1,2)           
where CG.InvestorType = 2 and CG.Negara= 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=6    
and CG.SACode = ''          
            
group by CS.FundPK            
) F On FCP.FundPK = F.FundPK            
             
LEFT JOIN             
(            
----------ASURANSI-----------        
select COUNT(*) jumlahAsuransi, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK = CG.FundClientPK and CG.Status in  (1,2)            
where CG.InvestorType = 2 and CG.Negara= 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe=4    
and CG.SACode = ''           
            
group by CS.FundPK            
) G On FCP.FundPK = G.FundPK            
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK=CG.FundClientPK  and CG.Status in  (1,2)     
where CG.InvestorType = 2 and CG.Negara= 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=4   
and CG.SACode = ''             
            
group by CS.FundPK            
) H On FCP.FundPK = H.FundPK            
             
LEFT JOIN             
(            
------------BANK-----------        
select COUNT(*) jumlahBank, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK = CG.FundClientPK  and CG.Status in  (1,2)           
where CG.InvestorType = 2 and CG.Negara= 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe=3 and CG.Karakteristik IN (1,2,3,4,5,6,7)   
and CG.SACode = ''              
            
group by CS.FundPK            
) I On FCP.FundPK = I.FundPK            
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK=CG.FundClientPK  and CG.Status in  (1,2)           
where CG.InvestorType = 2 and CG.Negara= 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=3 and CG.Karakteristik IN (1,2,3,4,5,6,7)  
and CG.SACode = ''              
            
group by CS.FundPK            
) J On FCP.FundPK = J.FundPK            
             
LEFT JOIN             
(            
--------PEURSAHAAN SWASTA-----------        
select COUNT(*) jumlahPT, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK = CG.FundClientPK  and CG.Status in  (1,2)          
where CG.InvestorType = 2 and CG.Negara= 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe=1 and CG.Karakteristik IN (2,3,4,5,6,7)    
and CG.SACode = ''          
            
group by CS.FundPK            
) K On FCP.FundPK = K.FundPK            
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK=CG.FundClientPK  and CG.Status in  (1,2)           
where CG.InvestorType = 2 and CG.Negara= 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=1 and CG.Karakteristik IN (2,3,4,5,6,7)   
and CG.SACode = ''         
            
group by CS.FundPK            
) L On FCP.FundPK = L.FundPK            
             
LEFT JOIN             
(            
---------------BUMN----------------        
select COUNT(*) jumlahBUMN, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK = CG.FundClientPK  and CG.Status in  (1,2)           
where CG.InvestorType = 2 and CG.Negara= 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe=1 and CG.Karakteristik = 1     
and CG.SACode = ''          
            
group by CS.FundPK            
) M On FCP.FundPK = M.FundPK            
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK=CG.FundClientPK  and CG.Status in  (1,2)          
where CG.InvestorType = 2 and CG.Negara= 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe=1 and CG.Karakteristik = 1   
and CG.SACode = ''               
            
group by CS.FundPK            
) N On FCP.FundPK = N.FundPK            
             
LEFT JOIN             
(            
-------------BUMD-------------        
select COUNT(*) jumlahBUMD, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK = CG.FundClientPK and CG.Status in  (1,2)           
where CG.InvestorType = 2 and CG.Negara= 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe=1 and CG.Karakteristik = 8   
and CG.SACode = ''              
            
group by CS.FundPK            
) O On FCP.FundPK = O.FundPK            
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK=CG.FundClientPK  and CG.Status in  (1,2)          
where CG.InvestorType = 2 and CG.Negara= 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe=1 and CG.Karakteristik = 8  
and CG.SACode = ''               
            
group by CS.FundPK            
) P On FCP.FundPK = P.FundPK            
             
LEFT JOIN             
(            
-----YAYASAN-----------        
select COUNT(*) jumlahYayasan, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK = CG.FundClientPK and CG.Status in  (1,2)           
where CG.InvestorType = 2 and CG.Negara= 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe=2   
and CG.SACode = ''             
            
group by CS.FundPK            
) Q On FCP.FundPK = Q.FundPK            
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK=CG.FundClientPK and CG.Status in  (1,2)            
where CG.InvestorType = 2 and CG.Negara= 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe=2   
and CG.SACode = ''            
            
group by CS.FundPK            
) R On FCP.FundPK = R.FundPK            
             
LEFT JOIN             
(            
------------KOPERASI--------------        
select COUNT(*) jumlahKoperasi, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK = CG.FundClientPK and CG.Status in  (1,2)            
where CG.InvestorType = 2 and CG.Negara= 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe= 3 and CG.Karakteristik = 8  
and CG.SACode = ''            
            
group by CS.FundPK            
) S On FCP.FundPK = S.FundPK            
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK=CG.FundClientPK  and CG.Status in  (1,2)           
where CG.InvestorType = 2 and CG.Negara= 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe= 3 and CG.Karakteristik = 8   
and CG.SACode = ''          
            
group by CS.FundPK            
) T On FCP.FundPK = T.FundPK    

					
------------LEMBAGA LAINNYA--------------            
             
LEFT JOIN             
(            
select COUNT(*) jumlahLembagaNasionalLainnya, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK = CG.FundClientPK and CG.Status in  (1,2)          
where CG.InvestorType = 2 and CG.Negara= 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe in (5,8)  
and CG.SACode = ''              
            
group by CS.FundPK            
) U On FCP.FundPK = U.FundPK            
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK=CG.FundClientPK  and CG.Status in  (1,2)          
where CG.InvestorType = 2 and CG.Negara= 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe in (5,8)    
and CG.SACode = ''         
            
group by CS.FundPK            
) V On FCP.FundPK = V.FundPK            
             
----ASING            
             
LEFT JOIN             
(            
select COUNT(*) jumlahPeroranganAsing, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK = CG.FundClientPK and CG.Status in  (1,2)            
where CG.InvestorType = 1 and CG.nationality <> 'ID'
and CS.Date = @FinalDate and CS.UnitAmount > 0     
and CG.SACode = ''           
            
group by CS.FundPK            
) AA On FCP.FundPK = AA.FundPK            
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK=CG.FundClientPK    and CG.Status in  (1,2)         
where CG.InvestorType = 1 and CG.nationality <> 'ID'
and CS.Date = @FinalDate and CS.UnitAmount > 0    
and CG.SACode = ''             
            
group by CS.FundPK            
) AB On FCP.FundPK = AB.FundPK            
             
LEFT JOIN             
(            
select COUNT(*) jumlahPerusahaanEfekAsing, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK = CG.FundClientPK  and CG.Status in  (1,2)          
where CG.InvestorType = 2 and CG.Negara<> 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe IN (7)     
and CG.SACode = ''        
            
group by CS.FundPK          
) AC On FCP.FundPK = AC.FundPK            
             
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK=CG.FundClientPK  and CG.Status in  (1,2)         
where CG.InvestorType = 2 and CG.Negara<> 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe IN (7)   
and CG.SACode = ''         
            
group by CS.FundPK            
) AD On FCP.FundPK = AD.FundPK            
             
             
LEFT JOIN             
(            
select COUNT(*) jumlahDanaPensiunAsing, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK = CG.FundClientPK and CG.Status in  (1,2)     
where CG.InvestorType = 2 and CG.Negara<> 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=6        
and CG.SACode = ''     
            
group by CS.FundPK            
) AE On FCP.FundPK = AE.FundPK            
             
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK=CG.FundClientPK and CG.Status in  (1,2)          
where CG.InvestorType = 2 and CG.Negara<> 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=6  
and CG.SACode = ''           
            
group by CS.FundPK            
) AF On FCP.FundPK = AF.FundPK            
             
LEFT JOIN             
(            
select COUNT(*) jumlahAsuransiAsing, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK = CG.FundClientPK and CG.Status in  (1,2)           
where CG.InvestorType = 2 and CG.Negara<> 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe=4     
and CG.SACode = ''        
            
group by CS.FundPK            
) AG On FCP.FundPK = AG.FundPK            
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK=CG.FundClientPK and CG.Status in  (1,2)          
where CG.InvestorType = 2 and CG.Negara<> 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=4   
and CG.SACode = ''           
            
group by CS.FundPK            
) AH On FCP.FundPK = AH.FundPK            
             
LEFT JOIN             
(            
select COUNT(*) jumlahBankAsing, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK = CG.FundClientPK and CG.Status in  (1,2)           
where CG.InvestorType = 2 and CG.Negara<> 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe=3 and CG.Karakteristik IN (1,2,3,4,5,6,7)  
and CG.SACode = ''          
            
group by CS.FundPK            
) AI On FCP.FundPK = AI.FundPK            
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK=CG.FundClientPK  and CG.Status in  (1,2)         
where CG.InvestorType = 2 and CG.Negara<> 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=3 and CG.Karakteristik IN (1,2,3,4,5,6,7)  
and CG.SACode = ''            
            
group by CS.FundPK            
) AJ On FCP.FundPK = AJ.FundPK            
             
LEFT JOIN             
(            
select COUNT(*) jumlahPTAsing, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK = CG.FundClientPK  and CG.Status in  (1,2)         
where CG.InvestorType = 2 and CG.Negara<> 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe=1 and CG.Karakteristik IN (2,3,4,5,6,7)   
and CG.SACode = ''          
            
group by CS.FundPK            
) AK On FCP.FundPK = AK.FundPK            
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK=CG.FundClientPK  and CG.Status in  (1,2)        
where CG.InvestorType = 2 and CG.Negara<> 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=1 and CG.Karakteristik IN (2,3,4,5,6,7)    
and CG.SACode = ''           
            
group by CS.FundPK            
) AL On FCP.FundPK = AL.FundPK            
             
LEFT JOIN             
(            
select COUNT(*) jumlahBUMNAsing, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK = CG.FundClientPK   and CG.Status in  (1,2)         
where CG.InvestorType = 2 and CG.Negara<> 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=1 and CG.Karakteristik =1   
and CG.SACode = ''            
            
group by CS.FundPK            
) AM On FCP.FundPK = AM.FundPK            
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK=CG.FundClientPK  and CG.Status in  (1,2)         
where CG.InvestorType = 2 and CG.Negara<> 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=1 and CG.Karakteristik =1      
and CG.SACode = ''       
            
group by CS.FundPK            
) AN On FCP.FundPK = AN.FundPK            
             
LEFT JOIN             
(            
select COUNT(*) jumlahBUMDAsing, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK = CG.FundClientPK and CG.Status in  (1,2)           
where CG.InvestorType = 2 and CG.Negara<>'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=1 and CG.Karakteristik = 8   
and CG.SACode = ''        
            
group by CS.FundPK            
) AO On FCP.FundPK = AO.FundPK            
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK=CG.FundClientPK and CG.Status in  (1,2)         
where CG.InvestorType = 2 and CG.Negara<> 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=1 and CG.Karakteristik = 8 
and CG.SACode = ''             
            
group by CS.FundPK            
) AP On FCP.FundPK = AP.FundPK            
             
LEFT JOIN             
(            
select COUNT(*) jumlahYayasanAsing, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK = CG.FundClientPK and CG.Status in  (1,2)          
where CG.InvestorType = 2 and CG.Negara<> 'ID'          
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=2    
and CG.SACode = ''         
            
group by CS.FundPK            
) AQ On FCP.FundPK = AQ.FundPK            
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK=CG.FundClientPK  and CG.Status in  (1,2)         
where CG.InvestorType = 2 and CG.Negara<> 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=2   
and CG.SACode = ''           
            
group by CS.FundPK            
) AR On FCP.FundPK = AR.FundPK            
             
LEFT JOIN             
(            
select COUNT(*) jumlahKoperasiAsing, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK = CG.FundClientPK  and CG.Status in  (1,2)         
where CG.InvestorType = 2 and CG.Negara<> 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe= 3 and CG.Karakteristik = 8   
and CG.SACode = ''            
            
group by CS.FundPK            
) SS On FCP.FundPK = SS.FundPK            
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK=CG.FundClientPK and CG.Status in  (1,2)         
where CG.InvestorType = 2 and CG.Negara<> 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe= 3 and CG.Karakteristik = 8  
and CG.SACode = ''            
            
group by CS.FundPK            
) AT On FCP.FundPK = AT.FundPK            
             
LEFT JOIN             
(            
select COUNT(*) jumlahLembagaAsingLainnya, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK = CG.FundClientPK and CG.Status in  (1,2)           
where CG.InvestorType = 2 and CG.Negara<> 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe in (5,8)   
and CG.SACode = ''           
            
group by CS.FundPK            
) AU On FCP.FundPK = AU.FundPK            
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK=CG.FundClientPK  and CG.Status in  (1,2)         
where CG.InvestorType = 2 and CG.Negara<> 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe in (5,8)    
and CG.SACode = ''         
            
group by CS.FundPK            
) AV On FCP.FundPK = AV.FundPK            
left Join Fund Z on FCp.FundPK = Z.FundPK and Z.Status in  (1,2)        
WHERE FCP.Date =@FinalDate
and Z.FundTypeInternal <> 2   -- BUKAN KPD        
AND FCP.UnitAmount > 10
GROUP BY FU.Name,FU.NKPDName,AAA.NKPDCode,A.jumlahPerorangan,            
B.UnitAmount           
,C.jumlahPerusahaanEfek,            
D.UnitAmount ,E.jumlahDanaPensiun,            
F.UnitAmount ,G.jumlahAsuransi,            
H.UnitAmount ,I.jumlahBank,           
J.UnitAmount ,K.jumlahPT,            
L.UnitAmount ,M.jumlahBUMN,            
N.UnitAmount ,O.jumlahBUMD,            
P.UnitAmount ,Q.jumlahYayasan,            
R.UnitAmount ,S.jumlahKoperasi,            
T.UnitAmount ,U.jumlahLembagaNasionalLainnya,            
V.UnitAmount ,            
----asing            
AA.jumlahPeroranganAsing,            
AB.UnitAmount ,AC.jumlahPerusahaanEfekAsing,            
AD.UnitAmount ,AE.jumlahDanaPensiunAsing,            
AF.UnitAmount ,AG.jumlahAsuransiAsing,            
AH.UnitAmount ,AI.jumlahBankAsing,            
AJ.UnitAmount ,AK.jumlahPTAsing,            
AL.UnitAmount ,AM.jumlahBUMNAsing,            
AN.UnitAmount ,AO.jumlahBUMDAsing,            
AP.UnitAmount ,AQ.jumlahYayasanAsing,            
AR.UnitAmount ,SS.jumlahKoperasiAsing,            
AT.UnitAmount ,AU.jumlahLembagaAsingLainnya,            
AV.UnitAmount , FCP.FundPK ";

                                    cmd.CommandTimeout = 0;
                                    cmd.Parameters.AddWithValue("@date", _OjkRpt.Date);


                                    using (SqlDataReader dr0 = cmd.ExecuteReader())
                                    {
                                        if (!dr0.HasRows)
                                        {
                                            return false;
                                        }
                                        else
                                        {
                                            string filePath = Tools.ReportsPath + "NKPD" + "_" + _userID + ".xlsx";
                                            string pdfPath = Tools.ReportsPath + "NKPD" + "_" + _userID + ".pdf";
                                            FileInfo excelFile = new FileInfo(filePath);
                                            if (excelFile.Exists)
                                            {
                                                excelFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                                excelFile = new FileInfo(filePath);
                                            }


                                            // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                            using (ExcelPackage package = new ExcelPackage(excelFile))
                                            {
                                                package.Workbook.Properties.Title = "NKPDReport";
                                                package.Workbook.Properties.Author = Tools.DefaultReportAuthor();
                                                package.Workbook.Properties.Comments = Tools.DefaultReportComments();
                                                package.Workbook.Properties.Company = Tools.DefaultReportCompany();
                                                package.Workbook.Properties.SetCustomPropertyValue("Checked by", _userID);
                                                package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", Tools.DefaultReportAssemblyName());

                                                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("NKPD Report");


                                                //ATUR DATA GROUPINGNYA DULU
                                                List<NKPD> rList = new List<NKPD>();
                                                while (dr0.Read())
                                                {
                                                    NKPD rSingle = new NKPD();
                                                    rSingle.FundName = Convert.ToString(dr0["FundName"]);
                                                    rSingle.KodeProduk = Convert.ToString(dr0["KodeProduk"]);
                                                    rSingle.KodeBK = Convert.ToString(dr0["KodeBK"]);
                                                    rSingle.JmlNasabahPerorangan = Convert.ToInt32(dr0["JmlNasabahPerorangan"]);
                                                    rSingle.DanaNasabahPerorangan = Convert.ToDecimal(dr0["DanaNasabahPerorangan"]);
                                                    rSingle.JmlNasabahLembagaPE = Convert.ToInt32(dr0["JmlNasabahLembagaPE"]);
                                                    rSingle.DanaNasabahLembagaPE = Convert.ToDecimal(dr0["DanaNasabahLembagaPE"]);
                                                    rSingle.JmlNasabahLembagaDAPEN = Convert.ToInt32(dr0["JmlNasabahLembagaDAPEN"]);
                                                    rSingle.DanaNasabahLembagaDAPEN = Convert.ToDecimal(dr0["DanaNasabahLembagaDAPEN"]);
                                                    rSingle.JmlNasabahLembagaAsuransi = Convert.ToInt32(dr0["JmlNasabahLembagaAsuransi"]);
                                                    rSingle.DanaNasabahLembagaAsuransi = Convert.ToDecimal(dr0["DanaNasabahLembagaAsuransi"]);
                                                    rSingle.JmlNasabahLembagaBank = Convert.ToInt32(dr0["JmlNasabahLembagaBank"]);
                                                    rSingle.DanaNasabahLembagaBank = Convert.ToDecimal(dr0["DanaNasabahLembagaBank"]);
                                                    rSingle.JmlNasabahLembagaSwasta = Convert.ToInt32(dr0["JmlNasabahLembagaSwasta"]);
                                                    rSingle.DanaNasabahLembagaSwasta = Convert.ToDecimal(dr0["DanaNasabahLembagaSwasta"]);
                                                    rSingle.JmlNasabahLembagaBUMN = Convert.ToInt32(dr0["JmlNasabahLembagaBUMN"]);
                                                    rSingle.DanaNasabahLembagaBUMN = Convert.ToDecimal(dr0["DanaNasabahLembagaBUMN"]);
                                                    rSingle.JmlNasabahLembagaBUMD = Convert.ToInt32(dr0["JmlNasabahLembagaBUMD"]);
                                                    rSingle.DanaNasabahLembagaBUMD = Convert.ToDecimal(dr0["DanaNasabahLembagaBUMD"]);
                                                    rSingle.JmlNasabahLembagaYayasan = Convert.ToInt32(dr0["JmlNasabahLembagaYayasan"]);
                                                    rSingle.DanaNasabahLembagaYayasan = Convert.ToDecimal(dr0["DanaNasabahLembagaYayasan"]);
                                                    rSingle.JmlNasabahLembagaKoperasi = Convert.ToInt32(dr0["JmlNasabahLembagaKoperasi"]);
                                                    rSingle.DanaNasabahLembagaKoperasi = Convert.ToDecimal(dr0["DanaNasabahLembagaKoperasi"]);
                                                    rSingle.JmlNasabahLembagaLainnya = Convert.ToInt32(dr0["JmlNasabahLembagaLainnya"]);
                                                    rSingle.DanaNasabahLembagaLainnya = Convert.ToDecimal(dr0["DanaNasabahLembagaLainnya"]);
                                                    rSingle.JmlAsingPerorangan = Convert.ToInt32(dr0["JmlAsingPerorangan"]);
                                                    rSingle.DanaAsingPerorangan = Convert.ToDecimal(dr0["DanaAsingPerorangan"]);
                                                    rSingle.JmlAsingLembagaPE = Convert.ToInt32(dr0["JmlAsingLembagaPE"]);
                                                    rSingle.DanaAsingLembagaPE = Convert.ToDecimal(dr0["DanaAsingLembagaPE"]);
                                                    rSingle.JmlAsingLembagaDAPEN = Convert.ToInt32(dr0["JmlAsingLembagaDAPEN"]);
                                                    rSingle.DanaAsingLembagaDAPEN = Convert.ToDecimal(dr0["DanaAsingLembagaDAPEN"]);
                                                    rSingle.JmlAsingLembagaAsuransi = Convert.ToInt32(dr0["JmlAsingLembagaAsuransi"]);
                                                    rSingle.DanaAsingLembagaAsuransi = Convert.ToDecimal(dr0["DanaAsingLembagaAsuransi"]);
                                                    rSingle.JmlAsingLembagaBank = Convert.ToInt32(dr0["JmlAsingLembagaBank"]);
                                                    rSingle.DanaAsingLembagaBank = Convert.ToDecimal(dr0["DanaAsingLembagaBank"]);
                                                    rSingle.JmlAsingLembagaSwasta = Convert.ToInt32(dr0["JmlAsingLembagaSwasta"]);
                                                    rSingle.DanaAsingLembagaSwasta = Convert.ToDecimal(dr0["DanaAsingLembagaSwasta"]);
                                                    rSingle.JmlAsingLembagaBUMN = Convert.ToInt32(dr0["JmlAsingLembagaBUMN"]);
                                                    rSingle.DanaAsingLembagaBUMN = Convert.ToDecimal(dr0["DanaAsingLembagaBUMN"]);
                                                    rSingle.JmlAsingLembagaBUMD = Convert.ToInt32(dr0["JmlAsingLembagaBUMD"]);
                                                    rSingle.DanaAsingLembagaBUMD = Convert.ToDecimal(dr0["DanaAsingLembagaBUMD"]);
                                                    rSingle.JmlAsingLembagaYayasan = Convert.ToInt32(dr0["JmlAsingLembagaYayasan"]);
                                                    rSingle.DanaAsingLembagaYayasan = Convert.ToDecimal(dr0["DanaAsingLembagaYayasan"]);
                                                    rSingle.JmlAsingLembagaKoperasi = Convert.ToInt32(dr0["JmlAsingLembagaKoperasi"]);
                                                    rSingle.DanaAsingLembagaKoperasi = Convert.ToDecimal(dr0["DanaAsingLembagaKoperasi"]);
                                                    rSingle.JmlAsingLembagaLainnya = Convert.ToInt32(dr0["JmlAsingLembagaLainnya"]);
                                                    rSingle.DanaAsingLembagaLainnya = Convert.ToDecimal(dr0["DanaAsingLembagaLainnya"]);
                                                    rSingle.InvestasiDN = Convert.ToDecimal(dr0["InvestasiDN"]);
                                                    rSingle.InvestasiLN = Convert.ToDecimal(dr0["InvestasiLN"]);
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
                                                    worksheet.Cells["A" + incRowExcel + ":AX" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                    worksheet.Cells["A" + incRowExcel + ":AX" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                    worksheet.Cells["A" + incRowExcel + ":AX" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                                    worksheet.Cells["A" + incRowExcel + ":AX" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Dotted;

                                                    worksheet.Cells["A" + incRowExcel + ":AX" + incRowExcel].Style.WrapText = true;

                                                    worksheet.Cells["A" + incRowExcel + ":AX" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                    worksheet.Cells["A" + incRowExcel + ":AX" + incRowExcel].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                    worksheet.Cells["A" + incRowExcel + ":AX" + incRowExcel].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                                    worksheet.Cells["A" + incRowExcel + ":AX" + incRowExcel].Style.Fill.BackgroundColor.SetColor(Color.LightSteelBlue);

                                                    worksheet.Cells[incRowExcel, 1].Value = "Nama Produk";
                                                    worksheet.Cells[incRowExcel, 2].Value = "Kode Produk";
                                                    worksheet.Cells[incRowExcel, 3].Value = "Kode BK";
                                                    worksheet.Cells[incRowExcel, 4].Value = "Jumlah Nasabah Nasional Perorangan";
                                                    worksheet.Cells[incRowExcel, 5].Value = "Dana Kelolaan Nasabah Nasional Perorangan";
                                                    worksheet.Cells[incRowExcel, 6].Value = "Jumlah Nasabah Nasional Lembaga PE";
                                                    worksheet.Cells[incRowExcel, 7].Value = "Dana Kelolaan Nasabah Nasional Lembaga PE";
                                                    worksheet.Cells[incRowExcel, 8].Value = "Jumlah Nasabah Nasional Lembaga DAPEN";
                                                    worksheet.Cells[incRowExcel, 9].Value = "Dana Kelolaan Nasabah Nasional Lembaga DAPEN";
                                                    worksheet.Cells[incRowExcel, 10].Value = "Jumlah Nasabah Nasional Lembaga Asuransi";
                                                    worksheet.Cells[incRowExcel, 11].Value = "Dana Kelolaan Nasabah Nasional Lembaga Asuransi";
                                                    worksheet.Cells[incRowExcel, 12].Value = "Jumlah Nasabah Nasional Lembaga Bank";
                                                    worksheet.Cells[incRowExcel, 13].Value = "Dana Kelolaan Nasabah Nasional Lembaga Bank";
                                                    worksheet.Cells[incRowExcel, 14].Value = "Jumlah Nasabah Nasional Lembaga Perus. Swasta/ Patungan";
                                                    worksheet.Cells[incRowExcel, 15].Value = "Dana Kelolaan Nasabah Nasional Lembaga Perus. Swasta/ Patungan";
                                                    worksheet.Cells[incRowExcel, 16].Value = "Jumlah Nasabah Nasional Lembaga BUMN";
                                                    worksheet.Cells[incRowExcel, 17].Value = "Dana Kelolaan Nasabah Nasional Lembaga BUMN";
                                                    worksheet.Cells[incRowExcel, 18].Value = "Jumlah Nasabah Nasional Lembaga BUMD";
                                                    worksheet.Cells[incRowExcel, 19].Value = "Dana Kelolaan Nasabah Nasional Lembaga BUMD";
                                                    worksheet.Cells[incRowExcel, 20].Value = "Jumlah Nasabah Nasional Lembaga Yayasan";
                                                    worksheet.Cells[incRowExcel, 21].Value = "Dana Kelolaan Nasabah Nasional Lembaga Yayasan";
                                                    worksheet.Cells[incRowExcel, 22].Value = "Jumlah Nasabah Nasional Lembaga Koperasi";
                                                    worksheet.Cells[incRowExcel, 23].Value = "Dana Kelolaan Nasabah Nasional Lembaga Koperasi";
                                                    worksheet.Cells[incRowExcel, 24].Value = "Jumlah Nasabah Nasional Lembaga Lainnya";
                                                    worksheet.Cells[incRowExcel, 25].Value = "Dana Kelolaan Nasabah Nasional Lembaga Lainnya";
                                                    worksheet.Cells[incRowExcel, 26].Value = "Jumlah Nasabah Asing Perorangan";
                                                    worksheet.Cells[incRowExcel, 27].Value = "Dana Kelolaan Nasabah Asing Perorangan";
                                                    worksheet.Cells[incRowExcel, 28].Value = "Jumlah Nasabah Asing Lembaga PE";
                                                    worksheet.Cells[incRowExcel, 29].Value = "Dana Kelolaan Nasabah Asing Lembaga PE";
                                                    worksheet.Cells[incRowExcel, 30].Value = "Jumlah Nasabah Asing Lembaga DAPEN";
                                                    worksheet.Cells[incRowExcel, 31].Value = "Dana Kelolaan Nasabah Asing Lembaga DAPEN";
                                                    worksheet.Cells[incRowExcel, 32].Value = "Jumlah Nasabah Asing Lembaga Asuransi";
                                                    worksheet.Cells[incRowExcel, 33].Value = "Dana Kelolaan Nasabah Asing Lembaga Asuransi";
                                                    worksheet.Cells[incRowExcel, 34].Value = "Jumlah Nasabah Asing Lembaga Bank";
                                                    worksheet.Cells[incRowExcel, 35].Value = "Dana Kelolaan Nasabah Asing Lembaga Bank";
                                                    worksheet.Cells[incRowExcel, 36].Value = "Jumlah Nasabah Asing Lembaga Perus. Swasta/ Patungan";
                                                    worksheet.Cells[incRowExcel, 37].Value = "Dana Kelolaan Nasabah Asing Lembaga Perus. Swasta/ Patungan";
                                                    worksheet.Cells[incRowExcel, 38].Value = "Jumlah Nasabah Asing Lembaga BUMN";
                                                    worksheet.Cells[incRowExcel, 39].Value = "Dana Kelolaan Nasabah Asing Lembaga BUMN";
                                                    worksheet.Cells[incRowExcel, 40].Value = "Jumlah Nasabah Asing Lembaga BUMD";
                                                    worksheet.Cells[incRowExcel, 41].Value = "Dana Kelolaan Nasabah Asing Lembaga BUMD";
                                                    worksheet.Cells[incRowExcel, 42].Value = "Jumlah Nasabah Asing Lembaga Yayasan";
                                                    worksheet.Cells[incRowExcel, 43].Value = "Dana Kelolaan Nasabah Asing Lembaga Yayasan";
                                                    worksheet.Cells[incRowExcel, 44].Value = "Jumlah Nasabah Asing Lembaga Koperasi";
                                                    worksheet.Cells[incRowExcel, 45].Value = "Dana Kelolaan Nasabah Asing Lembaga Koperasi";
                                                    worksheet.Cells[incRowExcel, 46].Value = "Jumlah Nasabah Asing Lembaga Lainnya";
                                                    worksheet.Cells[incRowExcel, 47].Value = "Dana Kelolaan Nasabah Asing Lembaga Lainnya";
                                                    worksheet.Cells[incRowExcel, 48].Value = "Investasi DN";
                                                    worksheet.Cells[incRowExcel, 49].Value = "Investasi LN";
                                                    worksheet.Cells[incRowExcel, 50].Value = "Total";

                                                    incRowExcel++;
                                                    worksheet.Cells["A" + incRowExcel + ":AX" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                    worksheet.Cells[incRowExcel, 1].Value = "(1)";
                                                    worksheet.Cells[incRowExcel, 2].Value = "(2)";
                                                    worksheet.Cells[incRowExcel, 3].Value = "(3)";
                                                    worksheet.Cells[incRowExcel, 4].Value = "(4)";
                                                    worksheet.Cells[incRowExcel, 5].Value = "(5)";
                                                    worksheet.Cells[incRowExcel, 6].Value = "(6)";
                                                    worksheet.Cells[incRowExcel, 7].Value = "(7)";
                                                    worksheet.Cells[incRowExcel, 8].Value = "(8)";
                                                    worksheet.Cells[incRowExcel, 9].Value = "(9)";
                                                    worksheet.Cells[incRowExcel, 10].Value = "(10)";
                                                    worksheet.Cells[incRowExcel, 11].Value = "(11)";
                                                    worksheet.Cells[incRowExcel, 12].Value = "(12)";
                                                    worksheet.Cells[incRowExcel, 13].Value = "(13)";
                                                    worksheet.Cells[incRowExcel, 14].Value = "(14)";
                                                    worksheet.Cells[incRowExcel, 15].Value = "(15)";
                                                    worksheet.Cells[incRowExcel, 16].Value = "(16)";
                                                    worksheet.Cells[incRowExcel, 17].Value = "(17)";
                                                    worksheet.Cells[incRowExcel, 18].Value = "(18)";
                                                    worksheet.Cells[incRowExcel, 19].Value = "(19)";
                                                    worksheet.Cells[incRowExcel, 20].Value = "(20)";
                                                    worksheet.Cells[incRowExcel, 21].Value = "(21)";
                                                    worksheet.Cells[incRowExcel, 22].Value = "(22)";
                                                    worksheet.Cells[incRowExcel, 23].Value = "(23)";
                                                    worksheet.Cells[incRowExcel, 24].Value = "(24)";
                                                    worksheet.Cells[incRowExcel, 25].Value = "(25)";
                                                    worksheet.Cells[incRowExcel, 26].Value = "(26)";
                                                    worksheet.Cells[incRowExcel, 27].Value = "(27)";
                                                    worksheet.Cells[incRowExcel, 28].Value = "(28)";
                                                    worksheet.Cells[incRowExcel, 29].Value = "(29)";
                                                    worksheet.Cells[incRowExcel, 30].Value = "(30)";
                                                    worksheet.Cells[incRowExcel, 31].Value = "(31)";
                                                    worksheet.Cells[incRowExcel, 32].Value = "(32)";
                                                    worksheet.Cells[incRowExcel, 33].Value = "(33)";
                                                    worksheet.Cells[incRowExcel, 34].Value = "(34)";
                                                    worksheet.Cells[incRowExcel, 35].Value = "(35)";
                                                    worksheet.Cells[incRowExcel, 36].Value = "(36)";
                                                    worksheet.Cells[incRowExcel, 37].Value = "(37)";
                                                    worksheet.Cells[incRowExcel, 38].Value = "(38)";
                                                    worksheet.Cells[incRowExcel, 39].Value = "(39)";
                                                    worksheet.Cells[incRowExcel, 40].Value = "(40)";
                                                    worksheet.Cells[incRowExcel, 41].Value = "(41)";
                                                    worksheet.Cells[incRowExcel, 42].Value = "(42)";
                                                    worksheet.Cells[incRowExcel, 43].Value = "(43)";
                                                    worksheet.Cells[incRowExcel, 44].Value = "(44)";
                                                    worksheet.Cells[incRowExcel, 45].Value = "(45)";
                                                    worksheet.Cells[incRowExcel, 46].Value = "(46)";
                                                    worksheet.Cells[incRowExcel, 47].Value = "(47)";
                                                    worksheet.Cells[incRowExcel, 48].Value = "(48)";

                                                    //area header
                                                    int _endRowDetail = 0;
                                                    int _startRow = incRowExcel;
                                                    incRowExcel++;
                                                    _startRowDetail = incRowExcel;
                                                    foreach (var rsDetail in rsHeader)
                                                    {
                                                        worksheet.Cells[incRowExcel, 1].Value = rsDetail.FundName;
                                                        worksheet.Cells[incRowExcel, 1].Style.WrapText = true;
                                                        worksheet.Cells[incRowExcel, 2].Value = rsDetail.KodeProduk;
                                                        worksheet.Cells[incRowExcel, 3].Value = rsDetail.KodeBK;
                                                        worksheet.Cells[incRowExcel, 4].Value = rsDetail.JmlNasabahPerorangan;
                                                        worksheet.Cells[incRowExcel, 5].Value = rsDetail.DanaNasabahPerorangan;
                                                        worksheet.Cells[incRowExcel, 6].Value = rsDetail.JmlNasabahLembagaPE;
                                                        worksheet.Cells[incRowExcel, 7].Value = rsDetail.DanaNasabahLembagaPE;
                                                        worksheet.Cells[incRowExcel, 8].Value = rsDetail.JmlNasabahLembagaDAPEN;
                                                        worksheet.Cells[incRowExcel, 9].Value = rsDetail.DanaNasabahLembagaDAPEN;
                                                        worksheet.Cells[incRowExcel, 10].Value = rsDetail.JmlNasabahLembagaAsuransi;
                                                        worksheet.Cells[incRowExcel, 11].Value = rsDetail.DanaNasabahLembagaAsuransi;
                                                        worksheet.Cells[incRowExcel, 12].Value = rsDetail.JmlNasabahLembagaBank;
                                                        worksheet.Cells[incRowExcel, 13].Value = rsDetail.DanaNasabahLembagaBank;
                                                        worksheet.Cells[incRowExcel, 14].Value = rsDetail.JmlNasabahLembagaSwasta;
                                                        worksheet.Cells[incRowExcel, 15].Value = rsDetail.DanaNasabahLembagaSwasta;
                                                        worksheet.Cells[incRowExcel, 16].Value = rsDetail.JmlNasabahLembagaBUMN;
                                                        worksheet.Cells[incRowExcel, 17].Value = rsDetail.DanaNasabahLembagaBUMN;
                                                        worksheet.Cells[incRowExcel, 18].Value = rsDetail.JmlNasabahLembagaBUMD;
                                                        worksheet.Cells[incRowExcel, 19].Value = rsDetail.DanaNasabahLembagaBUMD;
                                                        worksheet.Cells[incRowExcel, 20].Value = rsDetail.JmlNasabahLembagaYayasan;
                                                        worksheet.Cells[incRowExcel, 21].Value = rsDetail.DanaNasabahLembagaYayasan;
                                                        worksheet.Cells[incRowExcel, 22].Value = rsDetail.JmlNasabahLembagaKoperasi;
                                                        worksheet.Cells[incRowExcel, 23].Value = rsDetail.DanaNasabahLembagaKoperasi;
                                                        worksheet.Cells[incRowExcel, 24].Value = rsDetail.JmlNasabahLembagaLainnya;
                                                        worksheet.Cells[incRowExcel, 25].Value = rsDetail.DanaNasabahLembagaLainnya;
                                                        worksheet.Cells[incRowExcel, 26].Value = rsDetail.JmlAsingPerorangan;
                                                        worksheet.Cells[incRowExcel, 27].Value = rsDetail.DanaAsingPerorangan;
                                                        worksheet.Cells[incRowExcel, 28].Value = rsDetail.JmlAsingLembagaPE;
                                                        worksheet.Cells[incRowExcel, 29].Value = rsDetail.DanaAsingLembagaPE;
                                                        worksheet.Cells[incRowExcel, 30].Value = rsDetail.JmlAsingLembagaDAPEN;
                                                        worksheet.Cells[incRowExcel, 31].Value = rsDetail.DanaAsingLembagaDAPEN;
                                                        worksheet.Cells[incRowExcel, 32].Value = rsDetail.JmlAsingLembagaAsuransi;
                                                        worksheet.Cells[incRowExcel, 33].Value = rsDetail.DanaAsingLembagaAsuransi;
                                                        worksheet.Cells[incRowExcel, 34].Value = rsDetail.JmlAsingLembagaBank;
                                                        worksheet.Cells[incRowExcel, 35].Value = rsDetail.DanaAsingLembagaBank;
                                                        worksheet.Cells[incRowExcel, 36].Value = rsDetail.JmlAsingLembagaSwasta;
                                                        worksheet.Cells[incRowExcel, 37].Value = rsDetail.DanaAsingLembagaSwasta;
                                                        worksheet.Cells[incRowExcel, 38].Value = rsDetail.JmlAsingLembagaBUMN;
                                                        worksheet.Cells[incRowExcel, 39].Value = rsDetail.DanaAsingLembagaBUMN;
                                                        worksheet.Cells[incRowExcel, 40].Value = rsDetail.JmlAsingLembagaBUMD;
                                                        worksheet.Cells[incRowExcel, 41].Value = rsDetail.DanaAsingLembagaBUMD;
                                                        worksheet.Cells[incRowExcel, 42].Value = rsDetail.JmlAsingLembagaYayasan;
                                                        worksheet.Cells[incRowExcel, 43].Value = rsDetail.DanaAsingLembagaYayasan;
                                                        worksheet.Cells[incRowExcel, 44].Value = rsDetail.JmlAsingLembagaKoperasi;
                                                        worksheet.Cells[incRowExcel, 45].Value = rsDetail.DanaAsingLembagaKoperasi;
                                                        worksheet.Cells[incRowExcel, 46].Value = rsDetail.JmlAsingLembagaLainnya;
                                                        worksheet.Cells[incRowExcel, 47].Value = rsDetail.DanaAsingLembagaLainnya;
                                                        worksheet.Cells[incRowExcel, 48].Value = rsDetail.InvestasiDN;
                                                        worksheet.Cells[incRowExcel, 49].Value = rsDetail.InvestasiLN;
                                                        worksheet.Cells[incRowExcel, 50].Formula =
                                                        "SUM(E" + incRowExcel + "+G" + incRowExcel + "+I" + incRowExcel + "+K" + incRowExcel + "+M" + incRowExcel +
                                                        "+O" + incRowExcel + "+Q" + incRowExcel + "+S" + incRowExcel + "+U" + incRowExcel + "+W" + incRowExcel + "+Y" + incRowExcel +
                                                        "+AA" + incRowExcel + "+AC" + incRowExcel + "+AE" + incRowExcel + "+AG" + incRowExcel + "+AI" + incRowExcel + "+AK" + incRowExcel +
                                                        "+AM" + incRowExcel + "+AO" + incRowExcel + "+AQ" + incRowExcel + "+AS" + incRowExcel + "+AU" + incRowExcel + ")";
                                                        worksheet.Cells[incRowExcel, 50].Calculate();

                                                        _endRowDetail = incRowExcel;

                                                        incRowExcel++;

                                                    }

                                                    worksheet.Cells["A" + incRowExcel + ":AX" + incRowExcel].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                                    worksheet.Cells["A" + incRowExcel + ":AX" + incRowExcel].Style.Fill.BackgroundColor.SetColor(Color.LightSeaGreen);
                                                    worksheet.Cells[incRowExcel, 2].Value = "Total :";
                                                    worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                    worksheet.Cells[incRowExcel, 4].Formula = "SUM(D" + _startRowDetail + ":D" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 5].Formula = "SUM(E" + _startRowDetail + ":E" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 6].Formula = "SUM(F" + _startRowDetail + ":F" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 7].Formula = "SUM(G" + _startRowDetail + ":G" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 8].Formula = "SUM(H" + _startRowDetail + ":H" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 9].Formula = "SUM(I" + _startRowDetail + ":I" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 10].Formula = "SUM(J" + _startRowDetail + ":J" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 11].Formula = "SUM(K" + _startRowDetail + ":K" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 12].Formula = "SUM(L" + _startRowDetail + ":L" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 13].Formula = "SUM(M" + _startRowDetail + ":M" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 14].Formula = "SUM(N" + _startRowDetail + ":N" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 15].Formula = "SUM(O" + _startRowDetail + ":O" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 16].Formula = "SUM(P" + _startRowDetail + ":P" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 17].Formula = "SUM(Q" + _startRowDetail + ":Q" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 18].Formula = "SUM(R" + _startRowDetail + ":R" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 19].Formula = "SUM(S" + _startRowDetail + ":S" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 20].Formula = "SUM(T" + _startRowDetail + ":T" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 21].Formula = "SUM(U" + _startRowDetail + ":U" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 22].Formula = "SUM(V" + _startRowDetail + ":V" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 23].Formula = "SUM(W" + _startRowDetail + ":W" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 24].Formula = "SUM(X" + _startRowDetail + ":X" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 25].Formula = "SUM(Y" + _startRowDetail + ":Y" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 26].Formula = "SUM(Z" + _startRowDetail + ":Z" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 27].Formula = "SUM(AA" + _startRowDetail + ":AA" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 28].Formula = "SUM(AB" + _startRowDetail + ":AB" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 29].Formula = "SUM(AC" + _startRowDetail + ":AC" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 30].Formula = "SUM(AD" + _startRowDetail + ":AD" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 31].Formula = "SUM(AE" + _startRowDetail + ":AE" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 32].Formula = "SUM(AF" + _startRowDetail + ":AF" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 33].Formula = "SUM(AG" + _startRowDetail + ":AG" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 34].Formula = "SUM(AH" + _startRowDetail + ":AH" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 35].Formula = "SUM(AI" + _startRowDetail + ":AI" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 36].Formula = "SUM(AJ" + _startRowDetail + ":AJ" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 37].Formula = "SUM(AK" + _startRowDetail + ":AK" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 38].Formula = "SUM(AL" + _startRowDetail + ":AL" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 39].Formula = "SUM(AM" + _startRowDetail + ":AM" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 40].Formula = "SUM(AN" + _startRowDetail + ":AN" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 41].Formula = "SUM(AO" + _startRowDetail + ":AO" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 42].Formula = "SUM(AP" + _startRowDetail + ":AP" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 43].Formula = "SUM(AQ" + _startRowDetail + ":AQ" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 44].Formula = "SUM(AR" + _startRowDetail + ":AR" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 45].Formula = "SUM(AS" + _startRowDetail + ":AS" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 46].Formula = "SUM(AT" + _startRowDetail + ":AT" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 47].Formula = "SUM(AU" + _startRowDetail + ":AU" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 48].Formula = "SUM(AV" + _startRowDetail + ":AV" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 49].Formula = "SUM(AW" + _startRowDetail + ":AW" + _endRowDetail + ")";
                                                    worksheet.Cells[incRowExcel, 50].Formula = "SUM(AX" + _startRowDetail + ":AX" + _endRowDetail + ")";
                                                    worksheet.Cells["A" + incRowExcel + ":A" + incRowExcel].Calculate();
                                                    worksheet.Cells["B" + incRowExcel + ":B" + incRowExcel].Calculate();
                                                    worksheet.Cells["C" + incRowExcel + ":C" + incRowExcel].Calculate();
                                                    worksheet.Cells["D" + incRowExcel + ":D" + incRowExcel].Calculate();
                                                    worksheet.Cells["E" + incRowExcel + ":E" + incRowExcel].Calculate();
                                                    worksheet.Cells["F" + incRowExcel + ":F" + incRowExcel].Calculate();
                                                    worksheet.Cells["G" + incRowExcel + ":G" + incRowExcel].Calculate();
                                                    worksheet.Cells["H" + incRowExcel + ":H" + incRowExcel].Calculate();
                                                    worksheet.Cells["I" + incRowExcel + ":J" + incRowExcel].Calculate();
                                                    worksheet.Cells["J" + incRowExcel + ":K" + incRowExcel].Calculate();
                                                    worksheet.Cells["K" + incRowExcel + ":L" + incRowExcel].Calculate();
                                                    worksheet.Cells["L" + incRowExcel + ":M" + incRowExcel].Calculate();
                                                    worksheet.Cells["M" + incRowExcel + ":N" + incRowExcel].Calculate();
                                                    worksheet.Cells["N" + incRowExcel + ":O" + incRowExcel].Calculate();
                                                    worksheet.Cells["O" + incRowExcel + ":P" + incRowExcel].Calculate();
                                                    worksheet.Cells["P" + incRowExcel + ":Q" + incRowExcel].Calculate();
                                                    worksheet.Cells["Q" + incRowExcel + ":R" + incRowExcel].Calculate();
                                                    worksheet.Cells["R" + incRowExcel + ":S" + incRowExcel].Calculate();
                                                    worksheet.Cells["S" + incRowExcel + ":T" + incRowExcel].Calculate();
                                                    worksheet.Cells["T" + incRowExcel + ":U" + incRowExcel].Calculate();
                                                    worksheet.Cells["U" + incRowExcel + ":V" + incRowExcel].Calculate();
                                                    worksheet.Cells["V" + incRowExcel + ":W" + incRowExcel].Calculate();
                                                    worksheet.Cells["W" + incRowExcel + ":X" + incRowExcel].Calculate();
                                                    worksheet.Cells["X" + incRowExcel + ":Y" + incRowExcel].Calculate();
                                                    worksheet.Cells["Z" + incRowExcel + ":Z" + incRowExcel].Calculate();
                                                    worksheet.Cells["AA" + incRowExcel + ":AA" + incRowExcel].Calculate();
                                                    worksheet.Cells["AB" + incRowExcel + ":AB" + incRowExcel].Calculate();
                                                    worksheet.Cells["AC" + incRowExcel + ":AC" + incRowExcel].Calculate();
                                                    worksheet.Cells["AD" + incRowExcel + ":AD" + incRowExcel].Calculate();
                                                    worksheet.Cells["AE" + incRowExcel + ":AE" + incRowExcel].Calculate();
                                                    worksheet.Cells["AF" + incRowExcel + ":AF" + incRowExcel].Calculate();
                                                    worksheet.Cells["AG" + incRowExcel + ":AG" + incRowExcel].Calculate();
                                                    worksheet.Cells["AH" + incRowExcel + ":AH" + incRowExcel].Calculate();
                                                    worksheet.Cells["AI" + incRowExcel + ":AI" + incRowExcel].Calculate();
                                                    worksheet.Cells["AJ" + incRowExcel + ":AJ" + incRowExcel].Calculate();
                                                    worksheet.Cells["AK" + incRowExcel + ":AK" + incRowExcel].Calculate();
                                                    worksheet.Cells["AL" + incRowExcel + ":AL" + incRowExcel].Calculate();
                                                    worksheet.Cells["AM" + incRowExcel + ":AM" + incRowExcel].Calculate();
                                                    worksheet.Cells["AN" + incRowExcel + ":AN" + incRowExcel].Calculate();
                                                    worksheet.Cells["AO" + incRowExcel + ":AO" + incRowExcel].Calculate();
                                                    worksheet.Cells["AP" + incRowExcel + ":AP" + incRowExcel].Calculate();
                                                    worksheet.Cells["AQ" + incRowExcel + ":AQ" + incRowExcel].Calculate();
                                                    worksheet.Cells["AR" + incRowExcel + ":AR" + incRowExcel].Calculate();
                                                    worksheet.Cells["AS" + incRowExcel + ":AS" + incRowExcel].Calculate();
                                                    worksheet.Cells["AT" + incRowExcel + ":AT" + incRowExcel].Calculate();
                                                    worksheet.Cells["AU" + incRowExcel + ":AU" + incRowExcel].Calculate();
                                                    worksheet.Cells["AV" + incRowExcel + ":AV" + incRowExcel].Calculate();
                                                    worksheet.Cells["AW" + incRowExcel + ":AW" + incRowExcel].Calculate();
                                                    worksheet.Cells["AX" + incRowExcel + ":AX" + incRowExcel].Calculate();
                                                    worksheet.Cells["A" + incRowExcel + ":AX" + incRowExcel].Style.Numberformat.Format = "#,##0";
                                                    worksheet.Cells["A" + incRowExcel + ":AX" + incRowExcel].Style.Font.Bold = true;

                                                    worksheet.Cells["A" + _startRow + ":AX" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Dotted;
                                                    worksheet.Cells["A" + _startRow + ":AX" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                    worksheet.Cells["A" + _startRow + ":AX" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                                    worksheet.Cells["A" + incRowExcel + ":AX" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                    incRowExcel++;
                                                }

                                                worksheet.PrinterSettings.FitToPage = true;
                                                worksheet.PrinterSettings.FitToWidth = 1;
                                                worksheet.PrinterSettings.FitToHeight = 1;
                                                worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel - 1, 50];
                                                worksheet.Column(1).Width = 45;
                                                worksheet.Column(2).Width = 20;
                                                worksheet.Column(3).Width = 10;
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
                                                worksheet.Column(23).Width = 20;
                                                worksheet.Column(24).Width = 20;
                                                worksheet.Column(25).Width = 20;
                                                worksheet.Column(26).Width = 20;
                                                worksheet.Column(27).Width = 20;
                                                worksheet.Column(28).Width = 20;
                                                worksheet.Column(29).Width = 20;
                                                worksheet.Column(30).Width = 20;
                                                worksheet.Column(31).Width = 20;
                                                worksheet.Column(32).Width = 20;
                                                worksheet.Column(33).Width = 20;
                                                worksheet.Column(34).Width = 20;
                                                worksheet.Column(35).Width = 20;
                                                worksheet.Column(36).Width = 20;
                                                worksheet.Column(37).Width = 20;
                                                worksheet.Column(38).Width = 20;
                                                worksheet.Column(39).Width = 20;
                                                worksheet.Column(40).Width = 20;
                                                worksheet.Column(41).Width = 20;
                                                worksheet.Column(42).Width = 20;
                                                worksheet.Column(43).Width = 20;
                                                worksheet.Column(44).Width = 20;
                                                worksheet.Column(45).Width = 20;
                                                worksheet.Column(46).Width = 20;
                                                worksheet.Column(47).Width = 20;
                                                worksheet.Column(48).Width = 20;
                                                worksheet.Column(49).Width = 20;
                                                worksheet.Column(50).Width = 20;


                                                // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                                //worksheet.Cells["A3:O14"].AutoFitColumns();  // UNTUK AUTO FIT UKURAN TULISAN DENGAN KOLOM, KEBETULAN INI BUAT HEADER AJA DIPAKENYA

                                                worksheet.PrinterSettings.Orientation = eOrientation.Landscape; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                                worksheet.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN
                                                worksheet.HeaderFooter.OddHeader.RightAlignedText = "&12 NKPD REPORT";

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