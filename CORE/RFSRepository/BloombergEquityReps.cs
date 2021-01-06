using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using RFSModel;
using RFSUtility;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.IO;
using System.Text;
using System.Threading;
using System.Data;
using System.Data.SqlClient;
using System.Data.Odbc;
using System.Data.OleDb;

namespace RFSRepository
{
    public class BloombergEquityReps
    {
        Host _host = new Host();

        //1
        string _insertCommand = "INSERT INTO [dbo].[BloombergEquity] " +
                            "([BloombergEquityPK],[HistoryPK],[Status],[TickerName],[TickerCode],[FundName],[Weight],[Shares],[Price],[MarketCap],[PercentWeight],[GICSSector],[IndustryGroupIndex],[Y1],";
        string _paramaterCommand = "@TickerName,@TickerCode,@FundName,@Weight,@Shares,@Price,@MarketCap,@PercentWeight,@GICSSector,@IndustryGroupIndex,@Y1,";

        //2
        private BloombergEquity setBloombergEquity(SqlDataReader dr)
        {
            BloombergEquity M_BloombergEquity = new BloombergEquity();
            M_BloombergEquity.BloombergEquityPK = Convert.ToInt32(dr["BloombergEquityPK"]);
            M_BloombergEquity.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            //M_BloombergEquity.Selected = Convert.ToBoolean(dr["Selected"]);
            M_BloombergEquity.Status = Convert.ToInt32(dr["Status"]);
            //M_BloombergEquity.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_BloombergEquity.Notes = Convert.ToString(dr["Notes"]);

            M_BloombergEquity.TickerName = dr["TickerName"].ToString();
            M_BloombergEquity.TickerCode = dr["TickerCode"].ToString();
            M_BloombergEquity.FundName= dr["FundName"].ToString();
            M_BloombergEquity.Weight= Convert.ToInt32(dr["Weight"]);
            M_BloombergEquity.Shares= Convert.ToInt32(dr["Shares"]);
            M_BloombergEquity.Price= Convert.ToInt32(dr["Price"]);
            M_BloombergEquity.MarketCap= Convert.ToInt32(dr["MarketCap"]);
            M_BloombergEquity.PercentWeight= Convert.ToInt32(dr["PercentWeight"]);
            M_BloombergEquity.GICSSector= dr["GICSSector"].ToString();
            M_BloombergEquity.IndustryGroupIndex = dr["IndustryGroupIndex"].ToString();
            M_BloombergEquity.Y1 = dr["Y1"].ToString();

            M_BloombergEquity.EntryUsersID = dr["EntryUsersID"].ToString();
            M_BloombergEquity.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_BloombergEquity.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_BloombergEquity.VoidUsersID = dr["VoidUsersID"].ToString();
            M_BloombergEquity.EntryTime = dr["EntryTime"].ToString();
            M_BloombergEquity.UpdateTime = dr["UpdateTime"].ToString();
            M_BloombergEquity.ApprovedTime = dr["ApprovedTime"].ToString();
            M_BloombergEquity.VoidTime = dr["VoidTime"].ToString();
            M_BloombergEquity.DBUserID = dr["DBUserID"].ToString();
            M_BloombergEquity.DBTerminalID = dr["DBTerminalID"].ToString();
            M_BloombergEquity.LastUpdate = dr["LastUpdate"].ToString();
            return M_BloombergEquity;
        }

        public List<BloombergEquity> BloombergEquity_SelectByDateFromTo(int _status, DateTime _dateFrom, DateTime _dateTo)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<BloombergEquity> L_BloombergEquity = new List<BloombergEquity>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText =
                                @" 
                                    Select case when status=1 then 'PENDING' else Case When status = 2 then 'APPROVED' else Case when Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,* from BloombergEquity where status = @status and date between @datefrom and @dateto
                                ";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText =
                                @"
                                    --query masih belum
                                ";
                        }

