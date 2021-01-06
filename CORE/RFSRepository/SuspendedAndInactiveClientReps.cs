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

namespace RFSRepository
{
    public class SuspendedAndInactiveClientReps
    {

        Host _host = new Host();

        //1
        string _insertCommand = "INSERT INTO [dbo].[FundClient] " +
        "([FundClientPK],[HistoryPK],[Status],[ID],[Name]," +
        "[ClientCategory],[InvestorType],[NatureOfBusinessInsti],[InternalCategoryPK],[SellingAgentPK],[SID],[IFUACode],[Child]," +
        "[ARIA],[Registered],[JumlahDanaAwal],[JumlahDanaSaatIniCash],[JumlahDanaSaatIni]," +
        "[Negara],[Nationality],[NPWP],[SACode],[Propinsi],[TeleponSelular]," +
        "[Email],[Fax],[DormantDate],[Description],[JumlahBank],[NamaBank1]," +
        "[NomorRekening1],[BICCode1],[NamaNasabah1],[MataUang1],[NamaBank2]," +
        "[NomorRekening2],[BICCode2],[NamaNasabah2],[MataUang2],[NamaBank3]," +
        "[NomorRekening3],[BICCode3],[NamaNasabah3],[MataUang3],[NamaDepanInd]," +
        "[NamaTengahInd],[NamaBelakangInd],[TempatLahir],[TanggalLahir],[JenisKelamin]," +
        "[StatusPerkawinan],[Pekerjaan],[Pendidikan],[Agama],[PenghasilanInd]," +
        "[SumberDanaInd],[MaksudTujuanInd],[AlamatInd1],[KodeKotaInd1],[KodePosInd1]," +
        "[AlamatInd2],[KodeKotaInd2],[KodePosInd2],[NamaPerusahaan],[Domisili]," +
        "[Tipe],[Karakteristik],[NoSKD],[PenghasilanInstitusi],[SumberDanaInstitusi]," +
        "[MaksudTujuanInstitusi],[AlamatPerusahaan],[KodeKotaIns],[KodePosIns],[SpouseName],[MotherMaidenName]," +
        "[AhliWaris],[HubunganAhliWaris],[NatureOfBusiness],[NatureOfBusinessLainnya],[Politis]," +
        "[PolitisLainnya],[TeleponRumah],[OtherAlamatInd1],[OtherKodeKotaInd1],[OtherKodePosInd1]," +
        "[OtherPropinsiInd1],[CountryOfBirth],[OtherNegaraInd1],[OtherAlamatInd2],[OtherKodeKotaInd2],[OtherKodePosInd2]," +
        "[OtherPropinsiInd2],[OtherNegaraInd2],[OtherAlamatInd3],[OtherKodeKotaInd3],[OtherKodePosInd3]," +
        "[OtherPropinsiInd3],[OtherNegaraInd3],[OtherTeleponRumah],[OtherTeleponSelular],[OtherEmail]," +
        "[OtherFax],[JumlahIdentitasInd],[IdentitasInd1],[NoIdentitasInd1],[RegistrationDateIdentitasInd1]," +
        "[ExpiredDateIdentitasInd1],[IdentitasInd2],[NoIdentitasInd2],[RegistrationDateIdentitasInd2],[ExpiredDateIdentitasInd2]," +
        "[IdentitasInd3],[NoIdentitasInd3],[RegistrationDateIdentitasInd3],[ExpiredDateIdentitasInd3],[IdentitasInd4]," +
        "[NoIdentitasInd4],[RegistrationDateIdentitasInd4],[ExpiredDateIdentitasInd4],[RegistrationNPWP]," +
        "[ExpiredDateSKD],[TanggalBerdiri],[LokasiBerdiri],[TeleponBisnis],[NomorAnggaran]," +
        "[NomorSIUP],[AssetFor1Year],[AssetFor2Year],[AssetFor3Year],[OperatingProfitFor1Year]," +
        "[OperatingProfitFor2Year],[OperatingProfitFor3Year],[JumlahPejabat],[NamaDepanIns1],[NamaTengahIns1]," +
        "[NamaBelakangIns1],[Jabatan1],[JumlahIdentitasIns1],[IdentitasIns11],[NoIdentitasIns11]," +
        "[RegistrationDateIdentitasIns11],[ExpiredDateIdentitasIns11],[IdentitasIns12],[NoIdentitasIns12],[RegistrationDateIdentitasIns12]," +
        "[ExpiredDateIdentitasIns12],[IdentitasIns13],[NoIdentitasIns13],[RegistrationDateIdentitasIns13],[ExpiredDateIdentitasIns13]," +
        "[IdentitasIns14],[NoIdentitasIns14],[RegistrationDateIdentitasIns14],[ExpiredDateIdentitasIns14],[NamaDepanIns2]," +
        "[NamaTengahIns2],[NamaBelakangIns2],[Jabatan2],[JumlahIdentitasIns2],[IdentitasIns21]," +
        "[NoIdentitasIns21],[RegistrationDateIdentitasIns21],[ExpiredDateIdentitasIns21],[IdentitasIns22],[NoIdentitasIns22]," +
        "[RegistrationDateIdentitasIns22],[ExpiredDateIdentitasIns22],[IdentitasIns23],[NoIdentitasIns23],[RegistrationDateIdentitasIns23]," +
        "[ExpiredDateIdentitasIns23],[IdentitasIns24],[NoIdentitasIns24],[RegistrationDateIdentitasIns24],[ExpiredDateIdentitasIns24]," +
        "[NamaDepanIns3],[NamaTengahIns3],[NamaBelakangIns3],[Jabatan3],[JumlahIdentitasIns3]," +
        "[IdentitasIns31],[NoIdentitasIns31],[RegistrationDateIdentitasIns31],[ExpiredDateIdentitasIns31],[IdentitasIns32]," +
        "[NoIdentitasIns32],[RegistrationDateIdentitasIns32],[ExpiredDateIdentitasIns32],[IdentitasIns33],[NoIdentitasIns33]," +
        "[RegistrationDateIdentitasIns33],[ExpiredDateIdentitasIns33],[IdentitasIns34],[NoIdentitasIns34],[RegistrationDateIdentitasIns34]," +
        "[ExpiredDateIdentitasIns34],[NamaDepanIns4],[NamaTengahIns4],[NamaBelakangIns4],[Jabatan4]," +
        "[JumlahIdentitasIns4],[IdentitasIns41],[NoIdentitasIns41],[RegistrationDateIdentitasIns41],[ExpiredDateIdentitasIns41]," +
        "[IdentitasIns42],[NoIdentitasIns42],[RegistrationDateIdentitasIns42],[ExpiredDateIdentitasIns42],[IdentitasIns43]," +
        "[NoIdentitasIns43],[RegistrationDateIdentitasIns43],[ExpiredDateIdentitasIns43],[IdentitasIns44],[NoIdentitasIns44]," +
        "[RegistrationDateIdentitasIns44],[ExpiredDateIdentitasIns44],[CompanyTypeOJK],[BusinessTypeOJK],[BIMemberCode1],[BIMemberCode2],[BIMemberCode3],[PhoneIns1],[EmailIns1], " +
        "[PhoneIns2],[EmailIns2],[InvestorsRiskProfile],[AssetOwner],[StatementType],[FATCA],[TIN],[TINIssuanceCountry],[GIIN],[SubstantialOwnerName]," +
        "[SubstantialOwnerAddress],[SubstantialOwnerTIN],[BankBranchName1],[BankBranchName2],[BankBranchName3],[BankCountry1],[BankCountry2],[BankCountry3],[CountryofCorrespondence],[CountryofDomicile]," +
        "[SIUPExpirationDate],[CountryofEstablishment],[CompanyCityName],[CountryofCompany],[NPWPPerson1],[NPWPPerson2],[BankRDNPK],[RDNAccountNo],[RDNAccountName],[RiskProfileScore],[KYCRiskProfile],";
        string _paramaterCommand = "@ID,@Name," +
        "@ClientCategory,@InvestorType,@NatureOfBusinessInsti,@InternalCategoryPK,@SellingAgentPK,@SID,@IFUACode,@Child," +
        "@ARIA,@Registered,@JumlahDanaAwal,@JumlahDanaSaatIniCash,@JumlahDanaSaatIni," +
        "@Negara,@Nationality,@NPWP,@SACode,@Propinsi,@TeleponSelular," +
        "@Email,@Fax,@DormantDate,@Description,@JumlahBank,@NamaBank1," +
        "@NomorRekening1,@BICCode1,@NamaNasabah1,@MataUang1,@NamaBank2," +
        "@NomorRekening2,@BICCode2,@NamaNasabah2,@MataUang2,@NamaBank3," +
        "@NomorRekening3,@BICCode3,@NamaNasabah3,@MataUang3,@NamaDepanInd," +
        "@NamaTengahInd,@NamaBelakangInd,@TempatLahir,@TanggalLahir,@JenisKelamin," +
        "@StatusPerkawinan,@Pekerjaan,@Pendidikan,@Agama,@PenghasilanInd," +
        "@SumberDanaInd,@MaksudTujuanInd,@AlamatInd1,@KodeKotaInd1,@KodePosInd1," +
        "@AlamatInd2,@KodeKotaInd2,@KodePosInd2,@NamaPerusahaan,@Domisili," +
        "@Tipe,@Karakteristik,@NoSKD,@PenghasilanInstitusi,@SumberDanaInstitusi," +
        "@MaksudTujuanInstitusi,@AlamatPerusahaan,@KodeKotaIns,@KodePosIns,@SpouseName,@MotherMaidenName," +
        "@AhliWaris,@HubunganAhliWaris,@NatureOfBusiness,@NatureOfBusinessLainnya,@Politis," +
        "@PolitisLainnya,@TeleponRumah,@OtherAlamatInd1,@OtherKodeKotaInd1,@OtherKodePosInd1," +
        "@OtherPropinsiInd1,@CountryOfBirth,@OtherNegaraInd1,@OtherAlamatInd2,@OtherKodeKotaInd2,@OtherKodePosInd2," +
        "@OtherPropinsiInd2,@OtherNegaraInd2,@OtherAlamatInd3,@OtherKodeKotaInd3,@OtherKodePosInd3," +
        "@OtherPropinsiInd3,@OtherNegaraInd3,@OtherTeleponRumah,@OtherTeleponSelular,@OtherEmail," +
        "@OtherFax,@JumlahIdentitasInd,@IdentitasInd1,@NoIdentitasInd1,@RegistrationDateIdentitasInd1," +
        "@ExpiredDateIdentitasInd1,@IdentitasInd2,@NoIdentitasInd2,@RegistrationDateIdentitasInd2,@ExpiredDateIdentitasInd2," +
        "@IdentitasInd3,@NoIdentitasInd3,@RegistrationDateIdentitasInd3,@ExpiredDateIdentitasInd3,@IdentitasInd4," +
        "@NoIdentitasInd4,@RegistrationDateIdentitasInd4,@ExpiredDateIdentitasInd4,@RegistrationNPWP," +
        "@ExpiredDateSKD,@TanggalBerdiri,@LokasiBerdiri,@TeleponBisnis,@NomorAnggaran," +
        "@NomorSIUP,@AssetFor1Year,@AssetFor2Year,@AssetFor3Year,@OperatingProfitFor1Year," +
        "@OperatingProfitFor2Year,@OperatingProfitFor3Year,@JumlahPejabat,@NamaDepanIns1,@NamaTengahIns1," +
        "@NamaBelakangIns1,@Jabatan1,@JumlahIdentitasIns1,@IdentitasIns11,@NoIdentitasIns11," +
        "@RegistrationDateIdentitasIns11,@ExpiredDateIdentitasIns11,@IdentitasIns12,@NoIdentitasIns12,@RegistrationDateIdentitasIns12," +
        "@ExpiredDateIdentitasIns12,@IdentitasIns13,@NoIdentitasIns13,@RegistrationDateIdentitasIns13,@ExpiredDateIdentitasIns13," +
        "@IdentitasIns14,@NoIdentitasIns14,@RegistrationDateIdentitasIns14,@ExpiredDateIdentitasIns14,@NamaDepanIns2," +
        "@NamaTengahIns2,@NamaBelakangIns2,@Jabatan2,@JumlahIdentitasIns2,@IdentitasIns21," +
        "@NoIdentitasIns21,@RegistrationDateIdentitasIns21,@ExpiredDateIdentitasIns21,@IdentitasIns22,@NoIdentitasIns22," +
        "@RegistrationDateIdentitasIns22,@ExpiredDateIdentitasIns22,@IdentitasIns23,@NoIdentitasIns23,@RegistrationDateIdentitasIns23," +
        "@ExpiredDateIdentitasIns23,@IdentitasIns24,@NoIdentitasIns24,@RegistrationDateIdentitasIns24,@ExpiredDateIdentitasIns24," +
        "@NamaDepanIns3,@NamaTengahIns3,@NamaBelakangIns3,@Jabatan3,@JumlahIdentitasIns3," +
        "@IdentitasIns31,@NoIdentitasIns31,@RegistrationDateIdentitasIns31,@ExpiredDateIdentitasIns31,@IdentitasIns32," +
        "@NoIdentitasIns32,@RegistrationDateIdentitasIns32,@ExpiredDateIdentitasIns32,@IdentitasIns33,@NoIdentitasIns33," +
        "@RegistrationDateIdentitasIns33,@ExpiredDateIdentitasIns33,@IdentitasIns34,@NoIdentitasIns34,@RegistrationDateIdentitasIns34," +
        "@ExpiredDateIdentitasIns34,@NamaDepanIns4,@NamaTengahIns4,@NamaBelakangIns4,@Jabatan4," +
        "@JumlahIdentitasIns4,@IdentitasIns41,@NoIdentitasIns41,@RegistrationDateIdentitasIns41,@ExpiredDateIdentitasIns41," +
        "@IdentitasIns42,@NoIdentitasIns42,@RegistrationDateIdentitasIns42,@ExpiredDateIdentitasIns42,@IdentitasIns43," +
        "@NoIdentitasIns43,@RegistrationDateIdentitasIns43,@ExpiredDateIdentitasIns43,@IdentitasIns44,@NoIdentitasIns44," +
        "@RegistrationDateIdentitasIns44,@ExpiredDateIdentitasIns44,@CompanyTypeOJK,@BusinessTypeOJK,@BIMemberCode1,@BIMemberCode2,@BIMemberCode3,@PhoneIns1,@EmailIns1, " +
        "@PhoneIns2,@EmailIns2,@InvestorsRiskProfile,@AssetOwner,@StatementType,@FATCA,@TIN,@TINIssuanceCountry,@GIIN,@SubstantialOwnerName," +
        "@SubstantialOwnerAddress,@SubstantialOwnerTIN,@BankBranchName1,@BankBranchName2,@BankBranchName3,@BankCountry1,@BankCountry2,@BankCountry3,@CountryofCorrespondence,@CountryofDomicile," +
        "@SIUPExpirationDate,@CountryofEstablishment,@CompanyCityName,@CountryofCompany,@NPWPPerson1,@NPWPPerson2,@BankRDNPK,@RDNAccountNo,@RDNAccountName,@RiskProfileScore,@KYCRiskProfile,";


