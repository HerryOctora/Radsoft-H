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
using System.Drawing;
using System.Data;


namespace RFSRepository
{
    public class CurrencyRateReps
    {
        Host _host = new Host();
       
        //1
        string _insertCommand = "INSERT INTO [dbo].[CurrencyRate] " +
                            "([CurrencyRatePK],[HistoryPK],[Status],[Date],[CurrencyPK],[Rate],[ProductRate],[TaxRate],";
        string _paramaterCommand = "@Date,@CurrencyPK,@Rate,@ProductRate,@TaxRate,";

        //2
        private CurrencyRate setCurrencyRate(SqlDataReader dr)
        {
            CurrencyRate M_CurrencyRate = new CurrencyRate();
            M_CurrencyRate.CurrencyRatePK = Convert.ToInt32(dr["CurrencyRatePK"]);
            M_CurrencyRate.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_CurrencyRate.Status = Convert.ToInt32(dr["Status"]);
            M_CurrencyRate.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_CurrencyRate.Notes = Convert.ToString(dr["Notes"]);
            M_CurrencyRate.Date = dr["Date"].ToString();
            M_CurrencyRate.CurrencyPK = Convert.ToInt32(dr["CurrencyPK"]);
            M_CurrencyRate.CurrencyID = Convert.ToString(dr["CurrencyID"]);
            M_CurrencyRate.Rate = Convert.ToDecimal(dr["Rate"]);
            M_CurrencyRate.ProductRate = Convert.ToDecimal(dr["ProductRate"]);
            M_CurrencyRate.TaxRate = Convert.ToDecimal(dr["TaxRate"]);
            M_CurrencyRate.EntryUsersID = dr["EntryUsersID"].ToString();
            M_CurrencyRate.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_CurrencyRate.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_CurrencyRate.VoidUsersID = dr["VoidUsersID"].ToString();
            M_CurrencyRate.EntryTime = dr["EntryTime"].ToString();
            M_CurrencyRate.UpdateTime = dr["UpdateTime"].ToString();
            M_CurrencyRate.ApprovedTime = dr["ApprovedTime"].ToString();
            M_CurrencyRate.VoidTime = dr["VoidTime"].ToString();
            M_CurrencyRate.DBUserID = dr["DBUserID"].ToString();
            M_CurrencyRate.DBTerminalID = dr["DBTerminalID"].ToString();
            M_CurrencyRate.LastUpdate = dr["LastUpdate"].ToString();
            M_CurrencyRate.LastUpdateDB = Convert.ToString(dr["LastUpdateDB"]);
            return M_CurrencyRate;
        }

        public List<CurrencyRate> CurrencyRate_SelectDataByDateFromTo(int _status,DateTime _dateFrom, DateTime _dateTo)
        {
            
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<CurrencyRate> L_CurrencyRate = new List<CurrencyRate>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = "Select case when CR.status=1 then 'PENDING' else Case When CR.status = 2 then 'APPROVED' else Case when CR.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,C.ID CurrencyID,CR.* from CurrencyRate CR left join " +
                            "Currency C on CR.CurrencyPK = C.CurrencyPK and C.status = 2 " +
                            "where CR.status = @status and Date between @DateFrom and @DateTo ";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = "Select case when CR.status=1 then 'PENDING' else Case When CR.status = 2 then 'APPROVED' else Case when CR.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,C.ID CurrencyID,CR.* from CurrencyRate CR left join " +
                            "Currency C on CR.CurrencyPK = C.CurrencyPK and C.status = 2 where Date between @DateFrom and @DateTo ";
                        }

