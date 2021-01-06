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
using System.Text;
using System.Threading;
using System.Data;
using System.Data.Odbc;
using System.Data.OleDb;


namespace RFSRepository
{
    public class ManageURTransactionReps
    {
        Host _host = new Host();

     
        //2
        private ManageURTransaction setManageURTransaction(SqlDataReader dr)
        {
            ManageURTransaction M_ManageURTransaction = new ManageURTransaction();
            M_ManageURTransaction.SysNo = Convert.ToInt32(dr["SysNo"]);
            M_ManageURTransaction.Date = Convert.ToDateTime(dr["Date"]);
            M_ManageURTransaction.Selected = Convert.ToBoolean(dr["Selected"]);
            M_ManageURTransaction.Type = Convert.ToInt32(dr["Type"]);
            M_ManageURTransaction.TypeDesc = Convert.ToString(dr["TypeDesc"]);
            M_ManageURTransaction.TrxType = Convert.ToInt32(dr["TrxType"]);
            M_ManageURTransaction.TrxTypeDesc = Convert.ToString(dr["TrxTypeDesc"]);
            M_ManageURTransaction.FundClientName = Convert.ToString(dr["FundClientName"]);
            M_ManageURTransaction.FundID = Convert.ToString(dr["FundID"]);
            M_ManageURTransaction.CashAmount = Convert.ToDecimal(dr["CashAmount"]);
            M_ManageURTransaction.UnitAmount = Convert.ToDecimal(dr["UnitAmount"]);
            return M_ManageURTransaction;
        }


        public List<ManageURTransaction> Init_DataApplyParameter(ManageURTransaction _manageURTransaction)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<ManageURTransaction> L_ManageURTransaction = new List<ManageURTransaction>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        string _paramFund = "";
                        string _paramFundClient = "";
                        string _status = "";
                        string _table = "";
                        string _fundPK = "";
                        string _cash = "";
                        string _unit = "";

                        if (!_host.findString(_manageURTransaction.FundFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_manageURTransaction.FundFrom))
                        {
                            _paramFund = "And FundPK in ( " + _manageURTransaction.FundFrom + " ) ";
                        }
                        else
                        {
                            _paramFund = "";
                        }

                        if (!_host.findString(_manageURTransaction.FundClientFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_manageURTransaction.FundClientFrom))
                        {
                            _paramFundClient = "And FundClientPK in ( " + _manageURTransaction.FundClientFrom + " ) ";
                        }
                        else
                        {
                            _paramFundClient = "";
                        }

                        if (_manageURTransaction.Type == 1)
                        {
                            _table = " ClientSubscription";
                            _fundPK = " FundPK";
                            _cash = " ,TotalCashAmount";
                            _unit = " ,TotalUnitAmount";
                        }
                        else if (_manageURTransaction.Type == 2)
                        {
                            _table = " ClientRedemption";
                            _fundPK = " FundPK";
                            _cash = " ,TotalCashAmount";
                            _unit = " ,TotalUnitAmount";
                        }
                        else if (_manageURTransaction.Type == 3)
                        {
                            _table = " ClientSwitching";
                            _fundPK = " FundPKFrom";
                            _cash = " ,CashAmount";
                            _unit = " ,UnitAmount";
                        }

                        if (_manageURTransaction.Status == 1)
                        {
                            _status = " and Status = 2 and Posted = 1 and Revised = 0 ";

                        }
                        else if (_manageURTransaction.Status == 2)
                        {
                            _status = " and Status = 2 and Posted = 1 and Revised = 1 ";

                        }
                        else if (_manageURTransaction.Status == 3)
                        {
                            _status = " and Status = 2 and Posted = 0 and Revised = 0 ";

                        }
                        else if (_manageURTransaction.Status == 4)
                        {
                            _status = " and Status = 1  ";

                        }
                        else if (_manageURTransaction.Status == 5)
                        {
                            _status = " and Status = 3  ";

                        }
                        else if (_manageURTransaction.Status == 6)
                        {
                            _status = " and (Status = 2 or Posted = 1) and Revised = 0  ";

                        }
                        else if (_manageURTransaction.Status == 7)
                        {
                            _status = " and (Status = 1 Or Status = 2 or Posted = 1) and  Revised = 0  ";

                        }


                        cmd.CommandText = @"
                        