        //2
        private FundClient setFundClient(SqlDataReader dr)
        {
            FundClient M_FundClient = new FundClient();
            M_FundClient.FundClientPK = Convert.ToInt32(dr["FundClientPK"]);
            M_FundClient.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_FundClient.Status = Convert.ToInt32(dr["Status"]);
            M_FundClient.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_FundClient.Notes = dr["Notes"].ToString();
            M_FundClient.ID = dr["ID"].ToString();
            M_FundClient.OldID = dr["OldID"].ToString();
            M_FundClient.Name = dr["Name"].ToString();
            M_FundClient.ClientCategory = dr["ClientCategory"].ToString();
            M_FundClient.ClientCategoryDesc = dr["ClientCategoryDesc"].ToString();
            M_FundClient.InvestorType = dr["InvestorType"].ToString();
            M_FundClient.InvestorTypeDesc = dr["InvestorTypeDesc"].ToString();
            M_FundClient.NatureOfBusinessInsti = Convert.ToInt32(dr["NatureOfBusinessInsti"]);
            M_FundClient.NatureOfBusinessInstiDesc = dr["NatureOfBusinessInstiDesc"].ToString();
            M_FundClient.InternalCategoryPK = Convert.ToInt32(dr["InternalCategoryPK"]);
            M_FundClient.RiskProfileScore = Convert.ToInt32(dr["RiskProfileScore"]);
            M_FundClient.RiskProfileScoreDesc = dr["RiskProfileScoreDesc"].ToString();
            M_FundClient.InternalCategoryID = Convert.ToString(dr["InternalCategoryID"]);
            M_FundClient.SellingAgentPK = Convert.ToInt32(dr["SellingAgentPK"]);
            M_FundClient.SellingAgentID = Convert.ToString(dr["SellingAgentID"]);
            M_FundClient.SID = dr["SID"].ToString();
            M_FundClient.IFUACode = dr["IFUACode"].ToString();
            M_FundClient.Child = Convert.ToBoolean(dr["Child"]);
            M_FundClient.ARIA = Convert.ToBoolean(dr["ARIA"]);
            M_FundClient.Registered = Convert.ToBoolean(dr["Registered"]);
            M_FundClient.JumlahDanaAwal = Convert.ToInt32(dr["JumlahDanaAwal"]);
            M_FundClient.JumlahDanaSaatIniCash = Convert.ToInt32(dr["JumlahDanaSaatIniCash"]);
            M_FundClient.JumlahDanaSaatIni = Convert.ToInt32(dr["JumlahDanaSaatIni"]);
            M_FundClient.Negara = dr["Negara"].ToString();
            M_FundClient.NegaraDesc = Convert.ToString(dr["NegaraDesc"]);
            M_FundClient.Nationality = dr["Nationality"].ToString();
            M_FundClient.NationalityDesc = Convert.ToString(dr["NationalityDesc"]);
            M_FundClient.NPWP = dr["NPWP"].ToString();
            M_FundClient.SACode = dr["SACode"].ToString();
            M_FundClient.Propinsi = Convert.ToInt32(dr["Propinsi"]);
            M_FundClient.PropinsiDesc = Convert.ToString(dr["PropinsiDesc"]);
            M_FundClient.TeleponSelular = dr["TeleponSelular"].ToString();
            M_FundClient.Email = dr["Email"].ToString();
            M_FundClient.Fax = dr["Fax"].ToString();
            M_FundClient.DormantDate = dr["DormantDate"].ToString();
            M_FundClient.Description = dr["Description"].ToString();
            M_FundClient.JumlahBank = Convert.ToInt32(dr["JumlahBank"]);
            M_FundClient.NamaBank1 = Convert.ToInt32(dr["NamaBank1"]);
            M_FundClient.NomorRekening1 = dr["NomorRekening1"].ToString();
            M_FundClient.BICCode1 = Convert.ToString(dr["BICCode1"]);
            M_FundClient.BICCode1Name = Convert.ToString(dr["BICCode1Name"]);
            M_FundClient.NamaNasabah1 = dr["NamaNasabah1"].ToString();
            M_FundClient.MataUang1 = dr["MataUang1"].ToString();
            M_FundClient.NamaBank2 = Convert.ToInt32(dr["NamaBank2"]);
            M_FundClient.NomorRekening2 = dr["NomorRekening2"].ToString();
            M_FundClient.BICCode2 = Convert.ToString(dr["BICCode2"]);
            M_FundClient.BICCode2Name = Convert.ToString(dr["BICCode2Name"]);
            M_FundClient.NamaNasabah2 = dr["NamaNasabah2"].ToString();
            M_FundClient.MataUang2 = dr["MataUang2"].ToString();
            M_FundClient.NamaBank3 = Convert.ToInt32(dr["NamaBank3"]);
            M_FundClient.NomorRekening3 = dr["NomorRekening3"].ToString();
            M_FundClient.BICCode3 = Convert.ToString(dr["BICCode3"]);
            M_FundClient.BICCode3Name = Convert.ToString(dr["BICCode3Name"]);
            M_FundClient.NamaNasabah3 = dr["NamaNasabah3"].ToString();
            M_FundClient.MataUang3 = dr["MataUang3"].ToString();
            //Individual
            M_FundClient.NamaDepanInd = dr["NamaDepanInd"].ToString();
            M_FundClient.NamaTengahInd = dr["NamaTengahInd"].ToString();
            M_FundClient.NamaBelakangInd = dr["NamaBelakangInd"].ToString();
            M_FundClient.TempatLahir = dr["TempatLahir"].ToString();
            M_FundClient.TanggalLahir = dr["TanggalLahir"].ToString();
            M_FundClient.JenisKelamin = Convert.ToInt32(dr["JenisKelamin"]);
            M_FundClient.JenisKelaminDesc = Convert.ToString(dr["JenisKelaminDesc"]);
            M_FundClient.StatusPerkawinan = Convert.ToInt32(dr["StatusPerkawinan"]);
            M_FundClient.StatusPerkawinanDesc = Convert.ToString(dr["StatusPerkawinanDesc"]);
            M_FundClient.Pekerjaan = Convert.ToInt32(dr["Pekerjaan"]);
            M_FundClient.PekerjaanDesc = Convert.ToString(dr["PekerjaanDesc"]);
            M_FundClient.Pendidikan = Convert.ToInt32(dr["Pendidikan"]);
            M_FundClient.PendidikanDesc = Convert.ToString(dr["PendidikanDesc"]);
            M_FundClient.Agama = Convert.ToInt32(dr["Agama"]);
            M_FundClient.AgamaDesc = Convert.ToString(dr["AgamaDesc"]);
            M_FundClient.PenghasilanInd = Convert.ToInt32(dr["PenghasilanInd"]);
            M_FundClient.PenghasilanIndDesc = Convert.ToString(dr["PenghasilanIndDesc"]);
            M_FundClient.SumberDanaInd = Convert.ToInt32(dr["SumberDanaInd"]);
            M_FundClient.SumberDanaIndDesc = Convert.ToString(dr["SumberDanaIndDesc"]);
            M_FundClient.MaksudTujuanInd = Convert.ToInt32(dr["MaksudTujuanInd"]);
            M_FundClient.MaksudTujuanIndDesc = Convert.ToString(dr["MaksudTujuanIndDesc"]);
            M_FundClient.AlamatInd1 = dr["AlamatInd1"].ToString();
            M_FundClient.KodeKotaInd1 = dr["KodeKotaInd1"].ToString();
            M_FundClient.KodeKotaInd1Desc = dr["KodeKotaInd1Desc"].ToString();
            M_FundClient.KodePosInd1 = Convert.ToInt32(dr["KodePosInd1"]);
            M_FundClient.AlamatInd2 = dr["AlamatInd2"].ToString();
            M_FundClient.KodeKotaInd2 = dr["KodeKotaInd2"].ToString();
            M_FundClient.KodeKotaInd2Desc = dr["KodeKotaInd2Desc"].ToString();
            M_FundClient.KodePosInd2 = Convert.ToInt32(dr["KodePosInd2"]);
            M_FundClient.CountryOfBirth = dr["CountryOfBirth"].ToString();
            M_FundClient.CountryOfBirthDesc = Convert.ToString(dr["CountryOfBirthDesc"]);

            //Institution
            M_FundClient.NamaPerusahaan = dr["NamaPerusahaan"].ToString();
            M_FundClient.Domisili = Convert.ToInt32(dr["Domisili"]);
            M_FundClient.DomisiliDesc = Convert.ToString(dr["DomisiliDesc"]);
            M_FundClient.Tipe = Convert.ToInt32(dr["Tipe"]);
            M_FundClient.TipeDesc = Convert.ToString(dr["TipeDesc"]);
            M_FundClient.Karakteristik = Convert.ToInt32(dr["Karakteristik"]);
            M_FundClient.KarakteristikDesc = Convert.ToString(dr["KarakteristikDesc"]);
            M_FundClient.NoSKD = dr["NoSKD"].ToString();
            M_FundClient.PenghasilanInstitusi = Convert.ToInt32(dr["PenghasilanInstitusi"]);
            M_FundClient.PenghasilanInstitusiDesc = Convert.ToString(dr["PenghasilanInstitusiDesc"]);
            M_FundClient.SumberDanaInstitusi = Convert.ToInt32(dr["SumberDanaInstitusi"]);
            M_FundClient.SumberDanaInstitusiDesc = Convert.ToString(dr["SumberDanaInstitusiDesc"]);
            M_FundClient.MaksudTujuanInstitusi = Convert.ToInt32(dr["MaksudTujuanInstitusi"]);
            M_FundClient.MaksudTujuanInstitusiDesc = Convert.ToString(dr["MaksudTujuanInstitusiDesc"]);
            M_FundClient.AlamatPerusahaan = dr["AlamatPerusahaan"].ToString();
            M_FundClient.KodeKotaIns = dr["KodeKotaIns"].ToString();
            M_FundClient.KodeKotaInsDesc = dr["KodeKotaInsDesc"].ToString();
            M_FundClient.KodePosIns = Convert.ToInt32(dr["KodePosIns"]);
            M_FundClient.SpouseName = dr["SpouseName"].ToString();
            M_FundClient.MotherMaidenName = dr["MotherMaidenName"].ToString();
            M_FundClient.AhliWaris = dr["AhliWaris"].ToString();
            M_FundClient.HubunganAhliWaris = dr["HubunganAhliWaris"].ToString();
            M_FundClient.NatureOfBusiness = Convert.ToInt32(dr["NatureOfBusiness"]);
            M_FundClient.NatureOfBusinessDesc = dr["NatureOfBusinessDesc"].ToString();
            M_FundClient.NatureOfBusinessLainnya = dr["NatureOfBusinessLainnya"].ToString();
            M_FundClient.Politis = Convert.ToInt32(dr["Politis"]);
            M_FundClient.PolitisLainnya = dr["PolitisLainnya"].ToString();
            M_FundClient.TeleponRumah = dr["TeleponRumah"].ToString();
            M_FundClient.OtherAlamatInd1 = dr["OtherAlamatInd1"].ToString();
            M_FundClient.OtherKodeKotaInd1 = dr["OtherKodeKotaInd1"].ToString();
            M_FundClient.OtherKodeKotaInd1Desc = dr["OtherKodeKotaInd1Desc"].ToString();
            M_FundClient.OtherKodePosInd1 = Convert.ToInt32(dr["OtherKodePosInd1"]);
            M_FundClient.OtherPropinsiInd1 = Convert.ToInt32(dr["OtherPropinsiInd1"]);
            M_FundClient.OtherPropinsiInd1Desc = Convert.ToString(dr["OtherPropinsiInd1Desc"]);
            M_FundClient.OtherNegaraInd1 = Convert.ToString(dr["OtherNegaraInd1"]);
            M_FundClient.OtherNegaraInd1Desc = Convert.ToString(dr["OtherNegaraInd1Desc"]);
            M_FundClient.OtherAlamatInd2 = dr["OtherAlamatInd2"].ToString();
            M_FundClient.OtherKodeKotaInd2 = dr["OtherKodeKotaInd2"].ToString();
            M_FundClient.OtherKodeKotaInd2Desc = dr["OtherKodeKotaInd2Desc"].ToString();
            M_FundClient.OtherKodePosInd2 = Convert.ToInt32(dr["OtherKodePosInd2"]);
            M_FundClient.OtherPropinsiInd2 = Convert.ToInt32(dr["OtherPropinsiInd2"]);
            M_FundClient.OtherPropinsiInd2Desc = Convert.ToString(dr["OtherPropinsiInd2Desc"]);
            M_FundClient.OtherNegaraInd2 = Convert.ToString(dr["OtherNegaraInd2"]);
            M_FundClient.OtherNegaraInd2Desc = Convert.ToString(dr["OtherNegaraInd2Desc"]);
            M_FundClient.OtherAlamatInd3 = dr["OtherAlamatInd3"].ToString();
            M_FundClient.OtherKodeKotaInd3 = dr["OtherKodeKotaInd3"].ToString();
            M_FundClient.OtherKodeKotaInd3Desc = dr["OtherKodeKotaInd3Desc"].ToString();
            M_FundClient.OtherKodePosInd3 = Convert.ToInt32(dr["OtherKodePosInd3"]);
            M_FundClient.OtherPropinsiInd3 = Convert.ToInt32(dr["OtherPropinsiInd3"]);
            M_FundClient.OtherPropinsiInd3Desc = Convert.ToString(dr["OtherPropinsiInd3Desc"]);
            M_FundClient.OtherNegaraInd3 = Convert.ToString(dr["OtherNegaraInd3"]);
            M_FundClient.OtherNegaraInd3Desc = Convert.ToString(dr["OtherNegaraInd3Desc"]);
            M_FundClient.OtherTeleponRumah = dr["OtherTeleponRumah"].ToString();
            M_FundClient.OtherTeleponSelular = dr["OtherTeleponSelular"].ToString();
            M_FundClient.OtherEmail = dr["OtherEmail"].ToString();
            M_FundClient.OtherFax = dr["OtherFax"].ToString();
            M_FundClient.JumlahIdentitasInd = Convert.ToInt32(dr["JumlahIdentitasInd"]);
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
            M_FundClient.JumlahPejabat = Convert.ToInt32(dr["JumlahPejabat"]);
            M_FundClient.NamaDepanIns1 = dr["NamaDepanIns1"].ToString();
            M_FundClient.NamaTengahIns1 = dr["NamaTengahIns1"].ToString();
            M_FundClient.NamaBelakangIns1 = dr["NamaBelakangIns1"].ToString();
            M_FundClient.Jabatan1 = dr["Jabatan1"].ToString();
            M_FundClient.JumlahIdentitasIns1 = Convert.ToInt32(dr["JumlahIdentitasIns1"]);
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
            M_FundClient.JumlahIdentitasIns2 = Convert.ToInt32(dr["JumlahIdentitasIns2"]);
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
            M_FundClient.CompanyTypeOJK = Convert.ToInt32(dr["CompanyTypeOJK"]);
            M_FundClient.CompanyTypeOJKDesc = dr["CompanyTypeOJKDesc"].ToString();
            M_FundClient.BusinessTypeOJK = Convert.ToInt32(dr["BusinessTypeOJK"]);
            M_FundClient.BusinessTypeOJKDesc = dr["BusinessTypeOJKDesc"].ToString();

            // S-INVEST
            M_FundClient.BIMemberCode1 = dr["BIMemberCode1"].ToString();
            M_FundClient.BIMemberCode2 = dr["BIMemberCode2"].ToString();
            M_FundClient.BIMemberCode3 = dr["BIMemberCode3"].ToString();
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
            M_FundClient.TINIssuanceCountry = Convert.ToString(dr["TINIssuanceCountry"]);
            M_FundClient.TINIssuanceCountryDesc = dr["TINIssuanceCountryDesc"].ToString();
            M_FundClient.GIIN = dr["GIIN"].ToString();
            M_FundClient.SubstantialOwnerName = dr["SubstantialOwnerName"].ToString();
            M_FundClient.SubstantialOwnerAddress = dr["SubstantialOwnerAddress"].ToString();
            M_FundClient.SubstantialOwnerTIN = dr["SubstantialOwnerTIN"].ToString();
            M_FundClient.BankBranchName1 = dr["BankBranchName1"].ToString();
            M_FundClient.BankBranchName2 = dr["BankBranchName2"].ToString();
            M_FundClient.BankBranchName3 = dr["BankBranchName3"].ToString();
            M_FundClient.BankCountry1 = dr["BankCountry1"].ToString();
            M_FundClient.BankCountry1Desc = dr["BankCountry1Desc"].ToString();
            M_FundClient.BankCountry2 = dr["BankCountry2"].ToString();
            M_FundClient.BankCountry2Desc = dr["BankCountry2Desc"].ToString();
            M_FundClient.BankCountry3 = dr["BankCountry3"].ToString();
            M_FundClient.BankCountry3Desc = dr["BankCountry3Desc"].ToString();

            // new add on
            M_FundClient.CountryofCorrespondence = dr["CountryofCorrespondence"].ToString();
            M_FundClient.CountryofCorrespondenceDesc = Convert.ToString(dr["CountryofCorrespondenceDesc"]);
            M_FundClient.CountryofDomicile = dr["CountryofDomicile"].ToString();
            M_FundClient.CountryofDomicileDesc = Convert.ToString(dr["CountryofDomicileDesc"]);
            M_FundClient.SIUPExpirationDate = dr["SIUPExpirationDate"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["SIUPExpirationDate"]);
            M_FundClient.CountryofEstablishment = dr["CountryofEstablishment"].ToString();
            M_FundClient.CountryofEstablishmentDesc = dr["CountryofEstablishmentDesc"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["CountryofEstablishmentDesc"]);
            M_FundClient.CompanyCityName = dr["CompanyCityName"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["CompanyCityName"]);
            M_FundClient.CompanyCityNameDesc = dr["CompanyCityNameDesc"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["CompanyCityNameDesc"]);
            M_FundClient.CountryofCompany = dr["CountryofCompany"].ToString();
            M_FundClient.CountryofCompanyDesc = dr["CountryofCompanyDesc"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["CountryofCompanyDesc"]);
            M_FundClient.NPWPPerson1 = dr["NPWPPerson1"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["NPWPPerson1"]);
            M_FundClient.NPWPPerson2 = dr["NPWPPerson2"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["NPWPPerson2"]);
            M_FundClient.BitIsSuspend = Convert.ToBoolean(dr["BitIsSuspend"]);
            // RDN
            M_FundClient.BankRDNPK = Convert.ToInt32(dr["BankRDNPK"]);
            M_FundClient.RDNAccountNo = Convert.ToString(dr["RDNAccountNo"]);
            M_FundClient.RDNAccountName = Convert.ToString(dr["RDNAccountName"]);

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
            M_FundClient.LastUpdateDB = dr["LastUpdateDB"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["LastUpdateDB"]);
            M_FundClient.SuspendBy = dr["SuspendBy"].ToString();
            M_FundClient.SuspendTime = dr["SuspendTime"].ToString();
            M_FundClient.UnSuspendBy = dr["UnSuspendBy"].ToString();
            M_FundClient.UnSuspendTime = dr["UnSuspendTIme"].ToString();
            M_FundClient.BitIsAfiliated = dr["BitIsAfiliated"].Equals(DBNull.Value) == true ? false : Convert.ToBoolean(dr["BitIsAfiliated"]);
            M_FundClient.AfiliatedFrom = dr["AfiliatedFromPK"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["AfiliatedFromPK"]);
            M_FundClient.KYCRiskProfile = Convert.ToInt32(dr["KYCRiskProfile"]);
            M_FundClient.KYCRiskProfileDesc = dr["KYCRiskProfileDesc"].ToString();
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
                            cmd.CommandText = @"select case when FC.status=1 then 'PENDING' else Case When FC.status = 2 then 'APPROVED' else Case when FC.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,DormantDate,A.ID SellingAgentID,mv1.DescOne KodeKotaInd1Desc,mv2.DescOne KodeKotaInd2Desc,mv3.DescOne KodeKotaInsDesc,  
                                mv4.DescOne OtherKodeKotaInd1Desc,mv5.DescOne OtherKodeKotaInd2Desc,mv6.DescOne OtherKodeKotaInd3Desc,  
                                mv7.DescOne ClientCategoryDesc,mv8.DescOne InvestorTypeDesc,mv9.DescOne JenisKelaminDesc,  
                                mv10.DescOne StatusPerkawinanDesc,mv11.DescOne PekerjaanDesc,mv12.DescOne PendidikanDesc,  
                                mv13.DescOne AgamaDesc,mv14.DescOne PenghasilanIndDesc,mv15.DescOne SumberDanaIndDesc,  
                                mv16.DescOne MaksudTujuanIndDesc,mv17.DescOne DomisiliDesc,mv18.DescOne TipeDesc,  
                                mv19.DescOne KarakteristikDesc,mv20.DescOne PenghasilanInstitusiDesc, 
                                mv21.DescOne SumberDanaInstitusiDesc,mv22.DescOne MaksudTujuanInstitusiDesc, mv41.DescOne CountryOfBirthDesc,  

                                mv42.DescOne CountryofCorrespondenceDesc, mv43.DescOne CountryofDomicileDesc,  
                                mv44.DescOne CountryofEstablishmentDesc, mv45.DescOne CountryofCompanyDesc, mv46.DescOne CompanyCityNameDesc,  

                                mv24.DescOne NegaraDesc,mv25.DescOne NationalityDesc,mv26.DescOne PropinsiDesc,mv27.DescOne OtherPropinsiInd1Desc,mv28.DescOne OtherPropinsiInd2Desc,mv29.DescOne OtherPropinsiInd3Desc,  
                                mv30.DescOne OtherNegaraInd1Desc,mv31.DescOne OtherNegaraInd2Desc,mv32.DescOne OtherNegaraInd3Desc,isnull(IC.Name,'') InternalCategoryID,  
                                mv33.DescOne InvestorsRiskProfileDesc,mv34.DescOne AssetOwnerDesc,mv35.DescOne StatementTypeDesc,mv36.DescOne fatcaDesc,mv37.DescOne TINIssuanceCountryDesc,mv38.DescOne BankCountry1Desc,mv39.DescOne BankCountry2Desc,mv40.DescOne BankCountry3Desc, mv48.DescOne KYCRiskProfileDesc, 
                                mv49.DescOne CompanyTypeOJKDesc, mv50.DescOne BusinessTypeOJKDesc,mv51.DescOne NatureofBusinessDesc,  
                                BC1.BICode BICCode1Name,BC2.BICode BICCode2Name,BC3.BICode BICCode3Name, 
                                * from FundClient fc   
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
                                left join MasterValue mv24 on fc.Negara = mv24.code and  mv24.ID = 'KSEICountry' and mv24.status = 2  
                                left join MasterValue mv25 on fc.Nationality = mv25.code and  mv25.ID = 'Nationality' and mv25.status = 2  
                                left join MasterValue mv26 on fc.Propinsi = mv26.code and  mv26.ID = 'Province' and mv26.status = 2  
                                left join MasterValue mv27 on fc.OtherPropinsiInd1 = mv27.code and  mv27.ID = 'Province' and mv27.status = 2  
                                left join MasterValue mv28 on fc.OtherPropinsiInd2 = mv28.code and  mv28.ID = 'Province' and mv28.status = 2  
                                left join MasterValue mv29 on fc.OtherPropinsiInd3 = mv29.code and  mv29.ID = 'Province' and mv29.status = 2  
                                left join MasterValue mv30 on fc.OtherNegaraInd1 = mv30.code and  mv30.ID = 'KSEICountry' and mv30.status = 2  
                                left join MasterValue mv31 on fc.OtherNegaraInd2 = mv31.code and  mv31.ID = 'KSEICountry' and mv31.status = 2  
                                left join MasterValue mv32 on fc.OtherNegaraInd3 = mv32.code and  mv32.ID = 'KSEICountry' and mv32.status = 2  
                                left join InternalCategory IC on fc.InternalCategoryPK = IC.InternalCategoryPK and IC.status = 2  
                                left join MasterValue mv33 on fc.InvestorsRiskProfile = mv33.code and  mv33.ID = 'InvestorsRiskProfile' and mv33.status = 2  
                                left join MasterValue mv34 on fc.AssetOwner = mv34.code and  mv34.ID = 'AssetOwner' and mv34.status = 2  
                                left join MasterValue mv35 on fc.StatementType = mv35.code and  mv35.ID = 'StatementType' and mv35.status = 2  
                                left join MasterValue mv36 on fc.fatca = mv36.code and  mv36.ID = 'FATCA' and mv36.status = 2  
                                left join MasterValue mv37 on fc.TINIssuanceCountry = mv37.code and  mv37.ID = 'KSEICountry' and mv37.status = 2  
                                left join MasterValue mv38 on fc.BankCountry1 = mv38.code and  mv38.ID = 'KSEICountry' and mv38.status = 2  
                                left join MasterValue mv39 on fc.BankCountry2 = mv39.code and  mv39.ID = 'KSEICountry' and mv39.status = 2  
                                left join MasterValue mv40 on fc.BankCountry3 = mv40.code and  mv40.ID = 'KSEICountry' and mv40.status = 2  
                                left join MasterValue mv41 on fc.CountryOfBirth = mv41.code and  mv41.ID = 'KSEICountry' and mv41.status = 2  
                                left join MasterValue mv42 on fc.CountryofCorrespondence = mv42.code and  mv42.ID = 'KSEICountry' and mv42.status = 2  
                                left join MasterValue mv43 on fc.CountryofDomicile = mv43.code and  mv43.ID = 'KSEICountry' and mv43.status = 2  
                                left join MasterValue mv44 on fc.CountryofEstablishment = mv44.code and  mv44.ID = 'KSEICountry' and mv44.status = 2  
                                left join MasterValue mv45 on fc.CountryofCompany = mv45.code and  mv45.ID = 'KSEICountry' and mv45.status = 2  
                                left join MasterValue mv46 on fc.CompanyCityName = mv46.Code and mv46.ID = 'CityRHB' and mv46.status = 2  
                                left join MasterValue mv48 on fc.KYCRiskProfile = mv48.Code and mv48.ID = 'KYCRiskProfile' and mv48.status = 2  
                                left join MasterValue mv49 on fc.CompanyTypeOJK = mv49.Code and mv49.ID = 'CompanyTypeOJK' and mv49.status = 2  
                                left join MasterValue mv50 on fc.BusinessTypeOJK = mv50.Code and mv50.ID = 'BusinessTypeOJK' and mv50.status = 2  
                                left join MasterValue mv51 on fc.NatureofBusiness = mv51.Code and mv48.ID = 'NatureofBusiness' and mv51.status = 2  
                                left join Bank BC1 on fc.NamaBank1 = BC1.BankPK and BC1.status = 2   
                                left join Bank BC2 on fc.NamaBank2 = BC2.BankPK and BC2.status = 2   
                                left join Bank BC3 on fc.NamaBank3 = BC3.BankPK and BC3.status = 2   
                                where  FC.status = @status and FC.DormantDate > '01/01/1900' and FC.BitIsSuspend = 1

                                union all

                                select case when FC.status=1 then 'PENDING' else Case When FC.status = 2 then 'APPROVED' else Case when FC.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,DormantDate,A.ID SellingAgentID,mv1.DescOne KodeKotaInd1Desc,mv2.DescOne KodeKotaInd2Desc,mv3.DescOne KodeKotaInsDesc,  
                                mv4.DescOne OtherKodeKotaInd1Desc,mv5.DescOne OtherKodeKotaInd2Desc,mv6.DescOne OtherKodeKotaInd3Desc,  
                                mv7.DescOne ClientCategoryDesc,mv8.DescOne InvestorTypeDesc,mv9.DescOne JenisKelaminDesc,  
                                mv10.DescOne StatusPerkawinanDesc,mv11.DescOne PekerjaanDesc,mv12.DescOne PendidikanDesc,  
                                mv13.DescOne AgamaDesc,mv14.DescOne PenghasilanIndDesc,mv15.DescOne SumberDanaIndDesc,  
                                mv16.DescOne MaksudTujuanIndDesc,mv17.DescOne DomisiliDesc,mv18.DescOne TipeDesc,  
                                mv19.DescOne KarakteristikDesc,mv20.DescOne PenghasilanInstitusiDesc, 
                                mv21.DescOne SumberDanaInstitusiDesc,mv22.DescOne MaksudTujuanInstitusiDesc, mv41.DescOne CountryOfBirthDesc,  

                                mv42.DescOne CountryofCorrespondenceDesc, mv43.DescOne CountryofDomicileDesc,  
                                mv44.DescOne CountryofEstablishmentDesc, mv45.DescOne CountryofCompanyDesc, mv46.DescOne CompanyCityNameDesc,  

                                mv24.DescOne NegaraDesc,mv25.DescOne NationalityDesc,mv26.DescOne PropinsiDesc,mv27.DescOne OtherPropinsiInd1Desc,mv28.DescOne OtherPropinsiInd2Desc,mv29.DescOne OtherPropinsiInd3Desc,  
                                mv30.DescOne OtherNegaraInd1Desc,mv31.DescOne OtherNegaraInd2Desc,mv32.DescOne OtherNegaraInd3Desc,isnull(IC.Name,'') InternalCategoryID,  
                                mv33.DescOne InvestorsRiskProfileDesc,mv34.DescOne AssetOwnerDesc,mv35.DescOne StatementTypeDesc,mv36.DescOne fatcaDesc,mv37.DescOne TINIssuanceCountryDesc,mv38.DescOne BankCountry1Desc,mv39.DescOne BankCountry2Desc,mv40.DescOne BankCountry3Desc, mv48.DescOne KYCRiskProfileDesc, 
                                mv49.DescOne CompanyTypeOJKDesc, mv50.DescOne BusinessTypeOJKDesc,mv51.DescOne NatureofBusinessDesc,  
                                BC1.BICode BICCode1Name,BC2.BICode BICCode2Name,BC3.BICode BICCode3Name, 
                                * from FundClient fc   
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
                                left join MasterValue mv24 on fc.Negara = mv24.code and  mv24.ID = 'KSEICountry' and mv24.status = 2  
                                left join MasterValue mv25 on fc.Nationality = mv25.code and  mv25.ID = 'Nationality' and mv25.status = 2  
                                left join MasterValue mv26 on fc.Propinsi = mv26.code and  mv26.ID = 'Province' and mv26.status = 2  
                                left join MasterValue mv27 on fc.OtherPropinsiInd1 = mv27.code and  mv27.ID = 'Province' and mv27.status = 2  
                                left join MasterValue mv28 on fc.OtherPropinsiInd2 = mv28.code and  mv28.ID = 'Province' and mv28.status = 2  
                                left join MasterValue mv29 on fc.OtherPropinsiInd3 = mv29.code and  mv29.ID = 'Province' and mv29.status = 2  
                                left join MasterValue mv30 on fc.OtherNegaraInd1 = mv30.code and  mv30.ID = 'KSEICountry' and mv30.status = 2  
                                left join MasterValue mv31 on fc.OtherNegaraInd2 = mv31.code and  mv31.ID = 'KSEICountry' and mv31.status = 2  
                                left join MasterValue mv32 on fc.OtherNegaraInd3 = mv32.code and  mv32.ID = 'KSEICountry' and mv32.status = 2  
                                left join InternalCategory IC on fc.InternalCategoryPK = IC.InternalCategoryPK and IC.status = 2  
                                left join MasterValue mv33 on fc.InvestorsRiskProfile = mv33.code and  mv33.ID = 'InvestorsRiskProfile' and mv33.status = 2  
                                left join MasterValue mv34 on fc.AssetOwner = mv34.code and  mv34.ID = 'AssetOwner' and mv34.status = 2  
                                left join MasterValue mv35 on fc.StatementType = mv35.code and  mv35.ID = 'StatementType' and mv35.status = 2  
                                left join MasterValue mv36 on fc.fatca = mv36.code and  mv36.ID = 'FATCA' and mv36.status = 2  
                                left join MasterValue mv37 on fc.TINIssuanceCountry = mv37.code and  mv37.ID = 'KSEICountry' and mv37.status = 2  
                                left join MasterValue mv38 on fc.BankCountry1 = mv38.code and  mv38.ID = 'KSEICountry' and mv38.status = 2  
                                left join MasterValue mv39 on fc.BankCountry2 = mv39.code and  mv39.ID = 'KSEICountry' and mv39.status = 2  
                                left join MasterValue mv40 on fc.BankCountry3 = mv40.code and  mv40.ID = 'KSEICountry' and mv40.status = 2  
                                left join MasterValue mv41 on fc.CountryOfBirth = mv41.code and  mv41.ID = 'KSEICountry' and mv41.status = 2  
                                left join MasterValue mv42 on fc.CountryofCorrespondence = mv42.code and  mv42.ID = 'KSEICountry' and mv42.status = 2  
                                left join MasterValue mv43 on fc.CountryofDomicile = mv43.code and  mv43.ID = 'KSEICountry' and mv43.status = 2  
                                left join MasterValue mv44 on fc.CountryofEstablishment = mv44.code and  mv44.ID = 'KSEICountry' and mv44.status = 2  
                                left join MasterValue mv45 on fc.CountryofCompany = mv45.code and  mv45.ID = 'KSEICountry' and mv45.status = 2  
                                left join MasterValue mv46 on fc.CompanyCityName = mv46.Code and mv46.ID = 'CityRHB' and mv46.status = 2  
                                left join MasterValue mv48 on fc.KYCRiskProfile = mv48.Code and mv48.ID = 'KYCRiskProfile' and mv48.status = 2  
                                left join MasterValue mv49 on fc.CompanyTypeOJK = mv49.Code and mv49.ID = 'CompanyTypeOJK' and mv49.status = 2  
                                left join MasterValue mv50 on fc.BusinessTypeOJK = mv50.Code and mv50.ID = 'BusinessTypeOJK' and mv50.status = 2  
                                left join MasterValue mv51 on fc.NatureofBusiness = mv51.Code and mv48.ID = 'NatureofBusiness' and mv51.status = 2  
                                left join Bank BC1 on fc.NamaBank1 = BC1.BankPK and BC1.status = 2   
                                left join Bank BC2 on fc.NamaBank2 = BC2.BankPK and BC2.status = 2   
                                left join Bank BC3 on fc.NamaBank3 = BC3.BankPK and BC3.status = 2   
                                where FC.status = @status
                                and
                                FC.DormantDate >'01/01/1900' ";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = @"select case when FC.status=1 then 'PENDING' else Case When FC.status = 2 then 'APPROVED' else Case when FC.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,DormantDate,A.ID SellingAgentID,mv1.DescOne KodeKotaInd1Desc,mv2.DescOne KodeKotaInd2Desc,mv3.DescOne KodeKotaInsDesc,  
                                mv4.DescOne OtherKodeKotaInd1Desc,mv5.DescOne OtherKodeKotaInd2Desc,mv6.DescOne OtherKodeKotaInd3Desc,  
                                mv7.DescOne ClientCategoryDesc,mv8.DescOne InvestorTypeDesc,mv9.DescOne JenisKelaminDesc,  
                                mv10.DescOne StatusPerkawinanDesc,mv11.DescOne PekerjaanDesc,mv12.DescOne PendidikanDesc,  
                                mv13.DescOne AgamaDesc,mv14.DescOne PenghasilanIndDesc,mv15.DescOne SumberDanaIndDesc,  
                                mv16.DescOne MaksudTujuanIndDesc,mv17.DescOne DomisiliDesc,mv18.DescOne TipeDesc,  
                                mv19.DescOne KarakteristikDesc,mv20.DescOne PenghasilanInstitusiDesc, 
                                mv21.DescOne SumberDanaInstitusiDesc,mv22.DescOne MaksudTujuanInstitusiDesc, mv41.DescOne CountryOfBirthDesc,  

                                mv42.DescOne CountryofCorrespondenceDesc, mv43.DescOne CountryofDomicileDesc,  
                                mv44.DescOne CountryofEstablishmentDesc, mv45.DescOne CountryofCompanyDesc, mv46.DescOne CompanyCityNameDesc,  

                                mv24.DescOne NegaraDesc,mv25.DescOne NationalityDesc,mv26.DescOne PropinsiDesc,mv27.DescOne OtherPropinsiInd1Desc,mv28.DescOne OtherPropinsiInd2Desc,mv29.DescOne OtherPropinsiInd3Desc,  
                                mv30.DescOne OtherNegaraInd1Desc,mv31.DescOne OtherNegaraInd2Desc,mv32.DescOne OtherNegaraInd3Desc,isnull(IC.Name,'') InternalCategoryID,  
                                mv33.DescOne InvestorsRiskProfileDesc,mv34.DescOne AssetOwnerDesc,mv35.DescOne StatementTypeDesc,mv36.DescOne fatcaDesc,mv37.DescOne TINIssuanceCountryDesc,mv38.DescOne BankCountry1Desc,mv39.DescOne BankCountry2Desc,mv40.DescOne BankCountry3Desc, mv48.DescOne KYCRiskProfileDesc, 
                                mv49.DescOne CompanyTypeOJKDesc, mv50.DescOne BusinessTypeOJKDesc,mv51.DescOne NatureofBusinessDesc,  
                                BC1.BICode BICCode1Name,BC2.BICode BICCode2Name,BC3.BICode BICCode3Name, 
                                * from FundClient fc   
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
                                left join MasterValue mv24 on fc.Negara = mv24.code and  mv24.ID = 'KSEICountry' and mv24.status = 2  
                                left join MasterValue mv25 on fc.Nationality = mv25.code and  mv25.ID = 'Nationality' and mv25.status = 2  
                                left join MasterValue mv26 on fc.Propinsi = mv26.code and  mv26.ID = 'Province' and mv26.status = 2  
                                left join MasterValue mv27 on fc.OtherPropinsiInd1 = mv27.code and  mv27.ID = 'Province' and mv27.status = 2  
                                left join MasterValue mv28 on fc.OtherPropinsiInd2 = mv28.code and  mv28.ID = 'Province' and mv28.status = 2  
                                left join MasterValue mv29 on fc.OtherPropinsiInd3 = mv29.code and  mv29.ID = 'Province' and mv29.status = 2  
                                left join MasterValue mv30 on fc.OtherNegaraInd1 = mv30.code and  mv30.ID = 'KSEICountry' and mv30.status = 2  
                                left join MasterValue mv31 on fc.OtherNegaraInd2 = mv31.code and  mv31.ID = 'KSEICountry' and mv31.status = 2  
                                left join MasterValue mv32 on fc.OtherNegaraInd3 = mv32.code and  mv32.ID = 'KSEICountry' and mv32.status = 2  
                                left join InternalCategory IC on fc.InternalCategoryPK = IC.InternalCategoryPK and IC.status = 2  
                                left join MasterValue mv33 on fc.InvestorsRiskProfile = mv33.code and  mv33.ID = 'InvestorsRiskProfile' and mv33.status = 2  
                                left join MasterValue mv34 on fc.AssetOwner = mv34.code and  mv34.ID = 'AssetOwner' and mv34.status = 2  
                                left join MasterValue mv35 on fc.StatementType = mv35.code and  mv35.ID = 'StatementType' and mv35.status = 2  
                                left join MasterValue mv36 on fc.fatca = mv36.code and  mv36.ID = 'FATCA' and mv36.status = 2  
                                left join MasterValue mv37 on fc.TINIssuanceCountry = mv37.code and  mv37.ID = 'KSEICountry' and mv37.status = 2  
                                left join MasterValue mv38 on fc.BankCountry1 = mv38.code and  mv38.ID = 'KSEICountry' and mv38.status = 2  
                                left join MasterValue mv39 on fc.BankCountry2 = mv39.code and  mv39.ID = 'KSEICountry' and mv39.status = 2  
                                left join MasterValue mv40 on fc.BankCountry3 = mv40.code and  mv40.ID = 'KSEICountry' and mv40.status = 2  
                                left join MasterValue mv41 on fc.CountryOfBirth = mv41.code and  mv41.ID = 'KSEICountry' and mv41.status = 2  
                                left join MasterValue mv42 on fc.CountryofCorrespondence = mv42.code and  mv42.ID = 'KSEICountry' and mv42.status = 2  
                                left join MasterValue mv43 on fc.CountryofDomicile = mv43.code and  mv43.ID = 'KSEICountry' and mv43.status = 2  
                                left join MasterValue mv44 on fc.CountryofEstablishment = mv44.code and  mv44.ID = 'KSEICountry' and mv44.status = 2  
                                left join MasterValue mv45 on fc.CountryofCompany = mv45.code and  mv45.ID = 'KSEICountry' and mv45.status = 2  
                                left join MasterValue mv46 on fc.CompanyCityName = mv46.Code and mv46.ID = 'CityRHB' and mv46.status = 2  
                                left join MasterValue mv48 on fc.KYCRiskProfile = mv48.Code and mv48.ID = 'KYCRiskProfile' and mv48.status = 2  
                                left join MasterValue mv49 on fc.CompanyTypeOJK = mv49.Code and mv49.ID = 'CompanyTypeOJK' and mv49.status = 2  
                                left join MasterValue mv50 on fc.BusinessTypeOJK = mv50.Code and mv50.ID = 'BusinessTypeOJK' and mv50.status = 2  
                                left join MasterValue mv51 on fc.NatureofBusiness = mv51.Code and mv48.ID = 'NatureofBusiness' and mv51.status = 2  
                                left join Bank BC1 on fc.NamaBank1 = BC1.BankPK and BC1.status = 2   
                                left join Bank BC2 on fc.NamaBank2 = BC2.BankPK and BC2.status = 2   
                                left join Bank BC3 on fc.NamaBank3 = BC3.BankPK and BC3.status = 2   
                                where and FC.DormantDate >'01/01/1900' and  FC.BitIsSuspend = 1

                                union all

                                select case when FC.status=1 then 'PENDING' else Case When FC.status = 2 then 'APPROVED' else Case when FC.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,DormantDate,A.ID SellingAgentID,mv1.DescOne KodeKotaInd1Desc,mv2.DescOne KodeKotaInd2Desc,mv3.DescOne KodeKotaInsDesc,  
                                mv4.DescOne OtherKodeKotaInd1Desc,mv5.DescOne OtherKodeKotaInd2Desc,mv6.DescOne OtherKodeKotaInd3Desc,  
                                mv7.DescOne ClientCategoryDesc,mv8.DescOne InvestorTypeDesc,mv9.DescOne JenisKelaminDesc,  
                                mv10.DescOne StatusPerkawinanDesc,mv11.DescOne PekerjaanDesc,mv12.DescOne PendidikanDesc,  
                                mv13.DescOne AgamaDesc,mv14.DescOne PenghasilanIndDesc,mv15.DescOne SumberDanaIndDesc,  
                                mv16.DescOne MaksudTujuanIndDesc,mv17.DescOne DomisiliDesc,mv18.DescOne TipeDesc,  
                                mv19.DescOne KarakteristikDesc,mv20.DescOne PenghasilanInstitusiDesc, 
                                mv21.DescOne SumberDanaInstitusiDesc,mv22.DescOne MaksudTujuanInstitusiDesc, mv41.DescOne CountryOfBirthDesc,  

                                mv42.DescOne CountryofCorrespondenceDesc, mv43.DescOne CountryofDomicileDesc,  
                                mv44.DescOne CountryofEstablishmentDesc, mv45.DescOne CountryofCompanyDesc, mv46.DescOne CompanyCityNameDesc,  

                                mv24.DescOne NegaraDesc,mv25.DescOne NationalityDesc,mv26.DescOne PropinsiDesc,mv27.DescOne OtherPropinsiInd1Desc,mv28.DescOne OtherPropinsiInd2Desc,mv29.DescOne OtherPropinsiInd3Desc,  
                                mv30.DescOne OtherNegaraInd1Desc,mv31.DescOne OtherNegaraInd2Desc,mv32.DescOne OtherNegaraInd3Desc,isnull(IC.Name,'') InternalCategoryID,  
                                mv33.DescOne InvestorsRiskProfileDesc,mv34.DescOne AssetOwnerDesc,mv35.DescOne StatementTypeDesc,mv36.DescOne fatcaDesc,mv37.DescOne TINIssuanceCountryDesc,mv38.DescOne BankCountry1Desc,mv39.DescOne BankCountry2Desc,mv40.DescOne BankCountry3Desc, mv48.DescOne KYCRiskProfileDesc, 
                                mv49.DescOne CompanyTypeOJKDesc, mv50.DescOne BusinessTypeOJKDesc,mv51.DescOne NatureofBusinessDesc,  
                                BC1.BICode BICCode1Name,BC2.BICode BICCode2Name,BC3.BICode BICCode3Name, 
                                * from FundClient fc   
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
                                left join MasterValue mv24 on fc.Negara = mv24.code and  mv24.ID = 'KSEICountry' and mv24.status = 2  
                                left join MasterValue mv25 on fc.Nationality = mv25.code and  mv25.ID = 'Nationality' and mv25.status = 2  
                                left join MasterValue mv26 on fc.Propinsi = mv26.code and  mv26.ID = 'Province' and mv26.status = 2  
                                left join MasterValue mv27 on fc.OtherPropinsiInd1 = mv27.code and  mv27.ID = 'Province' and mv27.status = 2  
                                left join MasterValue mv28 on fc.OtherPropinsiInd2 = mv28.code and  mv28.ID = 'Province' and mv28.status = 2  
                                left join MasterValue mv29 on fc.OtherPropinsiInd3 = mv29.code and  mv29.ID = 'Province' and mv29.status = 2  
                                left join MasterValue mv30 on fc.OtherNegaraInd1 = mv30.code and  mv30.ID = 'KSEICountry' and mv30.status = 2  
                                left join MasterValue mv31 on fc.OtherNegaraInd2 = mv31.code and  mv31.ID = 'KSEICountry' and mv31.status = 2  
                                left join MasterValue mv32 on fc.OtherNegaraInd3 = mv32.code and  mv32.ID = 'KSEICountry' and mv32.status = 2  
                                left join InternalCategory IC on fc.InternalCategoryPK = IC.InternalCategoryPK and IC.status = 2  
                                left join MasterValue mv33 on fc.InvestorsRiskProfile = mv33.code and  mv33.ID = 'InvestorsRiskProfile' and mv33.status = 2  
                                left join MasterValue mv34 on fc.AssetOwner = mv34.code and  mv34.ID = 'AssetOwner' and mv34.status = 2  
                                left join MasterValue mv35 on fc.StatementType = mv35.code and  mv35.ID = 'StatementType' and mv35.status = 2  
                                left join MasterValue mv36 on fc.fatca = mv36.code and  mv36.ID = 'FATCA' and mv36.status = 2  
                                left join MasterValue mv37 on fc.TINIssuanceCountry = mv37.code and  mv37.ID = 'KSEICountry' and mv37.status = 2  
                                left join MasterValue mv38 on fc.BankCountry1 = mv38.code and  mv38.ID = 'KSEICountry' and mv38.status = 2  
                                left join MasterValue mv39 on fc.BankCountry2 = mv39.code and  mv39.ID = 'KSEICountry' and mv39.status = 2  
                                left join MasterValue mv40 on fc.BankCountry3 = mv40.code and  mv40.ID = 'KSEICountry' and mv40.status = 2  
                                left join MasterValue mv41 on fc.CountryOfBirth = mv41.code and  mv41.ID = 'KSEICountry' and mv41.status = 2  
                                left join MasterValue mv42 on fc.CountryofCorrespondence = mv42.code and  mv42.ID = 'KSEICountry' and mv42.status = 2  
                                left join MasterValue mv43 on fc.CountryofDomicile = mv43.code and  mv43.ID = 'KSEICountry' and mv43.status = 2  
                                left join MasterValue mv44 on fc.CountryofEstablishment = mv44.code and  mv44.ID = 'KSEICountry' and mv44.status = 2  
                                left join MasterValue mv45 on fc.CountryofCompany = mv45.code and  mv45.ID = 'KSEICountry' and mv45.status = 2  
                                left join MasterValue mv46 on fc.CompanyCityName = mv46.Code and mv46.ID = 'CityRHB' and mv46.status = 2  
                                left join MasterValue mv48 on fc.KYCRiskProfile = mv48.Code and mv48.ID = 'KYCRiskProfile' and mv48.status = 2  
                                left join MasterValue mv49 on fc.CompanyTypeOJK = mv49.Code and mv49.ID = 'CompanyTypeOJK' and mv49.status = 2  
                                left join MasterValue mv50 on fc.BusinessTypeOJK = mv50.Code and mv50.ID = 'BusinessTypeOJK' and mv50.status = 2  
                                left join MasterValue mv51 on fc.NatureofBusiness = mv51.Code and mv48.ID = 'NatureofBusiness' and mv51.status = 2  
                                left join Bank BC1 on fc.NamaBank1 = BC1.BankPK and BC1.status = 2   
                                left join Bank BC2 on fc.NamaBank2 = BC2.BankPK and BC2.status = 2   
                                left join Bank BC3 on fc.NamaBank3 = BC3.BankPK and BC3.status = 2   
                                where
                                FC.DormantDate > '01/01/1900' ";
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

                        cmd.CommandText = @"select case when FC.status=1 then 'PENDING' else Case When FC.status = 2 then 'APPROVED' else Case when FC.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,DormantDate,A.ID SellingAgentID,mv1.DescOne KodeKotaInd1Desc,mv2.DescOne KodeKotaInd2Desc,mv3.DescOne KodeKotaInsDesc,  
                                mv4.DescOne OtherKodeKotaInd1Desc,mv5.DescOne OtherKodeKotaInd2Desc,mv6.DescOne OtherKodeKotaInd3Desc,  
                                mv7.DescOne ClientCategoryDesc,mv8.DescOne InvestorTypeDesc,mv9.DescOne JenisKelaminDesc,  
                                mv10.DescOne StatusPerkawinanDesc,mv11.DescOne PekerjaanDesc,mv12.DescOne PendidikanDesc,  
                                mv13.DescOne AgamaDesc,mv14.DescOne PenghasilanIndDesc,mv15.DescOne SumberDanaIndDesc,  
                                mv16.DescOne MaksudTujuanIndDesc,mv17.DescOne DomisiliDesc,mv18.DescOne TipeDesc,  
                                mv19.DescOne KarakteristikDesc,mv20.DescOne PenghasilanInstitusiDesc, 
                                mv21.DescOne SumberDanaInstitusiDesc,mv22.DescOne MaksudTujuanInstitusiDesc, mv41.DescOne CountryOfBirthDesc,  

                                mv42.DescOne CountryofCorrespondenceDesc, mv43.DescOne CountryofDomicileDesc,  
                                mv44.DescOne CountryofEstablishmentDesc, mv45.DescOne CountryofCompanyDesc, mv46.DescOne CompanyCityNameDesc,  

                                mv24.DescOne NegaraDesc,mv25.DescOne NationalityDesc,mv26.DescOne PropinsiDesc,mv27.DescOne OtherPropinsiInd1Desc,mv28.DescOne OtherPropinsiInd2Desc,mv29.DescOne OtherPropinsiInd3Desc,  
                                mv30.DescOne OtherNegaraInd1Desc,mv31.DescOne OtherNegaraInd2Desc,mv32.DescOne OtherNegaraInd3Desc,isnull(IC.Name,'') InternalCategoryID,  
                                mv33.DescOne InvestorsRiskProfileDesc,mv34.DescOne AssetOwnerDesc,mv35.DescOne StatementTypeDesc,mv36.DescOne fatcaDesc,mv37.DescOne TINIssuanceCountryDesc,mv38.DescOne BankCountry1Desc,mv39.DescOne BankCountry2Desc,mv40.DescOne BankCountry3Desc, mv48.DescOne KYCRiskProfileDesc, 
                                mv49.DescOne CompanyTypeOJKDesc, mv50.DescOne BusinessTypeOJKDesc,mv51.DescOne NatureofBusinessDesc,  
                                BC1.BICode BICCode1Name,BC2.BICode BICCode2Name,BC3.BICode BICCode3Name, 
                                * from FundClient fc   
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
                                left join MasterValue mv24 on fc.Negara = mv24.code and  mv24.ID = 'KSEICountry' and mv24.status = 2  
                                left join MasterValue mv25 on fc.Nationality = mv25.code and  mv25.ID = 'Nationality' and mv25.status = 2  
                                left join MasterValue mv26 on fc.Propinsi = mv26.code and  mv26.ID = 'Province' and mv26.status = 2  
                                left join MasterValue mv27 on fc.OtherPropinsiInd1 = mv27.code and  mv27.ID = 'Province' and mv27.status = 2  
                                left join MasterValue mv28 on fc.OtherPropinsiInd2 = mv28.code and  mv28.ID = 'Province' and mv28.status = 2  
                                left join MasterValue mv29 on fc.OtherPropinsiInd3 = mv29.code and  mv29.ID = 'Province' and mv29.status = 2  
                                left join MasterValue mv30 on fc.OtherNegaraInd1 = mv30.code and  mv30.ID = 'KSEICountry' and mv30.status = 2  
                                left join MasterValue mv31 on fc.OtherNegaraInd2 = mv31.code and  mv31.ID = 'KSEICountry' and mv31.status = 2  
                                left join MasterValue mv32 on fc.OtherNegaraInd3 = mv32.code and  mv32.ID = 'KSEICountry' and mv32.status = 2  
                                left join InternalCategory IC on fc.InternalCategoryPK = IC.InternalCategoryPK and IC.status = 2  
                                left join MasterValue mv33 on fc.InvestorsRiskProfile = mv33.code and  mv33.ID = 'InvestorsRiskProfile' and mv33.status = 2  
                                left join MasterValue mv34 on fc.AssetOwner = mv34.code and  mv34.ID = 'AssetOwner' and mv34.status = 2  
                                left join MasterValue mv35 on fc.StatementType = mv35.code and  mv35.ID = 'StatementType' and mv35.status = 2  
                                left join MasterValue mv36 on fc.fatca = mv36.code and  mv36.ID = 'FATCA' and mv36.status = 2  
                                left join MasterValue mv37 on fc.TINIssuanceCountry = mv37.code and  mv37.ID = 'KSEICountry' and mv37.status = 2  
                                left join MasterValue mv38 on fc.BankCountry1 = mv38.code and  mv38.ID = 'KSEICountry' and mv38.status = 2  
                                left join MasterValue mv39 on fc.BankCountry2 = mv39.code and  mv39.ID = 'KSEICountry' and mv39.status = 2  
                                left join MasterValue mv40 on fc.BankCountry3 = mv40.code and  mv40.ID = 'KSEICountry' and mv40.status = 2  
                                left join MasterValue mv41 on fc.CountryOfBirth = mv41.code and  mv41.ID = 'KSEICountry' and mv41.status = 2  
                                left join MasterValue mv42 on fc.CountryofCorrespondence = mv42.code and  mv42.ID = 'KSEICountry' and mv42.status = 2  
                                left join MasterValue mv43 on fc.CountryofDomicile = mv43.code and  mv43.ID = 'KSEICountry' and mv43.status = 2  
                                left join MasterValue mv44 on fc.CountryofEstablishment = mv44.code and  mv44.ID = 'KSEICountry' and mv44.status = 2  
                                left join MasterValue mv45 on fc.CountryofCompany = mv45.code and  mv45.ID = 'KSEICountry' and mv45.status = 2  
                                left join MasterValue mv46 on fc.CompanyCityName = mv46.Code and mv46.ID = 'CityRHB' and mv46.status = 2  
                                left join MasterValue mv48 on fc.KYCRiskProfile = mv48.Code and mv48.ID = 'KYCRiskProfile' and mv48.status = 2  
                                left join MasterValue mv49 on fc.CompanyTypeOJK = mv49.Code and mv49.ID = 'CompanyTypeOJK' and mv49.status = 2  
                                left join MasterValue mv50 on fc.BusinessTypeOJK = mv50.Code and mv50.ID = 'BusinessTypeOJK' and mv50.status = 2  
                                left join MasterValue mv51 on fc.NatureofBusiness = mv51.Code and mv48.ID = 'NatureofBusiness' and mv51.status = 2  
                                left join Bank BC1 on fc.NamaBank1 = BC1.BankPK and BC1.status = 2   
                                left join Bank BC2 on fc.NamaBank2 = BC2.BankPK and BC2.status = 2   
                                left join Bank BC3 on fc.NamaBank3 = BC3.BankPK and BC3.status = 2   
                                where FC.DormantDate > '01/01/1900' and FC.BitIsSuspend = 1 and FC.HistoryPK = @HistoryPK and FC.FundClientPK  = @FundClientPK

                                union all

                                select case when FC.status=1 then 'PENDING' else Case When FC.status = 2 then 'APPROVED' else Case when FC.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,DormantDate,A.ID SellingAgentID,mv1.DescOne KodeKotaInd1Desc,mv2.DescOne KodeKotaInd2Desc,mv3.DescOne KodeKotaInsDesc,  
                                mv4.DescOne OtherKodeKotaInd1Desc,mv5.DescOne OtherKodeKotaInd2Desc,mv6.DescOne OtherKodeKotaInd3Desc,  
                                mv7.DescOne ClientCategoryDesc,mv8.DescOne InvestorTypeDesc,mv9.DescOne JenisKelaminDesc,  
                                mv10.DescOne StatusPerkawinanDesc,mv11.DescOne PekerjaanDesc,mv12.DescOne PendidikanDesc,  
                                mv13.DescOne AgamaDesc,mv14.DescOne PenghasilanIndDesc,mv15.DescOne SumberDanaIndDesc,  
                                mv16.DescOne MaksudTujuanIndDesc,mv17.DescOne DomisiliDesc,mv18.DescOne TipeDesc,  
                                mv19.DescOne KarakteristikDesc,mv20.DescOne PenghasilanInstitusiDesc, 
                                mv21.DescOne SumberDanaInstitusiDesc,mv22.DescOne MaksudTujuanInstitusiDesc, mv41.DescOne CountryOfBirthDesc,  

                                mv42.DescOne CountryofCorrespondenceDesc, mv43.DescOne CountryofDomicileDesc,  
                                mv44.DescOne CountryofEstablishmentDesc, mv45.DescOne CountryofCompanyDesc, mv46.DescOne CompanyCityNameDesc,  

                                mv24.DescOne NegaraDesc,mv25.DescOne NationalityDesc,mv26.DescOne PropinsiDesc,mv27.DescOne OtherPropinsiInd1Desc,mv28.DescOne OtherPropinsiInd2Desc,mv29.DescOne OtherPropinsiInd3Desc,  
                                mv30.DescOne OtherNegaraInd1Desc,mv31.DescOne OtherNegaraInd2Desc,mv32.DescOne OtherNegaraInd3Desc,isnull(IC.Name,'') InternalCategoryID,  
                                mv33.DescOne InvestorsRiskProfileDesc,mv34.DescOne AssetOwnerDesc,mv35.DescOne StatementTypeDesc,mv36.DescOne fatcaDesc,mv37.DescOne TINIssuanceCountryDesc,mv38.DescOne BankCountry1Desc,mv39.DescOne BankCountry2Desc,mv40.DescOne BankCountry3Desc, mv48.DescOne KYCRiskProfileDesc, 
                                mv49.DescOne CompanyTypeOJKDesc, mv50.DescOne BusinessTypeOJKDesc,mv51.DescOne NatureofBusinessDesc,  
                                BC1.BICode BICCode1Name,BC2.BICode BICCode2Name,BC3.BICode BICCode3Name, 
                                * from FundClient fc   
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
                                left join MasterValue mv24 on fc.Negara = mv24.code and  mv24.ID = 'KSEICountry' and mv24.status = 2  
                                left join MasterValue mv25 on fc.Nationality = mv25.code and  mv25.ID = 'Nationality' and mv25.status = 2  
                                left join MasterValue mv26 on fc.Propinsi = mv26.code and  mv26.ID = 'Province' and mv26.status = 2  
                                left join MasterValue mv27 on fc.OtherPropinsiInd1 = mv27.code and  mv27.ID = 'Province' and mv27.status = 2  
                                left join MasterValue mv28 on fc.OtherPropinsiInd2 = mv28.code and  mv28.ID = 'Province' and mv28.status = 2  
                                left join MasterValue mv29 on fc.OtherPropinsiInd3 = mv29.code and  mv29.ID = 'Province' and mv29.status = 2  
                                left join MasterValue mv30 on fc.OtherNegaraInd1 = mv30.code and  mv30.ID = 'KSEICountry' and mv30.status = 2  
                                left join MasterValue mv31 on fc.OtherNegaraInd2 = mv31.code and  mv31.ID = 'KSEICountry' and mv31.status = 2  
                                left join MasterValue mv32 on fc.OtherNegaraInd3 = mv32.code and  mv32.ID = 'KSEICountry' and mv32.status = 2  
                                left join InternalCategory IC on fc.InternalCategoryPK = IC.InternalCategoryPK and IC.status = 2  
                                left join MasterValue mv33 on fc.InvestorsRiskProfile = mv33.code and  mv33.ID = 'InvestorsRiskProfile' and mv33.status = 2  
                                left join MasterValue mv34 on fc.AssetOwner = mv34.code and  mv34.ID = 'AssetOwner' and mv34.status = 2  
                                left join MasterValue mv35 on fc.StatementType = mv35.code and  mv35.ID = 'StatementType' and mv35.status = 2  
                                left join MasterValue mv36 on fc.fatca = mv36.code and  mv36.ID = 'FATCA' and mv36.status = 2  
                                left join MasterValue mv37 on fc.TINIssuanceCountry = mv37.code and  mv37.ID = 'KSEICountry' and mv37.status = 2  
                                left join MasterValue mv38 on fc.BankCountry1 = mv38.code and  mv38.ID = 'KSEICountry' and mv38.status = 2  
                                left join MasterValue mv39 on fc.BankCountry2 = mv39.code and  mv39.ID = 'KSEICountry' and mv39.status = 2  
                                left join MasterValue mv40 on fc.BankCountry3 = mv40.code and  mv40.ID = 'KSEICountry' and mv40.status = 2  
                                left join MasterValue mv41 on fc.CountryOfBirth = mv41.code and  mv41.ID = 'KSEICountry' and mv41.status = 2  
                                left join MasterValue mv42 on fc.CountryofCorrespondence = mv42.code and  mv42.ID = 'KSEICountry' and mv42.status = 2  
                                left join MasterValue mv43 on fc.CountryofDomicile = mv43.code and  mv43.ID = 'KSEICountry' and mv43.status = 2  
                                left join MasterValue mv44 on fc.CountryofEstablishment = mv44.code and  mv44.ID = 'KSEICountry' and mv44.status = 2  
                                left join MasterValue mv45 on fc.CountryofCompany = mv45.code and  mv45.ID = 'KSEICountry' and mv45.status = 2  
                                left join MasterValue mv46 on fc.CompanyCityName = mv46.Code and mv46.ID = 'CityRHB' and mv46.status = 2  
                                left join MasterValue mv48 on fc.KYCRiskProfile = mv48.Code and mv48.ID = 'KYCRiskProfile' and mv48.status = 2  
                                left join MasterValue mv49 on fc.CompanyTypeOJK = mv49.Code and mv49.ID = 'CompanyTypeOJK' and mv49.status = 2  
                                left join MasterValue mv50 on fc.BusinessTypeOJK = mv50.Code and mv50.ID = 'BusinessTypeOJK' and mv50.status = 2  
                                left join MasterValue mv51 on fc.NatureofBusiness = mv51.Code and mv48.ID = 'NatureofBusiness' and mv51.status = 2  
                                left join Bank BC1 on fc.NamaBank1 = BC1.BankPK and BC1.status = 2   
                                left join Bank BC2 on fc.NamaBank2 = BC2.BankPK and BC2.status = 2   
                                left join Bank BC3 on fc.NamaBank3 = BC3.BankPK and BC3.status = 2
                                where FC.HistoryPK = @HistoryPK and FC.FundClientPK  = @FundClientPK 
                                and
                                FC.DormantDate >'01/01/1900'";
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

                        cmd.CommandText = @"select case when FC.status=1 then 'PENDING' else Case When FC.status = 2 then 'APPROVED' else Case when FC.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,DormantDate,A.ID SellingAgentID,mv1.DescOne KodeKotaInd1Desc,mv2.DescOne KodeKotaInd2Desc,mv3.DescOne KodeKotaInsDesc,  
                                mv4.DescOne OtherKodeKotaInd1Desc,mv5.DescOne OtherKodeKotaInd2Desc,mv6.DescOne OtherKodeKotaInd3Desc,  
                                mv7.DescOne ClientCategoryDesc,mv8.DescOne InvestorTypeDesc,mv9.DescOne JenisKelaminDesc,  
                                mv10.DescOne StatusPerkawinanDesc,mv11.DescOne PekerjaanDesc,mv12.DescOne PendidikanDesc,  
                                mv13.DescOne AgamaDesc,mv14.DescOne PenghasilanIndDesc,mv15.DescOne SumberDanaIndDesc,  
                                mv16.DescOne MaksudTujuanIndDesc,mv17.DescOne DomisiliDesc,mv18.DescOne TipeDesc,  
                                mv19.DescOne KarakteristikDesc,mv20.DescOne PenghasilanInstitusiDesc, 
                                mv21.DescOne SumberDanaInstitusiDesc,mv22.DescOne MaksudTujuanInstitusiDesc, mv41.DescOne CountryOfBirthDesc,  

                                mv42.DescOne CountryofCorrespondenceDesc, mv43.DescOne CountryofDomicileDesc,  
                                mv44.DescOne CountryofEstablishmentDesc, mv45.DescOne CountryofCompanyDesc, mv46.DescOne CompanyCityNameDesc,  

                                mv24.DescOne NegaraDesc,mv25.DescOne NationalityDesc,mv26.DescOne PropinsiDesc,mv27.DescOne OtherPropinsiInd1Desc,mv28.DescOne OtherPropinsiInd2Desc,mv29.DescOne OtherPropinsiInd3Desc,  
                                mv30.DescOne OtherNegaraInd1Desc,mv31.DescOne OtherNegaraInd2Desc,mv32.DescOne OtherNegaraInd3Desc,isnull(IC.Name,'') InternalCategoryID,  
                                mv33.DescOne InvestorsRiskProfileDesc,mv34.DescOne AssetOwnerDesc,mv35.DescOne StatementTypeDesc,mv36.DescOne fatcaDesc,mv37.DescOne TINIssuanceCountryDesc,mv38.DescOne BankCountry1Desc,mv39.DescOne BankCountry2Desc,mv40.DescOne BankCountry3Desc, mv48.DescOne KYCRiskProfileDesc, 
                                mv49.DescOne CompanyTypeOJKDesc, mv50.DescOne BusinessTypeOJKDesc,mv51.DescOne NatureofBusinessDesc,  
                                BC1.BICode BICCode1Name,BC2.BICode BICCode2Name,BC3.BICode BICCode3Name, 
                                * from FundClient fc   
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
                                left join MasterValue mv24 on fc.Negara = mv24.code and  mv24.ID = 'KSEICountry' and mv24.status = 2  
                                left join MasterValue mv25 on fc.Nationality = mv25.code and  mv25.ID = 'Nationality' and mv25.status = 2  
                                left join MasterValue mv26 on fc.Propinsi = mv26.code and  mv26.ID = 'Province' and mv26.status = 2  
                                left join MasterValue mv27 on fc.OtherPropinsiInd1 = mv27.code and  mv27.ID = 'Province' and mv27.status = 2  
                                left join MasterValue mv28 on fc.OtherPropinsiInd2 = mv28.code and  mv28.ID = 'Province' and mv28.status = 2  
                                left join MasterValue mv29 on fc.OtherPropinsiInd3 = mv29.code and  mv29.ID = 'Province' and mv29.status = 2  
                                left join MasterValue mv30 on fc.OtherNegaraInd1 = mv30.code and  mv30.ID = 'KSEICountry' and mv30.status = 2  
                                left join MasterValue mv31 on fc.OtherNegaraInd2 = mv31.code and  mv31.ID = 'KSEICountry' and mv31.status = 2  
                                left join MasterValue mv32 on fc.OtherNegaraInd3 = mv32.code and  mv32.ID = 'KSEICountry' and mv32.status = 2  
                                left join InternalCategory IC on fc.InternalCategoryPK = IC.InternalCategoryPK and IC.status = 2  
                                left join MasterValue mv33 on fc.InvestorsRiskProfile = mv33.code and  mv33.ID = 'InvestorsRiskProfile' and mv33.status = 2  
                                left join MasterValue mv34 on fc.AssetOwner = mv34.code and  mv34.ID = 'AssetOwner' and mv34.status = 2  
                                left join MasterValue mv35 on fc.StatementType = mv35.code and  mv35.ID = 'StatementType' and mv35.status = 2  
                                left join MasterValue mv36 on fc.fatca = mv36.code and  mv36.ID = 'FATCA' and mv36.status = 2  
                                left join MasterValue mv37 on fc.TINIssuanceCountry = mv37.code and  mv37.ID = 'KSEICountry' and mv37.status = 2  
                                left join MasterValue mv38 on fc.BankCountry1 = mv38.code and  mv38.ID = 'KSEICountry' and mv38.status = 2  
                                left join MasterValue mv39 on fc.BankCountry2 = mv39.code and  mv39.ID = 'KSEICountry' and mv39.status = 2  
                                left join MasterValue mv40 on fc.BankCountry3 = mv40.code and  mv40.ID = 'KSEICountry' and mv40.status = 2  
                                left join MasterValue mv41 on fc.CountryOfBirth = mv41.code and  mv41.ID = 'KSEICountry' and mv41.status = 2  
                                left join MasterValue mv42 on fc.CountryofCorrespondence = mv42.code and  mv42.ID = 'KSEICountry' and mv42.status = 2  
                                left join MasterValue mv43 on fc.CountryofDomicile = mv43.code and  mv43.ID = 'KSEICountry' and mv43.status = 2  
                                left join MasterValue mv44 on fc.CountryofEstablishment = mv44.code and  mv44.ID = 'KSEICountry' and mv44.status = 2  
                                left join MasterValue mv45 on fc.CountryofCompany = mv45.code and  mv45.ID = 'KSEICountry' and mv45.status = 2  
                                left join MasterValue mv46 on fc.CompanyCityName = mv46.Code and mv46.ID = 'CityRHB' and mv46.status = 2  
                                left join MasterValue mv48 on fc.KYCRiskProfile = mv48.Code and mv48.ID = 'KYCRiskProfile' and mv48.status = 2  
                                left join MasterValue mv49 on fc.CompanyTypeOJK = mv49.Code and mv49.ID = 'CompanyTypeOJK' and mv49.status = 2  
                                left join MasterValue mv50 on fc.BusinessTypeOJK = mv50.Code and mv50.ID = 'BusinessTypeOJK' and mv50.status = 2  
                                left join MasterValue mv51 on fc.NatureofBusiness = mv51.Code and mv48.ID = 'NatureofBusiness' and mv51.status = 2  
                                left join Bank BC1 on fc.NamaBank1 = BC1.BankPK and BC1.status = 2   
                                left join Bank BC2 on fc.NamaBank2 = BC2.BankPK and BC2.status = 2   
                                left join Bank BC3 on fc.NamaBank3 = BC3.BankPK and BC3.status = 2   
                                where FC.DormantDate > '01/01/1900' and not FC.BitIsSuspend = 0 and FC.status = @status and FC.FundClientPK  = @FundClientPK

                                union all

                                select case when FC.status=1 then 'PENDING' else Case When FC.status = 2 then 'APPROVED' else Case when FC.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,DormantDate,A.ID SellingAgentID,mv1.DescOne KodeKotaInd1Desc,mv2.DescOne KodeKotaInd2Desc,mv3.DescOne KodeKotaInsDesc,  
                                mv4.DescOne OtherKodeKotaInd1Desc,mv5.DescOne OtherKodeKotaInd2Desc,mv6.DescOne OtherKodeKotaInd3Desc,  
                                mv7.DescOne ClientCategoryDesc,mv8.DescOne InvestorTypeDesc,mv9.DescOne JenisKelaminDesc,  
                                mv10.DescOne StatusPerkawinanDesc,mv11.DescOne PekerjaanDesc,mv12.DescOne PendidikanDesc,  
                                mv13.DescOne AgamaDesc,mv14.DescOne PenghasilanIndDesc,mv15.DescOne SumberDanaIndDesc,  
                                mv16.DescOne MaksudTujuanIndDesc,mv17.DescOne DomisiliDesc,mv18.DescOne TipeDesc,  
                                mv19.DescOne KarakteristikDesc,mv20.DescOne PenghasilanInstitusiDesc, 
                                mv21.DescOne SumberDanaInstitusiDesc,mv22.DescOne MaksudTujuanInstitusiDesc, mv41.DescOne CountryOfBirthDesc,  

                                mv42.DescOne CountryofCorrespondenceDesc, mv43.DescOne CountryofDomicileDesc,  
                                mv44.DescOne CountryofEstablishmentDesc, mv45.DescOne CountryofCompanyDesc, mv46.DescOne CompanyCityNameDesc,  

                                mv24.DescOne NegaraDesc,mv25.DescOne NationalityDesc,mv26.DescOne PropinsiDesc,mv27.DescOne OtherPropinsiInd1Desc,mv28.DescOne OtherPropinsiInd2Desc,mv29.DescOne OtherPropinsiInd3Desc,  
                                mv30.DescOne OtherNegaraInd1Desc,mv31.DescOne OtherNegaraInd2Desc,mv32.DescOne OtherNegaraInd3Desc,isnull(IC.Name,'') InternalCategoryID,  
                                mv33.DescOne InvestorsRiskProfileDesc,mv34.DescOne AssetOwnerDesc,mv35.DescOne StatementTypeDesc,mv36.DescOne fatcaDesc,mv37.DescOne TINIssuanceCountryDesc,mv38.DescOne BankCountry1Desc,mv39.DescOne BankCountry2Desc,mv40.DescOne BankCountry3Desc, mv48.DescOne KYCRiskProfileDesc, 
                                mv49.DescOne CompanyTypeOJKDesc, mv50.DescOne BusinessTypeOJKDesc,mv51.DescOne NatureofBusinessDesc,  
                                BC1.BICode BICCode1Name,BC2.BICode BICCode2Name,BC3.BICode BICCode3Name, 
                                * from FundClient fc   
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
                                left join MasterValue mv24 on fc.Negara = mv24.code and  mv24.ID = 'KSEICountry' and mv24.status = 2  
                                left join MasterValue mv25 on fc.Nationality = mv25.code and  mv25.ID = 'Nationality' and mv25.status = 2  
                                left join MasterValue mv26 on fc.Propinsi = mv26.code and  mv26.ID = 'Province' and mv26.status = 2  
                                left join MasterValue mv27 on fc.OtherPropinsiInd1 = mv27.code and  mv27.ID = 'Province' and mv27.status = 2  
                                left join MasterValue mv28 on fc.OtherPropinsiInd2 = mv28.code and  mv28.ID = 'Province' and mv28.status = 2  
                                left join MasterValue mv29 on fc.OtherPropinsiInd3 = mv29.code and  mv29.ID = 'Province' and mv29.status = 2  
                                left join MasterValue mv30 on fc.OtherNegaraInd1 = mv30.code and  mv30.ID = 'KSEICountry' and mv30.status = 2  
                                left join MasterValue mv31 on fc.OtherNegaraInd2 = mv31.code and  mv31.ID = 'KSEICountry' and mv31.status = 2  
                                left join MasterValue mv32 on fc.OtherNegaraInd3 = mv32.code and  mv32.ID = 'KSEICountry' and mv32.status = 2  
                                left join InternalCategory IC on fc.InternalCategoryPK = IC.InternalCategoryPK and IC.status = 2  
                                left join MasterValue mv33 on fc.InvestorsRiskProfile = mv33.code and  mv33.ID = 'InvestorsRiskProfile' and mv33.status = 2  
                                left join MasterValue mv34 on fc.AssetOwner = mv34.code and  mv34.ID = 'AssetOwner' and mv34.status = 2  
                                left join MasterValue mv35 on fc.StatementType = mv35.code and  mv35.ID = 'StatementType' and mv35.status = 2  
                                left join MasterValue mv36 on fc.fatca = mv36.code and  mv36.ID = 'FATCA' and mv36.status = 2  
                                left join MasterValue mv37 on fc.TINIssuanceCountry = mv37.code and  mv37.ID = 'KSEICountry' and mv37.status = 2  
                                left join MasterValue mv38 on fc.BankCountry1 = mv38.code and  mv38.ID = 'KSEICountry' and mv38.status = 2  
                                left join MasterValue mv39 on fc.BankCountry2 = mv39.code and  mv39.ID = 'KSEICountry' and mv39.status = 2  
                                left join MasterValue mv40 on fc.BankCountry3 = mv40.code and  mv40.ID = 'KSEICountry' and mv40.status = 2  
                                left join MasterValue mv41 on fc.CountryOfBirth = mv41.code and  mv41.ID = 'KSEICountry' and mv41.status = 2  
                                left join MasterValue mv42 on fc.CountryofCorrespondence = mv42.code and  mv42.ID = 'KSEICountry' and mv42.status = 2  
                                left join MasterValue mv43 on fc.CountryofDomicile = mv43.code and  mv43.ID = 'KSEICountry' and mv43.status = 2  
                                left join MasterValue mv44 on fc.CountryofEstablishment = mv44.code and  mv44.ID = 'KSEICountry' and mv44.status = 2  
                                left join MasterValue mv45 on fc.CountryofCompany = mv45.code and  mv45.ID = 'KSEICountry' and mv45.status = 2  
                                left join MasterValue mv46 on fc.CompanyCityName = mv46.Code and mv46.ID = 'CityRHB' and mv46.status = 2  
                                left join MasterValue mv48 on fc.KYCRiskProfile = mv48.Code and mv48.ID = 'KYCRiskProfile' and mv48.status = 2  
                                left join MasterValue mv49 on fc.CompanyTypeOJK = mv49.Code and mv49.ID = 'CompanyTypeOJK' and mv49.status = 2  
                                left join MasterValue mv50 on fc.BusinessTypeOJK = mv50.Code and mv50.ID = 'BusinessTypeOJK' and mv50.status = 2  
                                left join MasterValue mv51 on fc.NatureofBusiness = mv51.Code and mv48.ID = 'NatureofBusiness' and mv51.status = 2  
                                left join Bank BC1 on fc.NamaBank1 = BC1.BankPK and BC1.status = 2   
                                left join Bank BC2 on fc.NamaBank2 = BC2.BankPK and BC2.status = 2   
                                left join Bank BC3 on fc.NamaBank3 = BC3.BankPK and BC3.status = 2
                                where FC.status = @status and FC.FundClientPK  = @FundClientPK 
                                and
                                FC.DormantDate >'01/01/1900'";
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
            if (_host.CheckColumnIsExist(dr, "HistoryPK"))
            {
                M_FundClientSearchResult.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            }
            M_FundClientSearchResult.ID = Convert.ToString(dr["ID"]);
            M_FundClientSearchResult.Name = Convert.ToString(dr["Name"]);
            M_FundClientSearchResult.SellingAgentID = Convert.ToString(dr["SellingAgentID"]);
            M_FundClientSearchResult.Email = Convert.ToString(dr["Email"]);
            M_FundClientSearchResult.TeleponSelular = Convert.ToString(dr["TeleponSelular"]);
            M_FundClientSearchResult.NamaBank1 = Convert.ToString(dr["NamaBank1"]);
            M_FundClientSearchResult.NomorRekening1 = Convert.ToString(dr["NomorRekening1"]);
            M_FundClientSearchResult.InvestorTypeDesc = Convert.ToString(dr["InvestorTypeDesc"]);
            //    M_FundClientSearchResult.AgentName = Convert.ToString(dr["AgentName"]);
            M_FundClientSearchResult.EntryUsersID = dr["EntryUsersID"].ToString();
            M_FundClientSearchResult.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_FundClientSearchResult.EntryTime = dr["EntryTime"].ToString();
            M_FundClientSearchResult.UpdateTime = dr["UpdateTime"].ToString();
            M_FundClientSearchResult.LastUpdate = Convert.ToString(dr["Lastupdate"]);
            M_FundClientSearchResult.BitIsSuspend = Convert.ToBoolean(dr["BitIsSuspend"]);
            return M_FundClientSearchResult;
        }

