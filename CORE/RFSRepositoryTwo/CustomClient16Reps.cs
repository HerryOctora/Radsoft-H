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

namespace RFSRepositoryTwo
{
    public class CustomClient16Reps
    {
        Host _host = new Host();

        public Boolean GenerateReportUnitRegistry(string _userID, UnitRegistryRpt _unitRegistryRpt)
        {
            #region Subscription Report
            if (_unitRegistryRpt.ReportName.Equals("Subscription Report"))
            {
                try
                {
                    using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                    {
                        DbCon.Open();
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            string _statusSubs = "";
                            string _paramFund = "";
                            string _paramAgent = "";
                            string _paramClient = "";

                            if (!_host.findString(_unitRegistryRpt.FundFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_unitRegistryRpt.FundFrom))
                            {
                                _paramFund = "And A.FundPK in ( " + _unitRegistryRpt.FundFrom + " ) ";
                            }
                            else
                            {
                                _paramFund = "";
                            }
                            if (!_host.findString(_unitRegistryRpt.AgentFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_unitRegistryRpt.AgentFrom))
                            {
                                _paramAgent = "And A.AgentPK in ( " + _unitRegistryRpt.AgentFrom + " ) ";
                            }
                            else
                            {
                                _paramAgent = "";
                            }
                            if (!_host.findString(_unitRegistryRpt.FundClientFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_unitRegistryRpt.FundClientFrom))
                            {
                                _paramClient = "And A.FundClientPK in ( " + _unitRegistryRpt.FundClientFrom + " ) ";
                            }
                            else
                            {
                                _paramClient = "";
                            }

                            if (_unitRegistryRpt.Status == 1)
                            {
                                _statusSubs = "  A.Status = 2 and A.Posted = 1 and A.Revised = 0 ";
                                //_statusRedemp = "  CR.Status = 2 and CR.Posted = 1 and CR.Revised = 0 ";
                            }
                            else if (_unitRegistryRpt.Status == 2)
                            {
                                _statusSubs = "  A.Status = 2 and A.Posted = 1 and A.Revised = 1 ";
                                //_statusRedemp = "  CR.Status = 2 and CR.Posted = 1 and CR.Revised = 1 ";
                            }
                            else if (_unitRegistryRpt.Status == 3)
                            {
                                _statusSubs = "  A.Status = 2 and A.Posted = 0 and A.Revised = 0 ";
                                //_statusRedemp = "  CR.Status = 2 and CR.Posted = 0 and CR.Revised = 0 ";
                            }
                            else if (_unitRegistryRpt.Status == 4)
                            {
                                _statusSubs = "  A.Status = 1  ";
                                //_statusRedemp = "  CR.Status = 1  ";
                            }
                            else if (_unitRegistryRpt.Status == 5)
                            {
                                _statusSubs = "  A.Status = 3  ";
                                //_statusRedemp = "  CR.Status = 3  ";
                            }
                            else if (_unitRegistryRpt.Status == 6)
                            {
                                _statusSubs = "  (A.Status = 2 or A.Posted = 1) and A.Revised = 0  ";
                                //_statusRedemp = "  (CR.Status = 2 or CR.Posted = 1) and CR.Revised = 0  ";
                            }
                            else if (_unitRegistryRpt.Status == 7)
                            {
                                _statusSubs = "  (A.Status = 1 Or A.Status = 2 or A.Posted = 1) and  A.Revised = 0  ";
                                //_statusRedemp = "  (CR.Status = 1 Or CR.Status = 2 or CR.Posted = 1) and CR.Revised = 0  ";
                            }


                            cmd.CommandText =

                            @"BEGIN    
                            SELECT 'Subscription' Type,ClientSubscriptionPK,D.ContactPerson ContactPerson,D.ID BankBranchID,D.Fax1 FaxNo,ValueDate Date,NavDate NavDate, 
                            C.Name FundName,B.ID ClientID, B.Name ClientName,A.NAV NAVAmount,CashAmount,
                            unitamount UnitAmount, A.SubscriptionFeePercent FeePercent,A.SubscriptionFeeAmount FeeAmount,
                            A.Description Remark,A.TotalCashAmount NetAmount,B.NPWP NPWP,
                            E.Name Agent,F.Name Mgt, Case when A.Type = 1 then 'Ordinary' else 'Reguler' end SubsType
                            from ClientSubscription A    
                            left join FundClient B ON B.fundclientpk = A.fundclientpk and B.status = 2   
                            left join Fund C ON A.FundPK = C.FundPK  and C.status = 2     
                            left join BankBranch D ON C.BankBranchPK = D.BankBranchPK  and D.status = 2  
                            left join Agent E on B.SellingAgentPK = E.AgentPK and E.Status = 2
                            left join Agent F on E.ParentPK = F.AgentPK and F.Status = 2
                            WHERE   " + _statusSubs + _paramFund + _paramAgent + _paramClient +
                            @"and NAVDate between @DateFrom and @DateTo 
                            END  ";
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
                                    string filePath = Tools.ReportsPath + "SubscriptionReport" + "_" + _userID + ".xlsx";
                                    string pdfPath = Tools.ReportsPath + "SubscriptionReport" + "_" + _userID + ".pdf";
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

                                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Subscription Report");


                                        //ATUR DATA GROUPINGNYA DULU
                                        List<ClientSubscriptionRept_SAM> rList = new List<ClientSubscriptionRept_SAM>();
                                        while (dr0.Read())
                                        {
                                            ClientSubscriptionRept_SAM rSingle = new ClientSubscriptionRept_SAM();
                                            rSingle.ClientSubscriptionPK = Convert.ToInt32(dr0["ClientSubscriptionPK"]);
                                            rSingle.FundName = Convert.ToString(dr0["FundName"]);
                                            rSingle.NAVDate = Convert.ToDateTime(dr0["NAVDate"]);
                                            rSingle.Date = Convert.ToDateTime(dr0["Date"]);
                                            rSingle.NAVAmount = Convert.ToDecimal(dr0["NAVAmount"]);
                                            rSingle.ClientID = Convert.ToString(dr0["ClientID"]);
                                            rSingle.ClientName = Convert.ToString(dr0["ClientName"]);
                                            rSingle.CashAmount = Convert.ToDecimal(dr0["CashAmount"]);
                                            rSingle.UnitAmount = Convert.ToDecimal(dr0["UnitAmount"]);
                                            rSingle.FeePercent = Convert.ToDecimal(dr0["FeePercent"]);
                                            rSingle.FeeAmount = Convert.ToDecimal(dr0["FeeAmount"]);
                                            rSingle.ContactPerson = Convert.ToString(dr0["ContactPerson"]);
                                            rSingle.FaxNo = Convert.ToString(dr0["FaxNo"]);
                                            rSingle.BankBranchID = Convert.ToString(dr0["BankBranchID"]);
                                            rSingle.Remark = Convert.ToString(dr0["Remark"]);
                                            rSingle.NetAmount = Convert.ToDecimal(dr0["NetAmount"]);
                                            rSingle.NPWP = Convert.ToString(dr0["NPWP"]);
                                            rSingle.Agent = Convert.ToString(dr0["Agent"]);
                                            rSingle.Mgt = Convert.ToString(dr0["Mgt"]);
                                            rSingle.Type = Convert.ToString(dr0["SubsType"]);

                                            rList.Add(rSingle);

                                        }


                                        var QueryByFundID =
                                            from r in rList
                                            group r by new { r.FundName } into rGroup
                                            select rGroup;

                                        int incRowExcel = 1;

                                        foreach (var rsHeader in QueryByFundID)
                                        {



                                            incRowExcel = incRowExcel + 1;

                                            int RowZ = incRowExcel;
                                            incRowExcel = incRowExcel + 2;
                                            worksheet.Cells[incRowExcel, 1].Value = _host.Get_CompanyName();
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Size = 14;
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells["A" + incRowExcel + ":E" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 12].Value = "Date       :";
                                            worksheet.Cells[incRowExcel, 13].Value = DateTime.Now;
                                            worksheet.Cells[incRowExcel, 13].Style.Numberformat.Format = "dd-MMMM-yyyy";
                                            worksheet.Cells[incRowExcel, 13].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            worksheet.Cells["M" + incRowExcel + ":M" + incRowExcel].Merge = true;
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Value = "SUBSCRIPTION REPORT";
                                            worksheet.Cells["A" + incRowExcel + ":E" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 12].Value = "Time       :";
                                            worksheet.Cells["L" + incRowExcel + ":L" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 13].Value = DateTime.Now.ToString("H:mm:ss");
                                            worksheet.Cells["M" + incRowExcel + ":M" + incRowExcel].Merge = true;
                                            incRowExcel++;

                                            //incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Value = "Period";
                                            worksheet.Cells[incRowExcel, 3].Value = ": ";
                                            worksheet.Cells[incRowExcel, 4].Value = _unitRegistryRpt.ValueDateFrom + " - " + _unitRegistryRpt.ValueDateTo;

                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Value = "Fund";
                                            worksheet.Cells[incRowExcel, 3].Value = ": ";
                                            worksheet.Cells[incRowExcel, 4].Value = rsHeader.Key.FundName;

                                            worksheet.Cells[incRowExcel, 8].Value = "Scope      :";

                                            worksheet.Cells[incRowExcel, 9].Value = "All Subscriptions";
                                            worksheet.Cells["I" + incRowExcel + ":I" + incRowExcel].Merge = true;
                                            incRowExcel++;


                                            incRowExcel = incRowExcel + 1;


                                            //incRowExcel++;

                                            //Row B = 3
                                            int RowB = incRowExcel;
                                            int RowG = incRowExcel + 1;


                                            worksheet.Cells[incRowExcel, 1].Value = "No";
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells["A" + RowB + ":A" + RowG].Merge = true;
                                            worksheet.Cells["A" + RowB + ":A" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["A" + RowB + ":A" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;


                                            worksheet.Cells[incRowExcel, 2].Value = "Customer";
                                            worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[RowG, 2].Value = "ID";
                                            worksheet.Cells[RowG, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[RowG, 2].Style.Font.Bold = true;


                                            worksheet.Cells[incRowExcel, 3].Value = "Customer";
                                            worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;
                                            worksheet.Cells["C" + RowB + ":D" + RowG].Merge = true;
                                            worksheet.Cells["C" + RowB + ":D" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["C" + RowB + ":D" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 5].Value = "Trade Date";
                                            worksheet.Cells[incRowExcel, 5].Style.Font.Bold = true;
                                            worksheet.Cells["E" + RowB + ":F" + RowG].Merge = true;
                                            worksheet.Cells["E" + RowB + ":F" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["E" + RowB + ":F" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;


                                            worksheet.Cells[incRowExcel, 7].Value = "Gross Amount";
                                            worksheet.Cells[incRowExcel, 7].Style.Font.Bold = true;
                                            worksheet.Cells["G" + RowB + ":G" + RowG].Merge = true;
                                            worksheet.Cells["G" + RowB + ":G" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["G" + RowB + ":G" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 8].Value = "Subs";
                                            worksheet.Cells[incRowExcel, 8].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[RowG, 8].Value = "Fee %";
                                            worksheet.Cells[RowG, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[RowG, 8].Style.Font.Bold = true;

                                            worksheet.Cells[incRowExcel, 9].Value = "Subscription";
                                            worksheet.Cells[incRowExcel, 9].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[RowG, 9].Value = "Fee";
                                            worksheet.Cells[RowG, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[RowG, 9].Style.Font.Bold = true;

                                            worksheet.Cells["I" + RowB + ":J" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["I" + RowB + ":J" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 11].Value = "Net Amount";
                                            worksheet.Cells[incRowExcel, 11].Style.Font.Bold = true;
                                            worksheet.Cells["K" + RowB + ":K" + RowG].Merge = true;
                                            worksheet.Cells["K" + RowB + ":K" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["K" + RowB + ":K" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 12].Value = "NAV";
                                            worksheet.Cells[incRowExcel, 12].Style.Font.Bold = true;
                                            worksheet.Cells["L" + RowB + ":L" + RowG].Merge = true;
                                            worksheet.Cells["L" + RowB + ":L" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["L" + RowB + ":L" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 13].Value = "Units";
                                            worksheet.Cells[incRowExcel, 13].Style.Font.Bold = true;
                                            worksheet.Cells["M" + RowB + ":M" + RowG].Merge = true;
                                            worksheet.Cells["M" + RowB + ":M" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["M" + RowB + ":M" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 14].Value = "Agent";
                                            worksheet.Cells[incRowExcel, 14].Style.Font.Bold = true;
                                            worksheet.Cells["N" + RowB + ":N" + RowG].Merge = true;
                                            worksheet.Cells["N" + RowB + ":N" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["N" + RowB + ":N" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 15].Value = "Type";
                                            worksheet.Cells[incRowExcel, 15].Style.Font.Bold = true;
                                            worksheet.Cells["O" + RowB + ":O" + RowG].Merge = true;
                                            worksheet.Cells["O" + RowB + ":O" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["O" + RowB + ":O" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;


                                            worksheet.Cells[incRowExcel, 16].Value = "Mgt";
                                            worksheet.Cells[incRowExcel, 16].Style.Font.Bold = true;
                                            worksheet.Cells["P" + RowB + ":P" + RowG].Merge = true;
                                            worksheet.Cells["P" + RowB + ":P" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["P" + RowB + ":P" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;






                                            incRowExcel++;

                                            // Row C = 4
                                            int RowC = incRowExcel;

                                            incRowExcel++;
                                            //area header

                                            int _no = 1;
                                            int _startRowDetail = incRowExcel;
                                            int _endRowDetail = 0;
                                            foreach (var rsDetail in rsHeader)
                                            {
                                                //Row D = 5
                                                int RowD = incRowExcel;
                                                int RowE = incRowExcel + 1;


                                                //ThickBox Border

                                                worksheet.Cells["A" + RowB + ":P" + RowG].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + RowB + ":P" + RowG].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                                //area detail
                                                worksheet.Cells[incRowExcel, 1].Value = _no;
                                                worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 2].Value = rsDetail.ClientID;
                                                worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.ClientName;
                                                worksheet.Cells[incRowExcel, 5].Value = rsDetail.Date;
                                                worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "dd/MMM/yyyy";
                                                worksheet.Cells[incRowExcel, 7].Value = rsDetail.CashAmount;
                                                worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 8].Value = rsDetail.FeePercent;
                                                worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0.0000";
                                                worksheet.Cells[incRowExcel, 9].Value = rsDetail.FeeAmount;
                                                worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 11].Value = rsDetail.NetAmount;
                                                worksheet.Cells[incRowExcel, 11].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 12].Value = rsDetail.NAVAmount;
                                                worksheet.Cells[incRowExcel, 12].Style.Numberformat.Format = "#,##0.0000";
                                                worksheet.Cells[incRowExcel, 13].Value = rsDetail.UnitAmount;
                                                worksheet.Cells[incRowExcel, 13].Style.Numberformat.Format = "#,##0.0000";
                                                worksheet.Cells[incRowExcel, 14].Value = rsDetail.Agent;
                                                //worksheet.Cells[incRowExcel, 14].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 15].Value = rsDetail.Type;
                                                worksheet.Cells[incRowExcel, 16].Value = rsDetail.Mgt;
                                                worksheet.Cells[incRowExcel, 16].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                _endRowDetail = incRowExcel;
                                                _no++;
                                                incRowExcel++;

                                            }


                                            worksheet.Cells["A" + _endRowDetail + ":P" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells[incRowExcel, 4].Value = "Total :";
                                            worksheet.Cells[incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                            worksheet.Cells[incRowExcel, 7].Formula = "SUM(G" + _startRowDetail + ":G" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 9].Formula = "SUM(I" + _startRowDetail + ":I" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 11].Formula = "SUM(K" + _startRowDetail + ":K" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 13].Formula = "SUM(M" + _startRowDetail + ":M" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 7].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 9].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 11].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 13].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 7].Calculate();
                                            worksheet.Cells[incRowExcel, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                            worksheet.Cells[incRowExcel, 9].Calculate();
                                            worksheet.Cells[incRowExcel, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                            worksheet.Cells[incRowExcel, 11].Calculate();
                                            worksheet.Cells[incRowExcel, 11].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                            worksheet.Cells["I" + incRowExcel + ":J" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 13].Calculate();
                                            worksheet.Cells[incRowExcel, 13].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                            worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0";
                                            worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0";
                                            worksheet.Cells[incRowExcel, 11].Style.Numberformat.Format = "#,##0";
                                            worksheet.Cells[incRowExcel, 13].Style.Numberformat.Format = "#,##0.0000";

                                            incRowExcel++;

                                            incRowExcel = incRowExcel + 4;

                                            worksheet.Cells[incRowExcel, 1].Value = "Disclaimer : ";
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Merge = true;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Size = 13;

                                            incRowExcel++;

                                            worksheet.Cells[incRowExcel, 1].Value = "Dokumen ini dipersiapkan oleh PT SUCOR SEKURITAS dan hanya bisa digunakan untuk kepentingan investor tersebut diatas dan tidak untuk pihak lainnya. Laporan ini bukan merupakan konfirmasi dari PT SUCOR SEKURITAS dan tidak untuk menggantikan laporan yang wajib diterbitkan oleh Bank Kustodian, jika ada perbedaan antara laporan ini dengan laporan Bank Kustodian, maka laporan Bank Kustodian adalah yang benar. Laporan ini diproses oleh computer dan tidak memerlukan tandatangan.";
                                            worksheet.Cells[incRowExcel, 1, incRowExcel + 2, 15].Style.Font.Size = 12;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel + 2, 15].Merge = true;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel + 2, 15].Style.WrapText = true;

                                            incRowExcel = incRowExcel + 4;

                                            worksheet.Cells[incRowExcel, 1].Value = "PT SUCOR SEKURITAS terdaftar dan diawasi oleh OJK, dengan nomor registrasi KEP-01/PM/MI/1999";
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 15].Style.Font.Size = 12;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 15].Merge = true;

                                            incRowExcel = incRowExcel + 2;

                                            //worksheet.Cells[incRowExcel, 1].Value = "PT SUCOR SEKURITAS";
                                            //worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Merge = true;
                                            //worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Bold = true;
                                            //worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Size = 13;

                                            //incRowExcel++;

                                            //worksheet.Cells[incRowExcel, 1].Value = "Sahid Sudirman Center, 12th Fl.";
                                            //worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Merge = true;
                                            //worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Size = 12;

                                            //incRowExcel++;

                                            //worksheet.Cells[incRowExcel, 1].Value = "Jl. Jenderal Sudirman Kav. 86, Jakarta 10220 - Indonesia";
                                            //worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Merge = true;
                                            //worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Size = 12;

                                            //incRowExcel++;

                                            //worksheet.Cells[incRowExcel, 1].Value = "p. +62-21  8067 3000, f.  +62-21  2788 9288 |sucorsekuritas.com";
                                            //worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Merge = true;
                                            //worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Size = 12;

                                            worksheet.Row(incRowExcel).PageBreak = _unitRegistryRpt.PageBreak;

                                        }
                                        //incRowExcel++;
                                        int _lastRow = incRowExcel;

                                        string _rangeA = "A5:P" + incRowExcel;
                                        using (ExcelRange r = worksheet.Cells[_rangeA]) // KALO  KOLOM 1 SAMPE 9 A-I
                                        {
                                            r.Style.Font.Size = 11;
                                        }

                                        worksheet.DeleteRow(_lastRow);


                                        worksheet.PrinterSettings.FitToPage = true;
                                        worksheet.PrinterSettings.FitToWidth = 1;
                                        worksheet.PrinterSettings.FitToHeight = 0;
                                        worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel - 1, 16];
                                        worksheet.Column(1).Width = 5;
                                        worksheet.Column(2).Width = 10;
                                        worksheet.Column(3).Width = 5;
                                        worksheet.Column(4).Width = 40;
                                        worksheet.Column(5).Width = 15;
                                        worksheet.Column(6).Width = 5;
                                        worksheet.Column(7).Width = 20;
                                        worksheet.Column(8).Width = 10;
                                        worksheet.Column(9).Width = 15;
                                        worksheet.Column(10).Width = 5;
                                        worksheet.Column(11).Width = 20;
                                        worksheet.Column(12).Width = 15;
                                        worksheet.Column(13).Width = 15;
                                        worksheet.Column(14).Width = 30;
                                        worksheet.Column(15).Width = 10;
                                        worksheet.Column(16).Width = 10;



                                        // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                        //worksheet.Cells["A3:I14"].AutoFitColumns(); // UNTUK AUTO FIT UKURAN TULISAN DENGAN KOLOM, KEBETULAN INI BUAT HEADER AJA DIPAKENYA
                                        //worksheetApproved.Cells["A3:E3"].AutoFitColumns();
                                        worksheet.PrinterSettings.Orientation = eOrientation.Landscape; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                        worksheet.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN

                                        //worksheet.HeaderFooter.OddHeader.CenteredText = "\n \n \n \n \n \n \n &30&B SUBSCRIPTION \n &28&B Batch Form";

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

            #region Redemption Report
            else if (_unitRegistryRpt.ReportName.Equals("Redemption Report"))
            {
                try
                {
                    using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                    {
                        DbCon.Open();
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            string _statusRedemp = "";
                            string _paramFund = "";
                            string _paramAgent = "";

                            if (!_host.findString(_unitRegistryRpt.FundFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_unitRegistryRpt.FundFrom))
                            {
                                _paramFund = "And A.FundPK in ( " + _unitRegistryRpt.FundFrom + " ) ";
                            }
                            else
                            {
                                _paramFund = "";
                            }
                            if (!_host.findString(_unitRegistryRpt.AgentFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_unitRegistryRpt.AgentFrom))
                            {
                                _paramAgent = "And A.AgentPK in ( " + _unitRegistryRpt.AgentFrom + " ) ";
                            }
                            else
                            {
                                _paramAgent = "";
                            }

                            if (_unitRegistryRpt.Status == 1)
                            {
                                _statusRedemp = "  A.Status = 2 and A.Posted = 1 and A.Revised = 0 ";
                                //_statusRedemp = "  CR.Status = 2 and CR.Posted = 1 and CR.Revised = 0 ";
                            }
                            else if (_unitRegistryRpt.Status == 2)
                            {
                                _statusRedemp = "  A.Status = 2 and A.Posted = 1 and A.Revised = 1 ";
                                //_statusRedemp = "  CR.Status = 2 and CR.Posted = 1 and CR.Revised = 1 ";
                            }
                            else if (_unitRegistryRpt.Status == 3)
                            {
                                _statusRedemp = "  A.Status = 2 and A.Posted = 0 and A.Revised = 0 ";
                                //_statusRedemp = "  CR.Status = 2 and CR.Posted = 0 and CR.Revised = 0 ";
                            }
                            else if (_unitRegistryRpt.Status == 4)
                            {
                                _statusRedemp = "  A.Status = 1  ";
                                //_statusRedemp = "  CR.Status = 1  ";
                            }
                            else if (_unitRegistryRpt.Status == 5)
                            {
                                _statusRedemp = "  A.Status = 3  ";
                                //_statusRedemp = "  CR.Status = 3  ";
                            }
                            else if (_unitRegistryRpt.Status == 6)
                            {
                                _statusRedemp = "  (A.Status = 2 or A.Posted = 1) and A.Revised = 0  ";
                                //_statusRedemp = "  (CR.Status = 2 or CR.Posted = 1) and CR.Revised = 0  ";
                            }
                            else if (_unitRegistryRpt.Status == 7)
                            {
                                _statusRedemp = "  (A.Status = 1 Or A.Status = 2 or A.Posted = 1) and  A.Revised = 0  ";
                                //_statusRedemp = "  (CR.Status = 1 Or CR.Status = 2 or CR.Posted = 1) and CR.Revised = 0  ";
                            }


                            cmd.CommandText =

                            @"BEGIN    
                            SELECT 'Redemption' Type,ClientRedemptionPK,D.ContactPerson ContactPerson,D.ID BankBranchID,D.Fax1 FaxNo,ValueDate Date,NavDate NavDate, C.Name FundName,B.ID ClientID, 
                            B.Name ClientName,A.NAV NAVAmount,CashAmount,unitamount UnitAmount, A.RedemptionFeePercent FeePercent,A.RedemptionFeeAmount FeeAmount, 
                            A.PaymentDate SettlementDate,A.Description Remark,A.TotalCashAmount NetAmount,B.NPWP NPWP,  E.Name Agent,F.Name Mgt  from ClientRedemption A    
                            left join FundClient B ON B.fundclientpk = A.fundclientpk and B.status = 2   
                            left join Fund C ON A.FundPK = C.FundPK  and C.status = 2     
                            left join BankBranch D ON C.BankBranchPK = D.BankBranchPK  and D.status = 2
                            left join Agent E on B.SellingAgentPK = E.AgentPK and E.Status = 2
                            left join Agent F on E.ParentPK = F.AgentPK and F.Status = 2   
                            WHERE   " + _statusRedemp + _paramFund + _paramAgent +
                            @"and NAVDate between @DateFrom and @DateTo 
                            END  ";
                            cmd.CommandTimeout = 0;
                            cmd.Parameters.AddWithValue("@DateFrom", _unitRegistryRpt.ValueDateFrom);
                            cmd.Parameters.AddWithValue("@DateTo", _unitRegistryRpt.ValueDateTo);
                            cmd.Parameters.AddWithValue("@FundFrom", _unitRegistryRpt.FundFrom);
                            cmd.Parameters.AddWithValue("@AgentFrom", _unitRegistryRpt.AgentFrom);


                            using (SqlDataReader dr0 = cmd.ExecuteReader())
                            {
                                if (!dr0.HasRows)
                                {
                                    return false;
                                }
                                else
                                {
                                    string filePath = Tools.ReportsPath + "RedemptionReport" + "_" + _userID + ".xlsx";
                                    string pdfPath = Tools.ReportsPath + "RedemptionReport" + "_" + _userID + ".pdf";
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

                                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Redemption Report");


                                        //ATUR DATA GROUPINGNYA DULU
                                        List<UnitRegistryRpt> rList = new List<UnitRegistryRpt>();
                                        while (dr0.Read())
                                        {
                                            UnitRegistryRpt rSingle = new UnitRegistryRpt();
                                            rSingle.ClientRedemptionPK = Convert.ToInt32(dr0["ClientRedemptionPK"]);
                                            rSingle.FundName = Convert.ToString(dr0["FundName"]);
                                            rSingle.NAVDate = Convert.ToDateTime(dr0["NAVDate"]);
                                            rSingle.Date = Convert.ToDateTime(dr0["Date"]);
                                            rSingle.SettlementDate = Convert.ToDateTime(dr0["SettlementDate"]);
                                            rSingle.Nav = Convert.ToDecimal(dr0["NAVAmount"]);
                                            rSingle.ClientID = Convert.ToString(dr0["ClientID"]);
                                            rSingle.ClientName = Convert.ToString(dr0["ClientName"]);
                                            rSingle.CashBalance = Convert.ToDecimal(dr0["CashAmount"]);
                                            rSingle.UnitBalance = Convert.ToDecimal(dr0["UnitAmount"]);
                                            rSingle.FeePercent = Convert.ToDecimal(dr0["FeePercent"]);
                                            rSingle.FeeAmount = Convert.ToDecimal(dr0["FeeAmount"]);
                                            rSingle.ContactPerson = Convert.ToString(dr0["ContactPerson"]);
                                            rSingle.FaxNo = Convert.ToString(dr0["FaxNo"]);
                                            rSingle.BankCustodiID = Convert.ToString(dr0["BankBranchID"]);
                                            rSingle.Remark = Convert.ToString(dr0["Remark"]);
                                            rSingle.NetAmount = Convert.ToDecimal(dr0["NetAmount"]);
                                            rSingle.NPWP = Convert.ToString(dr0["NPWP"]);
                                            rSingle.Agent = Convert.ToString(dr0["Agent"]);
                                            rSingle.Mgt = Convert.ToString(dr0["Mgt"]);
                                            rList.Add(rSingle);

                                        }


                                        var QueryByFundID =
                                            from r in rList
                                            group r by new { r.FundName, r.ContactPerson, r.FaxNo, r.BankCustodiID, r.AgentName } into rGroup
                                            select rGroup;

                                        int incRowExcel = 1;

                                        foreach (var rsHeader in QueryByFundID)
                                        {


                                            //incRowExcel = incRowExcel + 2;

                                            int RowZ = incRowExcel;
                                            incRowExcel = incRowExcel + 4;
                                            worksheet.Cells[incRowExcel, 1].Value = _host.Get_CompanyName();
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Size = 14;
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells["A" + incRowExcel + ":E" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 12].Value = "Date       :";
                                            worksheet.Cells[incRowExcel, 13].Value = DateTime.Now;
                                            worksheet.Cells[incRowExcel, 13].Style.Numberformat.Format = "dd-MMMM-yyyy";
                                            worksheet.Cells[incRowExcel, 13].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            worksheet.Cells["M" + incRowExcel + ":M" + incRowExcel].Merge = true;
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Value = "REDEMPTION REPORT";
                                            worksheet.Cells["A" + incRowExcel + ":E" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 12].Value = "Time       :";
                                            worksheet.Cells["L" + incRowExcel + ":L" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 13].Value = DateTime.Now.ToString("H:mm:ss");
                                            worksheet.Cells["M" + incRowExcel + ":M" + incRowExcel].Merge = true;
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Value = "Period";
                                            worksheet.Cells[incRowExcel, 3].Value = ": ";
                                            worksheet.Cells[incRowExcel, 4].Value = _unitRegistryRpt.ValueDateFrom + " - " + _unitRegistryRpt.ValueDateTo;
                                            worksheet.Cells["O" + incRowExcel + ":O" + incRowExcel].Merge = true;
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Value = "Fund";
                                            worksheet.Cells[incRowExcel, 3].Value = ": ";
                                            worksheet.Cells[incRowExcel, 4].Value = rsHeader.Key.FundName;
                                            worksheet.Cells[incRowExcel, 8].Value = "Scope      :";
                                            worksheet.Cells[incRowExcel, 9].Value = "All Redemption";
                                            worksheet.Cells["I" + incRowExcel + ":I" + incRowExcel].Merge = true;
                                            incRowExcel++;


                                            incRowExcel = incRowExcel + 1;

                                            //Row B = 3
                                            int RowB = incRowExcel;
                                            int RowG = incRowExcel + 1;


                                            worksheet.Cells[incRowExcel, 1].Value = "No";
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells["A" + RowB + ":A" + RowG].Merge = true;
                                            worksheet.Cells["A" + RowB + ":A" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["A" + RowB + ":A" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 2].Value = "Customer";
                                            worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[RowG, 2].Value = "ID";
                                            worksheet.Cells[RowG, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[RowG, 2].Style.Font.Bold = true;


                                            worksheet.Cells[incRowExcel, 3].Value = "Customer";
                                            worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;
                                            worksheet.Cells["C" + RowB + ":D" + RowG].Merge = true;
                                            worksheet.Cells["C" + RowB + ":D" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["C" + RowB + ":D" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 5].Value = "Trade Date";
                                            worksheet.Cells[incRowExcel, 5].Style.Font.Bold = true;
                                            worksheet.Cells["E" + RowB + ":F" + RowG].Merge = true;
                                            worksheet.Cells["E" + RowB + ":F" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["E" + RowB + ":F" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 7].Value = "Gross Amount";
                                            worksheet.Cells[incRowExcel, 7].Style.Font.Bold = true;
                                            worksheet.Cells["G" + RowB + ":G" + RowG].Merge = true;
                                            worksheet.Cells["G" + RowB + ":G" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["G" + RowB + ":G" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 8].Value = "Redem";
                                            worksheet.Cells[incRowExcel, 8].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[RowG, 8].Value = "Fee %";
                                            worksheet.Cells[RowG, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[RowG, 8].Style.Font.Bold = true;

                                            worksheet.Cells[incRowExcel, 9].Value = "Redemption";
                                            worksheet.Cells[incRowExcel, 9].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[RowG, 9].Value = "Fee";
                                            worksheet.Cells[RowG, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[RowG, 9].Style.Font.Bold = true;

                                            worksheet.Cells[incRowExcel, 10].Value = "Net Amount";
                                            worksheet.Cells[incRowExcel, 10].Style.Font.Bold = true;
                                            worksheet.Cells["J" + RowB + ":K" + RowG].Merge = true;
                                            worksheet.Cells["J" + RowB + ":K" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["J" + RowB + ":K" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 12].Value = "NAV";
                                            worksheet.Cells[incRowExcel, 12].Style.Font.Bold = true;
                                            worksheet.Cells["L" + RowB + ":L" + RowG].Merge = true;
                                            worksheet.Cells["L" + RowB + ":L" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["L" + RowB + ":L" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 13].Value = "Units";
                                            worksheet.Cells[incRowExcel, 13].Style.Font.Bold = true;
                                            worksheet.Cells["M" + RowB + ":M" + RowG].Merge = true;
                                            worksheet.Cells["M" + RowB + ":M" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["M" + RowB + ":M" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 14].Value = "Agent";
                                            worksheet.Cells[incRowExcel, 14].Style.Font.Bold = true;
                                            worksheet.Cells["N" + RowB + ":N" + RowG].Merge = true;
                                            worksheet.Cells["N" + RowB + ":N" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["N" + RowB + ":N" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 15].Value = "Mgt";
                                            worksheet.Cells[incRowExcel, 15].Style.Font.Bold = true;
                                            worksheet.Cells["O" + RowB + ":O" + RowG].Merge = true;
                                            worksheet.Cells["O" + RowB + ":O" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["O" + RowB + ":O" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            incRowExcel++;

                                            // Row C = 4
                                            int RowC = incRowExcel;

                                            incRowExcel++;
                                            //area header

                                            int _no = 1;
                                            int _startRowDetail = incRowExcel;
                                            int _endRowDetail = 0;
                                            foreach (var rsDetail in rsHeader)
                                            {
                                                //Row D = 5
                                                int RowD = incRowExcel;
                                                int RowE = incRowExcel + 1;


                                                //ThickBox Border

                                                worksheet.Cells["A" + RowB + ":O" + RowG].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + RowB + ":O" + RowG].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                                //area detail
                                                worksheet.Cells[incRowExcel, 1].Value = _no;
                                                worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 2].Value = rsDetail.ClientID;
                                                worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.ClientName;
                                                worksheet.Cells[incRowExcel, 5].Value = rsDetail.Date;
                                                worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "dd/MMM/yyyy";
                                                worksheet.Cells[incRowExcel, 7].Value = rsDetail.CashBalance;
                                                worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 8].Value = rsDetail.FeePercent;
                                                worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0.0000";
                                                worksheet.Cells[incRowExcel, 9].Value = rsDetail.FeeAmount;
                                                worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 10].Value = rsDetail.NetAmount;
                                                worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells["J" + incRowExcel + ":K" + incRowExcel].Merge = true;
                                                worksheet.Cells[incRowExcel, 12].Value = rsDetail.Nav;
                                                worksheet.Cells[incRowExcel, 12].Style.Numberformat.Format = "#,##0.0000";
                                                worksheet.Cells[incRowExcel, 13].Value = rsDetail.UnitBalance;
                                                worksheet.Cells[incRowExcel, 13].Style.Numberformat.Format = "#,##0.0000";
                                                worksheet.Cells[incRowExcel, 14].Value = rsDetail.Agent;
                                                //worksheet.Cells[incRowExcel, 14].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 15].Value = rsDetail.Mgt;
                                                worksheet.Cells[incRowExcel, 15].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                _endRowDetail = incRowExcel;
                                                _no++;
                                                incRowExcel++;

                                            }


                                            worksheet.Cells["A" + _endRowDetail + ":O" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells[incRowExcel, 4].Value = "Total :";
                                            worksheet.Cells[incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                            worksheet.Cells[incRowExcel, 7].Formula = "SUM(G" + _startRowDetail + ":G" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 9].Formula = "SUM(I" + _startRowDetail + ":I" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 10].Formula = "SUM(J" + _startRowDetail + ":J" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 13].Formula = "SUM(M" + _startRowDetail + ":M" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 7].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 9].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 10].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 13].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 7].Calculate();
                                            worksheet.Cells[incRowExcel, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                            worksheet.Cells[incRowExcel, 9].Calculate();
                                            worksheet.Cells[incRowExcel, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                            worksheet.Cells[incRowExcel, 10].Calculate();
                                            worksheet.Cells[incRowExcel, 10].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                            worksheet.Cells["J" + incRowExcel + ":K" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 13].Calculate();
                                            worksheet.Cells[incRowExcel, 13].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                            worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0";
                                            worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0";
                                            worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0";
                                            worksheet.Cells[incRowExcel, 13].Style.Numberformat.Format = "#,##0.0000";
                                            incRowExcel++;

                                            incRowExcel = incRowExcel + 4;

                                            worksheet.Cells[incRowExcel, 1].Value = "Disclaimer : ";
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Merge = true;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Size = 13;

                                            incRowExcel++;

                                            worksheet.Cells[incRowExcel, 1].Value = "Dokumen ini dipersiapkan oleh PT SUCOR SEKURITAS dan hanya bisa digunakan untuk kepentingan investor tersebut diatas dan tidak untuk pihak lainnya. Laporan ini bukan merupakan konfirmasi dari PT SUCOR SEKURITAS dan tidak untuk menggantikan laporan yang wajib diterbitkan oleh Bank Kustodian, jika ada perbedaan antara laporan ini dengan laporan Bank Kustodian, maka laporan Bank Kustodian adalah yang benar. Laporan ini diproses oleh computer dan tidak memerlukan tandatangan.";
                                            worksheet.Cells[incRowExcel, 1, incRowExcel + 2, 15].Style.Font.Size = 12;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel + 2, 15].Merge = true;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel + 2, 15].Style.WrapText = true;

                                            incRowExcel = incRowExcel + 4;

                                            worksheet.Cells[incRowExcel, 1].Value = "PT SUCOR SEKURITAS terdaftar dan diawasi oleh OJK, dengan nomor registrasi KEP-01/PM/MI/1999";
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 15].Style.Font.Size = 12;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 15].Merge = true;

                                            incRowExcel = incRowExcel + 2;

                                            //worksheet.Cells[incRowExcel, 1].Value = "PT SUCOR SEKURITAS";
                                            //worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Merge = true;
                                            //worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Bold = true;
                                            //worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Size = 13;

                                            //incRowExcel++;

                                            //worksheet.Cells[incRowExcel, 1].Value = "Sahid Sudirman Center, 12th Fl.";
                                            //worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Merge = true;
                                            //worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Size = 12;

                                            //incRowExcel++;

                                            //worksheet.Cells[incRowExcel, 1].Value = "Jl. Jenderal Sudirman Kav. 86, Jakarta 10220 - Indonesia";
                                            //worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Merge = true;
                                            //worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Size = 12;

                                            //incRowExcel++;

                                            //worksheet.Cells[incRowExcel, 1].Value = "p. +62-21  8067 3000, f.  +62-21  2788 9288 |sucorsekuritas.com";
                                            //worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Merge = true;
                                            //worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Size = 12;

                                            worksheet.Row(incRowExcel).PageBreak = _unitRegistryRpt.PageBreak;

                                        }
                                        //incRowExcel++;
                                        int _lastRow = incRowExcel;

                                        string _rangeA = "A5:P" + incRowExcel;
                                        using (ExcelRange r = worksheet.Cells[_rangeA]) // KALO  KOLOM 1 SAMPE 9 A-I
                                        {
                                            r.Style.Font.Size = 11;
                                        }

                                        worksheet.DeleteRow(_lastRow);

                                        worksheet.PrinterSettings.FitToPage = true;
                                        worksheet.PrinterSettings.FitToWidth = 1;
                                        worksheet.PrinterSettings.FitToHeight = 0;
                                        worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel - 1, 15];
                                        worksheet.Column(1).Width = 5;
                                        worksheet.Column(2).Width = 10;
                                        worksheet.Column(3).Width = 5;
                                        worksheet.Column(4).Width = 30;
                                        worksheet.Column(5).Width = 15;
                                        worksheet.Column(6).Width = 5;
                                        worksheet.Column(7).Width = 20;
                                        worksheet.Column(8).Width = 10;
                                        worksheet.Column(9).Width = 15;
                                        worksheet.Column(10).Width = 5;
                                        worksheet.Column(11).Width = 20;
                                        worksheet.Column(12).Width = 15;
                                        worksheet.Column(13).Width = 15;
                                        worksheet.Column(14).Width = 30;
                                        worksheet.Column(15).Width = 20;


                                        // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                        //worksheet.Cells["A3:I14"].AutoFitColumns(); // UNTUK AUTO FIT UKURAN TULISAN DENGAN KOLOM, KEBETULAN INI BUAT HEADER AJA DIPAKENYA
                                        //worksheetApproved.Cells["A3:E3"].AutoFitColumns();
                                        worksheet.PrinterSettings.Orientation = eOrientation.Landscape; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                        worksheet.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN

                                        //worksheet.HeaderFooter.OddHeader.CenteredText = "\n \n \n \n \n \n \n &30&B REDEMPTION \n &28&B Batch Form";

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

            #region Client Tracking
            else if (_unitRegistryRpt.ReportName.Equals("Client Tracking"))
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
                                _paramAgent = "And A.AgentPK in ( " + _unitRegistryRpt.AgentFrom + " ) ";
                            }
                            else
                            {
                                _paramAgent = "";
                            }




                            cmd.CommandText =

                            @"select B.ID FundID,B.Name FundName,isnull(C.NAV,0) NAV,D.Name FundClientName,A.UnitAmount,dbo.[FGetAVGForFundClientPosition](@ValueDateTo,A.FundClientPK,A.FundPK) AvgNAV,
                            E.ID Agent, F.ID Parent
                            from FundClientPosition A
                            left join Fund B on A.FundPK = B.FundPK and B.status = 2
                            left join closeNAV C on A.FundPK = C.FundPK and C.status = 2 and C.Date = @ValueDateTo
                            left join FundClient D on A.FundClientPK = D.FundClientPK and D.Status = 2
                            left join Agent E on D.SellingAgentPK = E.AgentPK and E.status = 2
                            left join Agent F on E.ParentPK = F.AgentPK and F.status = 2
                            where A.Date = @ValueDateTo and A.UnitAmount > 0 and C.NAV > 0 " + _paramFund + _paramFundClient + _paramAgent;

                            cmd.CommandTimeout = 0;
                            cmd.Parameters.AddWithValue("@ValueDateFrom", _unitRegistryRpt.ValueDateFrom);
                            cmd.Parameters.AddWithValue("@ValueDateTo", _unitRegistryRpt.ValueDateTo);
                            cmd.Parameters.AddWithValue("@FundFrom", _unitRegistryRpt.FundFrom);
                            cmd.Parameters.AddWithValue("@FundClientFrom", _unitRegistryRpt.FundClientFrom);
                            cmd.Parameters.AddWithValue("@AgentFrom", _unitRegistryRpt.AgentFrom);



                            using (SqlDataReader dr0 = cmd.ExecuteReader())
                            {
                                if (!dr0.HasRows)
                                {
                                    return false;
                                }
                                else
                                {
                                    string filePath = Tools.ReportsPath + "ClientTrackingReport" + "_" + _userID + ".xlsx";
                                    string pdfPath = Tools.ReportsPath + "ClientTrackingReport" + "_" + _userID + ".pdf";
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

                                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Client Tracking Report");


                                        //ATUR DATA GROUPINGNYA DULU
                                        List<UnitRegistryRpt> rList = new List<UnitRegistryRpt>();
                                        while (dr0.Read())
                                        {
                                            UnitRegistryRpt rSingle = new UnitRegistryRpt();
                                            rSingle.ValueDate = Convert.ToDateTime(_unitRegistryRpt.ValueDateTo);
                                            rSingle.ClientName = Convert.ToString(dr0["FundClientName"]);
                                            rSingle.DepartmentName = Convert.ToString(dr0["Parent"]);
                                            rSingle.FundName = Convert.ToString(dr0["FundName"]);
                                            rSingle.UnitBalance = Convert.ToDecimal(dr0["UnitAmount"]);
                                            rSingle.AvgNav = Convert.ToDecimal(dr0["AvgNav"]);
                                            rSingle.CloseNav = Convert.ToDecimal(dr0["NAV"]);
                                            rSingle.AgentName = Convert.ToString(dr0["Agent"]);
                                            rList.Add(rSingle);

                                        }


                                        var QueryByFundID =
                                            from r in rList
                                            group r by new { r.FundName, r.CloseNav, r.DepartmentName } into rGroup
                                            select rGroup;

                                        int incRowExcel = 1;
                                        worksheet.Cells[incRowExcel, 1].Value = _host.Get_CompanyName();
                                        worksheet.Cells["A" + incRowExcel + ":E" + incRowExcel].Merge = true;
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Size = 40;
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "FUND BALANCE DETAIL";
                                        worksheet.Cells["A" + incRowExcel + ":E" + incRowExcel].Merge = true;
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Size = 40;

                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "As Of : " + _unitRegistryRpt.ValueDateTo;
                                        worksheet.Cells["A" + incRowExcel + ":E" + incRowExcel].Merge = true;
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Size = 40;


                                        incRowExcel++;

                                        incRowExcel = incRowExcel + 2;

                                        //Row A = 2
                                        int RowA = incRowExcel;

                                        incRowExcel = incRowExcel + 2;



                                        foreach (var rsHeader in QueryByFundID)
                                        {

                                            int RowZ = incRowExcel;
                                            incRowExcel++;


                                            //incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Value = "Fund : ";
                                            worksheet.Cells["A" + incRowExcel + ":A" + incRowExcel].Style.Font.Bold = true;
                                            worksheet.Cells["A" + incRowExcel + ":A" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 2].Value = rsHeader.Key.FundName;
                                            worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                            worksheet.Cells["B" + incRowExcel + ":D" + incRowExcel].Merge = true;

                                            worksheet.Cells[incRowExcel, 5].Value = "Close Nav : ";
                                            worksheet.Cells[incRowExcel, 6].Value = rsHeader.Key.CloseNav;
                                            worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.0000";
                                            worksheet.Cells[incRowExcel, 5].Style.Font.Bold = true;

                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Value = "Branch :";
                                            worksheet.Cells[incRowExcel, 3].Value = rsHeader.Key.DepartmentName;
                                            worksheet.Cells["C" + incRowExcel + ":E" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Size = 40;


                                            incRowExcel = incRowExcel + 2;
                                            //Row B = 3
                                            int RowBZ = incRowExcel;
                                            int RowGZ = incRowExcel + 1;


                                            worksheet.Cells[incRowExcel, 1].Value = "No";
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells["A" + RowBZ + ":A" + RowGZ].Merge = true;
                                            worksheet.Cells["A" + RowBZ + ":A" + RowGZ].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["A" + RowBZ + ":A" + RowGZ].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 2].Value = "Mkt";
                                            worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                            worksheet.Cells["B" + RowBZ + ":C" + RowGZ].Merge = true;
                                            worksheet.Cells["B" + RowBZ + ":C" + RowGZ].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["B" + RowBZ + ":C" + RowGZ].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 4].Value = "Customer";
                                            worksheet.Cells[incRowExcel, 4].Style.Font.Bold = true;
                                            worksheet.Cells["D" + RowBZ + ":D" + RowGZ].Merge = true;
                                            worksheet.Cells["D" + RowBZ + ":D" + RowGZ].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["D" + RowBZ + ":D" + RowGZ].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 5].Value = "Unit";
                                            worksheet.Cells[incRowExcel, 5].Style.Font.Bold = true;
                                            worksheet.Cells["E" + RowBZ + ":E" + RowGZ].Merge = true;
                                            worksheet.Cells["E" + RowBZ + ":E" + RowGZ].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["E" + RowBZ + ":E" + RowGZ].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 6].Value = "Average Nav";
                                            worksheet.Cells[incRowExcel, 6].Style.Font.Bold = true;
                                            worksheet.Cells["F" + RowBZ + ":F" + RowGZ].Merge = true;
                                            worksheet.Cells["F" + RowBZ + ":F" + RowGZ].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["F" + RowBZ + ":F" + RowGZ].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 7].Value = "Amount At Avg Nav";

                                            worksheet.Cells[incRowExcel, 7].Style.Font.Bold = true;
                                            worksheet.Cells["G" + RowBZ + ":G" + RowGZ].Merge = true;
                                            worksheet.Cells["G" + RowBZ + ":G" + RowGZ].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["G" + RowBZ + ":G" + RowGZ].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;


                                            worksheet.Cells[incRowExcel, 8].Value = "Amount At Close Nav";
                                            worksheet.Cells[incRowExcel, 8].Style.Font.Bold = true;
                                            worksheet.Cells["H" + RowBZ + ":H" + RowGZ].Merge = true;
                                            worksheet.Cells["H" + RowBZ + ":H" + RowGZ].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["H" + RowBZ + ":H" + RowGZ].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 9].Value = "Unrealized (Gain/Loss)";
                                            worksheet.Cells[incRowExcel, 9].Style.Font.Bold = true;
                                            worksheet.Cells["I" + RowBZ + ":J" + RowGZ].Merge = true;
                                            worksheet.Cells["I" + RowBZ + ":J" + RowGZ].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["I" + RowBZ + ":J" + RowGZ].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 11].Value = "%";
                                            worksheet.Cells[incRowExcel, 11].Style.Font.Bold = true;
                                            worksheet.Cells["K" + RowBZ + ":K" + RowGZ].Merge = true;
                                            worksheet.Cells["K" + RowBZ + ":K" + RowGZ].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["K" + RowBZ + ":K" + RowGZ].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Row(incRowExcel).Height = 75;


                                            incRowExcel++;

                                            // Row C = 4
                                            int RowC = incRowExcel;

                                            incRowExcel++;
                                            //area header

                                            int _no = 1;
                                            int _startRowDetail = incRowExcel;
                                            int _endRowDetail = 0;
                                            foreach (var rsDetail in rsHeader)
                                            {
                                                //Row D = 5
                                                int RowD = incRowExcel;
                                                int RowE = incRowExcel + 1;


                                                //ThickBox Border

                                                worksheet.Cells["A" + RowBZ + ":K" + RowGZ].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + RowBZ + ":K" + RowGZ].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                                //area detail
                                                worksheet.Cells[incRowExcel, 1].Value = _no;
                                                worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 2].Value = rsDetail.AgentName;
                                                worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 4].Value = rsDetail.ClientName;
                                                worksheet.Cells[incRowExcel, 5].Value = rsDetail.UnitBalance;
                                                worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.0000";
                                                worksheet.Cells[incRowExcel, 6].Value = rsDetail.AvgNav;
                                                worksheet.Cells[incRowExcel, 6].Calculate();
                                                worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.0000";

                                                //disini
                                                worksheet.Cells[incRowExcel, 7].Value = rsDetail.UnitBalance * rsDetail.AvgNav;
                                                worksheet.Cells[incRowExcel, 7].Calculate();
                                                worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0.0000";

                                                worksheet.Cells[incRowExcel, 8].Value = rsDetail.UnitBalance * rsHeader.Key.CloseNav;
                                                worksheet.Cells[incRowExcel, 8].Calculate();
                                                worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0.0000";

                                                worksheet.Cells[incRowExcel, 9].Value = (rsDetail.UnitBalance * rsHeader.Key.CloseNav) - (rsDetail.UnitBalance * rsDetail.AvgNav);
                                                worksheet.Cells[incRowExcel, 9].Calculate();
                                                worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0.0000";

                                                worksheet.Cells["I" + incRowExcel + ":J" + incRowExcel].Merge = true;
                                                worksheet.Cells[incRowExcel, 11].Value = (((rsDetail.UnitBalance * rsHeader.Key.CloseNav) - (rsDetail.UnitBalance * rsDetail.AvgNav)) / (rsDetail.UnitBalance * rsHeader.Key.CloseNav)) * 100;
                                                worksheet.Cells[incRowExcel, 11].Calculate();
                                                worksheet.Cells[incRowExcel, 11].Style.Numberformat.Format = "#,##0.0000";


                                                _endRowDetail = incRowExcel;
                                                _no++;
                                                incRowExcel++;

                                            }

                                            worksheet.Cells["A" + RowBZ].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + RowBZ + ":A" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + RowBZ + ":A" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells["D" + RowBZ].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["D" + RowBZ + ":D" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["D" + RowBZ + ":D" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["D" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells["F" + RowBZ].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["F" + RowBZ + ":F" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["F" + RowBZ + ":F" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["F" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells["G" + RowBZ].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["G" + RowBZ + ":G" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["G" + RowBZ + ":G" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["G" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells["H" + RowBZ].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["H" + RowBZ + ":H" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["H" + RowBZ + ":H" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["H" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells["K" + RowBZ].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["K" + RowBZ + ":K" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["K" + RowBZ + ":K" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["K" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells["A" + _endRowDetail + ":K" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells[incRowExcel, 4].Value = "Total :";
                                            worksheet.Cells[incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                            worksheet.Cells[incRowExcel, 5].Formula = "SUM(E" + _startRowDetail + ":E" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 7].Formula = "SUM(G" + _startRowDetail + ":G" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 8].Formula = "SUM(H" + _startRowDetail + ":H" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 9].Formula = "SUM(I" + _startRowDetail + ":I" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 5].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 7].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 8].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 9].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 5].Calculate();
                                            worksheet.Cells[incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                            worksheet.Cells[incRowExcel, 7].Calculate();
                                            worksheet.Cells[incRowExcel, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                            worksheet.Cells[incRowExcel, 8].Calculate();
                                            worksheet.Cells[incRowExcel, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                            worksheet.Cells[incRowExcel, 9].Calculate();
                                            worksheet.Cells[incRowExcel, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                            worksheet.Cells["I" + incRowExcel + ":J" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.0000";
                                            worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0.0000";
                                            worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0.0000";
                                            worksheet.Cells["I" + incRowExcel + ":J" + incRowExcel].Style.Numberformat.Format = "#,##0.0000";
                                            incRowExcel = incRowExcel + 2;

                                            incRowExcel++;

                                            incRowExcel = incRowExcel + 4;

                                            worksheet.Cells[incRowExcel, 1].Value = "Disclaimer : ";
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Merge = true;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Size = 13;

                                            incRowExcel++;

                                            worksheet.Cells[incRowExcel, 1].Value = "Dokumen ini dipersiapkan oleh PT SUCOR SEKURITAS dan hanya bisa digunakan untuk kepentingan investor tersebut diatas dan tidak untuk pihak lainnya. Laporan ini bukan merupakan konfirmasi dari PT SUCOR SEKURITAS dan tidak untuk menggantikan laporan yang wajib diterbitkan oleh Bank Kustodian, jika ada perbedaan antara laporan ini dengan laporan Bank Kustodian, maka laporan Bank Kustodian adalah yang benar. Laporan ini diproses oleh computer dan tidak memerlukan tandatangan.";
                                            worksheet.Cells[incRowExcel, 1, incRowExcel + 2, 11].Style.Font.Size = 12;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel + 2, 11].Merge = true;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel + 2, 11].Style.WrapText = true;

                                            incRowExcel = incRowExcel + 4;

                                            worksheet.Cells[incRowExcel, 1].Value = "PT SUCOR SEKURITAS terdaftar dan diawasi oleh OJK, dengan nomor registrasi KEP-01/PM/MI/1999";
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 11].Style.Font.Size = 12;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 11].Merge = true;

                                            incRowExcel = incRowExcel + 2;

                                            //worksheet.Cells[incRowExcel, 1].Value = "PT SUCOR SEKURITAS";
                                            //worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Merge = true;
                                            //worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Bold = true;
                                            //worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Size = 13;

                                            //incRowExcel++;

                                            //worksheet.Cells[incRowExcel, 1].Value = "Sahid Sudirman Center, 12th Fl.";
                                            //worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Merge = true;
                                            //worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Size = 12;

                                            //incRowExcel++;

                                            //worksheet.Cells[incRowExcel, 1].Value = "Jl. Jenderal Sudirman Kav. 86, Jakarta 10220 - Indonesia";
                                            //worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Merge = true;
                                            //worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Size = 12;

                                            //incRowExcel++;

                                            //worksheet.Cells[incRowExcel, 1].Value = "p. +62-21  8067 3000, f.  +62-21  2788 9288 |sucorsekuritas.com";
                                            //worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Merge = true;
                                            //worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Size = 12;

                                            worksheet.Row(incRowExcel).PageBreak = false;

                                        }
                                        //incRowExcel++;
                                        int _lastRow = incRowExcel;

                                        string _rangeA = "A5:K" + incRowExcel;
                                        using (ExcelRange r = worksheet.Cells[_rangeA]) // KALO  KOLOM 1 SAMPE 9 A-I
                                        {
                                            r.Style.Font.Size = 28;
                                        }

                                        worksheet.DeleteRow(_lastRow);


                                        worksheet.PrinterSettings.FitToPage = true;
                                        worksheet.PrinterSettings.FitToWidth = 1;
                                        worksheet.PrinterSettings.FitToHeight = 0;
                                        worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel - 1, 12];
                                        worksheet.Column(1).Width = 20;
                                        worksheet.Column(2).Width = 35;
                                        worksheet.Column(3).Width = 2;
                                        worksheet.Column(4).Width = 100;
                                        worksheet.Column(5).Width = 50;
                                        worksheet.Column(6).Width = 50;
                                        worksheet.Column(7).Width = 50;
                                        worksheet.Column(8).Width = 50;
                                        worksheet.Column(9).Width = 50;
                                        worksheet.Column(10).Width = 2;
                                        worksheet.Column(11).Width = 50;

                                        // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                        //worksheet.Cells["A3:I14"].AutoFitColumns(); // UNTUK AUTO FIT UKURAN TULISAN DENGAN KOLOM, KEBETULAN INI BUAT HEADER AJA DIPAKENYA
                                        //worksheetApproved.Cells["A3:E3"].AutoFitColumns();
                                        worksheet.PrinterSettings.Orientation = eOrientation.Landscape; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                        worksheet.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN

                                        //worksheet.HeaderFooter.OddHeader.CenteredText = "\n \n \n \n \n \n \n &30&B SUBSCRIPTION \n &28&B Batch Form";



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

            #region Customer Portfolio
            else if (_unitRegistryRpt.ReportName.Equals("Customer Portfolio"))
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
                            string _status = "";

                            if (!_host.findString(_unitRegistryRpt.FundFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_unitRegistryRpt.FundFrom))
                            {
                                _paramFund = "And B.FundPK in ( " + _unitRegistryRpt.FundFrom + " ) ";
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
                                _status = " and  A.Status = 2 and A.Posted = 1 and A.Revised = 0 ";
                                //_statusRedemp = "  CR.Status = 2 and CR.Posted = 1 and CR.Revised = 0 ";
                            }
                            else if (_unitRegistryRpt.Status == 2)
                            {
                                _status = " and A.Status = 2 and A.Posted = 1 and A.Revised = 1 ";
                                //_statusRedemp = "  CR.Status = 2 and CR.Posted = 1 and CR.Revised = 1 ";
                            }
                            else if (_unitRegistryRpt.Status == 3)
                            {
                                _status = " and A.Status = 2 and A.Posted = 0 and A.Revised = 0 ";
                                //_statusRedemp = "  CR.Status = 2 and CR.Posted = 0 and CR.Revised = 0 ";
                            }
                            else if (_unitRegistryRpt.Status == 4)
                            {
                                _status = " and A.Status = 1  ";
                                //_statusRedemp = "  CR.Status = 1  ";
                            }
                            else if (_unitRegistryRpt.Status == 5)
                            {
                                _status = " and A.Status = 3  ";
                                //_statusRedemp = "  CR.Status = 3  ";
                            }
                            else if (_unitRegistryRpt.Status == 6)
                            {
                                _status = " and (A.Status = 2 or A.Posted = 1) and A.Revised = 0  ";
                                //_statusRedemp = "  (CR.Status = 2 or CR.Posted = 1) and CR.Revised = 0  ";
                            }
                            else if (_unitRegistryRpt.Status == 7)
                            {
                                _status = " and (A.Status = 1 Or A.Status = 2 or A.Posted = 1) and  A.Revised = 0  ";
                                //_statusRedemp = "  (CR.Status = 1 Or CR.Status = 2 or CR.Posted = 1) and CR.Revised = 0  ";
                            }


                            cmd.CommandText =

                            @" 
                            select * from (

                            Select C.ID,C.Name,
                            Case when C.InvestorType = 1 then C.AlamatInd1 else C.AlamatPerusahaan end Address,
                            Case when C.InvestorType = 1 then c.TeleponSelular + '-' + C.TeleponBisnis else C.PhoneIns1 + '-' + C.TeleponBisnis end Phone, 
                            B.Name FundName,B.FundPK FundPK, 'Subscribe' Description,A.ValueDate,A.TotalCashAmount,A.NAV,A.TotalUnitAmount,
                            [dbo].[Get_UnitAmountByFundPKandFundClientPK](ValueDate,A.FundClientPK,A.FundPK) balance
                            from ClientSubscription A
                            left join Fund B on A.FundPK = B.FundPK and B.status = 2
                            left join FundClient C on A.FundClientPK = C.FundClientPK and C.status = 2
                            where ValueDate between @DateFrom and @DateTo 
                            " + _paramFund + _paramFundClient + _status +
                            @"
                            UNION ALL

                            Select C.ID,C.Name,
                            Case when C.InvestorType = 1 then C.AlamatInd1 else C.AlamatPerusahaan end Address,
                            Case when C.InvestorType = 1 then c.TeleponSelular + '-' + C.TeleponBisnis else C.PhoneIns1 + '-' + C.TeleponBisnis end Phone, 
                            B.Name FundName,B.FundPK FundPK, 'Redemption' Description,A.ValueDate,A.TotalCashAmount,A.NAV,A.TotalUnitAmount,
                            [dbo].[Get_UnitAmountByFundPKandFundClientPK](ValueDate,A.FundClientPK,A.FundPK) balance
                            from ClientRedemption A
                            left join Fund B on A.FundPK = B.FundPK and B.status = 2
                            left join FundClient C on A.FundClientPK = C.FundClientPK and C.status = 2
                            where ValueDate between @DateFrom and @DateTo 
                            " + _paramFund + _paramFundClient + _status +
                            @"
                            UNION ALL

                            Select C.ID,C.Name,
                            Case when C.InvestorType = 1 then C.AlamatInd1 else C.AlamatPerusahaan end Address,
                            Case when C.InvestorType = 1 then c.TeleponSelular + '-' + C.TeleponBisnis else C.PhoneIns1 + '-' + C.TeleponBisnis end Phone, 
                            B.Name FundName,B.FundPK FundPK, 'Switching OUT' Description,A.ValueDate,A.TotalCashAmountFundFrom,A.NAVFundTo,A.TotalUnitAmountFundFrom,
                            [dbo].[Get_UnitAmountByFundPKandFundClientPK](ValueDate,A.FundClientPK,A.FundPKFrom) balance
                            from ClientSwitching A
                            left join Fund B on A.FundPKFrom = B.FundPK and B.status = 2
                            left join FundClient C on A.FundClientPK = C.FundClientPK and C.status = 2
                            where ValueDate between @DateFrom and @DateTo   
                            " + _paramFund + _paramFundClient + _status +
                                @"
                            UNION ALL

                            Select C.ID,C.Name,
                            Case when C.InvestorType = 1 then C.AlamatInd1 else C.AlamatPerusahaan end Address,
                            Case when C.InvestorType = 1 then c.TeleponSelular + '-' + C.TeleponBisnis else C.PhoneIns1 + '-' + C.TeleponBisnis end Phone, 
                            B.Name FundName,B.FundPK FundPK, 'Switching IN' Description,A.ValueDate,A.TotalCashAmountFundTo,A.NAVFundTo,A.TotalUnitAmountFundTo,
                            [dbo].[Get_UnitAmountByFundPKandFundClientPK](ValueDate,A.FundClientPK,A.FundPKTo) balance
                            from ClientSwitching A
                            left join Fund B on A.FundPKTo = B.FundPK and B.status = 2
                            left join FundClient C on A.FundClientPK = C.FundClientPK and C.status = 2
                            where ValueDate between @DateFrom and @DateTo    
                            " + _paramFund + _paramFundClient + _status +
                            @"
                            )A 
                            order by A.FundName,A.FundPK,ValueDate,name asc
                            ";

                            cmd.CommandTimeout = 0;
                            cmd.Parameters.AddWithValue("@DateFrom", _unitRegistryRpt.ValueDateFrom);
                            cmd.Parameters.AddWithValue("@DateTo", _unitRegistryRpt.ValueDateTo);
                            cmd.Parameters.AddWithValue("@FundFrom", _unitRegistryRpt.FundFrom);
                            cmd.Parameters.AddWithValue("@FundClientFrom", _unitRegistryRpt.FundClientFrom);


                            using (SqlDataReader dr0 = cmd.ExecuteReader())
                            {
                                if (!dr0.HasRows)
                                {
                                    return false;
                                }
                                else
                                {
                                    string filePath = Tools.ReportsPath + "CustomerPortfolio" + "_" + _userID + ".xlsx";
                                    string pdfPath = Tools.ReportsPath + "CustomerPortfolio" + "_" + _userID + ".pdf";
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

                                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Customer Portfolio");


                                        //ATUR DATA GROUPINGNYA DULU
                                        List<UnitRegistryRpt> rList = new List<UnitRegistryRpt>();
                                        while (dr0.Read())
                                        {
                                            UnitRegistryRpt rSingle = new UnitRegistryRpt();
                                            rSingle.ClientID = Convert.ToString(dr0["ID"]);
                                            rSingle.ClientName = Convert.ToString(dr0["Name"]);
                                            rSingle.CIF = Convert.ToString(dr0["Address"]);
                                            rSingle.CurrencyID = Convert.ToString(dr0["Phone"]);
                                            rSingle.FundPK = Convert.ToInt32(dr0["FundPK"]);
                                            rSingle.FundName = Convert.ToString(dr0["FundName"]);
                                            rSingle.Description = Convert.ToString(dr0["Description"]);
                                            rSingle.ValueDate = Convert.ToDateTime(dr0["ValueDate"]);
                                            rSingle.CashBalance = Convert.ToDecimal(dr0["TotalCashAmount"]);
                                            rSingle.Nav = Convert.ToDecimal(dr0["Nav"]);
                                            rSingle.UnitBalance = Convert.ToDecimal(dr0["TotalUnitAmount"]);
                                            rSingle.Charge = Convert.ToDecimal(dr0["Balance"]);
                                            rList.Add(rSingle);

                                        }


                                        var QueryByFundID =
                                            from r in rList
                                            group r by new { r.ClientID, r.ClientName, r.CIF, r.CurrencyID, r.FundName, r.FundPK } into rGroup
                                            select rGroup;


                                        int incRowExcel = 1;


                                        //incRowExcel++;

                                        //incRowExcel = incRowExcel + 2;

                                        //Row A = 2
                                        int RowA = incRowExcel;

                                        incRowExcel = incRowExcel + 2;



                                        foreach (var rsHeader in QueryByFundID)
                                        {

                                            int RowZ = incRowExcel;
                                            incRowExcel++;


                                            worksheet.Cells[incRowExcel, 1].Value = _host.Get_CompanyName();
                                            worksheet.Cells["A" + incRowExcel + ":E" + incRowExcel].Merge = true;
                                            worksheet.Cells["A" + incRowExcel + ":E" + incRowExcel].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Size = 18;
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Value = "CUSTOMER PORTFOLIO";
                                            worksheet.Cells["A" + incRowExcel + ":E" + incRowExcel].Merge = true;
                                            worksheet.Cells["A" + incRowExcel + ":E" + incRowExcel].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Size = 16;

                                            incRowExcel++;
                                            //worksheet.Cells[incRowExcel, 1].Value = "As Of : " + _unitRegistryRpt.ValueDateTo;
                                            //worksheet.Cells["A" + incRowExcel + ":E" + incRowExcel].Merge = true;
                                            //worksheet.Cells[incRowExcel, 1].Style.Font.Size = 16;
                                            incRowExcel = incRowExcel + 1;

                                            worksheet.Cells[incRowExcel, 1].Value = "A/C No : ";
                                            worksheet.Cells["A" + incRowExcel + ":A" + incRowExcel].Style.Font.Bold = true;
                                            worksheet.Cells["A" + incRowExcel + ":A" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 2].Value = rsHeader.Key.ClientID;
                                            worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                            worksheet.Cells["B" + incRowExcel + ":D" + incRowExcel].Merge = true;
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Value = "Name : ";
                                            worksheet.Cells["A" + incRowExcel + ":A" + incRowExcel].Style.Font.Bold = true;
                                            worksheet.Cells["A" + incRowExcel + ":A" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 2].Value = rsHeader.Key.ClientName;
                                            worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                            worksheet.Cells["B" + incRowExcel + ":D" + incRowExcel].Merge = true;
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Value = "Address : ";
                                            worksheet.Cells["A" + incRowExcel + ":A" + incRowExcel].Style.Font.Bold = true;
                                            worksheet.Cells["A" + incRowExcel + ":A" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 2].Value = rsHeader.Key.CIF;
                                            worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                            worksheet.Cells["B" + incRowExcel + ":D" + incRowExcel].Merge = true;
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Value = "Phone : ";
                                            worksheet.Cells["A" + incRowExcel + ":A" + incRowExcel].Style.Font.Bold = true;
                                            worksheet.Cells["A" + incRowExcel + ":A" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 2].Value = rsHeader.Key.CurrencyID;
                                            worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                            worksheet.Cells["B" + incRowExcel + ":D" + incRowExcel].Merge = true;




                                            incRowExcel = incRowExcel + 2;
                                            //Row B = 3
                                            int RowBZ = incRowExcel;
                                            int RowGZ = incRowExcel + 1;

                                            worksheet.Row(incRowExcel).Height = 25;
                                            worksheet.Cells[incRowExcel, 1].Value = "No";
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells["A" + RowBZ + ":A" + RowGZ].Merge = true;
                                            worksheet.Cells["A" + RowBZ + ":A" + RowGZ].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["A" + RowBZ + ":A" + RowGZ].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 2].Value = "Fund";
                                            worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                            worksheet.Cells["B" + RowBZ + ":C" + RowGZ].Merge = true;
                                            worksheet.Cells["B" + RowBZ + ":C" + RowGZ].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["B" + RowBZ + ":C" + RowGZ].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 4].Value = "Description";
                                            worksheet.Cells[incRowExcel, 4].Style.Font.Bold = true;
                                            worksheet.Cells["D" + RowBZ + ":D" + RowGZ].Merge = true;
                                            worksheet.Cells["D" + RowBZ + ":D" + RowGZ].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["D" + RowBZ + ":D" + RowGZ].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 5].Value = "Trans. Date";
                                            worksheet.Cells[incRowExcel, 5].Style.Font.Bold = true;
                                            worksheet.Cells["E" + RowBZ + ":E" + RowGZ].Merge = true;
                                            worksheet.Cells["E" + RowBZ + ":E" + RowGZ].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["E" + RowBZ + ":E" + RowGZ].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 6].Value = "Net Amount";
                                            worksheet.Cells[incRowExcel, 6].Style.Font.Bold = true;
                                            worksheet.Cells["F" + RowBZ + ":F" + RowGZ].Merge = true;
                                            worksheet.Cells["F" + RowBZ + ":F" + RowGZ].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["F" + RowBZ + ":F" + RowGZ].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 7].Value = "Nav";
                                            worksheet.Cells[incRowExcel, 7].Style.Font.Bold = true;
                                            worksheet.Cells["G" + RowBZ + ":G" + RowGZ].Merge = true;
                                            worksheet.Cells["G" + RowBZ + ":G" + RowGZ].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["G" + RowBZ + ":G" + RowGZ].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 8].Value = "Unit";
                                            worksheet.Cells[incRowExcel, 8].Style.Font.Bold = true;
                                            worksheet.Cells["H" + RowBZ + ":H" + RowGZ].Merge = true;
                                            worksheet.Cells["H" + RowBZ + ":H" + RowGZ].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["H" + RowBZ + ":H" + RowGZ].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 9].Value = "Balance";
                                            worksheet.Cells[incRowExcel, 9].Style.Font.Bold = true;
                                            worksheet.Cells["I" + RowBZ + ":J" + RowGZ].Merge = true;
                                            worksheet.Cells["I" + RowBZ + ":J" + RowGZ].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["I" + RowBZ + ":J" + RowGZ].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;


                                            incRowExcel++;

                                            // Row C = 4
                                            int RowC = incRowExcel;

                                            incRowExcel++;
                                            //area header

                                            int _no = 1;
                                            int _startRowDetail = incRowExcel;
                                            int _endRowDetail = 0;
                                            foreach (var rsDetail in rsHeader)
                                            {
                                                //Row D = 5
                                                int RowD = incRowExcel;
                                                int RowE = incRowExcel + 1;


                                                //ThickBox Border

                                                worksheet.Cells["A" + RowBZ + ":J" + RowGZ].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + RowBZ + ":J" + RowGZ].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                                //area detail
                                                worksheet.Cells[incRowExcel, 1].Value = _no;
                                                worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 2].Value = rsDetail.FundName;
                                                worksheet.Cells[incRowExcel, 4].Value = rsDetail.Description;

                                                worksheet.Cells[incRowExcel, 5].Value = rsDetail.ValueDate;
                                                worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "dd/MMM/yyyy";
                                                worksheet.Cells[incRowExcel, 6].Value = rsDetail.CashBalance;
                                                worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 7].Value = rsDetail.Nav;
                                                worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0.0000";
                                                worksheet.Cells[incRowExcel, 8].Value = rsDetail.UnitBalance;
                                                worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0.0000";
                                                worksheet.Cells[incRowExcel, 9].Value = rsDetail.Charge;
                                                worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells["I" + incRowExcel + ":J" + incRowExcel].Merge = true;
                                                worksheet.Row(incRowExcel).Height = 15;

                                                _endRowDetail = incRowExcel;
                                                _no++;
                                                incRowExcel++;

                                            }

                                            worksheet.Cells["A" + RowBZ].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + RowBZ + ":A" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + RowBZ + ":A" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells["B" + RowBZ].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["B" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells["C" + RowBZ].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["C" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells["D" + RowBZ].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["D" + RowBZ + ":D" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["D" + RowBZ + ":D" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["D" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells["E" + RowBZ].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["E" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells["F" + RowBZ].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["F" + RowBZ + ":F" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["F" + RowBZ + ":F" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["F" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells["G" + RowBZ].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["G" + RowBZ + ":G" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["G" + RowBZ + ":G" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["G" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells["H" + RowBZ].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["H" + RowBZ + ":H" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["H" + RowBZ + ":H" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["H" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells["I" + RowBZ].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["I" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells["J" + RowBZ].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["J" + RowBZ + ":J" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["J" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            incRowExcel = incRowExcel + 2;

                                            incRowExcel++;

                                            incRowExcel = incRowExcel + 4;

                                            worksheet.Cells[incRowExcel, 1].Value = "Disclaimer : ";
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Merge = true;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Size = 13;

                                            incRowExcel++;

                                            worksheet.Cells[incRowExcel, 1].Value = "Dokumen ini dipersiapkan oleh PT SUCOR SEKURITAS dan hanya bisa digunakan untuk kepentingan investor tersebut diatas dan tidak untuk pihak lainnya. Laporan ini bukan merupakan konfirmasi dari PT SUCOR SEKURITAS dan tidak untuk menggantikan laporan yang wajib diterbitkan oleh Bank Kustodian, jika ada perbedaan antara laporan ini dengan laporan Bank Kustodian, maka laporan Bank Kustodian adalah yang benar. Laporan ini diproses oleh computer dan tidak memerlukan tandatangan.";
                                            worksheet.Cells[incRowExcel, 1, incRowExcel + 2, 10].Style.Font.Size = 12;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel + 2, 10].Merge = true;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel + 2, 10].Style.WrapText = true;

                                            incRowExcel = incRowExcel + 4;

                                            worksheet.Cells[incRowExcel, 1].Value = "PT SUCOR SEKURITAS terdaftar dan diawasi oleh OJK, dengan nomor registrasi KEP-01/PM/MI/1999";
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 10].Style.Font.Size = 12;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 10].Merge = true;

                                            incRowExcel = incRowExcel + 2;

                                            //worksheet.Cells[incRowExcel, 1].Value = "PT SUCOR SEKURITAS";
                                            //worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Merge = true;
                                            //worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Bold = true;
                                            //worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Size = 13;

                                            //incRowExcel++;

                                            //worksheet.Cells[incRowExcel, 1].Value = "Sahid Sudirman Center, 12th Fl.";
                                            //worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Merge = true;
                                            //worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Size = 12;

                                            //incRowExcel++;

                                            //worksheet.Cells[incRowExcel, 1].Value = "Jl. Jenderal Sudirman Kav. 86, Jakarta 10220 - Indonesia";
                                            //worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Merge = true;
                                            //worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Size = 12;

                                            //incRowExcel++;

                                            //worksheet.Cells[incRowExcel, 1].Value = "p. +62-21  8067 3000, f.  +62-21  2788 9288 |sucorsekuritas.com";
                                            //worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Merge = true;
                                            //worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Size = 12;


                                            worksheet.Row(incRowExcel).PageBreak = _unitRegistryRpt.PageBreak;

                                        }
                                        //incRowExcel++;
                                        int _lastRow = incRowExcel;

                                        string _rangeA = "A5:J" + incRowExcel;
                                        using (ExcelRange r = worksheet.Cells[_rangeA]) // KALO  KOLOM 1 SAMPE 9 A-I
                                        {
                                            r.Style.Font.Size = 12;
                                        }

                                        worksheet.DeleteRow(_lastRow);


                                        worksheet.PrinterSettings.FitToPage = true;
                                        worksheet.PrinterSettings.FitToWidth = 1;
                                        worksheet.PrinterSettings.FitToHeight = 0;
                                        worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel - 1, 10];
                                        worksheet.Column(1).Width = 10;
                                        worksheet.Column(2).Width = 45;
                                        worksheet.Column(3).Width = 5;
                                        worksheet.Column(4).Width = 30;
                                        worksheet.Column(5).Width = 30;
                                        worksheet.Column(6).Width = 30;
                                        worksheet.Column(7).Width = 30;
                                        worksheet.Column(8).Width = 30;
                                        worksheet.Column(9).Width = 30;
                                        worksheet.Column(10).Width = 5;


                                        // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                        //worksheet.Cells["A3:I14"].AutoFitColumns(); // UNTUK AUTO FIT UKURAN TULISAN DENGAN KOLOM, KEBETULAN INI BUAT HEADER AJA DIPAKENYA
                                        //worksheetApproved.Cells["A3:E3"].AutoFitColumns();
                                        worksheet.PrinterSettings.Orientation = eOrientation.Landscape; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                        worksheet.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN

                                        //worksheet.HeaderFooter.OddHeader.CenteredText = "\n \n \n \n \n \n \n &30&B SUBSCRIPTION \n &28&B Batch Form";



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

            #region Rekap Management Fee Individu & Institusi
            else if (_unitRegistryRpt.ReportName.Equals("Rekap Management Fee Individu & Institusi"))
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
                                //_paramAgent = "And (E.ID  =  left(@AgentFrom,charindex('-',@AgentFrom) - 1) or F.ID = left(@AgentFrom,charindex('-',@AgentFrom) - 1)) ";
                                _paramAgent = "And A.AgentPK in ( " + _unitRegistryRpt.AgentFrom + " ) ";
                            }
                            else
                            {
                                _paramAgent = "";
                            }


                            cmd.CommandText =

                            @" 
                            select B.ID FundID,isnull(C.NAV,0) NAV,isnull(D.ID,'') + ' ' + D.Name ClientName,
                            E.Name Referal, F.Name Cabang,
                            A.UnitAmount,isnull(C.NAV * A.UnitAmount,0) AUM, isnull(G.ManagementFee,0) managementFee,
                            Case when D.InvestorType = 1 then 'INDIVIDU' else 'INSTITUSI' end InvestorType
                            from FundClientPosition A
                            left join Fund B on A.FundPK = B.FundPK and B.status = 2
                            left join closeNAV C on A.FundPK = C.FundPK and C.status = 2 and C.Date = @DateTo
                            left join FundClient D on A.FundClientPK = D.FundClientPK and D.Status = 2
                            left join Agent E on D.SellingAgentPK = E.AgentPK and E.Status  = 2 
                            left join Agent F on E.ParentPK = F.AgentPK  and F.Status = 2
                            left join 
                            (
	                        Select A.FundClientPK, A.FundPK,
	                        sum( case when dbo.CheckIsTommorowHoliday(A.Date) = 1 
                            then A.UnitAmount * isnull(B.Nav,0) * isnull(C.ManagementFeePercent,0) / 100 / 365 * dbo.CheckTotalSeriesHoliday(A.Date)
                            else A.UnitAmount * isnull(B.Nav,0) * isnull(C.ManagementFeePercent,0) / 100 / 365 end
                            ) ManagementFee  from 
	                        FundClientPosition A left join CloseNAV B on A.FundPK = B.FundPK and B.Status = 2 and A.Date = B.Date
	                        left join Fund C on A.FundPK = C.FundPK and C.status = 2
	                        where A.Date between @Datefrom and @DateTo 
	                        and B.Date between @Datefrom and @DateTo 
	                        group By A.FundClientPK,A.FundPK
                            )G on A.FundClientPK = G.FundClientPK and A.FundPK = G.FundPK 
                            where A.Date = @DateTo

                            " + _paramFund + _paramFundClient + _paramAgent;

                            cmd.CommandTimeout = 0;
                            cmd.Parameters.AddWithValue("@DateFrom", _unitRegistryRpt.ValueDateFrom);
                            cmd.Parameters.AddWithValue("@DateTo", _unitRegistryRpt.ValueDateTo);
                            cmd.Parameters.AddWithValue("@FundFrom", _unitRegistryRpt.FundFrom);
                            cmd.Parameters.AddWithValue("@FundClientFrom", _unitRegistryRpt.FundClientFrom);
                            cmd.Parameters.AddWithValue("@AgentFrom", _unitRegistryRpt.AgentFrom);



                            using (SqlDataReader dr0 = cmd.ExecuteReader())
                            {
                                if (!dr0.HasRows)
                                {
                                    return false;
                                }
                                else
                                {
                                    string filePath = Tools.ReportsPath + "RekapManagementFeeIndividuInstitusi" + "_" + _userID + ".xlsx";
                                    string pdfPath = Tools.ReportsPath + "RekapManagementFeeIndividuInstitusi" + "_" + _userID + ".pdf";
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

                                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Rekap Management Fee");


                                        //ATUR DATA GROUPINGNYA DULU
                                        List<RekapManagementFeeIndividuInstitusi> rList = new List<RekapManagementFeeIndividuInstitusi>();
                                        while (dr0.Read())
                                        {
                                            RekapManagementFeeIndividuInstitusi rSingle = new RekapManagementFeeIndividuInstitusi();
                                            rSingle.FundID = Convert.ToString(dr0["FundID"]);
                                            rSingle.NAV = Convert.ToDecimal(dr0["Nav"]);
                                            rSingle.ClientName = Convert.ToString(dr0["ClientName"]);
                                            rSingle.Referal = Convert.ToString(dr0["Referal"]);
                                            rSingle.Cabang = Convert.ToString(dr0["Cabang"]);
                                            rSingle.UnitAmount = Convert.ToDecimal(dr0["UnitAmount"]);
                                            rSingle.AUM = Convert.ToDecimal(dr0["AUM"]);
                                            rSingle.ManagementFee = Convert.ToDecimal(dr0["ManagementFee"]);
                                            rSingle.InvestorType = Convert.ToString(dr0["InvestorType"]);
                                            rList.Add(rSingle);
                                        }


                                        var QueryByFundID =
                                            from r in rList
                                            group r by new { r.InvestorType } into rGroup
                                            select rGroup;

                                        int incRowExcel = 1;



                                        int RowA = incRowExcel;

                                        int _lastRow = 0;

                                        foreach (var rsHeader in QueryByFundID)
                                        {

                                            int RowZ = incRowExcel;
                                            incRowExcel++;

                                            worksheet.Cells[incRowExcel, 1].Value = rsHeader.Key.InvestorType;

                                            incRowExcel = incRowExcel + 2;
                                            //Row B = 3
                                            int RowBZ = incRowExcel;
                                            int RowGZ = incRowExcel;


                                            worksheet.Cells[incRowExcel, 1].Value = "No";
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells["A" + RowBZ + ":A" + RowGZ].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["A" + RowBZ + ":A" + RowGZ].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 2].Value = "Customer";
                                            worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                            worksheet.Cells["B" + RowBZ + ":C" + RowGZ].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["B" + RowBZ + ":C" + RowGZ].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 4].Value = "Reksadana";
                                            worksheet.Cells[incRowExcel, 4].Style.Font.Bold = true;
                                            worksheet.Cells["D" + RowBZ + ":D" + RowGZ].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["D" + RowBZ + ":D" + RowGZ].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 5].Value = "Cabang";
                                            worksheet.Cells[incRowExcel, 5].Style.Font.Bold = true;
                                            worksheet.Cells["E" + RowBZ + ":E" + RowGZ].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["E" + RowBZ + ":E" + RowGZ].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 6].Value = "Referral";
                                            worksheet.Cells[incRowExcel, 6].Style.Font.Bold = true;
                                            worksheet.Cells["F" + RowBZ + ":F" + RowGZ].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["F" + RowBZ + ":F" + RowGZ].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 7].Value = "AUM";
                                            worksheet.Cells[incRowExcel, 7].Style.Font.Bold = true;
                                            worksheet.Cells["G" + RowBZ + ":G" + RowGZ].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["G" + RowBZ + ":G" + RowGZ].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 8].Value = "Management Fee";
                                            worksheet.Cells[incRowExcel, 8].Style.Font.Bold = true;
                                            worksheet.Cells["H" + RowBZ + ":H" + RowGZ].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["H" + RowBZ + ":H" + RowGZ].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            // Row C = 4
                                            int RowC = incRowExcel;

                                            incRowExcel++;
                                            //area header

                                            int _no = 1;
                                            int _startRowDetail = incRowExcel;
                                            int _endRowDetail = 0;
                                            foreach (var rsDetail in rsHeader)
                                            {
                                                //Row D = 5
                                                int RowD = incRowExcel;
                                                int RowE = incRowExcel + 1;


                                                //ThickBox Border

                                                worksheet.Cells["A" + RowBZ + ":H" + RowBZ].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + RowBZ + ":H" + RowBZ].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                                //area detail
                                                worksheet.Cells[incRowExcel, 1].Value = _no;
                                                worksheet.Cells["A" + incRowExcel + ":A" + incRowExcel].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells["A" + incRowExcel + ":A" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 2].Value = rsDetail.ClientName;
                                                worksheet.Cells[incRowExcel, 4].Value = rsDetail.FundID;

                                                worksheet.Cells[incRowExcel, 5].Value = rsDetail.Cabang;
                                                worksheet.Cells[incRowExcel, 6].Value = rsDetail.Referal;
                                                worksheet.Cells[incRowExcel, 7].Value = rsDetail.AUM;
                                                worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 8].Value = rsDetail.ManagementFee;
                                                worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0.0000";

                                                _endRowDetail = incRowExcel;
                                                _no++;
                                                incRowExcel++;

                                            }
                                            //wiwi

                                            worksheet.Cells["A" + RowBZ].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + RowBZ + ":A" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + RowBZ + ":A" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells["B" + RowBZ].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["B" + RowBZ + ":B" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["B" + RowBZ + ":B" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["B" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells["D" + RowBZ].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["D" + RowBZ + ":D" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["D" + RowBZ + ":D" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["D" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells["E" + RowBZ].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["E" + RowBZ + ":E" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["E" + RowBZ + ":E" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["E" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells["F" + RowBZ].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["F" + RowBZ + ":F" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["F" + RowBZ + ":F" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["F" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells["G" + RowBZ].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["G" + RowBZ + ":G" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["G" + RowBZ + ":G" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["G" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells["H" + RowBZ].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["H" + RowBZ + ":H" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["H" + RowBZ + ":H" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["H" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;


                                            _lastRow = incRowExcel;
                                            incRowExcel = incRowExcel + 2;

                                            incRowExcel = incRowExcel + 4;

                                            worksheet.Cells[incRowExcel, 1].Value = "Disclaimer : ";
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Merge = true;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Size = 13;

                                            incRowExcel++;

                                            worksheet.Cells[incRowExcel, 1].Value = "Dokumen ini dipersiapkan oleh PT SUCOR SEKURITAS dan hanya bisa digunakan untuk kepentingan investor tersebut diatas dan tidak untuk pihak lainnya. Laporan ini bukan merupakan konfirmasi dari PT SUCOR SEKURITAS dan tidak untuk menggantikan laporan yang wajib diterbitkan oleh Bank Kustodian, jika ada perbedaan antara laporan ini dengan laporan Bank Kustodian, maka laporan Bank Kustodian adalah yang benar. Laporan ini diproses oleh computer dan tidak memerlukan tandatangan.";
                                            worksheet.Cells[incRowExcel, 1, incRowExcel + 2, 8].Style.Font.Size = 12;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel + 2, 8].Merge = true;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel + 2, 8].Style.WrapText = true;

                                            incRowExcel = incRowExcel + 4;

                                            worksheet.Cells[incRowExcel, 1].Value = "PT SUCOR SEKURITAS terdaftar dan diawasi oleh OJK, dengan nomor registrasi KEP-01/PM/MI/1999";
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 8].Style.Font.Size = 12;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 8].Merge = true;

                                            incRowExcel = incRowExcel + 2;

                                            //worksheet.Cells[incRowExcel, 1].Value = "PT SUCOR SEKURITAS";
                                            //worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Merge = true;
                                            //worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Bold = true;
                                            //worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Size = 13;

                                            //incRowExcel++;

                                            //worksheet.Cells[incRowExcel, 1].Value = "Sahid Sudirman Center, 12th Fl.";
                                            //worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Merge = true;
                                            //worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Size = 12;

                                            //incRowExcel++;

                                            //worksheet.Cells[incRowExcel, 1].Value = "Jl. Jenderal Sudirman Kav. 86, Jakarta 10220 - Indonesia";
                                            //worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Merge = true;
                                            //worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Size = 12;

                                            //incRowExcel++;

                                            //worksheet.Cells[incRowExcel, 1].Value = "p. +62-21  8067 3000, f.  +62-21  2788 9288 |sucorsekuritas.com";
                                            //worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Merge = true;
                                            //worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Size = 12;

                                            incRowExcel++;



                                            worksheet.Row(incRowExcel).PageBreak = true;



                                        }
                                        //incRowExcel++;


                                        string _rangeA = "A2:H" + incRowExcel;
                                        using (ExcelRange r = worksheet.Cells[_rangeA]) // KALO  KOLOM 1 SAMPE 9 A-I
                                        {
                                            r.Style.Font.Size = 25;
                                        }

                                        worksheet.DeleteRow(_lastRow);


                                        worksheet.PrinterSettings.FitToPage = true;
                                        worksheet.PrinterSettings.FitToWidth = 1;
                                        worksheet.PrinterSettings.FitToHeight = 0;
                                        worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel - 1, 9];
                                        worksheet.Column(1).Width = 20;
                                        worksheet.Column(2).Width = 170;
                                        worksheet.Column(3).Width = 0;
                                        worksheet.Column(4).Width = 30;
                                        worksheet.Column(5).Width = 70;
                                        worksheet.Column(6).Width = 80;
                                        worksheet.Column(7).Width = 50;
                                        worksheet.Column(8).Width = 50;
                                        worksheet.Column(9).Width = 50;


                                        // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                        //worksheet.Cells["A3:I14"].AutoFitColumns(); // UNTUK AUTO FIT UKURAN TULISAN DENGAN KOLOM, KEBETULAN INI BUAT HEADER AJA DIPAKENYA
                                        //worksheetApproved.Cells["A3:E3"].AutoFitColumns();
                                        worksheet.PrinterSettings.Orientation = eOrientation.Landscape; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                        //worksheet.PrinterSettings.RepeatRows = new ExcelAddress("4:4");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN
                                        //worksheet.HeaderFooter.OddHeader.CenteredText = "\n \n \n \n \n \n \n &30&B SUBSCRIPTION \n &28&B Batch Form";



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

                                        worksheet.HeaderFooter.OddFooter.CenteredText = Tools.DefaultReportFooterCenterText();

                                        worksheet.HeaderFooter.OddHeader.LeftAlignedText =
                                            "&34" + _host.Get_CompanyName() + "\n" +
                                            "Rekap Management Fee \n" +
                                            "Period Date : " + _unitRegistryRpt.ValueDateFrom + " - " + _unitRegistryRpt.ValueDateTo;
                                        ;




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

            #region Rekap Management Fee Marketing
            else if (_unitRegistryRpt.ReportName.Equals("Rekap Management Fee Marketing"))
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
                                //_paramAgent = "And (E.ID  =  left(@AgentFrom,charindex('-',@AgentFrom) - 1) or F.ID = left(@AgentFrom,charindex('-',@AgentFrom) - 1)) ";
                                _paramAgent = "And A.AgentPK in ( " + _unitRegistryRpt.AgentFrom + " ) ";
                            }
                            else
                            {
                                _paramAgent = "";
                            }


                            cmd.CommandText =

                            @" 
                            select B.ID FundID,
                            E.Name Referal, F.Name Cabang,
                            sum(isnull(A.UnitAmount,0)) TotalUnit,sum(isnull(C.NAV * A.UnitAmount,0)) totalAUM, 
                            sum(isnull(G.managementFee,0)) totalManagementFee
                            from FundClientPosition A
                            left join Fund B on A.FundPK = B.FundPK and B.status = 2
                            left join closeNAV C on A.FundPK = C.FundPK and C.status = 2 and C.Date = @DateTo
                            left join FundClient D on A.FundClientPK = D.FundClientPK and D.Status = 2
                            left join Agent E on D.SellingAgentPK = E.AgentPK and E.Status  = 2 
                            left join Agent F on E.ParentPK = F.AgentPK  and F.Status = 2
                            left join 
                            (
	                        Select A.FundClientPK, A.FundPK,
	                        sum( case when dbo.CheckIsTommorowHoliday(A.Date) = 1 
                            then A.UnitAmount * isnull(B.Nav,0) * isnull(C.ManagementFeePercent,0) / 100 / 365 * dbo.CheckTotalSeriesHoliday(A.Date)
                            else A.UnitAmount * isnull(B.Nav,0) * isnull(C.ManagementFeePercent,0) / 100 / 365 end
                            ) ManagementFee  from 
	                        FundClientPosition A left join CloseNAV B on A.FundPK = B.FundPK and B.Status = 2 and A.Date = B.Date
	                        left join Fund C on A.FundPK = C.FundPK and C.status = 2
	                        where A.Date between @Datefrom and @DateTo 
	                        and B.Date between @Datefrom and @DateTo 
	                        group By A.FundClientPK,A.FundPK
                            )G on A.FundClientPK = G.FundClientPK and A.FundPK = G.FundPK
                            where A.Date = @DateTo and (D.SellingAgentPK <> 0 and D.SellingAgentPK is not null)
                            " + _paramFund + _paramFundClient + _paramAgent
                            + @" group by B.ID,E.Name,F.Name";

                            cmd.CommandTimeout = 0;
                            cmd.Parameters.AddWithValue("@DateFrom", _unitRegistryRpt.ValueDateFrom);
                            cmd.Parameters.AddWithValue("@DateTo", _unitRegistryRpt.ValueDateTo);
                            cmd.Parameters.AddWithValue("@FundFrom", _unitRegistryRpt.FundFrom);
                            cmd.Parameters.AddWithValue("@FundClientFrom", _unitRegistryRpt.FundClientFrom);
                            cmd.Parameters.AddWithValue("@AgentFrom", _unitRegistryRpt.AgentFrom);



                            using (SqlDataReader dr0 = cmd.ExecuteReader())
                            {
                                if (!dr0.HasRows)
                                {
                                    return false;
                                }
                                else
                                {
                                    string filePath = Tools.ReportsPath + "RekapManagementFeeMarketing" + "_" + _userID + ".xlsx";
                                    string pdfPath = Tools.ReportsPath + "RekapManagementFeeMarketing" + "_" + _userID + ".pdf";
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

                                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Rekap Management Fee");


                                        //ATUR DATA GROUPINGNYA DULU
                                        List<RekapManagementFeeMarketing> rList = new List<RekapManagementFeeMarketing>();
                                        while (dr0.Read())
                                        {
                                            RekapManagementFeeMarketing rSingle = new RekapManagementFeeMarketing();
                                            rSingle.FundID = Convert.ToString(dr0["FundID"]);
                                            rSingle.Referal = Convert.ToString(dr0["Referal"]);
                                            rSingle.Cabang = Convert.ToString(dr0["Cabang"]);
                                            rSingle.TotalUnit = Convert.ToDecimal(dr0["TotalUnit"]);
                                            rSingle.TotalAUM = Convert.ToDecimal(dr0["TotalAUM"]);
                                            rSingle.TotalManagementFee = Convert.ToDecimal(dr0["TotalManagementFee"]);
                                            rList.Add(rSingle);
                                        }


                                        var QueryByFundID =
                                            from r in rList
                                            group r by new { r.Cabang } into rGroup
                                            select rGroup;

                                        int incRowExcel = 1;

                                        int RowA = incRowExcel;


                                        foreach (var rsHeader in QueryByFundID)
                                        {

                                            int RowZ = incRowExcel;
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Value = rsHeader.Key.Cabang;

                                            incRowExcel = incRowExcel + 2;
                                            //Row B = 3
                                            int RowBZ = incRowExcel;
                                            int RowGZ = incRowExcel + 1;


                                            worksheet.Cells[incRowExcel, 1].Value = "No";
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells["A" + RowBZ + ":A" + RowBZ].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["A" + RowBZ + ":A" + RowBZ].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 2].Value = "Referral";
                                            worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                            worksheet.Cells["B" + RowBZ + ":C" + RowBZ].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["B" + RowBZ + ":C" + RowBZ].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 4].Value = "Reksadana";
                                            worksheet.Cells[incRowExcel, 4].Style.Font.Bold = true;
                                            worksheet.Cells["D" + RowBZ + ":D" + RowBZ].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["D" + RowBZ + ":D" + RowBZ].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 5].Value = "Total Unit";
                                            worksheet.Cells[incRowExcel, 5].Style.Font.Bold = true;
                                            worksheet.Cells["E" + RowBZ + ":E" + RowBZ].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["E" + RowBZ + ":E" + RowBZ].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 6].Value = "Total AUM";
                                            worksheet.Cells[incRowExcel, 6].Style.Font.Bold = true;
                                            worksheet.Cells["F" + RowBZ + ":F" + RowBZ].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["F" + RowBZ + ":F" + RowBZ].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 7].Value = "Management Fee";
                                            worksheet.Cells[incRowExcel, 7].Style.Font.Bold = true;
                                            worksheet.Cells["G" + RowBZ + ":G" + RowBZ].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["G" + RowBZ + ":G" + RowBZ].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 8].Value = "Company Fee(65%)";
                                            worksheet.Cells[incRowExcel, 8].Style.Font.Bold = true;
                                            worksheet.Cells["H" + RowBZ + ":H" + RowBZ].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["H" + RowBZ + ":H" + RowBZ].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 9].Value = "Referral Fee(35%)";
                                            worksheet.Cells[incRowExcel, 9].Style.Font.Bold = true;
                                            worksheet.Cells["I" + RowBZ + ":I" + RowBZ].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["I" + RowBZ + ":I" + RowBZ].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;


                                            //incRowExcel++;

                                            // Row C = 4
                                            int RowC = incRowExcel;

                                            incRowExcel++;
                                            //area header

                                            int _no = 1;
                                            int _startRowDetail = incRowExcel;
                                            int _endRowDetail = 0;
                                            foreach (var rsDetail in rsHeader)
                                            {
                                                //Row D = 5
                                                int RowD = incRowExcel;
                                                int RowE = incRowExcel + 1;


                                                //ThickBox Border

                                                worksheet.Cells["A" + RowBZ + ":I" + RowBZ].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + RowBZ + ":I" + RowBZ].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                                //area detail
                                                worksheet.Cells[incRowExcel, 1].Value = _no;
                                                worksheet.Cells["A" + incRowExcel + ":A" + incRowExcel].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells["A" + incRowExcel + ":A" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 2].Value = rsDetail.Referal;
                                                worksheet.Cells[incRowExcel, 4].Value = rsDetail.FundID;

                                                worksheet.Cells[incRowExcel, 5].Value = rsDetail.TotalUnit;
                                                worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.0000";
                                                worksheet.Cells[incRowExcel, 6].Value = rsDetail.TotalAUM;
                                                worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.0000";
                                                worksheet.Cells[incRowExcel, 7].Value = rsDetail.TotalManagementFee;
                                                worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0.0000";
                                                worksheet.Cells[incRowExcel, 8].Formula = "(G" + incRowExcel + "*0.65)";
                                                worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0.0000";
                                                worksheet.Cells[incRowExcel, 8].Calculate();
                                                worksheet.Cells[incRowExcel, 9].Formula = "(G" + incRowExcel + "*0.35)";
                                                worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0.0000";
                                                worksheet.Cells[incRowExcel, 9].Calculate();
                                                // disini


                                                _endRowDetail = incRowExcel;
                                                _no++;
                                                incRowExcel++;

                                            }

                                            worksheet.Cells["A" + RowBZ].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + RowBZ + ":A" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + RowBZ + ":A" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;


                                            worksheet.Cells["B" + RowBZ].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["B" + RowBZ + ":B" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["B" + RowBZ + ":B" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["B" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells["C" + RowBZ].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["C" + RowBZ + ":C" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["C" + RowBZ + ":C" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["C" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells["D" + RowBZ].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["D" + RowBZ + ":D" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["D" + RowBZ + ":D" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["D" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells["E" + RowBZ].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["E" + RowBZ + ":E" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["E" + RowBZ + ":E" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["E" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells["F" + RowBZ].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["F" + RowBZ + ":F" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["F" + RowBZ + ":F" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["F" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells["G" + RowBZ].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["G" + RowBZ + ":G" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["G" + RowBZ + ":G" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["G" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells["H" + RowBZ].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["H" + RowBZ + ":H" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["H" + RowBZ + ":H" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["H" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells["I" + RowBZ].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["I" + RowBZ + ":I" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["I" + RowBZ + ":I" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["I" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            incRowExcel = incRowExcel + 2;

                                            incRowExcel = incRowExcel + 4;

                                            worksheet.Cells[incRowExcel, 1].Value = "Disclaimer : ";
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Merge = true;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Size = 13;

                                            incRowExcel++;

                                            worksheet.Cells[incRowExcel, 1].Value = "Dokumen ini dipersiapkan oleh PT SUCOR SEKURITAS dan hanya bisa digunakan untuk kepentingan investor tersebut diatas dan tidak untuk pihak lainnya. Laporan ini bukan merupakan konfirmasi dari PT SUCOR SEKURITAS dan tidak untuk menggantikan laporan yang wajib diterbitkan oleh Bank Kustodian, jika ada perbedaan antara laporan ini dengan laporan Bank Kustodian, maka laporan Bank Kustodian adalah yang benar. Laporan ini diproses oleh computer dan tidak memerlukan tandatangan.";
                                            worksheet.Cells[incRowExcel, 1, incRowExcel + 2, 11].Style.Font.Size = 12;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel + 2, 11].Merge = true;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel + 2, 11].Style.WrapText = true;

                                            incRowExcel = incRowExcel + 4;

                                            worksheet.Cells[incRowExcel, 1].Value = "PT SUCOR SEKURITAS terdaftar dan diawasi oleh OJK, dengan nomor registrasi KEP-01/PM/MI/1999";
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 11].Style.Font.Size = 12;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 11].Merge = true;

                                            incRowExcel = incRowExcel + 2;

                                            //worksheet.Cells[incRowExcel, 1].Value = "PT SUCOR SEKURITAS";
                                            //worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Merge = true;
                                            //worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Bold = true;
                                            //worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Size = 13;

                                            //incRowExcel++;

                                            //worksheet.Cells[incRowExcel, 1].Value = "Sahid Sudirman Center, 12th Fl.";
                                            //worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Merge = true;
                                            //worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Size = 12;

                                            //incRowExcel++;

                                            //worksheet.Cells[incRowExcel, 1].Value = "Jl. Jenderal Sudirman Kav. 86, Jakarta 10220 - Indonesia";
                                            //worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Merge = true;
                                            //worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Size = 12;

                                            //incRowExcel++;

                                            //worksheet.Cells[incRowExcel, 1].Value = "p. +62-21  8067 3000, f.  +62-21  2788 9288 |sucorsekuritas.com";
                                            //worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Merge = true;
                                            //worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Size = 12;

                                            incRowExcel++;

                                            worksheet.Row(incRowExcel).PageBreak = true;

                                        }
                                        //incRowExcel++;
                                        int _lastRow = incRowExcel;

                                        string _rangeA = "A2:I" + incRowExcel;
                                        using (ExcelRange r = worksheet.Cells[_rangeA]) // KALO  KOLOM 1 SAMPE 9 A-I
                                        {
                                            r.Style.Font.Size = 28;
                                        }

                                        worksheet.DeleteRow(_lastRow);


                                        worksheet.PrinterSettings.FitToPage = true;
                                        worksheet.PrinterSettings.FitToWidth = 1;
                                        worksheet.PrinterSettings.FitToHeight = 0;
                                        worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel - 1, 11];
                                        worksheet.Column(1).Width = 20;
                                        worksheet.Column(2).Width = 100;
                                        worksheet.Column(3).Width = 0;
                                        worksheet.Column(4).Width = 30;
                                        worksheet.Column(5).Width = 70;
                                        worksheet.Column(6).Width = 80;
                                        worksheet.Column(7).Width = 70;
                                        worksheet.Column(8).Width = 50;
                                        worksheet.Column(9).Width = 50;
                                        worksheet.Column(10).Width = 2;
                                        worksheet.Column(11).Width = 50;

                                        // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                        //worksheet.Cells["A3:I14"].AutoFitColumns(); // UNTUK AUTO FIT UKURAN TULISAN DENGAN KOLOM, KEBETULAN INI BUAT HEADER AJA DIPAKENYA
                                        //worksheetApproved.Cells["A3:E3"].AutoFitColumns();
                                        worksheet.PrinterSettings.Orientation = eOrientation.Landscape; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                        //worksheet.PrinterSettings.RepeatRows = new ExcelAddress("11:11");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN
                                        //worksheet.HeaderFooter.OddHeader.CenteredText = "\n \n \n \n \n \n \n &30&B SUBSCRIPTION \n &28&B Batch Form";



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
                                        worksheet.HeaderFooter.OddFooter.LeftAlignedText = Tools.DefaultReportFooterLeftText();

                                        worksheet.HeaderFooter.OddHeader.LeftAlignedText =
                                            "&36" + _host.Get_CompanyName() + "\n" +
                                            "Rekap Management Fee - Marketing \n" +
                                            "Period Date : " + _unitRegistryRpt.ValueDateFrom + " - " + _unitRegistryRpt.ValueDateTo;
                                        ;



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

            #region Rekap Management Fee Fund
            else if (_unitRegistryRpt.ReportName.Equals("Rekap Management Fee Fund"))
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
                                //_paramAgent = "And (E.ID  =  left(@AgentFrom,charindex('-',@AgentFrom) - 1) or F.ID = left(@AgentFrom,charindex('-',@AgentFrom) - 1)) ";
                                _paramAgent = "And A.AgentPK in ( " + _unitRegistryRpt.AgentFrom + " ) ";
                            }
                            else
                            {
                                _paramAgent = "";
                            }


                            cmd.CommandText =

                                @" 
                                select B.ID FundID,case when D.InvestorType = 1 then 'Individual' else 'Institusi' end Type,Count(D.FundCLientPK) totalNasabah,
                                sum(isnull(A.UnitAmount,0)) TotalUnit,sum(isnull(C.NAV * A.UnitAmount,0)) totalAUM, sum(isnull(G.ManagementFee,0)) managementFee
                                from FundClientPosition A
                                left join Fund B on A.FundPK = B.FundPK and B.status = 2
                                left join closeNAV C on A.FundPK = C.FundPK and C.status = 2 and C.Date = @DateTo
                                left join FundClient D on A.FundClientPK = D.FundClientPK and D.Status = 2
                                left join Agent E on D.SellingAgentPK = E.AgentPK and E.Status  = 2 
                                left join Agent F on E.ParentPK = F.AgentPK  and F.Status = 2
                                left join
                                (
	                            Select A.FundClientPK, A.FundPK,
	                            sum( case when dbo.CheckIsTommorowHoliday(A.Date) = 1 
                                then A.UnitAmount * isnull(B.Nav,0) * isnull(C.ManagementFeePercent,0) / 100 / 365 * dbo.CheckTotalSeriesHoliday(A.Date)
                                else A.UnitAmount * isnull(B.Nav,0) * isnull(C.ManagementFeePercent,0) / 100 / 365 end
                                ) ManagementFee  from 
	                            FundClientPosition A left join CloseNAV B on A.FundPK = B.FundPK and B.Status = 2 and A.Date = B.Date
	                            left join Fund C on A.FundPK = C.FundPK and C.status = 2
	                            where A.Date between @Datefrom and @DateTo 
	                            and B.Date between @Datefrom and @DateTo 
	                            group By A.FundClientPK,A.FundPK
                                )G on A.FundClientPK = G.FundClientPK and A.FundPK = G.FundPK
                                where A.Date = @DateTo and A.UnitAmount > 0.00001
                               " + _paramFund + _paramFundClient + _paramAgent
                               + @" group by B.ID,D.InvestorType";

                            cmd.CommandTimeout = 0;
                            cmd.Parameters.AddWithValue("@DateFrom", _unitRegistryRpt.ValueDateFrom);
                            cmd.Parameters.AddWithValue("@DateTo", _unitRegistryRpt.ValueDateTo);
                            cmd.Parameters.AddWithValue("@FundFrom", _unitRegistryRpt.FundFrom);
                            cmd.Parameters.AddWithValue("@FundClientFrom", _unitRegistryRpt.FundClientFrom);
                            cmd.Parameters.AddWithValue("@AgentFrom", _unitRegistryRpt.AgentFrom);



                            using (SqlDataReader dr0 = cmd.ExecuteReader())
                            {
                                if (!dr0.HasRows)
                                {
                                    return false;
                                }
                                else
                                {
                                    string filePath = Tools.ReportsPath + "RekapManagementFeeFund" + "_" + _userID + ".xlsx";
                                    string pdfPath = Tools.ReportsPath + "RekapManagementFeeFund" + "_" + _userID + ".pdf";
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

                                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Rekap Management Fee");


                                        //ATUR DATA GROUPINGNYA DULU
                                        List<RekapManagementFeeFund> rList = new List<RekapManagementFeeFund>();
                                        while (dr0.Read())
                                        {
                                            RekapManagementFeeFund rSingle = new RekapManagementFeeFund();
                                            rSingle.FundID = Convert.ToString(dr0["FundID"]);
                                            rSingle.Type = Convert.ToString(dr0["Type"]);
                                            rSingle.TotalNasabah = Convert.ToInt32(dr0["TotalNasabah"]);
                                            rSingle.TotalUnit = Convert.ToDecimal(dr0["TotalUnit"]);
                                            rSingle.TotalAUM = Convert.ToDecimal(dr0["TotalAUM"]);
                                            rSingle.TotalManagementFee = Convert.ToDecimal(dr0["ManagementFee"]);
                                            rList.Add(rSingle);
                                        }


                                        var QueryByFundID =
                                            from r in rList
                                            group r by new { r.FundID } into rGroup
                                            select rGroup;

                                        int incRowExcel = 1;


                                        //incRowExcel++;

                                        incRowExcel = incRowExcel + 2;

                                        int RowA = incRowExcel;

                                        //incRowExcel = incRowExcel + 2;

                                        foreach (var rsHeader in QueryByFundID)
                                        {

                                            int RowZ = incRowExcel;
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Value = rsHeader.Key.FundID;

                                            incRowExcel = incRowExcel + 2;
                                            //Row B = 3
                                            int RowBZ = incRowExcel;
                                            int RowGZ = incRowExcel + 1;


                                            worksheet.Cells[incRowExcel, 1].Value = "No";
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells["A" + RowBZ + ":A" + RowGZ].Merge = true;
                                            worksheet.Cells["A" + RowBZ + ":A" + RowGZ].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["A" + RowBZ + ":A" + RowGZ].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;


                                            worksheet.Cells["A" + RowGZ + ":H" + RowGZ].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;


                                            worksheet.Cells[incRowExcel, 2].Value = "Reksadana";
                                            worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                            worksheet.Cells["B" + RowBZ + ":C" + RowGZ].Merge = true;
                                            worksheet.Cells["B" + RowBZ + ":C" + RowGZ].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["B" + RowBZ + ":C" + RowGZ].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 4].Value = "Type";
                                            worksheet.Cells[incRowExcel, 4].Style.Font.Bold = true;
                                            worksheet.Cells["D" + RowBZ + ":D" + RowGZ].Merge = true;
                                            worksheet.Cells["D" + RowBZ + ":D" + RowGZ].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["D" + RowBZ + ":D" + RowGZ].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 5].Value = "Total Nasabah";
                                            worksheet.Cells[incRowExcel, 5].Style.Font.Bold = true;
                                            worksheet.Cells["E" + RowBZ + ":E" + RowGZ].Merge = true;
                                            worksheet.Cells["E" + RowBZ + ":E" + RowGZ].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["E" + RowBZ + ":E" + RowGZ].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 6].Value = "Total Unit";
                                            worksheet.Cells[incRowExcel, 6].Style.Font.Bold = true;
                                            worksheet.Cells["F" + RowBZ + ":F" + RowGZ].Merge = true;
                                            worksheet.Cells["F" + RowBZ + ":F" + RowGZ].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["F" + RowBZ + ":F" + RowGZ].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 7].Value = "Total AUM";
                                            worksheet.Cells[incRowExcel, 7].Style.Font.Bold = true;
                                            worksheet.Cells["G" + RowBZ + ":G" + RowGZ].Merge = true;
                                            worksheet.Cells["G" + RowBZ + ":G" + RowGZ].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["G" + RowBZ + ":G" + RowGZ].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 8].Value = "Total ManagementFee";
                                            worksheet.Cells[incRowExcel, 8].Style.Font.Bold = true;
                                            worksheet.Cells["H" + RowBZ + ":H" + RowGZ].Merge = true;
                                            worksheet.Cells["H" + RowBZ + ":H" + RowGZ].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["H" + RowBZ + ":H" + RowGZ].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            incRowExcel++;

                                            // Row C = 4
                                            int RowC = incRowExcel;

                                            incRowExcel++;
                                            //area header

                                            int _no = 1;
                                            int _startRowDetail = incRowExcel;
                                            int _endRowDetail = 0;
                                            foreach (var rsDetail in rsHeader)
                                            {
                                                //Row D = 5
                                                int RowD = incRowExcel;
                                                int RowE = incRowExcel + 1;
                                                int RowEZ = incRowExcel - 1;


                                                //ThickBox Border

                                                worksheet.Cells["A" + RowBZ + ":H" + RowBZ].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + RowBZ + ":H" + RowBZ].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + incRowExcel + ":H" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + incRowExcel + ":H" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Medium;

                                                //area detail
                                                worksheet.Cells[incRowExcel, 1].Value = _no;
                                                worksheet.Cells["A" + incRowExcel + ":A" + incRowExcel].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells["A" + incRowExcel + ":A" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 2].Value = rsDetail.FundID;
                                                worksheet.Cells["B" + incRowExcel + ":C" + incRowExcel].Merge = true;
                                                worksheet.Cells[incRowExcel, 4].Value = rsDetail.Type;

                                                worksheet.Cells[incRowExcel, 5].Value = rsDetail.TotalNasabah;
                                                worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##";
                                                worksheet.Cells[incRowExcel, 6].Value = rsDetail.TotalUnit;
                                                worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 7].Value = rsDetail.TotalAUM;
                                                worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 8].Value = rsDetail.TotalManagementFee;
                                                worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0.00";


                                                _endRowDetail = incRowExcel;
                                                _no++;
                                                incRowExcel++;

                                            }

                                            worksheet.Cells["A" + RowBZ].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + RowBZ + ":A" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + RowBZ + ":A" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells["B" + RowBZ].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["B" + RowBZ + ":B" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["B" + RowBZ + ":B" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["B" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells["C" + RowBZ].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["C" + RowBZ + ":C" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["C" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells["D" + RowBZ].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["D" + RowBZ + ":D" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["D" + RowBZ + ":D" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["D" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells["E" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells["F" + RowBZ].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["F" + RowBZ + ":F" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["F" + RowBZ + ":F" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["F" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells["G" + RowBZ].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["G" + RowBZ + ":G" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["G" + RowBZ + ":G" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["G" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells["H" + RowBZ].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["H" + RowBZ + ":H" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["H" + RowBZ + ":H" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["H" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            incRowExcel = incRowExcel + 2;

                                            incRowExcel = incRowExcel + 4;

                                            worksheet.Cells[incRowExcel, 1].Value = "Disclaimer : ";
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Merge = true;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Size = 12;

                                            incRowExcel++;

                                            worksheet.Cells[incRowExcel, 1].Value = "Dokumen ini dipersiapkan oleh PT SUCOR SEKURITAS dan hanya bisa digunakan untuk kepentingan investor tersebut diatas dan tidak untuk pihak lainnya. Laporan ini bukan merupakan konfirmasi dari PT SUCOR SEKURITAS dan tidak untuk menggantikan laporan yang wajib diterbitkan oleh Bank Kustodian, jika ada perbedaan antara laporan ini dengan laporan Bank Kustodian, maka laporan Bank Kustodian adalah yang benar. Laporan ini diproses oleh computer dan tidak memerlukan tandatangan.";
                                            worksheet.Cells[incRowExcel, 1, incRowExcel + 2, 8].Style.Font.Size = 11;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel + 2, 8].Merge = true;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel + 2, 8].Style.WrapText = true;

                                            incRowExcel = incRowExcel + 4;

                                            worksheet.Cells[incRowExcel, 1].Value = "PT SUCOR SEKURITAS terdaftar dan diawasi oleh OJK, dengan nomor registrasi KEP-01/PM/MI/1999";
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 8].Style.Font.Size = 11;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 8].Merge = true;

                                            incRowExcel = incRowExcel + 2;

                                            //worksheet.Cells[incRowExcel, 1].Value = "PT SUCOR SEKURITAS";
                                            //worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Merge = true;
                                            //worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Bold = true;
                                            //worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Size = 12;

                                            //incRowExcel++;

                                            //worksheet.Cells[incRowExcel, 1].Value = "Sahid Sudirman Center, 12th Fl.";
                                            //worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Merge = true;
                                            //worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Size = 11;

                                            //incRowExcel++;

                                            //worksheet.Cells[incRowExcel, 1].Value = "Jl. Jenderal Sudirman Kav. 86, Jakarta 10220 - Indonesia";
                                            //worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Merge = true;
                                            //worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Size = 11;

                                            //incRowExcel++;

                                            //worksheet.Cells[incRowExcel, 1].Value = "p. +62-21  8067 3000, f.  +62-21  2788 9288 |sucorsekuritas.com";
                                            //worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Merge = true;
                                            //worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Size = 11;

                                            incRowExcel++;

                                            worksheet.Row(incRowExcel).PageBreak = false;

                                        }
                                        //incRowExcel++;
                                        int _lastRow = incRowExcel;

                                        string _rangeA = "A2:I" + incRowExcel;
                                        using (ExcelRange r = worksheet.Cells[_rangeA]) // KALO  KOLOM 1 SAMPE 9 A-I
                                        {
                                            r.Style.Font.Size = 28;
                                        }

                                        worksheet.DeleteRow(_lastRow);


                                        worksheet.PrinterSettings.FitToPage = true;
                                        worksheet.PrinterSettings.FitToWidth = 1;
                                        worksheet.PrinterSettings.FitToHeight = 0;
                                        worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel - 1, 11];
                                        worksheet.Column(1).Width = 20;
                                        worksheet.Column(2).Width = 50;
                                        worksheet.Column(3).Width = 2;
                                        worksheet.Column(4).Width = 30;
                                        worksheet.Column(5).Width = 45;
                                        worksheet.Column(6).Width = 40;
                                        worksheet.Column(7).Width = 60;
                                        worksheet.Column(8).Width = 70;


                                        // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                        //worksheet.Cells["A3:I14"].AutoFitColumns(); // UNTUK AUTO FIT UKURAN TULISAN DENGAN KOLOM, KEBETULAN INI BUAT HEADER AJA DIPAKENYA
                                        //worksheetApproved.Cells["A3:E3"].AutoFitColumns();
                                        worksheet.PrinterSettings.Orientation = eOrientation.Landscape; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                        //     worksheet.PrinterSettings.RepeatRows = new ExcelAddress("11:11");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN
                                        //worksheet.HeaderFooter.OddHeader.CenteredText = "\n \n \n \n \n \n \n &30&B SUBSCRIPTION \n &28&B Batch Form";



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

                                        worksheet.HeaderFooter.OddHeader.LeftAlignedText =
                                          "&28" + _host.Get_CompanyName() + "\n" +
                                          "Rekap Management Fee - Fund \n" +
                                          "Period Date : " + _unitRegistryRpt.ValueDateFrom + " - " + _unitRegistryRpt.ValueDateTo;
                                        ;


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

            #region Rekap Transaction Fee
            else if (_unitRegistryRpt.ReportName.Equals("Rekap Transaction Fee"))
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
                            string _status = "";

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
                                //_paramAgent = "And (E.ID  =  left(@AgentFrom,charindex('-',@AgentFrom) - 1) or F.ID = left(@AgentFrom,charindex('-',@AgentFrom) - 1)) ";
                                _paramAgent = "And A.AgentPK in ( " + _unitRegistryRpt.AgentFrom + " ) ";
                            }
                            else
                            {
                                _paramAgent = "";
                            }


                            if (_unitRegistryRpt.Status == 1)
                            {
                                _status = " and A.Status = 2 and A.Posted = 1 and A.Revised = 0 ";
                            }
                            else if (_unitRegistryRpt.Status == 2)
                            {
                                _status = " and A.Status = 2 and A.Posted = 1 and A.Revised = 1 ";
                            }
                            else if (_unitRegistryRpt.Status == 3)
                            {
                                _status = " and A.Status = 2 and A.Posted = 0 and A.Revised = 0 ";
                            }
                            else if (_unitRegistryRpt.Status == 4)
                            {
                                _status = " and A.Status = 1  ";
                            }
                            else if (_unitRegistryRpt.Status == 5)
                            {
                                _status = " and (A.Status = 2 or A.Posted = 1) and A.Revised = 0  ";
                            }
                            else if (_unitRegistryRpt.Status == 6)
                            {
                                _status = " and (A.Status = 1 Or A.Status = 2 or A.Posted = 1) and A.Revised = 0  ";
                            }

                            cmd.CommandText =

                            @" 
                           select * from (

                            Select isnull(C.ID,'') + ' ' + C.Name ClientName, 
                            B.ID FundID, 'Subscribe' Description, F.ID Cabang,E.ID Referral,
                            A.ValueDate,A.CashAmount,A.SubscriptionFeePercent FeePercent,A.SubscriptionFeeAmount FeeAmount,
                            A.SubscriptionFeeAmount / 1.1 NetFee

                            from ClientSubscription A
                            left join Fund B on A.FundPK = B.FundPK and B.status = 2
                            left join FundClient C on A.FundClientPK = C.FundClientPK and C.status = 2
                            left join Agent E on C.SellingAgentPK = E.AgentPK and E.Status  = 2 
                            left join Agent F on E.ParentPK = F.AgentPK  and F.Status = 2
                            where A.ValueDate between @DateFrom and @DateTo
                            " + _paramFund + _paramFundClient + _paramAgent + _status +

                            @"UNION ALL

                            Select isnull(C.ID,'') + ' ' + C.Name ClientName, 
                            B.ID FundID, 'Redemption' Description, F.ID Cabang,E.ID Referral,
                            A.ValueDate,A.CashAmount,
                            A.RedemptionFeePercent FeePercent,A.RedemptionFeeAmount FeeAmount,
                            A.RedemptionFeeAmount / 1.1 NetFee
                            from ClientRedemption A
                            left join Fund B on A.FundPK = B.FundPK and B.status = 2
                            left join FundClient C on A.FundClientPK = C.FundClientPK and C.status = 2
                            left join Agent E on C.SellingAgentPK = E.AgentPK and E.Status  = 2 
                            left join Agent F on E.ParentPK = F.AgentPK  and F.Status = 2
                            where ValueDate between @DateFrom and @DateTo 
                            " + _paramFund + _paramFundClient + _paramAgent + _status +
                            @")A 
                            order by A.ValueDate,ClientName,FundID asc
                            ";

                            cmd.CommandTimeout = 0;
                            cmd.Parameters.AddWithValue("@DateFrom", _unitRegistryRpt.ValueDateFrom);
                            cmd.Parameters.AddWithValue("@DateTo", _unitRegistryRpt.ValueDateTo);
                            cmd.Parameters.AddWithValue("@FundFrom", _unitRegistryRpt.FundFrom);
                            cmd.Parameters.AddWithValue("@FundClientFrom", _unitRegistryRpt.FundClientFrom);
                            cmd.Parameters.AddWithValue("@AgentFrom", _unitRegistryRpt.AgentFrom);



                            using (SqlDataReader dr0 = cmd.ExecuteReader())
                            {
                                if (!dr0.HasRows)
                                {
                                    return false;
                                }
                                else
                                {
                                    string filePath = Tools.ReportsPath + "RekapTransactionFee" + "_" + _userID + ".xlsx";
                                    string pdfPath = Tools.ReportsPath + "RekapTransactionFee" + "_" + _userID + ".pdf";
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

                                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Rekap Transaction Fee");


                                        //ATUR DATA GROUPINGNYA DULU
                                        List<RekapTransactionFee> rList = new List<RekapTransactionFee>();
                                        while (dr0.Read())
                                        {
                                            RekapTransactionFee rSingle = new RekapTransactionFee();
                                            rSingle.ClientName = Convert.ToString(dr0["ClientName"]);
                                            rSingle.FundID = Convert.ToString(dr0["FundID"]);
                                            rSingle.Cabang = Convert.ToString(dr0["Cabang"]);
                                            rSingle.Referral = Convert.ToString(dr0["Referral"]);
                                            rSingle.Description = Convert.ToString(dr0["Description"]);
                                            rSingle.ValueDate = Convert.ToDateTime(dr0["ValueDate"]);
                                            rSingle.CashAmount = Convert.ToDecimal(dr0["CashAmount"]);
                                            rSingle.FeePercent = Convert.ToDecimal(dr0["FeePercent"]);
                                            rSingle.FeeAmount = Convert.ToDecimal(dr0["FeeAmount"]);
                                            rSingle.NetFee = Convert.ToDecimal(dr0["NetFee"]);
                                            rList.Add(rSingle);
                                        }


                                        var QueryByFundID =
                                            from r in rList
                                            group r by new { r.Description } into rGroup
                                            select rGroup;

                                        int incRowExcel = 1;


                                        //incRowExcel++;

                                        //incRowExcel = incRowExcel + 2;

                                        int RowA = incRowExcel;

                                        //incRowExcel = incRowExcel + 2;

                                        foreach (var rsHeader in QueryByFundID)
                                        {

                                            int RowZ = incRowExcel;
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Value = rsHeader.Key.Description;

                                            incRowExcel = incRowExcel + 2;
                                            //Row B = 3
                                            int RowBZ = incRowExcel;
                                            int RowGZ = incRowExcel + 1;


                                            worksheet.Cells[incRowExcel, 1].Value = "No";
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells["A" + RowBZ + ":A" + RowGZ].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["A" + RowBZ + ":A" + RowGZ].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 2].Value = "Customer/Nasabah";
                                            worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                            worksheet.Cells["B" + RowBZ + ":C" + RowGZ].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["B" + RowBZ + ":C" + RowGZ].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 4].Value = "Tanggal";
                                            worksheet.Cells[incRowExcel, 4].Style.Font.Bold = true;
                                            worksheet.Cells["D" + RowBZ + ":D" + RowGZ].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["D" + RowBZ + ":D" + RowGZ].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 5].Value = "Reksadana";
                                            worksheet.Cells[incRowExcel, 5].Style.Font.Bold = true;
                                            worksheet.Cells["E" + RowBZ + ":E" + RowGZ].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["E" + RowBZ + ":E" + RowGZ].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 6].Value = "Cabang";
                                            worksheet.Cells[incRowExcel, 6].Style.Font.Bold = true;
                                            worksheet.Cells["F" + RowBZ + ":F" + RowGZ].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["F" + RowBZ + ":F" + RowGZ].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 7].Value = "Referral";
                                            worksheet.Cells[incRowExcel, 7].Style.Font.Bold = true;
                                            worksheet.Cells["G" + RowBZ + ":G" + RowGZ].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["G" + RowBZ + ":G" + RowGZ].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 8].Value = "Amount";
                                            worksheet.Cells[incRowExcel, 8].Style.Font.Bold = true;
                                            worksheet.Cells["H" + RowBZ + ":H" + RowGZ].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["H" + RowBZ + ":H" + RowGZ].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 9].Value = "Fee %";
                                            worksheet.Cells[incRowExcel, 9].Style.Font.Bold = true;
                                            worksheet.Cells["I" + RowBZ + ":I" + RowGZ].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["I" + RowBZ + ":I" + RowGZ].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 10].Value = "Fee Amount";
                                            worksheet.Cells[incRowExcel, 10].Style.Font.Bold = true;
                                            worksheet.Cells["J" + RowBZ + ":J" + RowGZ].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["J" + RowBZ + ":J" + RowGZ].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 11].Value = "Net Fee";
                                            worksheet.Cells[incRowExcel, 11].Style.Font.Bold = true;
                                            worksheet.Cells["K" + RowBZ + ":K" + RowGZ].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["K" + RowBZ + ":K" + RowGZ].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            incRowExcel++;

                                            // Row C = 4
                                            int RowC = incRowExcel;

                                            incRowExcel++;
                                            //area header

                                            int _no = 1;
                                            int _startRowDetail = incRowExcel;
                                            int _endRowDetail = 0;
                                            foreach (var rsDetail in rsHeader)
                                            {
                                                //Row D = 5
                                                int RowD = incRowExcel;
                                                int RowE = incRowExcel + 1;


                                                //ThickBox Border

                                                worksheet.Cells["A" + RowBZ + ":K" + RowBZ].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + RowBZ + ":K" + RowBZ].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                                //area detail
                                                worksheet.Cells[incRowExcel, 1].Value = _no;
                                                worksheet.Cells["A" + incRowExcel + ":A" + incRowExcel].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells["A" + incRowExcel + ":A" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 2].Value = rsDetail.ClientName;
                                                worksheet.Cells[incRowExcel, 4].Value = rsDetail.ValueDate;
                                                worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "dd/MMM/yyyy";
                                                worksheet.Cells[incRowExcel, 5].Value = rsDetail.FundID;
                                                worksheet.Cells[incRowExcel, 6].Value = rsDetail.Cabang;
                                                worksheet.Cells[incRowExcel, 7].Value = rsDetail.Referral;
                                                worksheet.Cells[incRowExcel, 8].Value = rsDetail.CashAmount;
                                                worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 9].Value = rsDetail.FeePercent;
                                                worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 10].Value = rsDetail.FeeAmount;
                                                worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 11].Value = rsDetail.NetFee;
                                                worksheet.Cells[incRowExcel, 11].Style.Numberformat.Format = "#,##0.00";


                                                _endRowDetail = incRowExcel;
                                                _no++;
                                                incRowExcel++;

                                            }

                                            worksheet.Cells["A" + RowBZ].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + RowBZ + ":A" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + RowBZ + ":A" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells["B" + RowBZ].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["B" + RowBZ + ":B" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["B" + RowBZ + ":B" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["B" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells["D" + RowBZ].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["D" + RowBZ + ":D" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["D" + RowBZ + ":D" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["D" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells["E" + RowBZ].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["E" + RowBZ + ":E" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["E" + RowBZ + ":E" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["E" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells["F" + RowBZ].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["F" + RowBZ + ":F" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["F" + RowBZ + ":F" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["F" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells["G" + RowBZ].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["G" + RowBZ + ":G" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["G" + RowBZ + ":G" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["G" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells["H" + RowBZ].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["H" + RowBZ + ":H" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["H" + RowBZ + ":H" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["H" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells["I" + RowBZ].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["I" + RowBZ + ":I" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["I" + RowBZ + ":I" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["I" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells["J" + RowBZ].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["J" + RowBZ + ":J" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["J" + RowBZ + ":J" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["J" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells["K" + RowBZ].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["K" + RowBZ + ":K" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["K" + RowBZ + ":K" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["K" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;


                                            incRowExcel = incRowExcel + 2;



                                            incRowExcel = incRowExcel + 4;

                                            worksheet.Cells[incRowExcel, 1].Value = "Disclaimer : ";
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Merge = true;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Size = 12;

                                            incRowExcel++;

                                            worksheet.Cells[incRowExcel, 1].Value = "Dokumen ini dipersiapkan oleh PT SUCOR SEKURITAS dan hanya bisa digunakan untuk kepentingan investor tersebut diatas dan tidak untuk pihak lainnya. Laporan ini bukan merupakan konfirmasi dari PT SUCOR SEKURITAS dan tidak untuk menggantikan laporan yang wajib diterbitkan oleh Bank Kustodian, jika ada perbedaan antara laporan ini dengan laporan Bank Kustodian, maka laporan Bank Kustodian adalah yang benar. Laporan ini diproses oleh computer dan tidak memerlukan tandatangan.";
                                            worksheet.Cells[incRowExcel, 1, incRowExcel + 2, 11].Style.Font.Size = 11;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel + 2, 11].Merge = true;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel + 2, 11].Style.WrapText = true;

                                            incRowExcel = incRowExcel + 4;

                                            worksheet.Cells[incRowExcel, 1].Value = "PT SUCOR SEKURITAS terdaftar dan diawasi oleh OJK, dengan nomor registrasi KEP-01/PM/MI/1999";
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 11].Style.Font.Size = 11;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 11].Merge = true;

                                            incRowExcel = incRowExcel + 2;

                                            incRowExcel++;

                                            worksheet.Row(incRowExcel).PageBreak = false;

                                        }
                                        //incRowExcel++;
                                        int _lastRow = incRowExcel;

                                        string _rangeA = "A2:K" + incRowExcel;
                                        using (ExcelRange r = worksheet.Cells[_rangeA]) // KALO  KOLOM 1 SAMPE 9 A-I
                                        {
                                            r.Style.Font.Size = 28;
                                        }

                                        worksheet.DeleteRow(_lastRow);

                                        worksheet.PrinterSettings.FitToPage = true;
                                        worksheet.PrinterSettings.FitToWidth = 1;
                                        worksheet.PrinterSettings.FitToHeight = 0;
                                        worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel - 1, 11];
                                        worksheet.Column(1).Width = 20;
                                        worksheet.Column(2).Width = 140;
                                        worksheet.Column(3).Width = 0;
                                        worksheet.Column(4).Width = 30;
                                        worksheet.Column(5).Width = 40;
                                        worksheet.Column(6).Width = 40;
                                        worksheet.Column(7).Width = 40;
                                        worksheet.Column(8).Width = 50;
                                        worksheet.Column(9).Width = 20;
                                        worksheet.Column(10).Width = 50;
                                        worksheet.Column(11).Width = 50;

                                        // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                        //worksheet.Cells["A3:I14"].AutoFitColumns(); // UNTUK AUTO FIT UKURAN TULISAN DENGAN KOLOM, KEBETULAN INI BUAT HEADER AJA DIPAKENYA
                                        //worksheetApproved.Cells["A3:E3"].AutoFitColumns();
                                        worksheet.PrinterSettings.Orientation = eOrientation.Landscape; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                        //  worksheet.PrinterSettings.RepeatRows = new ExcelAddress("11:11");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN
                                        //worksheet.HeaderFooter.OddHeader.CenteredText = "\n \n \n \n \n \n \n &30&B SUBSCRIPTION \n &28&B Batch Form";



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
                                        worksheet.HeaderFooter.OddFooter.LeftAlignedText = Tools.DefaultReportFooterLeftText();

                                        worksheet.HeaderFooter.OddHeader.LeftAlignedText =
                                         "&34" + _host.Get_CompanyName() + "\n" +
                                         "Rekap Transaction Fee \n" +
                                         "Period Date : " + _unitRegistryRpt.ValueDateFrom + " - " + _unitRegistryRpt.ValueDateTo;
                                        ;


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

            #region Daily Transaction Blotter Subscription
            else if (_unitRegistryRpt.ReportName.Equals("Daily Transaction Blotter Subscription"))
            {
                try
                {
                    using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                    {
                        DbCon.Open();
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            string _statusSubs = "";
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
                                _paramAgent = "And A.AgentPK in ( " + _unitRegistryRpt.AgentFrom + " ) ";
                            }
                            else
                            {
                                _paramAgent = "";
                            }

                            if (_unitRegistryRpt.Status == 1)
                            {
                                _statusSubs = "  A.Status = 2 and A.Posted = 1 and A.Revised = 0 ";
                                //_statusRedemp = "  CR.Status = 2 and CR.Posted = 1 and CR.Revised = 0 ";
                            }
                            else if (_unitRegistryRpt.Status == 2)
                            {
                                _statusSubs = "  A.Status = 2 and A.Posted = 1 and A.Revised = 1 ";
                                //_statusRedemp = "  CR.Status = 2 and CR.Posted = 1 and CR.Revised = 1 ";
                            }
                            else if (_unitRegistryRpt.Status == 3)
                            {
                                _statusSubs = "  A.Status = 2 and A.Posted = 0 and A.Revised = 0 ";
                                //_statusRedemp = "  CR.Status = 2 and CR.Posted = 0 and CR.Revised = 0 ";
                            }
                            else if (_unitRegistryRpt.Status == 4)
                            {
                                _statusSubs = "  A.Status = 1  ";
                                //_statusRedemp = "  CR.Status = 1  ";
                            }
                            else if (_unitRegistryRpt.Status == 5)
                            {
                                _statusSubs = "  A.Status = 3  ";
                                //_statusRedemp = "  CR.Status = 3  ";
                            }
                            else if (_unitRegistryRpt.Status == 6)
                            {
                                _statusSubs = "  (A.Status = 2 or A.Posted = 1) and A.Revised = 0  ";
                                //_statusRedemp = "  (CR.Status = 2 or CR.Posted = 1) and CR.Revised = 0  ";
                            }
                            else if (_unitRegistryRpt.Status == 7)
                            {
                                _statusSubs = "  (A.Status = 1 Or A.Status = 2 or A.Posted = 1) and  A.Revised = 0  ";
                                //_statusRedemp = "  (CR.Status = 1 Or CR.Status = 2 or CR.Posted = 1) and CR.Revised = 0  ";
                            }


                            cmd.CommandText =

                            @"
                            select A.ValueDate,case when B.ID = '0' then '' else B.ID end ID, 
                            B.Name,A.CashAmount,CAST(A.SubscriptionFeePercent as numeric(14,2)) SubscriptionFeePercent,
                            A.SubscriptionFeeAmount,
                            A.TotalCashAmount,A.NAV,A.TotalUnitAmount,C.Name FundName,B.RDNAccountNo + ' ' + B.RDNAccountName CashRefDesc,D.Name AgentName
                            from ClientSubscription A
                            left join fundclient B on A.FundClientPK = B.FundClientPK and B.status = 2
                            left join fund c on A.FundPK = C.FundPK and C.status = 2
                            left join Agent D on A.AgentPK = D.AgentPK and D.status = 2
                            left join FundCashRef E on A.CashRefPK = E.FundCashRefPK and E.status =2
                            WHERE " + _statusSubs + _paramFund + _paramAgent + _paramFundClient +
                            @" and ValueDate between @DateFrom and @DateTo";

                            cmd.CommandTimeout = 0;
                            cmd.Parameters.AddWithValue("@DateFrom", _unitRegistryRpt.ValueDateFrom);
                            cmd.Parameters.AddWithValue("@DateTo", _unitRegistryRpt.ValueDateTo);
                            cmd.Parameters.AddWithValue("@FundFrom", _unitRegistryRpt.FundFrom);
                            cmd.Parameters.AddWithValue("@FundClientFrom", _unitRegistryRpt.FundClientFrom);
                            cmd.Parameters.AddWithValue("@AgentFrom", _unitRegistryRpt.AgentFrom);


                            using (SqlDataReader dr0 = cmd.ExecuteReader())
                            {
                                if (!dr0.HasRows)
                                {
                                    return false;
                                }
                                else
                                {
                                    string filePath = Tools.ReportsPath + "DailyTransactionBlotterSubscription" + "_" + _userID + ".xlsx";
                                    string pdfPath = Tools.ReportsPath + "DailyTransactionBlotterSubscription" + "_" + _userID + ".pdf";
                                    FileInfo excelFile = new FileInfo(filePath);
                                    if (excelFile.Exists)
                                    {
                                        excelFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                        excelFile = new FileInfo(filePath);
                                    }

                                    // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                    using (ExcelPackage package = new ExcelPackage(excelFile))
                                    {
                                        package.Workbook.Properties.Title = "DailyTransactionBlotterSubscription";
                                        package.Workbook.Properties.Author = Tools.DefaultReportAuthor();
                                        package.Workbook.Properties.Comments = Tools.DefaultReportComments();
                                        package.Workbook.Properties.Company = Tools.DefaultReportCompany();
                                        package.Workbook.Properties.SetCustomPropertyValue("Checked by", _userID);
                                        package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", Tools.DefaultReportAssemblyName());

                                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Subscription");

                                        int incRowExcel = 1;

                                        worksheet.Cells[incRowExcel, 4].Value = "LAPORAN DAILY TRANSACTION BLOTTER";
                                        worksheet.Cells[incRowExcel, 4, incRowExcel, 10].Merge = true;
                                        worksheet.Cells[incRowExcel, 4, incRowExcel, 10].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 4, incRowExcel, 10].Style.Font.Size = 12;

                                        incRowExcel++;

                                        worksheet.Cells[incRowExcel, 4].Value = "SELLING AGENT " + _host.Get_CompanyName();
                                        worksheet.Cells[incRowExcel, 4, incRowExcel, 10].Merge = true;
                                        worksheet.Cells[incRowExcel, 4, incRowExcel, 10].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 4, incRowExcel, 10].Style.Font.Size = 12;

                                        incRowExcel = incRowExcel + 2;

                                        worksheet.Cells[incRowExcel, 4].Value = "Periode: " + Convert.ToDateTime(_unitRegistryRpt.ValueDateFrom).ToShortDateString() + " s/d " + Convert.ToDateTime(_unitRegistryRpt.ValueDateTo).ToShortDateString();
                                        worksheet.Cells[incRowExcel, 4, incRowExcel, 10].Merge = true;
                                        worksheet.Cells[incRowExcel, 4, incRowExcel, 10].Style.Font.Size = 9;
                                        worksheet.Cells[incRowExcel, 4, incRowExcel, 6].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                                        incRowExcel = incRowExcel + 2;

                                        worksheet.Cells[incRowExcel, 1].Value = "SUBSCRIPTION REPORT";
                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Merge = true;
                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.UnderLine = true;
                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Size = 11;

                                        incRowExcel = incRowExcel + 2;


                                        //ATUR DATA GROUPINGNYA DULU
                                        List<SubscriptionRpt> rList = new List<SubscriptionRpt>();
                                        while (dr0.Read())
                                        {
                                            SubscriptionRpt rSingle = new SubscriptionRpt();
                                            rSingle.TransactionDate = dr0["ValueDate"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["ValueDate"]);
                                            rSingle.ID = dr0["ID"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr0["ID"]);
                                            rSingle.InvestorName = dr0["Name"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["Name"]);
                                            rSingle.GrossAmount = dr0["CashAmount"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["CashAmount"]);
                                            rSingle.SubsFeePercentage = dr0["SubscriptionFeePercent"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["SubscriptionFeePercent"]);
                                            rSingle.SubsFee = dr0["SubscriptionFeeAmount"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["SubscriptionFeeAmount"]);
                                            rSingle.NettAmount = dr0["TotalCashAmount"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["TotalCashAmount"]);
                                            rSingle.NAV = dr0["NAV"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["NAV"]);
                                            rSingle.Units = dr0["TotalUnitAmount"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["TotalUnitAmount"]);
                                            rSingle.Fund = dr0["FundName"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["FundName"]);
                                            rSingle.CashRefDesc = dr0["CashRefDesc"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["CashRefDesc"]);
                                            rSingle.AgentName = dr0["AgentName"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["AgentName"]);
                                            rList.Add(rSingle);

                                        }

                                        var QueryByFundID =
                                                     from r in rList
                                                     group r by new { r.Fund } into rGroup
                                                     select rGroup;


                                        foreach (var rsHeader in QueryByFundID)
                                        {
                                            worksheet.Cells[incRowExcel, 1].Value = "FUND : ";
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 1].Merge = true;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 1].Style.Font.Size = 10;
                                            worksheet.Cells[incRowExcel, 2].Value = rsHeader.Key.Fund;
                                            worksheet.Cells[incRowExcel, 2, incRowExcel, 12].Merge = true;
                                            worksheet.Cells[incRowExcel, 2, incRowExcel, 12].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 2, incRowExcel, 12].Style.Font.Size = 10;

                                            incRowExcel = incRowExcel + 2;

                                            worksheet.Cells[incRowExcel, 1].Value = "NO";
                                            worksheet.Cells[incRowExcel, 2].Value = "TGL TRANSAKSI";
                                            worksheet.Cells[incRowExcel, 2].Style.WrapText = true;
                                            worksheet.Cells[incRowExcel, 3].Value = "CUSTOMER ID";
                                            worksheet.Cells[incRowExcel, 4].Value = "NAMA INVESTOR";
                                            worksheet.Cells[incRowExcel, 5].Value = "GROSS AMOUNT";
                                            worksheet.Cells[incRowExcel, 6].Value = "FEE (%)";
                                            worksheet.Cells[incRowExcel, 7].Value = "FEE";
                                            worksheet.Cells[incRowExcel, 8].Value = "NETT AMOUNT";
                                            worksheet.Cells[incRowExcel, 9].Value = "NAV";
                                            worksheet.Cells[incRowExcel, 10].Value = "UNITS";
                                            worksheet.Cells[incRowExcel, 11].Value = "TRANSFER TO BANK";
                                            worksheet.Cells[incRowExcel, 12].Value = "AGENT NAME";

                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 12].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 12].Style.Font.Size = 8;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 12].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 12].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(251, 228, 213));
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 12].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 12].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 12].Style.Border.Top.Style = ExcelBorderStyle.Double;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 12].Style.Border.Bottom.Style = ExcelBorderStyle.Double;
                                            worksheet.Cells["A" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells["L" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                                            incRowExcel = incRowExcel + 1;

                                            int first = incRowExcel;

                                            int no = 1;

                                            foreach (var rsDetail in rsHeader)
                                            {
                                                worksheet.Cells[incRowExcel, 1].Value = no;
                                                worksheet.Cells[incRowExcel, 1].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 1].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 2].Style.Numberformat.Format = "dd-MMM-yyyy";
                                                worksheet.Cells[incRowExcel, 2].Value = Convert.ToDateTime(rsDetail.TransactionDate);
                                                worksheet.Cells[incRowExcel, 2, incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 2, incRowExcel, 2].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.ID;
                                                worksheet.Cells[incRowExcel, 3, incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 3, incRowExcel, 3].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 4].Value = rsDetail.InvestorName;
                                                worksheet.Cells[incRowExcel, 4, incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                worksheet.Cells[incRowExcel, 4, incRowExcel, 4].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 5].Value = rsDetail.GrossAmount;
                                                worksheet.Cells[incRowExcel, 5, incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                worksheet.Cells[incRowExcel, 5, incRowExcel, 5].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 6].Value = rsDetail.SubsFeePercentage; // +".00%";
                                                worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "###0.00";
                                                worksheet.Cells[incRowExcel, 6, incRowExcel, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 6, incRowExcel, 6].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 7].Value = rsDetail.SubsFee;
                                                worksheet.Cells[incRowExcel, 7, incRowExcel, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                worksheet.Cells[incRowExcel, 7, incRowExcel, 7].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 8].Value = rsDetail.NettAmount;
                                                worksheet.Cells[incRowExcel, 8, incRowExcel, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                worksheet.Cells[incRowExcel, 8, incRowExcel, 8].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 9].Value = rsDetail.NAV;
                                                worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0.0000";
                                                worksheet.Cells[incRowExcel, 9, incRowExcel, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                worksheet.Cells[incRowExcel, 9, incRowExcel, 9].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0.0000";
                                                worksheet.Cells[incRowExcel, 10].Value = rsDetail.Units;
                                                worksheet.Cells[incRowExcel, 10, incRowExcel, 10].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                worksheet.Cells[incRowExcel, 10, incRowExcel, 10].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 11].Value = rsDetail.CashRefDesc;

                                                //worksheet.Cells[incRowExcel, 11].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                worksheet.Cells[incRowExcel, 11].Style.WrapText = true;
                                                worksheet.Cells[incRowExcel, 12].Value = rsDetail.AgentName;
                                                worksheet.Cells[incRowExcel, 12].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells[incRowExcel, 12].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                worksheet.Cells[incRowExcel, 12].Style.WrapText = true;

                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 12].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 12].Style.Font.Size = 8;

                                                incRowExcel++;
                                                no++;
                                            }

                                            int last = incRowExcel - 1;
                                            //incRowExcel = incRowExcel - 1;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 12].Style.Border.Top.Style = ExcelBorderStyle.Double;

                                            //incRowExcel++;

                                            worksheet.Cells[incRowExcel, 4].Value = "Total";
                                            worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.00";
                                            worksheet.Cells[incRowExcel, 5].Formula = "SUM(E" + first.ToString() + ":E" + last.ToString() + ")";
                                            worksheet.Cells[incRowExcel, 5].Calculate();
                                            worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0.00";
                                            worksheet.Cells[incRowExcel, 7].Formula = "SUM(G" + first.ToString() + ":G" + last.ToString() + ")";
                                            worksheet.Cells[incRowExcel, 7].Calculate();
                                            worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0.00";
                                            worksheet.Cells[incRowExcel, 8].Formula = "SUM(H" + first.ToString() + ":H" + last.ToString() + ")";
                                            worksheet.Cells[incRowExcel, 8].Calculate();
                                            worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0.0000";
                                            worksheet.Cells[incRowExcel, 10].Formula = "SUM(J" + first.ToString() + ":J" + last.ToString() + ")";
                                            worksheet.Cells[incRowExcel, 10].Calculate();

                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 11].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 11].Style.Font.Size = 8;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 11].Style.VerticalAlignment = ExcelVerticalAlignment.Center;


                                            //worksheet.Cells.Calculate();
                                            incRowExcel++;

                                            incRowExcel = incRowExcel + 2;



                                        }

                                        incRowExcel = incRowExcel + 4;

                                        worksheet.Cells[incRowExcel, 1].Value = "Disclaimer : ";
                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Merge = true;
                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Size = 12;

                                        incRowExcel++;

                                        worksheet.Cells[incRowExcel, 1].Value = "Dokumen ini dipersiapkan oleh PT SUCOR SEKURITAS dan hanya bisa digunakan untuk kepentingan investor tersebut diatas dan tidak untuk pihak lainnya. Laporan ini bukan merupakan konfirmasi dari PT SUCOR SEKURITAS dan tidak untuk menggantikan laporan yang wajib diterbitkan oleh Bank Kustodian, jika ada perbedaan antara laporan ini dengan laporan Bank Kustodian, maka laporan Bank Kustodian adalah yang benar. Laporan ini diproses oleh computer dan tidak memerlukan tandatangan.";
                                        worksheet.Cells[incRowExcel, 1, incRowExcel + 2, 12].Style.Font.Size = 11;
                                        worksheet.Cells[incRowExcel, 1, incRowExcel + 2, 12].Merge = true;
                                        worksheet.Cells[incRowExcel, 1, incRowExcel + 2, 12].Style.WrapText = true;

                                        incRowExcel = incRowExcel + 4;

                                        worksheet.Cells[incRowExcel, 1].Value = "PT SUCOR SEKURITAS terdaftar dan diawasi oleh OJK, dengan nomor registrasi KEP-01/PM/MI/1999";
                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 12].Style.Font.Size = 11;
                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 12].Merge = true;

                                        incRowExcel = incRowExcel + 2;

                                        //worksheet.Cells[incRowExcel, 1].Value = "PT SUCOR SEKURITAS";
                                        //worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Merge = true;
                                        //worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Bold = true;
                                        //worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Size = 12;

                                        //incRowExcel++;

                                        //worksheet.Cells[incRowExcel, 1].Value = "Sahid Sudirman Center, 12th Fl.";
                                        //worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Merge = true;
                                        //worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Size = 11;

                                        //incRowExcel++;

                                        //worksheet.Cells[incRowExcel, 1].Value = "Jl. Jenderal Sudirman Kav. 86, Jakarta 10220 - Indonesia";
                                        //worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Merge = true;
                                        //worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Size = 11;

                                        //incRowExcel++;

                                        //worksheet.Cells[incRowExcel, 1].Value = "p. +62-21  8067 3000, f.  +62-21  2788 9288 |sucorsekuritas.com";
                                        //worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Merge = true;
                                        //worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Size = 11;

                                        worksheet.PrinterSettings.FitToPage = true;
                                        worksheet.PrinterSettings.FitToWidth = 1;
                                        worksheet.PrinterSettings.FitToHeight = 0;
                                        worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel - 1, 12];
                                        worksheet.Column(1).Width = 7;
                                        worksheet.Column(2).Width = 15;
                                        worksheet.Column(3).Width = 14;
                                        worksheet.Column(4).Width = 30;
                                        worksheet.Column(5).Width = 15;
                                        worksheet.Column(6).Width = 10;
                                        worksheet.Column(7).Width = 10;
                                        worksheet.Column(8).Width = 15;
                                        worksheet.Column(9).Width = 12;
                                        worksheet.Column(10).Width = 13;
                                        worksheet.Column(11).Width = 25;
                                        worksheet.Column(12).Width = 25;

                                        // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                        //worksheet.Cells.AutoFitColumns(); // UNTUK AUTO FIT UKURAN TULISAN DENGAN KOLOM, KEBETULAN INI BUAT HEADER AJA DIPAKENYA
                                        //worksheet.PrinterSettings.FitToPage = true;
                                        worksheet.PrinterSettings.Orientation = eOrientation.Landscape; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                        worksheet.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN
                                        worksheet.HeaderFooter.OddHeader.RightAlignedText = "&12 Subscription REPORT";

                                        // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                        //ExcelWorksheet ws = package.Workbook.Worksheets.Add("Demo");

                                        worksheet.PrinterSettings.TopMargin = (decimal).3 / 2.54M; // narrow border
                                        worksheet.PrinterSettings.RightMargin = (decimal).3 / 2.54M; //narrow borderExcelWorksheet ws = pck.Workbook.Worksheets.Add("Demo");
                                        worksheet.PrinterSettings.LeftMargin = (decimal).3 / 2.54M; // narrow border

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

                                        //worksheet.HeaderFooter.OddFooter.CenteredText = Tools.DefaultReportFooterCenterText();
                                        worksheet.HeaderFooter.OddFooter.LeftAlignedText = Tools.DefaultReportFooterLeftText();

                                        package.Save();
                                        if (_unitRegistryRpt.DownloadMode == "PDF")
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

            #region Daily Transaction Blotter Redemption
            else if (_unitRegistryRpt.ReportName.Equals("Daily Transaction Blotter Redemption"))
            {
                try
                {
                    using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                    {
                        DbCon.Open();
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            string _statusSubs = "";
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
                                _paramFundClient = "And A.FundClientPK  in ( " + _unitRegistryRpt.FundClientFrom + " ) ";
                            }
                            else
                            {
                                _paramFundClient = "";
                            }
                            if (!_host.findString(_unitRegistryRpt.AgentFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_unitRegistryRpt.AgentFrom))
                            {
                                _paramAgent = "And A.AgentPK  in ( " + _unitRegistryRpt.AgentFrom + " ) ";
                            }
                            else
                            {
                                _paramAgent = "";
                            }

                            if (_unitRegistryRpt.Status == 1)
                            {
                                _statusSubs = "  A.Status = 2 and A.Posted = 1 and A.Revised = 0 ";
                                //_statusRedemp = "  CR.Status = 2 and CR.Posted = 1 and CR.Revised = 0 ";
                            }
                            else if (_unitRegistryRpt.Status == 2)
                            {
                                _statusSubs = "  A.Status = 2 and A.Posted = 1 and A.Revised = 1 ";
                                //_statusRedemp = "  CR.Status = 2 and CR.Posted = 1 and CR.Revised = 1 ";
                            }
                            else if (_unitRegistryRpt.Status == 3)
                            {
                                _statusSubs = "  A.Status = 2 and A.Posted = 0 and A.Revised = 0 ";
                                //_statusRedemp = "  CR.Status = 2 and CR.Posted = 0 and CR.Revised = 0 ";
                            }
                            else if (_unitRegistryRpt.Status == 4)
                            {
                                _statusSubs = "  A.Status = 1  ";
                                //_statusRedemp = "  CR.Status = 1  ";
                            }
                            else if (_unitRegistryRpt.Status == 5)
                            {
                                _statusSubs = "  A.Status = 3  ";
                                //_statusRedemp = "  CR.Status = 3  ";
                            }
                            else if (_unitRegistryRpt.Status == 6)
                            {
                                _statusSubs = "  (A.Status = 2 or A.Posted = 1) and A.Revised = 0  ";
                                //_statusRedemp = "  (CR.Status = 2 or CR.Posted = 1) and CR.Revised = 0  ";
                            }
                            else if (_unitRegistryRpt.Status == 7)
                            {
                                _statusSubs = "  (A.Status = 1 Or A.Status = 2 or A.Posted = 1) and  A.Revised = 0  ";
                                //_statusRedemp = "  (CR.Status = 1 Or CR.Status = 2 or CR.Posted = 1) and CR.Revised = 0  ";
                            }


                            cmd.CommandText =

                            @"
                            select A.ValueDate,case when B.ID = '0' then '' else B.ID end ID, 
                            B.Name,A.CashAmount,A.UnitAmount,CAST(A.RedemptionFeePercent as numeric(14,2)) RedemptionFeePercent,
                            A.RedemptionFeeAmount,
                            A.TotalCashAmount,A.NAV,A.TotalUnitAmount, C.Name FundName, A.PaymentDate,case when A.BitRedemptionAll = 1 then 'Y' else '' End BitRedemptionAllDesc,A.Description, 
                            Case when BankRecipientPK = 1 then G.Name + '-' + 'RDN' + '-' + B.RDNAccountName + '-' + B.RDNAccountNo
                            else Case when BankRecipientPK = 2 then E.Name + '-' + B.BankBranchName1 + '-' + B.NamaNasabah1 + '-' + B.NomorRekening1
                            else Case when BankRecipientPK = 3 then F.Name + '-' + B.BankBranchName2 + '-' + B.NamaNasabah2 + '-' + B.NomorRekening2
                            else Case when BankRecipientPK = 4 then F.Name + '-' + B.BankBranchName3 + '-' + B.NamaNasabah3 + '-' + B.NomorRekening3
                            else '' end end end end BeneficiaryBank, D.Name AgentName
                            from ClientRedemption A
                            left join fundclient B on A.FundClientPK = B.FundClientPK and B.status = 2
                            left join fund C on A.FundPK = C.FundPK and C.status = 2
                            left join Agent D on A.AgentPK = D.AgentPK and D.status = 2
                            left join Bank E on B.NamaBank1 = E.BankPK and E.status = 2
                            left join Bank F on B.NamaBank2 = F.BankPK and F.status = 2
                            left join Bank G on B.bankRDNPK = G.BankPK and G.status = 2
                            WHERE " + _statusSubs + _paramFund + _paramAgent + _paramFundClient +
                            " and ValueDate between @DateFrom and @DateTo";

                            cmd.CommandTimeout = 0;
                            cmd.Parameters.AddWithValue("@DateFrom", _unitRegistryRpt.ValueDateFrom);
                            cmd.Parameters.AddWithValue("@DateTo", _unitRegistryRpt.ValueDateTo);
                            cmd.Parameters.AddWithValue("@FundFrom", _unitRegistryRpt.FundFrom);
                            cmd.Parameters.AddWithValue("@FundClientFrom", _unitRegistryRpt.FundClientFrom);
                            cmd.Parameters.AddWithValue("@AgentFrom", _unitRegistryRpt.AgentFrom);


                            using (SqlDataReader dr0 = cmd.ExecuteReader())
                            {
                                if (!dr0.HasRows)
                                {
                                    return false;
                                }
                                else
                                {
                                    string filePath = Tools.ReportsPath + "DailyTransactionBlotterRedemption" + "_" + _userID + ".xlsx";
                                    string pdfPath = Tools.ReportsPath + "DailyTransactionBlotterRedemption" + "_" + _userID + ".pdf";
                                    FileInfo excelFile = new FileInfo(filePath);
                                    if (excelFile.Exists)
                                    {
                                        excelFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                        excelFile = new FileInfo(filePath);
                                    }

                                    // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                    using (ExcelPackage package = new ExcelPackage(excelFile))
                                    {
                                        package.Workbook.Properties.Title = "DailyTransactionBlotterRedemption";
                                        package.Workbook.Properties.Author = Tools.DefaultReportAuthor();
                                        package.Workbook.Properties.Comments = Tools.DefaultReportComments();
                                        package.Workbook.Properties.Company = Tools.DefaultReportCompany();
                                        package.Workbook.Properties.SetCustomPropertyValue("Checked by", _userID);
                                        package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", Tools.DefaultReportAssemblyName());

                                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Redemption");

                                        int incRowExcel = 1;

                                        worksheet.Cells[incRowExcel, 4].Value = "LAPORAN DAILY TRANSACTION BLOTTER";
                                        worksheet.Cells[incRowExcel, 4, incRowExcel, 10].Merge = true;
                                        worksheet.Cells[incRowExcel, 4, incRowExcel, 10].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 4, incRowExcel, 10].Style.Font.Size = 12;

                                        incRowExcel++;

                                        worksheet.Cells[incRowExcel, 4].Value = "SELLING AGENT " + _host.Get_CompanyName();
                                        worksheet.Cells[incRowExcel, 4, incRowExcel, 10].Merge = true;
                                        worksheet.Cells[incRowExcel, 4, incRowExcel, 10].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 4, incRowExcel, 10].Style.Font.Size = 12;

                                        incRowExcel = incRowExcel + 2;

                                        worksheet.Cells[incRowExcel, 4].Value = "Periode: " + Convert.ToDateTime(_unitRegistryRpt.ValueDateFrom).ToShortDateString() + " s/d " + Convert.ToDateTime(_unitRegistryRpt.ValueDateTo).ToShortDateString();
                                        worksheet.Cells[incRowExcel, 4, incRowExcel, 12].Merge = true;
                                        worksheet.Cells[incRowExcel, 4, incRowExcel, 12].Style.Font.Size = 9;
                                        worksheet.Cells[incRowExcel, 4, incRowExcel, 8].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                                        incRowExcel = incRowExcel + 2;

                                        worksheet.Cells[incRowExcel, 1].Value = "REDEMPTION REPORT";
                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Merge = true;
                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.UnderLine = true;
                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Size = 11;

                                        incRowExcel = incRowExcel + 2;


                                        //ATUR DATA GROUPINGNYA DULU
                                        List<RedemptionRpt> rList = new List<RedemptionRpt>();
                                        while (dr0.Read())
                                        {
                                            RedemptionRpt rSingle = new RedemptionRpt();
                                            rSingle.TransactionDate = dr0["ValueDate"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["ValueDate"]);
                                            rSingle.ID = dr0["ID"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr0["ID"]);
                                            rSingle.InvestorName = dr0["Name"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["Name"]);
                                            rSingle.AmountRedeem = dr0["CashAmount"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["CashAmount"]);
                                            rSingle.UnitRedeem = dr0["UnitAmount"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["UnitAmount"]);
                                            rSingle.RedemptionFeePercent = dr0["RedemptionFeePercent"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["RedemptionFeePercent"]);
                                            rSingle.RedemptionFeeAmount = dr0["RedemptionFeeAmount"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["RedemptionFeeAmount"]);
                                            rSingle.NettAmount = dr0["TotalCashAmount"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["TotalCashAmount"]);
                                            rSingle.NAV = dr0["NAV"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["NAV"]);
                                            rSingle.Units = dr0["TotalUnitAmount"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["TotalUnitAmount"]);
                                            rSingle.PaymentDate = dr0["PaymentDate"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["PaymentDate"]);
                                            rSingle.Fund = dr0["FundName"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["FundName"]);
                                            rSingle.BeneficiaryBank = dr0["BeneficiaryBank"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["BeneficiaryBank"]);
                                            rSingle.BitRedemptionAllDesc = Convert.ToString(dr0["BitRedemptionAllDesc"]);
                                            rSingle.AgentName = dr0["AgentName"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["AgentName"]);
                                            rList.Add(rSingle);

                                        }

                                        var QueryByFundID =
                                         from r in rList
                                         group r by new { r.Fund } into rGroup
                                         select rGroup;


                                        foreach (var rsHeader in QueryByFundID)
                                        {
                                            worksheet.Cells[incRowExcel, 1].Value = "FUND : ";
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 1].Merge = true;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 1].Style.Font.Size = 10;
                                            worksheet.Cells[incRowExcel, 2].Value = rsHeader.Key.Fund;
                                            worksheet.Cells[incRowExcel, 2, incRowExcel, 12].Merge = true;
                                            worksheet.Cells[incRowExcel, 2, incRowExcel, 12].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 2, incRowExcel, 12].Style.Font.Size = 10;

                                            incRowExcel = incRowExcel + 2;

                                            worksheet.Cells[incRowExcel, 1].Value = "NO";
                                            worksheet.Cells[incRowExcel, 2].Value = "TGL TRANSAKSI";
                                            worksheet.Cells[incRowExcel, 2].Style.WrapText = true;
                                            worksheet.Cells[incRowExcel, 3].Value = "CUSTOMER ID";
                                            worksheet.Cells[incRowExcel, 3].Style.WrapText = true;
                                            worksheet.Cells[incRowExcel, 4].Value = "NAMA INVESTOR";
                                            worksheet.Cells[incRowExcel, 5].Style.WrapText = true;
                                            worksheet.Cells[incRowExcel, 5].Value = "AMOUNT REDEEM";
                                            worksheet.Cells[incRowExcel, 6].Style.WrapText = true;
                                            worksheet.Cells[incRowExcel, 6].Value = "UNIT REDEEM";
                                            worksheet.Cells[incRowExcel, 7].Value = "FEE (%)";
                                            worksheet.Cells[incRowExcel, 8].Value = "FEE";
                                            worksheet.Cells[incRowExcel, 8].Style.WrapText = true;
                                            worksheet.Cells[incRowExcel, 9].Style.WrapText = true;
                                            worksheet.Cells[incRowExcel, 9].Value = "NETT AMOUNT";
                                            worksheet.Cells[incRowExcel, 10].Value = "NAV";
                                            worksheet.Cells[incRowExcel, 11].Value = "UNITS";
                                            worksheet.Cells[incRowExcel, 12].Style.WrapText = true;
                                            worksheet.Cells[incRowExcel, 12].Value = "REDEMPTION ALL";
                                            worksheet.Cells[incRowExcel, 13].Style.WrapText = true;
                                            worksheet.Cells[incRowExcel, 13].Value = "TANGGAL PEMBAYARAN";
                                            worksheet.Cells[incRowExcel, 14].Style.WrapText = true;
                                            worksheet.Cells[incRowExcel, 14].Value = "BENEFICIARY BANK ACCOUNT";
                                            worksheet.Cells[incRowExcel, 15].Style.WrapText = true;
                                            worksheet.Cells[incRowExcel, 15].Value = "AGENT NAME";

                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 15].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 15].Style.Font.Size = 8;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 15].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 15].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(251, 228, 213));
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 15].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 15].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 15].Style.Border.Top.Style = ExcelBorderStyle.Double;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 15].Style.Border.Bottom.Style = ExcelBorderStyle.Double;
                                            worksheet.Cells["A" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells["O" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                                            incRowExcel = incRowExcel + 1;

                                            int first = incRowExcel;

                                            int no = 1;

                                            foreach (var rsDetail in rsHeader)
                                            {
                                                worksheet.Cells[incRowExcel, 1].Value = no;
                                                worksheet.Cells[incRowExcel, 1].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 1].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 2].Style.Numberformat.Format = "dd-MMM-yyyy";
                                                worksheet.Cells[incRowExcel, 2].Value = Convert.ToDateTime(rsDetail.TransactionDate);
                                                worksheet.Cells[incRowExcel, 2, incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 2, incRowExcel, 2].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.ID;
                                                worksheet.Cells[incRowExcel, 3, incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 3, incRowExcel, 3].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 4].Value = rsDetail.InvestorName;
                                                worksheet.Cells[incRowExcel, 4, incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                worksheet.Cells[incRowExcel, 4, incRowExcel, 4].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 4].Style.WrapText = true;
                                                worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 5].Value = rsDetail.AmountRedeem;
                                                worksheet.Cells[incRowExcel, 5, incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                worksheet.Cells[incRowExcel, 5, incRowExcel, 5].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 6].Value = rsDetail.UnitRedeem;
                                                worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.0000";
                                                worksheet.Cells[incRowExcel, 6, incRowExcel, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                worksheet.Cells[incRowExcel, 6, incRowExcel, 6].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 7].Value = rsDetail.RedemptionFeePercent;
                                                worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "###0.00";
                                                worksheet.Cells[incRowExcel, 7, incRowExcel, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 7, incRowExcel, 7].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 8].Value = rsDetail.RedemptionFeeAmount;
                                                worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 8, incRowExcel, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                worksheet.Cells[incRowExcel, 8, incRowExcel, 8].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 9].Value = rsDetail.NettAmount;
                                                worksheet.Cells[incRowExcel, 9, incRowExcel, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                worksheet.Cells[incRowExcel, 9, incRowExcel, 9].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 10].Value = rsDetail.NAV;
                                                worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0.0000";
                                                worksheet.Cells[incRowExcel, 10, incRowExcel, 10].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                worksheet.Cells[incRowExcel, 10, incRowExcel, 10].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 11].Value = rsDetail.Units;
                                                worksheet.Cells[incRowExcel, 11].Style.Numberformat.Format = "#,##0.0000";
                                                worksheet.Cells[incRowExcel, 11, incRowExcel, 11].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                worksheet.Cells[incRowExcel, 11, incRowExcel, 11].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 12].Value = rsDetail.BitRedemptionAllDesc;
                                                worksheet.Cells[incRowExcel, 12, incRowExcel, 12].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 12, incRowExcel, 12].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 13].Style.Numberformat.Format = "dd-MMM-yyyy";
                                                worksheet.Cells[incRowExcel, 13].Value = Convert.ToDateTime(rsDetail.PaymentDate);
                                                worksheet.Cells[incRowExcel, 13, incRowExcel, 13].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 13, incRowExcel, 13].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 14].Value = rsDetail.BeneficiaryBank;
                                                worksheet.Cells[incRowExcel, 14].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells[incRowExcel, 14].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                worksheet.Cells[incRowExcel, 14].Style.WrapText = true;
                                                worksheet.Cells[incRowExcel, 15].Value = rsDetail.AgentName;
                                                worksheet.Cells[incRowExcel, 15].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells[incRowExcel, 15].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                worksheet.Cells[incRowExcel, 15].Style.WrapText = true;

                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 15].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 15].Style.Font.Size = 8;

                                                incRowExcel++;
                                                no++;
                                            }

                                            int last = incRowExcel - 1;

                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 14].Style.Border.Top.Style = ExcelBorderStyle.Double;


                                            //incRowExcel++;

                                            worksheet.Cells[incRowExcel, 4].Value = "Total";
                                            worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.00";
                                            worksheet.Cells[incRowExcel, 5].Formula = "SUM(E" + first.ToString() + ":E" + last.ToString() + ")";
                                            worksheet.Cells[incRowExcel, 5].Calculate();
                                            worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0.00";
                                            worksheet.Cells[incRowExcel, 8].Formula = "SUM(H" + first.ToString() + ":H" + last.ToString() + ")";
                                            worksheet.Cells[incRowExcel, 8].Calculate();
                                            worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0.00";
                                            worksheet.Cells[incRowExcel, 9].Formula = "SUM(I" + first.ToString() + ":I" + last.ToString() + ")";
                                            worksheet.Cells[incRowExcel, 9].Calculate();
                                            worksheet.Cells[incRowExcel, 11].Style.Numberformat.Format = "#,##0.0000";
                                            worksheet.Cells[incRowExcel, 11].Formula = "SUM(K" + first.ToString() + ":K" + last.ToString() + ")";
                                            worksheet.Cells[incRowExcel, 11].Calculate();

                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 15].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 15].Style.Font.Size = 8;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 15].Style.VerticalAlignment = ExcelVerticalAlignment.Center;


                                            //mekel
                                            incRowExcel++;

                                            incRowExcel = incRowExcel + 2;
                                        }

                                        incRowExcel = incRowExcel + 4;

                                        worksheet.Cells[incRowExcel, 1].Value = "Disclaimer : ";
                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Merge = true;
                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Size = 12;

                                        incRowExcel++;

                                        worksheet.Cells[incRowExcel, 1].Value = "Dokumen ini dipersiapkan oleh PT SUCOR SEKURITAS dan hanya bisa digunakan untuk kepentingan investor tersebut diatas dan tidak untuk pihak lainnya. Laporan ini bukan merupakan konfirmasi dari PT SUCOR SEKURITAS dan tidak untuk menggantikan laporan yang wajib diterbitkan oleh Bank Kustodian, jika ada perbedaan antara laporan ini dengan laporan Bank Kustodian, maka laporan Bank Kustodian adalah yang benar. Laporan ini diproses oleh computer dan tidak memerlukan tandatangan.";
                                        worksheet.Cells[incRowExcel, 1, incRowExcel + 2, 15].Style.Font.Size = 11;
                                        worksheet.Cells[incRowExcel, 1, incRowExcel + 2, 15].Merge = true;
                                        worksheet.Cells[incRowExcel, 1, incRowExcel + 2, 15].Style.WrapText = true;

                                        incRowExcel = incRowExcel + 4;

                                        worksheet.Cells[incRowExcel, 1].Value = "PT SUCOR SEKURITAS terdaftar dan diawasi oleh OJK, dengan nomor registrasi KEP-01/PM/MI/1999";
                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 15].Style.Font.Size = 11;
                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 15].Merge = true;

                                        incRowExcel = incRowExcel + 2;

                                        worksheet.PrinterSettings.FitToPage = true;
                                        worksheet.PrinterSettings.FitToWidth = 1;
                                        worksheet.PrinterSettings.FitToHeight = 0;
                                        worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel - 1, 15];
                                        worksheet.Column(1).Width = 7;
                                        worksheet.Column(2).Width = 15;
                                        worksheet.Column(3).Width = 10;
                                        worksheet.Column(4).Width = 25;
                                        worksheet.Column(5).Width = 15;
                                        worksheet.Column(6).Width = 15;
                                        worksheet.Column(7).Width = 10;
                                        worksheet.Column(8).Width = 10;
                                        worksheet.Column(9).Width = 15;
                                        worksheet.Column(10).Width = 10;
                                        worksheet.Column(11).Width = 15;
                                        worksheet.Column(12).Width = 10;
                                        worksheet.Column(13).Width = 15;
                                        worksheet.Column(14).Width = 25;
                                        worksheet.Column(15).Width = 25;

                                        // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                        //worksheet.Cells.AutoFitColumns(); // UNTUK AUTO FIT UKURAN TULISAN DENGAN KOLOM, KEBETULAN INI BUAT HEADER AJA DIPAKENYA
                                        //worksheet.PrinterSettings.FitToPage = true;
                                        worksheet.PrinterSettings.Orientation = eOrientation.Landscape; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                        worksheet.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN
                                        worksheet.HeaderFooter.OddHeader.RightAlignedText = "&12 Redemption REPORT";

                                        // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                        //ExcelWorksheet ws = package.Workbook.Worksheets.Add("Demo");

                                        worksheet.PrinterSettings.TopMargin = (decimal).3 / 2.54M; // narrow border
                                        worksheet.PrinterSettings.RightMargin = (decimal).3 / 2.54M; //narrow borderExcelWorksheet ws = pck.Workbook.Worksheets.Add("Demo");
                                        worksheet.PrinterSettings.LeftMargin = (decimal).3 / 2.54M; // narrow border

                                        //ws.PrinterSettings.TopMargin = (decimal).5 / 2.54M; // narrow border
                                        //ws.PrinterSettings.RightMargin = (decimal).5 / 2.54M; //narrow border
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

                                        //worksheet.HeaderFooter.OddFooter.CenteredText = Tools.DefaultReportFooterCenterText();
                                        worksheet.HeaderFooter.OddFooter.LeftAlignedText = Tools.DefaultReportFooterLeftText();

                                        package.Save();
                                        if (_unitRegistryRpt.DownloadMode == "PDF")
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

            #region Daily Transaction Blotter Switching
            else if (_unitRegistryRpt.ReportName.Equals("Daily Transaction Blotter Switching"))
            {
                try
                {
                    using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                    {
                        DbCon.Open();
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            string _statusSwitching = "";
                            string _paramFund = "";
                            string _paramFundClient = "";
                            string _paramAgent = "";

                            if (!_host.findString(_unitRegistryRpt.FundFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_unitRegistryRpt.FundFrom))
                            {
                                _paramFund = "And C.FundPK in ( " + _unitRegistryRpt.FundFrom + " ) ";
                            }
                            else
                            {
                                _paramFund = "";
                            }
                            if (!_host.findString(_unitRegistryRpt.FundClientFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_unitRegistryRpt.FundClientFrom))
                            {
                                _paramFundClient = "And A.FundClientPK in  ( " + _unitRegistryRpt.FundClientFrom + " ) ";
                            }
                            else
                            {
                                _paramFundClient = "";
                            }
                            if (!_host.findString(_unitRegistryRpt.AgentFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_unitRegistryRpt.AgentFrom))
                            {
                                _paramAgent = "And A.AgentPK  in  ( " + _unitRegistryRpt.AgentFrom + " ) ";
                            }
                            else
                            {
                                _paramAgent = "";
                            }

                            if (_unitRegistryRpt.Status == 1)
                            {
                                _statusSwitching = "  A.Status = 2 and A.Posted = 1 and A.Revised = 0 ";
                                //_statusRedemp = "  CR.Status = 2 and CR.Posted = 1 and CR.Revised = 0 ";
                            }
                            else if (_unitRegistryRpt.Status == 2)
                            {
                                _statusSwitching = "  A.Status = 2 and A.Posted = 1 and A.Revised = 1 ";
                                //_statusRedemp = "  CR.Status = 2 and CR.Posted = 1 and CR.Revised = 1 ";
                            }
                            else if (_unitRegistryRpt.Status == 3)
                            {
                                _statusSwitching = "  A.Status = 2 and A.Posted = 0 and A.Revised = 0 ";
                                //_statusRedemp = "  CR.Status = 2 and CR.Posted = 0 and CR.Revised = 0 ";
                            }
                            else if (_unitRegistryRpt.Status == 4)
                            {
                                _statusSwitching = "  A.Status = 1  ";
                                //_statusRedemp = "  CR.Status = 1  ";
                            }
                            else if (_unitRegistryRpt.Status == 5)
                            {
                                _statusSwitching = "  A.Status = 3  ";
                                //_statusRedemp = "  CR.Status = 3  ";
                            }
                            else if (_unitRegistryRpt.Status == 6)
                            {
                                _statusSwitching = "  (A.Status = 2 or A.Posted = 1) and A.Revised = 0  ";
                                //_statusRedemp = "  (CR.Status = 2 or CR.Posted = 1) and CR.Revised = 0  ";
                            }
                            else if (_unitRegistryRpt.Status == 7)
                            {
                                _statusSwitching = "  (A.Status = 1 Or A.Status = 2 or A.Posted = 1) and  A.Revised = 0  ";
                                //_statusRedemp = "  (CR.Status = 1 Or CR.Status = 2 or CR.Posted = 1) and CR.Revised = 0  ";
                            }


                            cmd.CommandText =

                            @"
                            SELECT 'Switching' Type,case when A.BitSwitchingAll = 1 then 'Y' else '' End BitSwitchingAllDesc,ClientSwitchingPK,D.ContactPerson ContactPerson,D.ID BankBranchID,D.Fax1 FaxNo,ValueDate Date,NavDate NavDate, C.ID FundIDFrom,C1.ID FundIDTo, C.Name FundNameFrom,C1.Name FundNameTo,B.ID ClientID, 
                            C.FundPK FundPK,B.Name ClientName,A.NAVFundFrom NAVFundFrom,A.NAVFundTo NAVFundTo,CashAmount CashFundFrom,unitamount UnitAmount, A.SwitchingFeePercent FeePercent,A.SwitchingFeeAmount FeeAmount, 
                            A.PaymentDate SettlementDate,A.Description Remark,A.TotalUnitAmountFundFrom TotalUnitFundFrom,A.TotalCashAmountFundFrom TotalCashFundFrom,A.TotalCashAmountFundTo TotalCashFundTo,B.NPWP NPWP,  E.Name Agent,F.Name Mgt  from ClientSwitching A    
                            left join FundClient B ON B.fundclientpk = A.fundclientpk and B.status = 2   
                            left join Fund C ON A.FundPKFrom = C.FundPK  and C.status = 2  
                            left join Fund C1 ON A.FundPKTo = C1.FundPK  and C1.status = 2     
                            left join BankBranch D ON C.BankBranchPK = D.BankBranchPK  and D.status = 2
                            left join Agent E on B.SellingAgentPK = E.AgentPK and E.Status = 2
                            left join Agent F on E.ParentPK = F.AgentPK and F.Status = 2   
                            WHERE   " + _statusSwitching + _paramFund + _paramFundClient + _paramAgent +
                            @"and NAVDate between @DateFrom and @DateTo 
                            order by C.FundPK,B.Name,E.Name";
                            cmd.CommandTimeout = 0;
                            cmd.Parameters.AddWithValue("@DateFrom", _unitRegistryRpt.ValueDateFrom);
                            cmd.Parameters.AddWithValue("@DateTo", _unitRegistryRpt.ValueDateTo);
                            cmd.Parameters.AddWithValue("@FundFrom", _unitRegistryRpt.FundFrom);
                            cmd.Parameters.AddWithValue("@FundClientFrom", _unitRegistryRpt.FundClientFrom);
                            cmd.Parameters.AddWithValue("@AgentFrom", _unitRegistryRpt.AgentFrom);


                            using (SqlDataReader dr0 = cmd.ExecuteReader())
                            {
                                if (!dr0.HasRows)
                                {
                                    return false;
                                }
                                else
                                {
                                    string filePath = Tools.ReportsPath + "DailyTransactionBlotterSwitching" + "_" + _userID + ".xlsx";
                                    string pdfPath = Tools.ReportsPath + "DailyTransactionBlotterSwitching" + "_" + _userID + ".pdf";
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

                                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Daily Transaction Blotter Switching");


                                        //ATUR DATA GROUPINGNYA DULU
                                        List<UnitRegistryRpt> rList = new List<UnitRegistryRpt>();
                                        while (dr0.Read())
                                        {
                                            UnitRegistryRpt rSingle = new UnitRegistryRpt();
                                            rSingle.ClientSwitchingPK = Convert.ToInt32(dr0["ClientSwitchingPK"]);
                                            rSingle.FundIDFrom = Convert.ToString(dr0["FundIDFrom"]);
                                            rSingle.FundIDTo = Convert.ToString(dr0["FundIDTo"]);
                                            rSingle.FundNameFrom = Convert.ToString(dr0["FundNameFrom"]);
                                            rSingle.FundPK = Convert.ToInt32(dr0["FundPK"]);
                                            rSingle.FundNameTo = Convert.ToString(dr0["FundNameTo"]);
                                            rSingle.NAVDate = Convert.ToDateTime(dr0["NAVDate"]);
                                            rSingle.NAVFundFrom = Convert.ToDecimal(dr0["NAVFundFrom"]);
                                            rSingle.NAVFundTo = Convert.ToDecimal(dr0["NAVFundTo"]);
                                            rSingle.ClientID = Convert.ToString(dr0["ClientID"]);
                                            rSingle.ClientName = Convert.ToString(dr0["ClientName"]);
                                            rSingle.TotalUnitFundFrom = Convert.ToDecimal(dr0["TotalUnitFundFrom"]);
                                            rSingle.CashFundFrom = Convert.ToDecimal(dr0["CashFundFrom"]);
                                            rSingle.FeePercent = Convert.ToDecimal(dr0["FeePercent"]);
                                            rSingle.FeeAmount = Convert.ToDecimal(dr0["FeeAmount"]);
                                            rSingle.TotalCashFundFrom = Convert.ToDecimal(dr0["TotalCashFundFrom"]);
                                            rSingle.TotalCashFundTo = Convert.ToDecimal(dr0["TotalCashFundTo"]);
                                            rSingle.Remark = Convert.ToString(dr0["Remark"]);
                                            rSingle.ContactPerson = Convert.ToString(dr0["ContactPerson"]);
                                            rSingle.FaxNo = Convert.ToString(dr0["FaxNo"]);
                                            rSingle.BankCustodiID = Convert.ToString(dr0["BankBranchID"]);
                                            rSingle.NPWP = Convert.ToString(dr0["NPWP"]);
                                            rSingle.Agent = Convert.ToString(dr0["Agent"]);
                                            rSingle.Mgt = Convert.ToString(dr0["Mgt"]);
                                            rSingle.BitSwitchingAllDesc = Convert.ToString(dr0["BitSwitchingAllDesc"]);
                                            rList.Add(rSingle);

                                        }


                                        var QueryByFundID =
                                            from r in rList
                                            group r by new { r.FundNameFrom, r.NAVDate, r.ContactPerson, r.FaxNo, r.BankCustodiID, r.FundPK } into rGroup
                                            select rGroup;

                                        int incRowExcel = 1;


                                        foreach (var rsHeader in QueryByFundID)
                                        {


                                            incRowExcel = incRowExcel + 2;

                                            //incRowExcel = incRowExcel + 2;

                                            int RowZ = incRowExcel;
                                            //incRowExcel = incRowExcel + 1;
                                            //worksheet.Cells["A" + incRowExcel + ":L" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells[incRowExcel, 1].Value = _host.Get_CompanyName();
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Size = 24;
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells["A" + incRowExcel + ":G" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 9].Value = "To ";
                                            worksheet.Cells[incRowExcel, 9].Style.Font.Size = 14;
                                            worksheet.Cells[incRowExcel, 10].Value = ": ";
                                            worksheet.Cells[incRowExcel, 10].Style.Font.Size = 14;
                                            worksheet.Cells[incRowExcel, 11].Value = rsHeader.Key.BankCustodiID;
                                            worksheet.Cells[incRowExcel, 11].Style.Font.Size = 14;
                                            worksheet.Cells["K" + incRowExcel + ":L" + incRowExcel].Merge = true;
                                            //worksheet.Cells[incRowExcel, 8].Value = "Date ";
                                            //worksheet.Cells[incRowExcel, 9].Value = ": ";
                                            //worksheet.Cells[incRowExcel, 10].Value = rsHeader.Key.Date;
                                            //worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "dd-MMMM-yyyy";
                                            //worksheet.Cells[incRowExcel, 10].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            //worksheet.Cells["J" + incRowExcel + ":L" + incRowExcel].Merge = true;

                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Value = "Gd. Equity Tower Lt. 31";
                                            worksheet.Cells["A" + incRowExcel + ":E" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 11].Value = "Custodian Services";
                                            worksheet.Cells["K" + incRowExcel + ":L" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 11].Style.Font.Size = 14;
                                            //worksheet.Cells[incRowExcel, 4, incRowExcel, 10].Merge = true;
                                            //worksheet.Cells[incRowExcel, 8].Value = "To ";
                                            //worksheet.Cells[incRowExcel, 9].Value = ": ";
                                            //worksheet.Cells[incRowExcel, 10].Value = rsHeader.Key.BankCustodiID;
                                            //worksheet.Cells["K" + incRowExcel + ":L" + incRowExcel].Merge = true;
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Value = "Jl. Jendral Sudirman kav 52-53, Jakarta 12910";
                                            worksheet.Cells["A" + incRowExcel + ":E" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 9].Value = "Attention ";
                                            worksheet.Cells[incRowExcel, 10].Value = ": ";
                                            worksheet.Cells[incRowExcel, 11].Value = rsHeader.Key.ContactPerson;
                                            worksheet.Cells["K" + incRowExcel + ":L" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 11].Style.Font.Size = 14;
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 9].Value = "Fax ";
                                            worksheet.Cells[incRowExcel, 10].Value = ": ";
                                            worksheet.Cells[incRowExcel, 11].Value = _host.Get_CompanyFax();
                                            worksheet.Cells["K" + incRowExcel + ":L" + incRowExcel].Merge = true;
                                            //worksheet.Cells[incRowExcel, 1].Value = "Jakarta 12710, Indonesia";
                                            //worksheet.Cells["A" + incRowExcel + ":E" + incRowExcel].Merge = true;
                                            //worksheet.Cells[incRowExcel, 8].Value = "Fax Number ";
                                            //worksheet.Cells[incRowExcel, 9].Value = ": ";
                                            //worksheet.Cells[incRowExcel, 10].Value = rsHeader.Key.FaxNo;
                                            //worksheet.Cells["J" + incRowExcel + ":L" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 1].Value = "Tel : ";
                                            //worksheet.Cells[incRowExcel, 2].Value = ": ";
                                            worksheet.Cells[incRowExcel, 2].Value = _host.Get_CompanyPhone();
                                            worksheet.Cells["B" + incRowExcel + ":B" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 3].Value = "Fax : ";
                                            //worksheet.Cells[incRowExcel, 6].Value = ": ";
                                            worksheet.Cells[incRowExcel, 4].Value = _host.Get_CompanyFax();
                                            worksheet.Cells["D" + incRowExcel + ":D" + incRowExcel].Merge = true;
                                            incRowExcel++;
                                            incRowExcel = incRowExcel + 1;
                                            worksheet.Cells[incRowExcel, 5].Value = "SWITCHING BATCH FORM";
                                            //worksheet.Cells[incRowExcel, 5].Style.Font.Size = 65;
                                            worksheet.Cells[incRowExcel, 5].Style.Font.Bold = true;
                                            worksheet.Cells["E" + incRowExcel + ":H" + incRowExcel].Merge = true;
                                            worksheet.Cells["E" + incRowExcel + ":H" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            incRowExcel = incRowExcel + 2;

                                            //worksheet.Cells[incRowExcel, 1].Value = "Please kindly receive our Subscription of unit holders as follow : ";
                                            //worksheet.Cells["A" + incRowExcel + ":F" + incRowExcel].Merge = true;
                                            //incRowExcel++;
                                            //Row A = 2
                                            int RowA = incRowExcel;

                                            worksheet.Cells[incRowExcel, 1].Value = "Fund Name ";
                                            worksheet.Cells["A" + incRowExcel + ":B" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 3].Value = ":";
                                            worksheet.Cells[incRowExcel, 4].Value = rsHeader.Key.FundNameFrom;
                                            worksheet.Cells[incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            worksheet.Cells[incRowExcel, 4].Style.Font.Bold = true;
                                            worksheet.Cells["D" + incRowExcel + ":F" + incRowExcel].Merge = true;
                                            //worksheet.Cells[incRowExcel, 9].Value = "Page   : 1 ";
                                            //worksheet.Cells["I" + incRowExcel + ":J" + incRowExcel].Merge = true;
                                            //worksheet.Cells[incRowExcel, 11].Value = "BATCH  : ALL ";
                                            //worksheet.Cells["K" + incRowExcel + ":L" + incRowExcel].Merge = true;
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Value = "NAV Date ";
                                            worksheet.Cells["A" + incRowExcel + ":B" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 3].Value = ": ";
                                            worksheet.Cells[incRowExcel, 4].Value = rsHeader.Key.NAVDate;
                                            worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "dd-MMMM-yyyy";
                                            worksheet.Cells[incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            worksheet.Cells["D" + incRowExcel + ":D" + incRowExcel].Merge = true;
                                            //worksheet.Cells[incRowExcel, 9].Value = "Ref#   : 0001/MAXI FUND/IX/2016 - Regular ";
                                            //worksheet.Cells["I" + incRowExcel + ":K" + incRowExcel].Merge = true;
                                            incRowExcel++;


                                            incRowExcel = incRowExcel + 1;

                                            //Row B = 3
                                            int RowB = incRowExcel;
                                            int RowG = incRowExcel + 1;

                                            //worksheet.Row(RowB).Height = 100;
                                            //worksheet.Row(RowG).Height = 100;

                                            worksheet.Cells[incRowExcel, 1].Value = "NO";
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells["A" + RowB + ":A" + RowG].Merge = true;
                                            worksheet.Cells["A" + RowB + ":A" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["A" + RowB + ":A" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 2].Value = "CUSTOMER ID";
                                            worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 2].Style.WrapText = true;
                                            worksheet.Cells["B" + RowB + ":B" + RowG].Merge = true;
                                            worksheet.Cells["B" + RowB + ":B" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["B" + RowB + ":B" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 3].Value = "NAME";
                                            worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;
                                            worksheet.Cells["C" + RowB + ":D" + RowG].Merge = true;
                                            worksheet.Cells["C" + RowB + ":D" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["C" + RowB + ":D" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 5].Value = "SWITCH FROM";
                                            worksheet.Cells[incRowExcel, 5].Style.Font.Bold = true;
                                            worksheet.Cells["E" + RowB + ":K" + RowB].Merge = true;
                                            worksheet.Cells["E" + RowB + ":J" + RowB].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["E" + RowB + ":J" + RowB].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[RowG, 5].Value = "FUND";
                                            worksheet.Cells[RowG, 5].Style.Font.Bold = true;
                                            worksheet.Cells["E" + RowG + ":E" + RowG].Merge = true;
                                            worksheet.Cells["E" + RowG + ":E" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["E" + RowG + ":E" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[RowG, 6].Value = "NO. OF UNITS";
                                            worksheet.Cells[RowG, 6].Style.Font.Bold = true;
                                            worksheet.Cells["F" + RowG + ":F" + RowG].Merge = true;
                                            worksheet.Cells["F" + RowG + ":F" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["F" + RowG + ":F" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[RowG, 7].Value = "NOMINAL AMOUNT";
                                            worksheet.Cells[RowG, 7].Style.Font.Bold = true;
                                            worksheet.Cells["G" + RowG + ":G" + RowG].Merge = true;
                                            worksheet.Cells["G" + RowG + ":G" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["G" + RowG + ":G" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[RowG, 8].Value = "% FEE";
                                            worksheet.Cells[RowG, 8].Style.Font.Bold = true;
                                            worksheet.Cells["H" + RowG + ":H" + RowG].Merge = true;
                                            worksheet.Cells["H" + RowG + ":H" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["H" + RowG + ":H" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[RowG, 9].Value = "AMOUNT FEE";
                                            worksheet.Cells[RowG, 9].Style.Font.Bold = true;
                                            worksheet.Cells["I" + RowG + ":I" + RowG].Merge = true;
                                            worksheet.Cells["I" + RowG + ":I" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["I" + RowG + ":I" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[RowG, 10].Value = "NET AMOUNT";
                                            worksheet.Cells[RowG, 10].Style.Font.Bold = true;
                                            worksheet.Cells["J" + RowG + ":K" + RowG].Merge = true;
                                            worksheet.Cells["J" + RowG + ":K" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["J" + RowG + ":K" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 12].Value = "SWITCH TO";
                                            worksheet.Cells[incRowExcel, 12].Style.Font.Bold = true;
                                            worksheet.Cells["L" + RowB + ":M" + RowB].Merge = true;
                                            worksheet.Cells["L" + RowB + ":L" + RowB].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["L" + RowB + ":L" + RowB].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[RowG, 12].Value = "FUND";
                                            worksheet.Cells[RowG, 12].Style.Font.Bold = true;
                                            worksheet.Cells["L" + RowG + ":L" + RowG].Merge = true;
                                            worksheet.Cells["L" + RowG + ":L" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["L" + RowG + ":L" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[RowG, 13].Value = "NET AMOUNT";
                                            worksheet.Cells[RowG, 13].Style.Font.Bold = true;
                                            worksheet.Cells["M" + RowG + ":M" + RowG].Merge = true;
                                            worksheet.Cells["M" + RowG + ":M" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["M" + RowG + ":M" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 14].Value = "ALL UNIT";
                                            worksheet.Cells[incRowExcel, 14].Style.Font.Bold = true;
                                            worksheet.Cells["N" + RowB + ":N" + RowG].Merge = true;
                                            worksheet.Cells["N" + RowB + ":N" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["N" + RowB + ":N" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[RowB, 1, RowG, 14].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                            worksheet.Cells[RowB, 1, RowG, 14].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(251, 228, 213));

                                            incRowExcel++;

                                            // Row C = 4
                                            int RowC = incRowExcel;

                                            incRowExcel++;
                                            //area header

                                            int _no = 1;
                                            int _startRowDetail = incRowExcel;
                                            int _endRowDetail = 0;
                                            int hight = 50;
                                            foreach (var rsDetail in rsHeader)
                                            {
                                                //Row D = 5
                                                int RowD = incRowExcel;
                                                int RowE = incRowExcel + 1;
                                                int RowH = RowB + 2;
                                                int RowI = RowB + 3;


                                                //ThickBox Border

                                                worksheet.Cells["A" + RowB + ":N" + RowG].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + RowB + ":N" + RowG].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + RowB + ":I" + RowG].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["J" + RowB + ":N" + RowG].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + RowB + ":N" + RowG].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + incRowExcel + ":N" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + incRowExcel + ":N" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Medium;


                                                //area detail
                                                worksheet.Cells[incRowExcel, 1].Value = _no;
                                                worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 2].Value = rsDetail.ClientID;
                                                worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.ClientName;
                                                worksheet.Cells[incRowExcel, 3, incRowExcel, 4].Merge = true;
                                                worksheet.Cells[incRowExcel, 3, incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                worksheet.Cells[incRowExcel, 5].Value = rsDetail.FundIDFrom;
                                                worksheet.Cells[incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 6].Value = rsDetail.TotalUnitFundFrom;
                                                worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.0000";
                                                worksheet.Cells[incRowExcel, 7].Value = rsDetail.CashFundFrom;
                                                worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 8].Value = rsDetail.FeePercent;
                                                worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 9].Value = rsDetail.FeeAmount;
                                                worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 10].Value = rsDetail.TotalCashFundFrom;
                                                worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 10, incRowExcel, 11].Merge = true;
                                                worksheet.Cells[incRowExcel, 10, incRowExcel, 11].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                worksheet.Cells[incRowExcel, 12].Value = rsDetail.FundIDTo;
                                                worksheet.Cells[incRowExcel, 12].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 13].Value = rsDetail.TotalCashFundTo;
                                                worksheet.Cells[incRowExcel, 13].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 14].Value = rsDetail.BitSwitchingAllDesc;
                                                worksheet.Cells[incRowExcel, 14].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                _endRowDetail = incRowExcel;
                                                _no++;
                                                incRowExcel++;

                                            }


                                            worksheet.Cells["A" + _endRowDetail + ":N" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            //worksheet.Cells["A" + _endRowDetail + ":N" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            //worksheet.Cells["A" + _endRowDetail + ":A" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Medium;



                                            //worksheet.Cells[incRowExcel, 5].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            //worksheet.Cells[incRowExcel, 5].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells[incRowExcel, 6].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells[incRowExcel, 6].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells[incRowExcel, 6].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells[incRowExcel, 6].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells[incRowExcel, 7].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells[incRowExcel, 7].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells[incRowExcel, 7].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells[incRowExcel, 7].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells[incRowExcel, 8].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells[incRowExcel, 9].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells[incRowExcel, 9].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells[incRowExcel, 9].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells[incRowExcel, 9].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells[incRowExcel, 10].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells[incRowExcel, 11].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells[incRowExcel, 11].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells[incRowExcel, 11].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells[incRowExcel, 12].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells[incRowExcel, 13].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells[incRowExcel, 13].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells[incRowExcel, 13].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells[incRowExcel, 13].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells[incRowExcel, 14].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells[incRowExcel, 14].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells[incRowExcel, 4].Value = "Total :";
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells[incRowExcel, 1].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells[incRowExcel, 6].Formula = "SUM(F" + _startRowDetail + ":F" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.0000";
                                            worksheet.Cells[incRowExcel, 7].Formula = "SUM(G" + _startRowDetail + ":G" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0";
                                            worksheet.Cells[incRowExcel, 9].Formula = "SUM(I" + _startRowDetail + ":I" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0";
                                            worksheet.Cells[incRowExcel, 11].Formula = "SUM(J" + _startRowDetail + ":J" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 11].Style.Numberformat.Format = "#,##0";
                                            worksheet.Cells[incRowExcel, 13].Formula = "SUM(M" + _startRowDetail + ":M" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 13].Style.Numberformat.Format = "#,##0";
                                            worksheet.Cells[incRowExcel, 6].Calculate();
                                            worksheet.Cells[incRowExcel, 7].Calculate();
                                            worksheet.Cells[incRowExcel, 9].Calculate();
                                            worksheet.Cells[incRowExcel, 11].Calculate();
                                            worksheet.Cells[incRowExcel, 13].Calculate();
                                            worksheet.Cells[incRowExcel, 6].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 7].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 9].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 11].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 13].Style.Font.Bold = true;

                                            incRowExcel = incRowExcel + 2;
                                            worksheet.Cells[incRowExcel, 1].Value = "Regards, ";
                                            worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                                            incRowExcel = incRowExcel + 4;
                                            //worksheet.Cells[incRowExcel, 1].Value = "Daryanti L. Anggraini ";
                                            //worksheet.Cells[incRowExcel, 5].Value = "Adrian Roza ";
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 3].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells[incRowExcel, 5, incRowExcel, 6].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Value = "Daryanti L. Anggraini ";
                                            worksheet.Cells[incRowExcel, 5].Value = "Adrian Roza ";

                                            int A = incRowExcel++;

                                            incRowExcel = incRowExcel + 4;

                                            worksheet.Cells[incRowExcel, 1].Value = "Disclaimer : ";
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Merge = true;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Size = 12;

                                            incRowExcel++;

                                            worksheet.Cells[incRowExcel, 1].Value = "Dokumen ini dipersiapkan oleh PT SUCOR SEKURITAS dan hanya bisa digunakan untuk kepentingan investor tersebut diatas dan tidak untuk pihak lainnya. Laporan ini bukan merupakan konfirmasi dari PT SUCOR SEKURITAS dan tidak untuk menggantikan laporan yang wajib diterbitkan oleh Bank Kustodian, jika ada perbedaan antara laporan ini dengan laporan Bank Kustodian, maka laporan Bank Kustodian adalah yang benar. Laporan ini diproses oleh computer dan tidak memerlukan tandatangan.";
                                            worksheet.Cells[incRowExcel, 1, incRowExcel + 2, 15].Style.Font.Size = 11;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel + 2, 15].Merge = true;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel + 2, 15].Style.WrapText = true;

                                            incRowExcel = incRowExcel + 4;

                                            worksheet.Cells[incRowExcel, 1].Value = "PT SUCOR SEKURITAS terdaftar dan diawasi oleh OJK, dengan nomor registrasi KEP-01/PM/MI/1999";
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 15].Style.Font.Size = 11;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 15].Merge = true;

                                            incRowExcel = incRowExcel + 2;

                                            worksheet.Row(A).PageBreak = _unitRegistryRpt.PageBreak;
                                            //worksheet.Row(incRowExcel).PageBreak = true;

                                        }
                                        //incRowExcel++;
                                        //int _lastRow = incRowExcel;

                                        string _rangeA = "A5:N" + incRowExcel;
                                        using (ExcelRange r = worksheet.Cells[_rangeA]) // KALO  KOLOM 1 SAMPE 9 A-I
                                        {
                                            //r.Style.Font.Size = 14;
                                        }

                                        //worksheet.DeleteRow(_lastRow);
                                        //worksheet.DeleteRow(_lastRow + 1);
                                        //worksheet.DeleteRow(_lastRow + 2);
                                        //worksheet.DeleteRow(_lastRow + 3);

                                        worksheet.PrinterSettings.FitToPage = true;
                                        worksheet.PrinterSettings.FitToWidth = 1;
                                        worksheet.PrinterSettings.FitToHeight = 0;
                                        worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel - 1, 14];
                                        worksheet.Column(1).Width = 5;
                                        worksheet.Column(2).Width = 15;
                                        worksheet.Column(3).Width = 5;
                                        worksheet.Column(4).Width = 40;
                                        worksheet.Column(5).Width = 10;
                                        worksheet.Column(6).Width = 20;
                                        worksheet.Column(7).Width = 25;
                                        worksheet.Column(8).Width = 10;
                                        worksheet.Column(9).Width = 20;
                                        worksheet.Column(10).Width = 5;
                                        worksheet.Column(11).Width = 20;
                                        worksheet.Column(12).Width = 10;
                                        worksheet.Column(13).Width = 25;
                                        worksheet.Column(14).Width = 15;


                                        // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                        //worksheet.Cells["A3:I14"].AutoFitColumns(); // UNTUK AUTO FIT UKURAN TULISAN DENGAN KOLOM, KEBETULAN INI BUAT HEADER AJA DIPAKENYA
                                        //worksheetApproved.Cells["A3:E3"].AutoFitColumns();
                                        worksheet.PrinterSettings.Orientation = eOrientation.Landscape; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                        worksheet.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN

                                        //worksheet.HeaderFooter.OddHeader.CenteredText = "\n \n \n \n \n \n \n &30&B REDEMPTION \n &28&B Batch Form";

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

            #region Laporan Portofolio
            else if (_unitRegistryRpt.ReportName.Equals("Laporan Portofolio"))
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
                            cmd.CommandText = @"
                                        Select C.Name FundName, isnull(A.UnitAmount,0) saldo,isnull(D.Nav,0) NAV, isnull(A.UnitAmount,0) * isnull(D.NAV,0) hargaPasar 
                                        , isnull(dbo.[FGetAVGForFundClientPosition](@DateFrom,A.FundClientPK,A.FundPK),0) AvgPrice,isnull(A.UnitAmount,0) * isnull(dbo.[FGetAVGForFundClientPosition](@DateFrom,A.FundClientPK,A.FundPK),0) NilaiAvg,sum(isnull(A.UnitAmount,0) * isnull(D.NAV,0) - (isnull(A.UnitAmount,0) * isnull(dbo.[FGetAVGForFundClientPosition](@DateFrom,A.FundClientPK,A.FundPK),0))) Realisasi ,
                                        B.AlamatInd1 + ' ' + B.AlamatPerusahaan Alamat, 
                                        B.Name, case when B.SID = '0' then '' else B.SID end SID
                                        from FundClientPosition A
                                        left join fundClient B on A.FundClientPK = B.FundClientPK and B.status = 2
                                        left join fund C on A.FundPK = C.FundPK and C.status = 2
                                        left join closeNav D on A.FundPK = D.FundPK and D.Date = @DateFrom and D.status = 2
                                        where A.Date = @DateFrom  " + _paramFund + _paramFundClient +
                                        @"Group By C.Name,A.UnitAmount,D.Nav,A.FundClientPK,A.FundPK,B.AlamatInd1,B.AlamatPerusahaan,B.Name,B.SID";

                            cmd.CommandTimeout = 0;
                            cmd.Parameters.AddWithValue("@DateFrom", _unitRegistryRpt.ValueDateFrom);
                            cmd.Parameters.AddWithValue("@FundFrom", _unitRegistryRpt.FundFrom);
                            cmd.Parameters.AddWithValue("@FundClientFrom", _unitRegistryRpt.FundClientFrom);


                            using (SqlDataReader dr0 = cmd.ExecuteReader())
                            {
                                if (!dr0.HasRows)
                                {
                                    return false;
                                }
                                else
                                {
                                    string filePath = Tools.ReportsPath + "LaporanPortofolio" + "_" + _userID + ".xlsx";
                                    string pdfPath = Tools.ReportsPath + "LaporanPortofolio" + "_" + _userID + ".pdf";
                                    FileInfo excelFile = new FileInfo(filePath);
                                    if (excelFile.Exists)
                                    {
                                        excelFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                        excelFile = new FileInfo(filePath);
                                    }

                                    // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                    using (ExcelPackage package = new ExcelPackage(excelFile))
                                    {
                                        package.Workbook.Properties.Title = "LaporanPortofolio";
                                        package.Workbook.Properties.Author = Tools.DefaultReportAuthor();
                                        package.Workbook.Properties.Comments = Tools.DefaultReportComments();
                                        package.Workbook.Properties.Company = Tools.DefaultReportCompany();
                                        package.Workbook.Properties.SetCustomPropertyValue("Checked by", _userID);
                                        package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", Tools.DefaultReportAssemblyName());

                                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("LaporanPortofolio");

                                        int incRowExcel = 1;

                                        worksheet.Cells[incRowExcel, 4].Value = "LAPORAN PORTOFOLIO";
                                        worksheet.Cells[incRowExcel, 4, incRowExcel, 7].Merge = true;
                                        worksheet.Cells[incRowExcel, 4, incRowExcel, 7].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 4, incRowExcel, 7].Style.Font.Size = 12;

                                        incRowExcel++;

                                        worksheet.Cells[incRowExcel, 4].Value = "REKSA DANA " + _host.Get_CompanyName();
                                        worksheet.Cells[incRowExcel, 4, incRowExcel, 9].Merge = true;
                                        worksheet.Cells[incRowExcel, 4, incRowExcel, 9].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 4, incRowExcel, 9].Style.Font.Size = 12;

                                        incRowExcel = incRowExcel + 2;

                                        worksheet.Cells[incRowExcel, 4].Value = "Posisi per tanggal: " + Convert.ToDateTime(_unitRegistryRpt.ValueDateTo).ToShortDateString();
                                        worksheet.Cells[incRowExcel, 4, incRowExcel, 7].Merge = true;
                                        worksheet.Cells[incRowExcel, 4, incRowExcel, 7].Style.Font.Size = 9;

                                        incRowExcel = incRowExcel + 2;

                                        //ATUR DATA GROUPINGNYA DULU
                                        List<Portofolio> rList = new List<Portofolio>();
                                        while (dr0.Read())
                                        {
                                            Portofolio rSingle = new Portofolio();
                                            rSingle.NamaReksaDana = dr0["FundName"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["FundName"]);
                                            rSingle.Saldo = dr0["saldo"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["saldo"]);
                                            rSingle.SID = dr0["SID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["SID"]);
                                            rSingle.Name = dr0["Name"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["Name"]);
                                            rSingle.AlamatInd1 = dr0["Alamat"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["Alamat"]);
                                            rSingle.HargaPasar = dr0["hargaPasar"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["hargaPasar"]);
                                            rSingle.UnitRataRata = dr0["AvgPrice"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["AvgPrice"]);
                                            rSingle.NilaiRataRata = dr0["NilaiAvg"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["NilaiAvg"]);
                                            rSingle.LabaRugi = dr0["Realisasi"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["Realisasi"]);
                                            rSingle.Unit = dr0["NAV"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["NAV"]);
                                            rList.Add(rSingle);
                                        }

                                        var QueryByClientID =
                                         from r in rList
                                         group r by new { r.SID, r.Name, r.AlamatInd1 } into rGroup
                                         select rGroup;

                                        foreach (var rsHeader in QueryByClientID)
                                        {
                                            worksheet.Cells[incRowExcel, 1].Value = rsHeader.Key.Name;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Merge = true;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Size = 8;

                                            incRowExcel++;

                                            worksheet.Cells[incRowExcel, 1].Value = rsHeader.Key.AlamatInd1;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 1].Merge = true;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 1].Style.WrapText = true;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 1].Style.Font.Size = 8;

                                            incRowExcel = incRowExcel + 5;

                                            worksheet.Cells[incRowExcel, 1].Value = "NOMOR SID : " + rsHeader.Key.SID;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 1].Merge = true;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 1].Style.Font.Size = 8;

                                            incRowExcel = incRowExcel + 3;

                                            worksheet.Cells[incRowExcel, 1].Value = "RINGKASAN PORTOFOLIO REKSADANA ANDA DI " + _host.Get_CompanyName();
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 7].Merge = true;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 7].Style.Font.Size = 8;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 7].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 7].Style.Font.Bold = true;

                                            incRowExcel = incRowExcel + 2;

                                            worksheet.Cells[incRowExcel, 1].Value = "NAMA REKSA DANA";
                                            worksheet.Cells[incRowExcel, 2].Value = "SALDO";
                                            worksheet.Cells[incRowExcel, 3].Value = "NAB/UNIT";
                                            worksheet.Cells[incRowExcel, 4].Value = "HARGA PASAR";
                                            worksheet.Cells[incRowExcel, 5].Style.WrapText = true;
                                            worksheet.Cells[incRowExcel, 5].Value = "NAB/UNIT RATA-RATA";
                                            worksheet.Cells[incRowExcel, 6].Style.WrapText = true;
                                            worksheet.Cells[incRowExcel, 6].Value = "NILAI RATA-RATA";
                                            worksheet.Cells[incRowExcel, 7].Style.WrapText = true;
                                            worksheet.Cells[incRowExcel, 7].Value = "REALISASI LABA/RUGI";

                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 7].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 7].Style.Font.Size = 8;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 7].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 7].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(251, 228, 213));
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 7].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 7].Style.Border.Top.Style = ExcelBorderStyle.Double;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 7].Style.Border.Bottom.Style = ExcelBorderStyle.Double;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 7].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 7].Style.Border.Right.Style = ExcelBorderStyle.Thin;


                                            incRowExcel++;

                                            int first = incRowExcel;

                                            int no = 1;

                                            foreach (var rsDetail in rsHeader)
                                            {
                                                worksheet.Cells[incRowExcel, 1].Value = rsDetail.NamaReksaDana;
                                                worksheet.Cells[incRowExcel, 2].Value = rsDetail.Saldo;
                                                worksheet.Cells[incRowExcel, 2].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.Unit;
                                                worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "#,##0.0000";
                                                worksheet.Cells[incRowExcel, 4].Value = rsDetail.HargaPasar;
                                                worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 5].Value = rsDetail.UnitRataRata;
                                                worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.0000";
                                                worksheet.Cells[incRowExcel, 6].Value = rsDetail.NilaiRataRata;
                                                worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 7].Value = rsDetail.LabaRugi;
                                                worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0.00";

                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 7].Style.Font.Size = 8;
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 7].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 7].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 7].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                                                incRowExcel++;
                                                no++;
                                            }

                                            int last = incRowExcel;

                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 7].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 7].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 7].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                                            incRowExcel = incRowExcel + 2;

                                            worksheet.Cells[incRowExcel, 2].Value = "Total : ";
                                            worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##0.00";
                                            worksheet.Cells[incRowExcel, 4].Formula = "SUM(D" + first.ToString() + ":D" + last.ToString() + ")";
                                            worksheet.Cells[incRowExcel, 4].Calculate();
                                            worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0.00";
                                            worksheet.Cells[incRowExcel, 7].Formula = "SUM(G" + first.ToString() + ":G" + last.ToString() + ")";
                                            worksheet.Cells[incRowExcel, 7].Calculate();
                                            worksheet.Cells[incRowExcel, 2, incRowExcel, 7].Style.Border.Top.Style = ExcelBorderStyle.Double;
                                            worksheet.Cells[incRowExcel, 2, incRowExcel, 7].Style.Border.Bottom.Style = ExcelBorderStyle.Double;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 7].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 7].Style.Font.Size = 7;

                                            incRowExcel = incRowExcel + 2;
                                        }

                                        incRowExcel = incRowExcel + 4;

                                        worksheet.Cells[incRowExcel, 1].Value = "Disclaimer : ";
                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Merge = true;
                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Size = 12;

                                        incRowExcel++;

                                        worksheet.Cells[incRowExcel, 1].Value = "Dokumen ini dipersiapkan oleh PT SUCOR SEKURITAS dan hanya bisa digunakan untuk kepentingan investor tersebut diatas dan tidak untuk pihak lainnya. Laporan ini bukan merupakan konfirmasi dari PT SUCOR SEKURITAS dan tidak untuk menggantikan laporan yang wajib diterbitkan oleh Bank Kustodian, jika ada perbedaan antara laporan ini dengan laporan Bank Kustodian, maka laporan Bank Kustodian adalah yang benar. Laporan ini diproses oleh computer dan tidak memerlukan tandatangan.";
                                        worksheet.Cells[incRowExcel, 1, incRowExcel + 2, 7].Style.Font.Size = 11;
                                        worksheet.Cells[incRowExcel, 1, incRowExcel + 2, 7].Merge = true;
                                        worksheet.Cells[incRowExcel, 1, incRowExcel + 2, 7].Style.WrapText = true;

                                        incRowExcel = incRowExcel + 4;

                                        worksheet.Cells[incRowExcel, 1].Value = "PT SUCOR SEKURITAS terdaftar dan diawasi oleh OJK, dengan nomor registrasi KEP-01/PM/MI/1999";
                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 7].Style.Font.Size = 11;
                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 7].Merge = true;

                                        incRowExcel = incRowExcel + 2;

                                        //worksheet.Cells[incRowExcel, 1].Value = "PT SUCORINVEST CENTRAL GANI";
                                        //worksheet.Cells[incRowExcel, 1, incRowExcel, 7].Merge = true;
                                        //worksheet.Cells[incRowExcel, 1, incRowExcel, 7].Style.Font.Bold = true;
                                        //worksheet.Cells[incRowExcel, 1, incRowExcel, 7].Style.Font.Size = 7;

                                        //incRowExcel++;

                                        //worksheet.Cells[incRowExcel, 1].Value = "Equity Tower, Lantai 31, Jalan Jend Sudirman Kav 52-53, Jakarta 12190 – Indonesia";
                                        //worksheet.Cells[incRowExcel, 1, incRowExcel, 7].Merge = true;
                                        //worksheet.Cells[incRowExcel, 1, incRowExcel, 7].Style.Font.Size = 8;

                                        //incRowExcel++;

                                        //worksheet.Cells[incRowExcel, 1].Value = "Phone: +62-21 299 60 800 (hunting); Fax: +62-21 5797 3929 ¦www.sucorinvest.com";
                                        //worksheet.Cells[incRowExcel, 1, incRowExcel, 7].Merge = true;
                                        //worksheet.Cells[incRowExcel, 1, incRowExcel, 7].Style.Font.Size = 8;

                                        incRowExcel = incRowExcel + 2;
                                        worksheet.PrinterSettings.FitToPage = true;
                                        worksheet.PrinterSettings.FitToWidth = 1;
                                        worksheet.PrinterSettings.FitToHeight = 0;
                                        worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel - 1, 7];
                                        worksheet.Column(1).Width = 50;
                                        worksheet.Column(2).Width = 15;
                                        worksheet.Column(3).Width = 15;
                                        worksheet.Column(4).Width = 15;
                                        worksheet.Column(5).Width = 15;
                                        worksheet.Column(6).Width = 15;
                                        worksheet.Column(7).Width = 20;


                                        // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                        //worksheet.Cells.AutoFitColumns(); // UNTUK AUTO FIT UKURAN TULISAN DENGAN KOLOM, KEBETULAN INI BUAT HEADER AJA DIPAKENYA
                                        //worksheet.PrinterSettings.FitToPage = true;
                                        worksheet.PrinterSettings.Orientation = eOrientation.Portrait; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                        worksheet.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN
                                        //worksheet.HeaderFooter.OddHeader.RightAlignedText = "&12 Subscription REPORT";

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

                                        //worksheet.HeaderFooter.OddFooter.CenteredText = Tools.DefaultReportFooterCenterText();
                                        worksheet.HeaderFooter.OddFooter.LeftAlignedText = Tools.DefaultReportFooterLeftText();

                                        package.Save();
                                        if (_unitRegistryRpt.DownloadMode == "PDF")
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

            #region Laporan Rincian Transaksi
            else if (_unitRegistryRpt.ReportName.Equals("Laporan Rincian Transaksi"))
            {
                try
                {
                    using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                    {
                        DbCon.Open();
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            string _statusSubs = "";
                            string _paramFund = "";

                            string _paramFundClient = "";

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


                            if (_unitRegistryRpt.Status == 1)
                            {
                                _statusSubs = "  A.Status = 2 and A.Posted = 1 and A.Revised = 0 ";
                                //_statusRedemp = "  CR.Status = 2 and CR.Posted = 1 and CR.Revised = 0 ";
                            }
                            else if (_unitRegistryRpt.Status == 2)
                            {
                                _statusSubs = "  A.Status = 2 and A.Posted = 1 and A.Revised = 1 ";
                                //_statusRedemp = "  CR.Status = 2 and CR.Posted = 1 and CR.Revised = 1 ";
                            }
                            else if (_unitRegistryRpt.Status == 3)
                            {
                                _statusSubs = "  A.Status = 2 and A.Posted = 0 and A.Revised = 0 ";
                                //_statusRedemp = "  CR.Status = 2 and CR.Posted = 0 and CR.Revised = 0 ";
                            }
                            else if (_unitRegistryRpt.Status == 4)
                            {
                                _statusSubs = "  A.Status = 1  ";
                                //_statusRedemp = "  CR.Status = 1  ";
                            }
                            else if (_unitRegistryRpt.Status == 5)
                            {
                                _statusSubs = "  A.Status = 3  ";
                                //_statusRedemp = "  CR.Status = 3  ";
                            }
                            else if (_unitRegistryRpt.Status == 6)
                            {
                                _statusSubs = "  (A.Status = 2 or A.Posted = 1) and A.Revised = 0  ";
                                //_statusRedemp = "  (CR.Status = 2 or CR.Posted = 1) and CR.Revised = 0  ";
                            }
                            else if (_unitRegistryRpt.Status == 7)
                            {
                                _statusSubs = "  (A.Status = 1 Or A.Status = 2 or A.Posted = 1) and  A.Revised = 0  ";
                                //_statusRedemp = "  (CR.Status = 1 Or CR.Status = 2 or CR.Posted = 1) and CR.Revised = 0  ";
                            }


                            cmd.CommandText =

                                @"
                                Select F.FundPK,FC.FundClientPK,F.Name,FC.Name fundClientName,FC.SID,FC.AlamatInd1,A.ValueDate,'Subscription' Type, A.TotalUnitAmount,A.NAV, A.TotalCashAmount,SubscriptionFeeAmount FeeAmount from ClientSubscription A
                                left join Fund F on A.FundPK = F.FundPK and F.status  = 2
                                left join FundClient FC on A.FundClientPK  = FC.FundClientPK and FC.status  = 2
                                WHERE " + _statusSubs + _paramFund + _paramFundClient +
                                @"and ValueDate between @DateFrom and @DateTo

                                UNION ALL

                                Select F.FundPK,FC.FundClientPK,F.Name,FC.Name fundClientName,FC.SID,FC.AlamatInd1,A.ValueDate,'Redemption' Type, A.TotalUnitAmount,A.NAV, A.TotalCashAmount,RedemptionFeeAmount FeeAmount  from ClientRedemption A
                                left join Fund F on A.FundPK = F.FundPK and F.status  = 2
                                left join FundClient FC on A.FundClientPK  = FC.FundClientPK and FC.status  = 2
                                WHERE " + _statusSubs + _paramFund + _paramFundClient +
                                @" and ValueDate between @DateFrom and @DateTo
                                order by A.ValueDate";



                            cmd.CommandTimeout = 0;
                            cmd.Parameters.AddWithValue("@DateFrom", _unitRegistryRpt.ValueDateFrom);
                            cmd.Parameters.AddWithValue("@DateTo", _unitRegistryRpt.ValueDateTo);
                            cmd.Parameters.AddWithValue("@FundFrom", _unitRegistryRpt.FundFrom);
                            cmd.Parameters.AddWithValue("@FundClientFrom", _unitRegistryRpt.FundClientFrom);


                            using (SqlDataReader dr0 = cmd.ExecuteReader())
                            {
                                if (!dr0.HasRows)
                                {
                                    return false;
                                }
                                else
                                {
                                    string filePath = Tools.ReportsPath + "LaporanRincianTransaksi" + "_" + _userID + ".xlsx";
                                    string pdfPath = Tools.ReportsPath + "LaporanRincianTransaksi" + "_" + _userID + ".pdf";
                                    FileInfo excelFile = new FileInfo(filePath);
                                    if (excelFile.Exists)
                                    {
                                        excelFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                        excelFile = new FileInfo(filePath);
                                    }

                                    // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                    using (ExcelPackage package = new ExcelPackage(excelFile))
                                    {
                                        package.Workbook.Properties.Title = "LaporanRincianTransaksi";
                                        package.Workbook.Properties.Author = Tools.DefaultReportAuthor();
                                        package.Workbook.Properties.Comments = Tools.DefaultReportComments();
                                        package.Workbook.Properties.Company = Tools.DefaultReportCompany();
                                        package.Workbook.Properties.SetCustomPropertyValue("Checked by", _userID);
                                        package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", Tools.DefaultReportAssemblyName());

                                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("LaporanRincianTransaksi");

                                        int incRowExcel = 1;

                                        worksheet.Cells[incRowExcel, 3].Value = "LAPORAN RINCIAN TRANSAKSI";
                                        worksheet.Cells[incRowExcel, 3, incRowExcel, 9].Merge = true;
                                        worksheet.Cells[incRowExcel, 3, incRowExcel, 9].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 3, incRowExcel, 9].Style.Font.Size = 14;

                                        incRowExcel++;

                                        worksheet.Cells[incRowExcel, 3].Value = "REKSA DANA " + _host.Get_CompanyName();
                                        worksheet.Cells[incRowExcel, 3, incRowExcel, 9].Merge = true;
                                        worksheet.Cells[incRowExcel, 3, incRowExcel, 9].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 3, incRowExcel, 9].Style.Font.Size = 14;

                                        incRowExcel = incRowExcel + 2;

                                        worksheet.Cells[incRowExcel, 3].Value = "Periode: " + Convert.ToDateTime(_unitRegistryRpt.ValueDateFrom).ToShortDateString() + " s/d " + Convert.ToDateTime(_unitRegistryRpt.ValueDateTo).ToShortDateString();
                                        worksheet.Cells[incRowExcel, 3, incRowExcel, 9].Merge = true;
                                        worksheet.Cells[incRowExcel, 3, incRowExcel, 9].Style.Font.Size = 11;
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 9].Style.Border.Bottom.Style = ExcelBorderStyle.Double;

                                        incRowExcel = incRowExcel + 2;

                                        //ATUR DATA GROUPINGNYA DULU
                                        List<ReksaDana> rList = new List<ReksaDana>();
                                        while (dr0.Read())
                                        {
                                            ReksaDana rSingle = new ReksaDana();

                                            rSingle.Type = dr0["Type"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["Type"]);
                                            rSingle.Fund = dr0["Name"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["Name"]);
                                            rSingle.SID = dr0["SID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["SID"]);
                                            rSingle.Name = dr0["fundClientName"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["fundClientName"]);
                                            rSingle.AlamatInd1 = dr0["AlamatInd1"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["AlamatInd1"]);
                                            rSingle.FundPK = dr0["FundPK"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr0["FundPK"]);
                                            rSingle.FundClientPK = dr0["FundClientPK"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr0["FundClientPK"]);
                                            rSingle.TransactionDate = dr0["ValueDate"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["ValueDate"]);
                                            rSingle.TotalCashAmount = dr0["TotalCashAmount"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["TotalCashAmount"]);
                                            rSingle.FeeAmount = dr0["FeeAmount"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["FeeAmount"]);
                                            rSingle.NAV = dr0["NAV"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["NAV"]);
                                            rSingle.TotalUnitAmount = dr0["TotalUnitAmount"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["TotalUnitAmount"]);

                                            rList.Add(rSingle);
                                        }

                                        var QueryByClientID =
                                         from r in rList
                                         group r by new { r.FundPK, r.FundClientPK, r.SID, r.Name, r.AlamatInd1 } into rGroup
                                         select rGroup;


                                        foreach (var rsHeader in QueryByClientID)
                                        {
                                            worksheet.Cells[incRowExcel, 2].Value = rsHeader.Key.Name;
                                            worksheet.Cells[incRowExcel, 2, incRowExcel, 5].Merge = true;
                                            worksheet.Cells[incRowExcel, 2, incRowExcel, 5].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 2, incRowExcel, 5].Style.Font.Size = 11;

                                            incRowExcel++;

                                            worksheet.Cells[incRowExcel, 2].Value = rsHeader.Key.AlamatInd1;
                                            worksheet.Cells[incRowExcel, 2, incRowExcel, 2].Merge = true;
                                            worksheet.Cells[incRowExcel, 2, incRowExcel, 2].Style.WrapText = true;
                                            worksheet.Cells[incRowExcel, 2, incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            worksheet.Cells[incRowExcel, 2, incRowExcel, 2].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                                            worksheet.Cells[incRowExcel, 2, incRowExcel, 2].Style.Font.Size = 9;

                                            incRowExcel = incRowExcel + 3;

                                            worksheet.Cells[incRowExcel, 2].Value = "NOMOR SID : " + rsHeader.Key.SID;
                                            worksheet.Cells[incRowExcel, 2, incRowExcel, 3].Merge = true;
                                            worksheet.Cells[incRowExcel, 2, incRowExcel, 3].Style.Font.Size = 11;

                                            incRowExcel = incRowExcel + 2;

                                            worksheet.Cells[incRowExcel, 1].Value = "NO";
                                            worksheet.Cells[incRowExcel, 2].Value = "NAMA REKSA DANA";
                                            worksheet.Cells[incRowExcel, 2].Style.WrapText = true;
                                            worksheet.Cells[incRowExcel, 3].Value = "DESKRIPSI";
                                            worksheet.Cells[incRowExcel, 4].Style.WrapText = true;
                                            worksheet.Cells[incRowExcel, 4].Value = "TGL TRANSAKSI";
                                            worksheet.Cells[incRowExcel, 5].Style.WrapText = true;
                                            worksheet.Cells[incRowExcel, 5].Value = "HARGA NETTO";
                                            worksheet.Cells[incRowExcel, 6].Style.WrapText = true;
                                            worksheet.Cells[incRowExcel, 6].Value = "BIAYA TRANSAKSI";
                                            worksheet.Cells[incRowExcel, 7].Value = "NAB / UNIT";
                                            worksheet.Cells[incRowExcel, 8].Value = "UNIT";
                                            worksheet.Cells[incRowExcel, 9].Value = "BALANCE";

                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 9].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 9].Style.Font.Size = 11;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 9].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 9].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(251, 228, 213));
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 9].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 9].Style.Border.Top.Style = ExcelBorderStyle.Double;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 9].Style.Border.Bottom.Style = ExcelBorderStyle.Double;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 9].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 9].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                                            //incRowExcel++;

                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 9].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 9].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                                            incRowExcel++;

                                            int first = incRowExcel;


                                            worksheet.Cells[incRowExcel, 9].Value = _host.Get_UnitAmountByFundPKandFundClientPK(rsHeader.Key.FundPK, rsHeader.Key.FundClientPK, _host.GetWorkingDay(Convert.ToDateTime(_unitRegistryRpt.ValueDateFrom), -1));
                                            worksheet.Row(incRowExcel).Height = 1;
                                            worksheet.Cells[incRowExcel, 9].Calculate();
                                            incRowExcel++;
                                            //area header

                                            int no = 1;
                                            //int _startRowDetail = incRowExcel;
                                            //int _endRowDetail = 0;
                                            foreach (var rsDetail in rsHeader)
                                            {
                                                int RowD = incRowExcel;
                                                int RowZZ = incRowExcel - 1;

                                                worksheet.Cells[incRowExcel, 1].Value = no;
                                                worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 1].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 2].Value = rsDetail.Fund;
                                                worksheet.Cells[incRowExcel, 2].Style.WrapText = true;
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.Type;
                                                worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 3].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "dd-MMM-yyyy";
                                                worksheet.Cells[incRowExcel, 4].Value = Convert.ToDateTime(rsDetail.TransactionDate);
                                                worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##";
                                                worksheet.Cells[incRowExcel, 5].Value = rsDetail.TotalCashAmount;
                                                worksheet.Cells[incRowExcel, 6].Value = rsDetail.FeeAmount;
                                                worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 7].Value = rsDetail.NAV;
                                                worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0.0000";
                                                worksheet.Cells[incRowExcel, 8].Value = rsDetail.TotalUnitAmount;
                                                worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0.0000";
                                                if (rsDetail.Type == "Subscription")
                                                {
                                                    worksheet.Cells[incRowExcel, 9].Formula = "SUM(I" + RowZZ + "+ H" + RowD + ")";
                                                    worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0.0000";

                                                }
                                                else
                                                {
                                                    worksheet.Cells[incRowExcel, 9].Formula = "SUM(I" + RowZZ + "- H" + RowD + ")";
                                                    worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0.0000";

                                                }
                                                worksheet.Cells[incRowExcel, 9].Calculate();

                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 9].Style.Font.Size = 11;
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 9].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 9].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 9].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                                worksheet.Row(incRowExcel).Height = 40;

                                                incRowExcel++;
                                                no++;
                                            }

                                            int last = incRowExcel;

                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 9].Style.Border.Top.Style = ExcelBorderStyle.Thin;


                                            incRowExcel = incRowExcel + 2;
                                        }

                                        incRowExcel = incRowExcel + 4;

                                        worksheet.Cells[incRowExcel, 1].Value = "Disclaimer : ";
                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Merge = true;
                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Size = 12;

                                        incRowExcel++;

                                        worksheet.Cells[incRowExcel, 1].Value = "Dokumen ini dipersiapkan oleh PT SUCOR SEKURITAS dan hanya bisa digunakan untuk kepentingan investor tersebut diatas dan tidak untuk pihak lainnya. Laporan ini bukan merupakan konfirmasi dari PT SUCOR SEKURITAS dan tidak untuk menggantikan laporan yang wajib diterbitkan oleh Bank Kustodian, jika ada perbedaan antara laporan ini dengan laporan Bank Kustodian, maka laporan Bank Kustodian adalah yang benar. Laporan ini diproses oleh computer dan tidak memerlukan tandatangan.";
                                        worksheet.Cells[incRowExcel, 1, incRowExcel + 2, 9].Style.Font.Size = 11;
                                        worksheet.Cells[incRowExcel, 1, incRowExcel + 2, 9].Merge = true;
                                        worksheet.Cells[incRowExcel, 1, incRowExcel + 2, 9].Style.WrapText = true;

                                        incRowExcel = incRowExcel + 4;

                                        worksheet.Cells[incRowExcel, 1].Value = _host.Get_CompanyName() + " terdaftar dan diawasi oleh OJK, dengan nomor registrasi KEP-01/PM/MI/1999";
                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 9].Style.Font.Size = 11;
                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 9].Merge = true;

                                        incRowExcel = incRowExcel + 2;

                                        //worksheet.Cells[incRowExcel, 1].Value = "PT SUCORINVEST CENTRAL GANI";
                                        //worksheet.Cells[incRowExcel, 1, incRowExcel, 9].Merge = true;
                                        //worksheet.Cells[incRowExcel, 1, incRowExcel, 9].Style.Font.Bold = true;
                                        //worksheet.Cells[incRowExcel, 1, incRowExcel, 9].Style.Font.Size = 9;

                                        //incRowExcel++;

                                        //worksheet.Cells[incRowExcel, 1].Value = "Equity Tower, Lantai 31, Jalan Jend Sudirman Kav 52-53, Jakarta 12190 – Indonesia";
                                        //worksheet.Cells[incRowExcel, 1, incRowExcel, 9].Merge = true;
                                        //worksheet.Cells[incRowExcel, 1, incRowExcel, 9].Style.Font.Size = 9;

                                        //incRowExcel++;

                                        //worksheet.Cells[incRowExcel, 1].Value = "Phone: +62-21 299 60 800 (hunting); Fax: +62-21 5797 3929 ¦www.sucorinvest.com";
                                        //worksheet.Cells[incRowExcel, 1, incRowExcel, 9].Merge = true;
                                        //worksheet.Cells[incRowExcel, 1, incRowExcel, 9].Style.Font.Size = 9;

                                        incRowExcel = incRowExcel + 2;
                                        worksheet.PrinterSettings.FitToPage = true;
                                        worksheet.PrinterSettings.FitToWidth = 1;
                                        worksheet.PrinterSettings.FitToHeight = 0;
                                        worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel - 1, 9];
                                        worksheet.Column(1).Width = 5;
                                        worksheet.Column(2).Width = 35;
                                        worksheet.Column(3).Width = 15;
                                        worksheet.Column(4).Width = 15;
                                        worksheet.Column(5).Width = 20;
                                        worksheet.Column(6).Width = 15;
                                        worksheet.Column(7).Width = 15;
                                        worksheet.Column(8).Width = 20;
                                        worksheet.Column(9).Width = 20;


                                        // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                        //worksheet.Cells.AutoFitColumns(); // UNTUK AUTO FIT UKURAN TULISAN DENGAN KOLOM, KEBETULAN INI BUAT HEADER AJA DIPAKENYA
                                        //worksheet.PrinterSettings.FitToPage = true;
                                        worksheet.PrinterSettings.Orientation = eOrientation.Portrait; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                        worksheet.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN
                                        //worksheet.HeaderFooter.OddHeader.RightAlignedText = "&12 Subscription REPORT";

                                        // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                        //ExcelWorksheet ws = package.Workbook.Worksheets.Add("Demo");
                                        //worksheet.PrinterSettings.TopMargin = (decimal).5 / 2.54M; // narrow border
                                        //worksheet.PrinterSettings.RightMargin = (decimal).5 / 2.54M; //narrow border

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

                                        //worksheet.HeaderFooter.OddFooter.CenteredText = Tools.DefaultReportFooterCenterText();
                                        worksheet.HeaderFooter.OddFooter.LeftAlignedText = Tools.DefaultReportFooterLeftText();

                                        package.Save();
                                        if (_unitRegistryRpt.DownloadMode == "PDF")
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
            #endregion //

            #region Laporan Rekap Total NAB
            else if (_unitRegistryRpt.ReportName.Equals("Laporan Rekap Total NAB"))
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
                                _paramFund = "And A.FundPK in ( " + _unitRegistryRpt.FundFrom + " ) ";
                            }
                            else
                            {
                                _paramFund = "";
                            }
                            if (!_host.findString(_unitRegistryRpt.FundClientFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_unitRegistryRpt.FundClientFrom))
                            {
                                _paramFundClient = "And A.FundClientPK  in ( " + _unitRegistryRpt.FundClientFrom + " ) ";
                            }
                            else
                            {
                                _paramFundClient = "";
                            }


                            cmd.CommandText =

                            @"
                            Select C.Name FundName,B.SID,B.IFUACode,B.Name,A.UnitAmount, isnull(D.Nav,0) NABPerUnit, A.UnitAmount * isnull(D.NAV,0) TotalNAB from FundClientPosition A
                            left join FundClient B on A.FundClientPK = B.FundClientPK and B.status = 2
                            left join Fund C on A.FundPK = C.FundPK and C.status = 2
                            left join CloseNAV D on A.Date = dbo.FWorkingDay(D.Date, - 1) and D.status = 2 and A.FundPK = D.FundPK
                            where A.Date = dbo.FWorkingDay(@DateTo, - 1)  " + _paramFund;



                            cmd.CommandTimeout = 0;
                            cmd.Parameters.AddWithValue("@DateTo", _unitRegistryRpt.ValueDateTo);
                            cmd.Parameters.AddWithValue("@FundFrom", _unitRegistryRpt.FundFrom);
                            cmd.Parameters.AddWithValue("@FundClientFrom", _unitRegistryRpt.FundClientFrom);



                            using (SqlDataReader dr0 = cmd.ExecuteReader())
                            {
                                if (!dr0.HasRows)
                                {
                                    return false;
                                }
                                else
                                {
                                    string filePath = Tools.ReportsPath + "LaporanRekapTotalNAB" + "_" + _userID + ".xlsx";
                                    string pdfPath = Tools.ReportsPath + "LaporanRekapTotalNAB" + "_" + _userID + ".pdf";
                                    FileInfo excelFile = new FileInfo(filePath);
                                    if (excelFile.Exists)
                                    {
                                        excelFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                        excelFile = new FileInfo(filePath);
                                    }

                                    // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                    using (ExcelPackage package = new ExcelPackage(excelFile))
                                    {
                                        package.Workbook.Properties.Title = "LaporanRekapTotalNAB";
                                        package.Workbook.Properties.Author = Tools.DefaultReportAuthor();
                                        package.Workbook.Properties.Comments = Tools.DefaultReportComments();
                                        package.Workbook.Properties.Company = Tools.DefaultReportCompany();
                                        package.Workbook.Properties.SetCustomPropertyValue("Checked by", _userID);
                                        package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", Tools.DefaultReportAssemblyName());

                                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("LaporanRekapTotalNAB");

                                        int incRowExcel = 1;

                                        worksheet.Cells[incRowExcel, 1].Value = "Periode: " + Convert.ToDateTime(_unitRegistryRpt.ValueDateFrom).ToShortDateString();//+ " s/d " + Convert.ToDateTime(_unitRegistryRpt.ValueDateTo).ToShortDateString();
                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 7].Merge = true;
                                        worksheet.Cells[incRowExcel, 1].Style.Font.UnderLine = true;
                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 7].Style.Font.Size = 9;
                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 7].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                        incRowExcel = incRowExcel + 2;


                                        //ATUR DATA GROUPINGNYA DULU
                                        List<LaporanRekapTotalNAB> rList = new List<LaporanRekapTotalNAB>();
                                        while (dr0.Read())
                                        {
                                            LaporanRekapTotalNAB rSingle = new LaporanRekapTotalNAB();
                                            rSingle.IFUACode = dr0["IFUACode"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IFUACode"]);
                                            rSingle.SID = dr0["SID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["SID"]);
                                            rSingle.InvestorName = dr0["Name"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["Name"]);
                                            rSingle.UnitAmount = dr0["UnitAmount"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["UnitAmount"]);
                                            rSingle.Fund = dr0["FundName"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["FundName"]);
                                            rSingle.NABPerUnit = dr0["NABPerUnit"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["NABPerUnit"]);
                                            rSingle.TotalNAB = dr0["TotalNAB"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["TotalNAB"]);
                                            rList.Add(rSingle);

                                        }

                                        var QueryByFundID =
                                                     from r in rList
                                                     group r by new { r.Fund } into rGroup
                                                     select rGroup;


                                        foreach (var rsHeader in QueryByFundID)
                                        {

                                            worksheet.Cells[incRowExcel, 1].Value = "FUND : ";
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 1].Merge = true;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 1].Style.Font.Size = 10;
                                            worksheet.Cells[incRowExcel, 2].Value = rsHeader.Key.Fund;
                                            worksheet.Cells[incRowExcel, 2, incRowExcel, 5].Merge = true;
                                            worksheet.Cells[incRowExcel, 2, incRowExcel, 5].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 2, incRowExcel, 5].Style.Font.Size = 10;

                                            incRowExcel = incRowExcel + 2;

                                            worksheet.Cells[incRowExcel, 1].Value = "NO";
                                            worksheet.Cells[incRowExcel, 2].Value = "SID";
                                            worksheet.Cells[incRowExcel, 2].Style.WrapText = true;
                                            worksheet.Cells[incRowExcel, 3].Value = "IFUA";
                                            worksheet.Cells[incRowExcel, 4].Value = "NAMA INVESTOR";
                                            worksheet.Cells[incRowExcel, 5].Value = "TOTAL UNIT PENYERTAAN";
                                            worksheet.Cells[incRowExcel, 6].Value = "NAB/UNIT";
                                            worksheet.Cells[incRowExcel, 7].Value = "TOTAL NAB";

                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 7].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 7].Style.Font.Size = 8;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 7].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 7].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(251, 228, 213));
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 7].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 7].Style.Border.Top.Style = ExcelBorderStyle.Double;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 7].Style.Border.Bottom.Style = ExcelBorderStyle.Double;
                                            worksheet.Cells["A" + incRowExcel + ":G" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells["G" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                                            incRowExcel = incRowExcel + 1;

                                            int first = incRowExcel;

                                            int no = 1;

                                            foreach (var rsDetail in rsHeader)
                                            {
                                                worksheet.Cells[incRowExcel, 1].Value = no;
                                                worksheet.Cells[incRowExcel, 1].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 1].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 2].Value = rsDetail.SID;
                                                worksheet.Cells[incRowExcel, 2, incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 2, incRowExcel, 2].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.IFUACode;
                                                worksheet.Cells[incRowExcel, 3, incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 3, incRowExcel, 3].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 4].Value = rsDetail.InvestorName;
                                                worksheet.Cells[incRowExcel, 4].Style.WrapText = true;
                                                worksheet.Cells[incRowExcel, 4, incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                worksheet.Cells[incRowExcel, 4, incRowExcel, 4].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.0000";
                                                worksheet.Cells[incRowExcel, 5].Value = rsDetail.UnitAmount;
                                                worksheet.Cells[incRowExcel, 5, incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                worksheet.Cells[incRowExcel, 5, incRowExcel, 5].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                //worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                //worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                                                worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.0000";
                                                worksheet.Cells[incRowExcel, 6].Value = rsDetail.NABPerUnit;
                                                worksheet.Cells[incRowExcel, 6, incRowExcel, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                worksheet.Cells[incRowExcel, 6, incRowExcel, 6].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                //worksheet.Cells[incRowExcel, 1, incRowExcel, 6].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                //worksheet.Cells[incRowExcel, 1, incRowExcel, 6].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                                                worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0.0000";
                                                worksheet.Cells[incRowExcel, 7].Value = rsDetail.TotalNAB;
                                                worksheet.Cells[incRowExcel, 7, incRowExcel, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                worksheet.Cells[incRowExcel, 7, incRowExcel, 7].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 7].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 7].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 7].Style.Font.Size = 8;


                                                incRowExcel++;
                                                no++;
                                            }

                                            int last = incRowExcel - 1;

                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 7].Style.Border.Top.Style = ExcelBorderStyle.Thin;

                                            incRowExcel++;

                                            worksheet.Cells[incRowExcel, 4].Value = "TOTAL UNIT PENYERTAAN :";
                                            worksheet.Cells[incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                                            worksheet.Cells[incRowExcel, 6].Value = "TOTAL NAB    :";
                                            worksheet.Cells[incRowExcel, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                                            worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.0000";
                                            worksheet.Cells[incRowExcel, 5].Formula = "SUM(E" + first.ToString() + ":E" + last.ToString() + ")";
                                            worksheet.Cells[incRowExcel, 5].Calculate();
                                            worksheet.Cells[incRowExcel, 5].Style.Border.Top.Style = ExcelBorderStyle.Double;
                                            worksheet.Cells[incRowExcel, 5].Style.Border.Bottom.Style = ExcelBorderStyle.Double;

                                            worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0.0000";
                                            worksheet.Cells[incRowExcel, 7].Formula = "SUM(G" + first.ToString() + ":G" + last.ToString() + ")";
                                            worksheet.Cells[incRowExcel, 7].Calculate();
                                            worksheet.Cells[incRowExcel, 7].Style.Border.Top.Style = ExcelBorderStyle.Double;
                                            worksheet.Cells[incRowExcel, 7].Style.Border.Bottom.Style = ExcelBorderStyle.Double;

                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 7].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 7].Style.Font.Size = 8;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 7].Style.VerticalAlignment = ExcelVerticalAlignment.Center;



                                            //worksheet.Cells.Calculate();
                                            incRowExcel++;

                                            incRowExcel = incRowExcel + 2;
                                        }

                                        incRowExcel = incRowExcel + 4;

                                        worksheet.Cells[incRowExcel, 1].Value = "Disclaimer : ";
                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Merge = true;
                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Size = 12;

                                        incRowExcel++;

                                        worksheet.Cells[incRowExcel, 1].Value = "Dokumen ini dipersiapkan oleh PT SUCOR SEKURITAS dan hanya bisa digunakan untuk kepentingan investor tersebut diatas dan tidak untuk pihak lainnya. Laporan ini bukan merupakan konfirmasi dari PT SUCOR SEKURITAS dan tidak untuk menggantikan laporan yang wajib diterbitkan oleh Bank Kustodian, jika ada perbedaan antara laporan ini dengan laporan Bank Kustodian, maka laporan Bank Kustodian adalah yang benar. Laporan ini diproses oleh computer dan tidak memerlukan tandatangan.";
                                        worksheet.Cells[incRowExcel, 1, incRowExcel + 2, 7].Style.Font.Size = 11;
                                        worksheet.Cells[incRowExcel, 1, incRowExcel + 2, 7].Merge = true;
                                        worksheet.Cells[incRowExcel, 1, incRowExcel + 2, 7].Style.WrapText = true;

                                        incRowExcel = incRowExcel + 4;

                                        worksheet.Cells[incRowExcel, 1].Value = "PT SUCOR SEKURITAS terdaftar dan diawasi oleh OJK, dengan nomor registrasi KEP-01/PM/MI/1999";
                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 7].Style.Font.Size = 11;
                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 7].Merge = true;

                                        incRowExcel = incRowExcel + 2;

                                        worksheet.PrinterSettings.FitToPage = true;
                                        worksheet.PrinterSettings.FitToWidth = 1;
                                        worksheet.PrinterSettings.FitToHeight = 0;
                                        worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel - 1, 7];
                                        worksheet.Column(1).Width = 7;
                                        worksheet.Column(2).Width = 20;
                                        worksheet.Column(3).Width = 20;
                                        worksheet.Column(4).Width = 45;
                                        worksheet.Column(5).Width = 20;
                                        worksheet.Column(6).Width = 20;
                                        worksheet.Column(7).Width = 20;

                                        // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                        //worksheet.Cells.AutoFitColumns(); // UNTUK AUTO FIT UKURAN TULISAN DENGAN KOLOM, KEBETULAN INI BUAT HEADER AJA DIPAKENYA
                                        //worksheet.PrinterSettings.FitToPage = true;
                                        worksheet.PrinterSettings.Orientation = eOrientation.Portrait; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                        worksheet.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN
                                        worksheet.HeaderFooter.OddHeader.CenteredText = "&14 LAPORAN TOTAL NAB \n " + "&12 REKSA DANA " + _host.Get_CompanyName();

                                        // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                        //ExcelWorksheet ws = package.Workbook.Worksheets.Add("Demo");

                                        worksheet.PrinterSettings.TopMargin = (decimal).3 / 2.54M; // narrow border
                                        worksheet.PrinterSettings.RightMargin = (decimal).3 / 2.54M; //narrow borderExcelWorksheet ws = pck.Workbook.Worksheets.Add("Demo");
                                        worksheet.PrinterSettings.LeftMargin = (decimal).3 / 2.54M; // narrow border

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

                                        //worksheet.HeaderFooter.OddFooter.CenteredText = Tools.DefaultReportFooterCenterText();
                                        worksheet.HeaderFooter.OddFooter.LeftAlignedText = Tools.DefaultReportFooterLeftText();

                                        package.Save();
                                        if (_unitRegistryRpt.DownloadMode == "PDF")
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

            #region Laporan Rekap Unit Penyertaan
            else if (_unitRegistryRpt.ReportName.Equals("Laporan Rekap Unit Penyertaan"))
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
                                _paramFund = "And A.FundPK in ( " + _unitRegistryRpt.FundFrom + " ) ";
                            }
                            else
                            {
                                _paramFund = "";
                            }
                            if (!_host.findString(_unitRegistryRpt.FundClientFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_unitRegistryRpt.FundClientFrom))
                            {
                                _paramFundClient = "And A.FundClientPK  in ( " + _unitRegistryRpt.FundClientFrom + " ) ";
                            }
                            else
                            {
                                _paramFundClient = "";
                            }


                            cmd.CommandText =

                            @"
                            Select C.Name FundName,B.SID,B.IFUACode,B.Name,A.UnitAmount from FundClientPosition A
                            left join FundClient B on A.FundClientPK = B.FundClientPK and B.status = 2
                            left join Fund C on A.FundPK = C.FundPK and C.status = 2
                            where A.Date = @DateTo " + _paramFund;



                            cmd.CommandTimeout = 0;
                            cmd.Parameters.AddWithValue("@DateTo", _unitRegistryRpt.ValueDateTo);
                            cmd.Parameters.AddWithValue("@FundFrom", _unitRegistryRpt.FundFrom);
                            cmd.Parameters.AddWithValue("@FundClientFrom", _unitRegistryRpt.FundClientFrom);



                            using (SqlDataReader dr0 = cmd.ExecuteReader())
                            {
                                if (!dr0.HasRows)
                                {
                                    return false;
                                }
                                else
                                {
                                    string filePath = Tools.ReportsPath + "LaporanRekapUnitPenyertaan" + "_" + _userID + ".xlsx";
                                    string pdfPath = Tools.ReportsPath + "LaporanRekapUnitPenyertaan" + "_" + _userID + ".pdf";
                                    FileInfo excelFile = new FileInfo(filePath);
                                    if (excelFile.Exists)
                                    {
                                        excelFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                        excelFile = new FileInfo(filePath);
                                    }

                                    // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                    using (ExcelPackage package = new ExcelPackage(excelFile))
                                    {
                                        package.Workbook.Properties.Title = "LaporanRekapUnitPenyertaan_";
                                        package.Workbook.Properties.Author = Tools.DefaultReportAuthor();
                                        package.Workbook.Properties.Comments = Tools.DefaultReportComments();
                                        package.Workbook.Properties.Company = Tools.DefaultReportCompany();
                                        package.Workbook.Properties.SetCustomPropertyValue("Checked by", _userID);
                                        package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", Tools.DefaultReportAssemblyName());

                                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("LaporanRekapUnitPenyertaan");

                                        int incRowExcel = 1;

                                        worksheet.Cells[incRowExcel, 1].Value = "Periode: " + Convert.ToDateTime(_unitRegistryRpt.ValueDateFrom).ToShortDateString(); // + " s/d " + Convert.ToDateTime(_unitRegistryRpt.ValueDateTo).ToShortDateString();
                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Merge = true;
                                        worksheet.Cells[incRowExcel, 1].Style.Font.UnderLine = true;
                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Size = 9;
                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                        incRowExcel = incRowExcel + 2;


                                        //ATUR DATA GROUPINGNYA DULU
                                        List<RekapUnitPenyertaan> rList = new List<RekapUnitPenyertaan>();
                                        while (dr0.Read())
                                        {
                                            RekapUnitPenyertaan rSingle = new RekapUnitPenyertaan();
                                            rSingle.IFUACode = dr0["IFUACode"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IFUACode"]);
                                            rSingle.SID = dr0["SID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["SID"]);
                                            rSingle.InvestorName = dr0["Name"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["Name"]);
                                            rSingle.UnitAmount = dr0["UnitAmount"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr0["UnitAmount"]);
                                            rSingle.Fund = dr0["FundName"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["FundName"]);
                                            rList.Add(rSingle);

                                        }

                                        var QueryByFundID =
                                                     from r in rList
                                                     group r by new { r.Fund } into rGroup
                                                     select rGroup;


                                        foreach (var rsHeader in QueryByFundID)
                                        {

                                            worksheet.Cells[incRowExcel, 1].Value = "FUND : ";
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 1].Merge = true;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 1].Style.Font.Size = 10;
                                            worksheet.Cells[incRowExcel, 2].Value = rsHeader.Key.Fund;
                                            worksheet.Cells[incRowExcel, 2, incRowExcel, 5].Merge = true;
                                            worksheet.Cells[incRowExcel, 2, incRowExcel, 5].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 2, incRowExcel, 5].Style.Font.Size = 10;

                                            incRowExcel = incRowExcel + 2;

                                            worksheet.Cells[incRowExcel, 1].Value = "NO";
                                            worksheet.Cells[incRowExcel, 2].Value = "SID";
                                            worksheet.Cells[incRowExcel, 2].Style.WrapText = true;
                                            worksheet.Cells[incRowExcel, 3].Value = "IFUA";
                                            worksheet.Cells[incRowExcel, 4].Value = "NAMA INVESTOR";
                                            worksheet.Cells[incRowExcel, 5].Value = "TOTAL UNIT PENYERTAAN";

                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Size = 8;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(251, 228, 213));
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Border.Top.Style = ExcelBorderStyle.Double;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Border.Bottom.Style = ExcelBorderStyle.Double;
                                            worksheet.Cells["A" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells["B" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells["C" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells["D" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells["E" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells["E" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                                            incRowExcel = incRowExcel + 1;

                                            int first = incRowExcel;

                                            int no = 1;

                                            foreach (var rsDetail in rsHeader)
                                            {
                                                worksheet.Cells[incRowExcel, 1].Value = no;
                                                worksheet.Cells[incRowExcel, 1].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 1].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 2].Value = rsDetail.SID;
                                                worksheet.Cells[incRowExcel, 2, incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 2, incRowExcel, 2].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.IFUACode;
                                                worksheet.Cells[incRowExcel, 3, incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 3, incRowExcel, 3].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 4].Value = rsDetail.InvestorName;
                                                worksheet.Cells[incRowExcel, 4].Style.WrapText = true;
                                                worksheet.Cells[incRowExcel, 4, incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                worksheet.Cells[incRowExcel, 4, incRowExcel, 4].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.0000";
                                                worksheet.Cells[incRowExcel, 5].Value = rsDetail.UnitAmount;
                                                worksheet.Cells[incRowExcel, 5, incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                worksheet.Cells[incRowExcel, 5, incRowExcel, 5].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Size = 8;


                                                incRowExcel++;
                                                no++;
                                            }

                                            int last = incRowExcel - 1;

                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Border.Top.Style = ExcelBorderStyle.Thin;

                                            incRowExcel++;
                                            incRowExcel = incRowExcel + 1;

                                            worksheet.Cells[incRowExcel, 4].Value = "TOTAL UNIT PENYERTAAN :";
                                            worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.0000";
                                            worksheet.Cells[incRowExcel, 5].Formula = "SUM(E" + first.ToString() + ":E" + last.ToString() + ")";
                                            worksheet.Cells[incRowExcel, 5].Calculate();

                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Size = 8;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 4, incRowExcel, 5].Style.Border.Top.Style = ExcelBorderStyle.Double;
                                            worksheet.Cells[incRowExcel, 4, incRowExcel, 5].Style.Border.Bottom.Style = ExcelBorderStyle.Double;

                                            //worksheet.Cells.Calculate();
                                            incRowExcel++;

                                            incRowExcel = incRowExcel + 2;
                                        }

                                        incRowExcel = incRowExcel + 4;

                                        worksheet.Cells[incRowExcel, 1].Value = "Disclaimer : ";
                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Merge = true;
                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Size = 12;

                                        incRowExcel++;

                                        worksheet.Cells[incRowExcel, 1].Value = "Dokumen ini dipersiapkan oleh PT SUCOR SEKURITAS dan hanya bisa digunakan untuk kepentingan investor tersebut diatas dan tidak untuk pihak lainnya. Laporan ini bukan merupakan konfirmasi dari PT SUCOR SEKURITAS dan tidak untuk menggantikan laporan yang wajib diterbitkan oleh Bank Kustodian, jika ada perbedaan antara laporan ini dengan laporan Bank Kustodian, maka laporan Bank Kustodian adalah yang benar. Laporan ini diproses oleh computer dan tidak memerlukan tandatangan.";
                                        worksheet.Cells[incRowExcel, 1, incRowExcel + 2, 5].Style.Font.Size = 11;
                                        worksheet.Cells[incRowExcel, 1, incRowExcel + 2, 5].Merge = true;
                                        worksheet.Cells[incRowExcel, 1, incRowExcel + 2, 5].Style.WrapText = true;

                                        incRowExcel = incRowExcel + 4;

                                        worksheet.Cells[incRowExcel, 1].Value = "PT SUCOR SEKURITAS terdaftar dan diawasi oleh OJK, dengan nomor registrasi KEP-01/PM/MI/1999";
                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Size = 11;
                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Merge = true;

                                        incRowExcel = incRowExcel + 2;

                                        worksheet.PrinterSettings.FitToPage = true;
                                        worksheet.PrinterSettings.FitToWidth = 1;
                                        worksheet.PrinterSettings.FitToHeight = 0;
                                        worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel - 1, 5];
                                        worksheet.Column(1).Width = 7;
                                        worksheet.Column(2).Width = 20;
                                        worksheet.Column(3).Width = 20;
                                        worksheet.Column(4).Width = 45;
                                        worksheet.Column(5).Width = 20;


                                        // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                        //worksheet.Cells.AutoFitColumns(); // UNTUK AUTO FIT UKURAN TULISAN DENGAN KOLOM, KEBETULAN INI BUAT HEADER AJA DIPAKENYA
                                        //worksheet.PrinterSettings.FitToPage = true;
                                        worksheet.PrinterSettings.Orientation = eOrientation.Portrait; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                        worksheet.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN
                                        worksheet.HeaderFooter.OddHeader.CenteredText = "&14 LAPORAN REKAP UNIT PENYERTAAN \n " + "&12 REKSA DANA " + _host.Get_CompanyName();

                                        // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                        //ExcelWorksheet ws = package.Workbook.Worksheets.Add("Demo");

                                        worksheet.PrinterSettings.TopMargin = (decimal).3 / 2.54M; // narrow border
                                        worksheet.PrinterSettings.RightMargin = (decimal).3 / 2.54M; //narrow borderExcelWorksheet ws = pck.Workbook.Worksheets.Add("Demo");
                                        worksheet.PrinterSettings.LeftMargin = (decimal).3 / 2.54M; // narrow border

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

                                        //worksheet.HeaderFooter.OddFooter.CenteredText = Tools.DefaultReportFooterCenterText();
                                        worksheet.HeaderFooter.OddFooter.LeftAlignedText = Tools.DefaultReportFooterLeftText();

                                        package.Save();
                                        if (_unitRegistryRpt.DownloadMode == "PDF")
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

            #region Laporan Rekap Penjualan Harian
            else if (_unitRegistryRpt.ReportName.Equals("Laporan Rekap Penjualan Harian"))
            {
                try
                {
                    using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                    {
                        DbCon.Open();
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {

                            cmd.CommandText = @"
                            declare @FundID nvarchar(50)

                            DECLARE @cols AS NVARCHAR(MAX),@colsForQuery AS NVARCHAR(MAX),
                            @query  AS NVARCHAR(MAX)


                            Create Table #Fund
                            (
                            FundID nvarchar(50)
                            )
                            insert into #Fund (FundID)
                            Select distinct FundID from (
                            Select B.Name,C.ID FundID, A.CashAmount from ClientSubscription A
                            Left join FundClient B on A.FundClientPK = B.FundClientPK and B.status = 2 
                            left join Fund C on A.FundPK = C.FundPK and C.status = 2
                            where A.Posted = 1 and A.Revised = 0 and A.status = 2 and ValueDate = @DateFrom
                            UNION ALL
                            Select B.Name,C.ID FundID, A.CashAmount * -1 from ClientRedemption A
                            Left join FundClient B on A.FundClientPK = B.FundClientPK and B.status = 2 
                            left join Fund C on A.FundPK = C.FundPK and C.status = 2
                            where A.Posted = 1 and A.Revised = 0 and A.status = 2 and ValueDate = @DateFrom
                            )A

                            select @colsForQuery = STUFF((SELECT ',isnull(' + QUOTENAME(FundID) +',0) ' + QUOTENAME(FundID) 
                            from #Fund
                            Order by FundID
                            FOR XML PATH(''), TYPE
                            ).value('.', 'NVARCHAR(MAX)') 
                            ,1,1,'')

                            select @cols = STUFF((SELECT ',' + QUOTENAME(FundID) 
                            from #Fund
                            Order by FundID
                            FOR XML PATH(''), TYPE
                            ).value('.', 'NVARCHAR(MAX)') 
                            ,1,1,'')

                            set @query = 'select row_number() OVER (ORDER BY Name) NO , Name as [NAMA KLIEN],' + @colsForQuery + ', 0 as [NET SALES] from 
                            (

                            Select A.Name ,A.FundID,Sum(A.CashAmount) NettSales from (
                            Select B.Name,C.ID FundID, A.CashAmount from ClientSubscription A
                            Left join FundClient B on A.FundClientPK = B.FundClientPK and B.status = 2 
                            left join Fund C on A.FundPK = C.FundPK and C.status = 2
                            where A.Posted = 1 and A.Revised = 0 and A.status = 2 and ValueDate = ''' + @DateFrom + '''
                            UNION ALL
                            Select B.Name,C.ID FundID, A.CashAmount * -1 from ClientRedemption A
                            Left join FundClient B on A.FundClientPK = B.FundClientPK and B.status = 2 
                            left join Fund C on A.FundPK = C.FundPK and C.status = 2
                            where A.Posted = 1 and A.Revised = 0 and A.status = 2 and ValueDate = ''' + @DateFrom + '''
                            )A
                            Group By A.Name,A.FundID
                            ) x
                            pivot 
                            (
                            AVG(NettSales)
                            for FundID in (' + @cols + ')
                            ) p 
                            '
                            Exec(@query)";
                            cmd.CommandTimeout = 0;
                            cmd.Parameters.AddWithValue("@DateFrom", _unitRegistryRpt.ValueDateFrom);
                            using (SqlDataReader dr0 = cmd.ExecuteReader())
                            {
                                if (!dr0.HasRows)
                                {
                                    return false;
                                }
                                else
                                {
                                    string filePath = Tools.ReportsPath + "LaporanRekapPenjualanHarian" + "_" + _userID + ".xlsx";
                                    string pdfPath = Tools.ReportsPath + "LaporanRekapPenjualanHarian" + "_" + _userID + ".pdf";

                                    FileInfo excelFile = new FileInfo(filePath);
                                    if (excelFile.Exists)
                                    {
                                        excelFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                        excelFile = new FileInfo(filePath);
                                    }

                                    // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                    using (ExcelPackage package = new ExcelPackage(excelFile))
                                    {
                                        package.Workbook.Properties.Title = "LaporanRekapPenjualanHarian";
                                        package.Workbook.Properties.Author = Tools.DefaultReportAuthor();
                                        package.Workbook.Properties.Comments = Tools.DefaultReportComments();
                                        package.Workbook.Properties.Company = Tools.DefaultReportCompany();
                                        package.Workbook.Properties.SetCustomPropertyValue("Checked by", _userID);
                                        package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", Tools.DefaultReportAssemblyName());

                                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Laporan Rekap Penjualan Harian");

                                        int incRowExcel = 1;

                                        worksheet.Cells[incRowExcel, 3].Value = "LAPORAN REKAP PENJUALAN HARIAN";
                                        worksheet.Cells[incRowExcel, 3, incRowExcel, 6].Merge = true;
                                        worksheet.Cells[incRowExcel, 3, incRowExcel, 6].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 3, incRowExcel, 6].Style.Font.Size = 14;
                                        worksheet.Cells[incRowExcel, 3, incRowExcel, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                        incRowExcel++;

                                        worksheet.Cells[incRowExcel, 3].Value = "REKSA DANA " + _host.Get_CompanyName();
                                        worksheet.Cells[incRowExcel, 3, incRowExcel, 6].Merge = true;
                                        worksheet.Cells[incRowExcel, 3, incRowExcel, 6].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 3, incRowExcel, 6].Style.Font.Size = 14;
                                        worksheet.Cells[incRowExcel, 3, incRowExcel, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                        incRowExcel = incRowExcel + 2;
                                        worksheet.Cells[incRowExcel, 3].Value = "Posisi per tanggal: " + Convert.ToDateTime(_unitRegistryRpt.ValueDateFrom).ToString("dd-MMMM-yyyy");
                                        worksheet.Cells[incRowExcel, 3, incRowExcel, 6].Merge = true;
                                        worksheet.Cells[incRowExcel, 3, incRowExcel, 6].Style.Font.Size = 12;
                                        worksheet.Cells[incRowExcel, 3, incRowExcel, 6].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 3, incRowExcel, 6].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                        worksheet.Cells[incRowExcel, 3, incRowExcel, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                        incRowExcel = incRowExcel + 3;

                                        int _startRowDetail = incRowExcel;
                                        int _rowCtrp = 0;
                                        int _endRowDetail = 0;
                                        int _totalRow = _host.Get_RowTotalCashAmountSubsRedempByDate(_unitRegistryRpt.ValueDateFrom) + 7;
                                        int _totalEndRow = _totalRow - 1;

                                        while (dr0.Read())
                                        {
                                            int incColExcel = 1;
                                            int colNettsales = dr0.FieldCount;

                                            for (int inc1 = 0; inc1 < dr0.FieldCount; inc1++)
                                            {
                                                //HEADER
                                                worksheet.Cells[6, incColExcel].Value = dr0.GetName(inc1);
                                                worksheet.Cells[6, incColExcel].Style.Font.Bold = true;
                                                worksheet.Cells[6, incColExcel].Style.WrapText = true;
                                                worksheet.Cells[6, incColExcel].Style.Border.Top.Style = ExcelBorderStyle.Double;
                                                worksheet.Cells[6, incColExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Double;
                                                worksheet.Cells[6, incColExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[6, incColExcel].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells[6, incColExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells[6, incColExcel].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                                                if (incColExcel == 2)
                                                {
                                                    worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToString(dr0.GetValue(inc1));
                                                    worksheet.Cells[incRowExcel, incColExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                    worksheet.Cells[incRowExcel, incColExcel].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                    worksheet.Cells[incRowExcel, incColExcel].Style.WrapText = true;
                                                    worksheet.Column(incColExcel).Width = 30;

                                                }
                                                else if (incColExcel >= 3)
                                                {
                                                    _rowCtrp = incColExcel;
                                                    worksheet.Cells[incRowExcel, incColExcel].Value = dr0.GetValue(inc1);
                                                    worksheet.Cells[incRowExcel, incColExcel].Style.Numberformat.Format = "#,##0";
                                                    worksheet.Column(incColExcel).Width = 20;
                                                    worksheet.Cells[incRowExcel, incColExcel].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                    ////Total Row
                                                    worksheet.Cells[_totalRow, _rowCtrp].Formula = "SUM(" + _host.IntToLetters(_rowCtrp) + "7:" + _host.IntToLetters(_rowCtrp) + _totalEndRow + ")";
                                                    worksheet.Cells[_totalRow, _rowCtrp].Style.Numberformat.Format = "#,##0";
                                                    worksheet.Cells[_totalRow, _rowCtrp].Style.Border.Top.Style = ExcelBorderStyle.Double;
                                                    worksheet.Cells[_totalRow, _rowCtrp].Style.Border.Bottom.Style = ExcelBorderStyle.Double;
                                                    worksheet.Cells[_totalRow, _rowCtrp].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                    worksheet.Cells[_totalRow, _rowCtrp].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                                    worksheet.Cells[_totalRow, _rowCtrp].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                    worksheet.Cells[_totalRow, _rowCtrp].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                                }
                                                else
                                                {
                                                    worksheet.Cells[incRowExcel, incColExcel].Value = dr0.GetValue(inc1).ToString();
                                                    worksheet.Cells[incRowExcel, incColExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                    worksheet.Cells[incRowExcel, incColExcel].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                }

                                                worksheet.Row(incRowExcel).Height = 30;

                                                worksheet.Cells[6, incColExcel].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                                worksheet.Cells[6, incColExcel].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(251, 228, 213));

                                                worksheet.Cells[incRowExcel, incColExcel].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells[incRowExcel, incColExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                                                worksheet.Cells[incRowExcel, incColExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells[incRowExcel, incColExcel].Style.Border.Right.Style = ExcelBorderStyle.Thin;


                                                incColExcel++;

                                            }


                                            //Total Column disamping

                                            worksheet.Cells[incRowExcel, colNettsales].Formula = "SUM(C" + incRowExcel + ":" + _host.IntToLetters(colNettsales - 1) + incRowExcel + ")";
                                            worksheet.Cells[incRowExcel, colNettsales].Style.Numberformat.Format = "#,##0";
                                            worksheet.Cells[incRowExcel, colNettsales].Calculate();

                                            incRowExcel++;



                                            _endRowDetail = incRowExcel - 1;


                                            worksheet.Column(incColExcel).Width = 20;

                                        }


                                        worksheet.Cells[incRowExcel, 2].Value = "TOTAL : ";
                                        worksheet.Cells[incRowExcel, 2].Style.Border.Top.Style = ExcelBorderStyle.Double;
                                        worksheet.Cells[incRowExcel, 2].Style.Border.Bottom.Style = ExcelBorderStyle.Double;
                                        worksheet.Cells[incRowExcel, 2].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                        worksheet.Cells[incRowExcel, 2].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                                        incRowExcel = incRowExcel + 4;

                                        worksheet.Cells[incRowExcel, 1].Value = "Disclaimer : ";
                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Merge = true;
                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Size = 12;

                                        incRowExcel++;

                                        worksheet.Cells[incRowExcel, 1].Value = "Dokumen ini dipersiapkan oleh PT SUCOR SEKURITAS dan hanya bisa digunakan untuk kepentingan investor tersebut diatas dan tidak untuk pihak lainnya. Laporan ini bukan merupakan konfirmasi dari PT SUCOR SEKURITAS dan tidak untuk menggantikan laporan yang wajib diterbitkan oleh Bank Kustodian, jika ada perbedaan antara laporan ini dengan laporan Bank Kustodian, maka laporan Bank Kustodian adalah yang benar. Laporan ini diproses oleh computer dan tidak memerlukan tandatangan.";
                                        worksheet.Cells[incRowExcel, 1, incRowExcel + 2, 6].Style.Font.Size = 11;
                                        worksheet.Cells[incRowExcel, 1, incRowExcel + 2, 6].Merge = true;
                                        worksheet.Cells[incRowExcel, 1, incRowExcel + 2, 6].Style.WrapText = true;

                                        incRowExcel = incRowExcel + 4;

                                        worksheet.Cells[incRowExcel, 1].Value = "PT SUCOR SEKURITAS terdaftar dan diawasi oleh OJK, dengan nomor registrasi KEP-01/PM/MI/1999";
                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 6].Style.Font.Size = 11;
                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 6].Merge = true;

                                        incRowExcel = incRowExcel + 2;

                                        // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                        //worksheet.Cells["A1:Z1"].AutoFitColumns(); // UNTUK AUTO FIT UKURAN TULISAN DENGAN KOLOM, KEBETULAN INI BUAT HEADER AJA DIPAKENYA
                                        //worksheet.Cells["K2:Z2"].AutoFitColumns(); // CEK DARI ENTRY ID SAMPE LAST UPDATE
                                        worksheet.PrinterSettings.Orientation = eOrientation.Landscape; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                        worksheet.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN
                                        //worksheet.HeaderFooter.OddHeader.RightAlignedText = "&14 COUNTERPART PRECENTAGE";

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

            #region Laporan Rekap Penjualan MTD
            else if (_unitRegistryRpt.ReportName.Equals("Laporan Rekap Penjualan MTD"))
            {
                try
                {
                    using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                    {
                        DbCon.Open();
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {

                            cmd.CommandText = @"
                            declare @FundID nvarchar(50)

                            DECLARE @cols AS NVARCHAR(MAX),@colsForQuery AS NVARCHAR(MAX),
                            @query  AS NVARCHAR(MAX)


                            Create Table #Fund
                            (
                            FundID nvarchar(50)
                            )
                            insert into #Fund (FundID)
                            Select distinct FundID from (
                            Select B.Name,C.ID FundID, A.CashAmount from ClientSubscription A
                            Left join FundClient B on A.FundClientPK = B.FundClientPK and B.status = 2 
                            left join Fund C on A.FundPK = C.FundPK and C.status = 2
                            where A.Posted = 1 and A.Revised = 0 and A.status = 2 and ValueDate between @DateFrom and @DateTo
                            UNION ALL
                            Select B.Name,C.ID FundID, A.CashAmount * -1 from ClientRedemption A
                            Left join FundClient B on A.FundClientPK = B.FundClientPK and B.status = 2 
                            left join Fund C on A.FundPK = C.FundPK and C.status = 2
                            where A.Posted = 1 and A.Revised = 0 and A.status = 2 and ValueDate between @DateFrom and @DateTo
                            )A

                            select @colsForQuery = STUFF((SELECT ',isnull(' + QUOTENAME(FundID) +',0) ' + QUOTENAME(FundID) 
                            from #Fund
                            Order by FundID
                            FOR XML PATH(''), TYPE
                            ).value('.', 'NVARCHAR(MAX)') 
                            ,1,1,'')

                            select @cols = STUFF((SELECT ',' + QUOTENAME(FundID) 
                            from #Fund
                            Order by FundID
                            FOR XML PATH(''), TYPE
                            ).value('.', 'NVARCHAR(MAX)') 
                            ,1,1,'')

                            set @query = 'select row_number() OVER (ORDER BY Name) NO , Name as [NAMA KLIEN],' + @colsForQuery + ', 0 as [NET SALES] from 
                            (

                            Select A.Name ,A.FundID,Sum(A.CashAmount) NettSales from (
                            Select B.Name,C.ID FundID, A.CashAmount from ClientSubscription A
                            Left join FundClient B on A.FundClientPK = B.FundClientPK and B.status = 2 
                            left join Fund C on A.FundPK = C.FundPK and C.status = 2
                            where A.Posted = 1 and A.Revised = 0 and A.status = 2 and ValueDate between ''' + @DateFrom + ''' and ''' + @DateTo + '''
                            UNION ALL
                            Select B.Name,C.ID FundID, A.CashAmount * -1 from ClientRedemption A
                            Left join FundClient B on A.FundClientPK = B.FundClientPK and B.status = 2 
                            left join Fund C on A.FundPK = C.FundPK and C.status = 2
                            where A.Posted = 1 and A.Revised = 0 and A.status = 2 and ValueDate between ''' + @DateFrom + ''' and ''' + @DateTo + '''
                            )A
                            Group By A.Name,A.FundID
                            ) x
                            pivot 
                            (
                            AVG(NettSales)
                            for FundID in (' + @cols + ')
                            ) p 
                            '
                            Exec(@query)";
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
                                    string filePath = Tools.ReportsPath + "LaporanRekapPenjualanMTD" + "_" + _userID + ".xlsx";
                                    string pdfPath = Tools.ReportsPath + "LaporanRekapPenjualanMTD" + "_" + _userID + ".pdf";
                                    FileInfo excelFile = new FileInfo(filePath);
                                    if (excelFile.Exists)
                                    {
                                        excelFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                        excelFile = new FileInfo(filePath);
                                    }

                                    // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                    using (ExcelPackage package = new ExcelPackage(excelFile))
                                    {
                                        package.Workbook.Properties.Title = "LaporanRekapPenjualanHarian";
                                        package.Workbook.Properties.Author = Tools.DefaultReportAuthor();
                                        package.Workbook.Properties.Comments = Tools.DefaultReportComments();
                                        package.Workbook.Properties.Company = Tools.DefaultReportCompany();
                                        package.Workbook.Properties.SetCustomPropertyValue("Checked by", _userID);
                                        package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", Tools.DefaultReportAssemblyName());

                                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Laporan Rekap Penjualan MTD");

                                        int incRowExcel = 1;

                                        worksheet.Cells[incRowExcel, 3].Value = "LAPORAN REKAP PENJUALAN MONTH TO DATE (MTD)";
                                        worksheet.Cells[incRowExcel, 3, incRowExcel, 6].Merge = true;
                                        worksheet.Cells[incRowExcel, 3, incRowExcel, 6].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 3, incRowExcel, 6].Style.Font.Size = 18;
                                        worksheet.Cells[incRowExcel, 3, incRowExcel, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                        incRowExcel++;

                                        worksheet.Cells[incRowExcel, 3].Value = "REKSA DANA " + _host.Get_CompanyName();
                                        worksheet.Cells[incRowExcel, 3, incRowExcel, 6].Merge = true;
                                        worksheet.Cells[incRowExcel, 3, incRowExcel, 6].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 3, incRowExcel, 6].Style.Font.Size = 18;
                                        worksheet.Cells[incRowExcel, 3, incRowExcel, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                        incRowExcel = incRowExcel + 2;

                                        worksheet.Cells[incRowExcel, 3].Value = "Period : " + Convert.ToDateTime(_unitRegistryRpt.ValueDateFrom).ToString("dd-MMMM-yyyy") + " s/d " + Convert.ToDateTime(_unitRegistryRpt.ValueDateTo).ToString("dd-MMMM-yyyy");
                                        worksheet.Cells[incRowExcel, 3, incRowExcel, 6].Merge = true;
                                        worksheet.Cells[incRowExcel, 3, incRowExcel, 6].Style.Font.Size = 12;
                                        worksheet.Cells[incRowExcel, 3, incRowExcel, 6].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 3, incRowExcel, 6].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                        worksheet.Cells[incRowExcel, 3, incRowExcel, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                        incRowExcel = incRowExcel + 3;

                                        int _startRowDetail = incRowExcel;
                                        int _rowCtrp = 0;
                                        int _endRowDetail = 0;
                                        int _totalRow = _host.Get_RowTotalCashAmountSubsRedempByDateFromTo(_unitRegistryRpt.ValueDateFrom, _unitRegistryRpt.ValueDateTo) + 7;
                                        int _totalEndRow = _totalRow - 1;

                                        while (dr0.Read())
                                        {
                                            int incColExcel = 1;
                                            int colNettsales = dr0.FieldCount;

                                            for (int inc1 = 0; inc1 < dr0.FieldCount; inc1++)
                                            {
                                                //HEADER
                                                worksheet.Cells[6, incColExcel].Value = dr0.GetName(inc1);
                                                worksheet.Cells[6, incColExcel].Style.Font.Bold = true;
                                                worksheet.Cells[6, incColExcel].Style.WrapText = true;
                                                worksheet.Cells[6, incColExcel].Style.Border.Top.Style = ExcelBorderStyle.Double;
                                                worksheet.Cells[6, incColExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Double;
                                                worksheet.Cells[6, incColExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[6, incColExcel].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells[6, incColExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells[6, incColExcel].Style.Border.Right.Style = ExcelBorderStyle.Thin;


                                                if (incColExcel == 2)
                                                {
                                                    worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToString(dr0.GetValue(inc1));
                                                    worksheet.Cells[incRowExcel, incColExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                    worksheet.Cells[incRowExcel, incColExcel].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                    worksheet.Column(incColExcel).Width = 30;
                                                    worksheet.Cells[incRowExcel, incColExcel].Style.WrapText = true;
                                                }
                                                else if (incColExcel >= 3)
                                                {
                                                    _rowCtrp = incColExcel;
                                                    worksheet.Cells[incRowExcel, incColExcel].Value = dr0.GetValue(inc1);
                                                    worksheet.Cells[incRowExcel, incColExcel].Style.Numberformat.Format = "#,##0";
                                                    worksheet.Column(incColExcel).Width = 20;
                                                    worksheet.Cells[incRowExcel, incColExcel].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                    ////Total Row
                                                    worksheet.Cells[_totalRow, _rowCtrp].Formula = "SUM(" + _host.IntToLetters(_rowCtrp) + "7:" + _host.IntToLetters(_rowCtrp) + _totalEndRow + ")";
                                                    worksheet.Cells[_totalRow, _rowCtrp].Style.Numberformat.Format = "#,##0";
                                                    worksheet.Cells[_totalRow, _rowCtrp].Style.Border.Top.Style = ExcelBorderStyle.Double;
                                                    worksheet.Cells[_totalRow, _rowCtrp].Style.Border.Bottom.Style = ExcelBorderStyle.Double;
                                                    worksheet.Cells[_totalRow, _rowCtrp].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                    worksheet.Cells[_totalRow, _rowCtrp].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                                    worksheet.Cells[_totalRow, _rowCtrp].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                    worksheet.Cells[_totalRow, _rowCtrp].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                                }
                                                else
                                                {
                                                    worksheet.Cells[incRowExcel, incColExcel].Value = dr0.GetValue(inc1).ToString();
                                                    worksheet.Cells[incRowExcel, incColExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                    worksheet.Cells[incRowExcel, incColExcel].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                }

                                                worksheet.Row(incRowExcel).Height = 30;

                                                worksheet.Cells[6, incColExcel].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                                worksheet.Cells[6, incColExcel].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(251, 228, 213));

                                                worksheet.Cells[incRowExcel, incColExcel].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells[incRowExcel, incColExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                                                worksheet.Cells[incRowExcel, incColExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells[incRowExcel, incColExcel].Style.Border.Right.Style = ExcelBorderStyle.Thin;


                                                incColExcel++;

                                            }


                                            //Total Column disamping

                                            worksheet.Cells[incRowExcel, colNettsales].Formula = "SUM(C" + incRowExcel + ":" + _host.IntToLetters(colNettsales - 1) + incRowExcel + ")";
                                            worksheet.Cells[incRowExcel, colNettsales].Style.Numberformat.Format = "#,##0";
                                            worksheet.Cells[incRowExcel, colNettsales].Calculate();

                                            incRowExcel++;



                                            _endRowDetail = incRowExcel - 1;


                                            worksheet.Column(incColExcel).Width = 20;

                                        }


                                        worksheet.Cells[incRowExcel, 2].Value = "TOTAL : ";
                                        worksheet.Cells[incRowExcel, 2].Style.Border.Top.Style = ExcelBorderStyle.Double;
                                        worksheet.Cells[incRowExcel, 2].Style.Border.Bottom.Style = ExcelBorderStyle.Double;
                                        worksheet.Cells[incRowExcel, 2].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                        worksheet.Cells[incRowExcel, 2].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                                        incRowExcel = incRowExcel + 4;

                                        worksheet.Cells[incRowExcel, 1].Value = "Disclaimer : ";
                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Merge = true;
                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Size = 12;

                                        incRowExcel++;

                                        worksheet.Cells[incRowExcel, 1].Value = "Dokumen ini dipersiapkan oleh PT SUCOR SEKURITAS dan hanya bisa digunakan untuk kepentingan investor tersebut diatas dan tidak untuk pihak lainnya. Laporan ini bukan merupakan konfirmasi dari PT SUCOR SEKURITAS dan tidak untuk menggantikan laporan yang wajib diterbitkan oleh Bank Kustodian, jika ada perbedaan antara laporan ini dengan laporan Bank Kustodian, maka laporan Bank Kustodian adalah yang benar. Laporan ini diproses oleh computer dan tidak memerlukan tandatangan.";
                                        worksheet.Cells[incRowExcel, 1, incRowExcel + 2, 6].Style.Font.Size = 11;
                                        worksheet.Cells[incRowExcel, 1, incRowExcel + 2, 6].Merge = true;
                                        worksheet.Cells[incRowExcel, 1, incRowExcel + 2, 6].Style.WrapText = true;

                                        incRowExcel = incRowExcel + 4;

                                        worksheet.Cells[incRowExcel, 1].Value = "PT SUCOR SEKURITAS terdaftar dan diawasi oleh OJK, dengan nomor registrasi KEP-01/PM/MI/1999";
                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 6].Style.Font.Size = 11;
                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 6].Merge = true;

                                        incRowExcel = incRowExcel + 2;

                                        // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                        //worksheet.Cells["A1:Z1"].AutoFitColumns(); // UNTUK AUTO FIT UKURAN TULISAN DENGAN KOLOM, KEBETULAN INI BUAT HEADER AJA DIPAKENYA
                                        //worksheet.Cells["K2:Z2"].AutoFitColumns(); // CEK DARI ENTRY ID SAMPE LAST UPDATE
                                        worksheet.PrinterSettings.Orientation = eOrientation.Landscape; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                        worksheet.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN
                                        //worksheet.HeaderFooter.OddHeader.RightAlignedText = "&14 COUNTERPART PRECENTAGE";

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



            //KELUAR
            #region Subs Batch Form
            else if (_unitRegistryRpt.ReportName.Equals("Subs Batch Form"))
            {
                try
                {
                    using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                    {
                        DbCon.Open();
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            string _statusSubs = "";
                            string _paramFund = "";
                            string _paramAgent = "";

                            if (!_host.findString(_unitRegistryRpt.FundFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_unitRegistryRpt.FundFrom))
                            {
                                _paramFund = "And A.FundPK in ( " + _unitRegistryRpt.FundFrom + " ) ";
                            }
                            else
                            {
                                _paramFund = "";
                            }
                            if (!_host.findString(_unitRegistryRpt.AgentFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_unitRegistryRpt.AgentFrom))
                            {
                                _paramAgent = "And A.AgentPK in ( " + _unitRegistryRpt.AgentFrom + " ) ";
                            }
                            else
                            {
                                _paramAgent = "";
                            }

                            if (_unitRegistryRpt.Status == 1)
                            {
                                _statusSubs = "  A.Status = 2 and A.Posted = 1 and A.Revised = 0 ";
                                //_statusRedemp = "  CR.Status = 2 and CR.Posted = 1 and CR.Revised = 0 ";
                            }
                            else if (_unitRegistryRpt.Status == 2)
                            {
                                _statusSubs = "  A.Status = 2 and A.Posted = 1 and A.Revised = 1 ";
                                //_statusRedemp = "  CR.Status = 2 and CR.Posted = 1 and CR.Revised = 1 ";
                            }
                            else if (_unitRegistryRpt.Status == 3)
                            {
                                _statusSubs = "  A.Status = 2 and A.Posted = 0 and A.Revised = 0 ";
                                //_statusRedemp = "  CR.Status = 2 and CR.Posted = 0 and CR.Revised = 0 ";
                            }
                            else if (_unitRegistryRpt.Status == 4)
                            {
                                _statusSubs = "  A.Status = 1  ";
                                //_statusRedemp = "  CR.Status = 1  ";
                            }
                            else if (_unitRegistryRpt.Status == 5)
                            {
                                _statusSubs = "  A.Status = 3  ";
                                //_statusRedemp = "  CR.Status = 3  ";
                            }
                            else if (_unitRegistryRpt.Status == 6)
                            {
                                _statusSubs = "  (A.Status = 2 or A.Posted = 1) and A.Revised = 0  ";
                                //_statusRedemp = "  (CR.Status = 2 or CR.Posted = 1) and CR.Revised = 0  ";
                            }
                            else if (_unitRegistryRpt.Status == 7)
                            {
                                _statusSubs = "  (A.Status = 1 Or A.Status = 2 or A.Posted = 1) and  A.Revised = 0  ";
                                //_statusRedemp = "  (CR.Status = 1 Or CR.Status = 2 or CR.Posted = 1) and CR.Revised = 0  ";
                            }


                            cmd.CommandText =

                            @"BEGIN    
                            SELECT 'Subscription' Type,ClientSubscriptionPK,D.ContactPerson ContactPerson,D.ID BankBranchID,D.Fax1 FaxNo,ValueDate Date,NavDate NavDate, C.Name FundName,B.ID ClientID, B.Name ClientName,A.NAV NAVAmount,CashAmount,unitamount UnitAmount, A.SubscriptionFeePercent FeePercent,A.SubscriptionFeeAmount FeeAmount,A.Description Remark,A.TotalCashAmount NetAmount,B.NPWP NPWP from ClientSubscription A    
                            left join FundClient B ON B.fundclientpk = A.fundclientpk and B.status = 2   
                            left join Fund C ON A.FundPK = C.FundPK  and C.status = 2     
                            left join BankBranch D ON C.BankBranchPK = D.BankBranchPK  and D.status = 2   
                            WHERE   " + _statusSubs + _paramFund + _paramAgent +
                            @"and NAVDate = @Date  
                            END  ";
                            cmd.CommandTimeout = 0;
                            cmd.Parameters.AddWithValue("@Date", _unitRegistryRpt.ValueDateFrom);
                            cmd.Parameters.AddWithValue("@FundFrom", _unitRegistryRpt.FundFrom);
                            cmd.Parameters.AddWithValue("@AgentFrom", _unitRegistryRpt.AgentFrom);


                            using (SqlDataReader dr0 = cmd.ExecuteReader())
                            {
                                if (!dr0.HasRows)
                                {
                                    return false;
                                }
                                else
                                {
                                    string filePath = Tools.ReportsPath + "SubsBatchForm" + "_" + _userID + ".xlsx";
                                    string pdfPath = Tools.ReportsPath + "SubsBatchForm" + "_" + _userID + ".pdf";
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

                                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Subs Batch Form");


                                        //ATUR DATA GROUPINGNYA DULU
                                        List<UnitRegistryRpt> rList = new List<UnitRegistryRpt>();
                                        while (dr0.Read())
                                        {
                                            UnitRegistryRpt rSingle = new UnitRegistryRpt();
                                            rSingle.ClientSubscriptionPK = Convert.ToInt32(dr0["ClientSubscriptionPK"]);
                                            rSingle.FundName = Convert.ToString(dr0["FundName"]);
                                            rSingle.NAVDate = Convert.ToDateTime(dr0["NAVDate"]);
                                            rSingle.Date = Convert.ToDateTime(dr0["Date"]);
                                            rSingle.Nav = Convert.ToDecimal(dr0["NAVAmount"]);
                                            rSingle.ClientID = Convert.ToString(dr0["ClientID"]);
                                            rSingle.ClientName = Convert.ToString(dr0["ClientName"]);
                                            rSingle.CashBalance = Convert.ToDecimal(dr0["CashAmount"]);
                                            rSingle.UnitBalance = Convert.ToDecimal(dr0["UnitAmount"]);
                                            rSingle.FeePercent = Convert.ToDecimal(dr0["FeePercent"]);
                                            rSingle.FeeAmount = Convert.ToDecimal(dr0["FeeAmount"]);
                                            rSingle.ContactPerson = Convert.ToString(dr0["ContactPerson"]);
                                            rSingle.FaxNo = Convert.ToString(dr0["FaxNo"]);
                                            rSingle.BankCustodiID = Convert.ToString(dr0["BankBranchID"]);
                                            rSingle.Remark = Convert.ToString(dr0["Remark"]);
                                            rSingle.NetAmount = Convert.ToDecimal(dr0["NetAmount"]);
                                            rSingle.NPWP = Convert.ToString(dr0["NPWP"]);
                                            rList.Add(rSingle);

                                        }


                                        var QueryByFundID =
                                            from r in rList
                                            group r by new { r.FundName, r.Date, r.ContactPerson, r.FaxNo, r.BankCustodiID, r.AgentName } into rGroup
                                            select rGroup;

                                        int incRowExcel = 1;

                                        foreach (var rsHeader in QueryByFundID)
                                        {




                                            incRowExcel = incRowExcel + 2;

                                            int RowZ = incRowExcel;
                                            incRowExcel = incRowExcel + 4;
                                            worksheet.Cells[incRowExcel, 1].Value = _host.Get_CompanyName();
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Size = 24;
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells["A" + incRowExcel + ":G" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 9].Value = "To ";
                                            worksheet.Cells[incRowExcel, 10].Value = ": ";
                                            worksheet.Cells[incRowExcel, 11].Value = rsHeader.Key.BankCustodiID;
                                            worksheet.Cells["K" + incRowExcel + ":L" + incRowExcel].Merge = true;


                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Value = "Gd. Equity Tower Lt. 31";
                                            worksheet.Cells["A" + incRowExcel + ":E" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 11].Value = "Custodian Services";
                                            worksheet.Cells["K" + incRowExcel + ":L" + incRowExcel].Merge = true;

                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Value = "Jl. Jendral Sudirman kav 52-53, Jakarta 12910";
                                            worksheet.Cells["A" + incRowExcel + ":E" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 9].Value = "Attention ";
                                            worksheet.Cells[incRowExcel, 10].Value = ": ";
                                            worksheet.Cells[incRowExcel, 11].Value = rsHeader.Key.ContactPerson;
                                            worksheet.Cells["K" + incRowExcel + ":L" + incRowExcel].Merge = true;
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 9].Value = "Fax ";
                                            worksheet.Cells[incRowExcel, 10].Value = ": ";
                                            worksheet.Cells[incRowExcel, 11].Value = _host.Get_CompanyFax();
                                            worksheet.Cells["K" + incRowExcel + ":L" + incRowExcel].Merge = true;

                                            worksheet.Cells[incRowExcel, 1].Value = "Tel : ";
                                            worksheet.Cells[incRowExcel, 2].Value = _host.Get_CompanyPhone();
                                            worksheet.Cells["B" + incRowExcel + ":B" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 3].Value = "Fax : ";
                                            worksheet.Cells[incRowExcel, 4].Value = _host.Get_CompanyFax();
                                            worksheet.Cells["D" + incRowExcel + ":D" + incRowExcel].Merge = true;
                                            incRowExcel++;
                                            incRowExcel = incRowExcel + 1;
                                            worksheet.Cells[incRowExcel, 5].Value = "SUBSCRIPTION BATCH FORM";
                                            worksheet.Cells[incRowExcel, 5].Style.Font.Size = 65;
                                            worksheet.Cells[incRowExcel, 5].Style.Font.Bold = true;
                                            worksheet.Cells["E" + incRowExcel + ":H" + incRowExcel].Merge = true;
                                            worksheet.Cells["E" + incRowExcel + ":H" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            incRowExcel = incRowExcel + 2;


                                            //Row A = 2
                                            int RowA = incRowExcel;

                                            worksheet.Cells[incRowExcel, 1].Value = "Fund Name ";
                                            worksheet.Cells["A" + incRowExcel + ":B" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 3].Value = ":";
                                            worksheet.Cells[incRowExcel, 4].Value = rsHeader.Key.FundName;
                                            worksheet.Cells[incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            worksheet.Cells[incRowExcel, 4].Style.Font.Bold = true;
                                            worksheet.Cells["D" + incRowExcel + ":F" + incRowExcel].Merge = true;

                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Value = "NAV Date ";
                                            worksheet.Cells["A" + incRowExcel + ":B" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 3].Value = ": ";
                                            worksheet.Cells[incRowExcel, 4].Value = rsHeader.Key.Date;
                                            worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "dd-MMMM-yyyy";
                                            worksheet.Cells[incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            worksheet.Cells["D" + incRowExcel + ":D" + incRowExcel].Merge = true;

                                            incRowExcel = incRowExcel + 2;




                                            //Row B = 3
                                            int RowB = incRowExcel;
                                            int RowG = incRowExcel + 1;


                                            worksheet.Cells[incRowExcel, 1].Value = "No";
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells["A" + RowB + ":A" + RowG].Merge = true;
                                            worksheet.Cells["A" + RowB + ":A" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["A" + RowB + ":A" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 2].Value = "Customer";
                                            worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[RowG, 2].Value = "ID";
                                            worksheet.Cells[RowG, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[RowG, 2].Style.Font.Bold = true;


                                            worksheet.Cells[incRowExcel, 3].Value = "Name";
                                            worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;
                                            worksheet.Cells["C" + RowB + ":D" + RowG].Merge = true;
                                            worksheet.Cells["C" + RowB + ":D" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["C" + RowB + ":D" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;


                                            worksheet.Cells[incRowExcel, 5].Value = "Gross Subscribed";
                                            worksheet.Cells[incRowExcel, 5].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[RowG, 5].Value = "IDR Amount";
                                            worksheet.Cells[RowG, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[RowG, 5].Style.Font.Bold = true;
                                            worksheet.Cells["E" + RowB + ":F" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["E" + RowB + ":F" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 7].Value = "Fee";
                                            worksheet.Cells[incRowExcel, 7].Style.Font.Bold = true;
                                            worksheet.Cells["G" + RowB + ":H" + RowB].Merge = true;
                                            worksheet.Cells["G" + RowB + ":H" + RowB].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[RowG, 7].Value = "%";
                                            worksheet.Cells[RowG, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[RowG, 7].Style.Font.Bold = true;
                                            worksheet.Cells[RowG, 8].Value = "Amount";
                                            worksheet.Cells[RowG, 8].Style.Font.Bold = true;
                                            worksheet.Cells[RowG, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 9].Value = "Net Subscribed";
                                            worksheet.Cells[incRowExcel, 9].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[RowG, 9].Value = "IDR Amount";
                                            worksheet.Cells[RowG, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[RowG, 9].Style.Font.Bold = true;
                                            worksheet.Cells["I" + RowB + ":J" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["I" + RowB + ":J" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 11].Value = "NPWP";
                                            worksheet.Cells[incRowExcel, 11].Style.Font.Bold = true;
                                            worksheet.Cells["K" + RowB + ":K" + RowG].Merge = true;
                                            worksheet.Cells["K" + RowB + ":K" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["K" + RowB + ":K" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 12].Value = "Transfer";
                                            worksheet.Cells[incRowExcel, 12].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 12].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[RowG, 12].Value = "To Bank";
                                            worksheet.Cells[RowG, 12].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[RowG, 12].Style.Font.Bold = true;

                                            incRowExcel++;

                                            // Row C = 4
                                            int RowC = incRowExcel;

                                            incRowExcel++;
                                            //area header

                                            int _no = 1;
                                            int _startRowDetail = incRowExcel;
                                            int _endRowDetail = 0;
                                            foreach (var rsDetail in rsHeader)
                                            {
                                                //Row D = 5
                                                int RowD = incRowExcel;
                                                int RowE = incRowExcel + 1;


                                                //ThickBox Border

                                                worksheet.Cells["A" + RowB + ":L" + RowG].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + RowB + ":L" + RowG].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;


                                                //area detail
                                                worksheet.Cells[incRowExcel, 1].Value = _no;
                                                worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 2].Value = rsDetail.ClientID;
                                                worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.ClientName;
                                                worksheet.Cells["C" + incRowExcel + ":D" + incRowExcel].Merge = true;
                                                worksheet.Cells[incRowExcel, 5].Value = rsDetail.CashBalance;
                                                worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells["E" + incRowExcel + ":F" + incRowExcel].Merge = true;
                                                worksheet.Cells[incRowExcel, 7].Value = rsDetail.FeePercent;
                                                worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0.0000";
                                                worksheet.Cells[incRowExcel, 8].Value = rsDetail.FeeAmount;
                                                worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 9].Value = rsDetail.NetAmount;
                                                worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells["I" + incRowExcel + ":J" + incRowExcel].Merge = true;
                                                worksheet.Cells[incRowExcel, 11].Value = rsDetail.NPWP;
                                                worksheet.Cells[incRowExcel, 11].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 12].Value = rsDetail.BankCustodiID;
                                                worksheet.Cells[incRowExcel, 12].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                _endRowDetail = incRowExcel;
                                                _no++;
                                                incRowExcel++;

                                            }

                                            worksheet.Cells["A" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + RowB + ":A" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + RowB + ":A" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells["B" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["B" + RowB + ":B" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["B" + RowB + ":B" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["B" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells["C" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["C" + RowB + ":C" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["C" + RowB + ":C" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["C" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells["D" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["D" + RowB + ":D" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["D" + RowB + ":D" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["D" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells["G" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["G" + RowB + ":G" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["G" + RowB + ":G" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["G" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells["H" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["H" + RowB + ":H" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["H" + RowB + ":H" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["H" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells["K" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["K" + RowB + ":K" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["K" + RowB + ":K" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["K" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells["L" + RowB].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["L" + RowB + ":L" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["L" + RowB + ":L" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["L" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells["A" + _endRowDetail + ":L" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells[incRowExcel, 4].Value = "Total :";
                                            worksheet.Cells[incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                            worksheet.Cells[incRowExcel, 5].Formula = "SUM(E" + _startRowDetail + ":E" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 8].Formula = "SUM(H" + _startRowDetail + ":H" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 9].Formula = "SUM(I" + _startRowDetail + ":I" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 5].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 8].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 9].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 5].Calculate();
                                            worksheet.Cells["E" + incRowExcel + ":F" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                            worksheet.Cells[incRowExcel, 8].Calculate();
                                            worksheet.Cells[incRowExcel, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                            worksheet.Cells[incRowExcel, 9].Calculate();
                                            worksheet.Cells[incRowExcel, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                            worksheet.Cells["I" + incRowExcel + ":J" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0";
                                            worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0";
                                            worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0";
                                            incRowExcel = incRowExcel + 2;
                                            worksheet.Cells[incRowExcel, 1].Value = "Regards,";
                                            worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            incRowExcel = incRowExcel + 7;
                                            worksheet.Cells["A" + incRowExcel + ":C" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells[incRowExcel, 1].Value = "Daryanti Lestari Anggraini";
                                            worksheet.Cells["E" + incRowExcel + ":F" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells[incRowExcel, 5].Value = "Daryanti Lestari Anggraini";
                                            worksheet.Cells["K" + incRowExcel + ":K" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells[incRowExcel, 11].Value = "Anggra Putri W";
                                            incRowExcel++;

                                            incRowExcel = incRowExcel + 4;

                                            worksheet.Cells[incRowExcel, 1].Value = "Disclaimer : ";
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Merge = true;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Size = 12;

                                            incRowExcel++;

                                            worksheet.Cells[incRowExcel, 1].Value = "Dokumen ini dipersiapkan oleh PT SUCOR SEKURITAS dan hanya bisa digunakan untuk kepentingan investor tersebut diatas dan tidak untuk pihak lainnya. Laporan ini bukan merupakan konfirmasi dari PT SUCOR SEKURITAS dan tidak untuk menggantikan laporan yang wajib diterbitkan oleh Bank Kustodian, jika ada perbedaan antara laporan ini dengan laporan Bank Kustodian, maka laporan Bank Kustodian adalah yang benar. Laporan ini diproses oleh computer dan tidak memerlukan tandatangan.";
                                            worksheet.Cells[incRowExcel, 1, incRowExcel + 2, 12].Style.Font.Size = 11;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel + 2, 12].Merge = true;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel + 2, 12].Style.WrapText = true;

                                            incRowExcel = incRowExcel + 4;

                                            worksheet.Cells[incRowExcel, 1].Value = "PT SUCOR SEKURITAS terdaftar dan diawasi oleh OJK, dengan nomor registrasi KEP-01/PM/MI/1999";
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 12].Style.Font.Size = 11;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 12].Merge = true;

                                            incRowExcel = incRowExcel + 2;

                                            worksheet.Row(incRowExcel).PageBreak = _unitRegistryRpt.PageBreak;

                                        }
                                        //incRowExcel++;
                                        int _lastRow = incRowExcel;

                                        string _rangeA = "A5:L" + incRowExcel;
                                        using (ExcelRange r = worksheet.Cells[_rangeA]) // KALO  KOLOM 1 SAMPE 9 A-I
                                        {
                                            r.Style.Font.Size = 28;
                                        }

                                        worksheet.DeleteRow(_lastRow);


                                        worksheet.PrinterSettings.FitToPage = true;
                                        worksheet.PrinterSettings.FitToWidth = 1;
                                        worksheet.PrinterSettings.FitToHeight = 0;
                                        worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel - 1, 12];
                                        worksheet.Column(1).Width = 10;
                                        worksheet.Column(2).Width = 35;
                                        worksheet.Column(3).Width = 2;
                                        worksheet.Column(4).Width = 100;
                                        worksheet.Column(5).Width = 50;
                                        worksheet.Column(6).Width = 2;
                                        worksheet.Column(7).Width = 35;
                                        worksheet.Column(8).Width = 35;
                                        worksheet.Column(9).Width = 50;
                                        worksheet.Column(10).Width = 2;
                                        worksheet.Column(11).Width = 60;
                                        worksheet.Column(12).Width = 45;

                                        // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                        //worksheet.Cells["A3:I14"].AutoFitColumns(); // UNTUK AUTO FIT UKURAN TULISAN DENGAN KOLOM, KEBETULAN INI BUAT HEADER AJA DIPAKENYA
                                        //worksheetApproved.Cells["A3:E3"].AutoFitColumns();
                                        worksheet.PrinterSettings.Orientation = eOrientation.Landscape; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                        worksheet.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN

                                        //worksheet.HeaderFooter.OddHeader.CenteredText = "\n \n \n \n \n \n \n &30&B SUBSCRIPTION \n &28&B Batch Form";



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

            #region Monthly APERD Profil Investor Perorangan
            else if (_unitRegistryRpt.ReportName.Equals("Monthly APERD Profil Investor Perorangan"))
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
              
                        select A.ID,name,B.DescOne Identitas,A.NoIdentitasInd1,A.NPWP,convert(nvarchar(10),A.tanggallahir,131) TanggalLahir,C.DescOne JenisKelamin, 
                        D.DescOne StatusPerkawinan,E.DescOne Kewarganegaraan,f.DescOne Pekerjaan,
                        MV12.Descone Pendidikan,MV13.Descone agama,MV15.Descone SumberDana,mv16.descone MaksudTujuan,MV14.descone Penghasilan,alamatind1,Mv1.DescOne KodeKota,
                        cast(KodePosind1 as nvarchar(10)) KodePosInd1
                        from fundclient A
                        left join Mastervalue B on A.IdentitasIND1 = B.Code and B.status = 2 and B.id = 'Identity'
                        left join Mastervalue C on A.JenisKelamin = C.Code and C.status = 2 and C.id = 'SDISex'
                        left join Mastervalue D on A.StatusPerkawinan = D.Code and D.status = 2 and D.id = 'MaritalStatus'
                        left join Mastervalue E on A.Nationality = E.Code and E.status = 2 and E.id = 'SDINationality'
                        left join Mastervalue F on A.Pekerjaan = F.Code and F.status = 2 and F.id = 'Occupation'
                        left join MasterValue mv12 on A.Pendidikan = mv12.code and  mv12.ID = 'EducationalBackground' and mv12.status = 2    
                        left join MasterValue mv13 on A.Agama = mv13.code and  mv13.ID = 'Religion' and mv13.status = 2    
                        left join MasterValue mv14 on A.PenghasilanInd = mv14.code and  mv14.ID = 'IncomeIND' and mv14.status = 2    
                        left join MasterValue mv15 on A.SumberDanaInd = mv15.code and  mv15.ID = 'IncomeSourceIND' and mv15.status = 2    
                        left join MasterValue mv16 on A.MaksudTujuanInd = mv16.code and  mv16.ID = 'InvestmentObjectivesIND' and mv16.status = 2   
                        left join MasterValue mv1 on A.KodeKotaInd1 = mv1.Code and mv1.ID = 'CityRHB' and mv1.status = 2 
                        where A.status = 2
                        ";

                            cmd.CommandTimeout = 0;


                            using (SqlDataReader dr0 = cmd.ExecuteReader())
                            {
                                if (!dr0.HasRows)
                                {
                                    return false;
                                }
                                else
                                {
                                    string filePath = Tools.ReportsPath + "MonthlyAperdProfilInvestorPerorangan" + "_" + _userID + ".xlsx";
                                    string pdfPath = Tools.ReportsPath + "MonthlyAperdProfilInvestorPerorangan" + "_" + _userID + ".pdf";
                                    FileInfo excelFile = new FileInfo(filePath);
                                    if (excelFile.Exists)
                                    {
                                        excelFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                        excelFile = new FileInfo(filePath);
                                    }

                                    // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                    using (ExcelPackage package = new ExcelPackage(excelFile))
                                    {
                                        package.Workbook.Properties.Title = "LaporanPenjualanReksaDana";
                                        package.Workbook.Properties.Author = Tools.DefaultReportAuthor();
                                        package.Workbook.Properties.Comments = Tools.DefaultReportComments();
                                        package.Workbook.Properties.Company = Tools.DefaultReportCompany();
                                        package.Workbook.Properties.SetCustomPropertyValue("Checked by", _userID);
                                        package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", Tools.DefaultReportAssemblyName());

                                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Penjualan Reksa Dana");

                                        int incRowExcel = 1;

                                        worksheet.Cells[incRowExcel, 6].Value = "LAPORAN PROFIL INVESTOR REKSADANA";
                                        worksheet.Cells[incRowExcel, 6, incRowExcel, 12].Merge = true;
                                        worksheet.Cells[incRowExcel, 6, incRowExcel, 12].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 6, incRowExcel, 12].Style.Font.Size = 12;

                                        incRowExcel++;

                                        worksheet.Cells[incRowExcel, 6].Value = "SELLING AGENT PT SUCOR SEKURITAS";
                                        worksheet.Cells[incRowExcel, 6, incRowExcel, 12].Merge = true;
                                        worksheet.Cells[incRowExcel, 6, incRowExcel, 12].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 6, incRowExcel, 12].Style.Font.Size = 12;

                                        incRowExcel = incRowExcel + 2;

                                        worksheet.Cells[incRowExcel, 6].Value = "Periode: " + Convert.ToDateTime(_unitRegistryRpt.ValueDateTo).ToString("MMMM") + " " + Convert.ToDateTime(_unitRegistryRpt.ValueDateTo).Year.ToString();
                                        worksheet.Cells[incRowExcel, 6, incRowExcel, 12].Merge = true;
                                        worksheet.Cells[incRowExcel, 6, incRowExcel, 12].Style.Font.Size = 9;
                                        worksheet.Cells[incRowExcel, 6, incRowExcel, 8].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                                        incRowExcel = incRowExcel + 2;

                                        worksheet.Cells[incRowExcel, 1].Value = "Tabel 1 Profil Investor Keuangan";
                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Merge = true;
                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.UnderLine = true;
                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Size = 11;

                                        incRowExcel = incRowExcel + 2;



                                        //ATUR DATA GROUPINGNYA DULU
                                        int x = 0;
                                        int counter = 0;
                                        while (dr0.Read())
                                        {
                                            int count = dr0.FieldCount;
                                            x = count;
                                            if (counter == 0)
                                            {
                                                for (int i = 0; i < count; i++)
                                                {
                                                    worksheet.Cells[incRowExcel, i + 1].Value = dr0.GetName(i).ToString();

                                                }
                                            }
                                            counter = 1;
                                            incRowExcel = incRowExcel + 1;
                                            for (int i = 0; i < count; i++)
                                            {
                                                worksheet.Cells[incRowExcel, i + 1].Value = dr0.GetString(i);
                                            }

                                        }

                                        incRowExcel = incRowExcel + 4;

                                        worksheet.Cells[incRowExcel, 1].Value = "Disclaimer : ";
                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 7].Merge = true;
                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 7].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 7].Style.Font.Size = 11;

                                        incRowExcel++;

                                        worksheet.Cells[incRowExcel, 1].Value = "Dokumen ini dipersiapkan oleh PT SUCOR SEKURITAS dan hanya bisa digunakan untuk kepentingan investor tersebut diatas dan tidak untuk pihak lainnya. Laporan ini bukan merupakan konfirmasi dari PT SUCOR SEKURITAS dan tidak untuk menggantikan laporan yang wajib diterbitkan oleh Bank Kustodian, jika ada perbedaan antara laporan ini dengan laporan Bank Kustodian, maka laporan Bank Kustodian adalah yang benar. Laporan ini diproses oleh computer dan tidak memerlukan tandatangan.";
                                        worksheet.Cells[incRowExcel, 1, incRowExcel + 2, 13].Style.Font.Size = 11;
                                        worksheet.Cells[incRowExcel, 1, incRowExcel + 2, 13].Merge = true;
                                        worksheet.Cells[incRowExcel, 1, incRowExcel + 2, 13].Style.WrapText = true;

                                        incRowExcel = incRowExcel + 4;

                                        worksheet.Cells[incRowExcel, 1].Value = "PT SUCOR SEKURITAS terdaftar dan diawasi oleh OJK, dengan nomor registrasi KEP-01/PM/MI/1999";
                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 7].Style.Font.Size = 11;
                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 7].Merge = true;

                                        //incRowExcel = incRowExcel + 2;

                                        //worksheet.Cells[incRowExcel, 1].Value = "PT SUCOR SEKURITAS";
                                        //worksheet.Cells[incRowExcel, 1, incRowExcel, 7].Merge = true;
                                        //worksheet.Cells[incRowExcel, 1, incRowExcel, 7].Style.Font.Bold = true;
                                        //worksheet.Cells[incRowExcel, 1, incRowExcel, 7].Style.Font.Size = 11;

                                        //incRowExcel++;

                                        //worksheet.Cells[incRowExcel, 1].Value = "Sahid Sudirman Center, 12th Fl.";
                                        //worksheet.Cells[incRowExcel, 1, incRowExcel, 7].Merge = true;
                                        //worksheet.Cells[incRowExcel, 1, incRowExcel, 7].Style.Font.Size = 12;

                                        //incRowExcel++;

                                        //worksheet.Cells[incRowExcel, 1].Value = "Jl. Jenderal Sudirman Kav. 86, Jakarta 10220 - Indonesia";
                                        //worksheet.Cells[incRowExcel, 1, incRowExcel, 7].Merge = true;
                                        //worksheet.Cells[incRowExcel, 1, incRowExcel, 7].Style.Font.Size = 12;

                                        //incRowExcel++;

                                        //worksheet.Cells[incRowExcel, 1].Value = "p. +62-21  8067 3000, f.  +62-21  2788 9288 |sucorsekuritas.com";
                                        //worksheet.Cells[incRowExcel, 1, incRowExcel, 7].Merge = true;
                                        //worksheet.Cells[incRowExcel, 1, incRowExcel, 7].Style.Font.Size = 12;


                                        //worksheet.Cells.Calculate();

                                        incRowExcel = incRowExcel + 2;

                                        worksheet.PrinterSettings.FitToPage = true;
                                        worksheet.PrinterSettings.FitToWidth = 1;
                                        worksheet.PrinterSettings.FitToHeight = 0;
                                        worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel - 1, x];

                                        // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                        //worksheet.Cells.AutoFitColumns(); // UNTUK AUTO FIT UKURAN TULISAN DENGAN KOLOM, KEBETULAN INI BUAT HEADER AJA DIPAKENYA
                                        //worksheet.PrinterSettings.FitToPage = true;
                                        worksheet.PrinterSettings.Orientation = eOrientation.Landscape; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                        worksheet.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN
                                        worksheet.HeaderFooter.OddHeader.RightAlignedText = "&12 Laporan Penjualan Reksa Dana";

                                        // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                        //ExcelWorksheet ws = package.Workbook.Worksheets.Add("Demo");

                                        worksheet.PrinterSettings.TopMargin = (decimal).3 / 2.54M; // narrow border
                                        worksheet.PrinterSettings.RightMargin = (decimal).3 / 2.54M; //narrow borderExcelWorksheet ws = pck.Workbook.Worksheets.Add("Demo");
                                        worksheet.PrinterSettings.LeftMargin = (decimal).3 / 2.54M; // narrow border

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

                                        //worksheet.HeaderFooter.OddFooter.CenteredText = Tools.DefaultReportFooterCenterText();
                                        worksheet.HeaderFooter.OddFooter.LeftAlignedText = Tools.DefaultReportFooterLeftText();

                                        package.Save();
                                        if (_unitRegistryRpt.DownloadMode == "PDF")
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

            #region Monthly APERD Penjualan Per Kantor Pemasaran
            else if (_unitRegistryRpt.ReportName.Equals("Monthly APERD Penjualan Per Kantor Pemasaran"))
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
                         Select 'Kantor Pusat' KantorCabang , sum(totalCashAmount) NilaiPenjualan, 0 USD
                        , avg(B.Perorangan) Perorangan , avg(C.Institusi) Institusi 
                        from clientSubscription A 
                        left join (
	                        Select Count(FundClientPK) Perorangan from FundClient where status = 2 and InvestorType = 1
	
                        )B on A.status = 2
                        left join (
	                        Select Count(FundClientPK) Institusi from FundClient where status = 2 and InvestorType = 2
	
                        )C on A.status = 2
                        where a.status = 2 and a.posted = 1 and revised = 0
                            and  valuedate between @DateFrom and @DateTo";

                            cmd.CommandTimeout = 0;
                            cmd.Parameters.AddWithValue("@DateTo", _unitRegistryRpt.ValueDateTo);
                            cmd.Parameters.AddWithValue("@DateFrom", _unitRegistryRpt.ValueDateFrom);


                            using (SqlDataReader dr0 = cmd.ExecuteReader())
                            {
                                if (!dr0.HasRows)
                                {
                                    return false;
                                }
                                else
                                {
                                    string filePath = Tools.ReportsPath + "MonthlyAperdPenjualanPerKantorPemasaran" + "_" + _userID + ".xlsx";
                                    string pdfPath = Tools.ReportsPath + "MonthlyAperdPenjualanPerKantorPemasaran" + "_" + _userID + ".pdf";
                                    FileInfo excelFile = new FileInfo(filePath);
                                    if (excelFile.Exists)
                                    {
                                        excelFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                        excelFile = new FileInfo(filePath);
                                    }

                                    // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                    using (ExcelPackage package = new ExcelPackage(excelFile))
                                    {
                                        package.Workbook.Properties.Title = "LaporanPenjualanReksaDana";
                                        package.Workbook.Properties.Author = Tools.DefaultReportAuthor();
                                        package.Workbook.Properties.Comments = Tools.DefaultReportComments();
                                        package.Workbook.Properties.Company = Tools.DefaultReportCompany();
                                        package.Workbook.Properties.SetCustomPropertyValue("Checked by", _userID);
                                        package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", Tools.DefaultReportAssemblyName());

                                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Penjualan Reksa Dana");

                                        int incRowExcel = 1;

                                        worksheet.Cells[incRowExcel, 4].Value = "LAPORAN PENJUALAN REKSADANA";
                                        worksheet.Cells[incRowExcel, 4, incRowExcel, 10].Merge = true;
                                        worksheet.Cells[incRowExcel, 4, incRowExcel, 10].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 4, incRowExcel, 10].Style.Font.Size = 12;

                                        incRowExcel++;

                                        worksheet.Cells[incRowExcel, 4].Value = "SELLING AGENT PT SUCOR SEKURITAS";
                                        worksheet.Cells[incRowExcel, 4, incRowExcel, 10].Merge = true;
                                        worksheet.Cells[incRowExcel, 4, incRowExcel, 10].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 4, incRowExcel, 10].Style.Font.Size = 12;

                                        incRowExcel = incRowExcel + 2;

                                        worksheet.Cells[incRowExcel, 4].Value = "Periode: " + Convert.ToDateTime(_unitRegistryRpt.ValueDateTo).ToString("MMMM") + " " + Convert.ToDateTime(_unitRegistryRpt.ValueDateTo).Year.ToString();
                                        worksheet.Cells[incRowExcel, 4, incRowExcel, 10].Merge = true;
                                        worksheet.Cells[incRowExcel, 4, incRowExcel, 10].Style.Font.Size = 9;
                                        worksheet.Cells[incRowExcel, 4, incRowExcel, 6].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                                        incRowExcel = incRowExcel + 2;

                                        worksheet.Cells[incRowExcel, 1].Value = "Tabel 1 Laporan Penjualan Reksa Dana Per Kantor Pemasaran";
                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Merge = true;
                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.UnderLine = true;
                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Size = 11;

                                        incRowExcel = incRowExcel + 2;


                                        //ATUR DATA GROUPINGNYA DULU
                                        List<UnitRegistryRpt> rList = new List<UnitRegistryRpt>();
                                        while (dr0.Read())
                                        {
                                            UnitRegistryRpt rSingle = new UnitRegistryRpt();
                                            rSingle.KantorCabang = Convert.ToString(dr0["KantorCabang"]);
                                            rSingle.NilaiPenjualan = Convert.ToDecimal(dr0["NilaiPenjualan"]);
                                            rSingle.USD = Convert.ToDecimal(dr0["USD"]);
                                            rList.Add(rSingle);

                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                                            worksheet.Cells[incRowExcel, 1].Value = "NO";
                                            worksheet.Cells[incRowExcel, 2].Value = "Kantor pusat, Kantor Lain Selain";
                                            worksheet.Cells[incRowExcel, 2].Style.WrapText = true;
                                            worksheet.Cells[incRowExcel, 3].Value = "Kantor Pusat, Kantor Lain Selain Kantor Pusat dan/atau Gerai Penjualan";
                                            worksheet.Cells[incRowExcel, 3].Style.WrapText = true;
                                            worksheet.Cells[incRowExcel, 4].Value = "Nilai Penjualan";
                                            worksheet.Cells[incRowExcel, 4].Style.WrapText = true;
                                            worksheet.Cells["D" + incRowExcel + ":E" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 6].Value = "Nilai Outstanding";
                                            worksheet.Cells[incRowExcel, 6].Style.WrapText = true;
                                            worksheet.Cells["F" + incRowExcel + ":G" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 8].Value = "Jumlah Nasabah";
                                            worksheet.Cells[incRowExcel, 8].Style.WrapText = true;
                                            worksheet.Cells["H" + incRowExcel + ":I" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 9].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 9].Style.Font.Size = 10;
                                            //worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                            //worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(251, 228, 213));
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 9].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 9].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                            //worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells["A" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells["I" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                                            incRowExcel = incRowExcel + 1;

                                            worksheet.Cells[incRowExcel, 4].Value = "Rp";
                                            worksheet.Cells[incRowExcel, 5].Value = "USD";
                                            worksheet.Cells[incRowExcel, 6].Value = "Rp";
                                            worksheet.Cells[incRowExcel, 7].Value = "USD";
                                            worksheet.Cells[incRowExcel, 8].Value = "I";
                                            worksheet.Cells[incRowExcel, 9].Value = "P";
                                            worksheet.Cells["A" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells["I" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells[incRowExcel, 4, incRowExcel, 9].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 4, incRowExcel, 9].Style.Font.Size = 10;
                                            worksheet.Cells[incRowExcel, 4, incRowExcel, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 4, incRowExcel, 9].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 9].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 9].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 9].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells[incRowExcel, 4, incRowExcel, 9].Style.Border.Top.Style = ExcelBorderStyle.Thin;

                                            incRowExcel = incRowExcel + 1;

                                            int first = incRowExcel;

                                            int no = 1;

                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 9].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 9].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 9].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 9].Style.Border.Top.Style = ExcelBorderStyle.Thin;


                                            worksheet.Cells[incRowExcel, 1].Value = no;
                                            worksheet.Cells[incRowExcel, 1].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 1].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 2].Value = Convert.ToString(dr0["KantorCabang"]);
                                            worksheet.Cells[incRowExcel, 2].Style.WrapText = true;


                                            worksheet.Cells[incRowExcel, 4].Value = Convert.ToDecimal(dr0["NilaiPenjualan"]);
                                            worksheet.Cells[incRowExcel, 4, incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                            worksheet.Cells[incRowExcel, 4, incRowExcel, 4].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##";
                                            worksheet.Cells[incRowExcel, 4, incRowExcel, 5].Style.Font.Size = 10;

                                            worksheet.Cells[incRowExcel, 5].Value = Convert.ToDecimal(dr0["USD"]);
                                            worksheet.Cells[incRowExcel, 5, incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                            worksheet.Cells[incRowExcel, 5, incRowExcel, 5].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##";
                                            worksheet.Cells[incRowExcel, 6].Value = 0;
                                            worksheet.Cells[incRowExcel, 7].Value = 0;
                                            worksheet.Cells[incRowExcel, 8].Value = Convert.ToInt32(dr0["Institusi"]);
                                            worksheet.Cells[incRowExcel, 9].Value = Convert.ToInt32(dr0["Perorangan"]);


                                            incRowExcel++;

                                            int last = incRowExcel - 1;
                                            //incRowExcel = incRowExcel - 1;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 7].Style.Border.Top.Style = ExcelBorderStyle.Thin;

                                            //incRowExcel++;

                                            worksheet.Cells[incRowExcel, 2].Value = "Total";
                                            worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##";
                                            worksheet.Cells[incRowExcel, 4].Formula = "SUM(D" + first.ToString() + ":D" + last.ToString() + ")";
                                            worksheet.Cells[incRowExcel, 4].Calculate();
                                            worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##";
                                            worksheet.Cells[incRowExcel, 5].Formula = "SUM(E" + first.ToString() + ":E" + last.ToString() + ")";
                                            worksheet.Cells[incRowExcel, 5].Calculate();

                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Size = 10;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                        }

                                        incRowExcel = incRowExcel + 4;

                                        worksheet.Cells[incRowExcel, 1].Value = "Disclaimer : ";
                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 7].Merge = true;
                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 7].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 7].Style.Font.Size = 11;

                                        incRowExcel++;

                                        worksheet.Cells[incRowExcel, 1].Value = "Dokumen ini dipersiapkan oleh PT SUCOR SEKURITAS dan hanya bisa digunakan untuk kepentingan investor tersebut diatas dan tidak untuk pihak lainnya. Laporan ini bukan merupakan konfirmasi dari PT SUCOR SEKURITAS dan tidak untuk menggantikan laporan yang wajib diterbitkan oleh Bank Kustodian, jika ada perbedaan antara laporan ini dengan laporan Bank Kustodian, maka laporan Bank Kustodian adalah yang benar. Laporan ini diproses oleh computer dan tidak memerlukan tandatangan.";
                                        worksheet.Cells[incRowExcel, 1, incRowExcel + 2, 9].Style.Font.Size = 11;
                                        worksheet.Cells[incRowExcel, 1, incRowExcel + 2, 9].Merge = true;
                                        worksheet.Cells[incRowExcel, 1, incRowExcel + 2, 9].Style.WrapText = true;

                                        incRowExcel = incRowExcel + 4;

                                        worksheet.Cells[incRowExcel, 1].Value = "PT SUCOR SEKURITAS terdaftar dan diawasi oleh OJK, dengan nomor registrasi KEP-01/PM/MI/1999";
                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 7].Style.Font.Size = 11;
                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 7].Merge = true;

                                        incRowExcel = incRowExcel + 2;

                                        //worksheet.Cells[incRowExcel, 1].Value = "PT SUCOR SEKURITAS";
                                        //worksheet.Cells[incRowExcel, 1, incRowExcel, 7].Merge = true;
                                        //worksheet.Cells[incRowExcel, 1, incRowExcel, 7].Style.Font.Bold = true;
                                        //worksheet.Cells[incRowExcel, 1, incRowExcel, 7].Style.Font.Size = 11;

                                        //incRowExcel++;

                                        //worksheet.Cells[incRowExcel, 1].Value = "Sahid Sudirman Center, 12th Fl.";
                                        //worksheet.Cells[incRowExcel, 1, incRowExcel, 7].Merge = true;
                                        //worksheet.Cells[incRowExcel, 1, incRowExcel, 7].Style.Font.Size = 12;

                                        //incRowExcel++;

                                        //worksheet.Cells[incRowExcel, 1].Value = "Jl. Jenderal Sudirman Kav. 86, Jakarta 10220 - Indonesia";
                                        //worksheet.Cells[incRowExcel, 1, incRowExcel, 7].Merge = true;
                                        //worksheet.Cells[incRowExcel, 1, incRowExcel, 7].Style.Font.Size = 12;

                                        //incRowExcel++;

                                        //worksheet.Cells[incRowExcel, 1].Value = "p. +62-21  8067 3000, f.  +62-21  2788 9288 |sucorsekuritas.com";
                                        //worksheet.Cells[incRowExcel, 1, incRowExcel, 7].Merge = true;
                                        //worksheet.Cells[incRowExcel, 1, incRowExcel, 7].Style.Font.Size = 12;

                                        //worksheet.Cells.Calculate();
                                        incRowExcel++;

                                        incRowExcel = incRowExcel + 2;

                                        worksheet.PrinterSettings.FitToPage = true;
                                        worksheet.PrinterSettings.FitToWidth = 1;
                                        worksheet.PrinterSettings.FitToHeight = 0;
                                        worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel - 1, 9];
                                        worksheet.Column(1).Width = 7;
                                        worksheet.Column(2).Width = 35;
                                        worksheet.Column(3).Width = 14;
                                        worksheet.Column(4).Width = 25;
                                        worksheet.Column(5).Width = 25;
                                        worksheet.Column(6).Width = 25;
                                        worksheet.Column(7).Width = 25;
                                        worksheet.Column(8).Width = 25;
                                        worksheet.Column(9).Width = 25;


                                        // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                        //worksheet.Cells.AutoFitColumns(); // UNTUK AUTO FIT UKURAN TULISAN DENGAN KOLOM, KEBETULAN INI BUAT HEADER AJA DIPAKENYA
                                        //worksheet.PrinterSettings.FitToPage = true;
                                        worksheet.PrinterSettings.Orientation = eOrientation.Landscape; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                        worksheet.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN
                                        worksheet.HeaderFooter.OddHeader.RightAlignedText = "&12 Laporan Penjualan Reksa Dana";

                                        // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                        //ExcelWorksheet ws = package.Workbook.Worksheets.Add("Demo");

                                        worksheet.PrinterSettings.TopMargin = (decimal).3 / 2.54M; // narrow border
                                        worksheet.PrinterSettings.RightMargin = (decimal).3 / 2.54M; //narrow borderExcelWorksheet ws = pck.Workbook.Worksheets.Add("Demo");
                                        worksheet.PrinterSettings.LeftMargin = (decimal).3 / 2.54M; // narrow border

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

                                        //worksheet.HeaderFooter.OddFooter.CenteredText = Tools.DefaultReportFooterCenterText();
                                        worksheet.HeaderFooter.OddFooter.LeftAlignedText = Tools.DefaultReportFooterLeftText();

                                        package.Save();
                                        if (_unitRegistryRpt.DownloadMode == "PDF")
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

            #region Monthly APERD Penjualan Per Tenaga Pemasaran
            else if (_unitRegistryRpt.ReportName.Equals("Monthly APERD Penjualan Per Tenaga Pemasaran"))
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
                            Select C.name AgentName,'Kantor Pusat' KantorCabang , sum(totalCashAmount) NilaiPenjualan, 0 USD from clientSubscription A
                            left join FundClient B on A.FundClientPK = B.FundClientPK and B.status = 2
                            left join Agent C on B.SellingAgentPK = C.AgentPK and C.status = 2
                            where a.status = 2 and a.posted = 1 and revised = 0
                            and  valuedate between @DateFrom and @DateTo
                            group by C.name";

                            cmd.CommandTimeout = 0;
                            cmd.Parameters.AddWithValue("@DateTo", _unitRegistryRpt.ValueDateTo);
                            cmd.Parameters.AddWithValue("@DateFrom", _unitRegistryRpt.ValueDateFrom);


                            using (SqlDataReader dr0 = cmd.ExecuteReader())
                            {
                                if (!dr0.HasRows)
                                {
                                    return false;
                                }
                                else
                                {
                                    string filePath = Tools.ReportsPath + "MonthlyAperdPenjualanPerTenagaPemasaran" + "_" + _userID + ".xlsx";
                                    string pdfPath = Tools.ReportsPath + "MonthlyAperdPenjualanPerTenagaPemasaran" + "_" + _userID + ".pdf";
                                    FileInfo excelFile = new FileInfo(filePath);
                                    if (excelFile.Exists)
                                    {
                                        excelFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                        excelFile = new FileInfo(filePath);
                                    }

                                    // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                    using (ExcelPackage package = new ExcelPackage(excelFile))
                                    {
                                        package.Workbook.Properties.Title = "LaporanPenjualanReksaDana";
                                        package.Workbook.Properties.Author = Tools.DefaultReportAuthor();
                                        package.Workbook.Properties.Comments = Tools.DefaultReportComments();
                                        package.Workbook.Properties.Company = Tools.DefaultReportCompany();
                                        package.Workbook.Properties.SetCustomPropertyValue("Checked by", _userID);
                                        package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", Tools.DefaultReportAssemblyName());

                                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Penjualan Reksa Dana");

                                        int incRowExcel = 1;

                                        worksheet.Cells[incRowExcel, 4].Value = "LAPORAN PENJUALAN REKSADANA";
                                        worksheet.Cells[incRowExcel, 4, incRowExcel, 10].Merge = true;
                                        worksheet.Cells[incRowExcel, 4, incRowExcel, 10].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 4, incRowExcel, 10].Style.Font.Size = 12;

                                        incRowExcel++;

                                        worksheet.Cells[incRowExcel, 4].Value = "SELLING AGENT PT SUCOR SEKURITAS";
                                        worksheet.Cells[incRowExcel, 4, incRowExcel, 10].Merge = true;
                                        worksheet.Cells[incRowExcel, 4, incRowExcel, 10].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 4, incRowExcel, 10].Style.Font.Size = 12;

                                        incRowExcel = incRowExcel + 2;

                                        worksheet.Cells[incRowExcel, 4].Value = "Periode: " + Convert.ToDateTime(_unitRegistryRpt.ValueDateTo).ToString("MMMM") + " " + Convert.ToDateTime(_unitRegistryRpt.ValueDateTo).Year.ToString();
                                        worksheet.Cells[incRowExcel, 4, incRowExcel, 10].Merge = true;
                                        worksheet.Cells[incRowExcel, 4, incRowExcel, 10].Style.Font.Size = 9;
                                        worksheet.Cells[incRowExcel, 4, incRowExcel, 5].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                                        incRowExcel = incRowExcel + 2;

                                        worksheet.Cells[incRowExcel, 1].Value = "Tabel 2 Laporan Penjualan Reksa Dana Per Tenaga Pemasaran";
                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Merge = true;
                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.UnderLine = true;
                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Size = 11;

                                        incRowExcel = incRowExcel + 2;


                                        //ATUR DATA GROUPINGNYA DULU
                                        List<UnitRegistryRpt> rList = new List<UnitRegistryRpt>();
                                        while (dr0.Read())
                                        {
                                            UnitRegistryRpt rSingle = new UnitRegistryRpt();
                                            rSingle.AgentName = Convert.ToString(dr0["AgentName"]);
                                            rSingle.KantorCabang = Convert.ToString(dr0["KantorCabang"]);
                                            rSingle.NilaiPenjualan = Convert.ToDecimal(dr0["NilaiPenjualan"]);
                                            rSingle.USD = Convert.ToDecimal(dr0["USD"]);

                                            rList.Add(rSingle);

                                        }

                                        var QueryByFundID =
                                                     from r in rList
                                                     group r by new { r.Agent } into rGroup
                                                     select rGroup;


                                        foreach (var rsHeader in QueryByFundID)
                                        {

                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                                            worksheet.Cells[incRowExcel, 1].Value = "NO";
                                            worksheet.Cells[incRowExcel, 2].Value = "Nama Pegawai (Izin WPE dan/atau  WAPERD)";
                                            worksheet.Cells[incRowExcel, 2].Style.WrapText = true;
                                            worksheet.Cells[incRowExcel, 3].Value = "Kantor Pusat, Kantor Lain Selain Kantor Pusat dan/atau Gerai Penjualan";
                                            worksheet.Cells[incRowExcel, 3].Style.WrapText = true;
                                            worksheet.Cells[incRowExcel, 4].Value = "Nilai Penjualan";
                                            worksheet.Cells[incRowExcel, 4].Style.WrapText = true;
                                            worksheet.Cells["D" + incRowExcel + ":E" + incRowExcel].Merge = true;

                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Size = 10;
                                            //worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                            //worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(251, 228, 213));
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                            //worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells["A" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells["E" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                                            incRowExcel = incRowExcel + 1;

                                            worksheet.Cells[incRowExcel, 4].Value = "Rp";
                                            worksheet.Cells[incRowExcel, 5].Value = "USD";
                                            worksheet.Cells["A" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells["E" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells[incRowExcel, 4, incRowExcel, 5].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 4, incRowExcel, 5].Style.Font.Size = 10;
                                            worksheet.Cells[incRowExcel, 4, incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 4, incRowExcel, 5].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells[incRowExcel, 4, incRowExcel, 5].Style.Border.Top.Style = ExcelBorderStyle.Thin;





                                            incRowExcel = incRowExcel + 1;

                                            int first = incRowExcel;

                                            int no = 1;

                                            foreach (var rsDetail in rsHeader)
                                            {
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Border.Top.Style = ExcelBorderStyle.Thin;


                                                worksheet.Cells[incRowExcel, 1].Value = no;
                                                worksheet.Cells[incRowExcel, 1].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 1, incRowExcel, 1].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 2].Value = rsDetail.AgentName;
                                                worksheet.Cells[incRowExcel, 2].Style.WrapText = true;
                                                //worksheet.Cells[incRowExcel, 2, incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                //worksheet.Cells[incRowExcel, 2, incRowExcel, 2].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.KantorCabang;
                                                worksheet.Cells[incRowExcel, 3, incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 3, incRowExcel, 3].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 4].Value = rsDetail.NilaiPenjualan;
                                                worksheet.Cells[incRowExcel, 4, incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                worksheet.Cells[incRowExcel, 4, incRowExcel, 4].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##0.00";
                                                //incRowExcel = incRowExcel + 1;
                                                //worksheet.Cells[incRowExcel, 4, incRowExcel, 5].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 4, incRowExcel, 5].Style.Font.Size = 10;
                                                worksheet.Cells[incRowExcel, 4].Value = rsDetail.NilaiPenjualan;
                                                worksheet.Cells[incRowExcel, 4, incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                worksheet.Cells[incRowExcel, 4, incRowExcel, 4].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                //worksheet.Cells[incRowExcel, 4, incRowExcel, 4].Style.Border.Top.Style = ExcelBorderStyle.Double;
                                                //worksheet.Cells[incRowExcel, 4, incRowExcel, 4].Style.Border.Bottom.Style = ExcelBorderStyle.Double;
                                                worksheet.Cells[incRowExcel, 5].Value = rsDetail.USD;
                                                worksheet.Cells[incRowExcel, 5, incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                worksheet.Cells[incRowExcel, 5, incRowExcel, 5].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.00";
                                                //worksheet.Cells[incRowExcel, 5, incRowExcel, 5].Style.Border.Top.Style = ExcelBorderStyle.Double;
                                                //worksheet.Cells[incRowExcel, 5, incRowExcel, 5].Style.Border.Bottom.Style = ExcelBorderStyle.Double;

                                                //incRowExcel = incRowExcel + 1;


                                                incRowExcel++;
                                                no++;
                                            }

                                            int last = incRowExcel - 1;
                                            //incRowExcel = incRowExcel - 1;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Border.Top.Style = ExcelBorderStyle.Thin;

                                            //incRowExcel++;

                                            worksheet.Cells[incRowExcel, 2].Value = "Total";
                                            worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##0.00";
                                            worksheet.Cells[incRowExcel, 4].Formula = "SUM(D" + first.ToString() + ":D" + last.ToString() + ")";
                                            worksheet.Cells[incRowExcel, 4].Calculate();
                                            worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.00";
                                            worksheet.Cells[incRowExcel, 5].Formula = "SUM(E" + first.ToString() + ":E" + last.ToString() + ")";
                                            worksheet.Cells[incRowExcel, 5].Calculate();

                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Size = 10;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.VerticalAlignment = ExcelVerticalAlignment.Center;


                                            //worksheet.Cells.Calculate();
                                            incRowExcel++;

                                            incRowExcel = incRowExcel + 2;
                                        }

                                        incRowExcel = incRowExcel + 4;

                                        worksheet.Cells[incRowExcel, 1].Value = "Disclaimer : ";
                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Merge = true;
                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Size = 11;

                                        incRowExcel++;

                                        worksheet.Cells[incRowExcel, 1].Value = "Dokumen ini dipersiapkan oleh PT SUCOR SEKURITAS dan hanya bisa digunakan untuk kepentingan investor tersebut diatas dan tidak untuk pihak lainnya. Laporan ini bukan merupakan konfirmasi dari PT SUCOR SEKURITAS dan tidak untuk menggantikan laporan yang wajib diterbitkan oleh Bank Kustodian, jika ada perbedaan antara laporan ini dengan laporan Bank Kustodian, maka laporan Bank Kustodian adalah yang benar. Laporan ini diproses oleh computer dan tidak memerlukan tandatangan.";
                                        worksheet.Cells[incRowExcel, 1, incRowExcel + 2, 5].Style.Font.Size = 11;
                                        worksheet.Cells[incRowExcel, 1, incRowExcel + 2, 5].Merge = true;
                                        worksheet.Cells[incRowExcel, 1, incRowExcel + 2, 5].Style.WrapText = true;

                                        incRowExcel = incRowExcel + 4;

                                        worksheet.Cells[incRowExcel, 1].Value = "PT SUCOR SEKURITAS terdaftar dan diawasi oleh OJK, dengan nomor registrasi KEP-01/PM/MI/1999";
                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Size = 11;
                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Merge = true;

                                        incRowExcel = incRowExcel + 2;

                                        //worksheet.Cells[incRowExcel, 1].Value = "PT SUCOR SEKURITAS";
                                        //worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Merge = true;
                                        //worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Bold = true;
                                        //worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Size = 11;

                                        //incRowExcel++;

                                        //worksheet.Cells[incRowExcel, 1].Value = "Sahid Sudirman Center, 12th Fl.";
                                        //worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Merge = true;
                                        //worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Size = 12;

                                        //incRowExcel++;

                                        //worksheet.Cells[incRowExcel, 1].Value = "Jl. Jenderal Sudirman Kav. 86, Jakarta 10220 - Indonesia";
                                        //worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Merge = true;
                                        //worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Size = 12;

                                        //incRowExcel++;

                                        //worksheet.Cells[incRowExcel, 1].Value = "p. +62-21  8067 3000, f.  +62-21  2788 9288 |sucorsekuritas.com";
                                        //worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Merge = true;
                                        //worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Size = 12;

                                        worksheet.PrinterSettings.FitToPage = true;
                                        worksheet.PrinterSettings.FitToWidth = 1;
                                        worksheet.PrinterSettings.FitToHeight = 0;
                                        worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel - 1, 5];
                                        worksheet.Column(1).Width = 7;
                                        worksheet.Column(2).Width = 35;
                                        worksheet.Column(3).Width = 14;
                                        worksheet.Column(4).Width = 35;
                                        worksheet.Column(5).Width = 35;


                                        // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                        //worksheet.Cells.AutoFitColumns(); // UNTUK AUTO FIT UKURAN TULISAN DENGAN KOLOM, KEBETULAN INI BUAT HEADER AJA DIPAKENYA
                                        //worksheet.PrinterSettings.FitToPage = true;
                                        worksheet.PrinterSettings.Orientation = eOrientation.Portrait; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                        worksheet.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN
                                        worksheet.HeaderFooter.OddHeader.RightAlignedText = "&12 Laporan Penjualan Reksa Dana";

                                        // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                        //ExcelWorksheet ws = package.Workbook.Worksheets.Add("Demo");

                                        worksheet.PrinterSettings.TopMargin = (decimal).3 / 2.54M; // narrow border
                                        worksheet.PrinterSettings.RightMargin = (decimal).3 / 2.54M; //narrow borderExcelWorksheet ws = pck.Workbook.Worksheets.Add("Demo");
                                        worksheet.PrinterSettings.LeftMargin = (decimal).3 / 2.54M; // narrow border

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

                                        //worksheet.HeaderFooter.OddFooter.CenteredText = Tools.DefaultReportFooterCenterText();
                                        worksheet.HeaderFooter.OddFooter.LeftAlignedText = Tools.DefaultReportFooterLeftText();

                                        package.Save();
                                        if (_unitRegistryRpt.DownloadMode == "PDF")
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

            #region High Risk Checking Subs Redemp
            else if (_unitRegistryRpt.ReportName.Equals("High Risk Checking Subs Redemp"))
            {
                try
                {
                    using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                    {
                        DbCon.Open();
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            string _paramFund = "";
                            string _status = "";
                            if (!_host.findString(_unitRegistryRpt.FundFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_unitRegistryRpt.FundFrom))
                            {
                                _paramFund = "And A.FundPK in ( " + _unitRegistryRpt.FundFrom + " ) ";
                            }
                            else
                            {
                                _paramFund = "";
                            }

                            if (_unitRegistryRpt.Status == 1)
                            {
                                _status = "  A.Status = 2 and A.Posted = 1 and A.Revised = 0 ";
                                //_statusRedemp = "  CR.Status = 2 and CR.Posted = 1 and CR.Revised = 0 ";
                            }
                            else if (_unitRegistryRpt.Status == 2)
                            {
                                _status = "  A.Status = 2 and A.Posted = 1 and A.Revised = 1 ";
                                //_statusRedemp = "  CR.Status = 2 and CR.Posted = 1 and CR.Revised = 1 ";
                            }
                            else if (_unitRegistryRpt.Status == 3)
                            {
                                _status = "  A.Status = 2 and A.Posted = 0 and A.Revised = 0 ";
                                //_statusRedemp = "  CR.Status = 2 and CR.Posted = 0 and CR.Revised = 0 ";
                            }
                            else if (_unitRegistryRpt.Status == 4)
                            {
                                _status = "  A.Status = 1  ";
                                //_statusRedemp = "  CR.Status = 1  ";
                            }
                            else if (_unitRegistryRpt.Status == 5)
                            {
                                _status = "  A.Status = 3  ";
                                //_statusRedemp = "  CR.Status = 3  ";
                            }
                            else if (_unitRegistryRpt.Status == 6)
                            {
                                _status = "  (A.Status = 2 or A.Posted = 1) and A.Revised = 0  ";
                                //_statusRedemp = "  (CR.Status = 2 or CR.Posted = 1) and CR.Revised = 0  ";
                            }
                            else if (_unitRegistryRpt.Status == 7)
                            {
                                _status = "  (A.Status = 1 Or A.Status = 2 or A.Posted = 1) and  A.Revised = 0  ";
                                //_statusRedemp = "  (CR.Status = 1 Or CR.Status = 2 or CR.Posted = 1) and CR.Revised = 0  ";
                            }


                            cmd.CommandText =

                            @"declare @FundPK int
                            declare @FundClientPK int
                            declare @TrxType nvarchar(50)


                            create table #Result
                            (
                            ValueDate datetime,
                            FundClientName nvarchar(100),
                            FundClientID nvarchar(50),
                            FundName nvarchar(100),
                            SubsAmount numeric(22,2),
                            RedAmount numeric(22,2),
                            )

                            create table #Report
                            (
                            PK int,
                            FundPK int,
                            ValueDate datetime,
                            TrxType nvarchar(50),
                            FundClientPK int,
                            Amount numeric(22,2)
                            )
                            insert into #Report(PK,FundPK,ValueDate,TrxType,FundClientPK,Amount)
                            select ClientSubscriptionPK,A.FundPK,ValueDate,'ClientSubscription',FundClientPK,TotalCashAmount from ClientSubscription A
                            left join Fund B on A.FundPK = B.FundPK and B.Status = 2
                            where A.status  = 2 and ValueDate between @ValueDateFrom and @ValueDateTo " + _paramFund +

                            @"insert into #Report(PK,FundPK,ValueDate,TrxType,FundClientPK,Amount)
                            select ClientRedemptionPK,A.FundPK,ValueDate,'ClientRedemption',FundClientPK,TotalCashAmount from ClientRedemption A
                            left join Fund B on A.FundPK = B.FundPK and B.Status = 2
                            where A.status  = 2 and ValueDate between @ValueDateFrom and @ValueDateTo " + _paramFund +

                            @"DECLARE A CURSOR FOR 
                            select FundPK,FundClientPK,TrxType from #Report
                            where ValueDate between @ValueDateFrom and @ValueDateTo
                            group by FundPK,TrxType,FundClientPK
                            having sum(Amount)>= 100000000 
	
                            Open A
                            Fetch Next From A
                            Into @FundPK,@FundClientPK,@TrxType

                            While @@FETCH_STATUS = 0
                            BEGIN

                            IF (@TrxType = 'ClientSubscription')
                            BEGIN
                            insert into #Result
                            (ValueDate,FundClientName,FundClientID,FundName,SubsAmount,RedAmount)
                            select ValueDate,FundClientName,FundClientID,FundName, SubsTotalCashAmount,0 RedTotalCashAmount from (
                            select A.ValueDate ValueDate,D.Name FundClientName,D.ID FundClientID,E.Name FundName,A.Amount,A.TrxType,B.TotalCashAmount SubsTotalCashAmount,0 RedTotalCashAmount from #Report A 
                            left join ClientSubscription B on A.ValueDate = B.ValueDate and A.FundClientPK = B.FundClientPK and A.FundPK = B.FundPK and B.status = 2 and B.Revised = 0
                            left join FundClient D on A.FundClientPK  = D.FundClientPK and D.Status = 2
                            left join Fund E on A.FundPK = E.FundPK and E.Status = 2
                            where A.FundPK = @FundPK and A.FundClientPK = @FundClientPK and A.ValueDate between @ValueDateFrom and @ValueDateTo and B.TotalCashAmount <> 0 and TrxType = 'ClientSubscription'
                            ) F 
                            END
                            ELSE
                            BEGIN
                            insert into #Result
                            (ValueDate,FundClientName,FundClientID,FundName,SubsAmount,RedAmount)
                            select ValueDate,FundClientName,FundClientID,FundName, 0 SubsTotalCashAmount,RedTotalCashAmount from (
                            select A.ValueDate ValueDate,D.Name FundClientName,D.ID FundClientID,E.Name FundName,A.Amount,A.TrxType,0 SubsTotalCashAmount,C.TotalCashAmount RedTotalCashAmount from #Report A 
                            left join ClientRedemption C on A.ValueDate = C.ValueDate and A.FundClientPK = C.FundClientPK and A.FundPK = C.FundPK and C.status = 2 and C.Revised = 0 
                            left join FundClient D on A.FundClientPK  = D.FundClientPK and D.Status = 2
                            left join Fund E on A.FundPK = E.FundPK and E.Status = 2
                            where A.FundPK = @FundPK and A.FundClientPK = @FundClientPK and A.ValueDate between @ValueDateFrom and @ValueDateTo and C.TotalCashAmount <> 0 and TrxType = 'ClientRedemption'
                            ) F
                            END



                            Fetch next From A Into @FundPK,@FundClientPK,@TrxType
                            END
                            Close A
                            Deallocate A

                            Select ValueDate,FundClientName,FundClientID,FundName,SubsAmount,RedAmount from #Result
                            order by FundClientName
                             ";
                            cmd.CommandTimeout = 0;
                            cmd.Parameters.AddWithValue("@ValueDateFrom", _unitRegistryRpt.ValueDateFrom);
                            cmd.Parameters.AddWithValue("@ValueDateTo", _unitRegistryRpt.ValueDateTo);
                            cmd.Parameters.AddWithValue("@FundFrom", _unitRegistryRpt.FundFrom);



                            using (SqlDataReader dr0 = cmd.ExecuteReader())
                            {
                                if (!dr0.HasRows)
                                {
                                    return false;
                                }
                                else
                                {
                                    string filePath = Tools.ReportsPath + "HighRiskCheckingSubsRedemp" + "_" + _userID + ".xlsx";
                                    string pdfPath = Tools.ReportsPath + "HighRiskCheckingSubsRedemp" + "_" + _userID + ".pdf";
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

                                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("High Risk Checking Subs Redemp");


                                        //ATUR DATA GROUPINGNYA DULU
                                        List<UnitRegistryRpt> rList = new List<UnitRegistryRpt>();
                                        while (dr0.Read())
                                        {
                                            UnitRegistryRpt rSingle = new UnitRegistryRpt();
                                            rSingle.Date = Convert.ToDateTime(dr0["ValueDate"]);
                                            rSingle.ClientName = Convert.ToString(dr0["FundClientName"]);
                                            rSingle.ClientID = Convert.ToString(dr0["FundClientID"]);
                                            rSingle.FundName = Convert.ToString(dr0["FundName"]);
                                            rSingle.SUBAmount = Convert.ToDecimal(dr0["SubsAmount"]);
                                            rSingle.REDAmount = Convert.ToDecimal(dr0["RedAmount"]);
                                            rSingle.CompanyID = _host.Get_CompanyID();

                                            rList.Add(rSingle);

                                        }


                                        var QueryByFundID =
                                            from r in rList
                                            group r by new { r.CompanyID } into rGroup
                                            select rGroup;

                                        int incRowExcel = 1;



                                        foreach (var rsHeader in QueryByFundID)
                                        {



                                            incRowExcel = incRowExcel + 2;

                                            int RowZ = incRowExcel;
                                            incRowExcel = incRowExcel + 5;

                                            //Row B = 3
                                            int RowB = incRowExcel;
                                            int RowG = incRowExcel + 1;


                                            worksheet.Cells[incRowExcel, 1].Value = "No";
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells["A" + RowB + ":A" + RowG].Merge = true;
                                            worksheet.Cells["A" + RowB + ":A" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["A" + RowB + ":A" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells["A" + RowB + ":I" + RowG].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + RowB + ":I" + RowG].Style.Border.Right.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells[incRowExcel, 2].Value = "Value Date";
                                            worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                            worksheet.Cells["B" + RowB + ":B" + RowG].Merge = true;
                                            worksheet.Cells["B" + RowB + ":B" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["B" + RowB + ":B" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 3].Value = "Fund Client Name";
                                            worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;
                                            worksheet.Cells["C" + RowB + ":C" + RowG].Merge = true;
                                            worksheet.Cells["C" + RowB + ":C" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["C" + RowB + ":C" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 4].Value = "ID";
                                            worksheet.Cells[incRowExcel, 4].Style.Font.Bold = true;
                                            worksheet.Cells["D" + RowB + ":D" + RowG].Merge = true;
                                            worksheet.Cells["D" + RowB + ":D" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["D" + RowB + ":D" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 5].Value = "Fund Name";
                                            worksheet.Cells[incRowExcel, 5].Style.Font.Bold = true;
                                            worksheet.Cells["E" + RowB + ":E" + RowG].Merge = true;
                                            worksheet.Cells["E" + RowB + ":E" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["E" + RowB + ":E" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 6].Value = "Subscription";
                                            worksheet.Cells[incRowExcel, 6].Style.Font.Bold = true;
                                            worksheet.Cells["F" + RowB + ":F" + RowG].Merge = true;
                                            worksheet.Cells["F" + RowB + ":F" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["F" + RowB + ":F" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 7].Value = "Net Subscription";
                                            worksheet.Cells[incRowExcel, 7].Style.Font.Bold = true;
                                            worksheet.Cells["G" + RowB + ":G" + RowG].Merge = true;
                                            worksheet.Cells["G" + RowB + ":G" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["G" + RowB + ":G" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 8].Value = "Redemption";
                                            worksheet.Cells[incRowExcel, 8].Style.Font.Bold = true;
                                            worksheet.Cells["H" + RowB + ":H" + RowG].Merge = true;
                                            worksheet.Cells["H" + RowB + ":H" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["H" + RowB + ":H" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 9].Value = "Net Redemption";
                                            worksheet.Cells[incRowExcel, 9].Style.Font.Bold = true;
                                            worksheet.Cells["I" + RowB + ":I" + RowG].Merge = true;
                                            worksheet.Cells["I" + RowB + ":I" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["I" + RowB + ":I" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            incRowExcel = incRowExcel + 1;

                                            // Row C = 4
                                            int RowC = incRowExcel;

                                            incRowExcel++;
                                            //area header
                                            worksheet.Cells[incRowExcel, 7].Value = 0;
                                            worksheet.Row(incRowExcel).Height = 5;
                                            worksheet.Cells[incRowExcel, 7].Calculate();
                                            worksheet.Cells[incRowExcel, 9].Value = 0;
                                            worksheet.Row(incRowExcel).Height = 5;
                                            worksheet.Cells[incRowExcel, 9].Calculate();
                                            incRowExcel++;
                                            int _no = 1;
                                            int _startRowDetail = incRowExcel;
                                            int _endRowDetail = 0;
                                            var _fundClientID = "";
                                            foreach (var rsDetail in rsHeader)
                                            {
                                                //Row D = 5
                                                int RowD = incRowExcel;
                                                int RowE = incRowExcel + 1;
                                                int RowZZ = incRowExcel - 1;

                                                //ThickBox Border

                                                worksheet.Cells["A" + RowB + ":I" + RowG].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + RowB + ":I" + RowG].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + incRowExcel + ":I" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + incRowExcel + ":I" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Medium;


                                                //area detail
                                                worksheet.Cells[incRowExcel, 1].Value = _no;
                                                worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 2].Style.Numberformat.Format = "MM/dd/yyyy";
                                                worksheet.Cells[incRowExcel, 2].Value = rsDetail.Date;
                                                worksheet.Cells["B" + incRowExcel + ":B" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.ClientName;
                                                worksheet.Cells[incRowExcel, 4].Value = rsDetail.ClientID;
                                                worksheet.Cells[incRowExcel, 5].Value = rsDetail.FundName;
                                                worksheet.Cells[incRowExcel, 6].Value = rsDetail.SUBAmount;
                                                worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0";
                                                if (_fundClientID != rsDetail.ClientID)
                                                {

                                                    worksheet.Cells[incRowExcel, 7].Formula = "SUM(F" + RowD + ")";
                                                    worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0";
                                                    worksheet.Cells[incRowExcel, 7].Calculate();
                                                }
                                                else
                                                {

                                                    worksheet.Cells[incRowExcel, 7].Formula = "SUM(G" + RowZZ + "+F" + RowD + ")";
                                                    worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0";
                                                    worksheet.Cells[incRowExcel, 7].Calculate();
                                                }


                                                worksheet.Cells[incRowExcel, 8].Value = rsDetail.REDAmount;
                                                worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0";

                                                if (_fundClientID != rsDetail.ClientID)
                                                {

                                                    worksheet.Cells[incRowExcel, 9].Formula = "SUM(H" + RowD + ")";
                                                    worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0";
                                                    worksheet.Cells[incRowExcel, 9].Calculate();
                                                }
                                                else
                                                {

                                                    worksheet.Cells[incRowExcel, 9].Formula = "SUM(I" + RowZZ + "+H" + RowD + ")";
                                                    worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0";
                                                    worksheet.Cells[incRowExcel, 9].Calculate();
                                                }

                                                _endRowDetail = incRowExcel;
                                                _no++;
                                                incRowExcel++;
                                                _fundClientID = rsDetail.ClientID;
                                            }


                                            worksheet.Cells["A" + _endRowDetail + ":I" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells[incRowExcel, 5].Value = "Total :";
                                            worksheet.Cells[incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                            worksheet.Cells[incRowExcel, 6].Formula = "SUM(F" + _startRowDetail + ":F" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 8].Formula = "SUM(H" + _startRowDetail + ":H" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 6].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 8].Style.Font.Bold = true;

                                            worksheet.Cells[incRowExcel, 6].Calculate();
                                            worksheet.Cells[incRowExcel, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                            worksheet.Cells[incRowExcel, 8].Calculate();
                                            worksheet.Cells[incRowExcel, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                            worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0";
                                            worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0";

                                            incRowExcel++;

                                            incRowExcel = incRowExcel + 4;

                                            worksheet.Cells[incRowExcel, 1].Value = "Disclaimer : ";
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Merge = true;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Size = 11;

                                            incRowExcel++;

                                            worksheet.Cells[incRowExcel, 1].Value = "Dokumen ini dipersiapkan oleh PT SUCOR SEKURITAS dan hanya bisa digunakan untuk kepentingan investor tersebut diatas dan tidak untuk pihak lainnya. Laporan ini bukan merupakan konfirmasi dari PT SUCOR SEKURITAS dan tidak untuk menggantikan laporan yang wajib diterbitkan oleh Bank Kustodian, jika ada perbedaan antara laporan ini dengan laporan Bank Kustodian, maka laporan Bank Kustodian adalah yang benar. Laporan ini diproses oleh computer dan tidak memerlukan tandatangan.";
                                            worksheet.Cells[incRowExcel, 1, incRowExcel + 2, 5].Style.Font.Size = 11;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel + 2, 5].Merge = true;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel + 2, 5].Style.WrapText = true;

                                            incRowExcel = incRowExcel + 4;

                                            worksheet.Cells[incRowExcel, 1].Value = "PT SUCOR SEKURITAS terdaftar dan diawasi oleh OJK, dengan nomor registrasi KEP-01/PM/MI/1999";
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Size = 11;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Merge = true;

                                            incRowExcel = incRowExcel + 2;

                                            //worksheet.Cells[incRowExcel, 1].Value = "PT SUCOR SEKURITAS";
                                            //worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Merge = true;
                                            //worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Bold = true;
                                            //worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Size = 11;

                                            //incRowExcel++;

                                            //worksheet.Cells[incRowExcel, 1].Value = "Sahid Sudirman Center, 12th Fl.";
                                            //worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Merge = true;
                                            //worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Size = 12;

                                            //incRowExcel++;

                                            //worksheet.Cells[incRowExcel, 1].Value = "Jl. Jenderal Sudirman Kav. 86, Jakarta 10220 - Indonesia";
                                            //worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Merge = true;
                                            //worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Size = 12;

                                            //incRowExcel++;

                                            //worksheet.Cells[incRowExcel, 1].Value = "p. +62-21  8067 3000, f.  +62-21  2788 9288 |sucorsekuritas.com";
                                            //worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Merge = true;
                                            //worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Size = 12;

                                            worksheet.Row(incRowExcel).PageBreak = _unitRegistryRpt.PageBreak;

                                        }



                                        //incRowExcel++;
                                        int _lastRow = incRowExcel;

                                        string _rangeA = "A5:P" + incRowExcel;
                                        using (ExcelRange r = worksheet.Cells[_rangeA]) // KALO  KOLOM 1 SAMPE 9 A-I
                                        {
                                            r.Style.Font.Size = 12;
                                        }

                                        worksheet.DeleteRow(_lastRow);




                                        worksheet.PrinterSettings.FitToPage = true;
                                        worksheet.PrinterSettings.FitToWidth = 1;
                                        worksheet.PrinterSettings.FitToHeight = 0;
                                        worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel - 1, 10];
                                        worksheet.Column(1).Width = 10;
                                        worksheet.Column(2).Width = 20;
                                        worksheet.Column(3).Width = 60;
                                        worksheet.Column(4).Width = 15;
                                        worksheet.Column(5).Width = 70;
                                        worksheet.Column(6).Width = 20;
                                        worksheet.Column(7).Width = 20;
                                        worksheet.Column(8).Width = 20;
                                        worksheet.Column(9).Width = 20;




                                        // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                        //worksheet.Cells["A3:I14"].AutoFitColumns(); // UNTUK AUTO FIT UKURAN TULISAN DENGAN KOLOM, KEBETULAN INI BUAT HEADER AJA DIPAKENYA
                                        //worksheetApproved.Cells["A3:E3"].AutoFitColumns();
                                        worksheet.PrinterSettings.Orientation = eOrientation.Landscape; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                        worksheet.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN

                                        //worksheet.HeaderFooter.OddHeader.CenteredText = "\n \n \n \n \n \n \n &30&B SUBSCRIPTION \n &28&B Batch Form";



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

            #region Monthly Report 100Mil
            else if (_unitRegistryRpt.ReportName.Equals("Monthly Report 100Mil"))
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



                            cmd.CommandText =

                            @"
                            select A.date Date, D.Name FundName,C.Name ClientName,C.SID SID, A.UnitAmount UnitAmount, B.Nav Nav, (A.UnitAmount * B.NAV) CashAmount from fundclientposition A
                            left join CloseNav B on A.fundpk = b.FundPK and b.Status = 2 and b.Date = @DATE 
                            left join FundClient C on A.FundClientPK = C.FundClientPK and C.Status = 2
                            left join Fund D on A.FundPK = D.FundPK and D.Status = 2
                            where A.date = @Date and (A.UnitAmount * B.NAV) > 100000000 " + _paramFund + _paramFundClient +
                            @"order by C.Name    ";
                            cmd.CommandTimeout = 0;
                            cmd.Parameters.AddWithValue("@Date", _unitRegistryRpt.ValueDateFrom);
                            cmd.Parameters.AddWithValue("@FundFrom", _unitRegistryRpt.FundFrom);
                            cmd.Parameters.AddWithValue("@FundClientFrom", _unitRegistryRpt.FundClientFrom);

                            using (SqlDataReader dr0 = cmd.ExecuteReader())
                            {
                                if (!dr0.HasRows)
                                {
                                    return false;
                                }
                                else
                                {
                                    string filePath = Tools.ReportsPath + "MonthlyReport100Mil" + "_" + _userID + ".xlsx";
                                    string pdfPath = Tools.ReportsPath + "MonthlyReport100Mil" + "_" + _userID + ".pdf";
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

                                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Monthly Report 100Mil");


                                        //ATUR DATA GROUPINGNYA DULU

                                        List<UnitRegistryRpt> rList = new List<UnitRegistryRpt>();
                                        while (dr0.Read())
                                        {
                                            UnitRegistryRpt rSingle = new UnitRegistryRpt();
                                            rSingle.Date = Convert.ToDateTime(dr0["Date"]);
                                            rSingle.FundName = Convert.ToString(dr0["FundName"]);
                                            rSingle.ClientName = Convert.ToString(dr0["ClientName"]);
                                            rSingle.SID = Convert.ToString(dr0["SID"]);
                                            rSingle.UnitBalance = Convert.ToDecimal(dr0["UnitAmount"]);
                                            rSingle.Nav = Convert.ToDecimal(dr0["Nav"]);
                                            rSingle.CashBalance = Convert.ToDecimal(dr0["CashAmount"]);
                                            rList.Add(rSingle);

                                        }


                                        var QueryByFundID =
                                            from r in rList
                                            group r by new { r.FundName } into rGroup
                                            select rGroup;

                                        int incRowExcel = 1;

                                        foreach (var rsHeader in QueryByFundID)
                                        {


                                            incRowExcel = incRowExcel + 2;

                                            int RowZ = incRowExcel;
                                            incRowExcel = incRowExcel + 4;

                                            worksheet.Cells[incRowExcel, 1].Value = _host.Get_CompanyName();
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Size = 12;
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells["A" + incRowExcel + ":E" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 6].Value = "Date       :";
                                            worksheet.Cells[incRowExcel, 7].Value = DateTime.Now;
                                            worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "dd-MMMM-yyyy";
                                            worksheet.Cells[incRowExcel, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Value = "MONTHLY REPORT 100 MILLION";
                                            worksheet.Cells["A" + incRowExcel + ":E" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 6].Value = "Time       :";
                                            worksheet.Cells[incRowExcel, 7].Value = DateTime.Now.ToString("H:mm:ss");
                                            incRowExcel++;

                                            incRowExcel++;


                                            incRowExcel = incRowExcel + 1;



                                            //Row B = 3
                                            int RowB = incRowExcel;
                                            int RowG = incRowExcel + 1;


                                            worksheet.Cells[incRowExcel, 1].Value = "No";
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells["A" + RowB + ":A" + RowG].Merge = true;
                                            worksheet.Cells["A" + RowB + ":A" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["A" + RowB + ":A" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells["A" + RowB + ":H" + RowG].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + RowB + ":H" + RowG].Style.Border.Right.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells[incRowExcel, 2].Value = "Date";
                                            worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                            worksheet.Cells["B" + RowB + ":B" + RowG].Merge = true;
                                            worksheet.Cells["B" + RowB + ":B" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["B" + RowB + ":B" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 3].Value = "Fund Name";
                                            worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;
                                            worksheet.Cells["C" + RowB + ":C" + RowG].Merge = true;
                                            worksheet.Cells["C" + RowB + ":C" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["C" + RowB + ":C" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 4].Value = "Client Name";
                                            worksheet.Cells[incRowExcel, 4].Style.Font.Bold = true;
                                            worksheet.Cells["D" + RowB + ":D" + RowG].Merge = true;
                                            worksheet.Cells["D" + RowB + ":D" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["D" + RowB + ":D" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 5].Value = "SID";
                                            worksheet.Cells[incRowExcel, 5].Style.Font.Bold = true;
                                            worksheet.Cells["E" + RowB + ":E" + RowG].Merge = true;
                                            worksheet.Cells["E" + RowB + ":E" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["E" + RowB + ":E" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;


                                            worksheet.Cells[incRowExcel, 6].Value = "Unit Amount";
                                            worksheet.Cells[incRowExcel, 6].Style.Font.Bold = true;
                                            worksheet.Cells["F" + RowB + ":F" + RowG].Merge = true;
                                            worksheet.Cells["F" + RowB + ":F" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["F" + RowB + ":F" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 7].Value = "Nav";
                                            worksheet.Cells[incRowExcel, 7].Style.Font.Bold = true;
                                            worksheet.Cells["G" + RowB + ":G" + RowG].Merge = true;
                                            worksheet.Cells["G" + RowB + ":G" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["G" + RowB + ":G" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 8].Value = "Cash Amount";
                                            worksheet.Cells[incRowExcel, 8].Style.Font.Bold = true;
                                            worksheet.Cells["H" + RowB + ":H" + RowG].Merge = true;
                                            worksheet.Cells["H" + RowB + ":H" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["H" + RowB + ":H" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;



                                            incRowExcel++;

                                            // Row C = 4
                                            int RowC = incRowExcel;

                                            incRowExcel++;
                                            //area header

                                            int _no = 1;
                                            int _startRowDetail = incRowExcel;
                                            int _endRowDetail = 0;
                                            foreach (var rsDetail in rsHeader)
                                            {
                                                //Row D = 5
                                                int RowD = incRowExcel;
                                                int RowE = incRowExcel + 1;


                                                //ThickBox Border

                                                worksheet.Cells["A" + RowB + ":H" + RowG].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + RowB + ":H" + RowG].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + incRowExcel + ":H" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + incRowExcel + ":H" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Medium;

                                                //area detail
                                                worksheet.Cells[incRowExcel, 1].Value = _no;
                                                worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 2].Value = rsDetail.Date;
                                                worksheet.Cells[incRowExcel, 2].Style.Numberformat.Format = "MM/dd/yyyy";
                                                worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.FundName;
                                                worksheet.Cells[incRowExcel, 4].Value = rsDetail.ClientName;
                                                worksheet.Cells[incRowExcel, 5].Value = rsDetail.SID;
                                                worksheet.Cells[incRowExcel, 6].Value = rsDetail.UnitBalance;
                                                worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.0000";
                                                worksheet.Cells[incRowExcel, 7].Value = rsDetail.Nav;
                                                worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0.0000";
                                                worksheet.Cells[incRowExcel, 8].Value = rsDetail.CashBalance;
                                                worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0";


                                                _endRowDetail = incRowExcel;
                                                _no++;
                                                incRowExcel++;

                                            }


                                            worksheet.Cells["A" + _endRowDetail + ":H" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + _startRowDetail + ":A" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + _startRowDetail + ":A" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["B" + _startRowDetail + ":B" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Medium;


                                            worksheet.Cells[incRowExcel, 4].Value = "Total :";
                                            worksheet.Cells[incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                            worksheet.Cells[incRowExcel, 6].Formula = "SUM(F" + _startRowDetail + ":F" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 8].Formula = "SUM(H" + _startRowDetail + ":H" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 6].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 8].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 6].Calculate();
                                            worksheet.Cells[incRowExcel, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                            worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0";
                                            worksheet.Cells[incRowExcel, 8].Calculate();
                                            worksheet.Cells[incRowExcel, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                            worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0";

                                            incRowExcel++;

                                            incRowExcel = incRowExcel + 4;

                                            worksheet.Cells[incRowExcel, 1].Value = "Disclaimer : ";
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Merge = true;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Size = 13;

                                            incRowExcel++;

                                            worksheet.Cells[incRowExcel, 1].Value = "Dokumen ini dipersiapkan oleh PT SUCOR SEKURITAS dan hanya bisa digunakan untuk kepentingan investor tersebut diatas dan tidak untuk pihak lainnya. Laporan ini bukan merupakan konfirmasi dari PT SUCOR SEKURITAS dan tidak untuk menggantikan laporan yang wajib diterbitkan oleh Bank Kustodian, jika ada perbedaan antara laporan ini dengan laporan Bank Kustodian, maka laporan Bank Kustodian adalah yang benar. Laporan ini diproses oleh computer dan tidak memerlukan tandatangan.";
                                            worksheet.Cells[incRowExcel, 1, incRowExcel + 2, 8].Style.Font.Size = 12;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel + 2, 8].Merge = true;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel + 2, 8].Style.WrapText = true;

                                            incRowExcel = incRowExcel + 4;

                                            worksheet.Cells[incRowExcel, 1].Value = "PT SUCOR SEKURITAS terdaftar dan diawasi oleh OJK, dengan nomor registrasi KEP-01/PM/MI/1999";
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 8].Style.Font.Size = 12;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 8].Merge = true;

                                            incRowExcel = incRowExcel + 2;

                                            //worksheet.Cells[incRowExcel, 1].Value = "PT SUCOR SEKURITAS";
                                            //worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Merge = true;
                                            //worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Bold = true;
                                            //worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Size = 13;

                                            //incRowExcel++;

                                            //worksheet.Cells[incRowExcel, 1].Value = "Sahid Sudirman Center, 12th Fl.";
                                            //worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Merge = true;
                                            //worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Size = 12;

                                            //incRowExcel++;

                                            //worksheet.Cells[incRowExcel, 1].Value = "Jl. Jenderal Sudirman Kav. 86, Jakarta 10220 - Indonesia";
                                            //worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Merge = true;
                                            //worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Size = 12;

                                            //incRowExcel++;

                                            //worksheet.Cells[incRowExcel, 1].Value = "p. +62-21  8067 3000, f.  +62-21  2788 9288 |sucorsekuritas.com";
                                            //worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Merge = true;
                                            //worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Size = 12;

                                            worksheet.Row(incRowExcel).PageBreak = _unitRegistryRpt.PageBreak;

                                        }
                                        //incRowExcel++;
                                        int _lastRow = incRowExcel;

                                        string _rangeA = "A5:P" + incRowExcel;
                                        using (ExcelRange r = worksheet.Cells[_rangeA]) // KALO  KOLOM 1 SAMPE 9 A-I
                                        {
                                            r.Style.Font.Size = 12;
                                        }

                                        worksheet.DeleteRow(_lastRow);


                                        worksheet.PrinterSettings.FitToPage = true;
                                        worksheet.PrinterSettings.FitToWidth = 1;
                                        worksheet.PrinterSettings.FitToHeight = 0;
                                        worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel - 1, 9];
                                        worksheet.Column(1).Width = 10;
                                        worksheet.Column(2).Width = 15;
                                        worksheet.Column(3).Width = 60;
                                        worksheet.Column(4).Width = 70;
                                        worksheet.Column(5).Width = 30;
                                        worksheet.Column(6).Width = 20;
                                        worksheet.Column(7).Width = 20;
                                        worksheet.Column(8).Width = 20;


                                        // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                        //worksheet.Cells["A3:I14"].AutoFitColumns(); // UNTUK AUTO FIT UKURAN TULISAN DENGAN KOLOM, KEBETULAN INI BUAT HEADER AJA DIPAKENYA
                                        //worksheetApproved.Cells["A3:E3"].AutoFitColumns();
                                        worksheet.PrinterSettings.Orientation = eOrientation.Landscape; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                        worksheet.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN

                                        //worksheet.HeaderFooter.OddHeader.CenteredText = "\n \n \n \n \n \n \n &30&B REDEMPTION \n &28&B Batch Form";



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

            #region Client Statement
            else if (_unitRegistryRpt.ReportName.Equals("Client Statement"))
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
                            string _paramFund = "";
                            string _paramFundClient = "";

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
                            if (_unitRegistryRpt.Status == 1)
                            {
                                _statusSubs = "  A.Status = 2 and A.Posted = 1 and A.Revised = 0 ";
                                _statusRedemp = "  A.Status = 2 and A.Posted = 1 and A.Revised = 0 ";
                            }
                            else if (_unitRegistryRpt.Status == 2)
                            {
                                _statusSubs = "  A.Status = 2 and A.Posted = 1 and A.Revised = 1 ";
                                _statusRedemp = "  A.Status = 2 and A.Posted = 1 and A.Revised = 1 ";
                            }
                            else if (_unitRegistryRpt.Status == 3)
                            {
                                _statusSubs = "  A.Status = 2 and A.Posted = 0 and A.Revised = 0 ";
                                _statusRedemp = "  A.Status = 2 and A.Posted = 0 and A.Revised = 0 ";
                            }
                            else if (_unitRegistryRpt.Status == 4)
                            {
                                _statusSubs = "  A.Status = 1  ";
                                _statusRedemp = "  A.Status = 1  ";
                            }
                            else if (_unitRegistryRpt.Status == 5)
                            {
                                _statusSubs = "  A.Status = 3  ";
                                _statusRedemp = "  A.Status = 3  ";
                            }
                            else if (_unitRegistryRpt.Status == 6)
                            {
                                _statusSubs = "  (A.Status = 2 or A.Posted = 1) and A.Revised = 0  ";
                                _statusRedemp = "  (A.Status = 2 or A.Posted = 1) and A.Revised = 0  ";
                            }
                            else if (_unitRegistryRpt.Status == 7)
                            {
                                _statusSubs = "  (A.Status = 1 Or A.Status = 2 or A.Posted = 1) and  A.Revised = 0  ";
                                _statusRedemp = "  (A.Status = 1 Or A.Status = 2 or A.Posted = 1) and A.Revised = 0  ";
                            }
                            cmd.CommandText =



                            @"Select A.FundPK,A.FundClientPK,A.SettlementDate,A.Remark,A.AgentFeeAmount,A.DepartmentName,A.AgentName,A.FundID,A.FundName ,A.NAVDate,A.Type,A.ClientID,A.ClientName,A.CashAmount,A.UnitAmount,A.NetAmount,A.Nav from ( 
                            Select F.FUndPK,FC.FundClientPK,A.ValueDate SettlementDate,A.Description Remark,A.AgentFeeAmount,D.Name DepartmentName,AG.Name AgentName,F.ID FundID,F.Name FundName,NAVDate,'Subscription' Type, Fc.ID ClientID,FC.Name ClientName, CashAmount, UnitAmount ,A.Nav,TotalCashAmount NetAmount  
                            from ClientSubscription A left join Fund F on A.FundPK = F.fundPK and f.Status=2   
                            left join FundClient FC on A.FundClientPK = FC.FundClientPK and fc.Status=2   
                            left join Agent AG on A.AgentPK = AG.AgentPK and AG.Status=2 
                            left join Department D on D.DepartmentPK = AG.DepartmentPK and D.Status=2
                            where " + _statusSubs + _paramFund + _paramFundClient +
                            @" and NAVDate Between @ValueDateFrom and @ValueDateTo  
                            UNION ALL   
                            Select F.FundPK,FC.FundClientPK,A.PaymentDate SettlementDate,A.Description Remark,A.AgentFeeAmount,D.Name DepartmentName,AG.Name AgentName,F.ID FundID,F.Name FundName,NAVDate,'Redemption' Type, Fc.ID ClientID,FC.Name ClientName, CashAmount, UnitAmount,A.Nav,TotalCashAmount NetAmount  
                            from ClientRedemption A left join Fund F on A.FundPK = F.fundPK and f.Status=2    
                            left join FundClient FC on A.FundClientPK = FC.FundClientPK and fc.Status=2    
                            left join Agent AG on A.AgentPK = AG.AgentPK and AG.Status=2 
                            left join Department D on D.DepartmentPK = AG.DepartmentPK and D.Status=2 
                            where " + _statusRedemp + _paramFund + _paramFundClient +
                            @" and NAVDate Between @ValueDateFrom and @ValueDateTo 
                            )A   
                            Group by A.FundPK,A.FundClientPK,A.SettlementDate,A.Remark,A.AgentFeeAmount,A.DepartmentName,A.AgentName,A.FundID,A.FundName ,A.NAVDate,A.Type,A.ClientID,A.ClientName,A.CashAmount,A.UnitAmount,A.NetAmount,A.Nav 
                            order by A.FundName, A.NAVDate Asc ";
                            cmd.CommandTimeout = 0;
                            cmd.Parameters.AddWithValue("@ValueDateFrom", _unitRegistryRpt.ValueDateFrom);
                            cmd.Parameters.AddWithValue("@ValueDateTo", _unitRegistryRpt.ValueDateTo);
                            cmd.Parameters.AddWithValue("@FundFrom", _unitRegistryRpt.FundFrom);
                            cmd.Parameters.AddWithValue("@FundClientFrom", _unitRegistryRpt.FundClientFrom);




                            using (SqlDataReader dr0 = cmd.ExecuteReader())
                            {
                                if (!dr0.HasRows)
                                {
                                    return false;
                                }
                                else
                                {
                                    string filePath = Tools.ReportsPath + "ClientStatement" + "_" + _userID + ".xlsx";
                                    string pdfPath = Tools.ReportsPath + "ClientStatement" + "_" + _userID + ".pdf";
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

                                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Client Statement");


                                        //ATUR DATA GROUPINGNYA DULU
                                        List<UnitRegistryRpt> rList = new List<UnitRegistryRpt>();
                                        while (dr0.Read())
                                        {
                                            UnitRegistryRpt rSingle = new UnitRegistryRpt();
                                            rSingle.FundPK = Convert.ToInt32(dr0["FundPK"]);
                                            rSingle.FundName = Convert.ToString(dr0["FundName"]);
                                            rSingle.NAVDate = Convert.ToDateTime(dr0["NAVDate"]);
                                            rSingle.Nav = Convert.ToDecimal(dr0["NAV"]);
                                            rSingle.FundClientPK = Convert.ToInt32(dr0["FundClientPK"]);
                                            rSingle.ClientID = Convert.ToString(dr0["ClientID"]);
                                            rSingle.ClientName = Convert.ToString(dr0["ClientName"]);
                                            rSingle.Type = Convert.ToString(dr0["Type"]);
                                            rSingle.CashBalance = Convert.ToDecimal(dr0["CashAmount"]);
                                            rSingle.UnitBalance = Convert.ToDecimal(dr0["UnitAmount"]);
                                            rSingle.AgentFeeAmount = Convert.ToDecimal(dr0["AgentFeeAmount"]);
                                            rSingle.NetAmount = Convert.ToDecimal(dr0["NetAmount"]);
                                            rSingle.Remark = Convert.ToString(dr0["Remark"]);
                                            rList.Add(rSingle);
                                        }


                                        var QueryByFundID =
                                           from r in rList
                                           orderby r.FundName, r.NAVDate ascending
                                           group r by new { r.FundClientPK, r.ClientName, r.FundName, r.FundPK } into rGroup
                                           select rGroup;

                                        int incRowExcel = 1;



                                        foreach (var rsHeader in QueryByFundID)
                                        {

                                            incRowExcel = incRowExcel + 2;


                                            int RowZ = incRowExcel;
                                            incRowExcel = incRowExcel + 4;
                                            worksheet.Cells[incRowExcel, 1].Value = _host.Get_CompanyName();
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Size = 24;
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells["A" + incRowExcel + ":C" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 9].Value = "CUSTOMER PORTFOLIO";
                                            worksheet.Cells[incRowExcel, 9].Style.Font.Bold = true;


                                            worksheet.Cells["I" + incRowExcel + ":J" + incRowExcel].Merge = true;

                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Value = "Gd. Equity Tower Lt. 31";
                                            worksheet.Cells["A" + incRowExcel + ":C" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 9].Value = "As of Date    :";
                                            worksheet.Cells[incRowExcel, 10].Value = _unitRegistryRpt.ValueDateTo;

                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Value = "Jl. Jendral Sudirman kav 52-53, Jakarta 12910";
                                            worksheet.Cells["A" + incRowExcel + ":D" + incRowExcel].Merge = true;

                                            incRowExcel++;

                                            worksheet.Cells[incRowExcel, 1].Value = "Tel : ";
                                            worksheet.Cells[incRowExcel, 2].Value = _host.Get_CompanyPhone();
                                            worksheet.Cells["B" + incRowExcel + ":B" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 3].Value = "Fax :      021-57973929";
                                            worksheet.Cells["C" + incRowExcel + ":E" + incRowExcel].Merge = true;

                                            incRowExcel++;
                                            incRowExcel = incRowExcel + 1;

                                            //Row A = 2
                                            int RowA = incRowExcel;
                                            worksheet.Cells[incRowExcel, 1].Value = "A/C No.  : ";
                                            worksheet.Cells[incRowExcel, 2].Value = "JKT 02001 ";
                                            worksheet.Cells[incRowExcel, 9].Value = "Date       : ";
                                            worksheet.Cells[incRowExcel, 10].Value = _unitRegistryRpt.ValueDateTo;

                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Value = "Name     : ";
                                            worksheet.Cells[incRowExcel, 2].Value = _host.Get_CompanyName() + " (SUCOR) ";
                                            worksheet.Cells[incRowExcel, 9].Value = "Time       : ";

                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Value = "Address :";
                                            worksheet.Cells[incRowExcel, 2].Value = "SAHID SUDIRMAN CENTER LANTAI 12 ";
                                            worksheet.Cells[incRowExcel, 9].Value = "Page       : ";

                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 2].Value = "JL. JEND. SUDIRMAN KAV.86";
                                            worksheet.Cells["B" + incRowExcel + ":C" + incRowExcel].Merge = true;
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 2].Value = "JAKARTA,";
                                            worksheet.Cells["B" + incRowExcel + ":C" + incRowExcel].Merge = true;
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Value = "Phone    : ";
                                            worksheet.Cells[incRowExcel, 2].Value = "021-806-73-000 ";
                                            worksheet.Cells[incRowExcel, 3].Value = "(Office)";
                                            worksheet.Cells[incRowExcel, 9].Value = "Fax        :";

                                            incRowExcel++;
                                            incRowExcel = incRowExcel + 1;
                                            worksheet.Cells["A" + incRowExcel + ":J" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Medium;


                                            //incRowExcel = incRowExcel + 2;


                                            //Row B = 3
                                            int RowB = incRowExcel;
                                            int RowG = incRowExcel + 1;


                                            worksheet.Cells[incRowExcel, 1].Value = "No";
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells["A" + RowB + ":A" + RowG].Merge = true;
                                            worksheet.Cells["A" + RowB + ":A" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["A" + RowB + ":A" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 2].Value = "Fund";
                                            worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                            worksheet.Cells["B" + RowB + ":C" + RowG].Merge = true;
                                            worksheet.Cells["B" + RowB + ":C" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["B" + RowB + ":C" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 4].Value = "Description";
                                            worksheet.Cells[incRowExcel, 4].Style.Font.Bold = true;
                                            worksheet.Cells["D" + RowB + ":D" + RowG].Merge = true;
                                            worksheet.Cells["D" + RowB + ":D" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["D" + RowB + ":D" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 5].Value = "Trans. Date";
                                            worksheet.Cells[incRowExcel, 5].Style.Font.Bold = true;
                                            worksheet.Cells["E" + RowB + ":E" + RowG].Merge = true;
                                            worksheet.Cells["E" + RowB + ":E" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["E" + RowB + ":E" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 6].Value = "Net Amount";
                                            worksheet.Cells[incRowExcel, 6].Style.Font.Bold = true;
                                            worksheet.Cells["F" + RowB + ":F" + RowG].Merge = true;
                                            worksheet.Cells["F" + RowB + ":F" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["F" + RowB + ":F" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 7].Value = "NAV";
                                            worksheet.Cells[incRowExcel, 7].Style.Font.Bold = true;
                                            worksheet.Cells["G" + RowB + ":G" + RowG].Merge = true;
                                            worksheet.Cells["G" + RowB + ":G" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["G" + RowB + ":G" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 8].Value = "Unit";
                                            worksheet.Cells[incRowExcel, 8].Style.Font.Bold = true;
                                            worksheet.Cells["H" + RowB + ":H" + RowG].Merge = true;
                                            worksheet.Cells["H" + RowB + ":H" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["H" + RowB + ":H" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;


                                            worksheet.Cells[incRowExcel, 9].Value = "Balance";
                                            worksheet.Cells[incRowExcel, 9].Style.Font.Bold = true;
                                            worksheet.Cells["I" + RowB + ":J" + RowG].Merge = true;
                                            worksheet.Cells["I" + RowB + ":J" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["I" + RowB + ":J" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;


                                            incRowExcel = incRowExcel + 2;

                                            // Row C = 4
                                            int RowC = incRowExcel;
                                            worksheet.Cells[incRowExcel, 9].Value = _host.Get_UnitAmountByFundPKandFundClientPK(rsHeader.Key.FundPK, rsHeader.Key.FundClientPK, _host.GetWorkingDay(Convert.ToDateTime(_unitRegistryRpt.ValueDateFrom), -1));
                                            worksheet.Row(incRowExcel).Height = 5;
                                            worksheet.Cells[incRowExcel, 9].Calculate();
                                            incRowExcel++;
                                            //area header

                                            int _no = 1;
                                            int _startRowDetail = incRowExcel;
                                            int _endRowDetail = 0;
                                            foreach (var rsDetail in rsHeader)
                                            {
                                                //Row D = 5
                                                int RowD = incRowExcel;
                                                int RowE = incRowExcel + 1;
                                                int RowZZ = incRowExcel - 1;

                                                //ThickBox Border

                                                worksheet.Cells["A" + RowB + ":J" + RowG].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + RowB + ":J" + RowG].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                                //area detail
                                                worksheet.Cells[incRowExcel, 1].Value = _no;
                                                worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 2].Value = rsDetail.FundName;
                                                worksheet.Cells["B" + incRowExcel + ":C" + incRowExcel].Merge = true;
                                                if (rsDetail.Type == "Subscription")
                                                {
                                                    worksheet.Cells[incRowExcel, 4].Value = "Subscribe";
                                                    worksheet.Cells[incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                }
                                                else
                                                {
                                                    worksheet.Cells[incRowExcel, 4].Value = "Redeem";
                                                    worksheet.Cells[incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                    worksheet.Cells["A" + incRowExcel + ":H" + incRowExcel].Style.Font.Color.SetColor(Color.Red);
                                                }
                                                worksheet.Cells[incRowExcel, 5].Value = rsDetail.NAVDate;
                                                worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "dd-MMM-yyyy";
                                                worksheet.Cells[incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                if (rsDetail.Type == "Subscription")
                                                {
                                                    worksheet.Cells[incRowExcel, 6].Value = rsDetail.NetAmount;
                                                }
                                                else
                                                {
                                                    worksheet.Cells[incRowExcel, 6].Value = rsDetail.NetAmount * -1;
                                                }
                                                worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 7].Value = rsDetail.Nav;
                                                worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0.0000";
                                                if (rsDetail.Type == "Subscription")
                                                {
                                                    worksheet.Cells[incRowExcel, 8].Value = rsDetail.UnitBalance;
                                                }
                                                else
                                                {
                                                    worksheet.Cells[incRowExcel, 8].Value = rsDetail.UnitBalance * -1;
                                                }
                                                worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0.0000";
                                                worksheet.Cells[incRowExcel, 9].Formula = "SUM(I" + RowZZ + "+H" + RowD + ")";
                                                worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 9].Calculate();
                                                worksheet.Cells["I" + incRowExcel + ":J" + incRowExcel].Merge = true;
                                                _endRowDetail = incRowExcel;
                                                _no++;
                                                incRowExcel++;

                                            }



                                            worksheet.Cells["A" + _endRowDetail + ":J" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;


                                            incRowExcel++;


                                            using (SqlConnection DbCon1 = new SqlConnection(Tools.conString))
                                            {
                                                DbCon1.Open();
                                                using (SqlCommand cmd1 = DbCon1.CreateCommand())
                                                {
                                                    cmd1.CommandText =

                                                   @"select FC.FundClientPK,F.Name FundName,isnull(UnitAmount,0)UnitAmount,1000 AvgNav,isnull(B.Nav,0) CloseNav, 1000 * UnitAmount FundValue,isnull(B.Nav,0) * isnull(UnitAmount,0) MarketValue,(isnull(B.Nav,0)-1000) * isnull(UnitAmount,0) Unrealized,
                                                    (((isnull(B.Nav,0)-1000) * isnull(UnitAmount,0))/(1000 * (case when UnitAmount  = 0 then 1 else isnull(UnitAmount,1)End))*100) UnrealizedPercent 
                                                    from FundClientPosition A
                                                    left join CloseNav B on A.FundPK = B.FundPK and A.Date = B.Date and B.Status  = 2
                                                    left join Fund F on A.FundPK = F.FundPK and F.Status  = 2
                                                    left join FundClient FC on A.FundClientPK = FC.FundClientPK and FC.Status  = 2
                                                    where A.Date = @ValueDateTo " + _paramFund + _paramFundClient;

                                                    cmd1.CommandTimeout = 0;
                                                    cmd1.Parameters.AddWithValue("@ValueDateTo", _unitRegistryRpt.ValueDateTo);
                                                    cmd1.Parameters.AddWithValue("@FundFrom", _unitRegistryRpt.FundFrom);
                                                    cmd1.Parameters.AddWithValue("@FundClientFrom", _unitRegistryRpt.FundClientFrom);


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
                                                                    rSingle1.FundClientPK = Convert.ToInt32(dr1["FundClientPK"]);
                                                                    rSingle1.FundName = Convert.ToString(dr1["FundName"]);
                                                                    rSingle1.UnitBalance = Convert.ToDecimal(dr1["UnitAmount"]);
                                                                    rSingle1.AvgNav = Convert.ToDecimal(dr1["AvgNav"]);
                                                                    rSingle1.CloseNav = Convert.ToDecimal(dr1["CloseNav"]);
                                                                    rSingle1.FundValue = Convert.ToDecimal(dr1["FundValue"]);
                                                                    rSingle1.MarketValue = Convert.ToDecimal(dr1["MarketValue"]);
                                                                    rSingle1.Unrealized = Convert.ToDecimal(dr1["Unrealized"]);
                                                                    rSingle1.UnrealizedPercent = Convert.ToDecimal(dr1["UnrealizedPercent"]);
                                                                    rList1.Add(rSingle1);

                                                                }


                                                                var QueryByFundID1 =
                                                                    from r1 in rList1
                                                                    group r1 by new { r1.FundClientPK } into rGroup1
                                                                    select rGroup1;

                                                                incRowExcel = incRowExcel + 1;
                                                                int _endRowDetailZ = 0;


                                                                foreach (var rsHeader1 in QueryByFundID1)
                                                                {

                                                                    incRowExcel = incRowExcel + 2;
                                                                    //Row B = 3
                                                                    int RowBZ = incRowExcel;
                                                                    int RowGZ = incRowExcel + 1;


                                                                    worksheet.Cells[incRowExcel, 1].Value = "Fund";
                                                                    worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                                                    worksheet.Cells["A" + RowBZ + ":C" + RowGZ].Merge = true;
                                                                    worksheet.Cells["A" + RowBZ + ":C" + RowGZ].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                                    worksheet.Cells["A" + RowBZ + ":C" + RowGZ].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                                    worksheet.Cells[incRowExcel, 4].Value = "Unit";
                                                                    worksheet.Cells[incRowExcel, 4].Style.Font.Bold = true;
                                                                    worksheet.Cells["D" + RowBZ + ":D" + RowGZ].Merge = true;
                                                                    worksheet.Cells["D" + RowBZ + ":D" + RowGZ].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                                    worksheet.Cells["D" + RowBZ + ":D" + RowGZ].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                                    worksheet.Cells[incRowExcel, 5].Value = "Average Nav";
                                                                    worksheet.Cells[incRowExcel, 5].Style.Font.Bold = true;
                                                                    worksheet.Cells["E" + RowBZ + ":E" + RowGZ].Merge = true;
                                                                    worksheet.Cells["E" + RowBZ + ":E" + RowGZ].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                                    worksheet.Cells["E" + RowBZ + ":E" + RowGZ].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                                    worksheet.Cells[incRowExcel, 6].Value = "Close Nav";
                                                                    worksheet.Cells[incRowExcel, 6].Style.Font.Bold = true;
                                                                    worksheet.Cells["F" + RowBZ + ":F" + RowGZ].Merge = true;
                                                                    worksheet.Cells["F" + RowBZ + ":F" + RowGZ].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                                    worksheet.Cells["F" + RowBZ + ":F" + RowGZ].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                                    worksheet.Cells[incRowExcel, 7].Value = "Fund Value";
                                                                    worksheet.Cells[incRowExcel, 7].Style.Font.Bold = true;
                                                                    worksheet.Cells["G" + RowBZ + ":G" + RowGZ].Merge = true;
                                                                    worksheet.Cells["G" + RowBZ + ":G" + RowGZ].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                                    worksheet.Cells["G" + RowBZ + ":G" + RowGZ].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                                    worksheet.Cells[incRowExcel, 8].Value = "Market Value";
                                                                    worksheet.Cells[incRowExcel, 8].Style.Font.Bold = true;
                                                                    worksheet.Cells["H" + RowBZ + ":H" + RowGZ].Merge = true;
                                                                    worksheet.Cells["H" + RowBZ + ":H" + RowGZ].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                                    worksheet.Cells["H" + RowBZ + ":H" + RowGZ].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                                    worksheet.Cells[incRowExcel, 9].Value = "Unrealized";
                                                                    worksheet.Cells[incRowExcel, 9].Style.Font.Bold = true;
                                                                    worksheet.Cells["I" + RowBZ + ":I" + RowGZ].Merge = true;
                                                                    worksheet.Cells["I" + RowBZ + ":I" + RowGZ].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                                    worksheet.Cells["I" + RowBZ + ":I" + RowGZ].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                                    worksheet.Cells[incRowExcel, 10].Value = "%";
                                                                    worksheet.Cells[incRowExcel, 10].Style.Font.Bold = true;
                                                                    worksheet.Cells["J" + RowBZ + ":J" + RowGZ].Merge = true;
                                                                    worksheet.Cells["J" + RowBZ + ":J" + RowGZ].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                                    worksheet.Cells["J" + RowBZ + ":J" + RowGZ].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                                    worksheet.Cells["A" + RowBZ + ":J" + RowGZ].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                                    worksheet.Cells["A" + RowBZ + ":J" + RowGZ].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

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


                                                                        //area detail
                                                                        worksheet.Cells[incRowExcel, 1].Value = rsDetail1.FundName;
                                                                        worksheet.Cells[incRowExcel, 4].Value = rsDetail1.UnitBalance;
                                                                        worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##0.0000";
                                                                        worksheet.Cells[incRowExcel, 5].Value = rsDetail1.AvgNav;
                                                                        worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.0000";
                                                                        worksheet.Cells[incRowExcel, 6].Value = rsDetail1.CloseNav;
                                                                        worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.0000";
                                                                        worksheet.Cells[incRowExcel, 7].Value = rsDetail1.FundValue;
                                                                        worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0.0000";
                                                                        worksheet.Cells[incRowExcel, 8].Value = rsDetail1.MarketValue;
                                                                        worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0.0000";
                                                                        worksheet.Cells[incRowExcel, 9].Value = rsDetail1.Unrealized;
                                                                        worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0.0000";
                                                                        worksheet.Cells[incRowExcel, 10].Value = rsDetail1.UnrealizedPercent;
                                                                        worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0.0000";

                                                                        _endRowDetailZ = incRowExcel;
                                                                        _noZ++;
                                                                        incRowExcel++;

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

                                            incRowExcel = incRowExcel + 4;

                                            worksheet.Cells[incRowExcel, 1].Value = "Disclaimer : ";
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Merge = true;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Size = 13;

                                            incRowExcel++;

                                            worksheet.Cells[incRowExcel, 1].Value = "Dokumen ini dipersiapkan oleh PT SUCOR SEKURITAS dan hanya bisa digunakan untuk kepentingan investor tersebut diatas dan tidak untuk pihak lainnya. Laporan ini bukan merupakan konfirmasi dari PT SUCOR SEKURITAS dan tidak untuk menggantikan laporan yang wajib diterbitkan oleh Bank Kustodian, jika ada perbedaan antara laporan ini dengan laporan Bank Kustodian, maka laporan Bank Kustodian adalah yang benar. Laporan ini diproses oleh computer dan tidak memerlukan tandatangan.";
                                            worksheet.Cells[incRowExcel, 1, incRowExcel + 2, 15].Style.Font.Size = 12;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel + 2, 15].Merge = true;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel + 2, 15].Style.WrapText = true;

                                            incRowExcel = incRowExcel + 4;

                                            worksheet.Cells[incRowExcel, 1].Value = "PT SUCOR SEKURITAS terdaftar dan diawasi oleh OJK, dengan nomor registrasi KEP-01/PM/MI/1999";
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 15].Style.Font.Size = 12;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 15].Merge = true;

                                            incRowExcel = incRowExcel + 2;

                                            //worksheet.Cells[incRowExcel, 1].Value = "PT SUCOR SEKURITAS";
                                            //worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Merge = true;
                                            //worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Bold = true;
                                            //worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Size = 13;

                                            //incRowExcel++;

                                            //worksheet.Cells[incRowExcel, 1].Value = "Sahid Sudirman Center, 12th Fl.";
                                            //worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Merge = true;
                                            //worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Size = 12;

                                            //incRowExcel++;

                                            //worksheet.Cells[incRowExcel, 1].Value = "Jl. Jenderal Sudirman Kav. 86, Jakarta 10220 - Indonesia";
                                            //worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Merge = true;
                                            //worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Size = 12;

                                            //incRowExcel++;

                                            //worksheet.Cells[incRowExcel, 1].Value = "p. +62-21  8067 3000, f.  +62-21  2788 9288 |sucorsekuritas.com";
                                            //worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Merge = true;
                                            //worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Size = 12;

                                            worksheet.Row(incRowExcel).PageBreak = _unitRegistryRpt.PageBreak;
                                        }

                                        int _lastRow = incRowExcel;

                                        string _rangeA = "A5:J" + incRowExcel;
                                        using (ExcelRange r = worksheet.Cells[_rangeA]) // KALO  KOLOM 1 SAMPE 9 A-I
                                        {
                                            r.Style.Font.Size = 28;
                                        }

                                        worksheet.DeleteRow(_lastRow);

                                        worksheet.PrinterSettings.FitToPage = true;
                                        worksheet.PrinterSettings.FitToWidth = 1;
                                        worksheet.PrinterSettings.FitToHeight = 0;
                                        worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel - 1, 11];
                                        worksheet.Column(1).Width = 20;
                                        worksheet.Column(2).Width = 70;
                                        worksheet.Column(3).Width = 2;
                                        worksheet.Column(4).Width = 40;
                                        worksheet.Column(5).Width = 40;
                                        worksheet.Column(6).Width = 60;
                                        worksheet.Column(7).Width = 60;
                                        worksheet.Column(8).Width = 60;
                                        worksheet.Column(9).Width = 60;
                                        worksheet.Column(10).Width = 30;

                                        // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                        //worksheet.Cells["A3:I14"].AutoFitColumns(); // UNTUK AUTO FIT UKURAN TULISAN DENGAN KOLOM, KEBETULAN INI BUAT HEADER AJA DIPAKENYA
                                        //worksheetApproved.Cells["A3:E3"].AutoFitColumns();
                                        worksheet.PrinterSettings.Orientation = eOrientation.Landscape; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                        worksheet.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN

                                        //worksheet.HeaderFooter.OddHeader.CenteredText = "\n \n \n \n \n \n \n \n \n &30&B Customer Portfolio";


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

            #region Switching Report
            else if (_unitRegistryRpt.ReportName.Equals("Switching Report"))
            {
                try
                {
                    using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                    {
                        DbCon.Open();
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            string _statusSwitching = "";
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
                                _paramAgent = "And A.AgentPK in ( " + _unitRegistryRpt.AgentFrom + " ) ";
                            }
                            else
                            {
                                _paramAgent = "";
                            }

                            if (_unitRegistryRpt.Status == 1)
                            {
                                _statusSwitching = "  A.Status = 2 and A.Posted = 1 and A.Revised = 0 ";
                                //_statusRedemp = "  CR.Status = 2 and CR.Posted = 1 and CR.Revised = 0 ";
                            }
                            else if (_unitRegistryRpt.Status == 2)
                            {
                                _statusSwitching = "  A.Status = 2 and A.Posted = 1 and A.Revised = 1 ";
                                //_statusRedemp = "  CR.Status = 2 and CR.Posted = 1 and CR.Revised = 1 ";
                            }
                            else if (_unitRegistryRpt.Status == 3)
                            {
                                _statusSwitching = "  A.Status = 2 and A.Posted = 0 and A.Revised = 0 ";
                                //_statusRedemp = "  CR.Status = 2 and CR.Posted = 0 and CR.Revised = 0 ";
                            }
                            else if (_unitRegistryRpt.Status == 4)
                            {
                                _statusSwitching = "  A.Status = 1  ";
                                //_statusRedemp = "  CR.Status = 1  ";
                            }
                            else if (_unitRegistryRpt.Status == 5)
                            {
                                _statusSwitching = "  A.Status = 3  ";
                                //_statusRedemp = "  CR.Status = 3  ";
                            }
                            else if (_unitRegistryRpt.Status == 6)
                            {
                                _statusSwitching = "  (A.Status = 2 or A.Posted = 1) and A.Revised = 0  ";
                                //_statusRedemp = "  (CR.Status = 2 or CR.Posted = 1) and CR.Revised = 0  ";
                            }
                            else if (_unitRegistryRpt.Status == 7)
                            {
                                _statusSwitching = "  (A.Status = 1 Or A.Status = 2 or A.Posted = 1) and  A.Revised = 0  ";
                                //_statusRedemp = "  (CR.Status = 1 Or CR.Status = 2 or CR.Posted = 1) and CR.Revised = 0  ";
                            }


                            cmd.CommandText =

                            @" BEGIN
                            SELECT 'Switching' Type,ClientSwitchingPK,D.ContactPerson ContactPerson,D.ID BankBranchID,D.Fax1 FaxNo,ValueDate Date,NavDate NavDate, C.ID FundIDFrom,C1.ID FundIDTo, C.Name FundNameFrom,C1.Name FundNameTo,B.ID ClientID, 
                            B.Name ClientName,A.NAVFundFrom NAVFundFrom,A.NAVFundTo NAVFundTo,CashAmount CashFundFrom,unitamount UnitAmount, A.SwitchingFeePercent FeePercent,A.SwitchingFeeAmount FeeAmount, 
                            A.PaymentDate SettlementDate,A.Description Remark,A.TotalUnitAmountFundFrom TotalUnitFundFrom,A.TotalCashAmountFundFrom TotalCashFundFrom,A.TotalCashAmountFundTo TotalCashFundTo,B.NPWP NPWP,  E.Name Agent,F.Name Mgt  from ClientSwitching A    
                            left join FundClient B ON B.fundclientpk = A.fundclientpk and B.status = 2   
                            left join Fund C ON A.FundPKFrom = C.FundPK  and C.status = 2  
                            left join Fund C1 ON A.FundPKTo = C1.FundPK  and C1.status = 2     
                            left join BankBranch D ON C.BankBranchPK = D.BankBranchPK  and D.status = 2
                            left join Agent E on B.SellingAgentPK = E.AgentPK and E.Status = 2
                            left join Agent F on E.ParentPK = F.AgentPK and F.Status = 2  
                            WHERE   " + _statusSwitching + _paramFund + _paramFundClient + _paramAgent +
                            @"and NAVDate between @DateFrom and @DateTo 
                            END  ";
                            cmd.CommandTimeout = 0;
                            cmd.Parameters.AddWithValue("@DateFrom", _unitRegistryRpt.ValueDateFrom);
                            cmd.Parameters.AddWithValue("@DateTo", _unitRegistryRpt.ValueDateTo);
                            cmd.Parameters.AddWithValue("@FundFrom", _unitRegistryRpt.FundFrom);
                            cmd.Parameters.AddWithValue("@FundClientFrom", _unitRegistryRpt.FundClientFrom);
                            cmd.Parameters.AddWithValue("@AgentFrom", _unitRegistryRpt.AgentFrom);


                            using (SqlDataReader dr0 = cmd.ExecuteReader())
                            {
                                if (!dr0.HasRows)
                                {
                                    return false;
                                }
                                else
                                {
                                    string filePath = Tools.ReportsPath + "SwitchingReport" + "_" + _userID + ".xlsx";
                                    string pdfPath = Tools.ReportsPath + "SwitchingReport" + "_" + _userID + ".pdf";
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

                                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Switching Report");


                                        //ATUR DATA GROUPINGNYA DULU
                                        List<UnitRegistryRpt> rList = new List<UnitRegistryRpt>();
                                        while (dr0.Read())
                                        {
                                            UnitRegistryRpt rSingle = new UnitRegistryRpt();
                                            rSingle.ClientSwitchingPK = Convert.ToInt32(dr0["ClientSwitchingPK"]);
                                            rSingle.FundIDFrom = Convert.ToString(dr0["FundIDFrom"]);
                                            rSingle.FundIDTo = Convert.ToString(dr0["FundIDTo"]);
                                            rSingle.FundNameFrom = Convert.ToString(dr0["FundNameFrom"]);
                                            rSingle.FundNameTo = Convert.ToString(dr0["FundNameTo"]);
                                            rSingle.NAVDate = Convert.ToDateTime(dr0["NAVDate"]);
                                            rSingle.NAVFundFrom = Convert.ToDecimal(dr0["NAVFundFrom"]);
                                            rSingle.NAVFundTo = Convert.ToDecimal(dr0["NAVFundTo"]);
                                            rSingle.ClientID = Convert.ToString(dr0["ClientID"]);
                                            rSingle.ClientName = Convert.ToString(dr0["ClientName"]);
                                            rSingle.TotalUnitFundFrom = Convert.ToDecimal(dr0["TotalUnitFundFrom"]);
                                            rSingle.CashFundFrom = Convert.ToDecimal(dr0["CashFundFrom"]);
                                            rSingle.FeePercent = Convert.ToDecimal(dr0["FeePercent"]);
                                            rSingle.FeeAmount = Convert.ToDecimal(dr0["FeeAmount"]);
                                            rSingle.TotalCashFundFrom = Convert.ToDecimal(dr0["TotalCashFundFrom"]);
                                            rSingle.TotalCashFundTo = Convert.ToDecimal(dr0["TotalCashFundTo"]);
                                            rSingle.Remark = Convert.ToString(dr0["Remark"]);
                                            rSingle.ContactPerson = Convert.ToString(dr0["ContactPerson"]);
                                            rSingle.FaxNo = Convert.ToString(dr0["FaxNo"]);
                                            rSingle.BankCustodiID = Convert.ToString(dr0["BankBranchID"]);
                                            rSingle.Remark = Convert.ToString(dr0["Remark"]);
                                            rSingle.NPWP = Convert.ToString(dr0["NPWP"]);
                                            rSingle.Agent = Convert.ToString(dr0["Agent"]);
                                            rSingle.Mgt = Convert.ToString(dr0["Mgt"]);
                                            rList.Add(rSingle);

                                        }


                                        var QueryByFundID =
                                            from r in rList
                                            group r by new { r.FundNameFrom, r.NAVDate, r.ContactPerson, r.FaxNo, r.BankCustodiID } into rGroup
                                            select rGroup;

                                        int incRowExcel = 1;


                                        foreach (var rsHeader in QueryByFundID)
                                        {


                                            incRowExcel = incRowExcel + 2;

                                            incRowExcel = incRowExcel + 2;

                                            int RowZ = incRowExcel;
                                            incRowExcel = incRowExcel + 4;
                                            worksheet.Cells[incRowExcel, 1].Value = _host.Get_CompanyName();
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Size = 24;
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells["A" + incRowExcel + ":G" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 9].Value = "To ";
                                            worksheet.Cells[incRowExcel, 10].Value = ": ";
                                            worksheet.Cells[incRowExcel, 11].Value = rsHeader.Key.BankCustodiID;
                                            worksheet.Cells["K" + incRowExcel + ":L" + incRowExcel].Merge = true;


                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Value = "Gd. Equity Tower Lt. 31";
                                            worksheet.Cells["A" + incRowExcel + ":E" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 11].Value = "Custodian Services";
                                            worksheet.Cells["K" + incRowExcel + ":L" + incRowExcel].Merge = true;

                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Value = "Jl. Jendral Sudirman kav 52-53, Jakarta 12910";
                                            worksheet.Cells["A" + incRowExcel + ":E" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 9].Value = "Attention ";
                                            worksheet.Cells[incRowExcel, 10].Value = ": ";
                                            worksheet.Cells[incRowExcel, 11].Value = rsHeader.Key.ContactPerson;
                                            worksheet.Cells["K" + incRowExcel + ":L" + incRowExcel].Merge = true;
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 9].Value = "Fax ";
                                            worksheet.Cells[incRowExcel, 10].Value = ": ";
                                            worksheet.Cells[incRowExcel, 11].Value = _host.Get_CompanyFax();
                                            worksheet.Cells["K" + incRowExcel + ":L" + incRowExcel].Merge = true;

                                            worksheet.Cells[incRowExcel, 1].Value = "Tel : ";
                                            worksheet.Cells[incRowExcel, 2].Value = _host.Get_CompanyPhone();
                                            worksheet.Cells["B" + incRowExcel + ":B" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 3].Value = "Fax : ";
                                            worksheet.Cells[incRowExcel, 4].Value = _host.Get_CompanyFax();
                                            worksheet.Cells["D" + incRowExcel + ":D" + incRowExcel].Merge = true;
                                            incRowExcel++;
                                            incRowExcel = incRowExcel + 1;
                                            worksheet.Cells[incRowExcel, 5].Value = "SWITCHING BATCH FORM";
                                            worksheet.Cells[incRowExcel, 5].Style.Font.Size = 65;
                                            worksheet.Cells[incRowExcel, 5].Style.Font.Bold = true;
                                            worksheet.Cells["E" + incRowExcel + ":H" + incRowExcel].Merge = true;
                                            worksheet.Cells["E" + incRowExcel + ":H" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            incRowExcel = incRowExcel + 2;


                                            //Row A = 2
                                            int RowA = incRowExcel;

                                            worksheet.Cells[incRowExcel, 1].Value = "Fund Name ";
                                            worksheet.Cells["A" + incRowExcel + ":B" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 3].Value = ":";
                                            worksheet.Cells[incRowExcel, 4].Value = rsHeader.Key.FundNameFrom;
                                            worksheet.Cells[incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            worksheet.Cells[incRowExcel, 4].Style.Font.Bold = true;
                                            worksheet.Cells["D" + incRowExcel + ":F" + incRowExcel].Merge = true;

                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Value = "NAV Date ";
                                            worksheet.Cells["A" + incRowExcel + ":B" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 3].Value = ": ";
                                            worksheet.Cells[incRowExcel, 4].Value = rsHeader.Key.NAVDate;
                                            worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "dd-MMMM-yyyy";
                                            worksheet.Cells[incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            worksheet.Cells["D" + incRowExcel + ":D" + incRowExcel].Merge = true;

                                            incRowExcel++;


                                            incRowExcel = incRowExcel + 1;

                                            //Row B = 3
                                            int RowB = incRowExcel;
                                            int RowG = incRowExcel + 1;

                                            worksheet.Row(RowB).Height = 100;
                                            worksheet.Row(RowG).Height = 100;

                                            worksheet.Cells[incRowExcel, 1].Value = "NO";
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells["A" + RowB + ":A" + RowG].Merge = true;
                                            worksheet.Cells["A" + RowB + ":A" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["A" + RowB + ":A" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 2].Value = "CUSTOMER ID";
                                            worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                            worksheet.Cells["B" + RowB + ":B" + RowG].Merge = true;
                                            worksheet.Cells["B" + RowB + ":B" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["B" + RowB + ":B" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 3].Value = "NAME";
                                            worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;
                                            worksheet.Cells["C" + RowB + ":D" + RowG].Merge = true;
                                            worksheet.Cells["C" + RowB + ":D" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["C" + RowB + ":D" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 5].Value = "SWITCH FROM";
                                            worksheet.Cells[incRowExcel, 5].Style.Font.Bold = true;
                                            worksheet.Cells["E" + RowB + ":K" + RowB].Merge = true;
                                            worksheet.Cells["E" + RowB + ":J" + RowB].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["E" + RowB + ":J" + RowB].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[RowG, 5].Value = "FUND";
                                            worksheet.Cells[RowG, 5].Style.Font.Bold = true;
                                            worksheet.Cells["E" + RowG + ":E" + RowG].Merge = true;
                                            worksheet.Cells["E" + RowG + ":E" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["E" + RowG + ":E" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[RowG, 6].Value = "NO. OF UNITS";
                                            worksheet.Cells[RowG, 6].Style.Font.Bold = true;
                                            worksheet.Cells["F" + RowG + ":F" + RowG].Merge = true;
                                            worksheet.Cells["F" + RowG + ":F" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["F" + RowG + ":F" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[RowG, 7].Value = "NOMINAL AMOUNT";
                                            worksheet.Cells[RowG, 7].Style.Font.Bold = true;
                                            worksheet.Cells["G" + RowG + ":G" + RowG].Merge = true;
                                            worksheet.Cells["G" + RowG + ":G" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["G" + RowG + ":G" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[RowG, 8].Value = "% FEE";
                                            worksheet.Cells[RowG, 8].Style.Font.Bold = true;
                                            worksheet.Cells["H" + RowG + ":H" + RowG].Merge = true;
                                            worksheet.Cells["H" + RowG + ":H" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["H" + RowG + ":H" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[RowG, 9].Value = "AMOUNT FEE";
                                            worksheet.Cells[RowG, 9].Style.Font.Bold = true;
                                            worksheet.Cells["I" + RowG + ":I" + RowG].Merge = true;
                                            worksheet.Cells["I" + RowG + ":I" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["I" + RowG + ":I" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[RowG, 10].Value = "NET AMOUNT";
                                            worksheet.Cells[RowG, 10].Style.Font.Bold = true;
                                            worksheet.Cells["J" + RowG + ":K" + RowG].Merge = true;
                                            worksheet.Cells["J" + RowG + ":K" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["J" + RowG + ":K" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 12].Value = "SWITCH TO";
                                            worksheet.Cells[incRowExcel, 12].Style.Font.Bold = true;
                                            worksheet.Cells["L" + RowB + ":M" + RowB].Merge = true;
                                            worksheet.Cells["L" + RowB + ":L" + RowB].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["L" + RowB + ":L" + RowB].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[RowG, 12].Value = "FUND";
                                            worksheet.Cells[RowG, 12].Style.Font.Bold = true;
                                            worksheet.Cells["L" + RowG + ":L" + RowG].Merge = true;
                                            worksheet.Cells["L" + RowG + ":L" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["L" + RowG + ":L" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[RowG, 13].Value = "NET AMOUNT";
                                            worksheet.Cells[RowG, 13].Style.Font.Bold = true;
                                            worksheet.Cells["M" + RowG + ":M" + RowG].Merge = true;
                                            worksheet.Cells["M" + RowG + ":M" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["M" + RowG + ":M" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 14].Value = "REMARKS";
                                            worksheet.Cells[incRowExcel, 14].Style.Font.Bold = true;
                                            worksheet.Cells["N" + RowB + ":N" + RowG].Merge = true;
                                            worksheet.Cells["N" + RowB + ":N" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["N" + RowB + ":N" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;



                                            incRowExcel++;

                                            // Row C = 4
                                            int RowC = incRowExcel;

                                            incRowExcel++;
                                            //area header

                                            int _no = 1;
                                            int _startRowDetail = incRowExcel;
                                            int _endRowDetail = 0;
                                            foreach (var rsDetail in rsHeader)
                                            {
                                                //Row D = 5
                                                int RowD = incRowExcel;
                                                int RowE = incRowExcel + 1;
                                                int RowH = RowB + 2;
                                                int RowI = RowB + 3;


                                                //ThickBox Border

                                                worksheet.Cells["A" + RowB + ":N" + RowG].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + RowB + ":N" + RowG].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + RowB + ":N" + RowG].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + RowB + ":N" + RowG].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + incRowExcel + ":N" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + incRowExcel + ":N" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Medium;


                                                //area detail
                                                worksheet.Cells[incRowExcel, 1].Value = _no;
                                                worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 2].Value = rsDetail.ClientID;
                                                worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.ClientName;
                                                worksheet.Cells[incRowExcel, 5].Value = rsDetail.FundIDFrom;
                                                worksheet.Cells[incRowExcel, 6].Value = rsDetail.TotalUnitFundFrom;
                                                worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.0000";
                                                worksheet.Cells[incRowExcel, 7].Value = rsDetail.CashFundFrom;
                                                worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 8].Value = rsDetail.FeePercent;
                                                worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 9].Value = rsDetail.FeeAmount;
                                                worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells["J" + incRowExcel + ":K" + incRowExcel].Merge = true;
                                                worksheet.Cells[incRowExcel, 11].Value = rsDetail.TotalCashFundFrom;
                                                worksheet.Cells[incRowExcel, 11].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 12].Value = rsDetail.FundIDTo;
                                                worksheet.Cells[incRowExcel, 13].Value = rsDetail.TotalCashFundTo;
                                                worksheet.Cells[incRowExcel, 13].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 14].Value = rsDetail.Remark;
                                                _endRowDetail = incRowExcel;
                                                _no++;
                                                incRowExcel++;

                                            }


                                            worksheet.Cells["A" + _endRowDetail + ":N" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells[incRowExcel, 4].Value = "Total :";
                                            worksheet.Cells[incRowExcel, 6].Formula = "SUM(F" + _startRowDetail + ":F" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 7].Formula = "SUM(G" + _startRowDetail + ":G" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 9].Formula = "SUM(I" + _startRowDetail + ":I" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 11].Formula = "SUM(K" + _startRowDetail + ":K" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 13].Formula = "SUM(M" + _startRowDetail + ":M" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 6].Calculate();
                                            worksheet.Cells[incRowExcel, 7].Calculate();
                                            worksheet.Cells[incRowExcel, 9].Calculate();
                                            worksheet.Cells[incRowExcel, 11].Calculate();
                                            worksheet.Cells[incRowExcel, 13].Calculate();
                                            worksheet.Cells[incRowExcel, 6].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 7].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 9].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 11].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 13].Style.Font.Bold = true;

                                            worksheet.Cells[incRowExcel, 6].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells[incRowExcel, 6].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells[incRowExcel, 6].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells[incRowExcel, 6].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells[incRowExcel, 7].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells[incRowExcel, 7].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells[incRowExcel, 7].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells[incRowExcel, 7].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells[incRowExcel, 8].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells[incRowExcel, 9].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells[incRowExcel, 9].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells[incRowExcel, 9].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells[incRowExcel, 9].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells[incRowExcel, 10].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells[incRowExcel, 11].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells[incRowExcel, 11].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells[incRowExcel, 11].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells[incRowExcel, 12].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells[incRowExcel, 13].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells[incRowExcel, 13].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells[incRowExcel, 13].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells[incRowExcel, 13].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells[incRowExcel, 14].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells[incRowExcel, 14].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;


                                            incRowExcel++;

                                            incRowExcel = incRowExcel + 4;

                                            worksheet.Cells[incRowExcel, 1].Value = "Disclaimer : ";
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Merge = true;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Size = 12;

                                            incRowExcel++;

                                            worksheet.Cells[incRowExcel, 1].Value = "Dokumen ini dipersiapkan oleh PT SUCOR SEKURITAS dan hanya bisa digunakan untuk kepentingan investor tersebut diatas dan tidak untuk pihak lainnya. Laporan ini bukan merupakan konfirmasi dari PT SUCOR SEKURITAS dan tidak untuk menggantikan laporan yang wajib diterbitkan oleh Bank Kustodian, jika ada perbedaan antara laporan ini dengan laporan Bank Kustodian, maka laporan Bank Kustodian adalah yang benar. Laporan ini diproses oleh computer dan tidak memerlukan tandatangan.";
                                            worksheet.Cells[incRowExcel, 1, incRowExcel + 2, 6].Style.Font.Size = 11;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel + 2, 6].Merge = true;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel + 2, 6].Style.WrapText = true;

                                            incRowExcel = incRowExcel + 4;

                                            worksheet.Cells[incRowExcel, 1].Value = "PT SUCOR SEKURITAS terdaftar dan diawasi oleh OJK, dengan nomor registrasi KEP-01/PM/MI/1999";
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 6].Style.Font.Size = 11;
                                            worksheet.Cells[incRowExcel, 1, incRowExcel, 6].Merge = true;

                                            incRowExcel = incRowExcel + 2;

                                            worksheet.Row(incRowExcel).PageBreak = _unitRegistryRpt.PageBreak;

                                        }
                                        //incRowExcel++;
                                        int _lastRow = incRowExcel;

                                        string _rangeA = "A5:N" + incRowExcel;
                                        using (ExcelRange r = worksheet.Cells[_rangeA]) // KALO  KOLOM 1 SAMPE 9 A-I
                                        {
                                            r.Style.Font.Size = 50;
                                        }

                                        worksheet.DeleteRow(_lastRow);


                                        worksheet.PrinterSettings.FitToPage = true;
                                        worksheet.PrinterSettings.FitToWidth = 1;
                                        worksheet.PrinterSettings.FitToHeight = 0;
                                        worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel - 1, 15];
                                        worksheet.Column(1).Width = 25;
                                        worksheet.Column(2).Width = 75;
                                        worksheet.Column(3).Width = 25;
                                        worksheet.Column(4).Width = 80;
                                        worksheet.Column(5).Width = 30;
                                        worksheet.Column(6).Width = 70;
                                        worksheet.Column(7).Width = 90;
                                        worksheet.Column(8).Width = 45;
                                        worksheet.Column(9).Width = 65;
                                        worksheet.Column(10).Width = 5;
                                        worksheet.Column(11).Width = 65;
                                        worksheet.Column(12).Width = 30;
                                        worksheet.Column(13).Width = 65;
                                        worksheet.Column(14).Width = 45;


                                        // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                        //worksheet.Cells["A3:I14"].AutoFitColumns(); // UNTUK AUTO FIT UKURAN TULISAN DENGAN KOLOM, KEBETULAN INI BUAT HEADER AJA DIPAKENYA
                                        //worksheetApproved.Cells["A3:E3"].AutoFitColumns();
                                        worksheet.PrinterSettings.Orientation = eOrientation.Landscape; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                        worksheet.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN

                                        //worksheet.HeaderFooter.OddHeader.CenteredText = "\n \n \n \n \n \n \n &30&B REDEMPTION \n &28&B Batch Form";



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

            #region Daily Report Total AUM Reksa Dana
            else if (_unitRegistryRpt.ReportName.Equals("Daily Report Total AUM Reksa Dana"))   // SAM
            {
                try
                {
                    using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                    {
                        DbCon.Open();
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            string _paramFund = "";

                            if (!_host.findString(_unitRegistryRpt.FundFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_unitRegistryRpt.FundFrom))
                            {
                                _paramFund = "And A.FundPK in ( " + _unitRegistryRpt.FundFrom + " ) ";
                            }
                            else
                            {
                                _paramFund = "";
                            }

                            cmd.CommandText = @"
                         DECLARE @cols AS NVARCHAR(MAX),@colsForQuery AS NVARCHAR(MAX),
                            @query  AS NVARCHAR(MAX)

                        select @colsForQuery = STUFF((SELECT ',isnull(' + QUOTENAME(NAME) +',0) ' + QUOTENAME(NAME) 
                                            from Fund
					                        where status = 2
					                        Order by FundPK
                                    FOR XML PATH(''), TYPE
                                    ).value('.', 'NVARCHAR(MAX)') 
                                ,1,1,'')

                        select @cols = STUFF((SELECT ',' + QUOTENAME(NAME) 
                                            from Fund
					                        where status = 2
					                        Order by FundPK
                                    FOR XML PATH(''), TYPE
                                    ).value('.', 'NVARCHAR(MAX)') 
                                ,1,1,'')

                        set @query = 'SELECT Date,' + @colsForQuery + ' from 
                                     (
                                        Select B.Name,A.Date,isnull(A.NAV,0) * ([dbo].[FGetTotalUnitByFundPK](dbo.fworkingday(A.date,-1), A.FundPK)) AUM from CloseNAV A 
				                        left join Fund B on A.FundPK = B.FundPK and B.status = 2
				                        where A.status = 2 and A.Date between ''' + @ValueDateFrom + ''' and ''' + @ValueDateTo + ''' 
                                    ) x
                                    pivot 
                                    (
                                        AVG(AUM)
                                        for Name in (' + @cols + ')
                                    ) p 
			                        '

                        Exec(@query)";
                            cmd.CommandTimeout = 0;
                            cmd.Parameters.AddWithValue("@FundFrom", _unitRegistryRpt.FundFrom);
                            cmd.Parameters.AddWithValue("@ValueDateFrom", _unitRegistryRpt.ValueDateFrom);
                            cmd.Parameters.AddWithValue("@ValueDateTo", _unitRegistryRpt.ValueDateTo);
                            using (SqlDataReader dr0 = cmd.ExecuteReader())
                            {
                                if (!dr0.HasRows)
                                {
                                    return false;
                                }
                                else
                                {
                                    string filePath = Tools.ReportsPath + "DailyReportTotalAUMReksaDana" + "_" + _userID + ".xlsx";
                                    string pdfPath = Tools.ReportsPath + "DailyReportTotalAUMReksaDana" + "_" + _userID + ".pdf";
                                    FileInfo excelFile = new FileInfo(filePath);
                                    if (excelFile.Exists)
                                    {
                                        excelFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                        excelFile = new FileInfo(filePath);
                                    }

                                    // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                    using (ExcelPackage package = new ExcelPackage(excelFile))
                                    {
                                        package.Workbook.Properties.Title = "DailyReportTotalAUMReksaDana_";
                                        package.Workbook.Properties.Author = Tools.DefaultReportAuthor();
                                        package.Workbook.Properties.Comments = Tools.DefaultReportComments();
                                        package.Workbook.Properties.Company = Tools.DefaultReportCompany();
                                        package.Workbook.Properties.SetCustomPropertyValue("Checked by", _userID);
                                        package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", Tools.DefaultReportAssemblyName());

                                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("DailyReportTotalAUMReksaDana_");



                                        int incRowExcel = 2;
                                        int _startRowDetail = incRowExcel;
                                        int _rowCtrp = 0;
                                        int _endRowDetail = 0;
                                        int _endColDetail = 0;

                                        while (dr0.Read())
                                        {
                                            int incColExcel = 1;
                                            for (int inc1 = 0; inc1 < dr0.FieldCount; inc1++)
                                            {
                                                worksheet.Cells[1, incColExcel].Value = dr0.GetName(inc1);
                                                worksheet.Cells[1, incColExcel].Style.WrapText = true;
                                                worksheet.Cells[1, incColExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[1, incColExcel].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                //incColExcel++;
                                                //worksheet.Cells[2, incColExcel].Value = dr0.GetName(inc1);
                                                //worksheet.Cells[2, incColExcel].Style.WrapText = true;
                                                //worksheet.Cells[2, incColExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                //worksheet.Cells[2, incColExcel].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells[incRowExcel, incColExcel].Style.Font.Size = Tools.DefaultReportFontSize();
                                                if (incColExcel == 1)
                                                {
                                                    //ini untuk nambah kolom excel
                                                    worksheet.Cells[incRowExcel, incColExcel].Value = Convert.ToDateTime(dr0.GetValue(inc1));
                                                    worksheet.Cells["A" + incRowExcel].Style.Numberformat.Format = "dd/MMM/yyyy";

                                                    worksheet.Column(incColExcel).Width = 15;
                                                }
                                                else if (incColExcel >= 2)
                                                {
                                                    _rowCtrp = incColExcel;
                                                    worksheet.Cells[incRowExcel, incColExcel].Value = dr0.GetValue(inc1);
                                                    worksheet.Cells[incRowExcel, incColExcel].Style.Numberformat.Format = "#,##0.0000";
                                                    worksheet.Column(incColExcel).Width = 20;
                                                    //worksheet.Cells[15, _rowCtrp].Formula = "SUM(" + _host.GetAlphabet(_rowCtrp) + _startRowDetail + ":" + _host.GetAlphabet(_rowCtrp) + "14)";
                                                    //worksheet.Cells[15, _rowCtrp].Style.Numberformat.Format = "#,##0";
                                                    //worksheet.Cells[16, _rowCtrp].Formula = "SUM(" + _host.GetAlphabet(_rowCtrp) + _startRowDetail + ":" + _host.GetAlphabet(_rowCtrp) + "14) * 100 /" + _totalAMount;
                                                    //worksheet.Cells[16, _rowCtrp].Style.Numberformat.Format = "#,##0";
                                                }
                                                else
                                                {
                                                    worksheet.Cells[incRowExcel, incColExcel].Value = dr0.GetValue(inc1).ToString();
                                                }



                                                worksheet.Cells[1, incColExcel].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                                worksheet.Cells[1, incColExcel].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(251, 228, 213));
                                                worksheet.Cells[1, incColExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells[1, incColExcel].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells[1, incColExcel].Style.Border.Top.Style = ExcelBorderStyle.Double;
                                                worksheet.Cells[1, incColExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Double;

                                                worksheet.Cells[incRowExcel, incColExcel].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells[incRowExcel, incColExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                                                worksheet.Cells[incRowExcel, incColExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells[incRowExcel, incColExcel].Style.Border.Right.Style = ExcelBorderStyle.Thin;


                                                incColExcel++;
                                                //incColExcel = incColExcel - 1;
                                                //worksheet.Cells[incRowExcel, incColExcel].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                                            }
                                            _endColDetail = incColExcel - 1;
                                            _endRowDetail = incRowExcel - 1;
                                            //worksheet.Cells[incRowExcel, incColExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                            //worksheet.Cells[incRowExcel, incColExcel].Formula = "SUM(D" + incRowExcel + ":" + _host.GetAlphabet(_endColDetail) + incRowExcel + ")";
                                            //worksheet.Cells[incRowExcel, incColExcel].Style.Numberformat.Format = "#,##0";
                                            worksheet.Column(incColExcel).Width = 20;

                                            //worksheet.Cells[15, incColExcel].Value = _totalAMount;
                                            //worksheet.Cells[15, incColExcel].Style.Numberformat.Format = "#,##0.0000";




                                            incRowExcel++;


                                        }
                                        //worksheet.Cells[15, 3].Value = "TOTAL : ";

                                        incRowExcel = incRowExcel + 4;

                                        worksheet.Cells[incRowExcel, 1].Value = "Disclaimer : ";
                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Merge = true;
                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Size = 12;

                                        incRowExcel++;

                                        worksheet.Cells[incRowExcel, 1].Value = "Dokumen ini dipersiapkan oleh PT SUCOR SEKURITAS dan hanya bisa digunakan untuk kepentingan investor tersebut diatas dan tidak untuk pihak lainnya. Laporan ini bukan merupakan konfirmasi dari PT SUCOR SEKURITAS dan tidak untuk menggantikan laporan yang wajib diterbitkan oleh Bank Kustodian, jika ada perbedaan antara laporan ini dengan laporan Bank Kustodian, maka laporan Bank Kustodian adalah yang benar. Laporan ini diproses oleh computer dan tidak memerlukan tandatangan.";
                                        worksheet.Cells[incRowExcel, 1, incRowExcel + 2, 5].Style.Font.Size = 11;
                                        worksheet.Cells[incRowExcel, 1, incRowExcel + 2, 5].Merge = true;
                                        worksheet.Cells[incRowExcel, 1, incRowExcel + 2, 5].Style.WrapText = true;

                                        incRowExcel = incRowExcel + 4;

                                        worksheet.Cells[incRowExcel, 1].Value = "PT SUCOR SEKURITAS terdaftar dan diawasi oleh OJK, dengan nomor registrasi KEP-01/PM/MI/1999";
                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Style.Font.Size = 11;
                                        worksheet.Cells[incRowExcel, 1, incRowExcel, 5].Merge = true;

                                        incRowExcel = incRowExcel + 2;


                                        // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                        //worksheet.Cells["A1:Z1"].AutoFitColumns(); // UNTUK AUTO FIT UKURAN TULISAN DENGAN KOLOM, KEBETULAN INI BUAT HEADER AJA DIPAKENYA
                                        //worksheet.Cells["K2:Z2"].AutoFitColumns(); // CEK DARI ENTRY ID SAMPE LAST UPDATE
                                        worksheet.PrinterSettings.Orientation = eOrientation.Landscape; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                        worksheet.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN
                                        worksheet.HeaderFooter.OddHeader.CenteredText = "&14 LAPORAN TOTAL AUM REKSA DANA (PUBLIC FUND) \n " + "&12 REKSA DANA " + _host.Get_CompanyName();

                                        // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                        worksheet.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
                                        worksheet.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
                                        worksheet.View.PageLayoutView = Tools.DefaultReportPageLayoutView();
                                        worksheet.View.ShowGridLines = Tools.DefaultReportShowGridLines();
                                        //worksheet.HeaderFooter.OddHeader.CenteredText = Tools.DefaultReportHeaderCenterText();
                                        //worksheet.HeaderFooter.OddHeader.LeftAlignedText = Tools.DefaultReportHeaderLeftText();
                                        //Image img = Image.FromFile(Tools.ReportImage);
                                        //worksheet.HeaderFooter.OddHeader.InsertPicture(img, PictureAlignment.Left);

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
            //KELUAR



            else
            {
                return false;
            }
        }


        public List<FundClientCombo> GetBankRecipientCombo_ByFundClientPK(int _fundClientPK)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<FundClientCombo> L_FundClient = new List<FundClientCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"        
                        select C.BankRecipientPK BankRecipientPK,C.Bank +  ' - ' + C.B as AccountNo from (  
                        select 1 BankRecipientPK,B.Name Bank,RDNAccountNo B from fundclient FC   
                        left join Bank B on FC.BankRDNPK = B.BankPK where fundclientpk = @FundClientPK and FC.status  = 2 and B.Status = 2      
                        )C 
                        UNION ALL

                        Select NoBank BankRecipientPK, B.Name + ' - ' + A.AccountNo from FundClientBankList A
                        left join Bank B on A.BankPK = B.BankPK and B.status in (1,2)
                        where fundClientPK = @FundClientPK and A.status = 2


                        ";
                        cmd.Parameters.AddWithValue("@FundClientPK", _fundClientPK);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    FundClientCombo M_FundClient = new FundClientCombo();
                                    M_FundClient.BankRecipientPK = Convert.ToInt32(dr["BankRecipientPK"]);
                                    M_FundClient.AccountNo = Convert.ToString(dr["AccountNo"]);
                                    L_FundClient.Add(M_FundClient);
                                }

                            }
                            return L_FundClient;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }


        }



        public string SInvestSwitchingRpt_BySelectedData(string _userID, DateTime _dateFrom, DateTime _dateTo, ParamUnitRegistryBySelected _paramUnitRegistryBySelected)
        {

            try
            {

                string paramClientSwitchingSelected = "";
                if (!_host.findString(_paramUnitRegistryBySelected.UnitRegistrySelected.ToLower(), "0", ",") && !string.IsNullOrEmpty(_paramUnitRegistryBySelected.UnitRegistrySelected))
                {
                    paramClientSwitchingSelected = " And ClientSwitchingPK in (" + _paramUnitRegistryBySelected.UnitRegistrySelected + ") ";
                }
                else
                {
                    paramClientSwitchingSelected = " And ClientSwitchingPK in (0) ";
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
                                    select ''   
                                    insert into #Text         
                                    Select  RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(CONVERT(VARCHAR(10), ValueDate, 112),''))))
                                    + '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(A.Type,''))))
                                    + '|' + @CompanyID
                                    + '|' + isnull(A.IFUACode,'')
                                    + '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(A.FundFromSinvest,''))))
                                    + '|' + case when A.CashAmount = 0 then '' else cast(isnull(cast(A.CashAmount as decimal(22,2)),'')as nvarchar) end
                                    + '|' + case when A.UnitAmount = 0 then '' else cast(isnull(cast(A.UnitAmount as decimal(22,4)),'') as nvarchar) end
                                    + '|' + case when A.BitSwitchingAll = 1 then 'Y' else '' end
                                    + '|' + 
                                    + '|' + 
                                    + '|' + 
                                    + '|' + case when A.FeePercent = 0 then '' else cast(isnull(cast(A.FeePercent as decimal(10,2)),'')as nvarchar) end
                                    + '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(A.FundToSinvest,''))))
                                    + '|' + ''        
                                    + '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(A.TransferType,''))))
                                    + '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(A.Reference,'')))) 

                                    from (   
  
                                    Select CW.FundPKFrom,CW.FundPKTo,CW.ValueDate,F.SInvestCode FundFromSinvest, F1.SInvestCode FundToSinvest,CW.PaymentDate SettlementDate,CW.SwitchingFeePercent FeePercent,CW.SwitchingFeeAmount FeeAmount,'3' Type,ROUND(CashAmount,2)CashAmount ,ROUND(UnitAmount,4)UnitAmount ,CW.BitSwitchingAll BitSwitchingAll,TransferType TransferType
                                    ,Case When ReferenceSInvest <> '' and ReferenceSInvest is not null then ReferenceSInvest else cast(ClientSwitchingPK as nvarchar) end Reference,FC.IFUACode
                                    from ClientSwitching CW 
                                    left join Fund F on CW.FundPKFrom = F.fundPK and f.Status=2 
                                    left join fund F1 on CW.FundPKTo = F1.FundPK and F1.status = 2 
                                    left join FundClient FC on CW.FundClientPK = FC.FundClientPK and fc.Status=2      
                                    where ValueDate =   @ValueDate and " + paramClientSwitchingSelected + @" and Cw.status = 2
                                    )A    
                                    Group by A.FundPKFrom,A.FundPKTo,A.ValueDate,A.FundFromSinvest,A.FundToSinvest,A.FeePercent,A.SettlementDate,A.FeeAmount,A.Type,A.CashAmount,A.UnitAmount,A.BitSwitchingAll,A.TransferType,A.Reference,A.IFUACode
                                    order by A.ValueDate Asc
                                    select * from #text          
                                    END ";
                        cmd.Parameters.AddWithValue("@CompanyID", _host.Get_CompanyID());
                        cmd.Parameters.AddWithValue("@ValueDate", _dateFrom);



                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                string filePath = Tools.SInvestTextPath + "SUBS_REDM_SWTC_Order_Upload_SUBS_Order.txt";
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
                                    return Tools.HtmlSinvestTextPath + "SUBS_REDM_SWTC_Order_Upload_SUBS_Order.txt";
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

    }
}