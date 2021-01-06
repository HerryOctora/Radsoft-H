using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RFSModel;
using RFSUtility;
using System.Data.SqlClient;

namespace RFSRepository
{
    public class PrepaidReps
    {
        Host _host = new Host();

        //1
        string _insertCommand = "INSERT INTO [dbo].[Prepaid] " +
                            "([PrepaidPK],[HistoryPK],[Status],[PeriodPK],[PaymentDate],[PeriodStartDate],[PeriodEndDate],[PaymentJournalNo],[Reference], " +
                            " [Description],[PrepaidAccount],[PrepaidAmount],[PrepaidCurrencyPK],[PrepaidCurrencyRate],[DebitAccount1],[DebitAmount1],[DebitCurrencyPk1],[DebitCurrencyRate1], " +
                            " [DebitAccount2],[DebitAmount2],[DebitCurrencyPk2],[DebitCurrencyRate2],[DebitAccount3],[DebitAmount3],[DebitCurrencyPk3],[DebitCurrencyRate3], " +
                            " [DebitAccount4],[DebitAmount4],[DebitCurrencyPk4],[DebitCurrencyRate4], " +
                            " [CreditAccount1],[CreditAmount1],[CreditCurrencyPk1],[CreditCurrencyRate1], " +
                            " [CreditAccount2],[CreditAmount2],[CreditCurrencyPk2],[CreditCurrencyRate2],[CreditAccount3],[CreditAmount3],[CreditCurrencyPk3],[CreditCurrencyRate3], " +
                            " [CreditAccount4],[CreditAmount4],[CreditCurrencyPk4],[CreditCurrencyRate4],[PrepaidExpAccount],[OfficePK], ";



