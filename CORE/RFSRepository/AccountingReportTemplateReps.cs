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
    public class AccountingReportTemplateReps
    {
        Host _host = new Host();

        //1
        string _insertCommand = "INSERT INTO [dbo].[AccountingReportTemplate] " +
                            "([AccountingReportTemplatePK],[HistoryPK],[Status],[ReportName],[Row],[Column],[RowType],[SourceAccount],[Operator],";

        string _paramaterCommand = "@ReportName,@Row,@Column,@RowType,@SourceAccount,@Operator,";

        //2
        private AccountingReportTemplate setAccountingReportTemplate(SqlDataReader dr)
        {
            AccountingReportTemplate M_AccountingReportTemplate = new AccountingReportTemplate();
            M_AccountingReportTemplate.AccountingReportTemplatePK = Convert.ToInt32(dr["AccountingReportTemplatePK"]);
            M_AccountingReportTemplate.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_AccountingReportTemplate.Status = Convert.ToInt32(dr["Status"]);
            M_AccountingReportTemplate.StatusDesc = dr["StatusDesc"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["StatusDesc"]);
            M_AccountingReportTemplate.Notes = Convert.ToString(dr["Notes"]);

            M_AccountingReportTemplate.ReportName = dr["ReportName"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["ReportName"]);
            M_AccountingReportTemplate.Row = dr["Row"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["Row"]);
            M_AccountingReportTemplate.Column = dr["Column"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["Column"]);
            M_AccountingReportTemplate.RowType = dr["RowType"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["RowType"]);
            M_AccountingReportTemplate.SourceAccount = dr["SourceAccount"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["SourceAccount"]);
            M_AccountingReportTemplate.SourceAccountName = dr["SourceAccountName"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["SourceAccountName"]);
            M_AccountingReportTemplate.Operator = dr["Operator"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["Operator"]);
            
            M_AccountingReportTemplate.EntryUsersID = dr["EntryUsersID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["EntryUsersID"]);
            M_AccountingReportTemplate.UpdateUsersID = dr["UpdateUsersID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["UpdateUsersID"]);
            M_AccountingReportTemplate.ApprovedUsersID = dr["ApprovedUsersID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["ApprovedUsersID"]);
            M_AccountingReportTemplate.VoidUsersID = dr["VoidUsersID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["VoidUsersID"]);
            M_AccountingReportTemplate.EntryTime = dr["EntryTime"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["EntryTime"]);
            M_AccountingReportTemplate.UpdateTime = dr["UpdateTime"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["UpdateTime"]);
            M_AccountingReportTemplate.ApprovedTime = dr["ApprovedTime"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["ApprovedTime"]);
            M_AccountingReportTemplate.VoidTime = dr["VoidTime"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["VoidTime"]);
            M_AccountingReportTemplate.DBUserID = dr["DBUserID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["DBUserID"]);
            M_AccountingReportTemplate.DBTerminalID = dr["DBTerminalID"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["DBTerminalID"]);
            M_AccountingReportTemplate.LastUpdate = dr["LastUpdate"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["LastUpdate"]);
            M_AccountingReportTemplate.LastUpdateDB = dr["LastUpdateDB"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["LastUpdateDB"]);
            return M_AccountingReportTemplate;
        }

        //3
        public List<AccountingReportTemplate> AccountingReportTemplate_Select(int _status)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<AccountingReportTemplate> L_AccountingReportTemplate = new List<AccountingReportTemplate>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText =
                                @"
                                    select case when a.Status = 1 then 'PENDING' else case When a.Status = 2 then 'APPROVED' else case when a.Status = 3 then 'VOID' else 'WAITING' end end end as StatusDesc, b.Name as SourceAccountName, a.*
                                    from AccountingReportTemplate a
	                                    left join Account b on a.SourceAccount = b.AccountPK and b.[Status] = 2
                                    where a.[Status] = @Status
                                    order by a.ReportName
                                ";
                            cmd.Parameters.AddWithValue("@Status", _status);
                        }
                        else
                        {
                            cmd.CommandText =
                                @"
                                    select case when a.Status = 1 then 'PENDING' else case When a.Status = 2 then 'APPROVED' else case when a.Status = 3 then 'VOID' else 'WAITING' end end end as StatusDesc, b.Name as SourceAccountName, a.*
                                    from AccountingReportTemplate a
	                                    left join Account b on a.SourceAccount = b.AccountPK and b.[Status] = 2
                                    order by a.ReportName
                                ";
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_AccountingReportTemplate.Add(setAccountingReportTemplate(dr));
                                }
                            }
                            return L_AccountingReportTemplate;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        //4
        public AccountingReportTemplate AccountingReportTemplate_SelectByAccountingReportTemplatePK(int _AccountingReportTemplatePK)
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
                                select b.Name as SourceAccountName, a.*
                                from AccountingReportTemplate a
	                                left join Account b on a.SourceAccount = b.AccountPK and b.[Status] = 2
                                where a.AccountingReportTemplatePK = @AccountingReportTemplatePK
                                order by a.ReportName
                            ";
                        cmd.Parameters.AddWithValue("@AccountingReportTemplatePK", _AccountingReportTemplatePK);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return setAccountingReportTemplate(dr);
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

        //5
        public int AccountingReportTemplate_Add(AccountingReportTemplate _AccountingReportTemplate, bool _havePrivillege)
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
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[ApprovedUsersID],[ApprovedTime],[LastUpdate])" +
                                 "select isnull(max(AccountingReportTemplatePK),0) + 1,1,@Status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@LastUpdate from AccountingReportTemplate";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _AccountingReportTemplate.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);

                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[LastUpdate])" +
                                "select isnull(max(AccountingReportTemplatePK),0) + 1,1,@Status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@LastUpdate from AccountingReportTemplate";
                        }
                        cmd.Parameters.AddWithValue("@Status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@ReportName", _AccountingReportTemplate.ReportName);
                        cmd.Parameters.AddWithValue("@Row", _AccountingReportTemplate.Row);
                        cmd.Parameters.AddWithValue("@Column", _AccountingReportTemplate.Column);
                        cmd.Parameters.AddWithValue("@RowType", _AccountingReportTemplate.RowType);
                        cmd.Parameters.AddWithValue("@SourceAccount", _AccountingReportTemplate.SourceAccount);
                        cmd.Parameters.AddWithValue("@Operator", _AccountingReportTemplate.Operator);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _AccountingReportTemplate.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();

                        return _host.Get_LastPKByLastUpate(_datetimeNow, "AccountingReportTemplate");
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }
        
        //6
        public int AccountingReportTemplate_Update(AccountingReportTemplate _AccountingReportTemplate, bool _havePrivillege)
        {
            try
            {
                int _newHisPK;
                int status = _host.Get_Status(_AccountingReportTemplate.AccountingReportTemplatePK, _AccountingReportTemplate.HistoryPK, "AccountingReportTemplate");
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {

                            cmd.CommandText = 
                                @"
                                    update AccountingReportTemplate set Status = 2, Notes = @Notes,
                                        ReportName = @ReportName, [Row] = @Row, [Column] = @Column, RowType = @RowType, SourceAccount = @SourceAccount, Operator = @Operator,
                                        ApprovedUsersID = @ApprovedUsersID, ApprovedTime = @ApprovedTime, UpdateUsersID = @UpdateUsersID, Updatetime = @Updatetime, LastUpdate = @LastUpdate
                                    where AccountingReportTemplatePK = @PK and HistoryPK = @HistoryPK
                                ";
                            cmd.Parameters.AddWithValue("@HistoryPK", _AccountingReportTemplate.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _AccountingReportTemplate.AccountingReportTemplatePK);
                            cmd.Parameters.AddWithValue("@Notes", _AccountingReportTemplate.Notes);
                            cmd.Parameters.AddWithValue("@ReportName", _AccountingReportTemplate.ReportName);
                            cmd.Parameters.AddWithValue("@Row", _AccountingReportTemplate.Row);
                            cmd.Parameters.AddWithValue("@Column", _AccountingReportTemplate.Column);
                            cmd.Parameters.AddWithValue("@RowType", _AccountingReportTemplate.RowType);
                            cmd.Parameters.AddWithValue("@SourceAccount", _AccountingReportTemplate.SourceAccount);
                            cmd.Parameters.AddWithValue("@Operator", _AccountingReportTemplate.Operator);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _AccountingReportTemplate.UpdateUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@EntryTime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                            cmd.ExecuteNonQuery();
                        }

                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = 
                                @"
                                    update AccountingReportTemplate set Status = 3, VoidUsersID = @VoidUsersID, VoidTime = @VoidTime, LastUpdate = @LastUpdate 
                                    where AccountingReportTemplatePK = @PK and Status = 4
                                ";
                            cmd.Parameters.AddWithValue("@PK", _AccountingReportTemplate.AccountingReportTemplatePK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _AccountingReportTemplate.UpdateUsersID);
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
                                cmd.CommandText =
                                    @"
                                        update AccountingReportTemplate set Notes = @Notes,
                                            ReportName = @ReportName, [Row] = @Row, [Column] = @Column, RowType = @RowType, SourceAccount = @SourceAccount, Operator = @Operator,
                                            UpdateUsersID = @UpdateUsersID, Updatetime = @Updatetime, LastUpdate = @LastUpdate
                                        where AccountingReportTemplatePK = @PK and HistoryPK = @HistoryPK
                                    ";
                                cmd.Parameters.AddWithValue("@HistoryPK", _AccountingReportTemplate.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _AccountingReportTemplate.AccountingReportTemplatePK);
                                cmd.Parameters.AddWithValue("@Notes", _AccountingReportTemplate.Notes);
                                cmd.Parameters.AddWithValue("@ReportName", _AccountingReportTemplate.ReportName);
                                cmd.Parameters.AddWithValue("@Row", _AccountingReportTemplate.Row);
                                cmd.Parameters.AddWithValue("@Column", _AccountingReportTemplate.Column);
                                cmd.Parameters.AddWithValue("@RowType", _AccountingReportTemplate.RowType);
                                cmd.Parameters.AddWithValue("@SourceAccount", _AccountingReportTemplate.SourceAccount);
                                cmd.Parameters.AddWithValue("@Operator", _AccountingReportTemplate.Operator);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _AccountingReportTemplate.UpdateUsersID);
                                cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@EntryTime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                                cmd.ExecuteNonQuery();
                            }
                            return 0;
                        }
                        else
                        {
                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                _newHisPK = _host.Get_NewHistoryPK(_AccountingReportTemplate.AccountingReportTemplatePK, "AccountingReportTemplate");

                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                "select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "from AccountingReportTemplate where AccountingReportTemplatePK = @PK and HistoryPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _AccountingReportTemplate.AccountingReportTemplatePK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _AccountingReportTemplate.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@ReportName", _AccountingReportTemplate.ReportName);
                                cmd.Parameters.AddWithValue("@Row", _AccountingReportTemplate.Row);
                                cmd.Parameters.AddWithValue("@Column", _AccountingReportTemplate.Column);
                                cmd.Parameters.AddWithValue("@RowType", _AccountingReportTemplate.RowType);
                                cmd.Parameters.AddWithValue("@SourceAccount", _AccountingReportTemplate.SourceAccount);
                                cmd.Parameters.AddWithValue("@Operator", _AccountingReportTemplate.Operator);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _AccountingReportTemplate.UpdateUsersID);
                                cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@EntryTime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText =
                                    @"
                                        update AccountingReportTemplate set 
                                            Status = 4, Notes = @Notes,
                                            UpdateUsersID = @UpdateUsersID, Updatetime = @Updatetime, LastUpdate = @LastUpdate 
                                        where AccountingReportTemplatePK = @PK and HistoryPK = @HistoryPK
                                    ";
                                cmd.Parameters.AddWithValue("@Notes", _AccountingReportTemplate.Notes);
                                cmd.Parameters.AddWithValue("@PK", _AccountingReportTemplate.AccountingReportTemplatePK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _AccountingReportTemplate.HistoryPK);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _AccountingReportTemplate.UpdateUsersID);
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
        public void AccountingReportTemplate_Approved(AccountingReportTemplate _AccountingReportTemplate)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = 
                            @"
                                update AccountingReportTemplate set Status = 2, ApprovedUsersID = @ApprovedUsersID, ApprovedTime = @ApprovedTime, LastUpdate = @LastUpdate
                                where AccountingReportTemplatePK = @PK and HistoryPK = @HistoryPK
                            ";
                        cmd.Parameters.AddWithValue("@PK", _AccountingReportTemplate.AccountingReportTemplatePK);
                        cmd.Parameters.AddWithValue("@HistoryPK", _AccountingReportTemplate.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _AccountingReportTemplate.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = 
                            @"
                                update AccountingReportTemplate set Status = 3, VoidUsersID = @VoidUsersID, VoidTime = @VoidTime, LastUpdate = @LastUpdate 
                                where AccountingReportTemplatePK = @PK and Status = 4
                            ";
                        cmd.Parameters.AddWithValue("@PK", _AccountingReportTemplate.AccountingReportTemplatePK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _AccountingReportTemplate.ApprovedUsersID);
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

        //8
        public void AccountingReportTemplate_Reject(AccountingReportTemplate _AccountingReportTemplate)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = 
                            @"
                                update AccountingReportTemplate set Status = 3,VoidUsersID = @VoidUsersID, VoidTime = @VoidTime, LastUpdate = @LastUpdate
                                where AccountingReportTemplatePK = @PK and HistoryPK = @HistoryPK
                            ";
                        cmd.Parameters.AddWithValue("@PK", _AccountingReportTemplate.AccountingReportTemplatePK);
                        cmd.Parameters.AddWithValue("@HistoryPK", _AccountingReportTemplate.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _AccountingReportTemplate.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = 
                            @"
                                update AccountingReportTemplate set Status = 2, LastUpdate = @LastUpdate 
                                where AccountingReportTemplatePK = @PK and Status = 4
                            ";
                        cmd.Parameters.AddWithValue("@PK", _AccountingReportTemplate.AccountingReportTemplatePK);
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
        
        //9
        public void AccountingReportTemplate_Void(AccountingReportTemplate _AccountingReportTemplate)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = 
                            @"
                                update AccountingReportTemplate set Status = 3, VoidUsersID = @VoidUsersID, VoidTime = @VoidTime, LastUpdate = @LastUpdate
                                where AccountingReportTemplatePK = @PK and HistoryPK = @HistoryPK
                            ";
                        cmd.Parameters.AddWithValue("@PK", _AccountingReportTemplate.AccountingReportTemplatePK);
                        cmd.Parameters.AddWithValue("@HistoryPK", _AccountingReportTemplate.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _AccountingReportTemplate.VoidUsersID);
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

        //10
        public string AccountingReportTemplate_CopyRecord(AccountingReportTemplate _AccountingReportTemplate)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText =
                            @"
                                if exists (
	                                select *
                                    from AccountingReportTemplate
                                    where AccountingReportTemplatePK = @RecordFrom and SourceAccount = @NewSourceAccount and [Status] in (1,2)
                                )
                                begin
	                                select 'Data Already Exsist!' as ResultMsg
                                end
                                else
                                begin
                                    declare @MaxPK          int,
                                            @ReportName     nvarchar(500),
                                            @Row            int,
                                            @Column         int,
                                            @RowType        nvarchar(50),
                                            @Operator       nvarchar(1)

                                    select @MaxPK = isnull(max(AccountingReportTemplatePK), 0) + 1
                                    from AccountingReportTemplate

                                    select 
                                        @ReportName = isnull(ReportName, ''), @Row = isnull([Row], 0), @Column = isnull([Column], 0), 
                                        @RowType = isnull(RowType, ''), @Operator = isnull(Operator, '')
                                    from AccountingReportTemplate
                                    where AccountingReportTemplatePK = @RecordFrom and [Status] in (2)

                                    insert into AccountingReportTemplate (
	                                    AccountingReportTemplatePK, HistoryPK, [Status], Notes, 
                                        ReportName, [Row], [Column], RowType, SourceAccount, Operator, 
                                        EntryUsersID, EntryTime, LastUpdate
                                    )
                                    select 
	                                    @MaxPK as AccountingReportTemplatePK, 1 as HistoryPK, @Status as [Status], 'Copy Record' as Notes, 
                                        @ReportName as ReportName, @Row as [Row], @Column as [Column], 
                                        @RowType as RowType, @NewSourceAccount as SourceAccount, @Operator as Operator, 
                                        @EntryUsersID, @EntryTime, @LastUpdate

	                                select 'Copy Record Success' as ResultMsg
                                end                                
                            ";
                        cmd.Parameters.AddWithValue("@Status", 1);
                        cmd.Parameters.AddWithValue("@RecordFrom", _AccountingReportTemplate.RecordFrom);
                        cmd.Parameters.AddWithValue("@NewSourceAccount", _AccountingReportTemplate.NewSourceAccount);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _AccountingReportTemplate.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (!dr.HasRows)
                            {
                                return "Result Msg Not Found!";
                            }
                            else
                            {
                                dr.Read();
                                return Convert.ToString(dr["ResultMsg"]);
                            }
                        }
                    }
                }
            }
            catch (Exception err)
            {
                return "Copy Record Canceled! Error : " + err.Message.ToString();
                throw err;
            }
        }
        
        //AREA LAIN-LAIN dimulai dari sini ( untuk function diluar standart )

        public bool AccountingReportTemplate_CheckDataExists(AccountingReportTemplate _AccountingReportTemplate, string _actions)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_actions == "UPDATE")
                        {
                            cmd.CommandText =
                                @"
                                    if exists (
	                                    select *
	                                    from AccountingReportTemplate
	                                    where [Status] in (1,2) --and ReportName = @ReportName and 
                                            and [Row] = @Row and [Column] = @Column 
                                            and RowType = @RowType and SourceAccount = @SourceAccount and Operator = @Operator
                                    )
                                    begin
	                                    select 1 as BitExists
                                    end
                                    else
                                    begin
	                                    select 0 as BitExists
                                    end
                                ";
                        }
                        else
                        {
                            cmd.CommandText =
                                @"
                                    if exists (
	                                    select *
	                                    from AccountingReportTemplate
	                                    where [Status] in (1,2) and ReportName = @ReportName and [Row] = @Row and [Column] = @Column 
                                            and RowType = @RowType and SourceAccount = @SourceAccount and Operator = @Operator
                                    )
                                    begin
	                                    select 1 as BitExists
                                    end
                                    else
                                    begin
	                                    select 0 as BitExists
                                    end
                                ";
                        }
                        cmd.Parameters.AddWithValue("@ReportName", _AccountingReportTemplate.ReportName);
                        cmd.Parameters.AddWithValue("@Row", _AccountingReportTemplate.Row);
                        cmd.Parameters.AddWithValue("@Column", _AccountingReportTemplate.Column);
                        cmd.Parameters.AddWithValue("@RowType", _AccountingReportTemplate.RowType);
                        cmd.Parameters.AddWithValue("@SourceAccount", _AccountingReportTemplate.SourceAccount);
                        cmd.Parameters.AddWithValue("@Operator", _AccountingReportTemplate.Operator);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (!dr.HasRows)
                            {
                                return false;
                            }
                            else
                            {
                                dr.Read();
                                return Convert.ToBoolean(dr["BitExists"]);
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

        public bool AccountingReportTemplate_CheckReportNameExists(AccountingReportTemplate _AccountingReportTemplate)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText =
                            @"
                                if exists (
	                                select *
	                                from AccountingReportTemplate
	                                where ReportName = @ReportName and [Status] in (1,2)
                                )
                                begin
	                                select 1 as BitExists
                                end
                                else
                                begin
	                                select 0 as BitExists
                                end
                            ";
                        cmd.Parameters.AddWithValue("@ReportName", _AccountingReportTemplate.ReportName);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (!dr.HasRows)
                            {
                                return false;
                            }
                            else
                            {
                                dr.Read();
                                return Convert.ToBoolean(dr["BitExists"]);
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

        public List<ReportNameCombo> GetReportName_Combo()
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<ReportNameCombo> L_Data = new List<ReportNameCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText =
                            @"
                                select distinct AccountingReportTemplatePK, HistoryPK, ReportName
                                from AccountingReportTemplate
                                where [Status] in (2)
                                order by ReportName asc
                            ";
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    ReportNameCombo M_Data = new ReportNameCombo();
                                    M_Data.AccountingReportTemplatePK = Convert.ToInt32(dr["AccountingReportTemplatePK"]);
                                    M_Data.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
                                    M_Data.ReportName = Convert.ToString(dr["ReportName"]);
                                    L_Data.Add(M_Data);
                                }
                            }
                            return L_Data;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }
        
        public List<RecordFromCombo> GetRecordFrom_Combo()
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<RecordFromCombo> L_Data = new List<RecordFromCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText =
                            @"
                                select distinct a.AccountingReportTemplatePK, a.HistoryPK, 
	                                ' SysNo : ' + cast(a.AccountingReportTemplatePK as nvarchar(10)) + 
	                                ' - Report Name : ' + a.ReportName + 
	                                ' - Row : '  + cast(a.[Row] as nvarchar(10)) + 
	                                ' - Column : '  + cast(a.[Column] as nvarchar(10)) +
	                                ' - Row Type : '  + cast(a.[RowType] as nvarchar(20)) as RecordFrom
                                from AccountingReportTemplate a
	                                left join Account b on a.SourceAccount = b.AccountPK and b.[Status] = 2
                                where a.[Status] in (2)
                                order by RecordFrom asc
                            ";
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    RecordFromCombo M_Data = new RecordFromCombo();
                                    M_Data.AccountingReportTemplatePK = Convert.ToInt32(dr["AccountingReportTemplatePK"]);
                                    M_Data.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
                                    M_Data.RecordFrom = Convert.ToString(dr["RecordFrom"]);
                                    L_Data.Add(M_Data);
                                }
                            }
                            return L_Data;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }
        
        public AccountingReportTemplate AccountingReportTemplate_SelectByPK(int _AccountingReportTemplatePK)
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
                                select b.Name as SourceAccountName, a.*
                                from AccountingReportTemplate a
	                                left join Account b on a.SourceAccount = b.AccountPK and b.[Status] = 2
                                where a.AccountingReportTemplatePK = @AccountingReportTemplatePK
                                order by a.ReportName
                            ";
                        cmd.Parameters.AddWithValue("@AccountingReportTemplatePK", _AccountingReportTemplatePK);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return setAccountingReportTemplate(dr);
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

        public AccountingReportTemplate AccountingReportTemplate_SelectByAccountingReportTemplateID(string _AccountingReportTemplateID)
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
                                select b.Name as SourceAccountName, a.*
                                from AccountingReportTemplate a
	                                left join Account b on a.SourceAccount = b.AccountPK and b.[Status] = 2
                                where a.[Status] = 2
                                order by a.ReportName
                            ";
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return setAccountingReportTemplate(dr);
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

    }
}