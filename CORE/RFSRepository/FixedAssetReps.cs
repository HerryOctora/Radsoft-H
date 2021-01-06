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
using OfficeOpenXml.Drawing;

namespace RFSRepository
{
    public class FixedAssetReps
    {
        Host _host = new Host();

        //1
        string _insertCommand = "INSERT INTO [dbo].[FixedAsset] " +
                            "([FixedAssetPK],[HistoryPK],[Status],[PeriodPK],[BuyValueDate],[BuyJournalNo],[BuyReference],[BuyDescription],[FixedAssetAccount], " +
                            " [AccountBuyCredit],[BuyAmount],[BitWithPPNBuy],[BitWithPPNSell],[AmountPPNBuy],[AmountPPNSell],[DepreciationMode],[JournalInterval],[IntervalSpecificDate], " +
                            " [DepreciationPeriod],[PeriodUnit],[DepreciationExpAccount],[AccumulatedDeprAccount],[OfficePK],[SellValueDate], " +
                            " [SellReference],[SellDescription],[SellJournalNo],[SellAmount],[AccountSellDebit],[BitSold],[BuyDebitCurrencyPK], " +
                            " [BuyCreditCurrencyPK],[BuyDebitRate],[BuyCreditRate],[SellDebitCurrencyPK],[SellCreditCurrencyPK],[SellDebitRate],[SellCreditRate], " +
                            " [DepartmentPK],[TypeOfAssetsPK],[Location],[ConsigneePK],[ResiduAmount],[VATPercent], ";

