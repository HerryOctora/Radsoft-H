using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RFSModel;
using RFSUtility;
using System.Data.SqlClient;

namespace RFSRepository
{
    public class RKAPAccountingReps
    {
        Host _host = new Host();

        string _insertCommand = "INSERT INTO [dbo].[RKAP_Accounting] " +
                           "([RKAP_AccountingPK],[HistoryPK],[Status],[ExcelRow],[Amount],[PeriodPK],[Type],";
        string _paramaterCommand = "@ExcelRow,@Amount,@PeriodPK,@Type,";

        private RKAPAccounting setRKAPAccounting(SqlDataReader dr)
        {
            RKAPAccounting M_RKAPAccounting = new RKAPAccounting();
            M_RKAPAccounting.RKAP_AccountingPK = Convert.ToInt32(dr["RKAP_AccountingPK"]);
            M_RKAPAccounting.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_RKAPAccounting.Status = Convert.ToInt32(dr["Status"]);
            M_RKAPAccounting.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_RKAPAccounting.Notes = Convert.ToString(dr["Notes"]);
            M_RKAPAccounting.ExcelRow = Convert.ToInt32(dr["ExcelRow"]);
            M_RKAPAccounting.Amount = Convert.ToDecimal(dr["Amount"]);
            M_RKAPAccounting.PeriodPK = Convert.ToInt32(dr["PeriodPK"]);
            M_RKAPAccounting.PeriodID = Convert.ToString(dr["PeriodID"]);
            M_RKAPAccounting.Type = Convert.ToInt32(dr["Type"]);
            M_RKAPAccounting.TypeDesc = Convert.ToString(dr["TypeDesc"]);
            M_RKAPAccounting.EntryUsersID = dr["EntryUsersID"].ToString();
            M_RKAPAccounting.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_RKAPAccounting.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_RKAPAccounting.VoidUsersID = dr["VoidUsersID"].ToString();
            M_RKAPAccounting.EntryTime = dr["EntryTime"].ToString();
            M_RKAPAccounting.UpdateTime = dr["UpdateTime"].ToString();
            M_RKAPAccounting.ApprovedTime = dr["ApprovedTime"].ToString();
            M_RKAPAccounting.VoidTime = dr["VoidTime"].ToString();
            M_RKAPAccounting.DBUserID = dr["DBUserID"].ToString();
            M_RKAPAccounting.DBTerminalID = dr["DBTerminalID"].ToString();
            M_RKAPAccounting.LastUpdate = dr["LastUpdate"].ToString();
            M_RKAPAccounting.LastUpdateDB = Convert.ToString(dr["LastUpdateDB"]);
            return M_RKAPAccounting;

            
        }

