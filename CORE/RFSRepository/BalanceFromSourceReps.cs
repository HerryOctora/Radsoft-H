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
using System.Data.Odbc;
using System.Data.OleDb;

namespace RFSRepository
{
    public class BalanceFromSourceReps
    {

        Host _host = new Host();

        //1
        string _insertCommand = "INSERT INTO [dbo].[BalanceFromSource] " +
                            "([BalanceFromSourcePK],[HistoryPK],[Status],[COAFromSourcePK],[Date],[PrevBalance],[CurrentBalance],[EndBalance],[MISCostCenterPK],";
        string _paramaterCommand = "@COAFromSourcePK,@Date,@PrevBalance,@CurrentBalance,@EndBalance,@MISCostCenterPK,";

        //2

        private BalanceFromSource setBalanceFromSource(SqlDataReader dr)
        {
            BalanceFromSource M_BalanceFromSource = new BalanceFromSource();
            M_BalanceFromSource.BalanceFromSourcePK = Convert.ToInt32(dr["BalanceFromSourcePK"]);
            M_BalanceFromSource.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_BalanceFromSource.Status = Convert.ToInt32(dr["Status"]);
            M_BalanceFromSource.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_BalanceFromSource.Notes = Convert.ToString(dr["Notes"]);
            M_BalanceFromSource.COAFromSourcePK = Convert.ToInt32(dr["COAFromSourcePK"]);
            M_BalanceFromSource.COAFromSourceID = dr["COAFromSourceID"].ToString();
            M_BalanceFromSource.COAFromSourceName = dr["COAFromSourceName"].ToString();
            M_BalanceFromSource.Date = dr["Date"].ToString();
            M_BalanceFromSource.PrevBalance = Convert.ToDecimal(dr["PrevBalance"]);
            M_BalanceFromSource.CurrentBalance = Convert.ToDecimal(dr["CurrentBalance"]);
            M_BalanceFromSource.EndBalance = Convert.ToDecimal(dr["EndBalance"]);
            M_BalanceFromSource.MISCostCenterPK = Convert.ToInt32(dr["MISCostCenterPK"]);
            M_BalanceFromSource.MISCostCenterID = Convert.ToString(dr["MISCostCenterID"]);
            M_BalanceFromSource.MISCostCenterName = Convert.ToString(dr["MISCostCenterName"]);
            M_BalanceFromSource.EntryUsersID = dr["EntryUsersID"].ToString();
            M_BalanceFromSource.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_BalanceFromSource.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_BalanceFromSource.VoidUsersID = dr["VoidUsersID"].ToString();
            M_BalanceFromSource.EntryTime = dr["EntryTime"].ToString();
            M_BalanceFromSource.UpdateTime = dr["UpdateTime"].ToString();
            M_BalanceFromSource.ApprovedTime = dr["ApprovedTime"].ToString();
            M_BalanceFromSource.VoidTime = dr["VoidTime"].ToString();
            M_BalanceFromSource.DBUserID = dr["DBUserID"].ToString();
            M_BalanceFromSource.DBTerminalID = dr["DBTerminalID"].ToString();
            M_BalanceFromSource.LastUpdate = dr["LastUpdate"].ToString();
            M_BalanceFromSource.LastUpdateDB = Convert.ToString(dr["LastUpdateDB"]);
            return M_BalanceFromSource;
        }

