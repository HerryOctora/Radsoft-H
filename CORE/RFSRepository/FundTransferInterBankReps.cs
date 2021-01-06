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
    public class FundTransferInterBankReps
    {
        Host _host = new Host();

        //1
        string _insertCommand = "INSERT INTO [dbo].[FundTransferInterBank] " +
                            "([FundTransferInterBankPK],[HistoryPK],[Status],[Date],[FundPK],[FundCashRefPKFrom],[FundCashRefPKTo],[InstructionNo],[InformationTo],[CCTo],[Subject],[Signature1],[Signature2],";
        string _paramaterCommand = "@Date,@FundPK,@FundCashRefPKFrom,@FundCashRefPKTo,@InstructionNo,@InformationTo,@CCTo,@Subject,@Signature1,@Signature2,";

        //2
        private FundTransferInterBank setFundTransferInterBank(SqlDataReader dr)
        {
            FundTransferInterBank M_FundTransferInterBank = new FundTransferInterBank();
            M_FundTransferInterBank.FundTransferInterBankPK = Convert.ToInt32(dr["FundTransferInterBankPK"]);
            M_FundTransferInterBank.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_FundTransferInterBank.Status = Convert.ToInt32(dr["Status"]);
            M_FundTransferInterBank.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_FundTransferInterBank.Notes = Convert.ToString(dr["Notes"]);
            M_FundTransferInterBank.Date = dr["Date"].ToString();
            M_FundTransferInterBank.FundPK = Convert.ToInt32(dr["FundPK"]);
            M_FundTransferInterBank.FundID = Convert.ToString(dr["FundID"]);
            M_FundTransferInterBank.FundCashRefPKFrom = Convert.ToInt32(dr["FundCashRefPKFrom"]);
            M_FundTransferInterBank.FundCashRefIDFrom = Convert.ToString(dr["FundCashRefIDFrom"]);
            M_FundTransferInterBank.FundCashRefPKTo = Convert.ToInt32(dr["FundCashRefPKTo"]);
            M_FundTransferInterBank.FundCashRefIDTo = Convert.ToString(dr["FundCashRefIDTo"]);
            M_FundTransferInterBank.InstructionNo = Convert.ToString(dr["InstructionNo"]);
            M_FundTransferInterBank.InformationTo = Convert.ToString(dr["InformationTo"]);
            M_FundTransferInterBank.CCTo = Convert.ToString(dr["CCTo"]);
            M_FundTransferInterBank.Subject = Convert.ToString(dr["Subject"]);
            M_FundTransferInterBank.Signature1 = Convert.ToInt32(dr["Signature1"]);
            M_FundTransferInterBank.SignatureName1 = Convert.ToString(dr["SignatureName1"]);
            M_FundTransferInterBank.Signature2 = Convert.ToInt32(dr["Signature2"]);
            M_FundTransferInterBank.SignatureName2 = Convert.ToString(dr["SignatureName2"]);
            M_FundTransferInterBank.EntryUsersID = dr["EntryUsersID"].ToString();
            M_FundTransferInterBank.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_FundTransferInterBank.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_FundTransferInterBank.VoidUsersID = dr["VoidUsersID"].ToString();
            M_FundTransferInterBank.EntryTime = dr["EntryTime"].ToString();
            M_FundTransferInterBank.UpdateTime = dr["UpdateTime"].ToString();
            M_FundTransferInterBank.ApprovedTime = dr["ApprovedTime"].ToString();
            M_FundTransferInterBank.VoidTime = dr["VoidTime"].ToString();
            M_FundTransferInterBank.DBUserID = dr["DBUserID"].ToString();
            M_FundTransferInterBank.DBTerminalID = dr["DBTerminalID"].ToString();
            M_FundTransferInterBank.LastUpdate = dr["LastUpdate"].ToString();
            M_FundTransferInterBank.LastUpdateDB = Convert.ToString(dr["LastUpdateDB"]);
            return M_FundTransferInterBank;
        }

        public List<FundTransferInterBank> FundTransferInterBank_Select(int _status)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<FundTransferInterBank> L_FundTransferInterBank = new List<FundTransferInterBank>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {


                        if (_status != 9)
                        {
                            cmd.CommandText = @"Select case when B.status=1 then 'PENDING' else Case When B.status = 2 then 'APPROVED' else Case when B.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,
                            B.ID FundID,C.ID FundCashRefIDFrom,C1.ID FundCashRefIDTo,D1.Name SignatureName1,D2.Name SignatureName2,* from FundTransferInterBank  A left join
                            Fund B on A.FundPK = B.FundPK and B.Status in(1,2) left join
                            FundCashRef C on A.FundCashRefPKFrom = C.FundCashRefPK and C.Status in (1,2) left join
                            FundCashRef C1 on A.FundCashRefPKTo = C1.FundCashRefPK and C1.Status in (1,2) left join
                            Signature D1 on A.Signature1 = D1.SignaturePK and D1.Status in (1,2) left join
                            Signature D2 on A.Signature2 = D2.SignaturePK and D2.Status in (1,2) where a.status = @status order by FundTransferInterBankPK";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = @"Select case when A.status=1 then 'PENDING' else Case When A.status = 2 then 'APPROVED' else Case when A.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,
                            B.ID FundID,C.ID FundCashRefIDFrom,C1.ID FundCashRefIDTo,D1.Name SignatureName1,D2.Name SignatureName2,* from FundTransferInterBank  A left join
                            Fund B on A.FundPK = B.FundPK and B.Status in(1,2) left join
                            FundCashRef C on A.FundCashRefPKFrom = C.FundCashRefPK and C.Status in (1,2) left join
                            FundCashRef C1 on A.FundCashRefPKTo = C1.FundCashRefPK and C1.Status in (1,2) left join
                            Signature D1 on A.Signature1 = D1.SignaturePK and D1.Status in (1,2) left join
                            Signature D2 on A.Signature2 = D2.SignaturePK and D2.Status in (1,2)  order by FundTransferInterBankPK";
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_FundTransferInterBank.Add(setFundTransferInterBank(dr));
                                }
                            }
                            return L_FundTransferInterBank;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int FundTransferInterBank_Add(FundTransferInterBank _FundTransferInterBank, bool _havePrivillege)
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
                                 "Select isnull(max(FundTransferInterBankPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@LastUpdate From FundTransferInterBank";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _FundTransferInterBank.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);
                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[LastUpdate])" +
                                "Select isnull(max(FundTransferInterBankPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@LastUpdate From FundTransferInterBank";
                        }
                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@Date", _FundTransferInterBank.Date);
                        cmd.Parameters.AddWithValue("@FundPK", _FundTransferInterBank.FundPK);
                        cmd.Parameters.AddWithValue("@FundCashRefPKFrom", _FundTransferInterBank.FundCashRefPKFrom);
                        cmd.Parameters.AddWithValue("@FundCashRefPKTo", _FundTransferInterBank.FundCashRefPKTo);
                        cmd.Parameters.AddWithValue("@InstructionNo", _FundTransferInterBank.InstructionNo);
                        cmd.Parameters.AddWithValue("@InformationTo", _FundTransferInterBank.InformationTo);
                        cmd.Parameters.AddWithValue("@CCTo", _FundTransferInterBank.CCTo);
                        cmd.Parameters.AddWithValue("@Subject", _FundTransferInterBank.Subject);
                        cmd.Parameters.AddWithValue("@Signature1", _FundTransferInterBank.Signature1);
                        cmd.Parameters.AddWithValue("@Signature2", _FundTransferInterBank.Signature2);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _FundTransferInterBank.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);

                        cmd.ExecuteNonQuery();


                        return _host.Get_LastPKByLastUpate(_dateTimeNow, "FundTransferInterBank");
                    }


                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int FundTransferInterBank_Update(FundTransferInterBank _FundTransferInterBank, bool _havePrivillege)
        {
            try
            {
                int _newHisPK;
                int status = _host.Get_Status(_FundTransferInterBank.FundTransferInterBankPK, _FundTransferInterBank.HistoryPK, "FundTransferInterBank");
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update FundTransferInterBank set status=2, Notes=@Notes,Date=@Date,FundPK=@FundPK,FundCashRefPKFrom=@FundCashRefPKFrom,FundCashRefPKTo=@FundCashRefPKTo,InstructionNo=@InstructionNo,InformationTo=@InformationTo,CCTo=@CCTo,Subject=@Subject,Signature1=@Signature1,Signature2=@Signature2," +
                                "ApprovedUsersID=@ApprovedUsersID, " +
                                "ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@LastUpdate " +
                                "where FundTransferInterBankPK = @PK and historyPK = @HistoryPK";
                            cmd.Parameters.AddWithValue("@HistoryPK", _FundTransferInterBank.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _FundTransferInterBank.FundTransferInterBankPK);
                            cmd.Parameters.AddWithValue("@Notes", _FundTransferInterBank.Notes);
                            cmd.Parameters.AddWithValue("@Date", _FundTransferInterBank.Date);
                            cmd.Parameters.AddWithValue("@FundPK", _FundTransferInterBank.FundPK);
                            cmd.Parameters.AddWithValue("@FundCashRefPKFrom", _FundTransferInterBank.FundCashRefPKFrom);
                            cmd.Parameters.AddWithValue("@FundCashRefPKTo", _FundTransferInterBank.FundCashRefPKTo);
                            cmd.Parameters.AddWithValue("@InstructionNo", _FundTransferInterBank.InstructionNo);
                            cmd.Parameters.AddWithValue("@InformationTo", _FundTransferInterBank.InformationTo);
                            cmd.Parameters.AddWithValue("@CCTo", _FundTransferInterBank.CCTo);
                            cmd.Parameters.AddWithValue("@Subject", _FundTransferInterBank.Subject);
                            cmd.Parameters.AddWithValue("@Signature1", _FundTransferInterBank.Signature1);
                            cmd.Parameters.AddWithValue("@Signature2", _FundTransferInterBank.Signature2);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _FundTransferInterBank.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _dateTimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _FundTransferInterBank.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);
                            cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update FundTransferInterBank set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where FundTransferInterBankPK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _FundTransferInterBank.FundTransferInterBankPK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _FundTransferInterBank.EntryUsersID);
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
                                cmd.CommandText = "Update FundTransferInterBank set Notes=@Notes,Date=@Date,FundPK=@FundPK,FundCashRefPKFrom=@FundCashRefPKFrom,FundCashRefPKTo=@FundCashRefPKTo,InstructionNo=@InstructionNo,InformationTo=@InformationTo,CCTo=@CCTo,Subject=@Subject,Signature1=@Signature1,Signature2=@Signature2," +
                                    "UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@LastUpdate " +
                                    "where FundTransferInterBankPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _FundTransferInterBank.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _FundTransferInterBank.FundTransferInterBankPK);
                                cmd.Parameters.AddWithValue("@Notes", _FundTransferInterBank.Notes);
                                cmd.Parameters.AddWithValue("@Date", _FundTransferInterBank.Date);
                                cmd.Parameters.AddWithValue("@FundPK", _FundTransferInterBank.FundPK);
                                cmd.Parameters.AddWithValue("@FundCashRefPKFrom", _FundTransferInterBank.FundCashRefPKFrom);
                                cmd.Parameters.AddWithValue("@FundCashRefPKTo", _FundTransferInterBank.FundCashRefPKTo);
                                cmd.Parameters.AddWithValue("@InstructionNo", _FundTransferInterBank.InstructionNo);
                                cmd.Parameters.AddWithValue("@InformationTo", _FundTransferInterBank.InformationTo);
                                cmd.Parameters.AddWithValue("@CCTo", _FundTransferInterBank.CCTo);
                                cmd.Parameters.AddWithValue("@Subject", _FundTransferInterBank.Subject);
                                cmd.Parameters.AddWithValue("@Signature1", _FundTransferInterBank.Signature1);
                                cmd.Parameters.AddWithValue("@Signature2", _FundTransferInterBank.Signature2);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _FundTransferInterBank.EntryUsersID);
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
                                _newHisPK = _host.Get_NewHistoryPK(_FundTransferInterBank.FundTransferInterBankPK, "FundTransferInterBank");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From FundTransferInterBank where FundTransferInterBankPK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _FundTransferInterBank.FundTransferInterBankPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _FundTransferInterBank.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@Date", _FundTransferInterBank.Date);
                                cmd.Parameters.AddWithValue("@FundPK", _FundTransferInterBank.FundPK);
                                cmd.Parameters.AddWithValue("@FundCashRefPKFrom", _FundTransferInterBank.FundCashRefPKFrom);
                                cmd.Parameters.AddWithValue("@FundCashRefPKTo", _FundTransferInterBank.FundCashRefPKTo);
                                cmd.Parameters.AddWithValue("@InstructionNo", _FundTransferInterBank.InstructionNo);
                                cmd.Parameters.AddWithValue("@InformationTo", _FundTransferInterBank.InformationTo);
                                cmd.Parameters.AddWithValue("@CCTo", _FundTransferInterBank.CCTo);
                                cmd.Parameters.AddWithValue("@Subject", _FundTransferInterBank.Subject);
                                cmd.Parameters.AddWithValue("@Signature1", _FundTransferInterBank.Signature1);
                                cmd.Parameters.AddWithValue("@Signature2", _FundTransferInterBank.Signature2);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _FundTransferInterBank.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _dateTimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update FundTransferInterBank set status= 4,Notes=@Notes," +
                                    "LastUpdate=LastUpdate where FundTransferInterBankPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _FundTransferInterBank.Notes);
                                cmd.Parameters.AddWithValue("@PK", _FundTransferInterBank.FundTransferInterBankPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _FundTransferInterBank.HistoryPK);
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

        public void FundTransferInterBank_Approved(FundTransferInterBank _FundTransferInterBank)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update FundTransferInterBank set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,LastUpdate=@LastUpdate " +
                            "where FundTransferInterBankPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _FundTransferInterBank.FundTransferInterBankPK);
                        cmd.Parameters.AddWithValue("@historyPK", _FundTransferInterBank.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _FundTransferInterBank.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update FundTransferInterBank set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where FundTransferInterBankPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _FundTransferInterBank.FundTransferInterBankPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _FundTransferInterBank.ApprovedUsersID);
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

        public void FundTransferInterBank_Reject(FundTransferInterBank _FundTransferInterBank)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update FundTransferInterBank set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where FundTransferInterBankPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _FundTransferInterBank.FundTransferInterBankPK);
                        cmd.Parameters.AddWithValue("@historyPK", _FundTransferInterBank.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _FundTransferInterBank.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update FundTransferInterBank set status= 2,LastUpdate=@LastUpdate where FundTransferInterBankPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _FundTransferInterBank.FundTransferInterBankPK);
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

        public void FundTransferInterBank_Void(FundTransferInterBank _FundTransferInterBank)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update FundTransferInterBank set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where FundTransferInterBankPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _FundTransferInterBank.FundTransferInterBankPK);
                        cmd.Parameters.AddWithValue("@historyPK", _FundTransferInterBank.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _FundTransferInterBank.VoidUsersID);
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

        // AREA LAIN-LAIN dimulai dari sini ( untuk function diluar standart )

        public Boolean Generate_FundTransferInterBank(string _userID, int _FundTransferInterBankPK)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText =
                         @" Select case when A.status=1 then 'PENDING' else Case When A.status = 2 then 'APPROVED' else Case when A.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,
                            B.ID FundID,C.ID FundCashRefIDFrom,C1.ID FundCashRefIDTo,D1.Name SignatureName1,D2.Name SignatureName2,C.BankAccountName FundCashRefAccountFrom, C.BankAccountNo FundCashRefAccountNoFrom,E1.BankAccountName BankFrom,
							C1.BankAccountName FundCashRefAccountTo, C1.BankAccountNo FundCashRefAccountNoTo,E2.BankAccountName BankTo, * from FundTransferInterBank  A left join
                            Fund B on A.FundPK = B.FundPK and B.Status in(1,2) left join
                            FundCashRef C on A.FundCashRefPKFrom = C.FundCashRefPK and C.Status in (1,2) left join
                            FundCashRef C1 on A.FundCashRefPKTo = C1.FundCashRefPK and C1.Status in (1,2) left join
                            Signature D1 on A.Signature1 = D1.SignaturePK and D1.Status in (1,2) left join
                            Signature D2 on A.Signature2 = D2.SignaturePK and D2.Status in (1,2) left join
							BankBranch E1 on C.BankBranchPK = E1.BankBranchPK and E1.Status in (1,2) left join
							BankBranch E2 on C1.BankBranchPK = E2.BankBranchPK and E2.Status in (1,2) where A.FundTransferInterBankPK = @FundTransferInterBankPK order by FundTransferInterBankPK ";

                        cmd.Parameters.AddWithValue("@FundTransferInterBankPK", _FundTransferInterBankPK);
                        //cmd.Parameters.AddWithValue("@Status", _cashier.Status);

                        using (SqlDataReader dr0 = cmd.ExecuteReader())
                        {
                            if (!dr0.HasRows)
                            {
                                return false;
                            }
                            else
                            {
                                string filePath = Tools.ReportsPath + "FundTransferInterBank" + "_" + _userID + ".xlsx";
                                string pdfPath = Tools.ReportsPath + "FundTransferInterBank" + "_" + _userID + ".pdf";
                                FileInfo excelFile = new FileInfo(filePath);
                                if (excelFile.Exists)
                                {
                                    excelFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                    excelFile = new FileInfo(filePath);
                                }

                                // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                using (ExcelPackage package = new ExcelPackage(excelFile))
                                {
                                    package.Workbook.Properties.Title = "PindahBuku";
                                    package.Workbook.Properties.Author = Tools.DefaultReportAuthor();
                                    package.Workbook.Properties.Comments = Tools.DefaultReportComments();
                                    package.Workbook.Properties.Company = Tools.DefaultReportCompany();
                                    package.Workbook.Properties.SetCustomPropertyValue("Checked by", _userID);
                                    package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", Tools.DefaultReportAssemblyName());

                                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Pindah Buku");


                                    //ATUR DATA GROUPINGNYA DULU
                                    List<FundTransferInterBank> rList = new List<FundTransferInterBank>();
                                    while (dr0.Read())
                                    {
                                        FundTransferInterBank rSingle = new FundTransferInterBank();
                                        rSingle.ValueDate = Convert.ToDateTime(dr0["Date"]);
                                        rSingle.FundCashRefIDFrom = Convert.ToString(dr0["FundCashRefIDFrom"]);
                                        rSingle.FundCashRefAccountFrom = Convert.ToString(dr0["FundCashRefAccountFrom"]);
                                        rSingle.FundCashRefAccountNoFrom = Convert.ToString(dr0["FundCashRefAccountNoFrom"]);
                                        rSingle.BankFrom = Convert.ToString(dr0["BankFrom"]);
                                        rSingle.FundCashRefIDTo = Convert.ToString(dr0["FundCashRefIDTo"]);
                                        rSingle.FundCashRefAccountTo = Convert.ToString(dr0["FundCashRefAccountTo"]);
                                        rSingle.FundCashRefAccountNoTo = Convert.ToString(dr0["FundCashRefAccountNoTo"]);
                                        rSingle.BankTo = Convert.ToString(dr0["BankTo"]);
                                        rSingle.InstructionNo = Convert.ToString(dr0["InstructionNo"]);
                                        rSingle.InformationTo = Convert.ToString(dr0["InformationTo"]);
                                        rSingle.CCTo = Convert.ToString(dr0["CCTo"]);
                                        rSingle.Subject = Convert.ToString(dr0["Subject"]);
                                        rSingle.SignatureName1 = Convert.ToString(dr0["SignatureName1"]);
                                        rSingle.SignatureName2 = Convert.ToString(dr0["SignatureName2"]);
                                        rList.Add(rSingle);

                                    }

                                    var GroupByReference =
                                                 from r in rList
                                                 group r by new { r.ValueDate, r.InstructionNo, r.InformationTo, r.CCTo, r.Subject, r.FundCashRefAccountFrom, r.FundCashRefAccountTo, r.FundCashRefAccountNoFrom, r.FundCashRefAccountNoTo, r.SignatureName1, r.SignatureName2, r.BankFrom, r.BankTo } into rGroup
                                                 select rGroup;

                                    int incRowExcel = 4;

                                    foreach (var rsHeader in GroupByReference)
                                    {
                                        worksheet.Cells[incRowExcel, 5].Value = _host.Get_CompanyName();
                                        worksheet.Cells["E" + incRowExcel + ":F" + incRowExcel].Merge = true;
                                        incRowExcel++;
                                        worksheet.Cells["E" + incRowExcel + ":F" + incRowExcel].Merge = true;
                                        worksheet.Cells[incRowExcel, 5].Value = _host.Get_CompanyAddress();
                                        worksheet.Cells[incRowExcel, 5].Style.WrapText = true;
                                        worksheet.Row(incRowExcel).Height = 40;
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "Jakarta, " + Convert.ToDateTime(rsHeader.Key.ValueDate).ToString("dd MMMM yyyy");
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "Instruksi No.   : " + rsHeader.Key.InstructionNo;
                                        incRowExcel = incRowExcel + 2;

                                        worksheet.Cells[incRowExcel, 1].Value = "Kepada : ";
                                        incRowExcel++;

                                        worksheet.Cells[incRowExcel, 1].Value = rsHeader.Key.InformationTo;
                                        incRowExcel = incRowExcel + 2;

                                        worksheet.Cells[incRowExcel, 1].Value = "Tembusan : ";
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = rsHeader.Key.CCTo;
                                        incRowExcel++;

                                        worksheet.Cells[incRowExcel, 1].Value = "(mohon untuk Dilaksanakan)";
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Italic = true;
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Color.SetColor(Color.DodgerBlue);
                                        incRowExcel++;


                                        worksheet.Cells[incRowExcel, 1].Value = "Perihal : " + rsHeader.Key.Subject;
                                        incRowExcel = incRowExcel + 2;

                                        worksheet.Cells[incRowExcel, 1].Value = "Dengan Hormat, ";
                                        incRowExcel = incRowExcel + 2;

                                        worksheet.Cells[incRowExcel, 1].Value = "Bersama ini kami mohon bantuan Saudara untuk melakukan pemindahbukuan dana (RTGS) sebagai berikut : ";
                                        worksheet.Cells["A" + incRowExcel + ":F" + incRowExcel].Merge = true;
                                        worksheet.Cells[incRowExcel, 1].Style.WrapText = true;
                                        worksheet.Row(incRowExcel).Height = 30;
                                        incRowExcel++;


                                        worksheet.Cells[incRowExcel, 1].Value = "Tanggal";
                                        worksheet.Cells[incRowExcel, 2].Value = " : " + Convert.ToDateTime(rsHeader.Key.ValueDate).ToString("dd MMMM yyyy");
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "Jumlah Uang";
                                        worksheet.Cells[incRowExcel, 2].Value = " : " + rsHeader.Key.Subject;
                                        incRowExcel = incRowExcel + 2;
                                        worksheet.Cells[incRowExcel, 1].Value = "Debet Account";
                                        worksheet.Cells[incRowExcel, 2].Value = " : " + rsHeader.Key.FundCashRefAccountFrom;
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "Nomor Account";
                                        worksheet.Cells[incRowExcel, 2].Value = " : " + rsHeader.Key.FundCashRefAccountNoFrom;
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "Bank";
                                        worksheet.Cells[incRowExcel, 2].Value = " : " + rsHeader.Key.BankFrom;
                                        incRowExcel = incRowExcel + 2;

                                        worksheet.Cells[incRowExcel, 1].Value = "Debit Account";
                                        worksheet.Cells[incRowExcel, 2].Value = " : " + rsHeader.Key.FundCashRefAccountTo;
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "Nomor Account";
                                        worksheet.Cells[incRowExcel, 2].Value = " : " + rsHeader.Key.FundCashRefAccountNoTo;
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "Bank";
                                        worksheet.Cells[incRowExcel, 2].Value = " : " + rsHeader.Key.BankTo;
                                        incRowExcel++;

                                        worksheet.Cells[incRowExcel, 1].Value = "Demikian kami sampaikan, terima kasih atas perhatian dan kerjasamanya.";
                                        worksheet.Cells["A" + incRowExcel + ":E" + incRowExcel].Merge = true;
                                        incRowExcel++;

                                        worksheet.Cells[incRowExcel, 1].Value = "Hormat Kami,";
                                        incRowExcel = incRowExcel + 4;

                                        worksheet.Cells[incRowExcel, 1].Value = rsHeader.Key.SignatureName1;

                                        worksheet.Cells[incRowExcel, 3].Value = rsHeader.Key.SignatureName2;
                                        incRowExcel++;
                                        worksheet.Row(incRowExcel).PageBreak = true;
                                        incRowExcel++;

                                    }

                                    // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                    worksheet.PrinterSettings.FitToPage = true;
                                    worksheet.PrinterSettings.FitToWidth = 1;
                                    worksheet.PrinterSettings.FitToHeight = 0;
                                    worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel - 1, 6];
                                    worksheet.Column(1).Width = 15;
                                    worksheet.Column(2).Width = 15;
                                    worksheet.Column(3).Width = 15;
                                    worksheet.Column(4).Width = 15;
                                    worksheet.Column(5).Width = 20;
                                    worksheet.Column(6).Width = 15;
                                    worksheet.PrinterSettings.Orientation = eOrientation.Portrait; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                    // worksheet.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN
                                    // worksheet.PrinterSettings.FitToPage = true;
                                    //worksheet.HeaderFooter.OddHeader.RightAlignedText = "&14 PAYMENT VOUCHER";
                                    // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                    worksheet.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
                                    worksheet.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
                                    //worksheet.View.PageLayoutView = Tools.DefaultReportPageLayoutView();
                                    worksheet.View.ShowGridLines = Tools.DefaultReportShowGridLines();
                                    //worksheet.HeaderFooter.OddHeader.CenteredText = "&14 PAYMENT / JOURNAL VOUCHER";
                                    Image img = Image.FromFile(Tools.ReportImage);
                                    worksheet.HeaderFooter.OddHeader.InsertPicture(img, PictureAlignment.Right);
                                    //worksheet.HeaderFooter.OddHeader.RightAlignedText = Tools.DefaultReportHeaderLeftText();

                                    worksheet.HeaderFooter.OddFooter.RightAlignedText =
                                    string.Format("Page {0} of {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);

                                    //worksheet.HeaderFooter.OddFooter.CenteredText = Tools.DefaultReportFooterCenterText();
                                    worksheet.HeaderFooter.OddFooter.LeftAlignedText = Tools.DefaultReportFooterLeftText();

                                    package.Save();

                                }
                                Tools.ExportFromExcelToPDF(filePath, pdfPath);
                                return true;
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

    }
}