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
using System.Drawing;
using System.Data;

namespace RFSRepository
{

    public class AddPeriod
    {
        int PeriodPK { get; set; }
        string ID { get; set; }
    }



    public class ReturnPeriod
    {
        int PeriodPK { get; set; }
        string ID { get; set; }
    }


    public class AddProfile
    {
        public string FOID { get; set; }
        public int FundclientPK { get; set; }
        public string IFUACode { get; set; }
        public string FrontID { get; set; }
        public string NamaDepanInd { get; set; }
        public string NamaTengahInd { get; set; }
        public string NamaBelakangInd { get; set; }
        public string Nationality { get; set; }
        public string NoIdentitasInd1 { get; set; }
        public string IdentitasInd1 { get; set; }
        public string NPWP { get; set; }
        public string RegistrationNPWP { get; set; }
        public string CountryOfBirth { get; set; }
        public string TempatLahir { get; set; }
        public string TanggalLahir { get; set; }
        public string JenisKelamin { get; set; }
        public string Pendidikan { get; set; }
        public string MotherMaidenName { get; set; }
        public string Agama { get; set; }
        public string Pekerjaan { get; set; }
        public string PenghasilanInd { get; set; }
        public string StatusPerkawinan { get; set; }
        public string SpouseName { get; set; }
        public string InvestorsRiskProfile { get; set; }
        public string MaksudTujuanInd { get; set; }
        public string SumberDanaInd { get; set; }
        public string AssetOwner { get; set; }
        public string OtherAlamatInd1 { get; set; }
        public string OtherKodeKotaInd1 { get; set; }
        public string OtherKodePosInd1 { get; set; }
        public string AlamatInd1 { get; set; }
        public string KodeKotaInd1 { get; set; }
        public string KodePosInd1 { get; set; }
        public string CountryofCorrespondence { get; set; }
        public string AlamatInd2 { get; set; }
        public string KodeKotaInd2 { get; set; }
        public string KodePosInd2 { get; set; }
        public string CountryofDomicile { get; set; }
        public string TeleponRumah { get; set; }
        public string TeleponSelular { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string StatementType { get; set; }
        public string FATCA { get; set; }
        public string TIN { get; set; }
        public string TINIssuanceCountry { get; set; }
        public string NamaBank1 { get; set; }
        public string BankBranchName1 { get; set; }
        public string MataUang1 { get; set; }
        public string NomorRekening1 { get; set; }
        public string NamaNasabah1 { get; set; }
        public string NamaBank2 { get; set; }
        public string BankBranchName2 { get; set; }
        public string MataUang2 { get; set; }
        public string NomorRekening2 { get; set; }
        public string NamaNasabah2 { get; set; }
        public string NamaBank3 { get; set; }
        public string BankBranchName3 { get; set; }
        public string MataUang3 { get; set; }
        public string NomorRekening3 { get; set; }
        public string NamaNasabah3 { get; set; }
    }

    public class AddTransaction
    {
        public string TrxID { get; set; }
        public string IFUACode { get; set; }
        public string NAVDate { get; set; }
        public string FundCode { get; set; }
        public string FundCodeTo { get; set; }
        public string TrxType { get; set; }
        public string Amount { get; set; }
        public string Unit { get; set; }
        public string FeePercent { get; set; }
        public string FeeAmount { get; set; }
        public string SourceOfFund { get; set; }
        public string BankRecipient { get; set; }
        public string Type { get; set; }
    }

    public class GetExistingClient
    {
        public string IDCardNo { get; set; }
        public string TanggalLahir { get; set; }
        public string Email { get; set; }
        public string NamaSesuaiKTP { get; set; }
    }

    public class ReturnGetExistingClient
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public string IFUACode { get; set; }
        public string SID { get; set; }
        public string Verify { get; set; }
        public string NamaDepanInd { get; set; }
        public string NamaTengahInd { get; set; }
        public string NamaBelakangInd { get; set; }
        public string Nationality { get; set; }
        public string NoIdentitasInd1 { get; set; }
        public string IdentitasInd1 { get; set; }
        public string NPWP { get; set; }
        public string RegistrationNPWP { get; set; }
        public string CountryOfBirth { get; set; }
        public string TempatLahir { get; set; }
        public string TanggalLahir { get; set; }
        public string JenisKelamin { get; set; }
        public string Pendidikan { get; set; }
        public string MotherMaidenName { get; set; }
        public string Agama { get; set; }
        public string Pekerjaan { get; set; }
        public string PenghasilanInd { get; set; }
        public string StatusPerkawinan { get; set; }
        public string SpouseName { get; set; }
        public string InvestorsRiskProfile { get; set; }
        public string MaksudTujuanInd { get; set; }
        public string SumberDanaInd { get; set; }
        public string AssetOwner { get; set; }
        public string OtherAlamatInd1 { get; set; }
        public string OtherKodeKotaInd1 { get; set; }
        public string OtherKodePosInd1 { get; set; }
        public string AlamatInd1 { get; set; }
        public string KodeKotaInd1 { get; set; }
        public string KodePosInd1 { get; set; }
        public string CountryofCorrespondence { get; set; }
        public string AlamatInd2 { get; set; }
        public string KodeKotaInd2 { get; set; }
        public string KodePosInd2 { get; set; }
        public string CountryofDomicile { get; set; }
        public string TeleponRumah { get; set; }
        public string TeleponSelular { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string StatementType { get; set; }
        public string FATCA { get; set; }
        public string TIN { get; set; }
        public string TINIssuanceCountry { get; set; }
        public string NamaBank1 { get; set; }
        public string BankBranchName1 { get; set; }
        public string MataUang1 { get; set; }
        public string NomorRekening1 { get; set; }
        public string NamaNasabah1 { get; set; }
        public string NamaBank2 { get; set; }
        public string BankBranchName2 { get; set; }
        public string MataUang2 { get; set; }
        public string NomorRekening2 { get; set; }
        public string NamaNasabah2 { get; set; }
        public string NamaBank3 { get; set; }
        public string BankBranchName3 { get; set; }
        public string MataUang3 { get; set; }
        public string NomorRekening3 { get; set; }
        public string NamaNasabah3 { get; set; }
        public string LastUpdate { get; set; }
    }


    public class RDOReps
    {
        Host _host = new Host();
        //Part GET
        #region MasterValue All

        public class MessageGetMasterValueAll
        {
            public int code { get; set; }
            public string message { get; set; }
            public List<MasterStructureAll> data { get; set; }

        }

        public class MasterStructureAll
        {
            public string Code { get; set; }
            public string ID { get; set; }
            public string Desc { get; set; }
            public string LastUpdate { get; set; }
        }


        private MasterStructureAll setMasterValueAll(SqlDataReader dr)
        {
            MasterStructureAll _m = new MasterStructureAll();
            _m.ID = dr["IDMasterValue"].ToString();
            _m.Code = dr["Code"].ToString();
            _m.Desc = dr["ID"].ToString();
            _m.LastUpdate = dr["LastUpdate"].ToString();
            return _m;
        }


