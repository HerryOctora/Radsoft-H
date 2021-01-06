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
    public class CounterpartReps
    {
        Host _host = new Host();
        
        //1
        string _insertCommand = "INSERT INTO [dbo].[Counterpart] " +
                             "([CounterpartPK],[HistoryPK],[Status],[ID],[Name],[ContactPerson],[Address],[Phone],[Fax],[Email],[NPWP],[CounterpartType],[Description],[BitSuspended],[BitRegistered],[CBestAccount],[MarketPK],[TDays]," +
                             "[CashRefPaymentPK],[BankPK],[BankAccountOffice],[BankAccountNo],[BankAccountRecipient],[LevyVATPercent],[RoundingMode],[DecimalPlaces],[SInvestCode],[TargetAllocationPercentage],[APERDMFeeMethod],";
        string _paramaterCommand = "@ID,@Name,@ContactPerson,@Address,@Phone,@Fax,@Email,@NPWP,@CounterpartType,@Description,@BitSuspended,@BitRegistered,@CBestAccount,@MarketPK,@TDays,@CashRefPaymentPK,@BankPK,@BankAccountOffice, " +
                                   "@BankAccountNo,@BankAccountRecipient,@LevyVATPercent,@RoundingMode,@DecimalPlaces,@SInvestCode,@TargetAllocationPercentage,@APERDMFeeMethod, ";

        //2
        private Counterpart setCounterpart(SqlDataReader dr)
        {
            Counterpart M_Counterpart = new Counterpart();
            M_Counterpart.CounterpartPK = Convert.ToInt32(dr["CounterpartPK"]);
            M_Counterpart.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_Counterpart.Status = Convert.ToInt32(dr["Status"]);
            M_Counterpart.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_Counterpart.Notes = Convert.ToString(dr["Notes"]);
            M_Counterpart.ID = Convert.ToString(dr["ID"]);
            M_Counterpart.Name = Convert.ToString(dr["Name"]);
            M_Counterpart.ContactPerson = Convert.ToString(dr["ContactPerson"]);
            M_Counterpart.Address = Convert.ToString(dr["Address"]);
            M_Counterpart.Phone = Convert.ToString(dr["Phone"]);
            M_Counterpart.Fax = Convert.ToString(dr["Fax"]);
            M_Counterpart.Email = Convert.ToString(dr["Email"]);
            M_Counterpart.NPWP = Convert.ToString(dr["NPWP"]);
            M_Counterpart.CounterpartType = Convert.ToInt32(dr["CounterpartType"]);
            M_Counterpart.CounterpartTypeDesc = Convert.ToString(dr["CounterpartTypeDesc"]);
            M_Counterpart.Description = Convert.ToString(dr["Description"]);
            M_Counterpart.BitSuspended = Convert.ToBoolean(dr["BitSuspended"]);
            M_Counterpart.BitRegistered = Convert.ToBoolean(dr["BitRegistered"]);
            M_Counterpart.CBestAccount = Convert.ToString(dr["CBestAccount"]);
            M_Counterpart.MarketPK = Convert.ToInt32(dr["MarketPK"]);
            M_Counterpart.MarketID = Convert.ToString(dr["MarketID"]);
            M_Counterpart.TDays = Convert.ToInt32(dr["TDays"]);
            M_Counterpart.CashRefPaymentPK = Convert.ToInt32(dr["CashRefPaymentPK"]);
            M_Counterpart.CashRefPaymentID = Convert.ToString(dr["CashRefPaymentID"]);
            M_Counterpart.BankPK = Convert.ToInt32(dr["BankPK"]);
            M_Counterpart.BankAccountID = Convert.ToString(dr["BankAccountID"]);
            M_Counterpart.BankAccountOffice = Convert.ToString(dr["BankAccountOffice"]);
            M_Counterpart.BankAccountNo = Convert.ToString(dr["BankAccountNo"]);
            M_Counterpart.BankAccountRecipient = Convert.ToString(dr["BankAccountRecipient"]);
            M_Counterpart.LevyVATPercent = Convert.ToDecimal(dr["LevyVATPercent"]);
            M_Counterpart.DecimalPlaces = Convert.ToInt32(dr["DecimalPlaces"]);
            M_Counterpart.RoundingMode = Convert.ToInt32(dr["RoundingMode"]);
            M_Counterpart.RoundingModeDesc = Convert.ToString(dr["RoundingModeDesc"]);
            M_Counterpart.SInvestCode = Convert.ToString(dr["SInvestCode"]);
            M_Counterpart.TargetAllocationPercentage = Convert.ToDecimal(dr["TargetAllocationPercentage"]);
            M_Counterpart.APERDMFeeMethod = Convert.ToInt32(dr["APERDMFeeMethod"]);
            M_Counterpart.APERDMFeeMethodID = Convert.ToString(dr["APERDMFeeMethodID"]);
            M_Counterpart.EntryUsersID = dr["EntryUsersID"].ToString();
            M_Counterpart.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_Counterpart.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_Counterpart.VoidUsersID = dr["VoidUsersID"].ToString();
            M_Counterpart.EntryTime = dr["EntryTime"].ToString();
            M_Counterpart.UpdateTime = dr["UpdateTime"].ToString();
            M_Counterpart.ApprovedTime = dr["ApprovedTime"].ToString();
            M_Counterpart.VoidTime = dr["VoidTime"].ToString();
            M_Counterpart.DBUserID = dr["DBUserID"].ToString();
            M_Counterpart.DBTerminalID = dr["DBTerminalID"].ToString();
            M_Counterpart.LastUpdate = dr["LastUpdate"].ToString();
            M_Counterpart.LastUpdateDB = Convert.ToString(dr["LastUpdateDB"]);
            return M_Counterpart;
        }

        public List<Counterpart> Counterpart_Select(int _status)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<Counterpart> L_Counterpart = new List<Counterpart>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = @"Select  case when A.status=1 then 'PENDING' else Case When A.status = 2 then 'APPROVED' else Case when A.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,
                            E.ID BankAccountID,D.ID CashRefPaymentID,B.ID MarketID,C.DescOne CounterpartTypeDesc,F.DescOne DecimalPlaces,G.DescOne RoundingModeDesc,H.DescOne APERDMFeeMethodID, A.* 
                            from Counterpart A 
                            left join Market B on A.MarketPK  = B.MarketPK and B.status = 2 
                            left join MasterValue C on A.CounterpartType  = C.Code and C.ID = 'CounterpartType' and C.Status = 2 
                            Left join FundCashRef D on A.CashRefPaymentPK = D.FundCashRefPK and D.Status = 2 
                            Left join BankBranch E on A.BankPK = E.BankBranchPK and E.status = 2							
                            left join MasterValue F on A.DecimalPlaces = F.Code and F.Status=2 and F.ID ='DecimalPlaces'
                            left join MasterValue G on A.RoundingMode = G.Code and G.Status=2 and G.ID ='RoundingMode'  
							left join MasterValue H on A.APERDMFeeMethod = H.Code and H.Status=2 and H.ID ='APERDMFeeMethod'  
                            where A.status = @status  order by CounterpartPK  ";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = @"Select  case when A.status=1 then 'PENDING' else Case When A.status = 2 then 'APPROVED' else Case when A.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,
                            E.ID BankAccountID,D.ID CashRefPaymentID,B.ID MarketID,C.DescOne CounterpartTypeDesc,F.DescOne DecimalPlaces,G.DescOne RoundingModeDesc,H.DescOne APERDMFeeMethodID, A.* 
                            from Counterpart A 
                            left join Market B on A.MarketPK  = B.MarketPK and B.status = 2 
                            left join MasterValue C on A.CounterpartType  = C.Code and C.ID = 'CounterpartType' and C.Status = 2 
                            Left join FundCashRef D on A.CashRefPaymentPK = D.FundCashRefPK and D.Status = 2 
                            Left join BankBranch E on A.BankPK = E.BankBranchPK and E.status = 2							
                            left join MasterValue F on A.DecimalPlaces = F.Code and F.Status=2 and F.ID ='DecimalPlaces'
                            left join MasterValue G on A.RoundingMode = G.Code and G.Status=2 and G.ID ='RoundingMode'  
							left join MasterValue H on A.APERDMFeeMethod = H.Code and H.Status=2 and H.ID ='APERDMFeeMethod'  
                            order by CounterpartPK ";
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_Counterpart.Add(setCounterpart(dr));
                                }
                            }
                            return L_Counterpart;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int Counterpart_Add(Counterpart _counterpart, bool _havePrivillege)
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
                                 "Select isnull(max(CounterpartPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@LastUpdate From Counterpart";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _counterpart.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);

                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[LastUpdate])" +
                                "Select isnull(max(CounterpartPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@LastUpdate From Counterpart";
                        }
                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@ID", _counterpart.ID);
                        cmd.Parameters.AddWithValue("@Name", _counterpart.Name);
                        cmd.Parameters.AddWithValue("@ContactPerson", _counterpart.ContactPerson);
                        cmd.Parameters.AddWithValue("@Address", _counterpart.Address);
                        cmd.Parameters.AddWithValue("@Phone", _counterpart.Phone);
                        cmd.Parameters.AddWithValue("@Fax", _counterpart.Fax);
                        cmd.Parameters.AddWithValue("@Email", _counterpart.Email);
                        cmd.Parameters.AddWithValue("@NPWP", _counterpart.NPWP);
                        cmd.Parameters.AddWithValue("@CounterpartType", _counterpart.CounterpartType);
                        cmd.Parameters.AddWithValue("@Description", _counterpart.Description);
                        cmd.Parameters.AddWithValue("@BitSuspended", _counterpart.BitSuspended);
                        cmd.Parameters.AddWithValue("@BitRegistered", _counterpart.BitRegistered);
                        cmd.Parameters.AddWithValue("@CBestAccount", _counterpart.CBestAccount);
                        cmd.Parameters.AddWithValue("@MarketPK", _counterpart.MarketPK);
                        cmd.Parameters.AddWithValue("@TDays", _counterpart.TDays);
                        cmd.Parameters.AddWithValue("@CashRefPaymentPK", _counterpart.CashRefPaymentPK);
                        cmd.Parameters.AddWithValue("@BankPK", _counterpart.BankPK);
                        cmd.Parameters.AddWithValue("@BankAccountOffice", _counterpart.BankAccountOffice);
                        cmd.Parameters.AddWithValue("@BankAccountNo", _counterpart.BankAccountNo);
                        cmd.Parameters.AddWithValue("@BankAccountRecipient", _counterpart.BankAccountRecipient);
                        cmd.Parameters.AddWithValue("@LevyVATPercent", _counterpart.LevyVATPercent);
                        cmd.Parameters.AddWithValue("@RoundingMode", _counterpart.RoundingMode);
                        cmd.Parameters.AddWithValue("@DecimalPlaces", _counterpart.DecimalPlaces);
                        cmd.Parameters.AddWithValue("@SInvestCode", _counterpart.SInvestCode);
                        cmd.Parameters.AddWithValue("@TargetAllocationPercentage", _counterpart.TargetAllocationPercentage);
                        cmd.Parameters.AddWithValue("@APERDMFeeMethod", _counterpart.APERDMFeeMethod);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _counterpart.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);

                        cmd.ExecuteNonQuery();

                        return _host.Get_LastPKByLastUpate(_dateTimeNow, "Counterpart");
                    }
                }

            }
            catch (Exception err)
            {
                throw err;
            }

        }


        public int Counterpart_Update(Counterpart _counterpart, bool _havePrivillege)
        {

            try
            {
                int _newHisPK;
                int status = _host.Get_Status(_counterpart.CounterpartPK, _counterpart.HistoryPK, "counterpart");
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update Counterpart set status=2,Notes=@Notes,ID=@ID,Name=@Name,ContactPerson=@ContactPerson,Address=@Address,Phone=@Phone,Fax=@Fax,Email=@Email,NPWP=@NPWP,CounterpartType=@CounterpartType,Description=@Description, " +
                                "BitSuspended=@BitSuspended,BitRegistered=@BitRegistered,CBestAccount=@CBestAccount,MarketPK=@MarketPK,TDays=@TDays, " +
                                "CashRefPaymentPK = @CashRefPaymentPK,BankPK = @BankPK,BankAccountOffice = @BankAccountOffice,BankAccountNo = @BankAccountNo,BankAccountRecipient = @BankAccountRecipient," +
                                "LevyVATPercent = @LevyVATPercent,RoundingMode = @RoundingMode,DecimalPlaces = @DecimalPlaces,SInvestCode = @SInvestCode, TargetAllocationPercentage=@TargetAllocationPercentage,APERDMFeeMethod=@APERDMFeeMethod," +
                                "ApprovedUsersID=@ApprovedUsersID, " +
                                "ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@LastUpdate " +
                                "where CounterpartPK = @PK and historyPK = @HistoryPK";
                            cmd.Parameters.AddWithValue("@HistoryPK", _counterpart.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _counterpart.CounterpartPK);
                            cmd.Parameters.AddWithValue("@Notes", _counterpart.Notes);
                            cmd.Parameters.AddWithValue("@ID", _counterpart.ID);
                            cmd.Parameters.AddWithValue("@Name", _counterpart.Name);
                            cmd.Parameters.AddWithValue("@ContactPerson", _counterpart.ContactPerson);
                            cmd.Parameters.AddWithValue("@Address", _counterpart.Address);
                            cmd.Parameters.AddWithValue("@Phone", _counterpart.Phone);
                            cmd.Parameters.AddWithValue("@Fax", _counterpart.Fax);
                            cmd.Parameters.AddWithValue("@Email", _counterpart.Email);
                            cmd.Parameters.AddWithValue("@NPWP", _counterpart.NPWP);
                            cmd.Parameters.AddWithValue("@CounterpartType", _counterpart.CounterpartType);
                            cmd.Parameters.AddWithValue("@Description", _counterpart.Description);
                            cmd.Parameters.AddWithValue("@BitSuspended", _counterpart.BitSuspended);
                            cmd.Parameters.AddWithValue("@BitRegistered", _counterpart.BitRegistered);
                            cmd.Parameters.AddWithValue("@CBestAccount", _counterpart.CBestAccount);
                            cmd.Parameters.AddWithValue("@MarketPK", _counterpart.MarketPK);
                            cmd.Parameters.AddWithValue("@TDays", _counterpart.TDays);
                            cmd.Parameters.AddWithValue("@CashRefPaymentPK", _counterpart.CashRefPaymentPK);
                            cmd.Parameters.AddWithValue("@BankPK", _counterpart.BankPK);
                            cmd.Parameters.AddWithValue("@BankAccountOffice", _counterpart.BankAccountOffice);
                            cmd.Parameters.AddWithValue("@BankAccountNo", _counterpart.BankAccountNo);
                            cmd.Parameters.AddWithValue("@BankAccountRecipient", _counterpart.BankAccountRecipient);
                            cmd.Parameters.AddWithValue("@LevyVATPercent", _counterpart.LevyVATPercent);
                            cmd.Parameters.AddWithValue("@TargetAllocationPercentage", _counterpart.TargetAllocationPercentage);
                            cmd.Parameters.AddWithValue("@APERDMFeeMethod", _counterpart.APERDMFeeMethod);
                            cmd.Parameters.AddWithValue("@RoundingMode", _counterpart.RoundingMode);
                            cmd.Parameters.AddWithValue("@DecimalPlaces", _counterpart.DecimalPlaces);
                            cmd.Parameters.AddWithValue("@SInvestCode", _counterpart.SInvestCode);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _counterpart.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _dateTimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _counterpart.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);
                            cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update Counterpart set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where CounterpartPK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _counterpart.CounterpartPK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _counterpart.EntryUsersID);
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
                                cmd.CommandText = "Update Counterpart set Notes=@Notes,ID=@ID,Name=@Name,ContactPerson=@ContactPerson,Address=@Address,Phone=@Phone,Fax=@Fax,Email=@Email,NPWP=@NPWP,CounterpartType=@CounterpartType,Description=@Description, " +
                                    "BitSuspended=@BitSuspended,BitRegistered=@BitRegistered,CBestAccount=@CBestAccount,MarketPK=@MarketPK,TDays=@TDays, " +
                                    "CashRefPaymentPK = @CashRefPaymentPK,BankPK = @BankPK,BankAccountOffice = @BankAccountOffice,BankAccountNo = @BankAccountNo,BankAccountRecipient = @BankAccountRecipient," +
                                    "LevyVATPercent = @LevyVATPercent ,RoundingMode = @RoundingMode,DecimalPlaces = @DecimalPlaces,SInvestCode = @SInvestCode, TargetAllocationPercentage = @TargetAllocationPercentage,APERDMFeeMethod=@APERDMFeeMethod," +
                                    "UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@LastUpdate " +
                                    "where CounterpartPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _counterpart.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _counterpart.CounterpartPK);
                                cmd.Parameters.AddWithValue("@Notes", _counterpart.Notes);
                                cmd.Parameters.AddWithValue("@ID", _counterpart.ID);
                                cmd.Parameters.AddWithValue("@Name", _counterpart.Name);
                                cmd.Parameters.AddWithValue("@ContactPerson", _counterpart.ContactPerson);
                                cmd.Parameters.AddWithValue("@Address", _counterpart.Address);
                                cmd.Parameters.AddWithValue("@Phone", _counterpart.Phone);
                                cmd.Parameters.AddWithValue("@Fax", _counterpart.Fax);
                                cmd.Parameters.AddWithValue("@Email", _counterpart.Email);
                                cmd.Parameters.AddWithValue("@NPWP", _counterpart.NPWP);
                                cmd.Parameters.AddWithValue("@CounterpartType", _counterpart.CounterpartType);
                                cmd.Parameters.AddWithValue("@Description", _counterpart.Description);
                                cmd.Parameters.AddWithValue("@BitSuspended", _counterpart.BitSuspended);
                                cmd.Parameters.AddWithValue("@BitRegistered", _counterpart.BitRegistered);
                                cmd.Parameters.AddWithValue("@CBestAccount", _counterpart.CBestAccount);
                                cmd.Parameters.AddWithValue("@MarketPK", _counterpart.MarketPK);
                                cmd.Parameters.AddWithValue("@TDays", _counterpart.TDays);
                                cmd.Parameters.AddWithValue("@CashRefPaymentPK", _counterpart.CashRefPaymentPK);
                                cmd.Parameters.AddWithValue("@BankPK", _counterpart.BankPK);
                                cmd.Parameters.AddWithValue("@BankAccountOffice", _counterpart.BankAccountOffice);
                                cmd.Parameters.AddWithValue("@BankAccountNo", _counterpart.BankAccountNo);
                                cmd.Parameters.AddWithValue("@BankAccountRecipient", _counterpart.BankAccountRecipient);
                                cmd.Parameters.AddWithValue("@LevyVATPercent", _counterpart.LevyVATPercent);
                                cmd.Parameters.AddWithValue("@RoundingMode", _counterpart.RoundingMode);
                                cmd.Parameters.AddWithValue("@DecimalPlaces", _counterpart.DecimalPlaces);
                                cmd.Parameters.AddWithValue("@SInvestCode", _counterpart.SInvestCode);
                                cmd.Parameters.AddWithValue("@TargetAllocationPercentage", _counterpart.TargetAllocationPercentage);
                                cmd.Parameters.AddWithValue("@APERDMFeeMethod", _counterpart.APERDMFeeMethod);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _counterpart.EntryUsersID);
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
                                _newHisPK = _host.Get_NewHistoryPK(_counterpart.CounterpartPK, "Counterpart");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From Counterpart where CounterpartPK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _counterpart.CounterpartPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _counterpart.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@ID", _counterpart.ID);
                                cmd.Parameters.AddWithValue("@Name", _counterpart.Name);
                                cmd.Parameters.AddWithValue("@ContactPerson", _counterpart.ContactPerson);
                                cmd.Parameters.AddWithValue("@Address", _counterpart.Address);
                                cmd.Parameters.AddWithValue("@Phone", _counterpart.Phone);
                                cmd.Parameters.AddWithValue("@Fax", _counterpart.Fax);
                                cmd.Parameters.AddWithValue("@Email", _counterpart.Email);
                                cmd.Parameters.AddWithValue("@NPWP", _counterpart.NPWP);
                                cmd.Parameters.AddWithValue("@CounterpartType", _counterpart.CounterpartType);
                                cmd.Parameters.AddWithValue("@Description", _counterpart.Description);
                                cmd.Parameters.AddWithValue("@BitSuspended", _counterpart.BitSuspended);
                                cmd.Parameters.AddWithValue("@BitRegistered", _counterpart.BitRegistered);
                                cmd.Parameters.AddWithValue("@CBestAccount", _counterpart.CBestAccount);
                                cmd.Parameters.AddWithValue("@MarketPK", _counterpart.MarketPK);
                                cmd.Parameters.AddWithValue("@TDays", _counterpart.TDays);
                                cmd.Parameters.AddWithValue("@CashRefPaymentPK", _counterpart.CashRefPaymentPK);
                                cmd.Parameters.AddWithValue("@BankPK", _counterpart.BankPK);
                                cmd.Parameters.AddWithValue("@BankAccountOffice", _counterpart.BankAccountOffice);
                                cmd.Parameters.AddWithValue("@BankAccountNo", _counterpart.BankAccountNo);
                                cmd.Parameters.AddWithValue("@BankAccountRecipient", _counterpart.BankAccountRecipient);
                                cmd.Parameters.AddWithValue("@LevyVATPercent", _counterpart.LevyVATPercent);
                                cmd.Parameters.AddWithValue("@RoundingMode", _counterpart.RoundingMode);
                                cmd.Parameters.AddWithValue("@DecimalPlaces", _counterpart.DecimalPlaces);
                                cmd.Parameters.AddWithValue("@SInvestCode", _counterpart.SInvestCode);
                                cmd.Parameters.AddWithValue("@TargetAllocationPercentage", _counterpart.TargetAllocationPercentage);
                                cmd.Parameters.AddWithValue("@APERDMFeeMethod", _counterpart.APERDMFeeMethod);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _counterpart.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _dateTimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update Counterpart set status= 4,Notes=@Notes," +
                                     "LastUpdate=@LastUpdate where CounterpartPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _counterpart.Notes);
                                cmd.Parameters.AddWithValue("@PK", _counterpart.CounterpartPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _counterpart.HistoryPK);
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

        public void Counterpart_Approved(Counterpart _counterpart)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update Counterpart set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,LastUpdate = @LastUpdate " +
                            "where CounterpartPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _counterpart.CounterpartPK);
                        cmd.Parameters.AddWithValue("@historyPK", _counterpart.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _counterpart.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate",_dateTimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update Counterpart set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where CounterpartPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _counterpart.CounterpartPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _counterpart.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate",_dateTimeNow);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
            
        }

        public void Counterpart_Reject(Counterpart _counterpart)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update Counterpart set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate = @LastUpdate " +
                            "where CounterpartPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _counterpart.CounterpartPK);
                        cmd.Parameters.AddWithValue("@historyPK", _counterpart.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _counterpart.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate",_dateTimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update Counterpart set status= 2,LastUpdate=@LastUpdate where CounterpartPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _counterpart.CounterpartPK);
                        cmd.Parameters.AddWithValue("@LastUpdate",_dateTimeNow);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
           
        }

        public void Counterpart_Void(Counterpart _counterpart)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update Counterpart set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate = @LastUpdate " +
                            "where CounterpartPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _counterpart.CounterpartPK);
                        cmd.Parameters.AddWithValue("@historyPK", _counterpart.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _counterpart.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate",_dateTimeNow);
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

        public List<CounterpartCombo> Counterpart_Combo()
        {
            
            
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<CounterpartCombo> L_Counterpart = new List<CounterpartCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "SELECT  CounterpartPK,ID +' - '+ Name ID, Name FROM [Counterpart]  where status = 2 order by ID,Name";
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    CounterpartCombo M_Counterpart = new CounterpartCombo();
                                    M_Counterpart.CounterpartPK = Convert.ToInt32(dr["CounterpartPK"]);
                                    M_Counterpart.ID = Convert.ToString(dr["ID"]);
                                    M_Counterpart.Name = Convert.ToString(dr["Name"]);
                                    L_Counterpart.Add(M_Counterpart);
                                }

                            }
                            return L_Counterpart;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
           
        }

        public List<CounterpartCombo> UpdateCounterpart_Combo()
        {


            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<CounterpartCombo> L_Counterpart = new List<CounterpartCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "SELECT  CounterpartPK,ID, Name FROM [Counterpart]  where status = 2 order by ID,Name";
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    CounterpartCombo M_Counterpart = new CounterpartCombo();
                                    M_Counterpart.CounterpartPK = Convert.ToInt32(dr["CounterpartPK"]);
                                    M_Counterpart.ID = Convert.ToString(dr["ID"]);
                                    M_Counterpart.Name = Convert.ToString(dr["Name"]);
                                    L_Counterpart.Add(M_Counterpart);
                                }

                            }
                            return L_Counterpart;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }
     
        public List<CounterpartCombo> Counterpart_ComboByMarketPK(int _marketpk)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<CounterpartCombo> L_Counterpart = new List<CounterpartCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText =
                            "select CounterpartPK, a.ID + ' - ' + a.Name ID, a.Name " +
                            "from Counterpart a " +
                                "left join Market b on b.Status = 2 " +
                            "where a.Status = 2 and b.MarketPK = @MarketPK " +
                            "order by a.ID, a.Name";
                        cmd.Parameters.AddWithValue("@MarketPK", _marketpk);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    CounterpartCombo M_Counterpart = new CounterpartCombo();
                                    M_Counterpart.CounterpartPK = Convert.ToInt32(dr["CounterpartPK"]);
                                    M_Counterpart.ID = Convert.ToString(dr["ID"]);
                                    M_Counterpart.Name = Convert.ToString(dr["Name"]);
                                    L_Counterpart.Add(M_Counterpart);
                                }
                            }
                            return L_Counterpart;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public List<CounterpartCombo> Counterpart_ComboRpt()
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<CounterpartCombo> L_Counterpart = new List<CounterpartCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "SELECT  CounterpartPK,ID + ' - ' + Name as ID, Name FROM [Counterpart]  where status = 2 union all select 0,'All', '' order by CounterpartPK,Name";
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    CounterpartCombo M_Counterpart = new CounterpartCombo();
                                    M_Counterpart.CounterpartPK = Convert.ToInt32(dr["CounterpartPK"]);
                                    M_Counterpart.ID = Convert.ToString(dr["ID"]);
                                    M_Counterpart.Name = Convert.ToString(dr["Name"]);
                                    L_Counterpart.Add(M_Counterpart);
                                }

                            }
                            return L_Counterpart;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }


        }


        public List<CounterpartCombo> Counterpart_ComboByCompanyAccountTradingPK(int _companyAccountTradingPK)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<CounterpartCombo> L_Counterpart = new List<CounterpartCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText =
                        @"declare @CounterpartPK int
                        select @CounterpartPK =  CounterpartPK from CompanyAccountTrading where CompanyAccountTradingPK = @CompanyAccountTradingPK
                        IF (@CounterpartPK = 0)
                        BEGIN
	                        select A.CounterpartPK, A.ID + ' - ' + A.Name ID, A.Name  
	                        from Counterpart A  
	                        left join CompanyAccountTrading B on A.CounterpartPK = B.CounterpartPK and B.Status = 2  
	                        where A.status = 2
                        END
                        ELSE
                        BEGIN
	                        select A.CounterpartPK, A.ID + ' - ' + A.Name ID, A.Name  
	                        from Counterpart A  
	                        left join CompanyAccountTrading B on A.CounterpartPK = B.CounterpartPK and B.Status = 2  
	                        where A.status = 2 and CompanyAccountTradingPK = @CompanyAccountTradingPK
                        END ";
                        cmd.Parameters.AddWithValue("@CompanyAccountTradingPK", _companyAccountTradingPK);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    CounterpartCombo M_Counterpart = new CounterpartCombo();
                                    M_Counterpart.CounterpartPK = Convert.ToInt32(dr["CounterpartPK"]);
                                    M_Counterpart.ID = Convert.ToString(dr["ID"]);
                                    M_Counterpart.Name = Convert.ToString(dr["Name"]);
                                    L_Counterpart.Add(M_Counterpart);
                                }
                            }
                            return L_Counterpart;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }



        public List<CounterpartCombo> Counterpart_ComboByCounterpartCommission(int _paramBoardType, string _date)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<CounterpartCombo> L_Counterpart = new List<CounterpartCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText =
                        @"
                     DECLARE @CounterpartCommission TABLE
                    (
	                    CounterpartPK INT,
	                    ID NVARCHAR(400),
	                    Name NVARCHAR(400)
                    )

                    DECLARE	@CounterpartPK int

                    DECLARE A CURSOR  
                        FOR SELECT DISTINCT CounterpartPK FROM dbo.CounterpartCommission WHERE status  = 2
                    OPEN A
                    FETCH NEXT FROM A
                    INTO @CounterpartPk
                    WHILE @@FETCH_STATUS = 0  
                    BEGIN
	
	                    INSERT INTO @CounterpartCommission
	                    SELECT A.CounterpartPK, B.ID + ' - ' + B.Name + ' - ' +  cast(CONVERT(DECIMAL(10,2),A.CommissionPercent) as varchar(10)) + '%'  ID, B.Name  
	                    from CounterpartCommission A left join Counterpart B on A.CounterpartPK = B.CounterpartPK and B.status = 2
	                    where BoardType = @ParamBoardType and A.status = 2 AND A.Date = 
	                    (
		                    SELECT MAX(date) FROM dbo.CounterpartCommission WHERE CounterpartPK = @CounterpartPK 
		                    AND BoardType = @ParamBoardType
		                    AND status = 2 AND Date <= @ValueDate
	                    )
	                    FETCH NEXT FROM A   
                        INTO @CounterpartPK

                    END   
                    CLOSE A;  
                    DEALLOCATE A;  