        public List<FundClientSearchResult> FundClientSearch_Select(int _status, string _param, string _usersID)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<FundClientSearchResult> L_FundClient = new List<FundClientSearchResult>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 0;
                        cmd.CommandText = "SearchFundClientDormant";
                        cmd.Parameters.AddWithValue("@str", _param);
                        cmd.Parameters.AddWithValue("@status", _status);
                        //cmd.Parameters.AddWithValue("@UsersID", _usersID);

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
                        cmd.Parameters.AddWithValue("@NatureOfBusinessInsti", _fundClient.NatureOfBusinessInsti);
                        cmd.Parameters.AddWithValue("@InternalCategoryPK", _fundClient.InternalCategoryPK);
                        cmd.Parameters.AddWithValue("@SellingAgentPK", _fundClient.SellingAgentPK);
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
                        cmd.Parameters.AddWithValue("@BICCode1", _fundClient.BICCode1);
                        cmd.Parameters.AddWithValue("@NamaNasabah1", _fundClient.NamaNasabah1);
                        cmd.Parameters.AddWithValue("@MataUang1", _fundClient.MataUang1);
                        cmd.Parameters.AddWithValue("@NamaBank2", _fundClient.NamaBank2);
                        cmd.Parameters.AddWithValue("@NomorRekening2", _fundClient.NomorRekening2);
                        cmd.Parameters.AddWithValue("@BICCode2", _fundClient.BICCode2);
                        cmd.Parameters.AddWithValue("@NamaNasabah2", _fundClient.NamaNasabah2);
                        cmd.Parameters.AddWithValue("@MataUang2", _fundClient.MataUang2);
                        cmd.Parameters.AddWithValue("@NamaBank3", _fundClient.NamaBank3);
                        cmd.Parameters.AddWithValue("@NomorRekening3", _fundClient.NomorRekening3);
                        cmd.Parameters.AddWithValue("@BICCode3", _fundClient.BICCode3);
                        cmd.Parameters.AddWithValue("@NamaNasabah3", _fundClient.NamaNasabah3);
                        cmd.Parameters.AddWithValue("@MataUang3", _fundClient.MataUang3);
                        cmd.Parameters.AddWithValue("@NamaDepanInd", _fundClient.NamaDepanInd);
                        cmd.Parameters.AddWithValue("@NamaTengahInd", _fundClient.NamaTengahInd);
                        cmd.Parameters.AddWithValue("@NamaBelakangInd", _fundClient.NamaBelakangInd);
                        cmd.Parameters.AddWithValue("@TempatLahir", _fundClient.TempatLahir);
                        cmd.Parameters.AddWithValue("@TanggalLahir", _fundClient.TanggalLahir);
                        cmd.Parameters.AddWithValue("@JenisKelamin", _fundClient.JenisKelamin);
                        cmd.Parameters.AddWithValue("@StatusPerkawinan", _fundClient.StatusPerkawinan);
                        cmd.Parameters.AddWithValue("@Pekerjaan", _fundClient.Pekerjaan);
                        cmd.Parameters.AddWithValue("@Pendidikan", _fundClient.Pendidikan);
                        cmd.Parameters.AddWithValue("@Agama", _fundClient.Agama);
                        cmd.Parameters.AddWithValue("@PenghasilanInd", _fundClient.PenghasilanInd);
                        cmd.Parameters.AddWithValue("@SumberDanaInd", _fundClient.SumberDanaInd);
                        cmd.Parameters.AddWithValue("@MaksudTujuanInd", _fundClient.MaksudTujuanInd);
                        cmd.Parameters.AddWithValue("@AlamatInd1", _fundClient.AlamatInd1);
                        cmd.Parameters.AddWithValue("@KodeKotaInd1", _fundClient.KodeKotaInd1);
                        cmd.Parameters.AddWithValue("@KodePosInd1", _fundClient.KodePosInd1);
                        cmd.Parameters.AddWithValue("@AlamatInd2", _fundClient.AlamatInd2);
                        cmd.Parameters.AddWithValue("@KodeKotaInd2", _fundClient.KodeKotaInd2);
                        cmd.Parameters.AddWithValue("@KodePosInd2", _fundClient.KodePosInd2);
                        cmd.Parameters.AddWithValue("@NamaPerusahaan", _fundClient.NamaPerusahaan);
                        cmd.Parameters.AddWithValue("@Domisili", _fundClient.Domisili);
                        cmd.Parameters.AddWithValue("@Tipe", _fundClient.Tipe);
                        cmd.Parameters.AddWithValue("@Karakteristik", _fundClient.Karakteristik);
                        cmd.Parameters.AddWithValue("@NoSKD", _fundClient.NoSKD);
                        cmd.Parameters.AddWithValue("@PenghasilanInstitusi", _fundClient.PenghasilanInstitusi);
                        cmd.Parameters.AddWithValue("@SumberDanaInstitusi", _fundClient.SumberDanaInstitusi);
                        cmd.Parameters.AddWithValue("@MaksudTujuanInstitusi", _fundClient.MaksudTujuanInstitusi);
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
                        cmd.Parameters.AddWithValue("@PolitisLainnya", _fundClient.PolitisLainnya);
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
                        cmd.Parameters.AddWithValue("@CompanyTypeOJK", _fundClient.CompanyTypeOJK);
                        cmd.Parameters.AddWithValue("@BusinessTypeOJK", _fundClient.BusinessTypeOJK);
                        cmd.Parameters.AddWithValue("@BIMemberCode1", _fundClient.BIMemberCode1);
                        cmd.Parameters.AddWithValue("@BIMemberCode2", _fundClient.BIMemberCode2);
                        cmd.Parameters.AddWithValue("@BIMemberCode3", _fundClient.BIMemberCode3);
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

