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
    public class UnitReconcileReps
    {
        Host _host = new Host();
        static readonly UnitReconcileReps _unitReconcileTempReps = new UnitReconcileReps();

        private UnitReconcile setUnitReconcile(SqlDataReader dr)
        {
            UnitReconcile M_Model = new UnitReconcile();
            M_Model.UnitReconcilePK = Convert.ToInt32(dr["UnitReconcilePK"]);
            M_Model.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_Model.Status = Convert.ToInt32(dr["Status"]);
            M_Model.Selected = Convert.ToBoolean(dr["Selected"]);
            M_Model.ValueDate = Convert.ToDateTime(dr["ValueDate"]);
            M_Model.FundID = dr["FundID"].ToString();
            M_Model.FundClientName = dr["FundClientName"].ToString();
            M_Model.UnitSystem = Convert.ToDecimal(dr["UnitSystem"]);
            M_Model.UnitSInvest = Convert.ToDecimal(dr["UnitSInvest"]);
            M_Model.Description = dr["Description"].ToString();
            M_Model.Difference = Convert.ToDecimal(dr["Difference"]);
            M_Model.AdjustmentUnit = Convert.ToDecimal(dr["AdjustmentUnit"]);
            return M_Model;
        }

        public List<UnitReconcile> UnitReconcile_SelectUnitReconcileDateFromTo(DateTime _dateFrom, DateTime _dateTo)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<UnitReconcile> L_Model = new List<UnitReconcile>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"select UnitReconcilePK,A.HistoryPK,A.Status,A.Selected,A.ValueDate,A.FundID,A.FundClientName,A.UnitSystem,A.UnitSInvest,A.Description,A.Difference,A.AdjustmentUnit from UnitReconcile A
                            left join Fund B on A.FundID = B.ID and B.status in (1,2)
                            where  ValueDate between @DateFrom and @DateTo and Difference <> 0 and selected = 1 and B.bitNeedRecon = 1
                            group by UnitReconcilePK,A.HistoryPK,A.Status,A.Selected,A.ValueDate,A.FundID,A.FundClientName,A.UnitSystem,A.UnitSInvest,A.Description,A.Difference,A.AdjustmentUnit order by A.Difference asc ";
                            cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                            cmd.Parameters.AddWithValue("@DateTo", _dateTo);
                      
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_Model.Add(setUnitReconcile(dr));
                                }
                            }
                            return L_Model;
                        }
                    }
                }

            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public string UnitReconcileTempImport(string _fileSource, string _userID)
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
                        cmd2.CommandText = "truncate table UnitReconcileTemp";
                        cmd2.ExecuteNonQuery();
                    }
                }

                // import data ke temp dulu
                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(Tools.conString, SqlBulkCopyOptions.TableLock))
                {
                    bulkCopy.DestinationTableName = "dbo.UnitReconcileTemp";
                    bulkCopy.WriteToServer(CreateDataTableFromUnitReconcileExcelFile(_fileSource));

                    _msg = "";
                }


                return _msg;
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        private DataTable CreateDataTableFromUnitReconcileExcelFile(string _path)
        {
            try
            {
                using (DataTable dt = new DataTable())
                {
                    DataColumn dc;
                    DataRow dr;

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Int32");
                    dc.ColumnName = "UnitReconcilePK";
                    dc.Unique = false;
                    dc.AutoIncrement = true;
                    dc.AutoIncrementSeed = 1;
                    dc.AutoIncrementStep = 1;
                    dt.Columns.Add(dc);


                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.DateTime");
                    dc.ColumnName = "ValueDate";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "IFUACode";
                    dc.Unique = false;
                    dt.Columns.Add(dc);


                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "FundCode";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "CurrencyID";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "SACode";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Decimal");
                    dc.ColumnName = "UnitBalance";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Decimal");
                    dc.ColumnName = "NAV";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Decimal");
                    dc.ColumnName = "AmountBalance";
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
                                    string _valueDate = Convert.ToString(odRdr[1]);
                                    if (!string.IsNullOrEmpty(_valueDate))
                                    {
                                        string _tgl = _valueDate.Substring(6, 2);
                                        string _bln = _valueDate.Substring(4, 2);
                                        string _thn = _valueDate.Substring(0, 4);

                                        _valueDate = _bln + "/" + _tgl + "/" + _thn; //Convert.ToDateTime(_strTransactionDate).ToString("MM/dd/yyyy");
                                    }


                                    dr = dt.NewRow();
                                    dr["ValueDate"] = _valueDate;
                                    dr["IFUACode"] = odRdr[2];
                                    dr["FundCode"] = odRdr[5];
                                    dr["CurrencyID"] = odRdr[7];
                                    dr["SACode"] = odRdr[12];
                                    dr["UnitBalance"] = Convert.ToDecimal(odRdr[14].ToString() == "" ? 0 : odRdr[14].Equals(DBNull.Value) == true ? 0 : odRdr[14]);
                                    dr["NAV"] = Convert.ToDecimal(odRdr[15].ToString() == "" ? 0 : odRdr[15].Equals(DBNull.Value) == true ? 0 : odRdr[15]);
                                    dr["AmountBalance"] = Convert.ToDecimal(odRdr[16].ToString() == "" ? 0 : odRdr[16].Equals(DBNull.Value) == true ? 0 : odRdr[16]);
                                    //13

                                    //dr["Amount"] = (odRdr[3].ToString().Replace(",", ""));

                                    if (dr["UnitReconcilePK"].Equals(DBNull.Value) != true) { dt.Rows.Add(dr); }
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

        public List<UnitReconcile> UnitReconcile_GenerateByDateFromTo(DateTime _dateFrom, DateTime _dateTo)
        {
            try
            {

                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<UnitReconcile> L_Model = new List<UnitReconcile>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = @"        
                        truncate table UnitReconcile
                        Insert into UnitReconcile (HistoryPK,Status,Selected,ValueDate,FundCode,
	                    FundID,IFUACode,FundClientName,SACode,UnitSystem,UnitSInvest,[Description],[Difference],AdjustmentUnit) 

                        Select 1,1,0, ValueDate,A.FundCode FundCode,B.ID FundID,A.IFUACode IFUACode,
	                    isnull(C.Name,'NO CLIENT') FundClientName,A.SACode,
	                    sum(isnull(UnitAmount,0)) UnitSystem,sum(isnull(UnitBalance,0)) UnitSInvest,
	                    'Rounding Unit',sum(isnull(UnitBalance,0)-isnull(UnitAmount,0)),0 from UnitReconcileTemp A 
                        left join Fund B on A.FundCode = B.SInvestCode and B.status in (1,2)
                        left join FundClient C on A.IFUACode = C.IFUACode and C.status in (1,2)
                        left join FundClientPosition E on B.FundPK = E.FundPK and C.FundClientPK = E.FundClientPK
	                    and E.Date = A.ValueDate
                        where A.SACode = @CompanyID 
   
                        group by ValueDate,A.FundCode,B.ID,A.IFUACode,C.Name,UnitAmount,A.SACode
                        having sum(isnull(UnitBalance,0)-isnull(UnitAmount,0)) <> 0
	
                        UNION ALL

                        select 1,1,0,ValueDate,H.FundCode,H.FundID,H.SACode,
	                    isnull(H.FundClientName,'NO CLIENT IN SYSTEM PLEASE CHECK'),H.SACode,isnull(I.UnitAmount,0) UnitAmount,H.UnitSInvest,
	                    'Rounding Unit',sum(isnull(H.UnitSInvest,0)-isnull(I.UnitAmount,0)),0 from
                        (Select B.FundPK,C.FundClientPK,ValueDate,A.FundCode FundCode,B.ID FundID,A.SACode IFUACode,C.Name FundClientName,A.SACode,sum(UnitBalance) UnitSInvest from UnitReconcileTemp A
                        left join Fund B on A.FundCode = B.SInvestCode and B.status in (1,2)
                        left join FundClient C on A.SACode = C.SACode and C.status in (1,2)
                        left join FundClientPosition E on B.FundPK = E.FundPK and C.FundClientPK = E.FundClientPK
	                    and E.Date = A.ValueDate
                        where A.SACode <> @CompanyID 
                        group by B.FundPK,C.FundClientPK,ValueDate,A.FundCode,B.ID,A.SACode,C.Name)
                        H left join
                        (
                        select G.SACode,F.FundClientPK,F.FundPK,F.UnitAmount from FundClientPosition F 
                        left join FundClient G on F.FundClientPK = G.FundClientPK and G.status in (1,2)
                        left join Fund Z on Z.FundPK = F.FundPK and Z.status in (1,2)
	                    where F.Date = @DateFrom
                        ) I on H.FundClientPK = I.FundClientPK and H.FundPK = I.FundPK
                        group by ValueDate,H.FundCode,H.FundID,H.SACode,H.FundClientName,I.UnitAmount,H.UnitSInvest

	                    UNION ALL

	                    select 1,1,0,ValueDate,A.FundCode,A.FundID,A.IFUACode,
	                    isnull(A.FundClientName,'NO CLIENT IN S-INVEST PLEASE CHECK'),SACode,sum(isnull(A.UnitAmount,0)) UnitAmount,
	                    sum(A.UnitSInvest),
	                    'Rounding Unit',sum(isnull(A.UnitSInvest,0)-isnull(A.UnitAmount,0)),0 from
                        (
	                  		   
						 Select  B.FundPK,A.FundClientPK,A.Date ValueDate,B.SInvestCode FundCode,
	                    B.ID FundID,isnull(C.IFUACode,'') IFUACode,C.Name FundClientName,C.SACode,A.UnitAmount unitAmount,
	                    0 UnitSInvest from FundClientPosition A
                        left join Fund B on A.FundPK = B.FundPK and B.status in (1,2)
                        left join FundClient C on A.FundClientPK = C.FundClientPK and C.status in (1,2)
	                    left join UnitReconcileTemp D on   C.IFUACode = D.IFUACode and  B.SInvestCode = D.FundCode
		                    where 
							 (D.IFUACode is null or D.FundCode is null)  and 
							 A.Date = @DateFrom  and A.UnitAmount <> 0 and isnull(C.SACode,'') = ''
						
	                    )A 
	                    group by ValueDate,FundCode,FundID,FundClientName,IFUACode,SACode

	                    UNION ALL
	                    select 1,1,0,ValueDate,A.FundCode,A.FundID,SACode,
	                    isnull(A.FundClientName,'NO CLIENT IN S-INVEST PLEASE CHECK'),A.SACode,sum(isnull(A.UnitAmount,0)) UnitAmount,
	                    sum(isnull(A.UnitSInvest,0)),
	                    'Rounding Unit',sum(isnull(A.UnitSInvest,0)-isnull(A.UnitAmount,0)),0 from
                        (
	                    Select B.FundPK,A.FundClientPK,A.Date ValueDate,B.SInvestCode FundCode,
	                    B.ID FundID,C.IFUACode IFUACode,C.Name FundClientName,C.SACode,A.UnitAmount unitAmount,
	                    D.Unit UnitSInvest from FundClientPosition A
                        left join Fund B on A.FundPK = B.FundPK and B.status in (1,2)
                        left join FundClient C on A.FundClientPK = C.FundClientPK and C.status in (1,2)
	                    left join(
		                    Select SACode,FundCode,Sum(UnitBalance) Unit from UnitReconcileTemp
		                    where SACode <> @CompanyID
		                    group by SACode,FundCode
	                    )D on C.SACode = D.SACode and B.SInvestCode = D.FundCode --and A.UnitAmount = D.Unit
	                    Where A.Date = @DateFrom and C.SACode <> ''
	                    )A 

	                    group by ValueDate,FundCode,FundID,SACode,FundClientName

                       create table #UnitReconcile
                       (

	                    HistoryPK int,
	                    Status int,
	                    Selected bit,
	                    ValueDate datetime,
	                    FundCode nvarchar(100),
	                    FundID nvarchar(100),
	                    IFUACode nvarchar(100),
	                    FundClientName nvarchar(100),
	                    SACode nvarchar(100),
	                    UnitSystem numeric(22,8),
	                    UnitSInvest numeric(22,8),
	                    [Description] nvarchar(2000),
	                    [Difference] numeric(22,8),
	                    AdjustmentUnit numeric(22,8)
                       )
    
	
	                    Insert into #UnitReconcile (HistoryPK,Status,Selected,ValueDate,FundCode,
	                    FundID,IFUACode,FundClientName,SACode,UnitSystem,UnitSInvest,[Description],[Difference],AdjustmentUnit) 
    
	                    Select distinct HistoryPK,Status,Selected,ValueDate,FundCode,FundID,IFUACode,FundClientName,SACode,UnitSystem,UnitSInvest,[Description],[Difference],AdjustmentUnit from UnitReconcile
                        where  ValueDate between @DateFrom and @DateTo  and difference <> 0
                       -- and Difference > -1 and difference < 1 
                       -- and  Difference <= -0.001 or  Difference >= 0.001
                        order by Difference asc

	                    truncate table UnitReconcile
	                    Insert into UnitReconcile (HistoryPK,Status,Selected,ValueDate,FundCode,
	                    FundID,IFUACode,FundClientName,SACode,UnitSystem,UnitSInvest,[Description],[Difference],AdjustmentUnit) 
	                    select A.* from #UnitReconcile A 
                        left join Fund B on A.FundCode COLLATE DATABASE_DEFAULT = B.SInvestCode COLLATE DATABASE_DEFAULT and B.status in (1,2)
                        where B.BitNeedRecon = 1 and B.SInvestCode <> ''

	                    select * from UnitReconcile A
                        left join Fund B on A.FundCode = B.SInvestCode and B.status in (1,2)
                        where B.BitNeedRecon = 1
                        ";

                        cmd.CommandTimeout = 0;
                        cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _dateTo);
                        cmd.Parameters.AddWithValue("@CompanyID", _host.Get_CompanyID());
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_Model.Add(setUnitReconcile(dr));
                                }
                            }
                            return L_Model;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }
        public int UnitReconcile_Update(UnitReconcile _unitReconcile, bool _havePrivillege)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();

                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update UnitReconcile set Notes=@Notes,Description=@Description,UnitSystem=UnitSystem + @AdjustmentUnit,AdjustmentUnit = @AdjustmentUnit," +
                            "EntryUsersID=@EntryUsersID,EntryTime=@EntryTime,ApprovedUsersID=@EntryUsersID,ApprovedTime=@EntryTime,LastUpdate=@LastUpdate " +
                            "where UnitReconcilePK = @PK";
                        cmd.Parameters.AddWithValue("@PK", _unitReconcile.UnitReconcilePK);
                        cmd.Parameters.AddWithValue("@Notes", _unitReconcile.Notes);
                        cmd.Parameters.AddWithValue("@Description", _unitReconcile.Description);
                        cmd.Parameters.AddWithValue("@AdjustmentUnit", _unitReconcile.AdjustmentUnit);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _unitReconcile.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    return 0;

                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }
        public void UnitReconcile_PostingBySelected(string _usersID, string _permissionID)
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
                        @"declare @FundClientPK int
                        declare @ValueDate DateTime
                        declare @FundPK int
                        declare @AgentPK int
                        declare @AdjustmentUnit numeric(22,8)
                        declare @IFUACode nvarchar(50)
                        declare @FundCode nvarchar(50)
                        declare @SACode nvarchar(50)
                    


                        DECLARE A CURSOR FOR 
                    
                        select ValueDate,A.SACode,A.IFUACode,A.FundCode,FundClientPK,FundPK,0 agentPK,AdjustmentUnit from UnitReconcile A
                        left join FundClient B on A.IFUACode  = B.IFUACode and B.status  = 2
                        left join Fund C on A.FundCode = C.SInvestCode and C.status  = 2 
                        where A.status  = 1 and A.Selected  = 1 and A.adjustmentUnit <> 0
                        and isnull(A.IFUACode,'') <> '' and A.SACode = @CompanyID and FundClientPK is not null
                        union All
                        select ValueDate,A.SACode,A.IFUACode,A.FundCode,FundClientPK,FundPK,isnull(AgentPK,0),AdjustmentUnit from UnitReconcile A
                        left join FundClient B on isnull(A.SACode,'')  = B.SACode and B.status  = 2
                        left join Fund C on A.FundCode = C.SInvestCode and C.status  = 2
                        left join Agent D on B.SellingAgentPK = D.AgentPK and D.status  = 2
                        where A.status  = 1 and A.Selected  = 1 and isnull(A.SACode,'') <> @CompanyID and adjustmentUnit <> 0
                        and isnull(A.SACode,'') <> ''
                        and FundClientPK is not null
                        union All
                        select ValueDate,A.SACode,A.IFUACode,A.FundCode,FundClientPK,FundPK,isnull(AgentPK,0),AdjustmentUnit from UnitReconcile A
                        left join FundClient B on A.IFUACode  = B.IFUACode and B.status  = 2
                        left join Fund C on A.FundCode = C.SInvestCode and C.status  = 2
                        left join Agent D on B.SellingAgentPK = D.AgentPK and D.status  = 2
                        where A.status  = 1 and A.Selected  = 1 and isnull(A.SACode,'') <> @CompanyID and adjustmentUnit <> 0
                        and FundClientPK is not null and isnull(A.IFUACode,'') <> ''
	
                        Open A
                        Fetch Next From A
                        Into @ValueDate,@SACode,@IFUACode,@FundCode,@FundClientPK,@FundPK,@AgentPK,@AdjustmentUnit

                        While @@FETCH_STATUS = 0
                        BEGIN
                        IF @AdjustmentUnit > 0 
                        BEGIN
                            INSERT INTO ClientSubscription(ClientSubscriptionPK,HistoryPK,Status,Type,NAVDate,ValueDate,NAV,FundPK,FundClientPK,CashRefPK,Description,CashAmount,UnitAmount,TotalCashAmount,TotalUnitAmount,SubscriptionFeePercent,SubscriptionFeeAmount,AgentPK,AgentFeePercent,AgentFeeAmount,DepartmentPK,CurrencyPK,EntryUsersID,EntryTime,LastUpdate,IsFrontSync)
                            Select isnull(max(ClientSubscriptionPK),0) + 1,1,2,3,@ValueDate,@ValueDate,0,@FundPK,@FundClientPK,0,'Rounding Unit',0,@AdjustmentUnit,0,@AdjustmentUnit,0,0,@AgentPK,0,0,0,1,@UsersID,@Time,@Time,1 from ClientSubscription
                        END
                        ELSE
                        BEGIN
                            INSERT INTO ClientRedemption(ClientRedemptionPK,HistoryPK,Status,Type,BitRedemptionAll,NAVDate,ValueDate,PaymentDate,NAV,FundPK,FundClientPK,CashRefPK,Description,CashAmount,UnitAmount,TotalCashAmount,TotalUnitAmount,RedemptionFeePercent,RedemptionFeeAmount,AgentPK,AgentFeePercent,AgentFeeAmount,DepartmentPK,CurrencyPK,UnitPosition,BankRecipientPK,TransferType,EntryUsersID,EntryTime,LastUpdate,IsFrontSync)
                            Select isnull(max(ClientRedemptionPk),0) + 1,1,2,3,0,@ValueDate,@ValueDate,@ValueDate,0,@FundPK,@FundClientPK,0,'Rounding Unit',0,@AdjustmentUnit * -1,0,@AdjustmentUnit * -1,0,0,@AgentPK,0,0,0,1,0,0,0,@UsersID,@Time,@Time,1 from ClientRedemption
                        END

                        Fetch next From A Into @ValueDate,@SACode,@IFUACode,@FundCode,@FundClientPK,@FundPK,@AgentPK,@AdjustmentUnit
                        END
                        Close A
                        Deallocate A    ";

                        cmd.CommandTimeout = 0;
                        cmd.Parameters.AddWithValue("@UsersID", _usersID);
                        cmd.Parameters.AddWithValue("@Time", _datetimeNow);
                        cmd.Parameters.AddWithValue("@CompanyID", _host.Get_CompanyID());
                        cmd.ExecuteNonQuery();
                    }

                   
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

