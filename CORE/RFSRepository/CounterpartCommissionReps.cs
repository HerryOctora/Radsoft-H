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



namespace RFSRepository
{
    public class CounterpartCommissionReps
    {
        Host _host = new Host();

        //1
        string _insertCommand = "INSERT INTO [dbo].[CounterpartCommission] " +
                            "([CounterpartCommissionPK],[HistoryPK],[Status],[Date],[CounterpartPK],[BoardType],[CommissionPercent],[LevyPercent],[KPEIPercent],[VATPercent],[WHTPercent],[OTCPercent],[IncomeTaxSellPercent],[IncomeTaxInterestPercent],[IncomeTaxGainPercent],[BitIncludeTax],[BitNoCapitalGainTax]," +
                            "[FundPK],[RoundingCommission],[DecimalCommission],[RoundingLevy],[DecimalLevy],[RoundingKPEI],[DecimalKPEI],[RoundingVAT],[DecimalVAT],[RoundingWHT],[DecimalWHT],[RoundingOTC],[DecimalOTC],[RoundingTaxSell],[DecimalTaxSell],[RoundingTaxInterest],[DecimalTaxInterest],[RoundingTaxGain],[DecimalTaxGain],[OTCAmount],";


        string _paramaterCommand = "@Date,@CounterpartPK,@BoardType,@CommissionPercent,@LevyPercent,@KPEIPercent,@VATPercent,@WHTPercent,@OTCPercent,@IncomeTaxSellPercent,@IncomeTaxInterestPercent,@IncomeTaxGainPercent,@BitIncludeTax,@BitNoCapitalGainTax, " +
                                           "@FundPK,@RoundingCommission,@DecimalCommission,@RoundingLevy,@DecimalLevy,@RoundingKPEI,@DecimalKPEI,@RoundingVAT,@DecimalVAT,@RoundingWHT,@DecimalWHT,@RoundingOTC,@DecimalOTC,@RoundingTaxSell,@DecimalTaxSell,@RoundingTaxInterest,@DecimalTaxInterest,@RoundingTaxGain,@DecimalTaxGain,@OTCAmount,";