                        cmd.Parameters.AddWithValue("@CountryofCorrespondence", _fundClient.CountryofCorrespondence);
                        cmd.Parameters.AddWithValue("@CountryofDomicile", _fundClient.CountryofDomicile);
                        cmd.Parameters.AddWithValue("@SIUPExpirationDate", _fundClient.SIUPExpirationDate);
                        cmd.Parameters.AddWithValue("@CountryofEstablishment", _fundClient.CountryofEstablishment);
                        cmd.Parameters.AddWithValue("@CompanyCityName", _fundClient.CompanyCityName);
                        cmd.Parameters.AddWithValue("@CountryofCompany", _fundClient.CountryofCompany);
                        cmd.Parameters.AddWithValue("@NPWPPerson1", _fundClient.NPWPPerson1);
                        cmd.Parameters.AddWithValue("@NPWPPerson2", _fundClient.NPWPPerson2);

                        cmd.Parameters.AddWithValue("@BankCountry1", _fundClient.BankCountry1);
                        cmd.Parameters.AddWithValue("@BankCountry2", _fundClient.BankCountry2);
                        cmd.Parameters.AddWithValue("@BankCountry3", _fundClient.BankCountry3);
                        // RDN
                        cmd.Parameters.AddWithValue("@BankRDNPK", _fundClient.BankRDNPK);
                        cmd.Parameters.AddWithValue("@RDNAccountNo", _fundClient.RDNAccountNo);
                        cmd.Parameters.AddWithValue("@RDNAccountName", _fundClient.RDNAccountName);
                        cmd.Parameters.AddWithValue("@RiskProfileScore", _fundClient.RiskProfileScore);
                        cmd.Parameters.AddWithValue("@KYCRiskProfile", _fundClient.KYCRiskProfile);

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

