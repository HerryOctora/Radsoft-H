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
    public class RL510CSbnReps
    {
        Host _host = new Host();

        //2
        private RL510CSbn setRL510CSbn(SqlDataReader dr)
        {
            RL510CSbn M_RL510CSbn = new RL510CSbn();
            M_RL510CSbn.Date = Convert.ToString(dr["Date"]);
            M_RL510CSbn.DueDate = Convert.ToString(dr["DueDate"]);
            M_RL510CSbn.MKBDTrailsPK = Convert.ToInt32(dr["MKBDTrailsPK"]);
            M_RL510CSbn.MKBDTrailsDesc = Convert.ToString(dr["MKBDTrailsDesc"]);
            M_RL510CSbn.InstrumentPK = Convert.ToInt32(dr["InstrumentPK"]);
            M_RL510CSbn.InstrumentID = Convert.ToString(dr["InstrumentID"]);
            M_RL510CSbn.InstrumentTypePK = Convert.ToInt32(dr["InstrumentTypePK"]);
            M_RL510CSbn.InstrumentTypeID = Convert.ToString(dr["InstrumentTypeID"]);
            M_RL510CSbn.HoldingPK = Convert.ToInt32(dr["HoldingPK"]);
            M_RL510CSbn.HoldingID = Convert.ToString(dr["HoldingID"]);
            M_RL510CSbn.MKBD01 = Convert.ToDecimal(dr["MKBD01"]);
            M_RL510CSbn.MKBD02 = Convert.ToDecimal(dr["MKBD02"]);
            M_RL510CSbn.MKBD03 = Convert.ToDecimal(dr["MKBD03"]);
            M_RL510CSbn.MKBD09 = Convert.ToDecimal(dr["MKBD09"]);
            M_RL510CSbn.Volume = Convert.ToDecimal(dr["Volume"]);
            M_RL510CSbn.Price = Convert.ToDecimal(dr["Price"]);
            M_RL510CSbn.MarketValue = Convert.ToDecimal(dr["MarketValue"]);
            M_RL510CSbn.HaircutPercent = Convert.ToDecimal(dr["HaircutPercent"]);
            M_RL510CSbn.HaircutAmount = Convert.ToDecimal(dr["HaircutAmount"]);
            M_RL510CSbn.AfterHaircutAmount = Convert.ToDecimal(dr["AfterHaircutAmount"]);
            M_RL510CSbn.TotalEquity = Convert.ToDecimal(dr["TotalEquity"]);
            M_RL510CSbn.ConcentrationRisk = Convert.ToDecimal(dr["ConcentrationRisk"]);
            M_RL510CSbn.RankingLiabilities = Convert.ToDecimal(dr["RankingLiabilities"]);
            return M_RL510CSbn;
        }

        public List<RL510CSbn> RL510CSbn_SelectByDateFromTo(int _status, DateTime _dateFrom, DateTime _dateTo)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<RL510CSbn> L_RL510CSbn = new List<RL510CSbn>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText =
                            @"select isnull(b.LogMessages,'') MKBDTrailsDesc,isnull(c.Name,'') InstrumentID,isnull(d.id,'') InstrumentTypeID,isnull(e.Name,'') HoldingID, * from RL510CSbn a 
                            left join MKBDTrails b on a.MKBDTrailsPK = b.MKBDTrailsPK
                            left join Instrument c on a.InstrumentPK = c.InstrumentPK and C.status = 2
                            left join InstrumentType d on a.InstrumentTypePK = d.InstrumentTypePK  and D.status = 2
                            left join Holding e on a.HoldingPK =e.HoldingPK  and E.status = 2
                            where Date between @DateFrom and @DateTo and A.status = 2 and B.status = 2  and a.Status = 2 order By a.RL510CSbnPK ";
                        }
                        else
                        {
                            cmd.CommandText =
                            @"select isnull(b.LogMessages,'') MKBDTrailsDesc,isnull(c.Name,'') InstrumentID,isnull(d.id,'') InstrumentTypeID,isnull(e.Name,'') HoldingID, * from RL510CSbn a 
                            left join MKBDTrails b on a.MKBDTrailsPK = b.MKBDTrailsPK
                            left join Instrument c on a.InstrumentPK = c.InstrumentPK  and C.status = 2
                            left join InstrumentType d on a.InstrumentTypePK = d.InstrumentTypePK  and D.status = 2
                            left join Holding e on a.HoldingPK =e.HoldingPK  and E.status = 2
                            where Date between @DateFrom and @DateTo and A.status = 2 and B.status = 2  order By a.RL510CSbnPK";
                        }

                        cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _dateTo);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_RL510CSbn.Add(setRL510CSbn(dr));
                                }
                            }
                            return L_RL510CSbn;
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