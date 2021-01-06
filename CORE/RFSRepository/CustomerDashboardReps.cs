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
using System.Text;
using System.Threading;
using System.Data;
using System.Data.OleDb;

namespace RFSRepository
{
    public class CustomerDashboardReps
    {
        Host _host = new Host();

        //2
        private CustomerDashboard setCustomerDashboard(SqlDataReader dr)
        {
            CustomerDashboard M_CustomerDashboard = new CustomerDashboard();

            M_CustomerDashboard.SID = Convert.ToString(dr["SID"]);
            M_CustomerDashboard.IFUACode = Convert.ToString(dr["IFUACode"]);
            M_CustomerDashboard.FundClientID = Convert.ToString(dr["FundClientID"]);
            M_CustomerDashboard.InternalCategory = Convert.ToString(dr["InternalCategory"]);
            M_CustomerDashboard.FundClientName = Convert.ToString(dr["FundClientName"]);
            M_CustomerDashboard.AgentName = Convert.ToString(dr["AgentName"]);
            M_CustomerDashboard.BankName = Convert.ToString(dr["BankName"]);
            M_CustomerDashboard.BankAccNo = Convert.ToString(dr["BankAccNo"]);
            M_CustomerDashboard.BankAccName = Convert.ToString(dr["BankAccName"]);
            M_CustomerDashboard.FundID = Convert.ToString(dr["FundID"]);
            M_CustomerDashboard.Amount = Convert.ToDecimal(dr["Amount"]);
            M_CustomerDashboard.Unit = Convert.ToDecimal(dr["Unit"]);
            M_CustomerDashboard.LastNav = Convert.ToDecimal(dr["LastNav"]);
            M_CustomerDashboard.LastNavDate = Convert.ToString(dr["LastNavDate"]);
            M_CustomerDashboard.PlaceOfBirth = Convert.ToString(dr["PlaceOfBirth"]);
            M_CustomerDashboard.DateOfBirth = Convert.ToString(dr["DateOfBirth"]);
            M_CustomerDashboard.KYCRiskAppetite = Convert.ToString(dr["KYCRiskAppetite"]);
            M_CustomerDashboard.IDType = Convert.ToString(dr["IDType"]);
            M_CustomerDashboard.IDNo = Convert.ToString(dr["IDNo"]);
            M_CustomerDashboard.MotherName = Convert.ToString(dr["MotherName"]);
            M_CustomerDashboard.HighRisk = Convert.ToString(dr["HighRisk"]);
            M_CustomerDashboard.RegDate = Convert.ToString(dr["RegDate"]);
            M_CustomerDashboard.OnlineFLag = Convert.ToString(dr["OnlineFLag"]);
            M_CustomerDashboard.CantSub = Convert.ToBoolean(dr["CantSub"]);
            M_CustomerDashboard.CantRed = Convert.ToBoolean(dr["CantRed"]);
            M_CustomerDashboard.LastKYCUpdate = Convert.ToString(dr["LastKYCUpdate"]);
            M_CustomerDashboard.Phone = Convert.ToString(dr["Phone"]);
            M_CustomerDashboard.Email = Convert.ToString(dr["Email"]);
            M_CustomerDashboard.Type = Convert.ToString(dr["Type"]);

            return M_CustomerDashboard;
        }

