using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RFSModel;
using RFSUtility;
using System.Data.SqlClient;


namespace RFSRepository
{
    public class FundReps
    {
        Host _host = new Host();
        ConnectionConfig DbCon = new ConnectionConfig();
        //1
        string _insertCommand = @"INSERT INTO [dbo].[Fund]  
                            ([FundPK],[HistoryPK],[Status],[ID],[Name],[CurrencyPK],[Type],  
                            [BankBranchPK],[MaxUnits],[TotalUnits],[Nav],[EffectiveDate],[MaturityDate],[CustodyFeePercent],[ManagementFeePercent],[SubscriptionFeePercent],[RedemptionFeePercent],[SwitchingFeePercent],[NavRoundingMode],[NavDecimalPlaces],[UnitDecimalPlaces],[UnitRoundingMode],  
                            [NKPDName],[SInvestCode],[FundTypeInternal],[BloombergCode],[MinSwitch],[MinBalSwitchAmt],[MinBalSwitchUnit],[MinSubs],[MinReds],[MinBalRedsAmt],[MinBalRedsUnit],[CounterpartPK],[IsPublic],[WHTDueDate],[IssueDate],[MFeeMethod],[RemainingBalanceUnit],[BitNeedRecon],[DefaultPaymentDate],[SharingFeeCalculation],[DividenDate],[NPWP],[OJKLetter],[BitSinvestFee],  
                            [KPDNoContract],[KPDDateFromContract],[KPDDateToContract],[KPDNoAdendum],[KPDDateAdendum],[CutOffTime],[ProspectusUrl],[FactsheetUrl],[ImageUrl],[ISIN],[EntryApproveTimeCutoff ],[BitInternalClosePrice],[BitInvestmentHighRisk],[OJKEffectiveStatementLetterDate],[MinBalSwitchToAmt],[BitSyariahFund],";

        string _paramaterCommand = @"@ID,@Name,@CurrencyPK,@Type,  
                            @BankBranchPK,@MaxUnits,@TotalUnits,@Nav,@EffectiveDate,@MaturityDate,@CustodyFeePercent,@ManagementFeePercent,@SubscriptionFeePercent,@RedemptionFeePercent,@SwitchingFeePercent,@NavRoundingMode,@NavDecimalPlaces,@UnitDecimalPlaces,@UnitRoundingMode, 
                            @NKPDName,@SInvestCode,@FundTypeInternal,@BloombergCode,@MinSwitch,@MinBalSwitchAmt,@MinBalSwitchUnit,@MinSubs,@MinReds,@MinBalRedsAmt,@MinBalRedsUnit,@CounterpartPK,@IsPublic,@WHTDueDate,@IssueDate,@MFeeMethod,@RemainingBalanceUnit,@BitNeedRecon,@DefaultPaymentDate,@SharingFeeCalculation,@DividenDate,@NPWP,@OJKLetter,@BitSinvestFee, 
                            @KPDNoContract,@KPDDateFromContract,@KPDDateToContract,@KPDNoAdendum,@KPDDateAdendum,@CutOffTime,@ProspectusUrl,@FactsheetUrl,@ImageUrl,@ISIN,@EntryApproveTimeCutoff,@BitInternalClosePrice,@BitInvestmentHighRisk,@OJKEffectiveStatementLetterDate,@MinBalSwitchToAmt,@BitSyariahFund,";

        //2
        private Fund setFund(SqlDataReader dr)
        {
            Fund M_Fund = new Fund();
            M_Fund.FundPK = Convert.ToInt32(dr["FundPK"]);
            M_Fund.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_Fund.Status = Convert.ToInt32(dr["Status"]);
            M_Fund.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_Fund.Notes = Convert.ToString(dr["Notes"]);
            M_Fund.ID = dr["ID"].ToString();
            M_Fund.Name = dr["Name"].ToString();
            M_Fund.CurrencyPK = Convert.ToInt16(dr["CurrencyPK"]);
            M_Fund.CurrencyID = Convert.ToString(dr["CurrencyID"]);
            M_Fund.Type = Convert.ToInt16(dr["Type"]);
            M_Fund.TypeDesc = Convert.ToString(dr["TypeDesc"]);
            M_Fund.BankBranchPK = Convert.ToInt16(dr["BankBranchPK"]);
            M_Fund.BankBranchID = Convert.ToString(dr["BankBranchID"]);
            M_Fund.MaxUnits = Convert.ToDecimal(dr["MaxUnits"]);
            M_Fund.TotalUnits = Convert.ToDecimal(dr["TotalUnits"]);
            M_Fund.Nav = Convert.ToDecimal(dr["Nav"]);
            M_Fund.EffectiveDate = dr["EffectiveDate"].ToString();
            M_Fund.MaturityDate = dr["MaturityDate"].ToString();
            M_Fund.CustodyFeePercent = Convert.ToDecimal(dr["CustodyFeePercent"]);
            M_Fund.ManagementFeePercent = Convert.ToDecimal(dr["ManagementFeePercent"]);
            M_Fund.SubscriptionFeePercent = Convert.ToDecimal(dr["SubscriptionFeePercent"]);
            M_Fund.RedemptionFeePercent = Convert.ToDecimal(dr["RedemptionFeePercent"]);
            M_Fund.SwitchingFeePercent = Convert.ToDecimal(dr["SwitchingFeePercent"]);
            M_Fund.NavRoundingMode = Convert.ToInt32(dr["NavRoundingMode"]);
            M_Fund.NavRoundingModeDesc = Convert.ToString(dr["NavRoundingModeDesc"]);
            M_Fund.NavDecimalPlaces = Convert.ToInt32(dr["NavDecimalPlaces"]);
            M_Fund.NavDecimalPlacesDesc = Convert.ToString(dr["NavDecimalPlacesDesc"]);
            M_Fund.UnitDecimalPlaces = Convert.ToInt32(dr["UnitDecimalPlaces"]);
            M_Fund.UnitDecimalPlacesDesc = Convert.ToString(dr["UnitDecimalPlacesDesc"]);
            M_Fund.UnitRoundingMode = Convert.ToInt32(dr["UnitRoundingMode"]);
            M_Fund.UnitRoundingModeDesc = Convert.ToString(dr["UnitRoundingModeDesc"]);
            M_Fund.NKPDName = Convert.ToString(dr["NKPDName"]);
            M_Fund.SInvestCode = Convert.ToString(dr["SInvestCode"]);
            M_Fund.BloombergCode = Convert.ToString(dr["BloombergCode"]);
            M_Fund.FundTypeInternal = Convert.ToInt16(dr["FundTypeInternal"]);
            M_Fund.FundTypeInternalDesc = Convert.ToString(dr["FundTypeInternalDesc"]);
            M_Fund.MinSwitch = Convert.ToDecimal(dr["MinSwitch"]);
            M_Fund.MinBalSwitchAmt = Convert.ToDecimal(dr["MinBalSwitchAmt"]);
            M_Fund.MinBalSwitchUnit = Convert.ToDecimal(dr["MinBalSwitchUnit"]);
            M_Fund.MinSubs = Convert.ToDecimal(dr["MinSubs"]);
            M_Fund.MinReds = Convert.ToDecimal(dr["MinReds"]);
            M_Fund.MinBalRedsAmt = Convert.ToDecimal(dr["MinBalRedsAmt"]);
            M_Fund.MinBalRedsUnit = Convert.ToDecimal(dr["MinBalRedsUnit"]);
            M_Fund.CounterpartPK = Convert.ToInt16(dr["CounterpartPK"]);
            M_Fund.CounterpartName = Convert.ToString(dr["CounterpartName"]);
            M_Fund.IsPublic = Convert.ToBoolean(dr["IsPublic"]);
            M_Fund.WHTDueDate = Convert.ToInt32(dr["WHTDueDate"]);
            M_Fund.WHTDueDateDesc = Convert.ToString(dr["WHTDueDateDesc"]);
            M_Fund.IssueDate = dr["IssueDate"].ToString();
            M_Fund.MFeeMethod = Convert.ToInt32(dr["MFeeMethod"]);
            M_Fund.MFeeMethodDesc = dr["MFeeMethodDesc"].Equals(DBNull.Value) == true ? "" : dr["MFeeMethodDesc"].ToString();
            M_Fund.RemainingBalanceUnit = dr["RemainingBalanceUnit"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["RemainingBalanceUnit"]);
            M_Fund.BitNeedRecon = Convert.ToBoolean(dr["BitNeedRecon"]);
            M_Fund.DefaultPaymentDate = dr["DefaultPaymentDate"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["DefaultPaymentDate"]);
            M_Fund.SharingFeeCalculation = dr["SharingFeeCalculation"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["SharingFeeCalculation"]);
            M_Fund.SharingFeeCalculationDesc = dr["SharingFeeCalculationDesc"].Equals(DBNull.Value) == true ? "" : dr["SharingFeeCalculationDesc"].ToString();
            M_Fund.DividenDate = dr["DividenDate"].ToString();
            M_Fund.NPWP = dr["NPWP"].ToString();
            M_Fund.OJKLetter = dr["OJKLetter"].ToString();
            M_Fund.BitSinvestFee = dr["BitSinvestFee"].Equals(DBNull.Value) == true ? false : Convert.ToBoolean(dr["BitSinvestFee"]);

            M_Fund.BitInternalClosePrice = dr["BitInternalClosePrice"].Equals(DBNull.Value) == true ? false : Convert.ToBoolean(dr["BitInternalClosePrice"]);
            M_Fund.BitInvestmentHighRisk = dr["BitInvestmentHighRisk"].Equals(DBNull.Value) == true ? false : Convert.ToBoolean(dr["BitInvestmentHighRisk"]);

            M_Fund.KPDNoContract = dr["KPDNoContract"].Equals(DBNull.Value) == true ? "" : dr["KPDNoContract"].ToString();
            M_Fund.KPDDateFromContract = dr["KPDDateFromContract"].Equals(DBNull.Value) == true ? "" : dr["KPDDateFromContract"].ToString();
            M_Fund.KPDDateToContract = dr["KPDDateToContract"].Equals(DBNull.Value) == true ? "" : dr["KPDDateToContract"].ToString();
            M_Fund.KPDNoAdendum = dr["KPDNoAdendum"].Equals(DBNull.Value) == true ? "" : dr["KPDNoAdendum"].ToString();
            M_Fund.KPDDateAdendum = dr["KPDDateAdendum"].Equals(DBNull.Value) == true ? "" : dr["KPDDateAdendum"].ToString();
            M_Fund.CutOffTime = dr["CutOffTime"].Equals(DBNull.Value) == true ? "" : dr["CutOffTime"].ToString();

            M_Fund.ProspectusUrl = dr["ProspectusUrl"].Equals(DBNull.Value) == true ? "" : dr["ProspectusUrl"].ToString();
            M_Fund.FactsheetUrl = dr["FactsheetUrl"].Equals(DBNull.Value) == true ? "" : dr["FactsheetUrl"].ToString();
            M_Fund.ImageUrl = dr["ImageUrl"].Equals(DBNull.Value) == true ? "" : dr["ImageUrl"].ToString();
            M_Fund.ISIN = dr["ISIN"].Equals(DBNull.Value) == true ? "" : dr["ISIN"].ToString();

            M_Fund.EntryApproveTimeCutoff = dr["EntryApproveTimeCutoff"].Equals(DBNull.Value) == true ? "" : dr["EntryApproveTimeCutoff"].ToString();
            if (_host.CheckColumnIsExist(dr, "OJKEffectiveStatementLetterDate"))
            {
                M_Fund.OJKEffectiveStatementLetterDate = dr["OJKEffectiveStatementLetterDate"].ToString();
            }
            if (_host.CheckColumnIsExist(dr, "MinBalSwitchToAmt"))
            {
                M_Fund.MinBalSwitchToAmt = dr["MinBalSwitchToAmt"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["MinBalSwitchToAmt"]); ;
            }

            M_Fund.EntryUsersID = dr["EntryUsersID"].ToString();
            M_Fund.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_Fund.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_Fund.VoidUsersID = dr["VoidUsersID"].ToString();
            M_Fund.EntryTime = dr["EntryTime"].ToString();
            M_Fund.UpdateTime = dr["UpdateTime"].ToString();
            M_Fund.ApprovedTime = dr["ApprovedTime"].ToString();
            M_Fund.VoidTime = dr["VoidTime"].ToString();
            M_Fund.DBUserID = dr["DBUserID"].ToString();
            M_Fund.DBTerminalID = dr["DBTerminalID"].ToString();
            M_Fund.LastUpdate = dr["LastUpdate"].ToString();
            M_Fund.LastUpdateDB = Convert.ToString(dr["LastUpdateDB"]);

            M_Fund.BitSyariahFund = dr["BitSyariahFund"].Equals(DBNull.Value) == true ? false : Convert.ToBoolean(dr["BitSyariahFund"]);
            return M_Fund;
        }

        public List<Fund> Fund_Select(int _status)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<Fund> L_Fund = new List<Fund>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = @"Select case when Fund.status=1 then 'PENDING' else Case When Fund.status = 2 then 'APPROVED' else Case when Fund.Status = 3 
                                then 'VOID' else 'WAITING' END END END StatusDesc,C.ID CurrencyID,BC.ID BankBranchID,CO.Name CounterpartName,
                                MV1.DescOne NavRoundingModeDesc, MV2.DescOne NavDecimalPlacesDesc,MV3.DescOne UnitDecimalPlacesDesc,
                                Mv4.DescOne TypeDesc,MV5.DescOne UnitRoundingModeDesc,MV6.DescOne FundTypeInternalDesc,case when WHTDueDate = 1 then 'ValueDate' else 'SettledDate' end WHTDueDatedesc, 
                                case when Fund.MFeeMethod = 1 then 'Mark to Market' else case when Fund.MFeeMethod = 2 then 'NAV 1000' else '' end end MFeeMethodDesc,MV7.DescOne SharingFeeCalculationDesc,
                                KPDNoContract,KPDDateFromContract,KPDDateToContract,KPDNoAdendum,KPDDateAdendum,
                                Fund.* from Fund left join  
                                Currency C on Fund.CurrencyPK = C.CurrencyPK and C.status = 2  left join  
                                BankBranch BC on Fund.BankBranchPK = BC.BankBranchPK and BC.status = 2 Left join  
                                MasterValue MV1 on Fund.NavRoundingMode = MV1.Code and MV1.ID ='RoundingMode' and MV1.status = 2 left join  
                                MasterValue MV2 on Fund.NavDecimalPlaces = MV2.Code and MV2.ID ='DecimalPlaces' and MV2.status = 2 left join  
                                MasterValue MV3 on Fund.UnitDecimalPlaces = MV3.Code and MV3.ID ='DecimalPlaces' and MV3.status = 2 left join  
                                MasterValue MV4 on Fund.Type = MV4.Code and MV4.ID ='FundType' and MV4.status = 2 left join  
                                MasterValue MV5 on Fund.UnitRoundingMode = MV5.Code and MV5.ID ='RoundingMode' and MV5.status = 2   left join
                                MasterValue MV6 on Fund.FundTypeInternal = MV6.Code and MV6.ID ='FundTypeInternal' and MV6.status = 2    left join 
                                MasterValue MV7 on Fund.SharingFeeCalculation = MV7.Code and MV7.ID ='SharingFeeCalculation' and MV7.status = 2    left join 
                                Counterpart CO on Fund.CounterpartPK = CO.CounterpartPK and CO.Status = 2
                                where Fund.status = @status ";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = @"
                                Select case when Fund.status=1 then 'PENDING' else Case When Fund.status = 2 then 'APPROVED' else Case when Fund.Status = 3 
                                then 'VOID' else 'WAITING' END END END StatusDesc,C.ID CurrencyID,BC.ID BankBranchID,CO.Name CounterpartName,
                                MV1.DescOne NavRoundingModeDesc, MV2.DescOne NavDecimalPlacesDesc,MV3.DescOne UnitDecimalPlacesDesc,
                                Mv4.DescOne TypeDesc,MV5.DescOne UnitRoundingModeDesc,MV6.DescOne FundTypeInternalDesc,case when WHTDueDate = 1 then 'ValueDate' else 'SettledDate' end WHTDueDatedesc, 
                                case when Fund.MFeeMethod = 1 then 'Mark to Market' else case when Fund.MFeeMethod = 2 then 'NAV 1000'  else '' end end MFeeMethodDesc,
                                MV7.DescOne SharingFeeCalculationDesc,
                                KPDNoContract,KPDDateFromContract,KPDDateToContract,KPDNoAdendum,KPDDateAdendum,
                                Fund.* from Fund left join  
                                Currency C on Fund.CurrencyPK = C.CurrencyPK and C.status = 2  left join  
                                BankBranch BC on Fund.BankBranchPK = BC.BankBranchPK and BC.status = 2 Left join  
                                MasterValue MV1 on Fund.NavRoundingMode = MV1.Code and MV1.ID ='RoundingMode' and MV1.status = 2 left join  
                                MasterValue MV2 on Fund.NavDecimalPlaces = MV2.Code and MV2.ID ='DecimalPlaces' and MV2.status = 2 left join  
                                MasterValue MV3 on Fund.UnitDecimalPlaces = MV3.Code and MV3.ID ='DecimalPlaces' and MV3.status = 2 left join  
                                MasterValue MV4 on Fund.Type = MV4.Code and MV4.ID ='FundType' and MV4.status = 2 left join  
                                MasterValue MV5 on Fund.UnitRoundingMode = MV5.Code and MV5.ID ='RoundingMode' and MV5.status = 2   left join
                                MasterValue MV6 on Fund.FundTypeInternal = MV6.Code and MV6.ID ='FundTypeInternal' and MV6.status = 2    left join 
                                MasterValue MV7 on Fund.SharingFeeCalculation = MV7.Code and MV7.ID ='SharingFeeCalculation' and MV7.status = 2    left join 
                                Counterpart CO on Fund.CounterpartPK = CO.CounterpartPK and CO.Status = 2
                                order by ID,Name ";
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_Fund.Add(setFund(dr));
                                }
                            }
                            return L_Fund;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int Fund_Add(Fund _fund, bool _havePrivillege)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_havePrivillege)
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[ApprovedUsersID],[ApprovedTime],lastupdate)" +
                                 "Select isnull(max(FundPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@lastupdate from Fund";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _fund.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],lastupdate)" +
                                "Select isnull(max(FundPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@lastupdate from Fund";
                        }
                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@ID", _fund.ID);
                        cmd.Parameters.AddWithValue("@Name", _fund.Name);
                        cmd.Parameters.AddWithValue("@CurrencyPK", _fund.CurrencyPK);
                        cmd.Parameters.AddWithValue("@Type", _fund.Type);
                        cmd.Parameters.AddWithValue("@BankBranchPK", _fund.BankBranchPK);
                        cmd.Parameters.AddWithValue("@MaxUnits", _fund.MaxUnits);
                        cmd.Parameters.AddWithValue("@TotalUnits", _fund.TotalUnits);
                        cmd.Parameters.AddWithValue("@NAV", _fund.Nav);
                        cmd.Parameters.AddWithValue("@EffectiveDate", _fund.EffectiveDate);
                        cmd.Parameters.AddWithValue("@MaturityDate", _fund.MaturityDate);
                        cmd.Parameters.AddWithValue("@CustodyFeePercent", _fund.CustodyFeePercent);
                        cmd.Parameters.AddWithValue("@ManagementFeePercent", _fund.ManagementFeePercent);
                        cmd.Parameters.AddWithValue("@SubscriptionFeePercent", _fund.SubscriptionFeePercent);
                        cmd.Parameters.AddWithValue("@RedemptionFeePercent", _fund.RedemptionFeePercent);
                        cmd.Parameters.AddWithValue("@SwitchingFeePercent", _fund.SwitchingFeePercent);
                        cmd.Parameters.AddWithValue("@NavRoundingMode", _fund.NavRoundingMode);
                        cmd.Parameters.AddWithValue("@NavDecimalPlaces", _fund.NavDecimalPlaces);
                        cmd.Parameters.AddWithValue("@UnitDecimalPlaces", _fund.UnitDecimalPlaces);
                        cmd.Parameters.AddWithValue("@UnitRoundingMode", _fund.UnitRoundingMode);
                        cmd.Parameters.AddWithValue("@NKPDName", _fund.NKPDName);
                        cmd.Parameters.AddWithValue("@SInvestCode", _fund.SInvestCode);
                        cmd.Parameters.AddWithValue("@BloombergCode", _fund.BloombergCode);
                        cmd.Parameters.AddWithValue("@FundTypeInternal", _fund.FundTypeInternal);
                        cmd.Parameters.AddWithValue("@MinSwitch", _fund.MinSwitch);
                        cmd.Parameters.AddWithValue("@MinBalSwitchAmt", _fund.MinBalSwitchAmt);
                        cmd.Parameters.AddWithValue("@MinBalSwitchUnit", _fund.MinBalSwitchUnit);

                        cmd.Parameters.AddWithValue("@MinSubs", _fund.MinSubs);
                        cmd.Parameters.AddWithValue("@MinReds", _fund.MinReds);
                        cmd.Parameters.AddWithValue("@MinBalRedsAmt", _fund.MinBalRedsAmt);
                        cmd.Parameters.AddWithValue("@MinBalRedsUnit", _fund.MinBalRedsUnit);
                        cmd.Parameters.AddWithValue("@RemainingBalanceUnit", _fund.RemainingBalanceUnit);
                        cmd.Parameters.AddWithValue("@CounterpartPK", _fund.CounterpartPK);
                        cmd.Parameters.AddWithValue("@IsPublic", _fund.IsPublic);
                        cmd.Parameters.AddWithValue("@WHTDueDate", _fund.WHTDueDate);
                        cmd.Parameters.AddWithValue("@IssueDate", _fund.IssueDate);
                        cmd.Parameters.AddWithValue("@MFeeMethod", _fund.MFeeMethod);
                        cmd.Parameters.AddWithValue("@SharingFeeCalculation", _fund.SharingFeeCalculation);
                        cmd.Parameters.AddWithValue("@BitNeedRecon", _fund.BitNeedRecon);
                        cmd.Parameters.AddWithValue("@DefaultPaymentDate", _fund.DefaultPaymentDate);
                        cmd.Parameters.AddWithValue("@DividenDate", _fund.DividenDate);
                        cmd.Parameters.AddWithValue("@NPWP", _fund.NPWP);
                        cmd.Parameters.AddWithValue("@OJKLetter", _fund.OJKLetter);
                        cmd.Parameters.AddWithValue("@BitSinvestFee", _fund.BitSinvestFee);

                        cmd.Parameters.AddWithValue("@BitInternalClosePrice", _fund.BitInternalClosePrice);
                        cmd.Parameters.AddWithValue("@BitInvestmentHighRisk", _fund.BitInvestmentHighRisk);

                        cmd.Parameters.AddWithValue("@KPDNoContract", _fund.KPDNoContract);
                        cmd.Parameters.AddWithValue("@KPDDateFromContract", _fund.KPDDateFromContract);
                        cmd.Parameters.AddWithValue("@KPDDateToContract", _fund.KPDDateToContract);
                        cmd.Parameters.AddWithValue("@KPDNoAdendum", _fund.KPDNoAdendum);
                        cmd.Parameters.AddWithValue("@KPDDateAdendum", _fund.KPDDateAdendum);
                        if (_fund.CutOffTime == "" || _fund.CutOffTime == null)
                        {
                            cmd.Parameters.AddWithValue("@CutOffTime", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@CutOffTime", _fund.CutOffTime);
                        }

                        cmd.Parameters.AddWithValue("@ProspectusUrl", _fund.ProspectusUrl);
                        cmd.Parameters.AddWithValue("@FactsheetUrl", _fund.FactsheetUrl);
                        cmd.Parameters.AddWithValue("@ImageUrl", _fund.ImageUrl);
                        cmd.Parameters.AddWithValue("@ISIN", _fund.ISIN);
                        cmd.Parameters.AddWithValue("@EntryApproveTimeCutoff", _fund.EntryApproveTimeCutoff);
                        if (_fund.OJKEffectiveStatementLetterDate == "" || _fund.OJKEffectiveStatementLetterDate == null)
                        {
                            cmd.Parameters.AddWithValue("@OJKEffectiveStatementLetterDate", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@OJKEffectiveStatementLetterDate", _fund.OJKEffectiveStatementLetterDate);
                        }

                        if (_fund.MinBalSwitchToAmt == 0 || _fund.MinBalSwitchToAmt == null)
                        {
                            cmd.Parameters.AddWithValue("@MinBalSwitchToAmt", 0);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@MinBalSwitchToAmt", _fund.MinBalSwitchToAmt);
                        }

                        cmd.Parameters.AddWithValue("@EntryUsersID", _fund.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.Parameters.AddWithValue("@BitSyariahFund", _fund.BitSyariahFund);
                        cmd.ExecuteNonQuery();

                        return _host.Get_LastPKByLastUpate(_datetimeNow, "Fund");
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }


        public int Fund_Update(Fund _fund, bool _havePrivillege)
        {

            try
            {
                int _newHisPK;
                int status = _host.Get_Status(_fund.FundPK, _fund.HistoryPK, "Fund");
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = @"Update Fund set status=2,Notes=@Notes,ID=@ID,Name=@Name,CurrencyPK=@CurrencyPK,Type=@Type,BankBranchPK=@BankBranchPK,FundTypeInternal=@FundTypeInternal,MFeeMethod=@MFeeMethod,
                                MaxUnits=@MaxUnits,TotalUnits=@TotalUnits,Nav=@Nav,EffectiveDate=@EffectiveDate,MaturityDate=@MaturityDate,CustodyFeePercent=@CustodyFeePercent,ManagementFeePercent=@ManagementFeePercent,SubscriptionFeePercent=@SubscriptionFeePercent,RedemptionFeePercent=@RedemptionFeePercent,SharingFeeCalculation=@SharingFeeCalculation,
                                SwitchingFeePercent=@SwitchingFeePercent,NavRoundingMode=@NavRoundingMode,NavDecimalPlaces=@NavDecimalPlaces,UnitDecimalPlaces=@UnitDecimalPlaces,
                                UnitRoundingMode=@UnitRoundingMode,NKPDName=@NKPDName,SInvestCode=@SInvestCode,BloombergCode=@BloombergCode,MinSwitch=@MinSwitch,MinBalSwitchAmt=@MinBalSwitchAmt,MinBalSwitchUnit=@MinBalSwitchUnit,MinSubs=@MinSubs,MinReds=@MinReds,MinBalRedsAmt=@MinBalRedsAmt,MinBalRedsUnit=@MinBalRedsUnit,RemainingBalanceUnit=@RemainingBalanceUnit,
                                CounterpartPK=@CounterpartPK,IsPublic=@IsPublic,WHTDueDate=@WHTDueDate,IssueDate=@IssueDate,BitNeedRecon=@BitNeedRecon,DefaultPaymentDate=@DefaultPaymentDate,DividenDate=@DividenDate,NPWP=@NPWP,OJKLetter=@OJKLetter,BitSinvestFee=@BitSinvestFee,BitInternalClosePrice=@BitInternalClosePrice,BitInvestmentHighRisk=@BitInvestmentHighRisk,
                                KPDNoContract=@KPDNoContract,KPDDateFromContract=@KPDDateFromContract,KPDDateToContract=@KPDDateToContract,KPDNoAdendum=@KPDNoAdendum,KPDDateAdendum=@KPDDateAdendum,CutOffTime=@CutOffTime,ProspectusUrl=@ProspectusUrl,FactsheetUrl=@FactsheetUrl,ImageUrl=@ImageUrl,ISIN=@ISIN,EntryApproveTimeCutoff=@EntryApproveTimeCutoff,OJKEffectiveStatementLetterDate=@OJKEffectiveStatementLetterDate,BitSyariahFund=@BitSyariahFund,
                                ApprovedUsersID=@ApprovedUsersID,ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime ,lastupdate=@lastupdate 
                                where fundPK = @PK and  historyPK = @HistoryPK";

                            cmd.Parameters.AddWithValue("@HistoryPK", _fund.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _fund.FundPK);
                            cmd.Parameters.AddWithValue("@Notes", _fund.Notes);
                            cmd.Parameters.AddWithValue("@ID", _fund.ID);
                            cmd.Parameters.AddWithValue("@Name", _fund.Name);
                            cmd.Parameters.AddWithValue("@CurrencyPK", _fund.CurrencyPK);
                            cmd.Parameters.AddWithValue("@Type", _fund.Type);
                            cmd.Parameters.AddWithValue("@BankBranchPK", _fund.BankBranchPK);
                            cmd.Parameters.AddWithValue("@MaxUnits", _fund.MaxUnits);
                            cmd.Parameters.AddWithValue("@TotalUnits", _fund.TotalUnits);
                            cmd.Parameters.AddWithValue("@Nav", _fund.Nav);
                            cmd.Parameters.AddWithValue("@EffectiveDate", _fund.EffectiveDate);
                            cmd.Parameters.AddWithValue("@MaturityDate", _fund.MaturityDate);
                            cmd.Parameters.AddWithValue("@CustodyFeePercent", _fund.CustodyFeePercent);
                            cmd.Parameters.AddWithValue("@ManagementFeePercent", _fund.ManagementFeePercent);
                            cmd.Parameters.AddWithValue("@SubscriptionFeePercent", _fund.SubscriptionFeePercent);
                            cmd.Parameters.AddWithValue("@RedemptionFeePercent", _fund.RedemptionFeePercent);
                            cmd.Parameters.AddWithValue("@SwitchingFeePercent", _fund.SwitchingFeePercent);
                            cmd.Parameters.AddWithValue("@NavRoundingMode", _fund.NavRoundingMode);
                            cmd.Parameters.AddWithValue("@NavDecimalPlaces", _fund.NavDecimalPlaces);
                            cmd.Parameters.AddWithValue("@UnitDecimalPlaces", _fund.UnitDecimalPlaces);
                            cmd.Parameters.AddWithValue("@UnitRoundingMode", _fund.UnitRoundingMode);
                            cmd.Parameters.AddWithValue("@NKPDName", _fund.NKPDName);
                            cmd.Parameters.AddWithValue("@SInvestCode", _fund.SInvestCode);
                            cmd.Parameters.AddWithValue("@BloombergCode", _fund.BloombergCode);
                            cmd.Parameters.AddWithValue("@FundTypeInternal", _fund.FundTypeInternal);
                            cmd.Parameters.AddWithValue("@MinSwitch", _fund.MinSwitch);
                            cmd.Parameters.AddWithValue("@MinBalSwitchAmt", _fund.MinBalSwitchAmt);
                            cmd.Parameters.AddWithValue("@MinBalSwitchUnit", _fund.MinBalSwitchUnit);
                            cmd.Parameters.AddWithValue("@MinSubs", _fund.MinSubs);
                            cmd.Parameters.AddWithValue("@MinReds", _fund.MinReds);
                            cmd.Parameters.AddWithValue("@MinBalRedsAmt", _fund.MinBalRedsAmt);
                            cmd.Parameters.AddWithValue("@MinBalRedsUnit", _fund.MinBalRedsUnit);
                            cmd.Parameters.AddWithValue("@CounterpartPK", _fund.CounterpartPK);
                            cmd.Parameters.AddWithValue("@IsPublic", _fund.IsPublic);
                            cmd.Parameters.AddWithValue("@WHTDueDate", _fund.WHTDueDate);
                            cmd.Parameters.AddWithValue("@IssueDate", _fund.IssueDate);
                            cmd.Parameters.AddWithValue("@MFeeMethod", _fund.MFeeMethod);
                            cmd.Parameters.AddWithValue("@RemainingBalanceUnit", _fund.RemainingBalanceUnit);
                            cmd.Parameters.AddWithValue("@BitNeedRecon", _fund.BitNeedRecon);
                            cmd.Parameters.AddWithValue("@DefaultPaymentDate", _fund.DefaultPaymentDate);
                            cmd.Parameters.AddWithValue("@SharingFeeCalculation", _fund.SharingFeeCalculation);
                            cmd.Parameters.AddWithValue("@DividenDate", _fund.DividenDate);
                            cmd.Parameters.AddWithValue("@NPWP", _fund.NPWP);
                            cmd.Parameters.AddWithValue("@OJKLetter", _fund.OJKLetter);
                            cmd.Parameters.AddWithValue("@BitSinvestFee", _fund.BitSinvestFee);
                            cmd.Parameters.AddWithValue("@BitInternalClosePrice", _fund.BitInternalClosePrice);
                            cmd.Parameters.AddWithValue("@BitInvestmentHighRisk", _fund.BitInvestmentHighRisk);
                            cmd.Parameters.AddWithValue("@KPDNoContract", _fund.KPDNoContract);
                            cmd.Parameters.AddWithValue("@KPDDateFromContract", _fund.KPDDateFromContract);
                            cmd.Parameters.AddWithValue("@KPDDateToContract", _fund.KPDDateToContract);
                            cmd.Parameters.AddWithValue("@KPDNoAdendum", _fund.KPDNoAdendum);
                            cmd.Parameters.AddWithValue("@KPDDateAdendum", _fund.KPDDateAdendum);
                            if (_fund.CutOffTime == "" || _fund.CutOffTime == null)
                            {
                                cmd.Parameters.AddWithValue("@CutOffTime", DBNull.Value);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@CutOffTime", _fund.CutOffTime);
                            }

                            cmd.Parameters.AddWithValue("@ProspectusUrl", _fund.ProspectusUrl);
                            cmd.Parameters.AddWithValue("@FactsheetUrl", _fund.FactsheetUrl);
                            cmd.Parameters.AddWithValue("@ImageUrl", _fund.ImageUrl);
                            cmd.Parameters.AddWithValue("@ISIN", _fund.ISIN);
                            cmd.Parameters.AddWithValue("@EntryApproveTimeCutoff", _fund.EntryApproveTimeCutoff);
                            if (_fund.OJKEffectiveStatementLetterDate == "" || _fund.OJKEffectiveStatementLetterDate == null)
                            {
                                cmd.Parameters.AddWithValue("@OJKEffectiveStatementLetterDate", DBNull.Value);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@OJKEffectiveStatementLetterDate", _fund.OJKEffectiveStatementLetterDate);
                            }
                            if (_fund.MinBalSwitchToAmt == 0 || _fund.MinBalSwitchToAmt == null)
                            {
                                cmd.Parameters.AddWithValue("@MinBalSwitchToAmt", 0);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@MinBalSwitchToAmt", _fund.MinBalSwitchToAmt);
                            }
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _fund.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _fund.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                            cmd.Parameters.AddWithValue("@BitSyariahFund", _fund.BitSyariahFund);
                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = @"Update Fund set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime ,lastupdate=@lastupdate where FundPK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _fund.FundPK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _fund.EntryUsersID);
                            cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
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
                                cmd.CommandText = @"Update Fund set Notes=@Notes,ID=@ID,Name=@Name,CurrencyPK=@CurrencyPK,Type=@Type,BankBranchPK=@BankBranchPK,MFeeMethod=@MFeeMethod,
                                    MaxUnits=@MaxUnits,TotalUnits=@TotalUnits,Nav=@Nav,EffectiveDate=@EffectiveDate,MaturityDate=@MaturityDate,CustodyFeePercent=@CustodyFeePercent,ManagementFeePercent=@ManagementFeePercent,SubscriptionFeePercent=@SubscriptionFeePercent,
                                    RedemptionFeePercent=@RedemptionFeePercent,SwitchingFeePercent=@SwitchingFeePercent,NavRoundingMode=@NavRoundingMode,NavDecimalPlaces=@NavDecimalPlaces,UnitDecimalPlaces=@UnitDecimalPlaces,UnitRoundingMode=@UnitRoundingMode,NKPDName=@NKPDName,
                                    SInvestCode=@SInvestCode,BloombergCode=@BloombergCode,MinSwitch=@MinSwitch,MinBalSwitchAmt=@MinBalSwitchAmt,MinBalSwitchUnit=@MinBalSwitchUnit,MinSubs=@MinSubs,MinReds=@MinReds,MinBalRedsAmt=@MinBalRedsAmt,MinBalRedsUnit=@MinBalRedsUnit,RemainingBalanceUnit=@RemainingBalanceUnit,SharingFeeCalculation=@SharingFeeCalculation,
                                    CounterpartPK=@CounterpartPK,IsPublic=@IsPublic,WHTDueDate=@WHTDueDate,IssueDate=@IssueDate,BitNeedRecon=@BitNeedRecon,DefaultPaymentDate=@DefaultPaymentDate,DividenDate=@DividenDate,NPWP=@NPWP,OJKLetter=@OJKLetter,BitSinvestFee=@BitSinvestFee,BitInternalClosePrice=@BitInternalClosePrice,BitInvestmentHighRisk=@BitInvestmentHighRisk,
                                    KPDNoContract=@KPDNoContract,KPDDateFromContract=@KPDDateFromContract,KPDDateToContract=@KPDDateToContract,KPDNoAdendum=@KPDNoAdendum,KPDDateAdendum=@KPDDateAdendum,CutOffTime=@CutOffTime,ProspectusUrl=@ProspectusUrl,FactsheetUrl=@FactsheetUrl,ImageUrl=@ImageUrl,ISIN=@ISIN,EntryApproveTimeCutoff=@EntryApproveTimeCutoff,OJKEffectiveStatementLetterDate=@OJKEffectiveStatementLetterDate,BitSyariahFund=@BitSyariahFund,
                                    ApprovedUsersID=@ApprovedUsersID,ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime ,lastupdate=@lastupdate
                                    where fundPK = @PK and  historyPK = @HistoryPK";

                                cmd.Parameters.AddWithValue("@HistoryPK", _fund.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _fund.FundPK);
                                cmd.Parameters.AddWithValue("@Notes", _fund.Notes);
                                cmd.Parameters.AddWithValue("@ID", _fund.ID);
                                cmd.Parameters.AddWithValue("@Name", _fund.Name);
                                cmd.Parameters.AddWithValue("@CurrencyPK", _fund.CurrencyPK);
                                cmd.Parameters.AddWithValue("@Type", _fund.Type);
                                cmd.Parameters.AddWithValue("@BankBranchPK", _fund.BankBranchPK);
                                cmd.Parameters.AddWithValue("@MaxUnits", _fund.MaxUnits);
                                cmd.Parameters.AddWithValue("@TotalUnits", _fund.TotalUnits);
                                cmd.Parameters.AddWithValue("@Nav", _fund.Nav);
                                cmd.Parameters.AddWithValue("@EffectiveDate", _fund.EffectiveDate);
                                cmd.Parameters.AddWithValue("@MaturityDate", _fund.MaturityDate);
                                cmd.Parameters.AddWithValue("@CustodyFeePercent", _fund.CustodyFeePercent);
                                cmd.Parameters.AddWithValue("@ManagementFeePercent", _fund.ManagementFeePercent);
                                cmd.Parameters.AddWithValue("@SubscriptionFeePercent", _fund.SubscriptionFeePercent);
                                cmd.Parameters.AddWithValue("@RedemptionFeePercent", _fund.RedemptionFeePercent);
                                cmd.Parameters.AddWithValue("@SwitchingFeePercent", _fund.SwitchingFeePercent);
                                cmd.Parameters.AddWithValue("@NavRoundingMode", _fund.NavRoundingMode);
                                cmd.Parameters.AddWithValue("@NavDecimalPlaces", _fund.NavDecimalPlaces);
                                cmd.Parameters.AddWithValue("@UnitDecimalPlaces", _fund.UnitDecimalPlaces);
                                cmd.Parameters.AddWithValue("@UnitRoundingMode", _fund.UnitRoundingMode);
                                cmd.Parameters.AddWithValue("@NKPDName", _fund.NKPDName);
                                cmd.Parameters.AddWithValue("@SInvestCode", _fund.SInvestCode);
                                cmd.Parameters.AddWithValue("@BloombergCode", _fund.BloombergCode);
                                cmd.Parameters.AddWithValue("@FundTypeInternal", _fund.FundTypeInternal);
                                cmd.Parameters.AddWithValue("@MinSwitch", _fund.MinSwitch);
                                cmd.Parameters.AddWithValue("@MinBalSwitchAmt", _fund.MinBalSwitchAmt);
                                cmd.Parameters.AddWithValue("@MinBalSwitchUnit", _fund.MinBalSwitchUnit);
                                cmd.Parameters.AddWithValue("@MinSubs", _fund.MinSubs);
                                cmd.Parameters.AddWithValue("@MinReds", _fund.MinReds);
                                cmd.Parameters.AddWithValue("@MinBalRedsAmt", _fund.MinBalRedsAmt);
                                cmd.Parameters.AddWithValue("@MinBalRedsUnit", _fund.MinBalRedsUnit);
                                cmd.Parameters.AddWithValue("@CounterpartPK", _fund.CounterpartPK);
                                cmd.Parameters.AddWithValue("@IsPublic", _fund.IsPublic);
                                cmd.Parameters.AddWithValue("@WHTDueDate", _fund.WHTDueDate);
                                cmd.Parameters.AddWithValue("@IssueDate", _fund.IssueDate);
                                cmd.Parameters.AddWithValue("@MFeeMethod", _fund.MFeeMethod);
                                cmd.Parameters.AddWithValue("@RemainingBalanceUnit", _fund.RemainingBalanceUnit);
                                cmd.Parameters.AddWithValue("@BitNeedRecon", _fund.BitNeedRecon);
                                cmd.Parameters.AddWithValue("@DefaultPaymentDate", _fund.DefaultPaymentDate);
                                cmd.Parameters.AddWithValue("@SharingFeeCalculation", _fund.SharingFeeCalculation);
                                cmd.Parameters.AddWithValue("@DividenDate", _fund.DividenDate);
                                cmd.Parameters.AddWithValue("@NPWP", _fund.NPWP);
                                cmd.Parameters.AddWithValue("@OJKLetter", _fund.OJKLetter);
                                cmd.Parameters.AddWithValue("@BitSinvestFee", _fund.BitSinvestFee);
                                cmd.Parameters.AddWithValue("@BitInternalClosePrice", _fund.BitInternalClosePrice);
                                cmd.Parameters.AddWithValue("@BitInvestmentHighRisk", _fund.BitInvestmentHighRisk);

                                cmd.Parameters.AddWithValue("@KPDNoContract", _fund.KPDNoContract);
                                cmd.Parameters.AddWithValue("@KPDDateFromContract", _fund.KPDDateFromContract);
                                cmd.Parameters.AddWithValue("@KPDDateToContract", _fund.KPDDateToContract);
                                cmd.Parameters.AddWithValue("@KPDNoAdendum", _fund.KPDNoAdendum);
                                cmd.Parameters.AddWithValue("@KPDDateAdendum", _fund.KPDDateAdendum);
                                if (_fund.CutOffTime == "" || _fund.CutOffTime == null)
                                {
                                    cmd.Parameters.AddWithValue("@CutOffTime", DBNull.Value);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@CutOffTime", _fund.CutOffTime);
                                }

                                cmd.Parameters.AddWithValue("@ProspectusUrl", _fund.ProspectusUrl);
                                cmd.Parameters.AddWithValue("@FactsheetUrl", _fund.FactsheetUrl);
                                cmd.Parameters.AddWithValue("@ImageUrl", _fund.ImageUrl);
                                cmd.Parameters.AddWithValue("@ISIN", _fund.ISIN);
                                cmd.Parameters.AddWithValue("@EntryApproveTimeCutoff", _fund.EntryApproveTimeCutoff);
                                if (_fund.OJKEffectiveStatementLetterDate == "" || _fund.OJKEffectiveStatementLetterDate == null)
                                {
                                    cmd.Parameters.AddWithValue("@OJKEffectiveStatementLetterDate", DBNull.Value);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@OJKEffectiveStatementLetterDate", _fund.OJKEffectiveStatementLetterDate);
                                }
                                if (_fund.MinBalSwitchToAmt == 0 || _fund.MinBalSwitchToAmt == null)
                                {
                                    cmd.Parameters.AddWithValue("@MinBalSwitchToAmt", 0);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@MinBalSwitchToAmt", _fund.MinBalSwitchToAmt);
                                }
                                cmd.Parameters.AddWithValue("@ApprovedUsersID", _fund.EntryUsersID);
                                cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _fund.EntryUsersID);
                                cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                                cmd.Parameters.AddWithValue("@BitSyariahFund", _fund.BitSyariahFund);
                                cmd.ExecuteNonQuery();
                            }

                            return 0;
                        }
                        else
                        {
                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                _newHisPK = _host.Get_NewHistoryPK(_fund.FundPK, "Fund");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From Fund where FundPK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _fund.FundPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _fund.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@ID", _fund.ID);
                                cmd.Parameters.AddWithValue("@Name", _fund.Name);
                                cmd.Parameters.AddWithValue("@CurrencyPK", _fund.CurrencyPK);
                                cmd.Parameters.AddWithValue("@Type", _fund.Type);
                                cmd.Parameters.AddWithValue("@BankBranchPK", _fund.BankBranchPK);
                                cmd.Parameters.AddWithValue("@MaxUnits", _fund.MaxUnits);
                                cmd.Parameters.AddWithValue("@TotalUnits", _fund.TotalUnits);
                                cmd.Parameters.AddWithValue("@Nav", _fund.Nav);
                                cmd.Parameters.AddWithValue("@EffectiveDate", _fund.EffectiveDate);
                                cmd.Parameters.AddWithValue("@MaturityDate", _fund.MaturityDate);
                                cmd.Parameters.AddWithValue("@CustodyFeePercent", _fund.CustodyFeePercent);
                                cmd.Parameters.AddWithValue("@ManagementFeePercent", _fund.ManagementFeePercent);
                                cmd.Parameters.AddWithValue("@SubscriptionFeePercent", _fund.SubscriptionFeePercent);
                                cmd.Parameters.AddWithValue("@RedemptionFeePercent", _fund.RedemptionFeePercent);
                                cmd.Parameters.AddWithValue("@SwitchingFeePercent", _fund.SwitchingFeePercent);
                                cmd.Parameters.AddWithValue("@NavRoundingMode", _fund.NavRoundingMode);
                                cmd.Parameters.AddWithValue("@NavDecimalPlaces", _fund.NavDecimalPlaces);
                                cmd.Parameters.AddWithValue("@UnitDecimalPlaces", _fund.UnitDecimalPlaces);
                                cmd.Parameters.AddWithValue("@UnitRoundingMode", _fund.UnitRoundingMode);
                                cmd.Parameters.AddWithValue("@NKPDName", _fund.NKPDName);
                                cmd.Parameters.AddWithValue("@SInvestCode", _fund.SInvestCode);
                                cmd.Parameters.AddWithValue("@BloombergCode", _fund.BloombergCode);
                                cmd.Parameters.AddWithValue("@FundTypeInternal", _fund.FundTypeInternal);
                                cmd.Parameters.AddWithValue("@MinSwitch", _fund.MinSwitch);
                                cmd.Parameters.AddWithValue("@MinBalSwitchAmt", _fund.MinBalSwitchAmt);
                                cmd.Parameters.AddWithValue("@MinBalSwitchUnit", _fund.MinBalSwitchUnit);
                                cmd.Parameters.AddWithValue("@MinSubs", _fund.MinSubs);
                                cmd.Parameters.AddWithValue("@MinReds", _fund.MinReds);
                                cmd.Parameters.AddWithValue("@MinBalRedsAmt", _fund.MinBalRedsAmt);
                                cmd.Parameters.AddWithValue("@MinBalRedsUnit", _fund.MinBalRedsUnit);
                                cmd.Parameters.AddWithValue("@CounterpartPK", _fund.CounterpartPK);
                                cmd.Parameters.AddWithValue("@IsPublic", _fund.IsPublic);
                                cmd.Parameters.AddWithValue("@WHTDueDate", _fund.WHTDueDate);
                                cmd.Parameters.AddWithValue("@IssueDate", _fund.IssueDate);
                                cmd.Parameters.AddWithValue("@MFeeMethod", _fund.MFeeMethod);
                                cmd.Parameters.AddWithValue("@RemainingBalanceUnit", _fund.RemainingBalanceUnit);
                                cmd.Parameters.AddWithValue("@BitNeedRecon", _fund.BitNeedRecon);
                                cmd.Parameters.AddWithValue("@DefaultPaymentDate", _fund.DefaultPaymentDate);
                                cmd.Parameters.AddWithValue("@SharingFeeCalculation", _fund.SharingFeeCalculation);
                                cmd.Parameters.AddWithValue("@DividenDate", _fund.DividenDate);
                                cmd.Parameters.AddWithValue("@NPWP", _fund.NPWP);
                                cmd.Parameters.AddWithValue("@OJKLetter", _fund.OJKLetter);
                                cmd.Parameters.AddWithValue("@BitSinvestFee", _fund.BitSinvestFee);
                                cmd.Parameters.AddWithValue("@BitInternalClosePrice", _fund.BitInternalClosePrice);
                                cmd.Parameters.AddWithValue("@BitInvestmentHighRisk", _fund.BitInvestmentHighRisk);
                                cmd.Parameters.AddWithValue("@KPDNoContract", _fund.KPDNoContract);
                                cmd.Parameters.AddWithValue("@KPDDateFromContract", _fund.KPDDateFromContract);
                                cmd.Parameters.AddWithValue("@KPDDateToContract", _fund.KPDDateToContract);
                                cmd.Parameters.AddWithValue("@KPDNoAdendum", _fund.KPDNoAdendum);
                                cmd.Parameters.AddWithValue("@KPDDateAdendum", _fund.KPDDateAdendum);
                                if (_fund.CutOffTime == "" || _fund.CutOffTime == null)
                                {
                                    cmd.Parameters.AddWithValue("@CutOffTime", DBNull.Value);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@CutOffTime", _fund.CutOffTime);
                                }

                                cmd.Parameters.AddWithValue("@ProspectusUrl", _fund.ProspectusUrl);
                                cmd.Parameters.AddWithValue("@FactsheetUrl", _fund.FactsheetUrl);
                                cmd.Parameters.AddWithValue("@ImageUrl", _fund.ImageUrl);
                                cmd.Parameters.AddWithValue("@ISIN", _fund.ISIN);
                                cmd.Parameters.AddWithValue("@EntryApproveTimeCutoff", _fund.EntryApproveTimeCutoff);
                                if (_fund.OJKEffectiveStatementLetterDate == "" || _fund.OJKEffectiveStatementLetterDate == null)
                                {
                                    cmd.Parameters.AddWithValue("@OJKEffectiveStatementLetterDate", DBNull.Value);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@OJKEffectiveStatementLetterDate", _fund.OJKEffectiveStatementLetterDate);
                                }
                                if (_fund.MinBalSwitchToAmt == 0 || _fund.MinBalSwitchToAmt == null)
                                {
                                    cmd.Parameters.AddWithValue("@MinBalSwitchToAmt", 0);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@MinBalSwitchToAmt", _fund.MinBalSwitchToAmt);
                                }
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _fund.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                                cmd.Parameters.AddWithValue("@BitSyariahFund", _fund.BitSyariahFund);
                                cmd.ExecuteNonQuery();
                            }


                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = @"Update Fund set status= 4,Notes=@Notes,
                                    lastupdate=@lastupdate where FundPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _fund.Notes);
                                cmd.Parameters.AddWithValue("@PK", _fund.FundPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _fund.HistoryPK);
                                cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
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


        public void Fund_Approved(Fund _fund)
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
                        IF EXISTS(select InstrumentPK from Instrument A
                        left join Fund B on A.ID = B.ID and B.status in (1,2)
                        where A.status in (1,2) and B.FundPK = @PK)
                        BEGIN                  
                            Update A set A.CurrencyPK = B.CurrencyPK,ReksadanaTypePK = case when  B.Type = 1 then 2 
	                        else case when B.Type in (2,5,9) then 1 
	                        else case when B.Type = 3 then 4 
	                        else case when B.Type = 4 then 7 
	                        else case when B.Type = 12 then 6 
	                        else case when B.Type = 11 then 8 
	                        else case when B.Type in (6,10) then 3 
	                        else  0 
	                        end end end end end end end,UpdateUsersID = @ApprovedUsersID,UpdateTime= @ApprovedTime,LastUpdate = @LastUpdate  
                            from Instrument A 
	                        left join Fund B on A.ID = B.ID and B.status in (1,2)
	                        where B.FundPK = @PK
                        END
                        ELSE
                        BEGIN
                        Declare @MaxInstrumentPK int
	                        select @MaxInstrumentPK = MAX(InstrumentPK) + 1 from Instrument
	                        set @MaxInstrumentPK = ISNULL(@MaxInstrumentPK,1)
	                        Insert into Instrument (InstrumentPK,HistoryPK,status, ID,Name,Category,Affiliated,InstrumentTypePK,ReksadanaTypePK,MarketPK,CurrencyPK,LotInShare,TaxExpensePercent,EntryUsersID,EntryTime,ApprovedUsersID,ApprovedTime,LastUpdate)
	                        select @MaxInstrumentPK,1,2,A.ID,A.Name,'',1,6,
	                        case when  A.Type = 1 then 2 
	                        else case when A.Type in (2,5,9) then 1 
	                        else case when A.Type = 3 then 4 
	                        else case when A.Type = 4 then 7 
	                        else case when A.Type = 12 then 6 
	                        else case when A.Type = 11 then 8 
	                        else case when A.Type in (6,10) then 3 
	                        else  0 
	                        end end end end end end end ReksadanaTypePK,
	                        1,CurrencyPK,100,0,@ApprovedUsersID,@ApprovedTime,@ApprovedUsersID,@ApprovedTime,@lastupdate From Fund A 
	                        left join MasterValue B on A.Type = B.Code and B.ID = 'FundType' and B.status = 2
	                        where FundPK = @PK and A.status = 1
                        END ";

                        cmd.Parameters.AddWithValue("@PK", _fund.FundPK);
                        cmd.Parameters.AddWithValue("@historyPK", _fund.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _fund.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }

                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"update Fund set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime ,lastupdate=@lastupdate 
                            where FundPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _fund.FundPK);
                        cmd.Parameters.AddWithValue("@historyPK", _fund.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _fund.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"Update Fund set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime ,lastupdate=@lastupdate where FundPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _fund.FundPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _fund.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }


                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public void Fund_Reject(Fund _fund)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"update Fund set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime ,lastupdate=@lastupdate 
                            where FundPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _fund.FundPK);
                        cmd.Parameters.AddWithValue("@historyPK", _fund.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _fund.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update Fund set status= 2,lastupdate=@lastupdate where FundPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _fund.FundPK);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }

                 
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public void Fund_Void(Fund _fund)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"Update A set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime, lastupdate=@lastupdate  from Instrument A 
                        left join Fund B on A.ID = B.ID and B.Status = 2
                        where A.InstrumentTypePK = 6 and A.status = 2 and B.FundPK = @PK and B.HistoryPK = @historyPK";

                        cmd.Parameters.AddWithValue("@PK", _fund.FundPK);
                        cmd.Parameters.AddWithValue("@historyPK", _fund.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _fund.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }

                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update Fund set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime, lastupdate=@lastupdate " +
                        "where FundPK = @PK and historypk = @historyPK";

                        cmd.Parameters.AddWithValue("@PK", _fund.FundPK);
                        cmd.Parameters.AddWithValue("@historyPK", _fund.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _fund.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }

                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"
                            update A set status = 3, VoidUsersID = @VoidUsersID, VoidTime = @VoidTime, LastUpdate = @lastupdate 
                            from  Instrument A  left join Fund B on A.ID = B.ID
                            where A.status = 2 and B.FundPK = @PK and B.HistoryPK = @HistoryPK ";

                        cmd.Parameters.AddWithValue("@PK", _fund.FundPK);
                        cmd.Parameters.AddWithValue("@historyPK", _fund.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _fund.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
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
        public List<FundCombo> Fund_Combo()
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<FundCombo> L_Fund = new List<FundCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "SELECT  FundPK,ID + ' - ' + Name as ID, Name FROM [Fund]  where status = 2 order by ID,Name";
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    FundCombo M_Fund = new FundCombo();
                                    M_Fund.FundPK = Convert.ToInt32(dr["FundPK"]);
                                    M_Fund.ID = Convert.ToString(dr["ID"]);
                                    M_Fund.Name = Convert.ToString(dr["Name"]);
                                    L_Fund.Add(M_Fund);
                                }

                            }
                            return L_Fund;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }


        }

        public List<FundCombo> Fund_ComboRpt()
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<FundCombo> L_Fund = new List<FundCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "SELECT  FundPK,ID + ' - ' + Name as ID, Name FROM [Fund]  where status = 2 union all select 0,'All', '' order by FundPK,Name";
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    FundCombo M_Fund = new FundCombo();
                                    M_Fund.FundPK = Convert.ToInt32(dr["FundPK"]);
                                    M_Fund.ID = Convert.ToString(dr["ID"]);
                                    M_Fund.Name = Convert.ToString(dr["Name"]);
                                    L_Fund.Add(M_Fund);
                                }

                            }
                            return L_Fund;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }


        }

        public decimal Fund_GetUnitPosition(int _fundPK, DateTime _date)
        {


            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();

                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = @" Select sum(isnull(UnitAmount,0)) UnitAmount from fundClientPosition where Date = (Select max(date) from fundclientposition where Date  <=  @Date and FundPK = @FundPK) 
                        and FundPK = @FundPK ";

                        cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                        cmd.Parameters.AddWithValue("@Date", _date);



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


        public FundLookup Fund_LookupByFundPK(int _fundPK)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {


                        cmd.CommandText = "Select * from Fund where status = 2 and FundPK = @FundPK";
                        cmd.Parameters.AddWithValue("@FundPK", _fundPK);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            { // 10 Field
                                dr.Read();
                                FundLookup M_Fund = new FundLookup();
                                M_Fund.FundPK = Convert.ToInt32(dr["FundPK"]);
                                M_Fund.ID = dr["ID"].ToString();
                                M_Fund.Name = dr["Name"].ToString();
                                M_Fund.CustodyFeePercent = Convert.ToDecimal(dr["CustodyFeePercent"]);
                                M_Fund.ManagementFeePercent = Convert.ToDecimal(dr["ManagementFeePercent"]);
                                M_Fund.SubscriptionFeePercent = Convert.ToDecimal(dr["SubscriptionFeePercent"]);
                                M_Fund.RedemptionFeePercent = Convert.ToDecimal(dr["RedemptionFeePercent"]);
                                M_Fund.SwitchingFeePercent = Convert.ToDecimal(dr["SwitchingFeePercent"]);
                                M_Fund.CurrencyPK = Convert.ToInt32(dr["CurrencyPK"]);
                                return M_Fund;

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

        public int Fund_GetDefaultPaymentDate(int _fundPK)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();

                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = @" Select isnull(DefaultPaymentDate,1) DefaultPaymentDate from Fund where FundPK = @FundPK and status in (1,2) ";

                        cmd.Parameters.AddWithValue("@FundPK", _fundPK);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return Convert.ToInt32(dr["DefaultPaymentDate"]);

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

        public string Get_NavDecimalPlaces(int _fundPK)
        {


            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();

                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = @" select B.DescOne DecimalPlaces from Fund A
                        left join MasterValue B on A.NavDecimalPlaces = B.Code and B.ID = 'DecimalPlaces' and B.Status = 2 
                        where A.FundPK = @FundPK and A.status = 2 ";

                        cmd.Parameters.AddWithValue("@FundPK", _fundPK);



                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return Convert.ToString(dr["DecimalPlaces"]);

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


        public List<FundCombo> Fund_ComboBitInternalClosePrice()
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<FundCombo> L_Fund = new List<FundCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "SELECT  FundPK,ID + ' - ' + Name as ID, Name FROM [Fund]  where status = 2 and BitInternalClosePrice = 1 order by ID,Name";
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    FundCombo M_Fund = new FundCombo();
                                    M_Fund.FundPK = Convert.ToInt32(dr["FundPK"]);
                                    M_Fund.ID = Convert.ToString(dr["ID"]);
                                    M_Fund.Name = Convert.ToString(dr["Name"]);
                                    L_Fund.Add(M_Fund);
                                }

                            }
                            return L_Fund;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }


        }

        public List<FundCombo> Fund_ComboBitNeedRecon()
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<FundCombo> L_Fund = new List<FundCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "SELECT  FundPK,ID + ' - ' + Name as ID, Name FROM [Fund]  where status = 2 and BitNeedRecon = 1 order by ID,Name";
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    FundCombo M_Fund = new FundCombo();
                                    M_Fund.FundPK = Convert.ToInt32(dr["FundPK"]);
                                    M_Fund.ID = Convert.ToString(dr["ID"]);
                                    M_Fund.Name = Convert.ToString(dr["Name"]);
                                    L_Fund.Add(M_Fund);
                                }

                            }
                            return L_Fund;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }


        }

        public bool Check_FundETF(int _fundPK)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    string date = System.DateTime.Today.ToString("MM/dd/yyyy");
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "select * from Fund where FundPK = @FundPK and Status in (1,2) and  Type = 10 ";
                        cmd.Parameters.AddWithValue("@FundPK", _fundPK);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                return true;
                            }
                            else
                            {
                                return false;
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

        public bool Check_EntryApproveTimeCutoff(int _fundPK)
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
                        Declare @StringTimeNow nvarchar(8)
                        Declare @CutoffTime decimal(22,0)
                        Declare @DecTimeNow decimal(22,0)

                        SELECT @StringTimeNow = REPLACE(substring(CONVERT(nvarchar(8),@TimeNow,108),0,9),':','')

                        select @CutoffTime = case when isnull(EntryApproveTimeCutoff,'') = '' then 999999  else cast(EntryApproveTimeCutoff as decimal(22,0)) end from Fund where FundPK = @FundPK and status in (1,2)

                        select @DecTimeNow = cast(@StringTimeNow as decimal(22,0))

                        IF (@DecTimeNow > @CutoffTime)
                        BEGIN
                        select 1 Result
                        END
                        ELSE 
                        BEGIN
                        select 0 Result
                        END     ";

                        cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                        cmd.Parameters.AddWithValue("@TimeNow", _dateTimeNow);

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


        public string Check_EntryApproveTimeCutoffBySelected(UnitRegistryFund _unitRegistryFund)
        {
            try
            {

                string paramSelected = "";
                if (!_host.findString(_unitRegistryFund.UnitRegistrySelected.ToLower(), "0", ",") && !string.IsNullOrEmpty(_unitRegistryFund.UnitRegistrySelected))
                {

                    paramSelected = " in (" + _unitRegistryFund.UnitRegistrySelected + ") ";
                }
                else
                {
                    paramSelected = " in (0) ";
                }

                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = @"
                        Create Table #ValidationTemp
                        (FundID nvarchar(50))
                        
                        Declare @StringTimeNow nvarchar(8)
                        Declare @CutoffTime decimal(22,0)
                        Declare @DecTimeNow decimal(22,0)

                        SELECT @StringTimeNow = REPLACE(substring(CONVERT(nvarchar(8),@TimeNow,108),0,9),':','')
                        DECLARE @combinedString VARCHAR(MAX)

                        IF(@UnitRegistryType = 'ClientSubscription')
                        BEGIN
                            Insert Into #ValidationTemp(FundID)
                            select distinct B.ID from ClientSubscription A
                            left join Fund B on A.FundPK = B.FundPK and B.status in (1,2)
                            where cast(@StringTimeNow as decimal(22,0)) > case when isnull(EntryApproveTimeCutoff,'') = '' then 999999  else cast(EntryApproveTimeCutoff as decimal(22,0)) end
                            and  A.ClientSubscriptionPK in 
                            (
	                            Select ClientSubscriptionPK from ClientSubscription where ValueDate between @DateFrom and @DateTo and Status = 1 and isnull(Notes,'') <> 'Pending Revised' and ClientSubscriptionPK " + paramSelected + @"
                            )


                            if exists(select distinct B.ID from ClientSubscription A
                            left join Fund B on A.FundPK = B.FundPK and B.status in (1,2)
                            where cast(@StringTimeNow as decimal(22,0)) > case when isnull(EntryApproveTimeCutoff,'') = '' then 999999  else cast(EntryApproveTimeCutoff as decimal(22,0)) end
                            and  A.ClientSubscriptionPK in 
                            (
	                            Select ClientSubscriptionPK from ClientSubscription where ValueDate between @DateFrom and @DateTo and Status = 1 and isnull(Notes,'') <> 'Pending Revised' and ClientSubscriptionPK " + paramSelected + @"
                            ))
                            BEGIN
                            SELECT @combinedString = COALESCE(@combinedString + ', ', '') + FundID
                            FROM #ValidationTemp
                            SELECT 'Transaction has passed the cut-off time, Please Check Fund : ' + @combinedString as Result 
                            END
                            ELSE
                            BEGIN
                            select '' Result
                            END  
                        END
                        ELSE IF(@UnitRegistryType = 'ClientRedemption')
                        BEGIN
                            Insert Into #ValidationTemp(FundID)
                            select distinct B.ID from ClientRedemption A
                            left join Fund B on A.FundPK = B.FundPK and B.status in (1,2)
                            where cast(@StringTimeNow as decimal(22,0)) > case when isnull(EntryApproveTimeCutoff,'') = '' then 999999  else cast(EntryApproveTimeCutoff as decimal(22,0)) end
                            and  A.ClientRedemptionPK in 
                            (
	                            Select ClientRedemptionPK from ClientRedemption where ValueDate between @DateFrom and @DateTo and Status = 1  and isnull(Notes,'') <> 'Pending Revised' and ClientRedemptionPK " + paramSelected + @"
                            )


                            if exists(select distinct B.ID from ClientRedemption A
                            left join Fund B on A.FundPK = B.FundPK and B.status in (1,2)
                            where cast(@StringTimeNow as decimal(22,0)) > case when isnull(EntryApproveTimeCutoff,'') = '' then 999999  else cast(EntryApproveTimeCutoff as decimal(22,0)) end
                            and  A.ClientRedemptionPK in 
                            (
	                            Select ClientRedemptionPK from ClientRedemption where ValueDate between @DateFrom and @DateTo and Status = 1  and isnull(Notes,'') <> 'Pending Revised' and ClientRedemptionPK " + paramSelected + @"
                            ))
                            BEGIN
                            SELECT @combinedString = COALESCE(@combinedString + ', ', '') + FundID
                            FROM #ValidationTemp
                            SELECT 'Transaction has passed the cut-off time, Please Check Fund : ' + @combinedString as Result 
                            END
                            ELSE
                            BEGIN
                            select '' Result
                            END  
                        END

                        ELSE
                        BEGIN
                            Insert Into #ValidationTemp(FundID)
                            select distinct B.ID from ClientSwitching A
                            left join Fund B on A.FundPKFrom = B.FundPK and B.status in (1,2)
                            where cast(@StringTimeNow as decimal(22,0)) > case when isnull(EntryApproveTimeCutoff,'') = '' then 999999  else cast(EntryApproveTimeCutoff as decimal(22,0)) end
                            and  A.ClientSwitchingPK in 
                            (
	                            Select ClientSwitchingPK from ClientSwitching where ValueDate between @DateFrom and @DateTo and Status = 1  and isnull(Notes,'') <> 'Pending Revised' and ClientSwitchingPK " + paramSelected + @"
                            )


                            if exists(select distinct B.ID from ClientSwitching A
                            left join Fund B on A.FundPKFrom = B.FundPK and B.status in (1,2)
                            where cast(@StringTimeNow as decimal(22,0)) > case when isnull(EntryApproveTimeCutoff,'') = '' then 999999  else cast(EntryApproveTimeCutoff as decimal(22,0)) end
                            and  A.ClientSwitchingPK in 
                            (
	                            Select ClientSwitchingPK from ClientSwitching where ValueDate between @DateFrom and @DateTo and Status = 1  and isnull(Notes,'') <> 'Pending Revised' and ClientSwitchingPK " + paramSelected + @"
                            ))
                            BEGIN
                      
                            SELECT @combinedString = COALESCE(@combinedString + ', ', '') + FundID
                            FROM #ValidationTemp
                            SELECT 'Transaction has passed the cut-off time, Please Check Fund : ' + @combinedString as Result 
                            END
                            ELSE
                            BEGIN
                            select '' Result
                            END  
                        END

";


                        cmd.Parameters.AddWithValue("@DateFrom", _unitRegistryFund.DateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _unitRegistryFund.DateTo);
                        cmd.Parameters.AddWithValue("@UnitRegistryType", _unitRegistryFund.UnitRegistryType);
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


        public Boolean Validate_CheckIssueDate(DateTime _valueDate, int _fundPK)
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

                        declare @IssueDate datetime

                        select @IssueDate = IssueDate from Fund where FundPK = @FundPK and status in (1,2)

                        IF (@ClientCode = 17)
                        BEGIN
                            IF(@IssueDate >= @ValueDate)
                            BEGIN
	                            SELECT 1 Result
	                            RETURN	
                            END	
                            ELSE
                            BEGIN
	                            select 0 Result
                            END 
                        END
                        ELSE
                        BEGIN
                            select 0 Result
                        END
                  ";

                        cmd.Parameters.AddWithValue("@ValueDate", _valueDate);
                        cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                        cmd.Parameters.AddWithValue("@ClientCode", Tools.ClientCode);
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


        public List<FundCombo> Fund_ComboByMaturity(DateTime _valueDate)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<FundCombo> L_Fund = new List<FundCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"
                                    --list dormant fund
                                    declare @ListFundDormant table
                                    (
	                                    FundPK int,
	                                    DormantDate date,
	                                    ActivateDate date,
	                                    StatusDormant int
                                    )

                                    insert into @ListFundDormant(FundPK)
                                    select distinct fundpk from DormantFundTrails where status = 2

                                    update A set A.DormantDate = B.DormantDate, A.ActivateDate = isnull(C.ActivateDate,'1900-01-01'), A.StatusDormant = case when isnull(C.ActivateDate,'1900-01-01') = '1900-01-01' then 1 
                                    when B.DormantDate > isnull(C.ActivateDate,'1900-01-01') then 1 else 0 end

                                    from @ListFundDormant A
                                    left join (
                                        select FundPK, Max(DormantDate) DormantDate from DormantFundTrails where status = 2 and BitDormant = 1
                                        group by FundPK
                                    ) B on A.FundPK = B.FundPK
                                    left join (
                                        select FundPK, Max(ActivateDate) ActivateDate from DormantFundTrails where status = 2 and BitDormant = 0
                                        group by FundPK
                                    ) C on A.FundPK = B.FundPK
                                    --end dormant fund


                                    SELECT  FundPK,ID + ' - ' + Name as ID, Name FROM [Fund]  where status = 2 and MaturityDate > @ValueDate and FundPK not in (select FundPK from @ListFundDormant where @ValueDate between DormantDate and case when isnull(ActivateDate,'1900-01-01') = '1900-01-01' then '2098-12-31' else dbo.FworkingDay(ActivateDate,-1) end)

";
                        cmd.Parameters.AddWithValue("@ValueDate", _valueDate);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    FundCombo M_Fund = new FundCombo();
                                    M_Fund.FundPK = Convert.ToInt32(dr["FundPK"]);
                                    M_Fund.ID = Convert.ToString(dr["ID"]);
                                    M_Fund.Name = Convert.ToString(dr["Name"]);
                                    L_Fund.Add(M_Fund);
                                }

                            }
                            return L_Fund;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }


        public decimal Fund_GetUnitPositionForDistributedIncome(int _fundPK, DateTime _date)
        {


            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();

                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = @" Select sum(isnull(UnitAmount,0)) UnitAmount from fundClientPosition where Date = (Select max(date) from fundclientposition where Date  <  @Date and FundPK = @FundPK) 
                        and FundPK = @FundPK ";

                        cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                        cmd.Parameters.AddWithValue("@Date", _date);



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

        public List<Fund> Fund_SelectMatureFundByDate(DateTime _dateTo)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<Fund> L_Fund = new List<Fund>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = @"select ID, Name From Fund
                                              Where MaturityDate = @DateTo and Status = 2";


                        cmd.Parameters.AddWithValue("@DateTo", _dateTo);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    Fund M_Fund = new Fund();
                                    M_Fund.ID = dr["ID"].ToString();
                                    M_Fund.Name = dr["Name"].ToString();
                                    L_Fund.Add(M_Fund);

                                }
                            }

                            return L_Fund;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }


        public List<FundCombo> Fund_ComboByBankPK(int _BankPK)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<FundCombo> L_Fund = new List<FundCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"
                            select FundPK,ID + ' - ' + Name as ID, Name from fund where BankBranchPK in (
	                            select BankBranchPK from BankBranch where bankpk = @BankPK and status = 2
                            )
                            and status = 2

";
                        cmd.Parameters.AddWithValue("@BankPK", _BankPK);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    FundCombo M_Fund = new FundCombo();
                                    M_Fund.FundPK = Convert.ToInt32(dr["FundPK"]);
                                    M_Fund.ID = Convert.ToString(dr["ID"]);
                                    M_Fund.Name = Convert.ToString(dr["Name"]);
                                    L_Fund.Add(M_Fund);
                                }

                            }
                            return L_Fund;
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