        public List<BalanceFromSource> BalanceFromSource_Select(int _status)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<BalanceFromSource> L_BalanceFromSource = new List<BalanceFromSource>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = @"Select case when A.status=1 then 'PENDING' else Case When A.status = 2 then 'APPROVED' else Case when A.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,B.ID COAFromSourceID, B.Name COAFromSourceName,C.ID MISCostCenterID,C.Name MISCostCenterName, A.* from BalanceFromSource A
                                                left join COAFromSource B on A.COAFromSourcePK = B.COAFromSourcePK and B.status = 2 
                                                left join MISCostCenter C on A.MISCostCenterPK = C.MISCostCenterPK  and C.status = 2 
                                                where A.status = @status";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = @"Select case when A.status=1 then 'PENDING' else Case When A.status = 2 then 'APPROVED' else Case when A.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,B.ID COAFromSourceID, B.Name COAFromSourceName,C.ID MISCostCenterID,C.Name MISCostCenterName, A.* from BalanceFromSource A
                                                left join COAFromSource B on A.COAFromSourcePK = B.COAFromSourcePK and B.status = 2 
                                                left join MISCostCenter C on A.MISCostCenterPK = C.MISCostCenterPK  and C.status = 2 
                                                order by COAFromSourcePK,MISCostCenterPK";
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_BalanceFromSource.Add(setBalanceFromSource(dr));
                                }
                            }
                            return L_BalanceFromSource;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int BalanceFromSource_Add(BalanceFromSource _BalanceFromSource, bool _havePrivillege)
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
                                 "Select isnull(max(BalanceFromSourcePk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@LastUpdate from BalanceFromSource";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _BalanceFromSource.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],LastUpdate)" +
                                "Select isnull(max(BalanceFromSourcePk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@LastUpdate from BalanceFromSource";
                        }
                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@COAFromSourcePK", _BalanceFromSource.COAFromSourcePK);
                        cmd.Parameters.AddWithValue("@Date", _BalanceFromSource.Date);
                        cmd.Parameters.AddWithValue("@PrevBalance", _BalanceFromSource.PrevBalance);
                        cmd.Parameters.AddWithValue("@CurrentBalance", _BalanceFromSource.CurrentBalance);
                        cmd.Parameters.AddWithValue("@EndBalance", _BalanceFromSource.EndBalance);
                        cmd.Parameters.AddWithValue("@MISCostCenterPK", _BalanceFromSource.MISCostCenterPK);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _BalanceFromSource.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);

                        cmd.ExecuteNonQuery();

                        return _host.Get_LastPKByLastUpate(_datetimeNow, "BalanceFromSource");
                    }

                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int BalanceFromSource_Update(BalanceFromSource _BalanceFromSource, bool _havePrivillege)
        {

            try
            {
                int _newHisPK;
                int status = _host.Get_Status(_BalanceFromSource.BalanceFromSourcePK, _BalanceFromSource.HistoryPK, "BalanceFromSource");
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update BalanceFromSource set status=2, Notes=@Notes,COAFromSourcePK=@COAFromSourcePK,Date=@Date,PrevBalance=@PrevBalance,CurrentBalance=@CurrentBalance,EndBalance=@EndBalance,MISCostCenterPK=@MISCostCenterPK," +
                                "ApprovedUsersID=@ApprovedUsersID, " +
                                "ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@lastupdate " +
                                "where BalanceFromSourcePK = @PK and historyPK = @HistoryPK";
                            cmd.Parameters.AddWithValue("@HistoryPK", _BalanceFromSource.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _BalanceFromSource.BalanceFromSourcePK);
                            cmd.Parameters.AddWithValue("@Notes", _BalanceFromSource.Notes);
                            cmd.Parameters.AddWithValue("@COAFromSourcePK", _BalanceFromSource.COAFromSourcePK);
                            cmd.Parameters.AddWithValue("@Date", _BalanceFromSource.Date);
                            cmd.Parameters.AddWithValue("@PrevBalance", _BalanceFromSource.PrevBalance);
                            cmd.Parameters.AddWithValue("@CurrentBalance", _BalanceFromSource.CurrentBalance);
                            cmd.Parameters.AddWithValue("@EndBalance", _BalanceFromSource.EndBalance);
                            cmd.Parameters.AddWithValue("@MISCostCenterPK", _BalanceFromSource.MISCostCenterPK);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _BalanceFromSource.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _BalanceFromSource.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);

                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update BalanceFromSource set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where BalanceFromSourcePK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _BalanceFromSource.BalanceFromSourcePK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _BalanceFromSource.EntryUsersID);
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
                                cmd.CommandText = "Update BalanceFromSource set Notes=@Notes,COAFromSourcePK=@COAFromSourcePK,Date=@Date,PrevBalance=@PrevBalance,CurrentBalance=@CurrentBalance,EndBalance=@EndBalance,MISCostCenterPK=@MISCostCenterPK," +
                                "UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@lastupdate " +
                                "where BalanceFromSourcePK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _BalanceFromSource.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _BalanceFromSource.BalanceFromSourcePK);
                                cmd.Parameters.AddWithValue("@Notes", _BalanceFromSource.Notes);
                                cmd.Parameters.AddWithValue("@COAFromSourcePK", _BalanceFromSource.COAFromSourcePK);
                                cmd.Parameters.AddWithValue("@Date", _BalanceFromSource.Date);
                                cmd.Parameters.AddWithValue("@PrevBalance", _BalanceFromSource.PrevBalance);
                                cmd.Parameters.AddWithValue("@CurrentBalance", _BalanceFromSource.CurrentBalance);
                                cmd.Parameters.AddWithValue("@EndBalance", _BalanceFromSource.EndBalance);
                                cmd.Parameters.AddWithValue("@MISCostCenterPK", _BalanceFromSource.MISCostCenterPK);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _BalanceFromSource.EntryUsersID);
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
                                _newHisPK = _host.Get_NewHistoryPK(_BalanceFromSource.BalanceFromSourcePK, "BalanceFromSource");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From BalanceFromSource where BalanceFromSourcePK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _BalanceFromSource.BalanceFromSourcePK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _BalanceFromSource.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@COAFromSourcePK", _BalanceFromSource.COAFromSourcePK);
                                cmd.Parameters.AddWithValue("@Date", _BalanceFromSource.Date);
                                cmd.Parameters.AddWithValue("@PrevBalance", _BalanceFromSource.PrevBalance);
                                cmd.Parameters.AddWithValue("@CurrentBalance", _BalanceFromSource.CurrentBalance);
                                cmd.Parameters.AddWithValue("@EndBalance", _BalanceFromSource.EndBalance);
                                cmd.Parameters.AddWithValue("@MISCostCenterPK", _BalanceFromSource.MISCostCenterPK);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _BalanceFromSource.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update BalanceFromSource set status= 4,Notes=@Notes," +
                                " LastUpdate=@lastupdate " +
                                " where BalanceFromSourcePK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _BalanceFromSource.Notes);
                                cmd.Parameters.AddWithValue("@PK", _BalanceFromSource.BalanceFromSourcePK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _BalanceFromSource.HistoryPK);
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

        public void BalanceFromSource_Approved(BalanceFromSource _BalanceFromSource)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update BalanceFromSource set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,LastUpdate=@lastupdate " +
                            "where BalanceFromSourcePK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _BalanceFromSource.BalanceFromSourcePK);
                        cmd.Parameters.AddWithValue("@historyPK", _BalanceFromSource.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _BalanceFromSource.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update BalanceFromSource set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where BalanceFromSourcePK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _BalanceFromSource.BalanceFromSourcePK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _BalanceFromSource.ApprovedUsersID);
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

        public void BalanceFromSource_Reject(BalanceFromSource _BalanceFromSource)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update BalanceFromSource set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where BalanceFromSourcePK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _BalanceFromSource.BalanceFromSourcePK);
                        cmd.Parameters.AddWithValue("@historyPK", _BalanceFromSource.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _BalanceFromSource.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update BalanceFromSource set status= 2,LastUpdate=@LastUpdate where BalanceFromSourcePK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _BalanceFromSource.BalanceFromSourcePK);
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

        public void BalanceFromSource_Void(BalanceFromSource _BalanceFromSource)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update BalanceFromSource set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where BalanceFromSourcePK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _BalanceFromSource.BalanceFromSourcePK);
                        cmd.Parameters.AddWithValue("@historyPK", _BalanceFromSource.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _BalanceFromSource.VoidUsersID);
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



        public string ImportBalanceFromSource(string _fileSource, string _userID, DateTime _date, int _costCenterPK)
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
                            cmd1.CommandText = "truncate table BalanceFromSourceTemp";
                            cmd1.ExecuteNonQuery();
                        }
                    }

                    // import data ke temp dulu
                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(Tools.conString, SqlBulkCopyOptions.TableLock))
                    {
                        bulkCopy.DestinationTableName = "dbo.BalanceFromSourceTemp";
                        bulkCopy.WriteToServer(CreateDataTableFromBalanceFromSourceTempExcelFile(_fileSource));
                    }

                    // logic kalo Reconcile success
                    using (SqlConnection conn = new SqlConnection(Tools.conString))
                    {
                        conn.Open();
                        using (SqlCommand cmd1 = conn.CreateCommand())
                        {
                            cmd1.CommandText = @"
                            Truncate Table ZTEMPMIS_2
                            Insert Into ZTEMPMIS_2 (ID,EndBalance,CurrentBalance,PrevBalance)
                            select ID,CONVERT(numeric(22,4),replace(replace(replace(RTRIM(LTRIM(EndBalance)),',',''),')',''),'(','-')) EndBalance,
                            CONVERT(numeric(22,4),replace(replace(replace(RTRIM(LTRIM(CurrentBalance)),',',''),')',''),'(','-')) CurrentBalance,
                            CONVERT(numeric(22,4),replace(replace(replace(RTRIM(LTRIM(PrevBalance)),',',''),')',''),'(','-')) PrevBalance
                            from BalanceFromSourceTemp where ID <> '' and EndBalance <> ''
                            
                            IF Exists(select * from ZTEMPMIS_2
                                where ID not in
                                (
                                Select ID from CoaFromSource where status = 2
                                )
                            )
                            BEGIN
                                select top 1 'ACCOUNT ID: ' + ID + ' Belum ada di Sistem COA From Source ' A from ZTEMPMIS_2
                                where ID not in
                                (
                                Select ID from CoaFromSource where status = 2
                                )
                            return
                            END


                            Declare @MaxPK int
                            select @MaxPK = Max(BalanceFromsourcePK) from BalanceFromSource
                            set @maxPK = isnull(@maxPK,1)

                            Delete BalanceFromSource where month(date) = month(@Date) and year(date) = year(@Date) and  MISCostCenterPK = @CcPK

                            insert into balanceFromsource(BalanceFromSourcePK,HistoryPK,status,notes,
                            COAFromSourcePK,MISCostCenterPK,Date,PrevBalance,CurrentBalance,EndBalance,lastupdate)
                            select  @MaxPK + ROW_NUMBER() OVER(ORDER BY A.ID ASC), 1,2,'',
                            B.COAFromSourcePK,@CcPK,@Date,isnull(A.PrevBalance,0),isnull(A.CurrentBalance,0),isnull(A.EndBalance,0),GetDate()
                            from ZTEMPMIS_2 A
                            left join COAFromSource B on A.ID = B.ID and B.status = 2

                            Select 'Import Success' A ";
                            cmd1.Parameters.AddWithValue("@UserID", _userID);
                            cmd1.Parameters.AddWithValue("@TimeNow", _now);
                            cmd1.Parameters.AddWithValue("@Date", _date);
                            cmd1.Parameters.AddWithValue("@CcPK", _costCenterPK);

                            using (SqlDataReader dr = cmd1.ExecuteReader())
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
            }


            catch (Exception err)
            {
                throw err;
            }
        }
        private DataTable CreateDataTableFromBalanceFromSourceTempExcelFile(string _path)
        {
            try
            {
                using (DataTable dt = new DataTable())
                {
                    DataColumn dc;
                    DataRow dr;

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Int32");
                    dc.ColumnName = "BalanceFromSourceTempPK";
                    dc.Unique = false;
                    dc.AutoIncrement = true;
                    dc.AutoIncrementSeed = 1;
                    dc.AutoIncrementStep = 1;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "ID";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "EndBalance";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "CurrentBalance";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "PrevBalance";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

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
                                for (int i = 1; i <= 9; i++)
                                {
                                    odRdr.Read();
                                }
                                do
                                {
                                    dr = dt.NewRow();

                                    dr["ID"] = Convert.ToString(odRdr[0]);
                                    dr["EndBalance"] = Convert.ToString(odRdr[18]);
                                    dr["CurrentBalance"] = Convert.ToString(odRdr[15]);
                                    dr["PrevBalance"] = Convert.ToString(odRdr[12]);
                                    if (dr["BalanceFromSourceTempPK"].Equals(DBNull.Value) != true) { dt.Rows.Add(dr); }
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

    }
}