        string _paramaterCommand = "@PeriodPK,@PaymentDate,@PeriodStartDate,@PeriodEndDate,@PaymentJournalNo,@Reference, " +
                            " @Description,@PrepaidAccount,@PrepaidAmount,@PrepaidCurrencyPK,@PrepaidCurrencyRate,@DebitAccount1,@DebitAmount1,@DebitCurrencyPk1,@DebitCurrencyRate1, " +
                            " @DebitAccount2,@DebitAmount2,@DebitCurrencyPk2,@DebitCurrencyRate2,@DebitAccount3,@DebitAmount3,@DebitCurrencyPk3,@DebitCurrencyRate3, " +
                            " @DebitAccount4,@DebitAmount4,@DebitCurrencyPk4,@DebitCurrencyRate4, " +
                            " @CreditAccount1,@CreditAmount1,@CreditCurrencyPk1,@CreditCurrencyRate1, " +
                            " @CreditAccount2,@CreditAmount2,@CreditCurrencyPk2,@CreditCurrencyRate2,@CreditAccount3,@CreditAmount3,@CreditCurrencyPk3,@CreditCurrencyRate3, " +
                            " @CreditAccount4,@CreditAmount4,@CreditCurrencyPk4,@CreditCurrencyRate4,@PrepaidExpAccount,@OfficePK, ";
        private Prepaid setPrepaid(SqlDataReader dr)
        {
     



            Prepaid M_Prepaid = new Prepaid();

            M_Prepaid.PrepaidPK = Convert.ToInt32(dr["PrepaidPK"]);
            M_Prepaid.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_Prepaid.Status = Convert.ToInt32(dr["Status"]);
            M_Prepaid.Notes = Convert.ToString(dr["Notes"]);
            M_Prepaid.PeriodPK = Convert.ToInt32(dr["PeriodPK"]);
            M_Prepaid.PeriodID = Convert.ToString(dr["PeriodID"]);
            M_Prepaid.PaymentDate = dr["PaymentDate"].ToString();
            M_Prepaid.PeriodStartDate = Convert.ToString(dr["PeriodStartDate"]);
            M_Prepaid.PeriodEndDate = Convert.ToString(dr["PeriodEndDate"]);
            M_Prepaid.PaymentJournalNo = Convert.ToInt32(dr["PaymentJournalNo"]);
            M_Prepaid.Reference = Convert.ToString(dr["Reference"]);
            M_Prepaid.Description = Convert.ToString(dr["Description"]);
            M_Prepaid.PrepaidAccount = Convert.ToInt32(dr["PrepaidAccount"]);
            M_Prepaid.PrepaidAccountID = Convert.ToString(dr["PrepaidAccountID"]);
            M_Prepaid.PrepaidAmount = Convert.ToDecimal(dr["PrepaidAmount"]);
            M_Prepaid.PrepaidCurrencyPK = Convert.ToInt32(dr["PrepaidCurrencyPK"]);
            M_Prepaid.PrepaidCurrencyID = Convert.ToString(dr["PrepaidCurrencyID"]);
            M_Prepaid.PrepaidCurrencyRate = Convert.ToDecimal(dr["PrepaidCurrencyRate"]);
            M_Prepaid.DebitAccount1 = Convert.ToInt32(dr["DebitAccount1"]);
            M_Prepaid.DebitAccount1ID = Convert.ToString(dr["DebitAccount1ID"]);
            M_Prepaid.DebitAmount1 = Convert.ToDecimal(dr["DebitAmount1"]);
            M_Prepaid.DebitCurrencyPk1 = Convert.ToInt32(dr["DebitCurrencyPk1"]);
            M_Prepaid.DebitCurrencyPk1ID = Convert.ToString(dr["DebitCurrencyPk1ID"]);
            M_Prepaid.DebitCurrencyRate1 = Convert.ToDecimal(dr["DebitCurrencyRate1"]);
            M_Prepaid.DebitAccount2 = Convert.ToInt32(dr["DebitAccount2"]);
            M_Prepaid.DebitAccount2ID = Convert.ToString(dr["DebitAccount2ID"]);
            M_Prepaid.DebitAmount2 = Convert.ToDecimal(dr["DebitAmount2"]);
            M_Prepaid.DebitCurrencyPk2 = Convert.ToInt32(dr["DebitCurrencyPk2"]);
            M_Prepaid.DebitCurrencyPk2ID = Convert.ToString(dr["DebitCurrencyPk2ID"]);
            M_Prepaid.DebitCurrencyRate2 = Convert.ToDecimal(dr["DebitCurrencyRate2"]);
            M_Prepaid.DebitAccount3 = Convert.ToInt32(dr["DebitAccount3"]);
            M_Prepaid.DebitAccount3ID = Convert.ToString(dr["DebitAccount3ID"]);
            M_Prepaid.DebitAmount3 = Convert.ToDecimal(dr["DebitAmount3"]);
            M_Prepaid.DebitCurrencyPk3 = Convert.ToInt32(dr["DebitCurrencyPk3"]);
            M_Prepaid.DebitCurrencyPk3ID = Convert.ToString(dr["DebitCurrencyPk3ID"]);
            M_Prepaid.DebitCurrencyRate3 = Convert.ToDecimal(dr["DebitCurrencyRate3"]);
            M_Prepaid.DebitAccount4 = Convert.ToInt32(dr["DebitAccount4"]);
            M_Prepaid.DebitAccount4ID = Convert.ToString(dr["DebitAccount4ID"]);
            M_Prepaid.DebitAmount4 = Convert.ToDecimal(dr["DebitAmount4"]);
            M_Prepaid.DebitCurrencyPk4 = Convert.ToInt32(dr["DebitCurrencyPk4"]);
            M_Prepaid.DebitCurrencyPk4ID = Convert.ToString(dr["DebitCurrencyPk4ID"]);
            M_Prepaid.DebitCurrencyRate4 = Convert.ToDecimal(dr["DebitCurrencyRate4"]);
            M_Prepaid.CreditAccount1 = Convert.ToInt32(dr["CreditAccount1"]);
            M_Prepaid.CreditAccount1ID = Convert.ToString(dr["CreditAccount1ID"]);
            M_Prepaid.CreditAmount1 = Convert.ToDecimal(dr["CreditAmount1"]);
            M_Prepaid.CreditCurrencyPk1 = Convert.ToInt32(dr["CreditCurrencyPk1"]);
            M_Prepaid.CreditCurrencyPk1ID = Convert.ToString(dr["CreditCurrencyPk1ID"]);
            M_Prepaid.CreditCurrencyRate1 = Convert.ToDecimal(dr["CreditCurrencyRate1"]);
            M_Prepaid.CreditAccount2 = Convert.ToInt32(dr["CreditAccount2"]);
            M_Prepaid.CreditAccount2ID = Convert.ToString(dr["CreditAccount2ID"]);
            M_Prepaid.CreditAmount2 = Convert.ToDecimal(dr["CreditAmount2"]);
            M_Prepaid.CreditCurrencyPk2 = Convert.ToInt32(dr["CreditCurrencyPk2"]);
            M_Prepaid.CreditCurrencyPk2ID = Convert.ToString(dr["CreditCurrencyPk2ID"]);
            M_Prepaid.CreditCurrencyRate2 = Convert.ToDecimal(dr["CreditCurrencyRate2"]);
            M_Prepaid.CreditAccount3 = Convert.ToInt32(dr["CreditAccount3"]);
            M_Prepaid.CreditAccount3ID = Convert.ToString(dr["CreditAccount3ID"]);
            M_Prepaid.CreditAmount3 = Convert.ToDecimal(dr["CreditAmount3"]);
            M_Prepaid.CreditCurrencyPk3 = Convert.ToInt32(dr["CreditCurrencyPk3"]);
            M_Prepaid.CreditCurrencyPk3ID = Convert.ToString(dr["CreditCurrencyPk3ID"]);
            M_Prepaid.CreditCurrencyRate3 = Convert.ToDecimal(dr["CreditCurrencyRate3"]);
            M_Prepaid.CreditAccount4 = Convert.ToInt32(dr["CreditAccount4"]);
            M_Prepaid.CreditAccount4ID = Convert.ToString(dr["CreditAccount4ID"]);
            M_Prepaid.CreditAmount4 = Convert.ToDecimal(dr["CreditAmount4"]);
            M_Prepaid.CreditCurrencyPk4 = Convert.ToInt32(dr["CreditCurrencyPk4"]);
            M_Prepaid.CreditCurrencyPk4ID = Convert.ToString(dr["CreditCurrencyPk4ID"]);
            M_Prepaid.CreditCurrencyRate4 = Convert.ToDecimal(dr["CreditCurrencyRate4"]);
            M_Prepaid.PrepaidExpAccount = Convert.ToInt32(dr["PrepaidExpAccount"]);
            M_Prepaid.PrepaidExpAccountID = Convert.ToString(dr["PrepaidExpAccountID"]);
            M_Prepaid.OfficePK = Convert.ToInt32(dr["OfficePK"]);
            M_Prepaid.OfficeID = Convert.ToString(dr["OfficeID"]);
            M_Prepaid.Posted = Convert.ToBoolean(dr["Posted"]);
            M_Prepaid.PostedBy = Convert.ToString(dr["PostedBy"]);
            M_Prepaid.PostedTime = Convert.ToString(dr["PostedTime"]);
            M_Prepaid.Revised = Convert.ToBoolean(dr["Revised"]);
            M_Prepaid.RevisedBy = Convert.ToString(dr["RevisedBy"]);
            M_Prepaid.RevisedTime = Convert.ToString(dr["RevisedTime"]);
            M_Prepaid.EntryUsersID = dr["EntryUsersID"].ToString();
            M_Prepaid.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_Prepaid.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_Prepaid.VoidUsersID = dr["VoidUsersID"].ToString();
            M_Prepaid.EntryTime = dr["EntryTime"].ToString();
            M_Prepaid.UpdateTime = dr["UpdateTime"].ToString();
            M_Prepaid.ApprovedTime = dr["ApprovedTime"].ToString();
            M_Prepaid.VoidTime = dr["VoidTime"].ToString();
            M_Prepaid.DBUserID = dr["DBUserID"].ToString();
            M_Prepaid.DBTerminalID = dr["DBTerminalID"].ToString();
            M_Prepaid.LastUpdate = dr["LastUpdate"].ToString();
            M_Prepaid.LastUpdateDB = dr["LastUpdateDB"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["LastUpdateDB"]);
            return M_Prepaid;
        }
        public List<Prepaid> Prepaid_Select(int _status)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<Prepaid> L_Prepaid = new List<Prepaid>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText =
                            " select PR.ID PeriodID,O.ID OfficeID,A1.ID DebitAccount1ID,C1.ID DebitCurrencyPk1ID, " +
                            " A2.ID DebitAccount2ID,C2.ID DebitCurrencyPk2ID, " +
                            " A3.ID DebitAccount3ID,C3.ID DebitCurrencyPk3ID, " +
                            " A4.ID DebitAccount4ID,C4.ID DebitCurrencyPk4ID, " +
                            " A5.ID CreditAccount1ID,C5.ID CreditCurrencyPk1ID, " +
                            " A6.ID CreditAccount2ID,C6.ID CreditCurrencyPk2ID, " +
                            " A7.ID CreditAccount3ID,C7.ID CreditCurrencyPk3ID, " +
                            " A8.ID CreditAccount4ID,C8.ID CreditCurrencyPk4ID, " +
                            " A9.ID PrepaidExpAccountID,A10.ID PrepaidAccountID,C10.ID PrepaidCurrencyID, P.* from Prepaid P Left join " +
                            " Period PR on P.PeriodPK = PR.PeriodPK and PR.Status = 2  Left join  " +
                            " Office O on P.OfficePK = O.OfficePK and O.Status = 2  Left join   " +
                            " Account A1 on P.DebitAccount1 = A1.AccountPK and A1.Status = 2  Left join   " +
                            " Currency C1 on P.DebitCurrencyPk1 = C1.CurrencyPK and C1.Status = 2  Left join   " +
                            " Account A2 on P.DebitAccount2 = A2.AccountPK and A2.Status = 2  Left join   " +
                            " Currency C2 on P.DebitCurrencyPk2 = C2.CurrencyPK and C2.Status = 2  Left join   " +
                            " Account A3 on P.DebitAccount3 = A3.AccountPK and A3.Status = 2  Left join   " +
                            " Currency C3 on P.DebitCurrencyPk3 = C3.CurrencyPK and C3.Status = 2  Left join   " +
                            " Account A4 on P.DebitAccount4 = A4.AccountPK and A4.Status = 2  Left join   " +
                            " Currency C4 on P.DebitCurrencyPk4 = C4.CurrencyPK and C4.Status = 2  Left join   " +
                            " Account A5 on P.CreditAccount1 = A5.AccountPK and A5.Status = 2  Left join   " +
                            " Currency C5 on P.CreditCurrencyPk1 = C5.CurrencyPK and C5.Status = 2  Left join   " +
                            " Account A6 on P.CreditAccount2 = A6.AccountPK and A6.Status = 2  Left join   " +
                            " Currency C6 on P.CreditCurrencyPk2 = C6.CurrencyPK and C6.Status = 2  Left join   " +
                            " Account A7 on P.CreditAccount3 = A7.AccountPK and A7.Status = 2  Left join   " +
                            " Currency C7 on P.CreditCurrencyPk3 = C7.CurrencyPK and C7.Status = 2  Left join   " +
                            " Account A8 on P.CreditAccount4 = A8.AccountPK and A8.Status = 2  Left join   " +
                            " Currency C8 on P.CreditCurrencyPk4 = C8.CurrencyPK and C8.Status = 2  Left join  " +
                            " Account A9 on P.PrepaidExpAccount = A9.AccountPK and A9.Status = 2 Left join   " +
                            " Account A10 on P.PrepaidAccount = A10.AccountPK and A10.Status = 2 Left join  " +
                            " Currency C10 on P.PrepaidCurrencyPK = C10.CurrencyPK and C10.Status = 2    " +
                            " Where P.Status = @status ";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = "Select * from Prepaid order by ID";
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_Prepaid.Add(setPrepaid(dr));
                                }
                            }
                            return L_Prepaid;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }
        public PrepaidAddNew Prepaid_Add(Prepaid _prepaid, bool _havePrivillege)
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
                            cmd.CommandText = "Declare @newPrepaidPK int \n " +
                                 "Select @newPrepaidPK = isnull(max(PrepaidPk),0) + 1 from Prepaid \n " +
                                _insertCommand + "[EntryUsersID],[EntryTime],[ApprovedUsersID],[ApprovedTime],LastUpdate)" +
                                 "Select @newPrepaidPK ,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@LastUpdate \n " +
                                 "Select @newPrepaidPK newPrepaidPK, 1 newHistoryPK ";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _prepaid.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        }
                        else
                        {
                            cmd.CommandText = "Declare @newPrepaidPK int \n " +
                                 "Select @newPrepaidPK = isnull(max(PrepaidPk),0) + 1 from Prepaid \n " +
                                _insertCommand + "[EntryUsersID],[EntryTime],LastUpdate)" +
                                "Select @newPrepaidPK ,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@LastUpdate \n " +
                                 "Select @newPrepaidPK newPrepaidPK, 1 newHistoryPK ";
                        }

                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@PeriodPK", _prepaid.PeriodPK);                   
                        cmd.Parameters.AddWithValue("@PaymentDate", _prepaid.PaymentDate);
                        cmd.Parameters.AddWithValue("@PeriodStartDate", _prepaid.PeriodStartDate);
                        cmd.Parameters.AddWithValue("@PeriodEndDate", _prepaid.PeriodEndDate);
                        cmd.Parameters.AddWithValue("@PaymentJournalNo", _prepaid.PaymentJournalNo);
                        cmd.Parameters.AddWithValue("@Reference", _prepaid.Reference);
                        cmd.Parameters.AddWithValue("@Description", _prepaid.Description);
                        cmd.Parameters.AddWithValue("@PrepaidAccount", _prepaid.PrepaidAccount);
                        cmd.Parameters.AddWithValue("@PrepaidAmount", _prepaid.PrepaidAmount);
                        cmd.Parameters.AddWithValue("@PrepaidCurrencyPK", _prepaid.PrepaidCurrencyPK);
                        cmd.Parameters.AddWithValue("@PrepaidCurrencyRate", _prepaid.PrepaidCurrencyRate);
                        cmd.Parameters.AddWithValue("@DebitAccount1", _prepaid.DebitAccount1);
                        cmd.Parameters.AddWithValue("@DebitAmount1", _prepaid.DebitAmount1);
                        cmd.Parameters.AddWithValue("@DebitCurrencyPk1", _prepaid.DebitCurrencyPk1);
                        cmd.Parameters.AddWithValue("@DebitCurrencyRate1", _prepaid.DebitCurrencyRate1);
                        cmd.Parameters.AddWithValue("@DebitAccount2", _prepaid.DebitAccount2);
                        cmd.Parameters.AddWithValue("@DebitAmount2", _prepaid.DebitAmount2);
                        cmd.Parameters.AddWithValue("@DebitCurrencyPk2", _prepaid.DebitCurrencyPk2);
                        cmd.Parameters.AddWithValue("@DebitCurrencyRate2", _prepaid.DebitCurrencyRate2);
                        cmd.Parameters.AddWithValue("@DebitAccount3", _prepaid.DebitAccount3);
                        cmd.Parameters.AddWithValue("@DebitAmount3", _prepaid.DebitAmount3);
                        cmd.Parameters.AddWithValue("@DebitCurrencyPk3", _prepaid.DebitCurrencyPk3);
                        cmd.Parameters.AddWithValue("@DebitCurrencyRate3", _prepaid.DebitCurrencyRate3);
                        cmd.Parameters.AddWithValue("@DebitAccount4", _prepaid.DebitAccount4);
                        cmd.Parameters.AddWithValue("@DebitAmount4", _prepaid.DebitAmount4);
                        cmd.Parameters.AddWithValue("@DebitCurrencyPk4", _prepaid.DebitCurrencyPk4);
                        cmd.Parameters.AddWithValue("@DebitCurrencyRate4", _prepaid.DebitCurrencyRate4);
                        cmd.Parameters.AddWithValue("@CreditAccount1", _prepaid.CreditAccount1);
                        cmd.Parameters.AddWithValue("@CreditAmount1", _prepaid.CreditAmount1);
                        cmd.Parameters.AddWithValue("@CreditCurrencyPk1", _prepaid.CreditCurrencyPk1);
                        cmd.Parameters.AddWithValue("@CreditCurrencyRate1", _prepaid.CreditCurrencyRate1);
                        cmd.Parameters.AddWithValue("@CreditAccount2", _prepaid.CreditAccount2);
                        cmd.Parameters.AddWithValue("@CreditAmount2", _prepaid.CreditAmount2);
                        cmd.Parameters.AddWithValue("@CreditCurrencyPk2", _prepaid.CreditCurrencyPk2);
                        cmd.Parameters.AddWithValue("@CreditCurrencyRate2", _prepaid.CreditCurrencyRate2);
                        cmd.Parameters.AddWithValue("@CreditAccount3", _prepaid.CreditAccount3);
                        cmd.Parameters.AddWithValue("@CreditAmount3", _prepaid.CreditAmount3);
                        cmd.Parameters.AddWithValue("@CreditCurrencyPk3", _prepaid.CreditCurrencyPk3);
                        cmd.Parameters.AddWithValue("@CreditCurrencyRate3", _prepaid.CreditCurrencyRate3);
                        cmd.Parameters.AddWithValue("@CreditAccount4", _prepaid.CreditAccount4);
                        cmd.Parameters.AddWithValue("@CreditAmount4", _prepaid.CreditAmount4);
                        cmd.Parameters.AddWithValue("@CreditCurrencyPk4", _prepaid.CreditCurrencyPk4);
                        cmd.Parameters.AddWithValue("@CreditCurrencyRate4", _prepaid.CreditCurrencyRate4);
                        cmd.Parameters.AddWithValue("@PrepaidExpAccount", _prepaid.PrepaidExpAccount);
                        cmd.Parameters.AddWithValue("@OfficePK", _prepaid.OfficePK);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _prepaid.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return new PrepaidAddNew()
                                {
                                    PrepaidPK = Convert.ToInt32(dr["newPrepaidPK"]),
                                    HistoryPK = Convert.ToInt64(dr["newHistoryPK"]),
                                    Message = "Insert Fixed Aset Success"
                                };
                            }
                            else
                            {
                                return null;
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
        public int Prepaid_Update(Prepaid _prepaid, bool _havePrivillege)
        {
            int _newHisPK;
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                int status = _host.Get_Status(_prepaid.PrepaidPK, _prepaid.HistoryPK, "Prepaid");
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {

                            cmd.CommandText = "Update Prepaid set status=2, Notes=@Notes,PeriodPK=@PeriodPK,PaymentDate=@PaymentDate,PeriodStartDate=@PeriodStartDate," +
                                "PeriodEndDate=@PeriodEndDate,PaymentJournalNo=@PaymentJournalNo,Reference=@Reference,Description=@Description, " +
                                "PrepaidAccount=@PrepaidAccount,PrepaidAmount=@PrepaidAmount,PrepaidCurrencyPK=@PrepaidCurrencyPK,PrepaidCurrencyRate=@PrepaidCurrencyRate, " +
                                "DebitAccount1=@DebitAccount1,DebitAmount1=@DebitAmount1,DebitCurrencyPk1=@DebitCurrencyPk1,DebitCurrencyRate1=@DebitCurrencyRate1, " +
                                "DebitAccount2=@DebitAccount2,DebitAmount2=@DebitAmount2,DebitCurrencyPk2=@DebitCurrencyPk2,DebitCurrencyRate2=@DebitCurrencyRate2, " +
                                "DebitAccount3=@DebitAccount3,DebitAmount3=@DebitAmount3,DebitCurrencyPk3=@DebitCurrencyPk3,DebitCurrencyRate3=@DebitCurrencyRate3, " +
                                "DebitAccount4=@DebitAccount4,DebitAmount4=@DebitAmount4,DebitCurrencyPk4=@DebitCurrencyPk4,DebitCurrencyRate4=@DebitCurrencyRate4, " +
                                "CreditAccount1=@CreditAccount1,CreditAmount1=@CreditAmount1,CreditCurrencyPk1=@CreditCurrencyPk1,CreditCurrencyRate1=@CreditCurrencyRate1, " +
                                "CreditAccount2=@CreditAccount2,CreditAmount2=@CreditAmount2,CreditCurrencyPk2=@CreditCurrencyPk2,CreditCurrencyRate2=@CreditCurrencyRate2, " +
                                "CreditAccount3=@CreditAccount3,CreditAmount3=@CreditAmount3,CreditCurrencyPk3=@CreditCurrencyPk3,CreditCurrencyRate3=@CreditCurrencyRate3, " +
                                "CreditAccount4=@CreditAccount4,CreditAmount4=@CreditAmount4,CreditCurrencyPk4=@CreditCurrencyPk4,CreditCurrencyRate4=@CreditCurrencyRate4,PrepaidExpAccount=@PrepaidExpAccount ,OfficePK=@OfficePK, " +   
                                "ApprovedUsersID=@ApprovedUsersID, " +
                                "ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime ,LastUpdate=@lastupdate " +
                                "where PrepaidPK = @PK and historyPK = @HistoryPK";

                            cmd.Parameters.AddWithValue("@HistoryPK", _prepaid.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _prepaid.PrepaidPK);
                            cmd.Parameters.AddWithValue("@Notes", _prepaid.Notes);
                            cmd.Parameters.AddWithValue("@PeriodPK", _prepaid.PeriodPK);
                            cmd.Parameters.AddWithValue("@PaymentDate", _prepaid.PaymentDate);
                            cmd.Parameters.AddWithValue("@PeriodStartDate", _prepaid.PeriodStartDate);
                            cmd.Parameters.AddWithValue("@PeriodEndDate", _prepaid.PeriodEndDate);
                            cmd.Parameters.AddWithValue("@PaymentJournalNo", _prepaid.PaymentJournalNo);
                            cmd.Parameters.AddWithValue("@Reference", _prepaid.Reference);
                            cmd.Parameters.AddWithValue("@Description", _prepaid.Description);
                            cmd.Parameters.AddWithValue("@PrepaidAccount", _prepaid.PrepaidAccount);
                            cmd.Parameters.AddWithValue("@PrepaidAmount", _prepaid.PrepaidAmount);
                            cmd.Parameters.AddWithValue("@PrepaidCurrencyPK", _prepaid.PrepaidCurrencyPK);
                            cmd.Parameters.AddWithValue("@PrepaidCurrencyRate", _prepaid.PrepaidCurrencyRate);
                            cmd.Parameters.AddWithValue("@DebitAccount1", _prepaid.DebitAccount1);
                            cmd.Parameters.AddWithValue("@DebitAmount1", _prepaid.DebitAmount1);
                            cmd.Parameters.AddWithValue("@DebitCurrencyPk1", _prepaid.DebitCurrencyPk1);
                            cmd.Parameters.AddWithValue("@DebitCurrencyRate1", _prepaid.DebitCurrencyRate1);
                            cmd.Parameters.AddWithValue("@DebitAccount2", _prepaid.DebitAccount2);
                            cmd.Parameters.AddWithValue("@DebitAmount2", _prepaid.DebitAmount2);
                            cmd.Parameters.AddWithValue("@DebitCurrencyPk2", _prepaid.DebitCurrencyPk2);
                            cmd.Parameters.AddWithValue("@DebitCurrencyRate2", _prepaid.DebitCurrencyRate2);
                            cmd.Parameters.AddWithValue("@DebitAccount3", _prepaid.DebitAccount3);
                            cmd.Parameters.AddWithValue("@DebitAmount3", _prepaid.DebitAmount3);
                            cmd.Parameters.AddWithValue("@DebitCurrencyPk3", _prepaid.DebitCurrencyPk3);
                            cmd.Parameters.AddWithValue("@DebitCurrencyRate3", _prepaid.DebitCurrencyRate3);
                            cmd.Parameters.AddWithValue("@DebitAccount4", _prepaid.DebitAccount4);
                            cmd.Parameters.AddWithValue("@DebitAmount4", _prepaid.DebitAmount4);
                            cmd.Parameters.AddWithValue("@DebitCurrencyPk4", _prepaid.DebitCurrencyPk4);
                            cmd.Parameters.AddWithValue("@DebitCurrencyRate4", _prepaid.DebitCurrencyRate4);
                            cmd.Parameters.AddWithValue("@CreditAccount1", _prepaid.CreditAccount1);
                            cmd.Parameters.AddWithValue("@CreditAmount1", _prepaid.CreditAmount1);
                            cmd.Parameters.AddWithValue("@CreditCurrencyPk1", _prepaid.CreditCurrencyPk1);
                            cmd.Parameters.AddWithValue("@CreditCurrencyRate1", _prepaid.CreditCurrencyRate1);
                            cmd.Parameters.AddWithValue("@CreditAccount2", _prepaid.CreditAccount2);
                            cmd.Parameters.AddWithValue("@CreditAmount2", _prepaid.CreditAmount2);
                            cmd.Parameters.AddWithValue("@CreditCurrencyPk2", _prepaid.CreditCurrencyPk2);
                            cmd.Parameters.AddWithValue("@CreditCurrencyRate2", _prepaid.CreditCurrencyRate2);
                            cmd.Parameters.AddWithValue("@CreditAccount3", _prepaid.CreditAccount3);
                            cmd.Parameters.AddWithValue("@CreditAmount3", _prepaid.CreditAmount3);
                            cmd.Parameters.AddWithValue("@CreditCurrencyPk3", _prepaid.CreditCurrencyPk3);
                            cmd.Parameters.AddWithValue("@CreditCurrencyRate3", _prepaid.CreditCurrencyRate3);
                            cmd.Parameters.AddWithValue("@CreditAccount4", _prepaid.CreditAccount4);
                            cmd.Parameters.AddWithValue("@CreditAmount4", _prepaid.CreditAmount4);
                            cmd.Parameters.AddWithValue("@CreditCurrencyPk4", _prepaid.CreditCurrencyPk4);
                            cmd.Parameters.AddWithValue("@CreditCurrencyRate4", _prepaid.CreditCurrencyRate4);
                            cmd.Parameters.AddWithValue("@PrepaidExpAccount", _prepaid.PrepaidExpAccount);
                            cmd.Parameters.AddWithValue("@OfficePK", _prepaid.OfficePK);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _prepaid.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _prepaid.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                            cmd.ExecuteNonQuery();
                        }

                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update Prepaid set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@lastupdate where PrepaidPK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _prepaid.PrepaidPK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _prepaid.EntryUsersID);
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
                                cmd.CommandText = "Update Prepaid set Notes=@Notes,PeriodPK=@PeriodPK,PaymentDate=@PaymentDate,PeriodStartDate=@PeriodStartDate," +
                                "PeriodEndDate=@PeriodEndDate,PaymentJournalNo=@PaymentJournalNo,Reference=@Reference,Description=@Description, " +
                                "PrepaidAccount=@PrepaidAccount,PrepaidAmount=@PrepaidAmount,PrepaidCurrencyPK=@PrepaidCurrencyPK,PrepaidCurrencyRate=@PrepaidCurrencyRate, " +
                                "DebitAccount1=@DebitAccount1,DebitAmount1=@DebitAmount1,DebitCurrencyPk1=@DebitCurrencyPk1,DebitCurrencyRate1=@DebitCurrencyRate1, " +
                                "DebitAccount2=@DebitAccount2,DebitAmount2=@DebitAmount2,DebitCurrencyPk2=@DebitCurrencyPk2,DebitCurrencyRate2=@DebitCurrencyRate2, " +
                                "DebitAccount3=@DebitAccount3,DebitAmount3=@DebitAmount3,DebitCurrencyPk3=@DebitCurrencyPk3,DebitCurrencyRate3=@DebitCurrencyRate3, " +
                                "DebitAccount4=@DebitAccount4,DebitAmount4=@DebitAmount4,DebitCurrencyPk4=@DebitCurrencyPk4,DebitCurrencyRate4=@DebitCurrencyRate4, " +
                                "CreditAccount1=@CreditAccount1,CreditAmount1=@CreditAmount1,CreditCurrencyPk1=@CreditCurrencyPk1,CreditCurrencyRate1=@CreditCurrencyRate1, " +
                                "CreditAccount2=@CreditAccount2,CreditAmount2=@CreditAmount2,CreditCurrencyPk2=@CreditCurrencyPk2,CreditCurrencyRate2=@CreditCurrencyRate2, " +
                                "CreditAccount3=@CreditAccount3,CreditAmount3=@CreditAmount3,CreditCurrencyPk3=@CreditCurrencyPk3,CreditCurrencyRate3=@CreditCurrencyRate3, " +
                                "CreditAccount4=@CreditAccount4,CreditAmount4=@CreditAmount4,CreditCurrencyPk4=@CreditCurrencyPk4,CreditCurrencyRate4=@CreditCurrencyRate4,PrepaidExpAccount=@PrepaidExpAccount, OfficePK=@OfficePK, " +   
                                "UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@lastupdate " +
                                "where PrepaidPK = @PK and historyPK = @HistoryPK";

                                cmd.Parameters.AddWithValue("@HistoryPK", _prepaid.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _prepaid.PrepaidPK);
                                cmd.Parameters.AddWithValue("@Notes", _prepaid.Notes);
                                cmd.Parameters.AddWithValue("@PeriodPK", _prepaid.PeriodPK);
                                cmd.Parameters.AddWithValue("@PaymentDate", _prepaid.PaymentDate);
                                cmd.Parameters.AddWithValue("@PeriodStartDate", _prepaid.PeriodStartDate);
                                cmd.Parameters.AddWithValue("@PeriodEndDate", _prepaid.PeriodEndDate);
                                cmd.Parameters.AddWithValue("@PaymentJournalNo", _prepaid.PaymentJournalNo);
                                cmd.Parameters.AddWithValue("@Reference", _prepaid.Reference);
                                cmd.Parameters.AddWithValue("@Description", _prepaid.Description);
                                cmd.Parameters.AddWithValue("@PrepaidAccount", _prepaid.PrepaidAccount);
                                cmd.Parameters.AddWithValue("@PrepaidAmount", _prepaid.PrepaidAmount);
                                cmd.Parameters.AddWithValue("@PrepaidCurrencyPK", _prepaid.PrepaidCurrencyPK);
                                cmd.Parameters.AddWithValue("@PrepaidCurrencyRate", _prepaid.PrepaidCurrencyRate);
                                cmd.Parameters.AddWithValue("@DebitAccount1", _prepaid.DebitAccount1);
                                cmd.Parameters.AddWithValue("@DebitAmount1", _prepaid.DebitAmount1);
                                cmd.Parameters.AddWithValue("@DebitCurrencyPk1", _prepaid.DebitCurrencyPk1);
                                cmd.Parameters.AddWithValue("@DebitCurrencyRate1", _prepaid.DebitCurrencyRate1);
                                cmd.Parameters.AddWithValue("@DebitAccount2", _prepaid.DebitAccount2);
                                cmd.Parameters.AddWithValue("@DebitAmount2", _prepaid.DebitAmount2);
                                cmd.Parameters.AddWithValue("@DebitCurrencyPk2", _prepaid.DebitCurrencyPk2);
                                cmd.Parameters.AddWithValue("@DebitCurrencyRate2", _prepaid.DebitCurrencyRate2);
                                cmd.Parameters.AddWithValue("@DebitAccount3", _prepaid.DebitAccount3);
                                cmd.Parameters.AddWithValue("@DebitAmount3", _prepaid.DebitAmount3);
                                cmd.Parameters.AddWithValue("@DebitCurrencyPk3", _prepaid.DebitCurrencyPk3);
                                cmd.Parameters.AddWithValue("@DebitCurrencyRate3", _prepaid.DebitCurrencyRate3);
                                cmd.Parameters.AddWithValue("@DebitAccount4", _prepaid.DebitAccount4);
                                cmd.Parameters.AddWithValue("@DebitAmount4", _prepaid.DebitAmount4);
                                cmd.Parameters.AddWithValue("@DebitCurrencyPk4", _prepaid.DebitCurrencyPk4);
                                cmd.Parameters.AddWithValue("@DebitCurrencyRate4", _prepaid.DebitCurrencyRate4);
                                cmd.Parameters.AddWithValue("@CreditAccount1", _prepaid.CreditAccount1);
                                cmd.Parameters.AddWithValue("@CreditAmount1", _prepaid.CreditAmount1);
                                cmd.Parameters.AddWithValue("@CreditCurrencyPk1", _prepaid.CreditCurrencyPk1);
                                cmd.Parameters.AddWithValue("@CreditCurrencyRate1", _prepaid.CreditCurrencyRate1);
                                cmd.Parameters.AddWithValue("@CreditAccount2", _prepaid.CreditAccount2);
                                cmd.Parameters.AddWithValue("@CreditAmount2", _prepaid.CreditAmount2);
                                cmd.Parameters.AddWithValue("@CreditCurrencyPk2", _prepaid.CreditCurrencyPk2);
                                cmd.Parameters.AddWithValue("@CreditCurrencyRate2", _prepaid.CreditCurrencyRate2);
                                cmd.Parameters.AddWithValue("@CreditAccount3", _prepaid.CreditAccount3);
                                cmd.Parameters.AddWithValue("@CreditAmount3", _prepaid.CreditAmount3);
                                cmd.Parameters.AddWithValue("@CreditCurrencyPk3", _prepaid.CreditCurrencyPk3);
                                cmd.Parameters.AddWithValue("@CreditCurrencyRate3", _prepaid.CreditCurrencyRate3);
                                cmd.Parameters.AddWithValue("@CreditAccount4", _prepaid.CreditAccount4);
                                cmd.Parameters.AddWithValue("@CreditAmount4", _prepaid.CreditAmount4);
                                cmd.Parameters.AddWithValue("@CreditCurrencyPk4", _prepaid.CreditCurrencyPk4);
                                cmd.Parameters.AddWithValue("@CreditCurrencyRate4", _prepaid.CreditCurrencyRate4);
                                cmd.Parameters.AddWithValue("@PrepaidExpAccount", _prepaid.PrepaidExpAccount);
                                cmd.Parameters.AddWithValue("@OfficePK", _prepaid.OfficePK);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _prepaid.EntryUsersID);
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
                                _newHisPK = _host.Get_NewHistoryPK(_prepaid.PrepaidPK, "Prepaid");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],LastUpdate)" +
                                   "Select @PK,@HistoryPK,1," + _paramaterCommand + "@EntryUsersID,@EntryTime,@LastUpdate";
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _host.Get_NewHistoryPK(_prepaid.PrepaidPK, "Prepaid"));
                                cmd.Parameters.AddWithValue("@PK", _prepaid.PrepaidPK);
                                cmd.Parameters.AddWithValue("@Notes", _prepaid.Notes);
                                cmd.Parameters.AddWithValue("@PeriodPK", _prepaid.PeriodPK);
                                cmd.Parameters.AddWithValue("@PaymentDate", _prepaid.PaymentDate);
                                cmd.Parameters.AddWithValue("@PeriodStartDate", _prepaid.PeriodStartDate);
                                cmd.Parameters.AddWithValue("@PeriodEndDate", _prepaid.PeriodEndDate);
                                cmd.Parameters.AddWithValue("@PaymentJournalNo", _prepaid.PaymentJournalNo);
                                cmd.Parameters.AddWithValue("@Reference", _prepaid.Reference);
                                cmd.Parameters.AddWithValue("@Description", _prepaid.Description);
                                cmd.Parameters.AddWithValue("@PrepaidAccount", _prepaid.PrepaidAccount);
                                cmd.Parameters.AddWithValue("@PrepaidAmount", _prepaid.PrepaidAmount);
                                cmd.Parameters.AddWithValue("@PrepaidCurrencyPK", _prepaid.PrepaidCurrencyPK);
                                cmd.Parameters.AddWithValue("@PrepaidCurrencyRate", _prepaid.PrepaidCurrencyRate);
                                cmd.Parameters.AddWithValue("@DebitAccount1", _prepaid.DebitAccount1);
                                cmd.Parameters.AddWithValue("@DebitAmount1", _prepaid.DebitAmount1);
                                cmd.Parameters.AddWithValue("@DebitCurrencyPk1", _prepaid.DebitCurrencyPk1);
                                cmd.Parameters.AddWithValue("@DebitCurrencyRate1", _prepaid.DebitCurrencyRate1);
                                cmd.Parameters.AddWithValue("@DebitAccount2", _prepaid.DebitAccount2);
                                cmd.Parameters.AddWithValue("@DebitAmount2", _prepaid.DebitAmount2);
                                cmd.Parameters.AddWithValue("@DebitCurrencyPk2", _prepaid.DebitCurrencyPk2);
                                cmd.Parameters.AddWithValue("@DebitCurrencyRate2", _prepaid.DebitCurrencyRate2);
                                cmd.Parameters.AddWithValue("@DebitAccount3", _prepaid.DebitAccount3);
                                cmd.Parameters.AddWithValue("@DebitAmount3", _prepaid.DebitAmount3);
                                cmd.Parameters.AddWithValue("@DebitCurrencyPk3", _prepaid.DebitCurrencyPk3);
                                cmd.Parameters.AddWithValue("@DebitCurrencyRate3", _prepaid.DebitCurrencyRate3);
                                cmd.Parameters.AddWithValue("@DebitAccount4", _prepaid.DebitAccount4);
                                cmd.Parameters.AddWithValue("@DebitAmount4", _prepaid.DebitAmount4);
                                cmd.Parameters.AddWithValue("@DebitCurrencyPk4", _prepaid.DebitCurrencyPk4);
                                cmd.Parameters.AddWithValue("@DebitCurrencyRate4", _prepaid.DebitCurrencyRate4);
                                cmd.Parameters.AddWithValue("@CreditAccount1", _prepaid.CreditAccount1);
                                cmd.Parameters.AddWithValue("@CreditAmount1", _prepaid.CreditAmount1);
                                cmd.Parameters.AddWithValue("@CreditCurrencyPk1", _prepaid.CreditCurrencyPk1);
                                cmd.Parameters.AddWithValue("@CreditCurrencyRate1", _prepaid.CreditCurrencyRate1);
                                cmd.Parameters.AddWithValue("@CreditAccount2", _prepaid.CreditAccount2);
                                cmd.Parameters.AddWithValue("@CreditAmount2", _prepaid.CreditAmount2);
                                cmd.Parameters.AddWithValue("@CreditCurrencyPk2", _prepaid.CreditCurrencyPk2);
                                cmd.Parameters.AddWithValue("@CreditCurrencyRate2", _prepaid.CreditCurrencyRate2);
                                cmd.Parameters.AddWithValue("@CreditAccount3", _prepaid.CreditAccount3);
                                cmd.Parameters.AddWithValue("@CreditAmount3", _prepaid.CreditAmount3);
                                cmd.Parameters.AddWithValue("@CreditCurrencyPk3", _prepaid.CreditCurrencyPk3);
                                cmd.Parameters.AddWithValue("@CreditCurrencyRate3", _prepaid.CreditCurrencyRate3);
                                cmd.Parameters.AddWithValue("@CreditAccount4", _prepaid.CreditAccount4);
                                cmd.Parameters.AddWithValue("@CreditAmount4", _prepaid.CreditAmount4);
                                cmd.Parameters.AddWithValue("@CreditCurrencyPk4", _prepaid.CreditCurrencyPk4);
                                cmd.Parameters.AddWithValue("@CreditCurrencyRate4", _prepaid.CreditCurrencyRate4);
                                cmd.Parameters.AddWithValue("@PrepaidExpAccount", _prepaid.PrepaidExpAccount);
                                cmd.Parameters.AddWithValue("@OfficePK", _prepaid.OfficePK);
                                cmd.Parameters.AddWithValue("@EntryUsersID", _prepaid.EntryUsersID);
                                cmd.Parameters.AddWithValue("@EntryTime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                                cmd.ExecuteNonQuery();

                            }
                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update Prepaid set status= 4,Notes=@Notes," +
                                    "UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@lastupdate where PrepaidPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _prepaid.Notes);
                                cmd.Parameters.AddWithValue("@PK", _prepaid.PrepaidPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _prepaid.HistoryPK);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _prepaid.EntryUsersID);
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
        public void Prepaid_Approved(Prepaid _prepaid)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update Prepaid set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,LastUpdate = @LastUpdate " +
                            "where PrepaidPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _prepaid.PrepaidPK);
                        cmd.Parameters.AddWithValue("@historyPK", _prepaid.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _prepaid.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update Prepaid set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where PrepaidPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _prepaid.PrepaidPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _prepaid.ApprovedUsersID);
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
        public void Prepaid_Reject(Prepaid _prepaid)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update Prepaid set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where PrepaidPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _prepaid.PrepaidPK);
                        cmd.Parameters.AddWithValue("@historyPK", _prepaid.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _prepaid.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update Prepaid set status= 2,LastUpdate=@LastUpdate where PrepaidPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _prepaid.PrepaidPK);
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
        public void Prepaid_Void(Prepaid _prepaid)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update Prepaid set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where PrepaidPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _prepaid.PrepaidPK);
                        cmd.Parameters.AddWithValue("@historyPK", _prepaid.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _prepaid.VoidUsersID);
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

        public void Prepaid_UnApproved(Prepaid _prepaid)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update Prepaid set status = 1,LastUpdate=@LastUpdate " +
                            "where PrepaidPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _prepaid.PrepaidPK);
                        cmd.Parameters.AddWithValue("@historyPK", _prepaid.HistoryPK);
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

        public List<Prepaid> Prepaid_SelectFromTo(int _status, string _type, DateTime _dateFrom, DateTime _dateTo)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<Prepaid> L_Prepaid = new List<Prepaid>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {

                            cmd.CommandText = " select PR.ID PeriodID,O.ID OfficeID,A1.ID DebitAccount1ID,C1.ID DebitCurrencyPk1ID, " +
                            " A2.ID DebitAccount2ID,C2.ID DebitCurrencyPk2ID, " +
                            " A3.ID DebitAccount3ID,C3.ID DebitCurrencyPk3ID, " +
                            " A4.ID DebitAccount4ID,C4.ID DebitCurrencyPk4ID, " +
                            " A5.ID CreditAccount1ID,C5.ID CreditCurrencyPk1ID, " +
                            " A6.ID CreditAccount2ID,C6.ID CreditCurrencyPk2ID, " +
                            " A7.ID CreditAccount3ID,C7.ID CreditCurrencyPk3ID, " +
                            " A8.ID CreditAccount4ID,C8.ID CreditCurrencyPk4ID, " +
                            " A9.ID PrepaidExpAccountID,A10.ID PrepaidAccountID,C10.ID PrepaidCurrencyID, P.* from Prepaid P Left join " +
                            " Period PR on P.PeriodPK = PR.PeriodPK and PR.Status = 2  Left join  " +
                            " Office O on P.OfficePK = O.OfficePK and O.Status = 2  Left join   " +
                            " Account A1 on P.DebitAccount1 = A1.AccountPK and A1.Status = 2  Left join   " +
                            " Currency C1 on P.DebitCurrencyPk1 = C1.CurrencyPK and C1.Status = 2  Left join   " +
                            " Account A2 on P.DebitAccount2 = A2.AccountPK and A2.Status = 2  Left join   " +
                            " Currency C2 on P.DebitCurrencyPk2 = C2.CurrencyPK and C2.Status = 2  Left join   " +
                            " Account A3 on P.DebitAccount3 = A3.AccountPK and A3.Status = 2  Left join   " +
                            " Currency C3 on P.DebitCurrencyPk3 = C3.CurrencyPK and C3.Status = 2  Left join   " +
                            " Account A4 on P.DebitAccount4 = A4.AccountPK and A4.Status = 2  Left join   " +
                            " Currency C4 on P.DebitCurrencyPk4 = C4.CurrencyPK and C4.Status = 2  Left join   " +
                            " Account A5 on P.CreditAccount1 = A5.AccountPK and A5.Status = 2  Left join   " +
                            " Currency C5 on P.CreditCurrencyPk1 = C5.CurrencyPK and C5.Status = 2  Left join   " +
                            " Account A6 on P.CreditAccount2 = A6.AccountPK and A6.Status = 2  Left join   " +
                            " Currency C6 on P.CreditCurrencyPk2 = C6.CurrencyPK and C6.Status = 2  Left join   " +
                            " Account A7 on P.CreditAccount3 = A7.AccountPK and A7.Status = 2  Left join   " +
                            " Currency C7 on P.CreditCurrencyPk3 = C7.CurrencyPK and C7.Status = 2  Left join   " +
                            " Account A8 on P.CreditAccount4 = A8.AccountPK and A8.Status = 2  Left join   " +
                            " Currency C8 on P.CreditCurrencyPk4 = C8.CurrencyPK and C8.Status = 2  Left join  " +
                            " Account A9 on P.PrepaidExpAccount = A9.AccountPK and A9.Status = 2 Left join   " +
                            " Account A10 on P.PrepaidAccount = A10.AccountPK and A10.Status = 2 Left join  " +
                            " Currency C10 on P.PrepaidCurrencyPK = C10.CurrencyPK and C10.Status = 2    " +
                            " Where  P.Status = @Status and PaymentDate between @DateFrom and @DateTo  ";
                            cmd.Parameters.AddWithValue("@Status", _status);
                            cmd.Parameters.AddWithValue("@Type", _type);
                            cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                            cmd.Parameters.AddWithValue("@DateTo", _dateTo);

                        }
                        else
                        {
                            cmd.CommandText = "Select * from Prepaid order by ID,Name";
                        }

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_Prepaid.Add(setPrepaid(dr));
                                }
                            }
                            return L_Prepaid;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }


        //Posting Prepaid
        public void Prepaid_Posting(Prepaid _prepaid)
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

                            cmd.CommandText = "Update Prepaid set Posted = 1 ," +
                             "PostedBy=@PostedBy,PostedTime=@PostedTime,LastUpdate=@lastupdate " +
                             "where PrepaidPK = @PK and historyPK = @HistoryPK";

                            cmd.Parameters.AddWithValue("@PK", _prepaid.PrepaidPK);
                            cmd.Parameters.AddWithValue("@HistoryPK", _prepaid.HistoryPK);
                            cmd.Parameters.AddWithValue("@PostedBy", _prepaid.EntryUsersID);
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

        //Revise Prepaid
        public void Prepaid_Revise(Prepaid _prepaid)
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

                            cmd.CommandText = "Update Prepaid set Revised = 1 ," +
                             "RevisedBy=@RevisedBy,RevisedTime=@RevisedTime,LastUpdate=@lastupdate " +
                             "where PrepaidPK = @PK and historyPK = @HistoryPK";

                            cmd.Parameters.AddWithValue("@PK", _prepaid.PrepaidPK);
                            cmd.Parameters.AddWithValue("@HistoryPK", _prepaid.HistoryPK);
                            cmd.Parameters.AddWithValue("@RevisedBy", _prepaid.EntryUsersID);
                            cmd.Parameters.AddWithValue("@RevisedTime", _dateTimeNow);
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

        ////Sell Prepaid
        //public void Prepaid_Sell(Prepaid _prepaid)
        //{

        //    try
        //    {
        //        DateTime _dateTimeNow = DateTime.Now;
        //        using (SqlConnection DbCon = new SqlConnection(Tools.conString))
        //        {
        //            DbCon.Open();
        //            using (SqlCommand cmd = DbCon.CreateCommand())
        //            {

        //                {

        //                    cmd.CommandText = "Update Prepaid set PeriodPK=@PeriodPK,CreditAccount1=@CreditAccount1,CreditAmount1=@CreditAmount1,CreditCurrencyPk1=@CreditCurrencyPk1,CreditCurrencyRate1=@CreditCurrencyRate1, " +
        //                        "CreditAccount2=@CreditAccount2,CreditAmount2=@CreditAmount2,CreditCurrencyPk2=@CreditCurrencyPk2,CreditCurrencyRate2=@CreditCurrencyRate2, " +
        //                        "CreditAccount3=@CreditAccount3,CreditAmount3=@CreditAmount3,CreditCurrencyPk3=@CreditCurrencyPk3,CreditCurrencyRate3=@CreditCurrencyRate3, " +
        //                        "CreditAccount4=@CreditAccount4,CreditAmount4=@CreditAmount4,CreditCurrencyPk4=@CreditCurrencyPk4,CreditCurrencyRate4=@CreditCurrencyRate4, OfficePK=@OfficePK, " +   
        //                        "UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@lastupdate " +
        //                        "where PrepaidPK = @PK and historyPK = @HistoryPK";

        //                    cmd.Parameters.AddWithValue("@PK", _prepaid.PrepaidPK);
        //                    cmd.Parameters.AddWithValue("@HistoryPK", _prepaid.HistoryPK);
        //                    cmd.Parameters.AddWithValue("@PeriodPK", _prepaid.PeriodPK);
        //                    cmd.Parameters.AddWithValue("@CreditAccount1", _prepaid.CreditAccount1);
        //                    cmd.Parameters.AddWithValue("@CreditAmount1", _prepaid.CreditAmount1);
        //                    cmd.Parameters.AddWithValue("@CreditCurrencyPk1", _prepaid.CreditCurrencyPk1);
        //                    cmd.Parameters.AddWithValue("@CreditCurrencyRate1", _prepaid.CreditCurrencyRate1);
        //                    cmd.Parameters.AddWithValue("@CreditAccount2", _prepaid.CreditAccount2);
        //                    cmd.Parameters.AddWithValue("@CreditAmount2", _prepaid.CreditAmount2);
        //                    cmd.Parameters.AddWithValue("@CreditCurrencyPk2", _prepaid.CreditCurrencyPk2);
        //                    cmd.Parameters.AddWithValue("@CreditCurrencyRate2", _prepaid.CreditCurrencyRate2);
        //                    cmd.Parameters.AddWithValue("@CreditAccount3", _prepaid.CreditAccount3);
        //                    cmd.Parameters.AddWithValue("@CreditAmount3", _prepaid.CreditAmount3);
        //                    cmd.Parameters.AddWithValue("@CreditCurrencyPk3", _prepaid.CreditCurrencyPk3);
        //                    cmd.Parameters.AddWithValue("@CreditCurrencyRate3", _prepaid.CreditCurrencyRate3);
        //                    cmd.Parameters.AddWithValue("@CreditAccount4", _prepaid.CreditAccount4);
        //                    cmd.Parameters.AddWithValue("@CreditAmount4", _prepaid.CreditAmount4);
        //                    cmd.Parameters.AddWithValue("@CreditCurrencyPk4", _prepaid.CreditCurrencyPk4);
        //                    cmd.Parameters.AddWithValue("@CreditCurrencyRate4", _prepaid.CreditCurrencyRate4);
        //                    cmd.Parameters.AddWithValue("@OfficePK", _prepaid.OfficePK);
        //                    cmd.Parameters.AddWithValue("@UpdateUsersID", _prepaid.EntryUsersID);
        //                    cmd.Parameters.AddWithValue("@Updatetime", _dateTimeNow);
        //                    cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);


        //                }
        //                cmd.ExecuteNonQuery();
        //            }
        //        }

        //    }
        //    catch (Exception err)
        //    {
        //        throw err;
        //    }
        //}

    }
}