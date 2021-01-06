using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using RFSModel;
using RFSUtility;
using System.IO;
using System.Text;
using System.Threading;
using System.Data;
using System.Data.SqlClient;
using System.Data.Odbc;
using System.Data.OleDb;

namespace RFSRepository
{
    public class BenchmarkIndexReps
    {
        Host _host = new Host();
        //1
        string _insertCommand = "INSERT INTO [dbo].[BenchmarkIndex] " +
                            "([BenchmarkIndexPK],[HistoryPK],[Status],[Date],[IndexPK],[OpenInd],[HighInd],[LowInd],[CloseInd],[Volume],[Value],[Approved1],";
        string _paramaterCommand = "@Date,@IndexPK,@OpenInd,@HighInd,@LowInd,@CloseInd,@Volume,@Value,@Approved1,";

        //2
        private BenchmarkIndex setBenchmarkIndex(SqlDataReader dr)
        {
            BenchmarkIndex M_BenchmarkIndex = new BenchmarkIndex();
            M_BenchmarkIndex.BenchmarkIndexPK = Convert.ToInt32(dr["BenchmarkIndexPK"]);
            M_BenchmarkIndex.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_BenchmarkIndex.Status = Convert.ToInt32(dr["Status"]);
            M_BenchmarkIndex.Selected = dr["Selected"].Equals(DBNull.Value) == true ? false : Convert.ToBoolean(dr["Selected"]);
            M_BenchmarkIndex.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_BenchmarkIndex.Notes = Convert.ToString(dr["Notes"]);
            M_BenchmarkIndex.Date = Convert.ToString(dr["Date"]);
            M_BenchmarkIndex.IndexPK = Convert.ToInt32(dr["IndexPK"]);
            M_BenchmarkIndex.IndexID = Convert.ToString(dr["IndexID"]);
            M_BenchmarkIndex.OpenInd = Convert.ToDecimal(dr["OpenInd"]);
            M_BenchmarkIndex.HighInd = Convert.ToDecimal(dr["HighInd"]);
            M_BenchmarkIndex.LowInd = Convert.ToDecimal(dr["LowInd"]);
            M_BenchmarkIndex.CloseInd = Convert.ToDecimal(dr["CloseInd"]);
            M_BenchmarkIndex.Volume = Convert.ToDecimal(dr["Volume"]);
            M_BenchmarkIndex.Value = Convert.ToDecimal(dr["Value"]);
            M_BenchmarkIndex.Approved1 = dr["Approved1"].Equals(DBNull.Value) == true ? false : Convert.ToBoolean(dr["Approved1"]);
            M_BenchmarkIndex.EntryUsersID = dr["EntryUsersID"].ToString();
            M_BenchmarkIndex.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_BenchmarkIndex.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_BenchmarkIndex.VoidUsersID = dr["VoidUsersID"].ToString();
            M_BenchmarkIndex.EntryTime = dr["EntryTime"].ToString();
            M_BenchmarkIndex.UpdateTime = dr["UpdateTime"].ToString();
            M_BenchmarkIndex.ApprovedTime = dr["ApprovedTime"].ToString();
            M_BenchmarkIndex.VoidTime = dr["VoidTime"].ToString();
            M_BenchmarkIndex.DBUserID = dr["DBUserID"].ToString();
            M_BenchmarkIndex.DBTerminalID = dr["DBTerminalID"].ToString();
            M_BenchmarkIndex.LastUpdate = dr["LastUpdate"].ToString();
            M_BenchmarkIndex.LastUpdateDB = Convert.ToString(dr["LastUpdateDB"]);
            return M_BenchmarkIndex;
        }


