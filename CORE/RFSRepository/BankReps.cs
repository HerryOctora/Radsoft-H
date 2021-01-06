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
    public class BankReps
    {
        Host _host = new Host();

        //1
        string _insertCommand = "INSERT INTO [dbo].[Bank] " +
                            "([BankPK],[HistoryPK],[Status],[ID],[Name],[BICode],[ClearingCode],[RTGSCode],[BitRDN],[BitSyariah]," +
                            " [FeeLLG],[FeeRTGS],[MinforRTGS],[InterestDays],[JournalRoundingMode],[JournalDecimalPlaces],[SinvestID],[Country],[InterestDaysType],[InterestPaymentType],[PaymentModeOnMaturity],[PaymentInterestSpecificDate],[PTPCode],[USDPTPCode],[NKPDCode],[IssuerPK],[CapitalClassification],";

        string _paramaterCommand = "@ID,@Name,@BICode,@ClearingCode,@RTGSCode,@BitRDN,@BitSyariah," +
                            " @FeeLLG,@FeeRTGS,@MinforRTGS,@InterestDays,@JournalRoundingMode,@JournalDecimalPlaces,@SinvestID,@Country,@InterestDaysType,@InterestPaymentType,@PaymentModeOnMaturity,@PaymentInterestSpecificDate,@PTPCode,@USDPTPCode,@NKPDCode,@IssuerPK,@CapitalClassification,";

        //2
        private Bank setBank(SqlDataReader dr)
        {
            Bank M_Bank = new Bank();
            M_Bank.BankPK = Convert.ToInt32(dr["BankPK"]);
            M_Bank.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_Bank.Status = Convert.ToInt32(dr["Status"]);
            M_Bank.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_Bank.Notes = Convert.ToString(dr["Notes"]);
            M_Bank.ID = dr["ID"].ToString();
            M_Bank.Name = dr["Name"].ToString();
            M_Bank.BICode = dr["BICode"].ToString();
            M_Bank.ClearingCode = Convert.ToString(dr["ClearingCode"]);
            M_Bank.RTGSCode = Convert.ToString(dr["RTGSCode"]);
            M_Bank.PTPCode = Convert.ToString(dr["PTPCode"]);
            M_Bank.USDPTPCode = Convert.ToString(dr["USDPTPCode"]);
            M_Bank.Country = Convert.ToString(dr["Country"]);
            M_Bank.CountryDesc = Convert.ToString(dr["CountryDesc"]);
            M_Bank.BitRDN = Convert.ToBoolean(dr["BitRDN"]);
            M_Bank.BitSyariah = Convert.ToBoolean(dr["BitSyariah"]);
            M_Bank.FeeLLG = Convert.ToDecimal(dr["FeeLLG"]);
            M_Bank.FeeRTGS = Convert.ToDecimal(dr["FeeRTGS"]);
            M_Bank.MinforRTGS = Convert.ToDecimal(dr["MinforRTGS"]);
            M_Bank.SinvestID = Convert.ToString(dr["SinvestID"]);
            M_Bank.InterestDays = Convert.ToInt32(dr["InterestDays"]);
            M_Bank.JournalDecimalPlaces = Convert.ToInt32(dr["JournalDecimalPlaces"]);
            M_Bank.JournalDecimalPlacesDesc = Convert.ToString(dr["JournalDecimalPlacesDesc"]);
            M_Bank.JournalRoundingMode = Convert.ToInt32(dr["JournalRoundingMode"]);
            M_Bank.JournalRoundingModeDesc = Convert.ToString(dr["JournalRoundingModeDesc"]);
            M_Bank.InterestDaysType = Convert.ToInt32(dr["InterestDaysType"]);
            M_Bank.InterestPaymentType = Convert.ToInt32(dr["InterestPaymentType"]);
            M_Bank.PaymentModeOnMaturity = Convert.ToInt32(dr["PaymentModeOnMaturity"]);
            M_Bank.PaymentInterestSpecificDate = dr["PaymentInterestSpecificDate"].ToString();
            M_Bank.NKPDCode = Convert.ToString(dr["NKPDCode"]);
            M_Bank.IssuerPK = dr["IssuerPK"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["IssuerPK"]);
            M_Bank.IssuerID = dr["IssuerID"].ToString();
            M_Bank.CapitalClassification = dr["CapitalClassification"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["CapitalClassification"]);
            M_Bank.CapitalClassificationDesc = dr["CapitalClassificationDesc"].ToString();
            M_Bank.EntryUsersID = dr["EntryUsersID"].ToString();
            M_Bank.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_Bank.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_Bank.VoidUsersID = dr["VoidUsersID"].ToString();
            M_Bank.EntryTime = dr["EntryTime"].ToString();
            M_Bank.UpdateTime = dr["UpdateTime"].ToString();
            M_Bank.ApprovedTime = dr["ApprovedTime"].ToString();
            M_Bank.VoidTime = dr["VoidTime"].ToString();
            M_Bank.DBUserID = dr["DBUserID"].ToString();
            M_Bank.DBTerminalID = dr["DBTerminalID"].ToString();
            M_Bank.LastUpdate = dr["LastUpdate"].ToString();
            M_Bank.LastUpdateDB = Convert.ToString(dr["LastUpdateDB"]);
            return M_Bank;
        }

        public List<Bank> Bank_Select(int _status)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<Bank> L_bank = new List<Bank>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = @"

                              Select case when A.status=1 then 'PENDING' else Case When A.status = 2 then 'APPROVED' else Case when A.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,
                            MV2.DescOne JournalRoundingModeDesc,MV4.DescOne JournalDecimalPlacesDesc,MV5.DescOne CountryDesc,B.ID IssuerID  ,
                            case when A.CapitalClassification = 1 then '1' else case when A.CapitalClassification = 2 then '2' else case when A.CapitalClassification = 3 then '3' else case when A.CapitalClassification = 4 then '4' else '' end end end end CapitalClassificationDesc,* from Bank A
                            left join MasterValue MV2 on A.JournalRoundingMode = MV2.Code and MV2.ID = 'RoundingMode' and MV2.status = 2 
                            left join MasterValue MV4 on A.JournalDecimalPlaces = MV4.Code and MV4.ID = 'DecimalPlaces' and MV4.status = 2 
                            left join MasterValue mv5 on A.Country = mv5.code and  mv5.ID = 'SDICountry' and mv5.status = 2 
                            left Join MasterValue MV6 on A.InterestDaysType = MV6.Code and MV6.ID = 'InterestDaysType' and MV6.Status = 2
                            Left Join MasterValue MV7 on A.InterestPaymentType = MV7.Code and MV7.ID = 'InterestPaymentType' and MV7.Status = 2
                            Left Join MasterValue MV8 on A.PaymentModeOnMaturity = MV8.Code and MV8.ID = 'PaymentModeOnMaturity' and MV8.Status = 2                               
                            Left Join Issuer B on A.IssuerPK = B.IssuerPK and B.Status = 2                               
                             where A.status = @status 
                                                     
                               ";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = @"
                             Select case when A.status=1 then 'PENDING' else Case When A.status = 2 then 'APPROVED' else Case when A.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,
                            MV2.DescOne JournalRoundingModeDesc,MV4.DescOne JournalDecimalPlacesDesc,MV5.DescOne CountryDesc,B.ID IssuerID  ,
                            case when A.CapitalClassification = 1 then '1' else case when A.CapitalClassification = 2 then '2' else case when A.CapitalClassification = 3 then '3' else case when A.CapitalClassification = 4 then '4' else '' end end end end CapitalClassificationDesc,* from Bank A
                            left join MasterValue MV2 on A.JournalRoundingMode = MV2.Code and MV2.ID = 'RoundingMode' and MV2.status = 2 
                            left join MasterValue MV4 on A.JournalDecimalPlaces = MV4.Code and MV4.ID = 'DecimalPlaces' and MV4.status = 2 
                            left join MasterValue mv5 on A.Country = mv5.code and  mv5.ID = 'SDICountry' and mv5.status = 2 
                            left Join MasterValue MV6 on A.InterestDaysType = MV6.Code and MV6.ID = 'InterestDaysType' and MV6.Status = 2
                            Left Join MasterValue MV7 on A.InterestPaymentType = MV7.Code and MV7.ID = 'InterestPaymentType' and MV7.Status = 2
                            Left Join MasterValue MV8 on A.PaymentModeOnMaturity = MV8.Code and MV8.ID = 'PaymentModeOnMaturity' and MV8.Status = 2                            
                            Left Join Issuer B on A.IssuerPK = B.IssuerPK and B.Status = 2   
                        ";
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_bank.Add(setBank(dr));
                                }
                            }
                            return L_bank;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }


        public int Bank_Add(Bank _bank, bool _havePrivillege)
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
                                 "Select isnull(max(BankPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@lastupdate from Bank";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _bank.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],lastupdate)" +
                                "Select isnull(max(BankPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@lastupdate from Bank";
                        }
                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@ID", _bank.ID);
                        cmd.Parameters.AddWithValue("@Name", _bank.Name);
                        cmd.Parameters.AddWithValue("@BICode", _bank.BICode);
                        cmd.Parameters.AddWithValue("@ClearingCode", _bank.ClearingCode);
                        cmd.Parameters.AddWithValue("@RTGSCode", _bank.RTGSCode);
                        cmd.Parameters.AddWithValue("@PTPCode", _bank.PTPCode);
                        cmd.Parameters.AddWithValue("@USDPTPCode", _bank.USDPTPCode);
                        cmd.Parameters.AddWithValue("@Country", _bank.Country);
                        cmd.Parameters.AddWithValue("@BitRDN", _bank.BitRDN);
                        cmd.Parameters.AddWithValue("@BitSyariah", _bank.BitSyariah);
                        cmd.Parameters.AddWithValue("@FeeLLG", _bank.FeeLLG);
                        cmd.Parameters.AddWithValue("@FeeRTGS", _bank.FeeRTGS);
                        cmd.Parameters.AddWithValue("@MinforRTGS", _bank.MinforRTGS);
                        cmd.Parameters.AddWithValue("@SinvestID", _bank.SinvestID);
                        cmd.Parameters.AddWithValue("@InterestDays", _bank.InterestDays);
                        cmd.Parameters.AddWithValue("@JournalDecimalPlaces", _bank.JournalDecimalPlaces);
                        cmd.Parameters.AddWithValue("@JournalRoundingMode", _bank.JournalRoundingMode);
                        cmd.Parameters.AddWithValue("@InterestDaysType", _bank.InterestDaysType);
                        cmd.Parameters.AddWithValue("@InterestPaymentType", _bank.InterestPaymentType);
                        cmd.Parameters.AddWithValue("@PaymentModeOnMaturity", _bank.PaymentModeOnMaturity);
                        cmd.Parameters.AddWithValue("@PaymentInterestSpecificDate", _bank.PaymentInterestSpecificDate);
                        cmd.Parameters.AddWithValue("@NKPDCode", _bank.NKPDCode);
                        cmd.Parameters.AddWithValue("@IssuerPK", _bank.IssuerPK);
                        cmd.Parameters.AddWithValue("@CapitalClassification", _bank.CapitalClassification);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _bank.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);

                        cmd.ExecuteNonQuery();


                        return _host.Get_LastPKByLastUpate(_datetimeNow, "Bank");
                    }


                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int Bank_Update(Bank _bank, bool _havePrivillege)
        {
            try
            {
                int _newHisPK;
                DateTime _datetimeNow = DateTime.Now;
                int status = _host.Get_Status(_bank.BankPK, _bank.HistoryPK, "Bank");
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update Bank set status=2,Notes=@Notes,ID=@ID,Name=@Name,BICode=@BICode," +
                                "ClearingCode=@ClearingCode,RTGSCode=@RTGSCode,PTPCode=@PTPCode,USDPTPCode=@USDPTPCode,BitRDN=@BitRDN,BitSyariah=@BitSyariah,IssuerPK=@IssuerPK,CapitalClassification=@CapitalClassification," +
                                "FeeLLG=@FeeLLG,FeeRTGS=@FeeRTGS,MinforRTGS=@MinforRTGS,InterestDays=@InterestDays,SinvestID = @SinvestID,JournalRoundingMode = @JournalRoundingMode, JournalDecimalPlaces = @JournalDecimalPlaces,Country=@Country, InterestDaysType = @InterestDaysType, InterestPaymentType = @InterestPaymentType, PaymentModeOnMaturity = @PaymentModeOnMaturity,PaymentInterestSpecificDate = @PaymentInterestSpecificDate, NKPDCode = @NKPDCode," +
                                "ApprovedUsersID=@ApprovedUsersID,ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,lastupdate=@lastupdate " +
                                "where BankPK = @PK and historyPK = @HistoryPK";

                            cmd.Parameters.AddWithValue("@HistoryPK", _bank.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _bank.BankPK);
                            cmd.Parameters.AddWithValue("@Notes", _bank.Notes);
                            cmd.Parameters.AddWithValue("@ID", _bank.ID);
                            cmd.Parameters.AddWithValue("@Name", _bank.Name);
                            cmd.Parameters.AddWithValue("@BICode", _bank.BICode);
                            cmd.Parameters.AddWithValue("@ClearingCode", _bank.ClearingCode);
                            cmd.Parameters.AddWithValue("@RTGSCode", _bank.RTGSCode);
                            cmd.Parameters.AddWithValue("@PTPCode", _bank.PTPCode);
                            cmd.Parameters.AddWithValue("@USDPTPCode", _bank.USDPTPCode);
                            cmd.Parameters.AddWithValue("@Country", _bank.Country);
                            cmd.Parameters.AddWithValue("@BitRDN", _bank.BitRDN);
                            cmd.Parameters.AddWithValue("@BitSyariah", _bank.BitSyariah);
                            cmd.Parameters.AddWithValue("@FeeLLG", _bank.FeeLLG);
                            cmd.Parameters.AddWithValue("@FeeRTGS", _bank.FeeRTGS);
                            cmd.Parameters.AddWithValue("@MinforRTGS", _bank.MinforRTGS);
                            cmd.Parameters.AddWithValue("@SinvestID", _bank.SinvestID);
                            cmd.Parameters.AddWithValue("@InterestDays", _bank.InterestDays);
                            cmd.Parameters.AddWithValue("@JournalDecimalPlaces", _bank.JournalDecimalPlaces);
                            cmd.Parameters.AddWithValue("@JournalRoundingMode", _bank.JournalRoundingMode);
                            cmd.Parameters.AddWithValue("@InterestDaysType", _bank.InterestDaysType);
                            cmd.Parameters.AddWithValue("@InterestPaymentType", _bank.InterestPaymentType);
                            cmd.Parameters.AddWithValue("@PaymentModeOnMaturity", _bank.PaymentModeOnMaturity);
                            cmd.Parameters.AddWithValue("@PaymentInterestSpecificDate", _bank.PaymentInterestSpecificDate);
                            cmd.Parameters.AddWithValue("@NKPDCode", _bank.NKPDCode);
                            cmd.Parameters.AddWithValue("@IssuerPK", _bank.IssuerPK);
                            cmd.Parameters.AddWithValue("@CapitalClassification", _bank.CapitalClassification);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _bank.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _bank.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);

                            cmd.ExecuteNonQuery();
                        }

                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update Bank set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,lastupdate=@lastupdate where BankPK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _bank.BankPK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _bank.EntryUsersID);
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
                                cmd.CommandText = "Update Bank set Notes=@Notes,ID=@ID,Name=@Name,BICode=@BICode," +
                                    "ClearingCode=@ClearingCode,RTGSCode=@RTGSCode,PTPCode=@PTPCode,USDPTPCode=@USDPTPCode,BitRDN=@BitRDN,BitSyariah=@BitSyariah,IssuerPK=@IssuerPK,CapitalClassification=@CapitalClassification," +
                                    "FeeLLG=@FeeLLG,FeeRTGS=@FeeRTGS,MinforRTGS=@MinforRTGS,InterestDays=@InterestDays,SinvestID = @SinvestID,JournalRoundingMode = @JournalRoundingMode, JournalDecimalPlaces = @JournalDecimalPlaces,Country=@Country,InterestDaysType = @InterestDaysType, InterestPaymentType = @InterestPaymentType, PaymentModeOnMaturity = @PaymentModeOnMaturity, PaymentInterestSpecificDate = @PaymentInterestSpecificDate, NKPDCode = @NKPDCode," +
                                    "UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,lastupdate=@lastupdate " +
                                    "where BankPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _bank.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _bank.BankPK);
                                cmd.Parameters.AddWithValue("@Notes", _bank.Notes);
                                cmd.Parameters.AddWithValue("@ID", _bank.ID);
                                cmd.Parameters.AddWithValue("@Name", _bank.Name);
                                cmd.Parameters.AddWithValue("@BICode", _bank.BICode);
                                cmd.Parameters.AddWithValue("@ClearingCode", _bank.ClearingCode);
                                cmd.Parameters.AddWithValue("@RTGSCode", _bank.RTGSCode);
                                cmd.Parameters.AddWithValue("@PTPCode", _bank.PTPCode);
                                cmd.Parameters.AddWithValue("@USDPTPCode", _bank.USDPTPCode);
                                cmd.Parameters.AddWithValue("@Country", _bank.Country);
                                cmd.Parameters.AddWithValue("@BitRDN", _bank.BitRDN);
                                cmd.Parameters.AddWithValue("@BitSyariah", _bank.BitSyariah);
                                cmd.Parameters.AddWithValue("@FeeLLG", _bank.FeeLLG);
                                cmd.Parameters.AddWithValue("@FeeRTGS", _bank.FeeRTGS);
                                cmd.Parameters.AddWithValue("@MinforRTGS", _bank.MinforRTGS);
                                cmd.Parameters.AddWithValue("@SinvestID", _bank.SinvestID);
                                cmd.Parameters.AddWithValue("@InterestDays", _bank.InterestDays);
                                cmd.Parameters.AddWithValue("@JournalDecimalPlaces", _bank.JournalDecimalPlaces);
                                cmd.Parameters.AddWithValue("@JournalRoundingMode", _bank.JournalRoundingMode);
                                cmd.Parameters.AddWithValue("@InterestDaysType", _bank.InterestDaysType);
                                cmd.Parameters.AddWithValue("@InterestPaymentType", _bank.InterestPaymentType);
                                cmd.Parameters.AddWithValue("@PaymentModeOnMaturity", _bank.PaymentModeOnMaturity);
                                cmd.Parameters.AddWithValue("@PaymentInterestSpecificDate", _bank.PaymentInterestSpecificDate);
                                cmd.Parameters.AddWithValue("@NKPDCode", _bank.NKPDCode);
                                cmd.Parameters.AddWithValue("@IssuerPK", _bank.IssuerPK);
                                cmd.Parameters.AddWithValue("@CapitalClassification", _bank.CapitalClassification);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _bank.EntryUsersID);
                                cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);

                                cmd.ExecuteNonQuery();
                            }
                            return 0;
                        }
                        else
                        {
                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                _newHisPK = _host.Get_NewHistoryPK(_bank.BankPK, "Bank");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From Bank where BankPK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _bank.BankPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _bank.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@ID", _bank.ID);
                                cmd.Parameters.AddWithValue("@Name", _bank.Name);
                                cmd.Parameters.AddWithValue("@BICode", _bank.BICode);
                                cmd.Parameters.AddWithValue("@ClearingCode", _bank.ClearingCode);
                                cmd.Parameters.AddWithValue("@RTGSCode", _bank.RTGSCode);
                                cmd.Parameters.AddWithValue("@PTPCode", _bank.PTPCode);
                                cmd.Parameters.AddWithValue("@USDPTPCode", _bank.USDPTPCode);
                                cmd.Parameters.AddWithValue("@Country", _bank.Country);
                                cmd.Parameters.AddWithValue("@BitRDN", _bank.BitRDN);
                                cmd.Parameters.AddWithValue("@BitSyariah", _bank.BitSyariah);
                                cmd.Parameters.AddWithValue("@FeeLLG", _bank.FeeLLG);
                                cmd.Parameters.AddWithValue("@FeeRTGS", _bank.FeeRTGS);
                                cmd.Parameters.AddWithValue("@MinforRTGS", _bank.MinforRTGS);
                                cmd.Parameters.AddWithValue("@SinvestID", _bank.SinvestID);
                                cmd.Parameters.AddWithValue("@InterestDays", _bank.InterestDays);
                                cmd.Parameters.AddWithValue("@JournalDecimalPlaces", _bank.JournalDecimalPlaces);
                                cmd.Parameters.AddWithValue("@JournalRoundingMode", _bank.JournalRoundingMode);
                                cmd.Parameters.AddWithValue("@InterestDaysType", _bank.InterestDaysType);
                                cmd.Parameters.AddWithValue("@InterestPaymentType", _bank.InterestPaymentType);
                                cmd.Parameters.AddWithValue("@PaymentModeOnMaturity", _bank.PaymentModeOnMaturity);
                                cmd.Parameters.AddWithValue("@PaymentInterestSpecificDate", _bank.PaymentInterestSpecificDate);
                                cmd.Parameters.AddWithValue("@NKPDCode", _bank.NKPDCode);
                                cmd.Parameters.AddWithValue("@IssuerPK", _bank.IssuerPK);
                                cmd.Parameters.AddWithValue("@CapitalClassification", _bank.CapitalClassification);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _bank.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);

                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update Bank set status= 4,Notes=@Notes, " +
                                " LastUpdate=@lastupdate where BankPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _bank.Notes);
                                cmd.Parameters.AddWithValue("@PK", _bank.BankPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _bank.HistoryPK);
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

        public void Bank_Approved(Bank _bank)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update Bank set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,lastupdate=@lastupdate " +
                            "where BankPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _bank.BankPK);
                        cmd.Parameters.AddWithValue("@historyPK", _bank.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _bank.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update Bank set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,lastupdate=@lastupdate where BankPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _bank.BankPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _bank.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }

                    // UPDATE KE INSTRUMENT UNTUK ISSUERPK
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText =

                        @"update A set A.IssuerPK = B.IssuerPK,A.UpdateUsersID=@ApprovedUsersID,A.UpdateTime=@ApprovedTime,A.lastupdate=@lastupdate from Instrument A
                        left join Bank B on A.BankPK = B.BankPK and B.status in (1,2)
                        where A.BankPK = @PK and A.status in (1,2) and B.Historypk = @historyPK";

                        cmd.Parameters.AddWithValue("@PK", _bank.BankPK);
                        cmd.Parameters.AddWithValue("@historyPK", _bank.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _bank.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
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

        public void Bank_Reject(Bank _bank)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update Bank set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,lastupdate=@lastupdate " +
                            "where BankPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _bank.BankPK);
                        cmd.Parameters.AddWithValue("@historyPK", _bank.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _bank.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update Bank set status= 2,lastupdate=@lastupdate where BankPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _bank.BankPK);
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

        public void Bank_Void(Bank _bank)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update Bank set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime, lastupdate=@lastupdate  " +
                            "where BankPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _bank.BankPK);
                        cmd.Parameters.AddWithValue("@historyPK", _bank.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _bank.VoidUsersID);
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

        public List<BankCombo> Bank_Combo()
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<BankCombo> L_bank = new List<BankCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText =
                        " SELECT  BankPK,ID + ' - ' + Name as ID, Name FROM [Bank] " +
                        " where  status = 2  order by ID,Name ";

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    BankCombo M_bank = new BankCombo();
                                    M_bank.BankPK = Convert.ToInt32(dr["BankPK"]);
                                    M_bank.ID = Convert.ToString(dr["ID"]);
                                    M_bank.Name = Convert.ToString(dr["Name"]);
                                    L_bank.Add(M_bank);
                                }

                            }
                            return L_bank;
                        }
                    }

                }

            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public List<BankCombo> Bank_Combo_RHBOmsBridgingBankBranchOnly()
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<BankCombo> L_bank = new List<BankCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText =
                       @"
                           SELECT  BankPK,ID + ' - ' + Name as ID, Name FROM [Bank]   
	                        where  status = 2  and BankPK in
	                        (
		                        Select distinct BankPK from OMSBridgingBankBranch 
	                        )
	                        order by ID,Name  
                                                ";

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    BankCombo M_bank = new BankCombo();
                                    M_bank.BankPK = Convert.ToInt32(dr["BankPK"]);
                                    M_bank.ID = Convert.ToString(dr["ID"]);
                                    M_bank.Name = Convert.ToString(dr["Name"]);
                                    L_bank.Add(M_bank);
                                }

                            }
                            return L_bank;
                        }
                    }

                }

            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public string Get_BICCodeByBankPK(int _bankPK)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<BankCombo> L_Bank = new List<BankCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {


                        cmd.CommandText = "SELECT BICode FROM [Bank]  where status =2 and BankPK = @BankPK ";
                        cmd.Parameters.AddWithValue("@BankPK", _bankPK);


                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    return Convert.ToString(dr["BICode"]);
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


        public BankCombo Get_BICCodeAndCountryByBankPK(int _bankPK)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {


                        cmd.CommandText = @"SELECT BICode,Country CountryDesc,SinvestID FROM [Bank] A
                        where A.status =2 and BankPK = @BankPK ";
                        cmd.Parameters.AddWithValue("@BankPK", _bankPK);


                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            { // 10 Field
                                dr.Read();
                                BankCombo M_Bank = new BankCombo();
                                M_Bank.BICode = dr["BICode"].ToString();
                                M_Bank.CountryDesc = dr["CountryDesc"].ToString();
                                M_Bank.SInvestID = dr["SinvestID"].ToString();
                                return M_Bank;

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

        public BankInterestInformation Bank_GetBankInterest(int _bankPK)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {


                        cmd.CommandText = @"SELECT PaymentModeOnMaturity,InterestPaymentType,InterestDaysType  FROM [Bank] A
                        where A.status =2 and BankPK = @BankPK ";
                        cmd.Parameters.AddWithValue("@BankPK", _bankPK);


                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            { // 10 Field
                                dr.Read();
                                BankInterestInformation M_Bank = new BankInterestInformation();
                                M_Bank.PaymentModeOnMaturity = Convert.ToInt32(dr["PaymentModeOnMaturity"]);
                                M_Bank.InterestPaymentType = Convert.ToInt32(dr["InterestPaymentType"]);
                                M_Bank.InterestDaysType = Convert.ToInt32(dr["InterestDaysType"]);
                                return M_Bank;
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

        public Boolean Validate_CheckBankPTPCode(int _bankPK)
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
                           
                            IF EXISTS(select PTPCode,USDPTPCode,* from Bank 
                            where (isnull(PTPCode,'') = '') and (isnull(USDPTPCode,'') = '') and BankPK = @BankPK and status = 2)
                            BEGIN
	                            select 1 Result
                            END
                            ELSE
                            BEGIN
	                            select 0 Result
                            END ";


                    
                        cmd.Parameters.AddWithValue("@BankPK", _bankPK);

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

        public List<CurrencyCombo> Get_BankCurrencyByPTPCode(int _bankPK)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<CurrencyCombo> L_Currency = new List<CurrencyCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText =
                         @" 
                            select A.CurrencyPK, B.ID + ' - ' + B.Name ID from (
                            select case when (isnull(PTPCode,'') <> '') then 1 end CurrencyPK from Bank 
                            where BankPK = @BankPK and status = 2
                            union all
                            select case when (isnull(USDPTPCode,'') <> '') then 2 end CurrencyPK from Bank 
                            where BankPK = @BankPK and status = 2) A
                            left join Currency B on A.CurrencyPK = B.CurrencyPK and B.status = 2 
                            where A.CurrencyPK is not null
                            ";

                        cmd.Parameters.AddWithValue("@BankPK", _bankPK);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    CurrencyCombo M_Currency = new CurrencyCombo();
                                    M_Currency.CurrencyPK = Convert.ToInt32(dr["CurrencyPK"]);
                                    M_Currency.ID = Convert.ToString(dr["ID"]);
                                    L_Currency.Add(M_Currency);
                                }

                            }
                            return L_Currency;
                        }
                    }

                }

            }
            catch (Exception err)
            {
                throw err;
            }

        }


        public List<BankCombo> Bank_ComboRpt()
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<BankCombo> L_Bank = new List<BankCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "SELECT  BankPK,ID + ' - ' + Name as ID, Name FROM [Bank]  where status = 2 union all select 0,'All', '' order by BankPK,Name";
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    BankCombo M_Bank = new BankCombo();
                                    M_Bank.BankPK = Convert.ToInt32(dr["BankPK"]);
                                    M_Bank.ID = Convert.ToString(dr["ID"]);
                                    M_Bank.Name = Convert.ToString(dr["Name"]);
                                    L_Bank.Add(M_Bank);
                                }

                            }
                            return L_Bank;
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