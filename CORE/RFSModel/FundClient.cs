﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RFSModel
{

    public class FundClient
    {
        public int FundClientPK { get; set; }
        public int HistoryPK { get; set; }
        public int Status { get; set; }
        public string StatusDesc { get; set; }
        public string Notes { get; set; }

        public string ReferralIDFO { get; set; }
        public string ComplRequired { get; set; }
        public int RiskProfileScore { get; set; }
        public string RiskProfileScoreDesc { get; set; }
        public string ID { get; set; }
        public string OldID { get; set; }
        public string Name { get; set; }
        public string ClientCategory { get; set; }
        public string ClientCategoryDesc { get; set; }
        public string FundClientSelected { get; set; }
        public string InvestorType { get; set; }
        public string InvestorTypeDesc { get; set; }
        public int InternalCategoryPK { get; set; }
        public string InternalCategoryID { get; set; }
        public int SellingAgentPK { get; set; }
        public string SellingAgentID { get; set; }
        public string SID { get; set; }
        public string IFUACode { get; set; }
        public bool Child { get; set; }
        public bool ARIA { get; set; }
        public bool Registered { get; set; }
        public int JumlahDanaAwal { get; set; }
        public int JumlahDanaSaatIniCash { get; set; }
        public int JumlahDanaSaatIni { get; set; }
        public string Negara { get; set; }
        public string NegaraDesc { get; set; }
        public string Nationality { get; set; }
        public string NationalityDesc { get; set; }
        public string NPWP { get; set; }
        public string SACode { get; set; }
        public int Propinsi { get; set; }
        public string PropinsiDesc { get; set; }
        public string TeleponSelular { get; set; }
        public string Email { get; set; }
        public string Fax { get; set; }
        public string DormantDate { get; set; }
        public string Description { get; set; }
        public int UsersPK { get; set; }
        public string UsersID { get; set; }
        public int JumlahBank { get; set; }
        public int NamaBank1 { get; set; }
        public string NomorRekening1 { get; set; }
        public string BICCode1Name { get; set; }
        public string NamaNasabah1 { get; set; }
        public string MataUang1 { get; set; }
        public string OtherCurrency { get; set; }
        public int NamaBank2 { get; set; }
        public string NomorRekening2 { get; set; }
        public string BICCode2Name { get; set; }
        public string NamaNasabah2 { get; set; }
        public string MataUang2 { get; set; }
        public int NamaBank3 { get; set; }
        public string NomorRekening3 { get; set; }
        public string BICCode3Name { get; set; }
        public string NamaNasabah3 { get; set; }
        public string MataUang3 { get; set; }
        public bool IsFaceToFace { get; set; }
        public int KYCRiskProfile { get; set; }
        public string KYCRiskProfileDesc { get; set; }

        //ARIA INDIVIDUAL
        public string NamaDepanInd { get; set; }
        public string NamaTengahInd { get; set; }
        public string NamaBelakangInd { get; set; }
        public string TempatLahir { get; set; }
        public string TanggalLahir { get; set; }
        public int JenisKelamin { get; set; }
        public string JenisKelaminDesc { get; set; }
        public int StatusPerkawinan { get; set; }
        public string StatusPerkawinanDesc { get; set; }
        public int Pekerjaan { get; set; }
        public string PekerjaanDesc { get; set; }
        public string OtherOccupation { get; set; }
        public int Pendidikan { get; set; }
        public string PendidikanDesc { get; set; }
        public string OtherPendidikan { get; set; }
        public int Agama { get; set; }
        public string AgamaDesc { get; set; }
        public string OtherAgama { get; set; }
        public int PenghasilanInd { get; set; }
        public string PenghasilanIndDesc { get; set; }
        public int SumberDanaInd { get; set; }
        public string SumberDanaIndDesc { get; set; }
        public string OtherSourceOfFunds { get; set; }
        public decimal CapitalPaidIn { get; set; }
        public int MaksudTujuanInd { get; set; }
        public string MaksudTujuanIndDesc { get; set; }
        public string OtherInvestmentObjectives { get; set; }
        public string AlamatInd1 { get; set; }
        public string KodeKotaInd1 { get; set; }
        public string KodeKotaInd1Desc { get; set; }
        public int KodePosInd1 { get; set; }
        public string AlamatInd2 { get; set; }
        public string KodeKotaInd2 { get; set; }
        public string KodeKotaInd2Desc { get; set; }
        public int KodePosInd2 { get; set; }
        //ARIA INSTITUTION
        public string NamaPerusahaan { get; set; }
        public int Domisili { get; set; }
        public string DomisiliDesc { get; set; }
        public int Tipe { get; set; }
        public string TipeDesc { get; set; }
        public string OtherTipe { get; set; }
        public int Karakteristik { get; set; }
        public string KarakteristikDesc { get; set; }
        public string OtherCharacteristic { get; set; }
        public string NoSKD { get; set; }
        public int PenghasilanInstitusi { get; set; }
        public string PenghasilanInstitusiDesc { get; set; }
        public int SumberDanaInstitusi { get; set; }
        public string SumberDanaInstitusiDesc { get; set; }
        public int MaksudTujuanInstitusi { get; set; }
        public string MaksudTujuanInstitusiDesc { get; set; }
        public string OtherSourceOfFundsIns { get; set; }
        public string OtherInvestmentObjectivesIns { get; set; }
        public string AlamatPerusahaan { get; set; }
        public string KodeKotaIns { get; set; }
        public string KodeKotaInsDesc { get; set; }
        public int KodePosIns { get; set; }

        public string TeleponKantor { get; set; }
        public string NationalityOfficer1 { get; set; }
        public string NationalityOfficer1Desc { get; set; }
        public string NationalityOfficer2 { get; set; }
        public string NationalityOfficer2Desc { get; set; }
        public string NationalityOfficer3 { get; set; }
        public string NationalityOfficer3Desc { get; set; }
        public string NationalityOfficer4 { get; set; }
        public string NationalityOfficer4Desc { get; set; }

        public int IdentityTypeOfficer1 { get; set; }
        public int IdentityTypeOfficer2 { get; set; }
        public int IdentityTypeOfficer3 { get; set; }
        public int IdentityTypeOfficer4 { get; set; }

        public string NoIdentitasOfficer1 { get; set; }
        public string NoIdentitasOfficer2 { get; set; }
        public string NoIdentitasOfficer3 { get; set; }
        public string NoIdentitasOfficer4 { get; set; }

        //KYC INDIVIDUAL
        public string SpouseName { get; set; }
        public string MotherMaidenName { get; set; }
        public string AhliWaris { get; set; }
        public string HubunganAhliWaris { get; set; }
        public int IncomeInformation { get; set; }
        public string IncomeInformationDesc { get; set; }
        public int NatureOfBusiness { get; set; }
        public string NatureOfBusinessLainnya { get; set; }
        public int Politis { get; set; }
        public int PolitisRelation { get; set; }
        public string PolitisLainnya { get; set; }
        public string PolitisName { get; set; }
        public string PolitisFT { get; set; }
        public string TeleponRumah { get; set; }
        public string OtherAlamatInd1 { get; set; }
        public string OtherKodeKotaInd1 { get; set; }
        public string OtherKodeKotaInd1Desc { get; set; }
        public int OtherKodePosInd1 { get; set; }
        public int OtherPropinsiInd1 { get; set; }
        public string OtherPropinsiInd1Desc { get; set; }
        public string CountryOfBirth { get; set; }
        public string CountryOfBirthDesc { get; set; }
        public string OtherNegaraInd1 { get; set; }
        public string OtherNegaraInd1Desc { get; set; }
        public string OtherAlamatInd2 { get; set; }
        public string OtherKodeKotaInd2 { get; set; }
        public string OtherKodeKotaInd2Desc { get; set; }
        public int OtherKodePosInd2 { get; set; }
        public int OtherPropinsiInd2 { get; set; }
        public string OtherPropinsiInd2Desc { get; set; }
        public string OtherNegaraInd2 { get; set; }
        public string OtherNegaraInd2Desc { get; set; }
        public string OtherAlamatInd3 { get; set; }
        public string OtherKodeKotaInd3 { get; set; }
        public string OtherKodeKotaInd3Desc { get; set; }
        public int OtherKodePosInd3 { get; set; }
        public int OtherPropinsiInd3 { get; set; }
        public string OtherPropinsiInd3Desc { get; set; }
        public string OtherNegaraInd3 { get; set; }
        public string OtherNegaraInd3Desc { get; set; }
        public string OtherTeleponRumah { get; set; }
        public string OtherTeleponSelular { get; set; }
        public string OtherEmail { get; set; }
        public string OtherFax { get; set; }
        public int JumlahIdentitasInd { get; set; }
        public int IdentitasInd1 { get; set; }
        public string NoIdentitasInd1 { get; set; }
        public string RegistrationDateIdentitasInd1 { get; set; }
        public string ExpiredDateIdentitasInd1 { get; set; }
        public int IdentitasInd2 { get; set; }
        public string NoIdentitasInd2 { get; set; }
        public string RegistrationDateIdentitasInd2 { get; set; }
        public string ExpiredDateIdentitasInd2 { get; set; }
        public int IdentitasInd3 { get; set; }
        public string NoIdentitasInd3 { get; set; }
        public string RegistrationDateIdentitasInd3 { get; set; }
        public string ExpiredDateIdentitasInd3 { get; set; }
        public int IdentitasInd4 { get; set; }
        public string NoIdentitasInd4 { get; set; }
        public string RegistrationDateIdentitasInd4 { get; set; }
        public string ExpiredDateIdentitasInd4 { get; set; }
        //KYC INSTITUTION
        public string RegistrationNPWP { get; set; }
        public string ExpiredDateSKD { get; set; }
        public string TanggalBerdiri { get; set; }
        public string LokasiBerdiri { get; set; }
        public string TeleponBisnis { get; set; }
        public string NomorAnggaran { get; set; }
        public string NomorSIUP { get; set; }
        public string AssetFor1Year { get; set; }
        public string AssetFor2Year { get; set; }
        public string AssetFor3Year { get; set; }
        public string OperatingProfitFor1Year { get; set; }
        public string OperatingProfitFor2Year { get; set; }
        public string OperatingProfitFor3Year { get; set; }
        public int JumlahPejabat { get; set; }
        public string NamaDepanIns1 { get; set; }
        public string NamaTengahIns1 { get; set; }
        public string NamaBelakangIns1 { get; set; }
        public string Jabatan1 { get; set; }
        public int JumlahIdentitasIns1 { get; set; }
        public int IdentitasIns11 { get; set; }
        public string NoIdentitasIns11 { get; set; }
        public string RegistrationDateIdentitasIns11 { get; set; }
        public string ExpiredDateIdentitasIns11 { get; set; }
        public int IdentitasIns12 { get; set; }
        public string NoIdentitasIns12 { get; set; }
        public string RegistrationDateIdentitasIns12 { get; set; }
        public string ExpiredDateIdentitasIns12 { get; set; }
        public int IdentitasIns13 { get; set; }
        public string NoIdentitasIns13 { get; set; }
        public string RegistrationDateIdentitasIns13 { get; set; }
        public string ExpiredDateIdentitasIns13 { get; set; }
        public int IdentitasIns14 { get; set; }
        public string NoIdentitasIns14 { get; set; }
        public string RegistrationDateIdentitasIns14 { get; set; }
        public string ExpiredDateIdentitasIns14 { get; set; }
        public string NamaDepanIns2 { get; set; }
        public string NamaTengahIns2 { get; set; }
        public string NamaBelakangIns2 { get; set; }
        public string Jabatan2 { get; set; }
        public int JumlahIdentitasIns2 { get; set; }
        public int IdentitasIns21 { get; set; }
        public string NoIdentitasIns21 { get; set; }
        public string RegistrationDateIdentitasIns21 { get; set; }
        public string ExpiredDateIdentitasIns21 { get; set; }
        public int IdentitasIns22 { get; set; }
        public string NoIdentitasIns22 { get; set; }
        public string RegistrationDateIdentitasIns22 { get; set; }
        public string ExpiredDateIdentitasIns22 { get; set; }
        public int IdentitasIns23 { get; set; }
        public string NoIdentitasIns23 { get; set; }
        public string RegistrationDateIdentitasIns23 { get; set; }
        public string ExpiredDateIdentitasIns23 { get; set; }
        public int IdentitasIns24 { get; set; }
        public string NoIdentitasIns24 { get; set; }
        public string RegistrationDateIdentitasIns24 { get; set; }
        public string ExpiredDateIdentitasIns24 { get; set; }
        public string NamaDepanIns3 { get; set; }
        public string NamaTengahIns3 { get; set; }
        public string NamaBelakangIns3 { get; set; }
        public string Jabatan3 { get; set; }
        public int JumlahIdentitasIns3 { get; set; }
        public int IdentitasIns31 { get; set; }
        public string NoIdentitasIns31 { get; set; }
        public string RegistrationDateIdentitasIns31 { get; set; }
        public string ExpiredDateIdentitasIns31 { get; set; }
        public int IdentitasIns32 { get; set; }
        public string NoIdentitasIns32 { get; set; }
        public string RegistrationDateIdentitasIns32 { get; set; }
        public string ExpiredDateIdentitasIns32 { get; set; }
        public int IdentitasIns33 { get; set; }
        public string NoIdentitasIns33 { get; set; }
        public string RegistrationDateIdentitasIns33 { get; set; }
        public string ExpiredDateIdentitasIns33 { get; set; }
        public int IdentitasIns34 { get; set; }
        public string NoIdentitasIns34 { get; set; }
        public string RegistrationDateIdentitasIns34 { get; set; }
        public string ExpiredDateIdentitasIns34 { get; set; }
        public string NamaDepanIns4 { get; set; }
        public string NamaTengahIns4 { get; set; }
        public string NamaBelakangIns4 { get; set; }
        public string Jabatan4 { get; set; }
        public int JumlahIdentitasIns4 { get; set; }
        public int IdentitasIns41 { get; set; }
        public string NoIdentitasIns41 { get; set; }
        public string RegistrationDateIdentitasIns41 { get; set; }
        public string ExpiredDateIdentitasIns41 { get; set; }
        public int IdentitasIns42 { get; set; }
        public string NoIdentitasIns42 { get; set; }
        public string RegistrationDateIdentitasIns42 { get; set; }
        public string ExpiredDateIdentitasIns42 { get; set; }
        public int IdentitasIns43 { get; set; }
        public string NoIdentitasIns43 { get; set; }
        public string RegistrationDateIdentitasIns43 { get; set; }
        public string ExpiredDateIdentitasIns43 { get; set; }
        public int IdentitasIns44 { get; set; }
        public string NoIdentitasIns44 { get; set; }
        public string RegistrationDateIdentitasIns44 { get; set; }
        public string ExpiredDateIdentitasIns44 { get; set; }


        // S-INVEST
        public string BIMemberCode1 { get; set; }
        public string BIMemberCode2 { get; set; }
        public string BIMemberCode3 { get; set; }
        public string PhoneIns1 { get; set; }
        public string EmailIns1 { get; set; }
        public string PhoneIns2 { get; set; }
        public string EmailIns2 { get; set; }
        public int InvestorsRiskProfile { get; set; }
        public string InvestorsRiskProfileDesc { get; set; }
        public int AssetOwner { get; set; }
        public string AssetOwnerDesc { get; set; }
        public int StatementType { get; set; }
        public string StatementTypeDesc { get; set; }
        public int FATCACRS { get; set; }
        public string FATCACRSDesc { get; set; }
        public int FATCA { get; set; }
        public string FATCADesc { get; set; }
        public string TIN { get; set; }
        public string TINIssuanceCountry { get; set; }
        public string TINIssuanceCountryDesc { get; set; }
        public string GIIN { get; set; }



        public string SubstantialOwnerName { get; set; }
        public string SubstantialOwnerAddress { get; set; }
        public string SubstantialOwnerTIN { get; set; }
        public string BankBranchName1 { get; set; }
        public string BankBranchName2 { get; set; }
        public string BankBranchName3 { get; set; }
        public string BankCountry1 { get; set; }
        public string BankCountry1Desc { get; set; }
        public string BankCountry2 { get; set; }
        public string BankCountry2Desc { get; set; }
        public string BankCountry3 { get; set; }
        public string BankCountry3Desc { get; set; }

        public string CountryofCorrespondence { get; set; }
        public string CountryofCorrespondenceDesc { get; set; }
        public string CountryofDomicile { get; set; }
        public string CountryofDomicileDesc { get; set; }
        public string SIUPExpirationDate { get; set; }
        public string CountryofEstablishment { get; set; }
        public string CountryofEstablishmentDesc { get; set; }
        public string CompanyCityName { get; set; }
        public string CompanyCityNameDesc { get; set; }
        public string CountryofCompany { get; set; }
        public string CountryofCompanyDesc { get; set; }
        public string NPWPPerson1 { get; set; }
        public string NPWPPerson2 { get; set; }
        public bool BitIsSuspend { get; set; }
        public string EntryUsersID { get; set; }
        public string EntryTime { get; set; }
        public string UpdateUsersID { get; set; }
        public string UpdateTime { get; set; }
        public string ApprovedUsersID { get; set; }
        public string ApprovedTime { get; set; }
        public string VoidUsersID { get; set; }
        public string VoidTime { get; set; }
        public string DBUserID { get; set; }
        public string DBTerminalID { get; set; }
        public string LastUpdate { get; set; }
        public string LastUpdateDB { get; set; }
        public string ParamCategory { get; set; }
        public string ParamInvestorType { get; set; }
        public string ParamDate { get; set; }

        public string SuspendBy { get; set; }
        public string SuspendTime { get; set; }
        public string UnSuspendBy { get; set; }
        public string UnSuspendTime { get; set; }

        public bool BitIsAfiliated { get; set; }
        public int AfiliatedFrom { get; set; }
        public bool BitDefaultPayment1 { get; set; }
        public bool BitDefaultPayment2 { get; set; }
        public bool BitDefaultPayment3 { get; set; }

        public string AlamatTIN { get; set; }
        public string SpouseParentSourceOfFund { get; set; }
        public string Heir { get; set; }
        public string HeirRelationship { get; set; }
        public string SpouseParentIDNumber { get; set; }
        public string SpouseParentCompanyName { get; set; }
        public string SpouseParentOfficePosition { get; set; }
        public string SpouseParentCompanyAddress { get; set; }

        // RDN
        public int BankRDNPK { get; set; }
        public string RDNAccountNo { get; set; }
        public string RDNAccountName { get; set; }
        public string RDNBankBranchName { get; set; }
        public string RDNCurrency { get; set; }

        //SPOUSE
        public string SpouseBirthPlace { get; set; }
        public string SpouseDateOfBirth { get; set; }
        public int SpouseOccupation { get; set; }
        public string SpouseOccupationDesc { get; set; }
        public string OtherSpouseOccupation { get; set; }
        public int SpouseNatureOfBusiness { get; set; }
        public string SpouseNatureOfBusinessDesc { get; set; }
        public string SpouseNatureOfBusinessOther { get; set; }
        public string SpouseIDNo { get; set; }
        public string SpouseNationality { get; set; }
        public string SpouseNationalityDesc { get; set; }
        public string SpouseAnnualIncome { get; set; }


        public string AlamatKantorInd { get; set; }
        public int KodeKotaKantorInd { get; set; }
        public string KodeKotaKantorIndDesc { get; set; }
        public int KodePosKantorInd { get; set; }
        public int KodePropinsiKantorInd { get; set; }
        public string KodePropinsiKantorIndDesc { get; set; }
        public string KodeCountryofKantor { get; set; }
        public string KodeCountryofKantorDesc { get; set; }
        public string CorrespondenceRT { get; set; }
        public string CorrespondenceRW { get; set; }
        public string DomicileRT { get; set; }
        public string DomicileRW { get; set; }
        public string Identity1RT { get; set; }
        public string Identity1RW { get; set; }
        public int KodeDomisiliPropinsi { get; set; }
        public string KodeDomisiliPropinsiDesc { get; set; }
        public string NamaKantor { get; set; }
        public int OfficePosition { get; set; }
        public string OfficePositionDesc { get; set; }
        public string JabatanKantor { get; set; }

        public string CompanyFax { get; set; }

        public string CompanyMail { get; set; }
        public int StatusPengkinianData { get; set; }
        public string DatePengkinianData { get; set; }
        public int MigrationStatus { get; set; }
        public int SegmentClass { get; set; }
        public int CompanyTypeOJK { get; set; }
        public int Legality { get; set; }
        public string RenewingDate { get; set; }
        public bool BitShareAbleToGroup { get; set; }
        public string RemarkBank1 { get; set; }
        public string RemarkBank2 { get; set; }
        public string RemarkBank3 { get; set; }
        public string SumberDana { get; set; }
        public bool CantSubs { get; set; }
        public bool CantRedempt { get; set; }
        public bool CantSwitch { get; set; }

        public string BeneficialName { get; set; }
        public string BeneficialAddress { get; set; }
        public string BeneficialIdentity { get; set; }
        public int BeneficialWork { get; set; }
        public string BeneficialRelation { get; set; }
        public string BeneficialHomeNo { get; set; }
        public string BeneficialPhoneNumber { get; set; }
        public string BeneficialNPWP { get; set; }
        public int ClientOnBoard { get; set; }
        public string ClientOnBoardDesc { get; set; }
        public string Referral { get; set; }
        public bool BitisTA { get; set; }
        public int NatureOfBusinessInsti { get; set; }
        public string NatureOfBusinessInstiDesc { get; set; }
        public string BICCode1 { get; set; }
        public string BICCode2 { get; set; }
        public string BICCode3 { get; set; }
        public string NatureOfBusinessDesc { get; set; }
        public string CompanyTypeOJKDesc { get; set; }
        public int BusinessTypeOJK { get; set; }
        public string BusinessTypeOJKDesc { get; set; }

        public string AlamatOfficer1 { get; set; }
        public string AlamatOfficer2 { get; set; }
        public string AlamatOfficer3 { get; set; }
        public string AlamatOfficer4 { get; set; }
        public int AgamaOfficer1 { get; set; }
        public string AgamaOfficer1Desc { get; set; }
        public int AgamaOfficer2 { get; set; }
        public string AgamaOfficer2Desc { get; set; }
        public int AgamaOfficer3 { get; set; }
        public string AgamaOfficer3Desc { get; set; }
        public int AgamaOfficer4 { get; set; }
        public string AgamaOfficer4Desc { get; set; }
        public string PlaceOfBirthOfficer1 { get; set; }
        public string PlaceOfBirthOfficer2 { get; set; }
        public string PlaceOfBirthOfficer3 { get; set; }
        public string PlaceOfBirthOfficer4 { get; set; }
        public string DOBOfficer1 { get; set; }
        public string DOBOfficer2 { get; set; }
        public string DOBOfficer3 { get; set; }
        public string DOBOfficer4 { get; set; }
        public string FaceToFaceDate { get; set; }
        public string FrontID { get; set; }
        public int EmployerLineOfBusiness { get; set; }
        public string EmployerLineOfBusinessDesc { get; set; }
        public string OpeningDateSinvest { get; set; }
        public int StatusAffiliated { get; set; }
        public string IdentitasInd1Desc { get; set; }
        public string IdentitasInd2Desc { get; set; }
        public string BeneficialName1 { get; set; }
        public string BeneficialAddress1 { get; set; }
        public string BeneficialIdentity1 { get; set; }
        public string BeneficialRelation1 { get; set; }
        public string BeneficialNPWP1 { get; set; }
        public int BeneficialIdentitasInd1 { get; set; }
        public string BeneficialIdentitasInd1Desc { get; set; }

        public string BeneficialName2 { get; set; }
        public string BeneficialAddress2 { get; set; }
        public string BeneficialIdentity2 { get; set; }
        public string BeneficialRelation2 { get; set; }
        public string BeneficialNPWP2 { get; set; }
        public int BeneficialIdentitasInd2 { get; set; }
        public string BeneficialIdentitasInd2Desc { get; set; }

        public string BeneficialName3 { get; set; }
        public string BeneficialAddress3 { get; set; }
        public string BeneficialIdentity3 { get; set; }
        public string BeneficialRelation3 { get; set; }
        public string BeneficialNPWP3 { get; set; }
        public int BeneficialIdentitasInd3 { get; set; }
        public string BeneficialIdentitasInd3Desc { get; set; }

        public int BeneficialIdentitasInd { get; set; }
        public string BeneficialIdentitasIndDesc { get; set; }

        public int APUPPTOccupation { get; set; }
        public string APUPPTOccupationDesc { get; set; }

        public string SelfieImgUrl { get; set; }
        public string KtpImgUrl { get; set; }
        public string DocPath { get; set; }
        public string FlagFailedSinvest { get; set; }
        public string FailedSinvestDesc { get; set; }

        public string ParamSInvestDateFrom { get; set; }
        public string ParamSInvestDateTo { get; set; }

        public decimal TotalAsset { get; set; }
    }

    public class FundClientCombo
    {
        public int FundClientPK { get; set; }
        public string ID { get; set; }
        public string Name { get; set; }
        public string BankName { get; set; }
        public string AccountNo { get; set; }
        public string InternalCategory { get; set; }
        public int InternalCategoryPK { get; set; }
        public int BankRecipientPK { get; set; }
        public string IFUA { get; set; }
        public decimal UnitAmount { get; set; }
        public string SID { get; set; }
        
    }

    public class FundClientComboVA
    {
        public int FundClientPK { get; set; }
        public int BankRecipientPK { get; set; }
        public string BankName { get; set; }
        public string AccountNo { get; set; }
        public string Currency { get; set; }
        public string AccountName { get; set; }

    }

    public class FundClientIdentity
    {
        public string Base64 { get; set; }
        public string Type { get; set; }

    }

    public class FundClientTrx
    {
        public int FundClientPK { get; set; }
        public string ID { get; set; }
        public string FundClientName { get; set; }
        public string IFUA { get; set; }
        public decimal UnitAmount { get; set; }

    }

    public class FundClientSearchResult
    {
        public bool Selected { get; set; }
        public int FundClientPK { get; set; }
        public int HistoryPK { get; set; }
        public string ComplRequired { get; set; }
        public string ID { get; set; }
        public string Name { get; set; }
        public string SellingAgentID { get; set; }
        public string Email { get; set; }
        public string TeleponSelular { get; set; }
        public string NamaBank1 { get; set; }
        public string NomorRekening1 { get; set; }
        public string InvestorTypeDesc { get; set; }
        public string AgentName { get; set; }
        public string IFUACode { get; set; }
        public string SID { get; set; }
        public string NoIdentitasInd1 { get; set; }
        public string TanggalLahir { get; set; }
        public int KYCRiskProfile { get; set; }
        public string EntryUsersID { get; set; }
        public string EntryTime { get; set; }
        public string UpdateUsersID { get; set; }
        public string UpdateTime { get; set; }

        public string SuspendBy { get; set; }
        public string SuspendTime { get; set; }
        public string UnSuspendTime { get; set; }
        public string UnSuspendBy { get; set; }
        public string FrontID { get; set; }

        public string LastUpdate { get; set; }
        public bool BitIsSuspend { get; set; }
        public string KYCRiskProfileDesc { get; set; }
        public string FlagFailedSinvest { get; set; }

    }

    public class GetSummary
    {
        public int FundClientPK { get; set; }
        public int FundPK { get; set; }
        public string FundID { get; set; }
        public decimal UnitAmount { get; set; }
        public decimal Nav { get; set; }
        public decimal TotalAmount { get; set; }

    }


    public class NKPD
    {
        public string FundName { get; set; }
        public string KodeProduk { get; set; }
        public string KodeBK { get; set; }
        public int JmlNasabahPerorangan { get; set; }
        public decimal DanaNasabahPerorangan { get; set; }
        public int JmlNasabahLembagaPE { get; set; }
        public decimal DanaNasabahLembagaPE { get; set; }
        public int JmlNasabahLembagaDAPEN { get; set; }
        public decimal DanaNasabahLembagaDAPEN { get; set; }
        public int JmlNasabahLembagaAsuransi { get; set; }
        public decimal DanaNasabahLembagaAsuransi { get; set; }
        public int JmlNasabahLembagaBank { get; set; }
        public decimal DanaNasabahLembagaBank { get; set; }
        public int JmlNasabahLembagaSwasta { get; set; }
        public decimal DanaNasabahLembagaSwasta { get; set; }
        public int JmlNasabahLembagaBUMN { get; set; }
        public decimal DanaNasabahLembagaBUMN { get; set; }
        public int JmlNasabahLembagaBUMD { get; set; }
        public decimal DanaNasabahLembagaBUMD { get; set; }
        public int JmlNasabahLembagaYayasan { get; set; }
        public decimal DanaNasabahLembagaYayasan { get; set; }
        public int JmlNasabahLembagaKoperasi { get; set; }
        public decimal DanaNasabahLembagaKoperasi { get; set; }
        public int JmlNasabahLembagaLainnya { get; set; }
        public decimal DanaNasabahLembagaLainnya { get; set; }
        public int JmlAsingPerorangan { get; set; }
        public decimal DanaAsingPerorangan { get; set; }
        public int JmlAsingLembagaPE { get; set; }
        public decimal DanaAsingLembagaPE { get; set; }
        public int JmlAsingLembagaDAPEN { get; set; }
        public decimal DanaAsingLembagaDAPEN { get; set; }
        public int JmlAsingLembagaAsuransi { get; set; }
        public decimal DanaAsingLembagaAsuransi { get; set; }
        public int JmlAsingLembagaBank { get; set; }
        public decimal DanaAsingLembagaBank { get; set; }
        public int JmlAsingLembagaSwasta { get; set; }
        public decimal DanaAsingLembagaSwasta { get; set; }
        public int JmlAsingLembagaBUMN { get; set; }
        public decimal DanaAsingLembagaBUMN { get; set; }
        public int JmlAsingLembagaBUMD { get; set; }
        public decimal DanaAsingLembagaBUMD { get; set; }
        public int JmlAsingLembagaYayasan { get; set; }
        public decimal DanaAsingLembagaYayasan { get; set; }
        public int JmlAsingLembagaKoperasi { get; set; }
        public decimal DanaAsingLembagaKoperasi { get; set; }
        public int JmlAsingLembagaLainnya { get; set; }
        public decimal DanaAsingLembagaLainnya { get; set; }
        public decimal InvestasiDN { get; set; }
        public decimal InvestasiLN { get; set; }

    }



    public class ARIA
    {
        public string IFUACode { get; set; }
        public string NamaDepanInd { get; set; }
        public string NamaTengahInd { get; set; }
        public string NamaBelakangInd { get; set; }
        public string IdentitasInd1 { get; set; }
        public string NoIdentitasInd1 { get; set; }
        public string NPWP { get; set; }
        public string TempatLahir { get; set; }
        public string TanggalLahir { get; set; }
        public string JenisKelamin { get; set; }
        public string StatusPerkawinan { get; set; }
        public string Nationality { get; set; }
        public string Pekerjaan { get; set; }
        public string Pendidikan { get; set; }
        public string Agama { get; set; }
        public string SumberDanaInd { get; set; }
        public string MaksudTujuanInd { get; set; }
        public string PenghasilanInd { get; set; }
        public string OtherAlamatInd1 { get; set; }
        public string KodeKotaInd1 { get; set; }
        public string KodePosInd1 { get; set; }
        public string AlamatInd2 { get; set; }
        public string KodeKotaInd2 { get; set; }
        public string KodePosInd2 { get; set; }
        public string SID { get; set; }

        //aria insti
        public string NamaPerusahaan { get; set; }
        public string Negara { get; set; }
        public string Tipe { get; set; }
        public string Karakteristik { get; set; }
        public string NoSKD { get; set; }
        public string TanggalBerdiri { get; set; }
        public string SumberDanaInstitusi { get; set; }
        public string MaksudTujuanInstitusi { get; set; }
        public string PenghasilanInstitusi { get; set; }
        public string AlamatPerusahaan { get; set; }
        public string KodeKotaIns { get; set; }
        public string KodePosIns { get; set; }

    }

    public class HutangValas
    {
        public decimal HutangBankUSD { get; set; }
        public decimal HutangBankEUR { get; set; }
        public decimal HutangBankJPY { get; set; }
        public decimal HutangBankGBP { get; set; }
        public decimal HutangBankAUD { get; set; }
        public decimal HutangBankSGD { get; set; }
        public decimal HutangBankLainnya { get; set; }
        public decimal HutangObligasiUSD { get; set; }
        public decimal HutangObligasiEUR { get; set; }
        public decimal HutangObligasiJPY { get; set; }
        public decimal HutangObligasiGBP { get; set; }
        public decimal HutangObligasiAUD { get; set; }
        public decimal HutangObligasiSGD { get; set; }
        public decimal HutangObligasiLainnya { get; set; }
        public decimal HutanglainlainUSD { get; set; }
        public decimal HutanglainlainEUR { get; set; }
        public decimal HutanglainlainJPY { get; set; }
        public decimal HutanglainlainGBP { get; set; }
        public decimal HutanglainlainAUD { get; set; }
        public decimal HutanglainlainSGD { get; set; }
        public decimal HutanglainlainLainnya { get; set; }

    }
    public class SIDRpt
    {
        public int FundClientPK { get; set; }
        public string Name { get; set; }
        public string SID { get; set; }
        public string Email { get; set; }
        public string Alamat { get; set; }
        public string IFUACode { get; set; }
        public string NoIdentitas { get; set; }
        public string KodePos { get; set; }
        public string Body { get; set; }
        public string LetterNo { get; set; }
        public string Month { get; set; }
        public string Path { get; set; }
        public string KodeKotaInd1 { get; set; }
        public string ReportName { get; set; }
        public string ClientName { get; set; }
        public string InvestorType { get; set; } 


    }


    public class UnitTrustReport
    {
        public string ClientName { get; set; }
        public string FundName { get; set; }
        public string FundID { get; set; }

        public string TrxDate { get; set; }
        public string PostedTime { get; set; }
        public string AccountNo { get; set; }
        public string Address { get; set; }
        public string CurrencyID { get; set; }
        public string TRXType { get; set; }
        public decimal Amount { get; set; }
        public decimal FeeAmount { get; set; }
        public decimal NAV { get; set; }
        public decimal AvgCost { get; set; }
        public decimal TotalUnitAmount { get; set; }
        public decimal BeginningBalance { get; set; }
        public decimal TotalCashAmount { get; set; }
        public decimal EndAVG { get; set; }
        public decimal EndNAV { get; set; }
        public decimal NetValue { get; set; }

    }


    public class GetHistoricalSummary
    {

        public string TradeDate { get; set; }
        public string FundID { get; set; }
        public string Type { get; set; }
        public decimal CashAmount { get; set; }
        public decimal UnitAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal NAV { get; set; }

    }

    public class GetPositionSummary
    {

        public string Date { get; set; }
        public string FundID { get; set; }
        public string FundName { get; set; }
        public string CurrencyID { get; set; }
        public decimal UnitAmount { get; set; }
        public decimal NAV { get; set; }
        public decimal Balance { get; set; }
        public decimal BalanceUSD { get; set; }

    }


    public class ParamUnitRegistryBySelected
    {
        public string UnitRegistrySelected { get; set; }
        public string ClientSubscriptionSelected { get; set; }
        public string ClientRedemptionSelected { get; set; }
        public string ClientSwitchingSelected { get; set; }


    }
}