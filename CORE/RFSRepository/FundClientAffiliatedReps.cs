using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RFSModel;
using RFSUtility;
using System.Data.SqlClient;


namespace RFSRepository
{
    public class FundClientAffiliatedReps
    {
        Host _host = new Host();


        //2
        private FundClientAffiliated setFundClientAffiliated(SqlDataReader dr)
        {
            FundClientAffiliated M_FundClientAffiliated = new FundClientAffiliated();
            M_FundClientAffiliated.NoIdentitasInd1 = dr["NoIdentitasInd1"].ToString();
            M_FundClientAffiliated.Status = Convert.ToInt32(dr["Status"]);
            M_FundClientAffiliated.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_FundClientAffiliated.InvestorType = dr["InvestorType"].ToString();
            M_FundClientAffiliated.Name = dr["Name"].ToString();
            M_FundClientAffiliated.SID = dr["SID"].ToString();
            M_FundClientAffiliated.NPWP = dr["NPWP"].ToString();
            M_FundClientAffiliated.RegistrationNPWP = dr["RegistrationNPWP"].ToString();
            M_FundClientAffiliated.InvestorsRiskProfile = dr["InvestorsRiskProfile"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["InvestorsRiskProfile"]);
            M_FundClientAffiliated.KYCRiskProfile = dr["KYCRiskProfile"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["KYCRiskProfile"]);
            M_FundClientAffiliated.BitShareAbleToGroup = Convert.ToBoolean(dr["BitShareAbleToGroup"]);
            M_FundClientAffiliated.AssetOwner = dr["AssetOwner"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["AssetOwner"]);
            M_FundClientAffiliated.DatePengkinianData = dr["DatePengkinianData"].ToString();
            M_FundClientAffiliated.NamaDepanInd = dr["NamaDepanInd"].ToString();
            M_FundClientAffiliated.NamaTengahInd = dr["NamaTengahInd"].ToString();
            M_FundClientAffiliated.NamaBelakangInd = dr["NamaBelakangInd"].ToString();
            M_FundClientAffiliated.IdentitasInd1 = dr["IdentitasInd1"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["IdentitasInd1"]);
            M_FundClientAffiliated.RegistrationDateIdentitasInd1 = dr["RegistrationDateIdentitasInd1"].ToString();
            M_FundClientAffiliated.ExpiredDateIdentitasInd1 = dr["ExpiredDateIdentitasInd1"].ToString();
            M_FundClientAffiliated.OtherAlamatInd1 = dr["OtherAlamatInd1"].ToString();
            M_FundClientAffiliated.OtherPropinsiInd1 = dr["OtherPropinsiInd1"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["OtherPropinsiInd1"]);
            M_FundClientAffiliated.OtherNegaraInd1 = dr["OtherNegaraInd1"].ToString();
            M_FundClientAffiliated.OtherKodePosInd1 = dr["OtherKodePosInd1"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["OtherKodePosInd1"]);
            M_FundClientAffiliated.IdentitasInd2 = dr["IdentitasInd2"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["IdentitasInd2"]);
            M_FundClientAffiliated.NoIdentitasInd2 = dr["NoIdentitasInd2"].ToString();
            M_FundClientAffiliated.RegistrationDateIdentitasInd2 = dr["RegistrationDateIdentitasInd2"].ToString();
            M_FundClientAffiliated.ExpiredDateIdentitasInd2 = dr["ExpiredDateIdentitasInd2"].ToString();
            M_FundClientAffiliated.AlamatInd2 = dr["AlamatInd2"].ToString();
            M_FundClientAffiliated.DomicileRT = dr["DomicileRT"].ToString();
            M_FundClientAffiliated.DomicileRW = dr["DomicileRW"].ToString();
            M_FundClientAffiliated.KodeKotaInd2 = dr["KodeKotaInd2"].ToString();
            M_FundClientAffiliated.KodeDomisiliPropinsi = dr["KodeDomisiliPropinsi"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["KodeDomisiliPropinsi"]);
            M_FundClientAffiliated.CountryofDomicile = dr["CountryofDomicile"].ToString();
            M_FundClientAffiliated.KodePosInd2 = dr["KodePosInd2"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["KodePosInd2"]);
            M_FundClientAffiliated.TempatLahir = dr["TempatLahir"].ToString();
            M_FundClientAffiliated.TanggalLahir = dr["TanggalLahir"].ToString();
            M_FundClientAffiliated.JenisKelamin = dr["JenisKelamin"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["JenisKelamin"]);
            M_FundClientAffiliated.Nationality = dr["Nationality"].ToString();
            M_FundClientAffiliated.CountryOfBirth = dr["CountryOfBirth"].ToString();
            M_FundClientAffiliated.Agama = dr["Agama"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["Agama"]);
            M_FundClientAffiliated.OtherAgama = dr["OtherAgama"].ToString();
            M_FundClientAffiliated.Pendidikan = dr["Pendidikan"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["Pendidikan"]);
            M_FundClientAffiliated.OtherPendidikan = dr["OtherPendidikan"].ToString();
            M_FundClientAffiliated.MotherMaidenName = dr["MotherMaidenName"].ToString();
            M_FundClientAffiliated.AhliWaris = dr["AhliWaris"].ToString();
            M_FundClientAffiliated.HubunganAhliWaris = dr["HubunganAhliWaris"].ToString();
            M_FundClientAffiliated.Pekerjaan = dr["Pekerjaan"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["Pekerjaan"]);
            M_FundClientAffiliated.OtherOccupation = dr["OtherOccupation"].ToString();
            M_FundClientAffiliated.NatureOfBusiness = dr["NatureOfBusiness"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["NatureOfBusiness"]);
            M_FundClientAffiliated.NatureOfBusinessLainnya = dr["NatureOfBusinessLainnya"].ToString();
            M_FundClientAffiliated.PenghasilanInd = dr["PenghasilanInd"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["PenghasilanInd"]);
            M_FundClientAffiliated.SumberDanaInd = dr["SumberDanaInd"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["SumberDanaInd"]);
            M_FundClientAffiliated.MaksudTujuanInd = dr["MaksudTujuanInd"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["MaksudTujuanInd"]);
            M_FundClientAffiliated.StatusPerkawinan = dr["StatusPerkawinan"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["StatusPerkawinan"]);
            M_FundClientAffiliated.SpouseName = dr["SpouseName"].ToString();
            M_FundClientAffiliated.SpouseDateOfBirth = dr["SpouseDateOfBirth"].ToString();
            M_FundClientAffiliated.SpouseBirthPlace = dr["SpouseBirthPlace"].ToString();
            M_FundClientAffiliated.SpouseOccupation = dr["SpouseOccupation"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["SpouseOccupation"]);
            M_FundClientAffiliated.OtherSpouseOccupation = dr["OtherSpouseOccupation"].ToString();
            M_FundClientAffiliated.SpouseNatureOfBusiness = dr["SpouseNatureOfBusiness"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["SpouseNatureOfBusiness"]);
            M_FundClientAffiliated.SpouseNatureOfBusinessOther = dr["SpouseNatureOfBusinessOther"].ToString();
            M_FundClientAffiliated.SpouseIDNo = dr["SpouseIDNo"].ToString();
            M_FundClientAffiliated.SpouseNationality = dr["SpouseNationality"].ToString();
            M_FundClientAffiliated.SpouseAnnualIncome = dr["SpouseAnnualIncome"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["SpouseAnnualIncome"]);
            M_FundClientAffiliated.NamaKantor = dr["NamaKantor"].ToString();
            M_FundClientAffiliated.EmployerLineOfBusiness = dr["EmployerLineOfBusiness"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["EmployerLineOfBusiness"]);
            M_FundClientAffiliated.JabatanKantor = dr["JabatanKantor"].ToString();
            M_FundClientAffiliated.TeleponKantor = dr["TeleponKantor"].ToString();
            M_FundClientAffiliated.AlamatKantorInd = dr["AlamatKantorInd"].ToString();
            M_FundClientAffiliated.KodeKotaKantorInd = dr["KodeKotaKantorInd"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["KodeKotaKantorInd"]);
            M_FundClientAffiliated.KodePropinsiKantorInd = dr["KodePropinsiKantorInd"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["KodePropinsiKantorInd"]);
            M_FundClientAffiliated.KodeCountryofKantor = dr["KodeCountryofKantor"].ToString();
            M_FundClientAffiliated.KodePosKantorInd = dr["KodePosKantorInd"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["KodePosKantorInd"]);
            M_FundClientAffiliated.Politis = dr["Politis"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["Politis"]);
            M_FundClientAffiliated.PolitisLainnya = dr["PolitisLainnya"].ToString();
            M_FundClientAffiliated.PolitisRelation = dr["PolitisRelation"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["PolitisRelation"]);
            M_FundClientAffiliated.PolitisName = dr["PolitisName"].ToString();
            M_FundClientAffiliated.PolitisFT = dr["PolitisFT"].ToString();
            M_FundClientAffiliated.EntryUsersID = dr["EntryUsersID"].ToString();
            M_FundClientAffiliated.EntryTime = dr["EntryTime"].ToString();
            M_FundClientAffiliated.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_FundClientAffiliated.UpdateTime = dr["UpdateTime"].ToString();
            M_FundClientAffiliated.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_FundClientAffiliated.ApprovedTime = dr["ApprovedTime"].ToString();
            M_FundClientAffiliated.VoidUsersID = dr["VoidUsersID"].ToString();
            M_FundClientAffiliated.VoidTime = dr["VoidTime"].ToString();
            M_FundClientAffiliated.LastUpdate = dr["LastUpdate"].ToString();
            return M_FundClientAffiliated;
        }

