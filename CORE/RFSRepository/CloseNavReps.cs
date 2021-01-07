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
using System.Data.OleDb;
using SucorInvest.Connect;


namespace RFSRepository
{
    public class CloseNavReps
    {
        Host _host = new Host();

        //1
        string _insertCommand = "INSERT INTO [dbo].[CloseNav] " +
                            "([CloseNavPK],[HistoryPK],[Status],[Date],[FundPK],[Nav],[AUM],[Approved1],[Approved2],";
        string _paramaterCommand = "@Date,@FundPK,@Nav,@AUM,@Approved1,@Approved2,";

        //2
        private CloseNav setCloseNav(SqlDataReader dr)
        {
            CloseNav M_CloseNav = new CloseNav();
            M_CloseNav.CloseNavPK = Convert.ToInt32(dr["CloseNavPK"]);
            M_CloseNav.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_CloseNav.Selected = Convert.ToBoolean(dr["Selected"]);
            M_CloseNav.Status = Convert.ToInt32(dr["Status"]);
            M_CloseNav.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_CloseNav.Notes = Convert.ToString(dr["Notes"]);
            M_CloseNav.Date = Convert.ToString(dr["Date"]);
            M_CloseNav.FundPK = Convert.ToInt32(dr["FundPK"]);
            M_CloseNav.FundID = Convert.ToString(dr["FundID"]);
            M_CloseNav.Nav = Convert.ToDecimal(dr["Nav"]);
            M_CloseNav.AUM = Convert.ToDecimal(dr["AUM"]);
            if (_host.CheckColumnIsExist(dr, "OutstandingUnit"))
            {
                M_CloseNav.OutstandingUnit = dr["OutstandingUnit"].Equals(DBNull.Value) == true ? 0 : Convert.ToDecimal(dr["OutstandingUnit"]);
            }
            M_CloseNav.FundName = Convert.ToString(dr["FundName"]);
            M_CloseNav.Approved1 = dr["Approved1"].Equals(DBNull.Value) == true ? false : Convert.ToBoolean(dr["Approved1"]);
            M_CloseNav.Approved2 = dr["Approved2"].Equals(DBNull.Value) == true ? false : Convert.ToBoolean(dr["Approved2"]);
            M_CloseNav.EntryUsersID = dr["EntryUsersID"].ToString();
            M_CloseNav.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_CloseNav.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_CloseNav.VoidUsersID = dr["VoidUsersID"].ToString();
            M_CloseNav.EntryTime = dr["EntryTime"].ToString();
            M_CloseNav.UpdateTime = dr["UpdateTime"].ToString();
            M_CloseNav.ApprovedTime = dr["ApprovedTime"].ToString();
            M_CloseNav.VoidTime = dr["VoidTime"].ToString();
            M_CloseNav.DBUserID = dr["DBUserID"].ToString();
            M_CloseNav.DBTerminalID = dr["DBTerminalID"].ToString();
            M_CloseNav.LastUpdate = dr["LastUpdate"].ToString();
            M_CloseNav.LastUpdateDB = Convert.ToString(dr["LastUpdateDB"]);
            return M_CloseNav;
        }



        public List<CloseNav> CloseNav_SelectByDateFromTo(int _status, DateTime _dateFrom, DateTime _dateTo)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<CloseNav> L_CloseNav = new List<CloseNav>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText =
                              "Select case when CN.status=1 then 'PENDING' else Case When CN.status = 2 then 'APPROVED' else Case when CN.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,F.ID FundID,F.Name FundName, CN.* from CloseNav CN left join " +
                              "Fund F on CN.FundPK = F.FundPK and F.status = 2 " +
                              "where CN.status = @status and Date between @DateFrom and @DateTo ";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText =
                              "Select case when CN.status=1 then 'PENDING' else Case When CN.status = 2 then 'APPROVED' else Case when CN.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,F.ID FundID,F.Name FundName, CN.* from CloseNav CN left join " +
                              "Fund F on CN.FundPK = F.FundPK and F.status = 2  " +
                              "where Date between @DateFrom and @DateTo ";
                        }