        public List<CustomerDashboard> CustomerDashboard_SelectByDate(int _status, DateTime _date)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<CustomerDashboard> L_CustomerDashboard = new List<CustomerDashboard>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {


                        cmd.CommandText =
                            @"
                        
DECLARE @CFundPK int
DECLARE @MaxDate datetime

select @MaxDate = Date from FundClientPosition where Date = (
select MAX(date) from FundClientPosition where Date <= @Date
)


if object_id('tempdb..#NAV', 'u') is not null drop table #NAV 
create table #NAV
(
	LastDate DATETIME,
	FundPK INT,
	NAV NUMERIC(22,8)
)
CREATE CLUSTERED INDEX indx_tableNAV ON #NAV (FundPK);

if object_id('tempdb..#TableFund', 'u') is not null drop table #TableFund 
create table #TableFund
(
	FundPK INT,
	maxDate date
)
CREATE CLUSTERED INDEX indx_tableNAV ON #TableFund (FundPK);

insert into #TableFund(maxDate,FundPK)
SELECT MAX(date),FundPK FROM CloseNAV WHERE
Date <= @MaxDate AND FundPK in (SELECT FundPK FROM Fund WHERE Status IN (1,2))
group by FundPK

insert into #NAV 
SELECT Date,A.FundPK,Nav FROM dbo.CloseNAV A
inner join #TableFund B on A.FundPK = B.FundPK and A.Date = B.maxDate
where A.Status = 2

SELECT  ISNULL(B.SID,'') SID
,ISNULL(B.IFUACode,'') IFUACode
,ISNULL(B.ID,'') FundClientID
,ISNULL(C.ID,'') InternalCategory
,ISNULL(B.Name,'') FundClientName
,ISNULL(D.ID,'') + ' - ' + isnull(D.Name,'')  AgentName
,case when B.EntryUsersID = 'BKLP' then ISNULL(I.BankName,'') 
when (f1.Fundclientpk is null) and F2.FundClientPK is null then ISNULL(E.ID,'')
when F1.FundClientPK is null then
	case when F2.BankRecipientPK = 1 then isnull(B6.ID,'') 
	when F2.BankRecipientPK = 2 then  isnull(B7.ID,'') 
	when F2.BankRecipientPK = 3 then  isnull(B8.ID,'') 
	else  isnull(B5.id,'') 
	end
else 
	case when F1.BankRecipientPK = 1 then isnull(B1.ID,'') 
	when F1.BankRecipientPK = 2 then isnull(B2.ID,'') 
	when F1.BankRecipientPK = 3 then isnull(B3.ID,'') 
	else  isnull(B4.ID,'') 
	end 
end BankName
,case when B.EntryUsersID = 'BKLP' then ISNULL(I.AccountNo,'') 
when (f1.Fundclientpk is null) and F2.FundClientPK is null then ISNULL(B.NomorRekening1,'')
when F1.FundClientPK is null then
	case when F2.BankRecipientPK = 1 then isnull(B.NomorRekening1,'') 
	when F2.BankRecipientPK = 2 then isnull(B.NomorRekening2,'') 
	when F2.BankRecipientPK = 3 then isnull(B.NomorRekening3,'') 
	else isnull(F4.AccountNo,'') 
	end
else 
	case when F1.BankRecipientPK = 1 then isnull(B.NomorRekening1,'') 
	when F1.BankRecipientPK = 2 then isnull(B.NomorRekening2,'') 
	when F1.BankRecipientPK = 3 then isnull(B.NomorRekening3,'') 
	else isnull(F3.AccountNo,'') 
	end 
end BankAccNo
,case when B.EntryUsersID = 'BKLP' then ISNULL(I.AccountName,'') 
when (f1.Fundclientpk is null) and F2.FundClientPK is null then ISNULL(B.NamaNasabah1,'')
when F1.FundClientPK is null then
	case when F2.BankRecipientPK = 1 then isnull(B.NamaNasabah1,'') 
	when F2.BankRecipientPK = 2 then isnull(B.NamaNasabah2,'') 
	when F2.BankRecipientPK = 3 then isnull(B.NamaNasabah3,'') 
	else isnull(F4.AccountName,'') 
	end
else 
	case when F1.BankRecipientPK = 1 then isnull(B.NamaNasabah1,'') 
	when F1.BankRecipientPK = 2 then isnull(B.NamaNasabah2,'') 
	when F1.BankRecipientPK = 3 then isnull(B.NamaNasabah3,'') 
	else isnull(F3.AccountName,'') 
	end 
end BankAccName
,ISNULL(F.ID,'') FundID
,ISNULL(A.UnitAmount,0) * ISNULL(G.NAV,0) Amount
,ISNULL(A.UnitAmount,0) Unit
,ISNULL(G.NAV,0) LastNAV
,ISNULL(G.LastDate,'') LastNAVDate
,ISNULL(B.TempatLahir,'') PlaceOfBirth
,ISNULL(B.TanggalLahir,'') DateOfBirth
,ISNULL(H.DescOne,'') IDType
,ISNULL(B.NoIdentitasInd1,'') IDNo
,ISNULL(B.MotherMaidenName,'') MotherName
,isnull(J.DescOne,'') HighRisk
,isnull(K.DescOne,'') KYCRiskAppetite
,ISNULL(B.EntryTime,'') RegDate
,isnull(B.FrontID,'') OnlineFlag
,ISNULL(B.CantSubs,0) CantSub
,ISNULL(B.CantRedempt,0) CantRed
,ISNULL(B.DatePengkinianData,'') LastKYCUpdate
,ISNULL(B.TeleponSelular,'') Phone
,ISNULL(B.Email,'') Email
,CASE WHEN B.InvestorType = 1 THEN 'INDIVIDUAL' ELSE 'INSTITUSI' END Type


FROM dbo.FundClientPosition A
LEFT JOIN dbo.FundClient B ON A.FundClientPK = B.FundClientPK AND B.status IN (1,2)
LEFT JOIN dbo.InternalCategory C ON B.InternalCategoryPK = C.InternalCategoryPK AND C.status IN (1,2)
LEFT JOIN dbo.Agent D ON B.SellingAgentPK = D.AgentPK AND D.status IN (1,2)
LEFT JOIN FundClientBankDefault F1 on A.FundClientPK = F1.FundClientPK and A.FundPK = F1.FundPK and F1.status in (1,2)
LEFT JOIN FundClientBankDefault F2 on A.FundClientPK = F2.FundClientPK and F2.FundPK = 0 and F2.status in (1,2)
LEFT JOIN Bank B1 on F1.BankRecipientPK = 1 and B.NamaBank1 = B1.BankPK and B1.Status in (1,2)
LEFT JOIN Bank B2 on F1.BankRecipientPK = 2 and B.NamaBank2 = B2.BankPK and B2.Status in (1,2)
LEFT JOIN Bank B3 on F1.BankRecipientPK = 3 and B.NamaBank3 = B3.BankPK and B3.Status in (1,2)
LEFT JOIN FundClientBankList F3 on F1.BankRecipientPK = F3.NoBank and F1.FundClientPK = F3.FundClientPK and F3.status in (1,2)
LEFT JOIN Bank B4 on F3.BankPK = B4.BankPK and B4.Status in (1,2)
LEFT JOIN FundClientBankList F4 on F2.BankRecipientPK = F4.NoBank and F2.FundClientPK = F4.FundClientPK and F4.status in (1,2)
LEFT JOIN Bank B5 on F4.BankPK = B5.BankPK and B5.Status in (1,2)
LEFT JOIN Bank B6 on F2.BankRecipientPK = 1 and B.NamaBank1 = B6.BankPK and B6.Status in (1,2)
LEFT JOIN Bank B7 on F2.BankRecipientPK = 2 and B.NamaBank2 = B7.BankPK and B7.Status in (1,2)
LEFT JOIN Bank B8 on F2.BankRecipientPK = 3 and B.NamaBank3 = B8.BankPK and B8.Status in (1,2)
LEFT JOIN Bank E ON B.NamaBank1 = E.bankPK AND E.status IN (1,2)
LEFT JOIN Fund F ON A.FundPK = F.FundPK AND F.status IN (1,2)
LEFT JOIN #NAV G ON A.FundPK = G.FundPK --and A.Date = G.LastDate
LEFT JOIN dbo.MasterValue H ON B.IdentitasInd1 = H.Code AND H.id = 'Identity' AND H.status IN (1,2)
LEFT JOIN dbo.ZRDO_80_BANK I on B.NamaBank1 = I.BankID and I.status = 2
LEFT JOIN MasterValue J on B.KYCRiskProfile = J.Code and J.ID = 'KYCRiskProfile' and J.Status in (1,2)
LEFT JOIN MasterValue K on B.investorsRiskProfile = K.Code and K.ID = 'InvestorsRiskProfile' and K.Status in (1,2)
WHERE A.Date = @MaxDate
--group by B.SID,B.IFUACode,B.ID,C.ID,B.Name,D.ID,D.Name,E.ID,B.EntryUsersID,I.BankName,F1.FundClientPK,F2.FundClientPK,F2.BankRecipientPK,
--B1.ID,F1.BankRecipientPK,B2.ID,B3.ID,B4.ID,B5.ID,B6.ID,B7.ID,B8.ID,I.AccountNo,B.NomorRekening1,B.NomorRekening2,B.NomorRekening3,F4.AccountNo,F3.AccountNo,
--B.NamaNasabah1,B.NamaNasabah2,B.NamaNasabah3,F4.AccountName,F3.AccountName,I.AccountName,F.ID,A.UnitAmount,G.NAV,G.LastDate,B.TempatLahir,B.TanggalLahir,
--H.DescOne,B.NoIdentitasInd1,B.MotherMaidenName,J.DescOne,K.DescOne,B.EntryTime,B.FrontID,B.CantSubs,B.CantRedempt,B.DatePengkinianData,B.TeleponSelular,
--B.Email,B.InvestorType


UNION ALL

SELECT  ISNULL(B.SID,'') SID
,ISNULL(B.IFUACode,'') IFUACode
,ISNULL(B.ID,'') FundClientID
,ISNULL(C.ID,'') InternalCategory
,ISNULL(B.Name,'') FundClientName
,ISNULL(D.ID,'') + ' - ' + isnull(D.Name,'') AgentName
,case when B.EntryUsersID = 'BKLP' then ISNULL(I.BankName,'') else ISNULL(E.ID,'') end BankName
,case when B.EntryUsersID = 'BKLP' then ISNULL(I.AccountNo,'') else ISNULL(B.NomorRekening1,'') end BankAccNo
,case when B.EntryUsersID = 'BKLP' then ISNULL(I.AccountName,'') else ISNULL(B.NamaNasabah1,'') end  BankAccName
,'' FundID
,0 Amount
,0 Unit
,0 LastNAV
,'' LastNAVDate
,ISNULL(B.TempatLahir,'') PlaceOfBirth
,ISNULL(B.TanggalLahir,'') DateOfBirth
,ISNULL(H.DescOne,'') IDType
,ISNULL(B.NoIdentitasInd1,'') IDNo
,ISNULL(B.MotherMaidenName,'') MotherName
,isnull(J.DescOne,'') HighRisk
,isnull(K.DescOne,'') KYCRiskAppetite
,ISNULL(B.EntryTime,'') RegDate
,isnull(B.FrontID,'') OnlineFlag
,ISNULL(B.CantSubs,0) CantSub
,ISNULL(B.CantRedempt,0) CantRed
,CASE when ISNULL(B.DatePengkinianData,'') = '' THEN ' ' ELSE B.DatePengkinianData END  LastKYCUpdate
,ISNULL(B.TeleponSelular,'') Phone
,ISNULL(B.Email,'') Email
,CASE WHEN B.InvestorType = 1 THEN 'INDIVIDUAL' ELSE 'INSTITUSI' END Type


FROM dbo.FundClient B 
LEFT JOIN dbo.InternalCategory C ON B.InternalCategoryPK = C.InternalCategoryPK AND C.status IN (1,2)
LEFT JOIN dbo.Agent D ON B.SellingAgentPK = D.AgentPK AND D.status IN (1,2)
LEFT JOIN Bank E ON B.NamaBank1 = E.bankPK AND E.status IN (1,2)
LEFT JOIN dbo.MasterValue H ON B.IdentitasInd1 = H.Code AND H.id = 'Identity' AND H.status IN (1,2)
LEFT JOIN dbo.ZRDO_80_BANK I on B.NamaBank1 = I.BankID and I.status = 2
LEFT JOIN MasterValue J on B.KYCRiskProfile = J.Code and J.ID = 'KYCRiskProfile' and J.Status in (1,2)
LEFT JOIN MasterValue K on B.investorsRiskProfile = K.Code and K.ID = 'InvestorsRiskProfile' and K.Status in (1,2)
WHERE  B.status in (1,2)
and B.FundClientPK 
not in (select distinct FundClientPK from FundClientPosition where Date = @MaxDate) 


                        ";


                        cmd.Parameters.AddWithValue("@Date", _date);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_CustomerDashboard.Add(setCustomerDashboard(dr));
                                }
                            }
                            return L_CustomerDashboard;
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