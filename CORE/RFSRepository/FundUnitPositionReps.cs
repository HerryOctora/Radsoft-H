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

namespace RFSRepository
{
    public class FundUnitPositionReps
    {
        Host _host = new Host();

        //2
        private FundUnitPosition setFundUnitPosition(SqlDataReader dr)
        {
            FundUnitPosition M_FundUnitPosition = new FundUnitPosition();
            //M_FundUnitPosition.FundUnitPositionPK = Convert.ToInt32(dr["FundUnitPositionPK"]);
            M_FundUnitPosition.Date = Convert.ToString(dr["Date"]);
            //M_FundUnitPosition.FundClientPK = Convert.ToInt32(dr["FundClientPK"]);
            //M_FundUnitPosition.FundClientID = Convert.ToString(dr["FundClientID"]);
            //M_FundUnitPosition.FundClientName = Convert.ToString(dr["FundClientName"]);
            M_FundUnitPosition.FundName = Convert.ToString(dr["FundName"]);
            M_FundUnitPosition.NAV = Convert.ToDecimal(dr["NAV"]);
            M_FundUnitPosition.NAVDate = Convert.ToString(dr["NAVDate"]);
            //M_FundUnitPosition.FundPK = Convert.ToInt32(dr["FundPK"]);
            M_FundUnitPosition.FundID = Convert.ToString(dr["FundID"]);
            M_FundUnitPosition.CurrencyID = dr["CurrencyID"].ToString();
            M_FundUnitPosition.UnitAmount = Convert.ToDecimal(dr["UnitAmount"]);
            //M_FundUnitPosition.SID = Convert.ToString(dr["SID"]);
            //M_FundUnitPosition.IFUA = Convert.ToString(dr["IFUA"]);
            M_FundUnitPosition.AUM = Convert.ToDecimal(dr["AUM"]);
            //M_FundUnitPosition.DBUserID = dr["DBUserID"].ToString();
            //M_FundUnitPosition.DBTerminalID = dr["DBTerminalID"].ToString();
            //M_FundUnitPosition.LastUpdate = dr["LastUpdate"].ToString();
            //M_FundUnitPosition.LastUpdateDB = Convert.ToString(dr["LastUpdateDB"]);
            return M_FundUnitPosition;
        }



        public List<FundUnitPosition> FundUnitPosition_SelectByDateFromTo(int _status, DateTime _dateFrom, DateTime _dateTo)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<FundUnitPosition> L_FundUnitPosition = new List<FundUnitPosition>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText =
                              @"declare @tableNav table
(
	FundPK int,
	NAV numeric(22,8),
	NAVDate date
)


insert into @tableNav
select A.FundPK,Nav,A.Date from CloseNAV A
inner join (
	select max(date) date,FundPK from CloseNAV where status = 2 and date <= @dateto and fundpk in(
	select distinct fundpk from fund where status = 2)
	group by FundPK
)B on A.FundPK = B.FundPK and A.Date = B.date
where A.fundpk in(
select distinct fundpk from fund where status = 2
) and status = 2


Select isnull(D.ID,'') CurrencyID,isnull(C.ID,'') FundID,isnull(C.Name,'') FundName,G.NAV,G.NAVDate,sum(A.UnitAmount * isnull(G.Nav,0)) AUM,sum(A.UnitAmount) UnitAmount,A.Date from FundClientPosition A 
left join FundClient B on A.FundClientPK = B.FundClientPK and B.status in (1,2) 
left join Fund C on A.FundPK = C.FundPK and C.status in (1,2)
left join Currency D on C.CurrencyPK = D.CurrencyPK and D.status in (1,2)
left join @tableNav G on A.FundPK = G.FundPK 
where  A.Date between @DateFrom and @DateTo 
group by D.ID,C.ID,C.Name,G.NAV,G.NAVDate,A.Date
order By C.ID  ";
                        }
                        else
                        {
                            cmd.CommandText =
                              @"declare @tableNav table
(
	FundPK int,
	NAV numeric(22,8),
	NAVDate date
)


insert into @tableNav
select A.FundPK,Nav,A.Date from CloseNAV A
inner join (
	select max(date) date,FundPK from CloseNAV where status = 2 and date <= @dateto and fundpk in(
	select distinct fundpk from fund where status = 2)
	group by FundPK
)B on A.FundPK = B.FundPK and A.Date = B.date
where A.fundpk in(
select distinct fundpk from fund where status = 2
) and status = 2


Select isnull(D.ID,'') CurrencyID,isnull(C.ID,'') FundID,isnull(C.Name,'') FundName,G.NAV,G.NAVDate,sum(A.UnitAmount * isnull(G.Nav,0)) AUM,sum(A.UnitAmount) UnitAmount,A.Date from FundClientPosition A 
left join FundClient B on A.FundClientPK = B.FundClientPK and B.status in (1,2) 
left join Fund C on A.FundPK = C.FundPK and C.status in (1,2)
left join Currency D on C.CurrencyPK = D.CurrencyPK and D.status in (1,2)
left join @tableNav G on A.FundPK = G.FundPK 
where  A.Date between @DateFrom and @DateTo 
group by D.ID,C.ID,C.Name,G.NAV,G.NAVDate,A.Date
order By C.ID  ";
                        }

                        cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _dateTo);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_FundUnitPosition.Add(setFundUnitPosition(dr));
                                }
                            }
                            return L_FundUnitPosition;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public decimal Validate_CashAmountByDate(int _fundPK, int _fundClientPK, DateTime _date)
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
                            select isnull(sum(B.Nav * A.UnitAmount),0) CashAmount from FundUnitPosition A
                            left join 
                                (
                                select * from CloseNAV Where  FundPK = @FundPK and Date =
                                (
                                    Select max(Date) From CloseNAV where Date <= @Date  and FundPK = @FundPK and status = 2
                                ) and status = 2
                                )B on A.FundPK = B.FundPK  and B.Status = 2
                            where  A.Date = (select max(Date) From FundUnitPosition where Date <= @date)
                            and A.FundPK = @FundPK and FundClientPK = @FundClientPK
                        ";
                        cmd.Parameters.AddWithValue("@FundClientPK", _fundClientPK);
                        cmd.Parameters.AddWithValue("@FundPK", _fundPK);
                        cmd.Parameters.AddWithValue("@Date", _date);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    return Convert.ToDecimal(dr["CashAmount"]);
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


    }
}