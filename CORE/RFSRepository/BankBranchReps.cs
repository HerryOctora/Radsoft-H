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
    public class BankBranchReps
    {
        Host _host = new Host();

        //1
        string _insertCommand = "INSERT INTO [dbo].[BankBranch] " +
                            "([BankBranchPK],[HistoryPK],[Status],[ID],[Type],[Address],[ContactPerson],[Fax1],[Fax2],[Fax3], " +
                            " [Email1],[Email2],[Email3],[Attn1],[Attn2],[Attn3],[City],[Phone1],[Phone2],[Phone3],[BankPK],[PTPCode],[BankAccountName],[BankAccountNo],[InterestDaysType],[BitIsEnabled]," +
                            " ";

        string _paramaterCommand = "@ID,@Type,@Address,@ContactPerson,@Fax1,@Fax2,@Fax3, " +
                            " @Email1,@Email2,@Email3,@Attn1,@Attn2,@Attn3,@City,@Phone1,@Phone2,@Phone3,@BankPK,@PTPCode,@BankAccountName,@BankAccountNo,@InterestDaysType,@BitIsEnabled," +
                            " ";

        //2
        private BankBranch setBankBranch(SqlDataReader dr)
        {
            BankBranch M_BankBranch = new BankBranch();
            M_BankBranch.BankBranchPK = Convert.ToInt32(dr["BankBranchPK"]);
            M_BankBranch.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_BankBranch.Status = Convert.ToInt32(dr["Status"]);
            M_BankBranch.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_BankBranch.Notes = Convert.ToString(dr["Notes"]);
            M_BankBranch.ID = dr["ID"].ToString();
            M_BankBranch.BankPK = dr["BankPK"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["BankPK"]);
            M_BankBranch.BankID = dr["BankID"].ToString();
            M_BankBranch.Type = Convert.ToInt32(dr["Type"]);
            M_BankBranch.TypeDesc = Convert.ToString(dr["TypeDesc"]);
            M_BankBranch.Address = dr["Address"].ToString();
            M_BankBranch.ContactPerson = dr["ContactPerson"].ToString();
            M_BankBranch.Fax1 = dr["Fax1"].ToString();
            M_BankBranch.Fax2 = dr["Fax2"].ToString();
            M_BankBranch.Fax3 = dr["Fax3"].ToString();
            M_BankBranch.Email1 = dr["Email1"].ToString();
            M_BankBranch.Email2 = dr["Email2"].ToString();
            M_BankBranch.Email3 = dr["Email3"].ToString();
            M_BankBranch.Attn1 = dr["Attn1"].ToString();
            M_BankBranch.Attn2 = dr["Attn2"].ToString();
            M_BankBranch.Attn3 = dr["Attn3"].ToString();
            M_BankBranch.City = dr["City"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["City"]);
            M_BankBranch.InterestDaysType = dr["InterestDaysType"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["InterestDaysType"]);
            M_BankBranch.CityDesc = dr["CityDesc"].ToString();
            M_BankBranch.Phone1 = dr["Phone1"].ToString();
            M_BankBranch.Phone2 = dr["Phone2"].ToString();
            M_BankBranch.Phone3 = dr["Phone3"].ToString();
            M_BankBranch.PTPCode = dr["PTPCode"].ToString();
            M_BankBranch.BankAccountName = dr["BankAccountName"].ToString();
            M_BankBranch.BankAccountNo = dr["BankAccountNo"].ToString();
            if (_host.CheckColumnIsExist(dr, "BankCode"))
            {
                M_BankBranch.BankCode = dr["BankCode"].ToString();
            }
            if (_host.CheckColumnIsExist(dr, "BankName"))
            {
                M_BankBranch.BankName = dr["BankName"].ToString();
            }
            if (_host.CheckColumnIsExist(dr, "BranchName"))
            {
                M_BankBranch.BranchName = dr["BranchName"].ToString();
            }
            M_BankBranch.BitIsEnabled = Convert.ToBoolean(dr["BitIsEnabled"]);
            M_BankBranch.EntryUsersID = dr["EntryUsersID"].ToString();
            M_BankBranch.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_BankBranch.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_BankBranch.VoidUsersID = dr["VoidUsersID"].ToString();
            M_BankBranch.EntryTime = dr["EntryTime"].ToString();
            M_BankBranch.UpdateTime = dr["UpdateTime"].ToString();
            M_BankBranch.ApprovedTime = dr["ApprovedTime"].ToString();
            M_BankBranch.VoidTime = dr["VoidTime"].ToString();
            M_BankBranch.DBUserID = dr["DBUserID"].ToString();
            M_BankBranch.DBTerminalID = dr["DBTerminalID"].ToString();
            M_BankBranch.LastUpdate = dr["LastUpdate"].ToString();
            M_BankBranch.LastUpdateDB = dr["LastUpdateDB"].ToString();
            return M_BankBranch;
        }

        public List<BankBranch> BankBranch_Select(int _status)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<BankBranch> L_bankBranch = new List<BankBranch>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        if (Tools.ClientCode == "05")
                        {
                            if (_status != 9)
                            {
                                cmd.CommandText = @"
                                Select case when B.status=1 then 'PENDING' else Case When B.status = 2 then 'APPROVED' else Case when B.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,
                                C.BankPK,C.BICode BankCode,C.Name BankName, B.PTPCode,B.ID BranchName,isnull(B.BankAccountNo,'') BankAccountNo,isnull(B.BankAccountName,'') BankAccountName,B.ContactPerson,B.Phone1,B.Fax1,B.Email1,isnull(BitIsEnabled,0) BitIsEnableds,C.ID BankID,MV.DescOne TypeDesc,MV1.DescOne CityDesc, B.* from BankBranch B    
                                left join MasterValue MV on B.Type = MV.Code and MV.ID ='BankCustodianType' and MV.status = 2    
                                left join MasterValue MV1 on B.City = MV1.Code and MV1.ID = 'CityRHB' and MV1.status = 2    
                                left join Bank C on B.BankPK = C.BankPK and C.status = 2
                                where B.status = @status order by C.Name";
                                cmd.Parameters.AddWithValue("@status", _status);
                            }
                            else
                            {
                                cmd.CommandText = @"
                                Select case when B.status=1 then 'PENDING' else Case When B.status = 2 then 'APPROVED' else Case when B.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,
                                C.BankPK,C.BICode BankCode,C.Name BankName, B.PTPCode,B.ID BranchName,isnull(B.BankAccountNo,'') BankAccountNo,isnull(B.BankAccountName,'') BankAccountName,B.ContactPerson,B.Phone1,B.Fax1,B.Email1,isnull(BitIsEnabled,0) BitIsEnableds,C.ID BankID,MV.DescOne TypeDesc,MV1.DescOne CityDesc, B.* from BankBranch B    
                                left join MasterValue MV on B.Type = MV.Code and MV.ID ='BankCustodianType' and MV.status = 2    
                                left join MasterValue MV1 on B.City = MV1.Code and MV1.ID = 'CityRHB' and MV1.status = 2    
                                left join Bank C on B.BankPK = C.BankPK and C.status = 2
                                order by C.Name";



                            }
                        }
                        else
                        {
                            if (_status != 9)
                            {
                                cmd.CommandText = @"
                                 Select case when B.status=1 then 'PENDING' else Case When B.status = 2 then 'APPROVED' else Case when B.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,
                                  C.BankPK,C.ID BankID,MV.DescOne TypeDesc,MV1.DescOne CityDesc,B.* from BankBranch B    
                                  left join MasterValue MV on B.Type = MV.Code and MV.ID ='BankCustodianType' and MV.status = 2    
                                  left join MasterValue MV1 on B.City = MV1.Code and MV1.ID = 'CityRHB' and MV1.status = 2    
                                  left join Bank C on B.BankPK = C.BankPK and C.status = 2
                                  where B.status = @status
                            ";
                                cmd.Parameters.AddWithValue("@status", _status);
                            }
                            else
                            {
                                cmd.CommandText = @"
                                  Select case when B.status=1 then 'PENDING' else Case When B.status = 2 then 'APPROVED' else Case when B.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,
                                  C.BankPK,C.ID BankID,MV.DescOne TypeDesc,MV1.DescOne CityDesc,B.* from BankBranch B    
                                  left join MasterValue MV on B.Type = MV.Code and MV.ID ='BankCustodianType' and MV.status = 2    
                                  left join MasterValue MV1 on B.City = MV1.Code and MV1.ID = 'CityRHB' and MV1.status = 2    
                                  left join Bank C on B.BankPK = C.BankPK and C.status = 2
                        ";
                            }





                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_bankBranch.Add(setBankBranch(dr));
                                }
                            }
                            return L_bankBranch;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int BankBranch_Add(BankBranch _bankBranch, bool _havePrivillege)
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
                                 "Select isnull(max(BankBranchPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@lastupdate from BankBranch";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _bankBranch.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],lastupdate)" +
                                "Select isnull(max(BankBranchPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@lastupdate from BankBranch";
                        }
                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@BankPK", _bankBranch.BankPK);
                        cmd.Parameters.AddWithValue("@ID", _bankBranch.ID);
                        cmd.Parameters.AddWithValue("@Type", _bankBranch.Type);
                        cmd.Parameters.AddWithValue("@Address", _bankBranch.Address);
                        cmd.Parameters.AddWithValue("@ContactPerson", _bankBranch.ContactPerson);
                        cmd.Parameters.AddWithValue("@Fax1", _bankBranch.Fax1);
                        cmd.Parameters.AddWithValue("@Fax2", _bankBranch.Fax2);
                        cmd.Parameters.AddWithValue("@Fax3", _bankBranch.Fax3);
                        cmd.Parameters.AddWithValue("@Email1", _bankBranch.Email1);
                        cmd.Parameters.AddWithValue("@Email2", _bankBranch.Email2);
                        cmd.Parameters.AddWithValue("@Email3", _bankBranch.Email3);
                        cmd.Parameters.AddWithValue("@Attn1", _bankBranch.Attn1);
                        cmd.Parameters.AddWithValue("@Attn2", _bankBranch.Attn2);
                        cmd.Parameters.AddWithValue("@Attn3", _bankBranch.Attn3);
                        cmd.Parameters.AddWithValue("@City", _bankBranch.City);
                        cmd.Parameters.AddWithValue("@Phone1", _bankBranch.Phone1);
                        cmd.Parameters.AddWithValue("@Phone2", _bankBranch.Phone2);
                        cmd.Parameters.AddWithValue("@Phone3", _bankBranch.Phone3);
                        cmd.Parameters.AddWithValue("@BankAccountName ", _bankBranch.BankAccountName);
                        cmd.Parameters.AddWithValue("@BankAccountNo ", _bankBranch.BankAccountNo);
                        cmd.Parameters.AddWithValue("@InterestDaysType ", _bankBranch.InterestDaysType);
                        cmd.Parameters.AddWithValue("@PTPCode", _bankBranch.PTPCode);
                        cmd.Parameters.AddWithValue("@BitIsEnabled", _bankBranch.BitIsEnabled);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _bankBranch.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);

                        cmd.ExecuteNonQuery();


                        return _host.Get_LastPKByLastUpate(_datetimeNow, "BankBranch");
                    }


                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int BankBranch_Update(BankBranch _bankBranch, bool _havePrivillege)
        {
            try
            {
                int _newHisPK;
                DateTime _datetimeNow = DateTime.Now;
                int status = _host.Get_Status(_bankBranch.BankBranchPK, _bankBranch.HistoryPK, "BankBranch");
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update BankBranch set status=2,Notes=@Notes,ID=@ID,Type=@Type," +
                                "Address=@Address,ContactPerson=@ContactPerson," +
                                "Fax1=@Fax1,Fax2=@Fax2,Fax3=@Fax3,Email1=@Email1,Email2=@Email2,Email3=@Email3,Attn1=@Attn1,Attn2=@Attn2,Attn3=@Attn3," +
                                "City=@City,Phone1=@Phone1,Phone2=@Phone2,Phone3=@Phone3,BankPK = @BankPK,PTPCode=@PTPCode,BankAccountName=@BankAccountName,BankAccountNo=@BankAccountNo,InterestDaysType=@InterestDaysType,BitIsEnabled=@BitIsEnabled," +
                                "ApprovedUsersID=@ApprovedUsersID,ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,lastupdate=@lastupdate " +
                                "where BankBranchPK = @PK and historyPK = @HistoryPK";

                            cmd.Parameters.AddWithValue("@HistoryPK", _bankBranch.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _bankBranch.BankBranchPK);
                            cmd.Parameters.AddWithValue("@Notes", _bankBranch.Notes);
                            cmd.Parameters.AddWithValue("@BankPK", _bankBranch.BankPK);
                            cmd.Parameters.AddWithValue("@ID", _bankBranch.ID);
                            cmd.Parameters.AddWithValue("@Type", _bankBranch.Type);
                            cmd.Parameters.AddWithValue("@Address", _bankBranch.Address);
                            cmd.Parameters.AddWithValue("@ContactPerson", _bankBranch.ContactPerson);
                            cmd.Parameters.AddWithValue("@Fax1", _bankBranch.Fax1);
                            cmd.Parameters.AddWithValue("@Fax2", _bankBranch.Fax2);
                            cmd.Parameters.AddWithValue("@Fax3", _bankBranch.Fax3);
                            cmd.Parameters.AddWithValue("@Email1", _bankBranch.Email1);
                            cmd.Parameters.AddWithValue("@Email2", _bankBranch.Email2);
                            cmd.Parameters.AddWithValue("@Email3", _bankBranch.Email3);
                            cmd.Parameters.AddWithValue("@Attn1", _bankBranch.Attn1);
                            cmd.Parameters.AddWithValue("@Attn2", _bankBranch.Attn2);
                            cmd.Parameters.AddWithValue("@Attn3", _bankBranch.Attn3);
                            cmd.Parameters.AddWithValue("@City", _bankBranch.City);
                            cmd.Parameters.AddWithValue("@Phone1", _bankBranch.Phone1);
                            cmd.Parameters.AddWithValue("@Phone2", _bankBranch.Phone2);
                            cmd.Parameters.AddWithValue("@Phone3", _bankBranch.Phone3);
                            cmd.Parameters.AddWithValue("@PTPCode", _bankBranch.PTPCode);
                            cmd.Parameters.AddWithValue("@BankAccountName", _bankBranch.BankAccountName);
                            cmd.Parameters.AddWithValue("@BankAccountNo", _bankBranch.BankAccountNo);
                            cmd.Parameters.AddWithValue("@BitIsEnabled", _bankBranch.BitIsEnabled);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _bankBranch.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _bankBranch.EntryUsersID);
                            cmd.Parameters.AddWithValue("@InterestDaysType", _bankBranch.InterestDaysType);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);

                            cmd.ExecuteNonQuery();
                        }

                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update BankBranch set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,lastupdate=@lastupdate where BankBranchPK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _bankBranch.BankBranchPK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _bankBranch.EntryUsersID);
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
                                cmd.CommandText = "Update BankBranch set Notes=@Notes,ID=@ID,Type=@Type," +
                                    "Address=@Address,ContactPerson=@ContactPerson," +
                                    "Fax1=@Fax1,Fax2=@Fax2,Fax3=@Fax3,Email1=@Email1,Email2=@Email2,Email3=@Email3,Attn1=@Attn1,Attn2=@Attn2,Attn3=@Attn3," +
                                    "City=@City,Phone1=@Phone1,Phone2=@Phone2,Phone3=@Phone3,BankPK = @BankPK,PTPCode=@PTPCode,BankAccountName=@BankAccountName,BankAccountNo=@BankAccountNo,InterestDaysType=@InterestDaysType ,BitIsEnabled=@BitIsEnabled," +
                                    "UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,lastupdate=@lastupdate " +
                                    "where BankBranchPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _bankBranch.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _bankBranch.BankBranchPK);
                                cmd.Parameters.AddWithValue("@Notes", _bankBranch.Notes);
                                cmd.Parameters.AddWithValue("@BankPK", _bankBranch.BankPK);
                                cmd.Parameters.AddWithValue("@ID", _bankBranch.ID);
                                cmd.Parameters.AddWithValue("@Type", _bankBranch.Type);
                                cmd.Parameters.AddWithValue("@Address", _bankBranch.Address);
                                cmd.Parameters.AddWithValue("@ContactPerson", _bankBranch.ContactPerson);
                                cmd.Parameters.AddWithValue("@Fax1", _bankBranch.Fax1);
                                cmd.Parameters.AddWithValue("@Fax2", _bankBranch.Fax2);
                                cmd.Parameters.AddWithValue("@Fax3", _bankBranch.Fax3);
                                cmd.Parameters.AddWithValue("@Email1", _bankBranch.Email1);
                                cmd.Parameters.AddWithValue("@Email2", _bankBranch.Email2);
                                cmd.Parameters.AddWithValue("@Email3", _bankBranch.Email3);
                                cmd.Parameters.AddWithValue("@Attn1", _bankBranch.Attn1);
                                cmd.Parameters.AddWithValue("@Attn2", _bankBranch.Attn2);
                                cmd.Parameters.AddWithValue("@Attn3", _bankBranch.Attn3);
                                cmd.Parameters.AddWithValue("@City", _bankBranch.City);
                                cmd.Parameters.AddWithValue("@Phone1", _bankBranch.Phone1);
                                cmd.Parameters.AddWithValue("@Phone2", _bankBranch.Phone2);
                                cmd.Parameters.AddWithValue("@Phone3", _bankBranch.Phone3);
                                cmd.Parameters.AddWithValue("@PTPCode", _bankBranch.PTPCode);
                                cmd.Parameters.AddWithValue("@BankAccountName", _bankBranch.BankAccountName);
                                cmd.Parameters.AddWithValue("@BankAccountNo", _bankBranch.BankAccountNo);
                                cmd.Parameters.AddWithValue("@BitIsEnabled", _bankBranch.BitIsEnabled);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _bankBranch.EntryUsersID);
                                cmd.Parameters.AddWithValue("@InterestDaysType", _bankBranch.InterestDaysType);
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
                                _newHisPK = _host.Get_NewHistoryPK(_bankBranch.BankBranchPK, "BankBranch");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From BankBranch where BankBranchPK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _bankBranch.BankBranchPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _bankBranch.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@BankPK", _bankBranch.BankPK);
                                cmd.Parameters.AddWithValue("@ID", _bankBranch.ID);
                                cmd.Parameters.AddWithValue("@Type", _bankBranch.Type);
                                cmd.Parameters.AddWithValue("@Address", _bankBranch.Address);
                                cmd.Parameters.AddWithValue("@ContactPerson", _bankBranch.ContactPerson);
                                cmd.Parameters.AddWithValue("@Fax1", _bankBranch.Fax1);
                                cmd.Parameters.AddWithValue("@Fax2", _bankBranch.Fax2);
                                cmd.Parameters.AddWithValue("@Fax3", _bankBranch.Fax3);
                                cmd.Parameters.AddWithValue("@Email1", _bankBranch.Email1);
                                cmd.Parameters.AddWithValue("@Email2", _bankBranch.Email2);
                                cmd.Parameters.AddWithValue("@Email3", _bankBranch.Email3);
                                cmd.Parameters.AddWithValue("@Attn1", _bankBranch.Attn1);
                                cmd.Parameters.AddWithValue("@Attn2", _bankBranch.Attn2);
                                cmd.Parameters.AddWithValue("@Attn3", _bankBranch.Attn3);
                                cmd.Parameters.AddWithValue("@City", _bankBranch.City);
                                cmd.Parameters.AddWithValue("@Phone1", _bankBranch.Phone1);
                                cmd.Parameters.AddWithValue("@Phone2", _bankBranch.Phone2);
                                cmd.Parameters.AddWithValue("@Phone3", _bankBranch.Phone3);
                                cmd.Parameters.AddWithValue("@PTPCode", _bankBranch.PTPCode);
                                cmd.Parameters.AddWithValue("@BankAccountName", _bankBranch.BankAccountName);
                                cmd.Parameters.AddWithValue("@BankAccountNo", _bankBranch.BankAccountNo);
                                cmd.Parameters.AddWithValue("@BitIsEnabled", _bankBranch.BitIsEnabled);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _bankBranch.EntryUsersID);
                                cmd.Parameters.AddWithValue("@InterestDaysType", _bankBranch.InterestDaysType);
                                cmd.Parameters.AddWithValue("@UpdateTime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);

                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update BankBranch set status= 4,Notes=@Notes, " +
                                " LastUpdate=@lastupdate where BankBranchPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _bankBranch.Notes);
                                cmd.Parameters.AddWithValue("@PK", _bankBranch.BankBranchPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _bankBranch.HistoryPK);
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


        public void BankBranch_Approved(BankBranch _bankBranch)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update BankBranch set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,lastupdate=@lastupdate " +
                            "where BankBranchPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _bankBranch.BankBranchPK);
                        cmd.Parameters.AddWithValue("@historyPK", _bankBranch.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _bankBranch.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update BankBranch set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,lastupdate=@lastupdate where BankBranchPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _bankBranch.BankBranchPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _bankBranch.ApprovedUsersID);
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

        public void BankBranch_Reject(BankBranch _bankBranch)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update BankBranch set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,lastupdate=@lastupdate " +
                            "where BankBranchPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _bankBranch.BankBranchPK);
                        cmd.Parameters.AddWithValue("@historyPK", _bankBranch.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _bankBranch.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update BankBranch set status= 2,lastupdate=@lastupdate where BankBranchPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _bankBranch.BankBranchPK);
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

        public void BankBranch_Void(BankBranch _bankBranch)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update BankBranch set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime, lastupdate=@lastupdate  " +
                            "where BankBranchPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _bankBranch.BankBranchPK);
                        cmd.Parameters.AddWithValue("@historyPK", _bankBranch.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _bankBranch.VoidUsersID);
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

        public List<BankBranchCombo> BankBranch_Combo()
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<BankBranchCombo> L_BankBranch = new List<BankBranchCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText =
                         @" SELECT  BankBranchPK,B.ID  + ' - ' + A.ID  ID FROM [BankBranch] A    
                        left join Bank B on A.BankPK = B.BankPK and B.status = 2
                        where  A.status = 2 ";
                        
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    BankBranchCombo M_BankBranch = new BankBranchCombo();
                                    M_BankBranch.BankBranchPK = Convert.ToInt32(dr["BankBranchPK"]);
                                    M_BankBranch.ID = Convert.ToString(dr["ID"]);
                                    L_BankBranch.Add(M_BankBranch);
                                }

                            }
                            return L_BankBranch;
                        }
                    }

                }

            }
            catch (Exception err)
            {
                throw err;
            }

        }


        public List<BankBranchCombo> BankBranch_ComboCustodiOnly()
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<BankBranchCombo> L_BankBranch = new List<BankBranchCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText =
                         @" SELECT  BankBranchPK,B.ID  + ' - ' + A.ID  ID FROM [BankBranch] A    
                        left join Bank B on A.BankPK = B.BankPK and B.status = 2
                        where  A.status = 2 and A.type = 2 ";

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    BankBranchCombo M_BankBranch = new BankBranchCombo();
                                    M_BankBranch.BankBranchPK = Convert.ToInt32(dr["BankBranchPK"]);
                                    M_BankBranch.ID = Convert.ToString(dr["ID"]);
                                    L_BankBranch.Add(M_BankBranch);
                                }

                            }
                            return L_BankBranch;
                        }
                    }

                }

            }
            catch (Exception err)
            {
                throw err;
            }

        }


        public List<BankBranchCombo> BankBranch_ComboBankOnly()
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<BankBranchCombo> L_BankBranch = new List<BankBranchCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText =
                         @" SELECT  BankBranchPK,B.ID  + ' - ' + A.ID  ID FROM [BankBranch] A    
                        left join Bank B on A.BankPK = B.BankPK and B.status = 2
                        where  A.status = 2 and A.type = 1 ";

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    BankBranchCombo M_BankBranch = new BankBranchCombo();
                                    M_BankBranch.BankBranchPK = Convert.ToInt32(dr["BankBranchPK"]);
                                    M_BankBranch.ID = Convert.ToString(dr["ID"]);
                                    L_BankBranch.Add(M_BankBranch);
                                }

                            }
                            return L_BankBranch;
                        }
                    }

                }

            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public List<BankBranchCombo> BankBranch_ComboByBankPK(int _bankPK)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<BankBranchCombo> L_BankBranch = new List<BankBranchCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText =
                         @" SELECT  BankBranchPK,B.ID  + ' - ' + A.ID  ID FROM [BankBranch] A    
                        left join Bank B on A.BankPK = B.BankPK and B.status = 2
                        where  A.status = 2 and B.BankPK = @BankPK ";

                        cmd.Parameters.AddWithValue("@BankPK", _bankPK);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    BankBranchCombo M_BankBranch = new BankBranchCombo();
                                    M_BankBranch.BankBranchPK = Convert.ToInt32(dr["BankBranchPK"]);
                                    M_BankBranch.ID = Convert.ToString(dr["ID"]);
                                    L_BankBranch.Add(M_BankBranch);
                                }

                            }
                            return L_BankBranch;
                        }
                    }

                }

            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public List<BankBranchCombo> BankBranch_ComboBankOnlyByBankPK(int _bankPK)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<BankBranchCombo> L_BankBranch = new List<BankBranchCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText =
                         @" SELECT  BankBranchPK, A.PTPCode +' - '+ B.ID  + ' - ' + A.ID  ID FROM [BankBranch] A    
                        left join Bank B on A.BankPK = B.BankPK and B.status = 2
                        where  A.status = 2 and A.Type = 1 and B.BankPK = @BankPK ";

                        cmd.Parameters.AddWithValue("@BankPK", _bankPK);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    BankBranchCombo M_BankBranch = new BankBranchCombo();
                                    M_BankBranch.BankBranchPK = Convert.ToInt32(dr["BankBranchPK"]);
                                    M_BankBranch.ID = Convert.ToString(dr["ID"]);
                                    L_BankBranch.Add(M_BankBranch);
                                }

                            }
                            return L_BankBranch;
                        }
                    }

                }

            }
            catch (Exception err)
            {
                throw err;
            }

        }


        public List<BankBranchCombo> BankBranch_ComboCustodiOnlyRpt()
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<BankBranchCombo> L_BankBranch = new List<BankBranchCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText =
                         @" SELECT  BankBranchPK,B.ID  + ' - ' + A.ID  ID  FROM [BankBranch] A    
                        left join Bank B on A.BankPK = B.BankPK and B.status = 2
                        where  A.status = 2 and A.type = 2 
                        union all select 0,'All'
                        order by BankBranchPK,ID
                        ";

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    BankBranchCombo M_BankBranch = new BankBranchCombo();
                                    M_BankBranch.BankBranchPK = Convert.ToInt32(dr["BankBranchPK"]);
                                    M_BankBranch.ID = Convert.ToString(dr["ID"]);
                                    L_BankBranch.Add(M_BankBranch);
                                }

                            }
                            return L_BankBranch;
                        }
                    }

                }

            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public BankInterestInformation BankBranch_GetInterestDaysType(int _bankBranchPK)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {


                        cmd.CommandText = @"SELECT InterestDaysType  FROM [BankBranch] A
                        where A.status = 2 and BankbranchPK = @BankBranchPK ";
                        cmd.Parameters.AddWithValue("@BankBranchPK", _bankBranchPK);


                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            { // 10 Field
                                dr.Read();
                                BankInterestInformation M_Bank = new BankInterestInformation();
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
    }
}