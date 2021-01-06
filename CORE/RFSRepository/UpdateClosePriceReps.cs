using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RFSModel;
using RFSUtility;
using System.Data.SqlClient;
using System.Drawing;
using OfficeOpenXml.Drawing;
using System.Data.OleDb;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.IO;
using System.Data.Odbc;
using System.Data;

namespace RFSRepository
{
    public class UpdateClosePriceReps
    {

        Host _host = new Host();

        //1
        string _insertCommand = "INSERT INTO [dbo].[UpdateClosePrice] " +
                            "([UpdateClosePricePK],[HistoryPK],[Status],[Date],[FundPK],[InstrumentPK],[ClosePriceValue],";
        string _paramaterCommand = "@Date,@FundPK,@InstrumentPK,@ClosePriceValue,";

        //2

        private UpdateClosePrice setUpdateClosePrice(SqlDataReader dr)
        {
            UpdateClosePrice M_updateClosePrice = new UpdateClosePrice();
            M_updateClosePrice.UpdateClosePricePK = Convert.ToInt32(dr["UpdateClosePricePK"]);
            M_updateClosePrice.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_updateClosePrice.Status = Convert.ToInt32(dr["Status"]);
            M_updateClosePrice.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_updateClosePrice.Notes = Convert.ToString(dr["Notes"]);
            M_updateClosePrice.Date = Convert.ToString(dr["Date"]);
            M_updateClosePrice.FundPK = Convert.ToInt32(dr["FundPK"]);
            M_updateClosePrice.FundID = Convert.ToString(dr["FundID"]);
            M_updateClosePrice.InstrumentPK = Convert.ToInt32(dr["InstrumentPK"]);
            M_updateClosePrice.InstrumentID = Convert.ToString(dr["InstrumentID"]);
            M_updateClosePrice.ClosePriceValue = Convert.ToDecimal(dr["ClosePriceValue"]);
            M_updateClosePrice.EntryUsersID = dr["EntryUsersID"].ToString();
            M_updateClosePrice.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_updateClosePrice.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_updateClosePrice.VoidUsersID = dr["VoidUsersID"].ToString();
            M_updateClosePrice.EntryTime = dr["EntryTime"].ToString();
            M_updateClosePrice.UpdateTime = dr["UpdateTime"].ToString();
            M_updateClosePrice.ApprovedTime = dr["ApprovedTime"].ToString();
            M_updateClosePrice.VoidTime = dr["VoidTime"].ToString();
            M_updateClosePrice.DBUserID = dr["DBUserID"].ToString();
            M_updateClosePrice.DBTerminalID = dr["DBTerminalID"].ToString();
            M_updateClosePrice.LastUpdate = dr["LastUpdate"].ToString();
            M_updateClosePrice.LastUpdateDB = Convert.ToString(dr["LastUpdateDB"]);
            return M_updateClosePrice;
        }

