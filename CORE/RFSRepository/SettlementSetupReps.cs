using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using RFSModel;
using RFSUtility;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.IO;
using System.Text;
using System.Threading;
using System.Data;
using System.Data.SqlClient;
using System.Data.Odbc;
using System.Data.OleDb;

namespace RFSRepository
{
    public class SettlementSetupReps
    {
        Host _host = new Host();

        //1
        string _insertCommand = "INSERT INTO [dbo].[SettlementSetup] " +
                            "([SettlementSetupPK],[HistoryPK],[Status],[RegDays],[KPDTaxExpenseForBond],";
        string _paramaterCommand = "@RegDays,@KPDTaxExpenseForBond,";

        //2
        private SettlementSetup setSettlementSetup(SqlDataReader dr)
        {
            SettlementSetup M_SettlementSetup = new SettlementSetup();
            M_SettlementSetup.SettlementSetupPK = Convert.ToInt32(dr["SettlementSetupPK"]);
            M_SettlementSetup.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_SettlementSetup.Status = Convert.ToInt32(dr["Status"]);
            M_SettlementSetup.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_SettlementSetup.Notes = Convert.ToString(dr["Notes"]);
            M_SettlementSetup.RegDays = Convert.ToDecimal(dr["RegDays"]);
            M_SettlementSetup.KPDTaxExpenseForBond = Convert.ToDecimal(dr["KPDTaxExpenseForBond"]);
            M_SettlementSetup.EntryUsersID = dr["EntryUsersID"].ToString();
            M_SettlementSetup.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_SettlementSetup.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_SettlementSetup.VoidUsersID = dr["VoidUsersID"].ToString();
            M_SettlementSetup.EntryTime = dr["EntryTime"].ToString();
            M_SettlementSetup.UpdateTime = dr["UpdateTime"].ToString();
            M_SettlementSetup.ApprovedTime = dr["ApprovedTime"].ToString();
            M_SettlementSetup.VoidTime = dr["VoidTime"].ToString();
            M_SettlementSetup.DBUserID = dr["DBUserID"].ToString();
            M_SettlementSetup.DBTerminalID = dr["DBTerminalID"].ToString();
            M_SettlementSetup.LastUpdate = dr["LastUpdate"].ToString();
            return M_SettlementSetup;
        }

