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


namespace RFSRepository
{
    public class InstrumentTypeReps
    {
        Host _host = new Host();

        //1
        string _insertCommand = "INSERT INTO [dbo].[InstrumentType] " +
                            "([InstrumentTypePK],[HistoryPK],[Status],[ID],[Name],[Type],[GroupType],";
        string _paramaterCommand = "@ID,@Name,@Type,@GroupType,";

        //2
        private InstrumentType setInstrumentType(SqlDataReader dr)
        {
            InstrumentType M_InstrumentType = new InstrumentType();
            M_InstrumentType.InstrumentTypePK = Convert.ToInt32(dr["InstrumentTypePK"]);
            M_InstrumentType.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_InstrumentType.Status = Convert.ToInt32(dr["Status"]);
            M_InstrumentType.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_InstrumentType.Notes = Convert.ToString(dr["Notes"]);
            M_InstrumentType.ID = dr["ID"].ToString();
            M_InstrumentType.Name = dr["Name"].ToString();
            M_InstrumentType.Type = Convert.ToString(dr["Type"]);
            M_InstrumentType.GroupType = Convert.ToString(dr["GroupType"]);
            M_InstrumentType.TypeDesc = Convert.ToString(dr["TypeDesc"]);
            M_InstrumentType.GroupTypeDesc = Convert.ToString(dr["GroupTypeDesc"]);
            M_InstrumentType.EntryUsersID = dr["EntryUsersID"].ToString();
            M_InstrumentType.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_InstrumentType.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_InstrumentType.VoidUsersID = dr["VoidUsersID"].ToString();
            M_InstrumentType.EntryTime = dr["EntryTime"].ToString();
            M_InstrumentType.UpdateTime = dr["UpdateTime"].ToString();
            M_InstrumentType.ApprovedTime = dr["ApprovedTime"].ToString();
            M_InstrumentType.VoidTime = dr["VoidTime"].ToString();
            M_InstrumentType.DBUserID = dr["DBUserID"].ToString();
            M_InstrumentType.DBTerminalID = dr["DBTerminalID"].ToString();
            M_InstrumentType.LastUpdate = dr["LastUpdate"].ToString();
            M_InstrumentType.LastUpdateDB = Convert.ToString(dr["LastUpdateDB"]);
            return M_InstrumentType;
        }