                            cmd.CommandText = "Update FundClient set status=2,Notes=@Notes,ID=@ID,Name=@Name," +
                            "ClientCategory=@ClientCategory ,InvestorType=@InvestorType ,NatureOfBusinessInsti=@NatureOfBusinessInsti ,InternalCategoryPK=@InternalCategoryPK ,SellingAgentPK=@SellingAgentPK ,SID=@SID ,IFUACode=@IFUACode ,Child=@Child , " +
                            "ARIA=@ARIA ,Registered=@Registered ,JumlahDanaAwal=@JumlahDanaAwal ,JumlahDanaSaatIniCash=@JumlahDanaSaatIniCash ,JumlahDanaSaatIni=@JumlahDanaSaatIni , " +
                            "Negara=@Negara ,Nationality=@Nationality ,NPWP=@NPWP, SACode=@SACode,Propinsi=@Propinsi ,TeleponSelular=@TeleponSelular , " +
                            "Email=@Email ,Fax=@Fax ,DormantDate=@DormantDate ,Description=@Description ,JumlahBank=@JumlahBank ,NamaBank1=@NamaBank1 , " +
                            "NomorRekening1=@NomorRekening1 ,BICCode1=@BICCode1 ,NamaNasabah1=@NamaNasabah1 ,MataUang1=@MataUang1 ,NamaBank2=@NamaBank2 , " +
                            "NomorRekening2=@NomorRekening2 ,BICCode2=@BICCode2 ,NamaNasabah2=@NamaNasabah2 ,MataUang2=@MataUang2 ,NamaBank3=@NamaBank3 , " +
                            "NomorRekening3=@NomorRekening3 ,BICCode3=@BICCode3 ,NamaNasabah3=@NamaNasabah3 ,MataUang3=@MataUang3 ,NamaDepanInd=@NamaDepanInd , " +
                            "NamaTengahInd=@NamaTengahInd ,NamaBelakangInd=@NamaBelakangInd ,TempatLahir=@TempatLahir ,TanggalLahir=@TanggalLahir ,JenisKelamin=@JenisKelamin , " +
                            "StatusPerkawinan=@StatusPerkawinan ,Pekerjaan=@Pekerjaan ,Pendidikan=@Pendidikan ,Agama=@Agama ,PenghasilanInd=@PenghasilanInd , " +
                            "SumberDanaInd=@SumberDanaInd ,MaksudTujuanInd=@MaksudTujuanInd ,AlamatInd1=@AlamatInd1 ,KodeKotaInd1=@KodeKotaInd1 ,KodePosInd1=@KodePosInd1 , " +
                            "AlamatInd2=@AlamatInd2 ,KodeKotaInd2=@KodeKotaInd2 ,KodePosInd2=@KodePosInd2 ,NamaPerusahaan=@NamaPerusahaan ,Domisili=@Domisili , " +
                            "Tipe=@Tipe ,Karakteristik=@Karakteristik ,NoSKD=@NoSKD ,PenghasilanInstitusi=@PenghasilanInstitusi ,SumberDanaInstitusi=@SumberDanaInstitusi , " +
                            "MaksudTujuanInstitusi=@MaksudTujuanInstitusi ,AlamatPerusahaan=@AlamatPerusahaan ,KodeKotaIns=@KodeKotaIns ,KodePosIns=@KodePosIns ,SpouseName=@SpouseName ,MotherMaidenName=@MotherMaidenName, " +
                            "AhliWaris=@AhliWaris ,HubunganAhliWaris=@HubunganAhliWaris ,NatureOfBusiness=@NatureOfBusiness ,NatureOfBusinessLainnya=@NatureOfBusinessLainnya ,Politis=@Politis , " +
                            "PolitisLainnya=@PolitisLainnya ,TeleponRumah=@TeleponRumah ,OtherAlamatInd1=@OtherAlamatInd1 ,OtherKodeKotaInd1=@OtherKodeKotaInd1 ,OtherKodePosInd1=@OtherKodePosInd1 , " +
                            "OtherPropinsiInd1=@OtherPropinsiInd1, CountryOfBirth=@CountryOfBirth ,OtherNegaraInd1=@OtherNegaraInd1 ,OtherAlamatInd2=@OtherAlamatInd2 ,OtherKodeKotaInd2=@OtherKodeKotaInd2 ,OtherKodePosInd2=@OtherKodePosInd2 , " +
                            "OtherPropinsiInd2=@OtherPropinsiInd2 ,OtherNegaraInd2=@OtherNegaraInd2 ,OtherAlamatInd3=@OtherAlamatInd3 ,OtherKodeKotaInd3=@OtherKodeKotaInd3 ,OtherKodePosInd3=@OtherKodePosInd3 , " +
                            "OtherPropinsiInd3=@OtherPropinsiInd3 ,OtherNegaraInd3=@OtherNegaraInd3 ,OtherTeleponRumah=@OtherTeleponRumah ,OtherTeleponSelular=@OtherTeleponSelular ,OtherEmail=@OtherEmail , " +
                            "OtherFax=@OtherFax ,JumlahIdentitasInd=@JumlahIdentitasInd ,IdentitasInd1=@IdentitasInd1 ,NoIdentitasInd1=@NoIdentitasInd1 ,RegistrationDateIdentitasInd1=@RegistrationDateIdentitasInd1 , " +
                            "ExpiredDateIdentitasInd1=@ExpiredDateIdentitasInd1 ,IdentitasInd2=@IdentitasInd2 ,NoIdentitasInd2=@NoIdentitasInd2 ,RegistrationDateIdentitasInd2=@RegistrationDateIdentitasInd2 ,ExpiredDateIdentitasInd2=@ExpiredDateIdentitasInd2 , " +
                            "IdentitasInd3=@IdentitasInd3 ,NoIdentitasInd3=@NoIdentitasInd3 ,RegistrationDateIdentitasInd3=@RegistrationDateIdentitasInd3 ,ExpiredDateIdentitasInd3=@ExpiredDateIdentitasInd3 ,IdentitasInd4=@IdentitasInd4 , " +
                            "NoIdentitasInd4=@NoIdentitasInd4 ,RegistrationDateIdentitasInd4=@RegistrationDateIdentitasInd4 ,ExpiredDateIdentitasInd4=@ExpiredDateIdentitasInd4 ,RegistrationNPWP=@RegistrationNPWP , " +
                            "ExpiredDateSKD=@ExpiredDateSKD ,TanggalBerdiri=@TanggalBerdiri ,LokasiBerdiri=@LokasiBerdiri ,TeleponBisnis=@TeleponBisnis ,NomorAnggaran=@NomorAnggaran , " +
                            "NomorSIUP=@NomorSIUP ,AssetFor1Year=@AssetFor1Year ,AssetFor2Year=@AssetFor2Year ,AssetFor3Year=@AssetFor3Year ,OperatingProfitFor1Year=@OperatingProfitFor1Year , " +
                            "OperatingProfitFor2Year=@OperatingProfitFor2Year ,OperatingProfitFor3Year=@OperatingProfitFor3Year ,JumlahPejabat=@JumlahPejabat ,NamaDepanIns1=@NamaDepanIns1 ,NamaTengahIns1=@NamaTengahIns1 , " +
                            "NamaBelakangIns1=@NamaBelakangIns1 ,Jabatan1=@Jabatan1 ,JumlahIdentitasIns1=@JumlahIdentitasIns1 ,IdentitasIns11=@IdentitasIns11 ,NoIdentitasIns11=@NoIdentitasIns11 , " +
                            "RegistrationDateIdentitasIns11=@RegistrationDateIdentitasIns11 ,ExpiredDateIdentitasIns11=@ExpiredDateIdentitasIns11 ,IdentitasIns12=@IdentitasIns12 ,NoIdentitasIns12=@NoIdentitasIns12 ,RegistrationDateIdentitasIns12=@RegistrationDateIdentitasIns12 , " +
                            "ExpiredDateIdentitasIns12=@ExpiredDateIdentitasIns12 ,IdentitasIns13=@IdentitasIns13 ,NoIdentitasIns13=@NoIdentitasIns13 ,RegistrationDateIdentitasIns13=@RegistrationDateIdentitasIns13 ,ExpiredDateIdentitasIns13=@ExpiredDateIdentitasIns13 , " +
                            "IdentitasIns14=@IdentitasIns14 ,NoIdentitasIns14=@NoIdentitasIns14 ,RegistrationDateIdentitasIns14=@RegistrationDateIdentitasIns14 ,ExpiredDateIdentitasIns14=@ExpiredDateIdentitasIns14 ,NamaDepanIns2=@NamaDepanIns2 , " +
                            "NamaTengahIns2=@NamaTengahIns2 ,NamaBelakangIns2=@NamaBelakangIns2 ,Jabatan2=@Jabatan2 ,JumlahIdentitasIns2=@JumlahIdentitasIns2 ,IdentitasIns21=@IdentitasIns21 , " +
                            "NoIdentitasIns21=@NoIdentitasIns21 ,RegistrationDateIdentitasIns21=@RegistrationDateIdentitasIns21 ,ExpiredDateIdentitasIns21=@ExpiredDateIdentitasIns21 ,IdentitasIns22=@IdentitasIns22 ,NoIdentitasIns22=@NoIdentitasIns22 , " +
                            "RegistrationDateIdentitasIns22=@RegistrationDateIdentitasIns22 ,ExpiredDateIdentitasIns22=@ExpiredDateIdentitasIns22 ,IdentitasIns23=@IdentitasIns23 ,NoIdentitasIns23=@NoIdentitasIns23 ,RegistrationDateIdentitasIns23=@RegistrationDateIdentitasIns23 , " +
                            "ExpiredDateIdentitasIns23=@ExpiredDateIdentitasIns23 ,IdentitasIns24=@IdentitasIns24 ,NoIdentitasIns24=@NoIdentitasIns24 ,RegistrationDateIdentitasIns24=@RegistrationDateIdentitasIns24 ,ExpiredDateIdentitasIns24=@ExpiredDateIdentitasIns24 , " +
                            "NamaDepanIns3=@NamaDepanIns3 ,NamaTengahIns3=@NamaTengahIns3 ,NamaBelakangIns3=@NamaBelakangIns3 ,Jabatan3=@Jabatan3 ,JumlahIdentitasIns3=@JumlahIdentitasIns3 , " +
                            "IdentitasIns31=@IdentitasIns31 ,NoIdentitasIns31=@NoIdentitasIns31 ,RegistrationDateIdentitasIns31=@RegistrationDateIdentitasIns31 ,ExpiredDateIdentitasIns31=@ExpiredDateIdentitasIns31 ,IdentitasIns32=@IdentitasIns32 , " +
                            "NoIdentitasIns32=@NoIdentitasIns32 ,RegistrationDateIdentitasIns32=@RegistrationDateIdentitasIns32 ,ExpiredDateIdentitasIns32=@ExpiredDateIdentitasIns32 ,IdentitasIns33=@IdentitasIns33 ,NoIdentitasIns33=@NoIdentitasIns33 , " +
                            "RegistrationDateIdentitasIns33=@RegistrationDateIdentitasIns33 ,ExpiredDateIdentitasIns33=@ExpiredDateIdentitasIns33 ,IdentitasIns34=@IdentitasIns34 ,NoIdentitasIns34=@NoIdentitasIns34 ,RegistrationDateIdentitasIns34=@RegistrationDateIdentitasIns34 , " +
                            "ExpiredDateIdentitasIns34=@ExpiredDateIdentitasIns34 ,NamaDepanIns4=@NamaDepanIns4 ,NamaTengahIns4=@NamaTengahIns4 ,NamaBelakangIns4=@NamaBelakangIns4 ,Jabatan4=@Jabatan4 , " +
                            "JumlahIdentitasIns4=@JumlahIdentitasIns4 ,IdentitasIns41=@IdentitasIns41 ,NoIdentitasIns41=@NoIdentitasIns41 ,RegistrationDateIdentitasIns41=@RegistrationDateIdentitasIns41 ,ExpiredDateIdentitasIns41=@ExpiredDateIdentitasIns41 , " +
                            "IdentitasIns42=@IdentitasIns42 ,NoIdentitasIns42=@NoIdentitasIns42 ,RegistrationDateIdentitasIns42=@RegistrationDateIdentitasIns42 ,ExpiredDateIdentitasIns42=@ExpiredDateIdentitasIns42 ,IdentitasIns43=@IdentitasIns43 , " +
                            "NoIdentitasIns43=@NoIdentitasIns43 ,RegistrationDateIdentitasIns43=@RegistrationDateIdentitasIns43 ,ExpiredDateIdentitasIns43=@ExpiredDateIdentitasIns43 ,IdentitasIns44=@IdentitasIns44 ,NoIdentitasIns44=@NoIdentitasIns44 , " +
                            "RegistrationDateIdentitasIns44=@RegistrationDateIdentitasIns44 ,ExpiredDateIdentitasIns44=@ExpiredDateIdentitasIns44,CompanyTypeOJK=@CompanyTypeOJK,BusinessTypeOJK=@BusinessTypeOJK,BIMemberCode1=@BIMemberCode1,BIMemberCode2=@BIMemberCode2,BIMemberCode3=@BIMemberCode3, " +
                            "PhoneIns1=@PhoneIns1,EmailIns1=@EmailIns1,PhoneIns2=@PhoneIns2,EmailIns2=@EmailIns2,InvestorsRiskProfile=@InvestorsRiskProfile,AssetOwner=@AssetOwner,StatementType=@StatementType,FATCA=@FATCA,TIN=@TIN," +
                            "TINIssuanceCountry=@TINIssuanceCountry,GIIN=@GIIN,SubstantialOwnerName=@SubstantialOwnerName,SubstantialOwnerAddress=@SubstantialOwnerAddress,SubstantialOwnerTIN=@SubstantialOwnerTIN," +
                            "BankBranchName1=@BankBranchName1,BankBranchName2=@BankBranchName2,BankBranchName3=@BankBranchName3," +
                            "BankCountry1=@BankCountry1,BankCountry2=@BankCountry2,BankCountry3=@BankCountry3,OldID=@OldID," +
                            "CountryofCorrespondence=@CountryofCorrespondence, CountryofDomicile=@CountryofDomicile," +
                            "SIUPExpirationDate=@SIUPExpirationDate, CountryofEstablishment=@CountryofEstablishment, CompanyCityName=@CompanyCityName," +
                            "CountryofCompany=@CountryofCompany, NPWPPerson1=@NPWPPerson1, NPWPPerson2=@NPWPPerson2,BankRDNPK = @BankRDNPK,RDNAccountNo = @RDNAccountNo,RDNAccountName = @RDNAccountName,RiskProfileScore = @RiskProfileScore, KYCRiskProfile = @KYCRiskProfile," +
                            "ApprovedUsersID=@ApprovedUsersID, " +
                            "ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime ,LastUpdate=@LastUpdate " +
                            "where FundClientPK = @PK and historyPK = @HistoryPK";
                            cmd.Parameters.AddWithValue("@HistoryPK", _fundClient.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _fundClient.FundClientPK);
                            cmd.Parameters.AddWithValue("@ID", _fundClient.ID);
                            cmd.Parameters.AddWithValue("@Notes", _fundClient.Notes);
                            cmd.Parameters.AddWithValue("@Name", _fundClient.Name);
                            cmd.Parameters.AddWithValue("@ClientCategory", _fundClient.ClientCategory);
                            cmd.Parameters.AddWithValue("@InvestorType", _fundClient.InvestorType);
                            cmd.Parameters.AddWithValue("@NatureOfBusinessInsti", _fundClient.NatureOfBusinessInsti);
                            cmd.Parameters.AddWithValue("@InternalCategoryPK", _fundClient.InternalCategoryPK);
                            cmd.Parameters.AddWithValue("@SellingAgentPK", _fundClient.SellingAgentPK);
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
                            cmd.Parameters.AddWithValue("@BICCode1", _fundClient.BICCode1);
                            cmd.Parameters.AddWithValue("@NamaNasabah1", _fundClient.NamaNasabah1);
                            cmd.Parameters.AddWithValue("@MataUang1", _fundClient.MataUang1);
                            cmd.Parameters.AddWithValue("@NamaBank2", _fundClient.NamaBank2);
                            cmd.Parameters.AddWithValue("@NomorRekening2", _fundClient.NomorRekening2);
                            cmd.Parameters.AddWithValue("@BICCode2", _fundClient.BICCode2);
                            cmd.Parameters.AddWithValue("@NamaNasabah2", _fundClient.NamaNasabah2);
                            cmd.Parameters.AddWithValue("@MataUang2", _fundClient.MataUang2);
                            cmd.Parameters.AddWithValue("@NamaBank3", _fundClient.NamaBank3);
                            cmd.Parameters.AddWithValue("@NomorRekening3", _fundClient.NomorRekening3);
                            cmd.Parameters.AddWithValue("@BICCode3", _fundClient.BICCode3);
                            cmd.Parameters.AddWithValue("@NamaNasabah3", _fundClient.NamaNasabah3);
                            cmd.Parameters.AddWithValue("@MataUang3", _fundClient.MataUang3);
                            cmd.Parameters.AddWithValue("@NamaDepanInd", _fundClient.NamaDepanInd);
                            cmd.Parameters.AddWithValue("@NamaTengahInd", _fundClient.NamaTengahInd);
                            cmd.Parameters.AddWithValue("@NamaBelakangInd", _fundClient.NamaBelakangInd);
                            cmd.Parameters.AddWithValue("@TempatLahir", _fundClient.TempatLahir);
                            cmd.Parameters.AddWithValue("@TanggalLahir", _fundClient.TanggalLahir);
                            cmd.Parameters.AddWithValue("@JenisKelamin", _fundClient.JenisKelamin);
                            cmd.Parameters.AddWithValue("@StatusPerkawinan", _fundClient.StatusPerkawinan);
                            cmd.Parameters.AddWithValue("@Pekerjaan", _fundClient.Pekerjaan);
                            cmd.Parameters.AddWithValue("@Pendidikan", _fundClient.Pendidikan);
                            cmd.Parameters.AddWithValue("@Agama", _fundClient.Agama);
                            cmd.Parameters.AddWithValue("@PenghasilanInd", _fundClient.PenghasilanInd);
                            cmd.Parameters.AddWithValue("@SumberDanaInd", _fundClient.SumberDanaInd);
                            cmd.Parameters.AddWithValue("@MaksudTujuanInd", _fundClient.MaksudTujuanInd);
                            cmd.Parameters.AddWithValue("@AlamatInd1", _fundClient.AlamatInd1);
                            cmd.Parameters.AddWithValue("@KodeKotaInd1", _fundClient.KodeKotaInd1);
                            cmd.Parameters.AddWithValue("@KodePosInd1", _fundClient.KodePosInd1);
                            cmd.Parameters.AddWithValue("@AlamatInd2", _fundClient.AlamatInd2);
                            cmd.Parameters.AddWithValue("@KodeKotaInd2", _fundClient.KodeKotaInd2);
                            cmd.Parameters.AddWithValue("@KodePosInd2", _fundClient.KodePosInd2);
                            cmd.Parameters.AddWithValue("@NamaPerusahaan", _fundClient.NamaPerusahaan);
                            cmd.Parameters.AddWithValue("@Domisili", _fundClient.Domisili);
                            cmd.Parameters.AddWithValue("@Tipe", _fundClient.Tipe);
                            cmd.Parameters.AddWithValue("@Karakteristik", _fundClient.Karakteristik);
                            cmd.Parameters.AddWithValue("@NoSKD", _fundClient.NoSKD);
                            cmd.Parameters.AddWithValue("@PenghasilanInstitusi", _fundClient.PenghasilanInstitusi);
                            cmd.Parameters.AddWithValue("@SumberDanaInstitusi", _fundClient.SumberDanaInstitusi);
                            cmd.Parameters.AddWithValue("@MaksudTujuanInstitusi", _fundClient.MaksudTujuanInstitusi);
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
                            cmd.Parameters.AddWithValue("@PolitisLainnya", _fundClient.PolitisLainnya);
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
                            cmd.Parameters.AddWithValue("@CompanyTypeOJK", _fundClient.CompanyTypeOJK);
                            cmd.Parameters.AddWithValue("@BusinessTypeOJK", _fundClient.BusinessTypeOJK);
                            cmd.Parameters.AddWithValue("@BIMemberCode1", _fundClient.BIMemberCode1);
                            cmd.Parameters.AddWithValue("@BIMemberCode2", _fundClient.BIMemberCode2);
                            cmd.Parameters.AddWithValue("@BIMemberCode3", _fundClient.BIMemberCode3);
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

                            cmd.Parameters.AddWithValue("@RiskProfileScore", _fundClient.RiskProfileScore);
                            cmd.Parameters.AddWithValue("@KYCRiskProfile", _fundClient.KYCRiskProfile);

                            cmd.Parameters.AddWithValue("@OldID", _fundClient.OldID);
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