//        public void UnitReconcile_RejectBySelected(string _usersID, string _permissionID)
//        {
//            try
//            {
//                DateTime _datetimeNow = DateTime.Now;
//                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
//                {
//                    DbCon.Open();
//                    using (SqlCommand cmd = DbCon.CreateCommand())
//                    {
//                        cmd.CommandText = "Declare @IPAddress nvarchar(50) select @IPAddress = IPAddress from Users where ID = @UsersID and Status = 2 \n " +
//                                          " \n Insert into Activity(Time,PermissionID,ObjectTable,ObjectTablePK,Status,Message,UsersID,IPAddress,LastUpdate) \n " +
//                                          "Select @Time,@PermissionID,'UnitReconcile',UnitReconcilePK,1,'Reject by Selected Data',@UsersID,@IPAddress,@Time  from UnitReconcile where Status = 1 and Selected  = 1 " +
//                                          "\n update UnitReconcile set status = 3,selected = 0,VoidUsersID = @UsersID,VoidTime = @Time,LastUpdate=@Time where UnitReconcilePK in ( Select UnitReconcilePK from UnitReconcile where Selected  = 1 ) \n " +
//                                            //-- DISINI BUAT BALIKIN NILAI UNIT
//                                          "";
//                        cmd.Parameters.AddWithValue("@PermissionID", _permissionID);
//                        cmd.Parameters.AddWithValue("@UsersID", _usersID);
//                        cmd.Parameters.AddWithValue("@Time", _datetimeNow);
//                        cmd.ExecuteNonQuery();
//                    }
//                    using (SqlCommand cmd = DbCon.CreateCommand())
//                    {
//                        cmd.CommandText = @"declare @FundClientPK int
//                                declare @ValueDate DateTime
//                                declare @FundPK int
//                                declare @AdjustmentUnit numeric(22,4)
//                      
//                                DECLARE A CURSOR FOR 
//                                        Select ValueDate,FundClientPK,FundPK,AdjustmentUnit From UnitReconcile A
//                                        left join FundClient B on A.FundClientName  = B.Name and B.status  = 2
//                                        left join Fund C on A.FundID = C.ID and C.status  = 2
//	                                    where A.status  = 1 and A.Selected  = 1
//	
//                                Open A
//                                Fetch Next From A
//                                Into @ValueDate,@FundClientPK,@FundPK,@AdjustmentUnit
//
//                                While @@FETCH_STATUS = 0
//                                BEGIN
//                                Update FundClientPosition set UnitAmount = Case when @AdjustmentUnit < 0 then UnitAmount + @AdjustmentUnit else UnitAmount - @AdjustmentUnit end where FundPK = @FundPK and FundClientPK = @FundClientPK and Date = @ValueDate
//                                Fetch next From A Into @ValueDate,@FundClientPK,@FundPK,@AdjustmentUnit
//                                END
//                                Close A
//                                Deallocate A ";

