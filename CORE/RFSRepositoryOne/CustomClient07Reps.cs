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
    public class CustomClient07Reps
    {
        Host _host = new Host();

        string _insertCommandRedemption = "INSERT INTO [dbo].[ClientRedemption] " +
                            "([ClientRedemptionPK],[HistoryPK],[Status],[NAVDate],[ValueDate],[PaymentDate]," +
                            " [NAV],[FundPK],[FundClientPK],[CashRefPK],[CurrencyPK],[Type],[BitRedemptionAll],[Description],[CashAmount],[UnitAmount],[TotalCashAmount],[TotalUnitAmount]," +
                            " [RedemptionFeePercent],[RedemptionFeeAmount],[AgentPK],[AgentFeePercent],[AgentFeeAmount],[UnitPosition],[BankRecipientPK],[TransferType],[FeeType],[ReferenceSInvest],";
        string _paramaterCommandRedemption = "@NAVDate,@ValueDate,@PaymentDate,@NAV,@FundPK,@FundClientPK,@CashRefPK,@CurrencyPK,Type=@Type," +
                            "@BitRedemptionAll,@Description,@CashAmount,@UnitAmount,@TotalCashAmount,@TotalUnitAmount,@RedemptionFeePercent,@RedemptionFeeAmount,@AgentPK,@AgentFeePercent,@AgentFeeAmount,@UnitPosition,@BankRecipientPK,@TransferType,@FeeType,@ReferenceSInvest,";

        string _insertCommandSubscription = "INSERT INTO [dbo].[ClientSubscription] " +
                            "([ClientSubscriptionPK],[HistoryPK],[Status],[NAVDate],[ValueDate]," +
                            " [NAV],[FundPK],[FundClientPK],[CashRefPK],[CurrencyPK],[Description],[CashAmount],[UnitAmount],[TotalCashAmount],[TotalUnitAmount]," +
                            " [SubscriptionFeePercent],[SubscriptionFeeAmount],[AgentPK],[AgentFeePercent],[AgentFeeAmount],[Type],[BitImmediateTransaction],[FeeType],[ReferenceSInvest],[Tenor],[InterestRate],[PaymentTerm],[SumberDana],[TransactionPromoPK],";
        string _paramaterCommandSubscription = "@NAVDate,@ValueDate,@NAV,@FundPK,@FundClientPK,@CashRefPK,@CurrencyPK," +
                            "@Description,@CashAmount,@UnitAmount,@TotalCashAmount,@TotalUnitAmount,@SubscriptionFeePercent,@SubscriptionFeeAmount,@AgentPK,@AgentFeePercent,@AgentFeeAmount,@Type,@BitImmediateTransaction,@FeeType,@ReferenceSInvest,@Tenor,@InterestRate,@PaymentTerm,@SumberDana,@TransactionPromoPK,";

        string _insertCommandSwitching = "INSERT INTO [dbo].[ClientSwitching] " +
                          "([ClientSwitchingPK],[HistoryPK],[Status],[NAVDate],[ValueDate],[PaymentDate]," +
                          "[NAVFundFrom],[NAVFundTo],[FundPKFrom],[FundPKTo],[FundClientPK],[CashRefPKFrom],[CashRefPKTo],[CurrencyPK],[Description],[CashAmount],[UnitAmount],[SwitchingFeePercent],[SwitchingFeeAmount],[TotalCashAmountFundFrom],[TotalCashAmountFundTo],[TotalUnitAmountFundFrom], " +
                          "[TotalUnitAmountFundTo],[BitSwitchingAll],[TransferType],[FeeType],[FeeTypeMethod],[ReferenceSInvest],[AgentPK],[Type],";
        string _paramaterCommandSwitching = "@NAVDate,@ValueDate,@PaymentDate,@NAVFundFrom,@NAVFundTo,@FundPKFrom,@FundPKTo,@FundClientPK,@CashRefPKFrom,@CashRefPKTo,@CurrencyPK," +
                            "@Description,@CashAmount,@UnitAmount,@SwitchingFeePercent,@SwitchingFeeAmount,@TotalCashAmountFundFrom,@TotalCashAmountFundTo,@TotalUnitAmountFundFrom,@TotalUnitAmountFundTo,@BitSwitchingAll,@TransferType,@FeeType,@FeeTypeMethod,@ReferenceSInvest,@AgentPK,@Type,";


        public bool FundClient_SInvest(string _userID, string _category, int _fundClientPKFrom, int _fundClientPKTo, FundClient _FundClient)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        string paramFundClientSelected = "";
                        if (_FundClient.FundClientSelected == "" || _FundClient.FundClientSelected == "0")
                        {
                            paramFundClientSelected = "";
                        }
                        else
                        {
                            paramFundClientSelected = "and FC.FundClientPK in (" + _FundClient.FundClientSelected + ") ";
                        }

                        string _paramFundClientPK = "";
                        if (_fundClientPKFrom == 0 || _fundClientPKTo == 0)
                        {
                            _paramFundClientPK = "";
                        }
                        else
                        {
                            _paramFundClientPK = " And FC.FundClientPK Between " + _fundClientPKFrom + @" and " + _fundClientPKTo;
                        }

                        if (_category == "1")
                        {

                            cmd.CommandText = @"

BEGIN  
SET NOCOUNT ON         
select '1'  
+'|' + @CompanyID    
+ '|' + ''  
+ '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(NamaDepanInd,''))))      
+ '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(NamaTengahInd,''))))      
+ '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(NamaBelakangInd,''))))   
+ '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(nationality,''))))  
+ '|' + (isnull(NoIdentitasInd1,''))
+ '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull((case when IdentitasInd1 = 7 then '99981231' else case when CONVERT(VARCHAR(10), ExpiredDateIdentitasInd1, 112) = '19000101' or CONVERT(VARCHAR(10), ExpiredDateIdentitasInd1, 112) < '20160802' then '' else CONVERT(VARCHAR(10), ExpiredDateIdentitasInd1, 112) end End),''))))          
+ '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(NPWP,''))))   
+ '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull((case when CONVERT(VARCHAR(10), RegistrationNPWP, 112) <> '19000101' then CONVERT(VARCHAR(10), RegistrationNPWP, 112) else '' End),''))))          
+ '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(Countryofbirth,''))))    
+ '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(TempatLahir,''))))   
+ '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull((case when CONVERT(VARCHAR(10), TanggalLahir, 112) <> '19000101' then CONVERT(VARCHAR(10), TanggalLahir, 112) else '' End),''))))   
+ '|' + case when JenisKelamin = '0' then '' else isnull(cast(JenisKelamin as nvarchar(1)),'') end 
+ '|' + case when Pendidikan = '0' then '' else isnull(cast(Pendidikan as nvarchar(1)),'') end  
+ '|' + case when mothermaidenname = '0' then '' else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(mothermaidenname ,'')))) end      
+ '|' + case when Agama = '0' then '' else isnull(cast(Agama as nvarchar(1)),'') end  
+ '|' + case when Pekerjaan = '0' then '' else isnull(cast(Pekerjaan as nvarchar(1)),'') end    
+ '|' + case when PenghasilanInd = '0' then '' else isnull(cast(PenghasilanInd as nvarchar(1)),'') end   
+ '|' + case when StatusPerkawinan = '0' then '' else isnull(cast(StatusPerkawinan as nvarchar(1)),'') end   
+ '|' + case when SpouseName = '0' then '' else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(SpouseName ,'')))) end      
+ '|' + case when InvestorsRiskProfile = '0' then '' else isnull(cast(InvestorsRiskProfile as nvarchar(1)),'') end  
+ '|' + case when MaksudTujuanInd = '0' then '' else isnull(cast(MaksudTujuanInd as nvarchar(1)),'') end   
+ '|' + case when SumberDanaInd = '0' then '' else isnull(cast(SumberDanaInd as nvarchar(2)),'') end   
+ '|' + case when AssetOwner = '0' then '' else isnull(cast(AssetOwner as nvarchar(1)),'') end  
+ '|' + case when OtherAlamatInd1 = '0' then '' else REPLACE(REPLACE(RTRIM(LTRIM(isnull(OtherAlamatInd1 + ' RT: ' + isnull(Identity1RT,' ') + ' RW: ' + isnull(Identity1RW ,'') ,''))),char(13),''),char(10),'') end
+ '|' + case when OtherKodeKotaInd1 = '0' then '' else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(cast(OtherKodeKotaInd1 as nvarchar(4)),'')))) end     
+ '|' + case when OtherKodePosInd1 = '0' then '' else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(OtherKodePosInd1 ,'')))) end      
+ '|' + case when AlamatInd1 = '0' then '' else   REPLACE(REPLACE(RTRIM(LTRIM(isnull(AlamatInd1 + ' RT: ' + isnull(CorrespondenceRT,' ') + ' RW: ' + isnull(CorrespondenceRW,''),''))),char(13),''),char(10),'') end      
+ '|' + case when KodeKotaInd1 = '0' then '' else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(cast(KodeKotaInd1 as nvarchar(4)),'')))) end  
+ '|' + isnull(MV13.DescOne,'')                                    
+ '|' + case when KodePosInd1 = '0' then '' else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(KodePosInd1 ,'')))) end    
+ '|' + isnull(CountryofCorrespondence,'')  
+ '|' + case when AlamatInd2 = '0' then '' else REPLACE(REPLACE(RTRIM(LTRIM(isnull(AlamatInd2 + ' RT: ' + isnull(DomicileRT,'') + ' RW: ' + isnull(DomicileRW,''),''))),char(13),''),char(10),'') end   
+ '|' + case when KodeKotaInd2 = '0' then '' else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(cast(KodeKotaInd2 as nvarchar(4)),'')))) end  
+ '|' + isnull(MV14.DescOne,'')                                     
+ '|' + case when KodePosInd2 = '0' then '' else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(KodePosInd2 ,'')))) end   
+ '|' + isnull(CountryofDomicile,'') 
+ '|' + case when TeleponRumah = '0' then '' else isnull(TeleponRumah ,'') end    
+ '|' + case when TeleponSelular = '0' then '' else isnull(TeleponSelular ,'') end    
+ '|' + case when fc.Fax = '0' then '' else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(fc.Fax ,'')))) end     
+ '|' + case when fc.Email = '0' then '' else isnull(fc.Email,'') end     
+ '|' + case when StatementType = '0' then '' else isnull(cast(StatementType as nvarchar(1)),'') end    
+ '|' + case when FATCA = '0' then '' else isnull(cast(FATCA as nvarchar(1)),'') end   
+ '|' + case when TIN = '0' then '' else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(TIN ,'')))) end    
+ '|' + case when TINIssuanceCountry = '0' then '' else isnull(cast(TINIssuanceCountry as nvarchar(2)),'') end                                   
+ '|' +  case when B1.Country = 'ID' then '' else isnull(B1.SInvestID,'') end  
+ '|' + case when B1.Country = 'ID' then isnull(B1.BICode,'') else '' end                           
+ '|' + isnull(B1.Name,'') 
+ '|' + isnull(B1.Country,'') 
+ '|' + case when BankBranchName1 = '0' then '' else isnull(cast(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(BankBranchName1 ,'')))) as nvarchar(100)),'') end 
+ '|' + isnull(MV15.DescOne,'') 
+ '|' + case when NomorRekening1 = '0' then '' else isnull(cast(NomorRekening1 as nvarchar(50)),'') end
+ '|' + case when NamaNasabah1 = '0' then '' else isnull(cast(NamaNasabah1 as nvarchar(100)),'') end 
+ '|' +  case when B2.Country = 'ID' then '' else isnull(B2.SInvestID,'') end  
+ '|' + case when B2.Country = 'ID' then isnull(B2.BICode,'') else '' end   
+ '|' + isnull(B2.Name,'') 
+ '|' + isnull(B2.Country,'') 
+ '|' + case when BankBranchName2 = '0' then '' else isnull(cast(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(BankBranchName2 ,'')))) as nvarchar(100)),'') end 
+ '|' + isnull(MV16.DescOne,'') 
+ '|' + case when NomorRekening2 = '0' then '' else isnull(cast(NomorRekening2 as nvarchar(50)),'') end 
+ '|' + case when NamaNasabah2 = '0' then '' else isnull(cast(NamaNasabah2 as nvarchar(100)),'') end   
+ '|' +  case when B3.Country = 'ID' then '' else isnull(B3.SInvestID,'') end  
+ '|' + case when B3.Country = 'ID' then isnull(B3.BICode,'') else '' end   
+ '|' + isnull(B3.Name,'') 
+ '|' + isnull(B3.Country,'') 
+ '|' + case when BankBranchName3 = '0' then '' else isnull(cast(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(BankBranchName3 ,'')))) as nvarchar(100)),'') end 
+ '|' + isnull(MV17.DescOne,'') 
+ '|' + case when NomorRekening3 = '0' then '' else isnull(cast(NomorRekening3 as nvarchar(50)),'') end 
+ '|' + case when NamaNasabah3 = '0' then '' else isnull(cast(NamaNasabah3 as nvarchar(100)),'') end                                      
+ '|' + isnull(FC.ID,'') ResultText                                     
from fundclient FC left join Agent A on FC.SellingAgentPK = A.AgentPK and A.Status = 2   
left join MasterValue MV3 on FC.JenisKelamin = MV3.Code and MV3.status =2  and MV3.ID ='Sex'   
left join MasterValue MV4 on FC.Pendidikan = MV4.Code and MV4.status =2  and MV4.ID ='EducationalBackground'   
left join MasterValue MV5 on FC.Agama = MV5.Code and MV5.status =2  and MV5.ID ='Religion'   
left join MasterValue MV6 on FC.Pekerjaan = MV6.Code and MV6.status =2  and MV6.ID ='Occupation'   
left join MasterValue MV7 on FC.PenghasilanInd = MV7.Code and MV7.status =2  and MV7.ID ='IncomeInd'   
left join MasterValue MV8 on FC.StatusPerkawinan = MV8.Code and MV8.status =2  and MV8.ID ='MaritalStatus'   
left join MasterValue MV9 on FC.InvestorsRiskProfile = MV9.Code and MV9.status =2  and MV9.ID ='InvestorsRiskProfile'  
left join MasterValue MV10 on FC.MaksudTujuanInd = MV10.Code and MV10.status =2  and MV10.ID ='InvestmentObjectivesIND'   
left join MasterValue MV11 on FC.SumberDanaInd = MV11.Code and MV11.status =2  and MV11.ID ='IncomeSourceIND'   
left join MasterValue MV12 on FC.AssetOwner = MV12.Code and MV12.status =2  and MV12.ID ='AssetOwner'   
left join MasterValue MV13 on FC.KodeKotaInd1 = MV13.Code and MV13.status =2  and MV13.ID ='CityRHB'   
left join MasterValue MV14 on FC.KodeKotaInd2 = MV14.Code and MV14.status =2  and MV14.ID ='CityRHB'   
left join MasterValue MV15 on FC.MataUang1 = MV15.Code and MV15.status =2  and MV15.ID ='MataUang'   
left join MasterValue MV16 on FC.MataUang2 = MV16.Code and MV16.status =2  and MV16.ID ='MataUang'   
left join MasterValue MV17 on FC.MataUang3 = MV17.Code and MV17.status =2  and MV17.ID ='MataUang' 
left join Bank B1 on fc.NamaBank1 = B1.BankPK and B1.status = 2   
left join Bank B2 on fc.NamaBank2 = B2.BankPK and B2.status = 2   
left join Bank B3 on fc.NamaBank3 = B3.BankPK and B3.status = 2 
where FC.Status = 2 and FC.InvestorType = 1 " + paramFundClientSelected + @"  and rtrim(ltrim(FC.IfuaCode)) = '' and rtrim(ltrim(FC.SID)) = ''
" + _paramFundClientPK + @" 
order by FC.name asc 

 END  ";
                            cmd.Parameters.AddWithValue("@CompanyID", _host.Get_CompanyID());
                            using (SqlDataReader dr1 = cmd.ExecuteReader())
                            {
                                if (dr1.HasRows)
                                {
                                    string filePath;
                                    filePath = Tools.SInvestTextPath + "SInvestIndividuTxtVersion.txt";
                                    FileInfo txtFile = new FileInfo(filePath);
                                    if (txtFile.Exists)
                                    {
                                        txtFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                    }

                                    using (System.IO.StreamWriter file = new System.IO.StreamWriter(filePath))
                                    {
                                        file.WriteLine("");
                                        while (dr1.Read())
                                        {

                                            file.WriteLine(Convert.ToString(dr1["ResultText"]));
                                        }
                                        return true;
                                    }
                                }
                            }
                        }
                        else
                        {
                            cmd.CommandText = @"
                                   
                    BEGIN 
                      select '1'  
+'|' + @CompanyID     
+'|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(SID,''))))    
+'|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(FC.Name,''))))       
+'|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(Negara,''))))  
+'|' + RTRIM(LTRIM(case when NomorSIUP = '0' then '' else isnull(cast(NomorSIUP as nvarchar(40)),'') end)) 
+'|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(case when CONVERT(VARCHAR(10), SIUPExpirationDate, 112) <> '19000101' then CONVERT(VARCHAR(10), SIUPExpirationDate, 112) else '' End))) 
+'|' + case when NoSKD = '0' then '' else RTRIM(LTRIM(isnull(cast(NoSKD as nvarchar(20)),''))) end
+'|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(case when CONVERT(VARCHAR(10), ExpiredDateSKD, 112) <> '19000101' then CONVERT(VARCHAR(10), ExpiredDateSKD, 112) else '' End))) 
+'|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(NPWP,'')))) 
+'|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(case when CONVERT(VARCHAR(10), RegistrationNPWP, 112) <> '19000101' then CONVERT(VARCHAR(10), RegistrationNPWP, 112) else '' End)))
+'|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(CountryofEstablishment,''))))  
+'|' + case when LokasiBerdiri = '0' then '' else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(LokasiBerdiri ,'')))) end  
+'|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull((case when CONVERT(VARCHAR(10), TanggalBerdiri, 112) <> '19000101' then CONVERT(VARCHAR(10), TanggalBerdiri, 112) else '' End),''))))         
+'|' + RTRIM(LTRIM((isnull(NomorAnggaran,''))))
+'|' + case when Tipe = '0' then '' else isnull(cast(Tipe as nvarchar(1)),'') end 
+'|' + case when Karakteristik = '0' then '' else isnull(cast(Karakteristik as nvarchar(1)),'') end 
+'|' + case when PenghasilanInstitusi = '0' then '' else isnull(cast(PenghasilanInstitusi as nvarchar(1)),'') end 
+'|' + case when InvestorsRiskProfile = '0' then '' else isnull(cast(InvestorsRiskProfile as nvarchar(1)),'') end 
+'|' + case when MaksudTujuanInstitusi = '0' then '' else isnull(cast(MaksudTujuanInstitusi as nvarchar(1)),'') end 
+'|' + case when SumberDanaInstitusi = '0' then '' else isnull(cast(SumberDanaInstitusi as nvarchar(1)),'') end 
+'|' + case when AssetOwner = '0' then '' else isnull(cast(AssetOwner as nvarchar(1)),'') end  
+'|' + case when AlamatPerusahaan = '0' then '' else REPLACE(REPLACE(RTRIM(LTRIM(isnull(AlamatPerusahaan ,''))),char(13),''),char(10),'') end   
+'|' + case when CompanyCityName = '0' then '' else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(cast(CompanyCityName as nvarchar(4)),'')))) end    
+'|' + isnull(MV18.DescOne,'')                                      
+'|' + case when KodePosIns = '0' then '' else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(KodePosIns ,'')))) end  
+'|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(CountryOfCompany,''))))   
+'|' + case when TeleponBisnis = '0' then '' else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(TeleponBisnis ,'')))) end    
+'|' + case when FC.Companyfax = '0' then '' else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(FC.Companyfax ,'')))) end    
+'|' + case when fc.CompanyMail = '0' then '' else isnull(fc.CompanyMail,'') end     
+'|' + case when StatementType = '0' then '' else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(StatementType ,'')))) end   
+'|' + case when NamaDepanIns1 = '0' then '' else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(NamaDepanIns1 ,'')))) end   
+'|' + case when NamaTengahIns1 = '0' then '' else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(NamaTengahIns1 ,'')))) end  
+'|' + case when NamaBelakangIns1 = '0' then '' else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(NamaBelakangIns1 ,'')))) end  
+'|' + case when Jabatan1 = '0' then '' else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(Jabatan1 ,'')))) end   
+'|' + case when fc.PhoneIns1 = '0' then '' else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(fc.PhoneIns1 ,'')))) end   
+'|' + case when fc.EmailIns1 = '0' then '' else isnull(fc.EmailIns1,'') end    
+'|' +  
+'|' + case when fc.NoIdentitasIns11 = '0' then '' else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(fc.NoIdentitasIns11 ,'')))) end   
+'|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull((case when CONVERT(VARCHAR(10), ExpiredDateIdentitasIns11, 112) <> '19000101' then CONVERT(VARCHAR(10), ExpiredDateIdentitasIns11, 112) else '' End),''))))  
+'|' +  
+'|' +  
+'|' + case when NamaDepanIns2 = '0' then '' else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(NamaDepanIns2 ,'')))) end   
+'|' + case when NamaTengahIns2 = '0' then '' else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(NamaTengahIns2 ,'')))) end  
+'|' + case when NamaBelakangIns2 = '0' then '' else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(NamaBelakangIns2 ,'')))) end  
+'|' + case when Jabatan2 = '0' then '' else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(Jabatan2 ,'')))) end   
+'|' + case when fc.PhoneIns2 = '0' then '' else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(fc.PhoneIns2 ,'')))) end   
+'|' + case when fc.EmailIns2 = '0' then '' else isnull(fc.EmailIns2,'') end   
+'|' +  
+'|' + case when fc.NoIdentitasIns21 = '0' then '' else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(fc.NoIdentitasIns21 ,'')))) end   
+'|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull((case when CONVERT(VARCHAR(10), ExpiredDateIdentitasIns21, 112) <> '19000101' then CONVERT(VARCHAR(10), ExpiredDateIdentitasIns21, 112) else '' End),''))))   
+'|' +  
+'|' +  
+'|' + case when AssetFor1Year = '0' then '' else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(AssetFor1Year ,'')))) end  
+'|' + case when AssetFor2Year = '0' then '' else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(AssetFor2Year ,'')))) end   
+'|' + case when AssetFor3Year = '0' then '' else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(AssetFor3Year ,'')))) end 
+'|' + case when OperatingProfitFor1Year = '0' then '' else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(OperatingProfitFor1Year ,'')))) end   
+'|' + case when OperatingProfitFor2Year = '0' then '' else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(OperatingProfitFor2Year ,'')))) end   
+'|' + case when OperatingProfitFor3Year = '0' then '' else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(OperatingProfitFor3Year ,'')))) end 
+'|' + case when FATCA = '0' then '' else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(FATCA ,'')))) end 
+'|' + case when TIN = '0' then '' else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(TIN ,'')))) end  
+'|' + case when TINIssuanceCountry = '0' then '' else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(TINIssuanceCountry ,'')))) end  
+'|' + case when GIIN = '0' then '' else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(GIIN ,'')))) end   
+'|' + case when SubstantialOwnerName = '0' then '' else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(SubstantialOwnerName ,'')))) end    
+'|' + case when SubstantialOwnerAddress = '0' then '' else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(SubstantialOwnerAddress ,'')))) end    
+'|' + case when SubstantialOwnerTIN = '0' then '' else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(SubstantialOwnerTIN ,'')))) end                                   
+ '|' + ''
+ '|' + isnull(B1.BICode,'')                        
+ '|' + isnull(B1.Name,'') 
+ '|' + isnull(B1.Country,'') 
+ '|' + case when BankBranchName1 = '0' then '' else isnull(cast(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(BankBranchName1,''))))    as nvarchar(100)),'') end 
+ '|' + isnull(MV15.DescOne,'') 
+ '|' + case when NomorRekening1 = '0' then '' else isnull(cast(NomorRekening1 as nvarchar(30)),'') end
+ '|' + case when NamaNasabah1 = '0' then '' else isnull(cast(NamaNasabah1 as nvarchar(100)),'') end 
+ '|' +  ''
+ '|' + isnull(B2.BICode,'')                        
+ '|' + isnull(B2.Name,'') 
+ '|' + isnull(B2.Country,'') 
+ '|' + case when BankBranchName2 = '0' then '' else isnull(cast(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(BankBranchName2,''))))    as nvarchar(100)),'') end 
+ '|' + isnull(MV16.DescOne,'') 
+ '|' + case when NomorRekening2 = '0' then '' else isnull(cast(NomorRekening2 as nvarchar(30)),'') end 
+ '|' + case when NamaNasabah2 = '0' then '' else isnull(cast(NamaNasabah2 as nvarchar(100)),'') end   
+ '|' + ''
+ '|' + isnull(B3.BICode,'')                        
+ '|' + isnull(B3.Name,'') 
+ '|' + isnull(B3.Country,'') 
+ '|' + case when BankBranchName3 = '0' then '' else isnull(cast(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(BankBranchName3,''))))    as nvarchar(100)),'') end 
+ '|' + isnull(MV17.DescOne,'') 
+ '|' + case when NomorRekening3 = '0' then '' else isnull(cast(NomorRekening3 as nvarchar(30)),'') end 
+ '|' + case when NamaNasabah3 = '0' then '' else isnull(cast(NamaNasabah3 as nvarchar(100)),'') end                                      
+ '|' + isnull(FC.ID,'')   ResultText
from fundclient FC left join Agent A on FC.SellingAgentPK = A.AgentPK and A.Status = 2   
left join MasterValue MV3 on FC.Tipe = MV3.Code and MV3.status =2  and MV3.ID ='CompanyType'   
left join MasterValue MV4 on FC.Karakteristik = MV4.Code and MV4.status =2  and MV4.ID ='CompanyCharacteristic'   
left join MasterValue MV5 on FC.PenghasilanInstitusi = MV5.Code and MV5.status =2  and MV5.ID ='IncomeINS'   
left join MasterValue MV6 on FC.InvestorsRiskProfile = MV6.Code and MV6.status =2  and MV6.ID ='InvestorsRiskProfile'   
left join MasterValue MV7 on FC.MaksudTujuanInstitusi = MV7.Code and MV7.status =2  and MV7.ID ='InvestmentObjectivesINS'  
left join MasterValue MV8 on FC.SumberDanaInstitusi = MV8.Code and MV8.status =2  and MV8.ID ='IncomeSourceINS'   
left join MasterValue MV9 on FC.AssetOwner = MV9.Code and MV9.status =2  and MV9.ID ='AssetOwner'   
left join MasterValue MV10 on FC.KodeKotaIns = MV10.Code and MV10.status =2  and MV10.ID ='CityRHB'   
left join MasterValue MV15 on FC.MataUang1 = MV15.Code and MV15.status =2  and MV15.ID ='MataUang'   
left join MasterValue MV16 on FC.MataUang2 = MV16.Code and MV16.status =2  and MV16.ID ='MataUang'   
left join MasterValue MV17 on FC.MataUang3 = MV17.Code and MV17.status =2  and MV17.ID ='MataUang'   
left join MasterValue MV18 on CompanyCityName = MV18.Code and MV18.status =2  and MV18.ID ='CityRHB'  
left join Bank B1 on fc.NamaBank1 = B1.BankPK and B1.status = 2   
left join Bank B2 on fc.NamaBank2 = B2.BankPK and B2.status = 2   
left join Bank B3 on fc.NamaBank3 = B3.BankPK and B3.status = 2 
where FC.Status = 2 and FC.InvestorType = 2 " + paramFundClientSelected + @" and FC.IfuaCode = '' and FC.SID = ''
" + _paramFundClientPK + @" 
order by FC.name asc  
                        END ";

                            cmd.Parameters.AddWithValue("@CompanyID", _host.Get_CompanyID());
                            using (SqlDataReader dr1 = cmd.ExecuteReader())
                            {
                                if (dr1.HasRows)
                                {
                                    if (dr1.HasRows)
                                    {
                                        string filePath;
                                        filePath = Tools.SInvestTextPath + "SInvestInstitusiTxtVersion.txt";
                                        FileInfo txtFile = new FileInfo(filePath);
                                        if (txtFile.Exists)
                                        {
                                            txtFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                        }

                                        using (System.IO.StreamWriter file = new System.IO.StreamWriter(filePath))
                                        {
                                            file.WriteLine("");
                                            while (dr1.Read())
                                            {
                                                file.WriteLine(Convert.ToString(dr1["ResultText"]));
                                            }
                                            return true;
                                        }
                                    }
                                    return false;
                                }
                            }
                        }
                        return false;

                    }

                }
            }
            catch (Exception err)
            {
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
                                    select ''   
                                    insert into #Text         
                                    Select  RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(CONVERT(VARCHAR(10), ValueDate, 112),''))))
                                    + '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(A.Type,''))))
                                    + '|' + @CompanyID
                                    + '|' + isnull(A.IFUACode,'')
                                    + '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(A.SInvestCode,''))))
                                    + '|' + case when A.CashAmount = 0 then '' else cast(isnull(cast(A.CashAmount as decimal(22,2)),'')as nvarchar) end -- TotalCashAmount
                                    + '|' + case when A.TotalUnitAmount = 0 then '' else cast(isnull(Round(A.TotalUnitAmount,4),'')as nvarchar) end
                                    + '|' + 
                                    + '|' + case when A.FeeAmount = 0 then '' else cast(isnull(cast(A.FeeAmount as decimal(22,2)),'')as nvarchar) end -- 0
                                    + '|' + 
                                    + '|' + 
                                    + '|' + 
                                    + '|' + 
                                    + '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(A.BICode,''))))
                                    + '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(A.AccNo,''))))
                                    + '|' +        
                                    + '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(A.TransferType,''))))
                                    + '|' + RTRIM(LTRIM(isnull(A.Reference,'')))
                                    from (      
                                     Select CS.ValueDate,F.SInvestCode, '' SettlementDate,CS.SubscriptionFeePercent FeePercent,CS.SubscriptionFeeAmount FeeAmount,'1' Type,
                                    ROUND(CS.CashAmount,2)CashAmount ,ROUND(CS.TotalUnitAmount,4)TotalUnitAmount ,'' BICode, '' AccNo ,'' TransferType, 
                                    Case When CS.ReferenceSInvest <> '' and CS.ReferenceSInvest is not null then CS.ReferenceSInvest  
                                    else case when cs.EntryUsersID = 'RDO' and B.TransactionID is null  then 'VA_SUBS_' + cast(ClientSubscriptionPK as nvarchar) 
                                    else  cast(ClientSubscriptionPK as nvarchar)   end end Reference
                                    ,FC.IFUACode  
                                    from ClientSubscription CS left join Fund F on Cs.FundPK = F.fundPK and f.Status in (1,2)     
                                    left join FundClient FC on CS.FundClientPK = FC.FundClientPK and fc.Status in (1,2)
                                    left join ModoitTransaction A on CS.TransactionPK = A.TransactionPK
                                    left join FundClientVerification B on A.MasterTransactionPK = B.TransactionID
                                    where    
                                    CS.ValueDate =  @ValueDate and " + paramClientSubscriptionSelected + @"  and Cs.status = 2
                                    )A    
                                    Group by A.ValueDate,A.SInvestCode,A.FeePercent,A.BICode,A.AccNo,A.SettlementDate,A.FeeAmount,A.Type,A.CashAmount,A.TotalUnitAmount,A.TransferType,A.Reference,A.IFUACode
                                    order by A.ValueDate Asc
                                    select * from #text          
                                    END ";
                        cmd.Parameters.AddWithValue("@CompanyID", _host.Get_CompanyID());
                        cmd.Parameters.AddWithValue("@ValueDate", _dateFrom);



                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                string filePath = Tools.SInvestTextPath + "SUBS_REDM_SWTC_Order_Upload_SUBS_Order.rad";
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
                                    return Tools.HtmlSinvestTextPath + "SUBS_REDM_SWTC_Order_Upload_SUBS_Order.rad";
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


                        set @ManagementFeeExpenseAmount = @ManagementFeeAmount
                        set @TaxARManagementFeeAmount = 0.02 * @ManagementFeeAmount
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


                        set @ManagementFeeExpenseAmount = @ManagementFeeAmount
                        set @TaxARManagementFeeAmount = 0.02 * @ManagementFeeAmount
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

        //redemption
        public int ClientRedemption_Add(ClientRedemption _clientRedemption, bool _havePrivillege)
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
                            cmd.CommandText = _insertCommandRedemption + "[IsBOTransaction],[EntryUsersID],[EntryTime],[ApprovedUsersID],[ApprovedTime],[LastUpdate])" +
                                 "Select isnull(max(ClientRedemptionPk),0) + 1,1,@status," + _paramaterCommandRedemption + "1,@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@LastUpdate From ClientRedemption";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _clientRedemption.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);
                        }
                        else
                        {
                            cmd.CommandText = @"Declare @ClientRedemptionPK int   
                                    Select @ClientRedemptionPK = Max(ClientRedemptionPK) + 1 From ClientRedemption
                                    set @ClientRedemptionPK = isnull(@ClientRedemptionPK,1) 
                                " + _insertCommandRedemption + "[IsBOTransaction],[EntryUsersID],[EntryTime],[LastUpdate])" +
                                @"  
                                
                            Select @ClientRedemptionPK,1,@status," + _paramaterCommandRedemption + "1,@EntryUsersID,@EntryTime,@LastUpdate" +
                                @"

                                   Declare @LastNAV numeric(22,8)
		                           Declare @RedemptUnit numeric(22,8)
		                           Select @LastNAV  = NAV
		                           from CloseNAV where Date = 
		                           (
			                        Select Max(date) From CloseNAV Where FundPK = @FundPK and status = 2 and Date < @NAVDate
		                           ) and status = 2 and FundPK = @FundPK

		                           set @LastNAV = isnull(@LastNAV,1)
		                           set @RedemptUnit = 0
		                           IF @CashAmount > 0 and @NAV = 0
		                           BEGIN
				                        set @RedemptUnit = @CashAmount / @LastNAV
		                           END
		                           ELSE
		                           BEGIN
				                        set @RedemptUnit = @UnitAmount
		                           END

                                    set @RedemptUnit = @RedemptUnit * -1

                                    Declare @UnitPrevious numeric(22,8)

                                    set @UnitPrevious = 0
                                    Select @UnitPrevious = isnull(Unit,0) From FundClientPositionSummary
                                    where FundClientPK = @FundClientPK and FundPK = @FundPK

                                    set @UnitPrevious = isnull(@UnitPrevious,0)

                                    update fundclientpositionsummary
		                           set Unit = Unit + @RedemptUnit
		                           where FundClientPK = @FundClientPK and FundPK = @FundPK
                                    
                               
                                    --Buy = 1,
                                    --Sell = 2,
                                    --Adjustment = 3,
                                    --SwitchingIn = 5,
                                    --SwitchingOut = 6

                                    insert into [FundClientPositionLog]
                                               ([UserId]
                                               ,[FundClientPK]
                                               ,[TransactionPK]
                                               ,[ClientTransactionPK]
                                               ,[UnitPrevious]
                                               ,[UnitChanges]
                                               ,[UnitAfter]
                                               ,[IsBoTransaction]
                                               ,[TransactionType]
                                               ,[Description]
                                                ,[FundPK])
                                    Select @EntryUsersID,@FundClientPK,NULL,@ClientRedemptionPK,@UnitPrevious,@RedemptUnit,@UnitPrevious + @RedemptUnit,1,2,'Add Redemption',@FundPK

                                "
                                ;
                        }
                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@Type", _clientRedemption.Type);
                        cmd.Parameters.AddWithValue("@NAVDate", _clientRedemption.NAVDate);
                        cmd.Parameters.AddWithValue("@ValueDate", _clientRedemption.ValueDate);
                        cmd.Parameters.AddWithValue("@PaymentDate", _clientRedemption.PaymentDate);
                        cmd.Parameters.AddWithValue("@NAV", _clientRedemption.NAV);
                        cmd.Parameters.AddWithValue("@FundPK", _clientRedemption.FundPK);
                        cmd.Parameters.AddWithValue("@FundClientPK", _clientRedemption.FundClientPK);
                        cmd.Parameters.AddWithValue("@CashRefPK", _clientRedemption.CashRefPK);
                        cmd.Parameters.AddWithValue("@CurrencyPK", _clientRedemption.CurrencyPK);
                        cmd.Parameters.AddWithValue("@BitRedemptionAll", _clientRedemption.BitRedemptionAll);
                        cmd.Parameters.AddWithValue("@Description", _clientRedemption.Description);
                        cmd.Parameters.AddWithValue("@ReferenceSInvest", _clientRedemption.ReferenceSInvest);
                        cmd.Parameters.AddWithValue("@CashAmount", _clientRedemption.CashAmount);
                        cmd.Parameters.AddWithValue("@UnitAmount", _clientRedemption.UnitAmount);
                        cmd.Parameters.AddWithValue("@TotalCashAmount", _clientRedemption.TotalCashAmount);
                        cmd.Parameters.AddWithValue("@TotalUnitAmount", _clientRedemption.TotalUnitAmount);
                        cmd.Parameters.AddWithValue("@RedemptionFeePercent", _clientRedemption.RedemptionFeePercent);
                        cmd.Parameters.AddWithValue("@RedemptionFeeAmount", _clientRedemption.RedemptionFeeAmount);
                        cmd.Parameters.AddWithValue("@AgentPK", _clientRedemption.AgentPK);
                        cmd.Parameters.AddWithValue("@AgentFeePercent", _clientRedemption.AgentFeePercent);
                        cmd.Parameters.AddWithValue("@AgentFeeAmount", _clientRedemption.AgentFeeAmount);
                        cmd.Parameters.AddWithValue("@UnitPosition", _clientRedemption.UnitPosition);
                        cmd.Parameters.AddWithValue("@BankRecipientPK", _clientRedemption.BankRecipientPK);
                        cmd.Parameters.AddWithValue("@TransferType", _clientRedemption.TransferType);
                        cmd.Parameters.AddWithValue("@FeeType", _clientRedemption.FeeType);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _clientRedemption.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);

                        cmd.ExecuteNonQuery();

                        return _host.Get_LastPKByLastUpate(_dateTimeNow, "ClientRedemption");
                    }
                }

            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int ClientRedemption_Update(ClientRedemption _clientRedemption, bool _havePrivillege)
        {
            try
            {
                int _newHisPK;
                int _return = 0;
                int status = _host.Get_Status(_clientRedemption.ClientRedemptionPK, _clientRedemption.HistoryPK, "ClientRedemption");
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = @"Update ClientRedemption set status=2, Notes=@Notes,NAVDate=@NAVDate,ValueDate=@ValueDate,PaymentDate=@PaymentDate, 
                                NAV=@NAV,FundPK=@FundPK,FundClientPK=@FundClientPK,CashRefPK=@CashRefPK,CurrencyPK=@CurrencyPK,BitRedemptionAll=@BitRedemptionAll,Description=@Description,CashAmount=@CashAmount,UnitAmount=@UnitAmount,TotalCashAmount=@TotalCashAmount,TotalUnitAmount=@TotalUnitAmount, 
                                RedemptionFeePercent=@RedemptionFeePercent,RedemptionFeeAmount=@RedemptionFeeAmount,AgentPK=@AgentPK,AgentFeePercent=@AgentFeePercent,AgentFeeAmount=@AgentFeeAmount,UnitPosition=@UnitPosition,BankRecipientPK=@BankRecipientPK, TransferType = @TransferType,
                                FeeType=@FeeType,  ReferenceSInvest = @ReferenceSInvest,  Type=@Type,                            
                                ApprovedUsersID=@ApprovedUsersID,  
                                ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@LastUpdate  
                                where ClientRedemptionPK = @PK and historyPK = @HistoryPK";
                            cmd.Parameters.AddWithValue("@HistoryPK", _clientRedemption.HistoryPK);
                            cmd.Parameters.AddWithValue("@Notes", _clientRedemption.Notes);
                            cmd.Parameters.AddWithValue("@PK", _clientRedemption.ClientRedemptionPK);
                            cmd.Parameters.AddWithValue("@Type", _clientRedemption.Type);
                            cmd.Parameters.AddWithValue("@NAVDate", _clientRedemption.NAVDate);
                            cmd.Parameters.AddWithValue("@ValueDate", _clientRedemption.ValueDate);
                            cmd.Parameters.AddWithValue("@PaymentDate", _clientRedemption.PaymentDate);
                            cmd.Parameters.AddWithValue("@NAV", _clientRedemption.NAV);
                            cmd.Parameters.AddWithValue("@FundPK", _clientRedemption.FundPK);
                            cmd.Parameters.AddWithValue("@FundClientPK", _clientRedemption.FundClientPK);
                            cmd.Parameters.AddWithValue("@CashRefPK", _clientRedemption.CashRefPK);
                            cmd.Parameters.AddWithValue("@CurrencyPK", _clientRedemption.CurrencyPK);
                            cmd.Parameters.AddWithValue("@BitRedemptionAll", _clientRedemption.BitRedemptionAll);
                            cmd.Parameters.AddWithValue("@Description", _clientRedemption.Description);
                            cmd.Parameters.AddWithValue("@ReferenceSInvest", _clientRedemption.ReferenceSInvest);
                            cmd.Parameters.AddWithValue("@CashAmount", _clientRedemption.CashAmount);
                            cmd.Parameters.AddWithValue("@UnitAmount", _clientRedemption.UnitAmount);
                            cmd.Parameters.AddWithValue("@TotalCashAmount", _clientRedemption.TotalCashAmount);
                            cmd.Parameters.AddWithValue("@TotalUnitAmount", _clientRedemption.TotalUnitAmount);
                            cmd.Parameters.AddWithValue("@RedemptionFeePercent", _clientRedemption.RedemptionFeePercent);
                            cmd.Parameters.AddWithValue("@RedemptionFeeAmount", _clientRedemption.RedemptionFeeAmount);
                            cmd.Parameters.AddWithValue("@AgentPK", _clientRedemption.AgentPK);
                            cmd.Parameters.AddWithValue("@AgentFeePercent", _clientRedemption.AgentFeePercent);
                            cmd.Parameters.AddWithValue("@AgentFeeAmount", _clientRedemption.AgentFeeAmount);
                            cmd.Parameters.AddWithValue("@UnitPosition", _clientRedemption.UnitPosition);
                            cmd.Parameters.AddWithValue("@BankRecipientPK", _clientRedemption.BankRecipientPK);
                            cmd.Parameters.AddWithValue("@TransferType", _clientRedemption.TransferType);
                            cmd.Parameters.AddWithValue("@FeeType", _clientRedemption.FeeType);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _clientRedemption.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _dateTimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _clientRedemption.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);
                            cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);

                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update ClientRedemption set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@lastUpdate where ClientRedemptionPK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _clientRedemption.ClientRedemptionPK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _clientRedemption.EntryUsersID);
                            cmd.Parameters.AddWithValue("@VoidTime", _dateTimeNow);
                            cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                            cmd.ExecuteNonQuery();
                        }
                        _return = 0;
                    }
                    else
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = @"
                                   Declare @LastNAV numeric(22,8)
		                           Declare @RedemptUnit numeric(22,8)


                                    Declare @OldRedemptUnit numeric(22,8)
                                    Declare @OldUnitAmount numeric(22,8)
                                    Declare @OldNAVDate datetime
                                    Declare @OldFundPK int
                                    Declare @OldFundClientPK int
                                    Declare @OldCashAmount numeric(24,4)
                                    Declare @OldNAV numeric(18,8)
                                    Declare @TrxFrom nvarchar(100)
                                    Declare @TransactionPK nvarchar(200)
                                    Select @OldFundPK = FundPK,@OldFundClientPK = FundClientPK, @OldNAVDate = NAVDate,@OldNAV = NAV,@TrxFrom = EntryUsersID,
                                    @TransactionPK = TransactionPK
                                    From ClientRedemption where ClientRedemptionPK = @PK and HistoryPK = @HistoryPK

                                    Select @OldUnitAmount = UnitChanges * -1 from FundClientPositionLog where FundClientPK = @OldFundClientPK and FundPK = @oldFundPK
                                    and ClientTransactionPK = @PK and TransactionType = 2 and ID =
                                    (
                                        Select max(ID) From FundClientPositionLog where FundClientPK = @OldFundClientPK and FundPK = @oldFundPK
                                                                         and ClientTransactionPK = @PK and TransactionType = 2
                                    )

                                    Set @OldCashAmount = 0


		                           Select @LastNAV  = NAV
		                           from CloseNAV where Date = 
		                           (
			                        Select Max(date) From CloseNAV Where FundPK = @OldFundPK and status = 2 and Date < @OldNAVDate
		                           ) and status = 2 and FundPK = @OldFundPK

		                           set @LastNAV = isnull(@LastNAV,1)
		                           set @OldRedemptUnit = 0
		                           IF @OldCashAmount > 0 and @oldNAV = 0
		                           BEGIN
				                        set @OldRedemptUnit = @OldCashAmount / @LastNAV
		                           END
		                           ELSE
		                           BEGIN
				                        set @OldRedemptUnit = @OldUnitAmount
		                           END

                                    Declare @UnitPrevious numeric(22,8)
                                    Select @UnitPrevious = isnull(Unit,0) From FundClientPositionSummary
                                    where FundClientPK = @OldFundClientPK and FundPK = @OldFundPK

                                    update fundclientpositionsummary
		                           set Unit = Unit + @OldRedemptUnit
		                           where FundClientPK = @OldFundClientPK and FundPK = @OldFundPK
                               
                                    --Buy = 1,
                                    --Sell = 2,
                                    --Adjustment = 3,
                                    --SwitchingIn = 5,
                                    --SwitchingOut = 6

                                    insert into [FundClientPositionLog]
                                               ([UserId]
                                               ,[FundClientPK]
                                               ,[TransactionPK]
                                               ,[ClientTransactionPK]
                                               ,[UnitPrevious]
                                               ,[UnitChanges]
                                               ,[UnitAfter]
                                               ,[IsBoTransaction]
                                               ,[TransactionType]
                                               ,[Description]
                                                ,[FundPK])
                                    Select @UpdateUsersID,@OldFundClientPK, @TransactionPK,@PK,@UnitPrevious,@OldRedemptUnit,@UnitPrevious + @OldRedemptUnit
                                    ,case when @TrxFrom = 'rdo' then 0 else 1 end
                                    ,2,'Update Redemption Old Data Revise',@OldFundPK
                                ";
                            cmd.Parameters.AddWithValue("@Notes", _clientRedemption.Notes);
                            cmd.Parameters.AddWithValue("@PK", _clientRedemption.ClientRedemptionPK);
                            cmd.Parameters.AddWithValue("@HistoryPK", _clientRedemption.HistoryPK);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _clientRedemption.EntryUsersID);
                            cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);


                            cmd.ExecuteNonQuery();
                        }


                        if (status == 1)
                        {
                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = @"Update ClientRedemption set Notes=@Notes,NAVDate=@NAVDate,ValueDate=@ValueDate,PaymentDate=@PaymentDate, 
                                NAV=@NAV,FundPK=@FundPK,FundClientPK=@FundClientPK,CashRefPK=@CashRefPK,CurrencyPK=@CurrencyPK,BitRedemptionAll=@BitRedemptionAll,Description=@Description,CashAmount=@CashAmount,UnitAmount=@UnitAmount,TotalCashAmount=@TotalCashAmount,TotalUnitAmount=@TotalUnitAmount, 
                                RedemptionFeePercent=@RedemptionFeePercent,RedemptionFeeAmount=@RedemptionFeeAmount,AgentPK=@AgentPK,AgentFeePercent=@AgentFeePercent,AgentFeeAmount=@AgentFeeAmount,UnitPosition=@UnitPosition,BankRecipientPK=@BankRecipientPK,  TransferType = @TransferType,
                                FeeType=@FeeType,ReferenceSInvest = @ReferenceSInvest, Type=@Type,                        
                                UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@LastUpdate  
                                where ClientRedemptionPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _clientRedemption.HistoryPK);
                                cmd.Parameters.AddWithValue("@Notes", _clientRedemption.Notes);
                                cmd.Parameters.AddWithValue("@PK", _clientRedemption.ClientRedemptionPK);
                                cmd.Parameters.AddWithValue("@Type", _clientRedemption.Type);
                                cmd.Parameters.AddWithValue("@NAVDate", _clientRedemption.NAVDate);
                                cmd.Parameters.AddWithValue("@ValueDate", _clientRedemption.ValueDate);
                                cmd.Parameters.AddWithValue("@PaymentDate", _clientRedemption.PaymentDate);
                                cmd.Parameters.AddWithValue("@NAV", _clientRedemption.NAV);
                                cmd.Parameters.AddWithValue("@FundPK", _clientRedemption.FundPK);
                                cmd.Parameters.AddWithValue("@FundClientPK", _clientRedemption.FundClientPK);
                                cmd.Parameters.AddWithValue("@CashRefPK", _clientRedemption.CashRefPK);
                                cmd.Parameters.AddWithValue("@CurrencyPK", _clientRedemption.CurrencyPK);
                                cmd.Parameters.AddWithValue("@BitRedemptionAll", _clientRedemption.BitRedemptionAll);
                                cmd.Parameters.AddWithValue("@TransferType", _clientRedemption.TransferType);
                                cmd.Parameters.AddWithValue("@Description", _clientRedemption.Description);
                                cmd.Parameters.AddWithValue("@ReferenceSInvest", _clientRedemption.ReferenceSInvest);
                                cmd.Parameters.AddWithValue("@CashAmount", _clientRedemption.CashAmount);
                                cmd.Parameters.AddWithValue("@UnitAmount", _clientRedemption.UnitAmount);
                                cmd.Parameters.AddWithValue("@TotalCashAmount", _clientRedemption.TotalCashAmount);
                                cmd.Parameters.AddWithValue("@TotalUnitAmount", _clientRedemption.TotalUnitAmount);
                                cmd.Parameters.AddWithValue("@RedemptionFeePercent", _clientRedemption.RedemptionFeePercent);
                                cmd.Parameters.AddWithValue("@RedemptionFeeAmount", _clientRedemption.RedemptionFeeAmount);
                                cmd.Parameters.AddWithValue("@AgentPK", _clientRedemption.AgentPK);
                                cmd.Parameters.AddWithValue("@AgentFeePercent", _clientRedemption.AgentFeePercent);
                                cmd.Parameters.AddWithValue("@AgentFeeAmount", _clientRedemption.AgentFeeAmount);
                                cmd.Parameters.AddWithValue("@UnitPosition", _clientRedemption.UnitPosition);
                                cmd.Parameters.AddWithValue("@BankRecipientPK", _clientRedemption.BankRecipientPK);
                                cmd.Parameters.AddWithValue("@FeeType", _clientRedemption.FeeType);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _clientRedemption.EntryUsersID);
                                cmd.Parameters.AddWithValue("@Updatetime", _dateTimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);

                                cmd.ExecuteNonQuery();
                            }
                            _return = 0;
                        }
                        else
                        {
                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                _newHisPK = _host.Get_NewHistoryPK(_clientRedemption.ClientRedemptionPK, "ClientRedemption");
                                cmd.CommandText = _insertCommandRedemption + "TransactionPK,[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                   "Select @PK,@NewHistoryPK,1," + _paramaterCommandRedemption + "@TransactionPK,EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From ClientRedemption where ClientRedemptionPK =@PK and historyPK = @HistoryPK ";
                                cmd.Parameters.AddWithValue("@PK", _clientRedemption.ClientRedemptionPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _clientRedemption.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@Type", _clientRedemption.Type);
                                cmd.Parameters.AddWithValue("@NAVDate", _clientRedemption.NAVDate);
                                cmd.Parameters.AddWithValue("@ValueDate", _clientRedemption.ValueDate);
                                cmd.Parameters.AddWithValue("@PaymentDate", _clientRedemption.PaymentDate);
                                cmd.Parameters.AddWithValue("@NAV", _clientRedemption.NAV);
                                cmd.Parameters.AddWithValue("@FundPK", _clientRedemption.FundPK);
                                cmd.Parameters.AddWithValue("@FundClientPK", _clientRedemption.FundClientPK);
                                cmd.Parameters.AddWithValue("@CashRefPK", _clientRedemption.CashRefPK);
                                cmd.Parameters.AddWithValue("@CurrencyPK", _clientRedemption.CurrencyPK);
                                cmd.Parameters.AddWithValue("@BitRedemptionAll", _clientRedemption.BitRedemptionAll);
                                cmd.Parameters.AddWithValue("@Description", _clientRedemption.Description);
                                cmd.Parameters.AddWithValue("@ReferenceSInvest", _clientRedemption.ReferenceSInvest);
                                cmd.Parameters.AddWithValue("@CashAmount", _clientRedemption.CashAmount);
                                cmd.Parameters.AddWithValue("@UnitAmount", _clientRedemption.UnitAmount);
                                cmd.Parameters.AddWithValue("@TotalCashAmount", _clientRedemption.TotalCashAmount);
                                cmd.Parameters.AddWithValue("@TotalUnitAmount", _clientRedemption.TotalUnitAmount);
                                cmd.Parameters.AddWithValue("@RedemptionFeePercent", _clientRedemption.RedemptionFeePercent);
                                cmd.Parameters.AddWithValue("@RedemptionFeeAmount", _clientRedemption.RedemptionFeeAmount);
                                cmd.Parameters.AddWithValue("@AgentPK", _clientRedemption.AgentPK);
                                cmd.Parameters.AddWithValue("@AgentFeePercent", _clientRedemption.AgentFeePercent);
                                cmd.Parameters.AddWithValue("@AgentFeeAmount", _clientRedemption.AgentFeeAmount);
                                cmd.Parameters.AddWithValue("@UnitPosition", _clientRedemption.UnitPosition);
                                cmd.Parameters.AddWithValue("@BankRecipientPK", _clientRedemption.BankRecipientPK);
                                cmd.Parameters.AddWithValue("@TransferType", _clientRedemption.TransferType);
                                cmd.Parameters.AddWithValue("@FeeType", _clientRedemption.FeeType);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _clientRedemption.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _dateTimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                                cmd.Parameters.AddWithValue("@TransactionPK", _clientRedemption.TransactionPK);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update ClientRedemption set status= 4,Notes=@Notes, " +
                                    " LastUpdate=@LastUpdate where ClientRedemptionPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _clientRedemption.Notes);
                                cmd.Parameters.AddWithValue("@PK", _clientRedemption.ClientRedemptionPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _clientRedemption.HistoryPK);
                                cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                                cmd.ExecuteNonQuery();
                            }


                            _return = _newHisPK;
                        }
                    }


                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"
                                   Declare @LastNAV numeric(22,8)
		                           Declare @RedemptUnit numeric(22,8)
                                    Declare @TrxFrom nvarchar(200)
                                    Declare  @TransactionPK nvarchar(200)
                                    Select @TrxFrom = EntryUsersID,  @TransactionPK = TransactionPK from ClientRedemption where ClientRedemptionPK = @PK and HistoryPK = @HistoryPK

                                  Select @LastNAV  = NAV
		                           from CloseNAV where Date = 
		                           (
			                        Select Max(date) From CloseNAV Where FundPK = @FundPK and status = 2 and Date < @NAVDate
		                           ) and status = 2 and FundPK = @FundPK

		                           set @LastNAV = isnull(@LastNAV,1)
		                           set @RedemptUnit = 0
		                           IF @CashAmount > 0 and @NAV = 0
		                           BEGIN
				                        set @RedemptUnit = @CashAmount / @LastNAV
		                           END
		                           ELSE
		                           BEGIN
				                        set @RedemptUnit = @UnitAmount
		                           END
                                   set @RedemptUnit = @RedemptUnit * -1
                                    Declare @UnitPrevious numeric(22,8)
                                    Select @UnitPrevious = isnull(Unit,0) From FundClientPositionSummary
                                    where FundClientPK = @FundClientPK and FundPK = @FundPK

                                    update fundclientpositionsummary
		                           set Unit = Unit + @RedemptUnit
		                           where FundClientPK = @FundClientPK and FundPK = @FundPK
                               
                                    --Buy = 1,
                                    --Sell = 2,
                                    --Adjustment = 3,
                                    --SwitchingIn = 5,
                                    --SwitchingOut = 6

                                    insert into [FundClientPositionLog]
                                               ([UserId]
                                               ,[FundClientPK]
                                               ,[TransactionPK]
                                               ,[ClientTransactionPK]
                                               ,[UnitPrevious]
                                               ,[UnitChanges]
                                               ,[UnitAfter]
                                               ,[IsBoTransaction]
                                               ,[TransactionType]
                                              ,[Description]
                                                ,[FundPK])
                                    Select @UpdateUsersID,@FundClientPK, @TransactionPK,@PK,@UnitPrevious,@RedemptUnit,@UnitPrevious + @RedemptUnit
                                    ,Case when @TrxFrom = 'rdo' then 0 else 1 end,2,'Update Redemption',@FundPK

                                ";
                        cmd.Parameters.AddWithValue("@Notes", _clientRedemption.Notes);
                        cmd.Parameters.AddWithValue("@PK", _clientRedemption.ClientRedemptionPK);
                        cmd.Parameters.AddWithValue("@HistoryPK", _clientRedemption.HistoryPK);
                        cmd.Parameters.AddWithValue("@UpdateUsersID", _clientRedemption.EntryUsersID);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);

                        cmd.Parameters.AddWithValue("@FundPK", _clientRedemption.FundPK);
                        cmd.Parameters.AddWithValue("@FundClientPK", _clientRedemption.FundClientPK);
                        cmd.Parameters.AddWithValue("@NAVDate", _clientRedemption.NAVDate);
                        cmd.Parameters.AddWithValue("@NAV", _clientRedemption.NAV);
                        cmd.Parameters.AddWithValue("@CashAmount", _clientRedemption.CashAmount);
                        cmd.Parameters.AddWithValue("@UnitAmount", _clientRedemption.UnitAmount);

                        cmd.ExecuteNonQuery();
                    }
                    return _return;
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public void ClientRedemption_Revise(ClientRedemption _clientRedemption)
        {

            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText =
                        @"                        
                                    update FundJournal set Status = 3,VoidTime = @RevisedTime,VoidUsersID = @RevisedBy  where Type = 3 and TrxNo = @ClientRedemptionPK 
                                    and Posted = 1 

                                    if exists(select * from FundClientPosition 
                                    where Date = @Date and FundClientPK = @FundClientPK and FundPK = @FundPK 
                                    )
                                    begin 
                                        Update FundClientPosition set CashAmount = CashAmount + @CashAmount, 
                                        UnitAmount = UnitAmount + @UnitAmount where Date = @Date and FundClientPK = @FundClientPK 
                                        and FundPK = @FundPK 
                                    end 
                         
                                     Declare @MaxClientRedemptionPK int 
                       
                                     Select @MaxClientRedemptionPK = ISNULL(MAX(ClientRedemptionPK),0) + 1 From ClientRedemption   
                                     INSERT INTO [dbo].[ClientRedemption]  
                                     ([ClientRedemptionPK],[HistoryPK] ,[Status],[Notes], [NAVDate] ,[ValueDate],
                                     [PaymentDate],[BitRedemptionAll], [NAV] ,[FundPK], [FundClientPK] , [CashRefPK] ,[Description] ,
                                     [CashAmount] ,[UnitAmount] ,[TotalCashAMount] ,[TotalUnitAmount] ,[RedemptionFeePercent] ,[RedemptionFeeAmount] ,
                                     [AgentPK] ,[AgentFeePercent],[AgentFeeAmount],[CurrencyPK],
                                     [DepartmentPK],[Type],[BankRecipientPK],[TransferType],[FeeType],
                                     [EntryUsersID],[EntryTime],[LastUpdate],[TransactionPK],[IsFrontSync])
                       
                                     SELECT @MaxClientRedemptionPK,1,1,'Pending Revised' ,[NAVDate] ,
                                     [ValueDate],[PaymentDate],[BitRedemptionAll],[NAV] ,[FundPK],[FundClientPK] ,
                                     [CashRefPK] ,[Description] ,[CashAmount] ,[UnitAmount] ,[TotalCashAMount] ,[TotalUnitAmount] ,
                                     [RedemptionFeePercent] ,[RedemptionFeeAmount] ,[AgentPK] ,[AgentFeePercent],[AgentFeeAmount],[CurrencyPK],
                                     [DepartmentPK],[Type],[BankRecipientPK],[TransferType],[FeeType],
                                     [EntryUsersID],[EntryTime] , @RevisedTime,[TransactionPK],0
                                     FROM ClientRedemption   
                                     WHERE ClientRedemptionPK = @ClientRedemptionPK   and status = 2 and posted = 1  
                        
                        
                                update ClientRedemption 
                                set RevisedBy = @RevisedBy,RevisedTime = @RevisedTime,Revised = 1, status = 3 , IsFrontSync = 0
                                where ClientRedemptionPK = @ClientRedemptionPK and Status = 2 and posted = 1 
                        
                                Declare @counterDate datetime 
                                set @counterDate = @Date 
                                while @counterDate < (select max(date) from fundClientPosition where FundClientPK = @FundClientPK and FundPK = @FundPK )
                                BEGIN " +
                                "set @counterDate = dbo.fworkingday(@counterDate,1) \n " +
                                @"update fundClientPosition set UnitAmount = UnitAmount + @UnitAmount,CashAmount = CashAmount + @CashAmount
                                where FundClientPK = @FundClientPK and FundPK = @FundPK and Date = @counterDate end 

                                  Declare @LastNAV numeric(22,8)
		                           Declare @RedemptUnit numeric(22,8)


                                    Declare @OldRedemptUnit numeric(22,8)
                                    Declare @OldUnitAmount numeric(22,8)
                                    Declare @OldNAVDate datetime
                                    Declare @OldFundPK int
                                    Declare @OldFundClientPK int
                                    Declare @OldCashAmount numeric(24,4)
                                    Declare @OldNAV numeric(18,8)
                                    Declare @TrxFrom nvarchar(200)
                                    Declare @TransactionPK nvarchar(200)
                                    Select @OldFundPK = FundPK,@OldFundClientPK = FundClientPK, @OldNAVDate = NAVDate,@oldNAV = NAV,@TransactionPK = TransactionPK,@TrxFrom = EntryUsersID
                                    From ClientRedemption where ClientRedemptionPK = @PK and HistoryPK = @HistoryPK

                                    Select @OldUnitAmount = UnitChanges * -1 from FundClientPositionLog where FundClientPK = @OldFundClientPK and FundPK = @oldFundPK
                                    and ClientTransactionPK = @PK and TransactionType = 2 and ID =
                                    (
                                        Select max(ID) from FundClientPositionLog where FundClientPK = @OldFundClientPK and FundPK = @oldFundPK
                                                                        and ClientTransactionPK = @PK and TransactionType = 2
                                    )

                                    Set @OldCashAmount = 0


		                           Select @LastNAV  = NAV
		                           from CloseNAV where Date = 
		                           (
			                        Select Max(date) From CloseNAV Where FundPK = @OldFundPK and status = 2 and Date < @OldNAVDate
		                           ) and status = 2 and FundPK = @OldFundPK

		                           set @LastNAV = isnull(@LastNAV,1)
		                           set @OldRedemptUnit = 0
		                           IF @OldCashAmount > 0 and @oldNAV = 0
		                           BEGIN
				                        set @OldRedemptUnit = @OldCashAmount / @LastNAV
		                           END
		                           ELSE
		                           BEGIN
				                        set @OldRedemptUnit = @OldUnitAmount
		                           END

                                    Declare @UnitPrevious numeric(22,8)
                                    Select @UnitPrevious = isnull(Unit,0) From FundClientPositionSummary
                                    where FundClientPK = @OldFundClientPK and FundPK = @OldFundPK
                                    set @UnitPrevious = isnull(@UnitPrevious,0)

                                    --Buy = 1,
                                    --Sell = 2,
                                    --Adjustment = 3,
                                    --SwitchingIn = 5,
                                    --SwitchingOut = 6

                                    insert into [FundClientPositionLog]
                                               ([UserId]
                                               ,[FundClientPK]
                                               ,[TransactionPK]
                                               ,[ClientTransactionPK]
                                               ,[UnitPrevious]
                                               ,[UnitChanges]
                                               ,[UnitAfter]
                                               ,[IsBoTransaction]
                                               ,[TransactionType]
                                               ,[Description]
                                                ,[FundPK])
                                    Select @RevisedBy,@OldFundClientPK,@TransactionPK,@PK,@UnitPrevious,@OldRedemptUnit,@UnitPrevious + @OldRedemptUnit
                                    ,Case when @TrxFrom = 'rdo' then 0 else 1 end,2,'Revise Redemption Old Data Revise',@OldFundPK

                                    insert into [FundClientPositionLog]
                                               ([UserId]
                                               ,[FundClientPK]
                                               ,[TransactionPK]
                                               ,[ClientTransactionPK]
                                               ,[UnitPrevious]
                                               ,[UnitChanges]
                                               ,[UnitAfter]
                                               ,[IsBoTransaction]
                                               ,[TransactionType]
                                               ,[Description]
                                                ,[FundPK])
                                    Select @RevisedBy,@OldFundClientPK,@TransactionPK,@MaxClientRedemptionPK,@UnitPrevious + @OldRedemptUnit,@OldRedemptUnit * -1,@UnitPrevious + @OldRedemptUnit + (@OldRedemptUnit *-1)
                                    ,Case when @TrxFrom = 'rdo' then 0 else 1 end,2,'Revise Redemption',@OldFundPK



";

                        cmd.Parameters.AddWithValue("@Date", _clientRedemption.ValueDate);
                        cmd.Parameters.AddWithValue("@FundClientPK", _clientRedemption.FundClientPK);
                        cmd.Parameters.AddWithValue("@FundPK", _clientRedemption.FundPK);
                        cmd.Parameters.AddWithValue("@CashAmount", _clientRedemption.TotalCashAmount);
                        cmd.Parameters.AddWithValue("@UnitAmount", _clientRedemption.UnitAmount);
                        cmd.Parameters.AddWithValue("@RevisedBy", _clientRedemption.RevisedBy);
                        cmd.Parameters.AddWithValue("@RevisedTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@ClientRedemptionPK", _clientRedemption.ClientRedemptionPK);
                        cmd.Parameters.AddWithValue("@UpdateUsersID", _clientRedemption.RevisedBy);
                        cmd.Parameters.AddWithValue("@PK", _clientRedemption.ClientRedemptionPK);
                        cmd.Parameters.AddWithValue("@HistoryPK", _clientRedemption.HistoryPK);
                        cmd.ExecuteNonQuery();
                    }


                }
            }



            catch (Exception err)
            {
                throw err;
            }

        }

        public void ClientRedemption_RejectBySelected(string _usersID, string _permissionID, DateTime _dateFrom, DateTime _dateTo, ParamUnitRegistryBySelected _paramUnitRegistryBySelected)
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

                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"Declare @IPAddress nvarchar(50) select @IPAddress = IPAddress from Users where ID = @UsersID and Status = 2 
                                          Insert into Activity(Time,PermissionID,ObjectTable,ObjectTablePK,Status,Message,UsersID,IPAddress,LastUpdate) 
                                          Select @Time,@PermissionID,'ClientRedemption',ClientRedemptionPK,1,'Reject by Selected Data',@UsersID,@IPAddress,@Time  from ClientRedemption where ValueDate between @DateFrom and @DateTo and Status = 1 " + paramClientRedemptionSelected + @"
                                         

                            Declare @PK int
                            Declare @HistoryPK int

                            Declare A Cursor For
	                            Select ClientRedemptionPK,historyPK from ClientRedemption where ValueDate between @DateFrom and @DateTo and Status = 1 " + paramClientRedemptionSelected + @"
                            Open A
                            Fetch next From A
                            into @PK,@HistoryPK
                            While @@Fetch_status = 0
                            Begin

				                             Declare @LastNAV numeric(22,8)
		                                    Declare @RedemptUnit numeric(22,8)


                                            Declare @OldRedemptUnit numeric(22,8)
                                            Declare @OldUnitAmount numeric(22,8)
                                            Declare @OldNAVDate datetime
                                            Declare @OldFundPK int
                                            Declare @OldFundClientPK int
                                            Declare @OldCashAmount numeric(24,4)
                                            Declare @TrxFrom nvarchar(200)
                                            Declare @TransactionPK nvarchar(200)
                                            Select @OldFundPK = FundPK,@OldFundClientPK = FundClientPK, @OldNAVDate = NAVDate, @TrxFrom = EntryUsersID,@TransactionPK = TransactionPK
                                            From ClientRedemption where ClientRedemptionPK = @PK and HistoryPK = @HistoryPK

                                            Select @OldUnitAmount = UnitChanges * -1 from FundClientPositionLog where FundClientPK = @OldFundClientPK and FundPK = @oldFundPK
                                           and ClientTransactionPK = @PK and TransactionType = 2 and ID =
                                            (
                                                Select max(ID) From FundClientPositionLog where FundClientPK = @OldFundClientPK and FundPK = @oldFundPK
                                                                                 and ClientTransactionPK = @PK and TransactionType = 2
                                            )

                                            Set @OldCashAmount = 0
				                            set @OldRedemptUnit = @OldUnitAmount

                                            Declare @UnitPrevious numeric(22,8)
                                            Select @UnitPrevious = isnull(Unit,0) From FundClientPositionSummary
                                            where FundClientPK = @OldFundClientPK and FundPK = @OldFundPK

                                            update fundclientpositionsummary
		                                    set Unit = Unit + @OldRedemptUnit
		                                    where FundClientPK = @OldFundClientPK and FundPK = @OldFundPK
                               
                                            --Buy = 1,
                                            --Sell = 2,
                                            --Adjustment = 3,
                                            --SwitchingIn = 5,
                                            --SwitchingOut = 6

                                            insert into [FundClientPositionLog]
                                                        ([UserId]
                                                        ,[FundClientPK]
                                                        ,[TransactionPK]
                                                        ,[ClientTransactionPK]
                                                        ,[UnitPrevious]
                                                        ,[UnitChanges]
                                                        ,[UnitAfter]
                                                        ,[IsBoTransaction]
                                                        ,[TransactionType]
                                                        ,[Description]
                                                        ,[FundPK])
                                            Select @UsersID,@OldFundClientPK,@TransactionPK,@PK,@UnitPrevious,@OldRedemptUnit,@UnitPrevious + @OldRedemptUnit
                                            ,Case when @TrxFrom = 'rdo' then 0 else 1 end,2,'Reject Redemption',@OldFundPK


                            fetch next From A into @PK,@HistoryPK
                            end
                            Close A
                            Deallocate A



                                          update ClientRedemption set status = 3,selected= 0 , VoidUsersID = @UsersID,VoidTime = @Time,LastUpdate=@Time where status = 1 and ClientRedemptionPK in ( Select ClientRedemptionPK from ClientRedemption where ValueDate between @DateFrom and @DateTo and Status = 1 " + paramClientRedemptionSelected + @") 
                                          Update ClientRedemption set status= 2  where status = 4 and ClientRedemptionPK in (Select ClientRedemptionPK from  ClientRedemption where ValueDate between @DateFrom and @DateTo and Status = 4  " + paramClientRedemptionSelected + @") 





                        ";

                        cmd.Parameters.AddWithValue("@PermissionID", _permissionID);
                        cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _dateTo);
                        cmd.Parameters.AddWithValue("@UsersID", _usersID);
                        cmd.Parameters.AddWithValue("@Time", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public void ClientRedemption_PostingBySelected(string _usersID, DateTime _dateFrom, DateTime _dateTo, bool _bitManageUR, ParamUnitRegistryBySelected _paramUnitRegistryBySelected)
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

                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        string _parambitManageUR = "";

                        if (_bitManageUR == true)
                        {
                            _parambitManageUR = "Where A.status = 2  and A.Posted = 0 and A.ClientRedemptionPK in (select PK from ZManage_UR where Selected = 1 and Type = 2  and Date between @DateFrom and @DateTo) ";
                        }
                        else
                        {
                            _parambitManageUR = "Where A.status = 2 and Posted = 0 and ValueDate between @DateFrom and @DateTo " + paramClientRedemptionSelected;
                        }

                        cmd.CommandTimeout = 0;
                        cmd.CommandText = @"
       
    Declare @CClientRedemptionPK int
    Declare @CNAVDate datetime
    Declare @CPaymentDate datetime
    Declare @CTotalUnitAmount numeric(18,8)
    Declare @CUnitAmount numeric(18,8)
    Declare @CTotalCashAmount numeric(22,4)
    Declare @CCashAmount numeric(18,4)
    Declare @CAgentFeeAmount numeric(18,4)
    Declare @CRedemptionFeeAmount numeric(18,4)
    Declare @CFundClientPK int
    Declare @CFundClientID nvarchar(100) 
    Declare @CFundClientName nvarchar(300) 
    Declare @CFundCashRefPK	int
    Declare @CFundPK int
    Declare @CHistoryPK int
    Declare @TotalFeeAmount numeric(18,4)
    Declare @TempAmount numeric(18,4)
    Declare @CType int
    
    Declare @RedemptionAcc int Declare @PayableRedemptionAcc int Declare @PayableRedemptionFee int
    Declare @PeriodPK int Declare @BankPK int Declare @BankCurrencyPK int 
    Declare @FundJournalPK int 



    DECLARE A CURSOR FOR 
    Select FundPK,ClientRedemptionPK,NAVDate,PaymentDate,isnull(sum(TotalUnitAmount),0)
	,isnull(sum(TotalCashAmount),0),isnull(sum(UnitAmount),0),isnull(sum(CashAmount),0),
    isnull(sum(AgentFeeAmount),0),isnull(sum(RedemptionFeeAmount),0),
    A.FundClientPK,B.ID,B.Name,CashRefPK,A.HistoryPK,A.Type
	 
    From ClientRedemption A Left join FundClient B on A.FundClientPK = B.FundClientPK and B.status = 2
" + _parambitManageUR + @"
    --Where A.status = 2 and Posted = 0 and ValueDate between @DateFrom and @DateTo and A.Selected = 1

	Group by FundPK,ClientRedemptionPK,NAVDate,PaymentDate,A.FundClientPK,B.ID,B.Name,CashRefPK,A.HistoryPK
	
    Open A
    Fetch Next From A
    Into @CFundPK,@CClientRedemptionPK,@CNAVDate,@CPaymentDate,@CTotalUnitAmount,@CTotalCashAmount,@CUnitAmount,@CCashAmount,@CAgentFeeAmount,@CRedemptionFeeAmount,
    @CFundClientPK,@CFundClientID,@CFundClientName,@CFundCashRefPK,@CHistoryPK, @CType

    While @@FETCH_STATUS = 0
    Begin

   Select @RedemptionAcc = Redemption,@PayableRedemptionAcc = PendingRedemption
,@PayableRedemptionFee = payableRedemptionfee    
 From Fundaccountingsetup 
    where status = 2  and FundPK = @CFundPK



    -- LOGIC INSERT KE JOURNAL
    If @CTotalCashAmount > 0  and @CType not in(3,6)
    Begin
		                                    
    Select @TotalFeeAmount =  @CRedemptionFeeAmount
    set @TotalFeeAmount = isnull(@TotalFeeAmount,0)
    select @BankPK = FundJournalAccountPK,@BankCurrencyPK = A.CurrencyPK from FundCashRef A  where A.FundCashRefPK = @CFundCashRefPK  and A.status = 2       
    Select @PeriodPK = PeriodPK From Period Where @DateFrom Between DateFrom and DateTo and Status = 2      
	
    set @BankPK = isnull(@BankPK,3)
    set @BankCurrencyPK = isnull(@BankCurrencyPK,1)                                         
    -- TSETTLED
                             

    select @FundJournalPK = isnull(max(FundJournalPK),0) + 1 From FundJournal 

        INSERT INTO [dbo].[FundJournal] ([FundJournalPK],[HistoryPK],[Status],[Notes],[PeriodPK],[ValueDate],[Type],[TrxNo],[TrxName],[Reference] ,[Description],[Posted],[PostedBy],[PostedTime],[EntryUsersID],[EntryTime],[LastUpdate]) 
	    Select	   @FundJournalPK, 1,2,'Posting Redemption Amount',@PeriodPK,dbo.fworkingday(@CNAVDate,1),3,@CClientRedemptionPK,'REDEMPTION', '','Redemption Client: ' + @CFundClientID + ' - ' + @CFundClientName,1,@PostedBy,@PostedTime,@PostedBy,@PostedTime,@PostedTime
	 
	 
	    INSERT INTO [dbo].[FundJournalDetail]  ([FundJournalPK],[AutoNo],[HistoryPK],[Status],[FundJournalAccountPK],[CurrencyPK],[FundPK],[FundClientPK],[InstrumentPK],[DetailDescription] ,[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])	    
        Select	   @FundJournalPK,1,1,2,@RedemptionAcc,1,@CFundPK,@CFundClientPK,0,'Redemption Client: ' + @CFundClientID + ' - ' + @CFundClientName,'D',@CCashAmount,@CCashAmount,0,1,@CCashAmount,0,@PostedTime 
	  
	  
	    INSERT INTO [dbo].[FundJournalDetail]([FundJournalPK],[AutoNo],[HistoryPK],[Status],[FundJournalAccountPK],[CurrencyPK],[FundPK],[FundClientPK],[InstrumentPK],[DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])
	    Select	   @FundJournalPK,2,1,2,@PayableRedemptionAcc,1,@CFundPK,@CFundClientPK,0,'Redemption Client: ' + @CFundClientID + ' - ' + @CFundClientName,'C',@CTotalCashAmount,0,@CTotalCashAmount,1,0,@CTotalCashAmount,@PostedTime 

    if @TotalFeeAmount > 0
    begin

        INSERT INTO [dbo].[FundJournalDetail]([FundJournalPK],[AutoNo],[HistoryPK],[Status],[FundJournalAccountPK],[CurrencyPK],[FundPK],[FundClientPK],[InstrumentPK],[DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])
	    Select	   @FundJournalPK,3,1,2,@PayableRedemptionFee,1,@CFundPK,@CFundClientPK,0,'Redemption Client: ' + @CFundClientID + ' - ' + @CFundClientName,'C',@TotalFeeAmount,0,@TotalFeeAmount,1,0,@TotalFeeAmount,@PostedTime 
    end


    select @FundJournalPK = isnull(max(FundJournalPK),0) + 1 From FundJournal 

    INSERT INTO [dbo].[FundJournal] ([FundJournalPK],[HistoryPK],[Status],[Notes],[PeriodPK],[ValueDate],[Type],[TrxNo],[TrxName],[Reference] ,[Description],[Posted],[PostedBy],[PostedTime],[EntryUsersID],[EntryTime],[LastUpdate]) 
    Select	   @FundJournalPK, 1,2,'Posting From Redemption',@PeriodPK,@CPaymentDate,3,@CClientRedemptionPK,'REDEMPTION', '','Redemption Client: ' + @CFundClientID + ' - ' + @CFundClientName,1,@PostedBy,@PostedTime,@PostedBy,@PostedTime,@PostedTime
    
	INSERT INTO [dbo].[FundJournalDetail]  ([FundJournalPK],[AutoNo],[HistoryPK],[Status],[FundJournalAccountPK],[CurrencyPK],[FundPK],[FundClientPK],[InstrumentPK],[DetailDescription] ,[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])    
    Select	   @FundJournalPK,1,1,2,@PayableRedemptionAcc,1,@CFundPK,@CFundClientPK,0,'Redemption Client: ' + @CFundClientID + ' - ' + @CFundClientName,'D',@CTotalCashAmount,@CTotalCashAmount,0,1,@CTotalCashAmount,0,@PostedTime 
 
    INSERT INTO [dbo].[FundJournalDetail]([FundJournalPK],[AutoNo],[HistoryPK],[Status],[FundJournalAccountPK],[CurrencyPK],[FundPK],[FundClientPK],[InstrumentPK],[DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])
    Select	   @FundJournalPK,2,1,2,@BankPK,@BankCurrencyPK,@CFundPK,@CFundClientPK,0,'Redemption Client: ' + @CFundClientID + ' - ' + @CFundClientName,'C',@CTotalCashAmount,0,@CTotalCashAmount,1,0,@CTotalCashAmount,@PostedTime 
			 
    End
	
	-- LOGIC FUND CLIENT POSITION
	if Exists(select * from FundClientPosition    
	where Date = @CNavDate and FundClientPK = @CFundClientPK and FundPK = @CFundPK)    
	begin    
		Update FundClientPosition set CashAmount = CashAmount  - @CCashAmount,    
		UnitAmount = UnitAmount - @CUnitAmount where Date = @CNavDate and FundClientPK = @CFundClientPK    
		and FundPK = @CFundPK    
	end    
	else    
	begin    
		if Exists(Select * from FundClientPosition where Date <= @CNavDate and year(date) = year(@DateFrom)     
		and FundClientPK = @CFundClientPK and FundPK = @CFundPK)    
 
		Begin    
			INSERT INTO  FundClientPosition (Date,FundClientPK,FundPK,CashAmount,UnitAmount)     
			Select @CNavDate,@CFundClientPK,@CFundPK,CashAmount - @CCashAmount,UnitAmount - @CUnitAmount From FundClientPosition    
			Where Date = (    
				select MAX(Date) MaxDate from FundClientPosition where FundClientPK = @CFundClientPK and FundPK = @CFundPK and Date <= @CNavDate    
				and year(date) = year(@CNavDate)    
				)  and FundPK = @CFundPK and FundClientPK = @CFundClientPK
		End 
		else 
		Begin    
			INSERT INTO  FundClientPosition (Date,FundClientPK,FundPK,CashAmount,UnitAmount)    
			select @CNavDate,@CFundClientPK,@CFundPK,@CCashAmount,@CTotalUnitAmount  
		end 
	end      

                
        Declare @LastNAV numeric(22,8)
		Declare @RedemptUnit numeric(22,8)



        Declare @OldRedemptUnit numeric(22,8)
        Declare @OldUnitAmount numeric(22,8)
        Declare @OldNAVDate datetime
        Declare @OldFundPK int
        Declare @OldFundClientPK int
        Declare @OldCashAmount numeric(24,4)
        Declare @OldNAV numeric(18,8)
        Declare @TrxFrom nvarchar(100)
        Declare @TransactionPK nvarchar(200)




        Select @OldFundPK = FundPK,@OldFundClientPK = FundClientPK, @OldNAVDate = NAVDate,@OldNAV = NAV,@TrxFrom = EntryUsersID,
        @TransactionPK = TransactionPK
        From ClientRedemption where ClientRedemptionPK = @CClientRedemptionPK and HistoryPK = @CHistoryPK

        Set @OldUnitAmount = 0


        Select @OldUnitAmount = UnitChanges * -1 from FundClientPositionLog where FundClientPK = @OldFundClientPK and FundPK = @oldFundPK
        and ClientTransactionPK = @CClientRedemptionPK and TransactionType = 2 and ID =
        (
            Select max(ID) From FundClientPositionLog where FundClientPK = @OldFundClientPK and FundPK = @oldFundPK
                                                and ClientTransactionPK = @CClientRedemptionPK and TransactionType = 2
        )

        Set @OldCashAmount = 0

        set @lastNAV = 1
		
Select @LastNAV  = NAV
		from CloseNAV where Date = 
		(
		Select Max(date) From CloseNAV Where FundPK = @OldFundPK and status = 2 and Date < @OldNAVDate
		) and status = 2 and FundPK = @OldFundPK

		set @LastNAV = isnull(@LastNAV,1)
		set @OldRedemptUnit = 0
		
        IF @OldCashAmount > 0 and @oldNAV = 0
		BEGIN
			set @OldRedemptUnit = @OldCashAmount / @LastNAV
		END
		ELSE
		BEGIN
			set @OldRedemptUnit = @OldUnitAmount
		END

        Declare @UnitPrevious numeric(22,8)
        
        set @UnitPrevious = 0

        Select @UnitPrevious = isnull(Unit,0) From FundClientPositionSummary
        where FundClientPK = @OldFundClientPK and FundPK = @OldFundPK

        update fundclientpositionsummary
		set Unit = Unit + isnull(@OldRedemptUnit,0)
		where FundClientPK = @OldFundClientPK and FundPK = @OldFundPK
                               
        --Buy = 1,
        --Sell = 2,
        --Adjustment = 3,
        --SwitchingIn = 5,
        --SwitchingOut = 6

        insert into [FundClientPositionLog]
                    ([UserId]
                    ,[FundClientPK]
                    ,[TransactionPK]
                    ,[ClientTransactionPK]
                    ,[UnitPrevious]
                    ,[UnitChanges]
                    ,[UnitAfter]
                    ,[IsBoTransaction]
                    ,[TransactionType]
                    ,[Description]
                    ,[FundPK])
        Select @PostedBy,@OldFundClientPK, @TransactionPK,@CClientRedemptionPK,@UnitPrevious,@OldRedemptUnit,@UnitPrevious + @OldRedemptUnit
        ,case when @TrxFrom = 'rdo' then 0 else 1 end
        ,2,'Posting Redemption Old Data Revise',@OldFundPK


        set @RedemptUnit = 0

		set @RedemptUnit = @CUnitAmount

        set @RedemptUnit = @RedemptUnit * -1

        set @UnitPrevious = 0

        Select @UnitPrevious = @UnitPrevious + @OldRedemptUnit

        set @UnitPrevious = isnull(@UnitPrevious,0)

        update fundclientpositionsummary
		set Unit = Unit + @RedemptUnit
		where FundClientPK = @CFundClientPK and FundPK = @CFundPK
                                    
                               
        --Buy = 1,
        --Sell = 2,
        --Adjustment = 3,
        --SwitchingIn = 5,
        --SwitchingOut = 6

        insert into [FundClientPositionLog]
                    ([UserId]
                    ,[FundClientPK]
                    ,[TransactionPK]
                    ,[ClientTransactionPK]
                    ,[UnitPrevious]
                    ,[UnitChanges]
                    ,[UnitAfter]
                    ,[IsBoTransaction]
                    ,[TransactionType]
                    ,[Description]
                    ,[FundPK])
        Select @PostedBy,@CFundClientPK,@TransactionPK,@CClientRedemptionPK,@UnitPrevious,@RedemptUnit,@UnitPrevious + @RedemptUnit
        ,case when @TrxFrom = 'rdo' then 0 else 1 end,2,'Posting Redemption',@CFundPK



	update clientRedemption    
	set PostedBy = @PostedBy,PostedTime = @PostedTime,Posted = 1,Lastupdate = @PostedTime    
	where ClientRedemptionPK = @CClientRedemptionPK and Status = 2    

	   
	Declare @counterDate datetime      
	set @counterDate = @CNavDate      

    while @counterDate < (select dbo.fworkingday(max(valueDate),1)  from EndDayTrails)    
	BEGIN    
	set @counterDate = dbo.fworkingday(@counterdate,1)      
	update fundClientPosition set UnitAmount = UnitAmount  - @CUnitAmount,CashAmount = CashAmount - @CCashAmount    
	where FundClientPK = @CFundClientPK and FundPK = @CFundPK and Date = @counterDate end    
	   
	
Fetch next From A 
Into @CFundPK,@CClientRedemptionPK,@CNAVDate,@CPaymentDate,@CTotalUnitAmount,
	    @CTotalCashAmount,@CUnitAmount,@CCashAmount,@CAgentFeeAmount,@CRedemptionFeeAmount,
	    @CFundClientPK,@CFundClientID,@CFundClientName,@CFundCashRefPK,@CHistoryPK, @CType
end
Close A
Deallocate A



                        ";
                        cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _dateTo);
                        cmd.Parameters.AddWithValue("@PostedBy", _usersID);
                        cmd.Parameters.AddWithValue("@PostedTime", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }
        public bool Validate_CheckMinimumBalanceRedemption(decimal _cashAmount, decimal _unitAmount, DateTime _valueDate, int _fundPK, int _fundClientPK)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_unitAmount == 0)
                        {
                            cmd.CommandText = @"
                            create table #UnitAmountTemps
                            (
                            UnitAmount decimal(18,4),
                            fundpk int,
							fundclientpk int
                            )
                            insert into #UnitAmountTemps
                            select Unit - (select @CashAmount/isnull([dbo].[FgetLastCloseNav] (@ValueDate - 1,@FundPK),0) Unit) UnitAmount,FundPK,FundClientPK from FundClientPositionSummary where FundPK = @FundPK and FundclientPK = @FundClientPK

							select b.UnitAmount,a.RemainingBalanceUnit,case when b.UnitAmount = 0 then 0 else case when RemainingBalanceUnit < b.UnitAmount then 0 else case when RemainingBalanceUnit > b.UnitAmount then 1 end end end hasil from Fund a left join #UnitAmountTemps b on a.FundPK = b.fundpk where b.FundPK = @FundPK
                            ";

                        }
                        else
                        {
                            cmd.CommandText = @"
                           create table #UnitAmountTemps
                            (
                            UnitAmount decimal(18,4),
                            fundpk int,
							fundclientpk int
                            )
                            insert into #UnitAmountTemps
                            select Unit - @UnitAmount  UnitAmount,FundPK,FundClientPK from FundClientPositionSummary where FundPK = @FundPK and FundclientPK = @FundClientPK

							
						    select b.UnitAmount,a.RemainingBalanceUnit,case when b.UnitAmount = 0 then 0 else case when RemainingBalanceUnit < b.UnitAmount then 0 else case when RemainingBalanceUnit > b.UnitAmount then 1 end end end hasil from Fund a left join #UnitAmountTemps b on a.FundPK = b.fundpk where b.FundPK = @FundPK
							";

                        }

                        cmd.Parameters.AddWithValue("@CashAmount", _cashAmount);
                        cmd.Parameters.AddWithValue("@UnitAmount", _unitAmount);
                        cmd.Parameters.AddWithValue("@ValueDate", _valueDate);
                        cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                        cmd.Parameters.AddWithValue("@FundClientPK", _fundClientPK);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return Convert.ToBoolean(dr["hasil"]);
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



        //Subscription
        public void ClientSubscription_Revise(ClientSubscription _clientSubscription)
        {

            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText =
                        @"

                        update FundJournal set Status = 3,VoidTime = @RevisedTime,VoidUsersID = @RevisedBy  where Type = 2 and TrxNo = @ClientSubscriptionPK 
                        and Posted = 1 

                        select * from FundClientPosition 
                        where Date = @Date and FundClientPK = @FundClientPK and FundPK = @FundPK 
                        if @@rowCount > 0 
                        begin 
                        Update FundClientPosition set CashAmount = CashAmount - @CashAmount, 
                        UnitAmount = UnitAmount - @UnitAmount where Date = @Date and FundClientPK = @FundClientPK 
                        and FundPK = @FundPK 
                        end 
                        
                         Declare @MaxClientSubscriptionPK int 
                        
                         Select @MaxClientSubscriptionPK = ISNULL(MAX(ClientSubscriptionPK),0) + 1 From ClientSubscription   
                         INSERT INTO [dbo].[ClientSubscription]  
                         ([ClientSubscriptionPK],[HistoryPK] ,[Status],[Notes], [NAVDate] ,[ValueDate],
                          [NAV] ,[FundPK], [FundClientPK] , [CashRefPK] , [Description] ,
                         [CashAmount] ,[UnitAmount] ,[TotalCashAMount] ,[TotalUnitAmount] ,[SubscriptionFeePercent] ,[SubscriptionFeeAmount] ,
                         [AgentPK] ,[AgentFeePercent],[AgentFeeAmount],[CurrencyPK],[DepartmentPK],[AutoDebitDate],[Type],
                         [EntryUsersID],[EntryTime],[LastUpdate],[BitImmediateTransaction],[FeeType],[TransactionPK],[IsFrontSync])
                        
                         SELECT @MaxClientSubscriptionPK,1,1,'Pending Revised' ,[NAVDate] ,
                         [ValueDate],[NAV] ,[FundPK],[FundClientPK] ,
                         [CashRefPK] ,[Description] ,[CashAmount] ,[UnitAmount] ,[TotalCashAMount] ,[TotalUnitAmount] ,
                         [SubscriptionFeePercent] ,[SubscriptionFeeAmount] ,[AgentPK] ,[AgentFeePercent],[AgentFeeAmount],[CurrencyPK],[DepartmentPK],[AutoDebitDate],[Type],
                         [EntryUsersID],[EntryTime] , @RevisedTime, [BitImmediateTransaction],[FeeType],[TransactionPK],0
                         FROM ClientSubscription  
                         WHERE ClientSubscriptionPK = @ClientSubscriptionPK   and status = 2 and posted = 1 
                       
                        
                        update ClientSubscription 
                        set RevisedBy = @RevisedBy,RevisedTime = @RevisedTime,Revised = 1 , status = 3 , IsFrontSync = 0
                        where ClientSubscriptionPK = @ClientSubscriptionPK and Status = 2 and posted = 1 
                        
                        Declare @counterDate datetime " +
                        "set @counterDate = @Date  " +
                        "while @counterDate < (select max(date) from fundClientPosition where FundClientPK = @FundClientPK and FundPK = @FundPK) " +
                        "BEGIN " +
                        "set @counterDate = dbo.fworkingday(@counterDate,1) \n " +
                        "update fundClientPosition set UnitAmount = UnitAmount - @UnitAmount,CashAmount = CashAmount - @CashAmount " +
                        "where FundClientPK = @FundClientPK and FundPK = @FundPK and Date = @counterDate end "
                        + @"
                        
                                    Declare @LastNAV numeric(22,8)
		                           Declare @SubsUnit numeric(22,8)


                                    Declare @OldSubsUnit numeric(22,8)
                                    Declare @OldUnitAmount numeric(22,8)
                                    Declare @OldNAVDate datetime
                                    Declare @OldFundPK int
                                    Declare @OldFundClientPK int
                                    Declare @OldCashAmount numeric(24,4)
                                    Declare @OldNAV numeric(18,8)
                                    Declare @TrxFrom nvarchar(200)
                                    Declare @TransactionPK nvarchar(200)

                                    Select @OldFundPK = FundPK,@OldFundClientPK = FundClientPK, @OldNAVDate = NAVDate,@oldNAV = NAV
                                    ,@TrxFrom = EntryUsersID, @TransactionPK = TransactionPK
                                    From ClientSubscription where ClientSubscriptionPK = @PK and HistoryPK = @HistoryPK

                                    Select @OldUnitAmount = UnitChanges * -1 from FundClientPositionLog where FundClientPK = @OldFundClientPK and FundPK = @oldFundPK
                                     and ClientTransactionPK = @PK and TransactionType = 1 and ID =
                                    (
                                        Select max(ID) From FundClientPositionLog where FundClientPK = @OldFundClientPK and FundPK = @oldFundPK
                                                                         and ClientTransactionPK = @PK and TransactionType = 1
                                    )


				                   set @OldSubsUnit = @OldUnitAmount
		                      

                                    Declare @UnitPrevious numeric(22,8)
                                    Select @UnitPrevious = isnull(Unit,0) From FundClientPositionSummary
                                    where FundClientPK = @OldFundClientPK and FundPK = @OldFundPK

                                    set @UnitPrevious = isnull(@UnitPrevious,0)

                                   update fundclientpositionsummary
		                           set Unit = Unit + @OldSubsUnit
		                           where FundClientPK = @FundClientPK and FundPK = @FundPK

                               
                                    --Buy = 1,
                                    --Sell = 2,
                                    --Adjustment = 3,
                                    --SwitchingIn = 5,
                                    --SwitchingOut = 6

                                    insert into [FundClientPositionLog]
                                               ([UserId]
                                               ,[FundClientPK]
                                               ,[TransactionPK]
                                               ,[ClientTransactionPK]
                                               ,[UnitPrevious]
                                               ,[UnitChanges]
                                               ,[UnitAfter]
                                               ,[IsBoTransaction]
                                               ,[TransactionType]
                                               ,[Description]
                                                ,[FundPK])
                                    Select @RevisedBy,@OldFundClientPK,@TransactionPK,@PK,@UnitPrevious,@OldSubsUnit,@UnitPrevious + @OldSubsUnit
                                    ,Case when @TrxFrom = 'rdo' then 0 else 1 end,1,'Revise Subscription Old Data Revise',@OldFundPK
                        
                        ";

                        cmd.Parameters.AddWithValue("@Date", _clientSubscription.ValueDate);
                        cmd.Parameters.AddWithValue("@FundClientPK", _clientSubscription.FundClientPK);
                        cmd.Parameters.AddWithValue("@FundPK", _clientSubscription.FundPK);
                        cmd.Parameters.AddWithValue("@CashAmount", _clientSubscription.TotalCashAmount);
                        cmd.Parameters.AddWithValue("@UnitAmount", _clientSubscription.TotalUnitAmount);
                        cmd.Parameters.AddWithValue("@RevisedBy", _clientSubscription.RevisedBy);
                        cmd.Parameters.AddWithValue("@RevisedTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@ClientSubscriptionPK", _clientSubscription.ClientSubscriptionPK);
                        cmd.Parameters.AddWithValue("@PK", _clientSubscription.ClientSubscriptionPK);
                        cmd.Parameters.AddWithValue("@HistoryPK", _clientSubscription.HistoryPK);
                        cmd.ExecuteNonQuery();
                    }
                }
            }



            catch (Exception err)
            {
                throw err;
            }

        }

        public void ClientSubscription_PostingBySelected(string _usersID, DateTime _dateFrom, DateTime _dateTo, bool _bitManageUR, ParamUnitRegistryBySelected _paramUnitRegistryBySelected)
        {
            try
            {
                string paramClientSubscriptionSelected = "";
                if (!_host.findString(_paramUnitRegistryBySelected.UnitRegistrySelected.ToLower(), "0", ",") && !string.IsNullOrEmpty(_paramUnitRegistryBySelected.UnitRegistrySelected))
                {
                    paramClientSubscriptionSelected = " And ClientSubscriptionPK in (" + _paramUnitRegistryBySelected.UnitRegistrySelected + ") ";
                }
                else
                {
                    paramClientSubscriptionSelected = " And ClientSubscriptionPK in (0) ";
                }

                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        string _parambitManageUR = "";

                        if (_bitManageUR == true)
                        {
                            _parambitManageUR = "Where A.status = 2  and A.Posted = 0 and A.ClientSubscriptionPK in (select PK from ZManage_UR where Selected = 1 and Type = 1 and Date between @DateFrom and @DateTo) ";
                        }
                        else
                        {
                            _parambitManageUR = "Where A.status = 2 and Posted = 0 and ValueDate between @DateFrom and @Dateto " + paramClientSubscriptionSelected;
                        }

                        cmd.CommandTimeout = 0;
                        cmd.CommandText = @"
                        Declare @CClientSubscriptionPK int
                        Declare @CNAVDate datetime
                        Declare @CTotalUnitAmount numeric(18,8)
                        Declare @CUnitAmount numeric(18,8)
                        Declare @CTotalCashAmount numeric(22,4)
                        Declare @CCashAmount numeric(18,4)
                        Declare @CAgentFeeAmount numeric(18,4)
                        Declare @CSubscriptionFeeAmount numeric(18,4)
                        Declare @CFundClientPK int
                        Declare @CFundClientID nvarchar(100) 
                        Declare @CFundClientName nvarchar(300) 
                        Declare @CFundCashRefPK	int
                        Declare @CFundPK int
                        Declare @IssueDate datetime
                        Declare @CType int

                        Declare @TotalFeeAmount numeric(18,4)
                        Declare @TrxFrom nvarchar(200)
                        Declare @TransactionPK nvarchar(200)

                        Declare @SubscriptionAcc int Declare @PayableSubsAcc int Declare  @PendingSubscription int
                        Declare @PeriodPK int Declare @BankPK int Declare @BankCurrencyPK int 
                        Declare @FundJournalPK int 

                        Select @SubscriptionAcc = Subscription,@PendingSubscription = PendingSubscription,@PayableSubsAcc = payablesubscriptionfee From Fundaccountingsetup where status = 2   

                        DECLARE A CURSOR FOR 
	                        Select FundPK,ClientSubscriptionPK,NAVDate,isnull(TotalUnitAmount,0),isnull(TotalCashAmount,0),isnull(UnitAmount,0),isnull(CashAmount,0),
	                        isnull(AgentFeeAmount,0),isnull(SubscriptionFeeAmount,0),
	                        A.FundClientPK,B.ID,B.Name,CashRefPK, A.EntryUsersID,A.TransactionPK,A.Type
	 
	                        From ClientSubscription A Left join FundClient B on A.FundClientPK = B.FundClientPK and B.status = 2
                            " + _parambitManageUR + @"
	                        --Where A.status = 2 and Posted = 0 and ValueDate between @DateFrom and @Dateto and A.Selected = 1 
	
                        Open A
                        Fetch Next From A
                        Into @CFundPK,@CClientSubscriptionPK,@CNAVDate,@CTotalUnitAmount,@CTotalCashAmount,@CUnitAmount,@CCashAmount,@CAgentFeeAmount,@CSubscriptionFeeAmount,
	                         @CFundClientPK,@CFundClientID,@CFundClientName,@CFundCashRefPK,@TrxFrom,@TransactionPK,@CType

                        While @@FETCH_STATUS = 0
                        Begin

                            select @IssueDate = IssueDate from Fund where status in (1,2) and FundPK = @CFundPK

                            IF (@CNAVDate <> @IssueDate)
                            BEGIN

	                        -- LOGIC INSERT KE JOURNAL
		                        If @CTotalCashAmount > 0 and @CType not in(3,6)
		                        Begin
                                    DECLARE @BitPendingSubscription bit
                                    SELECT @BitPendingSubscription = ISNULL(BitPendingSubscription,1) FROM dbo.FundFee WHERE fundPK = @CFundPK
                                    AND date = 
                                    (
	                                    SELECT MAX(Date) FROM dbo.FundFee WHERE Date <= @CNAVDate AND FundPK = @CFundPK
	                                    AND status = 2
                                    ) and STATUS = 2
                                    IF(@BitPendingSubscription = 1)
                                    BEGIN
			                           Select @SubscriptionAcc = Subscription,@PendingSubscription = PendingSubscription,@PayableSubsAcc = payablesubscriptionfee 
                                        From Fundaccountingsetup where status = 2   and FundPK = @CFundPK
			                      	    END
                                        ELSE
                                        BEGIN
                                          Select @SubscriptionAcc = Subscription,@PendingSubscription = 3,@PayableSubsAcc = payablesubscriptionfee From Fundaccountingsetup where status = 2   
                                        and FundPK = @CFundPK
                                        END

                                         Select @FundJournalPK = isnull(MAX(FundJournalPK) +  1,1) From FundJournal       
			                            Select @TotalFeeAmount = @CSubscriptionFeeAmount
		   
			                            select @BankPK = FundJournalAccountPK,@BankCurrencyPK = A.CurrencyPK from FundCashRef A  where A.FundCashRefPK = @CFundCashRefPK  and A.status = 2       
			                            Select @PeriodPK = PeriodPK From Period Where @DateFrom Between DateFrom and DateTo and Status = 2      
			
			                            INSERT INTO [dbo].[FundJournal] ([FundJournalPK],[HistoryPK],[Status],[Notes],[PeriodPK],[ValueDate],[Type],[TrxNo],[TrxName],[Reference] ,[Description],[Posted],[PostedBy],[PostedTime],[EntryUsersID],[EntryTime],[LastUpdate]) 
			                            Select	   @FundJournalPK, 1,2,'Posting From Subscription',@PeriodPK,dbo.fworkingday(@CNAVDate,1),2,@CClientSubscriptionPK,'SUBSCRIPTION', '','Subscription Client: ' + @CFundClientID + ' - ' + @CFundClientName,1,@PostedBy,@PostedTime,@PostedBy,@PostedTime,@PostedTime
			                            INSERT INTO [dbo].[FundJournalDetail]  ([FundJournalPK],[AutoNo],[HistoryPK],[Status],[FundJournalAccountPK],[CurrencyPK],[FundPK],[FundClientPK],[DetailDescription] ,[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate]) 
			                            Select		@FundJournalPK,1,1,2,@PendingSubscription,1,@CFundPK,@CFundClientPK,'Subscription Client: ' + @CFundClientID + ' - ' + @CFundClientName,'D',@CTotalCashAmount,@CTotalCashAmount,0,1,@CTotalCashAmount,0,@PostedTime 
			                            INSERT INTO [dbo].[FundJournalDetail]([FundJournalPK],[AutoNo],[HistoryPK],[Status],[FundJournalAccountPK],[CurrencyPK],[FundPK],[FundClientPK],[DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])
			                            Select		@FundJournalPK,2,1,2,@SubscriptionAcc,1,@CFundPK,@CFundClientPK,'Subscription Client: ' + @CFundClientID + ' - ' + @CFundClientName,'C',@CTotalCashAmount,	0,@CTotalCashAmount,1,0,@CTotalCashAmount,@PostedTime 
		                        End
	                        END
	                        -- LOGIC FUND CLIENT POSITION
	                        if Exists(select * from FundClientPosition    
	                        where Date = @CNavDate and FundClientPK = @CFundClientPK and FundPK = @CFundPK)    
	                        begin    
		                        Update FundClientPosition set CashAmount = CashAmount  + @CCashAmount,    
		                        UnitAmount = UnitAmount + @CTotalUnitAmount where Date = @CNavDate and FundClientPK = @CFundClientPK    
		                        and FundPK = @CFundPK    
	                        end    
	                        else    
	                        begin    
	   
			                        INSERT INTO  FundClientPosition (Date,FundClientPK,FundPK,CashAmount,UnitAmount)    
			                        select @CNavDate,@CFundClientPK,@CFundPK,@CCashAmount,@CTotalUnitAmount  
		
	                        end      

                                  Declare @LastNAV numeric(22,8)
		                           Declare @SubsUnit numeric(22,8)
		                           set @SubsUnit = 0
				                    set @SubsUnit = @CTotalUnitAmount
		                          

                                    Declare @UnitPrevious numeric(22,8)
                                    Select @UnitPrevious = isnull(Unit,0) From FundClientPositionSummary
                                    where FundClientPK = @CFundClientPK and FundPK = @CFundPK
                                   set @UnitPrevious = 0

                                    set @UnitPrevious = isnull(@UnitPrevious,0)

                                     if exists(
                                        Select * from FundClientPositionSummary where FundClientPK = @CFundClientPK and FundPK = @CFundPK
                                    )BEGIN
                                       update fundclientpositionsummary
		                               set Unit = Unit + @SubsUnit
		                               where FundClientPK = @CFundClientPK and FundPK = @CFundPK

                                    END
                                    ELSE
                                    BEGIN
                                          Insert into FundClientPositionSummary (FundPK,FundClientPK,Unit)
                                        Select @CFundPK,@CFundClientPK,@SubsUnit
                                    END

   
                               
                                    --Buy = 1,
                                    --Sell = 2,
                                    --Adjustment = 3,
                                    --SwitchingIn = 5,
                                    --SwitchingOut = 6

                                    insert into [FundClientPositionLog]
                                               ([UserId]
                                               ,[FundClientPK]
                                               ,[TransactionPK]
                                               ,[ClientTransactionPK]
                                               ,[UnitPrevious]
                                               ,[UnitChanges]
                                               ,[UnitAfter]
                                               ,[IsBoTransaction]
                                               ,[TransactionType]
                                               ,[Description]
                                                ,[FundPK])
                                    Select @PostedBy,@CFundClientPK,@TransactionPK,@CClientSubscriptionPK,@UnitPrevious,@SubsUnit,@UnitPrevious + @SubsUnit,
                                   Case when @TrxFrom = 'rdo' then 0 else 1 end,1,'Posting Subscription',@CFundPK



	                        update clientsubscription    
	                        set PostedBy = @PostedBy,PostedTime = @PostedTime,Posted = 1,Lastupdate = @PostedTime    
	                        where ClientSubscriptionPK = @CClientSubscriptionPK and Status = 2    

	   
	                        Declare @counterDate datetime      
	                        set @counterDate = @CNavDate      
	                        while @counterDate < (select dbo.fworkingday(max(valueDate),1)  from EndDayTrails Where status = 2)    
	                        BEGIN    
		                        set @counterDate = dbo.fworkingday(@counterDate,1)      
		                        if Exists(select * from FundClientPosition    
		                        where Date = @counterDate and FundClientPK = @CFundClientPK and FundPK = @CFundPK)    
		                        BEGIN
			                        update fundClientPosition set UnitAmount = UnitAmount  + @CTotalUnitAmount,CashAmount = CashAmount + @CCashAmount    
			                        where FundClientPK = @CFundClientPK and FundPK = @CFundPK and Date = @counterDate 
		                        END
		                        ELSE
		                        BEGIN
		                        INSERT INTO  FundClientPosition (Date,FundClientPK,FundPK,CashAmount,UnitAmount)    
			                        select @counterDate,@CFundClientPK,@CFundPK,@CCashAmount,@CTotalUnitAmount 
		                        END
	                        END    
	   
	
                        Fetch next From A 
                        Into @CFundPK,@CClientSubscriptionPK,@CNAVDate,@CTotalUnitAmount,@CTotalCashAmount,@CUnitAmount,@CCashAmount,@CAgentFeeAmount,@CSubscriptionFeeAmount,
	                         @CFundClientPK,@CFundClientID,@CFundClientName,@CFundCashRefPK,@TrxFrom,@TransactionPK,@CType
                        end
                        Close A
                        Deallocate A
                        ";
                        cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _dateTo);
                        cmd.Parameters.AddWithValue("@PostedBy", _usersID);
                        cmd.Parameters.AddWithValue("@PostedTime", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        //Switching
        public int ClientSwitching_Add(ClientSwitching _clientSwitching, bool _havePrivillege)
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
                            cmd.CommandText = _insertCommandSwitching + "[IsBOTransaction],[EntryUsersID],[EntryTime],[ApprovedUsersID],[ApprovedTime],[LastUpdate])" +
                                 "Select isnull(max(ClientSwitchingPk),0) + 1,1,@status," + _paramaterCommandSwitching + "1,@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@LastUpdate From ClientSwitching";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _clientSwitching.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);
                        }
                        else
                        {
                            cmd.CommandText = @"Declare @ClientSwitchingPK int   
                                    Select @ClientSwitchingPK = Max(ClientSwitchingPK) + 1 From ClientSwitching
                                    set @ClientSwitchingPK = isnull(@ClientSwitchingPK,1) 
                                " + _insertCommandSwitching + "[IsBOTransaction],[EntryUsersID],[EntryTime],[LastUpdate])" +
                                 @"  
                                
                            Select @ClientSwitchingPK,1,@status," + _paramaterCommandSwitching + "1,@EntryUsersID,@EntryTime,@LastUpdate" +
                                @"

                                   Declare @LastNAV numeric(22,8)
		                           Declare @SwitchingUnit numeric(22,8)
		                           Select @LastNAV  = NAV
		                           from CloseNAV where Date = 
		                           (
			                        Select Max(date) From CloseNAV Where FundPK = @FundPKFrom and status = 2 and Date < @NAVDate
		                           ) and status = 2 and FundPK = @FundPKFrom

		                           set @LastNAV = isnull(@LastNAV,1)
		                           set @SwitchingUnit = 0
		                           IF @CashAmount > 0 and @NAVFundFrom = 0
		                           BEGIN
				                        set @SwitchingUnit = @CashAmount / @LastNAV
		                           END
		                           ELSE
		                           BEGIN
				                        set @SwitchingUnit = @UnitAmount
		                           END

                                    set @SwitchingUnit = @SwitchingUnit * -1

                                    Declare @UnitPrevious numeric(22,8)
                                    set @UnitPrevious = 0
                                    Select @UnitPrevious = isnull(Unit,0) From FundClientPositionSummary
                                    where FundClientPK = @FundClientPK and FundPK = @FundPKFrom

                                    set @UnitPrevious = isnull(@UnitPrevious,0)

                                    update fundclientpositionsummary
		                           set Unit = Unit + @SwitchingUnit
		                           where FundClientPK = @FundClientPK and FundPK = @FundPKFrom
                               
                                    --Buy = 1,
                                    --Sell = 2,
                                    --Adjustment = 3,
                                    --SwitchingIn = 5,
                                    --SwitchingOut = 6

                                    insert into [FundClientPositionLog]
                                               ([UserId]
                                               ,[FundClientPK]
                                               ,[TransactionPK]
                                               ,[ClientTransactionPK]
                                               ,[UnitPrevious]
                                               ,[UnitChanges]
                                               ,[UnitAfter]
                                               ,[IsBoTransaction]
                                               ,[TransactionType]
                                               ,[Description]
                                                ,[FundPK])
                                    Select @EntryUsersID,@FundClientPK,NULL,@ClientSwitchingPK,@UnitPrevious,@SwitchingUnit,@UnitPrevious + @SwitchingUnit,1,6,'Add Switching out',@FundPKFrom

                                "

                                ;
                        }
                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@Type", _clientSwitching.Type);
                        cmd.Parameters.AddWithValue("@NAVDate", _clientSwitching.NAVDate);
                        cmd.Parameters.AddWithValue("@ValueDate", _clientSwitching.ValueDate);
                        cmd.Parameters.AddWithValue("@PaymentDate", _clientSwitching.PaymentDate);
                        cmd.Parameters.AddWithValue("@NAVFundFrom", _clientSwitching.NAVFundFrom);
                        cmd.Parameters.AddWithValue("@NAVFundTo", _clientSwitching.NAVFundTo);
                        cmd.Parameters.AddWithValue("@FundPKFrom", _clientSwitching.FundPKFrom);
                        cmd.Parameters.AddWithValue("@FundPKTo", _clientSwitching.FundPKTo);
                        cmd.Parameters.AddWithValue("@FundClientPK", _clientSwitching.FundClientPK);
                        cmd.Parameters.AddWithValue("@CashRefPKFrom", _clientSwitching.CashRefPKFrom);
                        cmd.Parameters.AddWithValue("@CashRefPKTo", _clientSwitching.CashRefPKTo);
                        cmd.Parameters.AddWithValue("@CurrencyPK", _clientSwitching.CurrencyPK);
                        cmd.Parameters.AddWithValue("@AgentPK", _clientSwitching.AgentPK);
                        cmd.Parameters.AddWithValue("@TransferType", _clientSwitching.TransferType);
                        cmd.Parameters.AddWithValue("@Description", _clientSwitching.Description);
                        cmd.Parameters.AddWithValue("@ReferenceSInvest", _clientSwitching.ReferenceSInvest);
                        cmd.Parameters.AddWithValue("@CashAmount", _clientSwitching.CashAmount);
                        cmd.Parameters.AddWithValue("@UnitAmount", _clientSwitching.UnitAmount);
                        cmd.Parameters.AddWithValue("@FeeType", _clientSwitching.FeeType);
                        cmd.Parameters.AddWithValue("@SwitchingFeePercent", _clientSwitching.SwitchingFeePercent);
                        cmd.Parameters.AddWithValue("@SwitchingFeeAmount", _clientSwitching.SwitchingFeeAmount);
                        cmd.Parameters.AddWithValue("@TotalCashAmountFundFrom", _clientSwitching.TotalCashAmountFundFrom);
                        cmd.Parameters.AddWithValue("@TotalCashAmountFundTo", _clientSwitching.TotalCashAmountFundTo);
                        cmd.Parameters.AddWithValue("@TotalUnitAmountFundFrom", _clientSwitching.TotalUnitAmountFundFrom);
                        cmd.Parameters.AddWithValue("@TotalUnitAmountFundTo", _clientSwitching.TotalUnitAmountFundTo);
                        cmd.Parameters.AddWithValue("@BitSwitchingAll", _clientSwitching.BitSwitchingAll);
                        cmd.Parameters.AddWithValue("@FeeTypeMethod", _clientSwitching.FeeTypeMethod);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _clientSwitching.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);

                        cmd.ExecuteNonQuery();

                        return _host.Get_LastPKByLastUpate(_dateTimeNow, "ClientSwitching");
                    }
                }

            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int ClientSwitching_Update(ClientSwitching _clientSwitching, bool _havePrivillege)
        {
            try
            {
                int _newHisPK;
                int _return = 0;
                int status = _host.Get_Status(_clientSwitching.ClientSwitchingPK, _clientSwitching.HistoryPK, "ClientSwitching");
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = @"Update ClientSwitching set status=2, Notes=@Notes,NAVDate=@NAVDate,ValueDate=@ValueDate,PaymentDate=@PaymentDate,Type=@Type,
                                NAVFundFrom=@NAVFundFrom,NAVFundTo=@NAVFundTo,FundPKFrom=@FundPKFrom,FundPKTo=@FundPKTo,FundClientPK=@FundClientPK,CashRefPKFrom=@CashRefPKFrom,CashRefPKTo=@CashRefPKTo,CurrencyPK=@CurrencyPK,Description=@Description,CashAmount=@CashAmount,UnitAmount=@UnitAmount,SwitchingFeePercent=@SwitchingFeePercent,SwitchingFeeAmount=@SwitchingFeeAmount,TotalCashAmountFundFrom=@TotalCashAmountFundFrom,TotalCashAmountFundTo=@TotalCashAmountFundTo,TotalUnitAmountFundFrom=@TotalUnitAmountFundFrom,TotalUnitAmountFundTo=@TotalUnitAmountFundTo,BitSwitchingAll=@BitSwitchingAll,TransferType=@TransferType,FeeType = @FeeType,
                                FeeTypeMethod=@FeeTypeMethod,ReferenceSInvest = @ReferenceSInvest,                                
                                ApprovedUsersID=@ApprovedUsersID, 
                                ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@LastUpdate 
                                where ClientSwitchingPK = @PK and historyPK = @HistoryPK";
                            cmd.Parameters.AddWithValue("@HistoryPK", _clientSwitching.HistoryPK);
                            cmd.Parameters.AddWithValue("@Notes", _clientSwitching.Notes);
                            cmd.Parameters.AddWithValue("@PK", _clientSwitching.ClientSwitchingPK);
                            cmd.Parameters.AddWithValue("@Type", _clientSwitching.Type);
                            cmd.Parameters.AddWithValue("@NAVDate", _clientSwitching.NAVDate);
                            cmd.Parameters.AddWithValue("@ValueDate", _clientSwitching.ValueDate);
                            cmd.Parameters.AddWithValue("@PaymentDate", _clientSwitching.PaymentDate);
                            cmd.Parameters.AddWithValue("@NAVFundFrom", _clientSwitching.NAVFundFrom);
                            cmd.Parameters.AddWithValue("@NAVFundTo", _clientSwitching.NAVFundTo);
                            cmd.Parameters.AddWithValue("@FundPKFrom", _clientSwitching.FundPKFrom);
                            cmd.Parameters.AddWithValue("@FundPKTo", _clientSwitching.FundPKTo);
                            cmd.Parameters.AddWithValue("@FundClientPK", _clientSwitching.FundClientPK);
                            cmd.Parameters.AddWithValue("@CashRefPKFrom", _clientSwitching.CashRefPKFrom);
                            cmd.Parameters.AddWithValue("@CashRefPKTo", _clientSwitching.CashRefPKTo);
                            cmd.Parameters.AddWithValue("@CurrencyPK", _clientSwitching.CurrencyPK);
                            cmd.Parameters.AddWithValue("@AgentPK", _clientSwitching.AgentPK);
                            cmd.Parameters.AddWithValue("@TransferType", _clientSwitching.TransferType);
                            cmd.Parameters.AddWithValue("@Description", _clientSwitching.Description);
                            cmd.Parameters.AddWithValue("@ReferenceSInvest", _clientSwitching.ReferenceSInvest);
                            cmd.Parameters.AddWithValue("@CashAmount", _clientSwitching.CashAmount);
                            cmd.Parameters.AddWithValue("@UnitAmount", _clientSwitching.UnitAmount);
                            cmd.Parameters.AddWithValue("@FeeType", _clientSwitching.FeeType);
                            cmd.Parameters.AddWithValue("@SwitchingFeePercent", _clientSwitching.SwitchingFeePercent);
                            cmd.Parameters.AddWithValue("@SwitchingFeeAmount", _clientSwitching.SwitchingFeeAmount);
                            cmd.Parameters.AddWithValue("@TotalCashAmountFundFrom", _clientSwitching.TotalCashAmountFundFrom);
                            cmd.Parameters.AddWithValue("@TotalCashAmountFundTo", _clientSwitching.TotalCashAmountFundTo);
                            cmd.Parameters.AddWithValue("@TotalUnitAmountFundFrom", _clientSwitching.TotalUnitAmountFundFrom);
                            cmd.Parameters.AddWithValue("@TotalUnitAmountFundTo", _clientSwitching.TotalUnitAmountFundTo);
                            cmd.Parameters.AddWithValue("@BitSwitchingAll", _clientSwitching.BitSwitchingAll);
                            cmd.Parameters.AddWithValue("@FeeTypeMethod", _clientSwitching.FeeTypeMethod);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _clientSwitching.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _dateTimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _clientSwitching.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);
                            cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);

                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update ClientSwitching set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@lastUpdate where ClientSwitchingPK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _clientSwitching.ClientSwitchingPK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _clientSwitching.EntryUsersID);
                            cmd.Parameters.AddWithValue("@VoidTime", _dateTimeNow);
                            cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                            cmd.ExecuteNonQuery();
                        }
                        _return = 0;
                    }
                    else
                    {


                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = @"
                                   Declare @LastNAV numeric(22,8)
		                           Declare @SwitchingUnit numeric(22,8)


                                    Declare @OldSwitchingUnit numeric(22,8)
                                    Declare @OldUnitAmount numeric(22,8)
                                    Declare @OldNAVDate datetime
                                    Declare @OldFundPK int
                                    Declare @OldFundClientPK int
                                    Declare @OldCashAmount numeric(24,4)
                                    Declare @OldNAV numeric(18,8)
                                    Declare @TrxFrom nvarchar(200) 
                                    declare @TransactionPK  nvarchar(200)
                                    Select @OldFundPK = FundPKFrom,@OldFundClientPK = FundClientPK, @OldNAVDate = NAVDate,@OldNAV = NAVFundFrom, @TrxFrom = EntryUsersID,@TransactionPK  = TransactionPK
                                    From ClientSwitching where ClientSwitchingPK = @PK and HistoryPK = @HistoryPK

                                    Select @OldUnitAmount = UnitChanges * -1 from FundClientPositionLog where FundClientPK = @OldFundClientPK and FundPK = @oldFundPK
                                    and ClientTransactionPK = @PK and TransactionType = 6 and ID =
                                    (
                                        Select max(ID) From FundClientPositionLog where FundClientPK = @OldFundClientPK and FundPK = @oldFundPK
                                                                         and ClientTransactionPK = @PK and TransactionType = 6
                                    )

                                    Set @OldCashAmount = 0


		                           Select @LastNAV  = NAV
		                           from CloseNAV where Date = 
		                           (
			                        Select Max(date) From CloseNAV Where FundPK = @OldFundPK and status = 2 and Date < @OldNAVDate
		                           ) and status = 2 and FundPK = @OldFundPK

		                           set @LastNAV = isnull(@LastNAV,1)
		                           set @OldSwitchingUnit = 0
		                           IF @OldCashAmount > 0 and @oldNAV = 0
		                           BEGIN
				                        set @OldSwitchingUnit = @OldCashAmount / @LastNAV
		                           END
		                           ELSE
		                           BEGIN
				                        set @OldSwitchingUnit = @OldUnitAmount
		                           END

                                    Declare @UnitPrevious numeric(22,8)
                                    Select @UnitPrevious = isnull(Unit,0) From FundClientPositionSummary
                                    where FundClientPK = @OldFundClientPK and FundPK = @OldFundPK

                                    update fundclientpositionsummary
		                           set Unit = Unit + @OldSwitchingUnit
		                           where FundClientPK = @OldFundClientPK and FundPK = @OldFundPK
                               
                                    --Buy = 1,
                                    --Sell = 2,
                                    --Adjustment = 3,
                                    --SwitchingIn = 5,
                                    --SwitchingOut = 6

                                    insert into [FundClientPositionLog]
                                               ([UserId]
                                               ,[FundClientPK]
                                               ,[TransactionPK]
                                               ,[ClientTransactionPK]
                                               ,[UnitPrevious]
                                               ,[UnitChanges]
                                               ,[UnitAfter]
                                               ,[IsBoTransaction]
                                               ,[TransactionType]
                                               ,[Description]
                                                ,[FundPK])
                                    Select @UpdateUsersID,@OldFundClientPK,@TransactionPK ,@PK,@UnitPrevious,@OldSwitchingUnit,@UnitPrevious + @OldSwitchingUnit
                                    ,Case when @TrxFrom = 'rdo' then 0 else 1 end,6,'Update Switching Out Old Data Revise',@OldFundPK
                                ";
                            cmd.Parameters.AddWithValue("@Notes", _clientSwitching.Notes);
                            cmd.Parameters.AddWithValue("@PK", _clientSwitching.ClientSwitchingPK);
                            cmd.Parameters.AddWithValue("@HistoryPK", _clientSwitching.HistoryPK);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _clientSwitching.EntryUsersID);
                            cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);


                            cmd.ExecuteNonQuery();
                        }



                        if (status == 1)
                        {
                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = @"Update ClientSwitching set Notes=@Notes,NAVDate=@NAVDate,ValueDate=@ValueDate,PaymentDate=@PaymentDate,AgentPK=@AgentPK,Type=@Type,
                                NAVFundFrom=@NAVFundFrom,NAVFundTo=@NAVFundTo,FundPKFrom=@FundPKFrom,FundPKTo=@FundPKTo,FundClientPK=@FundClientPK,CashRefPKFrom=@CashRefPKFrom,CashRefPKTo=@CashRefPKTo,CurrencyPK=@CurrencyPK,Description=@Description,CashAmount=@CashAmount,UnitAmount=@UnitAmount,SwitchingFeePercent=@SwitchingFeePercent,SwitchingFeeAmount=@SwitchingFeeAmount,TotalCashAmountFundFrom=@TotalCashAmountFundFrom,TotalCashAmountFundTo=@TotalCashAmountFundTo,TotalUnitAmountFundFrom=@TotalUnitAmountFundFrom,TotalUnitAmountFundTo=@TotalUnitAmountFundTo,BitSwitchingAll=@BitSwitchingAll,TransferType=@TransferType,
                                FeeTypeMethod=@FeeTypeMethod,ReferenceSInvest = @ReferenceSInvest,                          
                                UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@LastUpdate 
                                where ClientSwitchingPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _clientSwitching.HistoryPK);
                                cmd.Parameters.AddWithValue("@Notes", _clientSwitching.Notes);
                                cmd.Parameters.AddWithValue("@PK", _clientSwitching.ClientSwitchingPK);
                                cmd.Parameters.AddWithValue("@Type", _clientSwitching.Type);
                                cmd.Parameters.AddWithValue("@NAVDate", _clientSwitching.NAVDate);
                                cmd.Parameters.AddWithValue("@ValueDate", _clientSwitching.ValueDate);
                                cmd.Parameters.AddWithValue("@PaymentDate", _clientSwitching.PaymentDate);
                                cmd.Parameters.AddWithValue("@NAVFundFrom", _clientSwitching.NAVFundFrom);
                                cmd.Parameters.AddWithValue("@NAVFundTo", _clientSwitching.NAVFundTo);
                                cmd.Parameters.AddWithValue("@FundPKFrom", _clientSwitching.FundPKFrom);
                                cmd.Parameters.AddWithValue("@FundPKTo", _clientSwitching.FundPKTo);
                                cmd.Parameters.AddWithValue("@FundClientPK", _clientSwitching.FundClientPK);
                                cmd.Parameters.AddWithValue("@CashRefPKFrom", _clientSwitching.CashRefPKFrom);
                                cmd.Parameters.AddWithValue("@CashRefPKTo", _clientSwitching.CashRefPKTo);
                                cmd.Parameters.AddWithValue("@CurrencyPK", _clientSwitching.CurrencyPK);
                                cmd.Parameters.AddWithValue("@AgentPK", _clientSwitching.AgentPK);
                                cmd.Parameters.AddWithValue("@TransferType", _clientSwitching.TransferType);
                                cmd.Parameters.AddWithValue("@Description", _clientSwitching.Description);
                                cmd.Parameters.AddWithValue("@ReferenceSInvest", _clientSwitching.ReferenceSInvest);
                                cmd.Parameters.AddWithValue("@CashAmount", _clientSwitching.CashAmount);
                                cmd.Parameters.AddWithValue("@UnitAmount", _clientSwitching.UnitAmount);
                                cmd.Parameters.AddWithValue("@FeeType", _clientSwitching.FeeType);
                                cmd.Parameters.AddWithValue("@SwitchingFeePercent", _clientSwitching.SwitchingFeePercent);
                                cmd.Parameters.AddWithValue("@SwitchingFeeAmount", _clientSwitching.SwitchingFeeAmount);
                                cmd.Parameters.AddWithValue("@TotalCashAmountFundFrom", _clientSwitching.TotalCashAmountFundFrom);
                                cmd.Parameters.AddWithValue("@TotalCashAmountFundTo", _clientSwitching.TotalCashAmountFundTo);
                                cmd.Parameters.AddWithValue("@TotalUnitAmountFundFrom", _clientSwitching.TotalUnitAmountFundFrom);
                                cmd.Parameters.AddWithValue("@TotalUnitAmountFundTo", _clientSwitching.TotalUnitAmountFundTo);
                                cmd.Parameters.AddWithValue("@BitSwitchingAll", _clientSwitching.BitSwitchingAll);
                                cmd.Parameters.AddWithValue("@FeeTypeMethod", _clientSwitching.FeeTypeMethod);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _clientSwitching.EntryUsersID);
                                cmd.Parameters.AddWithValue("@Updatetime", _dateTimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);

                                cmd.ExecuteNonQuery();
                            }
                            _return = 0;
                        }
                        else
                        {
                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                _newHisPK = _host.Get_NewHistoryPK(_clientSwitching.ClientSwitchingPK, "ClientSwitching");
                                cmd.CommandText = _insertCommandSwitching + "UserSwitchingPK,TransactionPK,[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                   "Select @PK,@NewHistoryPK,1," + _paramaterCommandSwitching + "@UserSwitchingPK,@TransactionPK,EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From ClientSwitching where ClientSwitchingPK =@PK and historyPK = @HistoryPK ";
                                cmd.Parameters.AddWithValue("@PK", _clientSwitching.ClientSwitchingPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _clientSwitching.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@Type", _clientSwitching.Type);
                                cmd.Parameters.AddWithValue("@NAVDate", _clientSwitching.NAVDate);
                                cmd.Parameters.AddWithValue("@ValueDate", _clientSwitching.ValueDate);
                                cmd.Parameters.AddWithValue("@PaymentDate", _clientSwitching.PaymentDate);
                                cmd.Parameters.AddWithValue("@NAVFundFrom", _clientSwitching.NAVFundFrom);
                                cmd.Parameters.AddWithValue("@NAVFundTo", _clientSwitching.NAVFundTo);
                                cmd.Parameters.AddWithValue("@FundPKFrom", _clientSwitching.FundPKFrom);
                                cmd.Parameters.AddWithValue("@FundPKTo", _clientSwitching.FundPKTo);
                                cmd.Parameters.AddWithValue("@FundClientPK", _clientSwitching.FundClientPK);
                                cmd.Parameters.AddWithValue("@CashRefPKFrom", _clientSwitching.CashRefPKFrom);
                                cmd.Parameters.AddWithValue("@CashRefPKTo", _clientSwitching.CashRefPKTo);
                                cmd.Parameters.AddWithValue("@CurrencyPK", _clientSwitching.CurrencyPK);
                                cmd.Parameters.AddWithValue("@AgentPK", _clientSwitching.AgentPK);
                                cmd.Parameters.AddWithValue("@TransferType", _clientSwitching.TransferType);
                                cmd.Parameters.AddWithValue("@Description", _clientSwitching.Description);
                                cmd.Parameters.AddWithValue("@ReferenceSInvest", _clientSwitching.ReferenceSInvest);
                                cmd.Parameters.AddWithValue("@CashAmount", _clientSwitching.CashAmount);
                                cmd.Parameters.AddWithValue("@UnitAmount", _clientSwitching.UnitAmount);
                                cmd.Parameters.AddWithValue("@FeeType", _clientSwitching.FeeType);
                                cmd.Parameters.AddWithValue("@SwitchingFeePercent", _clientSwitching.SwitchingFeePercent);
                                cmd.Parameters.AddWithValue("@SwitchingFeeAmount", _clientSwitching.SwitchingFeeAmount);
                                cmd.Parameters.AddWithValue("@TotalCashAmountFundFrom", _clientSwitching.TotalCashAmountFundFrom);
                                cmd.Parameters.AddWithValue("@TotalCashAmountFundTo", _clientSwitching.TotalCashAmountFundTo);
                                cmd.Parameters.AddWithValue("@TotalUnitAmountFundFrom", _clientSwitching.TotalUnitAmountFundFrom);
                                cmd.Parameters.AddWithValue("@TotalUnitAmountFundTo", _clientSwitching.TotalUnitAmountFundTo);
                                cmd.Parameters.AddWithValue("@BitSwitchingAll", _clientSwitching.BitSwitchingAll);
                                cmd.Parameters.AddWithValue("@FeeTypeMethod", _clientSwitching.FeeTypeMethod);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _clientSwitching.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _dateTimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                                cmd.Parameters.AddWithValue("@TransactionPK", _clientSwitching.TransactionPK);
                                cmd.Parameters.AddWithValue("@UserSwitchingPK", _clientSwitching.UserSwitchingPK);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update ClientSwitching set status= 4,Notes=@Notes, " +
                                    " LastUpdate=@LastUpdate where ClientSwitchingPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _clientSwitching.Notes);
                                cmd.Parameters.AddWithValue("@PK", _clientSwitching.ClientSwitchingPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _clientSwitching.HistoryPK);
                                cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                                cmd.ExecuteNonQuery();
                            }
                            _return = _newHisPK;
                        }
                    }



                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"
                                  Declare @LastNAV numeric(22,8)
		                           Declare @SwitchingUnit numeric(22,8)
                                    Declare @TrxFrom nvarchar(200)
                                    declare @TransactionPK  nvarchar(200)
                                    Select @TrxFrom = EntryUsersID,@TransactionPK  = TransactionPK from ClientSwitching where ClientSwitchingPK = @PK and HistoryPK = @HistoryPK
                                  Select @LastNAV  = NAV
		                           from CloseNAV where Date = 
		                           (
			                        Select Max(date) From CloseNAV Where FundPK = @FundPKFrom and status = 2 and Date < @NAVDate
		                           ) and status = 2 and FundPK = @FundPKFrom

		                           set @LastNAV = isnull(@LastNAV,1)
		                           set @SwitchingUnit = 0
		                           IF @CashAmount > 0 and @NAVFundFrom = 0
		                           BEGIN
				                        set @SwitchingUnit = @CashAmount / @LastNAV
		                           END
		                           ELSE
		                           BEGIN
				                        set @SwitchingUnit = @UnitAmount
		                           END
                                   set @SwitchingUnit = @SwitchingUnit * -1
                                    Declare @UnitPrevious numeric(22,8)
                                    Select @UnitPrevious = isnull(Unit,0) From FundClientPositionSummary
                                    where FundClientPK = @FundClientPK and FundPK = @FundPKFrom

                                    update fundclientpositionsummary
		                           set Unit = Unit + @SwitchingUnit
		                           where FundClientPK = @FundClientPK and FundPK = @FundPKFrom
                               
                                    --Buy = 1,
                                    --Sell = 2,
                                    --Adjustment = 3,
                                    --SwitchingIn = 5,
                                    --SwitchingOut = 6

                                    insert into [FundClientPositionLog]
                                               ([UserId]
                                               ,[FundClientPK]
                                               ,[TransactionPK]
                                               ,[ClientTransactionPK]
                                               ,[UnitPrevious]
                                               ,[UnitChanges]
                                               ,[UnitAfter]
                                               ,[IsBoTransaction]
                                               ,[TransactionType]
                                              ,[Description]
                                                ,[FundPK])
                                    Select @UpdateUsersID,@FundClientPK,@TransactionPK ,@PK,@UnitPrevious,@SwitchingUnit,@UnitPrevious + @SwitchingUnit
                                    ,Case when @TrxFrom = 'rdo' then 0 else 1 end,6,'Update Switching Out',@FundPKFrom

                                ";
                        cmd.Parameters.AddWithValue("@Notes", _clientSwitching.Notes);
                        cmd.Parameters.AddWithValue("@PK", _clientSwitching.ClientSwitchingPK);
                        cmd.Parameters.AddWithValue("@HistoryPK", _clientSwitching.HistoryPK);
                        cmd.Parameters.AddWithValue("@UpdateUsersID", _clientSwitching.EntryUsersID);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);

                        cmd.Parameters.AddWithValue("@FundPKFrom", _clientSwitching.FundPKFrom);
                        cmd.Parameters.AddWithValue("@FundClientPK", _clientSwitching.FundClientPK);
                        cmd.Parameters.AddWithValue("@NAVDate", _clientSwitching.NAVDate);
                        cmd.Parameters.AddWithValue("@NAVFundFrom", _clientSwitching.NAVFundFrom);
                        cmd.Parameters.AddWithValue("@CashAmount", _clientSwitching.CashAmount);
                        cmd.Parameters.AddWithValue("@UnitAmount", _clientSwitching.UnitAmount);

                        cmd.ExecuteNonQuery();
                    }
                    return _return;
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }
        public void ClientSwitching_RejectBySelected(string _usersID, string _permissionID, DateTime _dateFrom, DateTime _dateTo)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"Declare @IPAddress nvarchar(50) select @IPAddress = IPAddress from Users where ID = @UsersID and Status = 2 
                                          Insert into Activity(Time,PermissionID,ObjectTable,ObjectTablePK,Status,Message,UsersID,IPAddress,LastUpdate) 
                                          Select @Time,@PermissionID,'ClientSwitching',ClientSwitchingPK,1,'Reject by Selected Data',@UsersID,@IPAddress,@Time  from ClientSwitching where ValueDate between @DateFrom and @DateTo and Status = 1 and Selected  = 1 
                                         
                           Declare @PK int
                            Declare @HistoryPK int

                            Declare A Cursor For
	                            Select ClientSwitchingPK,historyPK from ClientSwitching where ValueDate between @DateFrom and @DateTo and Status = 1 and Selected  = 1
                            Open A
                            Fetch next From A
                            into @PK,@HistoryPK
                            While @@Fetch_status = 0
                            Begin

				                            Declare @LastNAV numeric(22,8)
		                                    Declare @SwitchingUnit numeric(22,8)


                                            Declare @OldSwitchingUnit numeric(22,8)
                                            Declare @OldUnitAmount numeric(22,8)
                                            Declare @OldNAVDate datetime
                                            Declare @OldFundPK int
                                            Declare @OldFundClientPK int
                                            Declare @OldCashAmount numeric(24,4)
                                            Declare @TrxFrom nvarchar(200)
                                            Declare @TransactionPK nvarchar(200)
                                            Select @OldFundPK = FundPKFrom,@OldFundClientPK = FundClientPK, @OldNAVDate = NAVDate, @TrxFrom = EntryUsersID,@TransactionPK = TransactionPK
                                            From ClientSwitching where ClientSwitchingPK = @PK and HistoryPK = @HistoryPK

                                            Select @OldUnitAmount = UnitChanges * -1 from FundClientPositionLog where FundClientPK = @OldFundClientPK and FundPK = @oldFundPK
                                            and ClientTransactionPK = @PK and TransactionType = 6 and ID = 
                                        (
                                            Select max(ID) From FundClientPositionLog where FundClientPK = @OldFundClientPK and FundPK = @oldFundPK
                                                                             and ClientTransactionPK = @PK and TransactionType = 6
                                        )

                                            Set @OldCashAmount = 0
				                            set @OldSwitchingUnit = @OldUnitAmount

                                            Declare @UnitPrevious numeric(22,8)
                                            Select @UnitPrevious = isnull(Unit,0) From FundClientPositionSummary
                                            where FundClientPK = @OldFundClientPK and FundPK = @OldFundPK

                                            update fundclientpositionsummary
		                                    set Unit = Unit + isnull(@OldSwitchingUnit,0)
		                                    where FundClientPK = @OldFundClientPK and FundPK = @OldFundPK
                               
                                            --Buy = 1,
                                            --Sell = 2,
                                            --Adjustment = 3,
                                            --SwitchingIn = 5,
                                            --SwitchingOut = 6

                                            insert into [FundClientPositionLog]
                                                        ([UserId]
                                                        ,[FundClientPK]
                                                        ,[TransactionPK]
                                                        ,[ClientTransactionPK]
                                                        ,[UnitPrevious]
                                                        ,[UnitChanges]
                                                        ,[UnitAfter]
                                                        ,[IsBoTransaction]
                                                        ,[TransactionType]
                                                        ,[Description]
                                                        ,[FundPK])
                                            Select @UsersID,@OldFundClientPK,@TransactionPK,@PK,@UnitPrevious,@OldSwitchingUnit,@UnitPrevious + @OldSwitchingUnit,
                                            Case when @TrxFrom = 'rdo' then 0 else 1 end,2,'Reject Switching Out',@OldFundPK


                            fetch next From A into @PK,@HistoryPK
                            end
                            Close A
                            Deallocate A

                                          update ClientSwitching set status = 3,selected = 0,VoidUsersID = @UsersID,VoidTime = @Time,LastUpdate=@Time where status = 1 and ClientSwitchingPK in ( Select ClientSwitchingPK from ClientSwitching where ValueDate between @DateFrom and @DateTo and Status = 1 and Selected  = 1 ) 
                                          Update ClientSwitching set status= 2  where status = 4 and ClientSwitchingPK in (Select ClientSwitchingPK from ClientSwitching where ValueDate between @DateFrom and @DateTo and Status = 4 and Selected  = 1) ";

                        cmd.Parameters.AddWithValue("@PermissionID", _permissionID);
                        cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _dateTo);
                        cmd.Parameters.AddWithValue("@UsersID", _usersID);
                        cmd.Parameters.AddWithValue("@Time", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public void ClientSwitching_PostingBySelected(string _usersID, DateTime _dateFrom, DateTime _dateTo)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText =
                           @"
declare @CashAmountFundFrom numeric(22,2)
declare @CashAmountFundTo numeric(22,2)
declare @SwitchingFeeAmount numeric(22,2)
declare @TotalCashAmountFundFrom numeric(22,2)
declare @TotalCashAmountFundTo numeric(22,2)
declare @TotalUnitAmountFundFrom numeric(22,4)
declare @TotalUnitAmountFundTo numeric(22,4)
declare @ValueDate datetime
declare @FundClientPK int
declare @FundPKFrom int
declare @FundPKTo int
declare @ClientSwitchingPK int
Declare @HistoryPK int
declare @TrxFrom nvarchar(200)
declare @TransactionPK nvarchar(200)
Declare @FeeType nvarchar(100)
Declare @FundClientID nvarchar(200)
Declare @FundClientName nvarchar(200)
Declare @NAVDate datetime
Declare @PaymentDate datetime
Declare @UnitAmount numeric(22,4)
Declare @CType int

declare @BankPK int
declare @BankCurrencyPK int


Declare @SubscriptionAcc int Declare @PayableSubsAcc int Declare  @PendingSubscription int
Declare @RedemptionAcc int Declare @PayableRedemptionAcc int Declare @PayableSwitchingFee int

Declare @PeriodPK int 
Declare @FundjournalPK int

Select @PeriodPK = PeriodPK From Period Where @DateFrom Between DateFrom and DateTo and Status = 2     

Select @SubscriptionAcc = Subscription,@PendingSubscription = PendingSubscription,@PayableSubsAcc = payablesubscriptionfee 
,@RedemptionAcc = Redemption, @PayableRedemptionAcc = PendingRedemption
,@PayableSwitchingFee = PayableSwitchingFee
From Fundaccountingsetup where status = 2   

DECLARE A CURSOR FOR 
select TotalCashAmountFundFrom,TotalCashAmountFundTo, TotalUnitAmountFundFrom,
TotalUnitAmountFundTo,FundPKFrom, FundPKTo,A.FundClientPK,ValueDate, ClientSwitchingPK, A.EntryUsersID ,TransactionPK, A.HistoryPK,FeeType
,SwitchingFeeAmount,PaymentDate,NAVDate,B.ID,B.Name,CashAmount,UnitAmount,A.Type
from ClientSwitching A
left join FundClient B on A.FundClientPK = B.FundClientPK and B.status = 2
where  A.Status = 2 
and A.Selected = 1 and Posted  = 0 and Revised  = 0  and ValueDate between @datefrom and @dateto
	
Open A
Fetch Next From A
Into @TotalCashAmountFundFrom,@TotalCashAmountFundTo, @TotalUnitAmountFundFrom,@TotalUnitAmountFundTo,@FundPKFrom, @FundPKTo,@FundClientPK
,@ValueDate,@ClientSwitchingPK,@TrxFrom, @TransactionPK,@HistoryPK,@FeeType,@SwitchingFeeAmount,@PaymentDate,@NAVDate
,@FundClientID,@FundClientName,@CashAmountFundFrom,@UnitAmount,@CType


While @@FETCH_STATUS = 0
BEGIN

  set @BankPK = 3
  set @BankCurrencyPK = 1    

if @FeeType = 'OUT'  and @CType not in(3,6)
BEGIN
		set @NAVDate = dbo.fworkingday(@NAVDate,1)
		---------------- FUND OUT
	  select @FundJournalPK = isnull(max(FundJournalPK),0) + 1 From FundJournal 

        INSERT INTO [dbo].[FundJournal] ([FundJournalPK],[HistoryPK],[Status],[Notes],[PeriodPK],[ValueDate],[Type],[TrxNo],[TrxName],[Reference] ,[Description],[Posted],[PostedBy],[PostedTime],[EntryUsersID],[EntryTime],[LastUpdate]) 
	    Select	   @FundJournalPK, 1,2,'Posting Client Switching Out',@PeriodPK,@NAVDate,13,@ClientSwitchingPK,'SWITCHING', '','Switching Client: ' + @FundClientID + ' - ' + @FundClientName,1,@PostedBy,@PostedTime,@PostedBy,@PostedTime,@PostedTime
	 
	 
	    INSERT INTO [dbo].[FundJournalDetail]  ([FundJournalPK],[AutoNo],[HistoryPK],[Status],[FundJournalAccountPK],[CurrencyPK],[FundPK],[FundClientPK],[InstrumentPK],[DetailDescription] ,[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])	    
        Select	   @FundJournalPK,1,1,2,@RedemptionAcc,1,@FundPKFrom,@FundClientPK,0,'switching Client: ' + @FundClientID + ' - ' + @FundClientName,'D',@CashAmountFundFrom,@CashAmountFundFrom,0,1,@CashAmountFundFrom,0,@PostedTime 
	  
	  
	    INSERT INTO [dbo].[FundJournalDetail]([FundJournalPK],[AutoNo],[HistoryPK],[Status],[FundJournalAccountPK],[CurrencyPK],[FundPK],[FundClientPK],[InstrumentPK],[DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])
	    Select	   @FundJournalPK,2,1,2,@PayableRedemptionAcc,1,@FundPKFrom,@FundClientPK,0,'switching Client: ' + @FundClientID + ' - ' + @FundClientName,'C',@TotalCashAmountFundFrom,0,@TotalCashAmountFundFrom,1,0,@TotalCashAmountFundFrom,@PostedTime 

    if @SwitchingFeeAmount > 0
    begin

        INSERT INTO [dbo].[FundJournalDetail]([FundJournalPK],[AutoNo],[HistoryPK],[Status],[FundJournalAccountPK],[CurrencyPK],[FundPK],[FundClientPK],[InstrumentPK],[DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])
	    Select	   @FundJournalPK,3,1,2,@PayableSwitchingFee,1,@FundPKFrom,@FundClientPK,0,'switching Client: ' + @FundClientID + ' - ' + @FundClientName,'C',@SwitchingFeeAmount,0,@SwitchingFeeAmount,1,0,@SwitchingFeeAmount,@PostedTime 
    end

	  select @FundJournalPK = isnull(max(FundJournalPK),0) + 1 From FundJournal 

        INSERT INTO [dbo].[FundJournal] ([FundJournalPK],[HistoryPK],[Status],[Notes],[PeriodPK],[ValueDate],[Type],[TrxNo],[TrxName],[Reference] ,[Description],[Posted],[PostedBy],[PostedTime],[EntryUsersID],[EntryTime],[LastUpdate]) 
	    Select	   @FundJournalPK, 1,2,'Posting Client Switching Out Payment Date',@PeriodPK,@PaymentDate,13,@ClientSwitchingPK,'SWITCHING', '','Switching Client: ' + @FundClientID + ' - ' + @FundClientName,1,@PostedBy,@PostedTime,@PostedBy,@PostedTime,@PostedTime

		  INSERT INTO [dbo].[FundJournalDetail]  ([FundJournalPK],[AutoNo],[HistoryPK],[Status],[FundJournalAccountPK],[CurrencyPK],[FundPK],[FundClientPK],[InstrumentPK],[DetailDescription] ,[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])	    
        Select	   @FundJournalPK,1,1,2,@PayableRedemptionAcc,1,@FundPKFrom,@FundClientPK,0,'switching Client: ' + @FundClientID + ' - ' + @FundClientName,'D',@TotalCashAmountFundFrom,@TotalCashAmountFundFrom,0,1,@TotalCashAmountFundFrom,0,@PostedTime 
	  
	  
	    INSERT INTO [dbo].[FundJournalDetail]([FundJournalPK],[AutoNo],[HistoryPK],[Status],[FundJournalAccountPK],[CurrencyPK],[FundPK],[FundClientPK],[InstrumentPK],[DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])
	    Select	   @FundJournalPK,2,1,2,@BankPK,1,@FundPKFrom,@FundClientPK,0,'switching Client: ' + @FundClientID + ' - ' + @FundClientName,'C',@TotalCashAmountFundFrom,0,@TotalCashAmountFundFrom,1,0,@TotalCashAmountFundFrom,@PostedTime 



		------------FUND IN
		  select @FundJournalPK = isnull(max(FundJournalPK),0) + 1 From FundJournal 

        INSERT INTO [dbo].[FundJournal] ([FundJournalPK],[HistoryPK],[Status],[Notes],[PeriodPK],[ValueDate],[Type],[TrxNo],[TrxName],[Reference] ,[Description],[Posted],[PostedBy],[PostedTime],[EntryUsersID],[EntryTime],[LastUpdate]) 
	    Select	   @FundJournalPK, 1,2,'Posting Client Switching in',@PeriodPK,@NAVDate,13,@ClientSwitchingPK,'SWITCHING', '','Switching Client: ' + @FundClientID + ' - ' + @FundClientName,1,@PostedBy,@PostedTime,@PostedBy,@PostedTime,@PostedTime

		 INSERT INTO [dbo].[FundJournalDetail]  ([FundJournalPK],[AutoNo],[HistoryPK],[Status],[FundJournalAccountPK],[CurrencyPK],[FundPK],[FundClientPK],[InstrumentPK],[DetailDescription] ,[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])	    
        Select	   @FundJournalPK,1,1,2,@PendingSubscription,1,@FundPKTo,@FundClientPK,0,'switching Client: ' + @FundClientID + ' - ' + @FundClientName,'D',@TotalCashAmountFundTo,@TotalCashAmountFundTo,0,1,@TotalCashAmountFundTo,0,@PostedTime 
	  
	  
	    INSERT INTO [dbo].[FundJournalDetail]([FundJournalPK],[AutoNo],[HistoryPK],[Status],[FundJournalAccountPK],[CurrencyPK],[FundPK],[FundClientPK],[InstrumentPK],[DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])
	    Select	   @FundJournalPK,2,1,2,@SubscriptionAcc,1,@FundPKTo,@FundClientPK,0,'switching Client: ' + @FundClientID + ' - ' + @FundClientName,'C',@TotalCashAmountFundTo,0,@TotalCashAmountFundTo,1,0,@TotalCashAmountFundTo,@PostedTime 

		  select @FundJournalPK = isnull(max(FundJournalPK),0) + 1 From FundJournal 

        INSERT INTO [dbo].[FundJournal] ([FundJournalPK],[HistoryPK],[Status],[Notes],[PeriodPK],[ValueDate],[Type],[TrxNo],[TrxName],[Reference] ,[Description],[Posted],[PostedBy],[PostedTime],[EntryUsersID],[EntryTime],[LastUpdate]) 
	    Select	   @FundJournalPK, 1,2,'Posting Client Switching in payment date',@PeriodPK,@PaymentDate,13,@ClientSwitchingPK,'SWITCHING', '','Switching Client: ' + @FundClientID + ' - ' + @FundClientName,1,@PostedBy,@PostedTime,@PostedBy,@PostedTime,@PostedTime

		 INSERT INTO [dbo].[FundJournalDetail]  ([FundJournalPK],[AutoNo],[HistoryPK],[Status],[FundJournalAccountPK],[CurrencyPK],[FundPK],[FundClientPK],[InstrumentPK],[DetailDescription] ,[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])	    
        Select	   @FundJournalPK,1,1,2,@BankPK,1,@FundPKTo,@FundClientPK,0,'switching Client: ' + @FundClientID + ' - ' + @FundClientName,'D',@TotalCashAmountFundTo,@TotalCashAmountFundTo,0,1,@TotalCashAmountFundTo,0,@PostedTime 
	  
	  
	    INSERT INTO [dbo].[FundJournalDetail]([FundJournalPK],[AutoNo],[HistoryPK],[Status],[FundJournalAccountPK],[CurrencyPK],[FundPK],[FundClientPK],[InstrumentPK],[DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])
	    Select	   @FundJournalPK,2,1,2,@PendingSubscription,1,@FundPKTo,@FundClientPK,0,'switching Client: ' + @FundClientID + ' - ' + @FundClientName,'C',@TotalCashAmountFundTo,0,@TotalCashAmountFundTo,1,0,@TotalCashAmountFundTo,@PostedTime 

END


if @FeeType = 'IN' and @CType not in(3,6)
BEGIN
		---------------- FUND OUT
		  select @FundJournalPK = isnull(max(FundJournalPK),0) + 1 From FundJournal 

			INSERT INTO [dbo].[FundJournal] ([FundJournalPK],[HistoryPK],[Status],[Notes],[PeriodPK],[ValueDate],[Type],[TrxNo],[TrxName],[Reference] ,[Description],[Posted],[PostedBy],[PostedTime],[EntryUsersID],[EntryTime],[LastUpdate]) 
			Select	   @FundJournalPK, 1,2,'Posting Client Switching Out',@PeriodPK,@NAVDate,13,@ClientSwitchingPK,'SWITCHING', '','Switching Client: ' + @FundClientID + ' - ' + @FundClientName,1,@PostedBy,@PostedTime,@PostedBy,@PostedTime,@PostedTime
	 
	 
			INSERT INTO [dbo].[FundJournalDetail]  ([FundJournalPK],[AutoNo],[HistoryPK],[Status],[FundJournalAccountPK],[CurrencyPK],[FundPK],[FundClientPK],[InstrumentPK],[DetailDescription] ,[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])	    
			Select	   @FundJournalPK,1,1,2,@RedemptionAcc,1,@FundPKFrom,@FundClientPK,0,'switching Client: ' + @FundClientID + ' - ' + @FundClientName,'D',@TotalCashAmountFundFrom,@TotalCashAmountFundFrom,0,1,@TotalCashAmountFundFrom,0,@PostedTime 
	  
	  
			INSERT INTO [dbo].[FundJournalDetail]([FundJournalPK],[AutoNo],[HistoryPK],[Status],[FundJournalAccountPK],[CurrencyPK],[FundPK],[FundClientPK],[InstrumentPK],[DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])
			Select	   @FundJournalPK,2,1,2,@PayableRedemptionAcc,1,@FundPKFrom,@FundClientPK,0,'switching Client: ' + @FundClientID + ' - ' + @FundClientName,'C',@TotalCashAmountFundFrom,0,@TotalCashAmountFundFrom,1,0,@TotalCashAmountFundFrom,@PostedTime 

		

		  select @FundJournalPK = isnull(max(FundJournalPK),0) + 1 From FundJournal 

			INSERT INTO [dbo].[FundJournal] ([FundJournalPK],[HistoryPK],[Status],[Notes],[PeriodPK],[ValueDate],[Type],[TrxNo],[TrxName],[Reference] ,[Description],[Posted],[PostedBy],[PostedTime],[EntryUsersID],[EntryTime],[LastUpdate]) 
			Select	   @FundJournalPK, 1,2,'Posting Client Switching Out Payment Date',@PeriodPK,@PaymentDate,13,@ClientSwitchingPK,'SWITCHING', '','Switching Client: ' + @FundClientID + ' - ' + @FundClientName,1,@PostedBy,@PostedTime,@PostedBy,@PostedTime,@PostedTime

			  INSERT INTO [dbo].[FundJournalDetail]  ([FundJournalPK],[AutoNo],[HistoryPK],[Status],[FundJournalAccountPK],[CurrencyPK],[FundPK],[FundClientPK],[InstrumentPK],[DetailDescription] ,[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])	    
			Select	   @FundJournalPK,1,1,2,@PayableRedemptionAcc,1,@FundPKFrom,@FundClientPK,0,'switching Client: ' + @FundClientID + ' - ' + @FundClientName,'D',@TotalCashAmountFundFrom,@TotalCashAmountFundFrom,0,1,@TotalCashAmountFundFrom,0,@PostedTime 
	  
	  
			INSERT INTO [dbo].[FundJournalDetail]([FundJournalPK],[AutoNo],[HistoryPK],[Status],[FundJournalAccountPK],[CurrencyPK],[FundPK],[FundClientPK],[InstrumentPK],[DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])
			Select	   @FundJournalPK,2,1,2,@BankPK,1,@FundPKFrom,@FundClientPK,0,'switching Client: ' + @FundClientID + ' - ' + @FundClientName,'C',@TotalCashAmountFundFrom,0,@TotalCashAmountFundFrom,1,0,@TotalCashAmountFundFrom,@PostedTime 

			------------FUND IN
		  select @FundJournalPK = isnull(max(FundJournalPK),0) + 1 From FundJournal 

        INSERT INTO [dbo].[FundJournal] ([FundJournalPK],[HistoryPK],[Status],[Notes],[PeriodPK],[ValueDate],[Type],[TrxNo],[TrxName],[Reference] ,[Description],[Posted],[PostedBy],[PostedTime],[EntryUsersID],[EntryTime],[LastUpdate]) 
	    Select	   @FundJournalPK, 1,2,'Posting Client Switching in',@PeriodPK,@NAVDate,13,@ClientSwitchingPK,'SWITCHING', '','Switching Client: ' + @FundClientID + ' - ' + @FundClientName,1,@PostedBy,@PostedTime,@PostedBy,@PostedTime,@PostedTime

		 INSERT INTO [dbo].[FundJournalDetail]  ([FundJournalPK],[AutoNo],[HistoryPK],[Status],[FundJournalAccountPK],[CurrencyPK],[FundPK],[FundClientPK],[InstrumentPK],[DetailDescription] ,[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])	    
        Select	   @FundJournalPK,1,1,2,@PendingSubscription,1,@FundPKTo,@FundClientPK,0,'switching Client: ' + @FundClientID + ' - ' + @FundClientName,'D',@CashAmountFundFrom,@CashAmountFundFrom,0,1,@CashAmountFundFrom,0,@PostedTime 
	  
	  
	    INSERT INTO [dbo].[FundJournalDetail]([FundJournalPK],[AutoNo],[HistoryPK],[Status],[FundJournalAccountPK],[CurrencyPK],[FundPK],[FundClientPK],[InstrumentPK],[DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])
	    Select	   @FundJournalPK,2,1,2,@SubscriptionAcc,1,@FundPKTo,@FundClientPK,0,'switching Client: ' + @FundClientID + ' - ' + @FundClientName,'C',@TotalCashAmountFundTo,0,@TotalCashAmountFundTo,1,0,@TotalCashAmountFundTo,@PostedTime 


		  if @SwitchingFeeAmount > 0
    begin

        INSERT INTO [dbo].[FundJournalDetail]([FundJournalPK],[AutoNo],[HistoryPK],[Status],[FundJournalAccountPK],[CurrencyPK],[FundPK],[FundClientPK],[InstrumentPK],[DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])
	    Select	   @FundJournalPK,3,1,2,@PayableSwitchingFee,1,@FundPKTo,@FundClientPK,0,'switching Client: ' + @FundClientID + ' - ' + @FundClientName,'C',@SwitchingFeeAmount,0,@SwitchingFeeAmount,1,0,@SwitchingFeeAmount,@PostedTime 
    end


		  select @FundJournalPK = isnull(max(FundJournalPK),0) + 1 From FundJournal 

        INSERT INTO [dbo].[FundJournal] ([FundJournalPK],[HistoryPK],[Status],[Notes],[PeriodPK],[ValueDate],[Type],[TrxNo],[TrxName],[Reference] ,[Description],[Posted],[PostedBy],[PostedTime],[EntryUsersID],[EntryTime],[LastUpdate]) 
	    Select	   @FundJournalPK, 1,2,'Posting Client Switching in payment date',@PeriodPK,@PaymentDate,13,@ClientSwitchingPK,'SWITCHING', '','Switching Client: ' + @FundClientID + ' - ' + @FundClientName,1,@PostedBy,@PostedTime,@PostedBy,@PostedTime,@PostedTime

		 INSERT INTO [dbo].[FundJournalDetail]  ([FundJournalPK],[AutoNo],[HistoryPK],[Status],[FundJournalAccountPK],[CurrencyPK],[FundPK],[FundClientPK],[InstrumentPK],[DetailDescription] ,[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])	    
        Select	   @FundJournalPK,1,1,2,@BankPK,1,@FundPKTo,@FundClientPK,0,'switching Client: ' + @FundClientID + ' - ' + @FundClientName,'D',@TotalCashAmountFundTo,@TotalCashAmountFundTo,0,1,@TotalCashAmountFundTo,0,@PostedTime 
	  
	  
	    INSERT INTO [dbo].[FundJournalDetail]([FundJournalPK],[AutoNo],[HistoryPK],[Status],[FundJournalAccountPK],[CurrencyPK],[FundPK],[FundClientPK],[InstrumentPK],[DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])
	    Select	   @FundJournalPK,2,1,2,@PendingSubscription,1,@FundPKTo,@FundClientPK,0,'switching Client: ' + @FundClientID + ' - ' + @FundClientName,'C',@TotalCashAmountFundTo,0,@TotalCashAmountFundTo,1,0,@TotalCashAmountFundTo,@PostedTime 


END


if Exists(Select *from FundClientPosition where date = @ValueDate and FundClientPK = @FundClientPK and FundPK = @FundPKTo)
begin 
Update FundClientPosition set CashAmount = CashAmount + @TotalCashAmountFundTo, 
UnitAmount = UnitAmount + @TotalUnitAmountFundTo where Date = @ValueDate and FundClientPK = @FundClientPK 
and FundPK = @FundPKTo 
end 
else 
begin
INSERT INTO  FundClientPosition (Date,FundClientPK,FundPK,CashAmount,UnitAmount) 
select @ValueDate,@FundClientPK,@FundPKTo,@TotalCashAmountFundTo,@TotalUnitAmountFundTo 
end


Update FundClientPosition set  
UnitAmount = UnitAmount - @UnitAmount where Date = @ValueDate and FundClientPK = @FundClientPK 
and FundPK = @FundPKFrom 

        Declare @LastNAV numeric(22,8)
		Declare @SubsUnit numeric(22,8)

        set @SubsUnit = 0		                        
		set @SubsUnit = @TotalUnitAmountFundTo
		                          

        Declare @UnitPrevious numeric(22,8)
set @UnitPrevious = 0
        Select @UnitPrevious = isnull(Unit,0) From FundClientPositionSummary
        where FundClientPK = @FundClientPK and FundPK = @FundPKTo
        set @UnitPrevious = isnull(@UnitPrevious,0)
                               

        if exists(
            Select * from FundClientPositionSummary where FundClientPK = @FundClientPK and FundPK = @FundPKTo
        )BEGIN
        update fundclientpositionsummary
		set Unit = Unit + @SubsUnit
		where FundClientPK = @FundClientPK and FundPK = @FundPKTo
        END
        ELSE
        BEGIN
                Insert into FundClientPositionSummary (FundPK,FundClientPK,Unit)
            Select @FundPKTo,@FundClientPK,@SubsUnit
        END


                                
                               
        --Buy = 1,
        --Sell = 2,
        --Adjustment = 3,
        --SwitchingIn = 5,
        --SwitchingOut = 6

        insert into [FundClientPositionLog]
                    ([UserId]
                    ,[FundClientPK]
                    ,[TransactionPK]
                    ,[ClientTransactionPK]
                    ,[UnitPrevious]
                    ,[UnitChanges]
                    ,[UnitAfter]
                    ,[IsBoTransaction]
                    ,[TransactionType]
                    ,[Description]
                    ,[FundPK])
        Select @PostedBy,@FundClientPK, @TransactionPK,@ClientSwitchingPK,@UnitPrevious,@SubsUnit,@UnitPrevious + @SubsUnit
        ,Case when @TrxFrom = 'rdo' then 0 else 1 end,5,'Posting Switching In',@FundPKTo



		Declare @SwitchingUnit numeric(22,8)


        Declare @OldSwitchingUnit numeric(22,8)
        Declare @OldUnitAmount numeric(22,8)
        Declare @OldNAVDate datetime
        Declare @OldFundPK int
        Declare @OldFundClientPK int
        Declare @OldCashAmount numeric(24,4)
        Declare @OldNAV numeric(18,8)
                                   
        Select @OldFundPK = FundPKFrom,@OldFundClientPK = FundClientPK, @OldNAVDate = NAVDate,@OldNAV = NAVFundFrom, @TrxFrom = EntryUsersID,@TransactionPK  = TransactionPK
        From ClientSwitching where ClientSwitchingPK = @ClientSwitchingPK and HistoryPK = @HistoryPK

set @OldUnitAmount =0

        Select @OldUnitAmount = UnitChanges * -1 from FundClientPositionLog where FundClientPK = @OldFundClientPK and FundPK = @oldFundPK
        and ClientTransactionPK = @ClientSwitchingPK and TransactionType = 6 and ID =
        (
            Select max(ID) From FundClientPositionLog where FundClientPK = @OldFundClientPK and FundPK = @oldFundPK
                                                and ClientTransactionPK = @ClientSwitchingPK and TransactionType = 6
        )

        Set @OldCashAmount = 0

set @LastNAV =1
		Select @LastNAV  = NAV
		from CloseNAV where Date = 
		(
		Select Max(date) From CloseNAV Where FundPK = @OldFundPK and status = 2 and Date < @OldNAVDate
		) and status = 2 and FundPK = @OldFundPK

		set @LastNAV = isnull(@LastNAV,1)

		set @OldSwitchingUnit = 0

		IF @OldCashAmount > 0 and @oldNAV = 0
		BEGIN
			set @OldSwitchingUnit = @OldCashAmount / @LastNAV
		END
		ELSE
		BEGIN
			set @OldSwitchingUnit = @OldUnitAmount
		END

set @UnitPrevious = 0

        Select @UnitPrevious = isnull(Unit,0) From FundClientPositionSummary
        where FundClientPK = @OldFundClientPK and FundPK = @OldFundPK

        update fundclientpositionsummary
		set Unit = Unit + isnull(@OldSwitchingUnit,0)
		where FundClientPK = @OldFundClientPK and FundPK = @OldFundPK
                               
        --Buy = 1,
        --Sell = 2,
        --Adjustment = 3,
        --SwitchingIn = 5,
        --SwitchingOut = 6

        insert into [FundClientPositionLog]
                    ([UserId]
                    ,[FundClientPK]
                    ,[TransactionPK]
                    ,[ClientTransactionPK]
                    ,[UnitPrevious]
                    ,[UnitChanges]
                    ,[UnitAfter]
                    ,[IsBoTransaction]
                    ,[TransactionType]
                    ,[Description]
                    ,[FundPK])
        Select @PostedBy,@OldFundClientPK,@TransactionPK ,@ClientSwitchingPK,@UnitPrevious,@OldSwitchingUnit,@UnitPrevious + @OldSwitchingUnit
        ,Case when @TrxFrom = 'rdo' then 0 else 1 end,6,'Posting Switching Out Old Data Revise',@OldFundPK

		set  @SwitchingUnit = 0         
		set @SwitchingUnit = @TotalUnitAmountFundFrom
		                          

        set @SwitchingUnit = @SwitchingUnit * -1

		set  @UnitPrevious = 0   
        Select @UnitPrevious = isnull(Unit,0) From FundClientPositionSummary
        where FundClientPK = @FundClientPK and FundPK = @FundPKFrom

        set @UnitPrevious = isnull(@UnitPrevious,0)

        update fundclientpositionsummary
		set Unit = Unit + isnull(@SwitchingUnit,0)
		where FundClientPK = @FundClientPK and FundPK = @FundPKFrom
                               
        --Buy = 1,
        --Sell = 2,
        --Adjustment = 3,
        --SwitchingIn = 5,
        --SwitchingOut = 6

        insert into [FundClientPositionLog]
                    ([UserId]
                    ,[FundClientPK]
                    ,[TransactionPK]
                    ,[ClientTransactionPK]
                    ,[UnitPrevious]
                    ,[UnitChanges]
                    ,[UnitAfter]
                    ,[IsBoTransaction]
                    ,[TransactionType]
                    ,[Description]
                    ,[FundPK])
        Select @PostedBy,@FundClientPK,@TransactionPK,@ClientSwitchingPK,@UnitPrevious,@SwitchingUnit,@UnitPrevious + @SwitchingUnit,
    case when @TrxFrom = 'rdo' then 0 else 1 end,6,'Posting Switching out',@FundPKFrom





Declare @counterDateFrom datetime    
Declare @counterDateTo datetime      
set @counterDateFrom = @ValueDate  
set @counterDateTo = @ValueDate    
                            
while @counterDateTo < (select dbo.fworkingday(max(valueDate),1)  from EndDayTrails)    
BEGIN    
	set @counterDateTo = dateadd(day,0,dbo.FWorkingDay(@counterDateTo,1))     
 
	if Exists(Select *from FundClientPosition where date = @counterDateTo and FundClientPK = @FundClientPK and FundPK = @FundPKTo)
	begin 
	update fundClientPosition set UnitAmount = UnitAmount  + @TotalUnitAmountFundTo,CashAmount = CashAmount + @TotalCashAmountFundTo    
	where FundClientPK = @FundClientPK and FundPK = @FundPKTo and Date = @counterDateTo 
	end 
	else 
	begin
	INSERT INTO  FundClientPosition (Date,FundClientPK,FundPK,CashAmount,UnitAmount) 
	select @counterDateTo,@FundClientPK,@FundPKTo,@TotalCashAmountFundTo,@TotalUnitAmountFundTo 
	END
END

while @counterDateFrom < (select dbo.fworkingday(max(valueDate),1)  from EndDayTrails)    
BEGIN    
set @counterDateFrom = dbo.FWorkingDay(@counterDateFrom,1)     
update fundClientPosition set UnitAmount = UnitAmount  - @UnitAmount,CashAmount = CashAmount - @TotalCashAmountFundFrom    
where FundClientPK = @FundClientPK and FundPK = @FundPKFrom and Date = @counterDateFrom 
END



update ClientSwitching 
set PostedBy = @PostedBy,PostedTime = @PostedTime,Posted = 1,Lastupdate = @PostedTime 
where ValueDate = @ValueDate and FundClientPK = @FundClientPK 
and FundPKFrom = @FundPKFrom and FundPKTo = @FundPKTo and Status = 2 and Posted  = 0 and Revised = 0 and ClientSwitchingPK = @ClientSwitchingPK

Fetch next From A 
Into @TotalCashAmountFundFrom,@TotalCashAmountFundTo, @TotalUnitAmountFundFrom,@TotalUnitAmountFundTo,@FundPKFrom, 
@FundPKTo,@FundClientPK,@ValueDate,@ClientSwitchingPK,@TrxFrom, @TransactionPK,@HistoryPK,@FeeType
,@SwitchingFeeAmount,@PaymentDate,@NAVDate
,@FundClientID,@FundClientName,@CashAmountFundFrom,@UnitAmount,@CType
END
Close A
Deallocate A 
                            ";
                        cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _dateTo);
                        cmd.Parameters.AddWithValue("@PostedBy", _usersID);
                        cmd.Parameters.AddWithValue("@PostedTime", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public void ClientSwitching_Revise(string _usersID, ClientSwitching _clientSwitching)
        {

            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText =
                        @"
                            declare @TotalUnitAmountFundFrom numeric(22,4)
                            declare @TotalUnitAmountFundTo numeric(22,4)
                            declare @ValueDate datetime
                            declare @FundPKFrom int
                            declare @FundPKTo int
                            declare @MaxClientSwitchingPK int
                            declare @FundClientPK int

                            select @TotalUnitAmountFundFrom=unitamount,@TotalUnitAmountFundTo=TotalUnitAmountFundTo,
                            @ValueDate = NAVDate, @FundPKFrom = FundPKFrom, @FundPKTo = FundPKTo, @FundClientPK = FundClientPK
                            from ClientSwitching where  Status = 2 and clientSwitchingPK = @ClientSwitchingPK and Posted  = 1 and Revised  = 0 

                            if Exists(Select *from FundClientPosition where date = @ValueDate and FundClientPK = @FundClientPK and FundPK = @FundPKTo)
                            begin 
                            Update FundClientPosition set  
                            UnitAmount = UnitAmount - @TotalUnitAmountFundTo where Date = @ValueDate and FundClientPK = @FundClientPK 
                            and FundPK = @FundPKTo 
                            end 

                            if Exists(Select *from FundClientPosition where date = @ValueDate and FundClientPK = @FundClientPK and FundPK = @FundPKFrom)
                            begin 
                            Update FundClientPosition set  
                            UnitAmount = UnitAmount + @TotalUnitAmountFundFrom where Date = @ValueDate and FundClientPK = @FundClientPK 
                            and FundPK = @FundPKFrom 
                            end 
                            


                            Select @MaxClientSwitchingPK = ISNULL(MAX(ClientSwitchingPK),0) + 1 From ClientSwitching   
                            INSERT INTO [dbo].[ClientSwitching]  
                            ([ClientSwitchingPK],[HistoryPK] ,[Status],[Notes], [NAVDate] ,[ValueDate],
                            [PaymentDate], [NAVFundFrom] ,[NAVFundTo] ,[FundPKFrom],[FundPKTo], [FundClientPK] , [CashRefPKFrom] ,[CashRefPKTo] ,[BitSwitchingAll], [Description] ,
                            [CashAmount] ,[UnitAmount] ,[TotalCashAmountFundFrom] ,[TotalCashAmountFundTo] ,
                            [TotalUnitAmountFundFrom] ,[TotalUnitAmountFundTo] ,[SwitchingFeePercent] ,[SwitchingFeeAmount],[CurrencyPK],
                            [TransferType],[FeeType],[FeeTypeMethod],
                            [EntryUsersID],[EntryTime],[LastUpdate],IsProcessed,[IsFrontSync],userswitchingPK,TransactionPK)
                        
                            SELECT @MaxClientSwitchingPK,1,1,'Pending Revised' ,[NAVDate] ,
                            [ValueDate],[PaymentDate],[NAVFundFrom],[NAVFundTo] ,[FundPKFrom],[FundPKTo],[FundClientPK] ,
                            [CashRefPKFrom] ,[CashRefPKTo] ,[BitSwitchingAll],[Description] ,[CashAmount] ,[UnitAmount] ,[TotalCashAmountFundFrom] ,[TotalCashAmountFundTo] ,[TotalUnitAmountFundFrom] ,[TotalUnitAmountFundTo] ,
                            [SwitchingFeePercent] ,[SwitchingFeeAmount] ,[CurrencyPK],
                            [TransferType],[FeeType],[FeeTypeMethod],
                            [EntryUsersID],[EntryTime] ,@RevisedTime,0,0,userswitchingPK,TransactionPK 
                            FROM ClientSwitching  
                            where ClientSwitchingPK = @ClientSwitchingPK

                            update ClientSwitching 
                            set RevisedBy = @RevisedBy,RevisedTime = @RevisedTime,Revised = 1,Lastupdate = @RevisedTime, status = 3 ,IsFrontSync = 0
                            where  clientSwitchingPK = @ClientSwitchingPK and Status = 2 and Revised = 0 and Posted  = 1

                            Declare @counterDateFrom datetime    
                            Declare @counterDateTo datetime      
                            set @counterDateFrom = @ValueDate  
                            set @counterDateTo = @ValueDate  

                            while @counterDateTo < 
                            (select max(date) from fundClientPosition where FundClientPK = @FundClientPK)    
                            BEGIN 
                                set @counterDateTo = dbo.FWorkingDay(@counterDateTo,1)    
		                        update fundClientPosition set UnitAmount = UnitAmount  - @TotalUnitAmountFundTo    
		                        where FundClientPK = @FundClientPK and FundPK = @FundPKTo and Date = @counterDateTo 
	                            
                            END

                            while @counterDateFrom < (select max(date) from fundClientPosition where FundClientPK = @FundClientPK)    
                            BEGIN    
	                            set @counterDateFrom = dbo.FWorkingDay(@counterDateFrom,1)    
	                            update fundClientPosition set UnitAmount = UnitAmount  + @TotalUnitAmountFundFrom 
	                            where FundClientPK = @FundClientPK and FundPK = @FundPKFrom and Date = @counterDateFrom 
                            END

                                    Declare @LastNAV numeric(22,8)
		                           Declare @SubsUnit numeric(22,8)


                                    Declare @OldSubsUnit numeric(22,8)
                                    Declare @OldUnitAmount numeric(22,8)
                                    Declare @OldNAVDate datetime
                                    Declare @OldFundPK int
                                    Declare @OldFundClientPK int
                                    Declare @OldCashAmount numeric(24,4)
                                    Declare @OldNAV numeric(18,8)
                                    Declare @TrxFrom nvarchar(200)
                                    declare @TransactionPK  nvarchar(200)
                                    Select @OldFundPK = FundPKFrom,@OldFundClientPK = FundClientPK, @OldNAVDate = NAVDate,@oldNAV = NAVFundFrom,@TrxFrom = EntryUsersID,@TransactionPK  = TransactionPK
                                    From ClientSwitching where ClientSwitchingPK = @PK and HistoryPK = @HistoryPK

                                    Select @OldUnitAmount = UnitChanges * -1 from FundClientPositionLog where FundClientPK = @OldFundClientPK and FundPK = @oldFundPK
                                     and ClientTransactionPK = @PK and TransactionType = 6 and ID =
                                    (
                                        Select max(ID) From FundClientPositionLog where FundClientPK = @OldFundClientPK and FundPK = @oldFundPK
                                                                         and ClientTransactionPK = @PK and TransactionType = 6
                                    )

				                   set @OldSubsUnit = @OldUnitAmount
		                      

                                    Declare @UnitPrevious numeric(22,8)
                                    Select @UnitPrevious = isnull(Unit,0) From FundClientPositionSummary
                                    where FundClientPK = @OldFundClientPK and FundPK = @OldFundPK

                                   --update fundclientpositionsummary
		                           --set Unit = Unit + @OldSubsUnit
		                           --where FundClientPK = @OldFundClientPK and FundPK = @OldFundPK
                               
                                    --Buy = 1,
                                    --Sell = 2,
                                    --Adjustment = 3,
                                    --SwitchingIn = 5,
                                    --SwitchingOut = 6

                                    insert into [FundClientPositionLog]
                                               ([UserId]
                                               ,[FundClientPK]
                                               ,[TransactionPK]
                                               ,[ClientTransactionPK]
                                               ,[UnitPrevious]
                                               ,[UnitChanges]
                                               ,[UnitAfter]
                                               ,[IsBoTransaction]
                                               ,[TransactionType]
                                               ,[Description]
                                                ,[FundPK])
                                    Select @RevisedBy,@OldFundClientPK,@TransactionPK,@PK,@UnitPrevious,@OldSubsUnit,@UnitPrevious + @OldSubsUnit,
                                    Case when @TrxFrom = 'rdo' then 0 else 1 end,6,'Revise Switching Out Old Data Revise',@OldFundPK

                                    insert into [FundClientPositionLog]
                                               ([UserId]
                                               ,[FundClientPK]
                                               ,[TransactionPK]
                                               ,[ClientTransactionPK]
                                               ,[UnitPrevious]
                                               ,[UnitChanges]
                                               ,[UnitAfter]
                                               ,[IsBoTransaction]
                                               ,[TransactionType]
                                               ,[Description]
                                                ,[FundPK])
                                    Select @RevisedBy,@OldFundClientPK,@TransactionPK,@MaxClientSwitchingPK,@UnitPrevious + @OldSubsUnit,@OldSubsUnit * -1,@UnitPrevious + @OldSubsUnit + (@OldSubsUnit *-1)
                                    ,Case when @TrxFrom = 'rdo' then 0 else 1 end,6,'Revise Switching Out',@OldFundPK


                                    Select @OldFundPK = FundPKTo,@OldFundClientPK = FundClientPK, @OldNAVDate = NAVDate,@oldNAV = NAVFundTo,@TrxFrom = EntryUsersID,@TransactionPK  = TransactionPK
                                    From ClientSwitching where ClientSwitchingPK = @PK and HistoryPK = @HistoryPK

                                    Select @OldUnitAmount = UnitChanges * -1 from FundClientPositionLog where FundClientPK = @OldFundClientPK and FundPK = @oldFundPK
                                    and ClientTransactionPK = @PK and TransactionType = 5 and ID =
                                    (
                                        Select max(ID) From FundClientPositionLog where FundClientPK = @OldFundClientPK and FundPK = @oldFundPK
                                                                         and ClientTransactionPK = @PK and TransactionType = 5
                                    )


				                   set @OldSubsUnit = @OldUnitAmount
		                      

                                    Select @UnitPrevious = isnull(Unit,0) From FundClientPositionSummary
                                    where FundClientPK = @OldFundClientPK and FundPK = @OldFundPK

                                   update fundclientpositionsummary
		                           set Unit = Unit + @OldSubsUnit
		                           where FundClientPK = @OldFundClientPK and FundPK = @OldFundPK
                               
                                    --Buy = 1,
                                    --Sell = 2,
                                    --Adjustment = 3,
                                    --SwitchingIn = 5,
                                    --SwitchingOut = 6

                                    insert into [FundClientPositionLog]
                                               ([UserId]
                                               ,[FundClientPK]
                                               ,[TransactionPK]
                                               ,[ClientTransactionPK]
                                               ,[UnitPrevious]
                                               ,[UnitChanges]
                                               ,[UnitAfter]
                                               ,[IsBoTransaction]
                                               ,[TransactionType]
                                               ,[Description]
                                                ,[FundPK])
                                    Select @RevisedBy,@OldFundClientPK,@TransactionPK ,@PK,@UnitPrevious,@OldSubsUnit,@UnitPrevious + @OldSubsUnit
                                    ,Case when @TrxFrom = 'rdo' then 0 else 1 end,5,'Revise Switching IN Old Data Revise',@OldFundPK

                                    insert into [FundClientPositionLog]
                                               ([UserId]
                                               ,[FundClientPK]
                                               ,[TransactionPK]
                                               ,[ClientTransactionPK]
                                               ,[UnitPrevious]
                                               ,[UnitChanges]
                                               ,[UnitAfter]
                                               ,[IsBoTransaction]
                                               ,[TransactionType]
                                               ,[Description]
                                                ,[FundPK])
                                    Select @RevisedBy,@OldFundClientPK,@TransactionPK ,@MaxClientSwitchingPK,@UnitPrevious + @OldSubsUnit,@OldSubsUnit * -1,@UnitPrevious + @OldSubsUnit + (@OldSubsUnit *-1)
                                    ,Case when @TrxFrom = 'rdo' then 0 else 1 end,5,'Revise Switching IN',@OldFundPK

                        ";

                        cmd.Parameters.AddWithValue("@RevisedBy", _usersID);
                        cmd.Parameters.AddWithValue("@RevisedTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@ClientSwitchingPK", _clientSwitching.ClientSwitchingPK);
                        cmd.Parameters.AddWithValue("@PK", _clientSwitching.ClientSwitchingPK);
                        cmd.Parameters.AddWithValue("@HistoryPK", _clientSwitching.HistoryPK);
                        cmd.ExecuteNonQuery();
                    }
                }
            }



            catch (Exception err)
            {
                throw err;
            }

        }

        public bool Validate_CheckMinimumBalanceSwitching(decimal _cashAmount, decimal _unitAmount, DateTime _valueDate, int _fundPK, int _fundClientPK)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_unitAmount == 0)
                        {
                            cmd.CommandText = @"
                            create table #UnitAmountTemps
                            (
                            UnitAmount decimal(18,4),
                            fundpk int,
							fundclientpk int
                            )
                            insert into #UnitAmountTemps
                            select Unit - (select @CashAmount/isnull([dbo].[FgetLastCloseNav] (@ValueDate - 1,@FundPK),0) Unit) UnitAmount,FundPK,FundClientPK from FundClientPositionSummary where FundPK = @FundPK and FundclientPK = @FundClientPK

							select b.UnitAmount,a.RemainingBalanceUnit,case when b.UnitAmount = 0 then 0 else case when RemainingBalanceUnit < b.UnitAmount then 0 else case when RemainingBalanceUnit > b.UnitAmount then 1 end end end hasil from Fund a left join #UnitAmountTemps b on a.FundPK = b.fundpk where b.FundPK = @FundPK
                            ";

                        }
                        else
                        {
                            cmd.CommandText = @"
                            create table #UnitAmountTemps
                            (
                            UnitAmount decimal(18,4),
                            fundpk int,
							fundclientpk int
                            )
                            insert into #UnitAmountTemps
                            select Unit - @UnitAmount  UnitAmount,FundPK,FundClientPK from FundClientPositionSummary where FundPK = @FundPK and FundclientPK = @FundClientPK

							
						    select b.UnitAmount,a.RemainingBalanceUnit,case when b.UnitAmount = 0 then 0 else case when RemainingBalanceUnit < b.UnitAmount then 0 else case when RemainingBalanceUnit > b.UnitAmount then 1 end end end hasil from Fund a left join #UnitAmountTemps b on a.FundPK = b.fundpk where b.FundPK = @FundPK
							";

                        }

                        cmd.Parameters.AddWithValue("@CashAmount", _cashAmount);
                        cmd.Parameters.AddWithValue("@UnitAmount", _unitAmount);
                        cmd.Parameters.AddWithValue("@ValueDate", _valueDate);
                        cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                        cmd.Parameters.AddWithValue("@FundClientPK", _fundClientPK);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return Convert.ToBoolean(dr["hasil"]);
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