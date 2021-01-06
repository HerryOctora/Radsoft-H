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
    public class DailyDataForCommissionRptNewLogReps
    {
        Host _host = new Host();

        //2
        private DailyDataForCommissionRptNewLog setDailyDataForCommissionRptNewLog(SqlDataReader dr)
        {
            DailyDataForCommissionRptNewLog M_DailyDataForCommissionRptNewLog = new DailyDataForCommissionRptNewLog();
            M_DailyDataForCommissionRptNewLog.DailyDataForCommissionRptNewLogPK = Convert.ToInt32(dr["DailyDataForCommissionRptNewLogPK"]);
            M_DailyDataForCommissionRptNewLog.DateFrom = Convert.ToString(dr["DateFrom"]);
            M_DailyDataForCommissionRptNewLog.DateTo = Convert.ToString(dr["DateTo"]);
            M_DailyDataForCommissionRptNewLog.UsersID = Convert.ToString(dr["UsersID"]);
            M_DailyDataForCommissionRptNewLog.GenerateTime = Convert.ToString(dr["GenerateTime"]);
            M_DailyDataForCommissionRptNewLog.Fund = Convert.ToString(dr["Fund"]);
            M_DailyDataForCommissionRptNewLog.Client = Convert.ToString(dr["Client"]);
            M_DailyDataForCommissionRptNewLog.LastUpdate = dr["LastUpdate"].ToString();
            return M_DailyDataForCommissionRptNewLog;
        }

        public List<DailyDataForCommissionRptNewLog> DailyDataForCommissionRptNewLog_SelectByDateFromTo(int _status)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<DailyDataForCommissionRptNewLog> L_DailyDataForCommissionRptNewLog = new List<DailyDataForCommissionRptNewLog>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText =
                              @"select * from DailyDataForCommissionRptNewLog order by LastUpdate desc ";
                        }
                        else
                        {
                            cmd.CommandText =
                              @"select * from DailyDataForCommissionRptNewLog order by LastUpdate desc ";
                        }

                        //cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                        //cmd.Parameters.AddWithValue("@DateTo", _dateTo);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_DailyDataForCommissionRptNewLog.Add(setDailyDataForCommissionRptNewLog(dr));
                                }
                            }
                            return L_DailyDataForCommissionRptNewLog;
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
