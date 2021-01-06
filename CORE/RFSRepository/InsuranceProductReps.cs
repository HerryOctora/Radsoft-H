using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RFSModel;
using RFSUtility;
using System.Data.SqlClient;



namespace RFSRepository
{
    public class InsuranceProductReps
    {
        Host _host = new Host();

        //1
        string _insertCommand = @"INSERT INTO [dbo].[FundClient]  
        ([FundClientPK],[HistoryPK],[Status],
[ID],
[Name],
[NomorRekening1],
[NomorRekening2],
[NomorRekening3],
[MataUang1],
[MataUang2],
[MataUang3],
[NamaBank1],
[NamaBank2],
[NamaBank3],
[NamaNasabah1],
[NamaNasabah2],
[NamaNasabah3],
[BankBranchName1],
[BankBranchName2],
[BankBranchName3],
[RemarkBank1],
[RemarkBank2],
[RemarkBank3],
[Agama],
[AgamaOfficer1],
[AgamaOfficer2],
[AgamaOfficer3],
[AgamaOfficer4],
[AhliWaris],
[AlamatInd1],
[AlamatInd2],
[AlamatKantorInd],
[AlamatOfficer1],
[AlamatOfficer2],
[AlamatOfficer3],
[AlamatOfficer4],
[AlamatPerusahaan],
[ARIA],
[AssetFor1Year],
[AssetFor2Year],
[AssetFor3Year],
[AssetOwner],
[BankRDNPK],
[BeneficialAddress],
[BeneficialHomeNo],
[BeneficialIdentity],
[BeneficialName],
[BeneficialNPWP],
[BeneficialPhoneNumber],
[BeneficialRelation],
[BeneficialWork],
[BitDefaultPayment1],
[BitDefaultPayment2],
[BitDefaultPayment3],
[BitisTA],
[BitShareAbleToGroup],
[CantRedempt],
[CantSubs],
[CantSwitch],
[CapitalPaidIn],
[ClientCategory],
[ClientOnBoard],
[CompanyCityName],
[CompanyFax],
[CompanyMail],
[CompanyTypeOJK],
[CorrespondenceRT],
[CorrespondenceRW],
[CountryOfBirth],
[CountryofCompany],
[CountryofCorrespondence],
[CountryofDomicile],
[CountryofEstablishment],
[Description],
[DOBOfficer1],
[DOBOfficer2],
[DOBOfficer3],
[DOBOfficer4],
[DomicileRT],
[DomicileRW],
[Domisili],
[DormantDate],
[Email],
[EmailIns1],
[EmailIns2],
[EmployerLineOfBusiness],
[ExpiredDateIdentitasInd1],
[ExpiredDateIdentitasInd2],
[ExpiredDateIdentitasInd3],
[ExpiredDateIdentitasInd4],
[ExpiredDateIdentitasIns11],
[ExpiredDateIdentitasIns12],
[ExpiredDateIdentitasIns13],
[ExpiredDateIdentitasIns14],
[ExpiredDateIdentitasIns21],
[ExpiredDateIdentitasIns22],
[ExpiredDateIdentitasIns23],
[ExpiredDateIdentitasIns24],
[ExpiredDateIdentitasIns31],
[ExpiredDateIdentitasIns32],
[ExpiredDateIdentitasIns33],
[ExpiredDateIdentitasIns34],
[ExpiredDateIdentitasIns41],
[ExpiredDateIdentitasIns42],
[ExpiredDateIdentitasIns43],
[ExpiredDateIdentitasIns44],
[ExpiredDateSKD],
[FaceToFaceDate],
[FATCA],
[Fax],
[FrontID],
[GIIN],
[HubunganAhliWaris],
[IdentitasInd1],
[IdentitasInd2],
[IdentitasInd3],
[IdentitasInd4],
[IdentitasIns11],
[IdentitasIns12],
[IdentitasIns13],
[IdentitasIns14],
[IdentitasIns21],
[IdentitasIns22],
[IdentitasIns23],
[IdentitasIns24],
[IdentitasIns31],
[IdentitasIns32],
[IdentitasIns33],
[IdentitasIns34],
[IdentitasIns41],
[IdentitasIns42],
[IdentitasIns43],
[IdentitasIns44],
[Identity1RT],
[Identity1RW],
[IdentityTypeOfficer1],
[IdentityTypeOfficer2],
[IdentityTypeOfficer3],
[IdentityTypeOfficer4],
[IFUACode],
[InternalCategoryPK],
[InvestorsRiskProfile],
[InvestorType],
[IsFaceToFace],
[Jabatan1],
[Jabatan2],
[Jabatan3],
[Jabatan4],
[JabatanKantor],
[JenisKelamin],
[JumlahIdentitasIns3],
[JumlahIdentitasIns4],
[Karakteristik],
[KodeCountryofKantor],
[KodeDomisiliPropinsi],
[KodeKotaInd1],
[KodeKotaInd2],
[KodeKotaIns],
[KodeKotaKantorInd],
[KodePosInd1],
[KodePosInd2],
[KodePosIns],
[KodePosKantorInd],
[KodePropinsiKantorInd],
[KYCRiskProfile],
[Legality],
[LokasiBerdiri],
[MaksudTujuanInd],
[MaksudTujuanInstitusi],
[MigrationStatus],
[MotherMaidenName],
[NamaBelakangInd],
[NamaBelakangIns1],
[NamaBelakangIns2],
[NamaBelakangIns3],
[NamaBelakangIns4],
[NamaDepanInd],
[NamaDepanIns1],
[NamaDepanIns2],
[NamaDepanIns3],
[NamaDepanIns4],
[NamaKantor],
[NamaPerusahaan],
[NamaTengahInd],
[NamaTengahIns1],
[NamaTengahIns2],
[NamaTengahIns3],
[NamaTengahIns4],
[Nationality],
[NationalityOfficer1],
[NationalityOfficer2],
[NationalityOfficer3],
[NationalityOfficer4],
[NatureOfBusiness],
[NatureOfBusinessLainnya],
[Negara],
[NoIdentitasInd1],
[NoIdentitasInd2],
[NoIdentitasInd3],
[NoIdentitasInd4],
[NoIdentitasIns11],
[NoIdentitasIns12],
[NoIdentitasIns13],
[NoIdentitasIns14],
[NoIdentitasIns21],
[NoIdentitasIns22],
[NoIdentitasIns23],
[NoIdentitasIns24],
[NoIdentitasIns31],
[NoIdentitasIns32],
[NoIdentitasIns33],
[NoIdentitasIns34],
[NoIdentitasIns41],
[NoIdentitasIns42],
[NoIdentitasIns43],
[NoIdentitasIns44],
[NoIdentitasOfficer1],
[NoIdentitasOfficer2],
[NoIdentitasOfficer3],
[NoIdentitasOfficer4],
[NomorAnggaran],
[NomorSIUP],
[NoSKD],
[NPWP],
[NPWPPerson1],
[NPWPPerson2],
[OperatingProfitFor1Year],
[OperatingProfitFor2Year],
[OperatingProfitFor3Year],
[OtherAgama],
[OtherAlamatInd1],
[OtherAlamatInd2],
[OtherAlamatInd3],
[OtherCharacteristic],
[OtherCurrency],
[OtherEmail],
[OtherFax],
[OtherInvestmentObjectives],
[OtherInvestmentObjectivesIns],
[OtherKodeKotaInd1],
[OtherKodeKotaInd2],
[OtherKodeKotaInd3],
[OtherKodePosInd1],
[OtherKodePosInd2],
[OtherKodePosInd3],
[OtherNegaraInd1],
[OtherNegaraInd2],
[OtherNegaraInd3],
[OtherOccupation],
[OtherPendidikan],
[OtherPropinsiInd1],
[OtherPropinsiInd2],
[OtherPropinsiInd3],
[OtherSourceOfFunds],
[OtherSourceOfFundsIns],
[OtherSpouseOccupation],
[OtherTeleponRumah],
[OtherTeleponSelular],
[OtherTipe],
[Pekerjaan],
[Pendidikan],
[PenghasilanInd],
[PenghasilanInstitusi],
[PhoneIns1],
[PhoneIns2],
[PlaceOfBirthOfficer1],
[PlaceOfBirthOfficer2],
[PlaceOfBirthOfficer3],
[PlaceOfBirthOfficer4],
[Politis],
[PolitisFT],
[PolitisLainnya],
[PolitisName],
[PolitisRelation],
[Propinsi],
[RDNAccountName],
[RDNAccountNo],
[RDNBankBranchName],
[RDNCurrency],
[Referral],
[Registered],
[RegistrationDateIdentitasInd1],
[RegistrationDateIdentitasInd2],
[RegistrationDateIdentitasInd3],
[RegistrationDateIdentitasInd4],
[RegistrationDateIdentitasIns11],
[RegistrationDateIdentitasIns12],
[RegistrationDateIdentitasIns13],
[RegistrationDateIdentitasIns14],
[RegistrationDateIdentitasIns21],
[RegistrationDateIdentitasIns22],
[RegistrationDateIdentitasIns23],
[RegistrationDateIdentitasIns24],
[RegistrationDateIdentitasIns31],
[RegistrationDateIdentitasIns32],
[RegistrationDateIdentitasIns33],
[RegistrationDateIdentitasIns34],
[RegistrationDateIdentitasIns41],
[RegistrationDateIdentitasIns42],
[RegistrationDateIdentitasIns43],
[RegistrationDateIdentitasIns44],
[RegistrationNPWP],
[RenewingDate],
[RiskProfileScore],
[SACode],
[SegmentClass],
[SellingAgentPK],
[SID],
[SIUPExpirationDate],
[SpouseAnnualIncome],
[SpouseBirthPlace],
[SpouseDateOfBirth],
[SpouseIDNo],
[SpouseName],
[SpouseNationality],
[SpouseNatureOfBusiness],
[SpouseNatureOfBusinessOther],
[SpouseOccupation],
[StatementType],
[StatusPerkawinan],
[SubstantialOwnerAddress],
[SubstantialOwnerName],
[SubstantialOwnerTIN],
[SumberDanaInd],
[SumberDanaInstitusi],
[TanggalBerdiri],
[TanggalLahir],
[TeleponBisnis],
[TeleponKantor],
[TeleponRumah],
[TeleponSelular],
[TempatLahir],
[TIN],
[TINIssuanceCountry],
[Tipe],
        ";


        string _paramaterCommand = @"
@ID,
@Name,
@NomorRekening1,
@NomorRekening2,
@NomorRekening3,
@MataUang1,
@MataUang2,
@MataUang3,
@NamaBank1,
@NamaBank2,
@NamaBank3,
@NamaNasabah1,
@NamaNasabah2,
@NamaNasabah3,
@BankBranchName1,
@BankBranchName2,
@BankBranchName3,
@RemarkBank1,
@RemarkBank2,
@RemarkBank3,
'1',
'1',
'1',
'1',
'1',
'AhliWaris',
'AlamatInd1',
'AlamatInd2',
'AlamatKantorInd',
'AlamatOfficer1',
'AlamatOfficer2',
'AlamatOfficer3',
'AlamatOfficer4',
'AlamatPerusahaan',
'1',
'AssetFor1Year',
'AssetFor2Year',
'AssetFor3Year',
'1',
'1',
'BeneficialAddress',
'BeneficialHomeNo',
'BeneficialIdentity',
'BeneficialName',
'BeneficialNPWP',
'BeneficialPhoneNumber',
'BeneficialRelation',
'1',
'1',
'1',
'1',
'1',
'1',
'1',
'1',
'1',
'1',
'ClientCategory',
'1',
'CompanyCityName',
'CompanyFax',
'CompanyMail',
'1',
'CorrespondenceRT',
'CorrespondenceRW',
'CountryOfBirth',
'CountryofCompany',
'CountryofCorrespondence',
'CountryofDomicile',
'CountryofEstablishment',
'Description',
'01/01/2010',
'01/01/2010',
'01/01/2010',
'01/01/2010',
'DomicileRT',
'DomicileRW',
'1',
'01/01/2010',
'Email',
'EmailIns1',
'EmailIns2',
'1',
'01/01/2010',
'01/01/2010',
'01/01/2010',
'01/01/2010',
'01/01/2010',
'01/01/2010',
'01/01/2010',
'01/01/2010',
'01/01/2010',
'01/01/2010',
'01/01/2010',
'01/01/2010',
'01/01/2010',
'01/01/2010',
'01/01/2010',
'01/01/2010',
'01/01/2010',
'01/01/2010',
'01/01/2010',
'01/01/2010',
'01/01/2010',
'01/01/2010',
'1',
'Fax',
'FrontID',
'GIIN',
'HubunganAhliWaris',
'1',
'1',
'1',
'1',
'1',
'1',
'1',
'1',
'1',
'1',
'1',
'1',
'1',
'1',
'1',
'1',
'1',
'1',
'1',
'1',
'Identity1RT',
'Identity1RW',
'1',
'1',
'1',
'1',
'IFUACode',
'1',
'1',
'InvestorType',
'1',
'Jabatan1',
'Jabatan2',
'Jabatan3',
'Jabatan4',
'JabatanKantor',
'1',
'1',
'1',
'1',
'KodeCountryofKantor',
'1',
'KodeKotaInd1',
'KodeKotaInd2',
'KodeKotaIns',
'1',
'1',
'1',
'1',
'1',
'1',
'1',
'1',
'LokasiBerdiri',
'1',
'1',
'1',
'MotherMaidenName',
'NamaBelakangInd',
'NamaBelakangIns1',
'NamaBelakangIns2',
'NamaBelakangIns3',
'NamaBelakangIns4',
'NamaDepanInd',
'NamaDepanIns1',
'NamaDepanIns2',
'NamaDepanIns3',
'NamaDepanIns4',
'NamaKantor',
'NamaPerusahaan',
'NamaTengahInd',
'NamaTengahIns1',
'NamaTengahIns2',
'NamaTengahIns3',
'NamaTengahIns4',
'Nationality',
'NationalityOfficer1',
'NationalityOfficer2',
'NationalityOfficer3',
'NationalityOfficer4',
'1',
'NatureOfBusinessLainnya',
'Negara',
'NoIdentitasInd1',
'NoIdentitasInd2',
'NoIdentitasInd3',
'NoIdentitasInd4',
'NoIdentitasIns11',
'NoIdentitasIns12',
'NoIdentitasIns13',
'NoIdentitasIns14',
'NoIdentitasIns21',
'NoIdentitasIns22',
'NoIdentitasIns23',
'NoIdentitasIns24',
'NoIdentitasIns31',
'NoIdentitasIns32',
'NoIdentitasIns33',
'NoIdentitasIns34',
'NoIdentitasIns41',
'NoIdentitasIns42',
'NoIdentitasIns43',
'NoIdentitasIns44',
'NoIdentitasOfficer1',
'NoIdentitasOfficer2',
'NoIdentitasOfficer3',
'NoIdentitasOfficer4',
'NomorAnggaran',
'NomorSIUP',
'NoSKD',
'NPWP',
'NPWPPerson1',
'NPWPPerson2',
'OperatingProfitFor1Year',
'OperatingProfitFor2Year',
'OperatingProfitFor3Year',
'OtherAgama',
'OtherAlamatInd1',
'OtherAlamatInd2',
'OtherAlamatInd3',
'OtherCharacteristic',
'OtherCurrency',
'OtherEmail',
'OtherFax',
'OtherInvestmentObjectives',
'OtherInvestmentObjectivesIns',
'OtherKodeKotaInd1',
'OtherKodeKotaInd2',
'OtherKodeKotaInd3',
'1',
'1',
'1',
'OtherNegaraInd1',
'OtherNegaraInd2',
'OtherNegaraInd3',
'OtherOccupation',
'OtherPendidikan',
'1',
'1',
'1',
'OtherSourceOfFunds',
'OtherSourceOfFundsIns',
'OtherSpouseOccupation',
'OtherTeleponRumah',
'OtherTeleponSelular',
'OtherTipe',
'1',
'1',
'1',
'1',
'PhoneIns1',
'PhoneIns2',
'PlaceOfBirthOfficer1',
'PlaceOfBirthOfficer2',
'PlaceOfBirthOfficer3',
'PlaceOfBirthOfficer4',
'1',
'PolitisFT',
'PolitisLainnya',
'PolitisName',
'1',
'1',
'RDNAccountName',
'RDNAccountNo',
'RDNBankBranchName',
'RDNCurrency',
'Referral',
'1',
'01/01/2010',
'01/01/2010',
'01/01/2010',
'01/01/2010',
'01/01/2010',
'01/01/2010',
'01/01/2010',
'01/01/2010',
'01/01/2010',
'01/01/2010',
'01/01/2010',
'01/01/2010',
'01/01/2010',
'01/01/2010',
'01/01/2010',
'01/01/2010',
'01/01/2010',
'01/01/2010',
'01/01/2010',
'01/01/2010',
'01/01/2010',

'01/01/2010',
'1',
'SACode',
'1',
'1',
'SID',
'01/01/2010',
'1',
'SpouseBirthPlace',
'01/01/2010',
'SpouseIDNo',
'SpouseName',
'SpouseNationality',
'1',
'SpouseNatureOfBusinessOther',
'1',
'1',
'1',
'SubstantialOwnerAddress',
'SubstantialOwnerName',
'SubstantialOwnerTIN',
'1',
'1',
'01/01/2010',
'01/01/2010',
'TeleponBisnis',
'TeleponKantor',
'TeleponRumah',
'TeleponSelular',
'TempatLahir',
'1',
'TINIssuanceCountry',
'1',

";

        //2
        private InsuranceProduct setInsuranceProduct(SqlDataReader dr)
        {
            InsuranceProduct M_InsuranceProduct = new InsuranceProduct();
            //M_InsuranceProduct.InsuranceProductPK = Convert.ToInt32(dr["InsuranceProductPK"]);
            M_InsuranceProduct.FundClientPK = Convert.ToInt32(dr["FundClientPK"]);
            M_InsuranceProduct.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_InsuranceProduct.Status = Convert.ToInt32(dr["Status"]);
            M_InsuranceProduct.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_InsuranceProduct.Notes = Convert.ToString(dr["Notes"]);

            M_InsuranceProduct.ID = dr["ID"].ToString();
            M_InsuranceProduct.Name = dr["Name"].ToString();

            M_InsuranceProduct.NamaBank1 = Convert.ToInt32(dr["NamaBank1"]);
            M_InsuranceProduct.NamaBank1Desc = dr["NamaBank1Desc"].ToString();
            M_InsuranceProduct.NomorRekening1 = dr["NomorRekening1"].ToString();
            M_InsuranceProduct.NamaNasabah1 = dr["NamaNasabah1"].ToString();
            M_InsuranceProduct.MataUang1 = dr["MataUang1"].ToString();
            M_InsuranceProduct.MataUang1Desc = dr["MataUang1Desc"].ToString();
            M_InsuranceProduct.BankBranchName1 = dr["BankBranchName1"].ToString();
            M_InsuranceProduct.RemarkBank1= dr["RemarkBank1"].ToString();

            M_InsuranceProduct.NamaBank2 = Convert.ToInt32(dr["NamaBank2"]);
            M_InsuranceProduct.NamaBank2Desc = dr["NamaBank2Desc"].ToString();
            M_InsuranceProduct.NomorRekening2 = dr["NomorRekening2"].ToString();
            M_InsuranceProduct.NamaNasabah2 = dr["NamaNasabah2"].ToString();
            M_InsuranceProduct.MataUang2 = dr["MataUang2"].ToString();
            M_InsuranceProduct.MataUang2Desc = dr["MataUang2Desc"].ToString();
            M_InsuranceProduct.BankBranchName2 = dr["BankBranchName2"].ToString();
            M_InsuranceProduct.RemarkBank2 = dr["RemarkBank2"].ToString();

            M_InsuranceProduct.NamaBank3 = Convert.ToInt32(dr["NamaBank3"]);
            M_InsuranceProduct.NamaBank3Desc = dr["NamaBank3Desc"].ToString();
            M_InsuranceProduct.NomorRekening3 = dr["NomorRekening3"].ToString();
            M_InsuranceProduct.NamaNasabah3 = dr["NamaNasabah3"].ToString();
            M_InsuranceProduct.MataUang3 = dr["MataUang3"].ToString();
            M_InsuranceProduct.MataUang3Desc = dr["MataUang3Desc"].ToString();
            M_InsuranceProduct.BankBranchName3 = dr["BankBranchName3"].ToString();
            M_InsuranceProduct.RemarkBank3 = dr["RemarkBank3"].ToString();

            M_InsuranceProduct.EntryUsersID = dr["EntryUsersID"].ToString();
            M_InsuranceProduct.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_InsuranceProduct.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_InsuranceProduct.VoidUsersID = dr["VoidUsersID"].ToString();
            M_InsuranceProduct.EntryTime = dr["EntryTime"].ToString();
            M_InsuranceProduct.UpdateTime = dr["UpdateTime"].ToString();
            M_InsuranceProduct.ApprovedTime = dr["ApprovedTime"].ToString();
            M_InsuranceProduct.VoidTime = dr["VoidTime"].ToString();
            M_InsuranceProduct.DBUserID = dr["DBUserID"].ToString();
            M_InsuranceProduct.DBTerminalID = dr["DBTerminalID"].ToString();
            M_InsuranceProduct.LastUpdate = dr["LastUpdate"].ToString();
            M_InsuranceProduct.LastUpdateDB = Convert.ToString(dr["LastUpdateDB"]);
            return M_InsuranceProduct;
        }

        public List<InsuranceProduct> InsuranceProduct_Select(int _status)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<InsuranceProduct> L_InsuranceProduct = new List<InsuranceProduct>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = "Select case when A.status=1 then 'PENDING' else Case When A.status = 2 then 'APPROVED' else Case when A.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,isnull(B1.Name,'') NamaBank1Desc,isnull(B2.Name,'') NamaBank2Desc,isnull(B3.Name,'') NamaBank3Desc,isnull(C1.ID,'') MataUang1Desc,isnull(C2.ID,'') MataUang2Desc,isnull(C3.ID,'') MataUang3Desc,* from FundClient A left join Bank B1 on A.NamaBank1 = B1.BankPK and B1.Status in (1,2) left join Bank B2 on A.NamaBank1 = B2.BankPK and B2.Status in (1,2) left join Bank B3 on A.NamaBank1 = B3.BankPK and B3.Status in (1,2) left join Currency C1 on A.MataUang1 = C1.CurrencyPK and C1.Status in (1,2) left join Currency C2 on A.MataUang2 = C2.CurrencyPK and C2.Status in (1,2) left join Currency C3 on A.MataUang3 = C3.CurrencyPK and C3.Status in (1,2) where A.status = @status";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = "Select case when A.status=1 then 'PENDING' else Case When A.status = 2 then 'APPROVED' else Case when A.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,isnull(B1.Name,'') NamaBank1Desc,isnull(B2.Name,'') NamaBank2Desc,isnull(B3.Name,'') NamaBank3Desc,isnull(C1.ID,'') MataUang1Desc,isnull(C2.ID,'') MataUang2Desc,isnull(C3.ID,'') MataUang3Desc,* from FundClient A left join Bank B1 on A.NamaBank1 = B1.BankPK and B1.Status in (1,2) left join Bank B2 on A.NamaBank1 = B2.BankPK and B2.Status in (1,2) left join Bank B3 on A.NamaBank1 = B3.BankPK and B3.Status in (1,2) left join Currency C1 on A.MataUang1 = C1.CurrencyPK and C1.Status in (1,2) left join Currency C2 on A.MataUang2 = C2.CurrencyPK and C2.Status in (1,2) left join Currency C3 on A.MataUang3 = C3.CurrencyPK and C3.Status in (1,2) ";
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_InsuranceProduct.Add(setInsuranceProduct(dr));
                                }
                            }
                            return L_InsuranceProduct;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int InsuranceProduct_Add(InsuranceProduct _InsuranceProduct, bool _havePrivillege)
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
                                 "Select isnull(max(FundClientPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@LastUpdate from FundClient";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _InsuranceProduct.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);
                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[LastUpdate])" +
                                "Select isnull(max(FundClientPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@LastUpdate from FundClient";
                        }
                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@ID", _InsuranceProduct.ID);
                        cmd.Parameters.AddWithValue("@Name", _InsuranceProduct.Name);
                        cmd.Parameters.AddWithValue("@NomorRekening1", _InsuranceProduct.NomorRekening1);
                        cmd.Parameters.AddWithValue("@NomorRekening2", _InsuranceProduct.NomorRekening2);
                        cmd.Parameters.AddWithValue("@NomorRekening3", _InsuranceProduct.NomorRekening3);
                        cmd.Parameters.AddWithValue("@MataUang1", _InsuranceProduct.MataUang1);
                        cmd.Parameters.AddWithValue("@MataUang2", _InsuranceProduct.MataUang2);
                        cmd.Parameters.AddWithValue("@MataUang3", _InsuranceProduct.MataUang3);
                        cmd.Parameters.AddWithValue("@NamaBank1", _InsuranceProduct.NamaBank1);
                        cmd.Parameters.AddWithValue("@NamaBank2", _InsuranceProduct.NamaBank2);
                        cmd.Parameters.AddWithValue("@NamaBank3", _InsuranceProduct.NamaBank3);
                        cmd.Parameters.AddWithValue("@NamaNasabah1", _InsuranceProduct.NamaNasabah1);
                        cmd.Parameters.AddWithValue("@NamaNasabah2", _InsuranceProduct.NamaNasabah2);
                        cmd.Parameters.AddWithValue("@NamaNasabah3", _InsuranceProduct.NamaNasabah3);
                        cmd.Parameters.AddWithValue("@BankBranchName1", _InsuranceProduct.BankBranchName1);
                        cmd.Parameters.AddWithValue("@BankBranchName2", _InsuranceProduct.BankBranchName2);
                        cmd.Parameters.AddWithValue("@BankBranchName3", _InsuranceProduct.BankBranchName3);
                        cmd.Parameters.AddWithValue("@RemarkBank1", _InsuranceProduct.RemarkBank1);
                        cmd.Parameters.AddWithValue("@RemarkBank2", _InsuranceProduct.RemarkBank2);
                        cmd.Parameters.AddWithValue("@RemarkBank3", _InsuranceProduct.RemarkBank3);

                        //if (_InsuranceProduct.TeleponKantor == "" || _InsuranceProduct.TeleponKantor == null)
                        //{
                        //    cmd.Parameters.AddWithValue("@TeleponKantor", DBNull.Value);
                        //}
                        //else
                        //{
                        //    cmd.Parameters.AddWithValue("@TeleponKantor", _InsuranceProduct.TeleponKantor);
                        //}

                        //if (_InsuranceProduct.NationalityOfficer1 == "" || _InsuranceProduct.NationalityOfficer1 == null)
                        //{
                        //    cmd.Parameters.AddWithValue("@NationalityOfficer1", DBNull.Value);
                        //}
                        //else
                        //{
                        //    cmd.Parameters.AddWithValue("@NationalityOfficer1", _InsuranceProduct.NationalityOfficer1);
                        //}

                        //if (_InsuranceProduct.NationalityOfficer2 == "" || _InsuranceProduct.NationalityOfficer2 == null)
                        //{
                        //    cmd.Parameters.AddWithValue("@NationalityOfficer2", DBNull.Value);
                        //}
                        //else
                        //{
                        //    cmd.Parameters.AddWithValue("@NationalityOfficer2", _InsuranceProduct.NationalityOfficer2);
                        //}

                        //if (_InsuranceProduct.NationalityOfficer3 == "" || _InsuranceProduct.NationalityOfficer3 == null)
                        //{
                        //    cmd.Parameters.AddWithValue("@NationalityOfficer3", DBNull.Value);
                        //}
                        //else
                        //{
                        //    cmd.Parameters.AddWithValue("@NationalityOfficer3", _InsuranceProduct.NationalityOfficer3);
                        //}

                        //if (_InsuranceProduct.NationalityOfficer4 == "" || _InsuranceProduct.NationalityOfficer4 == null)
                        //{
                        //    cmd.Parameters.AddWithValue("@NationalityOfficer4", DBNull.Value);
                        //}
                        //else
                        //{
                        //    cmd.Parameters.AddWithValue("@NationalityOfficer4", _InsuranceProduct.NationalityOfficer4);
                        //}
                        //-------


                        cmd.Parameters.AddWithValue("@EntryUsersID", _InsuranceProduct.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);

                        cmd.ExecuteNonQuery();

                        return _host.Get_LastPKByLastUpate(_dateTimeNow, "FundClient");
                    }

                }
            }
            catch (Exception err)
            {
                throw err;
            }

    }

        public int InsuranceProduct_Update(InsuranceProduct _InsuranceProduct, bool _havePrivillege)
        {

            try
            {
                int _newHisPK;
                int status = _host.Get_Status(_InsuranceProduct.FundClientPK, _InsuranceProduct.HistoryPK, "FundClient");
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update FundClient set status=2, Notes=@Notes,"+
"ID=@ID,Name=@Name,NomorRekening1=@NomorRekening1,NomorRekening2=@NomorRekening2,NomorRekening3=@NomorRekening3,MataUang1=@MataUang1,MataUang2=@MataUang2,MataUang3=@MataUang3,NamaBank1=@NamaBank1,NamaBank2=@NamaBank2,NamaBank3=@NamaBank3,NamaNasabah1=@NamaNasabah1,NamaNasabah2=@NamaNasabah2,NamaNasabah3=@NamaNasabah3,BankBranchName1=@BankBranchName1,BankBranchName2=@BankBranchName2,BankBranchName3=@BankBranchName3,RemarkBank1=@RemarkBank1,RemarkBank2=@RemarkBank2,RemarkBank3=@RemarkBank3, " +
                                "ApprovedUsersID=@ApprovedUsersID, " +
                                "ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@lastupdate " +
                                "where FundClientPK = @PK and historyPK = @HistoryPK";
                            cmd.Parameters.AddWithValue("@HistoryPK", _InsuranceProduct.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _InsuranceProduct.FundClientPK);
                            cmd.Parameters.AddWithValue("@Notes", _InsuranceProduct.Notes);

                            cmd.Parameters.AddWithValue("@ID", _InsuranceProduct.ID);
                            cmd.Parameters.AddWithValue("@Name", _InsuranceProduct.Name);
                            cmd.Parameters.AddWithValue("@NomorRekening1", _InsuranceProduct.NomorRekening1);
                            cmd.Parameters.AddWithValue("@NomorRekening2", _InsuranceProduct.NomorRekening2);
                            cmd.Parameters.AddWithValue("@NomorRekening3", _InsuranceProduct.NomorRekening3);
                            cmd.Parameters.AddWithValue("@MataUang1", _InsuranceProduct.MataUang1);
                            cmd.Parameters.AddWithValue("@MataUang2", _InsuranceProduct.MataUang2);
                            cmd.Parameters.AddWithValue("@MataUang3", _InsuranceProduct.MataUang3);
                            cmd.Parameters.AddWithValue("@NamaNasabah1", _InsuranceProduct.NamaNasabah1);
                            cmd.Parameters.AddWithValue("@NamaNasabah2", _InsuranceProduct.NamaNasabah2);
                            cmd.Parameters.AddWithValue("@NamaNasabah3", _InsuranceProduct.NamaNasabah3);
                            cmd.Parameters.AddWithValue("@NamaBank1", _InsuranceProduct.NamaBank1);
                            cmd.Parameters.AddWithValue("@NamaBank2", _InsuranceProduct.NamaBank2);
                            cmd.Parameters.AddWithValue("@NamaBank3", _InsuranceProduct.NamaBank3);
                            cmd.Parameters.AddWithValue("@BankBranchName1", _InsuranceProduct.BankBranchName1);
                            cmd.Parameters.AddWithValue("@BankBranchName2", _InsuranceProduct.BankBranchName2);
                            cmd.Parameters.AddWithValue("@BankBranchName3", _InsuranceProduct.BankBranchName3);
                            cmd.Parameters.AddWithValue("@RemarkBank1", _InsuranceProduct.RemarkBank1);
                            cmd.Parameters.AddWithValue("@RemarkBank2", _InsuranceProduct.RemarkBank2);
                            cmd.Parameters.AddWithValue("@RemarkBank3", _InsuranceProduct.RemarkBank3);

                            cmd.Parameters.AddWithValue("@UpdateUsersID", _InsuranceProduct.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _InsuranceProduct.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);

                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update FundClient set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where FundClientPK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _InsuranceProduct.FundClientPK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _InsuranceProduct.EntryUsersID);
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
                                cmd.CommandText = "Update FundClient set Notes=@Notes,"+ "ID=@ID,Name=@Name,NomorRekening1=@NomorRekening1,NomorRekening2=@NomorRekening2,NomorRekening3=@NomorRekening3,MataUang1=@MataUang1,MataUang2=@MataUang2,MataUang3=@MataUang3,NamaBank1=@NamaBank1,NamaBank2=@NamaBank2,NamaBank3=@NamaBank3,NamaNasabah1=@NamaNasabah1,NamaNasabah2=@NamaNasabah2,NamaNasabah3=@NamaNasabah3,BankBranchName1=@BankBranchName1,BankBranchName2=@BankBranchName2,BankBranchName3=@BankBranchName3,RemarkBank1=@RemarkBank1,RemarkBank2=@RemarkBank2,RemarkBank3=@RemarkBank3," +
                                "UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@lastupdate " +
                                "where FundClientPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _InsuranceProduct.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _InsuranceProduct.FundClientPK);
                                cmd.Parameters.AddWithValue("@Notes", _InsuranceProduct.Notes);

                                cmd.Parameters.AddWithValue("@ID", _InsuranceProduct.ID);
                                cmd.Parameters.AddWithValue("@Name", _InsuranceProduct.Name);
                                cmd.Parameters.AddWithValue("@NomorRekening1", _InsuranceProduct.NomorRekening1);
                                cmd.Parameters.AddWithValue("@NomorRekening2", _InsuranceProduct.NomorRekening2);
                                cmd.Parameters.AddWithValue("@NomorRekening3", _InsuranceProduct.NomorRekening3);
                                cmd.Parameters.AddWithValue("@MataUang1", _InsuranceProduct.MataUang1);
                                cmd.Parameters.AddWithValue("@MataUang2", _InsuranceProduct.MataUang2);
                                cmd.Parameters.AddWithValue("@MataUang3", _InsuranceProduct.MataUang3);
                                cmd.Parameters.AddWithValue("@NamaNasabah1", _InsuranceProduct.NamaNasabah1);
                                cmd.Parameters.AddWithValue("@NamaNasabah2", _InsuranceProduct.NamaNasabah2);
                                cmd.Parameters.AddWithValue("@NamaNasabah3", _InsuranceProduct.NamaNasabah3);
                                cmd.Parameters.AddWithValue("@NamaBank1", _InsuranceProduct.NamaBank1);
                                cmd.Parameters.AddWithValue("@NamaBank2", _InsuranceProduct.NamaBank2);
                                cmd.Parameters.AddWithValue("@NamaBank3", _InsuranceProduct.NamaBank3);
                                cmd.Parameters.AddWithValue("@BankBranchName1", _InsuranceProduct.BankBranchName1);
                                cmd.Parameters.AddWithValue("@BankBranchName2", _InsuranceProduct.BankBranchName2);
                                cmd.Parameters.AddWithValue("@BankBranchName3", _InsuranceProduct.BankBranchName3);
                                cmd.Parameters.AddWithValue("@RemarkBank1", _InsuranceProduct.RemarkBank1);
                                cmd.Parameters.AddWithValue("@RemarkBank2", _InsuranceProduct.RemarkBank2);
                                cmd.Parameters.AddWithValue("@RemarkBank3", _InsuranceProduct.RemarkBank3);

                                cmd.Parameters.AddWithValue("@UpdateUsersID", _InsuranceProduct.EntryUsersID);
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
                                _newHisPK = _host.Get_NewHistoryPK(_InsuranceProduct.FundClientPK, "FundClient");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From FundClient where FundClientPK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _InsuranceProduct.FundClientPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _InsuranceProduct.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);

                                cmd.Parameters.AddWithValue("@ID", _InsuranceProduct.ID);
                                cmd.Parameters.AddWithValue("@Name", _InsuranceProduct.Name);
                                cmd.Parameters.AddWithValue("@NomorRekening1", _InsuranceProduct.NomorRekening1);
                                cmd.Parameters.AddWithValue("@NomorRekening2", _InsuranceProduct.NomorRekening2);
                                cmd.Parameters.AddWithValue("@NomorRekening3", _InsuranceProduct.NomorRekening3);
                                cmd.Parameters.AddWithValue("@MataUang1", _InsuranceProduct.MataUang1);
                                cmd.Parameters.AddWithValue("@MataUang2", _InsuranceProduct.MataUang2);
                                cmd.Parameters.AddWithValue("@MataUang3", _InsuranceProduct.MataUang3);
                                cmd.Parameters.AddWithValue("@NamaNasabah1", _InsuranceProduct.NamaNasabah1);
                                cmd.Parameters.AddWithValue("@NamaNasabah2", _InsuranceProduct.NamaNasabah2);
                                cmd.Parameters.AddWithValue("@NamaNasabah3", _InsuranceProduct.NamaNasabah3);
                                cmd.Parameters.AddWithValue("@NamaBank1", _InsuranceProduct.NamaBank1);
                                cmd.Parameters.AddWithValue("@NamaBank2", _InsuranceProduct.NamaBank2);
                                cmd.Parameters.AddWithValue("@NamaBank3", _InsuranceProduct.NamaBank3);
                                cmd.Parameters.AddWithValue("@BankBranchName1", _InsuranceProduct.BankBranchName1);
                                cmd.Parameters.AddWithValue("@BankBranchName2", _InsuranceProduct.BankBranchName2);
                                cmd.Parameters.AddWithValue("@BankBranchName3", _InsuranceProduct.BankBranchName3);
                                cmd.Parameters.AddWithValue("@RemarkBank1", _InsuranceProduct.RemarkBank1);
                                cmd.Parameters.AddWithValue("@RemarkBank2", _InsuranceProduct.RemarkBank2);
                                cmd.Parameters.AddWithValue("@RemarkBank3", _InsuranceProduct.RemarkBank3);

                                cmd.Parameters.AddWithValue("@UpdateUsersID", _InsuranceProduct.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update FundClient set status= 4,Notes=@Notes," +
                                " LastUpdate=@lastupdate " +
                                " where FundClientPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _InsuranceProduct.Notes);
                                cmd.Parameters.AddWithValue("@PK", _InsuranceProduct.FundClientPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _InsuranceProduct.HistoryPK);
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

        public void InsuranceProduct_Approved(InsuranceProduct _InsuranceProduct)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update FundClient set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,LastUpdate=@lastupdate " +
                            "where FundClientPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _InsuranceProduct.FundClientPK);
                        cmd.Parameters.AddWithValue("@historyPK", _InsuranceProduct.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _InsuranceProduct.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update FundClient set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where FundClientPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _InsuranceProduct.FundClientPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _InsuranceProduct.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
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

        public void InsuranceProduct_Reject(InsuranceProduct _InsuranceProduct)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update FundClient set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where FundClientPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _InsuranceProduct.FundClientPK);
                        cmd.Parameters.AddWithValue("@historyPK", _InsuranceProduct.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _InsuranceProduct.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update FundClient set status= 2,LastUpdate=@LastUpdate where FundClientPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _InsuranceProduct.FundClientPK);
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

        public void InsuranceProduct_Void(InsuranceProduct _InsuranceProduct)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update FundClient set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where FundClientPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _InsuranceProduct.FundClientPK);
                        cmd.Parameters.AddWithValue("@historyPK", _InsuranceProduct.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _InsuranceProduct.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
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


        // AREA LAIN-LAIN dimulai dari sini ( untuk function diluar standart )

        //public List<GroupsCombo> Groups_Combo()
        //{

        //    try
        //    {
        //        using (SqlConnection DbCon = new SqlConnection(Tools.conString))
        //        {
        //            DbCon.Open();
        //            List<GroupsCombo> L_Groups = new List<GroupsCombo>();
        //            using (SqlCommand cmd = DbCon.CreateCommand())
        //            {
        //                cmd.CommandText = "SELECT  GroupsPK,ID +' - '+ Name ID, Name FROM [Groups]  where status = 2 order by GroupsPK";
        //                using (SqlDataReader dr = cmd.ExecuteReader())
        //                {
        //                    if (dr.HasRows)
        //                    {
        //                        while (dr.Read())
        //                        {
        //                            GroupsCombo M_Groups = new GroupsCombo();
        //                            M_Groups.GroupsPK = Convert.ToInt32(dr["GroupsPK"]);
        //                            M_Groups.ID = Convert.ToString(dr["ID"]);
        //                            M_Groups.Name = Convert.ToString(dr["Name"]);
        //                            L_Groups.Add(M_Groups);
        //                        }

        //                    }
        //                    return L_Groups;
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception err)
        //    {
        //        throw err;
        //    }


        //}



    }
}