                        cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _dateTo);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_BloombergEquity.Add(setBloombergEquity(dr));
                                }
                            }
                            return L_BloombergEquity;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public int BloombergEquity_Add(BloombergEquity _BloombergEquity, bool _havePrivillege)
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
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[ApprovedUsersID],[ApprovedTime],[LastUpdate])" +
                                 "Select isnull(max(BloombergEquityPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@LastUpdate From BloombergEquity";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _BloombergEquity.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);
                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[LastUpdate])" +
                                "Select isnull(max(BloombergEquityPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@LastUpdate From BloombergEquity";
                        }
                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);

                        cmd.Parameters.AddWithValue("@TickerName", _BloombergEquity.TickerName);
                        cmd.Parameters.AddWithValue("@TickerCode", _BloombergEquity.TickerCode);
                        cmd.Parameters.AddWithValue("@FundName", _BloombergEquity.FundName);
                        cmd.Parameters.AddWithValue("@Weight", _BloombergEquity.Weight);
                        cmd.Parameters.AddWithValue("@Shares ", _BloombergEquity.Shares);
                        cmd.Parameters.AddWithValue("@Price", _BloombergEquity.Price);
                        cmd.Parameters.AddWithValue("@MarketCap", _BloombergEquity.MarketCap);
                        cmd.Parameters.AddWithValue("@PercentWeight", _BloombergEquity.PercentWeight);
                        cmd.Parameters.AddWithValue("@GICSSector", _BloombergEquity.GICSSector);
                        cmd.Parameters.AddWithValue("@IndustryGroupIndex", _BloombergEquity.IndustryGroupIndex);
                        cmd.Parameters.AddWithValue("@Y1", _BloombergEquity.Y1);

                        cmd.Parameters.AddWithValue("@EntryUsersID", _BloombergEquity.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                        cmd.ExecuteNonQuery();

                        return _host.Get_LastPKByLastUpate(_dateTimeNow, "BloombergEquity");
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public int BloombergEquity_Update(BloombergEquity _BloombergEquity, bool _havePrivillege)
        {
            try
            {
                int _newHisPK;
                int status = _host.Get_Status(_BloombergEquity.BloombergEquityPK, _BloombergEquity.HistoryPK, "BloombergEquity");
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update BloombergEquity set status=2,Notes=@Notes,TickerName=@TickerName,TickerCode=@TickerCode,FundName=@FundName,Weight=@Weight,Shares=@Shares,Price=@Price,MarketCap=@MarketCap,PercentWeight=@PercentWeight,GICSSector=@GICSSector,IndustryGroupIndex=@IndustryGroupIndex,Y1=@Y1, "+
                                "ApprovedUsersID=@ApprovedUsersID, " +
                                "ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@LastUpdate " +
                                "where BloombergEquityPK = @PK and historyPK = @HistoryPK ";
                            cmd.Parameters.AddWithValue("@HistoryPK", _BloombergEquity.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _BloombergEquity.BloombergEquityPK);

                            cmd.Parameters.AddWithValue("@TickerName", _BloombergEquity.TickerName);
                            cmd.Parameters.AddWithValue("@TickerCode", _BloombergEquity.TickerCode);
                            cmd.Parameters.AddWithValue("@FundName", _BloombergEquity.FundName);
                            cmd.Parameters.AddWithValue("@Weight", _BloombergEquity.Weight);
                            cmd.Parameters.AddWithValue("@Shares ", _BloombergEquity.Shares);
                            cmd.Parameters.AddWithValue("@Price", _BloombergEquity.Price);
                            cmd.Parameters.AddWithValue("@MarketCap", _BloombergEquity.MarketCap);
                            cmd.Parameters.AddWithValue("@PercentWeight", _BloombergEquity.PercentWeight);
                            cmd.Parameters.AddWithValue("@GICSSector", _BloombergEquity.GICSSector);
                            cmd.Parameters.AddWithValue("@IndustryGroupIndex", _BloombergEquity.IndustryGroupIndex);
                            cmd.Parameters.AddWithValue("@Y1", _BloombergEquity.Y1);

                            cmd.Parameters.AddWithValue("@UpdateUsersID", _BloombergEquity.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _dateTimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _BloombergEquity.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);
                            cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update BloombergEquity set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where BloombergEquityPK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _BloombergEquity.BloombergEquityPK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _BloombergEquity.EntryUsersID);
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
                                cmd.CommandText = "Update BloombergEquity set Notes=@Notes,TickerName=@TickerName,TickerCode=@TickerCode,FundName=@FundName,Weight=@Weight,Shares=@Shares,Price=@Price,MarketCap=@MarketCap,PercentWeight=@PercentWeight,GICSSector=@GICSSector,IndustryGroupIndex=@IndustryGroupIndex,Y1=@Y1, " +
                                "UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@LastUpdate " +
                                "where BloombergEquityPK = @PK and historyPK = @HistoryPK ";
                                cmd.Parameters.AddWithValue("@HistoryPK", _BloombergEquity.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _BloombergEquity.BloombergEquityPK);

                                cmd.Parameters.AddWithValue("@TickerName", _BloombergEquity.TickerName);
                                cmd.Parameters.AddWithValue("@TickerCode", _BloombergEquity.TickerCode);
                                cmd.Parameters.AddWithValue("@FundName", _BloombergEquity.FundName);
                                cmd.Parameters.AddWithValue("@Weight", _BloombergEquity.Weight);
                                cmd.Parameters.AddWithValue("@Shares ", _BloombergEquity.Shares);
                                cmd.Parameters.AddWithValue("@Price", _BloombergEquity.Price);
                                cmd.Parameters.AddWithValue("@MarketCap", _BloombergEquity.MarketCap);
                                cmd.Parameters.AddWithValue("@PercentWeight", _BloombergEquity.PercentWeight);
                                cmd.Parameters.AddWithValue("@GICSSector", _BloombergEquity.GICSSector);
                                cmd.Parameters.AddWithValue("@IndustryGroupIndex", _BloombergEquity.IndustryGroupIndex);
                                cmd.Parameters.AddWithValue("@Y1", _BloombergEquity.Y1);

                                cmd.Parameters.AddWithValue("@UpdateUsersID", _BloombergEquity.EntryUsersID);
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
                                _newHisPK = _host.Get_NewHistoryPK(_BloombergEquity.BloombergEquityPK, "BloombergEquity");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From BloombergEquity where BloombergEquityPK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _BloombergEquity.BloombergEquityPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _BloombergEquity.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);

                                cmd.Parameters.AddWithValue("@TickerName", _BloombergEquity.TickerName);
                                cmd.Parameters.AddWithValue("@TickerCode", _BloombergEquity.TickerCode);
                                cmd.Parameters.AddWithValue("@FundName", _BloombergEquity.FundName);
                                cmd.Parameters.AddWithValue("@Weight", _BloombergEquity.Weight);
                                cmd.Parameters.AddWithValue("@Shares ", _BloombergEquity.Shares);
                                cmd.Parameters.AddWithValue("@Price", _BloombergEquity.Price);
                                cmd.Parameters.AddWithValue("@MarketCap", _BloombergEquity.MarketCap);
                                cmd.Parameters.AddWithValue("@PercentWeight", _BloombergEquity.PercentWeight);
                                cmd.Parameters.AddWithValue("@GICSSector", _BloombergEquity.GICSSector);
                                cmd.Parameters.AddWithValue("@IndustryGroupIndex", _BloombergEquity.IndustryGroupIndex);
                                cmd.Parameters.AddWithValue("@Y1", _BloombergEquity.Y1);

                                cmd.Parameters.AddWithValue("@UpdateUsersID", _BloombergEquity.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _dateTimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update BloombergEquity set status= 4,Notes=@Notes," +
                                "LastUpdate=@LastUpdate where BloombergEquityPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _BloombergEquity.Notes);
                                cmd.Parameters.AddWithValue("@PK", _BloombergEquity.BloombergEquityPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _BloombergEquity.HistoryPK);
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

        public void BloombergEquity_Approved(BloombergEquity _BloombergEquity)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update BloombergEquity set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,LastUpdate=@LastUpdate " +
                            "where BloombergEquityPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _BloombergEquity.BloombergEquityPK);
                        cmd.Parameters.AddWithValue("@historyPK", _BloombergEquity.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _BloombergEquity.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update BloombergEquity set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where BloombergEquityPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _BloombergEquity.BloombergEquityPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _BloombergEquity.ApprovedUsersID);
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

        public void BloombergEquity_Reject(BloombergEquity _BloombergEquity)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update BloombergEquity set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate = @LastUpdate " +
                            "where BloombergEquityPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _BloombergEquity.BloombergEquityPK);
                        cmd.Parameters.AddWithValue("@historyPK", _BloombergEquity.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _BloombergEquity.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update BloombergEquity set status= 2,LastUpdate=@LastUpdate where BloombergEquityPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _BloombergEquity.BloombergEquityPK);
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

        public void BloombergEquity_Void(BloombergEquity _BloombergEquity)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update BloombergEquity set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate = @LastUpdate " +
                            "where BloombergEquityPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _BloombergEquity.BloombergEquityPK);
                        cmd.Parameters.AddWithValue("@historyPK", _BloombergEquity.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _BloombergEquity.VoidUsersID);
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

        //Import

        public string ImportBloombergEquity(string _fileSource, string _userID, string _date)
        {
            string _msg = "";
            try
            {
                DateTime _now = DateTime.Now;

                //delete data yang lama
                using (SqlConnection conn = new SqlConnection(Tools.conString))
                {
                    conn.Open();
                    using (SqlCommand cmd1 = conn.CreateCommand())
                    {
                        cmd1.CommandText = "truncate table BloombergEquityTemp";
                        cmd1.ExecuteNonQuery();
                    }
                }

                // import data ke temp dulu
                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(Tools.conString, SqlBulkCopyOptions.TableLock))
                {
                    bulkCopy.DestinationTableName = "dbo.BloombergEquityTemp";
                    bulkCopy.WriteToServer(CreateDataTableFromBloombergEquityTempExcelFile(_fileSource));
                    _msg = "Import Bloomberg Equity Success";
                }

                // logic kalo Reconcile success
                using (SqlConnection conn = new SqlConnection(Tools.conString))
                {
                    conn.Open();
                    using (SqlCommand cmd1 = conn.CreateCommand())
                    {
                        cmd1.CommandText =

                        @"
                        delete BloombergEquity where date = @Date

                        declare @BloombergEquity int 
                        select @BloombergEquity = isnull(max(isnull(BloombergEquityPK,0)),0) from BloombergEquity


                        INSERT INTO [dbo].[BloombergEquity]
                        ([BloombergEquityPK],[HistoryPK],[Status],[Date],[TickerName],[TickerCode],[FundName],[Weight],[Shares],[Price],[MarketCap],[PercentWeight],[GICSSector],[IndustryGroupIndex],[Y1],[EntryUsersID],[EntryTime],[ApprovedUsersID],[ApprovedTime],[LastUpdate])
                        select ROW_NUMBER() OVER(ORDER BY TickerCode ASC) + @BloombergEquity,1,2,@date,isnull(TickerName,''),isnull(TickerCode,''),isnull(FundName,''),isnull(Weight,0),isnull(Shares,0),isnull(Price,0),isnull(MarketCap,0),isnull(PercentWeight,0),isnull(GICSSector,''),
                        isnull(IndustryGroupIndex,''),isnull(Y1,0),@UsersID,@LastUpdate,@UsersID,@LastUpdate,@LastUpdate from BloombergEquityTemp
";
                        cmd1.Parameters.AddWithValue("@Date", _date);
                        cmd1.Parameters.AddWithValue("@UsersID", _userID);
                        cmd1.Parameters.AddWithValue("@LastUpdate", _now);
                        cmd1.ExecuteNonQuery();

                    }
                    _msg = "Import Bloomberg Equity Done";

                }



                return _msg;
            }
            catch (Exception err)
            {
                throw err;
            }
        }
        private DataTable CreateDataTableFromBloombergEquityTempExcelFile(string _path)
        {
            try
            {
                using (DataTable dt = new DataTable())
                {
                    DataColumn dc;
                    DataRow dr;

                    //dc = new DataColumn();
                    //dc.DataType = System.Type.GetType("System.String");
                    //dc.ColumnName = "Date";
                    //dc.Unique = false;
                    //dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "TickerName";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "TickerCode";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "FundName";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Weight";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Shares";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Price";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "MarketCap";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "PercentWeight";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "GICSSector";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "IndustryGroupIndex";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Y1";
                    dc.Unique = false;
                    dt.Columns.Add(dc);


                    FileInfo excelFile = new FileInfo(_path);
                    using (ExcelPackage package = new ExcelPackage(excelFile))
                    {
                        int i = 2;
                        ExcelWorksheet worksheet = package.Workbook.Worksheets[1];
                        var end = worksheet.Dimension.End;

                        while (i < end.Row)
                        {
                            dr = dt.NewRow();
                            if (worksheet.Cells[i, 1].Value == null)
                                dr["TickerName"] = "";
                            else
                                dr["TickerName"] = worksheet.Cells[i, 1].Value.ToString();

                            if (worksheet.Cells[i, 2].Value == null)
                                dr["TickerCode"] = "";
                            else
                                dr["TickerCode"] = worksheet.Cells[i, 2].Value.ToString();

                            if (worksheet.Cells[i, 3].Value == null)
                                dr["FundName"] = "";
                            else
                                dr["FundName"] = worksheet.Cells[i, 3].Value.ToString();

                            if (worksheet.Cells[i, 4].Value == null || worksheet.Cells[i, 4].Value.ToString() == "--")
                                dr["Weight"] = 0;
                            else
                                dr["Weight"] = worksheet.Cells[i, 4].Value.ToString();

                            if (worksheet.Cells[i, 5].Value == null || worksheet.Cells[i, 5].Value.ToString() == "--")
                                dr["Shares"] = 0;
                            else
                                dr["Shares"] = worksheet.Cells[i, 5].Value.ToString();

                            if (worksheet.Cells[i, 6].Value == null || worksheet.Cells[i, 6].Value.ToString() == "--")
                                dr["Price"] = 0;
                            else
                                dr["Price"] = worksheet.Cells[i, 6].Value.ToString();

                            if (worksheet.Cells[i, 7].Value == null || worksheet.Cells[i, 7].Value.ToString() == "--")
                                dr["MarketCap"] = 0;
                            else
                                dr["MarketCap"] = worksheet.Cells[i, 7].Value.ToString();

                            if (worksheet.Cells[i, 8].Value == null || worksheet.Cells[i, 8].Value.ToString() == "--")
                                dr["PercentWeight"] = 0;
                            else
                                dr["PercentWeight"] = worksheet.Cells[i, 8].Value.ToString();

                            if (worksheet.Cells[i, 9].Value == null)
                                dr["GICSSector"] = "";
                            else
                                dr["GICSSector"] = worksheet.Cells[i, 9].Value.ToString();

                            if (worksheet.Cells[i, 10].Value == null)
                                dr["IndustryGroupIndex"] = "";
                            else
                                dr["IndustryGroupIndex"] = worksheet.Cells[i, 10].Value.ToString();

                            if (worksheet.Cells[i, 11].Value == null || worksheet.Cells[i, 11].Value.ToString() == "--")
                                dr["Y1"] = 0;
                            else
                                dr["Y1"] = worksheet.Cells[i, 11].Value.ToString();

                            //dr["ClosePriceValue"] = worksheet.Cells[i, 2].Value.Equals(null) == true ? "" : worksheet.Cells[i, 2].Value.ToString();

                            if (dr["TickerName"].Equals(null) != true ||
                                dr["TickerCode"].Equals(null) != true ||
                                dr["FundName"].Equals(null) != true ||
                                dr["Weight"].Equals(null) != true ||
                                dr["Shares"].Equals(null) != true ||
                                dr["Price"].Equals(null) != true ||
                                dr["MarketCap"].Equals(null) != true ||
                                dr["PercentWeight"].Equals(null) != true ||
                                dr["GICSSector"].Equals(null) != true ||
                                dr["IndustryGroupIndex"].Equals(null) != true ||
                                dr["Y1"].Equals(null) != true) { dt.Rows.Add(dr); }
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
