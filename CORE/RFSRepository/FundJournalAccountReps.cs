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
    public class FundJournalAccountReps
    {
        Host _host = new Host();

        //1
        string _insertCommand = "INSERT INTO [dbo].[FundJournalAccount] " +
                            "([FundJournalAccountPK],[HistoryPK],[Status],[ID],[Name],[Type],[Groups],[Levels],[ParentPK],[CurrencyPK]," +
                            "[Show],[ParentPK1],[ParentPK2],[ParentPK3],[ParentPK4],[ParentPK5],[ParentPK6],[ParentPK7],[ParentPK8],[ParentPK9],[BitIsChange],[Fxd11AccountPK],";
        string _paramaterCommand = "@ID,@Name,@Type,@Groups,@Levels,@ParentPK,@CurrencyPK," +
                            "@Show,@ParentPK1,@ParentPK2,@ParentPK3,@ParentPK4,@ParentPK5,@ParentPK6,@ParentPK7,@ParentPK8,@ParentPK9,@BitIsChange,@Fxd11AccountPK,";

        //2
        private FundJournalAccount setFundJournalAccount(SqlDataReader dr)
        {
            FundJournalAccount M_fundJournalAccount = new FundJournalAccount();
            M_fundJournalAccount.FundJournalAccountPK = Convert.ToInt32(dr["FundJournalAccountPK"]);
            M_fundJournalAccount.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_fundJournalAccount.Status = Convert.ToInt32(dr["Status"]);
            M_fundJournalAccount.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_fundJournalAccount.Notes = Convert.ToString(dr["Notes"]);
            M_fundJournalAccount.ID = dr["ID"].ToString();
            M_fundJournalAccount.Name = dr["Name"].ToString();
            M_fundJournalAccount.Type = Convert.ToString(dr["Type"]);
            M_fundJournalAccount.TypeDesc = Convert.ToString(dr["TypeDesc"]);
            M_fundJournalAccount.Groups = Convert.ToBoolean(dr["Groups"]);
            M_fundJournalAccount.Levels = Convert.ToInt32(dr["Levels"]);
            M_fundJournalAccount.ParentPK = Convert.ToInt32(dr["ParentPK"]);
            M_fundJournalAccount.ParentID = Convert.ToString(dr["ParentID"]);
            M_fundJournalAccount.ParentName = Convert.ToString(dr["ParentName"]);
            M_fundJournalAccount.Depth = Convert.ToInt32(dr["Depth"]);
            M_fundJournalAccount.CurrencyPK = Convert.ToInt32(dr["CurrencyPK"]);
            M_fundJournalAccount.CurrencyID = Convert.ToString(dr["CurrencyID"]);
            M_fundJournalAccount.Show = Convert.ToBoolean(dr["Show"]);
            M_fundJournalAccount.ParentPK1 = Convert.ToInt32(dr["ParentPK1"]);
            M_fundJournalAccount.ParentPK2 = Convert.ToInt32(dr["ParentPK2"]);
            M_fundJournalAccount.ParentPK3 = Convert.ToInt32(dr["ParentPK3"]);
            M_fundJournalAccount.ParentPK4 = Convert.ToInt32(dr["ParentPK4"]);
            M_fundJournalAccount.ParentPK5 = Convert.ToInt32(dr["ParentPK5"]);
            M_fundJournalAccount.ParentPK6 = Convert.ToInt32(dr["ParentPK6"]);
            M_fundJournalAccount.ParentPK7 = Convert.ToInt32(dr["ParentPK7"]);
            M_fundJournalAccount.ParentPK8 = Convert.ToInt32(dr["ParentPK8"]);
            M_fundJournalAccount.ParentPK9 = Convert.ToInt32(dr["ParentPK9"]);
            M_fundJournalAccount.BitIsChange = Convert.ToBoolean(dr["BitIsChange"]);//
            M_fundJournalAccount.Fxd11AccountPK = Convert.ToInt32(dr["Fxd11AccountPK"]);
            M_fundJournalAccount.EntryUsersID = Convert.ToString(dr["EntryUsersID"]);
            M_fundJournalAccount.UpdateUsersID = Convert.ToString(dr["UpdateUsersID"]);
            M_fundJournalAccount.ApprovedUsersID = Convert.ToString(dr["ApprovedUsersID"]);
            M_fundJournalAccount.VoidUsersID = Convert.ToString(dr["VoidUsersID"]);
            M_fundJournalAccount.EntryTime = Convert.ToString(dr["EntryTime"]);
            M_fundJournalAccount.UpdateTime = Convert.ToString(dr["UpdateTime"]);
            M_fundJournalAccount.ApprovedTime = Convert.ToString(dr["ApprovedTime"]);
            M_fundJournalAccount.VoidTime = Convert.ToString(dr["VoidTime"]);
            M_fundJournalAccount.DBUserID = Convert.ToString(dr["DBUserID"]);
            M_fundJournalAccount.DBTerminalID = Convert.ToString(dr["DBTerminalID"]);
            M_fundJournalAccount.LastUpdate = Convert.ToString(dr["LastUpdate"]);
            M_fundJournalAccount.LastUpdateDB = Convert.ToString(dr["LastUpdateDB"]);
            return M_fundJournalAccount;
        }



        public List<FundJournalAccountCombo> FundJournalAccount_ComboChildOnlyAll()
        {
            try
            {

                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<FundJournalAccountCombo> L_fundJournalAccount = new List<FundJournalAccountCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "SELECT  FundJournalAccountPK,ID + ' - ' + Name as ID, Name FROM [FundJournalAccount]  where Groups = 0 and status = 2 union all select 0,'All', '' order by FundJournalAccountPK,Name";
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    FundJournalAccountCombo M_fundJournalAccount = new FundJournalAccountCombo();
                                    M_fundJournalAccount.FundJournalAccountPK = Convert.ToInt32(dr["FundJournalAccountPK"]);
                                    M_fundJournalAccount.ID = Convert.ToString(dr["ID"]);
                                    M_fundJournalAccount.Name = Convert.ToString(dr["Name"]);
                                    L_fundJournalAccount.Add(M_fundJournalAccount);
                                }
                            }
                            return L_fundJournalAccount;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public List<FundJournalAccount> FundJournalAccount_Select(int _status)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<FundJournalAccount> L_fundJournalAccount = new List<FundJournalAccount>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = " Select case when A.status=1 then 'PENDING' else Case When A.status = 2 then 'APPROVED' else Case when A.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,MV.DescOne TypeDesc,C.ID CurrencyID,ZA.ID ParentID,ZA.Name ParentName , B.ID Fxd11AccountID,B.Name Fxd11AccountName , A.* from FundJournalAccount A left join   " +
                            " Currency C on A.CurrencyPK = C.CurrencyPK and C.status = 2  left join   " +
                            " FundJournalAccount ZA on A.ParentPK = ZA.FundJournalAccountPK  and ZA.status in (1,2) left join " +
                            " MasterValue MV on A.Type=MV.Code and MV.ID ='FundJournalAccountType' " +
                            " Left join Fxd11Account B on A.Fxd11AccountPK = B.Fxd11AccountPK and B.status = 2 " +
                            " where A.status = @status  ";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = " Select case when A.status=1 then 'PENDING' else Case When A.status = 2 then 'APPROVED' else Case when A.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,MV.DescOne TypeDesc,C.ID CurrencyID,ZA.ID ParentID,ZA.Name ParentName, B.ID Fxd11AccountID,B.Name Fxd11AccountName , A.* from FundJournalAccount A left join   " +
                            " Currency C on A.CurrencyPK = C.CurrencyPK and C.status = 2  left join   " +
                            " FundJournalAccount ZA on A.ParentPK = ZA.FundJournalAccountPK  and ZA.status in (1,2) left join " +
                            " MasterValue MV on A.Type=MV.Code and MV.ID ='FundJournalAccountType' " +
                            " Left join Fxd11Account B on A.Fxd11AccountPK = B.Fxd11AccountPK and B.status = 2 " ;
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_fundJournalAccount.Add(setFundJournalAccount(dr));
                                }
                            }
                            return L_fundJournalAccount;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int FundJournalAccount_Add(FundJournalAccount _fundJournalAccount, bool _havePrivillege)
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
                                 "Select isnull(max(FundJournalAccountPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@lastupdate from FundJournalAccount";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _fundJournalAccount.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);

                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],lastupdate)" +
                                "Select isnull(max(FundJournalAccountPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@lastupdate from FundJournalAccount";
                        }
                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@ID", _fundJournalAccount.ID);
                        cmd.Parameters.AddWithValue("@Name", _fundJournalAccount.Name);
                        cmd.Parameters.AddWithValue("@Type", _fundJournalAccount.Type);
                        cmd.Parameters.AddWithValue("@Groups", _fundJournalAccount.Groups);
                        cmd.Parameters.AddWithValue("@Levels", _fundJournalAccount.Levels);
                        cmd.Parameters.AddWithValue("@ParentPK", _fundJournalAccount.ParentPK);
                        cmd.Parameters.AddWithValue("@CurrencyPK", _fundJournalAccount.CurrencyPK);
                        cmd.Parameters.AddWithValue("@Show", _fundJournalAccount.Show);
                        cmd.Parameters.AddWithValue("@ParentPK1", _fundJournalAccount.ParentPK1);
                        cmd.Parameters.AddWithValue("@ParentPK2", _fundJournalAccount.ParentPK2);
                        cmd.Parameters.AddWithValue("@ParentPK3", _fundJournalAccount.ParentPK3);
                        cmd.Parameters.AddWithValue("@ParentPK4", _fundJournalAccount.ParentPK4);
                        cmd.Parameters.AddWithValue("@ParentPK5", _fundJournalAccount.ParentPK5);
                        cmd.Parameters.AddWithValue("@ParentPK6", _fundJournalAccount.ParentPK6);
                        cmd.Parameters.AddWithValue("@ParentPK7", _fundJournalAccount.ParentPK7);
                        cmd.Parameters.AddWithValue("@ParentPK8", _fundJournalAccount.ParentPK8);
                        cmd.Parameters.AddWithValue("@ParentPK9", _fundJournalAccount.ParentPK9);
                        cmd.Parameters.AddWithValue("@BitIsChange", _fundJournalAccount.BitIsChange); 
                        cmd.Parameters.AddWithValue("@Fxd11AccountPK", _fundJournalAccount.Fxd11AccountPK);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _fundJournalAccount.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();

                        return _host.Get_LastPKByLastUpate(_datetimeNow, "FundJournalAccount");
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }


        public int FundJournalAccount_Update(FundJournalAccount _fundJournalAccount, bool _havePrivillege)
        {
            try
            {
                int _newHisPK;
                int status = _host.Get_Status(_fundJournalAccount.FundJournalAccountPK, _fundJournalAccount.HistoryPK, "FundJournalAccount");
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update FundJournalAccount set status=2,Notes=@Notes,ID=@ID,Name=@Name,Type=@Type,Groups=@Groups," +
                                "Levels=@Levels,ParentPK=@ParentPK,CurrencyPK=@CurrencyPK," +
                                "Show=@Show,ParentPK1=@ParentPK1,ParentPK2=@ParentPK2,ParentPK3=@ParentPK3,ParentPK4=@ParentPK4," +
                                "ParentPK5=@ParentPK5,ParentPK6=@ParentPK6,ParentPK7=@ParentPK7,ParentPK8=@ParentPK8,ParentPK9=@ParentPK9,BitIsChange=@BitIsChange,Fxd11AccountPK=@Fxd11AccountPK," +
                                "ApprovedUsersID=@ApprovedUsersID,ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,lastupdate=@lastupdate " +
                                "where FundJournalAccountPK = @PK and historyPK = @HistoryPK";
                            cmd.Parameters.AddWithValue("@HistoryPK", _fundJournalAccount.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _fundJournalAccount.FundJournalAccountPK);
                            cmd.Parameters.AddWithValue("@ID", _fundJournalAccount.ID);
                            cmd.Parameters.AddWithValue("@Notes", _fundJournalAccount.Notes);
                            cmd.Parameters.AddWithValue("@Name", _fundJournalAccount.Name);
                            cmd.Parameters.AddWithValue("@Type", _fundJournalAccount.Type);
                            cmd.Parameters.AddWithValue("@Groups", _fundJournalAccount.Groups);
                            cmd.Parameters.AddWithValue("@Levels", _fundJournalAccount.Levels);
                            cmd.Parameters.AddWithValue("@ParentPK", _fundJournalAccount.ParentPK);
                            cmd.Parameters.AddWithValue("@CurrencyPK", _fundJournalAccount.CurrencyPK);
                            cmd.Parameters.AddWithValue("@Show", _fundJournalAccount.Show);
                            cmd.Parameters.AddWithValue("@ParentPK1", _fundJournalAccount.ParentPK1);
                            cmd.Parameters.AddWithValue("@ParentPK2", _fundJournalAccount.ParentPK2);
                            cmd.Parameters.AddWithValue("@ParentPK3", _fundJournalAccount.ParentPK3);
                            cmd.Parameters.AddWithValue("@ParentPK4", _fundJournalAccount.ParentPK4);
                            cmd.Parameters.AddWithValue("@ParentPK5", _fundJournalAccount.ParentPK5);
                            cmd.Parameters.AddWithValue("@ParentPK6", _fundJournalAccount.ParentPK6);
                            cmd.Parameters.AddWithValue("@ParentPK7", _fundJournalAccount.ParentPK7);
                            cmd.Parameters.AddWithValue("@ParentPK8", _fundJournalAccount.ParentPK8);
                            cmd.Parameters.AddWithValue("@ParentPK9", _fundJournalAccount.ParentPK9);
                            cmd.Parameters.AddWithValue("@BitIsChange", _fundJournalAccount.BitIsChange);
                            cmd.Parameters.AddWithValue("@Fxd11AccountPK", _fundJournalAccount.Fxd11AccountPK);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _fundJournalAccount.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _fundJournalAccount.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                            cmd.ExecuteNonQuery();
                        }

                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update FundJournalAccount set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,lastupdate=@lastupdate where FundJournalAccountPK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _fundJournalAccount.FundJournalAccountPK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _fundJournalAccount.EntryUsersID);
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
                                cmd.CommandText = "Update FundJournalAccount set Notes=@Notes,ID=@ID,Name=@Name,Type=@Type,Groups=@Groups," +
                                    "Levels=@Levels,ParentPK=@ParentPK,CurrencyPK=@CurrencyPK," +
                                    "Show=@Show,ParentPK1=@ParentPK1,ParentPK2=@ParentPK2,ParentPK3=@ParentPK3,ParentPK4=@ParentPK4," +
                                    "ParentPK5=@ParentPK5,ParentPK6=@ParentPK6,ParentPK7=@ParentPK7,ParentPK8=@ParentPK8,ParentPK9=@ParentPK9,BitIsChange=@BistIsChange,Fxd11AccountPK=@Fxd11AccountPK," +
                                    "UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,lastupdate=@lastupdate " +
                                    "where FundJournalAccountPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _fundJournalAccount.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _fundJournalAccount.FundJournalAccountPK);
                                cmd.Parameters.AddWithValue("@ID", _fundJournalAccount.ID);
                                cmd.Parameters.AddWithValue("@Notes", _fundJournalAccount.Notes);
                                cmd.Parameters.AddWithValue("@Name", _fundJournalAccount.Name);
                                cmd.Parameters.AddWithValue("@Type", _fundJournalAccount.Type);
                                cmd.Parameters.AddWithValue("@Groups", _fundJournalAccount.Groups);
                                cmd.Parameters.AddWithValue("@Levels", _fundJournalAccount.Levels);
                                cmd.Parameters.AddWithValue("@ParentPK", _fundJournalAccount.ParentPK);
                                cmd.Parameters.AddWithValue("@CurrencyPK", _fundJournalAccount.CurrencyPK);
                                cmd.Parameters.AddWithValue("@Show", _fundJournalAccount.Show);
                                cmd.Parameters.AddWithValue("@ParentPK1", _fundJournalAccount.ParentPK1);
                                cmd.Parameters.AddWithValue("@ParentPK2", _fundJournalAccount.ParentPK2);
                                cmd.Parameters.AddWithValue("@ParentPK3", _fundJournalAccount.ParentPK3);
                                cmd.Parameters.AddWithValue("@ParentPK4", _fundJournalAccount.ParentPK4);
                                cmd.Parameters.AddWithValue("@ParentPK5", _fundJournalAccount.ParentPK5);
                                cmd.Parameters.AddWithValue("@ParentPK6", _fundJournalAccount.ParentPK6);
                                cmd.Parameters.AddWithValue("@ParentPK7", _fundJournalAccount.ParentPK7);
                                cmd.Parameters.AddWithValue("@ParentPK8", _fundJournalAccount.ParentPK8);
                                cmd.Parameters.AddWithValue("@ParentPK9", _fundJournalAccount.ParentPK9);
                                cmd.Parameters.AddWithValue("@BitIsChange", _fundJournalAccount.BitIsChange);
                                cmd.Parameters.AddWithValue("@Fxd11AccountPK", _fundJournalAccount.Fxd11AccountPK);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _fundJournalAccount.EntryUsersID);
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
                                _newHisPK = _host.Get_NewHistoryPK(_fundJournalAccount.FundJournalAccountPK, "FundJournalAccount");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From FundJournalAccount where FundJournalAccountPK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _fundJournalAccount.FundJournalAccountPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _fundJournalAccount.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@ID", _fundJournalAccount.ID);
                                cmd.Parameters.AddWithValue("@Name", _fundJournalAccount.Name);
                                cmd.Parameters.AddWithValue("@Type", _fundJournalAccount.Type);
                                cmd.Parameters.AddWithValue("@Groups", _fundJournalAccount.Groups);
                                cmd.Parameters.AddWithValue("@Levels", _fundJournalAccount.Levels);
                                cmd.Parameters.AddWithValue("@ParentPK", _fundJournalAccount.ParentPK);
                                cmd.Parameters.AddWithValue("@CurrencyPK", _fundJournalAccount.CurrencyPK);
                                cmd.Parameters.AddWithValue("@Show", _fundJournalAccount.Show);
                                cmd.Parameters.AddWithValue("@ParentPK1", _fundJournalAccount.ParentPK1);
                                cmd.Parameters.AddWithValue("@ParentPK2", _fundJournalAccount.ParentPK2);
                                cmd.Parameters.AddWithValue("@ParentPK3", _fundJournalAccount.ParentPK3);
                                cmd.Parameters.AddWithValue("@ParentPK4", _fundJournalAccount.ParentPK4);
                                cmd.Parameters.AddWithValue("@ParentPK5", _fundJournalAccount.ParentPK5);
                                cmd.Parameters.AddWithValue("@ParentPK6", _fundJournalAccount.ParentPK6);
                                cmd.Parameters.AddWithValue("@ParentPK7", _fundJournalAccount.ParentPK7);
                                cmd.Parameters.AddWithValue("@ParentPK8", _fundJournalAccount.ParentPK8);
                                cmd.Parameters.AddWithValue("@ParentPK9", _fundJournalAccount.ParentPK9);
                                cmd.Parameters.AddWithValue("@BitIsChange", _fundJournalAccount.BitIsChange);
                                cmd.Parameters.AddWithValue("@Fxd11AccountPK", _fundJournalAccount.Fxd11AccountPK);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _fundJournalAccount.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update FundJournalAccount set status = 4, Notes=@Notes, " +
                                "lastupdate=@lastupdate where FundJournalAccountPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _fundJournalAccount.Notes);
                                cmd.Parameters.AddWithValue("@PK", _fundJournalAccount.FundJournalAccountPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _fundJournalAccount.HistoryPK);
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

        public void FundJournalAccount_Approved(FundJournalAccount _fundJournalAccount)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update FundJournalAccount set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,lastupdate=@lastupdate " +
                            "where FundJournalAccountPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _fundJournalAccount.FundJournalAccountPK);
                        cmd.Parameters.AddWithValue("@historyPK", _fundJournalAccount.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _fundJournalAccount.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update FundJournalAccount set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,lastupdate=@lastupdate where FundJournalAccountPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _fundJournalAccount.FundJournalAccountPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _fundJournalAccount.ApprovedUsersID);
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

        public void FundJournalAccount_Reject(FundJournalAccount _fundJournalAccount)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update FundJournalAccount set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,lastupdate=@lastupdate " +
                            "where FundJournalAccountPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _fundJournalAccount.FundJournalAccountPK);
                        cmd.Parameters.AddWithValue("@historyPK", _fundJournalAccount.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _fundJournalAccount.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update FundJournalAccount set status= 2,lastupdate=@lastupdate where FundJournalAccountPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _fundJournalAccount.FundJournalAccountPK);
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

        public void FundJournalAccount_Void(FundJournalAccount _fundJournalAccount)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update FundJournalAccount set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime, lastupdate=@lastupdate " +
                            "where FundJournalAccountPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _fundJournalAccount.FundJournalAccountPK);
                        cmd.Parameters.AddWithValue("@historyPK", _fundJournalAccount.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _fundJournalAccount.VoidUsersID);
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

        public FundJournalAccount FundJournalAccount_SelectByPK(int _fundJournalAccountPK)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = " Select case when A.status=1 then 'PENDING' else Case When A.status = 2 then 'APPROVED' else Case when A.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,MV.DescOne TypeDesc,C.ID CurrencyID, ZA.ID ParentID,ZA.Name ParentName,B.ID Fxd11AccountID,B.Name Fxd11AccountName, A.* from FundJournalAccount A left join   " +
                            " Currency C on A.CurrencyPK = C.CurrencyPK and C.status = 2  left join   " +
                            " FundJournalAccount ZA on A.ParentPK = ZA.FundJournalAccountPK  and ZA.status in (1,2) left join " +
                            " MasterValue MV on A.Type=MV.Code and MV.ID ='FundJournalAccountType' " +
                             " Left join Fxd11Account B on A.Fxd11AccountPK = B.Fxd11AccountPK and B.status = 2 " +
                            " where A.FundJournalAccountPK = @FundJournalAccountPK ";
                        cmd.Parameters.AddWithValue("@FundJournalAccountPK", _fundJournalAccountPK);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return setFundJournalAccount(dr);
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


        public FundJournalAccount FundJournalAccount_LookupCurrencyByFundJournalAccountPK(int _fundJournalAccountPK)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Select * from FundJournalAccount where status = 2 and FundJournalAccountPK = @FundJournalAccountPK";
                        cmd.Parameters.AddWithValue("@FundJournalAccountPK", _fundJournalAccountPK);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                FundJournalAccount M_fundJournalAccount = new FundJournalAccount();
                                M_fundJournalAccount.FundJournalAccountPK = Convert.ToInt32(dr["FundJournalAccountPK"]);
                                M_fundJournalAccount.ID = dr["ID"].ToString();
                                M_fundJournalAccount.Name = dr["Name"].ToString();
                                M_fundJournalAccount.CurrencyPK = Convert.ToInt32(dr["CurrencyPK"]);
                                return M_fundJournalAccount;

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


        public List<FundJournalAccountCombo> FundJournalAccount_Combo()
        {
            try
            {

                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<FundJournalAccountCombo> L_fundJournalAccount = new List<FundJournalAccountCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "SELECT  FundJournalAccountPK,ID + ' - ' + Name as ID, Name FROM [FundJournalAccount]  where status = 2  order by ID,Name";
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    FundJournalAccountCombo M_fundJournalAccount = new FundJournalAccountCombo();
                                    M_fundJournalAccount.FundJournalAccountPK = Convert.ToInt32(dr["FundJournalAccountPK"]);
                                    M_fundJournalAccount.ID = Convert.ToString(dr["ID"]);
                                    M_fundJournalAccount.Name = Convert.ToString(dr["Name"]);
                                    L_fundJournalAccount.Add(M_fundJournalAccount);
                                }
                            }
                            return L_fundJournalAccount;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public List<FundJournalAccountCombo> FundJournalAccount_ComboChildOnly()
        {
            try
            {

                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<FundJournalAccountCombo> L_fundJournalAccount = new List<FundJournalAccountCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "SELECT  FundJournalAccountPK,ID + ' - ' + Name as ID, Name FROM [FundJournalAccount]  where status = 2 and Groups = 0 order by ID,Name";
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    FundJournalAccountCombo M_fundJournalAccount = new FundJournalAccountCombo();
                                    M_fundJournalAccount.FundJournalAccountPK = Convert.ToInt32(dr["FundJournalAccountPK"]);
                                    M_fundJournalAccount.ID = Convert.ToString(dr["ID"]);
                                    M_fundJournalAccount.Name = Convert.ToString(dr["Name"]);
                                    L_fundJournalAccount.Add(M_fundJournalAccount);
                                }
                            }
                            return L_fundJournalAccount;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public List<FundJournalAccountCombo> FundJournalAccount_ComboGroupsOnly()
        {
            try
            {

                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<FundJournalAccountCombo> L_fundJournalAccount = new List<FundJournalAccountCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "SELECT  FundJournalAccountPK,ID + ' - ' + Name as ID, Name FROM [FundJournalAccount]  where status = 2 and Groups = 1 order by ID,Name";
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    FundJournalAccountCombo M_fundJournalAccount = new FundJournalAccountCombo();
                                    M_fundJournalAccount.FundJournalAccountPK = Convert.ToInt32(dr["FundJournalAccountPK"]);
                                    M_fundJournalAccount.ID = Convert.ToString(dr["ID"]);
                                    M_fundJournalAccount.Name = Convert.ToString(dr["Name"]);
                                    L_fundJournalAccount.Add(M_fundJournalAccount);
                                }
                            }
                            return L_fundJournalAccount;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public List<FundJournalAccountCombo> FundJournalAccount_ParentCombo()
        {
            try
            {

                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<FundJournalAccountCombo> L_fundJournalAccount = new List<FundJournalAccountCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "SELECT  FundJournalAccountPK,ID + ' - ' + Name as ParentID, Name as ParentName FROM [FundJournalAccount]  where status in (1, 2) and Groups = 1 order by ID,Name";
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    FundJournalAccountCombo M_fundJournalAccount = new FundJournalAccountCombo();
                                    M_fundJournalAccount.FundJournalAccountPK = Convert.ToInt32(dr["FundJournalAccountPK"]);
                                    M_fundJournalAccount.ID = Convert.ToString(dr["ParentID"]);
                                    M_fundJournalAccount.Name = Convert.ToString(dr["ParentName"]);
                                    L_fundJournalAccount.Add(M_fundJournalAccount);
                                }
                            }
                            return L_fundJournalAccount;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }



        public bool FundJournalAccount_UpdateParentAndDepth()
        {
            try
            {
                using (SqlConnection DbConSubQuery = new SqlConnection(Tools.conString))
                {
                    DbConSubQuery.Open();
                    using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                    {
                        DbCon.Open();
                        using (SqlCommand cmd = DbConSubQuery.CreateCommand())
                        {
                            cmd.CommandText = "UPDATE FundJournalAccount SET " +
                                                    "FundJournalAccount.ParentPK1 = isnull(FundJournalAccount_1.FundJournalAccountPK,0), FundJournalAccount.ParentPK2 = isnull(FundJournalAccount_2.FundJournalAccountPK,0), " +
                                                    "FundJournalAccount.ParentPK3 = isnull(FundJournalAccount_3.FundJournalAccountPK,0), FundJournalAccount.ParentPK4 = isnull(FundJournalAccount_4.FundJournalAccountPK,0), " +
                                                    "FundJournalAccount.ParentPK5 = isnull(FundJournalAccount_5.FundJournalAccountPK,0), FundJournalAccount.ParentPK6 = isnull(FundJournalAccount_6.FundJournalAccountPK,0), " +
                                                    "FundJournalAccount.ParentPK7 = isnull(FundJournalAccount_7.FundJournalAccountPK,0), FundJournalAccount.ParentPK8 = isnull(FundJournalAccount_8.FundJournalAccountPK,0), " +
                                                    "FundJournalAccount.ParentPK9 = isnull(FundJournalAccount_9.FundJournalAccountPK,0)  " +
                                              "FROM FundJournalAccount " +
                                                    "LEFT JOIN FundJournalAccount AS FundJournalAccount_1 ON FundJournalAccount.ParentPK = FundJournalAccount_1.FundJournalAccountPK " +
                                                    "LEFT JOIN FundJournalAccount AS FundJournalAccount_2 ON FundJournalAccount_1.ParentPK = FundJournalAccount_2.FundJournalAccountPK " +
                                                    "LEFT JOIN FundJournalAccount AS FundJournalAccount_3 ON FundJournalAccount_2.ParentPK = FundJournalAccount_3.FundJournalAccountPK " +
                                                    "LEFT JOIN FundJournalAccount AS FundJournalAccount_4 ON FundJournalAccount_3.ParentPK = FundJournalAccount_4.FundJournalAccountPK " +
                                                    "LEFT JOIN FundJournalAccount AS FundJournalAccount_5 ON FundJournalAccount_4.ParentPK = FundJournalAccount_5.FundJournalAccountPK " +
                                                    "LEFT JOIN FundJournalAccount AS FundJournalAccount_6 ON FundJournalAccount_5.ParentPK = FundJournalAccount_6.FundJournalAccountPK " +
                                                    "LEFT JOIN FundJournalAccount AS FundJournalAccount_7 ON FundJournalAccount_6.ParentPK = FundJournalAccount_7.FundJournalAccountPK " +
                                                    "LEFT JOIN FundJournalAccount AS FundJournalAccount_8 ON FundJournalAccount_7.ParentPK = FundJournalAccount_8.FundJournalAccountPK " +
                                                    "LEFT JOIN FundJournalAccount AS FundJournalAccount_9 ON FundJournalAccount_8.ParentPK = FundJournalAccount_9.FundJournalAccountPK Where FundJournalAccount.Status = 2";
                            cmd.ExecuteNonQuery();
                            cmd.CommandText = "Select FundJournalAccountPK From FundJournalAccount Where Status = 2 and Groups = 1";
                            using (SqlDataReader dr = cmd.ExecuteReader())
                            {
                                if (dr.HasRows)
                                {
                                    try
                                    {
                                        while (dr.Read())
                                        {
                                            using (SqlCommand cmdSubQuery = DbCon.CreateCommand())
                                            {
                                                cmdSubQuery.CommandText = "Update FundJournalAccount set Depth = @Depth Where FundJournalAccountPK = @FundJournalAccountPK and Status = 2";
                                                cmdSubQuery.Parameters.AddWithValue("@Depth", GetFundJournalAccountDepth(Convert.ToInt32(dr["FundJournalAccountPK"])));
                                                cmdSubQuery.Parameters.AddWithValue("@FundJournalAccountPK", Convert.ToInt32(dr["FundJournalAccountPK"]));
                                                cmdSubQuery.ExecuteNonQuery();
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
                        return true;
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        private int GetFundJournalAccountDepth(int _fundJournalAccountPK)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "DECLARE @Depth INT,@Depth1 INT, @Depth2 INT, @Depth3 INT, @Depth4 INT, @Depth5 INT, " +
                                          "@Depth6 INT, @Depth7 INT, @Depth8 INT, @Depth9 INT, @Depth10 INT " +
                                          "SELECT @Depth1 = MAX(FundJournalAccount_1.ParentPK), @Depth2 = MAX(FundJournalAccount_2.ParentPK), " +
                                          "@Depth3 = MAX(FundJournalAccount_3.ParentPK), @Depth4 = MAX(FundJournalAccount_4.ParentPK), " +
                                          "@Depth5 = MAX(FundJournalAccount_5.ParentPK), @Depth6 = MAX(FundJournalAccount_6.ParentPK), " +
                                          "@Depth7 = MAX(FundJournalAccount_7.ParentPK), @Depth8 = MAX(FundJournalAccount_8.ParentPK), " +
                                          "@Depth9 = MAX(FundJournalAccount_9.ParentPK), @Depth10 = MAX(FundJournalAccount_10.ParentPK) " +
                                          "FROM FundJournalAccount AS FundJournalAccount_10 RIGHT JOIN (FundJournalAccount AS FundJournalAccount_9 " +
                                          "RIGHT JOIN (FundJournalAccount AS FundJournalAccount_8 RIGHT JOIN (FundJournalAccount AS FundJournalAccount_7 " +
                                          "RIGHT JOIN (FundJournalAccount AS FundJournalAccount_6 RIGHT JOIN (FundJournalAccount AS FundJournalAccount_5 " +
                                          "RIGHT JOIN (FundJournalAccount AS FundJournalAccount_4 RIGHT JOIN (FundJournalAccount AS FundJournalAccount_3 " +
                                          "RIGHT JOIN (FundJournalAccount AS FundJournalAccount_2 RIGHT JOIN (FundJournalAccount AS FundJournalAccount_1 " +
                                          "RIGHT JOIN FundJournalAccount ON FundJournalAccount_1.ParentPK = FundJournalAccount.FundJournalAccountPK) " +
                                          "ON FundJournalAccount_2.ParentPK = FundJournalAccount_1.FundJournalAccountPK) ON FundJournalAccount_3.ParentPK = FundJournalAccount_2.FundJournalAccountPK) " +
                                          "ON FundJournalAccount_4.ParentPK = FundJournalAccount_3.FundJournalAccountPK) ON FundJournalAccount_5.ParentPK = FundJournalAccount_4.FundJournalAccountPK) " +
                                          "ON FundJournalAccount_6.ParentPK = FundJournalAccount_5.FundJournalAccountPK) ON FundJournalAccount_7.ParentPK = FundJournalAccount_6.FundJournalAccountPK) " +
                                          "ON FundJournalAccount_8.ParentPK = FundJournalAccount_7.FundJournalAccountPK) ON FundJournalAccount_9.ParentPK = FundJournalAccount_8.FundJournalAccountPK) " +
                                          "ON FundJournalAccount_10.ParentPK = FundJournalAccount_9.FundJournalAccountPK  " +
                                          "WHERE FundJournalAccount.FundJournalAccountPK = @FundJournalAccountPK and FundJournalAccount.Status = 2 " +
                                          "IF @Depth1 IS NULL " +
                                          "SET @Depth = 0  " +
                                          "ELSE IF @Depth2 IS NULL " +
                                          "SET @Depth = 1 " +
                                          "ELSE IF @Depth3 IS NULL " +
                                          "SET @Depth = 2 " +
                                          "ELSE IF @Depth4 IS NULL " +
                                          "SET @Depth = 3 " +
                                          "ELSE IF @Depth5 IS NULL " +
                                          "SET @Depth = 4 " +
                                          "ELSE IF @Depth6 IS NULL " +
                                          "SET @Depth = 5 " +
                                          "ELSE IF @Depth7 IS NULL " +
                                          "SET @Depth = 6 " +
                                          "ELSE IF @Depth8 IS NULL " +
                                          "SET @Depth = 7 " +
                                          "ELSE IF @Depth9 IS NULL " +
                                          "SET @Depth = 8  " +
                                          "ELSE IF @Depth10 IS NULL " +
                                          "SET @Depth = 9  " +
                                          "ELSE " +
                                          "SET @Depth = 0 " +
                                          "select @depth depth";
                        cmd.Parameters.AddWithValue("@FundJournalAccountPK", _fundJournalAccountPK);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (!dr.Read())
                            {
                                return 0;
                            }
                            else
                            {
                                return Convert.ToInt32(dr["depth"]);
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




    }
}