        string _paramaterCommand = "@PeriodPK,@BuyValueDate,@BuyJournalNo,@BuyReference,@BuyDescription,@FixedAssetAccount, " +
                            " @AccountBuyCredit,@BuyAmount,@BitWithPPNBuy,@BitWithPPNSell,@AmountPPNBuy,@AmountPPNSell,@DepreciationMode,@JournalInterval,@IntervalSpecificDate, " +
                            " @DepreciationPeriod,@PeriodUnit,@DepreciationExpAccount,@AccumulatedDeprAccount,@OfficePK,@SellValueDate, " +
                            " @SellReference,@SellDescription,@SellJournalNo,@SellAmount,@AccountSellDebit,@BitSold,@BuyDebitCurrencyPK, " +
                            " @BuyCreditCurrencyPK,@BuyDebitRate,@BuyCreditRate,@SellDebitCurrencyPK,@SellCreditCurrencyPK,@SellDebitRate,@SellCreditRate, " +
                            " @DepartmentPK,@TypeOfAssetsPK,@Location,@ConsigneePK,@ResiduAmount,@VATPercent, ";
        private FixedAsset setFixedAsset(SqlDataReader dr)
        {
            FixedAsset M_FixedAsset = new FixedAsset();
            M_FixedAsset.FixedAssetPK = Convert.ToInt32(dr["FixedAssetPK"]);
            M_FixedAsset.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_FixedAsset.Status = Convert.ToInt32(dr["Status"]);
            M_FixedAsset.Notes = Convert.ToString(dr["Notes"]);
            M_FixedAsset.PeriodPK = Convert.ToInt32(dr["PeriodPK"]);
            M_FixedAsset.PeriodID = Convert.ToString(dr["PeriodID"]);
            M_FixedAsset.BuyValueDate = dr["BuyValueDate"].ToString();
            M_FixedAsset.BuyJournalNo = Convert.ToInt32(dr["BuyJournalNo"]);
            M_FixedAsset.BuyReference = Convert.ToString(dr["BuyReference"]);
            M_FixedAsset.BuyDescription = Convert.ToString(dr["BuyDescription"]);
            M_FixedAsset.FixedAssetAccount = Convert.ToInt32(dr["FixedAssetAccount"]);
            M_FixedAsset.FixedAssetAccountID = Convert.ToString(dr["FixedAssetAccountID"]);
            M_FixedAsset.AccountBuyCredit = Convert.ToInt32(dr["AccountBuyCredit"]);
            M_FixedAsset.AccountBuyCreditID = Convert.ToString(dr["AccountBuyCreditID"]);
            M_FixedAsset.BuyAmount = Convert.ToDecimal(dr["BuyAmount"]);
            M_FixedAsset.BitWithPPNBuy = Convert.ToInt32(dr["BitWithPPNBuy"]);
            M_FixedAsset.BitWithPPNSell = Convert.ToInt32(dr["BitWithPPNSell"]);
            M_FixedAsset.AmountPPNBuy = Convert.ToDecimal(dr["AmountPPNBuy"]);
            M_FixedAsset.AmountPPNSell = Convert.ToDecimal(dr["AmountPPNSell"]);
            M_FixedAsset.DepreciationMode = Convert.ToInt32(dr["DepreciationMode"]);
            M_FixedAsset.DepreciationModeDesc = Convert.ToString(dr["DepreciationModeDesc"]);
            M_FixedAsset.JournalInterval = Convert.ToInt32(dr["JournalInterval"]);
            M_FixedAsset.JournalIntervalDesc = Convert.ToString(dr["JournalIntervalDesc"]);
            M_FixedAsset.IntervalSpecificDate = Convert.ToString(dr["IntervalSpecificDate"]);
            M_FixedAsset.DepreciationPeriod = Convert.ToInt32(dr["DepreciationPeriod"]);
            M_FixedAsset.PeriodUnit = Convert.ToString(dr["PeriodUnit"]);
            M_FixedAsset.PeriodUnitDesc = Convert.ToString(dr["PeriodUnitDesc"]);
            M_FixedAsset.DepreciationExpAccount = Convert.ToInt32(dr["DepreciationExpAccount"]);
            M_FixedAsset.DepreciationExpAccountID = Convert.ToString(dr["DepreciationExpAccountID"]);
            M_FixedAsset.AccumulatedDeprAccount = Convert.ToInt32(dr["AccumulatedDeprAccount"]);
            M_FixedAsset.AccumulatedDeprAccountID = Convert.ToString(dr["AccumulatedDeprAccountID"]);
            M_FixedAsset.OfficePK = Convert.ToInt32(dr["OfficePK"]);
            M_FixedAsset.OfficeID = Convert.ToString(dr["OfficeID"]);
            M_FixedAsset.SellValueDate = Convert.ToString(dr["SellValueDate"]);
            M_FixedAsset.SellReference = Convert.ToString(dr["SellReference"]);
            M_FixedAsset.SellDescription = Convert.ToString(dr["SellDescription"]);
            M_FixedAsset.SellJournalNo = Convert.ToInt32(dr["SellJournalNo"]);
            M_FixedAsset.SellAmount = Convert.ToDecimal(dr["SellAmount"]);
            M_FixedAsset.AccountSellDebit = Convert.ToInt32(dr["AccountSellDebit"]);
            M_FixedAsset.AccountSellDebitID = Convert.ToString(dr["AccountSellDebitID"]);
            M_FixedAsset.BitSold = Convert.ToBoolean(dr["BitSold"]);
            M_FixedAsset.BuyDebitCurrencyPK = Convert.ToInt32(dr["BuyDebitCurrencyPK"]);
            M_FixedAsset.BuyDebitCurrencyID = Convert.ToString(dr["BuyDebitCurrencyID"]);
            M_FixedAsset.BuyCreditCurrencyPK = Convert.ToInt32(dr["BuyCreditCurrencyPK"]);
            M_FixedAsset.BuyCreditCurrencyID = Convert.ToString(dr["BuyCreditCurrencyID"]);
            M_FixedAsset.BuyDebitRate = Convert.ToInt32(dr["BuyDebitRate"]);
            M_FixedAsset.BuyCreditRate = Convert.ToInt32(dr["BuyCreditRate"]);
            M_FixedAsset.SellDebitCurrencyPK = Convert.ToInt32(dr["SellDebitCurrencyPK"]);
            M_FixedAsset.SellDebitCurrencyID = Convert.ToString(dr["SellDebitCurrencyID"]);
            M_FixedAsset.SellCreditCurrencyPK = Convert.ToInt32(dr["SellCreditCurrencyPK"]);
            M_FixedAsset.SellCreditCurrencyID = Convert.ToString(dr["SellCreditCurrencyID"]);
            M_FixedAsset.SellDebitRate = Convert.ToInt32(dr["SellDebitRate"]);
            M_FixedAsset.SellCreditRate = Convert.ToInt32(dr["SellCreditRate"]);
            M_FixedAsset.DepartmentPK = Convert.ToInt32(dr["DepartmentPK"]);
            M_FixedAsset.DepartmentID = Convert.ToString(dr["DepartmentID"]);
            M_FixedAsset.ConsigneePK = Convert.ToInt32(dr["ConsigneePK"]);
            M_FixedAsset.ConsigneeID = Convert.ToString(dr["ConsigneeID"]);
            M_FixedAsset.TypeOfAssetsPK = Convert.ToInt32(dr["TypeOfAssetsPK"]);
            M_FixedAsset.TypeOfAssetsID = Convert.ToString(dr["TypeOfAssetsID"]);
            M_FixedAsset.ResiduAmount = Convert.ToDecimal(dr["ResiduAmount"]);
            M_FixedAsset.Location = Convert.ToString(dr["Location"]);
            M_FixedAsset.VATPercent = Convert.ToDecimal(dr["VATPercent"]);
            M_FixedAsset.Posted = Convert.ToBoolean(dr["Posted"]);
            M_FixedAsset.PostedBy = Convert.ToString(dr["PostedBy"]);
            M_FixedAsset.PostedTime = Convert.ToString(dr["PostedTime"]);
            M_FixedAsset.Revised = Convert.ToBoolean(dr["Revised"]);
            M_FixedAsset.RevisedBy = Convert.ToString(dr["RevisedBy"]);
            M_FixedAsset.RevisedTime = Convert.ToString(dr["RevisedTime"]);
            M_FixedAsset.EntryUsersID = dr["EntryUsersID"].ToString();
            M_FixedAsset.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_FixedAsset.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_FixedAsset.VoidUsersID = dr["VoidUsersID"].ToString();
            M_FixedAsset.EntryTime = dr["EntryTime"].ToString();
            M_FixedAsset.UpdateTime = dr["UpdateTime"].ToString();
            M_FixedAsset.ApprovedTime = dr["ApprovedTime"].ToString();
            M_FixedAsset.VoidTime = dr["VoidTime"].ToString();
            M_FixedAsset.DBUserID = dr["DBUserID"].ToString();
            M_FixedAsset.DBTerminalID = dr["DBTerminalID"].ToString();
            M_FixedAsset.LastUpdate = dr["LastUpdate"].ToString();
            M_FixedAsset.LastUpdateDB = dr["LastUpdateDB"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["LastUpdateDB"]);
            return M_FixedAsset;
        }
        public List<FixedAsset> FixedAsset_Select(int _status)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<FixedAsset> L_FixedAsset = new List<FixedAsset>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText =
                            " select P.ID PeriodID,CR1.ID AccountBuyCreditID,C1.ID BuyDebitCurrencyID,C2.ID BuyCreditCurrencyID,C3.ID SellDebitCurrencyID,C4.ID SellCreditCurrencyID, " +
                            " O.ID OfficeID, CR2.ID AccountSellDebitID,MV1.DescOne DepreciationModeDesc, MV2.DescOne JournalIntervalDesc, " +
                            " MV3.DescOne PeriodUnitDesc,A1.ID FixedAssetAccountID,A2.ID DepreciationExpAccountID,A3.ID AccumulatedDeprAccountID ,* from FixedAsset FA Left join " +
                            " Period P on FA.PeriodPK = P.ID and P.Status = 2  Left join  " +
                            " Account CR1 on FA.AccountBuyCredit = CR1.AccountPK and CR1.Status = 2  Left join  " +
                            " Currency C1 on FA.BuyDebitCurrencyPK = C1.CurrencyPK and C1.Status = 2  Left join  " +
                            " Currency C2 on FA.BuyCreditCurrencyPK = C2.CurrencyPK and C2.Status = 2  Left join  " +
                            " Currency C3 on FA.SellDebitCurrencyPK = C3.CurrencyPK and C3.Status = 2  Left join  " +
                            " Currency C4 on FA.SellCreditCurrencyPK = C4.CurrencyPK and C4.Status = 2  Left join  " +
                            " Office O on FA.OfficePK = O.OfficePK and O.Status = 2  Left join  " +
                            " Account CR2 on FA.AccountSellDebit = CR2.AccountPK and CR2.Status = 2  Left join  " +
                            " Account A1 on FA.FixedAssetAccount = A1.AccountPK and A1.Status = 2  Left join  " +
                            " Account A2 on FA.DepreciationExpAccount = A2.AccountPK and A2.Status = 2  Left join  " +
                            " Account A3 on FA.AccumulatedDeprAccount = A3.AccountPK and A3.Status = 2  Left join  " +
                            " MasterValue MV1 on FA.DepreciationMode = MV1.Code and MV1.Status = 2 and MV1.ID = 'DepreciationMode' Left join  " +
                            " MasterValue MV2 on FA.JournalInterval = MV2.Code and MV2.Status = 2 and MV2.ID = 'JournalInterval' Left join  " +
                            " MasterValue MV3 on FA.PeriodUnit = MV3.Code and MV3.Status = 2 and MV3.ID = 'PeriodUnit'  " +
                            " Where FA.Status = @status ";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = " select P.ID PeriodID,CR1.ID AccountBuyCreditID,C1.ID BuyDebitCurrencyID,C2.ID BuyCreditCurrencyID,C3.ID SellDebitCurrencyID,C4.ID SellCreditCurrencyID, " +
                            " O.ID OfficeID, CR2.ID AccountSellDebitID,MV1.DescOne DepreciationModeDesc, MV2.DescOne JournalIntervalDesc, " +
                            " MV3.DescOne PeriodUnitDesc,A1.ID FixedAssetAccountID,A2.ID DepreciationExpAccountID,A3.ID AccumulatedDeprAccountID ,* from FixedAsset FA Left join " +
                            " Period P on FA.PeriodPK = P.ID and P.Status = 2  Left join  " +
                            " Account CR1 on FA.AccountBuyCredit = CR1.AccountPK and CR1.Status = 2  Left join  " +
                            " Currency C1 on FA.BuyDebitCurrencyPK = C1.CurrencyPK and C1.Status = 2  Left join  " +
                            " Currency C2 on FA.BuyCreditCurrencyPK = C2.CurrencyPK and C2.Status = 2  Left join  " +
                            " Currency C3 on FA.SellDebitCurrencyPK = C3.CurrencyPK and C3.Status = 2  Left join  " +
                            " Currency C4 on FA.SellCreditCurrencyPK = C4.CurrencyPK and C4.Status = 2  Left join  " +
                            " Office O on FA.OfficePK = O.OfficePK and O.Status = 2  Left join  " +
                            " Account CR2 on FA.AccountSellDebit = CR2.AccountPK and CR2.Status = 2  Left join  " +
                            " Account A1 on FA.FixedAssetAccount = A1.AccountPK and A1.Status = 2  Left join  " +
                            " Account A2 on FA.DepreciationExpAccount = A2.AccountPK and A2.Status = 2  Left join  " +
                            " Account A3 on FA.AccumulatedDeprAccount = A3.AccountPK and A3.Status = 2  Left join  " +
                            " MasterValue MV1 on FA.DepreciationMode = MV1.Code and MV1.Status = 2 and MV1.ID = 'DepreciationMode' Left join  " +
                            " MasterValue MV2 on FA.JournalInterval = MV2.Code and MV2.Status = 2 and MV2.ID = 'JournalInterval' Left join  " +
                            " MasterValue MV3 on FA.PeriodUnit = MV3.Code and MV3.Status = 2 and MV3.ID = 'PeriodUnit'  ";
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_FixedAsset.Add(setFixedAsset(dr));
                                }
                            }
                            return L_FixedAsset;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }
        public int FixedAsset_Add(FixedAsset _FixedAsset, bool _havePrivillege)
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
                                 "Select isnull(max(FixedAssetPK),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@lastupdate from FixedAsset";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _FixedAsset.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],lastupdate)" +
                                "Select isnull(max(FixedAssetPK),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@lastupdate from FixedAsset";
                        }

                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@PeriodPK", _FixedAsset.PeriodPK);
                        cmd.Parameters.AddWithValue("@BuyValueDate", _FixedAsset.BuyValueDate);
                        cmd.Parameters.AddWithValue("@BuyJournalNo", _FixedAsset.BuyJournalNo);
                        cmd.Parameters.AddWithValue("@BuyReference", _FixedAsset.BuyReference);
                        cmd.Parameters.AddWithValue("@BuyDescription", _FixedAsset.BuyDescription);
                        cmd.Parameters.AddWithValue("@FixedAssetAccount", _FixedAsset.FixedAssetAccount);
                        cmd.Parameters.AddWithValue("@AccountBuyCredit", _FixedAsset.AccountBuyCredit);
                        cmd.Parameters.AddWithValue("@BuyAmount", _FixedAsset.BuyAmount);
                        cmd.Parameters.AddWithValue("@BitWithPPNBuy", _FixedAsset.BitWithPPNBuy);
                        cmd.Parameters.AddWithValue("@BitWithPPNSell", _FixedAsset.BitWithPPNSell);
                        cmd.Parameters.AddWithValue("@AmountPPNBuy", _FixedAsset.AmountPPNBuy);
                        cmd.Parameters.AddWithValue("@AmountPPNSell", _FixedAsset.AmountPPNSell);
                        cmd.Parameters.AddWithValue("@DepreciationMode", _FixedAsset.DepreciationMode);
                        cmd.Parameters.AddWithValue("@JournalInterval", _FixedAsset.JournalInterval);
                        cmd.Parameters.AddWithValue("@IntervalSpecificDate", _FixedAsset.IntervalSpecificDate);
                        cmd.Parameters.AddWithValue("@DepreciationPeriod", _FixedAsset.DepreciationPeriod);
                        cmd.Parameters.AddWithValue("@PeriodUnit", _FixedAsset.PeriodUnit);
                        cmd.Parameters.AddWithValue("@DepreciationExpAccount", _FixedAsset.DepreciationExpAccount);
                        cmd.Parameters.AddWithValue("@AccumulatedDeprAccount", _FixedAsset.AccumulatedDeprAccount);
                        cmd.Parameters.AddWithValue("@OfficePK", _FixedAsset.OfficePK);
                        cmd.Parameters.AddWithValue("@SellValueDate", _FixedAsset.SellValueDate);
                        cmd.Parameters.AddWithValue("@SellReference", _FixedAsset.SellReference);
                        cmd.Parameters.AddWithValue("@SellDescription", _FixedAsset.SellDescription);
                        cmd.Parameters.AddWithValue("@SellJournalNo", _FixedAsset.SellJournalNo);
                        cmd.Parameters.AddWithValue("@SellAmount", _FixedAsset.SellAmount);
                        cmd.Parameters.AddWithValue("@AccountSellDebit", _FixedAsset.AccountSellDebit);
                        cmd.Parameters.AddWithValue("@BitSold", _FixedAsset.BitSold);
                        cmd.Parameters.AddWithValue("@BuyDebitCurrencyPK", _FixedAsset.BuyDebitCurrencyPK);
                        cmd.Parameters.AddWithValue("@BuyCreditCurrencyPK", _FixedAsset.BuyCreditCurrencyPK);
                        cmd.Parameters.AddWithValue("@BuyDebitRate", _FixedAsset.BuyDebitRate);
                        cmd.Parameters.AddWithValue("@BuyCreditRate", _FixedAsset.BuyCreditRate);
                        cmd.Parameters.AddWithValue("@SellDebitCurrencyPK", _FixedAsset.SellDebitCurrencyPK);
                        cmd.Parameters.AddWithValue("@SellCreditCurrencyPK", _FixedAsset.SellCreditCurrencyPK);
                        cmd.Parameters.AddWithValue("@SellDebitRate", _FixedAsset.SellDebitRate);
                        cmd.Parameters.AddWithValue("@SellCreditRate", _FixedAsset.SellCreditRate);
                        cmd.Parameters.AddWithValue("@DepartmentPK", _FixedAsset.DepartmentPK);
                        cmd.Parameters.AddWithValue("@TypeOfAssetsPK", _FixedAsset.TypeOfAssetsPK);
                        cmd.Parameters.AddWithValue("@Location", _FixedAsset.Location);
                        cmd.Parameters.AddWithValue("@ConsigneePK", _FixedAsset.ConsigneePK);
                        cmd.Parameters.AddWithValue("@ResiduAmount", _FixedAsset.ResiduAmount);
                        cmd.Parameters.AddWithValue("@VATPercent", _FixedAsset.VATPercent);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _FixedAsset.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();

                        return _host.Get_LastPKByLastUpate(_datetimeNow, "FixedAsset");
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }
        public int FixedAsset_Update(FixedAsset _FixedAsset, bool _havePrivillege)
        {

            try
            {
                int _newHisPK;
                int status = _host.Get_Status(_FixedAsset.FixedAssetPK, _FixedAsset.HistoryPK, "FixedAsset");
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update FixedAsset set status=2, Notes=@Notes,PeriodPK=@PeriodPK,BuyValueDate=@BuyValueDate,BuyJournalNo=@BuyJournalNo," +
                                "BuyReference=@BuyReference,BuyDescription=@BuyDescription,FixedAssetAccount=@FixedAssetAccount,AccountBuyCredit=@AccountBuyCredit, " +
                                "BuyAmount=@BuyAmount,BitWithPPNBuy=@BitWithPPNBuy,BitWithPPNSell=@BitWithPPNSell,AmountPPNBuy=@AmountPPNBuy,AmountPPNSell=@AmountPPNSell,DepreciationMode=@DepreciationMode,JournalInterval=@JournalInterval, " +
                                "IntervalSpecificDate=@IntervalSpecificDate,DepreciationPeriod=@DepreciationPeriod,PeriodUnit=@PeriodUnit,DepreciationExpAccount=@DepreciationExpAccount, " +
                                "AccumulatedDeprAccount=@AccumulatedDeprAccount,OfficePK=@OfficePK,SellValueDate=@SellValueDate, " +
                                "SellReference=@SellReference,SellDescription=@SellDescription,SellJournalNo=@SellJournalNo,SellAmount=@SellAmount,AccountSellDebit=@AccountSellDebit,BitSold=@BitSold, " +
                                "BuyDebitCurrencyPK=@BuyDebitCurrencyPK,BuyCreditCurrencyPK=@BuyCreditCurrencyPK,BuyDebitRate=@BuyDebitRate,BuyCreditRate=@BuyCreditRate, " +
                                "SellDebitCurrencyPK=@SellDebitCurrencyPK,SellCreditCurrencyPK=@SellCreditCurrencyPK,SellDebitRate=@SellDebitRate,SellCreditRate=@SellCreditRate, DepartmentPK=@DepartmentPK,TypeOfAssetsPK=@TypeOfAssetsPK,Location=@Location,ConsigneePK=@ConsigneePK,ResiduAmount=@ResiduAmount,VATPercent=@VATPercent, " +
                                "ApprovedUsersID=@ApprovedUsersID, " +
                                "ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime ,LastUpdate=@lastupdate " +
                                "where FixedAssetPK = @PK and historyPK = @HistoryPK";

                            cmd.Parameters.AddWithValue("@HistoryPK", _FixedAsset.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _FixedAsset.FixedAssetPK);
                            cmd.Parameters.AddWithValue("@Notes", _FixedAsset.Notes);
                            cmd.Parameters.AddWithValue("@PeriodPK", _FixedAsset.PeriodPK);
                            cmd.Parameters.AddWithValue("@BuyValueDate", _FixedAsset.BuyValueDate);
                            cmd.Parameters.AddWithValue("@BuyJournalNo", _FixedAsset.BuyJournalNo);
                            cmd.Parameters.AddWithValue("@BuyReference", _FixedAsset.BuyReference);
                            cmd.Parameters.AddWithValue("@BuyDescription", _FixedAsset.BuyDescription);
                            cmd.Parameters.AddWithValue("@FixedAssetAccount", _FixedAsset.FixedAssetAccount);
                            cmd.Parameters.AddWithValue("@AccountBuyCredit", _FixedAsset.AccountBuyCredit);
                            cmd.Parameters.AddWithValue("@BuyAmount", _FixedAsset.BuyAmount);
                            cmd.Parameters.AddWithValue("@BitWithPPNBuy", _FixedAsset.BitWithPPNBuy);
                            cmd.Parameters.AddWithValue("@BitWithPPNSell", _FixedAsset.BitWithPPNSell);
                            cmd.Parameters.AddWithValue("@AmountPPNBuy", _FixedAsset.AmountPPNBuy);
                            cmd.Parameters.AddWithValue("@AmountPPNSell", _FixedAsset.AmountPPNSell);
                            cmd.Parameters.AddWithValue("@DepreciationMode", _FixedAsset.DepreciationMode);
                            cmd.Parameters.AddWithValue("@JournalInterval", _FixedAsset.JournalInterval);
                            cmd.Parameters.AddWithValue("@IntervalSpecificDate", _FixedAsset.IntervalSpecificDate);
                            cmd.Parameters.AddWithValue("@DepreciationPeriod", _FixedAsset.DepreciationPeriod);
                            cmd.Parameters.AddWithValue("@PeriodUnit", _FixedAsset.PeriodUnit);
                            cmd.Parameters.AddWithValue("@DepreciationExpAccount", _FixedAsset.DepreciationExpAccount);
                            cmd.Parameters.AddWithValue("@AccumulatedDeprAccount", _FixedAsset.AccumulatedDeprAccount);
                            cmd.Parameters.AddWithValue("@OfficePK", _FixedAsset.OfficePK);
                            cmd.Parameters.AddWithValue("@SellValueDate", _FixedAsset.SellValueDate);
                            cmd.Parameters.AddWithValue("@SellReference", _FixedAsset.SellReference);
                            cmd.Parameters.AddWithValue("@SellDescription", _FixedAsset.SellDescription);
                            cmd.Parameters.AddWithValue("@SellJournalNo", _FixedAsset.SellJournalNo);
                            cmd.Parameters.AddWithValue("@SellAmount", _FixedAsset.SellAmount);
                            cmd.Parameters.AddWithValue("@AccountSellDebit", _FixedAsset.AccountSellDebit);
                            cmd.Parameters.AddWithValue("@BitSold", _FixedAsset.BitSold);
                            cmd.Parameters.AddWithValue("@BuyDebitCurrencyPK", _FixedAsset.BuyDebitCurrencyPK);
                            cmd.Parameters.AddWithValue("@BuyCreditCurrencyPK", _FixedAsset.BuyCreditCurrencyPK);
                            cmd.Parameters.AddWithValue("@BuyDebitRate", _FixedAsset.BuyDebitRate);
                            cmd.Parameters.AddWithValue("@BuyCreditRate", _FixedAsset.BuyCreditRate);
                            cmd.Parameters.AddWithValue("@SellDebitCurrencyPK", _FixedAsset.SellDebitCurrencyPK);
                            cmd.Parameters.AddWithValue("@SellCreditCurrencyPK", _FixedAsset.SellCreditCurrencyPK);
                            cmd.Parameters.AddWithValue("@SellDebitRate", _FixedAsset.SellDebitRate);
                            cmd.Parameters.AddWithValue("@SellCreditRate", _FixedAsset.SellCreditRate);
                            cmd.Parameters.AddWithValue("@DepartmentPK", _FixedAsset.DepartmentPK);
                            cmd.Parameters.AddWithValue("@TypeOfAssetsPK", _FixedAsset.TypeOfAssetsPK);
                            cmd.Parameters.AddWithValue("@Location", _FixedAsset.Location);
                            cmd.Parameters.AddWithValue("@ConsigneePK", _FixedAsset.ConsigneePK);
                            cmd.Parameters.AddWithValue("@ResiduAmount", _FixedAsset.ResiduAmount);
                            cmd.Parameters.AddWithValue("@VATPercent", _FixedAsset.VATPercent);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _FixedAsset.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _FixedAsset.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                            cmd.ExecuteNonQuery();
                        }

                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update FixedAsset set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@lastupdate where FixedAssetPK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _FixedAsset.FixedAssetPK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _FixedAsset.EntryUsersID);
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
                                cmd.CommandText = "Update FixedAsset set Notes=@Notes,PeriodPK=@PeriodPK,BuyValueDate=@BuyValueDate,BuyJournalNo=@BuyJournalNo," +
                                    "BuyReference=@BuyReference,BuyDescription=@BuyDescription,FixedAssetAccount=@FixedAssetAccount,AccountBuyCredit=@AccountBuyCredit, " +
                                    "BuyAmount=@BuyAmount,BitWithPPNBuy=@BitWithPPNBuy,BitWithPPNSell=@BitWithPPNSell,AmountPPNBuy=@AmountPPNBuy,AmountPPNSell=@AmountPPNSell,DepreciationMode=@DepreciationMode,JournalInterval=@JournalInterval, " +
                                    "IntervalSpecificDate=@IntervalSpecificDate,DepreciationPeriod=@DepreciationPeriod,PeriodUnit=@PeriodUnit,DepreciationExpAccount=@DepreciationExpAccount, " +
                                    "AccumulatedDeprAccount=@AccumulatedDeprAccount,OfficePK=@OfficePK,SellValueDate=@SellValueDate, " +
                                    "SellReference=@SellReference,SellDescription=@SellDescription,SellJournalNo=@SellJournalNo,SellAmount=@SellAmount,AccountSellDebit=@AccountSellDebit,BitSold=@BitSold, " +
                                    "BuyDebitCurrencyPK=@BuyDebitCurrencyPK,BuyCreditCurrencyPK=@BuyCreditCurrencyPK,BuyDebitRate=@BuyDebitRate,BuyCreditRate=@BuyCreditRate, " +
                                    "SellDebitCurrencyPK=@SellDebitCurrencyPK,SellCreditCurrencyPK=@SellCreditCurrencyPK,SellDebitRate=@SellDebitRate,SellCreditRate=@SellCreditRate, DepartmentPK=@DepartmentPK,TypeOfAssetsPK=@TypeOfAssetsPK,Location=@Location,ConsigneePK=@ConsigneePK,ResiduAmount=@ResiduAmount,VATPercent=@VATPercent," +
                                    "UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@lastupdate " +
                                    "where FixedAssetPK = @PK and historyPK = @HistoryPK";

                                cmd.Parameters.AddWithValue("@HistoryPK", _FixedAsset.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _FixedAsset.FixedAssetPK);
                                cmd.Parameters.AddWithValue("@Notes", _FixedAsset.Notes);
                                cmd.Parameters.AddWithValue("@PeriodPK", _FixedAsset.PeriodPK);
                                cmd.Parameters.AddWithValue("@BuyValueDate", _FixedAsset.BuyValueDate);
                                cmd.Parameters.AddWithValue("@BuyJournalNo", _FixedAsset.BuyJournalNo);
                                cmd.Parameters.AddWithValue("@BuyReference", _FixedAsset.BuyReference);
                                cmd.Parameters.AddWithValue("@BuyDescription", _FixedAsset.BuyDescription);
                                cmd.Parameters.AddWithValue("@FixedAssetAccount", _FixedAsset.FixedAssetAccount);
                                cmd.Parameters.AddWithValue("@AccountBuyCredit", _FixedAsset.AccountBuyCredit);
                                cmd.Parameters.AddWithValue("@BuyAmount", _FixedAsset.BuyAmount);
                                cmd.Parameters.AddWithValue("@BitWithPPNBuy", _FixedAsset.BitWithPPNBuy);
                                cmd.Parameters.AddWithValue("@BitWithPPNSell", _FixedAsset.BitWithPPNSell);
                                cmd.Parameters.AddWithValue("@AmountPPNBuy", _FixedAsset.AmountPPNBuy);
                                cmd.Parameters.AddWithValue("@AmountPPNSell", _FixedAsset.AmountPPNSell);
                                cmd.Parameters.AddWithValue("@DepreciationMode", _FixedAsset.DepreciationMode);
                                cmd.Parameters.AddWithValue("@JournalInterval", _FixedAsset.JournalInterval);
                                cmd.Parameters.AddWithValue("@IntervalSpecificDate", _FixedAsset.IntervalSpecificDate);
                                cmd.Parameters.AddWithValue("@DepreciationPeriod", _FixedAsset.DepreciationPeriod);
                                cmd.Parameters.AddWithValue("@PeriodUnit", _FixedAsset.PeriodUnit);
                                cmd.Parameters.AddWithValue("@DepreciationExpAccount", _FixedAsset.DepreciationExpAccount);
                                cmd.Parameters.AddWithValue("@AccumulatedDeprAccount", _FixedAsset.AccumulatedDeprAccount);
                                cmd.Parameters.AddWithValue("@OfficePK", _FixedAsset.OfficePK);
                                cmd.Parameters.AddWithValue("@SellValueDate", _FixedAsset.SellValueDate);
                                cmd.Parameters.AddWithValue("@SellReference", _FixedAsset.SellReference);
                                cmd.Parameters.AddWithValue("@SellDescription", _FixedAsset.SellDescription);
                                cmd.Parameters.AddWithValue("@SellJournalNo", _FixedAsset.SellJournalNo);
                                cmd.Parameters.AddWithValue("@SellAmount", _FixedAsset.SellAmount);
                                cmd.Parameters.AddWithValue("@AccountSellDebit", _FixedAsset.AccountSellDebit);
                                cmd.Parameters.AddWithValue("@BitSold", _FixedAsset.BitSold);
                                cmd.Parameters.AddWithValue("@BuyDebitCurrencyPK", _FixedAsset.BuyDebitCurrencyPK);
                                cmd.Parameters.AddWithValue("@BuyCreditCurrencyPK", _FixedAsset.BuyCreditCurrencyPK);
                                cmd.Parameters.AddWithValue("@BuyDebitRate", _FixedAsset.BuyDebitRate);
                                cmd.Parameters.AddWithValue("@BuyCreditRate", _FixedAsset.BuyCreditRate);
                                cmd.Parameters.AddWithValue("@SellDebitCurrencyPK", _FixedAsset.SellDebitCurrencyPK);
                                cmd.Parameters.AddWithValue("@SellCreditCurrencyPK", _FixedAsset.SellCreditCurrencyPK);
                                cmd.Parameters.AddWithValue("@SellDebitRate", _FixedAsset.SellDebitRate);
                                cmd.Parameters.AddWithValue("@SellCreditRate", _FixedAsset.SellCreditRate);
                                cmd.Parameters.AddWithValue("@DepartmentPK", _FixedAsset.DepartmentPK);
                                cmd.Parameters.AddWithValue("@TypeOfAssetsPK", _FixedAsset.TypeOfAssetsPK);
                                cmd.Parameters.AddWithValue("@Location", _FixedAsset.Location);
                                cmd.Parameters.AddWithValue("@ConsigneePK", _FixedAsset.ConsigneePK);
                                cmd.Parameters.AddWithValue("@ResiduAmount", _FixedAsset.ResiduAmount);
                                cmd.Parameters.AddWithValue("@VATPercent", _FixedAsset.VATPercent);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _FixedAsset.EntryUsersID);
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
                                _newHisPK = _host.Get_NewHistoryPK(_FixedAsset.FixedAssetPK, "FixedAsset");
                                cmd.CommandText = _insertCommand + "[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From FixedAsset where FixedAssetPK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _FixedAsset.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _FixedAsset.FixedAssetPK);
                                cmd.Parameters.AddWithValue("@Notes", _FixedAsset.Notes);
                                cmd.Parameters.AddWithValue("@PeriodPK", _FixedAsset.PeriodPK);
                                cmd.Parameters.AddWithValue("@BuyValueDate", _FixedAsset.BuyValueDate);
                                cmd.Parameters.AddWithValue("@BuyJournalNo", _FixedAsset.BuyJournalNo);
                                cmd.Parameters.AddWithValue("@BuyReference", _FixedAsset.BuyReference);
                                cmd.Parameters.AddWithValue("@BuyDescription", _FixedAsset.BuyDescription);
                                cmd.Parameters.AddWithValue("@FixedAssetAccount", _FixedAsset.FixedAssetAccount);
                                cmd.Parameters.AddWithValue("@AccountBuyCredit", _FixedAsset.AccountBuyCredit);
                                cmd.Parameters.AddWithValue("@BuyAmount", _FixedAsset.BuyAmount);
                                cmd.Parameters.AddWithValue("@BitWithPPNBuy", _FixedAsset.BitWithPPNBuy);
                                cmd.Parameters.AddWithValue("@BitWithPPNSell", _FixedAsset.BitWithPPNSell);
                                cmd.Parameters.AddWithValue("@AmountPPNBuy", _FixedAsset.AmountPPNBuy);
                                cmd.Parameters.AddWithValue("@AmountPPNSell", _FixedAsset.AmountPPNSell);
                                cmd.Parameters.AddWithValue("@DepreciationMode", _FixedAsset.DepreciationMode);
                                cmd.Parameters.AddWithValue("@JournalInterval", _FixedAsset.JournalInterval);
                                cmd.Parameters.AddWithValue("@IntervalSpecificDate", _FixedAsset.IntervalSpecificDate);
                                cmd.Parameters.AddWithValue("@DepreciationPeriod", _FixedAsset.DepreciationPeriod);
                                cmd.Parameters.AddWithValue("@PeriodUnit", _FixedAsset.PeriodUnit);
                                cmd.Parameters.AddWithValue("@DepreciationExpAccount", _FixedAsset.DepreciationExpAccount);
                                cmd.Parameters.AddWithValue("@AccumulatedDeprAccount", _FixedAsset.AccumulatedDeprAccount);
                                cmd.Parameters.AddWithValue("@OfficePK", _FixedAsset.OfficePK);
                                cmd.Parameters.AddWithValue("@SellValueDate", _FixedAsset.SellValueDate);
                                cmd.Parameters.AddWithValue("@SellReference", _FixedAsset.SellReference);
                                cmd.Parameters.AddWithValue("@SellDescription", _FixedAsset.SellDescription);
                                cmd.Parameters.AddWithValue("@SellJournalNo", _FixedAsset.SellJournalNo);
                                cmd.Parameters.AddWithValue("@SellAmount", _FixedAsset.SellAmount);
                                cmd.Parameters.AddWithValue("@AccountSellDebit", _FixedAsset.AccountSellDebit);
                                cmd.Parameters.AddWithValue("@BitSold", _FixedAsset.BitSold);
                                cmd.Parameters.AddWithValue("@BuyDebitCurrencyPK", _FixedAsset.BuyDebitCurrencyPK);
                                cmd.Parameters.AddWithValue("@BuyCreditCurrencyPK", _FixedAsset.BuyCreditCurrencyPK);
                                cmd.Parameters.AddWithValue("@BuyDebitRate", _FixedAsset.BuyDebitRate);
                                cmd.Parameters.AddWithValue("@BuyCreditRate", _FixedAsset.BuyCreditRate);
                                cmd.Parameters.AddWithValue("@SellDebitCurrencyPK", _FixedAsset.SellDebitCurrencyPK);
                                cmd.Parameters.AddWithValue("@SellCreditCurrencyPK", _FixedAsset.SellCreditCurrencyPK);
                                cmd.Parameters.AddWithValue("@SellDebitRate", _FixedAsset.SellDebitRate);
                                cmd.Parameters.AddWithValue("@SellCreditRate", _FixedAsset.SellCreditRate);
                                cmd.Parameters.AddWithValue("@DepartmentPK", _FixedAsset.DepartmentPK);
                                cmd.Parameters.AddWithValue("@TypeOfAssetsPK", _FixedAsset.TypeOfAssetsPK);
                                cmd.Parameters.AddWithValue("@Location", _FixedAsset.Location);
                                cmd.Parameters.AddWithValue("@ConsigneePK", _FixedAsset.ConsigneePK);
                                cmd.Parameters.AddWithValue("@ResiduAmount", _FixedAsset.ResiduAmount);
                                cmd.Parameters.AddWithValue("@VATPercent", _FixedAsset.VATPercent);
                                cmd.Parameters.AddWithValue("@EntryUsersID", _FixedAsset.EntryUsersID);
                                cmd.Parameters.AddWithValue("@EntryTime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _FixedAsset.EntryUsersID);
                                cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);

                                cmd.ExecuteNonQuery();
                            }


                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update FixedAsset set status= 4,Notes=@Notes," +
                                    "UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@lastupdate where FixedAssetPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _FixedAsset.Notes);
                                cmd.Parameters.AddWithValue("@PK", _FixedAsset.FixedAssetPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _FixedAsset.HistoryPK);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _FixedAsset.EntryUsersID);
                                cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
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
        //7
        public void FixedAsset_Approved(FixedAsset _FixedAsset)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update FixedAsset set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,LastUpdate = @LastUpdate " +
                            "where FixedAssetPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _FixedAsset.FixedAssetPK);
                        cmd.Parameters.AddWithValue("@historyPK", _FixedAsset.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _FixedAsset.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update FixedAsset set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where FixedAssetPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _FixedAsset.FixedAssetPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _FixedAsset.ApprovedUsersID);
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
        //8
        public void FixedAsset_Reject(FixedAsset _FixedAsset)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update FixedAsset set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where FixedAssetPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _FixedAsset.FixedAssetPK);
                        cmd.Parameters.AddWithValue("@historyPK", _FixedAsset.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _FixedAsset.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update FixedAsset set status= 2,LastUpdate=@LastUpdate where FixedAssetPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _FixedAsset.FixedAssetPK);
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
        //9
        public void FixedAsset_Void(FixedAsset _FixedAsset)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update FixedAsset set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where FixedAssetPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _FixedAsset.FixedAssetPK);
                        cmd.Parameters.AddWithValue("@historyPK", _FixedAsset.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _FixedAsset.VoidUsersID);
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

        public void FixedAsset_UnApproved(FixedAsset _FixedAsset)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update FixedAsset set status = 1,LastUpdate=@LastUpdate " +
                            "where FixedAssetPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _FixedAsset.FixedAssetPK);
                        cmd.Parameters.AddWithValue("@historyPK", _FixedAsset.HistoryPK);
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

        public List<FixedAsset> FixedAsset_SelectFromTo(int _status, string _type, DateTime _dateFrom, DateTime _dateTo)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<FixedAsset> L_FixedAsset = new List<FixedAsset>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {

                            cmd.CommandText = @"select P.ID PeriodID,CR1.ID AccountBuyCreditID,C1.ID BuyDebitCurrencyID,C2.ID BuyCreditCurrencyID,C3.ID SellDebitCurrencyID,C4.ID SellCreditCurrencyID,  
                            O.ID OfficeID, CR2.ID AccountSellDebitID,MV1.DescOne DepreciationModeDesc, MV2.DescOne JournalIntervalDesc,  
                            MV3.DescOne PeriodUnitDesc,A1.ID FixedAssetAccountID,A2.ID DepreciationExpAccountID,A3.ID AccumulatedDeprAccountID, 
                            D.ID DepartmentID, CO.ID ConsigneeID, BB.ID TypeOfAssetsID
                            ,* from FixedAsset FA 
                            Left join Period P on FA.PeriodPK = P.PeriodPK and P.Status in (1,2)
                            Left join Account CR1 on FA.AccountBuyCredit = CR1.AccountPK and CR1.Status in (1,2)
                            Left join Currency C1 on FA.BuyDebitCurrencyPK = C1.CurrencyPK and C1.Status in (1,2)
                            Left join Currency C2 on FA.BuyCreditCurrencyPK = C2.CurrencyPK and C2.Status in (1,2)
                            Left join Currency C3 on FA.SellDebitCurrencyPK = C3.CurrencyPK and C3.Status in (1,2)
                            Left join Currency C4 on FA.SellCreditCurrencyPK = C4.CurrencyPK and C4.Status in (1,2)
                            Left join Office O on FA.OfficePK = O.OfficePK and O.Status in (1,2)
                            Left join Account CR2 on FA.AccountSellDebit = CR2.AccountPK and CR2.Status in (1,2)
                            Left join Account A1 on FA.FixedAssetAccount = A1.AccountPK and A1.Status in (1,2)
                            Left join Account A2 on FA.DepreciationExpAccount = A2.AccountPK and A2.Status in (1,2)
                            Left join Account A3 on FA.AccumulatedDeprAccount = A3.AccountPK and A3.Status in (1,2)
                            Left join MasterValue MV1 on FA.DepreciationMode = MV1.Code and MV1.Status = 2 and MV1.ID = 'DepreciationMode' 
                            Left join MasterValue MV2 on FA.JournalInterval = MV2.Code and MV2.Status = 2 and MV2.ID = 'JournalInterval' 
                            Left join MasterValue MV3 on FA.PeriodUnit = MV3.Code and MV3.Status = 2 and MV3.ID = 'PeriodUnit' 
                            left join Department D on FA.DepartmentPK = D.DepartmentPK and D.Status in(1,2)  
                            Left join Consignee CO on FA.ConsigneePK = CO.ConsigneePK and CO.Status in(1,2)  
                            Left join TypeOfAssets BB on FA.TypeOfAssetsPK = BB.TypeOfAssetsPK and BB.Status in(1,2)      			
                            Where  FA.Status = @Status and BuyValueDate between @DateFrom and @DateTo  ";
                            cmd.Parameters.AddWithValue("@Status", _status);
                            cmd.Parameters.AddWithValue("@Type", _type);
                            cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                            cmd.Parameters.AddWithValue("@DateTo", _dateTo);

                        }
                        else
                        {
                            cmd.CommandText = @" select P.ID PeriodID,CR1.ID AccountBuyCreditID,C1.ID BuyDebitCurrencyID,C2.ID BuyCreditCurrencyID,C3.ID SellDebitCurrencyID,C4.ID SellCreditCurrencyID,  
                            O.ID OfficeID, CR2.ID AccountSellDebitID,MV1.DescOne DepreciationModeDesc, MV2.DescOne JournalIntervalDesc,  
                            MV3.DescOne PeriodUnitDesc,A1.ID FixedAssetAccountID,A2.ID DepreciationExpAccountID,A3.ID AccumulatedDeprAccountID, 
                            D.ID DepartmentID, CO.ID ConsigneeID, BB.ID TypeOfAssetsID
                            ,* from FixedAsset FA 
                            Left join Period P on FA.PeriodPK = P.PeriodPK and P.Status in (1,2)
                            Left join Account CR1 on FA.AccountBuyCredit = CR1.AccountPK and CR1.Status in (1,2)
                            Left join Currency C1 on FA.BuyDebitCurrencyPK = C1.CurrencyPK and C1.Status in (1,2)
                            Left join Currency C2 on FA.BuyCreditCurrencyPK = C2.CurrencyPK and C2.Status in (1,2)
                            Left join Currency C3 on FA.SellDebitCurrencyPK = C3.CurrencyPK and C3.Status in (1,2)
                            Left join Currency C4 on FA.SellCreditCurrencyPK = C4.CurrencyPK and C4.Status in (1,2)
                            Left join Office O on FA.OfficePK = O.OfficePK and O.Status in (1,2)
                            Left join Account CR2 on FA.AccountSellDebit = CR2.AccountPK and CR2.Status in (1,2)
                            Left join Account A1 on FA.FixedAssetAccount = A1.AccountPK and A1.Status in (1,2)
                            Left join Account A2 on FA.DepreciationExpAccount = A2.AccountPK and A2.Status in (1,2)
                            Left join Account A3 on FA.AccumulatedDeprAccount = A3.AccountPK and A3.Status in (1,2)
                            Left join MasterValue MV1 on FA.DepreciationMode = MV1.Code and MV1.Status = 2 and MV1.ID = 'DepreciationMode' 
                            Left join MasterValue MV2 on FA.JournalInterval = MV2.Code and MV2.Status = 2 and MV2.ID = 'JournalInterval' 
                            Left join MasterValue MV3 on FA.PeriodUnit = MV3.Code and MV3.Status = 2 and MV3.ID = 'PeriodUnit' 
                            left join Department D on FA.DepartmentPK = D.DepartmentPK and D.Status in(1,2)  
                            Left join Consignee CO on FA.ConsigneePK = CO.ConsigneePK and CO.Status in(1,2)  
                            Left join TypeOfAssets BB on FA.TypeOfAssetsPK = BB.TypeOfAssetsPK and BB.Status in(1,2)      			
                            Where  BuyValueDate between @DateFrom and @DateTo    ";
                            cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                            cmd.Parameters.AddWithValue("@DateTo", _dateTo);
                        }

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_FixedAsset.Add(setFixedAsset(dr));
                                }
                            }
                            return L_FixedAsset;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }


        //Posting FixedAsset
        public void FixedAsset_Posting(FixedAsset _FixedAsset)
        {

            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        {

                            cmd.CommandText = "Update FixedAsset set Posted = 1 ," +
                             "PostedBy=@PostedBy,PostedTime=@PostedTime,LastUpdate=@lastupdate " +
                             "where FixedAssetPK = @PK and historyPK = @HistoryPK";

                            cmd.Parameters.AddWithValue("@PK", _FixedAsset.FixedAssetPK);
                            cmd.Parameters.AddWithValue("@HistoryPK", _FixedAsset.HistoryPK);
                            cmd.Parameters.AddWithValue("@PostedBy", _FixedAsset.EntryUsersID);
                            cmd.Parameters.AddWithValue("@PostedTime", _dateTimeNow);
                            cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);


                        }
                        cmd.ExecuteNonQuery();
                    }
                }

            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public FixedAssetAddNew GetFixedAssetID(int _FixedAssetPK)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    FixedAssetAddNew M_FixedAsset = new FixedAssetAddNew();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "SELECT  FixedAssetPK,HistoryPK FROM [FixedAsset]  where status = 1 and FixedAssetPK = @FixedAssetPK";
                        cmd.Parameters.AddWithValue("@FixedAssetPK", _FixedAssetPK);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                M_FixedAsset.FixedAssetPK = Convert.ToInt32(dr["FixedAssetPK"]);
                                M_FixedAsset.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
                                M_FixedAsset.Message = "Insert Fixed Aset Success";
                            }
                            return M_FixedAsset;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        //Posting FixedAsset
        public void FixedAsset_Revise(FixedAsset _FixedAsset)
        {

            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        {

                            cmd.CommandText = @"
                            declare @ValueDate datetime
                            select @ValueDate = BuyValueDate from FixedAsset where FixedAssetPK = @FixedAssetPK and HistoryPK = @HistoryPK

                            
                            update Cashier set status = 3 where status in (1,2) and ValueDate = @ValueDate and TrxName = 'Fixed Asset' and TrxNo = @FixedAssetPK

                            update Journal set Status = 3,VoidTime = @LastUpdate,VoidUsersID = @RevisedBy  where Type = 8 and TrxNo = @FixedAssetPK
                            and Posted = 1

                            UPDATE Journal SET status = 3, Notes = 'Void By Fixed Asset Revise',  
                            VoidUsersID = @RevisedBy,VoidTime = @LastUpdate  
                            WHERE TrxName in ('CP') and TrxNo in  
                            (  
                                Select CashierPK from cashier where ValueDate = @ValueDate and TrxName = 'Fixed Asset' and TrxNo = @FixedAssetPK
                                and isnull(JournalNo,0) > 0
                            )  


                            Declare @MaxFixedAssetPK int

                            Select @MaxFixedAssetPK = ISNULL(MAX(FixedAssetPK), 0) + 1 From FixedAsset

                            INSERT INTO[dbo].[FixedAsset]
                            ([FixedAssetPK],[HistoryPK],[Status],[Notes],[PeriodPK],[BuyValueDate],[BuyJournalNo],
                            [BuyReference],[BuyDescription],[FixedAssetAccount],[AccountBuyCredit],[BuyAmount],
                            [BitWithPPNBuy],[BitWithPPNSell],[AmountPPNBuy],[AmountPPNSell],[DepreciationMode],
                            [JournalInterval],[IntervalSpecificDate],[DepreciationPeriod],[PeriodUnit],
                            [DepreciationExpAccount],[AccumulatedDeprAccount],[OfficePK],[SellValueDate],
                            [SellReference],[SellDescription],[SellJournalNo],[SellAmount],[AccountSellDebit],
                            [BitSold],[BuyDebitCurrencyPK],[BuyCreditCurrencyPK],[BuyDebitRate],[BuyCreditRate],
                            [SellDebitCurrencyPK],[SellCreditCurrencyPK],[SellDebitRate],[SellCreditRate],
                            [DepartmentPK],[TypeOfAssetsPK],[Location],[ConsigneePK],[ResiduAmount],
                            [DepreciationPerMonth],[VATPercent],[EntryUsersID],[EntryTime],[LastUpdate])


                            SELECT @MaxFixedAssetPK,1,1,'Pending Revised' ,[PeriodPK],[BuyValueDate],[BuyJournalNo],
                            [BuyReference],[BuyDescription],[FixedAssetAccount],[AccountBuyCredit],[BuyAmount],
                            [BitWithPPNBuy],[BitWithPPNSell],[AmountPPNBuy],[AmountPPNSell],[DepreciationMode],
                            [JournalInterval],[IntervalSpecificDate],[DepreciationPeriod],[PeriodUnit],
                            [DepreciationExpAccount],[AccumulatedDeprAccount],[OfficePK],[SellValueDate],
                            [SellReference],[SellDescription],[SellJournalNo],[SellAmount],[AccountSellDebit],
                            [BitSold],[BuyDebitCurrencyPK],[BuyCreditCurrencyPK],[BuyDebitRate],[BuyCreditRate],
                            [SellDebitCurrencyPK],[SellCreditCurrencyPK],[SellDebitRate],[SellCreditRate],
                            [DepartmentPK],[TypeOfAssetsPK],[Location],[ConsigneePK],[ResiduAmount],
                            [DepreciationPerMonth],[VATPercent],
                            [EntryUsersID],[EntryTime] , @LastUpdate
                            FROM FixedAsset
                            WHERE FixedAssetPK = @FixedAssetPK and HistoryPK = @HistoryPK  and status = 2 and posted = 1

                            update FixedAsset
                            set RevisedBy = @RevisedBy, RevisedTime = @LastUpdate, Revised = 1, status = 3
                            where FixedAssetPK = @FixedAssetPK and HistoryPK = @HistoryPK and Status = 2 and posted = 1
                              ";

                            cmd.Parameters.AddWithValue("@FixedAssetPK", _FixedAsset.FixedAssetPK);
                            cmd.Parameters.AddWithValue("@HistoryPK", _FixedAsset.HistoryPK);
                            cmd.Parameters.AddWithValue("@RevisedBy", _FixedAsset.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _dateTimeNow);
                            cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);


                        }
                        cmd.ExecuteNonQuery();
                    }
                }

            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public void FixedAsset_PostingBySelected(string _usersID, DateTime _dateFrom, DateTime _dateTo)
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
                        

                        --set @LastUpdate = getdate()
                        --DROP TABLE  #Cashier
                        --DROP TABLE  #Journal
                        --DROP TABLE  #JournalDetail

                        
                        Declare @FixedAssetPK int,@HistoryPK int, @JournalPK int, @ValueDate datetime, @Description nvarchar(100),
                        @DepreciationExpAccount int, @AccumulatedDeprAccount int, @OfficePK int,
                        @DepartmentPK int, @ConsigneePK int, @CurrencyPK int, @Rate numeric (18,2),
                        @PeriodID nvarchar(5), @CurReference nvarchar(100), @PeriodPK int,
                        @CounterDate datetime, @BuyAmount numeric(22,4),@DepreciationMode int, @JournalInterval int,@DepreciationAmount numeric(22,4), 
                        @PeriodUnit Nvarchar(1),@DepreciationPeriod int,@ResiduAmount numeric(22,4),@DepreciationPerMonth numeric(22,4),
                        @CashierPK int, @CashierID nvarchar(50),@FixedAssetAccount int,@AccountBuyCredit int, @CashRefPK int, @VATAmount numeric(22,4),
                        @FixedAsetBuyPPN int, @ParamPeriodUnit datetime, @ParamPeriodCounter datetime
	          
                        select @PeriodID = ID from Period where @DateFrom between DateFrom and DateTo and status = 2

                        DECLARE @CashierPKTemp INT
                        SET @CashierPKTemp = 0

                        DECLARE @JournalPKTemp INT
                        SET @JournalPKTemp = 0

                        DECLARE @JournalDate datetime
                        DECLARE @SpesificDate datetime

                        CREATE TABLE #Cashier
                        (
	                        CashierPK int,CashierID nvarchar(50),HistoryPK int,Status int,PeriodPK int,ValueDate datetime,Type nvarchar(50),
	                        Reference nvarchar(50),Description nvarchar(200),DebitCredit nvarchar(50),DebitCurrencyPK int,  
	                        CreditCurrencyPK int,DebitCashRefPK int,CreditCashRefPK int,DebitAccountPK int,CreditAccountPK int,Debit numeric(18,4),  
	                        Credit numeric(18,4),DebitCurrencyRate numeric(18,4),CreditCurrencyRate numeric(18,4),BaseDebit numeric(18,4),
	                        BaseCredit numeric(18,4),PercentAmount numeric(18,4),FinalAmount numeric(18,4),OfficePK int,DepartmentPK int,
	                        AgentPK int,CounterpartPK int,ConsigneePK int,InstrumentPK int,JournalNo nvarchar(50),TrxNo nvarchar(50),
	                        EntryUsersID nvarchar(50),EntryTime datetime,ApprovedUsersID nvarchar(50),ApprovedTime datetime,LastUpdate datetime
                        )

                        CREATE TABLE #Journal
                        (
	                        JournalPK int,HistoryPK int,Status int,Notes nvarchar(200),PeriodPK int,ValueDate datetime,Type int,TrxNo nvarchar(50),
	                        TrxName nvarchar(50),Reference nvarchar(50),Description nvarchar(200),Posted int,EntryUsersID nvarchar(50),EntryTime datetime,
	                        ApprovedUsersID nvarchar(50),ApprovedTime datetime,PostedBy nvarchar(50),PostedTime datetime,LastUpdate datetime
                        )

                        CREATE TABLE #JournalDetail
                        (
	                        JournalPK int,AutoNo int,HistoryPK int,Status int,AccountPK int,CurrencyPK int,InstrumentPK int,CounterpartPK int,                
	                        DetailDescription nvarchar(200),DebitCredit nvarchar(5),Amount numeric(18,4),Debit  numeric(18,4),Credit  numeric(18,4),
	                        CurrencyRate numeric(18,4),BaseDebit numeric(18,4),BaseCredit numeric(18,4),LastUpdate datetime
                        )



                        Declare A Cursor For                  
	                        select FixedAssetPK,HistoryPK,BuyValueDate,PeriodPK,BuyDescription,DepreciationExpAccount,AccumulatedDeprAccount,
	                        OfficePK,DepartmentPK,ConsigneePK,BuyDebitCurrencyPK,BuyDebitRate,FixedAssetAccount,AccountBuyCredit,
	                        BuyAmount,ResiduAmount,DepreciationMode,JournalInterval,PeriodUnit,DepreciationPeriod,AmountPPNBuy - BuyAmount
	
	                        from FixedAsset
	                        Where status = 2 and Posted = 0 and Revised = 0 and BuyValueDate between @DateFrom and @DateTo and Selected = 1              
	                        Open A                  
	                        Fetch Next From A                  
	                        Into @FixedAssetPK,@HistoryPK,@ValueDate,@PeriodPK,@Description,@DepreciationExpAccount,@AccumulatedDeprAccount,
	                        @OfficePK,@DepartmentPK,@ConsigneePK,@CurrencyPK,@Rate,@FixedAssetAccount,@AccountBuyCredit,
	                        @BuyAmount,@ResiduAmount,@DepreciationMode,@JournalInterval,@PeriodUnit,@DepreciationPeriod,@VATAmount      
                        While @@FETCH_STATUS = 0                  
                        BEGIN  

	                        select @FixedAsetBuyPPN = FixedAsetBuyPPN from AccountingSetup where status in (1,2)

	                        
                            if (@JournalInterval = 1)
                            begin

                            set @CounterDate = case when @PeriodUnit = 'm' then DATEADD(mm, DATEDIFF(mm, 0, @ValueDate) + 1, 0)
						                            else DATEADD(yy, DATEDIFF(yy, 0, @ValueDate) + 1, 0) end

                            set @ParamPeriodUnit = 
                            case when @PeriodUnit = 'm' then DATEADD(mm, DATEDIFF(mm, 0, dateadd(MONTH,@DepreciationPeriod - 1,@ValueDate)) + 1, 0) 
		                            else  DATEADD(yy, DATEDIFF(yy, 0, dateadd(YEAR,@DepreciationPeriod - 1,@ValueDate)) + 1, 0) end
                            end
                            else if (@JournalInterval = 2)
                            begin

                            set @CounterDate = case when @PeriodUnit = 'm' then DATEADD (dd, -1, DATEADD(mm, DATEDIFF(mm, 0, @ValueDate) + 1, 0))
						                            else DATEADD (dd, -1, DATEADD(yy, DATEDIFF(yy, 0, @ValueDate) + 1, 0)) end

                            set @ParamPeriodUnit = 
                            case when @PeriodUnit = 'm' then  DATEADD (dd, -1, DATEADD(mm, DATEDIFF(mm, 0, dateadd(MONTH,@DepreciationPeriod - 1,@ValueDate)) + 1, 0))
		                            else DATEADD (dd, -1, DATEADD(yy, DATEDIFF(yy, 0, dateadd(YEAR,@DepreciationPeriod - 1,@ValueDate)) + 1, 0))  end
                            end
                            else 
                            begin
                            select @SpesificDate = IntervalSpecificDate from FixedAsset where FixedAssetPK = @FixedAssetPK and HistoryPK = @HistoryPK

                            set @CounterDate = @SpesificDate

                            set @ParamPeriodUnit = 
                            case when @PeriodUnit = 'm' then dateadd(MONTH,@DepreciationPeriod - 1,@SpesificDate)
		                            else dateadd(YEAR,@DepreciationPeriod - 1,@SpesificDate) end

                            end


	                        select @CashierID = isnull(max(CashierID),0) + 1 from Cashier Where PeriodPK  = @PeriodPK and Type = 'CP' and status not in (3,4)                        
	                        select @CashRefPK = CashRefPK from CashRef where status in (1,2) and AccountPK = @AccountBuyCredit

	                        Select @CashierPKTemp = max(CashierPK) From #Cashier
	                        Select @CashierPKTemp = isnull(@CashierPKTemp,0)

                            select @JournalDate = case when dbo.CheckTodayIsHoliday(@CounterDate) = 1 then dbo.fworkingday(@CounterDate,1) else @CounterDate end

	                        INSERT INTO #Cashier  
	                        ([CashierPK],[CashierID],[HistoryPK],[Status],[PeriodPK],[ValueDate],[Type],[Reference],[Description],[DebitCredit],[DebitCurrencyPK],  
	                        [CreditCurrencyPK],[DebitCashRefPK],[CreditCashRefPK],[DebitAccountPK],[CreditAccountPK],[Debit],  
	                        [Credit],[DebitCurrencyRate],[CreditCurrencyRate],[BaseDebit],[BaseCredit],[PercentAmount],[FinalAmount],[OfficePK],[DepartmentPK],[AgentPK],[CounterpartPK],[ConsigneePK],[InstrumentPK],[JournalNo],[TrxNo],
	                        [EntryUsersID],[EntryTime],[ApprovedUsersID],[ApprovedTime],LastUpdate)

	                        select ROW_NUMBER() OVER(ORDER BY FixedAssetPK ASC) + @CashierPKTemp,@CashierID,1,1,@PeriodPK, @ValueDate,'CP','','FIXED ASSET: Paid ' + @Description,'D',1,
	                        1,0,@CashRefPK,@FixedAssetAccount,@AccountBuyCredit,@BuyAmount,@BuyAmount,1,1,@BuyAmount,@BuyAmount,100,@BuyAmount,@OfficePK,@DepartmentPK,0,0,@ConsigneePK,0,0,@FixedAssetPK,
	                        EntryusersID,EntryTime,@UsersID,@LastUpdate,@LastUpdate from FixedAsset where  FixedAssetPK = @FixedAssetPK and HistoryPK = @HistoryPK   

	                        Select @CashierPKTemp = max(CashierPK) From #Cashier
	                        Select @CashierPKTemp = isnull(@CashierPKTemp,0)
	                 
	                        INSERT INTO #Cashier  
	                        ([CashierPK],[CashierID],[HistoryPK],[Status],[PeriodPK],[ValueDate],[Type],[Reference],[Description],[DebitCredit],[DebitCurrencyPK],  
	                        [CreditCurrencyPK],[DebitCashRefPK],[CreditCashRefPK],[DebitAccountPK],[CreditAccountPK],[Debit],  
	                        [Credit],[DebitCurrencyRate],[CreditCurrencyRate],[BaseDebit],[BaseCredit],[PercentAmount],[FinalAmount],[OfficePK],[DepartmentPK],[AgentPK],[CounterpartPK],[ConsigneePK],[InstrumentPK],[JournalNo],[TrxNo],
	                        [EntryUsersID],[EntryTime],[ApprovedUsersID],[ApprovedTime],LastUpdate)

	                        select ROW_NUMBER() OVER(ORDER BY FixedAssetPK ASC) + @CashierPKTemp,@CashierID,1,1,@PeriodPK, @ValueDate,'CP','','FIXED ASSET: VAT ' + @Description,'D',1,
	                        1,0,@CashRefPK,@FixedAsetBuyPPN,@AccountBuyCredit,@VATAmount,@VATAmount,1,1,@VATAmount,@VATAmount,100,@VATAmount,@OfficePK,@DepartmentPK,0,0,@ConsigneePK,0,0,@FixedAssetPK,
	                        EntryusersID,EntryTime,@UsersID,@LastUpdate,@LastUpdate from FixedAsset where  FixedAssetPK = @FixedAssetPK and HistoryPK = @HistoryPK   


	                        IF @DepreciationMode = 1 and @DepreciationPeriod > 0
	                        BEGIN
		                        Select @DepreciationAmount =  (@BuyAmount - @ResiduAmount) / @DepreciationPeriod
	                        END


	                        WHILE @CounterDate <= @ParamPeriodUnit
	                        BEGIN


	                        
                            if(@JournalInterval = 2) -- EOMONTH
                            begin
	                            select @ParamPeriodCounter = 
	                            case when @PeriodUnit = 'm' then eomonth(dateadd(MONTH,1,@CounterDate))
	                            else eomonth(dateadd(YEAR,1,@CounterDate)) end
                            end
                            else
                            begin
	                            select @ParamPeriodCounter = 
	                            case when @PeriodUnit = 'm' then dateadd(MONTH,1,@CounterDate)
	                            else dateadd(YEAR,1,@CounterDate) end
                            end
              
	                        select @JournalDate = case when dbo.CheckTodayIsHoliday(@CounterDate) = 1 then dbo.FWorkingDay(@CounterDate,1) else @CounterDate end
                            
	                        exec getJournalReference @JournalDate,'FA',@CurReference out

	                        Select @JournalPKTemp = max(JournalPK) From #Journal
	                        Select @JournalPKTemp = isnull(@JournalPKTemp,0)                 

	                        ----T0                  
	                        INSERT INTO #Journal
	                        ([JournalPK],[HistoryPK],[Status],[Notes],[PeriodPK],[ValueDate],[Type],[TrxNo],[TrxName],[Reference]                  
	                        ,[Description],[Posted],[EntryUsersID],[EntryTime],[ApprovedUsersID],[ApprovedTime],[PostedBy],[PostedTime],[LastUpdate])                  

	                        Select  ROW_NUMBER() OVER(ORDER BY FixedAssetPK ASC) + @JournalPKTemp, 1,2,'',@PeriodPK,@JournalDate,8,FixedAssetPK,'FixedAsset',                  
	                        @CurReference,'Depreciation : ' + @Description + ',Month : ' + DateName( month , DateAdd( month , month(@JournalDate) , -1 ) )  + ', ' + CAST(year(@JournalDate) as NVARCHAR(4)),1,EntryusersID,EntryTime,@UsersID,@LastUpdate,@UsersID,@LastUpdate,@LastUpdate from FixedAsset 
	                        where  FixedAssetPK = @FixedAssetPK and HistoryPK = @HistoryPK               


	                        IF (@DepreciationAmount >= 0)
	                        BEGIN
		                        INSERT INTO #JournalDetail([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[InstrumentPK],[CounterpartPK],                
		                        [DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])                  

		                        Select  ROW_NUMBER() OVER(ORDER BY FixedAssetPK ASC) + @JournalPKTemp,1,1,2,@DepreciationExpAccount,@CurrencyPK,0,0,'FIXED ASSET','D',abs(@DepreciationAmount),                   
		                        abs(@DepreciationAmount),0,1,abs(@DepreciationAmount),0,@LastUpdate from FixedAsset 
		                        where  FixedAssetPK = @FixedAssetPK and HistoryPK = @HistoryPK                            


		                        INSERT INTO #JournalDetail([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[InstrumentPK],[CounterpartPK],                
		                        [DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])                   

		                        Select ROW_NUMBER() OVER(ORDER BY FixedAssetPK ASC) + @JournalPKTemp,2,1,2,@AccumulatedDeprAccount,@CurrencyPK,0,0,'FIXED ASSET','C',abs(@DepreciationAmount),                   
		                        0,abs(@DepreciationAmount),1,0,abs(@DepreciationAmount),@LastUpdate from FixedAsset 
		                        where  FixedAssetPK = @FixedAssetPK and HistoryPK = @HistoryPK                  
	                        END
	                        ELSE
	                        BEGIN
		                        INSERT INTO #JournalDetail([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[InstrumentPK],[CounterpartPK],                
		                        [DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])                  

		                        Select  ROW_NUMBER() OVER(ORDER BY FixedAssetPK ASC) + @JournalPKTemp,1,1,2,@AccumulatedDeprAccount,@CurrencyPK,0,0,'FIXED ASSET','D',abs(@DepreciationAmount),                   
		                        abs(@DepreciationAmount),0,1,abs(@DepreciationAmount),0,@LastUpdate from FixedAsset 
		                        where  FixedAssetPK = @FixedAssetPK and HistoryPK = @HistoryPK                                


		                        INSERT INTO #JournalDetail([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[InstrumentPK],[CounterpartPK],                
		                        [DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])                   

		                        Select ROW_NUMBER() OVER(ORDER BY FixedAssetPK ASC) + @JournalPKTemp,2,1,2,@DepreciationExpAccount,@CurrencyPK,0,0,'FIXED ASSET','C',abs(@DepreciationAmount),                   
		                        0,abs(@DepreciationAmount),1,0,abs(@DepreciationAmount),@LastUpdate from FixedAsset 
		                        where  FixedAssetPK = @FixedAssetPK and HistoryPK = @HistoryPK                   
	                        END

	

		                        set @CounterDate = @ParamPeriodCounter
	                        END


	                        update FixedAsset    
	                        set PostedBy = @UsersID,PostedTime = @LastUpdate,Lastupdate = @LastUpdate, posted = 1 
	                        where FixedAssetPK = @FixedAssetPK and HistoryPK = @HistoryPK and Status = 2 

                        Fetch next From A                   
	                        Into @FixedAssetPK,@HistoryPK,@ValueDate,@PeriodPK,@Description,@DepreciationExpAccount,@AccumulatedDeprAccount,
	                        @OfficePK,@DepartmentPK,@ConsigneePK,@CurrencyPK,@Rate,@FixedAssetAccount,@AccountBuyCredit,
	                        @BuyAmount,@ResiduAmount,@DepreciationMode,@JournalInterval,@PeriodUnit,@DepreciationPeriod,@VATAmount          
                        END                  
                        Close A                  
                        Deallocate A



                        Select @CashierPK = max(CashierPK) From Cashier
                        Select @CashierPK = isnull(@CashierPK,0)

                        Select @JournalPK = max(JournalPK) From Journal
                        Select @JournalPK = isnull(@JournalPK,0)

                        INSERT INTO [dbo].[Cashier]  
                        ([CashierPK],[CashierID],[HistoryPK],[Status],[PeriodPK],[ValueDate],[Type],[Reference],[Description],[DebitCredit],[DebitCurrencyPK],  
                        [CreditCurrencyPK],[DebitCashRefPK],[CreditCashRefPK],[DebitAccountPK],[CreditAccountPK],[Debit],  
                        [Credit],[DebitCurrencyRate],[CreditCurrencyRate],[BaseDebit],[BaseCredit],[PercentAmount],[FinalAmount],[OfficePK],[DepartmentPK],[AgentPK],[CounterpartPK],[ConsigneePK],[InstrumentPK],[JournalNo],[TrxNo],[TrxName],
                        [EntryUsersID],[EntryTime],[ApprovedUsersID],[ApprovedTime],LastUpdate)

                        select @CashierPK + [CashierPK],[CashierID],[HistoryPK],[Status],[PeriodPK],[ValueDate],[Type],[Reference],[Description],[DebitCredit],[DebitCurrencyPK],  
                        [CreditCurrencyPK],[DebitCashRefPK],[CreditCashRefPK],[DebitAccountPK],[CreditAccountPK],[Debit],  
                        [Credit],[DebitCurrencyRate],[CreditCurrencyRate],[BaseDebit],[BaseCredit],[PercentAmount],[FinalAmount],[OfficePK],[DepartmentPK],[AgentPK],[CounterpartPK],[ConsigneePK],[InstrumentPK],[JournalNo],[TrxNo],'Fixed Asset',
                        [EntryUsersID],[EntryTime],[ApprovedUsersID],[ApprovedTime],LastUpdate from #Cashier

                        INSERT INTO Journal
                        ([JournalPK],[HistoryPK],[Status],[Notes],[PeriodPK],[ValueDate],[Type],[TrxNo],[TrxName],[Reference]                  
                        ,[Description],[Posted],[EntryUsersID],[EntryTime],[ApprovedUsersID],[ApprovedTime],[PostedBy],[PostedTime],[LastUpdate])                  

                        select @JournalPK + [JournalPK],[HistoryPK],[Status],[Notes],[PeriodPK],[ValueDate],[Type],[TrxNo],[TrxName],[Reference]                  
                        ,[Description],[Posted],[EntryUsersID],[EntryTime],[ApprovedUsersID],[ApprovedTime],[PostedBy],[PostedTime],[LastUpdate] from #Journal


                        INSERT INTO JournalDetail([JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[InstrumentPK],[CounterpartPK],                
                        [DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate])                   

                        select @JournalPK + [JournalPK],[AutoNo],[HistoryPK],[Status],[AccountPK],[CurrencyPK],[InstrumentPK],[CounterpartPK],                
                        [DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate] from #JournalDetail                  


                        ";
                        cmd.CommandTimeout = 0;
                        cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _dateTo);
                        cmd.Parameters.AddWithValue("@UsersID", _usersID);
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

        //Sell FixedAsset
        public void FixedAsset_Sell(FixedAsset _FixedAsset)
        {

            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        {

                            cmd.CommandText = "Update FixedAsset set PeriodPK=@PeriodPK,SellValueDate=@SellValueDate, " +
                             "SellReference=@SellReference,SellDescription=@SellDescription,SellJournalNo=@SellJournalNo,SellAmount=@SellAmount,BitWithPPNSell=@BitWithPPNSell,AmountPPNSell=@AmountPPNSell, " +
                             "AccountSellDebit=@AccountSellDebit,BitSold='True',SellDebitCurrencyPK=@SellDebitCurrencyPK,SellCreditCurrencyPK=@SellCreditCurrencyPK, " +
                             "SellDebitRate=@SellDebitRate,SellCreditRate=@SellCreditRate," +
                             "UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@lastupdate " +
                             "where FixedAssetPK = @PK and historyPK = @HistoryPK";

                            cmd.Parameters.AddWithValue("@PK", _FixedAsset.FixedAssetPK);
                            cmd.Parameters.AddWithValue("@HistoryPK", _FixedAsset.HistoryPK);
                            cmd.Parameters.AddWithValue("@PeriodPK", _FixedAsset.PeriodPK);
                            cmd.Parameters.AddWithValue("@SellValueDate", _FixedAsset.SellValueDate);
                            cmd.Parameters.AddWithValue("@SellReference", _FixedAsset.SellReference);
                            cmd.Parameters.AddWithValue("@SellDescription", _FixedAsset.SellDescription);
                            cmd.Parameters.AddWithValue("@SellJournalNo", _FixedAsset.SellJournalNo);
                            cmd.Parameters.AddWithValue("@SellAmount", _FixedAsset.SellAmount);
                            cmd.Parameters.AddWithValue("@BitWithPPNSell", _FixedAsset.BitWithPPNSell);
                            cmd.Parameters.AddWithValue("@AmountPPNSell", _FixedAsset.AmountPPNSell);
                            cmd.Parameters.AddWithValue("@AccountSellDebit", _FixedAsset.AccountSellDebit);
                            cmd.Parameters.AddWithValue("@SellDebitCurrencyPK", _FixedAsset.SellDebitCurrencyPK);
                            cmd.Parameters.AddWithValue("@SellCreditCurrencyPK", _FixedAsset.SellCreditCurrencyPK);
                            cmd.Parameters.AddWithValue("@SellDebitRate", _FixedAsset.SellDebitRate);
                            cmd.Parameters.AddWithValue("@SellCreditRate", _FixedAsset.SellCreditRate);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _FixedAsset.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _dateTimeNow);
                            cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);


                        }
                        cmd.ExecuteNonQuery();
                    }
                }

            }
            catch (Exception err)
            {
                throw err;
            }
        }


        public List<SetFixedAsset> FixedAsset_GetFixedAsset(int _pk)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<SetFixedAsset> L_setFixedAsset = new List<SetFixedAsset>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"
                        Declare @BuyAmount numeric(22,4),@DepreciationMode int, @JournalInternal int, @PeriodUnit Nvarchar(1)
                        ,@DepreciationPeriod int,@ResiduAmount numeric(22,4)
                        ,@DepreciationPerMonth numeric(22,4)
                        ,@ValueDate Datetime

                        Select @BuyAmount = A.BuyAmount , @ResiduAmount = A.ResiduAmount
                        ,@DepreciationMode = A.DepreciationMode
                        ,@JournalInternal = A.JournalInterval
                        ,@PeriodUnit = A.PeriodUnit
                        ,@DepreciationPeriod = A.DepreciationPeriod
                        ,@ValueDate = BuyValueDate
                        from FixedAsset A
                        where A.FixedAssetPK = @FixedAssetPK
                        and A.status in (1,2)

                        set @DepreciationPerMonth = 0

                        -- NTAR INI DITUTUP
                        --set  @BuyAmount =50000000
                        --set @ResiduAmount = 5000000
                        --set @DepreciationMode = 1
                        --set @JournalInternal = 2
                        --set @PeriodUnit = 'm'
                        --set @DepreciationPeriod = 60
                        --set @ValueDate = '10/14/18'
                        -- SAMPE SINI


                        IF @DepreciationMode = 1 and @DepreciationPeriod > 0
                        BEGIN
	                        Select @DepreciationPerMonth =  (@BuyAmount - @ResiduAmount) / @DepreciationPeriod
                        END
                        ELSE 
                        BEGIN
	                        Declare @DepreciationPercent numeric(8,4)
	                        Select @DepreciationPercent = 2*(1 * 1.00/@DepreciationPeriod * 1.00)
	                        --Select @DepreciationPerMonth = 1 - Power((@r
                        END

                        Declare @CounterDate datetime

                        set @CounterDate = @ValueDate

                        Create table #FixedAssetResult
                        (
	                        [Year] int,
	                        [Month] Nvarchar(200),
	                        CostValue numeric(22,4),
	                        Depreciation numeric(22,4),
	                        TotalDepreciation numeric(22,4),
	                        BookValue numeric(22,4)
	
                        )

                        declare @TotalDepreciation Numeric(22,4)
                        Declare @Bookvalue numeric(22,4)

                        set @TotalDepreciation = 0
                        set @Bookvalue = @BuyAmount

                        WHILE @CounterDate < dateadd(month,@DepreciationPeriod,@ValueDate)
                        BEGIN
	                        set @TotalDepreciation = @TotalDepreciation + @DepreciationPerMonth
	                        set @Bookvalue = @BuyAmount - @TotalDepreciation
	                        insert into #FixedAssetResult
	                        Select Year(@CounterDate),DateName( month , DateAdd( month , month(@CounterDate) , -1 ) ),@BuyAmount
	                        ,@DepreciationPerMonth,@TotalDepreciation,@Bookvalue

	                        set @CounterDate = dateadd(month,1,@CounterDate)
                        END

                        select * from #FixedAssetResult";
                        cmd.Parameters.AddWithValue("@FixedAssetPK", _pk);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_setFixedAsset.Add(setFixedAssetDetail(dr));
                                }
                            }
                        }
                        return L_setFixedAsset;
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        private SetFixedAsset setFixedAssetDetail(SqlDataReader dr)
        {
            SetFixedAsset M_FixedAsset = new SetFixedAsset();
            //M_FixedAsset.FixedAssetPK = Convert.ToInt32(dr["FixedAssetPK"]);
            M_FixedAsset.Year = Convert.ToString(dr["Year"]);
            M_FixedAsset.Month = Convert.ToString(dr["Month"]);
            M_FixedAsset.CostValue = Convert.ToDecimal(dr["CostValue"]);
            M_FixedAsset.Depreciation = Convert.ToDecimal(dr["Depreciation"]);
            M_FixedAsset.TotalDepreciation = Convert.ToDecimal(dr["TotalDepreciation"]);
            M_FixedAsset.BookValue = Convert.ToDecimal(dr["BookValue"]);
            return M_FixedAsset;
        }


        public Boolean GenerateReportFixedAssets(string _userID, FixedAsset _FixedAsset)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = @" 
                declare @PeriodIDLastYear nvarchar(4)
				set @PeriodIDLastYear = @Year - 1

                  Create table #FixedAssetResultReport
                    (
	                    FixedAssetPK int,
	                    [Year] int,
	                    [Month] Nvarchar(200),
	                    CostValue numeric(22,4),
	                    Depreciation numeric(22,4),
	                    TotalDepreciation numeric(22,4),
	                    BookValue numeric(22,4),
	                    OrderByMonth int
	
                    )

                    Declare @CFixedAssetPK int
                    Declare @CounterDate datetime
                    declare @TotalDepreciation Numeric(22,4)
                    Declare @Bookvalue numeric(22,4)


                    Declare @BuyAmount numeric(22,4),@DepreciationMode int, @JournalInternal int, @PeriodUnit Nvarchar(1)
                    ,@DepreciationPeriod int,@ResiduAmount numeric(22,4)
                    ,@DepreciationPerMonth numeric(22,4)
                    ,@ValueDate Datetime

                    Declare A Cursor For
	                    Select FixedAssetPK from FixedAsset where status in (1,2)
                    Open A
                    Fetch Next From A
                    Into @CFixedAssetPK

                    WHILE @@FETCH_STATUS = 0  
                    BEGIN 
		
                    Select @BuyAmount = A.BuyAmount , @ResiduAmount = A.ResiduAmount
                    ,@DepreciationMode = A.DepreciationMode
                    ,@JournalInternal = A.JournalInterval
                    ,@PeriodUnit = A.PeriodUnit
                    ,@DepreciationPeriod = A.DepreciationPeriod
                    ,@ValueDate = BuyValueDate
                    from FixedAsset A
                    where A.FixedAssetPK = @CFixedAssetPK
                    and A.status in (1,2)

                    set @DepreciationPerMonth = 0


                    IF @DepreciationMode = 1 and @DepreciationPeriod > 0
                    BEGIN
	                    Select @DepreciationPerMonth =  (@BuyAmount - @ResiduAmount) / @DepreciationPeriod
                    END
                    ELSE 
                    BEGIN
	                    Declare @DepreciationPercent numeric(8,4)
	                    Select @DepreciationPercent = 2*(1 * 1.00/@DepreciationPeriod * 1.00)
	                    --Select @DepreciationPerMonth = 1 - Power((@r
                    END



                    set @CounterDate = @ValueDate
                    set @TotalDepreciation = 0
                    set @Bookvalue = @BuyAmount

                    WHILE @CounterDate < dateadd(month,@DepreciationPeriod,@ValueDate)
                    BEGIN
	                    set @TotalDepreciation = @TotalDepreciation + @DepreciationPerMonth
	                    set @Bookvalue = @BuyAmount - @TotalDepreciation
	                    insert into #FixedAssetResultReport
	                    Select @CFixedAssetPK,Year(@CounterDate),DateName( month , DateAdd( month , month(@CounterDate) , -1 ) ),@BuyAmount
	                    ,@DepreciationPerMonth,@TotalDepreciation,@Bookvalue,month(@CounterDate)
	                    set @CounterDate = dateadd(month,1,@CounterDate)
                    END

		


                    Fetch Next From A
                    into @CFixedAssetPK
                    END

                    Close A
                    Deallocate A


 
                     DECLARE @cols AS NVARCHAR(MAX),@colsForQuery AS NVARCHAR(MAX),
                            @query  AS NVARCHAR(MAX)
		


                        select @colsForQuery = STUFF((SELECT ',isnull(' + QUOTENAME([month]) +',0) ' + QUOTENAME([month]) 
                                            from (SELECT DISTINCT [month],OrderByMonth FROM #FixedAssetResultReport where [Year] = @Year) A
						                    order by A.OrderByMonth
                                    FOR XML PATH(''), TYPE
                                    ).value('.', 'NVARCHAR(MAX)') 
                                ,1,1,'')


                        select @cols = STUFF((SELECT distinct ',' + QUOTENAME([month]) 
                                            from #FixedAssetResultReport where [Year] = @Year
                                    FOR XML PATH(''), TYPE
                                    ).value('.', 'NVARCHAR(MAX)') 
                                ,1,1,'')
			
			                     set @query = 'SELECT FixedAssetPK,
								 isnull(TypeOfAssetsID,'''')
								 TypeOfAssetsID
								 ,GLAccount,BuyReference,BuyValueDate,BuyDescription,isnull(DepartmentID,'''') DepartmentID,Location,isnull(ConsigneeID,'''') ConsigneeID,DepreciationPeriod,BuyAmount
								 ,isnull(totalDepLastYear,0) AccDepretiationLastYear
								 ,isnull(BuyAmount,0) - isnull(totalDepLastYear,0) BookValueLastYear
								 , ResiduAmount,[year],' + @colsForQuery  + '  
								  ,isnull(totalDepYear,0) AccDepretiationYear
								 ,isnull(BuyAmount,0) - isnull(totalDepYear,0) BookValueYear
								 from 
                    (
                    SELECT A.FixedAssetPK,F.ID TypeOfAssetsID,C.ID GLAccount,A.[year],A.[month],A.Depreciation
					,B.BuyReference,B.BuyValueDate,B.BuyDescription,B.Location,D.ID DepartmentID, E.ID ConsigneeID, B.DepreciationPeriod,B.BuyAmount,B.ResiduAmount
					,isnull(G.totalDepLastYear,0) totalDepLastYear
					,isnull(H.totalDepYear,0) totalDepYear
					FROM #FixedAssetResultReport A
					Left join FixedAsset B on A.FixedAssetPK = B.FixedAssetPK and B.Status in (1,2)
					left join Account C on B.FixedAssetAccount = C.AccountPK and C.status in (1,2)
					left join Department D on B.DepartmentPK = D.DepartmentPK and D.status in (1,2)
					left join Consignee E on B.ConsigneePK = E.ConsigneePK and E.status in (1,2)
					left join TypeOfAssets F on B.TypeOFAssetsPK = F.TypeOfAssetsPK and F.status in(1,2)
					left join
					(
						Select FixedAssetPK, sum(Depreciation) totalDepLastYear From #FixedAssetResultReport
						where [year] = ' + cast(@PeriodIDLastYear as nvarchar(6)) + '
						group by FixedAssetPK
					)G on A.FixedAssetPK = G.FixedAssetPK
					left join
					(
						Select FixedAssetPK, sum(Depreciation) totalDepYear From #FixedAssetResultReport
						where [year] = ' + cast(@Year as nvarchar(6)) + '
						group by FixedAssetPK
					)H on A.FixedAssetPK = H.FixedAssetPK
					                    where [year] = ' + cast(@Year as nvarchar(6)) + '
					
					
                                    ) x
                                    pivot 
                                    (
                                        SUM(depreciation)
                                        for [month] in (' + @cols + ')
                                    ) p 
				
			                        order by [year] asc'

				

			 
		                    exec(@query)";
                        cmd.CommandTimeout = 0;
                        cmd.Parameters.AddWithValue("@Year", _FixedAsset.Period);

                        using (SqlDataReader dr0 = cmd.ExecuteReader())
                        {
                            if (!dr0.HasRows)
                            {
                                return false;
                            }
                            else
                            {
                                string filePath = Tools.ReportsPath + "FixedAssetsRpt" + "_" + _userID + ".xlsx";
                                string pdfPath = Tools.ReportsPath + "FixedAssetsRpt" + "_" + _userID + ".pdf";
                                FileInfo excelFile = new FileInfo(filePath);
                                if (excelFile.Exists)
                                {
                                    excelFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                    excelFile = new FileInfo(filePath);
                                }

                                // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                using (ExcelPackage package = new ExcelPackage(excelFile))
                                {
                                    package.Workbook.Properties.Title = "AccountingReport";
                                    package.Workbook.Properties.Author = Tools.DefaultReportAuthor();
                                    package.Workbook.Properties.Comments = Tools.DefaultReportComments();
                                    package.Workbook.Properties.Company = Tools.DefaultReportCompany();
                                    package.Workbook.Properties.SetCustomPropertyValue("Checked by", _userID);
                                    package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", Tools.DefaultReportAssemblyName());

                                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Journal Voucher");


                                    int incRowExcel = 1;
                                    int incColExcel = 16;
                                    string _cell = "";
                                    string _cell2 = "";
                                    int _Row = 0;
                                    string _cellTotal = "";
                                    int _start = 1;

                                    //ATUR DATA GROUPINGNYA DULU

                                    for (int inc1 = 15; inc1 < dr0.FieldCount; inc1++)
                                    {
                                        worksheet.Cells[1, incColExcel].Value = dr0.GetName(inc1).ToString();
                                        incColExcel++;
                                    }
                                    while (dr0.Read())
                                    {
                                        int RowA = incRowExcel;
                                        worksheet.Cells[1, 1].Value = "GL Account";
                                        worksheet.Cells[1, 2].Value = "Type Of Assets";
                                        worksheet.Cells[1, 3].Value = "Reference";
                                        worksheet.Cells[1, 4].Value = "Value Date";
                                        worksheet.Cells[1, 5].Value = "Description";
                                        worksheet.Cells[1, 6].Value = "DepartmentID";
                                        worksheet.Cells[1, 7].Value = "Location";
                                        worksheet.Cells[1, 8].Value = "Consignee";
                                        worksheet.Cells[1, 9].Value = "Depreciation Period";
                                        worksheet.Cells[1, 10].Value = "Amount";
                                        worksheet.Cells[1, 11].Value = "Acc Depretiation Last Year";
                                        worksheet.Cells[1, 12].Value = "Book Value Last Year";
                                        worksheet.Cells[1, 13].Value = "Residu Amount";
                                        worksheet.Cells[1, 14].Value = "Depreciation/month";
                                        worksheet.Cells[1, 15].Value = "year";

                                        worksheet.Cells[1, incColExcel].Value = "Total Peny " + dr0["year"].ToString();

                                        string _cellTotal1 = GetColumnName(incColExcel - 1);
                                        _cell2 = ":" + _cellTotal1;
                                        worksheet.Cells["A" + 1 + _cell2 + 1].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                        worksheet.Cells["A" + 1 + _cell2 + 1].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                        worksheet.Cells["A" + 1 + _cell2 + 1].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                        worksheet.Cells["A" + 1 + _cell2 + 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                        incRowExcel = incRowExcel + 1;

                                        worksheet.Cells[incRowExcel, 1].Value = dr0["GLAccount"].ToString();
                                        worksheet.Cells[incRowExcel, 2].Value = dr0["TypeOfAssetsID"].ToString();
                                        worksheet.Cells[incRowExcel, 3].Value = dr0["BuyReference"].ToString();
                                        worksheet.Cells[incRowExcel, 4].Value = Convert.ToDateTime(dr0["BuyValueDate"]);
                                        worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "dd/MMM/yyyy";
                                        worksheet.Cells[incRowExcel, 5].Value = dr0["BuyDescription"].ToString();
                                        worksheet.Cells[incRowExcel, 6].Value = dr0["DepartmentID"].ToString();
                                        worksheet.Cells[incRowExcel, 7].Value = dr0["Location"].ToString();
                                        worksheet.Cells[incRowExcel, 8].Value = dr0["ConsigneeID"].ToString();
                                        worksheet.Cells[incRowExcel, 9].Value = dr0["DepreciationPeriod"].ToString();
                                        worksheet.Cells[incRowExcel, 10].Value = Convert.ToDecimal(dr0["BuyAmount"]);
                                        worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0";
                                        worksheet.Cells[incRowExcel, 11].Value = Convert.ToDecimal(dr0["AccDepretiationLastYear"]);
                                        worksheet.Cells[incRowExcel, 11].Style.Numberformat.Format = "#,##0";
                                        worksheet.Cells[incRowExcel, 12].Value = Convert.ToDecimal(dr0["BookValueLastYear"]);
                                        worksheet.Cells[incRowExcel, 12].Style.Numberformat.Format = "#,##0";
                                        worksheet.Cells[incRowExcel, 13].Value = Convert.ToDecimal(dr0["ResiduAmount"]);
                                        worksheet.Cells[incRowExcel, 13].Style.Numberformat.Format = "#,##0";
                                        worksheet.Cells[incRowExcel, 14].Style.Numberformat.Format = "#,##0.00";
                                        worksheet.Cells[incRowExcel, 14].Formula = "SUM(J" + incRowExcel + "/I" + incRowExcel + ")";
                                        worksheet.Cells[incRowExcel, 14].Calculate();
                                        worksheet.Cells[incRowExcel, 15].Value = dr0["year"].ToString();


                                        for (int i = 15; i < dr0.FieldCount; i++)
                                        {
                                            worksheet.Cells[incRowExcel, i + 1].Value = Convert.ToDecimal(dr0[i]);
                                            worksheet.Cells[incRowExcel, i + 1].Style.Numberformat.Format = "#,##0";

                                            _cellTotal = GetColumnName(i);
                                            _cell = ":" + _cellTotal;
                                            _Row = i;
                                        }

                                        worksheet.Cells[incRowExcel, incColExcel].Style.Numberformat.Format = "#,##0.00";
                                        worksheet.Cells[incRowExcel, incColExcel].Formula = "SUM(N" + incRowExcel + _cell + incRowExcel + ")";
                                        worksheet.Cells[incRowExcel, incColExcel].Calculate();

                                        string _cellTotal3 = GetColumnName(incColExcel - 1);
                                        string _cell3 = ":" + _cellTotal3;

                                        worksheet.Cells["A" + incRowExcel + _cell3 + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                        worksheet.Cells["A" + incRowExcel + _cell3 + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                        worksheet.Cells["A" + incRowExcel + _cell3 + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                        worksheet.Cells["A" + incRowExcel + _cell3 + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                    }


                                    string _rangeDetail = "A:ZZ";

                                    using (ExcelRange r = worksheet.Cells[_rangeDetail]) // KALO  KOLOM 1 SAMPE 9 A-I
                                    {
                                        //NILAINYA NGAMBIL DARI DEFAULT DI TOOLS, KLO MAU BEDA SENDIRI BOLEH2 AJA.
                                        r.Style.Font.Size = 13;

                                    }
                                    // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                    worksheet.PrinterSettings.FitToPage = true;
                                    worksheet.PrinterSettings.FitToWidth = 1;
                                    worksheet.PrinterSettings.FitToHeight = 0;
                                    worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel, incColExcel];
                                    worksheet.Column(1).Width = 15;
                                    worksheet.Column(3).Width = 20;
                                    worksheet.Column(4).Width = 30;
                                    worksheet.Column(9).Width = 25;
                                    worksheet.Column(2).AutoFit();
                                    worksheet.Column(5).AutoFit();
                                    worksheet.Column(6).AutoFit();
                                    worksheet.Column(7).AutoFit();
                                    worksheet.Column(8).AutoFit();
                                    worksheet.Column(10).Width = 15;
                                    worksheet.Column(11).Width = 15;
                                    worksheet.Column(12).Width = 25;
                                    worksheet.Column(13).Width = 15;
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
                                    worksheet.PrinterSettings.Orientation = eOrientation.Landscape; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                    // worksheet.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN
                                    // worksheet.PrinterSettings.FitToPage = true;
                                    //worksheet.HeaderFooter.OddHeader.RightAlignedText = "&14 PAYMENT VOUCHER";
                                    // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                    worksheet.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
                                    worksheet.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
                                    //worksheet.View.PageLayoutView = Tools.DefaultReportPageLayoutView();
                                    worksheet.View.ShowGridLines = Tools.DefaultReportShowGridLines();
                                    worksheet.HeaderFooter.OddHeader.CenteredText = "&14 JOURNAL VOUCHER";

                                    Image img = Image.FromFile(Tools.ReportImage);
                                    worksheet.HeaderFooter.OddHeader.InsertPicture(img, PictureAlignment.Left);

                                    worksheet.HeaderFooter.OddFooter.RightAlignedText =
                                    string.Format("Page {0} of {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);

                                    worksheet.HeaderFooter.OddFooter.CenteredText = Tools.DefaultReportFooterCenterText();
                                    worksheet.HeaderFooter.OddFooter.LeftAlignedText = Tools.DefaultReportFooterLeftText();

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


        static string GetColumnName(int index)
        {
            const string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            var value = "";

            if (index >= letters.Length)
                value += letters[index / letters.Length - 1];

            value += letters[index % letters.Length];

            return value;
        }




        
        public void FixedAsset_ApproveBySelected(string _usersID, string _permissionID, DateTime _dateFrom, DateTime _dateTo)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Declare @IPAddress nvarchar(50) select @IPAddress = IPAddress from Users where ID = @UsersID and Status = 2 \n " +
                                 " \n Insert into Activity(Time,PermissionID,ObjectTable,ObjectTablePK,Status,Message,UsersID,IPAddress,LastUpdate) \n " +
                                 "Select @Time,@PermissionID,'FixedAsset',FixedAssetPK,1,'Approve by Selected Data',@UsersID,@IPAddress,@Time  from FixedAsset where BuyValueDate between @DateFrom and @DateTo and Status = 1 and Selected  = 1 " +
                                 "\n update FixedAsset set status = 2,ApprovedUsersID = @UsersID,ApprovedTime = @Time,LastUpdate=@Time where FixedAssetPK in ( Select FixedAssetPK from FixedAsset where BuyValueDate between @DateFrom and @DateTo and Status = 1 and Selected  = 1 ) \n " +
                                 "Update FixedAsset set status= 3,VoidUsersID=@UsersID,VoidTime=@Time,LastUpdate=@Time  where FixedAssetPK in (Select FixedAssetPK from FixedAsset where BuyValueDate between @DateFrom and @DateTo and Status = 4 and Selected  = 1) " +
                                 " " +
                                 "";
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


        public void FixedAsset_RejectBySelected(string _usersID, string _permissionID, DateTime _dateFrom, DateTime _dateTo)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Declare @IPAddress nvarchar(50) select @IPAddress = IPAddress from Users where ID = @UsersID and Status = 2 \n " +
                                          " \n Insert into Activity(Time,PermissionID,ObjectTable,ObjectTablePK,Status,Message,UsersID,IPAddress,LastUpdate) \n " +
                                          "Select @Time,@PermissionID,'FixedAsset',FixedAssetPK,1,'Reject by Selected Data',@UsersID,@IPAddress,@Time  from FixedAsset where BuyValueDate between @DateFrom and @DateTo and Status = 1 and Selected  = 1 " +
                                          "\n update FixedAsset set status = 3,VoidUsersID = @UsersID,VoidTime = @Time,LastUpdate=@Time where FixedAssetPK in ( Select FixedAssetPK from FixedAsset where BuyValueDate between @DateFrom and @DateTo and Status = 1 and Selected  = 1 ) \n " +
                                          "Update FixedAsset set status= 2  where FixedAssetPK in (Select FixedAssetPK from FixedAsset where BuyValueDate between @DateFrom and @DateTo and Status = 4 and Selected  = 1) " +
                                          " " +
                                          "";
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


        public List<TypeOfAssetsCombo> TypeOfAssets_Combo()
        {


            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<TypeOfAssetsCombo> L_TypeOfAssets = new List<TypeOfAssetsCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "SELECT  TypeOfAssetsPK,ID +' - '+ Name ID, Name FROM [TypeOfAssets]  where status = 2 order by ID,Name";
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    TypeOfAssetsCombo M_TypeOfAssets = new TypeOfAssetsCombo();
                                    M_TypeOfAssets.TypeOfAssetsPK = Convert.ToInt32(dr["TypeOfAssetsPK"]);
                                    M_TypeOfAssets.ID = Convert.ToString(dr["ID"]);
                                    M_TypeOfAssets.Name = Convert.ToString(dr["Name"]);
                                    L_TypeOfAssets.Add(M_TypeOfAssets);
                                }

                            }
                            return L_TypeOfAssets;
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