        public List<SettlementSetup> SettlementSetup_Select(int _status)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<SettlementSetup> L_SettlementSetup = new List<SettlementSetup>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText =
                                "Select case when Status=1 then 'PENDING' else case when Status = 2 then 'APPROVED' else case when Status = 3 then 'VOID' else 'WAITING' end end end StatusDesc, * from SettlementSetup " +
                                "where status = @status " +
                                "order by SettlementSetupPK";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText =
                                "Select case when Status=1 then 'PENDING' else case when Status = 2 then 'APPROVED' else case when Status = 3 then 'VOID' else 'WAITING' end end end StatusDesc, * from SettlementSetup " +
                                "order by SettlementSetupPK";
                        }

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_SettlementSetup.Add(setSettlementSetup(dr));
                                }
                            }
                            return L_SettlementSetup;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }


        public int SettlementSetup_Add(SettlementSetup _SettlementSetup, bool _havePrivillege)
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
                                 "Select isnull(max(SettlementSetupPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@LastUpdate From SettlementSetup";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _SettlementSetup.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);
                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[LastUpdate])" +
                                "Select isnull(max(SettlementSetupPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@LastUpdate From SettlementSetup";
                        }
                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@RegDays", _SettlementSetup.RegDays);
                        cmd.Parameters.AddWithValue("@KPDTaxExpenseForBond", _SettlementSetup.KPDTaxExpenseForBond);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _SettlementSetup.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                        cmd.ExecuteNonQuery();

                        return _host.Get_LastPKByLastUpate(_dateTimeNow, "SettlementSetup");
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public int SettlementSetup_Update(SettlementSetup _SettlementSetup, bool _havePrivillege)
        {
            try
            {
                int _newHisPK;
                int status = _host.Get_Status(_SettlementSetup.SettlementSetupPK, _SettlementSetup.HistoryPK, "SettlementSetup");
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update SettlementSetup set status=2,Notes=@Notes,RegDays=@RegDays,KPDTaxExpenseForBond=@KPDTaxExpenseForBond," +
                                "ApprovedUsersID=@ApprovedUsersID, " +
                                "ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@LastUpdate " +
                                "where SettlementSetupPK = @PK and historyPK = @HistoryPK ";
                            cmd.Parameters.AddWithValue("@HistoryPK", _SettlementSetup.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _SettlementSetup.SettlementSetupPK);
                            cmd.Parameters.AddWithValue("@Notes", _SettlementSetup.Notes);
                            cmd.Parameters.AddWithValue("@RegDays", _SettlementSetup.RegDays);
                            cmd.Parameters.AddWithValue("@KPDTaxExpenseForBond", _SettlementSetup.KPDTaxExpenseForBond);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _SettlementSetup.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _dateTimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _SettlementSetup.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);
                            cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update SettlementSetup set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where SettlementSetupPK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _SettlementSetup.SettlementSetupPK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _SettlementSetup.EntryUsersID);
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
                                cmd.CommandText = "Update SettlementSetup set Notes=@Notes,RegDays=@RegDays,KPDTaxExpenseForBond=@KPDTaxExpenseForBond," +
                                "UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@LastUpdate " +
                                "where SettlementSetupPK = @PK and historyPK = @HistoryPK ";
                                cmd.Parameters.AddWithValue("@HistoryPK", _SettlementSetup.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _SettlementSetup.SettlementSetupPK);
                                cmd.Parameters.AddWithValue("@Notes", _SettlementSetup.Notes);
                                cmd.Parameters.AddWithValue("@RegDays", _SettlementSetup.RegDays);
                                cmd.Parameters.AddWithValue("@KPDTaxExpenseForBond", _SettlementSetup.KPDTaxExpenseForBond);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _SettlementSetup.EntryUsersID);
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
                                _newHisPK = _host.Get_NewHistoryPK(_SettlementSetup.SettlementSetupPK, "SettlementSetup");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From SettlementSetup where SettlementSetupPK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _SettlementSetup.SettlementSetupPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _SettlementSetup.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@RegDays", _SettlementSetup.RegDays);
                                cmd.Parameters.AddWithValue("@KPDTaxExpenseForBond", _SettlementSetup.KPDTaxExpenseForBond);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _SettlementSetup.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _dateTimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update SettlementSetup set status= 4,Notes=@Notes," +
                                "LastUpdate=@LastUpdate where SettlementSetupPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _SettlementSetup.Notes);
                                cmd.Parameters.AddWithValue("@PK", _SettlementSetup.SettlementSetupPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _SettlementSetup.HistoryPK);
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

        public void SettlementSetup_Approved(SettlementSetup _SettlementSetup)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update SettlementSetup set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,LastUpdate=@LastUpdate " +
                            "where SettlementSetupPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _SettlementSetup.SettlementSetupPK);
                        cmd.Parameters.AddWithValue("@historyPK", _SettlementSetup.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _SettlementSetup.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update SettlementSetup set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where SettlementSetupPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _SettlementSetup.SettlementSetupPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _SettlementSetup.ApprovedUsersID);
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

        public void SettlementSetup_Reject(SettlementSetup _SettlementSetup)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update SettlementSetup set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate = @LastUpdate " +
                            "where SettlementSetupPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _SettlementSetup.SettlementSetupPK);
                        cmd.Parameters.AddWithValue("@historyPK", _SettlementSetup.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _SettlementSetup.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update SettlementSetup set status= 2,LastUpdate=@LastUpdate where SettlementSetupPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _SettlementSetup.SettlementSetupPK);
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

        public void SettlementSetup_Void(SettlementSetup _SettlementSetup)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update SettlementSetup set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate = @LastUpdate " +
                            "where SettlementSetupPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _SettlementSetup.SettlementSetupPK);
                        cmd.Parameters.AddWithValue("@historyPK", _SettlementSetup.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _SettlementSetup.VoidUsersID);
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

        public int Get_RegDays(int _fundPK)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = @" select RegDays from SettlementSetup where FundPK = @FundPK and status in (1,2) ";

                        cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (!dr.Read())
                            {
                                return 0;
                            }
                            else
                            {
                                return Convert.ToInt32(dr["RegDays"]);
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