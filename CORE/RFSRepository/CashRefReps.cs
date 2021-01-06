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
    public class CashRefReps
    {
        Host _host = new Host();

        //1
        string _insertCommand = "INSERT INTO [dbo].[CashRef] " +
                            "([CashRefPK],[HistoryPK],[Status],[ID],[Name],[AccountPK],[CurrencyPK],[BankBranchPK],[BankAccountNo],[FundOperationBank],[CashRefType],";
        string _paramaterCommand = "@ID,@Name,@AccountPK,@CurrencyPK,@BankBranchPK,@BankAccountNo,@FundOperationBank,@CashRefType,";

        //2
        private CashRef setCashRef(SqlDataReader dr)
        {
            CashRef M_CashRef = new CashRef();
            M_CashRef.CashRefPK = Convert.ToInt32(dr["CashRefPK"]);
            M_CashRef.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_CashRef.Status = Convert.ToInt32(dr["Status"]);
            M_CashRef.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_CashRef.Notes = Convert.ToString(dr["Notes"]);
            M_CashRef.ID = dr["ID"].ToString();
            M_CashRef.Name = dr["Name"].ToString();
            M_CashRef.AccountPK = Convert.ToInt32(dr["AccountPK"]);
            M_CashRef.AccountID = Convert.ToString(dr["AccountID"]);
            M_CashRef.AccountName = Convert.ToString(dr["AccountName"]);
            M_CashRef.CurrencyPK = Convert.ToInt32(dr["CurrencyPK"]);
            M_CashRef.CurrencyID = Convert.ToString(dr["CurrencyID"]);
            M_CashRef.BankBranchPK = Convert.ToInt32(dr["BankBranchPK"]);
            M_CashRef.BankBranchID = Convert.ToString(dr["BankBranchID"]);
            M_CashRef.BankBranchName = Convert.ToString(dr["BankBranchName"]);
            M_CashRef.CashRefType = Convert.ToInt32(dr["CashRefType"]);
            M_CashRef.CashRefTypeDesc = Convert.ToString(dr["CashRefTypeDesc"]);
            M_CashRef.BankAccountNo = dr["BankAccountNo"].ToString();
            M_CashRef.FundOperationBank = Convert.ToBoolean(dr["FundOperationBank"]);
            M_CashRef.EntryUsersID = dr["EntryUsersID"].ToString();
            M_CashRef.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_CashRef.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_CashRef.VoidUsersID = dr["VoidUsersID"].ToString();
            M_CashRef.EntryTime = dr["EntryTime"].ToString();
            M_CashRef.UpdateTime = dr["UpdateTime"].ToString();
            M_CashRef.ApprovedTime = dr["ApprovedTime"].ToString();
            M_CashRef.VoidTime = dr["VoidTime"].ToString();
            M_CashRef.DBUserID = dr["DBUserID"].ToString();
            M_CashRef.DBTerminalID = dr["DBTerminalID"].ToString();
            M_CashRef.LastUpdate = dr["LastUpdate"].ToString();
            M_CashRef.LastUpdateDB = Convert.ToString(dr["LastUpdateDB"]);
            return M_CashRef;
        }

        public List<CashRef> CashRef_Select(int _status)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<CashRef> L_CashRef = new List<CashRef>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = @"Select case when CR.status=1 then 'PENDING' else Case When CR.status = 2 then 'APPROVED' else Case when CR.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,
                            isnull(A.ID,'') AccountID,isnull(A.Name,'') AccountName, isnull(C.ID,'') CurrencyID, isnull(BC1.ID,'') BankBranchID,isnull(BC1.ID,'') BankBranchName, 
                            isnull(CR.CashRefPK,0) CashRefPK, isnull(CR.HistoryPK,0) HistoryPK, isnull(CR.Status,0) Status, isnull(CR.Notes,0) Notes,
                            isnull(CR.ID,0) ID, isnull(CR.Name,0) Name, isnull(CR.AccountPK,0) AccountPK, isnull(CR.CurrencyPK,0) CurrencyPK,
                            isnull(CR.BankBranchPK,0) BankBranchPK, isnull(CR.BankAccountNo,'') BankAccountNo, isnull(CR.BankAccountName,'') BankAccountName, 
                            isnull(CR.BankAccountBranchName,'') BankAccountBranchName, isnull(CR.FundOperationBank,0) FundOperationBank, isnull(CR.EntryUsersID,'') EntryUsersID,
                            isnull(CR.EntryTime,0) EntryTime, isnull(CR.UpdateUsersID,'') UpdateUsersID, isnull(CR.UpdateTime,0) UpdateTime, isnull(CR.ApprovedUsersID,'') ApprovedUsersID,
                            isnull(CR.ApprovedTime,0) ApprovedTime, isnull(CR.VoidUsersID,'') VoidUsersID, isnull(CR.VoidTime,0) VoidTime,	isnull(CR.DBUserID,'') DBUserID,	
                            isnull(CR.DBTerminalID,'') DBTerminalID,	isnull(CR.LastUpdate,0) LastUpdate,	isnull(CR.LastUpdateDB,0) LastUpdateDB,isnull(CR.CashRefType,'') CashRefType, 
							case when CR.CashRefType = 1 then 'Petty Cash' when CR.CashRefType = 2 then  'Bank'  else 'Deposito on Call' end	CashRefTypeDesc 				 
                            from CashRef CR left join 
                            Account A on CR.AccountPK = A.AccountPK and A.status =2 left join 
                            Currency C on CR.CurrencyPK = C.CurrencyPK and C.status = 2 left join 
                            BankBranch BC1 on CR.BankBranchPK = BC1.BankBranchPK and BC1.status = 2 
							
                            where CR.status = @status and A.status = 2  ";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = @"Select case when CR.status=1 then 'PENDING' else Case When CR.status = 2 then 'APPROVED' else Case when CR.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,
                            isnull(A.ID,'') AccountID,isnull(A.Name,'') AccountName, isnull(C.ID,'') CurrencyID, isnull(BC1.ID,'') BankBranchID,isnull(BC1.ID,'') BankBranchName, 
                            isnull(CR.CashRefPK,0) CashRefPK, isnull(CR.HistoryPK,0) HistoryPK, isnull(CR.Status,0) Status, isnull(CR.Notes,0) Notes,
                            isnull(CR.ID,0) ID, isnull(CR.Name,0) Name, isnull(CR.AccountPK,0) AccountPK, isnull(CR.CurrencyPK,0) CurrencyPK,
                            isnull(CR.BankBranchPK,0) BankBranchPK, isnull(CR.BankAccountNo,'') BankAccountNo, isnull(CR.BankAccountName,'') BankAccountName, 
                            isnull(CR.BankAccountBranchName,'') BankAccountBranchName, isnull(CR.FundOperationBank,0) FundOperationBank, isnull(CR.EntryUsersID,'') EntryUsersID,
                            isnull(CR.EntryTime,0) EntryTime, isnull(CR.UpdateUsersID,'') UpdateUsersID, isnull(CR.UpdateTime,0) UpdateTime, isnull(CR.ApprovedUsersID,'') ApprovedUsersID,
                            isnull(CR.ApprovedTime,0) ApprovedTime, isnull(CR.VoidUsersID,'') VoidUsersID, isnull(CR.VoidTime,0) VoidTime,	isnull(CR.DBUserID,'') DBUserID,	
                            isnull(CR.DBTerminalID,'') DBTerminalID,	isnull(CR.LastUpdate,0) LastUpdate,	isnull(CR.LastUpdateDB,0) LastUpdateDB,isnull(CR.CashRefType,'') CashRefType, 
		                    case when CR.CashRefType = 1 then 'Petty Cash' when CR.CashRefType = 2 then  'Bank'  else 'Deposito on Call' end	CashRefTypeDesc				 
                            from CashRef CR left join 
                            Account A on CR.AccountPK = A.AccountPK and A.status =2 left join 
                            Currency C on CR.CurrencyPK = C.CurrencyPK and C.status = 2 left join 
                            BankBranch BC1 on CR.BankBranchPK = BC1.BankBranchPK and BC1.status = 2 
							 ";
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_CashRef.Add(setCashRef(dr));
                                }
                            }
                            return L_CashRef;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int CashRef_Add(CashRef _cashRef, bool _havePrivillege)
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
                                 "Select isnull(max(CashRefPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@lastupdate from CashRef";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _cashRef.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);

                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],lastupdate)" +
                                "Select isnull(max(CashRefPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@lastupdate from CashRef";
                        }
                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@ID", _cashRef.ID);
                        cmd.Parameters.AddWithValue("@Name", _cashRef.Name);
                        cmd.Parameters.AddWithValue("@AccountPK", _cashRef.AccountPK);
                        cmd.Parameters.AddWithValue("@CurrencyPK", _cashRef.CurrencyPK);
                        cmd.Parameters.AddWithValue("@BankBranchPK", _cashRef.BankBranchPK);
                        cmd.Parameters.AddWithValue("@CashRefType", _cashRef.CashRefType);
                        cmd.Parameters.AddWithValue("@BankAccountNo", _cashRef.BankAccountNo);
                        cmd.Parameters.AddWithValue("@FundOperationBank", _cashRef.FundOperationBank);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _cashRef.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();

                        return _host.Get_LastPKByLastUpate(_datetimeNow, "CashRef");
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public int CashRef_Update(CashRef _cashRef, bool _havePrivillege)
        {
            try
            {
                int _newHisPK;
                DateTime _datetimeNow = DateTime.Now;
                int status = _host.Get_Status(_cashRef.CashRefPK, _cashRef.HistoryPK, "cashRef");
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update CashRef set status=2, Notes=@Notes,ID=@ID,Name=@Name,AccountPK=@AccountPK,CurrencyPK=@CurrencyPK,BankBranchPK=@BankBranchPK,BankAccountNo=@BankAccountNo,CashRefType=@CashRefType," +
                                    " FundOperationBank=@FundOperationBank," +
                                    "ApprovedUsersID=@ApprovedUsersID,ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,lastupdate=@lastupdate " +
                                    "where CashRefPK = @PK and historyPK = @HistoryPK";
                            cmd.Parameters.AddWithValue("@HistoryPK", _cashRef.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _cashRef.CashRefPK);
                            cmd.Parameters.AddWithValue("@ID", _cashRef.ID);
                            cmd.Parameters.AddWithValue("@Notes", _cashRef.Notes);
                            cmd.Parameters.AddWithValue("@Name", _cashRef.Name);
                            cmd.Parameters.AddWithValue("@AccountPK", _cashRef.AccountPK);
                            cmd.Parameters.AddWithValue("@CurrencyPK", _cashRef.CurrencyPK);
                            cmd.Parameters.AddWithValue("@BankBranchPK", _cashRef.BankBranchPK);
                            cmd.Parameters.AddWithValue("@BankAccountNo", _cashRef.BankAccountNo);
                            cmd.Parameters.AddWithValue("@CashRefType", _cashRef.CashRefType);
                            cmd.Parameters.AddWithValue("@FundOperationBank", _cashRef.FundOperationBank);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _cashRef.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _cashRef.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update CashRef set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,lastupdate=@lastupdate where CashRefPK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _cashRef.CashRefPK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _cashRef.EntryUsersID);
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
                                cmd.CommandText = "Update CashRef set Notes=@Notes,ID=@ID,Name=@Name,AccountPK=@AccountPK,CurrencyPK=@CurrencyPK,BankBranchPK=@BankBranchPK,BankAccountNo=@BankAccountNo,CashRefType=@CashRefType," +
                                    "FundOperationBank=@FundOperationBank," +
                                    "UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,lastupdate=@lastupdate " +
                                    "where CashRefPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _cashRef.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _cashRef.CashRefPK);
                                cmd.Parameters.AddWithValue("@ID", _cashRef.ID);
                                cmd.Parameters.AddWithValue("@Notes", _cashRef.Notes);
                                cmd.Parameters.AddWithValue("@Name", _cashRef.Name);
                                cmd.Parameters.AddWithValue("@AccountPK", _cashRef.AccountPK);
                                cmd.Parameters.AddWithValue("@CurrencyPK", _cashRef.CurrencyPK);
                                cmd.Parameters.AddWithValue("@BankBranchPK", _cashRef.BankBranchPK);
                                cmd.Parameters.AddWithValue("@BankAccountNo", _cashRef.BankAccountNo);
                                cmd.Parameters.AddWithValue("@CashRefType", _cashRef.CashRefType);
                                cmd.Parameters.AddWithValue("@FundOperationBank", _cashRef.FundOperationBank);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _cashRef.EntryUsersID);
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
                                _newHisPK = _host.Get_NewHistoryPK(_cashRef.CashRefPK, "CashRef");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From CashRef where CashRefPK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _cashRef.CashRefPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _cashRef.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@ID", _cashRef.ID);
                                cmd.Parameters.AddWithValue("@Name", _cashRef.Name);
                                cmd.Parameters.AddWithValue("@AccountPK", _cashRef.AccountPK);
                                cmd.Parameters.AddWithValue("@CurrencyPK", _cashRef.CurrencyPK);
                                cmd.Parameters.AddWithValue("@BankBranchPK", _cashRef.BankBranchPK);
                                cmd.Parameters.AddWithValue("@BankAccountNo", _cashRef.BankAccountNo);
                                cmd.Parameters.AddWithValue("@CashRefType", _cashRef.CashRefType);
                                cmd.Parameters.AddWithValue("@FundOperationBank", _cashRef.FundOperationBank);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _cashRef.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update CashRef set status= 4,Notes=@Notes, " +
                                    "lastupdate=@lastupdate where CashRefPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _cashRef.Notes);
                                cmd.Parameters.AddWithValue("@PK", _cashRef.CashRefPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _cashRef.HistoryPK);
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

        public void CashRef_Approved(CashRef _cashRef)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update CashRef set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,lastupdate=@lastupdate " +
                            "where CashRefPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _cashRef.CashRefPK);
                        cmd.Parameters.AddWithValue("@historyPK", _cashRef.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _cashRef.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update CashRef set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,lastupdate=@lastupdate where CashRefPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _cashRef.CashRefPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _cashRef.ApprovedUsersID);
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

        public void CashRef_Reject(CashRef _cashRef)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update CashRef set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,lastupdate=@lastupdate " +
                            "where CashRefPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _cashRef.CashRefPK);
                        cmd.Parameters.AddWithValue("@historyPK", _cashRef.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _cashRef.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update CashRef set status= 2,lastupdate=@lastupdate where CashRefPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _cashRef.CashRefPK);
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

        public void CashRef_Void(CashRef _cashRef)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update CashRef set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,lastupdate=@lastupdate " +
                            "where CashRefPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _cashRef.CashRefPK);
                        cmd.Parameters.AddWithValue("@historyPK", _cashRef.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _cashRef.VoidUsersID);
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

        public List<CashRefCombo> CashRef_ComboRpt()
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<CashRefCombo> L_CashRef = new List<CashRefCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "SELECT  CashRefPK,ID + ' - ' + Name as ID, Name FROM [CashRef]  where status = 2 union all select 0,'All', '' order by CashRefPK";
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    CashRefCombo M_CashRef = new CashRefCombo();
                                    M_CashRef.CashRefPK = Convert.ToInt32(dr["CashRefPK"]);
                                    M_CashRef.ID = Convert.ToString(dr["ID"]);
                                    M_CashRef.Name = Convert.ToString(dr["Name"]);
                                    L_CashRef.Add(M_CashRef);
                                }

                            }
                            return L_CashRef;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public List<CashRefCombo> CashRef_Combo()
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<CashRefCombo> L_CashRef = new List<CashRefCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "SELECT  CashRefPK,ID + ' - ' + Name as ID, Name, AccountPK FROM [CashRef]  where status = 2 order by ID,Name";
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    CashRefCombo M_CashRef = new CashRefCombo();
                                    M_CashRef.CashRefPK = Convert.ToInt32(dr["CashRefPK"]);
                                    M_CashRef.ID = Convert.ToString(dr["ID"]);
                                    M_CashRef.Name = Convert.ToString(dr["Name"]);
                                    M_CashRef.AccountPK = Convert.ToInt32(dr["AccountPK"]);
                                    L_CashRef.Add(M_CashRef);
                                }

                            }
                            return L_CashRef;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public CashRef CashRef_SelectByPK(int _cashRefPK)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {


                        cmd.CommandText = @"Select case when CR.status=1 then 'PENDING' else Case When CR.status = 2 then 'APPROVED' else Case when CR.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,A.ID AccountID,A.Name AccountName, C.ID CurrencyID, BC1.ID BankBranchID,'' BankBranchName,
                        case when CR.CashRefType = 1 then 'Petty Cash' else 'Bank' end CashRefTypeDesc,CR.* from CashRef CR left join 
                        Account A on CR.AccountPK = A.AccountPK and A.status =2 left join 
                        Currency C on CR.CurrencyPK = C.CurrencyPK and C.status = 2 left join 
                        BankBranch BC1 on CR.BankBranchPK = BC1.BankBranchPK and BC1.status = 2 
                        Where CR.CashRefPK= @CashRefPK and CR.status = 2 ";
                        cmd.Parameters.AddWithValue("@CashRefPK", _cashRefPK);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return setCashRef(dr);
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

        public List<CashRefCombo> CashRef_ComboByBankBranchPK(int _bankBranchPK)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<CashRefCombo> L_CashRef = new List<CashRefCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_bankBranchPK == 0)
                        {
                            cmd.CommandText = @" 
                            SELECT  CashRefPK,Name + ' - ' + isnull(BankAccountNo,'') as ID, Name Name FROM [CashRef] 
                            where status = 2 ";
                        }
                        else
                        {
                            cmd.CommandText = @" 
                            SELECT  CashRefPK,Name + ' - ' + isnull(BankAccountNo,'') as ID, Name Name FROM [CashRef] 
                            where status = 2 and BankBranchPK = @BankBranchPK";

                            cmd.Parameters.AddWithValue("@BankBranchPK", _bankBranchPK);
                        }

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    CashRefCombo M_CashRef = new CashRefCombo();
                                    M_CashRef.CashRefPK = Convert.ToInt32(dr["CashRefPK"]);
                                    M_CashRef.ID = Convert.ToString(dr["ID"]);
                                    M_CashRef.Name = Convert.ToString(dr["Name"]);
                                    L_CashRef.Add(M_CashRef);
                                }

                            }
                            return L_CashRef;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public string CashRef_GetSinvestReference(DateTime _date, string _Type)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {


                        cmd.CommandText = @"
                        --declare @date date
                        --declare @Type nvarchar(20)

                        --set @Type = 'TD'
                        --set @date = '2020-04-14'

                        --Type
                        --SUB
                        --RED
                        --SWI
                        --BOND
                        --EQ
                        --TD

                        declare @Reference nvarchar(100)
                        declare @No int
                        declare @MaxPK int

                        select @MaxPK = max(ReferenceForNikkoPK) from ReferenceForNikko

                        set @MaxPK = isnull(@MaxPK,0) + 1

                        select @No = MAX(no) from ReferenceForNikko where month = month(@date) and year = year(@date) and Type = @Type
						
	                    set @No = isnull(@No,0) + 1

                        if exists (select * from ReferenceForNikko where month = month(@date) and year = year(@date) and Type = @Type)
                        begin
	                        update ReferenceForNikko set No = @No where month = month(@date) and year = year(@date) and Type = @Type
                        end
                        else 
                        begin
	                        insert into ReferenceForNikko
	                        select @MaxPK,@No,@Type,month(@date),year(@Date)
                        end


                        set @Reference = cast(@No as nvarchar) + '/' + @Type + '/NSI/FM/' + cast(SUBSTRING(CONVERT(nvarchar(6),@Date, 112),5,2) as nvarchar) + '/' + substring(cast(year(@date) as nvarchar),3,2)

                        select @Reference Reference

";
                        cmd.Parameters.AddWithValue("@Date", _date);
                        cmd.Parameters.AddWithValue("@Type", _Type);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return dr["Reference"].ToString();
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