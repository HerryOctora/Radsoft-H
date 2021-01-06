using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RFSModel;
using RFSUtility;
using System.Data.SqlClient;


namespace RFSRepository
{
    public class FundFeeReps
    {
        Host _host = new Host();

        //1
        string _insertCommand = "INSERT INTO [dbo].[FundFee] " +
                            "([FundFeePK],[HistoryPK],[Status],[Date],[PaymentDate],[FundPK],[ManagementFeePercent]," +
                            "[CustodiFeePercent],[AuditFeeAmount],[ManagementFeeDays],[CustodiFeeDays],[AuditFeeDays],[PaymentModeOnMaturity],[DateOfPayment]," +
                            "[SwitchingFeePercent],[FeeTypeManagement],[FeeTypeSubscription],[FeeTypeRedemption],[FeeTypeSwitching],[SinvestMoneyMarketFeePercent]," +
                            "[SinvestBondFeePercent],[SinvestEquityFeePercent],[BitPendingSubscription],[BitActDivDays],[BitPendingSwitchIn],[SInvestFeeDays],[MovementFeeAmount],[OtherFeeOneAmount],[OtherFeeTwoAmount],[OtherFeeThreeAmount],[CBESTEquityAmount],[CBESTCorpBondAmount],[CBESTGovBondAmount],[AccruedInterestCalculation],[TaxInterestDeposit],";
        string _paramaterCommand = "@Date,@PaymentDate,@FundPK,@ManagementFeePercent,@CustodiFeePercent,@AuditFeeAmount,@ManagementFeeDays,@CustodiFeeDays,@AuditFeeDays," +
                                   "@PaymentModeOnMaturity,@DateOfPayment,@SwitchingFeePercent,@FeeTypeManagement,@FeeTypeSubscription,@FeeTypeRedemption,@FeeTypeSwitching," +
                                   "@SinvestMoneyMarketFeePercent,@SinvestBondFeePercent,@SinvestEquityFeePercent,@BitPendingSubscription,@BitActDivDays,@BitPendingSwitchIn,@SInvestFeeDays,@MovementFeeAmount,@OtherFeeOneAmount,@OtherFeeTwoAmount,@OtherFeeThreeAmount,@CBESTEquityAmount,@CBESTCorpBondAmount,@CBESTGovBondAmount,@AccruedInterestCalculation,@TaxInterestDeposit,";


        //2
        private FundFee setFundFee(SqlDataReader dr)
        {
            FundFee M_FundFee = new FundFee();
            M_FundFee.FundFeePK = Convert.ToInt32(dr["FundFeePK"]);
            M_FundFee.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_FundFee.Status = Convert.ToInt32(dr["Status"]);
            M_FundFee.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_FundFee.Notes = Convert.ToString(dr["Notes"]);
            M_FundFee.Date = dr["Date"].ToString();
            M_FundFee.PaymentDate = dr["PaymentDate"].ToString();
            M_FundFee.FundPK = Convert.ToInt32(dr["FundPK"]);
            M_FundFee.FundID = Convert.ToString(dr["FundID"]);
            M_FundFee.FundName = Convert.ToString(dr["FundName"]);
            M_FundFee.ManagementFeePercent = Convert.ToDecimal(dr["ManagementFeePercent"]);
            M_FundFee.CustodiFeePercent = Convert.ToDecimal(dr["CustodiFeePercent"]);
            M_FundFee.AuditFeeAmount = Convert.ToDecimal(dr["AuditFeeAmount"]);
            M_FundFee.ManagementFeeDays = Convert.ToInt32(dr["ManagementFeeDays"]);
            M_FundFee.CustodiFeeDays = Convert.ToInt32(dr["CustodiFeeDays"]);
            M_FundFee.AuditFeeDays = Convert.ToInt32(dr["AuditFeeDays"]);
            M_FundFee.SInvestFeeDays = Convert.ToInt32(dr["SInvestFeeDays"]);
            M_FundFee.PaymentModeOnMaturity = Convert.ToInt32(dr["PaymentModeOnMaturity"]);
            M_FundFee.PaymentModeOnMaturityDesc = dr["PaymentModeOnMaturityDesc"].ToString();
            M_FundFee.SwitchingFeePercent = Convert.ToDecimal(dr["SwitchingFeePercent"]);
            M_FundFee.FeeTypeManagement = Convert.ToInt32(dr["FeeTypeManagement"]);
            M_FundFee.FeeTypeManagementDesc = dr["FeeTypeManagementDesc"].ToString();
            M_FundFee.FeeTypeSubscription = Convert.ToInt32(dr["FeeTypeSubscription"]);
            M_FundFee.FeeTypeSubscriptionDesc = dr["FeeTypeSubscriptionDesc"].ToString();
            M_FundFee.FeeTypeRedemption = Convert.ToInt32(dr["FeeTypeRedemption"]);
            M_FundFee.FeeTypeRedemptionDesc = dr["FeeTypeRedemptionDesc"].ToString();
            M_FundFee.FeeTypeSwitching = Convert.ToInt32(dr["FeeTypeSwitching"]);
            M_FundFee.FeeTypeSwitchingDesc = dr["FeeTypeSwitchingDesc"].ToString();
            M_FundFee.DateOfPayment = Convert.ToDecimal(dr["DateOfPayment"]);
            M_FundFee.SinvestMoneyMarketFeePercent = dr["SinvestMoneyMarketFeePercent"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["SinvestMoneyMarketFeePercent"]);
            M_FundFee.SinvestBondFeePercent = dr["SinvestBondFeePercent"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["SinvestBondFeePercent"]);
            M_FundFee.SinvestEquityFeePercent = dr["SinvestEquityFeePercent"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["SinvestEquityFeePercent"]);
            M_FundFee.BitPendingSubscription = Convert.ToBoolean(dr["BitPendingSubscription"]);
            M_FundFee.BitPendingSwitchIn = Convert.ToBoolean(dr["BitPendingSwitchIn"]);
            if (_host.CheckColumnIsExist(dr, "BitActDivDays"))
            {
                M_FundFee.BitActDivDays = dr["BitActDivDays"].Equals(DBNull.Value) == true ? false : Convert.ToBoolean(dr["BitActDivDays"]);
            }
            M_FundFee.MovementFeeAmount = dr["MovementFeeAmount"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["MovementFeeAmount"]);
            M_FundFee.OtherFeeOneAmount = dr["OtherFeeOneAmount"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["OtherFeeOneAmount"]);
            M_FundFee.OtherFeeTwoAmount = dr["OtherFeeTwoAmount"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["OtherFeeTwoAmount"]);
            M_FundFee.OtherFeeThreeAmount = dr["OtherFeeThreeAmount"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["OtherFeeThreeAmount"]);

            M_FundFee.CBESTEquityAmount = dr["CBESTEquityAmount"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["CBESTEquityAmount"]);
            M_FundFee.CBESTCorpBondAmount = dr["CBESTCorpBondAmount"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["CBESTCorpBondAmount"]);
            M_FundFee.CBESTGovBondAmount = dr["CBESTGovBondAmount"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["CBESTGovBondAmount"]);

            M_FundFee.AccruedInterestCalculation = dr["AccruedInterestCalculation"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["AccruedInterestCalculation"]);
            //M_FundFee.AccruedInterestCalculationDesc = dr["AccruedInterestCalculationDesc"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["AccruedInterestCalculationDesc"]);

            if (_host.CheckColumnIsExist(dr, "AccruedInterestCalculationDesc"))
            {
                M_FundFee.AccruedInterestCalculationDesc = dr["AccruedInterestCalculationDesc"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["AccruedInterestCalculationDesc"]);
            }
            if (_host.CheckColumnIsExist(dr, "TaxInterestDeposit"))
            {
                M_FundFee.TaxInterestDeposit = dr["TaxInterestDeposit"].Equals(DBNull.Value) == true ? false : Convert.ToBoolean(dr["TaxInterestDeposit"]);
            }

            M_FundFee.EntryUsersID = dr["EntryUsersID"].ToString();
            M_FundFee.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_FundFee.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_FundFee.VoidUsersID = dr["VoidUsersID"].ToString();
            M_FundFee.EntryTime = dr["EntryTime"].ToString();
            M_FundFee.UpdateTime = dr["UpdateTime"].ToString();
            M_FundFee.ApprovedTime = dr["ApprovedTime"].ToString();
            M_FundFee.VoidTime = dr["VoidTime"].ToString();
            M_FundFee.DBUserID = dr["DBUserID"].ToString();
            M_FundFee.DBTerminalID = dr["DBTerminalID"].ToString();
            M_FundFee.LastUpdate = dr["LastUpdate"].ToString();
            M_FundFee.LastUpdateDB = dr["LastUpdateDB"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["LastUpdateDB"]);
            return M_FundFee;
        }

