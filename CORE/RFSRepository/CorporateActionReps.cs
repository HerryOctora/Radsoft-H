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
using System.Data;
using OfficeOpenXml.Drawing;


namespace RFSRepository
{
    public class CorporateActionReps
    {
        Host _host = new Host();

        //1
        string _insertCommand = "INSERT INTO [dbo].[CorporateAction] " +
                            "([CorporateActionPK],[HistoryPK],[Status],[Type],[ValueDate],[ExDate],[RecordingDate],[PaymentDate],[TaxDueDate],[ExpiredDate],[InstrumentPK],[Amount],[Price],[Earn],[Hold],";
        string _paramaterCommand = "@Type,@ValueDate,@ExDate,@RecordingDate,@PaymentDate,@TaxDueDate,@ExpiredDate,@InstrumentPK,@Amount,@Price,@Earn,@Hold,";

        //2
        private CorporateAction setCorporateAction(SqlDataReader dr)
        {
            CorporateAction M_corporateAction = new CorporateAction();
            M_corporateAction.CorporateActionPK = Convert.ToInt32(dr["CorporateActionPK"]);
            M_corporateAction.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_corporateAction.Status = Convert.ToInt32(dr["Status"]);
            M_corporateAction.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_corporateAction.Notes = Convert.ToString(dr["Notes"]);
            M_corporateAction.Type = Convert.ToInt32(dr["Type"]);
            M_corporateAction.TypeDesc = Convert.ToString(dr["TypeDesc"]);
            M_corporateAction.ValueDate = Convert.ToString(dr["ValueDate"]);
            M_corporateAction.ExDate = dr["ExDate"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["ExDate"]);
            M_corporateAction.RecordingDate = dr["RecordingDate"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["RecordingDate"]);
            M_corporateAction.PaymentDate = dr["PaymentDate"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["PaymentDate"]);
            M_corporateAction.TaxDueDate = dr["TaxDueDate"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["TaxDueDate"]);
            M_corporateAction.TaxDueDateDesc = dr["TaxDueDateDesc"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["TaxDueDateDesc"]);
            M_corporateAction.ExpiredDate = dr["ExpiredDate"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["ExpiredDate"]);
            M_corporateAction.InstrumentPK = Convert.ToInt32(dr["InstrumentPK"]);
            M_corporateAction.InstrumentID = Convert.ToString(dr["InstrumentID"]);
            M_corporateAction.InstrumentName = Convert.ToString(dr["InstrumentName"]);
            M_corporateAction.Amount = Convert.ToDecimal(dr["Amount"]);
            M_corporateAction.Price = Convert.ToDecimal(dr["Price"]);
            M_corporateAction.Earn = Convert.ToDecimal(dr["Earn"]);
            M_corporateAction.Hold = Convert.ToDecimal(dr["Hold"]);
            M_corporateAction.EntryUsersID = dr["EntryUsersID"].ToString();
            M_corporateAction.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_corporateAction.EntryUsersID = dr["EntryUsersID"].ToString();
            M_corporateAction.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_corporateAction.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_corporateAction.VoidUsersID = dr["VoidUsersID"].ToString();
            M_corporateAction.EntryTime = dr["EntryTime"].ToString();
            M_corporateAction.UpdateTime = dr["UpdateTime"].ToString();
            M_corporateAction.ApprovedTime = dr["ApprovedTime"].ToString();
            M_corporateAction.VoidTime = dr["VoidTime"].ToString();
            M_corporateAction.DBUserID = dr["DBUserID"].ToString();
            M_corporateAction.DBTerminalID = dr["DBTerminalID"].ToString();
            M_corporateAction.LastUpdate = dr["LastUpdate"].ToString();
            M_corporateAction.LastUpdateDB = dr["LastUpdateDB"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["LastUpdateDB"]);
            return M_corporateAction;
        }

