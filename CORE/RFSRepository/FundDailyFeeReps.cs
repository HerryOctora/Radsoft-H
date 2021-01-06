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
    public class FundDailyFeeReps
    {
        Host _host = new Host();

        //2
        private FundDailyFee setFundDailyFee(SqlDataReader dr)
        {
            FundDailyFee M_FundDailyFee = new FundDailyFee();
            M_FundDailyFee.Date = Convert.ToString(dr["Date"]);
            M_FundDailyFee.FundPK = Convert.ToInt32(dr["FundPK"]);
            M_FundDailyFee.FundID = Convert.ToString(dr["FundID"]);
            M_FundDailyFee.FundName = Convert.ToString(dr["FundName"]);
            M_FundDailyFee.ManagementFeeAmount = Convert.ToDecimal(dr["ManagementFeeAmount"]);
            M_FundDailyFee.CustodiFeeAmount = Convert.ToDecimal(dr["CustodiFeeAmount"]);
            M_FundDailyFee.SubscriptionFeeAmount = Convert.ToDecimal(dr["SubscriptionFeeAmount"]);
            M_FundDailyFee.RedemptionFeeAmount = Convert.ToDecimal(dr["RedemptionFeeAmount"]);
            M_FundDailyFee.SwitchingFeeAmount = Convert.ToDecimal(dr["SwitchingFeeAmount"]);
            return M_FundDailyFee;
        }

        public List<FundDailyFee> FundDailyFee_SelectByDateFromTo(int _status, DateTime _dateFrom, DateTime _dateTo)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<FundDailyFee> L_FundDailyFee = new List<FundDailyFee>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText =
                            @"Select C.ID FundID,C.Name FundName, A.* from FundDailyFee A 
                            left join Fund C on A.FundPK = C.FundPK and C.status = 2 
                            where Date between @DateFrom and @DateTo order By C.Name ";
                        }
                        else
                        {
                            cmd.CommandText =
                            @"Select C.ID FundID,C.Name FundName, A.* from FundDailyFee A 
                            left join Fund C on A.FundPK = C.FundPK and C.status = 2 
                            where Date between @DateFrom and @DateTo order By C.Name";
                        }

                        cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _dateTo);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_FundDailyFee.Add(setFundDailyFee(dr));
                                }
                            }
                            return L_FundDailyFee;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }



        public int FundDailyFee_Update(FundDailyFee _FundDailyFee, bool _havePrivillege)
        {

            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();

                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"Update FundDailyFee set ManagementFeeAmount=@ManagementFeeAmount, 
                            LastUpdateDB=@Lastupdate 
                            where Date = @Date and FundPK = @FundPK";
                        cmd.Parameters.AddWithValue("@Date", _FundDailyFee.Date);
                        cmd.Parameters.AddWithValue("@FundPK", _FundDailyFee.FundPK);
                        cmd.Parameters.AddWithValue("@ManagementFeeAmount", _FundDailyFee.ManagementFeeAmount);
                        cmd.Parameters.AddWithValue("@CustodiFeeAmount", _FundDailyFee.CustodiFeeAmount);
                        cmd.Parameters.AddWithValue("@SubscriptionFeeAmount", _FundDailyFee.SubscriptionFeeAmount);
                        cmd.Parameters.AddWithValue("@RedemptionFeeAmount", _FundDailyFee.CustodiFeeAmount);
                        cmd.Parameters.AddWithValue("@SwitchingFeeAmount", _FundDailyFee.SwitchingFeeAmount);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);

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

        public string Validate_CheckNotExistsFundDailyFee(DateTime _dateFrom, DateTime _dateTo)
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
                        
                        create table #date 
                        (
                        valuedate datetime
                        )

                        insert into #date (valuedate)
                        SELECT  TOP (DATEDIFF(DAY, @datefrom, @dateto) + 1) Dates = DATEADD(DAY, ROW_NUMBER() OVER(ORDER BY a.object_id) - 1, @datefrom)
                        FROM sys.all_objects a CROSS JOIN sys.all_objects b

                        delete from #date where dbo.[CheckTodayIsHoliday](valuedate) = 1

                        delete from #date where ValueDate in 
                        (Select Date from FundDailyFee where date between @datefrom and @dateto)

                        IF EXISTS(select valuedate From #date)
                        BEGIN
                        DECLARE @combinedString VARCHAR(MAX)
                        SELECT @combinedString = COALESCE(@combinedString + ', ', '') + replace(convert(NVARCHAR, valuedate, 106), ' ', '/')
                        FROM #date 
                        SELECT 'Retrive Cancel, No Data Fund Daily Fee in Date : ' + @combinedString as Result 
                        END
                        ELSE
                        BEGIN
                        select '' Result
                        END    ";

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

    }
}