                        cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _dateTo);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_CloseNav.Add(setCloseNav(dr));
                                }
                            }
                            return L_CloseNav;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public int CloseNav_Add(CloseNav _closeNav, bool _havePrivillege)
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
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[ApprovedUsersID],[ApprovedTime],LastUpdate)" +
                                 "Select isnull(max(CloseNavPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@LastUpdate from CloseNav";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _closeNav.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);
                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],LastUpdate)" +
                                "Select isnull(max(CloseNavPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@LastUpdate from CloseNav";
                        }
                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@Date", _closeNav.Date);
                        cmd.Parameters.AddWithValue("@FundPK", _closeNav.FundPK);
                        cmd.Parameters.AddWithValue("@Nav", _closeNav.Nav);
                        cmd.Parameters.AddWithValue("@AUM", _closeNav.AUM);
                        if (_closeNav.Approved1 == false || _closeNav.Approved1 == null)
                        {
                            cmd.Parameters.AddWithValue("@Approved1", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@Approved1", _closeNav.Approved1);
                        }
                        if (_closeNav.Approved2 == false || _closeNav.Approved2 == null)
                        {
                            cmd.Parameters.AddWithValue("@Approved2", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@Approved2", _closeNav.Approved2);
                        }
                        cmd.Parameters.AddWithValue("@EntryUsersID", _closeNav.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);

                        cmd.ExecuteNonQuery();
                        return _host.Get_LastPKByLastUpate(_dateTimeNow, "CloseNav");
                    }
                }

            }
            catch (Exception err)
            {
                throw err;
            }

        }


        public int CloseNav_Update(CloseNav _closeNav, bool _havePrivillege)
        {
            try
            {
                int _newHisPK;
                int status = _host.Get_Status(_closeNav.CloseNavPK, _closeNav.HistoryPK, "CloseNav");
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update CloseNav set  status=2, Notes=@Notes,Date=@Date,FundPK=@FundPK,Nav=@Nav,AUM=@AUM,Approved1=@Approved1,Approved2=@Approved2," +
                                "ApprovedUsersID=@ApprovedUsersID, " +
                                "ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime ,LastUpdate=@lastupdate " +
                                "where CloseNavPK = @PK and historyPK = @HistoryPK";
                            cmd.Parameters.AddWithValue("@HistoryPK", _closeNav.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _closeNav.CloseNavPK);
                            cmd.Parameters.AddWithValue("@Notes", _closeNav.Notes);
                            cmd.Parameters.AddWithValue("@Date", _closeNav.Date);
                            cmd.Parameters.AddWithValue("@FundPK", _closeNav.FundPK);
                            cmd.Parameters.AddWithValue("@Nav", _closeNav.Nav);
                            cmd.Parameters.AddWithValue("@AUM", _closeNav.AUM);
                            if ( _closeNav.Approved1 == null)
                            {
                                cmd.Parameters.AddWithValue("@Approved1", DBNull.Value);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@Approved1", _closeNav.Approved1);
                            }
                            if ( _closeNav.Approved2 == null)
                            {
                                cmd.Parameters.AddWithValue("@Approved2", DBNull.Value);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@Approved2", _closeNav.Approved2);
                            }
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _closeNav.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _dateTimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _closeNav.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);
                            cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update CloseNav set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where CloseNavPK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _closeNav.CloseNavPK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _closeNav.EntryUsersID);
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
                                cmd.CommandText = "Update CloseNav set   Notes=@Notes,Date=@Date,FundPK=@FundPK,Nav=@Nav,AUM=@AUM,Approved1=@Approved1,Approved2=@Approved2," +
                                "UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@lastupdate " +
                                "where CloseNavPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _closeNav.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _closeNav.CloseNavPK);
                                cmd.Parameters.AddWithValue("@Notes", _closeNav.Notes);
                                cmd.Parameters.AddWithValue("@Date", _closeNav.Date);
                                cmd.Parameters.AddWithValue("@FundPK", _closeNav.FundPK);
                                cmd.Parameters.AddWithValue("@Nav", _closeNav.Nav);
                                cmd.Parameters.AddWithValue("@AUM", _closeNav.AUM);
                                if ( _closeNav.Approved1 == null)
                                {
                                    cmd.Parameters.AddWithValue("@Approved1", DBNull.Value);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@Approved1", _closeNav.Approved1);
                                }
                                if ( _closeNav.Approved2 == null)
                                {
                                    cmd.Parameters.AddWithValue("@Approved2", DBNull.Value);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@Approved2", _closeNav.Approved2);
                                }
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _closeNav.EntryUsersID);
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
                                _newHisPK = _host.Get_NewHistoryPK(_closeNav.CloseNavPK, "CloseNav");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From CloseNav where CloseNavPK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _closeNav.CloseNavPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _closeNav.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@Date", _closeNav.Date);
                                cmd.Parameters.AddWithValue("@FundPK", _closeNav.FundPK);
                                cmd.Parameters.AddWithValue("@Nav", _closeNav.Nav);
                                cmd.Parameters.AddWithValue("@AUM", _closeNav.AUM);
                                if ( _closeNav.Approved1 == null)
                                {
                                    cmd.Parameters.AddWithValue("@Approved1", DBNull.Value);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@Approved1", _closeNav.Approved1);
                                }
                                if ( _closeNav.Approved2 == null)
                                {
                                    cmd.Parameters.AddWithValue("@Approved2", DBNull.Value);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@Approved2", _closeNav.Approved2);
                                }
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _closeNav.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _dateTimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update CloseNav set status= 4,Notes=@Notes where CloseNavPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _closeNav.Notes);
                                cmd.Parameters.AddWithValue("@PK", _closeNav.CloseNavPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _closeNav.HistoryPK);
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

        public void CloseNav_Approved(CloseNav _closeNav)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update CloseNav set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,LastUpdate=@lastupdate  " +
                            "where CloseNavPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _closeNav.CloseNavPK);
                        cmd.Parameters.AddWithValue("@historyPK", _closeNav.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _closeNav.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update CloseNav set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where CloseNavPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _closeNav.CloseNavPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _closeNav.ApprovedUsersID);
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

        public void CloseNav_Reject(CloseNav _closeNav)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update CloseNav set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime ,LastUpdate=@LastUpdate " +
                            "where CloseNavPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _closeNav.CloseNavPK);
                        cmd.Parameters.AddWithValue("@historyPK", _closeNav.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _closeNav.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update CloseNav set status= 2,LastUpdate=@LastUpdate where CloseNavPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _closeNav.CloseNavPK);
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

        public void CloseNav_Void(CloseNav _closeNav)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update CloseNav set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime ,LastUpdate=@LastUpdate " +
                            "where CloseNavPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _closeNav.CloseNavPK);
                        cmd.Parameters.AddWithValue("@historyPK", _closeNav.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _closeNav.VoidUsersID);
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


        public bool Validate_ApproveByCloseNAVPK(int _closeNAVPK)
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
                       declare @FundPK int
                        declare @Date datetime
                        Select @FundPK = FundPK, @Date = Date From CloseNAV where status = 1 and CloseNAVPK = @CloseNAVPK


                        if exists(Select * from closeNAV 
	                        where fundPK = @FundPK and status = 2 and Date = @Date)
                        BEGIN
	                        select 1 result
                        END
                        ELSE
                        BEGIN
	                        select 0 result
                        END
                        ";
                        cmd.Parameters.AddWithValue("@CloseNAVPK", _closeNAVPK);
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

        public bool Validate_ApproveBySelectedData(DateTime _dateFrom, DateTime _dateTo)
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
                        if exists(Select * from closeNAV 
	                        where Date between @DateFrom and @Dateto
	                        and FundPK  in
	                        (
		                        Select FundPK from CloseNAV where 
		                        Date between @DateFrom and @Dateto and status = 2
	                        ) and status = 1 and selected = 1)
                        BEGIN
	                        select 1 result
                        END
                        ELSE
                        BEGIN
	                        select 0 result
                        END
                        ";
                        cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _dateTo);
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


        public bool Validate_GetCloseNav(DateTime _valueDate, CloseNav _closeNAV)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        string _paramFund = "";

                        if (!_host.findString(_closeNAV.FundFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_closeNAV.FundFrom))
                        {
                            _paramFund = "And A.FundPK in ( " + _closeNAV.FundFrom + " ) ";
                        }
                        else
                        {
                            _paramFund = "";
                        }

                        cmd.CommandText = @"

                        if (@ClientCode = 25)
                        BEGIN
                        select 0 Result

                        END
                        ELSE if (@ClientCode = 21)
                        BEGIN
                        if Exists(select * From CloseNav A
                        left join Fund B on A.FundPK = B.FundPK and B.status in (1,2)
                        where A.Status in (1,2) and Date = @ValueDate " + _paramFund + @"
                        ) BEGIN Select 1 Result END ELSE BEGIN Select 0 Result END

                        END
                        ELSE
                        BEGIN
                        if Exists(select * From CloseNav A
                        left join Fund B on A.FundPK = B.FundPK and B.status in (1,2)
                        where A.Status in (1,2) and B.Type <> 10  and Date = @ValueDate " + _paramFund + @"
                        ) BEGIN Select 1 Result END ELSE BEGIN Select 0 Result END
                    
                        END

                        ";



                        cmd.Parameters.AddWithValue("@ValueDate", _valueDate);
                        cmd.Parameters.AddWithValue("@ClientCode", Tools.ClientCode);
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

        public void CloseNav_GetCloseNavByDate(string _usersID, DateTime _date, CloseNav _CloseNav)
        {

            try
            {

                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    DateTime _dateTimeNow = DateTime.Now;
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        string _paramFund = "";

                        if (!_host.findString(_CloseNav.FundFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_CloseNav.FundFrom))
                        {
                            _paramFund = "And A.FundPK in ( " + _CloseNav.FundFrom + " ) ";
                        }
                        else
                        {
                            _paramFund = "";
                        }

                        cmd.CommandText = @"

--list dormant fund
declare @ListFundDormant table
(
	FundPK int,
	DormantDate date,
	ActivateDate date,
	StatusDormant int
)

insert into @ListFundDormant(FundPK)
select distinct fundpk from DormantFundTrails where status = 2

update A set A.DormantDate = B.DormantDate, A.ActivateDate = isnull(C.ActivateDate,'1900-01-01'), A.StatusDormant = case when isnull(C.ActivateDate,'1900-01-01') = '1900-01-01' then 1 
when B.DormantDate > isnull(C.ActivateDate,'1900-01-01') then 1 else 0 end

from @ListFundDormant A
left join (
    select FundPK, Max(DormantDate) DormantDate from DormantFundTrails where status = 2 and BitDormant = 1
    group by FundPK
) B on A.FundPK = B.FundPK
left join (
    select FundPK, Max(ActivateDate) ActivateDate from DormantFundTrails where status = 2 and BitDormant = 0
    group by FundPK
) C on A.FundPK = B.FundPK
--end dormant fund


Declare @CloseNAVPK int        

CREATE TABLE #DateUnit
(
FundPK int,
DateUnit datetime
)

CREATE CLUSTERED INDEX indx_DateUnit ON #DateUnit (FundPK,DateUnit);

CREATE TABLE #TotalUnit
(
FundPK int,
TotalUnit  numeric(19,8)
)

CREATE CLUSTERED INDEX indx_TotalUnit ON #TotalUnit (FundPK,TotalUnit);

CREATE TABLE #JournalBalance
(
FundPK int,
Assets numeric(19,4),
Liabilities numeric(19,4),
PayableSubsFee numeric(19,4)
)

CREATE CLUSTERED INDEX indx_JournalBalance ON #JournalBalance (FundPK,Assets,Liabilities,PayableSubsFee);

insert into #JournalBalance
select A.FundPK,[dbo].[FGetGroupAccountFundJournalBalanceByFundPK](@date,1,A.FundPK),
[dbo].[FGetGroupAccountFundJournalBalanceByFundPK](@date,63,A.FundPK),
[dbo].[FGetGroupAccountFundJournalBalanceByFundPK](@date,85,A.FundPK)
from Fund A where status in (1,2) and A.FundPK not in (select FundPK from @ListFundDormant where @date between DormantDate and case when isnull(ActivateDate,'1900-01-01') = '1900-01-01' then '2098-12-31' else dbo.FworkingDay(ActivateDate,-1) end) " + _paramFund + @"





Insert into #DateUnit
select A.FundPK, Max(Date) From FundClientPosition A
left join Fund B on A.FundPK = B.FundPK and B.status in (1,2)
where date =
(
Select max(date) from FundClientPosition A 
left join Fund B on A.FundPK = B.FundPK and B.status in (1,2)
where date < @date  and B.Type <> 10 " + _paramFund + @"
) and B.Type <> 10 and A.FundPK not in (select FundPK from @ListFundDormant where @date between DormantDate and case when isnull(ActivateDate,'1900-01-01') = '1900-01-01' then '2098-12-31' else dbo.FworkingDay(ActivateDate,-1) end) " + _paramFund + @"
group by A.FundPK

Insert into #DateUnit
select A.FundPK, Max(Date) From FundClientPosition A
left join Fund B on A.FundPK = B.FundPK and B.status in (1,2)
where date =
(
Select max(date) from FundClientPosition A 
left join Fund B on A.FundPK = B.FundPK and B.status in (1,2)
where date <= @date  and B.Type = 10 " + _paramFund + @"
) and B.Type = 10 and A.FundPK not in (select FundPK from @ListFundDormant where @date between DormantDate and case when isnull(ActivateDate,'1900-01-01') = '1900-01-01' then '2098-12-31' else dbo.FworkingDay(ActivateDate,-1) end) " + _paramFund + @"
group by A.FundPK




insert into #TotalUnit
select A.FundPK,[dbo].[FGetTotalUnitByFundPK](C.DateUnit,A.FundPK)
from Fund A 
left join #DateUnit C on A.FundPK = C.FundPK
where status in (1,2) and A.FundPK not in (select FundPK from @ListFundDormant where @date between DormantDate and case when isnull(ActivateDate,'1900-01-01') = '1900-01-01' then '2098-12-31' else dbo.FworkingDay(ActivateDate,-1) end) " + _paramFund + @"





select @CloseNAVPK = isnull(max(CloseNAVPK),0) From closeNav    

    
Insert into CloseNAV(CloseNavPK,HistoryPK,Status,Notes,Date,FundPK,AUM,NAV,EntryUsersID,EntryTime,LastUpdate)        
Select ROW_NUMBER() over(order by A.FundPK) + @CloseNAVPK,1,1,'NAV by system',@Date,A.FundPK        
,

-- AUM
CASE WHEN ISNULL(IssueDate,'') <> '' AND ISNULL(IssueDate,'') = @Date THEN ISNULL(B.TotalSubs,0) ELSE CASE when  (D.Assets 
- D.Liabilities) = 0 
THEN D.Assets - D.PayableSubsFee
else
(D.Assets
- D.Liabilities) end END,

-- NAV
CASE WHEN ISNULL(IssueDate,'') <> '' AND ISNULL(IssueDate,'') = @Date THEN 
CASE WHEN A.CurrencyPK = 1 THEN 
1000 ELSE 1 END
ELSE  CASE when cast(C.TotalUnit as float) = 0 then 1000  else      
Case when  (cast(D.Assets as float)
- cast(D.Liabilities as float)) = 0 and cast(C.TotalUnit as float) = 0 then
1000 else

(cast(D.Assets as float) - cast(D.Liabilities as float))  / 
cast(C.TotalUnit as float) end END END

,

@UsersID,@LastUpdate,@LastUpdate  
From Fund A
LEFT JOIN 
(
	SELECT SUM(CashAmount) TotalSubs,FundPK FundPKSubQuery FROM dbo.ClientSubscription WHERE status <> 3 AND ValueDate = @Date
	GROUP BY FundPK
)B ON A.FundPK = B.FundPKSubQuery
LEFT JOIN #TotalUnit C on A.FundPK = C.FundPK
LEFT JOIN #JournalBalance D on A.FundPK = D.FundPK
WHERE A.status = 2 and A.FundPK not in (select FundPK from @ListFundDormant where @date between DormantDate and case when isnull(ActivateDate,'1900-01-01') = '1900-01-01' then '2098-12-31' else dbo.FworkingDay(ActivateDate,-1) end) " + _paramFund + @"
                        ";

                        cmd.CommandTimeout = 0;
                        cmd.Parameters.AddWithValue("@Date", _date);
                        cmd.Parameters.AddWithValue("@UsersID", _usersID);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                        cmd.ExecuteNonQuery();
                    }
                }

                // UPDATE ROUNDING NAV
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    DateTime _dateTimeNow = DateTime.Now;
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        string _paramFund = "";

                        if (!_host.findString(_CloseNav.FundFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_CloseNav.FundFrom))
                        {
                            _paramFund = "And A.FundPK in ( " + _CloseNav.FundFrom + " ) ";
                        }
                        else
                        {
                            _paramFund = "";
                        }

                        cmd.CommandTimeout = 0;
                        cmd.CommandText = @"
                        
                        Declare @NAVRoundingMode int
                        Declare @NAVDecimalPlaces int
                        Declare @FundPK int
                        Declare @NAV numeric(22,12)

                        DECLARE A CURSOR FOR 
                        Select NAVRoundingMode, NAVDecimalPlaces,A.FundPK,A.Nav
                        From CloseNAV  A left join Fund B on A.FundPK = B.FundPK Where A.Date = @Date and A.status = 1
                        and B.Status = 2 " + _paramFund + @"

                        Open A
                        Fetch Next From A
                        Into @NAVRoundingMode,@NAVDecimalPlaces,@FundPK,@NAV

                    
                        WHILE @@FETCH_STATUS = 0  
                        BEGIN 

                        If @NAVRoundingMode = 1 BEGIN Set  @NAV = ROUND(@NAV,@NAVDecimalPlaces) 
	                        IF @NAVDecimalPlaces = 0 BEGIN
		                        set @NAV = @NAV + 1
	                        END 
	                        IF @NAVDecimalPlaces = 2 BEGIN
		                        set @NAV = @NAV + 0.01
	                        END 
	                        IF @NAVDecimalPlaces = 4 BEGIN
		                        set @NAV = @NAV + 0.0001
	                        END 
	                        IF @NAVDecimalPlaces = 6 BEGIN
		                        set @NAV = @NAV + 0.000001
	                        END 
	                        IF @NAVDecimalPlaces = 8 BEGIN
		                        set @NAV = @NAV + 0.00000001
	                        END 


                        END 

                        If @NAVRoundingMode = 2 BEGIN Set  @NAV = ROUND(@NAV,@NAVDecimalPlaces,1)   END 
                        If @NAVRoundingMode = 3 BEGIN Set  @NAV = ROUND(@NAV,@NAVDecimalPlaces)  END 

                        Update CloseNav set Nav = @Nav where status = 1 and Date = @Date and FundPK = @FundPK

                        Fetch next From A Into  @NAVRoundingMode,@NAVDecimalPlaces,@FundPK,@NAV
                        END
                        Close A
                        Deallocate A

                        ";

                        cmd.Parameters.AddWithValue("@Date", _date);
                        cmd.Parameters.AddWithValue("@UsersID", _usersID);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                        cmd.ExecuteNonQuery();
                    }
                }

                //Insert Fund Daily Fee
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    DateTime _dateTimeNow = DateTime.Now;
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        string _paramFund = "";
                        string _tbRecon = "";

                        if (!_host.findString(_CloseNav.FundFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_CloseNav.FundFrom))
                        {
                            _paramFund = "And FundPK in ( " + _CloseNav.FundFrom + " ) ";
                        }
                        else
                        {
                            _paramFund = "";
                        }


                        if (Tools.ClientCode == "17")
                        {
                            _tbRecon = " and B.TrxName <> 'TB Reconcile' ";
                        }
                        else
                        {
                            _tbRecon = "";
                        }

                        cmd.CommandText = @"
                           
                        --list dormant fund
                        declare @ListFundDormant table
                        (
	                        FundPK int,
	                        DormantDate date,
	                        ActivateDate date,
	                        StatusDormant int
                        )

                        insert into @ListFundDormant(FundPK)
                        select distinct fundpk from DormantFundTrails where status = 2

                        update A set A.DormantDate = B.DormantDate, A.ActivateDate = isnull(C.ActivateDate,'1900-01-01'), A.StatusDormant = case when isnull(C.ActivateDate,'1900-01-01') = '1900-01-01' then 1 
                        when B.DormantDate > isnull(C.ActivateDate,'1900-01-01') then 1 else 0 end

                        from @ListFundDormant A
                        left join (
                        select FundPK, Max(DormantDate) DormantDate from DormantFundTrails where status = 2 and BitDormant = 1
                        group by FundPK
                        ) B on A.FundPK = B.FundPK
                        left join (
                        select FundPK, Max(ActivateDate) ActivateDate from DormantFundTrails where status = 2 and BitDormant = 0
                        group by FundPK
                        ) C on A.FundPK = B.FundPK
                        --end dormant fund
                            
                        Declare @PayableManagementFee int
                        Declare @PayableSubscriptionFee int
                        Declare @PayableRedemptionFee int
                        Declare @PayableSwitchingFee int
                        Declare @FundPK int 
                        Declare @AmountMFee numeric(22,4)
                        Declare @AmountSubsFee numeric(22,4)
                        Declare @AmountRedempFee numeric(22,4)
                        Declare @AmountSwitchFee numeric(22,4)

                        DECLARE A CURSOR FOR 

                        select FundPK,@Date from Fund A where status = 2 " + _paramFund + @"
                        and MaturityDate >= @Date and A.FundPK not in (select FundPK from @ListFundDormant where @date between DormantDate and case when isnull(ActivateDate,'1900-01-01') = '1900-01-01' then '2098-12-31' else dbo.FworkingDay(ActivateDate,-1) end)

                        Open A
                        Fetch Next From A
                        Into @FundPK,@Date

                        While @@FETCH_STATUS = 0
                        BEGIN  
                        select @PayableManagementFee = ManagementFeeExpense, @PayableSubscriptionFee = PayableSubscriptionFee, @PayableRedemptionFee = PayableRedemptionFee, @PayableSwitchingFee = PayableSwitchingFee From FundAccountingSetup where FundPK = @FundPK and status = 2

                        set @AmountMFee = 0  
                        set @AmountSubsFee = 0  
                        set @AmountRedempFee = 0  
                        set @AmountSwitchFee = 0        

                        -- TAMBAH LOGIK KLO 0
                        select @AmountMFee = sum(Amount) from (
                        select case when C.Type in (1,4) then isnull(sum(BaseDebit-BaseCredit),0) else isnull(sum(BaseDebit-BaseCredit),0) * -1 end Amount from FundJournalDetail A
                        left join FundJournal B on A.FundJournalPK = B.FundJournalPK
                        left join FundJournalAccount C on A.FundJournalAccountPK = C.FundJournalAccountPK and C.status in (1,2)
                        where A.FundJournalAccountPK = @PayableManagementFee and A.status = 2 
                        and B.Posted = 1 and B.Reversed = 0 and B.ValueDate  = @Date and FundPK = @FundPK and B.Status = 2 " + _tbRecon + @"
                        group by C.Type,B.ValueDate,A.FundPK
                        union all
                        select case when C.Type in (1,4) then isnull(sum(BaseDebit-BaseCredit),0) else isnull(sum(BaseDebit-BaseCredit),0) * -1 end Amount from FundJournalDetail A
                        left join FundJournal B on A.FundJournalPK = B.FundJournalPK
                        left join FundJournalAccount C on A.FundJournalAccountPK = C.FundJournalAccountPK and C.status in (1,2)
                        where A.FundJournalAccountPK = @PayableManagementFee and A.status = 2 
                        and B.Posted = 1 and B.Reversed = 0 and B.ValueDate  = @Date and FundPK = @FundPK and B.Status = 2 and B.TrxName = 'TB Reconcile' and A.Amount between -100000 and 100000 -- sementara pake ini
                        group by C.Type,B.ValueDate,A.FundPK
                        ) A

                        select @AmountSubsFee = case when C.Type in (1,4) then isnull(sum(BaseDebit-BaseCredit),0) else isnull(sum(BaseDebit-BaseCredit),0) * -1 end  from FundJournalDetail A
                        left join FundJournal B on A.FundJournalPK = B.FundJournalPK
                        left join FundJournalAccount C on A.FundJournalAccountPK = C.FundJournalAccountPK and C.status in (1,2)
                        where A.FundJournalAccountPK = @PayableSubscriptionFee and A.status = 2 
                        and B.Posted = 1 and B.Reversed = 0 and B.ValueDate  = @Date and FundPK = @FundPK and B.Status = 2
                        group by C.Type,B.ValueDate,A.FundPK

                        select @AmountRedempFee = case when C.Type in (1,4) then isnull(sum(BaseDebit-BaseCredit),0) else isnull(sum(BaseDebit-BaseCredit),0) * -1 end  from FundJournalDetail A
                        left join FundJournal B on A.FundJournalPK = B.FundJournalPK
                        left join FundJournalAccount C on A.FundJournalAccountPK = C.FundJournalAccountPK and C.status in (1,2)
                        where A.FundJournalAccountPK = @PayableRedemptionFee and A.status = 2 
                        and B.Posted = 1 and B.Reversed = 0 and B.ValueDate  = @Date and FundPK = @FundPK and B.Status = 2
                        group by C.Type,B.ValueDate,A.FundPK

                        select @AmountSwitchFee = case when C.Type in (1,4) then isnull(sum(BaseDebit-BaseCredit),0) else isnull(sum(BaseDebit-BaseCredit),0) * -1 end  from FundJournalDetail A
                        left join FundJournal B on A.FundJournalPK = B.FundJournalPK
                        left join FundJournalAccount C on A.FundJournalAccountPK = C.FundJournalAccountPK and C.status in (1,2)
                        where A.FundJournalAccountPK = @PayableSwitchingFee and A.status = 2 
                        and B.Posted = 1 and B.Reversed = 0 and B.ValueDate  = @Date and FundPK = @FundPK and B.Status = 2
                        group by C.Type,B.ValueDate,A.FundPK


                        Delete FundDailyFee Where Date = @Date and FundPK = @FundPK   
                        IF (isnull(@AmountMFee,0)= 0)
                        BEGIN
	                        INSERT INTO FundDailyFee(Date,FundPK,ManagementFeeAmount,CustodiFeeAmount,SubscriptionFeeAmount,RedemptionFeeAmount,SwitchingFeeAmount)   
	                        select @Date,@FundPK,0,0,0,0,0
                        END
                        ELSE
                        BEGIN

	                        INSERT INTO FundDailyFee(Date,FundPK,ManagementFeeAmount,CustodiFeeAmount,SubscriptionFeeAmount,RedemptionFeeAmount,SwitchingFeeAmount)                          
	                        select @Date,@FundPK,@AmountMFee,0,@AmountSubsFee,@AmountRedempFee,@AmountSwitchFee
                        END




                        Fetch next From A Into @FundPK,@Date
                        END
                        Close A
                        Deallocate A


                        ";
                        cmd.CommandTimeout = 0;
                        cmd.Parameters.AddWithValue("@Date", _date);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }


        public decimal CloseNav_GetNAVProjectionByFundPK(int _fundPK, string _date)
        {

            try
            {

                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    DateTime _dateTimeNow = DateTime.Now;
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = @"
 
                       
      DECLARE @DateUnit DATETIME 

    SELECT @DateUnit = Max(date) 
    FROM   fundclientposition 
    WHERE  date = (SELECT Max(date) 
                    FROM   fundclientposition 
                    WHERE  date < @date) 
    
        SELECT Case when 
		     [dbo].[Fgettotalunitbyfundpk](@DateUnit, @fundpk) = 0 then 1000 else Case
			WHEN ( 
		    [dbo].[Fgetgroupaccountfundjournalbalancebyfundpk](@date, 1, @FundPK) - 
		    [dbo].[Fgetgroupaccountfundjournalbalancebyfundpk](@date, 63, @FundPK) ) 
		    = 0 
		    AND [dbo].[Fgettotalunitbyfundpk](@DateUnit, @fundpk) = 0 THEN 1000 
			ELSE ( 
		    [dbo].[Fgetgroupaccountfundjournalbalancebyfundpk](@date, 1, @FundPK) - 
		    [dbo].[Fgetgroupaccountfundjournalbalancebyfundpk](@date, 63, @FundPK) ) / 
				    [dbo].[Fgettotalunitbyfundpk](@DateUnit, @FundPK) 
		    END END
			Result

                    ";
                        cmd.CommandTimeout = 0;
                        cmd.Parameters.AddWithValue("@Date", _date);
                        cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return Convert.ToDecimal(dr["Result"]);

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


        public decimal CloseNav_GetTotalAccountBalanceByFundPK(int _fundPK, int _fundJournalAccountPK, string _date)
        {

            try
            {

                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    DateTime _dateTimeNow = DateTime.Now;
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = @"

                      
Declare @periodPK int
Declare @BeginDate datetime

select @BeginDate = [dbo].[FgetMaxDatePeriodClosingByFundPK](@Date,@FundPK) 

select @PeriodPK = PeriodPK from Period where status = 2 and @Date between dateFrom and DateTo

SELECT   CASE 
                        WHEN C.type IN ( 1, 4 ) THEN Sum( 
                        B.basedebit - B.basecredit) 
                        ELSE Sum(B.basecredit - B.basedebit) 
                    END  Result
FROM   fundjournal A 
        LEFT JOIN fundjournaldetail B 
            ON A.fundjournalpk = B.fundjournalpk 
                AND B.status = 2 
        LEFT JOIN fundjournalaccount C 
            ON B.fundjournalaccountpk = C.fundjournalaccountpk 
                AND C.status = 2 
WHERE  A.ValueDate  between @BeginDate and @Date
        AND A.posted = 1 
        AND A.reversed = 0 
        AND A.status = 2 
        --AND A.PeriodPK = @PeriodPK
        AND B.fundpk = @FundPK 
        AND @FundJournalAccountPK IN ( 
            C.fundjournalaccountpk, C.parentpk1, 
            C.parentpk2, 
            C.parentpk3, 
            C.parentpk4, C.parentpk5, C.parentpk6, 
            C.parentpk7, 
            C.parentpk8, C.parentpk9 ) 
GROUP  BY C.type 

                    ";
                        cmd.CommandTimeout = 0;
                        cmd.Parameters.AddWithValue("@Date", _date);
                        cmd.Parameters.AddWithValue("@FundJournalAccountPK", _fundJournalAccountPK);
                        cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return Convert.ToDecimal(dr["Result"]);

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

        public AccountJournal CloseNav_GetTotalAccountBalanceByFundPKNew(int _fundPK, int _fundJournalAccountPK, string _date, int _fundJournalAccountPK2)
        {

            try
            {

                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    DateTime _dateTimeNow = DateTime.Now;
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = @"
declare @AUM numeric(32,4)
declare @Unit numeric(32,4)
Declare @BeginDate datetime
declare @MaturityDate datetime

select @BeginDate = [dbo].[FgetMaxDatePeriodClosingByFundPK](@Date,@FundPK) 


select @MaturityDate = MaturityDate from Fund where FundPK = @fundPK and status in (1,2)

                      
select @AUM = isnull(sum(Result),0) from (
SELECT   CASE WHEN C.type IN ( 1, 4 ) THEN Sum(B.basedebit - B.basecredit) 
ELSE Sum(B.basecredit - B.basedebit) END  Result
FROM   fundjournal A 
LEFT JOIN fundjournaldetail B ON A.fundjournalpk = B.fundjournalpk AND B.status = 2 
LEFT JOIN fundjournalaccount C ON B.fundjournalaccountpk = C.fundjournalaccountpk AND C.status = 2 
WHERE  A.valuedate between @BeginDate and @Date AND A.posted = 1 AND A.reversed = 0 AND A.status = 2 
AND B.fundpk = @FundPK AND @Asset IN ( 
C.fundjournalaccountpk, C.parentpk1,C.parentpk2, C.parentpk3, C.parentpk4, C.parentpk5, C.parentpk6, 
C.parentpk7, C.parentpk8, C.parentpk9 ) 
GROUP  BY C.type 

union all

SELECT  CASE WHEN C.type IN ( 1, 4 ) THEN Sum(B.basedebit - B.basecredit) 
ELSE Sum(B.basecredit - B.basedebit) END * -1  Result
FROM   fundjournal A 
LEFT JOIN fundjournaldetail B ON A.fundjournalpk = B.fundjournalpk AND B.status = 2 
LEFT JOIN fundjournalaccount C ON B.fundjournalaccountpk = C.fundjournalaccountpk AND C.status = 2 
WHERE  A.valuedate between @BeginDate and @Date AND A.posted = 1 AND A.reversed = 0 AND A.status = 2
AND B.fundpk = @FundPK AND @Liabilities IN ( 
C.fundjournalaccountpk, C.parentpk1, C.parentpk2, C.parentpk3,C.parentpk4, C.parentpk5, C.parentpk6, 
C.parentpk7,C.parentpk8, C.parentpk9 ) 
GROUP  BY C.type 
) A


Declare @UnitAmount numeric(22,4)
Declare @FundType int

select @FundType = Type From Fund where FundPK = @fundpk and status in (1,2)


IF (@FundType = 10) -- ETF
BEGIN
	Select @UnitAmount = sum(UnitAmount)  from FundClientPosition where 
	date = (select max(date) 
	from FundClientPosition where date <= @Date and fundPK = @FundPK)
	and FundPK = @FundPK 
END
ELSE
BEGIN
	Select @UnitAmount = sum(UnitAmount)  from FundClientPosition where 
	date = (select max(date) 
	from FundClientPosition where date < @Date and fundPK = @FundPK)
	and FundPK = @FundPK 
END


IF isnull(@UnitAmount,0) <> 0
BEGIN
	Select @Unit =  @UnitAmount
END
ELSE
BEGIN
	         Declare @periodPK int
                      select @PeriodPK = PeriodPK from Period where status = 2 and @Date between dateFrom and DateTo

                      SELECT @UnitAmount =  CASE 
                                              WHEN C.type IN ( 1, 4 ) THEN Sum( 
                                              B.basedebit - B.basecredit) 
                                              ELSE Sum(B.basecredit - B.basedebit) 
                                            END  
                      FROM   fundjournal A 
                             LEFT JOIN fundjournaldetail B 
                                    ON A.fundjournalpk = B.fundjournalpk 
                                       AND B.status = 2 
                             LEFT JOIN fundjournalaccount C 
                                    ON B.fundjournalaccountpk = C.fundjournalaccountpk 
                                       AND C.status = 2 
                      WHERE  A.valuedate <= @Date 
                             AND A.posted = 1 
                             AND A.reversed = 0 
                             AND A.status = 2 
                             --AND A.PeriodPK = @PeriodPK
                             AND B.fundpk = @FundPK 
                             AND 1 IN ( 
                                 C.fundjournalaccountpk, C.parentpk1, 
                                 C.parentpk2, 
                                 C.parentpk3, 
                                 C.parentpk4, C.parentpk5, C.parentpk6, 
                                 C.parentpk7, 
                                 C.parentpk8, C.parentpk9 ) 
                      GROUP  BY C.type 


					  If isnull(@UnitAmount,0) >0
					  BEGIN 

		
					    	Select @Unit = case when
 [dbo].[Fgetgroupaccountfundjournalbalancebyfundpk](@date, 1, @FundPK) - 
		    [dbo].[Fgetgroupaccountfundjournalbalancebyfundpk](@date, 63, @FundPK) 
		    = 0 then 0 else @UnitAmount/1000 end 
						END

END

declare @TotalAccount1 numeric(22,4)
declare @TotalAccount63 numeric(22,4)

select @BeginDate = [dbo].[FgetMaxDatePeriodClosingByFundPK](@Date,@FundPK) 

select @PeriodPK = PeriodPK from Period where status = 2 and @Date between dateFrom and DateTo

SELECT @TotalAccount1 =  CASE 
                        WHEN C.type IN ( 1, 4 ) THEN Sum( 
                        B.basedebit - B.basecredit) 
                        ELSE Sum(B.basecredit - B.basedebit) 
                    END 
FROM   fundjournal A 
        LEFT JOIN fundjournaldetail B 
            ON A.fundjournalpk = B.fundjournalpk 
                AND B.status = 2 
        LEFT JOIN fundjournalaccount C 
            ON B.fundjournalaccountpk = C.fundjournalaccountpk 
                AND C.status = 2 
WHERE  A.ValueDate  between @BeginDate and @Date
        AND A.posted = 1 
        AND A.reversed = 0 
        AND A.status = 2 
        --AND A.PeriodPK = @PeriodPK
        AND B.fundpk = @FundPK 
        AND @Asset IN ( 
            C.fundjournalaccountpk, C.parentpk1, 
            C.parentpk2, 
            C.parentpk3, 
            C.parentpk4, C.parentpk5, C.parentpk6, 
            C.parentpk7, 
            C.parentpk8, C.parentpk9 ) 
GROUP  BY C.type 

SELECT @TotalAccount63 =  CASE 
                        WHEN C.type IN ( 1, 4 ) THEN Sum( 
                        B.basedebit - B.basecredit) 
                        ELSE Sum(B.basecredit - B.basedebit) 
                    END 
FROM   fundjournal A 
        LEFT JOIN fundjournaldetail B 
            ON A.fundjournalpk = B.fundjournalpk 
                AND B.status = 2 
        LEFT JOIN fundjournalaccount C 
            ON B.fundjournalaccountpk = C.fundjournalaccountpk 
                AND C.status = 2 
WHERE  A.ValueDate  between @BeginDate and @Date
        AND A.posted = 1 
        AND A.reversed = 0 
        AND A.status = 2 
        --AND A.PeriodPK = @PeriodPK
        AND B.fundpk = @FundPK 
        AND @Liabilities IN ( 
            C.fundjournalaccountpk, C.parentpk1, 
            C.parentpk2, 
            C.parentpk3, 
            C.parentpk4, C.parentpk5, C.parentpk6, 
            C.parentpk7, 
            C.parentpk8, C.parentpk9 ) 
GROUP  BY C.type 

-- BUAT FUND YG MATURE
IF (@MaturityDate <= @date)
BEGIN
	Select @Unit =  0, @AUM = 0
END


select isnull(@AUM,0) AUM, isnull(@Unit,0) Unit,isnull(@TotalAccount1,0) TotalAccount1,isnull(@TotalAccount63,0) TotalAccount63

                    ";
                        cmd.CommandTimeout = 0;
                        cmd.Parameters.AddWithValue("@Date", _date);
                        cmd.Parameters.AddWithValue("@Asset", _fundJournalAccountPK);
                        cmd.Parameters.AddWithValue("@Liabilities", _fundJournalAccountPK2);
                        cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return new AccountJournal()
                                {
                                    TotalAccount1 = Convert.ToDecimal(dr["TotalAccount1"]),
                                    TotalAccount63 = Convert.ToDecimal(dr["TotalAccount63"]),
                                    AUM = Convert.ToDecimal(dr["AUM"]),
                                    Unit = Convert.ToDecimal(dr["Unit"])
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

        public AccountJournal CloseNav_GetNAVProjectionByTBRecon(int _fundPK, string _date)
        {

            try
            {

                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    DateTime _dateTimeNow = DateTime.Now;
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = @"

                        
                   
                        DECLARE @NAV numeric (22,8)
                        DECLARE @LastNAVyesterday numeric(22,8)
                        DECLARE @DateUnit DATETIME 
                        Declare @FundType int
                        DECLARE @LastNavDate DATETIME 
                        DECLARE @MaturityDate DATETIME
                        Declare @CurrencyPK int

                        declare @A numeric(22,8)
                        declare @B numeric(22,8)
                        declare @C numeric(22,8)

                        select @FundType = Type, @MaturityDate = MaturityDate,@CurrencyPK = CurrencyPK From Fund where FundPK = @fundpk and status in (1,2)


                        IF (@FundType = 10) -- ETF
                        BEGIN
                        SELECT @DateUnit = Max(date) 
                        FROM   fundclientposition 
                        WHERE  date = (SELECT Max(date) 
                        FROM   fundclientposition 
                        WHERE  date <= @date) 
                        END
                        ELSE
                        BEGIN
                        SELECT @DateUnit = Max(date) 
                        FROM   fundclientposition 
                        WHERE  date = (SELECT Max(date) 
                        FROM   fundclientposition 
                        WHERE  date < @date) 
                        END

                        set @A = [dbo].[Fgetgroupaccountfundjournalbalancebyfundpk](@date, 1, @FundPK)
                        set @B = [dbo].[Fgetgroupaccountfundjournalbalancebyfundpk](@date, 63, @FundPK)
                        set @C = [dbo].[Fgettotalunitbyfundpk](@DateUnit, @FundPK)
    
                        IF (@CurrencyPK = 1)
                        BEGIN
	                        SELECT @NAV = Case when @C = 0 then 1000 else Case WHEN ( @A - @B )  = 0 AND @C = 0 THEN 1000 
	                        ELSE ( @A - @B ) / @C END END
                        END
                        ELSE
                        BEGIN
	                        SELECT @NAV = Case when @C = 0 then 1 else Case WHEN ( @A - @B )  = 0 AND @C = 0 THEN 1 
	                        ELSE ( @A - @B ) / @C END END
                        END



			

                        Select @LastNAVyesterday = isnull(Nav,0), @LastNavDate = Date From CloseNav
                        Where FundPK= @FundPK and Date= (select max(date) from CloseNav where date < @date and status = 2 and FundPk = @FundPk) and status=2

                        set @LastNAVyesterday = ISNULL(@LastNAVyesterday,1000)
                        set @LastNavDate = ISNULL(@LastNavDate,@Date)

			
                        Declare @NAVRoundingMode int 
                        Declare @NAVDecimalPlaces int
                        
                        Select @NAVRoundingMode = NAVRoundingMode, @NAVDecimalPlaces = NAVDecimalPlaces
                        From Fund Where FundPK = @FundPK and Status = 2

                        If @NAVRoundingMode = 1 BEGIN Set  @NAV = ROUND(@NAV,@NAVDecimalPlaces) 
                        IF @NAVDecimalPlaces = 0 BEGIN
                        set @NAV = @NAV + 1
                        END 
                        IF @NAVDecimalPlaces = 2 BEGIN
                        set @NAV = @NAV + 0.01
                        END 
                        IF @NAVDecimalPlaces = 4 BEGIN
                        set @NAV = @NAV + 0.0001
                        END 
                        IF @NAVDecimalPlaces = 6 BEGIN
                        set @NAV = @NAV + 0.000001
                        END 
                        IF @NAVDecimalPlaces = 8 BEGIN
                        set @NAV = @NAV + 0.00000001
                        END 


                        END 

                        If @NAVRoundingMode = 2 BEGIN Set  @NAV = ROUND(@NAV,@NAVDecimalPlaces,1)   END 
                        If @NAVRoundingMode = 3 BEGIN Set  @NAV = ROUND(@NAV,@NAVDecimalPlaces)  END 


                        -- BUAT FUND YG MATURE
                        IF (@MaturityDate <= @date)
                        BEGIN
                            select @NAV = 0
                        END


                        Select @NAV NAV,@LastNAVyesterday NAVYesterday,CONVERT(varchar, @LastNavDate, 6) LastNavDate,isnull(sum(@Nav-@LastNAVyesterday),0) ChangeNav, isnull((sum(@Nav/case when @LastNAVyesterday = 0 then @Nav else @LastNAVyesterday end)-1) * 100,0) ChangeNavPercent
                                     ";
                        cmd.CommandTimeout = 0;
                        cmd.Parameters.AddWithValue("@Date", _date);
                        cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return new AccountJournal()
                                {
                                    NAV = Convert.ToDecimal(dr["NAV"]),
                                    NAVYesterday = Convert.ToDecimal(dr["NAVYesterday"]),
                                    LastNavDate = Convert.ToString(dr["LastNavDate"]),
                                    ChangeNAV = Convert.ToDecimal(dr["ChangeNAV"]),
                                    ChangeNAVPercent = Convert.ToDecimal(dr["ChangeNAVPercent"])
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



        public decimal CloseNav_GetTotalAUMByFundPK(int _fundPK, int _asset, int _liabilities, string _date)
        {

            try
            {

                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    DateTime _dateTimeNow = DateTime.Now;
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = @"

                        Declare @BeginDate datetime
                        select @BeginDate = [dbo].[FgetMaxDatePeriodClosingByFundPK](@Date,@FundPK) 
                      
                        select isnull(sum(Result),0) Result from (
                        SELECT   CASE WHEN C.type IN ( 1, 4 ) THEN Sum(B.basedebit - B.basecredit) 
                        ELSE Sum(B.basecredit - B.basedebit) END  Result
                        FROM   fundjournal A 
                        LEFT JOIN fundjournaldetail B ON A.fundjournalpk = B.fundjournalpk AND B.status = 2 
                        LEFT JOIN fundjournalaccount C ON B.fundjournalaccountpk = C.fundjournalaccountpk AND C.status = 2 
                        WHERE  A.valuedate between @BeginDate and @Date AND A.posted = 1 AND A.reversed = 0 AND A.status = 2 
                        AND B.fundpk = @FundPK AND @Asset IN ( 
                        C.fundjournalaccountpk, C.parentpk1,C.parentpk2, C.parentpk3, C.parentpk4, C.parentpk5, C.parentpk6, 
                        C.parentpk7, C.parentpk8, C.parentpk9 ) 
                        GROUP  BY C.type 

                        union all

                        SELECT  CASE WHEN C.type IN ( 1, 4 ) THEN Sum(B.basedebit - B.basecredit) 
                        ELSE Sum(B.basecredit - B.basedebit) END * -1  Result
                        FROM   fundjournal A 
                        LEFT JOIN fundjournaldetail B ON A.fundjournalpk = B.fundjournalpk AND B.status = 2 
                        LEFT JOIN fundjournalaccount C ON B.fundjournalaccountpk = C.fundjournalaccountpk AND C.status = 2 
                        WHERE  A.valuedate between @BeginDate and @Date AND A.posted = 1 AND A.reversed = 0 AND A.status = 2
                        AND B.fundpk = @FundPK AND @Liabilities IN ( 
                        C.fundjournalaccountpk, C.parentpk1, C.parentpk2, C.parentpk3,C.parentpk4, C.parentpk5, C.parentpk6, 
                        C.parentpk7,C.parentpk8, C.parentpk9 ) 
                        GROUP  BY C.type 
                        ) A

                        ";
                        cmd.CommandTimeout = 0;
                        cmd.Parameters.AddWithValue("@Date", _date);
                        cmd.Parameters.AddWithValue("@Asset", _asset);
                        cmd.Parameters.AddWithValue("@Liabilities", _liabilities);
                        cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return Convert.ToDecimal(dr["Result"]);

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

        public decimal CloseNav_GetTotalUnitByFundPK(int _fundPK, string _date)
        {

            try
            {

                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    DateTime _dateTimeNow = DateTime.Now;
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = @"
                      

Declare @UnitAmount numeric(22,4)



Select @UnitAmount = sum(UnitAmount)  from FundClientPosition where 
date = (select max(date) 
from FundClientPosition where date < @Date and fundPK = @FundPK)
and FundPK = @FundPK 


IF isnull(@UnitAmount,0) <> 0
BEGIN
	Select @UnitAmount Result
END
ELSE
BEGIN
	         Declare @periodPK int
                      select @PeriodPK = PeriodPK from Period where status = 2 and @Date between dateFrom and DateTo

                      SELECT @UnitAmount =  CASE 
                                              WHEN C.type IN ( 1, 4 ) THEN Sum( 
                                              B.basedebit - B.basecredit) 
                                              ELSE Sum(B.basecredit - B.basedebit) 
                                            END  
                      FROM   fundjournal A 
                             LEFT JOIN fundjournaldetail B 
                                    ON A.fundjournalpk = B.fundjournalpk 
                                       AND B.status = 2 
                             LEFT JOIN fundjournalaccount C 
                                    ON B.fundjournalaccountpk = C.fundjournalaccountpk 
                                       AND C.status = 2 
                      WHERE  A.valuedate <= @Date 
                             AND A.posted = 1 
                             AND A.reversed = 0 
                             AND A.status = 2 
                             AND A.PeriodPK = @PeriodPK
                             AND B.fundpk = @FundPK 
                             AND 1 IN ( 
                                 C.fundjournalaccountpk, C.parentpk1, 
                                 C.parentpk2, 
                                 C.parentpk3, 
                                 C.parentpk4, C.parentpk5, C.parentpk6, 
                                 C.parentpk7, 
                                 C.parentpk8, C.parentpk9 ) 
                      GROUP  BY C.type 
					  If isnull(@UnitAmount,0) >0
					  BEGIN 
					    	Select case when
 [dbo].[Fgetgroupaccountfundjournalbalancebyfundpk](@date, 1, @FundPK) - 
		    [dbo].[Fgetgroupaccountfundjournalbalancebyfundpk](@date, 63, @FundPK) 
		    = 0 then 0 else @UnitAmount/1000 end Result
						END
						ELSE
						BEGIN
						Select 0 result
						END
END

                    ";
                        cmd.CommandTimeout = 0;
                        cmd.Parameters.AddWithValue("@Date", _date);
                        cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return Convert.ToDecimal(dr["Result"]);

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

        public decimal CloseNav_GetCloseNavByDateAndFundPK(int _fundPK, DateTime _date)
        {

            try
            {

                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();

                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Select Nav From CloseNav " +
                            "Where FundPK= @FundPK and Date=@Date and status=2";
                        cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                        cmd.Parameters.AddWithValue("@Date", _date);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return Convert.ToDecimal(dr["Nav"]);
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


        public void CloseNav_ApproveBySelected(string _usersID, string _permissionID, DateTime _dateFrom, DateTime _dateTo, CloseNav _closeNAV)
        {
            try
            {
                string paramCloseNAVSelected = "";
                paramCloseNAVSelected = "CloseNAVPK in (" + _closeNAV.CloseNavSelected + ") ";

                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"
                        Declare @IPAddress nvarchar(50) select @IPAddress = IPAddress from Users where ID = @UsersID and Status = 2
                        Insert into Activity(Time,PermissionID,ObjectTable,ObjectTablePK,Status,Message,UsersID,IPAddress,LastUpdate)
                        Select @Time,@PermissionID,'CloseNav',CloseNavPK,1,'Approve by Selected Data',@UsersID,@IPAddress,@Time  from CloseNav where Date between @DateFrom and @DateTo and Status = 1 and " + paramCloseNAVSelected + @"
                       
                        update CloseNav set status = 2,ApprovedUsersID = @UsersID,ApprovedTime = @Time,LastUpdate=@Time 
                        where status = 1 and CloseNavPK in ( Select CloseNavPK from CloseNav where Date between @DateFrom and @DateTo and Status = 1 and " + paramCloseNAVSelected + @" ) 
                        
                        Update CloseNav set status= 3,VoidUsersID=@UsersID,VoidTime=@Time,LastUpdate=@Time  
                        where status = 4 and CloseNavPK in (Select CloseNavPK from CloseNav where Date between @DateFrom and @DateTo and Status = 4 and " + paramCloseNAVSelected + @")   

                        ";
                        cmd.Parameters.AddWithValue("@PermissionID", _permissionID);
                        cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _dateTo);
                        cmd.Parameters.AddWithValue("@UsersID", _usersID);
                        cmd.Parameters.AddWithValue("@Time", _datetimeNow);
                        cmd.ExecuteNonQuery();

                        if (Tools.RDOSync)
                        {
                            var data = BackOffice.ExtractNavTable(FoConnection.Authentication());
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public void CloseNav_RejectBySelected(string _usersID, string _permissionID, DateTime _dateFrom, DateTime _dateTo, CloseNav _closeNAV)
        {
            try
            {
                string paramCloseNAVSelected = "";
                paramCloseNAVSelected = "CloseNAVPK in (" + _closeNAV.CloseNavSelected + ") ";

                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"
                        Declare @IPAddress nvarchar(50) select @IPAddress = IPAddress from Users where ID = @UsersID and Status = 2      

                        Insert into Activity(Time,PermissionID,ObjectTable,ObjectTablePK,Status,Message,UsersID,IPAddress,LastUpdate)      
                        Select @Time,@PermissionID,'CloseNav',CloseNavPK,1,'Reject by Selected Data',@UsersID,@IPAddress,@Time  from CloseNav where Date between @DateFrom and @DateTo and Status = 1 and " + paramCloseNAVSelected + @"    

                        update CloseNav set status = 3,VoidUsersID = @UsersID,VoidTime = @Time,LastUpdate=@Time 
                        where status = 1 and CloseNavPK in ( Select CloseNavPK from CloseNav where Date between @DateFrom and @DateTo and Status = 1 and " + paramCloseNAVSelected + @" )      

                        Update CloseNav set status= 2  
                        where status = 4 and CloseNavPK in (Select CloseNavPK from CloseNav where Date between @DateFrom and @DateTo and Status = 4 and " + paramCloseNAVSelected + @")  
                        ";
                        cmd.Parameters.AddWithValue("@PermissionID", _permissionID);
                        cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _dateTo);
                        cmd.Parameters.AddWithValue("@UsersID", _usersID);
                        cmd.Parameters.AddWithValue("@Time", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public void CloseNav_VoidBySelected(string _usersID, string _permissionID, DateTime _dateFrom, DateTime _dateTo, CloseNav _closeNAV)
        {
            try
            {
                string paramCloseNAVSelected = "";
                paramCloseNAVSelected = "CloseNAVPK in (" + _closeNAV.CloseNavSelected + ") ";

                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"
                                            Declare @IPAddress nvarchar(50) 
                                            select @IPAddress = IPAddress from Users where ID = @UsersID and Status = 2 

                                            Insert into Activity(Time,PermissionID,ObjectTable,ObjectTablePK,Status,Message,UsersID,IPAddress,LastUpdate)
                                            Select @Time,@PermissionID,'CloseNav',CloseNavPK,1,'Void by Selected Data',@UsersID,@IPAddress,@Time  from CloseNav where Date between @DateFrom and @DateTo and Status = 2 and " + paramCloseNAVSelected + @"

                                            update CloseNav set status = 3,VoidUsersID = @UsersID,VoidTime = @Time,LastUpdate=@Time where " + paramCloseNAVSelected
                                            ;
                        cmd.Parameters.AddWithValue("@PermissionID", _permissionID);
                        cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _dateTo);
                        cmd.Parameters.AddWithValue("@UsersID", _usersID);
                        cmd.Parameters.AddWithValue("@Time", _datetimeNow);
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





        public string NavReconcileImport(string _fileSource, string _userID)
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
                        cmd2.CommandText = "truncate table NavReconcileTemp";
                        cmd2.ExecuteNonQuery();
                    }
                }

                // import data ke temp dulu
                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(Tools.conString, SqlBulkCopyOptions.TableLock))
                {
                    bulkCopy.DestinationTableName = "dbo.NavReconcileTemp";
                    bulkCopy.WriteToServer(CreateDataTableFromNavReconcileExcelFile(_fileSource));

                    _msg = "";
                }


                return _msg;
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        private DataTable CreateDataTableFromNavReconcileExcelFile(string _path)
        {
            try
            {
                using (DataTable dt = new DataTable())
                {
                    DataColumn dc;
                    DataRow dr;

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Int32");
                    dc.ColumnName = "NavReconcileTempPK";
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
                    dc.ColumnName = "Fund";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Decimal");
                    dc.ColumnName = "Nav";
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
                                    dr["ValueDate"] = odRdr[1];
                                    dr["Fund"] = odRdr[2];
                                    dr["Nav"] = Convert.ToDecimal(odRdr[10].ToString() == "" ? 0 : odRdr[10].Equals(DBNull.Value) == true ? 0 : odRdr[10]);

                                    if (dr["NavReconcileTempPK"].Equals(DBNull.Value) != true) { dt.Rows.Add(dr); }
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

        public List<NavReconcile> NavReconcile_GenerateByDate(DateTime _dateFrom)
        {
            try
            {

                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<NavReconcile> L_Model = new List<NavReconcile>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
//                        //NEW
//                        cmd.CommandText = @"  
//                        declare @Valuedate datetime
//                        select @valuedate = valuedate from navreconciletemp where valuedate is not null
//                        --truncate table NavReconcile
//
//                        --drop TABLE #TotalAUM
//                        --drop TABLE #TotalUnit
//                        --drop table #Shares
//                        --drop table #Cash
//                        --drop table #Expense
//                        --drop table #AR
//                        --drop table #Net
//                        --drop table #CashProjection
//                        --drop table #nav
//
//                        BEGIN
//
//                        Declare @FundPK  int
//                        Declare @StandartFundAdmin int
//                        declare @WorkingDate datetime
//                        select @WorkingDate =  dbo.FWorkingDay(@ValueDate, - 1)
//
//                        CREATE TABLE #TotalAUM
//                        (FundPK int,AUM numeric (22,2))
//                        CREATE TABLE #TotalUnit
//                        (FundPK int,Unit numeric (22,4))
//                        create table #Shares
//                        (FundPK int,amount numeric(22,4))
//                        create table #Cash
//                        (FundPK int,amount numeric(22,4))
//                        create table #Expense
//                        (FundPK int,amount numeric(22,4))
//                        create table #AR
//                        (FundPK int,amount numeric(22,4))
//                        create table #Net
//                        (FundPK int,amount numeric(22,4))
//                        create table #CashProjection
//                        (FundPK int,Valuedate datetime,amount numeric(22,4))
//                        create table #MappingFundJournalAccount
//                        (FundJournalAccountPK int)
//
//                        DECLARE A CURSOR FOR 
//                        Select FundPK,StandartFundAdmin from Fund A
//                        where A.status  = 2 
//
//                        Open A
//                        Fetch Next From A
//                        Into @FundPK,@StandartFundAdmin
//
//                        Declare @FundJournalAccountPK int
//
//                        While @@FETCH_STATUS = 0
//                        BEGIN
//
//
//                        insert into #CashProjection (FundPK,ValueDate,amount)
//                        select @FundPK,@ValueDate,sum(BaseDebit - BaseCredit) from fundjournaldetail A left join fundjournal B
//                        on A.FundJournalPK = B.FundJournalPK and B.Status= 2  and B.Posted  = 1 and B.Reversed = 0
//                        where  ValueDate <=@ValueDate and FundJournalAccountPK in (3) and fundpk = @FundPK  
//
//
//                        insert into #TotalUnit (FundPK,Unit)
//                        select @FundPK,case when isnull([dbo].[FGetTotalUnitByFundPK](@WorkingDate,@FundPK),0) = 0 then 1 else isnull([dbo].[FGetTotalUnitByFundPK](@WorkingDate,@FundPK),0) end
//
//                        IF (@StandartFundAdmin = 1)
//                        BEGIN
//                            Insert Into #TotalAUM (FundPK,AUM)
//                            Select @FundPK,sum(basedebit-basecredit) from FundJournalDetail A
//                            left join FundjournalAccount B on A.FundJournalAccountPK = B.FundJournalAccountPK and B.Status= 2
//                            left join FundJournal C on A.FundJournalPK = C.FundJournalPK and C.Status= 2 and C.Posted = 1 and C.Reversed = 0
//                            where FundPK = @FundPK and A.Status  = 2 and DetailDescription <> 'ADJUST' and Valuedate <= @ValueDate
//                            and B.Type in (1,2) and B.ID not like '202%'
//                        END
//                        ELSE
//                        BEGIN
//                            Insert Into #TotalAUM (FundPK,AUM)
//                            Select @FundPK,sum(basedebit-basecredit) from FundJournalDetail A
//                            left join FundjournalAccount B on A.FundJournalAccountPK = B.FundJournalAccountPK and B.Status= 2
//                            left join FundJournal C on A.FundJournalPK = C.FundJournalPK and C.Status= 2 and C.Posted = 1 and C.Reversed = 0
//                            where FundPK = @FundPK and A.Status  = 2 and DetailDescription <> 'ADJUST' and Valuedate <= @ValueDate and A.FundJournalAccountPK not in (109,110)
//                            
//                        END
//
//                        if Not Exists
//                        (select FundPK from CashProjection 
//                        where  ValueDate = @ValueDate  and fundpk = @FundPK) 
//                        BEGIN 
//
//                        insert into CashProjection (FundPK,ValueDate,amount)
//                        select FundPK,@ValueDate,sum(Amount) from #CashProjection  where fundPK = @FundPK and ValueDate  = @Valuedate Group By FundPK, ValueDate
//
//                        END
//                        Else 
//                        BEGIN
//                        Declare @Amount numeric (22,4)
//                        select @Amount  = sum(Amount) from #CashProjection
//                        where FundPK  =  @FundPK and ValueDate  = @Valuedate Group By FundPK, ValueDate
//                        Update CashProjection set Amount = @Amount where ValueDate = @ValueDate and FundPK  = @FundPK
//                        END
//
//
//                        Fetch next From A Into @FundPK,@StandartFundAdmin
//                        END
//                        Close A
//                        Deallocate A 
//
//
//                        Declare @NavReconcilePK int
//                        CREATE TABLE #NAV
//                        (ValueDate Datetime, FundPK int, Amount numeric(22,2))
//                        INSERT INTO #NAV (ValueDate,FundPK,Amount)
//                        select @ValueDate,FundPK,sum(AUM) from #TotalAUM Group By FundPK 
//
//                        delete from NavReconcile 
//                        --delete from NavReconcile where NavRecon = 0
//                        --delete A from NavReconcile A left join Fund B on A.FundName  = B.Name and B.status  = 2
//                        --where B.FundPK in (select FundPK From CloseNAV where Date = @ValueDate and status  = 2)
//
//
//                        insert into NavReconcile (NavReconcilePK,ValueDate,FundName,NavSystem,NavRecon,AUM)
//
//                        select ROW_NUMBER() OVER (ORDER BY C.Name),A.ValueDate,C.Name FundName,isnull(Sum(Amount/Unit),0) Nav,isnull(D.Nav,0),isnull(A.Amount,0) from #NAV A 
//                        left join #TotalUnit B on A.FundPK = B.FundPK
//                        left join Fund C on A.FundPK = C.FundPK and C.Status = 2
//                        left join NavReconcileTemp D on A.ValueDate = D.ValueDate and C.sinvestcode = D.Fund
//                        Group By NavReconcileTempPK,A.ValueDate,C.Name,D.Nav,A.Amount
//
//                        select * from NavReconcile
//                        END
//                                                  ";

                        
//                        cmd.CommandTimeout = 0;
//                        cmd.Parameters.AddWithValue("@date", _dateFrom);

                        //EMCO
                        cmd.CommandText = @" 
                        declare @Valuedate datetime
                        select @valuedate = valuedate from navreconciletemp where valuedate is not null
                        --truncate table NavReconcile

                        --drop TABLE #TotalAUM
                        --drop TABLE #TotalUnit
                        --drop table #Shares
                        --drop table #Cash
                        --drop table #Expense
                        --drop table #AR
                        --drop table #Net
                        --drop table #CashProjection
                        --drop table #nav

                        BEGIN

                        Declare @FundPK  int
                        Declare @StandartFundAdmin int
                        declare @WorkingDate datetime
                        select @WorkingDate =  dbo.FWorkingDay(@ValueDate, - 1)

                        CREATE TABLE #TotalAUM
                        (FundPK int,AUM numeric (22,2))
                        CREATE TABLE #TotalUnit
                        (FundPK int,Unit numeric (22,4))
                        create table #Shares
                        (FundPK int,amount numeric(22,4))
                        create table #Cash
                        (FundPK int,amount numeric(22,4))
                        create table #Expense
                        (FundPK int,amount numeric(22,4))
                        create table #AR
                        (FundPK int,amount numeric(22,4))
                        create table #Net
                        (FundPK int,amount numeric(22,4))
                        create table #CashProjection
                        (FundPK int,Valuedate datetime,amount numeric(22,4))
                        create table #MappingFundJournalAccount
                        (FundJournalAccountPK int)

                        DECLARE A CURSOR FOR 
                        Select FundPK,StandartFundAdmin from Fund A
                        where A.status  = 2 

                        Open A
                        Fetch Next From A
                        Into @FundPK,@StandartFundAdmin

                        Declare @FundJournalAccountPK int

                        While @@FETCH_STATUS = 0
                        BEGIN


                        insert into #CashProjection (FundPK,ValueDate,amount)
                        select @FundPK,@ValueDate,sum(BaseDebit - BaseCredit) from fundjournaldetail A left join fundjournal B
                        on A.FundJournalPK = B.FundJournalPK and B.Status= 2  and B.Posted  = 1 and B.Reversed = 0
                        where  ValueDate <=@ValueDate and FundJournalAccountPK in (3) and fundpk = @FundPK  


                        insert into #TotalUnit (FundPK,Unit)
                        select @FundPK,case when isnull([dbo].[FGetTotalUnitByFundPK](@WorkingDate,@FundPK),0) = 0 then 1 else isnull([dbo].[FGetTotalUnitByFundPK](@WorkingDate,@FundPK),0) end

                        IF (@StandartFundAdmin = 1)
                        BEGIN
                            IF (@FundPK = 4)
                            BEGIN
                            Insert Into #TotalAUM (FundPK,AUM)
                            Select @FundPK,sum(basedebit-basecredit) from FundJournalDetail A
                            left join FundjournalAccount B on A.FundJournalAccountPK = B.FundJournalAccountPK and B.Status= 2
                            left join FundJournal C on A.FundJournalPK = C.FundJournalPK and C.Status= 2 and C.Posted = 1 and C.Reversed = 0
                            where FundPK = @FundPK and A.Status  = 2 and DetailDescription <> 'ADJUST' and Valuedate <= @ValueDate
                            and B.Type in (1,2) and B.ID not like '202%' and A.FundJournalAccountPK <> 42
                            END
                            ELSE
                            BEGIN
                            Insert Into #TotalAUM (FundPK,AUM)
                            Select @FundPK,sum(basedebit-basecredit) from FundJournalDetail A
                            left join FundjournalAccount B on A.FundJournalAccountPK = B.FundJournalAccountPK and B.Status= 2
                            left join FundJournal C on A.FundJournalPK = C.FundJournalPK and C.Status= 2 and C.Posted = 1 and C.Reversed = 0
                            where FundPK = @FundPK and A.Status  = 2 and DetailDescription <> 'ADJUST' and Valuedate <= @ValueDate
                            and B.Type in (1,2) and B.ID not like '202%'
                            END
                        END
                        ELSE
                        BEGIN
                            Insert Into #TotalAUM (FundPK,AUM)
                            Select @FundPK,sum(basedebit-basecredit) from FundJournalDetail A
                            left join FundjournalAccount B on A.FundJournalAccountPK = B.FundJournalAccountPK and B.Status= 2
                            left join FundJournal C on A.FundJournalPK = C.FundJournalPK and C.Status= 2 and C.Posted = 1 and C.Reversed = 0
                            where FundPK = @FundPK and A.Status  = 2 and DetailDescription <> 'ADJUST' and Valuedate <= @ValueDate and A.FundJournalAccountPK not in (109,110)
                            
                        END

                        if Not Exists
                        (select FundPK from CashProjection 
                        where  ValueDate = @ValueDate  and fundpk = @FundPK) 
                        BEGIN 

                        insert into CashProjection (FundPK,ValueDate,amount)
                        select FundPK,@ValueDate,sum(Amount) from #CashProjection  where fundPK = @FundPK and ValueDate  = @Valuedate Group By FundPK, ValueDate

                        END
                        Else 
                        BEGIN
                        Declare @Amount numeric (22,4)
                        select @Amount  = sum(Amount) from #CashProjection
                        where FundPK  =  @FundPK and ValueDate  = @Valuedate Group By FundPK, ValueDate
                        Update CashProjection set Amount = @Amount where ValueDate = @ValueDate and FundPK  = @FundPK
                        END


                        Fetch next From A Into @FundPK,@StandartFundAdmin
                        END
                        Close A
                        Deallocate A 


                        Declare @NavReconcilePK int
                        CREATE TABLE #NAV
                        (ValueDate Datetime, FundPK int, Amount numeric(22,2))
                        INSERT INTO #NAV (ValueDate,FundPK,Amount)
                        select @ValueDate,FundPK,sum(AUM) from #TotalAUM Group By FundPK 

                        delete from NavReconcile 
                        --delete from NavReconcile where NavRecon = 0
                        --delete A from NavReconcile A left join Fund B on A.FundName  = B.Name and B.status  = 2
                        --where B.FundPK in (select FundPK From CloseNAV where Date = @ValueDate and status  = 2)


                        insert into NavReconcile (NavReconcilePK,ValueDate,FundName,NavSystem,NavRecon,AUM)

                        select ROW_NUMBER() OVER (ORDER BY C.Name),A.ValueDate,C.Name FundName,isnull(Sum(Amount/Unit),0) Nav,isnull(D.Nav,0),isnull(A.Amount,0) from #NAV A 
                        left join #TotalUnit B on A.FundPK = B.FundPK
                        left join Fund C on A.FundPK = C.FundPK and C.Status = 2
                        left join NavReconcileTemp D on A.ValueDate = D.ValueDate and C.sinvestcode = D.Fund
                        Group By NavReconcileTempPK,A.ValueDate,C.Name,D.Nav,A.Amount

                        select * from NavReconcile
                        END";
                        cmd.CommandTimeout = 0;
                        cmd.Parameters.AddWithValue("@date", _dateFrom);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_Model.Add(setNavReconcile(dr));
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

        public List<NavReconcile> CloseNav_SelectNavReconcile()
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<NavReconcile> L_Model = new List<NavReconcile>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = "Select * from NavReconcile order by FundName ";
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_Model.Add(setNavReconcile(dr));
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


        private NavReconcile setNavReconcile(SqlDataReader dr)
        {
            NavReconcile M_Model = new NavReconcile();
            M_Model.NavReconcilePK = Convert.ToInt32(dr["NavReconcilePK"]);
            M_Model.Selected = Convert.ToBoolean(dr["Selected"]);
            M_Model.ValueDate = Convert.ToString(dr["ValueDate"]);
            M_Model.FundName = dr["FundName"].ToString();
            M_Model.NavSystem = Convert.ToDecimal(dr["NavSystem"]);
            M_Model.NavRecon = Convert.ToDecimal(dr["NavRecon"]);

            return M_Model;
        }


        public int CloseNav_InsertNavReconcileBySelected(NavReconcile _navReconcile)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();

                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_navReconcile.Type == 1)
                        {
                            cmd.CommandText = @"
                            Declare @CloseNAVPK int        
                            select @CloseNAVPK = isnull(max(CloseNAVPK),0) From closeNav   
                            Insert into CloseNAV(CloseNavPK,HistoryPK,Status,Notes,Date,FundPK,AUM,NAV,EntryUsersID,EntryTime,LastUpdate) 
                            select ROW_NUMBER() over(order by B.FundPK) + @CloseNAVPK,1,1,'Insert NAV By System',ValueDate,B.FundPK,AUM,
                            NavSystem,@EntryUsersID,@Time,@Time
                            from NavReconcile A left join Fund B on A.FundName = B.Name where B.status  = 2 and selected = 1
                            Group By ValueDate,B.FundPK,AUM,NavSystem
                            update NavReconcile set selected = 0";
                        }
                        else
                        {
                            cmd.CommandText = @"
                            Declare @CloseNAVPK int        
                            select @CloseNAVPK = isnull(max(CloseNAVPK),0) From closeNav   
                            Insert into CloseNAV(CloseNavPK,HistoryPK,Status,Notes,Date,FundPK,AUM,NAV,EntryUsersID,EntryTime,LastUpdate) 
                            select ROW_NUMBER() over(order by B.FundPK) + @CloseNAVPK,1,1,'Insert NAV By System',ValueDate,B.FundPK,AUM,
                            NavRecon,@EntryUsersID,@Time,@Time
                            from NavReconcile A left join Fund B on A.FundName = B.Name where B.status  = 2 and selected = 1
                            Group By ValueDate,B.FundPK,AUM,NavRecon
                            update NavReconcile set selected = 0";
                        }


                        cmd.Parameters.AddWithValue("@EntryUsersID", _navReconcile.EntryUsersID);
                        cmd.Parameters.AddWithValue("@Time", _dateTimeNow);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    return Convert.ToInt32(dr["PK"]);
                                }
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



        public bool ValidateNavReconcile()
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        //cmd.CommandText = "if Exists(select * From EndDayTrails where Status in (1,2) and ValueDate = @ValueDate) BEGIN Select 1 Result END ELSE BEGIN Select 0 Result END";

                        cmd.CommandText = @"
                        DECLARE @Date datetime
                        DECLARE @fundPK int
                        DECLARE A CURSOR FOR 
                        select ValueDate, B.FundPK from NavReconcile A
                        left join Fund B on A.FundName = B.Name where B.status = 2 and selected = 1
                        Open A
                        Fetch Next From A
                        Into @date,@FundPK

                        While @@FETCH_STATUS = 0
                        BEGIN 
                        IF Exists(select * From CloseNAV where  Date = @date and FundPK = @FundPK and status in (1,2))
                        BEGIN Select 1 Result END 
                        ELSE BEGIN Select 0 Result END
                        Fetch Next From A
                        Into @date,@FundPK
                        END
                        Close A
                        Deallocate A ";

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

        public bool ValidateGenerateEndDayTrailsUnit(DateTime _valueDate)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        //RHB
                        cmd.CommandText = " if Exists(select * From EndDayTrails where Status not in (2,3) and ValueDate = @ValueDate)  " +
                                          " BEGIN Select 1 Result END ELSE BEGIN 0 Result END    ";

                        cmd.Parameters.AddWithValue("@ValueDate", _valueDate);
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

        public bool ValidateGenerateEndDayTrails()
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        //RHB
                        cmd.CommandText =
                        @" declare @ValueDate Datetime
                            select @ValueDate = ValueDate From NavReconcileTemp where valuedate is not null
                            if Not Exists(select * From EndDayTrails where Status = 2 and ValueDate = @ValueDate)  
                            BEGIN Select 1 Result END ELSE BEGIN Select 0 Result END    ";

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

        public string ImportCloseNav(string _fileSource, string _userID)
        {
            string _msg = "";
            try
            {
                DateTime _now = DateTime.Now;

                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    //delete data yang lama
                    using (SqlConnection conn = new SqlConnection(Tools.conString))
                    {
                        conn.Open();
                        using (SqlCommand cmd1 = conn.CreateCommand())
                        {
                            cmd1.CommandText = "truncate table CloseNavTemp";
                            cmd1.ExecuteNonQuery();
                        }
                    }

                    // import data ke temp dulu
                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(Tools.conString, SqlBulkCopyOptions.TableLock))
                    {
                        bulkCopy.DestinationTableName = "dbo.CloseNavTemp";
                        bulkCopy.WriteToServer(CreateDataTableFromCloseNavTempExcelFile(_fileSource));
                        //_msg = "Import Close Nav Success";
                    }

                    // logic kalo Reconcile success
                    using (SqlConnection conn = new SqlConnection(Tools.conString))
                    {
                        conn.Open();
                        using (SqlCommand cmd1 = conn.CreateCommand())
                        {
                            cmd1.CommandText = " Declare @Date datetime " +
                                               " Declare @FundName nvarchar(200) " +
                                               " Declare A Cursor For        " +
                                               " SELECT CN.Date,CN.FundName     " +
                                               " FROM CloseNavTemp CN Left Join Fund F on F.Name = CN.FundName and F.status = 2       " +
                                                 "\n " +
                                               " Open A      " +
                                               " Fetch Next From A   " +
                                               " Into @Date,@FundName    " +
                                                 "\n " +
                                               " While @@FETCH_STATUS = 0    " +
                                               " Begin         " +
                                                   " DECLARE @CloseNavPK BigInt " +
                                                   " SELECT @CloseNavPK = isnull(Max(CloseNavPK),0) FROM CloseNav \n " +
                                                   " Update CN set status  = 3 From CloseNav CN Left Join Fund F on F.FundPK = CN.FundPK and F.status = 2 where F.Name  = @FundName and Date =  @Date " +
                                                   " INSERT INTO CloseNav(CloseNavPK,HistoryPK,Status,Date,FundPK, " +
                                                   " AUM,Nav,EntryUsersID,EntryTime,LastUpdate) \n " +
                                                   " SELECT Row_number() over(order by CloseNavPK) + @CloseNavPK,1,1,convert(datetime, date, 101), " +
                                                   " F.FundPK,isnull(CN.AUM,0),isnull(CN.Nav,0),@UserID,@TimeNow,@TimeNow " +
                                                   " FROM CloseNavTemp CN Left Join Fund F on F.Name = CN.FundName and F.status = 2 where F.Name  = @FundName and Date =  @Date " +
                                                   " Select 'Import Success' A    " +
                                               " Fetch next From A         " +
                                               " Into @Date,@FundName            " +
                                               " End         " +
                                               " Close A         " +
                                               " Deallocate A   ";
                            cmd1.Parameters.AddWithValue("@UserID", _userID);
                            cmd1.Parameters.AddWithValue("@TimeNow", _now);

                            using (SqlDataReader dr = cmd1.ExecuteReader())
                            {
                                if (dr.HasRows)
                                {
                                    dr.Read();
                                    _msg = Convert.ToString(dr["A"]);
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
            }


            catch (Exception err)
            {
                throw err;
            }
        }

        private DataTable CreateDataTableFromCloseNavTempExcelFile(string _path)
        {
            try
            {
                using (DataTable dt = new DataTable())
                {
                    DataColumn dc;
                    DataRow dr;

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "NAVDate";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "FundCode";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "FundName";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "FundType";
                    dc.Unique = false;
                    dt.Columns.Add(dc);



                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "IMCode";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "IMName";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "FundCCY";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "CBCode";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "CBName";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Decimal");
                    dc.ColumnName = "NAVperUnit";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Decimal");
                    dc.ColumnName = "NAV";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Decimal");
                    dc.ColumnName = "MonthlyPerformance";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Decimal");
                    dc.ColumnName = "Last1YearPerformance";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Decimal");
                    dc.ColumnName = "Last1YearRealPerformance";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Decimal");
                    dc.ColumnName = "OutstandingUnit";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "InputDateTime";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "AmendmentDateTime";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    using (OleDbConnection odConnection = new OleDbConnection(Tools.ConStringExcel2007(_path)))
                    {
                        odConnection.Open();
                        using (OleDbCommand odCmd = odConnection.CreateCommand())
                        {
                            // _oldfilename = nama sheet yang ada di file excel yang diimport
                            string SheetName = "SELECT * FROM [" + (string.IsNullOrEmpty("") ? Tools.GetTableName(Tools.ConStringExcel2007(_path), 0) : "" + "$") + "]";
                            odCmd.CommandText = SheetName;
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

                                    dr["NAVDate"] = odRdr[1];
                                    dr["FundCode"] = odRdr[2];
                                    dr["FundName"] = odRdr[3];
                                    dr["FundType"] = odRdr[4];
                                    dr["IMCode"] = odRdr[5];
                                    dr["IMName"] = odRdr[6];
                                    dr["CBCode"] = odRdr[7];
                                    dr["CBName"] = odRdr[8];
                                    dr["FundCCY"] = odRdr[9];
                                    dr["NAVperUnit"] = odRdr[10];
                                    dr["NAV"] = odRdr[11];
                                    dr["MonthlyPerformance"] = odRdr[12];
                                    dr["Last1YearPerformance"] = odRdr[13];
                                    dr["Last1YearRealPerformance"] = odRdr[14];
                                    dr["OutstandingUnit"] = odRdr[15];
                                    dr["InputDateTime"] = odRdr[16];
                                    dr["AmendmentDateTime"] = odRdr[17];

                                    if (dr["NAVDate"].Equals(DBNull.Value) != true) { dt.Rows.Add(dr); }
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

        public bool ValidateCheckTBReconcile()
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        //cmd.CommandText = "if Exists(select * From EndDayTrails where Status in (1,2) and ValueDate = @ValueDate) BEGIN Select 1 Result END ELSE BEGIN Select 0 Result END";

                        cmd.CommandText = @"
                        DECLARE @Date datetime
                        select @Date = ValueDate from NavReconcile 

                        IF Not Exists(select * From FundJournal  
                        where Valuedate = @date and TrxName = 'TB RECONCILE' and status = 2 and Posted = 1 and Reversed = 0 )
                        BEGIN 
                            Select 1 Result 
                        END 
                        ELSE 
                        BEGIN 
                            Select 0 Result 
                        END ";

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


        public void Insert_IntoCloseNavByEndDayTrails(string _usersID, DateTime _date)
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
                        Declare @FundPK int
                        Declare @TempAUM numeric (22,4)
                        Declare @TempTotalUnit numeric (22,4)
                        Declare @TempNAV numeric (22,4)

                        DECLARE A CURSOR FOR 
                        Select FundPK from Fund A
                        where A.status  = 2 

                        Open A
                        Fetch Next From A
                        Into @FundPK

                        While @@FETCH_STATUS = 0
                        BEGIN

                        select @TempAUM = sum(A.Amount) from (
                        select [dbo].[FGetGroupAccountFundJournalBalanceByFundPK] (@Valuedate,1,@FundPK) Amount
                        union all
                        select [dbo].[FGetGroupAccountFundJournalBalanceByFundPK] (@Valuedate,63,@FundPK) Amount
                        )A

                        select @TempTotalUnit =  case when isnull([dbo].[FGetTotalUnitByFundPK](@Valuedate,@FundPK),0) = 0 
                        then 1 else isnull([dbo].[FGetTotalUnitByFundPK](@Valuedate,@FundPK),0) end
                        select @TempNAV = @TempAUM/@TempTotalUnit

                        Declare @CloseNAVPK int        
                        select @CloseNAVPK = isnull(max(CloseNAVPK),0) From closeNav   
                        Insert into CloseNAV(CloseNavPK,HistoryPK,Status,Notes,Date,FundPK,AUM,NAV,EntryUsersID,EntryTime,LastUpdate) 
                        select @CloseNAVPK + 1,1,1,'Insert NAV By System',@Valuedate,@FundPK,@TempAUM,
                        @TempNAV,@UsersID,@Time,@Time

                        Fetch next From A Into @FundPK
                        END
                        Close A
                        Deallocate A ";

                        cmd.Parameters.AddWithValue("@Valuedate", _date);
                        cmd.Parameters.AddWithValue("@UsersID", _usersID);
                        cmd.Parameters.AddWithValue("@Time", _dateTimeNow);
                        cmd.ExecuteNonQuery();
                    }

                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }


        private DataTable CreateDataTableFromFxdTempExcelFile(string _fileName)
        {
            DataTable dt = new DataTable();
            DataColumn dc;
            DataRow dr;

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Date";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Code";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "InvestmentMoneyMarket";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "InvestmentOtherDebt";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "InvestmentEquity";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "InvestmentWarranRight";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Cash";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "DividendReceivable";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "InterestReceivable";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "ReceivableonSecuritySold";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "OtherReceivable";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "PrepaidTax";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "TotalAssets";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "PayableonSecurityPurchase";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "OtherPayable";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "TotalLiabilities";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "TotalNetAssets";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Subscription";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Redemption";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "RetainedEarning";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "DistributedIncome";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "UnrealizedGainLoss";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "RealizedGainLoss";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "NetIncome";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "TotalNAV";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "OutstandingUnitIssued";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "NAVUnit";
            dc.Unique = false;
            dt.Columns.Add(dc);

            StreamReader sr;
            if (_fileName == Tools._fxdPathAutoNAV)
            {
                sr = new StreamReader(_fileName);
            }
            else
            {
                sr = new StreamReader(Tools.TxtFilePath + _fileName);
            }
            string input;

            sr.ReadLine();
            while ((input = sr.ReadLine()) != null)
            {


                string[] s = input.Split(new char[] { ',' });
                string _strDate = Convert.ToString(s[0]);
                if (!string.IsNullOrEmpty(_strDate))
                {
                    string _tgl = _strDate.Substring(6, 2);
                    string _bln = _strDate.Substring(4, 2);
                    string _thn = _strDate.Substring(0, 4);

                    _strDate = _bln + "/" + _tgl + "/" + _thn; //Convert.ToDateTime(_strTransactionDate).ToString("MM/dd/yyyy");
                }

                dr = dt.NewRow();
                dr["Date"] = _strDate;
                dr["Code"] = s[1];
                dr["InvestmentMoneyMarket"] = s[2];
                dr["InvestmentOtherDebt"] = s[3];
                dr["InvestmentEquity"] = s[4];
                dr["InvestmentWarranRight"] = s[5];
                dr["Cash"] = s[6];
                dr["DividendReceivable"] = s[7];
                dr["InterestReceivable"] = s[8];
                dr["ReceivableonSecuritySold"] = s[9];
                dr["OtherReceivable"] = s[10];
                dr["PrepaidTax"] = s[11];
                dr["TotalAssets"] = s[12];
                dr["PayableonSecurityPurchase"] = s[13];
                dr["OtherPayable"] = s[14];
                dr["TotalLiabilities"] = s[15];
                dr["TotalNetAssets"] = s[16];
                dr["Subscription"] = s[17];
                dr["Redemption"] = s[18];
                dr["RetainedEarning"] = s[19];
                dr["DistributedIncome"] = s[20];
                dr["UnrealizedGainLoss"] = s[21];
                dr["RealizedGainLoss"] = s[22];
                dr["NetIncome"] = s[23];
                dr["TotalNAV"] = s[24];
                dr["OutstandingUnitIssued"] = s[25];
                dr["NAVUnit"] = s[26];

                dt.Rows.Add(dr);
            }
            sr.Close();
            return dt;
        }


        public string FxdImport(string _fileSource, string _userID)
        {
            string _msg;
            DateTime _dateTime = DateTime.Now;
            try
            {

                using (SqlConnection conns = new SqlConnection(Tools.conString))
                {
                    conns.Open();
                    using (SqlCommand cmd0 = conns.CreateCommand())
                    {
                        cmd0.CommandText = "truncate table FxdTemp";
                        cmd0.ExecuteNonQuery();
                    }
                }

                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {

                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        // import data ke temp dulu
                        using (SqlBulkCopy bulkCopy = new SqlBulkCopy(Tools.conString, SqlBulkCopyOptions.TableLock))
                        {
                            bulkCopy.DestinationTableName = "dbo.FxdTemp";
                            bulkCopy.WriteToServer(CreateDataTableFromFxdTempExcelFile(_fileSource));
                            _msg = "Update Fxd Success";
                        }


                        // logic kalo Reconcile success
                        using (SqlConnection conn = new SqlConnection(Tools.conString))
                        {
                            conn.Open();
                            using (SqlCommand cmd2 = conn.CreateCommand())
                            {
                                cmd2.CommandText =
                                  @"
                                if Exists( Select * from Fxdtemp 
                                where Code not in
                                (
	                                select nkpdname from fund where status in (1,2)
                                ))
                                BEGIN

                                declare @Message nvarchar(max)
                                set @Message = ' '
                                select distinct @Message = @Message +' , ' + Code  from fxdtemp 
	                                where Code not in
		                                (
			                                select nkpdname from fund where status in (1,2)
		                                )

	                                Select top 1  'false' result,@Message ResultDesc from fxdtemp 
	                                where Code not in
		                                (
			                                select nkpdname from fund where status in (1,2)
		                                )
                                END
                                ELSE
                                BEGIN
	                                select 'true' result,'' ResultDesc
                                END";

                                using (SqlDataReader dr01 = cmd2.ExecuteReader())
                                {
                                    dr01.Read();
                                    if (Convert.ToString(dr01["Result"]) == "false")
                                    {
                                        _msg = Convert.ToString(dr01["ResultDesc"]);

                                    }
                                    else
                                    {

                                        using (SqlCommand cmd0 = DbCon.CreateCommand())
                                        {
                                            cmd0.CommandText = @"delete B from Fxdtemp A
                                            inner join Fxd11Data B on A.Date = B.Date and A.Code = B.Code
                                            where B.Date is not null";
                                            cmd0.ExecuteNonQuery();
                                        }
                                        // tutup sementara
                                        //                    using (SqlCommand cmd1 = DbCon.CreateCommand())
                                        //                    {
                                        //                        cmd1.CommandTimeout = 0;
                                        //                        cmd1.CommandText = @"insert into fxd11data
                                        //                        (date,code,InvestmentMoneyMarket,InvestmentOtherDebt,InvestmentEquity,InvestmentWarranRight,Cash,DividendReceivable,InterestReceivable,ReceivableonSecuritySold,
                                        //OtherReceivable,PrepaidTax,TotalAssets,PayableonSecurityPurchase,OtherPayable,TotalLiabilities,TotalNetAssets,Subscription,Redemption,RetainedEarning,DistributedIncome,
                                        //UnrealizedGainLoss,RealizedGainLoss,NetIncome,TotalNAV,OutstandingUnitIssued,NAVUnit)  

                                        //select date,code,InvestmentMoneyMarket,InvestmentOtherDebt,InvestmentEquity,InvestmentWarranRight,Cash,DividendReceivable,InterestReceivable,ReceivableonSecuritySold,
                                        //OtherReceivable,PrepaidTax,TotalAssets,PayableonSecurityPurchase,OtherPayable,TotalLiabilities,TotalNetAssets,Subscription,Redemption,RetainedEarning,DistributedIncome,
                                        //UnrealizedGainLoss,RealizedGainLoss,NetIncome,TotalNAV,OutstandingUnitIssued,NAVUnit from FxdTemp";
                                        //                        cmd1.ExecuteNonQuery();
                                        //                    }

                                        conn.Close();
                                        conn.Open();
                                        _msg = "Import Fxd 11 Success";
                                        using (SqlCommand cmd3 = conn.CreateCommand())
                                        {
                                            cmd3.CommandTimeout = 0;
                                            cmd3.CommandText =
                                            @"  
                                   
                                            Declare @Date Datetime
                                            Declare @FundPK int
                                            Declare @NAVUnit numeric(18,8)
                                            Declare @TotalNav numeric(18,4)
                                            Declare @CloseNavPK int
                                            --select @Date = Date from FxdTemp

								            --select @Date

                                            Declare A Cursor For
                                            select FundPK,TotalNav,NAVUnit, Date from FxdTemp A
                                            left join Fund b on a.Code = b.NKPDName and B.status in (1,2)

                                            Open A
                                            Fetch next From A
                                            into @FundPK,@TotalNav,@NAVUnit, @Date
                                            While @@Fetch_status = 0
                                            BEGIN
                                
								            --tambahan dari boris ngecek ke nav
								            if not exists (select * from CloseNAV where Date = @Date and FundPK = @FundPK and status in (1,2))
								            begin

									            update CloseNav set Status = 3
											            where FundPK = @FundPK and Date = @Date

									            Select @CloseNavPK = max(CloseNavPK) + 1 from CloseNAV
									            set @CloseNavPK = isnull(@CloseNavPK,1)

									            insert into 
									            [dbo].[CloseNAV]
									            ([CloseNavPK]
									            ,[HistoryPK]
									            ,[Status]
									            ,[Selected]
									            ,[Date]
									            ,[FundPK]
									            ,[AUM]
									            ,[Nav]   
									            ,[EntryUsersID]
									            ,[EntryTime]
									            ,[UpdateUsersID]
									            ,[UpdateTime]
									            ,[LastUpdate]
									            )

									            select @CloseNavPK,1,2,1,@Date,@fundPK,@TotalNav,@NAVUnit,@EntryUsersID,@LastUpdate,@EntryUsersID,@LastUpdate,@LastUpdate
                                
								            end


                                            fetch next From A into @FundPK,@TotalNav,@NAVUnit,@Date
                                            end
                                            Close A
                                            Deallocate A
                                            ";
                                            cmd3.Parameters.AddWithValue("@EntryUsersID", _userID);
                                            cmd3.Parameters.AddWithValue("@LastUpdate", _dateTime);
                                            cmd3.ExecuteNonQuery();
                                        }
                                    }
                                }
                            }
                        }



                    }
                }
                return _msg;
            }
            catch (Exception err)
            {
                throw err;
            }
        }


        public string FxdImportCsv(string _fileSource, string _userID)
        {

            try
            {
                string _msg;
                DateTime _dateTimeNow = DateTime.Now;

                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    
                    DbCon.Open();
                DateTime _dateTime = DateTime.Now;
                DataTable dt = new DataTable();
                DataColumn dc;

                dc = new DataColumn();
                dc.DataType = System.Type.GetType("System.String");
                dc.ColumnName = "ValueDate";
                dc.Unique = false;
                dt.Columns.Add(dc);

                StreamReader sr = new StreamReader(_fileSource);
                string input;
                string _valueDate;
                sr.ReadLine();
                while ((input = sr.ReadLine()) != null)
                {


                    string[] s = input.Split(new char[] { '|' });


                    _valueDate = Convert.ToString(s[0]);
                    if (!string.IsNullOrEmpty(_valueDate))
                    {
                        string _tgl = _valueDate.Substring(6, 2);
                        string _bln = _valueDate.Substring(4, 2);
                        string _thn = _valueDate.Substring(0, 4);

                        _valueDate = _bln + "/" + _tgl + "/" + _thn; //Convert.ToDateTime(_strTransactionDate).ToString("MM/dd/yyyy");
                    }
                    using (SqlCommand cmd1 = DbCon.CreateCommand())
                    {
                        cmd1.CommandText = "delete FxdTemp where Date = @Date";
                        cmd1.Parameters.AddWithValue("@Date", _valueDate);
                        cmd1.ExecuteNonQuery();
                    }
                }
                }
                    // import data ke temp dulu
                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(Tools.conString, SqlBulkCopyOptions.TableLock))
                    {
                        bulkCopy.DestinationTableName = "dbo.FxdTemp";
                        bulkCopy.WriteToServer(CreateDataTableFromFxdCsv(_fileSource));

                    }

                    // logic kalo Reconcile success
                    using (SqlConnection conn = new SqlConnection(Tools.conString))
                    {
                        conn.Open();
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.CommandText =
                              @"if Exists( Select * from FxdTemp 
where Code not in
(
	select nkpdname from fund where status in (1,2)
))
BEGIN

declare @Message nvarchar(max)
set @Message = ' '
select distinct @Message = @Message +' , ' + Code  from fxdtemp 
	where Code not in
		(
			select nkpdname from fund where status in (1,2)
		)

	Select top 1  'false' result,@Message ResultDesc from fxdtemp 
	where Code not in
		(
			select nkpdname from fund where status in (1,2)
		)
END
ELSE
BEGIN
	select 'true' result,'' ResultDesc
END";

                            using (SqlDataReader dr = cmd.ExecuteReader())
                            {
                                dr.Read();

                                if (Convert.ToString(dr["Result"]) == "false")
                                {
                                    _msg = Convert.ToString(dr["ResultDesc"]);

                                }
                                else
                                {
                                    conn.Close();
                                    conn.Open();
                                    _msg = "Import Fxd 11 Success";
                                    using (SqlCommand cmd2 = conn.CreateCommand())
                                    {
                                        cmd2.CommandText =
                                    @" 
                                Declare @Date Datetime
                                Declare @FundPK int
                                Declare @NAVUnit numeric(18,4)
                                Declare @TotalNav numeric(18,4)
                                Declare @CloseNavPK int
                                select @Date = Date from FxdTemp
                                Declare A Cursor For
                                select FundPK,TotalNav,NAVUnit from FxdTemp A
                                left join Fund b on a.Code = b.NKPDName and B.status = 2


                                Open A
                                Fetch next From A
                                into @FundPK,@TotalNav,@NAVUnit
                                While @@Fetch_status = 0
                                BEGIN


                                IF EXISTS (select distinct * from CloseNav where Status in (1,2) and Date = @Date and FundPK = @FundPK) 
                                BEGIN

                                        update CloseNav set AUM = @TotalNav, Nav = @NAVUnit 
                                        where FundPK = @FundPK and Date = @Date

                                END
                                ELSE
                                BEGIN

                                Select @CloseNavPK = max(CloseNavPK) + 1 from CloseNAV
                                set @CloseNavPK = isnull(@CloseNavPK,1)

                                insert into 
                                [dbo].[CloseNAV]
                                ([CloseNavPK]
                                ,[HistoryPK]
                                ,[Status]
                                ,[Selected]
                                ,[Date]
                                ,[FundPK]
                                ,[AUM]
                                ,[Nav]   
                                )

                                select @CloseNavPK,1,1,1,@Date,@fundPK,@TotalNav,@NAVUnit 

                                 
                                END

                                fetch next From A into @FundPK,@TotalNav,@NAVUnit
                                end
                                Close A
                                Deallocate A
                            ";
                                        cmd2.Parameters.AddWithValue("@UserID", _userID);
                                        cmd2.Parameters.AddWithValue("@Datetime", _dateTimeNow);
                                        using (SqlDataReader dr01 = cmd2.ExecuteReader())
                                        {
                                            if (dr01.HasRows)
                                            {
                                                dr01.Read();
                                                return Convert.ToString(dr01["Result"]);

                                            }
                                        }
                                    }


                                }
                            }
                        }

                        return _msg;

                    }
                

            }
            catch (Exception err)
            {
                throw err;
            }
        }

        private DataTable CreateDataTableFromFxdCsv(string _path)
        {
            DataTable dt = new DataTable();
            DataColumn dc;
            DataRow dr;

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Date";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Code";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "InvestmentMoneyMarket";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "InvestmentOtherDebt";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "InvestmentEquity";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "InvestmentWarranRight";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Cash";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "DividendReceivable";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "InterestReceivable";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "ReceivableonSecuritySold";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "OtherReceivable";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "PrepaidTax";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "TotalAssets";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "PayableonSecurityPurchase";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "OtherPayable";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "TotalLiabilities";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "TotalNetAssets";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Subscription";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Redemption";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "RetainedEarning";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "DistributedIncome";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "UnrealizedGainLoss";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "RealizedGainLoss";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "NetIncome";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "TotalNAV";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "OutstandingUnitIssued";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "NAVUnit";
            dc.Unique = false;
            dt.Columns.Add(dc);


            StreamReader sr = new StreamReader(_path);
            string input;

            sr.ReadLine();
            while ((input = sr.ReadLine()) != null)
            {
                string[] s = input.Split(new char[] { ',' });
                dr = dt.NewRow();

                string _strDate = Convert.ToString(s[0]);
                if (!string.IsNullOrEmpty(_strDate))
                {
                    string _tgl = _strDate.Substring(6, 2);
                    string _bln = _strDate.Substring(4, 2);
                    string _thn = _strDate.Substring(0, 4);

                    _strDate = _bln + "/" + _tgl + "/" + _thn; //Convert.ToDateTime(_strTransactionDate).ToString("MM/dd/yyyy");
                }


                dr["Date"] = _strDate;
                dr["Code"] = s[1];
                dr["InvestmentMoneyMarket"] = s[2];
                dr["InvestmentOtherDebt"] = s[3];
                dr["InvestmentEquity"] = s[4];
                dr["InvestmentWarranRight"] = s[5];
                dr["Cash"] = s[6];
                dr["DividendReceivable"] = s[7];
                dr["InterestReceivable"] = s[8];
                dr["ReceivableonSecuritySold"] = s[9];
                dr["OtherReceivable"] = s[10];
                dr["PrepaidTax"] = s[11];
                dr["TotalAssets"] = s[12];
                dr["PayableonSecurityPurchase"] = s[13];
                dr["OtherPayable"] = s[14];
                dr["TotalLiabilities"] = s[15];
                dr["TotalNetAssets"] = s[16];
                dr["Subscription"] = s[17];
                dr["Redemption"] = s[18];
                dr["RetainedEarning"] = s[19];
                dr["DistributedIncome"] = s[20];
                dr["UnrealizedGainLoss"] = s[21];
                dr["RealizedGainLoss"] = s[22];
                dr["NetIncome"] = s[23];
                dr["TotalNAV"] = s[24];
                dr["OutstandingUnitIssued"] = s[25];
                dr["NAVUnit"] = s[26];

                dt.Rows.Add(dr);
            }
            sr.Close();
            return dt;
        }


        public string CheckHassAdd(string _dateFrom, string _dateTo)
        {
            try
            {
                var _msg = "";
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    string date = System.DateTime.Today.ToString("MM/dd/yyyy");
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"
                        IF EXISTS(
                        SELECT * FROM CloseNAV A
                        INNER JOIN CloseNAV B ON A.FundPK = B.FundPK AND A.Date = B.Date 
                        WHERE A.Status = 1 AND B.status = 2 and A.Selected = 1 and A.Approved1 = 1 and A.Approved2 = 1 and A.Date Between @DateFrom and @DateTo
                        )
                        BEGIN

	                        SELECT 'true' ReturnDesc

                        END
                        ELSE
                        BEGIN
	                        SELECT 'false' ReturnDesc
                        END
                        ";
                        cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _dateTo);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return _msg = Convert.ToString(dr["ReturnDesc"]);
                            }
                        }
                    }
                }
                return _msg;
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public List<CloseNavByDate> GetNavByDate(string _date, string _dateTo)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<CloseNavByDate> List = new List<CloseNavByDate>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"
                             select A.Date,B.ID,B.Name,A.NAV from closeNAV A
                             left join Fund B on A.FundPK = B.FundPK and B.status in (1,2)
                             where A.Date between @Date and @DateTo and A.status = 2
                             order by A.Date Desc
                               ";
                        cmd.Parameters.AddWithValue("@Date", _date);
                        cmd.Parameters.AddWithValue("@DateTo", _dateTo);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {

                                while (dr.Read())
                                {
                                    CloseNavByDate model = new CloseNavByDate();
                                    model.Date = Convert.ToDateTime(dr["Date"]);
                                    model.ID = Convert.ToString(dr["ID"]);
                                    model.Name = Convert.ToString(dr["Name"]);
                                    model.Nav = Convert.ToDecimal(dr["NAV"]);

                                    List.Add(model);
                                }

                            }
                            return List;

                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }


        }

        public CloseNav Validate_GetCloseNavYesterday(DateTime _valueDate, CloseNav _closeNAV)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        string _paramFund = "";

                        if (!_host.findString(_closeNAV.FundFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_closeNAV.FundFrom))
                        {
                            _paramFund = "And FundPK in ( " + _closeNAV.FundFrom + " ) ";
                        }
                        else
                        {
                            _paramFund = "";
                        }

                        cmd.CommandText = @"
                        
                        
Declare @CFundPK int
Declare @IssueDate datetime                        

Declare @A Table (Result int, Notes nvarchar(max))

DECLARE A CURSOR FOR

select distinct FundPK,isnull(IssueDate,'01/01/1900') from Fund where status in (1,2) and MaturityDate >= @ValueDate " + _paramFund + @"

Open A
Fetch Next From A
Into @CFundPK,@IssueDate

While @@FETCH_STATUS = 0
Begin

IF (@IssueDate < @valuedate)
BEGIN
    -- FUND LAMA
	IF NOT Exists(select * From CloseNav A
	left join Fund B on A.FundPK = B.FundPK and B.status in (1,2)
	where A.Status in (1,2) and Date = dbo.Fworkingday(@ValueDate,-1) and B.IssueDate < @ValueDate and A.FundPK = @CFundPK ) 
	BEGIN 

		insert into @A
		Select 1 Result,'Please Check Close Nav Yesterday, Fund : ' + ID Notes from Fund where FundPk = @CFundPK and status in (1,2)

	END 
	ELSE 
	BEGIN 
		insert into @A
		Select 0 Result , '' Notes
	END
END
ELSE IF (@IssueDate > @valuedate)
BEGIN
	insert into @A
	Select 2 Result,'Please Check Issue Date Fund : ' + ID Notes from Fund where FundPk = @CFundPK and status in (1,2)
END
ELSE
BEGIN
	insert into @A
	Select 0 Result , '' Notes
END
Fetch next From A 
Into @CFundPK,@IssueDate
end
Close A
Deallocate A

select top 1 Result,Notes from @A
order by Result desc
                        ";
                        cmd.Parameters.AddWithValue("@ValueDate", _valueDate);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return new CloseNav()
                                {
                                    Result = dr["Result"].Equals(DBNull.Value) == true ? 0 : Convert.ToInt32(dr["Result"]),
                                    Notes = dr["Notes"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["Notes"]),
                                };
                            }
                            else
                            {
                                return new CloseNav()
                                {
                                    Result = 0,
                                    Notes = "",

                                };
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


        public string CloseNavSInvestFromTxt(string _fileName, string _userID)
        {
            string _msg = "";
            using (SqlConnection conn = new SqlConnection(Tools.conString))
            {
                conn.Open();
                using (SqlCommand cmd2 = conn.CreateCommand())
                {
                    cmd2.CommandText = "truncate table CloseNavSInvestTemp";
                    cmd2.ExecuteNonQuery();
                }
            }

            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(Tools.conString, SqlBulkCopyOptions.TableLock))
            {
                bulkCopy.DestinationTableName = "dbo.CloseNavSInvestTemp";
                bulkCopy.WriteToServer(CreateDataTableFromCloseNavSInvestTempDataTxtFile(_fileName));
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

CREATE TABLE #ValidateCheckFund
(
SInvestFundCode nvarchar(50)
)

                        declare @Date datetime
                        select @Date = NAVDate from CloseNAVSInvestTemp


                        update CloseNAV set status = 3 where date = @Date and fundpk in (
							select FundPK from Fund A
							inner join CloseNAVSInvestTemp B on A.SInvestCode = B.FundCode
							where A.Status in (1,2)
						)

insert into #ValidateCheckFund
select A.FundCode from CloseNAVSInvestTemp A 
left join Fund B on A.FundCode = B.SInvestCode and B.status in(1,2)  
where B.SInvestCode is null
                                                
                        Declare @CloseNAVPK int        
                        select @CloseNAVPK = isnull(max(CloseNAVPK),0) From closeNav   
 
                                               
                        if exists(
                        select SInvestFundCode from #ValidateCheckFund)
                        BEGIN

	                        DECLARE @combinedString VARCHAR(MAX)
	                        SELECT @combinedString = COALESCE(@combinedString + ', ', '') + SInvestFundCode
	                        FROM #ValidateCheckFund

	                        SELECT 'Import Cancel, Please Check Sinvest Code : ' + @combinedString as Msg

                        END
                        ELSE
                        BEGIN
	                        INSERT INTO CLOSENAV (CloseNAVPK,HistoryPK,Selected,Status,Date,FundPK,AUM,Nav,OutstandingUnit,EntryUsersID,Entrytime,LastUpdate)
	                        SELECT ROW_NUMBER() over(order by B.FundPK) + @CloseNAVPK,1,0,1,cast(A.NAVDate as date),B.FundPK,A.NAV,A.NAVperUnit,isnull(A.OutstandingUnit,0),@UserID,@Lastupdate,@Lastupdate from CloseNAVSInvestTemp A 
	                        Left Join Fund B on A.FundCode = B.SInvestCode and B.status in(1,2)


	                        select 'Import Close NAV Success, ' + cast(count(*) as nvarchar) + ' data has been updated' Msg from CloseNAVSInvestTemp A 
	                        Left Join Fund B on A.FundCode = B.SInvestCode and B.status in(1,2)

                        END
                        ";
                        cmd.CommandTimeout = 0;
                        cmd.Parameters.AddWithValue("@UserID", _userID);
                        cmd.Parameters.AddWithValue("@Lastupdate", _dateTimeNow);
                        cmd.ExecuteNonQuery();
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                _msg = Convert.ToString(dr["Msg"]);
                            }

                            return _msg;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        private DataTable CreateDataTableFromCloseNavSInvestTempDataTxtFile(string _fileName)
        {
            DataTable dt = new DataTable();
            DataColumn dc;
            DataRow dr;

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "NAVDate";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "FundCode";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "FundName";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "FundType";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "IMCode";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "IMName";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "CBCode";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "CBName";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "FundCCY";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.Decimal");
            dc.ColumnName = "NAVperUnit";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.Decimal");
            dc.ColumnName = "NAV";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.Decimal");
            dc.ColumnName = "MonthlyPerformance";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.Decimal");
            dc.ColumnName = "Last1YearPerformance";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.Decimal");
            dc.ColumnName = "Last1YearRealPerformance";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.Decimal");
            dc.ColumnName = "OutstandingUnit";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "InputDateTime";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "AmendmentDateTime";
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
                dr["NAVDate"] = _valueDate;
                dr["FundCode"] = s[1];
                dr["FundName"] = s[2];
                dr["FundType"] = "";
                dr["IMCode"] = s[3];
                dr["IMName"] = s[4];
                dr["CBCode"] = s[5];
                dr["CBName"] = s[6];
                dr["FundCCY"] = "";
                dr["NAVperUnit"] = s[7];
                dr["NAV"] = s[8];
                dr["MonthlyPerformance"] = s[9];
                dr["Last1YearPerformance"] = s[10];
                dr["Last1YearRealPerformance"] = s[11];
                dr["OutstandingUnit"] = s[12];
                dt.Rows.Add(dr);
            }
            sr.Close();
            return dt;
        }

        public string ImportCloseNavSInvestFromExcel(string _fileSource, string _userID)
        {
            string _msg = "";
            try
            {
                DateTime _now = DateTime.Now;

                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    //delete data yang lama
                    using (SqlConnection conn = new SqlConnection(Tools.conString))
                    {
                        conn.Open();
                        using (SqlCommand cmd1 = conn.CreateCommand())
                        {
                            cmd1.CommandText = "truncate table CloseNavSInvestTemp";
                            cmd1.ExecuteNonQuery();
                        }
                    }

                    // import data ke temp dulu
                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(Tools.conString, SqlBulkCopyOptions.TableLock))
                    {
                        bulkCopy.DestinationTableName = "dbo.CloseNavSInvestTemp";
                        bulkCopy.WriteToServer(CreateDataTableFromCloseNavTempExcelFile(_fileSource));
                        //_msg = "Import Close Nav Success";
                    }

                    // logic kalo Reconcile success
                    using (SqlConnection conn = new SqlConnection(Tools.conString))
                    {
                        conn.Open();
                        using (SqlCommand cmd1 = conn.CreateCommand())
                        {
                            DateTime _dateTimeNow = DateTime.Now;
                            cmd1.CommandText = @" 
                                            declare @Date datetime
                                            select @Date = NAVDate from CloseNAVSInvestTemp
                    
                                            update CloseNAV set status = 3 where date = @Date and fundpk in (
												select FundPK from Fund A
												inner join CloseNAVSInvestTemp B on A.SInvestCode = B.FundCode
												where A.Status in (1,2)
											)
                                                                    
                                            Declare @CloseNAVPK int        
                                            select @CloseNAVPK = isnull(max(CloseNAVPK),0) From closeNav    
                                                                   
                    
                                            --declare @FundPK int
                                            --select @FundPK = B.FundPK from CloseNAVSInvestTemp A 
                                            --left join Fund B on A.FundCode = B.SInvestCode and B.status in(1,2)  
                                    
                                            INSERT INTO CLOSENAV (CloseNAVPK,HistoryPK,Selected,Status,Date,FundPK,AUM,Nav,EntryUsersID,Entrytime,LastUpdate)
                                            SELECT ROW_NUMBER() over(order by B.FundPK) + @CloseNAVPK,1,0,1,cast(A.NAVDate as date),B.FundPK,A.NAV,A.NAVperUnit, @UserID,@Lastupdate,@Lastupdate from CloseNAVSInvestTemp A 
                                            Left Join Fund B on A.FundCode = B.SInvestCode and B.status in(1,2)


                                            select 'Import Close NAV Success, ' + cast(count(*) as nvarchar) + ' data has been updated' A from CloseNAVSInvestTemp A 
                                            Left Join Fund B on A.FundCode = B.SInvestCode and B.status in(1,2)
                                            ";
                            cmd1.Parameters.AddWithValue("@UserID", _userID);
                            cmd1.Parameters.AddWithValue("@Lastupdate", _dateTimeNow);

                            using (SqlDataReader dr = cmd1.ExecuteReader())
                            {
                                if (dr.HasRows)
                                {
                                    dr.Read();
                                    _msg = Convert.ToString(dr["A"]);
                                }

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


        //------------------------------Approve 1
        public void CloseNav_Approve1BySelected(string _usersID, string _permissionID, DateTime _dateFrom, DateTime _dateTo, CloseNav _closeNAV)
        {
            try
            {
                string paramCloseNAVSelected = "";
                paramCloseNAVSelected = "CloseNAVPK in (" + _closeNAV.CloseNavSelected + ") ";

                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"
                        Declare @IPAddress nvarchar(50) select @IPAddress = IPAddress from Users where ID = @UsersID and Status = 2
                        Insert into Activity(Time,PermissionID,ObjectTable,ObjectTablePK,Status,Message,UsersID,IPAddress,LastUpdate)
                        Select @Time,@PermissionID,'CloseNav',CloseNavPK,1,'Approve by Selected Data',@UsersID,@IPAddress,@Time  from CloseNav where Date between @DateFrom and @DateTo and Status = 1 and " + paramCloseNAVSelected + @"
                       
                        update CloseNav set Approved1 = 1,ApprovedUsersID = @UsersID,ApprovedTime = @Time,LastUpdate=@Time 
                        where status = 1 and CloseNavPK in ( Select CloseNavPK from CloseNav where Date between @DateFrom and @DateTo and Status = 1 and " + paramCloseNAVSelected + @")  

                        ";
                        cmd.Parameters.AddWithValue("@PermissionID", _permissionID);
                        cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _dateTo);
                        cmd.Parameters.AddWithValue("@UsersID", _usersID);
                        cmd.Parameters.AddWithValue("@Time", _datetimeNow);
                        cmd.ExecuteNonQuery();

                        if (Tools.RDOSync)
                        {
                            var data = BackOffice.ExtractNavTable(FoConnection.Authentication());
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }


        //------------------------------Approve 2
        public void CloseNav_Approve2BySelected(string _usersID, string _permissionID, DateTime _dateFrom, DateTime _dateTo, CloseNav _closeNAV)
        {
            try
            {
                string paramCloseNAVSelected = "";
                paramCloseNAVSelected = "CloseNAVPK in (" + _closeNAV.CloseNavSelected + ") ";

                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"
                        Declare @IPAddress nvarchar(50) select @IPAddress = IPAddress from Users where ID = @UsersID and Status = 2
                        Insert into Activity(Time,PermissionID,ObjectTable,ObjectTablePK,Status,Message,UsersID,IPAddress,LastUpdate)
                        Select @Time,@PermissionID,'CloseNav',CloseNavPK,1,'Approve by Selected Data',@UsersID,@IPAddress,@Time  from CloseNav where Date between @DateFrom and @DateTo and Status = 1 and " + paramCloseNAVSelected + @" 
                       
                        update CloseNav set Approved2 = 1,ApprovedUsersID = @UsersID,ApprovedTime = @Time,LastUpdate=@Time 
                        where status = 1 and CloseNavPK in ( Select CloseNavPK from CloseNav where Date between @DateFrom and @DateTo and Status = 1 and " + paramCloseNAVSelected + @" )  

                        ";
                        cmd.Parameters.AddWithValue("@PermissionID", _permissionID);
                        cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _dateTo);
                        cmd.Parameters.AddWithValue("@UsersID", _usersID);
                        cmd.Parameters.AddWithValue("@Time", _datetimeNow);
                        cmd.ExecuteNonQuery();

                        if (Tools.RDOSync)
                        {
                            var data = BackOffice.ExtractNavTable(FoConnection.Authentication());
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }


        public NavProjection CloseNav_GetCompareCloseNavByFundPK(int _fundPK, string _date)
        {

            try
            {

                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    DateTime _dateTimeNow = DateTime.Now;
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = @"
 
                        Declare @NAVRoundingMode int 
                        Declare @NAVDecimalPlaces int
                        Declare @Nav numeric(18,6)
                        Declare @NavYesterday numeric(18,6)


                       
                        DECLARE @DateUnit DATETIME 

                        SELECT @DateUnit = Max(date) 
                        FROM   fundclientposition 
                        WHERE  date = (SELECT Max(date) 
                        FROM   fundclientposition 
                        WHERE  date < @date) 
    
                        SELECT @NAV = Case when 
                        [dbo].[Fgettotalunitbyfundpk](@DateUnit, @fundpk) = 0 then 1000 else Case
                        WHEN ( 
                        [dbo].[Fgetgroupaccountfundjournalbalancebyfundpk](@date, 1, @FundPK) - 
                        [dbo].[Fgetgroupaccountfundjournalbalancebyfundpk](@date, 63, @FundPK) ) 
                        = 0 
                        AND [dbo].[Fgettotalunitbyfundpk](@DateUnit, @fundpk) = 0 THEN 1000 
                        ELSE ( 
                        [dbo].[Fgetgroupaccountfundjournalbalancebyfundpk](@date, 1, @FundPK) - 
                        [dbo].[Fgetgroupaccountfundjournalbalancebyfundpk](@date, 63, @FundPK) ) / 
                        [dbo].[Fgettotalunitbyfundpk](@DateUnit, @FundPK) 
                        END END
                        


                        
                        Select @NAVRoundingMode = NAVRoundingMode, @NAVDecimalPlaces = NAVDecimalPlaces
                        From Fund Where FundPK = @FundPK and Status = 2

                        If @NAVRoundingMode = 1 BEGIN Set  @NAV = ROUND(@NAV,@NAVDecimalPlaces) 
	                        IF @NAVDecimalPlaces = 0 BEGIN
		                        set @NAV = @NAV + 1
	                        END 
	                        IF @NAVDecimalPlaces = 2 BEGIN
		                        set @NAV = @NAV + 0.01
	                        END 
	                        IF @NAVDecimalPlaces = 4 BEGIN
		                        set @NAV = @NAV + 0.0001
	                        END 
	                        IF @NAVDecimalPlaces = 6 BEGIN
		                        set @NAV = @NAV + 0.000001
	                        END 
	                        IF @NAVDecimalPlaces = 8 BEGIN
		                        set @NAV = @NAV + 0.00000001
	                        END 


                        END 

                        If @NAVRoundingMode = 2 BEGIN Set  @NAV = ROUND(@NAV,@NAVDecimalPlaces,1)   END 
                        If @NAVRoundingMode = 3 BEGIN Set  @NAV = ROUND(@NAV,@NAVDecimalPlaces)  END 

                        Select @NavYesterday = Nav From CloseNav Where FundPK= @FundPK and Date=dbo.FworkingDay(@Date,-1) and status=2

                        Select isnull(sum(@Nav-@NavYesterday),0) ChangeNav,isnull((sum(@Nav/@NavYesterday)-1) * 100,0) ChangeNavPercent
                        
                    ";
                        cmd.CommandTimeout = 0;
                        cmd.Parameters.AddWithValue("@Date", _date);
                        cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return new NavProjection()
                                {
                                    ChangeNav = Convert.ToDecimal(dr["ChangeNav"]),
                                    ChangeNavPercent = Convert.ToDecimal(dr["ChangeNavPercent"])
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


        public decimal CloseNav_GetCloseNavYesterdayByFundPK(int _fundPK, DateTime _date)
        {

            try
            {

                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();

                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Select isnull(Nav,0) Nav From CloseNav " +
                            "Where FundPK= @FundPK and Date=dbo.FworkingDay(@Date,-1) and status=2";
                        cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                        cmd.Parameters.AddWithValue("@Date", _date);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return Convert.ToDecimal(dr["Nav"]);
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


        public string Validate_CheckCloseNavApproveForUnitRegistry(DateTime _dateFrom, DateTime _dateTo, string _table, ParamUnitRegistryBySelected _paramUnitRegistryBySelected)
        {
            try
            {
                string paramSelected = "";
                if (!_host.findString(_paramUnitRegistryBySelected.UnitRegistrySelected.ToLower(), "0", ",") && !string.IsNullOrEmpty(_paramUnitRegistryBySelected.UnitRegistrySelected))
                {
                    paramSelected = " and " + _table + "PK in (" + _paramUnitRegistryBySelected.UnitRegistrySelected + ") ";
                }
                else
                {
                    paramSelected = " and " + _table + "PK in (0) ";
                }

                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = 

                        @"
                        Declare @CFundPK int

                        Declare @Result table
                        (
                        Result int, ID nvarchar(50)
                        )

                        DECLARE A CURSOR FOR 
                        select distinct FundPK from " + _table + @" where status = 2 and Posted = 0 and ValueDate between @DateFrom and @DateTo " + paramSelected + @"

                        Open A
                        Fetch Next From A
                        Into @CFundPK

                        While @@FETCH_STATUS = 0
                        BEGIN  

                        IF NOT EXISTS(select * from CloseNav where FundPK = @CFundPK and status = 2 and Date between @DateFrom and @DateTo)
                        BEGIN
	                        insert into @Result
	                        select 1, ID From Fund where status in (1,2) and FundPk = @CFundPK
                        END

                        Fetch next From A                   
                        Into @CFundPK
                        END                  
                        Close A                  
                        Deallocate A

                        DECLARE @combinedString VARCHAR(MAX)
                        IF NOT EXISTS(select * from CloseNav where FundPK = @CFundPK and status = 2 and Date between @DateFrom and @DateTo)
                        BEGIN
                        SELECT @combinedString = COALESCE(@combinedString + ', ', '') + id
                        FROM @Result
                        SELECT 'Please Check Close Nav : ' + @combinedString as Result 
                        END
                        ELSE
                        BEGIN
                        select '' Result
                        END ";

                        cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _dateTo);
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

        public bool Validate_AddCloseNav(DateTime _valueDate, CloseNav _closeNAV)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        string _paramFund = "";

                        if (!_host.findString(_closeNAV.FundFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_closeNAV.FundFrom))
                        {
                            _paramFund = "And A.FundPK in ( " + _closeNAV.FundFrom + " ) ";
                        }
                        else
                        {
                            _paramFund = "";
                        }

                        cmd.CommandText = @"if Exists(select * From CloseNav A
                        left join Fund B on A.FundPK = B.FundPK and B.status in (1,2)
                        where A.Status in (1,2) and B.Type <> 10  and Date = @ValueDate " + _paramFund + ") BEGIN Select 1 Result END ELSE BEGIN Select 0 Result END";
                        cmd.Parameters.AddWithValue("@ValueDate", _valueDate);
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

        public bool Validate_CheckCloseNavHasAdd(DateTime _valueDate, CloseNav _closeNAV)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {


                        cmd.CommandText = @"if Exists(select * From CloseNav A
                        left join Fund B on A.FundPK = B.FundPK and B.status in (1,2)
                        where A.Status in (1,2) and B.Type <> 10  and Date = @ValueDate and A.FundPK = @FundPK) BEGIN Select 1 Result END ELSE BEGIN Select 0 Result END";
                        cmd.Parameters.AddWithValue("@ValueDate", _valueDate);
                        cmd.Parameters.AddWithValue("@FundPK", _closeNAV.FundPK);
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

        public string Validate_CheckCloseNavBySelected(DateTime _date, ParamEndDayTrailsBySelected _paramEndDayTrailsBySelected)
        {
            try
            {

                string paramSelected = "";
                if (!_host.findString(_paramEndDayTrailsBySelected.FundFrom.ToLower(), "0", ",") && !string.IsNullOrEmpty(_paramEndDayTrailsBySelected.FundFrom))
                {
                    paramSelected = " and A.FundPK in (" + _paramEndDayTrailsBySelected.FundFrom + ") ";
                }
                else
                {
                    paramSelected = "";
                }

                if (Tools.ClientCode == "05")
                {
                    DateTime _dateTimeNow = DateTime.Now;
                    using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                    {
                        DbCon.Open();
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {

                            cmd.CommandText = @"
                            Create Table #CloseNavTemp
                            (ID nvarchar(50))
                        
                            Insert Into #CloseNavTemp(ID)
                            select B.ID from CloseNav A
                            left join Fund B on A.FundPK = B.FundPK and B.status in (1,2)
                            where Date = @Date and A.status in (1,2) " + paramSelected + @"

                            if exists(select B.ID from CloseNav A
                            left join Fund B on A.FundPK = B.FundPK and B.status in (1,2)
                            where Date = @Date and A.status in (1,2) " + paramSelected + @")
                            BEGIN
                            DECLARE @combinedString VARCHAR(MAX)
                            SELECT @combinedString = COALESCE(@combinedString + ', ', '') + ID
                            FROM #CloseNavTemp A
                            SELECT 'EDT Cancel, Please Check Fund in Close Nav : ' + @combinedString as Result 
                            END
                            ELSE
                            BEGIN
                            select '' Result
                            END    ";

                            cmd.Parameters.AddWithValue("@Date", _date);

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
                else
                {
                    return "";
                }

            }
            catch (Exception err)
            {
                throw err;
            }

        }



    }
}