//                        cmd.ExecuteNonQuery();
//                    }
//                }
//            }
//            catch (Exception err)
//            {
//                throw err;
//            }

//        }

        public void UnitReconcile_AdjustmentUnitBySelected(string _usersID, string _permissionID)
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
                                update UnitReconcile set AdjustmentUnit = Difference where
                                selected = 1 and  (Difference <= -0.0001 or  Difference >= 0.0001)


                                update UnitReconcile set Difference = 0 where
                                selected = 1 and  (Difference <= -0.0001 or  Difference >= 0.0001)

                                ";

                        cmd.Parameters.AddWithValue("@CompanyID", _host.Get_CompanyID());
                        cmd.ExecuteNonQuery();
                    }
                   
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public Boolean ValidatePostingBySelectedData()
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
                        if Exists
                        (select * From UnitReconcile where Difference <> 0 and Selected =1)  
                        BEGIN 
                        Select 1 Result 
                        END 
                        ELSE 
                        BEGIN     
                        Select 0 Result 
                        END     ";

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

        public List<UnitReconcile> UnitReconcile_SelectUnitReconcileAfterAdjustment(DateTime _dateFrom, DateTime _dateTo)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<UnitReconcile> L_Model = new List<UnitReconcile>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"select UnitReconcilePK,A.HistoryPK,A.Status,A.Selected,A.ValueDate,A.FundID,A.FundClientName,A.UnitSystem,
                        A.UnitSInvest,A.Description,A.Difference,A.AdjustmentUnit from UnitReconcile A
                        left join Fund B on A.FundID = B.ID and B.status in (1,2)

                        where  A.ValueDate between @DateFrom and @DateTo  and B.bitNeedRecon = 1
                        and A.AdjustmentUnit <> 0 and A.Selected = 1
                        order by A.Difference asc ";
                        cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _dateTo);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_Model.Add(setUnitReconcile(dr));
                                }
                            }
                            return L_Model;
                        }
                    }
                }

            }
            catch (Exception err)
            {
                throw err;
            }

        }


        public string UnitReconcileTempImportFromTxt(string _fileName, string _userID)
        {
            string _msg = "";
            using (SqlConnection conn = new SqlConnection(Tools.conString))
            {
                conn.Open();
                using (SqlCommand cmd2 = conn.CreateCommand())
                {
                    if (Tools.ClientCode == "24")
                    {
                        cmd2.CommandText = "delete UnitReconcileTemp where SACode = @CompanyID";
                        cmd2.Parameters.AddWithValue("@CompanyID", _host.Get_CompanyID());
                        cmd2.ExecuteNonQuery();
                    }
                    else
                    {
                        cmd2.CommandText = "truncate table UnitReconcileTemp";
                        cmd2.ExecuteNonQuery();

                    }

                }
            }

            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(Tools.conString, SqlBulkCopyOptions.TableLock))
            {
                bulkCopy.DestinationTableName = "dbo.UnitReconcileTemp";
                bulkCopy.WriteToServer(CreateDataTableFromUnitReconcileTxtFile(_fileName, _userID));

                _msg = "";
            }



            return _msg;

        }



        private DataTable CreateDataTableFromUnitReconcileTxtAPERDFile(string _fileName, string _userID)
        {
            DataTable dt = new DataTable();
            DataColumn dc;
            DataRow dr;

            DateTime _datetimeNow = DateTime.Now;

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.Int32");
            dc.ColumnName = "UnitReconcilePK";
            dc.Unique = false;
            dc.AutoIncrement = true;
            dc.AutoIncrementSeed = 1;
            dc.AutoIncrementStep = 1;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "ValueDate";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "IFUACode";
            dc.Unique = false;
            dt.Columns.Add(dc);


            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "FundCode";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "CurrencyID";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "SACode";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.Decimal");
            dc.ColumnName = "UnitBalance";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.Decimal");
            dc.ColumnName = "NAV";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.Decimal");
            dc.ColumnName = "AmountBalance";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "EntryTime";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "EntryUserID";
            dc.Unique = false;
            dt.Columns.Add(dc);


            StreamReader sr = new StreamReader(Tools.TxtFilePath + _fileName);
            string input;

            sr.ReadLine();
            while ((input = sr.ReadLine()) != null)
            {


                string[] s = input.Split(new char[] { '|' });


                string _valueDate = Convert.ToString(s[0]);
                if (!string.IsNullOrEmpty(_valueDate))
                {
                    string _tgl = _valueDate.Substring(6, 2);
                    string _bln = _valueDate.Substring(4, 2);
                    string _thn = _valueDate.Substring(0, 4);

                    _valueDate = _bln + "/" + _tgl + "/" + _thn; //Convert.ToDateTime(_strTransactionDate).ToString("MM/dd/yyyy");
                }

                dr = dt.NewRow();
                dr["ValueDate"] = _valueDate;
                dr["IFUACode"] = 0;
                dr["FundCode"] = s[1];
                dr["CurrencyID"] = 0;
                dr["SACode"] = s[7];
                dr["UnitBalance"] = s[9] == "" ? 0 : Convert.ToDecimal(s[9]);
                dr["NAV"] = 0;
                dr["AmountBalance"] = s[10] == "" ? 0 : Convert.ToDecimal(s[10]);
                dr["EntryTime"] = _datetimeNow;
                dr["EntryUserID"] = _userID;
                dt.Rows.Add(dr);
            }
            sr.Close();
            return dt;
        }

        private DataTable CreateDataTableFromUnitReconcileTxtFile(string _fileName, string _userID)
        {
            DataTable dt = new DataTable();
            DataColumn dc;
            DataRow dr;

            DateTime _datetimeNow = DateTime.Now;

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.Int32");
            dc.ColumnName = "UnitReconcilePK";
            dc.Unique = false;
            dc.AutoIncrement = true;
            dc.AutoIncrementSeed = 1;
            dc.AutoIncrementStep = 1;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "ValueDate";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "IFUACode";
            dc.Unique = false;
            dt.Columns.Add(dc);


            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "FundCode";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "CurrencyID";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "SACode";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.Decimal");
            dc.ColumnName = "UnitBalance";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.Decimal");
            dc.ColumnName = "NAV";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.Decimal");
            dc.ColumnName = "AmountBalance";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "EntryTime";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "EntryUserID";
            dc.Unique = false;
            dt.Columns.Add(dc);


            StreamReader sr = new StreamReader(Tools.TxtFilePath + _fileName);
            string input;

            sr.ReadLine();
            while ((input = sr.ReadLine()) != null)
            {


                string[] s = input.Split(new char[] { '|' });


                string _valueDate = Convert.ToString(s[0]);
                if (!string.IsNullOrEmpty(_valueDate))
                {
                    string _tgl = _valueDate.Substring(6, 2);
                    string _bln = _valueDate.Substring(4, 2);
                    string _thn = _valueDate.Substring(0, 4);

                    _valueDate = _bln + "/" + _tgl + "/" + _thn; //Convert.ToDateTime(_strTransactionDate).ToString("MM/dd/yyyy");
                }

                dr = dt.NewRow();
                dr["ValueDate"] = _valueDate;
                dr["IFUACode"] = s[1];
                dr["FundCode"] = s[4];
                dr["CurrencyID"] = 0;
                dr["SACode"] = s[10];
                dr["UnitBalance"] = s[12] == "" ? 0 : Convert.ToDecimal(s[12]);
                dr["NAV"] = 0;
                dr["AmountBalance"] = s[13] == "" ? 0 : Convert.ToDecimal(s[13]);
                dr["EntryTime"] = _datetimeNow;
                dr["EntryUserID"] = _userID;
                dt.Rows.Add(dr);
            }
            sr.Close();
            return dt;
        }

        public string FundClientPositionForAPERDImport(string _fileSource, string _userID)
        {
            string _msg = "";
            try
            {
                DateTime _now = DateTime.Now;

                using (SqlConnection conn = new SqlConnection(Tools.conString))
                {
                    conn.Open();
                    using (SqlCommand cmd2 = conn.CreateCommand())
                    {
                        cmd2.CommandText = "truncate table FundClientPositionForAPERDTemp";
                        cmd2.ExecuteNonQuery();
                    }
                }


                // import data ke temp dulu
                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(Tools.conString, SqlBulkCopyOptions.TableLock))
                {
                    bulkCopy.DestinationTableName = "dbo.FundClientPositionForAPERDTemp";
                    bulkCopy.WriteToServer(CreateDataTableFromFundClientPositionForAPERDExcelFile(_fileSource));

                    //_msg = "";
                }

                //logic kalo Udh Masuk ke temp
                using (SqlConnection conn = new SqlConnection(Tools.conString))
                {
                    conn.Open();
                    using (SqlCommand cmd1 = conn.CreateCommand())
                    {
                        cmd1.CommandText = @"




                declare @date date
declare @CompanyID nvarchar(100)

select @CompanyID = ID from Company where status = 2

select @date = cast(date as date) from FundClientPositionForAperdTemp

delete FundClientPositionForAPERD where date = @date

insert into FundClientPositionForAPERD 
select * from FundClientPositionForAperdTemp where sacode in (
	select distinct sacode from fundclient where status = 2 and sacode not in ('',@CompanyID)
)
                                                                "
                            ;
                        cmd1.Parameters.AddWithValue("@UserID", _userID);
                        cmd1.Parameters.AddWithValue("@TimeNow", _now);

                        using (SqlDataReader dr = cmd1.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                _msg = "Import Fund Client Position For APERD Success"; //Convert.ToString(dr[""]);
                                return _msg;
                            }
                            else
                            {
                                _msg = "";
                                return _msg;
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

        private DataTable CreateDataTableFromFundClientPositionForAPERDExcelFile(string _path)
        {
            try
            {
                using (DataTable dt = new DataTable())
                {
                    DataColumn dc;
                    DataRow dr;



                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.DateTime");
                    dc.ColumnName = "Date";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "FundName";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "FundClientName";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "IFUA";
                    dc.Unique = false;
                    dt.Columns.Add(dc);


                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "SID";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "SACode";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Decimal");
                    dc.ColumnName = "UnitAmount";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Decimal");
                    dc.ColumnName = "NAV";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Decimal");
                    dc.ColumnName = "AUM";
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
                                    string _valueDate = Convert.ToString(odRdr[1]);
                                    if (!string.IsNullOrEmpty(_valueDate))
                                    {
                                        string _tgl = _valueDate.Substring(6, 2);
                                        string _bln = _valueDate.Substring(4, 2);
                                        string _thn = _valueDate.Substring(0, 4);

                                        _valueDate = _bln + "/" + _tgl + "/" + _thn; //Convert.ToDateTime(_strTransactionDate).ToString("MM/dd/yyyy");
                                    }


                                    dr = dt.NewRow();
                                    dr["Date"] = _valueDate;
                                    dr["FundName"] = odRdr[6];
                                    dr["FundClientName"] = odRdr[3];
                                    dr["IFUA"] = odRdr[2];
                                    dr["SID"] = odRdr[4];
                                    dr["SACode"] = odRdr[12];
                                    dr["UnitAmount"] = Convert.ToDecimal(odRdr[14].ToString() == "" ? 0 : odRdr[14].Equals(DBNull.Value) == true ? 0 : odRdr[14]);
                                    dr["NAV"] = Convert.ToDecimal(odRdr[15].ToString() == "" ? 0 : odRdr[15].Equals(DBNull.Value) == true ? 0 : odRdr[15]);
                                    dr["AUM"] = Convert.ToDecimal(odRdr[16].ToString() == "" ? 0 : odRdr[16].Equals(DBNull.Value) == true ? 0 : odRdr[16]);
                                    //13

                                    //dr["Amount"] = (odRdr[3].ToString().Replace(",", ""));

                                    if (dr["FundName"].Equals(DBNull.Value) != true) { dt.Rows.Add(dr); }
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


        public string UnitReconcileTempImportFromTxtAPERD(string _fileName, string _userID)
        {
            string _msg = "";
            using (SqlConnection conn = new SqlConnection(Tools.conString))
            {
                conn.Open();
                using (SqlCommand cmd2 = conn.CreateCommand())
                {
                    cmd2.CommandText = "truncate table UnitReconcileAPERDTemp";

                    cmd2.ExecuteNonQuery();
                }
            }

            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(Tools.conString, SqlBulkCopyOptions.TableLock))
            {
                bulkCopy.DestinationTableName = "dbo.UnitReconcileAPERDTemp";
                bulkCopy.WriteToServer(CreateDataTableFromUnitReconcileTxtAPERDFile(_fileName, _userID));

                _msg = "";
            }

            using (SqlConnection conn = new SqlConnection(Tools.conString))
            {
                conn.Open();
                using (SqlCommand cmd1 = conn.CreateCommand())
                {
                    cmd1.CommandText =

                    @"
                    delete UnitReconcileTemp where SACode <> @CompanyID
                    Insert into UnitReconcileTemp 
                    select * from UnitReconcileAPERDTemp where SACode <> @CompanyID

                        ";
                    cmd1.Parameters.AddWithValue("@CompanyID", _host.Get_CompanyID());
                    cmd1.ExecuteNonQuery();

                }
                _msg = "";

            }

            return _msg;

        }





    }
}