        //2
        private CounterpartCommission setCounterpartCommission(SqlDataReader dr)
        {
            CounterpartCommission M_CounterpartCommission = new CounterpartCommission();
            M_CounterpartCommission.CounterpartCommissionPK = Convert.ToInt32(dr["CounterpartCommissionPK"]);
            M_CounterpartCommission.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_CounterpartCommission.Status = Convert.ToInt32(dr["Status"]);
            M_CounterpartCommission.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_CounterpartCommission.Notes = Convert.ToString(dr["Notes"]);
            M_CounterpartCommission.Date = Convert.ToString(dr["Date"]);
            M_CounterpartCommission.CounterpartPK = Convert.ToInt32(dr["CounterpartPK"]);
            M_CounterpartCommission.CounterpartID = Convert.ToString(dr["CounterpartID"]);
            M_CounterpartCommission.CounterpartName = Convert.ToString(dr["CounterpartName"]);
            M_CounterpartCommission.BitIncludeTax = Convert.ToBoolean(dr["BitIncludeTax"]);
            M_CounterpartCommission.BoardType = Convert.ToInt32(dr["BoardType"]);
            M_CounterpartCommission.BoardTypeDesc = Convert.ToString(dr["BoardTypeDesc"]);
            M_CounterpartCommission.BrokerFeePercent = Convert.ToDecimal(dr["BrokerFeePercent"]);
            M_CounterpartCommission.CommissionPercent = Convert.ToDecimal(dr["CommissionPercent"]);
            M_CounterpartCommission.LevyPercent = Convert.ToDecimal(dr["LevyPercent"]);
            M_CounterpartCommission.KPEIPercent = Convert.ToDecimal(dr["KPEIPercent"]);
            M_CounterpartCommission.VATPercent = Convert.ToDecimal(dr["VATPercent"]);
            M_CounterpartCommission.WHTPercent = Convert.ToDecimal(dr["WHTPercent"]);
            M_CounterpartCommission.OTCPercent = Convert.ToDecimal(dr["OTCPercent"]);
            M_CounterpartCommission.IncomeTaxSellPercent = Convert.ToDecimal(dr["IncomeTaxSellPercent"]);
            M_CounterpartCommission.IncomeTaxInterestPercent = Convert.ToDecimal(dr["IncomeTaxInterestPercent"]);
            M_CounterpartCommission.IncomeTaxGainPercent = Convert.ToDecimal(dr["IncomeTaxGainPercent"]);
            M_CounterpartCommission.FundPK = Convert.ToInt32(dr["FundPK"]);
            M_CounterpartCommission.FundID = Convert.ToString(dr["FundID"]);
            M_CounterpartCommission.RoundingCommission = Convert.ToDecimal(dr["RoundingCommission"]);
            M_CounterpartCommission.RoundingCommissionDesc = Convert.ToString(dr["RoundingCommissionDesc"]);
            M_CounterpartCommission.DecimalCommission = Convert.ToDecimal(dr["DecimalCommission"]);
            M_CounterpartCommission.RoundingLevy = Convert.ToDecimal(dr["RoundingLevy"]);
            M_CounterpartCommission.RoundingLevyDesc = Convert.ToString(dr["RoundingLevyDesc"]);
            M_CounterpartCommission.DecimalLevy = Convert.ToDecimal(dr["DecimalLevy"]);
            M_CounterpartCommission.RoundingKPEI = Convert.ToDecimal(dr["RoundingKPEI"]);
            M_CounterpartCommission.RoundingKPEIDesc = Convert.ToString(dr["RoundingKPEIDesc"]);
            M_CounterpartCommission.DecimalKPEI = Convert.ToDecimal(dr["DecimalKPEI"]);
            M_CounterpartCommission.RoundingVAT = Convert.ToDecimal(dr["RoundingVAT"]);
            M_CounterpartCommission.RoundingVATDesc = Convert.ToString(dr["RoundingVATDesc"]);
            M_CounterpartCommission.DecimalVAT = Convert.ToDecimal(dr["DecimalVAT"]);
            M_CounterpartCommission.RoundingWHT = Convert.ToDecimal(dr["RoundingWHT"]);
            M_CounterpartCommission.RoundingWHTDesc = Convert.ToString(dr["RoundingWHTDesc"]);
            M_CounterpartCommission.DecimalWHT = Convert.ToDecimal(dr["DecimalWHT"]);
            M_CounterpartCommission.RoundingOTC = Convert.ToDecimal(dr["RoundingOTC"]);
            M_CounterpartCommission.RoundingOTCDesc = Convert.ToString(dr["RoundingOTCDesc"]);
            M_CounterpartCommission.DecimalOTC = Convert.ToDecimal(dr["DecimalOTC"]);
            M_CounterpartCommission.RoundingTaxSell = Convert.ToDecimal(dr["RoundingTaxSell"]);
            M_CounterpartCommission.RoundingTaxSellDesc = Convert.ToString(dr["RoundingTaxSellDesc"]);
            M_CounterpartCommission.DecimalTaxSell = Convert.ToDecimal(dr["DecimalTaxSell"]);
            M_CounterpartCommission.RoundingTaxInterest = Convert.ToDecimal(dr["RoundingTaxInterest"]);
            M_CounterpartCommission.RoundingTaxInterestDesc = Convert.ToString(dr["RoundingTaxInterestDesc"]);
            M_CounterpartCommission.DecimalTaxInterest = Convert.ToDecimal(dr["DecimalTaxInterest"]);
            M_CounterpartCommission.RoundingTaxGain = Convert.ToDecimal(dr["RoundingTaxGain"]);
            M_CounterpartCommission.RoundingTaxGainDesc = Convert.ToString(dr["RoundingTaxGainDesc"]);
            M_CounterpartCommission.DecimalTaxGain = Convert.ToDecimal(dr["DecimalTaxGain"]);

            M_CounterpartCommission.TotalSumFee = Convert.ToDecimal(dr["TotalSumFee"]);

            M_CounterpartCommission.EntryUsersID = dr["EntryUsersID"].ToString();
            M_CounterpartCommission.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_CounterpartCommission.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_CounterpartCommission.VoidUsersID = dr["VoidUsersID"].ToString();
            M_CounterpartCommission.EntryTime = dr["EntryTime"].ToString();
            M_CounterpartCommission.UpdateTime = dr["UpdateTime"].ToString();
            M_CounterpartCommission.ApprovedTime = dr["ApprovedTime"].ToString();
            M_CounterpartCommission.VoidTime = dr["VoidTime"].ToString();
            M_CounterpartCommission.DBUserID = dr["DBUserID"].ToString();
            M_CounterpartCommission.DBTerminalID = dr["DBTerminalID"].ToString();
            M_CounterpartCommission.LastUpdate = dr["LastUpdate"].ToString();
            M_CounterpartCommission.LastUpdateDB = Convert.ToString(dr["LastUpdateDB"]);
            if (_host.CheckColumnIsExist(dr, "BitNoCapitalGainTax"))
            {
                M_CounterpartCommission.BitNoCapitalGainTax = dr["BitNoCapitalGainTax"].Equals(DBNull.Value) == true ? false : Convert.ToBoolean(dr["BitNoCapitalGainTax"]);
            }
            if (_host.CheckColumnIsExist(dr, "OTCAmount"))
            {
                M_CounterpartCommission.OTCAmount = dr["OTCAmount"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["OTCAmount"]);
            }


            return M_CounterpartCommission;
        }