        public List<InstrumentType> InstrumentType_Select(int _status)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<InstrumentType> L_InstrumentType = new List<InstrumentType>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = 
                                @"  select case when it.Status=1 then 'PENDING' else case when it.Status = 2 then 'APPROVED' else case when it.Status = 3 then 'VOID' else 'WAITING' end end end StatusDesc,MV.DescTwo TypeDesc,isnull (B.DescOne,'') GroupTypeDesc,* from [InstrumentType] IT 
                                 left join MasterValue MV on IT.Type = MV.Code and MV.ID ='InstrumentType' 
                                 left join MasterValue B on IT.Type = B.Code and B.ID ='InstrumentGroupType'
                                where IT.status = @status
                                order by it.InstrumentTypePK,it.ID,it.name ";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText =
                                @" select case when it.Status=1 then 'PENDING' else case when it.Status = 2 then 'APPROVED' else case when it.Status = 3 then 'VOID' else 'WAITING' end end end StatusDesc,MV.DescTwo TypeDesc,isnull (B.DescTwo,'') GroupTypeDesc,* from [InstrumentType] IT 
                                    left join MasterValue MV on IT.Type = MV.Code and MV.ID ='InstrumentType' 
                                    left join MasterValue B on IT.Type = B.Code and B.ID ='InstrumentGroupType'
                                    order by it.InstrumentTypePK,it.ID,it.name ";
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_InstrumentType.Add(setInstrumentType(dr));
                                }
                            }
                            return L_InstrumentType;
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
        public int InstrumentType_Add(InstrumentType _instrumentType, bool _havePrivillege)
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
                                 "Select isnull(max(InstrumentTypePk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@LastUpdate From InstrumentType";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _instrumentType.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);

                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[LastUpdate])" +
                                "Select isnull(max(InstrumentTypePk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@LastUpdate From InstrumentType";
                        }
                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@ID", _instrumentType.ID);
                        cmd.Parameters.AddWithValue("@Name", _instrumentType.Name);
                        cmd.Parameters.AddWithValue("@Type", _instrumentType.Type);
                        cmd.Parameters.AddWithValue("@GroupType", _instrumentType.GroupType);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _instrumentType.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);

                        cmd.ExecuteNonQuery();

                        return _host.Get_LastPKByLastUpate(_dateTimeNow, "InstrumentType");
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }


        public int InstrumentType_Update(InstrumentType _instrumentType, bool _havePrivillege)
        {
            try
            {
                int _newHisPK;
                int status = _host.Get_Status(_instrumentType.InstrumentTypePK, _instrumentType.HistoryPK, "instrumentType");
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update InstrumentType set status=2,Notes=@Notes,ID=@ID,Name=@Name,Type=@Type,GroupType=@GroupType," +
                                "ApprovedUsersID=@ApprovedUsersID, " +
                                "ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate = @LastUpdate " +
                                "where InstrumentTypePK = @PK and historyPK = @HistoryPK";
                            cmd.Parameters.AddWithValue("@HistoryPK", _instrumentType.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _instrumentType.InstrumentTypePK);
                            cmd.Parameters.AddWithValue("@ID", _instrumentType.ID);
                            cmd.Parameters.AddWithValue("@Notes", _instrumentType.Notes);
                            cmd.Parameters.AddWithValue("@Name", _instrumentType.Name);
                            cmd.Parameters.AddWithValue("@Type", _instrumentType.Type);
                            cmd.Parameters.AddWithValue("@GroupType", _instrumentType.GroupType);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _instrumentType.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _dateTimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _instrumentType.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);
                            cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update InstrumentType set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where InstrumentTypePK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _instrumentType.InstrumentTypePK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _instrumentType.EntryUsersID);
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
                                cmd.CommandText = "Update InstrumentType set Notes=@Notes,ID=@ID,Name=@Name,Type=@Type," +
                                    "UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate = @LastUpdate " +
                                    "where InstrumentTypePK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _instrumentType.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _instrumentType.InstrumentTypePK);
                                cmd.Parameters.AddWithValue("@ID", _instrumentType.ID);
                                cmd.Parameters.AddWithValue("@Notes", _instrumentType.Notes);
                                cmd.Parameters.AddWithValue("@Name", _instrumentType.Name);
                                cmd.Parameters.AddWithValue("@Type", _instrumentType.Type);
                                cmd.Parameters.AddWithValue("@GroupType", _instrumentType.GroupType);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _instrumentType.EntryUsersID);
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
                                _newHisPK = _host.Get_NewHistoryPK(_instrumentType.InstrumentTypePK, "InstrumentType");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From InstrumentType where InstrumentTypePK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _instrumentType.InstrumentTypePK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _instrumentType.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@ID", _instrumentType.ID);
                                cmd.Parameters.AddWithValue("@Name", _instrumentType.Name);
                                cmd.Parameters.AddWithValue("@Type", _instrumentType.Type);
                                cmd.Parameters.AddWithValue("@GroupType", _instrumentType.GroupType);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _instrumentType.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _dateTimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update InstrumentType set status= 4,Notes=@Notes," +
                                    "LastUpdate=@LastUpdate where InstrumentTypePK = @PK and historyPK = @HistoryPK ";
                                cmd.Parameters.AddWithValue("@Notes", _instrumentType.Notes);
                                cmd.Parameters.AddWithValue("@PK", _instrumentType.InstrumentTypePK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _instrumentType.HistoryPK);
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

        //7
        public void InstrumentType_Approved(InstrumentType _instrumentType)
        {
            try
            {
                DateTime _DateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update InstrumentType set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,LastUpdate = @LastUpdate " +
                            "where InstrumentTypePK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _instrumentType.InstrumentTypePK);
                        cmd.Parameters.AddWithValue("@historyPK", _instrumentType.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _instrumentType.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _DateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _DateTimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update InstrumentType set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where InstrumentTypePK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _instrumentType.InstrumentTypePK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _instrumentType.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _DateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _DateTimeNow);
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
        public void InstrumentType_Reject(InstrumentType _instrumentType)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update InstrumentType set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate = @LastUpdate " +
                            "where InstrumentTypePK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _instrumentType.InstrumentTypePK);
                        cmd.Parameters.AddWithValue("@historyPK", _instrumentType.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _instrumentType.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update InstrumentType set status= 2,LastUpdate=@LastUpdate where InstrumentTypePK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _instrumentType.InstrumentTypePK);
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
        public void InstrumentType_Void(InstrumentType _instrumentType)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update InstrumentType set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate = @LastUpdate " +
                            "where InstrumentTypePK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _instrumentType.InstrumentTypePK);
                        cmd.Parameters.AddWithValue("@historyPK", _instrumentType.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _instrumentType.VoidUsersID);
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

        // 10 AREA LAIN-LAIN dimulai dari sini ( untuk function diluar standart )
        public List<InstrumentTypeCombo> InstrumentType_Combo()
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<InstrumentTypeCombo> L_InstrumentType = new List<InstrumentTypeCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "SELECT  InstrumentTypePK,ID + ' - ' + Name ID, Name FROM [InstrumentType]  where status = 2 order by ID,Name";
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    InstrumentTypeCombo M_InstrumentType = new InstrumentTypeCombo();
                                    M_InstrumentType.InstrumentTypePK = Convert.ToInt32(dr["InstrumentTypePK"]);
                                    M_InstrumentType.ID = Convert.ToString(dr["ID"]);
                                    M_InstrumentType.Name = Convert.ToString(dr["Name"]);
                                    L_InstrumentType.Add(M_InstrumentType);
                                }
                            }
                            return L_InstrumentType;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

    

        public string InstrumentType_GetTypeByInstrumentPK(int _instrumentTypePK)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = " select MV.DescTwo TypeDesc,* from [InstrumentType] IT left join MasterValue MV " +
                            " on IT.Type = MV.Code and MV.ID ='InstrumentType' " +
                            " Where IT.InstrumentTypePK = @InstrumentTypePK and IT.status=2";
                        cmd.Parameters.AddWithValue("@InstrumentTypePK", _instrumentTypePK);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                dr.Read();
                                return Convert.ToString(dr["Type"]);
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

        public bool Listing(string _userID)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "select top 1 * from instrumentType"; //UNTUK CEK, ADA DATA APA ENGGA DI TABLE INI
                        using (SqlDataReader dr0 = cmd.ExecuteReader())
                        {
                            if (!dr0.Read())
                            {
                                return false;
                            }
                            else
                            {

                                string filePath = Tools.ReportsPath + "InstrumentType" + "_" + _userID + ".xlsx";
                                FileInfo excelFile = new FileInfo(filePath);
                                if (excelFile.Exists)
                                {
                                    excelFile.Delete();  // MASTIIN INI FILE BARU, JADI KITA HAPUS YANG LAMA
                                    excelFile = new FileInfo(filePath);
                                }

                                // SETUP EXCEL FILENYA DAN KASI NILAI KE PROPERTIES
                                ExcelPackage package = new ExcelPackage(excelFile);
                                package.Workbook.Properties.Title = "Listing";
                                package.Workbook.Properties.Author = Tools.DefaultReportAuthor();
                                package.Workbook.Properties.Comments = Tools.DefaultReportComments();
                                package.Workbook.Properties.Company = Tools.DefaultReportCompany();
                                package.Workbook.Properties.SetCustomPropertyValue("Checked by", _userID);
                                package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", Tools.DefaultReportAssemblyName());

                                // 1. SHEET APPROVED DLU
                                DbCon.Close();
                                DbCon.Open();
                                cmd.CommandText = "Select id,name,type,EntryUsersID,EntryTime,ApprovedUsersID,ApprovedTime,LastUpdate " +
                                  " From InstrumentType where Status = 2 Order by Name";
                                using (SqlDataReader dr1 = cmd.ExecuteReader())
                                {
                                    if (dr1.HasRows)
                                    {
                                        // BUAT NAMBAH SHEET DI WORKBOOK
                                        ExcelWorksheet worksheetApproved = package.Workbook.Worksheets.Add("ListingApproved");
                                        // KASI JUDUL FIELD YANG MAU DITAMPILIN DI BARIS KE-1
                                        worksheetApproved.Cells[1, 1].Value = "ID";
                                        worksheetApproved.Cells[1, 2].Value = "NAME";
                                        worksheetApproved.Cells[1, 3].Value = "TYPE";
                                        worksheetApproved.Cells[1, 4].Value = "GROUPTYPE";
                                        worksheetApproved.Cells[1, 5].Value = "E. ID";
                                        worksheetApproved.Cells[1, 6].Value = "ENTRY TIME";
                                        worksheetApproved.Cells[1, 7].Value = "A. ID";
                                        worksheetApproved.Cells[1, 8].Value = "APPROVED TIME";
                                        worksheetApproved.Cells[1, 9].Value = "LAST UPDATE";



                                        //  KASI STYLES ATAU CSS UNTUK JUDUL FIELD DI BARIS KE-1
                                        using (ExcelRange r = worksheetApproved.Cells["A1:H1"]) // KOLOM 1 SAMPE 8 A-H
                                        {
                                            //NILAINYA NGAMBIL DARI DEFAULT DI TOOLS, KLO MAU BEDA SENDIRI BOLEH2 AJA.
                                            r.Style.Font.Color.SetColor(Tools.DefaultReportColumnHeaderFontColor());
                                            r.Style.HorizontalAlignment = Tools.DefaultReportColumnHeaderHorizontalAlignment();
                                            r.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                            r.Style.Fill.BackgroundColor.SetColor(Tools.DefaultReportColumnHeaderBackgroundColor());
                                            r.Style.Font.Size = Tools.DefaultReportColumnHeaderFontSize();
                                            r.Style.Font.Bold = Tools.DefaultReportColumnHeaderFontBold();
                                            r.Style.Border.Top.Style = Tools.DefaultReportColumnHeaderBorderTop();
                                            r.Style.Border.Bottom.Style = Tools.DefaultReportColumnHeaderBorderBottom();
                                        }


                                        int incRowExcel = 2;
                                        while (dr1.Read())
                                        {
                                            int incColExcel = 1;
                                            for (int inc1 = 0; inc1 < dr1.FieldCount; inc1++)
                                            {
                                                worksheetApproved.Cells[incRowExcel, incColExcel].Style.Font.Size = Tools.DefaultReportFontSize();
                                                worksheetApproved.Cells[incRowExcel, incColExcel].Value = dr1.GetValue(inc1).ToString();
                                                incColExcel++;
                                            }
                                            incRowExcel++;
                                        }
                                        // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                        worksheetApproved.Cells["A1:H1"].AutoFitColumns(); // UNTUK AUTO FIT UKURAN TULISAN DENGAN KOLOM, KEBETULAN INI BUAT HEADER AJA DIPAKENYA
                                        worksheetApproved.Cells["D2:H2"].AutoFitColumns(); // CEK DARI ENTRY ID SAMPE LAST UPDATE
                                        worksheetApproved.PrinterSettings.Orientation = eOrientation.Landscape; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                        worksheetApproved.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN
                                        worksheetApproved.HeaderFooter.OddHeader.RightAlignedText = "&14 MASTER INSTRUMENT TYPE/APPROVED";

                                        // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                        worksheetApproved.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
                                        worksheetApproved.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
                                        worksheetApproved.View.PageLayoutView = Tools.DefaultReportPageLayoutView();
                                        worksheetApproved.View.ShowGridLines = Tools.DefaultReportShowGridLines();
                                        worksheetApproved.HeaderFooter.OddHeader.CenteredText = Tools.DefaultReportHeaderCenterText();
                                        worksheetApproved.HeaderFooter.OddHeader.LeftAlignedText = Tools.DefaultReportHeaderLeftText();

                                        worksheetApproved.HeaderFooter.OddFooter.RightAlignedText =
                                        string.Format("Page {0} of {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);

                                        worksheetApproved.HeaderFooter.OddFooter.CenteredText = Tools.DefaultReportFooterCenterText();
                                        worksheetApproved.HeaderFooter.OddFooter.LeftAlignedText = Tools.DefaultReportFooterLeftText();

                                    }
                                }

                                // 2. SHEET PENDING
                                DbCon.Close();
                                DbCon.Open();
                                cmd.CommandText = "Select id,name,type,EntryUsersID,EntryTime,UpdateUsersID,UpdateTime,LastUpdate " +
                                " From InstrumentType where Status = 1 Order by Name";
                                using (SqlDataReader dr1 = cmd.ExecuteReader())
                                {
                                    if (dr1.HasRows)
                                    {
                                        // BUAT NAMBAH SHEET DI WORKBOOK
                                        ExcelWorksheet worksheetPending = package.Workbook.Worksheets.Add("ListingPending");
                                        // KASI JUDUL FIELD YANG MAU DITAMPILIN DI BARIS KE-1
                                        worksheetPending.Cells[1, 1].Value = "ID";
                                        worksheetPending.Cells[1, 2].Value = "NAME";
                                        worksheetPending.Cells[1, 3].Value = "TYPE";
                                        worksheetPending.Cells[1, 4].Value = "TYPE";
                                        worksheetPending.Cells[1, 5].Value = "E. ID";
                                        worksheetPending.Cells[1, 6].Value = "ENTRY TIME";
                                        worksheetPending.Cells[1, 7].Value = "U. ID";
                                        worksheetPending.Cells[1, 8].Value = "UPDATE TIME";
                                        worksheetPending.Cells[1, 9].Value = "LAST UPDATE";


                                        //  KASI STYLES ATAU CSS UNTUK JUDUL FIELD DI BARIS KE-1
                                        using (ExcelRange r = worksheetPending.Cells["A1:H1"]) // KOLOM 1 SAMPE 8 A-H
                                        {
                                            //NILAINYA NGAMBIL DARI DEFAULT DI TOOLS, KLO MAU BEDA SENDIRI BOLEH2 AJA.
                                            r.Style.Font.Color.SetColor(Tools.DefaultReportColumnHeaderFontColor());
                                            r.Style.HorizontalAlignment = Tools.DefaultReportColumnHeaderHorizontalAlignment();
                                            r.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                            r.Style.Fill.BackgroundColor.SetColor(Tools.DefaultReportColumnHeaderBackgroundColor());
                                            r.Style.Font.Size = Tools.DefaultReportColumnHeaderFontSize();
                                            r.Style.Font.Bold = Tools.DefaultReportColumnHeaderFontBold();
                                            r.Style.Border.Top.Style = Tools.DefaultReportColumnHeaderBorderTop();
                                            r.Style.Border.Bottom.Style = Tools.DefaultReportColumnHeaderBorderBottom();
                                        }

                                        int incRowExcel = 2;

                                        while (dr1.Read())
                                        {
                                            int incColExcel = 1;
                                            for (int inc1 = 0; inc1 < dr1.FieldCount; inc1++)
                                            {
                                                worksheetPending.Cells[incRowExcel, incColExcel].Style.Font.Size = Tools.DefaultReportFontSize();
                                                worksheetPending.Cells[incRowExcel, incColExcel].Value = dr1.GetValue(inc1).ToString();
                                                incColExcel++;
                                            }
                                            incRowExcel++;
                                        }
                                        // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                        worksheetPending.Cells["A1:H1"].AutoFitColumns(); // UNTUK AUTO FIT UKURAN TULISAN DENGAN KOLOM, KEBETULAN INI BUAT HEADER AJA DIPAKENYA
                                        worksheetPending.Cells["D2:H2"].AutoFitColumns(); // CEK DARI ENTRY ID SAMPE LAST UPDATE
                                        worksheetPending.PrinterSettings.Orientation = eOrientation.Landscape; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                        worksheetPending.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN
                                        worksheetPending.HeaderFooter.OddHeader.RightAlignedText = "&14 MASTER INSTRUMENT TYPE/PENDING";

                                        // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                        worksheetPending.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
                                        worksheetPending.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
                                        worksheetPending.View.PageLayoutView = Tools.DefaultReportPageLayoutView();
                                        worksheetPending.View.ShowGridLines = Tools.DefaultReportShowGridLines();
                                        worksheetPending.HeaderFooter.OddHeader.CenteredText = Tools.DefaultReportHeaderCenterText();
                                        worksheetPending.HeaderFooter.OddHeader.LeftAlignedText = Tools.DefaultReportHeaderLeftText();

                                        worksheetPending.HeaderFooter.OddFooter.RightAlignedText =
                                        string.Format("Page {0} of {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);

                                        worksheetPending.HeaderFooter.OddFooter.CenteredText = Tools.DefaultReportFooterCenterText();
                                        worksheetPending.HeaderFooter.OddFooter.LeftAlignedText = Tools.DefaultReportFooterLeftText();
                                    }
                                }

                                // 3. SHEET HISTORY
                                DbCon.Close();
                                DbCon.Open();
                                cmd.CommandText = "select InstrumentTypePK,historypk,id,name,type, " +
                                    "case when status = 3 then 'HISTORY' else 'VOID' end as [status],lastUpdate from InstrumentType " +
                                    "where status in (3,5) " +
                                    "order by InstrumentTypePK,historypk";
                                using (SqlDataReader dr3 = cmd.ExecuteReader())
                                {
                                    if (dr3.HasRows)
                                    {
                                        // BUAT NAMBAH SHEET DI WORKBOOK
                                        ExcelWorksheet worksheetHistory = package.Workbook.Worksheets.Add("ListingHistory");
                                        // KASI JUDUL FIELD YANG MAU DITAMPILIN DI BARIS KE-1
                                        worksheetHistory.Cells[1, 1].Value = "ITEM NO";
                                        worksheetHistory.Cells[1, 2].Value = "HIST NO";
                                        worksheetHistory.Cells[1, 3].Value = "ID";
                                        worksheetHistory.Cells[1, 4].Value = "NAME";
                                        worksheetHistory.Cells[1, 5].Value = "TYPE";
                                        worksheetHistory.Cells[1, 6].Value = "TYPE";
                                        worksheetHistory.Cells[1, 7].Value = "STATUS";
                                        worksheetHistory.Cells[1, 8].Value = "LAST UPDATE";

                                        //  KASI STYLES ATAU CSS UNTUK JUDUL FIELD DI BARIS KE-1
                                        using (ExcelRange r = worksheetHistory.Cells["A1:G1"]) // KOLOM 1 SAMPE 7 A-G
                                        {
                                            //NILAINYA NGAMBIL DARI DEFAULT DI TOOLS, KLO MAU BEDA SENDIRI BOLEH2 AJA.
                                            r.Style.Font.Color.SetColor(Tools.DefaultReportColumnHeaderFontColor());
                                            r.Style.HorizontalAlignment = Tools.DefaultReportColumnHeaderHorizontalAlignment();
                                            r.Style.Fill.PatternType = ExcelFillStyle.Solid;
                                            r.Style.Fill.BackgroundColor.SetColor(Tools.DefaultReportColumnHeaderBackgroundColor());
                                            r.Style.Font.Size = Tools.DefaultReportColumnHeaderFontSize();
                                            r.Style.Font.Bold = Tools.DefaultReportColumnHeaderFontBold();
                                            r.Style.Border.Top.Style = Tools.DefaultReportColumnHeaderBorderTop();
                                            r.Style.Border.Bottom.Style = Tools.DefaultReportColumnHeaderBorderBottom();
                                        }

                                        int incRowExcel = 2;

                                        while (dr3.Read())
                                        {
                                            int incColExcel = 1;
                                            for (int inc1 = 0; inc1 < dr3.FieldCount; inc1++)
                                            {
                                                worksheetHistory.Cells[incRowExcel, incColExcel].Style.Font.Size = Tools.DefaultReportFontSize();
                                                worksheetHistory.Cells[incRowExcel, incColExcel].Value = dr3.GetValue(inc1).ToString();
                                                incColExcel++;
                                            }
                                            incRowExcel++;
                                        }
                                        // BAGIAN INI DI EDIT MANUAL PER REPORT SESUAI KEBUTUHAN
                                        worksheetHistory.Cells["A1:G1"].AutoFitColumns(); // UNTUK AUTO FIT UKURAN TULISAN DENGAN KOLOM, KEBETULAN INI BUAT HEADER AJA DIPAKENYA
                                        worksheetHistory.PrinterSettings.Orientation = eOrientation.Landscape; // INI PER REPORT BEDA2 KEBUTUHANNYA JADI DI CEK WAKTU DESIGN REPORTNYA
                                        worksheetHistory.PrinterSettings.RepeatRows = new ExcelAddress("1:1");// INI PERLU AGAR BARIS 1 MUNCUL TERUS WAKTU PRINT DI TIAP HALAMAN
                                        worksheetHistory.HeaderFooter.OddHeader.RightAlignedText = "&14 MASTER INSTRUMENT TYPE/HISTORY";

                                        // BAGIAN INI BIASANYA AMBIL DARI DEFAULT SETTING DI TOOLS, TPI BISA DIUBAH SENDIRI SESUAI KEBUTUHAN PER REPORT
                                        worksheetHistory.PrinterSettings.TopMargin = Tools.DefaultReportTopMargin();
                                        worksheetHistory.PrinterSettings.PaperSize = Tools.DefaultReportPaperSize();
                                        worksheetHistory.View.PageLayoutView = Tools.DefaultReportPageLayoutView();
                                        worksheetHistory.View.ShowGridLines = Tools.DefaultReportShowGridLines();
                                        worksheetHistory.HeaderFooter.OddHeader.CenteredText = Tools.DefaultReportHeaderCenterText();
                                        worksheetHistory.HeaderFooter.OddHeader.LeftAlignedText = Tools.DefaultReportHeaderLeftText();

                                        worksheetHistory.HeaderFooter.OddFooter.RightAlignedText =
                                        string.Format("Page {0} of {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);

                                        worksheetHistory.HeaderFooter.OddFooter.CenteredText = Tools.DefaultReportFooterCenterText();
                                        worksheetHistory.HeaderFooter.OddFooter.LeftAlignedText = Tools.DefaultReportFooterLeftText();
                                    }
                                }
                                package.Save();
                                package.Dispose();
                                return true;
                            }
                        }
                    }
                }
            }
            catch (Exception err)
            {
                return false;
                throw err;
            }

        }

        public List<InstrumentTypeCombo> InstrumentType_ComboRpt()
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<InstrumentTypeCombo> L_InstrumentType = new List<InstrumentTypeCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"SELECT  A.InstrumentTypePK,MV.DescTwo ID, Name FROM [InstrumentType]  A
                                            left join MasterValue MV on A.Type = MV.Code and MV.ID ='InstrumentType'
                                            where A.status = 2 and A.instrumenttypePK in (1,3,5) union all select 0,'All', '' order by InstrumentTypePK,Name ";

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    InstrumentTypeCombo M_InstrumentType = new InstrumentTypeCombo();
                                    M_InstrumentType.InstrumentTypePK = Convert.ToInt32(dr["InstrumentTypePK"]);
                                    M_InstrumentType.ID = Convert.ToString(dr["ID"]);
                                    M_InstrumentType.Name = Convert.ToString(dr["Name"]);
                                    L_InstrumentType.Add(M_InstrumentType);
                                }
                            }
                            return L_InstrumentType;
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