                        truncate table ZMANAGE_UR
                        Insert into ZMANAGE_UR(PK,Selected,Date,Type,TrxType,FundClientPK,FundPK,Amount,Unit)
                        select " + _table + @"PK SysNo,0,ValueDate,@Type,@TrxType,FundClientPK," + _fundPK + _cash + _unit + @"  from " + _table + @" where ValueDate between @DateFrom and @DateTo and Type = @TrxType " + _paramFund + _paramFundClient + _status;



                        cmd.Parameters.AddWithValue("@DateFrom", _manageURTransaction.DateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _manageURTransaction.DateTo);
                        cmd.Parameters.AddWithValue("@Type", _manageURTransaction.Type);
                        cmd.Parameters.AddWithValue("@TrxType", _manageURTransaction.TrxType);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_ManageURTransaction.Add(setManageURTransaction(dr));
                                }
                            }
                            return L_ManageURTransaction;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }


        public List<ManageURTransaction> Get_DataApplyParameter()
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<ManageURTransaction> L_ManageURTransaction = new List<ManageURTransaction>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = @" select PK SysNo,Date,A.Selected,case when A.Type = 1 then 'Subscription' else case when A.Type = 2 then 'Redemption' else 'Switching' end end TypeDesc,D.Descone TrxTypeDesc,
                        B.ID FundID,C.Name FundClientName,Amount CashAmount,Unit UnitAmount,A.*  from ZMANAGE_UR A 
                        left join Fund B on A.FundPK = B.FundPK and B.status in (1,2)
                        left join FundClient C on A.FundClientPK = C.FundClientPK and C.status in (1,2)
                        left join MasterValue D on A.TrxType = D.Code and D.status in (1,2) and D.ID = 'SubscriptionType'
                        order by PK ";

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_ManageURTransaction.Add(setManageURTransaction(dr));
                                }
                            }
                            return L_ManageURTransaction;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }


        public void SelectDeselectData(bool _toggle, int _PK, int _type)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = "Update ZMANAGE_UR set Selected = @Toggle where PK  = @PK and Type = @Type ";
                        cmd.Parameters.AddWithValue("@PK", _PK);
                        cmd.Parameters.AddWithValue("@Toggle", _toggle);
                        cmd.Parameters.AddWithValue("@Type", _type);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }


        public void SelectDeselectAllData(bool _toggle)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update ZMANAGE_UR set Selected = @Toggle ";
                        cmd.Parameters.AddWithValue("@Toggle", _toggle);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }



        public void Update_NavDate(DateTime _navDate)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = @"
                        Declare @PK int
                        Declare @Type int

                        DECLARE A CURSOR FOR 
                        select PK,Type from ZMANAGE_UR where selected = 1
        	
                        Open A
                        Fetch Next From A
                        Into @PK,@Type
        
                        While @@FETCH_STATUS = 0
                        BEGIN
	                        IF (@Type = 1)
	                        BEGIN
		                        update ClientSubscription set NAVDate = @NavDate where ClientSubscriptionPK = @PK and status not in (3,4)
	                        END
	                        ELSE IF (@Type = 2)
	                        BEGIN
		                        update ClientRedemption set NAVDate = @NavDate where ClientRedemptionPK = @PK and status not in (3,4)
	                        END
	                        ELSE
	                        BEGIN
		                        update ClientSwitching set NAVDate = @NavDate where ClientSwitchingPK = @PK and status not in (3,4)
	                        END

                        Fetch next From A Into @PK,@Type
                        END
                        Close A
                        Deallocate A 
                        ";

                        cmd.Parameters.AddWithValue("@NavDate", _navDate);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public int ValidateClientSubscription_PostingNAVBySelectedByManageUR(DateTime _dateFrom, DateTime _dateTo)
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
                        if Exists(select * From ClientSubscription where Status = 2 and Type not in(3,6) and ValueDate between @ValueDateFrom and @ValueDateTo and ClientSubscriptionPK in (select PK from ZManage_UR where Selected = 1 and Type = 1 and Date between @valuedatefrom and @valuedateto) and Posted = 0 and Revised = 0 and isnull(Nav,0) = 0 ) 
                        BEGIN Select 1 Result END ELSE BEGIN Select 0 Result END";

                        cmd.Parameters.AddWithValue("@ValueDateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@ValueDateTo", _dateTo);
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

        public int ValidateClientSubscription_PostingBySelectedByManageUR(DateTime _dateFrom, DateTime _dateTo)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {


                        cmd.CommandText = @" Declare @NAVDate datetime
                                                Declare A Cursor For              
                                                    Select distinct NAVDate From ClientSubscription Where status = 2 and Valuedate = @ValueDateFrom and ClientSubscriptionPK in (select PK from ZManage_UR where Selected = 1 and Type = 1 and Date between @valuedatefrom and @valuedateto)
                                                Open  A              
              
                                                Fetch Next From  A              
                                                into @NAVDate
                  
                                                While @@Fetch_Status = 0              
                                                BEGIN              
		                                                if not exists(Select * from EndDayTrails where status = 2 and Valuedate = dbo.FWorkingDay(@NAVDate,-1))
		                                                begin
			                                                select 1 result
		                                                end
		                                                else if (dbo.CheckTodayIsHoliday(@NAVDate) = 1)
		                                                begin
			                                                select 2 result
		                                                end
		                                                else
		                                                begin
			                                                select 0 result
		                                                end
                                                Fetch next From A                   
                                                    Into @NAVDate
                                                end                  
                                                Close A                  
                                                Deallocate A ";

                        cmd.Parameters.AddWithValue("@ValueDateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@ValueDateTo", _dateTo);
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

        public int ValidateClientRedemption_PostingNAVBySelectedByManageUR(DateTime _dateFrom, DateTime _dateTo)
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
                        if Exists(select * From ClientRedemption where Status = 2 and Type not in(3,6) and ValueDate between @ValueDateFrom and @ValueDateTo and ClientRedemptionPK in (select PK from ZManage_UR where Selected = 1 and Type = 2 and Date between @valuedatefrom and @valuedateto) and Posted = 0 and Revised = 0 and isnull(Nav,0) = 0 ) 
                        BEGIN Select 1 Result END ELSE BEGIN Select 0 Result END";

                        cmd.Parameters.AddWithValue("@ValueDateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@ValueDateTo", _dateTo);
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

        public int ValidateClientRedemption_PostingBySelectedByManageUR(DateTime _dateFrom, DateTime _dateTo)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {


                        cmd.CommandText = @" Declare @NAVDate datetime
                                                Declare A Cursor For              
                                                    Select distinct NAVDate From ClientRedemption Where status = 2 and Valuedate = @ValueDateFrom and ClientRedemptionPK in (select PK from ZManage_UR where Selected = 1 and Type = 2 and Date between @valuedatefrom and @valuedateto)
                                                Open  A              
              
                                                Fetch Next From  A              
                                                into @NAVDate
                  
                                                While @@Fetch_Status = 0              
                                                BEGIN              
		                                                if not exists(Select * from EndDayTrails where status = 2 and Valuedate = dbo.FWorkingDay(@NAVDate,-1))
		                                                begin
			                                                select 1 result
		                                                end
		                                                else if (dbo.CheckTodayIsHoliday(@NAVDate) = 1)
		                                                begin
			                                                select 2 result
		                                                end
		                                                else
		                                                begin
			                                                select 0 result
		                                                end
                                                Fetch next From A                   
                                                    Into @NAVDate
                                                end                  
                                                Close A                  
                                                Deallocate A ";

                        cmd.Parameters.AddWithValue("@ValueDateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@ValueDateTo", _dateTo);
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

        public int ValidateClientSwitching_PostingNAVBySelectedByManageUR(DateTime _dateFrom, DateTime _dateTo)
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
                        if Exists(select * From ClientSwitching where Status = 2 and ValueDate between @ValueDateFrom and @ValueDateTo and ClientSwitchingPK in (select PK from ZManage_UR where Selected = 1 and Type = 3 and Date between @valuedatefrom and @valuedateto) and Posted = 0 and Revised = 0 
                        and (isnull(NavFundFrom,0) = 0 or isnull(NavFundTo,0) = 0  or isnull(TotalCashAmountFundFrom,0) = 0 or isnull(TotalCashAmountFundTo,0) = 0))
                        BEGIN Select 1 Result END ELSE BEGIN Select 0 Result END 
";

                        cmd.Parameters.AddWithValue("@ValueDateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@ValueDateTo", _dateTo);
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

        public int ValidateClientSwitching_PostingBySelectedByManageUR(DateTime _dateFrom, DateTime _dateTo)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {


                        cmd.CommandText = @"  if Not Exists(select * From EndDayTrails where Status =  2 and ValueDate = dbo.fworkingday(@ValueDateFrom, -1)) 
                         BEGIN 
                            Select 1 Result 
                        END 
			           
                        ELSE 
                        BEGIN  
                            Select 0 Result  
                        END";

                        cmd.Parameters.AddWithValue("@ValueDateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@ValueDateTo", _dateTo);
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