        public List<CounterpartCommission> CounterpartCommission_Select(int _status)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {

                    DbCon.Open();
                    List<CounterpartCommission> L_counterpartCommission = new List<CounterpartCommission>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {


                        if (_status != 9)
                        {
                            cmd.CommandText = @"Select case when A.status=1 then 'PENDING' else Case When A.status = 2 then 'APPROVED' else Case when A.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,MV.DescOne BoardTypeDesc,C.ID CounterpartID,C.Name CounterpartName,D.ID FundID,
	                        MV1.DescOne RoundingCommissionDesc,MV2.DescOne DecimalCommissionDesc,MV3.DescOne RoundingLevyDesc,MV4.DescOne DecimalLevyDesc,MV5.DescOne RoundingKPEIDesc,
	                        MV6.DescOne DecimalKPEIDesc,MV7.DescOne RoundingVATDesc,MV8.DescOne DecimalVATDesc,MV9.DescOne RoundingWHTDesc,MV10.DescOne DecimalWHTDesc,
	                        MV11.DescOne RoundingOTCDesc,MV12.DescOne DecimalOTCDesc,MV13.DescOne RoundingTaxSellDesc,MV14.DescOne DecimalTaxSellDesc,MV15.DescOne RoundingTaxInterestDesc,
	                        MV16.DescOne DecimalTaxInterestDesc,MV17.DescOne RoundingTaxGainDesc,MV18.DescOne DecimalTaxGainDesc,
                            ISNULL(ROUND(A.CommissionPercent + A.LevyPercent + A.KPEIPercent + A.VATPercent,4),0) BrokerFeePercent,
                            isnull(ISNULL(A.CommissionPercent,0) + isnull(A.LevyPercent,0) + isnull(A.KPEIPercent,0) + isnull(A.VATPercent,0) + isnull(A.WHTPercent,0) + isnull(A.OTCPercent,0),0) TotalSumFee,
                            * from CounterpartCommission A 
	                        left join MasterValue MV on A.BoardType = MV.Code and MV.Status=2 and MV.ID ='BoardType'
	                        left join MasterValue MV1 on A.RoundingCommission = MV1.Code and MV1.Status=2 and MV1.ID ='RoundingMode'
	                        left join MasterValue MV2 on A.DecimalCommission = MV2.Code and MV2.Status=2 and MV2.ID ='DecimalPlaces' 
	                        left join MasterValue MV3 on A.RoundingLevy = MV3.Code and MV3.Status=2 and MV3.ID ='RoundingMode'
	                        left join MasterValue MV4 on A.DecimalLevy = MV4.Code and MV4.Status=2 and MV4.ID ='DecimalPlaces'
	                        left join MasterValue MV5 on A.RoundingKPEI = MV5.Code and MV5.Status=2 and MV5.ID ='RoundingMode'
	                        left join MasterValue MV6 on A.DecimalKPEI = MV6.Code and MV6.Status=2 and MV6.ID ='DecimalPlaces'   
	                        left join MasterValue MV7 on A.RoundingVAT = MV7.Code and MV7.Status=2 and MV7.ID ='RoundingMode'
	                        left join MasterValue MV8 on A.DecimalVAT = MV8.Code and MV8.Status=2 and MV8.ID ='DecimalPlaces'   
	                        left join MasterValue MV9 on A.RoundingWHT = MV9.Code and MV9.Status=2 and MV9.ID ='RoundingMode'
	                        left join MasterValue MV10 on A.DecimalWHT = MV10.Code and MV10.Status=2 and MV10.ID ='DecimalPlaces'  
	                        left join MasterValue MV11 on A.RoundingOTC = MV11.Code and MV11.Status=2 and MV11.ID ='RoundingMode'
	                        left join MasterValue MV12 on A.DecimalOTC = MV12.Code and MV12.Status=2 and MV12.ID ='DecimalPlaces'  
	                        left join MasterValue MV13 on A.RoundingTaxSell = MV13.Code and MV13.Status=2 and MV13.ID ='RoundingMode'
	                        left join MasterValue MV14 on A.DecimalTaxSell = MV14.Code and MV14.Status=2 and MV14.ID ='DecimalPlaces' 
	                        left join MasterValue MV15 on A.RoundingTaxInterest = MV15.Code and MV15.Status=2 and MV15.ID ='RoundingMode'
	                        left join MasterValue MV16 on A.DecimalTaxInterest = MV16.Code and MV16.Status=2 and MV16.ID ='DecimalPlaces' 
	                        left join MasterValue MV17 on A.RoundingTaxGain = MV17.Code and MV17.Status=2 and MV17.ID ='RoundingMode'
	                        left join MasterValue MV18 on A.DecimalTaxGain = MV18.Code and MV18.Status=2 and MV18.ID ='DecimalPlaces' 
	                        left join Counterpart C on A.CounterpartPK = C.CounterpartPK and C.Status=2 
	                        left join Fund D on A.FundPK = D.FundPK and D.status=2 
	                        where A.status = @status order by CounterpartCommissionPK ";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = @"Select case when A.status=1 then 'PENDING' else Case When A.status = 2 then 'APPROVED' else Case when A.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,MV.DescOne BoardTypeDesc,C.ID CounterpartID,C.Name CounterpartName,D.ID FundID,
	                        MV1.DescOne RoundingCommissionDesc,MV2.DescOne DecimalCommissionDesc,MV3.DescOne RoundingLevyDesc,MV4.DescOne DecimalLevyDesc,MV5.DescOne RoundingKPEIDesc,
	                        MV6.DescOne DecimalKPEIDesc,MV7.DescOne RoundingVATDesc,MV8.DescOne DecimalVATDesc,MV9.DescOne RoundingWHTDesc,MV10.DescOne DecimalWHTDesc,
	                        MV11.DescOne RoundingOTCDesc,MV12.DescOne DecimalOTCDesc,MV13.DescOne RoundingTaxSellDesc,MV14.DescOne DecimalTaxSellDesc,MV15.DescOne RoundingTaxInterestDesc,
	                        MV16.DescOne DecimalTaxInterestDesc,MV17.DescOne RoundingTaxGainDesc,MV18.DescOne DecimalTaxGainDesc,
                            ISNULL(ROUND(A.CommissionPercent + A.LevyPercent + A.KPEIPercent + A.VATPercent,4),0) BrokerFeePercent,
                            isnull(ISNULL(A.CommissionPercent,0) + isnull(A.LevyPercent,0) + isnull(A.KPEIPercent,0) + isnull(A.VATPercent,0) + isnull(A.WHTPercent,0) + isnull(A.OTCPercent,0),0) TotalSumFee,
                            * from CounterpartCommission A 
	                        left join MasterValue MV on A.BoardType = MV.Code and MV.Status=2 and MV.ID ='BoardType'
	                        left join MasterValue MV1 on A.RoundingCommission = MV1.Code and MV1.Status=2 and MV1.ID ='RoundingMode'
	                        left join MasterValue MV2 on A.DecimalCommission = MV2.Code and MV2.Status=2 and MV2.ID ='DecimalPlaces' 
	                        left join MasterValue MV3 on A.RoundingLevy = MV3.Code and MV3.Status=2 and MV3.ID ='RoundingMode'
	                        left join MasterValue MV4 on A.DecimalLevy = MV4.Code and MV4.Status=2 and MV4.ID ='DecimalPlaces'
	                        left join MasterValue MV5 on A.RoundingKPEI = MV5.Code and MV5.Status=2 and MV5.ID ='RoundingMode'
	                        left join MasterValue MV6 on A.DecimalKPEI = MV6.Code and MV6.Status=2 and MV6.ID ='DecimalPlaces'   
	                        left join MasterValue MV7 on A.RoundingVAT = MV7.Code and MV7.Status=2 and MV7.ID ='RoundingMode'
	                        left join MasterValue MV8 on A.DecimalVAT = MV8.Code and MV8.Status=2 and MV8.ID ='DecimalPlaces'   
	                        left join MasterValue MV9 on A.RoundingWHT = MV9.Code and MV9.Status=2 and MV9.ID ='RoundingMode'
	                        left join MasterValue MV10 on A.DecimalWHT = MV10.Code and MV10.Status=2 and MV10.ID ='DecimalPlaces'  
	                        left join MasterValue MV11 on A.RoundingOTC = MV11.Code and MV11.Status=2 and MV11.ID ='RoundingMode'
	                        left join MasterValue MV12 on A.DecimalOTC = MV12.Code and MV12.Status=2 and MV12.ID ='DecimalPlaces'  
	                        left join MasterValue MV13 on A.RoundingTaxSell = MV13.Code and MV13.Status=2 and MV13.ID ='RoundingMode'
	                        left join MasterValue MV14 on A.DecimalTaxSell = MV14.Code and MV14.Status=2 and MV14.ID ='DecimalPlaces' 
	                        left join MasterValue MV15 on A.RoundingTaxInterest = MV15.Code and MV15.Status=2 and MV15.ID ='RoundingMode'
	                        left join MasterValue MV16 on A.DecimalTaxInterest = MV16.Code and MV16.Status=2 and MV16.ID ='DecimalPlaces' 
	                        left join MasterValue MV17 on A.RoundingTaxGain = MV17.Code and MV17.Status=2 and MV17.ID ='RoundingMode'
	                        left join MasterValue MV18 on A.DecimalTaxGain = MV18.Code and MV18.Status=2 and MV18.ID ='DecimalPlaces' 
	                        left join Counterpart C on A.CounterpartPK = C.CounterpartPK and C.Status=2 
	                        left join Fund D on A.FundPK = D.FundPK and D.status=2 
                            order by CounterpartCommissionPK ";
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_counterpartCommission.Add(setCounterpartCommission(dr));
                                }
                            }
                            return L_counterpartCommission;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }


