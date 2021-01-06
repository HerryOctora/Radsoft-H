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
using System.Data.Odbc;
using System.Data.OleDb;


namespace RFSRepository
{
    public class AccountBudgetReps
    {
        Host _host = new Host();
        //1
        string _insertCommand = "INSERT INTO [dbo].[AccountBudget] " +
                            "([AccountBudgetPK],[HistoryPK],[Status],[Version],[PeriodPK],[AccountPK],[OfficePK],[DepartmentPK],[Balance],[Month],[AgentPK],[CounterpartPK],[InstrumentPK],[ConsigneePK],";
        string _paramaterCommand = "@Version,@PeriodPK,@AccountPK,@OfficePK,@DepartmentPK,@Balance,@Month,@AgentPK,@counterpartPK,@InstrumentPK,@ConsigneePK,";

        //2
        private AccountBudget setAccountBudget(SqlDataReader dr)
        {
            AccountBudget M_AccountBudget = new AccountBudget();
            M_AccountBudget.AccountBudgetPK = Convert.ToInt32(dr["AccountBudgetPK"]);
            M_AccountBudget.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_AccountBudget.Status = Convert.ToInt32(dr["Status"]);
            M_AccountBudget.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_AccountBudget.Notes = Convert.ToString(dr["Notes"]);
            M_AccountBudget.Version = dr["Version"].ToString();
            M_AccountBudget.PeriodPK = Convert.ToInt32(dr["PeriodPK"]);
            M_AccountBudget.PeriodID = Convert.ToString(dr["PeriodID"]);
            M_AccountBudget.AccountPK = Convert.ToInt32(dr["AccountPK"]);
            M_AccountBudget.AccountID = Convert.ToString(dr["AccountID"]);
            M_AccountBudget.AccountName = Convert.ToString(dr["AccountName"]);
            M_AccountBudget.OfficePK = Convert.ToInt32(dr["OfficePK"]);
            M_AccountBudget.OfficeID = Convert.ToString(dr["OfficeID"]);
            M_AccountBudget.DepartmentPK = Convert.ToInt32(dr["DepartmentPK"]);
            M_AccountBudget.DepartmentID = Convert.ToString(dr["DepartmentID"]);
            M_AccountBudget.DepartmentName = Convert.ToString(dr["DepartmentName"]);
            M_AccountBudget.Balance = Convert.ToDecimal(dr["Balance"]);
            M_AccountBudget.Month = Convert.ToInt32(dr["Month"]);
            M_AccountBudget.MonthDesc = Convert.ToString(dr["MonthDesc"]);
            //FOR CLient 03
            if (_host.CheckColumnIsExist(dr, "AgentPK"))
            {
                M_AccountBudget.AgentPK = dr["AgentPK"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["AgentPK"]);
            }
            if (_host.CheckColumnIsExist(dr, "AgentID"))
            {
                M_AccountBudget.AgentID = Convert.ToString(dr["AgentID"]);
            }
            if (_host.CheckColumnIsExist(dr, "AgentName"))
            {
                M_AccountBudget.AgentName = Convert.ToString(dr["AgentName"]);
            }




            if (_host.CheckColumnIsExist(dr, "CounterpartPK"))
            {
                M_AccountBudget.CounterpartPK = dr["CounterpartPK"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["CounterpartPK"]);
            }
            if (_host.CheckColumnIsExist(dr, "CounterpartID"))
            {
                M_AccountBudget.CounterpartID = Convert.ToString(dr["CounterpartID"]);
            }
            if (_host.CheckColumnIsExist(dr, "CounterpartName"))
            {
                M_AccountBudget.CounterpartName = Convert.ToString(dr["CounterpartName"]);
            }




            if (_host.CheckColumnIsExist(dr, "InstrumentPK"))
            {
                M_AccountBudget.InstrumentPK = dr["InstrumentPK"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["InstrumentPK"]);
            }
            if (_host.CheckColumnIsExist(dr, "InstrumentID"))
            {
                M_AccountBudget.InstrumentID = Convert.ToString(dr["InstrumentID"]);
            }
            if (_host.CheckColumnIsExist(dr, "InstrumentName"))
            {
                M_AccountBudget.InstrumentName = Convert.ToString(dr["InstrumentName"]);
            }




            if (_host.CheckColumnIsExist(dr, "ConsigneePK"))
            {
                M_AccountBudget.ConsigneePK = dr["ConsigneePK"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["ConsigneePK"]);
            }
            if (_host.CheckColumnIsExist(dr, "ConsigneeID"))
            {
                M_AccountBudget.ConsigneeID = Convert.ToString(dr["ConsigneeID"]);
            }
            if (_host.CheckColumnIsExist(dr, "ConsigneeName"))
            {
                M_AccountBudget.ConsigneeName = Convert.ToString(dr["ConsigneeName"]);
            }



            //End FOR CLient 03
            M_AccountBudget.EntryUsersID = dr["EntryUsersID"].ToString();
            M_AccountBudget.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_AccountBudget.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_AccountBudget.VoidUsersID = dr["VoidUsersID"].ToString();
            M_AccountBudget.EntryTime = dr["EntryTime"].ToString();
            M_AccountBudget.UpdateTime = dr["UpdateTime"].ToString();
            M_AccountBudget.ApprovedTime = dr["ApprovedTime"].ToString();
            M_AccountBudget.VoidTime = dr["VoidTime"].ToString();
            M_AccountBudget.DBUserID = dr["DBUserID"].ToString();
            M_AccountBudget.DBTerminalID = dr["DBTerminalID"].ToString();
            M_AccountBudget.LastUpdate = dr["LastUpdate"].ToString();
            M_AccountBudget.LastUpdateDB = dr["LastUpdateDB"].ToString();
            return M_AccountBudget;
        }

