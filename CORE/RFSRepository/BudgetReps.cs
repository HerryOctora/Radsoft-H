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
    public class BudgetReps
    {
        Host _host = new Host();

        //1
        string _insertCommand = "INSERT INTO [dbo].[Budget] " +
                            @"([BudgetPK],[HistoryPK],[Status],[ReportPeriodPK],[PeriodPK],[DepartmentPK],[AccountPK],
                            [ItemBudgetPK],[Amount],[January],[February],
                            [March],[April],[May],[June],[July],[August],[September],
                            [October],[November],[December],";
        string _paramaterCommand = @"@ReportPeriodPK,@PeriodPK,@DepartmentPK,@AccountPK,@ItemBudgetPK,@Amount,@January,@February,@March,@April,@May
                                    ,@June,@July,@August,@September,@October,@November,@December,";

        //2
        private Budget setBudget(SqlDataReader dr)
        {
            Budget M_Budget = new Budget();
            M_Budget.BudgetPK = Convert.ToInt32(dr["BudgetPK"]);
            M_Budget.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_Budget.Status = Convert.ToInt32(dr["Status"]);
            M_Budget.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_Budget.Notes = Convert.ToString(dr["Notes"]);
            M_Budget.ReportPeriodPK = Convert.ToInt32(dr["ReportPeriodPK"]);
            M_Budget.ReportPeriodID = Convert.ToString(dr["ReportPeriodID"]);
            M_Budget.PeriodPK = Convert.ToInt32(dr["PeriodPK"]);
            M_Budget.PeriodID = Convert.ToString(dr["PeriodID"]);
            M_Budget.DepartmentPK = Convert.ToInt32(dr["DepartmentPK"]);
            M_Budget.DepartmentID = Convert.ToString(dr["DepartmentID"]);
            M_Budget.AccountPK = Convert.ToInt32(dr["AccountPK"]);
            M_Budget.AccountID = Convert.ToString(dr["AccountID"]);
            M_Budget.ItemBudgetPK = Convert.ToInt32(dr["ItemBudgetPK"]);
            M_Budget.ItemBudgetID = Convert.ToString(dr["ItemBudgetID"]);
            M_Budget.Amount = Convert.ToDecimal(dr["Amount"]);
            M_Budget.January = Convert.ToDecimal(dr["January"]);
            M_Budget.February = Convert.ToDecimal(dr["February"]);
            M_Budget.March = Convert.ToDecimal(dr["March"]);
            M_Budget.April = Convert.ToDecimal(dr["April"]);
            M_Budget.May = Convert.ToDecimal(dr["May"]);
            M_Budget.June = Convert.ToDecimal(dr["June"]);
            M_Budget.July = Convert.ToDecimal(dr["July"]);
            M_Budget.August = Convert.ToDecimal(dr["August"]);
            M_Budget.September = Convert.ToDecimal(dr["September"]);
            M_Budget.October = Convert.ToDecimal(dr["October"]);
            M_Budget.November = Convert.ToDecimal(dr["November"]);
            M_Budget.December = Convert.ToDecimal(dr["December"]);
            M_Budget.EntryUsersID = dr["EntryUsersID"].ToString();
            M_Budget.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_Budget.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_Budget.VoidUsersID = dr["VoidUsersID"].ToString();
            M_Budget.EntryTime = dr["EntryTime"].ToString();
            M_Budget.UpdateTime = dr["UpdateTime"].ToString();
            M_Budget.ApprovedTime = dr["ApprovedTime"].ToString();
            M_Budget.VoidTime = dr["VoidTime"].ToString();
            M_Budget.DBUserID = dr["DBUserID"].ToString();
            M_Budget.DBTerminalID = dr["DBTerminalID"].ToString();
            M_Budget.LastUpdate = dr["LastUpdate"].ToString();
            M_Budget.LastUpdateDB = Convert.ToString(dr["LastUpdateDB"]);
            return M_Budget;
        }

        public List<Budget> Budget_Select(int _status)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<Budget> L_Budget = new List<Budget>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = @"select case when A.status=1 then 'PENDING' else Case When A.status = 2 then 'APPROVED' else Case when A.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,
                                        f.Name ItemBudgetID,
                                        isnull(ab.PeriodPK,0) ReportPeriodPK,isnull(ab.ID,'') ReportPeriodID,b.PeriodPK,b.ID PeriodID, d.DepartmentPK, d.ID + ' - ' +  d.Name DepartmentID,e.AccountPK,e.id + ' - ' + e.Name AccountID,Amount,January,February,March,April,May,June,
										July,August,September,October,November,December,* from Budget A
                                        left join Period b on a.PeriodPK = b.PeriodPK and B.status in(1,2)
                                        left join Period ab on a.ReportPeriodPK = ab.PeriodPK and ab.status in(1,2)
										left join Department d on a.DepartmentPK = d.DepartmentPK and d.Status in(1,2)
										left join Account e on a.AccountPK = e.AccountPK and d.Status in(1,2) 
                                        left join ItemBudget f on a.ItemBudgetPK = f.ItemBudgetPK and f.Status in(1,2) 
                                        where A.status = @status order by BudgetPK";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = @"select case when A.status=1 then 'PENDING' else Case When A.status = 2 then 'APPROVED' else Case when A.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,
                                        f.Name ItemBudgetID,
                                        isnull(ab.PeriodPK,0) ReportPeriodPK,isnull(ab.ID,'') ReportPeriodID,b.PeriodPK,b.ID PeriodID, d.DepartmentPK, d.ID + ' - ' +  d.Name DepartmentID,e.AccountPK,e.id + ' - ' + e.Name AccountID,Amount,January,February,March,April,May,June,
										July,August,September,October,November,December,* from Budget A
                                        left join Period b on a.PeriodPK = b.PeriodPK and B.status in(1,2)
                                        left join Period ab on a.ReportPeriodPK = ab.PeriodPK and ab.status in(1,2)
										left join Department d on a.DepartmentPK = d.DepartmentPK and d.Status in(1,2)
										left join Account e on a.AccountPK = e.AccountPK and d.Status in(1,2) 
                                        left join ItemBudget f on a.ItemBudgetPK = f.ItemBudgetPK and f.Status in(1,2) 
                                        order by BudgetPK";
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_Budget.Add(setBudget(dr));
                                }
                            }
                            return L_Budget;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int Budget_Add(Budget _Budget, bool _havePrivillege)
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
                                 "Select isnull(max(BudgetPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@lastupdate From Budget";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _Budget.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);

                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],lastupdate)" +
                                "Select isnull(max(BudgetPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@lastupdate From Budget";
                        }

                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@ReportPeriodPK", _Budget.ReportPeriodPK);
                        cmd.Parameters.AddWithValue("@PeriodPK", _Budget.PeriodPK);
                        cmd.Parameters.AddWithValue("@DepartmentPK", _Budget.DepartmentPK);
                        cmd.Parameters.AddWithValue("@AccountPK", _Budget.AccountPK);
                        cmd.Parameters.AddWithValue("@ItemBudgetPK", _Budget.ItemBudgetPK);
                        cmd.Parameters.AddWithValue("@Amount", _Budget.Amount);
                        cmd.Parameters.AddWithValue("@January", _Budget.January);
                        cmd.Parameters.AddWithValue("@February", _Budget.February);
                        cmd.Parameters.AddWithValue("@March", _Budget.March);
                        cmd.Parameters.AddWithValue("@April", _Budget.April);
                        cmd.Parameters.AddWithValue("@May", _Budget.May);
                        cmd.Parameters.AddWithValue("@June", _Budget.June);
                        cmd.Parameters.AddWithValue("@July", _Budget.July);
                        cmd.Parameters.AddWithValue("@August", _Budget.August);
                        cmd.Parameters.AddWithValue("@September", _Budget.September);
                        cmd.Parameters.AddWithValue("@October", _Budget.October);
                        cmd.Parameters.AddWithValue("@November", _Budget.November);
                        cmd.Parameters.AddWithValue("@December", _Budget.December);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _Budget.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();

                        return _host.Get_LastPKByLastUpate(_datetimeNow, "Budget");
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }


        public int Budget_Update(Budget _Budget, bool _havePrivillege)
        {
            try
            {
                int _newHisPK;
                DateTime _datetimeNow = DateTime.Now;
                int status = _host.Get_Status(_Budget.BudgetPK, _Budget.HistoryPK, "Budget");
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update Budget set status=2, Notes=@Notes,ReportPeriodPK=@ReportPeriodPK,PeriodPK=@PeriodPK,DepartmentPK=@DepartmentPK,AccountPK=@AccountPK,ItemBudgetPK=@ItemBudgetPK,Amount=@Amount,January=@January,February=@February,March=@March," +
                                "April=@April,May=@May,June=@June,July=@July,August=@August,September=@September,October=@October,November=@November,December=@December,ApprovedUsersID=@ApprovedUsersID, " +
                                "ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,lastupdate=@lastupdate " +
                                "where BudgetPK = @PK and historyPK = @HistoryPK";
                            cmd.Parameters.AddWithValue("@HistoryPK", _Budget.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _Budget.BudgetPK);
                            cmd.Parameters.AddWithValue("@Notes", _Budget.Notes);
                            cmd.Parameters.AddWithValue("@ReportPeriodPK", _Budget.ReportPeriodPK);
                            cmd.Parameters.AddWithValue("@PeriodPK", _Budget.PeriodPK);
                            cmd.Parameters.AddWithValue("@DepartmentPK", _Budget.DepartmentPK);
                            cmd.Parameters.AddWithValue("@AccountPK", _Budget.AccountPK);
                            cmd.Parameters.AddWithValue("@ItemBudgetPK", _Budget.ItemBudgetPK);
                            cmd.Parameters.AddWithValue("@Amount", _Budget.Amount);
                            cmd.Parameters.AddWithValue("@January", _Budget.January);
                            cmd.Parameters.AddWithValue("@February", _Budget.February);
                            cmd.Parameters.AddWithValue("@March", _Budget.March);
                            cmd.Parameters.AddWithValue("@April", _Budget.April);
                            cmd.Parameters.AddWithValue("@May", _Budget.May);
                            cmd.Parameters.AddWithValue("@June", _Budget.June);
                            cmd.Parameters.AddWithValue("@July", _Budget.July);
                            cmd.Parameters.AddWithValue("@August", _Budget.August);
                            cmd.Parameters.AddWithValue("@September", _Budget.September);
                            cmd.Parameters.AddWithValue("@October", _Budget.October);
                            cmd.Parameters.AddWithValue("@November", _Budget.November);
                            cmd.Parameters.AddWithValue("@December", _Budget.December);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _Budget.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _Budget.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update Budget set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,lastupdate=@lastupdate where BudgetPK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _Budget.BudgetPK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _Budget.EntryUsersID);
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
                                cmd.CommandText = "Update Budget set  Notes=@Notes,ReportPeriodPK=@ReportPeriodPK,PeriodPK=@PeriodPK,DepartmentPK=@DepartmentPK,AccountPK=@AccountPK,ItemBudgetPK=@ItemBudgetPK,Amount=@Amount,January=@January,February=@February,March=@March," +
                                    "April=@April,May=@May,June=@June,July=@July,August=@August,September=@September,October=@October,November=@November,December=@December,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,lastupdate=@lastupdate " +
                                    "where BudgetPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _Budget.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _Budget.BudgetPK);
                                cmd.Parameters.AddWithValue("@Notes", _Budget.Notes);
                                cmd.Parameters.AddWithValue("@ReportPeriodPK", _Budget.ReportPeriodPK);
                                cmd.Parameters.AddWithValue("@PeriodPK", _Budget.PeriodPK);
                                cmd.Parameters.AddWithValue("@DepartmentPK", _Budget.DepartmentPK);
                                cmd.Parameters.AddWithValue("@AccountPK", _Budget.AccountPK);
                                cmd.Parameters.AddWithValue("@ItemBudgetPK", _Budget.ItemBudgetPK);
                                cmd.Parameters.AddWithValue("@Amount", _Budget.Amount);
                                cmd.Parameters.AddWithValue("@January", _Budget.January);
                                cmd.Parameters.AddWithValue("@February", _Budget.February);
                                cmd.Parameters.AddWithValue("@March", _Budget.March);
                                cmd.Parameters.AddWithValue("@April", _Budget.April);
                                cmd.Parameters.AddWithValue("@May", _Budget.May);
                                cmd.Parameters.AddWithValue("@June", _Budget.June);
                                cmd.Parameters.AddWithValue("@July", _Budget.July);
                                cmd.Parameters.AddWithValue("@August", _Budget.August);
                                cmd.Parameters.AddWithValue("@September", _Budget.September);
                                cmd.Parameters.AddWithValue("@October", _Budget.October);
                                cmd.Parameters.AddWithValue("@November", _Budget.November);
                                cmd.Parameters.AddWithValue("@December", _Budget.December);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _Budget.EntryUsersID);
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
                                _newHisPK = _host.Get_NewHistoryPK(_Budget.BudgetPK, "Budget");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From Budget where BudgetPK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _Budget.BudgetPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _Budget.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@ReportPeriodPK", _Budget.ReportPeriodPK);
                                cmd.Parameters.AddWithValue("@PeriodPK", _Budget.PeriodPK);
                                cmd.Parameters.AddWithValue("@DepartmentPK", _Budget.DepartmentPK);
                                cmd.Parameters.AddWithValue("@AccountPK", _Budget.AccountPK);
                                cmd.Parameters.AddWithValue("@ItemBudgetPK", _Budget.ItemBudgetPK);
                                cmd.Parameters.AddWithValue("@Amount", _Budget.Amount);
                                cmd.Parameters.AddWithValue("@January", _Budget.January);
                                cmd.Parameters.AddWithValue("@February", _Budget.February);
                                cmd.Parameters.AddWithValue("@March", _Budget.March);
                                cmd.Parameters.AddWithValue("@April", _Budget.April);
                                cmd.Parameters.AddWithValue("@May", _Budget.May);
                                cmd.Parameters.AddWithValue("@June", _Budget.June);
                                cmd.Parameters.AddWithValue("@July", _Budget.July);
                                cmd.Parameters.AddWithValue("@August", _Budget.August);
                                cmd.Parameters.AddWithValue("@September", _Budget.September);
                                cmd.Parameters.AddWithValue("@October", _Budget.October);
                                cmd.Parameters.AddWithValue("@November", _Budget.November);
                                cmd.Parameters.AddWithValue("@December", _Budget.December);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _Budget.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update Budget set status= 4,Notes=@Notes," +
                                    "lastupdate=@lastupdate where BudgetPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _Budget.Notes);
                                cmd.Parameters.AddWithValue("@PK", _Budget.BudgetPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _Budget.HistoryPK);
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

        public void Budget_Approved(Budget _Budget)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update Budget set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,lastupdate=@lastupdate " +
                            "where BudgetPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _Budget.BudgetPK);
                        cmd.Parameters.AddWithValue("@historyPK", _Budget.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _Budget.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update Budget set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,lastupdate=@lastupdate where BudgetPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _Budget.BudgetPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _Budget.ApprovedUsersID);
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

        public void Budget_Reject(Budget _Budget)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update Budget set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,lastupdate=@lastupdate " +
                            "where BudgetPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _Budget.BudgetPK);
                        cmd.Parameters.AddWithValue("@historyPK", _Budget.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _Budget.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update Budget set status= 2,lastupdate=@lastupdate where BudgetPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _Budget.BudgetPK);
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

        public void Budget_Void(Budget _Budget)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update Budget set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,lastupdate=@lastupdate " +
                            "where BudgetPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _Budget.BudgetPK);
                        cmd.Parameters.AddWithValue("@historyPK", _Budget.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _Budget.VoidUsersID);
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

        public bool CheckHassAdd(int _departmentPK, int _accountPK)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    string date = System.DateTime.Today.ToString("MM/dd/yyyy");
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"update Budget set Status = 3 where DepartmentPK = @DepartmentPK
										    --and AccountPK = @AccountPK 
                                            and status = 2 ";
                        cmd.Parameters.AddWithValue("@DepartmentPK", _departmentPK);
                        cmd.Parameters.AddWithValue("@AccountPK", _accountPK);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (!dr.HasRows)
                            {
                                return false;
                            }
                            else
                            {
                                return true;
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


        public Boolean GenerateReportBudget(string _userID, BudgetRpt _BudgetRpt)
        {
            #region 01 Laporan Ledger Budget
            if (_BudgetRpt.ReportName.Equals("01 Laporan Ledger Budget"))
            {
                try
                {
                    using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                    {
                        DbCon.Open();
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {

                            cmd.CommandText =
                            @"Select isnull(B.ID,'') CoaNo
                                ,isnull(B.Name,'') CoaName
                                ,isnull(C.Name,'') CostCenter
                                ,isnull(D.Name,'') ItemBudget
                                ,A.January,A.February,A.March,A.April,A.May,A.June
                                ,A.July,A.August,A.September,A.October,A.November,A.December

                                from Budget A
                                left join Account B on A.AccountPK = B.AccountPK and B.Status In (1,2)
                                left join department C on A.DepartmentPK = C.DepartmentPK and C.status in (1,2)
                                left join ItemBudget D on A.ItemBudgetPK = D.ItemBudgetPK and D.status in (1,2)

                                where A.status = 2
                                and A.PeriodPK = @PeriodPK ";

                            cmd.CommandTimeout = 0;
                            cmd.Parameters.AddWithValue("@PeriodPK", _BudgetRpt.PeriodPK);


                            using (SqlDataReader dr0 = cmd.ExecuteReader())
                            {
                                if (!dr0.HasRows)
                                {
                                    return false;
                                }
                                else
                                {
                                    string filePath = Tools.ReportsPath + "LaporanLedgerBudgetRpt" + "_" + _userID + ".xlsx";
                                    string pdfPath = Tools.ReportsPath + "LaporanLedgerBudgetRpt" + "_" + _userID + ".pdf";
                                    FileInfo excelFile = new FileInfo(filePath);
                                    if (excelFile.Exists)
                                    {
                                        excelFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                        excelFile = new FileInfo(filePath);
                                    }


                                    // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                    using (ExcelPackage package = new ExcelPackage(excelFile))
                                    {
                                        package.Workbook.Properties.Title = "BudgetReport";
                                        package.Workbook.Properties.Author = Tools.DefaultReportAuthor();
                                        package.Workbook.Properties.Comments = Tools.DefaultReportComments();
                                        package.Workbook.Properties.Company = Tools.DefaultReportCompany();
                                        package.Workbook.Properties.SetCustomPropertyValue("Checked by", _userID);
                                        package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", Tools.DefaultReportAssemblyName());

                                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Budget Report");


                                        //ATUR DATA GROUPINGNYA DULU
                                        List<BudgetRpt> rList = new List<BudgetRpt>();
                                        while (dr0.Read())
                                        {
                                            BudgetRpt rSingle = new BudgetRpt();
                                            rSingle.CoaNo = Convert.ToString(dr0["CoaNo"]);
                                            rSingle.CoaName = Convert.ToString(dr0["CoaName"]);
                                            rSingle.CostCenter = Convert.ToString(dr0["CostCenter"]);
                                            rSingle.ItemBudget = Convert.ToString(dr0["ItemBudget"]);
                                            rSingle.January = Convert.ToDecimal(dr0["January"]);
                                            rSingle.February = Convert.ToDecimal(dr0["February"]);
                                            rSingle.March = Convert.ToDecimal(dr0["March"]);
                                            rSingle.April = Convert.ToDecimal(dr0["April"]);
                                            rSingle.May = Convert.ToDecimal(dr0["May"]);
                                            rSingle.June = Convert.ToDecimal(dr0["June"]);
                                            rSingle.July = Convert.ToDecimal(dr0["July"]);
                                            rSingle.August = Convert.ToDecimal(dr0["August"]);
                                            rSingle.September = Convert.ToDecimal(dr0["September"]);
                                            rSingle.October = Convert.ToDecimal(dr0["October"]);
                                            rSingle.November = Convert.ToDecimal(dr0["November"]);
                                            rSingle.December = Convert.ToDecimal(dr0["December"]);
                                            rList.Add(rSingle);

                                        }

                                        var QueryByClientID =
                                         from r in rList
                                         group r by new { r.CoaNo, r.CoaName } into rGroup
                                         select rGroup;

                                        int incRowExcel = 1;

                                        foreach (var rsHeader in QueryByClientID)
                                        {
                                            int _start = incRowExcel;
                                            int _end = incRowExcel + 1;
                                            worksheet.Cells["A" + _start + ":B" + _end].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + _start + ":B" + _end].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + _start + ":B" + _end].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + _start + ":B" + _end].Style.Border.Right.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells[incRowExcel, 1].Value = "Coa No : ";
                                            worksheet.Cells[incRowExcel, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                            worksheet.Cells[incRowExcel, 1].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                                            worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 2].Value = rsHeader.Key.CoaNo;
                                            worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            incRowExcel++;

                                            worksheet.Cells[incRowExcel, 1].Value = "Coa Name : ";
                                            worksheet.Cells[incRowExcel, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                            worksheet.Cells[incRowExcel, 1].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                                            worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 2].Value = rsHeader.Key.CoaName;
                                            worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                                            incRowExcel = incRowExcel + 2;
                                            //area header

                                            worksheet.Cells["B" + incRowExcel + ":P" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["B" + incRowExcel + ":P" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["B" + incRowExcel + ":P" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["B" + incRowExcel + ":P" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Medium;


                                            worksheet.Cells[incRowExcel, 2].Value = "Cost Center";
                                            worksheet.Cells[incRowExcel, 3].Value = "Item Budget";
                                            worksheet.Cells[incRowExcel, 4].Value = "JAN";
                                            worksheet.Cells[incRowExcel, 5].Value = "FEB";
                                            worksheet.Cells[incRowExcel, 6].Value = "MAR";
                                            worksheet.Cells[incRowExcel, 7].Value = "APR";
                                            worksheet.Cells[incRowExcel, 8].Value = "MAY";
                                            worksheet.Cells[incRowExcel, 9].Value = "JUN";
                                            worksheet.Cells[incRowExcel, 10].Value = "JUL";
                                            worksheet.Cells[incRowExcel, 11].Value = "AUG";
                                            worksheet.Cells[incRowExcel, 12].Value = "SEP";
                                            worksheet.Cells[incRowExcel, 13].Value = "OCT";
                                            worksheet.Cells[incRowExcel, 14].Value = "NOV";
                                            worksheet.Cells[incRowExcel, 15].Value = "DEC";
                                            worksheet.Cells[incRowExcel, 16].Value = "TOTAL";
                                            worksheet.Cells["B" + incRowExcel + ":P" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells["B" + incRowExcel + ":P" + incRowExcel].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                            worksheet.Cells["B" + incRowExcel + ":P" + incRowExcel].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                                            worksheet.Cells["B" + incRowExcel + ":P" + incRowExcel].Style.Font.Bold = true;
                                            incRowExcel++;
                                            int _startRowDetail = incRowExcel;
                                            int _endRowDetail = 0;
                                            foreach (var rsDetail in rsHeader)
                                            {


                                                //ThickBox Border

                                                //worksheet.Cells["A" + RowB + ":J" + RowG].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                //worksheet.Cells["A" + RowC + ":J" + RowC].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                //worksheet.Cells["A" + RowB + ":J" + RowG].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                //worksheet.Cells["A" + RowB + ":J" + RowG].Style.Border.Right.Style = ExcelBorderStyle.Medium;

                                                worksheet.Cells[incRowExcel, 2].Value = rsDetail.CostCenter;
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.ItemBudget;
                                                worksheet.Cells[incRowExcel, 4].Value = rsDetail.January;
                                                worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 5].Value = rsDetail.February;
                                                worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 6].Value = rsDetail.March;
                                                worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 7].Value = rsDetail.April;
                                                worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 8].Value = rsDetail.May;
                                                worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 9].Value = rsDetail.June;
                                                worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 10].Value = rsDetail.July;
                                                worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 11].Value = rsDetail.August;
                                                worksheet.Cells[incRowExcel, 11].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 12].Value = rsDetail.September;
                                                worksheet.Cells[incRowExcel, 12].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 13].Value = rsDetail.October;
                                                worksheet.Cells[incRowExcel, 13].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 14].Value = rsDetail.November;
                                                worksheet.Cells[incRowExcel, 14].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 15].Value = rsDetail.December;
                                                worksheet.Cells[incRowExcel, 15].Style.Numberformat.Format = "#,##0";

                                                worksheet.Cells[incRowExcel, 16].Formula = "SUM(D" + incRowExcel + ":O" + incRowExcel + ")";
                                                worksheet.Cells[incRowExcel, 16].Calculate();
                                                worksheet.Cells[incRowExcel, 16].Style.Numberformat.Format = "#,##0";

                                                _endRowDetail = incRowExcel;
                                                incRowExcel++;



                                            }

                                            worksheet.Cells["B" + _startRowDetail + ":P" + _endRowDetail].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["B" + _startRowDetail + ":P" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["B" + _startRowDetail + ":P" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["B" + _startRowDetail + ":P" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Medium;


                                            worksheet.Cells[incRowExcel, 3].Value = "TOTAL :";
                                            worksheet.Cells[incRowExcel, 3].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            worksheet.Cells[incRowExcel, 4].Formula = "SUM(D" + _startRowDetail + ":D" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 4].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 4].Calculate();
                                            worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##0";
                                            worksheet.Cells["C" + incRowExcel + ":E" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells[incRowExcel, 4].Formula = "SUM(D" + _startRowDetail + ":D" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 4].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 4].Calculate();
                                            worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##0";

                                            worksheet.Cells[incRowExcel, 5].Formula = "SUM(E" + _startRowDetail + ":E" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 5].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 5].Calculate();
                                            worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0";

                                            worksheet.Cells[incRowExcel, 6].Formula = "SUM(F" + _startRowDetail + ":F" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 6].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 6].Calculate();
                                            worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0";

                                            worksheet.Cells[incRowExcel, 7].Formula = "SUM(G" + _startRowDetail + ":G" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 7].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 7].Calculate();
                                            worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0";

                                            worksheet.Cells[incRowExcel, 8].Formula = "SUM(H" + _startRowDetail + ":H" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 8].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 8].Calculate();
                                            worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0";

                                            worksheet.Cells[incRowExcel, 9].Formula = "SUM(I" + _startRowDetail + ":I" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 9].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 9].Calculate();
                                            worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0";

                                            worksheet.Cells[incRowExcel, 10].Formula = "SUM(J" + _startRowDetail + ":J" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 10].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 10].Calculate();
                                            worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0";

                                            worksheet.Cells[incRowExcel, 11].Formula = "SUM(K" + _startRowDetail + ":K" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 11].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 11].Calculate();
                                            worksheet.Cells[incRowExcel, 11].Style.Numberformat.Format = "#,##0";

                                            worksheet.Cells[incRowExcel, 12].Formula = "SUM(L" + _startRowDetail + ":L" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 12].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 12].Calculate();
                                            worksheet.Cells[incRowExcel, 12].Style.Numberformat.Format = "#,##0";

                                            worksheet.Cells[incRowExcel, 13].Formula = "SUM(M" + _startRowDetail + ":M" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 13].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 13].Calculate();
                                            worksheet.Cells[incRowExcel, 13].Style.Numberformat.Format = "#,##0";

                                            worksheet.Cells[incRowExcel, 14].Formula = "SUM(N" + _startRowDetail + ":N" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 14].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 14].Calculate();
                                            worksheet.Cells[incRowExcel, 14].Style.Numberformat.Format = "#,##0";

                                            worksheet.Cells[incRowExcel, 15].Formula = "SUM(O" + _startRowDetail + ":O" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 15].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 15].Calculate();
                                            worksheet.Cells[incRowExcel, 15].Style.Numberformat.Format = "#,##0";

                                            worksheet.Cells[incRowExcel, 16].Formula = "SUM(P" + _startRowDetail + ":P" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 16].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 16].Calculate();
                                            worksheet.Cells[incRowExcel, 16].Style.Numberformat.Format = "#,##0";

                                            worksheet.Cells["F" + incRowExcel + ":P" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            incRowExcel = incRowExcel + 3;
                                        }



                                        worksheet.PrinterSettings.FitToPage = true;
                                        worksheet.PrinterSettings.FitToWidth = 1;
                                        worksheet.PrinterSettings.FitToHeight = 0;
                                        worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel - 1, 16];
                                        worksheet.Column(1).Width = 25;
                                        worksheet.Column(2).Width = 45;
                                        worksheet.Column(3).Width = 35;
                                        worksheet.Column(4).Width = 15;
                                        worksheet.Column(5).Width = 15;
                                        worksheet.Column(6).Width = 15;
                                        worksheet.Column(7).Width = 15;
                                        worksheet.Column(8).Width = 15;
                                        worksheet.Column(9).Width = 17;
                                        worksheet.Column(10).Width = 17;
                                        worksheet.Column(11).Width = 17;
                                        worksheet.Column(12).Width = 17;
                                        worksheet.Column(13).Width = 17;
                                        worksheet.Column(14).Width = 17;
                                        worksheet.Column(15).Width = 17;
                                        worksheet.Column(16).Width = 17;



                                        // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                        //worksheet.Cells["A3:O14"].AutoFitColumns();  // UNTUK AUTO FIT UKURAN TULISAN DENGAN KOLOM, KEBETULAN INI BUAT HEADER AJA DIPAKENYA

                                        worksheet.PrinterSettings.Orientation = eOrientation.Landscape; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                        worksheet.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN
                                        worksheet.HeaderFooter.OddHeader.RightAlignedText = "&14 LEDGER BUDGET REPORT";

                                        // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                        worksheet.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
                                        worksheet.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
                                        worksheet.View.PageLayoutView = Tools.DefaultReportPageLayoutView();
                                        worksheet.View.ShowGridLines = Tools.DefaultReportShowGridLines();

                                        Image img = Image.FromFile(Tools.ReportImage);
                                        worksheet.HeaderFooter.OddHeader.InsertPicture(img, PictureAlignment.Left);
                                        //worksheet.HeaderFooter.OddHeader.CenteredText = Tools.DefaultReportHeaderCenterText();
                                        //worksheet.HeaderFooter.OddHeader.LeftAlignedText = Tools.DefaultReportHeaderLeftText();

                                        worksheet.HeaderFooter.OddFooter.RightAlignedText =
                                        string.Format("Page {0} of {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);

                                        ////worksheet.HeaderFooter.OddFooter.CenteredText = Tools.DefaultReportFooterCenterText();
                                        //worksheet.HeaderFooter.OddFooter.LeftAlignedText = Tools.DefaultReportFooterLeftText();



                                        package.Save();
                                        return true;
                                    }
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
            #endregion

            #region 02 Laporan Per Coa Per Cabang
            if (_BudgetRpt.ReportName.Equals("02 Laporan Per Coa Per Cabang"))
            {
                try
                {
                    using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                    {
                        DbCon.Open();
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText =
                            @"Select 
                            Case when B.Type <=2 then 'NERACA' else 'PL' end RPT
                            ,E.DescOne CTG,isnull(B.ID,'') CoaNo
                            ,isnull(B.Name,'') CoaName
                            ,isnull(C.Name,'') CostCenter
                            ,isnull(F.ID,'') CurrencyID
                            ,isnull(D.Name,'') ItemBudget
                            ,isnull(G.TotalLastYear,0) TotalLastYear
                            ,isnull(A.January,0) January
                            ,isnull(A.February,0) February
                            ,isnull(A.March,0) March
                            ,isnull(A.April,0) April
                            ,isnull(A.May,0) May
                            ,isnull(A.June,0) June
                            ,isnull(A.July,0) July,isnull(A.August,0) August
                            ,isnull(A.September,0) September
                            ,isnull(A.October,0) October,isnull(A.November,0) November,isnull(A.December,0) December
                            from Budget A
                            left join Account B on A.AccountPK = B.AccountPK and B.Status In (1,2)
                            left join department C on A.DepartmentPK = C.DepartmentPK and C.status in (1,2)
                            left join ItemBudget D on A.ItemBudgetPK = D.ItemBudgetPK and D.status in (1,2)
                            left join MasterValue E on B.Type = E.Code and E.status in (1,2) and E.ID = 'AccountType'
                            left join Currency F on B.CurrencyPK = F.CurrencyPK and F.status in (1,2)
                            left join 
                            (
	                            select AccountPK,ItemBudgetPK 
	                            ,sum(isnull(January,0) 
	                            + isnull(February,0)
	                            + isnull(March,0)
	                            + isnull(April,0)
	                            + isnull(May,0)
	                            + isnull(June,0)
	                            + isnull(July,0)
	                            + isnull(August,0)
	                            + isnull(September,0)
	                            + isnull(October,0)
	                            + isnull(November,0)
	                            + isnull(December,0)) TotalLastYear
	                            from Budget where PeriodPK = @PeriodPK - 1 and status = 2
	                            group by AccountPK,ItemBudgetPK
                            )G on A.AccountPK = G.AccountPK
                            and A.ItemBudgetPK = G.ItemBudgetPK
                            where A.status = 2
                            and A.PeriodPK = @PeriodPK ";

                            cmd.CommandTimeout = 0;
                            cmd.Parameters.AddWithValue("@PeriodPK", _BudgetRpt.PeriodPK);


                            using (SqlDataReader dr0 = cmd.ExecuteReader())
                            {
                                if (!dr0.HasRows)
                                {
                                    return false;
                                }
                                else
                                {
                                    string filePath = Tools.ReportsPath + "LaporanPerCoaPerCabangRpt" + "_" + _userID + ".xlsx";
                                    string pdfPath = Tools.ReportsPath + "LaporanPerCoaPerCabangRpt" + "_" + _userID + ".pdf";
                                    FileInfo excelFile = new FileInfo(filePath);
                                    if (excelFile.Exists)
                                    {
                                        excelFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                        excelFile = new FileInfo(filePath);
                                    }


                                    // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                    using (ExcelPackage package = new ExcelPackage(excelFile))
                                    {
                                        package.Workbook.Properties.Title = "BudgetReport";
                                        package.Workbook.Properties.Author = Tools.DefaultReportAuthor();
                                        package.Workbook.Properties.Comments = Tools.DefaultReportComments();
                                        package.Workbook.Properties.Company = Tools.DefaultReportCompany();
                                        package.Workbook.Properties.SetCustomPropertyValue("Checked by", _userID);
                                        package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", Tools.DefaultReportAssemblyName());

                                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Budget Report");


                                        //ATUR DATA GROUPINGNYA DULU
                                        List<BudgetRpt> rList = new List<BudgetRpt>();
                                        while (dr0.Read())
                                        {
                                            BudgetRpt rSingle = new BudgetRpt();
                                            rSingle.CoaNo = Convert.ToString(dr0["CoaNo"]);
                                            rSingle.CoaName = Convert.ToString(dr0["CoaName"]);
                                            rSingle.RPT = Convert.ToString(dr0["RPT"]);
                                            rSingle.CTG = Convert.ToString(dr0["CTG"]);
                                            rSingle.CurrencyID = Convert.ToString(dr0["CurrencyID"]);
                                            rSingle.CostCenter = Convert.ToString(dr0["CostCenter"]);
                                            rSingle.ItemBudget = Convert.ToString(dr0["ItemBudget"]);
                                            rSingle.TotalLastYear = Convert.ToDecimal(dr0["TotalLastYear"]);
                                            rSingle.January = Convert.ToDecimal(dr0["January"]);
                                            rSingle.February = Convert.ToDecimal(dr0["February"]);
                                            rSingle.March = Convert.ToDecimal(dr0["March"]);
                                            rSingle.April = Convert.ToDecimal(dr0["April"]);
                                            rSingle.May = Convert.ToDecimal(dr0["May"]);
                                            rSingle.June = Convert.ToDecimal(dr0["June"]);
                                            rSingle.July = Convert.ToDecimal(dr0["July"]);
                                            rSingle.August = Convert.ToDecimal(dr0["August"]);
                                            rSingle.September = Convert.ToDecimal(dr0["September"]);
                                            rSingle.October = Convert.ToDecimal(dr0["October"]);
                                            rSingle.November = Convert.ToDecimal(dr0["November"]);
                                            rSingle.December = Convert.ToDecimal(dr0["December"]);
                                            rList.Add(rSingle);

                                        }

                                        var QueryByClientID =
                                         from r in rList
                                         group r by new { r.CostCenter } into rGroup
                                         select rGroup;

                                        int incRowExcel = 1;
                                        int period = Convert.ToInt32(_BudgetRpt.PeriodID);
                                        foreach (var rsHeader in QueryByClientID)
                                        {
                                            int _start = incRowExcel;
                                            worksheet.Cells["A" + incRowExcel + ":C" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + incRowExcel + ":C" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + incRowExcel + ":C" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + incRowExcel + ":C" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells[incRowExcel, 1].Value = "Branch Name : ";
                                            worksheet.Cells[incRowExcel, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                            worksheet.Cells[incRowExcel, 1].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                                            worksheet.Cells[incRowExcel, 3].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                            worksheet.Cells[incRowExcel, 3].Style.Fill.BackgroundColor.SetColor(Color.LightSalmon);
                                            worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells["A" + incRowExcel + ":B" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 3].Value = rsHeader.Key.CostCenter;
                                            worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            incRowExcel++;
                                            //area header

                                            worksheet.Cells["A" + incRowExcel + ":T" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + incRowExcel + ":T" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + incRowExcel + ":T" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + incRowExcel + ":T" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Medium;


                                            worksheet.Cells[incRowExcel, 1].Value = "No";
                                            worksheet.Cells[incRowExcel, 2].Value = "RPT";
                                            worksheet.Cells[incRowExcel, 3].Value = "CTG";
                                            worksheet.Cells[incRowExcel, 4].Value = "Account";
                                            worksheet.Cells[incRowExcel, 5].Value = "Item Budget";
                                            worksheet.Cells[incRowExcel, 6].Value = "Currency ID";
                                            worksheet.Cells[incRowExcel, 7].Value = "BUDGET " + Convert.ToInt32(period - 1);
                                            worksheet.Cells[incRowExcel, 8].Value = "JAN";
                                            worksheet.Cells[incRowExcel, 9].Value = "FEB";
                                            worksheet.Cells[incRowExcel, 10].Value = "MAR";
                                            worksheet.Cells[incRowExcel, 11].Value = "APR";
                                            worksheet.Cells[incRowExcel, 12].Value = "MAY";
                                            worksheet.Cells[incRowExcel, 13].Value = "JUN";
                                            worksheet.Cells[incRowExcel, 14].Value = "JUL";
                                            worksheet.Cells[incRowExcel, 15].Value = "AUG";
                                            worksheet.Cells[incRowExcel, 16].Value = "SEP";
                                            worksheet.Cells[incRowExcel, 17].Value = "OCT";
                                            worksheet.Cells[incRowExcel, 18].Value = "NOV";
                                            worksheet.Cells[incRowExcel, 19].Value = "DEC";
                                            worksheet.Cells[incRowExcel, 20].Value = "BUDGET " + Convert.ToInt32(period);

                                            worksheet.Cells["A" + incRowExcel + ":T" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells["A" + incRowExcel + ":T" + incRowExcel].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                            worksheet.Cells["A" + incRowExcel + ":T" + incRowExcel].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                                            worksheet.Cells["A" + incRowExcel + ":T" + incRowExcel].Style.Font.Bold = true;
                                            incRowExcel++;
                                            int _startRowDetail = incRowExcel;
                                            int _endRowDetail = 0;
                                            int _no = 1;
                                            foreach (var rsDetail in rsHeader)
                                            {


                                                //ThickBox Border

                                                //worksheet.Cells["A" + RowB + ":J" + RowG].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                //worksheet.Cells["A" + RowC + ":J" + RowC].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                //worksheet.Cells["A" + RowB + ":J" + RowG].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                //worksheet.Cells["A" + RowB + ":J" + RowG].Style.Border.Right.Style = ExcelBorderStyle.Medium;

                                                worksheet.Cells[incRowExcel, 1].Value = _no;
                                                worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 2].Value = rsDetail.RPT;
                                                worksheet.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.CTG;
                                                worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 4].Value = rsDetail.CoaNo + " - " + rsDetail.CoaName;
                                                worksheet.Cells[incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 5].Value = rsDetail.ItemBudget;
                                                worksheet.Cells[incRowExcel, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 6].Value = rsDetail.CurrencyID;
                                                worksheet.Cells[incRowExcel, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 7].Value = rsDetail.TotalLastYear;
                                                worksheet.Cells[incRowExcel, 8].Value = rsDetail.January;
                                                worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 9].Value = rsDetail.February;
                                                worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 10].Value = rsDetail.March;
                                                worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 11].Value = rsDetail.April;
                                                worksheet.Cells[incRowExcel, 11].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 12].Value = rsDetail.May;
                                                worksheet.Cells[incRowExcel, 12].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 13].Value = rsDetail.June;
                                                worksheet.Cells[incRowExcel, 13].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 14].Value = rsDetail.July;
                                                worksheet.Cells[incRowExcel, 14].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 15].Value = rsDetail.August;
                                                worksheet.Cells[incRowExcel, 15].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 16].Value = rsDetail.September;
                                                worksheet.Cells[incRowExcel, 16].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 17].Value = rsDetail.October;
                                                worksheet.Cells[incRowExcel, 17].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 18].Value = rsDetail.November;
                                                worksheet.Cells[incRowExcel, 18].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 19].Value = rsDetail.December;
                                                worksheet.Cells[incRowExcel, 19].Style.Numberformat.Format = "#,##0";

                                                worksheet.Cells[incRowExcel, 20].Formula = "SUM(H" + incRowExcel + ":S" + incRowExcel + ")";
                                                worksheet.Cells[incRowExcel, 20].Calculate();
                                                worksheet.Cells[incRowExcel, 20].Style.Numberformat.Format = "#,##0";
                                                _no++;
                                                _endRowDetail = incRowExcel;
                                                incRowExcel++;



                                            }

                                            worksheet.Cells["A" + _startRowDetail + ":T" + _endRowDetail].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + _startRowDetail + ":T" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + _startRowDetail + ":T" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + _startRowDetail + ":T" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Medium;


                                            worksheet.Cells[incRowExcel, 7].Value = "TOTAL :";
                                            worksheet.Cells[incRowExcel, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                                            worksheet.Cells[incRowExcel, 8].Formula = "SUM(H" + _startRowDetail + ":H" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 8].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 8].Calculate();
                                            worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0";

                                            worksheet.Cells[incRowExcel, 9].Formula = "SUM(I" + _startRowDetail + ":I" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 9].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 9].Calculate();
                                            worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0";

                                            worksheet.Cells[incRowExcel, 10].Formula = "SUM(J" + _startRowDetail + ":J" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 10].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 10].Calculate();
                                            worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0";

                                            worksheet.Cells[incRowExcel, 11].Formula = "SUM(K" + _startRowDetail + ":K" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 11].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 11].Calculate();
                                            worksheet.Cells[incRowExcel, 11].Style.Numberformat.Format = "#,##0";

                                            worksheet.Cells[incRowExcel, 12].Formula = "SUM(L" + _startRowDetail + ":L" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 12].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 12].Calculate();
                                            worksheet.Cells[incRowExcel, 12].Style.Numberformat.Format = "#,##0";

                                            worksheet.Cells[incRowExcel, 13].Formula = "SUM(M" + _startRowDetail + ":M" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 13].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 13].Calculate();
                                            worksheet.Cells[incRowExcel, 13].Style.Numberformat.Format = "#,##0";

                                            worksheet.Cells[incRowExcel, 14].Formula = "SUM(N" + _startRowDetail + ":N" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 14].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 14].Calculate();
                                            worksheet.Cells[incRowExcel, 14].Style.Numberformat.Format = "#,##0";

                                            worksheet.Cells[incRowExcel, 15].Formula = "SUM(O" + _startRowDetail + ":O" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 15].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 15].Calculate();
                                            worksheet.Cells[incRowExcel, 15].Style.Numberformat.Format = "#,##0";

                                            worksheet.Cells[incRowExcel, 16].Formula = "SUM(P" + _startRowDetail + ":P" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 16].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 16].Calculate();
                                            worksheet.Cells[incRowExcel, 16].Style.Numberformat.Format = "#,##0";

                                            worksheet.Cells[incRowExcel, 17].Formula = "SUM(Q" + _startRowDetail + ":Q" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 17].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 17].Calculate();
                                            worksheet.Cells[incRowExcel, 17].Style.Numberformat.Format = "#,##0";

                                            worksheet.Cells[incRowExcel, 18].Formula = "SUM(R" + _startRowDetail + ":R" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 18].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 18].Calculate();
                                            worksheet.Cells[incRowExcel, 18].Style.Numberformat.Format = "#,##0";

                                            worksheet.Cells[incRowExcel, 19].Formula = "SUM(S" + _startRowDetail + ":S" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 19].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 19].Calculate();
                                            worksheet.Cells[incRowExcel, 19].Style.Numberformat.Format = "#,##0";

                                            worksheet.Cells[incRowExcel, 20].Formula = "SUM(T" + _startRowDetail + ":T" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 20].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 20].Calculate();
                                            worksheet.Cells[incRowExcel, 20].Style.Numberformat.Format = "#,##0";


                                            worksheet.Cells["G" + incRowExcel + ":T" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                                            incRowExcel = incRowExcel + 3;
                                        }



                                        worksheet.PrinterSettings.FitToPage = true;
                                        worksheet.PrinterSettings.FitToWidth = 1;
                                        worksheet.PrinterSettings.FitToHeight = 0;
                                        worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel - 1, 20];
                                        worksheet.Column(1).Width = 5;
                                        worksheet.Column(2).Width = 15;
                                        worksheet.Column(3).Width = 20;
                                        worksheet.Column(4).Width = 45;
                                        worksheet.Column(5).Width = 45;
                                        worksheet.Column(6).Width = 15;
                                        worksheet.Column(7).Width = 17;
                                        worksheet.Column(8).Width = 17;
                                        worksheet.Column(9).Width = 17;
                                        worksheet.Column(10).Width = 17;
                                        worksheet.Column(11).Width = 17;
                                        worksheet.Column(12).Width = 17;
                                        worksheet.Column(13).Width = 17;
                                        worksheet.Column(14).Width = 17;
                                        worksheet.Column(15).Width = 17;
                                        worksheet.Column(16).Width = 17;
                                        worksheet.Column(17).Width = 17;
                                        worksheet.Column(18).Width = 17;
                                        worksheet.Column(19).Width = 17;
                                        worksheet.Column(20).Width = 17;



                                        // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                        //worksheet.Cells["A3:O14"].AutoFitColumns();  // UNTUK AUTO FIT UKURAN TULISAN DENGAN KOLOM, KEBETULAN INI BUAT HEADER AJA DIPAKENYA

                                        worksheet.PrinterSettings.Orientation = eOrientation.Landscape; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                        worksheet.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN
                                        worksheet.HeaderFooter.OddHeader.RightAlignedText = "&14 LAPORAN PER COA PER CABANG";

                                        // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                        worksheet.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
                                        worksheet.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
                                        worksheet.View.PageLayoutView = Tools.DefaultReportPageLayoutView();
                                        worksheet.View.ShowGridLines = Tools.DefaultReportShowGridLines();

                                        Image img = Image.FromFile(Tools.ReportImage);
                                        worksheet.HeaderFooter.OddHeader.InsertPicture(img, PictureAlignment.Left);
                                        //worksheet.HeaderFooter.OddHeader.CenteredText = Tools.DefaultReportHeaderCenterText();
                                        //worksheet.HeaderFooter.OddHeader.LeftAlignedText = Tools.DefaultReportHeaderLeftText();

                                        worksheet.HeaderFooter.OddFooter.RightAlignedText =
                                        string.Format("Page {0} of {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);

                                        ////worksheet.HeaderFooter.OddFooter.CenteredText = Tools.DefaultReportFooterCenterText();
                                        //worksheet.HeaderFooter.OddFooter.LeftAlignedText = Tools.DefaultReportFooterLeftText();



                                        package.Save();
                                        return true;
                                    }
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
            #endregion

            #region 03 Laporan FundFlow Cabang
            if (_BudgetRpt.ReportName.Equals("03 Laporan FundFlow Cabang"))
            {
                try
                {
                    using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                    {
                        DbCon.Open();
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {

                            cmd.CommandText =
                            @"
                            --drop table if exists dbo.#TempSelect
                            create table #TempSelect
                            (
	                            AgentPK int,
	                            ReksadanaTypePk int,
	                            InstrumentPK int,
	                            PeriodPK int,
	                            ReportPeriodPK int,
	                            CountYear int
                            )

                            --drop table if exists dbo.#TempReport
                            create table #TempReport
                            (
	                            BranchName nvarchar(100),
	                            Category nvarchar(100),
	                            InstrumentName nvarchar(100),
	                            January1 numeric(22,4),
	                            February1 numeric(22,4),
	                            March1 numeric(22,4),
	                            April1 numeric(22,4),
	                            May1 numeric(22,4),
	                            June1 numeric(22,4),
	                            July1 numeric(22,4),
	                            August1 numeric(22,4),
	                            September1 numeric(22,4),
	                            October1 numeric(22,4),
	                            November1 numeric(22,4),
	                            December1 numeric(22,4),
	                            January2 numeric(22,4),
	                            February2 numeric(22,4),
	                            March2 numeric(22,4),
	                            April2 numeric(22,4),
	                            May2 numeric(22,4),
	                            June2 numeric(22,4),
	                            July2 numeric(22,4),
	                            August2 numeric(22,4),
	                            September2 numeric(22,4),
	                            October2 numeric(22,4),
	                            November2 numeric(22,4),
	                            December2 numeric(22,4)
                            )

                            insert into #TempSelect(AgentPK,ReksadanaTypePk,InstrumentPK,PeriodPK,ReportPeriodPK)
                            select AgentPk,ReksadanaTypePK,InstrumentPK,PeriodPK,ReportPeriodPK from Revenue
                            where Status = 2 and ReportPeriodPK = @ReportPeriodpk

                            update A set A.CountYear = B.AB from #TempSelect A
                            left join (select AgentPK,ReksadanaTypePK,InstrumentPK,ReportPeriodPK,Count(*) AB from #TempSelect 
                            group by AgentPk,ReksadanaTypePK,InstrumentPk,ReportPeriodPK)B on A.AgentPK = B.AgentPK and A.ReksadanaTypePk = B.ReksadanaTypePk and A.InstrumentPK = B.InstrumentPK and A.ReportPeriodPK = b.ReportPeriodPK

                            DECLARE @AgentPK int, @ReksadanaTypePK int, @InstrumentPK int, @PeriodPK int, @CountYear int, @Counter int,
                            @PrevAgentPK int, @PrevReksanaTypePK int, @PrevInstrumentPK int
 
                             set @Counter = 1


                            DECLARE A CURSOR FOR 
	                            SELECT AgentPK,ReksadanaTypePk,InstrumentPK,A.PeriodPK,CountYear from #TempSelect A
	                            inner join Period B on A.PeriodPK = B.PeriodPK and B.Status = 2
	                            order by AgentPK,ReksadanaTypePk,InstrumentPK,B.ID
 
                            OPEN A
 
                            FETCH NEXT FROM A INTO @AgentPK, @ReksadanaTypePK, @InstrumentPK, @PeriodPK, @CountYear
 
                            WHILE @@FETCH_STATUS = 0
                                BEGIN
		                            if @Counter = 1
		                            begin
			                            insert into #TempReport(BranchName,Category,InstrumentName,January1,February1,March1,April1,May1,June1,July1,August1,September1,October1,November1,December1,January2,February2,March2,April2,May2,June2,July2,August2,September2,October2,November2,December2)
			                            Select 
			                            isnull(C.Name,'') BranchName
			                            ,isnull(D.DescOne,'') Category
			                            ,isnull(E.Name,'') InstrumentName
			                            ,isnull(sum(A.January),0) January
			                            ,isnull(sum(A.February),0) February
			                            ,isnull(sum(A.March),0) March
			                            ,isnull(sum(A.April),0) April
			                            ,isnull(sum(A.May),0) May
			                            ,isnull(sum(A.June),0) June
			                            ,isnull(sum(A.July),0) July,isnull(sum(A.August),0) August
			                            ,isnull(sum(A.September),0) September
			                            ,isnull(sum(A.October),0) October,isnull(sum(A.November),0) November,isnull(sum(A.December),0) December
			                            ,0,0,0,0,0,0,0,0,0,0,0,0

			                            from Revenue A
			                            left join Agent B on A.AgentPK = B.AgentPK and B.status in (1,2)
			                            left join Department C on B.DepartmentPK = C.DepartmentPK and C.status in (1,2)
			                            left join MasterValue D on A.ReksadanaTypePK = D.Code and D.id = 'FundType' and D.status in (1,2)
			                            left join Instrument E on A.InstrumentPK = E.InstrumentPK and E.Status in(1,2)
			                            where A.Status = 2 and A.AgentPK = @AgentPK and A.ReksadanaTypePK = @ReksadanaTypePK and A.InstrumentPK = @InstrumentPK and A.PeriodPK = @PeriodPK and A.ReportPeriodPK = @ReportPeriodpk
			                            group by C.name,D.Descone, E.Name

			                            set @Counter = 2
			                            set @PrevAgentPK = @AgentPK
			                            set @PrevInstrumentPK = @InstrumentPK
			                            set @PrevReksanaTypePK = @ReksadanaTypePK
		                            end
		                            else
		                            begin
			                            if (@PrevAgentPK = @AgentPK and @PrevReksanaTypePK = @ReksadanaTypePK and @PrevInstrumentPK = @InstrumentPK)
			                            begin
				                            update A set A.January2 = B.January, A.February2 = B.February, A.March2 = B.March, A.April2 = B.April, A.May2 = B.May, A.June2 = B.June, A.July2 = B.July,A.August2 = B.August, A.September2 = B.September, A.October2 = B.October, A.November2 = B.November,A.December2 = B.December from #TempReport A
				                            left join (
				                            Select 
				                            isnull(C.Name,'') BranchName
				                            ,isnull(D.DescOne,'') Category
				                            ,isnull(E.Name,'') InstrumentName
				                            ,isnull(sum(A.January),0) January
				                            ,isnull(sum(A.February),0) February
				                            ,isnull(sum(A.March),0) March
				                            ,isnull(sum(A.April),0) April
				                            ,isnull(sum(A.May),0) May
				                            ,isnull(sum(A.June),0) June
				                            ,isnull(sum(A.July),0) July,isnull(sum(A.August),0) August
				                            ,isnull(sum(A.September),0) September
				                            ,isnull(sum(A.October),0) October,isnull(sum(A.November),0) November,isnull(sum(A.December),0) December

				                            from Revenue A
				                            left join Agent B on A.AgentPK = B.AgentPK and B.status in (1,2)
				                            left join Department C on B.DepartmentPK = C.DepartmentPK and C.status in (1,2)
				                            left join MasterValue D on A.ReksadanaTypePK = D.Code and D.id = 'FundType' and D.status in (1,2)
				                            left join Instrument E on A.InstrumentPK = E.InstrumentPK and E.Status in(1,2)
				                            where A.Status = 2 and A.AgentPK = @AgentPK and A.ReksadanaTypePK = @ReksadanaTypePK and A.InstrumentPK = @InstrumentPK and A.PeriodPK = @PeriodPK and A.ReportPeriodPK = @ReportPeriodpk
				                            group by C.name,D.Descone, E.Name
				                            ) B on A.BranchName = B.BranchName and A.Category = B.Category and A.InstrumentName = B.InstrumentName
			                            end
			                            else
			                            begin
				                            insert into #TempReport(BranchName,Category,InstrumentName,January1,February1,March1,April1,May1,June1,July1,August1,September1,October1,November1,December1,January2,February2,March2,April2,May2,June2,July2,August2,September2,October2,November2,December2)
				                            Select 
				                            isnull(C.Name,'') BranchName
				                            ,isnull(D.DescOne,'') Category
				                            ,isnull(E.Name,'') InstrumentName
				                            ,isnull(sum(A.January),0) January
				                            ,isnull(sum(A.February),0) February
				                            ,isnull(sum(A.March),0) March
				                            ,isnull(sum(A.April),0) April
				                            ,isnull(sum(A.May),0) May
				                            ,isnull(sum(A.June),0) June
				                            ,isnull(sum(A.July),0) July,isnull(sum(A.August),0) August
				                            ,isnull(sum(A.September),0) September
				                            ,isnull(sum(A.October),0) October,isnull(sum(A.November),0) November,isnull(sum(A.December),0) December
				                            ,0,0,0,0,0,0,0,0,0,0,0,0

				                            from Revenue A
				                            left join Agent B on A.AgentPK = B.AgentPK and B.status in (1,2)
				                            left join Department C on B.DepartmentPK = C.DepartmentPK and C.status in (1,2)
				                            left join MasterValue D on A.ReksadanaTypePK = D.Code and D.id = 'FundType' and D.status in (1,2)
				                            left join Instrument E on A.InstrumentPK = E.InstrumentPK and E.Status in(1,2)
				                            where A.Status = 2 and A.AgentPK = @AgentPK and A.ReksadanaTypePK = @ReksadanaTypePK and A.InstrumentPK = @InstrumentPK and A.PeriodPK = @PeriodPK and A.ReportPeriodPK = @ReportPeriodpk
				                            group by C.name,D.Descone, E.Name

			                            end
			
				                            set @PrevAgentPK = @AgentPK
				                            set @PrevInstrumentPK = @InstrumentPK
				                            set @PrevReksanaTypePK = @ReksadanaTypePK
		                            end

		
		                            FETCH NEXT FROM A INTO @AgentPK, @ReksadanaTypePK, @InstrumentPK, @PeriodPK, @CountYear
                                END
 
                            CLOSE A
 
                            DEALLOCATE A

                            select * from #TempReport
";

                            cmd.CommandTimeout = 0;
                            cmd.Parameters.AddWithValue("@ReportPeriodpk", _BudgetRpt.PeriodPK);


                            using (SqlDataReader dr0 = cmd.ExecuteReader())
                            {
                                if (!dr0.HasRows)
                                {
                                    return false;
                                }
                                else
                                {
                                    string filePath = Tools.ReportsPath + "LaporanFundFlowCabang" + "_" + _userID + ".xlsx";
                                    string pdfPath = Tools.ReportsPath + "LaporanFundFlowCabang" + "_" + _userID + ".pdf";
                                    FileInfo excelFile = new FileInfo(filePath);
                                    if (excelFile.Exists)
                                    {
                                        excelFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                        excelFile = new FileInfo(filePath);
                                    }


                                    // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                    using (ExcelPackage package = new ExcelPackage(excelFile))
                                    {
                                        package.Workbook.Properties.Title = "BudgetReport";
                                        package.Workbook.Properties.Author = Tools.DefaultReportAuthor();
                                        package.Workbook.Properties.Comments = Tools.DefaultReportComments();
                                        package.Workbook.Properties.Company = Tools.DefaultReportCompany();
                                        package.Workbook.Properties.SetCustomPropertyValue("Checked by", _userID);
                                        package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", Tools.DefaultReportAssemblyName());

                                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Budget Report");


                                        //ATUR DATA GROUPINGNYA DULU
                                        List<BudgetRpt> rList = new List<BudgetRpt>();
                                        while (dr0.Read())
                                        {
                                            BudgetRpt rSingle = new BudgetRpt();
                                            rSingle.BranchName = Convert.ToString(dr0["BranchName"]);
                                            rSingle.Category = Convert.ToString(dr0["Category"]);
                                            rSingle.InstrumentName = Convert.ToString(dr0["InstrumentName"]);
                                            rSingle.January1 = Convert.ToDecimal(dr0["January1"]);
                                            rSingle.February1 = Convert.ToDecimal(dr0["February1"]);
                                            rSingle.March1 = Convert.ToDecimal(dr0["March1"]);
                                            rSingle.April1 = Convert.ToDecimal(dr0["April1"]);
                                            rSingle.May1 = Convert.ToDecimal(dr0["May1"]);
                                            rSingle.June1 = Convert.ToDecimal(dr0["June1"]);
                                            rSingle.July1 = Convert.ToDecimal(dr0["July1"]);
                                            rSingle.August1 = Convert.ToDecimal(dr0["August1"]);
                                            rSingle.September1 = Convert.ToDecimal(dr0["September1"]);
                                            rSingle.October1 = Convert.ToDecimal(dr0["October1"]);
                                            rSingle.November1 = Convert.ToDecimal(dr0["November1"]);
                                            rSingle.December1 = Convert.ToDecimal(dr0["December1"]);
                                            rSingle.January2 = Convert.ToDecimal(dr0["January2"]);
                                            rSingle.February2 = Convert.ToDecimal(dr0["February2"]);
                                            rSingle.March2 = Convert.ToDecimal(dr0["March2"]);
                                            rSingle.April2 = Convert.ToDecimal(dr0["April2"]);
                                            rSingle.May2 = Convert.ToDecimal(dr0["May2"]);
                                            rSingle.June2 = Convert.ToDecimal(dr0["June2"]);
                                            rSingle.July2 = Convert.ToDecimal(dr0["July2"]);
                                            rSingle.August2 = Convert.ToDecimal(dr0["August2"]);
                                            rSingle.September2 = Convert.ToDecimal(dr0["September2"]);
                                            rSingle.October2 = Convert.ToDecimal(dr0["October2"]);
                                            rSingle.November2 = Convert.ToDecimal(dr0["November2"]);
                                            rSingle.December2 = Convert.ToDecimal(dr0["December2"]);
                                            rList.Add(rSingle);

                                        }

                                        var QueryByClientID =
                                         from r in rList
                                         group r by new { r.BranchName } into rGroup
                                         select rGroup;

                                        int incRowExcel = 1;

                                        foreach (var rsHeader in QueryByClientID)
                                        {
                                            int _start = incRowExcel;
                                            int _end = incRowExcel + 1;
                                            worksheet.Cells["A" + _start + ":C" + _end].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + _start + ":C" + _end].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + _start + ":C" + _end].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + _start + ":C" + _end].Style.Border.Right.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells[incRowExcel, 1].Value = "Division Name :";
                                            worksheet.Cells[incRowExcel, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                            worksheet.Cells[incRowExcel, 1].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                                            worksheet.Cells[incRowExcel, 3].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                            worksheet.Cells[incRowExcel, 3].Style.Fill.BackgroundColor.SetColor(Color.LightSalmon);
                                            worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells["A" + incRowExcel + ":B" + incRowExcel].Merge = true;
                                            //worksheet.Cells[incRowExcel, 2].Value = rsHeader.Key.CoaNo;
                                            worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            incRowExcel++;

                                            worksheet.Cells[incRowExcel, 1].Value = "Branch Name : ";
                                            worksheet.Cells[incRowExcel, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                            worksheet.Cells[incRowExcel, 1].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                                            worksheet.Cells[incRowExcel, 3].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                            worksheet.Cells[incRowExcel, 3].Style.Fill.BackgroundColor.SetColor(Color.LightSalmon);
                                            worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells["A" + incRowExcel + ":B" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 3].Value = rsHeader.Key.BranchName;
                                            worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                                            incRowExcel = incRowExcel + 2;
                                            //area header

                                            worksheet.Cells["A" + incRowExcel + ":AB" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + incRowExcel + ":AB" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + incRowExcel + ":AB" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + incRowExcel + ":AB" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Medium;


                                            worksheet.Cells[incRowExcel, 1].Value = "No";
                                            worksheet.Cells[incRowExcel, 2].Value = "Category 1";
                                            worksheet.Cells[incRowExcel, 3].Value = "Product";
                                            worksheet.Cells[incRowExcel, 4].Value = "JAN " + _host.Get_PeriodByRevenue(_BudgetRpt.PeriodPK, 1);
                                            worksheet.Cells[incRowExcel, 5].Value = "FEB " + _host.Get_PeriodByRevenue(_BudgetRpt.PeriodPK, 1);
                                            worksheet.Cells[incRowExcel, 6].Value = "MAR " + _host.Get_PeriodByRevenue(_BudgetRpt.PeriodPK, 1);
                                            worksheet.Cells[incRowExcel, 7].Value = "APR " + _host.Get_PeriodByRevenue(_BudgetRpt.PeriodPK, 1);
                                            worksheet.Cells[incRowExcel, 8].Value = "MAY " + _host.Get_PeriodByRevenue(_BudgetRpt.PeriodPK, 1);
                                            worksheet.Cells[incRowExcel, 9].Value = "JUN " + _host.Get_PeriodByRevenue(_BudgetRpt.PeriodPK, 1);
                                            worksheet.Cells[incRowExcel, 10].Value = "JUL " + _host.Get_PeriodByRevenue(_BudgetRpt.PeriodPK, 1);
                                            worksheet.Cells[incRowExcel, 11].Value = "AUG " + _host.Get_PeriodByRevenue(_BudgetRpt.PeriodPK, 1);
                                            worksheet.Cells[incRowExcel, 12].Value = "SEP " + _host.Get_PeriodByRevenue(_BudgetRpt.PeriodPK, 1);
                                            worksheet.Cells[incRowExcel, 13].Value = "OCT " + _host.Get_PeriodByRevenue(_BudgetRpt.PeriodPK, 1);
                                            worksheet.Cells[incRowExcel, 14].Value = "NOV " + _host.Get_PeriodByRevenue(_BudgetRpt.PeriodPK, 1);
                                            worksheet.Cells[incRowExcel, 15].Value = "DEC " + _host.Get_PeriodByRevenue(_BudgetRpt.PeriodPK, 1);

                                            worksheet.Cells[incRowExcel, 16].Value = "JAN " + _host.Get_PeriodByRevenue(_BudgetRpt.PeriodPK, 0);
                                            worksheet.Cells[incRowExcel, 17].Value = "FEB " + _host.Get_PeriodByRevenue(_BudgetRpt.PeriodPK, 0);
                                            worksheet.Cells[incRowExcel, 18].Value = "MAR " + _host.Get_PeriodByRevenue(_BudgetRpt.PeriodPK, 0);
                                            worksheet.Cells[incRowExcel, 19].Value = "APR " + _host.Get_PeriodByRevenue(_BudgetRpt.PeriodPK, 0);
                                            worksheet.Cells[incRowExcel, 20].Value = "MAY " + _host.Get_PeriodByRevenue(_BudgetRpt.PeriodPK, 0);
                                            worksheet.Cells[incRowExcel, 21].Value = "JUN " + _host.Get_PeriodByRevenue(_BudgetRpt.PeriodPK, 0);
                                            worksheet.Cells[incRowExcel, 22].Value = "JUL " + _host.Get_PeriodByRevenue(_BudgetRpt.PeriodPK, 0);
                                            worksheet.Cells[incRowExcel, 23].Value = "AUG " + _host.Get_PeriodByRevenue(_BudgetRpt.PeriodPK, 0);
                                            worksheet.Cells[incRowExcel, 24].Value = "SEP " + _host.Get_PeriodByRevenue(_BudgetRpt.PeriodPK, 0);
                                            worksheet.Cells[incRowExcel, 25].Value = "OCT " + _host.Get_PeriodByRevenue(_BudgetRpt.PeriodPK, 0);
                                            worksheet.Cells[incRowExcel, 26].Value = "NOV " + _host.Get_PeriodByRevenue(_BudgetRpt.PeriodPK, 0);
                                            worksheet.Cells[incRowExcel, 27].Value = "DEC " + _host.Get_PeriodByRevenue(_BudgetRpt.PeriodPK, 0);
                                            worksheet.Cells[incRowExcel, 28].Value = "TOTAL";

                                            worksheet.Cells["A" + incRowExcel + ":AB" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells["A" + incRowExcel + ":AB" + incRowExcel].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                            worksheet.Cells["A" + incRowExcel + ":AB" + incRowExcel].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                                            worksheet.Cells["A" + incRowExcel + ":AB" + incRowExcel].Style.Font.Bold = true;
                                            incRowExcel++;
                                            int _startRowDetail = incRowExcel;
                                            int _endRowDetail = 0;
                                            int _no = 1;
                                            foreach (var rsDetail in rsHeader)
                                            {


                                                //ThickBox Border

                                                //worksheet.Cells["A" + RowB + ":J" + RowG].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                                //worksheet.Cells["A" + RowC + ":J" + RowC].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                                //worksheet.Cells["A" + RowB + ":J" + RowG].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                                //worksheet.Cells["A" + RowB + ":J" + RowG].Style.Border.Right.Style = ExcelBorderStyle.Medium;

                                                worksheet.Cells[incRowExcel, 1].Value = _no;
                                                worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 2].Value = rsDetail.Category;
                                                worksheet.Cells[incRowExcel, 2].Style.WrapText = true;
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.Category;
                                                worksheet.Cells[incRowExcel, 4].Value = rsDetail.January1;
                                                worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 5].Value = rsDetail.February1;
                                                worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 6].Value = rsDetail.March1;
                                                worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 7].Value = rsDetail.April1;
                                                worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 8].Value = rsDetail.May1;
                                                worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 9].Value = rsDetail.June1;
                                                worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 10].Value = rsDetail.July1;
                                                worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 11].Value = rsDetail.August1;
                                                worksheet.Cells[incRowExcel, 11].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 12].Value = rsDetail.September1;
                                                worksheet.Cells[incRowExcel, 12].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 13].Value = rsDetail.October1;
                                                worksheet.Cells[incRowExcel, 13].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 14].Value = rsDetail.November1;
                                                worksheet.Cells[incRowExcel, 14].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 15].Value = rsDetail.December1;
                                                worksheet.Cells[incRowExcel, 15].Style.Numberformat.Format = "#,##0";

                                                worksheet.Cells[incRowExcel, 16].Value = rsDetail.January2;
                                                worksheet.Cells[incRowExcel, 16].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 17].Value = rsDetail.February2;
                                                worksheet.Cells[incRowExcel, 17].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 18].Value = rsDetail.March2;
                                                worksheet.Cells[incRowExcel, 18].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 19].Value = rsDetail.April2;
                                                worksheet.Cells[incRowExcel, 19].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 20].Value = rsDetail.May2;
                                                worksheet.Cells[incRowExcel, 20].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 21].Value = rsDetail.June2;
                                                worksheet.Cells[incRowExcel, 21].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 22].Value = rsDetail.July2;
                                                worksheet.Cells[incRowExcel, 22].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 23].Value = rsDetail.August2;
                                                worksheet.Cells[incRowExcel, 23].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 24].Value = rsDetail.September2;
                                                worksheet.Cells[incRowExcel, 24].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 25].Value = rsDetail.October2;
                                                worksheet.Cells[incRowExcel, 25].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 26].Value = rsDetail.November2;
                                                worksheet.Cells[incRowExcel, 26].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 27].Value = rsDetail.December2;
                                                worksheet.Cells[incRowExcel, 27].Style.Numberformat.Format = "#,##0";


                                                worksheet.Cells[incRowExcel, 28].Formula = "SUM(D" + incRowExcel + ":AA" + incRowExcel + ")";
                                                worksheet.Cells[incRowExcel, 28].Calculate();
                                                worksheet.Cells[incRowExcel, 28].Style.Numberformat.Format = "#,##0";
                                                _no++;
                                                _endRowDetail = incRowExcel;
                                                incRowExcel++;



                                            }

                                            worksheet.Cells["A" + _startRowDetail + ":AB" + _endRowDetail].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + _startRowDetail + ":AB" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + _startRowDetail + ":AB" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + _startRowDetail + ":AB" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Medium;


                                            worksheet.Cells[incRowExcel, 3].Value = "TOTAL :";
                                            worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                                            worksheet.Cells[incRowExcel, 4].Formula = "SUM(D" + _startRowDetail + ":D" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 4].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 4].Calculate();
                                            worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##0";

                                            worksheet.Cells[incRowExcel, 5].Formula = "SUM(E" + _startRowDetail + ":E" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 5].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 5].Calculate();
                                            worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0";

                                            worksheet.Cells[incRowExcel, 6].Formula = "SUM(F" + _startRowDetail + ":F" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 6].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 6].Calculate();
                                            worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0";

                                            worksheet.Cells[incRowExcel, 7].Formula = "SUM(G" + _startRowDetail + ":G" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 7].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 7].Calculate();
                                            worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0";

                                            worksheet.Cells[incRowExcel, 8].Formula = "SUM(H" + _startRowDetail + ":H" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 8].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 8].Calculate();
                                            worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0";

                                            worksheet.Cells[incRowExcel, 9].Formula = "SUM(I" + _startRowDetail + ":I" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 9].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 9].Calculate();
                                            worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0";

                                            worksheet.Cells[incRowExcel, 10].Formula = "SUM(J" + _startRowDetail + ":J" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 10].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 10].Calculate();
                                            worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0";

                                            worksheet.Cells[incRowExcel, 11].Formula = "SUM(K" + _startRowDetail + ":K" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 11].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 11].Calculate();
                                            worksheet.Cells[incRowExcel, 11].Style.Numberformat.Format = "#,##0";

                                            worksheet.Cells[incRowExcel, 12].Formula = "SUM(L" + _startRowDetail + ":L" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 12].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 12].Calculate();
                                            worksheet.Cells[incRowExcel, 12].Style.Numberformat.Format = "#,##0";

                                            worksheet.Cells[incRowExcel, 13].Formula = "SUM(M" + _startRowDetail + ":M" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 13].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 13].Calculate();
                                            worksheet.Cells[incRowExcel, 13].Style.Numberformat.Format = "#,##0";

                                            worksheet.Cells[incRowExcel, 14].Formula = "SUM(N" + _startRowDetail + ":N" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 14].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 14].Calculate();
                                            worksheet.Cells[incRowExcel, 14].Style.Numberformat.Format = "#,##0";

                                            worksheet.Cells[incRowExcel, 15].Formula = "SUM(O" + _startRowDetail + ":O" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 15].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 15].Calculate();
                                            worksheet.Cells[incRowExcel, 15].Style.Numberformat.Format = "#,##0";

                                            worksheet.Cells[incRowExcel, 16].Formula = "SUM(P" + _startRowDetail + ":P" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 16].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 16].Calculate();
                                            worksheet.Cells[incRowExcel, 16].Style.Numberformat.Format = "#,##0";

                                            worksheet.Cells[incRowExcel, 17].Formula = "SUM(Q" + _startRowDetail + ":Q" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 17].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 17].Calculate();
                                            worksheet.Cells[incRowExcel, 17].Style.Numberformat.Format = "#,##0";

                                            worksheet.Cells[incRowExcel, 18].Formula = "SUM(R" + _startRowDetail + ":R" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 18].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 18].Calculate();
                                            worksheet.Cells[incRowExcel, 18].Style.Numberformat.Format = "#,##0";

                                            worksheet.Cells[incRowExcel, 19].Formula = "SUM(S" + _startRowDetail + ":S" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 19].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 19].Calculate();
                                            worksheet.Cells[incRowExcel, 19].Style.Numberformat.Format = "#,##0";

                                            worksheet.Cells[incRowExcel, 20].Formula = "SUM(T" + _startRowDetail + ":T" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 20].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 20].Calculate();
                                            worksheet.Cells[incRowExcel, 20].Style.Numberformat.Format = "#,##0";

                                            worksheet.Cells[incRowExcel, 21].Formula = "SUM(U" + _startRowDetail + ":U" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 21].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 21].Calculate();
                                            worksheet.Cells[incRowExcel, 21].Style.Numberformat.Format = "#,##0";

                                            worksheet.Cells[incRowExcel, 22].Formula = "SUM(V" + _startRowDetail + ":V" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 22].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 22].Calculate();
                                            worksheet.Cells[incRowExcel, 22].Style.Numberformat.Format = "#,##0";

                                            worksheet.Cells[incRowExcel, 23].Formula = "SUM(W" + _startRowDetail + ":W" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 23].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 23].Calculate();
                                            worksheet.Cells[incRowExcel, 23].Style.Numberformat.Format = "#,##0";

                                            worksheet.Cells[incRowExcel, 24].Formula = "SUM(X" + _startRowDetail + ":X" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 24].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 24].Calculate();
                                            worksheet.Cells[incRowExcel, 24].Style.Numberformat.Format = "#,##0";

                                            worksheet.Cells[incRowExcel, 25].Formula = "SUM(Y" + _startRowDetail + ":Y" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 25].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 25].Calculate();
                                            worksheet.Cells[incRowExcel, 25].Style.Numberformat.Format = "#,##0";

                                            worksheet.Cells[incRowExcel, 26].Formula = "SUM(Z" + _startRowDetail + ":Z" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 26].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 26].Calculate();
                                            worksheet.Cells[incRowExcel, 26].Style.Numberformat.Format = "#,##0";

                                            worksheet.Cells[incRowExcel, 27].Formula = "SUM(AA" + _startRowDetail + ":AA" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 27].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 27].Calculate();
                                            worksheet.Cells[incRowExcel, 27].Style.Numberformat.Format = "#,##0";

                                            worksheet.Cells[incRowExcel, 28].Formula = "SUM(AB" + _startRowDetail + ":AB" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 28].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 28].Calculate();
                                            worksheet.Cells[incRowExcel, 28].Style.Numberformat.Format = "#,##0";

                                            worksheet.Cells["C" + incRowExcel + ":AB" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["C" + incRowExcel + ":AB" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["C" + incRowExcel + ":AB" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Medium;

                                            incRowExcel = incRowExcel + 3;
                                        }



                                        worksheet.PrinterSettings.FitToPage = true;
                                        worksheet.PrinterSettings.FitToWidth = 1;
                                        worksheet.PrinterSettings.FitToHeight = 0;
                                        worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel - 1, 28];
                                        worksheet.Column(1).Width = 7;
                                        worksheet.Column(2).Width = 25;
                                        worksheet.Column(3).Width = 25;
                                        worksheet.Column(4).Width = 17;
                                        worksheet.Column(5).Width = 17;
                                        worksheet.Column(6).Width = 17;
                                        worksheet.Column(7).Width = 17;
                                        worksheet.Column(8).Width = 17;
                                        worksheet.Column(9).Width = 17;
                                        worksheet.Column(10).Width = 17;
                                        worksheet.Column(11).Width = 17;
                                        worksheet.Column(12).Width = 17;
                                        worksheet.Column(13).Width = 17;
                                        worksheet.Column(14).Width = 17;
                                        worksheet.Column(15).Width = 17;
                                        worksheet.Column(16).Width = 17;
                                        worksheet.Column(17).Width = 17;
                                        worksheet.Column(18).Width = 17;
                                        worksheet.Column(19).Width = 17;
                                        worksheet.Column(20).Width = 17;
                                        worksheet.Column(21).Width = 17;
                                        worksheet.Column(22).Width = 17;
                                        worksheet.Column(23).Width = 17;
                                        worksheet.Column(24).Width = 17;
                                        worksheet.Column(25).Width = 17;
                                        worksheet.Column(26).Width = 17;
                                        worksheet.Column(27).Width = 17;
                                        worksheet.Column(28).Width = 17;



                                        // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                        //worksheet.Cells["A3:O14"].AutoFitColumns();  // UNTUK AUTO FIT UKURAN TULISAN DENGAN KOLOM, KEBETULAN INI BUAT HEADER AJA DIPAKENYA

                                        worksheet.PrinterSettings.Orientation = eOrientation.Landscape; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                        worksheet.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN
                                        worksheet.HeaderFooter.OddHeader.RightAlignedText = "&14 LAPORAN FUNDFLOW CABANG";

                                        // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                        worksheet.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
                                        worksheet.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
                                        worksheet.View.PageLayoutView = Tools.DefaultReportPageLayoutView();
                                        worksheet.View.ShowGridLines = Tools.DefaultReportShowGridLines();

                                        Image img = Image.FromFile(Tools.ReportImage);
                                        worksheet.HeaderFooter.OddHeader.InsertPicture(img, PictureAlignment.Left);
                                        //worksheet.HeaderFooter.OddHeader.CenteredText = Tools.DefaultReportHeaderCenterText();
                                        //worksheet.HeaderFooter.OddHeader.LeftAlignedText = Tools.DefaultReportHeaderLeftText();

                                        worksheet.HeaderFooter.OddFooter.RightAlignedText =
                                        string.Format("Page {0} of {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);

                                        ////worksheet.HeaderFooter.OddFooter.CenteredText = Tools.DefaultReportFooterCenterText();
                                        //worksheet.HeaderFooter.OddFooter.LeftAlignedText = Tools.DefaultReportFooterLeftText();



                                        package.Save();
                                        return true;
                                    }
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
            #endregion

            #region 04 Laporan FundFlow Per Sales
            if (_BudgetRpt.ReportName.Equals("04 Laporan FundFlow Per Sales"))
            {
                try
                {
                    string filePath = Tools.ReportsPath + "LaporanFundFlowPerSales" + "_" + _userID + ".xlsx";
                    string pdfPath = Tools.ReportsPath + "LaporanFundFlowPerSales" + "_" + _userID + ".pdf";
                    FileInfo excelFile = new FileInfo(filePath);
                    if (excelFile.Exists)
                    {
                        excelFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                        excelFile = new FileInfo(filePath);
                    }


                    // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                    using (ExcelPackage package = new ExcelPackage(excelFile))
                    {
                        using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                        {
                            DbCon.Open();
                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {

                                cmd.CommandText =
                                @"
                                DECLARE @PeriodPKTo INT

                                Declare @PeriodID nvarchar(50)
                                declare @RevenuePercent numeric(10,4)

                                select @PeriodID  = YEAR(dateadd(year, -1, ID)) From Period where PeriodPK = @PeriodPK and status in (1,2)

                                declare @tableRevenue table
                                (
	                                PeriodPK int,
	                                ReportPeriodPK int,
	                                InstrumentPK int,
	                                AgentPK int,
	                                MGTFee numeric(10,4),
	                                January NUMERIC(22,4),
	                                February NUMERIC(22,4),
	                                March NUMERIC(22,4),
	                                April NUMERIC(22,4),
	                                May NUMERIC(22,4),
	                                June NUMERIC(22,4),
	                                July NUMERIC(22,4),
	                                August NUMERIC(22,4),
	                                September NUMERIC(22,4),
	                                October NUMERIC(22,4),
	                                November NUMERIC(22,4),
	                                December NUMERIC(22,4)
                                )

                                insert into @tableRevenue
                                select A.PeriodPK,A.ReportPeriodPK,A.InstrumentPK,AgentPK,MGTFee,
							    January*case when B.ReksadanaTypePK in (5,6,7) then 1 else isnull(C.RevenuePercent/100,1) end,February*case when B.ReksadanaTypePK in (5,6,7) then 1 else isnull(C.RevenuePercent/100,1) end,
							    March*case when B.ReksadanaTypePK in (5,6,7) then 1 else isnull(C.RevenuePercent/100,1) end,April*case when B.ReksadanaTypePK in (5,6,7) then 1 else isnull(C.RevenuePercent/100,1) end,
							    May*case when B.ReksadanaTypePK in (5,6,7) then 1 else isnull(C.RevenuePercent/100,1) end,June*case when B.ReksadanaTypePK in (5,6,7) then 1 else isnull(C.RevenuePercent/100,1) end,
							    July*case when B.ReksadanaTypePK in (5,6,7) then 1 else isnull(C.RevenuePercent/100,1) end,August*case when B.ReksadanaTypePK in (5,6,7) then 1 else isnull(C.RevenuePercent/100,1) end,
							    September*case when B.ReksadanaTypePK in (5,6,7) then 1 else isnull(C.RevenuePercent/100,1) end,October*case when B.ReksadanaTypePK in (5,6,7) then 1 else isnull(C.RevenuePercent/100,1) end,
							    November*case when B.ReksadanaTypePK in (5,6,7) then 1 else isnull(C.RevenuePercent/100,1) end,December*case when B.ReksadanaTypePK in (5,6,7) then 1 else isnull(C.RevenuePercent/100,1) end 
                                from Revenue A
							    left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status in (1,2)
								left join BudgetVersioning C on A.PeriodPK = C.PeriodPK and C.ReportPeriodPK = C.ReportPeriodPK and C.Status = 2 and Date = (
	                                select max(date) from BudgetVersioning where status in (1,2) and ReportPeriodPK = @periodpk
                                )
							    where A.status = 2 and A.ReportPeriodPK = @periodpk
								
							    select @PeriodPKTo = max(periodpk) from Revenue where ReportPeriodPK = @PeriodPK and status = 2

                                select 
								isnull(C.Name,'') SalesName, isnull(D.Name,'') InstrumentName,isnull(F.Description,'') Category,isnull(E.Name,'') BranchName,isnull(B.TotalYTD,0) YTDDes,
								sum(A.January) January,sum(A.February) February,sum(A.March) March,sum(A.April) April,sum(A.May) May,sum(A.June) June,sum(A.July) July,
								sum(A.August) August,sum(A.September) September,sum(A.October) October,sum(A.November) November,sum(A.December) December
								from @tableRevenue A
								LEFT JOIN
								(
									SELECT A.InstrumentPK,A.AgentPK,sum(isnull(A.January,0) + isnull(A.February,0) + isnull(A.March,0) + isnull(A.April,0) + isnull(A.May,0) + isnull(A.June,0) + 
									isnull(A.July,0) + isnull(A.August,0) + isnull(A.September,0) + isnull(A.October,0) + isnull(A.November,0) + isnull(A.December,0)) TotalYTD FROM  @tableRevenue A
									WHERE A.ReportPeriodPK = @PeriodPK AND A.PeriodPK <> @PeriodPKTo 
									group by A.InstrumentPK,AgentPK
								)B ON A.AgentPK = B.AgentPK AND A.InstrumentPK = B.InstrumentPK
                                left join Agent C on A.AgentPK = C.AgentPK and C.Status = 2
                                left join Instrument D on A.InstrumentPK = D.InstrumentPK and D.Status = 2
                                left join Department E on C.DepartmentPK = E.DepartmentPK and E.Status = 2
                                left join ReksadanaType F on D.ReksadanaTypePK = F.ReksadanaTypePK  and F.status in (1,2)
								where A.ReportPeriodPK = @PeriodPK and A.PeriodPK = @PeriodPKTo
								GROUP BY A.InstrumentPK,C.Name,D.Name,F.Description,E.Name,B.TotalYTD
								ORDER BY A.InstrumentPK

    ";

                                cmd.CommandTimeout = 0;
                                cmd.Parameters.AddWithValue("@PeriodPK", _BudgetRpt.PeriodPK);


                                using (SqlDataReader dr0 = cmd.ExecuteReader())
                                {
                                    if (!dr0.HasRows)
                                    {
                                        return false;
                                    }
                                    else
                                    {
                                        package.Workbook.Properties.Title = "BudgetReport";
                                        package.Workbook.Properties.Author = Tools.DefaultReportAuthor();
                                        package.Workbook.Properties.Comments = Tools.DefaultReportComments();
                                        package.Workbook.Properties.Company = Tools.DefaultReportCompany();
                                        package.Workbook.Properties.SetCustomPropertyValue("Checked by", _userID);
                                        package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", Tools.DefaultReportAssemblyName());

                                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Budget Report");


                                        //ATUR DATA GROUPINGNYA DULU
                                        List<BudgetRpt> rList = new List<BudgetRpt>();
                                        while (dr0.Read())
                                        {
                                            BudgetRpt rSingle = new BudgetRpt();
                                            rSingle.BranchName = Convert.ToString(dr0["BranchName"]);
                                            rSingle.SalesName = Convert.ToString(dr0["SalesName"]);
                                            rSingle.Category = Convert.ToString(dr0["Category"]);
                                            rSingle.InstrumentName = Convert.ToString(dr0["InstrumentName"]);
                                            rSingle.BeginningAUMDes = Convert.ToDecimal(dr0["YTDDes"]);
                                            rSingle.January1 = Convert.ToDecimal(dr0["January"]);
                                            rSingle.February1 = Convert.ToDecimal(dr0["February"]);
                                            rSingle.March1 = Convert.ToDecimal(dr0["March"]);
                                            rSingle.April1 = Convert.ToDecimal(dr0["April"]);
                                            rSingle.May1 = Convert.ToDecimal(dr0["May"]);
                                            rSingle.June1 = Convert.ToDecimal(dr0["June"]);
                                            rSingle.July1 = Convert.ToDecimal(dr0["July"]);
                                            rSingle.August1 = Convert.ToDecimal(dr0["August"]);
                                            rSingle.September1 = Convert.ToDecimal(dr0["September"]);
                                            rSingle.October1 = Convert.ToDecimal(dr0["October"]);
                                            rSingle.November1 = Convert.ToDecimal(dr0["November"]);
                                            rSingle.December1 = Convert.ToDecimal(dr0["December"]);
                                            rList.Add(rSingle);

                                        }

                                        var QueryByClientID =
                                         from r in rList
                                         orderby r.BranchName, r.SalesName
                                         group r by new { r.BranchName, r.SalesName } into rGroup
                                         select rGroup;

                                        int incRowExcel = 1;

                                        worksheet.Cells[incRowExcel, 1].Value = "PT MNC ASSET MANAGEMENT";
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Size = 14;
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "LAPORAN AUM CABANG DAN SALES";
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Size = 14;
                                        incRowExcel++;
                                        incRowExcel++;
                                        incRowExcel++;

                                        foreach (var rsHeader in QueryByClientID)
                                        {
                                            int _start = incRowExcel;
                                            int _end = incRowExcel + 4;

                                            worksheet.Cells["A" + _start + ":C" + _end].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + _start + ":C" + _end].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + _start + ":C" + _end].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + _start + ":C" + _end].Style.Border.Right.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells[incRowExcel, 1].Value = "Branch Name : ";
                                            worksheet.Cells[incRowExcel, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                            worksheet.Cells[incRowExcel, 1].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                                            worksheet.Cells[incRowExcel, 3].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                            worksheet.Cells[incRowExcel, 3].Style.Fill.BackgroundColor.SetColor(Color.LightSalmon);
                                            worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells["A" + incRowExcel + ":B" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 3].Value = rsHeader.Key.BranchName;
                                            worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            incRowExcel++;

                                            worksheet.Cells[incRowExcel, 1].Value = "Agent Name :";
                                            worksheet.Cells[incRowExcel, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                            worksheet.Cells[incRowExcel, 1].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                                            worksheet.Cells[incRowExcel, 3].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                            worksheet.Cells[incRowExcel, 3].Style.Fill.BackgroundColor.SetColor(Color.LightSalmon);
                                            worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells["A" + incRowExcel + ":B" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 3].Value = rsHeader.Key.SalesName;
                                            worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            incRowExcel++;

                                            worksheet.Cells["A" + incRowExcel + ":Q" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + incRowExcel + ":Q" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + incRowExcel + ":Q" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + incRowExcel + ":Q" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Medium;


                                            worksheet.Cells[incRowExcel, 1].Value = "No";
                                            worksheet.Cells[incRowExcel, 2].Value = "Category";
                                            worksheet.Cells[incRowExcel, 3].Value = "Product";
                                            worksheet.Cells[incRowExcel, 4].Value = "OKT-DES " + _host.Get_PeriodByRevenue(_BudgetRpt.PeriodPK, 1);

                                            worksheet.Cells[incRowExcel, 5].Value = "JAN ";
                                            worksheet.Cells[incRowExcel, 6].Value = "FEB ";
                                            worksheet.Cells[incRowExcel, 7].Value = "MAR ";
                                            worksheet.Cells[incRowExcel, 8].Value = "APR ";
                                            worksheet.Cells[incRowExcel, 9].Value = "MAY ";
                                            worksheet.Cells[incRowExcel, 10].Value = "JUN ";
                                            worksheet.Cells[incRowExcel, 11].Value = "JUL ";
                                            worksheet.Cells[incRowExcel, 12].Value = "AUG ";
                                            worksheet.Cells[incRowExcel, 13].Value = "SEP ";
                                            worksheet.Cells[incRowExcel, 14].Value = "OCT ";
                                            worksheet.Cells[incRowExcel, 15].Value = "NOV ";
                                            worksheet.Cells[incRowExcel, 16].Value = "DEC ";
                                            worksheet.Cells[incRowExcel, 17].Value = "TOTAL";

                                            worksheet.Cells["A" + incRowExcel + ":Q" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells["A" + incRowExcel + ":Q" + incRowExcel].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                            worksheet.Cells["A" + incRowExcel + ":Q" + incRowExcel].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                                            worksheet.Cells["A" + incRowExcel + ":Q" + incRowExcel].Style.Font.Bold = true;
                                            incRowExcel++;
                                            int _startRowDetail = incRowExcel;
                                            int _endRowDetail = 0;
                                            int _no = 1;
                                            foreach (var rsDetail in rsHeader)
                                            {


                                                //ThickBox Border

                                                worksheet.Cells[incRowExcel, 1].Value = _no;
                                                worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 2].Value = rsDetail.Category;
                                                worksheet.Cells[incRowExcel, 2].Style.WrapText = true;
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.InstrumentName;
                                                worksheet.Cells[incRowExcel, 3].Style.WrapText = true;
                                                worksheet.Cells[incRowExcel, 4].Value = rsDetail.BeginningAUMDes;
                                                worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##0";

                                                worksheet.Cells[incRowExcel, 5].Value = rsDetail.January1;
                                                worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 6].Value = rsDetail.February1;
                                                worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 7].Value = rsDetail.March1;
                                                worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 8].Value = rsDetail.April1;
                                                worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 9].Value = rsDetail.May1;
                                                worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 10].Value = rsDetail.June1;
                                                worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 11].Value = rsDetail.July1;
                                                worksheet.Cells[incRowExcel, 11].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 12].Value = rsDetail.August1;
                                                worksheet.Cells[incRowExcel, 12].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 13].Value = rsDetail.September1;
                                                worksheet.Cells[incRowExcel, 13].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 14].Value = rsDetail.October1;
                                                worksheet.Cells[incRowExcel, 14].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 15].Value = rsDetail.November1;
                                                worksheet.Cells[incRowExcel, 15].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 16].Value = rsDetail.December1;
                                                worksheet.Cells[incRowExcel, 16].Style.Numberformat.Format = "#,##0";

                                                worksheet.Cells[incRowExcel, 17].Formula = "SUM(E" + incRowExcel + ":P" + incRowExcel + ")";
                                                worksheet.Cells[incRowExcel, 17].Calculate();
                                                worksheet.Cells[incRowExcel, 17].Style.Numberformat.Format = "#,##0";

                                                worksheet.Cells["A" + incRowExcel + ":Q" + incRowExcel].Style.VerticalAlignment = ExcelVerticalAlignment.Top;

                                                _no++;
                                                _endRowDetail = incRowExcel;
                                                incRowExcel++;



                                            }

                                            worksheet.Cells["A" + _startRowDetail + ":Q" + _endRowDetail].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + _startRowDetail + ":Q" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + _startRowDetail + ":Q" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + _startRowDetail + ":Q" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Medium;


                                            worksheet.Cells[incRowExcel, 3].Value = "TOTAL :";
                                            worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                                            worksheet.Cells[incRowExcel, 4].Formula = "SUM(D" + _startRowDetail + ":D" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 4].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 4].Calculate();
                                            worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##0";

                                            worksheet.Cells[incRowExcel, 5].Formula = "SUM(E" + _startRowDetail + ":E" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 5].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 5].Calculate();
                                            worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0";

                                            worksheet.Cells[incRowExcel, 6].Formula = "SUM(F" + _startRowDetail + ":F" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 6].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 6].Calculate();
                                            worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0";

                                            worksheet.Cells[incRowExcel, 7].Formula = "SUM(G" + _startRowDetail + ":G" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 7].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 7].Calculate();
                                            worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0";

                                            worksheet.Cells[incRowExcel, 8].Formula = "SUM(H" + _startRowDetail + ":H" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 8].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 8].Calculate();
                                            worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0";

                                            worksheet.Cells[incRowExcel, 9].Formula = "SUM(I" + _startRowDetail + ":I" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 9].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 9].Calculate();
                                            worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0";

                                            worksheet.Cells[incRowExcel, 10].Formula = "SUM(J" + _startRowDetail + ":J" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 10].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 10].Calculate();
                                            worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0";

                                            worksheet.Cells[incRowExcel, 11].Formula = "SUM(K" + _startRowDetail + ":K" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 11].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 11].Calculate();
                                            worksheet.Cells[incRowExcel, 11].Style.Numberformat.Format = "#,##0";

                                            worksheet.Cells[incRowExcel, 12].Formula = "SUM(L" + _startRowDetail + ":L" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 12].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 12].Calculate();
                                            worksheet.Cells[incRowExcel, 12].Style.Numberformat.Format = "#,##0";

                                            worksheet.Cells[incRowExcel, 13].Formula = "SUM(M" + _startRowDetail + ":M" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 13].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 13].Calculate();
                                            worksheet.Cells[incRowExcel, 13].Style.Numberformat.Format = "#,##0";

                                            worksheet.Cells[incRowExcel, 14].Formula = "SUM(N" + _startRowDetail + ":N" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 14].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 14].Calculate();
                                            worksheet.Cells[incRowExcel, 14].Style.Numberformat.Format = "#,##0";

                                            worksheet.Cells[incRowExcel, 15].Formula = "SUM(O" + _startRowDetail + ":O" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 15].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 15].Calculate();
                                            worksheet.Cells[incRowExcel, 15].Style.Numberformat.Format = "#,##0";

                                            worksheet.Cells[incRowExcel, 16].Formula = "SUM(P" + _startRowDetail + ":P" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 16].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 16].Calculate();
                                            worksheet.Cells[incRowExcel, 16].Style.Numberformat.Format = "#,##0";

                                            worksheet.Cells[incRowExcel, 17].Formula = "SUM(Q" + _startRowDetail + ":Q" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 17].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 17].Calculate();
                                            worksheet.Cells[incRowExcel, 17].Style.Numberformat.Format = "#,##0";


                                            worksheet.Cells["C" + incRowExcel + ":Q" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["C" + incRowExcel + ":Q" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["C" + incRowExcel + ":Q" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Medium;

                                            incRowExcel = incRowExcel + 3;
                                        }



                                        worksheet.PrinterSettings.FitToPage = true;
                                        worksheet.PrinterSettings.FitToWidth = 1;
                                        worksheet.PrinterSettings.FitToHeight = 0;
                                        worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel - 1, 17];
                                        worksheet.Column(1).Width = 8;
                                        worksheet.Column(2).Width = 25;
                                        worksheet.Column(3).Width = 40;
                                        worksheet.Column(4).Width = 15;
                                        worksheet.Column(5).Width = 25;
                                        worksheet.Column(6).Width = 15;
                                        worksheet.Column(7).Width = 15;
                                        worksheet.Column(8).Width = 15;
                                        worksheet.Column(9).Width = 17;
                                        worksheet.Column(10).Width = 17;
                                        worksheet.Column(11).Width = 17;
                                        worksheet.Column(12).Width = 17;
                                        worksheet.Column(13).Width = 17;
                                        worksheet.Column(14).Width = 17;
                                        worksheet.Column(15).Width = 17;
                                        worksheet.Column(16).Width = 17;
                                        worksheet.Column(17).Width = 17;


                                        // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                        //worksheet.Cells["A3:O14"].AutoFitColumns();  // UNTUK AUTO FIT UKURAN TULISAN DENGAN KOLOM, KEBETULAN INI BUAT HEADER AJA DIPAKENYA

                                        worksheet.PrinterSettings.Orientation = eOrientation.Landscape; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                        worksheet.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN
                                        worksheet.HeaderFooter.OddHeader.RightAlignedText = "&14 LAPORAN AUM CABANG DAN SALES";

                                        // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                        worksheet.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
                                        worksheet.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
                                        worksheet.View.PageLayoutView = Tools.DefaultReportPageLayoutView();
                                        worksheet.View.ShowGridLines = Tools.DefaultReportShowGridLines();

                                        Image img = Image.FromFile(Tools.ReportImage);
                                        worksheet.HeaderFooter.OddHeader.InsertPicture(img, PictureAlignment.Left);
                                        //worksheet.HeaderFooter.OddHeader.CenteredText = Tools.DefaultReportHeaderCenterText();
                                        //worksheet.HeaderFooter.OddHeader.LeftAlignedText = Tools.DefaultReportHeaderLeftText();

                                        worksheet.HeaderFooter.OddFooter.RightAlignedText =
                                        string.Format("Page {0} of {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);

                                        ////worksheet.HeaderFooter.OddFooter.CenteredText = Tools.DefaultReportFooterCenterText();
                                        worksheet.HeaderFooter.OddFooter.LeftAlignedText = Tools.DefaultReportFooterLeftText();



                                    }
                                }
                            }

                            DbCon.Close();
                        }

                        package.Save();
                        return true;

                    }

                }
                catch (Exception err)
                {
                    return false;
                    throw err;
                }
            }
            #endregion

            #region 05 Laporan FundFlow Summary
            if (_BudgetRpt.ReportName.Equals("05 Laporan FundFlow Summary"))
            {
                try
                {
                    using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                    {
                        DbCon.Open();
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {

                            cmd.CommandText =
                            @"

                            DECLARE @PeriodPKTo INT

                            Declare @PeriodID nvarchar(50)
                            declare @RevenuePercent numeric(10,4)

                            select @PeriodID  = YEAR(dateadd(year, 1, ID)) From Period where PeriodPK = @PeriodPK and status in (1,2)
                            select @PeriodPKTo = PeriodPK from Period where ID = @PeriodID and status in (1,2)

                            declare @tableRevenue table
                            (
	                            PeriodPK int,
	                            ReportPeriodPK int,
	                            InstrumentPK int,
	                            AgentPK int,
	                            MGTFee numeric(10,4),
	                            January NUMERIC(22,4),
	                            February NUMERIC(22,4),
	                            March NUMERIC(22,4),
	                            April NUMERIC(22,4),
	                            May NUMERIC(22,4),
	                            June NUMERIC(22,4),
	                            July NUMERIC(22,4),
	                            August NUMERIC(22,4),
	                            September NUMERIC(22,4),
	                            October NUMERIC(22,4),
	                            November NUMERIC(22,4),
	                            December NUMERIC(22,4)
                            )


                            DECLARE @AUMFinalPerPeriod TABLE
                            (
	                            Date DATETIME,
	                            InstrumentPK INT,
	                            AUM NUMERIC(22,4)
                            )
                            INSERT INTO @AUMFinalPerPeriod
                                    ( Date,InstrumentPK, AUM )
                            SELECT MAX(A.date) Date,A.InstrumentPK,sum(A.AUM)  
                            FROM dbo.AumForBudgetBegBalance A
                            WHERE A.PeriodPK = @PeriodPK AND A.status = 2 --and AUM != 0
                            GROUP BY A.InstrumentPK

                            insert into @tableRevenue
                            select A.PeriodPK,A.ReportPeriodPK,A.InstrumentPK,AgentPK,MGTFee,
							January*case when B.ReksadanaTypePK in (5,6,7) then 1 else isnull(C.RevenuePercent/100,1) end,February*case when B.ReksadanaTypePK in (5,6,7) then 1 else isnull(C.RevenuePercent/100,1) end,
							March*case when B.ReksadanaTypePK in (5,6,7) then 1 else isnull(C.RevenuePercent/100,1) end,April*case when B.ReksadanaTypePK in (5,6,7) then 1 else isnull(C.RevenuePercent/100,1) end,
							May*case when B.ReksadanaTypePK in (5,6,7) then 1 else isnull(C.RevenuePercent/100,1) end,June*case when B.ReksadanaTypePK in (5,6,7) then 1 else isnull(C.RevenuePercent/100,1) end,
							July*case when B.ReksadanaTypePK in (5,6,7) then 1 else isnull(C.RevenuePercent/100,1) end,August*case when B.ReksadanaTypePK in (5,6,7) then 1 else isnull(C.RevenuePercent/100,1) end,
							September*case when B.ReksadanaTypePK in (5,6,7) then 1 else isnull(C.RevenuePercent/100,1) end,October*case when B.ReksadanaTypePK in (5,6,7) then 1 else isnull(C.RevenuePercent/100,1) end,
							November*case when B.ReksadanaTypePK in (5,6,7) then 1 else isnull(C.RevenuePercent/100,1) end,December*case when B.ReksadanaTypePK in (5,6,7) then 1 else isnull(C.RevenuePercent/100,1) end 
                            from Revenue A
							left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status in (1,2)
							left join BudgetVersioning C on A.PeriodPK = C.PeriodPK and C.ReportPeriodPK = C.ReportPeriodPK and C.Status = 2 and Date = (
	                            select max(date) from BudgetVersioning where status in (1,2) and ReportPeriodPK = @periodpk
                            )
							where A.status = 2 and A.ReportPeriodPK = @periodpk

							select @PeriodPKTo = max(periodpk) from Revenue where ReportPeriodPK = @PeriodPK and status = 2
							
							select 
							isnull(C.Name,'') InstrumentName,isnull(D.Description,'') Category,isnull(B.TotalYTD,0) YTDDes,
							sum(A.January) January,sum(A.February) February,sum(A.March) March,sum(A.April) April,sum(A.May) May,sum(A.June) June,sum(A.July) July,
							sum(A.August) August,sum(A.September) September,sum(A.October) October,sum(A.November) November,sum(A.December) December
							from @tableRevenue A
							LEFT JOIN
                            (
	                            SELECT A.InstrumentPK,sum(isnull(A.January,0) + isnull(A.February,0) + isnull(A.March,0) + isnull(A.April,0) + isnull(A.May,0) + isnull(A.June,0) + 
								isnull(A.July,0) + isnull(A.August,0) + isnull(A.September,0) + isnull(A.October,0) + isnull(A.November,0) + isnull(A.December,0)) TotalYTD FROM  @tableRevenue A
	                            WHERE A.ReportPeriodPK = @PeriodPK AND A.PeriodPK <> @PeriodPKTo 
	                            group by A.InstrumentPK
                            )B ON A.InstrumentPK = B.InstrumentPK
                            left join Instrument C on A.InstrumentPK = C.InstrumentPK and C.status in (1,2)
                            left join ReksadanaType D on C.ReksadanaTypePK = D.ReksadanaTypePK  and D.status in (1,2)
							where A.ReportPeriodPK = @PeriodPK and A.PeriodPK = @PeriodPKTo
							GROUP BY A.InstrumentPK,C.Name,D.Description,B.TotalYTD
                            ORDER BY A.InstrumentPK


";

                            cmd.CommandTimeout = 0;
                            cmd.Parameters.AddWithValue("@PeriodPK", _BudgetRpt.PeriodPK);


                            using (SqlDataReader dr0 = cmd.ExecuteReader())
                            {
                                if (!dr0.HasRows)
                                {
                                    return false;
                                }
                                else
                                {
                                    string filePath = Tools.ReportsPath + "LaporanFundFlowSummary" + "_" + _userID + ".xlsx";
                                    string pdfPath = Tools.ReportsPath + "LaporanFundFlowSummary" + "_" + _userID + ".pdf";
                                    FileInfo excelFile = new FileInfo(filePath);
                                    if (excelFile.Exists)
                                    {
                                        excelFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                        excelFile = new FileInfo(filePath);
                                    }


                                    // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                    using (ExcelPackage package = new ExcelPackage(excelFile))
                                    {
                                        package.Workbook.Properties.Title = "BudgetReport";
                                        package.Workbook.Properties.Author = Tools.DefaultReportAuthor();
                                        package.Workbook.Properties.Comments = Tools.DefaultReportComments();
                                        package.Workbook.Properties.Company = Tools.DefaultReportCompany();
                                        package.Workbook.Properties.SetCustomPropertyValue("Checked by", _userID);
                                        package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", Tools.DefaultReportAssemblyName());

                                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Budget Report");


                                        //ATUR DATA GROUPINGNYA DULU
                                        List<BudgetRpt> rList = new List<BudgetRpt>();
                                        while (dr0.Read())
                                        {
                                            BudgetRpt rSingle = new BudgetRpt();
                                            rSingle.NamaProduk = Convert.ToString(dr0["InstrumentName"]);
                                            rSingle.ProdukKategori = Convert.ToString(dr0["Category"]);
                                            rSingle.YTDDes = Convert.ToDecimal(dr0["YTDDes"]);
                                            rSingle.January1 = Convert.ToDecimal(dr0["January"]);
                                            rSingle.February1 = Convert.ToDecimal(dr0["February"]);
                                            rSingle.March1 = Convert.ToDecimal(dr0["March"]);
                                            rSingle.April1 = Convert.ToDecimal(dr0["April"]);
                                            rSingle.May1 = Convert.ToDecimal(dr0["May"]);
                                            rSingle.June1 = Convert.ToDecimal(dr0["June"]);
                                            rSingle.July1 = Convert.ToDecimal(dr0["July"]);
                                            rSingle.August1 = Convert.ToDecimal(dr0["August"]);
                                            rSingle.September1 = Convert.ToDecimal(dr0["September"]);
                                            rSingle.October1 = Convert.ToDecimal(dr0["October"]);
                                            rSingle.November1 = Convert.ToDecimal(dr0["November"]);
                                            rSingle.December1 = Convert.ToDecimal(dr0["December"]);
                                            rList.Add(rSingle);

                                        }

                                        var QueryByClientID =
                                         from r in rList
                                         orderby r.ProdukKategori
                                         group r by new { r.ProdukKategori } into rGroup
                                         select rGroup;

                                        int incRowExcel = 1;
                                        worksheet.Cells[incRowExcel, 1].Value = "PT MNC ASSET MANAGEMENT";
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Size = 14;
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "LAPORAN FUND FLOW SUMMARY";
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Size = 14;
                                        incRowExcel++;
                                        incRowExcel++;
                                        incRowExcel++;

                                        worksheet.Cells["A" + incRowExcel + ":Q" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["A" + incRowExcel + ":Q" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["A" + incRowExcel + ":Q" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["A" + incRowExcel + ":Q" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Medium;


                                        worksheet.Cells[incRowExcel, 1].Value = "No";
                                        worksheet.Cells[incRowExcel, 2].Value = "Nama Produk";
                                        worksheet.Cells[incRowExcel, 3].Value = "Produk Kategori";
                                        worksheet.Cells[incRowExcel, 4].Value = "YTD Des " + _host.Get_PeriodByRevenue(_BudgetRpt.PeriodPK, 1);

                                        worksheet.Cells[incRowExcel, 5].Value = "JAN ";
                                        worksheet.Cells[incRowExcel, 6].Value = "FEB ";
                                        worksheet.Cells[incRowExcel, 7].Value = "MAR ";
                                        worksheet.Cells[incRowExcel, 8].Value = "APR ";
                                        worksheet.Cells[incRowExcel, 9].Value = "MAY ";
                                        worksheet.Cells[incRowExcel, 10].Value = "JUN ";
                                        worksheet.Cells[incRowExcel, 11].Value = "JUL ";
                                        worksheet.Cells[incRowExcel, 12].Value = "AUG ";
                                        worksheet.Cells[incRowExcel, 13].Value = "SEP ";
                                        worksheet.Cells[incRowExcel, 14].Value = "OCT ";
                                        worksheet.Cells[incRowExcel, 15].Value = "NOV ";
                                        worksheet.Cells[incRowExcel, 16].Value = "DEC ";
                                        worksheet.Cells[incRowExcel, 17].Value = "TOTAL";

                                        worksheet.Cells["A" + incRowExcel + ":Q" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        worksheet.Cells["A" + incRowExcel + ":Q" + incRowExcel].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                        worksheet.Cells["A" + incRowExcel + ":Q" + incRowExcel].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                                        worksheet.Cells["A" + incRowExcel + ":Q" + incRowExcel].Style.Font.Bold = true;
                                        incRowExcel++;
                                        int _startRowDetail = incRowExcel;
                                        int _endRowDetail = 0;
                                        int _no = 1;

                                        foreach (var rsHeader in QueryByClientID)
                                        {
                                            _startRowDetail = incRowExcel;
                                            foreach (var rsDetail in rsHeader)
                                            {


                                                //ThickBox Border

                                                worksheet.Cells["C" + incRowExcel + ":Q" + incRowExcel].Style.Fill.PatternType = ExcelFillStyle.None;

                                                worksheet.Cells[incRowExcel, 1].Value = _no;
                                                worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 2].Value = rsDetail.NamaProduk;
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.ProdukKategori;
                                                worksheet.Cells[incRowExcel, 4].Value = rsDetail.YTDDes;
                                                worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##0";

                                                worksheet.Cells[incRowExcel, 5].Value = rsDetail.January1;
                                                worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 6].Value = rsDetail.February1;
                                                worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 7].Value = rsDetail.March1;
                                                worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 8].Value = rsDetail.April1;
                                                worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 9].Value = rsDetail.May1;
                                                worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 10].Value = rsDetail.June1;
                                                worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 11].Value = rsDetail.July1;
                                                worksheet.Cells[incRowExcel, 11].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 12].Value = rsDetail.August1;
                                                worksheet.Cells[incRowExcel, 12].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 13].Value = rsDetail.September1;
                                                worksheet.Cells[incRowExcel, 13].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 14].Value = rsDetail.October1;
                                                worksheet.Cells[incRowExcel, 14].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 15].Value = rsDetail.November1;
                                                worksheet.Cells[incRowExcel, 15].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 16].Value = rsDetail.December1;
                                                worksheet.Cells[incRowExcel, 16].Style.Numberformat.Format = "#,##0";


                                                worksheet.Cells[incRowExcel, 17].Formula = "SUM(E" + incRowExcel + ":P" + incRowExcel + ")";
                                                worksheet.Cells[incRowExcel, 17].Calculate();
                                                worksheet.Cells[incRowExcel, 17].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells["A" + incRowExcel + ":Q" + incRowExcel].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                                                _no++;
                                                _endRowDetail = incRowExcel;
                                                incRowExcel++;



                                            }

                                            worksheet.Cells["A" + _startRowDetail + ":Q" + _endRowDetail].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + _startRowDetail + ":Q" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + _startRowDetail + ":Q" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + _startRowDetail + ":Q" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells["C" + incRowExcel + ":Q" + incRowExcel].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                            worksheet.Cells["C" + incRowExcel + ":Q" + incRowExcel].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightBlue);


                                            worksheet.Cells[incRowExcel, 3].Value = "TOTAL :";
                                            worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                                            worksheet.Cells[incRowExcel, 4].Formula = "SUM(D" + _startRowDetail + ":D" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 4].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 4].Calculate();
                                            worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##0";

                                            worksheet.Cells[incRowExcel, 5].Formula = "SUM(E" + _startRowDetail + ":E" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 5].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 5].Calculate();
                                            worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0";

                                            worksheet.Cells[incRowExcel, 6].Formula = "SUM(F" + _startRowDetail + ":F" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 6].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 6].Calculate();
                                            worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0";

                                            worksheet.Cells[incRowExcel, 7].Formula = "SUM(G" + _startRowDetail + ":G" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 7].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 7].Calculate();
                                            worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0";

                                            worksheet.Cells[incRowExcel, 8].Formula = "SUM(H" + _startRowDetail + ":H" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 8].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 8].Calculate();
                                            worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0";

                                            worksheet.Cells[incRowExcel, 9].Formula = "SUM(I" + _startRowDetail + ":I" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 9].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 9].Calculate();
                                            worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0";

                                            worksheet.Cells[incRowExcel, 10].Formula = "SUM(J" + _startRowDetail + ":J" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 10].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 10].Calculate();
                                            worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0";

                                            worksheet.Cells[incRowExcel, 11].Formula = "SUM(K" + _startRowDetail + ":K" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 11].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 11].Calculate();
                                            worksheet.Cells[incRowExcel, 11].Style.Numberformat.Format = "#,##0";

                                            worksheet.Cells[incRowExcel, 12].Formula = "SUM(L" + _startRowDetail + ":L" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 12].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 12].Calculate();
                                            worksheet.Cells[incRowExcel, 12].Style.Numberformat.Format = "#,##0";

                                            worksheet.Cells[incRowExcel, 13].Formula = "SUM(M" + _startRowDetail + ":M" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 13].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 13].Calculate();
                                            worksheet.Cells[incRowExcel, 13].Style.Numberformat.Format = "#,##0";

                                            worksheet.Cells[incRowExcel, 14].Formula = "SUM(N" + _startRowDetail + ":N" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 14].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 14].Calculate();
                                            worksheet.Cells[incRowExcel, 14].Style.Numberformat.Format = "#,##0";

                                            worksheet.Cells[incRowExcel, 15].Formula = "SUM(O" + _startRowDetail + ":O" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 15].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 15].Calculate();
                                            worksheet.Cells[incRowExcel, 15].Style.Numberformat.Format = "#,##0";

                                            worksheet.Cells[incRowExcel, 16].Formula = "SUM(P" + _startRowDetail + ":P" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 16].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 16].Calculate();
                                            worksheet.Cells[incRowExcel, 16].Style.Numberformat.Format = "#,##0";

                                            worksheet.Cells[incRowExcel, 17].Formula = "SUM(Q" + _startRowDetail + ":Q" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 17].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 17].Calculate();
                                            worksheet.Cells[incRowExcel, 17].Style.Numberformat.Format = "#,##0";

                                            worksheet.Cells["C" + incRowExcel + ":Q" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["C" + incRowExcel + ":Q" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["C" + incRowExcel + ":Q" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Medium;

                                            incRowExcel++;
                                        }



                                        worksheet.PrinterSettings.FitToPage = true;
                                        worksheet.PrinterSettings.FitToWidth = 1;
                                        worksheet.PrinterSettings.FitToHeight = 0;
                                        worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel - 1, 17];
                                        worksheet.Column(1).Width = 8;
                                        worksheet.Column(2).AutoFit();
                                        worksheet.Column(3).AutoFit();
                                        worksheet.Column(4).Width = 17;
                                        worksheet.Column(5).Width = 17;
                                        worksheet.Column(6).Width = 17;
                                        worksheet.Column(7).Width = 17;
                                        worksheet.Column(8).Width = 17;
                                        worksheet.Column(9).Width = 17;
                                        worksheet.Column(10).Width = 17;
                                        worksheet.Column(11).Width = 17;
                                        worksheet.Column(12).Width = 17;
                                        worksheet.Column(13).Width = 17;
                                        worksheet.Column(14).Width = 17;
                                        worksheet.Column(15).Width = 17;
                                        worksheet.Column(16).Width = 17;
                                        worksheet.Column(17).Width = 17;



                                        // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                        //worksheet.Cells["A3:O14"].AutoFitColumns();  // UNTUK AUTO FIT UKURAN TULISAN DENGAN KOLOM, KEBETULAN INI BUAT HEADER AJA DIPAKENYA

                                        worksheet.PrinterSettings.Orientation = eOrientation.Landscape; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                        worksheet.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN
                                        worksheet.HeaderFooter.OddHeader.RightAlignedText = "&14 LAPORAN FUND FLOW SUMMARY";

                                        // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                        worksheet.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
                                        worksheet.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
                                        worksheet.View.PageLayoutView = Tools.DefaultReportPageLayoutView();
                                        worksheet.View.ShowGridLines = Tools.DefaultReportShowGridLines();

                                        Image img = Image.FromFile(Tools.ReportImage);
                                        worksheet.HeaderFooter.OddHeader.InsertPicture(img, PictureAlignment.Left);
                                        //worksheet.HeaderFooter.OddHeader.CenteredText = Tools.DefaultReportHeaderCenterText();
                                        //worksheet.HeaderFooter.OddHeader.LeftAlignedText = Tools.DefaultReportHeaderLeftText();

                                        worksheet.HeaderFooter.OddFooter.RightAlignedText =
                                        string.Format("Page {0} of {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);

                                        ////worksheet.HeaderFooter.OddFooter.CenteredText = Tools.DefaultReportFooterCenterText();
                                        worksheet.HeaderFooter.OddFooter.LeftAlignedText = Tools.DefaultReportFooterLeftText();



                                        package.Save();
                                        return true;
                                    }
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
            #endregion

            #region 06 Laporan AUM Cabang dan Sales
            if (_BudgetRpt.ReportName.Equals("06 Laporan AUM Cabang dan Sales"))
            {
                try
                {
                    string filePath = Tools.ReportsPath + "LaporanAUMCabangdanSales" + "_" + _userID + ".xlsx";
                    string pdfPath = Tools.ReportsPath + "LaporanAUMCabangdanSales" + "_" + _userID + ".pdf";
                    FileInfo excelFile = new FileInfo(filePath);
                    if (excelFile.Exists)
                    {
                        excelFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                        excelFile = new FileInfo(filePath);
                    }


                    // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                    using (ExcelPackage package = new ExcelPackage(excelFile))
                    {
                        using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                        {
                            DbCon.Open();
                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {

                                cmd.CommandText =
                                @"
                                DECLARE @PeriodPKFrom INT
                                Declare @PeriodID nvarchar(50)

                                select @PeriodID  = YEAR(dateadd(year, -1, ID)) From Period where PeriodPK = @PeriodPK and status in (1,2)
                                select @PeriodPKFrom = PeriodPK from Period where ID = @PeriodID and status in (1,2)


                                DECLARE @Result TABLE
                                (
	                                AgentPK INT,
	                                InstrumentPK INT,
	                                MGTFee NUMERIC(18,8),
	                                BegLastYear NUMERIC(22,4),
	                                YTDLastYear NUMERIC(22,4)
                                )

                                declare @tableRevenue table
                                (
	                                PeriodPK int,
	                                ReportPeriodPK int,
	                                InstrumentPK int,
	                                AgentPK int,
	                                MGTFee numeric(10,4),
	                                January NUMERIC(22,4),
	                                February NUMERIC(22,4),
	                                March NUMERIC(22,4),
	                                April NUMERIC(22,4),
	                                May NUMERIC(22,4),
	                                June NUMERIC(22,4),
	                                July NUMERIC(22,4),
	                                August NUMERIC(22,4),
	                                September NUMERIC(22,4),
	                                October NUMERIC(22,4),
	                                November NUMERIC(22,4),
	                                December NUMERIC(22,4)
                                )


                                DECLARE @AUMFinalPerPeriod TABLE
                                (
	                                Date DATETIME,
	                                InstrumentPK INT,
	                                AgentPK int,
	                                AUM NUMERIC(22,4)
                                )
                                INSERT INTO @AUMFinalPerPeriod
                                        ( Date,InstrumentPK, AgentPK, AUM )
                                SELECT MAX(A.date) Date,A.InstrumentPK,A.AgentPK,A.AUM  
                                FROM AumForBudgetBegBalance A
                                WHERE A.ReportPeriodPK = @PeriodPK AND A.status = 2 and periodpk = @PeriodPKFrom
                                GROUP BY A.InstrumentPK,A.AgentPK,A.Aum

                                insert into @tableRevenue
                                select A.PeriodPK,A.ReportPeriodPK,A.InstrumentPK,AgentPK,MGTFee,
							    January*case when B.ReksadanaTypePK in (5,6,7) then 1 else isnull(C.RevenuePercent/100,1) end,February*case when B.ReksadanaTypePK in (5,6,7) then 1 else isnull(C.RevenuePercent/100,1) end,
							    March*case when B.ReksadanaTypePK in (5,6,7) then 1 else isnull(C.RevenuePercent/100,1) end,April*case when B.ReksadanaTypePK in (5,6,7) then 1 else isnull(C.RevenuePercent/100,1) end,
							    May*case when B.ReksadanaTypePK in (5,6,7) then 1 else isnull(C.RevenuePercent/100,1) end,June*case when B.ReksadanaTypePK in (5,6,7) then 1 else isnull(C.RevenuePercent/100,1) end,
							    July*case when B.ReksadanaTypePK in (5,6,7) then 1 else isnull(C.RevenuePercent/100,1) end,August*case when B.ReksadanaTypePK in (5,6,7) then 1 else isnull(C.RevenuePercent/100,1) end,
							    September*case when B.ReksadanaTypePK in (5,6,7) then 1 else isnull(C.RevenuePercent/100,1) end,October*case when B.ReksadanaTypePK in (5,6,7) then 1 else isnull(C.RevenuePercent/100,1) end,
							    November*case when B.ReksadanaTypePK in (5,6,7) then 1 else isnull(C.RevenuePercent/100,1) end,December*case when B.ReksadanaTypePK in (5,6,7) then 1 else isnull(C.RevenuePercent/100,1) end 
                                from Revenue A
							    left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status in (1,2)
								left join BudgetVersioning C on A.PeriodPK = C.PeriodPK and C.ReportPeriodPK = C.ReportPeriodPK and C.Status = 2 and Date = (
	                                select max(date) from BudgetVersioning where status in (1,2) and ReportPeriodPK = @periodpk
                                )
							    where A.status = 2 and A.ReportPeriodPK = @periodpk


                                INSERT INTO @Result
                                        ( AgentPK ,
                                          InstrumentPK ,
                                          MGTFee ,
                                          BegLastYear ,
                                          YTDLastYear
                                        )
                                SELECT A.AgentPK,A.InstrumentPK,ISNULL(B.MGTFee,0) MGTFee
                                ,ISNULL(A.AUM,0) BegLastYear
                                ,CASE WHEN MONTH(A.Date) = 8 THEN ISNULL(A.AUM,0) + ISNULL(B.September,0)  + ISNULL(B.October,0) + ISNULL(B.November,0) + ISNULL(B.December,0)
	                                WHEN MONTH(A.Date) = 9 THEN ISNULL(A.AUM,0)  + ISNULL(B.October,0) + ISNULL(B.November,0) + ISNULL(B.December,0)
		                                WHEN MONTH(A.Date) = 10 THEN ISNULL(A.AUM,0)  + ISNULL(B.November,0) + ISNULL(B.December,0)
			                                WHEN MONTH(A.Date) = 11 THEN ISNULL(A.AUM,0)  + ISNULL(B.November,0) + ISNULL(B.December,0) END YTDLastYear
                                FROM @AUMFinalPerPeriod A
                                LEFT JOIN
                                (
	                                SELECT * FROM @tableRevenue A
	                                WHERE A.ReportPeriodPK = @PeriodPK AND A.PeriodPK <> @PeriodPK --AND A.Status = 2 
                                )B ON A.InstrumentPK = B.InstrumentPK AND A.AgentPK = B.AgentPK


                                SELECT C.Name SalesName, D.Name InstrumentName,isnull(F.Description,'') Category,E.Name BranchName,A.MGTFee,A.BeglastYear AUMSeptember,ISNULL(A.YTDLastYear,0)  AUMDesember
                                ,ISNULL(A.YTDLastYear,0) + ISNULL(B.January,0) January
                                ,ISNULL(A.YTDLastYear,0) + ISNULL(B.January,0) + ISNULL(B.February,0) February
                                ,ISNULL(A.YTDLastYear,0) + ISNULL(B.January,0) + ISNULL(B.February,0) + ISNULL(B.March,0) March
                                ,ISNULL(A.YTDLastYear,0) + ISNULL(B.January,0) + ISNULL(B.February,0) + ISNULL(B.March,0) + ISNULL(B.April,0) April
                                ,ISNULL(A.YTDLastYear,0) + ISNULL(B.January,0) + ISNULL(B.February,0) + ISNULL(B.March,0) + ISNULL(B.April,0) + ISNULL(B.May,0) May
                                ,ISNULL(A.YTDLastYear,0) + ISNULL(B.January,0) + ISNULL(B.February,0) + ISNULL(B.March,0) + ISNULL(B.April,0) + ISNULL(B.May,0) + ISNULL(B.June,0) June
                                ,ISNULL(A.YTDLastYear,0) + ISNULL(B.January,0) + ISNULL(B.February,0) + ISNULL(B.March,0) + ISNULL(B.April,0) + ISNULL(B.May,0) + ISNULL(B.June,0) + ISNULL(B.July,0) July
                                ,ISNULL(A.YTDLastYear,0) + ISNULL(B.January,0) + ISNULL(B.February,0) + ISNULL(B.March,0) + ISNULL(B.April,0) + ISNULL(B.May,0) + ISNULL(B.June,0) + ISNULL(B.July,0) + ISNULL(B.August,0) August
                                ,ISNULL(A.YTDLastYear,0) + ISNULL(B.January,0) + ISNULL(B.February,0) + ISNULL(B.March,0) + ISNULL(B.April,0) + ISNULL(B.May,0) + ISNULL(B.June,0) + ISNULL(B.July,0) + ISNULL(B.August,0) + ISNULL(B.September,0) September
                                ,ISNULL(A.YTDLastYear,0) + ISNULL(B.January,0) + ISNULL(B.February,0) + ISNULL(B.March,0) + ISNULL(B.April,0) + ISNULL(B.May,0) + ISNULL(B.June,0) + ISNULL(B.July,0) + ISNULL(B.August,0) + ISNULL(B.September,0) + ISNULL(B.October,0) October
                                ,ISNULL(A.YTDLastYear,0) + ISNULL(B.January,0) + ISNULL(B.February,0) + ISNULL(B.March,0) + ISNULL(B.April,0) + ISNULL(B.May,0) + ISNULL(B.June,0) + ISNULL(B.July,0) + ISNULL(B.August,0) + ISNULL(B.September,0) + ISNULL(B.October,0) + ISNULL(B.November,0) November
                                ,ISNULL(A.YTDLastYear,0) + ISNULL(B.January,0) + ISNULL(B.February,0) + ISNULL(B.March,0) + ISNULL(B.April,0) + ISNULL(B.May,0) + ISNULL(B.June,0) + ISNULL(B.July,0) + ISNULL(B.August,0) + ISNULL(B.September,0) + ISNULL(B.October,0) + ISNULL(B.November,0) + ISNULL(B.December,0) December
                                FROM @Result A
                                LEFT JOIN
                                (
	                                SELECT * FROM  @tableRevenue A
	                                WHERE A.ReportPeriodPK = @PeriodPK AND A.PeriodPK = @PeriodPK 
                                )B ON A.AgentPK = B.AgentPK AND A.InstrumentPK = B.InstrumentPK
                                left join Agent C on A.AgentPK = C.AgentPK and C.Status = 2
                                left join Instrument D on A.InstrumentPK = D.InstrumentPK and D.Status = 2
                                left join Department E on C.DepartmentPK = E.DepartmentPK and E.Status = 2
                                left join ReksadanaType F on D.ReksadanaTypePK = F.ReksadanaTypePK  and F.status in (1,2)
                                ORDER BY A.AgentPK,A.InstrumentPK

    ";

                                cmd.CommandTimeout = 0;
                                cmd.Parameters.AddWithValue("@PeriodPK", _BudgetRpt.PeriodPK);


                                using (SqlDataReader dr0 = cmd.ExecuteReader())
                                {
                                    if (!dr0.HasRows)
                                    {
                                        return false;
                                    }
                                    else
                                    {
                                        package.Workbook.Properties.Title = "BudgetReport";
                                        package.Workbook.Properties.Author = Tools.DefaultReportAuthor();
                                        package.Workbook.Properties.Comments = Tools.DefaultReportComments();
                                        package.Workbook.Properties.Company = Tools.DefaultReportCompany();
                                        package.Workbook.Properties.SetCustomPropertyValue("Checked by", _userID);
                                        package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", Tools.DefaultReportAssemblyName());

                                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Budget Report");


                                        //ATUR DATA GROUPINGNYA DULU
                                        List<BudgetRpt> rList = new List<BudgetRpt>();
                                        while (dr0.Read())
                                        {
                                            BudgetRpt rSingle = new BudgetRpt();
                                            rSingle.BranchName = Convert.ToString(dr0["BranchName"]);
                                            rSingle.SalesName = Convert.ToString(dr0["SalesName"]);
                                            rSingle.Category = Convert.ToString(dr0["Category"]);
                                            rSingle.InstrumentName = Convert.ToString(dr0["InstrumentName"]);
                                            rSingle.AUMSeptember = Convert.ToDecimal(dr0["AUMSeptember"]);
                                            rSingle.BeginningAUMDes = Convert.ToDecimal(dr0["AUMDesember"]);
                                            rSingle.January1 = Convert.ToDecimal(dr0["January"]);
                                            rSingle.February1 = Convert.ToDecimal(dr0["February"]);
                                            rSingle.March1 = Convert.ToDecimal(dr0["March"]);
                                            rSingle.April1 = Convert.ToDecimal(dr0["April"]);
                                            rSingle.May1 = Convert.ToDecimal(dr0["May"]);
                                            rSingle.June1 = Convert.ToDecimal(dr0["June"]);
                                            rSingle.July1 = Convert.ToDecimal(dr0["July"]);
                                            rSingle.August1 = Convert.ToDecimal(dr0["August"]);
                                            rSingle.September1 = Convert.ToDecimal(dr0["September"]);
                                            rSingle.October1 = Convert.ToDecimal(dr0["October"]);
                                            rSingle.November1 = Convert.ToDecimal(dr0["November"]);
                                            rSingle.December1 = Convert.ToDecimal(dr0["December"]);
                                            rList.Add(rSingle);

                                        }

                                        var QueryByClientID =
                                         from r in rList
                                         orderby r.BranchName, r.SalesName
                                         group r by new { r.BranchName, r.SalesName } into rGroup
                                         select rGroup;

                                        int incRowExcel = 1;

                                        worksheet.Cells[incRowExcel, 1].Value = "PT MNC ASSET MANAGEMENT";
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Size = 14;
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "LAPORAN AUM CABANG DAN SALES";
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Size = 14;
                                        incRowExcel++;
                                        incRowExcel++;
                                        incRowExcel++;

                                        foreach (var rsHeader in QueryByClientID)
                                        {
                                            int _start = incRowExcel;
                                            int _end = incRowExcel + 4;

                                            worksheet.Cells["A" + _start + ":C" + _end].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + _start + ":C" + _end].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + _start + ":C" + _end].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + _start + ":C" + _end].Style.Border.Right.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells[incRowExcel, 1].Value = "Branch Name : ";
                                            worksheet.Cells[incRowExcel, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                            worksheet.Cells[incRowExcel, 1].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                                            worksheet.Cells[incRowExcel, 3].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                            worksheet.Cells[incRowExcel, 3].Style.Fill.BackgroundColor.SetColor(Color.LightSalmon);
                                            worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells["A" + incRowExcel + ":B" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 3].Value = rsHeader.Key.BranchName;
                                            worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            incRowExcel++;

                                            worksheet.Cells[incRowExcel, 1].Value = "Agent Name :";
                                            worksheet.Cells[incRowExcel, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                            worksheet.Cells[incRowExcel, 1].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                                            worksheet.Cells[incRowExcel, 3].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                            worksheet.Cells[incRowExcel, 3].Style.Fill.BackgroundColor.SetColor(Color.LightSalmon);
                                            worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells["A" + incRowExcel + ":B" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 3].Value = rsHeader.Key.SalesName;
                                            worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            incRowExcel++;

                                            worksheet.Cells["A" + incRowExcel + ":R" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + incRowExcel + ":R" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + incRowExcel + ":R" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + incRowExcel + ":R" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Medium;


                                            worksheet.Cells[incRowExcel, 1].Value = "No";
                                            worksheet.Cells[incRowExcel, 2].Value = "Category";
                                            worksheet.Cells[incRowExcel, 3].Value = "Product";
                                            worksheet.Cells[incRowExcel, 4].Value = "SEP " + _host.Get_PeriodByRevenue(_BudgetRpt.PeriodPK, 1);
                                            worksheet.Cells[incRowExcel, 5].Value = "BEGINNING AUM-DES " + _host.Get_PeriodByRevenue(_BudgetRpt.PeriodPK, 1);

                                            worksheet.Cells[incRowExcel, 6].Value = "JAN ";
                                            worksheet.Cells[incRowExcel, 7].Value = "FEB ";
                                            worksheet.Cells[incRowExcel, 8].Value = "MAR ";
                                            worksheet.Cells[incRowExcel, 9].Value = "APR ";
                                            worksheet.Cells[incRowExcel, 10].Value = "MAY ";
                                            worksheet.Cells[incRowExcel, 11].Value = "JUN ";
                                            worksheet.Cells[incRowExcel, 12].Value = "JUL ";
                                            worksheet.Cells[incRowExcel, 13].Value = "AUG ";
                                            worksheet.Cells[incRowExcel, 14].Value = "SEP ";
                                            worksheet.Cells[incRowExcel, 15].Value = "OCT ";
                                            worksheet.Cells[incRowExcel, 16].Value = "NOV ";
                                            worksheet.Cells[incRowExcel, 17].Value = "DEC ";
                                            worksheet.Cells[incRowExcel, 18].Value = "TOTAL";

                                            worksheet.Cells["A" + incRowExcel + ":R" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells["A" + incRowExcel + ":R" + incRowExcel].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                            worksheet.Cells["A" + incRowExcel + ":R" + incRowExcel].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                                            worksheet.Cells["A" + incRowExcel + ":R" + incRowExcel].Style.Font.Bold = true;
                                            incRowExcel++;
                                            int _startRowDetail = incRowExcel;
                                            int _endRowDetail = 0;
                                            int _no = 1;
                                            foreach (var rsDetail in rsHeader)
                                            {


                                                //ThickBox Border

                                                worksheet.Cells[incRowExcel, 1].Value = _no;
                                                worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 2].Value = rsDetail.Category;
                                                worksheet.Cells[incRowExcel, 2].Style.WrapText = true;
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.InstrumentName;
                                                worksheet.Cells[incRowExcel, 3].Style.WrapText = true;
                                                worksheet.Cells[incRowExcel, 4].Value = rsDetail.AUMSeptember;
                                                worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 5].Value = rsDetail.BeginningAUMDes;
                                                worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0";

                                                worksheet.Cells[incRowExcel, 6].Value = rsDetail.January1;
                                                worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 7].Value = rsDetail.February1;
                                                worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 8].Value = rsDetail.March1;
                                                worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 9].Value = rsDetail.April1;
                                                worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 10].Value = rsDetail.May1;
                                                worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 11].Value = rsDetail.June1;
                                                worksheet.Cells[incRowExcel, 11].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 12].Value = rsDetail.July1;
                                                worksheet.Cells[incRowExcel, 12].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 13].Value = rsDetail.August1;
                                                worksheet.Cells[incRowExcel, 13].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 14].Value = rsDetail.September1;
                                                worksheet.Cells[incRowExcel, 14].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 15].Value = rsDetail.October1;
                                                worksheet.Cells[incRowExcel, 15].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 16].Value = rsDetail.November1;
                                                worksheet.Cells[incRowExcel, 16].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 17].Value = rsDetail.December1;
                                                worksheet.Cells[incRowExcel, 17].Style.Numberformat.Format = "#,##0";

                                                worksheet.Cells[incRowExcel, 18].Formula = "SUM(Q" + incRowExcel + ":Q" + incRowExcel + ")";
                                                worksheet.Cells[incRowExcel, 18].Calculate();
                                                worksheet.Cells[incRowExcel, 18].Style.Numberformat.Format = "#,##0";

                                                worksheet.Cells["A" + incRowExcel + ":R" + incRowExcel].Style.VerticalAlignment = ExcelVerticalAlignment.Top;

                                                _no++;
                                                _endRowDetail = incRowExcel;
                                                incRowExcel++;



                                            }

                                            worksheet.Cells["A" + _startRowDetail + ":R" + _endRowDetail].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + _startRowDetail + ":R" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + _startRowDetail + ":R" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + _startRowDetail + ":R" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Medium;


                                            worksheet.Cells[incRowExcel, 3].Value = "TOTAL :";
                                            worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                                            worksheet.Cells[incRowExcel, 4].Formula = "SUM(D" + _startRowDetail + ":D" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 4].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 4].Calculate();
                                            worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##0";

                                            worksheet.Cells[incRowExcel, 5].Formula = "SUM(E" + _startRowDetail + ":E" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 5].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 5].Calculate();
                                            worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0";

                                            worksheet.Cells[incRowExcel, 6].Formula = "SUM(F" + _startRowDetail + ":F" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 6].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 6].Calculate();
                                            worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0";

                                            worksheet.Cells[incRowExcel, 7].Formula = "SUM(G" + _startRowDetail + ":G" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 7].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 7].Calculate();
                                            worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0";

                                            worksheet.Cells[incRowExcel, 8].Formula = "SUM(H" + _startRowDetail + ":H" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 8].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 8].Calculate();
                                            worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0";

                                            worksheet.Cells[incRowExcel, 9].Formula = "SUM(I" + _startRowDetail + ":I" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 9].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 9].Calculate();
                                            worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0";

                                            worksheet.Cells[incRowExcel, 10].Formula = "SUM(J" + _startRowDetail + ":J" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 10].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 10].Calculate();
                                            worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0";

                                            worksheet.Cells[incRowExcel, 11].Formula = "SUM(K" + _startRowDetail + ":K" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 11].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 11].Calculate();
                                            worksheet.Cells[incRowExcel, 11].Style.Numberformat.Format = "#,##0";

                                            worksheet.Cells[incRowExcel, 12].Formula = "SUM(L" + _startRowDetail + ":L" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 12].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 12].Calculate();
                                            worksheet.Cells[incRowExcel, 12].Style.Numberformat.Format = "#,##0";

                                            worksheet.Cells[incRowExcel, 13].Formula = "SUM(M" + _startRowDetail + ":M" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 13].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 13].Calculate();
                                            worksheet.Cells[incRowExcel, 13].Style.Numberformat.Format = "#,##0";

                                            worksheet.Cells[incRowExcel, 14].Formula = "SUM(N" + _startRowDetail + ":N" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 14].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 14].Calculate();
                                            worksheet.Cells[incRowExcel, 14].Style.Numberformat.Format = "#,##0";

                                            worksheet.Cells[incRowExcel, 15].Formula = "SUM(O" + _startRowDetail + ":O" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 15].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 15].Calculate();
                                            worksheet.Cells[incRowExcel, 15].Style.Numberformat.Format = "#,##0";

                                            worksheet.Cells[incRowExcel, 16].Formula = "SUM(P" + _startRowDetail + ":P" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 16].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 16].Calculate();
                                            worksheet.Cells[incRowExcel, 16].Style.Numberformat.Format = "#,##0";

                                            worksheet.Cells[incRowExcel, 17].Formula = "SUM(Q" + _startRowDetail + ":Q" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 17].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 17].Calculate();
                                            worksheet.Cells[incRowExcel, 17].Style.Numberformat.Format = "#,##0";

                                            worksheet.Cells[incRowExcel, 18].Formula = "SUM(R" + _startRowDetail + ":R" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 18].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 18].Calculate();
                                            worksheet.Cells[incRowExcel, 18].Style.Numberformat.Format = "#,##0";


                                            worksheet.Cells["C" + incRowExcel + ":R" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["C" + incRowExcel + ":R" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["C" + incRowExcel + ":R" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Medium;

                                            incRowExcel = incRowExcel + 3;
                                        }



                                        worksheet.PrinterSettings.FitToPage = true;
                                        worksheet.PrinterSettings.FitToWidth = 1;
                                        worksheet.PrinterSettings.FitToHeight = 0;
                                        worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel - 1, 18];
                                        worksheet.Column(1).Width = 8;
                                        worksheet.Column(2).Width = 25;
                                        worksheet.Column(3).Width = 40;
                                        worksheet.Column(4).Width = 15;
                                        worksheet.Column(5).Width = 25;
                                        worksheet.Column(6).Width = 15;
                                        worksheet.Column(7).Width = 15;
                                        worksheet.Column(8).Width = 15;
                                        worksheet.Column(9).Width = 17;
                                        worksheet.Column(10).Width = 17;
                                        worksheet.Column(11).Width = 17;
                                        worksheet.Column(12).Width = 17;
                                        worksheet.Column(13).Width = 17;
                                        worksheet.Column(14).Width = 17;
                                        worksheet.Column(15).Width = 17;
                                        worksheet.Column(16).Width = 17;
                                        worksheet.Column(17).Width = 17;
                                        worksheet.Column(18).Width = 17;


                                        // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                        //worksheet.Cells["A3:O14"].AutoFitColumns();  // UNTUK AUTO FIT UKURAN TULISAN DENGAN KOLOM, KEBETULAN INI BUAT HEADER AJA DIPAKENYA

                                        worksheet.PrinterSettings.Orientation = eOrientation.Landscape; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                        worksheet.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN
                                        worksheet.HeaderFooter.OddHeader.RightAlignedText = "&14 LAPORAN AUM CABANG DAN SALES";

                                        // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                        worksheet.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
                                        worksheet.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
                                        worksheet.View.PageLayoutView = Tools.DefaultReportPageLayoutView();
                                        worksheet.View.ShowGridLines = Tools.DefaultReportShowGridLines();

                                        Image img = Image.FromFile(Tools.ReportImage);
                                        worksheet.HeaderFooter.OddHeader.InsertPicture(img, PictureAlignment.Left);
                                        //worksheet.HeaderFooter.OddHeader.CenteredText = Tools.DefaultReportHeaderCenterText();
                                        //worksheet.HeaderFooter.OddHeader.LeftAlignedText = Tools.DefaultReportHeaderLeftText();

                                        worksheet.HeaderFooter.OddFooter.RightAlignedText =
                                        string.Format("Page {0} of {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);

                                        ////worksheet.HeaderFooter.OddFooter.CenteredText = Tools.DefaultReportFooterCenterText();
                                        worksheet.HeaderFooter.OddFooter.LeftAlignedText = Tools.DefaultReportFooterLeftText();



                                    }
                                }
                            }

                            DbCon.Close();
                        }

                        //sheet 2
                        ExcelWorksheet worksheet1 = package.Workbook.Worksheets.Add("Summary");

                        using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                        {
                            DbCon.Open();
                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {

                                cmd.CommandText =
                                @"
                                DECLARE @PeriodPKFrom INT

                                Declare @PeriodID nvarchar(50)

                                select @PeriodID  = YEAR(dateadd(year, -1, ID)) From Period where PeriodPK = @PeriodPK and status in (1,2)
                                select @PeriodPKFrom = PeriodPK from Period where ID = @PeriodID and status in (1,2)

                                DECLARE @Result TABLE
                                (
	                                AgentPK INT,
	                                InstrumentPK INT,
	                                MGTFee NUMERIC(18,8),
	                                BegLastYear NUMERIC(22,4),
	                                YTDLastYear NUMERIC(22,4)
                                )

                                declare @tableRevenue table
                                (
	                                PeriodPK int,
	                                ReportPeriodPK int,
	                                InstrumentPK int,
	                                AgentPK int,
	                                MGTFee numeric(10,4),
	                                January NUMERIC(22,4),
	                                February NUMERIC(22,4),
	                                March NUMERIC(22,4),
	                                April NUMERIC(22,4),
	                                May NUMERIC(22,4),
	                                June NUMERIC(22,4),
	                                July NUMERIC(22,4),
	                                August NUMERIC(22,4),
	                                September NUMERIC(22,4),
	                                October NUMERIC(22,4),
	                                November NUMERIC(22,4),
	                                December NUMERIC(22,4)
                                )


                                DECLARE @AUMFinalPerPeriod TABLE
                                (
	                                Date DATETIME,
	                                InstrumentPK INT,
	                                AgentPK int,
	                                AUM NUMERIC(22,4)
                                )
                                INSERT INTO @AUMFinalPerPeriod
                                        ( Date,InstrumentPK, AgentPK, AUM )
                                SELECT MAX(A.date) Date,A.InstrumentPK,A.AgentPK,A.AUM  
                                FROM AumForBudgetBegBalance A
                                WHERE A.ReportPeriodPK = @PeriodPK AND A.status = 2 and periodpk = @PeriodPKFrom
                                GROUP BY A.InstrumentPK,A.AgentPK,A.Aum

                                insert into @tableRevenue
                                select A.PeriodPK,A.ReportPeriodPK,A.InstrumentPK,AgentPK,MGTFee,
							    January*case when B.ReksadanaTypePK in (5,6,7) then 1 else isnull(C.RevenuePercent/100,1) end,February*case when B.ReksadanaTypePK in (5,6,7) then 1 else isnull(C.RevenuePercent/100,1) end,
							    March*case when B.ReksadanaTypePK in (5,6,7) then 1 else isnull(C.RevenuePercent/100,1) end,April*case when B.ReksadanaTypePK in (5,6,7) then 1 else isnull(C.RevenuePercent/100,1) end,
							    May*case when B.ReksadanaTypePK in (5,6,7) then 1 else isnull(C.RevenuePercent/100,1) end,June*case when B.ReksadanaTypePK in (5,6,7) then 1 else isnull(C.RevenuePercent/100,1) end,
							    July*case when B.ReksadanaTypePK in (5,6,7) then 1 else isnull(C.RevenuePercent/100,1) end,August*case when B.ReksadanaTypePK in (5,6,7) then 1 else isnull(C.RevenuePercent/100,1) end,
							    September*case when B.ReksadanaTypePK in (5,6,7) then 1 else isnull(C.RevenuePercent/100,1) end,October*case when B.ReksadanaTypePK in (5,6,7) then 1 else isnull(C.RevenuePercent/100,1) end,
							    November*case when B.ReksadanaTypePK in (5,6,7) then 1 else isnull(C.RevenuePercent/100,1) end,December*case when B.ReksadanaTypePK in (5,6,7) then 1 else isnull(C.RevenuePercent/100,1) end 
                                from Revenue A
							    left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status in (1,2)
								left join BudgetVersioning C on A.PeriodPK = C.PeriodPK and C.ReportPeriodPK = C.ReportPeriodPK and C.Status = 2 and Date = (
	                                select max(date) from BudgetVersioning where status in (1,2) and ReportPeriodPK = @periodpk
                                )
							    where A.status = 2 and A.ReportPeriodPK = @periodpk


                                INSERT INTO @Result
                                        ( AgentPK ,
                                          InstrumentPK ,
                                          MGTFee ,
                                          BegLastYear ,
                                          YTDLastYear
                                        )
                                SELECT A.AgentPK,A.InstrumentPK,ISNULL(B.MGTFee,0) MGTFee
                                ,ISNULL(A.AUM,0) BegLastYear
                                ,CASE WHEN MONTH(A.Date) = 8 THEN ISNULL(A.AUM,0) + ISNULL(B.September,0)  + ISNULL(B.October,0) + ISNULL(B.November,0) + ISNULL(B.December,0)
	                                WHEN MONTH(A.Date) = 9 THEN ISNULL(A.AUM,0)  + ISNULL(B.October,0) + ISNULL(B.November,0) + ISNULL(B.December,0)
		                                WHEN MONTH(A.Date) = 10 THEN ISNULL(A.AUM,0)  + ISNULL(B.November,0) + ISNULL(B.December,0)
			                                WHEN MONTH(A.Date) = 11 THEN ISNULL(A.AUM,0)  + ISNULL(B.November,0) + ISNULL(B.December,0) END YTDLastYear
                                FROM @AUMFinalPerPeriod A
                                LEFT JOIN
                                (
	                                SELECT * FROM @tableRevenue A
	                                WHERE A.ReportPeriodPK = @PeriodPK AND A.PeriodPK <> @PeriodPK --AND A.Status = 2 
                                )B ON A.InstrumentPK = B.InstrumentPK AND A.AgentPK = B.AgentPK



                                SELECT isnull(E.Name,'') Category,sum(A.BeglastYear) AUMSeptember,sum(ISNULL(A.YTDLastYear,0))  AUMDesember
                                ,sum(ISNULL(A.YTDLastYear,0) + ISNULL(B.January,0)) January
                                ,sum(ISNULL(A.YTDLastYear,0) + ISNULL(B.January,0) + ISNULL(B.February,0)) February
                                ,sum(ISNULL(A.YTDLastYear,0) + ISNULL(B.January,0) + ISNULL(B.February,0) + ISNULL(B.March,0)) March
                                ,sum(ISNULL(A.YTDLastYear,0) + ISNULL(B.January,0) + ISNULL(B.February,0) + ISNULL(B.March,0) + ISNULL(B.April,0)) April
                                ,sum(ISNULL(A.YTDLastYear,0) + ISNULL(B.January,0) + ISNULL(B.February,0) + ISNULL(B.March,0) + ISNULL(B.April,0) + ISNULL(B.May,0)) May
                                ,sum(ISNULL(A.YTDLastYear,0) + ISNULL(B.January,0) + ISNULL(B.February,0) + ISNULL(B.March,0) + ISNULL(B.April,0) + ISNULL(B.May,0) + ISNULL(B.June,0)) June
                                ,sum(ISNULL(A.YTDLastYear,0) + ISNULL(B.January,0) + ISNULL(B.February,0) + ISNULL(B.March,0) + ISNULL(B.April,0) + ISNULL(B.May,0) + ISNULL(B.June,0) + ISNULL(B.July,0)) July
                                ,sum(ISNULL(A.YTDLastYear,0) + ISNULL(B.January,0) + ISNULL(B.February,0) + ISNULL(B.March,0) + ISNULL(B.April,0) + ISNULL(B.May,0) + ISNULL(B.June,0) + ISNULL(B.July,0) + ISNULL(B.August,0)) August
                                ,sum(ISNULL(A.YTDLastYear,0) + ISNULL(B.January,0) + ISNULL(B.February,0) + ISNULL(B.March,0) + ISNULL(B.April,0) + ISNULL(B.May,0) + ISNULL(B.June,0) + ISNULL(B.July,0) + ISNULL(B.August,0) + ISNULL(B.September,0)) September
                                ,sum(ISNULL(A.YTDLastYear,0) + ISNULL(B.January,0) + ISNULL(B.February,0) + ISNULL(B.March,0) + ISNULL(B.April,0) + ISNULL(B.May,0) + ISNULL(B.June,0) + ISNULL(B.July,0) + ISNULL(B.August,0) + ISNULL(B.September,0) + ISNULL(B.October,0)) October
                                ,sum(ISNULL(A.YTDLastYear,0) + ISNULL(B.January,0) + ISNULL(B.February,0) + ISNULL(B.March,0) + ISNULL(B.April,0) + ISNULL(B.May,0) + ISNULL(B.June,0) + ISNULL(B.July,0) + ISNULL(B.August,0) + ISNULL(B.September,0) + ISNULL(B.October,0) + ISNULL(B.November,0)) November
                                ,sum(ISNULL(A.YTDLastYear,0) + ISNULL(B.January,0) + ISNULL(B.February,0) + ISNULL(B.March,0) + ISNULL(B.April,0) + ISNULL(B.May,0) + ISNULL(B.June,0) + ISNULL(B.July,0) + ISNULL(B.August,0) + ISNULL(B.September,0) + ISNULL(B.October,0) + ISNULL(B.November,0) + ISNULL(B.December,0)) December
                                FROM @Result A
                                LEFT JOIN
                                (
	                                SELECT * FROM  @tableRevenue A
	                                WHERE A.ReportPeriodPK = @PeriodPK AND A.PeriodPK = @PeriodPK 
                                )B ON A.AgentPK = B.AgentPK AND A.InstrumentPK = B.InstrumentPK
                                left join Agent C on A.AgentPK = C.AgentPK and C.Status = 2
                                left join Department E on C.DepartmentPK = E.DepartmentPK and E.Status = 2
								group by E.Name
                                ORDER BY E.Name
    ";

                                cmd.CommandTimeout = 0;
                                cmd.Parameters.AddWithValue("@PeriodPK", _BudgetRpt.PeriodPK);


                                using (SqlDataReader dr0 = cmd.ExecuteReader())
                                {
                                    if (!dr0.HasRows)
                                    {
                                        return false;
                                    }
                                    else
                                    {

                                        //ATUR DATA GROUPINGNYA DULU
                                        List<BudgetRpt> rList = new List<BudgetRpt>();
                                        while (dr0.Read())
                                        {
                                            BudgetRpt rSingle = new BudgetRpt();
                                            rSingle.Category = Convert.ToString(dr0["Category"]);
                                            rSingle.AUMSeptember = Convert.ToDecimal(dr0["AUMSeptember"]);
                                            rSingle.BeginningAUMDes = Convert.ToDecimal(dr0["AUMDesember"]);
                                            rSingle.January1 = Convert.ToDecimal(dr0["January"]);
                                            rSingle.February1 = Convert.ToDecimal(dr0["February"]);
                                            rSingle.March1 = Convert.ToDecimal(dr0["March"]);
                                            rSingle.April1 = Convert.ToDecimal(dr0["April"]);
                                            rSingle.May1 = Convert.ToDecimal(dr0["May"]);
                                            rSingle.June1 = Convert.ToDecimal(dr0["June"]);
                                            rSingle.July1 = Convert.ToDecimal(dr0["July"]);
                                            rSingle.August1 = Convert.ToDecimal(dr0["August"]);
                                            rSingle.September1 = Convert.ToDecimal(dr0["September"]);
                                            rSingle.October1 = Convert.ToDecimal(dr0["October"]);
                                            rSingle.November1 = Convert.ToDecimal(dr0["November"]);
                                            rSingle.December1 = Convert.ToDecimal(dr0["December"]);
                                            rList.Add(rSingle);

                                        }

                                        var QueryByClientID =
                                         from r in rList
                                         orderby r.Category
                                         group r by new { } into rGroup
                                         select rGroup;

                                        int incRowExcel = 1;

                                        worksheet1.Cells[incRowExcel, 1].Value = "PT MNC ASSET MANAGEMENT";
                                        worksheet1.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                        worksheet1.Cells[incRowExcel, 1].Style.Font.Size = 14;
                                        incRowExcel++;
                                        worksheet1.Cells[incRowExcel, 1].Value = "LAPORAN AUM CABANG DAN SALES";
                                        worksheet1.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                        worksheet1.Cells[incRowExcel, 1].Style.Font.Size = 14;
                                        incRowExcel++;
                                        incRowExcel++;
                                        incRowExcel++;

                                        foreach (var rsHeader in QueryByClientID)
                                        {
                                            var _nextRow = incRowExcel + 1;

                                            worksheet1.Cells["A" + incRowExcel + ":Q" + _nextRow].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet1.Cells["A" + incRowExcel + ":Q" + _nextRow].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                            worksheet1.Cells["A" + incRowExcel + ":Q" + _nextRow].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet1.Cells["A" + incRowExcel + ":Q" + _nextRow].Style.Border.Right.Style = ExcelBorderStyle.Medium;

                                            worksheet1.Cells["A" + incRowExcel + ":Q" + _nextRow].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet1.Cells["A" + incRowExcel + ":Q" + _nextRow].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                            worksheet1.Cells["A" + incRowExcel + ":Q" + _nextRow].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                                            worksheet1.Cells["A" + incRowExcel + ":Q" + _nextRow].Style.Font.Bold = true;

                                            worksheet1.Cells[incRowExcel, 1].Value = "No";
                                            worksheet1.Cells["A" + incRowExcel + ":A" + _nextRow].Merge = true;
                                            worksheet1.Cells[incRowExcel, 2].Value = "Category";
                                            worksheet1.Cells["B" + incRowExcel + ":B" + _nextRow].Merge = true;
                                            worksheet1.Cells[incRowExcel, 3].Value = "Actual";
                                            worksheet1.Cells[incRowExcel, 4].Value = "Estimated";
                                            worksheet1.Cells[incRowExcel, 5].Value = "Budget";
                                            worksheet1.Cells["E" + incRowExcel + ":Q" + incRowExcel].Merge = true;
                                            incRowExcel++;

                                            worksheet1.Cells[incRowExcel, 3].Value = "YTD SEP " + _host.Get_PeriodByRevenue(_BudgetRpt.PeriodPK, 1);
                                            worksheet1.Cells[incRowExcel, 4].Value = "OKT-DES " + _host.Get_PeriodByRevenue(_BudgetRpt.PeriodPK, 1);

                                            worksheet1.Cells[incRowExcel, 5].Value = "JAN ";
                                            worksheet1.Cells[incRowExcel, 6].Value = "FEB ";
                                            worksheet1.Cells[incRowExcel, 7].Value = "MAR ";
                                            worksheet1.Cells[incRowExcel, 8].Value = "APR ";
                                            worksheet1.Cells[incRowExcel, 9].Value = "MAY ";
                                            worksheet1.Cells[incRowExcel, 10].Value = "JUN ";
                                            worksheet1.Cells[incRowExcel, 11].Value = "JUL ";
                                            worksheet1.Cells[incRowExcel, 12].Value = "AUG ";
                                            worksheet1.Cells[incRowExcel, 13].Value = "SEP ";
                                            worksheet1.Cells[incRowExcel, 14].Value = "OCT ";
                                            worksheet1.Cells[incRowExcel, 15].Value = "NOV ";
                                            worksheet1.Cells[incRowExcel, 16].Value = "DEC ";
                                            worksheet1.Cells[incRowExcel, 17].Value = "TOTAL";

                                            incRowExcel++;
                                            int _startRowDetail = incRowExcel;
                                            int _endRowDetail = 0;
                                            int _no = 1;
                                            foreach (var rsDetail in rsHeader)
                                            {


                                                //ThickBox Border

                                                worksheet1.Cells[incRowExcel, 1].Value = _no;
                                                worksheet1.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet1.Cells[incRowExcel, 2].Value = rsDetail.Category;
                                                worksheet1.Cells[incRowExcel, 2].Style.WrapText = true;
                                                worksheet1.Cells[incRowExcel, 3].Value = rsDetail.AUMSeptember;
                                                worksheet1.Cells[incRowExcel, 3].Style.Numberformat.Format = "#,##0";
                                                worksheet1.Cells[incRowExcel, 4].Value = rsDetail.BeginningAUMDes;
                                                worksheet1.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##0";

                                                worksheet1.Cells[incRowExcel, 5].Value = rsDetail.January1;
                                                worksheet1.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0";
                                                worksheet1.Cells[incRowExcel, 6].Value = rsDetail.February1;
                                                worksheet1.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0";
                                                worksheet1.Cells[incRowExcel, 7].Value = rsDetail.March1;
                                                worksheet1.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0";
                                                worksheet1.Cells[incRowExcel, 8].Value = rsDetail.April1;
                                                worksheet1.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0";
                                                worksheet1.Cells[incRowExcel, 9].Value = rsDetail.May1;
                                                worksheet1.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0";
                                                worksheet1.Cells[incRowExcel, 10].Value = rsDetail.June1;
                                                worksheet1.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0";
                                                worksheet1.Cells[incRowExcel, 11].Value = rsDetail.July1;
                                                worksheet1.Cells[incRowExcel, 11].Style.Numberformat.Format = "#,##0";
                                                worksheet1.Cells[incRowExcel, 12].Value = rsDetail.August1;
                                                worksheet1.Cells[incRowExcel, 12].Style.Numberformat.Format = "#,##0";
                                                worksheet1.Cells[incRowExcel, 13].Value = rsDetail.September1;
                                                worksheet1.Cells[incRowExcel, 13].Style.Numberformat.Format = "#,##0";
                                                worksheet1.Cells[incRowExcel, 14].Value = rsDetail.October1;
                                                worksheet1.Cells[incRowExcel, 14].Style.Numberformat.Format = "#,##0";
                                                worksheet1.Cells[incRowExcel, 15].Value = rsDetail.November1;
                                                worksheet1.Cells[incRowExcel, 15].Style.Numberformat.Format = "#,##0";
                                                worksheet1.Cells[incRowExcel, 16].Value = rsDetail.December1;
                                                worksheet1.Cells[incRowExcel, 16].Style.Numberformat.Format = "#,##0";

                                                worksheet1.Cells[incRowExcel, 17].Formula = "SUM(P" + incRowExcel + ":P" + incRowExcel + ")";
                                                worksheet1.Cells[incRowExcel, 17].Calculate();
                                                worksheet1.Cells[incRowExcel, 17].Style.Numberformat.Format = "#,##0";

                                                worksheet1.Cells["A" + incRowExcel + ":Q" + incRowExcel].Style.VerticalAlignment = ExcelVerticalAlignment.Top;

                                                _no++;
                                                _endRowDetail = incRowExcel;
                                                incRowExcel++;



                                            }

                                            worksheet1.Cells["A" + _startRowDetail + ":Q" + _endRowDetail].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet1.Cells["A" + _startRowDetail + ":Q" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                            worksheet1.Cells["A" + _startRowDetail + ":Q" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet1.Cells["A" + _startRowDetail + ":Q" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Medium;

                                            worksheet1.Cells[incRowExcel, 2].Value = "TOTAL :";
                                            worksheet1.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                                            worksheet1.Cells[incRowExcel, 3].Formula = "SUM(C" + _startRowDetail + ":C" + _endRowDetail + ")";
                                            worksheet1.Cells[incRowExcel, 3].Style.Font.Bold = true;
                                            worksheet1.Cells[incRowExcel, 3].Calculate();
                                            worksheet1.Cells[incRowExcel, 3].Style.Numberformat.Format = "#,##0";

                                            worksheet1.Cells[incRowExcel, 4].Formula = "SUM(D" + _startRowDetail + ":D" + _endRowDetail + ")";
                                            worksheet1.Cells[incRowExcel, 4].Style.Font.Bold = true;
                                            worksheet1.Cells[incRowExcel, 4].Calculate();
                                            worksheet1.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##0";

                                            worksheet1.Cells[incRowExcel, 5].Formula = "SUM(E" + _startRowDetail + ":E" + _endRowDetail + ")";
                                            worksheet1.Cells[incRowExcel, 5].Style.Font.Bold = true;
                                            worksheet1.Cells[incRowExcel, 5].Calculate();
                                            worksheet1.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0";

                                            worksheet1.Cells[incRowExcel, 6].Formula = "SUM(F" + _startRowDetail + ":F" + _endRowDetail + ")";
                                            worksheet1.Cells[incRowExcel, 6].Style.Font.Bold = true;
                                            worksheet1.Cells[incRowExcel, 6].Calculate();
                                            worksheet1.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0";

                                            worksheet1.Cells[incRowExcel, 7].Formula = "SUM(G" + _startRowDetail + ":G" + _endRowDetail + ")";
                                            worksheet1.Cells[incRowExcel, 7].Style.Font.Bold = true;
                                            worksheet1.Cells[incRowExcel, 7].Calculate();
                                            worksheet1.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0";

                                            worksheet1.Cells[incRowExcel, 8].Formula = "SUM(H" + _startRowDetail + ":H" + _endRowDetail + ")";
                                            worksheet1.Cells[incRowExcel, 8].Style.Font.Bold = true;
                                            worksheet1.Cells[incRowExcel, 8].Calculate();
                                            worksheet1.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0";

                                            worksheet1.Cells[incRowExcel, 9].Formula = "SUM(I" + _startRowDetail + ":I" + _endRowDetail + ")";
                                            worksheet1.Cells[incRowExcel, 9].Style.Font.Bold = true;
                                            worksheet1.Cells[incRowExcel, 9].Calculate();
                                            worksheet1.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0";

                                            worksheet1.Cells[incRowExcel, 10].Formula = "SUM(J" + _startRowDetail + ":J" + _endRowDetail + ")";
                                            worksheet1.Cells[incRowExcel, 10].Style.Font.Bold = true;
                                            worksheet1.Cells[incRowExcel, 10].Calculate();
                                            worksheet1.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0";

                                            worksheet1.Cells[incRowExcel, 11].Formula = "SUM(K" + _startRowDetail + ":K" + _endRowDetail + ")";
                                            worksheet1.Cells[incRowExcel, 11].Style.Font.Bold = true;
                                            worksheet1.Cells[incRowExcel, 11].Calculate();
                                            worksheet1.Cells[incRowExcel, 11].Style.Numberformat.Format = "#,##0";

                                            worksheet1.Cells[incRowExcel, 12].Formula = "SUM(L" + _startRowDetail + ":L" + _endRowDetail + ")";
                                            worksheet1.Cells[incRowExcel, 12].Style.Font.Bold = true;
                                            worksheet1.Cells[incRowExcel, 12].Calculate();
                                            worksheet1.Cells[incRowExcel, 12].Style.Numberformat.Format = "#,##0";

                                            worksheet1.Cells[incRowExcel, 13].Formula = "SUM(M" + _startRowDetail + ":M" + _endRowDetail + ")";
                                            worksheet1.Cells[incRowExcel, 13].Style.Font.Bold = true;
                                            worksheet1.Cells[incRowExcel, 13].Calculate();
                                            worksheet1.Cells[incRowExcel, 13].Style.Numberformat.Format = "#,##0";

                                            worksheet1.Cells[incRowExcel, 14].Formula = "SUM(N" + _startRowDetail + ":N" + _endRowDetail + ")";
                                            worksheet1.Cells[incRowExcel, 14].Style.Font.Bold = true;
                                            worksheet1.Cells[incRowExcel, 14].Calculate();
                                            worksheet1.Cells[incRowExcel, 14].Style.Numberformat.Format = "#,##0";

                                            worksheet1.Cells[incRowExcel, 15].Formula = "SUM(O" + _startRowDetail + ":O" + _endRowDetail + ")";
                                            worksheet1.Cells[incRowExcel, 15].Style.Font.Bold = true;
                                            worksheet1.Cells[incRowExcel, 15].Calculate();
                                            worksheet1.Cells[incRowExcel, 15].Style.Numberformat.Format = "#,##0";

                                            worksheet1.Cells[incRowExcel, 16].Formula = "SUM(P" + _startRowDetail + ":P" + _endRowDetail + ")";
                                            worksheet1.Cells[incRowExcel, 16].Style.Font.Bold = true;
                                            worksheet1.Cells[incRowExcel, 16].Calculate();
                                            worksheet1.Cells[incRowExcel, 16].Style.Numberformat.Format = "#,##0";

                                            worksheet1.Cells[incRowExcel, 17].Formula = "SUM(Q" + _startRowDetail + ":Q" + _endRowDetail + ")";
                                            worksheet1.Cells[incRowExcel, 17].Style.Font.Bold = true;
                                            worksheet1.Cells[incRowExcel, 17].Calculate();
                                            worksheet1.Cells[incRowExcel, 17].Style.Numberformat.Format = "#,##0";

                                            worksheet1.Cells["C" + incRowExcel + ":Q" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                            worksheet1.Cells["C" + incRowExcel + ":Q" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet1.Cells["C" + incRowExcel + ":Q" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Medium;

                                            incRowExcel = incRowExcel + 3;
                                        }



                                        worksheet1.PrinterSettings.FitToPage = true;
                                        worksheet1.PrinterSettings.FitToWidth = 1;
                                        worksheet1.PrinterSettings.FitToHeight = 0;
                                        worksheet1.PrinterSettings.PrintArea = worksheet1.Cells[1, 1, incRowExcel - 1, 17];
                                        worksheet1.Column(1).Width = 8;
                                        worksheet1.Column(2).Width = 25;
                                        worksheet1.Column(3).Width = 22;
                                        worksheet1.Column(4).Width = 22;
                                        worksheet1.Column(5).Width = 22;
                                        worksheet1.Column(6).Width = 22;
                                        worksheet1.Column(7).Width = 22;
                                        worksheet1.Column(8).Width = 22;
                                        worksheet1.Column(9).Width = 22;
                                        worksheet1.Column(10).Width = 22;
                                        worksheet1.Column(11).Width = 22;
                                        worksheet1.Column(12).Width = 22;
                                        worksheet1.Column(13).Width = 22;
                                        worksheet1.Column(14).Width = 22;
                                        worksheet1.Column(15).Width = 22;
                                        worksheet1.Column(16).Width = 22;
                                        worksheet1.Column(17).Width = 22;


                                        // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                        //worksheet1.Cells["A3:O14"].AutoFitColumns();  // UNTUK AUTO FIT UKURAN TULISAN DENGAN KOLOM, KEBETULAN INI BUAT HEADER AJA DIPAKENYA

                                        worksheet1.PrinterSettings.Orientation = eOrientation.Landscape; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                        worksheet1.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN
                                        worksheet1.HeaderFooter.OddHeader.RightAlignedText = "&14 LAPORAN AUM CABANG DAN SALES";

                                        // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                        worksheet1.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
                                        worksheet1.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
                                        worksheet1.View.PageLayoutView = Tools.DefaultReportPageLayoutView();
                                        worksheet1.View.ShowGridLines = Tools.DefaultReportShowGridLines();

                                        Image img = Image.FromFile(Tools.ReportImage);
                                        worksheet1.HeaderFooter.OddHeader.InsertPicture(img, PictureAlignment.Left);
                                        //worksheet1.HeaderFooter.OddHeader.CenteredText = Tools.DefaultReportHeaderCenterText();
                                        //worksheet1.HeaderFooter.OddHeader.LeftAlignedText = Tools.DefaultReportHeaderLeftText();

                                        worksheet1.HeaderFooter.OddFooter.RightAlignedText =
                                        string.Format("Page {0} of {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);

                                        ////worksheet1.HeaderFooter.OddFooter.CenteredText = Tools.DefaultReportFooterCenterText();
                                        worksheet1.HeaderFooter.OddFooter.LeftAlignedText = Tools.DefaultReportFooterLeftText();



                                    }
                                }
                            }

                        }

                        package.Save();
                        return true;

                    }


                }
                catch (Exception err)
                {
                    return false;
                    throw err;
                }
            }
            #endregion

            #region 07 Laporan Aum Summary
            if (_BudgetRpt.ReportName.Equals("07 Laporan Aum Summary"))
            {
                try
                {
                    using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                    {
                        DbCon.Open();
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {

                            cmd.CommandText =
                            @"

                            DECLARE @PeriodPKFrom INT
                                Declare @PeriodID nvarchar(50)

                                select @PeriodID  = YEAR(dateadd(year, -1, ID)) From Period where PeriodPK = @PeriodPK and status in (1,2)
                                select @PeriodPKFrom = PeriodPK from Period where ID = @PeriodID and status in (1,2)

                            DECLARE @Result TABLE
                            (
	                            InstrumentPK INT,
	                            MGTFee NUMERIC(18,8),
	                            BegLastYear NUMERIC(22,4),
	                            YTDLastYear NUMERIC(22,4)
                            )

                            declare @tableRevenue table
                            (
	                            PeriodPK int,
	                            ReportPeriodPK int,
	                            InstrumentPK int,
	                            AgentPK int,
	                            MGTFee numeric(10,4),
	                            January NUMERIC(22,4),
	                            February NUMERIC(22,4),
	                            March NUMERIC(22,4),
	                            April NUMERIC(22,4),
	                            May NUMERIC(22,4),
	                            June NUMERIC(22,4),
	                            July NUMERIC(22,4),
	                            August NUMERIC(22,4),
	                            September NUMERIC(22,4),
	                            October NUMERIC(22,4),
	                            November NUMERIC(22,4),
	                            December NUMERIC(22,4)
                            )


                            DECLARE @AUMFinalPerPeriod TABLE
                            (
	                            Date DATETIME,
	                            InstrumentPK INT,
	                            AUM NUMERIC(22,4)
                            )
                            INSERT INTO @AUMFinalPerPeriod
                                    ( Date,InstrumentPK, AUM )
                            SELECT MAX(A.date) Date,A.InstrumentPK,sum(A.AUM)  
                            FROM dbo.AumForBudgetBegBalance A
                            WHERE A.ReportPeriodPK = @PeriodPK AND A.status = 2 and A.PeriodPK = @PeriodPKFrom
                            GROUP BY A.InstrumentPK

                            insert into @tableRevenue
                            select A.PeriodPK,A.ReportPeriodPK,A.InstrumentPK,AgentPK,MGTFee,
							January*case when B.ReksadanaTypePK in (5,6,7) then 1 else isnull(C.RevenuePercent/100,1) end,February*case when B.ReksadanaTypePK in (5,6,7) then 1 else isnull(C.RevenuePercent/100,1) end,
							March*case when B.ReksadanaTypePK in (5,6,7) then 1 else isnull(C.RevenuePercent/100,1) end,April*case when B.ReksadanaTypePK in (5,6,7) then 1 else isnull(C.RevenuePercent/100,1) end,
							May*case when B.ReksadanaTypePK in (5,6,7) then 1 else isnull(C.RevenuePercent/100,1) end,June*case when B.ReksadanaTypePK in (5,6,7) then 1 else isnull(C.RevenuePercent/100,1) end,
							July*case when B.ReksadanaTypePK in (5,6,7) then 1 else isnull(C.RevenuePercent/100,1) end,August*case when B.ReksadanaTypePK in (5,6,7) then 1 else isnull(C.RevenuePercent/100,1) end,
							September*case when B.ReksadanaTypePK in (5,6,7) then 1 else isnull(C.RevenuePercent/100,1) end,October*case when B.ReksadanaTypePK in (5,6,7) then 1 else isnull(C.RevenuePercent/100,1) end,
							November*case when B.ReksadanaTypePK in (5,6,7) then 1 else isnull(C.RevenuePercent/100,1) end,December*case when B.ReksadanaTypePK in (5,6,7) then 1 else isnull(C.RevenuePercent/100,1) end 
                            from Revenue A
							left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status in (1,2)
							left join BudgetVersioning C on A.PeriodPK = C.PeriodPK and C.ReportPeriodPK = C.ReportPeriodPK and C.Status = 2 and Date = (
	                            select max(date) from BudgetVersioning where status in (1,2) and ReportPeriodPK = @periodpk
                            )
							where A.status = 2 and A.ReportPeriodPK = @periodpk

                            INSERT INTO @Result
                                    ( 
                                      InstrumentPK ,
                                      MGTFee ,
                                      BegLastYear ,
                                      YTDLastYear
                                    )
                            SELECT A.InstrumentPK,ISNULL(B.MGTFee,0) MGTFee
                            ,ISNULL(A.AUM,0) BegLastYear
                            ,CASE WHEN MONTH(A.Date) = 8 THEN ISNULL(A.AUM,0) + sum(ISNULL(B.September,0)  + ISNULL(B.October,0) + ISNULL(B.November,0) + ISNULL(B.December,0))
	                            WHEN MONTH(A.Date) = 9 THEN ISNULL(A.AUM,0)  + sum(ISNULL(B.October,0) + ISNULL(B.November,0) + ISNULL(B.December,0))
		                            WHEN MONTH(A.Date) = 10 THEN ISNULL(A.AUM,0)  + sum(ISNULL(B.November,0) + ISNULL(B.December,0))
			                            WHEN MONTH(A.Date) = 11 THEN ISNULL(A.AUM,0)  + sum(ISNULL(B.November,0) + ISNULL(B.December,0)) END YTDLastYear
                            FROM @AUMFinalPerPeriod A
                            left JOIN
                            (
	                            SELECT A.InstrumentPK,A.MGTFee,sum(A.January) January, sum(A.February) February, sum(A.March) March, sum(A.April) April,sum(A.May) May, sum(A.June) June, sum(A.July) July, sum(A.August) August, sum(A.September) September, sum(A.October) October, sum(A.November) November, sum(A.December) December FROM @tableRevenue A
	                            WHERE A.ReportPeriodPK = @PeriodPK AND A.PeriodPK <> @PeriodPK 
	                            group by A.InstrumentPK, A.MGTFee
                            )B ON A.InstrumentPK = B.InstrumentPK 
                            left JOIN
                            (
	                            SELECT A.InstrumentPK,sum(A.January) January, sum(A.February) February, sum(A.March) March, sum(A.April) April,sum(A.May) May, sum(A.June) June, sum(A.July) July, sum(A.August) August, sum(A.September) September, sum(A.October) October, sum(A.November) November, sum(A.December) December FROM @tableRevenue A WHERE 
	                            ReportPeriodPK = @PeriodPK AND A.PeriodPK =  @PeriodPK 
	                            group by A.InstrumentPK
                            )C ON  A.InstrumentPK = C.InstrumentPK
                            group by A.InstrumentPK ,B.MGTFee,A.AUM,A.Date

                            SELECT 
                            C.Name InstrumentName,isnull(D.Description,'') Category,A.MGTFee, ISNULL(A.BeglastYear,0) YTDSep ,SUM(ISNULL(A.YTDLastYear,0))  YTDDes
                            ,SUM(ISNULL(A.YTDLastYear,0) + ISNULL(B.January,0)) January
                            ,SUM(ISNULL(A.YTDLastYear,0) + ISNULL(B.January,0) + ISNULL(B.February,0)) February
                            ,SUM(ISNULL(A.YTDLastYear,0) + ISNULL(B.January,0) + ISNULL(B.February,0) + ISNULL(B.March,0)) March
                            ,SUM(ISNULL(A.YTDLastYear,0) + ISNULL(B.January,0) + ISNULL(B.February,0) + ISNULL(B.March,0) + ISNULL(B.April,0)) April
                            ,SUM(ISNULL(A.YTDLastYear,0) + ISNULL(B.January,0) + ISNULL(B.February,0) + ISNULL(B.March,0) + ISNULL(B.April,0) + ISNULL(B.May,0)) May
                            ,SUM(ISNULL(A.YTDLastYear,0) + ISNULL(B.January,0) + ISNULL(B.February,0) + ISNULL(B.March,0) + ISNULL(B.April,0) + ISNULL(B.May,0) + ISNULL(B.June,0)) June
                            ,SUM(ISNULL(A.YTDLastYear,0) + ISNULL(B.January,0) + ISNULL(B.February,0) + ISNULL(B.March,0) + ISNULL(B.April,0) + ISNULL(B.May,0) + ISNULL(B.June,0) + ISNULL(B.July,0)) July
                            ,SUM(ISNULL(A.YTDLastYear,0) + ISNULL(B.January,0) + ISNULL(B.February,0) + ISNULL(B.March,0) + ISNULL(B.April,0) + ISNULL(B.May,0) + ISNULL(B.June,0) + ISNULL(B.July,0) + ISNULL(B.August,0)) August
                            ,SUM(ISNULL(A.YTDLastYear,0) + ISNULL(B.January,0) + ISNULL(B.February,0) + ISNULL(B.March,0) + ISNULL(B.April,0) + ISNULL(B.May,0) + ISNULL(B.June,0) + ISNULL(B.July,0) + ISNULL(B.August,0) + ISNULL(B.September,0)) September
                            ,SUM(ISNULL(A.YTDLastYear,0) + ISNULL(B.January,0) + ISNULL(B.February,0) + ISNULL(B.March,0) + ISNULL(B.April,0) + ISNULL(B.May,0) + ISNULL(B.June,0) + ISNULL(B.July,0) + ISNULL(B.August,0) + ISNULL(B.September,0) + ISNULL(B.October,0)) October
                            ,SUM(ISNULL(A.YTDLastYear,0) + ISNULL(B.January,0) + ISNULL(B.February,0) + ISNULL(B.March,0) + ISNULL(B.April,0) + ISNULL(B.May,0) + ISNULL(B.June,0) + ISNULL(B.July,0) + ISNULL(B.August,0) + ISNULL(B.September,0) + ISNULL(B.October,0) + ISNULL(B.November,0)) November
                            ,SUM(ISNULL(A.YTDLastYear,0) + ISNULL(B.January,0) + ISNULL(B.February,0) + ISNULL(B.March,0) + ISNULL(B.April,0) + ISNULL(B.May,0) + ISNULL(B.June,0) + ISNULL(B.July,0) + ISNULL(B.August,0) + ISNULL(B.September,0) + ISNULL(B.October,0) + ISNULL(B.November,0) + ISNULL(B.December,0)) December
                            FROM @Result A
                            LEFT JOIN
                            (
	                            SELECT A.InstrumentPK,sum(A.January) January, sum(A.February) February, sum(A.March) March, sum(A.April) April,sum(A.May) May, sum(A.June) June, sum(A.July) July, sum(A.August) August, sum(A.September) September, sum(A.October) October, sum(A.November) November, sum(A.December) December FROM  @tableRevenue A
	                            WHERE A.ReportPeriodPK = @PeriodPK AND A.PeriodPK = @PeriodPK 
	                            group by A.InstrumentPK
                            )B ON A.InstrumentPK = B.InstrumentPK
                            left join Instrument C on A.InstrumentPK = C.InstrumentPK and C.status in (1,2)
                            left join ReksadanaType D on C.ReksadanaTypePK = D.ReksadanaTypePK  and D.status in (1,2)
                            GROUP BY A.InstrumentPK,BeglastYear,C.Name,D.Description,A.MGTFee
                            ORDER BY A.InstrumentPK



";

                            cmd.CommandTimeout = 0;
                            cmd.Parameters.AddWithValue("@PeriodPK", _BudgetRpt.PeriodPK);


                            using (SqlDataReader dr0 = cmd.ExecuteReader())
                            {
                                if (!dr0.HasRows)
                                {
                                    return false;
                                }
                                else
                                {
                                    string filePath = Tools.ReportsPath + "LaporanAumSummary" + "_" + _userID + ".xlsx";
                                    string pdfPath = Tools.ReportsPath + "LaporanAumSummary" + "_" + _userID + ".pdf";
                                    FileInfo excelFile = new FileInfo(filePath);
                                    if (excelFile.Exists)
                                    {
                                        excelFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                        excelFile = new FileInfo(filePath);
                                    }


                                    // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                    using (ExcelPackage package = new ExcelPackage(excelFile))
                                    {
                                        package.Workbook.Properties.Title = "BudgetReport";
                                        package.Workbook.Properties.Author = Tools.DefaultReportAuthor();
                                        package.Workbook.Properties.Comments = Tools.DefaultReportComments();
                                        package.Workbook.Properties.Company = Tools.DefaultReportCompany();
                                        package.Workbook.Properties.SetCustomPropertyValue("Checked by", _userID);
                                        package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", Tools.DefaultReportAssemblyName());

                                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Budget Report");


                                        //ATUR DATA GROUPINGNYA DULU
                                        List<BudgetRpt> rList = new List<BudgetRpt>();
                                        while (dr0.Read())
                                        {
                                            BudgetRpt rSingle = new BudgetRpt();
                                            rSingle.NamaProduk = Convert.ToString(dr0["InstrumentName"]);
                                            rSingle.ProdukKategori = Convert.ToString(dr0["Category"]);
                                            rSingle.MGTFee = Convert.ToDecimal(dr0["MGTFee"]);
                                            rSingle.YTDSep = Convert.ToDecimal(dr0["YTDSep"]);
                                            rSingle.YTDDes = Convert.ToDecimal(dr0["YTDDes"]);
                                            rSingle.January1 = Convert.ToDecimal(dr0["January"]);
                                            rSingle.February1 = Convert.ToDecimal(dr0["February"]);
                                            rSingle.March1 = Convert.ToDecimal(dr0["March"]);
                                            rSingle.April1 = Convert.ToDecimal(dr0["April"]);
                                            rSingle.May1 = Convert.ToDecimal(dr0["May"]);
                                            rSingle.June1 = Convert.ToDecimal(dr0["June"]);
                                            rSingle.July1 = Convert.ToDecimal(dr0["July"]);
                                            rSingle.August1 = Convert.ToDecimal(dr0["August"]);
                                            rSingle.September1 = Convert.ToDecimal(dr0["September"]);
                                            rSingle.October1 = Convert.ToDecimal(dr0["October"]);
                                            rSingle.November1 = Convert.ToDecimal(dr0["November"]);
                                            rSingle.December1 = Convert.ToDecimal(dr0["December"]);
                                            rList.Add(rSingle);

                                        }

                                        var QueryByClientID =
                                         from r in rList
                                         orderby r.ProdukKategori
                                         group r by new { r.ProdukKategori } into rGroup
                                         select rGroup;

                                        int incRowExcel = 1;
                                        worksheet.Cells[incRowExcel, 1].Value = "PT MNC ASSET MANAGEMENT";
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Size = 14;
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "LAPORAN AUM SUMMARY";
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Size = 14;
                                        incRowExcel++;
                                        incRowExcel++;
                                        incRowExcel++;

                                        worksheet.Cells["A" + incRowExcel + ":S" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["A" + incRowExcel + ":S" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["A" + incRowExcel + ":S" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["A" + incRowExcel + ":S" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Medium;


                                        worksheet.Cells[incRowExcel, 1].Value = "No";
                                        worksheet.Cells[incRowExcel, 2].Value = "Nama Produk";
                                        worksheet.Cells[incRowExcel, 3].Value = "Produk Kategori";
                                        worksheet.Cells[incRowExcel, 4].Value = "MGT Fee";
                                        worksheet.Cells[incRowExcel, 5].Value = _host.Get_BeginningForBudgetReport(_BudgetRpt.PeriodPK);
                                        worksheet.Cells[incRowExcel, 6].Value = "YTD Des " + _host.Get_PeriodByRevenue(_BudgetRpt.PeriodPK, 1);

                                        worksheet.Cells[incRowExcel, 7].Value = "JAN ";
                                        worksheet.Cells[incRowExcel, 8].Value = "FEB ";
                                        worksheet.Cells[incRowExcel, 9].Value = "MAR ";
                                        worksheet.Cells[incRowExcel, 10].Value = "APR ";
                                        worksheet.Cells[incRowExcel, 11].Value = "MAY ";
                                        worksheet.Cells[incRowExcel, 12].Value = "JUN ";
                                        worksheet.Cells[incRowExcel, 13].Value = "JUL ";
                                        worksheet.Cells[incRowExcel, 14].Value = "AUG ";
                                        worksheet.Cells[incRowExcel, 15].Value = "SEP ";
                                        worksheet.Cells[incRowExcel, 16].Value = "OCT ";
                                        worksheet.Cells[incRowExcel, 17].Value = "NOV ";
                                        worksheet.Cells[incRowExcel, 18].Value = "DEC ";
                                        worksheet.Cells[incRowExcel, 19].Value = "TOTAL";

                                        worksheet.Cells["A" + incRowExcel + ":S" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        worksheet.Cells["A" + incRowExcel + ":S" + incRowExcel].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                        worksheet.Cells["A" + incRowExcel + ":S" + incRowExcel].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                                        worksheet.Cells["A" + incRowExcel + ":S" + incRowExcel].Style.Font.Bold = true;
                                        incRowExcel++;
                                        int _startTotal = incRowExcel;
                                        int _startRowDetail = incRowExcel;
                                        int _endRowDetail = 0;
                                        int _no = 1;

                                        foreach (var rsHeader in QueryByClientID)
                                        {
                                            _startRowDetail = incRowExcel;
                                            foreach (var rsDetail in rsHeader)
                                            {
                                                //ThickBox Border

                                                worksheet.Cells["C" + incRowExcel + ":S" + incRowExcel].Style.Fill.PatternType = ExcelFillStyle.None;

                                                worksheet.Cells[incRowExcel, 1].Value = _no;
                                                worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 2].Value = rsDetail.NamaProduk;
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.ProdukKategori;
                                                worksheet.Cells[incRowExcel, 4].Value = rsDetail.MGTFee;
                                                worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 5].Value = rsDetail.YTDSep;
                                                worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 6].Value = rsDetail.YTDDes;
                                                worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0";

                                                worksheet.Cells[incRowExcel, 7].Value = rsDetail.January1;
                                                worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 8].Value = rsDetail.February1;
                                                worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 9].Value = rsDetail.March1;
                                                worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 10].Value = rsDetail.April1;
                                                worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 11].Value = rsDetail.May1;
                                                worksheet.Cells[incRowExcel, 11].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 12].Value = rsDetail.June1;
                                                worksheet.Cells[incRowExcel, 12].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 13].Value = rsDetail.July1;
                                                worksheet.Cells[incRowExcel, 13].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 14].Value = rsDetail.August1;
                                                worksheet.Cells[incRowExcel, 14].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 15].Value = rsDetail.September1;
                                                worksheet.Cells[incRowExcel, 15].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 16].Value = rsDetail.October1;
                                                worksheet.Cells[incRowExcel, 16].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 17].Value = rsDetail.November1;
                                                worksheet.Cells[incRowExcel, 17].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 18].Value = rsDetail.December1;
                                                worksheet.Cells[incRowExcel, 18].Style.Numberformat.Format = "#,##0";

                                                worksheet.Cells[incRowExcel, 19].Formula = "SUM(R" + incRowExcel + ":R" + incRowExcel + ")";
                                                worksheet.Cells[incRowExcel, 19].Calculate();
                                                worksheet.Cells[incRowExcel, 19].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells["A" + incRowExcel + ":S" + incRowExcel].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                                                _no++;
                                                _endRowDetail = incRowExcel;
                                                incRowExcel++;



                                            }

                                            worksheet.Cells["A" + _startRowDetail + ":S" + _endRowDetail].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + _startRowDetail + ":S" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + _startRowDetail + ":S" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + _startRowDetail + ":S" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells["D" + incRowExcel + ":S" + incRowExcel].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                            worksheet.Cells["D" + incRowExcel + ":S" + incRowExcel].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightBlue);


                                            worksheet.Cells[incRowExcel, 4].Value = "TOTAL :";
                                            worksheet.Cells[incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                                            worksheet.Cells[incRowExcel, 5].Formula = "SUM(E" + _startRowDetail + ":E" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 5].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 5].Calculate();
                                            worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0";

                                            worksheet.Cells[incRowExcel, 6].Formula = "SUM(F" + _startRowDetail + ":F" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 6].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 6].Calculate();
                                            worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0";

                                            worksheet.Cells[incRowExcel, 7].Formula = "SUM(G" + _startRowDetail + ":G" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 7].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 7].Calculate();
                                            worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0";

                                            worksheet.Cells[incRowExcel, 8].Formula = "SUM(H" + _startRowDetail + ":H" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 8].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 8].Calculate();
                                            worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0";

                                            worksheet.Cells[incRowExcel, 9].Formula = "SUM(I" + _startRowDetail + ":I" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 9].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 9].Calculate();
                                            worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0";

                                            worksheet.Cells[incRowExcel, 10].Formula = "SUM(J" + _startRowDetail + ":J" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 10].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 10].Calculate();
                                            worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0";

                                            worksheet.Cells[incRowExcel, 11].Formula = "SUM(K" + _startRowDetail + ":K" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 11].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 11].Calculate();
                                            worksheet.Cells[incRowExcel, 11].Style.Numberformat.Format = "#,##0";

                                            worksheet.Cells[incRowExcel, 12].Formula = "SUM(L" + _startRowDetail + ":L" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 12].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 12].Calculate();
                                            worksheet.Cells[incRowExcel, 12].Style.Numberformat.Format = "#,##0";

                                            worksheet.Cells[incRowExcel, 13].Formula = "SUM(M" + _startRowDetail + ":M" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 13].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 13].Calculate();
                                            worksheet.Cells[incRowExcel, 13].Style.Numberformat.Format = "#,##0";

                                            worksheet.Cells[incRowExcel, 14].Formula = "SUM(N" + _startRowDetail + ":N" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 14].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 14].Calculate();
                                            worksheet.Cells[incRowExcel, 14].Style.Numberformat.Format = "#,##0";

                                            worksheet.Cells[incRowExcel, 15].Formula = "SUM(O" + _startRowDetail + ":O" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 15].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 15].Calculate();
                                            worksheet.Cells[incRowExcel, 15].Style.Numberformat.Format = "#,##0";

                                            worksheet.Cells[incRowExcel, 16].Formula = "SUM(P" + _startRowDetail + ":P" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 16].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 16].Calculate();
                                            worksheet.Cells[incRowExcel, 16].Style.Numberformat.Format = "#,##0";

                                            worksheet.Cells[incRowExcel, 17].Formula = "SUM(Q" + _startRowDetail + ":Q" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 17].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 17].Calculate();
                                            worksheet.Cells[incRowExcel, 17].Style.Numberformat.Format = "#,##0";

                                            worksheet.Cells[incRowExcel, 18].Formula = "SUM(R" + _startRowDetail + ":R" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 18].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 18].Calculate();
                                            worksheet.Cells[incRowExcel, 18].Style.Numberformat.Format = "#,##0";

                                            worksheet.Cells[incRowExcel, 19].Formula = "SUM(S" + _startRowDetail + ":S" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 19].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 19].Calculate();
                                            worksheet.Cells[incRowExcel, 19].Style.Numberformat.Format = "#,##0";

                                            worksheet.Cells["D" + incRowExcel + ":S" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["D" + incRowExcel + ":S" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["D" + incRowExcel + ":S" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Medium;

                                            _endRowDetail = incRowExcel;
                                            incRowExcel++;


                                        }

                                        worksheet.Cells["A" + _startRowDetail + ":S" + _endRowDetail].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["A" + _startRowDetail + ":S" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["A" + _startRowDetail + ":S" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["A" + _startRowDetail + ":S" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Medium;

                                        worksheet.Cells["D" + incRowExcel + ":S" + incRowExcel].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                        worksheet.Cells["D" + incRowExcel + ":S" + incRowExcel].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightBlue);

                                        worksheet.Cells[incRowExcel, 4].Value = "GRAND TOTAL :";
                                        worksheet.Cells[incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                                        worksheet.Cells[incRowExcel, 5].Formula = "SUM(E" + _startTotal + ":E" + _endRowDetail + ")/2";
                                        worksheet.Cells[incRowExcel, 5].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 5].Calculate();
                                        worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0";

                                        worksheet.Cells[incRowExcel, 6].Formula = "SUM(F" + _startTotal + ":F" + _endRowDetail + ")/2";
                                        worksheet.Cells[incRowExcel, 6].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 6].Calculate();
                                        worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0";

                                        worksheet.Cells[incRowExcel, 7].Formula = "SUM(G" + _startTotal + ":G" + _endRowDetail + ")/2";
                                        worksheet.Cells[incRowExcel, 7].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 7].Calculate();
                                        worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0";

                                        worksheet.Cells[incRowExcel, 8].Formula = "SUM(H" + _startTotal + ":H" + _endRowDetail + ")/2";
                                        worksheet.Cells[incRowExcel, 8].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 8].Calculate();
                                        worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0";

                                        worksheet.Cells[incRowExcel, 9].Formula = "SUM(I" + _startTotal + ":I" + _endRowDetail + ")/2";
                                        worksheet.Cells[incRowExcel, 9].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 9].Calculate();
                                        worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0";

                                        worksheet.Cells[incRowExcel, 10].Formula = "SUM(J" + _startTotal + ":J" + _endRowDetail + ")/2";
                                        worksheet.Cells[incRowExcel, 10].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 10].Calculate();
                                        worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0";

                                        worksheet.Cells[incRowExcel, 11].Formula = "SUM(K" + _startTotal + ":K" + _endRowDetail + ")/2";
                                        worksheet.Cells[incRowExcel, 11].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 11].Calculate();
                                        worksheet.Cells[incRowExcel, 11].Style.Numberformat.Format = "#,##0";

                                        worksheet.Cells[incRowExcel, 12].Formula = "SUM(L" + _startTotal + ":L" + _endRowDetail + ")/2";
                                        worksheet.Cells[incRowExcel, 12].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 12].Calculate();
                                        worksheet.Cells[incRowExcel, 12].Style.Numberformat.Format = "#,##0";

                                        worksheet.Cells[incRowExcel, 13].Formula = "SUM(M" + _startTotal + ":M" + _endRowDetail + ")/2";
                                        worksheet.Cells[incRowExcel, 13].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 13].Calculate();
                                        worksheet.Cells[incRowExcel, 13].Style.Numberformat.Format = "#,##0";

                                        worksheet.Cells[incRowExcel, 14].Formula = "SUM(N" + _startTotal + ":N" + _endRowDetail + ")/2";
                                        worksheet.Cells[incRowExcel, 14].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 14].Calculate();
                                        worksheet.Cells[incRowExcel, 14].Style.Numberformat.Format = "#,##0";

                                        worksheet.Cells[incRowExcel, 15].Formula = "SUM(O" + _startTotal + ":O" + _endRowDetail + ")/2";
                                        worksheet.Cells[incRowExcel, 15].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 15].Calculate();
                                        worksheet.Cells[incRowExcel, 15].Style.Numberformat.Format = "#,##0";

                                        worksheet.Cells[incRowExcel, 16].Formula = "SUM(P" + _startTotal + ":P" + _endRowDetail + ")/2";
                                        worksheet.Cells[incRowExcel, 16].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 16].Calculate();
                                        worksheet.Cells[incRowExcel, 16].Style.Numberformat.Format = "#,##0";

                                        worksheet.Cells[incRowExcel, 17].Formula = "SUM(Q" + _startTotal + ":Q" + _endRowDetail + ")/2";
                                        worksheet.Cells[incRowExcel, 17].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 17].Calculate();
                                        worksheet.Cells[incRowExcel, 17].Style.Numberformat.Format = "#,##0";

                                        worksheet.Cells[incRowExcel, 18].Formula = "SUM(R" + _startTotal + ":R" + _endRowDetail + ")/2";
                                        worksheet.Cells[incRowExcel, 18].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 18].Calculate();
                                        worksheet.Cells[incRowExcel, 18].Style.Numberformat.Format = "#,##0";

                                        worksheet.Cells[incRowExcel, 19].Formula = "SUM(S" + _startTotal + ":S" + _endRowDetail + ")/2";
                                        worksheet.Cells[incRowExcel, 19].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 19].Calculate();
                                        worksheet.Cells[incRowExcel, 19].Style.Numberformat.Format = "#,##0";

                                        worksheet.Cells["D" + incRowExcel + ":S" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["D" + incRowExcel + ":S" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["D" + incRowExcel + ":S" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Medium;



                                        worksheet.PrinterSettings.FitToPage = true;
                                        worksheet.PrinterSettings.FitToWidth = 1;
                                        worksheet.PrinterSettings.FitToHeight = 0;
                                        worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel - 1, 19];
                                        worksheet.Column(1).Width = 8;
                                        worksheet.Column(2).AutoFit();
                                        worksheet.Column(3).AutoFit();
                                        worksheet.Column(4).Width = 17;
                                        worksheet.Column(5).Width = 17;
                                        worksheet.Column(6).Width = 17;
                                        worksheet.Column(7).Width = 17;
                                        worksheet.Column(8).Width = 17;
                                        worksheet.Column(9).Width = 17;
                                        worksheet.Column(10).Width = 17;
                                        worksheet.Column(11).Width = 17;
                                        worksheet.Column(12).Width = 17;
                                        worksheet.Column(13).Width = 17;
                                        worksheet.Column(14).Width = 17;
                                        worksheet.Column(15).Width = 17;
                                        worksheet.Column(16).Width = 17;
                                        worksheet.Column(17).Width = 17;
                                        worksheet.Column(18).Width = 17;
                                        worksheet.Column(19).Width = 17;



                                        // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                        //worksheet.Cells["A3:O14"].AutoFitColumns();  // UNTUK AUTO FIT UKURAN TULISAN DENGAN KOLOM, KEBETULAN INI BUAT HEADER AJA DIPAKENYA

                                        worksheet.PrinterSettings.Orientation = eOrientation.Landscape; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                        worksheet.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN
                                        worksheet.HeaderFooter.OddHeader.RightAlignedText = "&14 LAPORAN AUM SUMMARY";

                                        // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                        worksheet.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
                                        worksheet.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
                                        worksheet.View.PageLayoutView = Tools.DefaultReportPageLayoutView();
                                        worksheet.View.ShowGridLines = Tools.DefaultReportShowGridLines();

                                        Image img = Image.FromFile(Tools.ReportImage);
                                        worksheet.HeaderFooter.OddHeader.InsertPicture(img, PictureAlignment.Left);
                                        //worksheet.HeaderFooter.OddHeader.CenteredText = Tools.DefaultReportHeaderCenterText();
                                        //worksheet.HeaderFooter.OddHeader.LeftAlignedText = Tools.DefaultReportHeaderLeftText();

                                        worksheet.HeaderFooter.OddFooter.RightAlignedText =
                                        string.Format("Page {0} of {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);

                                        ////worksheet.HeaderFooter.OddFooter.CenteredText = Tools.DefaultReportFooterCenterText();
                                        worksheet.HeaderFooter.OddFooter.LeftAlignedText = Tools.DefaultReportFooterLeftText();



                                        package.Save();
                                        return true;
                                    }
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
            #endregion

            #region 08 Laporan MI Fee Cabang dan Sales
            if (_BudgetRpt.ReportName.Equals("08 Laporan MI Fee Cabang dan Sales"))
            {
                try
                {
                    string filePath = Tools.ReportsPath + "LaporanMIFeeCabangdanSales" + "_" + _userID + ".xlsx";
                    string pdfPath = Tools.ReportsPath + "LaporanMIFeeCabangdanSales" + "_" + _userID + ".pdf";
                    FileInfo excelFile = new FileInfo(filePath);
                    if (excelFile.Exists)
                    {
                        excelFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                        excelFile = new FileInfo(filePath);
                    }

                    // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                    using (ExcelPackage package = new ExcelPackage(excelFile))
                    {
                        using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                        {
                            DbCon.Open();
                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {

                                cmd.CommandText =
                                @"

                                DECLARE @PeriodPKFrom INT
                                Declare @PeriodID nvarchar(50)

                                select @PeriodID  = YEAR(dateadd(year, -1, ID)) From Period where PeriodPK = @PeriodPK and status in (1,2)
                                select @PeriodPKFrom = PeriodPK from Period where ID = @PeriodID and status in (1,2)

                                DECLARE @Result TABLE
                                (
	                                AgentPK INT,
	                                InstrumentPK INT,
	                                MGTFee NUMERIC(18,8),
	                                BegLastYear NUMERIC(22,4),
	                                YTDLastYear NUMERIC(22,4),
	                                AUMLastYear numeric(22,4)
                                )


                                declare @tableRevenue table
                                (
	                                PeriodPK int,
	                                ReportPeriodPK int,
	                                InstrumentPK int,
	                                AgentPK int,
	                                MGTFee numeric(10,4),
	                                January NUMERIC(22,4),
	                                February NUMERIC(22,4),
	                                March NUMERIC(22,4),
	                                April NUMERIC(22,4),
	                                May NUMERIC(22,4),
	                                June NUMERIC(22,4),
	                                July NUMERIC(22,4),
	                                August NUMERIC(22,4),
	                                September NUMERIC(22,4),
	                                October NUMERIC(22,4),
	                                November NUMERIC(22,4),
	                                December NUMERIC(22,4)
                                )

                                DECLARE @AUMFinalPerPeriod TABLE
                                (
	                                Date DATETIME,
	                                InstrumentPK INT,
	                                AgentPK int,
	                                AUM NUMERIC(22,4),
	                                Mfee numeric(22,4)
                                )
                                INSERT INTO @AUMFinalPerPeriod
                                        ( Date,InstrumentPK, AgentPK, AUM, Mfee )
                                SELECT MAX(A.date) Date,A.InstrumentPK,A.AgentPK,sum(A.AUM), sum(A.MFeeAmount)
                                FROM dbo.AumForBudgetBegBalance A
                                WHERE A.ReportPeriodPK = @PeriodPK AND A.status = 2 and periodpk = @PeriodPKFrom
                                GROUP BY A.InstrumentPK,A.AgentPK

                                insert into @tableRevenue
                                select A.PeriodPK,A.ReportPeriodPK,A.InstrumentPK,AgentPK,MGTFee,
							    January*case when B.ReksadanaTypePK in (5,6,7) then 1 else isnull(C.RevenuePercent/100,1) end,February*case when B.ReksadanaTypePK in (5,6,7) then 1 else isnull(C.RevenuePercent/100,1) end,
							    March*case when B.ReksadanaTypePK in (5,6,7) then 1 else isnull(C.RevenuePercent/100,1) end,April*case when B.ReksadanaTypePK in (5,6,7) then 1 else isnull(C.RevenuePercent/100,1) end,
							    May*case when B.ReksadanaTypePK in (5,6,7) then 1 else isnull(C.RevenuePercent/100,1) end,June*case when B.ReksadanaTypePK in (5,6,7) then 1 else isnull(C.RevenuePercent/100,1) end,
							    July*case when B.ReksadanaTypePK in (5,6,7) then 1 else isnull(C.RevenuePercent/100,1) end,August*case when B.ReksadanaTypePK in (5,6,7) then 1 else isnull(C.RevenuePercent/100,1) end,
							    September*case when B.ReksadanaTypePK in (5,6,7) then 1 else isnull(C.RevenuePercent/100,1) end,October*case when B.ReksadanaTypePK in (5,6,7) then 1 else isnull(C.RevenuePercent/100,1) end,
							    November*case when B.ReksadanaTypePK in (5,6,7) then 1 else isnull(C.RevenuePercent/100,1) end,December*case when B.ReksadanaTypePK in (5,6,7) then 1 else isnull(C.RevenuePercent/100,1) end 
                                from Revenue A
							    left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status in (1,2)
								left join BudgetVersioning C on A.PeriodPK = C.PeriodPK and C.ReportPeriodPK = C.ReportPeriodPK and C.Status = 2 and Date = (
	                                select max(date) from BudgetVersioning where status in (1,2) and ReportPeriodPK = @periodpk
                                )
							    where A.status = 2 and A.ReportPeriodPK = @periodpk

                                INSERT INTO @Result
                                        ( AgentPK ,
                                          InstrumentPK ,
                                          MGTFee ,
                                          BegLastYear ,
                                          YTDLastYear,
		                                  AUMLastYear
                                        )
                                SELECT A.AgentPK,A.InstrumentPK,ISNULL(B.MGTFee,0) MGTFee
                                ,ISNULL(A.Mfee,0) BegLastYear
                                --handle mfee YTDLastyear
                                ,CASE WHEN MONTH(A.Date) = 8 THEN ISNULL(A.Mfee,0) + 
	                                ((isnull(A.AUM,0) + ISNULL(B.September,0) + isnull(A.AUM,0) ) /2 / 12 * B.MGTFee /100 ) +  -- september
	                                ((isnull(A.AUM,0) + ISNULL(B.September,0) + isnull(A.AUM,0) + ISNULL(B.September,0) + ISNULL(B.October,0) ) /2 / 12 * B.MGTFee / 100 )+ -- Oktober
	                                ((isnull(A.AUM,0) + ISNULL(B.September,0) + ISNULL(B.October,0) + isnull(A.AUM,0) + ISNULL(B.September,0) + ISNULL(B.October,0) + ISNULL(B.November,0) ) /2 / 12 * B.MGTFee / 100 )+ -- November
	                                ((isnull(A.AUM,0) + ISNULL(B.September,0) + ISNULL(B.October,0) + ISNULL(B.November,0) + isnull(A.AUM,0) + ISNULL(B.September,0) + ISNULL(B.October,0) + ISNULL(B.November,0) + ISNULL(B.December,0) ) /2 / 12 * B.MGTFee / 100 ) -- Desember

	                                WHEN MONTH(A.Date) = 9 THEN ISNULL(A.Mfee,0)  +
		                                ((isnull(A.AUM,0) + isnull(A.AUM,0) + ISNULL(B.October,0) ) /2 / 12 * B.MGTFee / 100 )+ -- Oktober
		                                ((isnull(A.AUM,0) + ISNULL(B.October,0) + isnull(A.AUM,0) + ISNULL(B.October,0) + ISNULL(B.November,0) ) /2 / 12 * B.MGTFee / 100 )+ -- November
		                                ((isnull(A.AUM,0) + ISNULL(B.October,0) + ISNULL(B.November,0) + isnull(A.AUM,0) + ISNULL(B.October,0) + ISNULL(B.November,0) + ISNULL(B.December,0) ) /2 / 12 * B.MGTFee / 100 ) -- Desember

		                                WHEN MONTH(A.Date) = 10 THEN ISNULL(A.Mfee,0)  + 
			                                ((isnull(A.AUM,0) + isnull(A.AUM,0) + ISNULL(B.November,0) ) /2 / 12 * B.MGTFee / 100 )+ -- November
			                                ((isnull(A.AUM,0) + ISNULL(B.November,0) + isnull(A.AUM,0) + ISNULL(B.November,0) + ISNULL(B.December,0) ) /2 / 12 * B.MGTFee / 100 ) -- Desember

			                                WHEN MONTH(A.Date) = 11 THEN ISNULL(A.Mfee,0)  +
				                                ((isnull(A.AUM,0) + isnull(A.AUM,0) + ISNULL(B.December,0) ) /2 / 12 * B.MGTFee / 100 ) -- Desember
			                                 END YTDLastYear,
                                --AUM LAST YEAR
                                CASE WHEN MONTH(A.Date) = 8 THEN ISNULL(A.AUM,0) + ISNULL(B.September,0)  + ISNULL(B.October,0) + ISNULL(B.November,0) + ISNULL(B.December,0)
		                                WHEN MONTH(A.Date) = 9 THEN ISNULL(A.AUM,0)  + ISNULL(B.October,0) + ISNULL(B.November,0) + ISNULL(B.December,0)
			                                WHEN MONTH(A.Date) = 10 THEN ISNULL(A.AUM,0)  + ISNULL(B.November,0) + ISNULL(B.December,0)
				                                WHEN MONTH(A.Date) = 11 THEN ISNULL(A.AUM,0)  + ISNULL(B.November,0) + ISNULL(B.December,0) END AUMLastYear
                                FROM @AUMFinalPerPeriod A
                                LEFT JOIN
                                (
	                                SELECT A.InstrumentPK,A.AgentPK,A.MGTFee,sum(A.January) January, sum(A.February) February, sum(A.March) March, sum(A.April) April,sum(A.May) May, sum(A.June) June, sum(A.July) July, sum(A.August) August, sum(A.September) September, sum(A.October) October, sum(A.November) November, sum(A.December) December FROM @tableRevenue A
	                                WHERE A.ReportPeriodPK = @PeriodPK AND A.PeriodPK <> @PeriodPK
	                                group by A.InstrumentPK, A.MGTFee,A.AgentPK
                                )B ON A.InstrumentPK = B.InstrumentPK AND A.AgentPK = B.AgentPK





                                select A.SalesName,A.InstrumentName,A.Category,A.BranchName,A.MGTFee MGTFeePercent,AUMSeptember MFeeAmountYTDSept,MFeeLastYear MFeeAmountYTDDec,
                                (AUMLastYear + January)/2/12/ 100 * MGTFee January,(January + February)/2/12/ 100 * MGTFee February,(February + March)/2/12/ 100 * MGTFee March, (March + April)/2/12/ 100 * MGTFee April,
                                (April + May)/2/12/ 100 * MGTFee May,(May + June)/2/12/ 100 * MGTFee june,(June + July)/2/12/ 100 * MGTFee July,(July + August)/2/12/ 100 * MGTFee August,(August + September)/2/12/ 100 * MGTFee September,
                                (September + October)/2/12/ 100 * MGTFee October,(October + November)/2/12/ 100 * MGTFee November,(November + December)/2/12/ 100 * MGTFee December
                                 from (
                                SELECT C.Name SalesName, D.Name InstrumentName,isnull(F.Description,'') Category,isnull(E.Name,'') BranchName,A.MGTFee,A.BeglastYear AUMSeptember,ISNULL(A.YTDLastYear,0)  MFeeLastYear, A.AUMLastYear
                                ,ISNULL(A.AUMLastYear,0) + ISNULL(B.January,0) January
                                ,ISNULL(A.AUMLastYear,0) + ISNULL(B.January,0) + ISNULL(B.February,0) February
                                ,ISNULL(A.AUMLastYear,0) + ISNULL(B.January,0) + ISNULL(B.February,0) + ISNULL(B.March,0) March
                                ,ISNULL(A.AUMLastYear,0) + ISNULL(B.January,0) + ISNULL(B.February,0) + ISNULL(B.March,0) + ISNULL(B.April,0) April
                                ,ISNULL(A.AUMLastYear,0) + ISNULL(B.January,0) + ISNULL(B.February,0) + ISNULL(B.March,0) + ISNULL(B.April,0) + ISNULL(B.May,0) May
                                ,ISNULL(A.AUMLastYear,0) + ISNULL(B.January,0) + ISNULL(B.February,0) + ISNULL(B.March,0) + ISNULL(B.April,0) + ISNULL(B.May,0) + ISNULL(B.June,0) June
                                ,ISNULL(A.AUMLastYear,0) + ISNULL(B.January,0) + ISNULL(B.February,0) + ISNULL(B.March,0) + ISNULL(B.April,0) + ISNULL(B.May,0) + ISNULL(B.June,0) + ISNULL(B.July,0) July
                                ,ISNULL(A.AUMLastYear,0) + ISNULL(B.January,0) + ISNULL(B.February,0) + ISNULL(B.March,0) + ISNULL(B.April,0) + ISNULL(B.May,0) + ISNULL(B.June,0) + ISNULL(B.July,0) + ISNULL(B.August,0) August
                                ,ISNULL(A.AUMLastYear,0) + ISNULL(B.January,0) + ISNULL(B.February,0) + ISNULL(B.March,0) + ISNULL(B.April,0) + ISNULL(B.May,0) + ISNULL(B.June,0) + ISNULL(B.July,0) + ISNULL(B.August,0) + ISNULL(B.September,0) September
                                ,ISNULL(A.AUMLastYear,0) + ISNULL(B.January,0) + ISNULL(B.February,0) + ISNULL(B.March,0) + ISNULL(B.April,0) + ISNULL(B.May,0) + ISNULL(B.June,0) + ISNULL(B.July,0) + ISNULL(B.August,0) + ISNULL(B.September,0) + ISNULL(B.October,0) October
                                ,ISNULL(A.AUMLastYear,0) + ISNULL(B.January,0) + ISNULL(B.February,0) + ISNULL(B.March,0) + ISNULL(B.April,0) + ISNULL(B.May,0) + ISNULL(B.June,0) + ISNULL(B.July,0) + ISNULL(B.August,0) + ISNULL(B.September,0) + ISNULL(B.October,0) + ISNULL(B.November,0) November
                                ,ISNULL(A.AUMLastYear,0) + ISNULL(B.January,0) + ISNULL(B.February,0) + ISNULL(B.March,0) + ISNULL(B.April,0) + ISNULL(B.May,0) + ISNULL(B.June,0) + ISNULL(B.July,0) + ISNULL(B.August,0) + ISNULL(B.September,0) + ISNULL(B.October,0) + ISNULL(B.November,0) + ISNULL(B.December,0) December
                                FROM @Result A
                                LEFT JOIN
                                (
	                                SELECT A.InstrumentPK,A.AgentPK,sum(A.January) January, sum(A.February) February, sum(A.March) March, sum(A.April) April,sum(A.May) May, sum(A.June) June, sum(A.July) July, sum(A.August) August, sum(A.September) September, sum(A.October) October, sum(A.November) November, sum(A.December) December FROM  @tableRevenue A
	                                WHERE A.ReportPeriodPK = @PeriodPK AND A.PeriodPK = @PeriodPK
	                                group by A.InstrumentPK,A.AgentPK
                                )B ON A.AgentPK = B.AgentPK AND A.InstrumentPK = B.InstrumentPK
                                left join Agent C on A.AgentPK = C.AgentPK and C.Status = 2
                                left join Instrument D on A.InstrumentPK = D.InstrumentPK and D.Status = 2
                                left join Department E on C.DepartmentPK = E.DepartmentPK and E.Status = 2
                                left join ReksadanaType F on D.ReksadanaTypePK = F.ReksadanaTypePK  and F.status in (1,2)

                                )A
                                ORDER BY A.SalesName,A.InstrumentName
";

                                cmd.CommandTimeout = 0;
                                cmd.Parameters.AddWithValue("@PeriodPK", _BudgetRpt.PeriodPK);


                                using (SqlDataReader dr0 = cmd.ExecuteReader())
                                {
                                    if (!dr0.HasRows)
                                    {
                                        return false;
                                    }
                                    else
                                    {
                                        package.Workbook.Properties.Title = "BudgetReport";
                                        package.Workbook.Properties.Author = Tools.DefaultReportAuthor();
                                        package.Workbook.Properties.Comments = Tools.DefaultReportComments();
                                        package.Workbook.Properties.Company = Tools.DefaultReportCompany();
                                        package.Workbook.Properties.SetCustomPropertyValue("Checked by", _userID);
                                        package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", Tools.DefaultReportAssemblyName());

                                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Budget Report");


                                        //ATUR DATA GROUPINGNYA DULU
                                        List<LaporanMIFeeCabangdanSales> rList = new List<LaporanMIFeeCabangdanSales>();
                                        while (dr0.Read())
                                        {
                                            LaporanMIFeeCabangdanSales rSingle = new LaporanMIFeeCabangdanSales();
                                            rSingle.SalesName = Convert.ToString(dr0["SalesName"]);
                                            rSingle.BranchName = Convert.ToString(dr0["BranchName"]);
                                            rSingle.Category = Convert.ToString(dr0["Category"]);
                                            rSingle.InstrumentName = Convert.ToString(dr0["InstrumentName"]);
                                            rSingle.FeeMIRate = Convert.ToDecimal(dr0["MGTFeePercent"]);
                                            rSingle.YTDSep = Convert.ToDecimal(dr0["MFeeAmountYTDSept"]);
                                            rSingle.YTDDes = Convert.ToDecimal(dr0["MFeeAmountYTDDec"]);
                                            rSingle.January1 = Convert.ToDecimal(dr0["January"]);
                                            rSingle.February1 = Convert.ToDecimal(dr0["February"]);
                                            rSingle.March1 = Convert.ToDecimal(dr0["March"]);
                                            rSingle.April1 = Convert.ToDecimal(dr0["April"]);
                                            rSingle.May1 = Convert.ToDecimal(dr0["May"]);
                                            rSingle.June1 = Convert.ToDecimal(dr0["June"]);
                                            rSingle.July1 = Convert.ToDecimal(dr0["July"]);
                                            rSingle.August1 = Convert.ToDecimal(dr0["August"]);
                                            rSingle.September1 = Convert.ToDecimal(dr0["September"]);
                                            rSingle.October1 = Convert.ToDecimal(dr0["October"]);
                                            rSingle.November1 = Convert.ToDecimal(dr0["November"]);
                                            rSingle.December1 = Convert.ToDecimal(dr0["December"]);
                                            rList.Add(rSingle);

                                        }

                                        var QueryByClientID =
                                         from r in rList
                                         orderby r.BranchName, r.SalesName
                                         group r by new { r.BranchName, r.SalesName } into rGroup
                                         select rGroup;

                                        int incRowExcel = 1;
                                        worksheet.Cells[incRowExcel, 1].Value = "PT MNC ASSET MANAGEMENT";
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Size = 14;
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "LAPORAN AUM CABANG DAN SALES";
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Size = 14;
                                        incRowExcel++;
                                        incRowExcel++;
                                        incRowExcel++;

                                        foreach (var rsHeader in QueryByClientID)
                                        {

                                            int _start = incRowExcel;
                                            int _end = incRowExcel + 4;
                                            worksheet.Cells["A" + _start + ":C" + _end].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + _start + ":C" + _end].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + _start + ":C" + _end].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + _start + ":C" + _end].Style.Border.Right.Style = ExcelBorderStyle.Medium;

                                            worksheet.Cells[incRowExcel, 1].Value = "Branch Name : ";
                                            worksheet.Cells[incRowExcel, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                            worksheet.Cells[incRowExcel, 1].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                                            worksheet.Cells[incRowExcel, 3].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                            worksheet.Cells[incRowExcel, 3].Style.Fill.BackgroundColor.SetColor(Color.LightSalmon);
                                            worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells["A" + incRowExcel + ":B" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 3].Value = rsHeader.Key.BranchName;
                                            worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            incRowExcel++;

                                            worksheet.Cells[incRowExcel, 1].Value = "Agent Name :";
                                            worksheet.Cells[incRowExcel, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                            worksheet.Cells[incRowExcel, 1].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                                            worksheet.Cells[incRowExcel, 3].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                            worksheet.Cells[incRowExcel, 3].Style.Fill.BackgroundColor.SetColor(Color.LightSalmon);
                                            worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                            worksheet.Cells["A" + incRowExcel + ":B" + incRowExcel].Merge = true;
                                            worksheet.Cells[incRowExcel, 3].Value = rsHeader.Key.SalesName;
                                            worksheet.Cells[incRowExcel, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                                            incRowExcel++;
                                            worksheet.Cells["A" + incRowExcel + ":S" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + incRowExcel + ":S" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + incRowExcel + ":S" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + incRowExcel + ":S" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Medium;


                                            worksheet.Cells[incRowExcel, 1].Value = "No";
                                            worksheet.Cells[incRowExcel, 2].Value = "Category";
                                            worksheet.Cells[incRowExcel, 3].Value = "Product";
                                            worksheet.Cells[incRowExcel, 4].Value = "Fee MI Rate";
                                            worksheet.Cells[incRowExcel, 5].Value = "YTD SEP " + _host.Get_PeriodByRevenue(_BudgetRpt.PeriodPK, 1);
                                            worksheet.Cells[incRowExcel, 6].Value = "YTD Des " + _host.Get_PeriodByRevenue(_BudgetRpt.PeriodPK, 1);
                                            worksheet.Cells[incRowExcel, 7].Value = "JAN ";
                                            worksheet.Cells[incRowExcel, 8].Value = "FEB ";
                                            worksheet.Cells[incRowExcel, 9].Value = "MAR ";
                                            worksheet.Cells[incRowExcel, 10].Value = "APR ";
                                            worksheet.Cells[incRowExcel, 11].Value = "MAY ";
                                            worksheet.Cells[incRowExcel, 12].Value = "JUN ";
                                            worksheet.Cells[incRowExcel, 13].Value = "JUL ";
                                            worksheet.Cells[incRowExcel, 14].Value = "AUG ";
                                            worksheet.Cells[incRowExcel, 15].Value = "SEP ";
                                            worksheet.Cells[incRowExcel, 16].Value = "OCT ";
                                            worksheet.Cells[incRowExcel, 17].Value = "NOV ";
                                            worksheet.Cells[incRowExcel, 18].Value = "DEC ";

                                            worksheet.Cells[incRowExcel, 19].Value = "TOTAL";

                                            worksheet.Cells["A" + incRowExcel + ":S" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet.Cells["A" + incRowExcel + ":S" + incRowExcel].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                            worksheet.Cells["A" + incRowExcel + ":S" + incRowExcel].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                                            worksheet.Cells["A" + incRowExcel + ":S" + incRowExcel].Style.Font.Bold = true;
                                            incRowExcel++;
                                            int _startRowDetail = incRowExcel;
                                            int _endRowDetail = 0;
                                            int _no = 1;
                                            foreach (var rsDetail in rsHeader)
                                            {


                                                //ThickBox Border

                                                worksheet.Cells[incRowExcel, 1].Value = _no;
                                                worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 2].Value = rsDetail.Category;
                                                worksheet.Cells[incRowExcel, 2].Style.WrapText = true;
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.InstrumentName;
                                                worksheet.Cells[incRowExcel, 3].Style.WrapText = true;
                                                worksheet.Cells[incRowExcel, 4].Value = rsDetail.FeeMIRate;
                                                worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 5].Value = rsDetail.YTDSep;
                                                worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 6].Value = rsDetail.YTDDes;
                                                worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0";


                                                worksheet.Cells[incRowExcel, 7].Value = rsDetail.January1;
                                                worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 8].Value = rsDetail.February1;
                                                worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 9].Value = rsDetail.March1;
                                                worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 10].Value = rsDetail.April1;
                                                worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 11].Value = rsDetail.May1;
                                                worksheet.Cells[incRowExcel, 11].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 12].Value = rsDetail.June1;
                                                worksheet.Cells[incRowExcel, 12].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 13].Value = rsDetail.July1;
                                                worksheet.Cells[incRowExcel, 13].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 14].Value = rsDetail.August1;
                                                worksheet.Cells[incRowExcel, 14].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 15].Value = rsDetail.September1;
                                                worksheet.Cells[incRowExcel, 15].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 16].Value = rsDetail.October1;
                                                worksheet.Cells[incRowExcel, 16].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 17].Value = rsDetail.November1;
                                                worksheet.Cells[incRowExcel, 17].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 18].Value = rsDetail.December1;
                                                worksheet.Cells[incRowExcel, 18].Style.Numberformat.Format = "#,##0";

                                                worksheet.Cells[incRowExcel, 19].Formula = "SUM(G" + incRowExcel + ":R" + incRowExcel + ")";
                                                worksheet.Cells[incRowExcel, 19].Calculate();
                                                worksheet.Cells[incRowExcel, 19].Style.Numberformat.Format = "#,##0";


                                                worksheet.Cells["A" + incRowExcel + ":S" + incRowExcel].Style.VerticalAlignment = ExcelVerticalAlignment.Top;


                                                _no++;
                                                _endRowDetail = incRowExcel;
                                                incRowExcel++;



                                            }

                                            worksheet.Cells["A" + _startRowDetail + ":S" + _endRowDetail].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + _startRowDetail + ":S" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + _startRowDetail + ":S" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + _startRowDetail + ":S" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Medium;


                                            worksheet.Cells[incRowExcel, 4].Value = "TOTAL :";
                                            worksheet.Cells[incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                                            worksheet.Cells[incRowExcel, 5].Formula = "SUM(E" + _startRowDetail + ":E" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 5].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 5].Calculate();
                                            worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0";

                                            worksheet.Cells[incRowExcel, 6].Formula = "SUM(F" + _startRowDetail + ":F" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 6].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 6].Calculate();
                                            worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0";

                                            worksheet.Cells[incRowExcel, 7].Formula = "SUM(G" + _startRowDetail + ":G" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 7].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 7].Calculate();
                                            worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0";

                                            worksheet.Cells[incRowExcel, 8].Formula = "SUM(H" + _startRowDetail + ":H" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 8].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 8].Calculate();
                                            worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0";

                                            worksheet.Cells[incRowExcel, 9].Formula = "SUM(I" + _startRowDetail + ":I" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 9].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 9].Calculate();
                                            worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0";

                                            worksheet.Cells[incRowExcel, 10].Formula = "SUM(J" + _startRowDetail + ":J" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 10].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 10].Calculate();
                                            worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0";

                                            worksheet.Cells[incRowExcel, 11].Formula = "SUM(K" + _startRowDetail + ":K" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 11].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 11].Calculate();
                                            worksheet.Cells[incRowExcel, 11].Style.Numberformat.Format = "#,##0";

                                            worksheet.Cells[incRowExcel, 12].Formula = "SUM(L" + _startRowDetail + ":L" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 12].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 12].Calculate();
                                            worksheet.Cells[incRowExcel, 12].Style.Numberformat.Format = "#,##0";

                                            worksheet.Cells[incRowExcel, 13].Formula = "SUM(M" + _startRowDetail + ":M" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 13].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 13].Calculate();
                                            worksheet.Cells[incRowExcel, 13].Style.Numberformat.Format = "#,##0";

                                            worksheet.Cells[incRowExcel, 14].Formula = "SUM(N" + _startRowDetail + ":N" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 14].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 14].Calculate();
                                            worksheet.Cells[incRowExcel, 14].Style.Numberformat.Format = "#,##0";

                                            worksheet.Cells[incRowExcel, 15].Formula = "SUM(O" + _startRowDetail + ":O" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 15].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 15].Calculate();
                                            worksheet.Cells[incRowExcel, 15].Style.Numberformat.Format = "#,##0";

                                            worksheet.Cells[incRowExcel, 16].Formula = "SUM(P" + _startRowDetail + ":P" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 16].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 16].Calculate();
                                            worksheet.Cells[incRowExcel, 16].Style.Numberformat.Format = "#,##0";

                                            worksheet.Cells[incRowExcel, 17].Formula = "SUM(Q" + _startRowDetail + ":Q" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 17].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 17].Calculate();
                                            worksheet.Cells[incRowExcel, 17].Style.Numberformat.Format = "#,##0";

                                            worksheet.Cells[incRowExcel, 18].Formula = "SUM(R" + _startRowDetail + ":R" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 18].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 18].Calculate();
                                            worksheet.Cells[incRowExcel, 18].Style.Numberformat.Format = "#,##0";

                                            worksheet.Cells[incRowExcel, 19].Formula = "SUM(S" + _startRowDetail + ":S" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 19].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 19].Calculate();
                                            worksheet.Cells[incRowExcel, 19].Style.Numberformat.Format = "#,##0";


                                            worksheet.Cells["D" + incRowExcel + ":S" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["D" + incRowExcel + ":S" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["D" + incRowExcel + ":S" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Medium;

                                            incRowExcel = incRowExcel + 3;
                                        }



                                        worksheet.PrinterSettings.FitToPage = true;
                                        worksheet.PrinterSettings.FitToWidth = 1;
                                        worksheet.PrinterSettings.FitToHeight = 0;
                                        worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel - 1, 19];
                                        worksheet.Column(1).Width = 8;
                                        worksheet.Column(2).Width = 30;
                                        worksheet.Column(3).Width = 40;
                                        worksheet.Column(4).Width = 15;
                                        worksheet.Column(5).Width = 15;
                                        worksheet.Column(6).Width = 15;
                                        worksheet.Column(7).Width = 15;
                                        worksheet.Column(8).Width = 15;
                                        worksheet.Column(9).Width = 17;
                                        worksheet.Column(10).Width = 17;
                                        worksheet.Column(11).Width = 17;
                                        worksheet.Column(12).Width = 17;
                                        worksheet.Column(13).Width = 17;
                                        worksheet.Column(14).Width = 17;
                                        worksheet.Column(15).Width = 17;
                                        worksheet.Column(16).Width = 17;
                                        worksheet.Column(17).Width = 17;
                                        worksheet.Column(18).Width = 17;
                                        worksheet.Column(19).Width = 17;



                                        // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                        //worksheet.Cells["A3:O14"].AutoFitColumns();  // UNTUK AUTO FIT UKURAN TULISAN DENGAN KOLOM, KEBETULAN INI BUAT HEADER AJA DIPAKENYA

                                        worksheet.PrinterSettings.Orientation = eOrientation.Landscape; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                        worksheet.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN
                                        worksheet.HeaderFooter.OddHeader.RightAlignedText = "&14 LAPORAN MI FEE CABANG DAN SALES";

                                        // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                        worksheet.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
                                        worksheet.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
                                        worksheet.View.PageLayoutView = Tools.DefaultReportPageLayoutView();
                                        worksheet.View.ShowGridLines = Tools.DefaultReportShowGridLines();

                                        Image img = Image.FromFile(Tools.ReportImage);
                                        worksheet.HeaderFooter.OddHeader.InsertPicture(img, PictureAlignment.Left);
                                        //worksheet.HeaderFooter.OddHeader.CenteredText = Tools.DefaultReportHeaderCenterText();
                                        //worksheet.HeaderFooter.OddHeader.LeftAlignedText = Tools.DefaultReportHeaderLeftText();

                                        worksheet.HeaderFooter.OddFooter.RightAlignedText =
                                        string.Format("Page {0} of {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);

                                        ////worksheet.HeaderFooter.OddFooter.CenteredText = Tools.DefaultReportFooterCenterText();
                                        worksheet.HeaderFooter.OddFooter.LeftAlignedText = Tools.DefaultReportFooterLeftText();


                                    }
                                }

                            }
                        }

                        //sheet 2
                        ExcelWorksheet worksheet1 = package.Workbook.Worksheets.Add("Summary");

                        using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                        {
                            DbCon.Open();
                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {

                                cmd.CommandText =
                                @"
                                DECLARE @PeriodPKFrom INT
                                Declare @PeriodID nvarchar(50)

                                select @PeriodID  = YEAR(dateadd(year, -1, ID)) From Period where PeriodPK = @PeriodPK and status in (1,2)
                                select @PeriodPKFrom = PeriodPK from Period where ID = @PeriodID and status in (1,2)

                                DECLARE @Result TABLE
                                (
	                                AgentPK INT,
	                                InstrumentPK INT,
	                                MGTFee NUMERIC(18,8),
	                                BegLastYear NUMERIC(22,4),
	                                YTDLastYear NUMERIC(22,4),
	                                AUMLastYear numeric(22,4)
                                )


                                declare @tableRevenue table
                                (
	                                PeriodPK int,
	                                ReportPeriodPK int,
	                                InstrumentPK int,
	                                AgentPK int,
	                                MGTFee numeric(10,4),
	                                January NUMERIC(22,4),
	                                February NUMERIC(22,4),
	                                March NUMERIC(22,4),
	                                April NUMERIC(22,4),
	                                May NUMERIC(22,4),
	                                June NUMERIC(22,4),
	                                July NUMERIC(22,4),
	                                August NUMERIC(22,4),
	                                September NUMERIC(22,4),
	                                October NUMERIC(22,4),
	                                November NUMERIC(22,4),
	                                December NUMERIC(22,4)
                                )

                                DECLARE @AUMFinalPerPeriod TABLE
                                (
	                                Date DATETIME,
	                                InstrumentPK INT,
	                                AgentPK int,
	                                AUM NUMERIC(22,4),
	                                Mfee numeric(22,4)
                                )
                                INSERT INTO @AUMFinalPerPeriod
                                        ( Date,InstrumentPK, AgentPK, AUM, Mfee )
                                SELECT MAX(A.date) Date,A.InstrumentPK,A.AgentPK,sum(A.AUM), sum(A.MFeeAmount)
                                FROM dbo.AumForBudgetBegBalance A
                                WHERE A.ReportPeriodPK = @PeriodPK AND A.status = 2 and periodpk = @PeriodPKFrom
                                GROUP BY A.InstrumentPK,A.AgentPK

                                insert into @tableRevenue
                                select A.PeriodPK,A.ReportPeriodPK,A.InstrumentPK,AgentPK,MGTFee,
							    January*case when B.ReksadanaTypePK in (5,6,7) then 1 else isnull(C.RevenuePercent/100,1) end,February*case when B.ReksadanaTypePK in (5,6,7) then 1 else isnull(C.RevenuePercent/100,1) end,
							    March*case when B.ReksadanaTypePK in (5,6,7) then 1 else isnull(C.RevenuePercent/100,1) end,April*case when B.ReksadanaTypePK in (5,6,7) then 1 else isnull(C.RevenuePercent/100,1) end,
							    May*case when B.ReksadanaTypePK in (5,6,7) then 1 else isnull(C.RevenuePercent/100,1) end,June*case when B.ReksadanaTypePK in (5,6,7) then 1 else isnull(C.RevenuePercent/100,1) end,
							    July*case when B.ReksadanaTypePK in (5,6,7) then 1 else isnull(C.RevenuePercent/100,1) end,August*case when B.ReksadanaTypePK in (5,6,7) then 1 else isnull(C.RevenuePercent/100,1) end,
							    September*case when B.ReksadanaTypePK in (5,6,7) then 1 else isnull(C.RevenuePercent/100,1) end,October*case when B.ReksadanaTypePK in (5,6,7) then 1 else isnull(C.RevenuePercent/100,1) end,
							    November*case when B.ReksadanaTypePK in (5,6,7) then 1 else isnull(C.RevenuePercent/100,1) end,December*case when B.ReksadanaTypePK in (5,6,7) then 1 else isnull(C.RevenuePercent/100,1) end 
                                from Revenue A
							    left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status in (1,2)
								left join BudgetVersioning C on A.PeriodPK = C.PeriodPK and C.ReportPeriodPK = C.ReportPeriodPK and C.Status = 2 and Date = (
	                                select max(date) from BudgetVersioning where status in (1,2) and ReportPeriodPK = @periodpk
                                )
							    where A.status = 2 and A.ReportPeriodPK = @periodpk

                                INSERT INTO @Result
                                        ( AgentPK ,
                                          InstrumentPK ,
                                          MGTFee ,
                                          BegLastYear ,
                                          YTDLastYear,
		                                  AUMLastYear
                                        )
                                SELECT A.AgentPK,A.InstrumentPK,ISNULL(B.MGTFee,0) MGTFee
                                ,ISNULL(A.Mfee,0) BegLastYear
                                --handle mfee YTDLastyear
                                ,CASE WHEN MONTH(A.Date) = 8 THEN ISNULL(A.Mfee,0) + 
	                                ((isnull(A.AUM,0) + ISNULL(B.September,0) + isnull(A.AUM,0) ) /2 / 12 * B.MGTFee /100 ) +  -- september
	                                ((isnull(A.AUM,0) + ISNULL(B.September,0) + isnull(A.AUM,0) + ISNULL(B.September,0) + ISNULL(B.October,0) ) /2 / 12 * B.MGTFee / 100 )+ -- Oktober
	                                ((isnull(A.AUM,0) + ISNULL(B.September,0) + ISNULL(B.October,0) + isnull(A.AUM,0) + ISNULL(B.September,0) + ISNULL(B.October,0) + ISNULL(B.November,0) ) /2 / 12 * B.MGTFee / 100 )+ -- November
	                                ((isnull(A.AUM,0) + ISNULL(B.September,0) + ISNULL(B.October,0) + ISNULL(B.November,0) + isnull(A.AUM,0) + ISNULL(B.September,0) + ISNULL(B.October,0) + ISNULL(B.November,0) + ISNULL(B.December,0) ) /2 / 12 * B.MGTFee / 100 ) -- Desember

	                                WHEN MONTH(A.Date) = 9 THEN ISNULL(A.Mfee,0)  +
		                                ((isnull(A.AUM,0) + isnull(A.AUM,0) + ISNULL(B.October,0) ) /2 / 12 * B.MGTFee / 100 )+ -- Oktober
		                                ((isnull(A.AUM,0) + ISNULL(B.October,0) + isnull(A.AUM,0) + ISNULL(B.October,0) + ISNULL(B.November,0) ) /2 / 12 * B.MGTFee / 100 )+ -- November
		                                ((isnull(A.AUM,0) + ISNULL(B.October,0) + ISNULL(B.November,0) + isnull(A.AUM,0) + ISNULL(B.October,0) + ISNULL(B.November,0) + ISNULL(B.December,0) ) /2 / 12 * B.MGTFee / 100 ) -- Desember

		                                WHEN MONTH(A.Date) = 10 THEN ISNULL(A.Mfee,0)  + 
			                                ((isnull(A.AUM,0) + isnull(A.AUM,0) + ISNULL(B.November,0) ) /2 / 12 * B.MGTFee / 100 )+ -- November
			                                ((isnull(A.AUM,0) + ISNULL(B.November,0) + isnull(A.AUM,0) + ISNULL(B.November,0) + ISNULL(B.December,0) ) /2 / 12 * B.MGTFee / 100 ) -- Desember

			                                WHEN MONTH(A.Date) = 11 THEN ISNULL(A.Mfee,0)  +
				                                ((isnull(A.AUM,0) + isnull(A.AUM,0) + ISNULL(B.December,0) ) /2 / 12 * B.MGTFee / 100 ) -- Desember
			                                 END YTDLastYear,
                                --AUM LAST YEAR
                                CASE WHEN MONTH(A.Date) = 8 THEN ISNULL(A.AUM,0) + ISNULL(B.September,0)  + ISNULL(B.October,0) + ISNULL(B.November,0) + ISNULL(B.December,0)
		                                WHEN MONTH(A.Date) = 9 THEN ISNULL(A.AUM,0)  + ISNULL(B.October,0) + ISNULL(B.November,0) + ISNULL(B.December,0)
			                                WHEN MONTH(A.Date) = 10 THEN ISNULL(A.AUM,0)  + ISNULL(B.November,0) + ISNULL(B.December,0)
				                                WHEN MONTH(A.Date) = 11 THEN ISNULL(A.AUM,0)  + ISNULL(B.November,0) + ISNULL(B.December,0) END AUMLastYear
                                FROM @AUMFinalPerPeriod A
                                LEFT JOIN
                                (
	                                SELECT A.InstrumentPK,A.AgentPK,A.MGTFee,sum(A.January) January, sum(A.February) February, sum(A.March) March, sum(A.April) April,sum(A.May) May, sum(A.June) June, sum(A.July) July, sum(A.August) August, sum(A.September) September, sum(A.October) October, sum(A.November) November, sum(A.December) December FROM @tableRevenue A
	                                WHERE A.ReportPeriodPK = @PeriodPK AND A.PeriodPK <> @PeriodPK
	                                group by A.InstrumentPK, A.MGTFee,A.AgentPK
                                )B ON A.InstrumentPK = B.InstrumentPK AND A.AgentPK = B.AgentPK





                                select isnull(A.BranchName,'') Category,sum(AUMSeptember) MFeeAmountYTDSept,sum(MFeeLastYear) MFeeAmountYTDDec,
                                sum((AUMLastYear + January)/2/12/ 100 * MGTFee) January,sum((January + February)/2/12/ 100 * MGTFee) February,sum((February + March)/2/12/ 100 * MGTFee) March, sum((March + April)/2/12/ 100 * MGTFee) April,
                                sum((April + May)/2/12/ 100 * MGTFee) May,sum((May + June)/2/12/ 100 * MGTFee) june,sum((June + July)/2/12/ 100 * MGTFee) July,sum((July + August)/2/12/ 100 * MGTFee) August,sum((August + September)/2/12/ 100 * MGTFee) September,
                                sum((September + October)/2/12/ 100 * MGTFee) October,sum((October + November)/2/12/ 100 * MGTFee) November,sum((November + December)/2/12/ 100 * MGTFee) December
                                 from (
                                SELECT C.Name SalesName, D.Name InstrumentName,isnull(F.Description,'') Category,isnull(E.Name,'') BranchName,A.MGTFee,A.BeglastYear AUMSeptember,ISNULL(A.YTDLastYear,0)  MFeeLastYear, A.AUMLastYear
                                ,ISNULL(A.AUMLastYear,0) + ISNULL(B.January,0) January
                                ,ISNULL(A.AUMLastYear,0) + ISNULL(B.January,0) + ISNULL(B.February,0) February
                                ,ISNULL(A.AUMLastYear,0) + ISNULL(B.January,0) + ISNULL(B.February,0) + ISNULL(B.March,0) March
                                ,ISNULL(A.AUMLastYear,0) + ISNULL(B.January,0) + ISNULL(B.February,0) + ISNULL(B.March,0) + ISNULL(B.April,0) April
                                ,ISNULL(A.AUMLastYear,0) + ISNULL(B.January,0) + ISNULL(B.February,0) + ISNULL(B.March,0) + ISNULL(B.April,0) + ISNULL(B.May,0) May
                                ,ISNULL(A.AUMLastYear,0) + ISNULL(B.January,0) + ISNULL(B.February,0) + ISNULL(B.March,0) + ISNULL(B.April,0) + ISNULL(B.May,0) + ISNULL(B.June,0) June
                                ,ISNULL(A.AUMLastYear,0) + ISNULL(B.January,0) + ISNULL(B.February,0) + ISNULL(B.March,0) + ISNULL(B.April,0) + ISNULL(B.May,0) + ISNULL(B.June,0) + ISNULL(B.July,0) July
                                ,ISNULL(A.AUMLastYear,0) + ISNULL(B.January,0) + ISNULL(B.February,0) + ISNULL(B.March,0) + ISNULL(B.April,0) + ISNULL(B.May,0) + ISNULL(B.June,0) + ISNULL(B.July,0) + ISNULL(B.August,0) August
                                ,ISNULL(A.AUMLastYear,0) + ISNULL(B.January,0) + ISNULL(B.February,0) + ISNULL(B.March,0) + ISNULL(B.April,0) + ISNULL(B.May,0) + ISNULL(B.June,0) + ISNULL(B.July,0) + ISNULL(B.August,0) + ISNULL(B.September,0) September
                                ,ISNULL(A.AUMLastYear,0) + ISNULL(B.January,0) + ISNULL(B.February,0) + ISNULL(B.March,0) + ISNULL(B.April,0) + ISNULL(B.May,0) + ISNULL(B.June,0) + ISNULL(B.July,0) + ISNULL(B.August,0) + ISNULL(B.September,0) + ISNULL(B.October,0) October
                                ,ISNULL(A.AUMLastYear,0) + ISNULL(B.January,0) + ISNULL(B.February,0) + ISNULL(B.March,0) + ISNULL(B.April,0) + ISNULL(B.May,0) + ISNULL(B.June,0) + ISNULL(B.July,0) + ISNULL(B.August,0) + ISNULL(B.September,0) + ISNULL(B.October,0) + ISNULL(B.November,0) November
                                ,ISNULL(A.AUMLastYear,0) + ISNULL(B.January,0) + ISNULL(B.February,0) + ISNULL(B.March,0) + ISNULL(B.April,0) + ISNULL(B.May,0) + ISNULL(B.June,0) + ISNULL(B.July,0) + ISNULL(B.August,0) + ISNULL(B.September,0) + ISNULL(B.October,0) + ISNULL(B.November,0) + ISNULL(B.December,0) December
                                FROM @Result A
                                LEFT JOIN
                                (
	                                SELECT A.InstrumentPK,A.AgentPK,sum(A.January) January, sum(A.February) February, sum(A.March) March, sum(A.April) April,sum(A.May) May, sum(A.June) June, sum(A.July) July, sum(A.August) August, sum(A.September) September, sum(A.October) October, sum(A.November) November, sum(A.December) December FROM  @tableRevenue A
	                                WHERE A.ReportPeriodPK = @PeriodPK AND A.PeriodPK = @PeriodPK
	                                group by A.InstrumentPK,A.AgentPK
                                )B ON A.AgentPK = B.AgentPK AND A.InstrumentPK = B.InstrumentPK
                                left join Agent C on A.AgentPK = C.AgentPK and C.Status = 2
                                left join Instrument D on A.InstrumentPK = D.InstrumentPK and D.Status = 2
                                left join Department E on C.DepartmentPK = E.DepartmentPK and E.Status = 2
                                left join ReksadanaType F on D.ReksadanaTypePK = F.ReksadanaTypePK  and F.status in (1,2)

                                )A
								group by A.BranchName
								order by A.BranchName
    ";

                                cmd.CommandTimeout = 0;
                                cmd.Parameters.AddWithValue("@PeriodPK", _BudgetRpt.PeriodPK);


                                using (SqlDataReader dr0 = cmd.ExecuteReader())
                                {
                                    if (!dr0.HasRows)
                                    {
                                        return false;
                                    }
                                    else
                                    {

                                        //ATUR DATA GROUPINGNYA DULU
                                        List<BudgetRpt> rList = new List<BudgetRpt>();
                                        while (dr0.Read())
                                        {
                                            BudgetRpt rSingle = new BudgetRpt();
                                            rSingle.Category = Convert.ToString(dr0["Category"]);
                                            rSingle.AUMSeptember = Convert.ToDecimal(dr0["MFeeAmountYTDSept"]);
                                            rSingle.BeginningAUMDes = Convert.ToDecimal(dr0["MFeeAmountYTDDec"]);
                                            rSingle.January1 = Convert.ToDecimal(dr0["January"]);
                                            rSingle.February1 = Convert.ToDecimal(dr0["February"]);
                                            rSingle.March1 = Convert.ToDecimal(dr0["March"]);
                                            rSingle.April1 = Convert.ToDecimal(dr0["April"]);
                                            rSingle.May1 = Convert.ToDecimal(dr0["May"]);
                                            rSingle.June1 = Convert.ToDecimal(dr0["June"]);
                                            rSingle.July1 = Convert.ToDecimal(dr0["July"]);
                                            rSingle.August1 = Convert.ToDecimal(dr0["August"]);
                                            rSingle.September1 = Convert.ToDecimal(dr0["September"]);
                                            rSingle.October1 = Convert.ToDecimal(dr0["October"]);
                                            rSingle.November1 = Convert.ToDecimal(dr0["November"]);
                                            rSingle.December1 = Convert.ToDecimal(dr0["December"]);
                                            rList.Add(rSingle);

                                        }

                                        var QueryByClientID =
                                         from r in rList
                                         orderby r.Category
                                         group r by new { } into rGroup
                                         select rGroup;

                                        int incRowExcel = 1;

                                        worksheet1.Cells[incRowExcel, 1].Value = "PT MNC ASSET MANAGEMENT";
                                        worksheet1.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                        worksheet1.Cells[incRowExcel, 1].Style.Font.Size = 14;
                                        incRowExcel++;
                                        worksheet1.Cells[incRowExcel, 1].Value = "LAPORAN MIFEE CABANG DAN SALES";
                                        worksheet1.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                        worksheet1.Cells[incRowExcel, 1].Style.Font.Size = 14;
                                        incRowExcel++;
                                        incRowExcel++;
                                        incRowExcel++;

                                        foreach (var rsHeader in QueryByClientID)
                                        {
                                            var _nextRow = incRowExcel + 1;

                                            worksheet1.Cells["A" + incRowExcel + ":Q" + _nextRow].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet1.Cells["A" + incRowExcel + ":Q" + _nextRow].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                            worksheet1.Cells["A" + incRowExcel + ":Q" + _nextRow].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet1.Cells["A" + incRowExcel + ":Q" + _nextRow].Style.Border.Right.Style = ExcelBorderStyle.Medium;

                                            worksheet1.Cells["A" + incRowExcel + ":Q" + _nextRow].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet1.Cells["A" + incRowExcel + ":Q" + _nextRow].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                            worksheet1.Cells["A" + incRowExcel + ":Q" + _nextRow].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                                            worksheet1.Cells["A" + incRowExcel + ":Q" + _nextRow].Style.Font.Bold = true;

                                            worksheet1.Cells[incRowExcel, 1].Value = "No";
                                            worksheet1.Cells["A" + incRowExcel + ":A" + _nextRow].Merge = true;
                                            worksheet1.Cells[incRowExcel, 2].Value = "Category";
                                            worksheet1.Cells["B" + incRowExcel + ":B" + _nextRow].Merge = true;
                                            worksheet1.Cells[incRowExcel, 3].Value = "Actual";
                                            worksheet1.Cells[incRowExcel, 4].Value = "Estimated";
                                            worksheet1.Cells[incRowExcel, 5].Value = "Budget";
                                            worksheet1.Cells["E" + incRowExcel + ":Q" + incRowExcel].Merge = true;
                                            incRowExcel++;

                                            worksheet1.Cells[incRowExcel, 3].Value = "YTD SEP " + _host.Get_PeriodByRevenue(_BudgetRpt.PeriodPK, 1);
                                            worksheet1.Cells[incRowExcel, 4].Value = "OKT-DES " + _host.Get_PeriodByRevenue(_BudgetRpt.PeriodPK, 1);

                                            worksheet1.Cells[incRowExcel, 5].Value = "JAN ";
                                            worksheet1.Cells[incRowExcel, 6].Value = "FEB ";
                                            worksheet1.Cells[incRowExcel, 7].Value = "MAR ";
                                            worksheet1.Cells[incRowExcel, 8].Value = "APR ";
                                            worksheet1.Cells[incRowExcel, 9].Value = "MAY ";
                                            worksheet1.Cells[incRowExcel, 10].Value = "JUN ";
                                            worksheet1.Cells[incRowExcel, 11].Value = "JUL ";
                                            worksheet1.Cells[incRowExcel, 12].Value = "AUG ";
                                            worksheet1.Cells[incRowExcel, 13].Value = "SEP ";
                                            worksheet1.Cells[incRowExcel, 14].Value = "OCT ";
                                            worksheet1.Cells[incRowExcel, 15].Value = "NOV ";
                                            worksheet1.Cells[incRowExcel, 16].Value = "DEC ";
                                            worksheet1.Cells[incRowExcel, 17].Value = "TOTAL";

                                            worksheet1.Cells["A" + incRowExcel + ":Q" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                            worksheet1.Cells["A" + incRowExcel + ":Q" + incRowExcel].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                            worksheet1.Cells["A" + incRowExcel + ":Q" + incRowExcel].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                                            worksheet1.Cells["A" + incRowExcel + ":Q" + incRowExcel].Style.Font.Bold = true;
                                            incRowExcel++;
                                            int _startRowDetail = incRowExcel;
                                            int _endRowDetail = 0;
                                            int _no = 1;
                                            foreach (var rsDetail in rsHeader)
                                            {


                                                //ThickBox Border

                                                worksheet1.Cells[incRowExcel, 1].Value = _no;
                                                worksheet1.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet1.Cells[incRowExcel, 2].Value = rsDetail.Category;
                                                worksheet1.Cells[incRowExcel, 2].Style.WrapText = true;
                                                worksheet1.Cells[incRowExcel, 3].Value = rsDetail.AUMSeptember;
                                                worksheet1.Cells[incRowExcel, 3].Style.Numberformat.Format = "#,##0";
                                                worksheet1.Cells[incRowExcel, 4].Value = rsDetail.BeginningAUMDes;
                                                worksheet1.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##0";

                                                worksheet1.Cells[incRowExcel, 5].Value = rsDetail.January1;
                                                worksheet1.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0";
                                                worksheet1.Cells[incRowExcel, 6].Value = rsDetail.February1;
                                                worksheet1.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0";
                                                worksheet1.Cells[incRowExcel, 7].Value = rsDetail.March1;
                                                worksheet1.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0";
                                                worksheet1.Cells[incRowExcel, 8].Value = rsDetail.April1;
                                                worksheet1.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0";
                                                worksheet1.Cells[incRowExcel, 9].Value = rsDetail.May1;
                                                worksheet1.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0";
                                                worksheet1.Cells[incRowExcel, 10].Value = rsDetail.June1;
                                                worksheet1.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0";
                                                worksheet1.Cells[incRowExcel, 11].Value = rsDetail.July1;
                                                worksheet1.Cells[incRowExcel, 11].Style.Numberformat.Format = "#,##0";
                                                worksheet1.Cells[incRowExcel, 12].Value = rsDetail.August1;
                                                worksheet1.Cells[incRowExcel, 12].Style.Numberformat.Format = "#,##0";
                                                worksheet1.Cells[incRowExcel, 13].Value = rsDetail.September1;
                                                worksheet1.Cells[incRowExcel, 13].Style.Numberformat.Format = "#,##0";
                                                worksheet1.Cells[incRowExcel, 14].Value = rsDetail.October1;
                                                worksheet1.Cells[incRowExcel, 14].Style.Numberformat.Format = "#,##0";
                                                worksheet1.Cells[incRowExcel, 15].Value = rsDetail.November1;
                                                worksheet1.Cells[incRowExcel, 15].Style.Numberformat.Format = "#,##0";
                                                worksheet1.Cells[incRowExcel, 16].Value = rsDetail.December1;
                                                worksheet1.Cells[incRowExcel, 16].Style.Numberformat.Format = "#,##0";

                                                worksheet1.Cells[incRowExcel, 17].Formula = "SUM(P" + incRowExcel + ":P" + incRowExcel + ")";
                                                worksheet1.Cells[incRowExcel, 17].Calculate();
                                                worksheet1.Cells[incRowExcel, 17].Style.Numberformat.Format = "#,##0";

                                                worksheet1.Cells["A" + incRowExcel + ":Q" + incRowExcel].Style.VerticalAlignment = ExcelVerticalAlignment.Top;

                                                _no++;
                                                _endRowDetail = incRowExcel;
                                                incRowExcel++;



                                            }

                                            worksheet1.Cells["A" + _startRowDetail + ":Q" + _endRowDetail].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet1.Cells["A" + _startRowDetail + ":Q" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                            worksheet1.Cells["A" + _startRowDetail + ":Q" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet1.Cells["A" + _startRowDetail + ":Q" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Medium;

                                            worksheet1.Cells[incRowExcel, 2].Value = "TOTAL :";
                                            worksheet1.Cells[incRowExcel, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                                            worksheet1.Cells[incRowExcel, 3].Formula = "SUM(C" + _startRowDetail + ":C" + _endRowDetail + ")";
                                            worksheet1.Cells[incRowExcel, 3].Style.Font.Bold = true;
                                            worksheet1.Cells[incRowExcel, 3].Calculate();
                                            worksheet1.Cells[incRowExcel, 3].Style.Numberformat.Format = "#,##0";

                                            worksheet1.Cells[incRowExcel, 4].Formula = "SUM(D" + _startRowDetail + ":D" + _endRowDetail + ")";
                                            worksheet1.Cells[incRowExcel, 4].Style.Font.Bold = true;
                                            worksheet1.Cells[incRowExcel, 4].Calculate();
                                            worksheet1.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##0";

                                            worksheet1.Cells[incRowExcel, 5].Formula = "SUM(E" + _startRowDetail + ":E" + _endRowDetail + ")";
                                            worksheet1.Cells[incRowExcel, 5].Style.Font.Bold = true;
                                            worksheet1.Cells[incRowExcel, 5].Calculate();
                                            worksheet1.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0";

                                            worksheet1.Cells[incRowExcel, 6].Formula = "SUM(F" + _startRowDetail + ":F" + _endRowDetail + ")";
                                            worksheet1.Cells[incRowExcel, 6].Style.Font.Bold = true;
                                            worksheet1.Cells[incRowExcel, 6].Calculate();
                                            worksheet1.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0";

                                            worksheet1.Cells[incRowExcel, 7].Formula = "SUM(G" + _startRowDetail + ":G" + _endRowDetail + ")";
                                            worksheet1.Cells[incRowExcel, 7].Style.Font.Bold = true;
                                            worksheet1.Cells[incRowExcel, 7].Calculate();
                                            worksheet1.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0";

                                            worksheet1.Cells[incRowExcel, 8].Formula = "SUM(H" + _startRowDetail + ":H" + _endRowDetail + ")";
                                            worksheet1.Cells[incRowExcel, 8].Style.Font.Bold = true;
                                            worksheet1.Cells[incRowExcel, 8].Calculate();
                                            worksheet1.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0";

                                            worksheet1.Cells[incRowExcel, 9].Formula = "SUM(I" + _startRowDetail + ":I" + _endRowDetail + ")";
                                            worksheet1.Cells[incRowExcel, 9].Style.Font.Bold = true;
                                            worksheet1.Cells[incRowExcel, 9].Calculate();
                                            worksheet1.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0";

                                            worksheet1.Cells[incRowExcel, 10].Formula = "SUM(J" + _startRowDetail + ":J" + _endRowDetail + ")";
                                            worksheet1.Cells[incRowExcel, 10].Style.Font.Bold = true;
                                            worksheet1.Cells[incRowExcel, 10].Calculate();
                                            worksheet1.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0";

                                            worksheet1.Cells[incRowExcel, 11].Formula = "SUM(K" + _startRowDetail + ":K" + _endRowDetail + ")";
                                            worksheet1.Cells[incRowExcel, 11].Style.Font.Bold = true;
                                            worksheet1.Cells[incRowExcel, 11].Calculate();
                                            worksheet1.Cells[incRowExcel, 11].Style.Numberformat.Format = "#,##0";

                                            worksheet1.Cells[incRowExcel, 12].Formula = "SUM(L" + _startRowDetail + ":L" + _endRowDetail + ")";
                                            worksheet1.Cells[incRowExcel, 12].Style.Font.Bold = true;
                                            worksheet1.Cells[incRowExcel, 12].Calculate();
                                            worksheet1.Cells[incRowExcel, 12].Style.Numberformat.Format = "#,##0";

                                            worksheet1.Cells[incRowExcel, 13].Formula = "SUM(M" + _startRowDetail + ":M" + _endRowDetail + ")";
                                            worksheet1.Cells[incRowExcel, 13].Style.Font.Bold = true;
                                            worksheet1.Cells[incRowExcel, 13].Calculate();
                                            worksheet1.Cells[incRowExcel, 13].Style.Numberformat.Format = "#,##0";

                                            worksheet1.Cells[incRowExcel, 14].Formula = "SUM(N" + _startRowDetail + ":N" + _endRowDetail + ")";
                                            worksheet1.Cells[incRowExcel, 14].Style.Font.Bold = true;
                                            worksheet1.Cells[incRowExcel, 14].Calculate();
                                            worksheet1.Cells[incRowExcel, 14].Style.Numberformat.Format = "#,##0";

                                            worksheet1.Cells[incRowExcel, 15].Formula = "SUM(O" + _startRowDetail + ":O" + _endRowDetail + ")";
                                            worksheet1.Cells[incRowExcel, 15].Style.Font.Bold = true;
                                            worksheet1.Cells[incRowExcel, 15].Calculate();
                                            worksheet1.Cells[incRowExcel, 15].Style.Numberformat.Format = "#,##0";

                                            worksheet1.Cells[incRowExcel, 16].Formula = "SUM(P" + _startRowDetail + ":P" + _endRowDetail + ")";
                                            worksheet1.Cells[incRowExcel, 16].Style.Font.Bold = true;
                                            worksheet1.Cells[incRowExcel, 16].Calculate();
                                            worksheet1.Cells[incRowExcel, 16].Style.Numberformat.Format = "#,##0";

                                            worksheet1.Cells[incRowExcel, 17].Formula = "SUM(Q" + _startRowDetail + ":Q" + _endRowDetail + ")";
                                            worksheet1.Cells[incRowExcel, 17].Style.Font.Bold = true;
                                            worksheet1.Cells[incRowExcel, 17].Calculate();
                                            worksheet1.Cells[incRowExcel, 17].Style.Numberformat.Format = "#,##0";

                                            worksheet1.Cells["C" + incRowExcel + ":Q" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                            worksheet1.Cells["C" + incRowExcel + ":Q" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet1.Cells["C" + incRowExcel + ":Q" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Medium;

                                            incRowExcel = incRowExcel + 3;
                                        }



                                        worksheet1.PrinterSettings.FitToPage = true;
                                        worksheet1.PrinterSettings.FitToWidth = 1;
                                        worksheet1.PrinterSettings.FitToHeight = 0;
                                        worksheet1.PrinterSettings.PrintArea = worksheet1.Cells[1, 1, incRowExcel - 1, 17];
                                        worksheet1.Column(1).Width = 8;
                                        worksheet1.Column(2).Width = 25;
                                        worksheet1.Column(3).Width = 22;
                                        worksheet1.Column(4).Width = 22;
                                        worksheet1.Column(5).Width = 22;
                                        worksheet1.Column(6).Width = 22;
                                        worksheet1.Column(7).Width = 22;
                                        worksheet1.Column(8).Width = 22;
                                        worksheet1.Column(9).Width = 22;
                                        worksheet1.Column(10).Width = 22;
                                        worksheet1.Column(11).Width = 22;
                                        worksheet1.Column(12).Width = 22;
                                        worksheet1.Column(13).Width = 22;
                                        worksheet1.Column(14).Width = 22;
                                        worksheet1.Column(15).Width = 22;
                                        worksheet1.Column(16).Width = 22;
                                        worksheet1.Column(17).Width = 22;


                                        // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                        //worksheet1.Cells["A3:O14"].AutoFitColumns();  // UNTUK AUTO FIT UKURAN TULISAN DENGAN KOLOM, KEBETULAN INI BUAT HEADER AJA DIPAKENYA

                                        worksheet1.PrinterSettings.Orientation = eOrientation.Landscape; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                        worksheet1.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN
                                        worksheet1.HeaderFooter.OddHeader.RightAlignedText = "&14 LAPORAN AUM CABANG DAN SALES";

                                        // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                        worksheet1.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
                                        worksheet1.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
                                        worksheet1.View.PageLayoutView = Tools.DefaultReportPageLayoutView();
                                        worksheet1.View.ShowGridLines = Tools.DefaultReportShowGridLines();

                                        Image img = Image.FromFile(Tools.ReportImage);
                                        worksheet1.HeaderFooter.OddHeader.InsertPicture(img, PictureAlignment.Left);
                                        //worksheet1.HeaderFooter.OddHeader.CenteredText = Tools.DefaultReportHeaderCenterText();
                                        //worksheet1.HeaderFooter.OddHeader.LeftAlignedText = Tools.DefaultReportHeaderLeftText();

                                        worksheet1.HeaderFooter.OddFooter.RightAlignedText =
                                        string.Format("Page {0} of {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);

                                        ////worksheet1.HeaderFooter.OddFooter.CenteredText = Tools.DefaultReportFooterCenterText();
                                        worksheet1.HeaderFooter.OddFooter.LeftAlignedText = Tools.DefaultReportFooterLeftText();



                                    }
                                }
                            }

                        }

                        package.Save();
                        return true;

                    }
                }
                catch (Exception err)
                {
                    return false;
                    throw err;
                }
            }
            #endregion

            #region 09 Laporan MI Fee Summary
            if (_BudgetRpt.ReportName.Equals("09 Laporan MI Fee Summary"))
            {
                try
                {
                    using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                    {
                        DbCon.Open();
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {

                            cmd.CommandText =
                            @"

                                DECLARE @PeriodPKFrom INT
                                Declare @PeriodID nvarchar(50)

                                select @PeriodID  = YEAR(dateadd(year, -1, ID)) From Period where PeriodPK = @PeriodPK and status in (1,2)
                                select @PeriodPKFrom = PeriodPK from Period where ID = @PeriodID and status in (1,2)

                                DECLARE @Result TABLE
                                (
	                                InstrumentPK INT,
	                                MGTFee NUMERIC(18,8),
	                                BegLastYear NUMERIC(22,4),
	                                YTDLastYear NUMERIC(22,4),
	                                AUMLastYear numeric(22,4)
                                )

                                declare @tableRevenue table
                                (
	                                PeriodPK int,
	                                ReportPeriodPK int,
	                                InstrumentPK int,
	                                AgentPK int,
	                                MGTFee numeric(10,4),
	                                January NUMERIC(22,4),
	                                February NUMERIC(22,4),
	                                March NUMERIC(22,4),
	                                April NUMERIC(22,4),
	                                May NUMERIC(22,4),
	                                June NUMERIC(22,4),
	                                July NUMERIC(22,4),
	                                August NUMERIC(22,4),
	                                September NUMERIC(22,4),
	                                October NUMERIC(22,4),
	                                November NUMERIC(22,4),
	                                December NUMERIC(22,4)
                                )

                                DECLARE @AUMFinalPerPeriod TABLE
                                (
	                                Date DATETIME,
	                                InstrumentPK INT,
	                                AUM NUMERIC(22,4),
	                                Mfee numeric(22,4)
                                )
                                INSERT INTO @AUMFinalPerPeriod
                                        ( Date,InstrumentPK, AUM, MFee )
                                SELECT MAX(A.date) Date,A.InstrumentPK,sum(A.AUM), sum(A.MFeeAmount)
                                FROM dbo.AumForBudgetBegBalance A
                                WHERE A.ReportPeriodPK = @PeriodPK AND A.status = 2 and periodpk = @PeriodPKFrom
                                GROUP BY A.InstrumentPK

                                insert into @tableRevenue
                                select A.PeriodPK,A.ReportPeriodPK,A.InstrumentPK,AgentPK,MGTFee,
							    January*case when B.ReksadanaTypePK in (5,6,7) then 1 else isnull(C.RevenuePercent/100,1) end,February*case when B.ReksadanaTypePK in (5,6,7) then 1 else isnull(C.RevenuePercent/100,1) end,
							    March*case when B.ReksadanaTypePK in (5,6,7) then 1 else isnull(C.RevenuePercent/100,1) end,April*case when B.ReksadanaTypePK in (5,6,7) then 1 else isnull(C.RevenuePercent/100,1) end,
							    May*case when B.ReksadanaTypePK in (5,6,7) then 1 else isnull(C.RevenuePercent/100,1) end,June*case when B.ReksadanaTypePK in (5,6,7) then 1 else isnull(C.RevenuePercent/100,1) end,
							    July*case when B.ReksadanaTypePK in (5,6,7) then 1 else isnull(C.RevenuePercent/100,1) end,August*case when B.ReksadanaTypePK in (5,6,7) then 1 else isnull(C.RevenuePercent/100,1) end,
							    September*case when B.ReksadanaTypePK in (5,6,7) then 1 else isnull(C.RevenuePercent/100,1) end,October*case when B.ReksadanaTypePK in (5,6,7) then 1 else isnull(C.RevenuePercent/100,1) end,
							    November*case when B.ReksadanaTypePK in (5,6,7) then 1 else isnull(C.RevenuePercent/100,1) end,December*case when B.ReksadanaTypePK in (5,6,7) then 1 else isnull(C.RevenuePercent/100,1) end 
                                from Revenue A
							    left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status in (1,2)
								left join BudgetVersioning C on A.PeriodPK = C.PeriodPK and C.ReportPeriodPK = C.ReportPeriodPK and C.Status = 2 and Date = (
	                                select max(date) from BudgetVersioning where status in (1,2) and ReportPeriodPK = @periodpk
                                )
							    where A.status = 2 and A.ReportPeriodPK = @periodpk

                                INSERT INTO @Result
                                        ( 
                                          InstrumentPK ,
                                          MGTFee ,
                                          BegLastYear ,
                                          YTDLastYear,
		                                  AUMLastYear
                                        )
                                SELECT A.InstrumentPK,ISNULL(B.MGTFee,0) MGTFee
                                ,ISNULL(A.Mfee,0) BegLastYear
                                --handle mfee YTDLastyear
                                ,CASE WHEN MONTH(A.Date) = 8 THEN ISNULL(A.Mfee,0) +  sum(
	                                ((isnull(A.AUM,0) + ISNULL(B.September,0) + isnull(A.AUM,0) ) /2 / 12 * B.MGTFee /100 ) +  -- september
	                                ((isnull(A.AUM,0) + ISNULL(B.September,0) + isnull(A.AUM,0) + ISNULL(B.September,0) + ISNULL(B.October,0) ) /2 / 12 * B.MGTFee / 100 )+ -- Oktober
	                                ((isnull(A.AUM,0) + ISNULL(B.September,0) + ISNULL(B.October,0) + isnull(A.AUM,0) + ISNULL(B.September,0) + ISNULL(B.October,0) + ISNULL(B.November,0) ) /2 / 12 * B.MGTFee / 100 )+ -- November
	                                ((isnull(A.AUM,0) + ISNULL(B.September,0) + ISNULL(B.October,0) + ISNULL(B.November,0) + isnull(A.AUM,0) + ISNULL(B.September,0) + ISNULL(B.October,0) + ISNULL(B.November,0) + ISNULL(B.December,0) ) /2 / 12 * B.MGTFee / 100 ) -- Desember
                                )
	                                WHEN MONTH(A.Date) = 9 THEN ISNULL(A.Mfee,0)  + sum(
		                                ((isnull(A.AUM,0) + isnull(A.AUM,0) + ISNULL(B.October,0) ) /2 / 12 * B.MGTFee / 100 )+ -- Oktober
		                                ((isnull(A.AUM,0) + ISNULL(B.October,0) + isnull(A.AUM,0) + ISNULL(B.October,0) + ISNULL(B.November,0) ) /2 / 12 * B.MGTFee / 100 )+ -- November
		                                ((isnull(A.AUM,0) + ISNULL(B.October,0) + ISNULL(B.November,0) + isnull(A.AUM,0) + ISNULL(B.October,0) + ISNULL(B.November,0) + ISNULL(B.December,0) ) /2 / 12 * B.MGTFee / 100 ) -- Desember
	                                )
		                                WHEN MONTH(A.Date) = 10 THEN ISNULL(A.Mfee,0)  + sum(
			                                ((isnull(A.AUM,0) + isnull(A.AUM,0) + ISNULL(B.November,0) ) /2 / 12 * B.MGTFee / 100 )+ -- November
			                                ((isnull(A.AUM,0) + ISNULL(B.November,0) + isnull(A.AUM,0) + ISNULL(B.November,0) + ISNULL(B.December,0) ) /2 / 12 * B.MGTFee / 100 ) -- Desember
		                                )
			                                WHEN MONTH(A.Date) = 11 THEN ISNULL(A.Mfee,0)  + sum(
				                                ((isnull(A.AUM,0) + isnull(A.AUM,0) + ISNULL(B.December,0) ) /2 / 12 * B.MGTFee / 100 ) -- Desember
			                                ) END YTDLastYear,
                                --AUM LAST YEAR
                                CASE WHEN MONTH(A.Date) = 8 THEN ISNULL(A.AUM,0) + sum(ISNULL(B.September,0)  + ISNULL(B.October,0) + ISNULL(B.November,0) + ISNULL(B.December,0))
		                                WHEN MONTH(A.Date) = 9 THEN ISNULL(A.Mfee,0)  + sum(ISNULL(B.October,0) + ISNULL(B.November,0) + ISNULL(B.December,0))
			                                WHEN MONTH(A.Date) = 10 THEN ISNULL(A.Mfee,0)  + sum(ISNULL(B.November,0) + ISNULL(B.December,0))
				                                WHEN MONTH(A.Date) = 11 THEN ISNULL(A.Mfee,0)  + sum(ISNULL(B.November,0) + ISNULL(B.December,0)) END AUMLastYear
                                FROM @AUMFinalPerPeriod A
                                left JOIN
                                (
	                                SELECT A.InstrumentPK,A.MGTFee,sum(A.January) January, sum(A.February) February, sum(A.March) March, sum(A.April) April,sum(A.May) May, sum(A.June) June, sum(A.July) July, sum(A.August) August, sum(A.September) September, sum(A.October) October, sum(A.November) November, sum(A.December) December FROM @tableRevenue A
	                                WHERE A.ReportPeriodPK = @PeriodPK AND A.PeriodPK <> @PeriodPK 
	                                group by A.InstrumentPK, A.MGTFee
                                )B ON A.InstrumentPK = B.InstrumentPK 
                                group by A.InstrumentPK ,B.MGTFee,A.AUM,A.Date,A.Mfee


                                select A.InstrumentName,A.Category,A.MGTFee MGTFeePercent,YTDSep MFeeAmountYTDSept,YTDDes MFeeAmountYTDDec,
                                (AUMLastYear + January)/2/12/100 * MGTFee January,(January + February)/2/12/100 * MGTFee February,(February + March)/2/12/100 * MGTFee March, (March + April)/2/12/100 * MGTFee April,
                                (April + May)/2/12/100 * MGTFee May,(May + June)/2/12/100 * MGTFee june,(June + July)/2/12/100 * MGTFee July,(July + August)/2/12/100 * MGTFee August,(August + September)/2/12/100 * MGTFee September,
                                (September + October)/2/12/100 * MGTFee October,(October + November)/2/12/100 * MGTFee November,(November + December)/2/12/100 * MGTFee December
                                 from (
                                SELECT 
                                C.Name InstrumentName,isnull(D.Description,'') Category,A.MGTFee, ISNULL(A.BeglastYear,0) YTDSep ,SUM(ISNULL(A.YTDLastYear,0))  YTDDes, A.AUMLastYear
                                ,SUM(ISNULL(A.AUMLastYear,0) + ISNULL(B.January,0)) January
                                ,SUM(ISNULL(A.AUMLastYear,0) + ISNULL(B.January,0) + ISNULL(B.February,0)) February
                                ,SUM(ISNULL(A.AUMLastYear,0) + ISNULL(B.January,0) + ISNULL(B.February,0) + ISNULL(B.March,0)) March
                                ,SUM(ISNULL(A.AUMLastYear,0) + ISNULL(B.January,0) + ISNULL(B.February,0) + ISNULL(B.March,0) + ISNULL(B.April,0)) April
                                ,SUM(ISNULL(A.AUMLastYear,0) + ISNULL(B.January,0) + ISNULL(B.February,0) + ISNULL(B.March,0) + ISNULL(B.April,0) + ISNULL(B.May,0)) May
                                ,SUM(ISNULL(A.AUMLastYear,0) + ISNULL(B.January,0) + ISNULL(B.February,0) + ISNULL(B.March,0) + ISNULL(B.April,0) + ISNULL(B.May,0) + ISNULL(B.June,0)) June
                                ,SUM(ISNULL(A.AUMLastYear,0) + ISNULL(B.January,0) + ISNULL(B.February,0) + ISNULL(B.March,0) + ISNULL(B.April,0) + ISNULL(B.May,0) + ISNULL(B.June,0) + ISNULL(B.July,0)) July
                                ,SUM(ISNULL(A.AUMLastYear,0) + ISNULL(B.January,0) + ISNULL(B.February,0) + ISNULL(B.March,0) + ISNULL(B.April,0) + ISNULL(B.May,0) + ISNULL(B.June,0) + ISNULL(B.July,0) + ISNULL(B.August,0)) August
                                ,SUM(ISNULL(A.AUMLastYear,0) + ISNULL(B.January,0) + ISNULL(B.February,0) + ISNULL(B.March,0) + ISNULL(B.April,0) + ISNULL(B.May,0) + ISNULL(B.June,0) + ISNULL(B.July,0) + ISNULL(B.August,0) + ISNULL(B.September,0)) September
                                ,SUM(ISNULL(A.AUMLastYear,0) + ISNULL(B.January,0) + ISNULL(B.February,0) + ISNULL(B.March,0) + ISNULL(B.April,0) + ISNULL(B.May,0) + ISNULL(B.June,0) + ISNULL(B.July,0) + ISNULL(B.August,0) + ISNULL(B.September,0) + ISNULL(B.October,0)) October
                                ,SUM(ISNULL(A.AUMLastYear,0) + ISNULL(B.January,0) + ISNULL(B.February,0) + ISNULL(B.March,0) + ISNULL(B.April,0) + ISNULL(B.May,0) + ISNULL(B.June,0) + ISNULL(B.July,0) + ISNULL(B.August,0) + ISNULL(B.September,0) + ISNULL(B.October,0) + ISNULL(B.November,0)) November
                                ,SUM(ISNULL(A.AUMLastYear,0) + ISNULL(B.January,0) + ISNULL(B.February,0) + ISNULL(B.March,0) + ISNULL(B.April,0) + ISNULL(B.May,0) + ISNULL(B.June,0) + ISNULL(B.July,0) + ISNULL(B.August,0) + ISNULL(B.September,0) + ISNULL(B.October,0) + ISNULL(B.November,0) + ISNULL(B.December,0)) December
                                FROM @Result A
                                LEFT JOIN
                                (
	                                SELECT A.InstrumentPK,sum(A.January) January, sum(A.February) February, sum(A.March) March, sum(A.April) April,sum(A.May) May, sum(A.June) June, sum(A.July) July, sum(A.August) August, sum(A.September) September, sum(A.October) October, sum(A.November) November, sum(A.December) December FROM  @tableRevenue A
	                                WHERE A.ReportPeriodPK = @PeriodPK AND A.PeriodPK = @PeriodPK
	                                group by A.InstrumentPK
                                )B ON A.InstrumentPK = B.InstrumentPK
                                left join Instrument C on A.InstrumentPK = C.InstrumentPK and C.status in (1,2)
                                left join ReksadanaType D on C.ReksadanaTypePK = D.ReksadanaTypePK and D.status in (1,2)
                                GROUP BY A.InstrumentPK,BeglastYear,C.Name,D.Description,A.MGTFee,A.AUMLastYear

                                )A
                                ORDER BY A.InstrumentName,Category
";

                            cmd.CommandTimeout = 0;
                            cmd.Parameters.AddWithValue("@PeriodPK", _BudgetRpt.PeriodPK);


                            using (SqlDataReader dr0 = cmd.ExecuteReader())
                            {
                                if (!dr0.HasRows)
                                {
                                    return false;
                                }
                                else
                                {
                                    string filePath = Tools.ReportsPath + "LaporanMIFeeSummary" + "_" + _userID + ".xlsx";
                                    string pdfPath = Tools.ReportsPath + "LaporanMIFeeSummary" + "_" + _userID + ".pdf";
                                    FileInfo excelFile = new FileInfo(filePath);
                                    if (excelFile.Exists)
                                    {
                                        excelFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                        excelFile = new FileInfo(filePath);
                                    }


                                    // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                    using (ExcelPackage package = new ExcelPackage(excelFile))
                                    {
                                        package.Workbook.Properties.Title = "BudgetReport";
                                        package.Workbook.Properties.Author = Tools.DefaultReportAuthor();
                                        package.Workbook.Properties.Comments = Tools.DefaultReportComments();
                                        package.Workbook.Properties.Company = Tools.DefaultReportCompany();
                                        package.Workbook.Properties.SetCustomPropertyValue("Checked by", _userID);
                                        package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", Tools.DefaultReportAssemblyName());

                                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Budget Report");


                                        //ATUR DATA GROUPINGNYA DULU
                                        List<LaporanMIFeeSummary> rList = new List<LaporanMIFeeSummary>();
                                        while (dr0.Read())
                                        {
                                            LaporanMIFeeSummary rSingle = new LaporanMIFeeSummary();
                                            rSingle.NamaProduk = Convert.ToString(dr0["InstrumentName"]);
                                            rSingle.ProdukKategori = Convert.ToString(dr0["Category"]);
                                            rSingle.YTDSep = Convert.ToDecimal(dr0["MFeeAmountYTDSept"]);
                                            rSingle.YTDDes = Convert.ToDecimal(dr0["MFeeAmountYTDDec"]);
                                            rSingle.MGTFee = Convert.ToDecimal(dr0["MGTFeePercent"]);
                                            rSingle.January1 = Convert.ToDecimal(dr0["January"]);
                                            rSingle.February1 = Convert.ToDecimal(dr0["February"]);
                                            rSingle.March1 = Convert.ToDecimal(dr0["March"]);
                                            rSingle.April1 = Convert.ToDecimal(dr0["April"]);
                                            rSingle.May1 = Convert.ToDecimal(dr0["May"]);
                                            rSingle.June1 = Convert.ToDecimal(dr0["June"]);
                                            rSingle.July1 = Convert.ToDecimal(dr0["July"]);
                                            rSingle.August1 = Convert.ToDecimal(dr0["August"]);
                                            rSingle.September1 = Convert.ToDecimal(dr0["September"]);
                                            rSingle.October1 = Convert.ToDecimal(dr0["October"]);
                                            rSingle.November1 = Convert.ToDecimal(dr0["November"]);
                                            rSingle.December1 = Convert.ToDecimal(dr0["December"]);
                                            rList.Add(rSingle);

                                        }

                                        var QueryByClientID =
                                         from r in rList
                                         orderby r.ProdukKategori
                                         group r by new { r.ProdukKategori } into rGroup
                                         select rGroup;

                                        int incRowExcel = 1;
                                        worksheet.Cells[incRowExcel, 1].Value = "PT MNC ASSET MANAGEMENT";
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Size = 14;
                                        incRowExcel++;
                                        worksheet.Cells[incRowExcel, 1].Value = "LAPORAN MI FEE SUMMARY";
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 1].Style.Font.Size = 14;
                                        incRowExcel++;
                                        incRowExcel++;
                                        incRowExcel++;

                                        worksheet.Cells["A" + incRowExcel + ":S" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["A" + incRowExcel + ":S" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["A" + incRowExcel + ":S" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["A" + incRowExcel + ":S" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Medium;


                                        worksheet.Cells[incRowExcel, 1].Value = "No";
                                        worksheet.Cells[incRowExcel, 2].Value = "Nama Produk";
                                        worksheet.Cells[incRowExcel, 3].Value = "Produk Kategori";
                                        worksheet.Cells[incRowExcel, 4].Value = "MGT Fee";
                                        worksheet.Cells[incRowExcel, 5].Value = "YTD Sep " + _host.Get_PeriodByRevenue(_BudgetRpt.PeriodPK, 1);
                                        worksheet.Cells[incRowExcel, 6].Value = "YTD Des " + _host.Get_PeriodByRevenue(_BudgetRpt.PeriodPK, 1);

                                        worksheet.Cells[incRowExcel, 7].Value = "JAN ";
                                        worksheet.Cells[incRowExcel, 8].Value = "FEB ";
                                        worksheet.Cells[incRowExcel, 9].Value = "MAR ";
                                        worksheet.Cells[incRowExcel, 10].Value = "APR ";
                                        worksheet.Cells[incRowExcel, 11].Value = "MAY ";
                                        worksheet.Cells[incRowExcel, 12].Value = "JUN ";
                                        worksheet.Cells[incRowExcel, 13].Value = "JUL ";
                                        worksheet.Cells[incRowExcel, 14].Value = "AUG ";
                                        worksheet.Cells[incRowExcel, 15].Value = "SEP ";
                                        worksheet.Cells[incRowExcel, 16].Value = "OCT ";
                                        worksheet.Cells[incRowExcel, 17].Value = "NOV ";
                                        worksheet.Cells[incRowExcel, 18].Value = "DEC ";
                                        worksheet.Cells[incRowExcel, 19].Value = "TOTAL";

                                        worksheet.Cells["A" + incRowExcel + ":S" + incRowExcel].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                        worksheet.Cells["A" + incRowExcel + ":S" + incRowExcel].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                        worksheet.Cells["A" + incRowExcel + ":S" + incRowExcel].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                                        worksheet.Cells["A" + incRowExcel + ":S" + incRowExcel].Style.Font.Bold = true;
                                        incRowExcel++;
                                        int _startTotal = incRowExcel;
                                        int _startRowDetail = incRowExcel;
                                        int _endRowDetail = 0;
                                        int _no = 1;

                                        foreach (var rsHeader in QueryByClientID)
                                        {
                                            _startRowDetail = incRowExcel;

                                            foreach (var rsDetail in rsHeader)
                                            {


                                                //ThickBox Border
                                                worksheet.Cells["C" + incRowExcel + ":S" + incRowExcel].Style.Fill.PatternType = ExcelFillStyle.None;

                                                worksheet.Cells[incRowExcel, 1].Value = _no;
                                                worksheet.Cells[incRowExcel, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                worksheet.Cells[incRowExcel, 2].Value = rsDetail.NamaProduk;
                                                worksheet.Cells[incRowExcel, 3].Value = rsDetail.ProdukKategori;
                                                worksheet.Cells[incRowExcel, 4].Value = rsDetail.MGTFee;
                                                worksheet.Cells[incRowExcel, 4].Style.Numberformat.Format = "#,##0.00";
                                                worksheet.Cells[incRowExcel, 5].Value = rsDetail.YTDSep;
                                                worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 6].Value = rsDetail.YTDDes;
                                                worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0";

                                                worksheet.Cells[incRowExcel, 7].Value = rsDetail.January1;
                                                worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 8].Value = rsDetail.February1;
                                                worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 9].Value = rsDetail.March1;
                                                worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 10].Value = rsDetail.April1;
                                                worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 11].Value = rsDetail.May1;
                                                worksheet.Cells[incRowExcel, 11].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 12].Value = rsDetail.June1;
                                                worksheet.Cells[incRowExcel, 12].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 13].Value = rsDetail.July1;
                                                worksheet.Cells[incRowExcel, 13].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 14].Value = rsDetail.August1;
                                                worksheet.Cells[incRowExcel, 14].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 15].Value = rsDetail.September1;
                                                worksheet.Cells[incRowExcel, 15].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 16].Value = rsDetail.October1;
                                                worksheet.Cells[incRowExcel, 16].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 17].Value = rsDetail.November1;
                                                worksheet.Cells[incRowExcel, 17].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells[incRowExcel, 18].Value = rsDetail.December1;
                                                worksheet.Cells[incRowExcel, 18].Style.Numberformat.Format = "#,##0";

                                                worksheet.Cells[incRowExcel, 19].Formula = "SUM(G" + incRowExcel + ":R" + incRowExcel + ")";
                                                worksheet.Cells[incRowExcel, 19].Calculate();
                                                worksheet.Cells[incRowExcel, 19].Style.Numberformat.Format = "#,##0";
                                                worksheet.Cells["A" + incRowExcel + ":S" + incRowExcel].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                                                _no++;
                                                _endRowDetail = incRowExcel;
                                                incRowExcel++;



                                            }

                                            worksheet.Cells["A" + _startRowDetail + ":S" + _endRowDetail].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + _startRowDetail + ":S" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + _startRowDetail + ":S" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["A" + _startRowDetail + ":S" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Medium;


                                            worksheet.Cells["D" + incRowExcel + ":S" + incRowExcel].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                            worksheet.Cells["D" + incRowExcel + ":S" + incRowExcel].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightBlue);


                                            worksheet.Cells[incRowExcel, 4].Value = "TOTAL :";
                                            worksheet.Cells[incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                                            worksheet.Cells[incRowExcel, 5].Formula = "SUM(E" + _startRowDetail + ":E" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 5].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 5].Calculate();
                                            worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0";

                                            worksheet.Cells[incRowExcel, 6].Formula = "SUM(F" + _startRowDetail + ":F" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 6].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 6].Calculate();
                                            worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0";

                                            worksheet.Cells[incRowExcel, 7].Formula = "SUM(G" + _startRowDetail + ":G" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 7].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 7].Calculate();
                                            worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0";

                                            worksheet.Cells[incRowExcel, 8].Formula = "SUM(H" + _startRowDetail + ":H" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 8].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 8].Calculate();
                                            worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0";

                                            worksheet.Cells[incRowExcel, 9].Formula = "SUM(I" + _startRowDetail + ":I" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 9].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 9].Calculate();
                                            worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0";

                                            worksheet.Cells[incRowExcel, 10].Formula = "SUM(J" + _startRowDetail + ":J" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 10].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 10].Calculate();
                                            worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0";

                                            worksheet.Cells[incRowExcel, 11].Formula = "SUM(K" + _startRowDetail + ":K" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 11].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 11].Calculate();
                                            worksheet.Cells[incRowExcel, 11].Style.Numberformat.Format = "#,##0";

                                            worksheet.Cells[incRowExcel, 12].Formula = "SUM(L" + _startRowDetail + ":L" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 12].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 12].Calculate();
                                            worksheet.Cells[incRowExcel, 12].Style.Numberformat.Format = "#,##0";

                                            worksheet.Cells[incRowExcel, 13].Formula = "SUM(M" + _startRowDetail + ":M" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 13].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 13].Calculate();
                                            worksheet.Cells[incRowExcel, 13].Style.Numberformat.Format = "#,##0";

                                            worksheet.Cells[incRowExcel, 14].Formula = "SUM(N" + _startRowDetail + ":N" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 14].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 14].Calculate();
                                            worksheet.Cells[incRowExcel, 14].Style.Numberformat.Format = "#,##0";

                                            worksheet.Cells[incRowExcel, 15].Formula = "SUM(O" + _startRowDetail + ":O" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 15].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 15].Calculate();
                                            worksheet.Cells[incRowExcel, 15].Style.Numberformat.Format = "#,##0";

                                            worksheet.Cells[incRowExcel, 16].Formula = "SUM(P" + _startRowDetail + ":P" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 16].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 16].Calculate();
                                            worksheet.Cells[incRowExcel, 16].Style.Numberformat.Format = "#,##0";

                                            worksheet.Cells[incRowExcel, 17].Formula = "SUM(Q" + _startRowDetail + ":Q" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 17].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 17].Calculate();
                                            worksheet.Cells[incRowExcel, 17].Style.Numberformat.Format = "#,##0";

                                            worksheet.Cells[incRowExcel, 18].Formula = "SUM(R" + _startRowDetail + ":R" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 18].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 18].Calculate();
                                            worksheet.Cells[incRowExcel, 18].Style.Numberformat.Format = "#,##0";

                                            worksheet.Cells[incRowExcel, 19].Formula = "SUM(S" + _startRowDetail + ":S" + _endRowDetail + ")";
                                            worksheet.Cells[incRowExcel, 19].Style.Font.Bold = true;
                                            worksheet.Cells[incRowExcel, 19].Calculate();
                                            worksheet.Cells[incRowExcel, 19].Style.Numberformat.Format = "#,##0";

                                            worksheet.Cells["D" + incRowExcel + ":S" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["D" + incRowExcel + ":S" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                            worksheet.Cells["D" + incRowExcel + ":S" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Medium;

                                            _endRowDetail = incRowExcel;
                                            incRowExcel++;


                                        }

                                        worksheet.Cells["A" + _startRowDetail + ":S" + _endRowDetail].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["A" + _startRowDetail + ":S" + _endRowDetail].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["A" + _startRowDetail + ":S" + _endRowDetail].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["A" + _startRowDetail + ":S" + _endRowDetail].Style.Border.Right.Style = ExcelBorderStyle.Medium;

                                        worksheet.Cells["D" + incRowExcel + ":S" + incRowExcel].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                        worksheet.Cells["D" + incRowExcel + ":S" + incRowExcel].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightBlue);

                                        worksheet.Cells[incRowExcel, 4].Value = "GRAND TOTAL :";
                                        worksheet.Cells[incRowExcel, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                                        worksheet.Cells[incRowExcel, 5].Formula = "SUM(E" + _startTotal + ":E" + _endRowDetail + ")/2";
                                        worksheet.Cells[incRowExcel, 5].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 5].Calculate();
                                        worksheet.Cells[incRowExcel, 5].Style.Numberformat.Format = "#,##0";

                                        worksheet.Cells[incRowExcel, 6].Formula = "SUM(F" + _startTotal + ":F" + _endRowDetail + ")/2";
                                        worksheet.Cells[incRowExcel, 6].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 6].Calculate();
                                        worksheet.Cells[incRowExcel, 6].Style.Numberformat.Format = "#,##0";

                                        worksheet.Cells[incRowExcel, 7].Formula = "SUM(G" + _startTotal + ":G" + _endRowDetail + ")/2";
                                        worksheet.Cells[incRowExcel, 7].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 7].Calculate();
                                        worksheet.Cells[incRowExcel, 7].Style.Numberformat.Format = "#,##0";

                                        worksheet.Cells[incRowExcel, 8].Formula = "SUM(H" + _startTotal + ":H" + _endRowDetail + ")/2";
                                        worksheet.Cells[incRowExcel, 8].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 8].Calculate();
                                        worksheet.Cells[incRowExcel, 8].Style.Numberformat.Format = "#,##0";

                                        worksheet.Cells[incRowExcel, 9].Formula = "SUM(I" + _startTotal + ":I" + _endRowDetail + ")/2";
                                        worksheet.Cells[incRowExcel, 9].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 9].Calculate();
                                        worksheet.Cells[incRowExcel, 9].Style.Numberformat.Format = "#,##0";

                                        worksheet.Cells[incRowExcel, 10].Formula = "SUM(J" + _startTotal + ":J" + _endRowDetail + ")/2";
                                        worksheet.Cells[incRowExcel, 10].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 10].Calculate();
                                        worksheet.Cells[incRowExcel, 10].Style.Numberformat.Format = "#,##0";

                                        worksheet.Cells[incRowExcel, 11].Formula = "SUM(K" + _startTotal + ":K" + _endRowDetail + ")/2";
                                        worksheet.Cells[incRowExcel, 11].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 11].Calculate();
                                        worksheet.Cells[incRowExcel, 11].Style.Numberformat.Format = "#,##0";

                                        worksheet.Cells[incRowExcel, 12].Formula = "SUM(L" + _startTotal + ":L" + _endRowDetail + ")/2";
                                        worksheet.Cells[incRowExcel, 12].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 12].Calculate();
                                        worksheet.Cells[incRowExcel, 12].Style.Numberformat.Format = "#,##0";

                                        worksheet.Cells[incRowExcel, 13].Formula = "SUM(M" + _startTotal + ":M" + _endRowDetail + ")/2";
                                        worksheet.Cells[incRowExcel, 13].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 13].Calculate();
                                        worksheet.Cells[incRowExcel, 13].Style.Numberformat.Format = "#,##0";

                                        worksheet.Cells[incRowExcel, 14].Formula = "SUM(N" + _startTotal + ":N" + _endRowDetail + ")/2";
                                        worksheet.Cells[incRowExcel, 14].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 14].Calculate();
                                        worksheet.Cells[incRowExcel, 14].Style.Numberformat.Format = "#,##0";

                                        worksheet.Cells[incRowExcel, 15].Formula = "SUM(O" + _startTotal + ":O" + _endRowDetail + ")/2";
                                        worksheet.Cells[incRowExcel, 15].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 15].Calculate();
                                        worksheet.Cells[incRowExcel, 15].Style.Numberformat.Format = "#,##0";

                                        worksheet.Cells[incRowExcel, 16].Formula = "SUM(P" + _startTotal + ":P" + _endRowDetail + ")/2";
                                        worksheet.Cells[incRowExcel, 16].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 16].Calculate();
                                        worksheet.Cells[incRowExcel, 16].Style.Numberformat.Format = "#,##0";

                                        worksheet.Cells[incRowExcel, 17].Formula = "SUM(Q" + _startTotal + ":Q" + _endRowDetail + ")/2";
                                        worksheet.Cells[incRowExcel, 17].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 17].Calculate();
                                        worksheet.Cells[incRowExcel, 17].Style.Numberformat.Format = "#,##0";

                                        worksheet.Cells[incRowExcel, 18].Formula = "SUM(R" + _startTotal + ":R" + _endRowDetail + ")/2";
                                        worksheet.Cells[incRowExcel, 18].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 18].Calculate();
                                        worksheet.Cells[incRowExcel, 18].Style.Numberformat.Format = "#,##0";

                                        worksheet.Cells[incRowExcel, 19].Formula = "SUM(S" + _startTotal + ":S" + _endRowDetail + ")/2";
                                        worksheet.Cells[incRowExcel, 19].Style.Font.Bold = true;
                                        worksheet.Cells[incRowExcel, 19].Calculate();
                                        worksheet.Cells[incRowExcel, 19].Style.Numberformat.Format = "#,##0";

                                        worksheet.Cells["D" + incRowExcel + ":S" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["D" + incRowExcel + ":S" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                                        worksheet.Cells["D" + incRowExcel + ":S" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Medium;



                                        worksheet.PrinterSettings.FitToPage = true;
                                        worksheet.PrinterSettings.FitToWidth = 1;
                                        worksheet.PrinterSettings.FitToHeight = 0;
                                        worksheet.PrinterSettings.PrintArea = worksheet.Cells[1, 1, incRowExcel - 1, 19];
                                        worksheet.Column(1).Width = 8;
                                        worksheet.Column(2).AutoFit();
                                        worksheet.Column(3).AutoFit();
                                        worksheet.Column(4).Width = 15;
                                        worksheet.Column(5).Width = 15;
                                        worksheet.Column(6).Width = 15;
                                        worksheet.Column(7).Width = 15;
                                        worksheet.Column(8).Width = 15;
                                        worksheet.Column(9).Width = 17;
                                        worksheet.Column(10).Width = 17;
                                        worksheet.Column(11).Width = 17;
                                        worksheet.Column(12).Width = 17;
                                        worksheet.Column(13).Width = 17;
                                        worksheet.Column(14).Width = 17;
                                        worksheet.Column(15).Width = 17;
                                        worksheet.Column(16).Width = 17;
                                        worksheet.Column(17).Width = 17;
                                        worksheet.Column(18).Width = 17;
                                        worksheet.Column(19).Width = 17;



                                        // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                        //worksheet.Cells["A3:O14"].AutoFitColumns();  // UNTUK AUTO FIT UKURAN TULISAN DENGAN KOLOM, KEBETULAN INI BUAT HEADER AJA DIPAKENYA

                                        worksheet.PrinterSettings.Orientation = eOrientation.Landscape; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                        worksheet.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN
                                        worksheet.HeaderFooter.OddHeader.RightAlignedText = "&14 LAPORAN MI FEE SUMMARY";

                                        // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                        worksheet.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
                                        worksheet.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
                                        worksheet.View.PageLayoutView = Tools.DefaultReportPageLayoutView();
                                        worksheet.View.ShowGridLines = Tools.DefaultReportShowGridLines();

                                        Image img = Image.FromFile(Tools.ReportImage);
                                        worksheet.HeaderFooter.OddHeader.InsertPicture(img, PictureAlignment.Left);
                                        //worksheet.HeaderFooter.OddHeader.CenteredText = Tools.DefaultReportHeaderCenterText();
                                        //worksheet.HeaderFooter.OddHeader.LeftAlignedText = Tools.DefaultReportHeaderLeftText();

                                        worksheet.HeaderFooter.OddFooter.RightAlignedText =
                                        string.Format("Page {0} of {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);

                                        ////worksheet.HeaderFooter.OddFooter.CenteredText = Tools.DefaultReportFooterCenterText();
                                        worksheet.HeaderFooter.OddFooter.LeftAlignedText = Tools.DefaultReportFooterLeftText();



                                        package.Save();
                                        return true;
                                    }
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
            #endregion

            #region 10 Laporan Profit And Loss
            if (_BudgetRpt.ReportName.Equals("10 Laporan Profit And Loss"))
            {
                try
                {
                    using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                    {
                        DbCon.Open();
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {


                            cmd.CommandText =
                            @"Select Row
                            ,isnull(sum(B.January),0) January
                            ,isnull(sum(B.February),0) February
                            ,isnull(sum(B.March),0) March
                            ,isnull(sum(B.April),0) April
                            ,isnull(sum(B.May),0) May
                            ,isnull(sum(B.June),0) June
                            ,isnull(sum(B.July),0) July,isnull(sum(B.August),0) August
                            ,isnull(sum(B.September),0) September
                            ,isnull(sum(B.October),0) October
                            ,isnull(sum(B.November),0) November
                            ,isnull(sum(B.December),0) December
                            from templateReport A
                            left join Budget B on A.AccountPK = B.AccountPK and B.PeriodPK = @PeriodPK
                            where A.ReportName = '10LaporanProfitAndLoss'
                            group by A.Row
                            order by A.Row
                                ";
                            cmd.CommandTimeout = 0;
                            cmd.Parameters.AddWithValue("@PeriodPK", _BudgetRpt.PeriodPK);

                            using (SqlDataReader dr0 = cmd.ExecuteReader())
                            {
                                if (!dr0.HasRows)
                                {
                                    return false;
                                }
                                else
                                {
                                    string filePath = Tools.ReportsPath + "LaporanProfitAndLoss" + "_" + _userID + ".xlsx";
                                    File.Copy(Tools.ReportsTemplatePath + "05_PnL.xlsx", filePath, true);
                                    FileInfo excelFile = new FileInfo(filePath);
                                    int _col = 0;
                                    using (ExcelPackage package = new ExcelPackage(excelFile))
                                    {

                                        ExcelWorksheet worksheet = package.Workbook.Worksheets[1];
                                        while (dr0.Read())
                                        {
                                            for (int inc1 = 0; inc1 < dr0.FieldCount; inc1++)
                                            {
                                                if (dr0.GetName(inc1).ToString() == "January")
                                                {
                                                    worksheet.Cells[Convert.ToInt32(dr0["Row"]), 3].Style.Numberformat.Format = "#,##0";
                                                    worksheet.Cells[Convert.ToInt32(dr0["Row"]), 3].Value = Convert.ToDecimal(dr0["January"]);
                                                }

                                                if (dr0.GetName(inc1).ToString() == "February")
                                                {
                                                    worksheet.Cells[Convert.ToInt32(dr0["Row"]), 4].Style.Numberformat.Format = "#,##0";
                                                    worksheet.Cells[Convert.ToInt32(dr0["Row"]), 4].Value = Convert.ToDecimal(dr0["February"]);
                                                }

                                                if (dr0.GetName(inc1).ToString() == "March")
                                                {
                                                    worksheet.Cells[Convert.ToInt32(dr0["Row"]), 5].Style.Numberformat.Format = "#,##0";
                                                    worksheet.Cells[Convert.ToInt32(dr0["Row"]), 5].Value = Convert.ToDecimal(dr0["March"]);
                                                }

                                                if (dr0.GetName(inc1).ToString() == "April")
                                                {
                                                    worksheet.Cells[Convert.ToInt32(dr0["Row"]), 7].Style.Numberformat.Format = "#,##0";
                                                    worksheet.Cells[Convert.ToInt32(dr0["Row"]), 7].Value = Convert.ToDecimal(dr0["April"]);
                                                }

                                                if (dr0.GetName(inc1).ToString() == "May")
                                                {
                                                    worksheet.Cells[Convert.ToInt32(dr0["Row"]), 8].Style.Numberformat.Format = "#,##0";
                                                    worksheet.Cells[Convert.ToInt32(dr0["Row"]), 8].Value = Convert.ToDecimal(dr0["May"]);
                                                }

                                                if (dr0.GetName(inc1).ToString() == "June")
                                                {
                                                    worksheet.Cells[Convert.ToInt32(dr0["Row"]), 9].Style.Numberformat.Format = "#,##0";
                                                    worksheet.Cells[Convert.ToInt32(dr0["Row"]), 9].Value = Convert.ToDecimal(dr0["June"]);
                                                }

                                                if (dr0.GetName(inc1).ToString() == "July")
                                                {
                                                    worksheet.Cells[Convert.ToInt32(dr0["Row"]), 11].Style.Numberformat.Format = "#,##0";
                                                    worksheet.Cells[Convert.ToInt32(dr0["Row"]), 11].Value = Convert.ToDecimal(dr0["July"]);
                                                }

                                                if (dr0.GetName(inc1).ToString() == "August")
                                                {
                                                    worksheet.Cells[Convert.ToInt32(dr0["Row"]), 12].Style.Numberformat.Format = "#,##0";
                                                    worksheet.Cells[Convert.ToInt32(dr0["Row"]), 12].Value = Convert.ToDecimal(dr0["August"]);
                                                }

                                                if (dr0.GetName(inc1).ToString() == "September")
                                                {
                                                    worksheet.Cells[Convert.ToInt32(dr0["Row"]), 13].Style.Numberformat.Format = "#,##0";
                                                    worksheet.Cells[Convert.ToInt32(dr0["Row"]), 13].Value = Convert.ToDecimal(dr0["September"]);
                                                }

                                                if (dr0.GetName(inc1).ToString() == "October")
                                                {
                                                    worksheet.Cells[Convert.ToInt32(dr0["Row"]), 15].Style.Numberformat.Format = "#,##0";
                                                    worksheet.Cells[Convert.ToInt32(dr0["Row"]), 15].Value = Convert.ToDecimal(dr0["October"]);
                                                }

                                                if (dr0.GetName(inc1).ToString() == "November")
                                                {
                                                    worksheet.Cells[Convert.ToInt32(dr0["Row"]), 16].Style.Numberformat.Format = "#,##0";
                                                    worksheet.Cells[Convert.ToInt32(dr0["Row"]), 16].Value = Convert.ToDecimal(dr0["November"]);
                                                }

                                                if (dr0.GetName(inc1).ToString() == "December")
                                                {
                                                    worksheet.Cells[Convert.ToInt32(dr0["Row"]), 17].Style.Numberformat.Format = "#,##0";
                                                    worksheet.Cells[Convert.ToInt32(dr0["Row"]), 17].Value = Convert.ToDecimal(dr0["December"]);
                                                }

                                            }

                                        }
                                        worksheet.Calculate();
                                        worksheet.PrinterSettings.Orientation = eOrientation.Landscape; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                        worksheet.Column(1).Width = 35;
                                        worksheet.Column(2).Width = 30;
                                        worksheet.Column(3).Width = 20;
                                        worksheet.Column(4).Width = 20;
                                        worksheet.Column(5).Width = 20;
                                        worksheet.Column(6).Width = 20;
                                        worksheet.Column(7).Width = 20;
                                        worksheet.Column(8).Width = 20;
                                        worksheet.Column(9).Width = 20;
                                        worksheet.Column(10).Width = 20;
                                        worksheet.Column(11).Width = 20;
                                        worksheet.Column(12).Width = 20;
                                        worksheet.Column(13).Width = 20;
                                        worksheet.Column(14).Width = 20;
                                        worksheet.Column(15).Width = 20;
                                        worksheet.Column(16).Width = 20;
                                        worksheet.Column(17).Width = 20;
                                        worksheet.Column(18).Width = 20;
                                        worksheet.Column(19).Width = 20;
                                        worksheet.Column(20).Width = 20;
                                        worksheet.Column(21).Width = 20;
                                        package.Save();
                                        return true;
                                    }

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
            #endregion


            else
            {
                return false;
            }

        }



        public string BudgetTemp(string _fileSource, string _userID)
        {
            string _msg = "";
            try
            {
                DateTime _now = DateTime.Now;

                //delete data yang lama
                using (SqlConnection conn = new SqlConnection(Tools.conString))
                {
                    conn.Open();
                    using (SqlCommand cmd2 = conn.CreateCommand())
                    {
                        cmd2.CommandText = "truncate table BudgetTemp";
                        cmd2.ExecuteNonQuery();
                    }
                }

                // import data ke temp dulu
                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(Tools.conString, SqlBulkCopyOptions.TableLock))
                {
                    bulkCopy.DestinationTableName = "dbo.BudgetTemp";
                    bulkCopy.WriteToServer(CreateDataTableFromBudgetTempExcelFile(_fileSource));

                    _msg = "";
                }

                using (SqlConnection conn = new SqlConnection(Tools.conString))
                {
                    conn.Open();
                    using (SqlCommand cmd3 = conn.CreateCommand())
                    {

                        cmd3.CommandText =
                        @"  
                                        DECLARE @combinedString VARCHAR(MAX)
                                        Create Table #DescriptionTemp
                                        (ItemBudget nvarchar(100))
                        
                                        Insert Into #DescriptionTemp(ItemBudget)
                                        select ItemBudget from BudgetTemp A
                                        left join ItemBudget B on A.ItemBudget = B.Name and B.status in (1,2)
                                        where (B.Name is null or B.Name = '')

                                        If Exists(select ItemBudget from BudgetTemp A
                                        left join ItemBudget B on A.ItemBudget = B.Name and B.status in (1,2)
                                        where B.Name is null)

                                        BEGIN
                                        SELECT @combinedString = COALESCE(@combinedString + ', ', '') + ItemBudget
                                        FROM #DescriptionTemp
                                        SELECT 'Please Check Item Budget : ' + @combinedString as Result 
                                        END
                                        ELSE
                                        BEGIN
                                            delete Budget where DepartmentPK in (select DepartmentPK from Budgettemp a left join Department b on a.CostCenter = b.ID and b.Status in(1,2))
                                            and PeriodPK in (select PeriodPK from BudgetTemp a left join Period b on a.Period = b.id and b.Status in(1,2))
										    -- update Budget set Status = 3 where DepartmentPK in (select DepartmentPK from Budgettemp a left join Department b on a.CostCenter = b.ID and b.Status in(1,2))
										    --and Instrumentpk in (select AccountPK from Budgettemp a left join Instrument b on a.COANumber = b.id and b.Status in(1,2))

                                            Declare @BudgetPK int
                                            Declare @ReportPeriodPK int
                                            Declare @PeriodPK int
                                            Declare @DepartmentPK int
                                            Declare @AccountPK int
                                            Declare @ItemBudget nvarchar(50)
                                            Declare @Amount numeric(18,4)
                                            Declare @Jan numeric(18,4)
                                            Declare @Feb numeric(18,4)
                                            Declare @Mar numeric(18,4)
                                            Declare @Apr numeric(18,4)
                                            Declare @May numeric(18,4)
                                            Declare @Jun numeric(18,4)
                                            Declare @Jul numeric(18,4)
                                            Declare @Aug numeric(18,4)
                                            Declare @Sep numeric(18,4)
                                            Declare @Okt numeric(18,4)
                                            Declare @Nov numeric(18,4)
                                            Declare @Dec numeric(18,4)

                                            Declare A Cursor For
                                            select ab.PeriodPK,b.PeriodPK, d.DepartmentPK,e.AccountPK,f.ItemBudgetPK,Amount,Jan,Feb,Mar,Apr,May,
										    Jun,Jul,Aug,Sep,Okt,Nov,Dec from BudgetTemp A
                                            left join Period b on a.Period = b.ID and B.status in(1,2)
                                            left join Period ab on a.ReportPeriod = ab.ID and AB.status in(1,2)
										    left join Department d on a.CostCenter = d.ID and d.Status in(1,2)
										    left join Account e on a.COANumber = e.ID and e.Status in(1,2)
										    left join ItemBudget f on a.ItemBudget = f.name and f.Status in(1,2)
                                            
                                            Open A
                                            Fetch next From A
                                            into @ReportPeriodPK,
                                             @PeriodPK, 
                                             @DepartmentPK ,
                                             @AccountPK, 
                                             @ItemBudget, 
                                             @Amount ,
                                             @Jan ,
                                             @Feb ,
                                             @Mar ,
                                             @Apr ,
                                             @May ,
                                             @Jun ,
                                             @Jul ,
                                             @Aug ,
                                             @Sep ,
                                             @Okt ,
                                             @Nov ,
                                             @Dec 
                                            While @@Fetch_status = 0
                                            BEGIN
										    Select @BudgetPK = max(BudgetPK) + 1 from Budget
                                            set @BudgetPK = isnull(@BudgetPK,1)

                                            insert into 
                                            [dbo].[Budget](
										    [BudgetPK] ,
										    [HistoryPK] ,
										    [Status] ,
										    [Notes] ,
                                            [ReportPeriodPK] ,
										    [PeriodPK] ,
										    [DepartmentPK] ,
										    [AccountPK] ,
										    [ItemBudgetPK] ,
										    [Amount] ,
										    [January] ,
										    [February] ,
										    [March] ,
										    [April] ,
										    [May] ,
										    [June] ,
										    [July] ,
										    [August] ,
										    [September] ,
										    [October] ,
										    [November] ,
										    [December] ,
										    [EntryUsersID] ,
										    [EntryTime] ,
										    [ApprovedUsersID] ,
										    [ApprovedTime] ,
										    [LastUpdate] 
                                            )

                                            select @BudgetPK,1,2,'',
                                             @ReportPeriodPK,
                                             @PeriodPK, 
                                             @DepartmentPK ,
                                             @AccountPK, 
                                             @ItemBudget, 
                                             @Amount ,
                                             @Jan ,
                                             @Feb ,
                                             @Mar ,
                                             @Apr ,
                                             @May ,
                                             @Jun ,
                                             @Jul ,
                                             @Aug ,
                                             @Sep ,
                                             @Okt ,
                                             @Nov ,
                                             @Dec ,
                                             @EntryUsersID ,
                                             @LastUpdate ,
                                             @EntryUsersID ,
                                             @LastUpdate ,
                                             @LastUpdate  


                                            fetch next From A into 
                                             @ReportPeriodPK,
										     @PeriodPK, 
                                             @DepartmentPK ,
                                             @AccountPK, 
                                             @ItemBudget, 
                                             @Amount ,
                                             @Jan ,
                                             @Feb ,
                                             @Mar ,
                                             @Apr ,
                                             @May ,
                                             @Jun ,
                                             @Jul ,
                                             @Aug ,
                                             @Sep ,
                                             @Okt ,
                                             @Nov ,
                                             @Dec 
                                            end
                                            Close A
                                            Deallocate A

                                        select 'Success Import Budget' Result
                                        END

                                        
                                ";
                        cmd3.Parameters.AddWithValue("@EntryUsersID", _userID);
                        cmd3.Parameters.AddWithValue("@LastUpdate", _now);
                        using (SqlDataReader dr = cmd3.ExecuteReader())
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

        private DataTable CreateDataTableFromBudgetTempExcelFile(string _path)
        {
            try
            {
                using (DataTable dt = new DataTable())
                {
                    DataColumn dc;
                    DataRow dr;

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "No";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "ReportPeriod";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Period";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "CostCenter";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "COANumber";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "COAName";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "ItemBudget";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Decimal");
                    dc.ColumnName = "Amount";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Decimal");
                    dc.ColumnName = "Jan";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Decimal");
                    dc.ColumnName = "Feb";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Decimal");
                    dc.ColumnName = "Mar";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Decimal");
                    dc.ColumnName = "Apr";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Decimal");
                    dc.ColumnName = "May";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Decimal");
                    dc.ColumnName = "Jun";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Decimal");
                    dc.ColumnName = "Jul";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Decimal");
                    dc.ColumnName = "Aug";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Decimal");
                    dc.ColumnName = "Sep";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Decimal");
                    dc.ColumnName = "Okt";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Decimal");
                    dc.ColumnName = "Nov";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Decimal");
                    dc.ColumnName = "Dec";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    using (OleDbConnection odConnection = new OleDbConnection(Tools.ConStringExcel2007(_path)))
                    {
                        odConnection.Open();
                        using (OleDbCommand odCmd = odConnection.CreateCommand())
                        {
                            // _oldfilename = nama sheet yang ada di file excel yang diimport
                            odCmd.CommandText = "SELECT * FROM [Sheet1$]";
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

                                    dr["No"] = odRdr[0]; //odRdr[1];
                                    dr["ReportPeriod"] = odRdr[1];
                                    dr["Period"] = odRdr[2];
                                    dr["CostCenter"] = odRdr[3];
                                    dr["COANumber"] = odRdr[4];
                                    dr["COAName"] = odRdr[5];
                                    dr["ItemBudget"] = odRdr[6];
                                    dr["Amount"] = odRdr[7];
                                    dr["Jan"] = odRdr[8];
                                    dr["Feb"] = odRdr[9];
                                    dr["Mar"] = odRdr[10];
                                    dr["Apr"] = odRdr[11];
                                    dr["May"] = odRdr[12];
                                    dr["Jun"] = odRdr[13];
                                    dr["Jul"] = odRdr[14];
                                    dr["Aug"] = odRdr[15];
                                    dr["Sep"] = odRdr[16];
                                    dr["Okt"] = odRdr[17];
                                    dr["Nov"] = odRdr[18];
                                    dr["Dec"] = odRdr[19];
                                    //dr["SwitchOutUnit"] = Convert.ToDecimal(odRdr[21].ToString() == "" ? 0 : odRdr[21].Equals(DBNull.Value) == true ? 0 : odRdr[21]);

                                    if (dr["No"].Equals(DBNull.Value) != true) { dt.Rows.Add(dr); }
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



    }
}