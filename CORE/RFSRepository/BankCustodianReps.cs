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
    public class BankCustodianReps
    {
        Host _host = new Host();

        //1
        string _insertCommand = "INSERT INTO [dbo].[BankCustodian] " +
                            "([BankCustodianPK],[HistoryPK],[Status],[ID],[Name],[Type],[Address],[Branch],[ContactPerson],[Fax1],[Fax2],[Fax3], " +
                            " [Email1],[Email2],[Email3],[Attn1],[Attn2],[Attn3],[BICode],[FundAccountPK],[ClearingCode],[RTGSCode],[BitRDN],[BitSyariah],[City],[Phone1],[Phone2],[Phone3]," +
                            " [FeeLLG],[FeeRTGS],[MinforRTGS],[JournalRoundingMode],[JournalDecimalPlaces],";

        string _paramaterCommand = "@ID,@Name,@Type,@Address,@Branch,@ContactPerson,@Fax1,@Fax2,@Fax3, " +
                            " @Email1,@Email2,@Email3,@Attn1,@Attn2,@Attn3,@BICode,@FundAccountPK,@ClearingCode,@RTGSCode,@BitRDN,@BitSyariah,@City,@Phone1,@Phone2,@Phone3," +
                            " @FeeLLG,@FeeRTGS,@MinforRTGS,@JournalRoundingMode,@JournalDecimalPlaces,";

        //2
        private BankCustodian setBankCustodian(SqlDataReader dr)
        {
            BankCustodian M_BankCustodian = new BankCustodian();
            M_BankCustodian.BankCustodianPK = Convert.ToInt32(dr["BankCustodianPK"]);
            M_BankCustodian.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_BankCustodian.Status = Convert.ToInt32(dr["Status"]);
            M_BankCustodian.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_BankCustodian.Notes = Convert.ToString(dr["Notes"]);
            M_BankCustodian.ID = dr["ID"].ToString();
            M_BankCustodian.Name = dr["Name"].ToString();
            M_BankCustodian.Type = Convert.ToInt32(dr["Type"]);
            M_BankCustodian.TypeDesc = Convert.ToString(dr["TypeDesc"]);
            M_BankCustodian.Address = dr["Address"].ToString();
            M_BankCustodian.Branch = dr["Branch"].ToString();
            M_BankCustodian.ContactPerson = dr["ContactPerson"].ToString();
            M_BankCustodian.Fax1 = dr["Fax1"].ToString();
            M_BankCustodian.Fax2 = dr["Fax2"].ToString();
            M_BankCustodian.Fax3 = dr["Fax3"].ToString();
            M_BankCustodian.Email1 = dr["Email1"].ToString();
            M_BankCustodian.Email2 = dr["Email2"].ToString();
            M_BankCustodian.Email3 = dr["Email3"].ToString();
            M_BankCustodian.Attn1= dr["Attn1"].ToString();
            M_BankCustodian.Attn2 = dr["Attn2"].ToString();
            M_BankCustodian.Attn3 = dr["Attn3"].ToString();
            M_BankCustodian.BICode = dr["BICode"].ToString();
            M_BankCustodian.FundAccountPK = Convert.ToInt32(dr["FundAccountPK"]);
            M_BankCustodian.FundAccountID = Convert.ToString(dr["FundAccountID"]);
            M_BankCustodian.ClearingCode = Convert.ToString(dr["ClearingCode"]);
            M_BankCustodian.RTGSCode = Convert.ToString(dr["RTGSCode"]);
            M_BankCustodian.BitRDN = Convert.ToBoolean(dr["BitRDN"]);
            M_BankCustodian.BitSyariah = Convert.ToBoolean(dr["BitSyariah"]);
            M_BankCustodian.City = Convert.ToInt32(dr["City"]);
            M_BankCustodian.CityDesc = Convert.ToString(dr["CityDesc"]);
            M_BankCustodian.Phone1 = Convert.ToString(dr["Phone1"]);
            M_BankCustodian.Phone2 = Convert.ToString(dr["Phone2"]);
            M_BankCustodian.Phone3 = Convert.ToString(dr["Phone3"]);
            M_BankCustodian.FeeLLG = Convert.ToDecimal(dr["FeeLLG"]);
            M_BankCustodian.FeeRTGS = Convert.ToDecimal(dr["FeeRTGS"]);
            M_BankCustodian.MinforRTGS = Convert.ToDecimal(dr["MinforRTGS"]);
            M_BankCustodian.JournalRoundingMode = Convert.ToInt32(dr["JournalRoundingMode"]);
            M_BankCustodian.JournalRoundingModeDesc = Convert.ToString(dr["JournalRoundingModeDesc"]);
            M_BankCustodian.JournalDecimalPlaces = Convert.ToInt32(dr["JournalDecimalPlaces"]);
            M_BankCustodian.JournalDecimalPlacesDesc = Convert.ToString(dr["JournalDecimalPlacesDesc"]);
            M_BankCustodian.EntryUsersID = dr["EntryUsersID"].ToString();
            M_BankCustodian.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_BankCustodian.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_BankCustodian.VoidUsersID = dr["VoidUsersID"].ToString();
            M_BankCustodian.EntryTime = dr["EntryTime"].ToString();
            M_BankCustodian.UpdateTime = dr["UpdateTime"].ToString();
            M_BankCustodian.ApprovedTime = dr["ApprovedTime"].ToString();
            M_BankCustodian.VoidTime = dr["VoidTime"].ToString();
            M_BankCustodian.DBUserID = dr["DBUserID"].ToString();
            M_BankCustodian.DBTerminalID = dr["DBTerminalID"].ToString();
            M_BankCustodian.LastUpdate = dr["LastUpdate"].ToString();
            M_BankCustodian.LastUpdateDB = Convert.ToString(dr["LastUpdateDB"]);
            return M_BankCustodian;
        }

        public List<BankCustodian> BankCustodian_Select(int _status)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<BankCustodian> L_BankCustodian = new List<BankCustodian>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = "Select case when B.status=1 then 'PENDING' else Case When B.status = 2 then 'APPROVED' else Case when B.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,A.Name FundAccountID,MV.DescOne TypeDesc,MV1.DescOne CityDesc,MV2.DescOne JournalRoundingModeDesc,MV4.DescOne JournalDecimalPlacesDesc,B.* from BankCustodian B " +
                            " left join Account A on B.FundAccountPK = A.AccountPK and A.status=2 " +
                            " left join MasterValue MV on B.Type = MV.Code and MV.ID ='BankCustodianType' and MV.status = 2 " +
                            " left join MasterValue MV1 on B.City = MV1.Code and MV1.ID = 'CityRHB' and MV1.status = 2 " +
                            " left join MasterValue MV2 on B.JournalRoundingMode = MV2.Code and MV2.ID = 'RoundingMode' and MV2.status = 2 " +
                            " left join MasterValue MV4 on B.JournalDecimalPlaces = MV4.Code and MV4.ID = 'DecimalPlaces' and MV4.status = 2 " +
                            " where B.status = @status ";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = "Select case when B.status=1 then 'PENDING' else Case When B.status = 2 then 'APPROVED' else Case when B.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,A.Name FundAccountID,MV.DescOne TypeDesc,MV1.DescOne CityDesc,MV2.DescOne JournalRoundingModeDesc,MV4.DescOne JournalDecimalPlacesDesc,B.* from BankCustodian B " +
                            " left join Account A on B.FundAccountPK = A.AccountPK and A.status=2 " +
                            " left join MasterValue MV on B.Type = MV.Code and MV.ID ='BankCustodianType' and MV.status = 2 " +
                            " left join MasterValue MV1 on B.City = MV1.Code and MV1.ID = 'CityRHB' and MV1.status = 2 " +
                            " left join MasterValue MV2 on B.JournalRoundingMode = MV2.Code and MV2.ID = 'RoundingMode' and MV2.status = 2 " +
                            " left join MasterValue MV4 on B.JournalDecimalPlaces = MV4.Code and MV4.ID = 'DecimalPlaces' and MV4.status = 2 ";

                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_BankCustodian.Add(setBankCustodian(dr));
                                }
                            }
                            return L_BankCustodian;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int BankCustodian_Add(BankCustodian _bankCustodian, bool _havePrivillege)
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
                                 "Select isnull(max(BankCustodianPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@lastupdate from BankCustodian";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _bankCustodian.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],lastupdate)" +
                                "Select isnull(max(BankCustodianPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@lastupdate from BankCustodian";
                        }
                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@ID", _bankCustodian.ID);
                        cmd.Parameters.AddWithValue("@Name", _bankCustodian.Name);
                        cmd.Parameters.AddWithValue("@Type", _bankCustodian.Type);
                        cmd.Parameters.AddWithValue("@Address", _bankCustodian.Address);
                        cmd.Parameters.AddWithValue("@Branch", _bankCustodian.Branch);
                        cmd.Parameters.AddWithValue("@ContactPerson", _bankCustodian.ContactPerson);
                        cmd.Parameters.AddWithValue("@Fax1", _bankCustodian.Fax1);
                        cmd.Parameters.AddWithValue("@Fax2", _bankCustodian.Fax2);
                        cmd.Parameters.AddWithValue("@Fax3", _bankCustodian.Fax3);
                        cmd.Parameters.AddWithValue("@Email1", _bankCustodian.Email1);
                        cmd.Parameters.AddWithValue("@Email2", _bankCustodian.Email2);
                        cmd.Parameters.AddWithValue("@Email3", _bankCustodian.Email3);
                        cmd.Parameters.AddWithValue("@Attn1", _bankCustodian.Attn1);
                        cmd.Parameters.AddWithValue("@Attn2", _bankCustodian.Attn2);
                        cmd.Parameters.AddWithValue("@Attn3", _bankCustodian.Attn3);
                        cmd.Parameters.AddWithValue("@BICode", _bankCustodian.BICode);
                        cmd.Parameters.AddWithValue("@FundAccountPK", _bankCustodian.FundAccountPK);
                        cmd.Parameters.AddWithValue("@ClearingCode", _bankCustodian.ClearingCode);
                        cmd.Parameters.AddWithValue("@RTGSCode", _bankCustodian.RTGSCode);
                        cmd.Parameters.AddWithValue("@BitRDN", _bankCustodian.BitRDN);
                        cmd.Parameters.AddWithValue("@BitSyariah", _bankCustodian.BitSyariah);
                        cmd.Parameters.AddWithValue("@City", _bankCustodian.City);
                        cmd.Parameters.AddWithValue("@Phone1", _bankCustodian.Phone1);
                        cmd.Parameters.AddWithValue("@Phone2", _bankCustodian.Phone2);
                        cmd.Parameters.AddWithValue("@Phone3", _bankCustodian.Phone3);
                        cmd.Parameters.AddWithValue("@FeeLLG", _bankCustodian.FeeLLG);
                        cmd.Parameters.AddWithValue("@FeeRTGS", _bankCustodian.FeeRTGS);
                        cmd.Parameters.AddWithValue("@MinforRTGS", _bankCustodian.MinforRTGS);
                        cmd.Parameters.AddWithValue("@JournalRoundingMode", _bankCustodian.JournalRoundingMode);
                        cmd.Parameters.AddWithValue("@JournalDecimalPlaces", _bankCustodian.JournalDecimalPlaces);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _bankCustodian.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);

                        cmd.ExecuteNonQuery();


                        return _host.Get_LastPKByLastUpate(_datetimeNow, "BankCustodian");
                    }


                }
            }
            catch (Exception err)
            {
                throw err;
            }
           
        }

        public int BankCustodian_Update(BankCustodian _bankCustodian, bool _havePrivillege)
       {
            try
            {
                int _newHisPK;
                DateTime _datetimeNow = DateTime.Now;
                int status = _host.Get_Status(_bankCustodian.BankCustodianPK, _bankCustodian.HistoryPK, "bankCustodian");
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                        cmd.CommandText = "Update BankCustodian set status=2,Notes=@Notes,ID=@ID,Name=@Name,Type=@Type," +
                            "Address=@Address,Branch=@Branch,ContactPerson=@ContactPerson," +
                            "Fax1=@Fax1,Fax2=@Fax2,Fax3=@Fax3,Email1=@Email1,Email2=@Email2,Email3=@Email3,Attn1=@Attn1,Attn2=@Attn2,Attn3=@Attn3,BICode=@BICode," +
                            "FundAccountPK=@FundAccountPK,ClearingCode=@ClearingCode,RTGSCode=@RTGSCode,BitRDN=@BitRDN,BitSyariah=@BitSyariah," +
                            "City=@City,Phone1=@Phone1,Phone2=@Phone2,Phone3=@Phone3," +
                            "FeeLLG=@FeeLLG,FeeRTGS=@FeeRTGS,MinforRTGS=@MinforRTGS,JournalRoundingMode=@JournalRoundingMode,JournalDecimalPlaces=@JournalDecimalPlaces, " +
                            "ApprovedUsersID=@ApprovedUsersID,ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,lastupdate=@lastupdate " +
                            "where BankCustodianPK = @PK and historyPK = @HistoryPK";

                        cmd.Parameters.AddWithValue("@HistoryPK", _bankCustodian.HistoryPK);
                        cmd.Parameters.AddWithValue("@PK", _bankCustodian.BankCustodianPK);
                        cmd.Parameters.AddWithValue("@Notes", _bankCustodian.Notes);
                        cmd.Parameters.AddWithValue("@ID", _bankCustodian.ID);
                        cmd.Parameters.AddWithValue("@Name", _bankCustodian.Name);
                        cmd.Parameters.AddWithValue("@Type", _bankCustodian.Type);
                        cmd.Parameters.AddWithValue("@Address", _bankCustodian.Address);
                        cmd.Parameters.AddWithValue("@Branch", _bankCustodian.Branch);
                        cmd.Parameters.AddWithValue("@ContactPerson", _bankCustodian.ContactPerson);
                        cmd.Parameters.AddWithValue("@Fax1", _bankCustodian.Fax1);
                        cmd.Parameters.AddWithValue("@Fax2", _bankCustodian.Fax2);
                        cmd.Parameters.AddWithValue("@Fax3", _bankCustodian.Fax3);
                        cmd.Parameters.AddWithValue("@Email1", _bankCustodian.Email1);
                        cmd.Parameters.AddWithValue("@Email2", _bankCustodian.Email2);
                        cmd.Parameters.AddWithValue("@Email3", _bankCustodian.Email3);
                        cmd.Parameters.AddWithValue("@Attn1", _bankCustodian.Attn1);
                        cmd.Parameters.AddWithValue("@Attn2", _bankCustodian.Attn2);
                        cmd.Parameters.AddWithValue("@Attn3", _bankCustodian.Attn3);
                        cmd.Parameters.AddWithValue("@BICode", _bankCustodian.BICode);
                        cmd.Parameters.AddWithValue("@FundAccountPK", _bankCustodian.FundAccountPK);
                        cmd.Parameters.AddWithValue("@ClearingCode", _bankCustodian.ClearingCode);
                        cmd.Parameters.AddWithValue("@RTGSCode", _bankCustodian.RTGSCode);
                        cmd.Parameters.AddWithValue("@BitRDN", _bankCustodian.BitRDN);
                        cmd.Parameters.AddWithValue("@BitSyariah", _bankCustodian.BitSyariah);
                        cmd.Parameters.AddWithValue("@City", _bankCustodian.City);
                        cmd.Parameters.AddWithValue("@Phone1", _bankCustodian.Phone1);
                        cmd.Parameters.AddWithValue("@Phone2", _bankCustodian.Phone2);
                        cmd.Parameters.AddWithValue("@Phone3", _bankCustodian.Phone3);
                        cmd.Parameters.AddWithValue("@FeeLLG", _bankCustodian.FeeLLG);
                        cmd.Parameters.AddWithValue("@FeeRTGS", _bankCustodian.FeeRTGS);
                        cmd.Parameters.AddWithValue("@MinforRTGS", _bankCustodian.MinforRTGS);
                        cmd.Parameters.AddWithValue("@JournalRoundingMode", _bankCustodian.JournalRoundingMode);
                        cmd.Parameters.AddWithValue("@JournalDecimalPlaces", _bankCustodian.JournalDecimalPlaces);
                        cmd.Parameters.AddWithValue("@UpdateUsersID", _bankCustodian.EntryUsersID);
                        cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _bankCustodian.EntryUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);

                        cmd.ExecuteNonQuery();
                    }

                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update BankCustodian set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,lastupdate=@lastupdate where BankCustodianPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _bankCustodian.BankCustodianPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _bankCustodian.EntryUsersID);
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
                            cmd.CommandText = "Update BankCustodian set Notes=@Notes,ID=@ID,Name=@Name,Type=@Type," +
                                "Address=@Address,Branch=@Branch,ContactPerson=@ContactPerson," +
                                "Fax1=@Fax1,Fax2=@Fax2,Fax3=@Fax3,Email1=@Email1,Email2=@Email2,Email3=@Email3,Attn1=@Attn1,Attn2=@Attn2,Attn3=@Attn3,BICode=@BICode," +
                                "FundAccountPK=@FundAccountPK,ClearingCode=@ClearingCode,RTGSCode=@RTGSCode,BitRDN=@BitRDN,BitSyariah=@BitSyariah," +
                                "City=@City,Phone1=@Phone1,Phone2=@Phone2,Phone3=@Phone3," +
                                "FeeLLG=@FeeLLG,FeeRTGS=@FeeRTGS,MinforRTGS=@MinforRTGS,JournalRoundingMode=@JournalRoundingMode,JournalDecimalPlaces=@JournalDecimalPlaces, " +
                                "UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,lastupdate=@lastupdate " +
                                "where BankCustodianPK = @PK and historyPK = @HistoryPK";
                            cmd.Parameters.AddWithValue("@HistoryPK", _bankCustodian.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _bankCustodian.BankCustodianPK);
                            cmd.Parameters.AddWithValue("@Notes", _bankCustodian.Notes);
                            cmd.Parameters.AddWithValue("@ID", _bankCustodian.ID);
                            cmd.Parameters.AddWithValue("@Name", _bankCustodian.Name);
                            cmd.Parameters.AddWithValue("@Type", _bankCustodian.Type);
                            cmd.Parameters.AddWithValue("@Address", _bankCustodian.Address);
                            cmd.Parameters.AddWithValue("@Branch", _bankCustodian.Branch);
                            cmd.Parameters.AddWithValue("@ContactPerson", _bankCustodian.ContactPerson);
                            cmd.Parameters.AddWithValue("@Fax1", _bankCustodian.Fax1);
                            cmd.Parameters.AddWithValue("@Fax2", _bankCustodian.Fax2);
                            cmd.Parameters.AddWithValue("@Fax3", _bankCustodian.Fax3);
                            cmd.Parameters.AddWithValue("@Email1", _bankCustodian.Email1);
                            cmd.Parameters.AddWithValue("@Email2", _bankCustodian.Email2);
                            cmd.Parameters.AddWithValue("@Email3", _bankCustodian.Email3);
                            cmd.Parameters.AddWithValue("@Attn1", _bankCustodian.Attn1);
                            cmd.Parameters.AddWithValue("@Attn2", _bankCustodian.Attn2);
                            cmd.Parameters.AddWithValue("@Attn3", _bankCustodian.Attn3);
                            cmd.Parameters.AddWithValue("@BICode", _bankCustodian.BICode);
                            cmd.Parameters.AddWithValue("@FundAccountPK", _bankCustodian.FundAccountPK);
                            cmd.Parameters.AddWithValue("@ClearingCode", _bankCustodian.ClearingCode);
                            cmd.Parameters.AddWithValue("@RTGSCode", _bankCustodian.RTGSCode);
                            cmd.Parameters.AddWithValue("@BitRDN", _bankCustodian.BitRDN);
                            cmd.Parameters.AddWithValue("@BitSyariah", _bankCustodian.BitSyariah);
                            cmd.Parameters.AddWithValue("@City", _bankCustodian.City);
                            cmd.Parameters.AddWithValue("@Phone1", _bankCustodian.Phone1);
                            cmd.Parameters.AddWithValue("@Phone2", _bankCustodian.Phone2);
                            cmd.Parameters.AddWithValue("@Phone3", _bankCustodian.Phone3);
                            cmd.Parameters.AddWithValue("@FeeLLG", _bankCustodian.FeeLLG);
                            cmd.Parameters.AddWithValue("@FeeRTGS", _bankCustodian.FeeRTGS);
                            cmd.Parameters.AddWithValue("@MinforRTGS", _bankCustodian.MinforRTGS);
                            cmd.Parameters.AddWithValue("@JournalRoundingMode", _bankCustodian.JournalRoundingMode);
                            cmd.Parameters.AddWithValue("@JournalDecimalPlaces", _bankCustodian.JournalDecimalPlaces);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _bankCustodian.EntryUsersID);
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
                            _newHisPK = _host.Get_NewHistoryPK(_bankCustodian.BankCustodianPK, "BankCustodian");
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                            "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                            "From BankCustodian where BankCustodianPK =@PK and historyPK = @HistoryPK ";

                            cmd.Parameters.AddWithValue("@PK", _bankCustodian.BankCustodianPK);
                            cmd.Parameters.AddWithValue("@HistoryPK", _bankCustodian.HistoryPK);
                            cmd.Parameters.AddWithValue("@NewHistoryPK",_newHisPK);
                            cmd.Parameters.AddWithValue("@ID", _bankCustodian.ID);
                            cmd.Parameters.AddWithValue("@Name", _bankCustodian.Name);
                            cmd.Parameters.AddWithValue("@Type", _bankCustodian.Type);
                            cmd.Parameters.AddWithValue("@Address", _bankCustodian.Address);
                            cmd.Parameters.AddWithValue("@Branch", _bankCustodian.Branch);
                            cmd.Parameters.AddWithValue("@ContactPerson", _bankCustodian.ContactPerson);
                            cmd.Parameters.AddWithValue("@Fax1", _bankCustodian.Fax1);
                            cmd.Parameters.AddWithValue("@Fax2", _bankCustodian.Fax2);
                            cmd.Parameters.AddWithValue("@Fax3", _bankCustodian.Fax3);
                            cmd.Parameters.AddWithValue("@Email1", _bankCustodian.Email1);
                            cmd.Parameters.AddWithValue("@Email2", _bankCustodian.Email2);
                            cmd.Parameters.AddWithValue("@Email3", _bankCustodian.Email3);
                            cmd.Parameters.AddWithValue("@Attn1", _bankCustodian.Attn1);
                            cmd.Parameters.AddWithValue("@Attn2", _bankCustodian.Attn2);
                            cmd.Parameters.AddWithValue("@Attn3", _bankCustodian.Attn3);
                            cmd.Parameters.AddWithValue("@BICode", _bankCustodian.BICode);
                            cmd.Parameters.AddWithValue("@FundAccountPK", _bankCustodian.FundAccountPK);
                            cmd.Parameters.AddWithValue("@ClearingCode", _bankCustodian.ClearingCode);
                            cmd.Parameters.AddWithValue("@RTGSCode", _bankCustodian.RTGSCode);
                            cmd.Parameters.AddWithValue("@BitRDN", _bankCustodian.BitRDN);
                            cmd.Parameters.AddWithValue("@BitSyariah", _bankCustodian.BitSyariah);
                            cmd.Parameters.AddWithValue("@City", _bankCustodian.City);
                            cmd.Parameters.AddWithValue("@Phone1", _bankCustodian.Phone1);
                            cmd.Parameters.AddWithValue("@Phone2", _bankCustodian.Phone2);
                            cmd.Parameters.AddWithValue("@Phone3", _bankCustodian.Phone3);
                            cmd.Parameters.AddWithValue("@FeeLLG", _bankCustodian.FeeLLG);
                            cmd.Parameters.AddWithValue("@FeeRTGS", _bankCustodian.FeeRTGS);
                            cmd.Parameters.AddWithValue("@MinforRTGS", _bankCustodian.MinforRTGS);
                            cmd.Parameters.AddWithValue("@JournalRoundingMode", _bankCustodian.JournalRoundingMode);
                            cmd.Parameters.AddWithValue("@JournalDecimalPlaces", _bankCustodian.JournalDecimalPlaces);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _bankCustodian.EntryUsersID);
                            cmd.Parameters.AddWithValue("@UpdateTime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);

                            cmd.ExecuteNonQuery();
                        }

                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update BankCustodian set status= 4,Notes=@Notes, " +
                            " LastUpdate=@lastupdate where BankCustodianPK = @PK and historyPK = @HistoryPK";
                            cmd.Parameters.AddWithValue("@Notes", _bankCustodian.Notes);
                            cmd.Parameters.AddWithValue("@PK", _bankCustodian.BankCustodianPK);
                            cmd.Parameters.AddWithValue("@HistoryPK", _bankCustodian.HistoryPK);
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

        public void BankCustodian_Approved(BankCustodian _bankCustodian)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update BankCustodian set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,lastupdate=@lastupdate " +
                            "where BankCustodianPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _bankCustodian.BankCustodianPK);
                        cmd.Parameters.AddWithValue("@historyPK", _bankCustodian.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _bankCustodian.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update BankCustodian set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,lastupdate=@lastupdate where BankCustodianPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _bankCustodian.BankCustodianPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _bankCustodian.ApprovedUsersID);
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

        public void BankCustodian_Reject(BankCustodian _bankCustodian)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update BankCustodian set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,lastupdate=@lastupdate " +
                            "where BankCustodianPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _bankCustodian.BankCustodianPK);
                        cmd.Parameters.AddWithValue("@historyPK", _bankCustodian.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _bankCustodian.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update BankCustodian set status= 2,lastupdate=@lastupdate where BankCustodianPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _bankCustodian.BankCustodianPK);
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

        public void BankCustodian_Void(BankCustodian _bankCustodian)
        {
          try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update BankCustodian set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime, lastupdate=@lastupdate  " +
                            "where BankCustodianPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _bankCustodian.BankCustodianPK);
                        cmd.Parameters.AddWithValue("@historyPK", _bankCustodian.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _bankCustodian.VoidUsersID);
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

        public List<BankCustodianCombo> BankCustodian_Combo(string _type)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<BankCustodianCombo> L_BankCustodian = new List<BankCustodianCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                      
                            cmd.CommandText =
                            " SELECT  BankCustodianPK,BC.ID + ' - ' + Name as ID, Name FROM [BankCustodian] BC " +
                            " left join mastervalue MV on BC.Type = MV.Code and MV.ID = 'BankCustodianType' " + 
                            " where  BC.status = 2 and MV.Type =@Type order by ID,Name ";

                            cmd.Parameters.AddWithValue("@Type", _type);
                            using (SqlDataReader dr = cmd.ExecuteReader())
                            {
                                if (dr.HasRows)
                                {
                                    while (dr.Read())
                                    {
                                        BankCustodianCombo M_BankCustodian = new BankCustodianCombo();
                                        M_BankCustodian.BankCustodianPK = Convert.ToInt32(dr["BankCustodianPK"]);
                                        M_BankCustodian.ID = Convert.ToString(dr["ID"]);
                                        M_BankCustodian.Name = Convert.ToString(dr["Name"]);
                                        L_BankCustodian.Add(M_BankCustodian);
                                    }

                                }
                                return L_BankCustodian;
                            }
                        }
     
                    }
                
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public List<BankCustodianCombo> BankCustodian_ComboIDOnly(string _type)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<BankCustodianCombo> L_BankCustodian = new List<BankCustodianCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText =
                        " select BankCustodianPK, bc.ID, bc.ID + ' - ' + bc.Name as Name from [BankCustodian] bc " +
                        " left join mastervalue mv on bc.Type = mv.Code and mv.ID = 'BankCustodianType' " +
                        " where bc.status = 2 and mv.Type = @Type order by bc.ID, bc.Name ";

                        cmd.Parameters.AddWithValue("@Type", _type);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    BankCustodianCombo M_BankCustodian = new BankCustodianCombo();
                                    M_BankCustodian.BankCustodianPK = Convert.ToInt32(dr["BankCustodianPK"]);
                                    M_BankCustodian.ID = Convert.ToString(dr["ID"]);
                                    M_BankCustodian.Name = Convert.ToString(dr["Name"]);
                                    L_BankCustodian.Add(M_BankCustodian);
                                }
                            }
                            return L_BankCustodian;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }


        public string Get_BranchNameByBankCustodianPK(int _bankCustodianPK)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<BankCustodianCombo> L_Bank = new List<BankCustodianCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {


                        cmd.CommandText = "SELECT Branch FROM [BankCustodian]  where status =2 and BankCustodianPK = @BankCustodianPK ";
                        cmd.Parameters.AddWithValue("@BankCustodianPK", _bankCustodianPK);


                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    return Convert.ToString(dr["Branch"]);
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

    }
}