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
    public class AuditCOAFromSourceReps
    {
          Host _host = new Host();

        //1
          string _insertCommand = "INSERT INTO [dbo].[AuditCOAFromSource] " +
                            "([AuditCOAFromSourcePK],[HistoryPK],[Status],[COAFromSourcePK],[PeriodPK],[Amount],[MISCostCenterPK],";
        string _paramaterCommand = "@COAFromSourcePK,@PeriodPK,@Amount,@MISCostCenterPK,";

        //2

        private AuditCOAFromSource setAuditCOAFromSource(SqlDataReader dr)
        {
            AuditCOAFromSource M_AuditCOAFromSource = new AuditCOAFromSource();
            M_AuditCOAFromSource.AuditCOAFromSourcePK = Convert.ToInt32(dr["AuditCOAFromSourcePK"]);
            M_AuditCOAFromSource.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_AuditCOAFromSource.Status = Convert.ToInt32(dr["Status"]);
            M_AuditCOAFromSource.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_AuditCOAFromSource.Notes = Convert.ToString(dr["Notes"]);
            M_AuditCOAFromSource.COAFromSourcePK = Convert.ToInt32(dr["COAFromSourcePK"]);
            M_AuditCOAFromSource.COAFromSourceID = Convert.ToString(dr["COAFromSourceID"]);
            M_AuditCOAFromSource.COAFromSourceName = Convert.ToString(dr["COAFromSourceName"]);
            M_AuditCOAFromSource.PeriodPK = Convert.ToInt32(dr["PeriodPK"]);
            M_AuditCOAFromSource.PeriodID = Convert.ToString(dr["PeriodID"]);
            M_AuditCOAFromSource.Amount = Convert.ToDecimal(dr["Amount"]);
            M_AuditCOAFromSource.MISCostCenterPK = Convert.ToInt32(dr["MISCostCenterPK"]);
            M_AuditCOAFromSource.MISCostCenterID = Convert.ToString(dr["MISCostCenterID"]);
            M_AuditCOAFromSource.MISCostCenterName = Convert.ToString(dr["MISCostCenterName"]);
            M_AuditCOAFromSource.EntryUsersID = dr["EntryUsersID"].ToString();
            M_AuditCOAFromSource.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_AuditCOAFromSource.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_AuditCOAFromSource.VoidUsersID = dr["VoidUsersID"].ToString();
            M_AuditCOAFromSource.EntryTime = dr["EntryTime"].ToString();
            M_AuditCOAFromSource.UpdateTime = dr["UpdateTime"].ToString();
            M_AuditCOAFromSource.ApprovedTime = dr["ApprovedTime"].ToString();
            M_AuditCOAFromSource.VoidTime = dr["VoidTime"].ToString();
            M_AuditCOAFromSource.DBUserID = dr["DBUserID"].ToString();
            M_AuditCOAFromSource.DBTerminalID = dr["DBTerminalID"].ToString();
            M_AuditCOAFromSource.LastUpdate = dr["LastUpdate"].ToString();
            M_AuditCOAFromSource.LastUpdateDB = Convert.ToString(dr["LastUpdateDB"]);
            return M_AuditCOAFromSource;
        }

        public List<AuditCOAFromSource> AuditCOAFromSource_Select(int _status)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<AuditCOAFromSource> L_AuditCOAFromSource = new List<AuditCOAFromSource>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = @"Select case when A.status=1 then 'PENDING' else Case When A.status = 2 then 'APPROVED' else Case when A.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,B.ID COAFromSourceID,B.Name COAFromSourceName,C.ID PeriodID,D.ID MISCostCenterID,D.Name MISCostCenterName, A.* from AuditCOAFromSource A 
                            left join COAFromSource B on A.COAFromSourcePK = B.COAFromSourcePK and B.status = 2 
                            left join Period C on A.PeriodPK = C.PeriodPK  and C.status = 2
                            left join MISCostCenter D on A.MISCostCenterPK = D.MISCostCenterPK and D.status = 2
                            where A.status= @status  ";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = @"Select case when A.status=1 then 'PENDING' else Case When A.status = 2 then 'APPROVED' else Case when A.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,B.ID COAFromSourceID,B.Name COAFromSourceName,C.ID PeriodID,D.ID MISCostCenterID,D.Name MISCostCenterName, A.* from AuditCOAFromSource A 
                            left join COAFromSource B on A.COAFromSourcePK = B.COAFromSourcePK and B.status = 2 
                            left join Period C on A.PeriodPK = C.PeriodPK  and C.status = 2
                            left join MISCostCenter D on A.MISCostCenterPK = D.MISCostCenterPK and D.status = 2
                            order by COAFromSourcePK,PeriodPK,MISCostCenterPK ";
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_AuditCOAFromSource.Add(setAuditCOAFromSource(dr));
                                }
                            }
                            return L_AuditCOAFromSource;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int AuditCOAFromSource_Add(AuditCOAFromSource _AuditCOAFromSource, bool _havePrivillege)
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
                                 "Select isnull(max(AuditCOAFromSourcePk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@LastUpdate from AuditCOAFromSource";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _AuditCOAFromSource.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],LastUpdate)" +
                                "Select isnull(max(AuditCOAFromSourcePk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@LastUpdate from AuditCOAFromSource";
                        }
                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@COAFromSourcePK", _AuditCOAFromSource.COAFromSourcePK);
                        cmd.Parameters.AddWithValue("@PeriodPK", _AuditCOAFromSource.PeriodPK); 
                        cmd.Parameters.AddWithValue("@Amount", _AuditCOAFromSource.Amount);
                        cmd.Parameters.AddWithValue("@MISCostCenterPK", _AuditCOAFromSource.MISCostCenterPK);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _AuditCOAFromSource.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);

                        cmd.ExecuteNonQuery();

                        return _host.Get_LastPKByLastUpate(_datetimeNow, "AuditCOAFromSource");
                    }
                }

            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int AuditCOAFromSource_Update(AuditCOAFromSource _AuditCOAFromSource, bool _havePrivillege)
        {

            try
            {
                int _newHisPK;
                int status = _host.Get_Status(_AuditCOAFromSource.AuditCOAFromSourcePK, _AuditCOAFromSource.HistoryPK, "AuditCOAFromSource"); ;
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update AuditCOAFromSource set status=2, Notes=@Notes,COAFromSourcePK=@COAFromSourcePK,PeriodPK=@PeriodPK,Amount=@Amount,MISCostCenterPK=@MISCostCenterPK," +
                                "ApprovedUsersID=@ApprovedUsersID, " +
                                "ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@lastupdate " +
                                "where AuditCOAFromSourcePK = @PK and historyPK = @HistoryPK";
                            cmd.Parameters.AddWithValue("@HistoryPK", _AuditCOAFromSource.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _AuditCOAFromSource.AuditCOAFromSourcePK);
                            cmd.Parameters.AddWithValue("@Notes", _AuditCOAFromSource.Notes);
                            cmd.Parameters.AddWithValue("@COAFromSourcePK", _AuditCOAFromSource.COAFromSourcePK);
                            cmd.Parameters.AddWithValue("@PeriodPK", _AuditCOAFromSource.PeriodPK);
                            cmd.Parameters.AddWithValue("@Amount", _AuditCOAFromSource.Amount);
                            cmd.Parameters.AddWithValue("@MISCostCenterPK", _AuditCOAFromSource.MISCostCenterPK);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _AuditCOAFromSource.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _AuditCOAFromSource.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);

                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update AuditCOAFromSource set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where AuditCOAFromSourcePK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _AuditCOAFromSource.AuditCOAFromSourcePK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _AuditCOAFromSource.EntryUsersID);
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
                                cmd.CommandText = "Update AuditCOAFromSource set Notes=@Notes,COAFromSourcePK=@COAFromSourcePK,PeriodPK=@PeriodPK,Amount=@Amount,MISCostCenterPK=@MISCostCenterPK," +
                                "UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime ,LastUpdate=@lastupdate " +
                                "where AuditCOAFromSourcePK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _AuditCOAFromSource.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _AuditCOAFromSource.AuditCOAFromSourcePK);
                                cmd.Parameters.AddWithValue("@Notes", _AuditCOAFromSource.Notes);
                                cmd.Parameters.AddWithValue("@COAFromSourcePK", _AuditCOAFromSource.COAFromSourcePK);
                                cmd.Parameters.AddWithValue("@PeriodPK", _AuditCOAFromSource.PeriodPK);
                                cmd.Parameters.AddWithValue("@Amount", _AuditCOAFromSource.Amount);
                                cmd.Parameters.AddWithValue("@MISCostCenterPK", _AuditCOAFromSource.MISCostCenterPK);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _AuditCOAFromSource.EntryUsersID);
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
                                _newHisPK = _host.Get_NewHistoryPK(_AuditCOAFromSource.AuditCOAFromSourcePK, "AuditCOAFromSource");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From AuditCOAFromSource where AuditCOAFromSourcePK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _AuditCOAFromSource.AuditCOAFromSourcePK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _AuditCOAFromSource.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@COAFromSourcePK", _AuditCOAFromSource.COAFromSourcePK);
                                cmd.Parameters.AddWithValue("@PeriodPK", _AuditCOAFromSource.PeriodPK);
                                cmd.Parameters.AddWithValue("@Amount", _AuditCOAFromSource.Amount);
                                cmd.Parameters.AddWithValue("@MISCostCenterPK", _AuditCOAFromSource.MISCostCenterPK);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _AuditCOAFromSource.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update AuditCOAFromSource set status= 4,Notes=@Notes," +
                                " LastUpdate=@lastupdate " +
                                " where AuditCOAFromSourcePK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _AuditCOAFromSource.Notes);
                                cmd.Parameters.AddWithValue("@PK", _AuditCOAFromSource.AuditCOAFromSourcePK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _AuditCOAFromSource.HistoryPK);
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

        public void AuditCOAFromSource_Approved(AuditCOAFromSource _AuditCOAFromSource)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update AuditCOAFromSource set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,LastUpdate=@lastupdate " +
                            "where AuditCOAFromSourcePK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _AuditCOAFromSource.AuditCOAFromSourcePK);
                        cmd.Parameters.AddWithValue("@historyPK", _AuditCOAFromSource.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _AuditCOAFromSource.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update AuditCOAFromSource set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where AuditCOAFromSourcePK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _AuditCOAFromSource.AuditCOAFromSourcePK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _AuditCOAFromSource.ApprovedUsersID);
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

        public void AuditCOAFromSource_Reject(AuditCOAFromSource _AuditCOAFromSource)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update AuditCOAFromSource set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where AuditCOAFromSourcePK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _AuditCOAFromSource.AuditCOAFromSourcePK);
                        cmd.Parameters.AddWithValue("@historyPK", _AuditCOAFromSource.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _AuditCOAFromSource.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update AuditCOAFromSource set status= 2,LastUpdate=@LastUpdate  where AuditCOAFromSourcePK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _AuditCOAFromSource.AuditCOAFromSourcePK);
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

        public void AuditCOAFromSource_Void(AuditCOAFromSource _AuditCOAFromSource)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update AuditCOAFromSource set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where AuditCOAFromSourcePK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _AuditCOAFromSource.AuditCOAFromSourcePK);
                        cmd.Parameters.AddWithValue("@historyPK", _AuditCOAFromSource.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _AuditCOAFromSource.VoidUsersID);
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

        // AREA LAIN-LAIN dimulai dari sini ( untuk function diluar standart )

        public string ImportAuditCOAFromSource(string _fileSource, string _userID, int _periodPK, int _costCenterPK)
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
                            cmd1.CommandText = "truncate table AuditCOAFromSourceTemp";
                            cmd1.ExecuteNonQuery();
                        }
                    }

                    // import data ke temp dulu
                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(Tools.conString, SqlBulkCopyOptions.TableLock))
                    {
                        bulkCopy.DestinationTableName = "dbo.AuditCOAFromSourceTemp";
                        bulkCopy.WriteToServer(CreateDataTableFromAuditCOAFromSourceTempExcelFile(_fileSource));
                    }

                    // logic kalo Reconcile success
                    using (SqlConnection conn = new SqlConnection(Tools.conString))
                    {
                        conn.Open();
                        using (SqlCommand cmd1 = conn.CreateCommand())
                        {
                            cmd1.CommandText = @"
                            Delete AuditCOAFromSource where PeriodPK = @PeriodPK and  MISCostCenterPK = @CcPK

                            Declare @MaxPK int
                            select @MaxPK = Max(AuditCOAFromSourcePK) from AuditCOAFromSource
                            set @maxPK = isnull(@maxPK,0)

                            Insert Into AuditCOAFromSource (AuditCOAFromSourcePK,HistoryPK,status,Notes,COAFromSourcePK,PeriodPK,Amount,MISCostCenterPK)
                            select  @MaxPK + ROW_NUMBER() OVER(ORDER BY COAFromSourcePK ASC), 1,2,'',COAFromSourcePK,@PeriodPK,CONVERT(numeric(22,4),replace(replace(replace(RTRIM(LTRIM(Balance)),',',''),')',''),'(','-')) Amount,@CcPK
                            from AuditCOAFromSourceTemp
                            
                            Select 'Import Success' A ";
                            cmd1.Parameters.AddWithValue("@UserID", _userID);
                            cmd1.Parameters.AddWithValue("@TimeNow", _now);
                            cmd1.Parameters.AddWithValue("@PeriodPK", _periodPK);
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
        private DataTable CreateDataTableFromAuditCOAFromSourceTempExcelFile(string _path)
        {
            try
            {
                using (DataTable dt = new DataTable())
                {
                    DataColumn dc;
                    DataRow dr;

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Int32");
                    dc.ColumnName = "AuditCOAFromSourceTempPK";
                    dc.Unique = false;
                    dc.AutoIncrement = true;
                    dc.AutoIncrementSeed = 1;
                    dc.AutoIncrementStep = 1;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Int32");
                    dc.ColumnName = "COAFromSourcePK";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Name";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Balance";
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
                                for (int i = 1; i <= 1; i++)
                                {
                                    odRdr.Read();
                                }
                                do
                                {
                                    dr = dt.NewRow();

                                    dr["COAFromSourcePK"] = Convert.ToInt32(odRdr[0]);
                                    dr["Name"] = Convert.ToString(odRdr[1]);
                                    dr["Balance"] = Convert.ToString(odRdr[2]);
                                    if (dr["AuditCOAFromSourceTempPK"].Equals(DBNull.Value) != true) { dt.Rows.Add(dr); }
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