        public MessageGetMasterValueAll GetMasterValueAll()
        {
            try
            {
                MessageGetMasterValueAll _m = new MessageGetMasterValueAll();
                _m.code = 200;
                _m.message = "Get Master Value All Success";

                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<MasterStructureAll> _l = new List<MasterStructureAll>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"
                        select ID IDMasterValue,Code,DescOne ID, CONVERT(VARCHAR(20), isnull(LastUpdate,0), 126) LastUpdate from MasterValue where status = 2";

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    _l.Add(setMasterValueAll(dr));
                                }
                            }
                            _m.data = _l;
                        }
                        return _m;
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public MessageGetMasterValueAll GetMasterValueAllWithLang(string _lang)
        {
            try
            {
                MessageGetMasterValueAll _m = new MessageGetMasterValueAll();
                _m.code = 200;
                _m.message = "Get Master Value All With lang Success";

                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<MasterStructureAll> _l = new List<MasterStructureAll>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        if (_lang.ToLower() == "en")
                        {
                            cmd.CommandText = @"
                        select ID IDMasterValue,Code,isnull(DescOne,'') ID, CONVERT(VARCHAR(20), isnull(LastUpdate,0), 126) LastUpdate
from MasterValue where status = 2";


                        }
                        else
                        {
                            cmd.CommandText = @"
                        select ID IDMasterValue,Code,isnull(DescTwo,'') ID, CONVERT(VARCHAR(20), isnull(LastUpdate,0), 126) LastUpdate
from MasterValue where status = 2";

                        }
                        
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    _l.Add(setMasterValueAll(dr));
                                }
                            }
                            _m.data = _l;
                        }
                        return _m;
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }
        #endregion

        #region Master Bank,Currency,MasterValue
        public class MessageGetMaster
        {
            public int code { get; set; }
            public string message { get; set; }
            public List<MasterStructure> data { get; set; }

        }

        public class MasterStructure
        {
            public int Code { get; set; }
            public string Desc { get; set; }
            public string LastUpdate { get; set; }
        }

        public class MessageGetCurrency
        {
            public int code { get; set; }
            public string message { get; set; }
            public List<MasterCurrency> data { get; set; }

        }
        public class MasterCurrency
        {
            public string Code { get; set; }
            public string Desc { get; set; }
            public string LastUpdate { get; set; }
        }

        private MasterStructure setMaster(SqlDataReader dr)
        {
            MasterStructure _m = new MasterStructure();
            _m.Code = Convert.ToInt32(dr["Code"]);
            _m.Desc = dr["ID"].ToString();
            _m.LastUpdate = dr["LastUpdate"].ToString();
            return _m;
        }

        private MasterCurrency setMasterCurrency(SqlDataReader dr)
        {
            MasterCurrency _m = new MasterCurrency();
            _m.Code = dr["Code"].ToString();
            _m.Desc = dr["ID"].ToString();
            _m.LastUpdate = dr["LastUpdate"].ToString();
            return _m;
        }
        public MessageGetMaster GetMasterBank()
        {
            try
            {
                MessageGetMaster _m = new MessageGetMaster();
                _m.code = 200;
                _m.message = "Get Bank Success";

                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<MasterStructure> _l = new List<MasterStructure>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"Select BankPK Code, isnull(ID,'') 
ID,CONVERT(VARCHAR(20), isnull(LastUpdate,0), 126) LastUpdate from Bank Where status = 2 order by BankPK Asc";

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    _l.Add(setMaster(dr));
                                }
                            }
                            _m.data = _l;
                        }
                        return _m;
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public MessageGetCurrency GetMasterCurrency()
        {
            try
            {
                MessageGetCurrency _m = new MessageGetCurrency();
                _m.code = 200;
                _m.message = "Get Currency Success";

                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<MasterCurrency> _l = new List<MasterCurrency>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"Select isnull(ID,'') Code,isnull(Name,'') ID,CONVERT(VARCHAR(20), isnull(LastUpdate,0), 126) LastUpdate from Currency Where status = 2 order by CurrencyPK Asc";

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    _l.Add(setMasterCurrency(dr));
                                }
                            }
                            _m.data = _l;
                        }
                        return _m;
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public MessageGetMaster GetMasterValue(string _param)
        {
            try
            {
                MessageGetMaster _m = new MessageGetMaster();
                _m.code = 200;
                _m.message = "Get " + _param + " Success";

                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<MasterStructure> _l = new List<MasterStructure>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"
                        select isnull(Code,'') Code,isnull(DescOne,'') ID, CONVERT(VARCHAR(20), isnull(LastUpdate,0), 126) LastUpdate from MasterValue where id = @Param and status = 2";

                        cmd.Parameters.AddWithValue("@Param", _param);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    _l.Add(setMaster(dr));
                                }
                            }
                            _m.data = _l;
                        }
                        return _m;
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public MessageGetMaster GetMasterValueWithLang(string _param, string _lang)
        {
            try
            {
                MessageGetMaster _m = new MessageGetMaster();
                _m.code = 200;
                _m.message = "Get " + _param + " Success";

                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<MasterStructure> _l = new List<MasterStructure>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_lang.ToLower() == "en")
                        {
                            cmd.CommandText = @"
                        select isnull(Code,'') Code,isnull(DescOne,'') ID, CONVERT(VARCHAR(20), isnull(LastUpdate,0), 126) LastUpdate 
                        from MasterValue where id = @Param and status = 2";

                        }
                        else {
                            cmd.CommandText = @"
                        select isnull(Code,'') Code,isnull(DescTwo,'') ID, CONVERT(VARCHAR(20), isnull(LastUpdate,0), 126) LastUpdate 
                        from MasterValue where id = @Param and status = 2";

                        }
                        
                        
                        cmd.Parameters.AddWithValue("@Param", _param);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    _l.Add(setMaster(dr));
                                }
                            }
                            _m.data = _l;
                        }
                        return _m;
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }
        #endregion

        #region Fund
        public class MessageGetMasterFund
        {
            public int code { get; set; }
            public string message { get; set; }
            public List<MasterFund> data { get; set; }
        }
        public class MasterFund
        {
            public int Code { get; set; }
            public string FundID { get; set; }
            public string FundName { get; set; }
            public string BankCustodian { get; set; }
            public string Currency { get; set; }
            public string FundType { get; set; }
            public string EffectiveDate { get; set; }
            public string MaturityDate { get; set; }
            public decimal CustodianFeePercent { get; set; }
            public decimal ManagementFeePercent { get; set; }
            public decimal SubscriptionFeePercent { get; set; }
            public string RekNumber { get; set; }
            public string RekName { get; set; }
            public decimal MinSubscriptionAmount { get; set; }
            public decimal MinSwitchUnit { get; set; }
            public decimal MinSwitchAmount { get; set; }
            public decimal MinRemainingBalanceUnit { get; set; }
            public decimal MinRedemptionAmount { get; set; }
            public decimal MinRedemptionUnit { get; set; }
            public string LastUpdate { get; set; }
            public int RiskProfileCode { get; set; }
            public string RiskProfileDesc { get; set; }
        }
        private MasterFund setMasterFund(SqlDataReader dr)
        {
            MasterFund _m = new MasterFund();
            _m.Code = Convert.ToInt32(dr["Code"]);
            _m.FundID = dr["FundID"].ToString();
            _m.FundName = dr["FundName"].ToString();
            _m.BankCustodian = dr["BankCustodian"].ToString();
            _m.Currency = dr["Currency"].ToString();
            _m.FundType = dr["FundType"].ToString();
            _m.EffectiveDate = Convert.ToDateTime(dr["EffectiveDate"].ToString()).ToString("yyyy-MM-dd");
            _m.MaturityDate = Convert.ToDateTime(dr["MaturityDate"].ToString()).ToString("yyyy-MM-dd");
            _m.CustodianFeePercent = Convert.ToDecimal(dr["CustodyFeePercent"]);
            _m.ManagementFeePercent = Convert.ToDecimal(dr["ManagementFeePercent"]);
            _m.SubscriptionFeePercent = Convert.ToDecimal(dr["SubscriptionFeePercent"]);
            _m.RekNumber = dr["RekNumber"].ToString();
            _m.RekName = dr["RekName"].ToString();
            _m.MinSubscriptionAmount = Convert.ToDecimal(dr["MinSubscriptionAmount"]);
            _m.MinSwitchUnit = Convert.ToDecimal(dr["MinSwitchUnit"]);
            _m.MinSwitchAmount = Convert.ToDecimal(dr["MinSwitchAmount"]);
            _m.MinRemainingBalanceUnit = Convert.ToDecimal(dr["MinRemainingBalanceUnit"]);
            _m.MinRedemptionAmount = Convert.ToDecimal(dr["MinRedemptionAmount"]);
            _m.MinRedemptionUnit = Convert.ToDecimal(dr["MinRedemptionUnit"]);
            _m.LastUpdate = dr["LastUpdate"].ToString();
            _m.RiskProfileCode =   Convert.ToInt16(dr["RiskProfileCode"]);
            _m.RiskProfileDesc = dr["RiskProfileDesc"].ToString();
            return _m;
        }
        public MessageGetMasterFund GetMasterFund()
        {
            try
            {
                MessageGetMasterFund _m = new MessageGetMasterFund();
                _m.code = 200;
                _m.message = "Get Fund Success";

                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<MasterFund> _l = new List<MasterFund>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"
SELECT 
                        A.FundPK Code
                        ,isnull(A.ID,'') FundID
                        ,isnull(A.Name,'') FundName
                        ,isnull(C.ID,'') BankCustodian
                        ,isnull(D.ID,'') Currency
                        ,isnull(E.DescOne,'') FundType
                        ,isnull(A.EffectiveDate,0) EffectiveDate
                        ,isnull(A.MaturityDate,0) MaturityDate
                        ,isnull(A.CustodyFeePercent,0) / 100 CustodyFeePercent
                        ,isnull(A.ManagementFeePercent,0) / 100 ManagementFeePercent
                        ,isnull(A.SubscriptionFeePercent,0) / 100 SubscriptionFeePercent
                        ,isnull(F.BankAccountNo,'') RekNumber
                        ,isnull(F.BankAccountName,'') RekName
                        ,isnull(A.MinSubs,0) MinSubscriptionAmount
                        ,isnull(A.MinBalSwitchUnit,0) MinSwitchUnit
                        ,isnull(A.MinBalSwitchAmt,0) MinSwitchAmount
                        ,isnull(A.RemainingBalanceUnit,0) MinRemainingBalanceUnit
                        -- TAMBAHAN 2 INI
                        ,isnull(A.MinBalRedsAmt,0) MinRedemptionAmount
                        ,isnull(A.MinBalRedsUnit,0) MinRedemptionUnit
                        --
                        ,CONVERT(VARCHAR(20), isnull(A.LastUpdate,0), 126) LastUpdate
						,ISNULL(G.RiskProfilePK,0) RiskProfileCode
						,ISNULL(H.DescOne,'') RiskProfileDesc
                        FROM fund A
                        LEFT JOIN BankBranch B on A.BankBranchPK = B.BankBranchPK and B.Status in(1,2)
                        LEFT JOIN Bank C ON B.BankPK = C.BankPK AND C.status IN (1,2)
                        LEFT JOIN Currency D ON A.CurrencyPK = D.CurrencyPK AND D.Status IN (1,2)
                        LEFT JOIN MasterValue E ON A.Type = E.Code and E.ID = 'FundType' and E.Status in(1,2)
                        LEFT JOIN FundCashRef F ON A.FundPK = F.FundPK AND F.status IN (1,2) AND F.isPublic = 1 
						LEFT JOIN dbo.FundRiskProfile G ON A.FundPK = G.FundPK AND G.status IN (1,2)
						LEFT JOIN MasterValue H ON H.ID = 'InvestorsRiskProfile' AND G.RiskProfilePK = H.Code AND H.status IN (1,2)
                        WHERE A.Status = 2 AND A.MaturityDate > GETDATE() and A.IsPublic = 1
                        Order By A.FundPK

                        ";

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    _l.Add(setMasterFund(dr));
                                }
                            }
                            _m.data = _l;
                        }
                        return _m;
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public MessageGetMasterFund GetMasterFundByFundCode(string _fundCode)
        {
            try
            {
                MessageGetMasterFund _m = new MessageGetMasterFund();
                _m.code = 200;
                _m.message = "Get Fund By Fund Code Success";

                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<MasterFund> _l = new List<MasterFund>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"SELECT 
                        A.FundPK Code
                        ,isnull(A.ID,'') FundID
                        ,isnull(A.Name,'') FundName
                        ,isnull(C.ID,'') BankCustodian
                        ,isnull(D.ID,'') Currency
                        ,isnull(E.DescOne,'') FundType
                        ,isnull(A.EffectiveDate,0) EffectiveDate
                        ,isnull(A.MaturityDate,0) MaturityDate
                        ,isnull(A.CustodyFeePercent,0) / 100 CustodyFeePercent
                        ,isnull(A.ManagementFeePercent,0) / 100 ManagementFeePercent
                        ,isnull(A.SubscriptionFeePercent,0) / 100 SubscriptionFeePercent
                        ,isnull(F.BankAccountNo,'') RekNumber
                        ,isnull(F.BankAccountName,'') RekName
                        ,isnull(A.MinSubs,0) MinSubscriptionAmount
                        ,isnull(A.MinBalSwitchUnit,0) MinSwitchUnit
                        ,isnull(A.MinBalSwitchAmt,0) MinSwitchAmount
                        ,isnull(A.RemainingBalanceUnit,0) MinRemainingBalanceUnit
                        -- TAMBAHAN 2 INI
                        ,isnull(A.MinBalRedsAmt,0) MinRedemptionAmount
                        ,isnull(A.MinBalRedsUnit,0) MinRedemptionUnit
                        --
                        ,CONVERT(VARCHAR(20), isnull(A.LastUpdate,0), 126) LastUpdate
	,ISNULL(G.RiskProfilePK,0) RiskProfileCode
						,ISNULL(H.DescOne,'') RiskProfileDesc
                        FROM fund A
                        LEFT JOIN BankBranch B on A.BankBranchPK = B.BankBranchPK and B.Status in(1,2)
                        LEFT JOIN Bank C ON B.BankPK = C.BankPK AND C.status IN (1,2)
                        LEFT JOIN Currency D ON A.CurrencyPK = D.CurrencyPK AND D.Status IN (1,2)
                        LEFT JOIN MasterValue E ON A.Type = E.Code and E.ID = 'FundType' and E.Status in(1,2)
                        LEFT JOIN FundCashRef F ON A.FundPK = F.FundPK AND F.status IN (1,2) AND F.isPublic = 1 
	LEFT JOIN dbo.FundRiskProfile G ON A.FundPK = G.FundPK AND G.status IN (1,2)
						LEFT JOIN MasterValue H ON H.ID = 'InvestorsRiskProfile' AND G.RiskProfilePK = H.Code AND H.status IN (1,2)
                        WHERE A.Status = 2 AND A.MaturityDate > GETDATE() and A.ID = @FundCode and A.IsPublic = 1
                        Order By A.FundPK
                        ";
                        cmd.Parameters.AddWithValue("@FundCode", _fundCode);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    _l.Add(setMasterFund(dr));
                                }
                            }
                            _m.data = _l;
                        }
                        return _m;
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }
        #endregion

        #region Matriks Switching
        public class MessageGetMatriksSwitching
        {
            public int code { get; set; }
            public string message { get; set; }
            public List<MasterMatriksSwitching> data { get; set; }
        }
        public class MasterMatriksSwitching
        {
            public string FundFrom { get; set; }
            public string FundTo { get; set; }
            public string LastUpdate { get; set; }
        }
        private MasterMatriksSwitching setMatriksSwitching(SqlDataReader dr)
        {
            MasterMatriksSwitching _m = new MasterMatriksSwitching();
            _m.FundFrom = dr["FundFrom"].ToString();
            _m.FundTo = dr["FundTo"].ToString();
            _m.LastUpdate = dr["LastUpdate"].ToString();
            return _m;
        }
        public MessageGetMatriksSwitching GetMatriksSwithing()
        {
            try
            {
                MessageGetMatriksSwitching _m = new MessageGetMatriksSwitching();
                _m.code = 200;
                _m.message = "Get Matriks Switching Success";

                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<MasterMatriksSwitching> _l = new List<MasterMatriksSwitching>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"SELECT isnull(B.ID,'') FundFrom 
                        ,isnull(C.ID,'') FundTo
                        ,CONVERT(VARCHAR(20), isnull(A.LastUpdate,0), 126) LastUpdate
                        FROM dbo.SwitchingFund A
                        LEFT JOIN dbo.Fund B ON A.FundFromPK = B.FundPK AND B.Status IN (1,2)
                        LEFT JOIN  dbo.Fund C ON A.FundToPK = C.FundPK AND C.Status IN (1,2)
                        WHERE A.Status = 2";

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    _l.Add(setMatriksSwitching(dr));
                                }
                            }
                            _m.data = _l;
                        }
                        return _m;
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public MessageGetMatriksSwitching GetMatriksSwithingByFundCode(string _fundCode)
        {
            try
            {
                MessageGetMatriksSwitching _m = new MessageGetMatriksSwitching();
                _m.code = 200;
                _m.message = "Get Matriks Switching By Fund Code Success";

                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<MasterMatriksSwitching> _l = new List<MasterMatriksSwitching>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"SELECT isnull(B.ID,'') FundFrom 
                        ,isnull(C.ID,'') FundTo
                        ,CONVERT(VARCHAR(20), isnull(A.LastUpdate,0), 126) LastUpdate
                        FROM dbo.SwitchingFund A
                        LEFT JOIN dbo.Fund B ON A.FundFromPK = B.FundPK AND B.Status IN (1,2) and B.IsPublic = 1
                        LEFT JOIN  dbo.Fund C ON A.FundToPK = C.FundPK AND C.Status IN (1,2) and C.IsPublic = 1
                        WHERE A.Status = 2 and B.ID = @FundCode and isnull(C.ID,'') <> '' ";
                        cmd.Parameters.AddWithValue("@FundCode", _fundCode);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    _l.Add(setMatriksSwitching(dr));
                                }
                            }
                            _m.data = _l;
                        }
                        return _m;
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }
        #endregion

        #region VerifyClient
        public class MessageVerifyClient
        {
            public int code { get; set; }
            public string message { get; set; }
            public List<MasterVerifyClient> data { get; set; }

        }

        public class MasterVerifyClient
        {
            public bool CFLAG { get; set; }
            public string FrontID { get; set; }
            public string IFUACode { get; set; }
            public string SID { get; set; }
            public string Verify { get; set; }
            public string NamaDepanInd { get; set; }
            public string NamaTengahInd { get; set; }
            public string NamaBelakangInd { get; set; }
            public string Nationality { get; set; }
            public string NoIdentitasInd1 { get; set; }
            public string IdentitasInd1 { get; set; }
            public string NPWP { get; set; }
            public string RegistrationNPWP { get; set; }
            public string CountryOfBirth { get; set; }
            public string TempatLahir { get; set; }
            public string TanggalLahir { get; set; }
            public string JenisKelamin { get; set; }
            public string Pendidikan { get; set; }
            public string MotherMaidenName { get; set; }
            public string Agama { get; set; }
            public string Pekerjaan { get; set; }
            public string PenghasilanInd { get; set; }
            public string StatusPerkawinan { get; set; }
            public string SpouseName { get; set; }
            public string InvestorsRiskProfile { get; set; }
            public string MaksudTujuanInd { get; set; }
            public string SumberDanaInd { get; set; }
            public string AssetOwner { get; set; }
            public string OtherAlamatInd1 { get; set; }
            public string OtherKodeKotaInd1 { get; set; }
            public string OtherKodePosInd1 { get; set; }
            public string AlamatInd1 { get; set; }
            public string KodeKotaInd1 { get; set; }
            public string KodePosInd1 { get; set; }
            public string CountryofCorrespondence { get; set; }
            public string AlamatInd2 { get; set; }
            public string KodeKotaInd2 { get; set; }
            public string KodePosInd2 { get; set; }
            public string CountryofDomicile { get; set; }
            public string TeleponRumah { get; set; }
            public string TeleponSelular { get; set; }
            public string Fax { get; set; }
            public string Email { get; set; }
            public string StatementType { get; set; }
            public string FATCA { get; set; }
            public string TIN { get; set; }
            public string TINIssuanceCountry { get; set; }
            public string NamaBank1 { get; set; }
            public string BankBranchName1 { get; set; }
            public string MataUang1 { get; set; }
            public string NomorRekening1 { get; set; }
            public string NamaNasabah1 { get; set; }
            public string NamaBank2 { get; set; }
            public string BankBranchName2 { get; set; }
            public string MataUang2 { get; set; }
            public string NomorRekening2 { get; set; }
            public string NamaNasabah2 { get; set; }
            public string NamaBank3 { get; set; }
            public string BankBranchName3 { get; set; }
            public string MataUang3 { get; set; }
            public string NomorRekening3 { get; set; }
            public string NamaNasabah3 { get; set; }
            public string LastUpdate { get; set; }
        }

        private MasterVerifyClient setVerifyClient(SqlDataReader dr)
        {
            MasterVerifyClient _m = new MasterVerifyClient();
            _m.CFLAG = Convert.ToBoolean(dr["CFLAG"]);
            _m.FrontID = dr["FrontID"].ToString();
            _m.IFUACode = dr["IFUACode"].ToString();
            _m.SID = dr["SID"].ToString();
            _m.Verify = dr["Verify"].ToString();
            _m.NamaDepanInd = dr["NamaDepanInd"].ToString();
            _m.NamaTengahInd = dr["NamaTengahInd"].ToString();
            _m.NamaBelakangInd = dr["NamaBelakangInd"].ToString();
            _m.Nationality = dr["Nationality"].ToString();
            _m.NoIdentitasInd1 = dr["NoIdentitasInd1"].ToString();
            _m.IdentitasInd1 = dr["IdentitasInd1"].ToString();
            _m.NPWP = dr["NPWP"].ToString();
            _m.RegistrationNPWP = Convert.ToDateTime(dr["RegistrationNPWP"].ToString()).ToString("yyyy-MM-dd");
            _m.CountryOfBirth = dr["CountryOfBirth"].ToString();
            _m.TempatLahir = dr["TempatLahir"].ToString();
            _m.TanggalLahir = Convert.ToDateTime(dr["TanggalLahir"].ToString()).ToString("yyyy-MM-dd");
            _m.JenisKelamin = dr["JenisKelamin"].ToString();
            _m.Pendidikan = dr["Pendidikan"].ToString();
            _m.MotherMaidenName = dr["MotherMaidenName"].ToString();
            _m.Agama = dr["Agama"].ToString();
            _m.Pekerjaan = dr["Pekerjaan"].ToString();
            _m.PenghasilanInd = dr["PenghasilanInd"].ToString();
            _m.StatusPerkawinan = dr["StatusPerkawinan"].ToString();
            _m.SpouseName = dr["SpouseName"].ToString();
            _m.InvestorsRiskProfile = dr["InvestorsRiskProfile"].ToString();
            _m.MaksudTujuanInd = dr["MaksudTujuanInd"].ToString();
            _m.SumberDanaInd = dr["SumberDanaInd"].ToString();
            _m.AssetOwner = dr["AssetOwner"].ToString();
            _m.OtherAlamatInd1 = dr["OtherAlamatInd1"].ToString();
            _m.OtherKodeKotaInd1 = dr["OtherKodeKotaInd1"].ToString();
            _m.OtherKodePosInd1 = dr["OtherKodePosInd1"].ToString();
            _m.AlamatInd1 = dr["AlamatInd1"].ToString();
            _m.KodeKotaInd1 = dr["KodeKotaInd1"].ToString();
            _m.KodePosInd1 = dr["KodePosInd1"].ToString();
            _m.CountryofCorrespondence = dr["CountryofCorrespondence"].ToString();
            _m.AlamatInd2 = dr["AlamatInd2"].ToString();
            _m.KodeKotaInd2 = dr["KodeKotaInd2"].ToString();
            _m.KodePosInd2 = dr["KodePosInd2"].ToString();
            _m.CountryofDomicile = dr["CountryofDomicile"].ToString();
            _m.TeleponRumah = dr["TeleponRumah"].ToString();
            _m.TeleponSelular = dr["TeleponSelular"].ToString();
            _m.Fax = dr["Fax"].ToString();
            _m.Email = dr["Email"].ToString();
            _m.StatementType = dr["StatementType"].ToString();
            _m.FATCA = dr["FATCA"].ToString();
            _m.TIN = dr["TIN"].ToString();
            _m.TINIssuanceCountry = dr["TINIssuanceCountry"].ToString();
            _m.NamaBank1 = dr["NamaBank1"].ToString();
            //_m.BankBranchName1 = dr["BankBranchName1"].ToString();
            _m.MataUang1 = dr["MataUang1"].ToString();
            _m.NomorRekening1 = dr["NomorRekening1"].ToString();
            _m.NamaNasabah1 = dr["NamaNasabah1"].ToString();
            _m.NamaBank2 = dr["NamaBank2"].ToString();
            //_m.BankBranchName2 = dr["BankBranchName2"].ToString();
            _m.MataUang2 = dr["MataUang2"].ToString();
            _m.NomorRekening2 = dr["NomorRekening2"].ToString();
            _m.NamaNasabah2 = dr["NamaNasabah2"].ToString();
            _m.NamaBank3 = dr["NamaBank3"].ToString();
            //_m.BankBranchName3 = dr["BankBranchName3"].ToString();
            _m.MataUang3 = dr["MataUang3"].ToString();
            _m.NomorRekening3 = dr["NomorRekening3"].ToString();
            _m.NamaNasabah3 = dr["NamaNasabah3"].ToString();
            _m.LastUpdate = dr["LastUpdate"].ToString();
            return _m;
        }

        public MessageVerifyClient VerifyAllClient()
        {
            try
            {
                MessageVerifyClient _m = new MessageVerifyClient();
                _m.code = 200;
                _m.message = "Verify All Client Success";

                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<MasterVerifyClient> _l = new List<MasterVerifyClient>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"select 0 CFLAG,isnull(a.FrontID,'') FrontID, 
                        isnull(a.IFUACode,'') IFUACode, 
                        isnull(a.SID,'') SID, 
                        case when a.Status = 2 and isnull(IFUACode,'') <> '' and a.EntryUsersID = 'rdo' then 'True' else 'False' end Verify,
                        isnull(NamaDepanInd,'') NamaDepanInd,
                        isnull(NamaTengahInd,'') NamaTengahInd,
                        isnull(NamaBelakangInd,'') NamaBelakangInd,
                        isnull(c.DescOne,'') Nationality,
                        isnull(a.NoIdentitasInd1,'') NoIdentitasInd1,
                        isnull(u.DescOne,'') IdentitasInd1,
                        isnull(a.NPWP,'') NPWP,
                        isnull(a.RegistrationNPWP,0) RegistrationNPWP,
                        isnull(b.DescOne,'') CountryOfBirth,
                        isnull(a.TempatLahir,'') TempatLahir, 
                        isnull(a.TanggalLahir,0) TanggalLahir,
                        isnull(d.DescOne,'') JenisKelamin,
                        isnull(e.DescOne,'') Pendidikan,
                        isnull(a.MotherMaidenName,'') MotherMaidenName,
                        isnull(f.DescOne,'') Agama,
                        isnull(g.DescOne,'') Pekerjaan,
                        isnull(h.DescOne,'') PenghasilanInd,
                        isnull(i.DescOne,'') StatusPerkawinan,
                        isnull(a.SpouseName,'') SpouseName,
                        isnull(j.DescOne,'') InvestorsRiskProfile,
                        isnull(k.DescOne,'') MaksudTujuanInd,
                        isnull(l.DescOne,'') SumberDanaInd,
                        isnull(m.DescOne,'') AssetOwner,
                        isnull(a.OtherAlamatInd1,'') OtherAlamatInd1,
                        isnull(a.OtherKodeKotaInd1,'') OtherKodeKotaInd1,
                        isnull(a.OtherKodePosInd1,'') OtherKodePosInd1,
                        isnull(a.AlamatInd1,'') AlamatInd1,
                        isnull(n.DescOne,'') KodeKotaInd1,
                        isnull(a.KodePosInd1,'') KodePosInd1,
                        isnull(o.DescOne,'') CountryofCorrespondence,
                        isnull(a.AlamatInd2,'') AlamatInd2,
                        isnull(p.DescOne,'') KodeKotaInd2,
                        isnull(a.KodePosInd2,'') KodePosInd2,
                        isnull(q.DescOne,'') CountryofDomicile,
                        isnull(a.TeleponRumah,'') TeleponRumah,
                        isnull(a.TeleponSelular,'') TeleponSelular,
                        isnull(a.Fax,'') Fax,
                        isnull(a.Email,'') Email,
                        isnull(r.DescOne,'') StatementType,
                        isnull(s.DescOne,'') FATCA,
                        isnull(a.TIN,'') TIN,
                        isnull(t.DescOne,'') TINIssuanceCountry, 
                        isnull(BC1.Name,'') NamaBank1, 
                        isnull(CC1.ID,'') MataUang1, 
                        isnull(a.NomorRekening1,'') NomorRekening1,
                        isnull(a.NamaNasabah1,'') NamaNasabah1,
                        isnull(BC2.Name,'') NamaBank2,
                        isnull(CC2.ID,'') MataUang2,
                        isnull(a.NomorRekening2,'') NomorRekening2,
                        isnull(a.NamaNasabah2,'') NamaNasabah2,
                        isnull(BC3.Name,'') NamaBank3, 
                        isnull(CC3.ID,'') MataUang3 , 
                        isnull(a.NomorRekening3,'') NomorRekening3, 
                        isnull(a.NamaNasabah3,'') NamaNasabah3,
                        CONVERT(VARCHAR(20), isnull(a.LastUpdate,0), 126) LastUpdate from FundClient a left join
                        MasterValue b on a.CountryOfBirth = b.Code and b.ID = 'SDICountry' and b.Status in(1,2) left join
                        MasterValue c on a.Nationality = c.Code and c.ID = 'SDICountry' and c.Status in(1,2) left join 
                        MasterValue d on a.JenisKelamin = d.Code and d.ID = 'Sex' and d.Status in(1,2) left join
                        MasterValue e on a.Pendidikan = e.Code and e.ID = 'EducationalBackground' and e.Status in(1,2) left join
                        MasterValue f on a.Agama = f.Code and f.ID = 'Religion' and f.Status in(1,2) left join
                        MasterValue g on a.Pekerjaan = g.Code and g.ID = 'Occupation' and g.Status in(1,2) left join
                        MasterValue h on a.PenghasilanInd = h.Code and h.ID = 'IncomeIND' and h.Status in(1,2) left join
                        MasterValue i on a.StatusPerkawinan = i.Code and i.ID = 'MaritalStatus' and i.Status in(1,2) left join
                        MasterValue j on a.InvestorsRiskProfile = j.Code and j.ID = 'InvestorsRiskProfile' and j.Status in(1,2) left join
                        MasterValue k on a.MaksudTujuanInd = k.Code and k.ID = 'InvestmentObjectivesIND' and k.Status in(1,2) left join
                        MasterValue l on a.SumberDanaInd = l.Code and l.ID = 'IncomeSourceIND' and l.Status in(1,2) left join
                        MasterValue m on a.AssetOwner = m.Code and m.ID = 'AssetOwner' and m.Status in(1,2) left join
                        MasterValue n on a.KodeKotaInd1 = n.Code and n.ID = 'CityRHB' and n.Status in(1,2) left join
                        MasterValue o on a.CountryofCorrespondence = o.Code and o.ID = 'SDICountry' and o.Status in(1,2) left join
                        MasterValue p on a.KodeKotaInd2 = p.Code and p.ID = 'CityRHB' and p.Status in(1,2) left join
                        MasterValue q on a.CountryofDomicile = q.Code and q.ID = 'SDICountry' and q.Status in(1,2) left join
                        MasterValue r on a.StatementType = r.Code and r.ID = 'StatementType' and r.Status in(1,2) left join
                        MasterValue s on a.FATCA = s.Code and s.ID = 'FATCA' and s.Status in(1,2) left join
                        MasterValue t on a.TINIssuanceCountry = t.Code and t.ID = 'SDICountry' and t.Status in(1,2) left join 
                        MasterValue u on a.IdentitasInd1 = u.Code and t.ID = 'Identity' and u.Status in(1,2) left join 
                        Bank BC1 on a.NamaBank1 = BC1.BankPK and BC1.status = 2  left join 
                        Bank BC2 on a.NamaBank2 = BC2.BankPK and BC2.status = 2  left join 
                        Bank BC3 on a.NamaBank3 = BC3.BankPK and BC3.status = 2  left join
                        Currency CC1 on a.MataUang1 = CC1.CurrencyPK and CC1.status = 2  left join
                        Currency CC2 on a.MataUang2 = CC2.CurrencyPK and CC2.status = 2  left join
                        Currency CC3 on a.MataUang3 = CC3.CurrencyPK and CC3.status = 2  
                        Where A.Status not in (3,4) and isnull(frontID,'') <> ''
                        order by FundClientPK desc";

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    _l.Add(setVerifyClient(dr));
                                }
                            }
                            _m.data = _l;
                        }
                        return _m;
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }


        public MessageVerifyClient VerifyClientByIFUACode(string _ifuaCode)
        {
            try
            {
                MessageVerifyClient _m = new MessageVerifyClient();
                _m.code = 200;
                _m.message = "Verify Client By IFUACode Success";

                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<MasterVerifyClient> _l = new List<MasterVerifyClient>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"select 0 CFLAG,isnull(a.FrontID,'') FrontID, 
                        isnull(a.IFUACode,'') IFUACode, 
                        isnull(a.SID,'') SID,
                        case when a.Status = 2 and isnull(IFUACode,'') <> '' and a.EntryUsersID = 'rdo' then 'True' else 'False' end Verify,
                        isnull(NamaDepanInd,'') NamaDepanInd,
                        isnull(NamaTengahInd,'') NamaTengahInd,
                        isnull(NamaBelakangInd,'') NamaBelakangInd,
                        isnull(c.DescOne,'') Nationality,
                        isnull(a.NoIdentitasInd1,'') NoIdentitasInd1,
                        isnull(u.DescOne,'') IdentitasInd1,
                        isnull(a.NPWP,'') NPWP,
                        isnull(a.RegistrationNPWP,0) RegistrationNPWP,
                        isnull(b.DescOne,'') CountryOfBirth,
                        isnull(a.TempatLahir,'') TempatLahir, 
                        isnull(a.TanggalLahir,0) TanggalLahir,
                        isnull(d.DescOne,'') JenisKelamin,
                        isnull(e.DescOne,'') Pendidikan,
                        isnull(a.MotherMaidenName,'') MotherMaidenName,
                        isnull(f.DescOne,'') Agama,
                        isnull(g.DescOne,'') Pekerjaan,
                        isnull(h.DescOne,'') PenghasilanInd,
                        isnull(i.DescOne,'') StatusPerkawinan,
                        isnull(a.SpouseName,'') SpouseName,
                        isnull(j.DescOne,'') InvestorsRiskProfile,
                        isnull(k.DescOne,'') MaksudTujuanInd,
                        isnull(l.DescOne,'') SumberDanaInd,
                        isnull(m.DescOne,'') AssetOwner,
                        isnull(a.OtherAlamatInd1,'') OtherAlamatInd1,
                        isnull(a.OtherKodeKotaInd1,'') OtherKodeKotaInd1,
                        isnull(a.OtherKodePosInd1,'') OtherKodePosInd1,
                        isnull(a.AlamatInd1,'') AlamatInd1,
                        isnull(n.DescOne,'') KodeKotaInd1,
                        isnull(a.KodePosInd1,'') KodePosInd1,
                        isnull(o.DescOne,'') CountryofCorrespondence,
                        isnull(a.AlamatInd2,'') AlamatInd2,
                        isnull(p.DescOne,'') KodeKotaInd2,
                        isnull(a.KodePosInd2,'') KodePosInd2,
                        isnull(q.DescOne,'') CountryofDomicile,
                        isnull(a.TeleponRumah,'') TeleponRumah,
                        isnull(a.TeleponSelular,'') TeleponSelular,
                        isnull(a.Fax,'') Fax,
                        isnull(a.Email,'') Email,
                        isnull(r.DescOne,'') StatementType,
                        isnull(s.DescOne,'') FATCA,
                        isnull(a.TIN,'') TIN,
                        isnull(t.DescOne,'') TINIssuanceCountry, 
                        isnull(BC1.Name,'') NamaBank1, 
                        isnull(CC1.ID,'') MataUang1, 
                        isnull(a.NomorRekening1,'') NomorRekening1,
                        isnull(a.NamaNasabah1,'') NamaNasabah1,
                        isnull(BC2.Name,'') NamaBank2,
                        isnull(CC2.ID,'') MataUang2,
                        isnull(a.NomorRekening2,'') NomorRekening2,
                        isnull(a.NamaNasabah2,'') NamaNasabah2,
                        isnull(BC3.Name,'') NamaBank3, 
                        isnull(CC3.ID,'') MataUang3 , 
                        isnull(a.NomorRekening3,'') NomorRekening3, 
                        isnull(a.NamaNasabah3,'') NamaNasabah3,
                        CONVERT(VARCHAR(20), isnull(a.LastUpdate,0), 126) LastUpdate from FundClient a left join
                        MasterValue b on a.CountryOfBirth = b.Code and b.ID = 'SDICountry' and b.Status in(1,2) left join
                        MasterValue c on a.Nationality = c.Code and c.ID = 'SDICountry' and c.Status in(1,2) left join 
                        MasterValue d on a.JenisKelamin = d.Code and d.ID = 'Sex' and d.Status in(1,2) left join
                        MasterValue e on a.Pendidikan = e.Code and e.ID = 'EducationalBackground' and e.Status in(1,2) left join
                        MasterValue f on a.Agama = f.Code and f.ID = 'Religion' and f.Status in(1,2) left join
                        MasterValue g on a.Pekerjaan = g.Code and g.ID = 'Occupation' and g.Status in(1,2) left join
                        MasterValue h on a.PenghasilanInd = h.Code and h.ID = 'IncomeIND' and h.Status in(1,2) left join
                        MasterValue i on a.StatusPerkawinan = i.Code and i.ID = 'MaritalStatus' and i.Status in(1,2) left join
                        MasterValue j on a.InvestorsRiskProfile = j.Code and j.ID = 'InvestorsRiskProfile' and j.Status in(1,2) left join
                        MasterValue k on a.MaksudTujuanInd = k.Code and k.ID = 'InvestmentObjectivesIND' and k.Status in(1,2) left join
                        MasterValue l on a.SumberDanaInd = l.Code and l.ID = 'IncomeSourceIND' and l.Status in(1,2) left join
                        MasterValue m on a.AssetOwner = m.Code and m.ID = 'AssetOwner' and m.Status in(1,2) left join
                        MasterValue n on a.KodeKotaInd1 = n.Code and n.ID = 'CityRHB' and n.Status in(1,2) left join
                        MasterValue o on a.CountryofCorrespondence = o.Code and o.ID = 'SDICountry' and o.Status in(1,2) left join
                        MasterValue p on a.KodeKotaInd2 = p.Code and p.ID = 'CityRHB' and p.Status in(1,2) left join
                        MasterValue q on a.CountryofDomicile = q.Code and q.ID = 'SDICountry' and q.Status in(1,2) left join
                        MasterValue r on a.StatementType = r.Code and r.ID = 'StatementType' and r.Status in(1,2) left join
                        MasterValue s on a.FATCA = s.Code and s.ID = 'FATCA' and s.Status in(1,2) left join
                        MasterValue t on a.TINIssuanceCountry = t.Code and t.ID = 'SDICountry' and t.Status in(1,2) left join 
                        MasterValue u on a.IdentitasInd1 = u.Code and t.ID = 'Identity' and u.Status in(1,2) left join 
                        Bank BC1 on a.NamaBank1 = BC1.BankPK and BC1.status = 2  left join 
                        Bank BC2 on a.NamaBank2 = BC2.BankPK and BC2.status = 2  left join 
                        Bank BC3 on a.NamaBank3 = BC3.BankPK and BC3.status = 2  left join
                        Currency CC1 on a.MataUang1 = CC1.CurrencyPK and CC1.status = 2  left join
                        Currency CC2 on a.MataUang2 = CC2.CurrencyPK and CC2.status = 2  left join
                        Currency CC3 on a.MataUang3 = CC3.CurrencyPK and CC3.status = 2  
                        where A.IFUACode = @IFUACode And 
A.Status not in (3,4) and isnull(frontID,'') <> ''
order by FundClientPK desc";

                        cmd.Parameters.AddWithValue("@IFUACode", _ifuaCode);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    _l.Add(setVerifyClient(dr));
                                }
                            }
                            _m.data = _l;
                        }
                        return _m;
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public MessageVerifyClient VerifyClientByEmail(string _email)
        {
            try
            {
                MessageVerifyClient _m = new MessageVerifyClient();
                _m.code = 200;
                _m.message = "Verify Client By Email Success";

                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<MasterVerifyClient> _l = new List<MasterVerifyClient>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"select 0 CFLAG,isnull(a.FrontID,'') FrontID, 
                        isnull(a.IFUACode,'') IFUACode, 
                        isnull(a.SID,'') SID,
                        case when a.Status = 2 and isnull(IFUACode,'') <> '' and a.EntryUsersID = 'rdo' then 'True' else 'False' end Verify,
                        isnull(NamaDepanInd,'') NamaDepanInd,
                        isnull(NamaTengahInd,'') NamaTengahInd,
                        isnull(NamaBelakangInd,'') NamaBelakangInd,
                        isnull(c.DescOne,'') Nationality,
                        isnull(a.NoIdentitasInd1,'') NoIdentitasInd1,
                        isnull(u.DescOne,'') IdentitasInd1,
                        isnull(a.NPWP,'') NPWP,
                        isnull(a.RegistrationNPWP,0) RegistrationNPWP,
                        isnull(b.DescOne,'') CountryOfBirth,
                        isnull(a.TempatLahir,'') TempatLahir, 
                        isnull(a.TanggalLahir,0) TanggalLahir,
                        isnull(d.DescOne,'') JenisKelamin,
                        isnull(e.DescOne,'') Pendidikan,
                        isnull(a.MotherMaidenName,'') MotherMaidenName,
                        isnull(f.DescOne,'') Agama,
                        isnull(g.DescOne,'') Pekerjaan,
                        isnull(h.DescOne,'') PenghasilanInd,
                        isnull(i.DescOne,'') StatusPerkawinan,
                        isnull(a.SpouseName,'') SpouseName,
                        isnull(j.DescOne,'') InvestorsRiskProfile,
                        isnull(k.DescOne,'') MaksudTujuanInd,
                        isnull(l.DescOne,'') SumberDanaInd,
                        isnull(m.DescOne,'') AssetOwner,
                        isnull(a.OtherAlamatInd1,'') OtherAlamatInd1,
                        isnull(a.OtherKodeKotaInd1,'') OtherKodeKotaInd1,
                        isnull(a.OtherKodePosInd1,'') OtherKodePosInd1,
                        isnull(a.AlamatInd1,'') AlamatInd1,
                        isnull(n.DescOne,'') KodeKotaInd1,
                        isnull(a.KodePosInd1,'') KodePosInd1,
                        isnull(o.DescOne,'') CountryofCorrespondence,
                        isnull(a.AlamatInd2,'') AlamatInd2,
                        isnull(p.DescOne,'') KodeKotaInd2,
                        isnull(a.KodePosInd2,'') KodePosInd2,
                        isnull(q.DescOne,'') CountryofDomicile,
                        isnull(a.TeleponRumah,'') TeleponRumah,
                        isnull(a.TeleponSelular,'') TeleponSelular,
                        isnull(a.Fax,'') Fax,
                        isnull(a.Email,'') Email,
                        isnull(r.DescOne,'') StatementType,
                        isnull(s.DescOne,'') FATCA,
                        isnull(a.TIN,'') TIN,
                        isnull(t.DescOne,'') TINIssuanceCountry, 
                        isnull(BC1.Name,'') NamaBank1, 
                        isnull(CC1.ID,'') MataUang1, 
                        isnull(a.NomorRekening1,'') NomorRekening1,
                        isnull(a.NamaNasabah1,'') NamaNasabah1,
                        isnull(BC2.Name,'') NamaBank2,
                        isnull(CC2.ID,'') MataUang2,
                        isnull(a.NomorRekening2,'') NomorRekening2,
                        isnull(a.NamaNasabah2,'') NamaNasabah2,
                        isnull(BC3.Name,'') NamaBank3, 
                        isnull(CC3.ID,'') MataUang3 , 
                        isnull(a.NomorRekening3,'') NomorRekening3, 
                        isnull(a.NamaNasabah3,'') NamaNasabah3,
                        CONVERT(VARCHAR(20), isnull(a.LastUpdate,0), 126) LastUpdate from FundClient a left join
                        MasterValue b on a.CountryOfBirth = b.Code and b.ID = 'SDICountry' and b.Status in(1,2) left join
                        MasterValue c on a.Nationality = c.Code and c.ID = 'SDICountry' and c.Status in(1,2) left join 
                        MasterValue d on a.JenisKelamin = d.Code and d.ID = 'Sex' and d.Status in(1,2) left join
                        MasterValue e on a.Pendidikan = e.Code and e.ID = 'EducationalBackground' and e.Status in(1,2) left join
                        MasterValue f on a.Agama = f.Code and f.ID = 'Religion' and f.Status in(1,2) left join
                        MasterValue g on a.Pekerjaan = g.Code and g.ID = 'Occupation' and g.Status in(1,2) left join
                        MasterValue h on a.PenghasilanInd = h.Code and h.ID = 'IncomeIND' and h.Status in(1,2) left join
                        MasterValue i on a.StatusPerkawinan = i.Code and i.ID = 'MaritalStatus' and i.Status in(1,2) left join
                        MasterValue j on a.InvestorsRiskProfile = j.Code and j.ID = 'InvestorsRiskProfile' and j.Status in(1,2) left join
                        MasterValue k on a.MaksudTujuanInd = k.Code and k.ID = 'InvestmentObjectivesIND' and k.Status in(1,2) left join
                        MasterValue l on a.SumberDanaInd = l.Code and l.ID = 'IncomeSourceIND' and l.Status in(1,2) left join
                        MasterValue m on a.AssetOwner = m.Code and m.ID = 'AssetOwner' and m.Status in(1,2) left join
                        MasterValue n on a.KodeKotaInd1 = n.Code and n.ID = 'CityRHB' and n.Status in(1,2) left join
                        MasterValue o on a.CountryofCorrespondence = o.Code and o.ID = 'SDICountry' and o.Status in(1,2) left join
                        MasterValue p on a.KodeKotaInd2 = p.Code and p.ID = 'CityRHB' and p.Status in(1,2) left join
                        MasterValue q on a.CountryofDomicile = q.Code and q.ID = 'SDICountry' and q.Status in(1,2) left join
                        MasterValue r on a.StatementType = r.Code and r.ID = 'StatementType' and r.Status in(1,2) left join
                        MasterValue s on a.FATCA = s.Code and s.ID = 'FATCA' and s.Status in(1,2) left join
                        MasterValue t on a.TINIssuanceCountry = t.Code and t.ID = 'SDICountry' and t.Status in(1,2) left join 
                        MasterValue u on a.IdentitasInd1 = u.Code and t.ID = 'Identity' and u.Status in(1,2) left join 
                        Bank BC1 on a.NamaBank1 = BC1.BankPK and BC1.status = 2  left join 
                        Bank BC2 on a.NamaBank2 = BC2.BankPK and BC2.status = 2  left join 
                        Bank BC3 on a.NamaBank3 = BC3.BankPK and BC3.status = 2  left join
                        Currency CC1 on a.MataUang1 = CC1.CurrencyPK and CC1.status = 2  left join
                        Currency CC2 on a.MataUang2 = CC2.CurrencyPK and CC2.status = 2  left join
                        Currency CC3 on a.MataUang3 = CC3.CurrencyPK and CC3.status = 2  
                        where A.Email = @Email And 
A.Status not in (3,4) and isnull(frontID,'') <> ''
order by FundClientPK desc";

                        cmd.Parameters.AddWithValue("@Email", _email);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    _l.Add(setVerifyClient(dr));
                                }
                            }
                            _m.data = _l;
                        }
                        return _m;
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }


        public MessageVerifyClient VerifyClientByLastUpdateFromTo(DateTime _dateFrom, DateTime _dateTo)
        {
            try
            {
                MessageVerifyClient _m = new MessageVerifyClient();
                _m.code = 200;
                _m.message = "Verify Client By LastUpdate From to Success";

                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<MasterVerifyClient> _l = new List<MasterVerifyClient>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"select 0 CFLAG,isnull(a.FrontID,'') FrontID, 
                        isnull(a.IFUACode,'') IFUACode, 
                        isnull(a.SID,'') SID,
                        case when a.Status = 2 and isnull(IFUACode,'') <> '' and a.EntryUsersID = 'rdo' then 'True' else 'False' end Verify,
                        isnull(NamaDepanInd,'') NamaDepanInd,
                        isnull(NamaTengahInd,'') NamaTengahInd,
                        isnull(NamaBelakangInd,'') NamaBelakangInd,
                        isnull(c.DescOne,'') Nationality,
                        isnull(a.NoIdentitasInd1,'') NoIdentitasInd1,
                        isnull(u.DescOne,'') IdentitasInd1,
                        isnull(a.NPWP,'') NPWP,
                        isnull(a.RegistrationNPWP,0) RegistrationNPWP,
                        isnull(b.DescOne,'') CountryOfBirth,
                        isnull(a.TempatLahir,'') TempatLahir, 
                        isnull(a.TanggalLahir,0) TanggalLahir,
                        isnull(d.DescOne,'') JenisKelamin,
                        isnull(e.DescOne,'') Pendidikan,
                        isnull(a.MotherMaidenName,'') MotherMaidenName,
                        isnull(f.DescOne,'') Agama,
                        isnull(g.DescOne,'') Pekerjaan,
                        isnull(h.DescOne,'') PenghasilanInd,
                        isnull(i.DescOne,'') StatusPerkawinan,
                        isnull(a.SpouseName,'') SpouseName,
                        isnull(j.DescOne,'') InvestorsRiskProfile,
                        isnull(k.DescOne,'') MaksudTujuanInd,
                        isnull(l.DescOne,'') SumberDanaInd,
                        isnull(m.DescOne,'') AssetOwner,
                        isnull(a.OtherAlamatInd1,'') OtherAlamatInd1,
                        isnull(a.OtherKodeKotaInd1,'') OtherKodeKotaInd1,
                        isnull(a.OtherKodePosInd1,'') OtherKodePosInd1,
                        isnull(a.AlamatInd1,'') AlamatInd1,
                        isnull(n.DescOne,'') KodeKotaInd1,
                        isnull(a.KodePosInd1,'') KodePosInd1,
                        isnull(o.DescOne,'') CountryofCorrespondence,
                        isnull(a.AlamatInd2,'') AlamatInd2,
                        isnull(p.DescOne,'') KodeKotaInd2,
                        isnull(a.KodePosInd2,'') KodePosInd2,
                        isnull(q.DescOne,'') CountryofDomicile,
                        isnull(a.TeleponRumah,'') TeleponRumah,
                        isnull(a.TeleponSelular,'') TeleponSelular,
                        isnull(a.Fax,'') Fax,
                        isnull(a.Email,'') Email,
                        isnull(r.DescOne,'') StatementType,
                        isnull(s.DescOne,'') FATCA,
                        isnull(a.TIN,'') TIN,
                        isnull(t.DescOne,'') TINIssuanceCountry, 
                        isnull(BC1.Name,'') NamaBank1, 
                        isnull(CC1.ID,'') MataUang1, 
                        isnull(a.NomorRekening1,'') NomorRekening1,
                        isnull(a.NamaNasabah1,'') NamaNasabah1,
                        isnull(BC2.Name,'') NamaBank2,
                        isnull(CC2.ID,'') MataUang2,
                        isnull(a.NomorRekening2,'') NomorRekening2,
                        isnull(a.NamaNasabah2,'') NamaNasabah2,
                        isnull(BC3.Name,'') NamaBank3, 
                        isnull(CC3.ID,'') MataUang3 , 
                        isnull(a.NomorRekening3,'') NomorRekening3, 
                        isnull(a.NamaNasabah3,'') NamaNasabah3,
                        CONVERT(VARCHAR(20), isnull(a.LastUpdate,0), 126) LastUpdate from FundClient a left join
                        MasterValue b on a.CountryOfBirth = b.Code and b.ID = 'SDICountry' and b.Status in(1,2) left join
                        MasterValue c on a.Nationality = c.Code and c.ID = 'SDICountry' and c.Status in(1,2) left join 
                        MasterValue d on a.JenisKelamin = d.Code and d.ID = 'Sex' and d.Status in(1,2) left join
                        MasterValue e on a.Pendidikan = e.Code and e.ID = 'EducationalBackground' and e.Status in(1,2) left join
                        MasterValue f on a.Agama = f.Code and f.ID = 'Religion' and f.Status in(1,2) left join
                        MasterValue g on a.Pekerjaan = g.Code and g.ID = 'Occupation' and g.Status in(1,2) left join
                        MasterValue h on a.PenghasilanInd = h.Code and h.ID = 'IncomeIND' and h.Status in(1,2) left join
                        MasterValue i on a.StatusPerkawinan = i.Code and i.ID = 'MaritalStatus' and i.Status in(1,2) left join
                        MasterValue j on a.InvestorsRiskProfile = j.Code and j.ID = 'InvestorsRiskProfile' and j.Status in(1,2) left join
                        MasterValue k on a.MaksudTujuanInd = k.Code and k.ID = 'InvestmentObjectivesIND' and k.Status in(1,2) left join
                        MasterValue l on a.SumberDanaInd = l.Code and l.ID = 'IncomeSourceIND' and l.Status in(1,2) left join
                        MasterValue m on a.AssetOwner = m.Code and m.ID = 'AssetOwner' and m.Status in(1,2) left join
                        MasterValue n on a.KodeKotaInd1 = n.Code and n.ID = 'CityRHB' and n.Status in(1,2) left join
                        MasterValue o on a.CountryofCorrespondence = o.Code and o.ID = 'SDICountry' and o.Status in(1,2) left join
                        MasterValue p on a.KodeKotaInd2 = p.Code and p.ID = 'CityRHB' and p.Status in(1,2) left join
                        MasterValue q on a.CountryofDomicile = q.Code and q.ID = 'SDICountry' and q.Status in(1,2) left join
                        MasterValue r on a.StatementType = r.Code and r.ID = 'StatementType' and r.Status in(1,2) left join
                        MasterValue s on a.FATCA = s.Code and s.ID = 'FATCA' and s.Status in(1,2) left join
                        MasterValue t on a.TINIssuanceCountry = t.Code and t.ID = 'SDICountry' and t.Status in(1,2) left join 
                        MasterValue u on a.IdentitasInd1 = u.Code and t.ID = 'Identity' and u.Status in(1,2) left join 
                        Bank BC1 on a.NamaBank1 = BC1.BankPK and BC1.status = 2  left join 
                        Bank BC2 on a.NamaBank2 = BC2.BankPK and BC2.status = 2  left join 
                        Bank BC3 on a.NamaBank3 = BC3.BankPK and BC3.status = 2  left join
                        Currency CC1 on a.MataUang1 = CC1.CurrencyPK and CC1.status = 2  left join
                        Currency CC2 on a.MataUang2 = CC2.CurrencyPK and CC2.status = 2  left join
                        Currency CC3 on a.MataUang3 = CC3.CurrencyPK and CC3.status = 2  
                        where  a.LastUpdate between @DateFrom and @DateTo  and 
A.Status not in (3,4) and isnull(frontID,'') <> ''
order by FundClientPK desc";

                        cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _dateTo);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    _l.Add(setVerifyClient(dr));
                                }
                            }
                            _m.data = _l;
                        }
                        return _m;
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }


        #endregion

        #region GetClientBalance
        public class MessageGetClientBalance
        {
            public int code { get; set; }
            public string message { get; set; }
            public List<MasterClientBalance> data { get; set; }

        }

        public class MasterClientBalance
        {
            public string Date { get; set; }
            public string IFUACode { get; set; }
            public string FundCode { get; set; }
            public decimal UnitAmount { get; set; }
            public decimal Nav { get; set; }
            public decimal UnrealisedGainLoss { get; set; }
            public decimal EndBalance { get; set; }
            public decimal PortfolioPercentage { get; set; }
            public decimal AvgNav { get; set; }
            public decimal TotalCost { get; set; }
            public string LastUpdate { get; set; }
        }

        private MasterClientBalance setClientBalance(SqlDataReader dr)
        {
            MasterClientBalance _m = new MasterClientBalance();
            _m.Date = Convert.ToDateTime(dr["Date"].ToString()).ToString("yyyy-MM-dd");
            _m.IFUACode = dr["IFUACode"].ToString();
            _m.FundCode = dr["FundCode"].ToString();
            _m.UnitAmount = Convert.ToDecimal(dr["UnitAmount"]);
            _m.Nav = Convert.ToDecimal(dr["Nav"]);
            _m.UnrealisedGainLoss = Convert.ToDecimal(dr["UnrealisedGainLoss"]);
            _m.EndBalance = Convert.ToDecimal(dr["EndBalance"]);
            _m.PortfolioPercentage = Convert.ToDecimal(dr["PortfolioPercentage"]);
            _m.AvgNav = Convert.ToDecimal(dr["AvgNAV"]);
            _m.TotalCost = Convert.ToDecimal(dr["TotalCost"]);
            _m.LastUpdate = dr["LastUpdate"].ToString();
            return _m;
        }

        public MessageGetClientBalance GetAllClientBalanceFromTo(DateTime _dateFrom, DateTime _dateto)
        {
            try
            {
                MessageGetClientBalance _m = new MessageGetClientBalance();
                _m.code = 200;
                _m.message = "Get All Client Balance Success";

                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<MasterClientBalance> _l = new List<MasterClientBalance>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"


DECLARE @tempC TABLE
(
	Date DATETIME,
	FundPK INT,
	NAV NUMERIC(22,4)
)

DECLARE @DateCounter DATETIME
SET @DateCounter = @DateFrom

WHILE @DateCounter <= @DateTo
BEGIN
	INSERT INTO @tempC 
	        ( Date, FundPK, NAV )
	SELECT @DateCounter,FundPK,NAV FROM dbo.CloseNAV
	WHERE status = 2 AND Date =
	(
		SELECT MAX(date) FROM CloseNAv WHERE status = 2 AND Date <= @DateCounter
	)
	SET @DateCounter = DATEADD(DAY,1,@DateCounter)
END

Declare @tempA TABLE 
(
 FundclientPK INT,
 Date datetime,
 TotalAUM numeric(22,4)
)


INSERT INTO @tempA
SELECT A.FundClientPK,A.Date,SUM(ISNULL(UnitAmount * ISNULL(B.NAV,0),0)) TotalAUM FROM dbo.FundClientPosition A
LEFT JOIN @tempC B ON A.Date = B.Date AND A.FundPK = B.FundPK 
	WHERE A.Date BETWEEN @DateFrom AND @DateTo
	GROUP BY A.Date,FundClientPK
	

DECLARE @tempB TABLE
(
	FundClientPK INT,
	IFUACode NVARCHAR(200)
)

INSERT INTO @tempB 
SELECT FundClientPK,IFUACode FROM FUndClient WHERE status IN (1,2)

SELECT 
ISNULL(b.IFUACode,'') IFUACode,
ISNULL(c.ID,'') FundCode,
ISNULL(a.Date,0) Date,
isnull(a.UnitAmount,0) UnitAmount,
ISNULL(E.NAV,0) Nav, 
(ISNULL(E.NAV,0) - isnull(a.AvgNAV,0)) * isnull(a.UnitAmount,0) UnrealisedGainLoss, 
ISNULL(E.NAV,0) * isnull(a.UnitAmount,0) EndBalance, 
CASE WHEN D.TotalAUM  > 0 THEN 
ISNULL(E.NAV,0) * isnull(a.UnitAmount,0) / D.TotalAum * 100 ELSE 0 END PortfolioPercentage,
ISNULL(a.AvgNAV,0) AvgNAV, 
isnull(a.UnitAmount,0)  * ISNULL(a.AvgNAV,0) TotalCost,
CONVERT(VARCHAR(20), isnull(a.LastUpdate,0), 126) LastUpdate 
FROM FundClientPosition a 
left join @tempB b on a.FundClientPK = b.FundClientPK 
left join fund c on a.FundPK = c.FundPK and c.Status in (1,2)
LEFT JOIN @tempA D ON A.Date = D.Date AND A.FundClientPK = D.FundclientPK
LEFT JOIN @tempC E ON A.Date = E.Date AND A.FundPK = E.FundPK 
where a.Date between @DateFrom and @DateTo
AND ISNULL(B.IFUACode,'') <> ''


";
                        cmd.CommandTimeout = 0;

                        cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _dateto);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    _l.Add(setClientBalance(dr));
                                }
                            }
                            _m.data = _l;
                        }
                        return _m;
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }


        public MessageGetClientBalance GetAllClientBalanceFromToByIFUACode(string _ifuaCode, DateTime _dateFrom, DateTime _dateto)
        {
            try
            {
                MessageGetClientBalance _m = new MessageGetClientBalance();
                _m.code = 200;
                _m.message = "Get Client Balance By IFUACode Success";

                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<MasterClientBalance> _l = new List<MasterClientBalance>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"


DECLARE @tempC TABLE
(
	Date DATETIME,
	FundPK INT,
	NAV NUMERIC(22,4)
)

DECLARE @DateCounter DATETIME
SET @DateCounter = @DateFrom

WHILE @DateCounter <= @DateTo
BEGIN
	INSERT INTO @tempC 
	        ( Date, FundPK, NAV )
	SELECT @DateCounter,FundPK,NAV FROM dbo.CloseNAV
	WHERE status = 2 AND Date =
	(
		SELECT MAX(date) FROM CloseNAv WHERE status = 2 AND Date <= @DateCounter
	)
	SET @DateCounter = DATEADD(DAY,1,@DateCounter)
END

Declare @tempA TABLE 
(
 FundclientPK INT,
 Date datetime,
 TotalAUM numeric(22,4)
)


INSERT INTO @tempA
SELECT A.FundClientPK,A.Date,SUM(ISNULL(UnitAmount * ISNULL(B.NAV,0),0)) TotalAUM FROM dbo.FundClientPosition A
LEFT JOIN @tempC B ON A.Date = B.Date AND A.FundPK = B.FundPK 
	WHERE A.Date BETWEEN @DateFrom AND @DateTo
	GROUP BY A.Date,FundClientPK
	

DECLARE @tempB TABLE
(
	FundClientPK INT,
	IFUACode NVARCHAR(200)
)

INSERT INTO @tempB 
SELECT FundClientPK,IFUACode FROM FUndClient WHERE status IN (1,2)

SELECT 
ISNULL(b.IFUACode,'') IFUACode,
ISNULL(c.ID,'') FundCode,
ISNULL(a.Date,0) Date,
isnull(a.UnitAmount,0) UnitAmount,
ISNULL(E.NAV,0) Nav, 
(ISNULL(E.NAV,0) - isnull(a.AvgNAV,0)) * isnull(a.UnitAmount,0) UnrealisedGainLoss, 
ISNULL(E.NAV,0) * isnull(a.UnitAmount,0) EndBalance, 
CASE WHEN D.TotalAUM  > 0 THEN 
ISNULL(E.NAV,0) * isnull(a.UnitAmount,0) / D.TotalAum * 100 ELSE 0 END PortfolioPercentage,
ISNULL(a.AvgNAV,0) AvgNAV, 
isnull(a.UnitAmount,0)  * ISNULL(a.AvgNAV,0) TotalCost,
CONVERT(VARCHAR(20), isnull(a.LastUpdate,0), 126) LastUpdate 
FROM FundClientPosition a 
left join @tempB b on a.FundClientPK = b.FundClientPK 
left join fund c on a.FundPK = c.FundPK and c.Status in (1,2)
LEFT JOIN @tempA D ON A.Date = D.Date AND A.FundClientPK = D.FundclientPK
LEFT JOIN @tempC E ON A.Date = E.Date AND A.FundPK = E.FundPK 
where a.Date between @DateFrom and @DateTo
AND B.IFUACode = @IFUACode
";
                        cmd.CommandTimeout = 0;
                        cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _dateto);
                        cmd.Parameters.AddWithValue("@IFUACode", _ifuaCode);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    _l.Add(setClientBalance(dr));
                                }
                            }
                            _m.data = _l;
                        }
                        return _m;
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }


        #endregion

        #region GetNAV
        public class MessageGetNAV
        {
            public int code { get; set; }
            public string message { get; set; }
            public List<MasterNAV> data { get; set; }

        }

        public class MasterNAV
        {
            public string Date { get; set; }
            public string FundCode { get; set; }
            public decimal Nav { get; set; }
            public string LastUpdate { get; set; }
        }

        private MasterNAV setNAV(SqlDataReader dr)
        {
            MasterNAV _m = new MasterNAV();
            _m.Date = Convert.ToDateTime(dr["Date"].ToString()).ToString("yyyy-MM-dd");
            _m.FundCode = dr["FundCode"].ToString();
            _m.Nav = Convert.ToDecimal(dr["Nav"]);
            _m.LastUpdate = dr["LastUpdate"].ToString();
            return _m;
        }

        public MessageGetNAV GetAllNAVFromTo(DateTime _dateFrom, DateTime _dateto)
        {
            try
            {
                MessageGetNAV _m = new MessageGetNAV();
                _m.code = 200;
                _m.message = "Get NAV Success";

                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<MasterNAV> _l = new List<MasterNAV>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"select a.Date,isnull(d.ID,'') FundCode,isnull(a.Nav,0) Nav, CONVERT(VARCHAR(20), isnull(a.LastUpdate,0), 126) LastUpdate 
from CloseNAV a left join 
                    
                        fund d on a.FundPK = d.FundPK and d.Status in (1,2) where a.Date between @DateFrom and @DateTo and a.Status = 2";

                        cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _dateto);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    _l.Add(setNAV(dr));
                                }
                            }
                            _m.data = _l;
                        }
                        return _m;
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }


        public MessageGetNAV GetAllNAVFromToByFundCode(string _fundCode, DateTime _dateFrom, DateTime _dateto)
        {
            try
            {
                MessageGetNAV _m = new MessageGetNAV();
                _m.code = 200;
                _m.message = "Get NAV By FundCode Success";

                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<MasterNAV> _l = new List<MasterNAV>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"select a.Date,isnull(d.ID,'') FundCode,isnull(a.Nav,0) Nav, CONVERT(VARCHAR(20), isnull(a.LastUpdate,0), 126) LastUpdate from CloseNAV a left join 
                     
                        fund d on a.FundPK = d.FundPK and d.Status in (1,2) 
where a.Date between @DateFrom and @DateTo and a.Status = 2 and D.ID = @FundCode";

                        cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _dateto);
                        cmd.Parameters.AddWithValue("@FundCode", _fundCode);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    _l.Add(setNAV(dr));
                                }
                            }
                            _m.data = _l;
                        }
                        return _m;
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }


        #endregion

        #region GetAllTransactionStatus
        public class MessageGetAllTransactionStatus
        {
            public int code { get; set; }
            public string message { get; set; }
            public List<MasterAllTransactionStatus> data { get; set; }

        }

        public class MessageGetAllTransaction
        {
            public int code { get; set; }
            public string message { get; set; }
            public List<MasterAllTransaction> data { get; set; }

        }

        public class MasterAllTransaction
        {
            public int TrxID { get; set; }
            public string IFUACode { get; set; }
            public string NAVDate { get; set; }
            public decimal NAV { get; set; }
            public decimal NAVFundTo { get; set; }
            public string FundCode { get; set; }
            public string FundCodeTo { get; set; }
            public string TrxType { get; set; }
            public decimal Amount { get; set; }
            public decimal Unit { get; set; }
            public decimal AmountTo { get; set; }
            public decimal UnitTo { get; set; }
            public decimal FeePercent { get; set; }
            public decimal FeeAmount { get; set; }
            public string SourceOfFund { get; set; }
            public int BankRecipient { get; set; }
            public string Type { get; set; }
            public string Status { get; set; }
            public string LastUpdate { get; set; }

        }

        public class MasterAllTransactionStatus
        {
            public int TransactionPK { get; set; }
            public string TrxID { get; set; }
            public string Status { get; set; }
            public string Reason { get; set; }
            public string LastUpdate { get; set; }
        }

        private MasterAllTransaction setAllTransaction(SqlDataReader dr)
        {
            MasterAllTransaction _m = new MasterAllTransaction();
            _m.TrxID = Convert.ToInt32(dr["TrxID"]);
            _m.IFUACode = dr["IFUACode"].ToString();
            _m.NAVDate = dr["NAVDate"].ToString();
            _m.NAV = Convert.ToDecimal(dr["NAV"]);
            _m.NAVFundTo = Convert.ToDecimal(dr["NAVFundTo"]);
            _m.FundCode = dr["FundCode"].ToString();
            _m.FundCodeTo = dr["FundCodeTo"].ToString();
            _m.TrxType = dr["TrxType"].ToString();
            _m.Amount = Convert.ToDecimal(dr["Amount"]);
            _m.Unit = Convert.ToDecimal(dr["Unit"]);
            _m.AmountTo = Convert.ToDecimal(dr["AmountTo"]);
            _m.UnitTo = Convert.ToDecimal(dr["UnitTo"]);
            _m.FeeAmount = Convert.ToDecimal(dr["FeeAmount"]);
            _m.FeePercent = Convert.ToDecimal(dr["FeePercent"]);
            _m.SourceOfFund = dr["SourceOfFund"].ToString();
            _m.BankRecipient = Convert.ToInt32(dr["BankRecipient"]);
            _m.Type = dr["Type"].ToString();
            _m.Status = dr["Status"].ToString();
            _m.LastUpdate = dr["LastUpdate"].ToString();
            return _m;
        }

        public MessageGetAllTransaction GetTransactioByIFUACode(string _ifuaCode)
        {
            try
            {
                MessageGetAllTransaction _m = new MessageGetAllTransaction();
                _m.code = 200;
                _m.message = "Get All Transaction By IFUACode Success";

                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<MasterAllTransaction> _l = new List<MasterAllTransaction>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"

SELECT ClientSubscriptionPK TransactionPK,isnull(TransactionPK,0) TrxID
,CASE WHEN A.status = 2 AND posted = 1 THEN 'POSTED' ELSE CASE WHEN A.status = 3 THEN 'REJECTED' ELSE 'PENDING'end end Status
,CASE WHEN A.status = 3 THEN A.description ELSE '' end Reason, CONVERT(VARCHAR(20), isnull(A.LastUpdate,0), 126) LastUpdate 
,ISNULL(B.IFUACode,'') IFUACode
,ISNULL(A.NAVDate,'') NAVDate
,ISNULL(A.NAV,0) NAV
,0 NAVFundTo
,ISNULL(C.ID,'') FundCode
,'' FundCodeTo
,'SUB' TrxType
,ISNULL(A.CashAmount,0)  Amount
,ISNULL(A.UnitAmount,0)  Unit
,0 AmountTo
,0  UnitTo
,ISNULL(A.SubscriptionFeeAmount,0) FeeAmount
,ISNULL(A.SubscriptionFeePercent,0) FeePercent  
,ISNULL(A.SumberDana,'') SourceOfFund
,0 BankRecipient
,'' Type
FROM dbo.ClientSubscription A 
LEFT JOIN dbo.FundClient B ON A.FundClientPK = B.FundClientPK AND B.Status IN (1,2)
LEFT JOIN Fund C ON A.FundPK = C.FundPK AND C.status IN (1,2)
WHERE B.IFUACode = @IFUACode
AND A.status <> 4

UNION ALL

SELECT ClientRedemptionPK TransactionPK,TransactionPK TrxID
,CASE WHEN A.status = 2 AND posted = 1 THEN 'POSTED' ELSE CASE WHEN A.status = 3 THEN 'REJECTED' ELSE 'PENDING'end end Status
,CASE WHEN A.status = 3 THEN A.description ELSE '' end Reason, CONVERT(VARCHAR(20), isnull(A.LastUpdate,0), 126) LastUpdate 
,ISNULL(B.IFUACode,'') IFUACode
,ISNULL(A.NAVDate,'') NAVDate
,ISNULL(A.NAV,0) NAV
,0 NAVFundTo
,ISNULL(C.ID,'') FundCode
,'' FundCodeTo
,'RED' TrxType
,ISNULL(A.CashAmount,0)  Amount
,ISNULL(A.UnitAmount,0)  Unit
,0 AmountTo
,0  UnitTo
,ISNULL(A.RedemptionFeeAmount,0) FeeAmount
,ISNULL(A.RedemptionFeePercent,0) FeePercent  
,'' SourceOfFund
,ISNULL(A.BankRecipientPK,0) BankRecipient
,CASE WHEN A.BitRedemptionAll = 1 THEN 'FULL' ELSE 'PARTIAL' END Type
FROM dbo.ClientRedemption A 
LEFT JOIN dbo.FundClient B ON A.FundClientPK = B.FundClientPK AND B.Status IN (1,2)
LEFT JOIN Fund C ON A.FundPK = C.FundPK AND C.status IN (1,2)
WHERE B.IFUACode = @IFUACode
AND A.status <> 4

UNION ALL

SELECT ClientSwitchingPK TransactionPK,TransactionPK TrxID
,CASE WHEN A.status = 2 AND posted = 1 THEN 'POSTED' ELSE CASE WHEN A.status = 3 THEN 'REJECTED' ELSE 'PENDING'end end Status
,CASE WHEN A.status = 3 THEN A.description ELSE '' end Reason, CONVERT(VARCHAR(20), isnull(A.LastUpdate,0), 126) LastUpdate
,ISNULL(B.IFUACode,'') IFUACode
,ISNULL(A.NAVDate,'') NAVDate
,ISNULL(A.NAVFundFrom,0) NAV
,ISNULL(A.NAVFundTo,0) NAVFundTo
,ISNULL(C.ID,'') FundCode
,ISNULL(D.ID,'') FundCodeTo
,'SWI' TrxType
,ISNULL(A.CashAmount,0)  Amount
,ISNULL(A.UnitAmount,0)  Unit
,ISNULL(A.TotalCashAmountFundTo,0)  AmountTo
,ISNULL(A.TotalUnitAmountFundTo,0)  UnitTo
,ISNULL(A.SwitchingFeeAmount,0) FeeAmount
,ISNULL(A.SwitchingFeePercent,0) FeePercent  
,'' SourceOfFund
,0 BankRecipient
,CASE WHEN A.BitSwitchingAll = 1 THEN 'FULL' ELSE 'PARTIAL' END Type
 
FROM dbo.ClientSwitching A
LEFT JOIN dbo.FundClient B ON A.FundClientPK = B.FundClientPK AND B.Status IN (1,2)
LEFT JOIN Fund C ON A.FundPKFrom = C.FundPK AND C.status IN (1,2)
LEFT JOIN Fund D ON A.FundPKTo = D.FundPK AND C.status IN (1,2)
WHERE B.IFUACode = @IFUACode
AND A.status <> 4
";

                        cmd.Parameters.AddWithValue("@IFUACode", _ifuaCode);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    _l.Add(setAllTransaction(dr));
                                }
                            }
                            _m.data = _l;
                        }
                        return _m;
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }
        private MasterAllTransactionStatus setAllTransactionStatus(SqlDataReader dr)
        {
            MasterAllTransactionStatus _m = new MasterAllTransactionStatus();
            _m.TransactionPK = Convert.ToInt32(dr["TransactionPK"]);
            _m.TrxID = dr["TrxID"].ToString();
            _m.Status = dr["Status"].ToString();
            _m.Reason = dr["Reason"].ToString();
            _m.LastUpdate = dr["LastUpdate"].ToString();
            return _m;
        }

        public MessageGetAllTransactionStatus GetAllTransactionStatus()
        {
            try
            {
                MessageGetAllTransactionStatus _m = new MessageGetAllTransactionStatus();
                _m.code = 200;
                _m.message = "Get Transaction Status Success";

                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<MasterAllTransactionStatus> _l = new List<MasterAllTransactionStatus>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"SELECT ClientSubscriptionPK TransactionPK,isnull(TransactionPK,0) TrxID
                        ,CASE WHEN status = 2 AND posted = 1 THEN 'POSTED' ELSE CASE WHEN status = 3 THEN 'REJECTED' ELSE 'PENDING'end end Status
                        ,CASE WHEN status = 3 THEN description ELSE '' end Reason, CONVERT(VARCHAR(20), isnull(LastUpdate,0), 126) LastUpdate FROM dbo.ClientSubscription 
WHERE isnull(TransactionPK,'') <> '' and TransactionPK > 0
UNION ALL
                        SELECT ClientRedemptionPK TransactionPK,isnull(TransactionPK,0) TrxID
                        ,CASE WHEN status = 2 AND posted = 1 THEN 'POSTED' ELSE CASE WHEN status = 3 THEN 'REJECTED' ELSE 'PENDING'end end Status
                        ,CASE WHEN status = 3 THEN description ELSE '' end Reason, CONVERT(VARCHAR(20), isnull(LastUpdate,0), 126) LastUpdate  FROM dbo.ClientRedemption 
WHERE isnull(TransactionPK,'') <> '' and TransactionPK > 0
                        UNION ALL
                        SELECT ClientSwitchingPK TransactionPK,isnull(TransactionPK,0) TrxID
                        ,CASE WHEN status = 2 AND posted = 1 THEN 'POSTED' ELSE CASE WHEN status = 3 THEN 'REJECTED' ELSE 'PENDING'end end Status
                        ,CASE WHEN status = 3 THEN description ELSE '' end Reason, CONVERT(VARCHAR(20), isnull(LastUpdate,0), 126) LastUpdate  FROM dbo.ClientSwitching 
WHERE isnull(TransactionPK,'') <> '' and TransactionPK > 0
";

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    _l.Add(setAllTransactionStatus(dr));
                                }
                            }
                            _m.data = _l;
                        }
                        return _m;
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public MessageGetAllTransactionStatus GetAllTransactionStatusFromTo(DateTime _dateFrom, DateTime _dateto)
        {
            try
            {
                MessageGetAllTransactionStatus _m = new MessageGetAllTransactionStatus();
                _m.code = 200;
                _m.message = "Get All Transaction Status From To Success";

                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<MasterAllTransactionStatus> _l = new List<MasterAllTransactionStatus>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"SELECT ClientSubscriptionPK TransactionPK,isnull(TransactionPK,0) TrxID
                        ,CASE WHEN status = 2 AND posted = 1 THEN 'POSTED' ELSE CASE WHEN status = 3 THEN 'REJECTED' ELSE 'PENDING'end end Status
                        ,CASE WHEN status = 3 THEN description ELSE '' end Reason, CONVERT(VARCHAR(20), isnull(LastUpdate,0), 126) LastUpdate  FROM dbo.ClientSubscription where ValueDate between @DateFrom and @DateTo
And isnull(TransactionPK,'') <> '' and TransactionPK > 0
                        UNION ALL
                        SELECT ClientRedemptionPK TransactionPK,TransactionPK TrxID
                        ,CASE WHEN status = 2 AND posted = 1 THEN 'POSTED' ELSE CASE WHEN status = 3 THEN 'REJECTED' ELSE 'PENDING'end end Status
                        ,CASE WHEN status = 3 THEN description ELSE '' end Reason, CONVERT(VARCHAR(20), isnull(LastUpdate,0), 126) LastUpdate  FROM dbo.ClientRedemption where ValueDate between @DateFrom and @DateTo
And isnull(TransactionPK,'') <> '' and TransactionPK > 0
                        UNION ALL
                        SELECT ClientSwitchingPK TransactionPK,TransactionPK TrxID
                        ,CASE WHEN status = 2 AND posted = 1 THEN 'POSTED' ELSE CASE WHEN status = 3 THEN 'REJECTED' ELSE 'PENDING'end end Status
                        ,CASE WHEN status = 3 THEN description ELSE '' end Reason, CONVERT(VARCHAR(20), isnull(LastUpdate,0), 126) LastUpdate  FROM dbo.ClientSwitching where ValueDate between @DateFrom and @DateTo
And isnull(TransactionPK,'') <> '' and TransactionPK > 0
";

                        cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _dateto);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    _l.Add(setAllTransactionStatus(dr));
                                }
                            }
                            _m.data = _l;
                        }
                        return _m;
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public MessageGetAllTransactionStatus GetTransactionStatusByTrxID(string _trxID)
        {
            try
            {
                MessageGetAllTransactionStatus _m = new MessageGetAllTransactionStatus();
                _m.code = 200;
                _m.message = "Get Transaction Status Success";

                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<MasterAllTransactionStatus> _l = new List<MasterAllTransactionStatus>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"SELECT ClientSubscriptionPK TransactionPK,isnull(TransactionPK,0) TrxID
                        ,CASE WHEN status = 2 AND posted = 1 THEN 'POSTED' ELSE CASE WHEN status = 3 THEN 'REJECTED' ELSE 'PENDING'end end Status
                        ,CASE WHEN status = 3 THEN description ELSE '' end Reason, CONVERT(VARCHAR(20), isnull(LastUpdate,0), 126) LastUpdate  FROM dbo.ClientSubscription where TransactionPK = @TrxID
                        UNION ALL
                        SELECT ClientRedemptionPK TransactionPK,TransactionPK TrxID
                        ,CASE WHEN status = 2 AND posted = 1 THEN 'POSTED' ELSE CASE WHEN status = 3 THEN 'REJECTED' ELSE 'PENDING'end end Status
                        ,CASE WHEN status = 3 THEN description ELSE '' end Reason, CONVERT(VARCHAR(20), isnull(LastUpdate,0), 126) LastUpdate  FROM dbo.ClientRedemption where TransactionPK = @TrxID
                        UNION ALL
                        SELECT ClientSwitchingPK TransactionPK,TransactionPK TrxID
                        ,CASE WHEN status = 2 AND posted = 1 THEN 'POSTED' ELSE CASE WHEN status = 3 THEN 'REJECTED' ELSE 'PENDING'end end Status
                        ,CASE WHEN status = 3 THEN description ELSE '' end Reason, CONVERT(VARCHAR(20), isnull(LastUpdate,0), 126) LastUpdate  FROM dbo.ClientSwitching where TransactionPK = @TrxID";

                        cmd.Parameters.AddWithValue("@TrxID", _trxID);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    _l.Add(setAllTransactionStatus(dr));
                                }
                            }
                            _m.data = _l;
                        }
                        return _m;
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }


        #endregion

        //Part POST
        // testing Posting
        #region test Post/Add data Period
        public class MessageAddPeriod
        {
            public int code { get; set; }
            public string message { get; set; }
            public List<AddPeriod> data { get; set; }
        }


        public MessageAddPeriod RDOAddPeriod(List<Period> _l)
        {
            using (DataTable dt = new DataTable())
            {
                DataColumn dc;
                DataRow dr;

                dc = new DataColumn();
                dc.DataType = System.Type.GetType("System.String");
                dc.ColumnName = "ID";
                dc.Unique = false;
                dt.Columns.Add(dc);

                dc = new DataColumn();
                dc.DataType = System.Type.GetType("System.DateTime");
                dc.ColumnName = "DateFrom";
                dc.Unique = false;
                dt.Columns.Add(dc);

                dc = new DataColumn();
                dc.DataType = System.Type.GetType("System.DateTime");
                dc.ColumnName = "DateTo";
                dc.Unique = false;
                dt.Columns.Add(dc);

                dc = new DataColumn();
                dc.DataType = System.Type.GetType("System.String");
                dc.ColumnName = "Description";
                dc.Unique = false;
                dt.Columns.Add(dc);

                if (_l.Count > 0)
                {
                    foreach (var result in _l)
                    {
                        dr = dt.NewRow();
                        dr["ID"] = result.ID;
                        dr["DateFrom"] = result.DateFrom;
                        dr["DateTo"] = result.DateTo;
                        dr["Description"] = result.Description;
                        dt.Rows.Add(dr);
                    }
                    //Tambahin Truncate
                    using (SqlConnection conns = new SqlConnection(Tools.conString))
                    {
                        conns.Open();
                        using (SqlCommand cmd0 = conns.CreateCommand())
                        {
                            cmd0.CommandText = "truncate table ZRDO_PERIOD";
                            cmd0.ExecuteNonQuery();
                        }
                    }

                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(Tools.conString, SqlBulkCopyOptions.TableLock))
                    {

                        bulkCopy.DestinationTableName = "dbo.ZRDO_PERIOD";
                        bulkCopy.WriteToServer(dt);
                    }
                    MessageAddPeriod _m = new MessageAddPeriod();
                    _m.code = 200;
                    _m.message = "SUKSES";
                }
                else
                {
                    MessageAddPeriod _m = new MessageAddPeriod();
                    _m.code = 204;
                    _m.message = "No Data To be insert";

                    return _m;
                }
            }
            return null;
        }
        #endregion

        //Add Profile
        #region test Post/Add data //Add Profile

        public class ReturnValidateProfile
        {
            public int ValidationID { get; set; }
            public string FOID { get; set; }
        }


        public class ReturnProfile
        {
            public int FundClientPK { get; set; }
            public string FOID { get; set; }
        }

        public class MessageAddProfile
        {
            public int code { get; set; }
            public string message { get; set; }
            public List<ReturnProfile> data { get; set; }
            public List<ReturnValidateProfile> dataValidate { get; set; }
        }

        private ReturnValidateProfile setReturnValidateProfile(SqlDataReader dr0)
        {
            ReturnValidateProfile _m = new ReturnValidateProfile();
            _m.ValidationID = Convert.ToInt32(dr0["ValidateID"]);
            _m.FOID = dr0["FOID"].ToString();
            return _m;
        }

        private ReturnProfile setReturnProfile(SqlDataReader dr0)
        {
            ReturnProfile _m = new ReturnProfile();
            _m.FundClientPK = Convert.ToInt32(dr0["FundClientPK"]);
            _m.FOID = dr0["FrontID"].ToString();
            return _m;
        }

        public MessageAddProfile RDOAddProfile(List<AddProfile> _l)
        {
            MessageAddProfile _m = new MessageAddProfile();
            using (DataTable dt = new DataTable())
            {
                #region datatableAddProfile
                DataColumn dc;
                DataRow dr;

                dc = new DataColumn();
                dc.DataType = System.Type.GetType("System.String");
                dc.ColumnName = "FrontID";
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
                dc.ColumnName = "NoIdentitasInd1";
                dc.Unique = false;
                dt.Columns.Add(dc);

                dc = new DataColumn();
                dc.DataType = System.Type.GetType("System.String");
                dc.ColumnName = "IdentitasInd1";
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
                dc.ColumnName = "Pekerjaan";
                dc.Unique = false;
                dt.Columns.Add(dc);

                dc = new DataColumn();
                dc.DataType = System.Type.GetType("System.String");
                dc.ColumnName = "PenghasilanInd";
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
                dc.ColumnName = "NamaBank1";
                dc.Unique = false;
                dt.Columns.Add(dc);

                dc = new DataColumn();
                dc.DataType = System.Type.GetType("System.String");
                dc.ColumnName = "BankBranchName1";
                dc.Unique = false;
                dt.Columns.Add(dc);

                dc = new DataColumn();
                dc.DataType = System.Type.GetType("System.String");
                dc.ColumnName = "MataUang1";
                dc.Unique = false;
                dt.Columns.Add(dc);

                dc = new DataColumn();
                dc.DataType = System.Type.GetType("System.String");
                dc.ColumnName = "NomorRekening1";
                dc.Unique = false;
                dt.Columns.Add(dc);

                dc = new DataColumn();
                dc.DataType = System.Type.GetType("System.String");
                dc.ColumnName = "NamaNasabah1";
                dc.Unique = false;
                dt.Columns.Add(dc);

                dc = new DataColumn();
                dc.DataType = System.Type.GetType("System.String");
                dc.ColumnName = "NamaBank2";
                dc.Unique = false;
                dt.Columns.Add(dc);

                dc = new DataColumn();
                dc.DataType = System.Type.GetType("System.String");
                dc.ColumnName = "BankBranchName2";
                dc.Unique = false;
                dt.Columns.Add(dc);

                dc = new DataColumn();
                dc.DataType = System.Type.GetType("System.String");
                dc.ColumnName = "MataUang2";
                dc.Unique = false;
                dt.Columns.Add(dc);

                dc = new DataColumn();
                dc.DataType = System.Type.GetType("System.String");
                dc.ColumnName = "NomorRekening2";
                dc.Unique = false;
                dt.Columns.Add(dc);

                dc = new DataColumn();
                dc.DataType = System.Type.GetType("System.String");
                dc.ColumnName = "NamaNasabah2";
                dc.Unique = false;
                dt.Columns.Add(dc);

                dc = new DataColumn();
                dc.DataType = System.Type.GetType("System.String");
                dc.ColumnName = "NamaBank3";
                dc.Unique = false;
                dt.Columns.Add(dc);

                dc = new DataColumn();
                dc.DataType = System.Type.GetType("System.String");
                dc.ColumnName = "BankBranchName3";
                dc.Unique = false;
                dt.Columns.Add(dc);

                dc = new DataColumn();
                dc.DataType = System.Type.GetType("System.String");
                dc.ColumnName = "MataUang3";
                dc.Unique = false;
                dt.Columns.Add(dc);

                dc = new DataColumn();
                dc.DataType = System.Type.GetType("System.String");
                dc.ColumnName = "NomorRekening3";
                dc.Unique = false;
                dt.Columns.Add(dc);

                dc = new DataColumn();
                dc.DataType = System.Type.GetType("System.String");
                dc.ColumnName = "NamaNasabah3";
                dc.Unique = false;
                dt.Columns.Add(dc);
                #endregion

                if (_l.Count > 0)
                {

                    foreach (var result in _l)
                    {

                        dr = dt.NewRow();
                        dr["FrontID"] = result.FrontID;
                        dr["NamaDepanInd"] = result.NamaDepanInd;
                        dr["NamaTengahInd"] = result.NamaTengahInd;
                        dr["NamaBelakangInd"] = result.NamaBelakangInd;
                        dr["Nationality"] = result.Nationality;
                        dr["NoIdentitasInd1"] = result.NoIdentitasInd1;
                        dr["IdentitasInd1"] = result.IdentitasInd1;
                        dr["NPWP"] = result.NPWP;
                        dr["RegistrationNPWP"] = result.RegistrationNPWP;
                        dr["CountryOfBirth"] = result.CountryOfBirth;
                        dr["TempatLahir"] = result.TempatLahir;
                        dr["TanggalLahir"] = result.TanggalLahir;
                        dr["JenisKelamin"] = result.JenisKelamin;
                        dr["Pendidikan"] = result.Pendidikan;
                        dr["MotherMaidenName"] = result.MotherMaidenName;
                        dr["Agama"] = result.Agama;
                        dr["Pekerjaan"] = result.Pekerjaan;
                        dr["PenghasilanInd"] = result.PenghasilanInd;
                        dr["StatusPerkawinan"] = result.StatusPerkawinan;
                        dr["SpouseName"] = result.SpouseName;
                        dr["InvestorsRiskProfile"] = result.InvestorsRiskProfile;
                        dr["MaksudTujuanInd"] = result.MaksudTujuanInd;
                        dr["SumberDanaInd"] = result.SumberDanaInd;
                        dr["AssetOwner"] = result.AssetOwner;
                        dr["OtherAlamatInd1"] = result.OtherAlamatInd1;
                        dr["OtherKodeKotaInd1"] = result.OtherKodeKotaInd1;
                        dr["OtherKodePosInd1"] = result.OtherKodePosInd1;
                        dr["AlamatInd1"] = result.AlamatInd1;
                        dr["KodeKotaInd1"] = result.KodeKotaInd1;
                        dr["KodePosInd1"] = result.KodePosInd1;
                        dr["CountryofCorrespondence"] = result.CountryofCorrespondence;
                        dr["AlamatInd2"] = result.AlamatInd2;
                        dr["KodeKotaInd2"] = result.KodeKotaInd2;
                        dr["KodePosInd2"] = result.KodePosInd2;
                        dr["CountryofDomicile"] = result.CountryofDomicile;
                        dr["TeleponRumah"] = result.TeleponRumah;
                        dr["TeleponSelular"] = result.TeleponSelular;
                        dr["Fax"] = result.Fax;
                        dr["Email"] = result.Email;
                        dr["StatementType"] = result.StatementType;
                        dr["FATCA"] = result.FATCA;
                        dr["TIN"] = result.TIN;
                        dr["TINIssuanceCountry"] = result.TINIssuanceCountry;
                        dr["NamaBank1"] = result.NamaBank1;
                        dr["BankBranchName1"] = result.BankBranchName1;
                        dr["MataUang1"] = result.MataUang1;
                        dr["NomorRekening1"] = result.NomorRekening1;
                        dr["NamaNasabah1"] = result.NamaNasabah1;
                        dr["NamaBank2"] = result.NamaBank2;
                        dr["BankBranchName2"] = result.BankBranchName2;
                        dr["MataUang2"] = result.MataUang2;
                        dr["NomorRekening2"] = result.NomorRekening2;
                        dr["NamaNasabah2"] = result.NamaNasabah2;
                        dr["NamaBank3"] = result.NamaBank3;
                        dr["BankBranchName3"] = result.BankBranchName3;
                        dr["MataUang3"] = result.MataUang3;
                        dr["NomorRekening3"] = result.NomorRekening3;
                        dr["NamaNasabah3"] = result.NamaNasabah3;
                        dt.Rows.Add(dr);
                    }
                    //Tambahin Truncate
                    using (SqlConnection conns = new SqlConnection(Tools.conString))
                    {
                        conns.Open();
                        using (SqlCommand cmd0 = conns.CreateCommand())
                        {
                            cmd0.CommandText = "truncate table ZRDO_ADDPROFILE";
                            cmd0.ExecuteNonQuery();
                        }
                    }
                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(Tools.conString, SqlBulkCopyOptions.TableLock))
                    {

                        bulkCopy.DestinationTableName = "dbo.ZRDO_ADDPROFILE";
                        bulkCopy.WriteToServer(dt);
                    }


                    // QUERY VALIDASI 
                    // Cek ada row gak
                    using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                    {
                        DbCon.Open();
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = @"
IF OBJECT_ID('tempdb..#ResultValidateProfile') IS NOT NULL
    DROP TABLE #Results

                        CREATE TABLE #ResultValidateProfile
                        (
	                        ValidateID INT,
	                        FOID NVARCHAR(100)
                        )

                        --1 : Email already exist
                        --2 : CellPhone already exist
                        --3 : KTPNo already exist


                        INSERT INTO #ResultValidateProfile
                                ( ValidateID, FOID )
                        SELECT 1,FrontID FROM dbo.ZRDO_ADDPROFILE
                        WHERE RTRIM(LTRIM(Email)) IN
                        (
	                        SELECT RTRIM(LTRIM(Email)) FROM FundClient WHERE status IN (1,2)
                        ) AND isnull(email,'') <> ''

                        INSERT INTO #ResultValidateProfile
                                ( ValidateID, FOID )
                        SELECT 2,FrontID FROM dbo.ZRDO_ADDPROFILE
                        WHERE RTRIM(LTRIM(TeleponSelular)) IN
                        (
	                        SELECT RTRIM(LTRIM(TeleponSelular)) FROM FundClient WHERE status IN (1,2)
                        ) AND isnull(TeleponSelular,'') <> ''

                        

                        --- UJUNG QUERY VALIDASI ADD PROFILE


                        SELECT  ValidateID,FOID FROM #ResultValidateProfile
                        WHERE   FOID
                        IN(
                        SELECT frontID FROM dbo.ZRDO_ADDPROFILE
                        )";

                            using (SqlDataReader dr0 = cmd.ExecuteReader())
                            {
                                if (dr0.HasRows)
                                {
                                    List<ReturnValidateProfile> _r = new List<ReturnValidateProfile>();
                                    while (dr0.Read())
                                    {

                                        _m.code = 400;
                                        _m.message = "Validation Not Pass";
                                        _r.Add(setReturnValidateProfile(dr0));
                                    }
                                    _m.dataValidate = _r;
                                }
                                else
                                {
                                    using (SqlConnection conn = new SqlConnection(Tools.conString))
                                    {
                                        List<ReturnProfile> _s = new List<ReturnProfile>();
                                        DateTime _now = DateTime.Now;
                                        conn.Open();
                                        using (SqlCommand cmd1 = conn.CreateCommand())
                                        {
                                            cmd1.CommandText = @"
                                            DECLARE @FundClientPK BigInt 
                                            SELECT @FundClientPK = isnull(Max(FundClientPK),0) FROM FundClient

                                            INSERT INTO FundClient	
                                            ( 
ClientCategory,
InvestorType,
FundClientPK ,
											  HistoryPK,
											  Status,
											  ID,
											  Name,
                                              FrontID ,
                                              NamaDepanInd ,
                                              NamaTengahInd ,
                                              NamaBelakangInd ,
                                              Nationality ,
                                              NoIdentitasInd1 ,
                                              IdentitasInd1 ,
                                              NPWP ,
                                              RegistrationNPWP ,
                                              CountryOfBirth ,
                                              TempatLahir ,
                                              TanggalLahir ,
                                              JenisKelamin ,
                                              Pendidikan ,
                                              MotherMaidenName ,
                                              Agama ,
                                              Pekerjaan ,
                                              PenghasilanInd ,
                                              StatusPerkawinan ,
                                              SpouseName ,
                                              InvestorsRiskProfile ,
                                              MaksudTujuanInd ,
                                              SumberDanaInd ,
                                              AssetOwner ,
                                              OtherAlamatInd1 ,
                                              OtherKodeKotaInd1 ,
                                              OtherKodePosInd1 ,
                                              AlamatInd1 ,
                                              KodeKotaInd1 ,
                                              KodePosInd1 ,
                                              CountryofCorrespondence ,
                                              AlamatInd2 ,
                                              KodeKotaInd2 ,
                                              KodePosInd2 ,
                                              CountryofDomicile ,
                                              TeleponRumah ,
                                              TeleponSelular ,
                                              Fax ,
                                              Email ,
                                              StatementType ,
                                              FATCA ,
                                              TIN ,
                                              TINIssuanceCountry ,
                                              NamaBank1 ,
                                              BankBranchName1 ,
                                              MataUang1 ,
                                              NomorRekening1 ,
                                              NamaNasabah1 ,
                                              NamaBank2 ,
                                              BankBranchName2 ,
                                              MataUang2 ,
                                              NomorRekening2 ,
                                              NamaNasabah2 ,
                                              NamaBank3 ,
                                              BankBranchName3 ,
                                              MataUang3 ,
                                              NomorRekening3 ,
                                              NamaNasabah3 , EntryUsersID,EntryTime,LastUpdate
                                            ) 
                                            

                                            SELECT 1,1,Row_number() over(order by FrontID) + @FundClientPK,1,1,
                                              '' ,
											  NamaDepanInd + ' ' + NamaTengahInd + ' ' + NamaBelakangInd,
                                              FrontID ,
                                              NamaDepanInd ,
                                              NamaTengahInd ,
                                              NamaBelakangInd ,
                                              Nationality ,
                                              NoIdentitasInd1 ,
                                              IdentitasInd1 ,
                                              NPWP ,
                                              RegistrationNPWP ,
                                              CountryOfBirth ,
                                              TempatLahir ,
                                              TanggalLahir ,
                                              JenisKelamin ,
                                              Pendidikan ,
                                              MotherMaidenName ,
                                              Agama ,
                                              Pekerjaan ,
                                              PenghasilanInd ,
                                              StatusPerkawinan ,
                                              SpouseName ,
                                              InvestorsRiskProfile ,
                                              MaksudTujuanInd ,
                                              SumberDanaInd ,
                                              AssetOwner ,
                                              OtherAlamatInd1 ,
                                              OtherKodeKotaInd1 ,
                                              OtherKodePosInd1 ,
                                              AlamatInd1 ,
                                              KodeKotaInd1 ,
                                              KodePosInd1 ,
                                              CountryofCorrespondence ,
                                              AlamatInd2 ,
                                              KodeKotaInd2 ,
                                              KodePosInd2 ,
                                              CountryofDomicile ,
                                              TeleponRumah ,
                                              TeleponSelular ,
                                              Fax ,
                                              Email ,
                                              StatementType ,
                                              FATCA ,
                                              TIN ,
                                              TINIssuanceCountry ,
                                              NamaBank1 ,
                                              BankBranchName1 ,
                                              MataUang1 ,
                                              NomorRekening1 ,
                                              NamaNasabah1 ,
                                              NamaBank2 ,
                                              BankBranchName2 ,
                                              MataUang2 ,
                                              NomorRekening2 ,
                                              NamaNasabah2 ,
                                              NamaBank3 ,
                                              BankBranchName3 ,
                                              MataUang3 ,
                                              NomorRekening3 ,
                                              NamaNasabah3 , 'RDO', @TimeNow, @TimeNow From dbo.ZRDO_ADDPROFILE
                                              


                                             Select FundClientPK, FrontID  from FundClient Where FrontID in(select FrontID  from ZRDO_ADDPROFILE)
                                            ";

                                            cmd1.Parameters.AddWithValue("@TimeNow", _now);
                                            using (SqlDataReader dr01 = cmd1.ExecuteReader())
                                            {
                                                if (dr01.HasRows)
                                                {
                                                    while (dr01.Read())
                                                    {

                                                        _m.code = 200;
                                                        _m.message = "Insert Fundclient Success";
                                                        _s.Add(setReturnProfile(dr01));
                                                    }
                                                    _m.data = _s;
                                                }

                                            }
                                        }
                                    }
                                }
                            }
                            return _m;
                        }
                    }

                    // Jika ada row
                    // Set ke ReturnValidateProfile
                    // return message

                    // Jika tidak ada row
                    // Query Insert dari temp ke Table Client
                    // set ke ReturnProfile
                    // return message
                }
                else
                {

                    _m.code = 204;
                    _m.message = "No Data To be insert";
                    return _m;
                }

            }
            //return null;

        }
        #endregion

        #region Update data Profile
        public MessageAddProfile RDOUpdateProfile(List<AddProfile> _l)
        {
            MessageAddProfile _m = new MessageAddProfile();
            using (DataTable dt = new DataTable())
            {
                #region datatableAddProfile
                DataColumn dc;
                DataRow dr;

                dc = new DataColumn();
                dc.DataType = System.Type.GetType("System.String");
                dc.ColumnName = "IFUACode";
                dc.Unique = false;
                dt.Columns.Add(dc);

                dc = new DataColumn();
                dc.DataType = System.Type.GetType("System.String");
                dc.ColumnName = "FrontID";
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
                dc.ColumnName = "NoIdentitasInd1";
                dc.Unique = false;
                dt.Columns.Add(dc);

                dc = new DataColumn();
                dc.DataType = System.Type.GetType("System.String");
                dc.ColumnName = "IdentitasInd1";
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
                dc.ColumnName = "Pekerjaan";
                dc.Unique = false;
                dt.Columns.Add(dc);

                dc = new DataColumn();
                dc.DataType = System.Type.GetType("System.String");
                dc.ColumnName = "PenghasilanInd";
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
                dc.ColumnName = "NamaBank1";
                dc.Unique = false;
                dt.Columns.Add(dc);

                dc = new DataColumn();
                dc.DataType = System.Type.GetType("System.String");
                dc.ColumnName = "BankBranchName1";
                dc.Unique = false;
                dt.Columns.Add(dc);

                dc = new DataColumn();
                dc.DataType = System.Type.GetType("System.String");
                dc.ColumnName = "MataUang1";
                dc.Unique = false;
                dt.Columns.Add(dc);

                dc = new DataColumn();
                dc.DataType = System.Type.GetType("System.String");
                dc.ColumnName = "NomorRekening1";
                dc.Unique = false;
                dt.Columns.Add(dc);

                dc = new DataColumn();
                dc.DataType = System.Type.GetType("System.String");
                dc.ColumnName = "NamaNasabah1";
                dc.Unique = false;
                dt.Columns.Add(dc);

                dc = new DataColumn();
                dc.DataType = System.Type.GetType("System.String");
                dc.ColumnName = "NamaBank2";
                dc.Unique = false;
                dt.Columns.Add(dc);

                dc = new DataColumn();
                dc.DataType = System.Type.GetType("System.String");
                dc.ColumnName = "BankBranchName2";
                dc.Unique = false;
                dt.Columns.Add(dc);

                dc = new DataColumn();
                dc.DataType = System.Type.GetType("System.String");
                dc.ColumnName = "MataUang2";
                dc.Unique = false;
                dt.Columns.Add(dc);

                dc = new DataColumn();
                dc.DataType = System.Type.GetType("System.String");
                dc.ColumnName = "NomorRekening2";
                dc.Unique = false;
                dt.Columns.Add(dc);

                dc = new DataColumn();
                dc.DataType = System.Type.GetType("System.String");
                dc.ColumnName = "NamaNasabah2";
                dc.Unique = false;
                dt.Columns.Add(dc);

                dc = new DataColumn();
                dc.DataType = System.Type.GetType("System.String");
                dc.ColumnName = "NamaBank3";
                dc.Unique = false;
                dt.Columns.Add(dc);

                dc = new DataColumn();
                dc.DataType = System.Type.GetType("System.String");
                dc.ColumnName = "BankBranchName3";
                dc.Unique = false;
                dt.Columns.Add(dc);

                dc = new DataColumn();
                dc.DataType = System.Type.GetType("System.String");
                dc.ColumnName = "MataUang3";
                dc.Unique = false;
                dt.Columns.Add(dc);

                dc = new DataColumn();
                dc.DataType = System.Type.GetType("System.String");
                dc.ColumnName = "NomorRekening3";
                dc.Unique = false;
                dt.Columns.Add(dc);

                dc = new DataColumn();
                dc.DataType = System.Type.GetType("System.String");
                dc.ColumnName = "NamaNasabah3";
                dc.Unique = false;
                dt.Columns.Add(dc);
                #endregion

                if (_l.Count > 0)
                {
                    foreach (var result in _l)
                    {
                        dr = dt.NewRow();
                        dr["IFUACode"] = result.IFUACode;
                        dr["FrontID"] = result.FOID;
                        dr["NamaDepanInd"] = result.NamaDepanInd;
                        dr["NamaTengahInd"] = result.NamaTengahInd;
                        dr["NamaBelakangInd"] = result.NamaBelakangInd;
                        dr["Nationality"] = result.Nationality;
                        dr["NoIdentitasInd1"] = result.NoIdentitasInd1;
                        dr["IdentitasInd1"] = result.IdentitasInd1;
                        dr["NPWP"] = result.NPWP;
                        dr["RegistrationNPWP"] = result.RegistrationNPWP;
                        dr["CountryOfBirth"] = result.CountryOfBirth;
                        dr["TempatLahir"] = result.TempatLahir;
                        dr["TanggalLahir"] = result.TanggalLahir;
                        dr["JenisKelamin"] = result.JenisKelamin;
                        dr["Pendidikan"] = result.Pendidikan;
                        dr["MotherMaidenName"] = result.MotherMaidenName;
                        dr["Agama"] = result.Agama;
                        dr["Pekerjaan"] = result.Pekerjaan;
                        dr["PenghasilanInd"] = result.PenghasilanInd;
                        dr["StatusPerkawinan"] = result.StatusPerkawinan;
                        dr["SpouseName"] = result.SpouseName;
                        dr["InvestorsRiskProfile"] = result.InvestorsRiskProfile;
                        dr["MaksudTujuanInd"] = result.MaksudTujuanInd;
                        dr["SumberDanaInd"] = result.SumberDanaInd;
                        dr["AssetOwner"] = result.AssetOwner;
                        dr["OtherAlamatInd1"] = result.OtherAlamatInd1;
                        dr["OtherKodeKotaInd1"] = result.OtherKodeKotaInd1;
                        dr["OtherKodePosInd1"] = result.OtherKodePosInd1;
                        dr["AlamatInd1"] = result.AlamatInd1;
                        dr["KodeKotaInd1"] = result.KodeKotaInd1;
                        dr["KodePosInd1"] = result.KodePosInd1;
                        dr["CountryofCorrespondence"] = result.CountryofCorrespondence;
                        dr["AlamatInd2"] = result.AlamatInd2;
                        dr["KodeKotaInd2"] = result.KodeKotaInd2;
                        dr["KodePosInd2"] = result.KodePosInd2;
                        dr["CountryofDomicile"] = result.CountryofDomicile;
                        dr["TeleponRumah"] = result.TeleponRumah;
                        dr["TeleponSelular"] = result.TeleponSelular;
                        dr["Fax"] = result.Fax;
                        dr["Email"] = result.Email;
                        dr["StatementType"] = result.StatementType;
                        dr["FATCA"] = result.FATCA;
                        dr["TIN"] = result.TIN;
                        dr["TINIssuanceCountry"] = result.TINIssuanceCountry;
                        dr["NamaBank1"] = result.NamaBank1;
                        dr["BankBranchName1"] = result.BankBranchName1;
                        dr["MataUang1"] = result.MataUang1;
                        dr["NomorRekening1"] = result.NomorRekening1;
                        dr["NamaNasabah1"] = result.NamaNasabah1;
                        dr["NamaBank2"] = result.NamaBank2;
                        dr["BankBranchName2"] = result.BankBranchName2;
                        dr["MataUang2"] = result.MataUang2;
                        dr["NomorRekening2"] = result.NomorRekening2;
                        dr["NamaNasabah2"] = result.NamaNasabah2;
                        dr["NamaBank3"] = result.NamaBank3;
                        dr["BankBranchName3"] = result.BankBranchName3;
                        dr["MataUang3"] = result.MataUang3;
                        dr["NomorRekening3"] = result.NomorRekening3;
                        dr["NamaNasabah3"] = result.NamaNasabah3;
                        dt.Rows.Add(dr);
                    }
                    //Tambahin Truncate
                    using (SqlConnection conns = new SqlConnection(Tools.conString))
                    {
                        conns.Open();
                        using (SqlCommand cmd0 = conns.CreateCommand())
                        {
                            cmd0.CommandText = "truncate table ZRDO_UPDATEPROFILE";
                            cmd0.ExecuteNonQuery();
                        }
                    }
                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(Tools.conString, SqlBulkCopyOptions.TableLock))
                    {

                        bulkCopy.DestinationTableName = "dbo.ZRDO_UPDATEPROFILE";
                        bulkCopy.WriteToServer(dt);
                    }


                    // QUERY VALIDASI 
                    // Cek ada row gak
                    using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                    {
                        DbCon.Open();
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            using (SqlConnection conn = new SqlConnection(Tools.conString))
                            {
                                List<ReturnProfile> _s = new List<ReturnProfile>();
                                DateTime _now = DateTime.Now;
                                conn.Open();
                                using (SqlCommand cmd1 = conn.CreateCommand())
                                {
                                    cmd1.CommandText = @"
                                       INSERT INTO [dbo].[FundClient]  
        (
		FrontID,
		IFUACode,
FundClientPK,
HistoryPK,
Selected,
Status,
Notes,
ID,
Name,
ClientCategory,
InvestorType,
InternalCategoryPK,
SellingAgentPK,
SID,
ARIA,
Registered,
Negara,
Propinsi,
Description,
NamaPerusahaan,
Domisili,
Tipe,
Karakteristik,
NoSKD,
PenghasilanInstitusi,
SumberDanaInstitusi,
MaksudTujuanInstitusi,
AlamatPerusahaan,
KodeKotaIns,
KodePosIns,
AhliWaris,
HubunganAhliWaris,
NatureOfBusiness,
NatureOfBusinessLainnya,
Politis,
PolitisLainnya,
OtherPropinsiInd1,
OtherNegaraInd1,
OtherAlamatInd2,
OtherKodeKotaInd2,
OtherKodePosInd2,
OtherPropinsiInd2,
OtherNegaraInd2,
OtherAlamatInd3,
OtherKodeKotaInd3,
OtherKodePosInd3,
OtherPropinsiInd3,
OtherNegaraInd3,
OtherTeleponRumah,
OtherTeleponSelular,
OtherEmail,
OtherFax,
RegistrationDateIdentitasInd1,
ExpiredDateIdentitasInd1,
IdentitasInd2,
NoIdentitasInd2,
RegistrationDateIdentitasInd2,
ExpiredDateIdentitasInd2,
IdentitasInd3,
NoIdentitasInd3,
RegistrationDateIdentitasInd3,
ExpiredDateIdentitasInd3,
IdentitasInd4,
NoIdentitasInd4,
RegistrationDateIdentitasInd4,
ExpiredDateIdentitasInd4,
ExpiredDateSKD,
TanggalBerdiri,
LokasiBerdiri,
TeleponBisnis,
PhoneIns1,
EmailIns1,
PhoneIns2,
EmailIns2,
NomorAnggaran,
NomorSIUP,
AssetFor1Year,
AssetFor2Year,
AssetFor3Year,
OperatingProfitFor1Year,
OperatingProfitFor2Year,
OperatingProfitFor3Year,
NamaDepanIns1,
NamaTengahIns1,
NamaBelakangIns1,
Jabatan1,
IdentitasIns11,
NoIdentitasIns11,
RegistrationDateIdentitasIns11,
ExpiredDateIdentitasIns11,
IdentitasIns12,
NoIdentitasIns12,
RegistrationDateIdentitasIns12,
ExpiredDateIdentitasIns12,
IdentitasIns13,
NoIdentitasIns13,
RegistrationDateIdentitasIns13,
ExpiredDateIdentitasIns13,
IdentitasIns14,
NoIdentitasIns14,
RegistrationDateIdentitasIns14,
ExpiredDateIdentitasIns14,
NamaDepanIns2,
NamaTengahIns2,
NamaBelakangIns2,
Jabatan2,
IdentitasIns21,
NoIdentitasIns21,
RegistrationDateIdentitasIns21,
ExpiredDateIdentitasIns21,
IdentitasIns22,
NoIdentitasIns22,
RegistrationDateIdentitasIns22,
ExpiredDateIdentitasIns22,
IdentitasIns23,
NoIdentitasIns23,
RegistrationDateIdentitasIns23,
ExpiredDateIdentitasIns23,
IdentitasIns24,
NoIdentitasIns24,
RegistrationDateIdentitasIns24,
ExpiredDateIdentitasIns24,
NamaDepanIns3,
NamaTengahIns3,
NamaBelakangIns3,
Jabatan3,
JumlahIdentitasIns3,
IdentitasIns31,
NoIdentitasIns31,
RegistrationDateIdentitasIns31,
ExpiredDateIdentitasIns31,
IdentitasIns32,
NoIdentitasIns32,
RegistrationDateIdentitasIns32,
ExpiredDateIdentitasIns32,
IdentitasIns33,
NoIdentitasIns33,
RegistrationDateIdentitasIns33,
ExpiredDateIdentitasIns33,
IdentitasIns34,
NoIdentitasIns34,
RegistrationDateIdentitasIns34,
ExpiredDateIdentitasIns34,
NamaDepanIns4,
NamaTengahIns4,
NamaBelakangIns4,
Jabatan4,
JumlahIdentitasIns4,
IdentitasIns41,
NoIdentitasIns41,
RegistrationDateIdentitasIns41,
ExpiredDateIdentitasIns41,
IdentitasIns42,
NoIdentitasIns42,
RegistrationDateIdentitasIns42,
ExpiredDateIdentitasIns42,
IdentitasIns43,
NoIdentitasIns43,
RegistrationDateIdentitasIns43,
ExpiredDateIdentitasIns43,
IdentitasIns44,
NoIdentitasIns44,
RegistrationDateIdentitasIns44,
ExpiredDateIdentitasIns44,
GIIN,
SubstantialOwnerName,
SubstantialOwnerAddress,
SubstantialOwnerTIN,
SACode,
OldID,

SIUPExpirationDate,
CountryofEstablishment,
CompanyCityName,
NPWPPerson1,
NPWPPerson2,
CountryofCompany,
BitIsAfiliated,
AfiliatedFromPK,
BankRDNPK,
RDNAccountNo,
RDNAccountName,
DormantDate,
SpouseOccupation,
BitIsSuspend,
SuspendBy,
SuspendTime,
UnSuspendBy,
UnSuspendTime,
IsFaceToFace,
BitDefaultPayment1,
BitDefaultPayment2,
BitDefaultPayment3,
SpouseNatureOfBusiness,
SpouseNaturOfBusinessOther,
SpouseBirthPlace,
SpouseDateOfBirth,
SpouseIDNo,
SpouseNationality,
SpouseAnnualIncome,
SpouseHandphone,
SpouseNatureOfBusinessOther,
RiskProfileScore,
KYCRiskProfile,
VIrtualAccountNo,
RDNBankBranchName,
RDNCurrency,
AlamatKantorInd,
KodeKotaKantorInd,
KodePosKantorInd,
KodePropinsiKantorInd,
KodeCountryofKantor,
CorrespondenceRT,
CorrespondenceRW,
DomicileRT,
DomicileRW,
Identity1RT,
Identity1RW,
KodeDomisiliPropinsi,
OtherAgama,
OtherPendidikan,
OtherOccupation,
OtherSpouseOccupation,
OtherCurrency,
OtherSourceOfFunds,
OtherTipe,
OtherInvestmentObjectives,
OtherCharacteristic,
OtherInvestmentObjectivesIns,
OtherSourceOfFundsIns,
NamaKantor,
JabatanKantor,
Companyfax,
CompanyMail,
FrontSync,
DatePengkinianData,
MigrationStatus,
SegmentClass,
CompanyTypeOJK,
Legality,
RenewingDate,
BitShareAbleToGroup,
RemarkBank1,
RemarkBank2,
RemarkBank3,
CantSubs,
CantRedempt,
CantSwitch,
BeneficialName,
BeneficialAddress,
BeneficialIdentity,
BeneficialWork,
BeneficialRelation,
BeneficialHomeNo,
BeneficialPhoneNumber,
BeneficialNPWP,
ClientOnBoard,
Referral,
BitIsTA,
AlamatOfficer1,
AlamatOfficer2,
AlamatOfficer3,
AlamatOfficer4,
AgamaOfficer1,
AgamaOfficer2,
AgamaOfficer3,
AgamaOfficer4,
PlaceOfBirthOfficer1,
PlaceOfBirthOfficer2,
PlaceOfBirthOfficer3,
PlaceOfBirthOfficer4,
DOBOfficer1,
DOBOfficer2,
DOBOfficer3,
DOBOfficer4,

-- START FIELD UPDATE
NamaDepanInd,
NamaTengahInd,
NamaBelakangInd,
Nationality,
NoIdentitasInd1,
IdentitasInd1,
NPWP,
RegistrationNPWP,
CountryOfBirth,
TempatLahir,
TanggalLahir,
JenisKelamin,
Pendidikan,
MotherMaidenName,
Agama,
Pekerjaan,
PenghasilanInd,
StatusPerkawinan,
SpouseName,
InvestorsRiskProfile,
MaksudTujuanInd,
SumberDanaInd,
AssetOwner,
OtherAlamatInd1,
OtherKodeKotaInd1,
OtherKodePosInd1,
AlamatInd1,
KodeKotaInd1,
KodePosInd1,
CountryofCorrespondence,
AlamatInd2,
KodeKotaInd2,
KodePosInd2,
CountryofDomicile,
TeleponRumah,
TeleponSelular,
Fax,
Email,
StatementType,
FATCA,
TIN,
TINIssuanceCountry,
NamaBank1,
BankBranchName1,
MataUang1,
NomorRekening1,
NamaNasabah1,
NamaBank2,
BankBranchName2,
MataUang2,
NomorRekening2,
NamaNasabah2,
NamaBank3,
BankBranchName3,
MataUang3,
NomorRekening3,
NamaNasabah3,
EntryUsersID,
EntryTime,
UpdateUsersID,
UpdateTime,
LastUpdate
		)

	
Select 
B.FrontID,
B.IFUACode,

B.FundClientPK,C.MaxHistory + 1,0,1,'',
ID,
A.NamaDepanInd + ' ' + A.NamaTengahInd + ' ' + A.NamaBelakangInd,
ClientCategory,
InvestorType,
InternalCategoryPK,
SellingAgentPK,
SID,
ARIA,
Registered,
Negara,
Propinsi,
Description,
NamaPerusahaan,
Domisili,
Tipe,
Karakteristik,
NoSKD,
PenghasilanInstitusi,
SumberDanaInstitusi,
MaksudTujuanInstitusi,
AlamatPerusahaan,
KodeKotaIns,
KodePosIns,
AhliWaris,
HubunganAhliWaris,
NatureOfBusiness,
NatureOfBusinessLainnya,
Politis,
PolitisLainnya,
OtherPropinsiInd1,
OtherNegaraInd1,
OtherAlamatInd2,
OtherKodeKotaInd2,
OtherKodePosInd2,
OtherPropinsiInd2,
OtherNegaraInd2,
OtherAlamatInd3,
OtherKodeKotaInd3,
OtherKodePosInd3,
OtherPropinsiInd3,
OtherNegaraInd3,
OtherTeleponRumah,
OtherTeleponSelular,
OtherEmail,
OtherFax,
RegistrationDateIdentitasInd1,
ExpiredDateIdentitasInd1,
IdentitasInd2,
NoIdentitasInd2,
RegistrationDateIdentitasInd2,
ExpiredDateIdentitasInd2,
IdentitasInd3,
NoIdentitasInd3,
RegistrationDateIdentitasInd3,
ExpiredDateIdentitasInd3,
IdentitasInd4,
NoIdentitasInd4,
RegistrationDateIdentitasInd4,
ExpiredDateIdentitasInd4,
ExpiredDateSKD,
TanggalBerdiri,
LokasiBerdiri,
TeleponBisnis,
PhoneIns1,
EmailIns1,
PhoneIns2,
EmailIns2,
NomorAnggaran,
NomorSIUP,
AssetFor1Year,
AssetFor2Year,
AssetFor3Year,
OperatingProfitFor1Year,
OperatingProfitFor2Year,
OperatingProfitFor3Year,
NamaDepanIns1,
NamaTengahIns1,
NamaBelakangIns1,
Jabatan1,
IdentitasIns11,
NoIdentitasIns11,
RegistrationDateIdentitasIns11,
ExpiredDateIdentitasIns11,
IdentitasIns12,
NoIdentitasIns12,
RegistrationDateIdentitasIns12,
ExpiredDateIdentitasIns12,
IdentitasIns13,
NoIdentitasIns13,
RegistrationDateIdentitasIns13,
ExpiredDateIdentitasIns13,
IdentitasIns14,
NoIdentitasIns14,
RegistrationDateIdentitasIns14,
ExpiredDateIdentitasIns14,
NamaDepanIns2,
NamaTengahIns2,
NamaBelakangIns2,
Jabatan2,
IdentitasIns21,
NoIdentitasIns21,
RegistrationDateIdentitasIns21,
ExpiredDateIdentitasIns21,
IdentitasIns22,
NoIdentitasIns22,
RegistrationDateIdentitasIns22,
ExpiredDateIdentitasIns22,
IdentitasIns23,
NoIdentitasIns23,
RegistrationDateIdentitasIns23,
ExpiredDateIdentitasIns23,
IdentitasIns24,
NoIdentitasIns24,
RegistrationDateIdentitasIns24,
ExpiredDateIdentitasIns24,
NamaDepanIns3,
NamaTengahIns3,
NamaBelakangIns3,
Jabatan3,
JumlahIdentitasIns3,
IdentitasIns31,
NoIdentitasIns31,
RegistrationDateIdentitasIns31,
ExpiredDateIdentitasIns31,
IdentitasIns32,
NoIdentitasIns32,
RegistrationDateIdentitasIns32,
ExpiredDateIdentitasIns32,
IdentitasIns33,
NoIdentitasIns33,
RegistrationDateIdentitasIns33,
ExpiredDateIdentitasIns33,
IdentitasIns34,
NoIdentitasIns34,
RegistrationDateIdentitasIns34,
ExpiredDateIdentitasIns34,
NamaDepanIns4,
NamaTengahIns4,
NamaBelakangIns4,
Jabatan4,
JumlahIdentitasIns4,
IdentitasIns41,
NoIdentitasIns41,
RegistrationDateIdentitasIns41,
ExpiredDateIdentitasIns41,
IdentitasIns42,
NoIdentitasIns42,
RegistrationDateIdentitasIns42,
ExpiredDateIdentitasIns42,
IdentitasIns43,
NoIdentitasIns43,
RegistrationDateIdentitasIns43,
ExpiredDateIdentitasIns43,
IdentitasIns44,
NoIdentitasIns44,
RegistrationDateIdentitasIns44,
ExpiredDateIdentitasIns44,
GIIN,
SubstantialOwnerName,
SubstantialOwnerAddress,
SubstantialOwnerTIN,
SACode,
OldID,
SIUPExpirationDate,
CountryofEstablishment,
CompanyCityName,
NPWPPerson1,
NPWPPerson2,
CountryofCompany,
BitIsAfiliated,
AfiliatedFromPK,
BankRDNPK,
RDNAccountNo,
RDNAccountName,
DormantDate,
SpouseOccupation,
BitIsSuspend,
SuspendBy,
SuspendTime,
UnSuspendBy,
UnSuspendTime,
IsFaceToFace,
BitDefaultPayment1,
BitDefaultPayment2,
BitDefaultPayment3,
SpouseNatureOfBusiness,
SpouseNaturOfBusinessOther,
SpouseBirthPlace,
SpouseDateOfBirth,
SpouseIDNo,
SpouseNationality,
SpouseAnnualIncome,
SpouseHandphone,
SpouseNatureOfBusinessOther,
RiskProfileScore,
KYCRiskProfile,
VIrtualAccountNo,
RDNBankBranchName,
RDNCurrency,
AlamatKantorInd,
KodeKotaKantorInd,
KodePosKantorInd,
KodePropinsiKantorInd,
KodeCountryofKantor,
CorrespondenceRT,
CorrespondenceRW,
DomicileRT,
DomicileRW,
Identity1RT,
Identity1RW,
KodeDomisiliPropinsi,
OtherAgama,
OtherPendidikan,
OtherOccupation,
OtherSpouseOccupation,
OtherCurrency,
OtherSourceOfFunds,
OtherTipe,
OtherInvestmentObjectives,
OtherCharacteristic,
OtherInvestmentObjectivesIns,
OtherSourceOfFundsIns,
NamaKantor,
JabatanKantor,
Companyfax,
CompanyMail,
FrontSync,
DatePengkinianData,
MigrationStatus,
SegmentClass,
CompanyTypeOJK,
Legality,
RenewingDate,
BitShareAbleToGroup,
RemarkBank1,
RemarkBank2,
RemarkBank3,
CantSubs,
CantRedempt,
CantSwitch,
BeneficialName,
BeneficialAddress,
BeneficialIdentity,
BeneficialWork,
BeneficialRelation,
BeneficialHomeNo,
BeneficialPhoneNumber,
BeneficialNPWP,
ClientOnBoard,
Referral,
BitIsTA,
AlamatOfficer1,
AlamatOfficer2,
AlamatOfficer3,
AlamatOfficer4,
AgamaOfficer1,
AgamaOfficer2,
AgamaOfficer3,
AgamaOfficer4,
PlaceOfBirthOfficer1,
PlaceOfBirthOfficer2,
PlaceOfBirthOfficer3,
PlaceOfBirthOfficer4,
DOBOfficer1,
DOBOfficer2,
DOBOfficer3,
DOBOfficer4,

--START FIELD UPDATEPROFILE
A.NamaDepanInd,
A.NamaTengahInd,
A.NamaBelakangInd,
A.Nationality,
A.NoIdentitasInd1,
A.IdentitasInd1,
A.NPWP,
A.RegistrationNPWP,
A.CountryOfBirth,
A.TempatLahir,
A.TanggalLahir,
A.JenisKelamin,
A.Pendidikan,
A.MotherMaidenName,
A.Agama,
A.Pekerjaan,
A.PenghasilanInd,
A.StatusPerkawinan,
A.SpouseName,
A.InvestorsRiskProfile,
A.MaksudTujuanInd,
A.SumberDanaInd,
A.AssetOwner,
A.OtherAlamatInd1,
A.OtherKodeKotaInd1,
A.OtherKodePosInd1,
A.AlamatInd1,
A.KodeKotaInd1,
A.KodePosInd1,
A.CountryofCorrespondence,
A.AlamatInd2,
A.KodeKotaInd2,
A.KodePosInd2,
A.CountryofDomicile,
A.TeleponRumah,
A.TeleponSelular,
A.Fax,
A.Email,
A.StatementType,
A.FATCA,
A.TIN,
A.TINIssuanceCountry,
A.NamaBank1,
A.BankBranchName1,
A.MataUang1,
A.NomorRekening1,
A.NamaNasabah1,
A.NamaBank2,
A.BankBranchName2,
A.MataUang2,
A.NomorRekening2,
A.NamaNasabah2,
A.NamaBank3,
A.BankBranchName3,
A.MataUang3,
A.NomorRekening3,
A.NamaNasabah3,
B.EntryUsersID,
B.EntryTime,
'RDO',
GetDate(),
GetDate()
from ZRDO_UPDATEPROFILE A
left join FundClient B on A.FrontID = B.FrontID and 
--A.IFUACode = B.IFUACode and 
B.status = 2
left join (
	Select distinct FundClientPK,max(historyPK) MaxHistory From FundClient
	Group by FundClientPK
)C on B.FundClientPK = C.FundClientPK
Where 
--isnull(B.IFUACode,'') <> '' and 
isnull(B.FrontID,'') <> ''
and C.MaxHistory > 0
and B.FundClientPK is not null

Update B set B.Status = 4 from ZRDO_UPDATEPROFILE A
left join FundClient B on A.FrontID = B.FrontID 
--and A.IFUACode = B.IFUACode 
and B.status = 2



update A set  
A.Name = B.NamaDepanInd + ' ' + B.NamaTengahInd + ' ' + B.NamaBelakangInd,
A.NamaDepanInd = B.NamaDepanInd,
A.NamaTengahInd = B.NamaTengahInd,
A.NamaBelakangInd = B.NamaBelakangInd,
A.Nationality = B.Nationality,
A.NoIdentitasInd1 = B.NoIdentitasInd1,
A.IdentitasInd1 = B.IdentitasInd1,
A.NPWP = B.NPWP,
A.RegistrationNPWP = B.RegistrationNPWP,
A.CountryOfBirth = B.CountryOfBirth,
A.TempatLahir = B.TempatLahir,
A.TanggalLahir = B.TanggalLahir,
A.JenisKelamin = B.JenisKelamin,
A.Pendidikan = B.Pendidikan,
A.MotherMaidenName = B.MotherMaidenName,
A.Agama = B.Agama,
A.Pekerjaan = B.Pekerjaan,
A.PenghasilanInd = B.PenghasilanInd,
A.StatusPerkawinan = B.StatusPerkawinan,
A.SpouseName = B.SpouseName,
A.InvestorsRiskProfile = B.InvestorsRiskProfile,
A.MaksudTujuanInd = B.MaksudTujuanInd,
A.SumberDanaInd = B.SumberDanaInd,
A.AssetOwner = B.AssetOwner,
A.OtherAlamatInd1 = B.OtherAlamatInd1,
A.OtherKodeKotaInd1 = B.OtherKodeKotaInd1,
A.OtherKodePosInd1 = B.OtherKodePosInd1,
A.AlamatInd1 = B.AlamatInd1,
A.KodeKotaInd1 = B.KodeKotaInd1,
A.KodePosInd1 = B.KodePosInd1,
A.CountryofCorrespondence = B.CountryofCorrespondence,
A.AlamatInd2 = B.AlamatInd2,
A.KodeKotaInd2 = B.KodeKotaInd2,
A.KodePosInd2 = B.KodePosInd2,
A.CountryofDomicile = B.CountryofDomicile,
A.TeleponRumah = B.TeleponRumah,
A.TeleponSelular = B.TeleponSelular,
A.Fax = B.Fax,
A.Email = B.Email,
A.StatementType = B.StatementType,
A.FATCA = B.FATCA,
A.TIN = B.TIN,
A.TINIssuanceCountry = B.TINIssuanceCountry,
A.NamaBank1 = B.NamaBank1,
A.BankBranchName1 = B.BankBranchName1,
A.MataUang1 = B.MataUang1,
A.NomorRekening1 = B.NomorRekening1,
A.NamaNasabah1 = B.NamaNasabah1,
A.NamaBank2 = B.NamaBank2,
A.BankBranchName2 = B.BankBranchName2,
A.MataUang2 = B.MataUang2,
A.NomorRekening2 = B.NomorRekening2,
A.NamaNasabah2 = B.NamaNasabah2,
A.NamaBank3 = B.NamaBank3,
A.BankBranchName3 = B.BankBranchName3,
A.MataUang3 = B.MataUang3,
A.NomorRekening3 = B.NomorRekening3,
A.NamaNasabah3 = B.NamaNasabah3,
A.UpdateUsersID = 'RDO',
A.UpdateTime = Getdate()
from FundClient A 
Inner Join ZRDO_UPDATEPROFILE B
on A.FrontID = B.FrontID --and A.IFUACode = B.IFUACode
where A.Status = 1

 


 
Select '1'
                                            ";

                                    cmd1.Parameters.AddWithValue("@TimeNow", _now);
                                    using (SqlDataReader dr01 = cmd1.ExecuteReader())
                                    {
                                        if (dr01.HasRows)
                                        {
                                            while (dr01.Read())
                                            {

                                                _m.code = 200;
                                                _m.message = "Update Fundclient Success";
                                                _m.data = null;
                                            }

                                        }
                                    }

                                }
                            }
                            return _m;
                        }
                    }

                    // Jika ada row
                    // Set ke ReturnValidateProfile
                    // return message

                    // Jika tidak ada row
                    // Query Insert dari temp ke Table Client
                    // set ke ReturnProfile
                    // return message
                }
                else
                {

                    _m.code = 204;
                    _m.message = "No Data To be Update";
                    return _m;
                }

            }
            //return null;

        }
        #endregion

        //Add Transaction
        #region test Post/Add data //Add Transaction

        public class ReturnTransaction
        {
            public int TransactionPK { get; set; }
            public string TrxID { get; set; }
            public string TrxType { get; set; }
        }

        public class MessageAddTransaction
        {
            public int code { get; set; }
            public string message { get; set; }
            public List<ReturnTransaction> data { get; set; }
        }


        private ReturnTransaction setReturnTransaction(SqlDataReader dr0)
        {
            ReturnTransaction _m = new ReturnTransaction();
            _m.TransactionPK = Convert.ToInt32(dr0["TransactionPK"]);
            _m.TrxID = dr0["TrxID"].ToString();
            _m.TrxType = dr0["Type"].ToString();
            return _m;
        }


        public MessageAddTransaction RDOAddTransaction(List<AddTransaction> _l)
        {
            MessageAddTransaction _m = new MessageAddTransaction();
            using (DataTable dt = new DataTable())
            {

                #region datatable AddTransaction
                DataColumn dc;
                DataRow dr;

                dc = new DataColumn();
                dc.DataType = System.Type.GetType("System.String");
                dc.ColumnName = "TrxID";
                dc.Unique = false;
                dt.Columns.Add(dc);

                dc = new DataColumn();
                dc.DataType = System.Type.GetType("System.String");
                dc.ColumnName = "IFUACode";
                dc.Unique = false;
                dt.Columns.Add(dc);

                dc = new DataColumn();
                dc.DataType = System.Type.GetType("System.String");
                dc.ColumnName = "NAVDate";
                dc.Unique = false;
                dt.Columns.Add(dc);

                dc = new DataColumn();
                dc.DataType = System.Type.GetType("System.String");
                dc.ColumnName = "FundCode";
                dc.Unique = false;
                dt.Columns.Add(dc);

                dc = new DataColumn();
                dc.DataType = System.Type.GetType("System.String");
                dc.ColumnName = "FundCodeTo";
                dc.Unique = false;
                dt.Columns.Add(dc);

                dc = new DataColumn();
                dc.DataType = System.Type.GetType("System.String");
                dc.ColumnName = "TrxType";
                dc.Unique = false;
                dt.Columns.Add(dc);

                dc = new DataColumn();
                dc.DataType = System.Type.GetType("System.String");
                dc.ColumnName = "Amount";
                dc.Unique = false;
                dt.Columns.Add(dc);

                dc = new DataColumn();
                dc.DataType = System.Type.GetType("System.String");
                dc.ColumnName = "Unit";
                dc.Unique = false;
                dt.Columns.Add(dc);

                dc = new DataColumn();
                dc.DataType = System.Type.GetType("System.String");
                dc.ColumnName = "FeePercent";
                dc.Unique = false;
                dt.Columns.Add(dc);

                dc = new DataColumn();
                dc.DataType = System.Type.GetType("System.String");
                dc.ColumnName = "FeeAmount";
                dc.Unique = false;
                dt.Columns.Add(dc);

                dc = new DataColumn();
                dc.DataType = System.Type.GetType("System.String");
                dc.ColumnName = "SourceOfFund";
                dc.Unique = false;
                dt.Columns.Add(dc);

                dc = new DataColumn();
                dc.DataType = System.Type.GetType("System.String");
                dc.ColumnName = "BankRecipient";
                dc.Unique = false;
                dt.Columns.Add(dc);

                dc = new DataColumn();
                dc.DataType = System.Type.GetType("System.String");
                dc.ColumnName = "Type";
                dc.Unique = false;
                dt.Columns.Add(dc);
                #endregion


                if (_l.Count > 0)
                {
                    foreach (var result in _l)
                    {
                        dr = dt.NewRow();
                        dr["TrxID"] = result.TrxID;
                        dr["IFUACode"] = result.IFUACode;
                        dr["NAVDate"] = result.NAVDate;
                        dr["FundCode"] = result.FundCode;
                        dr["FundCodeTo"] = result.FundCodeTo;
                        dr["TrxType"] = result.TrxType;
                        dr["Amount"] = result.Amount;
                        dr["Unit"] = result.Unit;
                        dr["FeePercent"] = result.FeePercent;
                        dr["FeeAmount"] = result.FeeAmount;
                        dr["SourceOfFund"] = result.SourceOfFund;
                        dr["BankRecipient"] = result.BankRecipient;
                        dr["Type"] = result.Type;
                        dt.Rows.Add(dr);
                    }
                    //Tambahin Truncate
                    using (SqlConnection conns = new SqlConnection(Tools.conString))
                    {
                        conns.Open();
                        using (SqlCommand cmd0 = conns.CreateCommand())
                        {
                            cmd0.CommandText = "truncate table ZRDO_ADDTRANSACTION";
                            cmd0.ExecuteNonQuery();
                        }
                    }

                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(Tools.conString, SqlBulkCopyOptions.TableLock))
                    {

                        bulkCopy.DestinationTableName = "dbo.ZRDO_ADDTRANSACTION";
                        bulkCopy.WriteToServer(dt);
                    }


                    using (SqlConnection conn = new SqlConnection(Tools.conString))
                    {
                        List<ReturnTransaction> _s = new List<ReturnTransaction>();
                        DateTime _now = DateTime.Now;
                        conn.Open();
                        using (SqlCommand cmd1 = conn.CreateCommand())
                        {
                            cmd1.CommandText = @"
                            DECLARE @MaxPK INT

                            SELECT @MaxPK = MAX(ClientSubscriptionPK) + 1 FROM dbo.ClientSubscription

                            SET @MaxPK = ISNULL(@MaxPK,0)

                            INSERT INTO dbo.ClientSubscription
                                    ( ClientSubscriptionPK ,
                                      HistoryPK ,
                                      Selected ,
                                      Status ,
                                      Notes ,
                                      Type , -- Tipe online
                                      FeeType , -- Case when fee amount > 0 atau feepercent > 0
                                      NAVDate , 
                                      ValueDate ,
                                      NAV , -- 0
                                      FundPK , -- FundCode join sama ID di table Fund
                                      FundClientPK , -- IFUACode
                                      CashRefPK , --0
                                      Description ,--''
                                      CashAmount ,
                                      UnitAmount ,
                                      TotalCashAmount ,
                                      TotalUnitAmount ,
                                      SubscriptionFeePercent ,
                                      SubscriptionFeeAmount ,
                                      AgentPK , --FundClient SellingAgentPK
                                      AgentFeePercent ,
                                      AgentFeeAmount ,
                                      DepartmentPK ,
                                      CurrencyPK , -- Ambil dari Fund
                                      AutoDebitDate ,
                                      IsBOTransaction ,--0
                                      BitSinvest ,--0
                                      EntryUsersID ,
                                      EntryTime ,
                                      LastUpdate ,
                                      BitImmediateTransaction ,
                                      TransactionPK , -- TRXID Dari Front
                                      IsFrontSync ,
                                      ReferenceSInvest ,
                                      BankRecipientPK ,
                                      Tenor ,
                                      InterestRate ,
                                      PaymentTerm ,
                                      SumberDana ,
                                      TransactionPromoPK
                                    )
                            SELECT @maxPK + ROW_NUMBER() OVER(ORDER BY navdate ASC) ClientSubscriptionPK,1 HistoryPK,0 Selected,1 Status,'' Notes,4 Type,  
                            case when a.FeeAmount > 0 then 2 else case when a.FeePercent > 0 then 1 end end FeeType, a.navdate,
                            a.navdate valuedate,0 NAV,B.FundPK,D.FundClientPK,0 CashRefPK,'',CAST(A.Amount AS NUMERIC(22,4)),CAST(A.Unit AS NUMERIC(22,4)),CAST(A.Amount AS NUMERIC(22,4)) - CAST(A.FeeAmount AS NUMERIC(22,4)),0,0,
                            CAST(A.FeeAmount AS NUMERIC(22,4)) SubsFeeAmount,D.SellingAgentPK,0,0,0,B.CurrencyPK,0 AutoDebitDate,0 IsBOTransaction, 0 BitSinvest,

                            'admin',@TimeNow,
                            @TimeNow,0 BitImmediateTransaction,A.TrxID TransactionPK,0 IsFrontSync,
                            0 ReferenceSInvest,a.BankRecipient BankRecipientPK,0 Tenor,0 InterestRate,0 PaymentTerm,A.SourceOfFund,0 TransactionPromoPK
                            FROM dbo.ZRDO_ADDTRANSACTION A
                            LEFT JOIN Fund B ON A.FundCode = B.ID AND B.status IN (1,2)
                            LEFT JOIN Fundclient D ON A.IFUACode = D.IFUACode AND D.status IN (1,2)
                            WHERE A.TrxType = 'SUB'


                            --DECLARE @MaxPK INT
                            SELECT @MaxPK = MAX(ClientRedemptionPK) + 1 FROM dbo.ClientRedemption

                            SET @MaxPK = ISNULL(@MaxPK,0)

                            INSERT INTO dbo.ClientRedemption
                                    ( ClientRedemptionPK ,
                                      HistoryPK ,Selected ,Status ,Notes ,Type ,FeeType ,NAVDate,
                                      ValueDate ,PaymentDate,NAV ,FundPK ,
                                      FundClientPK ,CashRefPK,BitRedemptionAll ,Description ,
                                      CashAmount ,UnitAmount ,TotalCashAmount ,
                                      TotalUnitAmount ,RedemptionFeePercent ,
                                      RedemptionFeeAmount ,AgentPK ,
                                      AgentFeePercent ,AgentFeeAmount ,
                                      DepartmentPK ,CurrencyPK ,
                                      IsBOTransaction ,
                                      BitSinvest ,Posted ,
                                      EntryUsersID ,
                                      EntryTime ,
		                              LastUpdate ,
                                      TransactionPK ,IsFrontSync ,
                                      ReferenceSInvest ,BankRecipientPK,TransferType
                                    )


                            SELECT @maxPK + ROW_NUMBER() OVER(ORDER BY NAVDate ASC),1,0,1,'',1,
                            case when a.FeeAmount > 0 then 2 else case when a.FeePercent > 0 then 1 end end
                            ,NAVDate,NAVDate,A.NAVDate,0,
                            B.FundPK,C.FundClientPK,0,0,
                            '',CAST(A.Amount AS NUMERIC(22,4)),CAST(A.Unit AS Decimal(22,4)),CAST(A.Amount AS NUMERIC(22,4)) - CAST(A.FeeAmount AS NUMERIC(22,4)),0,0,A.FeeAmount,C.SellingAgentPK,0,0
                            ,1,1,0,1,0,'admin',@TimeNow,@TimeNow,A.TrxID,0,'',a.BankRecipient,0
                            FROM dbo.ZRDO_ADDTRANSACTION A
                            LEFT JOIN Fund B ON A.FundCode = B.ID AND B.status IN (1,2)
                            LEFT JOIN Fundclient C ON A.IFUACode = C.IFUACode AND C.status IN (1,2)
                            WHERE A.TrxType = 'RED'

                            SELECT ClientSubscriptionPK TransactionPK,isnull(TransactionPK,0) TrxID
                            ,'SUB' Type FROM dbo.ClientSubscription where transactionpk in(select trxid from dbo.ZRDO_ADDTRANSACTION)
                            UNION ALL
                            SELECT ClientRedemptionPK TransactionPK,isnull(TransactionPK,0) TrxID
                            ,'RED' Type FROM dbo.ClientRedemption where transactionpk in(select trxid from dbo.ZRDO_ADDTRANSACTION)
                            UNION ALL
                            SELECT ClientSwitchingPK TransactionPK,isnull(TransactionPK,0) TrxID
                            ,'SWI' Type FROM dbo.ClientSwitching where transactionpk in(select trxid from dbo.ZRDO_ADDTRANSACTION)";

                            cmd1.Parameters.AddWithValue("@TimeNow", _now);
                            using (SqlDataReader dr01 = cmd1.ExecuteReader())
                            {
                                if (dr01.HasRows)
                                {
                                    while (dr01.Read())
                                    {

                                        _m.code = 200;
                                        _m.message = "Insert Transaction Success";
                                        _s.Add(setReturnTransaction(dr01));
                                    }
                                    _m.data = _s;
                                }
                            }
                        }
                    }
                    return _m;

                }
                else
                {

                    _m.code = 204;
                    _m.message = "No Data To be insert";

                    return _m;
                }
            }

        }
        #endregion

        //Get Existing Client
        #region getExistingClient


        public class MessageGetExistingClient
        {
            public int code { get; set; }
            public string message { get; set; }
            public List<GetExistingClient> data { get; set; }
            public List<ReturnGetExistingClient> dataReturn { get; set; }
        }


        private ReturnGetExistingClient setExistingClient(SqlDataReader dr)
        {
            ReturnGetExistingClient _m = new ReturnGetExistingClient();
            _m.NoIdentitasInd1 = dr["NoIdentitasInd1"].ToString();
            _m.Status = dr["Status"].ToString();
            _m.Message = dr["Message"].ToString();
            _m.IFUACode = dr["IFUACode"].ToString();
            _m.SID = dr["SID"].ToString();
            _m.Verify = dr["Verify"].ToString();
            _m.NamaDepanInd = dr["NamaDepanInd"].ToString();
            _m.NamaTengahInd = dr["NamaTengahInd"].ToString();
            _m.NamaBelakangInd = dr["NamaBelakangInd"].ToString();
            _m.Nationality = dr["Nationality"].ToString();
            _m.NoIdentitasInd1 = dr["NoIdentitasInd1"].ToString();
            _m.IdentitasInd1 = dr["IdentitasInd1"].ToString();
            _m.NPWP = dr["NPWP"].ToString();
            _m.RegistrationNPWP = Convert.ToDateTime(dr["RegistrationNPWP"].ToString()).ToString("yyyy-MM-dd");
            _m.CountryOfBirth = dr["CountryOfBirth"].ToString();
            _m.TempatLahir = dr["TempatLahir"].ToString();
            _m.TanggalLahir = Convert.ToDateTime(dr["TanggalLahir"].ToString()).ToString("yyyy-MM-dd");
            _m.JenisKelamin = dr["JenisKelamin"].ToString();
            _m.Pendidikan = dr["Pendidikan"].ToString();
            _m.MotherMaidenName = dr["MotherMaidenName"].ToString();
            _m.Agama = dr["Agama"].ToString();
            _m.Pekerjaan = dr["Pekerjaan"].ToString();
            _m.PenghasilanInd = dr["PenghasilanInd"].ToString();
            _m.StatusPerkawinan = dr["StatusPerkawinan"].ToString();
            _m.SpouseName = dr["SpouseName"].ToString();
            _m.InvestorsRiskProfile = dr["InvestorsRiskProfile"].ToString();
            _m.MaksudTujuanInd = dr["MaksudTujuanInd"].ToString();
            _m.SumberDanaInd = dr["SumberDanaInd"].ToString();
            _m.AssetOwner = dr["AssetOwner"].ToString();
            _m.OtherAlamatInd1 = dr["OtherAlamatInd1"].ToString();
            _m.OtherKodeKotaInd1 = dr["OtherKodeKotaInd1"].ToString();
            _m.OtherKodePosInd1 = dr["OtherKodePosInd1"].ToString();
            _m.AlamatInd1 = dr["AlamatInd1"].ToString();
            _m.KodeKotaInd1 = dr["KodeKotaInd1"].ToString();
            _m.KodePosInd1 = dr["KodePosInd1"].ToString();
            _m.CountryofCorrespondence = dr["CountryofCorrespondence"].ToString();
            _m.AlamatInd2 = dr["AlamatInd2"].ToString();
            _m.KodeKotaInd2 = dr["KodeKotaInd2"].ToString();
            _m.KodePosInd2 = dr["KodePosInd2"].ToString();
            _m.CountryofDomicile = dr["CountryofDomicile"].ToString();
            _m.TeleponRumah = dr["TeleponRumah"].ToString();
            _m.TeleponSelular = dr["TeleponSelular"].ToString();
            _m.Fax = dr["Fax"].ToString();
            _m.Email = dr["Email"].ToString();
            _m.StatementType = dr["StatementType"].ToString();
            _m.FATCA = dr["FATCA"].ToString();
            _m.TIN = dr["TIN"].ToString();
            _m.TINIssuanceCountry = dr["TINIssuanceCountry"].ToString();
            _m.NamaBank1 = dr["NamaBank1"].ToString();
            _m.BankBranchName1 = dr["BankBranchName1"].ToString();
            _m.MataUang1 = dr["MataUang1"].ToString();
            _m.NomorRekening1 = dr["NomorRekening1"].ToString();
            _m.NamaNasabah1 = dr["NamaNasabah1"].ToString();
            _m.NamaBank2 = dr["NamaBank2"].ToString();
            _m.BankBranchName2 = dr["BankBranchName2"].ToString();
            _m.MataUang2 = dr["MataUang2"].ToString();
            _m.NomorRekening2 = dr["NomorRekening2"].ToString();
            _m.NamaNasabah2 = dr["NamaNasabah2"].ToString();
            _m.NamaBank3 = dr["NamaBank3"].ToString();
            _m.BankBranchName3 = dr["BankBranchName3"].ToString();
            _m.MataUang3 = dr["MataUang3"].ToString();
            _m.NomorRekening3 = dr["NomorRekening3"].ToString();
            _m.NamaNasabah3 = dr["NamaNasabah3"].ToString();
            _m.LastUpdate = dr["LastUpdate"].ToString();
            return _m;
        }

        public MessageGetExistingClient GetExistingClient(List<GetExistingClient> _l)
        {
            MessageGetExistingClient _m = new MessageGetExistingClient();

            using (DataTable dt = new DataTable())
            {
                DataColumn dc;
                DataRow dr;
                dc = new DataColumn();
                dc.DataType = System.Type.GetType("System.String");
                dc.ColumnName = "IDCardNo";
                dc.Unique = false;
                dt.Columns.Add(dc);

                dc = new DataColumn();
                dc.DataType = System.Type.GetType("System.String");
                dc.ColumnName = "TanggalLahir";
                dc.Unique = false;
                dt.Columns.Add(dc);

                dc = new DataColumn();
                dc.DataType = System.Type.GetType("System.String");
                dc.ColumnName = "Email";
                dc.Unique = false;
                dt.Columns.Add(dc);

                dc = new DataColumn();
                dc.DataType = System.Type.GetType("System.String");
                dc.ColumnName = "NamaSesuaiKTP";
                dc.Unique = false;
                dt.Columns.Add(dc);

                if (_l.Count > 0)
                {
                    foreach (var result in _l)
                    {
                        dr = dt.NewRow();
                        dr["IDCardNo"] = result.IDCardNo;
                        dr["TanggalLahir"] = result.TanggalLahir;
                        dr["Email"] = result.Email;
                        dr["NamaSesuaiKTP"] = result.NamaSesuaiKTP;
                        dt.Rows.Add(dr);
                    }
                    //Tambahin Truncate

                    string _idCardNo = dt.Rows[0][0].ToString();
                    string _tanggalLahir = dt.Rows[0][1].ToString();
                    string _email = dt.Rows[0][2].ToString();
                    string _namaSesuaiKTP = dt.Rows[0][3].ToString();

                    _m.code = 200;
                    _m.message = "SUKSES";

                    using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                    {
                        DbCon.Open();
                        List<ReturnGetExistingClient> _e = new List<ReturnGetExistingClient>();
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = @"SELECT Status, '' Message,
                                              isnull(IFUACode,'')IFUACode,
											  isnull(SID,'')SID,
                                              'true' Verify,
                                              isnull(NamaDepanInd ,'')NamaDepanInd,
                                              isnull(NamaTengahInd ,'')NamaTengahInd,
                                              isnull(NamaBelakangInd ,'')NamaBelakangInd,
                                              isnull(Nationality,0)Nationality,
                                              isnull(NoIdentitasInd1 ,'')NoIdentitasInd1,
                                              isnull(IdentitasInd1 ,'')IdentitasInd1,
                                              isnull(NPWP ,'')NPWP,
                                              isnull(RegistrationNPWP ,'')RegistrationNPWP,
                                              isnull(CountryOfBirth ,0)CountryOfBirth,
                                              isnull(TempatLahir ,'')TempatLahir,
                                              isnull(TanggalLahir ,0)TanggalLahir,
                                              isnull(JenisKelamin ,0)JenisKelamin,
                                              isnull(Pendidikan ,0)Pendidikan,
                                              isnull(MotherMaidenName ,'')MotherMaidenName,
                                              isnull(Agama ,0)Agama,
                                              isnull(Pekerjaan ,0)Pekerjaan,
                                              isnull(PenghasilanInd ,0)PenghasilanInd,
                                              isnull(StatusPerkawinan ,0)StatusPerkawinan,
                                              isnull(SpouseName ,'')SpouseName,
                                              isnull(InvestorsRiskProfile ,0)InvestorsRiskProfile,
                                              isnull(MaksudTujuanInd ,0)MaksudTujuanInd,
                                              isnull(SumberDanaInd ,0)SumberDanaInd,
                                              isnull(AssetOwner ,0)AssetOwner,
                                              isnull(OtherAlamatInd1 ,'')OtherAlamatInd1,
                                              isnull(OtherKodeKotaInd1 ,0)OtherKodeKotaInd1,
                                              isnull(OtherKodePosInd1 ,'')OtherKodePosInd1,
                                              isnull(AlamatInd1 ,'')AlamatInd1,
                                              isnull(KodeKotaInd1 ,0)KodeKotaInd1,
                                              isnull(KodePosInd1 ,'')KodePosInd1,
                                              isnull(CountryofCorrespondence ,0)CountryofCorrespondence,
                                              isnull(AlamatInd2 ,'')AlamatInd2,
                                              isnull(KodeKotaInd2 ,0)KodeKotaInd2,
                                              isnull(KodePosInd2 ,'')KodePosInd2,
                                              isnull(CountryofDomicile ,0)CountryofDomicile,
                                              isnull(TeleponRumah ,'')TeleponRumah,
                                              isnull(TeleponSelular ,'')TeleponSelular,
                                              isnull(Fax ,'')Fax,
                                              isnull(Email ,'')Email,
                                              isnull(StatementType ,0)StatementType,
                                              isnull(FATCA ,0)FATCA,
                                              isnull(TIN ,'')TIN,
                                              isnull(TINIssuanceCountry ,0)TINIssuanceCountry,
                                              isnull(NamaBank1 ,0)NamaBank1,
                                              isnull(BankBranchName1 ,0)BankBranchName1,
                                              isnull(MataUang1 ,0)MataUang1,
                                              isnull(NomorRekening1 ,'')NomorRekening1,
                                              isnull(NamaNasabah1 ,'')NamaNasabah1,
                                              isnull(NamaBank2 ,0)NamaBank2,
                                              isnull(BankBranchName2 ,0)BankBranchName2,
                                              isnull(MataUang2 ,0)MataUang2,
                                              isnull(NomorRekening2 ,'')NomorRekening2,
                                              isnull(NamaNasabah2 ,'')NamaNasabah2,
                                              isnull(NamaBank3 ,0)NamaBank3,
                                              isnull(BankBranchName3 ,0)BankBranchName3,
                                              isnull(MataUang3 ,0)MataUang3,
                                              isnull(NomorRekening3 ,'')NomorRekening3,
                                              isnull(NamaNasabah3,'')NamaNasabah3, CONVERT(VARCHAR(20), isnull(LastUpdate,0), 126) LastUpdate  From FundClient WHERE NoIdentitasInd1 = @IDCardNo and TanggalLahir = @TanggalLahir
                                              and Email = @Email and Name = @NamaSesuaiKTP";

                            cmd.Parameters.AddWithValue("@IDCardNo", _idCardNo);
                            cmd.Parameters.AddWithValue("@TanggalLahir", _tanggalLahir);
                            cmd.Parameters.AddWithValue("@Email", _email);
                            cmd.Parameters.AddWithValue("@NamaSesuaiKTP", _namaSesuaiKTP);
                            using (SqlDataReader dr01 = cmd.ExecuteReader())
                            {
                                if (dr01.HasRows)
                                {
                                    while (dr01.Read())
                                    {
                                        _e.Add(setExistingClient(dr01));
                                    }
                                    _m.code = 200;
                                    _m.message = "Get Existing Client Success";
                                    _m.dataReturn = _e;
                                }
                                //else
                                //{
                                //    _m.code = 204;
                                //    _m.message = "No Data To be insert";
                                //}


                            }
                            return _m;
                        }
                    }


                }
                else
                {
                    _m.code = 204;
                    _m.message = "No Data To be insert";

                    return _m;
                }
            }

        }
        #endregion

        #region Validate Transaction
        public class MessageValidateTransaction
        {
            public int code { get; set; }
            public string message { get; set; }
            public List<ReturnValidateTransaction> data { get; set; }
        }

        public class ReturnValidateTransaction
        {
            public string Status { get; set; }
            public string Message { get; set; }

        }

        private ReturnValidateTransaction setReturnValidateTransaction(SqlDataReader dr0)
        {
            ReturnValidateTransaction _m = new ReturnValidateTransaction();
            _m.Status = dr0["Status"].ToString();
            _m.Message = dr0["Message"].ToString();
            return _m;
        }

        public MessageValidateTransaction ValidateTransaction(AddTransaction _l)
        {

            using (SqlConnection conn = new SqlConnection(Tools.conString))
            {
                List<ReturnValidateTransaction> _s = new List<ReturnValidateTransaction>();
                DateTime _now = DateTime.Now;
                conn.Open();
                using (SqlCommand cmd1 = conn.CreateCommand())
                {
                    cmd1.CommandText = @"
                                                    
                    IF @Amount < 100000
BEGIN
    Select 2 Status,'Minimum Subscription tidak terpenuhi' Message
END
ELSE
BEGIN
    Select 1 Status,'Success' Message
END
                            ";

                    cmd1.Parameters.AddWithValue("@Amount", _l.Amount);
                    using (SqlDataReader dr01 = cmd1.ExecuteReader())
                    {
                        MessageValidateTransaction _m = new MessageValidateTransaction();
                        if (dr01.HasRows)
                        {
                            
                            while (dr01.Read())
                            {
                                _m.code = 200;
                                _m.message = "Validate Transaction Success";
                                _s.Add(setReturnValidateTransaction(dr01));
                            }
                            _m.data = _s;
                            return _m;

                        }
                        else
                        {
                            _m.code = 200;
                            _m.message = "No Data For Validate Transaction";
                            _m.data = null;
                            return _m;
                        }
                       
                    }
                }
            }

        }
        #endregion
    }
}