        public List<CorporateAction> CorporateAction_SelectCorporateActionDateFromTo(int _status, DateTime _dateFrom, DateTime _dateTo)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<CorporateAction> L_corporateAction = new List<CorporateAction>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = @"select case when A.TaxDueDate = 1 then 'Tax Deducted Behind' else 'Tax Deducted Upfront' end TaxDueDateDesc, case when A.Status=1 then 'PENDING' else case when A.Status = 2 then 'APPROVED' else case when A.Status = 3 then 'VOID' else 'WAITING' end end end StatusDesc,C.ID InstrumentID,C.Name InstrumentName,D.DescOne TypeDesc,A.* from CorporateAction A 
left join Instrument C on A.InstrumentPK  = C.InstrumentPK and C.Status = 2
left join MasterValue D on A.Type = D.Code and D.ID = 'CorporateActionType' and D.Status = 2
where  A.status = @status and A.ValueDate between @DateFrom and @DateTo
                             ";
                            cmd.Parameters.AddWithValue("@Status", _status);
                            cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                            cmd.Parameters.AddWithValue("@DateTo", _dateTo);
                        }
                        else
                        {
                            cmd.CommandText = @"select case when A.TaxDueDate = 1 then 'Tax Deducted Behind' else 'Tax Deducted Upfront' end TaxDueDateDesc, case when A.Status=1 then 'PENDING' else case when A.Status = 2 then 'APPROVED' else case when A.Status = 3 then 'VOID' else 'WAITING' end end end StatusDesc,C.ID InstrumentID,C.Name InstrumentName,D.DescOne TypeDesc,A.* from CorporateAction A 
left join Instrument C on A.InstrumentPK  = C.InstrumentPK and C.Status = 2
left join MasterValue D on A.Type = D.Code and D.ID = 'CorporateActionType' and D.Status = 2
where  A.ValueDate between @DateFrom and @DateTo
                             ";
                            cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                            cmd.Parameters.AddWithValue("@DateTo", _dateTo);
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_corporateAction.Add(setCorporateAction(dr));
                                }
                            }
                            return L_corporateAction;
                        }
                    }
                }

            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int CorporateAction_Add(CorporateAction _corporateAction, bool _havePrivillege)
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
                                 "Select isnull(max(CorporateActionPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@LastUpdate From CorporateAction";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _corporateAction.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);
                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[LastUpdate])" +
                                "Select isnull(max(CorporateActionPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@LastUpdate From CorporateAction";
                        }
                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@Type", _corporateAction.Type);
                        cmd.Parameters.AddWithValue("@ValueDate", _corporateAction.ValueDate);
                        if (_corporateAction.ExDate == "" || _corporateAction.ExDate == null)
                        {
                            cmd.Parameters.AddWithValue("@ExDate", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@ExDate", _corporateAction.ExDate);
                        }
                        if (_corporateAction.RecordingDate == "" || _corporateAction.RecordingDate == null)
                        {
                            cmd.Parameters.AddWithValue("@RecordingDate", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@RecordingDate", _corporateAction.RecordingDate);
                        }
                        if (_corporateAction.PaymentDate == "" || _corporateAction.PaymentDate == null)
                        {
                            cmd.Parameters.AddWithValue("@PaymentDate", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@PaymentDate", _corporateAction.PaymentDate);
                        }
                        if (_corporateAction.TaxDueDate == 0 || _corporateAction.TaxDueDate == null)
                        {
                            cmd.Parameters.AddWithValue("@TaxDueDate", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@TaxDueDate", _corporateAction.TaxDueDate);
                        }
                        if (_corporateAction.ExpiredDate == "" || _corporateAction.ExpiredDate == null)
                        {
                            cmd.Parameters.AddWithValue("@ExpiredDate", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@ExpiredDate", _corporateAction.ExpiredDate);
                        }
                        cmd.Parameters.AddWithValue("@InstrumentPK", _corporateAction.InstrumentPK);
                        cmd.Parameters.AddWithValue("@Amount", _corporateAction.Amount);
                        cmd.Parameters.AddWithValue("@Price", _corporateAction.Price);
                        cmd.Parameters.AddWithValue("@Earn", _corporateAction.Earn);
                        cmd.Parameters.AddWithValue("@Hold", _corporateAction.Hold);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _corporateAction.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);

                        cmd.ExecuteNonQuery();

                        return _host.Get_LastPKByLastUpate(_dateTimeNow, "CorporateAction");
                    }
                }

            }
            catch (Exception err)
            {
                throw err;
            }

        }

        //6
        public int CorporateAction_Update(CorporateAction _corporateAction, bool _havePrivillege)
        {
            try
            {
                int _newHisPK;
                int status = _host.Get_Status(_corporateAction.CorporateActionPK, _corporateAction.HistoryPK, "CorporateAction");
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update CorporateAction set status=2, Notes=@Notes,Type=@Type,ValueDate=@ValueDate,ExDate=@ExDate,RecordingDate=@RecordingDate,PaymentDate=@PaymentDate,TaxDueDate=@TaxDueDate,ExpiredDate=@ExpiredDate," +
                                "InstrumentPK=@InstrumentPK,Amount=@Amount,Price=@Price,Earn=@Earn,Hold=@Hold," +
                                "ApprovedUsersID=@ApprovedUsersID, " +
                                "ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@LastUpdate " +
                                "where CorporateActionPK = @PK and historyPK = @HistoryPK";
                            cmd.Parameters.AddWithValue("@HistoryPK", _corporateAction.HistoryPK);
                            cmd.Parameters.AddWithValue("@Notes", _corporateAction.Notes);
                            cmd.Parameters.AddWithValue("@PK", _corporateAction.CorporateActionPK);
                            cmd.Parameters.AddWithValue("@Type", _corporateAction.Type);
                            cmd.Parameters.AddWithValue("@ValueDate", _corporateAction.ValueDate);
                            if (_corporateAction.ExDate == "" || _corporateAction.ExDate == null)
                            {
                                cmd.Parameters.AddWithValue("@ExDate", DBNull.Value);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@ExDate", _corporateAction.ExDate);
                            }
                            if (_corporateAction.RecordingDate == "" || _corporateAction.RecordingDate == null)
                            {
                                cmd.Parameters.AddWithValue("@RecordingDate", DBNull.Value);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@RecordingDate", _corporateAction.RecordingDate);
                            }
                            if (_corporateAction.PaymentDate == "" || _corporateAction.PaymentDate == null)
                            {
                                cmd.Parameters.AddWithValue("@PaymentDate", DBNull.Value);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@PaymentDate", _corporateAction.PaymentDate);
                            }
                            if (_corporateAction.TaxDueDate == 0 || _corporateAction.TaxDueDate == null)
                            {
                                cmd.Parameters.AddWithValue("@TaxDueDate", DBNull.Value);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@TaxDueDate", _corporateAction.TaxDueDate);
                            }
                            if (_corporateAction.ExpiredDate == "" || _corporateAction.ExpiredDate == null)
                            {
                                cmd.Parameters.AddWithValue("@ExpiredDate", DBNull.Value);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@ExpiredDate", _corporateAction.ExpiredDate);
                            }
                            cmd.Parameters.AddWithValue("@InstrumentPK", _corporateAction.InstrumentPK);
                            cmd.Parameters.AddWithValue("@Amount", _corporateAction.Amount);
                            cmd.Parameters.AddWithValue("@Price", _corporateAction.Price);
                            cmd.Parameters.AddWithValue("@Earn", _corporateAction.Earn);
                            cmd.Parameters.AddWithValue("@Hold", _corporateAction.Hold);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _corporateAction.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _dateTimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _corporateAction.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);
                            cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);

                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update CorporateAction set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@lastUpdate where CorporateActionPK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _corporateAction.CorporateActionPK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _corporateAction.EntryUsersID);
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
                                cmd.CommandText = "Update CorporateAction set Notes=@Notes,Type=@Type,ValueDate=@ValueDate,ExDate=@ExDate,RecordingDate=@RecordingDate,PaymentDate=@PaymentDate,TaxDueDate=@TaxDueDate,ExpiredDate=@ExpiredDate," +
                                "InstrumentPK=@InstrumentPK,Amount=@Amount,Price=@Price,Earn=@Earn,Hold=@Hold," +
                                "UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@LastUpdate " +
                                "where CorporateActionPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _corporateAction.HistoryPK);
                                cmd.Parameters.AddWithValue("@Notes", _corporateAction.Notes);
                                cmd.Parameters.AddWithValue("@PK", _corporateAction.CorporateActionPK);
                                cmd.Parameters.AddWithValue("@Type", _corporateAction.Type);
                                cmd.Parameters.AddWithValue("@ValueDate", _corporateAction.ValueDate);
                                if (_corporateAction.ExDate == "" || _corporateAction.ExDate == null)
                                {
                                    cmd.Parameters.AddWithValue("@ExDate", DBNull.Value);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@ExDate", _corporateAction.ExDate);
                                }
                                if (_corporateAction.RecordingDate == "" || _corporateAction.RecordingDate == null)
                                {
                                    cmd.Parameters.AddWithValue("@RecordingDate", DBNull.Value);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@RecordingDate", _corporateAction.RecordingDate);
                                }
                                if (_corporateAction.PaymentDate == "" || _corporateAction.PaymentDate == null)
                                {
                                    cmd.Parameters.AddWithValue("@PaymentDate", DBNull.Value);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@PaymentDate", _corporateAction.PaymentDate);
                                }
                                if (_corporateAction.TaxDueDate == 0 || _corporateAction.TaxDueDate == null)
                                {
                                    cmd.Parameters.AddWithValue("@TaxDueDate", DBNull.Value);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@TaxDueDate", _corporateAction.TaxDueDate);
                                }
                                if (_corporateAction.ExpiredDate == "" || _corporateAction.ExpiredDate == null)
                                {
                                    cmd.Parameters.AddWithValue("@ExpiredDate", DBNull.Value);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@ExpiredDate", _corporateAction.ExpiredDate);
                                }
                                cmd.Parameters.AddWithValue("@InstrumentPK", _corporateAction.InstrumentPK);
                                cmd.Parameters.AddWithValue("@Amount", _corporateAction.Amount);
                                cmd.Parameters.AddWithValue("@Price", _corporateAction.Price);
                                cmd.Parameters.AddWithValue("@Earn", _corporateAction.Earn);
                                cmd.Parameters.AddWithValue("@Hold", _corporateAction.Hold);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _corporateAction.EntryUsersID);
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
                                _newHisPK = _host.Get_NewHistoryPK(_corporateAction.CorporateActionPK, "CorporateAction");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                   "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From CorporateAction where CorporateActionPK =@PK and historyPK = @HistoryPK ";
                                cmd.Parameters.AddWithValue("@PK", _corporateAction.CorporateActionPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _corporateAction.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@Type", _corporateAction.Type);
                                cmd.Parameters.AddWithValue("@ValueDate", _corporateAction.ValueDate);
                                if (_corporateAction.ExDate == "" || _corporateAction.ExDate == null)
                                {
                                    cmd.Parameters.AddWithValue("@ExDate", DBNull.Value);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@ExDate", _corporateAction.ExDate);
                                }
                                if (_corporateAction.RecordingDate == "" || _corporateAction.RecordingDate == null)
                                {
                                    cmd.Parameters.AddWithValue("@RecordingDate", DBNull.Value);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@RecordingDate", _corporateAction.RecordingDate);
                                }
                                if (_corporateAction.PaymentDate == "" || _corporateAction.PaymentDate == null)
                                {
                                    cmd.Parameters.AddWithValue("@PaymentDate", DBNull.Value);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@PaymentDate", _corporateAction.PaymentDate);
                                }
                                if (_corporateAction.TaxDueDate == 0 || _corporateAction.TaxDueDate == null)
                                {
                                    cmd.Parameters.AddWithValue("@TaxDueDate", DBNull.Value);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@TaxDueDate", _corporateAction.TaxDueDate);
                                }
                                if (_corporateAction.ExpiredDate == "" || _corporateAction.ExpiredDate == null)
                                {
                                    cmd.Parameters.AddWithValue("@ExpiredDate", DBNull.Value);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@ExpiredDate", _corporateAction.ExpiredDate);
                                }
                                cmd.Parameters.AddWithValue("@InstrumentPK", _corporateAction.InstrumentPK);
                                cmd.Parameters.AddWithValue("@Amount", _corporateAction.Amount);
                                cmd.Parameters.AddWithValue("@Price", _corporateAction.Price);
                                cmd.Parameters.AddWithValue("@Earn", _corporateAction.Earn);
                                cmd.Parameters.AddWithValue("@Hold", _corporateAction.Hold);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _corporateAction.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _dateTimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update CorporateAction set status= 4,Notes=@Notes, " +
                                    " LastUpdate=@LastUpdate where CorporateActionPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _corporateAction.Notes);
                                cmd.Parameters.AddWithValue("@PK", _corporateAction.CorporateActionPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _corporateAction.HistoryPK);
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

        //7
        public void CorporateAction_Approved(CorporateAction _corporateAction)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update CorporateAction set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,LastUpdate = @LastUpdate " +
                            "where CorporateActionPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _corporateAction.CorporateActionPK);
                        cmd.Parameters.AddWithValue("@historyPK", _corporateAction.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _corporateAction.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update CorporateAction set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@lastUpdate where CorporateActionPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _corporateAction.CorporateActionPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _corporateAction.ApprovedUsersID);
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
        public void CorporateAction_Reject(CorporateAction _corporateAction)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update CorporateAction set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime, LastUpdate = @LastUpdate " +
                            "where CorporateActionPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _corporateAction.CorporateActionPK);
                        cmd.Parameters.AddWithValue("@historyPK", _corporateAction.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _corporateAction.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update CorporateAction set status= 2,LastUpdate=@lastUpdate where CorporateActionPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _corporateAction.CorporateActionPK);
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
        public void CorporateAction_Void(CorporateAction _corporateAction)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update CorporateAction set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate = @LastUpdate " +
                            "where CorporateActionPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _corporateAction.CorporateActionPK);
                        cmd.Parameters.AddWithValue("@historyPK", _corporateAction.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _corporateAction.VoidUsersID);
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

        //public void CorporateAction_Posting(CorporateAction _corporateAction)
        //{
        //    try
        //    {
        //        DateTime _dateTimeNow = DateTime.Now;
        //        using (SqlConnection DbCon = new SqlConnection(Tools.conString))
        //        {
        //            DbCon.Open();
        //            using (SqlCommand cmd = DbCon.CreateCommand())
        //            {
        //                cmd.CommandText =

        //                    "Declare @SubscriptionAcc int Declare @PayableSubsAcc int Declare @TotalCashAmount numeric(18,4) Declare @TotalFeeAmount numeric(18,4) Declare @PCashAmount numeric(18,4) " +
        //                    "Declare @ValueDate datetime Declare @PeriodPK int Declare @BankPK int Declare @BankCurrencyPK int Declare @FundClientID nvarchar(100) " +
        //                    "Declare @FundCashRefPK int Declare @FundJournalPK int \n " +
        //                    "Declare @CashAmount numeric (18,4) Declare @UnitAmount numeric(22,4) \n " +

        //                    // indasia dibuka
        //                    // RHB ditutup
        //                    //"Select @FundJournalPK = isnull(MAX(FundJournalPK) + 1,1) From FundJournal  \n " +
        //                    //"Select @PCashAmount = CashAmount,@TotalCashAmount = TotalCashAMount,@TotalFeeAmount = AgentFeeAmount + SubscriptionFeeAmount,@ValueDate =  PaymentDate, @FundCashRefPK = CashRefPK, @FundClientPK = @FundClientPK, " +
        //                    //"@FundClientID = B.ID From CorporateAction A Left join FundClient B on A.FundClientPK = B.FundClientPK and B.Status = 2 where CorporateActionPK = @CorporateActionPK and A.Status = 2 and Posted = 0 \n " +
        //                    //"Select @SubscriptionAcc = Subscription,@PayableSubsAcc = payablesubscriptionfee From Fundaccountingsetup where status = 2 \n " +
        //                    //"select @BankPK = FundJournalAccountPK,@BankCurrencyPK = A.CurrencyPK from FundCashRef A  where A.FundCashRefPK = @FundCashRefPK and A.status = 2 \n  " +
        //                    //"Select @PeriodPK = PeriodPK From Period Where @ValueDate Between DateFrom and DateTo and Status = 2 \n " +
        //                    //"INSERT INTO [dbo].[FundJournal] ([FundJournalPK],[HistoryPK],[Status],[Notes],[PeriodPK],[ValueDate],[Type],[TrxNo],[TrxName],[Reference] ,[Description],[Posted],[PostedBy],[PostedTime],[EntryUsersID],[EntryTime],[LastUpdate]) \n " +
        //                    //"Select	   @FundJournalPK, 1,2,'Posting From Subscription',@PeriodPK,@ValueDate,1,@CorporateActionPK,'SUBSCRIPTION', '','Subscription Client: ' + @FundClientID,1,@PostedBy,@PostedTime,@PostedBy,@PostedTime,@PostedTime \n " +
        //                    //"INSERT INTO [dbo].[FundJournalDetail]  ([FundJournalPK],[AutoNo],[HistoryPK],[Status],[FundJournalAccountPK],[CurrencyPK],[FundPK],[FundClientPK],[DetailDescription] ,[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate]) \n " +
        //                    //"Select		@FundJournalPK,1,1,2,@BankPK,@BankCurrencyPK,@FundPK,@FundClientPK,'Subscription Client: ' + @FundClientID,'D',@PCashAmount,@PCashAmount,0,1,@PCashAmount,0,@PostedTime \n " +
        //                    //"INSERT INTO [dbo].[FundJournalDetail]([FundJournalPK],[AutoNo],[HistoryPK],[Status],[FundJournalAccountPK],[CurrencyPK],[FundPK],[FundClientPK],[DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate]) \n " +
        //                    //"Select		@FundJournalPK,2,1,2,@SubscriptionAcc,1,@FundPK,@FundClientPK,'Subscription Client: ' + @FundClientID,'C',@TotalCashAmount,	0,@TotalCashAmount,1,0,@TotalCashAmount,@PostedTime \n " +
        //                    //"IF @TotalFeeAmount > 0 BEGIN \n " +
        //                    //"INSERT INTO [dbo].[FundJournalDetail]([FundJournalPK],[AutoNo],[HistoryPK],[Status],[FundJournalAccountPK],[CurrencyPK],[FundPK],[FundClientPK],[DetailDescription],[DebitCredit],[Amount],[Debit],[Credit],[CurrencyRate],[BaseDebit],[BaseCredit],[LastUpdate]) \n " +
        //                    //"Select	@FundJournalPK,3,1,2,@PayableSubsAcc,1,@FundPK,@FundClientPK,'Subscription Client: ' + @FundClientID,'C',@TotalFeeAmount,	0,@TotalFeeAmount,1,0,@TotalFeeAmount,@PostedTime \n END " +
        //                    //" \n " +
        //                    " select @CashAmount = CashAmount, @UnitAmount = UnitAmount from CorporateAction where CorporateActionPK = @CorporateActionPK and Status = 2 " +
        //                    "select * from FundClientPosition " +
        //                    "where Date = @Date and FundClientPK = @FundClientPK and FundPK = @FundPK " +
        //                    "if @@rowCount > 0 " +
        //                    "begin " +
        //                    "Update FundClientPosition set CashAmount = CashAmount + @CashAmount, " +
        //                    "UnitAmount = UnitAmount + @UnitAmount where Date = @Date and FundClientPK = @FundClientPK " +
        //                    "and FundPK = @FundPK " +
        //                    "end " +
        //                    "else " +
        //                    "begin " +
        //                    "Select * from FundClientPosition where Date <= @Date and year(date) = year(@Date)  " +
        //                    "and FundClientPK = @FundClientPK and FundPK = @FundPK " +
        //                    "if @@RowCount > 0 " +
        //                    "Begin " +
        //                    "INSERT INTO  FundClientPosition (Date,FundClientPK,FundPK,CashAmount,UnitAmount)  " +
        //                    "Select @Date,@FundClientPK,@FundPK,CashAmount + @CashAmount,UnitAmount + @UnitAmount From FundClientPosition " +
        //                    "Where Date = ( " +
        //                    "select MAX(Date) MaxDate from FundClientPosition where FundClientPK = @FundClientPK and FundPK = @FundPK and Date <= @Date " +
        //                    "and year(date) = year(@Date) " +
        //                    "and FundClientPK = @FundClientPK and FundPK = @FundPK ) End else Begin " +
        //                    "INSERT INTO  FundClientPosition (Date,FundClientPK,FundPK,CashAmount,UnitAmount) " +
        //                    "select @Date,@FundClientPK,@FundPK,@CashAmount,@UnitAmount  end end \n " +

        //                    "update CorporateAction " +
        //                    "set PostedBy = @PostedBy,PostedTime = @PostedTime,Posted = 1,Lastupdate = @PostedTime " +
        //                    "where CorporateActionPK = @CorporateActionPK and Status = 2 " +

        //                    " \n " +
        //                    "Declare @counterDate datetime \n " +
        //                    "set @counterDate = @Date \n " +
        //                    "while @counterDate < (select max(date) from fundClientPosition where FundClientPK = @FundClientPK and FundPK = @FundPK and year(date) = year(@Date)) " +
        //                    "BEGIN " +
        //                    "set @counterDate = dateadd(\"day\",1,@counterDate) \n " +
        //                    "update fundClientPosition set UnitAmount = UnitAmount + @UnitAmount,CashAmount = CashAmount + @CashAmount " +
        //                    "where FundClientPK = @FundClientPK and FundPK = @FundPK and Date = @counterDate end " +
        //                    " \n " +

        //                    "";
        //                cmd.Parameters.AddWithValue("@Date", _corporateAction.ValueDate);
        //                cmd.Parameters.AddWithValue("@FundPK", _corporateAction.FundPK);
        //                //cmd.Parameters.AddWithValue("@CashAmount", _corporateAction.TotalCashAmount);
        //                //cmd.Parameters.AddWithValue("@UnitAmount", _corporateAction.TotalUnitAmount);
        //                cmd.Parameters.AddWithValue("@PostedBy", _corporateAction.PostedBy);
        //                cmd.Parameters.AddWithValue("@PostedTime", _dateTimeNow);
        //                cmd.Parameters.AddWithValue("@CorporateActionPK", _corporateAction.CorporateActionPK);
        //                cmd.ExecuteNonQuery();
        //            }
        //        }
        //    }
        //    catch (Exception err)
        //    {
        //        throw err;
        //    }

        //}

        //public void CorporateAction_Revise(CorporateAction _corporateAction)
        //{

        //    try
        //    {
        //        DateTime _dateTimeNow = DateTime.Now;
        //        using (SqlConnection DbCon = new SqlConnection(Tools.conString))
        //        {
        //            DbCon.Open();
        //            using (SqlCommand cmd = DbCon.CreateCommand())
        //            {
        //                cmd.CommandText =
        //                "update FundJournal set Status = 3,VoidTime = @RevisedTime,VoidUsersID = @RevisedBy  where Type = 1 and TrxNo = 2 " +
        //                "and Posted = 1 \n " +
        //                "select * from FundClientPosition " +
        //                "where Date = @Date and FundClientPK = @FundClientPK and FundPK = @FundPK " +
        //                "if @@rowCount > 0 " +
        //                "begin " +
        //                "Update FundClientPosition set CashAmount = CashAmount - @CashAmount, " +
        //                "UnitAmount = UnitAmount - @UnitAmount where Date = @Date and FundClientPK = @FundClientPK " +
        //                "and FundPK = @FundPK " +
        //                "end " +
        //                " \n " +
        //                " Declare @MaxCorporateActionPK int  " +
        //                " \n " +
        //                " Select @MaxCorporateActionPK = ISNULL(MAX(CorporateActionPK),0) + 1 From CorporateAction   " +
        //                " INSERT INTO [dbo].[CorporateAction]  " +
        //                " ([CorporateActionPK],[HistoryPK] ,[Status],[Notes], [NAVDate] ,[ValueDate]," +
        //                " [PaymentDate], [NAV] ,[FundPK], [FundClientPK] , [CashRefPK] , [Description] ," +
        //                " [CashAmount] ,[UnitAmount] ,[TotalCashAMount] ,[TotalUnitAmount] ,[SubscriptionFeePercent] ,[SubscriptionFeeAmount] ," +
        //                " [AgentPK] ,[AgentFeePercent],[AgentFeeAmount],[CurrencyPK]," +
        //                " [EntryUsersID],[EntryTime],[LastUpdate])" +
        //                " \n " +
        //                " SELECT @MaxCorporateActionPK,1,1,'Pending Revised' ,[NAVDate] ," +
        //                " [ValueDate],[PaymentDate],[NAV] ,[FundPK],[FundClientPK] ," +
        //                " [CashRefPK] ,[Description] ,[CashAmount] ,[UnitAmount] ,[TotalCashAMount] ,[TotalUnitAmount] ," +
        //                " [SubscriptionFeePercent] ,[SubscriptionFeeAmount] ,[AgentPK] ,[AgentFeePercent],[AgentFeeAmount],[CurrencyPK]," +
        //                " [EntryUsersID],[EntryTime] , @RevisedTime " +
        //                " FROM CorporateAction   " +
        //                " WHERE CorporateActionPK = @CorporateActionPK   and status = 2 and posted = 1  " +
        //                " \n " +
        //                " \n " +
        //                "update CorporateAction " +
        //                "set RevisedBy = @RevisedBy,RevisedTime = @RevisedTime,Revised = 1 , status = 3 " +
        //                "where CorporateActionPK = @CorporateActionPK and Status = 2 and posted = 1 " +
        //                " \n " +
        //                "Declare @counterDate datetime \n " +
        //                "set @counterDate = @Date \n " +
        //                "while @counterDate < (select max(date) from fundClientPosition where FundClientPK = @FundClientPK and FundPK = @FundPK and year(date) = year(@Date)) " +
        //                "BEGIN " +
        //                "set @counterDate = dateadd(\"day\",1,@counterDate) \n " +
        //                "update fundClientPosition set UnitAmount = UnitAmount - @UnitAmount,CashAmount = CashAmount - @CashAmount " +
        //                "where FundClientPK = @FundClientPK and FundPK = @FundPK and Date = @counterDate end " +
        //                "";
        //                cmd.Parameters.AddWithValue("@Date", _corporateAction.ValueDate);
        //                cmd.Parameters.AddWithValue("@FundClientPK", _corporateAction.FundClientPK);
        //                cmd.Parameters.AddWithValue("@FundPK", _corporateAction.FundPK);
        //                cmd.Parameters.AddWithValue("@CashAmount", _corporateAction.TotalCashAmount);
        //                cmd.Parameters.AddWithValue("@UnitAmount", _corporateAction.TotalUnitAmount);
        //                cmd.Parameters.AddWithValue("@RevisedBy", _corporateAction.RevisedBy);
        //                cmd.Parameters.AddWithValue("@RevisedTime", _dateTimeNow);
        //                cmd.Parameters.AddWithValue("@CorporateActionPK", _corporateAction.CorporateActionPK);
        //                cmd.ExecuteNonQuery();
        //            }
        //        }
        //    }



        //    catch (Exception err)
        //    {
        //        throw err;
        //    }

        //}


        public bool Validate_CheckRights(int _instrumentPK, int _type)
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
                            Declare @ID nvarchar(50)
                            select @ID = ID from Instrument where status = 2 and InstrumentPK = @InstrumentPK
                            IF EXISTS(select * from instrument where InstrumentTypePK = @InstrumentTypePK and ID like '%' + @ID + '%')
                            BEGIN
	                            select 1 Result
                            END
                            ELSE
                            BEGIN
	                            select 0 Result
                            END
                           ";

                        cmd.Parameters.AddWithValue("@InstrumentPK", _instrumentPK);
                        if (_type == 3)
                        {
                            cmd.Parameters.AddWithValue("@InstrumentTypePK", 4);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@InstrumentTypePK", 16);
                        }

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


        public int Generate_ForAcc(CorporateAction _corporateAction)
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
                            
Create Table #ZDividenSaham                  
(                  
InstrumentPK int,     
FundPK int,                  
LastVolume numeric(18,4)     
)   


CREATE TABLE #AccPosition
(
FundPK int,
InstrumentPK int,
Balance numeric(19,2),
Date datetime
)


insert into #AccPosition(FundPK,InstrumentPK,Balance,Date)
Select FundPK,A.InstrumentPK,isnull(sum(A.BuyVolume) - sum(A.SellVolume),0) Balance,dbo.fworkingday(@Date,-1) from (

select 9999 FundPK,InstrumentPK,sum(Volume) BuyVolume,0 SellVolume from trxPortfolio

where ValueDate < @Date and Posted = 1 and InstrumentTypePK = 1 and trxType = 1 and status = 2 and Revised = 0

Group By InstrumentPK

UNION ALL

select 9999,InstrumentPK,0 BuyVolume,sum(Volume) SellVolume from trxPortfolio

where ValueDate < @Date and Posted = 1 and InstrumentTypePK = 1 and trxType = 2 and status = 2 and Revised = 0

Group By InstrumentPK

)A

Group By A.FundPK,A.InstrumentPK

having sum(A.BuyVolume) - sum(A.SellVolume) > 0



-- CORPORATE ACTION STOCK SPLIT / REVERSE STOCK SPLIT
	                    
delete CorporateActionResult where Date = @date and FundPK = 9999



-- Tarik Balance Cum / Valuedate - 1 + movement dengan batas settleddate <= recordingDate and ValueDate >= CumDate 
Insert into #ZDividenSaham(FundPK,InstrumentPK,LastVolume)
Select  B.FundPK,B.InstrumentPK,B.Balance + isnull(C.BalanceFromInv,0) LastBalance
From CorporateAction A 
left join #AccPosition B on A.InstrumentPK = B.InstrumentPK 
and B.Date = dbo.fworkingday(A.ValueDate,-1)
Left join (
	select sum(Case when TrxType = 1 then Volume else Volume * -1 end) BalanceFromInv,9999 FundPK, InstrumentPK
	, SettledDate, ValueDate 
	from TrxPortfolio where Status = 2 and Posted = 1 and Revised = 0
	and InstrumentTypePK = 1  
	Group by InstrumentPK,SettledDate,ValueDate
)C on   C.SettledDate <= A.ExDate and B.FundPK = C.FundPK and B.InstrumentPK = C.InstrumentPK
and C.ValueDate >= A.ValueDate
where A.Type = 4 and A.Status = 2 and A.ExDate = @date


Insert into #ZDividenSaham(FundPK,InstrumentPK,LastVolume)
Select B.FundPK,B.InstrumentPK,B.BalanceFromInv
from CorporateAction A
Left join (
	select sum(Case when TrxType = 1 then Volume else Volume * -1 end) BalanceFromInv,9999 FundPK, InstrumentPK
	, SettledDate, ValueDate 
	from TrxPortfolio where Status = 2 and Posted = 1 and Revised = 0
	and InstrumentTypePK = 1  
	Group by InstrumentPK,SettledDate,ValueDate
)B on  B.SettledDate <= A.ExDate and  A.InstrumentPK = B.InstrumentPK
and B.ValueDate >= A.ValueDate
left join #ZDividenSaham C on B.FundPK = C.FundPK and B.InstrumentPK = C.InstrumentPK 
where A.Type = 4 and A.Status = 2 and A.ExDate = @date
and C.FundPK is null and C.InstrumentPK is null
	
Insert into CorporateActionResult(CorporateActionPK,Status,Date,FundPK,InstrumentPK,Balance,Price)
Select A.CorporateActionPK,2,A.ExDate,B.FundPK,A.InstrumentPK,isnull((B.LastVolume/A.Hold * A.Earn) - B.LastVolume,0) DividenSaham,A.Price
from CorporateAction A left join #ZDividenSaham B on A.InstrumentPK = B.InstrumentPK
where A.Type = 4 and A.Status = 2 and A.ExDate = @date and B.FundPK = 9999

--select * from #AccPosition where instrumentPk = 956 --12014000.00
--select * from CorporateActionResult

---- Tarik Balance Cum / Valuedate - 1 + movement dengan batas settleddate <= recordingDate and ValueDate >= CumDate 
--Insert into #ZDividenSaham(FundPK,InstrumentPK,LastVolume)
--Select  B.FundPK,B.InstrumentPK,B.Balance + isnull(C.BalanceFromInv,0) LastBalance
--From CorporateAction A 
--left join FundPosition B on A.InstrumentPK = B.InstrumentPK 
--and B.Date = dbo.fworkingday(A.ValueDate,-1) and B.status = 2
--Left join (
--	select sum(Case when TrxType = 1 then DoneVolume else DoneVolume * -1 end) BalanceFromInv,FundPK, InstrumentPK
--	, SettlementDate, ValueDate 
--	from Investment where statusSettlement = 2
--	and InstrumentTypePK = 1  and FundPK = @CFundPK 
--	Group by InstrumentPK,FundPK,SettlementDate,ValueDate
--)C on   C.SettlementDate <= A.ExDate and B.FundPK = C.FundPK and B.InstrumentPK = C.InstrumentPK
--and C.ValueDate >= A.ValueDate
--where A.Type = 7 and A.Status = 2 and A.ExDate = @ValueDate

--Insert into #ZDividenSaham(FundPK,InstrumentPK,LastVolume)
--Select B.FundPK,B.InstrumentPK,B.BalanceFromInv
--from CorporateAction A
--Left join (
--	select sum(Case when TrxType = 1 then DoneVolume else DoneVolume * -1 end) BalanceFromInv,FundPK, InstrumentPK
--	, SettlementDate, ValueDate 
--	from Investment where statusSettlement = 2
--	and InstrumentTypePK = 1  and FundPK = @CFundPK 
--	Group by InstrumentPK,FundPK,SettlementDate,ValueDate
--)B on  B.SettlementDate <= A.ExDate and  A.InstrumentPK = B.InstrumentPK
--and B.ValueDate >= A.ValueDate
--left join #ZDividenSaham C on B.FundPK = C.FundPK and B.InstrumentPK = C.InstrumentPK 
--where A.Type = 7 and A.Status = 2 and A.ExDate = @ValueDate
--and C.FundPK is null and C.InstrumentPK is null
	
--Insert into CorporateActionResult(CorporateActionPK,Status,Date,FundPK,InstrumentPK,Balance,Price)
--Select A.CorporateActionPK,2,A.ExDate,B.FundPK,A.InstrumentPK,isnull((B.LastVolume/A.Hold * A.Earn) - B.LastVolume,0) DividenSaham,A.Price
--from CorporateAction A left join #ZDividenSaham B on A.InstrumentPK = B.InstrumentPK
--where A.Type = 7 and A.Status = 2 and A.ExDate = @ValueDate  and B.FundPK = @CFundPK

---- Tarik Balance Cum / Valuedate - 1 + movement dengan batas settleddate <= recordingDate and ValueDate >= CumDate 
--Insert into #ZDividenSaham(FundPK,InstrumentPK,LastVolume)
--Select  B.FundPK,B.InstrumentPK,B.Balance + isnull(C.BalanceFromInv,0) LastBalance
--From CorporateAction A 
--left join FundPosition B on A.InstrumentPK = B.InstrumentPK 
--and B.Date = dbo.fworkingday(A.ValueDate,-1) and B.status = 2
--Left join (
--	select sum(Case when TrxType = 1 then DoneVolume else DoneVolume * -1 end) BalanceFromInv,FundPK, InstrumentPK
--	, SettlementDate, ValueDate 
--	from Investment where statusSettlement = 2
--	and InstrumentTypePK = 1  and FundPK = @CFundPK 
--	Group by InstrumentPK,FundPK,SettlementDate,ValueDate
--)C on   C.SettlementDate <= A.RecordingDate and B.FundPK = C.FundPK and B.InstrumentPK = C.InstrumentPK
--and C.ValueDate >= A.ValueDate
--where A.Type = 2 and A.Status = 2 and A.ValueDate = @ValueDate

--Insert into #ZDividenSaham(FundPK,InstrumentPK,LastVolume)
--Select B.FundPK,B.InstrumentPK,B.BalanceFromInv
--from CorporateAction A
--Left join (
--	select sum(Case when TrxType = 1 then DoneVolume else DoneVolume * -1 end) BalanceFromInv,FundPK, InstrumentPK
--	, SettlementDate, ValueDate 
--	from Investment where statusSettlement = 2
--	and InstrumentTypePK = 1  and FundPK = @CFundPK 
--	Group by InstrumentPK,FundPK,SettlementDate,ValueDate
--)B on  B.SettlementDate <= A.RecordingDate and  A.InstrumentPK = B.InstrumentPK
--and B.ValueDate >= A.ValueDate
--left join #ZDividenSaham C on B.FundPK = C.FundPK and B.InstrumentPK = C.InstrumentPK 
--where A.Type = 2 and A.Status = 2 and A.ValueDate = @ValueDate
--and C.FundPK is null and C.InstrumentPK is null
	
--Insert into CorporateActionResult(CorporateActionPK,Status,Date,FundPK,InstrumentPK,Balance,Price)
--Select A.CorporateActionPK,2,A.ValueDate,B.FundPK,A.InstrumentPK,B.LastVolume / A.Hold * A.Earn DividenSaham,A.Price
--from CorporateAction A left join #ZDividenSaham B on A.InstrumentPK = B.InstrumentPK
--where A.Type = 2 and A.Status = 2 and A.ValueDate = @ValueDate and B.FundPK = @CFundPK


---- CORPORATE ACTION DIVIDEN RIGHTS
--truncate table #ZDividenSaham
--Insert into #ZDividenSaham(FundPK,InstrumentPK,LastVolume)
--Select  B.FundPK,B.InstrumentPK,B.Balance + isnull(C.BalanceFromInv,0) LastBalance
--From CorporateAction A 
--left join FundPosition B on A.InstrumentPK = B.InstrumentPK 
--and B.Date = dbo.fworkingday(A.ValueDate,-1) and B.status = 2
--Left join (
--	select sum(Case when TrxType = 1 then DoneVolume else DoneVolume * -1 end) BalanceFromInv,FundPK, InstrumentPK
--	, SettlementDate, ValueDate 
--	from Investment where statusSettlement = 2
--	and InstrumentTypePK = 1  and FundPK = @CFundPK 
--	Group by InstrumentPK,FundPK,SettlementDate,ValueDate
--)C on   C.SettlementDate <= A.RecordingDate and B.FundPK = C.FundPK and B.InstrumentPK = C.InstrumentPK
--and C.ValueDate >= A.ValueDate
--where A.Type = 3 and A.Status = 2 and A.PaymentDate = @ValueDate

--Insert into #ZDividenSaham(FundPK,InstrumentPK,LastVolume)
--Select B.FundPK,B.InstrumentPK,B.BalanceFromInv
--from CorporateAction A
--Left join (
--	select sum(Case when TrxType = 1 then DoneVolume else DoneVolume * -1 end) BalanceFromInv,FundPK, InstrumentPK
--	, SettlementDate, ValueDate 
--	from Investment where statusSettlement = 2
--	and InstrumentTypePK = 1  and FundPK = @CFundPK 
--	Group by InstrumentPK,FundPK,SettlementDate,ValueDate
--)B on  B.SettlementDate <= A.RecordingDate and  A.InstrumentPK = B.InstrumentPK
--and B.ValueDate >= A.ValueDate
--left join #ZDividenSaham C on B.FundPK = C.FundPK and B.InstrumentPK = C.InstrumentPK 
--where A.Type = 3 and A.Status = 2 and A.PaymentDate = @ValueDate
--and C.FundPK is null and C.InstrumentPK is null

--Insert into CorporateActionResult(CorporateActionPK,Status,Date,FundPK,InstrumentPK,Balance,Price)
--Select A.CorporateActionPK,2,A.PaymentDate,isnull(B.FundPK,0),isnull(D.InstrumentPK,0),isnull(B.LastVolume / A.Hold * A.Earn,0) DividenSaham,A.Price
--from CorporateAction A 
--left join #ZDividenSaham B on A.InstrumentPK = B.InstrumentPK
--left join Instrument C on A.InstrumentPK = C.InstrumentPK and C.status = 2
--left join Instrument D on D.ID = C.ID + '-R' and D.status = 2
--where A.Type = 3 and A.Status = 2 and A.PaymentDate = @ValueDate and B.FundPK = @CFundPK


---- CORPORATE ACTION DIVIDEN WARRANT
--truncate table #ZDividenSaham
--Insert into #ZDividenSaham(FundPK,InstrumentPK,LastVolume)
--Select  B.FundPK,B.InstrumentPK,B.Balance + isnull(C.BalanceFromInv,0) LastBalance
--From CorporateAction A 
--left join FundPosition B on A.InstrumentPK = B.InstrumentPK 
--and B.Date = dbo.fworkingday(A.ValueDate,-1) and B.status = 2
--Left join (
--	select sum(Case when TrxType = 1 then DoneVolume else DoneVolume * -1 end) BalanceFromInv,FundPK, InstrumentPK
--	, SettlementDate, ValueDate 
--	from Investment where statusSettlement = 2
--	and InstrumentTypePK = 1  and FundPK = @CFundPK 
--	Group by InstrumentPK,FundPK,SettlementDate,ValueDate
--)C on   C.SettlementDate <= A.RecordingDate and B.FundPK = C.FundPK and B.InstrumentPK = C.InstrumentPK
--and C.ValueDate >= A.ValueDate
--where A.Type = 5 and A.Status = 2 and A.PaymentDate = @ValueDate

--Insert into #ZDividenSaham(FundPK,InstrumentPK,LastVolume)
--Select B.FundPK,B.InstrumentPK,B.BalanceFromInv
--from CorporateAction A
--Left join (
--	select sum(Case when TrxType = 1 then DoneVolume else DoneVolume * -1 end) BalanceFromInv,FundPK, InstrumentPK
--	, SettlementDate, ValueDate 
--	from Investment where statusSettlement = 2
--	and InstrumentTypePK = 1  and FundPK = @CFundPK 
--	Group by InstrumentPK,FundPK,SettlementDate,ValueDate
--)B on  B.SettlementDate <= A.RecordingDate and  A.InstrumentPK = B.InstrumentPK
--and B.ValueDate >= A.ValueDate
--left join #ZDividenSaham C on B.FundPK = C.FundPK and B.InstrumentPK = C.InstrumentPK 
--where A.Type = 5 and A.Status = 2 and A.PaymentDate = @ValueDate
--and C.FundPK is null and C.InstrumentPK is null

--Insert into CorporateActionResult(CorporateActionPK,Status,Date,FundPK,InstrumentPK,Balance,Price)
--Select A.CorporateActionPK,2,A.PaymentDate,isnull(B.FundPK,0),isnull(D.InstrumentPK,0),isnull(B.LastVolume / A.Hold * A.Earn,0) DividenSaham,A.Price
--from CorporateAction A 
--left join #ZDividenSaham B on A.InstrumentPK = B.InstrumentPK
--left join Instrument C on A.InstrumentPK = C.InstrumentPK and C.status = 2
--left join Instrument D on D.ID = C.ID + '-W' and D.status = 2
--where A.Type = 5 and A.Status = 2 and A.PaymentDate = @ValueDate and B.FundPK = @CFundPK


---- PROSES EXERCISE YANG DAH DI DISTRIBUTION DATE
--Insert into CorporateActionResult(CorporateActionPK,Status,Date,FundPK,InstrumentPK,Balance,Price)
--Select ExercisePK,2,@ValueDate,FundPK,InstrumentPK,BalanceExercise,Price from Exercise 
--where DistributionDate  = @ValueDate and status = 2 and FundPK = @CFundPK


---- CORPORATE ACTION BOND AMORTIZEN
--TRUNCATE TABLE #ZDividenSaham
--Insert into #ZDividenSaham(FundPK,InstrumentPK,LastVolume)
--Select  B.FundPK,B.InstrumentPK,B.Balance  LastBalance
----B.Balance + C.BalanceFromInv LastBalance
--From CorporateAction A 
--left join FundPosition B on A.InstrumentPK = B.InstrumentPK 
--and B.Date = dbo.fworkingday(A.ValueDate,-1) and B.status = 2

--where A.Type = 6 and A.Status = 2 and A.PaymentDate = @ValueDate


	
--Insert into CorporateActionResult(CorporateActionPK,Status,Date,FundPK,InstrumentPK,Balance,Price)
--Select A.CorporateActionPK,2,A.PaymentDate,B.FundPK,A.InstrumentPK,B.LastVolume * A.Earn / A.Hold * -1,0
--from CorporateAction A left join #ZDividenSaham B on A.InstrumentPK = B.InstrumentPK
--where A.Type = 6 and A.Status = 2 and A.PaymentDate = @ValueDate and B.FundPK = @CFundPK
   

IF EXISTS(select * from CorporateActionResult where FundPK = 9999 and Date = @Date)
BEGIN
select 1 Result
END
ELSE 
BEGIN
select 0 Result
END 
                           ";

                        cmd.Parameters.AddWithValue("@Date", _corporateAction.DateTo);


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