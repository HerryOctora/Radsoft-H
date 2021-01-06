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
    public class BlackListNameReps
    {
        Host _host = new Host();
       
        //1
        string _insertCommand = "INSERT INTO [dbo].[BlackListName] " +
                            "([BlackListNamePK],[HistoryPK],[Status],[NoDoc],[Type],[Name],[NoIDQ],[NameAlias],[NoID],[TempatLahir],[TanggalLahir],[Kewarganegaraan],[Alamat],[Description],[Pekerjaan],[NomorPasport],[JenisKelamin],[Agama],[NoKTP],[NoNPWP],[Tanggal],[SumberData],";
        string _paramaterCommand = "@NoDoc,@Type,@Name,@NoIDQ,@NameAlias,@NoID,@TempatLahir,@TanggalLahir,@Kewarganegaraan,@Alamat,@Description,@Pekerjaan,@NomorPasport,@JenisKelamin,@Agama,@NoKTP,@NoNPWP,@Tanggal,@SumberData,";

        //2
        private BlackListName setBlackListName(SqlDataReader dr)
        {
            BlackListName M_BlackListName = new BlackListName();
            M_BlackListName.BlackListNamePK = Convert.ToInt32(dr["BlackListNamePK"]);
            M_BlackListName.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_BlackListName.Status = Convert.ToInt32(dr["Status"]);
            M_BlackListName.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_BlackListName.Notes = Convert.ToString(dr["Notes"]);
            M_BlackListName.NoDoc = dr["NoDoc"].ToString();
            M_BlackListName.Type = Convert.ToInt32(dr["Type"]);
            M_BlackListName.TypeDesc = dr["TypeDesc"].ToString();
            M_BlackListName.Name = dr["Name"].ToString();
            M_BlackListName.NoIDQ = dr["NoIDQ"].ToString();
            M_BlackListName.NameAlias = dr["NameAlias"].ToString();
            M_BlackListName.NoID = dr["NoID"].ToString();
            M_BlackListName.TempatLahir = dr["TempatLahir"].ToString();
            M_BlackListName.TanggalLahir = dr["TanggalLahir"].ToString();
            M_BlackListName.Kewarganegaraan = dr["Kewarganegaraan"].ToString();
            M_BlackListName.Alamat = dr["Alamat"].ToString();
            M_BlackListName.Description = dr["Description"].ToString();
            M_BlackListName.Pekerjaan = dr["Pekerjaan"].ToString();
            M_BlackListName.NomorPasport = dr["NomorPasport"].ToString();
            M_BlackListName.JenisKelamin = dr["JenisKelamin"].ToString();
            M_BlackListName.Agama = dr["Agama"].ToString();
            M_BlackListName.NoKTP = dr["NoKTP"].ToString();
            M_BlackListName.NoNPWP = dr["NoNPWP"].ToString();
            M_BlackListName.Tanggal = dr["Tanggal"].ToString();
            M_BlackListName.SumberData = dr["SumberData"].ToString();
            M_BlackListName.EntryUsersID = dr["EntryUsersID"].ToString();
            M_BlackListName.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_BlackListName.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_BlackListName.VoidUsersID = dr["VoidUsersID"].ToString();
            M_BlackListName.EntryTime = dr["EntryTime"].ToString();
            M_BlackListName.UpdateTime = dr["UpdateTime"].ToString();
            M_BlackListName.ApprovedTime = dr["ApprovedTime"].ToString();
            M_BlackListName.VoidTime = dr["VoidTime"].ToString();
            M_BlackListName.DBUserID = dr["DBUserID"].ToString();
            M_BlackListName.DBTerminalID = dr["DBTerminalID"].ToString();
            M_BlackListName.LastUpdate = dr["LastUpdate"].ToString();
            M_BlackListName.LastUpdateDB = dr["LastUpdateDB"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["LastUpdateDB"]);
            return M_BlackListName;
        }

        //3
        public List<BlackListName> BlackListName_Select(int _status)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<BlackListName> L_BlackListName = new List<BlackListName>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = @"Select case when A.status=1 then 'PENDING' else Case When A.status = 2 then 'APPROVED' else Case when A.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,B.DescOne TypeDesc,* from BlackListName A
                                                left join MasterValue B on A.Type = B.Code and B.ID = 'BlackListNameType' and B.status = 2
                                                where A.status = @status";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = @"Select case when A.status=1 then 'PENDING' else Case When A.status = 2 then 'APPROVED' else Case when A.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,B.DescOne TypeDesc,* from BlackListName A
                                                left join MasterValue B on A.Type = B.Code and B.ID = 'BlackListNameType' and B.status = 2 ";
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_BlackListName.Add(setBlackListName(dr));
                                }
                            }
                            return L_BlackListName;
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
        public int BlackListName_Add(BlackListName _blackListName, bool _havePrivillege)
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
                                 "Select isnull(max(BlackListNamePK),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,LastUpdate=@LastUpdate from BlackListName";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _blackListName.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);

                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[LastUpdate])" +
                                "Select isnull(max(BlackListNamePK),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@LastUpdate from BlackListName";
                        }
                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@NoDoc", _blackListName.NoDoc);
                        cmd.Parameters.AddWithValue("@Type", _blackListName.Type);
                        cmd.Parameters.AddWithValue("@Name", _blackListName.Name);
                        cmd.Parameters.AddWithValue("@NoIDQ", _blackListName.NoIDQ);
                        cmd.Parameters.AddWithValue("@NameAlias", _blackListName.NameAlias);
                        cmd.Parameters.AddWithValue("@NoID", _blackListName.NoID);
                        cmd.Parameters.AddWithValue("@TempatLahir", _blackListName.TempatLahir);
                        cmd.Parameters.AddWithValue("@TanggalLahir", _blackListName.TanggalLahir);
                        cmd.Parameters.AddWithValue("@Kewarganegaraan", _blackListName.Kewarganegaraan);                        
                        cmd.Parameters.AddWithValue("@Alamat", _blackListName.Alamat);                       
                        cmd.Parameters.AddWithValue("@Description", _blackListName.Description);
                        cmd.Parameters.AddWithValue("@Pekerjaan", _blackListName.Pekerjaan);
                        cmd.Parameters.AddWithValue("@NomorPasport", _blackListName.NomorPasport);
                        cmd.Parameters.AddWithValue("@JenisKelamin", _blackListName.JenisKelamin);
                        cmd.Parameters.AddWithValue("@Agama", _blackListName.Agama);
                        cmd.Parameters.AddWithValue("@NoKTP", _blackListName.NoKTP);
                        cmd.Parameters.AddWithValue("@NoNPWP", _blackListName.NoNPWP);
                        cmd.Parameters.AddWithValue("@Tanggal", _blackListName.Tanggal);
                        cmd.Parameters.AddWithValue("@SumberData", _blackListName.SumberData);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _blackListName.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);

                        cmd.ExecuteNonQuery();

                        return _host.Get_LastPKByLastUpate(_dateTimeNow, "BlackListName");
                    }
                }

            }
            catch (Exception err)
            {
                throw err;
            }

        }


        //6
        public int BlackListName_Update(BlackListName _blackListName, bool _havePrivillege)
        {

            try
            {
                int _newHisPK;
                int status = _host.Get_Status(_blackListName.BlackListNamePK, _blackListName.HistoryPK, "BlackListName");
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update BlackListName set status=2, Notes=@Notes,NoDoc=@NoDoc,Type=@Type,Name=@Name,NoIDQ=@NoIDQ,NameAlias=@NameAlias,NoID=@NoID,TempatLahir=@TempatLahir,TanggalLahir=@TanggalLahir," +
                                "Kewarganegaraan=@Kewarganegaraan,Alamat=@Alamat,Description=@Description,Pekerjaan=@Pekerjaan,NomorPasport=@NomorPasport,JenisKelamin=@JenisKelamin,Agama=@Agama,NoKTP=@NoKTP,NoNPWP=@NoNPWP,Tanggal=@Tanggal,SumberData=@SumberData," +
                                "ApprovedUsersID=@ApprovedUsersID, " +
                                "ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@LastUpdate " +
                                "where BlackListNamePK = @PK and historyPK = @HistoryPK";
                            cmd.Parameters.AddWithValue("@HistoryPK", _blackListName.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _blackListName.BlackListNamePK);
                            cmd.Parameters.AddWithValue("@Notes", _blackListName.Notes);
                            cmd.Parameters.AddWithValue("@NoDoc", _blackListName.NoDoc);
                            cmd.Parameters.AddWithValue("@Type", _blackListName.Type);
                            cmd.Parameters.AddWithValue("@Name", _blackListName.Name);
                            cmd.Parameters.AddWithValue("@NoIDQ", _blackListName.NoIDQ);
                            cmd.Parameters.AddWithValue("@NameAlias", _blackListName.NameAlias);
                            cmd.Parameters.AddWithValue("@NoID", _blackListName.NoID);
                            cmd.Parameters.AddWithValue("@TempatLahir", _blackListName.TempatLahir);
                            cmd.Parameters.AddWithValue("@TanggalLahir", _blackListName.TanggalLahir);
                            cmd.Parameters.AddWithValue("@Kewarganegaraan", _blackListName.Kewarganegaraan);
                            cmd.Parameters.AddWithValue("@Alamat", _blackListName.Alamat);
                            cmd.Parameters.AddWithValue("@Description", _blackListName.Description);
                            cmd.Parameters.AddWithValue("@Pekerjaan", _blackListName.Pekerjaan);
                            cmd.Parameters.AddWithValue("@NomorPasport", _blackListName.NomorPasport);
                            cmd.Parameters.AddWithValue("@JenisKelamin", _blackListName.JenisKelamin);
                            cmd.Parameters.AddWithValue("@Agama", _blackListName.Agama);
                            cmd.Parameters.AddWithValue("@NoKTP", _blackListName.NoKTP);
                            cmd.Parameters.AddWithValue("@NoNPWP", _blackListName.NoNPWP);
                            cmd.Parameters.AddWithValue("@Tanggal", _blackListName.Tanggal);
                            cmd.Parameters.AddWithValue("@SumberData", _blackListName.SumberData);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _blackListName.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _dateTimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _blackListName.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);
                            cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);

                            cmd.ExecuteNonQuery();
                        }

                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update BlackListName set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@lastUpdate where BlackListNamePK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _blackListName.BlackListNamePK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _blackListName.EntryUsersID);
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
                                cmd.CommandText = "Update BlackListName set Notes=@Notes,NoDoc=@NoDoc,Type=@Type,Name=@Name,NoIDQ=@NoIDQ,NameAlias=@NameAlias,NoID=@NoID,TempatLahir=@TempatLahir,TanggalLahir=@TanggalLahir,Kewarganegaraan=@Kewarganegaraan,Alamat=@Alamat,Description=@Description,Pekerjaan=@Pekerjaan,NomorPasport=@NomorPasport," +
                                    "JenisKelamin=@JenisKelamin,Agama=@Agama,NoKTP=@NoKTP,NoNPWP=@NoNPWP,Tanggal=@Tanggal,SumberData=@SumberData,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@LastUpdate " +
                                    "where BlackListNamePK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _blackListName.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _blackListName.BlackListNamePK);
                                cmd.Parameters.AddWithValue("@Notes", _blackListName.Notes);
                                cmd.Parameters.AddWithValue("@NoDoc", _blackListName.NoDoc);
                                cmd.Parameters.AddWithValue("@Type", _blackListName.Type);
                                cmd.Parameters.AddWithValue("@Name", _blackListName.Name);
                                cmd.Parameters.AddWithValue("@NoIDQ", _blackListName.NoIDQ);
                                cmd.Parameters.AddWithValue("@NameAlias", _blackListName.NameAlias);
                                cmd.Parameters.AddWithValue("@NoID", _blackListName.NoID);
                                cmd.Parameters.AddWithValue("@TempatLahir", _blackListName.TempatLahir);
                                cmd.Parameters.AddWithValue("@TanggalLahir", _blackListName.TanggalLahir);
                                cmd.Parameters.AddWithValue("@Kewarganegaraan", _blackListName.Kewarganegaraan);
                                cmd.Parameters.AddWithValue("@Alamat", _blackListName.Alamat);
                                cmd.Parameters.AddWithValue("@Description", _blackListName.Description);
                                cmd.Parameters.AddWithValue("@Pekerjaan", _blackListName.Pekerjaan);
                                cmd.Parameters.AddWithValue("@NomorPasport", _blackListName.NomorPasport);
                                cmd.Parameters.AddWithValue("@JenisKelamin", _blackListName.JenisKelamin);
                                cmd.Parameters.AddWithValue("@Agama", _blackListName.Agama);
                                cmd.Parameters.AddWithValue("@NoKTP", _blackListName.NoKTP);
                                cmd.Parameters.AddWithValue("@NoNPWP", _blackListName.NoNPWP);
                                cmd.Parameters.AddWithValue("@Tanggal", _blackListName.Tanggal);
                                cmd.Parameters.AddWithValue("@SumberData", _blackListName.SumberData);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _blackListName.EntryUsersID);
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
                                _newHisPK = _host.Get_NewHistoryPK(_blackListName.BlackListNamePK, "BlackListName");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From BlackListName where BlackListNamePK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _blackListName.BlackListNamePK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _blackListName.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@NoDoc", _blackListName.NoDoc);
                                cmd.Parameters.AddWithValue("@Type", _blackListName.Type);
                                cmd.Parameters.AddWithValue("@Name", _blackListName.Name);
                                cmd.Parameters.AddWithValue("@NoIDQ", _blackListName.NoIDQ);
                                cmd.Parameters.AddWithValue("@NameAlias", _blackListName.NameAlias);
                                cmd.Parameters.AddWithValue("@NoID", _blackListName.NoID);
                                cmd.Parameters.AddWithValue("@TempatLahir", _blackListName.TempatLahir);
                                cmd.Parameters.AddWithValue("@TanggalLahir", _blackListName.TanggalLahir);
                                cmd.Parameters.AddWithValue("@Kewarganegaraan", _blackListName.Kewarganegaraan);
                                cmd.Parameters.AddWithValue("@Alamat", _blackListName.Alamat);
                                cmd.Parameters.AddWithValue("@Description", _blackListName.Description);
                                cmd.Parameters.AddWithValue("@Pekerjaan", _blackListName.Pekerjaan);
                                cmd.Parameters.AddWithValue("@NomorPasport", _blackListName.NomorPasport);
                                cmd.Parameters.AddWithValue("@JenisKelamin", _blackListName.JenisKelamin);
                                cmd.Parameters.AddWithValue("@Agama", _blackListName.Agama);
                                cmd.Parameters.AddWithValue("@NoKTP", _blackListName.NoKTP);
                                cmd.Parameters.AddWithValue("@NoNPWP", _blackListName.NoNPWP);
                                cmd.Parameters.AddWithValue("@Tanggal", _blackListName.Tanggal);
                                cmd.Parameters.AddWithValue("@SumberData", _blackListName.SumberData);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _blackListName.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _dateTimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update BlackListName set status= 4,Notes=@Notes, " +
                                    " LastUpdate=@LastUpdate where BlackListNamePK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _blackListName.Notes);
                                cmd.Parameters.AddWithValue("@PK", _blackListName.BlackListNamePK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _blackListName.HistoryPK);
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
        public void BlackListName_Approved(BlackListName _blackListName)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update BlackListName set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,LastUpdate = @LastUpdate " +
                            "where BlackListNamePK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _blackListName.BlackListNamePK);
                        cmd.Parameters.AddWithValue("@historyPK", _blackListName.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _blackListName.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update BlackListName set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@lastUpdate where BlackListNamePK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _blackListName.BlackListNamePK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _blackListName.ApprovedUsersID);
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
        public void BlackListName_Reject(BlackListName _blackListName)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update BlackListName set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime, LastUpdate = @LastUpdate " +
                            "where BlackListNamePK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _blackListName.BlackListNamePK);
                        cmd.Parameters.AddWithValue("@historyPK", _blackListName.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _blackListName.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update BlackListName set status= 2,LastUpdate=@lastUpdate where BlackListNamePK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _blackListName.BlackListNamePK);
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
        public void BlackListName_Void(BlackListName _blackListName)
        {
            try
            {
                DateTime _dateTimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update BlackListName set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate = @LastUpdate " +
                            "where BlackListNamePK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _blackListName.BlackListNamePK);
                        cmd.Parameters.AddWithValue("@historyPK", _blackListName.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _blackListName.VoidUsersID);
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
        public string ImportBlackListName(string _fileSource, string _userID)
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

                        using (SqlConnection conn = new SqlConnection(Tools.conString))
                        {
                            conn.Open();
                            using (SqlCommand cmd1 = conn.CreateCommand())
                            {
                                cmd1.CommandText = "truncate table BlackListNameTemp";
                                cmd1.ExecuteNonQuery();
                            }
                        }

                        // import data ke temp dulu
                        using (SqlBulkCopy bulkCopy = new SqlBulkCopy(Tools.conString, SqlBulkCopyOptions.TableLock))
                        {
                            bulkCopy.DestinationTableName = "dbo.BlackListNameTemp";
                            bulkCopy.WriteToServer(CreateDataTableFromBlackListNameTempExcelFile(_fileSource));
                            _msg = "Import BlackListName Success";
                        }

                        // logic kalo Reconcile success
                        using (SqlConnection conn = new SqlConnection(Tools.conString))
                        {
                            conn.Open();
                            using (SqlCommand cmd1 = conn.CreateCommand())
                            {
                                cmd1.CommandText =
                                @"      delete from BlackListNameTemp where Type = 'Type'
                                        delete from BlackListNameTemp where (Type is null and Nama is null and NoIDQ is null 
                                        and NamaAlias is null and NOID is null and TempatLahir is null and TanggalLahir is null 
                                        and Kewarganegaraan is null and Alamat is null and Keterangan is null and NoDoc is null)

                                        declare @NoDoc nvarchar(100)
                                        select @NoDoc = NoDoc from BlackListNameTemp where BlackListNameTempPK = 1
                                        update BlackListNameTemp set NoDoc = @NoDoc
 

                                        DELETE BlackListName where NoDoc = @NoDoc
                                        delete from BlackListNameTemp where Type is null

                                        DECLARE @BlackListNamePK BigInt  
                                        SELECT @BlackListNamePK = isnull(Max(BlackListNamePK),0) FROM BlackListName


                                        set @BlackListNamePK = isnull(@BlackListNamePK,1)

                                        INSERT INTO [dbo].[BlackListName]
                                        ([BlackListNamePK],[HistoryPK],[Status],[NoDoc],[Type],[Name],[NoIDQ],[NameAlias],[NoID],[TempatLahir],
                                        [TanggalLahir],[Kewarganegaraan],[Alamat],[Description],[EntryUsersID],
                                        [EntryTime],[LastUpdate])
                                        select Row_number() over(order by BlackListNameTempPK) + @BlackListNamePK,1,2,NoDoc,isnull(B.Code,0) Type,isnull(Nama,''),isnull(NoIDQ,''),isnull(NamaAlias,''),isnull(NoID,'')
                                        ,isnull(TempatLahir,''),isnull(TanggalLahir,''),isnull(Kewarganegaraan,''),isnull(Alamat,''),isnull(Keterangan,''),
                                        @UsersID,@LastUpdate,@LastUpdate
                                        from BlackListNameTemp A left join MasterValue B on A.Type = B.DescOne and B.ID = 'BlackListNameType' and B.status = 2
                                        group by BlackListNameTempPK,NoDoc,B.Code,Nama,NoIDQ,NamaAlias,NoID,TempatLahir,TanggalLahir,Kewarganegaraan,Alamat,Keterangan
                                        order by NoIDQ ";

                                cmd1.Parameters.AddWithValue("@UsersID", _userID);
                                cmd1.Parameters.AddWithValue("@LastUpdate", _now);
                                cmd1.ExecuteNonQuery();

                            }
                            _msg = "Import BlackListName Done";

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
        private DataTable CreateDataTableFromBlackListNameTempExcelFile(string _path)
        {
            try
            {
                using (DataTable dt = new DataTable())
                {
                    DataColumn dc;
                    DataRow dr;

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Int32");
                    dc.ColumnName = "BlackListNameTempPK";
                    dc.Unique = false;
                    dc.AutoIncrement = true;
                    dc.AutoIncrementSeed = 1;
                    dc.AutoIncrementStep = 1;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "NoDoc";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Type";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Nama";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "NoIDQ";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "NamaAlias";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "NOID";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "TempatLahir";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "TanggalLahir";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Kewarganegaraan";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Alamat";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Keterangan";
                    dc.Unique = false;
                    dt.Columns.Add(dc);



                    using (OleDbConnection odConnection = new OleDbConnection(Tools.ConStringExcel2007(_path)))
                    {
                        odConnection.Open();
                        using (OleDbCommand odCmd = odConnection.CreateCommand())
                        {
                            // _oldfilename = nama sheet yang ada di file excel yang diimport
                            odCmd.CommandText = "SELECT * FROM [Instansi$] union all SELECT * FROM [Individu$]";
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

                                    dr["NoDoc"] = odRdr[0];
                                    dr["Type"] = odRdr[1];
                                    dr["Nama"] = odRdr[2];
                                    dr["NoIDQ"] = odRdr[3];
                                    dr["NamaAlias"] = odRdr[4];
                                    dr["NOID"] = odRdr[5];
                                    dr["TempatLahir"] = odRdr[6];
                                    dr["TanggalLahir"] = odRdr[7];
                                    dr["Kewarganegaraan"] = odRdr[8];
                                    dr["Alamat"] = odRdr[9];
                                    dr["Keterangan"] = odRdr[10];

                                    if (dr["BlackListNameTempPK"].Equals(DBNull.Value) != true) { dt.Rows.Add(dr); }
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

        public string ImportBlackListNameDTTOT(string _fileSource, string _userID, string _source)
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

                        using (SqlConnection conn = new SqlConnection(Tools.conString))
                        {
                            conn.Open();
                            using (SqlCommand cmd1 = conn.CreateCommand())
                            {
                                cmd1.CommandText = "truncate table BlackListNameTemp";
                                cmd1.ExecuteNonQuery();
                            }
                        }

                        // import data ke temp dulu
                        using (SqlBulkCopy bulkCopy = new SqlBulkCopy(Tools.conString, SqlBulkCopyOptions.TableLock))
                        {
                            bulkCopy.DestinationTableName = "dbo.BlackListNameTemp";
                            bulkCopy.WriteToServer(CreateDataTableFromBlackListNameDTTOTTempExcelFile(_fileSource, _source));
                            _msg = "Import BlackListName Success";
                        }

                         //logic kalo Reconcile success
                        using (SqlConnection conn = new SqlConnection(Tools.conString))
                        {
                            conn.Open();
                            using (SqlCommand cmd1 = conn.CreateCommand())
                            {
                                cmd1.CommandText =
                                @"      delete from BlackListNameTemp where Type = 'Type'
                                        delete from BlackListNameTemp where (Type is null and Nama is null and NoIDQ is null 
                                        and NamaAlias is null and NOID is null and TempatLahir is null and TanggalLahir is null 
                                        and Kewarganegaraan is null and Alamat is null and SumberData is null and Keterangan is null and NoDoc is null)

                                        declare @SumberData nvarchar(100)
                                        select @SumberData = SumberData from BlackListNameTemp where BlackListNameTempPK = 1

                                        update BlackListNameTemp set SumberData = @SumberData
 

                                        DELETE BlackListName where SumberData = @SumberData
                                        delete from BlackListNameTemp where Type is null

                                        DECLARE @BlackListNamePK BigInt  
                                        SELECT @BlackListNamePK = isnull(Max(BlackListNamePK),0) FROM BlackListName


                                        set @BlackListNamePK = isnull(@BlackListNamePK,1)

                                        INSERT INTO [dbo].[BlackListName]
                                        ([BlackListNamePK],[HistoryPK],[Status],[NoDoc],[Type],[Name],[NoIDQ],[NameAlias],[NoID],[TempatLahir],[JenisKelamin],[Agama],[Pekerjaan],[NoKTP],[NoNPWP],[Tanggal],
                                        [TanggalLahir],[Kewarganegaraan],[Alamat],[SumberData],[Description],[EntryUsersID],
                                        [EntryTime],[LastUpdate])
                                        select Row_number() over(order by BlackListNameTempPK) + @BlackListNamePK,1,2,NoDoc,isnull(B.Code,0) Type,isnull(Nama,''),NoIDQ,isnull(NamaAlias,''),NoID
                                        ,isnull(TempatLahir,''),isnull(JenisKelamin,''),isnull(Agama,''),isnull(Pekerjaan,''),isnull(No_KTP,''),isnull(No_NPWP,''),isnull(Tanggal,''),isnull(TanggalLahir,''),isnull(Kewarganegaraan,''),isnull(Alamat,''),isnull(SumberData,''),isnull(Keterangan,''),
                                        @UsersID,@LastUpdate,@LastUpdate
                                        from BlackListNameTemp A left join MasterValue B on A.Type = B.DescOne and B.ID = 'BlackListNameType' and B.status = 2
                                        group by BlackListNameTempPK,NoDoc,B.Code,Nama,NoIDQ,NamaAlias,NoID,TempatLahir,TanggalLahir,Kewarganegaraan,Alamat,SumberData,Keterangan,JenisKelamin,Agama,Pekerjaan,No_KTP,No_NPWP,Tanggal
                                        order by NoIDQ ";

                                cmd1.Parameters.AddWithValue("@UsersID", _userID);
                                cmd1.Parameters.AddWithValue("@LastUpdate", _now);
                                cmd1.ExecuteNonQuery();

                            }
                            _msg = "Import BlackListName Done";

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
        private DataTable CreateDataTableFromBlackListNameDTTOTTempExcelFile(string _path, string _dataSource)
        {
            try
            {
                using (DataTable dt = new DataTable())
                {
                    DataColumn dc;
                    DataRow dr;

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Int32");
                    dc.ColumnName = "BlackListNameTempPK";
                    dc.Unique = false;
                    dc.AutoIncrement = true;
                    dc.AutoIncrementSeed = 1;
                    dc.AutoIncrementStep = 1;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "NoDoc";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Type";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Nama";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "NoIDQ";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "NamaAlias";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "NOID";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "TempatLahir";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "TanggalLahir";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Kewarganegaraan";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Alamat";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Keterangan";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "JenisKelamin";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Agama";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Pekerjaan";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "No_KTP";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "No_NPWP";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "SumberData";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Tanggal";
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

                                    dr["NoDoc"] = "-";
                                    dr["Type"] = "BlackList";
                                    dr["Nama"] = odRdr[1];
                                    dr["NoIDQ"] = "-";
                                    dr["NamaAlias"] = odRdr[1];
                                    dr["NOID"] = "-";
                                    dr["TempatLahir"] = odRdr[2];
                                    dr["TanggalLahir"] = odRdr[3];
                                    dr["Kewarganegaraan"] = odRdr[5];
                                    dr["Alamat"] = odRdr[10];
                                    dr["Keterangan"] = odRdr[11];
                                    dr["JenisKelamin"] = odRdr[4];
                                    dr["Agama"] = odRdr[6];
                                    dr["Pekerjaan"] = odRdr[7];
                                    dr["No_KTP"] = odRdr[8];
                                    dr["No_NPWP"] = odRdr[9];
                                    dr["SumberData"] = _dataSource;
                                    dr["Tanggal"] = "-";

                                    if (dr["BlackListNameTempPK"].Equals(DBNull.Value) != true) { dt.Rows.Add(dr); }
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


        public string ImportBlackListNamePPATK(string _fileSource, string _userID, string _source)
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

                        using (SqlConnection conn = new SqlConnection(Tools.conString))
                        {
                            conn.Open();
                            using (SqlCommand cmd1 = conn.CreateCommand())
                            {
                                cmd1.CommandText = "truncate table BlackListNameTemp";
                                cmd1.ExecuteNonQuery();
                            }
                        }

                        // import data ke temp dulu
                        using (SqlBulkCopy bulkCopy = new SqlBulkCopy(Tools.conString, SqlBulkCopyOptions.TableLock))
                        {
                            bulkCopy.DestinationTableName = "dbo.BlackListNameTemp";
                            bulkCopy.WriteToServer(CreateDataTableFromBlackListNamePPATKTempExcelFile(_fileSource, _source));
                            _msg = "Import BlackListName Success";
                        }

                         //logic kalo Reconcile success
                                using (SqlConnection conn = new SqlConnection(Tools.conString))
                                {
                                    conn.Open();
                                    using (SqlCommand cmd1 = conn.CreateCommand())
                                    {
                                        cmd1.CommandText =
                                        @"      delete from BlackListNameTemp where Type = 'Type'
                                                delete from BlackListNameTemp where (Type is null and Nama is null and NoIDQ is null 
                                                and NamaAlias is null and NOID is null and TempatLahir is null and TanggalLahir is null 
                                                and Kewarganegaraan is null and Alamat is null and SumberData is null and Keterangan is null and NoDoc is null)

                                                declare @SumberData nvarchar(100)
                                                select @SumberData = SumberData from BlackListNameTemp where BlackListNameTempPK = 1

                                                update BlackListNameTemp set SumberData = @SumberData
 

                                                DELETE BlackListName where SumberData = @SumberData
                                                delete from BlackListNameTemp where Type is null

                                                DECLARE @BlackListNamePK BigInt  
                                                SELECT @BlackListNamePK = isnull(Max(BlackListNamePK),0) FROM BlackListName


                                                set @BlackListNamePK = isnull(@BlackListNamePK,1)

                                                INSERT INTO [dbo].[BlackListName]
                                                ([BlackListNamePK],[HistoryPK],[Status],[NoDoc],[Type],[Name],[NoIDQ],[NameAlias],[NoID],[TempatLahir],
                                                [TanggalLahir],[Kewarganegaraan],[Alamat],[SumberData],[Description],[JenisKelamin],[Agama],[Pekerjaan],[NoKTP],[NoNPWP],[Tanggal],[EntryUsersID],
                                                [EntryTime],[LastUpdate])
                                                select Row_number() over(order by BlackListNameTempPK) + @BlackListNamePK,1,2,NoDoc,isnull(B.Code,0) Type,isnull(Nama,''),NoIDQ,isnull(NamaAlias,''),NoID
                                                ,isnull(TempatLahir,''),isnull(TanggalLahir,''),isnull(Kewarganegaraan,''),isnull(Alamat,''),isnull(SumberData,''),isnull(Keterangan,''),isnull(JenisKelamin,''),isnull(Agama,''),isnull(Pekerjaan,''),isnull(No_KTP,''),isnull(No_NPWP,''),isnull(Tanggal,''),
                                                @UsersID,@LastUpdate,@LastUpdate
                                                from BlackListNameTemp A left join MasterValue B on A.Type = B.DescOne and B.ID = 'BlackListNameType' and B.status = 2
                                                group by BlackListNameTempPK,NoDoc,B.Code,Nama,NoIDQ,NamaAlias,NoID,TempatLahir,TanggalLahir,Kewarganegaraan,Alamat,SumberData,Keterangan,JenisKelamin,Agama,Pekerjaan,No_KTP,No_NPWP,Tanggal
                                                order by NoIDQ ";

                                        cmd1.Parameters.AddWithValue("@UsersID", _userID);
                                        cmd1.Parameters.AddWithValue("@LastUpdate", _now);
                                        cmd1.ExecuteNonQuery();

                                    }
                                    _msg = "Import BlackListName Done";

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
        private DataTable CreateDataTableFromBlackListNamePPATKTempExcelFile(string _path, string _dataSource)
        {
            try
            {
                using (DataTable dt = new DataTable())
                {
                    DataColumn dc;
                    DataRow dr;

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Int32");
                    dc.ColumnName = "BlackListNameTempPK";
                    dc.Unique = false;
                    dc.AutoIncrement = true;
                    dc.AutoIncrementSeed = 1;
                    dc.AutoIncrementStep = 1;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "NoDoc";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Type";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Nama";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "NoIDQ";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "NamaAlias";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "NOID";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "TempatLahir";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "TanggalLahir";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Kewarganegaraan";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Alamat";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Keterangan";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "JenisKelamin";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Agama";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Pekerjaan";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "No_KTP";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "No_NPWP";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "SumberData";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Tanggal";
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

                                    dr["NoDoc"] = "-";
                                    dr["Type"] = "BlackList";
                                    dr["Nama"] = odRdr[1];
                                    dr["NoIDQ"] = "-";
                                    dr["NamaAlias"] = odRdr[1];
                                    dr["NOID"] = "-";
                                    dr["TempatLahir"] = odRdr[2];
                                    dr["TanggalLahir"] = odRdr[3];
                                    dr["Kewarganegaraan"] = odRdr[5];
                                    dr["Alamat"] = odRdr[10];
                                    dr["Keterangan"] = odRdr[11];
                                    dr["JenisKelamin"] = odRdr[4];
                                    dr["Agama"] = odRdr[6];
                                    dr["Pekerjaan"] = odRdr[7];
                                    dr["No_KTP"] = odRdr[8];
                                    dr["No_NPWP"] = odRdr[9];
                                    dr["SumberData"] = _dataSource;
                                    dr["Tanggal"] = odRdr[0];

                                    if (dr["BlackListNameTempPK"].Equals(DBNull.Value) != true) { dt.Rows.Add(dr); }
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

        public string ImportBlackListNameKPK(string _fileSource, string _userID, string _source)
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

                        using (SqlConnection conn = new SqlConnection(Tools.conString))
                        {
                            conn.Open();
                            using (SqlCommand cmd1 = conn.CreateCommand())
                            {
                                cmd1.CommandText = "truncate table BlackListNameTemp";
                                cmd1.ExecuteNonQuery();
                            }
                        }

                        // import data ke temp dulu
                        using (SqlBulkCopy bulkCopy = new SqlBulkCopy(Tools.conString, SqlBulkCopyOptions.TableLock))
                        {
                            bulkCopy.DestinationTableName = "dbo.BlackListNameTemp";
                            bulkCopy.WriteToServer(CreateDataTableFromBlackListNameKPKTempExcelFile(_fileSource, _source));
                            _msg = "Import BlackListName Success";
                        }

                        // logic kalo Reconcile success
                        using (SqlConnection conn = new SqlConnection(Tools.conString))
                        {
                            conn.Open();
                            using (SqlCommand cmd1 = conn.CreateCommand())
                            {
                                cmd1.CommandText =
                                @"      delete from BlackListNameTemp where Type = 'Type'
                                        delete from BlackListNameTemp where (Type is null and Nama is null and NoIDQ is null 
                                        and NamaAlias is null and NOID is null and TempatLahir is null and TanggalLahir is null 
                                        and Kewarganegaraan is null and Alamat is null and SumberData is null and Keterangan is null and NoDoc is null)

                                        declare @SumberData nvarchar(100)
                                        select @SumberData = SumberData from BlackListNameTemp where BlackListNameTempPK = 1

                                        update BlackListNameTemp set SumberData = @SumberData
 

                                        DELETE BlackListName where SumberData = @SumberData
                                        delete from BlackListNameTemp where Type is null

                                        DECLARE @BlackListNamePK BigInt  
                                        SELECT @BlackListNamePK = isnull(Max(BlackListNamePK),0) FROM BlackListName


                                        set @BlackListNamePK = isnull(@BlackListNamePK,1)

                                        INSERT INTO [dbo].[BlackListName]
                                        ([BlackListNamePK],[HistoryPK],[Status],[NoDoc],[Type],[Name],[NoIDQ],[NameAlias],[NoID],[TempatLahir],[JenisKelamin],[Agama],[Pekerjaan],[NoKTP],[NoNPWP],[Tanggal],
                                        [TanggalLahir],[Kewarganegaraan],[Alamat],[SumberData],[Description],[EntryUsersID],
                                        [EntryTime],[LastUpdate])
                                        select Row_number() over(order by BlackListNameTempPK) + @BlackListNamePK,1,2,NoDoc,isnull(B.Code,0) Type,isnull(Nama,''),NoIDQ,isnull(NamaAlias,''),NoID
                                        ,isnull(TempatLahir,''),isnull(JenisKelamin,''),isnull(Agama,''),isnull(Pekerjaan,''),isnull(No_KTP,''),isnull(No_NPWP,''),isnull(Tanggal,''),isnull(TanggalLahir,''),isnull(Kewarganegaraan,''),isnull(Alamat,''),isnull(SumberData,''),isnull(Keterangan,''),
                                        @UsersID,@LastUpdate,@LastUpdate
                                        from BlackListNameTemp A left join MasterValue B on A.Type = B.DescOne and B.ID = 'BlackListNameType' and B.status = 2
                                        group by BlackListNameTempPK,NoDoc,B.Code,Nama,NoIDQ,NamaAlias,NoID,TempatLahir,TanggalLahir,Kewarganegaraan,Alamat,SumberData,Keterangan,JenisKelamin,Agama,Pekerjaan,No_KTP,No_NPWP,Tanggal
                                        order by NoIDQ";

                                cmd1.Parameters.AddWithValue("@UsersID", _userID);
                                cmd1.Parameters.AddWithValue("@LastUpdate", _now);
                                cmd1.ExecuteNonQuery();

                            }
                            _msg = "Import BlackListName Done";

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
        private DataTable CreateDataTableFromBlackListNameKPKTempExcelFile(string _path, string _dataSource)
        {
            try
            {
                using (DataTable dt = new DataTable())
                {
                    DataColumn dc;
                    DataRow dr;

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Int32");
                    dc.ColumnName = "BlackListNameTempPK";
                    dc.Unique = false;
                    dc.AutoIncrement = true;
                    dc.AutoIncrementSeed = 1;
                    dc.AutoIncrementStep = 1;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "NoDoc";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Type";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Nama";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "NoIDQ";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "NamaAlias";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "NOID";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "TempatLahir";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "TanggalLahir";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Kewarganegaraan";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Alamat";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Keterangan";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "JenisKelamin";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Agama";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Pekerjaan";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "No_KTP";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "No_NPWP";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "SumberData";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Tanggal";
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

                                    dr["NoDoc"] = "-";
                                    dr["Type"] = "BlackList";
                                    dr["Nama"] = odRdr[0];
                                    dr["NoIDQ"] = "-";
                                    dr["NamaAlias"] = odRdr[0];
                                    dr["NOID"] = "-";
                                    dr["TempatLahir"] = odRdr[1];
                                    dr["TanggalLahir"] = odRdr[2];
                                    dr["Kewarganegaraan"] = odRdr[4];
                                    dr["Alamat"] = odRdr[9];
                                    dr["Keterangan"] = odRdr[10];
                                    dr["JenisKelamin"] = odRdr[3];
                                    dr["Agama"] = odRdr[5];
                                    dr["Pekerjaan"] = odRdr[6];
                                    dr["No_KTP"] = odRdr[7];
                                    dr["No_NPWP"] = odRdr[8];
                                    dr["SumberData"] = _dataSource;
                                    dr["Tanggal"] = "-";

                                    if (dr["BlackListNameTempPK"].Equals(DBNull.Value) != true) { dt.Rows.Add(dr); }
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


        public void InsertBlackListName(string _usersID)
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
--declare @ClientCode nvarchar(20),
--@UsersID nvarchar(20), 
--@LastUpdate datetime

--set @ClientCode = '10'
--set @UsersID = 'admin'
--set @LastUpdate = getdate()



--drop table #tempTable  
--drop table #TableBlackList  
--drop table #tblClient  
--drop table #tempTableBlackListName  
--drop table #tblBlackList

CREATE table  #TableBlackList  
      ( 
         fundclientpk INT, 
         date         DATETIME, 
         reason       NVARCHAR(max) ,
		 StatusCountryRisk int, 
		 StatusOccupationRisk int, 
		 StatusPoliticallyRisk int, 
		 StatusBusinessRisk int, 
		 StatusClientNameRisk int, 
		 StatusSpouseNameRisk int, 
		 StatusRDNNameRisk int
      ) 
CREATE CLUSTERED INDEX indx_tblBlackList  ON #TableBlackList (fundclientpk);



    DECLARE @CheckInsert INT 
    DECLARE @StatusCountryRisk INT 
    DECLARE @StatusOccupationRisk INT 
    DECLARE @StatusPoliticallyRisk INT 
    DECLARE @StatusBusinessRisk INT 
    DECLARE @StatusClientNameRisk INT 
    DECLARE @StatusSpouseNameRisk INT 
    DECLARE @StatusRDNNameRisk INT 
    DECLARE @HrBusinessCode INT 
    DECLARE @HrPEPCode INT 
    DECLARE @HrOccupation INT 
    DECLARE @CountryCode NVARCHAR(10) 
    DECLARE @BlackListName NVARCHAR(500) 
    DECLARE @BlackListDesc NVARCHAR(max) 
    DECLARE @Name NVARCHAR(200) 
    DECLARE @InvestorFirstName NVARCHAR(200) 
    DECLARE @InvestorMiddleName NVARCHAR(200) 
    DECLARE @InvestorLastName NVARCHAR(200) 
    DECLARE @InvestorSpouseName NVARCHAR(200) 
    DECLARE @investorAuthorizedPersonFirstName1 NVARCHAR(200) 
    DECLARE @InvestorAuthorizedPersonMiddleName1 NVARCHAR(200) 
    DECLARE @InvestorAuthorizedPersonLastName1 NVARCHAR(200) 
	declare @BlackListTanggalLahir date

    CREATE TABLE #tempTable   
      (   
         BlacklistNamePK INT,   
         date         date
      )   

	create table #tempTableBlackListName (  
		BlackListName nvarchar(max),  
		BlackListDesc nvarchar(max),
		TanggalLahir date
	)  
		

	Create TABLE #tblBlackList  
    ( 
		fundclientpk int,
        fieldname    NVARCHAR(500), 
        NAME         NVARCHAR(500), 
        highriskname NVARCHAR(500), 
        highriskdesc NVARCHAR(1000), 
        percentage   NUMERIC(18, 0) 
    ) 

	

	Create table #tblClient (
		FundClientPK int,
		DOB DATE,
		NatureOfBusiness nvarchar(20),
		politis nvarchar(20),
		pekerjaan nvarchar(20),
		negara nvarchar(20),
		Name nvarchar(500)
	)
	CREATE CLUSTERED INDEX indx_tblClient  ON #tblClient (FundClientPK);

	
	begin
		insert into #tblClient 
		select distinct FundClientPK,TanggalLahir,NatureOfBusiness 
		,Politis,pekerjaan,negara,name
		from FundClient where status in (1,2) --and SID = ''
		--and FundClientPK between 21229 and 24229
		--and FundClientPK between 21588 and 21590

		insert into #tempTable 
		select distinct BlacklistNamePK,cast(TanggalLahir as date) from BlackListName A
		INNER JOIN #tblClient B ON A.TanggalLahir = B.DOB
		where status = 2 and isdate(Tanggallahir) = 1 

		delete #tblClient where DOB not in (select date from #tempTable)


		insert into #TableBlackList (FundClientPK,reason,StatusBusinessRisk)
		select A.FundClientPK, isnull(' | Masuk dalam category High risk Business : ' + LEFT(descone, Len(descone)),''),1 StatusBusinessRisk 
		from #tblClient A
		left join MasterValue B on A.NatureOfBusiness = B.Code and B.ID = 'HrBusiness' and B.Status = 2
		where  A.natureofbusiness <> 99 and len(descone) > 1 

		insert into #TableBlackList (FundClientPK,reason,StatusPoliticallyRisk)
		select A.FundClientPK, isnull(' | Masuk dalam category High risk Politically Exposed Person : ' + LEFT(descone, Len(descone)),''), 1 StatusPoliticallyRisk 
		from #tblClient A
		left join MasterValue B on A.politis = B.Code and B.ID = 'HrPEP' and B.Status = 2
		where A.politis <> 99 and len(descone) > 1 

		insert into #TableBlackList (FundClientPK,reason,StatusOccupationRisk)
		select A.FundClientPK, isnull(' | Masuk dalam category High risk Occupation : ' + LEFT(descone, Len(descone)),''), 1 StatusOccupationRisk 
		from #tblClient A
		left join MasterValue B on A.pekerjaan = B.Code and B.ID = 'Occupation' and B.Status = 2
		where A.pekerjaan = 5 and len(descone) > 1 

		insert into #TableBlackList (FundClientPK,reason,StatusCountryRisk)
		select A.FundClientPK, isnull(' | Masuk dalam category High risk Country : ' + LEFT(descone, Len(descone)),''), 1 StatusCountryRisk 
		from #tblClient A
		left join MasterValue B on A.negara = B.Code and B.ID = 'SDICountry' and B.Status = 2 and IsHighRisk = 1
		where  len(descone) > 1

		INSERT INTO #tempTableBlackListName(BlackListName,BlackListDesc,TanggalLahir)
		SELECT DISTINCT Isnull(namealias, ''),   
		  Isnull(nodoc, '') + ',' + Isnull(B.descone, '')   
		  + ',' + Isnull(noidq, '') + ',' + Isnull(noid, '') + ','   
		  + Isnull(tempatlahir, '') + ','   
		  + Isnull(tanggallahir, '') + ','   
		  + Isnull(kewarganegaraan, '') + ','   
		  + Isnull(alamat, '') + ','   
		  + Isnull(description, '') + ','   
		  + Isnull(pekerjaan, '') ,  
		  cast(TanggalLahir as date)
		 FROM   blacklistname A   
		  LEFT JOIN mastervalue B   
		   ON A.type = B.code   
			  AND B.id = 'BlackListNameType'   
			  AND b.status = 2   
			--LEFT JOIN #tempTable C on A.BlackListNamePK = C.BlacklistNamePK
		 WHERE  A.status = 2   
		  AND Isnull(A.namealias, '') <> ''   
		  AND Len(Rtrim(Ltrim(namealias))) > 1   
		  AND A.BlackListNamePK in (select BlacklistNamePK from #tempTable)
		 UNION ALL   
		 SELECT DISTINCT Isnull(NAME, ''),   
			 Isnull(nodoc, '') + ',' + Isnull(B.descone, '')   
			 + ',' + Isnull(noidq, '') + ',' + Isnull(noid, '') + ','   
			 + Isnull(tempatlahir, '') + ','   
			 + Isnull(tanggallahir, '') + ','   
			 + Isnull(kewarganegaraan, '') + ','   
			 + Isnull(alamat, '') + ','   
			 + Isnull(description, '') + ','   
			 + Isnull(pekerjaan, '') ,  
		  cast(TanggalLahir as date)  
		 FROM   blacklistname A   
		  LEFT JOIN mastervalue B   
		   ON A.type = B.code   
			  AND B.id = 'BlackListNameType'   
			  AND b.status = 2   
			--inner join #tempTable C on A.BlackListNamePK = C.BlacklistNamePK
		 WHERE  A.status = 2   
		  AND Isnull(A.NAME, '') <> ''   
		  AND Len(Rtrim(Ltrim(a.description))) > 3   
		  AND A.type <> 0   
		  AND Len(Rtrim(Ltrim(NAME))) > 1   
		  AND A.BlackListNamePK in (select BlacklistNamePK from #tempTable)
	end
	
	


insert into #tblBlackList
Select A.FundClientPK,'Name',A.Name,B.BlackListName,B.BlackListDesc, dbo.[Fgetpercentageoftwostringmatching](Name, BlackListName) A From #tblClient A
left join #tempTableBlackListName B on A.DOB = B.TanggalLahir
where dbo.[Fgetpercentageoftwostringmatching](Name, BlackListName) > 70



	update A set reason = isnull(reason,'') + Char(10) 
    + ' | Masuk dalam category High Risk ' 
    + B.fieldname + ' = Fund Client Name : ' + B.NAME 
    + ' * High Risk Name : ' + B.highriskname 
    + ' * Keterangan : ' + B.highriskdesc 
    + ' * Percentage : ' 
    + Cast(B.percentage AS NVARCHAR(10)) + '%  '  from #TableBlackList A
	left join #tblBlackList B on A.fundclientpk = B.FundClientPK
	where B.fundclientpk is not null


    DECLARE @CheckBlackListPK INT 
	
	delete CheckBlackList 
	
	--ini from ganti ke table baru




    SELECT @CheckBlackListPK = Max(CheckBlackListPK) 
    FROM   CheckBlackList 

    SET @CheckBlackListPK = Isnull(@CheckBlackListPK, 0) 

	insert into CheckBlackList
	SELECT ROW_NUMBER()  OVER (
	 ORDER BY FundClientPK
	   ) + @CheckBlackListPK, --pk
                 FundClientPK, 
				 @LastUpdate, --valuedate date
                 'Rincian :' + Name + ' High Risk Name: ' + highriskname + ' Desc: ' + highriskdesc + ' Percentage: ' + cast(percentage as nvarchar(20)), --nvarchar(max)
                 @UsersID, --entryuserid
                 @LastUpdate, --entrytime
                 @LastUpdate --lastupdate
				 from #tblBlackList 
				 where fundclientPK not in
			(
			Select fundclientPK From #TableBlackList
			)

	SELECT @CheckBlackListPK = Max(CheckBlackListPK) 
    FROM   CheckBlackList 


	--insert into
	insert into CheckBlackList
	SELECT ROW_NUMBER()  OVER (
	 ORDER BY FundClientPK
	   ) + @CheckBlackListPK, --pk
                 FundClientPK, 
				 @LastUpdate, --valuedate date
                 'Rincian :' + Reason, --nvarchar(max)
                 @UsersID, --entryuserid
                 @LastUpdate, --entrytime
                 @LastUpdate --lastupdate
				 from #TableBlackList



                             ";
                        cmd.Parameters.AddWithValue("@UsersID", _usersID);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _usersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
                        cmd.Parameters.AddWithValue("@ClientCode", Tools.ClientCode);
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


        public string ImportSDN(string _fileSource, string _userID)
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

                        using (SqlConnection conn = new SqlConnection(Tools.conString))
                        {
                            conn.Open();
                            using (SqlCommand cmd1 = conn.CreateCommand())
                            {
                                cmd1.CommandText = "truncate table ImportOFACSDNTemp";
                                cmd1.ExecuteNonQuery();
                            }
                        }

                        // import data ke temp dulu
                        using (SqlBulkCopy bulkCopy = new SqlBulkCopy(Tools.conString, SqlBulkCopyOptions.TableLock))
                        {
                            bulkCopy.DestinationTableName = "dbo.ImportOFACSDNTemp";
                            bulkCopy.WriteToServer(CreateDataTableFromImportSDNCsv(_fileSource));
                        }

                        _msg = "Import SDN Done";

                    }
                }



                return _msg;
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        private DataTable CreateDataTableFromImportSDNCsv(string _fileSource)
        {
            DataTable dt = new DataTable();
            DataColumn dc;
            DataRow dr;

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "A1";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "A2";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "A3";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "A4";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "A5";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "A6";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "A7";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "A8";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "A9";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "A10";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "A11";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "A12";
            dc.Unique = false;
            dt.Columns.Add(dc);

            string pathOnly = Path.GetDirectoryName(_fileSource);
            string fileName = Path.GetFileName(_fileSource);


            using (OleDbConnection odConnection = new OleDbConnection(Tools.ConStringCSV(pathOnly)))
            {
                odConnection.Open();
                using (OleDbCommand odCmd = odConnection.CreateCommand())
                {
                    // _oldfilename = nama sheet yang ada di file excel yang diimport
                    string SheetName = "SELECT * FROM [" + fileName + "]";
                    odCmd.CommandText = SheetName;
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

                            dr["A1"] = odRdr[0];
                            dr["A2"] = odRdr[1];
                            dr["A3"] = odRdr[2];
                            dr["A4"] = odRdr[3];
                            dr["A5"] = odRdr[4];
                            dr["A6"] = odRdr[5];
                            dr["A7"] = odRdr[6];
                            dr["A8"] = odRdr[7];
                            dr["A9"] = odRdr[8];
                            dr["A10"] = odRdr[9];
                            dr["A11"] = odRdr[10];
                            dr["A12"] = odRdr[11];

                            if (dr["A1"].Equals(DBNull.Value) != true) { dt.Rows.Add(dr); }
                        } while (odRdr.Read());
                    }
                }
                odConnection.Close();
            }

            return dt;
        }


        public string ImportAdd(string _fileSource, string _userID)
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

                        using (SqlConnection conn = new SqlConnection(Tools.conString))
                        {
                            conn.Open();
                            using (SqlCommand cmd1 = conn.CreateCommand())
                            {
                                cmd1.CommandText = "truncate table ImportOFACAddTemp";
                                cmd1.ExecuteNonQuery();
                            }
                        }

                        // import data ke temp dulu
                        using (SqlBulkCopy bulkCopy = new SqlBulkCopy(Tools.conString, SqlBulkCopyOptions.TableLock))
                        {
                            bulkCopy.DestinationTableName = "dbo.ImportOFACAddTemp";
                            bulkCopy.WriteToServer(CreateDataTableFromImportAddCsv(_fileSource));
                        }


                        _msg = "Import Address Done";

                    }
                }



                return _msg;
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        private DataTable CreateDataTableFromImportAddCsv(string _fileSource)
        {
            DataTable dt = new DataTable();
            DataColumn dc;
            DataRow dr;

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "A1";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "A2";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "A3";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "A4";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "A5";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "A6";
            dc.Unique = false;
            dt.Columns.Add(dc);

            string pathOnly = Path.GetDirectoryName(_fileSource);
            string fileName = Path.GetFileName(_fileSource);


            using (OleDbConnection odConnection = new OleDbConnection(Tools.ConStringCSV(pathOnly)))
            {
                odConnection.Open();
                using (OleDbCommand odCmd = odConnection.CreateCommand())
                {
                    // _oldfilename = nama sheet yang ada di file excel yang diimport
                    string SheetName = "SELECT * FROM [" + fileName + "]";
                    odCmd.CommandText = SheetName;
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

                            dr["A1"] = odRdr[0];
                            dr["A2"] = odRdr[1];
                            dr["A3"] = odRdr[2];
                            dr["A4"] = odRdr[3];
                            dr["A5"] = odRdr[4];
                            dr["A6"] = odRdr[5];

                            if (dr["A1"].Equals(DBNull.Value) != true) { dt.Rows.Add(dr); }
                        } while (odRdr.Read());
                    }
                }
                odConnection.Close();
            }

            return dt;
        }

        public string ImportAlt(string _fileSource, string _userID)
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

                        using (SqlConnection conn = new SqlConnection(Tools.conString))
                        {
                            conn.Open();
                            using (SqlCommand cmd1 = conn.CreateCommand())
                            {
                                cmd1.CommandText = "truncate table ImportOFACAltTemp";
                                cmd1.ExecuteNonQuery();
                            }
                        }

                        // import data ke temp dulu
                        using (SqlBulkCopy bulkCopy = new SqlBulkCopy(Tools.conString, SqlBulkCopyOptions.TableLock))
                        {
                            bulkCopy.DestinationTableName = "dbo.ImportOFACAltTemp";
                            bulkCopy.WriteToServer(CreateDataTableFromImportAltCsv(_fileSource));
                        }

                        _msg = "Import Alt Done";

                    }
                }



                return _msg;
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        private DataTable CreateDataTableFromImportAltCsv(string _fileSource)
        {
            DataTable dt = new DataTable();
            DataColumn dc;
            DataRow dr;

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "A1";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "A2";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "A3";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "A4";
            dc.Unique = false;
            dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = System.Type.GetType("System.String");
            dc.ColumnName = "A5";
            dc.Unique = false;
            dt.Columns.Add(dc);

            string pathOnly = Path.GetDirectoryName(_fileSource);
            string fileName = Path.GetFileName(_fileSource);


            using (OleDbConnection odConnection = new OleDbConnection(Tools.ConStringCSV(pathOnly)))
            {
                odConnection.Open();
                using (OleDbCommand odCmd = odConnection.CreateCommand())
                {
                    // _oldfilename = nama sheet yang ada di file excel yang diimport
                    string SheetName = "SELECT * FROM [" + fileName + "]";
                    odCmd.CommandText = SheetName;
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

                            dr["A1"] = odRdr[0];
                            dr["A2"] = odRdr[1];
                            dr["A3"] = odRdr[2];
                            dr["A4"] = odRdr[3];
                            dr["A5"] = odRdr[4];

                            if (dr["A1"].Equals(DBNull.Value) != true) { dt.Rows.Add(dr); }
                        } while (odRdr.Read());
                    }
                }
                odConnection.Close();
            }

            return dt;
        }

        public void InsertFromOFAC(string _usersID)
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

                            declare @tableAlamat table (
	                            A1 nvarchar(1000),
	                            A3 nvarchar(1000)
                            )

                            declare @A1 nvarchar(1000)
                            declare @PK int

                            select @PK = max(BlacklistNamePK) from BlackListName 
							set @PK = isnull(@PK,0)

                            DECLARE A CURSOR
                            FOR 
	                            SELECT DISTINCT A1 FROM ImportOFACAddTemp where A3 <> '-0-' and A4 <> '-0-' and A5 <> '-0-'
                            OPEN A;
 
                            FETCH NEXT FROM A INTO @A1
 
                            WHILE @@FETCH_STATUS = 0
                                BEGIN
		
		                            declare @Alamat nvarchar(1000)

		                            select @Alamat = COALESCE(@Alamat + ',', '') + RTRIM(LTRIM(case when A3 = '-0-' and A4 = '-0-' and A5 = '-0-' then '' else case when A3 = '-0-' then '' else A3 end + ' ' + case when A4 = '-0-' then '' else A4 end + ' ' + case when A5 = '-0-' then '' else A5 end end))
		                            from ImportOFACAddTemp where A1 = @A1

		                            insert into @tableAlamat(A1,A3)
		                            select @A1,@Alamat

                                    FETCH NEXT FROM A INTO @A1
                                END;
 
                            CLOSE A;
 
                            DEALLOCATE A;


                            update BlackListName set status = 3 where NoDoc in (select distinct A1 from ImportOFACSDNTemp) and Type = 5

                            insert into BlackListName(BlackListNamePK,HistoryPK,Status,NoDoc,Type,Name,NoIDQ,NameAlias,NoID,TempatLahir,TanggalLahir,Kewarganegaraan,Alamat,Description,EntryUsersID,EntryTime,LastUpdate)
                            select ROW_NUMBER() over (order by A.A1) + @PK,1,2, A.A1 NoDoc,5 Type,A.A2 name,case when A.A4 = '-0-' then '' else A.A4 end NoIDQ,case when C.A4 is null then A.A2 else C.A4 end NameAlias,case when A.A3 = '-0-' then '' else A.A3 end NoID,
                            case when A.A12 like '%POB%' then SUBSTRING(RTRIM(LTRIM(SUBSTRING(A.A12,CHARINDEX('POB ',A.A12)+LEN('POB '),LEN(A.A12)))),0,CHARINDEX(';',RTRIM(LTRIM(SUBSTRING(A.A12,CHARINDEX('POB ',A.A12)+LEN('POB '),LEN(A.A12)))))) else '' end TempatLahir,
                            case when A.A12 like '%DOB%' then SUBSTRING(RTRIM(LTRIM(SUBSTRING(A.A12,CHARINDEX('DOB ',A.A12)+LEN('DOB '),LEN(A.A12)))),0,CHARINDEX(';',RTRIM(LTRIM(SUBSTRING(A.A12,CHARINDEX('DOB ',A.A12)+LEN('DOB '),LEN(A.A12)))))) else '' end TanggalLahir,
                            '' Kewarganegaraan, case when B.A3 is null then '' else B.A3 end Alamat, case when A.A5 = '-0-' then '' else A.A5 end + ' ' + case when A.A6 = '-0-' then '' else A.A6 end + ' ' + case when A.A7 = '-0-' then '' else A.A7 end 
                            + ' ' + case when A.A8 = '-0-' then '' else A.A8 end + ' ' + case when A.A9 = '-0-' then '' else A.A9 end + ' ' + case when A.A10 = '-0-' then '' else A.A10 end + ' ' + case when A.A11 = '-0-' then '' else A.A11 end Description,
                            @UsersID, @LastUpdate, @LastUpdate
                            from ImportOFACSDNTemp A
                            left join @tableAlamat B on A.A1 = B.A1
                            left join ImportOFACAltTemp C on A.A1 = C.A1


                             ";
                        cmd.Parameters.AddWithValue("@UsersID", _usersID);
                        cmd.Parameters.AddWithValue("@LastUpdate", _dateTimeNow);
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


        public string ImportDowJones(string _fileSource, string _userID)
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
                            cmd1.CommandText = "truncate table DowJonesTemp";
                            cmd1.ExecuteNonQuery();
                        }
                    }

                    // import data ke temp dulu
                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(Tools.conString, SqlBulkCopyOptions.TableLock))
                    {
                        bulkCopy.DestinationTableName = "dbo.DowJonesTemp";
                        bulkCopy.WriteToServer(CreateDataTableFromDowJonesTempExcelFile(_fileSource));
                        //_msg = "Import Close Nav Success";
                    }

                    // logic kalo Reconcile success
                    using (SqlConnection conn = new SqlConnection(Tools.conString))
                    {
                        conn.Open();
                        using (SqlCommand cmd1 = conn.CreateCommand())
                        {
                            cmd1.CommandText = @"
                                
declare @MaxPK int

select @MaxPK = max(BlackListNamePK) from BlackListName
set @MaxPK = isnull(@MaxPK,0)

update BlackListName set status = 3 where Name in (select distinct Name from DowJonesTemp) and Type = 6

insert into BlackListName(BlackListNamePK,HistoryPK,Status,NoDoc,Type,Name,NoIDQ,NameAlias,NoID,TempatLahir,TanggalLahir,Kewarganegaraan,Alamat,Description,EntryUsersID,EntryTime,LastUpdate)
select @MaxPK + DowJonesPK,1,2,'',6,Name,'',Name,'',PlaceOfBirth,case when DateOfBirth = '' then '' when SUBSTRING(DateOfBirth,1,2) = '00' then right(DateOfBirth,4) else DateOfBirth end,'','','',@UsersID,@LastUpdate,@LastUpdate
from DowJonesTemp

Select 'success'
                                                "
                                ;
                            cmd1.Parameters.AddWithValue("@UsersID", _userID);
                            cmd1.Parameters.AddWithValue("@LastUpdate", _now);

                            using (SqlDataReader dr = cmd1.ExecuteReader())
                            {
                                if (dr.HasRows)
                                {
                                    dr.Read();
                                    _msg = "Import Dow Jones Success"; //Convert.ToString(dr[""]);
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
        private DataTable CreateDataTableFromDowJonesTempExcelFile(string _path)
        {
            try
            {
                using (DataTable dt = new DataTable())
                {
                    DataColumn dc;
                    DataRow dr;

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.Int32");
                    dc.ColumnName = "DowJonesPK";
                    dc.Unique = false;
                    dc.AutoIncrement = true;
                    dc.AutoIncrementSeed = 1;
                    dc.AutoIncrementStep = 1;
                    dt.Columns.Add(dc);


                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "Name";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "DateOfBirth";
                    dc.Unique = false;
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.DataType = System.Type.GetType("System.String");
                    dc.ColumnName = "PlaceOfBirth";
                    dc.Unique = false;
                    dt.Columns.Add(dc);




                    FileInfo excelFile = new FileInfo(_path);
                    using (ExcelPackage package = new ExcelPackage(excelFile))
                    {
                        int i = 4;
                        ExcelWorksheet worksheet = package.Workbook.Worksheets[1];
                        var end = worksheet.Dimension.End;
                        while (i <= end.Row)
                        {
                            dr = dt.NewRow();
                            if (worksheet.Cells[i, 1].Value == null)
                                dr["Name"] = "";
                            else
                                dr["Name"] = worksheet.Cells[i, 1].Value.ToString();

                            if (worksheet.Cells[i, 2].Value == null)
                                dr["DateOfBirth"] = "";
                            else
                                dr["DateOfBirth"] = worksheet.Cells[i, 2].Value.ToString();

                            if (worksheet.Cells[i, 3].Value == null)
                                dr["PlaceOfBirth"] = "";
                            else
                                dr["PlaceOfBirth"] = worksheet.Cells[i, 3].Value.ToString();


                            if (dr["Name"].Equals(null) != true ||
                                dr["DateOfBirth"].Equals(null) != true ||
                                dr["PlaceOfBirth"].Equals(null) != true
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