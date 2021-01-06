﻿using System;
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
using SucorInvest.Connect;



namespace RFSRepository
{
    public class FundClientReps
    {
        Host _host = new Host();

        //1
        string _insertCommand = @"INSERT INTO [dbo].[FundClient]  
        ([FundClientPK],[HistoryPK],[Status],[ID],[Name], 
        [ClientCategory],[InvestorType],[InternalCategoryPK],[SellingAgentPK],[UsersPK],[SID],[IFUACode], 
        [ARIA],[Registered],
        [Negara],[Nationality],[NPWP],[SACode],[Propinsi],[TeleponSelular], 
        [Email],[Fax],[DormantDate],[Description],[NamaBank1],
        [NomorRekening1],[NamaNasabah1],[MataUang1],[NamaBank2],
        [NomorRekening2],[NamaNasabah2],[MataUang2],[NamaBank3],
        [NomorRekening3],[NamaNasabah3],[MataUang3],[NamaDepanInd],
        [NamaTengahInd],[NamaBelakangInd],[TempatLahir],[TanggalLahir],[JenisKelamin],
        [StatusPerkawinan],[Pekerjaan],[Pendidikan],[Agama],[PenghasilanInd],
        [SumberDanaInd],[MaksudTujuanInd],[AlamatInd1],[KodeKotaInd1],[KodePosInd1],
        [AlamatInd2],[KodeKotaInd2],[KodePosInd2],[NamaPerusahaan],[Domisili],
        [Tipe],[Karakteristik],[NoSKD],[PenghasilanInstitusi],[SumberDanaInstitusi],
        [MaksudTujuanInstitusi],[AlamatPerusahaan],[KodeKotaIns],[KodePosIns],[SpouseName],[MotherMaidenName], 
        [AhliWaris],[HubunganAhliWaris],[NatureOfBusiness],[NatureOfBusinessLainnya],[Politis],
        [PolitisLainnya],[PolitisName],[PolitisFT],[TeleponRumah],[OtherAlamatInd1],[OtherKodeKotaInd1],[OtherKodePosInd1],
        [OtherPropinsiInd1],[CountryOfBirth],[OtherNegaraInd1],[OtherAlamatInd2],[OtherKodeKotaInd2],[OtherKodePosInd2],
        [OtherPropinsiInd2],[OtherNegaraInd2],[OtherAlamatInd3],[OtherKodeKotaInd3],[OtherKodePosInd3],
        [OtherPropinsiInd3],[OtherNegaraInd3],[OtherTeleponRumah],[OtherTeleponSelular],[OtherEmail],
        [OtherFax],[IdentitasInd1],[NoIdentitasInd1],[RegistrationDateIdentitasInd1],
        [ExpiredDateIdentitasInd1],[IdentitasInd2],[NoIdentitasInd2],[RegistrationDateIdentitasInd2],[ExpiredDateIdentitasInd2],
        [IdentitasInd3],[NoIdentitasInd3],[RegistrationDateIdentitasInd3],[ExpiredDateIdentitasInd3],[IdentitasInd4],
        [NoIdentitasInd4],[RegistrationDateIdentitasInd4],[ExpiredDateIdentitasInd4],[RegistrationNPWP],
        [ExpiredDateSKD],[TanggalBerdiri],[LokasiBerdiri],[TeleponBisnis],[NomorAnggaran],
        [NomorSIUP],[AssetFor1Year],[AssetFor2Year],[AssetFor3Year],[OperatingProfitFor1Year],
        [OperatingProfitFor2Year],[OperatingProfitFor3Year],[NamaDepanIns1],[NamaTengahIns1],
        [NamaBelakangIns1],[Jabatan1],[IdentitasIns11],[NoIdentitasIns11],
        [RegistrationDateIdentitasIns11],[ExpiredDateIdentitasIns11],[IdentitasIns12],[NoIdentitasIns12],[RegistrationDateIdentitasIns12],
        [ExpiredDateIdentitasIns12],[IdentitasIns13],[NoIdentitasIns13],[RegistrationDateIdentitasIns13],[ExpiredDateIdentitasIns13],
        [IdentitasIns14],[NoIdentitasIns14],[RegistrationDateIdentitasIns14],[ExpiredDateIdentitasIns14],[NamaDepanIns2],
        [NamaTengahIns2],[NamaBelakangIns2],[Jabatan2],[IdentitasIns21],
        [NoIdentitasIns21],[RegistrationDateIdentitasIns21],[ExpiredDateIdentitasIns21],[IdentitasIns22],[NoIdentitasIns22],
        [RegistrationDateIdentitasIns22],[ExpiredDateIdentitasIns22],[IdentitasIns23],[NoIdentitasIns23],[RegistrationDateIdentitasIns23],
        [ExpiredDateIdentitasIns23],[IdentitasIns24],[NoIdentitasIns24],[RegistrationDateIdentitasIns24],[ExpiredDateIdentitasIns24],
        [NamaDepanIns3],[NamaTengahIns3],[NamaBelakangIns3],[Jabatan3],[JumlahIdentitasIns3],
        [IdentitasIns31],[NoIdentitasIns31],[RegistrationDateIdentitasIns31],[ExpiredDateIdentitasIns31],[IdentitasIns32],
        [NoIdentitasIns32],[RegistrationDateIdentitasIns32],[ExpiredDateIdentitasIns32],[IdentitasIns33],[NoIdentitasIns33],
        [RegistrationDateIdentitasIns33],[ExpiredDateIdentitasIns33],[IdentitasIns34],[NoIdentitasIns34],[RegistrationDateIdentitasIns34],
        [ExpiredDateIdentitasIns34],[NamaDepanIns4],[NamaTengahIns4],[NamaBelakangIns4],[Jabatan4],
        [JumlahIdentitasIns4],[IdentitasIns41],[NoIdentitasIns41],[RegistrationDateIdentitasIns41],[ExpiredDateIdentitasIns41],
        [IdentitasIns42],[NoIdentitasIns42],[RegistrationDateIdentitasIns42],[ExpiredDateIdentitasIns42],[IdentitasIns43],
        [NoIdentitasIns43],[RegistrationDateIdentitasIns43],[ExpiredDateIdentitasIns43],[IdentitasIns44],[NoIdentitasIns44],
        [RegistrationDateIdentitasIns44],[ExpiredDateIdentitasIns44],[PhoneIns1],[EmailIns1],  
        [PhoneIns2],[EmailIns2],[InvestorsRiskProfile],[AssetOwner],[StatementType],[FATCA],[TIN],[TINIssuanceCountry],[GIIN],[SubstantialOwnerName], 
        [SubstantialOwnerAddress],[SubstantialOwnerTIN],[BankBranchName1],[BankBranchName2],[BankBranchName3],[CountryofCorrespondence],[CountryofDomicile], 
        [SIUPExpirationDate],[CountryofEstablishment],[CompanyCityName],[CountryofCompany],[NPWPPerson1],[NPWPPerson2],[BankRDNPK],[RDNAccountNo],[RDNAccountName],[RiskProfileScore],[RDNBankBranchName],[RDNCurrency],
        [SpouseBirthPlace],[SpouseDateOfBirth],[SpouseOccupation],[SpouseNatureOfBusiness],[SpouseNatureOfBusinessOther],[SpouseIDNo],[SpouseNationality],[SpouseAnnualIncome],[IsFaceToFace],[KYCRiskProfile],[BitDefaultPayment1],[BitDefaultPayment2],[BitDefaultPayment3],[AlamatKantorInd],
        [KodeKotaKantorInd],[KodePosKantorInd],[KodePropinsiKantorInd],[KodeCountryofKantor],[CorrespondenceRT],[CorrespondenceRW],[DomicileRT],[DomicileRW],[Identity1RT],[Identity1RW],[KodeDomisiliPropinsi],
        [NamaKantor],[JabatanKantor],[OtherAgama],[OtherPendidikan],[OtherOccupation],[OtherSpouseOccupation],[OtherCurrency],[OtherSourceOfFunds],
        [OtherInvestmentObjectives],[OtherTipe],[OtherCharacteristic],[OtherInvestmentObjectivesIns],[OtherSourceOfFundsIns],[CompanyFax],[CompanyMail], [MigrationStatus], [SegmentClass], [CompanyTypeOJK], [Legality], [RenewingDate], [BitShareAbleToGroup], [RemarkBank1], [RemarkBank2], [RemarkBank3],[CantSubs],[CantRedempt],[CantSwitch],
        [BeneficialName],[BeneficialAddress],[BeneficialIdentity],[BeneficialWork],[BeneficialRelation],[BeneficialHomeNo],[BeneficialPhoneNumber],[BeneficialNPWP],[ClientOnBoard],[Referral],[BitisTA],
        [AlamatOfficer1],[AlamatOfficer2],[AlamatOfficer3],[AlamatOfficer4],[AgamaOfficer1],[AgamaOfficer2],[AgamaOfficer3],[AgamaOfficer4],[PlaceOfBirthOfficer1],[PlaceOfBirthOfficer2],[PlaceOfBirthOfficer3],[PlaceOfBirthOfficer4],[DOBOfficer1],[DOBOfficer2],[DOBOfficer3],[DOBOfficer4],[FaceToFaceDate],[EmployerLineOfBusiness],[FrontID],
        [TeleponKantor],[NationalityOfficer1],[NationalityOfficer2],[NationalityOfficer3],[NationalityOfficer4],
        [IdentityTypeOfficer1],[IdentityTypeOfficer2],[IdentityTypeOfficer3],[IdentityTypeOfficer4],[NoIdentitasOfficer1],[NoIdentitasOfficer2],[NoIdentitasOfficer3],[NoIdentitasOfficer4],[PolitisRelation],[CapitalPaidIn],
        ";


        string _paramaterCommand = @"@ID,@Name, 
        @ClientCategory,@InvestorType,@InternalCategoryPK,@SellingAgentPK,@UsersPK,@SID,@IFUACode, 
        @ARIA,@Registered,
        @Negara,@Nationality,@NPWP,@SACode,@Propinsi,@TeleponSelular, 
        @Email,@Fax,@DormantDate,@Description,@NamaBank1,
        @NomorRekening1,@NamaNasabah1,@MataUang1,@NamaBank2,
        @NomorRekening2,@NamaNasabah2,@MataUang2,@NamaBank3,
        @NomorRekening3,@NamaNasabah3,@MataUang3,@NamaDepanInd,
        @NamaTengahInd,@NamaBelakangInd,@TempatLahir,@TanggalLahir,@JenisKelamin,
        @StatusPerkawinan,@Pekerjaan,@Pendidikan,@Agama,@PenghasilanInd,
        @SumberDanaInd,@MaksudTujuanInd,@AlamatInd1,@KodeKotaInd1,@KodePosInd1,
        @AlamatInd2,@KodeKotaInd2,@KodePosInd2,@NamaPerusahaan,@Domisili,
        @Tipe,@Karakteristik,@NoSKD,@PenghasilanInstitusi,@SumberDanaInstitusi,
        @MaksudTujuanInstitusi,@AlamatPerusahaan,@KodeKotaIns,@KodePosIns,@SpouseName,@MotherMaidenName, 
        @AhliWaris,@HubunganAhliWaris,@NatureOfBusiness,@NatureOfBusinessLainnya,@Politis,
        @PolitisLainnya,@PolitisName,@PolitisFT,@TeleponRumah,@OtherAlamatInd1,@OtherKodeKotaInd1,@OtherKodePosInd1,
        @OtherPropinsiInd1,@CountryOfBirth,@OtherNegaraInd1,@OtherAlamatInd2,@OtherKodeKotaInd2,@OtherKodePosInd2,
        @OtherPropinsiInd2,@OtherNegaraInd2,@OtherAlamatInd3,@OtherKodeKotaInd3,@OtherKodePosInd3,
        @OtherPropinsiInd3,@OtherNegaraInd3,@OtherTeleponRumah,@OtherTeleponSelular,@OtherEmail,
        @OtherFax,@IdentitasInd1,@NoIdentitasInd1,@RegistrationDateIdentitasInd1,
        @ExpiredDateIdentitasInd1,@IdentitasInd2,@NoIdentitasInd2,@RegistrationDateIdentitasInd2,@ExpiredDateIdentitasInd2,
        @IdentitasInd3,@NoIdentitasInd3,@RegistrationDateIdentitasInd3,@ExpiredDateIdentitasInd3,@IdentitasInd4,
        @NoIdentitasInd4,@RegistrationDateIdentitasInd4,@ExpiredDateIdentitasInd4,@RegistrationNPWP,
        @ExpiredDateSKD,@TanggalBerdiri,@LokasiBerdiri,@TeleponBisnis,@NomorAnggaran,
        @NomorSIUP,@AssetFor1Year,@AssetFor2Year,@AssetFor3Year,@OperatingProfitFor1Year,
        @OperatingProfitFor2Year,@OperatingProfitFor3Year,@NamaDepanIns1,@NamaTengahIns1,
        @NamaBelakangIns1,@Jabatan1,@IdentitasIns11,@NoIdentitasIns11,
        @RegistrationDateIdentitasIns11,@ExpiredDateIdentitasIns11,@IdentitasIns12,@NoIdentitasIns12,@RegistrationDateIdentitasIns12,
        @ExpiredDateIdentitasIns12,@IdentitasIns13,@NoIdentitasIns13,@RegistrationDateIdentitasIns13,@ExpiredDateIdentitasIns13,
        @IdentitasIns14,@NoIdentitasIns14,@RegistrationDateIdentitasIns14,@ExpiredDateIdentitasIns14,@NamaDepanIns2,
        @NamaTengahIns2,@NamaBelakangIns2,@Jabatan2,@IdentitasIns21,
        @NoIdentitasIns21,@RegistrationDateIdentitasIns21,@ExpiredDateIdentitasIns21,@IdentitasIns22,@NoIdentitasIns22,
        @RegistrationDateIdentitasIns22,@ExpiredDateIdentitasIns22,@IdentitasIns23,@NoIdentitasIns23,@RegistrationDateIdentitasIns23,
        @ExpiredDateIdentitasIns23,@IdentitasIns24,@NoIdentitasIns24,@RegistrationDateIdentitasIns24,@ExpiredDateIdentitasIns24,
        @NamaDepanIns3,@NamaTengahIns3,@NamaBelakangIns3,@Jabatan3,@JumlahIdentitasIns3,
        @IdentitasIns31,@NoIdentitasIns31,@RegistrationDateIdentitasIns31,@ExpiredDateIdentitasIns31,@IdentitasIns32,
        @NoIdentitasIns32,@RegistrationDateIdentitasIns32,@ExpiredDateIdentitasIns32,@IdentitasIns33,@NoIdentitasIns33,
        @RegistrationDateIdentitasIns33,@ExpiredDateIdentitasIns33,@IdentitasIns34,@NoIdentitasIns34,@RegistrationDateIdentitasIns34,
        @ExpiredDateIdentitasIns34,@NamaDepanIns4,@NamaTengahIns4,@NamaBelakangIns4,@Jabatan4,
        @JumlahIdentitasIns4,@IdentitasIns41,@NoIdentitasIns41,@RegistrationDateIdentitasIns41,@ExpiredDateIdentitasIns41,
        @IdentitasIns42,@NoIdentitasIns42,@RegistrationDateIdentitasIns42,@ExpiredDateIdentitasIns42,@IdentitasIns43,
        @NoIdentitasIns43,@RegistrationDateIdentitasIns43,@ExpiredDateIdentitasIns43,@IdentitasIns44,@NoIdentitasIns44,
        @RegistrationDateIdentitasIns44,@ExpiredDateIdentitasIns44,@PhoneIns1,@EmailIns1,  
        @PhoneIns2,@EmailIns2,@InvestorsRiskProfile,@AssetOwner,@StatementType,@FATCA,@TIN,@TINIssuanceCountry,@GIIN,@SubstantialOwnerName, 
        @SubstantialOwnerAddress,@SubstantialOwnerTIN,@BankBranchName1,@BankBranchName2,@BankBranchName3,@CountryofCorrespondence,@CountryofDomicile, 
        @SIUPExpirationDate,@CountryofEstablishment,@CompanyCityName,@CountryofCompany,@NPWPPerson1,@NPWPPerson2,@BankRDNPK,@RDNAccountNo,@RDNAccountName,@RiskProfileScore,@RDNBankBranchName,@RDNCurrency,
        @SpouseBirthPlace,@SpouseDateOfBirth,@SpouseOccupation,@SpouseNatureOfBusiness,@SpouseNatureOfBusinessOther,@SpouseIDNo,
        @SpouseNationality,@SpouseAnnualIncome,@IsFaceToFace,@KYCRiskProfile,@BitDefaultPayment1,@BitDefaultPayment2,@BitDefaultPayment3,@AlamatKantorInd,@KodeKotaKantorInd,@KodePosKantorInd,@KodePropinsiKantorInd,@KodeCountryofKantor,@CorrespondenceRT,@CorrespondenceRW,@DomicileRT,@DomicileRW,@Identity1RT,@Identity1RW,@KodeDomisiliPropinsi,
        @NamaKantor,@JabatanKantor,@OtherAgama,@OtherPendidikan,@OtherOccupation,@OtherSpouseOccupation,@OtherCurrency,@OtherSourceOfFunds,
        @OtherInvestmentObjectives,@OtherTipe,@OtherCharacteristic,@OtherInvestmentObjectivesIns,@OtherSourceOfFundsIns,
        @CompanyFax,@CompanyMail,@MigrationStatus, @SegmentClass,@CompanyTypeOJK, @Legality, @RenewingDate, @BitShareAbleToGroup,@RemarkBank1,@RemarkBank2,@RemarkBank3,@CantSubs,@CantRedempt,@CantSwitch,
        @BeneficialName,@BeneficialAddress,@BeneficialIdentity,@BeneficialWork,@BeneficialRelation,@BeneficialHomeNo,@BeneficialPhoneNumber,@BeneficialNPWP,@ClientOnBoard,@Referral,@BitisTA,
        @AlamatOfficer1,@AlamatOfficer2,@AlamatOfficer3,@AlamatOfficer4,@AgamaOfficer1,@AgamaOfficer2,@AgamaOfficer3,@AgamaOfficer4,@PlaceOfBirthOfficer1,@PlaceOfBirthOfficer2,@PlaceOfBirthOfficer3,@PlaceOfBirthOfficer4,@DOBOfficer1,@DOBOfficer2,@DOBOfficer3,@DOBOfficer4,@FaceToFaceDate,@EmployerLineOfBusiness,@FrontID,
        @TeleponKantor,@NationalityOfficer1,@NationalityOfficer2,@NationalityOfficer3,@NationalityOfficer4,
        @IdentityTypeOfficer1,@IdentityTypeOfficer2,@IdentityTypeOfficer3,@IdentityTypeOfficer4,@NoIdentitasOfficer1,@NoIdentitasOfficer2,@NoIdentitasOfficer3,@NoIdentitasOfficer4,@PolitisRelation,@CapitalPaidIn, ";




        public string FundClient_GenerateNewClientID(int _clientCategory, int _fundClientPK)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        //RCORE
                        cmd.CommandText = @" 							
                                        Declare @NewClientID  nvarchar(100)    
                                        Declare @MaxClientID  int
                                    
                                        select @MaxClientID =   max(convert(int,ID))  + 1 from FundClient where  status in (1,2) and id not like '%[a-zA-Z]%' and ID <> '' and len(ID)<=6
							            select @maxClientID = isnull(@MaxClientID,1)
							
							            declare @LENdigit int
							            select @LENdigit = LEN(@maxClientID) 
							
							            If @LENdigit = 1
							            BEGIN
								            set @NewClientID =  '00000' + CAST(@MaxClientID as nvarchar) 
                                        END
							            If @LENdigit = 2
							            BEGIN
								            set @NewClientID =  '0000' + CAST(@MaxClientID as nvarchar) 
                                        END
							            If @LENdigit = 3
							            BEGIN
								            set @NewClientID =  '000' + CAST(@MaxClientID as nvarchar) 
                                        END
							            If @LENdigit = 4
							            BEGIN
								            set @NewClientID =  '00' + CAST(@MaxClientID as nvarchar) 
                                        END
							            If @LENdigit = 5
							            BEGIN
								            set @NewClientID =  '0' + CAST(@MaxClientID as nvarchar) 
                                        END
                                        If @LENdigit = 6
                                        BEGIN
	                                        set @NewClientID =    CAST(@MaxClientID as nvarchar) 
                                        END

							            Select @NewClientID NewClientID
                       ";
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (!dr.Read())
                            {
                                return "";
                            }
                            else
                            {
                                return Convert.ToString(dr["NewClientID"]);
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

        private FundClient setFundClient(SqlDataReader dr)
        {
            FundClient M_FundClient = new FundClient();
            M_FundClient.FundClientPK = Convert.ToInt32(dr["FundClientPK"]);
            M_FundClient.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_FundClient.Status = Convert.ToInt32(dr["Status"]);
            M_FundClient.StatusDesc = dr["StatusDesc"].ToString();
            M_FundClient.Notes = dr["Notes"].ToString();
            M_FundClient.ID = dr["ID"].ToString();
            M_FundClient.OldID = dr["OldID"].ToString();
            M_FundClient.Name = dr["Name"].ToString();
            M_FundClient.ClientCategory = dr["ClientCategory"].ToString();
            M_FundClient.ClientCategoryDesc = dr["ClientCategoryDesc"].ToString();
            M_FundClient.InvestorType = dr["InvestorType"].ToString();
            M_FundClient.InvestorTypeDesc = dr["InvestorTypeDesc"].ToString();
            M_FundClient.InternalCategoryPK = Convert.ToInt32(dr["InternalCategoryPK"]);
            M_FundClient.RiskProfileScore = Convert.ToInt32(dr["RiskProfileScore"]);
            M_FundClient.RiskProfileScoreDesc = dr["RiskProfileScoreDesc"].ToString();
            M_FundClient.InternalCategoryID = dr["InternalCategoryID"].ToString();
            M_FundClient.SellingAgentPK = Convert.ToInt32(dr["SellingAgentPK"]);
            M_FundClient.SellingAgentID = dr["SellingAgentID"].ToString();
            M_FundClient.UsersPK = Convert.ToInt32(dr["UsersPK"]);
            M_FundClient.UsersID = dr["UsersID"].ToString();
            M_FundClient.ReferralIDFO = dr["ReferralIDFO"].ToString();
            M_FundClient.SID = dr["SID"].ToString();
            M_FundClient.IFUACode = dr["IFUACode"].ToString();
            //M_FundClient.Child = Convert.ToBoolean(dr["Child"]);
            M_FundClient.ARIA = Convert.ToBoolean(dr["ARIA"]);
            M_FundClient.Registered = Convert.ToBoolean(dr["Registered"]);
            //M_FundClient.JumlahDanaAwal = Convert.ToInt32(dr["JumlahDanaAwal"]);
            //M_FundClient.JumlahDanaSaatIniCash = Convert.ToInt32(dr["JumlahDanaSaatIniCash"]);
            //M_FundClient.JumlahDanaSaatIni = Convert.ToInt32(dr["JumlahDanaSaatIni"]);
            M_FundClient.Negara = dr["Negara"].ToString();
            M_FundClient.NegaraDesc = dr["NegaraDesc"].ToString();
            M_FundClient.Nationality = dr["Nationality"].ToString();
            M_FundClient.NationalityDesc = dr["NationalityDesc"].ToString();
            M_FundClient.NPWP = dr["NPWP"].ToString();
            M_FundClient.SACode = dr["SACode"].ToString();
            M_FundClient.Propinsi = Convert.ToInt32(dr["Propinsi"]);
            M_FundClient.PropinsiDesc = dr["PropinsiDesc"].ToString();
            M_FundClient.TeleponSelular = dr["TeleponSelular"].ToString();
            M_FundClient.Email = dr["Email"].ToString();
            M_FundClient.Fax = dr["Fax"].ToString();
            M_FundClient.DormantDate = dr["DormantDate"].ToString();
            M_FundClient.Description = dr["Description"].ToString();
            //M_FundClient.JumlahBank = Convert.ToInt32(dr["JumlahBank"]);
            M_FundClient.NamaBank1 = Convert.ToInt32(dr["NamaBank1"]);
            M_FundClient.NomorRekening1 = dr["NomorRekening1"].ToString();
            M_FundClient.BICCode1Name = dr["BICCode1Name"].ToString();
            //M_FundClient.BICCode1NameName = dr["BICCode1NameName"].ToString();
            M_FundClient.NamaNasabah1 = dr["NamaNasabah1"].ToString();
            M_FundClient.MataUang1 = dr["MataUang1"].ToString();
            M_FundClient.OtherCurrency = dr["OtherCurrency"].ToString();
            M_FundClient.NamaBank2 = Convert.ToInt32(dr["NamaBank2"]);
            M_FundClient.NomorRekening2 = dr["NomorRekening2"].ToString();
            //M_FundClient.BICCode2 = dr["BICCode2"].ToString();
            M_FundClient.BICCode2Name = dr["BICCode2Name"].ToString();
            M_FundClient.NamaNasabah2 = dr["NamaNasabah2"].ToString();
            M_FundClient.MataUang2 = dr["MataUang2"].ToString();
            M_FundClient.NamaBank3 = Convert.ToInt32(dr["NamaBank3"]);
            M_FundClient.NomorRekening3 = dr["NomorRekening3"].ToString();
            //M_FundClient.BICCode3 = dr["BICCode3"].ToString();
            M_FundClient.BICCode3Name = dr["BICCode3Name"].ToString();
            M_FundClient.NamaNasabah3 = dr["NamaNasabah3"].ToString();
            M_FundClient.MataUang3 = dr["MataUang3"].ToString();
            M_FundClient.IsFaceToFace = Convert.ToBoolean(dr["IsFaceToFace"]);
            M_FundClient.KYCRiskProfile = Convert.ToInt32(dr["KYCRiskProfile"]);
            M_FundClient.KYCRiskProfileDesc = dr["KYCRiskProfileDesc"].ToString();
            M_FundClient.DatePengkinianData = dr["DatePengkinianData"].ToString();

            M_FundClient.FrontID = dr["FrontID"].ToString();
            M_FundClient.FaceToFaceDate = dr["FaceToFaceDate"].ToString();
            M_FundClient.EmployerLineOfBusiness = dr["EmployerLineOfBusiness"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["EmployerLineOfBusiness"]);
            M_FundClient.EmployerLineOfBusinessDesc = dr["EmployerLineOfBusinessDesc"].ToString();

            //Individual
            M_FundClient.NamaDepanInd = dr["NamaDepanInd"].ToString();
            M_FundClient.NamaTengahInd = dr["NamaTengahInd"].ToString();
            M_FundClient.NamaBelakangInd = dr["NamaBelakangInd"].ToString();
            M_FundClient.TempatLahir = dr["TempatLahir"].ToString();
            M_FundClient.TanggalLahir = dr["TanggalLahir"].ToString();
            M_FundClient.JenisKelamin = Convert.ToInt32(dr["JenisKelamin"]);
            M_FundClient.JenisKelaminDesc = dr["JenisKelaminDesc"].ToString();
            M_FundClient.StatusPerkawinan = Convert.ToInt32(dr["StatusPerkawinan"]);
            M_FundClient.StatusPerkawinanDesc = dr["StatusPerkawinanDesc"].ToString();
            M_FundClient.Pekerjaan = Convert.ToInt32(dr["Pekerjaan"]);
            M_FundClient.PekerjaanDesc = dr["PekerjaanDesc"].ToString();
            M_FundClient.OtherOccupation = dr["OtherOccupation"].ToString();
            M_FundClient.OtherSpouseOccupation = dr["OtherSpouseOccupation"].ToString();
            M_FundClient.Pendidikan = Convert.ToInt32(dr["Pendidikan"]);
            M_FundClient.PendidikanDesc = dr["PendidikanDesc"].ToString();
            M_FundClient.OtherPendidikan = dr["OtherPendidikan"].ToString();
            M_FundClient.Agama = Convert.ToInt32(dr["Agama"]);
            M_FundClient.AgamaDesc = dr["AgamaDesc"].ToString();
            M_FundClient.OtherAgama = dr["OtherAgama"].ToString();
            M_FundClient.PenghasilanInd = Convert.ToInt32(dr["PenghasilanInd"]);
            M_FundClient.PenghasilanIndDesc = dr["PenghasilanIndDesc"].ToString();
            M_FundClient.SumberDanaInd = Convert.ToInt32(dr["SumberDanaInd"]);
            M_FundClient.SumberDanaIndDesc = dr["SumberDanaIndDesc"].ToString();
            M_FundClient.OtherSourceOfFunds = dr["OtherSourceOfFunds"].ToString();
            M_FundClient.CapitalPaidIn = Convert.ToInt32(dr["CapitalPaidIn"]);

            if (_host.CheckColumnIsExist(dr, "ComplRequired"))
            {
                M_FundClient.ComplRequired = dr["ComplRequired"].ToString();
            }
            //M_FundClient.ComplRequired = Convert.ToBoolean(dr["ComplRequired"]);

            M_FundClient.MaksudTujuanInd = Convert.ToInt32(dr["MaksudTujuanInd"]);
            M_FundClient.MaksudTujuanIndDesc = dr["MaksudTujuanIndDesc"].ToString();
            M_FundClient.OtherInvestmentObjectives = dr["OtherInvestmentObjectives"].ToString();
            M_FundClient.AlamatInd1 = dr["AlamatInd1"].ToString();
            M_FundClient.KodeKotaInd1 = dr["KodeKotaInd1"].ToString();
            M_FundClient.KodeKotaInd1Desc = dr["KodeKotaInd1Desc"].ToString();
            M_FundClient.KodePosInd1 = Convert.ToInt32(dr["KodePosInd1"]);
            M_FundClient.AlamatInd2 = dr["AlamatInd2"].ToString();
            M_FundClient.KodeKotaInd2 = dr["KodeKotaInd2"].ToString();
            M_FundClient.KodeKotaInd2Desc = dr["KodeKotaInd2Desc"].ToString();
            M_FundClient.KodePosInd2 = Convert.ToInt32(dr["KodePosInd2"]);
            M_FundClient.CountryOfBirth = dr["CountryOfBirth"].ToString();
            M_FundClient.CountryOfBirthDesc = dr["CountryOfBirthDesc"].ToString();

            //Institution
            M_FundClient.NamaPerusahaan = dr["NamaPerusahaan"].ToString();
            M_FundClient.Domisili = Convert.ToInt32(dr["Domisili"]);
            M_FundClient.DomisiliDesc = dr["DomisiliDesc"].ToString();
            M_FundClient.Tipe = Convert.ToInt32(dr["Tipe"]);
            M_FundClient.TipeDesc = dr["TipeDesc"].ToString();
            M_FundClient.OtherTipe = dr["OtherTipe"].ToString();
            M_FundClient.Karakteristik = Convert.ToInt32(dr["Karakteristik"]);
            M_FundClient.KarakteristikDesc = dr["KarakteristikDesc"].ToString();
            M_FundClient.OtherCharacteristic = dr["OtherCharacteristic"].ToString();
            M_FundClient.NoSKD = dr["NoSKD"].ToString();
            M_FundClient.PenghasilanInstitusi = Convert.ToInt32(dr["PenghasilanInstitusi"]);
            M_FundClient.PenghasilanInstitusiDesc = dr["PenghasilanInstitusiDesc"].ToString();
            M_FundClient.SumberDanaInstitusi = Convert.ToInt32(dr["SumberDanaInstitusi"]);
            M_FundClient.SumberDanaInstitusiDesc = dr["SumberDanaInstitusiDesc"].ToString();
            M_FundClient.OtherSourceOfFundsIns = dr["OtherSourceOfFundsIns"].ToString();
            M_FundClient.MaksudTujuanInstitusi = Convert.ToInt32(dr["MaksudTujuanInstitusi"]);
            M_FundClient.MaksudTujuanInstitusiDesc = dr["MaksudTujuanInstitusiDesc"].ToString();
            M_FundClient.OtherInvestmentObjectivesIns = dr["OtherInvestmentObjectivesIns"].ToString();
            M_FundClient.OtherInvestmentObjectives = dr["OtherInvestmentObjectives"].ToString();
            M_FundClient.AlamatPerusahaan = dr["AlamatPerusahaan"].ToString();
            M_FundClient.KodeKotaIns = dr["KodeKotaIns"].ToString();
            M_FundClient.KodeKotaInsDesc = dr["KodeKotaInsDesc"].ToString();
            M_FundClient.KodePosIns = Convert.ToInt32(dr["KodePosIns"]);
            M_FundClient.SpouseName = dr["SpouseName"].ToString();
            M_FundClient.MotherMaidenName = dr["MotherMaidenName"].ToString();
            M_FundClient.AhliWaris = dr["AhliWaris"].ToString();
            M_FundClient.HubunganAhliWaris = dr["HubunganAhliWaris"].ToString();
            M_FundClient.NatureOfBusiness = Convert.ToInt32(dr["NatureOfBusiness"]);
            M_FundClient.NatureOfBusinessLainnya = dr["NatureOfBusinessLainnya"].ToString();
            M_FundClient.Politis = Convert.ToInt32(dr["Politis"]);
            M_FundClient.PolitisRelation = dr["PolitisRelation"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["PolitisRelation"]);
            M_FundClient.PolitisLainnya = dr["PolitisLainnya"].ToString();
            M_FundClient.PolitisName = dr["PolitisName"].ToString();
            M_FundClient.PolitisFT = dr["PolitisFT"].ToString();
            M_FundClient.TeleponRumah = dr["TeleponRumah"].ToString();
            M_FundClient.OtherAlamatInd1 = dr["OtherAlamatInd1"].ToString();
            M_FundClient.OtherKodeKotaInd1 = dr["OtherKodeKotaInd1"].ToString();
            M_FundClient.OtherKodeKotaInd1Desc = dr["OtherKodeKotaInd1Desc"].ToString();
            M_FundClient.OtherKodePosInd1 = Convert.ToInt32(dr["OtherKodePosInd1"]);
            M_FundClient.OtherPropinsiInd1 = Convert.ToInt32(dr["OtherPropinsiInd1"]);
            M_FundClient.OtherPropinsiInd1Desc = dr["OtherPropinsiInd1Desc"].ToString();
            M_FundClient.OtherNegaraInd1 = dr["OtherNegaraInd1"].ToString();
            M_FundClient.OtherNegaraInd1Desc = dr["OtherNegaraInd1Desc"].ToString();
            M_FundClient.OtherAlamatInd2 = dr["OtherAlamatInd2"].ToString();
            M_FundClient.OtherKodeKotaInd2 = dr["OtherKodeKotaInd2"].ToString();
            M_FundClient.OtherKodeKotaInd2Desc = dr["OtherKodeKotaInd2Desc"].ToString();
            M_FundClient.OtherKodePosInd2 = Convert.ToInt32(dr["OtherKodePosInd2"]);
            M_FundClient.OtherPropinsiInd2 = Convert.ToInt32(dr["OtherPropinsiInd2"]);
            M_FundClient.OtherPropinsiInd2Desc = dr["OtherPropinsiInd2Desc"].ToString();
            M_FundClient.OtherNegaraInd2 = dr["OtherNegaraInd2"].ToString();
            M_FundClient.OtherNegaraInd2Desc = dr["OtherNegaraInd2Desc"].ToString();
            M_FundClient.OtherAlamatInd3 = dr["OtherAlamatInd3"].ToString();
            M_FundClient.OtherKodeKotaInd3 = dr["OtherKodeKotaInd3"].ToString();
            M_FundClient.OtherKodeKotaInd3Desc = dr["OtherKodeKotaInd3Desc"].ToString();
            M_FundClient.OtherKodePosInd3 = Convert.ToInt32(dr["OtherKodePosInd3"]);
            M_FundClient.OtherPropinsiInd3 = Convert.ToInt32(dr["OtherPropinsiInd3"]);
            M_FundClient.OtherPropinsiInd3Desc = dr["OtherPropinsiInd3Desc"].ToString();
            M_FundClient.OtherNegaraInd3 = dr["OtherNegaraInd3"].ToString();
            M_FundClient.OtherNegaraInd3Desc = dr["OtherNegaraInd3Desc"].ToString();
            M_FundClient.OtherTeleponRumah = dr["OtherTeleponRumah"].ToString();
            M_FundClient.OtherTeleponSelular = dr["OtherTeleponSelular"].ToString();
            M_FundClient.OtherEmail = dr["OtherEmail"].ToString();
            M_FundClient.OtherFax = dr["OtherFax"].ToString();
            //M_FundClient.JumlahIdentitasInd = Convert.ToInt32(dr["JumlahIdentitasInd"]);
            M_FundClient.IdentitasInd1 = Convert.ToInt32(dr["IdentitasInd1"]);
            M_FundClient.NoIdentitasInd1 = dr["NoIdentitasInd1"].ToString();
            M_FundClient.RegistrationDateIdentitasInd1 = dr["RegistrationDateIdentitasInd1"].ToString();
            M_FundClient.ExpiredDateIdentitasInd1 = dr["ExpiredDateIdentitasInd1"].ToString();
            M_FundClient.IdentitasInd2 = Convert.ToInt32(dr["IdentitasInd2"]);
            M_FundClient.NoIdentitasInd2 = dr["NoIdentitasInd2"].ToString();
            M_FundClient.RegistrationDateIdentitasInd2 = dr["RegistrationDateIdentitasInd2"].ToString();
            M_FundClient.ExpiredDateIdentitasInd2 = dr["ExpiredDateIdentitasInd2"].ToString();
            M_FundClient.IdentitasInd3 = Convert.ToInt32(dr["IdentitasInd3"]);
            M_FundClient.NoIdentitasInd3 = dr["NoIdentitasInd3"].ToString();
            M_FundClient.RegistrationDateIdentitasInd3 = dr["RegistrationDateIdentitasInd3"].ToString();
            M_FundClient.ExpiredDateIdentitasInd3 = dr["ExpiredDateIdentitasInd3"].ToString();
            M_FundClient.IdentitasInd4 = Convert.ToInt32(dr["IdentitasInd4"]);
            M_FundClient.NoIdentitasInd4 = dr["NoIdentitasInd4"].ToString();
            M_FundClient.RegistrationDateIdentitasInd4 = dr["RegistrationDateIdentitasInd4"].ToString();
            M_FundClient.ExpiredDateIdentitasInd4 = dr["ExpiredDateIdentitasInd4"].ToString();
            M_FundClient.RegistrationNPWP = dr["RegistrationNPWP"].ToString();
            M_FundClient.ExpiredDateSKD = dr["ExpiredDateSKD"].ToString();
            M_FundClient.TanggalBerdiri = dr["TanggalBerdiri"].ToString();
            M_FundClient.LokasiBerdiri = dr["LokasiBerdiri"].ToString();
            M_FundClient.TeleponBisnis = dr["TeleponBisnis"].ToString();
            M_FundClient.NomorAnggaran = dr["NomorAnggaran"].ToString();
            M_FundClient.NomorSIUP = dr["NomorSIUP"].ToString();
            M_FundClient.AssetFor1Year = dr["AssetFor1Year"].ToString();
            M_FundClient.AssetFor2Year = dr["AssetFor2Year"].ToString();
            M_FundClient.AssetFor3Year = dr["AssetFor3Year"].ToString();
            M_FundClient.OperatingProfitFor1Year = dr["OperatingProfitFor1Year"].ToString();
            M_FundClient.OperatingProfitFor2Year = dr["OperatingProfitFor2Year"].ToString();
            M_FundClient.OperatingProfitFor3Year = dr["OperatingProfitFor3Year"].ToString();
            //M_FundClient.JumlahPejabat = Convert.ToInt32(dr["JumlahPejabat"]);
            M_FundClient.NamaDepanIns1 = dr["NamaDepanIns1"].ToString();
            M_FundClient.NamaTengahIns1 = dr["NamaTengahIns1"].ToString();
            M_FundClient.NamaBelakangIns1 = dr["NamaBelakangIns1"].ToString();
            M_FundClient.Jabatan1 = dr["Jabatan1"].ToString();
            //M_FundClient.JumlahIdentitasIns1 = Convert.ToInt32(dr["JumlahIdentitasIns1"]);
            M_FundClient.IdentitasIns11 = Convert.ToInt32(dr["IdentitasIns11"]);
            M_FundClient.NoIdentitasIns11 = dr["NoIdentitasIns11"].ToString();
            M_FundClient.RegistrationDateIdentitasIns11 = dr["RegistrationDateIdentitasIns11"].ToString();
            M_FundClient.ExpiredDateIdentitasIns11 = dr["ExpiredDateIdentitasIns11"].ToString();
            M_FundClient.IdentitasIns12 = Convert.ToInt32(dr["IdentitasIns12"]);
            M_FundClient.NoIdentitasIns12 = dr["NoIdentitasIns12"].ToString();
            M_FundClient.RegistrationDateIdentitasIns12 = dr["RegistrationDateIdentitasIns12"].ToString();
            M_FundClient.ExpiredDateIdentitasIns12 = dr["ExpiredDateIdentitasIns12"].ToString();
            M_FundClient.IdentitasIns13 = Convert.ToInt32(dr["IdentitasIns13"]);
            M_FundClient.NoIdentitasIns13 = dr["NoIdentitasIns13"].ToString();
            M_FundClient.RegistrationDateIdentitasIns13 = dr["RegistrationDateIdentitasIns13"].ToString();
            M_FundClient.ExpiredDateIdentitasIns13 = dr["ExpiredDateIdentitasIns13"].ToString();
            M_FundClient.IdentitasIns14 = Convert.ToInt32(dr["IdentitasIns14"]);
            M_FundClient.NoIdentitasIns14 = dr["NoIdentitasIns14"].ToString();
            M_FundClient.RegistrationDateIdentitasIns14 = dr["RegistrationDateIdentitasIns14"].ToString();
            M_FundClient.ExpiredDateIdentitasIns14 = dr["ExpiredDateIdentitasIns14"].ToString();
            M_FundClient.NamaDepanIns2 = dr["NamaDepanIns2"].ToString();
            M_FundClient.NamaTengahIns2 = dr["NamaTengahIns2"].ToString();
            M_FundClient.NamaBelakangIns2 = dr["NamaBelakangIns2"].ToString();
            M_FundClient.Jabatan2 = dr["Jabatan2"].ToString();
            //M_FundClient.JumlahIdentitasIns2 = Convert.ToInt32(dr["JumlahIdentitasIns2"]);
            M_FundClient.IdentitasIns21 = Convert.ToInt32(dr["IdentitasIns21"]);
            M_FundClient.NoIdentitasIns21 = dr["NoIdentitasIns21"].ToString();
            M_FundClient.RegistrationDateIdentitasIns21 = dr["RegistrationDateIdentitasIns21"].ToString();
            M_FundClient.ExpiredDateIdentitasIns21 = dr["ExpiredDateIdentitasIns21"].ToString();
            M_FundClient.IdentitasIns22 = Convert.ToInt32(dr["IdentitasIns22"]);
            M_FundClient.NoIdentitasIns22 = dr["NoIdentitasIns22"].ToString();
            M_FundClient.RegistrationDateIdentitasIns22 = dr["RegistrationDateIdentitasIns22"].ToString();
            M_FundClient.ExpiredDateIdentitasIns22 = dr["ExpiredDateIdentitasIns22"].ToString();
            M_FundClient.IdentitasIns23 = Convert.ToInt32(dr["IdentitasIns23"]);
            M_FundClient.NoIdentitasIns23 = dr["NoIdentitasIns23"].ToString();
            M_FundClient.RegistrationDateIdentitasIns23 = dr["RegistrationDateIdentitasIns23"].ToString();
            M_FundClient.ExpiredDateIdentitasIns23 = dr["ExpiredDateIdentitasIns23"].ToString();
            M_FundClient.IdentitasIns24 = Convert.ToInt32(dr["IdentitasIns24"]);
            M_FundClient.NoIdentitasIns24 = dr["NoIdentitasIns24"].ToString();
            M_FundClient.RegistrationDateIdentitasIns24 = dr["RegistrationDateIdentitasIns24"].ToString();
            M_FundClient.ExpiredDateIdentitasIns24 = dr["ExpiredDateIdentitasIns24"].ToString();
            M_FundClient.NamaDepanIns3 = dr["NamaDepanIns3"].ToString();
            M_FundClient.NamaTengahIns3 = dr["NamaTengahIns3"].ToString();
            M_FundClient.NamaBelakangIns3 = dr["NamaBelakangIns3"].ToString();
            M_FundClient.Jabatan3 = dr["Jabatan3"].ToString();
            M_FundClient.JumlahIdentitasIns3 = Convert.ToInt32(dr["JumlahIdentitasIns3"]);
            M_FundClient.IdentitasIns31 = Convert.ToInt32(dr["IdentitasIns31"]);
            M_FundClient.NoIdentitasIns31 = dr["NoIdentitasIns31"].ToString();
            M_FundClient.RegistrationDateIdentitasIns31 = dr["RegistrationDateIdentitasIns31"].ToString();
            M_FundClient.ExpiredDateIdentitasIns31 = dr["ExpiredDateIdentitasIns31"].ToString();
            M_FundClient.IdentitasIns32 = Convert.ToInt32(dr["IdentitasIns32"]);
            M_FundClient.NoIdentitasIns32 = dr["NoIdentitasIns32"].ToString();
            M_FundClient.RegistrationDateIdentitasIns32 = dr["RegistrationDateIdentitasIns32"].ToString();
            M_FundClient.ExpiredDateIdentitasIns32 = dr["ExpiredDateIdentitasIns32"].ToString();
            M_FundClient.IdentitasIns33 = Convert.ToInt32(dr["IdentitasIns33"]);
            M_FundClient.NoIdentitasIns33 = dr["NoIdentitasIns33"].ToString();
            M_FundClient.RegistrationDateIdentitasIns33 = dr["RegistrationDateIdentitasIns33"].ToString();
            M_FundClient.ExpiredDateIdentitasIns33 = dr["ExpiredDateIdentitasIns33"].ToString();
            M_FundClient.IdentitasIns34 = Convert.ToInt32(dr["IdentitasIns34"]);
            M_FundClient.NoIdentitasIns34 = dr["NoIdentitasIns34"].ToString();
            M_FundClient.RegistrationDateIdentitasIns34 = dr["RegistrationDateIdentitasIns34"].ToString();
            M_FundClient.ExpiredDateIdentitasIns34 = dr["ExpiredDateIdentitasIns34"].ToString();
            M_FundClient.NamaDepanIns4 = dr["NamaDepanIns4"].ToString();
            M_FundClient.NamaTengahIns4 = dr["NamaTengahIns4"].ToString();
            M_FundClient.NamaBelakangIns4 = dr["NamaBelakangIns4"].ToString();
            M_FundClient.Jabatan4 = dr["Jabatan4"].ToString();
            M_FundClient.JumlahIdentitasIns4 = Convert.ToInt32(dr["JumlahIdentitasIns4"]);
            M_FundClient.IdentitasIns41 = Convert.ToInt32(dr["IdentitasIns41"]);
            M_FundClient.NoIdentitasIns41 = dr["NoIdentitasIns41"].ToString();
            M_FundClient.RegistrationDateIdentitasIns41 = dr["RegistrationDateIdentitasIns41"].ToString();
            M_FundClient.ExpiredDateIdentitasIns41 = dr["ExpiredDateIdentitasIns41"].ToString();
            M_FundClient.IdentitasIns42 = Convert.ToInt32(dr["IdentitasIns42"]);
            M_FundClient.NoIdentitasIns42 = dr["NoIdentitasIns42"].ToString();
            M_FundClient.RegistrationDateIdentitasIns42 = dr["RegistrationDateIdentitasIns42"].ToString();
            M_FundClient.ExpiredDateIdentitasIns42 = dr["ExpiredDateIdentitasIns42"].ToString();
            M_FundClient.IdentitasIns43 = Convert.ToInt32(dr["IdentitasIns43"]);
            M_FundClient.NoIdentitasIns43 = dr["NoIdentitasIns43"].ToString();
            M_FundClient.RegistrationDateIdentitasIns43 = dr["RegistrationDateIdentitasIns43"].ToString();
            M_FundClient.ExpiredDateIdentitasIns43 = dr["ExpiredDateIdentitasIns43"].ToString();
            M_FundClient.IdentitasIns44 = Convert.ToInt32(dr["IdentitasIns44"]);
            M_FundClient.NoIdentitasIns44 = dr["NoIdentitasIns44"].ToString();
            M_FundClient.RegistrationDateIdentitasIns44 = dr["RegistrationDateIdentitasIns44"].ToString();
            M_FundClient.ExpiredDateIdentitasIns44 = dr["ExpiredDateIdentitasIns44"].ToString();

            M_FundClient.AlamatOfficer1 = dr["AlamatOfficer1"].ToString();
            M_FundClient.AlamatOfficer2 = dr["AlamatOfficer2"].ToString();
            M_FundClient.AlamatOfficer3 = dr["AlamatOfficer3"].ToString();
            M_FundClient.AlamatOfficer4 = dr["AlamatOfficer4"].ToString();
            M_FundClient.AgamaOfficer1 = dr["AgamaOfficer1"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["AgamaOfficer1"]);
            M_FundClient.AgamaOfficer1Desc = dr["AgamaOfficer1Desc"].ToString();
            M_FundClient.AgamaOfficer2 = dr["AgamaOfficer2"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["AgamaOfficer2"]);
            M_FundClient.AgamaOfficer2Desc = dr["AgamaOfficer2Desc"].ToString();
            M_FundClient.AgamaOfficer3 = dr["AgamaOfficer3"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["AgamaOfficer3"]);
            M_FundClient.AgamaOfficer3Desc = dr["AgamaOfficer3Desc"].ToString();
            M_FundClient.AgamaOfficer4 = dr["AgamaOfficer4"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["AgamaOfficer4"]);
            M_FundClient.AgamaOfficer4Desc = dr["AgamaOfficer4Desc"].ToString();
            M_FundClient.PlaceOfBirthOfficer1 = dr["PlaceOfBirthOfficer1"].ToString();
            M_FundClient.PlaceOfBirthOfficer2 = dr["PlaceOfBirthOfficer2"].ToString();
            M_FundClient.PlaceOfBirthOfficer3 = dr["PlaceOfBirthOfficer3"].ToString();
            M_FundClient.PlaceOfBirthOfficer4 = dr["PlaceOfBirthOfficer4"].ToString();
            M_FundClient.DOBOfficer1 = dr["DOBOfficer1"].ToString();
            M_FundClient.DOBOfficer2 = dr["DOBOfficer2"].ToString();
            M_FundClient.DOBOfficer3 = dr["DOBOfficer3"].ToString();
            M_FundClient.DOBOfficer4 = dr["DOBOfficer4"].ToString();

            // S-INVEST
            //M_FundClient.BIMemberCode1 = dr["BIMemberCode1"].ToString();
            //M_FundClient.BIMemberCode2 = dr["BIMemberCode2"].ToString();
            //M_FundClient.BIMemberCode3 = dr["BIMemberCode3"].ToString();
            M_FundClient.PhoneIns1 = dr["PhoneIns1"].ToString();
            M_FundClient.EmailIns1 = dr["EmailIns1"].ToString();
            M_FundClient.PhoneIns2 = dr["PhoneIns2"].ToString();
            M_FundClient.EmailIns2 = dr["EmailIns2"].ToString();
            M_FundClient.InvestorsRiskProfile = Convert.ToInt32(dr["InvestorsRiskProfile"]);
            M_FundClient.InvestorsRiskProfileDesc = dr["InvestorsRiskProfileDesc"].ToString();
            M_FundClient.AssetOwner = Convert.ToInt32(dr["AssetOwner"]);
            M_FundClient.AssetOwnerDesc = dr["AssetOwnerDesc"].ToString();
            M_FundClient.StatementType = Convert.ToInt32(dr["StatementType"]);
            M_FundClient.StatementTypeDesc = dr["StatementTypeDesc"].ToString();
            M_FundClient.FATCA = Convert.ToInt32(dr["FATCA"]);
            M_FundClient.FATCADesc = dr["FATCADesc"].ToString();
            M_FundClient.TIN = dr["TIN"].ToString();
            M_FundClient.TINIssuanceCountry = dr["TINIssuanceCountry"].ToString();
            M_FundClient.TINIssuanceCountryDesc = dr["TINIssuanceCountryDesc"].ToString();
            M_FundClient.GIIN = dr["GIIN"].ToString();
            M_FundClient.SubstantialOwnerName = dr["SubstantialOwnerName"].ToString();
            M_FundClient.SubstantialOwnerAddress = dr["SubstantialOwnerAddress"].ToString();
            M_FundClient.SubstantialOwnerTIN = dr["SubstantialOwnerTIN"].ToString();
            M_FundClient.BankBranchName1 = dr["BankBranchName1"].ToString();
            M_FundClient.BankBranchName2 = dr["BankBranchName2"].ToString();
            M_FundClient.BankBranchName3 = dr["BankBranchName3"].ToString();
            //M_FundClient.BankCountry1 = dr["BankCountry1"].ToString();
            //M_FundClient.BankCountry1Desc = dr["BankCountry1Desc"].ToString();
            //M_FundClient.BankCountry2 = dr["BankCountry2"].ToString();
            //M_FundClient.BankCountry2Desc = dr["BankCountry2Desc"].ToString();
            //M_FundClient.BankCountry3 = dr["BankCountry3"].ToString();
            //M_FundClient.BankCountry3Desc = dr["BankCountry3Desc"].ToString();

            // new add on
            M_FundClient.CountryofCorrespondence = dr["CountryofCorrespondence"].ToString();
            M_FundClient.CountryofCorrespondenceDesc = dr["CountryofCorrespondenceDesc"].ToString();
            M_FundClient.CountryofDomicile = dr["CountryofDomicile"].ToString();
            M_FundClient.CountryofDomicileDesc = dr["CountryofDomicileDesc"].ToString();
            M_FundClient.SIUPExpirationDate = dr["SIUPExpirationDate"].Equals(DBNull.Value) == true ? "" : dr["SIUPExpirationDate"].ToString();
            M_FundClient.CountryofEstablishment = dr["CountryofEstablishment"].ToString();
            M_FundClient.CountryofEstablishmentDesc = dr["CountryofEstablishmentDesc"].Equals(DBNull.Value) == true ? "" : dr["CountryofEstablishmentDesc"].ToString();
            M_FundClient.CompanyCityName = dr["CompanyCityName"].Equals(DBNull.Value) == true ? "" : dr["CompanyCityName"].ToString();
            M_FundClient.CompanyCityNameDesc = dr["CompanyCityNameDesc"].Equals(DBNull.Value) == true ? "" : dr["CompanyCityNameDesc"].ToString();
            M_FundClient.CountryofCompany = dr["CountryofCompany"].ToString();
            M_FundClient.CountryofCompanyDesc = dr["CountryofCompanyDesc"].Equals(DBNull.Value) == true ? "" : dr["CountryofCompanyDesc"].ToString();
            M_FundClient.NPWPPerson1 = dr["NPWPPerson1"].Equals(DBNull.Value) == true ? "" : dr["NPWPPerson1"].ToString();
            M_FundClient.NPWPPerson2 = dr["NPWPPerson2"].Equals(DBNull.Value) == true ? "" : dr["NPWPPerson2"].ToString();
            M_FundClient.BitIsSuspend = Convert.ToBoolean(dr["BitIsSuspend"]);
            M_FundClient.BitDefaultPayment1 = Convert.ToBoolean(dr["BitDefaultPayment1"]);
            M_FundClient.BitDefaultPayment2 = Convert.ToBoolean(dr["BitDefaultPayment2"]);
            M_FundClient.BitDefaultPayment3 = Convert.ToBoolean(dr["BitDefaultPayment3"]);

            M_FundClient.AlamatKantorInd = dr["AlamatKantorInd"].ToString();
            M_FundClient.KodeKotaKantorInd = Convert.ToInt32(dr["KodeKotaKantorInd"]);
            M_FundClient.KodeKotaKantorIndDesc = dr["KodeKotaKantorIndDesc"].ToString();
            M_FundClient.KodePosKantorInd = Convert.ToInt32(dr["KodePosKantorInd"]);

            M_FundClient.KodePropinsiKantorInd = Convert.ToInt32(dr["KodePropinsiKantorInd"]);
            M_FundClient.KodePropinsiKantorIndDesc = dr["KodePropinsiKantorIndDesc"].ToString();
            M_FundClient.KodeCountryofKantor = dr["KodeCountryofKantor"].ToString();
            M_FundClient.KodeCountryofKantorDesc = dr["KodeCountryofKantorDesc"].ToString();
            M_FundClient.CorrespondenceRT = dr["CorrespondenceRT"].ToString();
            M_FundClient.CorrespondenceRW = dr["CorrespondenceRW"].ToString();
            M_FundClient.DomicileRT = dr["DomicileRT"].ToString();
            M_FundClient.DomicileRW = dr["DomicileRW"].ToString();
            M_FundClient.Identity1RT = dr["Identity1RT"].ToString();
            M_FundClient.Identity1RW = dr["Identity1RW"].ToString();
            M_FundClient.KodeDomisiliPropinsi = Convert.ToInt32(dr["KodeDomisiliPropinsi"]);
            M_FundClient.KodeDomisiliPropinsiDesc = dr["KodeDomisiliPropinsiDesc"].ToString();

            M_FundClient.NamaKantor = dr["NamaKantor"].ToString();
            M_FundClient.JabatanKantor = dr["JabatanKantor"].ToString();

            // RDN
            M_FundClient.BankRDNPK = Convert.ToInt32(dr["BankRDNPK"]);
            M_FundClient.RDNAccountNo = dr["RDNAccountNo"].ToString();
            M_FundClient.RDNAccountName = dr["RDNAccountName"].ToString();
            M_FundClient.RDNBankBranchName = dr["RDNBankBranchName"].ToString();
            M_FundClient.RDNCurrency = dr["RDNCurrency"].ToString();

            //SPOUSE
            M_FundClient.SpouseBirthPlace = dr["SpouseBirthPlace"].ToString();
            M_FundClient.SpouseDateOfBirth = dr["SpouseDateOfBirth"].ToString();
            M_FundClient.SpouseOccupation = Convert.ToInt32(dr["SpouseOccupation"]);
            M_FundClient.SpouseOccupationDesc = dr["SpouseOccupationDesc"].ToString();
            M_FundClient.OtherSpouseOccupation = dr["OtherSpouseOccupation"].ToString();
            M_FundClient.SpouseNatureOfBusiness = Convert.ToInt32(dr["SpouseNatureOfBusiness"]);
            M_FundClient.SpouseNatureOfBusinessDesc = dr["SpouseNatureOfBusinessDesc"].ToString();
            M_FundClient.SpouseNatureOfBusinessOther = dr["SpouseNatureOfBusinessOther"].ToString();
            M_FundClient.SpouseIDNo = dr["SpouseIDNo"].ToString();
            M_FundClient.SpouseNationality = dr["SpouseNationality"].ToString();
            M_FundClient.SpouseNationalityDesc = dr["SpouseNationalityDesc"].ToString();
            M_FundClient.SpouseAnnualIncome = dr["SpouseAnnualIncome"].ToString();

            M_FundClient.CompanyFax = dr["CompanyFax"].ToString();
            M_FundClient.CompanyMail = dr["CompanyMail"].ToString();
            M_FundClient.SegmentClass = dr["SegmentClass"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["SegmentClass"]);
            M_FundClient.MigrationStatus = dr["MigrationStatus"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["MigrationStatus"]);
            M_FundClient.CompanyTypeOJK = dr["CompanyTypeOJK"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["CompanyTypeOJK"]);
            M_FundClient.Legality = dr["Legality"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["Legality"]);
            M_FundClient.RenewingDate = dr["RenewingDate"].Equals(DBNull.Value) == true ? "" : dr["RenewingDate"].ToString();
            M_FundClient.BitShareAbleToGroup = dr["BitShareAbleToGroup"].Equals(DBNull.Value) == true ? false : Convert.ToBoolean(dr["BitShareAbleToGroup"]);
            M_FundClient.RemarkBank1 = dr["RemarkBank1"].Equals(DBNull.Value) == true ? "" : dr["RemarkBank1"].ToString();
            M_FundClient.RemarkBank2 = dr["RemarkBank2"].Equals(DBNull.Value) == true ? "" : dr["RemarkBank2"].ToString();
            M_FundClient.RemarkBank3 = dr["RemarkBank3"].Equals(DBNull.Value) == true ? "" : dr["RemarkBank3"].ToString();

            M_FundClient.CantSubs = dr["CantSubs"].Equals(DBNull.Value) == true ? false : Convert.ToBoolean(dr["CantSubs"]);
            M_FundClient.CantRedempt = dr["CantRedempt"].Equals(DBNull.Value) == true ? false : Convert.ToBoolean(dr["CantRedempt"]);
            M_FundClient.CantSwitch = dr["CantSwitch"].Equals(DBNull.Value) == true ? false : Convert.ToBoolean(dr["CantSwitch"]);

            M_FundClient.BeneficialName = dr["BeneficialName"].Equals(DBNull.Value) == true ? "" : dr["BeneficialName"].ToString();
            M_FundClient.BeneficialAddress = dr["BeneficialAddress"].Equals(DBNull.Value) == true ? "" : dr["BeneficialAddress"].ToString();
            M_FundClient.BeneficialIdentity = dr["BeneficialIdentity"].Equals(DBNull.Value) == true ? "" : dr["BeneficialIdentity"].ToString();
            M_FundClient.BeneficialWork = dr["BeneficialWork"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["BeneficialWork"]);
            M_FundClient.BeneficialRelation = dr["BeneficialRelation"].Equals(DBNull.Value) == true ? "" : dr["BeneficialRelation"].ToString();
            M_FundClient.BeneficialHomeNo = dr["BeneficialHomeNo"].Equals(DBNull.Value) == true ? "" : dr["BeneficialHomeNo"].ToString();
            M_FundClient.BeneficialPhoneNumber = dr["BeneficialPhoneNumber"].Equals(DBNull.Value) == true ? "" : dr["BeneficialPhoneNumber"].ToString();
            M_FundClient.BeneficialNPWP = dr["BeneficialNPWP"].Equals(DBNull.Value) == true ? "" : dr["BeneficialNPWP"].ToString();
            M_FundClient.ClientOnBoard = dr["ClientOnBoard"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["ClientOnBoard"]);
            M_FundClient.ClientOnBoardDesc = dr["ClientOnBoardDesc"].Equals(DBNull.Value) == true ? "" : dr["ClientOnBoardDesc"].ToString();
            M_FundClient.Referral = dr["Referral"].Equals(DBNull.Value) == true ? "" : dr["Referral"].ToString();

            M_FundClient.EntryUsersID = dr["EntryUsersID"].ToString();
            M_FundClient.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_FundClient.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_FundClient.VoidUsersID = dr["VoidUsersID"].ToString();
            M_FundClient.EntryTime = dr["EntryTime"].ToString();
            M_FundClient.UpdateTime = dr["UpdateTime"].ToString();
            M_FundClient.ApprovedTime = dr["ApprovedTime"].ToString();
            M_FundClient.VoidTime = dr["VoidTime"].ToString();
            M_FundClient.DBUserID = dr["DBUserID"].ToString();
            M_FundClient.DBTerminalID = dr["DBTerminalID"].ToString();
            M_FundClient.LastUpdate = dr["LastUpdate"].ToString();
            M_FundClient.LastUpdateDB = dr["LastUpdateDB"].Equals(DBNull.Value) == true ? "" : dr["LastUpdateDB"].ToString();
            M_FundClient.SuspendBy = dr["SuspendBy"].ToString();
            M_FundClient.SuspendTime = dr["SuspendTime"].ToString();
            M_FundClient.UnSuspendBy = dr["UnSuspendBy"].ToString();
            M_FundClient.UnSuspendTime = dr["UnSuspendTIme"].ToString();
            M_FundClient.BitIsAfiliated = dr["BitIsAfiliated"].Equals(DBNull.Value) == true ? false : Convert.ToBoolean(dr["BitIsAfiliated"]);
            M_FundClient.AfiliatedFrom = dr["AfiliatedFromPK"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["AfiliatedFromPK"]);
            M_FundClient.BitisTA = dr["BitisTA"].Equals(DBNull.Value) == true ? false : Convert.ToBoolean(dr["BitisTA"]);

            M_FundClient.TeleponKantor = dr["TeleponKantor"].ToString();
            M_FundClient.NationalityOfficer1 = dr["NationalityOfficer1"].ToString();
            M_FundClient.NationalityOfficer1Desc = dr["NationalityOfficer1Desc"].ToString();
            M_FundClient.NationalityOfficer2 = dr["NationalityOfficer2"].ToString();
            M_FundClient.NationalityOfficer2Desc = dr["NationalityOfficer2Desc"].ToString();
            M_FundClient.NationalityOfficer3 = dr["NationalityOfficer3"].ToString();
            M_FundClient.NationalityOfficer3Desc = dr["NationalityOfficer3Desc"].ToString();
            M_FundClient.NationalityOfficer4 = dr["NationalityOfficer4"].ToString();
            M_FundClient.NationalityOfficer4Desc = dr["NationalityOfficer4Desc"].ToString();

            M_FundClient.IdentityTypeOfficer1 = Convert.ToInt32(dr["IdentityTypeOfficer1"]);
            M_FundClient.IdentityTypeOfficer2 = Convert.ToInt32(dr["IdentityTypeOfficer2"]);
            M_FundClient.IdentityTypeOfficer3 = Convert.ToInt32(dr["IdentityTypeOfficer3"]);
            M_FundClient.IdentityTypeOfficer4 = Convert.ToInt32(dr["IdentityTypeOfficer4"]);
            M_FundClient.NoIdentitasOfficer1 = dr["NoIdentitasOfficer1"].ToString();
            M_FundClient.NoIdentitasOfficer2 = dr["NoIdentitasOfficer2"].ToString();
            M_FundClient.NoIdentitasOfficer3 = dr["NoIdentitasOfficer3"].ToString();
            M_FundClient.NoIdentitasOfficer4 = dr["NoIdentitasOfficer4"].ToString();
            if (_host.CheckColumnIsExist(dr, "OpeningDateSinvest"))
            {
                M_FundClient.OpeningDateSinvest = dr["OpeningDateSinvest"].ToString();
            }

            return M_FundClient;
        }

        public List<FundClient> FundClient_Select(int _status)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<FundClient> L_FundClient = new List<FundClient>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = "select  case when FC.status=1 then 'PENDING' else Case When FC.status = 2 then 'APPROVED' else Case when FC.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,A.ID SellingAgentID,U.ID UsersID,mv1.DescOne KodeKotaInd1Desc,mv2.DescOne KodeKotaInd2Desc,mv3.DescOne KodeKotaInsDesc, " +
                            "mv4.DescOne OtherKodeKotaInd1Desc,mv5.DescOne OtherKodeKotaInd2Desc,mv6.DescOne OtherKodeKotaInd3Desc, " +
                            "mv7.DescOne ClientCategoryDesc,mv8.DescOne InvestorTypeDesc,mv9.DescOne JenisKelaminDesc, " +
                            "mv10.DescOne StatusPerkawinanDesc,mv11.DescOne PekerjaanDesc,mv12.DescOne PendidikanDesc, " +
                            "mv13.DescOne AgamaDesc,mv14.DescOne PenghasilanIndDesc,mv15.DescOne SumberDanaIndDesc, " +
                            "mv16.DescOne MaksudTujuanIndDesc,mv17.DescOne DomisiliDesc,mv18.DescOne TipeDesc, " +
                            "mv19.DescOne KarakteristikDesc,mv20.DescOne PenghasilanInstitusiDesc, " +
                            "mv21.DescOne SumberDanaInstitusiDesc,mv22.DescOne MaksudTujuanInstitusiDesc, mv41.DescOne CountryOfBirthDesc, " +

                            "mv42.DescOne CountryofCorrespondenceDesc, mv43.DescOne CountryofDomicileDesc, " +
                            "mv44.DescOne CountryofEstablishmentDesc, mv45.DescOne CountryofCompanyDesc, mv46.DescOne CompanyCityNameDesc, " +

                            "mv24.DescOne NegaraDesc,mv25.DescOne NationalityDesc,mv26.DescOne PropinsiDesc,mv27.DescOne OtherPropinsiInd1Desc,mv28.DescOne OtherPropinsiInd2Desc,mv29.DescOne OtherPropinsiInd3Desc, " +
                            "mv30.DescOne OtherNegaraInd1Desc,mv31.DescOne OtherNegaraInd2Desc,mv32.DescOne OtherNegaraInd3Desc,isnull(IC.Name,'') InternalCategoryID, " +
                            "mv33.DescOne InvestorsRiskProfileDesc,mv34.DescOne AssetOwnerDesc,mv35.DescOne StatementTypeDesc,mv36.DescOne fatcaDesc,mv37.DescOne TINIssuanceCountryDesc,mv38.DescOne BankCountry1Desc,mv39.DescOne BankCountry2Desc,mv40.DescOne BankCountry3Desc,mv47.DescOne KodeKotaKantorIndDesc,mv48.DescOne KodePropinsiKantorIndDesc,mv49.DescOne KodeCountryofKantorDesc,mv50.DescOne KodeDomisiliPropinsiDesc," +
                            "mv53.DescOne AgamaOfficer1Desc,mv54.DescOne AgamaOfficer2Desc,mv55.DescOne AgamaOfficer3Desc,mv56.DescOne AgamaOfficer4Desc,mv57.DescOne KodeDomisiliPropinsiDesc,mv58.DescOne NationalityOfficer1Desc,mv59.DescOne NationalityOfficer2Desc,mv60.DescOne NationalityOfficer3Desc,mv61.DescOne NationalityOfficer4Desc," +
                            "BC1.BICode BIMemberCode1,BC2.BICode BIMemberCode2,BC3.BICode BIMemberCode3,BC4.SInvestID BICCode1Name,BC5.SInvestID BICCode2Name,BC6.SInvestID BICCode3Name, mv51.DescOne LegalityDesc, mv52.DescOne BeneficialWorkDesc, case when fc.ClientOnBoard = 1 then 'Conventional Walk-in' else case when fc.ClientOnBoard = 2 then 'Online' else case when fc.ClientOnBoard = 3 then 'Referral' else '' end end end ClientOnBoardDesc" +
                            "  case when fc.ClientOnBoard = 1 then 'Conventional Walk-in' else case when fc.ClientOnBoard = 2 then 'Online' else case when fc.ClientOnBoard = 3 then 'Referral' else '' end end end ClientOnBoardDesc, " +
                                "case when B.HighRiskMonitoringPK is null then 'NO' else 'YES' end ComplRequired," +
                            "FC.* from FundClient fc  " +
                            "left join Agent A on fc.SellingAgentPK = A.AgentPK and A.status = 2 " +
                            "left join Users U on fc.UsersPK = U.UsersPK and U.status = 2 " +
                            "left join MasterValue mv1 on fc.KodeKotaInd1 = mv1.Code and mv1.ID = 'CityRHB' and mv1.status = 2 " +
                            "left join MasterValue mv2 on fc.KodeKotaInd2 = mv2.Code and  mv2.ID = 'CityRHB' and mv2.status = 2 " +
                            "left join MasterValue mv3 on fc.KodeKotaIns = mv3.Code and  mv3.ID = 'CityRHB' and mv3.status = 2 " +
                            "left join MasterValue mv4 on fc.OtherKodeKotaInd1 = mv4.Code and mv4.ID = 'CityRHB' and mv4.status = 2 " +
                            "left join MasterValue mv5 on fc.OtherKodeKotaInd2 = mv5.Code and mv5.ID = 'CityRHB' and mv5.status = 2 " +
                            "left join MasterValue mv6 on fc.OtherKodeKotaInd3 = mv6.Code and mv6.ID = 'CityRHB' and mv6.status = 2 " +
                            "left join MasterValue mv7 on fc.ClientCategory = mv7.code  and  mv7.ID = 'ClientCategory' and mv7.status = 2 " +
                            "left join MasterValue mv8 on fc.InvestorType = mv8.code and  mv8.ID = 'ClientType' and mv8.status = 2 " +
                            "left join MasterValue mv9 on fc.JenisKelamin = mv9.code and  mv9.ID = 'Sex' and mv9.status = 2 " +
                            "left join MasterValue mv10 on fc.StatusPerkawinan = mv10.code and  mv10.ID = 'MaritalStatus' and mv10.status = 2 " +
                            "left join MasterValue mv11 on fc.Pekerjaan = mv11.code and  mv11.ID = 'Occupation' and mv11.status = 2 " +
                            "left join MasterValue mv12 on fc.Pendidikan = mv12.code and  mv12.ID = 'EducationalBackground' and mv12.status = 2 " +
                            "left join MasterValue mv13 on fc.Agama = mv13.code and  mv13.ID = 'Religion' and mv13.status = 2 " +
                            "left join MasterValue mv14 on fc.PenghasilanInd = mv14.code and  mv14.ID = 'IncomeIND' and mv14.status = 2 " +
                            "left join MasterValue mv15 on fc.SumberDanaInd = mv15.code and  mv15.ID = 'IncomeSourceIND' and mv15.status = 2 " +
                            "left join MasterValue mv16 on fc.MaksudTujuanInd = mv16.code and  mv16.ID = 'InvestmentObjectivesIND' and mv16.status = 2 " +
                            "left join MasterValue mv17 on fc.Domisili = mv17.code and  mv17.ID = 'Domicile' and mv17.status = 2 " +
                            "left join MasterValue mv18 on fc.Tipe = mv18.code and  mv18.ID = 'CompanyType' and mv18.status = 2 " +
                            "left join MasterValue mv19 on fc.Karakteristik = mv19.code and  mv19.ID = 'CompanyCharacteristic' and mv19.status = 2 " +
                            "left join MasterValue mv20 on fc.PenghasilanInstitusi = mv20.code and  mv20.ID = 'IncomeINS' and mv20.status = 2 " +
                            "left join MasterValue mv21 on fc.SumberDanaInstitusi = mv21.code and  mv21.ID = 'IncomeSourceIND' and mv21.status = 2 " +
                            "left join MasterValue mv22 on fc.MaksudTujuanInstitusi = mv22.code and  mv22.ID = 'InvestmentObjectivesINS' and mv22.status = 2 " +
                            "left join MasterValue mv24 on fc.Negara = mv24.code and  mv24.ID = 'SDICountry' and mv24.status = 2 " +
                            "left join MasterValue mv25 on fc.Nationality = mv25.code and  mv25.ID = 'SDICountry' and mv25.status = 2 " +
                            "left join MasterValue mv26 on fc.Propinsi = mv26.code and  mv26.ID = 'SDIProvince' and mv26.status = 2 " +
                            "left join MasterValue mv27 on fc.OtherPropinsiInd1 = mv27.code and  mv27.ID = 'SDIProvince' and mv27.status = 2 " +
                            "left join MasterValue mv28 on fc.OtherPropinsiInd2 = mv28.code and  mv28.ID = 'SDIProvince' and mv28.status = 2 " +
                            "left join MasterValue mv29 on fc.OtherPropinsiInd3 = mv29.code and  mv29.ID = 'SDIProvince' and mv29.status = 2 " +
                            "left join MasterValue mv30 on fc.OtherNegaraInd1 = mv30.code and  mv30.ID = 'SDICountry' and mv30.status = 2 " +
                            "left join MasterValue mv31 on fc.OtherNegaraInd2 = mv31.code and  mv31.ID = 'SDICountry' and mv31.status = 2 " +
                            "left join MasterValue mv32 on fc.OtherNegaraInd3 = mv32.code and  mv32.ID = 'SDICountry' and mv32.status = 2 " +
                            "left join InternalCategory IC on fc.InternalCategoryPK = IC.InternalCategoryPK and IC.status = 2 " +
                            "left join MasterValue mv33 on fc.InvestorsRiskProfile = mv33.code and  mv33.ID = 'InvestorsRiskProfile' and mv33.status = 2 " +
                            "left join MasterValue mv34 on fc.AssetOwner = mv34.code and  mv34.ID = 'AssetOwner' and mv34.status = 2 " +
                            "left join MasterValue mv35 on fc.StatementType = mv35.code and  mv35.ID = 'StatementType' and mv35.status = 2 " +
                            "left join MasterValue mv36 on fc.fatca = mv36.code and  mv36.ID = 'FATCA' and mv36.status = 2 " +
                            "left join MasterValue mv37 on fc.TINIssuanceCountry = mv37.code and  mv37.ID = 'SDICountry' and mv37.status = 2 " +
                            "left join MasterValue mv38 on fc.BankCountry1 = mv38.code and  mv38.ID = 'SDICountry' and mv38.status = 2 " +
                            "left join MasterValue mv39 on fc.BankCountry2 = mv39.code and  mv39.ID = 'SDICountry' and mv39.status = 2 " +
                            "left join MasterValue mv40 on fc.BankCountry3 = mv40.code and  mv40.ID = 'SDICountry' and mv40.status = 2 " +
                            "left join MasterValue mv41 on fc.CountryOfBirth = mv41.code and  mv41.ID = 'SDICountry' and mv41.status = 2 " +
                            "left join MasterValue mv42 on fc.CountryofCorrespondence = mv42.code and  mv42.ID = 'SDICountry' and mv42.status = 2 " +
                            "left join MasterValue mv43 on fc.CountryofDomicile = mv43.code and  mv43.ID = 'SDICountry' and mv43.status = 2 " +
                            "left join MasterValue mv44 on fc.CountryofEstablishment = mv44.code and  mv44.ID = 'SDICountry' and mv44.status = 2 " +
                            "left join MasterValue mv45 on fc.CountryofCompany = mv45.code and  mv45.ID = 'SDICountry' and mv45.status = 2 " +
                            "left join MasterValue mv46 on fc.CompanyCityName = mv46.Code and mv46.ID = 'CityRHB' and mv46.status = 2 " +
                            "left join MasterValue mv47 on fc.KodeKotaKantorInd = mv42.code and  mv47.ID = 'CityRHB' and mv47.status = 2 " +
                            "left join MasterValue mv48 on fc.KodePropinsiKantorInd = mv48.code and  mv48.ID = 'SDIProvince' and mv48.status = 2 " +
                            "left join MasterValue mv49 on fc.KodeCountryofKantor = mv49.code and  mv49.ID = 'SDICountry' and mv49.status = 2 " +
                            "left join MasterValue mv50 on fc.KodeDomisiliPropinsi = mv50.code and  mv50.ID = 'SDIProvince' and mv50.status = 2 " +
                            "left join MasterValue mv51 on fc.Legality = mv51.code and  mv51.ID = 'Legality' and mv51.status = 2 " +
                            "left join MasterValue mv52 on fc.BeneficialWork = mv52.code and  mv52.ID = 'Occupation' and mv52.status = 2 " +

                            "left join MasterValue mv53 on fc.Agama = mv53.code and  mv53.ID = 'Religion' and mv53.status = 2 " +
                            "left join MasterValue mv54 on fc.Agama = mv54.code and  mv54.ID = 'Religion' and mv54.status = 2 " +
                            "left join MasterValue mv55 on fc.Agama = mv55.code and  mv55.ID = 'Religion' and mv55.status = 2 " +
                            "left join MasterValue mv56 on fc.Agama = mv56.code and  mv56.ID = 'Religion' and mv56.status = 2 " +

                             "left join MasterValue mv57 on fc.EmployerLineOfBusiness = mv57.code and  mv57.ID = 'LineBusiness' and mv57.status = 2 " +
                             "left join MasterValue mv58 on fc.NationalityOfficer1 = mv58.code and  mv58.ID = 'SDICountry' and mv58.status = 2 " +
                             "left join MasterValue mv59 on fc.NationalityOfficer2 = mv59.code and  mv59.ID = 'SDICountry' and mv59.status = 2 " +
                             "left join MasterValue mv60 on fc.NationalityOfficer3 = mv60.code and  mv60.ID = 'SDICountry' and mv60.status = 2 " +
                             "left join MasterValue mv61 on fc.NationalityOfficer4 = mv61.code and  mv61.ID = 'SDICountry' and mv61.status = 2 " +

                            " left join Bank BC1 on fc.NamaBank1 = BC1.BankPK and BC1.status = 2  " +
                            " left join Bank BC2 on fc.NamaBank2 = BC2.BankPK and BC2.status = 2  " +
                            " left join Bank BC3 on fc.NamaBank3 = BC3.BankPK and BC3.status = 2  " +
                            " left join Bank BC4 on fc.NamaBank1 = BC4.BankPK and BC4.status = 2  " +
                            " left join Bank BC5 on fc.NamaBank2 = BC5.BankPK and BC5.status = 2  " +
                            " left join Bank BC6 on fc.NamaBank3 = BC6.BankPK and BC6.status = 2  " +
                            " left join HighRiskMonitoring B on FC.FundClientPK = B.FundClientPK and HighRiskType = 1 and B.Status = 1 " +
                            "where  FC.status = @status ";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = "select case when FC.status=1 then 'PENDING' else Case When FC.status = 2 then 'APPROVED' else Case when FC.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,A.ID SellingAgentID,U.ID UsersID,mv1.DescOne KodeKotaInd1Desc,mv2.DescOne KodeKotaInd2Desc,mv3.DescOne KodeKotaInsDesc, " +
                             "mv4.DescOne OtherKodeKotaInd1Desc,mv5.DescOne OtherKodeKotaInd2Desc,mv6.DescOne OtherKodeKotaInd3Desc, " +
                             "mv7.DescOne ClientCategoryDesc,mv8.DescOne InvestorTypeDesc,mv9.DescOne JenisKelaminDesc, " +
                             "mv10.DescOne StatusPerkawinanDesc,mv11.DescOne PekerjaanDesc,mv12.DescOne PendidikanDesc, " +
                             "mv13.DescOne AgamaDesc,mv14.DescOne PenghasilanIndDesc,mv15.DescOne SumberDanaIndDesc, " +
                             "mv16.DescOne MaksudTujuanIndDesc,mv17.DescOne DomisiliDesc,mv18.DescOne TipeDesc, " +
                             "mv19.DescOne KarakteristikDesc,mv20.DescOne PenghasilanInstitusiDesc, " +
                             "mv21.DescOne SumberDanaInstitusiDesc,mv22.DescOne MaksudTujuanInstitusiDesc, mv41.DescOne CountryOfBirthDesc, " +

                            "mv42.DescOne CountryofCorrespondenceDesc, mv43.DescOne CountryofDomicileDesc, " +
                            "mv44.DescOne CountryofEstablishmentDesc, mv45.DescOne CountryofCompanyDesc, mv46.DescOne CompanyCityNameDesc, " +

                             "mv24.DescOne NegaraDesc,mv25.DescOne NationalityDesc,mv26.DescOne PropinsiDesc,mv27.DescOne OtherPropinsiInd1Desc,mv28.DescOne OtherPropinsiInd2Desc,mv29.DescOne OtherPropinsiInd3Desc, " +
                             "mv30.DescOne OtherNegaraInd1Desc,mv31.DescOne OtherNegaraInd2Desc,mv32.DescOne OtherNegaraInd3Desc,isnull(IC.Name,'') InternalCategoryID, " +
                             "mv33.DescOne InvestorsRiskProfileDesc,mv34.DescOne AssetOwnerDesc,mv35.DescOne StatementTypeDesc,mv36.DescOne fatcaDesc,mv37.DescOne TINIssuanceCountryDesc,mv38.DescOne BankCountry1Desc,mv39.DescOne BankCountry2Desc,mv40.DescOne BankCountry3Desc,mv47.DescOne KodeKotaKantorIndDesc,mv48.DescOne KodePropinsiKantorIndDesc,mv49.DescOne KodeCountryofKantorDesc,mv50.DescOne KodeDomisiliPropinsiDesc," +
                                "BC1.BICode BIMemberCode1,BC2.BICode BIMemberCode2,BC3.BICode BIMemberCode3,BC4.SInvestID BICCode1Name,BC5.SInvestID BICCode2Name,BC6.SInvestID BICCode3Name, mv51.DescOne LegalityDesc, mv52.DescOne BeneficialWorkDesc, case when fc.ClientOnBoard = 1 then 'Conventional Walk-in' else case when fc.ClientOnBoard = 2 then 'Online' else case when fc.ClientOnBoard = 3 then 'Referral' else '' end end end ClientOnBoardDesc" +
                                "mv53.DescOne AgamaOfficer1Desc,mv54.DescOne AgamaOfficer2Desc,mv55.DescOne AgamaOfficer3Desc,mv56.DescOne AgamaOfficer4Desc," +
                                "case when B.HighRiskMonitoringPK is null then 'NO' else 'YES' end ComplRequired," +
                             "FC.* from FundClient fc  " +
                             "left join Agent A on fc.SellingAgentPK = A.AgentPK and A.status = 2 " +
                             "left join Users U on fc.UsersPK = U.UsersPK and U.status = 2 " +
                             "left join MasterValue mv1 on fc.KodeKotaInd1 = mv1.Code and mv1.ID = 'CityRHB' and mv1.status = 2 " +
                             "left join MasterValue mv2 on fc.KodeKotaInd2 = mv2.Code and  mv2.ID = 'CityRHB' and mv2.status = 2 " +
                             "left join MasterValue mv3 on fc.KodeKotaIns = mv3.Code and  mv3.ID = 'CityRHB' and mv3.status = 2 " +
                             "left join MasterValue mv4 on fc.OtherKodeKotaInd1 = mv4.Code and mv4.ID = 'CityRHB' and mv4.status = 2 " +
                             "left join MasterValue mv5 on fc.OtherKodeKotaInd2 = mv5.Code and mv5.ID = 'CityRHB' and mv5.status = 2 " +
                             "left join MasterValue mv6 on fc.OtherKodeKotaInd3 = mv6.Code and mv6.ID = 'CityRHB' and mv6.status = 2 " +
                             "left join MasterValue mv7 on fc.ClientCategory = mv7.code  and  mv7.ID = 'ClientCategory' and mv7.status = 2 " +
                             "left join MasterValue mv8 on fc.InvestorType = mv8.code and  mv8.ID = 'ClientType' and mv8.status = 2 " +
                             "left join MasterValue mv9 on fc.JenisKelamin = mv9.code and  mv9.ID = 'Sex' and mv9.status = 2 " +
                             "left join MasterValue mv10 on fc.StatusPerkawinan = mv10.code and  mv10.ID = 'MaritalStatus' and mv10.status = 2 " +
                             "left join MasterValue mv11 on fc.Pekerjaan = mv11.code and  mv11.ID = 'Occupation' and mv11.status = 2 " +
                             "left join MasterValue mv12 on fc.Pendidikan = mv12.code and  mv12.ID = 'EducationalBackground' and mv12.status = 2 " +
                             "left join MasterValue mv13 on fc.Agama = mv13.code and  mv13.ID = 'Religion' and mv13.status = 2 " +
                             "left join MasterValue mv14 on fc.PenghasilanInd = mv14.code and  mv14.ID = 'IncomeIND' and mv14.status = 2 " +
                             "left join MasterValue mv15 on fc.SumberDanaInd = mv15.code and  mv15.ID = 'IncomeSourceIND' and mv15.status = 2 " +
                             "left join MasterValue mv16 on fc.MaksudTujuanInd = mv16.code and  mv16.ID = 'InvestmentObjectivesIND' and mv16.status = 2 " +
                             "left join MasterValue mv17 on fc.Domisili = mv17.code and  mv17.ID = 'Domicile' and mv17.status = 2 " +
                             "left join MasterValue mv18 on fc.Tipe = mv18.code and  mv18.ID = 'CompanyType' and mv18.status = 2 " +
                             "left join MasterValue mv19 on fc.Karakteristik = mv19.code and  mv19.ID = 'CompanyCharacteristic' and mv19.status = 2 " +
                             "left join MasterValue mv20 on fc.PenghasilanInstitusi = mv20.code and  mv20.ID = 'IncomeINS' and mv20.status = 2 " +
                             "left join MasterValue mv21 on fc.SumberDanaInstitusi = mv21.code and  mv21.ID = 'IncomeSourceIND' and mv21.status = 2 " +
                             "left join MasterValue mv22 on fc.MaksudTujuanInstitusi = mv22.code and  mv22.ID = 'InvestmentObjectivesINS' and mv22.status = 2 " +
                             "left join MasterValue mv24 on fc.Negara = mv24.code and  mv24.ID = 'SDICountry' and mv24.status = 2 " +
                             "left join MasterValue mv25 on fc.Nationality = mv25.code and  mv25.ID = 'Nationality' and mv25.status = 2 " +
                             "left join MasterValue mv26 on fc.Propinsi = mv26.code and  mv26.ID = 'SDIProvince' and mv26.status = 2 " +
                             "left join MasterValue mv27 on fc.OtherPropinsiInd1 = mv27.code and  mv27.ID = 'SDIProvince' and mv27.status = 2 " +
                             "left join MasterValue mv28 on fc.OtherPropinsiInd2 = mv28.code and  mv28.ID = 'SDIProvince' and mv28.status = 2 " +
                             "left join MasterValue mv29 on fc.OtherPropinsiInd3 = mv29.code and  mv29.ID = 'SDIProvince' and mv29.status = 2 " +
                             "left join MasterValue mv30 on fc.OtherNegaraInd1 = mv30.code and  mv30.ID = 'SDICountry' and mv30.status = 2 " +
                             "left join MasterValue mv31 on fc.OtherNegaraInd2 = mv31.code and  mv31.ID = 'SDICountry' and mv31.status = 2 " +
                             "left join MasterValue mv32 on fc.OtherNegaraInd3 = mv32.code and  mv32.ID = 'SDICountry' and mv32.status = 2 " +
                             "left join InternalCategory IC on fc.InternalCategoryPK = IC.InternalCategoryPK and IC.status = 2 " +
                             "left join MasterValue mv33 on fc.InvestorsRiskProfile = mv33.code and  mv33.ID = 'InvestorsRiskProfile' and mv33.status = 2 " +
                             "left join MasterValue mv34 on fc.AssetOwner = mv34.code and  mv34.ID = 'AssetOwner' and mv34.status = 2 " +
                             "left join MasterValue mv35 on fc.StatementType = mv35.code and  mv35.ID = 'StatementType' and mv35.status = 2 " +
                             "left join MasterValue mv36 on fc.fatca = mv36.code and  mv36.ID = 'FATCA' and mv36.status = 2 " +
                             "left join MasterValue mv37 on fc.TINIssuanceCountry = mv37.code and  mv37.ID = 'SDICountry' and mv37.status = 2 " +
                             "left join MasterValue mv38 on fc.BankCountry1 = mv38.code and  mv38.ID = 'SDICountry' and mv38.status = 2 " +
                             "left join MasterValue mv39 on fc.BankCountry2 = mv39.code and  mv39.ID = 'SDICountry' and mv39.status = 2 " +
                             "left join MasterValue mv40 on fc.BankCountry3 = mv40.code and  mv40.ID = 'SDICountry' and mv40.status = 2 " +
                             "left join MasterValue mv41 on fc.CountryOfBirth = mv41.code and  mv41.ID = 'SDICountry' and mv41.status = 2 " +
                             "left join MasterValue mv42 on fc.CountryofCorrespondence = mv42.code and  mv42.ID = 'SDICountry' and mv42.status = 2 " +
                            "left join MasterValue mv43 on fc.CountryofDomicile = mv43.code and  mv43.ID = 'SDICountry' and mv43.status = 2 " +
                            "left join MasterValue mv44 on fc.CountryofEstablishment = mv44.code and  mv44.ID = 'SDICountry' and mv44.status = 2 " +
                            "left join MasterValue mv45 on fc.CountryofCompany = mv45.code and  mv45.ID = 'SDICountry' and mv45.status = 2 " +
                             "left join MasterValue mv46 on fc.CompanyCityName = mv46.Code and mv46.ID = 'CityRHB' and mv46.status = 2 " +
                            "left join MasterValue mv47 on fc.KodeKotaKantorInd = mv42.code and  mv47.ID = 'CityRHB' and mv47.status = 2 " +
                            "left join MasterValue mv48 on fc.KodePropinsiKantorInd = mv48.code and  mv48.ID = 'SDIProvince' and mv48.status = 2 " +
                            "left join MasterValue mv49 on fc.KodeCountryofKantor = mv49.code and  mv49.ID = 'SDICountry' and mv49.status = 2 " +
                            "left join MasterValue mv50 on fc.KodeDomisiliPropinsi = mv50.code and  mv50.ID = 'SDIProvince' and mv50.status = 2 " +
                            "left join MasterValue mv51 on fc.Legality = mv51.code and  mv51.ID = 'Legality' and mv51.status = 2 " +
                            "left join MasterValue mv52 on fc.BeneficialWork = mv52.code and  mv52.ID = 'Occupation' and mv52.status = 2 " +

                            "left join MasterValue mv53 on fc.Agama = mv53.code and  mv53.ID = 'Religion' and mv53.status = 2 " +
                            "left join MasterValue mv54 on fc.Agama = mv54.code and  mv54.ID = 'Religion' and mv54.status = 2 " +
                            "left join MasterValue mv55 on fc.Agama = mv55.code and  mv55.ID = 'Religion' and mv55.status = 2 " +
                            "left join MasterValue mv56 on fc.Agama = mv56.code and  mv56.ID = 'Religion' and mv56.status = 2 " +

                             " left join Bank BC1 on fc.NamaBank1 = BC1.BankPK and BC1.status = 2  " +
                             " left join Bank BC2 on fc.NamaBank2 = BC2.BankPK and BC2.status = 2  " +
                             " left join Bank BC3 on fc.NamaBank3 = BC3.BankPK and BC3.status = 2  " +
                            " left join Bank BC4 on fc.NamaBank1 = BC4.BankPK and BC4.status = 2  " +
                            " left join Bank BC5 on fc.NamaBank2 = BC5.BankPK and BC5.status = 2  " +
                            " left join Bank BC6 on fc.NamaBank3 = BC6.BankPK and BC6.status = 2  " +
                            " left join HighRiskMonitoring B on FC.FundClientPK = B.FundClientPK and HighRiskType = 1 and B.Status = 1 ";
                        }

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_FundClient.Add(setFundClient(dr));
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


        public FundClient FundClient_SelectByFundClientPKandHistoryPK(int _fundClientPK, int _historyPK)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandText = @"select  case when FC.status = 1 then 'PENDING' else Case When FC.status = 2 then 'APPROVED' else Case when FC.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,A.ID SellingAgentID,U.ID UsersID,mv1.DescOne KodeKotaInd1Desc,mv2.DescOne KodeKotaInd2Desc,mv3.DescOne KodeKotaInsDesc,    
                                       mv4.DescOne OtherKodeKotaInd1Desc,mv5.DescOne OtherKodeKotaInd2Desc,mv6.DescOne OtherKodeKotaInd3Desc,    
                                       mv7.DescOne ClientCategoryDesc,mv8.DescOne InvestorTypeDesc,mv9.DescOne JenisKelaminDesc,    
                                       mv10.DescOne StatusPerkawinanDesc,mv11.DescOne PekerjaanDesc,mv12.DescOne PendidikanDesc,    
                                       mv13.DescOne AgamaDesc,mv14.DescOne PenghasilanIndDesc,mv15.DescOne SumberDanaIndDesc,    
                                       mv16.DescOne MaksudTujuanIndDesc,mv17.DescOne DomisiliDesc,mv18.DescOne TipeDesc,    
                                       mv19.DescOne KarakteristikDesc,mv20.DescOne PenghasilanInstitusiDesc,    
                                       mv21.DescOne SumberDanaInstitusiDesc,mv22.DescOne MaksudTujuanInstitusiDesc,    

                                          mv42.DescOne CountryofCorrespondenceDesc, mv43.DescOne CountryofDomicileDesc,    
                                       mv44.DescOne CountryofEstablishmentDesc, mv45.DescOne CountryofCompanyDesc, mv46.DescOne CompanyCityNameDesc,    

                                       mv24.DescOne NegaraDesc,mv25.DescOne NationalityDesc,mv26.DescOne PropinsiDesc,mv27.DescOne OtherPropinsiInd1Desc,mv28.DescOne OtherPropinsiInd2Desc,mv29.DescOne OtherPropinsiInd3Desc,    
                                       mv41.DescOne CountryOfBirthDesc,mv30.DescOne OtherNegaraInd1Desc,mv31.DescOne OtherNegaraInd2Desc,mv32.DescOne OtherNegaraInd3Desc,isnull(IC.Name,'') InternalCategoryID,    
                                       mv33.DescOne InvestorsRiskProfileDesc,mv34.DescOne AssetOwnerDesc,mv35.DescOne StatementTypeDesc,mv36.DescOne fatcaDesc,mv37.DescOne TINIssuanceCountryDesc,  mv48.DescOne SpouseOccupationDesc,mv49.DescOne SpouseNatureOfBusinessDesc, mv50.DescOne SpouseNationalityDesc, mv51.DescOne KYCRiskProfileDesc, mv52.DescOne KodeKotaKantorIndDesc,mv53.DescOne KodePropinsiKantorIndDesc,mv54.DescOne KodeCountryofKantorDesc,mv55.DescOne KodeDomisiliPropinsiDesc, isnull(mv56.DescOne,'') LegalityDesc, isnull(mv57.DescOne, '') CompanyTypeOJKDesc, mv58.DescOne MigrationStatus, mv59.DescOne SegmentClass,   
                                       mv60.DescOne AgamaOfficer1Desc,mv61.DescOne AgamaOfficer2Desc,mv62.DescOne AgamaOfficer3Desc,mv63.DescOne AgamaOfficer4Desc,mv64.DescOne NationalityOfficer1Desc,mv65.DescOne NationalityOfficer2Desc,mv66.DescOne NationalityOfficer3Desc,mv67.DescOne NationalityOfficer4Desc,
                                        BC1.BICode BIMemberCode1,BC2.BICode BIMemberCode2,BC3.BICode BIMemberCode3,BC4.SInvestID BICCode1Name,BC5.SInvestID BICCode2Name,BC6.SInvestID BICCode3Name,mv47.DescOne RiskProfileScoreDesc,   isnull(mv68.DescOne,'') EmployerLineOfBusinessDesc, 
                                         case when fc.ClientOnBoard = 1 then 'Conventional Walk-in' else case when fc.ClientOnBoard = 2 then 'Online' else case when fc.ClientOnBoard = 3 then 'Referral' else '' end end end ClientOnBoardDesc,
                                case when B.HighRiskMonitoringPK is null then 'NO' else 'YES' end ComplRequired,
                                        FC.* from FundClient fc     
                                       left join Agent A on fc.SellingAgentPK = A.AgentPK and A.status = 2
                                       left join Users U on fc.UsersPK= U.UsersPK and U.status = 2        
                                       left join MasterValue mv1 on fc.KodeKotaInd1 = mv1.Code and mv1.ID = 'CityRHB' and mv1.status = 2    
                                       left join MasterValue mv2 on fc.KodeKotaInd2 = mv2.Code and  mv2.ID = 'CityRHB' and mv2.status = 2    
                                       left join MasterValue mv3 on fc.KodeKotaIns = mv3.Code and  mv3.ID = 'CityRHB' and mv3.status = 2    
                                       left join MasterValue mv4 on fc.OtherKodeKotaInd1 = mv4.Code and mv4.ID = 'CityRHB' and mv4.status = 2    
                                       left join MasterValue mv5 on fc.OtherKodeKotaInd2 = mv5.Code and mv5.ID = 'CityRHB' and mv5.status = 2    
                                       left join MasterValue mv6 on fc.OtherKodeKotaInd3 = mv6.Code and mv6.ID = 'CityRHB' and mv6.status = 2    
                                       left join MasterValue mv7 on fc.ClientCategory = mv7.code  and  mv7.ID = 'ClientCategory' and mv7.status = 2    
                                       left join MasterValue mv8 on fc.InvestorType = mv8.code and  mv8.ID = 'ClientType' and mv8.status = 2    
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
                                       left join MasterValue mv25 on fc.Nationality = mv25.code and  mv25.ID = 'SDICountry' and mv25.status = 2    
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
                                       left join MasterValue mv36 on fc.fatca = mv36.code and  mv36.ID = 'FATCA' and mv36.status = 2    
                                       left join MasterValue mv37 on fc.TINIssuanceCountry = mv37.code and  mv37.ID = 'SDICountry' and mv37.status = 2    
                                       left join MasterValue mv41 on fc.CountryOfBirth = mv41.code and  mv41.ID = 'SDICountry' and mv41.status = 2    
                                       left join MasterValue mv42 on fc.CountryofCorrespondence = mv42.code and  mv42.ID = 'SDICountry' and mv42.status = 2    
                                       left join MasterValue mv43 on fc.CountryofDomicile = mv43.code and  mv43.ID = 'SDICountry' and mv43.status = 2    
                                       left join MasterValue mv44 on fc.CountryofEstablishment = mv44.code and  mv44.ID = 'SDICountry' and mv44.status = 2    
                                       left join MasterValue mv45 on fc.CountryofCompany = mv45.code and  mv45.ID = 'SDICountry' and mv45.status = 2    
                                       left join MasterValue mv46 on fc.CompanyCityName = mv46.Code and mv46.ID = 'CityRHB' and mv46.status = 2    
                                       left join MasterValue mv47 on fc.RiskProfileScore = mv47.Code and mv47.ID = 'InvestorsRiskProfile' and mv47.status = 2    
                                       left join MasterValue mv48 on fc.SpouseOccupation = mv48.code and  mv48.ID = 'Occupation' and mv48.status = 2    
                                       left join MasterValue mv49 on fc.SpouseNatureOfBusiness = mv49.code and  mv49.ID = 'HRBusiness' and mv49.status = 2    
                                       left join MasterValue mv50 on fc.SpouseNationality = mv50.code and  mv50.ID = 'SDICountry' and mv50.status = 2    
                                       left join MasterValue mv51 on fc.KYCRiskProfile = mv51.Code and mv51.ID = 'KYCRiskProfile' and mv51.status = 2     
                                       left join MasterValue mv52 on fc.KodeKotaKantorInd = mv52.code and  mv52.ID = 'CityRHB' and mv52.status = 2    
                                       left join MasterValue mv53 on fc.KodePropinsiKantorInd = mv53.code and  mv53.ID = 'SDIProvince' and mv53.status = 2    
                                       left join MasterValue mv54 on fc.KodeCountryofKantor = mv54.code and  mv54.ID = 'SDICountry' and mv54.status = 2    
                                       left join MasterValue mv55 on fc.KodeDomisiliPropinsi = mv55.code and  mv55.ID = 'SDIProvince' and mv55.status = 2   
                                       left join MasterValue mv56 on fc.Legality = mv56.code and  mv56.ID = 'Legality' and mv56.status = 2     
                                       left join MasterValue mv57 on fc.CompanyTypeOJK = mv57.code and  mv57.ID = 'CompanyTypeOJK' and mv57.status = 2  
                                       left join MasterValue mv58 on fc.MigrationStatus = mv58.code and  mv58.ID = 'MigrationStatus' and mv58.status = 2     
                                       left join MasterValue mv59 on fc.SegmentClass = mv59.code and  mv59.ID = 'SegmentClass' and mv59.status = 2 
                                       left join MasterValue mv60 on fc.Agama = mv60.code and  mv60.ID = 'Religion' and mv60.status = 2 
                                       left join MasterValue mv61 on fc.Agama = mv61.code and  mv61.ID = 'Religion' and mv61.status = 2  
                                       left join MasterValue mv62 on fc.Agama = mv62.code and  mv62.ID = 'Religion' and mv62.status = 2  
                                       left join MasterValue mv63 on fc.Agama = mv63.code and  mv63.ID = 'Religion' and mv63.status = 2  

                                       left join MasterValue mv64 on fc.Nationality = mv64.code and  mv64.ID = 'SDICountry' and mv64.status = 2  
                                       left join MasterValue mv65 on fc.Nationality = mv65.code and  mv65.ID = 'SDICountry' and mv65.status = 2  
                                       left join MasterValue mv66 on fc.Nationality = mv66.code and  mv66.ID = 'SDICountry' and mv66.status = 2  
                                       left join MasterValue mv67 on fc.Nationality = mv67.code and  mv67.ID = 'SDICountry' and mv67.status = 2  
                                       left join MasterValue mv68 on fc.EmployerLineOfBusiness = mv68.code and  mv68.ID = 'LineBusiness' and mv67.status = 2  

                                       left join Bank BC1 on fc.NamaBank1 = BC1.BankPK and BC1.status = 2     
                                       left join Bank BC2 on fc.NamaBank2 = BC2.BankPK and BC2.status = 2     
                                       left join Bank BC3 on fc.NamaBank3 = BC3.BankPK and BC3.status = 2     
                                       left join Bank BC4 on fc.NamaBank1 = BC4.BankPK and BC4.status = 2     
                                       left join Bank BC5 on fc.NamaBank2 = BC5.BankPK and BC5.status = 2     
                                       left join Bank BC6 on fc.NamaBank3 = BC6.BankPK and BC6.status = 2 
                                        left join HighRiskMonitoring B on FC.FundClientPK = B.FundClientPK and HighRiskType = 1 and B.Status = 1 
                                      where  FC.HistoryPK = @HistoryPK and FC.FundClientPK  = @FundClientPK ";
                        cmd.Parameters.AddWithValue("@FundClientPK", _fundClientPK);
                        cmd.Parameters.AddWithValue("@HistoryPK", _historyPK);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return setFundClient(dr);
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

        public FundClient FundClient_SelectByFundClientPK(int _status, int _fundClientPK)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandText = @"select  case when FC.status=1 then 'PENDING' else Case When FC.status = 2 then 'APPROVED' else Case when FC.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,A.ID SellingAgentID,U.ID UsersID,mv1.DescOne KodeKotaInd1Desc,mv2.DescOne KodeKotaInd2Desc,mv3.DescOne KodeKotaInsDesc,  
                                        mv4.DescOne OtherKodeKotaInd1Desc,mv5.DescOne OtherKodeKotaInd2Desc,mv6.DescOne OtherKodeKotaInd3Desc,  
                                        mv7.DescOne ClientCategoryDesc,mv8.DescOne InvestorTypeDesc,mv9.DescOne JenisKelaminDesc,  
                                        mv10.DescOne StatusPerkawinanDesc,mv11.DescOne PekerjaanDesc,mv12.DescOne PendidikanDesc,  
                                        mv13.DescOne AgamaDesc,mv14.DescOne PenghasilanIndDesc,mv15.DescOne SumberDanaIndDesc,  
                                        mv16.DescOne MaksudTujuanIndDesc,mv17.DescOne DomisiliDesc,mv18.DescOne TipeDesc,  
                                        mv19.DescOne KarakteristikDesc,mv20.DescOne PenghasilanInstitusiDesc,  
                                        mv21.DescOne SumberDanaInstitusiDesc,mv22.DescOne MaksudTujuanInstitusiDesc,  
                                        mv42.DescOne CountryofCorrespondenceDesc, mv43.DescOne CountryofDomicileDesc,  
                                        mv44.DescOne CountryofEstablishmentDesc, mv45.DescOne CountryofCompanyDesc, mv46.DescOne CompanyCityNameDesc,  
                                        mv24.DescOne NegaraDesc,mv25.DescOne NationalityDesc,mv26.DescOne PropinsiDesc,mv27.DescOne OtherPropinsiInd1Desc,mv28.DescOne OtherPropinsiInd2Desc,mv29.DescOne OtherPropinsiInd3Desc,  
                                        mv41.DescOne CountryOfBirthDesc,mv30.DescOne OtherNegaraInd1Desc,mv31.DescOne OtherNegaraInd2Desc,mv32.DescOne OtherNegaraInd3Desc,isnull(IC.Name,'') InternalCategoryID,  
                                        mv33.DescOne InvestorsRiskProfileDesc,mv34.DescOne AssetOwnerDesc,mv35.DescOne StatementTypeDesc,mv36.DescOne fatcaDesc,mv37.DescOne TINIssuanceCountryDesc, 
                                        mv61.DescOne AgamaOfficer1Desc,mv62.DescOne AgamaOfficer2Desc,mv63.DescOne AgamaOfficer3Desc,mv64.DescOne AgamaOfficer4Desc,mv65.DescOne EmployerLineOfBusinessDesc,mv66.DescOne NationalityOfficer1Desc,mv67.DescOne NationalityOfficer2Desc,mv68.DescOne NationalityOfficer3Desc,mv69.DescOne NationalityOfficer4Desc,
                                        BC1.BICode BIMemberCode1,BC2.BICode BIMemberCode2,BC3.BICode BIMemberCode3,BC1.SInvestID BICCode1Name,BC2.SInvestID BICCode2Name,BC3.SInvestID BICCode3Name,mv47.DescOne RiskProfileScoreDesc, mv48.DescOne SpouseOccupationDesc,mv49.DescOne SpouseNatureOfBusinessDesc, mv50.DescOne SpouseNationalityDesc, mv51.DescOne KYCRiskProfileDesc,mv52.DescOne KodeKotaKantorIndDesc,mv53.DescOne KodePropinsiKantorIndDesc,mv54.DescOne KodeCountryofKantorDesc,mv55.DescOne KodeDomisiliPropinsiDesc, isnull(mv56.DescOne,'') LegalityDesc, isnull(mv57.DescOne, '') CompanyTypeOJKDesc, mv58.DescOne MigrationStatusDesc, mv59.DescOne SegmentClassDesc,  case when fc.ClientOnBoard = 1 then 'Conventional Walk-in' else case when fc.ClientOnBoard = 2 then 'Online' else case when fc.ClientOnBoard = 3 then 'Referral' else '' end end end ClientOnBoardDesc,
                                case when B.HighRiskMonitoringPK is null then 'NO' else 'YES' end ComplRequired,
                                        FC.* from FundClient fc   
                                        left join Agent A on fc.SellingAgentPK = A.AgentPK and A.status = 2  
                                        left join Users U on fc.UsersPK = U.UsersPK and U.status = 2  
                                        left join MasterValue mv1 on fc.KodeKotaInd1 = mv1.Code and mv1.ID = 'CityRHB' and mv1.status = 2  
                                        left join MasterValue mv2 on fc.KodeKotaInd2 = mv2.Code and  mv2.ID = 'CityRHB' and mv2.status = 2  
                                        left join MasterValue mv3 on fc.KodeKotaIns = mv3.Code and  mv3.ID = 'CityRHB' and mv3.status = 2  
                                        left join MasterValue mv4 on fc.OtherKodeKotaInd1 = mv4.Code and mv4.ID = 'CityRHB' and mv4.status = 2  
                                        left join MasterValue mv5 on fc.OtherKodeKotaInd2 = mv5.Code and mv5.ID = 'CityRHB' and mv5.status = 2  
                                        left join MasterValue mv6 on fc.OtherKodeKotaInd3 = mv6.Code and mv6.ID = 'CityRHB' and mv6.status = 2  
                                        left join MasterValue mv7 on fc.ClientCategory = mv7.code  and  mv7.ID = 'ClientCategory' and mv7.status = 2  
                                        left join MasterValue mv8 on fc.InvestorType = mv8.code and  mv8.ID = 'ClientType' and mv8.status = 2  
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
                                        left join MasterValue mv25 on fc.Nationality = mv25.code and  mv25.ID = 'SDICountry' and mv25.status = 2  
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
                                        left join MasterValue mv36 on fc.fatca = mv36.code and  mv36.ID = 'FATCA' and mv36.status = 2  
                                        left join MasterValue mv37 on fc.TINIssuanceCountry = mv37.code and  mv37.ID = 'SDICountry' and mv37.status = 2  
                                        left join MasterValue mv41 on fc.CountryOfBirth = mv41.code and  mv41.ID = 'SDICountry' and mv41.status = 2  
                                        left join MasterValue mv42 on fc.CountryofCorrespondence = mv42.code and  mv42.ID = 'SDICountry' and mv42.status = 2  
                                        left join MasterValue mv43 on fc.CountryofDomicile = mv43.code and  mv43.ID = 'SDICountry' and mv43.status = 2  
                                        left join MasterValue mv44 on fc.CountryofEstablishment = mv44.code and  mv44.ID = 'SDICountry' and mv44.status = 2  
                                        left join MasterValue mv45 on fc.CountryofCompany = mv45.code and  mv45.ID = 'SDICountry' and mv45.status = 2  
                                        left join MasterValue mv46 on fc.CompanyCityName = mv46.Code and mv46.ID = 'CityRHB' and mv46.status = 2  
                                        left join MasterValue mv47 on fc.RiskProfileScore = mv47.Code and mv47.ID = 'InvestorsRiskProfile' and mv47.status = 2 
                                        left join MasterValue mv48 on fc.SpouseOccupation = mv48.code and  mv48.ID = 'Occupation' and mv48.status = 2 
                                        left join MasterValue mv49 on fc.SpouseNatureOfBusiness = mv49.code and  mv49.ID = 'HRBusiness' and mv49.status = 2 
                                        left join MasterValue mv50 on fc.SpouseNationality = mv50.code and  mv50.ID = 'SDICountry' and mv50.status = 2  
                                        left join MasterValue mv51 on fc.KYCRiskProfile = mv51.Code and mv51.ID = 'KYCRiskProfile' and mv51.status = 2  
                                        left join MasterValue mv52 on fc.KodeKotaKantorInd = mv52.code and  mv52.ID = 'CityRHB' and mv52.status = 2 
                                        left join MasterValue mv53 on fc.KodePropinsiKantorInd = mv53.code and  mv53.ID = 'SDIProvince' and mv53.status = 2 
                                        left join MasterValue mv54 on fc.KodeCountryofKantor = mv54.code and  mv54.ID = 'SDICountry' and mv54.status = 2 
                                        left join MasterValue mv55 on fc.KodeDomisiliPropinsi = mv55.code and  mv55.ID = 'SDIProvince' and mv55.status = 2  
                                        left join MasterValue mv56 on fc.Legality = mv56.code and  mv56.ID = 'Legality' and mv56.status = 2        
                                        left join MasterValue mv57 on fc.CompanyTypeOJK = mv57.code and  mv57.ID = 'CompanyTypeOJK' and mv57.status = 2  
                                        left join MasterValue mv58 on fc.MigrationStatus = mv58.code and  mv58.ID = 'MigrationStatus' and mv58.status = 2     
                                        left join MasterValue mv59 on fc.SegmentClass = mv59.code and  mv59.ID = 'SegmentClass' and mv59.status = 2   
                                        left join MasterValue mv60 on fc.BeneficialWork = mv60.code and  mv60.ID = 'Occupation' and mv52.status = 2    
                                       left join MasterValue mv61 on fc.AgamaOfficer1 = mv61.code and  mv61.ID = 'Religion' and mv61.status = 2 
                                       left join MasterValue mv62 on fc.AgamaOfficer2 = mv62.code and  mv62.ID = 'Religion' and mv62.status = 2  
                                       left join MasterValue mv63 on fc.AgamaOfficer3 = mv63.code and  mv63.ID = 'Religion' and mv63.status = 2  
                                       left join MasterValue mv64 on fc.AgamaOfficer4 = mv64.code and  mv64.ID = 'Religion' and mv64.status = 2 
                                        left join MasterValue mv65 on fc.EmployerLineOfBusiness = mv65.code and  mv65.ID = 'LineBusiness' and mv65.status = 2  

                                       left join MasterValue mv66 on fc.NationalityOfficer1 = mv66.code and  mv66.ID = 'SDICountry' and mv66.status = 2  
                                       left join MasterValue mv67 on fc.NationalityOfficer2 = mv67.code and  mv67.ID = 'SDICountry' and mv67.status = 2  
                                       left join MasterValue mv68 on fc.NationalityOfficer3 = mv68.code and  mv68.ID = 'SDICountry' and mv68.status = 2  
                                       left join MasterValue mv69 on fc.NationalityOfficer4 = mv69.code and  mv69.ID = 'SDICountry' and mv69.status = 2 
                               
                                        left join Bank BC1 on fc.NamaBank1 = BC1.BankPK and BC1.status = 2   
                                        left join Bank BC2 on fc.NamaBank2 = BC2.BankPK and BC2.status = 2   
                                        left join Bank BC3 on fc.NamaBank3 = BC3.BankPK and BC3.status = 2
                                        left join HighRiskMonitoring B on FC.FundClientPK = B.FundClientPK and HighRiskType = 1 and B.Status = 1 
                                        where  FC.status = @status and FC.FundClientPK  = @FundClientPK ";
                        cmd.Parameters.AddWithValue("@Status", _status);
                        cmd.Parameters.AddWithValue("@FundClientPK", _fundClientPK);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return setFundClient(dr);
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


        private FundClientSearchResult setFundClientSearchResult(SqlDataReader dr)
        {
            FundClientSearchResult M_FundClientSearchResult = new FundClientSearchResult();
            M_FundClientSearchResult.Selected = Convert.ToBoolean(dr["Selected"]);
            M_FundClientSearchResult.FundClientPK = Convert.ToInt32(dr["FundClientPK"]);
            if (_host.CheckColumnIsExist(dr, "historyPK"))
            {
                M_FundClientSearchResult.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            }
            if (_host.CheckColumnIsExist(dr, "ComplRequired"))
            {
                M_FundClientSearchResult.ComplRequired = dr["ComplRequired"].ToString();
            }
            M_FundClientSearchResult.ID = Convert.ToString(dr["ID"]);
            M_FundClientSearchResult.Name = Convert.ToString(dr["Name"]);
            M_FundClientSearchResult.SellingAgentID = Convert.ToString(dr["SellingAgentID"]);
            M_FundClientSearchResult.Email = Convert.ToString(dr["Email"]);
            M_FundClientSearchResult.TeleponSelular = Convert.ToString(dr["TeleponSelular"]);
            M_FundClientSearchResult.NamaBank1 = Convert.ToString(dr["NamaBank1"]);
            M_FundClientSearchResult.NomorRekening1 = Convert.ToString(dr["NomorRekening1"]);
            M_FundClientSearchResult.ClientCategoryDesc = Convert.ToString(dr["ClientCategoryDesc"]);
            M_FundClientSearchResult.IFUACode = Convert.ToString(dr["IFUACode"]);
            M_FundClientSearchResult.SID = Convert.ToString(dr["SID"]);
            M_FundClientSearchResult.NoIdentitasInd1 = Convert.ToString(dr["NoIdentitasInd1"]);
            M_FundClientSearchResult.TanggalLahir = Convert.ToString(dr["TanggalLahir"]);
            M_FundClientSearchResult.KYCRiskProfile = Convert.ToInt32(dr["KYCRiskProfile"]);
            M_FundClientSearchResult.EntryUsersID = dr["EntryUsersID"].ToString();
            M_FundClientSearchResult.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_FundClientSearchResult.EntryTime = dr["EntryTime"].ToString();
            M_FundClientSearchResult.UpdateTime = dr["UpdateTime"].ToString();
            M_FundClientSearchResult.LastUpdate = Convert.ToString(dr["Lastupdate"]);
            M_FundClientSearchResult.BitIsSuspend = Convert.ToBoolean(dr["BitIsSuspend"]);
            M_FundClientSearchResult.SuspendTime = dr["SuspendTime"].ToString();
            M_FundClientSearchResult.UnSuspendTime = dr["UnSuspendTime"].ToString();
            M_FundClientSearchResult.SuspendBy = dr["SuspendBy"].ToString();
            M_FundClientSearchResult.UnSuspendBy = dr["UnSuspendBy"].ToString();
            if (_host.CheckColumnIsExist(dr, "FrontID"))
            {
                M_FundClientSearchResult.FrontID = dr["FrontID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["FrontID"]);
            }
            
            
            return M_FundClientSearchResult;
        }
        public List<FundClientSearchResult> FundClientSearch_Select(int _status, string _param, string _usersID, string _SelectType)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<FundClientSearchResult> L_FundClient = new List<FundClientSearchResult>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        if (Tools.ClientCode == "08")
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandTimeout = 0;
                            cmd.CommandText = "Searchfundclientv2_08";
                        }

                        else if (Tools.ClientCode == "10")
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandTimeout = 0;
                            cmd.CommandText = "Searchfundclientv2_10";
                        }
                        else
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandTimeout = 0;
                            //cmd.CommandText = "SearchFundClient";
                            cmd.CommandText = "Searchfundclient_v2";
                        }
                        cmd.Parameters.AddWithValue("@UserID", _usersID);
                        cmd.Parameters.AddWithValue("@SelectType", _SelectType);
                        cmd.Parameters.AddWithValue("@str", _param);
                        cmd.Parameters.AddWithValue("@status", _status);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_FundClient.Add(setFundClientSearchResult(dr));
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

        public int FundClient_Add(FundClient _fundClient, bool _havePrivillege)
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
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _fundClient.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);
                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[LastUpdate])" +
                                "Select isnull(max(FundClientPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@LastUpdate from FundClient";
                        }
                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@ID", _fundClient.ID);
                        cmd.Parameters.AddWithValue("@Name", _fundClient.Name);
                        cmd.Parameters.AddWithValue("@ClientCategory", _fundClient.ClientCategory);
                        cmd.Parameters.AddWithValue("@InvestorType", _fundClient.InvestorType);
                        cmd.Parameters.AddWithValue("@InternalCategoryPK", _fundClient.InternalCategoryPK);
                        cmd.Parameters.AddWithValue("@SellingAgentPK", _fundClient.SellingAgentPK);
                        cmd.Parameters.AddWithValue("@UsersPK", _fundClient.UsersPK);
                        cmd.Parameters.AddWithValue("@SID", _fundClient.SID);
                        cmd.Parameters.AddWithValue("@IFUACode", _fundClient.IFUACode);
                        cmd.Parameters.AddWithValue("@Child", _fundClient.Child);
                        cmd.Parameters.AddWithValue("@ARIA", _fundClient.ARIA);
                        cmd.Parameters.AddWithValue("@Registered", _fundClient.Registered);
                        cmd.Parameters.AddWithValue("@JumlahDanaAwal", _fundClient.JumlahDanaAwal);
                        cmd.Parameters.AddWithValue("@JumlahDanaSaatIniCash", _fundClient.JumlahDanaSaatIniCash);
                        cmd.Parameters.AddWithValue("@JumlahDanaSaatIni", _fundClient.JumlahDanaSaatIni);
                        cmd.Parameters.AddWithValue("@Negara", _fundClient.Negara);
                        cmd.Parameters.AddWithValue("@Nationality", _fundClient.Nationality);
                        cmd.Parameters.AddWithValue("@NPWP", _fundClient.NPWP);
                        cmd.Parameters.AddWithValue("@SACode", _fundClient.SACode);
                        cmd.Parameters.AddWithValue("@Propinsi", _fundClient.Propinsi);
                        cmd.Parameters.AddWithValue("@TeleponSelular", _fundClient.TeleponSelular);
                        cmd.Parameters.AddWithValue("@Email", _fundClient.Email);
                        cmd.Parameters.AddWithValue("@Fax", _fundClient.Fax);
                        cmd.Parameters.AddWithValue("@DormantDate", _fundClient.DormantDate);
                        cmd.Parameters.AddWithValue("@Description", _fundClient.Description);
                        cmd.Parameters.AddWithValue("@JumlahBank", _fundClient.JumlahBank);
                        cmd.Parameters.AddWithValue("@NamaBank1", _fundClient.NamaBank1);
                        cmd.Parameters.AddWithValue("@NomorRekening1", _fundClient.NomorRekening1);

                        cmd.Parameters.AddWithValue("@NamaNasabah1", _fundClient.NamaNasabah1);
                        cmd.Parameters.AddWithValue("@MataUang1", _fundClient.MataUang1);
                        cmd.Parameters.AddWithValue("@OtherCurrency", _fundClient.OtherCurrency);
                        cmd.Parameters.AddWithValue("@NamaBank2", _fundClient.NamaBank2);
                        cmd.Parameters.AddWithValue("@NomorRekening2", _fundClient.NomorRekening2);

                        cmd.Parameters.AddWithValue("@NamaNasabah2", _fundClient.NamaNasabah2);
                        cmd.Parameters.AddWithValue("@MataUang2", _fundClient.MataUang2);
                        cmd.Parameters.AddWithValue("@NamaBank3", _fundClient.NamaBank3);
                        cmd.Parameters.AddWithValue("@NomorRekening3", _fundClient.NomorRekening3);

                        cmd.Parameters.AddWithValue("@NamaNasabah3", _fundClient.NamaNasabah3);
                        cmd.Parameters.AddWithValue("@MataUang3", _fundClient.MataUang3);
                        cmd.Parameters.AddWithValue("@IsFaceToFace", _fundClient.IsFaceToFace);

                        if (Tools.ClientCode == "10")
                        {
                            cmd.Parameters.AddWithValue("@KYCRiskProfile", 2);
                        }
                        else
                        {
                            if (_fundClient.KYCRiskProfile == 0)
                            {
                                cmd.Parameters.AddWithValue("@KYCRiskProfile", 0);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@KYCRiskProfile", _fundClient.KYCRiskProfile);
                            }
                        }

                        cmd.Parameters.AddWithValue("@NamaDepanInd", _fundClient.NamaDepanInd);
                        cmd.Parameters.AddWithValue("@NamaTengahInd", _fundClient.NamaTengahInd);
                        cmd.Parameters.AddWithValue("@NamaBelakangInd", _fundClient.NamaBelakangInd);
                        cmd.Parameters.AddWithValue("@TempatLahir", _fundClient.TempatLahir);
                        cmd.Parameters.AddWithValue("@TanggalLahir", _fundClient.TanggalLahir);
                        cmd.Parameters.AddWithValue("@JenisKelamin", _fundClient.JenisKelamin);
                        cmd.Parameters.AddWithValue("@StatusPerkawinan", _fundClient.StatusPerkawinan);
                        cmd.Parameters.AddWithValue("@Pekerjaan", _fundClient.Pekerjaan);
                        cmd.Parameters.AddWithValue("@OtherOccupation", _fundClient.OtherOccupation);
                        cmd.Parameters.AddWithValue("@OtherSpouseOccupation", _fundClient.OtherSpouseOccupation);
                        cmd.Parameters.AddWithValue("@Pendidikan", _fundClient.Pendidikan);
                        cmd.Parameters.AddWithValue("@OtherPendidikan", _fundClient.OtherPendidikan);
                        cmd.Parameters.AddWithValue("@Agama", _fundClient.Agama);
                        cmd.Parameters.AddWithValue("@OtherAgama", _fundClient.OtherAgama);
                        cmd.Parameters.AddWithValue("@PenghasilanInd", _fundClient.PenghasilanInd);
                        cmd.Parameters.AddWithValue("@SumberDanaInd", _fundClient.SumberDanaInd);
                        cmd.Parameters.AddWithValue("@OtherSourceOfFunds", _fundClient.OtherSourceOfFunds);
                        cmd.Parameters.AddWithValue("@CapitalPaidIn", _fundClient.CapitalPaidIn);

                        cmd.Parameters.AddWithValue("@MaksudTujuanInd", _fundClient.MaksudTujuanInd);
                        cmd.Parameters.AddWithValue("@OtherInvestmentObjectives", _fundClient.OtherInvestmentObjectives);
                        cmd.Parameters.AddWithValue("@AlamatInd1", _fundClient.AlamatInd1);
                        cmd.Parameters.AddWithValue("@KodeKotaInd1", _fundClient.KodeKotaInd1);
                        cmd.Parameters.AddWithValue("@KodePosInd1", _fundClient.KodePosInd1);
                        cmd.Parameters.AddWithValue("@AlamatInd2", _fundClient.AlamatInd2);
                        cmd.Parameters.AddWithValue("@KodeKotaInd2", _fundClient.KodeKotaInd2);
                        cmd.Parameters.AddWithValue("@KodePosInd2", _fundClient.KodePosInd2);
                        cmd.Parameters.AddWithValue("@NamaPerusahaan", _fundClient.NamaPerusahaan);
                        cmd.Parameters.AddWithValue("@Domisili", _fundClient.Domisili);
                        cmd.Parameters.AddWithValue("@Tipe", _fundClient.Tipe);
                        cmd.Parameters.AddWithValue("@OtherTipe", _fundClient.OtherTipe);
                        cmd.Parameters.AddWithValue("@Karakteristik", _fundClient.Karakteristik);
                        cmd.Parameters.AddWithValue("@OtherCharacteristic", _fundClient.OtherCharacteristic);
                        cmd.Parameters.AddWithValue("@NoSKD", _fundClient.NoSKD);
                        cmd.Parameters.AddWithValue("@PenghasilanInstitusi", _fundClient.PenghasilanInstitusi);
                        cmd.Parameters.AddWithValue("@SumberDanaInstitusi", _fundClient.SumberDanaInstitusi);
                        cmd.Parameters.AddWithValue("@OtherSourceOfFundsIns", _fundClient.OtherSourceOfFundsIns);
                        cmd.Parameters.AddWithValue("@MaksudTujuanInstitusi", _fundClient.MaksudTujuanInstitusi);
                        cmd.Parameters.AddWithValue("@OtherInvestmentObjectivesIns", _fundClient.OtherInvestmentObjectivesIns);
                        cmd.Parameters.AddWithValue("@AlamatPerusahaan", _fundClient.AlamatPerusahaan);
                        cmd.Parameters.AddWithValue("@KodeKotaIns", _fundClient.KodeKotaIns);
                        cmd.Parameters.AddWithValue("@KodePosIns", _fundClient.KodePosIns);
                        cmd.Parameters.AddWithValue("@SpouseName", _fundClient.SpouseName);
                        cmd.Parameters.AddWithValue("@MotherMaidenName", _fundClient.MotherMaidenName);
                        cmd.Parameters.AddWithValue("@AhliWaris", _fundClient.AhliWaris);
                        cmd.Parameters.AddWithValue("@HubunganAhliWaris", _fundClient.HubunganAhliWaris);
                        cmd.Parameters.AddWithValue("@NatureOfBusiness", _fundClient.NatureOfBusiness);
                        cmd.Parameters.AddWithValue("@NatureOfBusinessLainnya", _fundClient.NatureOfBusinessLainnya);
                        cmd.Parameters.AddWithValue("@Politis", _fundClient.Politis);
                        cmd.Parameters.AddWithValue("@PolitisRelation", _fundClient.PolitisRelation);
                        cmd.Parameters.AddWithValue("@PolitisLainnya", _fundClient.PolitisLainnya);
                        //if (Tools.ClientCode == "10")
                        //{
                        //    cmd.Parameters.AddWithValue("@PolitisName", _fundClient.PolitisName);
                        //}
                        //else
                        //{
                        //    cmd.Parameters.AddWithValue("@PolitisName","");
                        //}

                        //if (Tools.ClientCode == "10")
                        //{
                        //    cmd.Parameters.AddWithValue("@PolitisFT", _fundClient.PolitisFT);
                        //}
                        //else
                        //{
                        //    cmd.Parameters.AddWithValue("@PolitisFT","");
                        //}
                        cmd.Parameters.AddWithValue("@PolitisName", _fundClient.PolitisName);
                        cmd.Parameters.AddWithValue("@PolitisFT", _fundClient.PolitisFT);
                        cmd.Parameters.AddWithValue("@TeleponRumah", _fundClient.TeleponRumah);
                        cmd.Parameters.AddWithValue("@OtherAlamatInd1", _fundClient.OtherAlamatInd1);
                        cmd.Parameters.AddWithValue("@OtherKodeKotaInd1", _fundClient.OtherKodeKotaInd1);
                        cmd.Parameters.AddWithValue("@OtherKodePosInd1", _fundClient.OtherKodePosInd1);
                        cmd.Parameters.AddWithValue("@OtherPropinsiInd1", _fundClient.OtherPropinsiInd1);
                        cmd.Parameters.AddWithValue("@CountryOfBirth", _fundClient.CountryOfBirth);
                        cmd.Parameters.AddWithValue("@OtherNegaraInd1", _fundClient.OtherNegaraInd1);
                        cmd.Parameters.AddWithValue("@OtherAlamatInd2", _fundClient.OtherAlamatInd2);
                        cmd.Parameters.AddWithValue("@OtherKodeKotaInd2", _fundClient.OtherKodeKotaInd2);
                        cmd.Parameters.AddWithValue("@OtherKodePosInd2", _fundClient.OtherKodePosInd2);
                        cmd.Parameters.AddWithValue("@OtherPropinsiInd2", _fundClient.OtherPropinsiInd2);
                        cmd.Parameters.AddWithValue("@OtherNegaraInd2", _fundClient.OtherNegaraInd2);
                        cmd.Parameters.AddWithValue("@OtherAlamatInd3", _fundClient.OtherAlamatInd3);
                        cmd.Parameters.AddWithValue("@OtherKodeKotaInd3", _fundClient.OtherKodeKotaInd3);
                        cmd.Parameters.AddWithValue("@OtherKodePosInd3", _fundClient.OtherKodePosInd3);
                        cmd.Parameters.AddWithValue("@OtherPropinsiInd3", _fundClient.OtherPropinsiInd3);
                        cmd.Parameters.AddWithValue("@OtherNegaraInd3", _fundClient.OtherNegaraInd3);
                        cmd.Parameters.AddWithValue("@OtherTeleponRumah", _fundClient.OtherTeleponRumah);
                        cmd.Parameters.AddWithValue("@OtherTeleponSelular", _fundClient.OtherTeleponSelular);
                        cmd.Parameters.AddWithValue("@OtherEmail", _fundClient.OtherEmail);
                        cmd.Parameters.AddWithValue("@OtherFax", _fundClient.OtherFax);
                        cmd.Parameters.AddWithValue("@JumlahIdentitasInd", _fundClient.JumlahIdentitasInd);
                        cmd.Parameters.AddWithValue("@IdentitasInd1", _fundClient.IdentitasInd1);
                        cmd.Parameters.AddWithValue("@NoIdentitasInd1", _fundClient.NoIdentitasInd1);
                        cmd.Parameters.AddWithValue("@RegistrationDateIdentitasInd1", _fundClient.RegistrationDateIdentitasInd1);
                        cmd.Parameters.AddWithValue("@ExpiredDateIdentitasInd1", _fundClient.ExpiredDateIdentitasInd1);
                        cmd.Parameters.AddWithValue("@IdentitasInd2", _fundClient.IdentitasInd2);
                        cmd.Parameters.AddWithValue("@NoIdentitasInd2", _fundClient.NoIdentitasInd2);
                        cmd.Parameters.AddWithValue("@RegistrationDateIdentitasInd2", _fundClient.RegistrationDateIdentitasInd2);
                        cmd.Parameters.AddWithValue("@ExpiredDateIdentitasInd2", _fundClient.ExpiredDateIdentitasInd2);
                        cmd.Parameters.AddWithValue("@IdentitasInd3", _fundClient.IdentitasInd3);
                        cmd.Parameters.AddWithValue("@NoIdentitasInd3", _fundClient.NoIdentitasInd3);
                        cmd.Parameters.AddWithValue("@RegistrationDateIdentitasInd3", _fundClient.RegistrationDateIdentitasInd3);
                        cmd.Parameters.AddWithValue("@ExpiredDateIdentitasInd3", _fundClient.ExpiredDateIdentitasInd3);
                        cmd.Parameters.AddWithValue("@IdentitasInd4", _fundClient.IdentitasInd4);
                        cmd.Parameters.AddWithValue("@NoIdentitasInd4", _fundClient.NoIdentitasInd4);
                        cmd.Parameters.AddWithValue("@RegistrationDateIdentitasInd4", _fundClient.RegistrationDateIdentitasInd4);
                        cmd.Parameters.AddWithValue("@ExpiredDateIdentitasInd4", _fundClient.ExpiredDateIdentitasInd4);
                        cmd.Parameters.AddWithValue("@RegistrationNPWP", _fundClient.RegistrationNPWP);
                        cmd.Parameters.AddWithValue("@ExpiredDateSKD", _fundClient.ExpiredDateSKD);
                        cmd.Parameters.AddWithValue("@TanggalBerdiri", _fundClient.TanggalBerdiri);
                        cmd.Parameters.AddWithValue("@LokasiBerdiri", _fundClient.LokasiBerdiri);
                        cmd.Parameters.AddWithValue("@TeleponBisnis", _fundClient.TeleponBisnis);
                        cmd.Parameters.AddWithValue("@NomorAnggaran", _fundClient.NomorAnggaran);
                        cmd.Parameters.AddWithValue("@NomorSIUP", _fundClient.NomorSIUP);
                        cmd.Parameters.AddWithValue("@AssetFor1Year", _fundClient.AssetFor1Year);
                        cmd.Parameters.AddWithValue("@AssetFor2Year", _fundClient.AssetFor2Year);
                        cmd.Parameters.AddWithValue("@AssetFor3Year", _fundClient.AssetFor3Year);
                        cmd.Parameters.AddWithValue("@OperatingProfitFor1Year", _fundClient.OperatingProfitFor1Year);
                        cmd.Parameters.AddWithValue("@OperatingProfitFor2Year", _fundClient.OperatingProfitFor2Year);
                        cmd.Parameters.AddWithValue("@OperatingProfitFor3Year", _fundClient.OperatingProfitFor3Year);
                        cmd.Parameters.AddWithValue("@JumlahPejabat", _fundClient.JumlahPejabat);
                        cmd.Parameters.AddWithValue("@NamaDepanIns1", _fundClient.NamaDepanIns1);
                        cmd.Parameters.AddWithValue("@NamaTengahIns1", _fundClient.NamaTengahIns1);
                        cmd.Parameters.AddWithValue("@NamaBelakangIns1", _fundClient.NamaBelakangIns1);
                        cmd.Parameters.AddWithValue("@Jabatan1", _fundClient.Jabatan1);
                        cmd.Parameters.AddWithValue("@JumlahIdentitasIns1", _fundClient.JumlahIdentitasIns1);
                        cmd.Parameters.AddWithValue("@IdentitasIns11", _fundClient.IdentitasIns11);
                        cmd.Parameters.AddWithValue("@NoIdentitasIns11", _fundClient.NoIdentitasIns11);
                        cmd.Parameters.AddWithValue("@RegistrationDateIdentitasIns11", _fundClient.RegistrationDateIdentitasIns11);
                        cmd.Parameters.AddWithValue("@ExpiredDateIdentitasIns11", _fundClient.ExpiredDateIdentitasIns11);
                        cmd.Parameters.AddWithValue("@IdentitasIns12", _fundClient.IdentitasIns12);
                        cmd.Parameters.AddWithValue("@NoIdentitasIns12", _fundClient.NoIdentitasIns12);
                        cmd.Parameters.AddWithValue("@RegistrationDateIdentitasIns12", _fundClient.RegistrationDateIdentitasIns12);
                        cmd.Parameters.AddWithValue("@ExpiredDateIdentitasIns12", _fundClient.ExpiredDateIdentitasIns12);
                        cmd.Parameters.AddWithValue("@IdentitasIns13", _fundClient.IdentitasIns13);
                        cmd.Parameters.AddWithValue("@NoIdentitasIns13", _fundClient.NoIdentitasIns13);
                        cmd.Parameters.AddWithValue("@RegistrationDateIdentitasIns13", _fundClient.RegistrationDateIdentitasIns13);
                        cmd.Parameters.AddWithValue("@ExpiredDateIdentitasIns13", _fundClient.ExpiredDateIdentitasIns13);
                        cmd.Parameters.AddWithValue("@IdentitasIns14", _fundClient.IdentitasIns14);
                        cmd.Parameters.AddWithValue("@NoIdentitasIns14", _fundClient.NoIdentitasIns14);
                        cmd.Parameters.AddWithValue("@RegistrationDateIdentitasIns14", _fundClient.RegistrationDateIdentitasIns14);
                        cmd.Parameters.AddWithValue("@ExpiredDateIdentitasIns14", _fundClient.ExpiredDateIdentitasIns14);
                        cmd.Parameters.AddWithValue("@NamaDepanIns2", _fundClient.NamaDepanIns2);
                        cmd.Parameters.AddWithValue("@NamaTengahIns2", _fundClient.NamaTengahIns2);
                        cmd.Parameters.AddWithValue("@NamaBelakangIns2", _fundClient.NamaBelakangIns2);
                        cmd.Parameters.AddWithValue("@Jabatan2", _fundClient.Jabatan2);
                        cmd.Parameters.AddWithValue("@JumlahIdentitasIns2", _fundClient.JumlahIdentitasIns2);
                        cmd.Parameters.AddWithValue("@IdentitasIns21", _fundClient.IdentitasIns21);
                        cmd.Parameters.AddWithValue("@NoIdentitasIns21", _fundClient.NoIdentitasIns21);
                        cmd.Parameters.AddWithValue("@RegistrationDateIdentitasIns21", _fundClient.RegistrationDateIdentitasIns21);
                        cmd.Parameters.AddWithValue("@ExpiredDateIdentitasIns21", _fundClient.ExpiredDateIdentitasIns21);
                        cmd.Parameters.AddWithValue("@IdentitasIns22", _fundClient.IdentitasIns22);
                        cmd.Parameters.AddWithValue("@NoIdentitasIns22", _fundClient.NoIdentitasIns22);
                        cmd.Parameters.AddWithValue("@RegistrationDateIdentitasIns22", _fundClient.RegistrationDateIdentitasIns22);
                        cmd.Parameters.AddWithValue("@ExpiredDateIdentitasIns22", _fundClient.ExpiredDateIdentitasIns22);
                        cmd.Parameters.AddWithValue("@IdentitasIns23", _fundClient.IdentitasIns23);
                        cmd.Parameters.AddWithValue("@NoIdentitasIns23", _fundClient.NoIdentitasIns23);
                        cmd.Parameters.AddWithValue("@RegistrationDateIdentitasIns23", _fundClient.RegistrationDateIdentitasIns23);
                        cmd.Parameters.AddWithValue("@ExpiredDateIdentitasIns23", _fundClient.ExpiredDateIdentitasIns23);
                        cmd.Parameters.AddWithValue("@IdentitasIns24", _fundClient.IdentitasIns24);
                        cmd.Parameters.AddWithValue("@NoIdentitasIns24", _fundClient.NoIdentitasIns24);
                        cmd.Parameters.AddWithValue("@RegistrationDateIdentitasIns24", _fundClient.RegistrationDateIdentitasIns24);
                        cmd.Parameters.AddWithValue("@ExpiredDateIdentitasIns24", _fundClient.ExpiredDateIdentitasIns24);
                        cmd.Parameters.AddWithValue("@NamaDepanIns3", _fundClient.NamaDepanIns3);
                        cmd.Parameters.AddWithValue("@NamaTengahIns3", _fundClient.NamaTengahIns3);
                        cmd.Parameters.AddWithValue("@NamaBelakangIns3", _fundClient.NamaBelakangIns3);
                        cmd.Parameters.AddWithValue("@Jabatan3", _fundClient.Jabatan3);
                        cmd.Parameters.AddWithValue("@JumlahIdentitasIns3", _fundClient.JumlahIdentitasIns3);
                        cmd.Parameters.AddWithValue("@IdentitasIns31", _fundClient.IdentitasIns31);
                        cmd.Parameters.AddWithValue("@NoIdentitasIns31", _fundClient.NoIdentitasIns31);
                        cmd.Parameters.AddWithValue("@RegistrationDateIdentitasIns31", _fundClient.RegistrationDateIdentitasIns31);
                        cmd.Parameters.AddWithValue("@ExpiredDateIdentitasIns31", _fundClient.ExpiredDateIdentitasIns31);
                        cmd.Parameters.AddWithValue("@IdentitasIns32", _fundClient.IdentitasIns32);
                        cmd.Parameters.AddWithValue("@NoIdentitasIns32", _fundClient.NoIdentitasIns32);
                        cmd.Parameters.AddWithValue("@RegistrationDateIdentitasIns32", _fundClient.RegistrationDateIdentitasIns32);
                        cmd.Parameters.AddWithValue("@ExpiredDateIdentitasIns32", _fundClient.ExpiredDateIdentitasIns32);
                        cmd.Parameters.AddWithValue("@IdentitasIns33", _fundClient.IdentitasIns33);
                        cmd.Parameters.AddWithValue("@NoIdentitasIns33", _fundClient.NoIdentitasIns33);
                        cmd.Parameters.AddWithValue("@RegistrationDateIdentitasIns33", _fundClient.RegistrationDateIdentitasIns33);
                        cmd.Parameters.AddWithValue("@ExpiredDateIdentitasIns33", _fundClient.ExpiredDateIdentitasIns33);
                        cmd.Parameters.AddWithValue("@IdentitasIns34", _fundClient.IdentitasIns34);
                        cmd.Parameters.AddWithValue("@NoIdentitasIns34", _fundClient.NoIdentitasIns34);
                        cmd.Parameters.AddWithValue("@RegistrationDateIdentitasIns34", _fundClient.RegistrationDateIdentitasIns34);
                        cmd.Parameters.AddWithValue("@ExpiredDateIdentitasIns34", _fundClient.ExpiredDateIdentitasIns34);
                        cmd.Parameters.AddWithValue("@NamaDepanIns4", _fundClient.NamaDepanIns4);
                        cmd.Parameters.AddWithValue("@NamaTengahIns4", _fundClient.NamaTengahIns4);
                        cmd.Parameters.AddWithValue("@NamaBelakangIns4", _fundClient.NamaBelakangIns4);
                        cmd.Parameters.AddWithValue("@Jabatan4", _fundClient.Jabatan4);
                        cmd.Parameters.AddWithValue("@JumlahIdentitasIns4", _fundClient.JumlahIdentitasIns4);
                        cmd.Parameters.AddWithValue("@IdentitasIns41", _fundClient.IdentitasIns41);
                        cmd.Parameters.AddWithValue("@NoIdentitasIns41", _fundClient.NoIdentitasIns41);
                        cmd.Parameters.AddWithValue("@RegistrationDateIdentitasIns41", _fundClient.RegistrationDateIdentitasIns41);
                        cmd.Parameters.AddWithValue("@ExpiredDateIdentitasIns41", _fundClient.ExpiredDateIdentitasIns41);
                        cmd.Parameters.AddWithValue("@IdentitasIns42", _fundClient.IdentitasIns42);
                        cmd.Parameters.AddWithValue("@NoIdentitasIns42", _fundClient.NoIdentitasIns42);
                        cmd.Parameters.AddWithValue("@RegistrationDateIdentitasIns42", _fundClient.RegistrationDateIdentitasIns42);
                        cmd.Parameters.AddWithValue("@ExpiredDateIdentitasIns42", _fundClient.ExpiredDateIdentitasIns42);
                        cmd.Parameters.AddWithValue("@IdentitasIns43", _fundClient.IdentitasIns43);
                        cmd.Parameters.AddWithValue("@NoIdentitasIns43", _fundClient.NoIdentitasIns43);
                        cmd.Parameters.AddWithValue("@RegistrationDateIdentitasIns43", _fundClient.RegistrationDateIdentitasIns43);
                        cmd.Parameters.AddWithValue("@ExpiredDateIdentitasIns43", _fundClient.ExpiredDateIdentitasIns43);
                        cmd.Parameters.AddWithValue("@IdentitasIns44", _fundClient.IdentitasIns44);
                        cmd.Parameters.AddWithValue("@NoIdentitasIns44", _fundClient.NoIdentitasIns44);
                        cmd.Parameters.AddWithValue("@RegistrationDateIdentitasIns44", _fundClient.RegistrationDateIdentitasIns44);
                        cmd.Parameters.AddWithValue("@ExpiredDateIdentitasIns44", _fundClient.ExpiredDateIdentitasIns44);

                        cmd.Parameters.AddWithValue("@PhoneIns1", _fundClient.@PhoneIns1);
                        cmd.Parameters.AddWithValue("@EmailIns1", _fundClient.@EmailIns1);
                        cmd.Parameters.AddWithValue("@PhoneIns2", _fundClient.PhoneIns2);
                        cmd.Parameters.AddWithValue("@EmailIns2", _fundClient.EmailIns2);
                        cmd.Parameters.AddWithValue("@InvestorsRiskProfile", _fundClient.InvestorsRiskProfile);
                        cmd.Parameters.AddWithValue("@AssetOwner", _fundClient.AssetOwner);
                        cmd.Parameters.AddWithValue("@StatementType", _fundClient.StatementType);
                        cmd.Parameters.AddWithValue("@FATCA", _fundClient.FATCA);
                        cmd.Parameters.AddWithValue("@TIN", _fundClient.TIN);
                        cmd.Parameters.AddWithValue("@TINIssuanceCountry", _fundClient.TINIssuanceCountry);
                        cmd.Parameters.AddWithValue("@GIIN", _fundClient.GIIN);
                        cmd.Parameters.AddWithValue("@SubstantialOwnerName", _fundClient.SubstantialOwnerName);
                        cmd.Parameters.AddWithValue("@SubstantialOwnerAddress", _fundClient.SubstantialOwnerAddress);
                        cmd.Parameters.AddWithValue("@SubstantialOwnerTIN", _fundClient.SubstantialOwnerTIN);
                        cmd.Parameters.AddWithValue("@BankBranchName1", _fundClient.BankBranchName1);
                        cmd.Parameters.AddWithValue("@BankBranchName2", _fundClient.BankBranchName2);
                        cmd.Parameters.AddWithValue("@BankBranchName3", _fundClient.BankBranchName3);
                        cmd.Parameters.AddWithValue("@BitDefaultPayment1", _fundClient.BitDefaultPayment1);
                        cmd.Parameters.AddWithValue("@BitDefaultPayment2", _fundClient.BitDefaultPayment2);
                        cmd.Parameters.AddWithValue("@BitDefaultPayment3", _fundClient.BitDefaultPayment3);

                        cmd.Parameters.AddWithValue("@AlamatKantorInd ", _fundClient.AlamatKantorInd);
                        cmd.Parameters.AddWithValue("@KodeKotaKantorInd ", _fundClient.KodeKotaKantorInd);
                        cmd.Parameters.AddWithValue("@KodePosKantorInd ", _fundClient.KodePosKantorInd);
                        cmd.Parameters.AddWithValue("@KodePropinsiKantorInd ", _fundClient.KodePropinsiKantorInd);
                        cmd.Parameters.AddWithValue("@KodeCountryofKantor ", _fundClient.KodeCountryofKantor);
                        cmd.Parameters.AddWithValue("@CorrespondenceRT ", _fundClient.CorrespondenceRT);
                        cmd.Parameters.AddWithValue("@DomicileRT ", _fundClient.DomicileRT);
                        cmd.Parameters.AddWithValue("@DomicileRW ", _fundClient.DomicileRW);
                        cmd.Parameters.AddWithValue("@Identity1RT ", _fundClient.Identity1RT);
                        cmd.Parameters.AddWithValue("@Identity1RW ", _fundClient.Identity1RW);
                        cmd.Parameters.AddWithValue("@KodeDomisiliPropinsi ", _fundClient.KodeDomisiliPropinsi);
                        cmd.Parameters.AddWithValue("@CorrespondenceRW ", _fundClient.CorrespondenceRW);

                        cmd.Parameters.AddWithValue("@CountryofCorrespondence", _fundClient.CountryofCorrespondence);
                        cmd.Parameters.AddWithValue("@CountryofDomicile", _fundClient.CountryofDomicile);
                        cmd.Parameters.AddWithValue("@SIUPExpirationDate", _fundClient.SIUPExpirationDate);
                        cmd.Parameters.AddWithValue("@CountryofEstablishment", _fundClient.CountryofEstablishment);
                        cmd.Parameters.AddWithValue("@CompanyCityName", _fundClient.CompanyCityName);
                        cmd.Parameters.AddWithValue("@CountryofCompany", _fundClient.CountryofCompany);
                        cmd.Parameters.AddWithValue("@NPWPPerson1", _fundClient.NPWPPerson1);
                        cmd.Parameters.AddWithValue("@NPWPPerson2", _fundClient.NPWPPerson2);

                        cmd.Parameters.AddWithValue("@NamaKantor", _fundClient.NamaKantor);
                        cmd.Parameters.AddWithValue("@JabatanKantor", _fundClient.JabatanKantor);

                        cmd.Parameters.AddWithValue("@BankCountry1", _fundClient.BankCountry1);
                        cmd.Parameters.AddWithValue("@BankCountry2", _fundClient.BankCountry2);
                        cmd.Parameters.AddWithValue("@BankCountry3", _fundClient.BankCountry3);
                        // RDN
                        cmd.Parameters.AddWithValue("@BankRDNPK", _fundClient.BankRDNPK);
                        cmd.Parameters.AddWithValue("@RDNAccountNo", _fundClient.RDNAccountNo);
                        cmd.Parameters.AddWithValue("@RDNAccountName", _fundClient.RDNAccountName);
                        cmd.Parameters.AddWithValue("@RDNBankBranchName", _fundClient.RDNBankBranchName);
                        cmd.Parameters.AddWithValue("@RDNCurrency", _fundClient.RDNCurrency);
                        cmd.Parameters.AddWithValue("@RiskProfileScore", _fundClient.RiskProfileScore);

                        //SPOUSE
                        cmd.Parameters.AddWithValue("@SpouseBirthPlace", _fundClient.SpouseBirthPlace);
                        cmd.Parameters.AddWithValue("@SpouseDateOfBirth", _fundClient.SpouseDateOfBirth);
                        cmd.Parameters.AddWithValue("@SpouseOccupation", _fundClient.SpouseOccupation);
                        cmd.Parameters.AddWithValue("@SpouseNatureOfBusiness", _fundClient.SpouseNatureOfBusiness);
                        cmd.Parameters.AddWithValue("@SpouseNatureOfBusinessOther", _fundClient.SpouseNatureOfBusinessOther);
                        cmd.Parameters.AddWithValue("@SpouseIDNo", _fundClient.SpouseIDNo);
                        cmd.Parameters.AddWithValue("@SpouseNationality", _fundClient.SpouseNationality);
                        cmd.Parameters.AddWithValue("@SpouseAnnualIncome", _fundClient.SpouseAnnualIncome);

                        cmd.Parameters.AddWithValue("@CompanyFax", _fundClient.CompanyFax);
                        cmd.Parameters.AddWithValue("@CompanyMail", _fundClient.CompanyMail);
                        cmd.Parameters.AddWithValue("@SegmentClass", _fundClient.SegmentClass);
                        cmd.Parameters.AddWithValue("@MigrationStatus", _fundClient.MigrationStatus);
                        cmd.Parameters.AddWithValue("@CompanyTypeOJK", _fundClient.CompanyTypeOJK);
                        cmd.Parameters.AddWithValue("@Legality", _fundClient.Legality);
                        cmd.Parameters.AddWithValue("@RenewingDate", _fundClient.RenewingDate);
                        cmd.Parameters.AddWithValue("@BitShareAbleToGroup", _fundClient.BitShareAbleToGroup);
                        cmd.Parameters.AddWithValue("@RemarkBank1", _fundClient.RemarkBank1);
                        cmd.Parameters.AddWithValue("@RemarkBank2", _fundClient.RemarkBank2);
                        cmd.Parameters.AddWithValue("@RemarkBank3", _fundClient.RemarkBank3);
                        cmd.Parameters.AddWithValue("@CantSubs", _fundClient.CantSubs);
                        cmd.Parameters.AddWithValue("@CantRedempt", _fundClient.CantRedempt);
                        cmd.Parameters.AddWithValue("@CantSwitch", _fundClient.CantSwitch);

                        cmd.Parameters.AddWithValue("@BeneficialName", _fundClient.BeneficialName);
                        cmd.Parameters.AddWithValue("@BeneficialAddress", _fundClient.BeneficialAddress);
                        cmd.Parameters.AddWithValue("@BeneficialIdentity", _fundClient.BeneficialIdentity);
                        cmd.Parameters.AddWithValue("@BeneficialWork", _fundClient.BeneficialWork);
                        cmd.Parameters.AddWithValue("@BeneficialRelation", _fundClient.BeneficialRelation);
                        cmd.Parameters.AddWithValue("@BeneficialHomeNo", _fundClient.BeneficialHomeNo);
                        cmd.Parameters.AddWithValue("@BeneficialPhoneNumber", _fundClient.BeneficialPhoneNumber);
                        cmd.Parameters.AddWithValue("@BeneficialNPWP", _fundClient.BeneficialNPWP);

                        cmd.Parameters.AddWithValue("@ClientOnBoard", _fundClient.ClientOnBoard);
                        cmd.Parameters.AddWithValue("@Referral", _fundClient.Referral);
                        cmd.Parameters.AddWithValue("@BitisTA", _fundClient.BitisTA);

                        cmd.Parameters.AddWithValue("@AlamatOfficer1", _fundClient.AlamatOfficer1);
                        cmd.Parameters.AddWithValue("@AlamatOfficer2", _fundClient.AlamatOfficer2);
                        cmd.Parameters.AddWithValue("@AlamatOfficer3", _fundClient.AlamatOfficer3);
                        cmd.Parameters.AddWithValue("@AlamatOfficer4", _fundClient.AlamatOfficer4);

                        cmd.Parameters.AddWithValue("@AgamaOfficer1", _fundClient.AgamaOfficer1);
                        cmd.Parameters.AddWithValue("@AgamaOfficer2", _fundClient.AgamaOfficer2);
                        cmd.Parameters.AddWithValue("@AgamaOfficer3", _fundClient.AgamaOfficer3);
                        cmd.Parameters.AddWithValue("@AgamaOfficer4", _fundClient.AgamaOfficer4);

                        cmd.Parameters.AddWithValue("@PlaceOfBirthOfficer1", _fundClient.PlaceOfBirthOfficer1);
                        cmd.Parameters.AddWithValue("@PlaceOfBirthOfficer2", _fundClient.PlaceOfBirthOfficer2);
                        cmd.Parameters.AddWithValue("@PlaceOfBirthOfficer3", _fundClient.PlaceOfBirthOfficer3);
                        cmd.Parameters.AddWithValue("@PlaceOfBirthOfficer4", _fundClient.PlaceOfBirthOfficer4);

                        cmd.Parameters.AddWithValue("@DOBOfficer1", _fundClient.DOBOfficer1);
                        cmd.Parameters.AddWithValue("@DOBOfficer2", _fundClient.DOBOfficer2);
                        cmd.Parameters.AddWithValue("@DOBOfficer3", _fundClient.DOBOfficer3);
                        cmd.Parameters.AddWithValue("@DOBOfficer4", _fundClient.DOBOfficer4);

                        if (_fundClient.FrontID == "" || _fundClient.FrontID == null)
                        {
                            cmd.Parameters.AddWithValue("@FrontID", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@FrontID", _fundClient.FrontID);
                        }
                        cmd.Parameters.AddWithValue("@FaceToFaceDate", _fundClient.FaceToFaceDate);
                        cmd.Parameters.AddWithValue("@EmployerLineOfBusiness", _fundClient.EmployerLineOfBusiness);


                        if (_fundClient.TeleponKantor == "" || _fundClient.TeleponKantor == null)
                        {
                            cmd.Parameters.AddWithValue("@TeleponKantor", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@TeleponKantor", _fundClient.TeleponKantor);
                        }

                        if (_fundClient.NationalityOfficer1 == "" || _fundClient.NationalityOfficer1 == null)
                        {
                            cmd.Parameters.AddWithValue("@NationalityOfficer1", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@NationalityOfficer1", _fundClient.NationalityOfficer1);
                        }

                        if (_fundClient.NationalityOfficer2 == "" || _fundClient.NationalityOfficer2 == null)
                        {
                            cmd.Parameters.AddWithValue("@NationalityOfficer2", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@NationalityOfficer2", _fundClient.NationalityOfficer2);
                        }

                        if (_fundClient.NationalityOfficer3 == "" || _fundClient.NationalityOfficer3 == null)
                        {
                            cmd.Parameters.AddWithValue("@NationalityOfficer3", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@NationalityOfficer3", _fundClient.NationalityOfficer3);
                        }

                        if (_fundClient.NationalityOfficer4 == "" || _fundClient.NationalityOfficer4 == null)
                        {
                            cmd.Parameters.AddWithValue("@NationalityOfficer4", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@NationalityOfficer4", _fundClient.NationalityOfficer4);
                        }
                        //-------

                        cmd.Parameters.AddWithValue("@IdentityTypeOfficer1", _fundClient.IdentityTypeOfficer1);
                        cmd.Parameters.AddWithValue("@IdentityTypeOfficer2", _fundClient.IdentityTypeOfficer2);
                        cmd.Parameters.AddWithValue("@IdentityTypeOfficer3", _fundClient.IdentityTypeOfficer3);
                        cmd.Parameters.AddWithValue("@IdentityTypeOfficer4", _fundClient.IdentityTypeOfficer4);
                        cmd.Parameters.AddWithValue("@NoIdentitasOfficer1", _fundClient.NoIdentitasOfficer1);
                        cmd.Parameters.AddWithValue("@NoIdentitasOfficer2", _fundClient.NoIdentitasOfficer2);
                        cmd.Parameters.AddWithValue("@NoIdentitasOfficer3", _fundClient.NoIdentitasOfficer3);
                        cmd.Parameters.AddWithValue("@NoIdentitasOfficer4", _fundClient.NoIdentitasOfficer4);

                        cmd.Parameters.AddWithValue("@EntryUsersID", _fundClient.EntryUsersID);
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

        public int FundClient_Update(FundClient _fundClient, bool _havePrivillege)
        {

            try
            {
                int _newHisPK;
                int status = _host.Get_Status(_fundClient.FundClientPK, _fundClient.HistoryPK, "FundClient");
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {

                            cmd.CommandText = @"Update FundClient set status=2,Notes=@Notes,ID=@ID,Name=@Name, ClientCategory=@ClientCategory ,InvestorType=@InvestorType ,InternalCategoryPK=@InternalCategoryPK ,
                            SellingAgentPK=@SellingAgentPK ,UsersPK=@UsersPK,SID=@SID ,IFUACode=@IFUACode , ARIA=@ARIA ,Registered=@Registered ,Negara=@Negara ,Nationality=@Nationality ,NPWP=@NPWP, SACode=@SACode,
                            Propinsi=@Propinsi ,TeleponSelular=@TeleponSelular,Email=@Email ,Fax=@Fax ,DormantDate=@DormantDate ,Description=@Description , 
                            NamaBank1=@NamaBank1 , NomorRekening1=@NomorRekening1 ,NamaNasabah1=@NamaNasabah1 ,MataUang1=@MataUang1 ,OtherCurrency=@,
                            NamaBank2=@NamaBank2 , NomorRekening2=@NomorRekening2 ,NamaNasabah2=@NamaNasabah2 ,MataUang2=@MataUang2 ,
                            NamaBank3=@NamaBank3 , NomorRekening3=@NomorRekening3 ,NamaNasabah3=@NamaNasabah3 ,MataUang3=@MataUang3 ,
                            NamaDepanInd=@NamaDepanInd ,  NamaTengahInd=@NamaTengahInd ,NamaBelakangInd=@NamaBelakangInd ,TempatLahir=@TempatLahir ,TanggalLahir=@TanggalLahir ,JenisKelamin=@JenisKelamin ,  
                            StatusPerkawinan=@StatusPerkawinan ,Pekerjaan=@Pekerjaan , OtherOccupation=@OtherOccupation,Pendidikan=@Pendidikan, OtherPendidikan=@OtherPendidikan ,Agama=@Agama , OtherAgama=@OtherAgama, PenghasilanInd=@PenghasilanInd ,  
                            SumberDanaInd=@SumberDanaInd ,OtherSourceOfFunds=@OtherSourceOfFunds,CapitalPaidIn=@CapitalPaidIn ,MaksudTujuanInd=@MaksudTujuanInd , OtherInvestmentObjectives=@OtherInvestmentObjectives,AlamatInd1=@AlamatInd1 ,KodeKotaInd1=@KodeKotaInd1 ,KodePosInd1=@KodePosInd1 ,  
                            AlamatInd2=@AlamatInd2 ,KodeKotaInd2=@KodeKotaInd2 ,KodePosInd2=@KodePosInd2 ,NamaPerusahaan=@NamaPerusahaan ,Domisili=@Domisili ,  
                            Tipe=@Tipe ,OtherTipe=@OtherTipe ,Karakteristik=@Karakteristik, OtherCharacteristic=@OtherCharacteristic ,NoSKD=@NoSKD ,PenghasilanInstitusi=@PenghasilanInstitusi ,SumberDanaInstitusi=@SumberDanaInstitusi ,  OtherSourceOfFundsIns=@OtherSourceOfFundsIns,
                            MaksudTujuanInstitusi=@MaksudTujuanInstitusi, OtherInvestmentObjectivesIns=@OtherInvestmentObjectivesIns ,AlamatPerusahaan=@AlamatPerusahaan ,KodeKotaIns=@KodeKotaIns ,KodePosIns=@KodePosIns ,SpouseName=@SpouseName ,MotherMaidenName=@MotherMaidenName,  
                            AhliWaris=@AhliWaris ,HubunganAhliWaris=@HubunganAhliWaris ,NatureOfBusiness=@NatureOfBusiness ,NatureOfBusinessLainnya=@NatureOfBusinessLainnya ,Politis=@Politis ,  
                            PolitisLainnya=@PolitisLainnya ,PolitisName=@PolitisName ,PolitisFT=@PolitisFT ,PolitisRelation=@PolitisRelation ,TeleponRumah=@TeleponRumah ,OtherAlamatInd1=@OtherAlamatInd1 ,OtherKodeKotaInd1=@OtherKodeKotaInd1 ,OtherKodePosInd1=@OtherKodePosInd1 ,  
                            OtherPropinsiInd1=@OtherPropinsiInd1, CountryOfBirth=@CountryOfBirth ,OtherNegaraInd1=@OtherNegaraInd1 ,OtherAlamatInd2=@OtherAlamatInd2 ,OtherKodeKotaInd2=@OtherKodeKotaInd2 ,OtherKodePosInd2=@OtherKodePosInd2 ,  
                            OtherPropinsiInd2=@OtherPropinsiInd2 ,OtherNegaraInd2=@OtherNegaraInd2 ,OtherAlamatInd3=@OtherAlamatInd3 ,OtherKodeKotaInd3=@OtherKodeKotaInd3 ,OtherKodePosInd3=@OtherKodePosInd3 ,  
                            OtherPropinsiInd3=@OtherPropinsiInd3 ,OtherNegaraInd3=@OtherNegaraInd3 ,OtherTeleponRumah=@OtherTeleponRumah ,OtherTeleponSelular=@OtherTeleponSelular ,OtherEmail=@OtherEmail ,  
                            OtherFax=@OtherFax ,IdentitasInd1=@IdentitasInd1 ,NoIdentitasInd1=@NoIdentitasInd1 ,RegistrationDateIdentitasInd1=@RegistrationDateIdentitasInd1 ,  
                            ExpiredDateIdentitasInd1=@ExpiredDateIdentitasInd1 ,IdentitasInd2=@IdentitasInd2 ,NoIdentitasInd2=@NoIdentitasInd2 ,RegistrationDateIdentitasInd2=@RegistrationDateIdentitasInd2 ,ExpiredDateIdentitasInd2=@ExpiredDateIdentitasInd2 ,  
                            IdentitasInd3=@IdentitasInd3 ,NoIdentitasInd3=@NoIdentitasInd3 ,RegistrationDateIdentitasInd3=@RegistrationDateIdentitasInd3 ,ExpiredDateIdentitasInd3=@ExpiredDateIdentitasInd3 ,IdentitasInd4=@IdentitasInd4 ,  
                            NoIdentitasInd4=@NoIdentitasInd4 ,RegistrationDateIdentitasInd4=@RegistrationDateIdentitasInd4 ,ExpiredDateIdentitasInd4=@ExpiredDateIdentitasInd4 ,RegistrationNPWP=@RegistrationNPWP ,  
                            ExpiredDateSKD=@ExpiredDateSKD ,TanggalBerdiri=@TanggalBerdiri ,LokasiBerdiri=@LokasiBerdiri ,TeleponBisnis=@TeleponBisnis ,NomorAnggaran=@NomorAnggaran ,  
                            NomorSIUP=@NomorSIUP ,AssetFor1Year=@AssetFor1Year ,AssetFor2Year=@AssetFor2Year ,AssetFor3Year=@AssetFor3Year ,OperatingProfitFor1Year=@OperatingProfitFor1Year ,  
                            OperatingProfitFor2Year=@OperatingProfitFor2Year ,OperatingProfitFor3Year=@OperatingProfitFor3Year ,NamaDepanIns1=@NamaDepanIns1 ,NamaTengahIns1=@NamaTengahIns1 ,  
                            NamaBelakangIns1=@NamaBelakangIns1 ,Jabatan1=@Jabatan1 ,IdentitasIns11=@IdentitasIns11 ,NoIdentitasIns11=@NoIdentitasIns11 ,  
                            RegistrationDateIdentitasIns11=@RegistrationDateIdentitasIns11 ,ExpiredDateIdentitasIns11=@ExpiredDateIdentitasIns11 ,IdentitasIns12=@IdentitasIns12 ,NoIdentitasIns12=@NoIdentitasIns12 ,RegistrationDateIdentitasIns12=@RegistrationDateIdentitasIns12 ,  
                            ExpiredDateIdentitasIns12=@ExpiredDateIdentitasIns12 ,IdentitasIns13=@IdentitasIns13 ,NoIdentitasIns13=@NoIdentitasIns13 ,RegistrationDateIdentitasIns13=@RegistrationDateIdentitasIns13 ,ExpiredDateIdentitasIns13=@ExpiredDateIdentitasIns13 ,  
                            IdentitasIns14=@IdentitasIns14 ,NoIdentitasIns14=@NoIdentitasIns14 ,RegistrationDateIdentitasIns14=@RegistrationDateIdentitasIns14 ,ExpiredDateIdentitasIns14=@ExpiredDateIdentitasIns14 ,NamaDepanIns2=@NamaDepanIns2 ,  
                            NamaTengahIns2=@NamaTengahIns2 ,NamaBelakangIns2=@NamaBelakangIns2 ,Jabatan2=@Jabatan2 ,IdentitasIns21=@IdentitasIns21 ,  
                            NoIdentitasIns21=@NoIdentitasIns21 ,RegistrationDateIdentitasIns21=@RegistrationDateIdentitasIns21 ,ExpiredDateIdentitasIns21=@ExpiredDateIdentitasIns21 ,IdentitasIns22=@IdentitasIns22 ,NoIdentitasIns22=@NoIdentitasIns22 ,  
                            RegistrationDateIdentitasIns22=@RegistrationDateIdentitasIns22 ,ExpiredDateIdentitasIns22=@ExpiredDateIdentitasIns22 ,IdentitasIns23=@IdentitasIns23 ,NoIdentitasIns23=@NoIdentitasIns23 ,RegistrationDateIdentitasIns23=@RegistrationDateIdentitasIns23 ,  
                            ExpiredDateIdentitasIns23=@ExpiredDateIdentitasIns23 ,IdentitasIns24=@IdentitasIns24 ,NoIdentitasIns24=@NoIdentitasIns24 ,RegistrationDateIdentitasIns24=@RegistrationDateIdentitasIns24 ,ExpiredDateIdentitasIns24=@ExpiredDateIdentitasIns24 ,  
                            NamaDepanIns3=@NamaDepanIns3 ,NamaTengahIns3=@NamaTengahIns3 ,NamaBelakangIns3=@NamaBelakangIns3 ,Jabatan3=@Jabatan3 ,JumlahIdentitasIns3=@JumlahIdentitasIns3 ,  
                            IdentitasIns31=@IdentitasIns31 ,NoIdentitasIns31=@NoIdentitasIns31 ,RegistrationDateIdentitasIns31=@RegistrationDateIdentitasIns31 ,ExpiredDateIdentitasIns31=@ExpiredDateIdentitasIns31 ,IdentitasIns32=@IdentitasIns32 ,  
                            NoIdentitasIns32=@NoIdentitasIns32 ,RegistrationDateIdentitasIns32=@RegistrationDateIdentitasIns32 ,ExpiredDateIdentitasIns32=@ExpiredDateIdentitasIns32 ,IdentitasIns33=@IdentitasIns33 ,NoIdentitasIns33=@NoIdentitasIns33 ,  
                            RegistrationDateIdentitasIns33=@RegistrationDateIdentitasIns33 ,ExpiredDateIdentitasIns33=@ExpiredDateIdentitasIns33 ,IdentitasIns34=@IdentitasIns34 ,NoIdentitasIns34=@NoIdentitasIns34 ,RegistrationDateIdentitasIns34=@RegistrationDateIdentitasIns34 ,  
                            ExpiredDateIdentitasIns34=@ExpiredDateIdentitasIns34 ,NamaDepanIns4=@NamaDepanIns4 ,NamaTengahIns4=@NamaTengahIns4 ,NamaBelakangIns4=@NamaBelakangIns4 ,Jabatan4=@Jabatan4 ,  
                            JumlahIdentitasIns4=@JumlahIdentitasIns4 ,IdentitasIns41=@IdentitasIns41 ,NoIdentitasIns41=@NoIdentitasIns41 ,RegistrationDateIdentitasIns41=@RegistrationDateIdentitasIns41 ,ExpiredDateIdentitasIns41=@ExpiredDateIdentitasIns41 ,  
                            IdentitasIns42=@IdentitasIns42 ,NoIdentitasIns42=@NoIdentitasIns42 ,RegistrationDateIdentitasIns42=@RegistrationDateIdentitasIns42 ,ExpiredDateIdentitasIns42=@ExpiredDateIdentitasIns42 ,IdentitasIns43=@IdentitasIns43 ,  
                            NoIdentitasIns43=@NoIdentitasIns43 ,RegistrationDateIdentitasIns43=@RegistrationDateIdentitasIns43 ,ExpiredDateIdentitasIns43=@ExpiredDateIdentitasIns43 ,IdentitasIns44=@IdentitasIns44 ,NoIdentitasIns44=@NoIdentitasIns44 ,  
                            RegistrationDateIdentitasIns44=@RegistrationDateIdentitasIns44 ,ExpiredDateIdentitasIns44=@ExpiredDateIdentitasIns44 ,
                            PhoneIns1=@PhoneIns1,EmailIns1=@EmailIns1,PhoneIns2=@PhoneIns2,EmailIns2=@EmailIns2,InvestorsRiskProfile=@InvestorsRiskProfile,AssetOwner=@AssetOwner,StatementType=@StatementType,FATCA=@FATCA,TIN=@TIN, 
                            TINIssuanceCountry=@TINIssuanceCountry,GIIN=@GIIN,SubstantialOwnerName=@SubstantialOwnerName,SubstantialOwnerAddress=@SubstantialOwnerAddress,SubstantialOwnerTIN=@SubstantialOwnerTIN, 
                            BankBranchName1=@BankBranchName1,BankBranchName2=@BankBranchName2,BankBranchName3=@BankBranchName3, 
                            CountryofCorrespondence=@CountryofCorrespondence, CountryofDomicile=@CountryofDomicile, 
                            SIUPExpirationDate=@SIUPExpirationDate, CountryofEstablishment=@CountryofEstablishment, CompanyCityName=@CompanyCityName, 
                            CountryofCompany=@CountryofCompany, NPWPPerson1=@NPWPPerson1, NPWPPerson2=@NPWPPerson2,BankRDNPK = @BankRDNPK,RDNAccountNo = @RDNAccountNo,RDNAccountName = @RDNAccountName,RiskProfileScore = @RiskProfileScore,RDNBankBranchName=@RDNBankBranchName,RDNCurrency=@RDNCurrency,  
                            SpouseBirthPlace=@SpouseBirthPlace,SpouseDateOfBirth=@SpouseDateOfBirth,SpouseOccupation=@SpouseOccupation,OtherSpouseOccupation=@OtherSpouseOccupation,SpouseNatureOfBusiness=@SpouseNatureOfBusiness,SpouseNatureOfBusinessOther=@SpouseNatureOfBusinessOther,
                            SpouseIDNo=@SpouseIDNo,SpouseNationality=@SpouseNationality,SpouseAnnualIncome=@SpouseAnnualIncome,IsFaceToFace=@IsFaceToFace,KYCRiskProfile=@KYCRiskProfile,BitDefaultPayment1=@BitDefaultPayment1,BitDefaultPayment2=@BitDefaultPayment2,BitDefaultPayment3=@BitDefaultPayment3
                            ,AlamatKantorInd=@AlamatKantorInd,KodeKotaKantorInd=@KodeKotaKantorInd,KodePosKantorInd=@KodePosKantorInd,KodePropinsiKantorInd=@KodePropinsiKantorInd,KodeCountryofKantor=@KodeCountryofKantor,CorrespondenceRT=@CorrespondenceRT,CorrespondenceRW=@CorrespondenceRW,DomicileRT=@DomicileRT,
                            DomicileRW=@DomicileRW,Identity1RT=@Identity1RT,Identity1RW=@Identity1RW,KodeDomisiliPropinsi=@KodeDomisiliPropinsi,BIMemberCode1=@BIMemberCode1,BIMemberCode2=@BIMemberCode2,BIMemberCode3=@BIMemberCode3,BICCode1Name=@BICCode1Name,BICCode2Name=@BICCode2Name,BICCode3Name=@BICCode3Name, 
                            CompanyFax = @CompanyFax,@CompanyMail = CompanyMail, MigrationStatus = @MigrationStatus, SegmentClass= @SegmentClass, CompanyTypeOJK= @CompanyTypeOJK, Legality = @Legality, RenewingDate=@RenewingDate, BitShareAbleToGroup=@BitShareAbleToGroup,RemarkBank1=@RemarkBank1,RemarkBank2=@RemarkBank2,RemarkBank3=@RemarkBank3,
                            CantSubs=@CantSubs,CantRedempt=@CantRedempt,CantSwitch=@CantSwitch,BeneficialName=@BeneficialName,BeneficialAddress=@BeneficialAddress,BeneficialIdentity=@BeneficialIdentity,BeneficialWork=@BeneficialWork,BeneficialRelation=@BeneficialRelation,BeneficialHomeNo=@BeneficialHomeNo,BeneficialPhoneNumber=@BeneficialPhoneNumber,BeneficialNPWP=@BeneficialNPWP,ClientOnBoard=@ClientOnBoard,Referral=@Referral,ReferralIDFO=@ReferralIDFO,
                            AlamatOfficer1=@AlamatOfficer1,AlamatOfficer2=@AlamatOfficer2,AlamatOfficer3=@AlamatOfficer3,AlamatOfficer4=@AlamatOfficer4,AgamaOfficer1=@AgamaOfficer1,AgamaOfficer2=@AgamaOfficer2,AgamaOfficer3=@AgamaOfficer3,AgamaOfficer4=@AgamaOfficer4,PlaceOfBirthOfficer1=@PlaceOfBirthOfficer1,PlaceOfBirthOfficer2=@PlaceOfBirthOfficer2,PlaceOfBirthOfficer3=@PlaceOfBirthOfficer3,PlaceOfBirthOfficer4=@PlaceOfBirthOfficer4,DOBOfficer1=@DOBOfficer1,DOBOfficer2=@DOBOfficer2,DOBOfficer3=@DOBOfficer3,DOBOfficer4=@DOBOfficer4,FaceToFaceDate=@FaceToFaceDate,EmployerLineOfBusiness=@EmployerLineOfBusiness,FrontID=@FrontID,
                            TeleponKantor=@TeleponKantor,NationalityOfficer1=@NationalityOfficer1,NationalityOfficer2=@NationalityOfficer2,NationalityOfficer3=@NationalityOfficer3,NationalityOfficer4=@NationalityOfficer4,
                            IdentityTypeOfficer1=@IdentityTypeOfficer1,IdentityTypeOfficer2=@IdentityTypeOfficer2,IdentityTypeOfficer3=@IdentityTypeOfficer3,IdentityTypeOfficer4=@IdentityTypeOfficer4,
                            NoIdentitasOfficer1=@NoIdentitasOfficer1,NoIdentitasOfficer2=@NoIdentitasOfficer2,NoIdentitasOfficer3=@NoIdentitasOfficer3,NoIdentitasOfficer4=@NoIdentitasOfficer4,
                            ApprovedUsersID=@ApprovedUsersID,  
                            ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime ,LastUpdate=@LastUpdate  
                            where FundClientPK = @PK and historyPK = @HistoryPK ";

                            cmd.Parameters.AddWithValue("@HistoryPK", _fundClient.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _fundClient.FundClientPK);
                            cmd.Parameters.AddWithValue("@ID", _fundClient.ID);
                            cmd.Parameters.AddWithValue("@Notes", _fundClient.Notes);
                            cmd.Parameters.AddWithValue("@Name", _fundClient.Name);
                            cmd.Parameters.AddWithValue("@ClientCategory", _fundClient.ClientCategory);
                            cmd.Parameters.AddWithValue("@InvestorType", _fundClient.InvestorType);
                            cmd.Parameters.AddWithValue("@InternalCategoryPK", _fundClient.InternalCategoryPK);
                            cmd.Parameters.AddWithValue("@SellingAgentPK", _fundClient.SellingAgentPK);
                            cmd.Parameters.AddWithValue("@UsersPK", _fundClient.UsersPK);
                            cmd.Parameters.AddWithValue("@SID", _fundClient.SID);
                            cmd.Parameters.AddWithValue("@IFUACode", _fundClient.IFUACode);
                            //cmd.Parameters.AddWithValue("@Child", _fundClient.Child);
                            cmd.Parameters.AddWithValue("@ARIA", _fundClient.ARIA);
                            cmd.Parameters.AddWithValue("@Registered", _fundClient.Registered);
                            //cmd.Parameters.AddWithValue("@JumlahDanaAwal", _fundClient.JumlahDanaAwal);
                            //cmd.Parameters.AddWithValue("@JumlahDanaSaatIniCash", _fundClient.JumlahDanaSaatIniCash);
                            //cmd.Parameters.AddWithValue("@JumlahDanaSaatIni", _fundClient.JumlahDanaSaatIni);
                            cmd.Parameters.AddWithValue("@Negara", _fundClient.Negara);
                            cmd.Parameters.AddWithValue("@Nationality", _fundClient.Nationality);
                            cmd.Parameters.AddWithValue("@NPWP", _fundClient.NPWP);
                            cmd.Parameters.AddWithValue("@SACode", _fundClient.SACode);
                            cmd.Parameters.AddWithValue("@Propinsi", _fundClient.Propinsi);
                            cmd.Parameters.AddWithValue("@TeleponSelular", _fundClient.TeleponSelular);
                            cmd.Parameters.AddWithValue("@Email", _fundClient.Email);
                            cmd.Parameters.AddWithValue("@Fax", _fundClient.Fax);
                            cmd.Parameters.AddWithValue("@DormantDate", _fundClient.DormantDate);
                            cmd.Parameters.AddWithValue("@Description", _fundClient.Description);
                            //cmd.Parameters.AddWithValue("@JumlahBank", _fundClient.JumlahBank);
                            cmd.Parameters.AddWithValue("@NamaBank1", _fundClient.NamaBank1);
                            cmd.Parameters.AddWithValue("@NomorRekening1", _fundClient.NomorRekening1);

                            cmd.Parameters.AddWithValue("@NamaNasabah1", _fundClient.NamaNasabah1);
                            cmd.Parameters.AddWithValue("@MataUang1", _fundClient.MataUang1);
                            cmd.Parameters.AddWithValue("@OtherCurrency", _fundClient.OtherCurrency);
                            cmd.Parameters.AddWithValue("@NamaBank2", _fundClient.NamaBank2);
                            cmd.Parameters.AddWithValue("@NomorRekening2", _fundClient.NomorRekening2);

                            cmd.Parameters.AddWithValue("@NamaNasabah2", _fundClient.NamaNasabah2);
                            cmd.Parameters.AddWithValue("@MataUang2", _fundClient.MataUang2);
                            cmd.Parameters.AddWithValue("@NamaBank3", _fundClient.NamaBank3);
                            cmd.Parameters.AddWithValue("@NomorRekening3", _fundClient.NomorRekening3);

                            cmd.Parameters.AddWithValue("@NamaNasabah3", _fundClient.NamaNasabah3);
                            cmd.Parameters.AddWithValue("@MataUang3", _fundClient.MataUang3);
                            cmd.Parameters.AddWithValue("@IsFaceToFace", _fundClient.IsFaceToFace);
                            cmd.Parameters.AddWithValue("@KYCRiskProfile", _fundClient.KYCRiskProfile);

                            cmd.Parameters.AddWithValue("@NamaDepanInd", _fundClient.NamaDepanInd);
                            cmd.Parameters.AddWithValue("@NamaTengahInd", _fundClient.NamaTengahInd);
                            cmd.Parameters.AddWithValue("@NamaBelakangInd", _fundClient.NamaBelakangInd);
                            cmd.Parameters.AddWithValue("@TempatLahir", _fundClient.TempatLahir);
                            cmd.Parameters.AddWithValue("@TanggalLahir", _fundClient.TanggalLahir);
                            cmd.Parameters.AddWithValue("@JenisKelamin", _fundClient.JenisKelamin);
                            cmd.Parameters.AddWithValue("@StatusPerkawinan", _fundClient.StatusPerkawinan);
                            cmd.Parameters.AddWithValue("@Pekerjaan", _fundClient.Pekerjaan);
                            cmd.Parameters.AddWithValue("@OtherOccupation", _fundClient.OtherOccupation);
                            cmd.Parameters.AddWithValue("@Pendidikan", _fundClient.Pendidikan);
                            cmd.Parameters.AddWithValue("@OtherPendidikan", _fundClient.OtherPendidikan);
                            cmd.Parameters.AddWithValue("@Agama", _fundClient.Agama);
                            cmd.Parameters.AddWithValue("@OtherAgama", _fundClient.OtherAgama);
                            cmd.Parameters.AddWithValue("@PenghasilanInd", _fundClient.PenghasilanInd);
                            cmd.Parameters.AddWithValue("@SumberDanaInd", _fundClient.SumberDanaInd);
                            cmd.Parameters.AddWithValue("@OtherSourceOfFunds", _fundClient.OtherSourceOfFunds);
                            cmd.Parameters.AddWithValue("@CapitalPaidIn", _fundClient.CapitalPaidIn);
                            cmd.Parameters.AddWithValue("@MaksudTujuanInd", _fundClient.MaksudTujuanInd);
                            cmd.Parameters.AddWithValue("@OtherInvestmentObjectives", _fundClient.OtherInvestmentObjectives);
                            cmd.Parameters.AddWithValue("@AlamatInd1", _fundClient.AlamatInd1);
                            cmd.Parameters.AddWithValue("@KodeKotaInd1", _fundClient.KodeKotaInd1);
                            cmd.Parameters.AddWithValue("@KodePosInd1", _fundClient.KodePosInd1);
                            cmd.Parameters.AddWithValue("@AlamatInd2", _fundClient.AlamatInd2);
                            cmd.Parameters.AddWithValue("@KodeKotaInd2", _fundClient.KodeKotaInd2);
                            cmd.Parameters.AddWithValue("@KodePosInd2", _fundClient.KodePosInd2);
                            cmd.Parameters.AddWithValue("@NamaPerusahaan", _fundClient.NamaPerusahaan);
                            cmd.Parameters.AddWithValue("@Domisili", _fundClient.Domisili);
                            cmd.Parameters.AddWithValue("@Tipe", _fundClient.Tipe);
                            cmd.Parameters.AddWithValue("@OtherTipe", _fundClient.OtherTipe);
                            cmd.Parameters.AddWithValue("@Karakteristik", _fundClient.Karakteristik);
                            cmd.Parameters.AddWithValue("@OtherCharacteristic", _fundClient.OtherCharacteristic);
                            cmd.Parameters.AddWithValue("@NoSKD", _fundClient.NoSKD);
                            cmd.Parameters.AddWithValue("@PenghasilanInstitusi", _fundClient.PenghasilanInstitusi);
                            cmd.Parameters.AddWithValue("@SumberDanaInstitusi", _fundClient.SumberDanaInstitusi);
                            cmd.Parameters.AddWithValue("@OtherSourceOfFundsIns", _fundClient.OtherSourceOfFundsIns);
                            cmd.Parameters.AddWithValue("@MaksudTujuanInstitusi", _fundClient.MaksudTujuanInstitusi);
                            cmd.Parameters.AddWithValue("@OtherInvestmentObjectivesIns", _fundClient.OtherInvestmentObjectivesIns);
                            cmd.Parameters.AddWithValue("@AlamatPerusahaan", _fundClient.AlamatPerusahaan);
                            cmd.Parameters.AddWithValue("@KodeKotaIns", _fundClient.KodeKotaIns);
                            cmd.Parameters.AddWithValue("@KodePosIns", _fundClient.KodePosIns);
                            cmd.Parameters.AddWithValue("@SpouseName", _fundClient.SpouseName);
                            cmd.Parameters.AddWithValue("@MotherMaidenName", _fundClient.MotherMaidenName);
                            cmd.Parameters.AddWithValue("@AhliWaris", _fundClient.AhliWaris);
                            cmd.Parameters.AddWithValue("@HubunganAhliWaris", _fundClient.HubunganAhliWaris);
                            cmd.Parameters.AddWithValue("@NatureOfBusiness", _fundClient.NatureOfBusiness);
                            cmd.Parameters.AddWithValue("@NatureOfBusinessLainnya", _fundClient.NatureOfBusinessLainnya);
                            cmd.Parameters.AddWithValue("@Politis", _fundClient.Politis);
                            cmd.Parameters.AddWithValue("@PolitisRelation", _fundClient.PolitisRelation);
                            cmd.Parameters.AddWithValue("@PolitisLainnya", _fundClient.PolitisLainnya);
                            cmd.Parameters.AddWithValue("@PolitisName", _fundClient.PolitisName);
                            cmd.Parameters.AddWithValue("@PolitisFT", _fundClient.PolitisFT);

                            cmd.Parameters.AddWithValue("@TeleponRumah", _fundClient.TeleponRumah);
                            cmd.Parameters.AddWithValue("@OtherAlamatInd1", _fundClient.OtherAlamatInd1);
                            cmd.Parameters.AddWithValue("@OtherKodeKotaInd1", _fundClient.OtherKodeKotaInd1);
                            cmd.Parameters.AddWithValue("@OtherKodePosInd1", _fundClient.OtherKodePosInd1);
                            cmd.Parameters.AddWithValue("@OtherPropinsiInd1", _fundClient.OtherPropinsiInd1);
                            cmd.Parameters.AddWithValue("@CountryOfBirth", _fundClient.CountryOfBirth);
                            cmd.Parameters.AddWithValue("@OtherNegaraInd1", _fundClient.OtherNegaraInd1);
                            cmd.Parameters.AddWithValue("@OtherAlamatInd2", _fundClient.OtherAlamatInd2);
                            cmd.Parameters.AddWithValue("@OtherKodeKotaInd2", _fundClient.OtherKodeKotaInd2);
                            cmd.Parameters.AddWithValue("@OtherKodePosInd2", _fundClient.OtherKodePosInd2);
                            cmd.Parameters.AddWithValue("@OtherPropinsiInd2", _fundClient.OtherPropinsiInd2);
                            cmd.Parameters.AddWithValue("@OtherNegaraInd2", _fundClient.OtherNegaraInd2);
                            cmd.Parameters.AddWithValue("@OtherAlamatInd3", _fundClient.OtherAlamatInd3);
                            cmd.Parameters.AddWithValue("@OtherKodeKotaInd3", _fundClient.OtherKodeKotaInd3);
                            cmd.Parameters.AddWithValue("@OtherKodePosInd3", _fundClient.OtherKodePosInd3);
                            cmd.Parameters.AddWithValue("@OtherPropinsiInd3", _fundClient.OtherPropinsiInd3);
                            cmd.Parameters.AddWithValue("@OtherNegaraInd3", _fundClient.OtherNegaraInd3);
                            cmd.Parameters.AddWithValue("@OtherTeleponRumah", _fundClient.OtherTeleponRumah);
                            cmd.Parameters.AddWithValue("@OtherTeleponSelular", _fundClient.OtherTeleponSelular);
                            cmd.Parameters.AddWithValue("@OtherEmail", _fundClient.OtherEmail);
                            cmd.Parameters.AddWithValue("@OtherFax", _fundClient.OtherFax);
                            cmd.Parameters.AddWithValue("@JumlahIdentitasInd", _fundClient.JumlahIdentitasInd);
                            cmd.Parameters.AddWithValue("@IdentitasInd1", _fundClient.IdentitasInd1);
                            cmd.Parameters.AddWithValue("@NoIdentitasInd1", _fundClient.NoIdentitasInd1);
                            cmd.Parameters.AddWithValue("@RegistrationDateIdentitasInd1", _fundClient.RegistrationDateIdentitasInd1);
                            cmd.Parameters.AddWithValue("@ExpiredDateIdentitasInd1", _fundClient.ExpiredDateIdentitasInd1);
                            cmd.Parameters.AddWithValue("@IdentitasInd2", _fundClient.IdentitasInd2);
                            cmd.Parameters.AddWithValue("@NoIdentitasInd2", _fundClient.NoIdentitasInd2);
                            cmd.Parameters.AddWithValue("@RegistrationDateIdentitasInd2", _fundClient.RegistrationDateIdentitasInd2);
                            cmd.Parameters.AddWithValue("@ExpiredDateIdentitasInd2", _fundClient.ExpiredDateIdentitasInd2);
                            cmd.Parameters.AddWithValue("@IdentitasInd3", _fundClient.IdentitasInd3);
                            cmd.Parameters.AddWithValue("@NoIdentitasInd3", _fundClient.NoIdentitasInd3);
                            cmd.Parameters.AddWithValue("@RegistrationDateIdentitasInd3", _fundClient.RegistrationDateIdentitasInd3);
                            cmd.Parameters.AddWithValue("@ExpiredDateIdentitasInd3", _fundClient.ExpiredDateIdentitasInd3);
                            cmd.Parameters.AddWithValue("@IdentitasInd4", _fundClient.IdentitasInd4);
                            cmd.Parameters.AddWithValue("@NoIdentitasInd4", _fundClient.NoIdentitasInd4);
                            cmd.Parameters.AddWithValue("@RegistrationDateIdentitasInd4", _fundClient.RegistrationDateIdentitasInd4);
                            cmd.Parameters.AddWithValue("@ExpiredDateIdentitasInd4", _fundClient.ExpiredDateIdentitasInd4);
                            cmd.Parameters.AddWithValue("@RegistrationNPWP", _fundClient.RegistrationNPWP);
                            cmd.Parameters.AddWithValue("@ExpiredDateSKD", _fundClient.ExpiredDateSKD);
                            cmd.Parameters.AddWithValue("@TanggalBerdiri", _fundClient.TanggalBerdiri);
                            cmd.Parameters.AddWithValue("@LokasiBerdiri", _fundClient.LokasiBerdiri);
                            cmd.Parameters.AddWithValue("@TeleponBisnis", _fundClient.TeleponBisnis);
                            cmd.Parameters.AddWithValue("@NomorAnggaran", _fundClient.NomorAnggaran);
                            cmd.Parameters.AddWithValue("@NomorSIUP", _fundClient.NomorSIUP);
                            cmd.Parameters.AddWithValue("@AssetFor1Year", _fundClient.AssetFor1Year);
                            cmd.Parameters.AddWithValue("@AssetFor2Year", _fundClient.AssetFor2Year);
                            cmd.Parameters.AddWithValue("@AssetFor3Year", _fundClient.AssetFor3Year);
                            cmd.Parameters.AddWithValue("@OperatingProfitFor1Year", _fundClient.OperatingProfitFor1Year);
                            cmd.Parameters.AddWithValue("@OperatingProfitFor2Year", _fundClient.OperatingProfitFor2Year);
                            cmd.Parameters.AddWithValue("@OperatingProfitFor3Year", _fundClient.OperatingProfitFor3Year);
                            cmd.Parameters.AddWithValue("@JumlahPejabat", _fundClient.JumlahPejabat);
                            cmd.Parameters.AddWithValue("@NamaDepanIns1", _fundClient.NamaDepanIns1);
                            cmd.Parameters.AddWithValue("@NamaTengahIns1", _fundClient.NamaTengahIns1);
                            cmd.Parameters.AddWithValue("@NamaBelakangIns1", _fundClient.NamaBelakangIns1);
                            cmd.Parameters.AddWithValue("@Jabatan1", _fundClient.Jabatan1);
                            //cmd.Parameters.AddWithValue("@JumlahIdentitasIns1", _fundClient.JumlahIdentitasIns1);
                            cmd.Parameters.AddWithValue("@IdentitasIns11", _fundClient.IdentitasIns11);
                            cmd.Parameters.AddWithValue("@NoIdentitasIns11", _fundClient.NoIdentitasIns11);
                            cmd.Parameters.AddWithValue("@RegistrationDateIdentitasIns11", _fundClient.RegistrationDateIdentitasIns11);
                            cmd.Parameters.AddWithValue("@ExpiredDateIdentitasIns11", _fundClient.ExpiredDateIdentitasIns11);
                            cmd.Parameters.AddWithValue("@IdentitasIns12", _fundClient.IdentitasIns12);
                            cmd.Parameters.AddWithValue("@NoIdentitasIns12", _fundClient.NoIdentitasIns12);
                            cmd.Parameters.AddWithValue("@RegistrationDateIdentitasIns12", _fundClient.RegistrationDateIdentitasIns12);
                            cmd.Parameters.AddWithValue("@ExpiredDateIdentitasIns12", _fundClient.ExpiredDateIdentitasIns12);
                            cmd.Parameters.AddWithValue("@IdentitasIns13", _fundClient.IdentitasIns13);
                            cmd.Parameters.AddWithValue("@NoIdentitasIns13", _fundClient.NoIdentitasIns13);
                            cmd.Parameters.AddWithValue("@RegistrationDateIdentitasIns13", _fundClient.RegistrationDateIdentitasIns13);
                            cmd.Parameters.AddWithValue("@ExpiredDateIdentitasIns13", _fundClient.ExpiredDateIdentitasIns13);
                            cmd.Parameters.AddWithValue("@IdentitasIns14", _fundClient.IdentitasIns14);
                            cmd.Parameters.AddWithValue("@NoIdentitasIns14", _fundClient.NoIdentitasIns14);
                            cmd.Parameters.AddWithValue("@RegistrationDateIdentitasIns14", _fundClient.RegistrationDateIdentitasIns14);
                            cmd.Parameters.AddWithValue("@ExpiredDateIdentitasIns14", _fundClient.ExpiredDateIdentitasIns14);
                            cmd.Parameters.AddWithValue("@NamaDepanIns2", _fundClient.NamaDepanIns2);
                            cmd.Parameters.AddWithValue("@NamaTengahIns2", _fundClient.NamaTengahIns2);
                            cmd.Parameters.AddWithValue("@NamaBelakangIns2", _fundClient.NamaBelakangIns2);
                            cmd.Parameters.AddWithValue("@Jabatan2", _fundClient.Jabatan2);
                            //cmd.Parameters.AddWithValue("@JumlahIdentitasIns2", _fundClient.JumlahIdentitasIns2);
                            cmd.Parameters.AddWithValue("@IdentitasIns21", _fundClient.IdentitasIns21);
                            cmd.Parameters.AddWithValue("@NoIdentitasIns21", _fundClient.NoIdentitasIns21);
                            cmd.Parameters.AddWithValue("@RegistrationDateIdentitasIns21", _fundClient.RegistrationDateIdentitasIns21);
                            cmd.Parameters.AddWithValue("@ExpiredDateIdentitasIns21", _fundClient.ExpiredDateIdentitasIns21);
                            cmd.Parameters.AddWithValue("@IdentitasIns22", _fundClient.IdentitasIns22);
                            cmd.Parameters.AddWithValue("@NoIdentitasIns22", _fundClient.NoIdentitasIns22);
                            cmd.Parameters.AddWithValue("@RegistrationDateIdentitasIns22", _fundClient.RegistrationDateIdentitasIns22);
                            cmd.Parameters.AddWithValue("@ExpiredDateIdentitasIns22", _fundClient.ExpiredDateIdentitasIns22);
                            cmd.Parameters.AddWithValue("@IdentitasIns23", _fundClient.IdentitasIns23);
                            cmd.Parameters.AddWithValue("@NoIdentitasIns23", _fundClient.NoIdentitasIns23);
                            cmd.Parameters.AddWithValue("@RegistrationDateIdentitasIns23", _fundClient.RegistrationDateIdentitasIns23);
                            cmd.Parameters.AddWithValue("@ExpiredDateIdentitasIns23", _fundClient.ExpiredDateIdentitasIns23);
                            cmd.Parameters.AddWithValue("@IdentitasIns24", _fundClient.IdentitasIns24);
                            cmd.Parameters.AddWithValue("@NoIdentitasIns24", _fundClient.NoIdentitasIns24);
                            cmd.Parameters.AddWithValue("@RegistrationDateIdentitasIns24", _fundClient.RegistrationDateIdentitasIns24);
                            cmd.Parameters.AddWithValue("@ExpiredDateIdentitasIns24", _fundClient.ExpiredDateIdentitasIns24);
                            cmd.Parameters.AddWithValue("@NamaDepanIns3", _fundClient.NamaDepanIns3);
                            cmd.Parameters.AddWithValue("@NamaTengahIns3", _fundClient.NamaTengahIns3);
                            cmd.Parameters.AddWithValue("@NamaBelakangIns3", _fundClient.NamaBelakangIns3);
                            cmd.Parameters.AddWithValue("@Jabatan3", _fundClient.Jabatan3);
                            cmd.Parameters.AddWithValue("@JumlahIdentitasIns3", _fundClient.JumlahIdentitasIns3);
                            cmd.Parameters.AddWithValue("@IdentitasIns31", _fundClient.IdentitasIns31);
                            cmd.Parameters.AddWithValue("@NoIdentitasIns31", _fundClient.NoIdentitasIns31);
                            cmd.Parameters.AddWithValue("@RegistrationDateIdentitasIns31", _fundClient.RegistrationDateIdentitasIns31);
                            cmd.Parameters.AddWithValue("@ExpiredDateIdentitasIns31", _fundClient.ExpiredDateIdentitasIns31);
                            cmd.Parameters.AddWithValue("@IdentitasIns32", _fundClient.IdentitasIns32);
                            cmd.Parameters.AddWithValue("@NoIdentitasIns32", _fundClient.NoIdentitasIns32);
                            cmd.Parameters.AddWithValue("@RegistrationDateIdentitasIns32", _fundClient.RegistrationDateIdentitasIns32);
                            cmd.Parameters.AddWithValue("@ExpiredDateIdentitasIns32", _fundClient.ExpiredDateIdentitasIns32);
                            cmd.Parameters.AddWithValue("@IdentitasIns33", _fundClient.IdentitasIns33);
                            cmd.Parameters.AddWithValue("@NoIdentitasIns33", _fundClient.NoIdentitasIns33);
                            cmd.Parameters.AddWithValue("@RegistrationDateIdentitasIns33", _fundClient.RegistrationDateIdentitasIns33);
                            cmd.Parameters.AddWithValue("@ExpiredDateIdentitasIns33", _fundClient.ExpiredDateIdentitasIns33);
                            cmd.Parameters.AddWithValue("@IdentitasIns34", _fundClient.IdentitasIns34);
                            cmd.Parameters.AddWithValue("@NoIdentitasIns34", _fundClient.NoIdentitasIns34);
                            cmd.Parameters.AddWithValue("@RegistrationDateIdentitasIns34", _fundClient.RegistrationDateIdentitasIns34);
                            cmd.Parameters.AddWithValue("@ExpiredDateIdentitasIns34", _fundClient.ExpiredDateIdentitasIns34);
                            cmd.Parameters.AddWithValue("@NamaDepanIns4", _fundClient.NamaDepanIns4);
                            cmd.Parameters.AddWithValue("@NamaTengahIns4", _fundClient.NamaTengahIns4);
                            cmd.Parameters.AddWithValue("@NamaBelakangIns4", _fundClient.NamaBelakangIns4);
                            cmd.Parameters.AddWithValue("@Jabatan4", _fundClient.Jabatan4);
                            cmd.Parameters.AddWithValue("@JumlahIdentitasIns4", _fundClient.JumlahIdentitasIns4);
                            cmd.Parameters.AddWithValue("@IdentitasIns41", _fundClient.IdentitasIns41);
                            cmd.Parameters.AddWithValue("@NoIdentitasIns41", _fundClient.NoIdentitasIns41);
                            cmd.Parameters.AddWithValue("@RegistrationDateIdentitasIns41", _fundClient.RegistrationDateIdentitasIns41);
                            cmd.Parameters.AddWithValue("@ExpiredDateIdentitasIns41", _fundClient.ExpiredDateIdentitasIns41);
                            cmd.Parameters.AddWithValue("@IdentitasIns42", _fundClient.IdentitasIns42);
                            cmd.Parameters.AddWithValue("@NoIdentitasIns42", _fundClient.NoIdentitasIns42);
                            cmd.Parameters.AddWithValue("@RegistrationDateIdentitasIns42", _fundClient.RegistrationDateIdentitasIns42);
                            cmd.Parameters.AddWithValue("@ExpiredDateIdentitasIns42", _fundClient.ExpiredDateIdentitasIns42);
                            cmd.Parameters.AddWithValue("@IdentitasIns43", _fundClient.IdentitasIns43);
                            cmd.Parameters.AddWithValue("@NoIdentitasIns43", _fundClient.NoIdentitasIns43);
                            cmd.Parameters.AddWithValue("@RegistrationDateIdentitasIns43", _fundClient.RegistrationDateIdentitasIns43);
                            cmd.Parameters.AddWithValue("@ExpiredDateIdentitasIns43", _fundClient.ExpiredDateIdentitasIns43);
                            cmd.Parameters.AddWithValue("@IdentitasIns44", _fundClient.IdentitasIns44);
                            cmd.Parameters.AddWithValue("@NoIdentitasIns44", _fundClient.NoIdentitasIns44);
                            cmd.Parameters.AddWithValue("@RegistrationDateIdentitasIns44", _fundClient.RegistrationDateIdentitasIns44);
                            cmd.Parameters.AddWithValue("@ExpiredDateIdentitasIns44", _fundClient.ExpiredDateIdentitasIns44);

                            cmd.Parameters.AddWithValue("@PhoneIns1", _fundClient.@PhoneIns1);
                            cmd.Parameters.AddWithValue("@EmailIns1", _fundClient.@EmailIns1);
                            cmd.Parameters.AddWithValue("@PhoneIns2", _fundClient.PhoneIns2);
                            cmd.Parameters.AddWithValue("@EmailIns2", _fundClient.EmailIns2);
                            cmd.Parameters.AddWithValue("@InvestorsRiskProfile", _fundClient.InvestorsRiskProfile);
                            cmd.Parameters.AddWithValue("@AssetOwner", _fundClient.AssetOwner);
                            cmd.Parameters.AddWithValue("@StatementType", _fundClient.StatementType);
                            cmd.Parameters.AddWithValue("@FATCA", _fundClient.FATCA);
                            cmd.Parameters.AddWithValue("@TIN", _fundClient.TIN);
                            cmd.Parameters.AddWithValue("@TINIssuanceCountry", _fundClient.TINIssuanceCountry);
                            cmd.Parameters.AddWithValue("@GIIN", _fundClient.GIIN);
                            cmd.Parameters.AddWithValue("@SubstantialOwnerName", _fundClient.SubstantialOwnerName);
                            cmd.Parameters.AddWithValue("@SubstantialOwnerAddress", _fundClient.SubstantialOwnerAddress);
                            cmd.Parameters.AddWithValue("@SubstantialOwnerTIN", _fundClient.SubstantialOwnerTIN);
                            cmd.Parameters.AddWithValue("@BankBranchName1", _fundClient.BankBranchName1);
                            cmd.Parameters.AddWithValue("@BankBranchName2", _fundClient.BankBranchName2);
                            cmd.Parameters.AddWithValue("@BankBranchName3", _fundClient.BankBranchName3);
                            cmd.Parameters.AddWithValue("@BankCountry1", _fundClient.BankCountry1);
                            cmd.Parameters.AddWithValue("@BankCountry2", _fundClient.BankCountry2);
                            cmd.Parameters.AddWithValue("@BankCountry3", _fundClient.BankCountry3);
                            cmd.Parameters.AddWithValue("@BitDefaultPayment1", _fundClient.BitDefaultPayment1);
                            cmd.Parameters.AddWithValue("@BitDefaultPayment2", _fundClient.BitDefaultPayment2);
                            cmd.Parameters.AddWithValue("@BitDefaultPayment3", _fundClient.BitDefaultPayment3);

                            cmd.Parameters.AddWithValue("@AlamatKantorInd", _fundClient.AlamatKantorInd);
                            cmd.Parameters.AddWithValue("@KodeKotaKantorInd", _fundClient.KodeKotaKantorInd);
                            cmd.Parameters.AddWithValue("@KodePosKantorInd", _fundClient.KodePosKantorInd);
                            cmd.Parameters.AddWithValue("@KodePropinsiKantorInd", _fundClient.KodePropinsiKantorInd);
                            cmd.Parameters.AddWithValue("@KodeCountryofKantor", _fundClient.KodeCountryofKantor);
                            cmd.Parameters.AddWithValue("@CorrespondenceRW", _fundClient.CorrespondenceRW);
                            cmd.Parameters.AddWithValue("@CorrespondenceRT", _fundClient.CorrespondenceRT);
                            cmd.Parameters.AddWithValue("@DomicileRT", _fundClient.DomicileRT);
                            cmd.Parameters.AddWithValue("@DomicileRW", _fundClient.DomicileRW);
                            cmd.Parameters.AddWithValue("@Identity1RT", _fundClient.Identity1RT);
                            cmd.Parameters.AddWithValue("@Identity1RW", _fundClient.Identity1RW);
                            cmd.Parameters.AddWithValue("@KodeDomisiliPropinsi", _fundClient.KodeDomisiliPropinsi);

                            cmd.Parameters.AddWithValue("@CountryofCorrespondence", _fundClient.CountryofCorrespondence);
                            cmd.Parameters.AddWithValue("@CountryofDomicile", _fundClient.CountryofDomicile);
                            cmd.Parameters.AddWithValue("@SIUPExpirationDate", _fundClient.SIUPExpirationDate);
                            cmd.Parameters.AddWithValue("@CountryofEstablishment", _fundClient.CountryofEstablishment);
                            cmd.Parameters.AddWithValue("@CompanyCityName", _fundClient.CompanyCityName);
                            cmd.Parameters.AddWithValue("@CountryofCompany", _fundClient.CountryofCompany);
                            cmd.Parameters.AddWithValue("@NPWPPerson1", _fundClient.NPWPPerson1);
                            cmd.Parameters.AddWithValue("@NPWPPerson2", _fundClient.NPWPPerson2);

                            // RDN
                            cmd.Parameters.AddWithValue("@BankRDNPK", _fundClient.BankRDNPK);
                            cmd.Parameters.AddWithValue("@RDNAccountNo", _fundClient.RDNAccountNo);
                            cmd.Parameters.AddWithValue("@RDNAccountName", _fundClient.RDNAccountName);
                            cmd.Parameters.AddWithValue("@RDNBankBranchName", _fundClient.RDNBankBranchName);
                            cmd.Parameters.AddWithValue("@RDNCurrency", _fundClient.RDNCurrency);

                            cmd.Parameters.AddWithValue("@RiskProfileScore", _fundClient.RiskProfileScore);
                            cmd.Parameters.AddWithValue("@NamaKantor", _fundClient.NamaKantor);
                            cmd.Parameters.AddWithValue("@JabatanKantor", _fundClient.JabatanKantor);

                            //SPOUSE
                            cmd.Parameters.AddWithValue("@SpouseBirthPlace", _fundClient.SpouseBirthPlace);
                            cmd.Parameters.AddWithValue("@SpouseDateOfBirth", _fundClient.SpouseDateOfBirth);
                            cmd.Parameters.AddWithValue("@SpouseOccupation", _fundClient.SpouseOccupation);
                            cmd.Parameters.AddWithValue("@OtherSpouseOccupation", _fundClient.OtherSpouseOccupation);
                            cmd.Parameters.AddWithValue("@SpouseNatureOfBusiness", _fundClient.SpouseNatureOfBusiness);
                            cmd.Parameters.AddWithValue("@SpouseNatureOfBusinessOther", _fundClient.SpouseNatureOfBusinessOther);
                            cmd.Parameters.AddWithValue("@SpouseIDNo", _fundClient.SpouseIDNo);
                            cmd.Parameters.AddWithValue("@SpouseNationality", _fundClient.SpouseNationality);
                            cmd.Parameters.AddWithValue("@SpouseAnnualIncome", _fundClient.SpouseAnnualIncome);

                            cmd.Parameters.AddWithValue("@CompanyFax", _fundClient.CompanyFax);
                            cmd.Parameters.AddWithValue("@CompanyMail", _fundClient.CompanyMail);
                            cmd.Parameters.AddWithValue("@SegmentClass", _fundClient.SegmentClass);
                            cmd.Parameters.AddWithValue("@MigrationStatus", _fundClient.MigrationStatus);
                            cmd.Parameters.AddWithValue("@CompanyTypeOJK", _fundClient.CompanyTypeOJK);
                            cmd.Parameters.AddWithValue("@Legality", _fundClient.Legality);
                            cmd.Parameters.AddWithValue("@RenewingDate", _fundClient.RenewingDate);
                            cmd.Parameters.AddWithValue("@BitShareAbleToGroup", _fundClient.BitShareAbleToGroup);
                            cmd.Parameters.AddWithValue("@RemarkBank1", _fundClient.RemarkBank1);
                            cmd.Parameters.AddWithValue("@RemarkBank2", _fundClient.RemarkBank2);
                            cmd.Parameters.AddWithValue("@RemarkBank3", _fundClient.RemarkBank3);
                            cmd.Parameters.AddWithValue("@CantSubs", _fundClient.CantSubs);
                            cmd.Parameters.AddWithValue("@CantRedempt", _fundClient.CantRedempt);
                            cmd.Parameters.AddWithValue("@CantSwitch", _fundClient.CantSwitch);

                            cmd.Parameters.AddWithValue("@BeneficialName", _fundClient.BeneficialName);
                            cmd.Parameters.AddWithValue("@BeneficialAddress", _fundClient.BeneficialAddress);
                            cmd.Parameters.AddWithValue("@BeneficialIdentity", _fundClient.BeneficialIdentity);
                            cmd.Parameters.AddWithValue("@BeneficialWork", _fundClient.BeneficialWork);
                            cmd.Parameters.AddWithValue("@BeneficialRelation", _fundClient.BeneficialRelation);
                            cmd.Parameters.AddWithValue("@BeneficialHomeNo", _fundClient.BeneficialHomeNo);
                            cmd.Parameters.AddWithValue("@BeneficialPhoneNumber", _fundClient.BeneficialPhoneNumber);
                            cmd.Parameters.AddWithValue("@BeneficialNPWP", _fundClient.BeneficialNPWP);

                            cmd.Parameters.AddWithValue("@ClientOnBoard", _fundClient.ClientOnBoard);
                            cmd.Parameters.AddWithValue("@Referral", _fundClient.Referral);
                            cmd.Parameters.AddWithValue("@BitisTA", _fundClient.BitisTA);

                            cmd.Parameters.AddWithValue("@AlamatOfficer1", _fundClient.AlamatOfficer1);
                            cmd.Parameters.AddWithValue("@AlamatOfficer2", _fundClient.AlamatOfficer2);
                            cmd.Parameters.AddWithValue("@AlamatOfficer3", _fundClient.AlamatOfficer3);
                            cmd.Parameters.AddWithValue("@AlamatOfficer4", _fundClient.AlamatOfficer4);

                            cmd.Parameters.AddWithValue("@AgamaOfficer1", _fundClient.AgamaOfficer1);
                            cmd.Parameters.AddWithValue("@AgamaOfficer2", _fundClient.AgamaOfficer2);
                            cmd.Parameters.AddWithValue("@AgamaOfficer3", _fundClient.AgamaOfficer3);
                            cmd.Parameters.AddWithValue("@AgamaOfficer4", _fundClient.AgamaOfficer4);

                            cmd.Parameters.AddWithValue("@PlaceOfBirthOfficer1", _fundClient.PlaceOfBirthOfficer1);
                            cmd.Parameters.AddWithValue("@PlaceOfBirthOfficer2", _fundClient.PlaceOfBirthOfficer2);
                            cmd.Parameters.AddWithValue("@PlaceOfBirthOfficer3", _fundClient.PlaceOfBirthOfficer3);
                            cmd.Parameters.AddWithValue("@PlaceOfBirthOfficer4", _fundClient.PlaceOfBirthOfficer4);

                            cmd.Parameters.AddWithValue("@DOBOfficer1", _fundClient.DOBOfficer1);
                            cmd.Parameters.AddWithValue("@DOBOfficer2", _fundClient.DOBOfficer2);
                            cmd.Parameters.AddWithValue("@DOBOfficer3", _fundClient.DOBOfficer3);
                            cmd.Parameters.AddWithValue("@DOBOfficer4", _fundClient.DOBOfficer4);

                            if (_fundClient.FrontID == "" || _fundClient.FrontID == null)
                            {
                                cmd.Parameters.AddWithValue("@FrontID", DBNull.Value);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@FrontID", _fundClient.FrontID);
                            }
                            cmd.Parameters.AddWithValue("@FaceToFaceDate", _fundClient.FaceToFaceDate);
                            cmd.Parameters.AddWithValue("@EmployerLineOfBusiness", _fundClient.EmployerLineOfBusiness);


                            if (_fundClient.TeleponKantor == "" || _fundClient.TeleponKantor == null)
                            {
                                cmd.Parameters.AddWithValue("@TeleponKantor", DBNull.Value);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@TeleponKantor", _fundClient.TeleponKantor);
                            }

                            if (_fundClient.NationalityOfficer1 == "" || _fundClient.NationalityOfficer1 == null)
                            {
                                cmd.Parameters.AddWithValue("@NationalityOfficer1", DBNull.Value);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@NationalityOfficer1", _fundClient.NationalityOfficer1);
                            }

                            if (_fundClient.NationalityOfficer2 == "" || _fundClient.NationalityOfficer2 == null)
                            {
                                cmd.Parameters.AddWithValue("@NationalityOfficer2", DBNull.Value);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@NationalityOfficer2", _fundClient.NationalityOfficer2);
                            }

                            if (_fundClient.NationalityOfficer3 == "" || _fundClient.NationalityOfficer3 == null)
                            {
                                cmd.Parameters.AddWithValue("@NationalityOfficer3", DBNull.Value);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@NationalityOfficer3", _fundClient.NationalityOfficer3);
                            }

                            if (_fundClient.NationalityOfficer4 == "" || _fundClient.NationalityOfficer4 == null)
                            {
                                cmd.Parameters.AddWithValue("@NationalityOfficer4", DBNull.Value);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@NationalityOfficer4", _fundClient.NationalityOfficer4);
                            }

                            //--------------------------------------------------------------------------------------//

                            cmd.Parameters.AddWithValue("@IdentityTypeOfficer1", _fundClient.IdentityTypeOfficer1);
                            cmd.Parameters.AddWithValue("@IdentityTypeOfficer2", _fundClient.IdentityTypeOfficer2);
                            cmd.Parameters.AddWithValue("@IdentityTypeOfficer3", _fundClient.IdentityTypeOfficer3);
                            cmd.Parameters.AddWithValue("@IdentityTypeOfficer4", _fundClient.IdentityTypeOfficer4);
                            cmd.Parameters.AddWithValue("@NoIdentitasOfficer1", _fundClient.NoIdentitasOfficer1);
                            cmd.Parameters.AddWithValue("@NoIdentitasOfficer2", _fundClient.NoIdentitasOfficer2);
                            cmd.Parameters.AddWithValue("@NoIdentitasOfficer3", _fundClient.NoIdentitasOfficer3);
                            cmd.Parameters.AddWithValue("@NoIdentitasOfficer4", _fundClient.NoIdentitasOfficer4);

                            //cmd.Parameters.AddWithValue("@OldID", _fundClient.OldID);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _fundClient.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _dateTimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _fundClient.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);
                            cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                            cmd.ExecuteNonQuery();
                        }

                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update FundClient set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@lastUpdate where FundClientPK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _fundClient.FundClientPK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _fundClient.EntryUsersID);
                            cmd.Parameters.AddWithValue("@VoidTime", _dateTimeNow);
                            cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
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

                                cmd.CommandText = @"Update FundClient set Notes=@Notes,ID=@ID,Name=@Name,
                                ClientCategory=@ClientCategory ,InvestorType=@InvestorType ,InternalCategoryPK=@InternalCategoryPK ,
                                SellingAgentPK=@SellingAgentPK ,UsersPK=@UsersPK,SID=@SID ,IFUACode=@IFUACode , ARIA=@ARIA ,Registered=@Registered ,Negara=@Negara ,Nationality=@Nationality ,NPWP=@NPWP, SACode=@SACode,
                                Propinsi=@Propinsi ,TeleponSelular=@TeleponSelular,Email=@Email ,Fax=@Fax ,DormantDate=@DormantDate ,Description=@Description , 
                                NamaBank1=@NamaBank1 , NomorRekening1=@NomorRekening1 ,NamaNasabah1=@NamaNasabah1 ,MataUang1=@MataUang1 ,OtherCurrency=@OtherCurrency,
                                NamaBank2=@NamaBank2 , NomorRekening2=@NomorRekening2 ,NamaNasabah2=@NamaNasabah2 ,MataUang2=@MataUang2 ,
                                NamaBank3=@NamaBank3 , NomorRekening3=@NomorRekening3 ,NamaNasabah3=@NamaNasabah3 ,MataUang3=@MataUang3 ,
                                NamaDepanInd=@NamaDepanInd ,  NamaTengahInd=@NamaTengahInd ,NamaBelakangInd=@NamaBelakangInd ,TempatLahir=@TempatLahir ,TanggalLahir=@TanggalLahir ,JenisKelamin=@JenisKelamin ,  
                                StatusPerkawinan=@StatusPerkawinan ,Pekerjaan=@Pekerjaan ,OtherOccupation=@OtherOccupation ,Pendidikan=@Pendidikan ,OtherPendidikan=@OtherPendidikan ,Agama=@Agama , OtherAgama=@OtherAgama,PenghasilanInd=@PenghasilanInd ,  
                                SumberDanaInd=@SumberDanaInd , OtherSourceOfFunds=@OtherSourceOfFunds, CapitalPaidIn=@CapitalPaidIn ,MaksudTujuanInd=@MaksudTujuanInd, OtherInvestmentObjectives=@OtherInvestmentObjectives,AlamatInd1=@AlamatInd1 ,KodeKotaInd1=@KodeKotaInd1 ,KodePosInd1=@KodePosInd1 ,  
                                AlamatInd2=@AlamatInd2 ,KodeKotaInd2=@KodeKotaInd2 ,KodePosInd2=@KodePosInd2 ,NamaPerusahaan=@NamaPerusahaan ,Domisili=@Domisili ,  
                                Tipe=@Tipe, OtherTipe=@OtherTipe ,Karakteristik=@Karakteristik,OtherCharacteristic=@OtherCharacteristic ,NoSKD=@NoSKD ,PenghasilanInstitusi=@PenghasilanInstitusi ,SumberDanaInstitusi=@SumberDanaInstitusi ,  OtherSourceOfFundsIns=@OtherSourceOfFundsIns,
                                MaksudTujuanInstitusi=@MaksudTujuanInstitusi, OtherInvestmentObjectivesIns=@OtherInvestmentObjectivesIns ,AlamatPerusahaan=@AlamatPerusahaan ,KodeKotaIns=@KodeKotaIns ,KodePosIns=@KodePosIns ,SpouseName=@SpouseName ,MotherMaidenName=@MotherMaidenName,  
                                AhliWaris=@AhliWaris ,HubunganAhliWaris=@HubunganAhliWaris ,NatureOfBusiness=@NatureOfBusiness ,NatureOfBusinessLainnya=@NatureOfBusinessLainnya ,Politis=@Politis ,  
                                PolitisLainnya=@PolitisLainnya ,PolitisName=@PolitisName ,PolitisFT=@PolitisFT ,PolitisRelation=@PolitisRelation ,TeleponRumah=@TeleponRumah ,OtherAlamatInd1=@OtherAlamatInd1 ,OtherKodeKotaInd1=@OtherKodeKotaInd1 ,OtherKodePosInd1=@OtherKodePosInd1 ,  
                                OtherPropinsiInd1=@OtherPropinsiInd1, CountryOfBirth=@CountryOfBirth ,OtherNegaraInd1=@OtherNegaraInd1 ,OtherAlamatInd2=@OtherAlamatInd2 ,OtherKodeKotaInd2=@OtherKodeKotaInd2 ,OtherKodePosInd2=@OtherKodePosInd2 ,  
                                OtherPropinsiInd2=@OtherPropinsiInd2 ,OtherNegaraInd2=@OtherNegaraInd2 ,OtherAlamatInd3=@OtherAlamatInd3 ,OtherKodeKotaInd3=@OtherKodeKotaInd3 ,OtherKodePosInd3=@OtherKodePosInd3 ,  
                                OtherPropinsiInd3=@OtherPropinsiInd3 ,OtherNegaraInd3=@OtherNegaraInd3 ,OtherTeleponRumah=@OtherTeleponRumah ,OtherTeleponSelular=@OtherTeleponSelular ,OtherEmail=@OtherEmail ,  
                                OtherFax=@OtherFax ,IdentitasInd1=@IdentitasInd1 ,NoIdentitasInd1=@NoIdentitasInd1 ,RegistrationDateIdentitasInd1=@RegistrationDateIdentitasInd1 ,  
                                ExpiredDateIdentitasInd1=@ExpiredDateIdentitasInd1 ,IdentitasInd2=@IdentitasInd2 ,NoIdentitasInd2=@NoIdentitasInd2 ,RegistrationDateIdentitasInd2=@RegistrationDateIdentitasInd2 ,ExpiredDateIdentitasInd2=@ExpiredDateIdentitasInd2 ,  
                                IdentitasInd3=@IdentitasInd3 ,NoIdentitasInd3=@NoIdentitasInd3 ,RegistrationDateIdentitasInd3=@RegistrationDateIdentitasInd3 ,ExpiredDateIdentitasInd3=@ExpiredDateIdentitasInd3 ,IdentitasInd4=@IdentitasInd4 ,  
                                NoIdentitasInd4=@NoIdentitasInd4 ,RegistrationDateIdentitasInd4=@RegistrationDateIdentitasInd4 ,ExpiredDateIdentitasInd4=@ExpiredDateIdentitasInd4 ,RegistrationNPWP=@RegistrationNPWP ,  
                                ExpiredDateSKD=@ExpiredDateSKD ,TanggalBerdiri=@TanggalBerdiri ,LokasiBerdiri=@LokasiBerdiri ,TeleponBisnis=@TeleponBisnis ,NomorAnggaran=@NomorAnggaran ,  
                                NomorSIUP=@NomorSIUP ,AssetFor1Year=@AssetFor1Year ,AssetFor2Year=@AssetFor2Year ,AssetFor3Year=@AssetFor3Year ,OperatingProfitFor1Year=@OperatingProfitFor1Year ,  
                                OperatingProfitFor2Year=@OperatingProfitFor2Year ,OperatingProfitFor3Year=@OperatingProfitFor3Year ,NamaDepanIns1=@NamaDepanIns1 ,NamaTengahIns1=@NamaTengahIns1 ,  
                                NamaBelakangIns1=@NamaBelakangIns1 ,Jabatan1=@Jabatan1 ,IdentitasIns11=@IdentitasIns11 ,NoIdentitasIns11=@NoIdentitasIns11 ,  
                                RegistrationDateIdentitasIns11=@RegistrationDateIdentitasIns11 ,ExpiredDateIdentitasIns11=@ExpiredDateIdentitasIns11 ,IdentitasIns12=@IdentitasIns12 ,NoIdentitasIns12=@NoIdentitasIns12 ,RegistrationDateIdentitasIns12=@RegistrationDateIdentitasIns12 ,  
                                ExpiredDateIdentitasIns12=@ExpiredDateIdentitasIns12 ,IdentitasIns13=@IdentitasIns13 ,NoIdentitasIns13=@NoIdentitasIns13 ,RegistrationDateIdentitasIns13=@RegistrationDateIdentitasIns13 ,ExpiredDateIdentitasIns13=@ExpiredDateIdentitasIns13 ,  
                                IdentitasIns14=@IdentitasIns14 ,NoIdentitasIns14=@NoIdentitasIns14 ,RegistrationDateIdentitasIns14=@RegistrationDateIdentitasIns14 ,ExpiredDateIdentitasIns14=@ExpiredDateIdentitasIns14 ,NamaDepanIns2=@NamaDepanIns2 ,  
                                NamaTengahIns2=@NamaTengahIns2 ,NamaBelakangIns2=@NamaBelakangIns2 ,Jabatan2=@Jabatan2 ,IdentitasIns21=@IdentitasIns21 ,  
                                NoIdentitasIns21=@NoIdentitasIns21 ,RegistrationDateIdentitasIns21=@RegistrationDateIdentitasIns21 ,ExpiredDateIdentitasIns21=@ExpiredDateIdentitasIns21 ,IdentitasIns22=@IdentitasIns22 ,NoIdentitasIns22=@NoIdentitasIns22 ,  
                                RegistrationDateIdentitasIns22=@RegistrationDateIdentitasIns22 ,ExpiredDateIdentitasIns22=@ExpiredDateIdentitasIns22 ,IdentitasIns23=@IdentitasIns23 ,NoIdentitasIns23=@NoIdentitasIns23 ,RegistrationDateIdentitasIns23=@RegistrationDateIdentitasIns23 ,  
                                ExpiredDateIdentitasIns23=@ExpiredDateIdentitasIns23 ,IdentitasIns24=@IdentitasIns24 ,NoIdentitasIns24=@NoIdentitasIns24 ,RegistrationDateIdentitasIns24=@RegistrationDateIdentitasIns24 ,ExpiredDateIdentitasIns24=@ExpiredDateIdentitasIns24 ,  
                                NamaDepanIns3=@NamaDepanIns3 ,NamaTengahIns3=@NamaTengahIns3 ,NamaBelakangIns3=@NamaBelakangIns3 ,Jabatan3=@Jabatan3 ,JumlahIdentitasIns3=@JumlahIdentitasIns3 ,  
                                IdentitasIns31=@IdentitasIns31 ,NoIdentitasIns31=@NoIdentitasIns31 ,RegistrationDateIdentitasIns31=@RegistrationDateIdentitasIns31 ,ExpiredDateIdentitasIns31=@ExpiredDateIdentitasIns31 ,IdentitasIns32=@IdentitasIns32 ,  
                                NoIdentitasIns32=@NoIdentitasIns32 ,RegistrationDateIdentitasIns32=@RegistrationDateIdentitasIns32 ,ExpiredDateIdentitasIns32=@ExpiredDateIdentitasIns32 ,IdentitasIns33=@IdentitasIns33 ,NoIdentitasIns33=@NoIdentitasIns33 ,  
                                RegistrationDateIdentitasIns33=@RegistrationDateIdentitasIns33 ,ExpiredDateIdentitasIns33=@ExpiredDateIdentitasIns33 ,IdentitasIns34=@IdentitasIns34 ,NoIdentitasIns34=@NoIdentitasIns34 ,RegistrationDateIdentitasIns34=@RegistrationDateIdentitasIns34 ,  
                                ExpiredDateIdentitasIns34=@ExpiredDateIdentitasIns34 ,NamaDepanIns4=@NamaDepanIns4 ,NamaTengahIns4=@NamaTengahIns4 ,NamaBelakangIns4=@NamaBelakangIns4 ,Jabatan4=@Jabatan4 ,  
                                JumlahIdentitasIns4=@JumlahIdentitasIns4 ,IdentitasIns41=@IdentitasIns41 ,NoIdentitasIns41=@NoIdentitasIns41 ,RegistrationDateIdentitasIns41=@RegistrationDateIdentitasIns41 ,ExpiredDateIdentitasIns41=@ExpiredDateIdentitasIns41 ,  
                                IdentitasIns42=@IdentitasIns42 ,NoIdentitasIns42=@NoIdentitasIns42 ,RegistrationDateIdentitasIns42=@RegistrationDateIdentitasIns42 ,ExpiredDateIdentitasIns42=@ExpiredDateIdentitasIns42 ,IdentitasIns43=@IdentitasIns43 ,  
                                NoIdentitasIns43=@NoIdentitasIns43 ,RegistrationDateIdentitasIns43=@RegistrationDateIdentitasIns43 ,ExpiredDateIdentitasIns43=@ExpiredDateIdentitasIns43 ,IdentitasIns44=@IdentitasIns44 ,NoIdentitasIns44=@NoIdentitasIns44 ,  
                                RegistrationDateIdentitasIns44=@RegistrationDateIdentitasIns44 ,ExpiredDateIdentitasIns44=@ExpiredDateIdentitasIns44 ,
                                PhoneIns1=@PhoneIns1,EmailIns1=@EmailIns1,PhoneIns2=@PhoneIns2,EmailIns2=@EmailIns2,InvestorsRiskProfile=@InvestorsRiskProfile,AssetOwner=@AssetOwner,StatementType=@StatementType,FATCA=@FATCA,TIN=@TIN, 
                                TINIssuanceCountry=@TINIssuanceCountry,GIIN=@GIIN,SubstantialOwnerName=@SubstantialOwnerName,SubstantialOwnerAddress=@SubstantialOwnerAddress,SubstantialOwnerTIN=@SubstantialOwnerTIN, 
                                BankBranchName1=@BankBranchName1,BankBranchName2=@BankBranchName2,BankBranchName3=@BankBranchName3,
                                CountryofCorrespondence=@CountryofCorrespondence, CountryofDomicile=@CountryofDomicile, 
                                SIUPExpirationDate=@SIUPExpirationDate, CountryofEstablishment=@CountryofEstablishment, CompanyCityName=@CompanyCityName, 
                                CountryofCompany=@CountryofCompany, NPWPPerson1=@NPWPPerson1, NPWPPerson2=@NPWPPerson2,BankRDNPK = @BankRDNPK,RDNAccountNo = @RDNAccountNo,RDNAccountName = @RDNAccountName,RiskProfileScore = @RiskProfileScore,RDNBankBranchName=@RDNBankBranchName,RDNCurrency=@RDNCurrency, 
                                SpouseBirthPlace=@SpouseBirthPlace,SpouseDateOfBirth=@SpouseDateOfBirth,SpouseOccupation=@SpouseOccupation, OtherSpouseOccupation=@OtherSpouseOccupation,SpouseNatureOfBusiness=@SpouseNatureOfBusiness,SpouseNatureOfBusinessOther=@SpouseNatureOfBusinessOther,
                                SpouseIDNo=@SpouseIDNo,SpouseNationality=@SpouseNationality,SpouseAnnualIncome=@SpouseAnnualIncome,IsFaceToFace=@IsFaceToFace,KYCRiskProfile=@KYCRiskProfile,BitDefaultPayment1=@BitDefaultPayment1, BitDefaultPayment2=@BitDefaultPayment2,BitDefaultPayment3=@BitDefaultPayment3,
                                AlamatKantorInd=@AlamatKantorInd,KodeKotaKantorInd=@KodeKotaKantorInd,KodePosKantorInd=@KodePosKantorInd,KodePropinsiKantorInd=@KodePropinsiKantorInd,KodeCountryofKantor=@KodeCountryofKantor,CorrespondenceRT=@CorrespondenceRT,CorrespondenceRW=@CorrespondenceRW,DomicileRT=@DomicileRT,DomicileRW=@DomicileRW,Identity1RT=@Identity1RT,Identity1RW=@Identity1RW,KodeDomisiliPropinsi=@KodeDomisiliPropinsi,                                           
                                NamaKantor = @NamaKantor,JabatanKantor = @JabatanKantor, CompanyFax = @CompanyFax, CompanyMail = @CompanyMail, MigrationStatus = @MigrationStatus, SegmentClass= @SegmentClass, CompanyTypeOJK= @CompanyTypeOJK, Legality = @Legality, RenewingDate=@RenewingDate, BitShareAbleToGroup=@BitShareAbleToGroup, RemarkBank1=@RemarkBank1,RemarkBank2=@RemarkBank2,RemarkBank3=@RemarkBank3,
                                CantSubs=@CantSubs,CantRedempt=@CantRedempt,CantSwitch=@CantSwitch,BeneficialName=@BeneficialName,BeneficialAddress=@BeneficialAddress,BeneficialIdentity=@BeneficialIdentity,BeneficialWork=@BeneficialWork,BeneficialRelation=@BeneficialRelation,BeneficialHomeNo=@BeneficialHomeNo,BeneficialPhoneNumber=@BeneficialPhoneNumber,BeneficialNPWP=@BeneficialNPWP,ClientOnBoard=@ClientOnBoard,Referral=@Referral,BitisTA=@BitisTA,
                                
                                AlamatOfficer1=@AlamatOfficer1,AlamatOfficer2=@AlamatOfficer2,AlamatOfficer3=@AlamatOfficer3,AlamatOfficer4=@AlamatOfficer4,AgamaOfficer1=@AgamaOfficer1,AgamaOfficer2=@AgamaOfficer2,AgamaOfficer3=@AgamaOfficer3,AgamaOfficer4=@AgamaOfficer4,PlaceOfBirthOfficer1=@PlaceOfBirthOfficer1,PlaceOfBirthOfficer2=@PlaceOfBirthOfficer2,PlaceOfBirthOfficer3=@PlaceOfBirthOfficer3,PlaceOfBirthOfficer4=@PlaceOfBirthOfficer4,DOBOfficer1=@DOBOfficer1,DOBOfficer2=@DOBOfficer2,DOBOfficer3=@DOBOfficer3,DOBOfficer4=@DOBOfficer4,FaceToFaceDate=@FaceToFaceDate,EmployerLineOfBusiness=@EmployerLineOfBusiness,FrontID=@FrontID,
                                TeleponKantor=@TeleponKantor,NationalityOfficer1=@NationalityOfficer1,NationalityOfficer2=@NationalityOfficer2,NationalityOfficer3=@NationalityOfficer3,NationalityOfficer4=@NationalityOfficer4,
                                IdentityTypeOfficer1=@IdentityTypeOfficer1,IdentityTypeOfficer2=@IdentityTypeOfficer2,IdentityTypeOfficer3=@IdentityTypeOfficer3,IdentityTypeOfficer4=@IdentityTypeOfficer4,
                                NoIdentitasOfficer1=@NoIdentitasOfficer1,NoIdentitasOfficer2=@NoIdentitasOfficer2,NoIdentitasOfficer3=@NoIdentitasOfficer3,NoIdentitasOfficer4=@NoIdentitasOfficer4,

                                UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@LastUpdate 
                                where FundClientPK = @PK and historyPK = @HistoryPK";

                                cmd.Parameters.AddWithValue("@HistoryPK", _fundClient.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _fundClient.FundClientPK);
                                cmd.Parameters.AddWithValue("@ID", _fundClient.ID);
                                cmd.Parameters.AddWithValue("@Notes", _fundClient.Notes);
                                cmd.Parameters.AddWithValue("@Name", _fundClient.Name);
                                cmd.Parameters.AddWithValue("@ClientCategory", _fundClient.ClientCategory);
                                cmd.Parameters.AddWithValue("@InvestorType", _fundClient.InvestorType);
                                cmd.Parameters.AddWithValue("@InternalCategoryPK", _fundClient.InternalCategoryPK);
                                cmd.Parameters.AddWithValue("@SellingAgentPK", _fundClient.SellingAgentPK);
                                cmd.Parameters.AddWithValue("@UsersPK", _fundClient.UsersPK);
                                cmd.Parameters.AddWithValue("@SID", _fundClient.SID);
                                cmd.Parameters.AddWithValue("@IFUACode", _fundClient.IFUACode);
                                //cmd.Parameters.AddWithValue("@Child", _fundClient.Child);
                                cmd.Parameters.AddWithValue("@ARIA", _fundClient.ARIA);
                                cmd.Parameters.AddWithValue("@Registered", _fundClient.Registered);
                                //cmd.Parameters.AddWithValue("@JumlahDanaAwal", _fundClient.JumlahDanaAwal);
                                //cmd.Parameters.AddWithValue("@JumlahDanaSaatIniCash", _fundClient.JumlahDanaSaatIniCash);
                                //cmd.Parameters.AddWithValue("@JumlahDanaSaatIni", _fundClient.JumlahDanaSaatIni);
                                cmd.Parameters.AddWithValue("@Negara", _fundClient.Negara);
                                cmd.Parameters.AddWithValue("@Nationality", _fundClient.Nationality);
                                cmd.Parameters.AddWithValue("@NPWP", _fundClient.NPWP);
                                cmd.Parameters.AddWithValue("@SACode", _fundClient.SACode);
                                cmd.Parameters.AddWithValue("@Propinsi", _fundClient.Propinsi);
                                cmd.Parameters.AddWithValue("@TeleponSelular", _fundClient.TeleponSelular);
                                cmd.Parameters.AddWithValue("@Email", _fundClient.Email);
                                cmd.Parameters.AddWithValue("@Fax", _fundClient.Fax);
                                cmd.Parameters.AddWithValue("@DormantDate", _fundClient.DormantDate);
                                cmd.Parameters.AddWithValue("@Description", _fundClient.Description);
                                //cmd.Parameters.AddWithValue("@JumlahBank", _fundClient.JumlahBank);
                                cmd.Parameters.AddWithValue("@NamaBank1", _fundClient.NamaBank1);
                                cmd.Parameters.AddWithValue("@NomorRekening1", _fundClient.NomorRekening1);

                                cmd.Parameters.AddWithValue("@NamaNasabah1", _fundClient.NamaNasabah1);
                                cmd.Parameters.AddWithValue("@MataUang1", _fundClient.MataUang1);
                                cmd.Parameters.AddWithValue("@OtherCurrency", _fundClient.OtherCurrency);
                                cmd.Parameters.AddWithValue("@NamaBank2", _fundClient.NamaBank2);
                                cmd.Parameters.AddWithValue("@NomorRekening2", _fundClient.NomorRekening2);

                                cmd.Parameters.AddWithValue("@NamaNasabah2", _fundClient.NamaNasabah2);
                                cmd.Parameters.AddWithValue("@MataUang2", _fundClient.MataUang2);
                                cmd.Parameters.AddWithValue("@NamaBank3", _fundClient.NamaBank3);
                                cmd.Parameters.AddWithValue("@NomorRekening3", _fundClient.NomorRekening3);

                                cmd.Parameters.AddWithValue("@NamaNasabah3", _fundClient.NamaNasabah3);
                                cmd.Parameters.AddWithValue("@MataUang3", _fundClient.MataUang3);
                                cmd.Parameters.AddWithValue("@IsFaceToFace", _fundClient.IsFaceToFace);
                                cmd.Parameters.AddWithValue("@KYCRiskProfile", _fundClient.KYCRiskProfile);

                                cmd.Parameters.AddWithValue("@NamaDepanInd", _fundClient.NamaDepanInd);
                                cmd.Parameters.AddWithValue("@NamaTengahInd", _fundClient.NamaTengahInd);
                                cmd.Parameters.AddWithValue("@NamaBelakangInd", _fundClient.NamaBelakangInd);
                                cmd.Parameters.AddWithValue("@TempatLahir", _fundClient.TempatLahir);
                                cmd.Parameters.AddWithValue("@TanggalLahir", _fundClient.TanggalLahir);
                                cmd.Parameters.AddWithValue("@JenisKelamin", _fundClient.JenisKelamin);
                                cmd.Parameters.AddWithValue("@StatusPerkawinan", _fundClient.StatusPerkawinan);
                                cmd.Parameters.AddWithValue("@Pekerjaan", _fundClient.Pekerjaan);
                                cmd.Parameters.AddWithValue("@OtherOccupation", _fundClient.OtherOccupation);
                                cmd.Parameters.AddWithValue("@Pendidikan", _fundClient.Pendidikan);
                                cmd.Parameters.AddWithValue("@OtherPendidikan", _fundClient.OtherPendidikan);
                                cmd.Parameters.AddWithValue("@Agama", _fundClient.Agama);
                                cmd.Parameters.AddWithValue("@OtherAgama", _fundClient.OtherAgama);
                                cmd.Parameters.AddWithValue("@PenghasilanInd", _fundClient.PenghasilanInd);
                                cmd.Parameters.AddWithValue("@SumberDanaInd", _fundClient.SumberDanaInd);
                                cmd.Parameters.AddWithValue("@OtherSourceOfFunds", _fundClient.OtherSourceOfFunds);
                                cmd.Parameters.AddWithValue("@CapitalPaidIn", _fundClient.CapitalPaidIn);

                                cmd.Parameters.AddWithValue("@MaksudTujuanInd", _fundClient.MaksudTujuanInd);
                                cmd.Parameters.AddWithValue("@OtherInvestmentObjectives", _fundClient.OtherInvestmentObjectives);
                                cmd.Parameters.AddWithValue("@AlamatInd1", _fundClient.AlamatInd1);
                                cmd.Parameters.AddWithValue("@KodeKotaInd1", _fundClient.KodeKotaInd1);
                                cmd.Parameters.AddWithValue("@KodePosInd1", _fundClient.KodePosInd1);
                                cmd.Parameters.AddWithValue("@AlamatInd2", _fundClient.AlamatInd2);
                                cmd.Parameters.AddWithValue("@KodeKotaInd2", _fundClient.KodeKotaInd2);
                                cmd.Parameters.AddWithValue("@KodePosInd2", _fundClient.KodePosInd2);
                                cmd.Parameters.AddWithValue("@NamaPerusahaan", _fundClient.NamaPerusahaan);
                                cmd.Parameters.AddWithValue("@Domisili", _fundClient.Domisili);
                                cmd.Parameters.AddWithValue("@Tipe", _fundClient.Tipe);
                                cmd.Parameters.AddWithValue("@OtherTipe", _fundClient.OtherTipe);
                                cmd.Parameters.AddWithValue("@Karakteristik", _fundClient.Karakteristik);
                                cmd.Parameters.AddWithValue("@OtherCharacteristic", _fundClient.OtherCharacteristic);
                                cmd.Parameters.AddWithValue("@NoSKD", _fundClient.NoSKD);
                                cmd.Parameters.AddWithValue("@PenghasilanInstitusi", _fundClient.PenghasilanInstitusi);
                                cmd.Parameters.AddWithValue("@SumberDanaInstitusi", _fundClient.SumberDanaInstitusi);
                                cmd.Parameters.AddWithValue("@OtherSourceOfFundsIns", _fundClient.OtherSourceOfFundsIns);
                                cmd.Parameters.AddWithValue("@MaksudTujuanInstitusi", _fundClient.MaksudTujuanInstitusi);
                                cmd.Parameters.AddWithValue("@OtherInvestmentObjectivesIns", _fundClient.OtherInvestmentObjectivesIns);
                                cmd.Parameters.AddWithValue("@AlamatPerusahaan", _fundClient.AlamatPerusahaan);
                                cmd.Parameters.AddWithValue("@KodeKotaIns", _fundClient.KodeKotaIns);
                                cmd.Parameters.AddWithValue("@KodePosIns", _fundClient.KodePosIns);
                                cmd.Parameters.AddWithValue("@SpouseName", _fundClient.SpouseName);
                                cmd.Parameters.AddWithValue("@MotherMaidenName", _fundClient.MotherMaidenName);
                                cmd.Parameters.AddWithValue("@AhliWaris", _fundClient.AhliWaris);
                                cmd.Parameters.AddWithValue("@HubunganAhliWaris", _fundClient.HubunganAhliWaris);
                                cmd.Parameters.AddWithValue("@NatureOfBusiness", _fundClient.NatureOfBusiness);
                                cmd.Parameters.AddWithValue("@NatureOfBusinessLainnya", _fundClient.NatureOfBusinessLainnya);
                                cmd.Parameters.AddWithValue("@Politis", _fundClient.Politis);
                                cmd.Parameters.AddWithValue("@PolitisRelation", _fundClient.PolitisRelation);
                                cmd.Parameters.AddWithValue("@PolitisLainnya", _fundClient.PolitisLainnya);
                                cmd.Parameters.AddWithValue("@PolitisName", _fundClient.PolitisName);
                                cmd.Parameters.AddWithValue("@PolitisFT", _fundClient.PolitisFT);
                                cmd.Parameters.AddWithValue("@TeleponRumah", _fundClient.TeleponRumah);
                                cmd.Parameters.AddWithValue("@OtherAlamatInd1", _fundClient.OtherAlamatInd1);
                                cmd.Parameters.AddWithValue("@OtherKodeKotaInd1", _fundClient.OtherKodeKotaInd1);
                                cmd.Parameters.AddWithValue("@OtherKodePosInd1", _fundClient.OtherKodePosInd1);
                                cmd.Parameters.AddWithValue("@OtherPropinsiInd1", _fundClient.OtherPropinsiInd1);
                                cmd.Parameters.AddWithValue("@CountryOfBirth", _fundClient.CountryOfBirth);
                                cmd.Parameters.AddWithValue("@OtherNegaraInd1", _fundClient.OtherNegaraInd1);
                                cmd.Parameters.AddWithValue("@OtherAlamatInd2", _fundClient.OtherAlamatInd2);
                                cmd.Parameters.AddWithValue("@OtherKodeKotaInd2", _fundClient.OtherKodeKotaInd2);
                                cmd.Parameters.AddWithValue("@OtherKodePosInd2", _fundClient.OtherKodePosInd2);
                                cmd.Parameters.AddWithValue("@OtherPropinsiInd2", _fundClient.OtherPropinsiInd2);
                                cmd.Parameters.AddWithValue("@OtherNegaraInd2", _fundClient.OtherNegaraInd2);
                                cmd.Parameters.AddWithValue("@OtherAlamatInd3", _fundClient.OtherAlamatInd3);
                                cmd.Parameters.AddWithValue("@OtherKodeKotaInd3", _fundClient.OtherKodeKotaInd3);
                                cmd.Parameters.AddWithValue("@OtherKodePosInd3", _fundClient.OtherKodePosInd3);
                                cmd.Parameters.AddWithValue("@OtherPropinsiInd3", _fundClient.OtherPropinsiInd3);
                                cmd.Parameters.AddWithValue("@OtherNegaraInd3", _fundClient.OtherNegaraInd3);
                                cmd.Parameters.AddWithValue("@OtherTeleponRumah", _fundClient.OtherTeleponRumah);
                                cmd.Parameters.AddWithValue("@OtherTeleponSelular", _fundClient.OtherTeleponSelular);
                                cmd.Parameters.AddWithValue("@OtherEmail", _fundClient.OtherEmail);
                                cmd.Parameters.AddWithValue("@OtherFax", _fundClient.OtherFax);
                                cmd.Parameters.AddWithValue("@JumlahIdentitasInd", _fundClient.JumlahIdentitasInd);
                                cmd.Parameters.AddWithValue("@IdentitasInd1", _fundClient.IdentitasInd1);
                                cmd.Parameters.AddWithValue("@NoIdentitasInd1", _fundClient.NoIdentitasInd1);
                                cmd.Parameters.AddWithValue("@RegistrationDateIdentitasInd1", _fundClient.RegistrationDateIdentitasInd1);
                                cmd.Parameters.AddWithValue("@ExpiredDateIdentitasInd1", _fundClient.ExpiredDateIdentitasInd1);
                                cmd.Parameters.AddWithValue("@IdentitasInd2", _fundClient.IdentitasInd2);
                                cmd.Parameters.AddWithValue("@NoIdentitasInd2", _fundClient.NoIdentitasInd2);
                                cmd.Parameters.AddWithValue("@RegistrationDateIdentitasInd2", _fundClient.RegistrationDateIdentitasInd2);
                                cmd.Parameters.AddWithValue("@ExpiredDateIdentitasInd2", _fundClient.ExpiredDateIdentitasInd2);
                                cmd.Parameters.AddWithValue("@IdentitasInd3", _fundClient.IdentitasInd3);
                                cmd.Parameters.AddWithValue("@NoIdentitasInd3", _fundClient.NoIdentitasInd3);
                                cmd.Parameters.AddWithValue("@RegistrationDateIdentitasInd3", _fundClient.RegistrationDateIdentitasInd3);
                                cmd.Parameters.AddWithValue("@ExpiredDateIdentitasInd3", _fundClient.ExpiredDateIdentitasInd3);
                                cmd.Parameters.AddWithValue("@IdentitasInd4", _fundClient.IdentitasInd4);
                                cmd.Parameters.AddWithValue("@NoIdentitasInd4", _fundClient.NoIdentitasInd4);
                                cmd.Parameters.AddWithValue("@RegistrationDateIdentitasInd4", _fundClient.RegistrationDateIdentitasInd4);
                                cmd.Parameters.AddWithValue("@ExpiredDateIdentitasInd4", _fundClient.ExpiredDateIdentitasInd4);
                                cmd.Parameters.AddWithValue("@RegistrationNPWP", _fundClient.RegistrationNPWP);
                                cmd.Parameters.AddWithValue("@ExpiredDateSKD", _fundClient.ExpiredDateSKD);
                                cmd.Parameters.AddWithValue("@TanggalBerdiri", _fundClient.TanggalBerdiri);
                                cmd.Parameters.AddWithValue("@LokasiBerdiri", _fundClient.LokasiBerdiri);
                                cmd.Parameters.AddWithValue("@TeleponBisnis", _fundClient.TeleponBisnis);
                                cmd.Parameters.AddWithValue("@NomorAnggaran", _fundClient.NomorAnggaran);
                                cmd.Parameters.AddWithValue("@NomorSIUP", _fundClient.NomorSIUP);
                                cmd.Parameters.AddWithValue("@AssetFor1Year", _fundClient.AssetFor1Year);
                                cmd.Parameters.AddWithValue("@AssetFor2Year", _fundClient.AssetFor2Year);
                                cmd.Parameters.AddWithValue("@AssetFor3Year", _fundClient.AssetFor3Year);
                                cmd.Parameters.AddWithValue("@OperatingProfitFor1Year", _fundClient.OperatingProfitFor1Year);
                                cmd.Parameters.AddWithValue("@OperatingProfitFor2Year", _fundClient.OperatingProfitFor2Year);
                                cmd.Parameters.AddWithValue("@OperatingProfitFor3Year", _fundClient.OperatingProfitFor3Year);
                                cmd.Parameters.AddWithValue("@JumlahPejabat", _fundClient.JumlahPejabat);
                                cmd.Parameters.AddWithValue("@NamaDepanIns1", _fundClient.NamaDepanIns1);
                                cmd.Parameters.AddWithValue("@NamaTengahIns1", _fundClient.NamaTengahIns1);
                                cmd.Parameters.AddWithValue("@NamaBelakangIns1", _fundClient.NamaBelakangIns1);
                                cmd.Parameters.AddWithValue("@Jabatan1", _fundClient.Jabatan1);
                                //cmd.Parameters.AddWithValue("@JumlahIdentitasIns1", _fundClient.JumlahIdentitasIns1);
                                cmd.Parameters.AddWithValue("@IdentitasIns11", _fundClient.IdentitasIns11);
                                cmd.Parameters.AddWithValue("@NoIdentitasIns11", _fundClient.NoIdentitasIns11);
                                cmd.Parameters.AddWithValue("@RegistrationDateIdentitasIns11", _fundClient.RegistrationDateIdentitasIns11);
                                cmd.Parameters.AddWithValue("@ExpiredDateIdentitasIns11", _fundClient.ExpiredDateIdentitasIns11);
                                cmd.Parameters.AddWithValue("@IdentitasIns12", _fundClient.IdentitasIns12);
                                cmd.Parameters.AddWithValue("@NoIdentitasIns12", _fundClient.NoIdentitasIns12);
                                cmd.Parameters.AddWithValue("@RegistrationDateIdentitasIns12", _fundClient.RegistrationDateIdentitasIns12);
                                cmd.Parameters.AddWithValue("@ExpiredDateIdentitasIns12", _fundClient.ExpiredDateIdentitasIns12);
                                cmd.Parameters.AddWithValue("@IdentitasIns13", _fundClient.IdentitasIns13);
                                cmd.Parameters.AddWithValue("@NoIdentitasIns13", _fundClient.NoIdentitasIns13);
                                cmd.Parameters.AddWithValue("@RegistrationDateIdentitasIns13", _fundClient.RegistrationDateIdentitasIns13);
                                cmd.Parameters.AddWithValue("@ExpiredDateIdentitasIns13", _fundClient.ExpiredDateIdentitasIns13);
                                cmd.Parameters.AddWithValue("@IdentitasIns14", _fundClient.IdentitasIns14);
                                cmd.Parameters.AddWithValue("@NoIdentitasIns14", _fundClient.NoIdentitasIns14);
                                cmd.Parameters.AddWithValue("@RegistrationDateIdentitasIns14", _fundClient.RegistrationDateIdentitasIns14);
                                cmd.Parameters.AddWithValue("@ExpiredDateIdentitasIns14", _fundClient.ExpiredDateIdentitasIns14);
                                cmd.Parameters.AddWithValue("@NamaDepanIns2", _fundClient.NamaDepanIns2);
                                cmd.Parameters.AddWithValue("@NamaTengahIns2", _fundClient.NamaTengahIns2);
                                cmd.Parameters.AddWithValue("@NamaBelakangIns2", _fundClient.NamaBelakangIns2);
                                cmd.Parameters.AddWithValue("@Jabatan2", _fundClient.Jabatan2);
                                //cmd.Parameters.AddWithValue("@JumlahIdentitasIns2", _fundClient.JumlahIdentitasIns2);
                                cmd.Parameters.AddWithValue("@IdentitasIns21", _fundClient.IdentitasIns21);
                                cmd.Parameters.AddWithValue("@NoIdentitasIns21", _fundClient.NoIdentitasIns21);
                                cmd.Parameters.AddWithValue("@RegistrationDateIdentitasIns21", _fundClient.RegistrationDateIdentitasIns21);
                                cmd.Parameters.AddWithValue("@ExpiredDateIdentitasIns21", _fundClient.ExpiredDateIdentitasIns21);
                                cmd.Parameters.AddWithValue("@IdentitasIns22", _fundClient.IdentitasIns22);
                                cmd.Parameters.AddWithValue("@NoIdentitasIns22", _fundClient.NoIdentitasIns22);
                                cmd.Parameters.AddWithValue("@RegistrationDateIdentitasIns22", _fundClient.RegistrationDateIdentitasIns22);
                                cmd.Parameters.AddWithValue("@ExpiredDateIdentitasIns22", _fundClient.ExpiredDateIdentitasIns22);
                                cmd.Parameters.AddWithValue("@IdentitasIns23", _fundClient.IdentitasIns23);
                                cmd.Parameters.AddWithValue("@NoIdentitasIns23", _fundClient.NoIdentitasIns23);
                                cmd.Parameters.AddWithValue("@RegistrationDateIdentitasIns23", _fundClient.RegistrationDateIdentitasIns23);
                                cmd.Parameters.AddWithValue("@ExpiredDateIdentitasIns23", _fundClient.ExpiredDateIdentitasIns23);
                                cmd.Parameters.AddWithValue("@IdentitasIns24", _fundClient.IdentitasIns24);
                                cmd.Parameters.AddWithValue("@NoIdentitasIns24", _fundClient.NoIdentitasIns24);
                                cmd.Parameters.AddWithValue("@RegistrationDateIdentitasIns24", _fundClient.RegistrationDateIdentitasIns24);
                                cmd.Parameters.AddWithValue("@ExpiredDateIdentitasIns24", _fundClient.ExpiredDateIdentitasIns24);
                                cmd.Parameters.AddWithValue("@NamaDepanIns3", _fundClient.NamaDepanIns3);
                                cmd.Parameters.AddWithValue("@NamaTengahIns3", _fundClient.NamaTengahIns3);
                                cmd.Parameters.AddWithValue("@NamaBelakangIns3", _fundClient.NamaBelakangIns3);
                                cmd.Parameters.AddWithValue("@Jabatan3", _fundClient.Jabatan3);
                                cmd.Parameters.AddWithValue("@JumlahIdentitasIns3", _fundClient.JumlahIdentitasIns3);
                                cmd.Parameters.AddWithValue("@IdentitasIns31", _fundClient.IdentitasIns31);
                                cmd.Parameters.AddWithValue("@NoIdentitasIns31", _fundClient.NoIdentitasIns31);
                                cmd.Parameters.AddWithValue("@RegistrationDateIdentitasIns31", _fundClient.RegistrationDateIdentitasIns31);
                                cmd.Parameters.AddWithValue("@ExpiredDateIdentitasIns31", _fundClient.ExpiredDateIdentitasIns31);
                                cmd.Parameters.AddWithValue("@IdentitasIns32", _fundClient.IdentitasIns32);
                                cmd.Parameters.AddWithValue("@NoIdentitasIns32", _fundClient.NoIdentitasIns32);
                                cmd.Parameters.AddWithValue("@RegistrationDateIdentitasIns32", _fundClient.RegistrationDateIdentitasIns32);
                                cmd.Parameters.AddWithValue("@ExpiredDateIdentitasIns32", _fundClient.ExpiredDateIdentitasIns32);
                                cmd.Parameters.AddWithValue("@IdentitasIns33", _fundClient.IdentitasIns33);
                                cmd.Parameters.AddWithValue("@NoIdentitasIns33", _fundClient.NoIdentitasIns33);
                                cmd.Parameters.AddWithValue("@RegistrationDateIdentitasIns33", _fundClient.RegistrationDateIdentitasIns33);
                                cmd.Parameters.AddWithValue("@ExpiredDateIdentitasIns33", _fundClient.ExpiredDateIdentitasIns33);
                                cmd.Parameters.AddWithValue("@IdentitasIns34", _fundClient.IdentitasIns34);
                                cmd.Parameters.AddWithValue("@NoIdentitasIns34", _fundClient.NoIdentitasIns34);
                                cmd.Parameters.AddWithValue("@RegistrationDateIdentitasIns34", _fundClient.RegistrationDateIdentitasIns34);
                                cmd.Parameters.AddWithValue("@ExpiredDateIdentitasIns34", _fundClient.ExpiredDateIdentitasIns34);
                                cmd.Parameters.AddWithValue("@NamaDepanIns4", _fundClient.NamaDepanIns4);
                                cmd.Parameters.AddWithValue("@NamaTengahIns4", _fundClient.NamaTengahIns4);
                                cmd.Parameters.AddWithValue("@NamaBelakangIns4", _fundClient.NamaBelakangIns4);
                                cmd.Parameters.AddWithValue("@Jabatan4", _fundClient.Jabatan4);
                                cmd.Parameters.AddWithValue("@JumlahIdentitasIns4", _fundClient.JumlahIdentitasIns4);
                                cmd.Parameters.AddWithValue("@IdentitasIns41", _fundClient.IdentitasIns41);
                                cmd.Parameters.AddWithValue("@NoIdentitasIns41", _fundClient.NoIdentitasIns41);
                                cmd.Parameters.AddWithValue("@RegistrationDateIdentitasIns41", _fundClient.RegistrationDateIdentitasIns41);
                                cmd.Parameters.AddWithValue("@ExpiredDateIdentitasIns41", _fundClient.ExpiredDateIdentitasIns41);
                                cmd.Parameters.AddWithValue("@IdentitasIns42", _fundClient.IdentitasIns42);
                                cmd.Parameters.AddWithValue("@NoIdentitasIns42", _fundClient.NoIdentitasIns42);
                                cmd.Parameters.AddWithValue("@RegistrationDateIdentitasIns42", _fundClient.RegistrationDateIdentitasIns42);
                                cmd.Parameters.AddWithValue("@ExpiredDateIdentitasIns42", _fundClient.ExpiredDateIdentitasIns42);
                                cmd.Parameters.AddWithValue("@IdentitasIns43", _fundClient.IdentitasIns43);
                                cmd.Parameters.AddWithValue("@NoIdentitasIns43", _fundClient.NoIdentitasIns43);
                                cmd.Parameters.AddWithValue("@RegistrationDateIdentitasIns43", _fundClient.RegistrationDateIdentitasIns43);
                                cmd.Parameters.AddWithValue("@ExpiredDateIdentitasIns43", _fundClient.ExpiredDateIdentitasIns43);
                                cmd.Parameters.AddWithValue("@IdentitasIns44", _fundClient.IdentitasIns44);
                                cmd.Parameters.AddWithValue("@NoIdentitasIns44", _fundClient.NoIdentitasIns44);
                                cmd.Parameters.AddWithValue("@RegistrationDateIdentitasIns44", _fundClient.RegistrationDateIdentitasIns44);
                                cmd.Parameters.AddWithValue("@ExpiredDateIdentitasIns44", _fundClient.ExpiredDateIdentitasIns44);

                                cmd.Parameters.AddWithValue("@PhoneIns1", _fundClient.@PhoneIns1);
                                cmd.Parameters.AddWithValue("@EmailIns1", _fundClient.@EmailIns1);
                                cmd.Parameters.AddWithValue("@PhoneIns2", _fundClient.PhoneIns2);
                                cmd.Parameters.AddWithValue("@EmailIns2", _fundClient.EmailIns2);
                                cmd.Parameters.AddWithValue("@InvestorsRiskProfile", _fundClient.InvestorsRiskProfile);
                                cmd.Parameters.AddWithValue("@AssetOwner", _fundClient.AssetOwner);
                                cmd.Parameters.AddWithValue("@StatementType", _fundClient.StatementType);
                                cmd.Parameters.AddWithValue("@FATCA", _fundClient.FATCA);
                                cmd.Parameters.AddWithValue("@TIN", _fundClient.TIN);
                                cmd.Parameters.AddWithValue("@TINIssuanceCountry", _fundClient.TINIssuanceCountry);
                                cmd.Parameters.AddWithValue("@GIIN", _fundClient.GIIN);
                                cmd.Parameters.AddWithValue("@SubstantialOwnerName", _fundClient.SubstantialOwnerName);
                                cmd.Parameters.AddWithValue("@SubstantialOwnerAddress", _fundClient.SubstantialOwnerAddress);
                                cmd.Parameters.AddWithValue("@SubstantialOwnerTIN", _fundClient.SubstantialOwnerTIN);
                                cmd.Parameters.AddWithValue("@BankBranchName1", _fundClient.BankBranchName1);
                                cmd.Parameters.AddWithValue("@BankBranchName2", _fundClient.BankBranchName2);
                                cmd.Parameters.AddWithValue("@BankBranchName3", _fundClient.BankBranchName3);
                                cmd.Parameters.AddWithValue("@BankCountry1", _fundClient.BankCountry1);
                                cmd.Parameters.AddWithValue("@BankCountry2", _fundClient.BankCountry2);
                                cmd.Parameters.AddWithValue("@BankCountry3", _fundClient.BankCountry3);
                                cmd.Parameters.AddWithValue("@BitDefaultPayment1", _fundClient.BitDefaultPayment1);
                                cmd.Parameters.AddWithValue("@BitDefaultPayment2", _fundClient.BitDefaultPayment2);
                                cmd.Parameters.AddWithValue("@BitDefaultPayment3", _fundClient.BitDefaultPayment3);

                                cmd.Parameters.AddWithValue("@AlamatKantorInd", _fundClient.AlamatKantorInd);
                                cmd.Parameters.AddWithValue("@KodeKotaKantorInd", _fundClient.KodeKotaKantorInd);
                                cmd.Parameters.AddWithValue("@KodePosKantorInd", _fundClient.KodePosKantorInd);
                                cmd.Parameters.AddWithValue("@KodePropinsiKantorInd", _fundClient.KodePropinsiKantorInd);
                                cmd.Parameters.AddWithValue("@KodeCountryofKantor", _fundClient.KodeCountryofKantor);
                                cmd.Parameters.AddWithValue("@CorrespondenceRW", _fundClient.CorrespondenceRW);
                                cmd.Parameters.AddWithValue("@CorrespondenceRT", _fundClient.CorrespondenceRT);
                                cmd.Parameters.AddWithValue("@DomicileRT", _fundClient.DomicileRT);
                                cmd.Parameters.AddWithValue("@DomicileRW", _fundClient.DomicileRW);
                                cmd.Parameters.AddWithValue("@Identity1RT", _fundClient.Identity1RT);
                                cmd.Parameters.AddWithValue("@Identity1RW", _fundClient.Identity1RW);
                                cmd.Parameters.AddWithValue("@KodeDomisiliPropinsi", _fundClient.KodeDomisiliPropinsi);

                                cmd.Parameters.AddWithValue("@CountryofCorrespondence", _fundClient.CountryofCorrespondence);
                                cmd.Parameters.AddWithValue("@CountryofDomicile", _fundClient.CountryofDomicile);
                                cmd.Parameters.AddWithValue("@SIUPExpirationDate", _fundClient.SIUPExpirationDate);
                                cmd.Parameters.AddWithValue("@CountryofEstablishment", _fundClient.CountryofEstablishment);
                                cmd.Parameters.AddWithValue("@CompanyCityName", _fundClient.CompanyCityName);
                                cmd.Parameters.AddWithValue("@CountryofCompany", _fundClient.CountryofCompany);
                                cmd.Parameters.AddWithValue("@NPWPPerson1", _fundClient.NPWPPerson1);
                                cmd.Parameters.AddWithValue("@NPWPPerson2", _fundClient.NPWPPerson2);

                                cmd.Parameters.AddWithValue("@NamaKantor", _fundClient.NamaKantor);
                                cmd.Parameters.AddWithValue("@JabatanKantor", _fundClient.JabatanKantor);

                                // RDN
                                cmd.Parameters.AddWithValue("@BankRDNPK", _fundClient.BankRDNPK);
                                cmd.Parameters.AddWithValue("@RDNAccountNo", _fundClient.RDNAccountNo);
                                cmd.Parameters.AddWithValue("@RDNAccountName", _fundClient.RDNAccountName);
                                cmd.Parameters.AddWithValue("@RDNBankBranchName", _fundClient.RDNBankBranchName);
                                cmd.Parameters.AddWithValue("@RDNCurrency", _fundClient.RDNCurrency);

                                //SPOUSE
                                cmd.Parameters.AddWithValue("@SpouseBirthPlace", _fundClient.SpouseBirthPlace);
                                cmd.Parameters.AddWithValue("@SpouseDateOfBirth", _fundClient.SpouseDateOfBirth);
                                cmd.Parameters.AddWithValue("@SpouseOccupation", _fundClient.SpouseOccupation);
                                cmd.Parameters.AddWithValue("@OtherSpouseOccupation", _fundClient.OtherSpouseOccupation);
                                cmd.Parameters.AddWithValue("@SpouseNatureOfBusiness", _fundClient.SpouseNatureOfBusiness);
                                cmd.Parameters.AddWithValue("@SpouseNatureOfBusinessOther", _fundClient.SpouseNatureOfBusinessOther);
                                cmd.Parameters.AddWithValue("@SpouseIDNo", _fundClient.SpouseIDNo);
                                cmd.Parameters.AddWithValue("@SpouseNationality", _fundClient.SpouseNationality);
                                cmd.Parameters.AddWithValue("@SpouseAnnualIncome", _fundClient.SpouseAnnualIncome);

                                cmd.Parameters.AddWithValue("@CompanyFax", _fundClient.CompanyFax);
                                cmd.Parameters.AddWithValue("@CompanyMail", _fundClient.CompanyMail);

                                cmd.Parameters.AddWithValue("@RiskProfileScore", _fundClient.RiskProfileScore);
                                cmd.Parameters.AddWithValue("@SegmentClass", _fundClient.SegmentClass);
                                cmd.Parameters.AddWithValue("@MigrationStatus", _fundClient.MigrationStatus);
                                cmd.Parameters.AddWithValue("@CompanyTypeOJK", _fundClient.CompanyTypeOJK);
                                cmd.Parameters.AddWithValue("@Legality", _fundClient.Legality);
                                cmd.Parameters.AddWithValue("@RenewingDate", _fundClient.RenewingDate);
                                cmd.Parameters.AddWithValue("@BitShareAbleToGroup", _fundClient.BitShareAbleToGroup);
                                cmd.Parameters.AddWithValue("@RemarkBank1", _fundClient.RemarkBank1);
                                cmd.Parameters.AddWithValue("@RemarkBank2", _fundClient.RemarkBank2);
                                cmd.Parameters.AddWithValue("@RemarkBank3", _fundClient.RemarkBank3);
                                cmd.Parameters.AddWithValue("@CantSubs", _fundClient.CantSubs);
                                cmd.Parameters.AddWithValue("@CantRedempt", _fundClient.CantRedempt);
                                cmd.Parameters.AddWithValue("@CantSwitch", _fundClient.CantSwitch);

                                cmd.Parameters.AddWithValue("@BeneficialName", _fundClient.BeneficialName);
                                cmd.Parameters.AddWithValue("@BeneficialAddress", _fundClient.BeneficialAddress);
                                cmd.Parameters.AddWithValue("@BeneficialIdentity", _fundClient.BeneficialIdentity);
                                cmd.Parameters.AddWithValue("@BeneficialWork", _fundClient.BeneficialWork);
                                cmd.Parameters.AddWithValue("@BeneficialRelation", _fundClient.BeneficialRelation);
                                cmd.Parameters.AddWithValue("@BeneficialHomeNo", _fundClient.BeneficialHomeNo);
                                cmd.Parameters.AddWithValue("@BeneficialPhoneNumber", _fundClient.BeneficialPhoneNumber);
                                cmd.Parameters.AddWithValue("@BeneficialNPWP", _fundClient.BeneficialNPWP);

                                cmd.Parameters.AddWithValue("@ClientOnBoard", _fundClient.ClientOnBoard);
                                cmd.Parameters.AddWithValue("@Referral", _fundClient.Referral);
                                cmd.Parameters.AddWithValue("@BitisTA", _fundClient.BitisTA);

                                cmd.Parameters.AddWithValue("@AlamatOfficer1", _fundClient.AlamatOfficer1);
                                cmd.Parameters.AddWithValue("@AlamatOfficer2", _fundClient.AlamatOfficer2);
                                cmd.Parameters.AddWithValue("@AlamatOfficer3", _fundClient.AlamatOfficer3);
                                cmd.Parameters.AddWithValue("@AlamatOfficer4", _fundClient.AlamatOfficer4);

                                cmd.Parameters.AddWithValue("@AgamaOfficer1", _fundClient.AgamaOfficer1);
                                cmd.Parameters.AddWithValue("@AgamaOfficer2", _fundClient.AgamaOfficer2);
                                cmd.Parameters.AddWithValue("@AgamaOfficer3", _fundClient.AgamaOfficer3);
                                cmd.Parameters.AddWithValue("@AgamaOfficer4", _fundClient.AgamaOfficer4);

                                cmd.Parameters.AddWithValue("@PlaceOfBirthOfficer1", _fundClient.PlaceOfBirthOfficer1);
                                cmd.Parameters.AddWithValue("@PlaceOfBirthOfficer2", _fundClient.PlaceOfBirthOfficer2);
                                cmd.Parameters.AddWithValue("@PlaceOfBirthOfficer3", _fundClient.PlaceOfBirthOfficer3);
                                cmd.Parameters.AddWithValue("@PlaceOfBirthOfficer4", _fundClient.PlaceOfBirthOfficer4);

                                cmd.Parameters.AddWithValue("@DOBOfficer1", _fundClient.DOBOfficer1);
                                cmd.Parameters.AddWithValue("@DOBOfficer2", _fundClient.DOBOfficer2);
                                cmd.Parameters.AddWithValue("@DOBOfficer3", _fundClient.DOBOfficer3);
                                cmd.Parameters.AddWithValue("@DOBOfficer4", _fundClient.DOBOfficer4);

                                if (_fundClient.FrontID == "" || _fundClient.FrontID == null)
                                {
                                    cmd.Parameters.AddWithValue("@FrontID", DBNull.Value);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@FrontID", _fundClient.FrontID);
                                }
                                cmd.Parameters.AddWithValue("@FaceToFaceDate", _fundClient.FaceToFaceDate);
                                cmd.Parameters.AddWithValue("@EmployerLineOfBusiness", _fundClient.EmployerLineOfBusiness);


                                if (_fundClient.TeleponKantor == "" || _fundClient.TeleponKantor == null)
                                {
                                    cmd.Parameters.AddWithValue("@TeleponKantor", DBNull.Value);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@TeleponKantor", _fundClient.TeleponKantor);
                                }

                                if (_fundClient.NationalityOfficer1 == "" || _fundClient.NationalityOfficer1 == null)
                                {
                                    cmd.Parameters.AddWithValue("@NationalityOfficer1", DBNull.Value);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@NationalityOfficer1", _fundClient.NationalityOfficer1);
                                }

                                if (_fundClient.NationalityOfficer2 == "" || _fundClient.NationalityOfficer2 == null)
                                {
                                    cmd.Parameters.AddWithValue("@NationalityOfficer2", DBNull.Value);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@NationalityOfficer2", _fundClient.NationalityOfficer2);
                                }

                                if (_fundClient.NationalityOfficer3 == "" || _fundClient.NationalityOfficer3 == null)
                                {
                                    cmd.Parameters.AddWithValue("@NationalityOfficer3", DBNull.Value);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@NationalityOfficer3", _fundClient.NationalityOfficer3);
                                }

                                if (_fundClient.NationalityOfficer4 == "" || _fundClient.NationalityOfficer4 == null)
                                {
                                    cmd.Parameters.AddWithValue("@NationalityOfficer4", DBNull.Value);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@NationalityOfficer4", _fundClient.NationalityOfficer4);
                                }

                                cmd.Parameters.AddWithValue("@IdentityTypeOfficer1", _fundClient.IdentityTypeOfficer1);
                                cmd.Parameters.AddWithValue("@IdentityTypeOfficer2", _fundClient.IdentityTypeOfficer2);
                                cmd.Parameters.AddWithValue("@IdentityTypeOfficer3", _fundClient.IdentityTypeOfficer3);
                                cmd.Parameters.AddWithValue("@IdentityTypeOfficer4", _fundClient.IdentityTypeOfficer4);
                                cmd.Parameters.AddWithValue("@NoIdentitasOfficer1", _fundClient.NoIdentitasOfficer1);
                                cmd.Parameters.AddWithValue("@NoIdentitasOfficer2", _fundClient.NoIdentitasOfficer2);
                                cmd.Parameters.AddWithValue("@NoIdentitasOfficer3", _fundClient.NoIdentitasOfficer3);
                                cmd.Parameters.AddWithValue("@NoIdentitasOfficer4", _fundClient.NoIdentitasOfficer4);

                                //cmd.Parameters.AddWithValue("@OldID", _fundClient.OldID);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _fundClient.EntryUsersID);
                                cmd.Parameters.AddWithValue("@Updatetime", _dateTimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                                cmd.ExecuteNonQuery();
                            }
                            return 0;
                        }
                        else
                        {

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                _newHisPK = _host.Get_NewHistoryPK(_fundClient.FundClientPK, "FundClient");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From FundClient where FundClientPK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _fundClient.FundClientPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _fundClient.HistoryPK);
                                cmd.Parameters.AddWithValue("@ID", _fundClient.ID);
                                cmd.Parameters.AddWithValue("@Name", _fundClient.Name);
                                cmd.Parameters.AddWithValue("@ClientCategory", _fundClient.ClientCategory);
                                cmd.Parameters.AddWithValue("@InvestorType", _fundClient.InvestorType);
                                cmd.Parameters.AddWithValue("@InternalCategoryPK", _fundClient.InternalCategoryPK);
                                cmd.Parameters.AddWithValue("@SellingAgentPK", _fundClient.SellingAgentPK);
                                cmd.Parameters.AddWithValue("@UsersPK", _fundClient.UsersPK);
                                cmd.Parameters.AddWithValue("@SID", _fundClient.SID);
                                cmd.Parameters.AddWithValue("@IFUACode", _fundClient.IFUACode);
                                //cmd.Parameters.AddWithValue("@Child", _fundClient.Child);
                                cmd.Parameters.AddWithValue("@ARIA", _fundClient.ARIA);
                                cmd.Parameters.AddWithValue("@Registered", _fundClient.Registered);
                                //cmd.Parameters.AddWithValue("@JumlahDanaAwal", _fundClient.JumlahDanaAwal);
                                //cmd.Parameters.AddWithValue("@JumlahDanaSaatIniCash", _fundClient.JumlahDanaSaatIniCash);
                                //cmd.Parameters.AddWithValue("@JumlahDanaSaatIni", _fundClient.JumlahDanaSaatIni);
                                cmd.Parameters.AddWithValue("@Negara", _fundClient.Negara);
                                cmd.Parameters.AddWithValue("@Nationality", _fundClient.Nationality);
                                cmd.Parameters.AddWithValue("@NPWP", _fundClient.NPWP);
                                cmd.Parameters.AddWithValue("@SACode", _fundClient.SACode);
                                cmd.Parameters.AddWithValue("@Propinsi", _fundClient.Propinsi);
                                cmd.Parameters.AddWithValue("@TeleponSelular", _fundClient.TeleponSelular);
                                cmd.Parameters.AddWithValue("@Email", _fundClient.Email);
                                cmd.Parameters.AddWithValue("@Fax", _fundClient.Fax);
                                cmd.Parameters.AddWithValue("@DormantDate", _fundClient.DormantDate);
                                cmd.Parameters.AddWithValue("@Description", _fundClient.Description);
                                //cmd.Parameters.AddWithValue("@JumlahBank", _fundClient.JumlahBank);
                                cmd.Parameters.AddWithValue("@NamaBank1", _fundClient.NamaBank1);
                                cmd.Parameters.AddWithValue("@NomorRekening1", _fundClient.NomorRekening1);

                                cmd.Parameters.AddWithValue("@NamaNasabah1", _fundClient.NamaNasabah1);
                                cmd.Parameters.AddWithValue("@MataUang1", _fundClient.MataUang1);
                                cmd.Parameters.AddWithValue("@OtherCurrency", _fundClient.OtherCurrency);
                                cmd.Parameters.AddWithValue("@NamaBank2", _fundClient.NamaBank2);
                                cmd.Parameters.AddWithValue("@NomorRekening2", _fundClient.NomorRekening2);

                                cmd.Parameters.AddWithValue("@NamaNasabah2", _fundClient.NamaNasabah2);
                                cmd.Parameters.AddWithValue("@MataUang2", _fundClient.MataUang2);
                                cmd.Parameters.AddWithValue("@NamaBank3", _fundClient.NamaBank3);
                                cmd.Parameters.AddWithValue("@NomorRekening3", _fundClient.NomorRekening3);

                                cmd.Parameters.AddWithValue("@NamaNasabah3", _fundClient.NamaNasabah3);
                                cmd.Parameters.AddWithValue("@MataUang3", _fundClient.MataUang3);
                                cmd.Parameters.AddWithValue("@IsFaceToFace", _fundClient.IsFaceToFace);
                                cmd.Parameters.AddWithValue("@KYCRiskProfile", _fundClient.KYCRiskProfile);

                                cmd.Parameters.AddWithValue("@NamaDepanInd", _fundClient.NamaDepanInd);
                                cmd.Parameters.AddWithValue("@NamaTengahInd", _fundClient.NamaTengahInd);
                                cmd.Parameters.AddWithValue("@NamaBelakangInd", _fundClient.NamaBelakangInd);
                                cmd.Parameters.AddWithValue("@TempatLahir", _fundClient.TempatLahir);
                                cmd.Parameters.AddWithValue("@TanggalLahir", _fundClient.TanggalLahir);
                                cmd.Parameters.AddWithValue("@JenisKelamin", _fundClient.JenisKelamin);
                                cmd.Parameters.AddWithValue("@StatusPerkawinan", _fundClient.StatusPerkawinan);
                                cmd.Parameters.AddWithValue("@Pekerjaan", _fundClient.Pekerjaan);
                                cmd.Parameters.AddWithValue("@OtherOccupation", _fundClient.OtherOccupation);
                                cmd.Parameters.AddWithValue("@Pendidikan", _fundClient.Pendidikan);
                                cmd.Parameters.AddWithValue("@OtherPendidikan", _fundClient.OtherPendidikan);
                                cmd.Parameters.AddWithValue("@Agama", _fundClient.Agama);
                                cmd.Parameters.AddWithValue("@OtherAgama", _fundClient.OtherAgama);
                                cmd.Parameters.AddWithValue("@PenghasilanInd", _fundClient.PenghasilanInd);
                                cmd.Parameters.AddWithValue("@SumberDanaInd", _fundClient.SumberDanaInd);
                                cmd.Parameters.AddWithValue("@OtherSourceOfFunds", _fundClient.OtherSourceOfFunds);
                                cmd.Parameters.AddWithValue("@CapitalPaidIn", _fundClient.CapitalPaidIn);

                                cmd.Parameters.AddWithValue("@MaksudTujuanInd", _fundClient.MaksudTujuanInd);
                                cmd.Parameters.AddWithValue("@OtherInvestmentObjectives", _fundClient.OtherInvestmentObjectives);
                                cmd.Parameters.AddWithValue("@AlamatInd1", _fundClient.AlamatInd1);
                                cmd.Parameters.AddWithValue("@KodeKotaInd1", _fundClient.KodeKotaInd1);
                                cmd.Parameters.AddWithValue("@KodePosInd1", _fundClient.KodePosInd1);
                                cmd.Parameters.AddWithValue("@AlamatInd2", _fundClient.AlamatInd2);
                                cmd.Parameters.AddWithValue("@KodeKotaInd2", _fundClient.KodeKotaInd2);
                                cmd.Parameters.AddWithValue("@KodePosInd2", _fundClient.KodePosInd2);
                                cmd.Parameters.AddWithValue("@NamaPerusahaan", _fundClient.NamaPerusahaan);
                                cmd.Parameters.AddWithValue("@Domisili", _fundClient.Domisili);
                                cmd.Parameters.AddWithValue("@Tipe", _fundClient.Tipe);
                                cmd.Parameters.AddWithValue("@OtherTipe", _fundClient.OtherTipe);
                                cmd.Parameters.AddWithValue("@Karakteristik", _fundClient.Karakteristik);
                                cmd.Parameters.AddWithValue("@OtherCharacteristic", _fundClient.OtherCharacteristic);
                                cmd.Parameters.AddWithValue("@NoSKD", _fundClient.NoSKD);
                                cmd.Parameters.AddWithValue("@PenghasilanInstitusi", _fundClient.PenghasilanInstitusi);
                                cmd.Parameters.AddWithValue("@SumberDanaInstitusi", _fundClient.SumberDanaInstitusi);
                                cmd.Parameters.AddWithValue("@OtherSourceOfFundsIns", _fundClient.OtherSourceOfFundsIns);
                                cmd.Parameters.AddWithValue("@MaksudTujuanInstitusi", _fundClient.MaksudTujuanInstitusi);
                                cmd.Parameters.AddWithValue("@OtherInvestmentObjectivesIns", _fundClient.OtherInvestmentObjectivesIns);

                                cmd.Parameters.AddWithValue("@AlamatPerusahaan", _fundClient.AlamatPerusahaan);
                                cmd.Parameters.AddWithValue("@KodeKotaIns", _fundClient.KodeKotaIns);
                                cmd.Parameters.AddWithValue("@KodePosIns", _fundClient.KodePosIns);
                                cmd.Parameters.AddWithValue("@SpouseName", _fundClient.SpouseName);
                                cmd.Parameters.AddWithValue("@MotherMaidenName", _fundClient.MotherMaidenName);
                                cmd.Parameters.AddWithValue("@AhliWaris", _fundClient.AhliWaris);
                                cmd.Parameters.AddWithValue("@HubunganAhliWaris", _fundClient.HubunganAhliWaris);
                                cmd.Parameters.AddWithValue("@NatureOfBusiness", _fundClient.NatureOfBusiness);
                                cmd.Parameters.AddWithValue("@NatureOfBusinessLainnya", _fundClient.NatureOfBusinessLainnya);
                                cmd.Parameters.AddWithValue("@Politis", _fundClient.Politis);
                                cmd.Parameters.AddWithValue("@PolitisRelation", _fundClient.PolitisRelation);
                                cmd.Parameters.AddWithValue("@PolitisLainnya", _fundClient.PolitisLainnya);
                                cmd.Parameters.AddWithValue("@PolitisName", _fundClient.PolitisName);
                                cmd.Parameters.AddWithValue("@PolitisFT", _fundClient.PolitisFT);
                                cmd.Parameters.AddWithValue("@TeleponRumah", _fundClient.TeleponRumah);
                                cmd.Parameters.AddWithValue("@OtherAlamatInd1", _fundClient.OtherAlamatInd1);
                                cmd.Parameters.AddWithValue("@OtherKodeKotaInd1", _fundClient.OtherKodeKotaInd1);
                                cmd.Parameters.AddWithValue("@OtherKodePosInd1", _fundClient.OtherKodePosInd1);
                                cmd.Parameters.AddWithValue("@OtherPropinsiInd1", _fundClient.OtherPropinsiInd1);
                                cmd.Parameters.AddWithValue("@CountryOfBirth", _fundClient.CountryOfBirth);
                                cmd.Parameters.AddWithValue("@OtherNegaraInd1", _fundClient.OtherNegaraInd1);
                                cmd.Parameters.AddWithValue("@OtherAlamatInd2", _fundClient.OtherAlamatInd2);
                                cmd.Parameters.AddWithValue("@OtherKodeKotaInd2", _fundClient.OtherKodeKotaInd2);
                                cmd.Parameters.AddWithValue("@OtherKodePosInd2", _fundClient.OtherKodePosInd2);
                                cmd.Parameters.AddWithValue("@OtherPropinsiInd2", _fundClient.OtherPropinsiInd2);
                                cmd.Parameters.AddWithValue("@OtherNegaraInd2", _fundClient.OtherNegaraInd2);
                                cmd.Parameters.AddWithValue("@OtherAlamatInd3", _fundClient.OtherAlamatInd3);
                                cmd.Parameters.AddWithValue("@OtherKodeKotaInd3", _fundClient.OtherKodeKotaInd3);
                                cmd.Parameters.AddWithValue("@OtherKodePosInd3", _fundClient.OtherKodePosInd3);
                                cmd.Parameters.AddWithValue("@OtherPropinsiInd3", _fundClient.OtherPropinsiInd3);
                                cmd.Parameters.AddWithValue("@OtherNegaraInd3", _fundClient.OtherNegaraInd3);
                                cmd.Parameters.AddWithValue("@OtherTeleponRumah", _fundClient.OtherTeleponRumah);
                                cmd.Parameters.AddWithValue("@OtherTeleponSelular", _fundClient.OtherTeleponSelular);
                                cmd.Parameters.AddWithValue("@OtherEmail", _fundClient.OtherEmail);
                                cmd.Parameters.AddWithValue("@OtherFax", _fundClient.OtherFax);
                                cmd.Parameters.AddWithValue("@JumlahIdentitasInd", _fundClient.JumlahIdentitasInd);
                                cmd.Parameters.AddWithValue("@IdentitasInd1", _fundClient.IdentitasInd1);
                                cmd.Parameters.AddWithValue("@NoIdentitasInd1", _fundClient.NoIdentitasInd1);
                                cmd.Parameters.AddWithValue("@RegistrationDateIdentitasInd1", _fundClient.RegistrationDateIdentitasInd1);
                                cmd.Parameters.AddWithValue("@ExpiredDateIdentitasInd1", _fundClient.ExpiredDateIdentitasInd1);
                                cmd.Parameters.AddWithValue("@IdentitasInd2", _fundClient.IdentitasInd2);
                                cmd.Parameters.AddWithValue("@NoIdentitasInd2", _fundClient.NoIdentitasInd2);
                                cmd.Parameters.AddWithValue("@RegistrationDateIdentitasInd2", _fundClient.RegistrationDateIdentitasInd2);
                                cmd.Parameters.AddWithValue("@ExpiredDateIdentitasInd2", _fundClient.ExpiredDateIdentitasInd2);
                                cmd.Parameters.AddWithValue("@IdentitasInd3", _fundClient.IdentitasInd3);
                                cmd.Parameters.AddWithValue("@NoIdentitasInd3", _fundClient.NoIdentitasInd3);
                                cmd.Parameters.AddWithValue("@RegistrationDateIdentitasInd3", _fundClient.RegistrationDateIdentitasInd3);
                                cmd.Parameters.AddWithValue("@ExpiredDateIdentitasInd3", _fundClient.ExpiredDateIdentitasInd3);
                                cmd.Parameters.AddWithValue("@IdentitasInd4", _fundClient.IdentitasInd4);
                                cmd.Parameters.AddWithValue("@NoIdentitasInd4", _fundClient.NoIdentitasInd4);
                                cmd.Parameters.AddWithValue("@RegistrationDateIdentitasInd4", _fundClient.RegistrationDateIdentitasInd4);
                                cmd.Parameters.AddWithValue("@ExpiredDateIdentitasInd4", _fundClient.ExpiredDateIdentitasInd4);
                                cmd.Parameters.AddWithValue("@RegistrationNPWP", _fundClient.RegistrationNPWP);
                                cmd.Parameters.AddWithValue("@ExpiredDateSKD", _fundClient.ExpiredDateSKD);
                                cmd.Parameters.AddWithValue("@TanggalBerdiri", _fundClient.TanggalBerdiri);
                                cmd.Parameters.AddWithValue("@LokasiBerdiri", _fundClient.LokasiBerdiri);
                                cmd.Parameters.AddWithValue("@TeleponBisnis", _fundClient.TeleponBisnis);
                                cmd.Parameters.AddWithValue("@NomorAnggaran", _fundClient.NomorAnggaran);
                                cmd.Parameters.AddWithValue("@NomorSIUP", _fundClient.NomorSIUP);
                                cmd.Parameters.AddWithValue("@AssetFor1Year", _fundClient.AssetFor1Year);
                                cmd.Parameters.AddWithValue("@AssetFor2Year", _fundClient.AssetFor2Year);
                                cmd.Parameters.AddWithValue("@AssetFor3Year", _fundClient.AssetFor3Year);
                                cmd.Parameters.AddWithValue("@OperatingProfitFor1Year", _fundClient.OperatingProfitFor1Year);
                                cmd.Parameters.AddWithValue("@OperatingProfitFor2Year", _fundClient.OperatingProfitFor2Year);
                                cmd.Parameters.AddWithValue("@OperatingProfitFor3Year", _fundClient.OperatingProfitFor3Year);
                                cmd.Parameters.AddWithValue("@JumlahPejabat", _fundClient.JumlahPejabat);
                                cmd.Parameters.AddWithValue("@NamaDepanIns1", _fundClient.NamaDepanIns1);
                                cmd.Parameters.AddWithValue("@NamaTengahIns1", _fundClient.NamaTengahIns1);
                                cmd.Parameters.AddWithValue("@NamaBelakangIns1", _fundClient.NamaBelakangIns1);
                                cmd.Parameters.AddWithValue("@Jabatan1", _fundClient.Jabatan1);
                                //cmd.Parameters.AddWithValue("@JumlahIdentitasIns1", _fundClient.JumlahIdentitasIns1);
                                cmd.Parameters.AddWithValue("@IdentitasIns11", _fundClient.IdentitasIns11);
                                cmd.Parameters.AddWithValue("@NoIdentitasIns11", _fundClient.NoIdentitasIns11);
                                cmd.Parameters.AddWithValue("@RegistrationDateIdentitasIns11", _fundClient.RegistrationDateIdentitasIns11);
                                cmd.Parameters.AddWithValue("@ExpiredDateIdentitasIns11", _fundClient.ExpiredDateIdentitasIns11);
                                cmd.Parameters.AddWithValue("@IdentitasIns12", _fundClient.IdentitasIns12);
                                cmd.Parameters.AddWithValue("@NoIdentitasIns12", _fundClient.NoIdentitasIns12);
                                cmd.Parameters.AddWithValue("@RegistrationDateIdentitasIns12", _fundClient.RegistrationDateIdentitasIns12);
                                cmd.Parameters.AddWithValue("@ExpiredDateIdentitasIns12", _fundClient.ExpiredDateIdentitasIns12);
                                cmd.Parameters.AddWithValue("@IdentitasIns13", _fundClient.IdentitasIns13);
                                cmd.Parameters.AddWithValue("@NoIdentitasIns13", _fundClient.NoIdentitasIns13);
                                cmd.Parameters.AddWithValue("@RegistrationDateIdentitasIns13", _fundClient.RegistrationDateIdentitasIns13);
                                cmd.Parameters.AddWithValue("@ExpiredDateIdentitasIns13", _fundClient.ExpiredDateIdentitasIns13);
                                cmd.Parameters.AddWithValue("@IdentitasIns14", _fundClient.IdentitasIns14);
                                cmd.Parameters.AddWithValue("@NoIdentitasIns14", _fundClient.NoIdentitasIns14);
                                cmd.Parameters.AddWithValue("@RegistrationDateIdentitasIns14", _fundClient.RegistrationDateIdentitasIns14);
                                cmd.Parameters.AddWithValue("@ExpiredDateIdentitasIns14", _fundClient.ExpiredDateIdentitasIns14);
                                cmd.Parameters.AddWithValue("@NamaDepanIns2", _fundClient.NamaDepanIns2);
                                cmd.Parameters.AddWithValue("@NamaTengahIns2", _fundClient.NamaTengahIns2);
                                cmd.Parameters.AddWithValue("@NamaBelakangIns2", _fundClient.NamaBelakangIns2);
                                cmd.Parameters.AddWithValue("@Jabatan2", _fundClient.Jabatan2);
                                //cmd.Parameters.AddWithValue("@JumlahIdentitasIns2", _fundClient.JumlahIdentitasIns2);
                                cmd.Parameters.AddWithValue("@IdentitasIns21", _fundClient.IdentitasIns21);
                                cmd.Parameters.AddWithValue("@NoIdentitasIns21", _fundClient.NoIdentitasIns21);
                                cmd.Parameters.AddWithValue("@RegistrationDateIdentitasIns21", _fundClient.RegistrationDateIdentitasIns21);
                                cmd.Parameters.AddWithValue("@ExpiredDateIdentitasIns21", _fundClient.ExpiredDateIdentitasIns21);
                                cmd.Parameters.AddWithValue("@IdentitasIns22", _fundClient.IdentitasIns22);
                                cmd.Parameters.AddWithValue("@NoIdentitasIns22", _fundClient.NoIdentitasIns22);
                                cmd.Parameters.AddWithValue("@RegistrationDateIdentitasIns22", _fundClient.RegistrationDateIdentitasIns22);
                                cmd.Parameters.AddWithValue("@ExpiredDateIdentitasIns22", _fundClient.ExpiredDateIdentitasIns22);
                                cmd.Parameters.AddWithValue("@IdentitasIns23", _fundClient.IdentitasIns23);
                                cmd.Parameters.AddWithValue("@NoIdentitasIns23", _fundClient.NoIdentitasIns23);
                                cmd.Parameters.AddWithValue("@RegistrationDateIdentitasIns23", _fundClient.RegistrationDateIdentitasIns23);
                                cmd.Parameters.AddWithValue("@ExpiredDateIdentitasIns23", _fundClient.ExpiredDateIdentitasIns23);
                                cmd.Parameters.AddWithValue("@IdentitasIns24", _fundClient.IdentitasIns24);
                                cmd.Parameters.AddWithValue("@NoIdentitasIns24", _fundClient.NoIdentitasIns24);
                                cmd.Parameters.AddWithValue("@RegistrationDateIdentitasIns24", _fundClient.RegistrationDateIdentitasIns24);
                                cmd.Parameters.AddWithValue("@ExpiredDateIdentitasIns24", _fundClient.ExpiredDateIdentitasIns24);
                                cmd.Parameters.AddWithValue("@NamaDepanIns3", _fundClient.NamaDepanIns3);
                                cmd.Parameters.AddWithValue("@NamaTengahIns3", _fundClient.NamaTengahIns3);
                                cmd.Parameters.AddWithValue("@NamaBelakangIns3", _fundClient.NamaBelakangIns3);
                                cmd.Parameters.AddWithValue("@Jabatan3", _fundClient.Jabatan3);
                                cmd.Parameters.AddWithValue("@JumlahIdentitasIns3", _fundClient.JumlahIdentitasIns3);
                                cmd.Parameters.AddWithValue("@IdentitasIns31", _fundClient.IdentitasIns31);
                                cmd.Parameters.AddWithValue("@NoIdentitasIns31", _fundClient.NoIdentitasIns31);
                                cmd.Parameters.AddWithValue("@RegistrationDateIdentitasIns31", _fundClient.RegistrationDateIdentitasIns31);
                                cmd.Parameters.AddWithValue("@ExpiredDateIdentitasIns31", _fundClient.ExpiredDateIdentitasIns31);
                                cmd.Parameters.AddWithValue("@IdentitasIns32", _fundClient.IdentitasIns32);
                                cmd.Parameters.AddWithValue("@NoIdentitasIns32", _fundClient.NoIdentitasIns32);
                                cmd.Parameters.AddWithValue("@RegistrationDateIdentitasIns32", _fundClient.RegistrationDateIdentitasIns32);
                                cmd.Parameters.AddWithValue("@ExpiredDateIdentitasIns32", _fundClient.ExpiredDateIdentitasIns32);
                                cmd.Parameters.AddWithValue("@IdentitasIns33", _fundClient.IdentitasIns33);
                                cmd.Parameters.AddWithValue("@NoIdentitasIns33", _fundClient.NoIdentitasIns33);
                                cmd.Parameters.AddWithValue("@RegistrationDateIdentitasIns33", _fundClient.RegistrationDateIdentitasIns33);
                                cmd.Parameters.AddWithValue("@ExpiredDateIdentitasIns33", _fundClient.ExpiredDateIdentitasIns33);
                                cmd.Parameters.AddWithValue("@IdentitasIns34", _fundClient.IdentitasIns34);
                                cmd.Parameters.AddWithValue("@NoIdentitasIns34", _fundClient.NoIdentitasIns34);
                                cmd.Parameters.AddWithValue("@RegistrationDateIdentitasIns34", _fundClient.RegistrationDateIdentitasIns34);
                                cmd.Parameters.AddWithValue("@ExpiredDateIdentitasIns34", _fundClient.ExpiredDateIdentitasIns34);
                                cmd.Parameters.AddWithValue("@NamaDepanIns4", _fundClient.NamaDepanIns4);
                                cmd.Parameters.AddWithValue("@NamaTengahIns4", _fundClient.NamaTengahIns4);
                                cmd.Parameters.AddWithValue("@NamaBelakangIns4", _fundClient.NamaBelakangIns4);
                                cmd.Parameters.AddWithValue("@Jabatan4", _fundClient.Jabatan4);
                                cmd.Parameters.AddWithValue("@JumlahIdentitasIns4", _fundClient.JumlahIdentitasIns4);
                                cmd.Parameters.AddWithValue("@IdentitasIns41", _fundClient.IdentitasIns41);
                                cmd.Parameters.AddWithValue("@NoIdentitasIns41", _fundClient.NoIdentitasIns41);
                                cmd.Parameters.AddWithValue("@RegistrationDateIdentitasIns41", _fundClient.RegistrationDateIdentitasIns41);
                                cmd.Parameters.AddWithValue("@ExpiredDateIdentitasIns41", _fundClient.ExpiredDateIdentitasIns41);
                                cmd.Parameters.AddWithValue("@IdentitasIns42", _fundClient.IdentitasIns42);
                                cmd.Parameters.AddWithValue("@NoIdentitasIns42", _fundClient.NoIdentitasIns42);
                                cmd.Parameters.AddWithValue("@RegistrationDateIdentitasIns42", _fundClient.RegistrationDateIdentitasIns42);
                                cmd.Parameters.AddWithValue("@ExpiredDateIdentitasIns42", _fundClient.ExpiredDateIdentitasIns42);
                                cmd.Parameters.AddWithValue("@IdentitasIns43", _fundClient.IdentitasIns43);
                                cmd.Parameters.AddWithValue("@NoIdentitasIns43", _fundClient.NoIdentitasIns43);
                                cmd.Parameters.AddWithValue("@RegistrationDateIdentitasIns43", _fundClient.RegistrationDateIdentitasIns43);
                                cmd.Parameters.AddWithValue("@ExpiredDateIdentitasIns43", _fundClient.ExpiredDateIdentitasIns43);
                                cmd.Parameters.AddWithValue("@IdentitasIns44", _fundClient.IdentitasIns44);
                                cmd.Parameters.AddWithValue("@NoIdentitasIns44", _fundClient.NoIdentitasIns44);
                                cmd.Parameters.AddWithValue("@RegistrationDateIdentitasIns44", _fundClient.RegistrationDateIdentitasIns44);
                                cmd.Parameters.AddWithValue("@ExpiredDateIdentitasIns44", _fundClient.ExpiredDateIdentitasIns44);

                                cmd.Parameters.AddWithValue("@PhoneIns1", _fundClient.@PhoneIns1);
                                cmd.Parameters.AddWithValue("@EmailIns1", _fundClient.@EmailIns1);
                                cmd.Parameters.AddWithValue("@PhoneIns2", _fundClient.PhoneIns2);
                                cmd.Parameters.AddWithValue("@EmailIns2", _fundClient.EmailIns2);
                                cmd.Parameters.AddWithValue("@InvestorsRiskProfile", _fundClient.InvestorsRiskProfile);
                                cmd.Parameters.AddWithValue("@AssetOwner", _fundClient.AssetOwner);
                                cmd.Parameters.AddWithValue("@StatementType", _fundClient.StatementType);
                                cmd.Parameters.AddWithValue("@FATCA", _fundClient.FATCA);
                                cmd.Parameters.AddWithValue("@TIN", _fundClient.TIN);
                                cmd.Parameters.AddWithValue("@TINIssuanceCountry", _fundClient.TINIssuanceCountry);
                                cmd.Parameters.AddWithValue("@GIIN", _fundClient.GIIN);
                                cmd.Parameters.AddWithValue("@SubstantialOwnerName", _fundClient.SubstantialOwnerName);
                                cmd.Parameters.AddWithValue("@SubstantialOwnerAddress", _fundClient.SubstantialOwnerAddress);
                                cmd.Parameters.AddWithValue("@SubstantialOwnerTIN", _fundClient.SubstantialOwnerTIN);
                                cmd.Parameters.AddWithValue("@BankBranchName1", _fundClient.BankBranchName1);
                                cmd.Parameters.AddWithValue("@BankBranchName2", _fundClient.BankBranchName2);
                                cmd.Parameters.AddWithValue("@BankBranchName3", _fundClient.BankBranchName3);
                                cmd.Parameters.AddWithValue("@BankCountry1", _fundClient.BankCountry1);
                                cmd.Parameters.AddWithValue("@BankCountry2", _fundClient.BankCountry2);
                                cmd.Parameters.AddWithValue("@BankCountry3", _fundClient.BankCountry3);
                                cmd.Parameters.AddWithValue("@BitDefaultPayment1", _fundClient.BitDefaultPayment1);
                                cmd.Parameters.AddWithValue("@BitDefaultPayment2", _fundClient.BitDefaultPayment2);
                                cmd.Parameters.AddWithValue("@BitDefaultPayment3", _fundClient.BitDefaultPayment3);

                                cmd.Parameters.AddWithValue("@AlamatKantorInd", _fundClient.AlamatKantorInd);
                                cmd.Parameters.AddWithValue("@KodeKotaKantorInd", _fundClient.KodeKotaKantorInd);
                                cmd.Parameters.AddWithValue("@KodePosKantorInd", _fundClient.KodePosKantorInd);
                                cmd.Parameters.AddWithValue("@KodePropinsiKantorInd", _fundClient.KodePropinsiKantorInd);
                                cmd.Parameters.AddWithValue("@KodeCountryofKantor", _fundClient.KodeCountryofKantor);
                                cmd.Parameters.AddWithValue("@CorrespondenceRW", _fundClient.CorrespondenceRW);
                                cmd.Parameters.AddWithValue("@CorrespondenceRT", _fundClient.CorrespondenceRT);
                                cmd.Parameters.AddWithValue("@DomicileRT", _fundClient.DomicileRT);
                                cmd.Parameters.AddWithValue("@DomicileRW", _fundClient.DomicileRW);
                                cmd.Parameters.AddWithValue("@Identity1RT", _fundClient.Identity1RT);
                                cmd.Parameters.AddWithValue("@Identity1RW", _fundClient.Identity1RW);
                                cmd.Parameters.AddWithValue("@KodeDomisiliPropinsi", _fundClient.KodeDomisiliPropinsi);

                                cmd.Parameters.AddWithValue("@CountryofCorrespondence", _fundClient.CountryofCorrespondence);
                                cmd.Parameters.AddWithValue("@CountryofDomicile", _fundClient.CountryofDomicile);
                                cmd.Parameters.AddWithValue("@SIUPExpirationDate", _fundClient.SIUPExpirationDate);
                                cmd.Parameters.AddWithValue("@CountryofEstablishment", _fundClient.CountryofEstablishment);
                                cmd.Parameters.AddWithValue("@CompanyCityName", _fundClient.CompanyCityName);
                                cmd.Parameters.AddWithValue("@CountryofCompany", _fundClient.CountryofCompany);
                                cmd.Parameters.AddWithValue("@NPWPPerson1", _fundClient.NPWPPerson1);
                                cmd.Parameters.AddWithValue("@NPWPPerson2", _fundClient.NPWPPerson2);

                                cmd.Parameters.AddWithValue("@NamaKantor", _fundClient.NamaKantor);
                                cmd.Parameters.AddWithValue("@JabatanKantor", _fundClient.JabatanKantor);

                                // RDN
                                cmd.Parameters.AddWithValue("@BankRDNPK", _fundClient.BankRDNPK);
                                cmd.Parameters.AddWithValue("@RDNAccountNo", _fundClient.RDNAccountNo);
                                cmd.Parameters.AddWithValue("@RDNAccountName", _fundClient.RDNAccountName);
                                cmd.Parameters.AddWithValue("@RDNBankBranchName", _fundClient.RDNBankBranchName);
                                cmd.Parameters.AddWithValue("@RDNCurrency", _fundClient.RDNCurrency);

                                //SPOUSE
                                cmd.Parameters.AddWithValue("@SpouseBirthPlace", _fundClient.SpouseBirthPlace);
                                cmd.Parameters.AddWithValue("@SpouseDateOfBirth", _fundClient.SpouseDateOfBirth);
                                cmd.Parameters.AddWithValue("@SpouseOccupation", _fundClient.SpouseOccupation);
                                cmd.Parameters.AddWithValue("@OtherSpouseOccupation", _fundClient.OtherSpouseOccupation);
                                cmd.Parameters.AddWithValue("@SpouseNatureOfBusiness", _fundClient.SpouseNatureOfBusiness);
                                cmd.Parameters.AddWithValue("@SpouseNatureOfBusinessOther", _fundClient.SpouseNatureOfBusinessOther);
                                cmd.Parameters.AddWithValue("@SpouseIDNo", _fundClient.SpouseIDNo);
                                cmd.Parameters.AddWithValue("@SpouseNationality", _fundClient.SpouseNationality);
                                cmd.Parameters.AddWithValue("@SpouseAnnualIncome", _fundClient.SpouseAnnualIncome);

                                cmd.Parameters.AddWithValue("@CompanyFax", _fundClient.CompanyFax);
                                cmd.Parameters.AddWithValue("@CompanyMail", _fundClient.CompanyMail);

                                cmd.Parameters.AddWithValue("@RiskProfileScore", _fundClient.RiskProfileScore);
                                cmd.Parameters.AddWithValue("@SegmentClass", _fundClient.SegmentClass);
                                cmd.Parameters.AddWithValue("@MigrationStatus", _fundClient.MigrationStatus);
                                cmd.Parameters.AddWithValue("@CompanyTypeOJK", _fundClient.CompanyTypeOJK);
                                cmd.Parameters.AddWithValue("@Legality", _fundClient.Legality);
                                cmd.Parameters.AddWithValue("@RenewingDate", _fundClient.RenewingDate);
                                cmd.Parameters.AddWithValue("@BitShareAbleToGroup", _fundClient.BitShareAbleToGroup);
                                cmd.Parameters.AddWithValue("@RemarkBank1", _fundClient.RemarkBank1);
                                cmd.Parameters.AddWithValue("@RemarkBank2", _fundClient.RemarkBank2);
                                cmd.Parameters.AddWithValue("@RemarkBank3", _fundClient.RemarkBank3);
                                cmd.Parameters.AddWithValue("@CantSubs", _fundClient.CantSubs);
                                cmd.Parameters.AddWithValue("@CantRedempt", _fundClient.CantRedempt);
                                cmd.Parameters.AddWithValue("@CantSwitch", _fundClient.CantSwitch);

                                cmd.Parameters.AddWithValue("@BeneficialName", _fundClient.BeneficialName);
                                cmd.Parameters.AddWithValue("@BeneficialAddress", _fundClient.BeneficialAddress);
                                cmd.Parameters.AddWithValue("@BeneficialIdentity", _fundClient.BeneficialIdentity);
                                cmd.Parameters.AddWithValue("@BeneficialWork", _fundClient.BeneficialWork);
                                cmd.Parameters.AddWithValue("@BeneficialRelation", _fundClient.BeneficialRelation);
                                cmd.Parameters.AddWithValue("@BeneficialHomeNo", _fundClient.BeneficialHomeNo);
                                cmd.Parameters.AddWithValue("@BeneficialPhoneNumber", _fundClient.BeneficialPhoneNumber);
                                cmd.Parameters.AddWithValue("@BeneficialNPWP", _fundClient.BeneficialNPWP);

                                cmd.Parameters.AddWithValue("@ClientOnBoard", _fundClient.ClientOnBoard);
                                cmd.Parameters.AddWithValue("@Referral", _fundClient.Referral);
                                cmd.Parameters.AddWithValue("@BitisTA", _fundClient.BitisTA);

                                cmd.Parameters.AddWithValue("@AlamatOfficer1", _fundClient.AlamatOfficer1);
                                cmd.Parameters.AddWithValue("@AlamatOfficer2", _fundClient.AlamatOfficer2);
                                cmd.Parameters.AddWithValue("@AlamatOfficer3", _fundClient.AlamatOfficer3);
                                cmd.Parameters.AddWithValue("@AlamatOfficer4", _fundClient.AlamatOfficer4);

                                cmd.Parameters.AddWithValue("@AgamaOfficer1", _fundClient.AgamaOfficer1);
                                cmd.Parameters.AddWithValue("@AgamaOfficer2", _fundClient.AgamaOfficer2);
                                cmd.Parameters.AddWithValue("@AgamaOfficer3", _fundClient.AgamaOfficer3);
                                cmd.Parameters.AddWithValue("@AgamaOfficer4", _fundClient.AgamaOfficer4);

                                cmd.Parameters.AddWithValue("@PlaceOfBirthOfficer1", _fundClient.PlaceOfBirthOfficer1);
                                cmd.Parameters.AddWithValue("@PlaceOfBirthOfficer2", _fundClient.PlaceOfBirthOfficer2);
                                cmd.Parameters.AddWithValue("@PlaceOfBirthOfficer3", _fundClient.PlaceOfBirthOfficer3);
                                cmd.Parameters.AddWithValue("@PlaceOfBirthOfficer4", _fundClient.PlaceOfBirthOfficer4);

                                cmd.Parameters.AddWithValue("@DOBOfficer1", _fundClient.DOBOfficer1);
                                cmd.Parameters.AddWithValue("@DOBOfficer2", _fundClient.DOBOfficer2);
                                cmd.Parameters.AddWithValue("@DOBOfficer3", _fundClient.DOBOfficer3);
                                cmd.Parameters.AddWithValue("@DOBOfficer4", _fundClient.DOBOfficer4);

                                if (_fundClient.FrontID == "" || _fundClient.FrontID == null)
                                {
                                    cmd.Parameters.AddWithValue("@FrontID", DBNull.Value);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@FrontID", _fundClient.FrontID);
                                }
                                //cmd.Parameters.AddWithValue("@FrontID", _fundClient.FrontID);
                                cmd.Parameters.AddWithValue("@FaceToFaceDate", _fundClient.FaceToFaceDate);
                                cmd.Parameters.AddWithValue("@EmployerLineOfBusiness", _fundClient.EmployerLineOfBusiness);

                                if (_fundClient.TeleponKantor == "" || _fundClient.TeleponKantor == null)
                                {
                                    cmd.Parameters.AddWithValue("@TeleponKantor", DBNull.Value);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@TeleponKantor", _fundClient.TeleponKantor);
                                }

                                if (_fundClient.NationalityOfficer1 == "" || _fundClient.NationalityOfficer1 == null)
                                {
                                    cmd.Parameters.AddWithValue("@NationalityOfficer1", DBNull.Value);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@NationalityOfficer1", _fundClient.NationalityOfficer1);
                                }

                                if (_fundClient.NationalityOfficer2 == "" || _fundClient.NationalityOfficer2 == null)
                                {
                                    cmd.Parameters.AddWithValue("@NationalityOfficer2", DBNull.Value);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@NationalityOfficer2", _fundClient.NationalityOfficer2);
                                }

                                if (_fundClient.NationalityOfficer3 == "" || _fundClient.NationalityOfficer3 == null)
                                {
                                    cmd.Parameters.AddWithValue("@NationalityOfficer3", DBNull.Value);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@NationalityOfficer3", _fundClient.NationalityOfficer3);
                                }

                                if (_fundClient.NationalityOfficer4 == "" || _fundClient.NationalityOfficer4 == null)
                                {
                                    cmd.Parameters.AddWithValue("@NationalityOfficer4", DBNull.Value);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@NationalityOfficer4", _fundClient.NationalityOfficer4);
                                }

                                cmd.Parameters.AddWithValue("@IdentityTypeOfficer1", _fundClient.IdentityTypeOfficer1);
                                cmd.Parameters.AddWithValue("@IdentityTypeOfficer2", _fundClient.IdentityTypeOfficer2);
                                cmd.Parameters.AddWithValue("@IdentityTypeOfficer3", _fundClient.IdentityTypeOfficer3);
                                cmd.Parameters.AddWithValue("@IdentityTypeOfficer4", _fundClient.IdentityTypeOfficer4);
                                cmd.Parameters.AddWithValue("@NoIdentitasOfficer1", _fundClient.NoIdentitasOfficer1);
                                cmd.Parameters.AddWithValue("@NoIdentitasOfficer2", _fundClient.NoIdentitasOfficer2);
                                cmd.Parameters.AddWithValue("@NoIdentitasOfficer3", _fundClient.NoIdentitasOfficer3);
                                cmd.Parameters.AddWithValue("@NoIdentitasOfficer4", _fundClient.NoIdentitasOfficer4);

                                cmd.Parameters.AddWithValue("@UpdateUsersID", _fundClient.UpdateUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _dateTimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update FundClient set status= 4,Notes=@Notes," +
                                    "LastUpdate=@LastUpdate where FundClientPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _fundClient.Notes);
                                cmd.Parameters.AddWithValue("@PK", _fundClient.FundClientPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _fundClient.HistoryPK);
                                cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
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


        public void FundClient_Approved(FundClient _fundClient)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();



                    // CIPTADANA REQUEST SUSPEND = 1
                    if (Tools.ClientCode == "21")
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = @"
                                update FundClient set ID= @ID ,status = 2, FrontSync = 1, BitIsSuspend = 1,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime ,LastUpdate=@LastUpdate 
                                where FundClientPK = @PK and historypk = @historyPK and EntryUsersID <> 'RDO' 

                                update FundClient set ID= @ID ,status = 2, FrontSync = 1, ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime ,LastUpdate=@LastUpdate 
                                where FundClientPK = @PK and historypk = @historyPK and EntryUsersID = 'RDO' 
       
                            ";
                            cmd.Parameters.AddWithValue("@PK", _fundClient.FundClientPK);
                            cmd.Parameters.AddWithValue("@historyPK", _fundClient.HistoryPK);
                            cmd.Parameters.AddWithValue("@ID", _fundClient.ID);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _fundClient.ApprovedUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);
                            cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        if (Tools.ClientCode == "10")
                        {
                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = @"
                                                --generate client id
                                                Declare @NewClientID  nvarchar(100)    
                                                Declare @MaxClientID  int
                                    
                                                select @MaxClientID =   max(convert(int,ID))  + 1 from FundClient where  status in (1,2) and id not like '%[a-zA-Z]%' and ID <> ''
							                    select @maxClientID = isnull(@MaxClientID,1)
							
							                    declare @LENdigit int
							                    select @LENdigit = LEN(@maxClientID) 
							
							                    If @LENdigit = 1
							                    BEGIN
								                    set @NewClientID =  '00000' + CAST(@MaxClientID as nvarchar) 
                                                END
							                    If @LENdigit = 2
							                    BEGIN
								                    set @NewClientID =  '0000' + CAST(@MaxClientID as nvarchar) 
                                                END
							                    If @LENdigit = 3
							                    BEGIN
								                    set @NewClientID =  '000' + CAST(@MaxClientID as nvarchar) 
                                                END
							                    If @LENdigit = 4
							                    BEGIN
								                    set @NewClientID =  '00' + CAST(@MaxClientID as nvarchar) 
                                                END
							                    If @LENdigit = 5
							                    BEGIN
								                    set @NewClientID =  '0' + CAST(@MaxClientID as nvarchar) 
                                                END
                                                If @LENdigit = 6
                                                BEGIN
	                                                set @NewClientID =    CAST(@MaxClientID as nvarchar) 
                                                END

                                                --update id to fundclient
                                                update FundClient set ID= @NewClientID ,status = 2, FrontSync = 1,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime ,LastUpdate=@LastUpdate 
                                                where FundClientPK = @PK and historypk = @historyPK

                                                ";

                                cmd.Parameters.AddWithValue("@PK", _fundClient.FundClientPK);
                                cmd.Parameters.AddWithValue("@historyPK", _fundClient.HistoryPK);

                                cmd.Parameters.AddWithValue("@ApprovedUsersID", _fundClient.ApprovedUsersID);
                                cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                                cmd.ExecuteNonQuery();
                            }
                        }
                        else
                        {
                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = @"update FundClient set ID= @ID ,status = 2, FrontSync = 1,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime ,LastUpdate=@LastUpdate 
                                                where FundClientPK = @PK and historypk = @historyPK";
                                cmd.Parameters.AddWithValue("@PK", _fundClient.FundClientPK);
                                cmd.Parameters.AddWithValue("@historyPK", _fundClient.HistoryPK);
                                cmd.Parameters.AddWithValue("@ID", _fundClient.ID);
                                cmd.Parameters.AddWithValue("@ApprovedUsersID", _fundClient.ApprovedUsersID);
                                cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                                cmd.ExecuteNonQuery();
                            }
                        }

                    }



                    if (Tools.RDOSync)
                    {
                        ActivityReps _activityReps = new ActivityReps();
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandTimeout = 0;
                            cmd.CommandText = @"if exists(
                            Select * from FundClient    
                            where fundClientPK = @fundclientPK
                            and status = 2 and historyPK = @historyPK)
                            begin
	                            Select '1'  result
                            end
                            else 
                            begin
	                            Select '0'  result
                            end";
                            cmd.Parameters.AddWithValue("@FundClientPK", _fundClient.FundClientPK);
                            cmd.Parameters.AddWithValue("@historyPK", _fundClient.HistoryPK);
                            using (SqlDataReader dr = cmd.ExecuteReader())
                            {
                                if (dr.HasRows)
                                {
                                    dr.Read();
                                    if (dr["result"].ToString().Equals("1"))
                                    {
                                        var data = BackOffice.VerifyProfile(_fundClient.FundClientPK.ToString(), FoConnection.Authentication());
                                        _activityReps.Activity_LogInsert(DateTime.Now, "FundClient_A", true, "", "", "FO Interface Fund client Verify Approved", _fundClient.ApprovedUsersID, _fundClient.FundClientPK, 0, 0, "");
                                    }
                                }
                            }
                        }
                    }



                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update FundClient set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@lastUpdate where FundClientPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _fundClient.FundClientPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _fundClient.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                        cmd.ExecuteNonQuery();
                    }

                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 0;
                        cmd.CommandText = "HighRiskMonitoring_AddByFundClientApproved";
                        cmd.Parameters.AddWithValue("@FundClientPK", _fundClient.FundClientPK);
                        cmd.Parameters.AddWithValue("@UsersID", _fundClient.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@ClientCode", Tools.ClientCode);
                        cmd.ExecuteNonQuery();
                    }


                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public void FundClient_Reject(FundClient _fundClient)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update FundClient set status = 3, Notes = @Notes, VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where FundClientPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _fundClient.FundClientPK);
                        cmd.Parameters.AddWithValue("@historyPK", _fundClient.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _fundClient.VoidUsersID);
                        cmd.Parameters.AddWithValue("@Notes", _fundClient.Notes);
                        cmd.Parameters.AddWithValue("@VoidTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update FundClient set status= 2, Notes = @Notes, LastUpdate=@LastUpdate where FundClientPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _fundClient.FundClientPK);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@Notes", _fundClient.Notes);
                        cmd.ExecuteNonQuery();
                    }

                    if (Tools.RDOSync)
                    {
                        ActivityReps _activityReps = new ActivityReps();
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandTimeout = 0;
                            cmd.CommandText = @"if exists(
                        Select * from FundClient 
                        where fundClientPK = @fundclientPK
                        and status = 2)
                        begin
	                        Select '1'  result
                        end
                        else 
                        begin
	                        Select '0'  result
                        end";
                            cmd.Parameters.AddWithValue("@FundClientPK", _fundClient.FundClientPK);
                            cmd.Parameters.AddWithValue("@historyPK", _fundClient.HistoryPK);
                            using (SqlDataReader dr = cmd.ExecuteReader())
                            {
                                if (dr.HasRows)
                                {
                                    dr.Read();
                                    if (dr["result"].ToString().Equals("1"))
                                    {
                                        var data = BackOffice.VerifyProfile(_fundClient.FundClientPK.ToString(), FoConnection.Authentication());
                                        _activityReps.Activity_LogInsert(DateTime.Now, "FundClient_A", true, "", "", "FO Interface Fund client Verify Reject", _fundClient.VoidUsersID, _fundClient.FundClientPK, 0, 0, "");
                                    }
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
        public void FundClient_Void(FundClient _fundClient)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update FundClient set status = 3, Notes = @Notes, VoidUsersID = @VoidUsersID,VoidTime = @VoidTime ,LastUpdate=@lastUpdate " +
                            "where FundClientPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _fundClient.FundClientPK);
                        cmd.Parameters.AddWithValue("@Notes", _fundClient.Notes);
                        cmd.Parameters.AddWithValue("@historyPK", _fundClient.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _fundClient.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
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
        public List<FundClientCombo> FundClientDetail_ComboRpt()
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<FundClientCombo> L_FundClient = new List<FundClientCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "SELECT  FundClientPK, ID + ' - ' + case when InvestorType = 1 then isnull(CONVERT(varchar(10), TanggalLahir, 20),'') + ',' + Name else isnull(CONVERT(varchar(10), TanggalBerdiri, 20),'') + ',' + Name end as ID, Name FROM [FundClient]  where status = 2 union all select 0,'All', '' order by FundClientPK,Name";
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    FundClientCombo M_FundClient = new FundClientCombo();
                                    M_FundClient.FundClientPK = Convert.ToInt32(dr["FundClientPK"]);
                                    M_FundClient.ID = Convert.ToString(dr["ID"]);
                                    M_FundClient.Name = Convert.ToString(dr["Name"]);
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
        public List<FundClientCombo> FundClientDetail_Combo()
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<FundClientCombo> L_FundClient = new List<FundClientCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "SELECT  FundClientPK, ID + ' - ' + case when InvestorType = 1 then isnull(CONVERT(varchar(10), TanggalLahir, 20),'') + ',' + Name else isnull(CONVERT(varchar(10), TanggalBerdiri, 20),'') + ',' + Name end as ID, Name FROM [FundClient]  where status = 2 order by ID,Name";
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    FundClientCombo M_FundClient = new FundClientCombo();
                                    M_FundClient.FundClientPK = Convert.ToInt32(dr["FundClientPK"]);
                                    M_FundClient.ID = Convert.ToString(dr["ID"]);
                                    M_FundClient.Name = Convert.ToString(dr["Name"]);
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

        public List<FundClientIdentity> FundClient_GetProfile(int _FundClientPK)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString1))
                {
                    DbCon.Open();
                    List<FundClientIdentity> L_FundClient = new List<FundClientIdentity>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"select ImageBase64 Base64,
                                            right(left(ImageBase64, charindex(',', ImageBase64)-8),len(left(ImageBase64, charindex(',', ImageBase64)-8))-charindex(':', ImageBase64)) Type 
                                            FROM IMAGE80.dbo.FundClientIdentity  where FundClientPK = @FundClientPK";
                        cmd.Parameters.AddWithValue("@FundClientPK", _FundClientPK);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    FundClientIdentity M_FundClient = new FundClientIdentity();
                                    M_FundClient.Base64 = Convert.ToString(dr["Base64"]);
                                    M_FundClient.Type = Convert.ToString(dr["Type"]);
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
        public List<FundClientCombo> FundClient_Combo()
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<FundClientCombo> L_FundClient = new List<FundClientCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "SELECT  FundClientPK,ID + ' - ' + Name as ID, Name FROM [FundClient]  where status = 2 order by ID,Name";
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    FundClientCombo M_FundClient = new FundClientCombo();
                                    M_FundClient.FundClientPK = Convert.ToInt32(dr["FundClientPK"]);
                                    M_FundClient.ID = Convert.ToString(dr["ID"]);
                                    M_FundClient.Name = Convert.ToString(dr["Name"]);
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
        public List<FundClientCombo> FundClient_ComboForTransaction()
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<FundClientCombo> L_FundClient = new List<FundClientCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"SELECT  FC.status,FC.FundClientPK,FC.ID + ' - ' + FC.Name as ID,FC.Name Name, IFUACode IFUA  FROM [FundClient] FC 
						  where FC.status = 2 ";
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    FundClientCombo M_FundClient = new FundClientCombo();
                                    M_FundClient.FundClientPK = Convert.ToInt32(dr["FundClientPK"]);
                                    M_FundClient.ID = Convert.ToString(dr["ID"]);
                                    M_FundClient.Name = Convert.ToString(dr["Name"]);
                                    M_FundClient.IFUA = Convert.ToString(dr["IFUA"]);
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
                        cmd.CommandText = @"select C.BankRecipientPK BankRecipientPK,C.Bank +  ' - ' + C.B as AccountNo from (  
                         select 1 BankRecipientPK,B.Name Bank,nomorrekening1 B from fundclient FC   
                         left join Bank B on FC.namabank1 = B.BankPK where fundclientpk = @FundClientPK and FC.status  = 2 and B.Status = 2    
                         union all    
                         select 2 BankRecipientPK,B.Name Bank,nomorrekening2 B from fundclient FC   
                         left join Bank B on FC.namabank2 = B.BankPK where fundclientpk = @FundClientPK and FC.status  = 2 and B.Status = 2   
                         union all    
                         select 3 BankRecipientPK,B.Name Bank,nomorrekening3 B from fundclient FC   
                         left join Bank B on FC.namabank3 = B.BankPK where fundclientpk = @FundClientPK and FC.status  = 2 and B.Status = 2    
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
        public List<FundClientCombo> FundClient_ComboRpt()
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<FundClientCombo> L_FundClient = new List<FundClientCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "SELECT  FundClientPK,ID, Name ,IFUACode, SID FROM [FundClient]  where status = 2 order by ID,Name";
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    FundClientCombo M_FundClient = new FundClientCombo();
                                    M_FundClient.FundClientPK = Convert.ToInt32(dr["FundClientPK"]);
                                    M_FundClient.ID = Convert.ToString(dr["ID"]);
                                    M_FundClient.Name = Convert.ToString(dr["Name"]);
                                    M_FundClient.IFUA = Convert.ToString(dr["IFUACode"]);
                                    M_FundClient.SID = Convert.ToString(dr["SID"]);
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
        public int GetDefaultBankRecipientCombo_ByFundClientPK(int _fundClientPK, int _fundPK)
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
                        Declare @BankPK int

                        Select @BankPK = BankRecipientPK from fundclientbankDefault A
                        Where A.Status = 2 and A.FundclientPK = @FundClientPK
                        and A.FundPK = 0


                        Select @BankPK = BankRecipientPK from fundclientbankDefault A
                        Where A.Status = 2 and A.FundclientPK = @FundClientPK
                        and A.FundPK = @FundPK


                        Select isnull(@BankPK,1) BankRecipientPK
                         ";
                        cmd.Parameters.AddWithValue("@FundClientPK", _fundClientPK);
                        cmd.Parameters.AddWithValue("@FundPK", _fundPK);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    return Convert.ToInt32(dr["BankRecipientPK"]);
                                }
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
        public string FundClient_GenerateARIAText(FundClient _fundClient)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_fundClient.ParamCategory == "Individual")
                        {
                            cmd.CommandText =
                               @"
                          BEGIN  
                              SET NOCOUNT ON         
          
                           create table #Text(      
                          [ResultText] [nvarchar](1000)  NULL          
                          )                        
        
                          truncate table #Text      
        
                              insert into #Text      
                              select isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(FC.IFUACode,'')))),'') + '|' 
	                          + isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(NamaDepanInd,'')))),'')  +    
                              '|' + isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(Case when NamaTengahInd  = '' then '0' else NamaTengahInd end))),'')  +       
                              '|' + isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(NamaBelakangInd,'0')))),'') + '|' +                
                              isnull(cast(case when IdentitasInd1 in (1,7) then 1 else 2 end  as nvarchar),'') + '|' 
	                          + isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(NoIdentitasInd1,'0')))),'') +         
                              '|' + isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(NPWP,0)))),'') + '|' 
	                          + isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(TempatLahir,'0')))),'') + '|'  +     
                              isnull(CONVERT(VARCHAR(8), TanggalLahir, 112) + '|' + cast(JenisKelamin as nvarchar),'') + '|'   +                       
                              isnull(cast(StatusPerkawinan as nvarchar),'') + '|' + case when Nationality = '85' then '1' else '2' end + '|' + isnull(cast(Pekerjaan as nvarchar),'')  +        
                              '|' + isnull(cast(Pendidikan as nvarchar),'') + '|' + isnull(cast(Agama as nvarchar),'') + '|' + isnull(cast(SumberDanaInd as nvarchar),'') + '|'  +    
                              isnull(cast(MaksudTujuanInd as nvarchar),'') + '|' + isnull(cast(PenghasilanInd as nvarchar),'') + '|' +                         
                              isnull(cast(RTRIM(LTRIM(isnull(OtherAlamatInd1,'0'))) as nvarchar(100)),'') +           
                              '|' + isnull(replace(KodeKotaInd1,'.',''),'') + '|' + isnull(cast(KodePosInd1 as nvarchar),'') + '|'   +       
                              isnull(cast(RTRIM(LTRIM(isnull(AlamatInd2,''))) as nvarchar(100)),'')   +                      
                              '|' + isnull(replace(KodeKotaInd2,'.',''),'') + '|' + isnull(cast(KodePosInd2 as nvarchar),'')   +  
                              '|' + isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(SID,0)))),'')                    
                              from  FundClient FC                             
                              where FC.ClientCategory = '1'                         
                              and FC.FundClientPK in (select fundClientPK from FundClientPosition where Date =  @ParamDate and UnitAmount >  0.0001   ) and FC.status in (1,2)         
            
                              order by FC.name asc  
                                         
                              select * from #text                               
                                 
                              END 

                        "
                               ;
                        }
                        else
                        {
                            cmd.CommandText =
                                @"
	                          BEGIN  
                              SET NOCOUNT ON         
      
                               create table #Text(      
                              [ResultText] [nvarchar](1000)  NULL          
                              )                        
             
                              truncate table #Text      
             
                              insert into #Text      
                              select isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(FC.IFUACode))),'') + 
	                          '|' + isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(NamaPerusahaan,'0')))),'') + '|' +     
                              isnull(cast(rtrim(ltrim(Negara)) as nvarchar),'') + '|' + isnull(cast(Tipe as nvarchar),'') +                     
                              '|' + isnull(cast(Karakteristik as nvarchar),'') + '|' + isnull(cast(rtrim(ltrim(dbo.AlphaRemoveExceptLetter(NPWP))) as nvarchar),'') +     
                              '|' + isnull(cast(rtrim(ltrim(NoSKD)) as nvarchar),'') + '(' + isnull(CONVERT(VARCHAR(8), TanggalBerdiri, 112),'') + ')' +'|' +          
                              isnull(cast(SumberDanaInstitusi as nvarchar),'') + '|' + isnull(cast(MaksudTujuanInstitusi as nvarchar),'') +      
                              '|' + isnull(cast(PenghasilanInstitusi as nvarchar),'') + '|' +               
                              isnull(cast(rtrim(ltrim(AlamatPerusahaan)) as nvarchar(100)),'') +          
                              '|' + isnull(replace(KodeKotaIns,'.','') + '|' + isnull(cast(KodePosIns as nvarchar),0),'')  +  
                              '|' + isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(SID,0)))),0)                  
                              from  FundClient FC             
                              where InvestorType = '2'         
                              and FC.FundClientPK in (select fundClientPK from FundClientPosition where Date = @ParamDate and UnitAmount >  0.0001   ) and FC.status = 2         
                              order by FC.Name Asc    
                              select * from #text      
             
                                  END     
                            ";

                        }
                        cmd.Parameters.AddWithValue("@ParamDate", _fundClient.ParamDate);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                if (_fundClient.ParamCategory == "Individual")
                                {
                                    string filePath = Tools.ARIATextPath + "IND002IND.txt";
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
                                        return Tools.HtmlARIATextPath + "IND002IND.txt";
                                    }
                                }
                                else
                                {
                                    string filePath = Tools.ARIATextPath + "IND002INS.txt";
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
                                        return Tools.HtmlARIATextPath + "IND002INS.txt";
                                    }

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

        public bool FundClient_GenerateARIA(string _userID, SInvestRpt _sInvestRpt)
        {
                try
                {
                    using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                    {
                        DbCon.Open();
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            if (_sInvestRpt.ParamCategory == "Individual")
                            {
                                cmd.CommandText =
                                   @"
                          BEGIN  
                              SET NOCOUNT ON         
          
                              select isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(FC.IFUACode,'')))),'') IFUACode,
							  isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(NamaDepanInd,'')))),'') NamaDepanInd,
							  isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(Case when NamaTengahInd  = '' then '' else NamaTengahInd end))),'') NamaTengahInd,
							  isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(NamaBelakangInd,'')))),'') NamaBelakangInd,           
                              isnull(cast(case when IdentitasInd1 in (1,7) then 1 else 2 end  as nvarchar),'') IdentitasInd1,
							  isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(NoIdentitasInd1,'')))),'') NoIdentitasInd1,
							  isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(NPWP,0)))),'') NPWP,
							  isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(TempatLahir,'')))),'') TempatLahir, 
                              isnull(CONVERT(VARCHAR(8), TanggalLahir, 112),0) TanggalLahir, 
							  cast(JenisKelamin as nvarchar) JenisKelamin,                
                              isnull(cast(StatusPerkawinan as nvarchar),'') StatusPerkawinan,
							  case when Nationality = '85' then '1' else '2' end Nationality,
							  isnull(cast(Pekerjaan as nvarchar),'') Pekerjaan, 
							  isnull(cast(Pendidikan as nvarchar),'') Pendidikan, 
							  isnull(cast(Agama as nvarchar),'') Agama, 
							  isnull(cast(SumberDanaInd as nvarchar),'') SumberDanaInd,    
                              isnull(cast(MaksudTujuanInd as nvarchar),'') MaksudTujuanInd, 
							  isnull(cast(PenghasilanInd as nvarchar),'') PenghasilanInd,                       
                              isnull(cast(RTRIM(LTRIM(isnull(OtherAlamatInd1,'0'))) as nvarchar(100)),'') OtherAlamatInd1, 
							  isnull(replace(KodeKotaInd1,'.',''),'') KodeKotaInd1,
							  isnull(cast(KodePosInd1 as nvarchar),'') KodePosInd1,      
                              isnull(cast(RTRIM(LTRIM(isnull(AlamatInd2,''))) as nvarchar(100)),'') AlamatInd2, 
							  isnull(replace(KodeKotaInd2,'.',''),'') KodeKotaInd2, 
							  isnull(cast(KodePosInd2 as nvarchar),'') KodePosInd2, 
							  isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(SID,0)))),'') SID                  
                              from  FundClient FC                             
                              where FC.ClientCategory = '1'                         
                              and FC.FundClientPK in (select fundClientPK from FundClientPosition where Date =  @ParamDate and UnitAmount >  0.0001   ) and FC.status in (1,2)         
            
                              order by FC.name asc  
                              END "
                                   ;


                                cmd.Parameters.AddWithValue("@ParamDate", _sInvestRpt.ParamDate);

                                using (SqlDataReader dr0 = cmd.ExecuteReader())
                                {
                                    if (!dr0.HasRows)
                                    {
                                        return false;
                                    }
                                    else
                                    {
                                        string filePath = Tools.ReportsPath + "ARIA_Indi" + "_" + _userID + ".xlsx";
                                        string pdfPath = Tools.ReportsPath + "ARIA_Indi" + "_" + _userID + ".pdf";
                                        FileInfo excelFile = new FileInfo(filePath);
                                        if (excelFile.Exists)
                                        {
                                            excelFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                            excelFile = new FileInfo(filePath);
                                        }


                                        // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                        using (ExcelPackage package = new ExcelPackage(excelFile))
                                        {
                                            package.Workbook.Properties.Title = "ARIAReport";
                                            package.Workbook.Properties.Author = Tools.DefaultReportAuthor();
                                            package.Workbook.Properties.Comments = Tools.DefaultReportComments();
                                            package.Workbook.Properties.Company = Tools.DefaultReportCompany();
                                            package.Workbook.Properties.SetCustomPropertyValue("Checked by", _userID);
                                            package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", Tools.DefaultReportAssemblyName());

                                            ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("ARIA Report");


                                            //ATUR DATA GROUPINGNYA DULU
                                            List<ARIA> rList = new List<ARIA>();
                                            while (dr0.Read())
                                            {
                                                ARIA rSingle = new ARIA();
                                                rSingle.IFUACode = Convert.ToString(dr0["IFUACode"]);
                                                rSingle.NamaDepanInd = Convert.ToString(dr0["NamaDepanInd"]);
                                                rSingle.NamaTengahInd = Convert.ToString(dr0["NamaTengahInd"]);
                                                rSingle.NamaBelakangInd = Convert.ToString(dr0["NamaBelakangInd"]);
                                                rSingle.IdentitasInd1 = Convert.ToString(dr0["IdentitasInd1"]);
                                                rSingle.NoIdentitasInd1 = Convert.ToString(dr0["NoIdentitasInd1"]);
                                                rSingle.NPWP = Convert.ToString(dr0["NPWP"]);
                                                rSingle.TempatLahir = Convert.ToString(dr0["TempatLahir"]);
                                                rSingle.TanggalLahir = Convert.ToString(dr0["TanggalLahir"]);
                                                rSingle.JenisKelamin = Convert.ToString(dr0["JenisKelamin"]);
                                                rSingle.StatusPerkawinan = Convert.ToString(dr0["StatusPerkawinan"]);
                                                rSingle.Nationality = Convert.ToString(dr0["Nationality"]);
                                                rSingle.Pekerjaan = Convert.ToString(dr0["Pekerjaan"]);
                                                rSingle.Pendidikan = Convert.ToString(dr0["Pendidikan"]);
                                                rSingle.Agama = Convert.ToString(dr0["Agama"]);
                                                rSingle.SumberDanaInd = Convert.ToString(dr0["SumberDanaInd"]);
                                                rSingle.MaksudTujuanInd = Convert.ToString(dr0["MaksudTujuanInd"]);
                                                rSingle.PenghasilanInd = Convert.ToString(dr0["PenghasilanInd"]);
                                                rSingle.OtherAlamatInd1 = Convert.ToString(dr0["OtherAlamatInd1"]);
                                                rSingle.KodeKotaInd1 = Convert.ToString(dr0["KodeKotaInd1"]);
                                                rSingle.KodePosInd1 = Convert.ToString(dr0["KodePosInd1"]);
                                                rSingle.AlamatInd2 = Convert.ToString(dr0["AlamatInd2"]);
                                                rSingle.KodeKotaInd2 = Convert.ToString(dr0["KodeKotaInd2"]);
                                                rSingle.KodePosInd2 = Convert.ToString(dr0["KodePosInd2"]);
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

                                                worksheet.Cells[incRowExcel, 1].Value = "IFUACode";
                                                worksheet.Cells[incRowExcel, 2].Value = "NamaDepanInd";
                                                worksheet.Cells[incRowExcel, 3].Value = "NamaTengahInd";
                                                worksheet.Cells[incRowExcel, 4].Value = "NamaBelakangInd";
                                                worksheet.Cells[incRowExcel, 5].Value = "IdentitasInd1";
                                                worksheet.Cells[incRowExcel, 6].Value = "NoIdentitasInd1";
                                                worksheet.Cells[incRowExcel, 7].Value = "NPWP";
                                                worksheet.Cells[incRowExcel, 8].Value = "TempatLahir";
                                                worksheet.Cells[incRowExcel, 9].Value = "TanggalLahir";
                                                worksheet.Cells[incRowExcel, 10].Value = "JenisKelamin";
                                                worksheet.Cells[incRowExcel, 11].Value = "StatusPerkawinan";
                                                worksheet.Cells[incRowExcel, 12].Value = "Nationality";
                                                worksheet.Cells[incRowExcel, 13].Value = "Pekerjaan";
                                                worksheet.Cells[incRowExcel, 14].Value = "Pendidikan";
                                                worksheet.Cells[incRowExcel, 15].Value = "Agama";
                                                worksheet.Cells[incRowExcel, 16].Value = "SumberDanaInd";
                                                worksheet.Cells[incRowExcel, 17].Value = "MaksudTujuanInd";
                                                worksheet.Cells[incRowExcel, 18].Value = "PenghasilanInd";
                                                worksheet.Cells[incRowExcel, 19].Value = "OtherAlamatInd1";
                                                worksheet.Cells[incRowExcel, 20].Value = "KodeKotaInd1";
                                                worksheet.Cells[incRowExcel, 21].Value = "KodePosInd1";
                                                worksheet.Cells[incRowExcel, 22].Value = "AlamatInd2";
                                                worksheet.Cells[incRowExcel, 23].Value = "KodeKotaInd2";
                                                worksheet.Cells[incRowExcel, 24].Value = "KodePosInd2";
                                                worksheet.Cells[incRowExcel, 25].Value = "SID";

                                                worksheet.Cells["A" + incRowExcel + ":Y" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + incRowExcel + ":Y" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + incRowExcel + ":Y" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + incRowExcel + ":Y" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                                //area header
                                                int _endRowDetail = 0;
                                                int _startRow = incRowExcel;
                                                incRowExcel++;
                                                _startRowDetail = incRowExcel;
                                                foreach (var rsDetail in rsHeader)
                                                {
                                                    worksheet.Cells[incRowExcel, 1].Value = rsDetail.IFUACode;
                                                    worksheet.Cells[incRowExcel, 2].Value = rsDetail.NamaDepanInd;
                                                    worksheet.Cells[incRowExcel, 3].Value = rsDetail.NamaTengahInd;
                                                    worksheet.Cells[incRowExcel, 4].Value = rsDetail.NamaBelakangInd;
                                                    worksheet.Cells[incRowExcel, 5].Value = rsDetail.IdentitasInd1;
                                                    worksheet.Cells[incRowExcel, 6].Value = rsDetail.NoIdentitasInd1;
                                                    worksheet.Cells[incRowExcel, 7].Value = rsDetail.NPWP;
                                                    worksheet.Cells[incRowExcel, 8].Value = rsDetail.TempatLahir;
                                                    worksheet.Cells[incRowExcel, 9].Value = rsDetail.TanggalLahir;
                                                    worksheet.Cells[incRowExcel, 10].Value = rsDetail.JenisKelamin;
                                                    worksheet.Cells[incRowExcel, 11].Value = rsDetail.StatusPerkawinan;
                                                    worksheet.Cells[incRowExcel, 12].Value = rsDetail.Nationality;
                                                    worksheet.Cells[incRowExcel, 13].Value = rsDetail.Pekerjaan;
                                                    worksheet.Cells[incRowExcel, 14].Value = rsDetail.Pendidikan;
                                                    worksheet.Cells[incRowExcel, 15].Value = rsDetail.Agama;
                                                    worksheet.Cells[incRowExcel, 16].Value = rsDetail.SumberDanaInd;
                                                    worksheet.Cells[incRowExcel, 17].Value = rsDetail.MaksudTujuanInd;
                                                    worksheet.Cells[incRowExcel, 18].Value = rsDetail.PenghasilanInd;
                                                    worksheet.Cells[incRowExcel, 19].Value = rsDetail.OtherAlamatInd1;
                                                    worksheet.Cells[incRowExcel, 20].Value = rsDetail.KodeKotaInd1;
                                                    worksheet.Cells[incRowExcel, 21].Value = rsDetail.KodePosInd1;
                                                    worksheet.Cells[incRowExcel, 22].Value = rsDetail.AlamatInd2;
                                                    worksheet.Cells[incRowExcel, 23].Value = rsDetail.KodeKotaInd2;
                                                    worksheet.Cells[incRowExcel, 24].Value = rsDetail.KodePosInd2;
                                                    worksheet.Cells[incRowExcel, 25].Value = rsDetail.SID;
                                                    worksheet.Cells["A" + incRowExcel + ":Y" + incRowExcel].Style.WrapText = true;

                                                    _endRowDetail = incRowExcel;

                                                    incRowExcel++;


                                                }
                                                worksheet.Row(incRowExcel).PageBreak = true;

                                                worksheet.Cells["A" + _startRow + ":A" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["Y" + _startRow + ":Y" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + _endRowDetail + ":Y" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                incRowExcel++;
                                            }
                                            string _rangeA = "A1:Y" + incRowExcel;
                                            using (ExcelRange r = worksheet.Cells[_rangeA]) // KALO  KOLOM 1 SAMPE 9 A-I
                                            {
                                                r.Style.Font.Size = 15;
                                            }


                                            worksheet.PrinterSettings.FitToPage = true;
                                            worksheet.PrinterSettings.FitToWidth = 1;
                                            worksheet.PrinterSettings.FitToHeight = 0;
                                            worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel - 1, 25];
                                            worksheet.Column(1).Width = 30;
                                            worksheet.Column(2).AutoFit();
                                            worksheet.Column(3).AutoFit();
                                            worksheet.Column(4).AutoFit(); 
                                            worksheet.Column(5).AutoFit();
                                            worksheet.Column(6).AutoFit();
                                            worksheet.Column(7).Width = 30;
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
                                            worksheet.Column(19).Width = 60;
                                            worksheet.Column(20).AutoFit();
                                            worksheet.Column(21).AutoFit();
                                            worksheet.Column(22).Width = 60;
                                            worksheet.Column(23).AutoFit();
                                            worksheet.Column(24).AutoFit();
                                            worksheet.Column(25).Width = 30;



                                            // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                            //worksheet.Cells["A3:O14"].AutoFitColumns();  // UNTUK AUTO FIT UKURAN TULISAN DENGAN KOLOM, KEBETULAN INI BUAT HEADER AJA DIPAKENYA

                                            worksheet.PrinterSettings.Orientation = eOrientation.Landscape; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                            worksheet.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN
                                            worksheet.HeaderFooter.OddHeader.RightAlignedText = "&12 ARIA REPORT";

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
                            else if (_sInvestRpt.ParamCategory == "Institution")
                            {

                                cmd.CommandText =
                                   @"
                               BEGIN
                               SET NOCOUNT ON         
         
                              select isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(FC.IFUACode))),'') IFUACode,
							  isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(NamaPerusahaan,'0')))),'') NamaPerusahaan,     
                              isnull(cast(rtrim(ltrim(Negara)) as nvarchar),'') Negara,
							  isnull(cast(Tipe as nvarchar),'') Tipe,
							  isnull(cast(Karakteristik as nvarchar),'') Karakteristik,
							  isnull(cast(rtrim(ltrim(dbo.AlphaRemoveExceptLetter(NPWP))) as nvarchar),'') NPWP,
							  isnull(cast(rtrim(ltrim(NoSKD)) as nvarchar),'') NoSKD,
							  isnull(CONVERT(VARCHAR(8), TanggalBerdiri, 112),'') TanggalBerdiri,          
                              isnull(cast(SumberDanaInstitusi as nvarchar),'') SumberDanaInstitusi, 
							  isnull(cast(MaksudTujuanInstitusi as nvarchar),'') MaksudTujuanInstitusi,
							  isnull(cast(PenghasilanInstitusi as nvarchar),'') PenghasilanInstitusi,              
                              isnull(cast(rtrim(ltrim(AlamatPerusahaan)) as nvarchar(100)),'') AlamatPerusahaan,
							  isnull(replace(KodeKotaIns,'.',''),'') KodeKotaIns,
							  isnull(cast(KodePosIns as nvarchar),0) KodePosIns,
							  isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(SID,0)))),0) SID                 
                              from  FundClient FC             
                              where InvestorType = '2'         
                              and FC.FundClientPK in (select fundClientPK from FundClientPosition where Date = @ParamDate and UnitAmount >  0.0001   ) and FC.status = 2         
                              order by FC.Name Asc      
             
                                  END"
                                   ;
                                cmd.Parameters.AddWithValue("@ParamDate", _sInvestRpt.ParamDate);

                                using (SqlDataReader dr0 = cmd.ExecuteReader())
                                {
                                    if (!dr0.HasRows)
                                    {
                                        return false;
                                    }
                                    else
                                    {
                                        string filePath = Tools.ReportsPath + "ARIA_Insti" + "_" + _userID + ".xlsx";
                                        string pdfPath = Tools.ReportsPath + "ARIA_Insti" + "_" + _userID + ".pdf";
                                        FileInfo excelFile = new FileInfo(filePath);
                                        if (excelFile.Exists)
                                        {
                                            excelFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                            excelFile = new FileInfo(filePath);
                                        }


                                        // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                        using (ExcelPackage package = new ExcelPackage(excelFile))
                                        {
                                            package.Workbook.Properties.Title = "ARIAReport";
                                            package.Workbook.Properties.Author = Tools.DefaultReportAuthor();
                                            package.Workbook.Properties.Comments = Tools.DefaultReportComments();
                                            package.Workbook.Properties.Company = Tools.DefaultReportCompany();
                                            package.Workbook.Properties.SetCustomPropertyValue("Checked by", _userID);
                                            package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", Tools.DefaultReportAssemblyName());

                                            ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("ARIA Report");


                                            //ATUR DATA GROUPINGNYA DULU
                                            List<ARIA> rList = new List<ARIA>();
                                            while (dr0.Read())
                                            {
                                                ARIA rSingle = new ARIA();
                                                rSingle.IFUACode = Convert.ToString(dr0["IFUACode"]);
                                                rSingle.NamaPerusahaan = Convert.ToString(dr0["NamaPerusahaan"]);
                                                rSingle.Negara = Convert.ToString(dr0["Negara"]);
                                                rSingle.Tipe = Convert.ToString(dr0["Tipe"]);
                                                rSingle.Karakteristik = Convert.ToString(dr0["Karakteristik"]);
                                                rSingle.NPWP = Convert.ToString(dr0["NPWP"]);
                                                rSingle.NoSKD = Convert.ToString(dr0["NoSKD"]);
                                                rSingle.TanggalBerdiri = Convert.ToString(dr0["TanggalBerdiri"]);
                                                rSingle.SumberDanaInstitusi = Convert.ToString(dr0["SumberDanaInstitusi"]);
                                                rSingle.MaksudTujuanInstitusi = Convert.ToString(dr0["MaksudTujuanInstitusi"]);
                                                rSingle.PenghasilanInstitusi = Convert.ToString(dr0["PenghasilanInstitusi"]);
                                                rSingle.AlamatPerusahaan = Convert.ToString(dr0["AlamatPerusahaan"]);
                                                rSingle.KodeKotaIns = Convert.ToString(dr0["KodeKotaIns"]);
                                                rSingle.KodePosIns = Convert.ToString(dr0["KodePosIns"]);
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

                                                worksheet.Cells[incRowExcel, 1].Value = "IFUACode";
                                                worksheet.Cells[incRowExcel, 2].Value = "NamaPerusahaan";
                                                worksheet.Cells[incRowExcel, 3].Value = "Negara";
                                                worksheet.Cells[incRowExcel, 4].Value = "Tipe";
                                                worksheet.Cells[incRowExcel, 5].Value = "Karakteristik";
                                                worksheet.Cells[incRowExcel, 6].Value = "NPWP";
                                                worksheet.Cells[incRowExcel, 7].Value = "NoSKD";
                                                worksheet.Cells[incRowExcel, 8].Value = "TanggalBerdiri";
                                                worksheet.Cells[incRowExcel, 9].Value = "SumberDanaInstitusi";
                                                worksheet.Cells[incRowExcel, 10].Value = "MaksudTujuanInstitusi";
                                                worksheet.Cells[incRowExcel, 11].Value = "PenghasilanInstitusi";
                                                worksheet.Cells[incRowExcel, 12].Value = "AlamatPerusahaan";
                                                worksheet.Cells[incRowExcel, 13].Value = "KodeKotaIns";
                                                worksheet.Cells[incRowExcel, 14].Value = "KodePosIns";
                                                worksheet.Cells[incRowExcel, 15].Value = "SID";

                                                worksheet.Cells["A" + incRowExcel + ":O" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + incRowExcel + ":O" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + incRowExcel + ":O" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + incRowExcel + ":O" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                                //area header
                                                int _endRowDetail = 0;
                                                int _startRow = incRowExcel;
                                                incRowExcel++;
                                                _startRowDetail = incRowExcel;
                                                foreach (var rsDetail in rsHeader)
                                                {
                                                    worksheet.Cells[incRowExcel, 1].Value = rsDetail.IFUACode;
                                                    worksheet.Cells[incRowExcel, 2].Value = rsDetail.NamaPerusahaan;
                                                    worksheet.Cells[incRowExcel, 3].Value = rsDetail.Negara;
                                                    worksheet.Cells[incRowExcel, 4].Value = rsDetail.Tipe;
                                                    worksheet.Cells[incRowExcel, 5].Value = rsDetail.Karakteristik;
                                                    worksheet.Cells[incRowExcel, 6].Value = rsDetail.NPWP;
                                                    worksheet.Cells[incRowExcel, 7].Value = rsDetail.NoSKD;
                                                    worksheet.Cells[incRowExcel, 8].Value = rsDetail.TanggalBerdiri;
                                                    worksheet.Cells[incRowExcel, 9].Value = rsDetail.SumberDanaInstitusi;
                                                    worksheet.Cells[incRowExcel, 10].Value = rsDetail.MaksudTujuanInstitusi;
                                                    worksheet.Cells[incRowExcel, 11].Value = rsDetail.PenghasilanInstitusi;
                                                    worksheet.Cells[incRowExcel, 12].Value = rsDetail.AlamatPerusahaan;
                                                    worksheet.Cells[incRowExcel, 13].Value = rsDetail.KodeKotaIns;
                                                    worksheet.Cells[incRowExcel, 14].Value = rsDetail.KodePosIns;
                                                    worksheet.Cells[incRowExcel, 15].Value = rsDetail.SID;

                                                    worksheet.Cells["A" + incRowExcel + ":O" + incRowExcel].Style.WrapText = true;

                                                    incRowExcel++;


                                                }

                                                _endRowDetail = incRowExcel;
                                                worksheet.Row(incRowExcel).PageBreak = true;

                                                worksheet.Cells["A" + _endRowDetail + ":O" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["A" + _startRowDetail + ":A" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                worksheet.Cells["O" + _startRowDetail + ":O" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                                incRowExcel++;
                                            }
                                            string _rangeA = "A1:O" + incRowExcel;
                                            using (ExcelRange r = worksheet.Cells[_rangeA]) // KALO  KOLOM 1 SAMPE 9 A-I
                                            {
                                                r.Style.Font.Size = 15;
                                            }



                                            worksheet.PrinterSettings.FitToPage = true;
                                            worksheet.PrinterSettings.FitToWidth = 1;
                                            worksheet.PrinterSettings.FitToHeight = 0;
                                            worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel - 1, 15];
                                            worksheet.Column(1).Width = 25;
                                            worksheet.Column(2).Width = 60;
                                            worksheet.Column(3).Width = 10;
                                            worksheet.Column(4).Width = 20;
                                            worksheet.Column(5).Width = 20;
                                            worksheet.Column(6).Width = 30;
                                            worksheet.Column(7).Width = 30;
                                            worksheet.Column(8).Width = 20;
                                            worksheet.Column(9).Width = 30;
                                            worksheet.Column(10).Width = 30;
                                            worksheet.Column(11).Width = 30;
                                            worksheet.Column(12).Width = 60;
                                            worksheet.Column(13).Width = 20;
                                            worksheet.Column(14).Width = 20;
                                            worksheet.Column(15).Width = 25;



                                            // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                            //worksheet.Cells["A3:O14"].AutoFitColumns();  // UNTUK AUTO FIT UKURAN TULISAN DENGAN KOLOM, KEBETULAN INI BUAT HEADER AJA DIPAKENYA

                                            worksheet.PrinterSettings.Orientation = eOrientation.Portrait; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                            worksheet.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN
                                            worksheet.HeaderFooter.OddHeader.RightAlignedText = "&12 ARIA REPORT";

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
                            else
                            {
                                return false;
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

        //public string FundClient_GenerateNewClientID(int _clientCategory)
        //{

        //    try
        //    {
        //        using (SqlConnection DbCon = new SqlConnection(Tools.conString))
        //        {
        //            DbCon.Open();
        //            using (SqlCommand cmd = DbCon.CreateCommand())
        //            {

        //                ////INDOASIA
        //                //cmd.CommandText =
        //                //" Declare @ClientCategory  nvarchar(100) " +  
        //                //" Declare @NewClientID  nvarchar(100) " +   
        //                //" Declare @MaxClientName  nvarchar(100)  " +
        //                //" Declare @Period int                " +               
        //                //" Declare @LENdigit int           " +     
        //                //" Declare @NewDigit Nvarchar(20)       " +            
        //                //" \n " +                     
        //                //" select @MaxClientName =   SUBSTRING ( ID ,11 , 3 )+1   from FundClient   " +     
        //                //" where ClientCategory=@ClientCategoryPK order by ID      " +           
        //                //" \n " +
        //                //" select @Period = (RIGHT(CONVERT(VARCHAR(8), getdate(), 3), 2))     " +         
        //                //" \n " +    
        //                //" select @ClientCategory = MAX(left(DescOne,3)) from FundClient F left join MasterValue MV on F.ClientCategory = MV.Code " +
        //                //" and MV.ID ='ClientCategory' and MV.status = 2  where ClientCategory = @ClientCategoryPK " +
        //                //" \n " +   
        //                //" select @LENdigit = LEN(@MaxClientName)        " +            
        //                //" \n " +                       
        //                //" if @LENdigit = 1  " +                  
        //                //" BEGIN                 " +   
        //                //" set @NewDigit = '00' + CAST(@MaxClientName as nvarchar)        " +            
        //                //" END                " +   
        //                //" if @LENdigit = 2         " +          
        //                //" BEGIN                 " +   
        //                //" set @NewDigit = '0' + CAST(@MaxClientName as nvarchar)        " +            
        //                //" END           " +   
        //                //" \n " +
        //                //" set @NewClientID =  CAST(@Period as nvarchar)+ 'RHB' + CAST(@ClientCategory as nvarchar) + CAST(@NewDigit as nvarchar)  " +
        //                //" \n " +
        //                //" Select @NewClientID   NewClientID   ";



        //                //RHB
        //                cmd.CommandText =
        //               " Declare @ClientCategory  nvarchar(100) " +
        //                " Declare @NewClientID  nvarchar(100) " +
        //                " Declare @MaxClientName  nvarchar(100)  " +
        //                " Declare @Period int                " +
        //                " Declare @LENdigit int           " +
        //                " Declare @NewDigit Nvarchar(20)       " +
        //                " \n " +
        //                " select @MaxClientName =   SUBSTRING ( ID ,6 , 4 )+1   from FundClient   " +
        //                " where ClientCategory=@ClientCategoryPK and status in (1,2) order by ID      " +
        //                " \n " +
        //                " select @Period = (RIGHT(CONVERT(VARCHAR(8), getdate(), 3), 2))     " +
        //                " \n " +
        //                " select @ClientCategory = MAX(left(DescOne,3)) from FundClient F left join MasterValue MV on F.ClientCategory = MV.Code " +
        //                " and MV.ID ='ClientCategory' and MV.status = 2  where ClientCategory = @ClientCategoryPK " +
        //                " \n " +
        //                " select @LENdigit = LEN(@MaxClientName)        " +
        //                " \n " +
        //                " if @LENdigit = 1  " +
        //                " BEGIN                 " +
        //                " set @NewDigit = '000' + CAST(@MaxClientName as nvarchar)        " +
        //                " END                " +
        //                " if @LENdigit = 2         " +
        //                " BEGIN                 " +
        //                " set @NewDigit = '00' + CAST(@MaxClientName as nvarchar)        " +
        //                " END           " +
        //                " if @LENdigit = 3         " +
        //                " BEGIN                 " +
        //                " set @NewDigit = '0' + CAST(@MaxClientName as nvarchar)        " +
        //                " END           " +
        //                " else         " +
        //                " BEGIN                 " +
        //                " set @NewDigit =  CAST(@MaxClientName as nvarchar)        " +
        //                " END           " +
        //                " \n " +
        //                " set @NewClientID =  CAST(@Period as nvarchar) + CAST(@ClientCategory as nvarchar) + CAST(@NewDigit as nvarchar)  " +
        //                " \n " +
        //                " Select @NewClientID   NewClientID   ";

        //                cmd.Parameters.AddWithValue("@ClientCategoryPK", _clientCategory);

        //                using (SqlDataReader dr = cmd.ExecuteReader())
        //                {
        //                    if (!dr.Read())
        //                    {
        //                        return "";
        //                    }
        //                    else
        //                    {
        //                        return Convert.ToString(dr["NewClientID"]);
        //                    }
        //                }
        //            }
        //        }

        //    }
        //    catch (Exception err)
        //    {
        //        throw err;
        //    }

        //}
        public List<FundClientTrx> Get_FundClientFromFundClientPositionSummary(int _fundPK, DateTime _date)
        {


            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<FundClientTrx> L_FundClient = new List<FundClientTrx>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = @"
                           select FC.ID + ' - ' + FC.Name as ID,FC.FundClientPK , FC.Name FundClientName,FC.IFUACode,FP.Unit UnitAmount 
                          from FundClientPositionSummary FP    
                          inner join FundClient FC on FP.FundClientPK  = FC.FundClientPK and FC.status in (1,2) 
                          where FP.FundPK =@FundPK and FP.Unit > 0 Order By FC.Name 
                            ";

                        cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                        cmd.Parameters.AddWithValue("@Date", _date);



                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {

                                    {
                                        FundClientTrx M_FundClient = new FundClientTrx();
                                        M_FundClient.FundClientPK = Convert.ToInt32(dr["FundClientPK"]);
                                        M_FundClient.ID = Convert.ToString(dr["ID"]);
                                        M_FundClient.FundClientName = Convert.ToString(dr["FundClientName"]);
                                        M_FundClient.IFUA = Convert.ToString(dr["IFUACode"]);
                                        M_FundClient.UnitAmount = Convert.ToDecimal(dr["UnitAmount"]);
                                        L_FundClient.Add(M_FundClient);


                                    }
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



        public List<FundClientTrx> Get_FundClientFromFundClientPosition(int _fundPK, DateTime _date)
        {


            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<FundClientTrx> L_FundClient = new List<FundClientTrx>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = @"
  
  Declare @MaxEDT datetime
                        Select @MaxEDT = max(Valuedate) from EndDayTrails where status = 2 and ValueDate <= @Date

                        set @MaxEDT = dbo.FWorkingDay(@MaxEDT,1)


						if object_id('tempdb..#tablePositionUnit', 'u') is not null drop table #tablePositionUnit 
	                    create table #tablePositionUnit
	                    (
		                    FundPK int,
							FundClientPk int,
							Date date
	                    )
	                    CREATE CLUSTERED INDEX indx_tablePositionUnit ON #tablePositionUnit (FundPK,FundClientPK);

						insert into #tablePositionUnit
						select distinct FundPK,FundClientPK,max(date) from FundClientPosition where Date <= @Date
						group by FundPK,FundClientPK

                        Select A.FundClientPK,D.ID + ' - ' + D.Name as ID,D.Name FundClientName,D.IFUACode, isnull(A.UnitAmount,0) - isnull(B.UnitAmount,0) - isnull(C.UnitAmount,0) UnitAmount from FundClientPosition A
                        left join (
                        SELECT FundClientPK,FundPK, sum(isnull(case when UnitAmount > 0 then UnitAmount else CashAmount / dbo.FgetLastCloseNav(NAVDate,FundPK) end,0)) UnitAmount FROM ClientRedemption  WHERE status not in (3,4) and posted = 0 and Revised = 0 and Notes <> 'Pending Revised'
                        group by FundClientPK,FundPK
                        ) B on A.FundClientPK = B.FundClientPK and A.FundPK = B.FundPK 

                        left join 
                        (
                        SELECT FundClientPK,FundPKFrom, sum(isnull(case when UnitAmount > 0 then UnitAmount else CashAmount / dbo.FgetLastCloseNav(NAVDate,FundPKFrom) end,0)) UnitAmount FROM ClientSwitching  WHERE status not in (3,4) and posted = 0 and Revised = 0 and Notes <> 'Pending Revised'
                        group by FundClientPK,FundPKFrom
                        ) C on A.FundClientPK = C.FundClientPK and A.FundPK = C.FundPKFrom
                        left join FundClient D on A.FundClientPK = D.FundClientPK and D.status in (1,2) 
						inner join #tablePositionUnit E on A.FundPK = E.FundPK and A.FundClientPK = E.FundClientPk and A.Date = E.Date
                        where A.FundPK = @FundPK 
                        and isnull(A.UnitAmount,0) - isnull(B.UnitAmount,0) - isnull(C.UnitAmount,0) > 0

                        Order By D.Name
                       ";

                        cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                        cmd.Parameters.AddWithValue("@Date", _date);



                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {

                                    {
                                        FundClientTrx M_FundClient = new FundClientTrx();
                                        M_FundClient.FundClientPK = Convert.ToInt32(dr["FundClientPK"]);
                                        M_FundClient.ID = Convert.ToString(dr["ID"]);
                                        M_FundClient.FundClientName = Convert.ToString(dr["FundClientName"]);
                                        M_FundClient.IFUA = Convert.ToString(dr["IFUACode"]);
                                        M_FundClient.UnitAmount = Convert.ToDecimal(dr["UnitAmount"]);
                                        L_FundClient.Add(M_FundClient);


                                    }
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

        public decimal FundClient_GetUnitPosition(int _fundPK, DateTime _date, int _fundClientPK)
        {


            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();

                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = @" 
                        Declare @MaxEDT datetime
                        Select @MaxEDT = max(Valuedate) from EndDayTrails where status = 2 and ValueDate <= @Date

                        set @MaxEDT = dbo.FWorkingDay(@MaxEDT,1)

                        Select isnull(A.UnitAmount,0) - isnull(B.UnitAmount,0) - isnull(C.UnitAmount,0) Unit
                        FROM FundClientPosition A
                        left join (
	                        SELECT FundClientPK,FundPK, sum(isnull(case when UnitAmount > 0 then UnitAmount else CashAmount / dbo.FgetLastCloseNav(NAVDate,FundPK) end,0)) UnitAmount FROM ClientRedemption  
	                        WHERE status not in (3,4) and posted = 0 and Revised = 0 AND FundPK = @FundPK AND FundClientPK = @FundClientPK
	                        group by FundClientPK,FundPK
                        ) B on A.FundClientPK = B.FundClientPK and A.FundPK = B.FundPK 

                        left join 
                        (
	                        SELECT FundClientPK,FundPKFrom, sum(isnull(case when UnitAmount > 0 then UnitAmount else CashAmount / dbo.FgetLastCloseNav(NAVDate,FundPKFrom) end,0)) UnitAmount FROM ClientSwitching  
	                        WHERE status not in (3,4) and posted = 0 and Revised = 0 AND FundPKFrom = @FundPK AND FundCLientPK = @FundClientPK
	                        group by FundClientPK,FundPKFrom
                        ) C on A.FundClientPK = C.FundClientPK and A.FundPK = C.FundPKFrom 
                        where Date = @MaxEDT  AND A.FundPK = @FundPK AND A.FundClientPK = @FundClientPK  ";

                        cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                        cmd.Parameters.AddWithValue("@Date", _date);
                        cmd.Parameters.AddWithValue("@FundClientPK", _fundClientPK);



                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return Convert.ToDecimal(dr["Unit"]);

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

        public decimal FundClient_GetEstimatedCashProjection(int _fundPK, string _date, decimal _unitPosition)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();

                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandTimeout = 0;
                        cmd.CommandText = @" 
                        create table #Temp
                        (
                        LastNav numeric(22,4),
                        FundPK int
                        )
                        insert into #Temp
                        select Distinct isnull([dbo].[FgetLastCloseNav] (@NAVDate,@FundPK),0),@FundPK 

                        select LastNav * @UnitPosition  EstimatedCashProjection  from #Temp A left join CloseNAV B on A.FundPK = B.FundPK where status = 2  ";

                        cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                        cmd.Parameters.AddWithValue("@NAVDate", _date);
                        cmd.Parameters.AddWithValue("@UnitPosition", _unitPosition);



                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                decimal _test = Convert.ToDecimal(dr["EstimatedCashProjection"]);
                                return Convert.ToDecimal(dr["EstimatedCashProjection"]);

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

        public string FundClient_GenerateNKPD(DateTime _date)
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
                        cmd.Parameters.AddWithValue("@Date", _date);

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
                                    return Tools.HtmlARIATextPath + "NKPD01.txt";
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

        public Boolean GenerateReportNKPD(string _userID, SInvestRpt _sInvestRpt)
        {
            #region NKPD
            if (_sInvestRpt.ReportName.Equals("3"))
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
where g.ClientCategory = 1 and g.nationality= 'ID'
and CS.Date = @FinalDate and CS.UnitAmount > 1     
and g.SACode = ''      
            
group by CS.FundPK            
) A On FCP.FundPK = A.FundPK            
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG        
on CS.FundClientPK=CG.FundClientPK    and CG.Status in  (1,2)       
where CG.ClientCategory = 1 and CG.nationality= 'ID'
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
where CG.ClientCategory = 2 and CG.Negara= 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe IN (7)   
and CG.SACode = ''               
            
group by CS.FundPK            
) C On FCP.FundPK = C.FundPK            
             
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK=CG.FundClientPK and CG.Status in  (1,2)          
where CG.ClientCategory = 2 and CG.Negara= 'ID'            
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
where CG.ClientCategory = 2 and CG.Negara= 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=6   
and CG.SACode = ''            
            
group by CS.FundPK            
) E On FCP.FundPK = E.FundPK            
             
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK=CG.FundClientPK  and CG.Status in  (1,2)           
where CG.ClientCategory = 2 and CG.Negara= 'ID'            
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
where CG.ClientCategory = 2 and CG.Negara= 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe=4    
and CG.SACode = ''           
            
group by CS.FundPK            
) G On FCP.FundPK = G.FundPK            
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK=CG.FundClientPK  and CG.Status in  (1,2)     
where CG.ClientCategory = 2 and CG.Negara= 'ID'            
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
where CG.ClientCategory = 2 and CG.Negara= 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe=3 and CG.Karakteristik IN (1,2,3,4,5,6,7)   
and CG.SACode = ''              
            
group by CS.FundPK            
) I On FCP.FundPK = I.FundPK            
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK=CG.FundClientPK  and CG.Status in  (1,2)           
where CG.ClientCategory = 2 and CG.Negara= 'ID'            
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
where CG.ClientCategory = 2 and CG.Negara= 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe=1 and CG.Karakteristik IN (2,3,4,5,6,7)    
and CG.SACode = ''          
            
group by CS.FundPK            
) K On FCP.FundPK = K.FundPK            
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK=CG.FundClientPK  and CG.Status in  (1,2)           
where CG.ClientCategory = 2 and CG.Negara= 'ID'            
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
where CG.ClientCategory = 2 and CG.Negara= 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe=1 and CG.Karakteristik = 1     
and CG.SACode = ''          
            
group by CS.FundPK            
) M On FCP.FundPK = M.FundPK            
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK=CG.FundClientPK  and CG.Status in  (1,2)          
where CG.ClientCategory = 2 and CG.Negara= 'ID'            
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
where CG.ClientCategory = 2 and CG.Negara= 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe=1 and CG.Karakteristik = 8   
and CG.SACode = ''              
            
group by CS.FundPK            
) O On FCP.FundPK = O.FundPK            
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK=CG.FundClientPK  and CG.Status in  (1,2)          
where CG.ClientCategory = 2 and CG.Negara= 'ID'            
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
where CG.ClientCategory = 2 and CG.Negara= 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe=2   
and CG.SACode = ''             
            
group by CS.FundPK            
) Q On FCP.FundPK = Q.FundPK            
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK=CG.FundClientPK and CG.Status in  (1,2)            
where CG.ClientCategory = 2 and CG.Negara= 'ID'            
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
where CG.ClientCategory = 2 and CG.Negara= 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe= 3 and CG.Karakteristik = 8  
and CG.SACode = ''            
            
group by CS.FundPK            
) S On FCP.FundPK = S.FundPK            
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK=CG.FundClientPK  and CG.Status in  (1,2)           
where CG.ClientCategory = 2 and CG.Negara= 'ID'            
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
where CG.ClientCategory = 2 and CG.Negara= 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe in (5,8)  
and CG.SACode = ''              
            
group by CS.FundPK            
) U On FCP.FundPK = U.FundPK            
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK=CG.FundClientPK  and CG.Status in  (1,2)          
where CG.ClientCategory = 2 and CG.Negara= 'ID'            
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
where CG.ClientCategory = 1 and CG.nationality <> 'ID'
and CS.Date = @FinalDate and CS.UnitAmount > 0     
and CG.SACode = ''           
            
group by CS.FundPK            
) AA On FCP.FundPK = AA.FundPK            
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK=CG.FundClientPK    and CG.Status in  (1,2)         
where CG.ClientCategory = 1 and CG.nationality <> 'ID'
and CS.Date = @FinalDate and CS.UnitAmount > 0    
and CG.SACode = ''             
            
group by CS.FundPK            
) AB On FCP.FundPK = AB.FundPK            
             
LEFT JOIN             
(            
select COUNT(*) jumlahPerusahaanEfekAsing, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK = CG.FundClientPK  and CG.Status in  (1,2)          
where CG.ClientCategory = 2 and CG.Negara<> 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe IN (7)     
and CG.SACode = ''        
            
group by CS.FundPK          
) AC On FCP.FundPK = AC.FundPK            
             
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK=CG.FundClientPK  and CG.Status in  (1,2)         
where CG.ClientCategory = 2 and CG.Negara<> 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe IN (7)   
and CG.SACode = ''         
            
group by CS.FundPK            
) AD On FCP.FundPK = AD.FundPK            
             
             
LEFT JOIN             
(            
select COUNT(*) jumlahDanaPensiunAsing, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK = CG.FundClientPK and CG.Status in  (1,2)     
where CG.ClientCategory = 2 and CG.Negara<> 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=6        
and CG.SACode = ''     
            
group by CS.FundPK            
) AE On FCP.FundPK = AE.FundPK            
             
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK=CG.FundClientPK and CG.Status in  (1,2)          
where CG.ClientCategory = 2 and CG.Negara<> 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=6  
and CG.SACode = ''           
            
group by CS.FundPK            
) AF On FCP.FundPK = AF.FundPK            
             
LEFT JOIN             
(            
select COUNT(*) jumlahAsuransiAsing, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK = CG.FundClientPK and CG.Status in  (1,2)           
where CG.ClientCategory = 2 and CG.Negara<> 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe=4     
and CG.SACode = ''        
            
group by CS.FundPK            
) AG On FCP.FundPK = AG.FundPK            
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK=CG.FundClientPK and CG.Status in  (1,2)          
where CG.ClientCategory = 2 and CG.Negara<> 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=4   
and CG.SACode = ''           
            
group by CS.FundPK            
) AH On FCP.FundPK = AH.FundPK            
             
LEFT JOIN             
(            
select COUNT(*) jumlahBankAsing, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK = CG.FundClientPK and CG.Status in  (1,2)           
where CG.ClientCategory = 2 and CG.Negara<> 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe=3 and CG.Karakteristik IN (1,2,3,4,5,6,7)  
and CG.SACode = ''          
            
group by CS.FundPK            
) AI On FCP.FundPK = AI.FundPK            
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK=CG.FundClientPK  and CG.Status in  (1,2)         
where CG.ClientCategory = 2 and CG.Negara<> 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=3 and CG.Karakteristik IN (1,2,3,4,5,6,7)  
and CG.SACode = ''            
            
group by CS.FundPK            
) AJ On FCP.FundPK = AJ.FundPK            
             
LEFT JOIN             
(            
select COUNT(*) jumlahPTAsing, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK = CG.FundClientPK  and CG.Status in  (1,2)         
where CG.ClientCategory = 2 and CG.Negara<> 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe=1 and CG.Karakteristik IN (2,3,4,5,6,7)   
and CG.SACode = ''          
            
group by CS.FundPK            
) AK On FCP.FundPK = AK.FundPK            
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK=CG.FundClientPK  and CG.Status in  (1,2)        
where CG.ClientCategory = 2 and CG.Negara<> 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=1 and CG.Karakteristik IN (2,3,4,5,6,7)    
and CG.SACode = ''           
            
group by CS.FundPK            
) AL On FCP.FundPK = AL.FundPK            
             
LEFT JOIN             
(            
select COUNT(*) jumlahBUMNAsing, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK = CG.FundClientPK   and CG.Status in  (1,2)         
where CG.ClientCategory = 2 and CG.Negara<> 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=1 and CG.Karakteristik =1   
and CG.SACode = ''            
            
group by CS.FundPK            
) AM On FCP.FundPK = AM.FundPK            
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK=CG.FundClientPK  and CG.Status in  (1,2)         
where CG.ClientCategory = 2 and CG.Negara<> 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=1 and CG.Karakteristik =1      
and CG.SACode = ''       
            
group by CS.FundPK            
) AN On FCP.FundPK = AN.FundPK            
             
LEFT JOIN             
(            
select COUNT(*) jumlahBUMDAsing, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK = CG.FundClientPK and CG.Status in  (1,2)           
where CG.ClientCategory = 2 and CG.Negara<>'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=1 and CG.Karakteristik = 8   
and CG.SACode = ''        
            
group by CS.FundPK            
) AO On FCP.FundPK = AO.FundPK            
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK=CG.FundClientPK and CG.Status in  (1,2)         
where CG.ClientCategory = 2 and CG.Negara<> 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=1 and CG.Karakteristik = 8 
and CG.SACode = ''             
            
group by CS.FundPK            
) AP On FCP.FundPK = AP.FundPK            
             
LEFT JOIN             
(            
select COUNT(*) jumlahYayasanAsing, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK = CG.FundClientPK and CG.Status in  (1,2)          
where CG.ClientCategory = 2 and CG.Negara<> 'ID'          
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=2    
and CG.SACode = ''         
            
group by CS.FundPK            
) AQ On FCP.FundPK = AQ.FundPK            
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK=CG.FundClientPK  and CG.Status in  (1,2)         
where CG.ClientCategory = 2 and CG.Negara<> 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=2   
and CG.SACode = ''           
            
group by CS.FundPK            
) AR On FCP.FundPK = AR.FundPK            
             
LEFT JOIN             
(            
select COUNT(*) jumlahKoperasiAsing, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK = CG.FundClientPK  and CG.Status in  (1,2)         
where CG.ClientCategory = 2 and CG.Negara<> 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe= 3 and CG.Karakteristik = 8   
and CG.SACode = ''            
            
group by CS.FundPK            
) SS On FCP.FundPK = SS.FundPK            
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK=CG.FundClientPK and CG.Status in  (1,2)         
where CG.ClientCategory = 2 and CG.Negara<> 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe= 3 and CG.Karakteristik = 8  
and CG.SACode = ''            
            
group by CS.FundPK            
) AT On FCP.FundPK = AT.FundPK            
             
LEFT JOIN             
(            
select COUNT(*) jumlahLembagaAsingLainnya, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK = CG.FundClientPK and CG.Status in  (1,2)           
where CG.ClientCategory = 2 and CG.Negara<> 'ID'            
and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe in (5,8)   
and CG.SACode = ''           
            
group by CS.FundPK            
) AU On FCP.FundPK = AU.FundPK            
             
LEFT JOIN             
(            
select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
left join FundClient CG            
on CS.FundClientPK=CG.FundClientPK  and CG.Status in  (1,2)         
where CG.ClientCategory = 2 and CG.Negara<> 'ID'            
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
                            cmd.Parameters.AddWithValue("@date", _sInvestRpt.ParamDate);


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
            #endregion

            else
            {
                return false;
            }
        }

//        public string FundClient_GenerateKPD(DateTime _Date, int _Fund)
//        {
//            try
//            {
//                DateTime _datetimeNow = DateTime.Now;
//                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
//                {
//                    DbCon.Open();
//                    using (SqlCommand cmd = DbCon.CreateCommand())
//                    {

//                        cmd.CommandTimeout = 0;
//                        cmd.CommandText = @"
//
//                       --drop table #Text
//                        create table #Text(      
//                        [ResultText] [nvarchar](1000)  NULL          
//                        )                        
//        
//                        truncate table #Text          
//
//                        --drop Table #KPD
//                        Create Table #KPD
//                        (PK int,KodeNasabah nvarchar(50),NamaNasabah nvarchar(50) ,NomorKontrak nvarchar(50),TanggalKontrak nvarchar(50),TanggalJatuhTempo nvarchar(50),
//                        NomorAdendum nvarchar(50), TanggalAdendum nvarchar(50),NilaiInvestasiAwalIDR nvarchar(50), NilaiInvestasiAwalNonIDR nvarchar(50),NilaiInvestasiAkhir nvarchar(50),
//                        NilaiInvestasiAkhirNonIDR nvarchar(50), JenisEfek nvarchar(50), DNatauLN int,JumlahEfek nvarchar(50),NilaiPembelian nvarchar(50), NilaiNominal nvarchar(50),
//                        HPW nvarchar(50), Deposito nvarchar(50), TotalNilai nvarchar(50),KodeBK  nvarchar(50)
//                        )
//
//                        DECLARE A CURSOR FOR 
//                        select FundPK
//                        from Fund
//                        where [Status] = 2 and FundTypeInternal = 2
//                        Open A
//                        Fetch Next From A
//                        Into @FundPK
//
//                        While @@FETCH_STATUS = 0
//                        Begin
//
//
//
//
//
//                        Insert into #KPD (PK,KodeNasabah ,NamaNasabah ,NomorKontrak,TanggalKontrak,TanggalJatuhTempo,
//                        NomorAdendum, TanggalAdendum,NilaiInvestasiAwalIDR, NilaiInvestasiAwalNonIDR,NilaiInvestasiAkhir,
//                        NilaiInvestasiAkhirNonIDR, JenisEfek, DNatauLN,JumlahEfek,NilaiPembelian, NilaiNominal,
//                        HPW, Deposito, TotalNilai,KodeBK)
//                        select ROW_NUMBER() OVER(ORDER BY C.ID ASC) AS PK,'1' KodeNasabah ,A.Name NamaNasabah ,NKPDName NomorKontrak,isnull(CONVERT(VARCHAR(8), A.EffectiveDate, 112),0) TanggalKontrak,isnull(CONVERT(VARCHAR(8), A.MaturityDate, 112),0) TanggalJatuhTempo,
//                        0 NomorAdendum,0 TanggalAdendum,cast(isnull(TotalUnits,CAST(TotalUnits AS DECIMAL(22, 0))) * isnull(A.Nav,CAST(A.Nav AS DECIMAL(22, 0)))as DECIMAL(22, 0)) NilaiInvestasiAwalIDR,0 NilaiInvestasiAwalNonIDR,CAST(sum(F.AUM) + isnull(E.CashAmount,0) AS DECIMAL(22, 0)) NilaiInvestasiAkhir,
//                        0 NilaiInvestasiAkhirNonIDR, C.ID JenisEfek,1 DNatauLN,B.Balance JumlahEfek,CAST(B.CostValue AS DECIMAL(22, 0)) NilaiPembelian,CAST(0 AS DECIMAL(22, 2)) NilaiNominal,
//                        CAST(B.ClosePrice AS DECIMAL(22, 0)) HPW,CAST(0 AS DECIMAL(22, 2)) Deposito,0 TotalNilai,isnull(D.ID,0) KodeBK from Fund A
//                        left join FundPosition B on A.FundPK = B.FundPK and B.Status = 2
//                        left join Instrument C on B.InstrumentPK = C.InstrumentPK and C.Status = 2 
//                        left join BankBranch D on A.BankBranchPK = D.BankBranchPK and D.status  = 2
//                        left join DistributedIncome E on A.FundPK = E.FundPK and E.status  = 2 and E.ValueDate <= @Date and Posted = 1
//                        left join CloseNAV F on A.FundPK = F.FundPK and F.Status = 2 and F.Date = @Date
//                        where A.FundPK = @FundPK and B.Date = @Date and A.status = 2
//                        Group By C.ID,A.Name,NKPDName,A.EffectiveDate,A.MaturityDate,A.TotalUnits,A.Nav,E.CashAmount,B.Balance,B.CostValue,B.ClosePrice,D.ID
//                        order By C.ID asc
//
//                        Fetch next From A Into @FundPK
//                        end
//                        Close A
//                        Deallocate A
//
//                        update #KPD set KodeNasabah = 0,NamaNasabah = 0,TanggalKontrak = 0,TanggalJatuhTempo = 0,
//                        NomorAdendum = 0, TanggalAdendum = 0,NilaiInvestasiAwalIDR = CAST(0 AS DECIMAL(32, 2)), NilaiInvestasiAwalNonIDR = 0,NilaiInvestasiAkhir = CAST(0 AS DECIMAL(22, 2)),
//                        NilaiInvestasiAkhirNonIDR = 0, KodeBK = 0 where PK <> 1
//
//
//
//                        insert into #Text 
//
//                        select isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(KodeNasabah,'')))),'') +
//                        '|' + isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(NamaNasabah,'')))),'')  +    
//                        '|' + isnull(RTRIM(LTRIM(isnull(NomorKontrak,''))),'')  +  
//                        '|' + isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(TanggalKontrak,'')))),'')  +  
//                        '|' + isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(TanggalJatuhTempo,'')))),'')  +  
//                        '|' + isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(NomorAdendum,'')))),'')  +  
//                        '|' + isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(TanggalAdendum,'')))),'')  +  
//                        '|' + isnull(RTRIM(LTRIM(isnull(NilaiInvestasiAwalIDR,''))),'')  +  
//                        '|' + isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(NilaiInvestasiAwalNonIDR,'')))),'')  + 
//                        '|' + isnull(RTRIM(LTRIM(isnull(NilaiInvestasiAkhir,''))),'')  +
//                        '|' + isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(NilaiInvestasiAkhirNonIDR,'')))),'')  +  
//                        '|' + isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(JenisEfek,'')))),'')  +  
//                        '|' + isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(DNatauLN,'')))),'')  + 
//                        '|' + isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(JumlahEfek,'')))),'')  +  
//                        '|' + isnull(RTRIM(LTRIM(isnull(NilaiPembelian,''))),'')  +  
//                        '|' + isnull(RTRIM(LTRIM(isnull(NilaiNominal,''))),'')  +  
//                        '|' + isnull(RTRIM(LTRIM(isnull(HPW,''))),'')  +  
//                        '|' + isnull(RTRIM(LTRIM(isnull(Deposito,''))),'')  +  
//                        '|' + isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(TotalNilai,'')))),'')  +   
//                        '|' + isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(KodeBK,'')))),'') 
//                        from #KPD
//
//
//                        select * from #Text
//
//                         ";
//                        cmd.Parameters.AddWithValue("@Date", _Date);
//                        cmd.Parameters.AddWithValue("@FundPK", _Fund);

//                        using (SqlDataReader dr = cmd.ExecuteReader())
//                        {
//                            if (dr.HasRows)
//                            {


//                                string filePath = Tools.ARIATextPath + "AH002KPD.txt";
//                                FileInfo txtFile = new FileInfo(filePath);
//                                if (txtFile.Exists)
//                                {
//                                    txtFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
//                                }

//                                using (System.IO.StreamWriter file = new System.IO.StreamWriter(filePath))
//                                {
//                                    while (dr.Read())
//                                    {
//                                        file.WriteLine(Convert.ToString(dr["ResultText"]));
//                                    }
//                                    return Tools.HtmlARIATextPath + "AH002KPD.txt";
//                                }

//                            }
//                            return null;
//                        }

//                    }
//                }
//            }
//            catch (Exception err)
//            {
//                throw err;
//            }

//        }

        public string FundClient_SInvest_BankAccount(FundClient _fundClient)
        {
            try
            {
                string paramFundClientSelected = "";
                paramFundClientSelected = "FundClientPK in (" + _fundClient.FundClientSelected + ") ";

                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"
                                    BEGIN  
                                    SET NOCOUNT ON  
                                    Select A.IFUACode  +'|' + A.BICCode+'|' + A.BIMemberCode  +'|' +A.NamaBank  +'|' +A.Country 
                                        +'|' + A.BankBranchName  +'|' +A.Currency  +'|' + A.NoRek  +'|' +A.NamaNasabah Result
                                    from 
                                    (
	                                    Select IFUACode,case when B.Country <> 'ID' then isnull(B.SInvestID,'') else '' end AS BICCode,
	                                    case when B.Country = 'ID' then isnull(B.BICode,'') else '' end  as BIMemberCode,
	                                    B.Name NamaBank,C.Code Country,
	                                    BankBranchName1 BankBranchName,D.DescOne Currency,NomorRekening1 NoRek,NamaNasabah1 NamaNasabah
	                                    from FundClient A
	                                    left join Bank B on A.NamaBank1 = B.BankPK and B.status = 2
	                                    left join MasterValue C on B.Country = C.Code and C.Id = 'SDICountry' and C.Status = 2
                                        left join MasterValue D on A.MataUang1 = D.Code and D.Id = 'MataUang' and D.Status = 2
	                                    where " + paramFundClientSelected + @" and A.status = 2 and NamaBank1 is not null and NamaBank1 > 0

	                                    UNION ALL

	                                    Select IFUACode,case when B.Country <> 'ID' then isnull(B.SInvestID,'') else '' end AS BICCode,
	                                    case when B.Country = 'ID' then isnull(B.BICode,'') else '' end  as BIMemberCode,
	                                    B.Name NamaBank,C.Code Country,
	                                    BankBranchName1 BankBranchName,D.DescOne Currency,NomorRekening2 NoRek,NamaNasabah1 NamaNasabah
	                                    from FundClient A
	                                    left join Bank B on A.NamaBank2 = B.BankPK and B.status = 2
	                                    left join MasterValue C on B.Country = C.Code and C.Id = 'SDICountry' and C.Status = 2
                                        left join MasterValue D on A.MataUang2 = D.Code and D.Id = 'MataUang' and D.Status = 2
	                                    where " + paramFundClientSelected + @" and A.status = 2 and NamaBank2 is not null and NamaBank2 > 0

	                                    UNION ALL

	                                    Select IFUACode,case when B.Country <> 'ID' then isnull(B.SInvestID,'') else '' end AS BICCode,
	                                    case when B.Country = 'ID' then isnull(B.BICode,'') else '' end  as BIMemberCode,
	                                    B.Name NamaBank,C.Code Country,
	                                    BankBranchName1 BankBranchName,D.DescOne Currency,NomorRekening3 NoRek,NamaNasabah1 NamaNasabah
	                                    from FundClient A
	                                    left join Bank B on A.NamaBank3 = B.BankPK and B.status = 2
	                                    left join MasterValue C on B.Country = C.Code and C.Id = 'SDICountry' and C.Status = 2
                                        left join MasterValue D on A.MataUang3 = D.Code and D.Id = 'MataUang' and D.Status = 2
	                                    where " + paramFundClientSelected + @" and A.status = 2 and NamaBank3 is not null and NamaBank3 > 0
                                    )A        
                                        END
                                   
                           ";
                        cmd.CommandTimeout = 0;

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {

                                {
                                    string filePath = Tools.SInvestTextPath + "S-Invest_BankAccount.txt";
                                    FileInfo txtFile = new FileInfo(filePath);
                                    if (txtFile.Exists)
                                    {
                                        txtFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                    }

                                    using (System.IO.StreamWriter file = new System.IO.StreamWriter(filePath))
                                    {
                                        while (dr.Read())
                                        {
                                            file.WriteLine(Convert.ToString(dr["Result"]));
                                        }
                                        return Tools.HtmlSinvestTextPath + "S-Invest_BankAccount.txt";
                                    }

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

        public string FundClient_SInvest_BankAccountVA(int _fundClientPK)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"
                                    BEGIN  
                                    SET NOCOUNT ON  
                                    Select A.IFUACode  +'|' + A.BICCode+'|' + A.BIMemberCode  +'|' +A.NamaBank  +'|' +A.Country 
                                        +'|' + A.BankBranchName  +'|' +A.Currency  +'|' + A.NoRek  +'|' +A.NamaNasabah Result
                                    from 
                                    (
	                                    Select IFUACode,case when B.Country <> 'ID' then isnull(B.SInvestID,'') else '' end AS BICCode,
	                                    case when B.Country = 'ID' then isnull(B.BICode,'') else '' end  as BIMemberCode,
	                                    isnull(B.Name,'') NamaBank,isnull(C.Code,'') Country,
	                                    BankBranchName1 BankBranchName,D.DescOne Currency,A.AccountNo NoRek,A.AccountName NamaNasabah
	                                    from ZRDO_80_BANK A
	                                    left join FundClient E on A.FundClientPK = E.FundClientPK and E.Status = 2
	                                    left join ZRDO_80_BANK_MAPPING F on A.BankName = F.PartnerCode
	                                    left join Bank B on F.RadsoftCode = B.ID and B.status = 2
	                                    left join MasterValue C on B.Country = C.Code and C.Id = 'SDICountry' and C.Status = 2
	                                    left join MasterValue D on E.MataUang1 = D.Code and D.Id = 'MataUang' and D.Status = 2
	                                    where A.FundClientPK = @FundClientPK and A.Status = 2
                                    )A        
                                    END
                                   
                           ";
                        cmd.CommandTimeout = 0;
                        cmd.Parameters.AddWithValue("@FundClientPK", _fundClientPK);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {

                                {
                                    string filePath = Tools.SInvestTextPath + "S-Invest_BankAccount.txt";
                                    FileInfo txtFile = new FileInfo(filePath);
                                    if (txtFile.Exists)
                                    {
                                        txtFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                    }

                                    using (System.IO.StreamWriter file = new System.IO.StreamWriter(filePath))
                                    {
                                        while (dr.Read())
                                        {
                                            file.WriteLine(Convert.ToString(dr["Result"]));
                                        }
                                        return Tools.HtmlSinvestTextPath + "S-Invest_BankAccount.txt";
                                    }

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

        public bool FundClient_SInvest(string _userID, string _category, int _fundClientPKFrom, int _fundClientPKTo, string _type, FundClient _FundClient)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        string paramFundClientSelected = "";
                        paramFundClientSelected = "FC.FundClientPK in (" + _FundClient.FundClientSelected + ") ";

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

                            if (_type == "1")
                            {

                                cmd.CommandText = @"

BEGIN  
SET NOCOUNT ON         
select @Type  
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
+ '|' + case when OtherAlamatInd1 = '0' then '' else REPLACE(REPLACE(RTRIM(LTRIM(isnull(OtherAlamatInd1,''))),char(13),''),char(10),'') end
+ '|' + case when OtherKodeKotaInd1 = '0' then '' else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(cast(OtherKodeKotaInd1 as nvarchar(4)),'')))) end     
+ '|' + case when OtherKodePosInd1 = '0' then '' else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(OtherKodePosInd1 ,'')))) end      
+ '|' + case when AlamatInd1 = '0' then '' else   REPLACE(REPLACE(RTRIM(LTRIM(isnull(AlamatInd1,''))),char(13),''),char(10),'') end      
+ '|' + case when KodeKotaInd1 = '0' then '' else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(cast(KodeKotaInd1 as nvarchar(4)),'')))) end  
+ '|' + isnull(MV13.DescOne,'')                                    
+ '|' + case when KodePosInd1 = '0' then '' else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(KodePosInd1 ,'')))) end    
+ '|' + isnull(CountryofCorrespondence,'')  
+ '|' + case when AlamatInd2 = '0' then '' else REPLACE(REPLACE(RTRIM(LTRIM(isnull(AlamatInd2,''))),char(13),''),char(10),'') end   
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
+ '|' +  case when B1.Country = 'ID' then case when isnull(B1.SInvestID,'') <> '' and  isnull(B1.BICode,'') <> '' then '' else isnull(B1.SInvestID,'') end else '' end 
+ '|' + case when B1.Country = 'ID' then isnull(B1.BICode,'') else '' end                           
+ '|' + isnull(B1.Name,'') 
+ '|' + isnull(B1.Country,'') 
+ '|' + case when BankBranchName1 = '0' then '' else isnull(cast(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(BankBranchName1 ,'')))) as nvarchar(100)),'') end 
+ '|' + isnull(MV15.DescOne,'') 
+ '|' + case when NomorRekening1 = '0' then '' else isnull(cast(NomorRekening1 as nvarchar(50)),'') end
+ '|' + case when NamaNasabah1 = '0' then '' else isnull(cast(NamaNasabah1 as nvarchar(100)),'') end 
+ '|' +  case when B2.Country = 'ID' then isnull(B2.SInvestID,'') else '' end  
+ '|' + case when B2.Country = 'ID' then isnull(B2.BICode,'') else '' end   
+ '|' + isnull(B2.Name,'') 
+ '|' + isnull(B2.Country,'') 
+ '|' + case when BankBranchName2 = '0' then '' else isnull(cast(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(BankBranchName2 ,'')))) as nvarchar(100)),'') end 
+ '|' + isnull(MV16.DescOne,'') 
+ '|' + case when NomorRekening2 = '0' then '' else isnull(cast(NomorRekening2 as nvarchar(50)),'') end 
+ '|' + case when NamaNasabah2 = '0' then '' else isnull(cast(NamaNasabah2 as nvarchar(100)),'') end   
+ '|' +  case when B3.Country = 'ID' then isnull(B3.SInvestID,'') else '' end  
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
where FC.Status = 2 and FC.InvestorType = 1  and " + paramFundClientSelected + @" 
                                " + _paramFundClientPK + @" 
                                order by FC.name asc 

                                 END    ";
                            }
                            else
                            {
                                cmd.CommandText = @"


BEGIN  
SET NOCOUNT ON         
select @Type  
+'|' + @CompanyID    
+ '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(SID,'')))) 
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
+ '|' + case when OtherAlamatInd1 = '0' then '' else REPLACE(REPLACE(RTRIM(LTRIM(isnull(OtherAlamatInd1,''))),char(13),''),char(10),'') end
+ '|' + case when OtherKodeKotaInd1 = '0' then '' else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(cast(OtherKodeKotaInd1 as nvarchar(4)),'')))) end     
+ '|' + case when OtherKodePosInd1 = '0' then '' else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(OtherKodePosInd1 ,'')))) end      
+ '|' + case when AlamatInd1 = '0' then '' else   REPLACE(REPLACE(RTRIM(LTRIM(isnull(AlamatInd1,''))),char(13),''),char(10),'') end      
+ '|' + case when KodeKotaInd1 = '0' then '' else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(cast(KodeKotaInd1 as nvarchar(4)),'')))) end  
+ '|' + isnull(MV13.DescOne,'')                                    
+ '|' + case when KodePosInd1 = '0' then '' else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(KodePosInd1 ,'')))) end    
+ '|' + isnull(CountryofCorrespondence,'')  
+ '|' + case when AlamatInd2 = '0' then '' else REPLACE(REPLACE(RTRIM(LTRIM(isnull(AlamatInd2,''))),char(13),''),char(10),'') end   
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
+ '|' + ''
+ '|' + ''
+ '|' + ''
+ '|' + ''
+ '|' + ''
+ '|' + ''
+ '|' + ''
+ '|' + ''
+ '|' + ''
+ '|' + ''
+ '|' + ''
+ '|' + ''
+ '|' + ''
+ '|' + ''
+ '|' + ''
+ '|' + ''
+ '|' + ''
+ '|' + ''
+ '|' + ''
+ '|' + ''
+ '|' + ''
+ '|' + ''
+ '|' + ''
+ '|' + ''                                                                    
+ '|' + '' ResultText                                     
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
where FC.Status = 2 and FC.InvestorType = 1  and " + paramFundClientSelected + @"
" + _paramFundClientPK + @" 
order by FC.name asc 

 END   

";
                            }




                            cmd.Parameters.AddWithValue("@CompanyID", _host.Get_CompanyID());
                            cmd.Parameters.AddWithValue("@Type", _type);
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
                      select @Type  
+'|' + @CompanyID     
+'|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(SID,''))))    
+'|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(FC.Name,''))))       
+'|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(Negara,''))))  
+'|' + RTRIM(LTRIM(case when NomorSIUP = '0' then '' else isnull(cast(NomorSIUP as nvarchar(40)),'') end))   
+'|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(case when CONVERT(VARCHAR(10), SIUPExpirationDate, 112) <> '19000101' then CONVERT(VARCHAR(10), SIUPExpirationDate, 112) else '' End))) 
+'|' + case when NoSKD = '0' then '' else RTRIM(LTRIM(isnull(cast(NoSKD as nvarchar(40)),''))) end
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
+'|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(CompanyCityName,'')))) 
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
+'|' + case when fc.NPWPPerson1 = '0' then '' else isnull(fc.NPWPPerson1,'') end  
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
+'|' + case when fc.NPWPPerson2 = '0' then '' else isnull(fc.NPWPPerson2,'') end  
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
where FC.Status = 2 and FC.InvestorType = 2 and " + paramFundClientSelected + @"

" + _paramFundClientPK + @" 
order by FC.name asc  
                        END ";



                            cmd.Parameters.AddWithValue("@CompanyID", _host.Get_CompanyID());
                            cmd.Parameters.AddWithValue("@Type", _type);
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

        public bool FundClient_GenerateCBestInterface(string _userID)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        string filePath = "";

                        filePath = Tools.ReportsPath + "GenerateCBestInterface" + "_" + _userID + ".xlsx";

                        FileInfo excelFile = new FileInfo(filePath);
                        if (excelFile.Exists)
                        {
                            excelFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                            excelFile = new FileInfo(filePath);
                        }

                        // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                        ExcelPackage package = new ExcelPackage(excelFile);
                        package.Workbook.Properties.Title = "Listing";
                        package.Workbook.Properties.Author = Tools.DefaultReportAuthor();
                        package.Workbook.Properties.Comments = Tools.DefaultReportComments();
                        package.Workbook.Properties.Company = Tools.DefaultReportCompany();
                        package.Workbook.Properties.SetCustomPropertyValue("Checked by", _userID);
                        package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", Tools.DefaultReportAssemblyName());


                        cmd.CommandText = @"
                                    BEGIN  
                                    SET NOCOUNT ON  
                                    
                                    Select 'CREATION' Action,isnull(B.DescOne,'') InvestorType,'' InvestorClientType,'' AccountLocalCode,'' AccountClientCode,'' AccountTaxCode,
                                    Case when ClientCategory = 1 then NamaDepanInd else NamaPerusahaan End InvestorFirstName,NamaTengahInd InvestorMiddleName,NamaBelakangInd InvestorLastName,isnull(C.DescOne,'') InvestorNationality,
                                    NoIdentitasInd1 InvestorKTPNumber,isnull((case when CONVERT(VARCHAR(10), ExpiredDateIdentitasInd1, 112) <> '19000101' then CONVERT(VARCHAR(10), ExpiredDateIdentitasInd1, 112) else '' End),'') InvestorKTPExpiredDate,NPWP InvestorNPWPNumber,isnull((case when CONVERT(VARCHAR(10), RegistrationNPWP, 112) <> '19000101' then CONVERT(VARCHAR(10), RegistrationNPWP, 112) else '' End),'') InvestorNPWPRegistrationDate,'' InvestorPassportNumber, '' InvestorPassportExpiredDate, NoSKD InvestorKitasSKDNumber,isnull((case when CONVERT(VARCHAR(10), ExpiredDateSKD, 112) <> '19000101' then CONVERT(VARCHAR(10), ExpiredDateSKD, 112) else '' End),'') InvestorKitasSKDExpiredDate,
                                    Case when ClientCategory = 1 then TempatLahir else LokasiBerdiri End InvestorBirthPlace,Case when ClientCategory = 1 then isnull((case when CONVERT(VARCHAR(10), TanggalLahir, 112) <> '19000101' then CONVERT(VARCHAR(10), TanggalLahir, 112) else '' End),'') else isnull((case when CONVERT(VARCHAR(10), TanggalBerdiri, 112) <> '19000101' then CONVERT(VARCHAR(10), TanggalBerdiri, 112) else '' End),'') End InvestorBirthDate,Case when ClientCategory = 1 then AlamatInd1 else AlamatPerusahaan End InvestorAddress1,AlamatInd2 InvestorAddress2,'' InvestorAddress3,'' InvestorCity,'' InvestorProvince,
                                    Case when ClientCategory = 1 then KodePosInd1 else KodePosIns End InvestorPostalCode,
                                    isnull(D.ParentCode,'') InvestorCountry,Case when ClientCategory = 1 then TeleponRumah else TeleponBisnis End InvestorHomePhone,TeleponSelular InvestorMobilePhone,Email InvestorEmail,Fax InvestorFax,OtherAlamatInd1 InvestorOtherAddress1,OtherAlamatInd2 InvestorOtherAddress2,'' InvestorOtherAddress3,'' InvestorOtherCity,'' InvestorOtherProvince,
                                    OtherKodePosInd1 InvestorOtherPostalCode,isnull(E.ParentCode,'') InvestorOtherCountry,OtherTeleponRumah InvestorOtherHomePhone,OtherTeleponSelular InvestorOtherMobilePhone,
                                    OtherEmail InvestorOtherEmail,OtherFax InvestorOtherFax,isnull(F.DescOne,'') InvestorSex,isnull(G.DescOne,'') InvestorMaritalStatus,isnull(SpouseName,'') InvestorSpouseName,
                                    AhliWaris InvestorHeirName,HubunganAhliWaris InvestorHeirRelation,isnull(H.DescOne,'') InvestorEducationalBackground,isnull(I.DescOne,'') InvestorOccupation,'' InvestorOccupationText,isnull(J.DescOne,'') InvestorNatureofBusiness,
                                    isnull(K.DescOne,'') InvestorIncomePerAnnum,isnull(L.DescOne,'') InvestorFundSource,'' InvestorFundSourceText,Description AccountDescription,
                                    isnull(M.Name,'') InvestorBankAccountName1,NomorRekening1 InvestorBankAccountNumber1,BIMemberCode1 InvestorBankAccountBICCode1Name,NamaNasabah1 InvestorBankAccountHolderName1,MataUang1 InvestorBankAccountCurrency1,
                                    isnull(N.Name,'') InvestorBankAccountName2,NomorRekening2 InvestorBankAccountNumber2,BIMemberCode2 InvestorBankAccountBICCode2Name,NamaNasabah2 InvestorBankAccountHolderName2,MataUang2 InvestorBankAccountCurrency2,
                                    isnull(O.Name,'') InvestorBankAccountName3,NomorRekening3 InvestorBankAccountNumber3,BIMemberCode3 InvestorBankAccountBICCode3Name,NamaNasabah3 InvestorBankAccountHolderName3,MataUang3 InvestorBankAccountCurrency3,
                                    isnull(P.DescOne,'') InvestorInvestmentObjective,isnull(MotherMaidenName,'') InvestorMothersMaidenName,'' DirectSid,isnull(Q.DescOne,'') AssetOwner from FundClient A
                                    left join MasterValue B on A.ClientCategory = B.Code and B.ID ='ClientCategory' and B.Status = 2 
                                    left join MasterValue C on A.Nationality = C.Code and C.ID ='Nationality' and C.Status = 2 
                                    left join MasterValue D on A.Negara = D.Code and D.ID ='SDICountry' and D.Status = 2 
                                    left join MasterValue E on A.OtherNegaraInd1 = E.Code and E.ID ='SDICountry' and E.Status = 2 
                                    left join MasterValue F on A.JenisKelamin = F.Code and F.ID ='Sex' and F.Status = 2 
                                    left join MasterValue G on A.StatusPerkawinan = G.Code and G.ID ='MaritalStatus' and G.Status = 2 
                                    left join MasterValue H on A.Pendidikan = H.Code and H.ID ='EducationalBackground' and H.Status = 2 
                                    left join MasterValue I on A.Pekerjaan = I.Code and I.ID ='Occupation' and I.Status = 2 
                                    left join MasterValue J on A.NatureOfBusiness = J.Code and J.ID ='HRBusiness' and J.Status = 2
                                    left join MasterValue K on A.PenghasilanInd = K.Code and K.ID ='IncomeIND' and K.Status = 2 
                                    left join MasterValue L on A.PenghasilanInd = L.Code and L.ID ='IncomeSourceIND' and L.Status = 2   
                                    left join Bank M on A.NamaBank1 = M.BankPK and M.Status = 2 
                                    left join Bank N on A.NamaBank2 = N.BankPK and N.Status = 2 
                                    left join Bank O on A.NamaBank2 = O.BankPK and O.Status = 2 
                                    left join MasterValue P on A.PenghasilanInd = P.Code and P.ID ='InvestmentObjectivesIND' and P.Status = 2  
                                    left join MasterValue Q on A.AssetOwner = Q.Code and Q.ID ='AssetOwner' and Q.Status = 2  
                                    where A.Status  = 2

                                    END
                                   
                           ";
                        using (SqlDataReader dr1 = cmd.ExecuteReader())
                        {
                            if (dr1.HasRows)
                            {
                                // BUAT NAMBAH SHEET DI WORKBOOK
                                ExcelWorksheet worksheetApproved = package.Workbook.Worksheets.Add("ListingApproved");
                                // KASI JUDUL FIELD YANG MAU DITAMPILIN DI BARIS KE-1

                                //  KASI STYLES ATAU CSS UNTUK JUDUL FIELD DI BARIS KE-1
                                using (ExcelRange r = worksheetApproved.Cells["A1:CE1"]) // KOLOM 1 SAMPE 10 A-J
                                {
                                    //NILAINYA NGAMBIL DARI DEFAULT DI TOOLS, KLO MAU BEDA SENDIRI BOLEH2 AJA.
                                    //r.Style.Font.Color.SetColor(Tools.DefaultReportColumnHeaderFontColor());
                                    //r.Style.HorizontalAlignment = Tools.DefaultReportColumnHeaderHorizontalAlignment();
                                    //r.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    //r.Style.Fill.BackgroundColor.SetColor(Tools.DefaultReportColumnHeaderBackgroundColor());
                                    //r.Style.Font.Size = Tools.DefaultReportColumnHeaderFontSize();
                                    //r.Style.Font.Bold = Tools.DefaultReportColumnHeaderFontBold();
                                    //r.Style.Border.Top.Style = Tools.DefaultReportColumnHeaderBorderTop();
                                    //r.Style.Border.Bottom.Style = Tools.DefaultReportColumnHeaderBorderBottom();
                                }

                                //int incRowExcel = 2;
                                //while (dr1.Read())
                                //{
                                //    int incColExcel = 1;
                                //    for (int inc1 = 0; inc1 < dr1.FieldCount; inc1++)
                                //    {
                                //        worksheetApproved.Cells[incRowExcel, incColExcel].Value = dr1.GetValue(inc1).ToString();
                                //        incColExcel++;
                                //    }
                                //    incRowExcel++;
                                //}

                                int incRowExcel;
                                int incColExcel = 1;
                                incRowExcel = 2;
                                // ini buat header
                                for (int inc1 = 0; inc1 < dr1.FieldCount; inc1++)
                                {
                                    worksheetApproved.Cells[1, incColExcel].Value = dr1.GetName(inc1).ToString();
                                    incColExcel++;
                                }
                                while (dr1.Read())
                                {
                                    incColExcel = 1;
                                    for (int inc1 = 0; inc1 < dr1.FieldCount; inc1++)
                                    {
                                        worksheetApproved.Cells[incRowExcel, incColExcel].Value = dr1.GetValue(inc1).ToString();
                                        incColExcel++;
                                    }
                                    incRowExcel++;
                                }

                                // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                worksheetApproved.Cells["A1:CE1"].AutoFitColumns(); // UNTUK AUTO FIT UKURAN TULISAN DENGAN KOLOM, KEBETULAN INI BUAT HEADER AJA DIPAKENYA
                                worksheetApproved.PrinterSettings.Orientation = eOrientation.Landscape; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                worksheetApproved.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN
                                worksheetApproved.HeaderFooter.OddHeader.RightAlignedText = "&14 C-BEST INTERFACE";

                                // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                worksheetApproved.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
                                worksheetApproved.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
                                worksheetApproved.View.PageLayoutView = Tools.DefaultReportPageLayoutView();
                                worksheetApproved.View.ShowGridLines = Tools.DefaultReportShowGridLines();
                                worksheetApproved.HeaderFooter.OddHeader.CenteredText = Tools.DefaultReportHeaderCenterText();
                                worksheetApproved.HeaderFooter.OddHeader.LeftAlignedText = Tools.DefaultReportHeaderLeftText();

                                worksheetApproved.HeaderFooter.OddFooter.RightAlignedText =
                                string.Format("Page {0} of {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);

                                worksheetApproved.HeaderFooter.OddFooter.CenteredText = Tools.DefaultReportFooterCenterText();
                                worksheetApproved.HeaderFooter.OddFooter.LeftAlignedText = Tools.DefaultReportFooterLeftText();
                                package.Save();
                                package.Dispose();
                                return true;

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
        public void FundClient_SuspendBySelectedData(string _usersID, string _permissionID, FundClient _FundClient)
        {
            try
            {
                string paramFundClientSelected = "";
                paramFundClientSelected = "FundClientPK in (" + _FundClient.FundClientSelected + ") ";

                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @" Declare @IPAddress nvarchar(50) select @IPAddress = IPAddress from Users where ID = @UsersID and Status = 2 
                                 Insert into Activity(Time,PermissionID,ObjectTable,ObjectTablePK,Status,Message,UsersID,IPAddress,LastUpdate) 
                                 Select @Time,@PermissionID,'FundClient',FundClientPK,1,'Approve by Selected Data',@UsersID,@IPAddress,@Time  from FundClient where Status = 2 and " + paramFundClientSelected + @"
                                 Update FundClient set BitIsSuspend= 1,SuspendBy=@UsersID,SuspendTime=@Time,LastUpdate=@Time  WHERE status = 2 and FundClientPK in (Select FundClientPK from FundClient where Status = 2 and " + paramFundClientSelected + @") ";

                        cmd.Parameters.AddWithValue("@PermissionID", _permissionID);
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
        public void FundClient_UnSuspendBySelectedData(string _usersID, string _permissionID, FundClient _FundClient)
        {
            try
            {
                string paramFundClientSelected = "";
                paramFundClientSelected = "FundClientPK in (" + _FundClient.FundClientSelected + ") ";

                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @" Declare @IPAddress nvarchar(50) select @IPAddress = IPAddress from Users where ID = @UsersID and Status = 2 
                                 Insert into Activity(Time,PermissionID,ObjectTable,ObjectTablePK,Status,Message,UsersID,IPAddress,LastUpdate) 
                                 Select @Time,@PermissionID,'FundClient',FundClientPK,1,'Approve by Selected Data',@UsersID,@IPAddress,@Time  from FundClient where Status = 2 and " + paramFundClientSelected + @"
                                 Update FundClient set BitIsSuspend= 0,UnSuspendBy=@UsersID,UnSuspendTime=@Time,LastUpdate=@Time  WHERE status = 2 and FundClientPK in (Select FundClientPK from FundClient where Status = 2 and " + paramFundClientSelected + @") ";

                        cmd.Parameters.AddWithValue("@PermissionID", _permissionID);
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
        public int FundClient_CreateAfiliatedClient(string _userID, int _fundClientPK)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    DateTime _lastUpdate = DateTime.Now;
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = @"
                           Declare @NewFundclientPK int

                                Select @NewFundClientPK = max(FundClientPK) + 1 From FundClient 

                                INSERT INTO [dbo].[FundClient]  
                                ([FundClientPK],[HistoryPK],[Status],[ID],[Name], 
                                [ClientCategory],[InvestorType],[InternalCategoryPK],[SellingAgentPK],[SID],[IFUACode],[KYCRiskProfile],[RiskProfileScore],
                                [ARIA],[Registered],
                                [Negara],[Nationality],[NPWP],[SACode],[Propinsi],[TeleponSelular], 
                                [Email],[Fax],[DormantDate],[Description],[NamaBank1],
                                [NomorRekening1],[NamaNasabah1],[MataUang1],[OtherCurrency],[NamaBank2],
                                [NomorRekening2],[NamaNasabah2],[MataUang2],[NamaBank3],
                                [NomorRekening3],[NamaNasabah3],[MataUang3],[NamaDepanInd],
                                [NamaTengahInd],[NamaBelakangInd],[TempatLahir],[TanggalLahir],[JenisKelamin],
                                [StatusPerkawinan],[Pekerjaan],[OtherOccupation],[Pendidikan],[OtherPendidikan],[Agama],[OtherAgama],[PenghasilanInd],
                                [SumberDanaInd],[OtherSourceOfFunds],[MaksudTujuanInd],[OtherInvestmentObjectives],[AlamatInd1],[KodeKotaInd1],[KodePosInd1],
                                [AlamatInd2],[KodeKotaInd2],[KodePosInd2],[NamaPerusahaan],[Domisili],
                                [Tipe],[OtherTipe],[Karakteristik],[OtherCharacteristic],[NoSKD],[PenghasilanInstitusi],[SumberDanaInstitusi],[OtherSourceOfFundsIns],
                                [MaksudTujuanInstitusi],[OtherInvestmentObjectivesIns],[AlamatPerusahaan],[KodeKotaIns],[KodePosIns],[SpouseName],[MotherMaidenName], 
                                [AhliWaris],[HubunganAhliWaris],[NatureOfBusiness],[NatureOfBusinessLainnya],[Politis],
                                [PolitisLainnya],[TeleponRumah],[OtherAlamatInd1],[OtherKodeKotaInd1],[OtherKodePosInd1],
                                [OtherPropinsiInd1],[CountryOfBirth],[OtherNegaraInd1],[OtherAlamatInd2],[OtherKodeKotaInd2],[OtherKodePosInd2],
                                [OtherPropinsiInd2],[OtherNegaraInd2],[OtherAlamatInd3],[OtherKodeKotaInd3],[OtherKodePosInd3],
                                [OtherPropinsiInd3],[OtherNegaraInd3],[OtherTeleponRumah],[OtherTeleponSelular],[OtherEmail],
                                [OtherFax],[IdentitasInd1],[NoIdentitasInd1],[RegistrationDateIdentitasInd1],
                                [ExpiredDateIdentitasInd1],[IdentitasInd2],[NoIdentitasInd2],[RegistrationDateIdentitasInd2],[ExpiredDateIdentitasInd2],
                                [IdentitasInd3],[NoIdentitasInd3],[RegistrationDateIdentitasInd3],[ExpiredDateIdentitasInd3],[IdentitasInd4],
                                [NoIdentitasInd4],[RegistrationDateIdentitasInd4],[ExpiredDateIdentitasInd4],[RegistrationNPWP],
                                [ExpiredDateSKD],[TanggalBerdiri],[LokasiBerdiri],[TeleponBisnis],[NomorAnggaran],
                                [NomorSIUP],[AssetFor1Year],[AssetFor2Year],[AssetFor3Year],[OperatingProfitFor1Year],
                                [OperatingProfitFor2Year],[OperatingProfitFor3Year],[NamaDepanIns1],[NamaTengahIns1],
                                [NamaBelakangIns1],[Jabatan1],[IdentitasIns11],[NoIdentitasIns11],
                                [RegistrationDateIdentitasIns11],[ExpiredDateIdentitasIns11],[IdentitasIns12],[NoIdentitasIns12],[RegistrationDateIdentitasIns12],
                                [ExpiredDateIdentitasIns12],[IdentitasIns13],[NoIdentitasIns13],[RegistrationDateIdentitasIns13],[ExpiredDateIdentitasIns13],
                                [IdentitasIns14],[NoIdentitasIns14],[RegistrationDateIdentitasIns14],[ExpiredDateIdentitasIns14],[NamaDepanIns2],
                                [NamaTengahIns2],[NamaBelakangIns2],[Jabatan2],[IdentitasIns21],
                                [NoIdentitasIns21],[RegistrationDateIdentitasIns21],[ExpiredDateIdentitasIns21],[IdentitasIns22],[NoIdentitasIns22],
                                [RegistrationDateIdentitasIns22],[ExpiredDateIdentitasIns22],[IdentitasIns23],[NoIdentitasIns23],[RegistrationDateIdentitasIns23],
                                [ExpiredDateIdentitasIns23],[IdentitasIns24],[NoIdentitasIns24],[RegistrationDateIdentitasIns24],[ExpiredDateIdentitasIns24],
                                [NamaDepanIns3],[NamaTengahIns3],[NamaBelakangIns3],[Jabatan3],[JumlahIdentitasIns3],
                                [IdentitasIns31],[NoIdentitasIns31],[RegistrationDateIdentitasIns31],[ExpiredDateIdentitasIns31],[IdentitasIns32],
                                [NoIdentitasIns32],[RegistrationDateIdentitasIns32],[ExpiredDateIdentitasIns32],[IdentitasIns33],[NoIdentitasIns33],
                                [RegistrationDateIdentitasIns33],[ExpiredDateIdentitasIns33],[IdentitasIns34],[NoIdentitasIns34],[RegistrationDateIdentitasIns34],
                                [ExpiredDateIdentitasIns34],[NamaDepanIns4],[NamaTengahIns4],[NamaBelakangIns4],[Jabatan4],
                                [JumlahIdentitasIns4],[IdentitasIns41],[NoIdentitasIns41],[RegistrationDateIdentitasIns41],[ExpiredDateIdentitasIns41],
                                [IdentitasIns42],[NoIdentitasIns42],[RegistrationDateIdentitasIns42],[ExpiredDateIdentitasIns42],[IdentitasIns43],
                                [NoIdentitasIns43],[RegistrationDateIdentitasIns43],[ExpiredDateIdentitasIns43],[IdentitasIns44],[NoIdentitasIns44],
                                [RegistrationDateIdentitasIns44],[ExpiredDateIdentitasIns44],[PhoneIns1],[EmailIns1],  
                                [PhoneIns2],[EmailIns2],[InvestorsRiskProfile],[AssetOwner],[StatementType],[FATCA],[TIN],[TINIssuanceCountry],[GIIN],[SubstantialOwnerName], 
                                [SubstantialOwnerAddress],[SubstantialOwnerTIN],[BankBranchName1],[BankBranchName2],[BankBranchName3],[CountryofCorrespondence],[CountryofDomicile], 
                                [SIUPExpirationDate],[CountryofEstablishment],[CompanyCityName],[CountryofCompany],[NPWPPerson1],[NPWPPerson2],[BankRDNPK],[RDNAccountNo],[RDNAccountName],[RDNBankBranchName],[RDNCurrency],
                                [SpouseBirthPlace],[SpouseDateOfBirth],[SpouseOccupation],[OtherSpouseOccupation],[SpouseNatureOfBusiness],[SpouseNatureOfBusinessOther],[SpouseIDNo],[SpouseNationality],[SpouseAnnualIncome],[DatePengkinianData],[BitDefaultPayment1],[BitDefaultPayment2],[BitDefaultPayment3],[Identity1RT],[Identity1RW],[CorrespondenceRT],[CorrespondenceRW],[DomicileRT],[DomicileRW],[KodeDomisiliPropinsi],
                                [NamaKantor],[JabatanKantor],[AlamatKantorInd],[KodeKotaKantorInd],[KodePropinsiKantorInd],[KodeCountryofKantor],[KodePosKantorInd],[CompanyMail],[Companyfax],
		                        [EntryUsersID],[EntryTime],[LastUpdate],BitIsAfiliated,AfiliatedFromPK,BitIsSuspend)
                                

                                Select  @NewFundClientPK,1,1,'',Name, 
                                ClientCategory,InvestorType,InternalCategoryPK,SellingAgentPK,SID,'',KYCRiskProfile,RiskProfileScore,
                                ARIA,Registered,
                                Negara,Nationality,NPWP,SACode,Propinsi,TeleponSelular, 
                                Email,Fax,DormantDate,Description,NamaBank1,
                                NomorRekening1,NamaNasabah1,MataUang1,OtherCurrency,NamaBank2,
                                NomorRekening2,NamaNasabah2,MataUang2,NamaBank3,
                                NomorRekening3,NamaNasabah3,MataUang3,NamaDepanInd,
                                NamaTengahInd,NamaBelakangInd,TempatLahir,TanggalLahir,JenisKelamin,
                                StatusPerkawinan,Pekerjaan,OtherOccupation,Pendidikan,OtherPendidikan,Agama,OtherAgama,PenghasilanInd,
                                SumberDanaInd,OtherSourceOfFunds,MaksudTujuanInd,OtherInvestmentObjectives,AlamatInd1,KodeKotaInd1,KodePosInd1,
                                AlamatInd2,KodeKotaInd2,KodePosInd2,NamaPerusahaan,Domisili,
                                Tipe,OtherTipe,Karakteristik,OtherCharacteristic,NoSKD,PenghasilanInstitusi,SumberDanaInstitusi,OtherSourceOfFundsIns,
                                MaksudTujuanInstitusi,OtherInvestmentObjectivesIns,AlamatPerusahaan,KodeKotaIns,KodePosIns,SpouseName,MotherMaidenName, 
                                AhliWaris,HubunganAhliWaris,NatureOfBusiness,NatureOfBusinessLainnya,Politis,
                                PolitisLainnya,TeleponRumah,OtherAlamatInd1,OtherKodeKotaInd1,OtherKodePosInd1,
                                OtherPropinsiInd1,CountryOfBirth,OtherNegaraInd1,OtherAlamatInd2,OtherKodeKotaInd2,OtherKodePosInd2,
                                OtherPropinsiInd2,OtherNegaraInd2,OtherAlamatInd3,OtherKodeKotaInd3,OtherKodePosInd3,
                                OtherPropinsiInd3,OtherNegaraInd3,OtherTeleponRumah,OtherTeleponSelular,OtherEmail,
                                OtherFax,IdentitasInd1,NoIdentitasInd1,RegistrationDateIdentitasInd1,
                                ExpiredDateIdentitasInd1,IdentitasInd2,NoIdentitasInd2,RegistrationDateIdentitasInd2,ExpiredDateIdentitasInd2,
                                IdentitasInd3,NoIdentitasInd3,RegistrationDateIdentitasInd3,ExpiredDateIdentitasInd3,IdentitasInd4,
                                NoIdentitasInd4,RegistrationDateIdentitasInd4,ExpiredDateIdentitasInd4,RegistrationNPWP,
                                ExpiredDateSKD,TanggalBerdiri,LokasiBerdiri,TeleponBisnis,NomorAnggaran,
                                NomorSIUP,AssetFor1Year,AssetFor2Year,AssetFor3Year,OperatingProfitFor1Year,
                                OperatingProfitFor2Year,OperatingProfitFor3Year,NamaDepanIns1,NamaTengahIns1,
                                NamaBelakangIns1,Jabatan1,IdentitasIns11,NoIdentitasIns11,
                                RegistrationDateIdentitasIns11,ExpiredDateIdentitasIns11,IdentitasIns12,NoIdentitasIns12,RegistrationDateIdentitasIns12,
                                ExpiredDateIdentitasIns12,IdentitasIns13,NoIdentitasIns13,RegistrationDateIdentitasIns13,ExpiredDateIdentitasIns13,
                                IdentitasIns14,NoIdentitasIns14,RegistrationDateIdentitasIns14,ExpiredDateIdentitasIns14,NamaDepanIns2,
                                NamaTengahIns2,NamaBelakangIns2,Jabatan2,IdentitasIns21,
                                NoIdentitasIns21,RegistrationDateIdentitasIns21,ExpiredDateIdentitasIns21,IdentitasIns22,NoIdentitasIns22,
                                RegistrationDateIdentitasIns22,ExpiredDateIdentitasIns22,IdentitasIns23,NoIdentitasIns23,RegistrationDateIdentitasIns23,
                                ExpiredDateIdentitasIns23,IdentitasIns24,NoIdentitasIns24,RegistrationDateIdentitasIns24,ExpiredDateIdentitasIns24,
                                NamaDepanIns3,NamaTengahIns3,NamaBelakangIns3,Jabatan3,JumlahIdentitasIns3,
                                IdentitasIns31,NoIdentitasIns31,RegistrationDateIdentitasIns31,ExpiredDateIdentitasIns31,IdentitasIns32,
                                NoIdentitasIns32,RegistrationDateIdentitasIns32,ExpiredDateIdentitasIns32,IdentitasIns33,NoIdentitasIns33,
                                RegistrationDateIdentitasIns33,ExpiredDateIdentitasIns33,IdentitasIns34,NoIdentitasIns34,RegistrationDateIdentitasIns34,
                                ExpiredDateIdentitasIns34,NamaDepanIns4,NamaTengahIns4,NamaBelakangIns4,Jabatan4,
                                JumlahIdentitasIns4,IdentitasIns41,NoIdentitasIns41,RegistrationDateIdentitasIns41,ExpiredDateIdentitasIns41,
                                IdentitasIns42,NoIdentitasIns42,RegistrationDateIdentitasIns42,ExpiredDateIdentitasIns42,IdentitasIns43,
                                NoIdentitasIns43,RegistrationDateIdentitasIns43,ExpiredDateIdentitasIns43,IdentitasIns44,NoIdentitasIns44,
                                RegistrationDateIdentitasIns44,ExpiredDateIdentitasIns44,PhoneIns1,EmailIns1,  
                                PhoneIns2,EmailIns2,InvestorsRiskProfile,AssetOwner,StatementType,FATCA,TIN,TINIssuanceCountry,GIIN,SubstantialOwnerName, 
                                SubstantialOwnerAddress,SubstantialOwnerTIN,BankBranchName1,BankBranchName2,BankBranchName3,CountryofCorrespondence,CountryofDomicile, 
                                SIUPExpirationDate,CountryofEstablishment,CompanyCityName,CountryofCompany,NPWPPerson1,NPWPPerson2,BankRDNPK,RDNAccountNo,RDNAccountName,RDNBankBranchName,RDNCurrency,
                                SpouseBirthPlace,SpouseDateOfBirth,SpouseOccupation,OtherSpouseOccupation,SpouseNatureOfBusiness,SpouseNatureOfBusinessOther,SpouseIDNo,SpouseNationality,SpouseAnnualIncome,DatePengkinianData,BitDefaultPayment1,BitDefaultPayment2,BitDefaultPayment3,Identity1RT,Identity1RW,CorrespondenceRT,CorrespondenceRW,DomicileRT,DomicileRW,KodeDomisiliPropinsi,
                                NamaKantor,JabatanKantor,AlamatKantorInd,KodeKotaKantorInd,KodePropinsiKantorInd,KodeCountryofKantor,KodePosKantorInd,CompanyMail,Companyfax,
                                @UserID,@Date,@Date
                                ,1,@FundClientPK,0
                                From FundClient where FundClientPK = @FundClientPK and status = 2

                                Select @NewFundClientPK NewFundClientPK
                        ";
                        cmd.Parameters.AddWithValue("@UserID", _userID);
                        cmd.Parameters.AddWithValue("@Date", _lastUpdate);
                        cmd.Parameters.AddWithValue("@FundClientPK", _fundClientPK);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return Convert.ToInt32(dr["NewFundClientPK"]);

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
        public bool Check_FundClientPending(int _fundClientPK)
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
                        
                        IF EXISTS (Select * from FundClient where FundClientPK = @FundClientPK and status = 1)
                        BEGIN
                        select 1 Result
                        END
                        ELSE 
                        BEGIN
                        select 0 Result
                        END    ";

                        cmd.Parameters.AddWithValue("@FundClientPK", _fundClientPK);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return Convert.ToBoolean(dr["Result"]);

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
        private DataTable CreateDataTableFromFundClientTempExcelFile(string _path)
        {
            try
            {
                using (DataTable dt = new DataTable())
                {
                    DataColumn dc;
                    DataRow dr;

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "A";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "LastUpdate";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "SACode";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "SellingAgentPK";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "SID";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "InvestorType";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "NamaPerusahaan";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Domisili";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "NomorSIUP";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "SIUPExpirationDate";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "NoSKD";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "ExpiredDateSKD";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "NPWP";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "RegistrationNPWP";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "CountryofEstablishment";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "LokasiBerdiri";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "TanggalBerdiri";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "NomorAnggaran";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Tipe";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Karakteristik";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "PenghasilanInstitusi";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "InvestorsRiskProfile";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "MaksudTujuanInstitusi";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "SumberDanaInstitusi";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "AssetOwner";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "AlamatPerusahaan";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "KodeKotaIns";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "AB";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "KodePosIns";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "CountryofCompany";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "TeleponBisnis";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Fax";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Email";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "StatementType";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "NamaDepanIns1";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "NamaTengahIns1";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "NamaBelakangIns1";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Jabatan1";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "PhoneIns1";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "EmailIns1";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "NPWPPerson1";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "NoIdentitasIns11";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "ExpiredDateIdentitasIns11";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "IdentitasIns12";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "ExpiredDateIdentitasIns12";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "NamaDepanIns2";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "NamaTengahIns2";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "NamaBelakangIns2";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Jabatan2";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "PhoneIns2";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "EmailIns2";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "NPWPPerson2";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "NoIdentitasIns21";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "ExpiredDateIdentitasIns21";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "NoIdentitasIns22";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "ExpiredDateIdentitasIns22";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "AssetFor1Year";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "AssetFor2Year";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "AssetFor3Year";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "OperatingProfitFor1Year";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "OperatingProfitFor2Year";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "OperatingProfitFor3Year";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "FATCA";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "TIN";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "TINIssuanceCountry";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "GIIN";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "SubstantialOwnerName";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "SubstantialOwnerAddress";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "SubstantialOwnerTIN";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    using (OleDbConnection odConnection = new OleDbConnection(Tools.ConStringExcel2007(_path)))
                    {
                        odConnection.Open();
                        using (OleDbCommand odCmd = odConnection.CreateCommand())
                        {
                            // _oldfilename = nama sheet yang ada di file excel yang diimport
                            odCmd.CommandText = "SELECT * FROM [Sheet1$]";
                            using (OleDbDataReader odRdr = odCmd.ExecuteReader())
                            {
                                // start counting from index = 1 --> skipping the header (index=0)
                                for (int i = 1; i <= 1; i++)
                                {
                                    odRdr.Read();
                                }
                                do
                                {
                                    dr = dt.NewRow();

                                    dr["A"] = odRdr[0];
                                    dr["LastUpdate"] = odRdr[1];
                                    dr["SACode"] = odRdr[2];
                                    dr["SellingAgentPK"] = odRdr[3];
                                    dr["SID"] = odRdr[4];
                                    dr["InvestorType"] = odRdr[5];
                                    dr["NamaPerusahaan"] = odRdr[6];
                                    dr["Domisili"] = odRdr[7];
                                    dr["NomorSIUP"] = odRdr[8];
                                    dr["SIUPExpirationDate"] = odRdr[9];
                                    dr["NoSKD"] = odRdr[10];
                                    dr["ExpiredDateSKD"] = odRdr[11];
                                    dr["NPWP"] = odRdr[12];
                                    dr["RegistrationNPWP"] = odRdr[13];
                                    dr["CountryofEstablishment"] = odRdr[14];
                                    dr["LokasiBerdiri"] = odRdr[15];
                                    dr["TanggalBerdiri"] = odRdr[16];
                                    dr["NomorAnggaran"] = odRdr[17];
                                    dr["Tipe"] = odRdr[18];
                                    dr["Karakteristik"] = odRdr[19];
                                    dr["PenghasilanInstitusi"] = odRdr[20];
                                    dr["InvestorsRiskProfile"] = odRdr[21];
                                    dr["MaksudTujuanInstitusi"] = odRdr[22];
                                    dr["SumberDanaInstitusi"] = odRdr[23];
                                    dr["AssetOwner"] = odRdr[24];
                                    dr["AlamatPerusahaan"] = odRdr[25];
                                    dr["KodeKotaIns"] = odRdr[26];
                                    dr["AB"] = odRdr[27];
                                    dr["KodePosIns"] = odRdr[28];
                                    dr["CountryofCompany"] = odRdr[29];
                                    dr["TeleponBisnis"] = odRdr[30];
                                    dr["Fax"] = odRdr[31];
                                    dr["Email"] = odRdr[32];
                                    dr["StatementType"] = odRdr[33];
                                    dr["NamaDepanIns1"] = odRdr[34];
                                    dr["NamaTengahIns1"] = odRdr[35];
                                    dr["NamaBelakangIns1"] = odRdr[36];
                                    dr["Jabatan1"] = odRdr[37];
                                    dr["PhoneIns1"] = odRdr[38];
                                    dr["EmailIns1"] = odRdr[39];
                                    dr["NPWPPerson1"] = odRdr[40];
                                    dr["NoIdentitasIns11"] = odRdr[41];
                                    dr["ExpiredDateIdentitasIns11"] = odRdr[42];
                                    dr["IdentitasIns12"] = odRdr[43];
                                    dr["ExpiredDateIdentitasIns12"] = odRdr[44];
                                    dr["NamaDepanIns2"] = odRdr[45];
                                    dr["NamaTengahIns2"] = odRdr[46];
                                    dr["NamaBelakangIns2"] = odRdr[47];
                                    dr["Jabatan2"] = odRdr[48];
                                    dr["PhoneIns2"] = odRdr[49];
                                    dr["EmailIns2"] = odRdr[50];
                                    dr["NPWPPerson2"] = odRdr[51];
                                    dr["NoIdentitasIns21"] = odRdr[52];
                                    dr["ExpiredDateIdentitasIns21"] = odRdr[53];
                                    dr["NoIdentitasIns22"] = odRdr[54];
                                    dr["ExpiredDateIdentitasIns22"] = odRdr[55];
                                    dr["AssetFor1Year"] = odRdr[56];
                                    dr["AssetFor2Year"] = odRdr[57];
                                    dr["AssetFor3Year"] = odRdr[58];
                                    dr["OperatingProfitFor1Year"] = odRdr[59];
                                    dr["OperatingProfitFor2Year"] = odRdr[60];
                                    dr["OperatingProfitFor3Year"] = odRdr[61];
                                    dr["FATCA"] = odRdr[62];
                                    dr["TIN"] = odRdr[63];
                                    dr["TINIssuanceCountry"] = odRdr[64];
                                    dr["GIIN"] = odRdr[65];
                                    dr["SubstantialOwnerName"] = odRdr[66];
                                    dr["SubstantialOwnerAddress"] = odRdr[67];
                                    dr["SubstantialOwnerTIN"] = odRdr[68];

                                    dt.Rows.Add(dr);

                                } while (odRdr.Read());
                            }
                        }
                        odConnection.Close();
                    }


                    return dt;
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }
        public string ImportFundClientInd(string _fileSource, string _userID)
        {
            using (SqlConnection conn = new SqlConnection(Tools.conString))
            {
                conn.Open();
                using (SqlCommand cmd2 = conn.CreateCommand())
                {
                    cmd2.CommandText = "truncate table FundClientTempInd";
                    cmd2.ExecuteNonQuery();
                }
            }

            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(Tools.conString, SqlBulkCopyOptions.TableLock))
            {
                bulkCopy.DestinationTableName = "dbo.FundClientTempInd";
                bulkCopy.WriteToServer(CreateDataTableFromFundClientTempIndExcelFile(_fileSource));
            }

            try
            {
                DateTime _dateTimeNow = DateTime.Now;

                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = @" 
       
Declare @SACode nvarchar(100)
select @SACode = ID From Company where status in (1,2)

declare @max int
select @max = max(fundClientPK) from fundclient
select @max = isnull(@max,0) 


insert into [dbo].[FundClient](
FundClientPK,HistoryPK,Status,ID,Name
,SACode,SellingAgentPK,SID,InvestorType,ClientCategory,IFUACode
,NamaDepanInd,NamaTengahInd,NamaBelakangInd,Nationality,IdentitasInd1
,NoIdentitasInd1,ExpiredDateIdentitasInd1,NPWP,RegistrationNPWP,CountryOfBirth
,TempatLahir,TanggalLahir,JenisKelamin,Pendidikan,MotherMaidenName
,Agama,Pekerjaan,PenghasilanInd,StatusPerkawinan,SpouseName
,InvestorsRiskProfile,MaksudTujuanInd,SumberDanaInd,AssetOwner,OtherAlamatInd1
,OtherKodeKotaInd1,OtherKodePosInd1,AlamatInd1,KodeKotaInd1,KodePosInd1
,CountryofCorrespondence,AlamatInd2,KodeKotaInd2,KodePosInd2,CountryofDomicile
,TeleponRumah,TeleponSelular,fax,Email,StatementType
,FATCA,TIN,TINIssuanceCountry
,EntryTime,EntryUsersID,LastUpdate
)
select  
ROW_NUMBER() OVER(ORDER BY Name ASC) + @max,1,1,ISNULL(ClientCode,''),ISNULL(Name,'')
,Case when SACode = @SACode then '' else SACode end,0,SID,InvestorType,InvestorType,IFUA
,ISNULL(NamaDepanInd,''),ISNULL(NamaTengahInd,''),ISNULL(NamaBelakangInd,''),ISNULL(Nationality,''),IdentitasInd1
,NoIdentitasInd1,convert(datetime,convert(varchar(10),ExpiredDateIdentitasInd1,120)) ,NPWP,RegistrationNPWP,ISNULL(CountryOfBirth,'')
,ISNULL(TempatLahir,''),convert(datetime,convert(varchar(10),TanggalLahir,120)),ISNULL(JenisKelamin,0),ISNULL(Pendidikan,0),ISNULL(MotherMaidenName,'')
,isnull(Agama,0),ISNULL(Occupation,0),ISNULL(IncomeLevel,0),ISNULL(StatusPerkawinan,0),ISNULL(SpouseName,'')
,ISNULL(InvestorsRiskProfile,0),ISNULL(MaksudTujuanInd,0),ISNULL(SumberDanaInd,0),ISNULL(AssetOwner,0),ISNULL(OtherAlamatInd1,'')
,ISNULL(OtherKodeKotaInd1,0),ISNULL(OtherKodePosInd1,''),ISNULL(AlamatInd1,''),ISNULL(KodeKotaInd1,0),ISNULL(KodePosInd1,'')
,ISNULL(CountryofCorrespondence,''),ISNULL(AlamatInd2,''),ISNULL(KodeKotaInd2,0),ISNULL(KodePosInd2,''),ISNULL(CountryofDomicile,'')
,ISNULL(TeleponRumah,''),ISNULL(TeleponSelular,''),ISNULL(fax,''),ISNULL(Email,''),ISNULL(StatementType,0)
,ISNULL(FATCA,0),ISNULL(TIN,''),ISNULL(TINIssuanceCountry,'')
,OpeningDate,@EntryUsersID,@LastUpdate
from FundClientTempInd
where IFUA not in
(
	Select distinct ifuaCode from fundclient
)
                                    
                                                select 'Success' A
                        ";
                        cmd.Parameters.AddWithValue("@EntryUsersID", _userID);
                        cmd.Parameters.AddWithValue("@Lastupdate", DateTime.Now);
                        cmd.ExecuteNonQuery();
                        return "Import Client individual Success";
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }
        private DataTable CreateDataTableFromFundClientTempIndExcelFile(string _path)
        {
            DataTable dt = new DataTable();
            DataColumn dc;
            DataRow dr;

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "SACode";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "SID";
            dc.Unique = false;
            dt.Columns.Add(dc);

       

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "NamaDepanInd";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "NamaTengahInd";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "NamaBelakangInd";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Nationality";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "IdentitasInd1";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "NoIdentitasInd1";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "ExpiredDateIdentitasInd1";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "NPWP";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "RegistrationNPWP";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "CountryOfBirth";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "TempatLahir";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "TanggalLahir";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "JenisKelamin";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Pendidikan";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "MotherMaidenName";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Agama";
            dc.Unique = false;
            dt.Columns.Add(dc);


            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "StatusPerkawinan";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "SpouseName";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "InvestorsRiskProfile";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "MaksudTujuanInd";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "SumberDanaInd";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "AssetOwner";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "OtherAlamatInd1";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "OtherKodeKotaInd1";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "OtherKodePosInd1";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "AlamatInd1";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "KodeKotaInd1";
            dc.Unique = false;
            dt.Columns.Add(dc);

          
            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "KodePosInd1";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "CountryofCorrespondence";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "AlamatInd2";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "KodeKotaInd2";
            dc.Unique = false;
            dt.Columns.Add(dc);


            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "KodePosInd2";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "CountryofDomicile";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "TeleponRumah";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "TeleponSelular";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Fax";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Email";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "StatementType";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "FATCA";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "TIN";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "TINIssuanceCountry";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "SAName";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "IFUA";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Name";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "ClientCode";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "InvestorType";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "SpouseOccupation";
            dc.Unique = false;
            dt.Columns.Add(dc);


            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "IncomeLevel";
            dc.Unique = false;
            dt.Columns.Add(dc);
           
            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "NamaKotaInd1";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "NamaKotaInd2";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "OpeningDate";
            dc.Unique = false;
            dt.Columns.Add(dc);

            StreamReader sr = new StreamReader(Tools.TxtFilePath + _path);
            string input;

            sr.ReadLine();
            while ((input = sr.ReadLine()) != null)
            {
                string[] s = input.Split(new char[] { '|' });
                dr = dt.NewRow();
                dr["SACode"] = s[0];
                dr["SAName"] = s[1];
                dr["SID"] = s[2];
                dr["IFUA"] = s[3];
                dr["Name"] = s[4];
                dr["ClientCode"] = s[5];
                dr["InvestorType"] = s[6];
                dr["NamaDepanInd"] = s[7];
                dr["NamaTengahInd"] = s[8];
                dr["NamaBelakangInd"] = s[9];
                dr["Nationality"] = s[10];
                dr["IdentitasInd1"] = s[11];
                dr["NoIdentitasInd1"] = s[12];
                dr["ExpiredDateIdentitasInd1"] = s[13];
                dr["NPWP"] = s[14];
                dr["RegistrationNPWP"] = s[15];
                dr["CountryOfBirth"] = s[16];
                dr["TempatLahir"] = s[17];
                dr["TanggalLahir"] = s[18];
                dr["JenisKelamin"] = s[19];
                dr["Pendidikan"] = s[20];
                dr["MotherMaidenName"] = s[21];
                dr["Agama"] = s[22];
                dr["SpouseOccupation"] = s[23];
                dr["IncomeLevel"] = s[24];
                dr["StatusPerkawinan"] = s[25];
                dr["SpouseName"] = s[26];
                dr["InvestorsRiskProfile"] = s[27];
                dr["MaksudTujuanInd"] = s[28];
                dr["SumberDanaInd"] = s[29];
                dr["AssetOwner"] = s[30];
                dr["OtherAlamatInd1"] = s[31];
                dr["OtherKodeKotaInd1"] = s[32];
                dr["OtherKodePosInd1"] = s[33];
                dr["AlamatInd1"] = s[34];
                dr["KodeKotaInd1"] = s[35];
                dr["NamaKotaInd1"] = s[36];
                dr["KodePosInd1"] = s[37];
                dr["CountryofCorrespondence"] = s[38];
                dr["AlamatInd2"] = s[39];
                dr["KodeKotaInd2"] = s[40];
                dr["NamaKotaInd2"] = s[41];
                dr["KodePosInd2"] = s[42];
                dr["CountryofDomicile"] = s[43];
                dr["TeleponRumah"] = s[44];
                dr["TeleponSelular"] = s[45];
                dr["Fax"] = s[46];
                dr["Email"] = s[47];
                dr["StatementType"] = s[48];
                dr["FATCA"] = s[49];
                dr["TIN"] = s[50];
                dr["TINIssuanceCountry"] = s[51];
         
                dr["OpeningDate"] = s[53];
                dt.Rows.Add(dr);
            }
            sr.Close();
            return dt;
        }
        private DataTable CreateDataTableFromBankExcelFile(string _path)
        {
            try
            {
                using (DataTable dt = new DataTable())
                {
                    DataColumn dc;
                    DataRow dr;

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Int32");
                    dc.ColumnName = "UpdateBankFundClientPK";
                    dc.Unique = false;
                    dc.AutoIncrement = true;
                    dt.Columns.Add(dc);


                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "SACode";
                    dc.Unique = false;
                    dt.Columns.Add(dc);


                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "NamaBankPK";
                    dc.Unique = false;
                    dt.Columns.Add(dc);


                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "SID";
                    dc.Unique = false;
                    dt.Columns.Add(dc);


                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "NomorNasabah";
                    dc.Unique = false;
                    dt.Columns.Add(dc);


                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "NamaNasabah";
                    dc.Unique = false;
                    dt.Columns.Add(dc);


                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "ClientCode";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Status";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "OpeningDate";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "BICode";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "PaymentBank";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "CurrencyPK";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "PaymentNo";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "PaymentName";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "SequentialCode";
                    dc.Unique = false;
                    dt.Columns.Add(dc);


                    using (OleDbConnection odConnection = new OleDbConnection(Tools.ConStringExcel2007(_path)))
                    {
                        odConnection.Open();
                        using (OleDbCommand odCmd = odConnection.CreateCommand())
                        {
                            // _oldfilename = nama sheet yang ada di file excel yang diimport
                            odCmd.CommandText = "SELECT * FROM [Sheet1$]";
                            using (OleDbDataReader odRdr = odCmd.ExecuteReader())
                            {
                                // start counting from index = 1 --> skipping the header (index=0)
                                for (int i = 1; i <= 1; i++)
                                {
                                    odRdr.Read();
                                }
                                do
                                {
                                    dr = dt.NewRow();

                                    dr["UpdateBankFundClientPK"] = odRdr[0];
                                    dr["SACode"] = odRdr[3];
                                    dr["NamaBankPK"] = odRdr[4];
                                    dr["SID"] = odRdr[5];
                                    dr["NomorNasabah"] = odRdr[6];
                                    dr["NamaNasabah"] = odRdr[7];
                                    dr["ClientCode"] = odRdr[8];
                                    dr["Status"] = odRdr[9];
                                    dr["OpeningDate"] = odRdr[10];
                                    dr["BICode"] = odRdr[13];
                                    dr["PaymentBank"] = odRdr[14];
                                    dr["CurrencyPK"] = odRdr[17];
                                    dr["PaymentNo"] = odRdr[18];
                                    dr["PaymentName"] = odRdr[19];
                                    dr["SequentialCode"] = odRdr[20];

                                    //13

                                    //dr["Amount"] = (odRdr[3].ToString().Replace(",", ""));

                                    if (dr["UpdateBankFundClientPK"].Equals(DBNull.Value) != true) { dt.Rows.Add(dr); }
                                } while (odRdr.Read());
                            }
                        }
                        odConnection.Close();
                    }

                    return dt;
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }
        public string BanksImport(string _fileSource, string _userID, string _date)
        {
            string _msg;
            DateTime _dateTime = DateTime.Now;
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        //delete data yang lama
                        using (SqlConnection conn = new SqlConnection(Tools.conString))
                        {
                            conn.Open();
                            using (SqlCommand cmd1 = conn.CreateCommand())
                            {
                                cmd1.CommandText = "truncate table UpdateaBankFundClient";
                                cmd1.ExecuteNonQuery();
                            }
                        }

                        // import data ke temp dulu
                        using (SqlBulkCopy bulkCopy = new SqlBulkCopy(Tools.conString, SqlBulkCopyOptions.TableLock))
                        {
                            bulkCopy.DestinationTableName = "dbo.UpdateaBankFundClient";
                            bulkCopy.WriteToServer(CreateDataTableFromBankExcelFile(_fileSource));
                            _msg = "Import Banks Success";
                        }

                        // logic kalo Reconcile success
                        using (SqlConnection conn = new SqlConnection(Tools.conString))
                        {
                            conn.Open();
                            using (SqlCommand cmd1 = conn.CreateCommand())
                            {
                                cmd1.CommandText =
                                @"Declare @BankPK int
                                             Select @BankPK = BankPK from UpdateaBankFundClient a left join bank b on a.BICode = b.BICode
                                             UPDATE  a set   a.MataUang1 = b.CurrencyPK, a.NamaNasabah1 = b.NamaNasabah,
                                             a.BankBranchName1 = b.NamaBankPK, a.NamaBank1 = @BankPK,
                                             a.NomorRekening1 = b.SID from fundclient A left JOIN UpdateaBankFundClient b ON a.ID = b.ClientCode where a.ID = b.ClientCode ";
                                cmd1.ExecuteNonQuery();

                            }
                            _msg = "Import Bank Done";

                        }



                    }
                }
                return _msg;
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        private DataTable CreateDataTableFromSIDTempExcelFile(string _fileName)
        {
            DataTable dt = new DataTable();
            DataColumn dc;
            DataRow dr;

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.Int32");
            dc.ColumnName = "UpdateSIDIFUACodeTempPK";
            dc.Unique = false;
            dc.AutoIncrement = true;
            dc.AutoIncrementSeed = 1;
            dc.AutoIncrementStep = 1;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "SID";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "IFUACode";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "ClientCode";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "OpeningDateSinvest";
            dc.Unique = false;
            dt.Columns.Add(dc);

            StreamReader sr = new StreamReader(Tools.TxtFilePath + _fileName);
            string input;

            sr.ReadLine();
            while ((input = sr.ReadLine()) != null)
            {
                string[] s = input.Split(new char[] { '|' });
                dr = dt.NewRow();
                dr["SID"] = s[2];
                dr["IFUACode"] = s[3];
                dr["ClientCode"] = s[5];
                dr["OpeningDateSinvest"] = s[53];
                dt.Rows.Add(dr);
            }
            sr.Close();
            return dt;
        }

        public string SIDImport(string _fileSource, string _userID)
        {
            string _msg;
            DateTime _dateTime = DateTime.Now;
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        //delete data yang lama
                        using (SqlConnection conn = new SqlConnection(Tools.conString))
                        {
                            conn.Open();
                            using (SqlCommand cmd1 = conn.CreateCommand())
                            {
                                cmd1.CommandText = "truncate table UpdateSIDIFUACodeTemp";
                                cmd1.ExecuteNonQuery();
                            }
                        }

                        // import data ke temp dulu
                        using (SqlBulkCopy bulkCopy = new SqlBulkCopy(Tools.conString, SqlBulkCopyOptions.TableLock))
                        {
                            bulkCopy.DestinationTableName = "dbo.UpdateSIDIFUACodeTemp";
                            bulkCopy.WriteToServer(CreateDataTableFromSIDTempExcelFile(_fileSource));
                            _msg = "Update SID/IFUA Success";
                        }

                        // logic kalo Reconcile success
                        using (SqlConnection conn = new SqlConnection(Tools.conString))
                        {
                            conn.Open();
                            using (SqlCommand cmd1 = conn.CreateCommand())
                            {
                                cmd1.CommandText =
                                @"  
                                   
                               update B set B.IFUACode = A.IFUACode, B.SID = A.SID,B.frontSync = 0
                                ,B.LastUpdate = @Lastupdate,B.UpdateTime = @Lastupdate,B.UpdateUsersID = @EntryUsersID 
                                from UpdateSIDIFUACodeTemp A 
                                left join FundClient B on A.ClientCode = B.ID
                                where isnull(A.IFUACode,'') not in
                                (
	                                Select distinct isnull(IFUACode,'') from FundClient where status in (1,2)
                                ) 
                                and B.status in (1,2)

                                ";
                                cmd1.CommandTimeout = 0;
                                cmd1.Parameters.AddWithValue("@EntryUsersID", _userID);
                                cmd1.Parameters.AddWithValue("@LastUpdate", _dateTime);
                                cmd1.ExecuteNonQuery();
                            }
                            _msg = "Import SID IFUA Done";

                        }



                    }
                }
                return _msg;
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public GetSummary GetSummary_ByFundClientPK(int _fundClientPK)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = @"
                        Select A.FundPK,B.ID FundID,A.Unit UnitAmount,0 NAV, 0 TotalAmount from FundClientPositionSummary A
                        left join Fund B on A.FundPK = B.FundPK and B.status in (1,2)
                        left join FundClient C on A.FundClientPK = C.FundClientPK and C.status in (1,2)
                        where A.FundClientPK = @FundClientPK
		                                       ";
                        cmd.Parameters.AddWithValue("@FundClientPK", _fundClientPK);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                //GetSummary M_FundClient = new GetSummary();
                                //M_FundClient.FundClientPK = Convert.ToInt32(dr["FundClientPK"]);
                                //M_FundClient.FundID = Convert.ToString(dr["FundID"]);
                                //M_FundClient.UnitAmount = Convert.ToDecimal(dr["UnitAmount"]);
                                //M_FundClient.Nav = Convert.ToDecimal(dr["Nav"]);
                                //M_FundClient.TotalAmount = Convert.ToDecimal(dr["TotalAmount"]);

                                return new GetSummary()
                                {
                                    FundPK = Convert.ToInt32(dr["FundPK"]),
                                    FundID = Convert.ToString(dr["FundID"]),
                                    UnitAmount = Convert.ToDecimal(dr["UnitAmount"]),
                                    Nav = Convert.ToDecimal(dr["Nav"]),
                                    TotalAmount = Convert.ToDecimal(dr["TotalAmount"])
                                };
                            }
                            else
                            {
                                return new GetSummary()
                                {
                                    FundPK = 0,
                                    FundID = string.Empty,
                                    UnitAmount = 0,
                                    Nav = 0,
                                    TotalAmount = 0
                                };
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
        public string ImportFundClientIns(string _fileSource, string _userID)
        {
            using (SqlConnection conn = new SqlConnection(Tools.conString))
            {
                conn.Open();
                using (SqlCommand cmd2 = conn.CreateCommand())
                {
                    cmd2.CommandText = "truncate table FundClientTempIns";
                    cmd2.ExecuteNonQuery();
                }
            }

            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(Tools.conString, SqlBulkCopyOptions.TableLock))
            {
                bulkCopy.DestinationTableName = "dbo.FundClientTempIns";
                bulkCopy.WriteToServer(CreateDataTableFromFundClientTempInsExcelFile(_fileSource));
            }

            try
            {
                DateTime _dateTimeNow = DateTime.Now;

                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = @" 
        
Declare @SACode nvarchar(100)
select @SACode = ID From Company where status in (1,2)

declare @max int
select @max = max(fundClientPK) from fundclient
select @max = isnull(@max,0) 


insert into [dbo].[FundClient](
FundClientPK,HistoryPK,Status,ID,Name
,SACode,SellingAgentPK,SID,InvestorType,ClientCategory,IFUACode
,NamaDepanInd,NamaTengahInd,NamaBelakangInd,Nationality,IdentitasInd1
,NoIdentitasInd1,ExpiredDateIdentitasInd1,NPWP,RegistrationNPWP,CountryOfBirth
,TempatLahir,TanggalLahir,JenisKelamin,Pendidikan,MotherMaidenName
,Agama,Pekerjaan,PenghasilanInd,StatusPerkawinan,SpouseName
,InvestorsRiskProfile,MaksudTujuanInd,SumberDanaInd,AssetOwner,OtherAlamatInd1
,OtherKodeKotaInd1,OtherKodePosInd1,AlamatInd1,KodeKotaInd1,KodePosInd1
,CountryofCorrespondence,AlamatInd2,KodeKotaInd2,KodePosInd2,CountryofDomicile
,TeleponRumah,TeleponSelular,fax,Email,StatementType
,FATCA,TIN,TINIssuanceCountry
,EntryTime,EntryUsersID,LastUpdate
)
select  
ROW_NUMBER() OVER(ORDER BY Name ASC) + @max,1,1,ISNULL(ClientCode,''),ISNULL(Name,'')
,Case when SACode = @SACode then '' else SACode end,0,SID,InvestorType,InvestorType,IFUA
,ISNULL(NamaDepanInd,''),ISNULL(NamaTengahInd,''),ISNULL(NamaBelakangInd,''),ISNULL(Nationality,''),IdentitasInd1
,NoIdentitasInd1,convert(datetime,convert(varchar(10),ExpiredDateIdentitasInd1,120)) ,NPWP,RegistrationNPWP,ISNULL(CountryOfBirth,'')
,ISNULL(TempatLahir,''),convert(datetime,convert(varchar(10),TanggalLahir,120)),ISNULL(JenisKelamin,0),ISNULL(Pendidikan,0),ISNULL(MotherMaidenName,'')
,isnull(Agama,0),ISNULL(Occupation,0),ISNULL(IncomeLevel,0),ISNULL(StatusPerkawinan,0),ISNULL(SpouseName,'')
,ISNULL(InvestorsRiskProfile,0),ISNULL(MaksudTujuanInd,0),ISNULL(SumberDanaInd,0),ISNULL(AssetOwner,0),ISNULL(OtherAlamatInd1,'')
,ISNULL(OtherKodeKotaInd1,0),ISNULL(OtherKodePosInd1,''),ISNULL(AlamatInd1,''),ISNULL(KodeKotaInd1,0),ISNULL(KodePosInd1,'')
,ISNULL(CountryofCorrespondence,''),ISNULL(AlamatInd2,''),ISNULL(KodeKotaInd2,0),ISNULL(KodePosInd2,''),ISNULL(CountryofDomicile,'')
,ISNULL(TeleponRumah,''),ISNULL(TeleponSelular,''),ISNULL(fax,''),ISNULL(Email,''),ISNULL(StatementType,0)
,ISNULL(FATCA,0),ISNULL(TIN,''),ISNULL(TINIssuanceCountry,'')
,OpeningDate,@EntryUsersID,@LastUpdate
from FundClientTempInd
where IFUA not in
(
	Select distinct ifuaCode from fundclient
)
                                              

                            select 'Success' A
                        ";
                        cmd.Parameters.AddWithValue("@EntryUsersID", _userID);
                        cmd.Parameters.AddWithValue("@Lastupdate", DateTime.Now);
                        cmd.ExecuteNonQuery();
                        return "Import Instrument Success";
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }
        private DataTable CreateDataTableFromFundClientTempInsExcelFile(string _path)
        {
            DataTable dt = new DataTable();
            DataColumn dc;
            DataRow dr;

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "SACode";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "SID";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "InvestorType";
            dc.Unique = false;
            dt.Columns.Add(dc);


            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "NamaPerusahaan";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Domisili";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "NomorSIUP";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "SIUPExpirationDate";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "NoSKD";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "ExpiredDateSKD";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "NPWP";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "RegistrationNPWP";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "CountryofEstablishment";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "LokasiBerdiri";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "TanggalBerdiri";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "NomorAnggaran";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Tipe";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Karakteristik";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "PenghasilanInstitusi";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "InvestorsRiskProfile";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "MaksudTujuanInstitusi";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "SumberDanaInstitusi";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "AssetOwner";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "AlamatPerusahaan";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "KodeKotaIns";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "KodePosIns";
            dc.Unique = false;
            dt.Columns.Add(dc);

       
           

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "CountryofCompany";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "TeleponBisnis";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Fax";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Email";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "StatementType";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "NamaDepanIns1";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "NamaTengahIns1";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "NamaBelakangIns1";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Jabatan1";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "PhoneIns1";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "EmailIns1";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "NPWPPerson1";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "NoIdentitasIns11";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "ExpiredDateIdentitasIns11";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "NoIdentitasIns12";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "ExpiredDateIdentitasIns12";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "NamaDepanIns2";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "NamaTengahIns2";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "NamaBelakangIns2";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Jabatan2";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "PhoneIns2";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "EmailIns2";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "NPWPPerson2";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "NoIdentitasIns21";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "ExpiredDateIdentitasIns21";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "NoIdentitasIns22";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "ExpiredDateIdentitasIns22";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "AssetFor1Year";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "AssetFor2Year";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "AssetFor3Year";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "OperatingProfitFor1Year";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "OperatingProfitFor2Year";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "OperatingProfitFor3Year";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "FATCA";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "TIN";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "TINIssuanceCountry";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "GIIN";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "SubstantialOwnerName";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "SubstantialOwnerAddress";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "SubstantialOwnerTIN";
            dc.Unique = false;
            dt.Columns.Add(dc);

         
            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "SAName";
            dc.Unique = false;
            dt.Columns.Add(dc);


            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "IFUA";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Name";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "ClientID";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "NamaKotaIns";
            dc.Unique = false;
            dt.Columns.Add(dc);


            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "OpeningDate";
            dc.Unique = false;
            dt.Columns.Add(dc);

            StreamReader sr = new StreamReader(Tools.TxtFilePath + _path);
            string input;

            sr.ReadLine();
            while ((input = sr.ReadLine()) != null)
            {
                string[] s = input.Split(new char[] { '|' });
                dr = dt.NewRow();
                dr["SACode"] = s[0];
                dr["SAName"] = s[1];
                dr["SID"] = s[2];
                dr["IFUA"] = s[3];
                dr["Name"] = s[4];
                dr["ClientID"] = s[5];
                dr["InvestorType"] = s[6];
                dr["NamaPerusahaan"] = s[7];
                dr["Domisili"] = s[8];
                dr["NomorSIUP"] = s[9];
                dr["SIUPExpirationDate"] = s[10];
                dr["NoSKD"] = s[11];
                dr["ExpiredDateSKD"] = s[12];
                dr["NPWP"] = s[13];
                dr["RegistrationNPWP"] = s[14];
                dr["CountryofEstablishment"] = s[15];
                dr["LokasiBerdiri"] = s[16];
                dr["TanggalBerdiri"] = s[17];
                dr["NomorAnggaran"] = s[18];
                dr["Tipe"] = s[19];
                dr["Karakteristik"] = s[20];
                dr["PenghasilanInstitusi"] = s[21];
                dr["InvestorsRiskProfile"] = s[22];
                dr["MaksudTujuanInstitusi"] = s[23];
                dr["SumberDanaInstitusi"] = s[24];
                dr["AssetOwner"] = s[25];
                dr["AlamatPerusahaan"] = s[26];
                dr["KodeKotaIns"] = s[27];
                dr["NamaKotaIns"] = s[28];
                dr["KodePosIns"] = s[29];
                dr["CountryofCompany"] = s[30];
                dr["TeleponBisnis"] = s[31];
                dr["Fax"] = s[32];
                dr["Email"] = s[33];
                dr["StatementType"] = s[34];
                dr["NamaDepanIns1"] = s[35];
                dr["NamaTengahIns1"] = s[36];
                dr["NamaBelakangIns1"] = s[37];
                dr["Jabatan1"] = s[38];
                dr["PhoneIns1"] = s[39];
                dr["EmailIns1"] = s[40];
                dr["NPWPPerson1"] = s[41];
                dr["NoIdentitasIns11"] = s[42];
                dr["ExpiredDateIdentitasIns11"] = s[43];
                dr["NoIdentitasIns12"] = s[44];
                dr["ExpiredDateIdentitasIns12"] = s[45];
                dr["NamaDepanIns2"] = s[46];
                dr["NamaTengahIns2"] = s[47];
                dr["NamaBelakangIns2"] = s[48];
                dr["Jabatan2"] = s[49];
                dr["PhoneIns2"] = s[50];
                dr["EmailIns2"] = s[51];
                dr["NPWPPerson2"] = s[52];
                dr["NoIdentitasIns21"] = s[53];
                dr["ExpiredDateIdentitasIns21"] = s[54];
                dr["NoIdentitasIns22"] = s[55];
                dr["ExpiredDateIdentitasIns22"] = s[56];
                dr["AssetFor1Year"] = s[57];
                dr["AssetFor2Year"] = s[58];
                dr["AssetFor3Year"] = s[59];
                dr["OperatingProfitFor1Year"] = s[60];
                dr["OperatingProfitFor2Year"] = s[61];
                dr["OperatingProfitFor3Year"] = s[62];
                dr["FATCA"] = s[63];
                dr["TIN"] = s[64];
                dr["TINIssuanceCountry"] = s[65];
                dr["GIIN"] = s[66];
                dr["SubstantialOwnerName"] = s[67];
                dr["SubstantialOwnerAddress"] = s[68];
                dr["SubstantialOwnerTIN"] = s[69];
                dr["OpeningDate"] = s[71];
                dt.Rows.Add(dr);
            }
            sr.Close();
            return dt;
        }

        //GetIdentity
        public GetCustomerHistory CustomerServiceBook_GetCustomerCombo(int _status, int _fundclientPK)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<GetCustomerHistory> L_CustomerServiceBook = new List<GetCustomerHistory>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"
                             Select case when A.ClientCategory=1 then isnull(A.TeleponSelular,0) else isnull(A.TeleponBisnis,0) end Phone, 
                             case when A.ClientCategory=1 then isnull(A.Email,'')
                             else isnull(A.CompanyMail,'') end Email from FundClient A
                             left join CustomerServiceBook CS on A.FundClientPK = CS.FundClientPK and CS.status = 2                  
                             where A.status = @status and A.FundclientPK = @FundclientPK
                               ";
                        cmd.Parameters.AddWithValue("@status", _status);
                        cmd.Parameters.AddWithValue("@FundclientPK", _fundclientPK);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return new GetCustomerHistory()
                                {

                                    Email = Convert.ToString(dr["Email"]),
                                    Phone = Convert.ToString(dr["Phone"]),
                                };
                            }
                            else
                            {
                                return new GetCustomerHistory()
                                {

                                    Email = "",
                                    Phone = "",
                                };
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

        public FundClient DatePengkinianData(int _historyPK, int _fundClientPK, string _usersID)
        {
            {

                try
                {
                    DateTime _datetimeNow = DateTime.Now;
                    string _datetimeNows = DateTime.Now.ToString("MM/dd/yyyy");
                    string DatePengkinianData = "";
                    using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                    {
                        DbCon.Open();
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update FundClient set DatePengkinianData = @DatePengkinianData," +
                            "UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@lastupdate " +
                            "where FundClientPK = @FundClientPK and historyPK = @HistoryPK";

                            cmd.Parameters.AddWithValue("@DatePengkinianData", _datetimeNows);
                            cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                            cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _usersID);
                            cmd.Parameters.AddWithValue("@HistoryPK", _historyPK);
                            cmd.Parameters.AddWithValue("@FundClientPK", _fundClientPK);
                            cmd.ExecuteNonQuery();

                        }
                        DatePengkinianData = Convert.ToString(_datetimeNow);

                    }
                    return new FundClient()
                    {
                        DatePengkinianData = DatePengkinianData
                    };

                }
                catch (Exception err)
                {
                    throw err;
                }
            }


        }
        public decimal FundClient_GetUnitPositionSwitching(int _fundPK, DateTime _date, int _fundClientPK)
        {


            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();

                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = @" select Unit from FundClientPositionSummary where FundPK = @FundPK and FundClientPK = @FundClientPK ";

                        cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                        cmd.Parameters.AddWithValue("@Date", _date);
                        cmd.Parameters.AddWithValue("@FundClientPK", _fundClientPK);



                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return Convert.ToDecimal(dr["Unit"]);

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


        public FundClient Get_ClientCategory(int _fundclientPK)
        {
            int ClientCategory = 0;
            int SumberDana = 0;
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"
                            Select case when clientcategory  = 1 then SumberDanaIND else sumberDanaInstitusi end SumberDana, ClientCategory from fundclient                   
                            where status = 2 and FundclientPK = @FundclientPK
                               ";
                        cmd.Parameters.AddWithValue("@FundclientPK", _fundclientPK);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                ClientCategory = Convert.ToInt32(dr["ClientCategory"]);
                                SumberDana = Convert.ToInt32(dr["SumberDana"]);
                            }

                        }
                    } return new FundClient()
                    {
                        ClientCategory = Convert.ToString(ClientCategory),
                        SumberDana = Convert.ToString(SumberDana)
                    };
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public List<FundClientComboVA> Get_BankVA(int _FundClientPK)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<FundClientComboVA> L_FundClient = new List<FundClientComboVA>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "select Case when Status = 2 then 'APPROVED' else 'HISTORY' end StatusDesc,BankName,AccountNo,AccountName,Currency from ZRDO_80_BANK where FundClientPk = @FundClientPK";
                        cmd.Parameters.AddWithValue("@FundclientPK", _FundClientPK);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    FundClientComboVA M_FundClient = new FundClientComboVA();
                                    M_FundClient.AccountNo = Convert.ToString(dr["AccountNo"]);
                                    M_FundClient.BankName = Convert.ToString(dr["BankName"]);
                                    M_FundClient.Currency = Convert.ToString(dr["Currency"]);
                                    M_FundClient.AccountName = Convert.ToString(dr["AccountName"]);
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

        public List<FundClientComboVA> Get_BankVAByClientRedemption(int _FundClientPK)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<FundClientComboVA> L_FundClient = new List<FundClientComboVA>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"select BankID,C.Name Bankname,AccountNo,AccountName,Currency,FundClientPK from ZRDO_80_BANK A
                                            left join ZRDO_80_BANK_MAPPING B on A.BankName = B.PartnerCode
                                            left join Bank C on B.RadsoftCode = C.ID and C.Status in (1, 2)
                                            where FundClientPk = @FundClientPk and A.Status = 2
                                            ";
                        cmd.Parameters.AddWithValue("@FundclientPK", _FundClientPK);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    FundClientComboVA M_FundClient = new FundClientComboVA();
                                    M_FundClient.BankRecipientPK = Convert.ToInt32(dr["BankID"]);
                                    M_FundClient.FundClientPK = Convert.ToInt32(dr["FundClientPK"]);
                                    M_FundClient.AccountNo = Convert.ToString(dr["AccountNo"]);
                                    M_FundClient.BankName = Convert.ToString(dr["BankName"]);
                                    M_FundClient.Currency = Convert.ToString(dr["Currency"]);
                                    M_FundClient.AccountName = Convert.ToString(dr["AccountName"]);
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

        public FundClient Get_SumberDana(int _fundclientPK)
        {
            int SumberDana = 0;
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"
								Select case when clientcategory  = 1 then SumberDanaIND else sumberDanaInstitusi end SumberDana from fundclient                   
                            where status = 2 and FundclientPK = @FundclientPK
                               ";
                        cmd.Parameters.AddWithValue("@FundclientPK", _fundclientPK);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                SumberDana = Convert.ToInt32(dr["SumberDana"]);
                            }

                        }
                    } 
                    return new FundClient()
                    {
                        SumberDana = Convert.ToString(SumberDana)
                    };
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }


        public bool Check_FundClientCantSubs(int _fundClientPK)
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
                        
                        IF EXISTS (Select * from FundClient where FundClientPK = @FundClientPK and status = 2 and CantSubs = 1)
                        BEGIN
                        select 1 Result
                        END
                        ELSE 
                        BEGIN
                        select 0 Result
                        END   ";

                        cmd.Parameters.AddWithValue("@FundClientPK", _fundClientPK);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return Convert.ToBoolean(dr["Result"]);

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

        public string Check_SInvestFundClientByHighRiskMonitoring(int _category, FundClient _FundClient)
        {
            try
            {
                string _msg = "";

                string paramFundClientSelected = "";
                paramFundClientSelected = "FundClientPK in (" + _FundClient.FundClientSelected + ") ";

                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = @"
                        
                         declare @msg nvarchar(max)
 
                         set @msg = ''

                         if exists (
                         select FundClientPK from FundClient where 
                         Status = 2 and InvestorType = @Category  and " + paramFundClientSelected + @"  and FundClientPK in ( select FundClientPK from HighRiskMonitoring where status = 1 and HighRiskType = 1)
                         )
                         begin
	                        set @msg = 'Warning : SysNo '
	                         select @msg = @msg + cast(FundClientPK as nvarchar) + ',' from FundClient where 
	                        Status = 2 and InvestorType = @Category  and " + paramFundClientSelected + @"  and FundClientPK in ( select FundClientPK from HighRiskMonitoring where status = 1 and HighRiskType = 1)

	                        set @msg = LEFT(@msg, LEN(@msg) - 1) + ' is on High Risk Monitoring'

                         end
                         else
                         begin
	                        set @msg = 'Export S-Invest Success'
                         end

                         select @msg Result
                        ";

                        cmd.Parameters.AddWithValue("@Category", _category);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                _msg = dr["Result"].ToString();

                            }
                            return _msg;
                        }
                    }
                }
                
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public bool Check_FundClientCantRedempt(int _fundClientPK)
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
                        
                        IF EXISTS (Select * from FundClient where FundClientPK = @FundClientPK and status = 2 and CantRedempt = 1)
                        BEGIN
                        select 1 Result
                        END
                        ELSE 
                        BEGIN
                        select 0 Result
                        END   ";

                        cmd.Parameters.AddWithValue("@FundClientPK", _fundClientPK);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return Convert.ToBoolean(dr["Result"]);

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

        public bool Check_FundClientCantSwitch(int _fundClientPK)
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
                        
                        IF EXISTS (Select * from FundClient where FundClientPK = @FundClientPK and status = 2 and CantSwitch = 1)
                        BEGIN
                        select 1 Result
                        END
                        ELSE 
                        BEGIN
                        select 0 Result
                        END   ";

                        cmd.Parameters.AddWithValue("@FundClientPK", _fundClientPK);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return Convert.ToBoolean(dr["Result"]);

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

        public string FundClient_GenerateHutangValas(DateTime _date)
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
             BEGIN  
                              SET NOCOUNT ON         
          
                           create table #Text(      
                          [ResultText] [nvarchar](1000)  NULL          
                          )                        
        
                          truncate table #Text      
        
                              insert into #Text      
                              select isnull(RTRIM(LTRIM(1)),'') + '|' + isnull(RTRIM(LTRIM(0)),'') + '|' + isnull(RTRIM(LTRIM(0)),'') +  '|' + 
							  isnull(RTRIM(LTRIM(0)),'') + '|' + isnull(RTRIM(LTRIM(0)),'') + '|' + isnull(RTRIM(LTRIM(0)),'') + '|' + isnull(RTRIM(LTRIM(0)),'') + '|' + 
							  isnull(RTRIM(LTRIM(0)),'')                
                              from  FundClient FC                             
                              where FC.ClientCategory = '1'                         
                              and FC.FundClientPK in (select fundClientPK from FundClientPosition where Date =  @ParamDate and UnitAmount >  0.0001   ) and FC.status in (1,2)         
								and FundClientPK between 1 and 6	
                              order by FC.name asc  
                                         
                              select * from #text                               
                                 
                              END";
                        cmd.Parameters.AddWithValue("@ParamDate", _date);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {

                                string filePath = Tools.ARIATextPath + "HutangValas.txt";
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
                                    return Tools.HtmlARIATextPath + "HutangValas.txt";
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

        public Boolean GenerateReportHutangValas(string _userID, SInvestRpt _sInvestRpt)
        {
            #region HutangValas
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        //string _paramFund = "";
                        //_paramFund = "left(@FundFrom,charindex('-',@FundFrom) - 1) ";


                        cmd.CommandText =
                        @"
                             select 1200 HutangBankUSD, 3500 HutangBankEUR, 4500 HutangBankJPY,
                             3000 HutangBankGBP, 5500 HutangBankAUD, 7500 HutangBankSGD, 2500 HutangBankLainnya,
                             1200 HutangObligasiUSD, 3500 HutangObligasiEUR, 4500 HutangObligasiJPY,
                             3000 HutangObligasiGBP, 5500 HutangObligasiAUD, 7500 HutangObligasiSGD, 2500 HutangObligasiLainnya,
                             1200 HutanglainlainUSD, 3500 HutanglainlainEUR, 4500 HutanglainlainJPY,
                             3000 HutanglainlainGBP, 5500 HutanglainlainAUD, 7500 HutanglainlainSGD, 2500 HutanglainlainLainnya
                             from fundclient where fundclientpk between 1 and 2 and status in(1,2)
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
                                string filePath = Tools.ReportsPath + "HutangValas" + "_" + _userID + ".xlsx";
                                string pdfPath = Tools.ReportsPath + "HutangValas" + "_" + _userID + ".pdf";
                                FileInfo excelFile = new FileInfo(filePath);
                                if (excelFile.Exists)
                                {
                                    excelFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                    excelFile = new FileInfo(filePath);
                                }


                                // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                using (ExcelPackage package = new ExcelPackage(excelFile))
                                {
                                    package.Workbook.Properties.Title = "HutangValasReport";
                                    package.Workbook.Properties.Author = Tools.DefaultReportAuthor();
                                    package.Workbook.Properties.Comments = Tools.DefaultReportComments();
                                    package.Workbook.Properties.Company = Tools.DefaultReportCompany();
                                    package.Workbook.Properties.SetCustomPropertyValue("Checked by", _userID);
                                    package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", Tools.DefaultReportAssemblyName());

                                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("HutangValas Report");


                                    //ATUR DATA GROUPINGNYA DULU
                                    List<HutangValas> rList = new List<HutangValas>();
                                    while (dr0.Read())
                                    {
                                        HutangValas rSingle = new HutangValas();
                                        rSingle.HutangBankUSD = Convert.ToDecimal(dr0["HutangBankUSD"]);
                                        rSingle.HutangBankEUR = Convert.ToDecimal(dr0["HutangBankEUR"]);
                                        rSingle.HutangBankJPY = Convert.ToDecimal(dr0["HutangBankJPY"]);
                                        rSingle.HutangBankGBP = Convert.ToDecimal(dr0["HutangBankGBP"]);
                                        rSingle.HutangBankAUD = Convert.ToDecimal(dr0["HutangBankAUD"]);
                                        rSingle.HutangBankSGD = Convert.ToDecimal(dr0["HutangBankSGD"]);
                                        rSingle.HutangBankLainnya = Convert.ToDecimal(dr0["HutangBankLainnya"]);
                                        rSingle.HutangObligasiUSD = Convert.ToDecimal(dr0["HutangObligasiUSD"]);
                                        rSingle.HutangObligasiEUR = Convert.ToDecimal(dr0["HutangObligasiEUR"]);
                                        rSingle.HutangObligasiJPY = Convert.ToDecimal(dr0["HutangObligasiJPY"]);
                                        rSingle.HutangObligasiGBP = Convert.ToDecimal(dr0["HutangObligasiGBP"]);
                                        rSingle.HutangObligasiAUD = Convert.ToDecimal(dr0["HutangObligasiAUD"]);
                                        rSingle.HutangObligasiSGD = Convert.ToDecimal(dr0["HutangObligasiSGD"]);
                                        rSingle.HutangObligasiLainnya = Convert.ToDecimal(dr0["HutangObligasiLainnya"]);
                                        rSingle.HutanglainlainUSD = Convert.ToDecimal(dr0["HutanglainlainUSD"]);
                                        rSingle.HutanglainlainEUR = Convert.ToDecimal(dr0["HutanglainlainEUR"]);
                                        rSingle.HutanglainlainJPY = Convert.ToDecimal(dr0["HutanglainlainJPY"]);
                                        rSingle.HutanglainlainGBP = Convert.ToDecimal(dr0["HutanglainlainGBP"]);
                                        rSingle.HutanglainlainAUD = Convert.ToDecimal(dr0["HutanglainlainAUD"]);
                                        rSingle.HutanglainlainSGD = Convert.ToDecimal(dr0["HutanglainlainSGD"]);
                                        rSingle.HutanglainlainLainnya = Convert.ToDecimal(dr0["HutanglainlainLainnya"]);
                                        rList.Add(rSingle);

                                    }

                                    var QueryByClientID =
                                     from r in rList
                                     group r by new
                                     {
                                         r.HutangBankUSD,
                                         r.HutangBankEUR,
                                         r.HutangBankJPY,
                                         r.HutangBankGBP,
                                         r.HutangBankAUD,
                                         r.HutangBankSGD,
                                         r.HutangBankLainnya,
                                         r.HutangObligasiUSD,
                                         r.HutangObligasiAUD,
                                         r.HutangObligasiEUR,
                                         r.HutangObligasiGBP,
                                         r.HutangObligasiJPY,
                                         r.HutangObligasiLainnya,
                                         r.HutangObligasiSGD,
                                         r.HutanglainlainAUD,
                                         r.HutanglainlainEUR,
                                         r.HutanglainlainGBP,
                                         r.HutanglainlainJPY,
                                         r.HutanglainlainSGD,
                                         r.HutanglainlainUSD,
                                         r.HutanglainlainLainnya
                                     } into rGroup
                                     select rGroup;

                                    int incRowExcel = 1;
                                    int _startRowDetail = 0;
                                    foreach (var rsHeader in QueryByClientID)
                                    {

                                        worksheet.Cells[incRowExcel, 6].Value = "Lampiran 1";
                                        worksheet.Cells["F" + incRowExcel + ":G" + incRowExcel].Merge = true;
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 6].Value = "Surat Direktur Pengelolaan Investasi";
                                        worksheet.Cells["F" + incRowExcel + ":I" + incRowExcel].Merge = true;
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 6].Value = "Nomor : S-2622/PM.21/2013";
                                        worksheet.Cells["F" + incRowExcel + ":I" + incRowExcel].Merge = true;
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 6].Value = "Tanggal : 11 Maret 2013";
                                        worksheet.Cells["F" + incRowExcel + ":I" + incRowExcel].Merge = true;
                                        incRowExcel = incRowExcel + 3;
                                        //Row A = 2

                                        worksheet.Cells[incRowExcel, 1].Value = _host.Get_CompanyName();
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                        worksheet.Cells["A" + incRowExcel + ":I" + incRowExcel].Merge = true;
                                        worksheet.Cells["A" + incRowExcel + ":I" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "Informasi Total Hutang/Pinjaman Dalam Valuta Asing";
                                        worksheet.Cells["A" + incRowExcel + ":I" + incRowExcel].Merge = true;
                                        worksheet.Cells["A" + incRowExcel + ":I" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "Per Bulan ";
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                        worksheet.Cells["A" + incRowExcel + ":I" + incRowExcel].Merge = true;
                                        worksheet.Cells["A" + incRowExcel + ":I" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                        incRowExcel = 11;
                                        int RowA = incRowExcel;
                                        int RowB = incRowExcel + 1;
                                        worksheet.Cells["A" + RowA + ":A" + RowB].Value = "NO";
                                        worksheet.Cells["B" + RowA + ":B" + RowB].Value = "Jenis Hutang/Pinjaman";

                                        worksheet.Cells["A" + RowA + ":A" + RowB].Merge = true;
                                        worksheet.Cells["B" + RowA + ":B" + RowB].Merge = true;

                                        worksheet.Cells[incRowExcel, 3].Value = "Jenis Valuta Asing";
                                        worksheet.Cells["C" + incRowExcel + ":I" + incRowExcel].Merge = true;
                                        worksheet.Cells["C" + incRowExcel + ":I" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 3].Value = "USD";
                                        worksheet.Cells[incRowExcel, 4].Value = "EUR";
                                        worksheet.Cells[incRowExcel, 5].Value = "JPY";
                                        worksheet.Cells[incRowExcel, 6].Value = "GBP";
                                        worksheet.Cells[incRowExcel, 7].Value = "AUD";
                                        worksheet.Cells[incRowExcel, 8].Value = "SGD";
                                        worksheet.Cells[incRowExcel, 9].Value = "Lainnya";
                                        worksheet.Cells["A13:A13"].Value = "1";
                                        worksheet.Cells["A14:A14"].Value = "2";
                                        worksheet.Cells["A15:A15"].Value = "3";
                                        worksheet.Cells["B13:B13"].Value = "Hutang Bank";
                                        worksheet.Cells["B14:B14"].Value = "Hutang Obligasi";
                                        worksheet.Cells["B15:B15"].Value = "Hutang Lain-lain";
                                        worksheet.Cells["B17:B17"].Value = "Total";
                                        worksheet.Cells["A" + incRowExcel + ":I" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                        //area header
                                        incRowExcel++;
                                        _startRowDetail = incRowExcel;

                                        worksheet.Cells["C13:C13"].Value = rsHeader.Key.HutangBankUSD;
                                        worksheet.Cells["D13:D13"].Value = rsHeader.Key.HutangBankEUR;
                                        worksheet.Cells["E13:E13"].Value = rsHeader.Key.HutangBankJPY;
                                        worksheet.Cells["F13:F13"].Value = rsHeader.Key.HutangBankGBP;
                                        worksheet.Cells["G13:G13"].Value = rsHeader.Key.HutangBankAUD;
                                        worksheet.Cells["H13:H13"].Value = rsHeader.Key.HutangBankSGD;
                                        worksheet.Cells["I13:I13"].Value = rsHeader.Key.HutangBankLainnya;

                                        worksheet.Cells["C14:C14"].Value = rsHeader.Key.HutangObligasiUSD;
                                        worksheet.Cells["D14:D14"].Value = rsHeader.Key.HutangObligasiEUR;
                                        worksheet.Cells["E14:E14"].Value = rsHeader.Key.HutangObligasiJPY;
                                        worksheet.Cells["F14:F14"].Value = rsHeader.Key.HutangObligasiGBP;
                                        worksheet.Cells["G14:G14"].Value = rsHeader.Key.HutangObligasiAUD;
                                        worksheet.Cells["H14:H14"].Value = rsHeader.Key.HutangObligasiSGD;
                                        worksheet.Cells["I14:I14"].Value = rsHeader.Key.HutangObligasiLainnya;

                                        worksheet.Cells["C15:C15"].Value = rsHeader.Key.HutanglainlainUSD;
                                        worksheet.Cells["D15:D15"].Value = rsHeader.Key.HutanglainlainEUR;
                                        worksheet.Cells["E15:E15"].Value = rsHeader.Key.HutanglainlainJPY;
                                        worksheet.Cells["F15:F15"].Value = rsHeader.Key.HutanglainlainGBP;
                                        worksheet.Cells["G15:G15"].Value = rsHeader.Key.HutanglainlainAUD;
                                        worksheet.Cells["H15:H15"].Value = rsHeader.Key.HutanglainlainSGD;
                                        worksheet.Cells["I15:I15"].Value = rsHeader.Key.HutanglainlainLainnya;


                                        incRowExcel = incRowExcel + 4;

                                        worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                        worksheet.Cells[incRowExcel, 3].Formula = "SUM(C" + "13" + ":C" + "15" + ")";
                                        worksheet.Cells[incRowExcel, 4].Formula = "SUM(D" + "13" + ":D" + "15" + ")";
                                        worksheet.Cells[incRowExcel, 5].Formula = "SUM(E" + "13" + ":E" + "15" + ")";
                                        worksheet.Cells[incRowExcel, 6].Formula = "SUM(F" + "13" + ":F" + "15" + ")";
                                        worksheet.Cells[incRowExcel, 7].Formula = "SUM(G" + "13" + ":G" + "15" + ")";
                                        worksheet.Cells[incRowExcel, 8].Formula = "SUM(H" + "13" + ":H" + "15" + ")";
                                        worksheet.Cells[incRowExcel, 9].Formula = "SUM(I" + "13" + ":I" + "15" + ")";

                                        worksheet.Cells[incRowExcel, 2].Calculate();
                                        worksheet.Cells[incRowExcel, 3].Calculate();
                                        worksheet.Cells[incRowExcel, 4].Calculate();
                                        worksheet.Cells[incRowExcel, 5].Calculate();
                                        worksheet.Cells[incRowExcel, 6].Calculate();
                                        worksheet.Cells[incRowExcel, 7].Calculate();
                                        worksheet.Cells[incRowExcel, 8].Calculate();
                                        worksheet.Cells[incRowExcel, 9].Calculate();

                                        worksheet.Cells["A" + incRowExcel + ":I" + incRowExcel].Style.Numberformat.Format = "#,##0";
                                        //worksheet.Cells["A" + incRowExcel + ":I" + incRowExcel].Style.Font.Bold = true;

                                        worksheet.Cells["A" + "11" + ":I" + "17"].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["A" + "11" + ":I" + "17"].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["A" + "11" + ":I" + "17"].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["A" + "11" + ":I" + "17"].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                                        incRowExcel++;
                                    }



                                    worksheet.PrinterSettings.FitToPage = true;
                                    worksheet.PrinterSettings.FitToWidth = 1;
                                    worksheet.PrinterSettings.FitToHeight = 1;
                                    worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel - 1, 9];
                                    worksheet.Column(1).Width = 5;
                                    worksheet.Column(2).Width = 25;
                                    worksheet.Column(3).Width = 15;
                                    worksheet.Column(4).Width = 15;
                                    worksheet.Column(5).Width = 15;
                                    worksheet.Column(6).Width = 15;
                                    worksheet.Column(7).Width = 15;
                                    worksheet.Column(8).Width = 15;
                                    worksheet.Column(9).Width = 15;


                                    // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                    //worksheet.Cells["A3:O14"].AutoFitColumns();  // UNTUK AUTO FIT UKURAN TULISAN DENGAN KOLOM, KEBETULAN INI BUAT HEADER AJA DIPAKENYA

                                    worksheet.PrinterSettings.Orientation = eOrientation.Landscape; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                    worksheet.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN
                                    worksheet.HeaderFooter.OddHeader.RightAlignedText = "&12 HUTANG VALAS REPORT";

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
            #endregion

        }

        public string FundClient_GenerateKPD(SInvestRpt _sinvestRpt)
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
select F.AUM,E.CashAmount,InstrumentTypePK,ROW_NUMBER() OVER(ORDER BY C.ID ASC) AS PK,H.ClientCategory KodeNasabah ,H.Name NamaNasabah ,NKPDName NomorKontrak,isnull(CONVERT(VARCHAR(8), A.EffectiveDate, 112),0) TanggalKontrak,isnull(CONVERT(VARCHAR(8), A.MaturityDate, 112),0) TanggalJatuhTempo,
0 NomorAdendum,0 TanggalAdendum,cast(isnull(TotalUnits,CAST(TotalUnits AS DECIMAL(22, 0))) * isnull(A.Nav,CAST(A.Nav AS DECIMAL(22, 0)))as DECIMAL(22, 0)) NilaiInvestasiAwalIDR,0 NilaiInvestasiAwalNonIDR,case when C.InstrumentTypePK not in (1,5) then CAST(isnull(dbo.FGetTotalMarketValue(@Date,A.FundPK),0) AS DECIMAL(22, 0)) else case when C.InstrumentTypePK = 1 then CAST(F.AUM - isnull(E.CashAmount,0) AS DECIMAL(22, 0)) end end NilaiInvestasiAkhir,
0 NilaiInvestasiAkhirNonIDR, C.ID JenisEfek,1 DNatauLN,B.Balance JumlahEfek,CAST(B.CostValue AS DECIMAL(22, 0)) NilaiPembelian,CAST(0 AS DECIMAL(22, 2)) NilaiNominal,
case when C.InstrumentTypePK not in (1,5) then   CAST(B.ClosePrice AS DECIMAL(22, 6))  else CAST(B.ClosePrice AS DECIMAL(22, 0)) end HPW,CAST(0 AS DECIMAL(22, 2)) Deposito,0 TotalNilai,isnull(D.ID,0) KodeBK,C.InstrumentTypePK type, '1' KodeSaham,CAST(B.MarketValue AS DECIMAL(22, 0)) MarketValue,isnull(H.SID,'') SID from Fund A
left join FundPosition B on A.FundPK = B.FundPK and B.Status = 2
left join Instrument C on B.InstrumentPK = C.InstrumentPK and C.Status = 2 
left join BankBranch D on A.BankBranchPK = D.BankBranchPK and D.status  = 2
left join DistributedIncome E on A.FundPK = E.FundPK and E.status  = 2 and E.ValueDate <= @Date and Posted = 1
left join CloseNAV F on A.FundPK = F.FundPK and F.Status = 2 and F.Date = @Date
left join FundClientposition G on A.FundPK = G.FundPK
left join FundClient H on G.FundClientPK = H.FundClientPK and H.status = 2
--left join ClosePrice I on B.InstrumentPK = I.instrumentPK and I.status = 2 and B.Date = @Date
where A.FundPK = @FundPK and B.Date = @Date and A.status = 2
Group By  F.AUM,E.CashAmount,C.ID,H.ClientCategory,H.Name,NKPDName,A.EffectiveDate,A.MaturityDate,A.TotalUnits,A.Nav,E.CashAmount,B.Balance,B.CostValue,B.ClosePrice,D.ID,C.InstrumentTypePK,B.MarketValue,A.FundPK,H.SID
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
'|' + case when type = 1 then isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(JumlahEfek,'')))),'') else case when type not in (1,5) then isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(KodeSaham,'')))),'') else '' end end + --14
--'|' + isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(JumlahEfek,'')))),'')  +  --14
'|' + case when type = 1 then isnull(RTRIM(LTRIM(isnull(NilaiPembelian,''))),'') else case when type not in (1,5) then isnull(RTRIM(LTRIM(isnull(JumlahEfek,''))),'') else '' end end + --15
--'|' + isnull(RTRIM(LTRIM(isnull(NilaiPembelian,''))),'')  +  --15
--'|' + case when type = 1 then isnull(RTRIM(LTRIM(isnull(MarketValue,''))),'') else case when type not in (1,5) then isnull(RTRIM(LTRIM(isnull(MarketValue,''))),'') else '' end end + --16
'|' + isnull(RTRIM(LTRIM(isnull(0,''))),'')  +  --16
--'|' + case when type = 1 then isnull(RTRIM(LTRIM(isnull(HPW,''))),'') else case when type not in (1,5) then isnull(RTRIM(LTRIM(isnull(PriceBond,''))),'') else '' end end +
'|' + isnull(RTRIM(LTRIM(isnull(HPW,''))),'')  +  --17
'|' + isnull(RTRIM(LTRIM(isnull(Deposito,''))),'')  +  --18
'|' + case when type = 1 then isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(MarketValue,'')))),'') else case when type not in (1,5) then isnull(RTRIM(LTRIM(isnull(MarketValue,''))),'') else '' end end + --19
--'|' + isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(TotalNilai,'')))),'')  +   --19
'|' + isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(KodeBK,'')))),'') + --20
'|' + '0' +--21
'|' + isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(SID,'')))),'') --22
from #KPD

select * from #text

                         ";
                        cmd.Parameters.AddWithValue("@Date", _sinvestRpt.Date);
                        cmd.Parameters.AddWithValue("@FundPK", _sinvestRpt.Fund);

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
                                    return Tools.HtmlARIATextPath + _companyCode + "KPD.txt";
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

        public Boolean GenerateReportKPD(string _userID, SInvestRpt _sInvestRpt)
        {
            #region KPD
            if (_sInvestRpt.ReportName.Equals("5"))
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


                             create table #Text(      
[ResultText] [nvarchar](1000)  NULL          
)                        
        
        

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
select F.AUM,E.CashAmount,InstrumentTypePK,ROW_NUMBER() OVER(ORDER BY C.ID ASC) AS PK,H.ClientCategory KodeNasabah ,H.Name NamaNasabah ,NKPDName NomorKontrak,isnull(CONVERT(VARCHAR(8), A.EffectiveDate, 112),0) TanggalKontrak,isnull(CONVERT(VARCHAR(8), A.MaturityDate, 112),0) TanggalJatuhTempo,
0 NomorAdendum,0 TanggalAdendum,cast(isnull(TotalUnits,CAST(TotalUnits AS DECIMAL(22, 0))) * isnull(A.Nav,CAST(A.Nav AS DECIMAL(22, 0)))as DECIMAL(22, 0)) NilaiInvestasiAwalIDR,0 NilaiInvestasiAwalNonIDR,case when C.InstrumentTypePK not in (1,5) then CAST(isnull(dbo.FGetTotalMarketValue(@Date,A.FundPK),0) AS DECIMAL(22, 0)) else case when C.InstrumentTypePK = 1 then CAST(F.AUM - isnull(E.CashAmount,0) AS DECIMAL(22, 0)) end end NilaiInvestasiAkhir,
0 NilaiInvestasiAkhirNonIDR, C.ID JenisEfek,1 DNatauLN,B.Balance JumlahEfek,CAST(B.CostValue AS DECIMAL(22, 0)) NilaiPembelian,CAST(0 AS DECIMAL(22, 2)) NilaiNominal,
case when C.InstrumentTypePK not in (1,5) then   CAST(B.ClosePrice AS DECIMAL(22, 6))  else CAST(B.ClosePrice AS DECIMAL(22, 0)) end HPW,CAST(0 AS DECIMAL(22, 2)) Deposito,0 TotalNilai,isnull(D.ID,0) KodeBK,C.InstrumentTypePK type, '1' KodeSaham,CAST(B.MarketValue AS DECIMAL(22, 0)) MarketValue,isnull(H.SID,'') SID from Fund A
left join FundPosition B on A.FundPK = B.FundPK and B.Status = 2
left join Instrument C on B.InstrumentPK = C.InstrumentPK and C.Status = 2 
left join BankBranch D on A.BankBranchPK = D.BankBranchPK and D.status  = 2
left join DistributedIncome E on A.FundPK = E.FundPK and E.status  = 2 and E.ValueDate <= @Date and Posted = 1
left join CloseNAV F on A.FundPK = F.FundPK and F.Status = 2 and F.Date = @Date
left join FundClientposition G on A.FundPK = G.FundPK
left join FundClient H on G.FundClientPK = H.FundClientPK and H.status = 2
--left join ClosePrice I on B.InstrumentPK = I.instrumentPK and I.status = 2 and B.Date = @Date
where A.FundPK = @FundPK and B.Date = @Date and A.status = 2
Group By  F.AUM,E.CashAmount,C.ID,H.ClientCategory,H.Name,NKPDName,A.EffectiveDate,A.MaturityDate,A.TotalUnits,A.Nav,E.CashAmount,B.Balance,B.CostValue,B.ClosePrice,D.ID,C.InstrumentTypePK,B.MarketValue,A.FundPK,H.SID
order By C.ID asc

Fetch next From A Into @FundPK
end
Close A
Deallocate A

update #KPD set 
NomorAdendum = 0, TanggalAdendum = 0, NilaiInvestasiAwalNonIDR = 0, --NilaiInvestasiAkhir = CAST(0 AS DECIMAL(22, 2)),
NilaiInvestasiAkhirNonIDR = 0 where PK <> 1


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
,case when type = 1 then isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(JumlahEfek,'')))),'') else case when type not in (1,5) then isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(KodeSaham,'')))),'') else '' end end JumlahEfek --14
--'|' + isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(JumlahEfek,'')))),'')  +  --14
, case when type = 1 then isnull(RTRIM(LTRIM(isnull(NilaiPembelian,''))),'') else case when type not in (1,5) then isnull(RTRIM(LTRIM(isnull(JumlahEfek,''))),'') else '' end end NilaiPembelian --15
--'|' + isnull(RTRIM(LTRIM(isnull(NilaiPembelian,''))),'')  +  --15
--'|' + case when type = 1 then isnull(RTRIM(LTRIM(isnull(MarketValue,''))),'') else case when type not in (1,5) then isnull(RTRIM(LTRIM(isnull(MarketValue,''))),'') else '' end end + --16
, isnull(RTRIM(LTRIM(isnull(0,''))),'')  NilaiNominal  --16
--'|' + case when type = 1 then isnull(RTRIM(LTRIM(isnull(HPW,''))),'') else case when type not in (1,5) then isnull(RTRIM(LTRIM(isnull(PriceBond,''))),'') else '' end end +
, isnull(RTRIM(LTRIM(isnull(HPW,''))),'') HPW   --17
, isnull(RTRIM(LTRIM(isnull(Deposito,''))),'')  Deposito  --18
, case when type = 1 then isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(MarketValue,'')))),'') else case when type not in (1,5) then isnull(RTRIM(LTRIM(isnull(MarketValue,''))),'') else '' end end TotalInvestasi --19
--'|' + isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(TotalNilai,'')))),'')    --19
, isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(KodeBK,'')))),'') KodeBK --20
, '0' Keterangan --21
, isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(SID,'')))),'') SID --22
from #KPD
                            ";


                            //cmd.Parameters.AddWithValue("@date", _sInvestRpt.ParamDate);

                            cmd.Parameters.AddWithValue("@Date", _sInvestRpt.Date);
                            cmd.Parameters.AddWithValue("@FundPK", _sInvestRpt.FundFrom);

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

            else
            {
                return false;
            }
        }


        public string GenerateSID(string _userID, SIDRpt _sidRpt)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                string FilePath = Tools.SIDPath + "SID" + "_" + _sidRpt.FundClientPK + ".xlsx";
                string pdfPath = Tools.SIDPath + "SID" + "_" + _sidRpt.FundClientPK + ".pdf";
                File.Copy(Tools.ReportsTemplatePath + "SID.xlsx", FilePath, true);
                FileInfo existingFile = new FileInfo(FilePath);
                //if (existingFile.Exists)
                //{
                //    existingFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                //    existingFile = new FileInfo(FilePath);
                //}
                using (ExcelPackage package = new ExcelPackage(existingFile))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[1];

                    using (SqlConnection DbCon01 = new SqlConnection(Tools.conString))
                    {
                        DbCon01.Open();
                        using (SqlCommand cmd01 = DbCon01.CreateCommand())
                        {
                            cmd01.CommandText = @"
                            declare @Kota nvarchar(500)
                            select @Kota = b.DescOne from fundclient a left join mastervalue b on a.KodeKotaInd1 = b.code and b.id = 'CityRHB' where a.status in (1,2) and FundClientPK = @FundClientPK

                            select 'PEM'+ '-' + CONVERT(varchar(10), FundClientPK) + '/' + 'STL-UR-ARO' + '/' + @Month + '/' + CONVERT(varchar(10), YEAR(getdate()))  LetterNo,Name, isnull(SID,'') SID, case when clientcategory = 1 then isnull(Email,'') else case when clientcategory = 2 then isnull(CompanyMail,'') end end Email, isnull(IFUACode,'') IFUACode, case when clientcategory = 1 then isnull(AlamatInd1,'') else case when clientcategory = 2 then isnull(AlamatPerusahaan,'') end end AlamatInd1,
                            case when clientcategory = 1 then isnull(KodePosInd1,'') else case when clientcategory = 2 then isnull(KodePosIns,'') end end KodePosInd1, case when clientcategory = 1 then isnull(NoIdentitasInd1,'') else case when clientcategory = 2 then isnull(NPWP,'') end end NoIdentitasInd1,  isnull(@Kota,'') KodeKotaInd1, 'Sehubungan dengan penerapan nomor tunggal identitas pemodal (Single Investor Identification - SID) untuk investor Reksa Dana, maka dengan ini kami memberitahukan sekaligus mendistribusikan Nomor SID Saudara sebagai berikut' Body,ClientCategory from fundclient where status in (1,2) and FundClientPK = @FundClientPK";
                            cmd01.Parameters.AddWithValue("@FundClientPK", _sidRpt.FundClientPK);
                            cmd01.Parameters.AddWithValue("@Month", _sidRpt.Month);

                            using (SqlDataReader dr01 = cmd01.ExecuteReader())
                            {
                                if (dr01.HasRows)
                                {
                                    List<SIDRpt> rList = new List<SIDRpt>();
                                    while (dr01.Read())
                                    {
                                        SIDRpt rSingle = new SIDRpt();
                                        rSingle.Name = Convert.ToString(dr01["Name"]);
                                        rSingle.SID = Convert.ToString(dr01["SID"]);
                                        rSingle.Email = Convert.ToString(dr01["Email"]);
                                        rSingle.Alamat = Convert.ToString(dr01["AlamatInd1"]);
                                        rSingle.IFUACode = Convert.ToString(dr01["IFUACode"]);
                                        rSingle.KodePos = Convert.ToString(dr01["KodePosInd1"]);
                                        rSingle.NoIdentitas = Convert.ToString(dr01["NoIdentitasInd1"]);
                                        rSingle.LetterNo = Convert.ToString(dr01["LetterNo"]);
                                        rSingle.Body = Convert.ToString(dr01["Body"]);
                                        rSingle.KodeKotaInd1 = Convert.ToString(dr01["KodeKotaInd1"]);
                                        rSingle.ClientCategory = Convert.ToString(dr01["ClientCategory"]);
                                        rList.Add(rSingle);
                                    }
                                    var QueryByClientID =
                                     from r in rList
                                     group r by new { r.Name, r.Alamat, r.KodePos, r.Body, r.SID, r.NoIdentitas, r.ClientCategory } into rGroup
                                     select rGroup;

                                    int incRowExcel = 9;
                                    int _IncRow = 14;
                                    int _incRow = 10;
                                    foreach (var rsHeader in QueryByClientID)
                                    {

                                        foreach (var rsDetail in rsHeader)
                                        {

                                            incRowExcel = incRowExcel + 13;
                                            worksheet.Cells["B7:B7"].Value = "Kepada Yth.";
                                            worksheet.Cells["B7:B7"].Style.Font.Bold = true;
                                            worksheet.Cells["B7:B7"].Style.Font.Size = 14;

                                            worksheet.Cells["B8:B8"].Value = "Investor Reksa Dana";
                                            worksheet.Cells["B8:B8"].Style.Font.Bold = true;
                                            worksheet.Cells["B8:B8"].Style.Font.Size = 14;
                                            if (rsDetail.ClientCategory == "1")
                                            {
                                                worksheet.Cells["B9:B9"].Value = "Bapak/Ibu " + rsDetail.Name;
                                                worksheet.Cells["B9:B9"].Style.Font.Bold = true;
                                                worksheet.Cells["B9:B9"].Style.Font.Size = 12;
                                            }
                                            else
                                            {
                                                worksheet.Cells["B9:B9"].Value = rsDetail.Name;
                                                worksheet.Cells["B9:B9"].Style.Font.Bold = true;
                                                worksheet.Cells["B9:B9"].Style.Font.Size = 12;
                                            }

                                            worksheet.Cells["B10:B10"].Value = rsDetail.Alamat + ", " + rsDetail.KodeKotaInd1 + ", " + rsDetail.KodePos;
                                            worksheet.Cells["B10:B10"].Style.WrapText = true;
                                            worksheet.Cells["B10:C10"].Merge = true;
                                            worksheet.Cells["B10:B10"].Style.Font.Size = 11;

                                            worksheet.Row(_incRow).Height = 50;
                                            //worksheet.Cells["B11:B11"].Value = rsDetail.KodePos;
                                            //worksheet.Cells["B11:B11"].Style.Font.Size = 11;



                                            worksheet.Cells["B14:E14"].Value = "PEMBERITAHUAN";
                                            worksheet.Cells["B14:E14"].Style.Font.Bold = true;
                                            worksheet.Cells["B14:E14"].Merge = true;
                                            worksheet.Cells["B14:E14"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;


                                            worksheet.Cells["B15:E15"].Value = "Nomor : " + rsDetail.LetterNo;
                                            worksheet.Cells["B15:E15"].Merge = true;
                                            worksheet.Cells["B15:E15"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                            incRowExcel = 17;
                                            worksheet.Cells["B17:E17"].Value = rsDetail.Body;
                                            worksheet.Cells["B17:E17"].Style.WrapText = true;
                                            worksheet.Cells["B17:E17"].Merge = true;
                                            worksheet.Row(incRowExcel).Height = 55;
                                            worksheet.Cells["B17:E17"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Justify;



                                            worksheet.Cells["B19:B19"].Value = "Nama ";
                                            worksheet.Cells["C19:C19"].Value = " : " + rsDetail.Name;
                                            worksheet.Cells["B19:C19"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;


                                            worksheet.Cells["B20:B20"].Value = "No. Identitas";
                                            worksheet.Cells["C20:C20"].Value = " : " + rsDetail.NoIdentitas;
                                            worksheet.Cells["B20:C20"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;


                                            worksheet.Cells["B21:B21"].Value = "No.SID";
                                            worksheet.Cells["C21:C21"].Value = " : " + rsDetail.SID;
                                            worksheet.Cells["B21:C21"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;


                                            worksheet.Cells["B27:E27"].Value = "Atas perhatian Bapak/Ibu, kami mengucapkan terima kasih";
                                            worksheet.Cells["B27:E27"].Merge = true;
                                            worksheet.Cells["B27:E27"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                                            worksheet.Cells["B37:B37"].Value = Convert.ToDateTime(_datetimeNow).ToString("dd MMMM yyyy");

                                            worksheet.Cells["B39:B39"].Value = "Hormat kami,";

                                            worksheet.Cells["B45:C45"].Value = _host.Get_CompanyName();
                                            worksheet.Cells["B45:C45"].Merge = true;
                                            worksheet.Cells["B45:C45"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;


                                            worksheet.Cells["B46:C46"].Value = "Fungsi Penyelesaian Transaksi Efek *";
                                            worksheet.Cells["B46:C46"].Merge = true;
                                            worksheet.Cells["B46:C46"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;


                                            worksheet.Cells["B55:E55"].Value = "*) Surat ini tidak memerlukan tanda tangan karena dicetak secara komputerisasi";
                                            worksheet.Cells["B55:E55"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            worksheet.Cells["B55:E55"].Merge = true;

                                            incRowExcel = 66;
                                            worksheet.Row(incRowExcel).PageBreak = true;
                                        }
                                    }

                                    string _range = "A" + _IncRow + ":M" + incRowExcel;
                                    using (ExcelRange r = worksheet.Cells[_range]) // KALO  KOLOM 1 SAMPE 9 A-I
                                    {
                                        //NILAINYA NGAMBIL DARI DEFAULT DI TOOLS, KLO MAU BEDA SENDIRI BOLEH2 AJA.
                                        r.Style.Font.Size = 14;

                                    }


                                    worksheet.PrinterSettings.FitToPage = true;
                                    worksheet.PrinterSettings.FitToWidth = 1;
                                    worksheet.PrinterSettings.FitToHeight = 0;
                                    worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel - 1, 6];
                                    worksheet.Column(1).Width = 10;
                                    worksheet.Column(2).Width = 35;
                                    worksheet.Column(3).Width = 35;
                                    worksheet.Column(4).Width = 35;
                                    worksheet.Column(5).Width = 35;
                                    worksheet.Column(6).Width = 10;

                                }


                            }

                        }
                    }

                    package.Save();
                    Tools.ExportFromExcelToPDF(FilePath, pdfPath);
                    return FilePath;
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }


        public string CheckExistingFile(string _userID, SIDRpt _sidRpt)
        {
            string fileName;
            if(_sidRpt.ReportName == "Generate SID")
            {
                fileName = Tools.SIDPath + "SID_" + _sidRpt.FundClientPK + ".pdf";

            }
            else
            {
                fileName = Tools.UnitTrustPath + "UnitTrustReport" + "_" + _sidRpt.ClientName + ".pdf";
            }
            

            if (File.Exists(fileName))
                return ("File is Exist");
            else
                return ("File not Exist");

        }


        public void SendMail(string _usersID, SIDRpt _sidRpt)
        {
            string uriPath;
            string localPath;
            string SubjectMail;
            string _from;
            var bodymail = "";

            if (_sidRpt.ReportName == "Generate SID")
            {
                bodymail = " <html> " +
                                " <head> " +
                                    " <title></title> " +
                                " </head> " +
                                " <body> " +
                                    " <div> " +
                                        " <div> Please find attached <br />  " +

                                        " </div> " +
                                        " <br /><br /><br /> " +
                                        " <br /><br /><br /> " +
                                        " Best Regards, " +
                                        " <br />" +
                                        "  Aurora Asset Management " +
                                        " <br /> " +
                                        " <br /><br /><br /> " +
                                    " </div> " +
                                " </body> " +
                                " </html> ";


                uriPath = Tools.SIDPath + "SID_" + _sidRpt.FundClientPK + ".pdf;" + Tools.SIDPath + "Surat Penyebarluasan Informasi Fitur AKSes Kepada Nasabah Produk Investasi (Ttd).pdf";
                localPath = new Uri(uriPath).LocalPath;
                SubjectMail = "SID Information_" + _sidRpt.Name;
                _from = "operation@aurora-am.co.id";
            }
            else
            {
                bodymail = " <html> " +
                                " <head> " +
                                    " <title></title> " +
                                " </head> " +
                                " <body> " +
                                    " <div> " +
                                        " <div> Please find attached <br />  " +

                                        " </div> " +
                                        " <br /><br /><br /> " +
                                        " <br /><br /><br /> " +
                                        " Best Regards, " +
                                        " <br />" +
                                        "  Jasa Capital Asset Management" +
                                        " <br /> " +
                                        " <br /><br /><br /> " +
                                    " </div> " +
                                " </body> " +
                                " </html> ";



                uriPath = Tools.UnitTrustPath + "UnitTrustReport_" + _sidRpt.Name + ".pdf";
                localPath = new Uri(uriPath).LocalPath;
                SubjectMail = "Unit Trust Report_" + _sidRpt.Name;
                _from = "info@jasacapital.co.id";
            }



            SendEmailReps.DataSendEmail dt = new SendEmailReps.DataSendEmail();
            dt = SendEmailReps.SendEmailTestingByInput(_usersID, _sidRpt.Email, SubjectMail, bodymail, localPath, _from);
        }




        public List<FundClientSearchResult> FundClientSearchViewOnAgentUsers_Select(int _status, string _param, string _usersID)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<FundClientSearchResult> L_FundClient = new List<FundClientSearchResult>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        if (Tools.ClientCode == "08")
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandTimeout = 0;
                            cmd.CommandText = "SearchFundClientViewOnAgentusers_08";
                        }
                        else
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandTimeout = 0;
                            cmd.CommandText = "SearchFundClientViewOnAgentusers";
                        }
                        cmd.Parameters.AddWithValue("@str", _param);
                        cmd.Parameters.AddWithValue("@status", _status);
                        cmd.Parameters.AddWithValue("@UsersID", _usersID);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_FundClient.Add(setFundClientSearchResult(dr));
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



        public List<GetHistoricalSummary> GetHistoricalSummary_ByFundClientPK(int _fundClientPK, DateTime _datefrom, DateTime _dateto)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<GetHistoricalSummary> L_HistoricalSummary = new List<GetHistoricalSummary>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = @"
                        
SELECT 
A.SettlementDate TradeDate
,ISNULL(A.FundID,'') FundID
,ISNULL(A.Type,'') Type
,ISNULL(A.CashAmount,0) CashAmount
,ISNULL(A.TotalUnitAmount,0) UnitAmount
,ISNULL(A.NetAmount,0) TotalAmount
,ISNULL(A.NAV,0) NAV
FROM 
(
	SELECT F.FUndPK,FC.FundClientPK,A.NAVDate SettlementDate,A.Description Remark,A.SubscriptionFeeAmount FeeAmount,isnull(D.Name, '') DepartmentName,isnull(AG.Name,'') AgentName,F.ID FundID,F.Name FundName,NAVDate
	,'Subscription' Type, Fc.ID ClientID,FC.Name ClientName, CashAmount, TotalUnitAmount ,A.Nav,TotalCashAmount NetAmount ,A.subscriptionFeePercent FeePercent
	from ClientSubscription A 
	left join Fund F on A.FundPK = F.fundPK and f.Status in (1,2)  
	left join FundClient FC on A.FundClientPK = FC.FundClientPK and fc.Status in (1,2) 
	left join Agent AG on A.AgentPK = AG.AgentPK and AG.Status in (1,2) 
	left join Department D on D.DepartmentPK = AG.DepartmentPK and D.Status in (1,2) 
	where 
	(A.Status = 1 Or A.Status = 2 or A.Posted = 1) and  A.Revised = 0 and A.FundClientPK = @FundClientPK and 
	A.status <> 3 and
	valuedate Between @ValueDateFrom and @ValueDateTo  --and A.status = 2 and F.fundPK = 1
	UNION ALL   
	Select F.FundPK,FC.FundClientPK,A.PaymentDate SettlementDate,A.Description Remark,A.RedemptionFeeAmount FeeAmount,isnull(D.Name, '') DepartmentName,isnull(AG.Name,'') AgentName,F.ID FundID,F.Name FundName,NAVDate
	,'Redemption' Type, Fc.ID ClientID,FC.Name ClientName, CashAmount, A.UnitAmount TotalUnitAmount,A.Nav,TotalCashAmount NetAmount ,A.RedemptionFeePercent FeePercent
	from ClientRedemption A 
	left join Fund F on A.FundPK = F.fundPK and f.Status in (1,2)    
	left join FundClient FC on A.FundClientPK = FC.FundClientPK and fc.Status in (1,2)   
	left join Agent AG on A.AgentPK = AG.AgentPK and AG.Status in (1,2) 
	left join Department D on D.DepartmentPK = AG.DepartmentPK and D.Status in (1,2)
	where 
	(A.Status = 1 Or A.Status = 2 or A.Posted = 1) and  A.Revised = 0 and A.FundClientPK = @FundClientPK and  
	A.status <> 3 and
	valuedate Between @ValueDateFrom and @ValueDateTo --and A.status = 2 and F.fundPK = 1
	UNION ALL   

	Select F.FundPK,FC.FundClientPK,A.PaymentDate SettlementDate,A.Description Remark,
	CASE WHEN FeeType = 'IN' then A.SwitchingFeeAmount ELSE 0 END  FeeAmount
	,isnull(D.Name, '') DepartmentName,isnull(AG.Name,'') AgentName,F.ID FundID,F.Name FundName,NAVDate
	,'Switching In' Type, Fc.ID ClientID,FC.Name ClientName, A.TotalCashAmountFundFrom CashAmount, A.TotalUnitAmountFundTo TotalUnitAmount,A.NAVFundTo Nav,A.TotalCashAmountFundTo NetAmount, 
	CASE WHEN FeeType = 'IN' then A.SwitchingFeePercent ELSE 0 END  FeePercent
	from ClientSwitching A 
	left join Fund F on A.FundPKTo = F.fundPK and f.Status in (1,2)    
	left join FundClient FC on A.FundClientPK = FC.FundClientPK and fc.Status in (1,2) 
	left join Agent AG on FC.SellingAgentPK = AG.AgentPK and AG.Status in (1,2) 
	left join Department D on D.DepartmentPK = AG.DepartmentPK and D.Status in (1,2)
	where 
	(A.Status = 1 Or A.Status = 2 or A.Posted = 1) and  A.Revised = 0 and A.FundClientPK = @FundClientPK and  
	A.status <> 3 and
	valuedate Between @ValueDateFrom and @ValueDateTo --and A.status = 2 and F.fundPK = 1
	UNION ALL   
	Select F.FundPK,FC.FundClientPK,A.PaymentDate SettlementDate,A.Description Remark,
	CASE WHEN FeeType = 'OUT' then A.SwitchingFeeAmount ELSE 0 END  FeeAmount
	,isnull(D.Name, '') DepartmentName,isnull(AG.Name,'') AgentName,F.ID FundID,F.Name FundName,NAVDate
	,'Switching Out' Type, Fc.ID ClientID,FC.Name ClientName, CashAmount, A.UnitAmount TotalUnitAmount,A.NAVFundFrom Nav,A.TotalCashAmountFundFrom NetAmount 
	,CASE WHEN FeeType = 'OUT' then A.SwitchingFeePercent ELSE 0 END  FeePercent
	from ClientSwitching A 
	left join Fund F on A.FundPKFrom = F.fundPK and f.Status in (1,2)    
	left join FundClient FC on A.FundClientPK = FC.FundClientPK and fc.Status in (1,2) 
	left join Agent AG on FC.SellingAgentPK = AG.AgentPK and AG.Status in (1,2) 
	left join Department D on D.DepartmentPK = AG.DepartmentPK and D.Status in (1,2)
	where 
	(A.Status = 1 Or A.Status = 2 or A.Posted = 1) and  A.Revised = 0 and A.FundClientPK = @FundClientPK and  
	A.status <> 3 and
	valuedate Between @ValueDateFrom and @ValueDateTo --and A.status = 2 and F.fundPK = 1


) A where Remark <> 'Rounding Unit' and A.SettlementDate between @valuedatefrom and @valuedateto
 Order By A.Settlementdate asc
		                                       ";
                        cmd.Parameters.AddWithValue("@FundClientPK", _fundClientPK);
                        cmd.Parameters.AddWithValue("@ValueDateFrom", _datefrom);
                        cmd.Parameters.AddWithValue("@ValueDateTo", _dateto);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_HistoricalSummary.Add(setHistoricalSummary(dr));
                                }
                            }
                            return L_HistoricalSummary;
                        }

                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }



        private GetHistoricalSummary setHistoricalSummary(SqlDataReader dr)
        {

            GetHistoricalSummary M_HistoricalSummary = new GetHistoricalSummary();
            M_HistoricalSummary.TradeDate = Convert.ToString(dr["TradeDate"]);
            M_HistoricalSummary.FundID = Convert.ToString(dr["FundID"]);
            M_HistoricalSummary.Type = Convert.ToString(dr["Type"]);
            M_HistoricalSummary.CashAmount = Convert.ToDecimal(dr["CashAmount"]);
            M_HistoricalSummary.UnitAmount = Convert.ToDecimal(dr["UnitAmount"]);
            M_HistoricalSummary.TotalAmount = Convert.ToDecimal(dr["TotalAmount"]);
            M_HistoricalSummary.NAV = Convert.ToDecimal(dr["NAV"]);
            return M_HistoricalSummary;
        }



        public List<GetPositionSummary> GetPositionSummary_ByFundClientPK(int _fundClientPK, DateTime _dateto)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<GetPositionSummary> L_PositionSummary = new List<GetPositionSummary>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandText = @"
                        DECLARE @MaxDate datetime

                        select @MaxDate = Date from FundClientPosition where Date = (
	                        SELECT MAX(date) from FundClientPosition where Date <= @Date
                        )


                        DECLARE @NAV TABLE
                        (
	                        LastDate DATETIME,
	                        FundPK INT,
	                        NAV NUMERIC(18,4)
                        )


                        DECLARE @NAVLastDate TABLE
                        (
	                        FundPK INT,
	                        LastDate datetime
                        )

                        INSERT INTO @NAVLastDate
                                ( FundPK, LastDate )
                        SELECT FundPK,MAX(Date) FROM CloseNAV WHERE status  = 2 AND Date <= @Date
                        GROUP BY FundPK


                        INSERT INTO @NAV
                                ( LastDate, FundPK, NAV )
                        SELECT A.LastDate,A.FundPK,B.Nav FROM @NAVLastDate A
                        LEFT JOIN dbo.CloseNAV B ON A.FundPK = B.FundPK AND A.LastDate = B.Date AND B.status = 2
                        WHERE B.Date IN(
	                        SELECT DISTINCT lastDate FROM @NAVLastDate
                        )

                        Select  @Date Date ,C.ID FundID,C.Name FundName,E.ID CurrencyID,isnull(D.NAV,0) NAV,A.UnitAmount,
                        case when C.CurrencyPK = 1 then isnull(sum(D.NAV * A.UnitAmount),0) else 0 end Balance,
                        case when C.CurrencyPK <> 1 then isnull(sum(D.NAV * A.UnitAmount),0) else 0 end BalanceUSD
                        From FundClientPosition A   
                        Left join FundClient B on A.FundClientPK = B.FundClientPK and B.Status = 2   
                        Left join Fund C on A.FundPK = C.FundPK and C.Status = 2
                        left join MasterValue mv on b.ClientCategory = mv.Code  and mv.ID = 'ClientCategory'  
                        left join MasterValue mv1 on b.Tipe = mv1.Code  and mv1.ID = 'CompanyType' 
                        left Join @NAV D on A.FundPK = D.FundPK    
                        left Join Currency E on C.CurrencyPK = E.CurrencyPK and E.status = 2
                        where A.Date = @MaxDate   and A.UnitAmount <> 0   and A.FundClientPK = @FundClientPK      
                        group by  C.ID ,C.Name,E.ID ,D.Nav,A.UnitAmount,C.CurrencyPK
		                                       ";
                        cmd.Parameters.AddWithValue("@FundClientPK", _fundClientPK);
                        cmd.Parameters.AddWithValue("@Date", _dateto);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_PositionSummary.Add(setPositionSummary(dr));
                                }
                            }
                            return L_PositionSummary;
                        }

                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }


        private GetPositionSummary setPositionSummary(SqlDataReader dr)
        {

            GetPositionSummary M_PositionSummary = new GetPositionSummary();
            M_PositionSummary.Date = Convert.ToString(dr["Date"]);
            M_PositionSummary.FundID = Convert.ToString(dr["FundID"]);
            M_PositionSummary.FundName = Convert.ToString(dr["FundName"]);
            M_PositionSummary.CurrencyID = Convert.ToString(dr["CurrencyID"]);
            M_PositionSummary.UnitAmount = Convert.ToDecimal(dr["UnitAmount"]);
            M_PositionSummary.NAV = Convert.ToDecimal(dr["NAV"]);
            M_PositionSummary.Balance = Convert.ToDecimal(dr["Balance"]);
            M_PositionSummary.BalanceUSD = Convert.ToDecimal(dr["BalanceUSD"]);
            return M_PositionSummary;
        }


        public List<FundClientCombo> FundClientSIDDetail_ComboRpt()
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<FundClientCombo> L_FundClient = new List<FundClientCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "SELECT  FundClientPK, SID + ' - ' + case when InvestorType = 1 then isnull(CONVERT(varchar(10), TanggalLahir, 20),'') + ',' + Name else isnull(CONVERT(varchar(10), TanggalBerdiri, 20),'') + ',' + Name end as SID, Name FROM [FundClient]  where status = 2 union all select 0,'All', '' order by FundClientPK,Name";
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    FundClientCombo M_FundClient = new FundClientCombo();
                                    M_FundClient.FundClientPK = Convert.ToInt32(dr["FundClientPK"]);
                                    M_FundClient.ID = Convert.ToString(dr["SID"]);
                                    M_FundClient.Name = Convert.ToString(dr["Name"]);
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

        public List<FundClientCombo> FundClientSID_ComboRpt()
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<FundClientCombo> L_FundClient = new List<FundClientCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "SELECT  distinct SID FROM [FundClient]  where status = 2";
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    FundClientCombo M_FundClient = new FundClientCombo();
                                    M_FundClient.SID = Convert.ToString(dr["SID"]);
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

        public List<FundClientCombo> FundClientInternalCategory_ComboRpt()
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<FundClientCombo> L_FundClient = new List<FundClientCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "SELECT  distinct ID InternalCategory,InternalCategoryPK FROM InternalCategory  where status = 2";
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    FundClientCombo M_FundClient = new FundClientCombo();
                                    M_FundClient.InternalCategory = Convert.ToString(dr["InternalCategory"]);
                                    M_FundClient.InternalCategoryPK = Convert.ToInt32(dr["InternalCategoryPK"]);
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



        public List<FundClientCombo> FundClientSIDDetailnstitusi_ComboRpt()
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<FundClientCombo> L_FundClient = new List<FundClientCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "SELECT  FundClientPK, SID + ' - ' + case when InvestorType = 1 then isnull(CONVERT(varchar(10), TanggalLahir, 20),'') + ',' + Name else isnull(CONVERT(varchar(10), TanggalBerdiri, 20),'') + ',' + Name end as SID, Name FROM [FundClient]  where status = 2 and ClientCategory = 2 union all select 0,'All', '' order by FundClientPK,Name";
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    FundClientCombo M_FundClient = new FundClientCombo();
                                    M_FundClient.FundClientPK = Convert.ToInt32(dr["FundClientPK"]);
                                    M_FundClient.ID = Convert.ToString(dr["SID"]);
                                    M_FundClient.Name = Convert.ToString(dr["Name"]);
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

        public List<FundClientCombo> FundClientSIDInsti_ComboRpt()
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<FundClientCombo> L_FundClient = new List<FundClientCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "SELECT  distinct SID FROM [FundClient]  where status = 2 and ClientCategory = 2";
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    FundClientCombo M_FundClient = new FundClientCombo();
                                    M_FundClient.SID = Convert.ToString(dr["SID"]);
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

        public string Validate_CheckFundClientPending(DateTime _dateFrom, DateTime _dateTo, string _table, ParamUnitRegistryBySelected _paramUnitRegistryBySelected)
        {
            try
            {

                string paramSelected = "";
                if (!_host.findString(_paramUnitRegistryBySelected.UnitRegistrySelected.ToLower(), "0", ",") && !string.IsNullOrEmpty(_paramUnitRegistryBySelected.UnitRegistrySelected))
                {
                    paramSelected = " and " + _table + "PK in (" + _paramUnitRegistryBySelected.UnitRegistrySelected + ") ";
                }
                else
                {
                    paramSelected = " and " + _table + "PK in (0) ";
                }



                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = @"
                        Create Table #FundClientTemp
                        (PK nvarchar(50))
                        
                        Insert Into #FundClientTemp(PK)
                        select " + _table + @"PK from " + _table + @" A
                        left join FundClient B on A.FundClientPK = B.FundClientPK and B.status in (1,2) 
                        where ValueDate between @DateFrom and @DateTo and A.status = 1 " + paramSelected + @"
                        and B.status = 1

                        if exists(select " + _table + @"PK from " + _table + @" A
                        left join FundClient B on A.FundClientPK = B.FundClientPK and B.status in (1,2) 
                        where ValueDate between @DateFrom and @DateTo and A.status = 1 " + paramSelected + @"
                        and B.status = 1)
                        BEGIN
                        DECLARE @combinedString VARCHAR(MAX)
                        SELECT @combinedString = COALESCE(@combinedString + ', ', '') + PK
                        FROM #FundClientTemp
                        SELECT 'Approve Cancel, Please Check Fund Client in SysNo : ' + @combinedString as Result 
                        END
                        ELSE
                        BEGIN
                        select '' Result
                        END    ";

                        cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _dateTo);

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

        public string FundClient_ValidateAnswerRiskQuesionnaire(int _FundClientPK, int _RiskQuestionnairePK, int _RiskQuestionnaireAnswerPK)
        {

            try
            {
                string _msg = "";
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<FundClientCombo> L_FundClient = new List<FundClientCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"
                                if exists (select * from FundClientRiskQuestionnaire where FundClientPK = @FundClientPK and RiskQuestionnairePK = @RiskQuestionnairePK)
	                                select 'Update' Result
                                else
	                                select '' Result
                            ";

                        cmd.Parameters.AddWithValue("@FundClientPK", _FundClientPK);
                        cmd.Parameters.AddWithValue("@RiskQuestionnairePK", _RiskQuestionnairePK);
                        cmd.Parameters.AddWithValue("@RiskQuestionnaireAnswerPK", _RiskQuestionnaireAnswerPK);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                _msg = Convert.ToString(dr["Result"]);

                            }
                            return _msg;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }


        }

        public string FundClient_InsertAnswerRiskQuesionnaire(int _FundClientPK, int _RiskQuestionnairePK, int _RiskQuestionnaireAnswerPK)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    string _msg;
                    List<FundClientCombo> L_FundClient = new List<FundClientCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"
                                insert into FundClientRiskQuestionnaire
                                select @FundClientPK,@RiskQuestionnairePK,@RiskQuestionnaireAnswerPK
                                    ";

                        cmd.Parameters.AddWithValue("@FundClientPK", _FundClientPK);
                        cmd.Parameters.AddWithValue("@RiskQuestionnairePK", _RiskQuestionnairePK);
                        cmd.Parameters.AddWithValue("@RiskQuestionnaireAnswerPK", _RiskQuestionnaireAnswerPK);

                        cmd.ExecuteReader();
                        return "";
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }


        }


        public string FundClient_UpdateAnswerRiskQuesionnaire(int _FundClientPK, int _RiskQuestionnairePK, int _RiskQuestionnaireAnswerPK)
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
                                Update FundClientRiskQuestionnaire set RiskQuestionnaireAnswerPK = @RiskQuestionnaireAnswerPK where FundClientPK = @FundClientPK and RiskQuestionnairePK = @RiskQuestionnairePK

                                        ";

                        cmd.Parameters.AddWithValue("@FundClientPK", _FundClientPK);
                        cmd.Parameters.AddWithValue("@RiskQuestionnairePK", _RiskQuestionnairePK);
                        cmd.Parameters.AddWithValue("@RiskQuestionnaireAnswerPK", _RiskQuestionnaireAnswerPK);
                        cmd.ExecuteReader();
                        return "";
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }


        }

        public int FundClient_SelectTotalScoreRiskQuestionnaire(int _FundClientPK)
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
                            select isnull(sum(Score),0) Result from FundClientRiskQuestionnaire A
                            left join RiskQuestionnaireAnswer B on A.RiskQuestionnaireAnswerPK = B.RiskQuestionnaireAnswerPK and B.Status = 2
                            where FundClientPk = @FundClientPK
                            ";

                        cmd.Parameters.AddWithValue("@FundClientPK", _FundClientPK);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return Convert.ToInt32(dr["Result"]);

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


    }
}