        public List<UpdateClosePrice> UpdateClosePrice_SelectDateFromTo(int _status, DateTime _dateFrom, DateTime _dateTo, string _fundPK)
        {
            string _fundFrom;
            if(_fundPK == "0")
            {
                _fundFrom = "";
            }
            else
            {
                _fundFrom = " and UC.FundPK = " + _fundPK;
            }
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<UpdateClosePrice> L_updateClosePrice = new List<UpdateClosePrice>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = @"Select case when UC.status=1 then 'PENDING' else Case When UC.status = 2 then 'APPROVED' else Case when UC.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,I.ID InstrumentID,I.Name InstrumentName,F.ID FundID,F.Name FundName,UC.* from UpdateClosePrice UC 
                            left join Instrument I on UC.InstrumentPK = I.InstrumentPK and I.status = 2
                            left join Fund F on UC.FundPK = F.FundPK and F.status = 2
                            where UC.Status = @status and Date between @DateFrom and @DateTo " + _fundFrom;
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = @"Select case when UC.status=1 then 'PENDING' else Case When UC.status = 2 then 'APPROVED' else Case when UC.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,I.ID InstrumentID,I.Name InstrumentName,F.ID FundID,F.Name FundName,UC.* from UpdateClosePrice UC 
                            left join Instrument I on UC.InstrumentPK = I.InstrumentPK and I.status = 2
                            left join Fund F on UC.FundPK = F.FundPK and F.status = 2  where Date between @DateFrom and @DateTo " + _fundFrom;
                        }
                        cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _dateTo);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_updateClosePrice.Add(setUpdateClosePrice(dr));
                                }
                            }
                            return L_updateClosePrice;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int UpdateClosePrice_Add(UpdateClosePrice _updateClosePrice, bool _havePrivillege)
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
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[ApprovedUsersID],[ApprovedTime],LastUpdate)" +
                                 "Select isnull(max(UpdateClosePricePk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@LastUpdate from UpdateClosePrice";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _updateClosePrice.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],LastUpdate)" +
                                "Select isnull(max(UpdateClosePricePk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@LastUpdate from UpdateClosePrice";
                        }
                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@Date", _updateClosePrice.Date);
                        cmd.Parameters.AddWithValue("@FundPK", _updateClosePrice.FundPK);
                        cmd.Parameters.AddWithValue("@InstrumentPK", _updateClosePrice.InstrumentPK);
                        cmd.Parameters.AddWithValue("@ClosePriceValue", _updateClosePrice.ClosePriceValue);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _updateClosePrice.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);

                        cmd.ExecuteNonQuery();

                        return _host.Get_LastPKByLastUpate(_datetimeNow, "UpdateClosePrice");
                    }

                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int UpdateClosePrice_Update(UpdateClosePrice _updateClosePrice, bool _havePrivillege)
        {

            try
            {
                int _newHisPK;
                int status = _host.Get_Status(_updateClosePrice.UpdateClosePricePK, _updateClosePrice.HistoryPK, "UpdateClosePrice");
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update UpdateClosePrice set status=2, Notes=@Notes,Date=@Date,FundPK=@FundPK,InstrumentPK=@InstrumentPK,ClosePriceValue=@ClosePriceValue," +
                                "ApprovedUsersID=@ApprovedUsersID, " +
                                "ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@lastupdate " +
                                "where UpdateClosePricePK = @PK and historyPK = @HistoryPK";
                            cmd.Parameters.AddWithValue("@HistoryPK", _updateClosePrice.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _updateClosePrice.UpdateClosePricePK);
                            cmd.Parameters.AddWithValue("@Notes", _updateClosePrice.Notes);
                            cmd.Parameters.AddWithValue("@Date", _updateClosePrice.Date);
                            cmd.Parameters.AddWithValue("@FundPK", _updateClosePrice.FundPK);
                            cmd.Parameters.AddWithValue("@InstrumentPK", _updateClosePrice.InstrumentPK);
                            cmd.Parameters.AddWithValue("@ClosePriceValue", _updateClosePrice.ClosePriceValue);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _updateClosePrice.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _updateClosePrice.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);

                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update UpdateClosePrice set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where UpdateClosePricePK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _updateClosePrice.UpdateClosePricePK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _updateClosePrice.EntryUsersID);
                            cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
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
                                cmd.CommandText = "Update UpdateClosePrice set Notes=@Notes,Date=@Date,FundPK=@FundPK,InstrumentPK=@InstrumentPK,ClosePriceValue=@ClosePriceValue," +
                                "UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@lastupdate " +
                                "where UpdateClosePricePK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _updateClosePrice.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _updateClosePrice.UpdateClosePricePK);
                                cmd.Parameters.AddWithValue("@Notes", _updateClosePrice.Notes);
                                cmd.Parameters.AddWithValue("@Date", _updateClosePrice.Date);
                                cmd.Parameters.AddWithValue("@FundPK", _updateClosePrice.FundPK);
                                cmd.Parameters.AddWithValue("@InstrumentPK", _updateClosePrice.InstrumentPK);
                                cmd.Parameters.AddWithValue("@ClosePriceValue", _updateClosePrice.ClosePriceValue);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _updateClosePrice.EntryUsersID);
                                cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);

                                cmd.ExecuteNonQuery();
                            }
                            return 0;
                        }
                        else
                        {
                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                _newHisPK = _host.Get_NewHistoryPK(_updateClosePrice.UpdateClosePricePK, "UpdateClosePrice");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From UpdateClosePrice where UpdateClosePricePK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _updateClosePrice.UpdateClosePricePK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _updateClosePrice.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@Date", _updateClosePrice.Date);
                                cmd.Parameters.AddWithValue("@FundPK", _updateClosePrice.FundPK);
                                cmd.Parameters.AddWithValue("@InstrumentPK", _updateClosePrice.InstrumentPK);
                                cmd.Parameters.AddWithValue("@ClosePriceValue", _updateClosePrice.ClosePriceValue);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _updateClosePrice.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update UpdateClosePrice set status= 4,Notes=@Notes," +
                                " LastUpdate=@lastupdate " +
                                " where UpdateClosePricePK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _updateClosePrice.Notes);
                                cmd.Parameters.AddWithValue("@PK", _updateClosePrice.UpdateClosePricePK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _updateClosePrice.HistoryPK);
                                cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
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

        public void UpdateClosePrice_Approved(UpdateClosePrice _updateClosePrice)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update UpdateClosePrice set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,LastUpdate=@lastupdate " +
                            "where UpdateClosePricePK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _updateClosePrice.UpdateClosePricePK);
                        cmd.Parameters.AddWithValue("@historyPK", _updateClosePrice.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _updateClosePrice.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update UpdateClosePrice set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where UpdateClosePricePK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _updateClosePrice.UpdateClosePricePK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _updateClosePrice.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public void UpdateClosePrice_Reject(UpdateClosePrice _updateClosePrice)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update UpdateClosePrice set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where UpdateClosePricePK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _updateClosePrice.UpdateClosePricePK);
                        cmd.Parameters.AddWithValue("@historyPK", _updateClosePrice.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _updateClosePrice.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update UpdateClosePrice set status= 2,LastUpdate=@LastUpdate where UpdateClosePricePK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _updateClosePrice.UpdateClosePricePK);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public void UpdateClosePrice_Void(UpdateClosePrice _updateClosePrice)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update UpdateClosePrice set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where UpdateClosePricePK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _updateClosePrice.UpdateClosePricePK);
                        cmd.Parameters.AddWithValue("@historyPK", _updateClosePrice.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _updateClosePrice.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }


        public void UpdateClosePrice_ApproveBySelected(string _usersID, string _permissionID, DateTime _dateFrom, DateTime _dateTo, UpdateClosePrice _updateClosePrice)
        {
            try
            {
                string paramUpdateClosePriceSelected = "";
                paramUpdateClosePriceSelected = " and UpdateClosePricePK in (" + _updateClosePrice.UpdateClosePriceSelected + ") ";

                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @" Declare @IPAddress nvarchar(50) select @IPAddress = IPAddress from Users where ID = @UsersID and Status = 2 
                                 Insert into Activity(Time,PermissionID,ObjectTable,ObjectTablePK,Status,Message,UsersID,IPAddress,LastUpdate) 
                                 Select @Time,@PermissionID,'UpdateClosePrice',UpdateClosePricePK,1,'Approve by Selected Data',@UsersID,@IPAddress,@Time  from UpdateClosePrice where Date between @DateFrom and @DateTo and Status = 1 " + paramUpdateClosePriceSelected + @"
                                 update UpdateClosePrice set status = 2,ApprovedUsersID = @UsersID,ApprovedTime = @Time,LastUpdate=@Time where status = 1  " + paramUpdateClosePriceSelected + @"
                                 Update UpdateClosePrice set status= 3,VoidUsersID=@UsersID,VoidTime=@Time,LastUpdate=@Time  where status = 4  " + paramUpdateClosePriceSelected;

                        cmd.Parameters.AddWithValue("@PermissionID", _permissionID);
                        cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _dateTo);
                        cmd.Parameters.AddWithValue("@UsersID", _usersID);
                        cmd.Parameters.AddWithValue("@Time", _datetimeNow);
                        cmd.CommandTimeout = 0;
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public void UpdateClosePrice_RejectBySelected(string _usersID, string _permissionID, DateTime _dateFrom, DateTime _dateTo, UpdateClosePrice _updateClosePrice)
        {
            try
            {
                string paramUpdateClosePriceSelected = "";
                paramUpdateClosePriceSelected = " and UpdateClosePricePK in (" + _updateClosePrice.UpdateClosePriceSelected + ") ";

                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"Declare @IPAddress nvarchar(50) select @IPAddress = IPAddress from Users where ID = @UsersID and Status = 2 
                                          Insert into Activity(Time,PermissionID,ObjectTable,ObjectTablePK,Status,Message,UsersID,IPAddress,LastUpdate) 
                                          Select @Time,@PermissionID,'UpdateClosePrice',UpdateClosePricePK,1,'Reject by Selected Data',@UsersID,@IPAddress,@Time  from UpdateClosePrice where Date between @DateFrom and @DateTo and Status = 1 " + paramUpdateClosePriceSelected + @"
                                          update UpdateClosePrice set status = 3,selected = 0,VoidUsersID = @UsersID,VoidTime = @Time,LastUpdate=@Time where status = 1 " + paramUpdateClosePriceSelected + @"
                                          Update UpdateClosePrice set status= 2  where status = 4 " + paramUpdateClosePriceSelected;

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

        public void UpdateClosePrice_VoidBySelectedData(string _usersID, string _permissionID, DateTime _dateFrom, DateTime _dateTo, UpdateClosePrice _updateClosePrice)
        {
            try
            {
                string paramUpdateClosePriceSelected = "";
                paramUpdateClosePriceSelected = " and UpdateClosePricePK in (" + _updateClosePrice.UpdateClosePriceSelected + ") ";

                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"Declare @IPAddress nvarchar(50) select @IPAddress = IPAddress from Users where ID = @UsersID and Status = 2 
                                          Insert into Activity(Time,PermissionID,ObjectTable,ObjectTablePK,Status,Message,UsersID,IPAddress,LastUpdate) 
                                          Select @Time,@PermissionID,'UpdateClosePrice',UpdateClosePricePK,1,'Void by Selected Data',@UsersID,@IPAddress,@Time  from UpdateClosePrice where Date between @DateFrom and @DateTo and Status = 2 " + paramUpdateClosePriceSelected + @"
                                          update UpdateClosePrice set status = 3,selected = 0,VoidUsersID = @UsersID,VoidTime = @Time,LastUpdate=@Time where status = 2 " + paramUpdateClosePriceSelected;

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


        private DataTable CreateDataTableFromInternalClosePriceExcelFile(string _fileSource)
        {
            try
            {
                using (DataTable dt = new DataTable())
                {
                    DataColumn dc;
                    DataRow dr;

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "FundID";
                    dc.Unique = false;
                    dt.Columns.Add(dc);


                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "InstrumentID";
                    dc.Unique = false;
                    dt.Columns.Add(dc);


                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Price";
                    dc.Unique = false;
                    dt.Columns.Add(dc);


                    using (OleDbConnection odConnection = new OleDbConnection(Tools.ConStringExcel2007(_fileSource)))
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

                                    dr["FundID"] = odRdr[0];
                                    dr["InstrumentID"] = odRdr[1];
                                    dr["Price"] = odRdr[2];


                                    if (dr["FundID"].Equals(DBNull.Value) != true) { dt.Rows.Add(dr); }
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
        public string InternalClosePriceImport(string _fileSource, string _userID)
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
                                cmd1.CommandText = "truncate table TempInternalClosePrice";
                                cmd1.ExecuteNonQuery();
                            }
                        }

                        // import data ke temp dulu
                        using (SqlBulkCopy bulkCopy = new SqlBulkCopy(Tools.conString, SqlBulkCopyOptions.TableLock))
                        {
                            bulkCopy.DestinationTableName = "dbo.TempInternalClosePrice";
                            bulkCopy.WriteToServer(CreateDataTableFromInternalClosePriceExcelFile(_fileSource));
                            _msg = "Import InternalClosePrice Success";
                        }

                        using (SqlConnection conn = new SqlConnection(Tools.conString))
                        {
                            conn.Open();
                            string Total;
                            using (SqlCommand cmd1 = conn.CreateCommand())
                            {
                                cmd1.CommandText =
                              @"
                                SELECT InstrumentID,FundID, COUNT(*), case when COUNT(*) > 1 then 'true' else 'false' end Total
                                FROM TempInternalClosePrice 
                                GROUP BY InstrumentID,FundID
                                HAVING ( COUNT(FundID) > 0 )";
                                using (SqlDataReader dr = cmd1.ExecuteReader())
                                {
                                    dr.Read();
                                    Total = Convert.ToString(dr["Total"]);
                                    if (Total == "true")
                                    {
                                        _msg = "More Than 1 Data in 1 Fund";

                                    }
                                    else
                                    {
                                        conn.Close();
                                        conn.Open();
                                        using (SqlCommand cmd2 = conn.CreateCommand())
                                        {
                                            cmd2.CommandText =
                                          @"
                                            declare @PK int
                                            select @PK = Max(UpdateClosePricePK) from UpdateClosePrice
								            set @PK = isnull(@PK,0)
								            insert into UpdateClosePrice ([UpdateClosePricePK],[HistoryPK],[Status],[Date],[FundPK],[InstrumentPK],[ClosePriceValue])
                                            SELECT @PK + ROW_NUMBER() OVER(ORDER BY FundID ASC) ,1,1,'', B.FundPK,  C.InstrumentPK, A.Price from TempInternalClosePrice A left join fund B
                                            on A.FundID = B.ID and B.Status in(1,2) left join Instrument C
                                            on A.InstrumentID = C.ID and C.Status in(1,2)";
                                            cmd2.ExecuteNonQuery();

                                        }
                                        _msg = "Import Internal Close Price Done";
                                    }
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

        public void ImportInternalClosePrice(UpdateClosePrice _UpdateClosePrice)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = @"
                            declare @FundPK int
                            declare @InstrumentPK int
                            select @FundPK = B.FundPK from TempInternalClosePrice A left join Fund B on A.FundID = B.ID and B.status in(1,2)
                            select @InstrumentPK = B.InstrumentPK from TempInternalClosePrice A left join Instrument B on A.InstrumentID = B.ID and B.status in(1,2)
                            update UpdateClosePrice set Date = @Date
							 ,EntryUsersID = @UsersID, EntryTime = @EntryTime, LastUpdate = @EntryTime 
							 where 
							 Date = '01/01/1900' and 
							 status = 1";
                            cmd.Parameters.AddWithValue("@Date", _UpdateClosePrice.DateImport);
                            cmd.Parameters.AddWithValue("@UsersID", _UpdateClosePrice.EntryUsersID);
                            cmd.Parameters.AddWithValue("@EntryTime", _datetimeNow);
                            cmd.ExecuteNonQuery();
                        }

                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public bool CheckHasAdd(string _date)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    string date = System.DateTime.Today.ToString("MM/dd/yyyy");
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        //cmd.CommandText = @"declare @FundPK int
                        //select @FundPK = FundPK from TempInternalClosePrice A left join Fund B on A.FundID = B.ID and B.Status in(1,2)

                        //select * from UpdateClosePrice where Date = @Date and FundPK in () and status in (1,2)";

                        cmd.CommandText = @"
                        select * from UpdateClosePrice where Date = @Date and FundPK in (select distinct FundPK from TempInternalClosePrice A left join Fund B on A.FundID = B.ID and B.Status in(1,2)) 
                        and status in (1,2)";
                        cmd.Parameters.AddWithValue("@Date", _date);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (!dr.HasRows)
                            {
                                return false;
                            }
                            else
                            {
                                return true;
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

        public bool Validate_GetUpdateClosePrice(DateTime _valueDate, UpdateClosePrice _updateClosePrice)
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

                        if Exists(select * From UpdateClosePrice 
                        where Status in (1,2) and Date = @ValueDate and FundPK = @FundPK and InstrumentPK = @InstrumentPK 
                        ) BEGIN Select 1 Result END ELSE BEGIN Select 0 Result END

                        ";



                        cmd.Parameters.AddWithValue("@ValueDate", _valueDate);
                        cmd.Parameters.AddWithValue("@FundPK", _updateClosePrice.FundPK);
                        cmd.Parameters.AddWithValue("@InstrumentPK", _updateClosePrice.InstrumentPK);
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



    }
}