        public List<AccountBudget> AccountBudget_Select(int _status)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {

                    DbCon.Open();
                    List<AccountBudget> L_AccountBudget = new List<AccountBudget>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = @"Select case when AB.status=1 then 'PENDING' else Case When AB.status = 2 then 'APPROVED' else Case when AB.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,A.ID AccountID,A.Name AccountName, O.ID OfficeID, D.ID DepartmentID,D.Name DepartmentName,mv.Descone MonthDesc ,B.ID PeriodID,
                            isnull(ag.ID, '') AgentID, isnull(ag.Name, '') AgentName, isnull(cp.ID, '') CounterpartID, isnull(cp.Name,'') CounterpartName, isnull(i.ID, '') InstrumentID, isnull(i.Name,'') InstrumentName, isnull(co.ID,'') ConsigneeID, isnull(Co.Name,'') ConsigneeName, AB.*from AccountBudget AB left join
                            Account A on AB.AccountPK = A.AccountPK and A.status = 2 left join
                            Period B on AB.PeriodPK = B.PeriodPK and B.Status = 2 left join
                            Office O on AB.OfficePK = O.OfficePK and O.status = 2 left join
                            Department D on AB.DepartmentPK = D.DepartmentPK and D.status = 2 left join
                            Agent ag on AB.AgentPK = ag.AgentPK and ag.Status = 2 Left join
                            Counterpart cp on AB.CounterpartPK = cp.CounterpartPK and cp.Status = 2 Left join
                            Instrument i on AB.InstrumentPK = i.InstrumentPK  and I.Status = 2 left join
                            Consignee co  on AB.consigneePK = co.consigneePK  and co.Status = 2  left join
                            mastervalue mv on AB.Month = mv.code where mv.type = 'Month'  and mv.status = 2
                            and AB.status = @status";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = @"	Select case when AB.status=1 then 'PENDING' else Case When AB.status = 2 then 'APPROVED' else Case when AB.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,A.ID AccountID,A.Name AccountName, O.ID OfficeID, D.ID DepartmentID,D.Name DepartmentName,mv.Descone MonthDesc ,B.ID PeriodID,
                            isnull(ag.ID, '') AgentID, isnull(ag.Name, '') AgentName, isnull(cp.ID, '') CounterpartID, isnull(cp.Name,'') CounterpartName, isnull(i.ID, '') InstrumentID, isnull(i.Name,'') InstrumentName, isnull(co.ID,'') ConsigneeID, isnull(Co.Name,'') ConsigneeName, AB.*from AccountBudget AB left join
                            Account A on AB.AccountPK = A.AccountPK and A.status = 2 left join
                            Period B on AB.PeriodPK = B.PeriodPK and B.Status = 2 left join
                            Office O on AB.OfficePK = O.OfficePK and O.status = 2 left join
                            Department D on AB.DepartmentPK = D.DepartmentPK and D.status = 2 left join
                            Agent ag on AB.AgentPK = ag.AgentPK and ag.Status = 2 Left join
                            Counterpart cp on AB.CounterpartPK = cp.CounterpartPK and cp.Status = 2 Left join
                            Instrument i on AB.InstrumentPK = i.InstrumentPK  and I.Status = 2 left join
                            Consignee co  on AB.consigneePK = co.consigneePK  and co.Status = 2  left join
                            mastervalue mv on AB.Month = mv.code where mv.type = 'Month'  and mv.status = 2";
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_AccountBudget.Add(setAccountBudget(dr));
                                }
                            }
                            return L_AccountBudget;
                        }
                    }
                }

            }
            catch (Exception err)
            {
                throw err;
            }
        }



 public int AccountBudget_Add(AccountBudget _accountBudget, bool _havePrivillege)
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
                                 "Select isnull(max(AccountBudgetPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@LastUpdate from AccountBudget";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _accountBudget.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);

                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[LastUpdate])" +
                                "Select isnull(max(AccountBudgetPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@LastUpdate from AccountBudget";
                        }
                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@Version", _accountBudget.Version);
                        cmd.Parameters.AddWithValue("@PeriodPK", _accountBudget.PeriodPK);
                        cmd.Parameters.AddWithValue("@AccountPK", _accountBudget.AccountPK);
                        cmd.Parameters.AddWithValue("@OfficePK", _accountBudget.OfficePK);
                        cmd.Parameters.AddWithValue("@DepartmentPK", _accountBudget.DepartmentPK);
                        cmd.Parameters.AddWithValue("@Balance", _accountBudget.Balance);
                        cmd.Parameters.AddWithValue("@Month", _accountBudget.Month);
                        cmd.Parameters.AddWithValue("@AgentPK", _accountBudget.AgentPK);
                        cmd.Parameters.AddWithValue("@CounterpartPK", _accountBudget.CounterpartPK);
                        cmd.Parameters.AddWithValue("@InstrumentPK", _accountBudget.InstrumentPK);
                        cmd.Parameters.AddWithValue("@ConsigneePK", _accountBudget.ConsigneePK);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _accountBudget.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);

                        cmd.ExecuteNonQuery();

                        return _host.Get_LastPKByLastUpate(_dateTimeNow, "AccountBudget");
                    }

                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }



        public int AccountBudget_Update(AccountBudget _accountBudget, bool _havePrivillege)
        {
            try
            {
                int _newHisPK;
                int status = _host.Get_Status(_accountBudget.AccountBudgetPK, _accountBudget.HistoryPK, "AccountBudget");
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = @"Update AccountBudget set status=2,Notes=@Notes,Version=@Version,
                                PeriodPK=@PeriodPK,AccountPK=@AccountPK,OfficePK=@OfficePK,DepartmentPK=@DepartmentPK,Balance=@Balance,Month=@Month,AgentPK = @AgentPK,CounterpartPK = @CounterpartPK,InstrumentPK = @InstrumentPK,
                                 ConsigneePK = @ConsigneePK
                                ApprovedUsersID=@ApprovedUsersID, 
                                ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@LastUpdate 
                                where AccountBudgetPK = @PK and historyPK = @HistoryPK";
                            cmd.Parameters.AddWithValue("@HistoryPK", _accountBudget.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _accountBudget.AccountBudgetPK);
                            cmd.Parameters.AddWithValue("@Notes", _accountBudget.Notes);
                            cmd.Parameters.AddWithValue("@Version", _accountBudget.Version);
                            cmd.Parameters.AddWithValue("@PeriodPK", _accountBudget.PeriodPK);
                            cmd.Parameters.AddWithValue("@AccountPK", _accountBudget.AccountPK);
                            cmd.Parameters.AddWithValue("@OfficePK", _accountBudget.OfficePK);
                            cmd.Parameters.AddWithValue("@DepartmentPK", _accountBudget.DepartmentPK);
                            cmd.Parameters.AddWithValue("@Balance", _accountBudget.Balance);
                            cmd.Parameters.AddWithValue("@Month", _accountBudget.Month);
                            cmd.Parameters.AddWithValue("@AgentPK", _accountBudget.AgentPK);
                            cmd.Parameters.AddWithValue("@CounterpartPK", _accountBudget.CounterpartPK);
                            cmd.Parameters.AddWithValue("@InstrumentPK", _accountBudget.InstrumentPK);
                            cmd.Parameters.AddWithValue("@ConsigneePK", _accountBudget.ConsigneePK);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _accountBudget.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _accountBudget.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);

                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update AccountBudget set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,lastupdate=@lastupdate where AccountBudgetPK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _accountBudget.AccountBudgetPK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _accountBudget.EntryUsersID);
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
                                cmd.CommandText = @"Update AccountBudget set Notes=@Notes,Version=@Version,
                                PeriodPK=@PeriodPK,AccountPK=@AccountPK,OfficePK=@OfficePK,DepartmentPK=@DepartmentPK,Balance=@Balance,Month=@Month,AgentPK = @AgentPK,CounterpartPK = @CounterpartPK,InstrumentPK = @InstrumentPK,
                                 ConsigneePK = @ConsigneePK,
                                ApprovedUsersID=@ApprovedUsersID, 
                                ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@LastUpdate 
                                where AccountBudgetPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _accountBudget.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _accountBudget.AccountBudgetPK);
                                cmd.Parameters.AddWithValue("@Notes", _accountBudget.Notes);
                                cmd.Parameters.AddWithValue("@Version", _accountBudget.Version);
                                cmd.Parameters.AddWithValue("@PeriodPK", _accountBudget.PeriodPK);
                                cmd.Parameters.AddWithValue("@AccountPK", _accountBudget.AccountPK);
                                cmd.Parameters.AddWithValue("@OfficePK", _accountBudget.OfficePK);
                                cmd.Parameters.AddWithValue("@DepartmentPK", _accountBudget.DepartmentPK);
                                cmd.Parameters.AddWithValue("@Balance", _accountBudget.Balance);
                                cmd.Parameters.AddWithValue("@Month", _accountBudget.Month);
                                cmd.Parameters.AddWithValue("@AgentPK", _accountBudget.AgentPK);
                                cmd.Parameters.AddWithValue("@CounterpartPK", _accountBudget.CounterpartPK);
                                cmd.Parameters.AddWithValue("@InstrumentPK", _accountBudget.InstrumentPK);
                                cmd.Parameters.AddWithValue("@ConsigneePK", _accountBudget.ConsigneePK);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _accountBudget.EntryUsersID);
                                cmd.Parameters.AddWithValue("@ApprovedUsersID", _accountBudget.EntryUsersID);
                                cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
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
                                _newHisPK = _host.Get_NewHistoryPK(_accountBudget.AccountBudgetPK, "AccountBudget");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From AccountBudget where AccountBudgetPK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _accountBudget.AccountBudgetPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _accountBudget.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@Version", _accountBudget.Version);
                                cmd.Parameters.AddWithValue("@PeriodPK", _accountBudget.PeriodPK);
                                cmd.Parameters.AddWithValue("@AccountPK", _accountBudget.AccountPK);
                                cmd.Parameters.AddWithValue("@OfficePK", _accountBudget.OfficePK);
                                cmd.Parameters.AddWithValue("@DepartmentPK", _accountBudget.DepartmentPK);
                                cmd.Parameters.AddWithValue("@Balance", _accountBudget.Balance);
                                cmd.Parameters.AddWithValue("@Month", _accountBudget.Month);
                                cmd.Parameters.AddWithValue("@AgentPK", _accountBudget.AgentPK);
                                cmd.Parameters.AddWithValue("@CounterpartPK", _accountBudget.CounterpartPK);
                                cmd.Parameters.AddWithValue("@InstrumentPK", _accountBudget.InstrumentPK);
                                cmd.Parameters.AddWithValue("@ConsigneePK", _accountBudget.ConsigneePK);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _accountBudget.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update AccountBudget set status= 4,Notes=@Notes, " +
                                    "LastUpdate=@LastUpdate where AccountBudgetPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _accountBudget.Notes);
                                cmd.Parameters.AddWithValue("@PK", _accountBudget.AccountBudgetPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _accountBudget.HistoryPK);
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


        public void AccountBudget_Approved(AccountBudget _accountBudget)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update AccountBudget set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,LastUpdate=@LastUpdate " +
                            "where AccountBudgetPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _accountBudget.AccountBudgetPK);
                        cmd.Parameters.AddWithValue("@historyPK", _accountBudget.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _accountBudget.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update AccountBudget set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where AccountBudgetPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _accountBudget.AccountBudgetPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _accountBudget.ApprovedUsersID);
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

        public void AccountBudget_Reject(AccountBudget _accountBudget)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update AccountBudget set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where AccountBudgetPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _accountBudget.AccountBudgetPK);
                        cmd.Parameters.AddWithValue("@historyPK", _accountBudget.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _accountBudget.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update AccountBudget set status= 2,LastUpdate=@LastUpdate where AccountBudgetPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _accountBudget.AccountBudgetPK);
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

        public void AccountBudget_Void(AccountBudget _accountBudget)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update AccountBudget set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where AccountBudgetPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _accountBudget.AccountBudgetPK);
                        cmd.Parameters.AddWithValue("@historyPK", _accountBudget.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _accountBudget.VoidUsersID);
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

        // REA LAIN-LAIN dimulai dari sini ( untuk function diluar standart )

        public List<AccountBudgetCombo> AccountBudget_Combo()
        {

            try
            {

                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<AccountBudgetCombo> L_AccountBudget = new List<AccountBudgetCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "SELECT  Distinct Version FROM [AccountBudget]  where status in (1,2) order by Version";
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    AccountBudgetCombo M_AccountBudget = new AccountBudgetCombo();
                                    M_AccountBudget.Version = Convert.ToString(dr["Version"]);
                                    L_AccountBudget.Add(M_AccountBudget);
                                }

                            }
                            return L_AccountBudget;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }


        public string AccountBudgetImport(string _fileSource, string _userID)
        {
            using (SqlConnection conn = new SqlConnection(Tools.conString))
            {
                conn.Open();
                using (SqlCommand cmd2 = conn.CreateCommand())
                {
                    cmd2.CommandText = "truncate table AccountBudgetTemp";
                    cmd2.ExecuteNonQuery();
                }
            }

            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(Tools.conString, SqlBulkCopyOptions.TableLock))
            {
                bulkCopy.DestinationTableName = "dbo.AccountBudgetTemp";
                bulkCopy.WriteToServer(CreateDataTableFromAccountBudgetTempExcelFile(_fileSource));
            }

            try
            {
                DateTime _dateTimeNow = DateTime.Now;

                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = @" 
                                select 'Success' A
                                                ";
                        cmd.Parameters.AddWithValue("@EntryUsersID", _userID);
                        cmd.Parameters.AddWithValue("@Lastupdate", DateTime.Now);
                        cmd.ExecuteNonQuery();
                        return "Import On Progressing";
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        private DataTable CreateDataTableFromAccountBudgetTempExcelFile(string _path)
        {
            try
            {
                using (DataTable dt = new DataTable())
                {
                    DataColumn dc;
                    DataRow dr;

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Int32");
                    dc.ColumnName = "AccountBudgetTempPK";
                    dc.Unique = false;
                    dc.AutoIncrement = true;
                    dc.AutoIncrementSeed = 1;
                    dc.AutoIncrementStep = 1;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "ID";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Name";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "JAN";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "FEB";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "MAR";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "APR";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "MEI";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "JUNI";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "JULI";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "AGUST";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "SEPT";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "OKT";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "NOV";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "DES";
                    dc.Unique = false;
                    dt.Columns.Add(dc);



                    using (OleDbConnection odConnection = new OleDbConnection(Tools.ConStringExcel2007(_path)))
                    {
                        odConnection.Open();
                        using (OleDbCommand odCmd = odConnection.CreateCommand())
                        {
                            // _oldfilename = nama sheet yang ada di file excel yang diimport
                            odCmd.CommandText = "SELECT * FROM [Sheet1$] ";
                            using (OleDbDataReader odRdr = odCmd.ExecuteReader())
                            {
                                // start counting from index = 1 --> skipping the header (index=0)
                                for (int i = 1; i <= 1; i++)
                                {
                                    odRdr.Read();
                                }
                                do
                                {
                                    dr = dt.NewRow();

                                    dr["ID"] = odRdr[0];
                                    dr["Name"] = odRdr[1];
                                    dr["JAN"] = odRdr[2];
                                    dr["FEB"] = odRdr[3];
                                    dr["MAR"] = odRdr[4];
                                    dr["APR"] = odRdr[5];
                                    dr["MEI"] = odRdr[6];
                                    dr["JUNI"] = odRdr[7];
                                    dr["JULI"] = odRdr[8];
                                    dr["AGUST"] = odRdr[9];
                                    dr["SEPT"] = odRdr[10];
                                    dr["OKT"] = odRdr[11];
                                    dr["NOV"] = odRdr[12];
                                    dr["DES"] = odRdr[13];

                                    if (dr["AccountBudgetTempPK"].Equals(DBNull.Value) != true) { dt.Rows.Add(dr); }
                                } while (odRdr.Read());
                            }
                        }
                        odConnection.Close();
                    }

                    return dt;
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public void UpdatePeriod(AccountBudget _AccountBudget)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update AccountBudgetTemp set PeriodPK = @PK";
                        cmd.Parameters.AddWithValue("@PK", _AccountBudget.IPeriodPK);
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