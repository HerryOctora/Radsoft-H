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
    public class FundCashRefReps
    {
        Host _host = new Host();

        //1
        string _insertCommand = "INSERT INTO [dbo].[FundCashRef] " +
                            "([FundCashRefPK],[HistoryPK],[Status],[ID],[Name],[Type],[FundJournalAccountPK],[CurrencyPK],[BankBranchPK],[BankAccountNo],[FundPK],[Remark],[IsPublic],[bitdefaultinvestment],[BankAccountName],[SafeKeepingAccountNo],";
        string _paramaterCommand = "@ID,@Name,@Type,@FundJournalAccountPK,@CurrencyPK,@BankBranchPK,@BankAccountNo,@FundPK,@Remark,@IsPublic,@bitdefaultinvestment,@BankAccountName,@SafeKeepingAccountNo,";

        //2
        private FundCashRef setFundCashRef(SqlDataReader dr)
        {
            FundCashRef M_FundCashRef = new FundCashRef();
            M_FundCashRef.FundCashRefPK = Convert.ToInt32(dr["FundCashRefPK"]);
            M_FundCashRef.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_FundCashRef.Status = Convert.ToInt32(dr["Status"]);
            M_FundCashRef.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_FundCashRef.Notes = dr["Notes"].ToString();
            M_FundCashRef.ID = dr["ID"].ToString();
            M_FundCashRef.Name = dr["Name"].ToString();
            M_FundCashRef.Type = Convert.ToInt32(dr["Type"]);
            M_FundCashRef.TypeDesc = Convert.ToString(dr["TypeDesc"]);
            M_FundCashRef.FundJournalAccountPK = Convert.ToInt32(dr["FundJournalAccountPK"]);
            M_FundCashRef.FundJournalAccountID = Convert.ToString(dr["FundJournalAccountID"]);
            M_FundCashRef.FundJournalAccountName = Convert.ToString(dr["FundJournalAccountName"]);
            M_FundCashRef.CurrencyPK = Convert.ToInt32(dr["CurrencyPK"]);
            M_FundCashRef.CurrencyID = Convert.ToString(dr["CurrencyID"]);
            M_FundCashRef.BankBranchPK = Convert.ToInt32(dr["BankBranchPK"]);
            M_FundCashRef.BankBranchID = dr["BankBranchID"].ToString();
            M_FundCashRef.BankAccountNo = dr["BankAccountNo"].ToString();
            M_FundCashRef.BankAccountName = dr["BankAccountName"].ToString();
            M_FundCashRef.SafeKeepingAccountNo = dr["SafeKeepingAccountNo"].ToString();
            M_FundCashRef.FundPK = Convert.ToInt32(dr["FundPK"]);
            M_FundCashRef.FundID = Convert.ToString(dr["FundID"]);
            M_FundCashRef.FundName = Convert.ToString(dr["FundName"]);
            M_FundCashRef.IsPublic = Convert.ToBoolean(dr["IsPublic"]);
            M_FundCashRef.bitdefaultinvestment = Convert.ToBoolean(dr["bitdefaultinvestment"]);
            M_FundCashRef.Remark = dr["Remark"].ToString();
            M_FundCashRef.EntryUsersID = dr["EntryUsersID"].ToString();
            M_FundCashRef.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_FundCashRef.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_FundCashRef.VoidUsersID = dr["VoidUsersID"].ToString();
            M_FundCashRef.EntryTime = dr["EntryTime"].ToString();
            M_FundCashRef.UpdateTime = dr["UpdateTime"].ToString();
            M_FundCashRef.ApprovedTime = dr["ApprovedTime"].ToString();
            M_FundCashRef.VoidTime = dr["VoidTime"].ToString();
            M_FundCashRef.DBUserID = dr["DBUserID"].ToString();
            M_FundCashRef.DBTerminalID = dr["DBTerminalID"].ToString();
            M_FundCashRef.LastUpdate = dr["LastUpdate"].ToString();
            M_FundCashRef.LastUpdateDB = Convert.ToString(dr["LastUpdateDB"]);
            return M_FundCashRef;
        }

        public List<FundCashRef> FundCashRef_Select(int _status)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<FundCashRef> L_fundCashRef = new List<FundCashRef>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = @"Select case when CR.status=1 then 'PENDING' else Case When CR.status = 2 then 'APPROVED' else Case when CR.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,A.ID FundJournalAccountID,A.Name FundJournalAccountName, C.ID CurrencyID, BC1.ID BankBranchID,MV.DescOne TypeDesc,
                            case when CR.FundPK = 0 then 'All' else F.ID End FundID,case when CR.FundPK = 0 then 'All Fund' else F.Name End FundName,  CR.* from FundCashRef CR left join
                            FundJournalAccount A on CR.FundJournalAccountPK = A.FundJournalAccountPK and A.status =2 left join 
                            Currency C on CR.CurrencyPK = C.CurrencyPK and C.status = 2 left join 
                            BankBranch BC1 on CR.BankBranchPK = BC1.BankBranchPK and BC1.status = 2 left join
                            MasterValue MV on CR.Type = MV.Code and MV.status = 2 and MV.ID ='TypeUnitRegistry' left join 
                            Fund F on CR.FundPK = F.FundPK and F.status = 2 
                                where CR.status = @status  ";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = @"Select case when CR.status=1 then 'PENDING' else Case When CR.status = 2 then 'APPROVED' else Case when CR.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,A.ID FundJournalAccountID,A.Name FundJournalAccountName, C.ID CurrencyID, BC1.ID BankBranchID,MV.DescOne TypeDesc,
                            case when CR.FundPK = 0 then 'All' else F.ID End FundID,case when CR.FundPK = 0 then 'All Fund' else F.Name End FundName,  CR.* from FundCashRef CR left join
                            FundJournalAccount A on CR.FundJournalAccountPK = A.FundJournalAccountPK and A.status =2 left join 
                            Currency C on CR.CurrencyPK = C.CurrencyPK and C.status = 2 left join 
                            BankBranch BC1 on CR.BankBranchPK = BC1.BankBranchPK and BC1.status = 2 left join
                            MasterValue MV on CR.Type = MV.Code and MV.status = 2 and MV.ID ='TypeUnitRegistry' left join 
                            Fund F on CR.FundPK = F.FundPK and F.status = 2  ";
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_fundCashRef.Add(setFundCashRef(dr));
                                }
                            }
                            return L_fundCashRef;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int FundCashRef_Add(FundCashRef _fundCashRef, bool _havePrivillege)
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
                                 "Select isnull(max(FundCashRefPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@lastupdate from FundCashRef";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _fundCashRef.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);

                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],lastupdate)" +
                                "Select isnull(max(FundCashRefPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@lastupdate from FundCashRef";
                        }
                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@ID", _fundCashRef.ID);
                        cmd.Parameters.AddWithValue("@Name", _fundCashRef.Name);
                        cmd.Parameters.AddWithValue("@Type", _fundCashRef.Type);
                        cmd.Parameters.AddWithValue("@FundJournalAccountPK", _fundCashRef.FundJournalAccountPK);
                        cmd.Parameters.AddWithValue("@CurrencyPK", _fundCashRef.CurrencyPK);
                        cmd.Parameters.AddWithValue("@BankBranchPK", _fundCashRef.BankBranchPK);
                        cmd.Parameters.AddWithValue("@BankAccountNo", _fundCashRef.BankAccountNo);
                        cmd.Parameters.AddWithValue("@BankAccountName", _fundCashRef.BankAccountName);
                        cmd.Parameters.AddWithValue("@SafeKeepingAccountNo", _fundCashRef.SafeKeepingAccountNo);
                        cmd.Parameters.AddWithValue("@FundPK", _fundCashRef.FundPK);
                        cmd.Parameters.AddWithValue("@IsPublic", _fundCashRef.IsPublic);
                        cmd.Parameters.AddWithValue("@bitdefaultinvestment", _fundCashRef.bitdefaultinvestment);
                        cmd.Parameters.AddWithValue("@Remark", _fundCashRef.Remark);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _fundCashRef.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();

                        return _host.Get_LastPKByLastUpate(_datetimeNow, "FundCashRef");
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }


        public int FundCashRef_Update(FundCashRef _fundCashRef, bool _havePrivillege)
        {
            try
            {
                int _newHisPK;
                DateTime _datetimeNow = DateTime.Now;
                int status = _host.Get_Status(_fundCashRef.FundCashRefPK, _fundCashRef.HistoryPK, "FundCashRef");
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = @"Update FundCashRef set status=2, Notes=@Notes,ID=@ID,Name=@Name,Type=@Type,FundJournalAccountPK=@FundJournalAccountPK,CurrencyPK=@CurrencyPK,BankBranchPK=@BankBranchPK,BankAccountNo=@BankAccountNo,
                                    FundPK=@FundPK,Remark=@Remark,IsPublic=@IsPublic,bitdefaultinvestment=@bitdefaultinvestment,BankAccountName=@BankAccountName,SafeKeepingAccountNo = @SafeKeepingAccountNo
                                    ApprovedUsersID=@ApprovedUsersID,ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,lastupdate=@lastupdate 
                                    where FundCashRefPK = @PK and historyPK = @HistoryPK";
                            cmd.Parameters.AddWithValue("@HistoryPK", _fundCashRef.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _fundCashRef.FundCashRefPK);
                            cmd.Parameters.AddWithValue("@ID", _fundCashRef.ID);
                            cmd.Parameters.AddWithValue("@Notes", _fundCashRef.Notes);
                            cmd.Parameters.AddWithValue("@Name", _fundCashRef.Name);
                            cmd.Parameters.AddWithValue("@Type", _fundCashRef.Type);
                            cmd.Parameters.AddWithValue("@FundJournalAccountPK", _fundCashRef.FundJournalAccountPK);
                            cmd.Parameters.AddWithValue("@CurrencyPK", _fundCashRef.CurrencyPK);
                            cmd.Parameters.AddWithValue("@BankBranchPK", _fundCashRef.BankBranchPK);
                            cmd.Parameters.AddWithValue("@BankAccountNo", _fundCashRef.BankAccountNo);
                            cmd.Parameters.AddWithValue("@BankAccountName", _fundCashRef.BankAccountName);
                            cmd.Parameters.AddWithValue("@SafeKeepingAccountNo", _fundCashRef.SafeKeepingAccountNo);
                            cmd.Parameters.AddWithValue("@FundPK", _fundCashRef.FundPK);
                            cmd.Parameters.AddWithValue("@IsPublic", _fundCashRef.IsPublic);
                            cmd.Parameters.AddWithValue("@bitdefaultinvestment", _fundCashRef.bitdefaultinvestment);
                            cmd.Parameters.AddWithValue("@Remark", _fundCashRef.Remark);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _fundCashRef.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _fundCashRef.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = @"Update FundCashRef set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,lastupdate=@lastupdate where FundCashRefPK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _fundCashRef.FundCashRefPK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _fundCashRef.EntryUsersID);
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
                                cmd.CommandText = @"Update FundCashRef set Notes=@Notes,ID=@ID,Name=@Name,Type=@Type,FundJournalAccountPK=@FundJournalAccountPK,CurrencyPK=@CurrencyPK,BankBranchPK=@BankBranchPK,BankAccountNo=@BankAccountNo,
                                    FundPK=@FundPK,Remark=@Remark,IsPublic=@IsPublic,bitdefaultinvestment=@bitdefaultinvestment,BankAccountName=@BankAccountName,SafeKeepingAccountNo = @SafeKeepingAccountNo
                                    UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,lastupdate=@lastupdate 
                                    where FundCashRefPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _fundCashRef.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _fundCashRef.FundCashRefPK);
                                cmd.Parameters.AddWithValue("@ID", _fundCashRef.ID);
                                cmd.Parameters.AddWithValue("@Notes", _fundCashRef.Notes);
                                cmd.Parameters.AddWithValue("@Name", _fundCashRef.Name);
                                cmd.Parameters.AddWithValue("@Type", _fundCashRef.Type);
                                cmd.Parameters.AddWithValue("@FundJournalAccountPK", _fundCashRef.FundJournalAccountPK);
                                cmd.Parameters.AddWithValue("@CurrencyPK", _fundCashRef.CurrencyPK);
                                cmd.Parameters.AddWithValue("@BankBranchPK", _fundCashRef.BankBranchPK);
                                cmd.Parameters.AddWithValue("@BankAccountNo", _fundCashRef.BankAccountNo);
                                cmd.Parameters.AddWithValue("@BankAccountName", _fundCashRef.BankAccountName);
                                cmd.Parameters.AddWithValue("@SafeKeepingAccountNo", _fundCashRef.SafeKeepingAccountNo);
                                cmd.Parameters.AddWithValue("@IsPublic", _fundCashRef.IsPublic);
                                cmd.Parameters.AddWithValue("@bitdefaultinvestment", _fundCashRef.bitdefaultinvestment);
                                cmd.Parameters.AddWithValue("@FundPK", _fundCashRef.FundPK);
                                cmd.Parameters.AddWithValue("@Remark", _fundCashRef.Remark);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _fundCashRef.EntryUsersID);
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
                                _newHisPK = _host.Get_NewHistoryPK(_fundCashRef.FundCashRefPK, "FundCashRef");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From FundCashRef where FundCashRefPK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _fundCashRef.FundCashRefPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _fundCashRef.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@ID", _fundCashRef.ID);
                                cmd.Parameters.AddWithValue("@Name", _fundCashRef.Name);
                                cmd.Parameters.AddWithValue("@Type", _fundCashRef.Type);
                                cmd.Parameters.AddWithValue("@FundJournalAccountPK", _fundCashRef.FundJournalAccountPK);
                                cmd.Parameters.AddWithValue("@CurrencyPK", _fundCashRef.CurrencyPK);
                                cmd.Parameters.AddWithValue("@BankBranchPK", _fundCashRef.BankBranchPK);
                                cmd.Parameters.AddWithValue("@BankAccountNo", _fundCashRef.BankAccountNo);
                                cmd.Parameters.AddWithValue("@BankAccountName", _fundCashRef.BankAccountName);
                                cmd.Parameters.AddWithValue("@SafeKeepingAccountNo", _fundCashRef.SafeKeepingAccountNo);
                                cmd.Parameters.AddWithValue("@IsPublic", _fundCashRef.IsPublic);
                                cmd.Parameters.AddWithValue("@bitdefaultinvestment", _fundCashRef.bitdefaultinvestment);
                                cmd.Parameters.AddWithValue("@FundPK", _fundCashRef.FundPK);
                                cmd.Parameters.AddWithValue("@Remark", _fundCashRef.Remark);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _fundCashRef.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update FundCashRef set status= 4,Notes=@Notes, " +
                                    "lastupdate=@lastupdate where FundCashRefPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _fundCashRef.Notes);
                                cmd.Parameters.AddWithValue("@PK", _fundCashRef.FundCashRefPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _fundCashRef.HistoryPK);
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

        public void FundCashRef_Approved(FundCashRef _fundCashRef)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update FundCashRef set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,lastupdate=@lastupdate " +
                            "where FundCashRefPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _fundCashRef.FundCashRefPK);
                        cmd.Parameters.AddWithValue("@historyPK", _fundCashRef.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _fundCashRef.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update FundCashRef set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,lastupdate=@lastupdate where FundCashRefPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _fundCashRef.FundCashRefPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _fundCashRef.ApprovedUsersID);
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

        public void FundCashRef_Reject(FundCashRef _fundCashRef)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update FundCashRef set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,lastupdate=@lastupdate " +
                            "where FundCashRefPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _fundCashRef.FundCashRefPK);
                        cmd.Parameters.AddWithValue("@historyPK", _fundCashRef.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _fundCashRef.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update FundCashRef set status= 2,lastupdate=@lastupdate where FundCashRefPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _fundCashRef.FundCashRefPK);
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

        public void FundCashRef_Void(FundCashRef _fundCashRef)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update FundCashRef set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,lastupdate=@lastupdate " +
                            "where FundCashRefPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _fundCashRef.FundCashRefPK);
                        cmd.Parameters.AddWithValue("@historyPK", _fundCashRef.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _fundCashRef.VoidUsersID);
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

        public List<FundCashRefCombo> FundCashRef_Combo()
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<FundCashRefCombo> L_FundCashRef = new List<FundCashRefCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "SELECT  FundCashRefPK,ID + ' - ' + Name as ID, Name, FundJournalAccountPK FROM [FundCashRef]  where status = 2 order by ID,Name";

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    FundCashRefCombo M_FundCashRef = new FundCashRefCombo();
                                    M_FundCashRef.FundCashRefPK = Convert.ToInt32(dr["FundCashRefPK"]);
                                    M_FundCashRef.ID = Convert.ToString(dr["ID"]);
                                    M_FundCashRef.Name = Convert.ToString(dr["Name"]);
                                    M_FundCashRef.FundJournalAccountPK = Convert.ToInt32(dr["FundJournalAccountPK"]);
                                    L_FundCashRef.Add(M_FundCashRef);
                                }

                            }
                            return L_FundCashRef;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }


        public List<FundCashRefCombo> FundCashRef_ComboByFundPK(int _fundPK, string _type)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<FundCashRefCombo> L_FundCashRef = new List<FundCashRefCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        if (_fundPK != 0 && _type == "SUB")
                        {
                            cmd.CommandText = "SELECT  FC.FundCashRefPK,FC.Name + ' - ' + FC.BankAccountNo as ID, FC.Name Name,FC.Remark Remark FROM [FundCashRef] FC left join Fund F on FC.FundPK = F.FundPK and F.status = 2 where FC.status = 2 and FC.FundPK =@FundPK and FC.Type in (1,3) ";
                            cmd.Parameters.AddWithValue("@FundPK", _fundPK);

                        }
                        else if (_fundPK != 0 && _type == "RED")
                        {
                            if (Tools.ClientCode == "29")
                            {
                                cmd.CommandText = "SELECT  FC.FundCashRefPK,FC.Name + ' - ' + FC.BankAccountNo + ' - ' + case when FC.Type = 1 then 'SUB' when FC.Type = 2 then 'RED' else 'ALL' end as ID, FC.Name Name,FC.Remark Remark FROM [FundCashRef] FC left join Fund F on FC.FundPK = F.FundPK and F.status = 2 where FC.status = 2 and FC.FundPK =@FundPK and FC.Type in (2,3) ";
                                cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                            }
                            else
                            {
                                cmd.CommandText = "SELECT  FC.FundCashRefPK,FC.Name + ' - ' + FC.BankAccountNo as ID, FC.Name Name,FC.Remark Remark FROM [FundCashRef] FC left join Fund F on FC.FundPK = F.FundPK and F.status = 2 where FC.status = 2 and FC.FundPK =@FundPK and FC.Type in (2,3) ";
                                cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                            }


                        }
                        else if (_fundPK != 0 && _type == "INV")
                        {
                            cmd.CommandText = "SELECT  FC.FundCashRefPK,FC.Name + ' - ' + FC.BankAccountNo as ID, FC.Name Name,FC.Remark Remark FROM [FundCashRef] FC left join Fund F on FC.FundPK = F.FundPK and F.status = 2 where FC.status = 2 and FC.FundPK =@FundPK and FC.Type in (4) ";
                            cmd.Parameters.AddWithValue("@FundPK", _fundPK);

                        }
                        else
                        {
                            cmd.CommandText = "SELECT  FC.FundCashRefPK,FC.Name + ' - ' + FC.BankAccountNo as ID, FC.Name Name,FC.Remark Remark FROM [FundCashRef] FC left join Fund F on FC.FundPK = F.FundPK and F.status = 2 where FC.status = 2 ";

                        }


                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    FundCashRefCombo M_FundCashRef = new FundCashRefCombo();
                                    M_FundCashRef.FundCashRefPK = Convert.ToInt32(dr["FundCashRefPK"]);
                                    M_FundCashRef.ID = Convert.ToString(dr["ID"]);
                                    M_FundCashRef.Name = Convert.ToString(dr["Name"]);
                                    M_FundCashRef.Remark = Convert.ToString(dr["Remark"]);
                                    L_FundCashRef.Add(M_FundCashRef);
                                }

                            }
                            return L_FundCashRef;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }



        public string Get_RemarkByFundCashRefPK(int _fundCashRefPK)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<FundCashRefCombo> L_FundCashRef = new List<FundCashRefCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {


                            cmd.CommandText = "SELECT Remark FROM [FundCashRef]  where status =2 and FundCashRefPK = @FundCashRefPK ";
                            cmd.Parameters.AddWithValue("@FundCashRefPK", _fundCashRefPK);


                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    return Convert.ToString(dr["Remark"]);
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
        public List<FundCashRefCombo> FundCashRef_ComboByFundClientCashRef(int _fundPK, int _fundClientPK, string _type)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<FundCashRefCombo> L_FundCashRef = new List<FundCashRefCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        string _paramFundClient = "";

                        if (_fundClientPK != 0)
                        {
                            _paramFundClient = "and B.FundClientPK = @FundClientPK ";
                        }
                        else
                        {
                            _paramFundClient = "";
                        }

                        if (_fundPK != 0 && _type == "SUB")
                        {
                            cmd.CommandText = @"SELECT  A.FundCashRefPK,A.Name + ' - ' + A.BankAccountNo as ID, A.Name Name,A.Remark Remark FROM [FundCashRef] A 
                            left join FundClientCashRef B on A.FundCashRefPK = B.FundCashRefPK and A.FundPK = B.FundPK and B.status = 2 
                            where A.status = 2 " + _paramFundClient + " and A.FundPK = @FundPK and A.Type in (1,3) ";

                            if (_fundClientPK != 0)
                            {
                                cmd.Parameters.AddWithValue("@FundClientPK", _fundClientPK);
                            }
                            cmd.Parameters.AddWithValue("@FundPK", _fundPK);

                        }
                        else if (_fundPK != 0 && _type == "RED")
                        {
                            cmd.CommandText = @"SELECT  A.FundCashRefPK,A.Name + ' - ' + A.BankAccountNo as ID, A.Name Name,A.Remark Remark FROM [FundCashRef] A 
                            left join FundClientCashRef B on A.FundCashRefPK = B.FundCashRefPK and A.FundPK = B.FundPK and B.status = 2 
                            where A.status = 2 " + _paramFundClient + " and A.FundPK = @FundPK and A.Type in (2,3) ";

                            if (_fundClientPK != 0)
                            {
                                cmd.Parameters.AddWithValue("@FundClientPK", _fundClientPK);
                            }
                            cmd.Parameters.AddWithValue("@FundPK", _fundPK);



                        }
                        else
                        {
                            cmd.CommandText = @"SELECT  A.FundCashRefPK,A.Name + ' - ' + A.BankAccountNo as ID, A.Name Name,A.Remark Remark FROM [FundCashRef] A 
                            left join FundClientCashRef B on A.FundCashRefPK = B.FundCashRefPK and A.FundPK = B.FundPK and B.status = 2 
                            where A.status = 2 " + _paramFundClient + " and A.FundPK = @FundPK  ";

                            if (_fundClientPK != 0)
                            {
                                cmd.Parameters.AddWithValue("@FundClientPK", _fundClientPK);
                            }
                            cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                        }


                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    FundCashRefCombo M_FundCashRef = new FundCashRefCombo();
                                    M_FundCashRef.FundCashRefPK = Convert.ToInt32(dr["FundCashRefPK"]);
                                    M_FundCashRef.ID = Convert.ToString(dr["ID"]);
                                    M_FundCashRef.Name = Convert.ToString(dr["Name"]);
                                    M_FundCashRef.Remark = Convert.ToString(dr["Remark"]);
                                    L_FundCashRef.Add(M_FundCashRef);
                                }

                            }
                            return L_FundCashRef;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }


        public List<FundCashRefCombo> FundCashRef_ComboByBankBranchPK(int _fundPK, int _bankBranchPK)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<FundCashRefCombo> L_FundCashRef = new List<FundCashRefCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        if (_fundPK != 0 )
                        {
                            cmd.CommandText = @" SELECT  A.FundCashRefPK,A.Name + ' - ' + A.BankAccountNo as ID, A.Name Name,A.Remark Remark FROM [FundCashRef] A 
                            left join FundClientCashRef B on A.FundCashRefPK = B.FundCashRefPK and A.FundPK = B.FundPK and B.status = 2 
                            where A.status = 2 and Type  = 4 and A.FundPK  = 0 and BankBranchPK = @BankBranchPK

                            union all

                            SELECT  A.FundCashRefPK,A.Name + ' - ' + A.BankAccountNo as ID, A.Name Name,A.Remark Remark FROM [FundCashRef] A 
                            left join FundClientCashRef B on A.FundCashRefPK = B.FundCashRefPK and A.FundPK = B.FundPK and B.status = 2 
                            where A.status = 2 and Type  = 4 and A.BankBranchPK  = @BankBranchPK and A.FundPK  = @FundPK    ";
                            cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                        }

                        else
                        {
                            cmd.CommandText = @" SELECT  A.FundCashRefPK,A.Name + ' - ' + A.BankAccountNo as ID, A.Name Name,A.Remark Remark FROM [FundCashRef] A 
                            left join FundClientCashRef B on A.FundCashRefPK = B.FundCashRefPK and A.FundPK = B.FundPK and B.status = 2 
                            where A.status = 2 and Type  = 4  and A.BankBranchPK  = @BankBranchPK";
                        }


            
                        cmd.Parameters.AddWithValue("@BankBranchPK", _bankBranchPK);


                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    FundCashRefCombo M_FundCashRef = new FundCashRefCombo();
                                    M_FundCashRef.FundCashRefPK = Convert.ToInt32(dr["FundCashRefPK"]);
                                    M_FundCashRef.ID = Convert.ToString(dr["ID"]);
                                    M_FundCashRef.Name = Convert.ToString(dr["Name"]);
                                    M_FundCashRef.Remark = Convert.ToString(dr["Remark"]);
                                    L_FundCashRef.Add(M_FundCashRef);
                                }

                            }
                            return L_FundCashRef;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public List<FundCashRefCombo> FundCashRef_ComboByBankBranchPKFromInvestment(int _fundPK, int _bankBranchPK)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<FundCashRefCombo> L_FundCashRef = new List<FundCashRefCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        if (_fundPK != 0)
                        {
                            cmd.CommandText = @" SELECT  A.FundCashRefPK,A.Name + ' - ' + A.BankAccountNo as ID, A.Name Name,A.Remark Remark FROM [FundCashRef] A 
                            left join BankBranch B on A.BankBranchPK = B.BankBranchPK and B.status = 2 
                            where A.status = 2 and A.Type  = 4 and A.FundPK  = 0 and A.BankBranchPK = @BankBranchPK

                            union all

                            SELECT  A.FundCashRefPK,A.Name + ' - ' + A.BankAccountNo as ID, A.Name Name,A.Remark Remark FROM [FundCashRef] A 
                            left join BankBranch B on A.BankBranchPK = B.BankBranchPK and B.status = 2 
                            where A.status = 2 and A.Type  = 4 and A.BankBranchPK  = @BankBranchPK and A.FundPK  = @FundPK    ";
                            cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                        }

                        else
                        {
                            cmd.CommandText = @" SELECT  A.FundCashRefPK,A.Name + ' - ' + A.BankAccountNo as ID, A.Name Name,A.Remark Remark FROM [FundCashRef] A 
                            left join BankBranch B on A.BankBranchPK = B.BankBranchPK and B.status = 2 
                            where A.status = 2 and A.Type  = 4  and A.BankBranchPK  = @BankBranchPK";
                        }



                        cmd.Parameters.AddWithValue("@BankBranchPK", _bankBranchPK);


                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    FundCashRefCombo M_FundCashRef = new FundCashRefCombo();
                                    M_FundCashRef.FundCashRefPK = Convert.ToInt32(dr["FundCashRefPK"]);
                                    M_FundCashRef.ID = Convert.ToString(dr["ID"]);
                                    M_FundCashRef.Name = Convert.ToString(dr["Name"]);
                                    M_FundCashRef.Remark = Convert.ToString(dr["Remark"]);
                                    L_FundCashRef.Add(M_FundCashRef);
                                }

                            }
                            return L_FundCashRef;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }


        public List<FundCashRefCombo> FundCashRef_ComboByFundCashRef(int _fundPK)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<FundCashRefCombo> L_FundCashRef = new List<FundCashRefCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "SELECT  FundCashRefPK,ID + ' - ' + Name as ID, Name, FundJournalAccountPK FROM [FundCashRef]  where status = 2 and FundPK = @FundPK order by ID,Name";
                        cmd.Parameters.AddWithValue("@FundPK", _fundPK);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    FundCashRefCombo M_FundCashRef = new FundCashRefCombo();
                                    M_FundCashRef.FundCashRefPK = Convert.ToInt32(dr["FundCashRefPK"]);
                                    M_FundCashRef.ID = Convert.ToString(dr["ID"]);
                                    M_FundCashRef.Name = Convert.ToString(dr["Name"]);
                                    M_FundCashRef.FundJournalAccountPK = Convert.ToInt32(dr["FundJournalAccountPK"]);
                                    L_FundCashRef.Add(M_FundCashRef);
                                }

                            }
                            return L_FundCashRef;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }



        public List<FundCashRefCombo> FundCashRef_ComboDefaultForInvestment()
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<FundCashRefCombo> L_FundCashRef = new List<FundCashRefCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "SELECT  FundCashRefPK,ID + ' - ' + Name as ID, Name, FundJournalAccountPK FROM [FundCashRef]  where status = 2 and bitdefaultinvestment=1 order by ID,Name";

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    FundCashRefCombo M_FundCashRef = new FundCashRefCombo();
                                    M_FundCashRef.FundCashRefPK = Convert.ToInt32(dr["FundCashRefPK"]);
                                    M_FundCashRef.ID = Convert.ToString(dr["ID"]);
                                    M_FundCashRef.Name = Convert.ToString(dr["Name"]);
                                    M_FundCashRef.FundJournalAccountPK = Convert.ToInt32(dr["FundJournalAccountPK"]);
                                    L_FundCashRef.Add(M_FundCashRef);
                                }

                            }
                            return L_FundCashRef;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public bool FundCashRef_CheckBitDefaultInvestment(FundCashRef _fundCashRef)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {


                        cmd.CommandText = @"
                        if (@DefaultInvestment = 1) AND EXISTS(select * from FundCashRef where status = 2 and FundPK = @FundPK and bitdefaultinvestment = 1)
                        BEGIN
                            Select 1 Result    
                        END
                        ELSE
                        BEGIN
                            Select 0 Result
                        END
                        ";
                        cmd.Parameters.AddWithValue("@FundPK", _fundCashRef.FundPK);
                        cmd.Parameters.AddWithValue("@DefaultInvestment", _fundCashRef.bitdefaultinvestment);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    return Convert.ToBoolean(dr["Result"]);
                                }

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


    }
}