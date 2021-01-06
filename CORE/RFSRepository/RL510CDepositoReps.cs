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
    public class RL510CDepositoReps
    {
        Host _host = new Host();

        //2
        private RL510CDeposito setRL510CDeposito(SqlDataReader dr)
        {
            RL510CDeposito M_RL510CDeposito = new RL510CDeposito();
            M_RL510CDeposito.Date = Convert.ToString(dr["Date"]);
            M_RL510CDeposito.DueDate = Convert.ToString(dr["DueDate"]);
            M_RL510CDeposito.MKBDTrailsPK = Convert.ToInt32(dr["MKBDTrailsPK"]);
            M_RL510CDeposito.MKBDTrailsID = Convert.ToString(dr["MKBDTrailsID"]);
            M_RL510CDeposito.InstrumentPK = Convert.ToInt32(dr["InstrumentPK"]);
            M_RL510CDeposito.InstrumentID = Convert.ToString(dr["InstrumentID"]);
            M_RL510CDeposito.IsLiquidated = Convert.ToString(dr["IsLiquidated"]);
            M_RL510CDeposito.DepositoTypePK = Convert.ToInt32(dr["DepositoTypePK"]);
            M_RL510CDeposito.DepositoTypeID = Convert.ToString(dr["DepositoTypeID"]);
            M_RL510CDeposito.IsLiquidated = Convert.ToString(dr["IsLiquidated"]);
            M_RL510CDeposito.MKBD01 = Convert.ToDecimal(dr["MKBD01"]);
            M_RL510CDeposito.MKBD02 = Convert.ToDecimal(dr["MKBD02"]);
            M_RL510CDeposito.MKBD03 = Convert.ToDecimal(dr["MKBD03"]);
            M_RL510CDeposito.MKBD09 = Convert.ToDecimal(dr["MKBD09"]);
            M_RL510CDeposito.IsUnderDuedateAmount = Convert.ToDecimal(dr["IsUnderDuedateAmount"]);
            M_RL510CDeposito.MarketValue = Convert.ToDecimal(dr["MarketValue"]);
            M_RL510CDeposito.IsUnderDuedateAmount = Convert.ToDecimal(dr["IsUnderDuedateAmount"]);
            M_RL510CDeposito.NotUnderDuedateAmount = Convert.ToDecimal(dr["NotUnderDuedateAmount"]);
            M_RL510CDeposito.QuaranteedAmount = Convert.ToDecimal(dr["QuaranteedAmount"]);
            M_RL510CDeposito.NotLiquidatedAmount = Convert.ToDecimal(dr["NotLiquidatedAmount"]);
            M_RL510CDeposito.LiquidatedAmount = Convert.ToDecimal(dr["LiquidatedAmount"]);
            M_RL510CDeposito.MarketValue = Convert.ToDecimal(dr["MarketValue"]);
            M_RL510CDeposito.HaircutPercent = Convert.ToDecimal(dr["HaircutPercent"]);
            M_RL510CDeposito.HaircutAmount = Convert.ToDecimal(dr["HaircutAmount"]);
            M_RL510CDeposito.AfterHaircutAmount = Convert.ToDecimal(dr["AfterHaircutAmount"]);
            M_RL510CDeposito.RankingLiabilities = Convert.ToDecimal(dr["RankingLiabilities"]);
            M_RL510CDeposito.EntryUsersID = dr["EntryUsersID"].ToString();
            M_RL510CDeposito.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_RL510CDeposito.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_RL510CDeposito.VoidUsersID = dr["VoidUsersID"].ToString();
            M_RL510CDeposito.EntryTime = dr["EntryTime"].ToString();
            M_RL510CDeposito.UpdateTime = dr["UpdateTime"].ToString();
            M_RL510CDeposito.ApprovedTime = dr["ApprovedTime"].ToString();
            M_RL510CDeposito.VoidTime = dr["VoidTime"].ToString();
            M_RL510CDeposito.DBUserID = dr["DBUserID"].ToString();
            M_RL510CDeposito.DBTerminalID = dr["DBTerminalID"].ToString();
            M_RL510CDeposito.LastUpdate = dr["LastUpdate"].ToString();
            M_RL510CDeposito.LastUpdateDB = Convert.ToString(dr["LastUpdateDB"]);
            return M_RL510CDeposito;
        }

        public List<RL510CDeposito> RL510CDeposito_SelectByDateFromTo(int _status, DateTime _dateFrom, DateTime _dateTo)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<RL510CDeposito> L_RL510CDeposito = new List<RL510CDeposito>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText =
                            @"select isnull(b.LogMessages,'') MKBDTrailsID,isnull(c.Name,'') InstrumentID,isnull(d.DepType,'') DepositoTypeID, * from RL510CDeposito a 
                            left join MKBDTrails b on a.MKBDTrailsPK = b.MKBDTrailsPK
                            left join Instrument c on a.InstrumentPK = c.InstrumentPK and C.status = 2
                            left join DepositoType d on a.DepositoTypePK = d.DepositoTypePK and D.status = 2
                            where Date between @DateFrom and @DateTo and A.status = 2 and B.status = 2 order By a.RL510CDepositoPK ";
                        }
                        else
                        {
                            cmd.CommandText =
                            @"select isnull(b.LogMessages,'') MKBDTrailsID,isnull(c.Name,'') InstrumentID,isnull(d.DepType,'') DepositoTypeID, * from RL510CDeposito a 
                            left join MKBDTrails b on a.MKBDTrailsPK = b.MKBDTrailsPK
                            left join Instrument c on a.InstrumentPK = c.InstrumentPK and C.status = 2
                            left join DepositoType d on a.DepositoTypePK = d.DepositoTypePK and D.status = 2
                            where Date between @DateFrom and @DateTo and A.status = 2 and B.status = 2  order By a.RL510CDepositoPK";
                        }

                        cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _dateTo);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_RL510CDeposito.Add(setRL510CDeposito(dr));
                                }
                            }
                            return L_RL510CDeposito;
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