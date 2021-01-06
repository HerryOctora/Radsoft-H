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
    public class RL510CEquityReps
    {
        Host _host = new Host();

        //2
        private RL510CEquity setRL510CEquity(SqlDataReader dr)
        {
            RL510CEquity M_RL510CEquity = new RL510CEquity();
            M_RL510CEquity.Date = Convert.ToString(dr["Date"]);
            M_RL510CEquity.MKBDTrailsPK = Convert.ToInt32(dr["MKBDTrailsPK"]);
            M_RL510CEquity.MKBDTrailsDesc = Convert.ToString(dr["MKBDTrailsDesc"]);
            M_RL510CEquity.InstrumentPK = Convert.ToInt32(dr["InstrumentPK"]);
            M_RL510CEquity.InstrumentID = Convert.ToString(dr["InstrumentID"]);
            M_RL510CEquity.InstrumentTypePK = Convert.ToInt32(dr["InstrumentTypePK"]);
            M_RL510CEquity.InstrumentTypeID = Convert.ToString(dr["InstrumentTypeID"]);
            M_RL510CEquity.HoldingPK = Convert.ToInt32(dr["HoldingPK"]);
            M_RL510CEquity.HoldingID = Convert.ToString(dr["HoldingID"]);
            M_RL510CEquity.MKBD01 = Convert.ToDecimal(dr["MKBD01"]);
            M_RL510CEquity.MKBD02 = Convert.ToDecimal(dr["MKBD02"]);
            M_RL510CEquity.MKBD03 = Convert.ToDecimal(dr["MKBD03"]);
            M_RL510CEquity.MKBD09 = Convert.ToDecimal(dr["MKBD09"]);
            M_RL510CEquity.Volume = Convert.ToDecimal(dr["Volume"]);
            M_RL510CEquity.Price = Convert.ToDecimal(dr["Price"]);
            M_RL510CEquity.ClosePrice = Convert.ToDecimal(dr["ClosePrice"]);
            M_RL510CEquity.MarketValue = Convert.ToDecimal(dr["MarketValue"]);
            M_RL510CEquity.HaircutPercent = Convert.ToDecimal(dr["HaircutPercent"]);
            M_RL510CEquity.HaircutAmount = Convert.ToDecimal(dr["HaircutAmount"]);
            M_RL510CEquity.AfterHaircutAmount = Convert.ToDecimal(dr["AfterHaircutAmount"]);
            M_RL510CEquity.TotalEquity = Convert.ToDecimal(dr["TotalEquity"]);
            M_RL510CEquity.ConcentrationRisk = Convert.ToDecimal(dr["ConcentrationRisk"]);
            M_RL510CEquity.RankingLiabilities = Convert.ToDecimal(dr["RankingLiabilities"]);
            return M_RL510CEquity;
        }

        public List<RL510CEquity> RL510CEquity_SelectByDateFromTo(int _status, DateTime _dateFrom, DateTime _dateTo)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<RL510CEquity> L_RL510CEquity = new List<RL510CEquity>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText =
                            @"select isnull(b.LogMessages,'') MKBDTrailsDesc,isnull(c.Name,'') InstrumentID,isnull(d.Name,'') InstrumentTypeID,isnull(e.Name,'') HoldingID, * from RL510CEquity a 
                            left join MKBDTrails b on a.MKBDTrailsPK = b.MKBDTrailsPK
                            left join Instrument c on a.InstrumentPK = c.InstrumentPK and C.status = 2
                            left join InstrumentType d on a.InstrumentTypePK = d.InstrumentTypePK and D.status = 2
                            left join Holding e on a.HoldingPK =e.HoldingPK and E.status = 2
                            where Date between @DateFrom and @DateTo and A.status = 2 and B.status = 2 order By a.RL510CEquityPK";
                        }
                        else
                        {
                            cmd.CommandText =
                            @"select isnull(b.LogMessages,'') MKBDTrailsDesc,isnull(c.Name,'') InstrumentID,isnull(d.Name,'') InstrumentTypeID,isnull(e.Name,'') HoldingID, * from RL510CEquity a 
                            left join MKBDTrails b on a.MKBDTrailsPK = b.MKBDTrailsPK
                            left join Instrument c on a.InstrumentPK = c.InstrumentPK and C.status = 2
                            left join InstrumentType d on a.InstrumentTypePK = d.InstrumentTypePK and D.status = 2
                            left join Holding e on a.HoldingPK =e.HoldingPK and E.status = 2
                            where Date between @DateFrom and @DateTo and A.status = 2 and B.status = 2 order By a.RL510CEquityPK";
                        }

                        cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _dateTo);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_RL510CEquity.Add(setRL510CEquity(dr));
                                }
                            }
                            return L_RL510CEquity;
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