        public List<RKAPAccounting> RKAPAccounting_Select(int _status)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<RKAPAccounting> L_RKAPAccounting = new List<RKAPAccounting>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = @"Select case when RA.status=1 then 'PENDING' else Case When RA.status = 2 then 'APPROVED' else Case when RA.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,P.ID PeriodID,Case When RA.Type = 1 then 'Balance Sheet' else 'Profit and Loss' end TypeDesc,RA.* from RKAP_Accounting RA left join
                           Period P on RA.PeriodPK = P.PeriodPK  and P.status = 2
                           where RA.status = @status ";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = @"Select case when RA.status=1 then 'PENDING' else Case When RA.status = 2 then 'APPROVED' else Case when RA.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,P.ID PeriodID,Case When RA.Type = 1 then 'Balance Sheet' else 'Profit and Loss' end TypeDesc,RA.* from RKAP_Accounting RA left join
                           Period P on RA.PeriodPK = P.PeriodPK  and P.status = 2 
                           ";
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_RKAPAccounting.Add(setRKAPAccounting(dr));
                                }
                            }
                            return L_RKAPAccounting;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int RKAPAccounting_Add(RKAPAccounting _RKAPAccounting, bool _havePrivillege)
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
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[ApprovedUsersID],[ApprovedTime],LastUpdate)" +
                                 "Select isnull(max(RKAP_AccountingPK),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@LastUpdate from RKAP_Accounting";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _RKAPAccounting.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],LastUpdate)" +
                                "Select isnull(max(RKAP_AccountingPK),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@LastUpdate from RKAP_Accounting";
                        }
                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@ExcelRow", _RKAPAccounting.ExcelRow);
                        cmd.Parameters.AddWithValue("@Amount", _RKAPAccounting.Amount);
                        cmd.Parameters.AddWithValue("@PeriodPK", _RKAPAccounting.PeriodPK);
                        cmd.Parameters.AddWithValue("@Type", _RKAPAccounting.Type);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _RKAPAccounting.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);

                        cmd.ExecuteNonQuery();

                        return _host.Get_LastPKByLastUpate(_datetimeNow, "RKAP_Accounting");
                    }
                }

            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int RKAPAccounting_Update(RKAPAccounting _RKAPAccounting, bool _havePrivillege)
        {

            try
            {
                int _newHisPK;
                int status = _host.Get_Status(_RKAPAccounting.RKAP_AccountingPK, _RKAPAccounting.HistoryPK, "RKAP_Accounting"); ;
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = @"Update RKAP_Accounting set status=2, Notes=@Notes,ExcelRow=@ExcelRow,Amount=@Amount,PeriodPK=@PeriodPK,Type=@Type,
                                ApprovedUsersID=@ApprovedUsersID,
                                ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@lastupdate
                                where RKAP_AccountingPK = @PK and historyPK = @HistoryPK";
                            cmd.Parameters.AddWithValue("@HistoryPK", _RKAPAccounting.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _RKAPAccounting.RKAP_AccountingPK);
                            cmd.Parameters.AddWithValue("@Notes", _RKAPAccounting.Notes);
                            cmd.Parameters.AddWithValue("@ExcelRow", _RKAPAccounting.ExcelRow);
                            cmd.Parameters.AddWithValue("@Amount", _RKAPAccounting.Amount);
                            cmd.Parameters.AddWithValue("@PeriodPK", _RKAPAccounting.PeriodPK);
                            cmd.Parameters.AddWithValue("@Type", _RKAPAccounting.Type);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _RKAPAccounting.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _RKAPAccounting.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);

                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update RKAP_Accounting set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where RKAP_AccountingPK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _RKAPAccounting.RKAP_AccountingPK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _RKAPAccounting.EntryUsersID);
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
                                cmd.CommandText = "Update RKAP_Accounting set Notes=@Notes,ExcelRow=@ExcelRow,Amount=@Amount,PeriodPK=@PeriodPK,Type=@Type," +
                                "UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime ,LastUpdate=@lastupdate " +
                                "where RKAP_AccountingPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _RKAPAccounting.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _RKAPAccounting.RKAP_AccountingPK);
                                cmd.Parameters.AddWithValue("@Notes", _RKAPAccounting.Notes);
                                cmd.Parameters.AddWithValue("@ExcelRow", _RKAPAccounting.ExcelRow);
                                cmd.Parameters.AddWithValue("@Amount", _RKAPAccounting.Amount);
                                cmd.Parameters.AddWithValue("@PeriodPK", _RKAPAccounting.PeriodPK);
                                cmd.Parameters.AddWithValue("@Type", _RKAPAccounting.Type);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _RKAPAccounting.EntryUsersID);
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
                                _newHisPK = _host.Get_NewHistoryPK(_RKAPAccounting.RKAP_AccountingPK, "RKAP_Accounting");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From RKAP_Accounting where RKAP_AccountingPK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _RKAPAccounting.RKAP_AccountingPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _RKAPAccounting.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@ExcelRow", _RKAPAccounting.ExcelRow);
                                cmd.Parameters.AddWithValue("@Amount", _RKAPAccounting.Amount);
                                cmd.Parameters.AddWithValue("@PeriodPK", _RKAPAccounting.PeriodPK);
                                cmd.Parameters.AddWithValue("@Type", _RKAPAccounting.Type);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _RKAPAccounting.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update RKAP_Accounting set status= 4,Notes=@Notes," +
                                " LastUpdate=@lastupdate " +
                                " where RKAP_AccountingPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _RKAPAccounting.Notes);
                                cmd.Parameters.AddWithValue("@PK", _RKAPAccounting.RKAP_AccountingPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _RKAPAccounting.HistoryPK);
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

        public void RKAPAccounting_Approved(RKAPAccounting _RKAPAccounting)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update RKAP_Accounting set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,LastUpdate=@lastupdate " +
                            "where RKAP_AccountingPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _RKAPAccounting.RKAP_AccountingPK);
                        cmd.Parameters.AddWithValue("@historyPK", _RKAPAccounting.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _RKAPAccounting.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update RKAP_Accounting set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where RKAP_AccountingPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _RKAPAccounting.RKAP_AccountingPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _RKAPAccounting.ApprovedUsersID);
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

        public void RKAPAccounting_Reject(RKAPAccounting _RKAPAccounting)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update RKAP_Accounting set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where RKAP_AccountingPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _RKAPAccounting.RKAP_AccountingPK);
                        cmd.Parameters.AddWithValue("@historyPK", _RKAPAccounting.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _RKAPAccounting.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update RKAP_Accounting set status= 2,LastUpdate=@LastUpdate  where RKAP_AccountingPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _RKAPAccounting.RKAP_AccountingPK);
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

        public void RKAPAccounting_Void(RKAPAccounting _RKAPAccounting)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update RKAP_Accounting set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where RKAP_AccountingPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _RKAPAccounting.RKAP_AccountingPK);
                        cmd.Parameters.AddWithValue("@historyPK", _RKAPAccounting.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _RKAPAccounting.VoidUsersID);
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
    }
}