        public List<FundFee> FundFee_Select(int _status)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<FundFee> L_FundFee = new List<FundFee>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = @"
Select case when A.status=1 then 'PENDING' else Case When A.status = 2 then 'APPROVED' else Case when A.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,
B.ID FundID,B.Name FundName,C.DescOne FeeTypeManagementDesc,C1.DescOne FeeTypeSubscriptionDesc,C2.DescOne FeeTypeRedemptionDesc,C3.DescOne FeeTypeSwitchingDesc, C4.DescOne PaymentModeOnMaturityDesc, isnull(A.SinvestMoneyMarketFeePercent,0) SinvestMoneyMarketFeePercent, isnull(A.SinvestBondFeePercent,0) SinvestBondFeePercent, isnull(A.SinvestEquityFeePercent,0) SinvestEquityFeePercent
,case when A.AccruedInterestCalculation=1 then 'Exact Calculation' else 'Rounded Calculation' end AccruedInterestCalculationDesc,  A.* from FundFee A
left join Fund B on A.FundPK = B.FundPK and B.status  = 2
left join mastervalue C on A.FeeTypeManagement = C.Code and C.ID = 'FundFeeType' and C.status = 2
left join mastervalue C1 on A.FeeTypeSubscription = C1.Code and C1.ID = 'FundFeeType' and C1.status = 2
left join mastervalue C2 on A.FeeTypeRedemption = C2.Code and C2.ID = 'FundFeeType' and C2.status = 2
left join mastervalue C3 on A.FeeTypeSwitching = C3.Code and C3.ID = 'FundFeeType' and C3.status = 2
Left join MasterValue C4 on A.PaymentModeOnMaturity = C4.Code and C4.ID = 'PaymentModeOnMaturity' and C4.Status =2
where A.status = @status";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = @"Select case when A.status=1 then 'PENDING' else Case When A.status = 2 then 'APPROVED' else Case when A.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,
B.ID FundID,B.Name FundName,C.DescOne FeeTypeManagementDesc,C1.DescOne FeeTypeSubscriptionDesc,C2.DescOne FeeTypeRedemptionDesc,C3.DescOne FeeTypeSwitchingDesc, C4.DescOne PaymentModeOnMaturityDesc, isnull(A.SinvestMoneyMarketFeePercent,0) SinvestMoneyMarketFeePercent, isnull(A.SinvestBondFeePercent,0) SinvestBondFeePercent, isnull(A.SinvestEquityFeePercent,0) SinvestEquityFeePercent
,case when A.AccruedInterestCalculation=1 then 'Exact Calculation' else 'Rounded Calculation' end AccruedInterestCalculationDesc, A.* from FundFee A
left join Fund B on A.FundPK = B.FundPK and B.status  = 2
left join mastervalue C on A.FeeTypeManagement = C.Code and C.ID = 'FundFeeType' and C.status = 2
left join mastervalue C1 on A.FeeTypeSubscription = C1.Code and C1.ID = 'FundFeeType' and C1.status = 2
left join mastervalue C2 on A.FeeTypeRedemption = C2.Code and C2.ID = 'FundFeeType' and C2.status = 2
left join mastervalue C3 on A.FeeTypeSwitching = C3.Code and C3.ID = 'FundFeeType' and C3.status = 2
Left join MasterValue C4 on A.PaymentModeOnMaturity = C4.Code and C4.ID = 'PaymentModeOnMaturity' and C4.Status =2
Order by FundPK";
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_FundFee.Add(setFundFee(dr));
                                }
                            }
                            return L_FundFee;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int FundFee_Add(FundFee _FundFee, bool _havePrivillege)
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
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[ApprovedUsersID],[ApprovedTime],LastUpdate)" +
                                 "Select isnull(max(FundFeePk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@LastUpdate from FundFee";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _FundFee.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);

                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],LastUpdate)" +
                                "Select isnull(max(FundFeePk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@LastUpdate from FundFee";
                        }
                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@Date", _FundFee.Date);
                        cmd.Parameters.AddWithValue("@PaymentDate", _FundFee.PaymentDate);
                        cmd.Parameters.AddWithValue("@FundPK", _FundFee.FundPK);
                        cmd.Parameters.AddWithValue("@ManagementFeePercent", _FundFee.ManagementFeePercent);
                        cmd.Parameters.AddWithValue("@CustodiFeePercent", _FundFee.CustodiFeePercent);
                        cmd.Parameters.AddWithValue("@AuditFeeAmount", _FundFee.AuditFeeAmount);
                        cmd.Parameters.AddWithValue("@ManagementFeeDays", _FundFee.ManagementFeeDays);
                        cmd.Parameters.AddWithValue("@CustodiFeeDays", _FundFee.CustodiFeeDays);
                        cmd.Parameters.AddWithValue("@AuditFeeDays", _FundFee.AuditFeeDays);
                        cmd.Parameters.AddWithValue("@SInvestFeeDays", _FundFee.SInvestFeeDays);
                        cmd.Parameters.AddWithValue("@PaymentModeOnMaturity", _FundFee.PaymentModeOnMaturity);
                        cmd.Parameters.AddWithValue("@DateOfPayment", _FundFee.DateOfPayment);
                        cmd.Parameters.AddWithValue("@SwitchingFeePercent", _FundFee.SwitchingFeePercent);
                        cmd.Parameters.AddWithValue("@FeeTypeManagement", _FundFee.FeeTypeManagement);
                        cmd.Parameters.AddWithValue("@FeeTypeSubscription", _FundFee.FeeTypeSubscription);
                        cmd.Parameters.AddWithValue("@FeeTypeRedemption", _FundFee.FeeTypeRedemption);
                        cmd.Parameters.AddWithValue("@FeeTypeSwitching", _FundFee.FeeTypeSwitching);
                        cmd.Parameters.AddWithValue("@SinvestMoneyMarketFeePercent", _FundFee.SinvestMoneyMarketFeePercent);
                        cmd.Parameters.AddWithValue("@SinvestBondFeePercent", _FundFee.SinvestBondFeePercent);
                        cmd.Parameters.AddWithValue("@SinvestEquityFeePercent", _FundFee.SinvestEquityFeePercent);
                        cmd.Parameters.AddWithValue("@BitPendingSubscription", _FundFee.BitPendingSubscription);
                        cmd.Parameters.AddWithValue("@BitActDivDays", _FundFee.BitActDivDays);
                        cmd.Parameters.AddWithValue("@BitPendingSwitchIn", _FundFee.BitPendingSwitchIn);
                        cmd.Parameters.AddWithValue("@MovementFeeAmount", _FundFee.MovementFeeAmount);
                        cmd.Parameters.AddWithValue("@OtherFeeOneAmount", _FundFee.OtherFeeOneAmount);
                        cmd.Parameters.AddWithValue("@OtherFeeTwoAmount", _FundFee.OtherFeeTwoAmount);
                        cmd.Parameters.AddWithValue("@OtherFeeThreeAmount", _FundFee.OtherFeeThreeAmount);

                        cmd.Parameters.AddWithValue("@CBESTEquityAmount", _FundFee.CBESTEquityAmount);
                        cmd.Parameters.AddWithValue("@CBESTCorpBondAmount", _FundFee.CBESTCorpBondAmount);
                        cmd.Parameters.AddWithValue("@CBESTGovBondAmount", _FundFee.CBESTGovBondAmount);

                        cmd.Parameters.AddWithValue("@AccruedInterestCalculation", _FundFee.AccruedInterestCalculation);
                        cmd.Parameters.AddWithValue("@TaxInterestDeposit", _FundFee.TaxInterestDeposit);

                        cmd.Parameters.AddWithValue("@EntryUsersID", _FundFee.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();

                        return _host.Get_LastPKByLastUpate(_datetimeNow, "FundFee");
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int FundFee_Update(FundFee _FundFee, bool _havePrivillege)
        {

            try
            {
                int _newHisPK;
                int status = _host.Get_Status(_FundFee.FundFeePK, _FundFee.HistoryPK, "FundFee");
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update FundFee set status=2, Notes=@Notes,Date=@Date,PaymentDate=@PaymentDate,FundPK=@FundPK,ManagementFeePercent=@ManagementFeePercent, " +
                                "CustodiFeePercent=@CustodiFeePercent,AuditFeeAmount=@AuditFeeAmount,ManagementFeeDays=@ManagementFeeDays, CustodiFeeDays=@CustodiFeeDays,AuditFeeDays=@AuditFeeDays,PaymentModeOnMaturity=@PaymentModeOnMaturity,SwitchingFeePercent=@SwitchingFeePercent,FeeTypeManagement=@FeeTypeManagement,DateOfPayment=@DateOfPayment,  " +
                                "FeeTypeSubscription=@FeeTypeSubscription,FeeTypeRedemption=@FeeTypeRedemption,FeeTypeSwitching=@FeeTypeSwitching,SinvestMoneyMarketFeePercent=@SinvestMoneyMarketFeePercent,SinvestBondFeePercent=@SinvestBondFeePercent,SinvestEquityFeePercent=@SinvestEquityFeePercent,BitPendingSubscription=@BitPendingSubscription,BitActDivDays=@BitActDivDays,BitPendingSwitchIn=@BitPendingSwitchIn,SInvestFeeDays=@SInvestFeeDays,OtherFeeOneAmount=@OtherFeeOneAmount,OtherFeeTwoAmount=@OtherFeeTwoAmount,OtherFeeThreeAmount=@OtherFeeThreeAmount,CBESTEquityAmount=@CBESTEquityAmount,CBESTCorpBondAmount=@CBESTCorpBondAmount,CBESTGovBondAmount=@CBESTGovBondAmount,TaxInterestDeposit=@TaxInterestDeposit,  " +
                                "ApprovedUsersID=@ApprovedUsersID, " +
                                "ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@lastupdate " +
                                "where FundFeePK = @PK and historyPK = @HistoryPK";
                            cmd.Parameters.AddWithValue("@HistoryPK", _FundFee.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _FundFee.FundFeePK);
                            cmd.Parameters.AddWithValue("@Notes", _FundFee.Notes);
                            cmd.Parameters.AddWithValue("@Date", _FundFee.Date);
                            cmd.Parameters.AddWithValue("@PaymentDate", _FundFee.PaymentDate);
                            cmd.Parameters.AddWithValue("@FundPK", _FundFee.FundPK);
                            cmd.Parameters.AddWithValue("@ManagementFeePercent", _FundFee.ManagementFeePercent);
                            cmd.Parameters.AddWithValue("@CustodiFeePercent", _FundFee.CustodiFeePercent);
                            cmd.Parameters.AddWithValue("@AuditFeeAmount", _FundFee.AuditFeeAmount);
                            cmd.Parameters.AddWithValue("@ManagementFeeDays", _FundFee.ManagementFeeDays);
                            cmd.Parameters.AddWithValue("@CustodiFeeDays", _FundFee.CustodiFeeDays);
                            cmd.Parameters.AddWithValue("@AuditFeeDays", _FundFee.AuditFeeDays);
                            cmd.Parameters.AddWithValue("@SInvestFeeDays", _FundFee.SInvestFeeDays);
                            cmd.Parameters.AddWithValue("@PaymentModeOnMaturity", _FundFee.PaymentModeOnMaturity);
                            cmd.Parameters.AddWithValue("@DateOfPayment", _FundFee.DateOfPayment);
                            cmd.Parameters.AddWithValue("@SwitchingFeePercent", _FundFee.SwitchingFeePercent);
                            cmd.Parameters.AddWithValue("@FeeTypeManagement", _FundFee.FeeTypeManagement);
                            cmd.Parameters.AddWithValue("@FeeTypeSubscription", _FundFee.FeeTypeSubscription);
                            cmd.Parameters.AddWithValue("@FeeTypeRedemption", _FundFee.FeeTypeRedemption);
                            cmd.Parameters.AddWithValue("@FeeTypeSwitching", _FundFee.FeeTypeSwitching);
                            cmd.Parameters.AddWithValue("@SinvestMoneyMarketFeePercent", _FundFee.SinvestMoneyMarketFeePercent);
                            cmd.Parameters.AddWithValue("@SinvestBondFeePercent", _FundFee.SinvestBondFeePercent);
                            cmd.Parameters.AddWithValue("@SinvestEquityFeePercent", _FundFee.SinvestEquityFeePercent);
                            cmd.Parameters.AddWithValue("@BitPendingSubscription", _FundFee.BitPendingSubscription);
                            cmd.Parameters.AddWithValue("@BitActDivDays", _FundFee.BitActDivDays);
                            cmd.Parameters.AddWithValue("@BitPendingSwitchIn", _FundFee.BitPendingSwitchIn);
                            cmd.Parameters.AddWithValue("@MovementFeeAmount", _FundFee.MovementFeeAmount);
                            cmd.Parameters.AddWithValue("@OtherFeeOneAmount", _FundFee.OtherFeeOneAmount);
                            cmd.Parameters.AddWithValue("@OtherFeeTwoAmount", _FundFee.OtherFeeTwoAmount);
                            cmd.Parameters.AddWithValue("@OtherFeeThreeAmount", _FundFee.OtherFeeThreeAmount);

                            cmd.Parameters.AddWithValue("@CBESTEquityAmount", _FundFee.CBESTEquityAmount);
                            cmd.Parameters.AddWithValue("@CBESTCorpBondAmount", _FundFee.CBESTCorpBondAmount);
                            cmd.Parameters.AddWithValue("@CBESTGovBondAmount", _FundFee.CBESTGovBondAmount);

                            cmd.Parameters.AddWithValue("@AccruedInterestCalculation", _FundFee.AccruedInterestCalculation);
                            cmd.Parameters.AddWithValue("@TaxInterestDeposit", _FundFee.TaxInterestDeposit);

                            cmd.Parameters.AddWithValue("@UpdateUsersID", _FundFee.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _FundFee.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);

                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update FundFee set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where FundFeePK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _FundFee.FundFeePK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _FundFee.EntryUsersID);
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
                                cmd.CommandText = "Update FundFee set Notes=@Notes,Date=@Date,PaymentDate=@PaymentDate,FundPK=@FundPK,ManagementFeePercent=@ManagementFeePercent, " +
                                "CustodiFeePercent=@CustodiFeePercent,AuditFeeAmount=@AuditFeeAmount,ManagementFeeDays=@ManagementFeeDays, CustodiFeeDays=@CustodiFeeDays,AuditFeeDays=@AuditFeeDays,PaymentModeOnMaturity=@PaymentModeOnMaturity,SwitchingFeePercent=@SwitchingFeePercent,FeeTypeManagement=@FeeTypeManagement,DateOfPayment=@DateOfPayment,  " +
                                "FeeTypeSubscription=@FeeTypeSubscription,FeeTypeRedemption=@FeeTypeRedemption,FeeTypeSwitching=@FeeTypeSwitching,SinvestMoneyMarketFeePercent=@SinvestMoneyMarketFeePercent,SinvestBondFeePercent=@SinvestBondFeePercent,SinvestEquityFeePercent=@SinvestEquityFeePercent,BitPendingSubscription=@BitPendingSubscription,BitActDivDays=@BitActDivDays,BitPendingSwitchIn=@BitPendingSwitchIn,SInvestFeeDays=@SInvestFeeDays,MovementFeeAmount=@MovementFeeAmount,OtherFeeOneAmount=@OtherFeeOneAmount,OtherFeeTwoAmount=@OtherFeeTwoAmount,OtherFeeThreeAmount=@OtherFeeThreeAmount,CBESTEquityAmount=@CBESTEquityAmount,CBESTCorpBondAmount=@CBESTCorpBondAmount,CBESTGovBondAmount=@CBESTGovBondAmount,AccruedInterestCalculation=@AccruedInterestCalculation,TaxInterestDeposit=@TaxInterestDeposit, " +
                                "UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@lastupdate " +
                                "where FundFeePK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _FundFee.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _FundFee.FundFeePK);
                                cmd.Parameters.AddWithValue("@Notes", _FundFee.Notes);
                                cmd.Parameters.AddWithValue("@Date", _FundFee.Date);
                                cmd.Parameters.AddWithValue("@PaymentDate", _FundFee.PaymentDate);
                                cmd.Parameters.AddWithValue("@FundPK", _FundFee.FundPK);
                                cmd.Parameters.AddWithValue("@ManagementFeePercent", _FundFee.ManagementFeePercent);
                                cmd.Parameters.AddWithValue("@CustodiFeePercent", _FundFee.CustodiFeePercent);
                                cmd.Parameters.AddWithValue("@AuditFeeAmount", _FundFee.AuditFeeAmount);
                                cmd.Parameters.AddWithValue("@ManagementFeeDays", _FundFee.ManagementFeeDays);
                                cmd.Parameters.AddWithValue("@CustodiFeeDays", _FundFee.CustodiFeeDays);
                                cmd.Parameters.AddWithValue("@AuditFeeDays", _FundFee.AuditFeeDays);
                                cmd.Parameters.AddWithValue("@SInvestFeeDays", _FundFee.SInvestFeeDays);
                                cmd.Parameters.AddWithValue("@PaymentModeOnMaturity", _FundFee.PaymentModeOnMaturity);
                                cmd.Parameters.AddWithValue("@DateOfPayment", _FundFee.DateOfPayment);
                                cmd.Parameters.AddWithValue("@SwitchingFeePercent", _FundFee.SwitchingFeePercent);
                                cmd.Parameters.AddWithValue("@FeeTypeManagement", _FundFee.FeeTypeManagement);
                                cmd.Parameters.AddWithValue("@FeeTypeSubscription", _FundFee.FeeTypeSubscription);
                                cmd.Parameters.AddWithValue("@FeeTypeRedemption", _FundFee.FeeTypeRedemption);
                                cmd.Parameters.AddWithValue("@FeeTypeSwitching", _FundFee.FeeTypeSwitching);
                                cmd.Parameters.AddWithValue("@SinvestMoneyMarketFeePercent", _FundFee.SinvestMoneyMarketFeePercent);
                                cmd.Parameters.AddWithValue("@SinvestBondFeePercent", _FundFee.SinvestBondFeePercent);
                                cmd.Parameters.AddWithValue("@SinvestEquityFeePercent", _FundFee.SinvestEquityFeePercent);
                                cmd.Parameters.AddWithValue("@BitPendingSubscription", _FundFee.BitPendingSubscription);
                                cmd.Parameters.AddWithValue("@BitActDivDays", _FundFee.BitActDivDays);
                                cmd.Parameters.AddWithValue("@BitPendingSwitchIn", _FundFee.BitPendingSwitchIn);
                                cmd.Parameters.AddWithValue("@MovementFeeAmount", _FundFee.MovementFeeAmount);
                                cmd.Parameters.AddWithValue("@OtherFeeOneAmount", _FundFee.OtherFeeOneAmount);
                                cmd.Parameters.AddWithValue("@OtherFeeTwoAmount", _FundFee.OtherFeeTwoAmount);
                                cmd.Parameters.AddWithValue("@OtherFeeThreeAmount", _FundFee.OtherFeeThreeAmount);

                                cmd.Parameters.AddWithValue("@CBESTEquityAmount", _FundFee.CBESTEquityAmount);
                                cmd.Parameters.AddWithValue("@CBESTCorpBondAmount", _FundFee.CBESTCorpBondAmount);
                                cmd.Parameters.AddWithValue("@CBESTGovBondAmount", _FundFee.CBESTGovBondAmount);

                                cmd.Parameters.AddWithValue("@AccruedInterestCalculation", _FundFee.AccruedInterestCalculation);
                                cmd.Parameters.AddWithValue("@TaxInterestDeposit", _FundFee.TaxInterestDeposit);

                                cmd.Parameters.AddWithValue("@UpdateUsersID", _FundFee.EntryUsersID);
                                cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                                cmd.ExecuteNonQuery();
                            }
                            return 0;
                        }
                        else
                        {
                            //ini untuk entrier
                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                _newHisPK = _host.Get_NewHistoryPK(_FundFee.FundFeePK, "FundFee");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                   "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                   "From FundFee where FundFeePK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _FundFee.FundFeePK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _FundFee.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@Date", _FundFee.Date);
                                cmd.Parameters.AddWithValue("@PaymentDate", _FundFee.PaymentDate);
                                cmd.Parameters.AddWithValue("@FundPK", _FundFee.FundPK);
                                cmd.Parameters.AddWithValue("@ManagementFeePercent", _FundFee.ManagementFeePercent);
                                cmd.Parameters.AddWithValue("@CustodiFeePercent", _FundFee.CustodiFeePercent);
                                cmd.Parameters.AddWithValue("@AuditFeeAmount", _FundFee.AuditFeeAmount);
                                cmd.Parameters.AddWithValue("@ManagementFeeDays", _FundFee.ManagementFeeDays);
                                cmd.Parameters.AddWithValue("@CustodiFeeDays", _FundFee.CustodiFeeDays);
                                cmd.Parameters.AddWithValue("@AuditFeeDays", _FundFee.AuditFeeDays);
                                cmd.Parameters.AddWithValue("@SInvestFeeDays", _FundFee.SInvestFeeDays);
                                cmd.Parameters.AddWithValue("@PaymentModeOnMaturity", _FundFee.PaymentModeOnMaturity);
                                cmd.Parameters.AddWithValue("@DateOfPayment", _FundFee.DateOfPayment);
                                cmd.Parameters.AddWithValue("@SwitchingFeePercent", _FundFee.SwitchingFeePercent);
                                cmd.Parameters.AddWithValue("@FeeTypeManagement", _FundFee.FeeTypeManagement);
                                cmd.Parameters.AddWithValue("@FeeTypeSubscription", _FundFee.FeeTypeSubscription);
                                cmd.Parameters.AddWithValue("@FeeTypeRedemption", _FundFee.FeeTypeRedemption);
                                cmd.Parameters.AddWithValue("@FeeTypeSwitching", _FundFee.FeeTypeSwitching);
                                cmd.Parameters.AddWithValue("@SinvestMoneyMarketFeePercent", _FundFee.SinvestMoneyMarketFeePercent);
                                cmd.Parameters.AddWithValue("@SinvestBondFeePercent", _FundFee.SinvestBondFeePercent);
                                cmd.Parameters.AddWithValue("@SinvestEquityFeePercent", _FundFee.SinvestEquityFeePercent);
                                cmd.Parameters.AddWithValue("@BitPendingSubscription", _FundFee.BitPendingSubscription);
                                cmd.Parameters.AddWithValue("@BitActDivDays", _FundFee.BitActDivDays);
                                cmd.Parameters.AddWithValue("@BitPendingSwitchIn", _FundFee.BitPendingSwitchIn);
                                cmd.Parameters.AddWithValue("@MovementFeeAmount", _FundFee.MovementFeeAmount);
                                cmd.Parameters.AddWithValue("@OtherFeeOneAmount", _FundFee.OtherFeeOneAmount);
                                cmd.Parameters.AddWithValue("@OtherFeeTwoAmount", _FundFee.OtherFeeTwoAmount);
                                cmd.Parameters.AddWithValue("@OtherFeeThreeAmount", _FundFee.OtherFeeThreeAmount);

                                cmd.Parameters.AddWithValue("@CBESTEquityAmount", _FundFee.CBESTEquityAmount);
                                cmd.Parameters.AddWithValue("@CBESTCorpBondAmount", _FundFee.CBESTCorpBondAmount);
                                cmd.Parameters.AddWithValue("@CBESTGovBondAmount", _FundFee.CBESTGovBondAmount);

                                cmd.Parameters.AddWithValue("@AccruedInterestCalculation", _FundFee.AccruedInterestCalculation);
                                cmd.Parameters.AddWithValue("@TaxInterestDeposit", _FundFee.TaxInterestDeposit);

                                cmd.Parameters.AddWithValue("@UpdateUsersID", _FundFee.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update FundFee set status= 4,Notes=@Notes, " +
                                    " LastUpdate=@lastupdate " +
                                    " where FundFeePK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _FundFee.Notes);
                                cmd.Parameters.AddWithValue("@PK", _FundFee.FundFeePK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _FundFee.HistoryPK);
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

        public void FundFee_Approved(FundFee _FundFee)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update FundFee set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,LastUpdate=@lastupdate " +
                            "where FundFeepk = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _FundFee.FundFeePK);
                        cmd.Parameters.AddWithValue("@historyPK", _FundFee.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _FundFee.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update FundFee set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where FundFeePK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _FundFee.FundFeePK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _FundFee.ApprovedUsersID);
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

        public void FundFee_Reject(FundFee _FundFee)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update FundFee set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate  " +
                            "where FundFeepk = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _FundFee.FundFeePK);
                        cmd.Parameters.AddWithValue("@historyPK", _FundFee.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _FundFee.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update FundFee set status= 2,LastUpdate=@LastUpdate  where FundFeePK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _FundFee.FundFeePK);
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

        public void FundFee_Void(FundFee _FundFee)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update FundFee set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where FundFeepk = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _FundFee.FundFeePK);
                        cmd.Parameters.AddWithValue("@historyPK", _FundFee.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _FundFee.VoidUsersID);
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

        public List<SetFundFeeSetup> FundFee_GetDataFundFee(int _fundPK)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<SetFundFeeSetup> L_setFundFeeSetup = new List<SetFundFeeSetup>();
                    using (SqlCommand cmd1 = DbCon.CreateCommand())
                    {
                        cmd1.CommandText = @"
                        select A.Status Status, case when A.status=1 then 'PENDING' else Case When A.status = 2 then 'APPROVED' else Case when A.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc, 
                        case when A.FundPK = 0 then 'ALL' else isnull(B.Name,'') end FundName, 
                        MV.DescOne FeeTypeDesc,
                         Date Date, DateAmortize, isnull(MiFeeAmount,0) MiFeeAmount,
                        isnull(MiFeePercent,0) MiFeePercent, isnull(RangeFrom,0) RangeFrom, isnull(RangeTo,0) RangeTo, isnull(B.FundPK,0),A.* 
                        from FundFeesetup A 
                        left join Fund B on A.FundPK = B.FundPK and B.status in (1,2) 
                        left join MasterValue MV on A.FeeType = MV.Code and MV.status in (1,2) and MV.ID = 'FundFeeType'
                        where A.FundPK  =  @FundPK and A.Status in (1,2)
                               ";
                        cmd1.Parameters.AddWithValue("@FundPK", _fundPK);
                        using (SqlDataReader dr = cmd1.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_setFundFeeSetup.Add(SetFundFeeSetup(dr));
                                }
                            }
                        }
                        return L_setFundFeeSetup;
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        private SetFundFeeSetup SetFundFeeSetup(SqlDataReader dr)
        {
            SetFundFeeSetup M_FundFeeSetup = new SetFundFeeSetup();
            M_FundFeeSetup.Status = Convert.ToInt32(dr["Status"]);
            M_FundFeeSetup.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_FundFeeSetup.Selected = Convert.ToBoolean(dr["Selected"]);
            M_FundFeeSetup.FundFeeSetupPK = Convert.ToInt32(dr["FundFeeSetupPK"]);
            M_FundFeeSetup.FundPK = Convert.ToInt32(dr["FundPK"]);
            M_FundFeeSetup.FundName = Convert.ToString(dr["FundName"]);
            M_FundFeeSetup.Date = Convert.ToString(dr["Date"]);
            M_FundFeeSetup.DateAmortize = Convert.ToString(dr["DateAmortize"]);
            M_FundFeeSetup.FeeType = Convert.ToInt32(dr["FeeType"]);
            M_FundFeeSetup.FeeTypeDesc = Convert.ToString(dr["FeeTypeDesc"]);
            M_FundFeeSetup.RangeTo = Convert.ToDecimal(dr["RangeTo"]);
            M_FundFeeSetup.RangeFrom = Convert.ToDecimal(dr["RangeFrom"]);
            M_FundFeeSetup.MiFeeAmount = Convert.ToDecimal(dr["MiFeeAmount"]);
            M_FundFeeSetup.MiFeePercent = Convert.ToDecimal(dr["MiFeePercent"]);
            return M_FundFeeSetup;
        }


        public int AddFundFee(FundFee _FundFee, bool _havePrivillege)
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
                            
                        Insert into FundFeeSetup(FundFeeSetupPK,HistoryPK,Status,FundPK,Date,DateAmortize,RangeFrom,RangeTo,MiFeeAmount,MiFeePercent,FeeType,LastUpdate,LastUpdateDB,EntryUsersID,EntryTime,ApprovedUsersID,ApprovedTime,UpdateUsersID,UpdateTime) 
                        Select isnull(max(FundFeeSetupPK),0) + 1,1,2,@FundPK,@Date,@DateAmortize,@RangeFrom,@RangeTo,@MiFeeAmount,@MiFeePercent,@FeeType,@LastUpdate,@LastUpdate,@UpdateUsersID,@UpdateTime,@UpdateUsersID,@UpdateTime,@UpdateUsersID,@UpdateTime from FundFeeSetup";

                        cmd.Parameters.AddWithValue("@FundPK", _FundFee.FundPK);
                        cmd.Parameters.AddWithValue("@Date", _FundFee.Date);
                        if (_FundFee.DateAmortize == null)
                        {
                            cmd.Parameters.AddWithValue("@DateAmortize", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@DateAmortize", _FundFee.DateAmortize);
                        }
                        cmd.Parameters.AddWithValue("@FeeType", _FundFee.FeeType);
                        cmd.Parameters.AddWithValue("@RangeTo", _FundFee.RangeTo);
                        cmd.Parameters.AddWithValue("@RangeFrom", _FundFee.RangeFrom);
                        cmd.Parameters.AddWithValue("@MiFeeAmount", _FundFee.MiFeeAmount);
                        cmd.Parameters.AddWithValue("@MiFeePercent", _FundFee.MiFeePercent);
                        cmd.Parameters.AddWithValue("@UpdateUsersID", _FundFee.EntryUsersID);
                        cmd.Parameters.AddWithValue("@UpdateTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    return _host.Get_LastPKByLastUpate(_datetimeNow, "FundFee");
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public void RejectDataBySelected(string param1, string param2)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update FundFeeSetup set Status = 3, VoidUsersID = @VoidUsersID, VoidTime = @VoidTime, LastUpdate = @LastUpdate " +
                            "where Selected = 1";
                        cmd.Parameters.AddWithValue("@VoidUsersID", param1);
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


        public void RejectedDataFundFeeSetupBySelected(string _usersID, string param2, int _fundPK)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update FundFeeSetup set Status = 3, VoidUsersID = @VoidUsersID, VoidTime = @VoidTime, LastUpdate = @LastUpdate " +
                            "where Selected = 1 and FundPK = @FundPK and status <> 3 "; 
                        cmd.Parameters.AddWithValue("@VoidUsersID", _usersID);
                        cmd.Parameters.AddWithValue("@FundPK", _fundPK);
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

       

        public bool CheckHassAddCopy(int _pk, string _date)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    string date = System.DateTime.Today.ToString("MM/dd/yyyy");
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "select * from FundFeeSetup where FundPK = @PK and Status in (1,2) and Date = @Date";
                        cmd.Parameters.AddWithValue("@PK", _pk);
                        cmd.Parameters.AddWithValue("@Date", _date);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (!dr.HasRows)
                            {
                                return false;
                            }
                            else
                            {
                                return true;
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


        public int CopyFundFee(FundFee _FundFee, bool _havePrivillege)
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
                         Declare @MaxPK int
                            select @MaxPK = Max(FundFeeSetupPK) from FundFeeSetup
                            set @maxPK = isnull(@maxPK,0)

                        --CREATE TABLE #CopyFundFee
                        --(
                        --selected bit,
                        --RangeFrom decimal(12,0),
                        --RangeTo decimal(12,0),
                       -- DateAmortize datetime,
                        --MiFeeAmount decimal(12,0),
                       -- MiFeePercent decimal(12,0),
                       -- FeeType int,
                        --FundPK int,
                       -- DateFrom datetime,
                       -- DateCopy datetime
                       -- )
                       -- INSERT INTO #CopyFundFee
                       -- SELECT Selected, RangeFrom, RangeTo, DateAmortize, MiFeeAmount, MiFeePercent, FeeType,FundPK,@DateFrom DateFrom,@DateTo DateCopy from FundFeeSetup where FundPK = @FundPK and Date = @DateFrom 
                       -- GROUP BY Selected,RangeFrom,RangeTo,DateAmortize,MiFeeAmount,MiFeePercent,FeeType,FundPK

                        INSERT INTO FundFeeSetup(FundFeeSetupPK,HistoryPK,Status,FundPK,Date,DateAmortize,RangeFrom,RangeTo,MiFeeAmount,MiFeePercent,FeeType,LastUpdate,LastUpdateDB,EntryUsersID,EntryTime,ApprovedUsersID,ApprovedTime,UpdateUsersID,UpdateTime) 
                        Select @MaxPK + ROW_NUMBER() OVER(ORDER BY A.FundFeeSetupPK ASC) FundFeeSetupPK,1,2,A.FundPK,@DateTo,A.DateAmortize,A.RangeFrom,A.RangeTo,A.MiFeeAmount,A.MiFeePercent,A.FeeType,@LastUpdate,@LastUpdate,@userID,@UpdateTime,@userID,@UpdateTime,@userID,@UpdateTime from FundFeeSetup A left JOIN FundFeeSetup B on A.FundPK = B.FundPK where A.FundPK = @FundPK and A.Date = @DateFrom
                        GROUP BY A.FundFeeSetupPK,A.Selected,A.RangeFrom,A.RangeTo,A.DateAmortize,A.MiFeeAmount,A.MiFeePercent,A.FeeType,A.FundPK

                        delete FundFeeSetup where FundPK IS NULL";

                        cmd.Parameters.AddWithValue("@FundPK", _FundFee.FundPK);
                        cmd.Parameters.AddWithValue("@DateFrom", _FundFee.Date);
                        cmd.Parameters.AddWithValue("@DateTo", _FundFee.ValueDateCopy);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.Parameters.AddWithValue("@UpdateTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@userID", _FundFee.EntryUsersID);

                        cmd.ExecuteNonQuery();
                    }
                    return _host.Get_LastPKByLastUpate(_datetimeNow, "FundFee");
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }


        public bool CheckMaxValue(CheckRangeTo _FundFee)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    if (_FundFee.FeeType == 1 || _FundFee.FeeType == 2)
                    {
                        int _RangeTo = 0;
                        string _range = "";
                        if (_FundFee.RangeTo == true)
                        {
                            _RangeTo = 999;
                            _range = " and RangeTo >" + _RangeTo;

                        }
                        else
                        {
                            _RangeTo = 99999;
                            _range = " and RangeTo >" + _RangeTo;
                        }
                        DbCon.Open();
                        string date = System.DateTime.Today.ToString("MM/dd/yyyy");
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "select * from FundFeeSetup where FundPK = @PK and Status in (1,2) and Date = @Date" + _range + @" and FeeType = " + _FundFee.FeeType;
                            cmd.Parameters.AddWithValue("@PK", _FundFee.FundPK);
                            cmd.Parameters.AddWithValue("@Date", _FundFee.Date);
                            cmd.Parameters.AddWithValue("@RangeTo", _RangeTo);

                            using (SqlDataReader dr = cmd.ExecuteReader())
                            {
                                if (!dr.HasRows)
                                {
                                    return false;
                                }
                                else
                                {
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
            catch (Exception err)
            {
                throw err;
            }
        }



        public string Add_Validate(FundFee _fundFee)
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
                        declare @FeeTypeDesc nvarchar(50)

                        select @FeeTypeDesc = DescOne from MasterValue where ID = 'FundFeeType' and status = 2 and Code = @FeeType


                        declare @FundFeeSetup table
                        (
                        FundPK int,
                        Date datetime,
                        RangeFrom numeric(22,0),
                        RangeTo numeric(22,0),
                        FeeType int
                        )


                        insert into @FundFeeSetup
                        select FundPK,Date,RangeFrom,RangeTo,FeeType from FundFeeSetup 
                        where status = 2 and Date = @Date and FundPK = @FundPK


                        IF @FeeType not in (2,3)
                        BEGIN
	                        IF EXISTS(select * from @FundFeeSetup)
	                        BEGIN
		                        select 'Fee Type : ' + @FeeTypeDesc + ', Cannot Insert Another Data in this days !' Result
	                        END
                            ELSE
                            BEGIN
                                select 'FALSE' Result
                            END
                        END
                        ELSE
                        BEGIN
	                        IF NOT EXISTS(select * from @FundFeeSetup where FeeType <> @FeeType)
	                        BEGIN
		                        IF EXISTS(
		                        SELECT * FROM FundFeeSetup 
		                        WHERE (@RangeFrom BETWEEN RangeFrom AND RangeTo 
			                        OR @RangeTo BETWEEN RangeFrom AND RangeTo
			                        OR RangeTo BETWEEN @RangeTo AND @RangeFrom
			                        OR RangeFrom BETWEEN @RangeFrom AND @RangeTo) and FundPK = @FundPK and Date = @Date and FeeType = @FeeType and status = 2
		                        )
		                        BEGIN
			                        select 'Fee Type : ' + @FeeTypeDesc + ', Cannot Insert Data, Please Check Range From and To !' Result
		                        END
		                        ELSE
		                        BEGIN
			                        select 'FALSE' Result
		                        END
	                        END
	                        ELSE
	                        BEGIN
		                        select 'Fee Type : ' + @FeeTypeDesc + ', Cannot Insert Another Type in this days !' Result
	                        END
                        END
                         ";

                        cmd.Parameters.AddWithValue("@Date", _fundFee.Date);
                        cmd.Parameters.AddWithValue("@FundPK", _fundFee.FundPK);
                        cmd.Parameters.AddWithValue("@FeeType", _fundFee.FeeType);
                        cmd.Parameters.AddWithValue("@RangeFrom", _fundFee.RangeFrom);
                        cmd.Parameters.AddWithValue("@RangeTo", _fundFee.RangeTo);

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

    }
}