                        cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _dateTo);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_CurrencyRate.Add(setCurrencyRate(dr));
                                }
                            }
                            return L_CurrencyRate;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }


        public int CurrencyRate_Add(CurrencyRate _currencyRate, bool _havePrivillege)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_havePrivillege)
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[ApprovedUsersID],[ApprovedTime],lastupdate)" +
                                 "Select isnull(max(currencyRatePk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@lastupdate from CurrencyRate";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _currencyRate.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],lastupdate)" +
                                "Select isnull(max(currencyRatePk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@lastupdate from CurrencyRate";
                        }
                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@Date", _currencyRate.Date);
                        cmd.Parameters.AddWithValue("@CurrencyPK", _currencyRate.CurrencyPK);
                        cmd.Parameters.AddWithValue("@Rate", _currencyRate.Rate);
                        cmd.Parameters.AddWithValue("@ProductRate", _currencyRate.ProductRate);
                        cmd.Parameters.AddWithValue("@TaxRate", _currencyRate.TaxRate);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _currencyRate.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();

                        return _host.Get_LastPKByLastUpate(_datetimeNow, "CurrencyRate");
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
            
        }

        public int CurrencyRate_Update(CurrencyRate _currencyRate, bool _havePrivillege)
        {
            try
            {
                int _newHisPK;
                DateTime _datetimeNow = DateTime.Now;
                int status = _host.Get_Status(_currencyRate.CurrencyRatePK, _currencyRate.HistoryPK, "currencyRate");
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update CurrencyRate set status=2,Notes=@Notes,Date=@Date,CurrencyPK=@CurrencyPK,Rate=@Rate,ProductRate=@ProductRate,TaxRate=@TaxRate,ApprovedUsersID=@ApprovedUsersID, " +
                                "ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,lastupdate=@lastupdate " +
                                "where CurrencyRatePK = @PK and historyPK = @HistoryPK";
                            cmd.Parameters.AddWithValue("@HistoryPK", _currencyRate.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _currencyRate.CurrencyRatePK);
                            cmd.Parameters.AddWithValue("@Date", _currencyRate.Date);
                            cmd.Parameters.AddWithValue("@Notes", _currencyRate.Notes);
                            cmd.Parameters.AddWithValue("@CurrencyPK", _currencyRate.CurrencyPK);
                            cmd.Parameters.AddWithValue("@Rate", _currencyRate.Rate);
                            cmd.Parameters.AddWithValue("@ProductRate", _currencyRate.ProductRate);
                            cmd.Parameters.AddWithValue("@TaxRate", _currencyRate.TaxRate);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _currencyRate.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _currencyRate.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update CurrencyRate set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,lastupdate=@lastupdate where CurrencyRatePK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _currencyRate.CurrencyRatePK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _currencyRate.EntryUsersID);
                            cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
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
                                cmd.CommandText = "Update CurrencyRate set Notes=@Notes,Date=@Date,CurrencyPK=@CurrencyPK,Rate=@Rate,ProductRate=@ProductRate,TaxRate=@TaxRate, " +
                                    "UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,lastupdate=@lastupdate " +
                                    "where CurrencyRatePK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _currencyRate.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _currencyRate.CurrencyRatePK);
                                cmd.Parameters.AddWithValue("@Date", _currencyRate.Date);
                                cmd.Parameters.AddWithValue("@Notes", _currencyRate.Notes);
                                cmd.Parameters.AddWithValue("@CurrencyPK", _currencyRate.CurrencyPK);
                                cmd.Parameters.AddWithValue("@Rate", _currencyRate.Rate);
                                cmd.Parameters.AddWithValue("@ProductRate", _currencyRate.ProductRate);
                                cmd.Parameters.AddWithValue("@TaxRate", _currencyRate.TaxRate);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _currencyRate.EntryUsersID);
                                cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                                cmd.ExecuteNonQuery();
                            }
                            return 0;
                        }
                        else
                        {
                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                _newHisPK = _host.Get_NewHistoryPK(_currencyRate.CurrencyRatePK, "CurrencyRate");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From CurrencyRate where CurrencyRatePK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _currencyRate.CurrencyRatePK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _currencyRate.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@Date", _currencyRate.Date);
                                cmd.Parameters.AddWithValue("@CurrencyPK", _currencyRate.CurrencyPK);
                                cmd.Parameters.AddWithValue("@Rate", _currencyRate.Rate);
                                cmd.Parameters.AddWithValue("@ProductRate", _currencyRate.ProductRate);
                                cmd.Parameters.AddWithValue("@TaxRate", _currencyRate.TaxRate);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _currencyRate.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update CurrencyRate set status= 4,Notes=@Notes,"+
                                    "lastupdate=@lastupdate where CurrencyRatePK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _currencyRate.Notes);
                                cmd.Parameters.AddWithValue("@PK", _currencyRate.CurrencyRatePK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _currencyRate.HistoryPK);
                                cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
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

        public void CurrencyRate_Approved(CurrencyRate _currencyRate)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update CurrencyRate set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,lastupdate=@lastupdate " +
                            "where currencyRatepk = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _currencyRate.CurrencyRatePK);
                        cmd.Parameters.AddWithValue("@historyPK", _currencyRate.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _currencyRate.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update CurrencyRate set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,lastupdate=@lastupdate where CurrencyRatePK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _currencyRate.CurrencyRatePK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _currencyRate.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public void CurrencyRate_Reject(CurrencyRate _currencyRate)
        {
           try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update CurrencyRate set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,lastupdate=@lastupdate " +
                            "where CurrencyRatepk = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _currencyRate.CurrencyRatePK);
                        cmd.Parameters.AddWithValue("@historyPK", _currencyRate.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _currencyRate.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update CurrencyRate set status= 2,lastupdate=@lastupdate where CurrencyRatePK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _currencyRate.CurrencyRatePK);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public void CurrencyRate_Void(CurrencyRate _currencyRate)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update CurrencyRate set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,lastupdate=@lastupdate " +
                            "where CurrencyRatepk = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _currencyRate.CurrencyRatePK);
                        cmd.Parameters.AddWithValue("@historyPK", _currencyRate.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _currencyRate.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@lastupdate", _datetimeNow);
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
        public decimal GetLastCurrencyRate_ByCurrencyPK(LastRate _lastRate)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "select isnull(Rate,0) Rate from currencyRate (nolock) cr inner join ( " +
                                                "select [CurrencyRate].CurrencyPK, Max([CurrencyRate].Date) AS MaxDate " +
                                                "from [CurrencyRate] (nolock) " +
                                                "where [CurrencyRate].Date <= @date and [CurrencyRate].status = 2  " +
                                                "group by [CurrencyRate].CurrencyPK " +
                                                ")As J1 on cr.CurrencyPK = J1.CurrencyPK and cr.Date = J1.MaxDate  " +
                                                "Where cr.currencyPK = @currencyPK and cr.status = 2 ";
                        cmd.Parameters.AddWithValue("@date", _lastRate.Date);
                        cmd.Parameters.AddWithValue("@currencyPK", _lastRate.CurrencyPK);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return Convert.ToDecimal(dr["Rate"]);
                            }
                            return 1;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
           
        }


        public string ImportCurrencyRate(string _fileSource, string _userID)
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
                            cmd1.CommandText = "truncate table CurrencyRateTemp";
                            cmd1.ExecuteNonQuery();
                        }
                    }

                    // import data ke temp dulu
                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(Tools.conString, SqlBulkCopyOptions.TableLock))
                    {
                        bulkCopy.DestinationTableName = "dbo.CurrencyRateTemp";
                        bulkCopy.WriteToServer(CreateDataTableFromCurrencyRateTempExcelFile(_fileSource));
                        //_msg = "Import Close Nav Success";
                    }

                    // logic kalo Reconcile success
                    using (SqlConnection conn = new SqlConnection(Tools.conString))
                    {
                        conn.Open();
                        using (SqlCommand cmd1 = conn.CreateCommand())
                        {
                            cmd1.CommandText = @"
                                
--declare @UserID nvarchar(20)
--declare @TimeNow datetime

--set @UserID = 'admin'
--set @TimeNow = getdate()


update A set status = 3
from CurrencyRate A
left join Currency B on A.CurrencyPK = B.CurrencyPK and B.Status = 2
inner join CurrencyRateTemp C on B.ID = C.CurrencyID 
where A.Status in (1,2) and A.Date = Cast( C.Date as date)

declare @MaxCurrencyRatePK int

select @MaxCurrencyRatePK = max(CurrencyRatePK) from CurrencyRate

set @MaxCurrencyRatePK = isnull(@MaxCurrencyRatePK,0)


insert into CurrencyRate (CurrencyRatePK,HistoryPK,Status,Date,CurrencyPK,Rate,EntryUsersID,EntryTime,LastUpdate,ProductRate,TaxRate)
select ROW_NUMBER() over (order by A.CurrencyRatePK ) + @MaxCurrencyRatePK,1,1,cast( A.Date as date),B.CurrencyPK,A.GeneralRate,@UserID,@TimeNow,@TimeNow,A.ProductRate,A.TaxRate from CurrencyRateTemp A
left join Currency B on A.CurrencyID = B.ID and B.Status = 2

Select 'success'
                                                "
                                ;
                            cmd1.Parameters.AddWithValue("@UserID", _userID);
                            cmd1.Parameters.AddWithValue("@TimeNow", _now);

                            using (SqlDataReader dr = cmd1.ExecuteReader())
                            {
                                if (dr.HasRows)
                                {
                                    dr.Read();
                                    _msg = "Import Currency Rate Success"; //Convert.ToString(dr[""]);
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
        private DataTable CreateDataTableFromCurrencyRateTempExcelFile(string _path)
        {
            try
            {
                using (DataTable dt = new DataTable())
                {
                    DataColumn dc;
                    DataRow dr;

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Int32");
                    dc.ColumnName = "CurrencyRatePK";
                    dc.Unique = false;
                    dc.AutoIncrement = true;
                    dc.AutoIncrementSeed = 1;
                    dc.AutoIncrementStep = 1;
                    dt.Columns.Add(dc);


                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Date";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "CurrencyID";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Decimal");
                    dc.ColumnName = "GeneralRate";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Decimal");
                    dc.ColumnName = "ProductRate";
                    dc.Unique = false;
                    dt.Columns.Add(dc);


                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Decimal");
                    dc.ColumnName = "TaxRate";
                    dc.Unique = false;
                    dt.Columns.Add(dc);



                    FileInfo excelFile = new FileInfo(_path);
                    using (ExcelPackage package = new ExcelPackage(excelFile))
                    {
                        int i = 2;
                        ExcelWorksheet worksheet = package.Workbook.Worksheets[1];
                        var end = worksheet.Dimension.End;
                        while (i <= end.Row)
                        {
                            dr = dt.NewRow();
                            if (worksheet.Cells[i, 1].Value == null)
                                dr["Date"] = "";
                            else
                                dr["Date"] = worksheet.Cells[i, 1].Value.ToString();

                            if (worksheet.Cells[i, 2].Value == null)
                                dr["CurrencyID"] = "";
                            else
                                dr["CurrencyID"] = worksheet.Cells[i, 2].Value.ToString();

                            if (worksheet.Cells[i, 3].Value == null)
                                dr["GeneralRate"] = 0;
                            else
                                dr["GeneralRate"] = worksheet.Cells[i, 3].Value.ToString();

                            if (worksheet.Cells[i, 4].Value == null)
                                dr["ProductRate"] = 0;
                            else
                                dr["ProductRate"] = worksheet.Cells[i, 4].Value.ToString();

                            if (worksheet.Cells[i, 5].Value == null)
                                dr["TaxRate"] = 0;
                            else
                                dr["TaxRate"] = worksheet.Cells[i, 5].Value.ToString();




                            if (dr["Date"].Equals(null) != true ||
                                dr["CurrencyID"].Equals(null) != true ||
                                dr["GeneralRate"].Equals(null) != true ||
                                dr["ProductRate"].Equals(null) != true ||
                                dr["TaxRate"].Equals(null) != true
                                )


                            { dt.Rows.Add(dr); }
                            i++;

                        }
                    }

                    return dt;
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }


    }
}