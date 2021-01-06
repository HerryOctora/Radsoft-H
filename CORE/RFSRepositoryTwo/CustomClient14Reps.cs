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
    public class CustomClient14Reps
    {
        Host _host = new Host();

        private class RebalancingDaily
        {
            public string InstrumentCode { get; set; }
            public decimal Balance { get; set; }
            public decimal AvgPrice { get; set; }
            public decimal MarketValue { get; set; }
            public decimal ClosePrice { get; set; }
            public decimal CostValue { get; set; }
            public decimal Berat { get; set; }
            public decimal BeratMin { get; set; }
            public decimal BeratMax { get; set; }
            public decimal Pos1 { get; set; }
            public bool Pos2 { get; set; }
            public bool Pos3 { get; set; }

        }

        private class CompareIncomeStatementRpt
        {

            public decimal Value { get; set; }
            public decimal ValueLastYear { get; set; }
            public int Col { get; set; }
            public int Row { get; set; }

        } 
        
        private class CounterFundUnder10BioRpt
        {
            
            public string FundID { get; set; }
            public string FundName { get; set; }
            public string InstrumentID { get; set; }
            public decimal AmountDeposito { get; set; }
            public decimal NAVDeposito { get; set; }
            public decimal AmountBond { get; set; }
            public decimal NavBond { get; set; }
            public decimal AmountEquity { get; set; }
            public decimal NAVEquity { get; set; }
            public decimal TotalInvestment { get; set; }
            public decimal Amount { get; set; }
            public decimal NAV { get; set; }
            public decimal DaysPelanggaranAktif { get; set; }
            public decimal DaysPelanggaranPasif { get; set; }
            public decimal TotalAUM { get; set; }
            public decimal Under10Bio { get; set; }

        }


        private class WeeklySalesReport
        {
            public string Type { get; set; }
            public string Code { get; set; }
            public string AgentName { get; set; }
            public DateTime JoinDate { get; set; }
            public decimal TotalNewClientThisWeek { get; set; }
            public decimal TotalAllClient { get; set; }
            public decimal TotalSubs { get; set; }
            public decimal TotalRed { get; set; }
            public decimal AUM { get; set; }
            public decimal TotalTopUp { get; set; }
            public decimal AmountTopUp { get; set; }
            public string Remark { get; set; }
        } 

        private class WeeklySalesReportINDIRECT
        {
            public string Type { get; set; }
            public string Code { get; set; }
            public string AgentName { get; set; }
            public DateTime JoinDate { get; set; }
            public decimal TotalNewClientThisWeek { get; set; }
            public decimal TotalAllClient { get; set; }
            public decimal TotalSubs { get; set; }
            public decimal TotalRed { get; set; }
            public decimal AUM { get; set; }
            public decimal TotalTopUp { get; set; }
            public decimal AmountTopUp { get; set; }
            public string Remark { get; set; }
        } 

        public Boolean GenerateReportUnitRegistry(string _userID, UnitRegistryRpt _unitRegistryRpt)
        {
            #region Daily Transaction Report For All
            if (_unitRegistryRpt.ReportName.Equals("Daily Transaction Report For All"))
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
                            string _paramAgent = "";
                            string _paramFundClient = "";
                            string _paramDepartment = "";
                            string _paramAgentSwitching = "";
                            string _paramDepartmentSwitching = "";

                            if (!_host.findString(_unitRegistryRpt.FundFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_unitRegistryRpt.FundFrom))
                            {
                                _paramFund = "And F.FundPK in ( " + _unitRegistryRpt.FundFrom + " ) ";
                            }
                            else
                            {
                                _paramFund = "";
                            }
                            if (!_host.findString(_unitRegistryRpt.AgentFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_unitRegistryRpt.AgentFrom))
                            {
                                _paramAgent = "And A.AgentPK in ( " + _unitRegistryRpt.AgentFrom + " ) ";
                                _paramAgentSwitching = "And AG.AgentPK in ( " + _unitRegistryRpt.AgentFrom + " ) ";
                            }
                            else
                            {
                                _paramAgent = "";
                                _paramAgentSwitching = "";
                            }
                            if (!_host.findString(_unitRegistryRpt.FundClientFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_unitRegistryRpt.FundClientFrom))
                            {
                                _paramFundClient = "And A.FundClientPK in ( " + _unitRegistryRpt.FundClientFrom + " ) ";
                            }
                            else
                            {
                                _paramFundClient = "";
                            }
                            if (!_host.findString(_unitRegistryRpt.DepartmentFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_unitRegistryRpt.DepartmentFrom))
                            {
                                _paramDepartment = "And A.DepartmentPK in ( " + _unitRegistryRpt.DepartmentFrom + " ) ";
                                _paramDepartmentSwitching = "And AG.DepartmentPK in ( " + _unitRegistryRpt.DepartmentFrom + " ) ";
                            }
                            else
                            {
                                _paramDepartment = "";
                                _paramDepartmentSwitching = "";
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
                                _statusSubs = "  (A.Status = 2 or A.Posted = 1) and A.Revised = 0  ";
                                _statusRedemp = "  (A.Status = 2 or A.Posted = 1) and A.Revised = 0  ";
                                _statusSwitch = "  (A.Status = 2 or A.Posted = 1) and A.Revised = 0  ";
                            }
                            else if (_unitRegistryRpt.Status == 7)
                            {
                                _statusSubs = "  (A.Status = 1 Or A.Status = 2 or A.Posted = 1) and  A.Revised = 0  ";
                                _statusRedemp = "  (A.Status = 1 Or A.Status = 2 or A.Posted = 1) and A.Revised = 0  ";
                                _statusSwitch = "  (A.Status = 1 Or A.Status = 2 or A.Posted = 1) and A.Revised = 0  ";
                            }
                            //                          
                            cmd.CommandText =
                                @"
                                --declare @ValueDateFrom datetime
                                --declare @valuedateTo datetime
                                --set @ValueDateFrom = '01/01/18'
                                --set @valuedateTo = '10/31/18'

                                --DROP TABLE #A
                                --drop table #date 

                                CREATE TABLE #A
                                (
                                NoRow int,SettlementDate datetime,Remark nvarchar(100),FeeAmount numeric(19,2),FeePercent numeric(18,4),
                                DepartmentName nvarchar(100),AgentName nvarchar(100),FundID nvarchar(100),FundName nvarchar(100),
                                FundIDFrom nvarchar(100),FundNameFrom nvarchar(100),FundToID nvarchar(100),FundNameTo nvarchar(100),NAVDate datetime,
                                Type nvarchar(100),ClientID nvarchar(100),ClientName nvarchar(100),TotalCashAmount numeric(19,2),TotalUnitAmount numeric(18,4), 
                                TotalCashAmountFundFrom numeric(19,2),TotalCashAmountFundTo numeric(19,2), TotalUnitAmountFundFrom numeric(18,4),
                                TotalUnitAmountFundTo numeric(18,4),InvestorType nvarchar(100),NAV numeric(18,8),NAVFrom numeric(18,8),NAVTo numeric(18,8),
                                CashAmount numeric(19,2),FeeType nvarchar(100),FundPK int,FundClientPK int,NEW nvarchar(50)
                                )


                                create table #date 
                                (
                                valuedate datetime
                                )


                                declare @Date datetime,@FundClientPK int,@FirstDate datetime

                                insert into #A
                                Select A.NoRow,A.SettlementDate,A.Remark,A.FeeAmount, A.FeePercent,A.DepartmentName,A.AgentName,A.FundID,A.FundName , A.FundIDFrom, A.FundNameFrom,A.FundToID,A.FundNameTo,A.NAVDate,A.Type,A.ClientID,A.ClientName,
                                A.TotalCashAmount,A.TotalUnitAmount, A.TotalCashAmountFundFrom,A.TotalCashAmountFundTo, A.TotalUnitAmountFundFrom,A.TotalUnitAmountFundTo 
                                ,A.InvestorType,A.NAV, A.NAVFrom ,  A.NAVTo, A.CashAmount,A.FeeType,A.FundPK,A.FundClientPK,'NEW' NEW
                                from (  

                                Select '1' NoRow, A.NAVDate SettlementDate,A.Description Remark,A.SubscriptionFeeAmount FeeAmount,A.SubscriptionFeePercent FeePercent,D.Name DepartmentName,
                                AG.Name AgentName,F.ID FundID,F.Name FundName,'' FundIDFrom,''FundToID ,'' FundNameFrom, '' FundNameTo,NAVDate,'Subscription' Type, Fc.ID ClientID,FC.Name ClientName, TotalCashAmount, TotalUnitAmount ,
                                0 TotalCashAmountFundFrom,0 TotalCashAmountFundTo, 0 TotalUnitAmountFundFrom,0 TotalUnitAmountFundTo  ,
                                Case when FC.InvestorType = 1 then 'Individual' else 'Institusi' end InvestorType, A.NAV NAV, 0 NAVFrom , 0 NAVTo, CashAmount, '' FeeType,A.FundPK,A.FundClientPK
                                from ClientSubscription A 
                                left join Fund F on A.FundPK = F.fundPK and f.Status in (1,2)   
                                left join FundClient FC on A.FundClientPK = FC.FundClientPK and fc.Status in (1,2)   
                                left join Agent AG on A.AgentPK = AG.AgentPK and AG.Status in (1,2)  
                                left join Department D on D.DepartmentPK = AG.DepartmentPK and D.Status in (1,2)
                                where 
                                NAVDate Between @ValueDateFrom and @ValueDateTo  and A.Posted = 1 and A.Revised = 0  
                                " + _paramFund + _paramFundClient + _paramDepartment + _paramAgent + @"
                                UNION ALL   
                                Select '2' NoRow,A.PaymentDate SettlementDate,A.Description Remark,A.RedemptionFeeAmount FeeAmount,A.RedemptionFeePercent FeePercent,D.Name DepartmentName,
                                AG.Name AgentName,F.ID FundID,F.Name FundName,'' FundIDFrom,''FundToID ,'' FundNameFrom, '' FundNameTo,NAVDate,'Redemption' Type, Fc.ID ClientID,FC.Name ClientName, TotalCashAmount, TotalUnitAmount  ,
                                0 TotalCashAmountFundFrom,0 TotalCashAmountFundTo, 0 TotalUnitAmountFundFrom,0 TotalUnitAmountFundTo  ,
                                Case when FC.InvestorType = 1 then 'Individual' else 'Institusi' end InvestorType, A.NAV NAV, 0 NAVFrom , 0 NAVTo, CashAmount, '' FeeType,A.FundPK,A.FundClientPK
                                from ClientRedemption A 
                                left join Fund F on A.FundPK = F.fundPK and f.Status in (1,2)   
                                left join FundClient FC on A.FundClientPK = FC.FundClientPK and fc.Status in (1,2)    
                                left join Agent AG on A.AgentPK = AG.AgentPK and AG.Status in (1,2) 
                                left join Department D on D.DepartmentPK = AG.DepartmentPK and D.Status in (1,2)  
                                where 
                                NAVDate Between @ValueDateFrom and @ValueDateTo   and A.Posted = 1 and A.Revised = 0 
                                " + _paramFund + _paramFundClient + _paramDepartment + _paramAgent + @" 
                                UNION ALL   
                                Select '3' NoRow,A.PaymentDate SettlementDate,A.Description Remark,A.SwitchingFeeAmount FeeAmount,A.SwitchingFeePercent FeePercent,D.Name DepartmentName,
                                AG.Name AgentName,'' FundID,'' FundName,F.ID FundIDfrom,G.ID FundToID ,F.Name FundNameFrom, G.Name FundNameTo,NAVDate,'Switching' Type, Fc.ID ClientID,FC.Name ClientName,
                                0 TotalCashAmount, 0 TotalUnitAmount, A.TotalCashAmountFundFrom,A.TotalCashAmountFundTo, A.TotalUnitAmountFundFrom,A.TotalUnitAmountFundTo  ,
                                Case when FC.InvestorType = 1 then 'Individual' else 'Institusi' end InvestorType, 0 NAV , A.NAVFundFrom NAVFrom , A.NAVFundTo NAVTo, CashAmount , FeeType,A.FundPKFrom FundPK,A.FundClientPK
                                from ClientSwitching A 
                                left join Fund F on A.FundPKFrom = F.fundPK and f.Status in (1,2)  
                                left join Fund G on A.FundPKTo = G.fundPK and G.Status in (1,2)   
                                left join FundClient FC on A.FundClientPK = FC.FundClientPK and fc.Status in (1,2)    
                                left join Agent AG on FC.SellingAgentPK = AG.AgentPK and AG.Status in (1,2) 
                                left join Department D on D.DepartmentPK = AG.DepartmentPK and D.Status in (1,2)  
                                where 
                                NAVDate Between @ValueDateFrom and @ValueDateTo   and A.Posted = 1 and A.Revised = 0 
                                " + _paramFund + _paramFundClient + _paramDepartmentSwitching + _paramAgentSwitching + @" 
                                )A
                                DECLARE A CURSOR FOR  
                                select FundClientPK,SettlementDate from #A

                                Open A
                                Fetch Next From A
                                Into @FundClientPK,@Date

                                While @@FETCH_STATUS = 0
                                BEGIN    
                                select TOP 1 @FirstDate =  ValueDate from ClientSubscription 
                                where FundClientPK = @FundClientPK and status = 2 and Posted = 1 and Revised = 0 order by ValueDate asc

                                IF (@Date > @FirstDate)
                                BEGIN
	                                update #A set new = 'TOP UP' where  SettlementDate =  @Date and FundClientPK = @FundClientPK
                                END

                                Fetch next From A Into @FundClientPK,@Date
                                END
                                Close A
                                Deallocate A                    

                                select * from #A 
                                order by SettlementDate,ClientID asc ";
                            cmd.CommandTimeout = 0;
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
                                    string filePath = Tools.ReportsPath + "DailyTransactionReportforAll" + "_" + _userID + ".xlsx";
                                    string pdfPath = Tools.ReportsPath + "DailyTransactionReportforAll" + "_" + _userID + ".pdf";
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

                                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Daily Transaction Report for All");


                                        //ATUR DATA GROUPINGNYA DULU
                                        List<UnitRegistryRpt> rList = new List<UnitRegistryRpt>();
                                        while (dr0.Read())
                                        {
                                            UnitRegistryRpt rSingle = new UnitRegistryRpt();
                                            rSingle.ClientID = Convert.ToString(dr0["ClientID"]);
                                            rSingle.FundID = dr0["FundID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["FundID"]);
                                            rSingle.FundName = dr0["FundName"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["FundName"]);
                                            rSingle.ClientName = Convert.ToString(dr0["ClientName"]);
                                            rSingle.AgentName = dr0["AgentName"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["AgentName"]);
                                            rSingle.DepartmentName = dr0["DepartmentName"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["DepartmentName"]);
                                            rSingle.NAVDate = Convert.ToDateTime(dr0["NAVDate"]);
                                            rSingle.SettlementDate = Convert.ToDateTime(dr0["SettlementDate"]);
                                            rSingle.FeeAmount = Convert.ToDecimal(dr0["FeeAmount"]);
                                            rSingle.Type = Convert.ToString(dr0["Type"]);
                                            rSingle.Remark = Convert.ToString(dr0["Remark"]);
                                            rSingle.CashBalance = Convert.ToDecimal(dr0["TotalCashAmount"]);
                                            rSingle.UnitBalance = Convert.ToDecimal(dr0["TotalUnitAmount"]);
                                            rSingle.Nav = Convert.ToDecimal(dr0["NAV"]);
                                            rSingle.InvestorType = Convert.ToString(dr0["InvestorType"]);

                                            rSingle.FundIDFrom = Convert.ToString(dr0["FundIDFrom"]);
                                            rSingle.FundNameFrom = Convert.ToString(dr0["FundNameFrom"]);
                                            rSingle.FundToID = Convert.ToString(dr0["FundToID"]);
                                            rSingle.FundNameTo = Convert.ToString(dr0["FundNameTo"]);
                                            rSingle.FeePercent = Convert.ToDecimal(dr0["FeePercent"]);
                                            rSingle.NAVFrom = Convert.ToDecimal(dr0["NAVFrom"]);
                                            rSingle.NAVTo = Convert.ToDecimal(dr0["NAVTo"]);
                                            rSingle.TotalCashAmountFundFrom = Convert.ToDecimal(dr0["TotalCashAmountFundFrom"]);
                                            rSingle.TotalCashAmountFundTo = Convert.ToDecimal(dr0["TotalCashAmountFundTo"]);
                                            rSingle.TotalUnitAmountFundFrom = Convert.ToDecimal(dr0["TotalUnitAmountFundFrom"]);
                                            rSingle.TotalUnitAmountFundTo = Convert.ToDecimal(dr0["TotalUnitAmountFundTo"]);
                                            rSingle.CashAmount = Convert.ToDecimal(dr0["CashAmount"]);
                                            rSingle.FeeType = dr0["FeeType"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["FeeType"]);
                                            rSingle.TopUp = Convert.ToString(dr0["NEW"]);

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
                                            worksheet.Cells[incRowExcel, 1].Value = "Daily Total Transaction Report For All";
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Size = 20;
                                            worksheet.Cells["A" + incRowExcel + ":N" + incRowExcel].Merge = true;
                                            worksheet.Cells["A" + incRowExcel + ":N" + incRowExcel].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Value = "Subscription , Redemption , Switching";
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Size = 15;
                                            worksheet.Cells["A" + incRowExcel + ":N" + incRowExcel].Merge = true;
                                            worksheet.Cells["A" + incRowExcel + ":N" + incRowExcel].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            incRowExcel = incRowExcel + 2;
                                            worksheet.Cells[incRowExcel, 1].Value = rsHeader.Key.Type;
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 2].Value = "Period";
                                            worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                            worksheet.Cells[incRowExcel, 3].Value = " : ";
                                            worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
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

                                            if (rsHeader.Key.Type == "Subscription")
                                            {
                                                worksheet.Cells[incRowExcel, 2].Value = "Trx Type";
                                                worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                                worksheet.Cells["B" + RowB + ":B" + RowG].Merge = true;
                                                worksheet.Cells["B" + RowB + ":B" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells["B" + RowB + ":B" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 3].Value = "Fund";
                                                worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;
                                                worksheet.Cells["C" + RowB + ":C" + RowG].Merge = true;
                                                worksheet.Cells["C" + RowB + ":C" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells["C" + RowB + ":C" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 4].Value = "Client Name";
                                                worksheet.Cells[incRowExcel, 4].Style.Font.Bold = true;
                                                worksheet.Cells["D" + RowB + ":D" + RowG].Merge = true;
                                                worksheet.Cells["D" + RowB + ":D" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells["D" + RowB + ":D" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 5].Value = "Invst. Type";
                                                worksheet.Cells[incRowExcel, 5].Style.Font.Bold = true;
                                                worksheet.Cells["E" + RowB + ":E" + RowG].Merge = true;
                                                worksheet.Cells["E" + RowB + ":E" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells["E" + RowB + ":E" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;


                                                worksheet.Cells[incRowExcel, 6].Value = "Sales Name";
                                                worksheet.Cells[incRowExcel, 6].Style.Font.Bold = true;
                                                worksheet.Cells["F" + RowB + ":F" + RowG].Merge = true;
                                                worksheet.Cells["F" + RowB + ":F" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells["F" + RowB + ":F" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 7].Value = "NAV Date";
                                                worksheet.Cells[incRowExcel, 7].Style.Font.Bold = true;
                                                worksheet.Cells["G" + RowB + ":G" + RowG].Merge = true;
                                                worksheet.Cells["G" + RowB + ":G" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells["G" + RowB + ":G" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 8].Value = "Fee %";
                                                worksheet.Cells[incRowExcel, 8].Style.Font.Bold = true;
                                                worksheet.Cells["H" + RowB + ":H" + RowG].Merge = true;
                                                worksheet.Cells["H" + RowB + ":H" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells["H" + RowB + ":H" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 9].Value = "Fee Amount";
                                                worksheet.Cells[incRowExcel, 9].Style.Font.Bold = true;
                                                worksheet.Cells["I" + RowB + ":I" + RowG].Merge = true;
                                                worksheet.Cells["I" + RowB + ":I" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells["I" + RowB + ":I" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 10].Value = "Net IDR";
                                                worksheet.Cells[incRowExcel, 10].Style.Font.Bold = true;
                                                worksheet.Cells["J" + RowB + ":J" + RowG].Merge = true;
                                                worksheet.Cells["J" + RowB + ":J" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells["J" + RowB + ":J" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 11].Value = "Unit";
                                                worksheet.Cells[incRowExcel, 11].Style.Font.Bold = true;
                                                worksheet.Cells["K" + RowB + ":K" + RowG].Merge = true;
                                                worksheet.Cells["K" + RowB + ":K" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells["K" + RowB + ":K" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 12].Value = "NAV";
                                                worksheet.Cells[incRowExcel, 12].Style.Font.Bold = true;
                                                worksheet.Cells["L" + RowB + ":L" + RowG].Merge = true;
                                                worksheet.Cells["L" + RowB + ":L" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells["L" + RowB + ":L" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 13].Value = "Remark";
                                                worksheet.Cells[incRowExcel, 13].Style.Font.Bold = true;
                                                worksheet.Cells["M" + RowB + ":M" + RowG].Merge = true;
                                                worksheet.Cells["M" + RowB + ":M" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells["M" + RowB + ":M" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 14].Value = "New/Top Up";
                                                worksheet.Cells[incRowExcel, 14].Style.Font.Bold = true;
                                                worksheet.Cells["N" + RowB + ":N" + RowG].Merge = true;
                                                worksheet.Cells["N" + RowB + ":N" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells["N" + RowB + ":N" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;


                                            }
                                            else if (rsHeader.Key.Type == "Redemption")
                                            {
                                                worksheet.Cells[incRowExcel, 2].Value = "Trx Type";
                                                worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                                worksheet.Cells["B" + RowB + ":B" + RowG].Merge = true;
                                                worksheet.Cells["B" + RowB + ":B" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells["B" + RowB + ":B" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 3].Value = "Fund";
                                                worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;
                                                worksheet.Cells["C" + RowB + ":C" + RowG].Merge = true;
                                                worksheet.Cells["C" + RowB + ":C" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells["C" + RowB + ":C" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 4].Value = "Client Name";
                                                worksheet.Cells[incRowExcel, 4].Style.Font.Bold = true;
                                                worksheet.Cells["D" + RowB + ":D" + RowG].Merge = true;
                                                worksheet.Cells["D" + RowB + ":D" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells["D" + RowB + ":D" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 5].Value = "Invest. Type";
                                                worksheet.Cells[incRowExcel, 5].Style.Font.Bold = true;
                                                worksheet.Cells["E" + RowB + ":E" + RowG].Merge = true;
                                                worksheet.Cells["E" + RowB + ":E" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells["E" + RowB + ":E" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 6].Value = "Sales Name";
                                                worksheet.Cells[incRowExcel, 6].Style.Font.Bold = true;
                                                worksheet.Cells["F" + RowB + ":F" + RowG].Merge = true;
                                                worksheet.Cells["F" + RowB + ":F" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells["F" + RowB + ":F" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;


                                                worksheet.Cells[incRowExcel, 7].Value = "NAV Date";
                                                worksheet.Cells[incRowExcel, 7].Style.Font.Bold = true;
                                                worksheet.Cells["G" + RowB + ":G" + RowG].Merge = true;
                                                worksheet.Cells["G" + RowB + ":G" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells["G" + RowB + ":G" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 8].Value = "Payment Date";
                                                worksheet.Cells[incRowExcel, 8].Style.Font.Bold = true;
                                                worksheet.Cells["H" + RowB + ":H" + RowG].Merge = true;
                                                worksheet.Cells["H" + RowB + ":H" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells["H" + RowB + ":H" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 9].Value = "Fee %";
                                                worksheet.Cells[incRowExcel, 9].Style.Font.Bold = true;
                                                worksheet.Cells["I" + RowB + ":I" + RowG].Merge = true;
                                                worksheet.Cells["I" + RowB + ":I" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells["I" + RowB + ":I" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 10].Value = "Fee Amount";
                                                worksheet.Cells[incRowExcel, 10].Style.Font.Bold = true;
                                                worksheet.Cells["J" + RowB + ":J" + RowG].Merge = true;
                                                worksheet.Cells["J" + RowB + ":J" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells["J" + RowB + ":J" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 11].Value = "Net IDR";
                                                worksheet.Cells[incRowExcel, 11].Style.Font.Bold = true;
                                                worksheet.Cells["K" + RowB + ":K" + RowG].Merge = true;
                                                worksheet.Cells["K" + RowB + ":K" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells["K" + RowB + ":K" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 12].Value = "Unit";
                                                worksheet.Cells[incRowExcel, 12].Style.Font.Bold = true;
                                                worksheet.Cells["L" + RowB + ":L" + RowG].Merge = true;
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
                                                worksheet.Cells[incRowExcel, 2].Value = "FundFrom";
                                                worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                                worksheet.Cells["B" + RowB + ":B" + RowG].Merge = true;
                                                worksheet.Cells["B" + RowB + ":B" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells["B" + RowB + ":B" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 3].Value = "FundTo";
                                                worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;
                                                worksheet.Cells["C" + RowB + ":C" + RowG].Merge = true;
                                                worksheet.Cells["C" + RowB + ":C" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells["C" + RowB + ":C" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 4].Value = "Client Name";
                                                worksheet.Cells[incRowExcel, 4].Style.Font.Bold = true;
                                                worksheet.Cells["D" + RowB + ":D" + RowG].Merge = true;
                                                worksheet.Cells["D" + RowB + ":D" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells["D" + RowB + ":D" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;


                                                worksheet.Cells[incRowExcel, 5].Value = "Invest. Type";
                                                worksheet.Cells[incRowExcel, 5].Style.Font.Bold = true;
                                                worksheet.Cells["E" + RowB + ":E" + RowG].Merge = true;
                                                worksheet.Cells["E" + RowB + ":E" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells["E" + RowB + ":E" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;


                                                worksheet.Cells[incRowExcel, 6].Value = "Sales Name";
                                                worksheet.Cells[incRowExcel, 6].Style.Font.Bold = true;
                                                worksheet.Cells["F" + RowB + ":F" + RowG].Merge = true;
                                                worksheet.Cells["F" + RowB + ":F" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells["F" + RowB + ":F" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 7].Value = "NAV Date";
                                                worksheet.Cells[incRowExcel, 7].Style.Font.Bold = true;
                                                worksheet.Cells["G" + RowB + ":G" + RowG].Merge = true;
                                                worksheet.Cells["G" + RowB + ":G" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells["G" + RowB + ":G" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 8].Value = "Payment Date";
                                                worksheet.Cells[incRowExcel, 8].Style.Font.Bold = true;
                                                worksheet.Cells["H" + RowB + ":H" + RowG].Merge = true;
                                                worksheet.Cells["H" + RowB + ":H" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells["H" + RowB + ":H" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 9].Value = "Fee Type";
                                                worksheet.Cells[incRowExcel, 9].Style.Font.Bold = true;
                                                worksheet.Cells["I" + RowB + ":I" + RowG].Merge = true;
                                                worksheet.Cells["I" + RowB + ":I" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells["I" + RowB + ":I" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 10].Value = "Fee %";
                                                worksheet.Cells[incRowExcel, 10].Style.Font.Bold = true;
                                                worksheet.Cells["J" + RowB + ":J" + RowG].Merge = true;
                                                worksheet.Cells["J" + RowB + ":J" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells["J" + RowB + ":J" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 11].Value = "Fee Amount";
                                                worksheet.Cells[incRowExcel, 11].Style.Font.Bold = true;
                                                worksheet.Cells["K" + RowB + ":K" + RowG].Merge = true;
                                                worksheet.Cells["K" + RowB + ":K" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells["K" + RowB + ":K" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 12].Value = "Net IDR From";
                                                worksheet.Cells[incRowExcel, 12].Style.Font.Bold = true;
                                                worksheet.Cells["L" + RowB + ":L" + RowG].Merge = true;
                                                worksheet.Cells["L" + RowB + ":L" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells["L" + RowB + ":L" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 13].Value = "Unit From";
                                                worksheet.Cells[incRowExcel, 13].Style.Font.Bold = true;
                                                worksheet.Cells["M" + RowB + ":M" + RowG].Merge = true;
                                                worksheet.Cells["M" + RowB + ":M" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells["M" + RowB + ":M" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 14].Value = "NAV From";
                                                worksheet.Cells[incRowExcel, 14].Style.Font.Bold = true;
                                                worksheet.Cells["N" + RowB + ":N" + RowG].Merge = true;
                                                worksheet.Cells["N" + RowB + ":N" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells["N" + RowB + ":N" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 15].Value = "Net IDR From";
                                                worksheet.Cells[incRowExcel, 15].Style.Font.Bold = true;
                                                worksheet.Cells["O" + RowB + ":O" + RowG].Merge = true;
                                                worksheet.Cells["O" + RowB + ":O" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells["O" + RowB + ":O" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 16].Value = "Unit To";
                                                worksheet.Cells[incRowExcel, 16].Style.Font.Bold = true;
                                                worksheet.Cells["P" + RowB + ":P" + RowG].Merge = true;
                                                worksheet.Cells["P" + RowB + ":P" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells["P" + RowB + ":P" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 17].Value = "NAV To";
                                                worksheet.Cells[incRowExcel, 17].Style.Font.Bold = true;
                                                worksheet.Cells["Q" + RowB + ":Q" + RowG].Merge = true;
                                                worksheet.Cells["Q" + RowB + ":Q" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells["Q" + RowB + ":Q" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 18].Value = "Remark";
                                                worksheet.Cells[incRowExcel, 18].Style.Font.Bold = true;
                                                worksheet.Cells["R" + RowB + ":R" + RowG].Merge = true;
                                                worksheet.Cells["R" + RowB + ":R" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells["R" + RowB + ":R" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

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



                                                //if (_fundID != rsDetail.FundID)
                                                //{
                                                //    worksheet.Cells["A" + incRowExcel + ":T" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                //}

                                                //ThickBox Border
                                                if (rsDetail.Type == "Subscription")
                                                {
                                                    worksheet.Cells["A" + RowB + ":N" + RowG].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                    worksheet.Cells["A" + RowB + ":N" + RowG].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                    worksheet.Cells["A" + RowB + ":N" + RowG].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                                    worksheet.Cells["A" + RowB + ":A" + RowG].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                }
                                                else if (rsDetail.Type == "Redemption")
                                                {
                                                    worksheet.Cells["A" + RowB + ":N" + RowG].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                    worksheet.Cells["A" + RowB + ":N" + RowG].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                    worksheet.Cells["A" + RowB + ":N" + RowG].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                                    worksheet.Cells["A" + RowB + ":A" + RowG].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                }
                                                else
                                                {
                                                    worksheet.Cells["A" + RowB + ":R" + RowG].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                    worksheet.Cells["A" + RowB + ":R" + RowG].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                    worksheet.Cells["A" + RowB + ":R" + RowG].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                                    worksheet.Cells["A" + RowB + ":A" + RowG].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                }


                                                //area detail
                                                worksheet.Cells[incRowExcel, 1].Value = rsDetail.ClientID;
                                                worksheet.Cells[incRowExcel, 1].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 1].Style.WrapText = true;


                                                if (rsDetail.Type == "Subscription")
                                                {
                                                    worksheet.Cells[incRowExcel, 2].Value = rsDetail.Type;
                                                    worksheet.Cells[incRowExcel, 2].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                    worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                    worksheet.Cells[incRowExcel, 2].Style.WrapText = true;

                                                    worksheet.Cells[incRowExcel, 3].Value = rsDetail.FundName;
                                                    worksheet.Cells[incRowExcel, 3].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                    worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                    worksheet.Cells[incRowExcel, 3].Style.WrapText = true;


                                                    worksheet.Cells[incRowExcel, 4].Value = rsDetail.ClientName;
                                                    worksheet.Cells[incRowExcel, 4].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                    worksheet.Cells[incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                    worksheet.Cells[incRowExcel, 4].Style.WrapText = true;

                                                    worksheet.Cells[incRowExcel, 5].Value = rsDetail.InvestorType;
                                                    worksheet.Cells[incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                    worksheet.Cells[incRowExcel, 5].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                    worksheet.Cells[incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                    worksheet.Cells[incRowExcel, 5].Style.WrapText = true;

                                                    worksheet.Cells[incRowExcel, 6].Value = rsDetail.AgentName;
                                                    worksheet.Cells[incRowExcel, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                    worksheet.Cells[incRowExcel, 6].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                    worksheet.Cells[incRowExcel, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                    worksheet.Cells[incRowExcel, 6].Style.WrapText = true;

                                                    worksheet.Cells[incRowExcel, 7].Value = rsDetail.NAVDate;
                                                    worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "dd-MMM-yyyy";
                                                    worksheet.Cells[incRowExcel, 7].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                    worksheet.Cells[incRowExcel, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                    worksheet.Cells[incRowExcel, 7].Style.WrapText = true;

                                                    worksheet.Cells[incRowExcel, 8].Value = rsDetail.FeePercent;
                                                    worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0.00";
                                                    worksheet.Cells[incRowExcel, 8].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                    worksheet.Cells[incRowExcel, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                    worksheet.Cells[incRowExcel, 8].Style.WrapText = true;


                                                    worksheet.Cells[incRowExcel, 9].Value = rsDetail.FeeAmount;
                                                    worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0.00";
                                                    worksheet.Cells[incRowExcel, 9].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                    worksheet.Cells[incRowExcel, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                    worksheet.Cells[incRowExcel, 9].Style.WrapText = true;

                                                    worksheet.Cells[incRowExcel, 10].Value = rsDetail.CashBalance;
                                                    worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0.00";
                                                    worksheet.Cells[incRowExcel, 10].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                    worksheet.Cells[incRowExcel, 10].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                    worksheet.Cells[incRowExcel, 10].Style.WrapText = true;


                                                    worksheet.Cells[incRowExcel, 11].Value = rsDetail.UnitBalance;
                                                    worksheet.Cells[incRowExcel, 11].Style.Numberformat.Format = "#,##0.0000";
                                                    worksheet.Cells[incRowExcel, 11].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                    worksheet.Cells[incRowExcel, 11].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                    worksheet.Cells[incRowExcel, 11].Style.WrapText = true;

                                                    worksheet.Cells[incRowExcel, 12].Value = rsDetail.Nav;
                                                    worksheet.Cells[incRowExcel, 12].Style.Numberformat.Format = "#,##0.0000";
                                                    worksheet.Cells[incRowExcel, 12].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                    worksheet.Cells[incRowExcel, 12].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                    worksheet.Cells[incRowExcel, 12].Style.WrapText = true;


                                                    worksheet.Cells[incRowExcel, 13].Value = rsDetail.Remark;
                                                    worksheet.Cells[incRowExcel, 13].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                    worksheet.Cells[incRowExcel, 13].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                    worksheet.Cells[incRowExcel, 13].Style.WrapText = true;

                                                    worksheet.Cells[incRowExcel, 14].Value = rsDetail.TopUp;
                                                    worksheet.Cells[incRowExcel, 14].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                    worksheet.Cells[incRowExcel, 14].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                    worksheet.Cells[incRowExcel, 14].Style.WrapText = true;

                                                }
                                                else if (rsDetail.Type == "Redemption")
                                                {
                                                    worksheet.Cells[incRowExcel, 2].Value = rsDetail.Type;
                                                    worksheet.Cells[incRowExcel, 2].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                    worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                    worksheet.Cells[incRowExcel, 2].Style.WrapText = true;


                                                    worksheet.Cells[incRowExcel, 3].Value = rsDetail.FundName;
                                                    worksheet.Cells[incRowExcel, 3].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                    worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                    worksheet.Cells[incRowExcel, 3].Style.WrapText = true;


                                                    worksheet.Cells[incRowExcel, 4].Value = rsDetail.ClientName;
                                                    worksheet.Cells[incRowExcel, 4].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                    worksheet.Cells[incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                    worksheet.Cells[incRowExcel, 4].Style.WrapText = true;

                                                    worksheet.Cells[incRowExcel, 5].Value = rsDetail.InvestorType;
                                                    worksheet.Cells[incRowExcel, 5].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                    worksheet.Cells[incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                    worksheet.Cells[incRowExcel, 5].Style.WrapText = true;


                                                    worksheet.Cells[incRowExcel, 6].Value = rsDetail.AgentName;
                                                    worksheet.Cells[incRowExcel, 6].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                    worksheet.Cells[incRowExcel, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                    worksheet.Cells[incRowExcel, 6].Style.WrapText = true;


                                                    worksheet.Cells[incRowExcel, 7].Value = rsDetail.NAVDate;
                                                    worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "dd-MMM-yyyy";
                                                    worksheet.Cells[incRowExcel, 7].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                    worksheet.Cells[incRowExcel, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                    worksheet.Cells[incRowExcel, 7].Style.WrapText = true;


                                                    worksheet.Cells[incRowExcel, 8].Value = rsDetail.SettlementDate;
                                                    worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "dd-MMM-yyyy";
                                                    worksheet.Cells[incRowExcel, 8].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                    worksheet.Cells[incRowExcel, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                    worksheet.Cells[incRowExcel, 8].Style.WrapText = true;


                                                    worksheet.Cells[incRowExcel, 9].Value = rsDetail.FeePercent;
                                                    worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0.00";
                                                    worksheet.Cells[incRowExcel, 9].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                    worksheet.Cells[incRowExcel, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                    worksheet.Cells[incRowExcel, 9].Style.WrapText = true;


                                                    worksheet.Cells[incRowExcel, 10].Value = rsDetail.FeeAmount;
                                                    worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0.00";
                                                    worksheet.Cells[incRowExcel, 10].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                    worksheet.Cells[incRowExcel, 10].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                    worksheet.Cells[incRowExcel, 10].Style.WrapText = true;


                                                    worksheet.Cells[incRowExcel, 11].Value = rsDetail.CashBalance;
                                                    worksheet.Cells[incRowExcel, 11].Style.Numberformat.Format = "#,##0.00";
                                                    worksheet.Cells[incRowExcel, 11].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                    worksheet.Cells[incRowExcel, 11].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                    worksheet.Cells[incRowExcel, 11].Style.WrapText = true;


                                                    worksheet.Cells[incRowExcel, 12].Value = rsDetail.UnitBalance;
                                                    worksheet.Cells[incRowExcel, 12].Style.Numberformat.Format = "#,##0.0000";
                                                    worksheet.Cells[incRowExcel, 12].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                    worksheet.Cells[incRowExcel, 12].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                    worksheet.Cells[incRowExcel, 12].Style.WrapText = true;


                                                    worksheet.Cells[incRowExcel, 13].Value = rsDetail.Nav;
                                                    worksheet.Cells[incRowExcel, 13].Style.Numberformat.Format = "#,##0.0000";
                                                    worksheet.Cells[incRowExcel, 13].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                    worksheet.Cells[incRowExcel, 13].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                    worksheet.Cells[incRowExcel, 31].Style.WrapText = true;


                                                    worksheet.Cells[incRowExcel, 14].Value = rsDetail.Remark;
                                                    worksheet.Cells[incRowExcel, 14].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                    worksheet.Cells[incRowExcel, 14].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                    worksheet.Cells[incRowExcel, 14].Style.WrapText = true;

                                                }
                                                else
                                                {
                                                    worksheet.Cells[incRowExcel, 2].Value = rsDetail.FundNameFrom;                          
                                                    worksheet.Cells[incRowExcel, 2].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                    worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                    worksheet.Cells[incRowExcel, 2].Style.WrapText = true;

                                                    worksheet.Cells[incRowExcel, 3].Value = rsDetail.FundNameTo;
                                                    worksheet.Cells[incRowExcel, 3].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                    worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                    worksheet.Cells[incRowExcel, 3].Style.WrapText = true;

                                                    worksheet.Cells[incRowExcel, 4].Value = rsDetail.ClientName;
                                                    worksheet.Cells[incRowExcel, 4].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                    worksheet.Cells[incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                    worksheet.Cells[incRowExcel, 4].Style.WrapText = true;


                                                    worksheet.Cells[incRowExcel, 5].Value = rsDetail.InvestorType;
                                                    worksheet.Cells[incRowExcel, 5].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                    worksheet.Cells[incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                    worksheet.Cells[incRowExcel, 5].Style.WrapText = true;


                                                    worksheet.Cells[incRowExcel, 6].Value = rsDetail.AgentName;
                                                    worksheet.Cells[incRowExcel, 6].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                    worksheet.Cells[incRowExcel, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                    worksheet.Cells[incRowExcel, 6].Style.WrapText = true;

                                                    worksheet.Cells[incRowExcel, 7].Value = rsDetail.NAVDate;
                                                    worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "dd-MMM-yyyy";
                                                    worksheet.Cells[incRowExcel, 7].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                    worksheet.Cells[incRowExcel, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                    worksheet.Cells[incRowExcel, 7].Style.WrapText = true;

                                                    worksheet.Cells[incRowExcel, 8].Value = rsDetail.SettlementDate;
                                                    worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "dd-MMM-yyyy";
                                                    worksheet.Cells[incRowExcel, 8].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                    worksheet.Cells[incRowExcel, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                    worksheet.Cells[incRowExcel, 8].Style.WrapText = true;

                                                    worksheet.Cells[incRowExcel, 9].Value = rsDetail.FeeType;
                                                    worksheet.Cells[incRowExcel, 9].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                    worksheet.Cells[incRowExcel, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                    worksheet.Cells[incRowExcel, 9].Style.WrapText = true;

                                                    worksheet.Cells[incRowExcel, 10].Value = rsDetail.FeePercent;
                                                    worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0.00";
                                                    worksheet.Cells[incRowExcel, 10].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                    worksheet.Cells[incRowExcel, 10].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                    worksheet.Cells[incRowExcel, 10].Style.WrapText = true;

                                                    worksheet.Cells[incRowExcel, 11].Value = rsDetail.FeeAmount;
                                                    worksheet.Cells[incRowExcel, 11].Style.Numberformat.Format = "#,##0.00";
                                                    worksheet.Cells[incRowExcel, 11].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                    worksheet.Cells[incRowExcel, 11].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                    worksheet.Cells[incRowExcel, 11].Style.WrapText = true;

                                                    worksheet.Cells[incRowExcel, 12].Value = rsDetail.TotalCashAmountFundFrom;
                                                    worksheet.Cells[incRowExcel, 12].Style.Numberformat.Format = "#,##0.00";
                                                    worksheet.Cells[incRowExcel, 12].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                    worksheet.Cells[incRowExcel, 12].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                    worksheet.Cells[incRowExcel, 12].Style.WrapText = true;

                                                    worksheet.Cells[incRowExcel, 13].Value = rsDetail.TotalUnitAmountFundFrom;
                                                    worksheet.Cells[incRowExcel, 13].Style.Numberformat.Format = "#,##0.00";
                                                    worksheet.Cells[incRowExcel, 13].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                    worksheet.Cells[incRowExcel, 13].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                    worksheet.Cells[incRowExcel, 13].Style.WrapText = true;

                                                    worksheet.Cells[incRowExcel, 14].Value = rsDetail.NAVFrom;
                                                    worksheet.Cells[incRowExcel, 14].Style.Numberformat.Format = "#,##0.0000";
                                                    worksheet.Cells[incRowExcel, 14].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                    worksheet.Cells[incRowExcel, 14].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                    worksheet.Cells[incRowExcel, 14].Style.WrapText = true;

                                                    worksheet.Cells[incRowExcel, 15].Value = rsDetail.TotalCashAmountFundTo;
                                                    worksheet.Cells[incRowExcel, 15].Style.Numberformat.Format = "#,##0.0000";
                                                    worksheet.Cells[incRowExcel, 15].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                    worksheet.Cells[incRowExcel, 15].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                    worksheet.Cells[incRowExcel, 15].Style.WrapText = true;

                                                    worksheet.Cells[incRowExcel, 16].Value = rsDetail.TotalUnitAmountFundTo;
                                                    worksheet.Cells[incRowExcel, 16].Style.Numberformat.Format = "#,##0.0000";
                                                    worksheet.Cells[incRowExcel, 16].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                    worksheet.Cells[incRowExcel, 16].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                    worksheet.Cells[incRowExcel, 16].Style.WrapText = true;

                                                    worksheet.Cells[incRowExcel, 17].Value = rsDetail.NAVTo;
                                                    worksheet.Cells[incRowExcel, 17].Style.Numberformat.Format = "#,##0.0000";
                                                    worksheet.Cells[incRowExcel, 17].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                    worksheet.Cells[incRowExcel, 17].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                    worksheet.Cells[incRowExcel, 17].Style.WrapText = true;

                                                    worksheet.Cells[incRowExcel, 18].Value = rsDetail.Remark;
                                                    worksheet.Cells[incRowExcel, 18].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                    worksheet.Cells[incRowExcel, 18].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                    worksheet.Cells[incRowExcel, 18].Style.WrapText = true;

                                                }


                                                _endRowDetail = incRowExcel;
                                                _no++;
                                                incRowExcel++;
                                                _fundID = rsDetail.FundID;
                                            }
                                            if (rsHeader.Key.Type == "Subscription")
                                            {
                                                worksheet.Cells["A" + _endRowDetail + ":N" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + _startRowDetail + ":N" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells["A" + _startRowDetail + ":A" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                            }
                                            else if (rsHeader.Key.Type == "Redemption")
                                            {
                                                worksheet.Cells["A" + _endRowDetail + ":N" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + _startRowDetail + ":N" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells["A" + _startRowDetail + ":A" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                            }
                                            else
                                            {
                                                worksheet.Cells["A" + _endRowDetail + ":R" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + _startRowDetail + ":R" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells["A" + _startRowDetail + ":A" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                            }

                                        }




                                        incRowExcel++;

                                        //-----------------------------------
                                        using (SqlConnection DbCon1 = new SqlConnection(Tools.conString))
                                        {
                                            DbCon1.Open();
                                            using (SqlCommand cmd1 = DbCon1.CreateCommand())
                                            {
                                                //                                              
                                                cmd1.CommandText =
                                                @"
                                                Select A.FundName, A.FundNameTo,A.NoRow,A.CurrencyID,A.Type,A.FundID,A.FundIDTo,sum(A.TotalCashAmountFundTo)TotalCashAmountFundTo,sum(A.TotalUnitAmountFundTo)TotalUnitAmountFundTo,sum(A.TotalCashAmount)TotalCashAmount,sum(A.TotalUnitAmount)TotalUnitAmount from ( 
                                                Select  F.Name FundName, G.Name FundNameTo,'1' NoRow,CU.ID CurrencyID,'Subscription' Type ,F.ID FundID,G.ID FundIDTo,sum (0)TotalCashAmountFundTo,sum (0)TotalUnitAmountFundTo,
                                                sum (TotalCashAmount)TotalCashAmount,sum (TotalUnitAmount)TotalUnitAmount 
                                                from ClientSubscription A 
                                                left join Fund F on A.FundPK = F.fundPK and f.Status in (1,2) 
                                                left join Fund G on A.FundPK = G.fundPK and G.Status in (1,2)   
                                                left join Currency CU on A.CurrencyPK = CU.CurrencyPK and CU.Status in (1,2)   
                                                left join FundClient FC on A.FundClientPK = FC.FundClientPK and fc.Status in (1,2)    
                                                left join Agent AG on A.AgentPK = AG.AgentPK and AG.Status in (1,2)  
                                                left join Department D on D.DepartmentPK = AG.DepartmentPK and D.Status in (1,2)
                                                where " + _statusSubs + _paramFund + _paramAgent + _paramFundClient + _paramDepartment + @" and 
                                                NAVDate Between @ValueDateFrom and @ValueDateTo  
                                                Group By F.ID,CU.ID,G.ID,F.Name,G.Name
                                                UNION ALL   
                                                Select F.Name FundName, G.Name FundNameTo,'2' NoRow,CU.ID CurrencyID,'Redemption' Type,F.ID FundID,G.ID FundIDTo,sum (0)TotalCashAmountFundTo,sum (0)TotalUnitAmountFundTo,
                                                sum (TotalCashAmount)TotalCashAmount,sum (TotalUnitAmount)TotalUnitAmount
                                                from ClientRedemption A 
                                                left join Fund F on A.FundPK = F.fundPK and f.Status in (1,2)    
                                                left join Fund G on A.FundPK = G.fundPK and G.Status in (1,2) 
                                                left join Currency CU on A.CurrencyPK = CU.CurrencyPK and CU.Status in (1,2)    
                                                left join FundClient FC on A.FundClientPK = FC.FundClientPK and fc.Status in (1,2)    
                                                left join Agent AG on A.AgentPK = AG.AgentPK and AG.Status in (1,2)  
                                                left join Department D on D.DepartmentPK = AG.DepartmentPK and D.Status in (1,2)
                                                where  " + _statusRedemp + _paramFund + _paramAgent + _paramFundClient + _paramDepartment + @" and 
                                                NAVDate Between @ValueDateFrom and @ValueDateTo  
                                                Group By F.ID,CU.ID,G.ID,F.Name,G.Name
                                                UNION ALL   
                                                Select F.Name FundName, G.Name FundNameTo,'3' NoRow,CU.ID CurrencyID,'Switching' Type,F.ID FundID,G.ID FundIDTo,sum (TotalCashAmountFundTo)TotalCashAmountFundTo,sum (TotalUnitAmountFundTo)TotalUnitAmountFundTo,
                                                sum (TotalCashAmountFundFrom)TotalCashAmount,sum (TotalUnitAmountFundFrom)TotalUnitAmount
                                                from ClientSwitching A 
                                                left join Fund F on A.FundPKFrom = F.fundPK and f.Status in (1,2) 
                                                left join Fund G on A.FundPKTo = G.fundPK and G.Status in (1,2)    
                                                left join Currency CU on A.CurrencyPK = CU.CurrencyPK and CU.Status in (1,2)    
                                                left join FundClient FC on A.FundClientPK = FC.FundClientPK and fc.Status in (1,2)    
                                                left join Agent AG on FC.SellingAgentPK = AG.AgentPK and AG.Status in (1,2) 
                                                left join Department D on D.DepartmentPK = AG.DepartmentPK and D.Status in (1,2)
                                                where  " + _statusSwitch + _paramFund + _paramAgentSwitching + _paramFundClient + _paramDepartmentSwitching + @" and 
                                                NAVDate Between @ValueDateFrom and @ValueDateTo  
                                                Group By F.ID,CU.ID,G.ID,F.Name,G.Name
                                                )A   
                                                Group by A.NoRow,A.Type,A.FundID,A.FundIDTo,A.CurrencyID,A.FundName,A.FundNameTo
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
                                                                rSingle1.FundIDTo = Convert.ToString(dr1["FundIDTo"]);
                                                                rSingle1.FundName = Convert.ToString(dr1["FundName"]);
                                                                rSingle1.FundNameTo = Convert.ToString(dr1["FundNameTo"]);
                                                                rSingle1.CashBalance = Convert.ToDecimal(dr1["TotalCashAmount"]);
                                                                rSingle1.UnitBalance = Convert.ToDecimal(dr1["TotalUnitAmount"]);
                                                                rSingle1.TotalCashAmountFundTo = Convert.ToDecimal(dr1["TotalCashAmountFundTo"]);
                                                                rSingle1.TotalUnitAmountFundTo = Convert.ToDecimal(dr1["TotalUnitAmountFundTo"]);
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



                                                                if (rsHeader1.Key.Type == "Subscription")
                                                                {
                                                                    worksheet.Cells[incRowExcel, 2].Value = "Fund";
                                                                    worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                                                    worksheet.Cells["B" + RowBZ + ":C" + RowGZ].Merge = true;
                                                                    worksheet.Cells["B" + RowBZ + ":C" + RowGZ].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                                    worksheet.Cells["B" + RowBZ + ":C" + RowGZ].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                                    worksheet.Cells[incRowExcel, 2].Style.WrapText = true;

                                                                    //worksheet.Cells[incRowExcel, 3].Value = " ";
                                                                    //worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;
                                                                    //worksheet.Cells["C" + RowBZ + ":C" + RowGZ].Merge = true;
                                                                    //worksheet.Cells["C" + RowBZ + ":C" + RowGZ].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                                    //worksheet.Cells["C" + RowBZ + ":C" + RowGZ].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                                    worksheet.Cells[incRowExcel, 4].Value = "Amount";
                                                                    worksheet.Cells[incRowExcel, 4].Style.Font.Bold = true;
                                                                    worksheet.Cells["D" + RowBZ + ":D" + RowGZ].Merge = true;
                                                                    worksheet.Cells["D" + RowBZ + ":D" + RowGZ].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                                    worksheet.Cells["D" + RowBZ + ":D" + RowGZ].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                                    worksheet.Cells[incRowExcel, 5].Value = "Currency";
                                                                    worksheet.Cells[incRowExcel, 5].Style.Font.Bold = true;
                                                                    worksheet.Cells["E" + RowBZ + ":E" + RowGZ].Merge = true;
                                                                    worksheet.Cells["E" + RowBZ + ":E" + RowGZ].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                                    worksheet.Cells["E" + RowBZ + ":E" + RowGZ].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                                }
                                                                else if (rsHeader1.Key.Type == "Redemption")
                                                                {
                                                                    worksheet.Cells[incRowExcel, 2].Value = "Fund";
                                                                    worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                                                    worksheet.Cells["B" + RowBZ + ":C" + RowGZ].Merge = true;
                                                                    worksheet.Cells["B" + RowBZ + ":C" + RowGZ].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                                    worksheet.Cells["B" + RowBZ + ":C" + RowGZ].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                                    worksheet.Cells[incRowExcel, 2].Style.WrapText = true;

                                                                    //worksheet.Cells[incRowExcel, 3].Value = " ";
                                                                    //worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;
                                                                    //worksheet.Cells["C" + RowBZ + ":C" + RowGZ].Merge = true;
                                                                    //worksheet.Cells["C" + RowBZ + ":C" + RowGZ].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                                    //worksheet.Cells["C" + RowBZ + ":C" + RowGZ].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                                    worksheet.Cells[incRowExcel, 4].Value = "Unit";
                                                                    worksheet.Cells[incRowExcel, 4].Style.Font.Bold = true;
                                                                    worksheet.Cells["D" + RowBZ + ":D" + RowGZ].Merge = true;
                                                                    worksheet.Cells["D" + RowBZ + ":D" + RowGZ].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                                    worksheet.Cells["D" + RowBZ + ":D" + RowGZ].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

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
                                                                else
                                                                {
                                                                    worksheet.Cells[incRowExcel, 2].Value = "Fund From";
                                                                    worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                                                    worksheet.Cells["B" + RowBZ + ":B" + RowGZ].Merge = true;
                                                                    worksheet.Cells["B" + RowBZ + ":B" + RowGZ].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                                    worksheet.Cells["B" + RowBZ + ":B" + RowGZ].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                                    worksheet.Cells[incRowExcel, 3].Value = "Fund To";
                                                                    worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;
                                                                    worksheet.Cells["C" + RowBZ + ":C" + RowGZ].Merge = true;
                                                                    worksheet.Cells["C" + RowBZ + ":C" + RowGZ].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                                    worksheet.Cells["C" + RowBZ + ":C" + RowGZ].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                                    worksheet.Cells[incRowExcel, 4].Value = "Unit From";
                                                                    worksheet.Cells[incRowExcel, 4].Style.Font.Bold = true;
                                                                    worksheet.Cells["D" + RowBZ + ":D" + RowGZ].Merge = true;
                                                                    worksheet.Cells["D" + RowBZ + ":D" + RowGZ].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                                    worksheet.Cells["D" + RowBZ + ":D" + RowGZ].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                                    worksheet.Cells[incRowExcel, 5].Value = "Amount From";
                                                                    worksheet.Cells[incRowExcel, 5].Style.Font.Bold = true;
                                                                    worksheet.Cells["E" + RowBZ + ":E" + RowGZ].Merge = true;
                                                                    worksheet.Cells["E" + RowBZ + ":E" + RowGZ].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                                    worksheet.Cells["E" + RowBZ + ":E" + RowGZ].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                                    worksheet.Cells[incRowExcel, 6].Value = "Unit To";
                                                                    worksheet.Cells[incRowExcel, 6].Style.Font.Bold = true;
                                                                    worksheet.Cells["F" + RowBZ + ":F" + RowGZ].Merge = true;
                                                                    worksheet.Cells["F" + RowBZ + ":F" + RowGZ].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                                    worksheet.Cells["F" + RowBZ + ":F" + RowGZ].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                                    worksheet.Cells[incRowExcel, 7].Value = "Amount To";
                                                                    worksheet.Cells[incRowExcel, 7].Style.Font.Bold = true;
                                                                    worksheet.Cells["G" + RowBZ + ":G" + RowGZ].Merge = true;
                                                                    worksheet.Cells["G" + RowBZ + ":G" + RowGZ].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                                    worksheet.Cells["G" + RowBZ + ":G" + RowGZ].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                                    worksheet.Cells[incRowExcel, 8].Value = "Currency";
                                                                    worksheet.Cells[incRowExcel, 8].Style.Font.Bold = true;
                                                                    worksheet.Cells["H" + RowBZ + ":H" + RowGZ].Merge = true;
                                                                    worksheet.Cells["H" + RowBZ + ":H" + RowGZ].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                                    worksheet.Cells["H" + RowBZ + ":H" + RowGZ].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;


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
                                                                        worksheet.Cells["A" + RowBZ + ":E" + RowGZ].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                                                        worksheet.Cells["A" + RowBZ + ":E" + RowGZ].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                                    }
                                                                    else if (rsDetail1.Type == "Redemption")
                                                                    {
                                                                        worksheet.Cells["A" + RowBZ + ":F" + RowGZ].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                                        worksheet.Cells["A" + RowBZ + ":F" + RowGZ].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                                        worksheet.Cells["A" + RowBZ + ":F" + RowGZ].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                                                        worksheet.Cells["A" + RowBZ + ":F" + RowGZ].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                                    }
                                                                    else
                                                                    {
                                                                        worksheet.Cells["A" + RowBZ + ":H" + RowGZ].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                                        worksheet.Cells["A" + RowBZ + ":H" + RowGZ].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                                        worksheet.Cells["A" + RowBZ + ":H" + RowGZ].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                                                        worksheet.Cells["A" + RowBZ + ":H" + RowGZ].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                                    }

                                                                    //area detail
                                                                    worksheet.Cells[incRowExcel, 1].Value = _noZ;
                                                                    worksheet.Cells[incRowExcel, 1].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                                    worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                                    worksheet.Cells[incRowExcel, 1].Style.WrapText = true;

                                                                    if (rsDetail1.Type == "Subscription")
                                                                    {
                                                                        worksheet.Cells[incRowExcel, 2].Value = rsDetail1.FundName;
                                                                        worksheet.Cells[incRowExcel, 2].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                                        worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                                        worksheet.Cells[incRowExcel, 2].Style.WrapText = true;
                                                                        worksheet.Cells["B" + incRowExcel + ":C" + incRowExcel].Merge = true;


                                                                        worksheet.Cells[incRowExcel, 4].Value = rsDetail1.CashBalance;
                                                                        worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##0.00";
                                                                        worksheet.Cells[incRowExcel, 4].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                                        worksheet.Cells[incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                                        worksheet.Cells[incRowExcel, 4].Style.WrapText = true;

                                                                        worksheet.Cells[incRowExcel, 5].Value = rsDetail1.CurrencyID;
                                                                        worksheet.Cells[incRowExcel, 5].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                                        worksheet.Cells[incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                                        worksheet.Cells[incRowExcel, 5].Style.WrapText = true;
                                                                    }
                                                                    else if (rsDetail1.Type == "Redemption")
                                                                    {
                                                                        worksheet.Cells[incRowExcel, 2].Value = rsDetail1.FundName;
                                                                        worksheet.Cells[incRowExcel, 2].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                                        worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                                        worksheet.Cells[incRowExcel, 2].Style.WrapText = true;
                                                                        worksheet.Cells["B" + incRowExcel + ":C" + incRowExcel].Merge = true;

                                                                        worksheet.Cells[incRowExcel, 4].Value = rsDetail1.UnitBalance;
                                                                        worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##0.000000";
                                                                        worksheet.Cells[incRowExcel, 4].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                                        worksheet.Cells[incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                                        worksheet.Cells[incRowExcel, 4].Style.WrapText = true;

                                                                        worksheet.Cells[incRowExcel, 5].Value = rsDetail1.CashBalance;
                                                                        worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.00";
                                                                        worksheet.Cells[incRowExcel, 5].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                                        worksheet.Cells[incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                                        worksheet.Cells[incRowExcel, 5].Style.WrapText = true;

                                                                        worksheet.Cells[incRowExcel, 6].Value = rsDetail1.CurrencyID;
                                                                        worksheet.Cells[incRowExcel, 6].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                                        worksheet.Cells[incRowExcel, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                                        worksheet.Cells[incRowExcel, 6].Style.WrapText = true;

                                                                    }
                                                                    else
                                                                    {
                                                                        worksheet.Cells[incRowExcel, 2].Value = rsDetail1.FundName;
                                                                        worksheet.Cells[incRowExcel, 2].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                                        worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                                        worksheet.Cells[incRowExcel, 2].Style.WrapText = true;

                                                                        worksheet.Cells[incRowExcel, 3].Value = rsDetail1.FundNameTo;
                                                                        worksheet.Cells[incRowExcel, 3].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                                        worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                                        worksheet.Cells[incRowExcel, 3].Style.WrapText = true;

                                                                        worksheet.Cells[incRowExcel, 4].Value = rsDetail1.UnitBalance;
                                                                        worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##0.000000";
                                                                        worksheet.Cells[incRowExcel, 4].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                                        worksheet.Cells[incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                                        worksheet.Cells[incRowExcel, 4].Style.WrapText = true;

                                                                        worksheet.Cells[incRowExcel, 5].Value = rsDetail1.CashBalance;
                                                                        worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.00";
                                                                        worksheet.Cells[incRowExcel, 5].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                                        worksheet.Cells[incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                                        worksheet.Cells[incRowExcel, 5].Style.WrapText = true;

                                                                        worksheet.Cells[incRowExcel, 6].Value = rsDetail1.TotalUnitAmountFundTo;
                                                                        worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.000000";
                                                                        worksheet.Cells[incRowExcel, 6].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                                        worksheet.Cells[incRowExcel, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                                        worksheet.Cells[incRowExcel, 6].Style.WrapText = true;

                                                                        worksheet.Cells[incRowExcel, 7].Value = rsDetail1.TotalCashAmountFundTo;
                                                                        worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0.00";
                                                                        worksheet.Cells[incRowExcel, 7].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                                        worksheet.Cells[incRowExcel, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                                        worksheet.Cells[incRowExcel, 7].Style.WrapText = true;

                                                                        worksheet.Cells[incRowExcel, 8].Value = rsDetail1.CurrencyID;
                                                                        worksheet.Cells[incRowExcel, 8].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                                        worksheet.Cells[incRowExcel, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                                        worksheet.Cells[incRowExcel, 8].Style.WrapText = true;
                                                                    }


                                                                    _endRowDetailZ = incRowExcel;
                                                                    _noZ++;
                                                                    incRowExcel++;

                                                                }


                                                                if (rsHeader1.Key.Type == "Subscription")
                                                                {
                                                                    worksheet.Cells["A" + _startRowDetailZ + ":E" + _endRowDetailZ].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                                    worksheet.Cells["A" + _startRowDetailZ + ":E" + _endRowDetailZ].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                                                    worksheet.Cells["A" + _startRowDetailZ + ":E" + _endRowDetailZ].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                                }
                                                                else if (rsHeader1.Key.Type == "Redemption")
                                                                {
                                                                    worksheet.Cells["A" + _startRowDetailZ + ":F" + _endRowDetailZ].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                                    worksheet.Cells["A" + _startRowDetailZ + ":F" + _endRowDetailZ].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                                                    worksheet.Cells["A" + _startRowDetailZ + ":F" + _endRowDetailZ].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                                }
                                                                else
                                                                {
                                                                    worksheet.Cells["A" + _startRowDetailZ + ":H" + _endRowDetailZ].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                                    worksheet.Cells["A" + _startRowDetailZ + ":H" + _endRowDetailZ].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                                                    worksheet.Cells["A" + _startRowDetailZ + ":H" + _endRowDetailZ].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                                }






                                                                if (rsHeader1.Key.Type == "Subscription")
                                                                {
                                                                    worksheet.Cells[_endRowDetailZ + 1, 4].Style.Numberformat.Format = "#,##0.00";
                                                                    worksheet.Cells[_endRowDetailZ + 1, 4].Formula = "SUM(D" + _startRowDetailZ + ":D" + _endRowDetailZ + ")";
                                                                    worksheet.Cells[_endRowDetailZ + 1, 4].Calculate();
                                                                    worksheet.Cells[_endRowDetailZ + 1, 4].Style.Font.Bold = true;
                                                                }
                                                                else if (rsHeader1.Key.Type == "Redemption")
                                                                {

                                                                    worksheet.Cells[_endRowDetailZ + 1, 4].Style.Numberformat.Format = "#,##0.000000";
                                                                    worksheet.Cells[_endRowDetailZ + 1, 4].Formula = "SUM(D" + _startRowDetailZ + ":D" + _endRowDetailZ + ")";
                                                                    worksheet.Cells[_endRowDetailZ + 1, 4].Calculate();
                                                                    worksheet.Cells[_endRowDetailZ + 1, 4].Style.Font.Bold = true;

                                                                    worksheet.Cells[_endRowDetailZ + 1, 5].Style.Numberformat.Format = "#,##0.00";
                                                                    worksheet.Cells[_endRowDetailZ + 1, 5].Formula = "SUM(E" + _startRowDetailZ + ":E" + _endRowDetailZ + ")";
                                                                    worksheet.Cells[_endRowDetailZ + 1, 5].Calculate();
                                                                    worksheet.Cells[_endRowDetailZ + 1, 5].Style.Font.Bold = true;
                                                                }
                                                                else
                                                                {
                                                                    worksheet.Cells[_endRowDetailZ + 1, 4].Style.Numberformat.Format = "#,##0.000000";
                                                                    worksheet.Cells[_endRowDetailZ + 1, 4].Formula = "SUM(D" + _startRowDetailZ + ":D" + _endRowDetailZ + ")";
                                                                    worksheet.Cells[_endRowDetailZ + 1, 4].Calculate();
                                                                    worksheet.Cells[_endRowDetailZ + 1, 4].Style.Font.Bold = true;

                                                                    worksheet.Cells[_endRowDetailZ + 1, 5].Style.Numberformat.Format = "#,##0.00";
                                                                    worksheet.Cells[_endRowDetailZ + 1, 5].Formula = "SUM(E" + _startRowDetailZ + ":E" + _endRowDetailZ + ")";
                                                                    worksheet.Cells[_endRowDetailZ + 1, 5].Calculate();
                                                                    worksheet.Cells[_endRowDetailZ + 1, 5].Style.Font.Bold = true;

                                                                    worksheet.Cells[_endRowDetailZ + 1, 6].Style.Numberformat.Format = "#,##0.000000";
                                                                    worksheet.Cells[_endRowDetailZ + 1, 6].Formula = "SUM(F" + _startRowDetailZ + ":F" + _endRowDetailZ + ")";
                                                                    worksheet.Cells[_endRowDetailZ + 1, 6].Calculate();
                                                                    worksheet.Cells[_endRowDetailZ + 1, 6].Style.Font.Bold = true;

                                                                    worksheet.Cells[_endRowDetailZ + 1, 7].Style.Numberformat.Format = "#,##0.00";
                                                                    worksheet.Cells[_endRowDetailZ + 1, 7].Formula = "SUM(G" + _startRowDetailZ + ":G" + _endRowDetailZ + ")";
                                                                    worksheet.Cells[_endRowDetailZ + 1, 7].Calculate();
                                                                    worksheet.Cells[_endRowDetailZ + 1, 7].Style.Font.Bold = true;
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

                                        worksheet.Row(incRowExcel).PageBreak = _unitRegistryRpt.PageBreak;

                                        string _rangeA = "A:T" + incRowExcel;
                                        using (ExcelRange r = worksheet.Cells[_rangeA]) // KALO  KOLOM 1 SAMPE 9 A-I
                                        {
                                            r.Style.Font.Size = 6;
                                        }

                                        worksheet.DeleteRow(_lastRow);

                                        worksheet.PrinterSettings.FitToPage = true;
                                        //worksheet.PrinterSettings.FitToWidth = 1;
                                        worksheet.PrinterSettings.FitToHeight = 0;
                                        //worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel - 1, 20];
                                        //worksheet.Column(1).Width = 15;
                                        //worksheet.Column(2).Width = 15;
                                        //worksheet.Column(3).Width = 15;
                                        //worksheet.Column(4).Width = 15;
                                        //worksheet.Column(5).Width = 15;
                                        //worksheet.Column(6).Width = 15;
                                        //worksheet.Column(7).Width = 15;
                                        //worksheet.Column(8).Width = 15;
                                        //worksheet.Column(9).Width = 15;
                                        //worksheet.Column(10).Width = 15;
                                        //worksheet.Column(11).Width = 15;
                                        //worksheet.Column(12).Width = 15;
                                        //worksheet.Column(13).Width = 15;
                                        //worksheet.Column(14).Width = 15;
                                        //worksheet.Column(15).Width = 15;
                                        //worksheet.Column(16).Width = 15;
                                        //worksheet.Column(17).Width = 15;
                                        //worksheet.Column(18).Width = 15;
                                        //worksheet.Column(19).Width = 15;
                                        //worksheet.Column(20).Width = 15;

                                        worksheet.Column(1).AutoFit();
                                        worksheet.Column(2).Width = 15;
                                        worksheet.Column(3).Width = 15;
                                        worksheet.Column(4).Width = 15;
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
                                        worksheet.Column(16).AutoFit();
                                        worksheet.Column(17).AutoFit();
                                        worksheet.Column(18).AutoFit();
                                        worksheet.Column(19).AutoFit();


                                        // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                        //worksheet.Cells["A3:I14"].AutoFitColumns(); // UNTUK AUTO FIT UKURAN TULISAN DENGAN KOLOM, KEBETULAN INI BUAT HEADER AJA DIPAKENYA
                                        //worksheetApproved.Cells["A3:E3"].AutoFitColumns();
                                        worksheet.PrinterSettings.Orientation = eOrientation.Landscape; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                        worksheet.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN

                                        //worksheet.HeaderFooter.OddHeader.CenteredText = "\n \n \n \n \n \n \n &30&B Daily Total Transaction Report For All \n &28&B Subscription , Redemption , Switching";



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
                                        //worksheet.HeaderFooter.OddFooter.LeftAlignedText = Tools.DefaultReportFooterLeftText();



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

            #region Counter Fund Under 10 Bio
            else if (_unitRegistryRpt.ReportName.Equals("Counter Fund Under 10 Bio"))
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
                            string _paramAgent = "";
                            string _paramFundClient = "";
                            string _paramDepartment = "";
                            string _paramAgentSwitching = "";
                            string _paramDepartmentSwitching = "";

                            if (!_host.findString(_unitRegistryRpt.FundFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_unitRegistryRpt.FundFrom))
                            {
                                _paramFund = "And F.FundPK in ( " + _unitRegistryRpt.FundFrom + " ) ";
                            }
                            else
                            {
                                _paramFund = "";
                            }
                            if (!_host.findString(_unitRegistryRpt.AgentFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_unitRegistryRpt.AgentFrom))
                            {
                                _paramAgent = "And A.AgentPK in ( " + _unitRegistryRpt.AgentFrom + " ) ";
                                _paramAgentSwitching = "And AG.AgentPK in ( " + _unitRegistryRpt.AgentFrom + " ) ";
                            }
                            else
                            {
                                _paramAgent = "";
                                _paramAgentSwitching = "";
                            }
                            if (!_host.findString(_unitRegistryRpt.FundClientFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_unitRegistryRpt.FundClientFrom))
                            {
                                _paramFundClient = "And A.FundClientPK in ( " + _unitRegistryRpt.FundClientFrom + " ) ";
                            }
                            else
                            {
                                _paramFundClient = "";
                            }
                            if (!_host.findString(_unitRegistryRpt.DepartmentFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_unitRegistryRpt.DepartmentFrom))
                            {
                                _paramDepartment = "And A.DepartmentPK in ( " + _unitRegistryRpt.DepartmentFrom + " ) ";
                                _paramDepartmentSwitching = "And AG.DepartmentPK in ( " + _unitRegistryRpt.DepartmentFrom + " ) ";
                            }
                            else
                            {
                                _paramDepartment = "";
                                _paramDepartmentSwitching = "";
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
                                _statusSubs = "  (A.Status = 2 or A.Posted = 1) and A.Revised = 0  ";
                                _statusRedemp = "  (A.Status = 2 or A.Posted = 1) and A.Revised = 0  ";
                                _statusSwitch = "  (A.Status = 2 or A.Posted = 1) and A.Revised = 0  ";
                            }
                            else if (_unitRegistryRpt.Status == 7)
                            {
                                _statusSubs = "  (A.Status = 1 Or A.Status = 2 or A.Posted = 1) and  A.Revised = 0  ";
                                _statusRedemp = "  (A.Status = 1 Or A.Status = 2 or A.Posted = 1) and A.Revised = 0  ";
                                _statusSwitch = "  (A.Status = 1 Or A.Status = 2 or A.Posted = 1) and A.Revised = 0  ";
                            }
                            //                          
                            cmd.CommandText =
                                @"
                                select b.id FundID, b.Name FundName,c.id InstrumentID,a.DoneVolume AmountDeposito, DoneAmount NAVDeposito,a.DoneVolume AmountBond, DoneAmount NAVBond,
                                a.DoneVolume AmountEquity, DoneAmount NAVEquity,a.DoneVolume TotalInvestment, DoneAmount Amount, DoneAmount NAV, DoneAmount DaysPelanggaranAktif, DoneAmount DaysPelanggaranPasif, DoneAmount TotalAUM,DoneAmount  Under10Bio
                                 from Investment a left join fund b on a.fundpk = b.fundpk and b.Status in(1,2) 
                                left join Instrument c on a.InstrumentPK = c.InstrumentPK and c.status in(1,2)
                                where ValueDate between @ValueDateFrom and @ValueDateTo
                                 ";
                            cmd.CommandTimeout = 0;
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
                                    string filePath = Tools.ReportsPath + "CounterFundUnder10Bio" + "_" + _userID + ".xlsx";
                                    string pdfPath = Tools.ReportsPath + "CounterFundUnder10Bio" + "_" + _userID + ".pdf";
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

                                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Counter Fund Under 10 Bio");


                                        //ATUR DATA GROUPINGNYA DULU
                                        List<CounterFundUnder10BioRpt> rList = new List<CounterFundUnder10BioRpt>();
                                        while (dr0.Read())
                                        {
                                            CounterFundUnder10BioRpt rSingle = new CounterFundUnder10BioRpt();
                                            rSingle.FundID = dr0["FundID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["FundID"]);
                                            rSingle.FundName = dr0["FundName"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["FundName"]);
                                            rSingle.InstrumentID = Convert.ToString(dr0["InstrumentID"]);
                                            rSingle.AmountDeposito = Convert.ToDecimal(dr0["AmountDeposito"]);
                                            rSingle.NAVDeposito = Convert.ToDecimal(dr0["NAVDeposito"]);
                                            rSingle.AmountBond = Convert.ToDecimal(dr0["AmountBond"]);
                                            rSingle.NavBond = Convert.ToDecimal(dr0["NavBond"]);
                                            rSingle.AmountEquity = Convert.ToDecimal(dr0["AmountEquity"]);
                                            rSingle.NAVEquity = Convert.ToDecimal(dr0["NAVEquity"]);
                                            rSingle.TotalInvestment = Convert.ToDecimal(dr0["TotalInvestment"]);
                                            rSingle.Amount = Convert.ToDecimal(dr0["Amount"]);
                                            rSingle.NAV = Convert.ToDecimal(dr0["NAV"]);
                                            rSingle.DaysPelanggaranAktif = Convert.ToDecimal(dr0["DaysPelanggaranAktif"]);
                                            rSingle.DaysPelanggaranPasif = Convert.ToDecimal(dr0["DaysPelanggaranPasif"]);

                                            rList.Add(rSingle);

                                        }


                                        var QueryByFundID =
                                            from r in rList
                                            group r by new { } into rGroup
                                            select rGroup;

                                        int incRowExcel = 1;

                                        foreach (var rsHeader in QueryByFundID)
                                        {
                                            incRowExcel = incRowExcel + 2;
                                            worksheet.Cells[incRowExcel, 1].Value = "Daily Compliance Report";
                                            worksheet.Cells["A" + incRowExcel + ":I" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Size = 18;
                                            worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Value = "Date : ";
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 2].Value = _unitRegistryRpt.ValueDateFrom;
                                            worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 2].Style.Numberformat.Format = "dd-MMM-yyyy";
                                            incRowExcel = incRowExcel + 2;

                                            int _rowA = incRowExcel;
                                            int _rowB = incRowExcel + 1;
                                            int _startRowDetail = incRowExcel;
                                            worksheet.Cells[incRowExcel, 1].Value = "ID";
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Size = 20;
                                            worksheet.Cells["A" + _rowA + ":A" + _rowB].Merge = true;
                                            worksheet.Cells["A" + _rowA + ":A" + _rowB].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 1].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 2].Value = "FUND NAME";
                                            worksheet.Cells[incRowExcel, 2].Style.Font.Size = 15;
                                            worksheet.Cells["B" + _rowA + ":B" + _rowB].Merge = true;
                                            worksheet.Cells["B" + _rowA + ":B" + _rowB].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 2].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 3].Value = "DEPOSITO";
                                            worksheet.Cells["C" + incRowExcel + ":D" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 5].Value = "BOND";
                                            worksheet.Cells[incRowExcel, 5].Style.Font.Bold = true;
                                            worksheet.Cells["E" + incRowExcel + ":F" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 7].Value = "EQUITY";
                                            worksheet.Cells["G" + incRowExcel + ":H" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 7].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 9].Value = "TOTAL INVESTMENT(%NAV)";
                                            worksheet.Cells["I" + _rowA + ":I" + _rowB].Merge = true;
                                            worksheet.Cells["I" + _rowA + ":I" + _rowB].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 9].Style.Font.Size = 15;
                                            worksheet.Cells[incRowExcel, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 9].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 9].Style.WrapText = true;

                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 3].Value = "Amount";
                                            worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 4].Value = "%NAV";
                                            worksheet.Cells[incRowExcel, 4].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 5].Value = "Amount";
                                            worksheet.Cells[incRowExcel, 5].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 6].Value = "%NAV";
                                            worksheet.Cells[incRowExcel, 6].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 7].Value = "Amount";
                                            worksheet.Cells[incRowExcel, 7].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 8].Value = "%NAV";
                                            worksheet.Cells[incRowExcel, 8].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            //area header
                                            incRowExcel++;
                                            int _endRowDetail = 0;
                                            foreach (var rsDetail in rsHeader)
                                            {

                                                worksheet.Cells[incRowExcel, 1].Value = rsDetail.FundID;
                                                worksheet.Cells[incRowExcel, 2].Value = rsDetail.FundName;
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.AmountDeposito;
                                                worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 4].Value = rsDetail.NAVDeposito;
                                                worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##0.0000";
                                                worksheet.Cells[incRowExcel, 5].Value = rsDetail.AmountBond;
                                                worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 6].Value = rsDetail.NavBond;
                                                worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.0000";
                                                worksheet.Cells[incRowExcel, 7].Value = rsDetail.AmountEquity;
                                                worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 8].Value = rsDetail.NAVEquity;
                                                worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0.0000";
                                                worksheet.Cells[incRowExcel, 9].Value = rsDetail.TotalInvestment;
                                                worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0.0000";
                                                _endRowDetail = incRowExcel;
                                                incRowExcel++;
                                            }

                                            worksheet.Cells["A" + _startRowDetail + ":I" + _endRowDetail].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + _startRowDetail + ":I" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + _startRowDetail + ":I" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + _startRowDetail + ":I" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                        }




                                        incRowExcel = incRowExcel + 2;

                                        //-----------------------------------
                                        using (SqlConnection DbCon1 = new SqlConnection(Tools.conString))
                                        {
                                            DbCon1.Open();
                                            using (SqlCommand cmd1 = DbCon1.CreateCommand())
                                            {
                                                //                                              
                                                cmd1.CommandText =
                                                @"
                                                select b.id FundID, b.Name FundName,c.id InstrumentID,a.DoneVolume AmountDeposito, DoneAmount NAVDeposito,a.DoneVolume AmountBond, DoneAmount NAVBond,
                                                a.DoneVolume AmountEquity, DoneAmount NAVEquity,a.DoneVolume TotalInvestment, DoneAmount Amount, DoneAmount NAV, DoneAmount DaysPelanggaranAktif, DoneAmount DaysPelanggaranPasif, DoneAmount TotalAUM,DoneAmount  Under10Bio
                                                 from Investment a left join fund b on a.fundpk = b.fundpk and b.Status in(1,2) 
                                                left join Instrument c on a.InstrumentPK = c.InstrumentPK and c.status in(1,2)
                                                where ValueDate between @ValueDateFrom and @ValueDateTo
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
                                                            List<CounterFundUnder10BioRpt> rList1 = new List<CounterFundUnder10BioRpt>();
                                                            while (dr1.Read())
                                                            {
                                                                CounterFundUnder10BioRpt rSingle1 = new CounterFundUnder10BioRpt();
                                                                rSingle1.FundID = Convert.ToString(dr1["FundID"]);
                                                                rSingle1.InstrumentID = Convert.ToString(dr1["InstrumentID"]);
                                                                rSingle1.Amount = Convert.ToDecimal(dr1["Amount"]);
                                                                rSingle1.NAV = Convert.ToDecimal(dr1["NAV"]);
                                                                rSingle1.DaysPelanggaranAktif = Convert.ToDecimal(dr1["DaysPelanggaranAktif"]);
                                                                rSingle1.DaysPelanggaranPasif = Convert.ToDecimal(dr1["DaysPelanggaranPasif"]);
                                                                rList1.Add(rSingle1);

                                                            }


                                                            var QueryByFundID1 =
                                                                from r1 in rList1
                                                                group r1 by new { } into rGroup1
                                                                select rGroup1;

                                                            int _endRowDetailZ = 0;
                                                            int _startRowDetailZ = incRowExcel;

                                                            foreach (var rsHeader1 in QueryByFundID1)
                                                            {
                                                                worksheet.Cells["A" + incRowExcel + ":F" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                                worksheet.Cells[incRowExcel, 1].Value = "Fund";
                                                                worksheet.Cells[incRowExcel, 2].Value = "Instrument";
                                                                worksheet.Cells[incRowExcel, 3].Value = "Amount";
                                                                worksheet.Cells[incRowExcel, 4].Value = "% of NAV";
                                                                worksheet.Cells[incRowExcel, 5].Value = "Days Pelanggaran Aktif";
                                                                worksheet.Cells[incRowExcel, 6].Value = "Days Pelanggaran Pasif";
                                                                worksheet.Cells["A" + incRowExcel + ":F" + incRowExcel].Style.Font.Bold = true;

                                                                incRowExcel++;

                                                                //area header

                                                                foreach (var rsDetail1 in rsHeader1)
                                                                {
                                                                    worksheet.Cells[incRowExcel, 1].Value = rsDetail1.FundID;
                                                                    worksheet.Cells[incRowExcel, 2].Value = rsDetail1.InstrumentID;
                                                                    worksheet.Cells[incRowExcel, 3].Value = rsDetail1.Amount;
                                                                    worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "#,##0.00";
                                                                    worksheet.Cells[incRowExcel, 4].Value = rsDetail1.NAV;
                                                                    worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##0.0000";
                                                                    worksheet.Cells[incRowExcel, 5].Value = rsDetail1.DaysPelanggaranAktif;
                                                                    worksheet.Cells[incRowExcel, 6].Value = rsDetail1.DaysPelanggaranPasif;

                                                                    _endRowDetailZ = incRowExcel;
                                                                    incRowExcel++;

                                                                }


                                                                worksheet.Cells["A" + _startRowDetailZ + ":F" + _endRowDetailZ].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                                worksheet.Cells["A" + _startRowDetailZ + ":F" + _endRowDetailZ].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                                worksheet.Cells["A" + _startRowDetailZ + ":F" + _endRowDetailZ].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                                worksheet.Cells["A" + _startRowDetailZ + ":F" + _endRowDetailZ].Style.Border.Right.Style = ExcelBorderStyle.Medium;

                                                                incRowExcel++;
                                                            }


                                                        }

                                                    }
                                                }
                                            }
                                        }
                                        incRowExcel++;
                                        using (SqlConnection DbCon2 = new SqlConnection(Tools.conString))
                                        {
                                            {
                                                DbCon2.Open();
                                                using (SqlCommand cmd2 = DbCon2.CreateCommand())
                                                {
                                                    //                                              
                                                    cmd2.CommandText =
                                                    @"
                                                select b.id FundID, b.Name FundName,c.id InstrumentID,a.DoneVolume AmountDeposito, DoneAmount NAVDeposito,a.DoneVolume AmountBond, DoneAmount NAVBond,
                                                a.DoneVolume AmountEquity, DoneAmount NAVEquity,a.DoneVolume TotalInvestment, DoneAmount Amount, DoneAmount NAV, DoneAmount DaysPelanggaranAktif, DoneAmount DaysPelanggaranPasif, DoneAmount TotalAUM,DoneAmount  Under10Bio
                                                 from Investment a left join fund b on a.fundpk = b.fundpk and b.Status in(1,2) 
                                                left join Instrument c on a.InstrumentPK = c.InstrumentPK and c.status in(1,2)
                                                where ValueDate between @ValueDateFrom and @ValueDateTo
                                                ";
                                                    cmd2.CommandTimeout = 0;

                                                    cmd2.Parameters.AddWithValue("@ValueDateFrom", _unitRegistryRpt.ValueDateFrom);
                                                    cmd2.Parameters.AddWithValue("@ValueDateTo", _unitRegistryRpt.ValueDateTo);
                                                    cmd2.Parameters.AddWithValue("@FundFrom", _unitRegistryRpt.FundFrom);
                                                    cmd2.Parameters.AddWithValue("@AgentFrom", _unitRegistryRpt.AgentFrom);
                                                    cmd2.Parameters.AddWithValue("@FundClientFrom", _unitRegistryRpt.FundClientFrom);
                                                    cmd2.Parameters.AddWithValue("@DepartmentFrom", _unitRegistryRpt.DepartmentFrom);

                                                    cmd2.ExecuteNonQuery();


                                                    using (SqlDataReader dr2 = cmd2.ExecuteReader())
                                                    {
                                                        if (!dr2.HasRows)
                                                        {
                                                            return false;
                                                        }
                                                        else
                                                        {


                                                            // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                                            using (ExcelPackage package1 = new ExcelPackage(excelFile))
                                                            {

                                                                //ATUR DATA GROUPINGNYA DULU
                                                                List<CounterFundUnder10BioRpt> rList2 = new List<CounterFundUnder10BioRpt>();
                                                                while (dr2.Read())
                                                                {
                                                                    CounterFundUnder10BioRpt rSingle2 = new CounterFundUnder10BioRpt();
                                                                    rSingle2.FundID = Convert.ToString(dr2["FundID"]);
                                                                    rSingle2.FundName = Convert.ToString(dr2["FundName"]);
                                                                    rSingle2.TotalAUM = Convert.ToDecimal(dr2["TotalAUM"]);
                                                                    rSingle2.Under10Bio = Convert.ToDecimal(dr2["Under10Bio"]);
                                                                    rList2.Add(rSingle2);

                                                                }


                                                                var QueryByFundID2 =
                                                                    from r2 in rList2
                                                                    group r2 by new { } into rGroup2
                                                                    select rGroup2;

                                                                int _endRowDetailZZ = 0;
                                                                int _startRowDetailZZ = incRowExcel;

                                                                foreach (var rsHeader2 in QueryByFundID2)
                                                                {
                                                                    worksheet.Cells[incRowExcel, 1].Value = "ID";
                                                                    worksheet.Cells[incRowExcel, 2].Value = "Fund";
                                                                    worksheet.Cells[incRowExcel, 3].Value = "Total AUM";
                                                                    worksheet.Cells[incRowExcel, 4].Value = "Under 10 Bio";
                                                                    worksheet.Cells["A" + incRowExcel + ":D" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                                    worksheet.Cells["A" + incRowExcel + ":D" + incRowExcel].Style.Font.Bold = true;

                                                                    incRowExcel++;

                                                                    //area header
                                                                    foreach (var rsDetail2 in rsHeader2)
                                                                    {
                                                                        worksheet.Cells[incRowExcel, 1].Value = rsDetail2.FundID;
                                                                        worksheet.Cells[incRowExcel, 2].Value = rsDetail2.FundName;
                                                                        worksheet.Cells[incRowExcel, 3].Value = rsDetail2.TotalAUM;
                                                                        worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "#,##0.00";
                                                                        worksheet.Cells[incRowExcel, 4].Value = rsDetail2.Under10Bio + " days";

                                                                        _endRowDetailZZ = incRowExcel;
                                                                        incRowExcel++;

                                                                    }


                                                                    worksheet.Cells["A" + _startRowDetailZZ + ":D" + _endRowDetailZZ].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                                    worksheet.Cells["A" + _startRowDetailZZ + ":D" + _endRowDetailZZ].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                                    worksheet.Cells["A" + _startRowDetailZZ + ":D" + _endRowDetailZZ].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                                    worksheet.Cells["A" + _startRowDetailZZ + ":D" + _endRowDetailZZ].Style.Border.Right.Style = ExcelBorderStyle.Medium;


                                                                    incRowExcel++;
                                                                }


                                                            }

                                                        }
                                                    }
                                                }
                                            }

                                            int _lastRow = incRowExcel;

                                            worksheet.Row(incRowExcel).PageBreak = _unitRegistryRpt.PageBreak;
                                            int _Row = incRowExcel + 2;
                                            string _rangeA = "A:T" + _Row;
                                            using (ExcelRange r = worksheet.Cells[_rangeA]) // KALO  KOLOM 1 SAMPE 9 A-I
                                            {
                                                r.Style.Font.Size = 12;
                                            }

                                            worksheet.DeleteRow(_lastRow);

                                            worksheet.PrinterSettings.FitToPage = true;
                                            worksheet.PrinterSettings.FitToWidth = 1;
                                            worksheet.PrinterSettings.FitToHeight = 0;
                                            worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel - 1, 9];
                                            worksheet.Column(1).Width = 15;
                                            worksheet.Column(2).Width = 75;
                                            worksheet.Column(3).Width = 25;
                                            worksheet.Column(4).Width = 25;
                                            worksheet.Column(5).Width = 25;
                                            worksheet.Column(6).Width = 25;
                                            worksheet.Column(7).Width = 25;
                                            worksheet.Column(8).Width = 25;
                                            worksheet.Column(9).Width = 35;

                                            // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                            //worksheet.Cells["A3:I14"].AutoFitColumns(); // UNTUK AUTO FIT UKURAN TULISAN DENGAN KOLOM, KEBETULAN INI BUAT HEADER AJA DIPAKENYA
                                            //worksheetApproved.Cells["A3:E3"].AutoFitColumns();
                                            worksheet.PrinterSettings.Orientation = eOrientation.Landscape; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                            worksheet.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN

                                            //worksheet.HeaderFooter.OddHeader.CenteredText = "\n \n \n \n \n \n \n &30&B Daily Total Transaction Report For All \n &28&B Subscription , Redemption , Switching";



                                            // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                            worksheet.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
                                            worksheet.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
                                            worksheet.View.PageLayoutView = Tools.DefaultReportPageLayoutView();
                                            worksheet.View.ShowGridLines = Tools.DefaultReportShowGridLines();

                                            Image img = Image.FromFile(Tools.ReportImage);
                                            Image thumb = img.GetThumbnailImage(Tools.imgWidth, Tools.imgHeight, null, IntPtr.Zero);
                                            worksheet.HeaderFooter.OddHeader.InsertPicture(img, PictureAlignment.Left);
                                            //worksheet.HeaderFooter.OddHeader.CenteredText = Tools.DefaultReportHeaderCenterText();
                                            //worksheet.HeaderFooter.OddHeader.LeftAlignedText = Tools.DefaultReportHeaderLeftText();

                                            worksheet.HeaderFooter.OddFooter.RightAlignedText =
                                            string.Format("Page {0} of {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);

                                            ////worksheet.HeaderFooter.OddFooter.CenteredText = Tools.DefaultReportFooterCenterText();
                                            //worksheet.HeaderFooter.OddFooter.LeftAlignedText = Tools.DefaultReportFooterLeftText();



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
                }
                catch (Exception err)
                {
                    return false;
                    throw err;
                }

            }
            #endregion

            #region Weekly Sales Report
            if (_unitRegistryRpt.ReportName.Equals("Weekly Sales Report"))
            {
                try
                {
                    int _rowtotal = 1;
                    int _counter = 1;
                    int _counterRow = 1;
                    int _RowBaris = 1;
                    int[] IsiCol;
                    int[] IsiRow;

                    #region DIRECT 

                    using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                    {
                        DbCon.Open();
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {



                            cmd.CommandText = @"

--DROP TABLE #tableAgent 
--DROP TABLE #tableAgentClientSubs
--DROP TABLE #tableAgentClientFCP
--DROP TABLE #tableAgentClientFCPHaveAumOnly
--DROP TABLE #tableAgentClientSubsDetail
--DROP TABLE #tableAgentClientRedDetail
--DROP TABLE #tableAgentFCPAum
--DROP TABLE #tableResult
--DROP TABLE #tableResultSubs
--DROP TABLE #tableResultRed

DECLARE @DateToMinOne DATETIME

SET @DateToMinOne = dbo.FWorkingDay(@DateTo,-1)


CREATE table #tableAgent 
(
	AgentPK int
)
CREATE CLUSTERED INDEX indx_tableAgent ON #tableAgent (AgentPK);


INSERT INTO #tableAgent
        ( AgentPK )
SELECT DISTINCT SellingAgentPK FROM FundClient WHERE status IN (1,2)
AND SellingAgentPK <> 0


CREATE table #tableAgentClientSubs 
(
	AgentPK INT,
	FundClientPK int
)
CREATE CLUSTERED INDEX indx_AgentClientSubs   ON #tableAgentClientSubs  (AgentPK,FundClientPK);
INSERT INTO #tableAgentClientSubs
        ( AgentPK, FundClientPK )
SELECT DISTINCT B.SellingAgentPK,A.FundClientPK FROM dbo.ClientSubscription A 
LEFT JOIN FundClient B ON A.FundClientPK = B.FundClientPK AND B.status IN (1,2)
WHERE A.Posted = 1 
AND A.Status <> 3 AND A.Revised = 0
AND A.NAVDate BETWEEN @DateFrom AND @DateTo
AND B.SellingAgentPK <> 0 


CREATE table #tableAgentClientFCP
(
	AgentPK INT,
	FundClientPK int
)
CREATE CLUSTERED INDEX indx_tableAgentClientFCP  ON #tableAgentClientFCP (AgentPK,FundClientPK);
INSERT INTO #tableAgentClientFCP
        ( AgentPK, FundClientPK )
SELECT Distinct B.SellingAgentPK,A.FundClientPK FROM dbo.FundClientPosition A 
LEFT JOIN FundClient B ON A.FundClientPK = B.FundClientPK AND B.status IN (1,2)
WHERE Date = @DateFrom
AND B.SellingAgentPK <> 0

CREATE table #tableAgentClientFCPHaveAumOnly
(
	AgentPK INT,
	FundClientPK int
)
CREATE CLUSTERED INDEX indx_tableAgentClientFCPHaveAumOnly  ON #tableAgentClientFCPHaveAumOnly (AgentPK,FundClientPK);
INSERT INTO #tableAgentClientFCPHaveAumOnly
        ( AgentPK, FundClientPK )
SELECT Distinct B.SellingAgentPK,A.FundClientPK FROM dbo.FundClientPosition A 
LEFT JOIN FundClient B ON A.FundClientPK = B.FundClientPK AND B.status IN (1,2)
WHERE Date = @DateToMinOne
AND B.SellingAgentPK <> 0 AND A.UnitAmount > 1



CREATE table #tableAgentClientSubsDetail
(
	AgentPK INT,
	FundClientPK INT,
	ClientSubscriptionPK INT,
	Amount NUMERIC(22,4)
)
CREATE CLUSTERED INDEX indx_tableAgentClientSubsDetail  ON #tableAgentClientSubsDetail (AgentPK,FundClientPK,ClientSubscriptionPK);
INSERT INTO #tableAgentClientSubsDetail
        ( AgentPK,FundClientPK, ClientSubscriptionPK,Amount )

SELECT B.SellingAgentPK,A.FundClientPK,A.ClientSubscriptionPK,A.CashAmount FROM dbo.ClientSubscription A 
LEFT JOIN FundClient B ON A.FundClientPK = B.FundClientPK AND B.status IN (1,2)
WHERE A.Posted = 1 
AND A.Status <> 3 AND A.Revised = 0
AND A.NAVDate BETWEEN @DateFrom AND @DateTo
AND B.SellingAgentPK <> 0 



CREATE table #tableAgentClientRedDetail
(
	AgentPK INT,
	ClientRedemptionPK int
)
CREATE CLUSTERED INDEX indx_tableAgentClientRedDetail  ON #tableAgentClientRedDetail (AgentPK,ClientRedemptionPK);
INSERT INTO #tableAgentClientRedDetail
        ( AgentPK, ClientRedemptionPK )

SELECT B.SellingAgentPK,A.ClientRedemptionPK FROM dbo.ClientRedemption A 
LEFT JOIN FundClient B ON A.FundClientPK = B.FundClientPK AND B.status IN (1,2)
WHERE A.Posted = 1 
AND A.Status <> 3 AND A.Revised = 0
AND A.NAVDate BETWEEN @DateFrom AND @DateTo
AND B.SellingAgentPK <> 0 



CREATE table #tableAgentFCPAum
(
	AgentPK INT,
	AUM NUMERIC(22,4)
)
CREATE CLUSTERED INDEX indx_tableAgentFCPAum  ON #tableAgentFCPAum (AgentPK);
INSERT INTO #tableAgentFCPAum
        ( AgentPK, AUM )
SELECT B.SellingAgentPK,SUM(ISNULL(A.UnitAmount,0) * ISNULL(C.Nav,0)) AUM FROM dbo.FundClientPosition A 
LEFT JOIN FundClient B ON A.FundClientPK = B.FundClientPK AND B.status IN (1,2)
LEFT JOIN dbo.CloseNAV C ON A.FundPK = C.FundPK AND C.date = @DateTo  AND C.status IN (1,2)
WHERE A.Date = @DateToMinOne
AND B.SellingAgentPK <> 0 AND A.UnitAmount > 1
GROUP BY B.SellingAgentPK


CREATE table #tableResult
(
	AgentPK INT,
	Type NVARCHAR(100),
	Code NVARCHAR(100),
	AgentName NVARCHAR(300),
	joindate date,
	TotalNewClientThisWeek INT,
	TotalAllClient INT,
	TotalSubs INT,
	TotalRed INT,
	AUM NUMERIC(22,4),
	TotalTopUp int,
	AmountTopUp numeric(22,4)
)
CREATE CLUSTERED INDEX indx_tableResult  ON #tableResult (AgentPK);

INSERT INTO #tableResult
SELECT  
A.AgentPK
,CASE WHEN B.Type = 1 THEN 'DIRECT' ELSE 'INDIRECT' END Type,B.ID Code,B.Name AgentName 
,FORMAT(B.JoinDate,'d-MMM-yy') JoinDate
,ISNULL(C.totalNewClientThisWeek,0) totalNewClientThisWeekE6
,ISNULL(D.TotalAllClient,0) TotalAllClientE7
,ISNULL(E.TotalSubs,0) TotalSubs
,ISNULL(F.TotalRed,0) TotalRed
,ISNULL(G.AUM,0) / 1000000 AUM
,ISNULL(H.TotalTopUp,0) TotalTopUp
,ISNULL(I.AmountTopUp,0) / 1000000 AmountTopUp
FROM #tableAgent A
INNER JOIN Agent B ON A.AgentPK = B.AgentPK AND B.status IN (1,2)
left JOIN
(
	SELECT A.AgentPK,COUNT(A.FundClientPK) totalNewClientThisWeek FROM #tableAgentClientSubs A
	LEFT JOIN #tableAgentClientFCP B ON A.fundClientPK = B.FundClientPK 
	WHERE B.FundClientPK  IS NULL
	GROUP BY A.AgentPK
)C on A.AgentPK = C.AgentPK 
left JOIN
(
	SELECT A.AgentPK,COUNT(A.FundClientPK) TotalAllClient FROM #tableAgentClientFCPHaveAumOnly A
	GROUP BY A.AgentPK
)D on A.AgentPK = D.AgentPK 
left join
(
	Select AgentPK,count(ClientsubscriptionPK) totalSubs from #tableAgentClientSubsDetail
	group By AgentPK
)E on A.AgentPK = E.AgentPK
left join
(
	Select AgentPK,count(ClientRedemptionPK) totalRed from #tableAgentClientRedDetail
	group By AgentPK
)F on A.AgentPK = F.AgentPK
left join
(
	Select * from #tableAgentFCPAum
)G on A.AgentPK = G.AgentPK
left JOIN
(
	SELECT A.AgentPK,COUNT(A.ClientsubscriptionPK) TotalTopUp FROM #tableAgentClientSubsDetail A
	INNER JOIN #tableAgentClientFCP B ON A.fundClientPK = B.FundClientPK 
	GROUP BY A.AgentPK
)H on A.AgentPK = H.AgentPK 
left JOIN
(
	SELECT A.AgentPK,SUM(ISNULL(Amount,0)) AmountTopUp FROM #tableAgentClientSubsDetail A
	INNER JOIN #tableAgentClientFCP B ON A.fundClientPK = B.FundClientPK 
	GROUP BY A.AgentPK
)I on A.AgentPK = I.AgentPK 





DECLARE @cols AS NVARCHAR(MAX),@colsForQuery AS NVARCHAR(MAX),@colsRed AS NVARCHAR(MAX),@colsForQueryRed AS NVARCHAR(MAX),
    @query  AS NVARCHAR(MAX)

select @colsForQuery = STUFF((SELECT ',isnull(' + QUOTENAME(ID) +',0) ' + QUOTENAME(ID) 
                    from (SELECT DISTINCT C.ID FROM dbo.ClientSubscription A 
							LEFT JOIN FundClient B ON A.FundClientPK = B.FundClientPK AND B.status IN (1,2)
							left join Fund C on A.FundPK = C.FundPK and C.status in (1,2)
							WHERE A.Posted = 1 
							AND A.Status <> 3 AND A.Revised = 0
							AND A.NAVDate BETWEEN @DateFrom AND @DateTo
							AND B.SellingAgentPK <> 0 ) A
					order by A.ID
            FOR XML PATH(''), TYPE
            ).value('.', 'NVARCHAR(MAX)') 
        ,1,1,'')

select @cols = STUFF((SELECT distinct ',' + QUOTENAME(C.ID) 
                    FROM dbo.ClientSubscription A 
							LEFT JOIN FundClient B ON A.FundClientPK = B.FundClientPK AND B.status IN (1,2)
							left join Fund C on A.FundPK = C.FundPK and C.status in (1,2)
							WHERE A.Posted = 1 
							AND A.Status <> 3 AND A.Revised = 0
							AND A.NAVDate BETWEEN @DateFrom AND @DateTo
							AND B.SellingAgentPK <> 0
				
            FOR XML PATH(''), TYPE
            ).value('.', 'NVARCHAR(MAX)') 
        ,1,1,'')



CREATE table #tableResultSubs
(

	SellingAgentPK INT,
	ID NVARCHAR(200),
	Subs NUMERIC(22,4)
)
INSERT INTO #tableResultSubs
        ( SellingAgentPK, ID, Subs )

SELECT B.SellingAgentPK,C.ID ,A.CashAmount/1000000 Subs FROM dbo.ClientSubscription A 
					LEFT JOIN FundClient B ON A.FundClientPK = B.FundClientPK AND B.status IN (1,2)
					left join Fund C on A.FundPK = C.FundPK and C.status in (1,2)
					WHERE A.Posted = 1 
					AND A.Status <> 3 AND A.Revised = 0
					AND A.NAVDate BETWEEN @DateFrom AND @DateTo
					AND B.SellingAgentPK <> 0 


CREATE table #tableResultRed
(

	SellingAgentPK INT,
	ID NVARCHAR(200),
	Red NUMERIC(22,4)
)
INSERT INTO #tableResultRed
        ( SellingAgentPK, ID, Red )

SELECT B.SellingAgentPK,C.ID ,A.CashAmount/1000000 Red FROM dbo.ClientRedemption A 
					LEFT JOIN FundClient B ON A.FundClientPK = B.FundClientPK AND B.status IN (1,2)
					left join Fund C on A.FundPK = C.FundPK and C.status in (1,2)
					WHERE A.Posted = 1 
					AND A.Status <> 3 AND A.Revised = 0
					AND A.NAVDate BETWEEN @DateFrom AND @DateTo
					AND B.SellingAgentPK <> 0 


SELECT B.Type,B.Code,B.Agentname,B.JoinDate,B.TotalNewClientThisWeek,B.TotalAllClient,B.TotalSubs,B.TotalRed
			,B.AUM,B.TotalTopUp,B.AmountTopUp,'''' Remark
			from #tableResult B
			where B.AgentPK in (select * from 
					(select distinct SellingAgentPK from #tableResultRed
					union all
					select distinct SellingAgentPK from #tableResultSubs) A
					group by A.SellingAgentPK)
					and B.Type = 'DIRECT'
			order by B.AgentPK asc	

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
                                    int _rowAkhir = 0;
                                    int _rowAkhir2 = 0;
                                    string filePath = Tools.ReportsPath + "WeeklySalesReport" + "_" + _userID + ".xlsx";
                                    string pdfPath = Tools.ReportsPath + "WeeklySalesReport" + "_" + _userID + ".pdf";
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

                                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("DIRECT");
                                        ExcelWorksheet worksheet1 = package.Workbook.Worksheets.Add("IN DIRECT");


                                        //ATUR DATA GROUPINGNYA DULU
                                        List<WeeklySalesReport> rList = new List<WeeklySalesReport>();
                                        while (dr0.Read())
                                        {

                                            WeeklySalesReport rSingle = new WeeklySalesReport();
                                            rSingle.Type = Convert.ToString(dr0["Type"]);
                                            rSingle.Code = Convert.ToString(dr0["Code"]);
                                            rSingle.AgentName = Convert.ToString(dr0["AgentName"]);
                                            rSingle.JoinDate = Convert.ToDateTime(dr0["JoinDate"]);
                                            rSingle.TotalNewClientThisWeek = Convert.ToDecimal(dr0["TotalNewClientThisWeek"]);
                                            rSingle.TotalAllClient = Convert.ToDecimal(dr0["TotalAllClient"]);
                                            rSingle.TotalSubs = Convert.ToDecimal(dr0["TotalSubs"]);
                                            rSingle.TotalRed = Convert.ToDecimal(dr0["TotalRed"]);
                                            rSingle.AUM = Convert.ToDecimal(dr0["AUM"]);
                                            rSingle.TotalTopUp = Convert.ToDecimal(dr0["TotalTopUp"]);
                                            rSingle.AmountTopUp = Convert.ToDecimal(dr0["AmountTopUp"]);
                                            rSingle.Remark = Convert.ToString(dr0["Remark"]);
                                            rList.Add(rSingle);

                                        }



                                        var GroupByReference =
                                                from r in rList
                                                    //orderby r.Type ascending
                                                group r by new { } into rGroup
                                                select rGroup;

                                        int incRowExcel = 0;






                                        foreach (var rsHeader in GroupByReference)
                                        {
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 1].Value = "PT AYERS ASIA ASSET MANAGEMENT";
                                            worksheet.Cells["A" + incRowExcel + ":I" + incRowExcel].Merge = true;
                                            worksheet.Cells["A" + incRowExcel + ":I" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 1].Value = "WEEKLY MARKETING PERFORMANCE - (AYERS INTERNAL MARKETING)";
                                            worksheet.Cells["A" + incRowExcel + ":I" + incRowExcel].Merge = true;
                                            worksheet.Cells["A" + incRowExcel + ":I" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 1].Value = "PERIOD " + Convert.ToDateTime(_unitRegistryRpt.ValueDateFrom).ToString("dd-MMM-yyyy") + " - " + Convert.ToDateTime(_unitRegistryRpt.ValueDateTo).ToString("dd-MMM-yyyy");
                                            worksheet.Cells["A" + incRowExcel + ":I" + incRowExcel].Merge = true;
                                            worksheet.Cells["A" + incRowExcel + ":I" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Value = "Type : DIRECT ";
                                            incRowExcel++;

                                            int RowB = incRowExcel;
                                            int RowG = incRowExcel + 1;


                                            worksheet.Cells[incRowExcel, 1].Value = "NO";
                                            worksheet.Cells["A" + RowB + ":A" + RowG].Merge = true;
                                            worksheet.Cells["A" + RowB + ":A" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["A" + RowB + ":A" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 2].Value = "MARKETING";
                                            worksheet.Cells["B" + RowB + ":D" + RowB].Merge = true;
                                            worksheet.Cells["B" + RowB + ":D" + RowB].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["B" + RowB + ":D" + RowB].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[RowG, 2].Value = "CODE";
                                            worksheet.Cells["B" + RowG + ":B" + RowG].Merge = true;
                                            worksheet.Cells["B" + RowG + ":B" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["B" + RowG + ":B" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[RowG, 3].Value = "NAME";
                                            worksheet.Cells["C" + RowG + ":C" + RowG].Merge = true;
                                            worksheet.Cells["C" + RowG + ":C" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["C" + RowG + ":C" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[RowG, 4].Value = "JOIN DATE";
                                            worksheet.Cells["D" + RowG + ":D" + RowG].Merge = true;
                                            worksheet.Cells["D" + RowG + ":D" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["D" + RowG + ":D" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 5].Value = "NEW CLIENT";
                                            worksheet.Cells["E" + RowB + ":E" + RowB].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[RowG, 5].Value = "This Week";
                                            worksheet.Cells["E" + RowG + ":E" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[RowG, 5].Value = "Accumulate";
                                            worksheet.Cells["E" + RowG + ":E" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 6].Value = "No of Transaction";
                                            worksheet.Cells["F" + RowB + ":F" + RowG].Merge = true;
                                            worksheet.Cells["F" + RowB + ":F" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["F" + RowB + ":F" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 7].Value = "LATEST AUM";
                                            worksheet.Cells["G" + RowB + ":G" + RowB].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[RowG, 7].Value = "(Amount Per Mil Rupiah)";
                                            worksheet.Cells["G" + RowG + ":G" + RowG].Merge = true;
                                            worksheet.Cells["G" + RowG + ":G" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["G" + RowG + ":G" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 8].Value = "TOP UP";
                                            worksheet.Cells["H" + RowB + ":H" + RowB].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;



                                            worksheet.Cells[RowG, 8].Value = "No. of Transaction";
                                            worksheet.Cells["H" + RowG + ":H" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[RowG, 8].Value = "Total Amount";
                                            worksheet.Cells["H" + RowG + ":H" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 9].Value = "REMARK";
                                            worksheet.Cells["I" + RowB + ":I" + RowG].Merge = true;
                                            worksheet.Cells["I" + RowB + ":I" + RowG].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                            worksheet.Cells["I" + RowB + ":I" + RowG].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;



                                            worksheet.Cells["A" + RowB + ":I" + RowG].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells["A" + RowB + ":I" + RowG].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells["A" + RowB + ":I" + RowG].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells["A" + RowB + ":I" + RowG].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                                            incRowExcel = incRowExcel + 3;

                                            int first = incRowExcel;

                                            int _no = 1;
                                            int _startRowDetail = incRowExcel;
                                            int _endRowDetail = 0;

                                            int RowC = incRowExcel;
                                            int RowF = incRowExcel + 1;


                                            foreach (var rsDetail in rsHeader)
                                            {
                                                worksheet.Cells["A" + incRowExcel + ":I" + RowF].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells["A" + incRowExcel + ":I" + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells["A" + incRowExcel + ":I" + RowF].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                worksheet.Cells["A" + incRowExcel + ":I" + RowF].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                                                worksheet.Cells[incRowExcel, 1].Value = _no;
                                                worksheet.Cells["A" + incRowExcel + ":A" + RowF].Merge = true;
                                                worksheet.Cells["A" + incRowExcel + ":A" + RowF].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells["A" + incRowExcel + ":A" + RowF].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 2].Value = rsDetail.Code;
                                                worksheet.Cells["B" + incRowExcel + ":B" + RowF].Merge = true;
                                                worksheet.Cells["B" + incRowExcel + ":B" + RowF].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells["B" + incRowExcel + ":B" + RowF].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.AgentName;
                                                worksheet.Cells["C" + incRowExcel + ":C" + RowF].Merge = true;
                                                worksheet.Cells["C" + incRowExcel + ":C" + RowF].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells["C" + incRowExcel + ":C" + RowF].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 4].Value = rsDetail.JoinDate.ToString("dd/MM/yyyy");
                                                worksheet.Cells["D" + incRowExcel + ":D" + RowF].Merge = true;
                                                worksheet.Cells["D" + incRowExcel + ":D" + RowF].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells["D" + incRowExcel + ":D" + RowF].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                worksheet.Cells[incRowExcel, 5].Value = rsDetail.TotalNewClientThisWeek;
                                                worksheet.Cells["E" + incRowExcel + ":E" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                                                worksheet.Cells[RowF, 5].Value = rsDetail.TotalAllClient;
                                                worksheet.Cells["E" + RowF + ":E" + RowF].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                                                worksheet.Cells[incRowExcel, 6].Value = rsDetail.TotalSubs;
                                                worksheet.Cells["F" + incRowExcel + ":F" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                                                worksheet.Cells[RowF, 6].Value = rsDetail.TotalRed;
                                                worksheet.Cells["F" + RowF + ":F" + RowF].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                                                worksheet.Cells[incRowExcel, 7].Value = rsDetail.AUM;
                                                worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells["G" + incRowExcel + ":G" + RowF].Merge = true;
                                                worksheet.Cells["G" + incRowExcel + ":G" + RowF].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells["G" + incRowExcel + ":G" + RowF].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                                                worksheet.Cells[incRowExcel, 8].Value = rsDetail.TotalTopUp;
                                                worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells["H" + incRowExcel + ":H" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                                                worksheet.Cells[RowF, 8].Value = rsDetail.AmountTopUp;
                                                worksheet.Cells[RowF, 8].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells["H" + RowF + ":H" + RowF].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;


                                                worksheet.Cells[incRowExcel, 9].Value = rsDetail.Remark;
                                                worksheet.Cells["I" + incRowExcel + ":I" + RowF].Merge = true;
                                                worksheet.Cells["I" + incRowExcel + ":I" + RowF].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                worksheet.Cells["I" + incRowExcel + ":I" + RowF].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                _rowtotal++;
                                                _no++;
                                                _endRowDetail = incRowExcel;
                                                incRowExcel++;
                                                incRowExcel++;
                                                _RowBaris = incRowExcel;

                                                RowF = incRowExcel + 1;


                                            }


                                        }

                                        #endregion

                                        #region DIRECT SUBS
                                        // SUBS
                                        using (SqlConnection DbCon1 = new SqlConnection(Tools.conString))
                                        {
                                            DbCon1.Open();
                                            using (SqlCommand cmd1 = DbCon1.CreateCommand())
                                            {
                                                cmd1.CommandText =

                                                @"  
                                                --DROP TABLE #tableAgent 
                                                --DROP TABLE #tableAgentClientSubs
                                                --DROP TABLE #tableAgentClientFCP
                                                --DROP TABLE #tableAgentClientFCPHaveAumOnly
                                                --DROP TABLE #tableAgentClientSubsDetail
                                                --DROP TABLE #tableAgentClientRedDetail
                                                --DROP TABLE #tableAgentFCPAum
                                                --DROP TABLE #tableResult
                                                --DROP TABLE #tableResultSubs
                                                --DROP TABLE #tableResultRed

                                                DECLARE @DateToMinOne DATETIME

                                                SET @DateToMinOne = dbo.FWorkingDay(@DateTo,-1)


                                                CREATE table #tableAgent 
                                                (
	                                                AgentPK int
                                                )
                                                CREATE CLUSTERED INDEX indx_tableAgent ON #tableAgent (AgentPK);


                                                INSERT INTO #tableAgent
                                                        ( AgentPK )
                                                SELECT DISTINCT SellingAgentPK FROM FundClient WHERE status IN (1,2)
                                                AND SellingAgentPK <> 0


                                                CREATE table #tableAgentClientSubs 
                                                (
	                                                AgentPK INT,
	                                                FundClientPK int
                                                )
                                                CREATE CLUSTERED INDEX indx_AgentClientSubs   ON #tableAgentClientSubs  (AgentPK,FundClientPK);
                                                INSERT INTO #tableAgentClientSubs
                                                        ( AgentPK, FundClientPK )
                                                SELECT DISTINCT B.SellingAgentPK,A.FundClientPK FROM dbo.ClientSubscription A 
                                                LEFT JOIN FundClient B ON A.FundClientPK = B.FundClientPK AND B.status IN (1,2)
                                                WHERE A.Posted = 1 
                                                AND A.Status <> 3 AND A.Revised = 0
                                                AND A.NAVDate BETWEEN @DateFrom AND @DateTo
                                                AND B.SellingAgentPK <> 0 


                                                CREATE table #tableAgentClientFCP
                                                (
	                                                AgentPK INT,
	                                                FundClientPK int
                                                )
                                                CREATE CLUSTERED INDEX indx_tableAgentClientFCP  ON #tableAgentClientFCP (AgentPK,FundClientPK);
                                                INSERT INTO #tableAgentClientFCP
                                                        ( AgentPK, FundClientPK )
                                                SELECT Distinct B.SellingAgentPK,A.FundClientPK FROM dbo.FundClientPosition A 
                                                LEFT JOIN FundClient B ON A.FundClientPK = B.FundClientPK AND B.status IN (1,2)
                                                WHERE Date = @DateFrom
                                                AND B.SellingAgentPK <> 0

                                                CREATE table #tableAgentClientFCPHaveAumOnly
                                                (
	                                                AgentPK INT,
	                                                FundClientPK int
                                                )
                                                CREATE CLUSTERED INDEX indx_tableAgentClientFCPHaveAumOnly  ON #tableAgentClientFCPHaveAumOnly (AgentPK,FundClientPK);
                                                INSERT INTO #tableAgentClientFCPHaveAumOnly
                                                        ( AgentPK, FundClientPK )
                                                SELECT Distinct B.SellingAgentPK,A.FundClientPK FROM dbo.FundClientPosition A 
                                                LEFT JOIN FundClient B ON A.FundClientPK = B.FundClientPK AND B.status IN (1,2)
                                                WHERE Date = @DateToMinOne
                                                AND B.SellingAgentPK <> 0 AND A.UnitAmount > 1



                                                CREATE table #tableAgentClientSubsDetail
                                                (
	                                                AgentPK INT,
	                                                FundClientPK INT,
	                                                ClientSubscriptionPK INT,
	                                                Amount NUMERIC(22,4)
                                                )
                                                CREATE CLUSTERED INDEX indx_tableAgentClientSubsDetail  ON #tableAgentClientSubsDetail (AgentPK,FundClientPK,ClientSubscriptionPK);
                                                INSERT INTO #tableAgentClientSubsDetail
                                                        ( AgentPK,FundClientPK, ClientSubscriptionPK,Amount )

                                                SELECT B.SellingAgentPK,A.FundClientPK,A.ClientSubscriptionPK,A.CashAmount FROM dbo.ClientSubscription A 
                                                LEFT JOIN FundClient B ON A.FundClientPK = B.FundClientPK AND B.status IN (1,2)
                                                WHERE A.Posted = 1 
                                                AND A.Status <> 3 AND A.Revised = 0
                                                AND A.NAVDate BETWEEN @DateFrom AND @DateTo
                                                AND B.SellingAgentPK <> 0 



                                                CREATE table #tableAgentClientRedDetail
                                                (
	                                                AgentPK INT,
	                                                ClientRedemptionPK int
                                                )
                                                CREATE CLUSTERED INDEX indx_tableAgentClientRedDetail  ON #tableAgentClientRedDetail (AgentPK,ClientRedemptionPK);
                                                INSERT INTO #tableAgentClientRedDetail
                                                        ( AgentPK, ClientRedemptionPK )

                                                SELECT B.SellingAgentPK,A.ClientRedemptionPK FROM dbo.ClientRedemption A 
                                                LEFT JOIN FundClient B ON A.FundClientPK = B.FundClientPK AND B.status IN (1,2)
                                                WHERE A.Posted = 1 
                                                AND A.Status <> 3 AND A.Revised = 0
                                                AND A.NAVDate BETWEEN @DateFrom AND @DateTo
                                                AND B.SellingAgentPK <> 0 



                                                CREATE table #tableAgentFCPAum
                                                (
	                                                AgentPK INT,
	                                                AUM NUMERIC(22,4)
                                                )
                                                CREATE CLUSTERED INDEX indx_tableAgentFCPAum  ON #tableAgentFCPAum (AgentPK);
                                                INSERT INTO #tableAgentFCPAum
                                                        ( AgentPK, AUM )
                                                SELECT B.SellingAgentPK,SUM(ISNULL(A.UnitAmount,0) * ISNULL(C.Nav,0)) AUM FROM dbo.FundClientPosition A 
                                                LEFT JOIN FundClient B ON A.FundClientPK = B.FundClientPK AND B.status IN (1,2)
                                                LEFT JOIN dbo.CloseNAV C ON A.FundPK = C.FundPK AND C.date = @DateTo  AND C.status IN (1,2)
                                                WHERE A.Date = @DateToMinOne
                                                AND B.SellingAgentPK <> 0 AND A.UnitAmount > 1
                                                GROUP BY B.SellingAgentPK


                                                CREATE table #tableResult
                                                (
	                                                AgentPK INT,
	                                                Type NVARCHAR(100),
	                                                Code NVARCHAR(100),
	                                                AgentName NVARCHAR(300),
	                                                TotalNewClientThisWeek INT,
	                                                TotalAllClient INT,
	                                                TotalSubs INT,
	                                                TotalRed INT,
	                                                AUM NUMERIC(22,4),
	                                                TotalTopUp int,
	                                                AmountTopUp numeric(22,4)
                                                )
                                                CREATE CLUSTERED INDEX indx_tableResult  ON #tableResult (AgentPK);

                                                INSERT INTO #tableResult
                                                SELECT  
                                                A.AgentPK
                                                ,CASE WHEN B.Type = 1 THEN 'DIRECT' ELSE 'INDIRECT' END Type,B.ID Code,B.Name AgentName 
                                                --,FORMAT(B.JoinDate,''d-MMM-yy''
                                                ,ISNULL(C.totalNewClientThisWeek,0) totalNewClientThisWeekE6
                                                ,ISNULL(D.TotalAllClient,0) TotalAllClientE7
                                                ,ISNULL(E.TotalSubs,0) TotalSubs
                                                ,ISNULL(F.TotalRed,0) TotalRed
                                                ,ISNULL(G.AUM,0) / 1000000 AUM
                                                ,ISNULL(H.TotalTopUp,0) TotalTopUp
                                                ,ISNULL(I.AmountTopUp,0) / 1000000 AmountTopUp
                                                FROM #tableAgent A
                                                INNER JOIN Agent B ON A.AgentPK = B.AgentPK AND B.status IN (1,2)
                                                left JOIN
                                                (
	                                                SELECT A.AgentPK,COUNT(A.FundClientPK) totalNewClientThisWeek FROM #tableAgentClientSubs A
	                                                LEFT JOIN #tableAgentClientFCP B ON A.fundClientPK = B.FundClientPK 
	                                                WHERE B.FundClientPK  IS NULL
	                                                GROUP BY A.AgentPK
                                                )C on A.AgentPK = C.AgentPK 
                                                left JOIN
                                                (
	                                                SELECT A.AgentPK,COUNT(A.FundClientPK) TotalAllClient FROM #tableAgentClientFCPHaveAumOnly A
	                                                GROUP BY A.AgentPK
                                                )D on A.AgentPK = D.AgentPK 
                                                left join
                                                (
	                                                Select AgentPK,count(ClientsubscriptionPK) totalSubs from #tableAgentClientSubsDetail
	                                                group By AgentPK
                                                )E on A.AgentPK = E.AgentPK
                                                left join
                                                (
	                                                Select AgentPK,count(ClientRedemptionPK) totalRed from #tableAgentClientRedDetail
	                                                group By AgentPK
                                                )F on A.AgentPK = F.AgentPK
                                                left join
                                                (
	                                                Select * from #tableAgentFCPAum
                                                )G on A.AgentPK = G.AgentPK
                                                left JOIN
                                                (
	                                                SELECT A.AgentPK,COUNT(A.ClientsubscriptionPK) TotalTopUp FROM #tableAgentClientSubsDetail A
	                                                INNER JOIN #tableAgentClientFCP B ON A.fundClientPK = B.FundClientPK 
	                                                GROUP BY A.AgentPK
                                                )H on A.AgentPK = H.AgentPK 
                                                left JOIN
                                                (
	                                                SELECT A.AgentPK,SUM(ISNULL(Amount,0)) AmountTopUp FROM #tableAgentClientSubsDetail A
	                                                INNER JOIN #tableAgentClientFCP B ON A.fundClientPK = B.FundClientPK 
	                                                GROUP BY A.AgentPK
                                                )I on A.AgentPK = I.AgentPK 
												where B.Type = 1






                                                DECLARE @cols AS NVARCHAR(MAX),@colsForQuery AS NVARCHAR(MAX),@colsRed AS NVARCHAR(MAX),@colsForQueryRed AS NVARCHAR(MAX),
                                                    @querySubs  AS NVARCHAR(MAX)

                                                select @colsForQuery = STUFF((SELECT ',isnull(' + QUOTENAME(ID) +',0) ' + QUOTENAME(ID) 
                                                                    from (SELECT DISTINCT C.ID FROM dbo.ClientSubscription A 
							                                                LEFT JOIN FundClient B ON A.FundClientPK = B.FundClientPK AND B.status IN (1,2)
							                                                left join Fund C on A.FundPK = C.FundPK and C.status in (1,2)
							                                                WHERE A.Posted = 1 
							                                                AND A.Status <> 3 AND A.Revised = 0
							                                                AND A.NAVDate BETWEEN @DateFrom AND @DateTo
							                                                AND B.SellingAgentPK <> 0 ) A
					                                                order by A.ID
                                                            FOR XML PATH(''), TYPE
                                                            ).value('.', 'NVARCHAR(MAX)') 
                                                        ,1,1,'')

                                                select @cols = STUFF((SELECT distinct ',' + QUOTENAME(C.ID) 
                                                                    FROM dbo.ClientSubscription A 
							                                                LEFT JOIN FundClient B ON A.FundClientPK = B.FundClientPK AND B.status IN (1,2)
							                                                left join Fund C on A.FundPK = C.FundPK and C.status in (1,2)
							                                                WHERE A.Posted = 1 
							                                                AND A.Status <> 3 AND A.Revised = 0
							                                                AND A.NAVDate BETWEEN @DateFrom AND @DateTo
							                                                AND B.SellingAgentPK <> 0
				
                                                            FOR XML PATH(''), TYPE
                                                            ).value('.', 'NVARCHAR(MAX)') 
                                                        ,1,1,'')



                                                --select @colsForQueryRed = STUFF((SELECT ',isnull(O.' + QUOTENAME(ID) +',0) ' + QUOTENAME('RED'+ID) 
                                                --                    from (SELECT DISTINCT C.ID FROM dbo.ClientRedemption A 
                                                --							LEFT JOIN FundClient B ON A.FundClientPK = B.FundClientPK AND B.status IN (1,2)
                                                --							left join Fund C on A.FundPK = C.FundPK and C.status in (1,2)
                                                --							WHERE A.Posted = 1 
                                                --							AND A.Status <> 3 AND A.Revised = 0
                                                --							AND A.NAVDate BETWEEN @DateFrom AND @DateTo
                                                --							AND B.SellingAgentPK <> 0 ) A
                                                --					order by A.ID
                                                --            FOR XML PATH(''), TYPE
                                                --            ).value('.', 'NVARCHAR(MAX)') 
                                                --        ,1,1,'')

                                                --select @colsRed = STUFF((SELECT distinct ',O.' + QUOTENAME(C.ID) 
                                                --                    FROM dbo.ClientRedemption A 
                                                --							LEFT JOIN FundClient B ON A.FundClientPK = B.FundClientPK AND B.status IN (1,2)
                                                --							left join Fund C on A.FundPK = C.FundPK and C.status in (1,2)
                                                --							WHERE A.Posted = 1 
                                                --							AND A.Status <> 3 AND A.Revised = 0
                                                --							AND A.NAVDate BETWEEN @DateFrom AND @DateTo
                                                --							AND B.SellingAgentPK <> 0
				
                                                --            FOR XML PATH(''), TYPE
                                                --            ).value('.', 'NVARCHAR(MAX)') 
                                                --        ,1,1,'')



                                                CREATE table #tableResultSubs
                                                (

	                                                SellingAgentPK INT,
	                                                ID NVARCHAR(200),
	                                                Subs NUMERIC(22,4)
                                                )
                                                INSERT INTO #tableResultSubs
                                                        ( SellingAgentPK, ID, Subs )

                                                SELECT B.SellingAgentPK,C.ID ,A.CashAmount/1000000 Subs FROM dbo.ClientSubscription A 
					                                                LEFT JOIN FundClient B ON A.FundClientPK = B.FundClientPK AND B.status IN (1,2)
					                                                left join Fund C on A.FundPK = C.FundPK and C.status in (1,2)
                                                                    INNER JOIN Agent D ON B.SellingAgentPK = D.AgentPK AND D.status IN (1,2)
					                                                WHERE A.Posted = 1 
					                                                AND A.Status <> 3 AND A.Revised = 0
					                                                AND A.NAVDate BETWEEN @DateFrom AND @DateTo
					                                                AND B.SellingAgentPK <> 0 and D.Type = 1


                                                CREATE table #tableResultRed
                                                (

	                                                SellingAgentPK INT,
	                                                ID NVARCHAR(200),
	                                                Red NUMERIC(22,4)
                                                )
                                                INSERT INTO #tableResultRed
                                                        ( SellingAgentPK, ID, Red )

                                                SELECT B.SellingAgentPK,C.ID ,A.CashAmount/1000000 Red FROM dbo.ClientRedemption A 
					                                                LEFT JOIN FundClient B ON A.FundClientPK = B.FundClientPK AND B.status IN (1,2)
					                                                left join Fund C on A.FundPK = C.FundPK and C.status in (1,2)
                                                                    INNER JOIN Agent D ON B.SellingAgentPK = D.AgentPK AND D.status IN (1,2)
					                                                WHERE A.Posted = 1 
					                                                AND A.Status <> 3 AND A.Revised = 0
					                                                AND A.NAVDate BETWEEN @DateFrom AND @DateTo
					                                                AND B.SellingAgentPK <> 0 and D.Type = 1



                                                set @querySubs = 'SELECT B.Type,B.Code,B.Agentname,  ' + @colsForQuery + ' 
			                                                from 
                                                                (
				                                                SELECT * FROM #tableResultSubs
                                                            ) x
                                                            pivot 
                                                            (
                                                                SUM(Subs)
                                                                for ID in (' + @cols + ')
                                                            ) p
			                                                left join  #tableResult B on SellingAgentPK = B.AgentPK
                                                            where B.Type = ''DIRECT''
			                                                order by SellingAgentPK asc
			                                                '

                                                exec(@querySubs)	";

                                                cmd1.CommandTimeout = 0;
                                                cmd1.Parameters.AddWithValue("@DateFrom", _unitRegistryRpt.ValueDateFrom);
                                                cmd1.Parameters.AddWithValue("@DateTo", _unitRegistryRpt.ValueDateTo);

                                                cmd1.ExecuteNonQuery();


                                                using (SqlDataReader dr1 = cmd1.ExecuteReader())
                                                {
                                                    incRowExcel = 5;
                                                    int incColExcel = 10;


                                                    worksheet.Cells[incRowExcel, incColExcel].Value = "WEEKLY SUBSCRIPTION (Amount per Mil Rupiah)";
                                                    //worksheet.Cells["J" + incRowExcel + ":F" + incColExcel].Merge = true;
                                                    //worksheet.Cells["J" + incRowExcel + ":F" + incColExcel].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                    //worksheet.Cells["J" + incRowExcel + ":F" + incColExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                    incRowExcel++;
                                                    worksheet.Cells[incRowExcel, incColExcel].Value = "WEEKLY REDEMPTION (Amount per Mil Rupiah)";
                                                    incRowExcel++;

                                                    for (int inc1 = 3; inc1 < dr1.FieldCount; inc1++)
                                                    {

                                                        worksheet.Cells[incRowExcel, incColExcel].Value = dr1.GetName(inc1).ToString();
                                                        //worksheet.Cells[incRowExcel, incColExcel].Style.Numberformat.Format = "#,##0.0000";
                                                        worksheet.Cells[incRowExcel, incColExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                        incColExcel++;
                                                    }

                                                    string _alphabet = _host.GetAlphabet(incColExcel - 1);

                                                    worksheet.Cells["A" + incRowExcel + ":" + _alphabet + incColExcel].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                                    worksheet.Cells["A" + incRowExcel + ":" + _alphabet + incColExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                                    worksheet.Cells["A" + incRowExcel + ":" + _alphabet + incColExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                    worksheet.Cells["A" + incRowExcel + ":" + _alphabet + incColExcel].Style.Border.Right.Style = ExcelBorderStyle.Thin;



                                                    incRowExcel++;
                                                    while (dr1.Read())
                                                    {

                                                        int RowB = incRowExcel;
                                                        int RowG = incRowExcel + 1;



                                                        int RowC = incRowExcel;
                                                        int RowF = incRowExcel + 1;
                                                        int set = 8;

                                                        for (int inc2 = 0; inc2 < _rowtotal; inc2++)
                                                        {
                                                            if (worksheet.Cells[set, 3].Value.ToString() == dr1.GetValue(2).ToString())
                                                            {
                                                                _counterRow = set;
                                                                break;
                                                            }
                                                            set = set + 2;
                                                        }

                                                        incColExcel = 10;
                                                        for (int inc1 = 3; inc1 < dr1.FieldCount; inc1++)
                                                        {
                                                            worksheet.Cells[_counterRow, incColExcel].Value = dr1.GetValue(inc1).ToString();
                                                            worksheet.Cells[_counterRow, incColExcel].Style.Numberformat.Format = "#,##0.0000";
                                                            worksheet.Cells[_counterRow, incColExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                                                            incColExcel++;
                                                            _rowAkhir = incColExcel;
                                                        }

                                                        string _alphabetDetail = _host.GetAlphabet(incColExcel - 1);

                                                        worksheet.Cells["A" + incRowExcel + ":" + _alphabetDetail + RowF].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                                        worksheet.Cells["A" + incRowExcel + ":" + _alphabetDetail + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                                        worksheet.Cells["A" + incRowExcel + ":" + _alphabetDetail + RowF].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                        worksheet.Cells["A" + incRowExcel + ":" + _alphabetDetail + RowF].Style.Border.Right.Style = ExcelBorderStyle.Thin;



                                                        worksheet.Cells["A" + RowB + ":" + _alphabetDetail + RowF].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                                        worksheet.Cells["A" + RowB + ":" + _alphabetDetail + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                                        worksheet.Cells["A" + RowB + ":" + _alphabetDetail + RowF].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                        worksheet.Cells["A" + RowB + ":" + _alphabetDetail + RowF].Style.Border.Right.Style = ExcelBorderStyle.Thin;





                                                        incRowExcel = incRowExcel + 2;


                                                        RowF = incRowExcel + 1;


                                                        //worksheet.Cells[5, 10].Value = "WEEKLY SUBSCRIPTION (Amount per Mil Rupiah)";
                                                        ////worksheet.Cells["E5" + ":" + _alphabet + incColExcel].Merge = true;
                                                        //worksheet.Cells["E5" + ":" + _alphabet + incColExcel].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                        //worksheet.Cells["E5" + ":" + _alphabet + incColExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                        //worksheet.Cells[6, 10].Value = "WEEKLY REDEMPTION (Amount per Mil Rupiah)";
                                                        ////worksheet.Cells["F5" + ":" + _alphabet + incColExcel].Merge = true;
                                                        //worksheet.Cells["F5" + ":" + _alphabet + incColExcel].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                        //worksheet.Cells["F5" + ":" + _alphabet + incColExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;





                                                    }





                                                }
                                            }
                                        }

                                        #endregion

                                        #region DIRECT RED

                                        int i = 0;
                                        // RED
                                        using (SqlConnection DbCon1 = new SqlConnection(Tools.conString))
                                        {
                                            DbCon1.Open();
                                            using (SqlCommand cmd1 = DbCon1.CreateCommand())
                                            {
                                                cmd1.CommandText =

                                                @"  
                                                --DROP TABLE #tableAgent 
--                                                DROP TABLE #tableAgentClientSubs
--                                                DROP TABLE #tableAgentClientFCP
--                                                DROP TABLE #tableAgentClientFCPHaveAumOnly
--                                                DROP TABLE #tableAgentClientSubsDetail
--                                                DROP TABLE #tableAgentClientRedDetail
--                                                DROP TABLE #tableAgentFCPAum
--                                                DROP TABLE #tableResult
--                                                DROP TABLE #tableResultSubs
--                                                DROP TABLE #tableResultRed

                                                DECLARE @DateToMinOne DATETIME

                                                SET @DateToMinOne = dbo.FWorkingDay(@DateTo,-1)


                                                CREATE table #tableAgent 
                                                (
	                                                AgentPK int
                                                )
                                                CREATE CLUSTERED INDEX indx_tableAgent ON #tableAgent (AgentPK);


                                                INSERT INTO #tableAgent
                                                        ( AgentPK )
                                                SELECT DISTINCT SellingAgentPK FROM FundClient WHERE status IN (1,2)
                                                AND SellingAgentPK <> 0


                                                CREATE table #tableAgentClientSubs 
                                                (
	                                                AgentPK INT,
	                                                FundClientPK int
                                                )
                                                CREATE CLUSTERED INDEX indx_AgentClientSubs   ON #tableAgentClientSubs  (AgentPK,FundClientPK);
                                                INSERT INTO #tableAgentClientSubs
                                                        ( AgentPK, FundClientPK )
                                                SELECT DISTINCT B.SellingAgentPK,A.FundClientPK FROM dbo.ClientSubscription A 
                                                LEFT JOIN FundClient B ON A.FundClientPK = B.FundClientPK AND B.status IN (1,2)
                                                WHERE A.Posted = 1 
                                                AND A.Status <> 3 AND A.Revised = 0
                                                AND A.NAVDate BETWEEN @DateFrom AND @DateTo
                                                AND B.SellingAgentPK <> 0 


                                                CREATE table #tableAgentClientFCP
                                                (
	                                                AgentPK INT,
	                                                FundClientPK int
                                                )
                                                CREATE CLUSTERED INDEX indx_tableAgentClientFCP  ON #tableAgentClientFCP (AgentPK,FundClientPK);
                                                INSERT INTO #tableAgentClientFCP
                                                        ( AgentPK, FundClientPK )
                                                SELECT Distinct B.SellingAgentPK,A.FundClientPK FROM dbo.FundClientPosition A 
                                                LEFT JOIN FundClient B ON A.FundClientPK = B.FundClientPK AND B.status IN (1,2)
                                                WHERE Date = @DateFrom
                                                AND B.SellingAgentPK <> 0

                                                CREATE table #tableAgentClientFCPHaveAumOnly
                                                (
	                                                AgentPK INT,
	                                                FundClientPK int
                                                )
                                                CREATE CLUSTERED INDEX indx_tableAgentClientFCPHaveAumOnly  ON #tableAgentClientFCPHaveAumOnly (AgentPK,FundClientPK);
                                                INSERT INTO #tableAgentClientFCPHaveAumOnly
                                                        ( AgentPK, FundClientPK )
                                                SELECT Distinct B.SellingAgentPK,A.FundClientPK FROM dbo.FundClientPosition A 
                                                LEFT JOIN FundClient B ON A.FundClientPK = B.FundClientPK AND B.status IN (1,2)
                                                WHERE Date = @DateToMinOne
                                                AND B.SellingAgentPK <> 0 AND A.UnitAmount > 1



                                                CREATE table #tableAgentClientSubsDetail
                                                (
	                                                AgentPK INT,
	                                                FundClientPK INT,
	                                                ClientSubscriptionPK INT,
	                                                Amount NUMERIC(22,4)
                                                )
                                                CREATE CLUSTERED INDEX indx_tableAgentClientSubsDetail  ON #tableAgentClientSubsDetail (AgentPK,FundClientPK,ClientSubscriptionPK);
                                                INSERT INTO #tableAgentClientSubsDetail
                                                        ( AgentPK,FundClientPK, ClientSubscriptionPK,Amount )

                                                SELECT B.SellingAgentPK,A.FundClientPK,A.ClientSubscriptionPK,A.CashAmount FROM dbo.ClientSubscription A 
                                                LEFT JOIN FundClient B ON A.FundClientPK = B.FundClientPK AND B.status IN (1,2)
                                                WHERE A.Posted = 1 
                                                AND A.Status <> 3 AND A.Revised = 0
                                                AND A.NAVDate BETWEEN @DateFrom AND @DateTo
                                                AND B.SellingAgentPK <> 0 



                                                CREATE table #tableAgentClientRedDetail
                                                (
	                                                AgentPK INT,
	                                                ClientRedemptionPK int
                                                )
                                                CREATE CLUSTERED INDEX indx_tableAgentClientRedDetail  ON #tableAgentClientRedDetail (AgentPK,ClientRedemptionPK);
                                                INSERT INTO #tableAgentClientRedDetail
                                                        ( AgentPK, ClientRedemptionPK )

                                                SELECT B.SellingAgentPK,A.ClientRedemptionPK FROM dbo.ClientRedemption A 
                                                LEFT JOIN FundClient B ON A.FundClientPK = B.FundClientPK AND B.status IN (1,2)
                                                WHERE A.Posted = 1 
                                                AND A.Status <> 3 AND A.Revised = 0
                                                AND A.NAVDate BETWEEN @DateFrom AND @DateTo
                                                AND B.SellingAgentPK <> 0 



                                                CREATE table #tableAgentFCPAum
                                                (
	                                                AgentPK INT,
	                                                AUM NUMERIC(22,4)
                                                )
                                                CREATE CLUSTERED INDEX indx_tableAgentFCPAum  ON #tableAgentFCPAum (AgentPK);
                                                INSERT INTO #tableAgentFCPAum
                                                        ( AgentPK, AUM )
                                                SELECT B.SellingAgentPK,SUM(ISNULL(A.UnitAmount,0) * ISNULL(C.Nav,0)) AUM FROM dbo.FundClientPosition A 
                                                LEFT JOIN FundClient B ON A.FundClientPK = B.FundClientPK AND B.status IN (1,2)
                                                LEFT JOIN dbo.CloseNAV C ON A.FundPK = C.FundPK AND C.date = @DateTo  AND C.status IN (1,2)
                                                WHERE A.Date = @DateToMinOne
                                                AND B.SellingAgentPK <> 0 AND A.UnitAmount > 1
                                                GROUP BY B.SellingAgentPK


                                                CREATE table #tableResult
                                                (
	                                                AgentPK INT,
	                                                Type NVARCHAR(100),
	                                                Code NVARCHAR(100),
	                                                AgentName NVARCHAR(300),
	                                                TotalNewClientThisWeek INT,
	                                                TotalAllClient INT,
	                                                TotalSubs INT,
	                                                TotalRed INT,
	                                                AUM NUMERIC(22,4),
	                                                TotalTopUp int,
	                                                AmountTopUp numeric(22,4)
                                                )
                                                CREATE CLUSTERED INDEX indx_tableResult  ON #tableResult (AgentPK);

                                                INSERT INTO #tableResult
												SELECT  
												A.AgentPK
												,CASE WHEN B.Type = 1 THEN 'DIRECT' ELSE 'INDIRECT' END Type,B.ID Code,B.Name AgentName 
												--,FORMAT(B.JoinDate,''d-MMM-yy''
												,ISNULL(C.totalNewClientThisWeek,0) totalNewClientThisWeekE6
												,ISNULL(D.TotalAllClient,0) TotalAllClientE7
												,ISNULL(E.TotalSubs,0) TotalSubs
												,ISNULL(F.TotalRed,0) TotalRed
												,ISNULL(G.AUM,0) / 1000000 AUM
												,ISNULL(H.TotalTopUp,0) TotalTopUp
												,ISNULL(I.AmountTopUp,0) / 1000000 AmountTopUp
												FROM #tableAgent A
												INNER JOIN Agent B ON A.AgentPK = B.AgentPK AND B.status IN (1,2)
												left JOIN
												(
													SELECT A.AgentPK,COUNT(A.FundClientPK) totalNewClientThisWeek FROM #tableAgentClientSubs A
													LEFT JOIN #tableAgentClientFCP B ON A.fundClientPK = B.FundClientPK 
													WHERE B.FundClientPK  IS NULL
													GROUP BY A.AgentPK
												)C on A.AgentPK = C.AgentPK 
												left JOIN
												(
													SELECT A.AgentPK,COUNT(A.FundClientPK) TotalAllClient FROM #tableAgentClientFCPHaveAumOnly A
													GROUP BY A.AgentPK
												)D on A.AgentPK = D.AgentPK 
												left join
												(
													Select AgentPK,count(ClientsubscriptionPK) totalSubs from #tableAgentClientSubsDetail
													group By AgentPK
												)E on A.AgentPK = E.AgentPK
												left join
												(
													Select AgentPK,count(ClientRedemptionPK) totalRed from #tableAgentClientRedDetail
													group By AgentPK
												)F on A.AgentPK = F.AgentPK
												left join
												(
													Select * from #tableAgentFCPAum
												)G on A.AgentPK = G.AgentPK
												left JOIN
												(
													SELECT A.AgentPK,COUNT(A.ClientsubscriptionPK) TotalTopUp FROM #tableAgentClientSubsDetail A
													INNER JOIN #tableAgentClientFCP B ON A.fundClientPK = B.FundClientPK 
													GROUP BY A.AgentPK
												)H on A.AgentPK = H.AgentPK 
												left JOIN
												(
													SELECT A.AgentPK,SUM(ISNULL(Amount,0)) AmountTopUp FROM #tableAgentClientSubsDetail A
													INNER JOIN #tableAgentClientFCP B ON A.fundClientPK = B.FundClientPK 
													GROUP BY A.AgentPK
												)I on A.AgentPK = I.AgentPK
												where B.Type = 1




                                                DECLARE @cols AS NVARCHAR(MAX),@colsForQuery AS NVARCHAR(MAX),@colsRed AS NVARCHAR(MAX),@colsForQueryRed AS NVARCHAR(MAX),
                                                    @querySubs  AS NVARCHAR(MAX)

                                                select @colsForQueryRed = STUFF((SELECT ',isnull(' + QUOTENAME(ID) +',0) ' + QUOTENAME(ID) 
                                                                    from (SELECT DISTINCT C.ID FROM dbo.ClientRedemption A 
                                                							LEFT JOIN FundClient B ON A.FundClientPK = B.FundClientPK AND B.status IN (1,2)
                                                							left join Fund C on A.FundPK = C.FundPK and C.status in (1,2)
                                                							WHERE A.Posted = 1 
                                                							AND A.Status <> 3 AND A.Revised = 0
                                                							AND A.NAVDate BETWEEN @DateFrom AND @DateTo
                                                							AND B.SellingAgentPK <> 0 ) A
                                                					order by A.ID
                                                            FOR XML PATH(''), TYPE
                                                            ).value('.', 'NVARCHAR(MAX)') 
                                                        ,1,1,'')

                                                select @colsRed = STUFF((SELECT distinct ',' + QUOTENAME(C.ID) 
                                                                    FROM dbo.ClientRedemption A 
                                                							LEFT JOIN FundClient B ON A.FundClientPK = B.FundClientPK AND B.status IN (1,2)
                                                							left join Fund C on A.FundPK = C.FundPK and C.status in (1,2)
                                                							WHERE A.Posted = 1 
                                                							AND A.Status <> 3 AND A.Revised = 0
                                                							AND A.NAVDate BETWEEN @DateFrom AND @DateTo
                                                							AND B.SellingAgentPK <> 0
				
                                                            FOR XML PATH(''), TYPE
                                                            ).value('.', 'NVARCHAR(MAX)') 
                                                        ,1,1,'')



                                                CREATE table #tableResultSubs
                                                (

	                                                SellingAgentPK INT,
	                                                ID NVARCHAR(200),
	                                                Subs NUMERIC(22,4)
                                                )
                                                INSERT INTO #tableResultSubs
                                                        ( SellingAgentPK, ID, Subs )

                                                SELECT B.SellingAgentPK,C.ID ,A.CashAmount/1000000 Subs FROM dbo.ClientSubscription A 
					                                                LEFT JOIN FundClient B ON A.FundClientPK = B.FundClientPK AND B.status IN (1,2)
					                                                left join Fund C on A.FundPK = C.FundPK and C.status in (1,2)
                                                                    INNER JOIN Agent D ON B.SellingAgentPK = D.AgentPK AND D.status IN (1,2)
					                                                WHERE A.Posted = 1 
					                                                AND A.Status <> 3 AND A.Revised = 0
					                                                AND A.NAVDate BETWEEN @DateFrom AND @DateTo
					                                                AND B.SellingAgentPK <> 0 and D.Type = 1


                                                CREATE table #tableResultRed
                                                (

	                                                SellingAgentPK INT,
	                                                ID NVARCHAR(200),
	                                                Red NUMERIC(22,4)
                                                )
                                                INSERT INTO #tableResultRed
                                                        ( SellingAgentPK, ID, Red )

                                                SELECT B.SellingAgentPK,C.ID ,A.CashAmount/1000000 Red FROM dbo.ClientRedemption A 
					                                                LEFT JOIN FundClient B ON A.FundClientPK = B.FundClientPK AND B.status IN (1,2)
					                                                left join Fund C on A.FundPK = C.FundPK and C.status in (1,2)
                                                                    INNER JOIN Agent D ON B.SellingAgentPK = D.AgentPK AND D.status IN (1,2)
					                                                WHERE A.Posted = 1 
					                                                AND A.Status <> 3 AND A.Revised = 0
					                                                AND A.NAVDate BETWEEN @DateFrom AND @DateTo
					                                                AND B.SellingAgentPK <> 0 and D.Type = 1



                                                set @querySubs = 'SELECT B.Type,B.Code,B.Agentname,  ' + @colsForQueryRed + ' 
			                                                from 
                                                                (
				                                                SELECT * FROM #tableResultRed
                                                            ) x
                                                            pivot 
                                                            (
                                                                SUM(red)
                                                                for ID in (' + @colsRed + ')
                                                            ) p
			                                                left join  #tableResult B on SellingAgentPK = B.AgentPK
                                                            where B.Type = ''DIRECT''
			                                                order by SellingAgentPK desc
			                                                '

                                                exec(@querySubs)";

                                                cmd1.CommandTimeout = 0;
                                                cmd1.Parameters.AddWithValue("@DateFrom", _unitRegistryRpt.ValueDateFrom);
                                                cmd1.Parameters.AddWithValue("@DateTo", _unitRegistryRpt.ValueDateTo);

                                                cmd1.ExecuteNonQuery();


                                                using (SqlDataReader dr1 = cmd1.ExecuteReader())
                                                {
                                                    incRowExcel = 8;
                                                    int incColExcel = 10;
                                                    IsiCol = new int[dr1.FieldCount];
                                                    IsiRow = new int[_rowtotal];
                                                    _counter = 1;
                                                    _counterRow = 1;

                                                    incRowExcel++;
                                                    while (dr1.Read())
                                                    {

                                                        int RowB = incRowExcel;
                                                        int RowG = incRowExcel + 1;



                                                        int RowC = incRowExcel;
                                                        int RowF = incRowExcel + 1;
                                                        int _rowsebelumnya = incRowExcel - 1;
                                                        int set = 8;
                                                        int y0;
                                                        string _alphabetDetail = _host.GetAlphabet(incColExcel - 1);

                                                        //find same row


                                                        incColExcel = 10;
                                                        for (int inc2 = 0; inc2 < _rowtotal; inc2++)
                                                        {
                                                            string abc = dr1.GetValue(2).ToString();
                                                            if (worksheet.Cells[set, 3].Value.ToString() == dr1.GetValue(2).ToString())
                                                            {
                                                                _counterRow = set + 1;
                                                                break;
                                                            }
                                                            set = set + 2;
                                                        }

                                                        set = 7;
                                                        i++;
                                                        //find same column
                                                        if (_counter == 1)
                                                        {

                                                            for (int inc1 = 3; inc1 < dr1.FieldCount; inc1++)
                                                            {
                                                                incColExcel = 10;
                                                                string abc = dr1.GetName(inc1).ToString();
                                                                for (int inc2 = 3; inc2 <= dr1.FieldCount; inc2++)
                                                                {

                                                                    if (worksheet.Cells[set, incColExcel].Value == null)
                                                                    {
                                                                        IsiCol[inc1] = _rowAkhir;
                                                                        //worksheet.Cells[set, incColExcel].Value = dr1.GetName(inc1).ToString();
                                                                        _rowAkhir++;
                                                                        break;
                                                                    }
                                                                    else if (worksheet.Cells[set, incColExcel].Value.ToString() == dr1.GetName(inc1).ToString())
                                                                    {
                                                                        IsiCol[inc1] = incColExcel;
                                                                        break;
                                                                    }
                                                                    incColExcel++;
                                                                }

                                                            }

                                                            for (int inc0 = 3; inc0 < dr1.FieldCount; inc0++)
                                                            {
                                                                y0 = IsiCol[inc0];
                                                                if (y0 == 0)
                                                                    y0 = 1;
                                                                worksheet.Cells[7, y0].Value = dr1.GetName(inc0).ToString();
                                                                worksheet.Cells[7, y0].Style.Numberformat.Format = "#,##0.0000";
                                                                worksheet.Cells[7, y0].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                                worksheet.Column(y0).Width = 21;
                                                            }
                                                            worksheet.Cells[5, 10, 7, _rowAkhir - 1].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                                            worksheet.Cells[5, 10, 7, _rowAkhir - 1].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                                            worksheet.Cells[5, 10, 7, _rowAkhir - 1].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                            worksheet.Cells[5, 10, 7, _rowAkhir - 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                                            worksheet.Cells[5, 10, 5, _rowAkhir - 1].Merge = true;
                                                            worksheet.Cells[6, 10, 6, _rowAkhir - 1].Merge = true;
                                                            _counter++;
                                                        }

                                                        for (int inc0 = 3; inc0 < dr1.FieldCount; inc0++)
                                                        {
                                                            y0 = IsiCol[inc0];
                                                            worksheet.Cells[_counterRow, y0].Value = dr1.GetValue(inc0).ToString();
                                                            worksheet.Cells[_counterRow, y0].Style.Numberformat.Format = "#,##0.0000";
                                                            worksheet.Cells[_counterRow, y0].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                        }


                                                        worksheet.Cells["A" + incRowExcel + ":" + _alphabetDetail + RowF].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                                        worksheet.Cells["A" + incRowExcel + ":" + _alphabetDetail + RowF].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                                        worksheet.Cells["A" + incRowExcel + ":" + _alphabetDetail + RowF].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                        worksheet.Cells["A" + incRowExcel + ":" + _alphabetDetail + RowF].Style.Border.Right.Style = ExcelBorderStyle.Thin;



                                                        worksheet.Cells[incRowExcel - 1, 1, _RowBaris - 1, _rowAkhir - 1].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                                        worksheet.Cells[incRowExcel - 1, 1, _RowBaris - 1, _rowAkhir - 1].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                                        worksheet.Cells[incRowExcel - 1, 1, _RowBaris - 1, _rowAkhir - 1].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                        worksheet.Cells[incRowExcel - 1, 1, _RowBaris - 1, _rowAkhir - 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;





                                                        incRowExcel = incRowExcel + 2;


                                                        RowF = incRowExcel + 1;


                                                        //worksheet.Cells[5, 10].Value = "WEEKLY SUBSCRIPTION (Amount per Mil Rupiah)";
                                                        ////worksheet.Cells["E5" + ":" + _alphabet + incColExcel].Merge = true;
                                                        //worksheet.Cells["E5" + ":" + _alphabet + incColExcel].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                        //worksheet.Cells["E5" + ":" + _alphabet + incColExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                        //worksheet.Cells[6, 10].Value = "WEEKLY REDEMPTION (Amount per Mil Rupiah)";
                                                        ////worksheet.Cells["F5" + ":" + _alphabet + incColExcel].Merge = true;
                                                        //worksheet.Cells["F5" + ":" + _alphabet + incColExcel].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                        //worksheet.Cells["F5" + ":" + _alphabet + incColExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;





                                                    }





                                                }
                                            }
                                        }


                                        worksheet.PrinterSettings.FitToPage = true;
                                        worksheet.PrinterSettings.FitToWidth = 1;
                                        worksheet.PrinterSettings.FitToHeight = 0;
                                        worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel + 8, 8];
                                        worksheet.Column(1).Width = 6;
                                        worksheet.Column(2).AutoFit();
                                        worksheet.Column(3).Width = 40;
                                        worksheet.Column(4).Width = 21;
                                        worksheet.Column(5).Width = 21;
                                        worksheet.Column(6).Width = 21;
                                        worksheet.Column(7).Width = 21;
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






                                        // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                        //worksheet.Cells.AutoFitColumns(); // UNTUK AUTO FIT UKURAN TULISAN DENGAN KOLOM, KEBETULAN INI BUAT HEADER AJA DIPAKENYA
                                        //worksheet.PrinterSettings.FitToPage = true;
                                        worksheet.PrinterSettings.Orientation = eOrientation.Landscape; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                        worksheet.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN
                                        worksheet.HeaderFooter.OddHeader.CenteredText = "&12 Weekly Sales Report";


                                        Image img = Image.FromFile(Tools.ReportImage);
                                        worksheet.HeaderFooter.OddHeader.InsertPicture(img, PictureAlignment.Left);

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

                                        #endregion




                                        int _rowtotal2 = 1;
                                        int _counter2 = 1;
                                        int _counterRow2 = 1;
                                        int _RowBaris2 = 1;
                                        int[] IsiCol2;
                                        int[] IsiRow2;
                                        int _StatusRow = 0;

                                        #region INDIRECT 

                                        using (SqlConnection DbCon3 = new SqlConnection(Tools.conString))
                                        {

                                            //worksheet1 = package.Workbook.worksheet1.Add("IN DIRECT");

                                            DbCon3.Close();
                                            DbCon3.Open();
                                            using (SqlCommand cmd2 = DbCon3.CreateCommand())
                                            {


                                                cmd2.CommandText = @"

--DROP TABLE #tableAgent 
--DROP TABLE #tableAgentClientSubs
--DROP TABLE #tableAgentClientFCP
--DROP TABLE #tableAgentClientFCPHaveAumOnly
--DROP TABLE #tableAgentClientSubsDetail
--DROP TABLE #tableAgentClientRedDetail
--DROP TABLE #tableAgentFCPAum
--DROP TABLE #tableResult
--DROP TABLE #tableResultSubs
--DROP TABLE #tableResultRed

DECLARE @DateToMinOne DATETIME

SET @DateToMinOne = dbo.FWorkingDay(@DateTo,-1)


CREATE table #tableAgent 
(
	AgentPK int
)
CREATE CLUSTERED INDEX indx_tableAgent ON #tableAgent (AgentPK);


INSERT INTO #tableAgent
        ( AgentPK )
SELECT DISTINCT SellingAgentPK FROM FundClient WHERE status IN (1,2)
AND SellingAgentPK <> 0


CREATE table #tableAgentClientSubs 
(
	AgentPK INT,
	FundClientPK int
)
CREATE CLUSTERED INDEX indx_AgentClientSubs   ON #tableAgentClientSubs  (AgentPK,FundClientPK);
INSERT INTO #tableAgentClientSubs
        ( AgentPK, FundClientPK )
SELECT DISTINCT B.SellingAgentPK,A.FundClientPK FROM dbo.ClientSubscription A 
LEFT JOIN FundClient B ON A.FundClientPK = B.FundClientPK AND B.status IN (1,2)
WHERE A.Posted = 1 
AND A.Status <> 3 AND A.Revised = 0
AND A.NAVDate BETWEEN @DateFrom AND @DateTo
AND B.SellingAgentPK <> 0 


CREATE table #tableAgentClientFCP
(
	AgentPK INT,
	FundClientPK int
)
CREATE CLUSTERED INDEX indx_tableAgentClientFCP  ON #tableAgentClientFCP (AgentPK,FundClientPK);
INSERT INTO #tableAgentClientFCP
        ( AgentPK, FundClientPK )
SELECT Distinct B.SellingAgentPK,A.FundClientPK FROM dbo.FundClientPosition A 
LEFT JOIN FundClient B ON A.FundClientPK = B.FundClientPK AND B.status IN (1,2)
WHERE Date = @DateFrom
AND B.SellingAgentPK <> 0

CREATE table #tableAgentClientFCPHaveAumOnly
(
	AgentPK INT,
	FundClientPK int
)
CREATE CLUSTERED INDEX indx_tableAgentClientFCPHaveAumOnly  ON #tableAgentClientFCPHaveAumOnly (AgentPK,FundClientPK);
INSERT INTO #tableAgentClientFCPHaveAumOnly
        ( AgentPK, FundClientPK )
SELECT Distinct B.SellingAgentPK,A.FundClientPK FROM dbo.FundClientPosition A 
LEFT JOIN FundClient B ON A.FundClientPK = B.FundClientPK AND B.status IN (1,2)
WHERE Date = @DateToMinOne
AND B.SellingAgentPK <> 0 AND A.UnitAmount > 1



CREATE table #tableAgentClientSubsDetail
(
	AgentPK INT,
	FundClientPK INT,
	ClientSubscriptionPK INT,
	Amount NUMERIC(22,4)
)
CREATE CLUSTERED INDEX indx_tableAgentClientSubsDetail  ON #tableAgentClientSubsDetail (AgentPK,FundClientPK,ClientSubscriptionPK);
INSERT INTO #tableAgentClientSubsDetail
        ( AgentPK,FundClientPK, ClientSubscriptionPK,Amount )

SELECT B.SellingAgentPK,A.FundClientPK,A.ClientSubscriptionPK,A.CashAmount FROM dbo.ClientSubscription A 
LEFT JOIN FundClient B ON A.FundClientPK = B.FundClientPK AND B.status IN (1,2)
WHERE A.Posted = 1 
AND A.Status <> 3 AND A.Revised = 0
AND A.NAVDate BETWEEN @DateFrom AND @DateTo
AND B.SellingAgentPK <> 0 



CREATE table #tableAgentClientRedDetail
(
	AgentPK INT,
	ClientRedemptionPK int
)
CREATE CLUSTERED INDEX indx_tableAgentClientRedDetail  ON #tableAgentClientRedDetail (AgentPK,ClientRedemptionPK);
INSERT INTO #tableAgentClientRedDetail
        ( AgentPK, ClientRedemptionPK )

SELECT B.SellingAgentPK,A.ClientRedemptionPK FROM dbo.ClientRedemption A 
LEFT JOIN FundClient B ON A.FundClientPK = B.FundClientPK AND B.status IN (1,2)
WHERE A.Posted = 1 
AND A.Status <> 3 AND A.Revised = 0
AND A.NAVDate BETWEEN @DateFrom AND @DateTo
AND B.SellingAgentPK <> 0 



CREATE table #tableAgentFCPAum
(
	AgentPK INT,
	AUM NUMERIC(22,4)
)
CREATE CLUSTERED INDEX indx_tableAgentFCPAum  ON #tableAgentFCPAum (AgentPK);
INSERT INTO #tableAgentFCPAum
        ( AgentPK, AUM )
SELECT B.SellingAgentPK,SUM(ISNULL(A.UnitAmount,0) * ISNULL(C.Nav,0)) AUM FROM dbo.FundClientPosition A 
LEFT JOIN FundClient B ON A.FundClientPK = B.FundClientPK AND B.status IN (1,2)
LEFT JOIN dbo.CloseNAV C ON A.FundPK = C.FundPK AND C.date = @DateTo  AND C.status IN (1,2)
WHERE A.Date = @DateToMinOne
AND B.SellingAgentPK <> 0 AND A.UnitAmount > 1
GROUP BY B.SellingAgentPK


CREATE table #tableResult
(
	AgentPK INT,
	Type NVARCHAR(100),
	Code NVARCHAR(100),
	AgentName NVARCHAR(300),
	joindate date,
	TotalNewClientThisWeek INT,
	TotalAllClient INT,
	TotalSubs INT,
	TotalRed INT,
	AUM NUMERIC(22,4),
	TotalTopUp int,
	AmountTopUp numeric(22,4)
)
CREATE CLUSTERED INDEX indx_tableResult  ON #tableResult (AgentPK);

INSERT INTO #tableResult
SELECT  
A.AgentPK
,CASE WHEN B.Type = 1 THEN 'DIRECT' ELSE 'INDIRECT' END Type,B.ID Code,B.Name AgentName 
,FORMAT(B.JoinDate,'d-MMM-yy') JoinDate
,ISNULL(C.totalNewClientThisWeek,0) totalNewClientThisWeekE6
,ISNULL(D.TotalAllClient,0) TotalAllClientE7
,ISNULL(E.TotalSubs,0) TotalSubs
,ISNULL(F.TotalRed,0) TotalRed
,ISNULL(G.AUM,0) / 1000000 AUM
,ISNULL(H.TotalTopUp,0) TotalTopUp
,ISNULL(I.AmountTopUp,0) / 1000000 AmountTopUp
FROM #tableAgent A
INNER JOIN Agent B ON A.AgentPK = B.AgentPK AND B.status IN (1,2)
left JOIN
(
	SELECT A.AgentPK,COUNT(A.FundClientPK) totalNewClientThisWeek FROM #tableAgentClientSubs A
	LEFT JOIN #tableAgentClientFCP B ON A.fundClientPK = B.FundClientPK 
	WHERE B.FundClientPK  IS NULL
	GROUP BY A.AgentPK
)C on A.AgentPK = C.AgentPK 
left JOIN
(
	SELECT A.AgentPK,COUNT(A.FundClientPK) TotalAllClient FROM #tableAgentClientFCPHaveAumOnly A
	GROUP BY A.AgentPK
)D on A.AgentPK = D.AgentPK 
left join
(
	Select AgentPK,count(ClientsubscriptionPK) totalSubs from #tableAgentClientSubsDetail
	group By AgentPK
)E on A.AgentPK = E.AgentPK
left join
(
	Select AgentPK,count(ClientRedemptionPK) totalRed from #tableAgentClientRedDetail
	group By AgentPK
)F on A.AgentPK = F.AgentPK
left join
(
	Select * from #tableAgentFCPAum
)G on A.AgentPK = G.AgentPK
left JOIN
(
	SELECT A.AgentPK,COUNT(A.ClientsubscriptionPK) TotalTopUp FROM #tableAgentClientSubsDetail A
	INNER JOIN #tableAgentClientFCP B ON A.fundClientPK = B.FundClientPK 
	GROUP BY A.AgentPK
)H on A.AgentPK = H.AgentPK 
left JOIN
(
	SELECT A.AgentPK,SUM(ISNULL(Amount,0)) AmountTopUp FROM #tableAgentClientSubsDetail A
	INNER JOIN #tableAgentClientFCP B ON A.fundClientPK = B.FundClientPK 
	GROUP BY A.AgentPK
)I on A.AgentPK = I.AgentPK 





DECLARE @cols AS NVARCHAR(MAX),@colsForQuery AS NVARCHAR(MAX),@colsRed AS NVARCHAR(MAX),@colsForQueryRed AS NVARCHAR(MAX),
    @query  AS NVARCHAR(MAX)

select @colsForQuery = STUFF((SELECT ',isnull(' + QUOTENAME(ID) +',0) ' + QUOTENAME(ID) 
                    from (SELECT DISTINCT C.ID FROM dbo.ClientSubscription A 
							LEFT JOIN FundClient B ON A.FundClientPK = B.FundClientPK AND B.status IN (1,2)
							left join Fund C on A.FundPK = C.FundPK and C.status in (1,2)
							WHERE A.Posted = 1 
							AND A.Status <> 3 AND A.Revised = 0
							AND A.NAVDate BETWEEN @DateFrom AND @DateTo
							AND B.SellingAgentPK <> 0 ) A
					order by A.ID
            FOR XML PATH(''), TYPE
            ).value('.', 'NVARCHAR(MAX)') 
        ,1,1,'')

select @cols = STUFF((SELECT distinct ',' + QUOTENAME(C.ID) 
                    FROM dbo.ClientSubscription A 
							LEFT JOIN FundClient B ON A.FundClientPK = B.FundClientPK AND B.status IN (1,2)
							left join Fund C on A.FundPK = C.FundPK and C.status in (1,2)
							WHERE A.Posted = 1 
							AND A.Status <> 3 AND A.Revised = 0
							AND A.NAVDate BETWEEN @DateFrom AND @DateTo
							AND B.SellingAgentPK <> 0
				
            FOR XML PATH(''), TYPE
            ).value('.', 'NVARCHAR(MAX)') 
        ,1,1,'')


CREATE table #tableResultSubs
(

	SellingAgentPK INT,
	ID NVARCHAR(200),
	Subs NUMERIC(22,4)
)
INSERT INTO #tableResultSubs
        ( SellingAgentPK, ID, Subs )

SELECT B.SellingAgentPK,C.ID ,A.CashAmount/1000000 Subs FROM dbo.ClientSubscription A 
					LEFT JOIN FundClient B ON A.FundClientPK = B.FundClientPK AND B.status IN (1,2)
					left join Fund C on A.FundPK = C.FundPK and C.status in (1,2)
					WHERE A.Posted = 1 
					AND A.Status <> 3 AND A.Revised = 0
					AND A.NAVDate BETWEEN @DateFrom AND @DateTo
					AND B.SellingAgentPK <> 0 


CREATE table #tableResultRed
(

	SellingAgentPK INT,
	ID NVARCHAR(200),
	Red NUMERIC(22,4)
)
INSERT INTO #tableResultRed
        ( SellingAgentPK, ID, Red )

SELECT B.SellingAgentPK,C.ID ,A.CashAmount/1000000 Red FROM dbo.ClientRedemption A 
					LEFT JOIN FundClient B ON A.FundClientPK = B.FundClientPK AND B.status IN (1,2)
					left join Fund C on A.FundPK = C.FundPK and C.status in (1,2)
					WHERE A.Posted = 1 
					AND A.Status <> 3 AND A.Revised = 0
					AND A.NAVDate BETWEEN @DateFrom AND @DateTo
					AND B.SellingAgentPK <> 0 

SELECT B.Type,B.Code,B.Agentname,B.JoinDate,B.TotalNewClientThisWeek,B.TotalAllClient,B.TotalSubs,B.TotalRed
			,B.AUM,B.TotalTopUp,B.AmountTopUp,'''' Remark
			from #tableResult B
			where B.AgentPK in (select * from 
					(select distinct SellingAgentPK from #tableResultRed
					union all
					select distinct SellingAgentPK from #tableResultSubs) A
					group by A.SellingAgentPK)
					and B.Type = 'INDIRECT'
			order by B.AgentPK asc
	

                            ";

                                                cmd2.CommandTimeout = 0;
                                                cmd2.Parameters.AddWithValue("@DateFrom", _unitRegistryRpt.ValueDateFrom);
                                                cmd2.Parameters.AddWithValue("@DateTo", _unitRegistryRpt.ValueDateTo);


                                                using (SqlDataReader dr2 = cmd2.ExecuteReader())
                                                {
                                                    if (!dr2.HasRows)
                                                    {
                                                        _StatusRow = 1;
                                                    }

                                                    if (_StatusRow == 0)
                                                    { 

                                                    //ATUR DATA GROUPINGNYA DULU
                                                    List<WeeklySalesReportINDIRECT> rList2 = new List<WeeklySalesReportINDIRECT>();
                                                    while (dr2.Read())
                                                    {

                                                        WeeklySalesReportINDIRECT rSingle2 = new WeeklySalesReportINDIRECT();
                                                        rSingle2.Type = Convert.ToString(dr2["Type"]);
                                                        rSingle2.Code = Convert.ToString(dr2["Code"]);
                                                        rSingle2.AgentName = Convert.ToString(dr2["AgentName"]);
                                                        rSingle2.JoinDate = Convert.ToDateTime(dr2["JoinDate"]);
                                                        rSingle2.TotalNewClientThisWeek = Convert.ToDecimal(dr2["TotalNewClientThisWeek"]);
                                                        rSingle2.TotalAllClient = Convert.ToDecimal(dr2["TotalAllClient"]);
                                                        rSingle2.TotalSubs = Convert.ToDecimal(dr2["TotalSubs"]);
                                                        rSingle2.TotalRed = Convert.ToDecimal(dr2["TotalRed"]);
                                                        rSingle2.AUM = Convert.ToDecimal(dr2["AUM"]);
                                                        rSingle2.TotalTopUp = Convert.ToDecimal(dr2["TotalTopUp"]);
                                                        rSingle2.AmountTopUp = Convert.ToDecimal(dr2["AmountTopUp"]);
                                                        rSingle2.Remark = Convert.ToString(dr2["Remark"]);
                                                        rList2.Add(rSingle2);

                                                    }



                                                    var GroupByReference2 =
                                                            from r2 in rList2
                                                                //orderby r ascending
                                                            group r2 by new { } into rGroup2
                                                            select rGroup2;

                                                    //int incRowExcel = 0;
                                                    incRowExcel = 0;






                                                    foreach (var rsHeader2 in GroupByReference2)
                                                    {
                                                        incRowExcel++;
                                                        worksheet1.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                                        worksheet1.Cells[incRowExcel, 1].Value = "PT AYERS ASIA ASSET MANAGEMENT";
                                                        worksheet1.Cells["A" + incRowExcel + ":I" + incRowExcel].Merge = true;
                                                        worksheet1.Cells["A" + incRowExcel + ":I" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                        incRowExcel++;
                                                        worksheet1.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                                        worksheet1.Cells[incRowExcel, 1].Value = "WEEKLY MARKETING PERFORMANCE - (AYERS INTERNAL MARKETING)";
                                                        worksheet1.Cells["A" + incRowExcel + ":I" + incRowExcel].Merge = true;
                                                        worksheet1.Cells["A" + incRowExcel + ":I" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                        incRowExcel++;
                                                        worksheet1.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                                        worksheet1.Cells[incRowExcel, 1].Value = "PERIOD " + Convert.ToDateTime(_unitRegistryRpt.ValueDateFrom).ToString("dd-MMM-yyyy") + " - " + Convert.ToDateTime(_unitRegistryRpt.ValueDateTo).ToString("dd-MMM-yyyy");
                                                        worksheet1.Cells["A" + incRowExcel + ":I" + incRowExcel].Merge = true;
                                                        worksheet1.Cells["A" + incRowExcel + ":I" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                        incRowExcel++;
                                                        worksheet1.Cells[incRowExcel, 1].Value = "Type : INDIRECT";
                                                        incRowExcel++;

                                                        int RowB2 = incRowExcel;
                                                        int RowG2 = incRowExcel + 1;


                                                        worksheet1.Cells[incRowExcel, 1].Value = "NO";
                                                        worksheet1.Cells["A" + RowB2 + ":A" + RowG2].Merge = true;
                                                        worksheet1.Cells["A" + RowB2 + ":A" + RowG2].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                        worksheet1.Cells["A" + RowB2 + ":A" + RowG2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                        worksheet1.Cells[incRowExcel, 2].Value = "MARKETING";
                                                        worksheet1.Cells["B" + RowB2 + ":D" + RowB2].Merge = true;
                                                        worksheet1.Cells["B" + RowB2 + ":D" + RowB2].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                        worksheet1.Cells["B" + RowB2 + ":D" + RowB2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                        worksheet1.Cells[RowG2, 2].Value = "CODE";
                                                        worksheet1.Cells["B" + RowG2 + ":B" + RowG2].Merge = true;
                                                        worksheet1.Cells["B" + RowG2 + ":B" + RowG2].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                        worksheet1.Cells["B" + RowG2 + ":B" + RowG2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                        worksheet1.Cells[RowG2, 3].Value = "NAME";
                                                        worksheet1.Cells["C" + RowG2 + ":C" + RowG2].Merge = true;
                                                        worksheet1.Cells["C" + RowG2 + ":C" + RowG2].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                        worksheet1.Cells["C" + RowG2 + ":C" + RowG2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                        worksheet1.Cells[RowG2, 4].Value = "JOIN DATE";
                                                        worksheet1.Cells["D" + RowG2 + ":D" + RowG2].Merge = true;
                                                        worksheet1.Cells["D" + RowG2 + ":D" + RowG2].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                        worksheet1.Cells["D" + RowG2 + ":D" + RowG2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                        worksheet1.Cells[incRowExcel, 5].Value = "NEW CLIENT";
                                                        worksheet1.Cells["E" + RowB2 + ":E" + RowB2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                        worksheet1.Cells[RowG2, 5].Value = "This Week";
                                                        worksheet1.Cells["E" + RowG2 + ":E" + RowG2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                        worksheet1.Cells[RowG2, 5].Value = "Accumulate";
                                                        worksheet1.Cells["E" + RowG2 + ":E" + RowG2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                        worksheet1.Cells[incRowExcel, 6].Value = "No of Transaction";
                                                        worksheet1.Cells["F" + RowB2 + ":F" + RowG2].Merge = true;
                                                        worksheet1.Cells["F" + RowB2 + ":F" + RowG2].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                        worksheet1.Cells["F" + RowB2 + ":F" + RowG2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                        worksheet1.Cells[incRowExcel, 7].Value = "LATEST AUM";
                                                        worksheet1.Cells["G" + RowB2 + ":G" + RowB2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                        worksheet1.Cells[RowG2, 7].Value = "(Amount Per Mil Rupiah)";
                                                        worksheet1.Cells["G" + RowG2 + ":G" + RowG2].Merge = true;
                                                        worksheet1.Cells["G" + RowG2 + ":G" + RowG2].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                        worksheet1.Cells["G" + RowG2 + ":G" + RowG2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                        worksheet1.Cells[incRowExcel, 8].Value = "TOP UP";
                                                        worksheet1.Cells["H" + RowB2 + ":H" + RowB2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;



                                                        worksheet1.Cells[RowG2, 8].Value = "No. of Transaction";
                                                        worksheet1.Cells["H" + RowG2 + ":H" + RowG2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                        worksheet1.Cells[RowG2, 8].Value = "Total Amount";
                                                        worksheet1.Cells["H" + RowG2 + ":H" + RowG2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                        worksheet1.Cells[incRowExcel, 9].Value = "REMARK";
                                                        worksheet1.Cells["I" + RowB2 + ":I" + RowG2].Merge = true;
                                                        worksheet1.Cells["I" + RowB2 + ":I" + RowG2].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                        worksheet1.Cells["I" + RowB2 + ":I" + RowG2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;




                                                        worksheet1.Cells["A" + RowB2 + ":I" + RowG2].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                                        worksheet1.Cells["A" + RowB2 + ":I" + RowG2].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                                        worksheet1.Cells["A" + RowB2 + ":I" + RowG2].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                        worksheet1.Cells["A" + RowB2 + ":I" + RowG2].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                                                        incRowExcel = incRowExcel + 3;

                                                        int first2 = incRowExcel;

                                                        int _no2 = 1;
                                                        int _startRowDetail2 = incRowExcel;
                                                        int _endRowDetail2 = 0;

                                                        int RowC2 = incRowExcel;
                                                        int RowF2 = incRowExcel + 1;


                                                        foreach (var rsDetail2 in rsHeader2)
                                                        {
                                                            worksheet1.Cells["A" + incRowExcel + ":I" + RowG2].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                                            worksheet1.Cells["A" + incRowExcel + ":I" + RowG2].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                                            worksheet1.Cells["A" + incRowExcel + ":I" + RowG2].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                            worksheet1.Cells["A" + incRowExcel + ":I" + RowG2].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                                                            worksheet1.Cells[incRowExcel, 1].Value = _no2;
                                                            worksheet1.Cells["A" + incRowExcel + ":A" + RowF2].Merge = true;
                                                            worksheet1.Cells["A" + incRowExcel + ":A" + RowF2].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                            worksheet1.Cells["A" + incRowExcel + ":A" + RowF2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                            worksheet1.Cells[incRowExcel, 2].Value = rsDetail2.Code;
                                                            worksheet1.Cells["B" + incRowExcel + ":B" + RowF2].Merge = true;
                                                            worksheet1.Cells["B" + incRowExcel + ":B" + RowF2].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                            worksheet1.Cells["B" + incRowExcel + ":B" + RowF2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                            worksheet1.Cells[incRowExcel, 3].Value = rsDetail2.AgentName;
                                                            worksheet1.Cells["C" + incRowExcel + ":C" + RowF2].Merge = true;
                                                            worksheet1.Cells["C" + incRowExcel + ":C" + RowF2].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                            worksheet1.Cells["C" + incRowExcel + ":C" + RowF2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                            worksheet1.Cells[incRowExcel, 4].Value = rsDetail2.JoinDate.ToString("dd/MM/yyyy");
                                                            worksheet1.Cells["D" + incRowExcel + ":D" + RowF2].Merge = true;
                                                            worksheet1.Cells["D" + incRowExcel + ":D" + RowF2].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                            worksheet1.Cells["D" + incRowExcel + ":D" + RowF2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                            worksheet1.Cells[incRowExcel, 5].Value = rsDetail2.TotalNewClientThisWeek;
                                                            worksheet1.Cells["E" + incRowExcel + ":E" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                                                            worksheet1.Cells[RowF2, 5].Value = rsDetail2.TotalAllClient;
                                                            worksheet1.Cells["E" + RowF2 + ":E" + RowF2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                                                            worksheet1.Cells[incRowExcel, 6].Value = rsDetail2.TotalSubs;
                                                            worksheet1.Cells["F" + incRowExcel + ":F" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                                                            worksheet1.Cells[RowF2, 6].Value = rsDetail2.TotalRed;
                                                            worksheet1.Cells["F" + RowF2 + ":F" + RowF2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                                                            worksheet1.Cells[incRowExcel, 7].Value = rsDetail2.AUM;
                                                            worksheet1.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0.00";
                                                            worksheet1.Cells["G" + incRowExcel + ":G" + RowF2].Merge = true;
                                                            worksheet1.Cells["G" + incRowExcel + ":G" + RowF2].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                            worksheet1.Cells["G" + incRowExcel + ":G" + RowF2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                                                            worksheet1.Cells[incRowExcel, 8].Value = rsDetail2.TotalTopUp;
                                                            worksheet1.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0.00";
                                                            worksheet1.Cells["H" + incRowExcel + ":H" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                                                            worksheet1.Cells[RowF2, 8].Value = rsDetail2.AmountTopUp;
                                                            worksheet1.Cells[RowF2, 8].Style.Numberformat.Format = "#,##0.00";
                                                            worksheet1.Cells["H" + RowF2 + ":H" + RowF2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;


                                                            worksheet1.Cells[incRowExcel, 9].Value = rsDetail2.Remark;
                                                            worksheet1.Cells["I" + incRowExcel + ":I" + RowF2].Merge = true;
                                                            worksheet1.Cells["I" + incRowExcel + ":I" + RowF2].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                            worksheet1.Cells["I" + incRowExcel + ":I" + RowF2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                            _rowtotal2++;
                                                            _no2++;
                                                            _endRowDetail2 = incRowExcel;
                                                            incRowExcel++;
                                                            incRowExcel++;
                                                            _RowBaris2 = incRowExcel;

                                                            RowF2 = incRowExcel + 1;


                                                        }

                                                    }

                                                    #endregion

                                                #region INDIRECT SUBS
                                                // SUBS
                                                using (SqlConnection DbCon5 = new SqlConnection(Tools.conString))
                                                {
                                                    DbCon5.Open();
                                                    using (SqlCommand cmd5 = DbCon5.CreateCommand())
                                                    {
                                                        cmd5.CommandText =

                                                        @"  
                                            --DROP TABLE #tableAgent 
                                            --DROP TABLE #tableAgentClientSubs
                                            --DROP TABLE #tableAgentClientFCP
                                            --DROP TABLE #tableAgentClientFCPHaveAumOnly
                                            --DROP TABLE #tableAgentClientSubsDetail
                                            --DROP TABLE #tableAgentClientRedDetail
                                            --DROP TABLE #tableAgentFCPAum
                                            --DROP TABLE #tableResult
                                            --DROP TABLE #tableResultSubs
                                            --DROP TABLE #tableResultRed

                                            DECLARE @DateToMinOne DATETIME

                                            SET @DateToMinOne = dbo.FWorkingDay(@DateTo,-1)


                                            CREATE table #tableAgent 
                                            (
	                                            AgentPK int
                                            )
                                            CREATE CLUSTERED INDEX indx_tableAgent ON #tableAgent (AgentPK);


                                            INSERT INTO #tableAgent
                                                    ( AgentPK )
                                            SELECT DISTINCT SellingAgentPK FROM FundClient WHERE status IN (1,2)
                                            AND SellingAgentPK <> 0


                                            CREATE table #tableAgentClientSubs 
                                            (
	                                            AgentPK INT,
	                                            FundClientPK int
                                            )
                                            CREATE CLUSTERED INDEX indx_AgentClientSubs   ON #tableAgentClientSubs  (AgentPK,FundClientPK);
                                            INSERT INTO #tableAgentClientSubs
                                                    ( AgentPK, FundClientPK )
                                            SELECT DISTINCT B.SellingAgentPK,A.FundClientPK FROM dbo.ClientSubscription A 
                                            LEFT JOIN FundClient B ON A.FundClientPK = B.FundClientPK AND B.status IN (1,2)
                                            WHERE A.Posted = 1 
                                            AND A.Status <> 3 AND A.Revised = 0
                                            AND A.NAVDate BETWEEN @DateFrom AND @DateTo
                                            AND B.SellingAgentPK <> 0 


                                            CREATE table #tableAgentClientFCP
                                            (
	                                            AgentPK INT,
	                                            FundClientPK int
                                            )
                                            CREATE CLUSTERED INDEX indx_tableAgentClientFCP  ON #tableAgentClientFCP (AgentPK,FundClientPK);
                                            INSERT INTO #tableAgentClientFCP
                                                    ( AgentPK, FundClientPK )
                                            SELECT Distinct B.SellingAgentPK,A.FundClientPK FROM dbo.FundClientPosition A 
                                            LEFT JOIN FundClient B ON A.FundClientPK = B.FundClientPK AND B.status IN (1,2)
                                            WHERE Date = @DateFrom
                                            AND B.SellingAgentPK <> 0

                                            CREATE table #tableAgentClientFCPHaveAumOnly
                                            (
	                                            AgentPK INT,
	                                            FundClientPK int
                                            )
                                            CREATE CLUSTERED INDEX indx_tableAgentClientFCPHaveAumOnly  ON #tableAgentClientFCPHaveAumOnly (AgentPK,FundClientPK);
                                            INSERT INTO #tableAgentClientFCPHaveAumOnly
                                                    ( AgentPK, FundClientPK )
                                            SELECT Distinct B.SellingAgentPK,A.FundClientPK FROM dbo.FundClientPosition A 
                                            LEFT JOIN FundClient B ON A.FundClientPK = B.FundClientPK AND B.status IN (1,2)
                                            WHERE Date = @DateToMinOne
                                            AND B.SellingAgentPK <> 0 AND A.UnitAmount > 1



                                            CREATE table #tableAgentClientSubsDetail
                                            (
	                                            AgentPK INT,
	                                            FundClientPK INT,
	                                            ClientSubscriptionPK INT,
	                                            Amount NUMERIC(22,4)
                                            )
                                            CREATE CLUSTERED INDEX indx_tableAgentClientSubsDetail  ON #tableAgentClientSubsDetail (AgentPK,FundClientPK,ClientSubscriptionPK);
                                            INSERT INTO #tableAgentClientSubsDetail
                                                    ( AgentPK,FundClientPK, ClientSubscriptionPK,Amount )

                                            SELECT B.SellingAgentPK,A.FundClientPK,A.ClientSubscriptionPK,A.CashAmount FROM dbo.ClientSubscription A 
                                            LEFT JOIN FundClient B ON A.FundClientPK = B.FundClientPK AND B.status IN (1,2)
                                            WHERE A.Posted = 1 
                                            AND A.Status <> 3 AND A.Revised = 0
                                            AND A.NAVDate BETWEEN @DateFrom AND @DateTo
                                            AND B.SellingAgentPK <> 0 



                                            CREATE table #tableAgentClientRedDetail
                                            (
	                                            AgentPK INT,
	                                            ClientRedemptionPK int
                                            )
                                            CREATE CLUSTERED INDEX indx_tableAgentClientRedDetail  ON #tableAgentClientRedDetail (AgentPK,ClientRedemptionPK);
                                            INSERT INTO #tableAgentClientRedDetail
                                                    ( AgentPK, ClientRedemptionPK )

                                            SELECT B.SellingAgentPK,A.ClientRedemptionPK FROM dbo.ClientRedemption A 
                                            LEFT JOIN FundClient B ON A.FundClientPK = B.FundClientPK AND B.status IN (1,2)
                                            WHERE A.Posted = 1 
                                            AND A.Status <> 3 AND A.Revised = 0
                                            AND A.NAVDate BETWEEN @DateFrom AND @DateTo
                                            AND B.SellingAgentPK <> 0 



                                            CREATE table #tableAgentFCPAum
                                            (
	                                            AgentPK INT,
	                                            AUM NUMERIC(22,4)
                                            )
                                            CREATE CLUSTERED INDEX indx_tableAgentFCPAum  ON #tableAgentFCPAum (AgentPK);
                                            INSERT INTO #tableAgentFCPAum
                                                    ( AgentPK, AUM )
                                            SELECT B.SellingAgentPK,SUM(ISNULL(A.UnitAmount,0) * ISNULL(C.Nav,0)) AUM FROM dbo.FundClientPosition A 
                                            LEFT JOIN FundClient B ON A.FundClientPK = B.FundClientPK AND B.status IN (1,2)
                                            LEFT JOIN dbo.CloseNAV C ON A.FundPK = C.FundPK AND C.date = @DateTo  AND C.status IN (1,2)
                                            WHERE A.Date = @DateToMinOne
                                            AND B.SellingAgentPK <> 0 AND A.UnitAmount > 1
                                            GROUP BY B.SellingAgentPK


                                            CREATE table #tableResult
                                            (
	                                            AgentPK INT,
	                                            Type NVARCHAR(100),
	                                            Code NVARCHAR(100),
	                                            AgentName NVARCHAR(300),
	                                            TotalNewClientThisWeek INT,
	                                            TotalAllClient INT,
	                                            TotalSubs INT,
	                                            TotalRed INT,
	                                            AUM NUMERIC(22,4),
	                                            TotalTopUp int,
	                                            AmountTopUp numeric(22,4)
                                            )
                                            CREATE CLUSTERED INDEX indx_tableResult  ON #tableResult (AgentPK);

                                            INSERT INTO #tableResult
                                            SELECT  
                                            A.AgentPK
                                            ,CASE WHEN B.Type = 1 THEN 'DIRECT' ELSE 'INDIRECT' END Type,B.ID Code,B.Name AgentName 
                                            --,FORMAT(B.JoinDate,''d-MMM-yy''
                                            ,ISNULL(C.totalNewClientThisWeek,0) totalNewClientThisWeekE6
                                            ,ISNULL(D.TotalAllClient,0) TotalAllClientE7
                                            ,ISNULL(E.TotalSubs,0) TotalSubs
                                            ,ISNULL(F.TotalRed,0) TotalRed
                                            ,ISNULL(G.AUM,0) / 1000000 AUM
                                            ,ISNULL(H.TotalTopUp,0) TotalTopUp
                                            ,ISNULL(I.AmountTopUp,0) / 1000000 AmountTopUp
                                            FROM #tableAgent A
                                            INNER JOIN Agent B ON A.AgentPK = B.AgentPK AND B.status IN (1,2)
                                            left JOIN
                                            (
	                                            SELECT A.AgentPK,COUNT(A.FundClientPK) totalNewClientThisWeek FROM #tableAgentClientSubs A
	                                            LEFT JOIN #tableAgentClientFCP B ON A.fundClientPK = B.FundClientPK 
	                                            WHERE B.FundClientPK  IS NULL
	                                            GROUP BY A.AgentPK
                                            )C on A.AgentPK = C.AgentPK 
                                            left JOIN
                                            (
	                                            SELECT A.AgentPK,COUNT(A.FundClientPK) TotalAllClient FROM #tableAgentClientFCPHaveAumOnly A
	                                            GROUP BY A.AgentPK
                                            )D on A.AgentPK = D.AgentPK 
                                            left join
                                            (
	                                            Select AgentPK,count(ClientsubscriptionPK) totalSubs from #tableAgentClientSubsDetail
	                                            group By AgentPK
                                            )E on A.AgentPK = E.AgentPK
                                            left join
                                            (
	                                            Select AgentPK,count(ClientRedemptionPK) totalRed from #tableAgentClientRedDetail
	                                            group By AgentPK
                                            )F on A.AgentPK = F.AgentPK
                                            left join
                                            (
	                                            Select * from #tableAgentFCPAum
                                            )G on A.AgentPK = G.AgentPK
                                            left JOIN
                                            (
	                                            SELECT A.AgentPK,COUNT(A.ClientsubscriptionPK) TotalTopUp FROM #tableAgentClientSubsDetail A
	                                            INNER JOIN #tableAgentClientFCP B ON A.fundClientPK = B.FundClientPK 
	                                            GROUP BY A.AgentPK
                                            )H on A.AgentPK = H.AgentPK 
                                            left JOIN
                                            (
	                                            SELECT A.AgentPK,SUM(ISNULL(Amount,0)) AmountTopUp FROM #tableAgentClientSubsDetail A
	                                            INNER JOIN #tableAgentClientFCP B ON A.fundClientPK = B.FundClientPK 
	                                            GROUP BY A.AgentPK
                                            )I on A.AgentPK = I.AgentPK 
											--where B.Type = 1






                                            DECLARE @cols AS NVARCHAR(MAX),@colsForQuery AS NVARCHAR(MAX),@colsRed AS NVARCHAR(MAX),@colsForQueryRed AS NVARCHAR(MAX),
                                                @querySubs  AS NVARCHAR(MAX)

                                            select @colsForQuery = STUFF((SELECT ',isnull(' + QUOTENAME(ID) +',0) ' + QUOTENAME(ID) 
                                                                from (SELECT DISTINCT C.ID FROM dbo.ClientSubscription A 
							                                            LEFT JOIN FundClient B ON A.FundClientPK = B.FundClientPK AND B.status IN (1,2)
							                                            left join Fund C on A.FundPK = C.FundPK and C.status in (1,2)
							                                            WHERE A.Posted = 1 
							                                            AND A.Status <> 3 AND A.Revised = 0
							                                            AND A.NAVDate BETWEEN @DateFrom AND @DateTo
							                                            AND B.SellingAgentPK <> 0 ) A
					                                            order by A.ID
                                                        FOR XML PATH(''), TYPE
                                                        ).value('.', 'NVARCHAR(MAX)') 
                                                    ,1,1,'')

                                            select @cols = STUFF((SELECT distinct ',' + QUOTENAME(C.ID) 
                                                                FROM dbo.ClientSubscription A 
							                                            LEFT JOIN FundClient B ON A.FundClientPK = B.FundClientPK AND B.status IN (1,2)
							                                            left join Fund C on A.FundPK = C.FundPK and C.status in (1,2)
							                                            WHERE A.Posted = 1 
							                                            AND A.Status <> 3 AND A.Revised = 0
							                                            AND A.NAVDate BETWEEN @DateFrom AND @DateTo
							                                            AND B.SellingAgentPK <> 0
				
                                                        FOR XML PATH(''), TYPE
                                                        ).value('.', 'NVARCHAR(MAX)') 
                                                    ,1,1,'')



                                            --select @colsForQueryRed = STUFF((SELECT ',isnull(O.' + QUOTENAME(ID) +',0) ' + QUOTENAME('RED'+ID) 
                                            --                    from (SELECT DISTINCT C.ID FROM dbo.ClientRedemption A 
                                            --							LEFT JOIN FundClient B ON A.FundClientPK = B.FundClientPK AND B.status IN (1,2)
                                            --							left join Fund C on A.FundPK = C.FundPK and C.status in (1,2)
                                            --							WHERE A.Posted = 1 
                                            --							AND A.Status <> 3 AND A.Revised = 0
                                            --							AND A.NAVDate BETWEEN @DateFrom AND @DateTo
                                            --							AND B.SellingAgentPK <> 0 ) A
                                            --					order by A.ID
                                            --            FOR XML PATH(''), TYPE
                                            --            ).value('.', 'NVARCHAR(MAX)') 
                                            --        ,1,1,'')

                                            --select @colsRed = STUFF((SELECT distinct ',O.' + QUOTENAME(C.ID) 
                                            --                    FROM dbo.ClientRedemption A 
                                            --							LEFT JOIN FundClient B ON A.FundClientPK = B.FundClientPK AND B.status IN (1,2)
                                            --							left join Fund C on A.FundPK = C.FundPK and C.status in (1,2)
                                            --							WHERE A.Posted = 1 
                                            --							AND A.Status <> 3 AND A.Revised = 0
                                            --							AND A.NAVDate BETWEEN @DateFrom AND @DateTo
                                            --							AND B.SellingAgentPK <> 0
				
                                            --            FOR XML PATH(''), TYPE
                                            --            ).value('.', 'NVARCHAR(MAX)') 
                                            --        ,1,1,'')



                                            CREATE table #tableResultSubs
                                            (

	                                            SellingAgentPK INT,
	                                            ID NVARCHAR(200),
	                                            Subs NUMERIC(22,4)
                                            )
                                            INSERT INTO #tableResultSubs
                                                    ( SellingAgentPK, ID, Subs )

                                            SELECT B.SellingAgentPK,C.ID ,A.CashAmount/1000000 Subs FROM dbo.ClientSubscription A 
					                                            LEFT JOIN FundClient B ON A.FundClientPK = B.FundClientPK AND B.status IN (1,2)
					                                            left join Fund C on A.FundPK = C.FundPK and C.status in (1,2)
                                                                INNER JOIN Agent D ON B.SellingAgentPK = D.AgentPK AND D.status IN (1,2)
					                                            WHERE A.Posted = 1 
					                                            AND A.Status <> 3 AND A.Revised = 0
					                                            AND A.NAVDate BETWEEN @DateFrom AND @DateTo
					                                            AND B.SellingAgentPK <> 0 --and D.Type = 1


                                            CREATE table #tableResultRed
                                            (

	                                            SellingAgentPK INT,
	                                            ID NVARCHAR(200),
	                                            Red NUMERIC(22,4)
                                            )
                                            INSERT INTO #tableResultRed
                                                    ( SellingAgentPK, ID, Red )

                                            SELECT B.SellingAgentPK,C.ID ,A.CashAmount/1000000 Red FROM dbo.ClientRedemption A 
					                                            LEFT JOIN FundClient B ON A.FundClientPK = B.FundClientPK AND B.status IN (1,2)
					                                            left join Fund C on A.FundPK = C.FundPK and C.status in (1,2)
                                                                INNER JOIN Agent D ON B.SellingAgentPK = D.AgentPK AND D.status IN (1,2)
					                                            WHERE A.Posted = 1 
					                                            AND A.Status <> 3 AND A.Revised = 0
					                                            AND A.NAVDate BETWEEN @DateFrom AND @DateTo
					                                            AND B.SellingAgentPK <> 0 --and D.Type = 1



                                            set @querySubs = 'SELECT B.Type,B.Code,B.Agentname,  ' + @colsForQuery + ' 
			                                            from 
                                                            (
				                                            SELECT * FROM #tableResultSubs
                                                        ) x
                                                        pivot 
                                                        (
                                                            SUM(Subs)
                                                            for ID in (' + @cols + ')
                                                        ) p
			                                            left join  #tableResult B on SellingAgentPK = B.AgentPK
                                                        where B.Type = ''INDIRECT''
			                                            order by SellingAgentPK asc
			                                            '

                                            exec(@querySubs)	";

                                                        cmd5.CommandTimeout = 0;
                                                        cmd5.Parameters.AddWithValue("@DateFrom", _unitRegistryRpt.ValueDateFrom);
                                                        cmd5.Parameters.AddWithValue("@DateTo", _unitRegistryRpt.ValueDateTo);

                                                        cmd5.ExecuteNonQuery();


                                                        using (SqlDataReader dr3 = cmd5.ExecuteReader())
                                                        {
                                                            incRowExcel = 5;
                                                            int incColExcel = 10;


                                                            worksheet1.Cells[incRowExcel, incColExcel].Value = "WEEKLY SUBSCRIPTION (Amount per Mil Rupiah)";
                                                            //worksheet1.Cells["J" + incRowExcel + ":F" + incColExcel].Merge = true;
                                                            //worksheet1.Cells["J" + incRowExcel + ":F" + incColExcel].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                            //worksheet1.Cells["J" + incRowExcel + ":F" + incColExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                            incRowExcel++;
                                                            worksheet1.Cells[incRowExcel, incColExcel].Value = "WEEKLY REDEMPTION (Amount per Mil Rupiah)";
                                                            incRowExcel++;

                                                            for (int inc4 = 3; inc4 < dr3.FieldCount; inc4++)
                                                            {

                                                                worksheet1.Cells[incRowExcel, incColExcel].Value = dr3.GetName(inc4).ToString();
                                                                //worksheet1.Cells[incRowExcel, incColExcel].Style.Numberformat.Format = "#,##0.0000";
                                                                worksheet1.Cells[incRowExcel, incColExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                                incColExcel++;
                                                            }

                                                            string _alphabet2 = _host.GetAlphabet(incColExcel - 1);

                                                            worksheet1.Cells["A" + incRowExcel + ":" + _alphabet2 + incColExcel].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                                            worksheet1.Cells["A" + incRowExcel + ":" + _alphabet2 + incColExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                                            worksheet1.Cells["A" + incRowExcel + ":" + _alphabet2 + incColExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                            worksheet1.Cells["A" + incRowExcel + ":" + _alphabet2 + incColExcel].Style.Border.Right.Style = ExcelBorderStyle.Thin;



                                                            incRowExcel++;
                                                            while (dr3.Read())
                                                            {

                                                                int RowB2 = incRowExcel;
                                                                int RowG2 = incRowExcel + 1;
                                                                int RowR2 = incRowExcel + 2;



                                                                int RowC2 = incRowExcel;
                                                                int RowF2 = incRowExcel + 1;
                                                                int RowS2 = incRowExcel + 2;
                                                                int set2 = 8;

                                                                for (int inc4 = 0; inc4 < _rowtotal2; inc4++)
                                                                {
                                                                    if (worksheet1.Cells[set2, 3].Value.ToString() == dr3.GetValue(2).ToString())
                                                                    {
                                                                        _counterRow2 = set2;
                                                                        break;
                                                                    }
                                                                    set2 = set2 + 2;
                                                                }

                                                                incColExcel = 10;
                                                                for (int inc4 = 3; inc4 < dr3.FieldCount; inc4++)
                                                                {
                                                                    worksheet1.Cells[_counterRow2, incColExcel].Value = dr3.GetValue(inc4).ToString();
                                                                    worksheet1.Cells[_counterRow2, incColExcel].Style.Numberformat.Format = "#,##0.0000";
                                                                    worksheet1.Cells[_counterRow2, incColExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                                                                    incColExcel++;
                                                                    _rowAkhir2 = incColExcel;
                                                                }

                                                                string _alphabetDetail2 = _host.GetAlphabet(incColExcel - 1);

                                                                worksheet1.Cells["A" + incRowExcel + ":" + _alphabetDetail2 + RowS2].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                                                worksheet1.Cells["A" + incRowExcel + ":" + _alphabetDetail2 + RowS2].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                                                worksheet1.Cells["A" + incRowExcel + ":" + _alphabetDetail2 + RowS2].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                                worksheet1.Cells["A" + incRowExcel + ":" + _alphabetDetail2 + RowS2].Style.Border.Right.Style = ExcelBorderStyle.Thin;



                                                                worksheet1.Cells["A" + RowB2 + ":" + _alphabetDetail2 + RowR2].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                                                worksheet1.Cells["A" + RowB2 + ":" + _alphabetDetail2 + RowR2].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                                                worksheet1.Cells["A" + RowB2 + ":" + _alphabetDetail2 + RowR2].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                                worksheet1.Cells["A" + RowB2 + ":" + _alphabetDetail2 + RowR2].Style.Border.Right.Style = ExcelBorderStyle.Thin;




                                                                _RowBaris2 = incRowExcel;
                                                                incRowExcel = incRowExcel + 2;



                                                                RowF2 = incRowExcel + 1;


                                                                //worksheet1.Cells[5, 10].Value = "WEEKLY SUBSCRIPTION (Amount per Mil Rupiah)";
                                                                ////worksheet1.Cells["E5" + ":" + _alphabet + incColExcel].Merge = true;
                                                                //worksheet1.Cells["E5" + ":" + _alphabet + incColExcel].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                                //worksheet1.Cells["E5" + ":" + _alphabet + incColExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                                //worksheet1.Cells[6, 10].Value = "WEEKLY REDEMPTION (Amount per Mil Rupiah)";
                                                                ////worksheet1.Cells["F5" + ":" + _alphabet + incColExcel].Merge = true;
                                                                //worksheet1.Cells["F5" + ":" + _alphabet + incColExcel].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                                //worksheet1.Cells["F5" + ":" + _alphabet + incColExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;





                                                            }





                                                        }
                                                    }
                                                }

                                                #endregion

                                                #region INDIRECT RED

                                                int i2 = 0;
                                                // RED
                                                using (SqlConnection DbCon5 = new SqlConnection(Tools.conString))
                                                {
                                                    DbCon5.Open();
                                                    using (SqlCommand cmd5 = DbCon5.CreateCommand())
                                                    {
                                                        cmd5.CommandText =

                                                        @"  
                                            --DROP TABLE #tableAgent 
--                                                DROP TABLE #tableAgentClientSubs
--                                                DROP TABLE #tableAgentClientFCP
--                                                DROP TABLE #tableAgentClientFCPHaveAumOnly
--                                                DROP TABLE #tableAgentClientSubsDetail
--                                                DROP TABLE #tableAgentClientRedDetail
--                                                DROP TABLE #tableAgentFCPAum
--                                                DROP TABLE #tableResult
--                                                DROP TABLE #tableResultSubs
--                                                DROP TABLE #tableResultRed

                                            DECLARE @DateToMinOne DATETIME

                                            SET @DateToMinOne = dbo.FWorkingDay(@DateTo,-1)


                                            CREATE table #tableAgent 
                                            (
	                                            AgentPK int
                                            )
                                            CREATE CLUSTERED INDEX indx_tableAgent ON #tableAgent (AgentPK);


                                            INSERT INTO #tableAgent
                                                    ( AgentPK )
                                            SELECT DISTINCT SellingAgentPK FROM FundClient WHERE status IN (1,2)
                                            AND SellingAgentPK <> 0


                                            CREATE table #tableAgentClientSubs 
                                            (
	                                            AgentPK INT,
	                                            FundClientPK int
                                            )
                                            CREATE CLUSTERED INDEX indx_AgentClientSubs   ON #tableAgentClientSubs  (AgentPK,FundClientPK);
                                            INSERT INTO #tableAgentClientSubs
                                                    ( AgentPK, FundClientPK )
                                            SELECT DISTINCT B.SellingAgentPK,A.FundClientPK FROM dbo.ClientSubscription A 
                                            LEFT JOIN FundClient B ON A.FundClientPK = B.FundClientPK AND B.status IN (1,2)
                                            WHERE A.Posted = 1 
                                            AND A.Status <> 3 AND A.Revised = 0
                                            AND A.NAVDate BETWEEN @DateFrom AND @DateTo
                                            AND B.SellingAgentPK <> 0 


                                            CREATE table #tableAgentClientFCP
                                            (
	                                            AgentPK INT,
	                                            FundClientPK int
                                            )
                                            CREATE CLUSTERED INDEX indx_tableAgentClientFCP  ON #tableAgentClientFCP (AgentPK,FundClientPK);
                                            INSERT INTO #tableAgentClientFCP
                                                    ( AgentPK, FundClientPK )
                                            SELECT Distinct B.SellingAgentPK,A.FundClientPK FROM dbo.FundClientPosition A 
                                            LEFT JOIN FundClient B ON A.FundClientPK = B.FundClientPK AND B.status IN (1,2)
                                            WHERE Date = @DateFrom
                                            AND B.SellingAgentPK <> 0

                                            CREATE table #tableAgentClientFCPHaveAumOnly
                                            (
	                                            AgentPK INT,
	                                            FundClientPK int
                                            )
                                            CREATE CLUSTERED INDEX indx_tableAgentClientFCPHaveAumOnly  ON #tableAgentClientFCPHaveAumOnly (AgentPK,FundClientPK);
                                            INSERT INTO #tableAgentClientFCPHaveAumOnly
                                                    ( AgentPK, FundClientPK )
                                            SELECT Distinct B.SellingAgentPK,A.FundClientPK FROM dbo.FundClientPosition A 
                                            LEFT JOIN FundClient B ON A.FundClientPK = B.FundClientPK AND B.status IN (1,2)
                                            WHERE Date = @DateToMinOne
                                            AND B.SellingAgentPK <> 0 AND A.UnitAmount > 1



                                            CREATE table #tableAgentClientSubsDetail
                                            (
	                                            AgentPK INT,
	                                            FundClientPK INT,
	                                            ClientSubscriptionPK INT,
	                                            Amount NUMERIC(22,4)
                                            )
                                            CREATE CLUSTERED INDEX indx_tableAgentClientSubsDetail  ON #tableAgentClientSubsDetail (AgentPK,FundClientPK,ClientSubscriptionPK);
                                            INSERT INTO #tableAgentClientSubsDetail
                                                    ( AgentPK,FundClientPK, ClientSubscriptionPK,Amount )

                                            SELECT B.SellingAgentPK,A.FundClientPK,A.ClientSubscriptionPK,A.CashAmount FROM dbo.ClientSubscription A 
                                            LEFT JOIN FundClient B ON A.FundClientPK = B.FundClientPK AND B.status IN (1,2)
                                            WHERE A.Posted = 1 
                                            AND A.Status <> 3 AND A.Revised = 0
                                            AND A.NAVDate BETWEEN @DateFrom AND @DateTo
                                            AND B.SellingAgentPK <> 0 



                                            CREATE table #tableAgentClientRedDetail
                                            (
	                                            AgentPK INT,
	                                            ClientRedemptionPK int
                                            )
                                            CREATE CLUSTERED INDEX indx_tableAgentClientRedDetail  ON #tableAgentClientRedDetail (AgentPK,ClientRedemptionPK);
                                            INSERT INTO #tableAgentClientRedDetail
                                                    ( AgentPK, ClientRedemptionPK )

                                            SELECT B.SellingAgentPK,A.ClientRedemptionPK FROM dbo.ClientRedemption A 
                                            LEFT JOIN FundClient B ON A.FundClientPK = B.FundClientPK AND B.status IN (1,2)
                                            WHERE A.Posted = 1 
                                            AND A.Status <> 3 AND A.Revised = 0
                                            AND A.NAVDate BETWEEN @DateFrom AND @DateTo
                                            AND B.SellingAgentPK <> 0 



                                            CREATE table #tableAgentFCPAum
                                            (
	                                            AgentPK INT,
	                                            AUM NUMERIC(22,4)
                                            )
                                            CREATE CLUSTERED INDEX indx_tableAgentFCPAum  ON #tableAgentFCPAum (AgentPK);
                                            INSERT INTO #tableAgentFCPAum
                                                    ( AgentPK, AUM )
                                            SELECT B.SellingAgentPK,SUM(ISNULL(A.UnitAmount,0) * ISNULL(C.Nav,0)) AUM FROM dbo.FundClientPosition A 
                                            LEFT JOIN FundClient B ON A.FundClientPK = B.FundClientPK AND B.status IN (1,2)
                                            LEFT JOIN dbo.CloseNAV C ON A.FundPK = C.FundPK AND C.date = @DateTo  AND C.status IN (1,2)
                                            WHERE A.Date = @DateToMinOne
                                            AND B.SellingAgentPK <> 0 AND A.UnitAmount > 1
                                            GROUP BY B.SellingAgentPK


                                            CREATE table #tableResult
                                            (
	                                            AgentPK INT,
	                                            Type NVARCHAR(100),
	                                            Code NVARCHAR(100),
	                                            AgentName NVARCHAR(300),
	                                            TotalNewClientThisWeek INT,
	                                            TotalAllClient INT,
	                                            TotalSubs INT,
	                                            TotalRed INT,
	                                            AUM NUMERIC(22,4),
	                                            TotalTopUp int,
	                                            AmountTopUp numeric(22,4)
                                            )
                                            CREATE CLUSTERED INDEX indx_tableResult  ON #tableResult (AgentPK);

                                            INSERT INTO #tableResult
											SELECT  
											A.AgentPK
											,CASE WHEN B.Type = 1 THEN 'DIRECT' ELSE 'INDIRECT' END Type,B.ID Code,B.Name AgentName 
											--,FORMAT(B.JoinDate,''d-MMM-yy''
											,ISNULL(C.totalNewClientThisWeek,0) totalNewClientThisWeekE6
											,ISNULL(D.TotalAllClient,0) TotalAllClientE7
											,ISNULL(E.TotalSubs,0) TotalSubs
											,ISNULL(F.TotalRed,0) TotalRed
											,ISNULL(G.AUM,0) / 1000000 AUM
											,ISNULL(H.TotalTopUp,0) TotalTopUp
											,ISNULL(I.AmountTopUp,0) / 1000000 AmountTopUp
											FROM #tableAgent A
											INNER JOIN Agent B ON A.AgentPK = B.AgentPK AND B.status IN (1,2)
											left JOIN
											(
												SELECT A.AgentPK,COUNT(A.FundClientPK) totalNewClientThisWeek FROM #tableAgentClientSubs A
												LEFT JOIN #tableAgentClientFCP B ON A.fundClientPK = B.FundClientPK 
												WHERE B.FundClientPK  IS NULL
												GROUP BY A.AgentPK
											)C on A.AgentPK = C.AgentPK 
											left JOIN
											(
												SELECT A.AgentPK,COUNT(A.FundClientPK) TotalAllClient FROM #tableAgentClientFCPHaveAumOnly A
												GROUP BY A.AgentPK
											)D on A.AgentPK = D.AgentPK 
											left join
											(
												Select AgentPK,count(ClientsubscriptionPK) totalSubs from #tableAgentClientSubsDetail
												group By AgentPK
											)E on A.AgentPK = E.AgentPK
											left join
											(
												Select AgentPK,count(ClientRedemptionPK) totalRed from #tableAgentClientRedDetail
												group By AgentPK
											)F on A.AgentPK = F.AgentPK
											left join
											(
												Select * from #tableAgentFCPAum
											)G on A.AgentPK = G.AgentPK
											left JOIN
											(
												SELECT A.AgentPK,COUNT(A.ClientsubscriptionPK) TotalTopUp FROM #tableAgentClientSubsDetail A
												INNER JOIN #tableAgentClientFCP B ON A.fundClientPK = B.FundClientPK 
												GROUP BY A.AgentPK
											)H on A.AgentPK = H.AgentPK 
											left JOIN
											(
												SELECT A.AgentPK,SUM(ISNULL(Amount,0)) AmountTopUp FROM #tableAgentClientSubsDetail A
												INNER JOIN #tableAgentClientFCP B ON A.fundClientPK = B.FundClientPK 
												GROUP BY A.AgentPK
											)I on A.AgentPK = I.AgentPK
											--where B.Type = 1




                                            DECLARE @cols AS NVARCHAR(MAX),@colsForQuery AS NVARCHAR(MAX),@colsRed AS NVARCHAR(MAX),@colsForQueryRed AS NVARCHAR(MAX),
                                                @querySubs  AS NVARCHAR(MAX)

                                            select @colsForQueryRed = STUFF((SELECT ',isnull(' + QUOTENAME(ID) +',0) ' + QUOTENAME(ID) 
                                                                from (SELECT DISTINCT C.ID FROM dbo.ClientRedemption A 
                                                						LEFT JOIN FundClient B ON A.FundClientPK = B.FundClientPK AND B.status IN (1,2)
                                                						left join Fund C on A.FundPK = C.FundPK and C.status in (1,2)
                                                						WHERE A.Posted = 1 
                                                						AND A.Status <> 3 AND A.Revised = 0
                                                						AND A.NAVDate BETWEEN @DateFrom AND @DateTo
                                                						AND B.SellingAgentPK <> 0 ) A
                                                				order by A.ID
                                                        FOR XML PATH(''), TYPE
                                                        ).value('.', 'NVARCHAR(MAX)') 
                                                    ,1,1,'')

                                            select @colsRed = STUFF((SELECT distinct ',' + QUOTENAME(C.ID) 
                                                                FROM dbo.ClientRedemption A 
                                                						LEFT JOIN FundClient B ON A.FundClientPK = B.FundClientPK AND B.status IN (1,2)
                                                						left join Fund C on A.FundPK = C.FundPK and C.status in (1,2)
                                                						WHERE A.Posted = 1 
                                                						AND A.Status <> 3 AND A.Revised = 0
                                                						AND A.NAVDate BETWEEN @DateFrom AND @DateTo
                                                						AND B.SellingAgentPK <> 0
				
                                                        FOR XML PATH(''), TYPE
                                                        ).value('.', 'NVARCHAR(MAX)') 
                                                    ,1,1,'')



                                            CREATE table #tableResultSubs
                                            (

	                                            SellingAgentPK INT,
	                                            ID NVARCHAR(200),
	                                            Subs NUMERIC(22,4)
                                            )
                                            INSERT INTO #tableResultSubs
                                                    ( SellingAgentPK, ID, Subs )

                                            SELECT B.SellingAgentPK,C.ID ,A.CashAmount/1000000 Subs FROM dbo.ClientSubscription A 
					                                            LEFT JOIN FundClient B ON A.FundClientPK = B.FundClientPK AND B.status IN (1,2)
					                                            left join Fund C on A.FundPK = C.FundPK and C.status in (1,2)
                                                                INNER JOIN Agent D ON B.SellingAgentPK = D.AgentPK AND D.status IN (1,2)
					                                            WHERE A.Posted = 1 
					                                            AND A.Status <> 3 AND A.Revised = 0
					                                            AND A.NAVDate BETWEEN @DateFrom AND @DateTo
					                                            AND B.SellingAgentPK <> 0 --and D.Type = 1


                                            CREATE table #tableResultRed
                                            (

	                                            SellingAgentPK INT,
	                                            ID NVARCHAR(200),
	                                            Red NUMERIC(22,4)
                                            )
                                            INSERT INTO #tableResultRed
                                                    ( SellingAgentPK, ID, Red )

                                            SELECT B.SellingAgentPK,C.ID ,A.CashAmount/1000000 Red FROM dbo.ClientRedemption A 
					                                            LEFT JOIN FundClient B ON A.FundClientPK = B.FundClientPK AND B.status IN (1,2)
					                                            left join Fund C on A.FundPK = C.FundPK and C.status in (1,2)
                                                                INNER JOIN Agent D ON B.SellingAgentPK = D.AgentPK AND D.status IN (1,2)
					                                            WHERE A.Posted = 1 
					                                            AND A.Status <> 3 AND A.Revised = 0
					                                            AND A.NAVDate BETWEEN @DateFrom AND @DateTo
					                                            AND B.SellingAgentPK <> 0 --and D.Type = 1



                                            set @querySubs = 'SELECT B.Type,B.Code,B.Agentname,  ' + @colsForQueryRed + ' 
			                                            from 
                                                            (
				                                            SELECT * FROM #tableResultRed
                                                        ) x
                                                        pivot 
                                                        (
                                                            SUM(red)
                                                            for ID in (' + @colsRed + ')
                                                        ) p
			                                            left join  #tableResult B on SellingAgentPK = B.AgentPK
                                                        where B.Type = ''INDIRECT''
			                                            order by SellingAgentPK desc
			                                            '

                                            exec(@querySubs) ";

                                                        cmd5.CommandTimeout = 0;
                                                        cmd5.Parameters.AddWithValue("@DateFrom", _unitRegistryRpt.ValueDateFrom);
                                                        cmd5.Parameters.AddWithValue("@DateTo", _unitRegistryRpt.ValueDateTo);

                                                        cmd5.ExecuteNonQuery();


                                                        using (SqlDataReader dr3 = cmd5.ExecuteReader())
                                                        {
                                                            incRowExcel = 8;
                                                            int incColExcel = 10;
                                                            IsiCol2 = new int[dr3.FieldCount];
                                                            IsiRow2 = new int[_rowtotal2];
                                                            _counter2 = 1;
                                                            _counterRow2 = 1;

                                                            incRowExcel++;
                                                            while (dr3.Read())
                                                            {

                                                                int RowB2 = incRowExcel;
                                                                int RowG2 = incRowExcel + 1;



                                                                int RowC2 = incRowExcel;
                                                                int RowF2 = incRowExcel + 1;
                                                                int _rowsebelumnya2 = incRowExcel - 1;
                                                                int set2 = 8;
                                                                int y2;
                                                                string _alphabetDetail2 = _host.GetAlphabet(incColExcel - 1);

                                                                //find same row


                                                                incColExcel = 10;
                                                                for (int inc3 = 0; inc3 < _rowtotal2; inc3++)
                                                                {
                                                                    if (worksheet1.Cells[set2, 3].Value.ToString() == dr3.GetValue(2).ToString())
                                                                    {
                                                                        _counterRow2 = set2 + 1;
                                                                        break;
                                                                    }
                                                                    set2 = set2 + 2;
                                                                }

                                                                set2 = 7;
                                                                i2++;
                                                                //find same column
                                                                if (_counter2 == 1)
                                                                {

                                                                    for (int inc3 = 3; inc3 < dr3.FieldCount; inc3++)
                                                                    {
                                                                        incColExcel = 10;
                                                                        string abcd = dr3.GetName(inc3).ToString();
                                                                        for (int inc2 = 3; inc2 < dr3.FieldCount; inc2++)
                                                                        {

                                                                            if (worksheet1.Cells[set2, incColExcel].Value == null)
                                                                            {
                                                                                IsiCol2[inc3] = _rowAkhir2;
                                                                                //worksheet1.Cells[set, incColExcel].Value = dr1.GetName(inc1).ToString();
                                                                                _rowAkhir2++;
                                                                                break;
                                                                            }
                                                                            else if (worksheet1.Cells[set2, incColExcel].Value.ToString() == dr3.GetName(inc3).ToString())
                                                                            {
                                                                                IsiCol2[inc3] = incColExcel;
                                                                                break;
                                                                            }
                                                                            incColExcel++;
                                                                        }

                                                                    }

                                                                    for (int inc3 = 3; inc3 < dr3.FieldCount; inc3++)
                                                                    {
                                                                        y2 = IsiCol2[inc3];
                                                                        worksheet1.Cells[7, y2].Value = dr3.GetName(inc3).ToString();
                                                                        worksheet1.Cells[7, y2].Style.Numberformat.Format = "#,##0.0000";
                                                                        worksheet1.Cells[7, y2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                                        worksheet1.Column(y2).Width = 21;
                                                                    }
                                                                    worksheet1.Cells[5, 10, 7, _rowAkhir2 - 1].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                                                    worksheet1.Cells[5, 10, 7, _rowAkhir2 - 1].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                                                    worksheet1.Cells[5, 10, 7, _rowAkhir2 - 1].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                                    worksheet1.Cells[5, 10, 7, _rowAkhir2 - 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                                                    worksheet1.Cells[5, 10, 5, _rowAkhir2 - 1].Merge = true;
                                                                    worksheet1.Cells[6, 10, 6, _rowAkhir2 - 1].Merge = true;
                                                                    _counter2++;
                                                                }

                                                                for (int inc3 = 3; inc3 < dr3.FieldCount; inc3++)
                                                                {
                                                                    y2 = IsiCol2[inc3];
                                                                    worksheet1.Cells[_counterRow2, y2].Value = dr3.GetValue(inc3).ToString();
                                                                    worksheet1.Cells[_counterRow2, y2].Style.Numberformat.Format = "#,##0.0000";
                                                                    worksheet1.Cells[_counterRow2, y2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                                }


                                                                worksheet1.Cells["A" + incRowExcel + ":" + _alphabetDetail2 + RowF2].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                                                worksheet1.Cells["A" + incRowExcel + ":" + _alphabetDetail2 + RowF2].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                                                worksheet1.Cells["A" + incRowExcel + ":" + _alphabetDetail2 + RowF2].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                                worksheet1.Cells["A" + incRowExcel + ":" + _alphabetDetail2 + RowF2].Style.Border.Right.Style = ExcelBorderStyle.Thin;



                                                                worksheet1.Cells[incRowExcel - 1, 1, _RowBaris2 -1, _rowAkhir2 - 1].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                                                worksheet1.Cells[incRowExcel - 1, 1, _RowBaris2 -1, _rowAkhir2 - 1].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                                                worksheet1.Cells[incRowExcel - 1, 1, _RowBaris2 -1, _rowAkhir2 - 1].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                                worksheet1.Cells[incRowExcel - 1, 1, _RowBaris2 -1, _rowAkhir2 - 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;




                                                                _RowBaris2 = incRowExcel;
                                                                incRowExcel = incRowExcel + 2;


                                                                RowF2 = incRowExcel + 1;


                                                                //worksheet1.Cells[5, 10].Value = "WEEKLY SUBSCRIPTION (Amount per Mil Rupiah)";
                                                                ////worksheet1.Cells["E5" + ":" + _alphabet + incColExcel].Merge = true;
                                                                //worksheet1.Cells["E5" + ":" + _alphabet + incColExcel].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                                //worksheet1.Cells["E5" + ":" + _alphabet + incColExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                                                //worksheet1.Cells[6, 10].Value = "WEEKLY REDEMPTION (Amount per Mil Rupiah)";
                                                                ////worksheet1.Cells["F5" + ":" + _alphabet + incColExcel].Merge = true;
                                                                //worksheet1.Cells["F5" + ":" + _alphabet + incColExcel].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                                                                //worksheet1.Cells["F5" + ":" + _alphabet + incColExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;





                                                            }





                                                        }
                                                    }
                                                }


                                                worksheet1.PrinterSettings.FitToPage = true;
                                                worksheet1.PrinterSettings.FitToWidth = 1;
                                                worksheet1.PrinterSettings.FitToHeight = 0;
                                                worksheet1.PrinterSettings.PrintArea = worksheet1.Cells[1, 1, incRowExcel + 8, 8];
                                                worksheet1.Column(1).Width = 6;
                                                worksheet1.Column(2).AutoFit();
                                                worksheet1.Column(3).Width = 40;
                                                worksheet1.Column(4).Width = 21;
                                                worksheet1.Column(5).Width = 21;
                                                worksheet1.Column(6).Width = 21;
                                                worksheet1.Column(7).Width = 21;
                                                worksheet1.Column(8).Width = 21;
                                                worksheet1.Column(9).Width = 21;
                                                worksheet1.Column(10).Width = 21;
                                                worksheet1.Column(11).Width = 21;
                                                worksheet1.Column(12).Width = 21;
                                                worksheet1.Column(13).Width = 21;
                                                worksheet1.Column(14).Width = 21;
                                                worksheet1.Column(15).Width = 21;
                                                worksheet1.Column(16).Width = 21;
                                                worksheet1.Column(17).Width = 21;
                                                worksheet1.Column(18).Width = 21;
                                                worksheet1.Column(19).Width = 21;
                                                worksheet1.Column(20).Width = 21;






                                                // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                                //worksheet.Cells.AutoFitColumns(); // UNTUK AUTO FIT UKURAN TULISAN DENGAN KOLOM, KEBETULAN INI BUAT HEADER AJA DIPAKENYA
                                                //worksheet.PrinterSettings.FitToPage = true;
                                                worksheet.PrinterSettings.Orientation = eOrientation.Landscape; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                                worksheet.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN
                                                worksheet.HeaderFooter.OddHeader.CenteredText = "&12 Weekly Sales Report";


                                                //Image img = Image.FromFile(Tools.ReportImage);
                                                //worksheet.HeaderFooter.OddHeader.InsertPicture(img, PictureAlignment.Left);

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



                                                //}
                                            }
                                        }
                                        }

                                    }

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

            #endregion

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


        public Boolean Settlement_ListingRpt(string _userID, InvestmentRpt _investmentRpt)
        {
            #region Rebalancing Daily
            if (_investmentRpt.ReportName.Equals("Rebalancing Daily"))
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
                            string _paramFundFrom = "";
                            string _paramFundTo = "";

                            if (!_host.findString(_investmentRpt.FundFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_investmentRpt.FundFrom))
                            {
                                _paramFund = "And A.FundPK in ( " + _investmentRpt.FundFrom + " ) ";
                            }
                            else
                            {
                                _paramFund = "";
                            }
                            cmd.CommandText =

                               @"
                            SELECT A.InstrumentID,ISNULL(A.Balance,0) Balance
                            ,ISNULL(A.AvgPrice,0) AvgPrice
                            ,ISNULL(A.MarketValue,0) MarketValue
                            ,ISNULL(A.CostValue,0) CostValue
                            ,ISNULL(B.Berat,0) Berat
                            ,ISNULL(B.berat * 0.8,0) BeratMin
                            ,ISNULL(B.berat * 1.2,0) BeratMax
                            ,CASE WHEN C.TotalMarketValue > 0
	                            THEN A.MarketValue/C.TotalMarketValue * 100 ELSE 0 END Pos1
                            ,CASE WHEN C.TotalMarketValue > 0  Then 
                            CASE WHEN A.MarketValue/C.TotalMarketValue * 100 >= ISNULL(B.berat * 0.8,0) THEN 1 ELSE 0 END END Pos2
                            ,CASE WHEN C.TotalMarketValue > 0  Then 
                            CASE WHEN A.MarketValue/C.TotalMarketValue * 100 <= ISNULL(B.berat * 1.2,0) THEN 1 ELSE 0 END END Pos3
                            ,ISNULL(A.ClosePrice,0) ClosePrice
                            FROM dbo.FundPosition A
                            LEFT JOIN 
                            (
	                            SELECT instrumentPK,Weight Berat FROM dbo.InstrumentIndex WHERE status = 2
	                            AND Date = 
	                            (
		                            SELECT MAX(Date) FROM dbo.InstrumentIndex WHERE status = 2
		                            AND Date <= @Date AND IndexPK = @IndexPK and status in (1,2)
	                            )
                            )B ON  A.InstrumentPK = B.InstrumentPK
                            LEFT JOIN 
                            (
	                            SELECT A.FundPK,SUM(ISNULL(MarketValue,0)) TotalMarketValue 
	                            FROM dbo.FundPosition A
	                            LEFT JOIN dbo.Instrument B ON A.InstrumentPK = B.InstrumentPK AND B.status IN (1,2)
	                            WHERE  A.Date = @Date and A.status = 2
	                            --AND B.InstrumentTypePK IN (1,4,16) 
	                            " + _paramFund + @"
	                            GROUP BY A.FundPK
                            )C ON A.FundPK = C.FundPK
                            LEFT JOIN Instrument D ON A.InstrumentPK = D.InstrumentPK AND D.status IN (1,2)
                            WHERE A.Date = @Date 
                            AND D.InstrumentTypePK IN (1,4,16)  and A.status = 2
                            " + _paramFund;
                            cmd.CommandTimeout = 0;
                            cmd.Parameters.AddWithValue("@Date", _investmentRpt.ValueDateFrom);
                            cmd.Parameters.AddWithValue("@IndexPK", _investmentRpt.IndexPK);
                            

                            using (SqlDataReader dr0 = cmd.ExecuteReader()) 
                            {
                                if (!dr0.HasRows)
                                {
                                    return false;
                                }
                                else
                                {
                                    string filePath = Tools.ReportsPath + "RebalancingDaily" + "_" + _userID + ".xlsx";
                                    string pdfPath = Tools.ReportsPath + "RebalancingDaily" + "_" + _userID + ".pdf";
                                    FileInfo excelFile = new FileInfo(filePath);
                                    if (excelFile.Exists)
                                    {
                                        excelFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                        excelFile = new FileInfo(filePath);
                                    }

                                    // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                    using (ExcelPackage package = new ExcelPackage(excelFile))
                                    {
                                        package.Workbook.Properties.Title = "ReportDailyTransaction";
                                        package.Workbook.Properties.Author = Tools.DefaultReportAuthor();
                                        package.Workbook.Properties.Comments = Tools.DefaultReportComments();
                                        package.Workbook.Properties.Company = Tools.DefaultReportCompany();
                                        package.Workbook.Properties.SetCustomPropertyValue("Checked by", _userID);
                                        package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", Tools.DefaultReportAssemblyName());

                                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Report Rebalancing Daily");


                                        //ATUR DATA GROUPINGNYA DULU
                                        List<RebalancingDaily> rList = new List<RebalancingDaily>();
                                        while (dr0.Read())
                                        {
                                            RebalancingDaily rSingle = new RebalancingDaily();
                                            rSingle.InstrumentCode = Convert.ToString(dr0["InstrumentID"]);
                                            rSingle.Balance = Convert.ToDecimal(dr0["Balance"]);
                                            rSingle.AvgPrice = Convert.ToDecimal(dr0["AvgPrice"]);
                                            rSingle.MarketValue = Convert.ToDecimal(dr0["MarketValue"]);
                                            rSingle.ClosePrice = Convert.ToDecimal(dr0["ClosePrice"]);
                                            rSingle.CostValue = Convert.ToDecimal(dr0["CostValue"]);
                                            rSingle.Berat = Convert.ToDecimal(dr0["Berat"]);
                                            rSingle.BeratMin = Convert.ToDecimal(dr0["BeratMin"]);
                                            rSingle.BeratMax = Convert.ToDecimal(dr0["BeratMax"]);
                                            rSingle.Pos1 = Convert.ToDecimal(dr0["Pos1"]);
                                            rSingle.Pos2 = Convert.ToBoolean(dr0["Pos2"]);
                                            rSingle.Pos3 = Convert.ToBoolean(dr0["Pos3"]);

                                            rList.Add(rSingle);

                                        }


                                        var QueryByFundID =
                                            from r in rList
                                            group r by new { } into rGroup
                                            select rGroup;

                                        int incRowExcel = 1;

                                        foreach (var rsHeader in QueryByFundID)
                                        {

                                            incRowExcel++;

                                            worksheet.Cells[incRowExcel, 1].Value = "No";
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 2].Value = "Code";
                                            worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 3].Value = "Volume(Shares)";
                                            worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 4].Value = "Average Price";
                                            worksheet.Cells[incRowExcel, 4].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 5].Value = "Market Price";
                                            worksheet.Cells[incRowExcel, 5].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 6].Value = "Cost Value";
                                            worksheet.Cells[incRowExcel, 6].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 7].Value = "Market Value";
                                            worksheet.Cells[incRowExcel, 7].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 8].Value = "Weight";
                                            worksheet.Cells[incRowExcel, 8].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 9].Value = "Min.";
                                            worksheet.Cells[incRowExcel, 9].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 10].Value = "Max.";
                                            worksheet.Cells[incRowExcel, 10].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 10].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 11].Value = "Position";
                                            worksheet.Cells[incRowExcel, 11].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 11].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 12].Value = "Weigth >= Min";
                                            worksheet.Cells[incRowExcel, 12].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 12].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 13].Value = "Weigth <= Min";
                                            worksheet.Cells[incRowExcel, 13].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 13].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells[incRowExcel, 14].Value = "Add (Less)";
                                            worksheet.Cells[incRowExcel, 14].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 14].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            worksheet.Cells["A" + incRowExcel + ":N" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells["A" + incRowExcel + ":N" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells["A" + incRowExcel + ":N" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells["A" + incRowExcel + ":N" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                            incRowExcel++;
                                            //area header

                                            int _no = 1;
                                            int _startRowDetail = incRowExcel;
                                            int _endRowDetail = 0;
                                            foreach (var rsDetail in rsHeader)
                                            {

                                                //area detail

                                                worksheet.Cells[incRowExcel, 1].Value = _no;
                                                worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 2].Value = rsDetail.InstrumentCode;
                                                worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.Balance;
                                                worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                worksheet.Cells[incRowExcel, 3].Style.Numberformat.Format = "#,##0.0000";
                                                worksheet.Cells[incRowExcel, 4].Value = rsDetail.AvgPrice;
                                                worksheet.Cells[incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##0.0000";

                                                worksheet.Cells[incRowExcel, 5].Value = rsDetail.ClosePrice;
                                                worksheet.Cells[incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0.0000";

                                                worksheet.Cells[incRowExcel, 6].Value = rsDetail.CostValue;
                                                worksheet.Cells[incRowExcel, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.0000";

                                                worksheet.Cells[incRowExcel, 7].Value = rsDetail.MarketValue;
                                                worksheet.Cells[incRowExcel, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0.0000";

                                                worksheet.Cells[incRowExcel, 8].Value = rsDetail.Berat;
                                                worksheet.Cells[incRowExcel, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0.00";

                                                worksheet.Cells[incRowExcel, 9].Value = rsDetail.BeratMin;
                                                worksheet.Cells[incRowExcel, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0.00";

                                                worksheet.Cells[incRowExcel, 10].Value = rsDetail.BeratMax;
                                                worksheet.Cells[incRowExcel, 10].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0.00";

                                                worksheet.Cells[incRowExcel, 11].Value = rsDetail.Pos1;
                                                worksheet.Cells[incRowExcel, 11].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                worksheet.Cells[incRowExcel, 11].Style.Numberformat.Format = "#,##0.00";

                                                worksheet.Cells[incRowExcel, 12].Value = rsDetail.Pos2;
                                                worksheet.Cells[incRowExcel, 12].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                

                                                worksheet.Cells[incRowExcel, 13].Value = rsDetail.Pos3;
                                                worksheet.Cells[incRowExcel, 13].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                                                if (rsDetail.Pos1 > rsDetail.BeratMax)
                                                {
                                                    worksheet.Cells[incRowExcel, 14].Value = ((((rsDetail.BeratMax / rsDetail.Pos1) * rsDetail.Balance) - rsDetail.Balance) * rsDetail.ClosePrice);
                                                }
                                                else if (rsDetail.Pos1 < rsDetail.BeratMin)
                                                {
                                                    worksheet.Cells[incRowExcel, 14].Value = ((((rsDetail.BeratMin / rsDetail.Pos1) * rsDetail.Balance) - rsDetail.Balance) * rsDetail.ClosePrice);
                                                }
                                                else
                                                {
                                                    worksheet.Cells[incRowExcel, 14].Value = "No Adjustment";
                                                   
                                                }
                                                worksheet.Cells[incRowExcel, 14].Style.Numberformat.Format = "#,##0.00";
                                                //worksheet.Cells[incRowExcel, 12].Value = rsDetail.Add;
                                                //worksheet.Cells[incRowExcel, 12].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                                //worksheet.Cells[incRowExcel, 12].Style.Numberformat.Format = "#,##0.0000";

                                                _endRowDetail = incRowExcel;
                                                _no++;
                                                incRowExcel++;

                                            }
                                            worksheet.Cells["A" + _startRowDetail + ":N" + _endRowDetail].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells["A" + _startRowDetail + ":N" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells["A" + _startRowDetail + ":N" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                            worksheet.Cells["A" + _startRowDetail + ":N" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                                            //worksheet.Cells["C" + incRowExcel + ":D" + incRowExcel].Style.Font.Bold = true;
                                            //worksheet.Cells["C" + incRowExcel + ":D" + incRowExcel].Value = "TOTAL Transaksi";
                                            //worksheet.Cells["C" + incRowExcel + ":D" + incRowExcel].Merge = true;
                                            //worksheet.Cells["C" + incRowExcel + ":D" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells[incRowExcel, 6].Formula = "SUM(F" + _startRowDetail + ":F" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 6].Calculate();
                                            worksheet.Cells[incRowExcel, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                            worksheet.Cells[incRowExcel, 6].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0.0000";

                                            worksheet.Cells[incRowExcel, 7].Formula = "SUM(G" + _startRowDetail + ":G" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 7].Calculate();
                                            worksheet.Cells[incRowExcel, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                            worksheet.Cells[incRowExcel, 7].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0.0000";

                                            worksheet.Cells[incRowExcel, 8].Formula = "SUM(H" + _startRowDetail + ":H" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 8].Calculate();
                                            worksheet.Cells[incRowExcel, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                            worksheet.Cells[incRowExcel, 8].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0.0000";




                                            //worksheet.Cells["A" + incRowExcel + ":E" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                            //worksheet.Cells["A" + incRowExcel + ":E" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                            //worksheet.Cells["A" + incRowExcel + ":E" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                            //worksheet.Cells["A" + incRowExcel + ":E" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                                            incRowExcel++;

                                        }

                                        worksheet.PrinterSettings.FitToPage = true;
                                        worksheet.PrinterSettings.FitToWidth = 1;
                                        worksheet.PrinterSettings.FitToHeight = 0;
                                        worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel - 1, 14];
                                        worksheet.Column(1).Width = 5;
                                        worksheet.Column(2).Width = 15;
                                        worksheet.Column(3).Width = 20;
                                        worksheet.Column(4).Width = 20;
                                        worksheet.Column(5).Width = 25;
                                        worksheet.Column(6).Width = 20;
                                        worksheet.Column(7).Width = 20;
                                        worksheet.Column(8).Width = 15;
                                        worksheet.Column(9).Width = 15;
                                        worksheet.Column(10).Width = 15;
                                        worksheet.Column(11).Width = 20;
                                        worksheet.Column(12).Width = 15;
                                        worksheet.Column(13).Width = 15;
                                        worksheet.Column(14).Width = 25;


                                        // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                        //worksheet.Cells["A3:I14"].AutoFitColumns(); // UNTUK AUTO FIT UKURAN TULISAN DENGAN KOLOM, KEBETULAN INI BUAT HEADER AJA DIPAKENYA
                                        //worksheetApproved.Cells["A3:E3"].AutoFitColumns();
                                        worksheet.PrinterSettings.Orientation = eOrientation.Landscape; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                        worksheet.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN

                                        //worksheet.HeaderFooter.OddHeader.CenteredText = "\n \n \n \n \n \n \n &30&B SUBSCRIPTION \n &28&B Batch Form";

                                        worksheet.PrinterSettings.TopMargin = (decimal).5 / 2.54M; // narrow border
                                        worksheet.PrinterSettings.RightMargin = (decimal).5 / 2.54M; //narrow borderExcelWorksheet ws = pck.Workbook.Worksheets.Add("Demo");
                                        worksheet.PrinterSettings.LeftMargin = (decimal).5 / 2.54M; // narrow border

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
                                        //worksheet.HeaderFooter.OddFooter.LeftAlignedText = Tools.DefaultReportFooterLeftText();



                                        package.Save();
                                        if (_investmentRpt.DownloadMode == "PDF")
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


        public string CompareIncomeStatement(string _userID, AccountingRpt _accountingRpt)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                string FilePath = Tools.ReportsPath + "CompareIncomeStatement" + "_" + _userID + ".xlsx";
                File.Copy(Tools.ReportsTemplatePath + "\\14\\" + "14_CompareIncomeStatement.xlsx", FilePath, true);
                FileInfo existingFile = new FileInfo(FilePath);
                using (ExcelPackage package = new ExcelPackage(existingFile))
                {
                    ExcelWorksheet worksheet2 = package.Workbook.Worksheets[2];

                    using (SqlConnection DbCon01 = new SqlConnection(Tools.conString))
                    {
                        DbCon01.Open();
                        using (SqlCommand cmd01 = DbCon01.CreateCommand())
                        {
                            cmd01.CommandText = @"select a.AccountPK, * from templatereport a 
                            --left join Account b on b.AccountPK = a.AccountPK and b.Status in(1,2)
                            left join JournalDetail c on a.AccountPK = c.AccountPK and c.Status in(1,2)
                            where a.AccountPK in(3,5,6,7)
                            order by a.AccountPK,a.Row";
                            cmd01.Parameters.AddWithValue("@Date", _accountingRpt.ValueDateFrom);

                            using (SqlDataReader dr01 = cmd01.ExecuteReader())
                            {
                                if (dr01.HasRows)
                                {
                                    List<CompareIncomeStatementRpt> rList = new List<CompareIncomeStatementRpt>();
                                    while (dr01.Read())
                                    {
                                        CompareIncomeStatementRpt rSingle = new CompareIncomeStatementRpt();
                                        rSingle.Row = Convert.ToInt32(dr01["Row"]);
                                        rSingle.Col = Convert.ToInt32(dr01["Col"]);
                                        rSingle.Value = Convert.ToDecimal(dr01["Amount"]);
                                        rSingle.ValueLastYear = Convert.ToDecimal(dr01["Amount"]);
                                        rList.Add(rSingle);
                                    }
                                    var QueryByClientID =
                                     from r in rList
                                     group r by new { } into rGroup
                                     select rGroup;

                                    foreach (var rsHeader in QueryByClientID)
                                    {
                                        var _date = Convert.ToDateTime(_accountingRpt.ValueDateFrom).ToString("dd-MMM-yy");
                                        worksheet2.Cells[7, 6].Value = _date;
                                        foreach (var rsDetail in rsHeader)
                                        {

                                            worksheet2.Cells[rsDetail.Row, rsDetail.Col].Value = rsDetail.Value;
                                            worksheet2.Cells[rsDetail.Row, rsDetail.Col].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                            worksheet2.Cells[rsDetail.Row, rsDetail.Col].Style.Numberformat.Format = "#,##0.0000";

                                            worksheet2.Cells[rsDetail.Row, rsDetail.Col].Value = rsDetail.ValueLastYear;
                                            worksheet2.Cells[rsDetail.Row, rsDetail.Col].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                            worksheet2.Cells[rsDetail.Row, rsDetail.Col].Style.Numberformat.Format = "#,##0.0000";




                                        }
                                        //int _IncRowA = incRowExcel + 1000;
                                        //worksheet2.Cells["A" + incRowExcel + ":I" + _IncRowA].Style.Fill.PatternType = ExcelFillStyle.None;
                                        worksheet2.Cells[20, 6].Calculate();
                                        worksheet2.Cells[29, 6].Calculate();
                                        worksheet2.Cells[32, 6].Calculate();
                                        worksheet2.Cells[44, 6].Calculate();
                                        worksheet2.Cells[46, 6].Calculate();
                                    }

                                    //worksheet2.PrinterSettings.FitToPage = true;
                                    //worksheet2.PrinterSettings.FitToWidth = 1;
                                    //worksheet2.PrinterSettings.FitToHeight = 0;
                                    //worksheet2.PrinterSettings.PrintArea = worksheet2.Cells[1, 1, incRowExcel - 1, 9];
                                    //worksheet2.Column(1).AutoFit();
                                    //worksheet2.Column(2).AutoFit();
                                    //worksheet2.Column(3).AutoFit();
                                    //worksheet2.Column(4).AutoFit();
                                    //worksheet2.Column(5).AutoFit();
                                    //worksheet2.Column(6).AutoFit();
                                    //worksheet2.Column(7).AutoFit();
                                    //worksheet2.Column(8).AutoFit();
                                    //worksheet2.Column(9).AutoFit();

                                }


                            }

                        }
                    }
                    #region sheet 3
                    // MKBD02
                    ExcelWorksheet worksheet3 = package.Workbook.Worksheets[3];
                    using (SqlConnection DbCon02 = new SqlConnection(Tools.conString))
                    {
                        DbCon02.Open();
                        using (SqlCommand cmd02 = DbCon02.CreateCommand())
                        {
                            cmd02.CommandText = @"select a.AccountPK, a.Row, a.Col, isnull(c.Amount,0) Amount from templatereport a 
                            --left join Account b on b.AccountPK = a.AccountPK and b.Status in(1,2)
                            left join JournalDetail c on a.AccountPK = c.AccountPK and c.Status in(1,2)
                            where a.AccountPK not in(3,5,6,7)
                            order by a.AccountPK,a.Row";
                            cmd02.Parameters.AddWithValue("@Date", _accountingRpt.ValueDateFrom);

                            using (SqlDataReader dr02 = cmd02.ExecuteReader())
                            {
                                if (dr02.HasRows)
                                {
                                    List<CompareIncomeStatementRpt> rList = new List<CompareIncomeStatementRpt>();
                                    while (dr02.Read())
                                    {
                                        CompareIncomeStatementRpt rSingle = new CompareIncomeStatementRpt();
                                        rSingle.Row = Convert.ToInt32(dr02["Row"]);
                                        rSingle.Col = Convert.ToInt32(dr02["Col"]);
                                        rSingle.Value = Convert.ToDecimal(dr02["Amount"]);
                                        rSingle.ValueLastYear = Convert.ToDecimal(dr02["Amount"]);
                                        rList.Add(rSingle);
                                    }
                                    var QueryByClientID2 =
                                     from r in rList
                                     group r by new { } into rGroup
                                     select rGroup;

                                    foreach (var rsHeader in QueryByClientID2)
                                    {
                                        foreach (var rsDetail in rsHeader)
                                        {

                                            worksheet3.Cells[rsDetail.Row, rsDetail.Col].Value = rsDetail.Value;
                                            worksheet3.Cells[rsDetail.Row, rsDetail.Col].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                            worksheet3.Cells[rsDetail.Row, rsDetail.Col].Style.Numberformat.Format = "#,##0.0000";

                                            worksheet3.Cells[rsDetail.Row, rsDetail.Col].Value = rsDetail.ValueLastYear;
                                            worksheet3.Cells[rsDetail.Row, rsDetail.Col].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                            worksheet3.Cells[rsDetail.Row, rsDetail.Col].Style.Numberformat.Format = "#,##0.0000";
                                            
                                        }
                                        
                                    }


                                }


                            }

                        }

                    }
                    #endregion


                    package.Save();
                    return FilePath;
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public Boolean GenerateRptFundClientByInvestorType(string _userID, FundClientRpt _fundClientRpt)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_fundClientRpt.InvestorType == 1)
                    {
                        string _paramDateFrom = "";
                        if (_fundClientRpt.ParamDate == "1")
                        {
                            _paramDateFrom = " and FC.ExpiredDateIdentitasInd1 <> '01/01/1900' and FC.ExpiredDateIdentitasInd1 is not null  and FC.ExpiredDateIdentitasInd1 Between '" + _fundClientRpt.DateFrom + "'  " + "and '" + _fundClientRpt.DateTo + "'";
                        }
                        else if (_fundClientRpt.ParamDate == "9")
                        {
                            _paramDateFrom = " and FC.DatePengkinianData <> '01/01/1900' and FC.DatePengkinianData is not null and FC.DatePengkinianData Between  '" + _fundClientRpt.DateFrom + "'  " + "and '" + _fundClientRpt.DateTo + "'";
                        }
                        else if (_fundClientRpt.ParamDate == "10")
                        {
                            if (_fundClientRpt.ParamMonth != "")
                            {
                                if (_fundClientRpt.ParamMonth == "1")
                                {
                                    _paramDateFrom = " and FC.TanggalLahir <> '01/01/1900' and MONTH(FC.TanggalLahir) = 1";
                                }
                                else if (_fundClientRpt.ParamMonth == "2")
                                {
                                    _paramDateFrom = " and FC.TanggalLahir <> '02/01/1900' and MONTH(FC.TanggalLahir) = 2";
                                }
                                else if (_fundClientRpt.ParamMonth == "3")
                                {
                                    _paramDateFrom = " and FC.TanggalLahir <> '01/01/1900' and MONTH(FC.TanggalLahir) = 3";
                                }
                                else if (_fundClientRpt.ParamMonth == "4")
                                {
                                    _paramDateFrom = " and FC.TanggalLahir <> '01/01/1900' and MONTH(FC.TanggalLahir) = 4";
                                }
                                else if (_fundClientRpt.ParamMonth == "5")
                                {
                                    _paramDateFrom = " and FC.TanggalLahir <> '01/01/1900' and MONTH(FC.TanggalLahir) = 5";
                                }
                                else if (_fundClientRpt.ParamMonth == "6")
                                {
                                    _paramDateFrom = " and FC.TanggalLahir <> '01/01/1900' and MONTH(FC.TanggalLahir) = 6";
                                }
                                else if (_fundClientRpt.ParamMonth == "7")
                                {
                                    _paramDateFrom = " and FC.TanggalLahir <> '01/01/1900' and MONTH(FC.TanggalLahir) = 7";
                                }
                                else if (_fundClientRpt.ParamMonth == "8")
                                {
                                    _paramDateFrom = " and FC.TanggalLahir <> '01/01/1900' and MONTH(FC.TanggalLahir) = 8";
                                }
                                else if (_fundClientRpt.ParamMonth == "9")
                                {
                                    _paramDateFrom = " and FC.TanggalLahir <> '01/01/1900' and MONTH(FC.TanggalLahir) = 9";
                                }
                                else if (_fundClientRpt.ParamMonth == "10")
                                {
                                    _paramDateFrom = " and FC.TanggalLahir <> '01/01/1900' and MONTH(FC.TanggalLahir) = 10";
                                }
                                else if (_fundClientRpt.ParamMonth == "11")
                                {
                                    _paramDateFrom = " and FC.TanggalLahir <> '01/01/1900' and MONTH(FC.TanggalLahir) = 11";
                                }
                                else if (_fundClientRpt.ParamMonth == "12")
                                {
                                    _paramDateFrom = " and FC.TanggalLahir <> '01/01/1900' and MONTH(FC.TanggalLahir) = 12";
                                }

                            }
                            else if (_fundClientRpt.ParamMonth == "")
                            {
                                _paramDateFrom = @" and FC.TanggalLahir is not null and (month(FC.TanggalLahir)) between (month(@DateFrom)) and (month(@DateTo)) 
							    and (day(FC.TanggalLahir)) between (day(@DateFrom)) and (day(@DateTo))";
                            }

                        }
                        else if (_fundClientRpt.ParamDate == "11")
                        {
                            _paramDateFrom = " and FC.Agama = " + _fundClientRpt.ParamReligion;
                        }
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            string _cmd = @" 
                            select mv13.DescOne Religion,Case When fc.jeniskelamin = 1 then 'MALE' ELSE CASE WHEN FC.JenisKelamin = 2 THEN 'FEMALE' ELSE '' END END SEX,*
                            from FundClient fc   
                            left join MasterValue mv13 on fc.Agama = mv13.code and  mv13.ID = 'Religion' and mv13.status = 2  
                            where  FC.status in(1,2) and FC.InvestorType = @InvestorType " + _paramDateFrom + @" ";

                            cmd.CommandText = _cmd;
                            cmd.CommandTimeout = 0;
                            cmd.Parameters.AddWithValue("@InvestorType", _fundClientRpt.InvestorType);
                            cmd.Parameters.AddWithValue("@DateFrom", _fundClientRpt.DateFrom);
                            cmd.Parameters.AddWithValue("@DateTo", _fundClientRpt.DateTo);
                            using (SqlDataReader dr0 = cmd.ExecuteReader())
                            {
                                if (!dr0.HasRows)
                                {
                                    return false;
                                }
                                else
                                {
                                    string filePath = Tools.ReportsPath + "FundClientByIndividual" + "_" + _userID + ".xlsx";
                                    FileInfo excelFile = new FileInfo(filePath);
                                    if (excelFile.Exists)
                                    {
                                        excelFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                        excelFile = new FileInfo(filePath);
                                    }

                                    // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                    using (ExcelPackage package = new ExcelPackage(excelFile))
                                    {
                                        package.Workbook.Properties.Title = "FundClientByIndividual";
                                        package.Workbook.Properties.Author = Tools.DefaultReportAuthor();
                                        package.Workbook.Properties.Comments = Tools.DefaultReportComments();
                                        package.Workbook.Properties.Company = Tools.DefaultReportCompany();
                                        package.Workbook.Properties.SetCustomPropertyValue("Checked by", _userID);
                                        package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", Tools.DefaultReportAssemblyName());

                                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("FundClientByIndividual");


                                        //ATUR DATA GROUPINGNYA DULU
                                        List<FundClientRptByInvestorType> rList = new List<FundClientRptByInvestorType>();
                                        while (dr0.Read())
                                        {
                                            FundClientRptByInvestorType rSingle = new FundClientRptByInvestorType();
                                            rSingle.FundClientID = dr0["ID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["ID"]);
                                            rSingle.InternalName = dr0["Name"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["Name"]);
                                            rSingle.DOB = dr0["TanggalLahir"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["TanggalLahir"]);
                                            rSingle.Religion = dr0["Religion"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["Religion"]);
                                            rSingle.GenderSex = dr0["SEX"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["SEX"]);
                                            rSingle.Email = dr0["Email"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["Email"]);
                                            rSingle.AlamatInd1 = dr0["AlamatInd1"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["AlamatInd1"]);
                                            rSingle.MobilePhone = dr0["TeleponSelular"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["TeleponSelular"]);
                                            rSingle.PhoneNumber = dr0["TeleponRumah"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["TeleponRumah"]);
                                            rSingle.RegistrationDateIdentitasInd1 = dr0["RegistrationDateIdentitasInd1"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["RegistrationDateIdentitasInd1"]);
                                            rSingle.ExpiredDateIdentitasInd1 = dr0["ExpiredDateIdentitasInd1"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["ExpiredDateIdentitasInd1"]);
                                            rSingle.ExpiredDateIdentitasInd2 = dr0["ExpiredDateIdentitasInd2"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["ExpiredDateIdentitasInd2"]);
                                            rSingle.ExpiredDateIdentitasInd3 = dr0["ExpiredDateIdentitasInd3"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["ExpiredDateIdentitasInd3"]);

                                            rList.Add(rSingle);

                                        }

                                        var GroupByTitle =
                                            from r in rList
                                            group r by new { r.InvestorType } into rGroup
                                            select rGroup;

                                        int incRowExcel = 3;

                                        foreach (var rsHeader in GroupByTitle)
                                        {

                                            incRowExcel++;

                                            worksheet.Cells[incRowExcel, 1].Value = "List Ulang Tahun Nasabah";
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Size = 17;
                                            incRowExcel = incRowExcel + 3;
                                            worksheet.Cells[incRowExcel, 1].Value = _host.Get_CompanyName();
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Size = 15;
                                            incRowExcel = incRowExcel + 5;

                                            worksheet.Cells[incRowExcel, 1].Value = "ID";
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 2].Value = "Name";
                                            worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                                
                                            if (_fundClientRpt.ParamDate == "10")
                                            {worksheet.Cells[incRowExcel, 3].Value = "Birth Date";
                                                worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 4].Value = "Sex";
                                                worksheet.Cells[incRowExcel, 4].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 5].Value = "Phone Number";
                                                worksheet.Cells[incRowExcel, 5].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 6].Value = "Email";
                                                worksheet.Cells[incRowExcel, 6].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 7].Value = "Address";
                                                worksheet.Cells[incRowExcel, 7].Style.Font.Bold = true;

                                                worksheet.Cells["A" + incRowExcel + ":G" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + incRowExcel + ":G" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + incRowExcel + ":G" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + incRowExcel + ":G" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                            }
                                            else if (_fundClientRpt.ParamDate == "11")
                                            {
                                                worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 3].Value = "Religion";
                                                worksheet.Cells[incRowExcel, 4].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 4].Value = "Sex";
                                                worksheet.Cells[incRowExcel, 5].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 5].Value = "Mobile Phone Number";
                                                worksheet.Cells[incRowExcel, 6].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 6].Value = "Email";
                                                worksheet.Cells[incRowExcel, 7].Style.Font.Bold = true;
                                                worksheet.Cells[incRowExcel, 7].Value = "Address";

                                                worksheet.Cells["A" + incRowExcel + ":G" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + incRowExcel + ":G" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + incRowExcel + ":G" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + incRowExcel + ":G" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                            }


                                            incRowExcel++;


                                            int _startRowDetail = incRowExcel;
                                            int _endRowDetail = 0;
                                            //end area header
                                            foreach (var rsDetail in rsHeader)
                                            {

                                                worksheet.Cells[incRowExcel, 1].Value = rsDetail.FundClientID;
                                                worksheet.Cells[incRowExcel, 2].Value = rsDetail.InternalName;
                                                if (_fundClientRpt.ParamDate == "11")
                                                {
                                                    worksheet.Cells[incRowExcel, 3].Value = rsDetail.Religion;
                                                    worksheet.Cells[incRowExcel, 4].Value = rsDetail.GenderSex;
                                                    worksheet.Cells[incRowExcel, 5].Value = rsDetail.PhoneNumber;
                                                    worksheet.Cells[incRowExcel, 6].Value = rsDetail.Email;
                                                    worksheet.Cells[incRowExcel, 7].Value = rsDetail.AlamatInd1;

                                                    worksheet.Cells["A" + incRowExcel + ":C" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                    worksheet.Cells["A" + incRowExcel + ":C" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                                }
                                                else if (_fundClientRpt.ParamDate == "10")
                                                {
                                                    worksheet.Cells[incRowExcel, 3].Value = Convert.ToDateTime(rsDetail.DOB).ToString("dd/MM/yyyy");
                                                    worksheet.Cells[incRowExcel, 4].Value = rsDetail.GenderSex;
                                                    worksheet.Cells[incRowExcel, 5].Value = rsDetail.MobilePhone;
                                                    worksheet.Cells[incRowExcel, 6].Value = rsDetail.Email;
                                                    worksheet.Cells[incRowExcel, 7].Value = rsDetail.AlamatInd1;

                                                }

                                                _endRowDetail = incRowExcel;

                                                incRowExcel++;

                                            }

                                            if (_fundClientRpt.ParamDate == "1" || _fundClientRpt.ParamDate == "2")
                                            {
                                                worksheet.Cells["A" + _endRowDetail + ":B" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                            }
                                            else if (_fundClientRpt.ParamDate == "9")
                                            {
                                                worksheet.Cells["A" + _endRowDetail + ":D" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                            }
                                            else 
                                            {
                                                worksheet.Cells["A" + _endRowDetail + ":G" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                            }



                                        }

                                        // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                        worksheet.Cells.AutoFitColumns(0);
                                        worksheet.PrinterSettings.Orientation = eOrientation.Landscape; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                        // worksheet.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN
                                        // worksheet.PrinterSettings.FitToPage = true;
                                        //worksheet.HeaderFooter.OddHeader.RightAlignedText = "&14 PAYMENT VOUCHER";
                                        // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                        worksheet.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
                                        worksheet.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
                                        //worksheet.View.PageLayoutView = Tools.DefaultReportPageLayoutView();
                                        worksheet.View.ShowGridLines = Tools.DefaultReportShowGridLines();
                                        worksheet.HeaderFooter.OddHeader.CenteredText = "&14 Fund Client";
                                        worksheet.HeaderFooter.OddHeader.LeftAlignedText = Tools.DefaultReportHeaderLeftText();

                                        Image img = Image.FromFile(Tools.ReportImage);
                                        worksheet.HeaderFooter.OddHeader.InsertPicture(img, PictureAlignment.Right);
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
                    else if (_fundClientRpt.InvestorType == 2)
                    {
                        string _paramDateFrom = "";
                        if (_fundClientRpt.ParamDate == "3")
                        {
                            _paramDateFrom = " and FC.ExpiredDateSKD <> '01/01/1900' and FC.ExpiredDateSKD is not null and FC.ExpiredDateSKD Between  '" + _fundClientRpt.DateFrom + "'  " + "and '" + _fundClientRpt.DateTo + "'";
                        }
                        else if (_fundClientRpt.ParamDate == "4")
                        {
                            _paramDateFrom = " and FC.SIUPExpirationDate <> '01/01/1900' and FC.SIUPExpirationDate is not null and FC.SIUPExpirationDate Between  '" + _fundClientRpt.DateFrom + "'  " + "and '" + _fundClientRpt.DateTo + "'";
                        }
                        else if (_fundClientRpt.ParamDate == "5")
                        {
                            _paramDateFrom = " and FC.ExpiredDateIdentitasIns11 <> '01/01/1900' and FC.ExpiredDateIdentitasIns11 is not null and FC.ExpiredDateIdentitasIns11 Between  '" + _fundClientRpt.DateFrom + "'  " + "and '" + _fundClientRpt.DateTo + "'";
                        }
                        else if (_fundClientRpt.ParamDate == "6")
                        {
                            _paramDateFrom = " and FC.ExpiredDateIdentitasIns12 <> '01/01/1900' and FC.ExpiredDateIdentitasIns12 is not null and FC.ExpiredDateIdentitasIns12 Between  '" + _fundClientRpt.DateFrom + "'  " + "and '" + _fundClientRpt.DateTo + "'";
                        }
                        else if (_fundClientRpt.ParamDate == "7")
                        {
                            _paramDateFrom = " and FC.ExpiredDateIdentitasIns21 <> '01/01/1900' and FC.ExpiredDateIdentitasIns21 is not null and FC.ExpiredDateIdentitasIns21 Between  '" + _fundClientRpt.DateFrom + "'  " + "and '" + _fundClientRpt.DateTo + "'";
                        }
                        else if (_fundClientRpt.ParamDate == "8")
                        {
                            _paramDateFrom = " and FC.ExpiredDateIdentitasIns22 <> '01/01/1900' and FC.ExpiredDateIdentitasIns22 is not null and FC.ExpiredDateIdentitasIns22 Between  '" + _fundClientRpt.DateFrom + "'  " + "and '" + _fundClientRpt.DateTo + "'";
                        }
                        else if (_fundClientRpt.ParamDate == "9")
                        {
                            _paramDateFrom = " and FC.DatePengkinianData <> '01/01/1900' and FC.DatePengkinianData is not null and FC.DatePengkinianData Between  '" + _fundClientRpt.DateFrom + "'  " + "and '" + _fundClientRpt.DateTo + "'";
                        }
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = @"
                                select FC.ID FundClientID,mv8.DescOne InvestorType,isnull(IC.Name,'') InternalCategory,  FC.NAME InternalName,A.Name SellingAgent,FC.SID,FC.IFUACode,
                                mv33.DescOne InvestorsRiskProfile, FC.DormantDate, case when FC.BitIsAfiliated=0 then 'No' else 'Yes' end Affiliated, Z.NAME AffiliatedWith,
                                case when FC.BitIsSuspend=0 then 'No' else 'Yes' end Suspended,mv69.DescOne KYCRiskProfile,
                                FC.NPWP,FC.RegistrationNPWP,FC.CompanyMail Email,FC.TeleponBisnis PhoneNumber,FC.TeleponSelular MobilePhone,FC.Companyfax Fax,
                                mv24.DescOne Country,mv25.DescOne Nationality,fc.SACode,mv43.DescOne CountryofDomicile,mv3.DescOne CityOfEstablishment,fc.TeleponBisnis BusinessPhone, 
                                BC4.ID BankRDN,Fc.RDNAccountNo RDNAccountName, FC.RDNAccountName RDNAccountNumber,
                                BC1.ID BankName1,Fc.NamaNasabah1 BankAccountName1, FC.NomorRekening1 BankAccountNumber1, FC.BankBranchName1, Y.ID Currency1, BC1.Name BICode1,BC1.Name BIMemberCode1,
                                BC1.ID BankName2,Fc.NamaNasabah2 BankAccountName2, FC.NomorRekening2 BankAccountNumber2, FC.BankBranchName2, W.ID Currency2, BC2.Name BICode2,BC2.Name BIMemberCode2, 
                                BC1.ID BankName3,Fc.NamaNasabah3 BankAccountName3, FC.NomorRekening3 BankAccountNumber3, FC.BankBranchName3, V.ID Currency3, BC3.Name BICode3,BC3.Name BIMemberCode3,  
                                mv34.DescOne AssetOwner,mv35.DescOne StatementType,mv36.DescOne FATCAStatus,FC.TIN,mv37.DescOne TINIssuanceCountry, FC.GIIN, FC.SubstantialOwnerName,  
                                FC.SubstantialOwnerAddress, FC.SubstantialOwnerTIN,Fc.NamaPerusahaan CompanyName, FC.AlamatPerusahaan CompanyAddress, FC.KodePosIns CompanyZipCode, mv3.DescOne CompanyCity, mv17.DescOne CompanyLegalDomicile, 
                                FC.TanggalBerdiri EstablishmentDate,FC.LokasiBerdiri EstablishmentPlace, mv44.DescOne CountryofEstablishment, FC.NoSKD SKDNumber,  FC.ExpiredDateSKD,
                                FC.NomorAnggaran ArticleOfAssociation, FC.NomorSIUP SIUPNumber,FC.SIUPExpirationDate ,FC.AssetFor1Year, FC.AssetFor2Year, FC.AssetFor3Year,
                                FC.OperatingProfitFor1Year,FC.OperatingProfitFor2Year,FC.OperatingProfitFor3Year,
                                mv18.DescOne CompanyType, mv19.DescOne CompanyCharacteristic,mv20.DescOne CompanyIncomePerAnnum,  
                                mv21.DescOne CompanySourceOfFunds,mv22.DescOne CompanyInvestmentObjective,   
                                mv45.DescOne CountryofCompany, mv46.DescOne CompanyCityName, mv26.DescOne Province, 
                                FC.NamaDepanIns1 FirstNameOfficer1,FC.NamaTengahIns1 MiddleNameOfficer1,FC.NamaBelakangIns1 LastNameOfficer1,FC.Jabatan1 PositionOfficer1,FC.PhoneIns1 PhoneNumberOfficer1, FC.EmailIns1 EmailOfficer1,
                                FC.IdentitasIns11 IDType1Officer1, FC.NoIdentitasIns11 IDNumber1Officer1, FC.RegistrationDateIdentitasIns11 IDRegDate1Officer1, FC.ExpiredDateIdentitasIns11 IDExpireDate1Officer1,
                                FC.IdentitasIns12 IDType2Officer1, FC.NoIdentitasIns12 IDNumber2Officer1, FC.RegistrationDateIdentitasIns12 IDRegDate2Officer1, FC.ExpiredDateIdentitasIns12 IDExpireDate2Officer1,
                                FC.IdentitasIns13 IDType3Officer1, FC.NoIdentitasIns13 IDNumber3Officer1, FC.RegistrationDateIdentitasIns13 IDRegDate3Officer1, FC.ExpiredDateIdentitasIns13 IDExpireDate3Officer1,
                                FC.IdentitasIns14 IDType4Officer1, FC.NoIdentitasIns14 IDNumber4Officer1, FC.RegistrationDateIdentitasIns14 IDRegDate4Officer1, FC.ExpiredDateIdentitasIns14 IDExpireDate4Officer1,
                                FC.NamaDepanIns2 FirstNameOfficer2,FC.NamaTengahIns2 MiddleNameOfficer2,FC.NamaBelakangIns2 LastNameOfficer2,FC.Jabatan2 PositionOfficer2,
                                FC.IdentitasIns21 IDType1Officer2, FC.NoIdentitasIns21 IDNumber1Officer2, FC.RegistrationDateIdentitasIns21 IDRegDate1Officer2, FC.ExpiredDateIdentitasIns21 IDExpireDate1Officer2,
                                FC.IdentitasIns22 IDType2Officer2, FC.NoIdentitasIns22 IDNumber2Officer2, FC.RegistrationDateIdentitasIns22 IDRegDate2Officer2, FC.ExpiredDateIdentitasIns22 IDExpireDate2Officer2,
                                FC.IdentitasIns23 IDType3Officer2, FC.NoIdentitasIns23 IDNumber3Officer2, FC.RegistrationDateIdentitasIns23 IDRegDate3Officer2, FC.ExpiredDateIdentitasIns23 IDExpireDate3Officer2,
                                FC.IdentitasIns24 IDType4Officer2, FC.NoIdentitasIns24 IDNumber4Officer2, FC.RegistrationDateIdentitasIns24 IDRegDate4Officer2, FC.ExpiredDateIdentitasIns24 IDExpireDate4Officer2,
                                FC.NamaDepanIns3 FirstNameOfficer3,FC.NamaTengahIns3 MiddleNameOfficer3,FC.NamaBelakangIns3 LastNameOfficer3,FC.Jabatan3 PositionOfficer3,
                                FC.IdentitasIns31 IDType1Officer3, FC.NoIdentitasIns31 IDNumber1Officer3, FC.RegistrationDateIdentitasIns31 IDRegDate1Officer3, FC.ExpiredDateIdentitasIns31 IDExpireDate1Officer3,
                                FC.IdentitasIns32 IDType2Officer3, FC.NoIdentitasIns32 IDNumber2Officer3, FC.RegistrationDateIdentitasIns32 IDRegDate2Officer3, FC.ExpiredDateIdentitasIns32 IDExpireDate2Officer3,
                                FC.IdentitasIns33 IDType3Officer3, FC.NoIdentitasIns33 IDNumber3Officer3, FC.RegistrationDateIdentitasIns33 IDRegDate3Officer3, FC.ExpiredDateIdentitasIns33 IDExpireDate3Officer3,
                                FC.IdentitasIns34 IDType4Officer3, FC.NoIdentitasIns34 IDNumber4Officer3, FC.RegistrationDateIdentitasIns34 IDRegDate4Officer3, FC.ExpiredDateIdentitasIns34 IDExpireDate4Officer3,
                                FC.NamaDepanIns4 FirstNameOfficer4,FC.NamaTengahIns4 MiddleNameOfficer4,FC.NamaBelakangIns4 LastNameOfficer4,FC.Jabatan4 PositionOfficer4,
                                FC.IdentitasIns41 IDType1Officer4, FC.NoIdentitasIns41 IDNumber1Officer4, FC.RegistrationDateIdentitasIns41 IDRegDate1Officer4, FC.ExpiredDateIdentitasIns41 IDExpireDate1Officer4,
                                FC.IdentitasIns42 IDType2Officer4, FC.NoIdentitasIns42 IDNumber2Officer4, FC.RegistrationDateIdentitasIns42 IDRegDate2Officer4, FC.ExpiredDateIdentitasIns42 IDExpireDate2Officer4,
                                FC.IdentitasIns43 IDType3Officer4, FC.NoIdentitasIns43 IDNumber3Officer4, FC.RegistrationDateIdentitasIns43 IDRegDate3Officer4, FC.ExpiredDateIdentitasIns43 IDExpireDate3Officer4,
                                FC.IdentitasIns44 IDType4Officer4, FC.NoIdentitasIns44 IDNumber4Officer4, FC.RegistrationDateIdentitasIns44 IDRegDate4Officer4, FC.ExpiredDateIdentitasIns44 IDExpireDate4Officer4,
                                FC.Description, FC.EntryUsersID, FC.EntryTime, FC.UpdateUsersID, FC.UpdateTime, FC.ApprovedUsersID, FC.ApprovedTime, FC.VoidUsersID, FC.VoidTime,
                                FC.SuspendBy, FC.SuspendTime, FC.UnSuspendBy, FC.UnSuspendTime
                                from FundClient fc   
                                left join Agent A on fc.SellingAgentPK = A.AgentPK and A.status = 2  
                                left join MasterValue mv1 on fc.KodeKotaInd1 = mv1.Code and mv1.ID = 'CityRHB' and mv1.status = 2  
                                left join MasterValue mv2 on fc.KodeKotaInd2 = mv2.Code and  mv2.ID = 'CityRHB' and mv2.status = 2  
                                left join MasterValue mv3 on fc.KodeKotaIns = mv3.Code and  mv3.ID = 'CityRHB' and mv3.status = 2  
                                left join MasterValue mv4 on fc.OtherKodeKotaInd1 = mv4.Code and mv4.ID = 'CityRHB' and mv4.status = 2  
                                left join MasterValue mv5 on fc.OtherKodeKotaInd2 = mv5.Code and mv5.ID = 'CityRHB' and mv5.status = 2  
                                left join MasterValue mv6 on fc.OtherKodeKotaInd3 = mv6.Code and mv6.ID = 'CityRHB' and mv6.status = 2  
                                left join MasterValue mv7 on fc.ClientCategory = mv7.code  and  mv7.ID = 'ClientCategory' and mv7.status = 2  
                                left join MasterValue mv8 on fc.InvestorType = mv8.code and  mv8.ID = 'InvestorType' and mv8.status = 2  
                                left join MasterValue mv9 on fc.JenisKelamin = mv9.code and  mv9.ID = 'Sex' and mv9.status = 2  
                                left join MasterValue mv10 on fc.StatusPerkawinan = mv10.code and  mv10.ID = 'MaritalStatus' and mv10.status = 2  
                                left join MasterValue mv11 on fc.Pekerjaan = mv11.code and  mv11.ID = 'Occupation' and mv11.status = 2  
                                left join MasterValue mv12 on fc.Pendidikan = mv12.code and  mv12.ID = 'EducationalBackground' and mv12.status = 2  
                                left join MasterValue mv13 on fc.Agama = mv13.code and  mv13.ID = 'Religion' and mv13.status = 2  
                                left join MasterValue mv14 on fc.PenghasilanInd = mv14.code and  mv14.ID = 'IncomeIND' and mv14.status = 2  
                                left join MasterValue mv15 on fc.SumberDanaInd = mv15.code and  mv15.ID = 'IncomeSourceIND' and mv15.status = 2  
                                left join MasterValue mv16 on fc.MaksudTujuanInd = mv16.code and  mv16.ID = 'InvestmentObjectivesIND' and mv16.status = 2  
                                left join MasterValue mv17 on fc.Domisili = mv17.code and  mv17.ID = 'Domicile' and mv17.status = 2  
                                left join MasterValue mv18 on fc.Tipe = mv18.code and  mv18.ID = 'CompanyType' and mv18.status = 2  
                                left join MasterValue mv19 on fc.Karakteristik = mv19.code and  mv19.ID = 'CompanyCharacteristic' and mv19.status = 2  
                                left join MasterValue mv20 on fc.PenghasilanInstitusi = mv20.code and  mv20.ID = 'IncomeINS' and mv20.status = 2  
                                left join MasterValue mv21 on fc.SumberDanaInstitusi = mv21.code and  mv21.ID = 'IncomeSourceIND' and mv21.status = 2  
                                left join MasterValue mv22 on fc.MaksudTujuanInstitusi = mv22.code and  mv22.ID = 'InvestmentObjectivesINS' and mv22.status = 2  
                                left join MasterValue mv24 on fc.Negara = mv24.code and  mv24.ID = 'SDICountry' and mv24.status = 2  
                                left join MasterValue mv25 on fc.Nationality = mv25.code and  mv25.ID = 'Nationality' and mv25.status = 2  
                                left join MasterValue mv26 on fc.Propinsi = mv26.code and  mv26.ID = 'SDIProvince' and mv26.status = 2  
                                left join MasterValue mv27 on fc.OtherPropinsiInd1 = mv27.code and  mv27.ID = 'SDIProvince' and mv27.status = 2  
                                left join MasterValue mv28 on fc.OtherPropinsiInd2 = mv28.code and  mv28.ID = 'SDIProvince' and mv28.status = 2  
                                left join MasterValue mv29 on fc.OtherPropinsiInd3 = mv29.code and  mv29.ID = 'SDIProvince' and mv29.status = 2  
                                left join MasterValue mv30 on fc.OtherNegaraInd1 = mv30.code and  mv30.ID = 'SDICountry' and mv30.status = 2  
                                left join MasterValue mv31 on fc.OtherNegaraInd2 = mv31.code and  mv31.ID = 'SDICountry' and mv31.status = 2  
                                left join MasterValue mv32 on fc.OtherNegaraInd3 = mv32.code and  mv32.ID = 'SDICountry' and mv32.status = 2  
                                left join InternalCategory IC on fc.InternalCategoryPK = IC.InternalCategoryPK and IC.status = 2  
                                left join MasterValue mv33 on fc.InvestorsRiskProfile = mv33.code and  mv33.ID = 'InvestorsRiskProfile' and mv33.status = 2  
                                left join MasterValue mv34 on fc.AssetOwner = mv34.code and  mv34.ID = 'AssetOwner' and mv34.status = 2  
                                left join MasterValue mv35 on fc.StatementType = mv35.code and  mv35.ID = 'StatementType' and mv35.status = 2  
                                left join MasterValue mv36 on fc.fatca = mv36.code and  mv36.ID = 'FATCAInsti' and mv36.status = 2  
                                left join MasterValue mv37 on fc.TINIssuanceCountry = mv37.code and  mv37.ID = 'SDICountry' and mv37.status = 2  
                                left join MasterValue mv41 on fc.CountryOfBirth = mv41.code and  mv41.ID = 'SDICountry' and mv41.status = 2  
                                left join MasterValue mv42 on fc.CountryofCorrespondence = mv42.code and  mv42.ID = 'SDICountry' and mv42.status = 2  
                                left join MasterValue mv43 on fc.Negara = mv43.code and  mv43.ID = 'SDICountry' and mv43.status = 2  
                                left join MasterValue mv44 on fc.CountryofEstablishment = mv44.code and  mv44.ID = 'SDICountry' and mv44.status = 2  
                                left join MasterValue mv45 on fc.CountryofCompany = mv45.code and  mv45.ID = 'SDICountry' and mv45.status = 2  
                                left join MasterValue mv46 on fc.CompanyCityName = mv46.Code and mv46.ID = 'CityRHB' and mv46.status = 2  
                                left join MasterValue mv47 on fc.NatureOfBusiness = mv47.Code and mv47.ID = 'HRBusiness' and mv47.status = 2  
                                left join MasterValue mv48 on fc.Politis = mv48.Code and mv48.ID = 'PoliticallyExposed' and mv48.status = 2
                                left join MasterValue mv49 on fc.IdentitasInd1 = mv49.Code and mv49.ID = 'Identity' and mv49.status = 2
                                left join MasterValue mv50 on fc.IdentitasInd2 = mv50.Code and mv50.ID = 'Identity' and mv50.status = 2
                                left join MasterValue mv51 on fc.IdentitasInd3 = mv51.Code and mv51.ID = 'Identity' and mv51.status = 2
                                left join MasterValue mv52 on fc.IdentitasIns11 = mv52.Code and mv52.ID = 'Identity' and mv52.status = 2
                                left join MasterValue mv53 on fc.IdentitasIns12 = mv53.Code and mv53.ID = 'Identity' and mv53.status = 2
                                left join MasterValue mv54 on fc.IdentitasIns13 = mv54.Code and mv54.ID = 'Identity' and mv54.status = 2
                                left join MasterValue mv55 on fc.IdentitasIns14 = mv55.Code and mv55.ID = 'Identity' and mv55.status = 2
                                left join MasterValue mv56 on fc.IdentitasIns21 = mv56.Code and mv56.ID = 'Identity' and mv56.status = 2
                                left join MasterValue mv57 on fc.IdentitasIns22 = mv57.Code and mv57.ID = 'Identity' and mv57.status = 2
                                left join MasterValue mv58 on fc.IdentitasIns23 = mv58.Code and mv58.ID = 'Identity' and mv58.status = 2
                                left join MasterValue mv59 on fc.IdentitasIns24 = mv59.Code and mv59.ID = 'Identity' and mv59.status = 2
                                left join MasterValue mv60 on fc.IdentitasIns31 = mv60.Code and mv60.ID = 'Identity' and mv60.status = 2
                                left join MasterValue mv61 on fc.IdentitasIns32 = mv61.Code and mv61.ID = 'Identity' and mv61.status = 2
                                left join MasterValue mv62 on fc.IdentitasIns33 = mv62.Code and mv62.ID = 'Identity' and mv62.status = 2
                                left join MasterValue mv63 on fc.IdentitasIns34 = mv63.Code and mv63.ID = 'Identity' and mv63.status = 2
                                left join MasterValue mv64 on fc.IdentitasIns41 = mv64.Code and mv64.ID = 'Identity' and mv64.status = 2
                                left join MasterValue mv65 on fc.IdentitasIns42 = mv65.Code and mv65.ID = 'Identity' and mv65.status = 2
                                left join MasterValue mv66 on fc.IdentitasIns43 = mv66.Code and mv66.ID = 'Identity' and mv66.status = 2
                                left join MasterValue mv67 on fc.IdentitasIns44 = mv67.Code and mv67.ID = 'Identity' and mv67.status = 2
                                left join MasterValue mv68 on fc.SpouseOccupation = mv68.code and  mv68.ID = 'Occupation' and mv68.status = 2
                                left join MasterValue mv69 on fc.KYCRiskProfile = mv69.code and  mv69.ID = 'KYCRiskProfile' and mv69.status = 2
                                left join fundclient Z on FC.AfiliatedFromPK = Z.FundclientPK and  Z.status = 2
                                left join Currency Y on FC.MataUang1 =  Y.CurrencyPK and Y.status = 2
                                left join Currency W on FC.MataUang2 =  W.CurrencyPK and W.status = 2
                                left join Currency V on FC.MataUang3 =  V.CurrencyPK and V.status = 2
                                left join Bank BC1 on fc.NamaBank1 = BC1.BankPK and BC1.status = 2   
                                left join Bank BC2 on fc.NamaBank2 = BC2.BankPK and BC2.status = 2   
                                left join Bank BC3 on fc.NamaBank3 = BC3.BankPK and BC3.status = 2   
                                left join Bank BC4 on fc.BankRDNPK = BC4.BankPK and BC4.status = 2
                            where  FC.status in (1,2) and FC.InvestorType = @InvestorType " + _paramDateFrom + @" ";
                            cmd.CommandTimeout = 0;
                            cmd.Parameters.AddWithValue("@InvestorType", _fundClientRpt.InvestorType);
                            using (SqlDataReader dr0 = cmd.ExecuteReader())
                            {
                                if (!dr0.HasRows)
                                {
                                    return false;
                                }
                                else
                                {
                                    string filePath = Tools.ReportsPath + "FundClientByInstitution" + "_" + _userID + ".xlsx";
                                    FileInfo excelFile = new FileInfo(filePath);
                                    if (excelFile.Exists)
                                    {
                                        excelFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                        excelFile = new FileInfo(filePath);
                                    }

                                    // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                    using (ExcelPackage package = new ExcelPackage(excelFile))
                                    {
                                        package.Workbook.Properties.Title = "FundClientByInstitution";
                                        package.Workbook.Properties.Author = Tools.DefaultReportAuthor();
                                        package.Workbook.Properties.Comments = Tools.DefaultReportComments();
                                        package.Workbook.Properties.Company = Tools.DefaultReportCompany();
                                        package.Workbook.Properties.SetCustomPropertyValue("Checked by", _userID);
                                        package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", Tools.DefaultReportAssemblyName());

                                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("FundClientByInstitution");


                                        //ATUR DATA GROUPINGNYA DULU
                                        List<FundClientRptByInvestorType> rList = new List<FundClientRptByInvestorType>();
                                        while (dr0.Read())
                                        {
                                            FundClientRptByInvestorType rSingle = new FundClientRptByInvestorType();

                                            rSingle.FundClientID = dr0["FundClientID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["FundClientID"]);
                                            rSingle.InvestorType = dr0["InvestorType"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["InvestorType"]);
                                            rSingle.InternalCategory = dr0["InternalCategory"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["InternalCategory"]);
                                            rSingle.InternalName = dr0["InternalName"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["InternalName"]);
                                            rSingle.SellingAgent = dr0["SellingAgent"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["SellingAgent"]);
                                            rSingle.SID = dr0["SID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["SID"]);
                                            rSingle.IFUACode = dr0["IFUACode"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IFUACode"]);
                                            rSingle.InvestorsRiskProfile = dr0["InvestorsRiskProfile"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["InvestorsRiskProfile"]);
                                            rSingle.DormantDate = dr0["DormantDate"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["DormantDate"]);
                                            rSingle.Affiliated = dr0["Affiliated"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["Affiliated"]);
                                            rSingle.AffiliatedWith = dr0["AffiliatedWith"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["AffiliatedWith"]);
                                            rSingle.Suspended = dr0["Suspended"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["Suspended"]);
                                            rSingle.NPWP = dr0["NPWP"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["NPWP"]);
                                            rSingle.RegistrationNPWP = dr0["RegistrationNPWP"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["RegistrationNPWP"]);
                                            rSingle.Email = dr0["Email"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["Email"]);
                                            rSingle.PhoneNumber = dr0["PhoneNumber"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["PhoneNumber"]);
                                            rSingle.MobilePhone = dr0["MobilePhone"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["MobilePhone"]);
                                            rSingle.BusinessPhone = dr0["BusinessPhone"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["BusinessPhone"]);
                                            rSingle.Fax = dr0["Fax"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["Fax"]);
                                            rSingle.Country = dr0["Country"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["Country"]);
                                            rSingle.Nationality = dr0["Nationality"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["Nationality"]);
                                            rSingle.BankRDN = dr0["BankRDN"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["BankRDN"]);
                                            rSingle.RDNAccountName = dr0["RDNAccountName"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["RDNAccountName"]);
                                            rSingle.RDNAccountNumber = dr0["RDNAccountNumber"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["RDNAccountNumber"]);
                                            rSingle.BankName1 = dr0["BankName1"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["BankName1"]);
                                            rSingle.BankAccountName1 = dr0["BankAccountName1"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["BankAccountName1"]);
                                            rSingle.BankAccountNumber1 = dr0["BankAccountNumber1"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["BankAccountNumber1"]);
                                            rSingle.BankBranchName1 = dr0["BankBranchName1"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["BankBranchName1"]);
                                            rSingle.Currency1 = dr0["Currency1"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["Currency1"]);
                                            rSingle.BankName2 = dr0["BankName2"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["BankName2"]);
                                            rSingle.BankAccountName2 = dr0["BankAccountName2"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["BankAccountName2"]);
                                            rSingle.BankAccountNumber2 = dr0["BankAccountNumber2"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["BankAccountNumber2"]);
                                            rSingle.BankBranchName2 = dr0["BankBranchName2"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["BankBranchName2"]);
                                            rSingle.Currency2 = dr0["Currency2"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["Currency2"]);
                                            rSingle.BankName3 = dr0["BankName3"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["BankName3"]);
                                            rSingle.BankAccountName3 = dr0["BankAccountName3"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["BankAccountName3"]);
                                            rSingle.BankAccountNumber3 = dr0["BankAccountNumber3"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["BankAccountNumber3"]);
                                            rSingle.BankBranchName3 = dr0["BankBranchName3"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["BankBranchName3"]);
                                            rSingle.Currency3 = dr0["Currency3"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["Currency3"]);
                                            rSingle.AssetOwner = dr0["AssetOwner"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["AssetOwner"]);
                                            rSingle.StatementType = dr0["StatementType"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["StatementType"]);
                                            rSingle.FATCAStatus = dr0["FATCAStatus"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["FATCAStatus"]);
                                            rSingle.TIN = dr0["TIN"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["TIN"]);
                                            rSingle.TINIssuanceCountry = dr0["TINIssuanceCountry"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["TINIssuanceCountry"]);
                                            rSingle.GIIN = dr0["GIIN"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["GIIN"]);
                                            rSingle.SubstantialOwnerName = dr0["SubstantialOwnerName"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["SubstantialOwnerName"]);
                                            rSingle.SubstantialOwnerAddress = dr0["SubstantialOwnerAddress"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["SubstantialOwnerAddress"]);
                                            rSingle.SubstantialOwnerTIN = dr0["SubstantialOwnerTIN"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["SubstantialOwnerTIN"]);
                                            rSingle.CompanyName = dr0["CompanyName"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["CompanyName"]);
                                            rSingle.CompanyAddress = dr0["CompanyAddress"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["CompanyAddress"]);
                                            rSingle.CompanyZipCode = dr0["CompanyZipCode"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["CompanyZipCode"]);
                                            rSingle.CompanyCity = dr0["CompanyCity"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["CompanyCity"]);
                                            rSingle.CompanyLegalDomicile = dr0["CompanyLegalDomicile"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["CompanyLegalDomicile"]);
                                            rSingle.EstablishmentDate = dr0["EstablishmentDate"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["EstablishmentDate"]);
                                            rSingle.EstablishmentPlace = dr0["EstablishmentPlace"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["EstablishmentPlace"]);
                                            rSingle.CountryofEstablishment = dr0["CountryofEstablishment"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["CountryofEstablishment"]);
                                            rSingle.SKDNumber = dr0["SKDNumber"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["SKDNumber"]);
                                            rSingle.ExpiredDateSKD = dr0["ExpiredDateSKD"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["ExpiredDateSKD"]);
                                            rSingle.ArticleOfAssociation = dr0["ArticleOfAssociation"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["ArticleOfAssociation"]);
                                            rSingle.SIUPNumber = dr0["SIUPNumber"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["SIUPNumber"]);
                                            rSingle.SIUPExpirationDate = dr0["SIUPExpirationDate"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["SIUPExpirationDate"]);
                                            rSingle.AssetFor1Year = dr0["AssetFor1Year"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["AssetFor1Year"]);
                                            rSingle.AssetFor2Year = dr0["AssetFor2Year"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["AssetFor2Year"]);
                                            rSingle.AssetFor3Year = dr0["AssetFor3Year"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["AssetFor3Year"]);
                                            rSingle.OperatingProfitFor1Year = dr0["OperatingProfitFor1Year"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["OperatingProfitFor1Year"]);
                                            rSingle.OperatingProfitFor2Year = dr0["OperatingProfitFor2Year"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["OperatingProfitFor2Year"]);
                                            rSingle.OperatingProfitFor3Year = dr0["OperatingProfitFor3Year"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["OperatingProfitFor3Year"]);
                                            rSingle.CompanyType = dr0["CompanyType"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["CompanyType"]);
                                            rSingle.CompanyCharacteristic = dr0["CompanyCharacteristic"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["CompanyCharacteristic"]);
                                            rSingle.CompanyIncomePerAnnum = dr0["CompanyIncomePerAnnum"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["CompanyIncomePerAnnum"]);
                                            rSingle.CompanySourceOfFunds = dr0["CompanySourceOfFunds"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["CompanySourceOfFunds"]);
                                            rSingle.CompanyInvestmentObjective = dr0["CompanyInvestmentObjective"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["CompanyInvestmentObjective"]);
                                            rSingle.CountryofCompany = dr0["CountryofCompany"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["CountryofCompany"]);
                                            rSingle.CompanyCityName = dr0["CompanyCityName"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["CompanyCityName"]);
                                            rSingle.Province = dr0["Province"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["Province"]);
                                            rSingle.FirstNameOfficer1 = dr0["FirstNameOfficer1"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["FirstNameOfficer1"]);
                                            rSingle.MiddleNameOfficer1 = dr0["MiddleNameOfficer1"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["MiddleNameOfficer1"]);
                                            rSingle.LastNameOfficer1 = dr0["LastNameOfficer1"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["LastNameOfficer1"]);
                                            rSingle.PositionOfficer1 = dr0["PositionOfficer1"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["PositionOfficer1"]);
                                            rSingle.PhoneNumberOfficer1 = dr0["PhoneNumberOfficer1"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["PhoneNumberOfficer1"]);
                                            rSingle.EmailOfficer1 = dr0["EmailOfficer1"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["EmailOfficer1"]);
                                            rSingle.IDType1Officer1 = dr0["IDType1Officer1"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IDType1Officer1"]);
                                            rSingle.IDNumber1Officer1 = dr0["IDNumber1Officer1"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IDNumber1Officer1"]);
                                            rSingle.IDRegDate1Officer1 = dr0["IDRegDate1Officer1"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IDRegDate1Officer1"]);
                                            rSingle.IDExpireDate1Officer1 = dr0["IDExpireDate1Officer1"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IDExpireDate1Officer1"]);
                                            rSingle.IDType2Officer1 = dr0["IDType2Officer1"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IDType2Officer1"]);
                                            rSingle.IDNumber2Officer1 = dr0["IDNumber2Officer1"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IDNumber2Officer1"]);
                                            rSingle.IDRegDate2Officer1 = dr0["IDRegDate2Officer1"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IDRegDate2Officer1"]);
                                            rSingle.IDExpireDate2Officer1 = dr0["IDExpireDate2Officer1"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IDExpireDate2Officer1"]);
                                            rSingle.IDType3Officer1 = dr0["IDType3Officer1"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IDType3Officer1"]);
                                            rSingle.IDNumber3Officer1 = dr0["IDNumber3Officer1"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IDNumber3Officer1"]);
                                            rSingle.IDRegDate3Officer1 = dr0["IDRegDate3Officer1"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IDRegDate3Officer1"]);
                                            rSingle.IDExpireDate3Officer1 = dr0["IDExpireDate3Officer1"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IDExpireDate3Officer1"]);
                                            rSingle.IDType4Officer1 = dr0["IDType4Officer1"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IDType4Officer1"]);
                                            rSingle.IDNumber4Officer1 = dr0["IDNumber4Officer1"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IDNumber4Officer1"]);
                                            rSingle.IDRegDate4Officer1 = dr0["IDRegDate4Officer1"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IDRegDate4Officer1"]);
                                            rSingle.IDExpireDate4Officer1 = dr0["IDExpireDate4Officer1"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IDExpireDate4Officer1"]);
                                            rSingle.FirstNameOfficer2 = dr0["FirstNameOfficer2"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["FirstNameOfficer2"]);
                                            rSingle.MiddleNameOfficer2 = dr0["MiddleNameOfficer2"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["MiddleNameOfficer2"]);
                                            rSingle.LastNameOfficer2 = dr0["LastNameOfficer2"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["LastNameOfficer2"]);
                                            rSingle.PositionOfficer2 = dr0["PositionOfficer2"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["PositionOfficer2"]);
                                            rSingle.IDType1Officer2 = dr0["IDType1Officer2"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IDType1Officer2"]);
                                            rSingle.IDNumber1Officer2 = dr0["IDNumber1Officer2"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IDNumber1Officer2"]);
                                            rSingle.IDRegDate1Officer2 = dr0["IDRegDate1Officer2"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IDRegDate1Officer2"]);
                                            rSingle.IDExpireDate1Officer2 = dr0["IDExpireDate1Officer2"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IDExpireDate1Officer2"]);
                                            rSingle.IDType2Officer2 = dr0["IDType2Officer2"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IDType2Officer2"]);
                                            rSingle.IDNumber2Officer2 = dr0["IDNumber2Officer2"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IDNumber2Officer2"]);
                                            rSingle.IDRegDate2Officer2 = dr0["IDRegDate2Officer2"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IDRegDate2Officer2"]);
                                            rSingle.IDExpireDate2Officer2 = dr0["IDExpireDate2Officer2"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IDExpireDate2Officer2"]);
                                            rSingle.IDType3Officer2 = dr0["IDType3Officer2"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IDType3Officer2"]);
                                            rSingle.IDNumber3Officer2 = dr0["IDNumber3Officer2"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IDNumber3Officer2"]);
                                            rSingle.IDRegDate3Officer2 = dr0["IDRegDate3Officer2"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IDRegDate3Officer2"]);
                                            rSingle.IDExpireDate3Officer2 = dr0["IDExpireDate3Officer2"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IDExpireDate3Officer2"]);
                                            rSingle.IDType4Officer2 = dr0["IDType4Officer2"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IDType4Officer2"]);
                                            rSingle.IDNumber4Officer2 = dr0["IDNumber4Officer2"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IDNumber4Officer2"]);
                                            rSingle.IDRegDate4Officer2 = dr0["IDRegDate4Officer2"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IDRegDate4Officer2"]);
                                            rSingle.IDExpireDate4Officer2 = dr0["IDExpireDate4Officer2"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IDExpireDate4Officer2"]);
                                            rSingle.FirstNameOfficer3 = dr0["FirstNameOfficer3"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["FirstNameOfficer3"]);
                                            rSingle.MiddleNameOfficer3 = dr0["MiddleNameOfficer3"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["MiddleNameOfficer3"]);
                                            rSingle.LastNameOfficer3 = dr0["LastNameOfficer3"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["LastNameOfficer3"]);
                                            rSingle.PositionOfficer3 = dr0["PositionOfficer3"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["PositionOfficer3"]);
                                            rSingle.IDType1Officer3 = dr0["IDType1Officer3"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IDType1Officer3"]);
                                            rSingle.IDNumber1Officer3 = dr0["IDNumber1Officer3"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IDNumber1Officer3"]);
                                            rSingle.IDRegDate1Officer3 = dr0["IDRegDate1Officer3"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IDRegDate1Officer3"]);
                                            rSingle.IDExpireDate1Officer3 = dr0["IDExpireDate1Officer3"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IDExpireDate1Officer3"]);
                                            rSingle.IDType2Officer3 = dr0["IDType2Officer3"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IDType2Officer3"]);
                                            rSingle.IDNumber2Officer3 = dr0["IDNumber2Officer3"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IDNumber2Officer3"]);
                                            rSingle.IDRegDate2Officer3 = dr0["IDRegDate2Officer3"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IDRegDate2Officer3"]);
                                            rSingle.IDExpireDate2Officer3 = dr0["IDExpireDate2Officer3"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IDExpireDate2Officer3"]);
                                            rSingle.IDType3Officer3 = dr0["IDType3Officer3"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IDType3Officer3"]);
                                            rSingle.IDNumber3Officer3 = dr0["IDNumber3Officer3"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IDNumber3Officer3"]);
                                            rSingle.IDRegDate3Officer3 = dr0["IDRegDate3Officer3"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IDRegDate3Officer3"]);
                                            rSingle.IDExpireDate3Officer3 = dr0["IDExpireDate3Officer3"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IDExpireDate3Officer3"]);
                                            rSingle.IDType4Officer3 = dr0["IDType4Officer3"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IDType4Officer3"]);
                                            rSingle.IDNumber4Officer3 = dr0["IDNumber4Officer3"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IDNumber4Officer3"]);
                                            rSingle.IDRegDate4Officer3 = dr0["IDRegDate4Officer3"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IDRegDate4Officer3"]);
                                            rSingle.IDExpireDate4Officer3 = dr0["IDExpireDate4Officer3"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IDExpireDate4Officer3"]);
                                            rSingle.FirstNameOfficer4 = dr0["FirstNameOfficer4"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["FirstNameOfficer4"]);
                                            rSingle.MiddleNameOfficer4 = dr0["MiddleNameOfficer4"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["MiddleNameOfficer4"]);
                                            rSingle.LastNameOfficer4 = dr0["LastNameOfficer4"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["LastNameOfficer4"]);
                                            rSingle.PositionOfficer4 = dr0["PositionOfficer4"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["PositionOfficer4"]);
                                            rSingle.IDType1Officer4 = dr0["IDType1Officer4"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IDType1Officer4"]);
                                            rSingle.IDNumber1Officer4 = dr0["IDNumber1Officer4"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IDNumber1Officer4"]);
                                            rSingle.IDRegDate1Officer4 = dr0["IDRegDate1Officer4"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IDRegDate1Officer4"]);
                                            rSingle.IDExpireDate1Officer4 = dr0["IDExpireDate1Officer4"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IDExpireDate1Officer4"]);
                                            rSingle.IDType2Officer4 = dr0["IDType2Officer4"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IDType2Officer4"]);
                                            rSingle.IDNumber2Officer4 = dr0["IDNumber2Officer4"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IDNumber2Officer4"]);
                                            rSingle.IDRegDate2Officer4 = dr0["IDRegDate2Officer4"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IDRegDate2Officer4"]);
                                            rSingle.IDExpireDate2Officer4 = dr0["IDExpireDate2Officer4"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IDExpireDate2Officer4"]);
                                            rSingle.IDType3Officer4 = dr0["IDType3Officer4"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IDType3Officer4"]);
                                            rSingle.IDNumber3Officer4 = dr0["IDNumber3Officer4"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IDNumber3Officer4"]);
                                            rSingle.IDRegDate3Officer4 = dr0["IDRegDate3Officer4"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IDRegDate3Officer4"]);
                                            rSingle.IDExpireDate3Officer4 = dr0["IDExpireDate3Officer4"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IDExpireDate3Officer4"]);
                                            rSingle.IDType4Officer4 = dr0["IDType4Officer4"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IDType4Officer4"]);
                                            rSingle.IDNumber4Officer4 = dr0["IDNumber4Officer4"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IDNumber4Officer4"]);
                                            rSingle.IDRegDate4Officer4 = dr0["IDRegDate4Officer4"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IDRegDate4Officer4"]);
                                            rSingle.IDExpireDate4Officer4 = dr0["IDExpireDate4Officer4"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IDExpireDate4Officer4"]);
                                            rSingle.Description = dr0["Description"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["Description"]);
                                            rSingle.CityOfEstablishment = dr0["CityOfEstablishment"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["CityOfEstablishment"]);
                                            rSingle.SellingAgentCode = dr0["SACode"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["SACode"]);
                                            rSingle.CountryofDomicile = dr0["CountryofDomicile"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["CountryofDomicile"]);
                                            rSingle.KYCRiskProfile = dr0["KYCRiskProfile"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["KYCRiskProfile"]);
                                            rSingle.BICCode1 = dr0["BICode1"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["BICode1"]);
                                            rSingle.BICCode2 = dr0["BICode2"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["BICode2"]);
                                            rSingle.BICCode3 = dr0["BICode3"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["BICode3"]);
                                            rSingle.BIMemberCode1 = dr0["BIMemberCode1"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["BIMemberCode1"]);
                                            rSingle.BIMemberCode2 = dr0["BIMemberCode2"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["BIMemberCode2"]);
                                            rSingle.BIMemberCode3 = dr0["BIMemberCode3"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["BIMemberCode3"]);
                                            rSingle.EntryUsersID = dr0["EntryUsersID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["EntryUsersID"]);
                                            rSingle.EntryTime = dr0["EntryTime"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["EntryTime"]);
                                            rSingle.UpdateUsersID = dr0["UpdateUsersID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["UpdateUsersID"]);
                                            rSingle.UpdateTime = dr0["UpdateTime"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["UpdateTime"]);
                                            rSingle.ApprovedUsersID = dr0["ApprovedUsersID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["ApprovedUsersID"]);
                                            rSingle.ApprovedTime = dr0["ApprovedTime"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["ApprovedTime"]);
                                            rSingle.VoidUsersID = dr0["VoidUsersID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["VoidUsersID"]);
                                            rSingle.VoidTime = dr0["VoidTime"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["VoidTime"]);
                                            rSingle.SuspendBy = dr0["SuspendBy"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["SuspendBy"]);
                                            rSingle.SuspendTime = dr0["SuspendTime"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["SuspendTime"]);
                                            rSingle.UnSuspendBy = dr0["UnSuspendBy"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["UnSuspendBy"]);
                                            rSingle.UnSuspendTime = dr0["UnSuspendTime"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["UnSuspendTime"]);
                                            rList.Add(rSingle);

                                        }

                                        var GroupByTitle =
                                            from r in rList
                                            group r by new { r.InvestorType } into rGroup
                                            select rGroup;

                                        int incRowExcel = 0;

                                        foreach (var rsHeader in GroupByTitle)
                                        {

                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Value = "Investor Type :";
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 2].Value = rsHeader.Key.InvestorType;
                                            worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                            incRowExcel++;
                                            worksheet.Cells[incRowExcel, 1].Value = "FundClientID";
                                            worksheet.Cells[incRowExcel, 2].Value = "InvestorType";
                                            worksheet.Cells[incRowExcel, 3].Value = "InternalCategory";
                                            worksheet.Cells[incRowExcel, 4].Value = "InternalName";
                                            worksheet.Cells[incRowExcel, 5].Value = "SellingAgent";
                                            worksheet.Cells[incRowExcel, 6].Value = "NPWP";
                                            worksheet.Cells[incRowExcel, 7].Value = "RegistrationNPWP";
                                            worksheet.Cells[incRowExcel, 8].Value = "Email";
                                            worksheet.Cells[incRowExcel, 9].Value = "PhoneNumber";
                                            worksheet.Cells[incRowExcel, 10].Value = "MobilePhone";
                                            worksheet.Cells[incRowExcel, 11].Value = "Fax";
                                            worksheet.Cells[incRowExcel, 12].Value = "SID";
                                            worksheet.Cells[incRowExcel, 13].Value = "IFUACode";
                                            worksheet.Cells[incRowExcel, 14].Value = "InvestorsRiskProfile";
                                            worksheet.Cells[incRowExcel, 15].Value = "KYCRiskProfile";
                                            worksheet.Cells[incRowExcel, 16].Value = "AssetOwner";
                                            worksheet.Cells[incRowExcel, 17].Value = "StatementType";
                                            worksheet.Cells[incRowExcel, 18].Value = "SellingAgentCode";
                                            worksheet.Cells[incRowExcel, 19].Value = "DormantDate";
                                            worksheet.Cells[incRowExcel, 20].Value = "Description";
                                            worksheet.Cells[incRowExcel, 21].Value = "Affiliated";
                                            worksheet.Cells[incRowExcel, 22].Value = "AffiliatedWith";
                                            worksheet.Cells[incRowExcel, 23].Value = "Suspended";
                                            worksheet.Cells[incRowExcel, 24].Value = "BankName1";
                                            worksheet.Cells[incRowExcel, 25].Value = "BankAccountName1";
                                            worksheet.Cells[incRowExcel, 26].Value = "BankAccountNumber1";
                                            worksheet.Cells[incRowExcel, 27].Value = "BankBranchName1";
                                            worksheet.Cells[incRowExcel, 28].Value = "Currency1";
                                            worksheet.Cells[incRowExcel, 29].Value = "BICCode1";
                                            worksheet.Cells[incRowExcel, 30].Value = "BIMemberCode1";
                                            worksheet.Cells[incRowExcel, 31].Value = "BankName2";
                                            worksheet.Cells[incRowExcel, 32].Value = "BankAccountName2";
                                            worksheet.Cells[incRowExcel, 33].Value = "BankAccountNumber2";
                                            worksheet.Cells[incRowExcel, 34].Value = "BankBranchName2";
                                            worksheet.Cells[incRowExcel, 35].Value = "Currency2";
                                            worksheet.Cells[incRowExcel, 36].Value = "BICCode2";
                                            worksheet.Cells[incRowExcel, 37].Value = "BIMemberCode2";
                                            worksheet.Cells[incRowExcel, 38].Value = "BankName3";
                                            worksheet.Cells[incRowExcel, 39].Value = "BankAccountName3";
                                            worksheet.Cells[incRowExcel, 40].Value = "BankAccountNumber3";
                                            worksheet.Cells[incRowExcel, 41].Value = "BankBranchName3";
                                            worksheet.Cells[incRowExcel, 42].Value = "Currency3";
                                            worksheet.Cells[incRowExcel, 43].Value = "BICCode3";
                                            worksheet.Cells[incRowExcel, 44].Value = "BIMemberCode3";
                                            worksheet.Cells[incRowExcel, 45].Value = "BankRDN";
                                            worksheet.Cells[incRowExcel, 46].Value = "RDNAccountName";
                                            worksheet.Cells[incRowExcel, 47].Value = "RDNAccountNumber";
                                            worksheet.Cells[incRowExcel, 48].Value = "CompanyName";
                                            worksheet.Cells[incRowExcel, 49].Value = "CompanyAddress";
                                            worksheet.Cells[incRowExcel, 50].Value = "CompanyZipCode";
                                            worksheet.Cells[incRowExcel, 51].Value = "CompanyCity";
                                            worksheet.Cells[incRowExcel, 52].Value = "CompanyLegalDomicile";
                                            worksheet.Cells[incRowExcel, 53].Value = "CountryOfDomicile";
                                            worksheet.Cells[incRowExcel, 54].Value = "CompanyType";
                                            worksheet.Cells[incRowExcel, 55].Value = "CompanyCharacteristic";
                                            worksheet.Cells[incRowExcel, 56].Value = "CompanyIncomePerAnnum";
                                            worksheet.Cells[incRowExcel, 57].Value = "CompanySourceOfFunds";
                                            worksheet.Cells[incRowExcel, 58].Value = "CompanyInvestmentObjective";
                                            worksheet.Cells[incRowExcel, 59].Value = "SKDNumber";
                                            worksheet.Cells[incRowExcel, 60].Value = "ExpiredDateSKD";
                                            worksheet.Cells[incRowExcel, 61].Value = "ArticleOfAssociation";
                                            worksheet.Cells[incRowExcel, 62].Value = "SIUPNumber";
                                            worksheet.Cells[incRowExcel, 63].Value = "SIUPExpirationDate";
                                            worksheet.Cells[incRowExcel, 64].Value = "FirstNameOfficer1";
                                            worksheet.Cells[incRowExcel, 65].Value = "MiddleNameOfficer1";
                                            worksheet.Cells[incRowExcel, 66].Value = "LastNameOfficer1";
                                            worksheet.Cells[incRowExcel, 67].Value = "PositionOfficer1";
                                            worksheet.Cells[incRowExcel, 68].Value = "PhoneNumberOfficer1";
                                            worksheet.Cells[incRowExcel, 69].Value = "EmailOfficer1";
                                            worksheet.Cells[incRowExcel, 70].Value = "IDType1Officer1";
                                            worksheet.Cells[incRowExcel, 71].Value = "IDNumber1Officer1";
                                            worksheet.Cells[incRowExcel, 72].Value = "IDRegDate1Officer1";
                                            worksheet.Cells[incRowExcel, 73].Value = "IDExpireDate1Officer1";
                                            worksheet.Cells[incRowExcel, 74].Value = "IDType2Officer1";
                                            worksheet.Cells[incRowExcel, 75].Value = "IDNumber2Officer1";
                                            worksheet.Cells[incRowExcel, 76].Value = "IDRegDate2Officer1";
                                            worksheet.Cells[incRowExcel, 77].Value = "IDExpireDate2Officer1";
                                            worksheet.Cells[incRowExcel, 78].Value = "IDType3Officer1";
                                            worksheet.Cells[incRowExcel, 79].Value = "IDNumber3Officer1";
                                            worksheet.Cells[incRowExcel, 80].Value = "IDRegDate3Officer1";
                                            worksheet.Cells[incRowExcel, 81].Value = "IDExpireDate3Officer1";
                                            worksheet.Cells[incRowExcel, 82].Value = "IDType4Officer1";
                                            worksheet.Cells[incRowExcel, 83].Value = "IDNumber4Officer1";
                                            worksheet.Cells[incRowExcel, 84].Value = "IDRegDate4Officer1";
                                            worksheet.Cells[incRowExcel, 85].Value = "IDExpireDate4Officer1";
                                            worksheet.Cells[incRowExcel, 86].Value = "FirstNameOfficer2";
                                            worksheet.Cells[incRowExcel, 87].Value = "MiddleNameOfficer2";
                                            worksheet.Cells[incRowExcel, 88].Value = "LastNameOfficer2";
                                            worksheet.Cells[incRowExcel, 89].Value = "PositionOfficer2";
                                            worksheet.Cells[incRowExcel, 90].Value = "IDType1Officer2";
                                            worksheet.Cells[incRowExcel, 91].Value = "IDNumber1Officer2";
                                            worksheet.Cells[incRowExcel, 92].Value = "IDRegDate1Officer2";
                                            worksheet.Cells[incRowExcel, 93].Value = "IDExpireDate1Officer2";
                                            worksheet.Cells[incRowExcel, 94].Value = "IDType2Officer2";
                                            worksheet.Cells[incRowExcel, 95].Value = "IDNumber2Officer2";
                                            worksheet.Cells[incRowExcel, 96].Value = "IDRegDate2Officer2";
                                            worksheet.Cells[incRowExcel, 97].Value = "IDExpireDate2Officer2";
                                            worksheet.Cells[incRowExcel, 98].Value = "IDType3Officer2";
                                            worksheet.Cells[incRowExcel, 99].Value = "IDNumber3Officer2";
                                            worksheet.Cells[incRowExcel, 100].Value = "IDRegDate3Officer2";
                                            worksheet.Cells[incRowExcel, 101].Value = "IDExpireDate3Officer2";
                                            worksheet.Cells[incRowExcel, 102].Value = "IDType4Officer2";
                                            worksheet.Cells[incRowExcel, 103].Value = "IDNumber4Officer2";
                                            worksheet.Cells[incRowExcel, 104].Value = "IDRegDate4Officer2";
                                            worksheet.Cells[incRowExcel, 105].Value = "IDExpireDate4Officer2";
                                            worksheet.Cells[incRowExcel, 106].Value = "EstablishmentDate";
                                            worksheet.Cells[incRowExcel, 107].Value = "EstablishmentPlace";
                                            worksheet.Cells[incRowExcel, 108].Value = "CountryofEstablishment";
                                            worksheet.Cells[incRowExcel, 109].Value = "CityOfEstablishment";
                                            worksheet.Cells[incRowExcel, 110].Value = "CountryofCompany";
                                            worksheet.Cells[incRowExcel, 111].Value = "CompanyCityName";
                                            worksheet.Cells[incRowExcel, 112].Value = "CompanyAddress";
                                            worksheet.Cells[incRowExcel, 113].Value = "ZIPCode";
                                            worksheet.Cells[incRowExcel, 114].Value = "BusinessPhone";
                                            worksheet.Cells[incRowExcel, 115].Value = "AssetFor1Year";
                                            worksheet.Cells[incRowExcel, 116].Value = "AssetFor2Year";
                                            worksheet.Cells[incRowExcel, 117].Value = "AssetFor3Year";
                                            worksheet.Cells[incRowExcel, 118].Value = "OperatingProfitFor1Year";
                                            worksheet.Cells[incRowExcel, 119].Value = "OperatingProfitFor2Year";
                                            worksheet.Cells[incRowExcel, 120].Value = "OperatingProfitFor3Year";
                                            worksheet.Cells[incRowExcel, 121].Value = "FirstNameOfficer3";
                                            worksheet.Cells[incRowExcel, 122].Value = "MiddleNameOfficer3";
                                            worksheet.Cells[incRowExcel, 123].Value = "LastNameOfficer3";
                                            worksheet.Cells[incRowExcel, 124].Value = "PositionOfficer3";
                                            worksheet.Cells[incRowExcel, 125].Value = "IDType1Officer3";
                                            worksheet.Cells[incRowExcel, 126].Value = "IDNumber1Officer3";
                                            worksheet.Cells[incRowExcel, 127].Value = "IDRegDate1Officer3";
                                            worksheet.Cells[incRowExcel, 128].Value = "IDExpireDate1Officer3";
                                            worksheet.Cells[incRowExcel, 129].Value = "IDType2Officer3";
                                            worksheet.Cells[incRowExcel, 130].Value = "IDNumber2Officer3";
                                            worksheet.Cells[incRowExcel, 131].Value = "IDRegDate2Officer3";
                                            worksheet.Cells[incRowExcel, 132].Value = "IDExpireDate2Officer3";
                                            worksheet.Cells[incRowExcel, 133].Value = "IDType3Officer3";
                                            worksheet.Cells[incRowExcel, 134].Value = "IDNumber3Officer3";
                                            worksheet.Cells[incRowExcel, 135].Value = "IDRegDate3Officer3";
                                            worksheet.Cells[incRowExcel, 136].Value = "IDExpireDate3Officer3";
                                            worksheet.Cells[incRowExcel, 137].Value = "IDType4Officer3";
                                            worksheet.Cells[incRowExcel, 138].Value = "IDNumber4Officer3";
                                            worksheet.Cells[incRowExcel, 139].Value = "IDRegDate4Officer3";
                                            worksheet.Cells[incRowExcel, 140].Value = "IDExpireDate4Officer3";
                                            worksheet.Cells[incRowExcel, 141].Value = "FirstNameOfficer4";
                                            worksheet.Cells[incRowExcel, 142].Value = "MiddleNameOfficer4";
                                            worksheet.Cells[incRowExcel, 143].Value = "LastNameOfficer4";
                                            worksheet.Cells[incRowExcel, 144].Value = "PositionOfficer4";
                                            worksheet.Cells[incRowExcel, 145].Value = "IDType1Officer4";
                                            worksheet.Cells[incRowExcel, 146].Value = "IDNumber1Officer4";
                                            worksheet.Cells[incRowExcel, 147].Value = "IDRegDate1Officer4";
                                            worksheet.Cells[incRowExcel, 148].Value = "IDExpireDate1Officer4";
                                            worksheet.Cells[incRowExcel, 149].Value = "IDType2Officer4";
                                            worksheet.Cells[incRowExcel, 150].Value = "IDNumber2Officer4";
                                            worksheet.Cells[incRowExcel, 151].Value = "IDRegDate2Officer4";
                                            worksheet.Cells[incRowExcel, 152].Value = "IDExpireDate2Officer4";
                                            worksheet.Cells[incRowExcel, 153].Value = "IDType3Officer4";
                                            worksheet.Cells[incRowExcel, 154].Value = "IDNumber3Officer4";
                                            worksheet.Cells[incRowExcel, 155].Value = "IDRegDate3Officer4";
                                            worksheet.Cells[incRowExcel, 156].Value = "IDExpireDate3Officer4";
                                            worksheet.Cells[incRowExcel, 157].Value = "IDType4Officer4";
                                            worksheet.Cells[incRowExcel, 158].Value = "IDNumber4Officer4";
                                            worksheet.Cells[incRowExcel, 159].Value = "IDRegDate4Officer4";
                                            worksheet.Cells[incRowExcel, 160].Value = "IDExpireDate4Officer4";
                                            worksheet.Cells[incRowExcel, 161].Value = "FATCAStatus";
                                            worksheet.Cells[incRowExcel, 162].Value = "TIN";
                                            worksheet.Cells[incRowExcel, 163].Value = "TINIssuanceCountry";
                                            worksheet.Cells[incRowExcel, 164].Value = "GIIN";
                                            worksheet.Cells[incRowExcel, 165].Value = "SubstantialOwnerName";
                                            worksheet.Cells[incRowExcel, 166].Value = "SubstantialOwnerAddress";
                                            worksheet.Cells[incRowExcel, 167].Value = "SubstantialOwnerTIN";
                                            worksheet.Cells[incRowExcel, 168].Value = "EntryUsersID";
                                            worksheet.Cells[incRowExcel, 169].Value = "EntryTime";
                                            worksheet.Cells[incRowExcel, 170].Value = "UpdateUsersID";
                                            worksheet.Cells[incRowExcel, 171].Value = "UpdateTime";
                                            worksheet.Cells[incRowExcel, 172].Value = "ApprovedUsersID";
                                            worksheet.Cells[incRowExcel, 173].Value = "ApprovedTime";
                                            worksheet.Cells[incRowExcel, 174].Value = "VoidUsersID";
                                            worksheet.Cells[incRowExcel, 175].Value = "VoidTime";
                                            worksheet.Cells[incRowExcel, 176].Value = "SuspendBy";
                                            worksheet.Cells[incRowExcel, 177].Value = "SuspendTime";
                                            worksheet.Cells[incRowExcel, 178].Value = "UnSuspendBy";
                                            worksheet.Cells[incRowExcel, 179].Value = "UnSuspendTime";
                                            string _range = "A" + incRowExcel + ":FW" + incRowExcel;
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
                                                r.Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                r.Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            }
                                            incRowExcel++;


                                            int _startRowDetail = incRowExcel;
                                            int _endRowDetail = 0;
                                            //end area header
                                            foreach (var rsDetail in rsHeader)
                                            {
                                                //area detail
                                                worksheet.Cells[incRowExcel, 1].Value = rsDetail.FundClientID;
                                                worksheet.Cells[incRowExcel, 2].Value = rsDetail.InvestorType;
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.InternalCategory;
                                                worksheet.Cells[incRowExcel, 4].Value = rsDetail.InternalName;
                                                worksheet.Cells[incRowExcel, 5].Value = rsDetail.SellingAgent;
                                                worksheet.Cells[incRowExcel, 6].Value = rsDetail.NPWP;
                                                worksheet.Cells[incRowExcel, 7].Value = rsDetail.RegistrationNPWP;
                                                worksheet.Cells[incRowExcel, 8].Value = rsDetail.Email;
                                                worksheet.Cells[incRowExcel, 9].Value = rsDetail.PhoneNumber;
                                                worksheet.Cells[incRowExcel, 10].Value = rsDetail.MobilePhone;
                                                worksheet.Cells[incRowExcel, 11].Value = rsDetail.Fax;
                                                worksheet.Cells[incRowExcel, 12].Value = rsDetail.SID;
                                                worksheet.Cells[incRowExcel, 13].Value = rsDetail.IFUACode;
                                                worksheet.Cells[incRowExcel, 14].Value = rsDetail.InvestorsRiskProfile;
                                                worksheet.Cells[incRowExcel, 15].Value = rsDetail.KYCRiskProfile;
                                                worksheet.Cells[incRowExcel, 16].Value = rsDetail.AssetOwner;
                                                worksheet.Cells[incRowExcel, 17].Value = rsDetail.StatementType;
                                                worksheet.Cells[incRowExcel, 18].Value = rsDetail.SellingAgentCode;
                                                worksheet.Cells[incRowExcel, 19].Value = rsDetail.DormantDate;
                                                worksheet.Cells[incRowExcel, 20].Value = rsDetail.Description;
                                                worksheet.Cells[incRowExcel, 21].Value = rsDetail.Affiliated;
                                                worksheet.Cells[incRowExcel, 22].Value = rsDetail.AffiliatedWith;
                                                worksheet.Cells[incRowExcel, 23].Value = rsDetail.Suspended;
                                                worksheet.Cells[incRowExcel, 24].Value = rsDetail.BankName1;
                                                worksheet.Cells[incRowExcel, 25].Value = rsDetail.BankAccountName1;
                                                worksheet.Cells[incRowExcel, 26].Value = rsDetail.BankAccountNumber1;
                                                worksheet.Cells[incRowExcel, 27].Value = rsDetail.BankBranchName1;
                                                worksheet.Cells[incRowExcel, 28].Value = rsDetail.Currency1;
                                                worksheet.Cells[incRowExcel, 29].Value = rsDetail.BICCode1;
                                                worksheet.Cells[incRowExcel, 30].Value = rsDetail.BIMemberCode1;
                                                worksheet.Cells[incRowExcel, 31].Value = rsDetail.BankName2;
                                                worksheet.Cells[incRowExcel, 32].Value = rsDetail.BankAccountName2;
                                                worksheet.Cells[incRowExcel, 33].Value = rsDetail.BankAccountNumber2;
                                                worksheet.Cells[incRowExcel, 34].Value = rsDetail.BankBranchName2;
                                                worksheet.Cells[incRowExcel, 35].Value = rsDetail.Currency2;
                                                worksheet.Cells[incRowExcel, 36].Value = rsDetail.BICCode2;
                                                worksheet.Cells[incRowExcel, 37].Value = rsDetail.BIMemberCode2;
                                                worksheet.Cells[incRowExcel, 38].Value = rsDetail.BankName3;
                                                worksheet.Cells[incRowExcel, 39].Value = rsDetail.BankAccountName3;
                                                worksheet.Cells[incRowExcel, 40].Value = rsDetail.BankAccountNumber3;
                                                worksheet.Cells[incRowExcel, 41].Value = rsDetail.BankBranchName3;
                                                worksheet.Cells[incRowExcel, 42].Value = rsDetail.Currency3;
                                                worksheet.Cells[incRowExcel, 43].Value = rsDetail.BICCode3;
                                                worksheet.Cells[incRowExcel, 44].Value = rsDetail.BIMemberCode3;
                                                worksheet.Cells[incRowExcel, 45].Value = rsDetail.BankRDN;
                                                worksheet.Cells[incRowExcel, 46].Value = rsDetail.RDNAccountName;
                                                worksheet.Cells[incRowExcel, 47].Value = rsDetail.RDNAccountNumber;
                                                worksheet.Cells[incRowExcel, 48].Value = rsDetail.CompanyName;
                                                worksheet.Cells[incRowExcel, 49].Value = rsDetail.CompanyAddress;
                                                worksheet.Cells[incRowExcel, 50].Value = rsDetail.CompanyZipCode;
                                                worksheet.Cells[incRowExcel, 51].Value = rsDetail.CompanyCity;
                                                worksheet.Cells[incRowExcel, 52].Value = rsDetail.CompanyLegalDomicile;
                                                worksheet.Cells[incRowExcel, 53].Value = rsDetail.CountryofDomicile;
                                                worksheet.Cells[incRowExcel, 54].Value = rsDetail.CompanyType;
                                                worksheet.Cells[incRowExcel, 55].Value = rsDetail.CompanyCharacteristic;
                                                worksheet.Cells[incRowExcel, 56].Value = rsDetail.CompanyIncomePerAnnum;
                                                worksheet.Cells[incRowExcel, 57].Value = rsDetail.CompanySourceOfFunds;
                                                worksheet.Cells[incRowExcel, 58].Value = rsDetail.CompanyInvestmentObjective;
                                                worksheet.Cells[incRowExcel, 59].Value = rsDetail.SKDNumber;
                                                worksheet.Cells[incRowExcel, 60].Value = Convert.ToDateTime(rsDetail.ExpiredDateSKD).ToString("dd/MM/yyyy");
                                                worksheet.Cells[incRowExcel, 61].Value = rsDetail.ArticleOfAssociation;
                                                worksheet.Cells[incRowExcel, 62].Value = rsDetail.SIUPNumber;
                                                worksheet.Cells[incRowExcel, 63].Value = Convert.ToDateTime(rsDetail.SIUPExpirationDate).ToString("dd/MM/yyyy");
                                                worksheet.Cells[incRowExcel, 64].Value = rsDetail.FirstNameOfficer1;
                                                worksheet.Cells[incRowExcel, 65].Value = rsDetail.MiddleNameOfficer1;
                                                worksheet.Cells[incRowExcel, 66].Value = rsDetail.LastNameOfficer1;
                                                worksheet.Cells[incRowExcel, 67].Value = rsDetail.PositionOfficer1;
                                                worksheet.Cells[incRowExcel, 68].Value = rsDetail.PhoneNumberOfficer1;
                                                worksheet.Cells[incRowExcel, 69].Value = rsDetail.EmailOfficer1;
                                                worksheet.Cells[incRowExcel, 70].Value = rsDetail.IDType1Officer1;
                                                worksheet.Cells[incRowExcel, 71].Value = rsDetail.IDNumber1Officer1;
                                                worksheet.Cells[incRowExcel, 72].Value = Convert.ToDateTime(rsDetail.IDRegDate1Officer1).ToString("dd/MM/yyyy");
                                                worksheet.Cells[incRowExcel, 73].Value = Convert.ToDateTime(rsDetail.IDExpireDate1Officer1).ToString("dd/MM/yyyy");
                                                worksheet.Cells[incRowExcel, 74].Value = rsDetail.IDType2Officer1;
                                                worksheet.Cells[incRowExcel, 75].Value = rsDetail.IDNumber2Officer1;
                                                worksheet.Cells[incRowExcel, 76].Value = Convert.ToDateTime(rsDetail.IDRegDate2Officer1).ToString("dd/MM/yyyy");
                                                worksheet.Cells[incRowExcel, 77].Value = Convert.ToDateTime(rsDetail.IDExpireDate2Officer1).ToString("dd/MM/yyyy");
                                                worksheet.Cells[incRowExcel, 78].Value = rsDetail.IDType3Officer1;
                                                worksheet.Cells[incRowExcel, 79].Value = rsDetail.IDNumber3Officer1;
                                                worksheet.Cells[incRowExcel, 80].Value = Convert.ToDateTime(rsDetail.IDRegDate3Officer1).ToString("dd/MM/yyyy");
                                                worksheet.Cells[incRowExcel, 81].Value = Convert.ToDateTime(rsDetail.IDExpireDate3Officer1).ToString("dd/MM/yyyy");
                                                worksheet.Cells[incRowExcel, 82].Value = rsDetail.IDType4Officer1;
                                                worksheet.Cells[incRowExcel, 83].Value = rsDetail.IDNumber4Officer1;
                                                worksheet.Cells[incRowExcel, 84].Value = Convert.ToDateTime(rsDetail.IDRegDate4Officer1).ToString("dd/MM/yyyy");
                                                worksheet.Cells[incRowExcel, 85].Value = Convert.ToDateTime(rsDetail.IDExpireDate4Officer1).ToString("dd/MM/yyyy");
                                                worksheet.Cells[incRowExcel, 86].Value = rsDetail.FirstNameOfficer2;
                                                worksheet.Cells[incRowExcel, 87].Value = rsDetail.MiddleNameOfficer2;
                                                worksheet.Cells[incRowExcel, 88].Value = rsDetail.LastNameOfficer2;
                                                worksheet.Cells[incRowExcel, 89].Value = rsDetail.PositionOfficer2;
                                                worksheet.Cells[incRowExcel, 90].Value = rsDetail.IDType1Officer2;
                                                worksheet.Cells[incRowExcel, 91].Value = rsDetail.IDNumber1Officer2;
                                                worksheet.Cells[incRowExcel, 92].Value = Convert.ToDateTime(rsDetail.IDRegDate1Officer2).ToString("dd/MM/yyyy");
                                                worksheet.Cells[incRowExcel, 93].Value = Convert.ToDateTime(rsDetail.IDExpireDate1Officer2).ToString("dd/MM/yyyy");
                                                worksheet.Cells[incRowExcel, 94].Value = rsDetail.IDType2Officer2;
                                                worksheet.Cells[incRowExcel, 95].Value = rsDetail.IDNumber2Officer2;
                                                worksheet.Cells[incRowExcel, 96].Value = Convert.ToDateTime(rsDetail.IDRegDate2Officer2).ToString("dd/MM/yyyy");
                                                worksheet.Cells[incRowExcel, 97].Value = Convert.ToDateTime(rsDetail.IDExpireDate2Officer2).ToString("dd/MM/yyyy");
                                                worksheet.Cells[incRowExcel, 98].Value = rsDetail.IDType3Officer2;
                                                worksheet.Cells[incRowExcel, 99].Value = rsDetail.IDNumber3Officer2;
                                                worksheet.Cells[incRowExcel, 100].Value = Convert.ToDateTime(rsDetail.IDRegDate3Officer2).ToString("dd/MM/yyyy");
                                                worksheet.Cells[incRowExcel, 101].Value = Convert.ToDateTime(rsDetail.IDExpireDate3Officer2).ToString("dd/MM/yyyy");
                                                worksheet.Cells[incRowExcel, 102].Value = rsDetail.IDType4Officer2;
                                                worksheet.Cells[incRowExcel, 103].Value = rsDetail.IDNumber4Officer2;
                                                worksheet.Cells[incRowExcel, 104].Value = Convert.ToDateTime(rsDetail.IDRegDate4Officer2).ToString("dd/MM/yyyy");
                                                worksheet.Cells[incRowExcel, 105].Value = Convert.ToDateTime(rsDetail.IDExpireDate4Officer2).ToString("dd/MM/yyyy");
                                                worksheet.Cells[incRowExcel, 106].Value = Convert.ToDateTime(rsDetail.EstablishmentDate).ToString("dd/MM/yyyy");
                                                worksheet.Cells[incRowExcel, 107].Value = rsDetail.EstablishmentPlace;
                                                worksheet.Cells[incRowExcel, 108].Value = rsDetail.CountryofEstablishment;
                                                worksheet.Cells[incRowExcel, 109].Value = rsDetail.CityOfEstablishment;
                                                worksheet.Cells[incRowExcel, 110].Value = rsDetail.CountryofCompany;
                                                worksheet.Cells[incRowExcel, 111].Value = rsDetail.CompanyCityName;
                                                worksheet.Cells[incRowExcel, 112].Value = rsDetail.CompanyAddress;
                                                worksheet.Cells[incRowExcel, 113].Value = rsDetail.CompanyZipCode;
                                                worksheet.Cells[incRowExcel, 114].Value = rsDetail.BusinessPhone;
                                                worksheet.Cells[incRowExcel, 115].Value = rsDetail.AssetFor1Year;
                                                worksheet.Cells[incRowExcel, 116].Value = rsDetail.AssetFor2Year;
                                                worksheet.Cells[incRowExcel, 117].Value = rsDetail.AssetFor3Year;
                                                worksheet.Cells[incRowExcel, 118].Value = rsDetail.OperatingProfitFor1Year;
                                                worksheet.Cells[incRowExcel, 119].Value = rsDetail.OperatingProfitFor2Year;
                                                worksheet.Cells[incRowExcel, 120].Value = rsDetail.OperatingProfitFor3Year;
                                                worksheet.Cells[incRowExcel, 121].Value = rsDetail.FirstNameOfficer3;
                                                worksheet.Cells[incRowExcel, 122].Value = rsDetail.MiddleNameOfficer3;
                                                worksheet.Cells[incRowExcel, 123].Value = rsDetail.LastNameOfficer3;
                                                worksheet.Cells[incRowExcel, 124].Value = rsDetail.PositionOfficer3;
                                                worksheet.Cells[incRowExcel, 125].Value = rsDetail.IDType1Officer3;
                                                worksheet.Cells[incRowExcel, 126].Value = rsDetail.IDNumber1Officer3;
                                                worksheet.Cells[incRowExcel, 127].Value = Convert.ToDateTime(rsDetail.IDRegDate1Officer3).ToString("dd/MM/yyyy");
                                                worksheet.Cells[incRowExcel, 128].Value = Convert.ToDateTime(rsDetail.IDExpireDate1Officer3).ToString("dd/MM/yyyy");
                                                worksheet.Cells[incRowExcel, 129].Value = rsDetail.IDType2Officer3;
                                                worksheet.Cells[incRowExcel, 130].Value = rsDetail.IDNumber2Officer3;
                                                worksheet.Cells[incRowExcel, 131].Value = Convert.ToDateTime(rsDetail.IDRegDate2Officer3).ToString("dd/MM/yyyy");
                                                worksheet.Cells[incRowExcel, 132].Value = Convert.ToDateTime(rsDetail.IDExpireDate2Officer3).ToString("dd/MM/yyyy");
                                                worksheet.Cells[incRowExcel, 133].Value = rsDetail.IDType3Officer3;
                                                worksheet.Cells[incRowExcel, 134].Value = rsDetail.IDNumber3Officer3;
                                                worksheet.Cells[incRowExcel, 135].Value = Convert.ToDateTime(rsDetail.IDRegDate3Officer3).ToString("dd/MM/yyyy");
                                                worksheet.Cells[incRowExcel, 136].Value = Convert.ToDateTime(rsDetail.IDExpireDate3Officer3).ToString("dd/MM/yyyy");
                                                worksheet.Cells[incRowExcel, 137].Value = rsDetail.IDType4Officer3;
                                                worksheet.Cells[incRowExcel, 138].Value = rsDetail.IDNumber4Officer3;
                                                worksheet.Cells[incRowExcel, 139].Value = Convert.ToDateTime(rsDetail.IDRegDate4Officer3).ToString("dd/MM/yyyy");
                                                worksheet.Cells[incRowExcel, 140].Value = Convert.ToDateTime(rsDetail.IDExpireDate4Officer3).ToString("dd/MM/yyyy");
                                                worksheet.Cells[incRowExcel, 141].Value = rsDetail.FirstNameOfficer4;
                                                worksheet.Cells[incRowExcel, 142].Value = rsDetail.MiddleNameOfficer4;
                                                worksheet.Cells[incRowExcel, 143].Value = rsDetail.LastNameOfficer4;
                                                worksheet.Cells[incRowExcel, 144].Value = rsDetail.PositionOfficer4;
                                                worksheet.Cells[incRowExcel, 145].Value = rsDetail.IDType1Officer4;
                                                worksheet.Cells[incRowExcel, 146].Value = rsDetail.IDNumber1Officer4;
                                                worksheet.Cells[incRowExcel, 147].Value = Convert.ToDateTime(rsDetail.IDRegDate1Officer4).ToString("dd/MM/yyyy");
                                                worksheet.Cells[incRowExcel, 148].Value = Convert.ToDateTime(rsDetail.IDExpireDate1Officer4).ToString("dd/MM/yyyy");
                                                worksheet.Cells[incRowExcel, 149].Value = rsDetail.IDType2Officer4;
                                                worksheet.Cells[incRowExcel, 150].Value = rsDetail.IDNumber2Officer4;
                                                worksheet.Cells[incRowExcel, 151].Value = Convert.ToDateTime(rsDetail.IDRegDate2Officer4).ToString("dd/MM/yyyy");
                                                worksheet.Cells[incRowExcel, 152].Value = Convert.ToDateTime(rsDetail.IDExpireDate2Officer4).ToString("dd/MM/yyyy");
                                                worksheet.Cells[incRowExcel, 153].Value = rsDetail.IDType3Officer4;
                                                worksheet.Cells[incRowExcel, 154].Value = rsDetail.IDNumber3Officer4;
                                                worksheet.Cells[incRowExcel, 155].Value = Convert.ToDateTime(rsDetail.IDRegDate3Officer4).ToString("dd/MM/yyyy");
                                                worksheet.Cells[incRowExcel, 156].Value = Convert.ToDateTime(rsDetail.IDExpireDate3Officer4).ToString("dd/MM/yyyy");
                                                worksheet.Cells[incRowExcel, 157].Value = rsDetail.IDType4Officer4;
                                                worksheet.Cells[incRowExcel, 158].Value = rsDetail.IDNumber4Officer4;
                                                worksheet.Cells[incRowExcel, 159].Value = Convert.ToDateTime(rsDetail.IDRegDate4Officer4).ToString("dd/MM/yyyy");
                                                worksheet.Cells[incRowExcel, 160].Value = Convert.ToDateTime(rsDetail.IDExpireDate4Officer4).ToString("dd/MM/yyyy");
                                                worksheet.Cells[incRowExcel, 161].Value = rsDetail.FATCAStatus;
                                                worksheet.Cells[incRowExcel, 162].Value = rsDetail.TIN;
                                                worksheet.Cells[incRowExcel, 163].Value = rsDetail.TINIssuanceCountry;
                                                worksheet.Cells[incRowExcel, 164].Value = rsDetail.GIIN;
                                                worksheet.Cells[incRowExcel, 165].Value = rsDetail.SubstantialOwnerName;
                                                worksheet.Cells[incRowExcel, 166].Value = rsDetail.SubstantialOwnerAddress;
                                                worksheet.Cells[incRowExcel, 167].Value = rsDetail.SubstantialOwnerTIN;
                                                worksheet.Cells[incRowExcel, 168].Value = rsDetail.EntryUsersID;
                                                worksheet.Cells[incRowExcel, 169].Value = rsDetail.EntryTime;
                                                worksheet.Cells[incRowExcel, 170].Value = rsDetail.UpdateUsersID;
                                                worksheet.Cells[incRowExcel, 171].Value = rsDetail.UpdateTime;
                                                worksheet.Cells[incRowExcel, 172].Value = rsDetail.ApprovedUsersID;
                                                worksheet.Cells[incRowExcel, 173].Value = rsDetail.ApprovedTime;
                                                worksheet.Cells[incRowExcel, 174].Value = rsDetail.VoidUsersID;
                                                worksheet.Cells[incRowExcel, 175].Value = rsDetail.VoidTime;
                                                worksheet.Cells[incRowExcel, 176].Value = rsDetail.SuspendBy;
                                                worksheet.Cells[incRowExcel, 177].Value = rsDetail.SuspendTime;
                                                worksheet.Cells[incRowExcel, 178].Value = rsDetail.UnSuspendBy;
                                                worksheet.Cells[incRowExcel, 179].Value = rsDetail.UnSuspendTime;

                                                _endRowDetail = incRowExcel;
                                                worksheet.Cells["A" + incRowExcel + ":FW" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + incRowExcel + ":FW" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                                incRowExcel++;
                                            }

                                            //_endRowDetail = incRowExcel;
                                            //worksheet.Cells["A" + _endRowDetail + ":FY" + _endRowDetail].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + _endRowDetail + ":FW" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;


                                        }

                                        // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                        worksheet.Cells.AutoFitColumns(0);
                                        worksheet.PrinterSettings.Orientation = eOrientation.Landscape; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                        // worksheet.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN
                                        // worksheet.PrinterSettings.FitToPage = true;
                                        //worksheet.HeaderFooter.OddHeader.RightAlignedText = "&14 PAYMENT VOUCHER";
                                        // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                        worksheet.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
                                        worksheet.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
                                        //worksheet.View.PageLayoutView = Tools.DefaultReportPageLayoutView();
                                        worksheet.View.ShowGridLines = Tools.DefaultReportShowGridLines();
                                        worksheet.HeaderFooter.OddHeader.CenteredText = "&14 Fund Client";
                                        worksheet.HeaderFooter.OddHeader.LeftAlignedText = Tools.DefaultReportHeaderLeftText();

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
                    else if (_fundClientRpt.InvestorType == 3)
                    {
                        string _paramDateFrom = "";
                        if (_fundClientRpt.ParamDate == "9")
                        {
                            _paramDateFrom = " and FC.DatePengkinianData <> '01/01/1900' and FC.DatePengkinianData is not null and FC.DatePengkinianData Between  '" + _fundClientRpt.DateFrom + "'  " + " and '" + _fundClientRpt.DateTo + "'";
                        }
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = @"
                                select FC.ID FundClientID,mv8.DescOne InvestorType,isnull(IC.Name,'') InternalCategory,  FC.NAME InternalName,A.Name SellingAgent,FC.SID,FC.IFUACode,
                                mv33.DescOne InvestorsRiskProfile, FC.DormantDate, case when FC.BitIsAfiliated=0 then 'No' else 'Yes' end Affiliated, Z.NAME AffiliatedWith,
                                case when FC.BitIsSuspend=0 then 'No' else 'Yes' end Suspended,mv69.DescOne KYCRiskProfile,
                                FC.NPWP,FC.RegistrationNPWP,FC.CompanyMail Email,FC.TeleponBisnis PhoneNumber,FC.TeleponSelular MobilePhone,FC.Companyfax Fax,
                                mv24.DescOne Country,mv25.DescOne Nationality,fc.SACode,mv43.DescOne CountryofDomicile,mv3.DescOne CityOfEstablishment,fc.TeleponBisnis BusinessPhone, 
                                BC4.ID BankRDN,Fc.RDNAccountNo RDNAccountName, FC.RDNAccountName RDNAccountNumber,
                                BC1.ID BankName1,Fc.NamaNasabah1 BankAccountName1, FC.NomorRekening1 BankAccountNumber1, FC.BankBranchName1, Y.ID Currency1, BC1.Name BICode1,BC1.Name BIMemberCode1,
                                BC1.ID BankName2,Fc.NamaNasabah2 BankAccountName2, FC.NomorRekening2 BankAccountNumber2, FC.BankBranchName2, W.ID Currency2, BC2.Name BICode2,BC2.Name BIMemberCode2, 
                                BC1.ID BankName3,Fc.NamaNasabah3 BankAccountName3, FC.NomorRekening3 BankAccountNumber3, FC.BankBranchName3, V.ID Currency3, BC3.Name BICode3,BC3.Name BIMemberCode3,  
                                mv34.DescOne AssetOwner,mv35.DescOne StatementType,mv36.DescOne FATCAStatus,FC.TIN,mv37.DescOne TINIssuanceCountry, FC.GIIN, FC.SubstantialOwnerName,  
                                FC.SubstantialOwnerAddress, FC.SubstantialOwnerTIN,Fc.NamaPerusahaan CompanyName, FC.AlamatPerusahaan CompanyAddress, FC.KodePosIns CompanyZipCode, mv3.DescOne CompanyCity, mv17.DescOne CompanyLegalDomicile, 
                                FC.TanggalBerdiri EstablishmentDate,FC.LokasiBerdiri EstablishmentPlace, mv44.DescOne CountryofEstablishment, FC.NoSKD SKDNumber,  FC.ExpiredDateSKD,
                                FC.NomorAnggaran ArticleOfAssociation, FC.NomorSIUP SIUPNumber,FC.SIUPExpirationDate ,FC.AssetFor1Year, FC.AssetFor2Year, FC.AssetFor3Year,
                                FC.OperatingProfitFor1Year,FC.OperatingProfitFor2Year,FC.OperatingProfitFor3Year,
                                mv18.DescOne CompanyType, mv19.DescOne CompanyCharacteristic,mv20.DescOne CompanyIncomePerAnnum,  
                                mv21.DescOne CompanySourceOfFunds,mv22.DescOne CompanyInvestmentObjective,   
                                mv45.DescOne CountryofCompany, mv46.DescOne CompanyCityName, mv26.DescOne Province, 
                                FC.NamaDepanIns1 FirstNameOfficer1,FC.NamaTengahIns1 MiddleNameOfficer1,FC.NamaBelakangIns1 LastNameOfficer1,FC.Jabatan1 PositionOfficer1,FC.PhoneIns1 PhoneNumberOfficer1, FC.EmailIns1 EmailOfficer1,
                                FC.IdentitasIns11 IDType1Officer1, FC.NoIdentitasIns11 IDNumber1Officer1, FC.RegistrationDateIdentitasIns11 IDRegDate1Officer1, FC.ExpiredDateIdentitasIns11 IDExpireDate1Officer1,
                                FC.IdentitasIns12 IDType2Officer1, FC.NoIdentitasIns12 IDNumber2Officer1, FC.RegistrationDateIdentitasIns12 IDRegDate2Officer1, FC.ExpiredDateIdentitasIns12 IDExpireDate2Officer1,
                                FC.IdentitasIns13 IDType3Officer1, FC.NoIdentitasIns13 IDNumber3Officer1, FC.RegistrationDateIdentitasIns13 IDRegDate3Officer1, FC.ExpiredDateIdentitasIns13 IDExpireDate3Officer1,
                                FC.IdentitasIns14 IDType4Officer1, FC.NoIdentitasIns14 IDNumber4Officer1, FC.RegistrationDateIdentitasIns14 IDRegDate4Officer1, FC.ExpiredDateIdentitasIns14 IDExpireDate4Officer1,
                                FC.NamaDepanIns2 FirstNameOfficer2,FC.NamaTengahIns2 MiddleNameOfficer2,FC.NamaBelakangIns2 LastNameOfficer2,FC.Jabatan2 PositionOfficer2,
                                FC.IdentitasIns11 IDType1Officer2, FC.NoIdentitasIns11 IDNumber1Officer2, FC.RegistrationDateIdentitasIns11 IDRegDate1Officer2, FC.ExpiredDateIdentitasIns11 IDExpireDate1Officer2,
                                FC.IdentitasIns12 IDType2Officer2, FC.NoIdentitasIns12 IDNumber2Officer2, FC.RegistrationDateIdentitasIns12 IDRegDate2Officer2, FC.ExpiredDateIdentitasIns12 IDExpireDate2Officer2,
                                FC.IdentitasIns13 IDType3Officer2, FC.NoIdentitasIns13 IDNumber3Officer2, FC.RegistrationDateIdentitasIns13 IDRegDate3Officer2, FC.ExpiredDateIdentitasIns13 IDExpireDate3Officer2,
                                FC.IdentitasIns14 IDType4Officer2, FC.NoIdentitasIns14 IDNumber4Officer2, FC.RegistrationDateIdentitasIns14 IDRegDate4Officer2, FC.ExpiredDateIdentitasIns14 IDExpireDate4Officer2,
                                FC.NamaDepanIns3 FirstNameOfficer3,FC.NamaTengahIns3 MiddleNameOfficer3,FC.NamaBelakangIns3 LastNameOfficer3,FC.Jabatan3 PositionOfficer3,
                                FC.IdentitasIns11 IDType1Officer3, FC.NoIdentitasIns11 IDNumber1Officer3, FC.RegistrationDateIdentitasIns11 IDRegDate1Officer3, FC.ExpiredDateIdentitasIns11 IDExpireDate1Officer3,
                                FC.IdentitasIns12 IDType2Officer3, FC.NoIdentitasIns12 IDNumber2Officer3, FC.RegistrationDateIdentitasIns12 IDRegDate2Officer3, FC.ExpiredDateIdentitasIns12 IDExpireDate2Officer3,
                                FC.IdentitasIns13 IDType3Officer3, FC.NoIdentitasIns13 IDNumber3Officer3, FC.RegistrationDateIdentitasIns13 IDRegDate3Officer3, FC.ExpiredDateIdentitasIns13 IDExpireDate3Officer3,
                                FC.IdentitasIns14 IDType4Officer3, FC.NoIdentitasIns14 IDNumber4Officer3, FC.RegistrationDateIdentitasIns14 IDRegDate4Officer3, FC.ExpiredDateIdentitasIns14 IDExpireDate4Officer3,
                                FC.NamaDepanIns4 FirstNameOfficer4,FC.NamaTengahIns4 MiddleNameOfficer4,FC.NamaBelakangIns4 LastNameOfficer4,FC.Jabatan4 PositionOfficer4,
                                FC.IdentitasIns11 IDType1Officer4, FC.NoIdentitasIns11 IDNumber1Officer4, FC.RegistrationDateIdentitasIns11 IDRegDate1Officer4, FC.ExpiredDateIdentitasIns11 IDExpireDate1Officer4,
                                FC.IdentitasIns12 IDType2Officer4, FC.NoIdentitasIns12 IDNumber2Officer4, FC.RegistrationDateIdentitasIns12 IDRegDate2Officer4, FC.ExpiredDateIdentitasIns12 IDExpireDate2Officer4,
                                FC.IdentitasIns13 IDType3Officer4, FC.NoIdentitasIns13 IDNumber3Officer4, FC.RegistrationDateIdentitasIns13 IDRegDate3Officer4, FC.ExpiredDateIdentitasIns13 IDExpireDate3Officer4,
                                FC.IdentitasIns14 IDType4Officer4, FC.NoIdentitasIns14 IDNumber4Officer4, FC.RegistrationDateIdentitasIns14 IDRegDate4Officer4, FC.ExpiredDateIdentitasIns14 IDExpireDate4Officer4,

								--individu
								FC.NamaDepanInd FirstNameInd,FC.NamaTengahInd MiddleNameInd,FC.NamaBelakangInd LastNameInd, FC.TempatLahir BirthPlace,FC.TanggalLahir TanggalLahir,FC.AhliWaris Heir, FC.HubunganAhliWaris HeirRelation,FC.NatureOfBusinessLainnya NatureOfBusinessDesc,
								FC.PolitisLainnya PoliticallyExposedDesc,FC.OtherTeleponRumah OtherHomePhone, FC.OtherTeleponSelular OtherCellPhone, FC.OtherFax, FC.OtherEmail,FC.AlamatInd1 CorrespondenceAddress,FC.AlamatInd2 DomicileAddress,
								FC.KodePosInd2 DomicileZipCode,FC.OtherAlamatInd1 IdentityAddress1,FC.OtherKodePosInd1 IdentityZipCode1,FC.OtherAlamatInd2 IdentityAddress2,FC.OtherKodePosInd2 IdentityZipCode2,FC.OtherAlamatInd3 IdentityAddress3,
								FC.OtherKodePosInd3 IdentityZipCode3,FC.NoIdentitasInd1 IdentityNumber1, FC.RegistrationDateIdentitasInd1, FC.ExpiredDateIdentitasInd1, FC.NoIdentitasInd2 IdentityNumber2, FC.RegistrationDateIdentitasInd2, FC.ExpiredDateIdentitasInd2, 
								FC.NoIdentitasInd3 IdentityNumber3, FC.RegistrationDateIdentitasInd3, FC.ExpiredDateIdentitasInd3, fc.CorrespondenceRT, fc.CorrespondenceRW,FC.Description, Fc.AlamatKantorInd,fc.DomicileRT, fc.DomicileRW, fc.Identity1RT, fc.Identity1RW,FC.MotherMaidenName MotherMaidenName,
								FC.SpouseName , fc.KodeDomisiliPropinsi, fc.KodePosKantorInd, FC.KodePosInd1 CorrespondenceZipCode,
								mv1.DescOne CorrespondenceCity,mv4.DescOne IdentityCity1, mv9.DescOne GenderSex, mv10.DescOne MaritalStatus, mv11.DescOne Occupation, mv12.DescOne Education, mv13.DescOne Religion, mv14.DescOne IncomePerAnnum, mv15.DescOne SourceOfFunds,
								mv16.DescOne InvestmentObjectives, mv68.DescOne SpouseOccupation, mv47.DescOne NatureOfBusiness, mv48.DescOne PoliticallyExposed, mv42.DescOne CountryofCorrespondence, mv1.DescOne DomicileCity, 
								mv73.DescOne CountryofDomicile, mv26.DescOne Propinsi, mv27.DescOne IdentityProvince1, mv30.DescOne IdentityCountry1, mv2.DescOne IdentityCity2, mv28.DescOne IdentityProvince2, mv31.DescOne IdentityCountry2, mv6.DescOne IdentityCity3,
								mv29.DescOne IdentityProvince3, mv32.DescOne IdentityCountry3,mv41.DescOne CountryOfBirth, mv49.DescOne IdentityType1, mv50.DescOne IdentityType2, mv51.DescOne IdentityType3, mv72.DescOne KodeKotaKantorInd, mv74.DescOne KodePropinsiKantorInd,
								mv71.DescOne KodeCountryofKantor, mv70.DescOne KodeDomisiliPropinsi,

                                FC.Description, FC.EntryUsersID, FC.EntryTime, FC.UpdateUsersID, FC.UpdateTime, FC.ApprovedUsersID, FC.ApprovedTime, FC.VoidUsersID, FC.VoidTime,
                                FC.SuspendBy, FC.SuspendTime, FC.UnSuspendBy, FC.UnSuspendTime
                                from FundClient fc   
                                left join Agent A on fc.SellingAgentPK = A.AgentPK and A.status = 2  
                                left join MasterValue mv1 on fc.KodeKotaInd1 = mv1.Code and mv1.ID = 'CityRHB' and mv1.status = 2  
                                left join MasterValue mv2 on fc.KodeKotaInd2 = mv2.Code and  mv2.ID = 'CityRHB' and mv2.status = 2  
                                left join MasterValue mv3 on fc.KodeKotaIns = mv3.Code and  mv3.ID = 'CityRHB' and mv3.status = 2  
                                left join MasterValue mv7 on fc.ClientCategory = mv7.code  and  mv7.ID = 'ClientCategory' and mv7.status = 2  
                                left join MasterValue mv8 on fc.InvestorType = mv8.code and  mv8.ID = 'InvestorType' and mv8.status = 2  
                                left join MasterValue mv17 on fc.Domisili = mv17.code and  mv17.ID = 'Domicile' and mv17.status = 2  
                                left join MasterValue mv18 on fc.Tipe = mv18.code and  mv18.ID = 'CompanyType' and mv18.status = 2  
                                left join MasterValue mv19 on fc.Karakteristik = mv19.code and  mv19.ID = 'CompanyCharacteristic' and mv19.status = 2  
                                left join MasterValue mv20 on fc.PenghasilanInstitusi = mv20.code and  mv20.ID = 'IncomeINS' and mv20.status = 2  
                                left join MasterValue mv21 on fc.SumberDanaInstitusi = mv21.code and  mv21.ID = 'IncomeSourceIND' and mv21.status = 2  
                                left join MasterValue mv22 on fc.MaksudTujuanInstitusi = mv22.code and  mv22.ID = 'InvestmentObjectivesINS' and mv22.status = 2  
                                left join MasterValue mv24 on fc.Negara = mv24.code and  mv24.ID = 'SDICountry' and mv24.status = 2  
                                left join MasterValue mv25 on fc.Nationality = mv25.code and  mv25.ID = 'Nationality' and mv25.status = 2  
                                left join MasterValue mv26 on fc.Propinsi = mv26.code and  mv26.ID = 'SDIProvince' and mv26.status = 2  
                                left join InternalCategory IC on fc.InternalCategoryPK = IC.InternalCategoryPK and IC.status = 2  
                                left join MasterValue mv33 on fc.InvestorsRiskProfile = mv33.code and  mv33.ID = 'InvestorsRiskProfile' and mv33.status = 2  
                                left join MasterValue mv34 on fc.AssetOwner = mv34.code and  mv34.ID = 'AssetOwner' and mv34.status = 2  
                                left join MasterValue mv35 on fc.StatementType = mv35.code and  mv35.ID = 'StatementType' and mv35.status = 2  
                                left join MasterValue mv36 on fc.fatca = mv36.code and  mv36.ID = 'FATCAInsti' and mv36.status = 2  
                                left join MasterValue mv37 on fc.TINIssuanceCountry = mv37.code and  mv37.ID = 'SDICountry' and mv37.status = 2  
                                left join MasterValue mv41 on fc.CountryOfBirth = mv41.code and  mv41.ID = 'SDICountry' and mv41.status = 2  
                                left join MasterValue mv42 on fc.CountryofCorrespondence = mv42.code and  mv42.ID = 'SDICountry' and mv42.status = 2  
                                left join MasterValue mv43 on fc.Negara = mv43.code and  mv43.ID = 'SDICountry' and mv43.status = 2  
                                left join MasterValue mv44 on fc.CountryofEstablishment = mv44.code and  mv44.ID = 'SDICountry' and mv44.status = 2  
                                left join MasterValue mv45 on fc.CountryofCompany = mv45.code and  mv45.ID = 'SDICountry' and mv45.status = 2  
                                left join MasterValue mv46 on fc.CompanyCityName = mv46.Code and mv46.ID = 'CityRHB' and mv46.status = 2  
                                left join MasterValue mv52 on fc.IdentitasIns11 = mv52.Code and mv52.ID = 'Identity' and mv52.status = 2
                                left join MasterValue mv53 on fc.IdentitasIns12 = mv53.Code and mv53.ID = 'Identity' and mv53.status = 2
                                left join MasterValue mv54 on fc.IdentitasIns13 = mv54.Code and mv54.ID = 'Identity' and mv54.status = 2
                                left join MasterValue mv55 on fc.IdentitasIns14 = mv55.Code and mv55.ID = 'Identity' and mv55.status = 2
                                left join MasterValue mv56 on fc.IdentitasIns21 = mv56.Code and mv56.ID = 'Identity' and mv56.status = 2
                                left join MasterValue mv57 on fc.IdentitasIns22 = mv57.Code and mv57.ID = 'Identity' and mv57.status = 2
                                left join MasterValue mv58 on fc.IdentitasIns23 = mv58.Code and mv58.ID = 'Identity' and mv58.status = 2
                                left join MasterValue mv59 on fc.IdentitasIns24 = mv59.Code and mv59.ID = 'Identity' and mv59.status = 2
                                left join MasterValue mv60 on fc.IdentitasIns31 = mv60.Code and mv60.ID = 'Identity' and mv60.status = 2
                                left join MasterValue mv61 on fc.IdentitasIns32 = mv61.Code and mv61.ID = 'Identity' and mv61.status = 2
                                left join MasterValue mv62 on fc.IdentitasIns33 = mv62.Code and mv62.ID = 'Identity' and mv62.status = 2
                                left join MasterValue mv63 on fc.IdentitasIns34 = mv63.Code and mv63.ID = 'Identity' and mv63.status = 2
                                left join MasterValue mv64 on fc.IdentitasIns41 = mv64.Code and mv64.ID = 'Identity' and mv64.status = 2
                                left join MasterValue mv65 on fc.IdentitasIns42 = mv65.Code and mv65.ID = 'Identity' and mv65.status = 2
                                left join MasterValue mv66 on fc.IdentitasIns43 = mv66.Code and mv66.ID = 'Identity' and mv66.status = 2
                                left join MasterValue mv67 on fc.IdentitasIns44 = mv67.Code and mv67.ID = 'Identity' and mv67.status = 2
                                left join MasterValue mv69 on fc.KYCRiskProfile = mv69.code and  mv69.ID = 'KYCRiskProfile' and mv69.status = 2

								--individu 
                                left join MasterValue mv4 on fc.OtherKodeKotaInd1 = mv4.Code and mv4.ID = 'CityRHB' and mv4.status = 2  
                                left join MasterValue mv5 on fc.OtherKodeKotaInd2 = mv5.Code and mv5.ID = 'CityRHB' and mv5.status = 2  
                                left join MasterValue mv6 on fc.OtherKodeKotaInd3 = mv6.Code and mv6.ID = 'CityRHB' and mv6.status = 2 
                                left join MasterValue mv9 on fc.JenisKelamin = mv9.code and  mv9.ID = 'Sex' and mv9.status = 2  
                                left join MasterValue mv10 on fc.StatusPerkawinan = mv10.code and  mv10.ID = 'MaritalStatus' and mv10.status = 2  
                                left join MasterValue mv11 on fc.Pekerjaan = mv11.code and  mv11.ID = 'Occupation' and mv11.status = 2  
                                left join MasterValue mv12 on fc.Pendidikan = mv12.code and  mv12.ID = 'EducationalBackground' and mv12.status = 2  
                                left join MasterValue mv13 on fc.Agama = mv13.code and  mv13.ID = 'Religion' and mv13.status = 2  
                                left join MasterValue mv14 on fc.PenghasilanInd = mv14.code and  mv14.ID = 'IncomeIND' and mv14.status = 2  
                                left join MasterValue mv15 on fc.SumberDanaInd = mv15.code and  mv15.ID = 'IncomeSourceIND' and mv15.status = 2  
                                left join MasterValue mv16 on fc.MaksudTujuanInd = mv16.code and  mv16.ID = 'InvestmentObjectivesIND' and mv16.status = 2   
                                left join MasterValue mv27 on fc.OtherPropinsiInd1 = mv27.code and  mv27.ID = 'SDIProvince' and mv27.status = 2  
                                left join MasterValue mv28 on fc.OtherPropinsiInd2 = mv28.code and  mv28.ID = 'SDIProvince' and mv28.status = 2  
                                left join MasterValue mv29 on fc.OtherPropinsiInd3 = mv29.code and  mv29.ID = 'SDIProvince' and mv29.status = 2 
                                left join MasterValue mv30 on fc.OtherNegaraInd1 = mv30.code and  mv30.ID = 'SDICountry' and mv30.status = 2  
                                left join MasterValue mv31 on fc.OtherNegaraInd2 = mv31.code and  mv31.ID = 'SDICountry' and mv31.status = 2  
                                left join MasterValue mv32 on fc.OtherNegaraInd3 = mv32.code and  mv32.ID = 'SDICountry' and mv32.status = 2  
                                left join MasterValue mv47 on fc.NatureOfBusiness = mv47.Code and mv47.ID = 'HRBusiness' and mv47.status = 2  
                                left join MasterValue mv48 on fc.Politis = mv48.Code and mv48.ID = 'PoliticallyExposed' and mv48.status = 2
                                left join MasterValue mv49 on fc.IdentitasInd1 = mv49.Code and mv49.ID = 'Identity' and mv49.status = 2
                                left join MasterValue mv50 on fc.IdentitasInd2 = mv50.Code and mv50.ID = 'Identity' and mv50.status = 2
                                left join MasterValue mv51 on fc.IdentitasInd3 = mv51.Code and mv51.ID = 'Identity' and mv51.status = 2
                                left join MasterValue mv68 on fc.SpouseOccupation = mv68.code and  mv68.ID = 'Occupation' and mv68.status = 2
								left join MasterValue mv70 on fc.KodeDomisiliPropinsi = mv70.code and  mv70.ID = 'SDIProvince' and mv70.status = 2
								left join MasterValue mv71 on fc.KodeCountryofKantor = mv71.code and  mv71.ID = 'SDICountry' and mv71.status = 2 
								left join MasterValue mv72 on fc.KodeKotaKantorInd = mv72.code and  mv72.ID = 'CityRHB' and mv72.status = 2 
								left join MasterValue mv73 on fc.CountryofDomicile = mv73.code and  mv73.ID = 'SDICountry' and mv73.status = 2
								left join MasterValue mv74 on fc.KodePropinsiKantorInd = mv74.code and  mv74.ID = 'SDIProvince' and mv74.status = 2


                                left join fundclient Z on FC.AfiliatedFromPK = Z.FundclientPK and  Z.status = 2
                                left join Currency Y on FC.MataUang1 =  Y.CurrencyPK and Y.status = 2
                                left join Currency W on FC.MataUang2 =  W.CurrencyPK and W.status = 2
                                left join Currency V on FC.MataUang3 =  V.CurrencyPK and V.status = 2
                                left join Bank BC1 on fc.NamaBank1 = BC1.BankPK and BC1.status = 2   
                                left join Bank BC2 on fc.NamaBank2 = BC2.BankPK and BC2.status = 2   
                                left join Bank BC3 on fc.NamaBank3 = BC3.BankPK and BC3.status = 2   
                                left join Bank BC4 on fc.BankRDNPK = BC4.BankPK and BC4.status = 2
                                where  FC.status in (1,2)  " + _paramDateFrom + @"  order by FC.InvestorType";
                            cmd.CommandTimeout = 0;
                            using (SqlDataReader dr0 = cmd.ExecuteReader())
                            {
                                if (!dr0.HasRows)
                                {
                                    return false;
                                }
                                else
                                {
                                    string filePath = Tools.ReportsPath + "FundClientAll" + "_" + _userID + ".xlsx";
                                    FileInfo excelFile = new FileInfo(filePath);
                                    if (excelFile.Exists)
                                    {
                                        excelFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                        excelFile = new FileInfo(filePath);
                                    }

                                    // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                    using (ExcelPackage package = new ExcelPackage(excelFile))
                                    {
                                        package.Workbook.Properties.Title = "FundClientAll";
                                        package.Workbook.Properties.Author = Tools.DefaultReportAuthor();
                                        package.Workbook.Properties.Comments = Tools.DefaultReportComments();
                                        package.Workbook.Properties.Company = Tools.DefaultReportCompany();
                                        package.Workbook.Properties.SetCustomPropertyValue("Checked by", _userID);
                                        package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", Tools.DefaultReportAssemblyName());

                                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("FundClientAll");


                                        //ATUR DATA GROUPINGNYA DULU
                                        List<FundClientRptByInvestorType> rList = new List<FundClientRptByInvestorType>();
                                        while (dr0.Read())
                                        {
                                            FundClientRptByInvestorType rSingle = new FundClientRptByInvestorType();

                                            rSingle.FundClientID = dr0["FundClientID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["FundClientID"]);
                                            rSingle.InvestorType = dr0["InvestorType"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["InvestorType"]);
                                            rSingle.InternalCategory = dr0["InternalCategory"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["InternalCategory"]);
                                            rSingle.InternalName = dr0["InternalName"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["InternalName"]);
                                            rSingle.SellingAgent = dr0["SellingAgent"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["SellingAgent"]);
                                            rSingle.SID = dr0["SID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["SID"]);
                                            rSingle.IFUACode = dr0["IFUACode"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IFUACode"]);
                                            rSingle.InvestorsRiskProfile = dr0["InvestorsRiskProfile"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["InvestorsRiskProfile"]);
                                            rSingle.DormantDate = dr0["DormantDate"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["DormantDate"]);
                                            rSingle.Affiliated = dr0["Affiliated"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["Affiliated"]);
                                            rSingle.AffiliatedWith = dr0["AffiliatedWith"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["AffiliatedWith"]);
                                            rSingle.Suspended = dr0["Suspended"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["Suspended"]);
                                            rSingle.NPWP = dr0["NPWP"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["NPWP"]);
                                            rSingle.RegistrationNPWP = dr0["RegistrationNPWP"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["RegistrationNPWP"]);
                                            rSingle.Email = dr0["Email"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["Email"]);
                                            rSingle.PhoneNumber = dr0["PhoneNumber"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["PhoneNumber"]);
                                            rSingle.MobilePhone = dr0["MobilePhone"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["MobilePhone"]);
                                            rSingle.BusinessPhone = dr0["BusinessPhone"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["BusinessPhone"]);
                                            rSingle.Fax = dr0["Fax"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["Fax"]);
                                            rSingle.Country = dr0["Country"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["Country"]);
                                            rSingle.Nationality = dr0["Nationality"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["Nationality"]);
                                            rSingle.BankRDN = dr0["BankRDN"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["BankRDN"]);
                                            rSingle.RDNAccountName = dr0["RDNAccountName"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["RDNAccountName"]);
                                            rSingle.RDNAccountNumber = dr0["RDNAccountNumber"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["RDNAccountNumber"]);
                                            rSingle.BankName1 = dr0["BankName1"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["BankName1"]);
                                            rSingle.BankAccountName1 = dr0["BankAccountName1"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["BankAccountName1"]);
                                            rSingle.BankAccountNumber1 = dr0["BankAccountNumber1"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["BankAccountNumber1"]);
                                            rSingle.BankBranchName1 = dr0["BankBranchName1"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["BankBranchName1"]);
                                            rSingle.Currency1 = dr0["Currency1"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["Currency1"]);
                                            rSingle.BankName2 = dr0["BankName2"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["BankName2"]);
                                            rSingle.BankAccountName2 = dr0["BankAccountName2"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["BankAccountName2"]);
                                            rSingle.BankAccountNumber2 = dr0["BankAccountNumber2"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["BankAccountNumber2"]);
                                            rSingle.BankBranchName2 = dr0["BankBranchName2"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["BankBranchName2"]);
                                            rSingle.Currency2 = dr0["Currency2"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["Currency2"]);
                                            rSingle.BankName3 = dr0["BankName3"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["BankName3"]);
                                            rSingle.BankAccountName3 = dr0["BankAccountName3"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["BankAccountName3"]);
                                            rSingle.BankAccountNumber3 = dr0["BankAccountNumber3"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["BankAccountNumber3"]);
                                            rSingle.BankBranchName3 = dr0["BankBranchName3"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["BankBranchName3"]);
                                            rSingle.Currency3 = dr0["Currency3"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["Currency3"]);
                                            rSingle.AssetOwner = dr0["AssetOwner"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["AssetOwner"]);
                                            rSingle.StatementType = dr0["StatementType"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["StatementType"]);
                                            rSingle.FATCAStatus = dr0["FATCAStatus"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["FATCAStatus"]);
                                            rSingle.TIN = dr0["TIN"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["TIN"]);
                                            rSingle.TINIssuanceCountry = dr0["TINIssuanceCountry"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["TINIssuanceCountry"]);
                                            rSingle.GIIN = dr0["GIIN"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["GIIN"]);
                                            rSingle.SubstantialOwnerName = dr0["SubstantialOwnerName"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["SubstantialOwnerName"]);
                                            rSingle.SubstantialOwnerAddress = dr0["SubstantialOwnerAddress"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["SubstantialOwnerAddress"]);
                                            rSingle.SubstantialOwnerTIN = dr0["SubstantialOwnerTIN"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["SubstantialOwnerTIN"]);
                                            rSingle.CompanyName = dr0["CompanyName"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["CompanyName"]);
                                            rSingle.CompanyAddress = dr0["CompanyAddress"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["CompanyAddress"]);
                                            rSingle.CompanyZipCode = dr0["CompanyZipCode"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["CompanyZipCode"]);
                                            rSingle.CompanyCity = dr0["CompanyCity"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["CompanyCity"]);
                                            rSingle.CompanyLegalDomicile = dr0["CompanyLegalDomicile"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["CompanyLegalDomicile"]);
                                            rSingle.EstablishmentDate = dr0["EstablishmentDate"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["EstablishmentDate"]);
                                            rSingle.EstablishmentPlace = dr0["EstablishmentPlace"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["EstablishmentPlace"]);
                                            rSingle.CountryofEstablishment = dr0["CountryofEstablishment"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["CountryofEstablishment"]);
                                            rSingle.SKDNumber = dr0["SKDNumber"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["SKDNumber"]);
                                            rSingle.ExpiredDateSKD = dr0["ExpiredDateSKD"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["ExpiredDateSKD"]);
                                            rSingle.ArticleOfAssociation = dr0["ArticleOfAssociation"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["ArticleOfAssociation"]);
                                            rSingle.SIUPNumber = dr0["SIUPNumber"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["SIUPNumber"]);
                                            rSingle.SIUPExpirationDate = dr0["SIUPExpirationDate"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["SIUPExpirationDate"]);
                                            rSingle.AssetFor1Year = dr0["AssetFor1Year"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["AssetFor1Year"]);
                                            rSingle.AssetFor2Year = dr0["AssetFor2Year"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["AssetFor2Year"]);
                                            rSingle.AssetFor3Year = dr0["AssetFor3Year"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["AssetFor3Year"]);
                                            rSingle.OperatingProfitFor1Year = dr0["OperatingProfitFor1Year"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["OperatingProfitFor1Year"]);
                                            rSingle.OperatingProfitFor2Year = dr0["OperatingProfitFor2Year"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["OperatingProfitFor2Year"]);
                                            rSingle.OperatingProfitFor3Year = dr0["OperatingProfitFor3Year"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["OperatingProfitFor3Year"]);
                                            rSingle.CompanyType = dr0["CompanyType"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["CompanyType"]);
                                            rSingle.CompanyCharacteristic = dr0["CompanyCharacteristic"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["CompanyCharacteristic"]);
                                            rSingle.CompanyIncomePerAnnum = dr0["CompanyIncomePerAnnum"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["CompanyIncomePerAnnum"]);
                                            rSingle.CompanySourceOfFunds = dr0["CompanySourceOfFunds"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["CompanySourceOfFunds"]);
                                            rSingle.CompanyInvestmentObjective = dr0["CompanyInvestmentObjective"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["CompanyInvestmentObjective"]);
                                            rSingle.CountryofCompany = dr0["CountryofCompany"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["CountryofCompany"]);
                                            rSingle.CompanyCityName = dr0["CompanyCityName"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["CompanyCityName"]);
                                            rSingle.Province = dr0["Province"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["Province"]);
                                            rSingle.FirstNameOfficer1 = dr0["FirstNameOfficer1"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["FirstNameOfficer1"]);
                                            rSingle.MiddleNameOfficer1 = dr0["MiddleNameOfficer1"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["MiddleNameOfficer1"]);
                                            rSingle.LastNameOfficer1 = dr0["LastNameOfficer1"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["LastNameOfficer1"]);
                                            rSingle.PositionOfficer1 = dr0["PositionOfficer1"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["PositionOfficer1"]);
                                            rSingle.PhoneNumberOfficer1 = dr0["PhoneNumberOfficer1"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["PhoneNumberOfficer1"]);
                                            rSingle.EmailOfficer1 = dr0["EmailOfficer1"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["EmailOfficer1"]);
                                            rSingle.IDType1Officer1 = dr0["IDType1Officer1"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IDType1Officer1"]);
                                            rSingle.IDNumber1Officer1 = dr0["IDNumber1Officer1"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IDNumber1Officer1"]);
                                            rSingle.IDRegDate1Officer1 = dr0["IDRegDate1Officer1"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IDRegDate1Officer1"]);
                                            rSingle.IDExpireDate1Officer1 = dr0["IDExpireDate1Officer1"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IDExpireDate1Officer1"]);
                                            rSingle.IDType2Officer1 = dr0["IDType2Officer1"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IDType2Officer1"]);
                                            rSingle.IDNumber2Officer1 = dr0["IDNumber2Officer1"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IDNumber2Officer1"]);
                                            rSingle.IDRegDate2Officer1 = dr0["IDRegDate2Officer1"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IDRegDate2Officer1"]);
                                            rSingle.IDExpireDate2Officer1 = dr0["IDExpireDate2Officer1"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IDExpireDate2Officer1"]);
                                            rSingle.IDType3Officer1 = dr0["IDType3Officer1"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IDType3Officer1"]);
                                            rSingle.IDNumber3Officer1 = dr0["IDNumber3Officer1"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IDNumber3Officer1"]);
                                            rSingle.IDRegDate3Officer1 = dr0["IDRegDate3Officer1"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IDRegDate3Officer1"]);
                                            rSingle.IDExpireDate3Officer1 = dr0["IDExpireDate3Officer1"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IDExpireDate3Officer1"]);
                                            rSingle.IDType4Officer1 = dr0["IDType4Officer1"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IDType4Officer1"]);
                                            rSingle.IDNumber4Officer1 = dr0["IDNumber4Officer1"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IDNumber4Officer1"]);
                                            rSingle.IDRegDate4Officer1 = dr0["IDRegDate4Officer1"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IDRegDate4Officer1"]);
                                            rSingle.IDExpireDate4Officer1 = dr0["IDExpireDate4Officer1"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IDExpireDate4Officer1"]);
                                            rSingle.FirstNameOfficer2 = dr0["FirstNameOfficer2"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["FirstNameOfficer2"]);
                                            rSingle.MiddleNameOfficer2 = dr0["MiddleNameOfficer2"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["MiddleNameOfficer2"]);
                                            rSingle.LastNameOfficer2 = dr0["LastNameOfficer2"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["LastNameOfficer2"]);
                                            rSingle.PositionOfficer2 = dr0["PositionOfficer2"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["PositionOfficer2"]);
                                            rSingle.IDType1Officer2 = dr0["IDType1Officer2"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IDType1Officer2"]);
                                            rSingle.IDNumber1Officer2 = dr0["IDNumber1Officer2"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IDNumber1Officer2"]);
                                            rSingle.IDRegDate1Officer2 = dr0["IDRegDate1Officer2"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IDRegDate1Officer2"]);
                                            rSingle.IDExpireDate1Officer2 = dr0["IDExpireDate1Officer2"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IDExpireDate1Officer2"]);
                                            rSingle.IDType2Officer2 = dr0["IDType2Officer2"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IDType2Officer2"]);
                                            rSingle.IDNumber2Officer2 = dr0["IDNumber2Officer2"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IDNumber2Officer2"]);
                                            rSingle.IDRegDate2Officer2 = dr0["IDRegDate2Officer2"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IDRegDate2Officer2"]);
                                            rSingle.IDExpireDate2Officer2 = dr0["IDExpireDate2Officer2"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IDExpireDate2Officer2"]);
                                            rSingle.IDType3Officer2 = dr0["IDType3Officer2"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IDType3Officer2"]);
                                            rSingle.IDNumber3Officer2 = dr0["IDNumber3Officer2"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IDNumber3Officer2"]);
                                            rSingle.IDRegDate3Officer2 = dr0["IDRegDate3Officer2"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IDRegDate3Officer2"]);
                                            rSingle.IDExpireDate3Officer2 = dr0["IDExpireDate3Officer2"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IDExpireDate3Officer2"]);
                                            rSingle.IDType4Officer2 = dr0["IDType4Officer2"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IDType4Officer2"]);
                                            rSingle.IDNumber4Officer2 = dr0["IDNumber4Officer2"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IDNumber4Officer2"]);
                                            rSingle.IDRegDate4Officer2 = dr0["IDRegDate4Officer2"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IDRegDate4Officer2"]);
                                            rSingle.IDExpireDate4Officer2 = dr0["IDExpireDate4Officer2"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IDExpireDate4Officer2"]);
                                            rSingle.FirstNameOfficer3 = dr0["FirstNameOfficer3"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["FirstNameOfficer3"]);
                                            rSingle.MiddleNameOfficer3 = dr0["MiddleNameOfficer3"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["MiddleNameOfficer3"]);
                                            rSingle.LastNameOfficer3 = dr0["LastNameOfficer3"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["LastNameOfficer3"]);
                                            rSingle.PositionOfficer3 = dr0["PositionOfficer3"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["PositionOfficer3"]);
                                            rSingle.IDType1Officer3 = dr0["IDType1Officer3"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IDType1Officer3"]);
                                            rSingle.IDNumber1Officer3 = dr0["IDNumber1Officer3"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IDNumber1Officer3"]);
                                            rSingle.IDRegDate1Officer3 = dr0["IDRegDate1Officer3"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IDRegDate1Officer3"]);
                                            rSingle.IDExpireDate1Officer3 = dr0["IDExpireDate1Officer3"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IDExpireDate1Officer3"]);
                                            rSingle.IDType2Officer3 = dr0["IDType2Officer3"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IDType2Officer3"]);
                                            rSingle.IDNumber2Officer3 = dr0["IDNumber2Officer3"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IDNumber2Officer3"]);
                                            rSingle.IDRegDate2Officer3 = dr0["IDRegDate2Officer3"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IDRegDate2Officer3"]);
                                            rSingle.IDExpireDate2Officer3 = dr0["IDExpireDate2Officer3"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IDExpireDate2Officer3"]);
                                            rSingle.IDType3Officer3 = dr0["IDType3Officer3"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IDType3Officer3"]);
                                            rSingle.IDNumber3Officer3 = dr0["IDNumber3Officer3"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IDNumber3Officer3"]);
                                            rSingle.IDRegDate3Officer3 = dr0["IDRegDate3Officer3"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IDRegDate3Officer3"]);
                                            rSingle.IDExpireDate3Officer3 = dr0["IDExpireDate3Officer3"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IDExpireDate3Officer3"]);
                                            rSingle.IDType4Officer3 = dr0["IDType4Officer3"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IDType4Officer3"]);
                                            rSingle.IDNumber4Officer3 = dr0["IDNumber4Officer3"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IDNumber4Officer3"]);
                                            rSingle.IDRegDate4Officer3 = dr0["IDRegDate4Officer3"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IDRegDate4Officer3"]);
                                            rSingle.IDExpireDate4Officer3 = dr0["IDExpireDate4Officer3"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IDExpireDate4Officer3"]);
                                            rSingle.FirstNameOfficer4 = dr0["FirstNameOfficer4"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["FirstNameOfficer4"]);
                                            rSingle.MiddleNameOfficer4 = dr0["MiddleNameOfficer4"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["MiddleNameOfficer4"]);
                                            rSingle.LastNameOfficer4 = dr0["LastNameOfficer4"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["LastNameOfficer4"]);
                                            rSingle.PositionOfficer4 = dr0["PositionOfficer4"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["PositionOfficer4"]);
                                            rSingle.IDType1Officer4 = dr0["IDType1Officer4"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IDType1Officer4"]);
                                            rSingle.IDNumber1Officer4 = dr0["IDNumber1Officer4"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IDNumber1Officer4"]);
                                            rSingle.IDRegDate1Officer4 = dr0["IDRegDate1Officer4"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IDRegDate1Officer4"]);
                                            rSingle.IDExpireDate1Officer4 = dr0["IDExpireDate1Officer4"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IDExpireDate1Officer4"]);
                                            rSingle.IDType2Officer4 = dr0["IDType2Officer4"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IDType2Officer4"]);
                                            rSingle.IDNumber2Officer4 = dr0["IDNumber2Officer4"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IDNumber2Officer4"]);
                                            rSingle.IDRegDate2Officer4 = dr0["IDRegDate2Officer4"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IDRegDate2Officer4"]);
                                            rSingle.IDExpireDate2Officer4 = dr0["IDExpireDate2Officer4"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IDExpireDate2Officer4"]);
                                            rSingle.IDType3Officer4 = dr0["IDType3Officer4"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IDType3Officer4"]);
                                            rSingle.IDNumber3Officer4 = dr0["IDNumber3Officer4"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IDNumber3Officer4"]);
                                            rSingle.IDRegDate3Officer4 = dr0["IDRegDate3Officer4"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IDRegDate3Officer4"]);
                                            rSingle.IDExpireDate3Officer4 = dr0["IDExpireDate3Officer4"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IDExpireDate3Officer4"]);
                                            rSingle.IDType4Officer4 = dr0["IDType4Officer4"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IDType4Officer4"]);
                                            rSingle.IDNumber4Officer4 = dr0["IDNumber4Officer4"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IDNumber4Officer4"]);
                                            rSingle.IDRegDate4Officer4 = dr0["IDRegDate4Officer4"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IDRegDate4Officer4"]);
                                            rSingle.IDExpireDate4Officer4 = dr0["IDExpireDate4Officer4"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IDExpireDate4Officer4"]);
                                            rSingle.Description = dr0["Description"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["Description"]);
                                            rSingle.CityOfEstablishment = dr0["CityOfEstablishment"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["CityOfEstablishment"]);
                                            rSingle.SellingAgentCode = dr0["SACode"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["SACode"]);
                                            rSingle.CountryofDomicile = dr0["CountryofDomicile"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["CountryofDomicile"]);
                                            rSingle.KYCRiskProfile = dr0["KYCRiskProfile"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["KYCRiskProfile"]);
                                            rSingle.BICCode1 = dr0["BICode1"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["BICode1"]);
                                            rSingle.BICCode2 = dr0["BICode2"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["BICode2"]);
                                            rSingle.BICCode3 = dr0["BICode3"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["BICode3"]);
                                            rSingle.BIMemberCode1 = dr0["BIMemberCode1"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["BIMemberCode1"]);
                                            rSingle.BIMemberCode2 = dr0["BIMemberCode2"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["BIMemberCode2"]);
                                            rSingle.BIMemberCode3 = dr0["BIMemberCode3"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["BIMemberCode3"]);

                                            // individual
                                            rSingle.FirstNameInd = dr0["FirstNameInd"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["FirstNameInd"]);
                                            rSingle.MiddleNameInd = dr0["MiddleNameInd"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["MiddleNameInd"]);
                                            rSingle.LastNameInd = dr0["LastNameInd"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["LastNameInd"]);
                                            rSingle.BirthPlace = dr0["BirthPlace"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["BirthPlace"]);
                                            rSingle.CountryOfBirth = dr0["CountryOfBirth"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["CountryOfBirth"]);
                                            rSingle.DOB = dr0["TanggalLahir"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["TanggalLahir"]);
                                            rSingle.GenderSex = dr0["GenderSex"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["GenderSex"]);
                                            rSingle.MaritalStatus = dr0["MaritalStatus"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["MaritalStatus"]);
                                            rSingle.Occupation = dr0["Occupation"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["Occupation"]);
                                            rSingle.Education = dr0["Education"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["Education"]);
                                            rSingle.Religion = dr0["Religion"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["Religion"]);
                                            rSingle.IncomePerAnnum = dr0["IncomePerAnnum"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IncomePerAnnum"]);
                                            rSingle.SourceOfFunds = dr0["SourceOfFunds"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["SourceOfFunds"]);
                                            rSingle.InvestmentObjectives = dr0["InvestmentObjectives"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["InvestmentObjectives"]);
                                            rSingle.MotherMaidenName = dr0["MotherMaidenName"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["MotherMaidenName"]);
                                            rSingle.SpouseName = dr0["SpouseName"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["SpouseName"]);
                                            rSingle.SpouseOccupation = dr0["SpouseOccupation"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["SpouseOccupation"]);
                                            rSingle.Heir = dr0["Heir"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["Heir"]);
                                            rSingle.HeirRelation = dr0["HeirRelation"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["HeirRelation"]);
                                            rSingle.NatureOfBusiness = dr0["NatureOfBusiness"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["NatureOfBusiness"]);
                                            rSingle.NatureOfBusinessDesc = dr0["NatureOfBusinessDesc"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["NatureOfBusinessDesc"]);
                                            rSingle.PoliticallyExposed = dr0["PoliticallyExposed"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["PoliticallyExposed"]);
                                            rSingle.PoliticallyExposedDesc = dr0["PoliticallyExposedDesc"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["PoliticallyExposedDesc"]);
                                            rSingle.OtherHomePhone = dr0["OtherHomePhone"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["OtherHomePhone"]);
                                            rSingle.OtherCellPhone = dr0["OtherCellPhone"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["OtherCellPhone"]);
                                            rSingle.OtherFax = dr0["OtherFax"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["OtherFax"]);
                                            rSingle.OtherEmail = dr0["OtherEmail"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["OtherEmail"]);
                                            rSingle.CorrespondenceAddress = dr0["CorrespondenceAddress"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["CorrespondenceAddress"]);
                                            rSingle.CorrespondenceCity = dr0["CorrespondenceCity"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["CorrespondenceCity"]);
                                            rSingle.CorrespondenceZipCode = dr0["CorrespondenceZipCode"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["CorrespondenceZipCode"]);
                                            rSingle.CountryofCorrespondence = dr0["CountryofCorrespondence"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["CountryofCorrespondence"]);
                                            rSingle.DomicileAddress = dr0["DomicileAddress"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["DomicileAddress"]);
                                            rSingle.DomicileCity = dr0["DomicileCity"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["DomicileCity"]);
                                            rSingle.DomicileZipCode = dr0["DomicileZipCode"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["DomicileZipCode"]);
                                            rSingle.CountryofDomicile = dr0["CountryofDomicile"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["CountryofDomicile"]);
                                            rSingle.IdentityAddress1 = dr0["IdentityAddress1"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IdentityAddress1"]);
                                            rSingle.IdentityCity1 = dr0["IdentityCity1"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IdentityCity1"]);
                                            rSingle.IdentityZipCode1 = dr0["IdentityZipCode1"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IdentityZipCode1"]);
                                            rSingle.IdentityProvince1 = dr0["IdentityProvince1"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IdentityProvince1"]);
                                            rSingle.Propinsi = dr0["Propinsi"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["Propinsi"]);
                                            rSingle.IdentityCountry1 = dr0["IdentityCountry1"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IdentityCountry1"]);
                                            rSingle.IdentityAddress2 = dr0["IdentityAddress2"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IdentityAddress2"]);
                                            rSingle.IdentityCity2 = dr0["IdentityCity2"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IdentityCity2"]);
                                            rSingle.IdentityZipCode2 = dr0["IdentityZipCode2"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IdentityZipCode2"]);
                                            rSingle.IdentityProvince2 = dr0["IdentityProvince2"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IdentityProvince2"]);
                                            rSingle.IdentityCountry2 = dr0["IdentityCountry2"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IdentityCountry2"]);
                                            rSingle.IdentityAddress3 = dr0["IdentityAddress3"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IdentityAddress3"]);
                                            rSingle.IdentityCity3 = dr0["IdentityCity3"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IdentityCity3"]);
                                            rSingle.IdentityZipCode3 = dr0["IdentityZipCode3"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IdentityZipCode3"]);
                                            rSingle.IdentityProvince3 = dr0["IdentityProvince3"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IdentityProvince3"]);
                                            rSingle.IdentityCountry3 = dr0["IdentityCountry3"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IdentityCountry3"]);
                                            rSingle.IdentityType1 = dr0["IdentityType1"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IdentityType1"]);
                                            rSingle.IdentityNumber1 = dr0["IdentityNumber1"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IdentityNumber1"]);
                                            rSingle.RegistrationDateIdentitasInd1 = dr0["RegistrationDateIdentitasInd1"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["RegistrationDateIdentitasInd1"]);
                                            rSingle.ExpiredDateIdentitasInd1 = dr0["ExpiredDateIdentitasInd1"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["ExpiredDateIdentitasInd1"]);
                                            rSingle.IdentityType2 = dr0["IdentityType2"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IdentityType2"]);
                                            rSingle.IdentityNumber2 = dr0["IdentityNumber2"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IdentityNumber2"]);
                                            rSingle.RegistrationDateIdentitasInd2 = dr0["RegistrationDateIdentitasInd2"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["RegistrationDateIdentitasInd2"]);
                                            rSingle.ExpiredDateIdentitasInd2 = dr0["ExpiredDateIdentitasInd2"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["ExpiredDateIdentitasInd2"]);
                                            rSingle.IdentityType3 = dr0["IdentityType3"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IdentityType3"]);
                                            rSingle.IdentityNumber3 = dr0["IdentityNumber3"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["IdentityNumber3"]);
                                            rSingle.RegistrationDateIdentitasInd3 = dr0["RegistrationDateIdentitasInd3"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["RegistrationDateIdentitasInd3"]);
                                            rSingle.ExpiredDateIdentitasInd3 = dr0["ExpiredDateIdentitasInd3"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["ExpiredDateIdentitasInd3"]);
                                            rSingle.AlamatKantorInd = dr0["AlamatKantorInd"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["AlamatKantorInd"]);
                                            rSingle.KodeKotaKantorInd = dr0["KodeKotaKantorInd"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["KodeKotaKantorInd"]);
                                            rSingle.KodePosKantorInd = dr0["KodePosKantorInd"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["KodePosKantorInd"]);
                                            rSingle.KodePropinsiKantorInd = dr0["KodePropinsiKantorInd"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["KodePropinsiKantorInd"]);
                                            rSingle.KodeCountryofKantor = dr0["KodeCountryofKantor"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["KodeCountryofKantor"]);
                                            rSingle.CorrespondenceRT = dr0["CorrespondenceRT"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["CorrespondenceRT"]);
                                            rSingle.CorrespondenceRW = dr0["CorrespondenceRW"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["CorrespondenceRW"]);
                                            rSingle.DomicileRT = dr0["DomicileRT"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["DomicileRT"]);
                                            rSingle.DomicileRW = dr0["DomicileRW"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["DomicileRW"]);
                                            rSingle.Identity1RT = dr0["Identity1RT"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["Identity1RT"]);
                                            rSingle.Identity1RW = dr0["Identity1RW"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["Identity1RW"]);
                                            rSingle.KodeDomisiliPropinsi = dr0["KodeDomisiliPropinsi"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["KodeDomisiliPropinsi"]);

                                            rSingle.EntryUsersID = dr0["EntryUsersID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["EntryUsersID"]);
                                            rSingle.EntryTime = dr0["EntryTime"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["EntryTime"]);
                                            rSingle.UpdateUsersID = dr0["UpdateUsersID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["UpdateUsersID"]);
                                            rSingle.UpdateTime = dr0["UpdateTime"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["UpdateTime"]);
                                            rSingle.ApprovedUsersID = dr0["ApprovedUsersID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["ApprovedUsersID"]);
                                            rSingle.ApprovedTime = dr0["ApprovedTime"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["ApprovedTime"]);
                                            rSingle.VoidUsersID = dr0["VoidUsersID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["VoidUsersID"]);
                                            rSingle.VoidTime = dr0["VoidTime"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["VoidTime"]);
                                            rSingle.SuspendBy = dr0["SuspendBy"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["SuspendBy"]);
                                            rSingle.SuspendTime = dr0["SuspendTime"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["SuspendTime"]);
                                            rSingle.UnSuspendBy = dr0["UnSuspendBy"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["UnSuspendBy"]);
                                            rSingle.UnSuspendTime = dr0["UnSuspendTime"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr0["UnSuspendTime"]);
                                            rList.Add(rSingle);

                                        }

                                        var GroupByTitle =
                                            from r in rList
                                            group r by new { r.InvestorType } into rGroup
                                            select rGroup;

                                        int incRowExcel = 0;

                                        foreach (var rsHeader in GroupByTitle)
                                        {

                                            incRowExcel++;
                                            string _range = "";
                                            worksheet.Cells[incRowExcel, 1].Value = "Investor Type :";
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 2].Value = rsHeader.Key.InvestorType;
                                            worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                            if (rsHeader.Key.InvestorType == "INDIVIDUAL")
                                            {
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = "FundClientID";
                                                worksheet.Cells[incRowExcel, 2].Value = "OldID";
                                                worksheet.Cells[incRowExcel, 3].Value = "InvestorType";
                                                worksheet.Cells[incRowExcel, 4].Value = "InternalCategory";
                                                worksheet.Cells[incRowExcel, 5].Value = "InternalName";
                                                worksheet.Cells[incRowExcel, 6].Value = "SellingAgent";
                                                worksheet.Cells[incRowExcel, 7].Value = "SID";
                                                worksheet.Cells[incRowExcel, 8].Value = "IFUACode";
                                                worksheet.Cells[incRowExcel, 9].Value = "InvestorsRiskProfile";
                                                worksheet.Cells[incRowExcel, 10].Value = "KYCRiskProfile";
                                                worksheet.Cells[incRowExcel, 11].Value = "AssetOwner";
                                                worksheet.Cells[incRowExcel, 12].Value = "StatementType";
                                                worksheet.Cells[incRowExcel, 13].Value = "DormantDate";
                                                worksheet.Cells[incRowExcel, 14].Value = "Affiliated";
                                                worksheet.Cells[incRowExcel, 15].Value = "AffiliatedWith";
                                                worksheet.Cells[incRowExcel, 16].Value = "Suspended";
                                                worksheet.Cells[incRowExcel, 17].Value = "NPWP";
                                                worksheet.Cells[incRowExcel, 18].Value = "RegistrationNPWP";
                                                worksheet.Cells[incRowExcel, 19].Value = "Email";
                                                worksheet.Cells[incRowExcel, 20].Value = "PhoneNumber";
                                                worksheet.Cells[incRowExcel, 21].Value = "MobilePhone";
                                                worksheet.Cells[incRowExcel, 22].Value = "Fax";
                                                worksheet.Cells[incRowExcel, 23].Value = "Country";
                                                worksheet.Cells[incRowExcel, 24].Value = "Nationality";
                                                worksheet.Cells[incRowExcel, 25].Value = "BankRDN";
                                                worksheet.Cells[incRowExcel, 26].Value = "RDNAccountName";
                                                worksheet.Cells[incRowExcel, 27].Value = "RDNAccountNumber";
                                                worksheet.Cells[incRowExcel, 28].Value = "BankName1";
                                                worksheet.Cells[incRowExcel, 29].Value = "BankAccountName1";
                                                worksheet.Cells[incRowExcel, 30].Value = "BankAccountNumber1";
                                                worksheet.Cells[incRowExcel, 31].Value = "BankBranchName1";
                                                worksheet.Cells[incRowExcel, 32].Value = "Currency1";
                                                worksheet.Cells[incRowExcel, 33].Value = "BICCode1";
                                                worksheet.Cells[incRowExcel, 34].Value = "BIMemberCode1";

                                                worksheet.Cells[incRowExcel, 35].Value = "BankName2";
                                                worksheet.Cells[incRowExcel, 36].Value = "BankAccountName2";
                                                worksheet.Cells[incRowExcel, 37].Value = "BankAccountNumber2";
                                                worksheet.Cells[incRowExcel, 38].Value = "BankBranchName2";
                                                worksheet.Cells[incRowExcel, 39].Value = "Currency2";
                                                worksheet.Cells[incRowExcel, 40].Value = "BICCode2";
                                                worksheet.Cells[incRowExcel, 41].Value = "BIMemberCode2";

                                                worksheet.Cells[incRowExcel, 42].Value = "BankName3";
                                                worksheet.Cells[incRowExcel, 43].Value = "BankAccountName3";
                                                worksheet.Cells[incRowExcel, 44].Value = "BankAccountNumber3";
                                                worksheet.Cells[incRowExcel, 45].Value = "BankBranchName3";
                                                worksheet.Cells[incRowExcel, 46].Value = "Currency3";
                                                worksheet.Cells[incRowExcel, 47].Value = "BICCode3";
                                                worksheet.Cells[incRowExcel, 48].Value = "BIMemberCode3";

                                                worksheet.Cells[incRowExcel, 49].Value = "FirstNameInd";
                                                worksheet.Cells[incRowExcel, 50].Value = "MiddleNameInd";
                                                worksheet.Cells[incRowExcel, 51].Value = "LastNameInd";
                                                worksheet.Cells[incRowExcel, 52].Value = "BirthPlace";
                                                worksheet.Cells[incRowExcel, 53].Value = "CountryOfBirth";
                                                worksheet.Cells[incRowExcel, 54].Value = "DOB";
                                                worksheet.Cells[incRowExcel, 55].Value = "GenderSex";
                                                worksheet.Cells[incRowExcel, 56].Value = "MaritalStatus";
                                                worksheet.Cells[incRowExcel, 57].Value = "Occupation";
                                                worksheet.Cells[incRowExcel, 58].Value = "Education";
                                                worksheet.Cells[incRowExcel, 59].Value = "Religion";
                                                worksheet.Cells[incRowExcel, 60].Value = "IncomePerAnnum";
                                                worksheet.Cells[incRowExcel, 61].Value = "SourceOfFunds";
                                                worksheet.Cells[incRowExcel, 62].Value = "InvestmentObjectives";
                                                worksheet.Cells[incRowExcel, 63].Value = "MotherMaidenName";
                                                worksheet.Cells[incRowExcel, 64].Value = "SpouseName";
                                                worksheet.Cells[incRowExcel, 65].Value = "SpouseOccupation";
                                                worksheet.Cells[incRowExcel, 66].Value = "Heir";
                                                worksheet.Cells[incRowExcel, 67].Value = "HeirRelation";
                                                worksheet.Cells[incRowExcel, 68].Value = "NatureOfBusiness";
                                                worksheet.Cells[incRowExcel, 69].Value = "NatureOfBusinessDesc";
                                                worksheet.Cells[incRowExcel, 70].Value = "PoliticallyExposed";
                                                worksheet.Cells[incRowExcel, 71].Value = "PoliticallyExposedDesc";
                                                worksheet.Cells[incRowExcel, 72].Value = "OtherHomePhone";
                                                worksheet.Cells[incRowExcel, 73].Value = "OtherCellPhone";
                                                worksheet.Cells[incRowExcel, 74].Value = "OtherFax";
                                                worksheet.Cells[incRowExcel, 75].Value = "OtherEmail";
                                                worksheet.Cells[incRowExcel, 76].Value = "CorrespondenceAddress";
                                                worksheet.Cells[incRowExcel, 77].Value = "CorrespondenceCity";
                                                worksheet.Cells[incRowExcel, 78].Value = "CorrespondenceZipCode";
                                                worksheet.Cells[incRowExcel, 79].Value = "CorrespondenceProvince";
                                                worksheet.Cells[incRowExcel, 80].Value = "CorrespondenceRT";
                                                worksheet.Cells[incRowExcel, 81].Value = "CorrespondenceRW";
                                                worksheet.Cells[incRowExcel, 82].Value = "CountryofCorrespondence";
                                                worksheet.Cells[incRowExcel, 83].Value = "DomicileAddress";
                                                worksheet.Cells[incRowExcel, 84].Value = "KodeDomisiliPropinsi";
                                                worksheet.Cells[incRowExcel, 85].Value = "DomicileCity";
                                                worksheet.Cells[incRowExcel, 86].Value = "DomicileZipCode";
                                                worksheet.Cells[incRowExcel, 87].Value = "DomicileRT";
                                                worksheet.Cells[incRowExcel, 88].Value = "DomicileRW";
                                                worksheet.Cells[incRowExcel, 89].Value = "CountryofDomicile";
                                                worksheet.Cells[incRowExcel, 90].Value = "IdentityAddress1";
                                                worksheet.Cells[incRowExcel, 91].Value = "IdentityCity1";
                                                worksheet.Cells[incRowExcel, 92].Value = "IdentityZipCode1";
                                                worksheet.Cells[incRowExcel, 93].Value = "IdentityProvince1";
                                                worksheet.Cells[incRowExcel, 94].Value = "IdentityCountry1";
                                                worksheet.Cells[incRowExcel, 95].Value = "IdentityAddress2";
                                                worksheet.Cells[incRowExcel, 96].Value = "IdentityCity2";
                                                worksheet.Cells[incRowExcel, 97].Value = "IdentityZipCode2";
                                                worksheet.Cells[incRowExcel, 98].Value = "IdentityProvince2";
                                                worksheet.Cells[incRowExcel, 99].Value = "IdentityCountry2";
                                                worksheet.Cells[incRowExcel, 100].Value = "IdentityAddress3";
                                                worksheet.Cells[incRowExcel, 101].Value = "IdentityCity3";
                                                worksheet.Cells[incRowExcel, 102].Value = "IdentityZipCode3";
                                                worksheet.Cells[incRowExcel, 103].Value = "IdentityProvince3";
                                                worksheet.Cells[incRowExcel, 104].Value = "IdentityCountry3";
                                                worksheet.Cells[incRowExcel, 105].Value = "IdentityType1";
                                                worksheet.Cells[incRowExcel, 106].Value = "IdentityNumber1";
                                                worksheet.Cells[incRowExcel, 107].Value = "Identity1RT";
                                                worksheet.Cells[incRowExcel, 108].Value = "Identity1RW";
                                                worksheet.Cells[incRowExcel, 109].Value = "RegistrationDateIdentitasInd1";
                                                worksheet.Cells[incRowExcel, 110].Value = "ExpiredDateIdentitasInd1";
                                                worksheet.Cells[incRowExcel, 111].Value = "IdentityType2";
                                                worksheet.Cells[incRowExcel, 112].Value = "IdentityNumber2";
                                                worksheet.Cells[incRowExcel, 113].Value = "RegistrationDateIdentitasInd2";
                                                worksheet.Cells[incRowExcel, 114].Value = "ExpiredDateIdentitasInd2";
                                                worksheet.Cells[incRowExcel, 115].Value = "IdentityType3";
                                                worksheet.Cells[incRowExcel, 116].Value = "IdentityNumber3";
                                                worksheet.Cells[incRowExcel, 117].Value = "RegistrationDateIdentitasInd3";
                                                worksheet.Cells[incRowExcel, 118].Value = "ExpiredDateIdentitasInd3";
                                                worksheet.Cells[incRowExcel, 119].Value = "FATCAStatus";
                                                worksheet.Cells[incRowExcel, 120].Value = "TIN";
                                                worksheet.Cells[incRowExcel, 121].Value = "TINIssuanceCountry";
                                                worksheet.Cells[incRowExcel, 122].Value = "GIIN";
                                                worksheet.Cells[incRowExcel, 123].Value = "SubstantialOwnerName";
                                                worksheet.Cells[incRowExcel, 124].Value = "SubstantialOwnerAddress";
                                                worksheet.Cells[incRowExcel, 125].Value = "SubstantialOwnerTIN";
                                                worksheet.Cells[incRowExcel, 126].Value = "AlamatKantorInd";
                                                worksheet.Cells[incRowExcel, 127].Value = "KodeKotaKantorInd";
                                                worksheet.Cells[incRowExcel, 128].Value = "KodePosKantorInd";
                                                worksheet.Cells[incRowExcel, 129].Value = "KodePropinsiKantorInd";
                                                worksheet.Cells[incRowExcel, 130].Value = "KodeCountryofKantor";
                                                worksheet.Cells[incRowExcel, 131].Value = "Description";


                                                worksheet.Cells[incRowExcel, 132].Value = "EntryUsersID";
                                                worksheet.Cells[incRowExcel, 133].Value = "EntryTime";
                                                worksheet.Cells[incRowExcel, 134].Value = "UpdateUsersID";
                                                worksheet.Cells[incRowExcel, 135].Value = "UpdateTime";
                                                worksheet.Cells[incRowExcel, 136].Value = "ApprovedUsersID";
                                                worksheet.Cells[incRowExcel, 137].Value = "ApprovedTime";
                                                worksheet.Cells[incRowExcel, 138].Value = "VoidUsersID";
                                                worksheet.Cells[incRowExcel, 139].Value = "VoidTime";
                                                worksheet.Cells[incRowExcel, 140].Value = "SuspendBy";
                                                worksheet.Cells[incRowExcel, 141].Value = "SuspendTime";
                                                worksheet.Cells[incRowExcel, 142].Value = "UnSuspendBy";
                                                worksheet.Cells[incRowExcel, 143].Value = "UnSuspendTime";
                                                _range = "A" + incRowExcel + ":EM" + incRowExcel;
                                            }
                                            else
                                            {
                                                incRowExcel++;
                                                worksheet.Cells[incRowExcel, 1].Value = "FundClientID";
                                                worksheet.Cells[incRowExcel, 2].Value = "InvestorType";
                                                worksheet.Cells[incRowExcel, 3].Value = "InternalCategory";
                                                worksheet.Cells[incRowExcel, 4].Value = "InternalName";
                                                worksheet.Cells[incRowExcel, 5].Value = "SellingAgent";
                                                worksheet.Cells[incRowExcel, 6].Value = "NPWP";
                                                worksheet.Cells[incRowExcel, 7].Value = "RegistrationNPWP";
                                                worksheet.Cells[incRowExcel, 8].Value = "Email";
                                                worksheet.Cells[incRowExcel, 9].Value = "PhoneNumber";
                                                worksheet.Cells[incRowExcel, 10].Value = "MobilePhone";
                                                worksheet.Cells[incRowExcel, 11].Value = "Fax";
                                                worksheet.Cells[incRowExcel, 12].Value = "SID";
                                                worksheet.Cells[incRowExcel, 13].Value = "IFUACode";
                                                worksheet.Cells[incRowExcel, 14].Value = "InvestorsRiskProfile";
                                                worksheet.Cells[incRowExcel, 15].Value = "KYCRiskProfile";
                                                worksheet.Cells[incRowExcel, 16].Value = "AssetOwner";
                                                worksheet.Cells[incRowExcel, 17].Value = "StatementType";
                                                worksheet.Cells[incRowExcel, 18].Value = "SellingAgentCode";
                                                worksheet.Cells[incRowExcel, 19].Value = "DormantDate";
                                                worksheet.Cells[incRowExcel, 20].Value = "Description";
                                                worksheet.Cells[incRowExcel, 21].Value = "Affiliated";
                                                worksheet.Cells[incRowExcel, 22].Value = "AffiliatedWith";
                                                worksheet.Cells[incRowExcel, 23].Value = "Suspended";
                                                worksheet.Cells[incRowExcel, 24].Value = "BankName1";
                                                worksheet.Cells[incRowExcel, 25].Value = "BankAccountName1";
                                                worksheet.Cells[incRowExcel, 26].Value = "BankAccountNumber1";
                                                worksheet.Cells[incRowExcel, 27].Value = "BankBranchName1";
                                                worksheet.Cells[incRowExcel, 28].Value = "Currency1";
                                                worksheet.Cells[incRowExcel, 29].Value = "BICCode1";
                                                worksheet.Cells[incRowExcel, 30].Value = "BIMemberCode1";
                                                worksheet.Cells[incRowExcel, 31].Value = "BankName2";
                                                worksheet.Cells[incRowExcel, 32].Value = "BankAccountName2";
                                                worksheet.Cells[incRowExcel, 33].Value = "BankAccountNumber2";
                                                worksheet.Cells[incRowExcel, 34].Value = "BankBranchName2";
                                                worksheet.Cells[incRowExcel, 35].Value = "Currency2";
                                                worksheet.Cells[incRowExcel, 36].Value = "BICCode2";
                                                worksheet.Cells[incRowExcel, 37].Value = "BIMemberCode2";
                                                worksheet.Cells[incRowExcel, 38].Value = "BankName3";
                                                worksheet.Cells[incRowExcel, 39].Value = "BankAccountName3";
                                                worksheet.Cells[incRowExcel, 40].Value = "BankAccountNumber3";
                                                worksheet.Cells[incRowExcel, 41].Value = "BankBranchName3";
                                                worksheet.Cells[incRowExcel, 42].Value = "Currency3";
                                                worksheet.Cells[incRowExcel, 43].Value = "BICCode3";
                                                worksheet.Cells[incRowExcel, 44].Value = "BIMemberCode3";
                                                worksheet.Cells[incRowExcel, 45].Value = "BankRDN";
                                                worksheet.Cells[incRowExcel, 46].Value = "RDNAccountName";
                                                worksheet.Cells[incRowExcel, 47].Value = "RDNAccountNumber";
                                                worksheet.Cells[incRowExcel, 48].Value = "CompanyName";
                                                worksheet.Cells[incRowExcel, 49].Value = "CompanyAddress";
                                                worksheet.Cells[incRowExcel, 50].Value = "CompanyZipCode";
                                                worksheet.Cells[incRowExcel, 51].Value = "CompanyCity";
                                                worksheet.Cells[incRowExcel, 52].Value = "CompanyLegalDomicile";
                                                worksheet.Cells[incRowExcel, 53].Value = "CountryOfDomicile";
                                                worksheet.Cells[incRowExcel, 54].Value = "CompanyType";
                                                worksheet.Cells[incRowExcel, 55].Value = "CompanyCharacteristic";
                                                worksheet.Cells[incRowExcel, 56].Value = "CompanyIncomePerAnnum";
                                                worksheet.Cells[incRowExcel, 57].Value = "CompanySourceOfFunds";
                                                worksheet.Cells[incRowExcel, 58].Value = "CompanyInvestmentObjective";
                                                worksheet.Cells[incRowExcel, 59].Value = "SKDNumber";
                                                worksheet.Cells[incRowExcel, 60].Value = "ExpiredDateSKD";
                                                worksheet.Cells[incRowExcel, 61].Value = "ArticleOfAssociation";
                                                worksheet.Cells[incRowExcel, 62].Value = "SIUPNumber";
                                                worksheet.Cells[incRowExcel, 63].Value = "SIUPExpirationDate";
                                                worksheet.Cells[incRowExcel, 64].Value = "FirstNameOfficer1";
                                                worksheet.Cells[incRowExcel, 65].Value = "MiddleNameOfficer1";
                                                worksheet.Cells[incRowExcel, 66].Value = "LastNameOfficer1";
                                                worksheet.Cells[incRowExcel, 67].Value = "PositionOfficer1";
                                                worksheet.Cells[incRowExcel, 68].Value = "PhoneNumberOfficer1";
                                                worksheet.Cells[incRowExcel, 69].Value = "EmailOfficer1";
                                                worksheet.Cells[incRowExcel, 70].Value = "IDType1Officer1";
                                                worksheet.Cells[incRowExcel, 71].Value = "IDNumber1Officer1";
                                                worksheet.Cells[incRowExcel, 72].Value = "IDRegDate1Officer1";
                                                worksheet.Cells[incRowExcel, 73].Value = "IDExpireDate1Officer1";
                                                worksheet.Cells[incRowExcel, 74].Value = "IDType2Officer1";
                                                worksheet.Cells[incRowExcel, 75].Value = "IDNumber2Officer1";
                                                worksheet.Cells[incRowExcel, 76].Value = "IDRegDate2Officer1";
                                                worksheet.Cells[incRowExcel, 77].Value = "IDExpireDate2Officer1";
                                                worksheet.Cells[incRowExcel, 78].Value = "IDType3Officer1";
                                                worksheet.Cells[incRowExcel, 79].Value = "IDNumber3Officer1";
                                                worksheet.Cells[incRowExcel, 80].Value = "IDRegDate3Officer1";
                                                worksheet.Cells[incRowExcel, 81].Value = "IDExpireDate3Officer1";
                                                worksheet.Cells[incRowExcel, 82].Value = "IDType4Officer1";
                                                worksheet.Cells[incRowExcel, 83].Value = "IDNumber4Officer1";
                                                worksheet.Cells[incRowExcel, 84].Value = "IDRegDate4Officer1";
                                                worksheet.Cells[incRowExcel, 85].Value = "IDExpireDate4Officer1";
                                                worksheet.Cells[incRowExcel, 86].Value = "FirstNameOfficer2";
                                                worksheet.Cells[incRowExcel, 87].Value = "MiddleNameOfficer2";
                                                worksheet.Cells[incRowExcel, 88].Value = "LastNameOfficer2";
                                                worksheet.Cells[incRowExcel, 89].Value = "PositionOfficer2";
                                                worksheet.Cells[incRowExcel, 90].Value = "IDType1Officer2";
                                                worksheet.Cells[incRowExcel, 91].Value = "IDNumber1Officer2";
                                                worksheet.Cells[incRowExcel, 92].Value = "IDRegDate1Officer2";
                                                worksheet.Cells[incRowExcel, 93].Value = "IDExpireDate1Officer2";
                                                worksheet.Cells[incRowExcel, 94].Value = "IDType2Officer2";
                                                worksheet.Cells[incRowExcel, 95].Value = "IDNumber2Officer2";
                                                worksheet.Cells[incRowExcel, 96].Value = "IDRegDate2Officer2";
                                                worksheet.Cells[incRowExcel, 97].Value = "IDExpireDate2Officer2";
                                                worksheet.Cells[incRowExcel, 98].Value = "IDType3Officer2";
                                                worksheet.Cells[incRowExcel, 99].Value = "IDNumber3Officer2";
                                                worksheet.Cells[incRowExcel, 100].Value = "IDRegDate3Officer2";
                                                worksheet.Cells[incRowExcel, 101].Value = "IDExpireDate3Officer2";
                                                worksheet.Cells[incRowExcel, 102].Value = "IDType4Officer2";
                                                worksheet.Cells[incRowExcel, 103].Value = "IDNumber4Officer2";
                                                worksheet.Cells[incRowExcel, 104].Value = "IDRegDate4Officer2";
                                                worksheet.Cells[incRowExcel, 105].Value = "IDExpireDate4Officer2";
                                                worksheet.Cells[incRowExcel, 106].Value = "EstablishmentDate";
                                                worksheet.Cells[incRowExcel, 107].Value = "EstablishmentPlace";
                                                worksheet.Cells[incRowExcel, 108].Value = "CountryofEstablishment";
                                                worksheet.Cells[incRowExcel, 109].Value = "CityOfEstablishment";
                                                worksheet.Cells[incRowExcel, 110].Value = "CountryofCompany";
                                                worksheet.Cells[incRowExcel, 111].Value = "CompanyCityName";
                                                worksheet.Cells[incRowExcel, 112].Value = "CompanyAddress";
                                                worksheet.Cells[incRowExcel, 113].Value = "ZIPCode";
                                                worksheet.Cells[incRowExcel, 114].Value = "BusinessPhone";
                                                worksheet.Cells[incRowExcel, 115].Value = "AssetFor1Year";
                                                worksheet.Cells[incRowExcel, 116].Value = "AssetFor2Year";
                                                worksheet.Cells[incRowExcel, 117].Value = "AssetFor3Year";
                                                worksheet.Cells[incRowExcel, 118].Value = "OperatingProfitFor1Year";
                                                worksheet.Cells[incRowExcel, 119].Value = "OperatingProfitFor2Year";
                                                worksheet.Cells[incRowExcel, 120].Value = "OperatingProfitFor3Year";
                                                worksheet.Cells[incRowExcel, 121].Value = "FirstNameOfficer3";
                                                worksheet.Cells[incRowExcel, 122].Value = "MiddleNameOfficer3";
                                                worksheet.Cells[incRowExcel, 123].Value = "LastNameOfficer3";
                                                worksheet.Cells[incRowExcel, 124].Value = "PositionOfficer3";
                                                worksheet.Cells[incRowExcel, 125].Value = "IDType1Officer3";
                                                worksheet.Cells[incRowExcel, 126].Value = "IDNumber1Officer3";
                                                worksheet.Cells[incRowExcel, 127].Value = "IDRegDate1Officer3";
                                                worksheet.Cells[incRowExcel, 128].Value = "IDExpireDate1Officer3";
                                                worksheet.Cells[incRowExcel, 129].Value = "IDType2Officer3";
                                                worksheet.Cells[incRowExcel, 130].Value = "IDNumber2Officer3";
                                                worksheet.Cells[incRowExcel, 131].Value = "IDRegDate2Officer3";
                                                worksheet.Cells[incRowExcel, 132].Value = "IDExpireDate2Officer3";
                                                worksheet.Cells[incRowExcel, 133].Value = "IDType3Officer3";
                                                worksheet.Cells[incRowExcel, 134].Value = "IDNumber3Officer3";
                                                worksheet.Cells[incRowExcel, 135].Value = "IDRegDate3Officer3";
                                                worksheet.Cells[incRowExcel, 136].Value = "IDExpireDate3Officer3";
                                                worksheet.Cells[incRowExcel, 137].Value = "IDType4Officer3";
                                                worksheet.Cells[incRowExcel, 138].Value = "IDNumber4Officer3";
                                                worksheet.Cells[incRowExcel, 139].Value = "IDRegDate4Officer3";
                                                worksheet.Cells[incRowExcel, 140].Value = "IDExpireDate4Officer3";
                                                worksheet.Cells[incRowExcel, 141].Value = "FirstNameOfficer4";
                                                worksheet.Cells[incRowExcel, 142].Value = "MiddleNameOfficer4";
                                                worksheet.Cells[incRowExcel, 143].Value = "LastNameOfficer4";
                                                worksheet.Cells[incRowExcel, 144].Value = "PositionOfficer4";
                                                worksheet.Cells[incRowExcel, 145].Value = "IDType1Officer4";
                                                worksheet.Cells[incRowExcel, 146].Value = "IDNumber1Officer4";
                                                worksheet.Cells[incRowExcel, 147].Value = "IDRegDate1Officer4";
                                                worksheet.Cells[incRowExcel, 148].Value = "IDExpireDate1Officer4";
                                                worksheet.Cells[incRowExcel, 149].Value = "IDType2Officer4";
                                                worksheet.Cells[incRowExcel, 150].Value = "IDNumber2Officer4";
                                                worksheet.Cells[incRowExcel, 151].Value = "IDRegDate2Officer4";
                                                worksheet.Cells[incRowExcel, 152].Value = "IDExpireDate2Officer4";
                                                worksheet.Cells[incRowExcel, 153].Value = "IDType3Officer4";
                                                worksheet.Cells[incRowExcel, 154].Value = "IDNumber3Officer4";
                                                worksheet.Cells[incRowExcel, 155].Value = "IDRegDate3Officer4";
                                                worksheet.Cells[incRowExcel, 156].Value = "IDExpireDate3Officer4";
                                                worksheet.Cells[incRowExcel, 157].Value = "IDType4Officer4";
                                                worksheet.Cells[incRowExcel, 158].Value = "IDNumber4Officer4";
                                                worksheet.Cells[incRowExcel, 159].Value = "IDRegDate4Officer4";
                                                worksheet.Cells[incRowExcel, 160].Value = "IDExpireDate4Officer4";
                                                worksheet.Cells[incRowExcel, 161].Value = "FATCAStatus";
                                                worksheet.Cells[incRowExcel, 162].Value = "TIN";
                                                worksheet.Cells[incRowExcel, 163].Value = "TINIssuanceCountry";
                                                worksheet.Cells[incRowExcel, 164].Value = "GIIN";
                                                worksheet.Cells[incRowExcel, 165].Value = "SubstantialOwnerName";
                                                worksheet.Cells[incRowExcel, 166].Value = "SubstantialOwnerAddress";
                                                worksheet.Cells[incRowExcel, 167].Value = "SubstantialOwnerTIN";
                                                worksheet.Cells[incRowExcel, 168].Value = "EntryUsersID";
                                                worksheet.Cells[incRowExcel, 169].Value = "EntryTime";
                                                worksheet.Cells[incRowExcel, 170].Value = "UpdateUsersID";
                                                worksheet.Cells[incRowExcel, 171].Value = "UpdateTime";
                                                worksheet.Cells[incRowExcel, 172].Value = "ApprovedUsersID";
                                                worksheet.Cells[incRowExcel, 173].Value = "ApprovedTime";
                                                worksheet.Cells[incRowExcel, 174].Value = "VoidUsersID";
                                                worksheet.Cells[incRowExcel, 175].Value = "VoidTime";
                                                worksheet.Cells[incRowExcel, 176].Value = "SuspendBy";
                                                worksheet.Cells[incRowExcel, 177].Value = "SuspendTime";
                                                worksheet.Cells[incRowExcel, 178].Value = "UnSuspendBy";
                                                worksheet.Cells[incRowExcel, 179].Value = "UnSuspendTime";
                                                _range = "A" + incRowExcel + ":FW" + incRowExcel;
                                            }

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
                                                r.Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                r.Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                            }
                                            incRowExcel++;


                                            int _startRowDetail = incRowExcel;
                                            int _endRowDetail = 0;
                                            //end area header
                                            foreach (var rsDetail in rsHeader)
                                            {
                                                if (rsDetail.InvestorType == "INDIVIDUAL")
                                                {
                                                    worksheet.Cells[incRowExcel, 1].Value = rsDetail.FundClientID;
                                                    worksheet.Cells[incRowExcel, 2].Value = rsDetail.OldID;
                                                    worksheet.Cells[incRowExcel, 3].Value = rsDetail.InvestorType;
                                                    worksheet.Cells[incRowExcel, 4].Value = rsDetail.InternalCategory;
                                                    worksheet.Cells[incRowExcel, 5].Value = rsDetail.InternalName;
                                                    worksheet.Cells[incRowExcel, 6].Value = rsDetail.SellingAgent;
                                                    worksheet.Cells[incRowExcel, 7].Value = rsDetail.SID;
                                                    worksheet.Cells[incRowExcel, 8].Value = rsDetail.IFUACode;
                                                    worksheet.Cells[incRowExcel, 9].Value = rsDetail.InvestorsRiskProfile;
                                                    worksheet.Cells[incRowExcel, 10].Value = rsDetail.KYCRiskProfile;
                                                    worksheet.Cells[incRowExcel, 11].Value = rsDetail.AssetOwner;
                                                    worksheet.Cells[incRowExcel, 12].Value = rsDetail.StatementType;
                                                    worksheet.Cells[incRowExcel, 13].Value = rsDetail.DormantDate;
                                                    worksheet.Cells[incRowExcel, 14].Value = rsDetail.Affiliated;
                                                    worksheet.Cells[incRowExcel, 15].Value = rsDetail.AffiliatedWith;
                                                    worksheet.Cells[incRowExcel, 16].Value = rsDetail.Suspended;
                                                    worksheet.Cells[incRowExcel, 17].Value = Convert.ToDateTime(rsDetail.NPWP).ToString("dd/MM/yyyy");
                                                    worksheet.Cells[incRowExcel, 18].Value = rsDetail.RegistrationNPWP;
                                                    worksheet.Cells[incRowExcel, 19].Value = rsDetail.Email;
                                                    worksheet.Cells[incRowExcel, 20].Value = rsDetail.PhoneNumber;
                                                    worksheet.Cells[incRowExcel, 21].Value = rsDetail.MobilePhone;
                                                    worksheet.Cells[incRowExcel, 22].Value = rsDetail.Fax;
                                                    worksheet.Cells[incRowExcel, 23].Value = rsDetail.Country;
                                                    worksheet.Cells[incRowExcel, 24].Value = rsDetail.Nationality;
                                                    worksheet.Cells[incRowExcel, 25].Value = rsDetail.BankRDN;
                                                    worksheet.Cells[incRowExcel, 26].Value = rsDetail.RDNAccountName;
                                                    worksheet.Cells[incRowExcel, 27].Value = rsDetail.RDNAccountNumber;
                                                    worksheet.Cells[incRowExcel, 28].Value = rsDetail.BankName1;
                                                    worksheet.Cells[incRowExcel, 29].Value = rsDetail.BankAccountName1;
                                                    worksheet.Cells[incRowExcel, 30].Value = rsDetail.BankAccountNumber1;
                                                    worksheet.Cells[incRowExcel, 31].Value = rsDetail.BankBranchName1;
                                                    worksheet.Cells[incRowExcel, 32].Value = rsDetail.Currency1;
                                                    worksheet.Cells[incRowExcel, 33].Value = rsDetail.BICCode1;
                                                    worksheet.Cells[incRowExcel, 34].Value = rsDetail.BIMemberCode1;

                                                    worksheet.Cells[incRowExcel, 35].Value = rsDetail.BankName2;
                                                    worksheet.Cells[incRowExcel, 36].Value = rsDetail.BankAccountName2;
                                                    worksheet.Cells[incRowExcel, 37].Value = rsDetail.BankAccountNumber2;
                                                    worksheet.Cells[incRowExcel, 38].Value = rsDetail.BankBranchName2;
                                                    worksheet.Cells[incRowExcel, 39].Value = rsDetail.Currency2;
                                                    worksheet.Cells[incRowExcel, 40].Value = rsDetail.BICCode2;
                                                    worksheet.Cells[incRowExcel, 41].Value = rsDetail.BIMemberCode2;

                                                    worksheet.Cells[incRowExcel, 42].Value = rsDetail.BankName3;
                                                    worksheet.Cells[incRowExcel, 43].Value = rsDetail.BankAccountName3;
                                                    worksheet.Cells[incRowExcel, 44].Value = rsDetail.BankAccountNumber3;
                                                    worksheet.Cells[incRowExcel, 45].Value = rsDetail.BankBranchName3;
                                                    worksheet.Cells[incRowExcel, 46].Value = rsDetail.Currency3;
                                                    worksheet.Cells[incRowExcel, 47].Value = rsDetail.BICCode3;
                                                    worksheet.Cells[incRowExcel, 48].Value = rsDetail.BIMemberCode3;

                                                    worksheet.Cells[incRowExcel, 49].Value = rsDetail.FirstNameInd;
                                                    worksheet.Cells[incRowExcel, 50].Value = rsDetail.MiddleNameInd;
                                                    worksheet.Cells[incRowExcel, 51].Value = rsDetail.LastNameInd;
                                                    worksheet.Cells[incRowExcel, 52].Value = rsDetail.BirthPlace;
                                                    worksheet.Cells[incRowExcel, 53].Value = Convert.ToDateTime(rsDetail.CountryOfBirth).ToString("dd/MM/yyyy");
                                                    worksheet.Cells[incRowExcel, 54].Value = rsDetail.DOB;
                                                    worksheet.Cells[incRowExcel, 55].Value = rsDetail.GenderSex;
                                                    worksheet.Cells[incRowExcel, 56].Value = rsDetail.MaritalStatus;
                                                    worksheet.Cells[incRowExcel, 57].Value = rsDetail.Occupation;
                                                    worksheet.Cells[incRowExcel, 58].Value = rsDetail.Education;
                                                    worksheet.Cells[incRowExcel, 59].Value = rsDetail.Religion;
                                                    worksheet.Cells[incRowExcel, 60].Value = rsDetail.IncomePerAnnum;
                                                    worksheet.Cells[incRowExcel, 61].Value = rsDetail.SourceOfFunds;
                                                    worksheet.Cells[incRowExcel, 62].Value = rsDetail.InvestmentObjectives;
                                                    worksheet.Cells[incRowExcel, 63].Value = rsDetail.MotherMaidenName;
                                                    worksheet.Cells[incRowExcel, 64].Value = rsDetail.SpouseName;
                                                    worksheet.Cells[incRowExcel, 65].Value = rsDetail.SpouseOccupation;
                                                    worksheet.Cells[incRowExcel, 66].Value = rsDetail.Heir;
                                                    worksheet.Cells[incRowExcel, 67].Value = rsDetail.HeirRelation;
                                                    worksheet.Cells[incRowExcel, 68].Value = rsDetail.NatureOfBusiness;
                                                    worksheet.Cells[incRowExcel, 69].Value = rsDetail.NatureOfBusinessDesc;
                                                    worksheet.Cells[incRowExcel, 70].Value = rsDetail.PoliticallyExposed;
                                                    worksheet.Cells[incRowExcel, 71].Value = rsDetail.PoliticallyExposedDesc;
                                                    worksheet.Cells[incRowExcel, 72].Value = rsDetail.OtherHomePhone;
                                                    worksheet.Cells[incRowExcel, 73].Value = rsDetail.OtherCellPhone;
                                                    worksheet.Cells[incRowExcel, 74].Value = rsDetail.OtherFax;
                                                    worksheet.Cells[incRowExcel, 75].Value = rsDetail.OtherEmail;
                                                    worksheet.Cells[incRowExcel, 76].Value = rsDetail.CorrespondenceAddress;
                                                    worksheet.Cells[incRowExcel, 77].Value = rsDetail.CorrespondenceCity;
                                                    worksheet.Cells[incRowExcel, 78].Value = rsDetail.CorrespondenceZipCode;
                                                    worksheet.Cells[incRowExcel, 79].Value = rsDetail.Propinsi;
                                                    worksheet.Cells[incRowExcel, 80].Value = rsDetail.CorrespondenceRT;
                                                    worksheet.Cells[incRowExcel, 81].Value = rsDetail.CorrespondenceRW;
                                                    worksheet.Cells[incRowExcel, 82].Value = rsDetail.CountryofCorrespondence;
                                                    worksheet.Cells[incRowExcel, 83].Value = rsDetail.DomicileAddress;
                                                    worksheet.Cells[incRowExcel, 84].Value = rsDetail.KodeDomisiliPropinsi;
                                                    worksheet.Cells[incRowExcel, 85].Value = rsDetail.DomicileCity;
                                                    worksheet.Cells[incRowExcel, 86].Value = rsDetail.DomicileZipCode;
                                                    worksheet.Cells[incRowExcel, 87].Value = rsDetail.DomicileRT;
                                                    worksheet.Cells[incRowExcel, 88].Value = rsDetail.DomicileRW;
                                                    worksheet.Cells[incRowExcel, 89].Value = rsDetail.CountryofDomicile;
                                                    worksheet.Cells[incRowExcel, 90].Value = rsDetail.IdentityAddress1;
                                                    worksheet.Cells[incRowExcel, 91].Value = rsDetail.IdentityCity1;
                                                    worksheet.Cells[incRowExcel, 92].Value = rsDetail.IdentityZipCode1;
                                                    worksheet.Cells[incRowExcel, 93].Value = rsDetail.IdentityProvince1;
                                                    worksheet.Cells[incRowExcel, 94].Value = rsDetail.IdentityCountry1;
                                                    worksheet.Cells[incRowExcel, 95].Value = rsDetail.IdentityAddress2;
                                                    worksheet.Cells[incRowExcel, 96].Value = rsDetail.IdentityCity2;
                                                    worksheet.Cells[incRowExcel, 97].Value = rsDetail.IdentityZipCode2;
                                                    worksheet.Cells[incRowExcel, 98].Value = rsDetail.IdentityProvince2;
                                                    worksheet.Cells[incRowExcel, 99].Value = rsDetail.IdentityCountry2;
                                                    worksheet.Cells[incRowExcel, 100].Value = rsDetail.IdentityAddress3;
                                                    worksheet.Cells[incRowExcel, 101].Value = rsDetail.IdentityCity3;
                                                    worksheet.Cells[incRowExcel, 102].Value = rsDetail.IdentityZipCode3;
                                                    worksheet.Cells[incRowExcel, 103].Value = rsDetail.IdentityProvince3;
                                                    worksheet.Cells[incRowExcel, 104].Value = rsDetail.IdentityCountry3;
                                                    worksheet.Cells[incRowExcel, 105].Value = rsDetail.IdentityType1;
                                                    worksheet.Cells[incRowExcel, 106].Value = rsDetail.IdentityNumber1;
                                                    worksheet.Cells[incRowExcel, 107].Value = rsDetail.Identity1RT;
                                                    worksheet.Cells[incRowExcel, 108].Value = rsDetail.Identity1RW;
                                                    worksheet.Cells[incRowExcel, 109].Value = Convert.ToDateTime(rsDetail.RegistrationDateIdentitasInd1).ToString("dd/MM/yyyy");
                                                    worksheet.Cells[incRowExcel, 110].Value = Convert.ToDateTime(rsDetail.ExpiredDateIdentitasInd1).ToString("dd/MM/yyyy");
                                                    worksheet.Cells[incRowExcel, 111].Value = rsDetail.IdentityType2;
                                                    worksheet.Cells[incRowExcel, 112].Value = rsDetail.IdentityNumber2;
                                                    worksheet.Cells[incRowExcel, 113].Value = Convert.ToDateTime(rsDetail.RegistrationDateIdentitasInd2).ToString("dd/MM/yyyy");
                                                    worksheet.Cells[incRowExcel, 114].Value = Convert.ToDateTime(rsDetail.ExpiredDateIdentitasInd2).ToString("dd/MM/yyyy");
                                                    worksheet.Cells[incRowExcel, 115].Value = rsDetail.IdentityType3;
                                                    worksheet.Cells[incRowExcel, 116].Value = rsDetail.IdentityNumber3;
                                                    worksheet.Cells[incRowExcel, 117].Value = Convert.ToDateTime(rsDetail.RegistrationDateIdentitasInd3).ToString("dd/MM/yyyy");
                                                    worksheet.Cells[incRowExcel, 118].Value = Convert.ToDateTime(rsDetail.ExpiredDateIdentitasInd3).ToString("dd/MM/yyyy");
                                                    worksheet.Cells[incRowExcel, 119].Value = rsDetail.FATCAStatus;
                                                    worksheet.Cells[incRowExcel, 120].Value = rsDetail.TIN;
                                                    worksheet.Cells[incRowExcel, 121].Value = rsDetail.TINIssuanceCountry;
                                                    worksheet.Cells[incRowExcel, 122].Value = rsDetail.GIIN;
                                                    worksheet.Cells[incRowExcel, 123].Value = rsDetail.SubstantialOwnerName;
                                                    worksheet.Cells[incRowExcel, 124].Value = rsDetail.SubstantialOwnerAddress;
                                                    worksheet.Cells[incRowExcel, 125].Value = rsDetail.SubstantialOwnerTIN;
                                                    worksheet.Cells[incRowExcel, 126].Value = rsDetail.AlamatKantorInd;
                                                    worksheet.Cells[incRowExcel, 127].Value = rsDetail.KodeKotaKantorInd;
                                                    worksheet.Cells[incRowExcel, 128].Value = rsDetail.KodePosKantorInd;
                                                    worksheet.Cells[incRowExcel, 129].Value = rsDetail.KodePropinsiKantorInd;
                                                    worksheet.Cells[incRowExcel, 130].Value = rsDetail.KodeCountryofKantor;
                                                    worksheet.Cells[incRowExcel, 131].Value = rsDetail.Description;

                                                    worksheet.Cells[incRowExcel, 132].Value = rsDetail.EntryUsersID;
                                                    worksheet.Cells[incRowExcel, 133].Value = rsDetail.EntryTime;
                                                    worksheet.Cells[incRowExcel, 134].Value = rsDetail.UpdateUsersID;
                                                    worksheet.Cells[incRowExcel, 135].Value = rsDetail.UpdateTime;
                                                    worksheet.Cells[incRowExcel, 136].Value = rsDetail.ApprovedUsersID;
                                                    worksheet.Cells[incRowExcel, 137].Value = rsDetail.ApprovedTime;
                                                    worksheet.Cells[incRowExcel, 138].Value = rsDetail.VoidUsersID;
                                                    worksheet.Cells[incRowExcel, 139].Value = rsDetail.VoidTime;
                                                    worksheet.Cells[incRowExcel, 140].Value = rsDetail.SuspendBy;
                                                    worksheet.Cells[incRowExcel, 141].Value = rsDetail.SuspendTime;
                                                    worksheet.Cells[incRowExcel, 142].Value = rsDetail.UnSuspendBy;
                                                    worksheet.Cells[incRowExcel, 143].Value = rsDetail.UnSuspendTime;
                                                    _endRowDetail = incRowExcel;
                                                    incRowExcel++;

                                                    //worksheet.Cells["A" + _endRowDetail + ":EM" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                    worksheet.Cells["A" + _endRowDetail + ":EM" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                    worksheet.Cells["A" + _endRowDetail + ":EM" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                                    worksheet.Cells["A" + _endRowDetail + ":EM" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                }
                                                else
                                                {
                                                    worksheet.Cells[incRowExcel, 1].Value = rsDetail.FundClientID;
                                                    worksheet.Cells[incRowExcel, 2].Value = rsDetail.InvestorType;
                                                    worksheet.Cells[incRowExcel, 3].Value = rsDetail.InternalCategory;
                                                    worksheet.Cells[incRowExcel, 4].Value = rsDetail.InternalName;
                                                    worksheet.Cells[incRowExcel, 5].Value = rsDetail.SellingAgent;
                                                    worksheet.Cells[incRowExcel, 6].Value = rsDetail.NPWP;
                                                    worksheet.Cells[incRowExcel, 7].Value = rsDetail.RegistrationNPWP;
                                                    worksheet.Cells[incRowExcel, 8].Value = rsDetail.Email;
                                                    worksheet.Cells[incRowExcel, 9].Value = rsDetail.PhoneNumber;
                                                    worksheet.Cells[incRowExcel, 10].Value = rsDetail.MobilePhone;
                                                    worksheet.Cells[incRowExcel, 11].Value = rsDetail.Fax;
                                                    worksheet.Cells[incRowExcel, 12].Value = rsDetail.SID;
                                                    worksheet.Cells[incRowExcel, 13].Value = rsDetail.IFUACode;
                                                    worksheet.Cells[incRowExcel, 14].Value = rsDetail.InvestorsRiskProfile;
                                                    worksheet.Cells[incRowExcel, 15].Value = rsDetail.KYCRiskProfile;
                                                    worksheet.Cells[incRowExcel, 16].Value = rsDetail.AssetOwner;
                                                    worksheet.Cells[incRowExcel, 17].Value = rsDetail.StatementType;
                                                    worksheet.Cells[incRowExcel, 18].Value = rsDetail.SellingAgentCode;
                                                    worksheet.Cells[incRowExcel, 19].Value = rsDetail.DormantDate;
                                                    worksheet.Cells[incRowExcel, 20].Value = rsDetail.Description;
                                                    worksheet.Cells[incRowExcel, 21].Value = rsDetail.Affiliated;
                                                    worksheet.Cells[incRowExcel, 22].Value = rsDetail.AffiliatedWith;
                                                    worksheet.Cells[incRowExcel, 23].Value = rsDetail.Suspended;
                                                    worksheet.Cells[incRowExcel, 24].Value = rsDetail.BankName1;
                                                    worksheet.Cells[incRowExcel, 25].Value = rsDetail.BankAccountName1;
                                                    worksheet.Cells[incRowExcel, 26].Value = rsDetail.BankAccountNumber1;
                                                    worksheet.Cells[incRowExcel, 27].Value = rsDetail.BankBranchName1;
                                                    worksheet.Cells[incRowExcel, 28].Value = rsDetail.Currency1;
                                                    worksheet.Cells[incRowExcel, 29].Value = rsDetail.BICCode1;
                                                    worksheet.Cells[incRowExcel, 30].Value = rsDetail.BIMemberCode1;
                                                    worksheet.Cells[incRowExcel, 31].Value = rsDetail.BankName2;
                                                    worksheet.Cells[incRowExcel, 32].Value = rsDetail.BankAccountName2;
                                                    worksheet.Cells[incRowExcel, 33].Value = rsDetail.BankAccountNumber2;
                                                    worksheet.Cells[incRowExcel, 34].Value = rsDetail.BankBranchName2;
                                                    worksheet.Cells[incRowExcel, 35].Value = rsDetail.Currency2;
                                                    worksheet.Cells[incRowExcel, 36].Value = rsDetail.BICCode2;
                                                    worksheet.Cells[incRowExcel, 37].Value = rsDetail.BIMemberCode2;
                                                    worksheet.Cells[incRowExcel, 38].Value = rsDetail.BankName3;
                                                    worksheet.Cells[incRowExcel, 39].Value = rsDetail.BankAccountName3;
                                                    worksheet.Cells[incRowExcel, 40].Value = rsDetail.BankAccountNumber3;
                                                    worksheet.Cells[incRowExcel, 41].Value = rsDetail.BankBranchName3;
                                                    worksheet.Cells[incRowExcel, 42].Value = rsDetail.Currency3;
                                                    worksheet.Cells[incRowExcel, 43].Value = rsDetail.BICCode3;
                                                    worksheet.Cells[incRowExcel, 44].Value = rsDetail.BIMemberCode3;
                                                    worksheet.Cells[incRowExcel, 45].Value = rsDetail.BankRDN;
                                                    worksheet.Cells[incRowExcel, 46].Value = rsDetail.RDNAccountName;
                                                    worksheet.Cells[incRowExcel, 47].Value = rsDetail.RDNAccountNumber;
                                                    worksheet.Cells[incRowExcel, 48].Value = rsDetail.CompanyName;
                                                    worksheet.Cells[incRowExcel, 49].Value = rsDetail.CompanyAddress;
                                                    worksheet.Cells[incRowExcel, 50].Value = rsDetail.CompanyZipCode;
                                                    worksheet.Cells[incRowExcel, 51].Value = rsDetail.CompanyCity;
                                                    worksheet.Cells[incRowExcel, 52].Value = rsDetail.CompanyLegalDomicile;
                                                    worksheet.Cells[incRowExcel, 53].Value = rsDetail.CountryofDomicile;
                                                    worksheet.Cells[incRowExcel, 54].Value = rsDetail.CompanyType;
                                                    worksheet.Cells[incRowExcel, 55].Value = rsDetail.CompanyCharacteristic;
                                                    worksheet.Cells[incRowExcel, 56].Value = rsDetail.CompanyIncomePerAnnum;
                                                    worksheet.Cells[incRowExcel, 57].Value = rsDetail.CompanySourceOfFunds;
                                                    worksheet.Cells[incRowExcel, 58].Value = rsDetail.CompanyInvestmentObjective;
                                                    worksheet.Cells[incRowExcel, 59].Value = rsDetail.SKDNumber;
                                                    worksheet.Cells[incRowExcel, 60].Value = Convert.ToDateTime(rsDetail.ExpiredDateSKD).ToString("dd/MM/yyyy");
                                                    worksheet.Cells[incRowExcel, 61].Value = rsDetail.ArticleOfAssociation;
                                                    worksheet.Cells[incRowExcel, 62].Value = rsDetail.SIUPNumber;
                                                    worksheet.Cells[incRowExcel, 63].Value = Convert.ToDateTime(rsDetail.SIUPExpirationDate).ToString("dd/MM/yyyy");
                                                    worksheet.Cells[incRowExcel, 64].Value = rsDetail.FirstNameOfficer1;
                                                    worksheet.Cells[incRowExcel, 65].Value = rsDetail.MiddleNameOfficer1;
                                                    worksheet.Cells[incRowExcel, 66].Value = rsDetail.LastNameOfficer1;
                                                    worksheet.Cells[incRowExcel, 67].Value = rsDetail.PositionOfficer1;
                                                    worksheet.Cells[incRowExcel, 68].Value = rsDetail.PhoneNumberOfficer1;
                                                    worksheet.Cells[incRowExcel, 69].Value = rsDetail.EmailOfficer1;
                                                    worksheet.Cells[incRowExcel, 70].Value = rsDetail.IDType1Officer1;
                                                    worksheet.Cells[incRowExcel, 71].Value = rsDetail.IDNumber1Officer1;
                                                    worksheet.Cells[incRowExcel, 72].Value = Convert.ToDateTime(rsDetail.IDRegDate1Officer1).ToString("dd/MM/yyyy");
                                                    worksheet.Cells[incRowExcel, 73].Value = Convert.ToDateTime(rsDetail.IDExpireDate1Officer1).ToString("dd/MM/yyyy");
                                                    worksheet.Cells[incRowExcel, 74].Value = rsDetail.IDType2Officer1;
                                                    worksheet.Cells[incRowExcel, 75].Value = rsDetail.IDNumber2Officer1;
                                                    worksheet.Cells[incRowExcel, 76].Value = Convert.ToDateTime(rsDetail.IDRegDate2Officer1).ToString("dd/MM/yyyy");
                                                    worksheet.Cells[incRowExcel, 77].Value = Convert.ToDateTime(rsDetail.IDExpireDate2Officer1).ToString("dd/MM/yyyy");
                                                    worksheet.Cells[incRowExcel, 78].Value = rsDetail.IDType3Officer1;
                                                    worksheet.Cells[incRowExcel, 79].Value = rsDetail.IDNumber3Officer1;
                                                    worksheet.Cells[incRowExcel, 80].Value = Convert.ToDateTime(rsDetail.IDRegDate3Officer1).ToString("dd/MM/yyyy");
                                                    worksheet.Cells[incRowExcel, 81].Value = Convert.ToDateTime(rsDetail.IDExpireDate3Officer1).ToString("dd/MM/yyyy");
                                                    worksheet.Cells[incRowExcel, 82].Value = rsDetail.IDType4Officer1;
                                                    worksheet.Cells[incRowExcel, 83].Value = rsDetail.IDNumber4Officer1;
                                                    worksheet.Cells[incRowExcel, 84].Value = Convert.ToDateTime(rsDetail.IDRegDate4Officer1).ToString("dd/MM/yyyy");
                                                    worksheet.Cells[incRowExcel, 85].Value = Convert.ToDateTime(rsDetail.IDExpireDate4Officer1).ToString("dd/MM/yyyy");
                                                    worksheet.Cells[incRowExcel, 86].Value = rsDetail.FirstNameOfficer2;
                                                    worksheet.Cells[incRowExcel, 87].Value = rsDetail.MiddleNameOfficer2;
                                                    worksheet.Cells[incRowExcel, 88].Value = rsDetail.LastNameOfficer2;
                                                    worksheet.Cells[incRowExcel, 89].Value = rsDetail.PositionOfficer2;
                                                    worksheet.Cells[incRowExcel, 90].Value = rsDetail.IDType1Officer2;
                                                    worksheet.Cells[incRowExcel, 91].Value = rsDetail.IDNumber1Officer2;
                                                    worksheet.Cells[incRowExcel, 92].Value = Convert.ToDateTime(rsDetail.IDRegDate1Officer2).ToString("dd/MM/yyyy");
                                                    worksheet.Cells[incRowExcel, 93].Value = Convert.ToDateTime(rsDetail.IDExpireDate1Officer2).ToString("dd/MM/yyyy");
                                                    worksheet.Cells[incRowExcel, 94].Value = rsDetail.IDType2Officer2;
                                                    worksheet.Cells[incRowExcel, 95].Value = rsDetail.IDNumber2Officer2;
                                                    worksheet.Cells[incRowExcel, 96].Value = Convert.ToDateTime(rsDetail.IDRegDate2Officer2).ToString("dd/MM/yyyy");
                                                    worksheet.Cells[incRowExcel, 97].Value = Convert.ToDateTime(rsDetail.IDExpireDate2Officer2).ToString("dd/MM/yyyy");
                                                    worksheet.Cells[incRowExcel, 98].Value = rsDetail.IDType3Officer2;
                                                    worksheet.Cells[incRowExcel, 99].Value = rsDetail.IDNumber3Officer2;
                                                    worksheet.Cells[incRowExcel, 100].Value = Convert.ToDateTime(rsDetail.IDRegDate3Officer2).ToString("dd/MM/yyyy");
                                                    worksheet.Cells[incRowExcel, 101].Value = Convert.ToDateTime(rsDetail.IDExpireDate3Officer2).ToString("dd/MM/yyyy");
                                                    worksheet.Cells[incRowExcel, 102].Value = rsDetail.IDType4Officer2;
                                                    worksheet.Cells[incRowExcel, 103].Value = rsDetail.IDNumber4Officer2;
                                                    worksheet.Cells[incRowExcel, 104].Value = Convert.ToDateTime(rsDetail.IDRegDate4Officer2).ToString("dd/MM/yyyy");
                                                    worksheet.Cells[incRowExcel, 105].Value = Convert.ToDateTime(rsDetail.IDExpireDate4Officer2).ToString("dd/MM/yyyy");
                                                    worksheet.Cells[incRowExcel, 106].Value = Convert.ToDateTime(rsDetail.EstablishmentDate).ToString("dd/MM/yyyy");
                                                    worksheet.Cells[incRowExcel, 107].Value = rsDetail.EstablishmentPlace;
                                                    worksheet.Cells[incRowExcel, 108].Value = rsDetail.CountryofEstablishment;
                                                    worksheet.Cells[incRowExcel, 109].Value = rsDetail.CityOfEstablishment;
                                                    worksheet.Cells[incRowExcel, 110].Value = rsDetail.CountryofCompany;
                                                    worksheet.Cells[incRowExcel, 111].Value = rsDetail.CompanyCityName;
                                                    worksheet.Cells[incRowExcel, 112].Value = rsDetail.CompanyAddress;
                                                    worksheet.Cells[incRowExcel, 113].Value = rsDetail.CompanyZipCode;
                                                    worksheet.Cells[incRowExcel, 114].Value = rsDetail.BusinessPhone;
                                                    worksheet.Cells[incRowExcel, 115].Value = rsDetail.AssetFor1Year;
                                                    worksheet.Cells[incRowExcel, 116].Value = rsDetail.AssetFor2Year;
                                                    worksheet.Cells[incRowExcel, 117].Value = rsDetail.AssetFor3Year;
                                                    worksheet.Cells[incRowExcel, 118].Value = rsDetail.OperatingProfitFor1Year;
                                                    worksheet.Cells[incRowExcel, 119].Value = rsDetail.OperatingProfitFor2Year;
                                                    worksheet.Cells[incRowExcel, 120].Value = rsDetail.OperatingProfitFor3Year;
                                                    worksheet.Cells[incRowExcel, 121].Value = rsDetail.FirstNameOfficer3;
                                                    worksheet.Cells[incRowExcel, 122].Value = rsDetail.MiddleNameOfficer3;
                                                    worksheet.Cells[incRowExcel, 123].Value = rsDetail.LastNameOfficer3;
                                                    worksheet.Cells[incRowExcel, 124].Value = rsDetail.PositionOfficer3;
                                                    worksheet.Cells[incRowExcel, 125].Value = rsDetail.IDType1Officer3;
                                                    worksheet.Cells[incRowExcel, 126].Value = rsDetail.IDNumber1Officer3;
                                                    worksheet.Cells[incRowExcel, 127].Value = Convert.ToDateTime(rsDetail.IDRegDate1Officer3).ToString("dd/MM/yyyy");
                                                    worksheet.Cells[incRowExcel, 128].Value = Convert.ToDateTime(rsDetail.IDExpireDate1Officer3).ToString("dd/MM/yyyy");
                                                    worksheet.Cells[incRowExcel, 129].Value = rsDetail.IDType2Officer3;
                                                    worksheet.Cells[incRowExcel, 130].Value = rsDetail.IDNumber2Officer3;
                                                    worksheet.Cells[incRowExcel, 131].Value = Convert.ToDateTime(rsDetail.IDRegDate2Officer3).ToString("dd/MM/yyyy");
                                                    worksheet.Cells[incRowExcel, 132].Value = Convert.ToDateTime(rsDetail.IDExpireDate2Officer3).ToString("dd/MM/yyyy");
                                                    worksheet.Cells[incRowExcel, 133].Value = rsDetail.IDType3Officer3;
                                                    worksheet.Cells[incRowExcel, 134].Value = rsDetail.IDNumber3Officer3;
                                                    worksheet.Cells[incRowExcel, 135].Value = Convert.ToDateTime(rsDetail.IDRegDate3Officer3).ToString("dd/MM/yyyy");
                                                    worksheet.Cells[incRowExcel, 136].Value = Convert.ToDateTime(rsDetail.IDExpireDate3Officer3).ToString("dd/MM/yyyy");
                                                    worksheet.Cells[incRowExcel, 137].Value = rsDetail.IDType4Officer3;
                                                    worksheet.Cells[incRowExcel, 138].Value = rsDetail.IDNumber4Officer3;
                                                    worksheet.Cells[incRowExcel, 139].Value = Convert.ToDateTime(rsDetail.IDRegDate4Officer3).ToString("dd/MM/yyyy");
                                                    worksheet.Cells[incRowExcel, 140].Value = Convert.ToDateTime(rsDetail.IDExpireDate4Officer3).ToString("dd/MM/yyyy");
                                                    worksheet.Cells[incRowExcel, 141].Value = rsDetail.FirstNameOfficer4;
                                                    worksheet.Cells[incRowExcel, 142].Value = rsDetail.MiddleNameOfficer4;
                                                    worksheet.Cells[incRowExcel, 143].Value = rsDetail.LastNameOfficer4;
                                                    worksheet.Cells[incRowExcel, 144].Value = rsDetail.PositionOfficer4;
                                                    worksheet.Cells[incRowExcel, 145].Value = rsDetail.IDType1Officer4;
                                                    worksheet.Cells[incRowExcel, 146].Value = rsDetail.IDNumber1Officer4;
                                                    worksheet.Cells[incRowExcel, 147].Value = Convert.ToDateTime(rsDetail.IDRegDate1Officer4).ToString("dd/MM/yyyy");
                                                    worksheet.Cells[incRowExcel, 148].Value = Convert.ToDateTime(rsDetail.IDExpireDate1Officer4).ToString("dd/MM/yyyy");
                                                    worksheet.Cells[incRowExcel, 149].Value = rsDetail.IDType2Officer4;
                                                    worksheet.Cells[incRowExcel, 150].Value = rsDetail.IDNumber2Officer4;
                                                    worksheet.Cells[incRowExcel, 151].Value = Convert.ToDateTime(rsDetail.IDRegDate2Officer4).ToString("dd/MM/yyyy");
                                                    worksheet.Cells[incRowExcel, 152].Value = Convert.ToDateTime(rsDetail.IDExpireDate2Officer4).ToString("dd/MM/yyyy");
                                                    worksheet.Cells[incRowExcel, 153].Value = rsDetail.IDType3Officer4;
                                                    worksheet.Cells[incRowExcel, 154].Value = rsDetail.IDNumber3Officer4;
                                                    worksheet.Cells[incRowExcel, 155].Value = Convert.ToDateTime(rsDetail.IDRegDate3Officer4).ToString("dd/MM/yyyy");
                                                    worksheet.Cells[incRowExcel, 156].Value = Convert.ToDateTime(rsDetail.IDExpireDate3Officer4).ToString("dd/MM/yyyy");
                                                    worksheet.Cells[incRowExcel, 157].Value = rsDetail.IDType4Officer4;
                                                    worksheet.Cells[incRowExcel, 158].Value = rsDetail.IDNumber4Officer4;
                                                    worksheet.Cells[incRowExcel, 159].Value = Convert.ToDateTime(rsDetail.IDRegDate4Officer4).ToString("dd/MM/yyyy");
                                                    worksheet.Cells[incRowExcel, 160].Value = Convert.ToDateTime(rsDetail.IDExpireDate4Officer4).ToString("dd/MM/yyyy");
                                                    worksheet.Cells[incRowExcel, 161].Value = rsDetail.FATCAStatus;
                                                    worksheet.Cells[incRowExcel, 162].Value = rsDetail.TIN;
                                                    worksheet.Cells[incRowExcel, 163].Value = rsDetail.TINIssuanceCountry;
                                                    worksheet.Cells[incRowExcel, 164].Value = rsDetail.GIIN;
                                                    worksheet.Cells[incRowExcel, 165].Value = rsDetail.SubstantialOwnerName;
                                                    worksheet.Cells[incRowExcel, 166].Value = rsDetail.SubstantialOwnerAddress;
                                                    worksheet.Cells[incRowExcel, 167].Value = rsDetail.SubstantialOwnerTIN;
                                                    worksheet.Cells[incRowExcel, 168].Value = rsDetail.EntryUsersID;
                                                    worksheet.Cells[incRowExcel, 169].Value = rsDetail.EntryTime;
                                                    worksheet.Cells[incRowExcel, 170].Value = rsDetail.UpdateUsersID;
                                                    worksheet.Cells[incRowExcel, 171].Value = rsDetail.UpdateTime;
                                                    worksheet.Cells[incRowExcel, 172].Value = rsDetail.ApprovedUsersID;
                                                    worksheet.Cells[incRowExcel, 173].Value = rsDetail.ApprovedTime;
                                                    worksheet.Cells[incRowExcel, 174].Value = rsDetail.VoidUsersID;
                                                    worksheet.Cells[incRowExcel, 175].Value = rsDetail.VoidTime;
                                                    worksheet.Cells[incRowExcel, 176].Value = rsDetail.SuspendBy;
                                                    worksheet.Cells[incRowExcel, 177].Value = rsDetail.SuspendTime;
                                                    worksheet.Cells[incRowExcel, 178].Value = rsDetail.UnSuspendBy;
                                                    worksheet.Cells[incRowExcel, 179].Value = rsDetail.UnSuspendTime;
                                                    _endRowDetail = incRowExcel;
                                                    incRowExcel++;
                                                    worksheet.Cells["A" + _endRowDetail + ":FW" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                    worksheet.Cells["A" + _endRowDetail + ":FW" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                                    worksheet.Cells["A" + _endRowDetail + ":FW" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                }
                                                //area detail


                                            }
                                            incRowExcel = incRowExcel + 2;

                                            //_endRowDetail = incRowExcel;
                                            //worksheet.Cells["A" + _endRowDetail + ":FY" + _endRowDetail].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            //worksheet.Cells["A" + _endRowDetail + ":FY" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;


                                        }

                                        // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                        worksheet.Cells.AutoFitColumns(0);
                                        worksheet.PrinterSettings.Orientation = eOrientation.Landscape; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                        // worksheet.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN
                                        // worksheet.PrinterSettings.FitToPage = true;
                                        //worksheet.HeaderFooter.OddHeader.RightAlignedText = "&14 PAYMENT VOUCHER";
                                        // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                        worksheet.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
                                        worksheet.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
                                        //worksheet.View.PageLayoutView = Tools.DefaultReportPageLayoutView();
                                        worksheet.View.ShowGridLines = Tools.DefaultReportShowGridLines();
                                        worksheet.HeaderFooter.OddHeader.CenteredText = "&14 Fund Client";
                                        worksheet.HeaderFooter.OddHeader.LeftAlignedText = Tools.DefaultReportHeaderLeftText();

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
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception err)
            {
                return false;
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
                         @" 
                            
                        Select lower(C.ApprovedUsersID) CheckedBy,lower(C.ApprovedUsersID) ApprovedBy, lower(C.EntryUsersID) PreparedBy, C.Reference ,      
                        valuedate,A.ID AccountID, A.Name AccountName, C.Description,     
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
                        valuedate,A.ID AccountID, A.Name AccountName, '' Description,         
                        'C', 0 Debit,SUM(Case When DebitCredit ='D' then BaseDebit else BaseCredit * -1 end)Credit,'' DepartmentID ,3 Row          
                        from Cashier C       
                        left join Account A on C.Creditaccountpk =A.Accountpk and A.status = 2       
                        left join Office E on C.OfficePK = E.OfficePK and E.status = 2       
                        left join Department F on C.DepartmentPK = F.DepartmentPK and F.status = 2       
                        left join Agent G on C.AgentPK = G.AgentPK and G.status = 2       
                        left join Consignee H on C.ConsigneePK = H.ConsigneePK and H.status = 2       
                        left join Instrument I on C.InstrumentPK = I.InstrumentPK and I.status = 2       
                        Where C.CashierID = @CashierID  and C.Type = 'CP' and C.PeriodPK = @PeriodPK and C.Status in (1,2)    
                        group by C.EntryUsersID, C.ApprovedUsersID, Valuedate,A.ID, A.Name, C.Reference     
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
                                                 group r by new { r.Reference, r.ValueDate, r.CheckedBy, r.PreparedBy, r.ApprovedBy } into rGroup
                                                 select rGroup;

                                    int incRowExcel = 0;

                                    foreach (var rsHeader in GroupByReference)
                                    {

                                        incRowExcel = incRowExcel + 4;
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 1].Value = Tools.DefaultReportHeaderLeftBatchReport();
                                        worksheet.Cells["A" + incRowExcel + ":C" + incRowExcel].Merge = true;
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Size = 13;

                                        incRowExcel = incRowExcel + 2;
                                        worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 2].Value = "REFERENCE : ";
                                        worksheet.Cells[incRowExcel, 3].Value = rsHeader.Key.Reference;
                                        worksheet.Cells[incRowExcel, 5].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 5].Value = "DATE : ";
                                        //worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "dd-MM-yyyy";
                                        worksheet.Cells[incRowExcel, 6].Value = Convert.ToDateTime(rsHeader.Key.ValueDate).ToString("MMMM/dd/yyyy");
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
                                        worksheet.Cells[incRowExcel, 2].Value = "(     " + rsHeader.Key.PreparedBy + "      )";
                                        worksheet.Cells["B" + incRowExcel + ":C" + incRowExcel].Merge = true;

                                        worksheet.Cells[incRowExcel, 4].Value = "(      " + rsHeader.Key.CheckedBy + "      )";
                                        worksheet.Cells["D" + incRowExcel + ":E" + incRowExcel].Merge = true;

                                        worksheet.Cells[incRowExcel, 6].Value = "(      " + rsHeader.Key.ApprovedBy + "      )";
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


                                    Image img = Image.FromFile(Tools.ReportImage);
                                    worksheet.HeaderFooter.OddHeader.InsertPicture(img, PictureAlignment.Left);

                                    worksheet.HeaderFooter.OddHeader.CenteredText = "&14 PAYMENT / JOURNAL VOUCHER";
                                    //worksheet.HeaderFooter.OddHeader.LeftAlignedText = Tools.DefaultReportHeaderLeftText();

                                    worksheet.HeaderFooter.OddFooter.RightAlignedText =
                                    string.Format("Page {0} of {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);

                                    worksheet.HeaderFooter.OddFooter.CenteredText = Tools.DefaultReportFooterCenterText();
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
                            valuedate,A.ID AccountID, A.Name AccountName, C.Description,       
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
                            valuedate,A.ID AccountID, A.Name AccountName, '' Description,       
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
                            group by C.EntryUsersID , C.ApprovedUsersID, Valuedate,A.ID, A.Name,C.Reference     
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
                                                 group r by new { r.Reference, r.ValueDate, r.CheckedBy, r.ApprovedBy, r.PreparedBy } into rGroup
                                                 select rGroup;

                                    int incRowExcel = 0;

                                    foreach (var rsHeader in GroupByReference)
                                    {

                                        incRowExcel = incRowExcel + 4;
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 1].Value = Tools.DefaultReportHeaderLeftBatchReport();
                                        worksheet.Cells["A" + incRowExcel + ":C" + incRowExcel].Merge = true;
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Size = 13;

                                        incRowExcel = incRowExcel + 2;
                                        worksheet.Cells[incRowExcel, 2].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 2].Value = "REFERENCE : ";
                                        worksheet.Cells[incRowExcel, 3].Value = rsHeader.Key.Reference;
                                        worksheet.Cells[incRowExcel, 5].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 5].Value = "DATE : ";
                                        //worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "dd-MM-yyyy";
                                        worksheet.Cells[incRowExcel, 6].Value = Convert.ToDateTime(rsHeader.Key.ValueDate).ToString("MMMM/dd/yyyy");
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
                                        worksheet.Cells[incRowExcel, 2].Value = "(     " + rsHeader.Key.PreparedBy + "      )";
                                        worksheet.Cells["B" + incRowExcel + ":C" + incRowExcel].Merge = true;

                                        worksheet.Cells[incRowExcel, 4].Value = "(      " + rsHeader.Key.CheckedBy + "      )";
                                        worksheet.Cells["D" + incRowExcel + ":E" + incRowExcel].Merge = true;

                                        worksheet.Cells[incRowExcel, 6].Value = "(      " + rsHeader.Key.ApprovedBy + "      )";
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


                                    Image img = Image.FromFile(Tools.ReportImage);
                                    worksheet.HeaderFooter.OddHeader.InsertPicture(img, PictureAlignment.Left);

                                    worksheet.HeaderFooter.OddHeader.CenteredText = "&14 RECEIPT / JOURNAL VOUCHER";
                                    //worksheet.HeaderFooter.OddHeader.LeftAlignedText = Tools.DefaultReportHeaderLeftText();

                                    worksheet.HeaderFooter.OddFooter.RightAlignedText =
                                    string.Format("Page {0} of {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);

                                    worksheet.HeaderFooter.OddFooter.CenteredText = Tools.DefaultReportFooterCenterText();
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


        public string SInvestSubscriptionRpt_BySelectedData(string _userID, DateTime _dateFrom, DateTime _dateTo, ParamUnitRegistryBySelected _paramUnitRegistryBySelected)
        {

            try
            {

                string paramClientSubscriptionSelected = "";
                if (!_host.findString(_paramUnitRegistryBySelected.UnitRegistrySelected.ToLower(), "0", ",") && !string.IsNullOrEmpty(_paramUnitRegistryBySelected.UnitRegistrySelected))
                {
                    paramClientSubscriptionSelected = "  ClientSubscriptionPK in (" + _paramUnitRegistryBySelected.UnitRegistrySelected + ") ";
                }
                else
                {
                    paramClientSubscriptionSelected = "  ClientSubscriptionPK in (0) ";
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
                                    select 'Transaction Date|Transaction Type|SA Code|Investor Fund Unit A/C No.|Fund Code|Amount (Nominal)|Amount (Unit)|Amount (All Units)|Fee (Nominal)|Fee (Unit)|Fee (%)|REDM Payment A/C Sequential Code|REDM Payment Bank BIC Code|REDM Payment Bank BI Member Code|REDM Payment A/C No.|Payment Date|Transfer Type|SA Reference No.'
                                    
                                    insert into #Text         
                                    Select  RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(CONVERT(VARCHAR(10), ValueDate, 112),''))))
                                    + '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(A.Type,''))))
                                    + '|' + @CompanyID
                                    + '|' + isnull(A.IFUACode,'')
                                    + '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(A.SInvestCode,''))))
                                    + '|' + case when A.CashAmount = 0 then '' else cast(isnull(cast(A.CashAmount as decimal(22,2)),'')as nvarchar) end -- TotalCashAmount
                                    + '|' + case when A.TotalUnitAmount = 0 then '' else cast(isnull(Round(A.TotalUnitAmount,4),'')as nvarchar) end
                                    + '|' + 
                                    + '|' + case when A.FeeType = 1 then '' else case when A.FeeAmount = 0 then '' else cast(isnull(cast(A.FeeAmount as decimal(22,2)),'')as nvarchar) end end-- 0
                                    + '|' + 
                                    + '|' + case when A.FeeType = 2 then '' else case when A.FeePercent = 0 then '' else cast(isnull(cast(A.FeePercent as decimal(22,2)),'')as nvarchar) end end-- 0 -- Feepercent
                                    + '|' + 
                                    + '|' + 
                                    + '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(A.BICode,''))))
                                    + '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(A.AccNo,''))))
                                    + '|' +        
                                    + '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(A.TransferType,''))))
                                    + '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(A.Reference,''))))
                                    from (      
                                    Select CS.ValueDate,F.SInvestCode, '' SettlementDate,CS.SubscriptionFeePercent FeePercent,CS.SubscriptionFeeAmount FeeAmount,'1' Type,
                                    ROUND(CashAmount,2)CashAmount ,ROUND(TotalUnitAmount,4)TotalUnitAmount ,'' BICode, '' AccNo ,'' TransferType, 
                                    Case When ReferenceSInvest <> '' and ReferenceSInvest is not null then ReferenceSInvest else cast(ClientSubscriptionPK as nvarchar) end Reference
                                    ,FC.IFUACode,CS.FeeType  
                                    from ClientSubscription CS 
                                    left join Fund F on Cs.FundPK = F.fundPK and f.Status in (1,2)     
                                    left join FundClient FC on CS.FundClientPK = FC.FundClientPK and fc.Status in (1,2)
                                    where    
                                    ValueDate =  @ValueDate and " + paramClientSubscriptionSelected + @" and Cs.status = 2
                                    )A    
                                    Group by A.ValueDate,A.SInvestCode,A.FeePercent,A.BICode,A.AccNo,A.SettlementDate,A.FeeAmount,A.Type,A.CashAmount,A.TotalUnitAmount,A.TransferType,A.Reference,A.IFUACode,A.FeeType
                                    order by A.ValueDate Asc
                                    select * from #text          
                                    END  ";
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


        public string SInvestRedemptionRpt_BySelectedData(string _userID, DateTime _dateFrom, DateTime _dateTo, ParamUnitRegistryBySelected _paramUnitRegistryBySelected)
        {

            try
            {

                string paramClientRedemptionSelected = "";
                if (!_host.findString(_paramUnitRegistryBySelected.UnitRegistrySelected.ToLower(), "0", ",") && !string.IsNullOrEmpty(_paramUnitRegistryBySelected.UnitRegistrySelected))
                {
                    paramClientRedemptionSelected = " And ClientRedemptionPK in (" + _paramUnitRegistryBySelected.UnitRegistrySelected + ") ";
                }
                else
                {
                    paramClientRedemptionSelected = " And ClientRedemptionPK in (0) ";
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
                        select 'Transaction Date|Transaction Type|SA Code|Investor Fund Unit A/C No.|Fund Code|Amount (Nominal)|Amount (Unit)|Amount (All Units)|Fee (Nominal)|Fee (Unit)|Fee (%)|REDM Payment A/C Sequential Code|REDM Payment Bank BIC Code|REDM Payment Bank BI Member Code|REDM Payment A/C No.|Payment Date|Transfer Type|SA Reference No.' 
     
                        insert into #Text           
                        Select  RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(CONVERT(VARCHAR(10), ValueDate, 112),''))))
                        + '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(A.Type,''))))
                        + '|' + @CompanyID
                        + '|' + isnull(A.IFUACode,'')
                        + '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(A.SInvestCode,''))))
                        + '|' + case when A.CashAmount = 0 then '' else cast(isnull(cast(A.CashAmount as decimal(22,2)),'')as nvarchar) end
                        + '|' + case when A.TotalUnitAmount = 0 then '' else cast(isnull(Round(A.TotalUnitAmount,4),'')as nvarchar) end
                        + '|' + case when A.BitRedemptionAll = 1 then 'Y' else '' end
                        + '|' + case when A.FeeType = 1 then '' else case when A.FeeAmount = 0 then '' else cast(isnull(cast(A.FeeAmount as decimal(22,2)),'')as nvarchar) end end-- 0
                        + '|' + 
                        + '|' + case when A.FeeType = 2 then '' else case when A.FeePercent = 0 then '' else cast(isnull(cast(A.FeePercent as decimal(22,2)),'')as nvarchar) end end-- 0 -- Feepercent
                        + '|' + 
                        + '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(A.SInvestID,''))))
                        + '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(A.BICode,''))))
                        + '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(A.AccNo,''))))
                        + '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull((case when CONVERT(VARCHAR(10), SettlementDate, 112) <> '19000101' then CONVERT(VARCHAR(10), SettlementDate, 112) else '' End),''))))          
                        + '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(A.TransferType,''))))
                        + '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(A.Reference,''))))
                        from (      
                                    
                        Select CR.ValueDate,F.SInvestCode,CR.PaymentDate SettlementDate,CR.RedemptionFeePercent FeePercent,CR.RedemptionFeeAmount FeeAmount,'2' Type,
                        ROUND(CashAmount,2)CashAmount ,ROUND(TotalUnitAmount,4)TotalUnitAmount ,CR.BitRedemptionAll BitRedemptionAll , CR.FeeType FeeType, 

                        case when BankRecipientPK= 1 and B1.Country <> 'ID' then isnull(B1.SInvestID,'') else case when BankRecipientPK= 2 and B2.Country <> 'ID' then isnull(B2.SInvestID,'')  else
                        Case when BankRecipientPK= 3 and B3.Country <> 'ID' then isnull(B3.SInvestID,'') else case when   B4.Country <> 'ID' then isnull(B4.SInvestID,'')
                        else '' end end end end SInvestID,

                        case when BankRecipientPK= 1 and B1.Country = 'ID' then isnull(B1.BICode,'') else case when BankRecipientPK= 2 and B2.Country = 'ID' then isnull(B2.BICode,'')  else
                        Case when BankRecipientPK= 3 and B3.Country = 'ID' then isnull(B3.BICode,'') else case when  B4.Country = 'ID' then isnull(B4.BICode,'')
                        else '' end end end end BICode,

                        case when BankRecipientPK = 0 then  '' when BankRecipientPK=1 then FC.NomorRekening1 
                        else case when BankRecipientPK = 2 then FC.NomorRekening2  else 
                        case when BankRecipientPK = 3 then FC.NomorRekening3 else FCB.AccountNo end end end AccNo ,


                        TransferType TransferType
                        ,Case When ReferenceSInvest <> '' and ReferenceSInvest is not null then ReferenceSInvest else  cast(ClientRedemptionPK as nvarchar) end Reference,FC.IFUACode
                        from ClientRedemption CR 


                        left join Fund F on CR.FundPK = F.fundPK and f.Status in (1,2)
                        left join FundClient FC on CR.FundClientPK = FC.FundClientPK and fc.Status in (1,2)      
                        left join Bank B1 ON FC.NamaBank1 = B1.BankPK  and B1.status in (1,2)   
                        left join Bank B2 ON FC.NamaBank2 = B2.BankPK  and B2.status in (1,2)    
                        left join Bank B3 ON FC.NamaBank3 = B3.BankPK  and B3.status in (1,2)   
                        Left Join FundClientBankList FCB on CR.BankRecipientPK = FCB.NoBank and FCB.status in (1,2)
                        and FCB.fundclientPK = Cr.FundClientPK
                        Left join Bank B4 on FCB.bankPK = B4.BankPK and B4.status in (1,2)
                        where ValueDate =  @ValueDate  and Cr.status = 2 " + paramClientRedemptionSelected + @"
                        )A    
                        Group by A.ValueDate,A.SInvestCode,A.FeePercent,A.BICode,A.AccNo,A.SettlementDate,A.FeeAmount,A.Type,A.CashAmount,A.TotalUnitAmount,A.BitRedemptionAll,A.TransferType,A.Reference,A.IFUACode, A.FeeType, A.SInvestID
                        order by A.ValueDate Asc        
                        select * from #text  
                        END";

                        cmd.Parameters.AddWithValue("@CompanyID", _host.Get_CompanyID());
                        cmd.Parameters.AddWithValue("@ValueDate", _dateFrom);



                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                string filePath = Tools.SInvestTextPath + "SUBS_REDM_SWTC_Order_Upload_REDM_Order.txt";
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
                                    return Tools.HtmlSinvestTextPath + "SUBS_REDM_SWTC_Order_Upload_REDM_Order.txt";
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
                                    --drop table #text   
                                    create table #Text(    
                                    [ResultText] [nvarchar](1000)  NULL        
                                    )   
                   
                                    truncate table #Text     
                                    insert into #Text     
                                    select 'Transaction Date|Transaction Type|SA Code|Investor Fund Unit A/C No.|Fund Code|Amount (Nominal)|Amount (Unit)|Amount (All Units)|Fee (Nominal)|Fee (Unit)|Fee (%)|REDM Payment A/C Sequential Code|REDM Payment Bank BIC Code|REDM Payment Bank BI Member Code|REDM Payment A/C No.|Payment Date|Transfer Type|SA Reference No.'

                                    insert into #Text         
                                    Select  RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(CONVERT(VARCHAR(10), ValueDate, 112),''))))
                                    + '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(A.Type,''))))
                                    + '|' + @CompanyID
                                    + '|' + isnull(A.IFUACode,'')
                                    + '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(A.FundFromSinvest,''))))
                                    + '|' + case when A.CashAmount = 0 then '' else cast(isnull(cast(A.CashAmount as decimal(22,2)),'')as nvarchar) end
                                    + '|' + case when A.UnitAmount = 0 then '' else cast(isnull(cast(A.UnitAmount as decimal(22,4)),'') as nvarchar) end
                                    + '|' + case when A.BitSwitchingAll = 1 then 'Y' else '' end
                                    + '|' + case when A.Feetype = 'OUT' then '1' else '2' end
                                    + '|' + case when A.FeeTypeMethod = 1 then '' else case when A.FeeAmount = 0 then '' else cast(isnull(cast(A.FeeAmount as decimal(22,2)),'')as nvarchar) end end-- 0
                                    + '|' + 
                                    + '|' + case when A.FeeTypeMethod = 2 then '' else case when A.FeePercent = 0 then '' else cast(isnull(cast(A.FeePercent as decimal(22,2)),'')as nvarchar) end end-- 0 -- Feepercent
                                    + '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(A.FundToSinvest,''))))
                                    + '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull((case when CONVERT(VARCHAR(10), SettlementDate, 112) <> '19000101' then CONVERT(VARCHAR(10), SettlementDate, 112) else '' End),''))))          
                                    + '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(A.TransferType,''))))
                                    + '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(A.Reference,'')))) 

                                    from (   
  
                                    Select CW.FundPKFrom,CW.FundPKTo,CW.ValueDate,F.SInvestCode FundFromSinvest, F1.SInvestCode FundToSinvest,CW.PaymentDate SettlementDate,CW.SwitchingFeePercent FeePercent,CW.SwitchingFeeAmount FeeAmount,'3' Type,ROUND(CashAmount,2)CashAmount ,ROUND(UnitAmount,4)UnitAmount ,CW.BitSwitchingAll BitSwitchingAll,TransferType TransferType
                                    ,Case When ReferenceSInvest <> '' and ReferenceSInvest is not null then ReferenceSInvest else cast(ClientSwitchingPK as nvarchar) end Reference,FC.IFUACode,CW.FeeType,CW.FeeTypeMethod
                                    from ClientSwitching CW 
                                    left join Fund F on CW.FundPKFrom = F.fundPK and f.Status=2 
                                    left join fund F1 on CW.FundPKTo = F1.FundPK and F1.status = 2 
                                    left join FundClient FC on CW.FundClientPK = FC.FundClientPK and fc.Status=2      
                                    where ValueDate =   @ValueDate " + paramClientSwitchingSelected + @" and Cw.status = 2
                                    )A    
                                    Group by A.FundPKFrom,A.FundPKTo,A.ValueDate,A.FundFromSinvest,A.FundToSinvest,A.FeePercent,A.SettlementDate,A.FeeAmount,A.Type,A.CashAmount,A.UnitAmount,A.BitSwitchingAll,A.TransferType,A.Reference,A.IFUACode,A.FeeType,A.FeeTypeMethod
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