        public int CounterpartCommission_Add(CounterpartCommission _counterpartCommission, bool _havePrivillege)
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
                                 "Select isnull(max(CounterpartCommissionPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@LastUpdate from CounterpartCommission";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _counterpartCommission.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);

                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[LastUpdate])" +
                                "Select isnull(max(CounterpartCommissionPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@LastUpdate from CounterpartCommission";
                        }
                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@Date", _counterpartCommission.Date);
                        cmd.Parameters.AddWithValue("@CounterpartPK", _counterpartCommission.CounterpartPK);
                        cmd.Parameters.AddWithValue("@BoardType", _counterpartCommission.BoardType);
                        cmd.Parameters.AddWithValue("@BitIncludeTax", _counterpartCommission.BitIncludeTax);
                        cmd.Parameters.AddWithValue("@BitNoCapitalGainTax", _counterpartCommission.BitNoCapitalGainTax);
                        cmd.Parameters.AddWithValue("@CommissionPercent", _counterpartCommission.CommissionPercent);
                        cmd.Parameters.AddWithValue("@LevyPercent", _counterpartCommission.LevyPercent);
                        cmd.Parameters.AddWithValue("@KPEIPercent", _counterpartCommission.KPEIPercent);
                        cmd.Parameters.AddWithValue("@VATPercent", _counterpartCommission.VATPercent);
                        cmd.Parameters.AddWithValue("@WHTPercent", _counterpartCommission.WHTPercent);
                        cmd.Parameters.AddWithValue("@OTCPercent", _counterpartCommission.OTCPercent);
                        cmd.Parameters.AddWithValue("@OTCAmount", _counterpartCommission.OTCAmount);
                        cmd.Parameters.AddWithValue("@IncomeTaxSellPercent", _counterpartCommission.IncomeTaxSellPercent);
                        cmd.Parameters.AddWithValue("@IncomeTaxInterestPercent", _counterpartCommission.IncomeTaxInterestPercent);
                        cmd.Parameters.AddWithValue("@IncomeTaxGainPercent", _counterpartCommission.IncomeTaxGainPercent);
                        cmd.Parameters.AddWithValue("@FundPK", _counterpartCommission.FundPK);
                        cmd.Parameters.AddWithValue("@RoundingCommission", _counterpartCommission.RoundingCommission);
                        cmd.Parameters.AddWithValue("@DecimalCommission", _counterpartCommission.DecimalCommission);
                        cmd.Parameters.AddWithValue("@RoundingLevy", _counterpartCommission.RoundingLevy);
                        cmd.Parameters.AddWithValue("@DecimalLevy", _counterpartCommission.DecimalLevy);
                        cmd.Parameters.AddWithValue("@RoundingKPEI", _counterpartCommission.RoundingKPEI);
                        cmd.Parameters.AddWithValue("@DecimalKPEI", _counterpartCommission.DecimalKPEI);
                        cmd.Parameters.AddWithValue("@RoundingVAT", _counterpartCommission.RoundingVAT);
                        cmd.Parameters.AddWithValue("@DecimalVAT", _counterpartCommission.DecimalVAT);
                        cmd.Parameters.AddWithValue("@RoundingWHT", _counterpartCommission.RoundingWHT);
                        cmd.Parameters.AddWithValue("@DecimalWHT", _counterpartCommission.DecimalWHT);
                        cmd.Parameters.AddWithValue("@RoundingOTC", _counterpartCommission.RoundingOTC);
                        cmd.Parameters.AddWithValue("@DecimalOTC", _counterpartCommission.DecimalOTC);
                        cmd.Parameters.AddWithValue("@RoundingTaxSell", _counterpartCommission.RoundingTaxSell);
                        cmd.Parameters.AddWithValue("@DecimalTaxSell", _counterpartCommission.DecimalTaxSell);
                        cmd.Parameters.AddWithValue("@RoundingTaxInterest", _counterpartCommission.RoundingTaxInterest);
                        cmd.Parameters.AddWithValue("@DecimalTaxInterest", _counterpartCommission.DecimalTaxInterest);
                        cmd.Parameters.AddWithValue("@RoundingTaxGain", _counterpartCommission.RoundingTaxGain);
                        cmd.Parameters.AddWithValue("@DecimalTaxGain", _counterpartCommission.DecimalTaxGain);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _counterpartCommission.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);

                        cmd.ExecuteNonQuery();


                        return _host.Get_LastPKByLastUpate(_dateTimeNow, "CounterpartCommission");
                    }


                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }


        public int CounterpartCommission_Update(CounterpartCommission _counterpartCommission, bool _havePrivillege)
        {
            try
            {
                int _newHisPK;
                int status = _host.Get_Status(_counterpartCommission.CounterpartCommissionPK, _counterpartCommission.HistoryPK, "CounterpartCommission");
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update CounterpartCommission set status=2, Notes=@Notes,Date=@Date,CounterpartPK=@CounterpartPK,BoardType=@BoardType,CommissionPercent=@CommissionPercent,LevyPercent=@LevyPercent,KPEIPercent=@KPEIPercent,VATPercent=@VATPercent,WHTPercent=@WHTPercent,OTCAmount = @OTCAmount, " +
                                "OTCPercent=@OTCPercent,IncomeTaxSellPercent=@IncomeTaxSellPercent,IncomeTaxInterestPercent=@IncomeTaxInterestPercent,IncomeTaxGainPercent=@IncomeTaxGainPercent,BitIncludeTax=@BitIncludeTax,BitNoCapitalGainTax = @BitNoCapitalGainTax,@Bit,FundPK=@FundPK,RoundingCommission=@RoundingCommission,DecimalCommission=@DecimalCommission,RoundingLevy=@RoundingLevy,DecimalLevy=@DecimalLevy,RoundingKPEI=@RoundingKPEI,DecimalKPEI=@DecimalKPEI,RoundingVAT=@RoundingVAT,DecimalVAT=@DecimalVAT,RoundingWHT=@RoundingWHT,DecimalWHT=@DecimalWHT,RoundingOTC=@RoundingOTC,DecimalOTC=@DecimalOTC,RoundingTaxSell=@RoundingTaxSell,DecimalTaxSell=@DecimalTaxSell,RoundingTaxInterest=@RoundingTaxInterest,DecimalTaxInterest=@DecimalTaxInterest,RoundingTaxGain=@RoundingTaxGain,DecimalTaxGain=@DecimalTaxGain," +
                                "ApprovedUsersID=@ApprovedUsersID, " +
                                "ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@lastUpdate " +
                                "where CounterpartCommissionPK = @PK and historyPK = @HistoryPK";
                            cmd.Parameters.AddWithValue("@HistoryPK", _counterpartCommission.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _counterpartCommission.CounterpartCommissionPK);
                            cmd.Parameters.AddWithValue("@Notes", _counterpartCommission.Notes);
                            cmd.Parameters.AddWithValue("@Date", _counterpartCommission.Date);
                            cmd.Parameters.AddWithValue("@CounterpartPK", _counterpartCommission.CounterpartPK);
                            cmd.Parameters.AddWithValue("@BoardType", _counterpartCommission.BoardType);
                            cmd.Parameters.AddWithValue("@BitIncludeTax", _counterpartCommission.BitIncludeTax);
                            cmd.Parameters.AddWithValue("@BitNoCapitalGainTax", _counterpartCommission.BitNoCapitalGainTax);
                            cmd.Parameters.AddWithValue("@CommissionPercent", _counterpartCommission.CommissionPercent);
                            cmd.Parameters.AddWithValue("@LevyPercent", _counterpartCommission.LevyPercent);
                            cmd.Parameters.AddWithValue("@KPEIPercent", _counterpartCommission.KPEIPercent);
                            cmd.Parameters.AddWithValue("@VATPercent", _counterpartCommission.VATPercent);
                            cmd.Parameters.AddWithValue("@WHTPercent", _counterpartCommission.WHTPercent);
                            cmd.Parameters.AddWithValue("@OTCPercent", _counterpartCommission.OTCPercent);
                            cmd.Parameters.AddWithValue("@OTCAmount", _counterpartCommission.OTCAmount);
                            cmd.Parameters.AddWithValue("@IncomeTaxSellPercent", _counterpartCommission.IncomeTaxSellPercent);
                            cmd.Parameters.AddWithValue("@IncomeTaxInterestPercent", _counterpartCommission.IncomeTaxInterestPercent);
                            cmd.Parameters.AddWithValue("@IncomeTaxGainPercent", _counterpartCommission.IncomeTaxGainPercent);
                            cmd.Parameters.AddWithValue("@FundPK", _counterpartCommission.FundPK);
                            cmd.Parameters.AddWithValue("@RoundingCommission", _counterpartCommission.RoundingCommission);
                            cmd.Parameters.AddWithValue("@DecimalCommission", _counterpartCommission.DecimalCommission);
                            cmd.Parameters.AddWithValue("@RoundingLevy", _counterpartCommission.RoundingLevy);
                            cmd.Parameters.AddWithValue("@DecimalLevy", _counterpartCommission.DecimalLevy);
                            cmd.Parameters.AddWithValue("@RoundingKPEI", _counterpartCommission.RoundingKPEI);
                            cmd.Parameters.AddWithValue("@DecimalKPEI", _counterpartCommission.DecimalKPEI);
                            cmd.Parameters.AddWithValue("@RoundingVAT", _counterpartCommission.RoundingVAT);
                            cmd.Parameters.AddWithValue("@DecimalVAT", _counterpartCommission.DecimalVAT);
                            cmd.Parameters.AddWithValue("@RoundingWHT", _counterpartCommission.RoundingWHT);
                            cmd.Parameters.AddWithValue("@DecimalWHT", _counterpartCommission.DecimalWHT);
                            cmd.Parameters.AddWithValue("@RoundingOTC", _counterpartCommission.RoundingOTC);
                            cmd.Parameters.AddWithValue("@DecimalOTC", _counterpartCommission.DecimalOTC);
                            cmd.Parameters.AddWithValue("@RoundingTaxSell", _counterpartCommission.RoundingTaxSell);
                            cmd.Parameters.AddWithValue("@DecimalTaxSell", _counterpartCommission.DecimalTaxSell);
                            cmd.Parameters.AddWithValue("@RoundingTaxInterest", _counterpartCommission.RoundingTaxInterest);
                            cmd.Parameters.AddWithValue("@DecimalTaxInterest", _counterpartCommission.DecimalTaxInterest);
                            cmd.Parameters.AddWithValue("@RoundingTaxGain", _counterpartCommission.RoundingTaxGain);
                            cmd.Parameters.AddWithValue("@DecimalTaxGain", _counterpartCommission.DecimalTaxGain);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _counterpartCommission.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _dateTimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _counterpartCommission.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);
                            cmd.Parameters.AddWithValue("@lastupdate", _dateTimeNow);
                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update CounterpartCommission set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where CounterpartCommissionPK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _counterpartCommission.CounterpartCommissionPK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _counterpartCommission.EntryUsersID);
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
                                cmd.CommandText = "Update CounterpartCommission set  Notes=@Notes,Date=@Date,CounterpartPK=@CounterpartPK,BoardType=@BoardType,CommissionPercent=@CommissionPercent,LevyPercent=@LevyPercent,KPEIPercent=@KPEIPercent,VATPercent=@VATPercent,WHTPercent=@WHTPercent,OTCAmount = @OTCAmount, " +
                                "OTCPercent=@OTCPercent,IncomeTaxSellPercent=@IncomeTaxSellPercent,IncomeTaxInterestPercent=@IncomeTaxInterestPercent,IncomeTaxGainPercent=@IncomeTaxGainPercent,BitIncludeTax=@BitIncludeTax,BitNoCapitalGainTax = @BitNoCapitalGainTax,FundPK=@FundPK,RoundingCommission=@RoundingCommission,DecimalCommission=@DecimalCommission,RoundingLevy=@RoundingLevy,DecimalLevy=@DecimalLevy,RoundingKPEI=@RoundingKPEI,DecimalKPEI=@DecimalKPEI,RoundingVAT=@RoundingVAT,DecimalVAT=@DecimalVAT,RoundingWHT=@RoundingWHT,DecimalWHT=@DecimalWHT,RoundingOTC=@RoundingOTC,DecimalOTC=@DecimalOTC,RoundingTaxSell=@RoundingTaxSell,DecimalTaxSell=@DecimalTaxSell,RoundingTaxInterest=@RoundingTaxInterest,DecimalTaxInterest=@DecimalTaxInterest,RoundingTaxGain=@RoundingTaxGain,DecimalTaxGain=@DecimalTaxGain," +
                                "UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@LastUpdate " +
                                "where CounterpartCommissionPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _counterpartCommission.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _counterpartCommission.CounterpartCommissionPK);
                                cmd.Parameters.AddWithValue("@Notes", _counterpartCommission.Notes);
                                cmd.Parameters.AddWithValue("@Date", _counterpartCommission.Date);
                                cmd.Parameters.AddWithValue("@CounterpartPK", _counterpartCommission.CounterpartPK);
                                cmd.Parameters.AddWithValue("@BoardType", _counterpartCommission.BoardType);
                                cmd.Parameters.AddWithValue("@BitIncludeTax", _counterpartCommission.BitIncludeTax);
                                cmd.Parameters.AddWithValue("@BitNoCapitalGainTax", _counterpartCommission.BitNoCapitalGainTax);
                                cmd.Parameters.AddWithValue("@CommissionPercent", _counterpartCommission.CommissionPercent);
                                cmd.Parameters.AddWithValue("@LevyPercent", _counterpartCommission.LevyPercent);
                                cmd.Parameters.AddWithValue("@KPEIPercent", _counterpartCommission.KPEIPercent);
                                cmd.Parameters.AddWithValue("@VATPercent", _counterpartCommission.VATPercent);
                                cmd.Parameters.AddWithValue("@WHTPercent", _counterpartCommission.WHTPercent);
                                cmd.Parameters.AddWithValue("@OTCPercent", _counterpartCommission.OTCPercent);
                                cmd.Parameters.AddWithValue("@OTCAmount", _counterpartCommission.OTCAmount);
                                cmd.Parameters.AddWithValue("@IncomeTaxSellPercent", _counterpartCommission.IncomeTaxSellPercent);
                                cmd.Parameters.AddWithValue("@IncomeTaxInterestPercent", _counterpartCommission.IncomeTaxInterestPercent);
                                cmd.Parameters.AddWithValue("@IncomeTaxGainPercent", _counterpartCommission.IncomeTaxGainPercent);
                                cmd.Parameters.AddWithValue("@FundPK", _counterpartCommission.FundPK);
                                cmd.Parameters.AddWithValue("@RoundingCommission", _counterpartCommission.RoundingCommission);
                                cmd.Parameters.AddWithValue("@DecimalCommission", _counterpartCommission.DecimalCommission);
                                cmd.Parameters.AddWithValue("@RoundingLevy", _counterpartCommission.RoundingLevy);
                                cmd.Parameters.AddWithValue("@DecimalLevy", _counterpartCommission.DecimalLevy);
                                cmd.Parameters.AddWithValue("@RoundingKPEI", _counterpartCommission.RoundingKPEI);
                                cmd.Parameters.AddWithValue("@DecimalKPEI", _counterpartCommission.DecimalKPEI);
                                cmd.Parameters.AddWithValue("@RoundingVAT", _counterpartCommission.RoundingVAT);
                                cmd.Parameters.AddWithValue("@DecimalVAT", _counterpartCommission.DecimalVAT);
                                cmd.Parameters.AddWithValue("@RoundingWHT", _counterpartCommission.RoundingWHT);
                                cmd.Parameters.AddWithValue("@DecimalWHT", _counterpartCommission.DecimalWHT);
                                cmd.Parameters.AddWithValue("@RoundingOTC", _counterpartCommission.RoundingOTC);
                                cmd.Parameters.AddWithValue("@DecimalOTC", _counterpartCommission.DecimalOTC);
                                cmd.Parameters.AddWithValue("@RoundingTaxSell", _counterpartCommission.RoundingTaxSell);
                                cmd.Parameters.AddWithValue("@DecimalTaxSell", _counterpartCommission.DecimalTaxSell);
                                cmd.Parameters.AddWithValue("@RoundingTaxInterest", _counterpartCommission.RoundingTaxInterest);
                                cmd.Parameters.AddWithValue("@DecimalTaxInterest", _counterpartCommission.DecimalTaxInterest);
                                cmd.Parameters.AddWithValue("@RoundingTaxGain", _counterpartCommission.RoundingTaxGain);
                                cmd.Parameters.AddWithValue("@DecimalTaxGain", _counterpartCommission.DecimalTaxGain);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _counterpartCommission.EntryUsersID);
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
                                _newHisPK = _host.Get_NewHistoryPK(_counterpartCommission.CounterpartCommissionPK, "CounterpartCommission");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From CounterpartCommission where CounterpartCommissionPK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _counterpartCommission.CounterpartCommissionPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _counterpartCommission.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@Date", _counterpartCommission.Date);
                                cmd.Parameters.AddWithValue("@CounterpartPK", _counterpartCommission.CounterpartPK);
                                cmd.Parameters.AddWithValue("@BoardType", _counterpartCommission.BoardType);
                                cmd.Parameters.AddWithValue("@BitIncludeTax", _counterpartCommission.BitIncludeTax);
                                cmd.Parameters.AddWithValue("@BitNoCapitalGainTax", _counterpartCommission.BitNoCapitalGainTax);
                                cmd.Parameters.AddWithValue("@CommissionPercent", _counterpartCommission.CommissionPercent);
                                cmd.Parameters.AddWithValue("@LevyPercent", _counterpartCommission.LevyPercent);
                                cmd.Parameters.AddWithValue("@KPEIPercent", _counterpartCommission.KPEIPercent);
                                cmd.Parameters.AddWithValue("@VATPercent", _counterpartCommission.VATPercent);
                                cmd.Parameters.AddWithValue("@WHTPercent", _counterpartCommission.WHTPercent);
                                cmd.Parameters.AddWithValue("@OTCPercent", _counterpartCommission.OTCPercent);
                                cmd.Parameters.AddWithValue("@OTCAmount", _counterpartCommission.OTCAmount);
                                cmd.Parameters.AddWithValue("@IncomeTaxSellPercent", _counterpartCommission.IncomeTaxSellPercent);
                                cmd.Parameters.AddWithValue("@IncomeTaxInterestPercent", _counterpartCommission.IncomeTaxInterestPercent);
                                cmd.Parameters.AddWithValue("@IncomeTaxGainPercent", _counterpartCommission.IncomeTaxGainPercent);
                                cmd.Parameters.AddWithValue("@FundPK", _counterpartCommission.FundPK);
                                cmd.Parameters.AddWithValue("@RoundingCommission", _counterpartCommission.RoundingCommission);
                                cmd.Parameters.AddWithValue("@DecimalCommission", _counterpartCommission.DecimalCommission);
                                cmd.Parameters.AddWithValue("@RoundingLevy", _counterpartCommission.RoundingLevy);
                                cmd.Parameters.AddWithValue("@DecimalLevy", _counterpartCommission.DecimalLevy);
                                cmd.Parameters.AddWithValue("@RoundingKPEI", _counterpartCommission.RoundingKPEI);
                                cmd.Parameters.AddWithValue("@DecimalKPEI", _counterpartCommission.DecimalKPEI);
                                cmd.Parameters.AddWithValue("@RoundingVAT", _counterpartCommission.RoundingVAT);
                                cmd.Parameters.AddWithValue("@DecimalVAT", _counterpartCommission.DecimalVAT);
                                cmd.Parameters.AddWithValue("@RoundingWHT", _counterpartCommission.RoundingWHT);
                                cmd.Parameters.AddWithValue("@DecimalWHT", _counterpartCommission.DecimalWHT);
                                cmd.Parameters.AddWithValue("@RoundingOTC", _counterpartCommission.RoundingOTC);
                                cmd.Parameters.AddWithValue("@DecimalOTC", _counterpartCommission.DecimalOTC);
                                cmd.Parameters.AddWithValue("@RoundingTaxSell", _counterpartCommission.RoundingTaxSell);
                                cmd.Parameters.AddWithValue("@DecimalTaxSell", _counterpartCommission.DecimalTaxSell);
                                cmd.Parameters.AddWithValue("@RoundingTaxInterest", _counterpartCommission.RoundingTaxInterest);
                                cmd.Parameters.AddWithValue("@DecimalTaxInterest", _counterpartCommission.DecimalTaxInterest);
                                cmd.Parameters.AddWithValue("@RoundingTaxGain", _counterpartCommission.RoundingTaxGain);
                                cmd.Parameters.AddWithValue("@DecimalTaxGain", _counterpartCommission.DecimalTaxGain);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _counterpartCommission.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _dateTimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update CounterpartCommission set status= 4,Notes=@Notes, " +
                                " lastupdate=@lastupdate where CounterpartCommissionPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _counterpartCommission.Notes);
                                cmd.Parameters.AddWithValue("@PK", _counterpartCommission.CounterpartCommissionPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _counterpartCommission.HistoryPK);
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


        public void CounterpartCommission_Approved(CounterpartCommission _counterpartCommission)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update CounterpartCommission set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,LastUpdate=@LastUpdate " +
                            "where CounterpartCommissionPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _counterpartCommission.CounterpartCommissionPK);
                        cmd.Parameters.AddWithValue("@historyPK", _counterpartCommission.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _counterpartCommission.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update CounterpartCommission set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where CounterpartCommissionPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _counterpartCommission.CounterpartCommissionPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _counterpartCommission.ApprovedUsersID);
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

        public void CounterpartCommission_Reject(CounterpartCommission _counterpartCommission)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update CounterpartCommission set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where CounterpartCommissionPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _counterpartCommission.CounterpartCommissionPK);
                        cmd.Parameters.AddWithValue("@historyPK", _counterpartCommission.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _counterpartCommission.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update CounterpartCommission set status= 2,LastUpdate=@LastUpdate where CounterpartCommissionPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _counterpartCommission.CounterpartCommissionPK);
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

        public void CounterpartCommission_Void(CounterpartCommission _counterpartCommission)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update CounterpartCommission set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@lastUpdate " +
                            "where CounterpartCommissionPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _counterpartCommission.CounterpartCommissionPK);
                        cmd.Parameters.AddWithValue("@historyPK", _counterpartCommission.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _counterpartCommission.VoidUsersID);
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

        public CounterpartCommission Get_CommissionIncludeTax(decimal _brokerFeePercent)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    CounterpartCommission M_CounterpartCommission = new CounterpartCommission();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = @"declare @CommissionPercent numeric (22,12)
                        declare @LevyPercent numeric (22,12)
                        declare @KPEIPercent numeric (22,12)
                        declare @VATPercent numeric (22,12)
                        declare @WHTPercent numeric (22,12)


                        select @LevyPercent = 0.033
                        select @KPEIPercent = 0.01
                        select @CommissionPercent = (@ParamComm - (@LevyPercent + @KPEIPercent))/1.1 
                        select @VATPercent = 0.1 * @CommissionPercent
                        select @WHTPercent = 0.02 * @CommissionPercent
                        select @CommissionPercent CommissionPercent,@LevyPercent LevyPercent,@KPEIPercent KPEIPercent,@VATPercent VATPercent,@WHTPercent WHTPercent  ";

                        cmd.Parameters.AddWithValue("@ParamComm", _brokerFeePercent);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {

                                    M_CounterpartCommission.CommissionPercent = Convert.ToDecimal(dr["CommissionPercent"]);
                                    M_CounterpartCommission.LevyPercent = Convert.ToDecimal(dr["LevyPercent"]);
                                    M_CounterpartCommission.KPEIPercent = Convert.ToDecimal(dr["KPEIPercent"]);
                                    M_CounterpartCommission.VATPercent = Convert.ToDecimal(dr["VATPercent"]);
                                    M_CounterpartCommission.WHTPercent = Convert.ToDecimal(dr["WHTPercent"]);

                                    return M_CounterpartCommission;
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


        public int Validate_UpdateSettlement(int _counterpartPK , int _boardType)
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
                        if Exists
                        (select * From CounterpartCommission where Counterpartpk = @CounterpartPK and BoardType = @BoardType)
                        BEGIN 
                        Select 1 Result 
                        END 
                        ELSE 
                        BEGIN     
                        Select 0 Result 
                        END       ";

                        cmd.Parameters.AddWithValue("@CounterpartPK", _counterpartPK);
                        cmd.Parameters.AddWithValue("@BoardType", _boardType);

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