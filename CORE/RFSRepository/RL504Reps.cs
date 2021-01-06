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
    public class RL504Reps
    {
        Host _host = new Host();

        //2
        private RL504 setRL504(SqlDataReader dr)
        {
            RL504 M_RL504 = new RL504();
            M_RL504.Date = Convert.ToString(dr["Date"]);
            M_RL504.MKBDTrailsPK = Convert.ToInt32(dr["MKBDTrailsPK"]);
            M_RL504.MKBDTrailsDesc = Convert.ToString(dr["MKBDTrailsDesc"]);
            M_RL504.InstrumentPK = Convert.ToInt32(dr["InstrumentPK"]);
            M_RL504.InstrumentID = Convert.ToString(dr["InstrumentID"]);
            M_RL504.ReksadanaTypePK = Convert.ToInt32(dr["ReksadanaTypePK"]);
            M_RL504.ReksadanaTypeID = Convert.ToString(dr["ReksadanaTypeID"]);
            M_RL504.Affiliated = Convert.ToBoolean(dr["Affiliated"]);
            M_RL504.MKBD01 = Convert.ToDecimal(dr["MKBD01"]);
            M_RL504.MKBD02 = Convert.ToDecimal(dr["MKBD02"]);
            M_RL504.MKBD03 = Convert.ToDecimal(dr["MKBD03"]);
            M_RL504.MKBD09 = Convert.ToDecimal(dr["MKBD09"]);
            M_RL504.Volume = Convert.ToDecimal(dr["Volume"]);
            M_RL504.CloseNAV = Convert.ToDecimal(dr["CloseNAV"]);
            M_RL504.TotalNAVReksadana = Convert.ToDecimal(dr["TotalNAVReksadana"]);
            M_RL504.MarketValue = Convert.ToDecimal(dr["MarketValue"]);
            M_RL504.HaircutPercent = Convert.ToDecimal(dr["HaircutPercent"]);
            M_RL504.HaircutAmount = Convert.ToDecimal(dr["HaircutAmount"]);
            M_RL504.ConcentrationLimit = Convert.ToDecimal(dr["ConcentrationLimit"]);
            M_RL504.ConcentrationRisk = Convert.ToDecimal(dr["ConcentrationRisk"]);
            M_RL504.RankingLiabilities = Convert.ToDecimal(dr["RankingLiabilities"]);
            return M_RL504;
        }

        public List<RL504> RL504_SelectByDateFromTo(int _status, DateTime _dateFrom, DateTime _dateTo)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<RL504> L_RL504 = new List<RL504>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText =
                            @"select isnull(b.LogMessages,'') MKBDTrailsDesc,isnull(c.Name,'') InstrumentID,isnull(d.Description,'') ReksadanaTypeID, * from RL504 a 
                            left join MKBDTrails b on a.MKBDTrailsPK = b.MKBDTrailsPK
                            left join Instrument c on a.InstrumentPK = c.InstrumentPK and C.status = 2
                            left join ReksaDanaType d on a.ReksaDanaTypePK = d.ReksaDanaTypePK and D.status = 2
                            where Date between @DateFrom and @DateTo and A.status = 2 and B.status = 2 order By a.RL504PK ";
                        }
                        else
                        {
                            cmd.CommandText =
                            @"select isnull(b.LogMessages,'') MKBDTrailsDesc,isnull(c.Name,'') InstrumentID,isnull(d.Description,'') ReksadanaTypeID, * from RL504 a 
                            left join MKBDTrails b on a.MKBDTrailsPK = b.MKBDTrailsPK
                            left join Instrument c on a.InstrumentPK = c.InstrumentPK and C.status = 2
                            left join ReksaDanaType d on a.ReksaDanaTypePK = d.ReksaDanaTypePK and D.status = 2
                            where Date between @DateFrom and @DateTo and A.status = 2 and B.status = 2 order By a.RL504PK";
                        }

                        cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _dateTo);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_RL504.Add(setRL504(dr));
                                }
                            }
                            return L_RL504;
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