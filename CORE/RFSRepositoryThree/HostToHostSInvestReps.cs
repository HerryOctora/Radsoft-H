using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Deserializers;
using System.Security.Cryptography;
using RFSRepository;
using System.Data.SqlClient;


namespace RFSRepositoryThree
{
    public class HostToHostSInvestReps
    {
        Host _host = new Host();
        public class MessageGetMaster
        {
            public int code { get; set; }
            public string message { get; set; }
            public List<MasterStructure> data { get; set; }

        }

        public class MasterStructure
        {
            public string Code { get; set; }
            public string Desc { get; set; }
            public string LastUpdate { get; set; }
        }


        public string ComputeSha256Hash(string rawData)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public string GenerateH2HReference()
        {
            string _time = DateTime.Now.ToString("yyyyMMddhhmmssfff");
            _time = _time + "001" ;

            //_time = "H2H20170224015301678001ksei123";
          
            return _time;
        }

        //H2H20170224015301678001ksei123
        public string test()
        {

            string _h2hReference = GenerateH2HReference();
            string _hash;
            _hash = ComputeSha256Hash(Tools._sinvestH2HPrefix + _h2hReference + Tools._sinvestH2HPassword);

            var Client = new RestClient("http://158.140.177.25:17071");
            var Request = new RestRequest("Radsoft/RDO/GetMasterBank/RDOAPI",Method.GET);
            Request.RequestFormat = DataFormat.Json;
            var response = Client.Execute(Request);
            JsonDeserializer deserial = new JsonDeserializer();
            MessageGetMaster _m = new MessageGetMaster();
            _m = deserial.Deserialize<MessageGetMaster>(response); 
       

            return "OK";
        }


