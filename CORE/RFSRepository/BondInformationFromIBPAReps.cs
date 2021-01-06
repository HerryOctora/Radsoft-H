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
    public class BondInformationFromIBPAReps
    {
        Host _host = new Host();

        //2
        private BondInformationFromIBPA setBondInformationFromIBPA(SqlDataReader dr)
        {
            BondInformationFromIBPA M_BondInformationFromIBPA = new BondInformationFromIBPA();
            M_BondInformationFromIBPA.BondInformationFromIBPAPK = Convert.ToInt32(dr["BondInformationFromIBPAPK"]);
            M_BondInformationFromIBPA.Date = Convert.ToDateTime(dr["Date"]);
            M_BondInformationFromIBPA.Series = Convert.ToString(dr["Series"]);
            M_BondInformationFromIBPA.ISINCode = Convert.ToString(dr["ISINCode"]);
            M_BondInformationFromIBPA.BondName = Convert.ToString(dr["BondName"]);
            M_BondInformationFromIBPA.Rating = Convert.ToString(dr["Rating"]);
            M_BondInformationFromIBPA.CouponRate = Convert.ToDecimal(dr["CouponRate"]);
            M_BondInformationFromIBPA.MaturityDate = Convert.ToDateTime(dr["MaturityDate"]);
            M_BondInformationFromIBPA.TTM = Convert.ToDecimal(dr["TTM"]);
            M_BondInformationFromIBPA.TodayYield = Convert.ToDecimal(dr["TodayYield"]);
            M_BondInformationFromIBPA.TodayLowPrice = Convert.ToDecimal(dr["TodayLowPrice"]);
            M_BondInformationFromIBPA.TodayFairPrice = Convert.ToDecimal(dr["TodayFairPrice"]);
            M_BondInformationFromIBPA.TodayHighPrice = Convert.ToDecimal(dr["TodayHighPrice"]);
            M_BondInformationFromIBPA.Change = Convert.ToDecimal(dr["Change"]);
            M_BondInformationFromIBPA.YesterdayYield = Convert.ToDecimal(dr["YesterdayYield"]);
            M_BondInformationFromIBPA.YesterdayPrice = Convert.ToDecimal(dr["YesterdayPrice"]);
            M_BondInformationFromIBPA.LastWeekYield = Convert.ToDecimal(dr["LastWeekYield"]);
            M_BondInformationFromIBPA.LastWeekPrice = Convert.ToDecimal(dr["LastWeekPrice"]);


            return M_BondInformationFromIBPA;
        }


        public List<BondInformationFromIBPA> _BondInformationFromIBPAReps_SelectByDate(int _status, DateTime _dateFrom)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<BondInformationFromIBPA> L_BondInformationFromIBPA = new List<BondInformationFromIBPA>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText =
                              @"select * From BondInformationFromIBPATemp where date = @datefrom
                                   ";

                        cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_BondInformationFromIBPA.Add(setBondInformationFromIBPA(dr));
                                }
                            }
                            return L_BondInformationFromIBPA;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public string ImportBondInformationFromIBPA(string _fileSource, string _userID)
        {
            string _msg = "";
            try
            {
                DateTime _now = DateTime.Now;

                //delete data yang lama
                using (SqlConnection conn = new SqlConnection(Tools.conString))
                {
                    conn.Open();
                    using (SqlCommand cmd2 = conn.CreateCommand())
                    {
                        cmd2.CommandText = "truncate table BondInformationFromIBPATemp";
                        cmd2.ExecuteNonQuery();
                    }
                }

                // import data ke temp dulu
                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(Tools.conString, SqlBulkCopyOptions.TableLock))
                {
                    bulkCopy.DestinationTableName = "dbo.BondInformationFromIBPATemp";
                    bulkCopy.WriteToServer(CreateDataTableFromFileBondInformationFromIBPA(_fileSource));

                    _msg = "";
                }


                return _msg;
            }
            catch (Exception err)
            {
                throw err;
            }
        }
        private DataTable CreateDataTableFromFileBondInformationFromIBPA(string _fileName)
        {
            try
            {
                using (DataTable dt = new DataTable())
                {
                    DataColumn dc;
                    DataRow dr;

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "BondInformationFromIBPATempPK";
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
                    dc.ColumnName = "Series";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "ISINCode";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "BondName";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Rating";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Decimal");
                    dc.ColumnName = "CouponRate";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "MaturityDate";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Decimal");
                    dc.ColumnName = "TTM";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Decimal");
                    dc.ColumnName = "TodayYield";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Decimal");
                    dc.ColumnName = "TodayLowPrice";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Decimal");
                    dc.ColumnName = "TodayFairPrice";
                    dc.Unique = false;
                    dt.Columns.Add(dc);


                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Decimal");
                    dc.ColumnName = "TodayHighPrice";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Decimal");
                    dc.ColumnName = "Change";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Decimal");
                    dc.ColumnName = "YesterdayYield";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Decimal");
                    dc.ColumnName = "YesterdayPrice";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Decimal");
                    dc.ColumnName = "LastWeekYield";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Decimal");
                    dc.ColumnName = "LastWeekPrice";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Decimal");
                    dc.ColumnName = "LastMonthYield";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Decimal");
                    dc.ColumnName = "LastMonthPrice";
                    dc.Unique = false;
                    dt.Columns.Add(dc);


                    FileInfo excelFile = new FileInfo(_fileName);
                    using (ExcelPackage package = new ExcelPackage(excelFile))
                    {
                        int i = 2;
                        ExcelWorksheet worksheet = package.Workbook.Worksheets[1];
                        var end = worksheet.Dimension.End;
                        while (i <= end.Row)
                        {
                            dr = dt.NewRow();

                            string _valueDate = worksheet.Cells[i, 1].Value.ToString();
                            if (!string.IsNullOrEmpty(_valueDate))
                            {
                                string _tgl = _valueDate.Substring(6, 2);
                                string _bln = _valueDate.Substring(4, 2);
                                string _thn = _valueDate.Substring(0, 4);

                                _valueDate = _bln + "/" + _tgl + "/" + _thn; //Convert.ToDateTime(_strTransactionDate).ToString("MM/dd/yyyy");
                            }

                            if (worksheet.Cells[i, 1].Value == null)
                                dr["Date"] = "";
                            else
                                dr["Date"] = _valueDate;

                            if (worksheet.Cells[i, 2].Value == null)
                                dr["Series"] = "";
                            else
                                dr["Series"] = worksheet.Cells[i, 2].Value.ToString();

                            if (worksheet.Cells[i, 3].Value == null)
                                dr["ISINCode"] = "";
                            else
                                dr["ISINCode"] = worksheet.Cells[i, 3].Value.ToString();

                            if (worksheet.Cells[i, 4].Value == null)
                                dr["BondName"] = "";
                            else
                                dr["BondName"] = worksheet.Cells[i, 4].Value.ToString();

                            if (worksheet.Cells[i, 5].Value == null)
                                dr["Rating"] = "";
                            else
                                dr["Rating"] = worksheet.Cells[i, 5].Value.ToString();

                            if (worksheet.Cells[i, 6].Value == null)
                                dr["CouponRate"] = 0;
                            else
                                dr["CouponRate"] = (Convert.ToDecimal(worksheet.Cells[i, 6].Value)).ToString();

                            string _maturityDate = worksheet.Cells[i, 7].Value.ToString();
                            if (!string.IsNullOrEmpty(_maturityDate))
                            {
                                string _tglm = _maturityDate.Substring(6, 2);
                                string _blnm = _maturityDate.Substring(4, 2);
                                string _thnm = _maturityDate.Substring(0, 4);

                                _maturityDate = _blnm + "/" + _tglm + "/" + _thnm; //Convert.ToDateTime(_strTransactionDate).ToString("MM/dd/yyyy");
                            }

                            if (worksheet.Cells[i, 7].Value == null)
                                dr["MaturityDate"] = "";
                            else
                                dr["MaturityDate"] = _maturityDate;

                            if (worksheet.Cells[i, 8].Value == null)
                                dr["TTM"] = 0;
                            else
                                dr["TTM"] = worksheet.Cells[i, 8].Value.ToString();

                            if (worksheet.Cells[i, 9].Value == null)
                                dr["TodayYield"] = 0;
                            else
                                dr["TodayYield"] = worksheet.Cells[i, 9].Value.ToString();

                            if (worksheet.Cells[i, 10].Value == null)
                                dr["TodayLowPrice"] = 0;
                            else
                                dr["TodayLowPrice"] = worksheet.Cells[i, 10].Value.ToString();

                            if (worksheet.Cells[i, 11].Value == null)
                                dr["TodayFairPrice"] = 0;
                            else
                                dr["TodayFairPrice"] = worksheet.Cells[i, 11].Value.ToString();

                            if (worksheet.Cells[i, 12].Value == null)
                                dr["TodayHighPrice"] = 0;
                            else
                                dr["TodayHighPrice"] = worksheet.Cells[i, 12].Value.ToString();

                            if (worksheet.Cells[i, 13].Value == null)
                                dr["Change"] = 0;
                            else
                                dr["Change"] = worksheet.Cells[i, 13].Value.ToString();

                            if (worksheet.Cells[i, 14].Value == null)
                                dr["YesterdayYield"] = 0;
                            else
                                dr["YesterdayYield"] = worksheet.Cells[i, 14].Value.ToString();

                            if (worksheet.Cells[i, 15].Value == null)
                                dr["YesterdayPrice"] = 0;
                            else
                                dr["YesterdayPrice"] = worksheet.Cells[i, 15].Value.ToString();

                            if (worksheet.Cells[i, 16].Value == null)
                                dr["LastWeekYield"] = 0;
                            else
                                dr["LastWeekYield"] = worksheet.Cells[i, 16].Value.ToString();

                            if (worksheet.Cells[i, 17].Value == null)
                                dr["LastWeekPrice"] = 0;
                            else
                                dr["LastWeekPrice"] = worksheet.Cells[i, 17].Value.ToString();

                            if (worksheet.Cells[i, 18].Value == null)
                                dr["LastMonthYield"] = 0;
                            else
                                dr["LastMonthYield"] = worksheet.Cells[i, 18].Value.ToString();

                            if (worksheet.Cells[i, 19].Value == null)
                                dr["LastMonthPrice"] = 0;
                            else
                                dr["LastMonthPrice"] = worksheet.Cells[i, 19].Value.ToString();




                            //dr["ClosePriceValue"] = worksheet.Cells[i, 2].Value.Equals(null) == true ? "" : worksheet.Cells[i, 2].Value.ToString();

                            if (dr["Date"].Equals(null) != true ||
                                dr["Series"].Equals(null) != true ||
                                dr["ISINCode"].Equals(null) != true ||
                                dr["BondName"].Equals(null) != true ||
                                dr["Rating"].Equals(null) != true ||
                                dr["CouponRate"].Equals(null) != true ||
                                dr["MaturityDate"].Equals(null) != true ||
                                dr["TTM"].Equals(null) != true ||
                                dr["TodayYield"].Equals(null) != true ||
                                dr["TodayLowPrice"].Equals(null) != true ||
                                dr["TodayFairPrice"].Equals(null) != true ||

                                dr["TodayHighPrice"].Equals(null) != true ||
                                dr["Change"].Equals(null) != true ||
                                dr["YesterdayYield"].Equals(null) != true ||
                                dr["YesterdayPrice"].Equals(null) != true ||
                                dr["LastWeekYield"].Equals(null) != true ||
                                dr["LastWeekPrice"].Equals(null) != true ||
                                dr["LastMonthYield"].Equals(null) != true ||
                                dr["LastMonthPrice"].Equals(null) != true
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


        public string ImportBondInformationFromIBPACsv(string _fileSource, string _userID)
        {
            string _msg = "";
            try
            {
                DateTime _now = DateTime.Now;

                //delete data yang lama
                using (SqlConnection conn = new SqlConnection(Tools.conString))
                {
                    conn.Open();
                    using (SqlCommand cmd2 = conn.CreateCommand())
                    {
                        cmd2.CommandText = "truncate table BondInformationFromIBPATemp";
                        cmd2.ExecuteNonQuery();
                    }
                }

                // import data ke temp dulu
                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(Tools.conString, SqlBulkCopyOptions.TableLock))
                {
                    bulkCopy.DestinationTableName = "dbo.BondInformationFromIBPATemp";
                    bulkCopy.WriteToServer(CreateDataTableFromFileBondInformationFromIBPACsv(_fileSource));

                    _msg = "";
                }


                return _msg;
            }
            catch (Exception err)
            {
                throw err;
            }
        }
        private DataTable CreateDataTableFromFileBondInformationFromIBPACsv(string _fileSource)
        {
            try
            {
                using (DataTable dt = new DataTable())
                {
                    DataColumn dc;
                    DataRow dr;

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "BondInformationFromIBPATempPK";
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
                    dc.ColumnName = "Series";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "ISINCode";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "BondName";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Rating";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Decimal");
                    dc.ColumnName = "CouponRate";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "MaturityDate";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Decimal");
                    dc.ColumnName = "TTM";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Decimal");
                    dc.ColumnName = "TodayYield";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Decimal");
                    dc.ColumnName = "TodayLowPrice";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Decimal");
                    dc.ColumnName = "TodayFairPrice";
                    dc.Unique = false;
                    dt.Columns.Add(dc);


                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Decimal");
                    dc.ColumnName = "TodayHighPrice";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Decimal");
                    dc.ColumnName = "Change";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Decimal");
                    dc.ColumnName = "YesterdayYield";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Decimal");
                    dc.ColumnName = "YesterdayPrice";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Decimal");
                    dc.ColumnName = "LastWeekYield";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Decimal");
                    dc.ColumnName = "LastWeekPrice";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Decimal");
                    dc.ColumnName = "LastMonthYield";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Decimal");
                    dc.ColumnName = "LastMonthPrice";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    string[] allLines = File.ReadAllLines(_fileSource);
                    {
                        for (int i = 1; i < allLines.Length; i++)
                        {

                            string[] items = allLines[i].Split(new char[] { ',' });


                            dr = dt.NewRow();

                            string _valueDate = Convert.ToString(items[0]);
                            if (!string.IsNullOrEmpty(_valueDate))
                            {
                                string _tgl = _valueDate.Substring(6, 2);
                                string _bln = _valueDate.Substring(4, 2);
                                string _thn = _valueDate.Substring(0, 4);

                                _valueDate = _bln + "/" + _tgl + "/" + _thn; //Convert.ToDateTime(_strTransactionDate).ToString("MM/dd/yyyy");
                            }

                            if (items[0] == "")
                                dr["Date"] = "";
                            else
                                dr["Date"] = _valueDate;


                            if (items[1] == "")
                                dr["Series"] = "";
                            else
                                dr["Series"] = Convert.ToString(items[1]);

                            if (items[2] == "")
                                dr["IsinCode"] = "";
                            else
                                dr["IsinCode"] = Convert.ToString(items[2]);

                            if (items[3] == "")
                                dr["BondName"] = "";
                            else
                                dr["BondName"] = Convert.ToString(items[3]);

                            if (items[4] == "")
                                dr["Rating"] = "";
                            else
                                dr["Rating"] = Convert.ToString(items[4]);

                            if (items[5] == "")
                                dr["CouponRate"] = 0;
                            else
                                dr["CouponRate"] = (Convert.ToDecimal(items[5]));

                            string _maturityDate = Convert.ToString(items[6]);
                            if (!string.IsNullOrEmpty(_maturityDate))
                            {
                                string _tglm = _maturityDate.Substring(6, 2);
                                string _blnm = _maturityDate.Substring(4, 2);
                                string _thnm = _maturityDate.Substring(0, 4);

                                _maturityDate = _blnm + "/" + _tglm + "/" + _thnm; //Convert.ToDateTime(_strTransactionDate).ToString("MM/dd/yyyy");
                            }

                            if (items[6] == "")
                                dr["MaturityDate"] = "";
                            else
                                dr["MaturityDate"] = _maturityDate;

                            if (items[7] == "")
                                dr["TTM"] = 0;
                            else
                                dr["TTM"] = Convert.ToDecimal(items[7]);

                            if (items[8] == "")
                                dr["TodayYield"] = 0;
                            else
                                dr["TodayYield"] = Convert.ToDecimal(items[8]);

                            if (items[9] == "")
                                dr["TodayLowPrice"] = 0;
                            else
                                dr["TodayLowPrice"] = Convert.ToDecimal(items[9]);

                            if (items[10] == "")
                                dr["TodayFairPrice"] = 0;
                            else
                                dr["TodayFairPrice"] = Convert.ToDecimal(items[10]);

                            if (items[11] == "")
                                dr["TodayHighPrice"] = 0;
                            else
                                dr["TodayHighPrice"] = Convert.ToDecimal(items[11]);

                            if (items[12] == "")
                                dr["Change"] = 0;
                            else
                                dr["Change"] = Convert.ToDecimal(items[12]);

                            if (items[13] == "")
                                dr["YesterdayYield"] = 0;
                            else
                                dr["YesterdayYield"] = Convert.ToDecimal(items[13]);

                            if (items[14] == "")
                                dr["YesterdayPrice"] = 0;
                            else
                                dr["YesterdayPrice"] = Convert.ToDecimal(items[14]);

                            if (items[15] == "")
                                dr["LastWeekYield"] = 0;
                            else
                                dr["LastWeekYield"] = Convert.ToDecimal(items[15]);

                            if (items[16] == "")
                                dr["LastWeekPrice"] = 0;
                            else
                                dr["LastWeekPrice"] = Convert.ToDecimal(items[16]);

                            if (items[17] == "")
                                dr["LastMonthYield"] = 0;
                            else
                                dr["LastMonthYield"] = Convert.ToDecimal(items[17]);

                            if (items[18] == "")
                                dr["LastMonthPrice"] = 0;
                            else
                                dr["LastMonthPrice"] = Convert.ToDecimal(items[18]);


                            dt.Rows.Add(dr);
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

        public string BondInformationFromIBPA_UpdateToInstrument(DateTime _date, string _UsersID)
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
                         

                            update A set A.InterestPercent = B.CouponRate,A.AnotherRating = B.Rating, A.LastUpdate = @LastUpdate, UpdateUsersID = @UserID, A.UpdateTime = @LastUpdate 
							from Instrument A
							inner join BondInformationFromIBPATemp B on A.ID = B.Series
							where A.Status = 2 and B.Date = @Date

							select 'Update To Instrument Success!' Result
                        ";
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@UserID", _UsersID);
                        cmd.Parameters.AddWithValue("@Date", _date);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    return dr["Result"].ToString();
                                }
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


        public string BondInformationFromIBPA_ApproveBondInformationFromIBPAData(string _userID)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();

                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {


                        cmd.CommandText =
                        @"declare @Date Datetime
                        select @Date = Date From BondInformationFromIBPATemp

                        Update A set status = 3 from BondInformationFromIBPA A 
                        left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2
                        where B.ID in (select Series from BondInformationFromIBPAIBPATemp)
                        and A.Status in (1,2) and Date = @Date

                        Declare @BondInformationFromIBPAPK int
                            Select @BondInformationFromIBPAPK = max(BondInformationFromIBPAPK) + 1 from BondInformationFromIBPA
                            set @BondInformationFromIBPAPK = isnull(@BondInformationFromIBPAPK,0)
                            INSERT INTO [dbo].[BondInformationFromIBPA]
                                        ([BondInformationFromIBPAPK]
                                        ,[HistoryPK]
                                        ,[Status]
                                        ,[Notes]
                                        ,[Date]
                                        ,[InstrumentPK]
                                        ,[BondInformationFromIBPAValue]
                                        ,[LowPriceValue]
                                        ,[HighPriceValue]      
                                        ,[EntryUsersID]
                                        ,[EntryTime]
                                        ,[LastUpdate]
                            )
                            Select @BondInformationFromIBPAPK +  ROW_NUMBER() OVER(Order By A.Series ASC) ,1,1,'',A.Date,isnull(B.InstrumentPK,0)
                            ,A.FairPrice,A.LowPrice,A.HighPrice
                            ,@userID,@Datetime,@Datetime
                            from BondInformationFromIBPAIBPATemp  A
                            Left join Instrument B on A.Series = B.ID and B.status = 2

                            delete BondInformationFromIBPA where InstrumentPK = 0

	                        select 'Import IBPA Success' Result

                        ";

                        cmd.Parameters.AddWithValue("@UserID", _userID);
                        cmd.Parameters.AddWithValue("@Datetime", _dateTimeNow);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    return Convert.ToString(dr["Result"]);
                                }
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

//        public decimal Validate_CashAmountByDate(int _fundPK, int _fundClientPK, DateTime _date)
//        {
//            try
//            {
//                DateTime _dateTimeNow = DateTime.Now;
//                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
//                {
//                    DbCon.Open();
//                    using (SqlCommand cmd = DbCon.CreateCommand())
//                    {
//                        cmd.CommandText = @"
//                            select isnull(sum(B.Nav * A.UnitAmount),0) CashAmount from FundClientPosition A
//                            left join 
//                                (
//                                select * from CloseNAV Where  FundPK = @FundPK and Date =
//                                (
//                                    Select max(Date) From CloseNAV where Date <= @Date  and FundPK = @FundPK and status = 2
//                                ) and status = 2
//                                )B on A.FundPK = B.FundPK  and B.Status = 2
//                            where  A.Date = (select max(Date) From FundClientPosition where Date <= @date)
//                            and A.FundPK = @FundPK and FundClientPK = @FundClientPK
//                        ";
//                        cmd.Parameters.AddWithValue("@FundClientPK", _fundClientPK);
//                        cmd.Parameters.AddWithValue("@FundPK", _fundPK);
//                        cmd.Parameters.AddWithValue("@Date", _date);
//                        using (SqlDataReader dr = cmd.ExecuteReader())
//                        {
//                            if (dr.HasRows)
//                            {
//                                while (dr.Read())
//                                {
//                                    return Convert.ToDecimal(dr["CashAmount"]);
//                                }
//                            }
//                            return 0;
//                        }
//                    }
//                }
//            }
//            catch (Exception err)
//            {
//                throw err;
//            }
//        }
    }
}
