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
    public class RL510CBondReps
    {
        Host _host = new Host();

        //2
        private RL510CBond setRL510CBond(SqlDataReader dr)
        {
            RL510CBond M_RL510CBond = new RL510CBond();
            M_RL510CBond.Date = Convert.ToString(dr["Date"]);
            M_RL510CBond.MKBDTrailsPK = Convert.ToInt32(dr["MKBDTrailsPK"]);
            M_RL510CBond.MKBDTrailsDesc = Convert.ToString(dr["MKBDTrailsDesc"]);
            M_RL510CBond.InstrumentPK = Convert.ToInt32(dr["InstrumentPK"]);
            M_RL510CBond.InstrumentID = Convert.ToString(dr["InstrumentID"]);
            M_RL510CBond.ObRating = Convert.ToString(dr["ObRating"]); 
            M_RL510CBond.InstrumentTypePK = Convert.ToInt32(dr["InstrumentTypePK"]);
            M_RL510CBond.InstrumentTypeID = Convert.ToString(dr["InstrumentTypeID"]);
            M_RL510CBond.HoldingPK = Convert.ToInt32(dr["HoldingPK"]);
            M_RL510CBond.HoldingID = Convert.ToString(dr["HoldingID"]);
            M_RL510CBond.MKBD01 = Convert.ToDecimal(dr["MKBD01"]);
            M_RL510CBond.MKBD02 = Convert.ToDecimal(dr["MKBD02"]);
            M_RL510CBond.MKBD03 = Convert.ToDecimal(dr["MKBD03"]);
            M_RL510CBond.MKBD09 = Convert.ToDecimal(dr["MKBD09"]);
            M_RL510CBond.Volume = Convert.ToDecimal(dr["Volume"]);
            M_RL510CBond.Price = Convert.ToDecimal(dr["Price"]);
            M_RL510CBond.ClosePrice = Convert.ToDecimal(dr["ClosePrice"]);
            M_RL510CBond.MarketValue = Convert.ToDecimal(dr["MarketValue"]);
            M_RL510CBond.HaircutPercent = Convert.ToDecimal(dr["HaircutPercent"]);
            M_RL510CBond.HaircutAmount = Convert.ToDecimal(dr["HaircutAmount"]);
            M_RL510CBond.AfterHaircutAmount = Convert.ToDecimal(dr["AfterHaircutAmount"]);
            M_RL510CBond.TotalEquity = Convert.ToDecimal(dr["TotalEquity"]);
            M_RL510CBond.ConcentrationRisk = Convert.ToDecimal(dr["ConcentrationRisk"]);
            M_RL510CBond.RankingLiabilities = Convert.ToDecimal(dr["RankingLiabilities"]);
            return M_RL510CBond;
        }

        public List<RL510CBond> RL510CBond_SelectByDateFromTo(int _status, DateTime _dateFrom, DateTime _dateTo)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<RL510CBond> L_RL510CBond = new List<RL510CBond>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText =
                            @"select isnull(b.LogMessages,'') MKBDTrailsDesc,isnull(c.Name,'') InstrumentID,isnull(d.id,'') InstrumentTypeID,isnull(e.Name,'') HoldingID, * from RL510CBond a 
                            left join MKBDTrails b on a.MKBDTrailsPK = b.MKBDTrailsPK
                            left join Instrument c on a.InstrumentPK = c.InstrumentPK and C.status = 2
                            left join InstrumentType d on a.InstrumentTypePK = d.InstrumentTypePK and D.status = 2
                            left join Holding e on a.HoldingPK =e.HoldingPK and E.status = 2
                            where Date between @DateFrom and @DateTo and A.status = 2 and B.status = 2  order By a.RL510CBondPK ";
                        }
                        else
                        {
                            cmd.CommandText =
                            @"select isnull(b.LogMessages,'') MKBDTrailsDesc,isnull(c.Name,'') InstrumentID,isnull(d.id,'') InstrumentTypeID,isnull(e.Name,'') HoldingID, * from RL510CBond a 
                            left join MKBDTrails b on a.MKBDTrailsPK = b.MKBDTrailsPK
                            left join Instrument c on a.InstrumentPK = c.InstrumentPK and C.status = 2
                            left join InstrumentType d on a.InstrumentTypePK = d.InstrumentTypePK and D.status = 2
                            left join Holding e on a.HoldingPK =e.HoldingPK and E.status = 2
                            where Date between @DateFrom and @DateTo and A.status = 2 and B.status = 2 order By a.RL510CBondPK";
                        }

                        cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _dateTo);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_RL510CBond.Add(setRL510CBond(dr));
                                }
                            }
                            return L_RL510CBond;
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