        public string KycUpload(int _fundclientPK, string _kycReqType, string _investorType)
        {
            using (SqlConnection DbCon = new SqlConnection(Tools.conString))
            {
                  DbCon.Open();
                  using (SqlCommand cmd = DbCon.CreateCommand())
                  {
                      if (_investorType == "1")
                      {
                          if (Tools.ClientCode == "10")
                          {
                              #region QueryMandiri 
                              cmd.CommandText = @"
                                IF EXISTS(
                                        select FundClientPK from HighRiskMonitoring where status = 1 and HighRiskType = 1 and FundClientPK = @FundClientPK
                                    )BEGIN return END 

                                    create table #tableBank
                                    (
	                                    BankID nvarchar(50) COLLATE DATABASE_DEFAULT,
	                                    status int,
	                                    BankName nvarchar(50) COLLATE DATABASE_DEFAULT,
	                                    AccountNo nvarchar(50) COLLATE DATABASE_DEFAULT,
	                                    AccountName nvarchar(50) COLLATE DATABASE_DEFAULT,
	                                    Currency nvarchar(50) COLLATE DATABASE_DEFAULT,
	                                    FundClientPK int,
                                    )

                                    insert into #tableBank
                                    select top 3 ROW_NUMBER() over (order by FundClientPK),status,BankName,AccountNo,AccountName,currency,fundclientpk from ZRDO_80_BANK where FundClientPK = @FundClientPK and status = 2
                                    order by BankID asc

                                    BEGIN  
                                    SET NOCOUNT ON         
                                    select @Type  KycReqType 
                                    , @CompanyID  SaCode   
                                    , '' Sid   
                                    , RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(NamaDepanInd,'')))) FirstName      
                                    , RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(NamaTengahInd,'')))) MiddleName      
                                    , RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(NamaBelakangInd,'')))) LastName    
                                    , RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(nationality,''))))  CountryOfNationality 
                                    , (isnull(NoIdentitasInd1,'')) IdNo 
                                    , RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull((case when IdentitasInd1 = 7 then '99981231' else case when CONVERT(VARCHAR(10), ExpiredDateIdentitasInd1, 112) = '19000101' or CONVERT(VARCHAR(10), ExpiredDateIdentitasInd1, 112) < '20160802' then '' else CONVERT(VARCHAR(10), ExpiredDateIdentitasInd1, 112) end End),''))))    IdExpirationDate       
                                    , RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(NPWP,''))))   NpwpNo 
                                    , RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull((case when CONVERT(VARCHAR(10), RegistrationNPWP, 112) <> '19000101' then CONVERT(VARCHAR(10), RegistrationNPWP, 112) else '' End),'')))) NpwpRegiDate          
                                    , RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(Countryofbirth,''))))  CountryOfBirth   
                                    , RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(TempatLahir,''))))  PlaceOfBirth  
                                    , RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull((case when CONVERT(VARCHAR(10), TanggalLahir, 112) <> '19000101' then CONVERT(VARCHAR(10), TanggalLahir, 112) else '' End),''))))  BirthDate  
                                    , case when JenisKelamin = '0' then '' else isnull(cast(JenisKelamin as nvarchar(1)),'') end Gender 
                                    , case when Pendidikan = '0' then '' else isnull(cast(Pendidikan as nvarchar(1)),'') end  EducationalBackground 
                                    , case when mothermaidenname = '0' then '' else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(mothermaidenname ,'')))) end   MotherMaidenName    
                                    , case when Agama = '0' then '' else isnull(cast(Agama as nvarchar(1)),'') end  Religion 
                                    , case when Pekerjaan = '0' then '' else isnull(cast(Pekerjaan as nvarchar(1)),'') end    Occupation 
                                    , case when PenghasilanInd = '0' then '' else isnull(cast(PenghasilanInd as nvarchar(1)),'') end   IndividualIncomeLevel 
                                    , case when StatusPerkawinan = '0' then '' else isnull(cast(StatusPerkawinan as nvarchar(1)),'') end   MaritalStatus 
                                    , case when SpouseName = '0' then '' else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(SpouseName ,'')))) end     SpousesName  
                                    , case when InvestorsRiskProfile = '0' then '' else isnull(cast(InvestorsRiskProfile as nvarchar(1)),'') end  InvestorRiskProfile 
                                    , case when MaksudTujuanInd = '0' then '' else isnull(cast(MaksudTujuanInd as nvarchar(1)),'') end InvestmentObjective   
                                    , case when SumberDanaInd = '0' then '' else isnull(cast(SumberDanaInd as nvarchar(2)),'') end  IndividualSourceOfFund  
                                    , case when AssetOwner = '0' then '' else isnull(cast(AssetOwner as nvarchar(1)),'') end  AssetOwner 
                                    , case when OtherAlamatInd1 = '0' then '' else REPLACE(REPLACE(RTRIM(LTRIM(isnull(OtherAlamatInd1,''))),char(13),''),char(10),'') end KtpAddr 
                                    , case when OtherKodeKotaInd1 = '0' then '' else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(cast(OtherKodeKotaInd1 as nvarchar(4)),'')))) end  KtpCityCode    
                                    , case when OtherKodePosInd1 = '0' then '' else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(OtherKodePosInd1 ,'')))) end KtpPostalCode      
                                    , case when AlamatInd1 = '0' then '' else   REPLACE(REPLACE(RTRIM(LTRIM(isnull(AlamatInd1,''))),char(13),''),char(10),'') end  CorrespondenceAddr     
                                    , case when KodeKotaInd1 = '0' then '' else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(cast(KodeKotaInd1 as nvarchar(4)),'')))) end  CorrespondenceCityCode 
                                    , isnull(MV13.DescOne,'') CorrespondenceCityName                                    
                                    , case when KodePosInd1 = '0' then '' else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(KodePosInd1 ,'')))) end  CorrespondencePostalCode   
                                    , isnull(CountryofCorrespondence,'')  CorrespondenceCountry 
                                    , case when AlamatInd2 = '0' then '' else REPLACE(REPLACE(RTRIM(LTRIM(isnull(AlamatInd2,''))),char(13),''),char(10),'') end  DomicileAddr  
                                    , case when KodeKotaInd2 = '0' then '' else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(cast(KodeKotaInd2 as nvarchar(4)),'')))) end  DomicileCityCode 
                                    , isnull(MV14.DescOne,'')   DomicileCityName                                   
                                    , case when KodePosInd2 = '0' then '' else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(KodePosInd2 ,'')))) end   DomicilePostalCode 
                                    , isnull(CountryofDomicile,'') DomicileCountry 
                                    , case when TeleponRumah = '0' then '' else isnull(TeleponRumah ,'') end HomePhone    
                                    , case when TeleponSelular = '0' then '' else isnull(TeleponSelular ,'') end    Mobile 
                                    , case when fc.Fax = '0' then '' else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(fc.Fax ,'')))) end  Facsimile    
                                    , case when fc.Email = '0' then '' else isnull(fc.Email,'') end   EmailAddr   
                                    , case when StatementType = '0' then '' else isnull(cast(StatementType as nvarchar(1)),'') end  StatementType   
                                    , case when FATCA = '0' then '' else isnull(cast(FATCA as nvarchar(1)),'') end   IndividualFatcaStatus 
                                    , case when TIN = '0' then '' else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(TIN ,'')))) end  ForeignTin   
                                    , case when TINIssuanceCountry = '0' then '' else isnull(cast(TINIssuanceCountry as nvarchar(2)),'') end  ForeignTinIssuanceCountry                                  
                                    --1
                                    , case when FC.EntryUsersID = 'BKLP' then case when G1.Country = 'ID' then case when isnull(G1.SInvestID,'') <> '' and  isnull(G1.BICode,'') <> '' then '' else isnull(B1.SInvestID,'') end else '' end else case when B1.Country = 'ID' then case when isnull(B1.SInvestID,'') <> '' and  isnull(B1.BICode,'') <> '' then '' else isnull(B1.SInvestID,'') end else '' end end RedmBankBicCode1 
                                    , case when FC.EntryUsersID = 'BKLP' then case when G1.Country = 'ID' then isnull(G1.BICode,'') else '' end else case when B1.Country = 'ID' then isnull(B1.BICode,'') else '' end end  RedmBankBiMemberCode1                          
                                    , case when FC.EntryUsersID = 'BKLP' then isnull(G1.Name,'') else isnull(B1.Name,'') end RedmBankName1 
                                    , case when FC.EntryUsersID = 'BKLP' then isnull(G1.Country,'') else isnull(B1.Country,'') end RedmBankCountry1 
                                    , case when BankBranchName1 = '0' then '' else isnull(cast(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(BankBranchName1 ,'')))) as nvarchar(100)),'') end  RedmBankBranch1 
                                    , case when FC.EntryUsersID = 'BKLP' then isnull(D1.DescOne,'') else isnull(MV15.DescOne,'') end RedmBankAcCcy1 
                                    , case when FC.EntryUsersID = 'BKLP' then case when C1.AccountNo = '0' then '' else isnull(cast(C1.AccountNo as nvarchar(50)),'') end else case when NomorRekening1 = '0' then '' else isnull(cast(NomorRekening1 as nvarchar(50)),'') end end RedmBankAcNo1 
                                    , case when FC.EntryUsersID = 'BKLP' then case when C1.AccountName = '0' then '' else isnull(cast(C1.AccountName as nvarchar(100)),'') end else case when NamaNasabah1 = '0' then '' else isnull(cast(NamaNasabah1 as nvarchar(100)),'') end end RedmBankAcName1 
                                    --2
                                    , case when FC.EntryUsersID = 'BKLP' then case when G2.Country = 'ID' then isnull(G2.SInvestID,'') else '' end else case when B2.Country = 'ID' then isnull(B2.SInvestID,'') else '' end end RedmBankBicCode2 
                                    , case when FC.EntryUsersID = 'BKLP' then case when G2.Country = 'ID' then isnull(G2.BICode,'') else '' end else case when B2.Country = 'ID' then isnull(B2.BICode,'') else '' end end RedmBankBiMemberCode2 
                                    , case when FC.EntryUsersID = 'BKLP' then isnull(G2.Name,'') else isnull(B2.Name,'') end RedmBankName2 
                                    , case when FC.EntryUsersID = 'BKLP' then isnull(G2.Country,'') else isnull(B2.Country,'') end RedmBankCountry2 
                                    , case when BankBranchName2 = '0' then '' else isnull(cast(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(BankBranchName2 ,'')))) as nvarchar(100)),'') end  RedmBankBranch2 
                                    , case when FC.EntryUsersID = 'BKLP' then isnull(D2.DescOne,'') else isnull(MV16.DescOne,'') end RedmBankAcCcy2 
                                    , case when FC.EntryUsersID = 'BKLP' then case when C2.AccountNo = '0' then '' else isnull(cast(C2.AccountNo as nvarchar(50)),'') end else case when NomorRekening2 = '0' then '' else isnull(cast(NomorRekening2 as nvarchar(50)),'') end end RedmBankAcNo2 
                                    , case when FC.EntryUsersID = 'BKLP' then case when C2.AccountName = '0' then '' else isnull(cast(C2.AccountName as nvarchar(100)),'') end else case when NamaNasabah2 = '0' then '' else isnull(cast(NamaNasabah2 as nvarchar(100)),'') end end RedmBankAcName2 
                                    --3
                                    , case when FC.EntryUsersID = 'BKLP' then case when G3.Country = 'ID' then isnull(G3.SInvestID,'') else '' end else case when B3.Country = 'ID' then isnull(B3.SInvestID,'') else '' end end RedmBankBicCode3 
                                    , case when FC.EntryUsersID = 'BKLP' then case when G3.Country = 'ID' then isnull(G3.BICode,'') else '' end else case when B3.Country = 'ID' then isnull(B3.BICode,'') else '' end end RedmBankBiMemberCode3 
                                    , case when FC.EntryUsersID = 'BKLP' then isnull(G3.Name,'') else isnull(B3.Name,'') end RedmBankName3  
                                    , case when FC.EntryUsersID = 'BKLP' then isnull(G3.Country,'') else isnull(B3.Country,'') end RedmBankCountry3 
                                    , case when BankBranchName3 = '0' then '' else isnull(cast(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(BankBranchName3 ,'')))) as nvarchar(100)),'') end  RedmBankBranch3 
                                    , case when FC.EntryUsersID = 'BKLP' then isnull(D3.DescOne,'') else isnull(MV17.DescOne,'') end RedmBankAcCcy3 
                                    , case when FC.EntryUsersID = 'BKLP' then case when C3.AccountNo = '0' then '' else isnull(cast(C3.AccountNo as nvarchar(50)),'') end else case when NomorRekening3 = '0' then '' else isnull(cast(NomorRekening3 as nvarchar(50)),'') end end RedmBankAcNo3 
                                    , case when FC.EntryUsersID = 'BKLP' then case when C3.AccountName = '0' then '' else isnull(cast(C3.AccountName as nvarchar(100)),'') end else case when NamaNasabah3 = '0' then '' else isnull(cast(NamaNasabah3 as nvarchar(100)),'') end end RedmBankAcName3                                     
                                    , isnull(FC.ID,'') ClientCode                                       
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
                                    left join #tableBank C1 on FC.FundClientPK = C1.FundClientPK and C1.BankID = 1
                                    left join ZRDO_80_BANK_MAPPING F1 on C1.BankName = F1.PartnerCode and C1.BankID = 1
                                    left join Bank G1 on F1.RadsoftCode = G1.ID and G1.status = 2 
                                    left join MasterValue E1 on G1.Country = E1.Code and E1.Id = 'SDICountry' and E1.Status = 2
                                    left join MasterValue D1 on FC.MataUang1 = D1.Code and D1.Id = 'MataUang' and D1.Status = 2
                                    left join #tableBank C2 on FC.FundClientPK = C2.FundClientPK and C2.BankID = 2
                                    left join ZRDO_80_BANK_MAPPING F2 on C2.BankName = F2.PartnerCode and C2.BankID = 2
                                    left join Bank G2 on F2.RadsoftCode = G2.ID and G2.status = 2 
                                    left join MasterValue E2 on G2.Country = E2.Code and E2.Id = 'SDICountry' and E2.Status = 2
                                    left join MasterValue D2 on FC.MataUang1 = D2.Code and D2.Id = 'MataUang' and D2.Status = 2
                                    left join #tableBank C3 on FC.FundClientPK = C3.FundClientPK and C3.BankID = 3
                                    left join ZRDO_80_BANK_MAPPING F3 on C3.BankName = F3.PartnerCode and C3.BankID = 3
                                    left join Bank G3 on F3.RadsoftCode = G3.ID and G3.status = 2 
                                    left join MasterValue E3 on G3.Country = E3.Code and E3.Id = 'SDICountry' and E3.Status = 2
                                    left join MasterValue D3 on FC.MataUang1 = D3.Code and D3.Id = 'MataUang' and D3.Status = 2
                                    where FC.Status = 2 and FC.InvestorType = 1 
                                    and FC.FundClientPK = @FundClientPK
                                    order by FC.name asc  END  ";
                              #endregion    
                           
                          }

                              cmd.Parameters.AddWithValue("@CompanyID", _host.Get_CompanyID());
                              cmd.Parameters.AddWithValue("@Type", _kycReqType);
                              cmd.Parameters.AddWithValue("@FundClientPK", _fundclientPK);
                              using (SqlDataReader dr1 = cmd.ExecuteReader())
                              {
                                  if (dr1.HasRows)
                                  {
                                      dr1.Read();
                                      string _h2hReference = GenerateH2HReference();
                                      string _hash;
                                      _hash = ComputeSha256Hash(Tools._sinvestH2HPrefix + _h2hReference + Tools._sinvestH2HPassword);
                                      var Client = new RestClient(Tools._sinvestH2HUrl);
                                      var Request = new RestRequest("ksei/static/kyc/ind/upload", Method.POST);
                                      Request.AddHeader("Content-Type", "application/json");
                                      Request.AddHeader("username ", Tools._sinvestH2HUserName);
                                      Request.AddHeader("h2hreference ", _h2hReference);
                                      Request.AddHeader("Token ", _hash);
                                      Request.RequestFormat = DataFormat.Json;
                                      Request.AddBody(
                                          new
                                          {
                                              KycReqType = dr1["KycReqType"].ToString(),
                                              SaCode = dr1["SaCode"].ToString(),
                                              Sid = dr1["Sid"].ToString(),
                                              FirstName = dr1["FirstName"].ToString(),
                                              MiddleName = dr1["MiddleName"].ToString(),
                                              LastName = dr1["LastName"].ToString(),
                                              CountryOfNationality = dr1["CountryOfNationality"].ToString(),
                                              IdNo = dr1["IdNo"].ToString(),
                                              IdExpirationDate = dr1["IdExpirationDate"].ToString(),
                                              NpwpNo = dr1["NpwpNo"].ToString(),
                                              NpwpRegiDate = dr1["NpwpRegiDate"].ToString(),
                                              CountryOfBirth = dr1["CountryOfBirth"].ToString(),
                                              PlaceOfBirth = dr1["PlaceOfBirth"].ToString(),
                                              BirthDate = dr1["BirthDate"].ToString(),
                                              Gender = dr1["Gender"].ToString(),
                                              EducationalBackground = dr1["EducationalBackground"].ToString(),
                                              MotherMaidenName = dr1["MotherMaidenName"].ToString(),
                                              Religion = dr1["Religion"].ToString(),
                                              Occupation = dr1["Occupation"].ToString(),
                                              IndividualIncomeLevel = dr1["IndividualIncomeLevel"].ToString(),
                                              MaritalStatus = dr1["MaritalStatus"].ToString(),
                                              SpousesName = dr1["SpousesName"].ToString(),
                                              InvestorRiskProfile = dr1["InvestorRiskProfile"].ToString(),
                                              InvestmentObjective = dr1["InvestmentObjective"].ToString(),
                                              IndividualSourceOfFund = dr1["IndividualSourceOfFund"].ToString(),
                                              AssetOwner = dr1["AssetOwner"].ToString(),
                                              KtpAddr = dr1["KtpAddr"].ToString(),
                                              KtpCityCode = dr1["KtpCityCode"].ToString(),
                                              KtpPostalCode = dr1["KtpPostalCode"].ToString(),
                                              CorrespondenceAddr = dr1["CorrespondenceAddr"].ToString(),
                                              CorrespondenceCityCode = dr1["CorrespondenceCityCode"].ToString(),
                                              CorrespondenceCityName = dr1["CorrespondenceCityName"].ToString(),
                                              CorrespondencePostalCode = dr1["CorrespondencePostalCode"].ToString(),
                                              CorrespondenceCountry = dr1["CorrespondenceCountry"].ToString(),
                                              DomicileAddr = dr1["DomicileAddr"].ToString(),
                                              DomicileCityCode = dr1["DomicileCityCode"].ToString(),
                                              DomicileCityName = dr1["DomicileCityName"].ToString(),
                                              DomicilePostalCode = dr1["DomicilePostalCode"].ToString(),
                                              DomicileCountry = dr1["DomicileCountry"].ToString(),
                                              HomePhone = dr1["HomePhone"].ToString(),
                                              Mobile = dr1["Mobile"].ToString(),
                                              Facsimile = dr1["Facsimile"].ToString(),
                                              EmailAddr = dr1["EmailAddr"].ToString(),
                                              StatementType = dr1["StatementType"].ToString(),
                                              IndividualFatcaStatus = dr1["IndividualFatcaStatus"].ToString(),
                                              ForeignTin = dr1["ForeignTin"].ToString(),
                                              ForeignTinIssuanceCountry = dr1["ForeignTinIssuanceCountry"].ToString(),
                                              RedmBankBicCode1 = dr1["RedmBankBicCode1"].ToString(),
                                              RedmBankBiMemberCode1 = dr1["RedmBankBiMemberCode1"].ToString(),
                                              RedmBankName1 = dr1["RedmBankName1"].ToString(),
                                              RedmBankCountry1 = dr1["RedmBankCountry1"].ToString(),
                                              RedmBankBranch1 = dr1["RedmBankBranch1"].ToString(),
                                              RedmBankAcCcy1 = dr1["RedmBankAcCcy1"].ToString(),
                                              RedmBankAcNo1 = dr1["RedmBankAcNo1"].ToString(),
                                              RedmBankAcName1 = dr1["RedmBankAcName1"].ToString(),
                                              RedmBankBicCode2 = dr1["RedmBankBicCode2"].ToString(),
                                              RedmBankBiMemberCode2 = dr1["RedmBankBiMemberCode2"].ToString(),
                                              RedmBankName2 = dr1["RedmBankName2"].ToString(),
                                              RedmBankCountry2 = dr1["RedmBankCountry2"].ToString(),
                                              RedmBankBranch2 = dr1["RedmBankBranch2"].ToString(),
                                              RedmBankAcCcy2 = dr1["RedmBankAcCcy2"].ToString(),
                                              RedmBankAcNo2 = dr1["RedmBankAcNo2"].ToString(),
                                              RedmBankAcName2 = dr1["RedmBankAcName2"].ToString(),
                                              RedmBankBicCode3 = dr1["RedmBankBicCode3"].ToString(),
                                              RedmBankBiMemberCode3 = dr1["RedmBankBiMemberCode3"].ToString(),
                                              RedmBankName3 = dr1["RedmBankName3"].ToString(),
                                              RedmBankCountry3 = dr1["RedmBankCountry3"].ToString(),
                                              RedmBankBranch3 = dr1["RedmBankBranch3"].ToString(),
                                              RedmBankAcCcy3 = dr1["RedmBankAcCcy3"].ToString(),
                                              RedmBankAcNo3 = dr1["RedmBankAcNo3"].ToString(),
                                              RedmBankAcName3 = dr1["RedmBankAcName3"].ToString(),
                                              ClientCode = dr1["ClientCode"].ToString()
                                          });
                                      var response = Client.Execute(Request);
                                      return response.Content;
                                  }
                                  else
                                  {
                                      return "CHECK HIGH RISK MONITORING FOR THIS CLIENT";
                                  }
                              }
                      }
                  }
            }
            return "OK";
        }
    }
}
