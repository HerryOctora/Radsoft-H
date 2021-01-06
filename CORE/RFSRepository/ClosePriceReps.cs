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
    public class ClosePriceReps
    {
        Host _host = new Host();

        //1
        string _insertCommand = "INSERT INTO [dbo].[ClosePrice] " +
                            "([ClosePricePK],[HistoryPK],[Status],[Date],[InstrumentPK],[ClosePriceValue],[LowPriceValue],[HighPriceValue],[LiquidityPercent],[HaircutPercent],[CloseNAV],[TotalNAVReksadana],[NAWCHaircut],[BondRating],";
        string _paramaterCommand = "@Date,@InstrumentPK,@ClosePriceValue,@LowPriceValue,@HighPriceValue,@LiquidityPercent,@HaircutPercent,@CloseNAV,@TotalNAVReksadana,@NAWCHaircut,@BondRating,";

        //2
        private ClosePrice setClosePrice(SqlDataReader dr)
        {
            ClosePrice M_ClosePrice = new ClosePrice();
            M_ClosePrice.ClosePricePK = Convert.ToInt32(dr["ClosePricePK"]);
            M_ClosePrice.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_ClosePrice.Selected = Convert.ToBoolean(dr["Selected"]);
            M_ClosePrice.Status = Convert.ToInt32(dr["Status"]);
            M_ClosePrice.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_ClosePrice.Notes = Convert.ToString(dr["Notes"]);
            M_ClosePrice.Date = Convert.ToString(dr["Date"]);
            M_ClosePrice.InstrumentPK = Convert.ToInt32(dr["InstrumentPK"]);
            M_ClosePrice.InstrumentID = Convert.ToString(dr["InstrumentID"]);
            M_ClosePrice.InstrumentName = Convert.ToString(dr["InstrumentName"]);
            M_ClosePrice.ClosePriceValue = Convert.ToDecimal(dr["ClosePriceValue"]);
            M_ClosePrice.LowPriceValue = Convert.ToDecimal(dr["LowPriceValue"]);
            M_ClosePrice.HighPriceValue = Convert.ToDecimal(dr["HighPriceValue"]);
            M_ClosePrice.LiquidityPercent = Convert.ToDecimal(dr["LiquidityPercent"]);
            M_ClosePrice.HaircutPercent = Convert.ToDecimal(dr["HaircutPercent"]);
            M_ClosePrice.CloseNAV = Convert.ToDecimal(dr["CloseNAV"]);
            M_ClosePrice.TotalNAVReksadana = Convert.ToDecimal(dr["TotalNAVReksadana"]);
            M_ClosePrice.NAWCHaircut = Convert.ToDecimal(dr["NAWCHaircut"]);
            M_ClosePrice.BondRating = Convert.ToString(dr["BondRating"]);
            M_ClosePrice.BondRatingDesc = Convert.ToString(dr["BondRatingDesc"]);
            M_ClosePrice.Type = Convert.ToString(dr["Type"]);
            M_ClosePrice.EntryUsersID = dr["EntryUsersID"].ToString();
            M_ClosePrice.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_ClosePrice.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_ClosePrice.VoidUsersID = dr["VoidUsersID"].ToString();
            M_ClosePrice.EntryTime = dr["EntryTime"].ToString();
            M_ClosePrice.UpdateTime = dr["UpdateTime"].ToString();
            M_ClosePrice.ApprovedTime = dr["ApprovedTime"].ToString();
            M_ClosePrice.VoidTime = dr["VoidTime"].ToString();
            M_ClosePrice.DBUserID = dr["DBUserID"].ToString();
            M_ClosePrice.DBTerminalID = dr["DBTerminalID"].ToString();
            M_ClosePrice.LastUpdate = dr["LastUpdate"].ToString();
            return M_ClosePrice;
        }

        public List<ClosePrice> ClosePrice_SelectByDateFromTo(int _status, DateTime _dateFrom, DateTime _dateTo)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<ClosePrice> L_ClosePrice = new List<ClosePrice>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText =
                                @"Select case when cp.Status=1 then 'PENDING' else case when cp.Status = 2 then 'APPROVED' else case when cp.Status = 3 then 'VOID' else 'WAITING' end end end StatusDesc,I.ID InstrumentID, I.Name InstrumentName,MV.DescOne BondRatingDesc,J.ID Type, CP.* from ClosePrice CP left join 
                                Instrument I on CP.InstrumentPK = I.InstrumentPK and I.status = 2 left join 
                                MasterValue MV on CP.BondRating = MV.Code and MV.status = 2 and MV.ID = 'BondRating' left join
                                InstrumentType J on I.InstrumentTypePK = J.InstrumentTypePK and J.status = 2 
                                where CP.status = @status and Date between @DateFrom and @DateTo 
                                order by cp.ClosePricePK, cp.Date";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText =
                                @"Select case when cp.Status=1 then 'PENDING' else case when cp.Status = 2 then 'APPROVED' else case when cp.Status = 3 then 'VOID' else 'WAITING' end end end StatusDesc,I.ID InstrumentID, I.Name InstrumentName,MV.DescOne BondRatingDesc,J.ID Type, CP.* from ClosePrice CP left join 
                                Instrument I on CP.InstrumentPK = I.InstrumentPK and I.status = 2 left join 
                                MasterValue MV on CP.BondRating = MV.Code and MV.status = 2 and MV.ID = 'BondRating'left join
                                InstrumentType J on I.InstrumentTypePK = J.InstrumentTypePK and J.status = 2
                                where Date between @DateFrom and @DateTo 
                                order by cp.ClosePricePK, cp.Date";
                        }

                        cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _dateTo);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_ClosePrice.Add(setClosePrice(dr));
                                }
                            }
                            return L_ClosePrice;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public int ClosePrice_Add(ClosePrice _closePrice, bool _havePrivillege)
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
                                 "Select isnull(max(ClosePricePk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@LastUpdate From ClosePrice";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _closePrice.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);
                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[LastUpdate])" +
                                "Select isnull(max(ClosePricePk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@LastUpdate From ClosePrice";
                        }
                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@Date", _closePrice.Date);
                        cmd.Parameters.AddWithValue("@InstrumentPK", _closePrice.InstrumentPK);
                        cmd.Parameters.AddWithValue("@ClosePriceValue", _closePrice.ClosePriceValue);
                        cmd.Parameters.AddWithValue("@LowPriceValue", _closePrice.LowPriceValue);
                        cmd.Parameters.AddWithValue("@HighPriceValue ", _closePrice.HighPriceValue);
                        cmd.Parameters.AddWithValue("@LiquidityPercent", _closePrice.LiquidityPercent);
                        cmd.Parameters.AddWithValue("@HaircutPercent", _closePrice.HaircutPercent);
                        cmd.Parameters.AddWithValue("@CloseNAV", _closePrice.CloseNAV);
                        cmd.Parameters.AddWithValue("@TotalNAVReksadana", _closePrice.TotalNAVReksadana);
                        cmd.Parameters.AddWithValue("@NAWCHaircut", _closePrice.NAWCHaircut);
                        cmd.Parameters.AddWithValue("@BondRating", _closePrice.BondRating);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _closePrice.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                        cmd.ExecuteNonQuery();

                        return _host.Get_LastPKByLastUpate(_dateTimeNow, "ClosePrice");
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public int ClosePrice_Update(ClosePrice _closePrice, bool _havePrivillege)
        {
            try
            {
                int _newHisPK;
                int status = _host.Get_Status(_closePrice.ClosePricePK, _closePrice.HistoryPK, "closePrice");
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update ClosePrice set status=2,Notes=@Notes,Date=@Date,InstrumentPK=@InstrumentPK,ClosePriceValue=@ClosePriceValue,LowPriceValue=@LowPriceValue,HighPriceValue=@HighPriceValue,LiquidityPercent=@LiquidityPercent, " +
                                "HaircutPercent=@HaircutPercent,CloseNAV=@CloseNAV,TotalNAVReksadana=@TotalNAVReksadana,NAWCHaircut=@NAWCHaircut,BondRating=@BondRating," +
                                "ApprovedUsersID=@ApprovedUsersID, " +
                                "ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@LastUpdate " +
                                "where ClosePricePK = @PK and historyPK = @HistoryPK ";
                            cmd.Parameters.AddWithValue("@HistoryPK", _closePrice.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _closePrice.ClosePricePK);
                            cmd.Parameters.AddWithValue("@Date", _closePrice.Date);
                            cmd.Parameters.AddWithValue("@Notes", _closePrice.Notes);
                            cmd.Parameters.AddWithValue("@InstrumentPK", _closePrice.InstrumentPK);
                            cmd.Parameters.AddWithValue("@ClosePriceValue", _closePrice.ClosePriceValue);
                            cmd.Parameters.AddWithValue("@LowPriceValue", _closePrice.LowPriceValue);
                            cmd.Parameters.AddWithValue("@HighPriceValue ", _closePrice.HighPriceValue);
                            cmd.Parameters.AddWithValue("@LiquidityPercent", _closePrice.LiquidityPercent);
                            cmd.Parameters.AddWithValue("@HaircutPercent", _closePrice.HaircutPercent);
                            cmd.Parameters.AddWithValue("@CloseNAV", _closePrice.CloseNAV);
                            cmd.Parameters.AddWithValue("@TotalNAVReksadana", _closePrice.TotalNAVReksadana);
                            cmd.Parameters.AddWithValue("@NAWCHaircut", _closePrice.NAWCHaircut);
                            cmd.Parameters.AddWithValue("@BondRating", _closePrice.BondRating);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _closePrice.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _dateTimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _closePrice.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);
                            cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update ClosePrice set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where ClosePricePK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _closePrice.ClosePricePK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _closePrice.EntryUsersID);
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
                                cmd.CommandText = "Update ClosePrice set Notes=@Notes,Date=@Date,InstrumentPK=@InstrumentPK,ClosePriceValue=@ClosePriceValue,LowPriceValue=@LowPriceValue,HighPriceValue=@HighPriceValue,LiquidityPercent=@LiquidityPercent," +
                                " HaircutPercent=@HaircutPercent,CloseNAV=@CloseNAV,TotalNAVReksadana=@TotalNAVReksadana,NAWCHaircut=@NAWCHaircut,BondRating=@BondRating," +
                                "UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@LastUpdate " +
                                "where ClosePricePK = @PK and historyPK = @HistoryPK ";
                                cmd.Parameters.AddWithValue("@HistoryPK", _closePrice.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _closePrice.ClosePricePK);
                                cmd.Parameters.AddWithValue("@Date", _closePrice.Date);
                                cmd.Parameters.AddWithValue("@Notes", _closePrice.Notes);
                                cmd.Parameters.AddWithValue("@InstrumentPK", _closePrice.InstrumentPK);
                                cmd.Parameters.AddWithValue("@ClosePriceValue", _closePrice.ClosePriceValue);
                                cmd.Parameters.AddWithValue("@LowPriceValue", _closePrice.LowPriceValue);
                                cmd.Parameters.AddWithValue("@HighPriceValue", _closePrice.HighPriceValue);
                                cmd.Parameters.AddWithValue("@LiquidityPercent", _closePrice.LiquidityPercent);
                                cmd.Parameters.AddWithValue("@HaircutPercent", _closePrice.HaircutPercent);
                                cmd.Parameters.AddWithValue("@CloseNAV", _closePrice.CloseNAV);
                                cmd.Parameters.AddWithValue("@TotalNAVReksadana", _closePrice.TotalNAVReksadana);
                                cmd.Parameters.AddWithValue("@NAWCHaircut", _closePrice.NAWCHaircut);
                                cmd.Parameters.AddWithValue("@BondRating", _closePrice.BondRating);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _closePrice.EntryUsersID);
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
                                _newHisPK = _host.Get_NewHistoryPK(_closePrice.ClosePricePK, "ClosePrice");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From ClosePrice where ClosePricePK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _closePrice.ClosePricePK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _closePrice.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@Date", _closePrice.Date);
                                cmd.Parameters.AddWithValue("@InstrumentPK", _closePrice.InstrumentPK);
                                cmd.Parameters.AddWithValue("@ClosePriceValue", _closePrice.ClosePriceValue);
                                cmd.Parameters.AddWithValue("@LowPriceValue", _closePrice.LowPriceValue);
                                cmd.Parameters.AddWithValue("@HighPriceValue", _closePrice.HighPriceValue);
                                cmd.Parameters.AddWithValue("@LiquidityPercent", _closePrice.LiquidityPercent);
                                cmd.Parameters.AddWithValue("@HaircutPercent", _closePrice.HaircutPercent);
                                cmd.Parameters.AddWithValue("@CloseNAV", _closePrice.CloseNAV);
                                cmd.Parameters.AddWithValue("@TotalNAVReksadana", _closePrice.TotalNAVReksadana);
                                cmd.Parameters.AddWithValue("@NAWCHaircut", _closePrice.NAWCHaircut);
                                cmd.Parameters.AddWithValue("@BondRating", _closePrice.BondRating);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _closePrice.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _dateTimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update ClosePrice set status= 4,Notes=@Notes," +
                                "LastUpdate=@LastUpdate where ClosePricePK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _closePrice.Notes);
                                cmd.Parameters.AddWithValue("@PK", _closePrice.ClosePricePK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _closePrice.HistoryPK);
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

        public void ClosePrice_Approved(ClosePrice _closePrice)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update ClosePrice set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,LastUpdate=@LastUpdate " +
                            "where ClosePricePK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _closePrice.ClosePricePK);
                        cmd.Parameters.AddWithValue("@historyPK", _closePrice.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _closePrice.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update ClosePrice set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where ClosePricePK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _closePrice.ClosePricePK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _closePrice.ApprovedUsersID);
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

        public void ClosePrice_Reject(ClosePrice _closePrice)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update ClosePrice set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate = @LastUpdate " +
                            "where ClosePricePK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _closePrice.ClosePricePK);
                        cmd.Parameters.AddWithValue("@historyPK", _closePrice.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _closePrice.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update ClosePrice set status= 2,LastUpdate=@LastUpdate where ClosePricePK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _closePrice.ClosePricePK);
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

        public void ClosePrice_Void(ClosePrice _closePrice)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update ClosePrice set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate = @LastUpdate " +
                            "where ClosePricePK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _closePrice.ClosePricePK);
                        cmd.Parameters.AddWithValue("@historyPK", _closePrice.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _closePrice.VoidUsersID);
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

        // AREA LAIN-LAIN dimulai dari sini ( untuk function diluar standart )     
        public string ImportHaircutMKBD(string _fileName, string _userID, string _date)
        {
            string result = string.Empty;
            string _msg;
            SqlBulkCopy bulkCopy = new SqlBulkCopy(Tools.conString,
                SqlBulkCopyOptions.TableLock);
            bulkCopy.DestinationTableName = "dbo.HaircutMKBDImportTemp";
            bulkCopy.WriteToServer(CreateDataTableFromFileHaircutMKBD(_fileName));

            //UPDATE HAIRCUT KE TABEL CLOSE PRICE
            try
            {
                DateTime _dateTimeNow = DateTime.Now;

                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = " IF Exists(select * from HaircutMKBDImportTemp Where KodeSaham in (   " +
                                          " Select Id From instrument Where Status = 2 ))   " +

                                          " BEGIN    " +

                                               " IF Exists(Select * from closePrice where Date = @Date and status = 2) " +
                                               " BEGIN " +
                                                   " Update  A set A.NAWCHaircut = isnull(B.HCMKBD,0) from  ClosePrice A  " +
                                                   " Left join  Instrument C on A.InstrumentPK = C.InstrumentPK and C.Status = 2 " +
                                                   " Left join HaircutMKBDImportTemp B on C.ID = B.KodeSaham and A.status =2	  " +
                                                   " where A.Date = @Date " +
                                                   " select 'Success Update Haircut MKBD / NAWC' A " +
                                               " END " +
                                               " ELSE " +
                                               " BEGIN " +
                                                   " select 'Fail, no data Close Price' A " +
                                               " END " +
                                          " END    ";

                        cmd.Parameters.AddWithValue("@Date", _date);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                _msg = Convert.ToString(dr["A"]);
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
            catch (Exception err)
            {
                throw err;
            }
        }

        private DataTable CreateDataTableFromFileHaircutMKBD(string _fileName)
        {
            DataTable dt = new DataTable();
            DataColumn dc;
            DataRow dr;

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Tanggal";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "KodeSaham";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Nama";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.Decimal");
            dc.ColumnName = "HCMKBD";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Keterangan";
            dc.Unique = false;
            dt.Columns.Add(dc);

            StreamReader sr = new StreamReader(Tools.TxtFilePath + _fileName);
            string input;

            while ((input = sr.ReadLine()) != null)
            {
                string[] s = input.Split(new char[] { '|' });
                dr = dt.NewRow();
                dr["Tanggal"] = s[0];
                dr["KodeSaham"] = s[1];
                dr["Nama"] = s[2];
                dr["HCMKBD"] = s[3];
                dr["Keterangan"] = s[4];
                dt.Rows.Add(dr);
            }
            sr.Close();
            return dt;
        }

        //public string ImportClosePrice(string _fileName, string _userID, string _date)
        //{
        //    string _msg;
        //    DateTime _dateTime = DateTime.Now;
        //    try
        //    {
        //        using (SqlConnection DbCon = new SqlConnection(Tools.conString))
        //        {
        //            DbCon.Open();
        //            using (SqlCommand cmd = DbCon.CreateCommand())
        //            {

        //                //Validasi
        //                cmd.CommandText = "select * from ClosePrice CP left join Instrument I on CP.InstrumentPK = I.InstrumentPK where I.status = 2 and Date = @Date and I.InstrumentTypePK in (1) and CP.status in (1,2) ";
        //                cmd.Parameters.AddWithValue("@Date", _date);
        //                using (SqlDataReader dr1 = cmd.ExecuteReader())
        //                {
        //                    if (dr1.HasRows)
        //                    {
        //                        return "Import Cancel, Already Import Close Price Equity For this Day, Void / Reject First!";
        //                    }
        //                    else
        //                    {
        //                        using (OdbcConnection odConnection = new OdbcConnection(Tools.conStringOdBc))
        //                        {

        //                            odConnection.Open();
        //                            using (OdbcCommand odCmd = odConnection.CreateCommand())
        //                            {
        //                                odCmd.CommandText = "SELECT * FROM " + Tools.DBFFilePath + _fileName;
        //                                using (OdbcDataReader odRdr = odCmd.ExecuteReader())
        //                                {
        //                                    using (SqlConnection sqlConnection = new SqlConnection(Tools.conString))
        //                                    {
        //                                        sqlConnection.Open();
        //                                        SqlTransaction sqlT;
        //                                        sqlT = sqlConnection.BeginTransaction("ImportClosePrice");
        //                                        try
        //                                        {
        //                                            using (SqlCommand sqlCmd = new SqlCommand("Truncate Table ClosePriceImportTemp", sqlConnection))
        //                                            {
        //                                                sqlCmd.Transaction = sqlT;
        //                                                sqlCmd.ExecuteNonQuery();
        //                                            }

        //                                            while (odRdr.Read())
        //                                            {
        //                                                using (SqlCommand sqlCmd = new SqlCommand("insert into ClosePriceImportTemp(STK_CODE,STK_CLOS)  " +
        //                                                    "values (@param1,@param2)", sqlConnection))
        //                                                {
        //                                                    sqlCmd.Transaction = sqlT;
        //                                                    sqlCmd.Parameters.AddWithValue("@param1", odRdr[0]);
        //                                                    sqlCmd.Parameters.AddWithValue("@param2", odRdr[1]);

        //                                                    sqlCmd.ExecuteNonQuery();
        //                                                }
        //                                            }

        //                                            using (SqlCommand sqlCmd = sqlConnection.CreateCommand())
        //                                            {
        //                                                sqlCmd.CommandText = "IF Exists(select * from ClosePriceImportTemp Where STK_Code in ( " +
        //                                                    "Select Id From instrument Where Status = 2 and InstrumentTypePK  = 1)) \n BEGIN " +
        //                                                    "DELETE A from ClosePrice A left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2 " +
        //                                                    "Where Date = @Date and B.InstrumentTypePK = 1" +
        //                                                    "DECLARE @ClosePricePK BigInt " +
        //                                                    "SELECT @ClosePricePK = isnull(Max(ClosePricePK),0) FROM ClosePrice \n " +
        //                                                    "INSERT INTO ClosePrice(ClosePricePK,HistoryPK,Status,Date,InstrumentPK, " +
        //                                                    "ClosePriceValue,EntryUsersID,EntryTime,LastUpdate) \n " +
        //                                                    "SELECT Row_number() over(order by STK_Code) + @ClosePricePK,1,1,@Date, " +
        //                                                    "A.InstrumentPK,isnull(B.STK_CLOS,0),@UserID,@TimeNow,@TimeNow " +
        //                                                    "FROM ClosePriceImportTemp B LEFT JOIN Instrument A On A.ID = B.STK_CODE and A.status = 2 where A.InstrumentTypePK = 1  " +
        //                                                    "\n " +
        //                                                    "Select 'Success' A END " +
        //                                                    " ";

        //                                                sqlCmd.Parameters.AddWithValue("@UserID", _userID);
        //                                                sqlCmd.Parameters.AddWithValue("@Date", _date);
        //                                                sqlCmd.Parameters.AddWithValue("@TimeNow", _dateTime);
        //                                                sqlCmd.Transaction = sqlT;
        //                                                using (SqlDataReader dr = sqlCmd.ExecuteReader())
        //                                                {
        //                                                    if (!dr.Read())
        //                                                    {
        //                                                        _msg = "Import Close Price Failed";
        //                                                    }
        //                                                    else
        //                                                    {
        //                                                        _msg = "Import Close Price Success";
        //                                                    }
        //                                                }

        //                                                sqlT.Commit();
        //                                                return _msg;
        //                                            }
        //                                        }
        //                                        catch (Exception err)
        //                                        {
        //                                            sqlT.Rollback();
        //                                            throw err;
        //                                        }
        //                                    }
        //                                }
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception err)
        //    {
        //        throw err;
        //    }
        //}

        public string ImportClosePriceBond(string _fileSource, string _userID, string _date)
        {
            string _msg = "";
            try
            {
                DateTime _now = DateTime.Now;

                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        //Validasi
                        cmd.CommandText = "select * from ClosePrice CP left join Instrument I on CP.InstrumentPK = I.InstrumentPK where I.status = 2 and Date = @Date and I.InstrumentTypePK in (2,3) and CP.status in (1,2) ";
                        cmd.Parameters.AddWithValue("@Date", _date);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                return "Import Cancel, Already Import Close Price Bond For this Day, Void / Reject First!";
                            }
                            else
                            {
                                //delete data yang lama
                                using (SqlConnection conn = new SqlConnection(Tools.conString))
                                {
                                    conn.Open();
                                    using (SqlCommand cmd1 = conn.CreateCommand())
                                    {
                                        cmd1.CommandText = "truncate table ClosePriceBondTemp";
                                        cmd1.ExecuteNonQuery();
                                    }
                                }

                                // import data ke temp dulu
                                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(Tools.conString, SqlBulkCopyOptions.TableLock))
                                {
                                    bulkCopy.DestinationTableName = "dbo.ClosePriceBondTemp";
                                    bulkCopy.WriteToServer(CreateDataTableFromClosePriceBondTempExcelFile(_fileSource));
                                    _msg = "Import Close Price Bond Success";
                                }

                                // logic kalo Reconcile success
                                using (SqlConnection conn = new SqlConnection(Tools.conString))
                                {
                                    conn.Open();
                                    using (SqlCommand cmd1 = conn.CreateCommand())
                                    {
                                        cmd1.CommandText =

                                        @"Declare @ClosePricePK bigint 
                                        Select @ClosePricePK  = isnull(max(ClosePricePk),0) from ClosePrice   
                                        Insert Into ClosePrice (ClosePricePK,historyPK,status,Date,InstrumentPK,ClosePriceValue,LowPriceValue,HighPriceValue ) 
                                        select Row_number() over(order by I.InstrumentPK) + @ClosePricePK,1,1,@Date,I.InstrumentPK,Amount,0,0 from ClosePriceBondTemp C 
                                        Left Join Instrument I on C.InstrumentID = I.ID and I.status = 2 
                                        where C.InstrumentID is not null ";

                                        cmd1.Parameters.AddWithValue("@Date", _date);
                                        cmd1.Parameters.AddWithValue("@UsersID", _userID);
                                        cmd1.Parameters.AddWithValue("@LastUpdate", _now);
                                        cmd1.ExecuteNonQuery();

                                    }
                                    _msg = "Import Close Price Bond Done";

                                }

                            }
                        }
                    }
                }



                return _msg;
            }
            catch (Exception err)
            {
                throw err;
            }
        }
        
        private DataTable CreateDataTableFromClosePriceBondTempExcelFile(string _path)
        {
            try
            {
                using (DataTable dt = new DataTable())
                {
                    DataColumn dc;
                    DataRow dr;

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Int32");
                    dc.ColumnName = "ClosePriceBondPK";
                    dc.Unique = false;
                    dc.AutoIncrement = true;
                    dc.AutoIncrementSeed = 1;
                    dc.AutoIncrementStep = 1;
                    dt.Columns.Add(dc);

                    //dc = new DataColumn();
                    //dc.DataType = System.Type.GetType("System.String");
                    //dc.ColumnName = "Date";
                    //dc.Unique = false;
                    //dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "InstrumentID";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Decimal");
                    dc.ColumnName = "ClosePriceValue";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    //dc = new DataColumn();
                    //dc.DataType = System.Type.GetType("System.Decimal");
                    //dc.ColumnName = "LowPriceValue";
                    //dc.Unique = false;
                    //dt.Columns.Add(dc);

                    //dc = new DataColumn();
                    //dc.DataType = System.Type.GetType("System.Decimal");
                    //dc.ColumnName = "HighPriceValue";
                    //dc.Unique = false;
                    //dt.Columns.Add(dc);



                    using (OleDbConnection odConnection = new OleDbConnection(Tools.ConStringExcel2007(_path)))
                    {
                        odConnection.Open();
                        using (OleDbCommand odCmd = odConnection.CreateCommand())
                        {
                            // _oldfilename = nama sheet yang ada di file excel yang diimport
                            odCmd.CommandText = "SELECT * FROM [Sheet1$]";
                            using (OleDbDataReader odRdr = odCmd.ExecuteReader())
                            {
                                // start counting from index = 1 --> skipping the header (index=0)
                                for (int i = 1; i <= 1; i++)
                                {
                                    odRdr.Read();
                                }
                                do
                                {
                                    dr = dt.NewRow();

                                    //dr["Date"] = odRdr[0];
                                    dr["InstrumentID"] = odRdr[0];
                                    dr["ClosePriceValue"] = odRdr[1];
                                   
                                    //dr["ClosePriceValue"] = (odRdr[1].ToString().Replace(",", ""));
                                    //dr["LowPriceValue"] = (odRdr[2].ToString().Replace(",", ""));
                                    //dr["HighPriceValue"] = (odRdr[3].ToString().Replace(",", ""));

                                    if (dr["ClosePriceBondPK"].Equals(DBNull.Value) != true) { dt.Rows.Add(dr); }
                                } while (odRdr.Read());
                            }
                        }
                        odConnection.Close();
                    }

                    return dt;
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public void ClosePrice_ApproveBySelected(string _usersID, string _permissionID, DateTime _dateFrom, DateTime _dateTo)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Declare @IPAddress nvarchar(50) select @IPAddress = IPAddress from Users where ID = @UsersID and Status = 2 \n " +
                                 " \n Insert into Activity(Time,PermissionID,ObjectTable,ObjectTablePK,Status,Message,UsersID,IPAddress,LastUpdate) \n " +
                                 "Select @Time,@PermissionID,'ClosePrice',ClosePricePK,1,'Approve by Selected Data',@UsersID,@IPAddress,@Time  from ClosePrice where Date between @DateFrom and @DateTo and Status = 1 and Selected  = 1 " +
                                 "\n update ClosePrice set status = 2,ApprovedUsersID = @UsersID,ApprovedTime = @Time,LastUpdate=@Time  where Date between @DateFrom and @DateTo and Status = 1 and Selected  = 1 \n " +
                                 "Update ClosePrice set status= 3,VoidUsersID=@UsersID,VoidTime=@Time,LastUpdate=@Time  where Date between @DateFrom and @DateTo and Status = 4 and Selected  = 1 " +
                                 " " +
                                 "";
                        cmd.Parameters.AddWithValue("@PermissionID", _permissionID);
                        cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _dateTo);
                        cmd.Parameters.AddWithValue("@UsersID", _usersID);
                        cmd.Parameters.AddWithValue("@Time", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public void ClosePrice_RejectBySelected(string _usersID, string _permissionID, DateTime _dateFrom, DateTime _dateTo)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Declare @IPAddress nvarchar(50) select @IPAddress = IPAddress from Users where ID = @UsersID and Status = 2 \n " +
                                          " \n Insert into Activity(Time,PermissionID,ObjectTable,ObjectTablePK,Status,Message,UsersID,IPAddress,LastUpdate) \n " +
                                          "Select @Time,@PermissionID,'ClosePrice',ClosePricePK,1,'Reject by Selected Data',@UsersID,@IPAddress,@Time  from ClosePrice where Date between @DateFrom and @DateTo and Status = 1 and Selected  = 1 " +
                                          "\n update ClosePrice set status = 3,VoidUsersID = @UsersID,VoidTime = @Time,LastUpdate=@Time where Date between @DateFrom and @DateTo and Status = 1 and Selected  = 1 " +
                                          "Update ClosePrice set status= 2  where Date between @DateFrom and @DateTo and Status = 4 and Selected  = 1" +
                                          " " +
                                          "";
                        cmd.Parameters.AddWithValue("@PermissionID", _permissionID);
                        cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _dateTo);
                        cmd.Parameters.AddWithValue("@UsersID", _usersID);
                        cmd.Parameters.AddWithValue("@Time", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public void ClosePrice_VoidBySelected(string _usersID, string _permissionID, DateTime _dateFrom, DateTime _dateTo)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Declare @IPAddress nvarchar(50) select @IPAddress = IPAddress from Users where ID = @UsersID and Status = 2 \n " +
                                          " \n Insert into Activity(Time,PermissionID,ObjectTable,ObjectTablePK,Status,Message,UsersID,IPAddress,LastUpdate) \n " +
                                          "Select @Time,@PermissionID,'ClosePrice',ClosePricePK,1,'Void by Selected Data',@UsersID,@IPAddress,@Time  from ClosePrice where Date between @DateFrom and @DateTo and Status = 2  and Selected  = 1 " +
                                          "\n update ClosePrice set status = 3,VoidUsersID = @UsersID,VoidTime = @Time,LastUpdate=@Time where Date between @DateFrom and @DateTo and Status = 2 and Selected  = 1 " +
                                          " " +
                                          "";
                        cmd.Parameters.AddWithValue("@PermissionID", _permissionID);
                        cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _dateTo);
                        cmd.Parameters.AddWithValue("@UsersID", _usersID);
                        cmd.Parameters.AddWithValue("@Time", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public bool Validate_ApproveClosePrice(DateTime _dateFrom, DateTime _dateTo)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {


                        cmd.CommandText = " if Not Exists(select * From ClosePrice where Status = 1 and Date between @ValueDateFrom and @ValueDateTo and (UpdateUsersID is not null or UpdateUsersID <> '')) " +
                        " BEGIN Select 1 Result END ELSE BEGIN Select 0 Result END ";

                        cmd.Parameters.AddWithValue("@ValueDateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@ValueDateTo", _dateTo);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return Convert.ToBoolean(dr["Result"]);

                            }
                            return false;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public bool Validate_EndDayTrailsFundPortfolio(DateTime _valueDate)
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
                        if Not Exists
                        (select * From ClosePrice where  Status = 2 and Date = @ValueDate)    
                        BEGIN 
                        Select 1 Result 
                        END 
                        ELSE 
                        BEGIN     
                        Select 0 Result 
                        END    ";

                        cmd.Parameters.AddWithValue("@ValueDate", _valueDate);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return Convert.ToBoolean(dr["Result"]);

                            }
                            return false;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public string ImportClosePrice(string _fileName, string _userID, string _date)
        {
            string _msg;
            try
            {
                DateTime _dateTimeNow = DateTime.Now;

               using (OdbcConnection odConnection = new OdbcConnection(Tools.conStringOdBc))
               {              

                    odConnection.Open();
                    using (OdbcCommand odCmd = odConnection.CreateCommand())
                    {
                        odCmd.CommandText = "SELECT * FROM " + Tools.DBFFilePath + _fileName;
                        using (OdbcDataReader odRdr = odCmd.ExecuteReader())
                        {
                            using (SqlConnection sqlConnection = new SqlConnection(Tools.conString))
                            {
                                sqlConnection.Open();
                                SqlTransaction sqlT;
                                sqlT = sqlConnection.BeginTransaction("ImportClosePrice");
                                try
                                {
                                    using (SqlCommand sqlCmd = new SqlCommand("Truncate Table ClosePriceImportTemp", sqlConnection))
                                    {
                                        sqlCmd.Transaction = sqlT;
                                        sqlCmd.ExecuteNonQuery();
                                    }

                                    while (odRdr.Read())
                                    {
                                        using (SqlCommand sqlCmd = new SqlCommand("insert into ClosePriceImportTemp(DATE,STK_CODE,STK_CLOS)  " +
                                            "values (@Date,@param1,@param2)", sqlConnection))
                                        {
                                            sqlCmd.Transaction = sqlT;
                                            sqlCmd.Parameters.AddWithValue("@Date", _date);
                                            sqlCmd.Parameters.AddWithValue("@param1", odRdr[0]);
                                            sqlCmd.Parameters.AddWithValue("@param2", odRdr[1]);

                                            sqlCmd.ExecuteNonQuery();
                                        }
                                    }

                                    using (SqlCommand sqlCmd = sqlConnection.CreateCommand())
                                    {
                                        sqlCmd.CommandText =
                                        @"
                                        If Exists(select distinct A.InstrumentPK from closeprice A 
                                        left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2
                                        where B.ID in (select STK_CODE from ClosePriceImportTemp)
                                        and A.Status in (1,2) and Date = @Date)

                                        BEGIN
                                            select 'Already Data' Result
                                        END
                                        ELSE
                                        BEGIN

                                            DECLARE @ClosePricePK BigInt  
                                            SELECT @ClosePricePK = isnull(Max(ClosePricePK),0) FROM ClosePrice
                                            INSERT INTO ClosePrice(ClosePricePK,HistoryPK,Status,Date,InstrumentPK,  
                                            ClosePriceValue,EntryUsersID,EntryTime,LastUpdate)
                                            SELECT Row_number() over(order by STK_Code) + @ClosePricePK,1,1,@Date,  
                                            A.InstrumentPK,isnull(B.STK_CLOS,0),@UserID,@TimeNow,@TimeNow  
                                            FROM ClosePriceImportTemp B LEFT JOIN Instrument A On A.ID = B.STK_CODE and A.status = 2 where A.InstrumentTypePK in (1,4,16)   

                                            delete ClosePrice where InstrumentPK = 0

                                            select 'Success' Result
                                        END ";

                                        sqlCmd.Parameters.AddWithValue("@Date", _date);
                                        sqlCmd.Parameters.AddWithValue("@UserID", _userID);
                                        sqlCmd.Parameters.AddWithValue("@TimeNow", _dateTimeNow);
                                        sqlCmd.Transaction = sqlT;
                                        using (SqlDataReader dr = sqlCmd.ExecuteReader())
                                        {
                                            if (dr.Read())
                                            {
                                                _msg = Convert.ToString(dr["Result"]);
                                            }
                                            else
                                            {
                                                _msg = "";
                                            }
                                        }

                                        sqlT.Commit();
                                        return _msg;
                                    }
                                }
                                catch (Exception err)
                                {
                                    sqlT.Rollback();
                                    throw err;
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

        public string ImportIBPA(string _fileName, string _userID, string _date)
        {
            string _msg;
            try
            {
                DateTime _dateTime = DateTime.Now;

                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = "truncate table ClosePriceIBPATemp";
                        cmd.ExecuteNonQuery();

                    }

                    // import data ke temp dulu
                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(Tools.conString, SqlBulkCopyOptions.TableLock))
                    {
                        bulkCopy.DestinationTableName = "dbo.ClosePriceIBPATemp";
                        bulkCopy.WriteToServer(CreateDataTableFromFileClosePriceIBPA(_fileName));

                    }

                    using (SqlConnection conn = new SqlConnection(Tools.conString))
                    {
                        conn.Open();
                        using (SqlCommand cmd1 = conn.CreateCommand())
                        {
                            cmd1.CommandText =
                          @"
                          


                                If Exists(Select * from ClosePriceIBPATemp where (FairPrice < LowPrice) or (FairPrice > HighPrice))

                                BEGIN
                                    select 'Fair Price' Result
                                END
                                ELSE
                                BEGIN
                                    select 'Done' Result 
                                END
                            
                                ";
                            using (SqlDataReader dr = cmd1.ExecuteReader())
                            {
                                dr.Read();
                                if (Convert.ToString(dr["Result"]) == "Fair Price")
                                {
                                    _msg = Convert.ToString(dr["Result"]);

                                }
                                else
                                {

                                    conn.Close();
                                    conn.Open();
                                    using (SqlCommand cmd2 = conn.CreateCommand())
                                    {
                                        cmd2.CommandText =
                                        @"
                                            declare @Date Datetime
                                            select @Date = Date From ClosePriceIBPATemp

                                            If Exists(select distinct A.InstrumentPK from closeprice A 
                                            left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2
                                            where B.ID in (select Series from ClosePriceIBPATemp)
                                            and A.Status in (1,2) and Date = @Date)

                                            BEGIN
                                                select 'Already Data' Result
                                            END
                                            ELSE
                                            BEGIN
                                                select 'Done' Result 
                                            END

                            
                                            ";
                                        using (SqlDataReader dr1 = cmd2.ExecuteReader())
                                        {
                                            dr1.Read();
                                            if (Convert.ToString(dr1["Result"]) == "Already Data")
                                            {
                                                _msg = Convert.ToString(dr1["Result"]);

                                            }
                                            else
                                            {


                                                conn.Close();
                                                conn.Open();


                                                using (SqlCommand cmd3 = conn.CreateCommand())
                                                {
                                                    cmd3.CommandText =
                                                    @"
                                                                    
                                            declare @Date Datetime
                                            select @Date = Date From ClosePriceIBPATemp

                                            Update A set status = 3 from closeprice A 
                                            left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2
                                            where B.ID in (select Series from ClosePriceIBPATemp)
                                            and A.Status in (1,2) and Date = @Date

                                            Declare @ClosePricePK int
                                                Select @ClosePricePK = max(ClosePricePK) + 1 from ClosePrice
                                                set @ClosePricePK = isnull(@ClosePricePK,0)
                                                INSERT INTO [dbo].[ClosePrice]
                                                            ([ClosePricePK]
                                                            ,[HistoryPK]
                                                            ,[Status]
                                                            ,[Notes]
                                                            ,[Date]
                                                            ,[InstrumentPK]
                                                            ,[ClosePriceValue]
                                                            ,[LowPriceValue]
                                                            ,[HighPriceValue]      
                                                            ,[EntryUsersID]
                                                            ,[EntryTime]
                                                            ,[LastUpdate]
                                                )
                                                Select @ClosePricePK +  ROW_NUMBER() OVER(Order By A.Series ASC) ,1,1,'',A.Date,isnull(B.InstrumentPK,0)
                                                ,A.FairPrice,A.LowPrice,A.HighPrice
                                                ,@userID,@Datetime,@Datetime
                                                from ClosePriceIBPATemp  A
                                                Left join Instrument B on A.Series = B.ID and B.status = 2

                                                delete ClosePrice where InstrumentPK = 0

	                                            select 'Import IBPA Success' Result

                                            ";
                                                    cmd3.Parameters.AddWithValue("@UserID", _userID);
                                                    cmd3.Parameters.AddWithValue("@Datetime", _dateTime);
                                                    cmd3.ExecuteNonQuery();

                                                    _msg = "Import IBPA Done";

                                                }

                                            }
                                        }
                                    }
                                }

                                return _msg;

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

        private DataTable CreateDataTableFromFileClosePriceIBPA(string _fileName)
        {
            DataTable dt = new DataTable();
            DataColumn dc;
            DataRow dr;

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
            dc.DataType = System.Type.GetType("System.Decimal");
            dc.ColumnName = "LowPrice";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.Decimal");
            dc.ColumnName = "FairPrice";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.Decimal");
            dc.ColumnName = "HighPrice";
            dc.Unique = false;
            dt.Columns.Add(dc);


            StreamReader sr = new StreamReader(_fileName);
            string input;

            sr.ReadLine();
            while ((input = sr.ReadLine()) != null)
            {
                string[] s = input.Split(new char[] { ',' });
                dr = dt.NewRow();
                dr["Date"] = s[0];
                dr["Series"] = s[1];
                dr["LowPrice"] = s[8];
                dr["FairPrice"] = s[9];
                dr["HighPrice"] = s[10];
                dt.Rows.Add(dr);
            }
            sr.Close();
            return dt;
        }

        public string ClosePrice_ApproveIBPAData(string _userID)
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
                        select @Date = Date From ClosePriceIBPATemp

                        Update A set status = 3 from closeprice A 
                        left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2
                        where B.ID in (select Series from ClosePriceIBPATemp)
                        and A.Status in (1,2) and Date = @Date

                        Declare @ClosePricePK int
                            Select @ClosePricePK = max(ClosePricePK) + 1 from ClosePrice
                            set @ClosePricePK = isnull(@ClosePricePK,0)
                            INSERT INTO [dbo].[ClosePrice]
                                        ([ClosePricePK]
                                        ,[HistoryPK]
                                        ,[Status]
                                        ,[Notes]
                                        ,[Date]
                                        ,[InstrumentPK]
                                        ,[ClosePriceValue]
                                        ,[LowPriceValue]
                                        ,[HighPriceValue]      
                                        ,[EntryUsersID]
                                        ,[EntryTime]
                                        ,[LastUpdate]
                            )
                            Select @ClosePricePK +  ROW_NUMBER() OVER(Order By A.Series ASC) ,1,1,'',A.Date,isnull(B.InstrumentPK,0)
                            ,A.FairPrice,A.LowPrice,A.HighPrice
                            ,@userID,@Datetime,@Datetime
                            from ClosePriceIBPATemp  A
                            Left join Instrument B on A.Series = B.ID and B.status = 2

                            delete ClosePrice where InstrumentPK = 0

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

        public string ClosePrice_ApproveClosePriceEquityData(string _userID)
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
                        select @Date = Date From ClosePriceImportTemp

                        Update A set status = 3 from closeprice A 
                        left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2
                        where B.ID in (select STK_CODE from ClosePriceImportTemp)
                        and A.Status in (1,2) and Date = @Date

                        DECLARE @ClosePricePK BigInt  
                        SELECT @ClosePricePK = isnull(Max(ClosePricePK),0) FROM ClosePrice
                        INSERT INTO ClosePrice(ClosePricePK,HistoryPK,Status,Date,InstrumentPK,  
                        ClosePriceValue,EntryUsersID,EntryTime,LastUpdate)
                        SELECT Row_number() over(order by STK_Code) + @ClosePricePK,1,1,@Date,  
                        A.InstrumentPK,isnull(B.STK_CLOS,0),@UserID,@TimeNow,@TimeNow  
                        FROM ClosePriceImportTemp B LEFT JOIN Instrument A On A.ID = B.STK_CODE and A.status = 2 where A.InstrumentTypePK = 1   

                        delete ClosePrice where InstrumentPK = 0

                        select 'Import Close Price Equity Success' Result

                        ";

                        cmd.Parameters.AddWithValue("@UserID", _userID);
                        cmd.Parameters.AddWithValue("@TimeNow", _dateTimeNow);

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

        public string ImportOldIBPA(string _fileName, string _userID)
        {

            try
            {
                DateTime _dateTimeNow = DateTime.Now;

                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = "truncate table ClosePriceIBPATemp";
                        cmd.ExecuteNonQuery();

                    }

                    // import data ke temp dulu
                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(Tools.conString, SqlBulkCopyOptions.TableLock))
                    {
                        bulkCopy.DestinationTableName = "dbo.ClosePriceIBPATemp";
                        bulkCopy.WriteToServer(CreateDataTableFromFileClosePriceOldIBPA(_fileName));

                    }

                    // logic kalo Reconcile success
                    using (SqlConnection conn = new SqlConnection(Tools.conString))
                    {
                        conn.Open();
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.CommandText =
                            @"declare @Date Datetime
                            select @Date = Date From ClosePriceIBPATemp

                            If Exists(select distinct A.InstrumentPK from closeprice A 
                            left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2
                            where B.ID in (select Series from ClosePriceIBPATemp)
                            and A.Status in (1,2) and Date = @Date)

                            BEGIN
                                select 'Already Data' Result
                            END
                            ELSE
                            BEGIN
                                Declare @ClosePricePK int
                                Select @ClosePricePK = max(ClosePricePK) + 1 from ClosePrice
                                set @ClosePricePK = isnull(@ClosePricePK,0)

                                INSERT INTO [dbo].[ClosePrice]
                                ([ClosePricePK]
                                ,[HistoryPK]
                                ,[Status]
                                ,[Notes]
                                ,[Date]
                                ,[InstrumentPK]
                                ,[ClosePriceValue]
                                ,[LowPriceValue]
                                ,[HighPriceValue]      
                                ,[EntryUsersID]
                                ,[EntryTime]
                                ,[LastUpdate]
                                )
                                Select @ClosePricePK +  ROW_NUMBER() OVER(Order By A.Series ASC) ,1,1,'',A.Date,isnull(B.InstrumentPK,0)
                                ,A.FairPrice,A.LowPrice,A.HighPrice
                                ,@UserID,@Datetime,@Datetime
                                from ClosePriceIBPATemp  A
                                Left join Instrument B on A.Series = B.ID and B.status = 2

                                delete ClosePrice where InstrumentPK = 0

                                select 'Success' Result
                            END ";
                            cmd.Parameters.AddWithValue("@UserID", _userID);
                            cmd.Parameters.AddWithValue("@Datetime", _dateTimeNow);
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

            }
            catch (Exception err)
            {
                throw err;
            }
        }

        private DataTable CreateDataTableFromFileClosePriceOldIBPA(string _fileName)
        {
            DataTable dt = new DataTable();
            DataColumn dc;
            DataRow dr;

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
            dc.DataType = System.Type.GetType("System.Decimal");
            dc.ColumnName = "LowPrice";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.Decimal");
            dc.ColumnName = "FairPrice";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.Decimal");
            dc.ColumnName = "HighPrice";
            dc.Unique = false;
            dt.Columns.Add(dc);


            StreamReader sr = new StreamReader(_fileName);
            string input;

            sr.ReadLine();
            while ((input = sr.ReadLine()) != null)
            {
                string[] s = input.Split(new char[] { ',' });
                dr = dt.NewRow();
                dr["Date"] = s[0];
                dr["Series"] = s[1];
                dr["LowPrice"] = s[3];
                dr["FairPrice"] = s[4];
                dr["HighPrice"] = s[5];
                dt.Rows.Add(dr);
            }
            sr.Close();
            return dt;
        }

        public string ClosePrice_ApproveOldIBPAData(string _userID)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();

                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        if (Tools.ClientCode == "21")
                        {
                            cmd.CommandText = @"
                                                                    
                            declare @Date Datetime
                            select @Date = Date From ClosePriceOldIBPATemp

                            Update A set status = 3 from closeprice A 
                            left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2
                            where B.ID in (select CodeBase from ClosePriceOldIBPATemp)
                            and A.Status in (1,2) and Date = @Date

                            Declare @ClosePricePK int
                                Select @ClosePricePK = max(ClosePricePK) + 1 from ClosePrice
                                set @ClosePricePK = isnull(@ClosePricePK,0)
                                INSERT INTO [dbo].[ClosePrice]
                                            ([ClosePricePK]
                                            ,[HistoryPK]
                                            ,[Status]
                                            ,[Notes]
                                            ,[Date]
                                            ,[InstrumentPK]
                                            ,[ClosePriceValue]
                                            ,[LowPriceValue]
                                            ,[HighPriceValue]      
                                            ,[EntryUsersID]
                                            ,[EntryTime]
                                            ,[LastUpdate]
                                )
                                Select @ClosePricePK +  ROW_NUMBER() OVER(Order By A.CodeBase ASC) ,1,1,'',A.Date,isnull(B.InstrumentPK,0)
                                ,A.MIPrice,A.LowerPrice,A.UpperPrice
                                ,@userID,@Datetime,@Datetime
                                from ClosePriceOldIBPATemp  A
                                Left join Instrument B on A.CodeBase = B.ID and B.status = 2

                                delete ClosePrice where InstrumentPK = 0

	                            select 'Import IBPA Success' Result

                            ";
                        }
                        else
                        {
                            cmd.CommandText =
                            @"declare @Date Datetime
                            select @Date = Date From ClosePriceIBPATemp

                            Update A set status = 3 from closeprice A 
                            left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2
                            where B.ID in (select Series from ClosePriceIBPATemp)
                            and A.Status in (1,2) and Date = @Date

                            Declare @ClosePricePK int
                                Select @ClosePricePK = max(ClosePricePK) + 1 from ClosePrice
                                set @ClosePricePK = isnull(@ClosePricePK,0)
                                INSERT INTO [dbo].[ClosePrice]
                                            ([ClosePricePK]
                                            ,[HistoryPK]
                                            ,[Status]
                                            ,[Notes]
                                            ,[Date]
                                            ,[InstrumentPK]
                                            ,[ClosePriceValue]
                                            ,[LowPriceValue]
                                            ,[HighPriceValue]      
                                            ,[EntryUsersID]
                                            ,[EntryTime]
                                            ,[LastUpdate]
                                )
                                Select @ClosePricePK +  ROW_NUMBER() OVER(Order By A.Series ASC) ,1,1,'',A.Date,isnull(B.InstrumentPK,0)
                                ,A.FairPrice,A.LowPrice,A.HighPrice
                                ,@userID,@Datetime,@Datetime
                                from ClosePriceIBPATemp  A
                                Left join Instrument B on A.Series = B.ID and B.status = 2

                                delete ClosePrice where InstrumentPK = 0

	                            select 'Import IBPA Success' Result

                            ";
                        }


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

        public string ImportClosePriceReksadana(string _fileSource, string _userID, string _date)
        {
            string _msg = "";
            try
            {
                DateTime _now = DateTime.Now;

                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        //Validasi
                        cmd.CommandText = "select * from ClosePrice CP left join Instrument I on CP.InstrumentPK = I.InstrumentPK where I.status = 2 and Date = @Date and I.InstrumentTypePK in (6) and CP.status in (1,2) ";
                        cmd.Parameters.AddWithValue("@Date", _date);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                return "Import Cancel, Already Import Close Price Reksadana For this Day, Void / Reject First!";
                            }
                            else
                            {
                                //delete data yang lama
                                using (SqlConnection conn = new SqlConnection(Tools.conString))
                                {
                                    conn.Open();
                                    using (SqlCommand cmd1 = conn.CreateCommand())
                                    {
                                        cmd1.CommandText = "truncate table ClosePriceReksadanaTemp";
                                        cmd1.ExecuteNonQuery();
                                    }
                                }

                                // import data ke temp dulu
                                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(Tools.conString, SqlBulkCopyOptions.TableLock))
                                {
                                    bulkCopy.DestinationTableName = "dbo.ClosePriceReksadanaTemp";
                                    bulkCopy.WriteToServer(CreateDataTableFromClosePriceReksadanaTempExcelFile(_fileSource));
                                    _msg = "Import Close Price Reksadana Success";
                                }

                                // logic kalo Reconcile success
                                using (SqlConnection conn = new SqlConnection(Tools.conString))
                                {
                                    conn.Open();
                                    using (SqlCommand cmd1 = conn.CreateCommand())
                                    {
                                        cmd1.CommandText =

                                        @"Declare @ClosePricePK int
                                        Declare @InstrumentPK int
                                        Declare @ClosePriceValue numeric(18,8)

                                        --select @Date = Date from FxdTemp

                                        --select @Date

                                        Declare A Cursor For
                                        select B.InstrumentPK,A.Amount from ClosePriceReksadanaTemp A
                                        Left Join Instrument B on A.InstrumentName = B.Name 
                                        where A.InstrumentName in(Select Name From Instrument where B.status in(1,2))


                                        Open A
                                        Fetch next From A
                                        into @InstrumentPK,@ClosePriceValue
                                        While @@Fetch_status = 0
                                        BEGIN

                                        Select @ClosePricePK = max(ClosePricePK) + 1 from ClosePrice
                                        set @ClosePricePK = isnull(@ClosePricePK,1)
								
                                        insert into 
                                        [dbo].ClosePrice
                                        ([ClosePricePK]
                                        ,[HistoryPK]
                                        ,[Status]
                                        ,[Date]
                                        ,[InstrumentPK]
                                        ,[ClosePriceValue]
                                        ,[EntryUsersID]
                                        ,[EntryTime]
                                        ,[LastUpdate]
                                        )

                                        select @ClosePricePK,1,1,@Date,@InstrumentPK,@ClosePriceValue,@UsersID,@LastUpdate,@LastUpdate
                                

                                        fetch next From A into @InstrumentPK,@ClosePriceValue
                                        end
                                        Close A
                                        Deallocate A ";
                                        //napa yo ada yang perlu ubah lagi ga?
                                        cmd1.Parameters.AddWithValue("@Date", _date);
                                        cmd1.Parameters.AddWithValue("@UsersID", _userID);
                                        cmd1.Parameters.AddWithValue("@LastUpdate", _now);
                                        cmd1.ExecuteNonQuery();

                                    }
                                    _msg = "Import Close Price Reksadana Done";

                                }

                            }
                        }
                    }
                }



                return _msg;
            }
            catch (Exception err)
            {
                throw err;
            }
        }
        
        private DataTable CreateDataTableFromClosePriceReksadanaTempExcelFile(string _path)
        {
            try
            {
                using (DataTable dt = new DataTable())
                {
                    DataColumn dc;
                    DataRow dr;

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Int32");
                    dc.ColumnName = "ClosePriceReksadanaPK";
                    dc.Unique = false;
                    dc.AutoIncrement = true;
                    dc.AutoIncrementSeed = 1;
                    dc.AutoIncrementStep = 1;
                    dt.Columns.Add(dc);


                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "InstrumentName";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Decimal");
                    dc.ColumnName = "ClosePriceValue";
                    dc.Unique = false;
                    dt.Columns.Add(dc);




                    using (OleDbConnection odConnection = new OleDbConnection(Tools.ConStringExcel2007(_path)))
                    {
                        odConnection.Open();
                        using (OleDbCommand odCmd = odConnection.CreateCommand())
                        {
                            if (Tools.ClientCode == "11")
                            {
                                // _oldfilename = nama sheet yang ada di file excel yang diimport
                                odCmd.CommandText = "SELECT * FROM [sheet0$]";
                            }
                            else
                            {
                                // _oldfilename = nama sheet yang ada di file excel yang diimport
                                odCmd.CommandText = "SELECT * FROM [new sheet$]";
                            }

                            using (OleDbDataReader odRdr = odCmd.ExecuteReader())
                            {
                                // start counting from index = 1 --> skipping the header (index=0)
                                for (int i = 1; i <= 1; i++)
                                {
                                    odRdr.Read();
                                }
                                do
                                {
                                    dr = dt.NewRow();

                                    //dr["Date"] = odRdr[0];
                                    dr["InstrumentName"] = odRdr[0];
                                    dr["ClosePriceValue"] = odRdr[4];


                                    if (dr["ClosePriceReksadanaPK"].Equals(DBNull.Value) != true) { dt.Rows.Add(dr); }
                                } while (odRdr.Read());
                            }
                        }
                        odConnection.Close();
                    }



                    return dt;
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public string ImportIBPA_Text(string _fileSource, string _userID)
        {
            string _msg;
            DateTime _dateTime = DateTime.Now;
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        //delete data yang lama
                        using (SqlConnection conn = new SqlConnection(Tools.conString))
                        {
                            conn.Open();
                            using (SqlCommand cmd1 = conn.CreateCommand())
                            {
                                cmd1.CommandText = "truncate table ClosePriceIBPATemp";
                                cmd1.ExecuteNonQuery();
                            }
                        }

                        // import data ke temp dulu
                        using (SqlBulkCopy bulkCopy = new SqlBulkCopy(Tools.conString, SqlBulkCopyOptions.TableLock))
                        {
                            bulkCopy.DestinationTableName = "dbo.ClosePriceIBPATemp";
                            bulkCopy.WriteToServer(CreateDataTableFromIBPATextFile(_fileSource));
                        }

                        using (SqlConnection conn = new SqlConnection(Tools.conString))
                        {
                            conn.Open();
                            using (SqlCommand cmd1 = conn.CreateCommand())
                            {
                                cmd1.CommandText =
                              @"
                          


                                If Exists(Select * from ClosePriceIBPATemp where (FairPrice < LowPrice) or (FairPrice > HighPrice))

                                BEGIN
                                    select 'Fair Price' Result
                                END
                                ELSE
                                BEGIN
                                    select 'Done' Result 
                                END
                            
                                ";
                                using (SqlDataReader dr = cmd1.ExecuteReader())
                                {
                                    dr.Read();
                                    if (Convert.ToString(dr["Result"]) == "Fair Price")
                                    {
                                        _msg = Convert.ToString(dr["Result"]);

                                    }
                                    else
                                    {

                                        conn.Close();
                                        conn.Open();
                                        using (SqlCommand cmd2 = conn.CreateCommand())
                                        {
                                            cmd2.CommandText =
                                            @"
                                            declare @Date Datetime
                                            select @Date = Date From ClosePriceIBPATemp

                                            If Exists(select distinct A.InstrumentPK from closeprice A 
                                            left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2
                                            where B.ID in (select Series from ClosePriceIBPATemp)
                                            and A.Status in (1,2) and Date = @Date)

                                            BEGIN
                                                select 'Already Data' Result
                                            END
                                            ELSE
                                            BEGIN
                                                select 'Done' Result 
                                            END

                            
                                            ";
                                            using (SqlDataReader dr1 = cmd2.ExecuteReader())
                                            {
                                                dr1.Read();
                                                if (Convert.ToString(dr1["Result"]) == "Already Data")
                                                {
                                                    _msg = Convert.ToString(dr1["Result"]);

                                                }
                                                else
                                                {


                                                    conn.Close();
                                                    conn.Open();


                                                    using (SqlCommand cmd3 = conn.CreateCommand())
                                                    {
                                                        cmd3.CommandText =
                                                        @"
                                                                    
                                            declare @Date Datetime
                                            select @Date = Date From ClosePriceIBPATemp

                                            Update A set status = 3 from closeprice A 
                                            left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2
                                            where B.ID in (select Series from ClosePriceIBPATemp)
                                            and A.Status in (1,2) and Date = @Date

                                            Declare @ClosePricePK int
                                                Select @ClosePricePK = max(ClosePricePK) + 1 from ClosePrice
                                                set @ClosePricePK = isnull(@ClosePricePK,0)
                                                INSERT INTO [dbo].[ClosePrice]
                                                            ([ClosePricePK]
                                                            ,[HistoryPK]
                                                            ,[Status]
                                                            ,[Notes]
                                                            ,[Date]
                                                            ,[InstrumentPK]
                                                            ,[ClosePriceValue]
                                                            ,[LowPriceValue]
                                                            ,[HighPriceValue]      
                                                            ,[EntryUsersID]
                                                            ,[EntryTime]
                                                            ,[LastUpdate]
                                                )
                                                Select @ClosePricePK +  ROW_NUMBER() OVER(Order By A.Series ASC) ,1,1,'',A.Date,isnull(B.InstrumentPK,0)
                                                ,A.FairPrice,A.LowPrice,A.HighPrice
                                                ,@userID,@Datetime,@Datetime
                                                from ClosePriceIBPATemp  A
                                                Left join Instrument B on A.Series = B.ID and B.status = 2

                                                delete ClosePrice where InstrumentPK = 0

	                                            select 'Import IBPA Success' Result

                                            ";
                                                        cmd3.Parameters.AddWithValue("@UserID", _userID);
                                                        cmd3.Parameters.AddWithValue("@Datetime", _dateTime);
                                                        cmd3.ExecuteNonQuery();

                                                        _msg = "Import IBPA Done";

                                                    }

                                                }
                                            }
                                        }
                                    }

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

        private DataTable CreateDataTableFromIBPATextFile(string _fileName)
        {
            DataTable dt = new DataTable();
            DataColumn dc;
            DataRow dr;

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
            dc.DataType = System.Type.GetType("System.Decimal");
            dc.ColumnName = "LowPrice";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.Decimal");
            dc.ColumnName = "FairPrice";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.Decimal");
            dc.ColumnName = "HighPrice";
            dc.Unique = false;
            dt.Columns.Add(dc);


            StreamReader sr = new StreamReader(Tools.TxtFilePath + _fileName);
            string input;

            sr.ReadLine();
            while ((input = sr.ReadLine()) != null)
            {
                string[] s = input.Split(new char[] { '|' });


                dr = dt.NewRow();
                dr["Date"] = s[0];
                dr["Series"] = s[1];
                dr["LowPrice"] = s[8];
                dr["FairPrice"] = s[9];
                dr["HighPrice"] = s[10];

                dt.Rows.Add(dr);
            }
            sr.Close();
            return dt;
        }

        public bool Validate_GetClosePrice(DateTime _valueDate, ClosePrice _closePricee)
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

                        if Exists(select * From ClosePrice 
                        where Status in (1,2) and Date = @ValueDate and InstrumentPK = @InstrumentPK 
                        ) BEGIN Select 1 Result END ELSE BEGIN Select 0 Result END

                        ";



                        cmd.Parameters.AddWithValue("@ValueDate", _valueDate);
                        cmd.Parameters.AddWithValue("@InstrumentPK", _closePricee.InstrumentPK);
                        //cmd.Parameters.AddWithValue("@ClientCode", Tools.ClientCode);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return Convert.ToBoolean(dr["Result"]);

                            }
                            return false;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public string ImportClosePriceEquityFromExcelFile(string _fileSource, string _userID, string _date)
        {
            string _msg = "";
            try
            {
                DateTime _now = DateTime.Now;

                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        //Validasi
                        cmd.CommandText = "select * from ClosePrice CP left join Instrument I on CP.InstrumentPK = I.InstrumentPK where I.status = 2 and Date = @Date and I.InstrumentTypePK in (1,4,16) and CP.status in (1,2) ";
                        cmd.Parameters.AddWithValue("@Date", _date);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                return "Import Cancel, Already Import Close Price Equity For this Day, Void / Reject First!";
                            }
                            else
                            {
                                //delete data yang lama
                                using (SqlConnection conn = new SqlConnection(Tools.conString))
                                {
                                    conn.Open();
                                    using (SqlCommand cmd1 = conn.CreateCommand())
                                    {
                                        cmd1.CommandText = "truncate table ClosePriceEquityTemp";
                                        cmd1.ExecuteNonQuery();
                                    }
                                }

                                // import data ke temp dulu
                                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(Tools.conString, SqlBulkCopyOptions.TableLock))
                                {
                                    bulkCopy.DestinationTableName = "dbo.ClosePriceEquityTemp";
                                    bulkCopy.WriteToServer(CreateDataTableFromClosePriceEquityTempExcelFile(_fileSource, _date));
                                    _msg = "Import Close Price Equity Success";
                                }

                                // logic kalo Reconcile success
                                using (SqlConnection conn = new SqlConnection(Tools.conString))
                                {
                                    conn.Open();
                                    using (SqlCommand cmd1 = conn.CreateCommand())
                                    {
                                        cmd1.CommandText =

                                        @"Declare @ClosePricePK int
                                    Declare @InstrumentPK int
                                    Declare @ClosePriceValue numeric(18,8)

                                    --select @Date = Date from FxdTemp

                                    --select @Date

                                    Declare A Cursor For
                                    select B.InstrumentPK,A.Amount from ClosePriceEquityTemp A
                                    Left Join Instrument B on A.InstrumentName = B.ID 
                                    where A.InstrumentName in (Select ID From Instrument where status in(1,2))  and InstrumentTypePK in (1,4,16)


                                    Open A
                                    Fetch next From A
                                    into @InstrumentPK,@ClosePriceValue
                                    While @@Fetch_status = 0
                                    BEGIN

                                    Select @ClosePricePK = max(ClosePricePK) + 1 from ClosePrice
                                    set @ClosePricePK = isnull(@ClosePricePK,1)
								
                                    insert into 
                                    [dbo].ClosePrice
                                    ([ClosePricePK]
                                    ,[HistoryPK]
                                    ,[Status]
                                    ,[Date]
                                    ,[InstrumentPK]
                                    ,[ClosePriceValue]
                                    ,[EntryUsersID]
                                    ,[EntryTime]
                                    ,[LastUpdate]
                                    )

                                    select @ClosePricePK,1,1,@Date,@InstrumentPK,@ClosePriceValue,@UsersID,@LastUpdate,@LastUpdate
                                

                                    fetch next From A into @InstrumentPK,@ClosePriceValue
                                    end
                                    Close A
                                    Deallocate A  ";

                                        cmd1.Parameters.AddWithValue("@Date", _date);
                                        cmd1.Parameters.AddWithValue("@UsersID", _userID);
                                        cmd1.Parameters.AddWithValue("@LastUpdate", _now);
                                        cmd1.ExecuteNonQuery();

                                    }
                                    _msg = "Import Close Price Equity Done";

                                }


                            }
                        }
                    }
                }



                return _msg;
            }
            catch (Exception err)
            {
                throw err;
            }
        }



        private DataTable CreateDataTableFromClosePriceEquityTempExcelFile(string _path, string _date)
        {
            try
            {
                using (DataTable dt = new DataTable())
                {
                    DataColumn dc;
                    DataRow dr;

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Int32");
                    dc.ColumnName = "ClosePriceEquityPK";
                    dc.Unique = false;
                    dc.AutoIncrement = true;
                    dc.AutoIncrementSeed = 1;
                    dc.AutoIncrementStep = 1;
                    dt.Columns.Add(dc);


                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "InstrumentName";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Decimal");
                    dc.ColumnName = "Amount";
                    dc.Unique = false;
                    dt.Columns.Add(dc);




                    using (OleDbConnection odConnection1 = new OleDbConnection(Tools.ConStringExcel2007(_path)))
                    {
                        odConnection1.Open();
                        using (OleDbCommand odCmd = odConnection1.CreateCommand())
                        {
                            // _oldfilename = nama sheet yang ada di file excel yang diimport
                            odCmd.CommandText = "SELECT * FROM [Sheet1$]";

                            using (OleDbDataReader odRdr = odCmd.ExecuteReader())
                            {
                                // start counting from index = 1 --> skipping the header (index=0)
                                for (int i = 1; i <= 1; i++)
                                {
                                    odRdr.Read();
                                }
                                do
                                {
                                    dr = dt.NewRow();

                                    //dr["Date"] = odRdr[0];
                                    dr["InstrumentName"] = odRdr[0];
                                    dr["Amount"] = odRdr[1];


                                    if (dr["ClosePriceEquityPK"].Equals(DBNull.Value) != true) { dt.Rows.Add(dr); }
                                } while (odRdr.Read());
                            }
                        }
                        odConnection1.Close();
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