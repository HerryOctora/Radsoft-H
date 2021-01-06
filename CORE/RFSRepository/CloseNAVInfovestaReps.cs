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
    public class CloseNAVInfovestaReps
    {

        Host _host = new Host();
        //1
        string _insertCommand = "INSERT INTO [dbo].[CloseNAVInfovesta] " +
                            "([CloseNAVInfovestaPK],[HistoryPK],[Status],[Nama],[Tanggal],[PertumbuhanReturn],";
        string _paramaterCommand = "@Nama,@Tanggal,@PertumbuhanReturn,";
        
        //2
        private CloseNAVInfovesta setCloseNAVInfovesta(SqlDataReader dr)
        {
            CloseNAVInfovesta M_CloseNAVInfovesta = new CloseNAVInfovesta();
            M_CloseNAVInfovesta.CloseNAVInfovestaPK = Convert.ToInt32(dr["CloseNAVInfovestaPK"]);
            M_CloseNAVInfovesta.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_CloseNAVInfovesta.Status = Convert.ToInt32(dr["Status"]);
            M_CloseNAVInfovesta.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_CloseNAVInfovesta.Notes = Convert.ToString(dr["Notes"]);
            M_CloseNAVInfovesta.Nama = Convert.ToString(dr["Nama"]);
            M_CloseNAVInfovesta.Tanggal = Convert.ToDateTime(dr["Tanggal"]);
            M_CloseNAVInfovesta.PertumbuhanReturn = Convert.ToDecimal(dr["PertumbuhanReturn"]);
            M_CloseNAVInfovesta.EntryUsersID = dr["EntryUsersID"].ToString();
            M_CloseNAVInfovesta.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_CloseNAVInfovesta.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_CloseNAVInfovesta.VoidUsersID = dr["VoidUsersID"].ToString();
            M_CloseNAVInfovesta.EntryTime = dr["EntryTime"].ToString();
            M_CloseNAVInfovesta.UpdateTime = dr["UpdateTime"].ToString();
            M_CloseNAVInfovesta.ApprovedTime = dr["ApprovedTime"].ToString();
            M_CloseNAVInfovesta.VoidTime = dr["VoidTime"].ToString();
            M_CloseNAVInfovesta.DBUserID = dr["DBUserID"].ToString();
            M_CloseNAVInfovesta.DBTerminalID = dr["DBTerminalID"].ToString();
            M_CloseNAVInfovesta.LastUpdate = dr["LastUpdate"].ToString();
            M_CloseNAVInfovesta.LastUpdateDB = Convert.ToString(dr["LastUpdateDB"]);
            return M_CloseNAVInfovesta;
        }

        public List<CloseNAVInfovesta> CloseNAVInfovesta_Select(int _status)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<CloseNAVInfovesta> L_CloseNAVInfovesta = new List<CloseNAVInfovesta>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = @"Select case when CN.status=1 then 'PENDING' else Case When CN.status = 2 then 'APPROVED' else Case when CN.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,* from CloseNAVInfovesta CN
                           where CN.status = @status";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = @"Select case when CN.status=1 then 'PENDING' else Case When CN.status = 2 then 'APPROVED' else Case when CN.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,* from CloseNAVInfovesta CN
                            ";
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_CloseNAVInfovesta.Add(setCloseNAVInfovesta(dr));
                                }
                            }
                            return L_CloseNAVInfovesta;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int CloseNAVInfovesta_Add(CloseNAVInfovesta _CloseNAVInfovesta, bool _havePrivillege)
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
                                 "Select isnull(max(CloseNAVInfovestaPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@LastUpdate from CloseNAVInfovesta";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _CloseNAVInfovesta.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],LastUpdate)" +
                                "Select isnull(max(CloseNAVInfovestaPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@LastUpdate from CloseNAVInfovesta";
                        }
                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@Nama", _CloseNAVInfovesta.Nama);
                        cmd.Parameters.AddWithValue("@Tanggal", _CloseNAVInfovesta.Tanggal);
                        cmd.Parameters.AddWithValue("@PertumbuhanReturn", _CloseNAVInfovesta.PertumbuhanReturn);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _CloseNAVInfovesta.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);

                        cmd.ExecuteNonQuery();

                        return _host.Get_LastPKByLastUpate(_datetimeNow, "CloseNAVInfovesta");
                    }
                }

            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int CloseNAVInfovesta_Update(CloseNAVInfovesta _CloseNAVInfovesta, bool _havePrivillege)
        {

            try
            {
                int _newHisPK;
                int status = _host.Get_Status(_CloseNAVInfovesta.CloseNAVInfovestaPK, _CloseNAVInfovesta.HistoryPK, "CloseNAVInfovesta"); ;
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update CloseNAVInfovesta set status=2, Notes=@Notes,Nama=@Nama,Tanggal=@Tanggal,PertumbuhanReturn=@PertumbuhanReturn," +
                                "ApprovedUsersID=@ApprovedUsersID, " +
                                "ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@lastupdate " +
                                "where CloseNAVInfovestaPK = @PK and historyPK = @HistoryPK";
                            cmd.Parameters.AddWithValue("@HistoryPK", _CloseNAVInfovesta.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _CloseNAVInfovesta.CloseNAVInfovestaPK);
                            cmd.Parameters.AddWithValue("@Notes", _CloseNAVInfovesta.Notes);
                            cmd.Parameters.AddWithValue("@Nama", _CloseNAVInfovesta.Nama);
                            cmd.Parameters.AddWithValue("@Tanggal", _CloseNAVInfovesta.Tanggal);
                            cmd.Parameters.AddWithValue("@PertumbuhanReturn", _CloseNAVInfovesta.PertumbuhanReturn);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _CloseNAVInfovesta.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _CloseNAVInfovesta.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);

                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update CloseNAVInfovesta set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where CloseNAVInfovestaPK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _CloseNAVInfovesta.CloseNAVInfovestaPK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _CloseNAVInfovesta.EntryUsersID);
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
                                cmd.CommandText = "Update CloseNAVInfovesta set Notes=@Notes,Tanggal=@Tanggal,PertumbuhanReturn=@PertumbuhanReturn," +
                                "UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime ,LastUpdate=@lastupdate " +
                                "where CloseNAVInfovestaPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _CloseNAVInfovesta.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _CloseNAVInfovesta.CloseNAVInfovestaPK);
                                cmd.Parameters.AddWithValue("@Notes", _CloseNAVInfovesta.Notes);
                                cmd.Parameters.AddWithValue("@Nama", _CloseNAVInfovesta.Nama);
                                cmd.Parameters.AddWithValue("@Tanggal", _CloseNAVInfovesta.Tanggal);
                                cmd.Parameters.AddWithValue("@PertumbuhanReturn", _CloseNAVInfovesta.PertumbuhanReturn);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _CloseNAVInfovesta.EntryUsersID);
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
                                _newHisPK = _host.Get_NewHistoryPK(_CloseNAVInfovesta.CloseNAVInfovestaPK, "CloseNAVInfovesta");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From CloseNAVInfovesta where CloseNAVInfovestaPK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _CloseNAVInfovesta.CloseNAVInfovestaPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _CloseNAVInfovesta.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@Nama", _CloseNAVInfovesta.Nama);
                                cmd.Parameters.AddWithValue("@Tanggal", _CloseNAVInfovesta.Tanggal);
                                cmd.Parameters.AddWithValue("@PertumbuhanReturn", _CloseNAVInfovesta.PertumbuhanReturn);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _CloseNAVInfovesta.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update CloseNAVInfovesta set status= 4,Notes=@Notes," +
                                " LastUpdate=@lastupdate " +
                                " where CloseNAVInfovestaPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _CloseNAVInfovesta.Notes);
                                cmd.Parameters.AddWithValue("@PK", _CloseNAVInfovesta.CloseNAVInfovestaPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _CloseNAVInfovesta.HistoryPK);
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

        public void CloseNAVInfovesta_Approved(CloseNAVInfovesta _CloseNAVInfovesta)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update CloseNAVInfovesta set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,LastUpdate=@lastupdate " +
                            "where CloseNAVInfovestaPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _CloseNAVInfovesta.CloseNAVInfovestaPK);
                        cmd.Parameters.AddWithValue("@historyPK", _CloseNAVInfovesta.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _CloseNAVInfovesta.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update CloseNAVInfovesta set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where CloseNAVInfovestaPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _CloseNAVInfovesta.CloseNAVInfovestaPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _CloseNAVInfovesta.ApprovedUsersID);
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

        public void CloseNAVInfovesta_Reject(CloseNAVInfovesta _CloseNAVInfovesta)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update CloseNAVInfovesta set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where CloseNAVInfovestaPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _CloseNAVInfovesta.CloseNAVInfovestaPK);
                        cmd.Parameters.AddWithValue("@historyPK", _CloseNAVInfovesta.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _CloseNAVInfovesta.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update CloseNAVInfovesta set status= 2,LastUpdate=@LastUpdate  where CloseNAVInfovestaPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _CloseNAVInfovesta.CloseNAVInfovestaPK);
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

        public void CloseNAVInfovesta_Void(CloseNAVInfovesta _CloseNAVInfovesta)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update CloseNAVInfovesta set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where CloseNAVInfovestaPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _CloseNAVInfovesta.CloseNAVInfovestaPK);
                        cmd.Parameters.AddWithValue("@historyPK", _CloseNAVInfovesta.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _CloseNAVInfovesta.VoidUsersID);
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


        public string ImportCloseNAVInfovesta(string _fileSource, string _userID)
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
                            cmd1.CommandText = "truncate table CloseNAVInfovestaTemp";
                            cmd1.ExecuteNonQuery();
                        }
                    }

                    // import data ke temp dulu
                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(Tools.conString, SqlBulkCopyOptions.TableLock))
                    {
                        bulkCopy.DestinationTableName = "dbo.CloseNAVInfovestaTemp";
                        bulkCopy.WriteToServer(CreateDataTableFromCloseNAVInfovestaTempExcelFile(_fileSource));
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
from CloseNAVInfovesta A
inner join CloseNAVInfovestaTemp C on C.CloseNAVInfovestaTempPK = A.CloseNAVInfovestaPK
where A.Status in (1,2) and A.Tanggal = Cast( C.Tanggal as date)

declare @MaxCloseNAVInfovestaPK int

select @MaxCloseNAVInfovestaPK = max(CloseNAVInfovestaPK) from CloseNAVInfovesta

set @MaxCloseNAVInfovestaPK = isnull(@MaxCloseNAVInfovestaPK,0)


insert into CloseNAVInfovesta (CloseNAVInfovestaPK,HistoryPK,Status,Nama,Tanggal,PertumbuhanReturn,EntryUsersID,EntryTime,LastUpdate)
select ROW_NUMBER() over (order by A.CloseNAVInfovestaTempPK ) + @MaxCloseNAVInfovestaPK,1,2,A.Nama,cast( A.Tanggal as date),A.PertumbuhanReturn,@UserID,@TimeNow,@TimeNow from CloseNAVInfovestaTemp A

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
                                    _msg = "Import Close Nav Infovesta Success"; //Convert.ToString(dr[""]);
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
        private DataTable CreateDataTableFromCloseNAVInfovestaTempExcelFile(string _path)
        {
            try
            {
                using (DataTable dt = new DataTable())
                {
                    DataColumn dc;
                    DataRow dr;

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Int32");
                    dc.ColumnName = "CloseNAVInfovestaTempPK";
                    dc.Unique = false;
                    dc.AutoIncrement = true;
                    dc.AutoIncrementSeed = 1;
                    dc.AutoIncrementStep = 1;
                    dt.Columns.Add(dc);


                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Nama";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Tanggal";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Decimal");
                    dc.ColumnName = "PertumbuhanReturn";
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
                                dr["Nama"] = "";
                            else
                                dr["Nama"] = worksheet.Cells[i, 1].Value.ToString();

                            if (worksheet.Cells[i, 2].Value == null)
                                dr["Tanggal"] = "";
                            else
                                dr["Tanggal"] = worksheet.Cells[i, 2].Value.ToString();

                            if (worksheet.Cells[i, 3].Value == null)
                                dr["PertumbuhanReturn"] = 0;
                            else
                                dr["PertumbuhanReturn"] = worksheet.Cells[i, 3].Value.ToString();




                            if (dr["Nama"].Equals(null) != true ||
                                dr["Tanggal"].Equals(null) != true ||
                                dr["PertumbuhanReturn"].Equals(null) != true
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