        //3
        public List<FundClientAffiliated> FundClientAffiliated_Select(int _status)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<FundClientAffiliated> L_FundClientAffiliated = new List<FundClientAffiliated>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = @"
                                                select * from FundClientAffiliated where status = @status
                                                ";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = @"
                                                select * from FundClientAffiliated
                                                ";
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_FundClientAffiliated.Add(setFundClientAffiliated(dr));
                                }
                            }
                            return L_FundClientAffiliated;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public List<FundClientAffiliated> FundClientAffiliated_SelectByFundClient(int _status)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<FundClientAffiliated> L_FundClientAffiliated = new List<FundClientAffiliated>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"

                            
                            select InvestorType,Name,SID,NPWP,RegistrationNPWP,InvestorsRiskProfile,KYCRiskProfile,BitShareAbleToGroup,AssetOwner,DatePengkinianData,
                            NamaDepanInd,NamaTengahInd,NamaBelakangInd,IdentitasInd1,NoIdentitasInd1,RegistrationDateIdentitasInd1,ExpiredDateIdentitasInd1,OtherAlamatInd1,OtherPropinsiInd1,
                            OtherNegaraInd1,OtherKodePosInd1,IdentitasInd2,NoIdentitasInd2,RegistrationDateIdentitasInd2,ExpiredDateIdentitasInd2,AlamatInd2,DomicileRT,DomicileRW,KodeKotaInd2,
                            KodeDomisiliPropinsi,CountryofDomicile,KodePosInd2,TempatLahir,TanggalLahir,JenisKelamin,Nationality,CountryOfBirth,Agama,OtherAgama,Pendidikan,OtherPendidikan,MotherMaidenName,
                            AhliWaris,HubunganAhliWaris,Pekerjaan,OtherOccupation,NatureOfBusiness,NatureOfBusinessLainnya,PenghasilanInd,SumberDanaInd,MaksudTujuanInd,StatusPerkawinan,SpouseName,SpouseDateOfBirth,
                            SpouseBirthPlace,SpouseOccupation,OtherSpouseOccupation,SpouseNatureOfBusiness,SpouseNatureOfBusinessOther,SpouseIDNo,SpouseNationality,SpouseAnnualIncome,NamaKantor,EmployerLineOfBusiness,
                            JabatanKantor,TeleponKantor,AlamatKantorInd,KodeKotaKantorInd,KodePropinsiKantorInd,KodeCountryofKantor,KodePosKantorInd,Politis,PolitisLainnya,PolitisRelation,PolitisName,PolitisFT,2 status, 1 historypk,
							'' EntryUsersID,'' EntryTime,'' UpdateUsersID,'' UpdateTime,'' ApprovedUsersID,'' ApprovedTime,'' VoidUsersID,'' VoidTime,'' LastUpdate
                            from FundClient where NoIdentitasInd1 in (
	                            select NoIdentitasInd1 from FundClient where status in (1,2)
	                            and NoIdentitasInd1 not in ('null','') and NoIdentitasInd1 not in (select NoIdentitasInd1 from FundClientAffiliated where status = 1)
	                            group by NoIdentitasInd1
	                            having count(*) > 1
                            )

                            group by InvestorType,Name,SID,NPWP,RegistrationNPWP,InvestorsRiskProfile,KYCRiskProfile,BitShareAbleToGroup,AssetOwner,DatePengkinianData,
                            NamaDepanInd,NamaTengahInd,NamaBelakangInd,IdentitasInd1,NoIdentitasInd1,RegistrationDateIdentitasInd1,ExpiredDateIdentitasInd1,OtherAlamatInd1,OtherPropinsiInd1,
                            OtherNegaraInd1,OtherKodePosInd1,IdentitasInd2,NoIdentitasInd2,RegistrationDateIdentitasInd2,ExpiredDateIdentitasInd2,AlamatInd2,DomicileRT,DomicileRW,KodeKotaInd2,
                            KodeDomisiliPropinsi,CountryofDomicile,KodePosInd2,TempatLahir,TanggalLahir,JenisKelamin,Nationality,CountryOfBirth,Agama,OtherAgama,Pendidikan,OtherPendidikan,MotherMaidenName,
                            AhliWaris,HubunganAhliWaris,Pekerjaan,OtherOccupation,NatureOfBusiness,NatureOfBusinessLainnya,PenghasilanInd,SumberDanaInd,MaksudTujuanInd,StatusPerkawinan,SpouseName,SpouseDateOfBirth,
                            SpouseBirthPlace,SpouseOccupation,OtherSpouseOccupation,SpouseNatureOfBusiness,SpouseNatureOfBusinessOther,SpouseIDNo,SpouseNationality,SpouseAnnualIncome,NamaKantor,EmployerLineOfBusiness,
                            JabatanKantor,TeleponKantor,AlamatKantorInd,KodeKotaKantorInd,KodePropinsiKantorInd,KodeCountryofKantor,KodePosKantorInd,Politis,PolitisLainnya,PolitisRelation,PolitisName,PolitisFT

                            order by NoIdentitasInd1


                                                ";
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_FundClientAffiliated.Add(setFundClientAffiliated(dr));
                                }
                            }
                            return L_FundClientAffiliated;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int FundClientAffiliated_Add(FundClientAffiliated _FundClientAffiliated, bool _havePrivillege)
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
                            declare @newHistoryPK int
                            select @newHistoryPK = max(HistoryPK) + 1 from FundClientAffiliated where NoIdentitasInd1 = @NoIdentitasInd1
                            set @newHistoryPK = isnull(@newHistoryPK,1)

                            update FundClientAffiliated set status = 3, VoidUsersID = @EntryUsersID, VoidTime = @EntryTime, LastUpdate = @LastUpdate  where NoIdentitasInd1 = @NoIdentitasInd1 and status in (2)

                            INSERT INTO [dbo].[FundClientAffiliated] 
                            (Status,HistoryPK,InvestorType,Name,SID,NPWP,RegistrationNPWP,InvestorsRiskProfile,KYCRiskProfile,BitShareAbleToGroup,AssetOwner,DatePengkinianData,
                            NamaDepanInd,NamaTengahInd,NamaBelakangInd,IdentitasInd1,NoIdentitasInd1,RegistrationDateIdentitasInd1,ExpiredDateIdentitasInd1,OtherAlamatInd1,OtherPropinsiInd1,
                            OtherNegaraInd1,OtherKodePosInd1,IdentitasInd2,NoIdentitasInd2,RegistrationDateIdentitasInd2,ExpiredDateIdentitasInd2,AlamatInd2,DomicileRT,DomicileRW,KodeKotaInd2,
                            KodeDomisiliPropinsi,CountryofDomicile,KodePosInd2,TempatLahir,TanggalLahir,JenisKelamin,Nationality,CountryOfBirth,Agama,OtherAgama,Pendidikan,OtherPendidikan,MotherMaidenName,
                            AhliWaris,HubunganAhliWaris,Pekerjaan,OtherOccupation,NatureOfBusiness,NatureOfBusinessLainnya,PenghasilanInd,SumberDanaInd,MaksudTujuanInd,StatusPerkawinan,SpouseName,SpouseDateOfBirth,
                            SpouseBirthPlace,SpouseOccupation,OtherSpouseOccupation,SpouseNatureOfBusiness,SpouseNatureOfBusinessOther,SpouseIDNo,SpouseNationality,SpouseAnnualIncome,NamaKantor,EmployerLineOfBusiness,
                            JabatanKantor,TeleponKantor,AlamatKantorInd,KodeKotaKantorInd,KodePropinsiKantorInd,KodeCountryofKantor,KodePosKantorInd,Politis,PolitisLainnya,PolitisRelation,PolitisName,PolitisFT,EntryUsersID,EntryTime,LastUpdate)

                            select 1,@newHistoryPK,@InvestorType,@Name,@SID,@NPWP,@RegistrationNPWP,@InvestorsRiskProfile,@KYCRiskProfile,@BitShareAbleToGroup,@AssetOwner,@DatePengkinianData,
                            @NamaDepanInd,@NamaTengahInd,@NamaBelakangInd,@IdentitasInd1,@NoIdentitasInd1,@RegistrationDateIdentitasInd1,@ExpiredDateIdentitasInd1,@OtherAlamatInd1,@OtherPropinsiInd1,
                            @OtherNegaraInd1,@OtherKodePosInd1,@IdentitasInd2,@NoIdentitasInd2,@RegistrationDateIdentitasInd2,@ExpiredDateIdentitasInd2,@AlamatInd2,@DomicileRT,@DomicileRW,@KodeKotaInd2,
                            @KodeDomisiliPropinsi,@CountryofDomicile,@KodePosInd2,@TempatLahir,@TanggalLahir,@JenisKelamin,@Nationality,@CountryOfBirth,@Agama,@OtherAgama,@Pendidikan,@OtherPendidikan,@MotherMaidenName,
                            @AhliWaris,@HubunganAhliWaris,@Pekerjaan,@OtherOccupation,@NatureOfBusiness,@NatureOfBusinessLainnya,@PenghasilanInd,@SumberDanaInd,@MaksudTujuanInd,@StatusPerkawinan,@SpouseName,@SpouseDateOfBirth,
                            @SpouseBirthPlace,@SpouseOccupation,@OtherSpouseOccupation,@SpouseNatureOfBusiness,@SpouseNatureOfBusinessOther,@SpouseIDNo,@SpouseNationality,@SpouseAnnualIncome,@NamaKantor,@EmployerLineOfBusiness,
                            @JabatanKantor,@TeleponKantor,@AlamatKantorInd,@KodeKotaKantorInd,@KodePropinsiKantorInd,@KodeCountryofKantor,@KodePosKantorInd,@Politis,@PolitisLainnya,@PolitisRelation,@PolitisName,@PolitisFT,@EntryUsersID,@EntryTime,@LastUpdate

                                                            ";

                        cmd.Parameters.AddWithValue("@IdentitasInd1", _FundClientAffiliated.IdentitasInd1);
                        cmd.Parameters.AddWithValue("@InvestorType", _FundClientAffiliated.InvestorType);
                        cmd.Parameters.AddWithValue("@Name", _FundClientAffiliated.Name);
                        cmd.Parameters.AddWithValue("@SID", _FundClientAffiliated.SID);
                        cmd.Parameters.AddWithValue("@NPWP", _FundClientAffiliated.NPWP);
                        cmd.Parameters.AddWithValue("@RegistrationNPWP", _FundClientAffiliated.RegistrationNPWP);
                        cmd.Parameters.AddWithValue("@InvestorsRiskProfile", _FundClientAffiliated.InvestorsRiskProfile);
                        cmd.Parameters.AddWithValue("@KYCRiskProfile", _FundClientAffiliated.KYCRiskProfile);
                        cmd.Parameters.AddWithValue("@BitShareAbleToGroup", _FundClientAffiliated.BitShareAbleToGroup);
                        cmd.Parameters.AddWithValue("@AssetOwner", _FundClientAffiliated.AssetOwner);
                        cmd.Parameters.AddWithValue("@DatePengkinianData", _FundClientAffiliated.DatePengkinianData);
                        cmd.Parameters.AddWithValue("@NamaDepanInd", _FundClientAffiliated.NamaDepanInd);
                        cmd.Parameters.AddWithValue("@NamaTengahInd", _FundClientAffiliated.NamaTengahInd);
                        cmd.Parameters.AddWithValue("@NamaBelakangInd", _FundClientAffiliated.NamaBelakangInd);
                        cmd.Parameters.AddWithValue("@NoIdentitasInd1", _FundClientAffiliated.NoIdentitasInd1);
                        cmd.Parameters.AddWithValue("@RegistrationDateIdentitasInd1", _FundClientAffiliated.RegistrationDateIdentitasInd1);
                        cmd.Parameters.AddWithValue("@ExpiredDateIdentitasInd1", _FundClientAffiliated.ExpiredDateIdentitasInd1);
                        cmd.Parameters.AddWithValue("@OtherAlamatInd1", _FundClientAffiliated.OtherAlamatInd1);
                        cmd.Parameters.AddWithValue("@OtherPropinsiInd1", _FundClientAffiliated.OtherPropinsiInd1);
                        cmd.Parameters.AddWithValue("@OtherNegaraInd1", _FundClientAffiliated.OtherNegaraInd1);
                        cmd.Parameters.AddWithValue("@OtherKodePosInd1", _FundClientAffiliated.OtherKodePosInd1);
                        cmd.Parameters.AddWithValue("@IdentitasInd2", _FundClientAffiliated.IdentitasInd2);
                        cmd.Parameters.AddWithValue("@NoIdentitasInd2", _FundClientAffiliated.NoIdentitasInd2);
                        cmd.Parameters.AddWithValue("@RegistrationDateIdentitasInd2", _FundClientAffiliated.RegistrationDateIdentitasInd2);
                        cmd.Parameters.AddWithValue("@ExpiredDateIdentitasInd2", _FundClientAffiliated.ExpiredDateIdentitasInd2);
                        cmd.Parameters.AddWithValue("@AlamatInd2", _FundClientAffiliated.AlamatInd2);
                        cmd.Parameters.AddWithValue("@DomicileRT", _FundClientAffiliated.DomicileRT);
                        cmd.Parameters.AddWithValue("@DomicileRW", _FundClientAffiliated.DomicileRW);
                        cmd.Parameters.AddWithValue("@KodeKotaInd2", _FundClientAffiliated.KodeKotaInd2);
                        cmd.Parameters.AddWithValue("@KodeDomisiliPropinsi", _FundClientAffiliated.KodeDomisiliPropinsi);
                        cmd.Parameters.AddWithValue("@CountryofDomicile", _FundClientAffiliated.CountryofDomicile);
                        cmd.Parameters.AddWithValue("@KodePosInd2", _FundClientAffiliated.KodePosInd2);
                        cmd.Parameters.AddWithValue("@TempatLahir", _FundClientAffiliated.TempatLahir);
                        cmd.Parameters.AddWithValue("@TanggalLahir", _FundClientAffiliated.TanggalLahir);
                        cmd.Parameters.AddWithValue("@JenisKelamin", _FundClientAffiliated.JenisKelamin);
                        cmd.Parameters.AddWithValue("@Nationality", _FundClientAffiliated.Nationality);
                        cmd.Parameters.AddWithValue("@CountryOfBirth", _FundClientAffiliated.CountryOfBirth);
                        cmd.Parameters.AddWithValue("@Agama", _FundClientAffiliated.Agama);
                        cmd.Parameters.AddWithValue("@OtherAgama", _FundClientAffiliated.OtherAgama);
                        cmd.Parameters.AddWithValue("@Pendidikan", _FundClientAffiliated.Pendidikan);
                        cmd.Parameters.AddWithValue("@OtherPendidikan", _FundClientAffiliated.OtherPendidikan);
                        cmd.Parameters.AddWithValue("@MotherMaidenName", _FundClientAffiliated.MotherMaidenName);
                        cmd.Parameters.AddWithValue("@AhliWaris", _FundClientAffiliated.AhliWaris);
                        cmd.Parameters.AddWithValue("@HubunganAhliWaris", _FundClientAffiliated.HubunganAhliWaris);
                        cmd.Parameters.AddWithValue("@Pekerjaan", _FundClientAffiliated.Pekerjaan);
                        cmd.Parameters.AddWithValue("@OtherOccupation", _FundClientAffiliated.OtherOccupation);
                        cmd.Parameters.AddWithValue("@NatureOfBusiness", _FundClientAffiliated.NatureOfBusiness);
                        cmd.Parameters.AddWithValue("@NatureOfBusinessLainnya", _FundClientAffiliated.NatureOfBusinessLainnya);
                        cmd.Parameters.AddWithValue("@PenghasilanInd", _FundClientAffiliated.PenghasilanInd);
                        cmd.Parameters.AddWithValue("@SumberDanaInd", _FundClientAffiliated.SumberDanaInd);
                        cmd.Parameters.AddWithValue("@MaksudTujuanInd", _FundClientAffiliated.MaksudTujuanInd);
                        cmd.Parameters.AddWithValue("@StatusPerkawinan", _FundClientAffiliated.StatusPerkawinan);
                        cmd.Parameters.AddWithValue("@SpouseName", _FundClientAffiliated.SpouseName);
                        cmd.Parameters.AddWithValue("@SpouseDateOfBirth", _FundClientAffiliated.SpouseDateOfBirth);
                        cmd.Parameters.AddWithValue("@SpouseBirthPlace", _FundClientAffiliated.SpouseBirthPlace);
                        cmd.Parameters.AddWithValue("@SpouseOccupation", _FundClientAffiliated.SpouseOccupation);
                        cmd.Parameters.AddWithValue("@OtherSpouseOccupation", _FundClientAffiliated.OtherSpouseOccupation);
                        cmd.Parameters.AddWithValue("@SpouseNatureOfBusiness", _FundClientAffiliated.SpouseNatureOfBusiness);
                        cmd.Parameters.AddWithValue("@SpouseNatureOfBusinessOther", _FundClientAffiliated.SpouseNatureOfBusinessOther);
                        cmd.Parameters.AddWithValue("@SpouseIDNo", _FundClientAffiliated.SpouseIDNo);
                        cmd.Parameters.AddWithValue("@SpouseNationality", _FundClientAffiliated.SpouseNationality);
                        cmd.Parameters.AddWithValue("@SpouseAnnualIncome", _FundClientAffiliated.SpouseAnnualIncome);
                        cmd.Parameters.AddWithValue("@NamaKantor", _FundClientAffiliated.NamaKantor);
                        cmd.Parameters.AddWithValue("@EmployerLineOfBusiness", _FundClientAffiliated.EmployerLineOfBusiness);
                        cmd.Parameters.AddWithValue("@JabatanKantor", _FundClientAffiliated.JabatanKantor);
                        cmd.Parameters.AddWithValue("@TeleponKantor", _FundClientAffiliated.TeleponKantor);
                        cmd.Parameters.AddWithValue("@AlamatKantorInd", _FundClientAffiliated.AlamatKantorInd);
                        cmd.Parameters.AddWithValue("@KodeKotaKantorInd", _FundClientAffiliated.KodeKotaKantorInd);
                        cmd.Parameters.AddWithValue("@KodePropinsiKantorInd", _FundClientAffiliated.KodePropinsiKantorInd);
                        cmd.Parameters.AddWithValue("@KodeCountryofKantor", _FundClientAffiliated.KodeCountryofKantor);
                        cmd.Parameters.AddWithValue("@KodePosKantorInd", _FundClientAffiliated.KodePosKantorInd);
                        cmd.Parameters.AddWithValue("@Politis", _FundClientAffiliated.Politis);
                        cmd.Parameters.AddWithValue("@PolitisLainnya", _FundClientAffiliated.PolitisLainnya);
                        cmd.Parameters.AddWithValue("@PolitisRelation", _FundClientAffiliated.PolitisRelation);
                        cmd.Parameters.AddWithValue("@PolitisName", _FundClientAffiliated.PolitisName);
                        cmd.Parameters.AddWithValue("@PolitisFT", _FundClientAffiliated.PolitisFT);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _FundClientAffiliated.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);

                        cmd.ExecuteNonQuery();

                        return 0;
                    }
                }

            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int FundClientAffiliated_Update(FundClientAffiliated _FundClientAffiliated, bool _havePrivillege)
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
                            declare @newHistoryPK int
                            select @newHistoryPK = max(HistoryPK) + 1 from FundClientAffiliated where NoIdentitasInd1 = @NoIdentitasInd1
                            set @newHistoryPK = isnull(@newHistoryPK,1)

                            update FundClientAffiliated set status = 3, VoidUsersID = @EntryUsersID, VoidTime = @EntryTime, LastUpdate = @LastUpdate  where NoIdentitasInd1 = @NoIdentitasInd1 and historyPK = @HistoryPK

                            INSERT INTO [dbo].[FundClientAffiliated] 
                            (Status,HistoryPK,InvestorType,Name,SID,NPWP,RegistrationNPWP,InvestorsRiskProfile,KYCRiskProfile,BitShareAbleToGroup,AssetOwner,DatePengkinianData,
                            NamaDepanInd,NamaTengahInd,NamaBelakangInd,IdentitasInd1,NoIdentitasInd1,RegistrationDateIdentitasInd1,ExpiredDateIdentitasInd1,OtherAlamatInd1,OtherPropinsiInd1,
                            OtherNegaraInd1,OtherKodePosInd1,IdentitasInd2,NoIdentitasInd2,RegistrationDateIdentitasInd2,ExpiredDateIdentitasInd2,AlamatInd2,DomicileRT,DomicileRW,KodeKotaInd2,
                            KodeDomisiliPropinsi,CountryofDomicile,KodePosInd2,TempatLahir,TanggalLahir,JenisKelamin,Nationality,CountryOfBirth,Agama,OtherAgama,Pendidikan,OtherPendidikan,MotherMaidenName,
                            AhliWaris,HubunganAhliWaris,Pekerjaan,OtherOccupation,NatureOfBusiness,NatureOfBusinessLainnya,PenghasilanInd,SumberDanaInd,MaksudTujuanInd,StatusPerkawinan,SpouseName,SpouseDateOfBirth,
                            SpouseBirthPlace,SpouseOccupation,OtherSpouseOccupation,SpouseNatureOfBusiness,SpouseNatureOfBusinessOther,SpouseIDNo,SpouseNationality,SpouseAnnualIncome,NamaKantor,EmployerLineOfBusiness,
                            JabatanKantor,TeleponKantor,AlamatKantorInd,KodeKotaKantorInd,KodePropinsiKantorInd,KodeCountryofKantor,KodePosKantorInd,Politis,PolitisLainnya,PolitisRelation,PolitisName,PolitisFT,EntryUsersID,EntryTime,UpdateUsersID,UpdateTime,LastUpdate)

                            select 1,@newHistoryPK,@InvestorType,@Name,@SID,@NPWP,@RegistrationNPWP,@InvestorsRiskProfile,@KYCRiskProfile,@BitShareAbleToGroup,@AssetOwner,@DatePengkinianData,
                            @NamaDepanInd,@NamaTengahInd,@NamaBelakangInd,@IdentitasInd1,@NoIdentitasInd1,@RegistrationDateIdentitasInd1,@ExpiredDateIdentitasInd1,@OtherAlamatInd1,@OtherPropinsiInd1,
                            @OtherNegaraInd1,@OtherKodePosInd1,@IdentitasInd2,@NoIdentitasInd2,@RegistrationDateIdentitasInd2,@ExpiredDateIdentitasInd2,@AlamatInd2,@DomicileRT,@DomicileRW,@KodeKotaInd2,
                            @KodeDomisiliPropinsi,@CountryofDomicile,@KodePosInd2,@TempatLahir,@TanggalLahir,@JenisKelamin,@Nationality,@CountryOfBirth,@Agama,@OtherAgama,@Pendidikan,@OtherPendidikan,@MotherMaidenName,
                            @AhliWaris,@HubunganAhliWaris,@Pekerjaan,@OtherOccupation,@NatureOfBusiness,@NatureOfBusinessLainnya,@PenghasilanInd,@SumberDanaInd,@MaksudTujuanInd,@StatusPerkawinan,@SpouseName,@SpouseDateOfBirth,
                            @SpouseBirthPlace,@SpouseOccupation,@OtherSpouseOccupation,@SpouseNatureOfBusiness,@SpouseNatureOfBusinessOther,@SpouseIDNo,@SpouseNationality,@SpouseAnnualIncome,@NamaKantor,@EmployerLineOfBusiness,
                            @JabatanKantor,@TeleponKantor,@AlamatKantorInd,@KodeKotaKantorInd,@KodePropinsiKantorInd,@KodeCountryofKantor,@KodePosKantorInd,@Politis,@PolitisLainnya,@PolitisRelation,@PolitisName,@PolitisFT,@EntryUsersID,@EntryTime,@EntryUsersID,@EntryTime,@LastUpdate

                                                            ";
                        cmd.Parameters.AddWithValue("@IdentitasInd1", _FundClientAffiliated.IdentitasInd1);
                        cmd.Parameters.AddWithValue("@HistoryPK", _FundClientAffiliated.HistoryPK);
                        cmd.Parameters.AddWithValue("@InvestorType", _FundClientAffiliated.InvestorType);
                        cmd.Parameters.AddWithValue("@Name", _FundClientAffiliated.Name);
                        cmd.Parameters.AddWithValue("@SID", _FundClientAffiliated.SID);
                        cmd.Parameters.AddWithValue("@NPWP", _FundClientAffiliated.NPWP);
                        cmd.Parameters.AddWithValue("@RegistrationNPWP", _FundClientAffiliated.RegistrationNPWP);
                        cmd.Parameters.AddWithValue("@InvestorsRiskProfile", _FundClientAffiliated.InvestorsRiskProfile);
                        cmd.Parameters.AddWithValue("@KYCRiskProfile", _FundClientAffiliated.KYCRiskProfile);
                        cmd.Parameters.AddWithValue("@BitShareAbleToGroup", _FundClientAffiliated.BitShareAbleToGroup);
                        cmd.Parameters.AddWithValue("@AssetOwner", _FundClientAffiliated.AssetOwner);
                        cmd.Parameters.AddWithValue("@DatePengkinianData", _FundClientAffiliated.DatePengkinianData);
                        cmd.Parameters.AddWithValue("@NamaDepanInd", _FundClientAffiliated.NamaDepanInd);
                        cmd.Parameters.AddWithValue("@NamaTengahInd", _FundClientAffiliated.NamaTengahInd);
                        cmd.Parameters.AddWithValue("@NamaBelakangInd", _FundClientAffiliated.NamaBelakangInd);
                        cmd.Parameters.AddWithValue("@NoIdentitasInd1", _FundClientAffiliated.NoIdentitasInd1);
                        cmd.Parameters.AddWithValue("@RegistrationDateIdentitasInd1", _FundClientAffiliated.RegistrationDateIdentitasInd1);
                        cmd.Parameters.AddWithValue("@ExpiredDateIdentitasInd1", _FundClientAffiliated.ExpiredDateIdentitasInd1);
                        cmd.Parameters.AddWithValue("@OtherAlamatInd1", _FundClientAffiliated.OtherAlamatInd1);
                        cmd.Parameters.AddWithValue("@OtherPropinsiInd1", _FundClientAffiliated.OtherPropinsiInd1);
                        cmd.Parameters.AddWithValue("@OtherNegaraInd1", _FundClientAffiliated.OtherNegaraInd1);
                        cmd.Parameters.AddWithValue("@OtherKodePosInd1", _FundClientAffiliated.OtherKodePosInd1);
                        cmd.Parameters.AddWithValue("@IdentitasInd2", _FundClientAffiliated.IdentitasInd2);
                        cmd.Parameters.AddWithValue("@NoIdentitasInd2", _FundClientAffiliated.NoIdentitasInd2);
                        cmd.Parameters.AddWithValue("@RegistrationDateIdentitasInd2", _FundClientAffiliated.RegistrationDateIdentitasInd2);
                        cmd.Parameters.AddWithValue("@ExpiredDateIdentitasInd2", _FundClientAffiliated.ExpiredDateIdentitasInd2);
                        cmd.Parameters.AddWithValue("@AlamatInd2", _FundClientAffiliated.AlamatInd2);
                        cmd.Parameters.AddWithValue("@DomicileRT", _FundClientAffiliated.DomicileRT);
                        cmd.Parameters.AddWithValue("@DomicileRW", _FundClientAffiliated.DomicileRW);
                        cmd.Parameters.AddWithValue("@KodeKotaInd2", _FundClientAffiliated.KodeKotaInd2);
                        cmd.Parameters.AddWithValue("@KodeDomisiliPropinsi", _FundClientAffiliated.KodeDomisiliPropinsi);
                        cmd.Parameters.AddWithValue("@CountryofDomicile", _FundClientAffiliated.CountryofDomicile);
                        cmd.Parameters.AddWithValue("@KodePosInd2", _FundClientAffiliated.KodePosInd2);
                        cmd.Parameters.AddWithValue("@TempatLahir", _FundClientAffiliated.TempatLahir);
                        cmd.Parameters.AddWithValue("@TanggalLahir", _FundClientAffiliated.TanggalLahir);
                        cmd.Parameters.AddWithValue("@JenisKelamin", _FundClientAffiliated.JenisKelamin);
                        cmd.Parameters.AddWithValue("@Nationality", _FundClientAffiliated.Nationality);
                        cmd.Parameters.AddWithValue("@CountryOfBirth", _FundClientAffiliated.CountryOfBirth);
                        cmd.Parameters.AddWithValue("@Agama", _FundClientAffiliated.Agama);
                        cmd.Parameters.AddWithValue("@OtherAgama", _FundClientAffiliated.OtherAgama);
                        cmd.Parameters.AddWithValue("@Pendidikan", _FundClientAffiliated.Pendidikan);
                        cmd.Parameters.AddWithValue("@OtherPendidikan", _FundClientAffiliated.OtherPendidikan);
                        cmd.Parameters.AddWithValue("@MotherMaidenName", _FundClientAffiliated.MotherMaidenName);
                        cmd.Parameters.AddWithValue("@AhliWaris", _FundClientAffiliated.AhliWaris);
                        cmd.Parameters.AddWithValue("@HubunganAhliWaris", _FundClientAffiliated.HubunganAhliWaris);
                        cmd.Parameters.AddWithValue("@Pekerjaan", _FundClientAffiliated.Pekerjaan);
                        cmd.Parameters.AddWithValue("@OtherOccupation", _FundClientAffiliated.OtherOccupation);
                        cmd.Parameters.AddWithValue("@NatureOfBusiness", _FundClientAffiliated.NatureOfBusiness);
                        cmd.Parameters.AddWithValue("@NatureOfBusinessLainnya", _FundClientAffiliated.NatureOfBusinessLainnya);
                        cmd.Parameters.AddWithValue("@PenghasilanInd", _FundClientAffiliated.PenghasilanInd);
                        cmd.Parameters.AddWithValue("@SumberDanaInd", _FundClientAffiliated.SumberDanaInd);
                        cmd.Parameters.AddWithValue("@MaksudTujuanInd", _FundClientAffiliated.MaksudTujuanInd);
                        cmd.Parameters.AddWithValue("@StatusPerkawinan", _FundClientAffiliated.StatusPerkawinan);
                        cmd.Parameters.AddWithValue("@SpouseName", _FundClientAffiliated.SpouseName);
                        cmd.Parameters.AddWithValue("@SpouseDateOfBirth", _FundClientAffiliated.SpouseDateOfBirth);
                        cmd.Parameters.AddWithValue("@SpouseBirthPlace", _FundClientAffiliated.SpouseBirthPlace);
                        cmd.Parameters.AddWithValue("@SpouseOccupation", _FundClientAffiliated.SpouseOccupation);
                        cmd.Parameters.AddWithValue("@OtherSpouseOccupation", _FundClientAffiliated.OtherSpouseOccupation);
                        cmd.Parameters.AddWithValue("@SpouseNatureOfBusiness", _FundClientAffiliated.SpouseNatureOfBusiness);
                        cmd.Parameters.AddWithValue("@SpouseNatureOfBusinessOther", _FundClientAffiliated.SpouseNatureOfBusinessOther);
                        cmd.Parameters.AddWithValue("@SpouseIDNo", _FundClientAffiliated.SpouseIDNo);
                        cmd.Parameters.AddWithValue("@SpouseNationality", _FundClientAffiliated.SpouseNationality);
                        cmd.Parameters.AddWithValue("@SpouseAnnualIncome", _FundClientAffiliated.SpouseAnnualIncome);
                        cmd.Parameters.AddWithValue("@NamaKantor", _FundClientAffiliated.NamaKantor);
                        cmd.Parameters.AddWithValue("@EmployerLineOfBusiness", _FundClientAffiliated.EmployerLineOfBusiness);
                        cmd.Parameters.AddWithValue("@JabatanKantor", _FundClientAffiliated.JabatanKantor);
                        cmd.Parameters.AddWithValue("@TeleponKantor", _FundClientAffiliated.TeleponKantor);
                        cmd.Parameters.AddWithValue("@AlamatKantorInd", _FundClientAffiliated.AlamatKantorInd);
                        cmd.Parameters.AddWithValue("@KodeKotaKantorInd", _FundClientAffiliated.KodeKotaKantorInd);
                        cmd.Parameters.AddWithValue("@KodePropinsiKantorInd", _FundClientAffiliated.KodePropinsiKantorInd);
                        cmd.Parameters.AddWithValue("@KodeCountryofKantor", _FundClientAffiliated.KodeCountryofKantor);
                        cmd.Parameters.AddWithValue("@KodePosKantorInd", _FundClientAffiliated.KodePosKantorInd);
                        cmd.Parameters.AddWithValue("@Politis", _FundClientAffiliated.Politis);
                        cmd.Parameters.AddWithValue("@PolitisLainnya", _FundClientAffiliated.PolitisLainnya);
                        cmd.Parameters.AddWithValue("@PolitisRelation", _FundClientAffiliated.PolitisRelation);
                        cmd.Parameters.AddWithValue("@PolitisName", _FundClientAffiliated.PolitisName);
                        cmd.Parameters.AddWithValue("@PolitisFT", _FundClientAffiliated.PolitisFT);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _FundClientAffiliated.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);

                        cmd.ExecuteNonQuery();

                        return 0;
                    }
                }

            }
            catch (Exception err)
            {
                throw err;
            }

        }

        

        public void FundClientAffiliated_Approved(FundClientAffiliated _FundClientAffiliated)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"update FundClientAffiliated set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,LastUpdate=@lastupdate 
                            where NoIdentitasInd1 = @NoIdentitasInd1 and historypk = @historyPK


                            update A set 
							A.IdentitasInd1 = B.IdentitasInd1, A.InvestorType = B.InvestorType, A.Name = B.Name, A.SID = B.SID, A.NPWP = B.NPWP, A.RegistrationNPWP = B.RegistrationNPWP, A.InvestorsRiskProfile = B.InvestorsRiskProfile,
							A.KYCRiskProfile = B.KYCRiskProfile, A.BitShareAbleToGroup = B.BitShareAbleToGroup, A.AssetOwner = B.AssetOwner, A.DatePengkinianData = B.DatePengkinianData, A.NamaDepanInd = B.NamaDepanInd, A.NamaTengahInd = B.NamaTengahInd,
							A.NamaBelakangInd = B.NamaBelakangInd, A.NoIdentitasInd1 = B.NoIdentitasInd1, A.RegistrationDateIdentitasInd1 = B.RegistrationDateIdentitasInd1, A.ExpiredDateIdentitasInd1 = B.ExpiredDateIdentitasInd1,
							A.OtherAlamatInd1 = B.OtherAlamatInd1, A.OtherPropinsiInd1 = B.OtherPropinsiInd1, A.OtherNegaraInd1 = B.OtherNegaraInd1, A.OtherKodePosInd1 = B.OtherKodePosInd1, A.IdentitasInd2 = B.IdentitasInd2, A.NoIdentitasInd2 = B.NoIdentitasInd2,
							A.RegistrationDateIdentitasInd2 = B.RegistrationDateIdentitasInd2, A.ExpiredDateIdentitasInd2 = B.ExpiredDateIdentitasInd2, A.AlamatInd2 = B.AlamatInd2, A.DomicileRT = B.DomicileRT, A.DomicileRW = B.DomicileRW, 
							A.KodeKotaInd2 = B.KodeKotaInd2, A.KodeDomisiliPropinsi = B.KodeDomisiliPropinsi, A.CountryofDomicile = B.CountryofDomicile, A.KodePosInd2 = B.KodePosInd2, A.TempatLahir = B.TempatLahir, A.TanggalLahir = B.TanggalLahir,
							A.JenisKelamin = B.JenisKelamin, A.Nationality = B.Nationality, A.CountryOfBirth = B.CountryOfBirth, A.Agama = B.Agama, A.OtherAgama = B.OtherAgama, A.Pendidikan = B.Pendidikan, A.OtherPendidikan = B.OtherPendidikan,
							A.MotherMaidenName = B.MotherMaidenName, A.AhliWaris = B.AhliWaris, A.HubunganAhliWaris = B.HubunganAhliWaris, A.Pekerjaan = B.Pekerjaan, A.OtherOccupation = B.OtherOccupation, A.NatureOfBusiness = B.NatureOfBusiness,
							A.NatureOfBusinessLainnya = B.NatureOfBusinessLainnya, A.PenghasilanInd = B.PenghasilanInd, A.SumberDanaInd = B.SumberDanaInd, A.MaksudTujuanInd = B.MaksudTujuanInd, A.StatusPerkawinan = B.StatusPerkawinan,
							A.SpouseName = B.SpouseName, A.SpouseDateOfBirth = B.SpouseDateOfBirth, A.SpouseBirthPlace = B.SpouseBirthPlace, A.SpouseOccupation = B.SpouseOccupation, A.OtherSpouseOccupation = B.OtherSpouseOccupation,
							A.SpouseNatureOfBusiness = B.SpouseNatureOfBusiness, A.SpouseNatureOfBusinessOther = B.SpouseNatureOfBusinessOther, A.SpouseIDNo = B.SpouseIDNo, A.SpouseNationality = B.SpouseNationality,
							A.SpouseAnnualIncome = B.SpouseAnnualIncome, A.NamaKantor = B.NamaKantor, A.EmployerLineOfBusiness = B.EmployerLineOfBusiness, A.JabatanKantor = B.JabatanKantor, A.TeleponKantor = B.TeleponKantor,
							A.AlamatKantorInd = B.AlamatKantorInd, A.KodeKotaKantorInd = B.KodeKotaKantorInd, A.KodePropinsiKantorInd = B.KodePropinsiKantorInd, A.KodeCountryofKantor = B.KodeCountryofKantor,
							A.KodePosKantorInd = B.KodePosKantorInd, A.Politis = B.Politis, A.PolitisLainnya = B.PolitisLainnya, A.PolitisRelation = B.PolitisRelation, A.PolitisName = B.PolitisName, A.PolitisFT = B.PolitisFT,
							A.UpdateUsersID = @ApprovedUsersID, A.UpdateTime = @ApprovedTime, A.LastUpdate = @LastUpdate
                            from FundClient A
                            left join FundClientAffiliated B on A.NoIdentitasInd1 = B.NoIdentitasInd1 and B.status = 2
                            where A.NoIdentitasInd1 = @NoIdentitasInd1 and A.Status in (1,2)
                        ";
                        cmd.Parameters.AddWithValue("@NoIdentitasInd1", _FundClientAffiliated.NoIdentitasInd1);
                        cmd.Parameters.AddWithValue("@historyPK", _FundClientAffiliated.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _FundClientAffiliated.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update FundClientAffiliated set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where NoIdentitasInd1 = @NoIdentitasInd1 and status = 4";
                        cmd.Parameters.AddWithValue("@NoIdentitasInd1", _FundClientAffiliated.NoIdentitasInd1);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _FundClientAffiliated.ApprovedUsersID);
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

        public void FundClientAffiliated_Reject(FundClientAffiliated _FundClientAffiliated)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update FundClientAffiliated set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where NoIdentitasInd1 = @NoIdentitasInd1 and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@NoIdentitasInd1", _FundClientAffiliated.NoIdentitasInd1);
                        cmd.Parameters.AddWithValue("@historyPK", _FundClientAffiliated.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _FundClientAffiliated.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update FundClientAffiliated set status= 2,LastUpdate=@LastUpdate where NoIdentitasInd1 = @NoIdentitasInd1 and status = 4";
                        cmd.Parameters.AddWithValue("@NoIdentitasInd1", _FundClientAffiliated.NoIdentitasInd1);
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

    }
}