SELECT distinct CounterpartPK,ID,Name FROM @CounterpartCommission
                         ";


                        cmd.Parameters.AddWithValue("@ParamBoardType", _paramBoardType);
                        cmd.Parameters.AddWithValue("@ValueDate", _date);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    CounterpartCombo M_Counterpart = new CounterpartCombo();
                                    M_Counterpart.CounterpartPK = Convert.ToInt32(dr["CounterpartPK"]);
                                    M_Counterpart.ID = Convert.ToString(dr["ID"]);
                                    M_Counterpart.Name = Convert.ToString(dr["Name"]);
                                    L_Counterpart.Add(M_Counterpart);
                                }
                            }
                            return L_Counterpart;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }



        public List<SetCounterpartFeeSetup> Counterpart_GetDataCounterpartFee(int _counterpartPK)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<SetCounterpartFeeSetup> L_setCounterpartFeeSetup = new List<SetCounterpartFeeSetup>();
                    using (SqlCommand cmd1 = DbCon.CreateCommand())
                    {
                        cmd1.CommandText = @"
                        select A.Status Status, case when A.status=1 then 'PENDING' else Case When A.status = 2 then 'APPROVED' else Case when A.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc, 
                        case when A.CounterpartPK = 0 then 'ALL' else isnull(B.Name,'') end CounterpartName, 
                        MV.DescOne FeeTypeDesc,
                        Date Date, DateAmortize, isnull(APERDPortionAmount,0) APERDPortionAmount,
                        isnull(APERDPortionPercent,0) APERDPortionPercent, isnull(RangeFrom,0) RangeFrom, isnull(RangeTo,0) RangeTo, isnull(B.CounterpartPK,0),A.* 
                        from CounterpartFeesetup A 
                        left join Counterpart B on A.CounterpartPK = B.CounterpartPK and B.status in (1,2) 
                        left join MasterValue MV on A.FeeType = MV.Code and MV.status in (1,2) and MV.ID = 'FundFeeType'
                        where A.CounterpartPK  =  @CounterpartPK and A.Status in (1,2)
                               ";
                        cmd1.Parameters.AddWithValue("@CounterpartPK", _counterpartPK);
                        using (SqlDataReader dr = cmd1.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_setCounterpartFeeSetup.Add(SetCounterpartFeeSetup(dr));
                                }
                            }
                        }
                        return L_setCounterpartFeeSetup;
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        private SetCounterpartFeeSetup SetCounterpartFeeSetup(SqlDataReader dr)
        {
            SetCounterpartFeeSetup M_CounterpartFeeSetup = new SetCounterpartFeeSetup();
            M_CounterpartFeeSetup.Status = Convert.ToInt32(dr["Status"]);
            M_CounterpartFeeSetup.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_CounterpartFeeSetup.Selected = Convert.ToBoolean(dr["Selected"]);
            M_CounterpartFeeSetup.CounterpartFeeSetupPK = Convert.ToInt32(dr["CounterpartFeeSetupPK"]);
            M_CounterpartFeeSetup.CounterpartPK = Convert.ToInt32(dr["CounterpartPK"]);
            M_CounterpartFeeSetup.CounterpartName = Convert.ToString(dr["CounterpartName"]);
            M_CounterpartFeeSetup.Date = Convert.ToString(dr["Date"]);
            M_CounterpartFeeSetup.DateAmortize = Convert.ToString(dr["DateAmortize"]);
            M_CounterpartFeeSetup.FeeType = Convert.ToInt32(dr["FeeType"]);
            M_CounterpartFeeSetup.FeeTypeDesc = Convert.ToString(dr["FeeTypeDesc"]);
            M_CounterpartFeeSetup.RangeTo = Convert.ToDecimal(dr["RangeTo"]);
            M_CounterpartFeeSetup.RangeFrom = Convert.ToDecimal(dr["RangeFrom"]);
            M_CounterpartFeeSetup.APERDPortionAmount = Convert.ToDecimal(dr["APERDPortionAmount"]);
            M_CounterpartFeeSetup.APERDPortionPercent = Convert.ToDecimal(dr["APERDPortionPercent"]);
            return M_CounterpartFeeSetup;
        }


        public int AddCounterpartFee(Counterpart _counterpart, bool _havePrivillege)
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
                            
                        Insert into CounterpartFeeSetup(CounterpartFeeSetupPK,HistoryPK,Status,CounterpartPK,Date,DateAmortize,RangeFrom,RangeTo,APERDPortionAmount,APERDPortionPercent,FeeType,LastUpdate,LastUpdateDB,EntryUsersID,EntryTime,ApprovedUsersID,ApprovedTime,UpdateUsersID,UpdateTime) 
                        Select isnull(max(CounterpartFeeSetupPK),0) + 1,1,2,@CounterpartPK,@Date,@DateAmortize,@RangeFrom,@RangeTo,@APERDPortionAmount,@APERDPortionPercent,@FeeType,@LastUpdate,@LastUpdate,@UpdateUsersID,@UpdateTime,@UpdateUsersID,@UpdateTime,@UpdateUsersID,@UpdateTime from CounterpartFeeSetup";

                        cmd.Parameters.AddWithValue("@CounterpartPK", _counterpart.CounterpartPK);
                        cmd.Parameters.AddWithValue("@Date", _counterpart.Date);
                        if (_counterpart.DateAmortize == null)
                        {
                            cmd.Parameters.AddWithValue("@DateAmortize", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@DateAmortize", _counterpart.DateAmortize);
                        }
                        cmd.Parameters.AddWithValue("@FeeType", _counterpart.FeeType);
                        cmd.Parameters.AddWithValue("@RangeTo", _counterpart.RangeTo);
                        cmd.Parameters.AddWithValue("@RangeFrom", _counterpart.RangeFrom);
                        cmd.Parameters.AddWithValue("@APERDPortionAmount", _counterpart.APERDPortionAmount);
                        cmd.Parameters.AddWithValue("@APERDPortionPercent", _counterpart.APERDPortionPercent);
                        cmd.Parameters.AddWithValue("@UpdateUsersID", _counterpart.EntryUsersID);
                        cmd.Parameters.AddWithValue("@UpdateTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    return _host.Get_LastPKByLastUpate(_datetimeNow, "Counterpart");
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

        public void RejectedDataCounterpartFeeSetupBySelected(string _usersID, string param2, int _counterpartPK)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update CounterpartFeesetup set Status = 3, VoidUsersID = @VoidUsersID, VoidTime = @VoidTime, LastUpdate = @LastUpdate " +
                        "where Selected = 1 and CounterpartPK = @CounterpartPK and status <> 3 ";
                        cmd.Parameters.AddWithValue("@VoidUsersID", _usersID);
                        cmd.Parameters.AddWithValue("@CounterpartPK", _counterpartPK);
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

        public string Add_Validate(Counterpart _counterpart)
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


                        declare @CounterpartFeeSetup table
                        (
                        CounterpartPK int,
                        Date datetime,
                        RangeFrom numeric(22,0),
                        RangeTo numeric(22,0),
                        FeeType int
                        )


                        insert into @CounterpartFeeSetup
                        select CounterpartPK,Date,RangeFrom,RangeTo,FeeType from CounterpartFeeSetup 
                        where status = 2 and Date = @Date and CounterpartPK = @CounterpartPK


                        IF @FeeType not in (2,3)
                        BEGIN
	                        IF EXISTS(select * from @CounterpartFeeSetup)
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
	                        IF NOT EXISTS(select * from @CounterpartFeeSetup where FeeType <> @FeeType)
	                        BEGIN
		                        IF EXISTS(
		                        SELECT * FROM CounterpartFeeSetup 
		                        WHERE (@RangeFrom BETWEEN RangeFrom AND RangeTo 
			                        OR @RangeTo BETWEEN RangeFrom AND RangeTo
			                        OR RangeTo BETWEEN @RangeTo AND @RangeFrom
			                        OR RangeFrom BETWEEN @RangeFrom AND @RangeTo) and CounterpartPK = @CounterpartPK and Date = @Date and FeeType = @FeeType and status = 2
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

                        cmd.Parameters.AddWithValue("@Date", _counterpart.Date);
                        cmd.Parameters.AddWithValue("@CounterpartPK", _counterpart.CounterpartPK);
                        cmd.Parameters.AddWithValue("@FeeType", _counterpart.FeeType);
                        cmd.Parameters.AddWithValue("@RangeFrom", _counterpart.RangeFrom);
                        cmd.Parameters.AddWithValue("@RangeTo", _counterpart.RangeTo);

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