                                cmd.CommandText = "Update FundClient set Notes=@Notes,ID=@ID,Name=@Name," +
                            "ClientCategory=@ClientCategory ,InvestorType=@InvestorType,NatureOfBusinessInsti=@NatureOfBusinessInsti  ,InternalCategoryPK=@InternalCategoryPK ,SellingAgentPK=@SellingAgentPK ,SID=@SID ,IFUACode=@IFUACode ,Child=@Child , " +
                            "ARIA=@ARIA ,Registered=@Registered ,JumlahDanaAwal=@JumlahDanaAwal ,JumlahDanaSaatIniCash=@JumlahDanaSaatIniCash ,JumlahDanaSaatIni=@JumlahDanaSaatIni , " +
                            "Negara=@Negara ,Nationality=@Nationality ,NPWP=@NPWP ,SACode=@SACode ,Propinsi=@Propinsi ,TeleponSelular=@TeleponSelular , " +
                            "Email=@Email ,Fax=@Fax ,DormantDate=@DormantDate ,Description=@Description ,JumlahBank=@JumlahBank ,NamaBank1=@NamaBank1 , " +
                            "NomorRekening1=@NomorRekening1 ,BICCode1=@BICCode1 ,NamaNasabah1=@NamaNasabah1 ,MataUang1=@MataUang1 ,NamaBank2=@NamaBank2 , " +
                            "NomorRekening2=@NomorRekening2 ,BICCode2=@BICCode2 ,NamaNasabah2=@NamaNasabah2 ,MataUang2=@MataUang2 ,NamaBank3=@NamaBank3 , " +
                            "NomorRekening3=@NomorRekening3 ,BICCode3=@BICCode3 ,NamaNasabah3=@NamaNasabah3 ,MataUang3=@MataUang3 ,NamaDepanInd=@NamaDepanInd , " +
                            "NamaTengahInd=@NamaTengahInd ,NamaBelakangInd=@NamaBelakangInd ,TempatLahir=@TempatLahir ,TanggalLahir=@TanggalLahir ,JenisKelamin=@JenisKelamin , " +
                            "StatusPerkawinan=@StatusPerkawinan ,Pekerjaan=@Pekerjaan ,Pendidikan=@Pendidikan ,Agama=@Agama ,PenghasilanInd=@PenghasilanInd , " +
                            "SumberDanaInd=@SumberDanaInd ,MaksudTujuanInd=@MaksudTujuanInd ,AlamatInd1=@AlamatInd1 ,KodeKotaInd1=@KodeKotaInd1 ,KodePosInd1=@KodePosInd1 , " +
                            "AlamatInd2=@AlamatInd2 ,KodeKotaInd2=@KodeKotaInd2 ,KodePosInd2=@KodePosInd2 ,NamaPerusahaan=@NamaPerusahaan ,Domisili=@Domisili , " +
                            "Tipe=@Tipe ,Karakteristik=@Karakteristik ,NoSKD=@NoSKD ,PenghasilanInstitusi=@PenghasilanInstitusi ,SumberDanaInstitusi=@SumberDanaInstitusi , " +
                            "MaksudTujuanInstitusi=@MaksudTujuanInstitusi ,AlamatPerusahaan=@AlamatPerusahaan ,KodeKotaIns=@KodeKotaIns ,KodePosIns=@KodePosIns ,SpouseName=@SpouseName ,MotherMaidenName=@MotherMaidenName, " +
                            "AhliWaris=@AhliWaris ,HubunganAhliWaris=@HubunganAhliWaris ,NatureOfBusiness=@NatureOfBusiness ,NatureOfBusinessLainnya=@NatureOfBusinessLainnya ,Politis=@Politis , " +
                            "PolitisLainnya=@PolitisLainnya ,TeleponRumah=@TeleponRumah ,OtherAlamatInd1=@OtherAlamatInd1 ,OtherKodeKotaInd1=@OtherKodeKotaInd1 ,OtherKodePosInd1=@OtherKodePosInd1 , " +
                            "OtherPropinsiInd1=@OtherPropinsiInd1, CountryOfBirth=@CountryOfBirth ,OtherNegaraInd1=@OtherNegaraInd1 ,OtherAlamatInd2=@OtherAlamatInd2 ,OtherKodeKotaInd2=@OtherKodeKotaInd2 ,OtherKodePosInd2=@OtherKodePosInd2 , " +
                            "OtherPropinsiInd2=@OtherPropinsiInd2 ,OtherNegaraInd2=@OtherNegaraInd2 ,OtherAlamatInd3=@OtherAlamatInd3 ,OtherKodeKotaInd3=@OtherKodeKotaInd3 ,OtherKodePosInd3=@OtherKodePosInd3 , " +
                            "OtherPropinsiInd3=@OtherPropinsiInd3 ,OtherNegaraInd3=@OtherNegaraInd3 ,OtherTeleponRumah=@OtherTeleponRumah ,OtherTeleponSelular=@OtherTeleponSelular ,OtherEmail=@OtherEmail , " +
                            "OtherFax=@OtherFax ,JumlahIdentitasInd=@JumlahIdentitasInd ,IdentitasInd1=@IdentitasInd1 ,NoIdentitasInd1=@NoIdentitasInd1 ,RegistrationDateIdentitasInd1=@RegistrationDateIdentitasInd1 , " +
                            "ExpiredDateIdentitasInd1=@ExpiredDateIdentitasInd1 ,IdentitasInd2=@IdentitasInd2 ,NoIdentitasInd2=@NoIdentitasInd2 ,RegistrationDateIdentitasInd2=@RegistrationDateIdentitasInd2 ,ExpiredDateIdentitasInd2=@ExpiredDateIdentitasInd2 , " +
                            "IdentitasInd3=@IdentitasInd3 ,NoIdentitasInd3=@NoIdentitasInd3 ,RegistrationDateIdentitasInd3=@RegistrationDateIdentitasInd3 ,ExpiredDateIdentitasInd3=@ExpiredDateIdentitasInd3 ,IdentitasInd4=@IdentitasInd4 , " +
                            "NoIdentitasInd4=@NoIdentitasInd4 ,RegistrationDateIdentitasInd4=@RegistrationDateIdentitasInd4 ,ExpiredDateIdentitasInd4=@ExpiredDateIdentitasInd4 ,RegistrationNPWP=@RegistrationNPWP , " +
                            "ExpiredDateSKD=@ExpiredDateSKD ,TanggalBerdiri=@TanggalBerdiri ,LokasiBerdiri=@LokasiBerdiri ,TeleponBisnis=@TeleponBisnis ,NomorAnggaran=@NomorAnggaran , " +
                            "NomorSIUP=@NomorSIUP ,AssetFor1Year=@AssetFor1Year ,AssetFor2Year=@AssetFor2Year ,AssetFor3Year=@AssetFor3Year ,OperatingProfitFor1Year=@OperatingProfitFor1Year , " +
                            "OperatingProfitFor2Year=@OperatingProfitFor2Year ,OperatingProfitFor3Year=@OperatingProfitFor3Year ,JumlahPejabat=@JumlahPejabat ,NamaDepanIns1=@NamaDepanIns1 ,NamaTengahIns1=@NamaTengahIns1 , " +
                            "NamaBelakangIns1=@NamaBelakangIns1 ,Jabatan1=@Jabatan1 ,JumlahIdentitasIns1=@JumlahIdentitasIns1 ,IdentitasIns11=@IdentitasIns11 ,NoIdentitasIns11=@NoIdentitasIns11 , " +
                            "RegistrationDateIdentitasIns11=@RegistrationDateIdentitasIns11 ,ExpiredDateIdentitasIns11=@ExpiredDateIdentitasIns11 ,IdentitasIns12=@IdentitasIns12 ,NoIdentitasIns12=@NoIdentitasIns12 ,RegistrationDateIdentitasIns12=@RegistrationDateIdentitasIns12 , " +
                            "ExpiredDateIdentitasIns12=@ExpiredDateIdentitasIns12 ,IdentitasIns13=@IdentitasIns13 ,NoIdentitasIns13=@NoIdentitasIns13 ,RegistrationDateIdentitasIns13=@RegistrationDateIdentitasIns13 ,ExpiredDateIdentitasIns13=@ExpiredDateIdentitasIns13 , " +
                            "IdentitasIns14=@IdentitasIns14 ,NoIdentitasIns14=@NoIdentitasIns14 ,RegistrationDateIdentitasIns14=@RegistrationDateIdentitasIns14 ,ExpiredDateIdentitasIns14=@ExpiredDateIdentitasIns14 ,NamaDepanIns2=@NamaDepanIns2 , " +
                            "NamaTengahIns2=@NamaTengahIns2 ,NamaBelakangIns2=@NamaBelakangIns2 ,Jabatan2=@Jabatan2 ,JumlahIdentitasIns2=@JumlahIdentitasIns2 ,IdentitasIns21=@IdentitasIns21 , " +
                            "NoIdentitasIns21=@NoIdentitasIns21 ,RegistrationDateIdentitasIns21=@RegistrationDateIdentitasIns21 ,ExpiredDateIdentitasIns21=@ExpiredDateIdentitasIns21 ,IdentitasIns22=@IdentitasIns22 ,NoIdentitasIns22=@NoIdentitasIns22 , " +
                            "RegistrationDateIdentitasIns22=@RegistrationDateIdentitasIns22 ,ExpiredDateIdentitasIns22=@ExpiredDateIdentitasIns22 ,IdentitasIns23=@IdentitasIns23 ,NoIdentitasIns23=@NoIdentitasIns23 ,RegistrationDateIdentitasIns23=@RegistrationDateIdentitasIns23 , " +
                            "ExpiredDateIdentitasIns23=@ExpiredDateIdentitasIns23 ,IdentitasIns24=@IdentitasIns24 ,NoIdentitasIns24=@NoIdentitasIns24 ,RegistrationDateIdentitasIns24=@RegistrationDateIdentitasIns24 ,ExpiredDateIdentitasIns24=@ExpiredDateIdentitasIns24 , " +
                            "NamaDepanIns3=@NamaDepanIns3 ,NamaTengahIns3=@NamaTengahIns3 ,NamaBelakangIns3=@NamaBelakangIns3 ,Jabatan3=@Jabatan3 ,JumlahIdentitasIns3=@JumlahIdentitasIns3 , " +
                            "IdentitasIns31=@IdentitasIns31 ,NoIdentitasIns31=@NoIdentitasIns31 ,RegistrationDateIdentitasIns31=@RegistrationDateIdentitasIns31 ,ExpiredDateIdentitasIns31=@ExpiredDateIdentitasIns31 ,IdentitasIns32=@IdentitasIns32 , " +
                            "NoIdentitasIns32=@NoIdentitasIns32 ,RegistrationDateIdentitasIns32=@RegistrationDateIdentitasIns32 ,ExpiredDateIdentitasIns32=@ExpiredDateIdentitasIns32 ,IdentitasIns33=@IdentitasIns33 ,NoIdentitasIns33=@NoIdentitasIns33 , " +
                            "RegistrationDateIdentitasIns33=@RegistrationDateIdentitasIns33 ,ExpiredDateIdentitasIns33=@ExpiredDateIdentitasIns33 ,IdentitasIns34=@IdentitasIns34 ,NoIdentitasIns34=@NoIdentitasIns34 ,RegistrationDateIdentitasIns34=@RegistrationDateIdentitasIns34 , " +
                            "ExpiredDateIdentitasIns34=@ExpiredDateIdentitasIns34 ,NamaDepanIns4=@NamaDepanIns4 ,NamaTengahIns4=@NamaTengahIns4 ,NamaBelakangIns4=@NamaBelakangIns4 ,Jabatan4=@Jabatan4 , " +
                            "JumlahIdentitasIns4=@JumlahIdentitasIns4 ,IdentitasIns41=@IdentitasIns41 ,NoIdentitasIns41=@NoIdentitasIns41 ,RegistrationDateIdentitasIns41=@RegistrationDateIdentitasIns41 ,ExpiredDateIdentitasIns41=@ExpiredDateIdentitasIns41 , " +
                            "IdentitasIns42=@IdentitasIns42 ,NoIdentitasIns42=@NoIdentitasIns42 ,RegistrationDateIdentitasIns42=@RegistrationDateIdentitasIns42 ,ExpiredDateIdentitasIns42=@ExpiredDateIdentitasIns42 ,IdentitasIns43=@IdentitasIns43 , " +
                            "NoIdentitasIns43=@NoIdentitasIns43 ,RegistrationDateIdentitasIns43=@RegistrationDateIdentitasIns43 ,ExpiredDateIdentitasIns43=@ExpiredDateIdentitasIns43 ,IdentitasIns44=@IdentitasIns44 ,NoIdentitasIns44=@NoIdentitasIns44 , " +
                            "RegistrationDateIdentitasIns44=@RegistrationDateIdentitasIns44 ,ExpiredDateIdentitasIns44=@ExpiredDateIdentitasIns44,CompanyTypeOJK=@CompanyTypeOJK ,BusinessTypeOJK=@BusinessTypeOJK ,BIMemberCode1=@BIMemberCode1,BIMemberCode2=@BIMemberCode2,BIMemberCode3=@BIMemberCode3, " +
                            "PhoneIns1=@PhoneIns1,EmailIns1=@EmailIns1,PhoneIns2=@PhoneIns2,EmailIns2=@EmailIns2,InvestorsRiskProfile=@InvestorsRiskProfile,AssetOwner=@AssetOwner,StatementType=@StatementType,FATCA=@FATCA,TIN=@TIN," +
                            "TINIssuanceCountry=@TINIssuanceCountry,GIIN=@GIIN,SubstantialOwnerName=@SubstantialOwnerName,SubstantialOwnerAddress=@SubstantialOwnerAddress,SubstantialOwnerTIN=@SubstantialOwnerTIN," +
                            "BankBranchName1=@BankBranchName1,BankBranchName2=@BankBranchName2,BankBranchName3=@BankBranchName3," +
                            "BankCountry1=@BankCountry1,BankCountry2=@BankCountry2,BankCountry3=@BankCountry3," +
                           "CountryofCorrespondence=@CountryofCorrespondence,CountryofDomicile=@CountryofDomicile," +
                            "SIUPExpirationDate=@SIUPExpirationDate, CountryofEstablishment=@CountryofEstablishment, CompanyCityName=@CompanyCityName," +
                        "CountryofCompany=@CountryofCompany, NPWPPerson1=@NPWPPerson1, NPWPPerson2=@NPWPPerson2,BankRDNPK = @BankRDNPK,RDNAccountNo = @RDNAccountNo,RDNAccountName = @RDNAccountName,RiskProfileScore = @RiskProfileScore, KYCRiskProfile=@KYCRiskProfile," +
                            "UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@LastUpdate " +
                            "where FundClientPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _fundClient.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _fundClient.FundClientPK);
                                cmd.Parameters.AddWithValue("@ID", _fundClient.ID);
                                cmd.Parameters.AddWithValue("@Notes", _fundClient.Notes);
                                cmd.Parameters.AddWithValue("@Name", _fundClient.Name);
                                cmd.Parameters.AddWithValue("@NatureOfBusinessInsti", _fundClient.NatureOfBusinessInsti);
                                cmd.Parameters.AddWithValue("@ClientCategory", _fundClient.ClientCategory);
                                cmd.Parameters.AddWithValue("@InvestorType", _fundClient.InvestorType);
                                cmd.Parameters.AddWithValue("@InternalCategoryPK", _fundClient.InternalCategoryPK);
                                cmd.Parameters.AddWithValue("@SellingAgentPK", _fundClient.SellingAgentPK);
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
                                cmd.Parameters.AddWithValue("@BICCode1", _fundClient.BICCode1);
                                cmd.Parameters.AddWithValue("@NamaNasabah1", _fundClient.NamaNasabah1);
                                cmd.Parameters.AddWithValue("@MataUang1", _fundClient.MataUang1);
                                cmd.Parameters.AddWithValue("@NamaBank2", _fundClient.NamaBank2);
                                cmd.Parameters.AddWithValue("@NomorRekening2", _fundClient.NomorRekening2);
                                cmd.Parameters.AddWithValue("@BICCode2", _fundClient.BICCode2);
                                cmd.Parameters.AddWithValue("@NamaNasabah2", _fundClient.NamaNasabah2);
                                cmd.Parameters.AddWithValue("@MataUang2", _fundClient.MataUang2);
                                cmd.Parameters.AddWithValue("@NamaBank3", _fundClient.NamaBank3);
                                cmd.Parameters.AddWithValue("@NomorRekening3", _fundClient.NomorRekening3);
                                cmd.Parameters.AddWithValue("@BICCode3", _fundClient.BICCode3);
                                cmd.Parameters.AddWithValue("@NamaNasabah3", _fundClient.NamaNasabah3);
                                cmd.Parameters.AddWithValue("@MataUang3", _fundClient.MataUang3);
                                cmd.Parameters.AddWithValue("@NamaDepanInd", _fundClient.NamaDepanInd);
                                cmd.Parameters.AddWithValue("@NamaTengahInd", _fundClient.NamaTengahInd);
                                cmd.Parameters.AddWithValue("@NamaBelakangInd", _fundClient.NamaBelakangInd);
                                cmd.Parameters.AddWithValue("@TempatLahir", _fundClient.TempatLahir);
                                cmd.Parameters.AddWithValue("@TanggalLahir", _fundClient.TanggalLahir);
                                cmd.Parameters.AddWithValue("@JenisKelamin", _fundClient.JenisKelamin);
                                cmd.Parameters.AddWithValue("@StatusPerkawinan", _fundClient.StatusPerkawinan);
                                cmd.Parameters.AddWithValue("@Pekerjaan", _fundClient.Pekerjaan);
                                cmd.Parameters.AddWithValue("@Pendidikan", _fundClient.Pendidikan);
                                cmd.Parameters.AddWithValue("@Agama", _fundClient.Agama);
                                cmd.Parameters.AddWithValue("@PenghasilanInd", _fundClient.PenghasilanInd);
                                cmd.Parameters.AddWithValue("@SumberDanaInd", _fundClient.SumberDanaInd);
                                cmd.Parameters.AddWithValue("@MaksudTujuanInd", _fundClient.MaksudTujuanInd);
                                cmd.Parameters.AddWithValue("@AlamatInd1", _fundClient.AlamatInd1);
                                cmd.Parameters.AddWithValue("@KodeKotaInd1", _fundClient.KodeKotaInd1);
                                cmd.Parameters.AddWithValue("@KodePosInd1", _fundClient.KodePosInd1);
                                cmd.Parameters.AddWithValue("@AlamatInd2", _fundClient.AlamatInd2);
                                cmd.Parameters.AddWithValue("@KodeKotaInd2", _fundClient.KodeKotaInd2);
                                cmd.Parameters.AddWithValue("@KodePosInd2", _fundClient.KodePosInd2);
                                cmd.Parameters.AddWithValue("@NamaPerusahaan", _fundClient.NamaPerusahaan);
                                cmd.Parameters.AddWithValue("@Domisili", _fundClient.Domisili);
                                cmd.Parameters.AddWithValue("@Tipe", _fundClient.Tipe);
                                cmd.Parameters.AddWithValue("@Karakteristik", _fundClient.Karakteristik);
                                cmd.Parameters.AddWithValue("@NoSKD", _fundClient.NoSKD);
                                cmd.Parameters.AddWithValue("@PenghasilanInstitusi", _fundClient.PenghasilanInstitusi);
                                cmd.Parameters.AddWithValue("@SumberDanaInstitusi", _fundClient.SumberDanaInstitusi);
                                cmd.Parameters.AddWithValue("@MaksudTujuanInstitusi", _fundClient.MaksudTujuanInstitusi);
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
                                cmd.Parameters.AddWithValue("@PolitisLainnya", _fundClient.PolitisLainnya);
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
                                cmd.Parameters.AddWithValue("@CompanyTypeOJK", _fundClient.CompanyTypeOJK);
                                cmd.Parameters.AddWithValue("@BusinessTypeOJK", _fundClient.BusinessTypeOJK);
                                cmd.Parameters.AddWithValue("@BIMemberCode1", _fundClient.BIMemberCode1);
                                cmd.Parameters.AddWithValue("@BIMemberCode2", _fundClient.BIMemberCode2);
                                cmd.Parameters.AddWithValue("@BIMemberCode3", _fundClient.BIMemberCode3);
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
                                cmd.Parameters.AddWithValue("@RiskProfileScore", _fundClient.RiskProfileScore);
                                cmd.Parameters.AddWithValue("@KYCRiskProfile", _fundClient.KYCRiskProfile);

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
                                cmd.Parameters.AddWithValue("@NatureOfBusinessInsti", _fundClient.NatureOfBusinessInsti);
                                cmd.Parameters.AddWithValue("@ClientCategory", _fundClient.ClientCategory);
                                cmd.Parameters.AddWithValue("@InvestorType", _fundClient.InvestorType);
                                cmd.Parameters.AddWithValue("@InternalCategoryPK", _fundClient.InternalCategoryPK);
                                cmd.Parameters.AddWithValue("@SellingAgentPK", _fundClient.SellingAgentPK);
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
                                cmd.Parameters.AddWithValue("@BICCode1", _fundClient.BICCode1);
                                cmd.Parameters.AddWithValue("@NamaNasabah1", _fundClient.NamaNasabah1);
                                cmd.Parameters.AddWithValue("@MataUang1", _fundClient.MataUang1);
                                cmd.Parameters.AddWithValue("@NamaBank2", _fundClient.NamaBank2);
                                cmd.Parameters.AddWithValue("@NomorRekening2", _fundClient.NomorRekening2);
                                cmd.Parameters.AddWithValue("@BICCode2", _fundClient.BICCode2);
                                cmd.Parameters.AddWithValue("@NamaNasabah2", _fundClient.NamaNasabah2);
                                cmd.Parameters.AddWithValue("@MataUang2", _fundClient.MataUang2);
                                cmd.Parameters.AddWithValue("@NamaBank3", _fundClient.NamaBank3);
                                cmd.Parameters.AddWithValue("@NomorRekening3", _fundClient.NomorRekening3);
                                cmd.Parameters.AddWithValue("@BICCode3", _fundClient.BICCode3);
                                cmd.Parameters.AddWithValue("@NamaNasabah3", _fundClient.NamaNasabah3);
                                cmd.Parameters.AddWithValue("@MataUang3", _fundClient.MataUang3);
                                cmd.Parameters.AddWithValue("@NamaDepanInd", _fundClient.NamaDepanInd);
                                cmd.Parameters.AddWithValue("@NamaTengahInd", _fundClient.NamaTengahInd);
                                cmd.Parameters.AddWithValue("@NamaBelakangInd", _fundClient.NamaBelakangInd);
                                cmd.Parameters.AddWithValue("@TempatLahir", _fundClient.TempatLahir);
                                cmd.Parameters.AddWithValue("@TanggalLahir", _fundClient.TanggalLahir);
                                cmd.Parameters.AddWithValue("@JenisKelamin", _fundClient.JenisKelamin);
                                cmd.Parameters.AddWithValue("@StatusPerkawinan", _fundClient.StatusPerkawinan);
                                cmd.Parameters.AddWithValue("@Pekerjaan", _fundClient.Pekerjaan);
                                cmd.Parameters.AddWithValue("@Pendidikan", _fundClient.Pendidikan);
                                cmd.Parameters.AddWithValue("@Agama", _fundClient.Agama);
                                cmd.Parameters.AddWithValue("@PenghasilanInd", _fundClient.PenghasilanInd);
                                cmd.Parameters.AddWithValue("@SumberDanaInd", _fundClient.SumberDanaInd);
                                cmd.Parameters.AddWithValue("@MaksudTujuanInd", _fundClient.MaksudTujuanInd);
                                cmd.Parameters.AddWithValue("@AlamatInd1", _fundClient.AlamatInd1);
                                cmd.Parameters.AddWithValue("@KodeKotaInd1", _fundClient.KodeKotaInd1);
                                cmd.Parameters.AddWithValue("@KodePosInd1", _fundClient.KodePosInd1);
                                cmd.Parameters.AddWithValue("@AlamatInd2", _fundClient.AlamatInd2);
                                cmd.Parameters.AddWithValue("@KodeKotaInd2", _fundClient.KodeKotaInd2);
                                cmd.Parameters.AddWithValue("@KodePosInd2", _fundClient.KodePosInd2);
                                cmd.Parameters.AddWithValue("@NamaPerusahaan", _fundClient.NamaPerusahaan);
                                cmd.Parameters.AddWithValue("@Domisili", _fundClient.Domisili);
                                cmd.Parameters.AddWithValue("@Tipe", _fundClient.Tipe);
                                cmd.Parameters.AddWithValue("@Karakteristik", _fundClient.Karakteristik);
                                cmd.Parameters.AddWithValue("@NoSKD", _fundClient.NoSKD);
                                cmd.Parameters.AddWithValue("@PenghasilanInstitusi", _fundClient.PenghasilanInstitusi);
                                cmd.Parameters.AddWithValue("@SumberDanaInstitusi", _fundClient.SumberDanaInstitusi);
                                cmd.Parameters.AddWithValue("@MaksudTujuanInstitusi", _fundClient.MaksudTujuanInstitusi);
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
                                cmd.Parameters.AddWithValue("@PolitisLainnya", _fundClient.PolitisLainnya);
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
                                cmd.Parameters.AddWithValue("@CompanyTypeOJK", _fundClient.CompanyTypeOJK);
                                cmd.Parameters.AddWithValue("@BusinessTypeOJK", _fundClient.BusinessTypeOJK);
                                cmd.Parameters.AddWithValue("@BIMemberCode1", _fundClient.BIMemberCode1);
                                cmd.Parameters.AddWithValue("@BIMemberCode2", _fundClient.BIMemberCode2);
                                cmd.Parameters.AddWithValue("@BIMemberCode3", _fundClient.BIMemberCode3);
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
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _fundClient.EntryUsersID);

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
                                cmd.Parameters.AddWithValue("@RiskProfileScore", _fundClient.RiskProfileScore);
                                cmd.Parameters.AddWithValue("@KYCRiskProfile", _fundClient.KYCRiskProfile);

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

                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = 0;
                        cmd.CommandText = "HighRiskMonitoring_AddByFundClientApproved";
                        cmd.Parameters.AddWithValue("@FundClientPK", _fundClient.FundClientPK);
                        cmd.Parameters.AddWithValue("@UsersID", _fundClient.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"update FundClient set ID= @ID ,status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime ,LastUpdate=@LastUpdate, KYCRiskProfile = 1
                            where FundClientPK = @PK and historypk = @historyPK
                        

                            update FundClient set KYCRiskProfile = 3 
                            where FundClientPK = @PK and historypk = @historyPK
                            and (Negara in
                            (
                            'KP','IR','IQ','SY','BA','YE','ET','UG'
                            )
                            or Pekerjaan
                            in
                            (
                            '1','2','5','6'
                            )
                            or (Politis <> 0 and politis != 99)
                            or InvestorType = 2
                            )


                        
                        ";
                        cmd.Parameters.AddWithValue("@PK", _fundClient.FundClientPK);
                        cmd.Parameters.AddWithValue("@historyPK", _fundClient.HistoryPK);
                        cmd.Parameters.AddWithValue("@ID", _fundClient.ID);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _fundClient.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                        cmd.ExecuteNonQuery();
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
                        cmd.CommandText = "update FundClient set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where FundClientPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _fundClient.FundClientPK);
                        cmd.Parameters.AddWithValue("@historyPK", _fundClient.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _fundClient.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update FundClient set status= 2,LastUpdate=@LastUpdate where FundClientPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _fundClient.FundClientPK);
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
                        cmd.CommandText = "update FundClient set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime ,LastUpdate=@lastUpdate " +
                            "where FundClientPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _fundClient.FundClientPK);
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
                        cmd.CommandText = "SELECT  FundClientPK,ID + ' - ' + Name as ID, Name FROM [FundClient]  where status = 2";
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
                        cmd.CommandText = " select C.BankRecipientPK BankRecipientPK,C.Bank + ' - ' + C.B as AccountNo from ( " +
                         " select 1 BankRecipientPK,B.Name Bank,nomorrekening1 B from fundclient FC  " +
                         " left join Bank B on FC.namabank1 = B.BankPK where fundclientpk = @FundClientPK and FC.status  = 2    " +
                         " union all   " +
                         " select 2 BankRecipientPK,B.Name Bank,nomorrekening2 B from fundclient FC  " +
                         " left join Bank B on FC.namabank2 = B.BankPK where fundclientpk = @FundClientPK and FC.status  = 2  " +
                         " union all   " +
                         " select 3 BankRecipientPK,B.Name Bank,nomorrekening3 B from fundclient FC  " +
                         " left join Bank B on FC.namabank3 = B.BankPK where fundclientpk = @FundClientPK and FC.status  = 2  " +
                         " )C ";
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
                              select isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(FC.ID,'')))),'') + '|' 
	                          + isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(NamaDepanInd,'')))),'')  +    
                              '|' + isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(Case when NamaTengahInd  = '' then '0' else NamaTengahInd end))),'')  +       
                              '|' + isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(NamaBelakangInd,'0')))),'') + '|' +                
                              isnull(cast(IdentitasInd1 as nvarchar),'') + '|' 
	                          + isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(NoIdentitasInd1,'0')))),'') +         
                              '|' + isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(NPWP,0)))),'') + '|' 
	                          + isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(TempatLahir,'0')))),'') + '|'  +     
                              isnull(CONVERT(VARCHAR(8), TanggalLahir, 112) + '|' + cast(JenisKelamin as nvarchar),'') + '|'   +                       
                              isnull(cast(StatusPerkawinan as nvarchar),'') + '|' + case when Nationality = '85' then '1' else '2' end + '|' + isnull(cast(Pekerjaan as nvarchar),'')  +        
                              '|' + isnull(cast(Pendidikan as nvarchar),'') + '|' + isnull(cast(Agama as nvarchar),'') + '|' + isnull(cast(SumberDanaInd as nvarchar),'') + '|'  +    
                              isnull(cast(MaksudTujuanInd as nvarchar),'') + '|' + isnull(cast(PenghasilanInd as nvarchar),'') + '|' +                         
                              isnull(cast(RTRIM(LTRIM(isnull(AlamatInd1,'0'))) as nvarchar(100)),'') +           
                              '|' + isnull(replace(KodeKotaInd1,'.',''),'') + '|' + isnull(cast(KodePosInd1 as nvarchar),'') + '|'   +       
                              isnull(cast(RTRIM(LTRIM(isnull(AlamatInd2,''))) as nvarchar(100)),'')   +                      
                              '|' + isnull(replace(KodeKotaInd2,'.',''),'') + '|' + isnull(cast(KodePosInd2 as nvarchar),'')   +  
                              '|' + isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(SID,0)))),'')                    
                              from  FundClient FC                             
                              where InvestorType = '1'                         
                              and FC.FundClientPK in (select fundClientPK from FundClientPosition where Date = @ParamDate and UnitAmount >  0.0001   ) and FC.status = 2         
            
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
                              select isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(FC.ID))),'') + 
	                          '|' + isnull(RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(NamaPerusahaan,'0')))),'') + '|' +     
                              isnull(cast(rtrim(ltrim(Domisili)) as nvarchar),'') + '|' + isnull(cast(Tipe as nvarchar),'') +                     
                              '|' + isnull(cast(Karakteristik as nvarchar),'') + '|' + isnull(cast(rtrim(ltrim(dbo.AlphaRemoveExceptLetter(NPWP))) as nvarchar),'') +     
                              '|' + isnull(cast(rtrim(ltrim(dbo.AlphaRemoveExceptLetter(NoSKD))) as nvarchar),'') + '(' + isnull(CONVERT(VARCHAR(8), TanggalBerdiri, 112),'') + ')' +'|' +          
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
                                    string filePath = Tools.ARIATextPath + "IND002IND.rad";
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
                                        return Tools.HtmlARIATextPath + "IND002IND.rad";
                                    }
                                }
                                else
                                {
                                    string filePath = Tools.ARIATextPath + "IND002INS.rad";
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
                                        return Tools.HtmlARIATextPath + "IND002INS.rad";
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
                          select FC.FundClientPK , FC.Name FundClientName,FP.UnitAmount UnitAmount 
                          from FundClientPosition FP    
                          inner join FundClient FC on FP.FundClientPK  = FC.FundClientPK and FC.status = 2 
                          where FP.FundPK =@FundPK and FP.DATE  = (select MAX(Date) MaxDate from FundClientPosition 
                          where FundPK = @FundPK and Date <= @Date ) and FP.UnitAmount > 0 Order By FC.Name 
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
                                        M_FundClient.FundClientName = Convert.ToString(dr["FundClientName"]);
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

                        cmd.CommandText = @" Select isnull(UnitAmount,0) UnitAmount from fundClientPosition where Date = (Select max(date) from fundclientposition where Date  <=  @Date and FundPK = @FundPK and FundClientPK = @FundClientPK) 
                        and FundPK = @FundPK and FundClientPK = @FundClientPK ";

                        cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                        cmd.Parameters.AddWithValue("@Date", _date);
                        cmd.Parameters.AddWithValue("@FundClientPK", _fundClientPK);



                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return Convert.ToDecimal(dr["UnitAmount"]);

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
                    set @FinalDate = dbo.FWorkingDay(@Date,-1)

 
                    create table #Text(                    
                    [ResultText] [nvarchar](1000)  NULL                    
                )                     
                
                insert into #Text   
                 

              SELECT  RTRIM(LTRIM(isnull(FU.NKPDName,'')))             
                    + '|' + '0'         
                    + '|' + RTRIM(LTRIM(isnull(A.jumlahPerorangan,0)))
                    + '|' + CAST(CAST(isnull(B.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,10)) as NVARCHAR(30))                  
                    + '|' + RTRIM(LTRIM(isnull(C.jumlahPerusahaanEfek,0)))    
                    + '|' + CAST(CAST(isnull(D.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,10)) as NVARCHAR(30))                     
                    + '|' + RTRIM(LTRIM(isnull(E.jumlahDanaPensiun,0)))    
                    + '|' + CAST(CAST(isnull(F.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,10)) as NVARCHAR(30))                  
                    + '|' + RTRIM(LTRIM(isnull(G.jumlahAsuransi,0)))    
                    + '|' + CAST(CAST(isnull(H.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,10)) as NVARCHAR(30))                   
                    + '|' + RTRIM(LTRIM(isnull(I.jumlahBank,0)))        
                    + '|' + CAST(CAST(isnull(J.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,10)) as NVARCHAR(30))              
                    + '|' + RTRIM(LTRIM(isnull(K.jumlahPT,0)))     
                    + '|' + CAST(CAST(isnull(L.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,10)) as NVARCHAR(30))                    
                    + '|' + RTRIM(LTRIM(isnull(M.jumlahBUMN,0)))     
                    + '|' + CAST(CAST(isnull(N.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,10)) as NVARCHAR(30))                  
                    + '|' + RTRIM(LTRIM(isnull(O.jumlahBUMD,0)))        
                    + '|' + CAST(CAST(isnull(P.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,10)) as NVARCHAR(30))              
                    + '|' + RTRIM(LTRIM(isnull(Q.jumlahYayasan,0)))  
                    + '|' + CAST(CAST(isnull(R.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,10)) as NVARCHAR(30))                     
                    + '|' + RTRIM(LTRIM(isnull(S.jumlahKoperasi,0)))     
                    + '|' + CAST(CAST(isnull(T.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,10)) as NVARCHAR(30))                 
                    + '|' + RTRIM(LTRIM(isnull(U.jumlahLembagaNasionalLainnya,0)))
                    + '|' + CAST(CAST(isnull(V.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,10)) as NVARCHAR(30))             
    

			
             
                    ------ASING            
                    + '|' + RTRIM(LTRIM(isnull(AA.jumlahPeroranganAsing,0)))      
                    + '|' + CAST(CAST(isnull(AB.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,10)) as NVARCHAR(30))                   
                    + '|' + RTRIM(LTRIM(isnull(AC.jumlahPerusahaanEfekAsing,0)))     
                    + '|' + CAST(CAST(isnull(AD.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,10)) as NVARCHAR(30))                     
                    + '|' + RTRIM(LTRIM(isnull(AE.jumlahDanaPensiunAsing,0)))  
                    + '|' + CAST(CAST(isnull(AF.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,10)) as NVARCHAR(30))                   
                    + '|' + RTRIM(LTRIM(isnull(AG.jumlahAsuransiAsing,0)))        
                    + '|' + CAST(CAST(isnull(AH.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,10)) as NVARCHAR(30))                 
                    + '|' + RTRIM(LTRIM(isnull(AI.jumlahBankAsing,0)))   
                    + '|' + CAST(CAST(isnull(AJ.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,10)) as NVARCHAR(30))                        
                    + '|' + RTRIM(LTRIM(isnull(AK.jumlahPTAsing,0))) 
                    + '|' + CAST(CAST(isnull(AL.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,10)) as NVARCHAR(30))                         
                    + '|' + RTRIM(LTRIM(isnull(AM.jumlahBUMNAsing,0)))    
                    + '|' + CAST(CAST(isnull(AN.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,10)) as NVARCHAR(30))                     
                    + '|' + RTRIM(LTRIM(isnull(AO.jumlahBUMDAsing,0)))   
                    + '|' + CAST(CAST(isnull(AP.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,10)) as NVARCHAR(30))                       
                    + '|' + RTRIM(LTRIM(isnull(AQ.jumlahYayasanAsing,0)))  
                    + '|' + CAST(CAST(isnull(AR.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,10)) as NVARCHAR(30))                       
                    + '|' + RTRIM(LTRIM(isnull(SS.jumlahKoperasiAsing,0)))       
                    + '|' + CAST(CAST(isnull(AT.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,10)) as NVARCHAR(30))                   
                    + '|' + RTRIM(LTRIM(isnull(AU.jumlahLembagaAsingLainnya,0)))           
                    + '|' + CAST(CAST(isnull(AV.UnitAmount * dbo.FgetCloseNav(@date,FCP.FundPK),0) as DECIMAL(30,10)) as NVARCHAR(30)) 
	                + '|' + CASE WHEN (sum(FCP.UnitAmount) * dbo.FgetCloseNav(@date,FCP.FundPK)) > 0 
	                then  CAST(CAST(isnull((isnull(B.UnitAmount,0) + isnull(D.UnitAmount,0) + isnull(F.UnitAmount,0) + isnull(H.UnitAmount,0) 
	                + isnull(J.UnitAmount,0) + isnull(L.UnitAmount,0) + isnull(N.UnitAmount,0) + isnull(P.UnitAmount,0) + isnull(R.UnitAmount,0) 
	                + isnull(T.UnitAmount,0) + isnull(V.UnitAmount,0)) * dbo.FgetCloseNav(@date,FCP.FundPK),0) 
	                / (sum(FCP.UnitAmount) * dbo.FgetCloseNav(@date,FCP.FundPK)) * 100  as DECIMAL(30,10)) as NVARCHAR(30)) ELSE '0' END               
                    + '|' + CASE WHEN (sum(FCP.UnitAmount) * dbo.FgetCloseNav(@date,FCP.FundPK)) > 0 
	                then  CAST(CAST(isnull((isnull(AB.UnitAmount,0) + isnull(AD.UnitAmount,0) + isnull(AF.UnitAmount,0) + isnull(AH.UnitAmount,0) 
	                + isnull(AJ.UnitAmount,0) + isnull(AL.UnitAmount,0) + isnull(AN.UnitAmount,0) + isnull(AP.UnitAmount,0) + isnull(AR.UnitAmount,0) 
	                + isnull(AT.UnitAmount,0) + isnull(AV.UnitAmount,0)) * dbo.FgetCloseNav(@date,FCP.FundPK),0) 
	                / (sum(FCP.UnitAmount) * dbo.FgetCloseNav(@date,FCP.FundPK)) * 100  as DECIMAL(30,10)) as NVARCHAR(30)) ELSE '0' END   
	         
             
                    FROM FundClientPosition FCP (NOLOCK)                      
                    LEFT JOIN FundClient FC (NOLOCK) ON FCP.FundClientPK = FC.FundClientPK and FC.Status = 2           
                    LEFT JOIN Fund FU on FCP.FundPK = FU.FundPK and FU.Status = 2  
                    --LEFT JOIN FundCashRef FCR on FU.FundPK = FCR.FundPK and FCR.Status = 2         
                    LEFT JOIN             
                    (            
                    select COUNT (*) jumlahperorangan, CS.FundPK from FundClientPosition CS            
                    left join FundClient g            
                    on CS.FundClientPK = g.FundClientPK   and g.Status = 2         
                    where g.InvestorType = 1 and g.nationality= 85
                    and CS.Date = @FinalDate and CS.UnitAmount > 0            
            
                    group by CS.FundPK            
                    ) A On FCP.FundPK = A.FundPK            
             
                    LEFT JOIN             
                    (            
                    select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
                    left join FundClient CG        
                    on CS.FundClientPK=CG.FundClientPK    and CG.Status = 2        
                    where CG.InvestorType = 1 and CG.nationality= 85
                    and CS.Date = @FinalDate and CS.UnitAmount > 0             
            
                    group by CS.FundPK            
                    ) B On FCP.FundPK = B.FundPK            
             
                    LEFT JOIN             
                    (            
                    ----------EFEK----------------        
                    select COUNT(*) jumlahPerusahaanEfek, CS.FundPK from FundClientPosition CS            
                    left join FundClient CG            
                    on CS.FundClientPK = CG.FundClientPK and CG.Status = 2            
                    where CG.InvestorType = 2 and CG.Negara= 112            
                    and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe = 7
            
                    group by CS.FundPK            
                    ) C On FCP.FundPK = C.FundPK            
             
             
                    LEFT JOIN             
                    (            
                    select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
                    left join FundClient CG            
                    on CS.FundClientPK=CG.FundClientPK and CG.Status = 2            
                    where CG.InvestorType = 2 and CG.Negara= 112            
                    and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe = 7
            
                    group by CS.FundPK            
                    ) D On FCP.FundPK = D.FundPK            
             
             
                    LEFT JOIN             
                    (            
                    ---------DANA PENSIUN-------------        
                    select COUNT(*) jumlahDanaPensiun, CS.FundPK from FundClientPosition CS            
                    left join FundClient CG            
                    on CS.FundClientPK = CG.FundClientPK and CG.Status = 2            
                    where CG.InvestorType = 2 and CG.Negara= 112            
                    and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=6           
            
                    group by CS.FundPK            
                    ) E On FCP.FundPK = E.FundPK            
             
             
                    LEFT JOIN             
                    (            
                    select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
                    left join FundClient CG            
                    on CS.FundClientPK=CG.FundClientPK  and CG.Status = 2           
                    where CG.InvestorType = 2 and CG.Negara= 112            
                    and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=6          
            
                    group by CS.FundPK            
                    ) F On FCP.FundPK = F.FundPK            
             
                    LEFT JOIN             
                    (            
                    ----------ASURANSI-----------        
                    select COUNT(*) jumlahAsuransi, CS.FundPK from FundClientPosition CS            
                    left join FundClient CG            
                    on CS.FundClientPK = CG.FundClientPK and CG.Status = 2            
                    where CG.InvestorType = 2 and CG.Negara= 112            
                    and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe=4           
            
                    group by CS.FundPK            
                    ) G On FCP.FundPK = G.FundPK            
             
                    LEFT JOIN             
                    (            
                    select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
                    left join FundClient CG            
                    on CS.FundClientPK=CG.FundClientPK  and CG.Status = 2      
                    where CG.InvestorType = 2 and CG.Negara= 112            
                    and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=4            
            
                    group by CS.FundPK            
                    ) H On FCP.FundPK = H.FundPK            
             
                    LEFT JOIN             
                    (            
                    ------------BANK-----------        
                    select COUNT(*) jumlahBank, CS.FundPK from FundClientPosition CS            
                    left join FundClient CG            
                    on CS.FundClientPK = CG.FundClientPK  and CG.Status = 2           
                    where CG.InvestorType = 2 and CG.Negara= 112            
                    and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe=3 and CG.Karakteristik IN (1,2,3,4,5,6,7)             
            
                    group by CS.FundPK            
                    ) I On FCP.FundPK = I.FundPK            
             
                    LEFT JOIN             
                    (            
                    select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
                    left join FundClient CG            
                    on CS.FundClientPK=CG.FundClientPK  and CG.Status = 2           
                    where CG.InvestorType = 2 and CG.Negara= 112            
                    and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=3 and CG.Karakteristik IN (1,2,3,4,5,6,7)            
            
                    group by CS.FundPK            
                    ) J On FCP.FundPK = J.FundPK            
             
                LEFT JOIN             
                    (            
                    --------PEURSAHAAN SWASTA-----------        
                    select COUNT(*) jumlahPT, CS.FundPK from FundClientPosition CS            
                    left join FundClient CG            
                    on CS.FundClientPK = CG.FundClientPK  and CG.Status = 2           
                    where CG.InvestorType = 2 and CG.Negara= 112            
                    and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe=1 and CG.Karakteristik IN (2,3,4,5,6,7)          
            
                    group by CS.FundPK            
                    ) K On FCP.FundPK = K.FundPK            
             
                    LEFT JOIN             
                    (            
                    select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
                    left join FundClient CG            
                    on CS.FundClientPK=CG.FundClientPK  and CG.Status = 2           
                    where CG.InvestorType = 2 and CG.Negara= 112            
                    and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=1 and CG.Karakteristik IN (2,3,4,5,6,7)        
            
                    group by CS.FundPK            
                    ) L On FCP.FundPK = L.FundPK            
             
                    LEFT JOIN             
                    (            
                    ---------------BUMN----------------        
                    select COUNT(*) jumlahBUMN, CS.FundPK from FundClientPosition CS            
                    left join FundClient CG            
                    on CS.FundClientPK = CG.FundClientPK  and CG.Status = 2           
                    where CG.InvestorType = 2 and CG.Negara= 112            
                    and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe=1 and CG.Karakteristik = 1           
            
                    group by CS.FundPK            
                    ) M On FCP.FundPK = M.FundPK            
             
                    LEFT JOIN             
                    (            
                    select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
                    left join FundClient CG            
                    on CS.FundClientPK=CG.FundClientPK  and CG.Status = 2           
                    where CG.InvestorType = 2 and CG.Negara= 112            
                    and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe=1 and CG.Karakteristik = 1              
            
                    group by CS.FundPK            
                    ) N On FCP.FundPK = N.FundPK            
             
                    LEFT JOIN             
                    (            
                    -------------BUMD-------------        
                    select COUNT(*) jumlahBUMD, CS.FundPK from FundClientPosition CS            
                    left join FundClient CG            
                    on CS.FundClientPK = CG.FundClientPK and CG.Status = 2            
                    where CG.InvestorType = 2 and CG.Negara= 112            
                    and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe=1 and CG.Karakteristik = 8             
            
                    group by CS.FundPK            
                    ) O On FCP.FundPK = O.FundPK            
             
                    LEFT JOIN             
                    (            
                    select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
                    left join FundClient CG            
                    on CS.FundClientPK=CG.FundClientPK  and CG.Status = 2           
                    where CG.InvestorType = 2 and CG.Negara= 112            
                    and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe=1 and CG.Karakteristik = 8             
            
                    group by CS.FundPK            
                    ) P On FCP.FundPK = P.FundPK            
             
                    LEFT JOIN             
                    (            
                    -----YAYASAN-----------        
                    select COUNT(*) jumlahYayasan, CS.FundPK from FundClientPosition CS            
                    left join FundClient CG            
                    on CS.FundClientPK = CG.FundClientPK and CG.Status = 2            
                    where CG.InvestorType = 2 and CG.Negara= 112            
                    and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe=2            
            
                    group by CS.FundPK            
                    ) Q On FCP.FundPK = Q.FundPK            
             
                    LEFT JOIN             
                    (            
                    select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
                    left join FundClient CG            
                    on CS.FundClientPK=CG.FundClientPK and CG.Status = 2            
                    where CG.InvestorType = 2 and CG.Negara= 112            
                    and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe=2           
            
                    group by CS.FundPK            
                    ) R On FCP.FundPK = R.FundPK            
             
                    LEFT JOIN             
                    (            
                    ------------KOPERASI--------------        
                    select COUNT(*) jumlahKoperasi, CS.FundPK from FundClientPosition CS            
                    left join FundClient CG            
                    on CS.FundClientPK = CG.FundClientPK and CG.Status = 2            
                    where CG.InvestorType = 2 and CG.Negara= 112            
                    and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe= 3 and CG.Karakteristik = 8           
            
                    group by CS.FundPK            
                    ) S On FCP.FundPK = S.FundPK            
             
                    LEFT JOIN             
                    (            
                    select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
                    left join FundClient CG            
                    on CS.FundClientPK=CG.FundClientPK  and CG.Status = 2           
                    where CG.InvestorType = 2 and CG.Negara= 112            
                    and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe= 3 and CG.Karakteristik = 8         
            
                    group by CS.FundPK            
                    ) T On FCP.FundPK = T.FundPK    

					
                    ------------LEMBAGA LAINNYA--------------            
             
                    LEFT JOIN             
                    (            
                    select COUNT(*) jumlahLembagaNasionalLainnya, CS.FundPK from FundClientPosition CS            
                    left join FundClient CG            
                    on CS.FundClientPK = CG.FundClientPK and CG.Status = 2            
                    where CG.InvestorType = 2 and CG.Negara= 112            
                    and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe=8            
            
                    group by CS.FundPK            
                    ) U On FCP.FundPK = U.FundPK            
             
                    LEFT JOIN             
                    (            
                    select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
                    left join FundClient CG            
                    on CS.FundClientPK=CG.FundClientPK  and CG.Status = 2           
                    where CG.InvestorType = 2 and CG.Negara= 112            
                    and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe=8            
            
                    group by CS.FundPK            
                    ) V On FCP.FundPK = V.FundPK            
             
                    ----ASING            
             
                    LEFT JOIN             
                    (            
                    select COUNT(*) jumlahPeroranganAsing, CS.FundPK from FundClientPosition CS            
                    left join FundClient CG            
                    on CS.FundClientPK = CG.FundClientPK and CG.Status = 2            
                    where CG.InvestorType = 1 and CG.nationality <> 85
                    and CS.Date = @FinalDate and CS.UnitAmount > 0            
            
                    group by CS.FundPK            
                    ) AA On FCP.FundPK = AA.FundPK            
             
                    LEFT JOIN             
                    (            
                    select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
                    left join FundClient CG            
                    on CS.FundClientPK=CG.FundClientPK    and CG.Status = 2         
                    where CG.InvestorType = 1 and CG.nationality <> 85
                    and CS.Date = @FinalDate and CS.UnitAmount > 0             
            
                    group by CS.FundPK            
                    ) AB On FCP.FundPK = AB.FundPK            
             
                    LEFT JOIN             
                    (            
                    select COUNT(*) jumlahPerusahaanEfekAsing, CS.FundPK from FundClientPosition CS            
                    left join FundClient CG            
                    on CS.FundClientPK = CG.FundClientPK  and CG.Status = 2          
                    where CG.InvestorType = 2 and CG.Negara<> 112            
                    and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe IN (3,7)           
            
                    group by CS.FundPK          
                    ) AC On FCP.FundPK = AC.FundPK            
             
             
                    LEFT JOIN             
                    (            
                    select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
                    left join FundClient CG            
                    on CS.FundClientPK=CG.FundClientPK  and CG.Status = 2          
                    where CG.InvestorType = 2 and CG.Negara<> 112            
                    and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe IN (3,7)          
            
                    group by CS.FundPK            
                    ) AD On FCP.FundPK = AD.FundPK            
             
             
                    LEFT JOIN             
                    (            
                    select COUNT(*) jumlahDanaPensiunAsing, CS.FundPK from FundClientPosition CS            
                    left join FundClient CG            
                    on CS.FundClientPK = CG.FundClientPK and CG.Status = 2      
                    where CG.InvestorType = 2 and CG.Negara<> 112            
                    and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=6           
            
                    group by CS.FundPK            
                    ) AE On FCP.FundPK = AE.FundPK            
             
             
                    LEFT JOIN             
                    (            
                    select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
                    left join FundClient CG            
                    on CS.FundClientPK=CG.FundClientPK and CG.Status = 2           
                    where CG.InvestorType = 2 and CG.Negara<> 112            
                    and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=6           
            
                    group by CS.FundPK            
                    ) AF On FCP.FundPK = AF.FundPK            
             
                    LEFT JOIN             
                    (            
                    select COUNT(*) jumlahAsuransiAsing, CS.FundPK from FundClientPosition CS            
                    left join FundClient CG            
                    on CS.FundClientPK = CG.FundClientPK and CG.Status = 2           
                    where CG.InvestorType = 2 and CG.Negara<> 112            
                    and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe=4           
            
                    group by CS.FundPK            
                    ) AG On FCP.FundPK = AG.FundPK            
             
                    LEFT JOIN             
                    (            
                    select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
                    left join FundClient CG            
                    on CS.FundClientPK=CG.FundClientPK and CG.Status = 2           
                    where CG.InvestorType = 2 and CG.Negara<> 112            
                    and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=4            
            
                    group by CS.FundPK            
                    ) AH On FCP.FundPK = AH.FundPK            
             
                    LEFT JOIN             
                    (            
                    select COUNT(*) jumlahBankAsing, CS.FundPK from FundClientPosition CS            
                    left join FundClient CG            
                    on CS.FundClientPK = CG.FundClientPK and CG.Status = 2           
                    where CG.InvestorType = 2 and CG.Negara<> 112            
                    and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe=3 and CG.Karakteristik IN (1,2,3,4,5,6,7)          
            
                    group by CS.FundPK            
                    ) AI On FCP.FundPK = AI.FundPK            
             
                    LEFT JOIN             
                    (            
                    select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
                    left join FundClient CG            
                    on CS.FundClientPK=CG.FundClientPK  and CG.Status = 2          
                    where CG.InvestorType = 2 and CG.Negara<> 112            
                    and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=3 and CG.Karakteristik IN (1,2,3,4,5,6,7)            
            
                    group by CS.FundPK            
                    ) AJ On FCP.FundPK = AJ.FundPK            
             
                    LEFT JOIN             
                    (            
                    select COUNT(*) jumlahPTAsing, CS.FundPK from FundClientPosition CS            
                    left join FundClient CG            
                    on CS.FundClientPK = CG.FundClientPK  and CG.Status = 2          
                    where CG.InvestorType = 2 and CG.Negara<> 112            
                    and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.tipe=1 and CG.Karakteristik IN (2,3,4,5,6,7)           
            
                    group by CS.FundPK            
                    ) AK On FCP.FundPK = AK.FundPK            
             
                    LEFT JOIN             
                    (            
                    select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
                    left join FundClient CG            
                    on CS.FundClientPK=CG.FundClientPK  and CG.Status = 2          
                    where CG.InvestorType = 2 and CG.Negara<> 112            
                    and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=1 and CG.Karakteristik IN (2,3,4,5,6,7)             
            
                    group by CS.FundPK            
                    ) AL On FCP.FundPK = AL.FundPK            
             
                    LEFT JOIN             
                    (            
                    select COUNT(*) jumlahBUMNAsing, CS.FundPK from FundClientPosition CS            
                    left join FundClient CG            
                    on CS.FundClientPK = CG.FundClientPK   and CG.Status = 2         
                    where CG.InvestorType = 2 and CG.Negara<> 112            
                    and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=1 and CG.Karakteristik =1             
            
                    group by CS.FundPK            
                    ) AM On FCP.FundPK = AM.FundPK            
             
                    LEFT JOIN             
                    (            
                    select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
                    left join FundClient CG            
                    on CS.FundClientPK=CG.FundClientPK  and CG.Status = 2          
                    where CG.InvestorType = 2 and CG.Negara<> 112            
                    and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=1 and CG.Karakteristik =1           
            
                    group by CS.FundPK            
                    ) AN On FCP.FundPK = AN.FundPK            
             
                    LEFT JOIN             
                    (            
                    select COUNT(*) jumlahBUMDAsing, CS.FundPK from FundClientPosition CS            
                    left join FundClient CG            
                    on CS.FundClientPK = CG.FundClientPK and CG.Status = 2           
                    where CG.InvestorType = 2 and CG.Negara<>112            
                    and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=1 and CG.Karakteristik = 8         
            
                    group by CS.FundPK            
                    ) AO On FCP.FundPK = AO.FundPK            
             
                    LEFT JOIN             
                    (            
                    select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
                    left join FundClient CG            
                    on CS.FundClientPK=CG.FundClientPK and CG.Status = 2           
                    where CG.InvestorType = 2 and CG.Negara<> 112            
                    and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=1 and CG.Karakteristik = 8            
            
                    group by CS.FundPK            
                    ) AP On FCP.FundPK = AP.FundPK            
             
                    LEFT JOIN             
                    (            
                    select COUNT(*) jumlahYayasanAsing, CS.FundPK from FundClientPosition CS            
                    left join FundClient CG            
                    on CS.FundClientPK = CG.FundClientPK and CG.Status = 2           
                    where CG.InvestorType = 2 and CG.Negara<> 112          
                    and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=2           
            
                    group by CS.FundPK            
                    ) AQ On FCP.FundPK = AQ.FundPK            
             
                    LEFT JOIN             
                    (            
                    select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
                    left join FundClient CG            
                    on CS.FundClientPK=CG.FundClientPK  and CG.Status = 2          
                    where CG.InvestorType = 2 and CG.Negara<> 112            
                    and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=2            
            
                    group by CS.FundPK            
                    ) AR On FCP.FundPK = AR.FundPK            
             
                    LEFT JOIN             
                    (            
                    select COUNT(*) jumlahKoperasiAsing, CS.FundPK from FundClientPosition CS            
                    left join FundClient CG            
                    on CS.FundClientPK = CG.FundClientPK  and CG.Status = 2          
                    where CG.InvestorType = 2 and CG.Negara<> 112            
                    and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe= 3 and CG.Karakteristik = 8             
            
                    group by CS.FundPK            
                    ) SS On FCP.FundPK = SS.FundPK            
             
                    LEFT JOIN             
                    (            
                    select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
                    left join FundClient CG            
                    on CS.FundClientPK=CG.FundClientPK and CG.Status = 2           
                    where CG.InvestorType = 2 and CG.Negara<> 112            
                    and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe= 3 and CG.Karakteristik = 8            
            
                    group by CS.FundPK            
                    ) AT On FCP.FundPK = AT.FundPK            
             
                    LEFT JOIN             
                    (            
                    select COUNT(*) jumlahLembagaAsingLainnya, CS.FundPK from FundClientPosition CS            
                    left join FundClient CG            
                    on CS.FundClientPK = CG.FundClientPK and CG.Status = 2           
                    where CG.InvestorType = 2 and CG.Negara<> 112            
                    and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=8            
            
                    group by CS.FundPK            
                    ) AU On FCP.FundPK = AU.FundPK            
             
                    LEFT JOIN             
                    (            
                    select SUM (UnitAmount) UnitAmount, CS.FundPK from FundClientPosition CS            
                    left join FundClient CG            
                    on CS.FundClientPK=CG.FundClientPK  and CG.Status = 2          
                    where CG.InvestorType = 2 and CG.Negara<> 112            
                    and CS.Date = @FinalDate and CS.UnitAmount > 0 and CG.Tipe=8            
            
                    group by CS.FundPK            
                    ) AV On FCP.FundPK = AV.FundPK            
             
                    WHERE FCP.Date =@date            
                    GROUP BY FU.NKPDName,A.jumlahPerorangan,            
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


                                string filePath = Tools.ARIATextPath + "NKPD01.rad";
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
                                    return Tools.HtmlARIATextPath + "NKPD01.rad";
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
        //        public string FundClient_GenerateNewClientID(int _clientCategory, int _fundClientPK)
        //        {

        //            try
        //            {
        //                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
        //                {
        //                    DbCon.Open();
        //                    using (SqlCommand cmd = DbCon.CreateCommand())
        //                    {
        //                        //IAM
        //                        //                //cmd.CommandText =
        //                        //                //" Declare @ClientCategory  nvarchar(100) " +  
        //                        //                //" Declare @NewClientID  nvarchar(100) " +   
        //                        //                //" Declare @MaxClientName  nvarchar(100)  " +
        //                        //                //" Declare @Period int                " +               
        //                        //                //" Declare @LENdigit int           " +     
        //                        //                //" Declare @NewDigit Nvarchar(20)       " +            
        //                        //                //" \n " +                     
        //                        //                //" select @MaxClientName =   SUBSTRING ( ID ,11 , 3 )+1   from FundClient   " +     
        //                        //                //" where ClientCategory=@ClientCategoryPK order by ID      " +           
        //                        //                //" \n " +
        //                        //                //" select @Period = (RIGHT(CONVERT(VARCHAR(8), getdate(), 3), 2))     " +         
        //                        //                //" \n " +    
        //                        //                //" select @ClientCategory = MAX(left(DescOne,3)) from FundClient F left join MasterValue MV on F.ClientCategory = MV.Code " +
        //                        //                //" and MV.ID ='ClientCategory' and MV.status = 2  where ClientCategory = @ClientCategoryPK " +
        //                        //                //" \n " +   
        //                        //                //" select @LENdigit = LEN(@MaxClientName)        " +            
        //                        //                //" \n " +                       
        //                        //                //" if @LENdigit = 1  " +                  
        //                        //                //" BEGIN                 " +   
        //                        //                //" set @NewDigit = '00' + CAST(@MaxClientName as nvarchar)        " +            
        //                        //                //" END                " +   
        //                        //                //" if @LENdigit = 2         " +          
        //                        //                //" BEGIN                 " +   
        //                        //                //" set @NewDigit = '0' + CAST(@MaxClientName as nvarchar)        " +            
        //                        //                //" END           " +   
        //                        //                //" \n " +
        //                        //                //" set @NewClientID =  CAST(@Period as nvarchar)+ 'RHB' + CAST(@ClientCategory as nvarchar) + CAST(@NewDigit as nvarchar)  " +
        //                        //                //" \n " +
        //                        //                //" Select @NewClientID   NewClientID   ";


        //                        ////SAM
        //                        cmd.CommandText =
        //                         @" Declare @NewClientID  nvarchar(100)    
        //                            Declare @MaxClientID  nvarchar(100)   
        //                                    
        //                            select @MaxClientID =   max(SUBSTRING ( ID ,3 , 4 ) +1)   from FundClient where  status in (1,2) 
        //
        //                            set @NewClientID =  '00' + CAST(@MaxClientID as nvarchar) 
        //                            Select @NewClientID   ID ";


        //                        //RHB
        ////                        cmd.CommandText =

        ////                         //" Declare @ClientCategory int  " +
        ////                         @" Declare @NewClientID  nvarchar(100)    
        ////                          Declare @MaxClientName  nvarchar(100)        
        ////                          Declare @LENdigit int               
        ////                          Declare @NewDigit Nvarchar(20)     
        ////                         -- select @ClientCategory = ClientCategory from FundClient where FundClientPK = @FundClientPK and status = 2     
        ////                          select @MaxClientName =     max(SUBSTRING ( ID ,3 , 4 ) +1)    from FundClient where  status in (1,2) 
        ////                          select @LENdigit = LEN(@MaxClientName)           
        ////                          if @LENdigit = 1      
        ////                          BEGIN              
        ////                          set @NewDigit = '000' + CAST(@MaxClientName as nvarchar)       
        ////                          END                    
        ////                          if @LENdigit = 2         
        ////                          BEGIN                     
        ////                          set @NewDigit = '00' + CAST(@MaxClientName as nvarchar)      
        ////                          END              
        ////                          if @LENdigit = 3      
        ////                          BEGIN                    
        ////                          set @NewDigit = '0' + CAST(@MaxClientName as nvarchar)       
        ////                          END         
        ////                          ELSE BEGIN                     
        ////                          set @NewDigit = CAST(@MaxClientName as nvarchar)        
        ////                          END        
        ////                          set @NewClientID =  '1' + CAST(@ClientCategory as nvarchar) + CAST(@NewDigit as nvarchar) 
        ////                          Select @NewClientID   NewClientID  ";


        //                      // FOR EMCO  cmd.Parameters.AddWithValue("@ClientCategory", _clientCategory);
        //                        //cmd.Parameters.AddWithValue("@FundClientPK", _fundClientPK);

        //                        using (SqlDataReader dr = cmd.ExecuteReader())
        //                        {
        //                            if (!dr.Read())
        //                            {
        //                                return "";
        //                            }
        //                            else
        //                            {
        //                                return Convert.ToString(dr["NewClientID"]);
        //                            }
        //                        }
        //                    }
        //                }

        //            }
        //            catch (Exception err)
        //            {
        //                throw err;
        //            }

        //        }
        public bool FundClient_SInvest_BankAccount(string _userID)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        string filePath = "";

                        filePath = Tools.ReportsPath + "SInvestBankAccountxtVersion" + "_" + _userID + ".xlsx";

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
                                    Select A.IFUACode  +'|' + A.BIMemberCode+'|' + ''  +'|' +A.NamaBank  +'|' +A.Country 
                                     +'|' + A.BankBranchName  +'|' +A.Currency  +'|' + A.NoRek  +'|' +A.NamaNasabah Result
                                    from 
                                    (
	                                    Select IFUACode,BICCode1 BIC,BIMemberCode1 BIMemberCode,B.Name NamaBank,C.Code Country,
	                                    BankBranchName1 BankBranchName,D.DescOne Currency,NomorRekening1 NoRek,NamaNasabah1 NamaNasabah
	                                    from FundClient A
	                                    left join Bank B on A.NamaBank1 = B.BankPK and B.status = 2
	                                    left join MasterValue C on A.BankCountry1 = C.Code and C.Id = 'SDICountry' and C.Status = 2
                                        left join MasterValue D on A.MataUang1 = D.Code and D.Id = 'MataUang' and D.Status = 2
	                                    where A.Selected = 1 and A.status = 2 and NamaBank1 is not null and NamaBank1 > 0

	                                    UNION ALL

	                                    Select IFUACode,BICCode2 BIC,BIMemberCode2 BIMemberCode,B.Name NamaBank,C.Code Country,
	                                    BankBranchName2 BankBranchName,D.DescOne Currency,NomorRekening2 NoRek,NamaNasabah2 NamaNasabah
	                                    from FundClient A
	                                    left join Bank B on A.NamaBank2 = B.BankPK and B.status = 2
	                                    left join MasterValue C on A.BankCountry2 = C.Code and C.Id = 'SDICountry' and C.Status = 2
                                        left join MasterValue D on A.MataUang2 = D.Code and D.Id = 'MataUang' and D.Status = 2
	                                    where A.Selected = 1 and A.status = 2 and NamaBank2 is not null and NamaBank2 > 0

	                                    UNION ALL

	                                    Select IFUACode,BICCode3 BIC,BIMemberCode3 BIMemberCode,B.Name NamaBank,C.Code Country,
	                                    BankBranchName3 BankBranchName,D.DescOne Currency,NomorRekening3 NoRek,NamaNasabah3 NamaNasabah
	                                    from FundClient A
	                                    left join Bank B on A.NamaBank3 = B.BankPK and B.status = 2
	                                    left join MasterValue C on A.BankCountry3 = C.Code and C.Id = 'SDICountry' and C.Status = 2
                                        left join MasterValue D on A.MataUang3 = D.Code and D.Id = 'MataUang' and D.Status = 2
	                                    where A.Selected = 1 and A.status = 2 and NamaBank3 is not null and NamaBank3 > 0
                                    )A        
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

                                int incRowExcel = 2;
                                while (dr1.Read())
                                {
                                    int incColExcel = 1;
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
                                worksheetApproved.HeaderFooter.OddHeader.RightAlignedText = "&14 S-INVEST BANK ACCOUNT";

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
        public bool FundClient_SInvest(string _category, string _userID)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        if (_category == "1")
                        {
                            cmd.CommandText = @"
                                  BEGIN  
                                    SET NOCOUNT ON         
                                    select '1'  
                                     +'|' + @CompanyID    
                                     + '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(SID,''))))  
                                    + '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(NamaDepanInd,''))))      
                                    + '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(NamaTengahInd,''))))      
                                    + '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(NamaBelakangInd,''))))   
                                    + '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(nationality,''))))  
                                    + '|' + (isnull(NoIdentitasInd1,''))
                                    + '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull((case when CONVERT(VARCHAR(10), ExpiredDateIdentitasInd1, 112) = '19000101' or CONVERT(VARCHAR(10), ExpiredDateIdentitasInd1, 112) < '20160802' then '' else CONVERT(VARCHAR(10), ExpiredDateIdentitasInd1, 112) End),''))))          
                                    + '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(NPWP,''))))   
                                    + '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull((case when CONVERT(VARCHAR(10), RegistrationNPWP, 112) <> '19000101' then CONVERT(VARCHAR(10), RegistrationNPWP, 112) else '' End),''))))          
                                    + '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(Countryofbirth,''))))    
                                    + '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(TempatLahir,''))))   
                                    + '|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull((case when CONVERT(VARCHAR(10), TanggalLahir, 112) <> '19000101' then CONVERT(VARCHAR(10), TanggalLahir, 112) else '' End),''))))   
                                    + '|' + case when JenisKelamin = '0' then '' else isnull(cast(JenisKelamin as nvarchar),'') end 
                                    + '|' + case when Pendidikan = '0' then '' else isnull(cast(Pendidikan as nvarchar),'') end  
                                   + '|' + case when mothermaidenname = '0' then '' else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(mothermaidenname ,'')))) end      
                                    + '|' + case when Agama = '0' then '' else isnull(cast(Agama as nvarchar),'') end  
                                    + '|' + case when Pekerjaan = '0' then '' else isnull(cast(Pekerjaan as nvarchar),'') end    
                                    + '|' + case when PenghasilanInd = '0' then '' else isnull(cast(PenghasilanInd as nvarchar),'') end   
                                    + '|' + case when StatusPerkawinan = '0' then '' else isnull(cast(StatusPerkawinan as nvarchar),'') end   
                                    + '|' + case when SpouseName = '0' then '' else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(SpouseName ,'')))) end      
                                    + '|' + case when InvestorsRiskProfile = '0' then '' else isnull(cast(InvestorsRiskProfile as nvarchar),'') end  
                                    + '|' + case when MaksudTujuanInd = '0' then '' else isnull(cast(MaksudTujuanInd as nvarchar),'') end   
                                    + '|' + case when SumberDanaInd = '0' then '' else isnull(cast(SumberDanaInd as nvarchar),'') end   
                                    + '|' + case when AssetOwner = '0' then '' else isnull(cast(AssetOwner as nvarchar),'') end  
                                    + '|' + case when OtherAlamatInd1 = '0' then '' else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(OtherAlamatInd1 ,'')))) end     
                                    + '|' + case when OtherKodeKotaInd1 = '0' then '' else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(cast(OtherKodeKotaInd1 as nvarchar),'')))) end     
                                    + '|' + case when OtherKodePosInd1 = '0' then '' else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(OtherKodePosInd1 ,'')))) end      
                                    + '|' + case when AlamatInd1 = '0' then '' else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(AlamatInd1 ,'')))) end      
                                    + '|' + case when KodeKotaInd1 = '0' then '' else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(cast(KodeKotaInd1 as nvarchar),'')))) end  
                                    + '|' + isnull(MV14.DescOne,'')                                    
                                    + '|' + case when KodePosInd1 = '0' then '' else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(KodePosInd1 ,'')))) end    
                                    + '|' + isnull(CountryofCorrespondence,'')  
                                    + '|' + case when AlamatInd2 = '0' then '' else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(AlamatInd2 ,'')))) end   
                                    + '|' + case when KodeKotaInd2 = '0' then '' else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(cast(KodeKotaInd2 as nvarchar),'')))) end  
                                    + '|' + isnull(MV13.DescOne,'')                                     
                                    + '|' + case when KodePosInd2 = '0' then '' else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(KodePosInd2 ,'')))) end   
                                    + '|' + isnull(CountryofDomicile,'') 
                                    + '|' + case when TeleponRumah = '0' then '' else isnull(TeleponRumah ,'') end    
                                    + '|' + case when TeleponSelular = '0' then '' else isnull(TeleponSelular ,'') end    
                                    + '|' + case when fc.Fax = '0' then '' else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(fc.Fax ,'')))) end     
                                    + '|' + case when fc.Email = '0' then '' else isnull(fc.Email,'') end     
                                    + '|' + case when StatementType = '0' then '' else isnull(cast(StatementType as nvarchar),'') end    
                                    + '|' + case when FATCA = '0' then '' else isnull(cast(FATCA as nvarchar),'') end   
                                    + '|' + case when TIN = '0' then '' else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(TIN ,'')))) end    
                                    + '|' + case when TINIssuanceCountry = '0' then '' else isnull(cast(TINIssuanceCountry as nvarchar),'') end                                   
                                    + '|' +  case when BankCountry1 = 'ID' then '' else isnull(B1.SInvestID,'') end  
                                     + '|' + case when BankCountry1 = 'ID' then isnull(B1.BICode,'') else '' end                           
                                     + '|' + isnull(B1.Name,'') 
                                     + '|' + isnull(BankCountry1,'') 
                                     + '|' + case when BankBranchName1 = '0' then '' else isnull(cast(BankBranchName1 as nvarchar),'') end 
                                     + '|' + isnull(MV15.DescOne,'') 
                                     + '|' + case when NomorRekening1 = '0' then '' else isnull(cast(NomorRekening1 as nvarchar),'') end
                                     + '|' + case when NamaNasabah1 = '0' then '' else isnull(cast(NamaNasabah1 as nvarchar),'') end 
                                    + '|' +  case when BankCountry2 = 'ID' then '' else isnull(B2.SInvestID,'') end  
                                     + '|' + case when BankCountry2 = 'ID' then isnull(B2.BICode,'') else '' end   
                                     + '|' + isnull(B2.Name,'') 
                                     + '|' + isnull(BankCountry2,'') 
                                     + '|' + case when BankBranchName2 = '0' then '' else isnull(cast(BankBranchName2 as nvarchar),'') end 
                                     + '|' + isnull(MV16.DescOne,'') 
                                     + '|' + case when NomorRekening2 = '0' then '' else isnull(cast(NomorRekening2 as nvarchar),'') end 
                                     + '|' + case when NamaNasabah2 = '0' then '' else isnull(cast(NamaNasabah2 as nvarchar),'') end   
									+ '|' +  case when BankCountry3 = 'ID' then '' else isnull(B3.SInvestID,'') end  
                                     + '|' + case when BankCountry3 = 'ID' then isnull(B3.BICode,'') else '' end   
                                     + '|' + isnull(B3.Name,'') 
                                     + '|' + isnull(BankCountry3,'') 
                                     + '|' + case when BankBranchName3 = '0' then '' else isnull(cast(BankBranchName3 as nvarchar),'') end 
                                     + '|' + isnull(MV17.DescOne,'') 
                                     + '|' + case when NomorRekening3 = '0' then '' else isnull(cast(NomorRekening3 as nvarchar),'') end 
                                     + '|' + case when NamaNasabah3 = '0' then '' else isnull(cast(NamaNasabah3 as nvarchar),'') end                                      
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
                                    where FC.Status = 2 and FC.InvestorType = 1  and FC.Selected = 1   
                                    order by FC.name asc END  ";
                            cmd.Parameters.AddWithValue("@CompanyID", _host.Get_CompanyID());
                            using (SqlDataReader dr1 = cmd.ExecuteReader())
                            {
                                if (dr1.HasRows)
                                {
                                    string filePath;
                                    filePath = Tools.SInvestTextPath + "SInvestIndividuTxtVersion.rad";
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
                                     +'|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(case when NomorSIUP = '0' then '' else isnull(cast(NomorSIUP as nvarchar),'') end)))   
                                     +'|' + 
                                     +'|' + case when NoSKD = '0' then '' else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(cast(NoSKD as nvarchar),'')))) end
                                     +'|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(case when CONVERT(VARCHAR(10), ExpiredDateSKD, 112) <> '19000101' then CONVERT(VARCHAR(10), ExpiredDateSKD, 112) else '' End))) 
                                     +'|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(NPWP,'')))) 
                                     +'|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(case when CONVERT(VARCHAR(10), RegistrationNPWP, 112) <> '19000101' then CONVERT(VARCHAR(10), RegistrationNPWP, 112) else '' End)))
                                     +'|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(CountryofEstablishment,''))))  
                                     +'|' + case when LokasiBerdiri = '0' then '' else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(LokasiBerdiri ,'')))) end  
                                     +'|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull((case when CONVERT(VARCHAR(10), TanggalBerdiri, 112) <> '19000101' then CONVERT(VARCHAR(10), TanggalBerdiri, 112) else '' End),''))))         
                                     +'|' + '0'
                                     +'|' + case when Tipe = '0' then '' else isnull(cast(Tipe as nvarchar),'') end 
                                     +'|' + case when Karakteristik = '0' then '' else isnull(cast(Karakteristik as nvarchar),'') end 
                                     +'|' + case when PenghasilanInstitusi = '0' then '' else isnull(cast(PenghasilanInstitusi as nvarchar),'') end 
                                     +'|' + case when InvestorsRiskProfile = '0' then '' else isnull(cast(InvestorsRiskProfile as nvarchar),'') end 
                                     +'|' + case when MaksudTujuanInstitusi = '0' then '' else isnull(cast(MaksudTujuanInstitusi as nvarchar),'') end 
                                     +'|' + case when SumberDanaInstitusi = '0' then '' else isnull(cast(SumberDanaInstitusi as nvarchar),'') end 
                                     +'|' + case when AssetOwner = '0' then '' else isnull(cast(AssetOwner as nvarchar),'') end  
                                     +'|' + case when AlamatPerusahaan = '0' then '' else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(AlamatPerusahaan ,'')))) end   
                                     +'|' + case when KodeKotaIns = '0' then '' else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(cast(KodeKotaIns as nvarchar),'')))) end    
                                     +'|' + isnull(MV10.DescOne,'')                                      
                                     +'|' + case when KodePosIns = '0' then '' else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(KodePosIns ,'')))) end  
                                     +'|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(CountryOfCompany,''))))   
                                     +'|' + case when TeleponBisnis = '0' then '' else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(TeleponBisnis ,'')))) end    
                                     +'|' + case when FC.Fax = '0' then '' else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(FC.Fax ,'')))) end    
                                     +'|' + case when fc.Email = '0' then '' else isnull(fc.Email,'') end     
                                     +'|' + case when StatementType = '0' then '' else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(StatementType ,'')))) end   
                                     +'|' + case when NamaDepanIns1 = '0' then '' else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(NamaDepanIns1 ,'')))) end   
                                     +'|' + case when NamaTengahIns1 = '0' then '' else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(NamaTengahIns1 ,'')))) end  
                                     +'|' + case when NamaBelakangIns1 = '0' then '' else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(NamaBelakangIns1 ,'')))) end  
                                     +'|' + case when Jabatan1 = '0' then '' else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(Jabatan1 ,'')))) end   
                                     +'|' + case when fc.PhoneIns1 = '0' then '' else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(fc.PhoneIns1 ,'')))) end   
                                     +'|' + case when fc.EmailIns1 = '0' then '' else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(fc.EmailIns1 ,'')))) end    
                                     +'|' +  
                                     +'|' + case when fc.NoIdentitasIns11 = '0' then '' else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(fc.NoIdentitasIns11 ,'')))) end   
                                     +'|' + RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull((case when CONVERT(VARCHAR(10), ExpiredDateIdentitasIns11, 112) <> '19000101' then CONVERT(VARCHAR(10), ExpiredDateIdentitasIns11, 112) else '' End),''))))  
                                     +'|' +  
                                     +'|' +  
                                     +'|' + case when NamaDepanIns2 = '0' then '' else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(NamaDepanIns2 ,'')))) end   
                                     +'|' + case when NamaTengahIns2 = '0' then '' else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(NamaTengahIns2 ,'')))) end  
                                     +'|' + case when NamaBelakangIns2 = '0' then '' else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(NamaBelakangIns2 ,'')))) end  
                                     +'|' + case when Jabatan2 = '0' then '' else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(Jabatan1 ,'')))) end   
                                     +'|' + case when fc.PhoneIns2 = '0' then '' else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(fc.PhoneIns2 ,'')))) end   
                                     +'|' + case when fc.EmailIns2 = '0' then '' else RTRIM(LTRIM(dbo.AlphaRemoveExceptLetter(isnull(fc.EmailIns2 ,'')))) end  
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
                                     + '|' + isnull(BankCountry1,'') 
                                     + '|' + case when BankBranchName1 = '0' then '' else isnull(cast(BankBranchName1 as nvarchar),'') end 
                                     + '|' + isnull(MV15.DescOne,'') 
                                     + '|' + case when NomorRekening1 = '0' then '' else isnull(cast(NomorRekening1 as nvarchar),'') end
                                     + '|' + case when NamaNasabah1 = '0' then '' else isnull(cast(NamaNasabah1 as nvarchar),'') end 
                                     + '|' +  ''
                                     + '|' + isnull(B2.BICode,'')                        
                                     + '|' + isnull(B2.Name,'') 
                                     + '|' + isnull(BankCountry2,'') 
                                     + '|' + case when BankBranchName2 = '0' then '' else isnull(cast(BankBranchName2 as nvarchar),'') end 
                                     + '|' + isnull(MV16.DescOne,'') 
                                     + '|' + case when NomorRekening2 = '0' then '' else isnull(cast(NomorRekening2 as nvarchar),'') end 
                                     + '|' + case when NamaNasabah2 = '0' then '' else isnull(cast(NamaNasabah2 as nvarchar),'') end   
                                     + '|' + ''
                                     + '|' + isnull(B3.BICode,'')                        
                                     + '|' + isnull(B3.Name,'') 
                                     + '|' + isnull(BankCountry3,'') 
                                     + '|' + case when BankBranchName3 = '0' then '' else isnull(cast(BankBranchName3 as nvarchar),'') end 
                                     + '|' + isnull(MV17.DescOne,'') 
                                     + '|' + case when NomorRekening3 = '0' then '' else isnull(cast(NomorRekening3 as nvarchar),'') end 
                                     + '|' + case when NamaNasabah3 = '0' then '' else isnull(cast(NamaNasabah3 as nvarchar),'') end                                      
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
                                     left join Bank B1 on fc.NamaBank1 = B1.BankPK and B1.status = 2   
                                     left join Bank B2 on fc.NamaBank2 = B2.BankPK and B2.status = 2   
                                     left join Bank B3 on fc.NamaBank3 = B3.BankPK and B3.status = 2 
                                     where FC.Status = 2 and FC.InvestorType = 2 and Fc.Selected = 1
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
                                        filePath = Tools.SInvestTextPath + "SInvestInstitusiTxtVersion.rad";
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
        public string FundClient_GenerateNewClientID(int _investorType, int _fundClientPK)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @" 							
                                        Declare @NewClientID  nvarchar(100)    
                                        Declare @MaxClientID  int
                                    
                                        select @MaxClientID =   max(convert(int,ID))  + 1 from FundClient where  status in (1,2) 
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


                        //cmd.Parameters.AddWithValue("@ClientCategory", _clientCategory);
                        //cmd.Parameters.AddWithValue("@FundClientPK", _fundClientPK);

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
                                    Case when InvestorType = 1 then NamaDepanInd else NamaPerusahaan End InvestorFirstName,NamaTengahInd InvestorMiddleName,NamaBelakangInd InvestorLastName,isnull(C.DescOne,'') InvestorNationality,
                                    NoIdentitasInd1 InvestorKTPNumber,isnull((case when CONVERT(VARCHAR(10), ExpiredDateIdentitasInd1, 112) <> '19000101' then CONVERT(VARCHAR(10), ExpiredDateIdentitasInd1, 112) else '' End),'') InvestorKTPExpiredDate,NPWP InvestorNPWPNumber,isnull((case when CONVERT(VARCHAR(10), RegistrationNPWP, 112) <> '19000101' then CONVERT(VARCHAR(10), RegistrationNPWP, 112) else '' End),'') InvestorNPWPRegistrationDate,'' InvestorPassportNumber, '' InvestorPassportExpiredDate, NoSKD InvestorKitasSKDNumber,isnull((case when CONVERT(VARCHAR(10), ExpiredDateSKD, 112) <> '19000101' then CONVERT(VARCHAR(10), ExpiredDateSKD, 112) else '' End),'') InvestorKitasSKDExpiredDate,
                                    Case when InvestorType = 1 then TempatLahir else LokasiBerdiri End InvestorBirthPlace,Case when InvestorType = 1 then isnull((case when CONVERT(VARCHAR(10), TanggalLahir, 112) <> '19000101' then CONVERT(VARCHAR(10), TanggalLahir, 112) else '' End),'') else isnull((case when CONVERT(VARCHAR(10), TanggalBerdiri, 112) <> '19000101' then CONVERT(VARCHAR(10), TanggalBerdiri, 112) else '' End),'') End InvestorBirthDate,Case when InvestorType = 1 then AlamatInd1 else AlamatPerusahaan End InvestorAddress1,AlamatInd2 InvestorAddress2,'' InvestorAddress3,'' InvestorCity,'' InvestorProvince,
                                    Case when InvestorType = 1 then KodePosInd1 else KodePosIns End InvestorPostalCode,
                                    isnull(D.ParentCode,'') InvestorCountry,Case when InvestorType = 1 then TeleponRumah else TeleponBisnis End InvestorHomePhone,TeleponSelular InvestorMobilePhone,Email InvestorEmail,Fax InvestorFax,OtherAlamatInd1 InvestorOtherAddress1,OtherAlamatInd2 InvestorOtherAddress2,'' InvestorOtherAddress3,'' InvestorOtherCity,'' InvestorOtherProvince,
                                    OtherKodePosInd1 InvestorOtherPostalCode,isnull(E.ParentCode,'') InvestorOtherCountry,OtherTeleponRumah InvestorOtherHomePhone,OtherTeleponSelular InvestorOtherMobilePhone,
                                    OtherEmail InvestorOtherEmail,OtherFax InvestorOtherFax,isnull(F.DescOne,'') InvestorSex,isnull(G.DescOne,'') InvestorMaritalStatus,isnull(SpouseName,'') InvestorSpouseName,
                                    AhliWaris InvestorHeirName,HubunganAhliWaris InvestorHeirRelation,isnull(H.DescOne,'') InvestorEducationalBackground,isnull(I.DescOne,'') InvestorOccupation,'' InvestorOccupationText,isnull(J.DescOne,'') InvestorNatureofBusiness,
                                    isnull(K.DescOne,'') InvestorIncomePerAnnum,isnull(L.DescOne,'') InvestorFundSource,'' InvestorFundSourceText,Description AccountDescription,
                                    isnull(M.Name,'') InvestorBankAccountName1,NomorRekening1 InvestorBankAccountNumber1,BIMemberCode1 InvestorBankAccountBICCode1,NamaNasabah1 InvestorBankAccountHolderName1,MataUang1 InvestorBankAccountCurrency1,
                                    isnull(N.Name,'') InvestorBankAccountName2,NomorRekening2 InvestorBankAccountNumber2,BIMemberCode2 InvestorBankAccountBICCode2,NamaNasabah2 InvestorBankAccountHolderName2,MataUang2 InvestorBankAccountCurrency2,
                                    isnull(O.Name,'') InvestorBankAccountName3,NomorRekening3 InvestorBankAccountNumber3,BIMemberCode3 InvestorBankAccountBICCode3,NamaNasabah3 InvestorBankAccountHolderName3,MataUang3 InvestorBankAccountCurrency3,
                                    isnull(P.DescOne,'') InvestorInvestmentObjective,isnull(MotherMaidenName,'') InvestorMothersMaidenName,'' DirectSid,isnull(Q.DescOne,'') AssetOwner from FundClient A
                                    left join MasterValue B on A.InvestorType = B.Code and B.ID ='InvestorType' and B.Status = 2 
                                    left join MasterValue C on A.Nationality = C.Code and C.ID ='Nationality' and C.Status = 2 
                                    left join MasterValue D on A.Negara = D.Code and D.ID ='KSEICountry' and D.Status = 2 
                                    left join MasterValue E on A.OtherNegaraInd1 = E.Code and E.ID ='KSEICountry' and E.Status = 2 
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
        public void FundClient_SuspendBySelectedData(string _usersID, string _permissionID)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @" Declare @IPAddress nvarchar(50) select @IPAddress = IPAddress from Users where ID = @UsersID and Status = 2 
                                 Insert into Activity(Time,PermissionID,ObjectTable,ObjectTablePK,Status,Message,UsersID,IPAddress,LastUpdate) 
                                 Select @Time,@PermissionID,'FundClient',FundClientPK,1,'Approve by Selected Data',@UsersID,@IPAddress,@Time  from FundClient where Status = 2 and Selected  = 1 
                                 Update FundClient set BitIsSuspend= 1,SuspendBy=@UsersID,SuspendTime=@Time,LastUpdate=@Time  WHERE status = 2 and FundClientPK in (Select FundClientPK from FundClient where Status = 2 and Selected  = 1) ";

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
        public void FundClient_UnSuspendBySelectedData(string _usersID, string _permissionID)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @" Declare @IPAddress nvarchar(50) select @IPAddress = IPAddress from Users where ID = @UsersID and Status = 2 
                                 Insert into Activity(Time,PermissionID,ObjectTable,ObjectTablePK,Status,Message,UsersID,IPAddress,LastUpdate) 
                                 Select @Time,@PermissionID,'FundClient',FundClientPK,1,'Approve by Selected Data',@UsersID,@IPAddress,@Time  from FundClient where Status = 2 and Selected  = 1 
                                 Update FundClient set BitIsSuspend= 0,UnSuspendBy=@UsersID,UnSuspendTime=@Time,LastUpdate=@Time  WHERE status = 2 and FundClientPK in (Select FundClientPK from FundClient where Status = 2 and Selected  = 1) ";

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
                                [ClientCategory],[InvestorType],[InternalCategoryPK],[SellingAgentPK],[SID],[IFUACode],[Child], 
                                [ARIA],[Registered],[JumlahDanaAwal],[JumlahDanaSaatIniCash],[JumlahDanaSaatIni],
                                [Negara],[Nationality],[NPWP],[SACode],[Propinsi],[TeleponSelular], 
                                [Email],[Fax],[DormantDate],[Description],[JumlahBank],[NamaBank1],
                                [NomorRekening1],[BICCode1],[NamaNasabah1],[MataUang1],[NamaBank2],
                                [NomorRekening2],[BICCode2],[NamaNasabah2],[MataUang2],[NamaBank3],
                                [NomorRekening3],[BICCode3],[NamaNasabah3],[MataUang3],[NamaDepanInd],
                                [NamaTengahInd],[NamaBelakangInd],[TempatLahir],[TanggalLahir],[JenisKelamin],
                                [StatusPerkawinan],[Pekerjaan],[Pendidikan],[Agama],[PenghasilanInd],
                                [SumberDanaInd],[MaksudTujuanInd],[AlamatInd1],[KodeKotaInd1],[KodePosInd1],
                                [AlamatInd2],[KodeKotaInd2],[KodePosInd2],[NamaPerusahaan],[Domisili],
                                [Tipe],[Karakteristik],[NoSKD],[PenghasilanInstitusi],[SumberDanaInstitusi],
                                [MaksudTujuanInstitusi],[AlamatPerusahaan],[KodeKotaIns],[KodePosIns],[SpouseName],[MotherMaidenName], 
                                [AhliWaris],[HubunganAhliWaris],[NatureOfBusiness],[NatureOfBusinessLainnya],[Politis],
                                [PolitisLainnya],[TeleponRumah],[OtherAlamatInd1],[OtherKodeKotaInd1],[OtherKodePosInd1],
                                [OtherPropinsiInd1],[CountryOfBirth],[OtherNegaraInd1],[OtherAlamatInd2],[OtherKodeKotaInd2],[OtherKodePosInd2],
                                [OtherPropinsiInd2],[OtherNegaraInd2],[OtherAlamatInd3],[OtherKodeKotaInd3],[OtherKodePosInd3],
                                [OtherPropinsiInd3],[OtherNegaraInd3],[OtherTeleponRumah],[OtherTeleponSelular],[OtherEmail],
                                [OtherFax],[JumlahIdentitasInd],[IdentitasInd1],[NoIdentitasInd1],[RegistrationDateIdentitasInd1],
                                [ExpiredDateIdentitasInd1],[IdentitasInd2],[NoIdentitasInd2],[RegistrationDateIdentitasInd2],[ExpiredDateIdentitasInd2],
                                [IdentitasInd3],[NoIdentitasInd3],[RegistrationDateIdentitasInd3],[ExpiredDateIdentitasInd3],[IdentitasInd4],
                                [NoIdentitasInd4],[RegistrationDateIdentitasInd4],[ExpiredDateIdentitasInd4],[RegistrationNPWP],
                                [ExpiredDateSKD],[TanggalBerdiri],[LokasiBerdiri],[TeleponBisnis],[NomorAnggaran],
                                [NomorSIUP],[AssetFor1Year],[AssetFor2Year],[AssetFor3Year],[OperatingProfitFor1Year],
                                [OperatingProfitFor2Year],[OperatingProfitFor3Year],[JumlahPejabat],[NamaDepanIns1],[NamaTengahIns1],
                                [NamaBelakangIns1],[Jabatan1],[JumlahIdentitasIns1],[IdentitasIns11],[NoIdentitasIns11],
                                [RegistrationDateIdentitasIns11],[ExpiredDateIdentitasIns11],[IdentitasIns12],[NoIdentitasIns12],[RegistrationDateIdentitasIns12],
                                [ExpiredDateIdentitasIns12],[IdentitasIns13],[NoIdentitasIns13],[RegistrationDateIdentitasIns13],[ExpiredDateIdentitasIns13],
                                [IdentitasIns14],[NoIdentitasIns14],[RegistrationDateIdentitasIns14],[ExpiredDateIdentitasIns14],[NamaDepanIns2],
                                [NamaTengahIns2],[NamaBelakangIns2],[Jabatan2],[JumlahIdentitasIns2],[IdentitasIns21],
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
                                [RegistrationDateIdentitasIns44],[ExpiredDateIdentitasIns44],[CompanyTypeOJK],[BusinessTypeOJK],[BIMemberCode1],[BIMemberCode2],[BIMemberCode3],[PhoneIns1],[EmailIns1],  
                                [PhoneIns2],[EmailIns2],[InvestorsRiskProfile],[AssetOwner],[StatementType],[FATCA],[TIN],[TINIssuanceCountry],[GIIN],[SubstantialOwnerName], 
                                [SubstantialOwnerAddress],[SubstantialOwnerTIN],[BankBranchName1],[BankBranchName2],[BankBranchName3],[BankCountry1],[BankCountry2],[BankCountry3],[CountryofCorrespondence],[CountryofDomicile], 
                                [SIUPExpirationDate],[CountryofEstablishment],[CompanyCityName],[CountryofCompany],[NPWPPerson1],[NPWPPerson2],[BankRDNPK],[RDNAccountNo],[RDNAccountName],
		                        [EntryUsersID],[EntryTime],[LastUpdate],BitIsAfiliated,AfiliatedFromPK,BitIsSuspend)

                                Select  @NewFundClientPK,1,1,'',Name, 
                                ClientCategory,InvestorType,InternalCategoryPK,SellingAgentPK,SID,'',Child, 
                                ARIA,Registered,JumlahDanaAwal,JumlahDanaSaatIniCash,JumlahDanaSaatIni,
                                Negara,Nationality,NPWP,SACode,Propinsi,TeleponSelular, 
                                Email,Fax,DormantDate,Description,JumlahBank,NamaBank1,
                                NomorRekening1,BICCode1,NamaNasabah1,MataUang1,NamaBank2,
                                NomorRekening2,BICCode2,NamaNasabah2,MataUang2,NamaBank3,
                                NomorRekening3,BICCode3,NamaNasabah3,MataUang3,NamaDepanInd,
                                NamaTengahInd,NamaBelakangInd,TempatLahir,TanggalLahir,JenisKelamin,
                                StatusPerkawinan,Pekerjaan,Pendidikan,Agama,PenghasilanInd,
                                SumberDanaInd,MaksudTujuanInd,AlamatInd1,KodeKotaInd1,KodePosInd1,
                                AlamatInd2,KodeKotaInd2,KodePosInd2,NamaPerusahaan,Domisili,
                                Tipe,Karakteristik,NoSKD,PenghasilanInstitusi,SumberDanaInstitusi,
                                MaksudTujuanInstitusi,AlamatPerusahaan,KodeKotaIns,KodePosIns,SpouseName,MotherMaidenName, 
                                AhliWaris,HubunganAhliWaris,NatureOfBusiness,NatureOfBusinessLainnya,Politis,
                                PolitisLainnya,TeleponRumah,OtherAlamatInd1,OtherKodeKotaInd1,OtherKodePosInd1,
                                OtherPropinsiInd1,CountryOfBirth,OtherNegaraInd1,OtherAlamatInd2,OtherKodeKotaInd2,OtherKodePosInd2,
                                OtherPropinsiInd2,OtherNegaraInd2,OtherAlamatInd3,OtherKodeKotaInd3,OtherKodePosInd3,
                                OtherPropinsiInd3,OtherNegaraInd3,OtherTeleponRumah,OtherTeleponSelular,OtherEmail,
                                OtherFax,JumlahIdentitasInd,IdentitasInd1,NoIdentitasInd1,RegistrationDateIdentitasInd1,
                                ExpiredDateIdentitasInd1,IdentitasInd2,NoIdentitasInd2,RegistrationDateIdentitasInd2,ExpiredDateIdentitasInd2,
                                IdentitasInd3,NoIdentitasInd3,RegistrationDateIdentitasInd3,ExpiredDateIdentitasInd3,IdentitasInd4,
                                NoIdentitasInd4,RegistrationDateIdentitasInd4,ExpiredDateIdentitasInd4,RegistrationNPWP,
                                ExpiredDateSKD,TanggalBerdiri,LokasiBerdiri,TeleponBisnis,NomorAnggaran,
                                NomorSIUP,AssetFor1Year,AssetFor2Year,AssetFor3Year,OperatingProfitFor1Year,
                                OperatingProfitFor2Year,OperatingProfitFor3Year,JumlahPejabat,NamaDepanIns1,NamaTengahIns1,
                                NamaBelakangIns1,Jabatan1,JumlahIdentitasIns1,IdentitasIns11,NoIdentitasIns11,
                                RegistrationDateIdentitasIns11,ExpiredDateIdentitasIns11,IdentitasIns12,NoIdentitasIns12,RegistrationDateIdentitasIns12,
                                ExpiredDateIdentitasIns12,IdentitasIns13,NoIdentitasIns13,RegistrationDateIdentitasIns13,ExpiredDateIdentitasIns13,
                                IdentitasIns14,NoIdentitasIns14,RegistrationDateIdentitasIns14,ExpiredDateIdentitasIns14,NamaDepanIns2,
                                NamaTengahIns2,NamaBelakangIns2,Jabatan2,JumlahIdentitasIns2,IdentitasIns21,
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
                                RegistrationDateIdentitasIns44,ExpiredDateIdentitasIns44,CompanyTypeOJK,BusinessTypeOJK,BIMemberCode1,BIMemberCode2,BIMemberCode3,PhoneIns1,EmailIns1,  
                                PhoneIns2,EmailIns2,InvestorsRiskProfile,AssetOwner,StatementType,FATCA,TIN,TINIssuanceCountry,GIIN,SubstantialOwnerName, 
                                SubstantialOwnerAddress,SubstantialOwnerTIN,BankBranchName1,BankBranchName2,BankBranchName3,BankCountry1,BankCountry2,BankCountry3,CountryofCorrespondence,CountryofDomicile, 
                                SIUPExpirationDate,CountryofEstablishment,CompanyCityName,CountryofCompany,NPWPPerson1,NPWPPerson2,BankRDNPK,RDNAccountNo,RDNAccountName,@UserID,@Date,@Date
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

    }
}