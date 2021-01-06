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
    public class FundClientDocumentReps
    {
        Host _host = new Host();

        //1
        string _insertCommand = "INSERT INTO [dbo].[FundClientDocumentFiles] " +
                            "([FundClientDocumentFilesPK],[HistoryPK],[Status],[Description],";
        string _paramaterCommand = "@Description,";

        //2
        private FundClientDocument setFundClientDocument(SqlDataReader dr)
        {
            FundClientDocument M_FundClientDocument = new FundClientDocument();
            M_FundClientDocument.FundClientDocumentPK = Convert.ToInt32(dr["FundClientDocumentFilesPK"]);
            M_FundClientDocument.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_FundClientDocument.Status = Convert.ToInt32(dr["Status"]);
            M_FundClientDocument.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_FundClientDocument.Notes = Convert.ToString(dr["Notes"]);
            M_FundClientDocument.FundClientPK = Convert.ToInt32(dr["FundClientPK"]);
            M_FundClientDocument.ID = Convert.ToString(dr["ID"]);
            M_FundClientDocument.Description = Convert.ToString(dr["Description"]);
            M_FundClientDocument.DocPath = Convert.ToString(dr["FilePath"]);
            M_FundClientDocument.EntryUsersID = dr["EntryUsersID"].ToString();
            M_FundClientDocument.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_FundClientDocument.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_FundClientDocument.VoidUsersID = dr["VoidUsersID"].ToString();
            M_FundClientDocument.EntryTime = dr["EntryTime"].ToString();
            M_FundClientDocument.UpdateTime = dr["UpdateTime"].ToString();
            M_FundClientDocument.ApprovedTime = dr["ApprovedTime"].ToString();
            M_FundClientDocument.VoidTime = dr["VoidTime"].ToString();
            M_FundClientDocument.DBUserID = dr["DBUserID"].ToString();
            M_FundClientDocument.DBTerminalID = dr["DBTerminalID"].ToString();
            M_FundClientDocument.LastUpdate = dr["LastUpdate"].ToString();
            M_FundClientDocument.LastUpdateDB = Convert.ToString(dr["LastUpdateDB"]);
            return M_FundClientDocument;
        }

        public List<FundClientDocument> FundClientDocument_Select(int _status)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<FundClientDocument> L_FundClientDocument = new List<FundClientDocument>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = "Select case when A.status=1 then 'PENDING' else Case When A.status = 2 then 'APPROVED' else Case when A.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,B.ID + '-' + B.Name ID,* From FundClientDocumentFiles A left join " +
                            " FundClient B on A.FundClientPK = B.FundClientPK and B.Status = 2 " +
                            " where A.status = @status order by FundClientDocumentFilesPK ";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = "Select case when A.status=1 then 'PENDING' else Case When A.status = 2 then 'APPROVED' else Case when A.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,B.ID + '-' + B.Name ID,* From FundClientDocumentFiles A left join " +
                            " FundClient B on A.FundClientPK = B.FundClientPK and B.Status = 2 order by FundClientDocumentFilesPK ";
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_FundClientDocument.Add(setFundClientDocument(dr));
                                }
                            }
                            return L_FundClientDocument;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int FundClientDocument_Update(FundClientDocument _FundClientDocument, bool _havePrivillege)
        {

            try
            {
                int _newHisPK;
                int status = _host.Get_Status(_FundClientDocument.FundClientDocumentPK, _FundClientDocument.HistoryPK, "FundClientDocumentFiles");
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update FundClientDocumentFiles set status = 2, Notes=@Notes,Description=@Description," +
                                "ApprovedUsersID=@ApprovedUsersID, " +
                                "ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@lastupdate " +
                                "where FundClientDocumentFilesPK = @PK and historyPK = @HistoryPK";
                            cmd.Parameters.AddWithValue("@HistoryPK", _FundClientDocument.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _FundClientDocument.FundClientDocumentPK);
                            cmd.Parameters.AddWithValue("@Notes", _FundClientDocument.Notes);
                            cmd.Parameters.AddWithValue("@Description", _FundClientDocument.Description);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _FundClientDocument.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _FundClientDocument.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update FundClientDocumentFiles set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where FundClientDocumentFilesPK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _FundClientDocument.FundClientDocumentPK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _FundClientDocument.EntryUsersID);
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
                                cmd.CommandText = "Update FundClientDocumentFiles set Notes=@Notes,Description=@Description," +
                                    "UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@lastupdate " +
                                    "where FundClientDocumentFilesPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _FundClientDocument.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _FundClientDocument.FundClientDocumentPK);
                                cmd.Parameters.AddWithValue("@Notes", _FundClientDocument.Notes);
                                cmd.Parameters.AddWithValue("@Description", _FundClientDocument.Description);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _FundClientDocument.EntryUsersID);
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
                                _newHisPK = _host.Get_NewHistoryPK(_FundClientDocument.FundClientDocumentPK, "FundClientDocumentFiles");
                                cmd.CommandText = @" declare @FileNumbers int
                                Select @FileNumbers = Filenumber from FundClientDocumentFiles where fundclientPK = @FundClientPK  and FIlePath = @DocPath


                                INSERT INTO [dbo].[FundClientDocumentFiles] ([FundClientDocumentFilesPK],[HistoryPK],[Status],[FundClientPK],[Description],[FilePath],[FileNumber],[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                "Select @PK,@NewHistoryPK,1,@FundClientPK,@Description,@DocPath,@FileNumbers,EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From FundClientDocumentFiles where FundClientDocumentFilesPK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _FundClientDocument.FundClientDocumentPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _FundClientDocument.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@Description", _FundClientDocument.Description);
                                cmd.Parameters.AddWithValue("@DocPath", _FundClientDocument.DocPath);
                                cmd.Parameters.AddWithValue("@FundClientPK", _FundClientDocument.FundClientPK);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _FundClientDocument.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                                cmd.ExecuteNonQuery();
                            }
                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update FundClientDocumentFiles set status= 4,Notes=@Notes," +
                                    "LastUpdate=@lastupdate where FundClientDocumentFilesPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _FundClientDocument.Notes);
                                cmd.Parameters.AddWithValue("@PK", _FundClientDocument.FundClientDocumentPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _FundClientDocument.HistoryPK);
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

        public void FundClientDocument_Approved(FundClientDocument _FundClientDocument)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update FundClientDocumentFiles set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,LastUpdate = @LastUpdate " +
                            "where FundClientDocumentFilesPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _FundClientDocument.FundClientDocumentPK);
                        cmd.Parameters.AddWithValue("@historyPK", _FundClientDocument.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _FundClientDocument.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update FundClientDocumentFiles set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where FundClientDocumentFilesPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _FundClientDocument.FundClientDocumentPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _FundClientDocument.ApprovedUsersID);
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

        public void FundClientDocument_Reject(FundClientDocument _FundClientDocument)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update FundClientDocumentFiles set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where FundClientDocumentFilesPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _FundClientDocument.FundClientDocumentPK);
                        cmd.Parameters.AddWithValue("@historyPK", _FundClientDocument.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _FundClientDocument.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update FundClientDocumentFiles set status= 2,LastUpdate=@LastUpdate where FundClientDocumentFilesPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _FundClientDocument.FundClientDocumentPK);
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

        public void FundClientDocument_Void(FundClientDocument _FundClientDocument)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update FundClientDocumentFiles set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where FundClientDocumentFilesPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _FundClientDocument.FundClientDocumentPK);
                        cmd.Parameters.AddWithValue("@historyPK", _FundClientDocument.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _FundClientDocument.VoidUsersID);
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
        public string DocumentImport(string _userID, string _fundclientID, int _number, string _newFile, string _desc)
        {
            DateTime _dateTime = DateTime.Now;
            try
            {

                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {


                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"
                                        
                                        DECLARE @MaxPK BIGINT, @FileNumber int

                                        SELECT @MaxPK = MAX(FundClientDocumentFilesPK) + 1 FROM FundClientDocumentFiles

                                        SET @MaxPK = ISNULL(@MaxPK,1)

                                        SELECT @FileNumber = @Number

                                        INSERT into FundClientDocumentFiles (FundClientDocumentFilesPK, HistoryPK, Status, FundClientPK, FileNumber, FilePath, Description, EntryUsersID,EntryTime,Lastupdate)
                                        Select @MaxPK, 1, 1,ISNULL(@FundClientPK,0),@FileNumber,@FileName, @Description, @EntryUsersID, @EntryTime ,@EntryTime 

                                        Select @FileName Msg
                                        ";
                        cmd.Parameters.AddWithValue("@FundClientPK", _fundclientID);
                        cmd.Parameters.AddWithValue("@Number", _number);
                        cmd.Parameters.AddWithValue("@FileName", _newFile);
                        cmd.Parameters.AddWithValue("@Description", _desc);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _userID);
                        cmd.Parameters.AddWithValue("@EntryTime", _dateTime);
                        using (SqlDataReader dr1 = cmd.ExecuteReader())
                        {
                            if (dr1.HasRows)
                            {
                                dr1.Read();
                                return Convert.ToString(dr1["Msg"]);

                            }

                        }
                        return "";
                    }



                }


            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int DocumentImport_GetFileNumber(string _userID, string _fundclientID)
        {
            DateTime _dateTime = DateTime.Now;
            try
            {

                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"
                                        
                                        DECLARE @FileNumber int


                                        SELECT @FileNumber = MAX(FileNumber) + 1 FROM FundClientDocumentFiles WHERE FundClientPK = @FundClientPK
                                        SET @FileNumber = ISNULL(@FileNumber,1)

                                        Select @FileNumber FileNumber

                                        ";
                        cmd.Parameters.AddWithValue("@FundClientPK", _fundclientID);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _userID);
                        using (SqlDataReader dr1 = cmd.ExecuteReader())
                        {
                            if (dr1.HasRows)
                            {
                                dr1.Read();
                                return Convert.ToInt32(dr1["FileNumber"]);

                            }

                        }
                        return 0;

                    }
                }


            }
            catch (Exception err)
            {
                throw err;
            }

        }


        public List<FundClientDocument> FundClientDocument_SelectForHighRiskMonitoring(int _FundClientPK)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<FundClientDocument> L_FundClientDocument = new List<FundClientDocument>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = @"
                             Select case when A.status=1 then 'PENDING' else Case When A.status = 2 then 'APPROVED' else Case when A.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,B.ID + '-' + B.Name ID,* From FundClientDocumentFiles A left join
                             FundClient B on A.FundClientPK = B.FundClientPK and B.Status = 2 
                             where A.status in (1,2) and A.FundClientPK = @FundClientPK order by FundClientDocumentFilesPK 
                            ";
                        cmd.Parameters.AddWithValue("@FundClientPK", _FundClientPK);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_FundClientDocument.Add(setFundClientDocument(dr));
                                }
                            }
                            return L_FundClientDocument;
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