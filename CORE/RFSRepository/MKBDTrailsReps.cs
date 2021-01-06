using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using RFSModel;
using RFSUtility;
using System.Data.SqlClient;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.IO;
using System.Drawing;



namespace RFSRepository
{
    public class MKBDTrailsReps
    {
        Host _host = new Host();

        //1
        string _insertCommand = "INSERT INTO [dbo].[MKBDTrails] " +
                            "([MKBDTrailsPK],[HistoryPK],[Status],[BitValidate],[ValueDate],[LogMessages],[LastUsersToText],[ToTextTime],[GenerateToTextCount],";
        string _paramaterCommand = "@BitValidate,@ValueDate,@LogMessages,@LastUsersToText,@ToTextTime,@GenerateToTextCount,";
        //2
        private MKBDTrails setMKBDTrails(SqlDataReader dr)
        {
            MKBDTrails M_MKBDTrails = new MKBDTrails();
            M_MKBDTrails.MKBDTrailsPK = Convert.ToInt32(dr["MKBDTrailsPK"]);
            M_MKBDTrails.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_MKBDTrails.Status = Convert.ToInt32(dr["Status"]);
            M_MKBDTrails.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_MKBDTrails.Notes = Convert.ToString(dr["Notes"]);
            M_MKBDTrails.BitValidate = Convert.ToBoolean(dr["BitValidate"]);
            M_MKBDTrails.Row113B = Convert.ToDecimal(dr["Row113B"]);
            M_MKBDTrails.Row173B = Convert.ToDecimal(dr["Row173B"]);
            M_MKBDTrails.Row104G = Convert.ToDecimal(dr["Row104G"]);
            M_MKBDTrails.ValueDate = dr["ValueDate"].ToString();
            M_MKBDTrails.LogMessages = dr["LogMessages"].ToString();
            M_MKBDTrails.LastUsersToText = dr["LastUsersToText"].ToString();
            M_MKBDTrails.ToTextTime = dr["ToTextTime"].ToString();
            M_MKBDTrails.GenerateToTextCount = Convert.ToInt32(dr["GenerateToTextCount"]);
            M_MKBDTrails.EntryUsersID = dr["EntryUsersID"].ToString();
            M_MKBDTrails.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_MKBDTrails.VoidUsersID = dr["VoidUsersID"].ToString();
            M_MKBDTrails.EntryTime = dr["EntryTime"].ToString();
            M_MKBDTrails.ApprovedTime = dr["ApprovedTime"].ToString();
            M_MKBDTrails.VoidTime = dr["VoidTime"].ToString();
            M_MKBDTrails.DBUserID = dr["DBUserID"].ToString();
            M_MKBDTrails.DBTerminalID = dr["DBTerminalID"].ToString();
            M_MKBDTrails.LastUpdate = dr["LastUpdate"].ToString();
            M_MKBDTrails.LastUpdateDB = dr["LastUpdateDB"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["LastUpdateDB"]);
            return M_MKBDTrails;
        }