        public List<BenchmarkIndex> BenchmarkIndex_Select(int _status)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<BenchmarkIndex> L_BenchmarkIndex = new List<BenchmarkIndex>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = @"Select case when GR.status=1 then 'PENDING' else Case When GR.status = 2 then 'APPROVED' 
                            else Case when GR.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc, R.ID IndexID,GR.* from BenchmarkIndex GR left join    
                            [Index] R on GR.IndexPK = R.IndexPK  and R.status = 2    
                            where GR.status = @status  ";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = @"Select case when GR.status=1 then 'PENDING' else Case When GR.status = 2 then 'APPROVED' 
                            else Case when GR.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc, R.ID IndexID,GR.* from BenchmarkIndex GR left join    
                            [Index] R on GR.IndexPK = R.IndexPK  and R.status = 2    ";
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_BenchmarkIndex.Add(setBenchmarkIndex(dr));
                                }
                            }
                            return L_BenchmarkIndex;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int BenchmarkIndex_Add(BenchmarkIndex _BenchmarkIndex, bool _havePrivillege)
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
                                 "Select isnull(max(BenchmarkIndexPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@LastUpdate from BenchmarkIndex";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _BenchmarkIndex.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],LastUpdate)" +
                                "Select isnull(max(BenchmarkIndexPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@LastUpdate from BenchmarkIndex";
                        }
                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@IndexPK", _BenchmarkIndex.IndexPK);
                        cmd.Parameters.AddWithValue("@Date", _BenchmarkIndex.Date);
                        cmd.Parameters.AddWithValue("@OpenInd", _BenchmarkIndex.OpenInd);
                        cmd.Parameters.AddWithValue("@HighInd", _BenchmarkIndex.HighInd);
                        cmd.Parameters.AddWithValue("@LowInd", _BenchmarkIndex.LowInd);
                        cmd.Parameters.AddWithValue("@CloseInd", _BenchmarkIndex.CloseInd);
                        cmd.Parameters.AddWithValue("@Volume", _BenchmarkIndex.Volume);
                        cmd.Parameters.AddWithValue("@Value", _BenchmarkIndex.Value);
                        if (_BenchmarkIndex.Approved1 == false || _BenchmarkIndex.Approved1 == null)
                        {
                            cmd.Parameters.AddWithValue("@Approved1", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@Approved1", _BenchmarkIndex.Approved1);
                        }
                        cmd.Parameters.AddWithValue("@EntryUsersID", _BenchmarkIndex.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);

                        cmd.ExecuteNonQuery();

                        return _host.Get_LastPKByLastUpate(_datetimeNow, "BenchmarkIndex");
                    }
                }

            }
            catch (Exception err)
            {
                throw err;
            }

        }


        public int BenchmarkIndex_Update(BenchmarkIndex _BenchmarkIndex, bool _havePrivillege)
        {

            try
            {
                int _newHisPK;
                int status = _host.Get_Status(_BenchmarkIndex.BenchmarkIndexPK, _BenchmarkIndex.HistoryPK, "BenchmarkIndex"); ;
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update BenchmarkIndex set status=2, Notes=@Notes,IndexPK=@IndexPK,Date = @Date,OpenInd = @OpenInd," +
                                "HighInd = @HighInd,LowInd = @LowInd,CloseInd = @CloseInd,Volume = @Volume,Value = @Value,Approved1=@Approved1,ApprovedUsersID=@ApprovedUsersID, " +
                                "ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@lastupdate " +
                                "where BenchmarkIndexPK = @PK and historyPK = @HistoryPK";
                            cmd.Parameters.AddWithValue("@HistoryPK", _BenchmarkIndex.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _BenchmarkIndex.BenchmarkIndexPK);
                            cmd.Parameters.AddWithValue("@Notes", _BenchmarkIndex.Notes);
                            cmd.Parameters.AddWithValue("@IndexPK", _BenchmarkIndex.IndexPK);
                            cmd.Parameters.AddWithValue("@Date", _BenchmarkIndex.Date);
                            cmd.Parameters.AddWithValue("@OpenInd", _BenchmarkIndex.OpenInd);
                            cmd.Parameters.AddWithValue("@HighInd", _BenchmarkIndex.HighInd);
                            cmd.Parameters.AddWithValue("@LowInd", _BenchmarkIndex.LowInd);
                            cmd.Parameters.AddWithValue("@CloseInd", _BenchmarkIndex.CloseInd);
                            cmd.Parameters.AddWithValue("@Volume", _BenchmarkIndex.Volume);
                            cmd.Parameters.AddWithValue("@Value", _BenchmarkIndex.Value);
                            if (_BenchmarkIndex.Approved1 == null)
                            {
                                cmd.Parameters.AddWithValue("@Approved1", DBNull.Value);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@Approved1", _BenchmarkIndex.Approved1);
                            }

                            cmd.Parameters.AddWithValue("@UpdateUsersID", _BenchmarkIndex.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _BenchmarkIndex.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);

                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update BenchmarkIndex set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where BenchmarkIndexPK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _BenchmarkIndex.BenchmarkIndexPK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _BenchmarkIndex.EntryUsersID);
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
                                cmd.CommandText = "Update BenchmarkIndex set Notes=@Notes,IndexPK=@IndexPK,Date = @Date,OpenInd = @OpenInd," +
                                "HighInd = @HighInd,LowInd = @LowInd,CloseInd = @CloseInd,Volume = @Volume,Value = @Value,Approved1=@Approved1,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime ,LastUpdate=@lastupdate " +
                                "where BenchmarkIndexPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _BenchmarkIndex.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _BenchmarkIndex.BenchmarkIndexPK);
                                cmd.Parameters.AddWithValue("@Notes", _BenchmarkIndex.Notes);
                                cmd.Parameters.AddWithValue("@IndexPK", _BenchmarkIndex.IndexPK);
                                cmd.Parameters.AddWithValue("@Date", _BenchmarkIndex.Date);
                                cmd.Parameters.AddWithValue("@OpenInd", _BenchmarkIndex.OpenInd);
                                cmd.Parameters.AddWithValue("@HighInd", _BenchmarkIndex.HighInd);
                                cmd.Parameters.AddWithValue("@LowInd", _BenchmarkIndex.LowInd);
                                cmd.Parameters.AddWithValue("@CloseInd", _BenchmarkIndex.CloseInd);
                                cmd.Parameters.AddWithValue("@Volume", _BenchmarkIndex.Volume);
                                cmd.Parameters.AddWithValue("@Value", _BenchmarkIndex.Value);
                                if (_BenchmarkIndex.Approved1 == null)
                                {
                                    cmd.Parameters.AddWithValue("@Approved1", DBNull.Value);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@Approved1", _BenchmarkIndex.Approved1);
                                }
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _BenchmarkIndex.EntryUsersID);
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
                                _newHisPK = _host.Get_NewHistoryPK(_BenchmarkIndex.BenchmarkIndexPK, "BenchmarkIndex");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From BenchmarkIndex where BenchmarkIndexPK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _BenchmarkIndex.BenchmarkIndexPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _BenchmarkIndex.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@IndexPK", _BenchmarkIndex.IndexPK);
                                cmd.Parameters.AddWithValue("@Date", _BenchmarkIndex.Date);
                                cmd.Parameters.AddWithValue("@OpenInd", _BenchmarkIndex.OpenInd);
                                cmd.Parameters.AddWithValue("@HighInd", _BenchmarkIndex.HighInd);
                                cmd.Parameters.AddWithValue("@LowInd", _BenchmarkIndex.LowInd);
                                cmd.Parameters.AddWithValue("@CloseInd", _BenchmarkIndex.CloseInd);
                                cmd.Parameters.AddWithValue("@Volume", _BenchmarkIndex.Volume);
                                cmd.Parameters.AddWithValue("@Value", _BenchmarkIndex.Value);
                                if (_BenchmarkIndex.Approved1 == null)
                                {
                                    cmd.Parameters.AddWithValue("@Approved1", DBNull.Value);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@Approved1", _BenchmarkIndex.Approved1);
                                }
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _BenchmarkIndex.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update BenchmarkIndex set status= 4,Notes=@Notes," +
                                " LastUpdate=@lastupdate " +
                                " where BenchmarkIndexPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _BenchmarkIndex.Notes);
                                cmd.Parameters.AddWithValue("@PK", _BenchmarkIndex.BenchmarkIndexPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _BenchmarkIndex.HistoryPK);
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

        public void BenchmarkIndex_Approved(BenchmarkIndex _BenchmarkIndex)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update BenchmarkIndex set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,LastUpdate=@lastupdate " +
                            "where BenchmarkIndexPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _BenchmarkIndex.BenchmarkIndexPK);
                        cmd.Parameters.AddWithValue("@historyPK", _BenchmarkIndex.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _BenchmarkIndex.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update BenchmarkIndex set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where BenchmarkIndexPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _BenchmarkIndex.BenchmarkIndexPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _BenchmarkIndex.ApprovedUsersID);
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

        public void BenchmarkIndex_Reject(BenchmarkIndex _BenchmarkIndex)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update BenchmarkIndex set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where BenchmarkIndexPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _BenchmarkIndex.BenchmarkIndexPK);
                        cmd.Parameters.AddWithValue("@historyPK", _BenchmarkIndex.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _BenchmarkIndex.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update BenchmarkIndex set status= 2,LastUpdate=@LastUpdate  where BenchmarkIndexPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _BenchmarkIndex.BenchmarkIndexPK);
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

        public void BenchmarkIndex_Void(BenchmarkIndex _BenchmarkIndex)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update BenchmarkIndex set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where BenchmarkIndexPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _BenchmarkIndex.BenchmarkIndexPK);
                        cmd.Parameters.AddWithValue("@historyPK", _BenchmarkIndex.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _BenchmarkIndex.VoidUsersID);
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

        public string BenchmarkIndexUploadFromYahoo(string _fileSource, string _userID, string _indexID)
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

                        //delete data yang lama
                        using (SqlConnection conn = new SqlConnection(Tools.conString))
                        {
                            conn.Open();
                            using (SqlCommand cmd2 = conn.CreateCommand())
                            {
                                cmd2.CommandText = "truncate table BenchmarkIndexTemp";
                                cmd2.ExecuteNonQuery();
                            }
                        }

                        // import data ke temp dulu
                        using (SqlBulkCopy bulkCopy = new SqlBulkCopy(Tools.conString, SqlBulkCopyOptions.TableLock))
                        {
                            bulkCopy.DestinationTableName = "dbo.BenchmarkIndexTemp";
                            bulkCopy.WriteToServer(CreateDataTableYahoo(_fileSource, _indexID));

                            _msg = "";
                        }
                        // logic kalo Reconcile success
                        using (SqlConnection conn = new SqlConnection(Tools.conString))
                        {
                            conn.Open();
                            using (SqlCommand cmd2 = conn.CreateCommand())
                            {
                                DateTime _datetimeNow = DateTime.Now;
                                cmd2.CommandText = @"
                                DECLARE @MaxPK INT

                                SELECT @MaxPK = isnull(max(BenchmarkIndexPK),0) FROM dbo.BenchmarkIndex


                                update benchMarkIndex set status = 3
                                where IndexPK in
                                (

                                select B.IndexPK from BenchmarkIndextemp A
                                left join [Index] B on RTRIM(LTRIM(left(A.IndexID, charindex('-', A.IndexID) - 1))) = B.ID and B.status in(1,2)
                                where isnull(B.IndexPK,0) <> 0

                                )
                                and Date in
                                (
                                    Select Date From BenchmarkIndextemp
                                )

                                INSERT INTO dbo.BenchmarkIndex
                                        ( BenchmarkIndexPK ,
                                            HistoryPK ,
                                            Status ,
                                            Notes ,
                                            Date ,
                                            IndexPK ,
                                            OpenInd ,
                                            HighInd ,
                                            LowInd ,
                                            CloseInd ,
                                            Volume ,
                                            Value ,
                                            EntryUsersID ,
                                            EntryTime ,
                                            LastUpdate 
                                        )

                                SELECT @MaxPK + Row_number() over(order by A.Date),1,2,'', 
                                A.Date,B.IndexPK,A.OpenInd,A.HighInd,A.LowInd,A.CloseInd,0,0,@UserID,@Date,@Date
                                FROM dbo.BenchmarkIndextemp A
                                LEFT JOIN [Index] B ON RTRIM(LTRIM(left(A.IndexID, charindex('-', A.IndexID) - 1))) = B.ID and B.status in(1,2)
                                WHERE B.IndexPK IS NOT NULL";
                                cmd2.Parameters.AddWithValue("@Date", _datetimeNow);
                                cmd2.Parameters.AddWithValue("@UserID", _userID);
                                cmd2.ExecuteNonQuery();

                            }
                            _msg = "Import Done";

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


        private DataTable CreateDataTableYahoo(string _path, string _indexID)
        {
            try
            {
                using (DataTable dt = new DataTable())
                {
                    DataColumn dc;
                    DataRow dr;

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Date";
                    dc.Unique = false;
                    dt.Columns.Add(dc);
                    
                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "IndexID";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "OpenInd";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "HighInd";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "LowInd";
                    dc.Unique = false;
                    dt.Columns.Add(dc);


                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "CloseInd";
                    dc.Unique = false;
                    dt.Columns.Add(dc);


                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Volume";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Value";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    string[] allLines = File.ReadAllLines(_path);
                    {
                        for (int i = 1; i < allLines.Length; i++)
                        {
                    
                            string[] items = allLines[i].Split(new char[] { ',' });
                            dr = dt.NewRow();
                            dr["Date"] = items[0];

                            dr["IndexID"] = _indexID;

                            if (items[1] == "null")
                                dr["OpenInd"] = 0;
                            else
                                dr["OpenInd"] = Convert.ToDecimal(items[1]);

                            if (items[2] == "null")
                                dr["HighInd"] = 0;
                            else
                                dr["HighInd"] = Convert.ToDecimal(items[2]);

                            if (items[3] == "null")
                                dr["LowInd"] = 0;
                            else
                                dr["LowInd"] = Convert.ToDecimal(items[3]);

                            if (items[4] == "null")
                                dr["CloseInd"] = 0;
                            else
                                dr["CloseInd"] = Convert.ToDecimal(items[4]);

                            if (items[5] == "null")
                                dr["Volume"] = 0;
                            else
                                dr["Volume"] = Convert.ToDecimal(items[5]);

                            //dr["Volume"] = 0;

                            if (items[6] == "null")
                                dr["Value"] = 0;
                            else
                                dr["Value"] = Convert.ToDecimal(items[6]);
                            //dr["Value"] = 0;
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


        public string BenchmarkIndexUploadFromIDX(string _fileSource, string _userID)
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

                        //delete data yang lama
                        using (SqlConnection conn = new SqlConnection(Tools.conString))
                        {
                            conn.Open();
                            using (SqlCommand cmd2 = conn.CreateCommand())
                            {
                                cmd2.CommandText = "truncate table BenchmarkIndexTemp";
                                cmd2.ExecuteNonQuery();
                            }
                        }

                        // import data ke temp dulu
                        using (SqlBulkCopy bulkCopy = new SqlBulkCopy(Tools.conString, SqlBulkCopyOptions.TableLock))
                        {
                            bulkCopy.DestinationTableName = "dbo.BenchmarkIndexTemp";
                            bulkCopy.WriteToServer(CreateDataTableIDX(_fileSource));

                            _msg = "";
                        }
                        // logic kalo Reconcile success
                        using (SqlConnection conn = new SqlConnection(Tools.conString))
                        {
                            conn.Open();
                            using (SqlCommand cmd2 = conn.CreateCommand())
                            {
                                DateTime _datetimeNow = DateTime.Now;
                                cmd2.CommandText = @"
                            DECLARE @MaxPK INT

                            SELECT @MaxPK = MAX(InstrumentIndexPK) + 1 FROM dbo.InstrumentIndex
                            SET @MaxPK = ISNULL(@MaxPK,1)

                            update benchMarkIndex set status = 3
                            where IndexPK in
                            (

                            select B.IndexPK from BenchmarkIndextemp A
                            left join [Index] B on A.IndexID = B.ID and B.status in(1,2)
                            where isnull(B.IndexPK,0) <> 0

                            )
                            and Date in
                            (
                               Select Date From BenchmarkIndextemp
                            )
    

                            INSERT INTO dbo.BenchmarkIndex
                                    ( BenchmarkIndexPK ,
                                      HistoryPK ,
                                      Status ,
                                      Notes ,
                                      Date ,
                                      IndexPK ,
                                      OpenInd ,
                                      HighInd ,
                                      LowInd ,
                                      CloseInd ,
                                      Volume ,
                                      Value ,
                                      EntryUsersID ,
                                      EntryTime ,
                                      LastUpdate 
                                    )

                            SELECT @MaxPK + Row_number() over(order by A.Date),1,2,'', 
                            A.Date,C.IndexPK,0,0,0,A.CloseInd,A.Volume,A.Value,@UserID,@Date,@Date
                            FROM dbo.BenchmarkIndextemp A
                            INNER JOIN dbo.[Index] C ON A.IndexID = C.ID AND C.status = 2
                            LEFT JOIN BenchmarkIndex B ON C.IndexPK = B.IndexPK AND A.Date = B.Date
                            WHERE B.BenchmarkIndexPK IS NULL

                            UPDATE B SET B.CloseIND = A.CloseIND, B.Volume = A.Volume, B.Value = A.Value, B.UpdateUsersID = @UserID,B.UpdateTime = @Date,  
                            from dbo.BenchmarkIndextemp A
                            INNER JOIN dbo.[Index] C ON A.IndexID = C.ID AND C.status = 2
                            LEFT JOIN BenchmarkIndex B ON C.IndexPK = B.IndexPK AND A.Date = B.Date
                            WHERE B.BenchmarkIndexPK IS NOT NULL";
                                cmd2.Parameters.AddWithValue("@Date", _datetimeNow);
                                cmd2.Parameters.AddWithValue("@UserID", _userID);
                                cmd2.ExecuteNonQuery();

                            }
                            _msg = "Import Done";

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

        private DataTable CreateDataTableIDX(string _path)
        {
            try
            {
                using (DataTable dt = new DataTable())
                {
                    DataColumn dc;
                    DataRow dr;

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Date";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "IndexID";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Decimal");
                    dc.ColumnName = "OpenInd";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Decimal");
                    dc.ColumnName = "HighInd";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Decimal");
                    dc.ColumnName = "LowInd";
                    dc.Unique = false;
                    dt.Columns.Add(dc);


                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Decimal");
                    dc.ColumnName = "CloseInd";
                    dc.Unique = false;
                    dt.Columns.Add(dc);


                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Decimal");
                    dc.ColumnName = "Volume";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Decimal");
                    dc.ColumnName = "Value";
                    dc.Unique = false;
                    dt.Columns.Add(dc);


                    using (OdbcConnection odConnection = new OdbcConnection(Tools.conStringOdBc))
                    {
                        odConnection.Open();
                        using (OdbcCommand odCmd = odConnection.CreateCommand())
                        {
                            odCmd.CommandText = "SELECT * FROM " + Tools.DBFFilePath + _path;
                            using (OdbcDataReader odRdr = odCmd.ExecuteReader())
                            {
                                for (int i = 1; i <= 1; i++)
                                {
                                    odRdr.Read();
                                }
                                do
                                {
                                    dr = dt.NewRow();

                                    dr["Date"] = odRdr[0];
                                    dr["IndexID"] = odRdr[1];
                                    dr["OpenInd"] = 0;
                                    dr["HighInd"] = 0;
                                    dr["LowInd"] = 0;
                                    dr["CloseInd"] = odRdr[4];
                                    dr["Volume"] = odRdr[5];
                                    dr["Value"] = odRdr[6];

                                    dt.Rows.Add(dr);
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


        #region Benchmark Index
        public string ImportBenchmarkIndex(string _fileSource, string _userID)
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
                                cmd1.CommandText = "truncate table ZUpload_BenchmarkIndex";
                                cmd1.ExecuteNonQuery();
                            }
                        }

                        // import data ke temp dulu
                        using (SqlBulkCopy bulkCopy = new SqlBulkCopy(Tools.conString, SqlBulkCopyOptions.TableLock))
                        {
                            bulkCopy.DestinationTableName = "dbo.ZUpload_BenchmarkIndex";
                            bulkCopy.WriteToServer(CreateDataTableFromBenchmarkIndex(_fileSource));
                            _msg = "Import Benchmark Index Success";
                        }

                        using (SqlConnection conn = new SqlConnection(Tools.conString))
                        {
                            conn.Open();
                            using (SqlCommand cmd1 = conn.CreateCommand())
                            {
                                cmd1.CommandTimeout = 0;
                                cmd1.CommandText =
                              @"

                                Declare @Date datetime
                                declare @Indeks nvarchar(50)

                                Declare @MaxBenchmarkIndexPK int



                                Declare A Cursor for 
                                Select distinct Date,Indeks from ZUPLOAD_BENCHMARKINDEX where Date is not null
                                Open A
                                FETCH NEXT FROM A   
                                INTO @Date,@Indeks

                                WHILE @@FETCH_STATUS = 0  
                                BEGIN 

                                select @MaxBenchmarkIndexPK = isnull(max(BenchmarkIndexPK),0) From BenchmarkIndex        

                                delete A from  BenchmarkIndex A
                                left join [Index] B on A.IndexPK = B.IndexPK and B.status in (1,2)
                                where A.Date = @Date and B.ID = @Indeks

                                insert into BenchmarkIndex (BenchmarkIndexPK,HistoryPK,status,Date,IndexPK,FundPK,OpenInd,HighInd,LowInd,CloseInd,Volume,Value,EntryUsersID,EntryTime,LastUpdate)
                                select @MaxBenchmarkIndexPK + ROW_NUMBER() OVER(ORDER BY A.Date ASC),1,1,@Date,isnull(B.IndexPK,0),0,0,0,0,CloseInd,0,0,'sa',getdate(),getdate() from ZUPLOAD_BENCHMARKINDEX A
                                left join [Index] B on A.Indeks = B.ID and B.status in (1,2)
                                where Date = @Date  and A.Indeks is not null and isnull(B.IndexPK,0) <> 0


                                FETCH NEXT FROM A   
                                INTO @Date,@Indeks
                                END   
                                CLOSE A  
                                DEALLOCATE A
                                ";

                                cmd1.ExecuteNonQuery();

                                return _msg;

                            }

                        }

                        return _msg;
                    }
                }

            }
            catch (Exception err)
            {
                throw err;
            }
        }

        private DataTable CreateDataTableFromBenchmarkIndex(string _fileName)
        {
            DataTable dt = new DataTable();
            DataColumn dc;
            DataRow dr;

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Indeks";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "Date";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "CloseInd";
            dc.Unique = false;
            dt.Columns.Add(dc);



            using (OleDbConnection odConnection = new OleDbConnection(Tools.ConStringExcel2007(_fileName)))
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
                            dr["Indeks"] = odRdr[0];
                            dr["Date"] = odRdr[1];
                            dr["CloseInd"] = odRdr[2];


                            dt.Rows.Add(dr);

                        } while (odRdr.Read());
                    }
                }
                odConnection.Close();
            }


            return dt;
        }
        #endregion


        public List<BenchmarkIndexCombo> BenchmarkIndex_Combo()
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<BenchmarkIndexCombo> L_BenchmarkIndex = new List<BenchmarkIndexCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"IF (@ClientCode = 20) 
                                            BEGIN
                                               SELECT top 1 BenchmarkIndexPK, CloseInd FROM BenchmarkIndex  where status = 2 order by date desc 
                                            END
                                            ELSE
                                            BEGIN
                                                SELECT 0 BenchmarkIndexPK, 0 CloseInd
                                            END ";
                        cmd.Parameters.AddWithValue("@ClientCode", Tools.ClientCode);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    BenchmarkIndexCombo M_BenchmarkIndex = new BenchmarkIndexCombo();
                                    M_BenchmarkIndex.BenchmarkIndexPK = Convert.ToInt32(dr["BenchmarkIndexPK"]);
                                    M_BenchmarkIndex.CloseInd = Convert.ToDecimal(dr["CloseInd"]);
                                    L_BenchmarkIndex.Add(M_BenchmarkIndex);
                                }

                            }
                            return L_BenchmarkIndex;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }


        }

        public List<BenchmarkIndex> BenchmarkIndex_SelectByDateFromTo(int _status, DateTime _dateFrom, DateTime _dateTo)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<BenchmarkIndex> L_BenchmarkIndex = new List<BenchmarkIndex>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText =
                              @"Select case when GR.status=1 then 'PENDING' else Case When GR.status = 2 then 'APPROVED' 
							 else Case when GR.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,R.name IndexID,R.ID IndexID, GR.* from BenchmarkIndex GR left join 
                              [Index] R on GR.IndexPK = R.IndexPK  and R.status = 2
                              where GR.status = @status and Date between @DateFrom and @DateTo ";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText =
                              @"Select case when GR.status=1 then 'PENDING' else Case When GR.status = 2 then 'APPROVED' 
                            else Case when GR.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,R.ID IndexID,R.Name IndexName, GR.* from BenchmarkIndex GR left join
                              [Index] R on GR.IndexPK = R.IndexPK  and R.status = 2
                              where Date between @DateFrom and @DateTo ";
                        }

                        cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _dateTo);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_BenchmarkIndex.Add(setBenchmarkIndex(dr));
                                }
                            }
                            return L_BenchmarkIndex;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public void BenchmarkIndex_ApproveBySelected(string _usersID, string _permissionID, DateTime _dateFrom, DateTime _dateTo, BenchmarkIndex _BenchmarkIndex)
        {
            try
            {
                string paramBenchmarkIndexSelected = "";
                paramBenchmarkIndexSelected = "BenchmarkIndexPK in (" + _BenchmarkIndex.BenchmarkIndexSelected + ") ";

                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"
                        Declare @IPAddress nvarchar(50) select @IPAddress = IPAddress from Users where ID = @UsersID and Status = 2
                        Insert into Activity(Time,PermissionID,ObjectTable,ObjectTablePK,Status,Message,UsersID,IPAddress,LastUpdate)
                        Select @Time,@PermissionID,'BenchmarkIndex',BenchmarkIndexPK,1,'Approve by Selected Data',@UsersID,@IPAddress,@Time  from BenchmarkIndex where Date between @DateFrom and @DateTo and Status = 1 and " + paramBenchmarkIndexSelected + @"
                       
                        update BenchmarkIndex set status = 2,ApprovedUsersID = @UsersID,ApprovedTime = @Time,LastUpdate=@Time 
                        where status = 1 and BenchmarkIndexPK in ( Select BenchmarkIndexPK from BenchmarkIndex where Date between @DateFrom and @DateTo and Status = 1 and " + paramBenchmarkIndexSelected + @" ) 
                        
                        Update BenchmarkIndex set status= 3,VoidUsersID=@UsersID,VoidTime=@Time,LastUpdate=@Time  
                        where status = 4 and BenchmarkIndexPK in (Select BenchmarkIndexPK from BenchmarkIndex where Date between @DateFrom and @DateTo and Status = 4 and " + paramBenchmarkIndexSelected + @")   

                        ";
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

        public string CheckHassAdd(string _dateFrom, string _dateTo)
        {
            try
            {
                var _msg = "";
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    string date = System.DateTime.Today.ToString("MM/dd/yyyy");
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"
                        IF EXISTS(
                        SELECT * FROM BenchmarkIndex A
                        INNER JOIN BenchmarkIndex B ON A.FundPK = B.FundPK AND A.Date = B.Date 
                        WHERE A.Status = 1 AND B.status = 2 and A.Selected = 1 and A.Approved1 = 1  and A.Date Between @DateFrom and @DateTo
                        )
                        BEGIN

	                        SELECT 'true' ReturnDesc

                        END
                        ELSE
                        BEGIN
	                        SELECT 'false' ReturnDesc
                        END
                        ";
                        cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _dateTo);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return _msg = Convert.ToString(dr["ReturnDesc"]);
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

        public void BenchmarkIndex_RejectBySelected(string _usersID, string _permissionID, DateTime _dateFrom, DateTime _dateTo, BenchmarkIndex _BenchmarkIndex)
        {
            try
            {
                string paramBenchmarkIndexSelected = "";
                paramBenchmarkIndexSelected = "BenchmarkIndexPK in (" + _BenchmarkIndex.BenchmarkIndexSelected + ") ";

                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"
                        Declare @IPAddress nvarchar(50) select @IPAddress = IPAddress from Users where ID = @UsersID and Status = 2      

                        Insert into Activity(Time,PermissionID,ObjectTable,ObjectTablePK,Status,Message,UsersID,IPAddress,LastUpdate)      
                        Select @Time,@PermissionID,'BenchmarkIndex',BenchmarkIndexPK,1,'Reject by Selected Data',@UsersID,@IPAddress,@Time  from BenchmarkIndex where Date between @DateFrom and @DateTo and Status = 1 and " + paramBenchmarkIndexSelected + @"    

                        update BenchmarkIndex set status = 3,VoidUsersID = @UsersID,VoidTime = @Time,LastUpdate=@Time 
                        where status = 1 and BenchmarkIndexPK in ( Select BenchmarkIndexPK from BenchmarkIndex where Date between @DateFrom and @DateTo and Status = 1 and " + paramBenchmarkIndexSelected + @" )      

                        Update BenchmarkIndex set status= 2  
                        where status = 4 and BenchmarkIndexPK in (Select BenchmarkIndexPK from BenchmarkIndex where Date between @DateFrom and @DateTo and Status = 4 and " + paramBenchmarkIndexSelected + @")  
                        ";
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

        public void BenchmarkIndex_VoidBySelected(string _usersID, string _permissionID, DateTime _dateFrom, DateTime _dateTo, BenchmarkIndex _BenchmarkIndex)
        {
            try
            {
                string paramBenchmarkIndexSelected = "";
                paramBenchmarkIndexSelected = "BenchmarkIndexPK in (" + _BenchmarkIndex.BenchmarkIndexSelected + ") ";

                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"
                                            Declare @IPAddress nvarchar(50) 
                                            select @IPAddress = IPAddress from Users where ID = @UsersID and Status = 2 

                                            Insert into Activity(Time,PermissionID,ObjectTable,ObjectTablePK,Status,Message,UsersID,IPAddress,LastUpdate)
                                            Select @Time,@PermissionID,'BenchmarkIndex',BenchmarkIndexPK,1,'Void by Selected Data',@UsersID,@IPAddress,@Time  from BenchmarkIndex where Date between @DateFrom and @DateTo and Status = 2 and " + paramBenchmarkIndexSelected + @"

                                            update BenchmarkIndex set status = 3,VoidUsersID = @UsersID,VoidTime = @Time,LastUpdate=@Time where " + paramBenchmarkIndexSelected
                                            ;
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

        //------------------------------Approve 1

        public void BenchmarkIndex_Approve1BySelected(string _usersID, string _permissionID, DateTime _dateFrom, DateTime _dateTo, BenchmarkIndex _BenchmarkIndex)
        {
            try
            {
                string paramBenchmarkIndexSelected = "";
                paramBenchmarkIndexSelected = "BenchmarkIndexPK in (" + _BenchmarkIndex.BenchmarkIndexSelected + ") ";

                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"
                        Declare @IPAddress nvarchar(50) select @IPAddress = IPAddress from Users where ID = @UsersID and Status = 2
                        Insert into Activity(Time,PermissionID,ObjectTable,ObjectTablePK,Status,Message,UsersID,IPAddress,LastUpdate)
                        Select @Time,@PermissionID,'BenchmarkIndex',BenchmarkIndexPK,1,'Approve by Selected Data',@UsersID,@IPAddress,@Time  from BenchmarkIndex where Date between @DateFrom and @DateTo and Status = 1 and " + paramBenchmarkIndexSelected + @"
                       
                        update BenchmarkIndex set Approved1 = 1,ApprovedUsersID = @UsersID,ApprovedTime = @Time,LastUpdate=@Time 
                        where status = 1 and BenchmarkIndexPK in ( Select BenchmarkIndexPK from BenchmarkIndex where Date between @DateFrom and @DateTo and Status = 1 and " + paramBenchmarkIndexSelected + @")  

                        ";
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




    }
}