        //3
        public List<MKBDTrails> MKBDTrails_Select(int _status)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {

                    DbCon.Open();
                    List<MKBDTrails> L_MKBDTrails = new List<MKBDTrails>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {


                        if (_status != 9)
                        {
                            cmd.CommandText = "Select Select case when status=1 then 'PENDING' else Case When status = 2 then 'APPROVED' else Case when Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,* from MKBDTrails where status = @status";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = "Select Select case when status=1 then 'PENDING' else Case When status = 2 then 'APPROVED' else Case when Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,* from MKBDTrails";
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_MKBDTrails.Add(setMKBDTrails(dr));
                                }
                            }
                            return L_MKBDTrails;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public List<MKBDTrails> MKBDTrails_SelectMKBDTrailsDateFromTo(int _status, DateTime _dateFrom, DateTime _dateTo)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<MKBDTrails> L_MKBDTrails = new List<MKBDTrails>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        if (_status != 9)
                        {
                            cmd.CommandText = "Select case when MT.status=1 then 'PENDING' else Case When MT.status = 2 then 'APPROVED' else Case when MT.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc, M1.[113B] Row113B,M2.[173B] Row173B,M9.[104G] Row104G,* from MKBDTrails MT " +
                            " left join MKBD01 M1 on MT.MKBDTrailsPK = M1.MKBDTrailsPK " +
                            " left join MKBD02 M2 on MT.MKBDTrailsPK = M2.MKBDTrailsPK " +
                            " left join MKBD09 M9 on MT.MKBDTrailsPK = M9.MKBDTrailsPK " +
                            " where status = @status and ValueDate between @DateFrom and @DateTo ";
                            cmd.Parameters.AddWithValue("@Status", _status);
                        }
                        else
                        {
                            cmd.CommandText = "Select case when MT.status=1 then 'PENDING' else Case When MT.status = 2 then 'APPROVED' else Case when MT.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc, M1.[113B] Row113B,M2.[173B] Row173B,M9.[104G] Row104G,* from MKBDTrails MT " +
                         " left join MKBD01 M1 on MT.MKBDTrailsPK = M1.MKBDTrailsPK " +
                         " left join MKBD02 M2 on MT.MKBDTrailsPK = M2.MKBDTrailsPK " +
                         " left join MKBD09 M9 on MT.MKBDTrailsPK = M9.MKBDTrailsPK " +
                         " where ValueDate between @DateFrom and @DateTo ";
                        }
                        cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _dateTo);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_MKBDTrails.Add(setMKBDTrails(dr));
                                }
                            }
                            return L_MKBDTrails;
                        }
                    }
                }

            }
            catch (Exception err)
            {
                throw err;
            }

        }

        //4
        public MKBDTrails MKBDTrails_SelectByMKBDTrailsPK(int _MKBDTrailsPK)
        {

            try
            {

                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {


                        cmd.CommandText = "Select * From MKBDTrails " +
                            "Where MKBDTrailsPK= @MKBDTrailsPK and status=4";
                        cmd.Parameters.AddWithValue("@MKBDTrailsPK", _MKBDTrailsPK);


                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return setMKBDTrails(dr);
                            }
                            return null;
                        }
                    }

                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        //5
        public void MKBDTrails_Add(MKBDTrails _MKBDTrails, bool _havePrivillege)
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
                                 "Select isnull(max(MKBDTrailsPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@LastUpdate from MKBDTrails";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _MKBDTrails.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);

                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[LastUpdate])" +
                                "Select isnull(max(MKBDTrailsPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@LastUpdate from MKBDTrails";
                        }
                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@BitValidate", _MKBDTrails.BitValidate);
                        cmd.Parameters.AddWithValue("@ValueDate", _MKBDTrails.ValueDate);
                        cmd.Parameters.AddWithValue("@LogMessages", _MKBDTrails.LogMessages);
                        cmd.Parameters.AddWithValue("@LastUsersToText", _MKBDTrails.LastUsersToText);
                        cmd.Parameters.AddWithValue("@ToTextTime", _MKBDTrails.ToTextTime);
                        cmd.Parameters.AddWithValue("@GenerateToTextCount", _MKBDTrails.GenerateToTextCount);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _MKBDTrails.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _dateTimeNow);
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

        //7
        public void MKBDTrails_Approved(MKBDTrails _MKBDTrails)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update MKBDTrails set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,LastUpdate=@LastUpdate " +
                            "where MKBDTrailsPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _MKBDTrails.MKBDTrailsPK);
                        cmd.Parameters.AddWithValue("@historyPK", _MKBDTrails.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _MKBDTrails.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update MKBDTrails set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where MKBDTrailsPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _MKBDTrails.MKBDTrailsPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _MKBDTrails.ApprovedUsersID);
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

        //8
        public void MKBDTrails_Reject(MKBDTrails _MKBDTrails)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update MKBDTrails set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where MKBDTrailsPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _MKBDTrails.MKBDTrailsPK);
                        cmd.Parameters.AddWithValue("@historyPK", _MKBDTrails.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _MKBDTrails.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update MKBDTrails set status= 2,LastUpdate=@LastUpdate where MKBDTrailsPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _MKBDTrails.MKBDTrailsPK);
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

        //9
        public void MKBDTrails_Void(MKBDTrails _MKBDTrails)
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
                            @"

                            Declare @ValueDate datetime

                            select @ValueDate = ValueDate from MKBDTrails where MKBDTrailsPK = @PK and historypk = @historyPK 

                            update MKBDTrails set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@lastUpdate 
                            where MKBDTrailsPK = @PK and historypk = @historyPK


                            delete mkbd01 where date = @valuedate
                            delete mkbd02 where date = @valuedate
                            delete mkbd03 where date = @valuedate
                            delete mkbd04 where date = @valuedate
                            delete mkbd05 where date = @valuedate
                            delete mkbd06 where date = @valuedate
                            delete mkbd06Detail where date = @valuedate
                            delete mkbd07 where date = @valuedate
                            delete mkbd08 where date = @valuedate
                            delete mkbd09 where date = @valuedate
                            delete mkbd510A where date = @valuedate
                            delete mkbd510B where date = @valuedate
                            delete mkbd510C where date = @valuedate
                            delete mkbd510D where date = @valuedate
                            delete mkbd510E where date = @valuedate
                            delete mkbd510F where date = @valuedate
                            delete mkbd510G where date = @valuedate
                            delete mkbd510H where date = @valuedate
                            delete mkbd510I where date = @valuedate
                            delete RL504 where date = @valuedate
                            delete RL510ARepo where date = @valuedate
                            delete RL510BReverseRepo where date = @valuedate
                            delete RL510CBond where date = @valuedate
                            delete RL510CDeposito where date = @valuedate
                            delete RL510CEquity where date = @valuedate
                            delete RL510CSbn where date = @valuedate 


                        ";
                        cmd.Parameters.AddWithValue("@PK", _MKBDTrails.MKBDTrailsPK);
                        cmd.Parameters.AddWithValue("@historyPK", _MKBDTrails.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _MKBDTrails.VoidUsersID);
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

        public string MKBDTrails_ToTextFromTo(string _userID, MKBDTrails _mkbdTrails)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                DateTime _dateCounter = _mkbdTrails.DateFrom;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        string _dateFolder = DateTime.Now.ToString("MMddyyyy");
                        string _folder = Tools.MKBDTextFolder;
                        string _subFolder = Tools.MKBDTextPath + "NotepadFile\\" + _dateFolder;
                        string _filePathZIP = Tools.MKBDTextPath + "NotepadFile\\" + _dateFolder + ".zip";
                        Tools.CreateFolder(_folder, _subFolder);


                        while (_dateCounter <= _mkbdTrails.DateTo)
                        {

                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandText = "GeneratemkbdtotextFromTo";
                            cmd.Parameters.Clear();
                            cmd.CommandTimeout = 0;


                            cmd.Parameters.AddWithValue("@Date", _dateCounter);
                            cmd.Parameters.AddWithValue("@VersionMode", _mkbdTrails.VersionMode);
                            cmd.Parameters.AddWithValue("@UsersID", _userID);
                            cmd.Parameters.AddWithValue("@TimeNow", _datetimeNow);
                            using (SqlDataReader dr = cmd.ExecuteReader())
                            {
                                if (dr.HasRows)
                                {
                                    string filePath = Tools.MKBDTextPath + "NotepadFile\\" + _dateFolder + "\\" + _host.Get_MKBDCode() + _dateCounter.ToString("yyMMdd") + ".mkb";
                                    FileInfo txtFile = new FileInfo(filePath);
                                    if (txtFile.Exists)
                                    {
                                        txtFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                    }

                                    using (System.IO.StreamWriter file = new System.IO.StreamWriter(filePath))
                                    {
                                        while (dr.Read())
                                        {
                                            file.WriteLine(Convert.ToString(dr["ResultText"]));
                                        }
                                    }
                                }
                            }

                            _dateCounter = _dateCounter.AddDays(1);
                        }
                        Tools.CreateFolderToZIP(Tools.MKBDTextPath + "NotepadFile\\" + _dateFolder);
                        return Tools.HtmlMKBDTextPath + "NotepadFile\\" + _dateFolder + ".zip";

                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public string MKBDTrails_ToExcel(string _userID, MKBDTrails _mKBDTrails)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                string _companyName = _host.Get_CompanyName();
                string _directorName = _host.Get_DirectorName();
                string _date = "";
                using (SqlConnection DbConDate = new SqlConnection(Tools.conString))
                {
                    DbConDate.Open();
                    using (SqlCommand cmdDate = DbConDate.CreateCommand())
                    {
                        cmdDate.CommandText = "Select ValueDate from MKBDTrails Where MKBDTrailsPK = @MKBDTrailsPK and status not in (3,4)";
                        cmdDate.Parameters.AddWithValue("@MKBDTrailsPK", _mKBDTrails.MKBDTrailsPK);
                        using (SqlDataReader drDate = cmdDate.ExecuteReader())
                        {
                            if (drDate.HasRows)
                            {
                                drDate.Read();

                                //_date = Convert.ToString(drDate["ValueDate"]).Substring(0, 10);
                                _date = Convert.ToDateTime(drDate["ValueDate"]).ToShortDateString();

                            }

                        }
                    }
                }
                string FilePath = Tools.MKBDExcelPath + _host.Get_MKBDCode() + _host.Get_ValueDateByMKBDTrails(_mKBDTrails.MKBDTrailsPK).ToString("yyMMdd") + ".xlsx";
                File.Copy(Tools.MKBDExcelPath + "NEWMKBDTemplate.xlsx", FilePath, true);
                FileInfo existingFile = new FileInfo(FilePath);
                using (ExcelPackage package = new ExcelPackage(existingFile))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[1];




                    using (SqlConnection DbConParent01 = new SqlConnection(Tools.conString))
                    {
                        DbConParent01.Open();
                        using (SqlConnection DbCon01 = new SqlConnection(Tools.conString))
                        {
                            DbCon01.Open();
                            using (SqlCommand cmdParent01 = DbConParent01.CreateCommand())
                            {
                                cmdParent01.CommandText = "Select * from MKBDToExcelSetup Where MKBDName = 'MKBD01'";
                                using (SqlDataReader drParent01 = cmdParent01.ExecuteReader())
                                {
                                    if (drParent01.HasRows)
                                    {
                                        using (SqlCommand cmd01 = DbCon01.CreateCommand())
                                        {
                                            cmd01.CommandText = "Select * from MKBD01 Where MKBDTrailsPK = @MKBDTrailsPK";
                                            cmd01.Parameters.AddWithValue("@MKBDTrailsPK", _mKBDTrails.MKBDTrailsPK);

                                            using (SqlDataReader dr01 = cmd01.ExecuteReader())
                                            {
                                                if (dr01.HasRows)
                                                {
                                                    dr01.Read();
                                                    while (drParent01.Read())
                                                    {
                                                        string _pos = Convert.ToString(drParent01["col"]) + Convert.ToString(drParent01["row"]);
                                                        string _source = Convert.ToString(drParent01["source"]);
                                                        worksheet.Cells[_pos].Value = Convert.ToDecimal(dr01[_source]);
                                                    }

                                                    worksheet.Cells[6, 7].Value = _companyName;
                                                    worksheet.Cells[7, 7].Value = _date;
                                                    worksheet.Cells[8, 7].Value = _directorName;
                                                }


                                            }
                                        }
                                    }
                                }
                            }
                        }

                    }

                    // MKBD02
                    ExcelWorksheet worksheet2 = package.Workbook.Worksheets[2];
                    using (SqlConnection DbConParent02 = new SqlConnection(Tools.conString))
                    {
                        DbConParent02.Open();
                        using (SqlConnection DbCon02 = new SqlConnection(Tools.conString))
                        {
                            DbCon02.Open();
                            using (SqlCommand cmdParent02 = DbConParent02.CreateCommand())
                            {
                                cmdParent02.CommandText = "Select * from MKBDToExcelSetup Where MKBDName = 'MKBD02'";
                                using (SqlDataReader drParent02 = cmdParent02.ExecuteReader())
                                {
                                    if (drParent02.HasRows)
                                    {
                                        using (SqlCommand cmd02 = DbCon02.CreateCommand())
                                        {
                                            cmd02.CommandText = "Select * from MKBD02 Where MKBDTrailsPK = @MKBDTrailsPK";
                                            cmd02.Parameters.AddWithValue("@MKBDTrailsPK", _mKBDTrails.MKBDTrailsPK);

                                            using (SqlDataReader dr02 = cmd02.ExecuteReader())
                                            {
                                                if (dr02.HasRows)
                                                {
                                                    dr02.Read();
                                                    while (drParent02.Read())
                                                    {
                                                        string _pos = Convert.ToString(drParent02["col"]) + Convert.ToString(drParent02["row"]);
                                                        string _source = Convert.ToString(drParent02["source"]);
                                                        worksheet2.Cells[_pos].Value = Convert.ToDecimal(dr02[_source]);
                                                    }

                                                    worksheet2.Cells[6, 7].Value = _companyName;
                                                    worksheet2.Cells[7, 7].Value = _date;
                                                    worksheet2.Cells[8, 7].Value = _directorName;

                                                }
                                            }

                                        }
                                    }
                                }
                            }
                        }
                    }

                    // MKBD03
                    ExcelWorksheet worksheet3 = package.Workbook.Worksheets[3];
                    using (SqlConnection DbConParent03 = new SqlConnection(Tools.conString))
                    {
                        DbConParent03.Open();
                        using (SqlConnection DbCon03 = new SqlConnection(Tools.conString))
                        {
                            DbCon03.Open();
                            using (SqlCommand cmdParent03 = DbConParent03.CreateCommand())
                            {
                                cmdParent03.CommandText = "Select * from MKBDToExcelSetup Where MKBDName = 'MKBD03'";
                                using (SqlDataReader drParent03 = cmdParent03.ExecuteReader())
                                {
                                    if (drParent03.HasRows)
                                    {
                                        using (SqlCommand cmd03 = DbCon03.CreateCommand())
                                        {
                                            cmd03.CommandText = "Select * from MKBD03 Where MKBDTrailsPK = @MKBDTrailsPK";
                                            cmd03.Parameters.AddWithValue("@MKBDTrailsPK", _mKBDTrails.MKBDTrailsPK);

                                            using (SqlDataReader dr03 = cmd03.ExecuteReader())
                                            {
                                                if (dr03.HasRows)
                                                {
                                                    dr03.Read();
                                                    while (drParent03.Read())
                                                    {
                                                        string _pos = Convert.ToString(drParent03["col"]) + Convert.ToString(drParent03["row"]);
                                                        string _source = Convert.ToString(drParent03["source"]);
                                                        worksheet3.Cells[_pos].Value = Convert.ToDecimal(dr03[_source]);
                                                    }

                                                    worksheet3.Cells[6, 9].Value = _companyName;
                                                    worksheet3.Cells[7, 9].Value = _date;
                                                    worksheet3.Cells[8, 9].Value = _directorName;

                                                }
                                            }

                                        }
                                    }
                                }
                            }
                        }
                    }


                    // MKBD04
                    ExcelWorksheet worksheet4 = package.Workbook.Worksheets[4];
                    using (SqlConnection DbCon04 = new SqlConnection(Tools.conString))
                    {
                        DbCon04.Open();
                        using (SqlCommand cmd04 = DbCon04.CreateCommand())
                        {

                            if (_mKBDTrails.VersionMode == "Internal")
                            {
                                cmd04.CommandText = @"
                                                       select No,A,B,C,D,E,F,G,H from MKBD04LIFO where Date = @Date

                                                                ";
                            }
                            else
                            {
                                cmd04.CommandText = "select No,A,B,C,D,E,F,G,H from MKBD04 where MKBDTrailsPK = @MKBDTrailsPK ";
                            }


                            cmd04.Parameters.AddWithValue("@MKBDTrailsPK", _mKBDTrails.MKBDTrailsPK);
                            cmd04.Parameters.AddWithValue("@Date", _date);

                            using (SqlDataReader dr04 = cmd04.ExecuteReader())
                            {
                                int incRowExcel = 11;
                                while (dr04.Read())
                                {
                                    int incColExcel = 1;
                                    int _startRow = 11;
                                    for (int inc1 = 0; inc1 < dr04.FieldCount; inc1++)
                                    {
                                        if (inc1 == 4 || inc1 == 5 || inc1 == 7 || inc1 == 8)
                                        {
                                            worksheet4.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr04.GetValue(inc1));
                                            worksheet4.Cells[incRowExcel, incColExcel].Style.Numberformat.Format = "#,##0.00";
                                        }
                                        else
                                        {
                                            worksheet4.Cells[incRowExcel, incColExcel].Value = dr04.GetValue(inc1).ToString();
                                        }

                                        incColExcel++;
                                    }
                                    worksheet4.Cells["A" + _startRow + ":I" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                    worksheet4.Cells["A" + _startRow + ":I" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                    worksheet4.Cells["A" + _startRow + ":I" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                    worksheet4.Cells["A" + _startRow + ":I" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                    incRowExcel++;
                                    _startRow++;
                                }
                                worksheet4.Cells[6, 5].Value = _companyName;
                                worksheet4.Cells[7, 5].Value = _date;
                                worksheet4.Cells[8, 5].Value = _directorName;

                            }
                        }

                    }


                    // MKBD05
                    ExcelWorksheet worksheet5 = package.Workbook.Worksheets[5];
                    using (SqlConnection DbCon05 = new SqlConnection(Tools.conString))
                    {
                        DbCon05.Open();

                        using (SqlCommand cmd05 = DbCon05.CreateCommand())
                        {
                            cmd05.CommandText = "select No,A,B,C,D,E,F,G,H from MKBD05 where MKBDTrailsPK = @MKBDTrailsPK ";
                            cmd05.Parameters.AddWithValue("@MKBDTrailsPK", _mKBDTrails.MKBDTrailsPK);

                            using (SqlDataReader dr05 = cmd05.ExecuteReader())
                            {
                                int incRowExcel = 10;
                                int _startRow = 11;
                                while (dr05.Read())
                                {
                                    int incColExcel = 1;
                                    for (int inc1 = 0; inc1 < dr05.FieldCount; inc1++)
                                    {
                                        worksheet5.Cells[incRowExcel, incColExcel].Value = dr05.GetValue(inc1).ToString();
                                        incColExcel++;
                                    }
                                    worksheet5.Cells["A" + _startRow + ":I" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                    worksheet5.Cells["A" + _startRow + ":I" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                    worksheet5.Cells["A" + _startRow + ":I" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                    worksheet5.Cells["A" + _startRow + ":I" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                    incRowExcel++;
                                    _startRow++;
                                }
                                worksheet5.Cells[6, 5].Value = _companyName;
                                worksheet5.Cells[7, 5].Value = _date;
                                worksheet5.Cells[8, 5].Value = _directorName;

                            }
                        }



                    }


                    // MKBD06
                    ExcelWorksheet worksheet6 = package.Workbook.Worksheets[6];
                    using (SqlConnection DbConParent06 = new SqlConnection(Tools.conString))
                    {
                        DbConParent06.Open();
                        using (SqlConnection DbCon06 = new SqlConnection(Tools.conString))
                        {
                            DbCon06.Open();
                            using (SqlCommand cmdParent06 = DbConParent06.CreateCommand())
                            {
                                cmdParent06.CommandText = "Select * from MKBDToExcelSetup Where MKBDName = 'MKBD06'";
                                using (SqlDataReader drParent06 = cmdParent06.ExecuteReader())
                                {
                                    if (drParent06.HasRows)
                                    {
                                        using (SqlCommand cmd06 = DbCon06.CreateCommand())
                                        {
                                            cmd06.CommandText = "Select * from MKBD06 Where MKBDTrailsPK = @MKBDTrailsPK";
                                            cmd06.Parameters.AddWithValue("@MKBDTrailsPK", _mKBDTrails.MKBDTrailsPK);

                                            using (SqlDataReader dr06 = cmd06.ExecuteReader())
                                            {
                                                if (dr06.HasRows)
                                                {
                                                    dr06.Read();

                                                    while (drParent06.Read())
                                                    {

                                                        string _pos = Convert.ToString(drParent06["col"]) + Convert.ToString(drParent06["row"]);
                                                        string _source = Convert.ToString(drParent06["source"]);
                                                        worksheet6.Cells[_pos].Value = Convert.ToString(dr06[_source]);
                                                    }
                                                    worksheet6.Cells[6, 9].Value = _companyName;
                                                    worksheet6.Cells[7, 9].Value = _date;
                                                    worksheet6.Cells[8, 9].Value = _directorName;


                                                }
                                            }

                                        }
                                    }
                                }
                            }
                        }
                    }

                    // MKBD06D
                    ExcelWorksheet worksheet6D = package.Workbook.Worksheets[6];
                    using (SqlConnection DbCon06D = new SqlConnection(Tools.conString))
                    {
                        DbCon06D.Open();

                        using (SqlCommand cmd06D = DbCon06D.CreateCommand())
                        {
                            cmd06D.CommandText = "select No,A + ' ' + isnull(B.Name,'') A,'','','','',B,C,D,E,F from MKBD06Detail A left join Bank B on ltrim(rtrim(A.A)) = Ltrim(rtrim(B.RTGSCode)) and B.status in (1,2) where MKBDTrailsPK = @MKBDTrailsPK ";
                            cmd06D.Parameters.AddWithValue("@MKBDTrailsPK", _mKBDTrails.MKBDTrailsPK);

                            using (SqlDataReader dr06D = cmd06D.ExecuteReader())
                            {
                                int incRowExcel = 31;
                                while (dr06D.Read())
                                {
                                    int incColExcel = 1;
                                    int _startRow = 31;
                                    for (int inc1 = 0; inc1 < dr06D.FieldCount; inc1++)
                                    {
                                        if (incColExcel == 10 || incColExcel == 11)
                                        {
                                            worksheet6D.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr06D[inc1]);
                                            worksheet6D.Cells[incRowExcel, incColExcel].Style.Numberformat.Format = "#,##0.00";
                                        }
                                        else
                                        {
                                            worksheet6D.Cells[incRowExcel, incColExcel].Value = dr06D.GetValue(inc1).ToString();
                                        }

                                        incColExcel++;
                                    }
                                    worksheet6D.Cells["A" + _startRow + ":K" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                    worksheet6D.Cells["A" + _startRow + ":K" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                    worksheet6D.Cells["A" + _startRow + ":K" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                    worksheet6D.Cells["A" + _startRow + ":K" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                    incRowExcel++;
                                }
                            }
                        }

                    }

                    // MKBD07
                    ExcelWorksheet worksheet7 = package.Workbook.Worksheets[7];
                    using (SqlConnection DbConParent07 = new SqlConnection(Tools.conString))
                    {
                        DbConParent07.Open();
                        using (SqlConnection DbCon07 = new SqlConnection(Tools.conString))
                        {
                            DbCon07.Open();
                            using (SqlCommand cmdParent07 = DbConParent07.CreateCommand())
                            {
                                cmdParent07.CommandText = "Select * from MKBDToExcelSetup Where MKBDName = 'MKBD07'";
                                using (SqlDataReader drParent07 = cmdParent07.ExecuteReader())
                                {
                                    if (drParent07.HasRows)
                                    {
                                        using (SqlCommand cmd07 = DbCon07.CreateCommand())
                                        {
                                            cmd07.CommandText = "Select * from MKBD07 Where MKBDTrailsPK = @MKBDTrailsPK";
                                            cmd07.Parameters.AddWithValue("@MKBDTrailsPK", _mKBDTrails.MKBDTrailsPK);

                                            using (SqlDataReader dr07 = cmd07.ExecuteReader())
                                            {
                                                if (dr07.HasRows)
                                                {
                                                    dr07.Read();
                                                    while (drParent07.Read())
                                                    {
                                                        string _pos = Convert.ToString(drParent07["col"]) + Convert.ToString(drParent07["row"]);
                                                        string _source = Convert.ToString(drParent07["source"]);
                                                        worksheet7.Cells[_pos].Value = Convert.ToDecimal(dr07[_source]);
                                                        worksheet7.Cells[_pos].Style.Numberformat.Format = "#,##0.00";
                                                    }
                                                    worksheet7.Cells[6, 9].Value = _companyName;
                                                    worksheet7.Cells[7, 9].Value = _date;
                                                    worksheet7.Cells[8, 9].Value = _directorName;

                                                }
                                            }

                                        }
                                    }
                                }
                            }
                        }
                    }
                    // MKBD08
                    ExcelWorksheet worksheet8 = package.Workbook.Worksheets[8];
                    using (SqlConnection DbConParent08 = new SqlConnection(Tools.conString))
                    {
                        DbConParent08.Open();
                        using (SqlConnection DbCon08 = new SqlConnection(Tools.conString))
                        {
                            DbCon08.Open();
                            using (SqlCommand cmdParent08 = DbConParent08.CreateCommand())
                            {
                                cmdParent08.CommandText = "Select * from MKBDToExcelSetup Where MKBDName = 'MKBD08'";
                                using (SqlDataReader drParent08 = cmdParent08.ExecuteReader())
                                {
                                    if (drParent08.HasRows)
                                    {
                                        using (SqlCommand cmd08 = DbCon08.CreateCommand())
                                        {
                                            cmd08.CommandText = "Select * from MKBD08 Where MKBDTrailsPK = @MKBDTrailsPK";
                                            cmd08.Parameters.AddWithValue("@MKBDTrailsPK", _mKBDTrails.MKBDTrailsPK);

                                            using (SqlDataReader dr08 = cmd08.ExecuteReader())
                                            {
                                                if (dr08.HasRows)
                                                {
                                                    dr08.Read();
                                                    while (drParent08.Read())
                                                    {
                                                        string _pos = Convert.ToString(drParent08["col"]) + Convert.ToString(drParent08["row"]);
                                                        string _source = Convert.ToString(drParent08["source"]);
                                                        worksheet8.Cells[_pos].Value = Convert.ToDecimal(dr08[_source]);
                                                        worksheet8.Cells[_pos].Style.Numberformat.Format = "#,##0.00";
                                                    }
                                                    worksheet8.Cells[6, 9].Value = _companyName;
                                                    worksheet8.Cells[7, 9].Value = _date;
                                                    worksheet8.Cells[8, 9].Value = _directorName;

                                                }
                                            }

                                        }
                                    }
                                }
                            }
                        }
                    }
                    // MKBD09
                    ExcelWorksheet worksheet9 = package.Workbook.Worksheets[9];
                    using (SqlConnection DbConParent09 = new SqlConnection(Tools.conString))
                    {
                        DbConParent09.Open();
                        using (SqlConnection DbCon09 = new SqlConnection(Tools.conString))
                        {
                            DbCon09.Open();
                            using (SqlCommand cmdParent09 = DbConParent09.CreateCommand())
                            {
                                cmdParent09.CommandText = "Select * from MKBDToExcelSetup Where MKBDName = 'MKBD09'";
                                using (SqlDataReader drParent09 = cmdParent09.ExecuteReader())
                                {
                                    if (drParent09.HasRows)
                                    {
                                        using (SqlCommand cmd09 = DbCon09.CreateCommand())
                                        {
                                            cmd09.CommandText = "Select * from MKBD09 Where MKBDTrailsPK = @MKBDTrailsPK";
                                            cmd09.Parameters.AddWithValue("@MKBDTrailsPK", _mKBDTrails.MKBDTrailsPK);

                                            using (SqlDataReader dr09 = cmd09.ExecuteReader())
                                            {
                                                if (dr09.HasRows)
                                                {
                                                    dr09.Read();
                                                    while (drParent09.Read())
                                                    {
                                                        string _pos = Convert.ToString(drParent09["col"]) + Convert.ToString(drParent09["row"]);
                                                        string _source = Convert.ToString(drParent09["source"]);
                                                        worksheet9.Cells[_pos].Value = Convert.ToDecimal(dr09[_source]);
                                                        worksheet9.Cells[_pos].Style.Numberformat.Format = "#,##0.00";
                                                    }
                                                    worksheet9.Cells[6, 10].Value = _companyName;
                                                    worksheet9.Cells[7, 10].Value = _date;
                                                    worksheet9.Cells[8, 10].Value = _directorName;

                                                }
                                            }

                                        }
                                    }
                                }
                            }
                        }
                    }

                    // MKBD510A
                    ExcelWorksheet worksheet10A = package.Workbook.Worksheets[10];
                    using (SqlConnection DbCon10A = new SqlConnection(Tools.conString))
                    {
                        DbCon10A.Open();

                        using (SqlCommand cmd10A = DbCon10A.CreateCommand())
                        {
                            cmd10A.CommandText = "select A,B,C,D,E,F,G,H,I,J,K from MKBD510A where MKBDTrailsPK = @MKBDTrailsPK ";
                            cmd10A.Parameters.AddWithValue("@MKBDTrailsPK", _mKBDTrails.MKBDTrailsPK);

                            using (SqlDataReader dr10A = cmd10A.ExecuteReader())
                            {
                                int incRowExcel = 11;
                                while (dr10A.Read())
                                {
                                    int incColExcel = 1;
                                    int _startRow = 11;
                                    for (int inc1 = 0; inc1 < dr10A.FieldCount - 1; inc1++)
                                    {
                                        worksheet10A.Cells[incRowExcel, incColExcel].Value = dr10A.GetValue(inc1).ToString();
                                        incColExcel++;
                                    }
                                    worksheet10A.Cells["A" + _startRow + ":I" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                    worksheet10A.Cells["A" + _startRow + ":I" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                    worksheet10A.Cells["A" + _startRow + ":I" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                    worksheet10A.Cells["A" + _startRow + ":I" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                    incRowExcel++;
                                    _startRow++;
                                }
                                worksheet10A.Cells[5, 5].Value = _companyName;
                                worksheet10A.Cells[6, 5].Value = _date;
                                worksheet10A.Cells[7, 5].Value = _directorName;
                            }
                        }



                    }

                    // MKBD510B
                    ExcelWorksheet worksheet10B = package.Workbook.Worksheets[11];
                    using (SqlConnection DbCon10B = new SqlConnection(Tools.conString))
                    {
                        DbCon10B.Open();

                        using (SqlCommand cmd10B = DbCon10B.CreateCommand())
                        {
                            cmd10B.CommandText = "select A,B,C,D,E,F,G,H,I,J,K from MKBD510B where MKBDTrailsPK = @MKBDTrailsPK ";
                            cmd10B.Parameters.AddWithValue("@MKBDTrailsPK", _mKBDTrails.MKBDTrailsPK);

                            using (SqlDataReader dr10B = cmd10B.ExecuteReader())
                            {
                                int incRowExcel = 11;
                                while (dr10B.Read())
                                {
                                    int incColExcel = 1;
                                    int _startRow = 11;
                                    for (int inc1 = 0; inc1 < dr10B.FieldCount; inc1++)
                                    {
                                        worksheet10B.Cells[incRowExcel, incColExcel].Value = dr10B.GetValue(inc1).ToString();
                                        incColExcel++;
                                    }
                                    worksheet10A.Cells["A" + _startRow + ":I" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                    worksheet10A.Cells["A" + _startRow + ":I" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                    worksheet10A.Cells["A" + _startRow + ":I" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                    worksheet10A.Cells["A" + _startRow + ":I" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                    incRowExcel++;
                                    _startRow++;
                                }
                                worksheet10B.Cells[5, 5].Value = _companyName;
                                worksheet10B.Cells[6, 5].Value = _date;
                                worksheet10B.Cells[7, 5].Value = _directorName;
                            }
                        }



                    }

                    // MKBD510C
                    ExcelWorksheet worksheet10C = package.Workbook.Worksheets[12];
                    using (SqlConnection DbCon10C = new SqlConnection(Tools.conString))
                    {
                        DbCon10C.Open();

                        using (SqlCommand cmd10C = DbCon10C.CreateCommand())
                        {
                            cmd10C.CommandText = "select A,B,C,D,E,F,G,H,I,J,K from MKBD510C where MKBDTrailsPK = @MKBDTrailsPK ";
                            cmd10C.Parameters.AddWithValue("@MKBDTrailsPK", _mKBDTrails.MKBDTrailsPK);

                            using (SqlDataReader dr10C = cmd10C.ExecuteReader())
                            {
                                int incRowExcel = 11;
                                while (dr10C.Read())
                                {
                                    int incColExcel = 1;
                                    int _startRow = 11;
                                    for (int inc1 = 0; inc1 < dr10C.FieldCount; inc1++)
                                    {
                                        if (incColExcel == 5 || incColExcel == 6 || incColExcel == 7 || incColExcel == 8 || incColExcel == 10 || incColExcel == 11)
                                        {
                                            worksheet10C.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr10C[inc1]);
                                            worksheet10C.Cells[incRowExcel, incColExcel].Style.Numberformat.Format = "#,##0.00";
                                        }
                                        else
                                        {
                                            worksheet10C.Cells[incRowExcel, incColExcel].Value = dr10C.GetValue(inc1).ToString();
                                        }

                                        incColExcel++;
                                    }
                                    worksheet10A.Cells["A" + _startRow + ":K" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                    worksheet10A.Cells["A" + _startRow + ":K" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                    worksheet10A.Cells["A" + _startRow + ":K" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                    worksheet10A.Cells["A" + _startRow + ":K" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                    incRowExcel++;
                                }
                                worksheet10C.Cells[5, 5].Value = _companyName;
                                worksheet10C.Cells[6, 5].Value = _date;
                                worksheet10C.Cells[7, 5].Value = _directorName;

                            }
                        }


                    }

                    // MKBD510D
                    ExcelWorksheet worksheet10D = package.Workbook.Worksheets[13];
                    using (SqlConnection DbCon10D = new SqlConnection(Tools.conString))
                    {
                        DbCon10D.Open();

                        using (SqlCommand cmd10D = DbCon10D.CreateCommand())
                        {
                            cmd10D.CommandText = "select A,B,C,D,E,F,G,H from MKBD510D where MKBDTrailsPK = @MKBDTrailsPK ";
                            cmd10D.Parameters.AddWithValue("@MKBDTrailsPK", _mKBDTrails.MKBDTrailsPK);

                            using (SqlDataReader dr10D = cmd10D.ExecuteReader())
                            {
                                int incRowExcel = 11;
                                while (dr10D.Read())
                                {
                                    int incColExcel = 1;
                                    for (int inc1 = 0; inc1 < dr10D.FieldCount; inc1++)
                                    {
                                        worksheet10D.Cells[incRowExcel, incColExcel].Value = dr10D.GetValue(inc1).ToString();
                                        incColExcel++;
                                    }
                                    incRowExcel++;
                                }
                                worksheet10D.Cells[5, 5].Value = _companyName;
                                worksheet10D.Cells[6, 5].Value = _date;
                                worksheet10D.Cells[7, 5].Value = _directorName;


                            }
                        }



                    }

                    // MKBD510E
                    ExcelWorksheet worksheet10E = package.Workbook.Worksheets[14];
                    using (SqlConnection DbCon10E = new SqlConnection(Tools.conString))
                    {
                        DbCon10E.Open();

                        using (SqlCommand cmd10E = DbCon10E.CreateCommand())
                        {
                            cmd10E.CommandText = "select A,B,C,D,E,F from MKBD510E where MKBDTrailsPK = @MKBDTrailsPK ";
                            cmd10E.Parameters.AddWithValue("@MKBDTrailsPK", _mKBDTrails.MKBDTrailsPK);

                            using (SqlDataReader dr10E = cmd10E.ExecuteReader())
                            {
                                int incRowExcel = 11;
                                while (dr10E.Read())
                                {
                                    int incColExcel = 1;
                                    for (int inc1 = 0; inc1 < dr10E.FieldCount; inc1++)
                                    {
                                        worksheet10E.Cells[incRowExcel, incColExcel].Value = dr10E.GetValue(inc1).ToString();
                                        incColExcel++;
                                    }
                                    incRowExcel++;
                                }
                                worksheet10E.Cells[5, 5].Value = _companyName;
                                worksheet10E.Cells[6, 5].Value = _date;
                                worksheet10E.Cells[7, 5].Value = _directorName;

                            }
                        }



                    }

                    // MKBD510F
                    ExcelWorksheet worksheet10F = package.Workbook.Worksheets[15];
                    using (SqlConnection DbCon10F = new SqlConnection(Tools.conString))
                    {
                        DbCon10F.Open();

                        using (SqlCommand cmd10F = DbCon10F.CreateCommand())
                        {
                            cmd10F.CommandText = "select A,B,C,D,E,F,G,H,I,J from MKBD510F where MKBDTrailsPK = @MKBDTrailsPK ";
                            cmd10F.Parameters.AddWithValue("@MKBDTrailsPK", _mKBDTrails.MKBDTrailsPK);

                            using (SqlDataReader dr10F = cmd10F.ExecuteReader())
                            {
                                int incRowExcel = 11;
                                while (dr10F.Read())
                                {
                                    int incColExcel = 1;
                                    for (int inc1 = 0; inc1 < dr10F.FieldCount; inc1++)
                                    {
                                        worksheet10F.Cells[incRowExcel, incColExcel].Value = dr10F.GetValue(inc1).ToString();
                                        incColExcel++;
                                    }
                                    incRowExcel++;
                                }
                                worksheet10F.Cells[5, 5].Value = _companyName;
                                worksheet10F.Cells[6, 5].Value = _date;
                                worksheet10F.Cells[7, 5].Value = _directorName;


                            }
                        }



                    }

                    // MKBD510G
                    ExcelWorksheet worksheet10G = package.Workbook.Worksheets[16];
                    using (SqlConnection DbCon10G = new SqlConnection(Tools.conString))
                    {
                        DbCon10G.Open();

                        using (SqlCommand cmd10G = DbCon10G.CreateCommand())
                        {
                            cmd10G.CommandText = "select A,B,C,D,E,F,G,H,I from MKBD510G where MKBDTrailsPK = @MKBDTrailsPK ";
                            cmd10G.Parameters.AddWithValue("@MKBDTrailsPK", _mKBDTrails.MKBDTrailsPK);

                            using (SqlDataReader dr10G = cmd10G.ExecuteReader())
                            {
                                int incRowExcel = 11;
                                while (dr10G.Read())
                                {
                                    int incColExcel = 1;
                                    for (int inc1 = 0; inc1 < dr10G.FieldCount; inc1++)
                                    {
                                        worksheet10G.Cells[incRowExcel, incColExcel].Value = dr10G.GetValue(inc1).ToString();
                                        incColExcel++;
                                    }
                                    incRowExcel++;
                                }
                                worksheet10G.Cells[5, 5].Value = _companyName;
                                worksheet10G.Cells[6, 5].Value = _date;
                                worksheet10G.Cells[7, 5].Value = _directorName;

                            }
                        }



                    }

                    // MKBD510H
                    ExcelWorksheet worksheet10H = package.Workbook.Worksheets[17];
                    using (SqlConnection DbCon10H = new SqlConnection(Tools.conString))
                    {
                        DbCon10H.Open();

                        using (SqlCommand cmd10H = DbCon10H.CreateCommand())
                        {
                            cmd10H.CommandText = "select A,B,C,D,E,F,G from MKBD510H where MKBDTrailsPK = @MKBDTrailsPK ";
                            cmd10H.Parameters.AddWithValue("@MKBDTrailsPK", _mKBDTrails.MKBDTrailsPK);

                            using (SqlDataReader dr10H = cmd10H.ExecuteReader())
                            {
                                int incRowExcel = 11;
                                while (dr10H.Read())
                                {
                                    int incColExcel = 1;
                                    for (int inc1 = 0; inc1 < dr10H.FieldCount; inc1++)
                                    {
                                        worksheet10H.Cells[incRowExcel, incColExcel].Value = dr10H.GetValue(inc1).ToString();
                                        incColExcel++;
                                    }
                                    incRowExcel++;
                                }
                                worksheet10H.Cells[5, 5].Value = _companyName;
                                worksheet10H.Cells[6, 5].Value = _date;
                                worksheet10H.Cells[7, 5].Value = _directorName;

                            }
                        }



                    }

                    // MKBD510I
                    ExcelWorksheet worksheet10I = package.Workbook.Worksheets[18];
                    using (SqlConnection DbCon10I = new SqlConnection(Tools.conString))
                    {
                        DbCon10I.Open();

                        using (SqlCommand cmd10I = DbCon10I.CreateCommand())
                        {
                            cmd10I.CommandText = "select A,B,C,D,E,F,G from MKBD510I where MKBDTrailsPK = @MKBDTrailsPK ";
                            cmd10I.Parameters.AddWithValue("@MKBDTrailsPK", _mKBDTrails.MKBDTrailsPK);

                            using (SqlDataReader dr10I = cmd10I.ExecuteReader())
                            {
                                int incRowExcel = 11;
                                while (dr10I.Read())
                                {
                                    int incColExcel = 1;
                                    for (int inc1 = 0; inc1 < dr10I.FieldCount; inc1++)
                                    {
                                        worksheet10I.Cells[incRowExcel, incColExcel].Value = dr10I.GetValue(inc1).ToString();
                                        incColExcel++;
                                    }
                                    incRowExcel++;
                                }
                                worksheet10I.Cells[5, 5].Value = _companyName;
                                worksheet10I.Cells[6, 5].Value = _date;
                                worksheet10I.Cells[7, 5].Value = _directorName;

                            }
                        }



                    }
                    package.Save();
                    return Tools.HtmlMKBDExcelPath + _host.Get_MKBDCode() + _host.Get_ValueDateByMKBDTrails(_mKBDTrails.MKBDTrailsPK).ToString("yyMMdd") + ".xlsx";
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public string MKBDTrails_ToExcelFromTo(string _userID, MKBDTrails _mkbdTrails)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                DateTime _dateCounter = _mkbdTrails.DateFrom;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        string _dateFolder = DateTime.Now.ToString("MMddyyyy");
                        string _folder = Tools.MKBDExcelFolder;
                        string _subFolder = Tools.MKBDExcelPath + "ExcelFile\\" + _dateFolder;
                        string _filePathZIP = Tools.MKBDExcelPath + "ExcelFile\\" + _dateFolder + ".zip";
                        Tools.CreateFolder(_folder, _subFolder);
                        string _a;

                        while (_dateCounter <= _mkbdTrails.DateTo)
                        {

                            string _companyName = _host.Get_CompanyName();
                            string _directorName = _host.Get_DirectorName();
                            int _MKBDTrailsPK = 0;

                            using (SqlConnection DbConDate = new SqlConnection(Tools.conString))
                            {
                                DbConDate.Open();
                                using (SqlCommand cmdDate = DbConDate.CreateCommand())
                                {
                                    cmdDate.CommandText = "Select MKBDTrailsPK from MKBDTrails Where ValueDate = @Date and status not in (3,4)";
                                    cmdDate.Parameters.AddWithValue("@Date", _dateCounter);
                                    using (SqlDataReader drDate = cmdDate.ExecuteReader())
                                    {
                                        if (drDate.HasRows)
                                        {
                                            drDate.Read();

                                            //_date = Convert.ToString(drDate["ValueDate"]).Substring(0, 10);
                                            _MKBDTrailsPK = Convert.ToInt32(drDate["MKBDTrailsPK"]);

                                        }

                                    }
                                }
                            }
                            string FilePath = Tools.MKBDExcelPath + "ExcelFile\\" + _dateFolder + "\\" + _host.Get_MKBDCode() + _host.Get_ValueDateByMKBDTrails(_MKBDTrailsPK).ToString("yyMMdd") + ".xlsx";
                            File.Copy(Tools.MKBDExcelPath + "NEWMKBDTemplate.xlsx", FilePath, true);
                            FileInfo existingFile = new FileInfo(FilePath);
                            using (ExcelPackage package = new ExcelPackage(existingFile))
                            {
                                ExcelWorksheet worksheet = package.Workbook.Worksheets[1];

                                using (SqlConnection DbConParent01 = new SqlConnection(Tools.conString))
                                {
                                    DbConParent01.Open();
                                    using (SqlConnection DbCon01 = new SqlConnection(Tools.conString))
                                    {
                                        DbCon01.Open();
                                        using (SqlCommand cmdParent01 = DbConParent01.CreateCommand())
                                        {
                                            cmdParent01.CommandText = "Select * from MKBDToExcelSetup Where MKBDName = 'MKBD01'";
                                            using (SqlDataReader drParent01 = cmdParent01.ExecuteReader())
                                            {
                                                if (drParent01.HasRows)
                                                {
                                                    using (SqlCommand cmd01 = DbCon01.CreateCommand())
                                                    {
                                                        cmd01.CommandText = "Select * from MKBD01 Where MKBDTrailsPK = @MKBDTrailsPK";
                                                        cmd01.Parameters.AddWithValue("@MKBDTrailsPK", _MKBDTrailsPK);

                                                        using (SqlDataReader dr01 = cmd01.ExecuteReader())
                                                        {
                                                            if (dr01.HasRows)
                                                            {
                                                                dr01.Read();
                                                                while (drParent01.Read())
                                                                {
                                                                    string _pos = Convert.ToString(drParent01["col"]) + Convert.ToString(drParent01["row"]);
                                                                    string _source = Convert.ToString(drParent01["source"]);
                                                                    worksheet.Cells[_pos].Value = Convert.ToDecimal(dr01[_source]);
                                                                }

                                                                worksheet.Cells[6, 7].Value = _companyName;
                                                                worksheet.Cells[7, 7].Value = _dateCounter;
                                                                worksheet.Cells[8, 7].Value = _directorName;
                                                            }


                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }

                                }

                                // MKBD02
                                ExcelWorksheet worksheet2 = package.Workbook.Worksheets[2];
                                using (SqlConnection DbConParent02 = new SqlConnection(Tools.conString))
                                {
                                    DbConParent02.Open();
                                    using (SqlConnection DbCon02 = new SqlConnection(Tools.conString))
                                    {
                                        DbCon02.Open();
                                        using (SqlCommand cmdParent02 = DbConParent02.CreateCommand())
                                        {
                                            cmdParent02.CommandText = "Select * from MKBDToExcelSetup Where MKBDName = 'MKBD02'";
                                            using (SqlDataReader drParent02 = cmdParent02.ExecuteReader())
                                            {
                                                if (drParent02.HasRows)
                                                {
                                                    using (SqlCommand cmd02 = DbCon02.CreateCommand())
                                                    {
                                                        cmd02.CommandText = "Select * from MKBD02 Where MKBDTrailsPK = @MKBDTrailsPK";
                                                        cmd02.Parameters.AddWithValue("@MKBDTrailsPK", _MKBDTrailsPK);

                                                        using (SqlDataReader dr02 = cmd02.ExecuteReader())
                                                        {
                                                            if (dr02.HasRows)
                                                            {
                                                                dr02.Read();
                                                                while (drParent02.Read())
                                                                {
                                                                    string _pos = Convert.ToString(drParent02["col"]) + Convert.ToString(drParent02["row"]);
                                                                    string _source = Convert.ToString(drParent02["source"]);
                                                                    worksheet2.Cells[_pos].Value = Convert.ToDecimal(dr02[_source]);
                                                                }

                                                                worksheet2.Cells[6, 7].Value = _companyName;
                                                                worksheet2.Cells[7, 7].Value = _dateCounter;
                                                                worksheet2.Cells[8, 7].Value = _directorName;

                                                            }
                                                        }

                                                    }
                                                }
                                            }
                                        }
                                    }
                                }


                                // MKBD03
                                ExcelWorksheet worksheet3 = package.Workbook.Worksheets[3];
                                using (SqlConnection DbConParent03 = new SqlConnection(Tools.conString))
                                {
                                    DbConParent03.Open();
                                    using (SqlConnection DbCon03 = new SqlConnection(Tools.conString))
                                    {
                                        DbCon03.Open();
                                        using (SqlCommand cmdParent03 = DbConParent03.CreateCommand())
                                        {
                                            cmdParent03.CommandText = "Select * from MKBDToExcelSetup Where MKBDName = 'MKBD03'";
                                            using (SqlDataReader drParent03 = cmdParent03.ExecuteReader())
                                            {
                                                if (drParent03.HasRows)
                                                {
                                                    using (SqlCommand cmd03 = DbCon03.CreateCommand())
                                                    {
                                                        cmd03.CommandText = "Select * from MKBD03 Where MKBDTrailsPK = @MKBDTrailsPK";
                                                        cmd03.Parameters.AddWithValue("@MKBDTrailsPK", _MKBDTrailsPK);

                                                        using (SqlDataReader dr03 = cmd03.ExecuteReader())
                                                        {
                                                            if (dr03.HasRows)
                                                            {
                                                                dr03.Read();
                                                                while (drParent03.Read())
                                                                {
                                                                    string _pos = Convert.ToString(drParent03["col"]) + Convert.ToString(drParent03["row"]);
                                                                    string _source = Convert.ToString(drParent03["source"]);
                                                                    worksheet3.Cells[_pos].Value = Convert.ToDecimal(dr03[_source]);
                                                                }

                                                                worksheet3.Cells[6, 9].Value = _companyName;
                                                                worksheet3.Cells[7, 9].Value = _dateCounter;
                                                                worksheet3.Cells[8, 9].Value = _directorName;

                                                            }
                                                        }

                                                    }
                                                }
                                            }
                                        }
                                    }
                                }


                                // MKBD04
                                ExcelWorksheet worksheet4 = package.Workbook.Worksheets[4];
                                using (SqlConnection DbCon04 = new SqlConnection(Tools.conString))
                                {
                                    DbCon04.Open();
                                    using (SqlCommand cmd04 = DbCon04.CreateCommand())
                                    {
                                        if (_mkbdTrails.VersionMode == "Internal")
                                        {
                                            cmd04.CommandText = @"
                                                                select No,A,B,C,D,E,F,G,H from MKBD04LIFO where Date = @Date

                                                                ";
                                        }
                                        else
                                        {
                                            cmd04.CommandText = "select No,A,B,C,D,E,F,G,H from MKBD04 where MKBDTrailsPK = @MKBDTrailsPK ";
                                        }

                                        cmd04.Parameters.AddWithValue("@MKBDTrailsPK", _MKBDTrailsPK);
                                        cmd04.Parameters.AddWithValue("@Date", _dateCounter);
                                        using (SqlDataReader dr04 = cmd04.ExecuteReader())
                                        {
                                            int incRowExcel = 11;
                                            while (dr04.Read())
                                            {
                                                int incColExcel = 1;
                                                int _startRow = 11;
                                                for (int inc1 = 0; inc1 < dr04.FieldCount; inc1++)
                                                {
                                                    if (inc1 == 4 || inc1 == 5 || inc1 == 7 || inc1 == 8)
                                                    {
                                                        worksheet4.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr04.GetValue(inc1));
                                                        worksheet4.Cells[incRowExcel, incColExcel].Style.Numberformat.Format = "#,##0.00";
                                                    }
                                                    else
                                                    {
                                                        worksheet4.Cells[incRowExcel, incColExcel].Value = dr04.GetValue(inc1).ToString();
                                                    }

                                                    incColExcel++;
                                                }
                                                worksheet4.Cells["A" + _startRow + ":I" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                                worksheet4.Cells["A" + _startRow + ":I" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                                worksheet4.Cells["A" + _startRow + ":I" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                worksheet4.Cells["A" + _startRow + ":I" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                                incRowExcel++;
                                                _startRow++;
                                            }
                                            worksheet4.Cells[6, 5].Value = _companyName;
                                            worksheet4.Cells[7, 5].Value = _dateCounter;
                                            worksheet4.Cells[8, 5].Value = _directorName;

                                        }
                                    }

                                }


                                // MKBD05
                                ExcelWorksheet worksheet5 = package.Workbook.Worksheets[5];
                                using (SqlConnection DbCon05 = new SqlConnection(Tools.conString))
                                {
                                    DbCon05.Open();

                                    using (SqlCommand cmd05 = DbCon05.CreateCommand())
                                    {
                                        cmd05.CommandText = "select No,A,B,C,D,E,F,G,H from MKBD05 where MKBDTrailsPK = @MKBDTrailsPK ";
                                        cmd05.Parameters.AddWithValue("@MKBDTrailsPK", _MKBDTrailsPK);

                                        using (SqlDataReader dr05 = cmd05.ExecuteReader())
                                        {
                                            int incRowExcel = 10;
                                            int _startRow = 11;
                                            while (dr05.Read())
                                            {
                                                int incColExcel = 1;
                                                for (int inc1 = 0; inc1 < dr05.FieldCount; inc1++)
                                                {
                                                    worksheet5.Cells[incRowExcel, incColExcel].Value = dr05.GetValue(inc1).ToString();
                                                    incColExcel++;
                                                }
                                                worksheet5.Cells["A" + _startRow + ":I" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                                worksheet5.Cells["A" + _startRow + ":I" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                                worksheet5.Cells["A" + _startRow + ":I" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                worksheet5.Cells["A" + _startRow + ":I" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                                incRowExcel++;
                                                _startRow++;
                                            }
                                            worksheet5.Cells[6, 5].Value = _companyName;
                                            worksheet5.Cells[7, 5].Value = _dateCounter;
                                            worksheet5.Cells[8, 5].Value = _directorName;

                                        }
                                    }



                                }


                                // MKBD06
                                ExcelWorksheet worksheet6 = package.Workbook.Worksheets[6];
                                using (SqlConnection DbConParent06 = new SqlConnection(Tools.conString))
                                {
                                    DbConParent06.Open();
                                    using (SqlConnection DbCon06 = new SqlConnection(Tools.conString))
                                    {
                                        DbCon06.Open();
                                        using (SqlCommand cmdParent06 = DbConParent06.CreateCommand())
                                        {
                                            cmdParent06.CommandText = "Select * from MKBDToExcelSetup Where MKBDName = 'MKBD06'";
                                            using (SqlDataReader drParent06 = cmdParent06.ExecuteReader())
                                            {
                                                if (drParent06.HasRows)
                                                {
                                                    using (SqlCommand cmd06 = DbCon06.CreateCommand())
                                                    {
                                                        cmd06.CommandText = "Select * from MKBD06 Where MKBDTrailsPK = @MKBDTrailsPK";
                                                        cmd06.Parameters.AddWithValue("@MKBDTrailsPK", _MKBDTrailsPK);

                                                        using (SqlDataReader dr06 = cmd06.ExecuteReader())
                                                        {
                                                            if (dr06.HasRows)
                                                            {
                                                                dr06.Read();

                                                                while (drParent06.Read())
                                                                {

                                                                    string _pos = Convert.ToString(drParent06["col"]) + Convert.ToString(drParent06["row"]);
                                                                    string _source = Convert.ToString(drParent06["source"]);
                                                                    worksheet6.Cells[_pos].Value = Convert.ToString(dr06[_source]);
                                                                }
                                                                worksheet6.Cells[6, 9].Value = _companyName;
                                                                worksheet6.Cells[7, 9].Value = _dateCounter;
                                                                worksheet6.Cells[8, 9].Value = _directorName;


                                                            }
                                                        }

                                                    }
                                                }
                                            }
                                        }
                                    }
                                }

                                // MKBD06D
                                ExcelWorksheet worksheet6D = package.Workbook.Worksheets[6];
                                using (SqlConnection DbCon06D = new SqlConnection(Tools.conString))
                                {
                                    DbCon06D.Open();

                                    using (SqlCommand cmd06D = DbCon06D.CreateCommand())
                                    {
                                        cmd06D.CommandText = "select No,A + ' ' + isnull(B.Name,'') A,'','','','',B,C,D,E,F from MKBD06Detail A left join Bank B on ltrim(rtrim(A.A)) = Ltrim(rtrim(B.RTGSCode)) and B.status in (1,2) where MKBDTrailsPK = @MKBDTrailsPK ";
                                        cmd06D.Parameters.AddWithValue("@MKBDTrailsPK", _MKBDTrailsPK);

                                        using (SqlDataReader dr06D = cmd06D.ExecuteReader())
                                        {
                                            int incRowExcel = 31;
                                            while (dr06D.Read())
                                            {
                                                int incColExcel = 1;
                                                int _startRow = 31;
                                                for (int inc1 = 0; inc1 < dr06D.FieldCount; inc1++)
                                                {
                                                    if (incColExcel == 10 || incColExcel == 11)
                                                    {
                                                        worksheet6D.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr06D[inc1]);
                                                        worksheet6D.Cells[incRowExcel, incColExcel].Style.Numberformat.Format = "#,##0.00";
                                                    }
                                                    else
                                                    {
                                                        worksheet6D.Cells[incRowExcel, incColExcel].Value = dr06D.GetValue(inc1).ToString();
                                                    }

                                                    incColExcel++;
                                                }
                                                worksheet6D.Cells["A" + _startRow + ":K" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                                worksheet6D.Cells["A" + _startRow + ":K" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                                worksheet6D.Cells["A" + _startRow + ":K" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                worksheet6D.Cells["A" + _startRow + ":K" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                                incRowExcel++;
                                            }
                                        }
                                    }

                                }

                                // MKBD07
                                ExcelWorksheet worksheet7 = package.Workbook.Worksheets[7];
                                using (SqlConnection DbConParent07 = new SqlConnection(Tools.conString))
                                {
                                    DbConParent07.Open();
                                    using (SqlConnection DbCon07 = new SqlConnection(Tools.conString))
                                    {
                                        DbCon07.Open();
                                        using (SqlCommand cmdParent07 = DbConParent07.CreateCommand())
                                        {
                                            cmdParent07.CommandText = "Select * from MKBDToExcelSetup Where MKBDName = 'MKBD07'";
                                            using (SqlDataReader drParent07 = cmdParent07.ExecuteReader())
                                            {
                                                if (drParent07.HasRows)
                                                {
                                                    using (SqlCommand cmd07 = DbCon07.CreateCommand())
                                                    {
                                                        cmd07.CommandText = "Select * from MKBD07 Where MKBDTrailsPK = @MKBDTrailsPK";
                                                        cmd07.Parameters.AddWithValue("@MKBDTrailsPK", _MKBDTrailsPK);

                                                        using (SqlDataReader dr07 = cmd07.ExecuteReader())
                                                        {
                                                            if (dr07.HasRows)
                                                            {
                                                                dr07.Read();
                                                                while (drParent07.Read())
                                                                {
                                                                    string _pos = Convert.ToString(drParent07["col"]) + Convert.ToString(drParent07["row"]);
                                                                    string _source = Convert.ToString(drParent07["source"]);
                                                                    worksheet7.Cells[_pos].Value = Convert.ToDecimal(dr07[_source]);
                                                                    worksheet7.Cells[_pos].Style.Numberformat.Format = "#,##0.00";
                                                                }
                                                                worksheet7.Cells[6, 9].Value = _companyName;
                                                                worksheet7.Cells[7, 9].Value = _dateCounter;
                                                                worksheet7.Cells[8, 9].Value = _directorName;

                                                            }
                                                        }

                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                // MKBD08
                                ExcelWorksheet worksheet8 = package.Workbook.Worksheets[8];
                                using (SqlConnection DbConParent08 = new SqlConnection(Tools.conString))
                                {
                                    DbConParent08.Open();
                                    using (SqlConnection DbCon08 = new SqlConnection(Tools.conString))
                                    {
                                        DbCon08.Open();
                                        using (SqlCommand cmdParent08 = DbConParent08.CreateCommand())
                                        {
                                            cmdParent08.CommandText = "Select * from MKBDToExcelSetup Where MKBDName = 'MKBD08'";
                                            using (SqlDataReader drParent08 = cmdParent08.ExecuteReader())
                                            {
                                                if (drParent08.HasRows)
                                                {
                                                    using (SqlCommand cmd08 = DbCon08.CreateCommand())
                                                    {
                                                        cmd08.CommandText = "Select * from MKBD08 Where MKBDTrailsPK = @MKBDTrailsPK";
                                                        cmd08.Parameters.AddWithValue("@MKBDTrailsPK", _MKBDTrailsPK);

                                                        using (SqlDataReader dr08 = cmd08.ExecuteReader())
                                                        {
                                                            if (dr08.HasRows)
                                                            {
                                                                dr08.Read();
                                                                while (drParent08.Read())
                                                                {
                                                                    string _pos = Convert.ToString(drParent08["col"]) + Convert.ToString(drParent08["row"]);
                                                                    string _source = Convert.ToString(drParent08["source"]);
                                                                    worksheet8.Cells[_pos].Value = Convert.ToDecimal(dr08[_source]);
                                                                    worksheet8.Cells[_pos].Style.Numberformat.Format = "#,##0.00";
                                                                }
                                                                worksheet8.Cells[6, 9].Value = _companyName;
                                                                worksheet8.Cells[7, 9].Value = _dateCounter;
                                                                worksheet8.Cells[8, 9].Value = _directorName;

                                                            }
                                                        }

                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                // MKBD09
                                ExcelWorksheet worksheet9 = package.Workbook.Worksheets[9];
                                using (SqlConnection DbConParent09 = new SqlConnection(Tools.conString))
                                {
                                    DbConParent09.Open();
                                    using (SqlConnection DbCon09 = new SqlConnection(Tools.conString))
                                    {
                                        DbCon09.Open();
                                        using (SqlCommand cmdParent09 = DbConParent09.CreateCommand())
                                        {
                                            cmdParent09.CommandText = "Select * from MKBDToExcelSetup Where MKBDName = 'MKBD09'";
                                            using (SqlDataReader drParent09 = cmdParent09.ExecuteReader())
                                            {
                                                if (drParent09.HasRows)
                                                {
                                                    using (SqlCommand cmd09 = DbCon09.CreateCommand())
                                                    {
                                                        cmd09.CommandText = "Select * from MKBD09 Where MKBDTrailsPK = @MKBDTrailsPK";
                                                        cmd09.Parameters.AddWithValue("@MKBDTrailsPK", _MKBDTrailsPK);

                                                        using (SqlDataReader dr09 = cmd09.ExecuteReader())
                                                        {
                                                            if (dr09.HasRows)
                                                            {
                                                                dr09.Read();
                                                                while (drParent09.Read())
                                                                {
                                                                    string _pos = Convert.ToString(drParent09["col"]) + Convert.ToString(drParent09["row"]);
                                                                    string _source = Convert.ToString(drParent09["source"]);
                                                                    worksheet9.Cells[_pos].Value = Convert.ToDecimal(dr09[_source]);
                                                                    worksheet9.Cells[_pos].Style.Numberformat.Format = "#,##0.00";
                                                                }
                                                                worksheet9.Cells[6, 10].Value = _companyName;
                                                                worksheet9.Cells[7, 10].Value = _dateCounter;
                                                                worksheet9.Cells[8, 10].Value = _directorName;

                                                            }
                                                        }

                                                    }
                                                }
                                            }
                                        }
                                    }
                                }

                                // MKBD510A
                                ExcelWorksheet worksheet10A = package.Workbook.Worksheets[10];
                                using (SqlConnection DbCon10A = new SqlConnection(Tools.conString))
                                {
                                    DbCon10A.Open();

                                    using (SqlCommand cmd10A = DbCon10A.CreateCommand())
                                    {
                                        cmd10A.CommandText = "select A,B,C,D,E,F,G,H,I,J,K from MKBD510A where MKBDTrailsPK = @MKBDTrailsPK ";
                                        cmd10A.Parameters.AddWithValue("@MKBDTrailsPK", _MKBDTrailsPK);

                                        using (SqlDataReader dr10A = cmd10A.ExecuteReader())
                                        {
                                            int incRowExcel = 11;
                                            while (dr10A.Read())
                                            {
                                                int incColExcel = 1;
                                                int _startRow = 11;
                                                for (int inc1 = 0; inc1 < dr10A.FieldCount - 1; inc1++)
                                                {
                                                    worksheet10A.Cells[incRowExcel, incColExcel].Value = dr10A.GetValue(inc1).ToString();
                                                    incColExcel++;
                                                }
                                                worksheet10A.Cells["A" + _startRow + ":I" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                                worksheet10A.Cells["A" + _startRow + ":I" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                                worksheet10A.Cells["A" + _startRow + ":I" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                worksheet10A.Cells["A" + _startRow + ":I" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                                incRowExcel++;
                                                _startRow++;
                                            }
                                            worksheet10A.Cells[5, 5].Value = _companyName;
                                            worksheet10A.Cells[6, 5].Value = _dateCounter;
                                            worksheet10A.Cells[7, 5].Value = _directorName;
                                        }
                                    }



                                }

                                // MKBD510B
                                ExcelWorksheet worksheet10B = package.Workbook.Worksheets[11];
                                using (SqlConnection DbCon10B = new SqlConnection(Tools.conString))
                                {
                                    DbCon10B.Open();

                                    using (SqlCommand cmd10B = DbCon10B.CreateCommand())
                                    {
                                        cmd10B.CommandText = "select A,B,C,D,E,F,G,H,I,J,K from MKBD510B where MKBDTrailsPK = @MKBDTrailsPK ";
                                        cmd10B.Parameters.AddWithValue("@MKBDTrailsPK", _MKBDTrailsPK);

                                        using (SqlDataReader dr10B = cmd10B.ExecuteReader())
                                        {
                                            int incRowExcel = 11;
                                            while (dr10B.Read())
                                            {
                                                int incColExcel = 1;
                                                int _startRow = 11;
                                                for (int inc1 = 0; inc1 < dr10B.FieldCount; inc1++)
                                                {
                                                    worksheet10B.Cells[incRowExcel, incColExcel].Value = dr10B.GetValue(inc1).ToString();
                                                    incColExcel++;
                                                }
                                                worksheet10A.Cells["A" + _startRow + ":I" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                                worksheet10A.Cells["A" + _startRow + ":I" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                                worksheet10A.Cells["A" + _startRow + ":I" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                worksheet10A.Cells["A" + _startRow + ":I" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                                incRowExcel++;
                                                _startRow++;
                                            }
                                            worksheet10B.Cells[5, 5].Value = _companyName;
                                            worksheet10B.Cells[6, 5].Value = _dateCounter;
                                            worksheet10B.Cells[7, 5].Value = _directorName;
                                        }
                                    }



                                }

                                // MKBD510C
                                ExcelWorksheet worksheet10C = package.Workbook.Worksheets[12];
                                using (SqlConnection DbCon10C = new SqlConnection(Tools.conString))
                                {
                                    DbCon10C.Open();

                                    using (SqlCommand cmd10C = DbCon10C.CreateCommand())
                                    {
                                        cmd10C.CommandText = "select A,B,C,D,E,F,G,H,I,J,K from MKBD510C where MKBDTrailsPK = @MKBDTrailsPK ";
                                        cmd10C.Parameters.AddWithValue("@MKBDTrailsPK", _MKBDTrailsPK);

                                        using (SqlDataReader dr10C = cmd10C.ExecuteReader())
                                        {
                                            int incRowExcel = 11;
                                            while (dr10C.Read())
                                            {
                                                int incColExcel = 1;
                                                int _startRow = 11;
                                                for (int inc1 = 0; inc1 < dr10C.FieldCount; inc1++)
                                                {
                                                    if (incColExcel == 5 || incColExcel == 6 || incColExcel == 7 || incColExcel == 8 || incColExcel == 10 || incColExcel == 11)
                                                    {
                                                        worksheet10C.Cells[incRowExcel, incColExcel].Value = Convert.ToDecimal(dr10C[inc1]);
                                                        worksheet10C.Cells[incRowExcel, incColExcel].Style.Numberformat.Format = "#,##0.00";
                                                    }
                                                    else
                                                    {
                                                        worksheet10C.Cells[incRowExcel, incColExcel].Value = dr10C.GetValue(inc1).ToString();
                                                    }

                                                    incColExcel++;
                                                }
                                                worksheet10A.Cells["A" + _startRow + ":K" + incRowExcel].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                                worksheet10A.Cells["A" + _startRow + ":K" + incRowExcel].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                                worksheet10A.Cells["A" + _startRow + ":K" + incRowExcel].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                                worksheet10A.Cells["A" + _startRow + ":K" + incRowExcel].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                                incRowExcel++;
                                            }
                                            worksheet10C.Cells[5, 5].Value = _companyName;
                                            worksheet10C.Cells[6, 5].Value = _dateCounter;
                                            worksheet10C.Cells[7, 5].Value = _directorName;

                                        }
                                    }


                                }

                                // MKBD510D
                                ExcelWorksheet worksheet10D = package.Workbook.Worksheets[13];
                                using (SqlConnection DbCon10D = new SqlConnection(Tools.conString))
                                {
                                    DbCon10D.Open();

                                    using (SqlCommand cmd10D = DbCon10D.CreateCommand())
                                    {
                                        cmd10D.CommandText = "select A,B,C,D,E,F,G,H from MKBD510D where MKBDTrailsPK = @MKBDTrailsPK ";
                                        cmd10D.Parameters.AddWithValue("@MKBDTrailsPK", _MKBDTrailsPK);

                                        using (SqlDataReader dr10D = cmd10D.ExecuteReader())
                                        {
                                            int incRowExcel = 11;
                                            while (dr10D.Read())
                                            {
                                                int incColExcel = 1;
                                                for (int inc1 = 0; inc1 < dr10D.FieldCount; inc1++)
                                                {
                                                    worksheet10D.Cells[incRowExcel, incColExcel].Value = dr10D.GetValue(inc1).ToString();
                                                    incColExcel++;
                                                }
                                                incRowExcel++;
                                            }
                                            worksheet10D.Cells[5, 5].Value = _companyName;
                                            worksheet10D.Cells[6, 5].Value = _dateCounter;
                                            worksheet10D.Cells[7, 5].Value = _directorName;


                                        }
                                    }



                                }

                                // MKBD510E
                                ExcelWorksheet worksheet10E = package.Workbook.Worksheets[14];
                                using (SqlConnection DbCon10E = new SqlConnection(Tools.conString))
                                {
                                    DbCon10E.Open();

                                    using (SqlCommand cmd10E = DbCon10E.CreateCommand())
                                    {
                                        cmd10E.CommandText = "select A,B,C,D,E,F from MKBD510E where MKBDTrailsPK = @MKBDTrailsPK ";
                                        cmd10E.Parameters.AddWithValue("@MKBDTrailsPK", _MKBDTrailsPK);

                                        using (SqlDataReader dr10E = cmd10E.ExecuteReader())
                                        {
                                            int incRowExcel = 11;
                                            while (dr10E.Read())
                                            {
                                                int incColExcel = 1;
                                                for (int inc1 = 0; inc1 < dr10E.FieldCount; inc1++)
                                                {
                                                    worksheet10E.Cells[incRowExcel, incColExcel].Value = dr10E.GetValue(inc1).ToString();
                                                    incColExcel++;
                                                }
                                                incRowExcel++;
                                            }
                                            worksheet10E.Cells[5, 5].Value = _companyName;
                                            worksheet10E.Cells[6, 5].Value = _dateCounter;
                                            worksheet10E.Cells[7, 5].Value = _directorName;

                                        }
                                    }



                                }

                                // MKBD510F
                                ExcelWorksheet worksheet10F = package.Workbook.Worksheets[15];
                                using (SqlConnection DbCon10F = new SqlConnection(Tools.conString))
                                {
                                    DbCon10F.Open();

                                    using (SqlCommand cmd10F = DbCon10F.CreateCommand())
                                    {
                                        cmd10F.CommandText = "select A,B,C,D,E,F,G,H,I,J from MKBD510F where MKBDTrailsPK = @MKBDTrailsPK ";
                                        cmd10F.Parameters.AddWithValue("@MKBDTrailsPK", _MKBDTrailsPK);

                                        using (SqlDataReader dr10F = cmd10F.ExecuteReader())
                                        {
                                            int incRowExcel = 11;
                                            while (dr10F.Read())
                                            {
                                                int incColExcel = 1;
                                                for (int inc1 = 0; inc1 < dr10F.FieldCount; inc1++)
                                                {
                                                    worksheet10F.Cells[incRowExcel, incColExcel].Value = dr10F.GetValue(inc1).ToString();
                                                    incColExcel++;
                                                }
                                                incRowExcel++;
                                            }
                                            worksheet10F.Cells[5, 5].Value = _companyName;
                                            worksheet10F.Cells[6, 5].Value = _dateCounter;
                                            worksheet10F.Cells[7, 5].Value = _directorName;


                                        }
                                    }



                                }

                                // MKBD510G
                                ExcelWorksheet worksheet10G = package.Workbook.Worksheets[16];
                                using (SqlConnection DbCon10G = new SqlConnection(Tools.conString))
                                {
                                    DbCon10G.Open();

                                    using (SqlCommand cmd10G = DbCon10G.CreateCommand())
                                    {
                                        cmd10G.CommandText = "select A,B,C,D,E,F,G,H,I from MKBD510G where MKBDTrailsPK = @MKBDTrailsPK ";
                                        cmd10G.Parameters.AddWithValue("@MKBDTrailsPK", _MKBDTrailsPK);

                                        using (SqlDataReader dr10G = cmd10G.ExecuteReader())
                                        {
                                            int incRowExcel = 11;
                                            while (dr10G.Read())
                                            {
                                                int incColExcel = 1;
                                                for (int inc1 = 0; inc1 < dr10G.FieldCount; inc1++)
                                                {
                                                    worksheet10G.Cells[incRowExcel, incColExcel].Value = dr10G.GetValue(inc1).ToString();
                                                    incColExcel++;
                                                }
                                                incRowExcel++;
                                            }
                                            worksheet10G.Cells[5, 5].Value = _companyName;
                                            worksheet10G.Cells[6, 5].Value = _dateCounter;
                                            worksheet10G.Cells[7, 5].Value = _directorName;

                                        }
                                    }



                                }

                                // MKBD510H
                                ExcelWorksheet worksheet10H = package.Workbook.Worksheets[17];
                                using (SqlConnection DbCon10H = new SqlConnection(Tools.conString))
                                {
                                    DbCon10H.Open();

                                    using (SqlCommand cmd10H = DbCon10H.CreateCommand())
                                    {
                                        cmd10H.CommandText = "select A,B,C,D,E,F,G from MKBD510H where MKBDTrailsPK = @MKBDTrailsPK ";
                                        cmd10H.Parameters.AddWithValue("@MKBDTrailsPK", _MKBDTrailsPK);

                                        using (SqlDataReader dr10H = cmd10H.ExecuteReader())
                                        {
                                            int incRowExcel = 11;
                                            while (dr10H.Read())
                                            {
                                                int incColExcel = 1;
                                                for (int inc1 = 0; inc1 < dr10H.FieldCount; inc1++)
                                                {
                                                    worksheet10H.Cells[incRowExcel, incColExcel].Value = dr10H.GetValue(inc1).ToString();
                                                    incColExcel++;
                                                }
                                                incRowExcel++;
                                            }
                                            worksheet10H.Cells[5, 5].Value = _companyName;
                                            worksheet10H.Cells[6, 5].Value = _dateCounter;
                                            worksheet10H.Cells[7, 5].Value = _directorName;

                                        }
                                    }



                                }

                                // MKBD510I
                                ExcelWorksheet worksheet10I = package.Workbook.Worksheets[18];
                                using (SqlConnection DbCon10I = new SqlConnection(Tools.conString))
                                {
                                    DbCon10I.Open();

                                    using (SqlCommand cmd10I = DbCon10I.CreateCommand())
                                    {
                                        cmd10I.CommandText = "select A,B,C,D,E,F,G from MKBD510I where MKBDTrailsPK = @MKBDTrailsPK ";
                                        cmd10I.Parameters.AddWithValue("@MKBDTrailsPK", _MKBDTrailsPK);

                                        using (SqlDataReader dr10I = cmd10I.ExecuteReader())
                                        {
                                            int incRowExcel = 11;
                                            while (dr10I.Read())
                                            {
                                                int incColExcel = 1;
                                                for (int inc1 = 0; inc1 < dr10I.FieldCount; inc1++)
                                                {
                                                    worksheet10I.Cells[incRowExcel, incColExcel].Value = dr10I.GetValue(inc1).ToString();
                                                    incColExcel++;
                                                }
                                                incRowExcel++;
                                            }
                                            worksheet10I.Cells[5, 5].Value = _companyName;
                                            worksheet10I.Cells[6, 5].Value = _dateCounter;
                                            worksheet10I.Cells[7, 5].Value = _directorName;

                                        }
                                    }
                                }

                                package.Save();
                                _dateCounter = _dateCounter.AddDays(1);

                                //return _a = Tools.HtmlMKBDExcelPath + _host.Get_MKBDCode() + _host.Get_ValueDateByMKBDTrails(_MKBDTrailsPK).ToString("yyMMdd") + ".xlsx";
                            }
                        }
                        Tools.CreateFolderToZIP(Tools.MKBDExcelPath + "ExcelFile\\" + _dateFolder);
                        return Tools.HtmlMKBDExcelPath + "ExcelFile\\" + _dateFolder + ".zip";
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public string MKBDTrails_ToText(string _userID, int _MKBDTrailsPK, string _VersionMode)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "GenerateMKBDToText";
                        cmd.Parameters.AddWithValue("@MKBDTrailsPK", _MKBDTrailsPK);
                        cmd.Parameters.AddWithValue("@VersionMode", _VersionMode);
                        cmd.Parameters.AddWithValue("@UsersID", _userID);
                        cmd.Parameters.AddWithValue("@TimeNow", _datetimeNow);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                string filePath = Tools.MKBDTextPath + _host.Get_MKBDCode() + _host.Get_ValueDateByMKBDTrails(_MKBDTrailsPK).ToString("yyMMdd") + ".mkb";
                                FileInfo txtFile = new FileInfo(filePath);
                                if (txtFile.Exists)
                                {
                                    txtFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                }

                                using (System.IO.StreamWriter file = new System.IO.StreamWriter(filePath))
                                {
                                    while (dr.Read())
                                    {
                                        file.WriteLine(Convert.ToString(dr["ResultText"]));
                                    }
                                    return Tools.HtmlMKBDTextPath + _host.Get_MKBDCode() + _host.Get_ValueDateByMKBDTrails(_MKBDTrailsPK).ToString("yyMMdd") + ".mkb";
                                }

                            }
                            return null;
                        }

                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }


        public bool Validate_ApproveBySelected(DateTime _dateFrom, DateTime _dateTo)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {


                        cmd.CommandText = " if Exists(select * From MKBDTrails where Status = 2 and ValueDate between @ValueDateFrom and @ValueDateTo ) " +
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

        public void MKBDTrails_ApproveBySelected(string _usersID, string _permissionID, DateTime _dateFrom, DateTime _dateTo)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @" Declare @IPAddress nvarchar(50) select @IPAddress = IPAddress from Users where ID = @UsersID and Status = 2 
                                 Insert into Activity(Time,PermissionID,ObjectTable,ObjectTablePK,Status,Message,UsersID,IPAddress,LastUpdate) 
                                 Select @Time,@PermissionID,'MKBDTrails',MKBDTrailsPK,1,'Approve by Selected Data',@UsersID,@IPAddress,@Time  from MKBDTrails where ValueDate between @DateFrom and @DateTo and Status = 1 and Selected  = 1 
                                 update MKBDTrails set status = 2,ApprovedUsersID = @UsersID,ApprovedTime = @Time,LastUpdate=@Time where status = 1 and MKBDTrailsPK in ( Select MKBDTrailsPK from MKBDTrails where ValueDate between @DateFrom and @DateTo and Status = 1 and Selected  = 1 ) 
                                 Update MKBDTrails set status= 3,VoidUsersID=@UsersID,VoidTime=@Time,LastUpdate=@Time  where status = 4 and MKBDTrailsPK in (Select MKBDTrailsPK from MKBDTrails where ValueDate between @DateFrom and @DateTo and Status = 4 and Selected  = 1) ";

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

        public void MKBDTrails_RejectBySelected(string _usersID, string _permissionID, DateTime _dateFrom, DateTime _dateTo)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"Declare @IPAddress nvarchar(50) select @IPAddress = IPAddress from Users where ID = @UsersID and Status = 2 
                                          Insert into Activity(Time,PermissionID,ObjectTable,ObjectTablePK,Status,Message,UsersID,IPAddress,LastUpdate) 
                                          Select @Time,@PermissionID,'MKBDTrails',MKBDTrailsPK,1,'Reject by Selected Data',@UsersID,@IPAddress,@Time  from MKBDTrails where ValueDate between @DateFrom and @DateTo and Status = 1 and Selected  = 1 
                                          update MKBDTrails set status = 3,selected = 0,VoidUsersID = @UsersID,VoidTime = @Time,LastUpdate=@Time where status = 1 and MKBDTrailsPK in ( Select MKBDTrailsPK from MKBDTrails where ValueDate between @DateFrom and @DateTo and Status = 1 and Selected  = 1 ) 
                                          Update MKBDTrails set status= 2  where status = 4 and MKBDTrailsPK in (Select MKBDTrailsPK from MKBDTrails where ValueDate between @DateFrom and @DateTo and Status = 4 and Selected  = 1) ";

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

        public NAWCDailyProcess Generate_NAWCDailyByDateFromTo(string _userID, DateTime _dateFrom, DateTime _dateTo)
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
                        --declare @dateFrom date
--declare @Dateto date
--declare @timeNow datetime
--declare @ClientCode nvarchar(100)
--declare @status int
--declare @UsersID nvarchar(100)

--set @dateFrom = '2020-07-01'
--set @Dateto = '2020-07-01'
--set @timeNow = getdate()
--set @ClientCode = '00'
--set @status = 1
--set @UsersID = 'admin'


--drop table #date 
--drop table #tmpReksadana
--drop table #tmpDeposito
--drop table #tmpBond
--drop table #tmpEquity
--drop table #tmpSBN
--drop table #IsUnderDueDateAmount
--drop table #QuaranteedAmount
--drop table #NotLiquidatedAmount
--drop table #LiquidatedAmount
--drop table #MKBD04    

Declare @SumLiabilities numeric(18,4) 
Declare @SumLiabilitiesEquity numeric(18,4) 
Declare @SumEquity numeric(18,4) 
Declare @Date datetime

                        Declare @NewMKBDTrailsPK int

                        Declare @BitValidate bit





                        declare @ssql nvarchar(4000)   

                        declare @field nvarchar(5)    

                        declare @value numeric(18,4)  

                        declare @TotalEquity float   



                        declare @CompanyID nvarchar(2)        

                        declare @CompanyName nvarchar(50)        

                        declare @DirectorName nvarchar(50)    

                        Declare @PeriodPK int



                        Declare @MsgSuccess nvarchar(max)



                        create table #date 
                        (
                        valuedate datetime
                        )
						

                        Create Table #tmpReksadana

                        (  

                        [InstrumentPK] INTEGER NOT NULL,  

                        [LastVolume] [numeric](18, 4) NOT NULL,
                        
                        ValueDate Datetime

                        )
						

                        Create Table #tmpDeposito

                        (  

                        [InstrumentPK] INTEGER NOT NULL,  

                        [LastAmount] [numeric](18, 4) NOT NULL,

                        [MaturityDate] Datetime NOT NULL,
                        
                        ValueDate Datetime


                        )
						

                        Create Table #tmpBond

                        (  

                        [InstrumentPK] INTEGER NOT NULL,  

                        [Volume] [numeric](18,4) NOT NULL,
                     
                        ValueDate Datetime

                        )
						

                        Create Table #tmpEquity

                        (  

                        [InstrumentPK] INTEGER NOT NULL,  

                        [LastVolume] [numeric](18, 4) NOT NULL,
                        
                        ValueDate Datetime

                        )
						

                        Create Table #tmpSBN

                        (  

                        [InstrumentPK] INTEGER NOT NULL,  

                        [Volume] [numeric](18,4) NOT NULL,

                        MaturityDate Datetime,
                        
                        ValueDate Datetime

                        )
						
                        CREATE TABLE #IsUnderDueDateAmount (
                        [HistoryPK] [bigint] NOT NULL,
                        [Status] [int] NOT NULL,
                        [Notes] [nvarchar](1000) NULL,
                        [Date] [datetime] NOT NULL,
                        [MKBDTrailsPK] [int] NOT NULL,
                        [MKBD01] [int] NULL,
                        [MKBD02] [int] NULL,
                        [MKBD03] [int] NULL,
                        [MKBD09] [int] NULL,
                        [InstrumentPK] [int] NOT NULL,
                        [DepositoTypePK] [int] NOT NULL,
                        [IsLiquidated] [nchar](10) NOT NULL,
                        [DueDate] [datetime] NOT NULL,
                        [MarketValue] [numeric](18, 4) NOT NULL,
                        [IsUnderDuedateAmount] [numeric](18, 4) NOT NULL,
                        [NotUnderDuedateAmount] [numeric](18, 4) NOT NULL,
                        [QuaranteedAmount] [numeric](18, 4) NOT NULL,
                        [NotLiquidatedAmount] [numeric](18, 4) NOT NULL,
                        [LiquidatedAmount] [numeric](18, 4) NOT NULL,
                        [HaircutPercent] [numeric](18, 4) NOT NULL,
                        [HaircutAmount] [numeric](18, 4) NOT NULL,
                        [AfterHaircutAmount] [numeric](18, 4) NOT NULL,
                        [RankingLiabilities] [numeric](18, 4) NOT NULL,
                        [EntryUsersID] [nvarchar](70) NULL,
                        [EntryTime] [datetime] NULL,
                        [UpdateUsersID] [nvarchar](70) NULL,
                        [UpdateTime] [datetime] NULL,
                        [ApprovedUsersID] [nvarchar](70) NULL,
                        [ApprovedTime] [datetime] NULL,
                        [VoidUsersID] [nvarchar](70) NULL,
                        [VoidTime] [datetime] NULL,
                        [DBUserID] [nvarchar](50) NULL,
                        [DBTerminalID] [nvarchar](50) NULL,
                        [LastUpdate] [datetime] NULL,
                        [LastUpdateDB] [datetime] NULL
                        )
						
                        CREATE TABLE #QuaranteedAmount (
                        [HistoryPK] [bigint] NOT NULL,
                        [Status] [int] NOT NULL,
                        [Notes] [nvarchar](1000) NULL,
                        [Date] [datetime] NOT NULL,
                        [MKBDTrailsPK] [int] NOT NULL,
                        [MKBD01] [int] NULL,
                        [MKBD02] [int] NULL,
                        [MKBD03] [int] NULL,
                        [MKBD09] [int] NULL,
                        [InstrumentPK] [int] NOT NULL,
                        [DepositoTypePK] [int] NOT NULL,
                        [IsLiquidated] [nchar](10) NOT NULL,
                        [DueDate] [datetime] NOT NULL,
                        [MarketValue] [numeric](18, 4) NOT NULL,
                        [IsUnderDuedateAmount] [numeric](18, 4) NOT NULL,
                        [NotUnderDuedateAmount] [numeric](18, 4) NOT NULL,
                        [QuaranteedAmount] [numeric](18, 4) NOT NULL,
                        [NotLiquidatedAmount] [numeric](18, 4) NOT NULL,
                        [LiquidatedAmount] [numeric](18, 4) NOT NULL,
                        [HaircutPercent] [numeric](18, 4) NOT NULL,
                        [HaircutAmount] [numeric](18, 4) NOT NULL,
                        [AfterHaircutAmount] [numeric](18, 4) NOT NULL,
                        [RankingLiabilities] [numeric](18, 4) NOT NULL,
                        [EntryUsersID] [nvarchar](70) NULL,
                        [EntryTime] [datetime] NULL,
                        [UpdateUsersID] [nvarchar](70) NULL,
                        [UpdateTime] [datetime] NULL,
                        [ApprovedUsersID] [nvarchar](70) NULL,
                        [ApprovedTime] [datetime] NULL,
                        [VoidUsersID] [nvarchar](70) NULL,
                        [VoidTime] [datetime] NULL,
                        [DBUserID] [nvarchar](50) NULL,
                        [DBTerminalID] [nvarchar](50) NULL,
                        [LastUpdate] [datetime] NULL,
                        [LastUpdateDB] [datetime] NULL
                        )
						
                        CREATE TABLE #NotLiquidatedAmount (
                        [HistoryPK] [bigint] NOT NULL,
                        [Status] [int] NOT NULL,
                        [Notes] [nvarchar](1000) NULL,
                        [Date] [datetime] NOT NULL,
                        [MKBDTrailsPK] [int] NOT NULL,
                        [MKBD01] [int] NULL,
                        [MKBD02] [int] NULL,
                        [MKBD03] [int] NULL,
                        [MKBD09] [int] NULL,
                        [InstrumentPK] [int] NOT NULL,
                        [DepositoTypePK] [int] NOT NULL,
                        [IsLiquidated] [nchar](10) NOT NULL,
                        [DueDate] [datetime] NOT NULL,
                        [MarketValue] [numeric](18, 4) NOT NULL,
                        [IsUnderDuedateAmount] [numeric](18, 4) NOT NULL,
                        [NotUnderDuedateAmount] [numeric](18, 4) NOT NULL,
                        [QuaranteedAmount] [numeric](18, 4) NOT NULL,
                        [NotLiquidatedAmount] [numeric](18, 4) NOT NULL,
                        [LiquidatedAmount] [numeric](18, 4) NOT NULL,
                        [HaircutPercent] [numeric](18, 4) NOT NULL,
                        [HaircutAmount] [numeric](18, 4) NOT NULL,
                        [AfterHaircutAmount] [numeric](18, 4) NOT NULL,
                        [RankingLiabilities] [numeric](18, 4) NOT NULL,
                        [EntryUsersID] [nvarchar](70) NULL,
                        [EntryTime] [datetime] NULL,
                        [UpdateUsersID] [nvarchar](70) NULL,
                        [UpdateTime] [datetime] NULL,
                        [ApprovedUsersID] [nvarchar](70) NULL,
                        [ApprovedTime] [datetime] NULL,
                        [VoidUsersID] [nvarchar](70) NULL,
                        [VoidTime] [datetime] NULL,
                        [DBUserID] [nvarchar](50) NULL,
                        [DBTerminalID] [nvarchar](50) NULL,
                        [LastUpdate] [datetime] NULL,
                        [LastUpdateDB] [datetime] NULL
                        )
						
                        CREATE TABLE #LiquidatedAmount (
                        [HistoryPK] [bigint] NOT NULL,
                        [Status] [int] NOT NULL,
                        [Notes] [nvarchar](1000) NULL,
                        [Date] [datetime] NOT NULL,
                        [MKBDTrailsPK] [int] NOT NULL,
                        [MKBD01] [int] NULL,
                        [MKBD02] [int] NULL,
                        [MKBD03] [int] NULL,
                        [MKBD09] [int] NULL,
                        [InstrumentPK] [int] NOT NULL,
                        [DepositoTypePK] [int] NOT NULL,
                        [IsLiquidated] [nchar](10) NOT NULL,
                        [DueDate] [datetime] NOT NULL,
                        [MarketValue] [numeric](18, 4) NOT NULL,
                        [IsUnderDuedateAmount] [numeric](18, 4) NOT NULL,
                        [NotUnderDuedateAmount] [numeric](18, 4) NOT NULL,
                        [QuaranteedAmount] [numeric](18, 4) NOT NULL,
                        [NotLiquidatedAmount] [numeric](18, 4) NOT NULL,
                        [LiquidatedAmount] [numeric](18, 4) NOT NULL,
                        [HaircutPercent] [numeric](18, 4) NOT NULL,
                        [HaircutAmount] [numeric](18, 4) NOT NULL,
                        [AfterHaircutAmount] [numeric](18, 4) NOT NULL,
                        [RankingLiabilities] [numeric](18, 4) NOT NULL,
                        [EntryUsersID] [nvarchar](70) NULL,
                        [EntryTime] [datetime] NULL,
                        [UpdateUsersID] [nvarchar](70) NULL,
                        [UpdateTime] [datetime] NULL,
                        [ApprovedUsersID] [nvarchar](70) NULL,
                        [ApprovedTime] [datetime] NULL,
                        [VoidUsersID] [nvarchar](70) NULL,
                        [VoidTime] [datetime] NULL,
                        [DBUserID] [nvarchar](50) NULL,
                        [DBTerminalID] [nvarchar](50) NULL,
                        [LastUpdate] [datetime] NULL,
                        [LastUpdateDB] [datetime] NULL
                        )
						

                        Create Table #MKBD04

                        (  

                        [MKBDTrailsPK] INTEGER NOT NULL,  

                        [Date] [datetime] NOT NULL,  

                        [CompanyID] [nvarchar](12) NOT NULL,  

                        [CompanyName] [nvarchar](50) NOT NULL,  

                        [DirectorName] [nvarchar](50) NOT NULL,  

                        [MKBDNo] [int] NOT NULL,  

                        [No] [int] IDENTITY(1,1) NOT NULL,  

                        [A] [nvarchar](50) NOT NULL,  

                        [B] [nvarchar](50) NOT NULL,  

                        [C] [nvarchar](50) NOT NULL,  

                        [D] [numeric](18, 4) NOT NULL,  

                        [E] [numeric](18, 4) NOT NULL,  

                        [F] [nvarchar](50) NOT NULL,  

                        [G] [numeric](18, 4) NOT NULL,  

                        [H] [numeric](18, 4) NOT NULL,  

                        LastUpdate Datetime  NOT NULL

                        )


                        insert into #date (valuedate)
                        SELECT  TOP (DATEDIFF(DAY, @datefrom, @dateto) + 1) Dates = DATEADD(DAY, ROW_NUMBER() OVER(ORDER BY a.object_id) - 1, @datefrom)
                        FROM sys.all_objects a CROSS JOIN sys.all_objects b

                        delete from #date where dbo.[CheckTodayIsHoliday](valuedate) = 1

                        DECLARE DATE CURSOR FOR  

                        select distinct valuedate from #date

                        OPEN DATE 
                        FETCH NEXT FROM DATE 
                        INTO @Date

                        While @@Fetch_Status  = 0 
                        BEGIN


                        if exists(Select * from MKBDTrails where status = 2 and ValueDate = @Date)

                        BEGIN

                        select 'NAWC Already Generated and Approved for this Date' Msg

                        return

                        END



                        if exists(select * from company where status = 2)

                        BEGIN

                        if exists(Select * from Company Where (ID ='' or ID is null or Name ='' or Name is null or DirectorOne ='' or DirectorOne is null) and status = 2)

                        BEGIN

                        select 'Master Company has no data in Field ID/Name/Director One,Please Check it' Msg

                        return

                        END

                        ELSE

                        BEGIN

                        Select @CompanyID = ID, @CompanyName = Name, @DirectorName = DirectorOne from Company (nolock) 

                        where status = 2  

                        Select @MsgSuccess = 'Company Check Success  <br/> '

                        END

                        END

                        ELSE

                        BEGIN

                        select 'Master Company has no Approved Data,Please Check it' Msg

                        return

                        END

  



                        select @PeriodPK = periodPK     

                        from Period (nolock) where @Date between Datefrom and Dateto and status = 2  




                        select @TotalEquity = isnull([172B],0) 

                        from MKBD02 A (nolock) LEFT JOIN MKBDTrails B on A.MKBDTrailsPK = B.MKBDTrailsPK and B.status in (1,2) and B.BitValidate = 1

                        where A.Date = dbo.FWorkingDay(@Date, -1) 




                        --If @TotalEquity = 0 OR @TotalEquity Is null

                        --BEGIN

                        --	select 'Total Equity in MKBD02 Yesterday is 0 or Not Generate yet,Please Check it' Msg

                        --	return

                        --END



                        Select @MsgSuccess = @MsgSuccess + 'Total Equity Check Success <br/> '



                        Select @NewMKBDTrailsPK = isnull(Max(MKBDTrailsPK),0) + 1 From MKBDTrails



                        Insert into MKBDTrails(MKBDTrailsPK,historyPK,status,Notes,BitValidate,ValueDate,

                        LogMessages,LastUsersToText,ToTextTime,GenerateToTextCount,EntryUsersID,EntryTime)

                        Select @NewMKBDTrailsPK,1,@Status,'',0,@Date,'','',null,0,@UsersID,@timeNow



                        -- INIT MKBD 01,02,03,08,09



                        Insert into MKBD01(MKBDTrailsPK,Date,CompanyID,CompanyName,DirectorName,LastUpdate)

                        Select @NewMKBDTrailsPK,@Date,@CompanyID,@CompanyName,@DirectorName,@timeNow



                        Insert into MKBD02(MKBDTrailsPK,Date,CompanyID,CompanyName,DirectorName,LastUpdate)

                        Select @NewMKBDTrailsPK,@Date,@CompanyID,@CompanyName,@DirectorName,@timeNow



                        Insert into MKBD03(MKBDTrailsPK,Date,CompanyID,CompanyName,DirectorName,LastUpdate)

                        Select @NewMKBDTrailsPK,@Date,@CompanyID,@CompanyName,@DirectorName,@timeNow



                        Insert into MKBD08(MKBDTrailsPK,Date,CompanyID,CompanyName,DirectorName,LastUpdate)

                        Select @NewMKBDTrailsPK,@Date,@CompanyID,@CompanyName,@DirectorName,@timeNow



                        Insert into MKBD09(MKBDTrailsPK,Date,CompanyID,CompanyName,DirectorName,LastUpdate)

                        Select @NewMKBDTrailsPK,@Date,@CompanyID,@CompanyName,@DirectorName,@timeNow



                        declare @msg nvarchar(1000)



                        ---- Portfolio Checking REPO



 --                       IF EXISTS(Select * from TrxRepo Where Posted = 1 and BuyBackDate <= @Date and TrxType = 1)

 --                       BEGIN

 --                       set @msg = ''

 --                       Select distinct @msg = @msg + B.ID + ', ' from TrxRepo A

 --                       left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2

 --                       where A.Posted = 1 and A.BuyBackDate <= @Date and TrxType = 1 and A.instrumentPK not in

 --                       (

 --                       Select InstrumentPK

 --                       from ClosePrice 

 --                       where status = 2 and Date = @Date

 --                       )

 --                       if @@Rowcount > 0

 --                       BEGIN

 --                       Select 'Cek Close Price untuk Data : ' + @msg Msg

 --                       update MKBDTrails set LogMessages = 'Cek Close Price untuk Data : ' + @msg where MKBDTrailsPK = @NEWMKBDTrailsPK

 --                       return

 --                       END

 --                       ELSE

 --                       BEGIN

 --                       Select @MsgSuccess = @MsgSuccess + 'Company has REPO <br/> '	

 --                       Insert Into RL510ARepo (HistoryPK,Status,Notes,MKBDTrailsPK,Date,CounterPartPK,InstrumentPK,InstrumentTypePK,SellDate,

 --                       BuyBackDate,SellAmount,Expense,BuyBackAmount,HaircutPercent,Volume,ClosePrice,MarketValue,RankingLiabilities,MKBD01,MKBD02,MKBD03,MKBD09)

 --                       Select 1,2,'',@NewMKBDTrailsPK,@Date,CounterPartPK,A.InstrumentPK,B.InstrumentTypePK,SellDate,BuyBackDate,SellAmount,Expense,BuyBackAmount,

 --                       Case when d.type = 1 then HcEquity.RGHaircutMKBD when d.type = 2 then HcObl.OBHaircutMKBD when d.type = 5 then HcSBN.SBNHaircutMKBD else 0 End

 --                       ,Volume,

 --                       C.ClosePriceValue,Volume * C.ClosePriceValue, .05 * BuyBackAmount

 --                       ,Case when d.type = 1 then HcEquity.RGMKBD01 when d.type = 2 then HcObl.OBMKBD01 when d.type = 5 then HcSBN.SBNMKBD01 else 0 End

 --                       ,Case when d.type = 1 then HcEquity.RGMKBD02 when d.type = 2 then HcObl.OBMKBD02 when d.type = 5 then HcSBN.SBNMKBD02 else 0 End

 --                       ,Case when d.type = 1 then HcEquity.RGMKBD03 when d.type = 2 then HcObl.OBMKBD03 when d.type = 5 then HcSBN.SBNMKBD03 else 0 End

 --                       ,Case when d.type = 1 then HcEquity.RGMKBD09 when d.type = 2 then HcObl.OBMKBD09 when d.type = 5 then HcSBN.SBNMKBD09 else 0 End

		

 --                       from TrxRepo A

 --                       left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2

 --                       left join ClosePrice C on A.InstrumentPk = C.InstrumentPK and C.Status = 2

 --                       Left join InstrumentType D on B.InstrumentTypePK = D.Type and D.status = 2

		

 --                       Left join 

 --                       (

 --                       select InstrumentTypePK, HaircutFrom, HaircutTo, HaircutMKBD as RGHaircutMKBD, ConcentrationRisk as RGConcentrationRisk,    

 --                       MKBD01 as RGMKBD01, MKBD02 as RGMKBD02, MKBD03 as RGMKBD03, MKBD09 as RGMKBD09    

 --                       from MKBDSetup (nolock) A

 --                       where [Date] = 

 --                       (    

 --                       select Max([Date]) as MaxDate from MKBDSetup (nolock)    

 --                       where [Date] <= @Date and rlName = 'Rl510A'    

 --                       )and rlName = 'Rl510A' and InstrumentTypePK = 1 and status = 2

 --                       ) as HcEquity ON D.Type = HcEquity.InstrumentTypePK and [dbo].[FGetLastNAWCHaircut](@Date,A.InstrumentPK) = HcEquity.RGHaircutMKBD  

		

 --                       Left Join

 --                       (            

 --                       select InstrumentTypePK, BondRating, HaircutMKBD as OBHaircutMKBD, ConcentrationRisk as OBConcentrationRisk,            

 --                       MKBD01 as OBMKBD01, MKBD02 as OBMKBD02, MKBD03 as OBMKBD03, MKBD09 as OBMKBD09            

 --                       from MKBDSetup  (nolock)            

 --                       where [Date] = (                

 --                       select Max([Date]) as MaxDate from MKBDSetup (nolock)                

 --                       where [Date] <= @Date and RLName = 'Rl510A'                

 --                       )and RLName = 'Rl510A' and InstrumentTypePK = 2 and status = 2

 --                       ) as HcObl     

 --                       ON D.Type = HcObl.InstrumentTypePK and     

 --                       dbo.[FGetLastOBRating](@Date, A.InstrumentPK) = HcObl.BondRating 

	    

 --                       Left Join

 --                       (

 --                       select InstrumentTypePK, SBNTenorFrom, SBNTenorTo, HaircutMKBD as SBNHaircutMKBD, ConcentrationRisk as SBNConcentrationRisk,      

 --                       MKBD01 as SBNMKBD01, MKBD02 as SBNMKBD02, MKBD03 as SBNMKBD03, MKBD09 as SBNMKBD09      

 --                       from MKBDSetup (nolock)      

 --                       where [Date] =       

 --                       (      

 --                       select Max([Date]) as MaxDate       

 --                       from MKBDSetup (nolock)      

 --                       where [Date] <= @Date and RLName = 'Rl510A'       

 --                       ) and RLName = 'Rl510A' and InstrumentTypePK = 5  and status = 2

 --                       ) as HcSBN ON --St.StockType = HcSBN.StockType and     

 --                       cast(datediff(dd, @Date, isnull(B.MaturityDate,@Date)) as float)/365 between HcSBN.SBNTenorFrom and HcSBN.SBNTenorTo       

		

 --                       Where Posted = 1 and BuyBackDate <= @Date and TrxType = 1

	



 --                       -- RL510 A Update MKBD 01

 --                       set @ssql = ''

 --                       declare A cursor for    

 --                       select cast(MKBD01 as varchar) + 'B', sum(MarketValue) as Value    

 --                       from RL510aRepo (nolock) where date = @date and MKBD01 > 0 

 --                       and MKBDTrailsPK = @NewMKBDTrailsPK

 --                       group by MKBD01    



 --                       union all    



 --                       select cast(rlb.MKBD01 as varchar) + 'B', -sum(MarketValue) as Value    

 --                       from RL510aRepo rla (nolock)    

 --                       left join     

 --                       (    

 --                       select MKBD01, InstrumentPK from RL510cEquity (nolock) where date = @date and MKBD01 <> 0 	and MKBDTrailsPK = @NewMKBDTrailsPK    

 --                       union all    

 --                       select MKBD01, InstrumentPK from RL510cSBN (nolock) where date = @date and MKBD01 <> 0 	and MKBDTrailsPK = @NewMKBDTrailsPK   

 --                       union all    

 --                       select MKBD01, InstrumentPK from RL510cBond (nolock) where date = @date and MKBD01 <> 0 and MKBDTrailsPK = @NewMKBDTrailsPK

 --                       ) rlb on rla.InstrumentPK = rlb.InstrumentPK     

 --                       where date = @date and rlb.MKBD01 > 0 

 --                       and MKBDTrailsPK = @NewMKBDTrailsPK

 --                       group by rlb.MKBD01    



 --                       open A    



 --                       fetch next from A    

 --                       into @field, @value    



 --                       while @@FETCH_STATUS = 0    

 --                       begin     

 --                       set @ssql = 'update MKBD01 set [' + @field + '] = ' + cast(@value as varchar) + ' where date = ''' +  convert(varchar(8),@date,112) + ''''  + ' and MKBDTrailsPK = ' +  cast(@NewMKBDTrailsPK as varchar)     

 --                       exec (@ssql)    



 --                       fetch next from A    

 --                       into @field, @value    

 --                       end    



 --                       close A    

 --                       deallocate A   

	

 --                       -- RL510 A Update MKBD 02

 --                       set @ssql = ''

 --                       declare A cursor for    

 --                       select cast(MKBD02 as varchar) + 'B', sum(MarketValue) as Value    

 --                       from RL510aRepo (nolock) where date = @date and MKBD02 > 0 

 --                       and MKBDTrailsPK = @NewMKBDTrailsPK

 --                       group by MKBD02    



 --                       open A    



 --                       fetch next from A    

 --                       into @field, @value    



 --                       while @@FETCH_STATUS = 0    

 --                       begin     

 --                       set @ssql = 'update MKBD02 set [' + @field + '] = ' + cast(@value as varchar) + ' where date = ''' +  convert(varchar(8),@date,112) + '''' + ' and MKBDTrailsPK = ' +  cast(@NewMKBDTrailsPK as varchar)        

 --                       exec (@ssql)    



 --                       fetch next from A    

 --                       into @field, @value    

 --                       end    



 --                       close A    

 --                       deallocate A   

	

 --                       -- RL510 A Update MKBD 03

 --                       set @ssql = ''

 --                       declare A cursor for  

 --                       select cast(MKBD03 as varchar) + 'B', sum(RankingLiabilities) as Value  

 --                       from RL510aRepo (nolock) where date = @date and MKBD03 > 0 

 --                       and MKBDTrailsPK = @NewMKBDTrailsPK

 --                       group by MKBD03  



 --                       open A  



 --                       fetch next from A  

 --                       into @field, @value  



 --                       while @@FETCH_STATUS = 0  

 --                       begin   

 --                       set @ssql = 'update MKBD03 set [' + @field + '] = ' + cast(@value as varchar) + ' where date = ''' +  convert(varchar(8),@date,112) + ''''  + ' and MKBDTrailsPK = ' +  cast(@NewMKBDTrailsPK as varchar)        

 --                       exec (@ssql)  



 --                       fetch next from A  

 --                       into @field, @value  

 --                       end  



 --                       close A  

 --                       deallocate A   

	

 --                       -- RL510 A Update MKBD 09

 --                       set @ssql = ''

 --                       declare A cursor for    

 --                       select cast(MKBD09 as varchar) + 'E', sum(MarketValue) as Value    

 --                       from RL510aRepo (nolock) where date = @date and MKBD09 > 0 

 --                       and MKBDTrailsPK = @NewMKBDTrailsPK

 --                       group by MKBD09    

 --                       union all  

 --                       select cast(MKBD09 as varchar) + 'G', sum(MarketValue * haircutpercent) as Value    

 --                       from RL510aRepo (nolock) where date = @date and MKBD09 > 0 

 --                       and MKBDTrailsPK = @NewMKBDTrailsPK

 --                       group by MKBD09   

 --                       open A    



 --                       fetch next from A    

 --                       into @field, @value    



 --                       while @@FETCH_STATUS = 0    

 --                       begin     

 --                       set @ssql = 'update MKBD09 set [' + @field + '] = ' + cast(@value as varchar) + ' where date = ''' +  convert(varchar(8),@date,112) + ''''+ ' and MKBDTrailsPK = ' +  cast(@NewMKBDTrailsPK as varchar)   

 --                       exec (@ssql)    



 --                       fetch next from A    

 --                       into @field, @value    

 --                       end    



 --                       close A    

 --                       deallocate A   

	

 --                       END

 --                       END



 --                       -- Portfolio Checking REVERSE REPO



 --                       IF EXISTS(Select * from TrxRepo Where Posted = 1 and SellBackDate <= @Date and TrxType = 2)

 --                       BEGIN

 --                       set @msg = ''

 --                       Select distinct @msg = @msg + B.ID + ', ' from #TrxRepo A

 --                       left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2

 --                       where A.Posted = 1 and A.SellBackDate <= @Date and TrxType = 2 and A.instrumentPK not in

 --                       (

 --                       Select InstrumentPK

 --                       from ClosePrice 

 --                       where status = 2 and Date = @Date

 --                       )

 --                       if @@Rowcount > 0

 --                       BEGIN

 --                       Select 'Cek Close Price untuk Data : ' + @msg Msg

 --                       update MKBDTrails set LogMessages = 'Cek Close Price untuk Data : ' + @msg where MKBDTrailsPK = @NEWMKBDTrailsPK

 --                       return

 --                       END

 --                       ELSE

 --                       BEGIN

 --                       select * from RL510BReverseRepo

 --                       Select @MsgSuccess = @MsgSuccess + 'Company has REVERSE REPO <br/> '	

 --                       Insert Into RL510BReverseRepo (HistoryPK,Status,Notes,MKBDTrailsPK,Date,CounterPartPK,InstrumentPK,InstrumentTypePK,BuyDate,

 --                       SellBackDate,BuyAmount,Income,SellBackAmount,HaircutPercent,Volume,ClosePrice,MarketValue,AmountAfterHaircut,SellBackAmountAdjusted

 --                       ,RankingLiabilities,MKBD01,MKBD02,MKBD03,MKBD09)

 --                       Select 1,2,'',@NewMKBDTrailsPK,@Date,CounterPartPK,A.InstrumentPK,B.InstrumentTypePK,BuyDate,SellBackDate,BuyAmount,Income,SellBackAmount,

 --                       Case when d.type = 1 then HcEquity.RGHaircutMKBD when d.type = 2 then HcObl.OBHaircutMKBD when d.type = 5 then HcSBN.SBNHaircutMKBD else 0 End

 --                       ,Volume,

 --                       C.ClosePriceValue,Volume * C.ClosePriceValue,0,0,0

 --                       ,Case when d.type = 1 then HcEquity.RGMKBD01 when d.type = 2 then HcObl.OBMKBD01 when d.type = 5 then HcSBN.SBNMKBD01 else 0 End

 --                       ,Case when d.type = 1 then HcEquity.RGMKBD02 when d.type = 2 then HcObl.OBMKBD02 when d.type = 5 then HcSBN.SBNMKBD02 else 0 End

 --                       ,Case when d.type = 1 then HcEquity.RGMKBD03 when d.type = 2 then HcObl.OBMKBD03 when d.type = 5 then HcSBN.SBNMKBD03 else 0 End

 --                       ,Case when d.type = 1 then HcEquity.RGMKBD09 when d.type = 2 then HcObl.OBMKBD09 when d.type = 5 then HcSBN.SBNMKBD09 else 0 End

		

 --                       from TrxRepo A

 --                       left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2

 --                       left join ClosePrice C on A.InstrumentPk = C.InstrumentPK and C.Status = 2

 --                       Left join InstrumentType D on B.InstrumentTypePK = D.Type and D.status = 2

		

 --                       Left join 

 --                       (

 --                       select InstrumentTypePK, HaircutFrom, HaircutTo, HaircutMKBD as RGHaircutMKBD, ConcentrationRisk as RGConcentrationRisk,    

 --                       MKBD01 as RGMKBD01, MKBD02 as RGMKBD02, MKBD03 as RGMKBD03, MKBD09 as RGMKBD09    

 --                       from MKBDSetup (nolock) A

 --                       where [Date] = 

 --                       (    

 --                       select Max([Date]) as MaxDate from MKBDSetup (nolock)    

 --                       where [Date] <= @Date and rlName = 'Rl510B'    

 --                       )and rlName = 'Rl510B' and InstrumentTypePK = 1 and status = 2

 --                       ) as HcEquity ON D.Type = HcEquity.InstrumentTypePK and [dbo].[FGetLastNAWCHaircut](@Date,A.InstrumentPK) = HcEquity.RGHaircutMKBD  

		

 --                       Left Join

 --                       (            

 --                       select InstrumentTypePK, BondRating, HaircutMKBD as OBHaircutMKBD, ConcentrationRisk as OBConcentrationRisk,            

 --                       MKBD01 as OBMKBD01, MKBD02 as OBMKBD02, MKBD03 as OBMKBD03, MKBD09 as OBMKBD09            

 --                       from MKBDSetup  (nolock)            

 --                       where [Date] = (                

 --                       select Max([Date]) as MaxDate from MKBDSetup (nolock)                

 --                       where [Date] <= @Date and RLName = 'Rl510B'                

 --                       )and RLName = 'Rl510B' and InstrumentTypePK = 2 and status = 2

 --                       ) as HcObl     

 --                       ON D.Type = HcObl.InstrumentTypePK and     

 --                       dbo.[FGetLastOBRating](@Date, A.InstrumentPK) = HcObl.BondRating 

	    

 --                       Left Join

 --                       (

 --                       select InstrumentTypePK, SBNTenorFrom, SBNTenorTo, HaircutMKBD as SBNHaircutMKBD, ConcentrationRisk as SBNConcentrationRisk,      

 --                       MKBD01 as SBNMKBD01, MKBD02 as SBNMKBD02, MKBD03 as SBNMKBD03, MKBD09 as SBNMKBD09      

 --                       from MKBDSetup (nolock)      

 --                       where [Date] =       

 --                       (      

 --                       select Max([Date]) as MaxDate       

 --                       from MKBDSetup (nolock)      

 --                       where [Date] <= @Date and RLName = 'Rl510B'       

 --                       ) and RLName = 'Rl510B' and InstrumentTypePK = 5  and status = 2

 --                       ) as HcSBN ON --St.StockType = HcSBN.StockType and     

 --                       cast(datediff(dd, @Date, isnull(B.MaturityDate,@Date)) as float)/365 between HcSBN.SBNTenorFrom and HcSBN.SBNTenorTo       

		

 --                       Where Posted = 1 and SellBackDate <= @Date and TrxType = 2

		

 --                       -- UPDATE RL

 --                       Update A Set

 --                       A.AmountAfterHaircut = A.MarketValue * (1 - A.HaircutPercent/100) ,

 --                       A.SellBackAmountAdjusted = isnull(Case When B.Type = 1 Then 1.2 When B.Type = 2 then 1.1

 --                       When B.Type = 5 then 1.05 else 0 END,0) * SellBackAmount,

 --                       RankingLiabilities = CASE WHEN SellBackAmountAdjusted > AmountAfterHaircut      

 --                       THEN SellBackAmountAdjusted - AmountAfterHaircut ELSE 0 END

 --                       From RL510BReverseRepo A LEFT JOIN InstrumentType B on A.InstrumentTypePK = B.InstrumentTypePK

 --                       and B.Status = 2

 --                       WHERE A.MKBDTrailsPK = @NewMKBDTrailsPK

		

 --                       -- RL510 B Update MKBD01

 --                       Set @ssql = ''

 --                       declare A cursor for      

 --                       --select cast(MKBD01 as varchar) + 'B', sum(BuyAmount + Income) as Value  

 --                       select cast(MKBD01 as varchar) + 'B', ([BuyAmount])  as Value       

 --                       from RL510bReverseRepo (nolock) where date = @date and MKBD01 > 0 

 --                       and MKBDTrailsPK = @NewMKBDTrailsPK 

 --                       group by MKBD01, BuyAmount      

 --                       open A      



 --                       fetch next from A      

 --                       into @field, @value      



 --                       while @@FETCH_STATUS = 0      

 --                       begin       

 --                       set @ssql = 'update MKBD01 set [' + @field + '] = ' + cast(@value as varchar) + ' where date = ''' +  convert(varchar(8),@date,112) + ''''+ ' and MKBDTrailsPK = ' +  cast(@NewMKBDTrailsPK as varchar)                  

 --                       exec (@ssql)      



 --                       fetch next from A      

 --                       into @field, @value      

 --                       end      



 --                       close A      

 --                       deallocate A   

		

 --                       -- RL510 B Update MKBD03

 --                       set @ssql = ''

 --                       declare A cursor for  

 --                       select cast(MKBD03 as varchar) + 'B', sum(RankingLiabilities) as Value  

 --                       from RL510bReverseRepo (nolock) where date = @date and MKBD03 > 0 

 --                       and MKBDTrailsPK = @NewMKBDTrailsPK 

 --                       group by MKBD03  



 --                       open A  



 --                       fetch next from A  

 --                       into @field, @value  



 --                       while @@FETCH_STATUS = 0  

 --                       begin   

 --                       set @ssql = 'update MKBD03 set [' + @field + '] = ' + cast(@value as varchar) + ' where date = ''' +  convert(varchar(8),@date,112) + ''''  + ' and MKBDTrailsPK = ' +  cast(@NewMKBDTrailsPK as varchar)                  

 --                       exec (@ssql)  



 --                       fetch next from A  

 --                       into @field, @value  

 --                       end  



 --                       close A  

 --                       deallocate A 

		

 --                       END

 --                       END

                        -- Portfolio Checking REKSADANA

                        -- itung volume reksadana terakhir klo lebih > 0 baru jalan bawah

                        -- GENERATE MKBDLIFO CUSTOM VAM
                        IF(@ClientCode = '25')
                        BEGIN
                            exec GenerateMKBDLIFO @Date,@NewMKBDTrailsPK
                        END
                      

                        delete #tmpReksadana where ValueDate <> @Date

                        Insert into #tmpReksadana

                        Select A.InstrumentPK,isnull(sum(A.BuyVolume) - sum(A.SellVolume),0) LastVolume,@Date

                        from (

                        select InstrumentPK,sum(Volume) BuyVolume,0 SellVolume from trxPortfolio

                        where ValueDate <= @Date and Posted = 1 and InstrumentTypePK = 4 and trxType = 1 and Revised = 0 and status = 2

                        Group By InstrumentPK

                        UNION ALL

                        select InstrumentPK,0 BuyVolume,sum(Volume) SellVolume from trxPortfolio

                        where ValueDate <= @Date and Posted = 1 and InstrumentTypePK = 4 and trxType = 2 and Revised = 0 and status = 2

                        Group By InstrumentPK

                        )A

                        Group By A.InstrumentPK

						--tambahan dari boris untuk dormant
                        having sum(A.BuyVolume) - sum(A.SellVolume) > 1

                        if Exists

                        (

                        select * from #tmpReksadana	

                        )

                        BEGIN

                        -- Cek Close NAV and TotalNAVReksadana Sudah ada apa belum di hari generate

                        Declare @SubsAmount numeric(18,2)

                        select @SubsAmount = case when @ClientCode = '03' then isnull(sum(Amount),0) else 0 end from TrxPortfolio where InstrumentTypePK = 4 and ValueDate = @Date 
                        and status = 2 and posted = 1 and revised = 0 and TrxType = 1


                        set @msg = ''

                        Select distinct @msg = @msg + B.ID + ', ' from #tmpReksadana A

                        left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2

                        where A.instrumentPK not in

                        (

                        Select InstrumentPK

                        from ClosePrice 

                        where status = 2 and Date = @Date and ClosePriceValue >= 0  and TotalNAVReksadana > 0

                        )

                        if @@Rowcount > 0

                        BEGIN

                        Select '1.Cek Close Price untuk Data : ' + @msg Msg

                        update MKBDTrails set LogMessages = 'Cek Close Price untuk Data : ' + @msg where MKBDTrailsPK = @NEWMKBDTrailsPK

                        return

                        END

                        ELSE

                        BEGIN

                        -- Insert Data TO RL504 Reksadana From Portfolio join with MKBDSetup

                        Select @MsgSuccess = @MsgSuccess + 'Company has Reksadana <br/> '		



                        Insert into RL504(HistoryPK,Status,Notes,MKBDTrailsPK,Date,InstrumentPK,

                        ReksadanaTypePK,Volume,CloseNAV,MarketValue,TotalNAVReksadana,HaircutPercent,HaircutAmount,

                        ConcentrationRisk,ConcentrationLimit,affiliated,MKBD01,MKBD02,MKBD03,MKBD09)


                        Select 1,2,'',@NewMKBDTrailsPK,@Date,A.InstrumentPK,B.ReksadanaTypePK,A.LastVolume,

                        C.ClosePriceValue * isnull(F.Rate,1) ClosePriceValue,A.LastVolume * C.ClosePriceValue  * isnull(F.Rate,1) MV,C.TotalNAVReksadana + @SubsAmount,E.RDHaircutMKBD,

                        (C.ClosePriceValue * A.LastVolume * isnull(E.RDHaircutMKBD/100,0) * isnull(F.Rate,1)),E.RDConcentrationRisk,

                        E.RDConcentrationRisk * (C.TotalNAVReksadana + @SubsAmount),B.Affiliated,E.RDMKBD01,E.RDMKBD02,E.RDMKBD03,E.RDMKBD09

                        From #tmpReksadana A 

                        Left Join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2

                        Left Join ClosePrice C on A.InstrumentPK = C.InstrumentPK and C.Status = 2 and c.Date = @Date

                        Left join ReksadanaType D on B.ReksadanaTypePK = D.ReksadanaTypePK and D.Status = 2

                        Left Join CurrencyRate F on B.CurrencyPK = F.CurrencyPK and F.Status = 2 and F.Date = @Date

                        Left Join (              

                        SELECT  ReksadanaTypePK,InstrumentTypePK, HaircutMKBD AS RDHaircutMKBD, ConcentrationRisk AS RDConcentrationRisk,                  

                        MKBD01 AS RDMKBD01, MKBD02 AS RDMKBD02, MKBD03 AS RDMKBD03, MKBD09 AS RDMKBD09             

                        FROM MKBDSetup (NOLOCK)              

                        WHERE RLName = 'RL504' and Status = 2 and [Date] = (  

                        select Max([Date]) as MaxDate from MKBDSetup (nolock)      

                        where [Date] <= @date and RLName = 'RL504' and status = 2     

                        )

                        )AS E ON  D.ReksadanaTypePK = E.ReksadanaTypePK

		

                        -- UPDATE RL504 RANGKING LIABILITIES

                        update rl      

                        set rl.RankingLiabilities =         

                        case when MarketValue > ConcentrationLimit           

                        then MarketValue - ConcentrationLimit else 0    end           

                        from RL504 rl     

                        where rl.Date = @Date and MKBDTrailsPK = @NewMKBDTrailsPK

		

                        -- INSERT TO MKBD04

                        Declare @No int


		                        

		

                        DECLARE A CURSOR FOR  

                        select MKBDNo  

                        from ReksadanaType (nolock)  

                        where status = 2 

                        order by mkbdno  



                        OPEN A  

		  

                        FETCH NEXT FROM A  

                        INTO @No   

		  

                        WHILE @@FETCH_STATUS = 0  

                        BEGIN  

		

                        truncate table #MKBD04

                        insert into #MKBD04  

                        (MKBDTrailsPK,Date, CompanyID, CompanyName, DirectorName, MKBDNo,   

                        A, B, C, D, E, F, G, H,[lastUpdate])  



                        select @NewMKBDTrailsPK, @Date, @CompanyID, @CompanyName, @DirectorName, Rd.MKBDNo, 

                        rd.Description, isnull(IT.ISIN,''),case when RL.Affiliated is null then ''   

                        when RL.Affiliated = 0 then 'Tidak Terafiliasi'  

                        when RL.Affiliated = 1 then 'Afiliasi' end,   

                        isnull(MarketValue, 0), isnull(rl.TotalNAVReksadana, 0),  

                        'Kelebihan atas ' + cast(cast(Hc.ConcentrationRisk * 100 as int) as varchar) + '% NAB',  

                        isnull(ConcentrationLimit, 0), isnull(RankingLiabilities, 0),@timeNow

                        from ReksadanaType Rd  

			 

                        left join RL504 rl on rl.ReksadanaTypePK = Rd.ReksadanaTypePK and rl.Date = @Date   

                        left join Instrument IT on rl.InstrumentPK = IT.InstrumentPK  and IT.status = 2

                        left join   

                        (  

                        select ReksadanaTypePK, ConcentrationRisk   

                        from MKBDSetup (nolock)  

                        where RLName = 'RL504'  and status = 2

                        ) Hc on Rd.ReksadanaTypePK = Hc.ReksadanaTypePK  

                        where Rd.MKBDNo = @No and Rd.status = 2 and rl.MKBDTrailsPK = @NewMKBDTrailsPK



                        insert into MKBD04   

                        (MKBDTrailsPK, Date, CompanyID, CompanyName, DirectorName, [No],   

                        A, B, C, D, E, F, G, H, [LastUpdate])  

                        select MKBDTrailsPK, Date, CompanyID, CompanyName, DirectorName, cast(MKBDNo as varchar) + '.' + cast(No as varchar),   

                        A, B, C, D, E, F, G, H, @timeNow

                        from #MKBD04  

		   

                        FETCH NEXT FROM A  

                        INTO @No   

                        END  

		  

                        CLOSE A  

                        DEALLOCATE A 

		

                        -- RL Update MKBD 01

                        set @ssql = ''

		

                        declare A cursor for    

                        select cast(MKBD01 as varchar) + 'B', sum(MarketValue) as Value    

                        from RL504 (nolock) where date = @date and MKBD01 > 0 

                        and status =  2 and MKBDTrailsPK = @NewMKBDTrailsPK

                        group by MKBD01    



                        open A    



                        fetch next from A    

                        into @field, @value    



                        while @@FETCH_STATUS = 0    

                        begin     

                        set @ssql = 'update MKBD01 set [' + @field + '] = ' + cast(@value as varchar) + ' where date = ''' +  convert(varchar(8),@date,112) + '''' + ' and MKBDTrailsPK = ' +  cast(@NewMKBDTrailsPK as varchar)           

                        exec (@ssql)    



                        fetch next from A    

                        into @field, @value    

                        end    



                        close A    

                        deallocate A   

		

                        -- RL Update MKBD 03

                        set @ssql = ''

                        declare A cursor for  

                        select cast(MKBD03 as varchar) + 'B', case when @ClientCode = '25' then 0 else sum(RankingLiabilities) end Value  

                        from RL504 (nolock) where date = @date and MKBD03 > 0

                        and MKBDTrailsPK = @NewMKBDTrailsPK 

                        group by MKBD03  



                        open A  



                        fetch next from A  

                        into @field, @value  



                        while @@FETCH_STATUS = 0  

                        begin   

                        set @ssql = 'update MKBD03 set [' + @field + '] = ' + cast(@value as varchar) + ' where date = ''' +  convert(varchar(8),@date,112) + ''''  + ' and MKBDTrailsPK = ' +  cast(@NewMKBDTrailsPK as varchar)   

                        exec (@ssql)  



                        fetch next from A  

                        into @field, @value  

                        end  



                        close A  

                        deallocate A   

		

                        -- RL Update MKBD 09

                        set @ssql = ''

                        declare A cursor for    

                        select cast(MKBD09 as varchar) + 'E', sum(MarketValue) as Value    

                        from RL504 (nolock) where date = @date and MKBD09 > 0 

                        and MKBDTrailsPK = @NewMKBDTrailsPK

                        group by MKBD09    

                        Union all  

                        select cast(MKBD09 as varchar) + 'G', sum(HaircutAmount) as Value    

                        from RL504 (nolock) where date = @date and MKBD09 > 0 

                        and MKBDTrailsPK = @NewMKBDTrailsPK

                        group by MKBD09    

                        open A    



                        fetch next from A    

                        into @field, @value    



                        while @@FETCH_STATUS = 0    

                        begin     

                        set @ssql = 'update MKBD09 set [' + @field + '] = ' + cast(@value as varchar) + ' where date = ''' +  convert(varchar(8),@date,112) + ''''+ ' and MKBDTrailsPK = ' +  cast(@NewMKBDTrailsPK as varchar)         

                        exec (@ssql)    



                        fetch next from A    

                        into @field, @value    

                        end    



                        close A    

                        deallocate A   

				

                        END

                        END



                        -- Portfolio Checking DEPOSITO

                        delete #tmpDeposito where ValueDate <> @Date

                        Insert into #tmpDeposito

                        
                        Select A.InstrumentPK,isnull(sum(A.BuyAmount) - sum(A.SellAmount),0) LastAmount,MaturityDate,@Date from (

                        select InstrumentPK,sum(Amount) BuyAmount,0 SellAmount,MaturityDate from trxPortfolio

                        where ValueDate <= @Date and MaturityDate > @Date and Posted = 1 and InstrumentTypePK = 3 and trxType in (1,3) and Revised = 0 and status = 2

                        Group By InstrumentPK,MaturityDate

                        UNION ALL

                        select InstrumentPK,0 BuyAmount,sum(Amount) SellAmount,MaturityDate from trxPortfolio

                        where ValueDate <= @Date and MaturityDate > @Date and Posted = 1 and InstrumentTypePK = 3 and trxType = 2 and Revised = 0 and status = 2

                        Group By InstrumentPK,MaturityDate

                        )A

                        Group By A.InstrumentPK,MaturityDate

                        having sum(A.BuyAmount) - sum(A.SellAmount) > 0



                        if Exists

                        (

                        select * from #tmpDeposito	

                        )

                        BEGIN

                        Select @MsgSuccess = @MsgSuccess + 'Company has Deposito <br/> '

                        Insert into Rl510CDeposito(HistoryPK,Status,Notes,MKBDTrailsPK,Date,InstrumentPK,

                        DepositoTypePK,MarketValue,DueDate)

		

                        Select 1,2,'',@NewMKBDTrailsPK,@Date,A.InstrumentPK,B.DepositoTypePK,A.LastAmount,

                        A.MaturityDate From #tmpDeposito A

                        Left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2

		

                        --RL510CDepositoUpdateData

                        IF (@ClientCode = 17)
                        BEGIN

                            update rl     

                            set isUnderDuedateAmount = case when datediff(dd, Date, DueDate) <= TenorLimit then MarketValue else 0 end,    

                            notUnderDuedateAmount = case when datediff(dd, Date, DueDate) <= TenorLimit then 0 else MarketValue end,    

                            quaranteedAmount =     

                            case when datediff(dd, Date, DueDate) <= TenorLimit OR B.InterestPercent > 6.75  then 0     

                            else     

                            case when MarketValue >= CollateralLimit then CollateralLimit     

                            else MarketValue end     

                            end     

                            from RL510cDeposito rl    

                            left join DepositoType DnT on rl.DepositoTypePK = DnT.DepositoTypePK   

                            LEFT JOIN Instrument B ON rl.instrumentPK = B.InstrumentPK AND B.STATUS IN (1,2)

                            where DnT.DEPType in ('DEP-BUM') and rl.Date = @Date  and MKBDTrailsPK = @NewMKBDTrailsPK

                       
                        END
                        ELSE
                        BEGIN
                                -- NORMAL
                                update rl     

                                set isUnderDuedateAmount = case when datediff(dd, Date, DueDate) <= TenorLimit then MarketValue else 0 end,    

                                notUnderDuedateAmount = case when datediff(dd, Date, DueDate) <= TenorLimit then 0 else MarketValue end,    

                                quaranteedAmount =     

                                case when datediff(dd, Date, DueDate) <= TenorLimit  then 0     

                                else     

                                case when MarketValue >= CollateralLimit then CollateralLimit     

                                else MarketValue end     

                                end     

                                from RL510cDeposito rl    

                                left join DepositoType DnT on rl.DepositoTypePK = DnT.DepositoTypePK   

                                where DnT.DEPType in ('DEP-BUM') and rl.Date = @Date  and MKBDTrailsPK = @NewMKBDTrailsPK

                        END


                        update rl set   

                        NotLiquidatedAmount = case when isLiquidated = 0 then NotUnderDuedateAmount - QuaranteedAmount else 0 end,    

                        LiquidatedAmount = case when isLiquidated = 1 then NotUnderDuedateAmount - QuaranteedAmount else 0 end    

                        from RL510cDeposito rl    

                        left join DepositoType DnT on rl.DepositoTypePK = DnT.DepositoTypePK   

                        where DnT.DEPType in ('DEP-BUM') and rl.Date = @Date  and MKBDTrailsPK = @NewMKBDTrailsPK







                        IF (@ClientCode = 17)
                        BEGIN
                            UPDATE A SET A.QuaranteedAmount = 0,A.NotLiquidatedAmount = A.NotUnderDuedateAmount FROM dbo.RL510CDeposito A
                            LEFT JOIN Instrument B ON A.InstrumentPK = B.InstrumentPK AND B.status IN (1,2)
                            WHERE A.MKBDTrailsPK = @NewMKBDTrailsPK
                            AND B.BankPK in
                            (
                            SELECT B.BankPK FROM dbo.RL510CDeposito A
                            LEFT JOIN Instrument B ON A.InstrumentPK = B.InstrumentPK AND B.status IN (1,2)
                            LEFT JOIN Bank D ON B.BankPK = D.BankPK AND D.status IN (1,2)
                            WHERE MKBDTrailsPK = @NewMKBDTrailsPK AND B.InterestPercent <= 6.75
                            GROUP BY D.Name,B.BankPK
                            HAVING SUM(A.MarketValue) > 2000000000
                            )


                            UPDATE A SET A.QuaranteedAmount = 2000000000,A.NotLiquidatedAmount = A.NotUnderDuedateAmount - 2000000000 FROM dbo.RL510CDeposito A

                            WHERE A.RL510CDepositoPK IN
                            (
	                            SELECT TOP 1 A.RL510CDepositoPK FROM dbo.RL510CDeposito A
	                            LEFT JOIN Instrument B ON A.InstrumentPK = B.InstrumentPK AND B.status IN (1,2)
	                            WHERE A.MKBDTrailsPK = @NewMKBDTrailsPK
	                            AND B.BankPK in
	                            (
	                            SELECT B.BankPK FROM dbo.RL510CDeposito A
	                            LEFT JOIN Instrument B ON A.InstrumentPK = B.InstrumentPK AND B.status IN (1,2)
	                            LEFT JOIN Bank D ON B.BankPK = D.BankPK AND D.status IN (1,2)
	                            WHERE MKBDTrailsPK = @NewMKBDTrailsPK AND B.InterestPercent <= 6.75
	                            GROUP BY D.Name,B.BankPK
	                            HAVING SUM(A.MarketValue) > 2000000000
	                            )
                            )

                            UPDATE A SET A.IsUnderDuedateAmount = IsUnderDuedateAmount - A.QuaranteedAmount FROM dbo.RL510CDeposito A
				            where A.QuaranteedAmount > 0 and MKBDTrailsPK = @NewMKBDTrailsPK

                        END



                        insert into #IsUnderDueDateAmount(
                        [HistoryPK],[Status],[Notes],[Date],[MKBDTrailsPK],[MKBD01],
                        [MKBD02],[MKBD03],[MKBD09],[InstrumentPK],[DepositoTypePK],[IsLiquidated],[DueDate],[MarketValue],[IsUnderDuedateAmount],
                        [NotUnderDuedateAmount],[QuaranteedAmount],[NotLiquidatedAmount],[LiquidatedAmount],[HaircutPercent],[HaircutAmount],
                        [AfterHaircutAmount],[RankingLiabilities],[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[ApprovedUsersID],
                        [ApprovedTime],[VoidUsersID],[VoidTime],[DBUserID],[DBTerminalID],[LastUpdate],[LastUpdateDB])
    
                        select A.HistoryPK,A.Status,A.Notes,Date,MKBDTrailsPK,MKBD01,
                        MKBD02,MKBD03,MKBD09,InstrumentPK,A.DepositoTypePK,IsLiquidated,DueDate,MarketValue,IsUnderDuedateAmount,
                        NotUnderDuedateAmount,QuaranteedAmount,NotLiquidatedAmount,LiquidatedAmount,HaircutPercent,HaircutAmount,
                        AfterHaircutAmount,RankingLiabilities,A.EntryUsersID,A.EntryTime,A.UpdateUsersID,A.UpdateTime,A.ApprovedUsersID,
                        A.ApprovedTime,A.VoidUsersID,A.VoidTime,A.DBUserID,A.DBTerminalID,A.LastUpdate,A.LastUpdateDB


                        from RL510cDeposito A 

                        left join DepositoType B on A.DepositoTypePK = B.DepositoTypePK  

                        where DEPType in ('DEP-BUM') and Date = @Date and IsUnderDuedateAmount > 0    

                        and MKBD01 = 0 and MKBD09 = 0    

		

                        update rl set   

                        HaircutPercent = DEPHaircutMKBD, MarketValue = IsUnderDuedateAmount,    

                        HaircutAmount = IsUnderDuedateAmount * (DEPHaircutMKBD/100), AfterHaircutAmount = IsUnderDuedateAmount * (1 - (DEPHaircutMKBD/100)),    

                        rl.MKBD01 = isnull(DepMKBD01, 0), rl.MKBD02 = isnull(DepMKBD02, 0),    

                        rl.MKBD03 = isnull(DepMKBD03, 0), rl.MKBD09 = isnull(DepMKBD09, 0),    

                        NotUnderDuedateAmount = 0, QuaranteedAmount = 0, NotLiquidatedAmount = 0, LiquidatedAmount = 0    

                        from #IsUnderDueDateAmount rl    

                        left join DepositoType DnT on rl.DepositoTypePK = DnT.DepositoTypePK  and Dnt.Status = 2 

                        left join 

                        (    

                        select A.DepositoTypePK ,HaircutMKBD as DEPHaircutMKBD,     

                        MKBD01 as DepMKBD01, MKBD02 as DepMKBD02, MKBD03 as DepMKBD03, MKBD09 as DepMKBD09    

                        from MKBDSetup A (nolock) 

                        left join DepositoType B on A.DepositoTypePK = B.DepositoTypePK and B.status = 2   

                        where [Date] = (  

                        select Max([Date]) as MaxDate from MKBDSetup (nolock)    

                        where [Date] <= @Date and RLName = 'RL510cDeposito'    

                        ) and RLName = 'RL510cDeposito' and DEPType in ('DEP-BUM') and BitDEPisDue = 1 and A.Status = 2   

                        ) as HcDep ON DnT.DepositoTypePK = HcDep.DepositoTypePK 

		

                        insert into #QuaranteedAmount(
                        [HistoryPK],[Status],[Notes],[Date],[MKBDTrailsPK],[MKBD01],
                        [MKBD02],[MKBD03],[MKBD09],[InstrumentPK],[DepositoTypePK],[IsLiquidated],[DueDate],[MarketValue],[IsUnderDuedateAmount],
                        [NotUnderDuedateAmount],[QuaranteedAmount],[NotLiquidatedAmount],[LiquidatedAmount],[HaircutPercent],[HaircutAmount],
                        [AfterHaircutAmount],[RankingLiabilities],[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[ApprovedUsersID],
                        [ApprovedTime],[VoidUsersID],[VoidTime],[DBUserID],[DBTerminalID],[LastUpdate],[LastUpdateDB])
    
                        select A.HistoryPK,A.Status,A.Notes,Date,MKBDTrailsPK,MKBD01,
                        MKBD02,MKBD03,MKBD09,InstrumentPK,A.DepositoTypePK,IsLiquidated,DueDate,MarketValue,IsUnderDuedateAmount,
                        NotUnderDuedateAmount,QuaranteedAmount,NotLiquidatedAmount,LiquidatedAmount,HaircutPercent,HaircutAmount,
                        AfterHaircutAmount,RankingLiabilities,A.EntryUsersID,A.EntryTime,A.UpdateUsersID,A.UpdateTime,A.ApprovedUsersID,
                        A.ApprovedTime,A.VoidUsersID,A.VoidTime,A.DBUserID,A.DBTerminalID,A.LastUpdate,A.LastUpdateDB


                        from RL510cDeposito  A   

                        left join DepositoType B on A.DepositoTypePK = B.DepositoTypePK  

                        where DEPType in ('DEP-BUM') and Date = @Date and QuaranteedAmount > 0    

                        and MKBD01 = 0 and MKBD09 = 0    



                        update rl set   

                        HaircutPercent = DEPHaircutMKBD, MarketValue = QuaranteedAmount,    

                        HaircutAmount = QuaranteedAmount * (DEPHaircutMKBD/100), AfterHaircutAmount = QuaranteedAmount * (1 - (DEPHaircutMKBD/100)),    

                        rl.MKBD01 = isnull(DepMKBD01, 0), rl.MKBD02 = isnull(DepMKBD02, 0),    

                        rl.MKBD03 = isnull(DepMKBD03, 0), rl.MKBD09 = isnull(DepMKBD09, 0),    

                        NotUnderDuedateAmount = QuaranteedAmount, IsUnderDuedateAmount = 0, NotLiquidatedAmount = 0, LiquidatedAmount = 0    

                        from #QuaranteedAmount rl    

                        left join DepositoType DnT on rl.DepositoTypePK = DnT.DepositoTypePK   

                        left join (    

                        select A.DepositoTypePK, HaircutMKBD as DEPHaircutMKBD, MKBD01 as DepMKBD01,   

                        MKBD02 as DepMKBD02, MKBD03 as DepMKBD03, MKBD09 as DepMKBD09    

                        from MKBDSetup A (nolock)  

                        left join DepositoType B on A.DepositoTypePK = B.DepositoTypePK and B.status = 2  

                        where [Date] = (  

                        select Max([Date]) as MaxDate from [MKBDSetup] (nolock)    

                        where [Date] <= @Date  and RLName = 'RL510cDeposito'    

                        )and RLName = 'RL510cDeposito'  and DEPType in ('DEP-BUM') and BitDEPisGuaranteed = 1    

                        ) as HcDep ON DnT.DepositoTypePK = HcDep.DepositoTypePK

		

                        insert into RL510cDeposito      

                        (HistoryPK,Status,Notes,MKBDTrailsPK,Date, MKBD01, MKBD02, MKBD03, MKBD09, InstrumentPK, DepositoTypePK, IsLiquidated, DueDate, MarketValue, IsUnderDuedateAmount,   

                        NotUnderDuedateAmount, QuaranteedAmount, NotLiquidatedAmount, LiquidatedAmount, HaircutPercent, HaircutAmount, AfterHaircutAmount, RankingLiabilities)      

                        select 1,2,'',@NewMKBDTrailsPK,Date, MKBD01, MKBD02, MKBD03, MKBD09, InstrumentPK, DepositoTypePK, IsLiquidated, DueDate, MarketValue, IsUnderDuedateAmount,   

                        NotUnderDuedateAmount, QuaranteedAmount, NotLiquidatedAmount, LiquidatedAmount, HaircutPercent, HaircutAmount, AfterHaircutAmount, RankingLiabilities         

                        from (   

                        select * from #IsUnderDueDateAmount    

                        union all    

                        select * from #QuaranteedAmount    

                        ) Dep 
                        --order by RL510CDepositoPK   





                        insert into #NotLiquidatedAmount(
                        [HistoryPK],[Status],[Notes],[Date],[MKBDTrailsPK],[MKBD01],
                        [MKBD02],[MKBD03],[MKBD09],[InstrumentPK],[DepositoTypePK],[IsLiquidated],[DueDate],[MarketValue],[IsUnderDuedateAmount],
                        [NotUnderDuedateAmount],[QuaranteedAmount],[NotLiquidatedAmount],[LiquidatedAmount],[HaircutPercent],[HaircutAmount],
                        [AfterHaircutAmount],[RankingLiabilities],[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[ApprovedUsersID],
                        [ApprovedTime],[VoidUsersID],[VoidTime],[DBUserID],[DBTerminalID],[LastUpdate],[LastUpdateDB])
    
                        select A.HistoryPK,A.Status,A.Notes,Date,MKBDTrailsPK,MKBD01,
                        MKBD02,MKBD03,MKBD09,InstrumentPK,A.DepositoTypePK,IsLiquidated,DueDate,MarketValue,IsUnderDuedateAmount,
                        NotUnderDuedateAmount,QuaranteedAmount,NotLiquidatedAmount,LiquidatedAmount,HaircutPercent,HaircutAmount,
                        AfterHaircutAmount,RankingLiabilities,A.EntryUsersID,A.EntryTime,A.UpdateUsersID,A.UpdateTime,A.ApprovedUsersID,
                        A.ApprovedTime,A.VoidUsersID,A.VoidTime,A.DBUserID,A.DBTerminalID,A.LastUpdate,A.LastUpdateDB


                        from RL510cDeposito A       

                        left join DepositoType B on A.DepositoTypePK = B.DepositoTypePK  and B.status = 2

                        where B.DEPType in ('DEP-BUM') and A.Date = @Date and A.NotLiquidatedAmount > 0      

                        and MKBD01 = 0 and MKBD09 = 0  



                        update rl set   

                        HaircutPercent = isnull(DEPHaircutMKBD,0), MarketValue = NotLiquidatedAmount,      

                        HaircutAmount = isnull(NotLiquidatedAmount * (DEPHaircutMKBD/100),0), AfterHaircutAmount = isnull(NotLiquidatedAmount * (1 - (DEPHaircutMKBD/100)),0),      

                        rl.MKBD01 = isnull(DepMKBD01, 0), rl.MKBD02 = isnull(DepMKBD02, 0),      

                        rl.MKBD03 = isnull(DepMKBD03, 0), rl.MKBD09 = isnull(DepMKBD09, 0),      

                        NotUnderDuedateAmount = NotLiquidatedAmount, IsUnderDuedateAmount = 0, QuaranteedAmount = 0, LiquidatedAmount = 0      

                        from #NotLiquidatedAmount rl      

                        left join DepositoType DnT on rl.DepositoTypePK = DnT.DepositoTypePK   and Dnt.Status = 2

                        left join

                        (      

                        select  A.DepositoTypePK , HaircutMKBD as DEPHaircutMKBD,       

                        MKBD01 as DepMKBD01, MKBD02 as DepMKBD02, MKBD03 as DepMKBD03, MKBD09 as DepMKBD09      

                        from MKBDSetup A (nolock) 

                        left join DepositoType B on A.DepositoTypePK = B.DepositoTypePK and B.status = 2

                        where RLName = 'RL510cDeposito' and B.DEPType in ('DEP-BUM') and BitDEPisGuaranteed = 0 and BitDEPisDue = 0 and BitDEPisPailit = 0      

                        and A.status = 2 and [Date] = (  

                        select Max([Date]) as MaxDate from MKBDSetup (nolock)      

                        where [Date] <= @Date and RLName = 'RL510cDeposito'  and status = 2     

                        )

                        ) as HcDep ON DnT.DepositoTypePK = HcDep.DepositoTypePK    

		

                        insert into #LiquidatedAmount(
                        [HistoryPK],[Status],[Notes],[Date],[MKBDTrailsPK],[MKBD01],
                        [MKBD02],[MKBD03],[MKBD09],[InstrumentPK],[DepositoTypePK],[IsLiquidated],[DueDate],[MarketValue],[IsUnderDuedateAmount],
                        [NotUnderDuedateAmount],[QuaranteedAmount],[NotLiquidatedAmount],[LiquidatedAmount],[HaircutPercent],[HaircutAmount],
                        [AfterHaircutAmount],[RankingLiabilities],[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[ApprovedUsersID],
                        [ApprovedTime],[VoidUsersID],[VoidTime],[DBUserID],[DBTerminalID],[LastUpdate],[LastUpdateDB])
    
                        select A.HistoryPK,A.Status,A.Notes,Date,MKBDTrailsPK,MKBD01,
                        MKBD02,MKBD03,MKBD09,InstrumentPK,A.DepositoTypePK,IsLiquidated,DueDate,MarketValue,IsUnderDuedateAmount,
                        NotUnderDuedateAmount,QuaranteedAmount,NotLiquidatedAmount,LiquidatedAmount,HaircutPercent,HaircutAmount,
                        AfterHaircutAmount,RankingLiabilities,A.EntryUsersID,A.EntryTime,A.UpdateUsersID,A.UpdateTime,A.ApprovedUsersID,
                        A.ApprovedTime,A.VoidUsersID,A.VoidTime,A.DBUserID,A.DBTerminalID,A.LastUpdate,A.LastUpdateDB
   

                        from RL510cDeposito A 

                        left join DepositoType B on A.DepositoTypePK = B.DepositoTypePK  and B.status = 2

                        where DEPType in ('DEP-BUM') and Date = @Date and LiquidatedAmount > 0      

                        and MKBD01 = 0 and MKBD09 = 0      



                        update rl set   

                        HaircutPercent = DEPHaircutMKBD, MarketValue = LiquidatedAmount,      

                        HaircutAmount = LiquidatedAmount * (DEPHaircutMKBD/100), AfterHaircutAmount = LiquidatedAmount * (1 - (DEPHaircutMKBD/100)),      

                        rl.MKBD01 = isnull(DepMKBD01, 0), rl.MKBD02 = isnull(DepMKBD02, 0),      

                        rl.MKBD03 = isnull(DepMKBD03, 0), rl.MKBD09 = isnull(DepMKBD09, 0),      

                        NotUnderDuedateAmount = LiquidatedAmount, IsUnderDuedateAmount = 0, QuaranteedAmount = 0, NotLiquidatedAmount = 0  

                        from #LiquidatedAmount rl      

                        left join DepositoType DnT on rl.DepositoTypePK = DnT.DepositoTypePK   and Dnt.Status = 2  

                        left join 

                        (      

                        select  A.DepositoTypePK , HaircutMKBD as DEPHaircutMKBD,       

                        MKBD01 as DepMKBD01, MKBD02 as DepMKBD02, MKBD03 as DepMKBD03, MKBD09 as DepMKBD09      

                        from MKBDSetup A (nolock) 

                        left join DepositoType B on A.DepositoTypePK = B.DepositoTypePK and B.status = 2

                        where RLName = 'RL510cDeposito' and B.DEPType in ('DEP-BUM') and BitDEPisPailit = 1      

                        and A.status = 2 and [Date] = (  

                        select Max([Date]) as MaxDate from MKBDSetup (nolock)      

                        where [Date] <= @Date and RLName = 'RL510cDeposito' and status = 2      

                        )

                        ) as HcDep ON DnT.DepositoTypePK = HcDep.DepositoTypePK

		

                        insert into RL510cDeposito      

                        (HistoryPK,Status,Notes,MKBDTrailsPK,Date, MKBD01, MKBD02, MKBD03, MKBD09, InstrumentPK, DepositoTypePK, IsLiquidated, DueDate, MarketValue, IsUnderDuedateAmount,   

                        NotUnderDuedateAmount, QuaranteedAmount, NotLiquidatedAmount, LiquidatedAmount, HaircutPercent, HaircutAmount, AfterHaircutAmount, RankingLiabilities)      

                        select 1,2,'',@NewMKBDTrailsPK,Date, MKBD01, MKBD02, MKBD03, MKBD09, InstrumentPK, DepositoTypePK, IsLiquidated, DueDate, MarketValue, IsUnderDuedateAmount,   

                        NotUnderDuedateAmount, QuaranteedAmount, NotLiquidatedAmount, LiquidatedAmount, HaircutPercent, HaircutAmount, AfterHaircutAmount, RankingLiabilities       

                        from (  

                        select * from #NotLiquidatedAmount      

                        union all      

                        select * from #LiquidatedAmount      

                        ) Dep 
                        --order by RL510CDepositoPK     



                        delete RL510cDeposito  

                        from RL510cDeposito       

                        left join DepositoType on RL510cDeposito.DepositoTypePK = DepositoType.DepositoTypePK  and  DepositoType.status = 2

                        where DEPType in ('DEP-BUM') and Date = @Date      

                        and MKBD01 = 0 and MKBD09 = 0   

		

                        --RL510CUpdateMKBD01

                        Set @ssql = ''

		

                        declare A cursor for    

                        select cast(MKBD01 as varchar) + 'B', sum(MarketValue) as Value    

                        from RL510cDeposito (nolock) where date = @date and MKBD01 > 0 

                        and status =  2 and MKBDTrailsPK = @NewMKBDTrailsPK

                        group by MKBD01    



                        open A    



                        fetch next from A    

                        into @field, @value    



                        while @@FETCH_STATUS = 0    

                        begin     

                        set @ssql = 'update MKBD01 set [' + @field + '] = ' + cast(@value as varchar) + ' where date = ''' +  convert(varchar(8),@date,112) + ''''+ ' and MKBDTrailsPK = ' +  cast(@NewMKBDTrailsPK as varchar)         

                        exec (@ssql)    



                        fetch next from A    

                        into @field, @value    

                        end      

	

		

                        close A    

                        deallocate A   

		

                        --RL510CUpdateMKBD09

		

                        Set @ssql = ''

                        declare A cursor for    

                        select cast(MKBD09 as varchar) + 'E', sum(MarketValue) as Value    

                        from RL510cDeposito (nolock) where date = @date and MKBD09 > 0 

                        and MKBDTrailsPK = @NewMKBDTrailsPK

                        group by MKBD09   

                        union all  

                        select cast(MKBD09 as varchar) + 'G', sum(haircutamount) as Value    

                        from RL510cDeposito (nolock) where date = @date and MKBD09 > 0 

                        and MKBDTrailsPK = @NewMKBDTrailsPK

                        group by MKBD09   

                        open A    

                        fetch next from A    

                        into @field, @value    



                        while @@FETCH_STATUS = 0    

                        begin     

                        set @ssql = 'update MKBD09 set [' + @field + '] = ' + cast(@value as varchar) + ' where date = ''' +  convert(varchar(8),@date,112) + ''''+ ' and MKBDTrailsPK = ' +  cast(@NewMKBDTrailsPK as varchar)     

                        exec (@ssql)    



                        fetch next from A    

                        into @field, @value    

                        end    



                        close A    

                        deallocate A   



                        END		

                        -- Portfolio Checking BOND , Corporate Bond berhubungan dengan RATING

                        

                        delete #tmpBond where ValueDate <> @Date

                        Insert into #tmpBond ([InstrumentPK],[Volume],ValueDate)

                        Select A.InstrumentPK,isnull(sum(A.BuyVolume) - sum(A.SellVolume),0) Volume,@Date from (

	                    select A.InstrumentPK,sum(Volume) BuyVolume,0 SellVolume from trxPortfolio A

	                    left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2

	                    left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.Status = 2

	                    where ValueDate <= @Date and Posted = 1 and c.InstrumentTypePK in (2,3,8,9,11,13,14,15) and trxType = 1 and Revised = 0 and A.status = 2 and A.MaturityDate >= @Date

	                    Group By A.InstrumentPK

	                    UNION ALL

	                    select A.InstrumentPK,0 BuyVolume,sum(Volume) SellVolume from trxPortfolio A

	                    left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2

	                    left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.Status = 2

	                    where ValueDate <= @Date and Posted = 1 and c.InstrumentTypePK  in (2,3,8,9,11,13,14,15)  and trxType = 2 and Revised = 0 and A.status = 2 and A.MaturityDate >= @Date

	                    Group By A.InstrumentPK

	                    )A 



	                    Group By A.InstrumentPK

	                    having sum(A.BuyVolume) - sum(A.SellVolume) > 0

                        if Exists

                        (

                        select * from #tmpBond	

                        )

                        BEGIN

                        -- Cek Close Price Bond and Bond Rating

                        set @msg = ''

                        Select distinct @msg = @msg + B.ID + ', ' from #tmpBond A

                        left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2

                        where A.instrumentPK not in

                        (

                        Select InstrumentPK

                        from ClosePrice 

                        where status = 2 and Date = @Date and ClosePriceValue >= 0 

                        )

                        if @@Rowcount > 0

                        BEGIN

                        Select 'Cek Close Price untuk Data : ' + @msg Msg

                        update MKBDTrails set LogMessages = 'Cek Close Price untuk Data : ' + @msg where MKBDTrailsPK = @NEWMKBDTrailsPK

                        return

                        END

                        ELSE

                        BEGIN

                        Select @MsgSuccess = @MsgSuccess + 'Company has Corporate Bond <br/> '

                        Insert into Rl510cBond(HistoryPK,Status,Notes,MKBDTrailsPK,Date,InstrumentPK,

                        InstrumentTypePK,HoldingPK,ObRating,Volume)

		

                        Select 1,2,'',@NewMKBDTrailsPK,@Date,A.InstrumentPK,

                        B.InstrumentTypePK,B.HoldingPK,B.BondRating,A.Volume

                        From #tmpBond A

                        Left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2

		

                        --RL510CBondUpdateData

		 

                        update rl set       

                        rl.price = dbo.[FGetLastClosePriceForFundPosition](@Date,rl.InstrumentPK),    

                        rl.obrating =  CASE WHEN dbo.[FGetLastOBRating](@Date, rl.InstrumentPK) = '' Then st.BondRating Else dbo.[FGetLastOBRating](@Date, rl.InstrumentPK) END,    

                        rl.InstrumentTypePK = isnull(St.instrumentTypePK, 0),     

                        rl.MarketValue = rl.volume *  (dbo.[FGetLastClosePriceForFundPosition](@Date,rl.InstrumentPK))/100  * isnull(F.Rate,1),    

                        rl.HoldingPK = isnull(St.HoldingPK,0),          

                         rl.HaircutPercent = isnull(case when St.CurrencyPK <> 1 then 90 else HcObl.OBHaircutMKBD end, 0),   

                        rl.ConcentrationRisk = isnull(HcObl.OBConcentrationRisk, 0),          

                        rl.MKBD01 = case when St.CurrencyPK <> 1 then 82 else  isnull(obMKBD01, 0) end,     

                        rl.MKBD02 = isnull(obMKBD02, 0),          

                        rl.MKBD03 = case when St.CurrencyPK <> 1 then 23 else  isnull(obMKBD03, 0) end,     

                        rl.MKBD09 = case when St.CurrencyPK <> 1 then 56 else  isnull(obMKBD09, 0) end       

                        from RL510CBond rl         

                        left join Instrument St on rl.InstrumentPK = St.InstrumentPK and st.status = 2

                        left join InstrumentType It on case when st.instrumentTypePK in (2,3,8,9,11,13,14,15) then 2 end = It.instrumentTypePK and It.status = 2

                        Left Join CurrencyRate F on St.CurrencyPK = F.CurrencyPK and F.Status = 2 and F.Date = @Date

                        left join 

                        (            

                        select InstrumentTypePK, BondRating, HaircutMKBD as OBHaircutMKBD, ConcentrationRisk as OBConcentrationRisk,            

                        MKBD01 as OBMKBD01, MKBD02 as OBMKBD02, MKBD03 as OBMKBD03, MKBD09 as OBMKBD09            

                        from MKBDSetup  (nolock)            

                        where [Date] = (                

                        select Max([Date]) as MaxDate from MKBDSetup (nolock)                

                        where [Date] <= @date and RLName = 'RL510CBond'                

                        )and RLName = 'RL510CBond' and status = 2

                        ) as HcObl     

                        ON case when It.instrumentTypePK in (2,3,8,9,11,13,14,15) then 2 end = HcObl.InstrumentTypePK and     

                        dbo.[FGetLastOBRating](@Date, rl.InstrumentPK) = HcObl.BondRating 

                        where rl.Date = @Date and MKBDTrailsPK = @NewMKBDTrailsPK

		 

		



                        update rl set   

                        rl.HaircutAmount = rl.MarketValue * (rl.HaircutPercent/100),     

                        rl.AfterHaircutAmount = rl.MarketValue * (1-(rl.HaircutPercent/100)),    

                        rl.TotalEquity = @TotalEquity  

                        from RL510cBond rl where rl.Date = @Date and MKBDTrailsPK = @NewMKBDTrailsPK    



                        update rl set   

                        rl.RankingLiabilities =    

                        case when MarketValue > TotalEquity * ConcentrationRisk    

                        then MarketValue - (TotalEquity * ConcentrationRisk)    

                        else 0 end     

                        from RL510cBond rl where rl.Date = @Date and MKBDTrailsPK = @NewMKBDTrailsPK     

		

                        -- BOND UPDATE MKBD 01 

                        set @ssql = ''

                        declare A cursor for    

                        select cast(MKBD01 as varchar) + 'B', sum(MarketValue) as Value    

                        from RL510cBond (nolock) where date = @date and MKBD01 > 0 

                        and status =  2 
                        
                         and MKBDTrailsPK = @NewMKBDTrailsPK

                        group by MKBD01    



                        open A    



                        fetch next from A    

                        into @field, @value    



                        while @@FETCH_STATUS = 0    

                        begin  


                        set @ssql = 'update MKBD01 set [' + @field + '] = ' + cast(@value as varchar) + ' where date = ''' +  convert(varchar(8),@date,112) + ''''+ ' and MKBDTrailsPK = ' +  cast(@NewMKBDTrailsPK as varchar)         

                        exec (@ssql)    



                        fetch next from A    

                        into @field, @value    

                        end    



                        close A    

                        deallocate A   

		

                        -- BOND UPDATE MKBD 03 

                        set @ssql = ''

                        declare A cursor for  

                        select cast(MKBD03 as varchar) + 'B', sum(RankingLiabilities) as Value  

                        from RL510cBond (nolock) where date = @date and MKBD03 > 0 

                        and MKBDTrailsPK = @NewMKBDTrailsPK 

                        group by MKBD03  

		

                        open A  



                        fetch next from A  

                        into @field, @value  



                        while @@FETCH_STATUS = 0  

                        begin   

                        set @ssql = 'update MKBD03 set [' + @field + '] = ' + cast(@value as varchar) + ' where date = ''' +  convert(varchar(8),@date,112) + ''''  + ' and MKBDTrailsPK = ' +  cast(@NewMKBDTrailsPK as varchar)        

                        exec (@ssql)  



                        fetch next from A  

                        into @field, @value  

                        end  



                        close A  

                        deallocate A   

		

                        --BOND UPDATE MKBD09

                        declare A cursor for    

                        select cast(MKBD09 as varchar) + 'E', sum(MarketValue) as Value    

                        from RL510cBond (nolock) where date = @date and MKBD09 > 0 

                        and MKBDTrailsPK = @NewMKBDTrailsPK

                        group by MKBD09  

                        union all  

                        select cast(MKBD09 as varchar) + 'G', sum(HaircutAmount) as Value    

                        from RL510cBond (nolock) where date = @date and MKBD09 > 0 

                        and MKBDTrailsPK = @NewMKBDTrailsPK

                        group by MKBD09    



                        open A    



                        fetch next from A    

                        into @field, @value    



                        while @@FETCH_STATUS = 0    

                        begin     

                        set @ssql = 'update MKBD09 set [' + @field + '] = ' + cast(@value as varchar) + ' where date = ''' +  convert(varchar(8),@date,112) + '''' +' and MKBDTrailsPK = ' +  cast(@NewMKBDTrailsPK as varchar)        

                        exec (@ssql)    



                        fetch next from A    

                        into @field, @value    

                        end    



                        close A    

                        deallocate A    

		

                        END

                        END



                        -- Portfolio Checking EQUITY

                        delete #tmpEquity where ValueDate <> @Date

                        Insert into #tmpEquity



                        Select A.InstrumentPK,isnull(sum(A.BuyVolume) - sum(A.SellVolume),0) LastVolume,@Date from (

                        select InstrumentPK,sum(Volume) BuyVolume,0 SellVolume from trxPortfolio

                        where ValueDate <= @Date and Posted = 1 and InstrumentTypePK = 1 and trxType = 1 and status = 2 and Revised = 0

                        Group By InstrumentPK

                        UNION ALL

                        select InstrumentPK,0 BuyVolume,sum(Volume) SellVolume from trxPortfolio

                        where ValueDate <= @Date and Posted = 1 and InstrumentTypePK = 1 and trxType = 2 and status = 2 and Revised = 0

                        Group By InstrumentPK

                        UNION ALL 
  
                        select InstrumentPK,sum(Balance) BuyVolume,0 SellVolume from CorporateActionResult 

                        where Date = @Date and FundPK = 9999

                        Group By InstrumentPK

                        )A

                        Group By A.InstrumentPK

                        having sum(A.BuyVolume) - sum(A.SellVolume) > 0

                        if Exists

                        (

                        select * from #tmpEquity	

                        )

                        BEGIN



                        -- Cek Close Price Equity

                        set @msg = ''

                        Select distinct @msg = @msg + B.ID + ', ' from #tmpEquity A

                        left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2

                        where A.instrumentPK not in

                        (

                        Select InstrumentPK

                        from ClosePrice 

                        where status = 2 and Date = @Date and ClosePriceValue >= 0 

                        )

                        if @@Rowcount > 0

                        BEGIN

                        Select 'Cek Close Price untuk Data : ' + @msg Msg

                        update MKBDTrails set LogMessages = 'Cek Close Price untuk Data : ' + @msg where MKBDTrailsPK = @NEWMKBDTrailsPK

                        return

                        END

                        ELSE

                        BEGIN

                        Select @MsgSuccess = @MsgSuccess + 'Company has Equity <br/> '

                        Insert into Rl510cEquity(HistoryPK,Status,Notes,MKBDTrailsPK,Date,InstrumentPK,

                        InstrumentTypePK,HoldingPK,Volume)

		

                        Select 1,2,'',@NewMKBDTrailsPK,@Date,A.InstrumentPK,

                        B.InstrumentTypePK,B.HoldingPK,A.LastVolume

                        From #tmpEquity A

                        Left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2


                        
                        --Update Rl510CEquity

                        update rl set   

                        rl.ClosePrice = isnull(CP.ClosePriceValue, 0), rl.MarketValue = isnull(CP.ClosePriceValue, 0) * rl.Volume  * isnull(F.Rate,1),   

                        rl.HaircutPercent = case when hc.haircut between 0 and 10 then 10
				                                    when hc.haircut between 15 and 20 then 20 
					                                    when hc.haircut between 55 and 80 then 55
					                                        when hc.haircut between 85 and 100 then 85 else isnull(hc.haircut,100) end, 

                        rl.ConcentrationRisk = isnull(RGConcentrationRisk, 0),    

                        rl.MKBD01 = isnull(RGMKBD01, 0), rl.MKBD02 = isnull(RGMKBD02, 0),    

                        rl.MKBD03 = isnull(RGMKBD03, 0), rl.MKBD09 = isnull(RGMKBD09, 0) 

                        from Rl510cEquity rl    

                        left join Instrument St on rl.InstrumentPK = St.InstrumentPK and st.status = 2 

                        left join InstrumentType It on st.InstrumentTypePK = It.InstrumentTypePK

                        Left Join CurrencyRate F on St.CurrencyPK = F.CurrencyPK and F.Status = 2 and F.Date = @Date

                        left join HaircutMKBD HC on RL.InstrumentPK = HC.InstrumentPK and HC.Date  = (select Max(Date) from HaircutMKBD where Date <= @Date and Status = 2) and HC.status = 2

                        left join 

                        (    

                        select InstrumentPK, ClosePriceValue from [ClosePrice] (nolock)    

                        where Date = @Date  and status = 2

                        ) as CP ON St.InstrumentPK = CP.InstrumentPK   

                        left join 

                        (  

                        select InstrumentTypePK, HaircutFrom, HaircutTo, HaircutMKBD as RGHaircutMKBD, ConcentrationRisk as RGConcentrationRisk,    

                        MKBD01 as RGMKBD01, MKBD02 as RGMKBD02, MKBD03 as RGMKBD03, MKBD09 as RGMKBD09    

                        from MKBDSetup (nolock) A

                        where [Date] = 

                        (    

                        select Max([Date]) as MaxDate from MKBDSetup (nolock)    

                        where [Date] <= @Date and rlName = 'RL510cEquity'    

                        )and rlName = 'RL510cEquity' 

                        ) as HcStock ON It.Type = HcStock.InstrumentTypePK and hc.haircut/100 between HcStock.HaircutFrom and HcStock.HaircutTo   

                        where rl.Date = @Date and MKBDTrailsPK = @NewMKBDTrailsPK

		

                        --UPDATE Rl510cEquity RL

		

		

                        update rl set   

                        rl.HaircutAmount = rl.MarketValue * (rl.HaircutPercent/100),   

                        rl.AfterHaircutAmount = rl.MarketValue * (1-(rl.HaircutPercent/100)),  

                        rl.TotalEquity = @TotalEquity

                        from Rl510cEquity rl  

                        where rl.Date = @Date  and MKBDTrailsPK = @NewMKBDTrailsPK



                        update rl set   

                        rl.RankingLiabilities =  

                        case when MarketValue > TotalEquity * ConcentrationRisk  

                        then MarketValue - (TotalEquity * ConcentrationRisk)  

                        else 0 end   

                        from Rl510cEquity rl  

                        where rl.Date = @Date   and MKBDTrailsPK = @NewMKBDTrailsPK

	
                        

                        --RL510cEquity Update 01

                        set @ssql = ''

                        declare A cursor for   



                        select cast(MKBD01 as varchar) + 'B', sum(MarketValue) as Value    

                        from RL510cEquity (nolock) where date = @date and MKBD01 > 0 

                        and status =  2 and MKBDTrailsPK = @NewMKBDTrailsPK

                        group by MKBD01    



                        open A    



                        fetch next from A    

                        into @field, @value    



                        while @@FETCH_STATUS = 0    

                        begin     

                        set @ssql = 'update MKBD01 set [' + @field + '] = ' + cast(@value as varchar) + ' where date = ''' +  convert(varchar(8),@date,112) + ''''+' and MKBDTrailsPK = ' +  cast(@NewMKBDTrailsPK as varchar)            

                        exec (@ssql)    



                        fetch next from A    

                        into @field, @value    

                        end    



                        close A    

                        deallocate A    

		

                        --RL510cEquity Update MKBD 03

                        set @ssql = ''

                        declare A cursor for  

                        select cast(MKBD03 as varchar) + 'B', sum(RankingLiabilities) as Value  

                        from RL510cEquity (nolock) where date = @date and MKBD03 > 0 

                        and MKBDTrailsPK = @NewMKBDTrailsPK 

                        group by MKBD03  



                        open A  



                        fetch next from A  

                        into @field, @value  



                        while @@FETCH_STATUS = 0  

                        begin   

                        set @ssql = 'update MKBD03 set [' + @field + '] = ' + cast(@value as varchar) + ' where date = ''' +  convert(varchar(8),@date,112) + ''''    + ' and MKBDTrailsPK = ' +  cast(@NewMKBDTrailsPK as varchar)        

                        exec (@ssql)  



                        fetch next from A  

                        into @field, @value  

                        end  



                        close A  

                        deallocate A 

		

                        --RL510cEquity Update 09

                        set @ssql = ''

                        declare A cursor for    

                        select cast(MKBD09 as varchar) + 'E', sum(MarketValue) as Value    

                        from RL510cEquity (nolock) where date = @date and MKBD09 > 0 

                        and MKBDTrailsPK = @NewMKBDTrailsPK

                        group by MKBD09    

                        union all  

                        select cast(MKBD09 as varchar) + 'G', sum(HaircutAmount) as Value    

                        from RL510cEquity (nolock) where date = @date and MKBD09 > 0 

                        and MKBDTrailsPK = @NewMKBDTrailsPK

                        group by MKBD09    

                        open A    



                        fetch next from A    

                        into @field, @value    



                        while @@FETCH_STATUS = 0    

                        begin     

                        set @ssql = 'update MKBD09 set [' + @field + '] = ' + cast(@value as varchar) + ' where date = ''' +  convert(varchar(8),@date,112) + ''''    + ' and MKBDTrailsPK = ' +  cast(@NewMKBDTrailsPK as varchar)     

                        exec (@ssql)    



                        fetch next from A    

                        into @field, @value    

                        end    



                        close A    

                        deallocate A   



		

                        END

                        END



                        -- Portfolio Checking SBN / GOVERNMENT BOND

                        

                        Insert into #tmpSBN

                        Select A.InstrumentPK,isnull(sum(A.BuyVolume) - sum(A.SellVolume),0) Volume,A.MaturityDate,@Date from (

                        select A.InstrumentPK,sum(Volume) BuyVolume,0 SellVolume,A.MaturityDate from trxPortfolio A

                        left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2

                        left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.Status = 2

                        where ValueDate <= @Date and Posted = 1 and c.Type = 5 and trxType = 1 and Revised = 0 and A.status = 2

                        Group By A.InstrumentPK,A.MaturityDate

                        UNION ALL

                        select A.InstrumentPK,0 BuyVolume,sum(Volume) SellVolume,A.MaturityDate from trxPortfolio A

                        left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2

                        left join InstrumentType C on B.InstrumentTypePK = C.InstrumentTypePK and C.Status = 2

                        where ValueDate <= @Date and Posted = 1 and c.Type = 5 and trxType = 2 and Revised = 0 and A.status = 2

                        Group By A.InstrumentPK,A.MaturityDate

                        )A 

                        Group By A.InstrumentPK,A.MaturityDate

                        having sum(A.BuyVolume) - sum(A.SellVolume) > 0

                        if Exists

                        (

                        select * from #tmpSBN	

                        )

                        BEGIN

                        -- Cek Close Price Bond and Bond Rating

                        set @msg = ''

                        Select distinct @msg = @msg + B.ID + ', ' from #tmpSBN A

                        left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2

                        where A.instrumentPK not in

                        (

                        Select InstrumentPK

                        from ClosePrice 

                        where status = 2 and Date = @Date and ClosePriceValue >= 0 

                        )

                        if @@Rowcount > 0

                        BEGIN

                        Select 'Cek Close Price untuk Data : ' + @msg Msg

                        update MKBDTrails set LogMessages = 'Cek Close Price untuk Data : ' + @msg where MKBDTrailsPK = @NEWMKBDTrailsPK

                        return

                        END

                        ELSE

                        BEGIN

                        Select @MsgSuccess = @MsgSuccess + 'Company has Government Bond <br/> '

                        Insert into rl510cSbn(HistoryPK,Status,Notes,MKBDTrailsPK,Date,InstrumentPK,

                        InstrumentTypePK,HoldingPK,Volume,DueDate)

		

                        Select 1,2,'',@NewMKBDTrailsPK,@Date,A.InstrumentPK,

                        B.InstrumentTypePK,B.HoldingPK,A.Volume,A.MaturityDate

                        From #tmpSBN A

                        Left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.status = 2

		

                        -- RL510cSBN Update Data

	

                        update rl       

                        set rl.Price = isnull(CP.ClosePriceValue, 0), rl.MarketValue = isnull(CP.ClosePriceValue, 0) * rl.Volume,   

                        rl.HaircutPercent = isnull(SBNHaircutMKBD, 0), rl.ConcentrationRisk = isnull(SBNConcentrationRisk,0),      

                        rl.MKBD01 = isnull(SBNMKBD01, 0), rl.MKBD02 = isnull(SBNMKBD02, 0),      

                        rl.MKBD03 = isnull(SBNMKBD03, 0), rl.MKBD09 = isnull(SBNMKBD09, 0)      

                        from RL510cSBN rl      

                        left join Instrument St on rl.InstrumentPK = St.InstrumentPK

                        left join 

                        (      

                        select [ClosePrice].InstrumentPK, [ClosePrice].ClosePriceValue

                        from [ClosePrice] (nolock)      

                        where [ClosePrice].Date = @Date      

                        ) as CP ON St.InstrumentPK = CP.InstrumentPK      

                        left join 

                        (      

                        select InstrumentTypePK, SBNTenorFrom, SBNTenorTo, HaircutMKBD as SBNHaircutMKBD, ConcentrationRisk as SBNConcentrationRisk,      

                        MKBD01 as SBNMKBD01, MKBD02 as SBNMKBD02, MKBD03 as SBNMKBD03, MKBD09 as SBNMKBD09      

                        from MKBDSetup (nolock)      

                        where [Date] =       

                        (      

                        select Max([Date]) as MaxDate       

                        from MKBDSetup (nolock)      

                        where [Date] <= @Date and RLName = 'RL510cSBN'       

                        ) and RLName = 'RL510cSBN'      

                        ) as HcSBN ON --St.StockType = HcSBN.StockType and     

                        cast(datediff(dd, @Date, isnull(Rl.DueDate,@Date)) as float)/365 between HcSBN.SBNTenorFrom and HcSBN.SBNTenorTo       

                        where rl.Date = @Date  and MKBDTrailsPK = @NewMKBDTrailsPK

			

                        -- RL510cSBN Update RL

		

                        update rl   

                        set rl.HaircutAmount = rl.MarketValue * (rl.HaircutPercent/100),   

                        rl.AfterHaircutAmount = rl.MarketValue * (1-(rl.HaircutPercent/100)),  

                        rl.TotalEquity = @TotalEquity

                        from RL510cSBN rl  

                        where rl.Date = @Date  and MKBDTrailsPK = @NewMKBDTrailsPK



                        update rl  

                        set rl.RankingLiabilities =  

                        case when MarketValue > TotalEquity * ConcentrationRisk  

                        then MarketValue - (TotalEquity * ConcentrationRisk)  

                        else 0  

                        end   

                        from RL510cSBN rl  

                        where rl.Date = @Date  and MKBDTrailsPK = @NewMKBDTrailsPK       

		

                        -- RL510cSBN update 01

                        set @ssql = ''

                        declare A cursor for    

                        select cast(MKBD01 as varchar) + 'B', sum(MarketValue) as Value    

                        from RL510cSBN (nolock) where date = @date and MKBD01 > 0 

                        and status =  2 and MKBDTrailsPK = @NewMKBDTrailsPK

                        group by MKBD01    



                        open A    



                        fetch next from A    

                        into @field, @value    



                        while @@FETCH_STATUS = 0    

                        begin     

                        set @ssql = 'update MKBD01 set [' + @field + '] = ' + cast(@value as varchar) + ' where date = ''' +  convert(varchar(8),@date,112) + ''''+ ' and MKBDTrailsPK = ' +  cast(@NewMKBDTrailsPK as varchar)         

                        exec (@ssql)    



                        fetch next from A    

                        into @field, @value    

                        end    



                        close A    

                        deallocate A   

		

                        -- RL510cSBN update 03

                        set @ssql = ''

                        declare A cursor for  

                        select cast(MKBD03 as varchar) + 'B', sum(RankingLiabilities) as Value  

                        from RL510cSBN (nolock) where date = @date and MKBD03 > 0 

                        and MKBDTrailsPK = @NewMKBDTrailsPK 

                        group by MKBD03  



                        open A  



                        fetch next from A  

                        into @field, @value  



                        while @@FETCH_STATUS = 0  

                        begin   

                        set @ssql = 'update MKBD03 set [' + @field + '] = ' + cast(@value as varchar) + ' where date = ''' +  convert(varchar(8),@date,112) + '''' + ' and MKBDTrailsPK = ' +  cast(@NewMKBDTrailsPK as varchar)         

                        exec (@ssql)  



                        fetch next from A  

                        into @field, @value  

                        end  



                        close A  

                        deallocate A  

		 

                        -- RL510cSBN update 09

                        set @ssql = ''

                        declare A cursor for    

                        select cast(MKBD09 as varchar) + 'E', sum(MarketValue) as Value    

                        from RL510cSBN (nolock) where date = @date and MKBD09 > 0 

                        and MKBDTrailsPK = @NewMKBDTrailsPK

                        group by MKBD09   

                        union all  

                        select cast(MKBD09 as varchar) + 'G', sum(haircutamount) as Value    

                        from RL510cSBN (nolock) where date = @date and MKBD09 > 0 

                        and MKBDTrailsPK = @NewMKBDTrailsPK

                        group by MKBD09     



                        open A    



                        fetch next from A    

                        into @field, @value    



                        while @@FETCH_STATUS = 0    

                        begin     

                        set @ssql = 'update MKBD09 set [' + @field + '] = ' + cast(@value as varchar) + ' where date = ''' +  convert(varchar(8),@date,112) + '''' + ' and MKBDTrailsPK = ' +  cast(@NewMKBDTrailsPK as varchar)       

                        exec (@ssql)    



                        fetch next from A    

                        into @field, @value    

                        end    



                        close A    

                        deallocate A    

                        END

                        END

                        declare @MKBD06Bank int
                        declare @MKBD06PettyCash int
                        declare @InvestmentEquity int



                        select @MKBD06Bank = MKBD06Bank,@MKBD06PettyCash = MKBD06PettyCash,@InvestmentEquity = InvInEquity from AccountingSetup 
                        where status in (2)



                        Insert into MKBD06(MKBDTrailsPK,Date,CompanyID,CompanyName,DirectorName,LastUpdate,

                        [8B],[17B],[19B])

                        Select @NewMKBDTrailsPK,@Date,@CompanyID,@CompanyName,@DirectorName,@timeNow

                        ,sum(dbo.[FGetAccountBalanceByDateByParent](@Date,@MKBD06PettyCash) + dbo.[FGetAccountBalanceByDateByParent](@Date,@MKBD06Bank)),dbo.[FGetAccountBalanceByDateByParent](@Date,@MKBD06PettyCash),

                        dbo.[FGetAccountBalanceByDateByParent](@Date,@MKBD06Bank)

                        ----------------------------------------------------------------------

                        Update MKBD06 set [15B] = [8B],[17C] = [17B],[19C] = [19B],[23B] = [8B] where MKBDTrailsPK = @NewMKBDTrailsPK


                        Insert into MKBD06Detail(MKBDTrailsPK,Date,CompanyID,CompanyName,DirectorName,LastUpdate,

                        [No],A,B,C,D,E,F)


                        Select @NewMKBDTrailsPK,@Date,@CompanyID,@CompanyName,@DirectorName,@timeNow,ROW_NUMBER() OVER(ORDER BY A.ID ASC),isnull(E.RTGSCode,''),
                        'S' SendiriNasabah,A.BankAccountNo NoRek,F.ID Currency,case when F.CurrencyPK = 2 then isnull(sum(BaseDebit - BaseCredit)/G.Rate,0) else sum(BaseDebit - BaseCredit) end Saldo, sum(BaseDebit - BaseCredit) SaldoRp from CashRef A 
                        left join JournalDetail B on A.AccountPK = B.AccountPK and B.status = 2
                        left join Journal C on B.JournalPK = C.JournalPK and C.status = 2
                        left join BankBranch D on A.BankBranchPK = D.BankBranchPK and D.status = 2
                        left join Bank E on D.BankPK = E.BankPK and E.status = 2
                        left join Currency F on A.CurrencyPK = F.CurrencyPK and F.status = 2
                        left join CurrencyRate G on A.CurrencyPK = G.CurrencyPK and G.Status = 2 and G.Date = @Date
                        where A.Status in (2) and C.Posted = 1 and C.Reversed = 0 and ValueDate <= @Date and PeriodPK = @PeriodPK
                        and A.BankbranchPK <> 0 and A.CashRefType = 2
                        group by F.ID,A.ID,A.BankAccountNo,E.RTGSCode,F.CurrencyPK,G.Rate



                        declare @TotalMKBD07 numeric (22,2)
                        IF (@ClientCode = 21)
                        BEGIN
                            
                            select @TotalMKBD07 = isnull(sum(A.MarketValue),0)
                            from (
                            select A.InstrumentTypePK,B.ID InstrumentID,A.Volume,dbo.[FGetLastAvgFromInvestment_Acc](@Date,A.InstrumentPK) AvgPrice,A.ClosePrice,A.MarketValue,A.RankingLiabilities from RL510CEquity A
                            left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2 where Date = @Date and MKBDTrailsPK = @NewMKBDTrailsPK
                            union all
                            select A.InstrumentTypePK,B.ID InstrumentID,A.Volume,dbo.[FGetLastAvgFromInvestment_Acc](@Date,A.InstrumentPK) AvgPrice,A.ClosePrice,A.MarketValue,A.RankingLiabilities from RL510CBond A
                            left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2 where Date = @Date and MKBDTrailsPK = @NewMKBDTrailsPK

                            ) A
                        END
                        ELSE
                        BEGIN

                            select @TotalMKBD07 = isnull(sum(A.MarketValue),0)
                            from (
                            select A.InstrumentTypePK,B.ID InstrumentID,A.Volume,dbo.[FGetLastAvgFromInvestment_Acc](@Date,A.InstrumentPK) AvgPrice,A.ClosePrice,A.MarketValue,A.RankingLiabilities from RL510CEquity A
                            left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2 where Date = @Date and MKBDTrailsPK = @NewMKBDTrailsPK
                            union all
                            select A.InstrumentTypePK,B.ID InstrumentID,A.Volume,dbo.[FGetLastAvgFromInvestment_Acc](@Date,A.InstrumentPK) AvgPrice,A.ClosePrice,A.MarketValue,A.RankingLiabilities from RL510CBond A
                            left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2 where Date = @Date and MKBDTrailsPK = @NewMKBDTrailsPK
                            union all
                            select B.InstrumentTypePK,B.ID InstrumentID,A.Volume,dbo.[FGetLastAvgFromInvestment_Acc](@Date,A.InstrumentPK) AvgPrice,A.CloseNAV,A.MarketValue,A.RankingLiabilities from RL504 A
                            left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2 where Date = @Date and MKBDTrailsPK = @NewMKBDTrailsPK                        

                            ) A
                        END



                        -- INSERT MKBD 07 FROM JOURNAL

                        Insert into MKBD07(MKBDTrailsPK,Date,CompanyID,CompanyName,DirectorName,LastUpdate,

                        [9B])

                        Select @NewMKBDTrailsPK,@Date,@CompanyID,@CompanyName,@DirectorName,@timeNow,

                        @TotalMKBD07


                        Update MKBD07 set [26B] = [9B],[31B] = [9B],[31C] = [9B],[36B] = [9B],[36C]=[9B],[63B] = [9B],[63C] = [9B] where MKBDTrailsPK = @NewMKBDTrailsPK







                        -- ACCOUNT UPDATE MKBD 01

                        set @ssql = ''

                        declare A cursor for    

                        select cast(dbo.[FVDNoByAccount](sj.AccountPK) as varchar) + 'B',     

                        ABS(sum

                        (    

                        case when A.Type = 1 then BaseDebit - BaseCredit    

                        else BaseCredit - BaseDebit end  

                        )    
                        )

                        from Journal J (nolock)    

                        left join JournalDetail Sj (nolock) on J.JournalPK = Sj.JournalPK      

                        left join Account A (nolock) on A.AccountPK = Sj.AccountPK and A.status = 2   

                        where J.Status  = 2 and j.Posted = 1 and J.ValueDate <= @Date and J.PeriodPK = @PeriodPK and A.Type = 1 and J.Reversed = 0

                        group by dbo.[FVDNoByAccount](sj.AccountPK)     

                        --group by A.[Type]   



                        open A    



                        fetch next from A    

                        into @field, @value    



                        while @@FETCH_STATUS = 0    

                        begin     



                        set @ssql = 'update MKBD01 set [' + @field + '] = [' + @field + '] + ' + cast(@value as varchar) +' Where MKBDTrailsPK = ' +  cast(@NewMKBDTrailsPK as varchar)   

                        exec (@ssql)    



                        fetch next from A    

                        into @field, @value    

                        end    



                        close A    

                        deallocate A    



                        -- MKBD01 UPDATE TOTAL ASET LANCAR



                        set @ssql = ''

                        declare A cursor for    

                        select col.name from sysobjects obj    

                        left join syscolumns col on obj.id = col.id    

                        where obj.name = 'MKBD01'    

                        and col.name not in ('MKBD01PK','MKBDTrailsPK', 'Date', 'CompanyID', 'CompanyName', 'DirectorName',     

                        'LastUpdate','DBUserID','DBTerminalID','LastUpdateDB')     

                        and col.name like '%B' and cast(left(col.name, len(col.name)-1) as int) between 10 and 99    

     

                        open A    

    

                        fetch next from A    

                        into @field     

    

                        while @@FETCH_STATUS = 0    

                        begin     

                        select @ssql = 'update MKBD01 set [100B] = [100B] + ['+ @field + '] where date = ''' +  convert(varchar(8),@date,112) + ''''+ ' and MKBDTrailsPK = ' +  cast(@NewMKBDTrailsPK as varchar)           

                        exec (@ssql)    

      

                        fetch next from A    

                        into @field     

                        end    

    

                        close A    

                        deallocate A    



                        -- MKBD01 UPDATE TOTAL ASSET TETAP

                        set @ssql = ''

                        declare A cursor for    

                        select col.name from sysobjects obj    

                        left join syscolumns col on obj.id = col.id    

                        where obj.name = 'MKBD01'    

                        and col.name not in ('MKBD01PK','MKBDTrailsPK', 'Date', 'CompanyID', 'CompanyName', 'DirectorName',     

                        'LastUpdate','DBUserID','DBTerminalID','LastUpdateDB')     

                        and col.name like '%B' and cast(left(col.name, len(col.name)-1) as int) between 102 and 111    
                        --EMCO
                        --and col.name like '%B' and cast(left(col.name, len(col.name)-1) as int) between 108 and 111 
     

                        open A    

    

                        fetch next from A    

                        into @field     

    

                        while @@FETCH_STATUS = 0    

                        begin     

                        select @ssql = 'update MKBD01 set [112B] = [112B] + [' + @field + '] where date = ''' +  convert(varchar(8),@date,112) + ''''+ ' and MKBDTrailsPK = ' +  cast(@NewMKBDTrailsPK as varchar)               

                        exec (@ssql)    

      

                        fetch next from A    

                        into @field     

                        end    

    

                        close A    

                        deallocate A    



                        -- MKBD 01 UPDATE TOTAL



                        set @ssql =''

                        declare A cursor for      

                        select col.name from sysobjects obj      

                        left join syscolumns col on obj.id = col.id      

                        where obj.name = 'MKBD01'      

                        and col.name not in ('MKBD01PK','MKBDTrailsPK', 'Date', 'CompanyID', 'CompanyName', 'DirectorName',     

                        'LastUpdate','DBUserID','DBTerminalID','LastUpdateDB')     

                        and col.name like '%B' and cast(left(col.name, len(col.name)-1) as int) in (100,112)
                        --EMCO       
                        --and col.name like '%B' and cast(left(col.name, len(col.name)-1) as int) between 100 and 111 
       

                        open A      

      

                        fetch next from A      

                        into @field       

      

                        while @@FETCH_STATUS = 0      

                        begin       

                        select @ssql = 'update MKBD01 set [113B] = [113B] + [' + @field + '] where date = ''' +  convert(varchar(8),@date,112) + '''' + ' and MKBDTrailsPK = ' +  cast(@NewMKBDTrailsPK as varchar)     

                        exec (@ssql)      

        

                        fetch next from A      

                        into @field       

                        end      

      

                        close A      

                        deallocate A      

      





                        -- ACCOUNT UPDATE MKBD 02

                        set @ssql = ''

                        declare A cursor for    

                        select cast(dbo.[FVDNoByAccount](sj.AccountPK) as varchar) + 'B',     

                        sum(    

                        case when A.Type = 1 then BaseDebit - BaseCredit    

                        else BaseCredit - BaseDebit    

                        end     

                        )    

                        from Journal J (nolock)    

                        left join JournalDetail Sj (nolock)     

                        on J.JournalPK = Sj.JournalPK      

                        left join Account A (nolock)    

                        on A.AccountPK = Sj.AccountPK  and A.status = 2    

                        where J.Status  = 2 and j.Posted = 1 and J.ValueDate <= @Date and J.PeriodPK = @PeriodPK and A.Type > 1  and J.Reversed = 0  

                        group by dbo.[FVDNoByAccount](sj.AccountPK)     

                        --group by A.[Type]      

       

                        open A    

    

                        fetch next from A    

                        into @field, @value    

    

                        while @@FETCH_STATUS = 0    

                        begin     

                        set @ssql = 'update MKBD02 set [' + @field + '] = [' + @field + '] + ' + cast(@value as varchar) + ' where  MKBDTrailsPK = ' +  cast(@NewMKBDTrailsPK as varchar)

                        exec (@ssql)    

     

                        fetch next from A    

                        into @field, @value    

                        end    

    

                        close A    

                        deallocate A    



                        -- MKBD02UpdateTotalLiabilities

                        set @ssql = ''



                   	select @SumLiabilities = ([122B] + [123B] + [125B] + [126B] + [127B] + [129B] + [130B] + [133B] + [134B] + [136B] + [137B] + [139B] + [140B] + [141B] + [142B] + [143B] + [146B] + [147B] + [148B] + [149B] + [151B] + [152B] + [154B] + [155B] + [156B] + [157B]  + [158B] + [159B] + [160B] + [161B] + [162B] + [163B]) from MKBD02 where date = convert(varchar(8),@date,112) and MKBDTrailsPK = cast(@NewMKBDTrailsPK as varchar)
                        update MKBD02 set [164B] = @SumLiabilities where date = convert(varchar(8),@date,112) and MKBDTrailsPK = cast(@NewMKBDTrailsPK as varchar)




  

                        -- MKBD02UpdateTotalEquities



                     	select @SumEquity = ([167B] + [168B] + [169B] + [170B] + [171B]) from MKBD02 where date = convert(varchar(8),@date,112) and MKBDTrailsPK = cast(@NewMKBDTrailsPK as varchar)   
                        update MKBD02 set [172B] = @SumEquity where date = convert(varchar(8),@date,112) and MKBDTrailsPK =  cast(@NewMKBDTrailsPK as varchar)   




                        -- MKBD02UpdateTotal

                      select @SumLiabilities = ([164B] + [172B]) from MKBD02 where date = convert(varchar(8),@date,112) and MKBDTrailsPK = cast(@NewMKBDTrailsPK as varchar)
                        
                        update MKBD02 set [173B] = @SumLiabilities where date = convert(varchar(8),@date,112) and MKBDTrailsPK = cast(@NewMKBDTrailsPK as varchar)    




                        Declare @TotalEquityToday numeric(22,2)       

                        -- INSERT VD510C

                        
                        select @TotalEquityToday = [172B] from MKBD02 where MKBDTrailsPK = @NewMKBDTrailsPK


                        IF (@ClientCode = '03') -- MINTA CLOSEPRICE PAKAI NILAI USD BUKAN YG SUDAH DI KALI RATE
                        BEGIN
                            INSERT INTO [dbo].[MKBD510C]
                            ([MKBDTrailsPK],[Date],[CompanyID],[CompanyName],[DirectorName],[A],[B],[C]
                            ,[D],[E],[F],[G],[H],[I],[J],[K])	


                            Select @NewMKBDTrailsPK,@Date,@CompanyID,@CompanyName,@DirectorName, 
                            ROW_NUMBER() OVER(ORDER BY InstrumentTypePK,InstrumentID ASC) AS No,
                            case when InstrumentTypePK = 1 then 'saham' else 'obligasi' end B,A.InstrumentID C,case when Affiliated = 0 then 'Tidak Terafiliasi' else 'Terafiliasi' end  D, A.Volume E,AvgPrice F,A.ClosePrice G,A.MarketValue H,'' I,A.Presentase J,A.RankingLiabilities K 
                            from (
                            select B.Affiliated,A.InstrumentTypePK,B.ID InstrumentID,A.Volume,dbo.[FGetLastAvgFromInvestment_Acc](@Date,A.InstrumentPK) AvgPrice,case when F.CurrencyPK = 2 then isnull(A.ClosePrice,0) else A.ClosePrice end ClosePrice,A.MarketValue,A.RankingLiabilities,sum(A.MarketValue/@TotalEquityToday)*100 Presentase from RL510CEquity A
                            left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2 
                            left join Currency F on B.CurrencyPK = F.CurrencyPK and F.status = 2
                            left join CurrencyRate G on F.CurrencyPK = G.CurrencyPK and G.Status = 2 and G.Date = @Date
                            where A.Date = @Date and MKBDTrailsPK = @NewMKBDTrailsPK
                            group by B.Affiliated,A.InstrumentTypePK,B.ID,A.Volume,A.instrumentPK,A.ClosePrice,A.MarketValue,A.RankingLiabilities,F.CurrencyPK,G.Rate
                            union all
                            select B.Affiliated,A.InstrumentTypePK,B.ID InstrumentID,A.Volume,dbo.[FGetLastAvgFromInvestment_Acc](@Date,A.InstrumentPK) AvgPrice,case when F.CurrencyPK = 2 then isnull(A.Price,0) else A.Price end ClosePrice,A.MarketValue,A.RankingLiabilities,sum(A.MarketValue/@TotalEquityToday)*100 Presentase from RL510CBond A
                            left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2 
                            left join Currency F on B.CurrencyPK = F.CurrencyPK and F.status = 2
                            left join CurrencyRate G on F.CurrencyPK = G.CurrencyPK and G.Status = 2 and G.Date = @Date
                            where A.Date = @Date and MKBDTrailsPK = @NewMKBDTrailsPK
                            group by B.Affiliated,A.InstrumentTypePK,B.ID,A.Volume,A.instrumentPK,A.Price,A.MarketValue,A.RankingLiabilities,F.CurrencyPK,G.Rate
                            ) A

                        END
                        ELSE
                        BEGIN

                            INSERT INTO [dbo].[MKBD510C]
                            ([MKBDTrailsPK],[Date],[CompanyID],[CompanyName],[DirectorName],[A],[B],[C]
                            ,[D],[E],[F],[G],[H],[I],[J],[K])	


                            Select @NewMKBDTrailsPK,@Date,@CompanyID,@CompanyName,@DirectorName, 
                            ROW_NUMBER() OVER(ORDER BY InstrumentTypePK,InstrumentID ASC) AS No,
                            case when InstrumentTypePK = 1 then 'saham' else 'obligasi' end B,A.InstrumentID C,case when Affiliated = 0 then 'Tidak Terafiliasi' else 'Terafiliasi' end D, A.Volume E,AvgPrice F,A.ClosePrice G,A.MarketValue H,'' I,A.Presentase J,A.RankingLiabilities K 
                            from (
                            select B.Affiliated,A.InstrumentTypePK,B.ID InstrumentID,A.Volume,dbo.[FGetLastAvgFromInvestment_Acc](@Date,A.InstrumentPK) AvgPrice,case when F.CurrencyPK = 2 then isnull(A.ClosePrice/G.Rate,0) else A.ClosePrice end ClosePrice,A.MarketValue,A.RankingLiabilities,sum(A.MarketValue/@TotalEquityToday)*100 Presentase from RL510CEquity A
                            left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2 
                            left join Currency F on B.CurrencyPK = F.CurrencyPK and F.status = 2
                            left join CurrencyRate G on F.CurrencyPK = G.CurrencyPK and G.Status = 2 and G.Date = @Date
                            where A.Date = @Date and MKBDTrailsPK = @NewMKBDTrailsPK
                            group by B.Affiliated,A.InstrumentTypePK,B.ID,A.Volume,A.instrumentPK,A.ClosePrice,A.MarketValue,A.RankingLiabilities,F.CurrencyPK,G.Rate
                            union all
                            select B.Affiliated,A.InstrumentTypePK,B.ID InstrumentID,A.Volume,dbo.[FGetLastAvgFromInvestment_Acc](@Date,A.InstrumentPK) AvgPrice,case when F.CurrencyPK = 2 then isnull(A.Price/G.Rate,0) else A.Price end ClosePrice,A.MarketValue,A.RankingLiabilities,sum(A.MarketValue/@TotalEquityToday)*100 Presentase from RL510CBond A
                            left join Instrument B on A.InstrumentPK = B.InstrumentPK and B.Status = 2 
                            left join Currency F on B.CurrencyPK = F.CurrencyPK and F.status = 2
                            left join CurrencyRate G on F.CurrencyPK = G.CurrencyPK and G.Status = 2 and G.Date = @Date
                            where A.Date = @Date and MKBDTrailsPK = @NewMKBDTrailsPK
                            group by B.Affiliated,A.InstrumentTypePK,B.ID,A.Volume,A.instrumentPK,A.Price,A.MarketValue,A.RankingLiabilities,F.CurrencyPK,G.Rate
                            ) A

                        END

                        





                        declare @TotalMKBD01 numeric(18,4)      

                        declare @TotalMKBD02 numeric(18,4)      

                        declare @SelisihPembulatan numeric(18,4)      

      

                        select @SelisihPembulatan = 100   

                        select @TotalMKBD01 = isnull([113B],0) from MKBD01 (nolock) where Date = @Date  and MKBDTrailsPK = @NewMKBDTrailsPK    

                        select @TotalMKBD02 = isnull([173B],0) from MKBD02 (nolock) where Date = @Date  and MKBDTrailsPK = @NewMKBDTrailsPK    

      

                        if abs(@TotalMKBD01 - @TotalMKBD02) <= @SelisihPembulatan       

                        begin      

                        update MKBD02 set [170B] = [170B] + (@TotalMKBD01 - @TotalMKBD02),       

                        [172B] = [172B] + (@TotalMKBD01 - @TotalMKBD02), [173B] = [173B] + (@TotalMKBD01 - @TotalMKBD02)      

                        where Date = @Date and MKBDTrailsPK = @NewMKBDTrailsPK     

                        end 





                        -- MKBD 03 UPDATE TOTAL RL

                        set @ssql = ''

                        declare A cursor for    

                        select col.name from sysobjects obj    

                        left join syscolumns col on obj.id = col.id    

                        where obj.name = 'MKBD03'    

                        and col.name not in ('MKBD03PK','MKBDTrailsPK', 'Date', 'CompanyID', 'CompanyName', 'DirectorName',     

                        'LastUpdate','DBUserID','DBTerminalID','31B','LastUpdateDB') 

                        and col.name like '%B'   

     

                        open A    

                        fetch next from A  

                        into @field   

  

                        while @@FETCH_STATUS = 0  

                        begin   

                        set @ssql = 'update MKBD03 set [31B] = [31B] + [' + @field + '] where date = ''' +  convert(varchar(8),@date,112) + ''''+ ' and MKBDTrailsPK = ' +  cast(@NewMKBDTrailsPK as varchar)   

                        exec (@ssql)  

   

                        fetch next from A  

                        into @field   

                        end  

  

                        close A  

                        deallocate A   



                        -- MKBD08 UPDATE DATA

                        declare @PPEMinimalMKBD decimal(19,4)      

                        declare @MIMinimalMKBD decimal(19,4)       

                        declare @MITotalFund decimal(19,4)  



                        select @PPEMinimalMKBD = isnull(PPEMinimalMKBD,0), @MIMinimalMKBD = isnull(MIMinimalMKBD,0) from Company (nolock) 

                        where status = 2



                        select @MITotalFund = isnull(Amount,0) from AUM (nolock)  

                        where status = 2 and Date = 

                        (select MAX(Date) MaxDate from AUM (nolock) where Date <= @Date and status = 2)

  

                        update M1 set   

                        M1.[8E] = M2.[164B], M1.[11E] = M2.[163B], M1.[13E] = M2.[146B] ,M1.[14E] = M2.[147B],  

                        M1.[15E] = M2.[148B] ,M1.[18E] = @PPEMinimalMKBD ,M1.[22E] = @MIMinimalMKBD, M1.[23E] = isnull(@MITotalFund,0),  

                        M1.[9E] = M3.[31B]  

                        from MKBD08 M1        

                        left join MKBD02 M2 on M1.Date = M2.Date and M1.MKBDTrailsPK = M2.MKBDTrailsPK    

                        Left join MKBD03 M3 on M1.Date = M3.Date and M1.MKBDTrailsPK = M3.MKBDTrailsPK

                        where M1.Date = @Date        

                        and M1.MKBDTrailsPK = @NewMKBDTrailsPK

  

                        update MKBD08 set   

                        [10E] = [8E] + [9E]   

                        where [Date] = @Date

                        and MKBDTrailsPK = @NewMKBDTrailsPK

    

                        update MKBD08 set   

                        [16E] = [10E] - [11E]- [13E] - [14E] -[15E]   

                        where [Date] = @Date 

                        and MKBDTrailsPK = @NewMKBDTrailsPK

   

                        update MKBD08 set [19E] = '0'

                        --EMCO  
                        --update MKBD08 set [19E] = 0.0625 * [16E]    

                        --where [Date] = @Date and MKBDTrailsPK = @NewMKBDTrailsPK

   

                        update MKBD08 set   

                        [20E] = case when [19E] > [18E] then [19E] else [18E] end   

                        where [Date] = @Date and MKBDTrailsPK = @NewMKBDTrailsPK

  

                        Update MKBD08 set   

                        [24E] = 0.001 * [23E]   

                        where [Date] = @Date and MKBDTrailsPK = @NewMKBDTrailsPK

   

                        update MKBD08 Set   

                        [25E] = [22E] + [24E]  

                        where [Date] = @Date and MKBDTrailsPK = @NewMKBDTrailsPK

   

                        update MKBD08 Set   

                        [26E] = [20E] + [25E]   

                        where [Date] = @Date and MKBDTrailsPK = @NewMKBDTrailsPK



                        -- UPDATE MKBD 09

                        update M9 set   

                        M9.[9E] = M1.[100B], M9.[11E] = M2.[164B], M9.[12E] = M3.[31B],  

                        M9.[17E] = M2.[163B], M9.[24E] = M1.[16B],  

                        M9.[93E] = M1.[39B], M9.[94G] = M1.[44B], M9.[103G] = M8.[26E],   

                        M9.[96G] = Case when M6.[10B] >= M6.[20D] then M6.[10B] - M6.[20D] else 0 end,  

                        M9.[97G] = case when M6.[13B] >= M6.[21B] then M6.[13B] - M6.[21B] else 0 end,  

                        M9.[98G] = case when M7.[11B] >= M7.[36D] then M7.[11B] - M7.[36D] else 0 end,  

                        M9.[99G] = case when M7.[61E] >= M7.[13B] then M7.[61E] - M7.[13B] else 0 end  

                        from MKBD09 M9    

                        left join MKBD01 M1 on M9.Date = M1.Date and  M9.MKBDTrailsPK = M1.MKBDTrailsPK     

                        left join MKBD02 M2 on M9.Date = M2.Date and  M9.MKBDTrailsPK = M2.MKBDTrailsPK   

                        left join MKBD03 M3 on M9.Date = M3.Date and  M9.MKBDTrailsPK = M3.MKBDTrailsPK     

                        left join MKBD06 M6 on M9.Date = M6.Date and  M9.MKBDTrailsPK = M6.MKBDTrailsPK     

                        left join MKBD07 M7 on M9.Date = M7.Date and  M9.MKBDTrailsPK = M7.MKBDTrailsPK     

                        left join MKBD08 M8 on M9.Date = M8.Date and  M9.MKBDTrailsPK = M8.MKBDTrailsPK     

                        where M9.Date = @Date  and M9.MKBDTrailsPK = @NewMKBDTrailsPK  

  

                        update MKBD09 set   

                        [13E] = [9E] - [11E] - [12E] where [Date] = @Date and MKBDTrailsPK = @NewMKBDTrailsPK   

                        update MKBD09 set   

                        [15E] = [13E] where [Date] = @Date and MKBDTrailsPK = @NewMKBDTrailsPK  

                        update MKBD09 set   

                        [18E] = [15E] + [17E] where [Date] = @Date and MKBDTrailsPK = @NewMKBDTrailsPK  

                        update MKBD09 set   

                        [20G] = [18E] where [Date] = @Date and MKBDTrailsPK = @NewMKBDTrailsPK  

                        update MKBD09 set   

                        [102G] = [20G] + [101G] where [Date] = @Date and MKBDTrailsPK = @NewMKBDTrailsPK  



                        set @ssql = ''    

                        declare A cursor for    

                        select col.name from sysobjects obj    

                        left join syscolumns col on obj.id = col.id    

                        where obj.name = 'MKBD09'    

                        and col.name not in ('MKBD09PK','MKBDTrailsPK', 'Date', 'CompanyID', 'CompanyName', 'DirectorName',     

                        'LastUpdate','DBUserID','DBTerminalID','LastUpdateDB')  

                        and col.name like '%G' and cast(left(col.name, len(col.name)-1) as int) between 24 and 99     

     

                        open A    

    

                        fetch next from A    

                        into @field     

    

                        while @@FETCH_STATUS = 0    

                        begin     

                        select @ssql = 'update MKBD09 set [102G] = [102G] - [' + @field + '] where date = ''' +  convert(varchar(8),@date,112) + ''''+ ' and MKBDTrailsPK = ' +  cast(@NewMKBDTrailsPK as varchar)    

                        exec (@ssql)    

      

                        fetch next from A    

                        into @field     

                        end    

    

                        close A    

                        deallocate A 



                        update MKBD09 set   

                        [104G] = [102G] - [103G] where [Date] = @Date and MKBDTrailsPK = @NewMKBDTrailsPK  



                        --Select @MsgSuccess = @MsgSuccess + 'NAWC Process Done'

                        --select '' ErrMsg,@MsgSuccess Msg



                        update mkbdTrails set BitValidate = 1,logMEssages = @MsgSuccess where MKBDTrailsPK = @NewMKBDTrailsPK


                        FETCH NEXT FROM DATE 
                        INTO @Date
                        END 
                        CLOSE DATE  
                        DEALLOCATE DATE

                        DECLARE @combinedString NVARCHAR(MAX)
                        SELECT @combinedString = COALESCE(@combinedString + ', ', '') + replace(convert(NVARCHAR, valuedate, 106), ' ', '/') from
                        (
                        select distinct  valuedate from #date

                        )A


                        IF (@ClientCode in ('03','21'))
                        BEGIN
                            -- TAMBAHAN
                            update A set A.A = A.B from MKBD04 A
                            left join MKBDTrails B on A.MKBDTrailsPK = B.MKBDTrailsPK 
                            where  date between @dateFrom and @dateTo and B.status = 1 


                            update A set A.B = '' from MKBD04 A
                            left join MKBDTrails B on A.MKBDTrailsPK = B.MKBDTrailsPK 
                            where  date between @dateFrom and @dateTo and B.status = 1 
                        END




                        SELECT 'Generate MKBD Success ! Valuedate : '  + @combinedString + ', Please Check Log' as msg
                        ";

                        cmd.CommandTimeout = 0;
                        cmd.Parameters.AddWithValue("@Status", 1);
                        cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _dateTo);
                        cmd.Parameters.AddWithValue("@UsersID", _userID);
                        cmd.Parameters.AddWithValue("@TimeNow", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@ClientCode", Tools.ClientCode);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                NAWCDailyProcess _NAWC = new NAWCDailyProcess();
                                _NAWC.Msg = Convert.ToString(dr["Msg"]);
                                return _NAWC;
                            }
                            return null;
                        }
                    }



                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }


        public string Validate_CheckNAWCDaily(DateTime _dateFrom, DateTime _dateTo)
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
                        
                        Create Table #NAWC
                        (ValueDate datetime)
                        
                        Insert Into #NAWC(ValueDate)
                        select ValueDate from MKBDTrails where ValueDate between @DateFrom and @DateTo and status = 2

                        if exists(select ValueDate from MKBDTrails where ValueDate between @DateFrom and @DateTo and status = 2)
                        BEGIN
                        DECLARE @combinedString VARCHAR(MAX)
                        SELECT @combinedString = COALESCE(@combinedString + ', ', '') + replace(convert(NVARCHAR, valuedate, 106), ' ', '/')
                        FROM #NAWC
                        SELECT 'Retrive Cancel, Please Check MKBDTrails in ValueDate : ' + @combinedString as Result 
                        END
                        ELSE
                        BEGIN
                        select '' Result
                        END   ";

                        cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _dateTo);


                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return Convert.ToString(dr["Result"]);

                            }
                            return "SALAH WWOYYYYY";
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }


        public string Validate_CheckJournal(DateTime _dateFrom, DateTime _dateTo)
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
                        
                        Create Table #Journal
                        (Reference nvarchar(50))
                        
                        Insert Into #Journal(Reference)
                        select Reference from Journal where ValueDate between @DateFrom and @DateTo and ((status = 2 and Posted = 0) or status = 1) 
                        DECLARE @combinedString VARCHAR(MAX)
                        if exists(select Reference from Journal where ValueDate between @DateFrom and @DateTo and ((status = 2 and Posted = 0) or status = 1)) 
                        BEGIN
                        SELECT @combinedString = COALESCE(@combinedString + ', ', '') + replace(convert(NVARCHAR, Reference, 106), ' ', '/')
                        FROM #Journal
                        SELECT 'Retrive Cancel, Please Check Journal in Reference : ' + @combinedString as Result 
                        END
                        ELSE
                        BEGIN
                        select '' Result
                        END 
                        ";

                        cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _dateTo);


                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return Convert.ToString(dr["Result"]);

                            }
                            return "SALAH WWOYYYYY";
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public string Validate_CheckCashier(DateTime _dateFrom, DateTime _dateTo)
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
                        
                        Create Table #Cashier
                        (Reference nvarchar(50))
                        
                        Insert Into #Cashier(Reference)
                        select Reference from Cashier where ValueDate between @DateFrom and @DateTo and ((status = 2 and Posted = 0) or status = 1) 
                        DECLARE @combinedString VARCHAR(MAX)
                        if exists(select Reference from Cashier where ValueDate between @DateFrom and @DateTo and ((status = 2 and Posted = 0) or status = 1)) 
                        BEGIN
                        SELECT @combinedString = COALESCE(@combinedString + ', ', '') + replace(convert(NVARCHAR, Reference, 106), ' ', '/')
                        FROM #Cashier
                        SELECT 'Retrive Cancel, Please Check Cashier in Reference : ' + @combinedString as Result 
                        END
                        ELSE
                        BEGIN
                        select '' Result
                        END 
                          ";

                        cmd.Parameters.AddWithValue("@DateFrom", _dateFrom);
                        cmd.Parameters.AddWithValue("@DateTo", _dateTo);


                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return Convert.ToString(dr["Result"]);

                            }
                            return "SALAH WWOYYYYY";
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }


        public void MKBDTrails_VoidBySelected(string _usersID, string _permissionID, DateTime _dateFrom, DateTime _dateTo)
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

                            Declare @ValueDate datetime
                            Declare @MKBDTrailsPK int
                            Declare @HistoryPK int

                            Declare A Cursor For
	                            Select ValueDate,MKBDTrailsPK,HistoryPK from MKBDTrails where ValueDate between @DateFrom and @DateTo and Status = 2 and Selected  = 1
                                order by ValueDate asc
                            Open A
                            Fetch next From A
                            into @ValueDate,@MKBDTrailsPK,@HistoryPK
                            While @@Fetch_status = 0
                            Begin

                            update MKBDTrails set status = 3,VoidUsersID = @UsersID,VoidTime = @Time,LastUpdate=@Time 
                            where MKBDTrailsPK = @MKBDTrailsPK and historypk = @historyPK

 
                            delete mkbd01 where date = @valuedate
                            delete mkbd02 where date = @valuedate
                            delete mkbd03 where date = @valuedate
                            delete mkbd04 where date = @valuedate
                            delete mkbd05 where date = @valuedate
                            delete mkbd06 where date = @valuedate
                            delete mkbd06Detail where date = @valuedate
                            delete mkbd07 where date = @valuedate
                            delete mkbd08 where date = @valuedate
                            delete mkbd09 where date = @valuedate
                            delete mkbd510A where date = @valuedate
                            delete mkbd510B where date = @valuedate
                            delete mkbd510C where date = @valuedate
                            delete mkbd510D where date = @valuedate
                            delete mkbd510E where date = @valuedate
                            delete mkbd510F where date = @valuedate
                            delete mkbd510G where date = @valuedate
                            delete mkbd510H where date = @valuedate
                            delete mkbd510I where date = @valuedate
                            delete RL504 where date = @valuedate
                            delete RL510ARepo where date = @valuedate
                            delete RL510BReverseRepo where date = @valuedate
                            delete RL510CBond where date = @valuedate
                            delete RL510CDeposito where date = @valuedate
                            delete RL510CEquity where date = @valuedate
                            delete RL510CSbn where date = @valuedate 

                            fetch next From A  into @ValueDate,@MKBDTrailsPK,@HistoryPK
                            end
                            Close A
                            Deallocate A

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