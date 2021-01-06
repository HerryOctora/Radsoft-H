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
    public class SectorReps
    {
        Host _host = new Host();
        
        //1
        string _insertCommand = "INSERT INTO [dbo].[Sector] " +
                            "([SectorPK],[HistoryPK],[Status],[ID],[Name],";
        string _paramaterCommand = "@ID,@Name,";

        //2
        private Sector setSector(SqlDataReader dr)
        {
            Sector M_Sector = new Sector();
            M_Sector.SectorPK = Convert.ToInt32(dr["SectorPK"]);
            M_Sector.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_Sector.Status = Convert.ToInt32(dr["Status"]);
            M_Sector.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_Sector.Notes = Convert.ToString(dr["Notes"]);
            M_Sector.ID = dr["ID"].ToString();
            M_Sector.Name = dr["Name"].ToString();
            M_Sector.EntryUsersID = dr["EntryUsersID"].ToString();
            M_Sector.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_Sector.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_Sector.VoidUsersID = dr["VoidUsersID"].ToString();
            M_Sector.EntryTime = dr["EntryTime"].ToString();
            M_Sector.UpdateTime = dr["UpdateTime"].ToString();
            M_Sector.ApprovedTime = dr["ApprovedTime"].ToString();
            M_Sector.VoidTime = dr["VoidTime"].ToString();
            M_Sector.DBUserID = dr["DBUserID"].ToString();
            M_Sector.DBTerminalID = dr["DBTerminalID"].ToString();
            M_Sector.LastUpdate = dr["LastUpdate"].ToString();
            M_Sector.LastUpdateDB = dr["LastUpdateDB"].Equals(DBNull.Value) == true ? "" : Convert.ToString(dr["LastUpdateDB"]);
            return M_Sector;
        }

        public List<Sector> Sector_Select(int _status)
        {
            
             try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<Sector> L_Sector = new List<Sector>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = "Select case when status=1 then 'PENDING' else Case When status = 2 then 'APPROVED' else Case when Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,* from Sector where status = @status";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = "Select case when status=1 then 'PENDING' else Case When status = 2 then 'APPROVED' else Case when Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,* from Sector order by ID,Name";
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_Sector.Add(setSector(dr));
                                }
                            }
                            return L_Sector;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
            
        }

        public int Sector_Add(Sector _sector, bool _havePrivillege)
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
                                 "Select isnull(max(SectorPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@LastUpdate From Sector";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _sector.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],LastUpdate)" +
                                "Select isnull(max(SectorPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@LastUpdate From Sector";
                        }
                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@ID", _sector.ID);
                        cmd.Parameters.AddWithValue("@Name", _sector.Name);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _sector.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();

                        return _host.Get_LastPKByLastUpate(_datetimeNow, "Sector");
                    }

                }
            }
            catch (Exception err)
            {
                throw err;
            }
           
        }

        public int Sector_Update(Sector _sector, bool _havePrivillege)
        {
            
            try
            {
                int _newHisPK;
                DateTime _datetimeNow = DateTime.Now;
                int status = _host.Get_Status(_sector.SectorPK, _sector.HistoryPK, "sector");
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update Sector set status=2,Notes=@Notes,ID=@ID,Name=@Name," +
                                "ApprovedUsersID=@ApprovedUsersID, " +
                                "ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@lastupdate " +
                                "where SectorPK = @PK and historyPK = @HistoryPK";
                            cmd.Parameters.AddWithValue("@HistoryPK", _sector.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _sector.SectorPK);
                            cmd.Parameters.AddWithValue("@ID", _sector.ID);
                            cmd.Parameters.AddWithValue("@Notes", _sector.Notes);
                            cmd.Parameters.AddWithValue("@Name", _sector.Name);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _sector.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _sector.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update Sector set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@lastupdate where SectorPK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _sector.SectorPK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _sector.EntryUsersID);
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
                                cmd.CommandText = "Update Sector set Notes=@Notes,ID=@ID,Name=@Name," +
                                    "UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@lastupdate " +
                                    "where SectorPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _sector.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _sector.SectorPK);
                                cmd.Parameters.AddWithValue("@ID", _sector.ID);
                                cmd.Parameters.AddWithValue("@Notes", _sector.Notes);
                                cmd.Parameters.AddWithValue("@Name", _sector.Name);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _sector.EntryUsersID);
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
                                _newHisPK = _host.Get_NewHistoryPK(_sector.SectorPK, "Sector");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From Sector where SectorPK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _sector.SectorPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _sector.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@ID", _sector.ID);
                                cmd.Parameters.AddWithValue("@Name", _sector.Name);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _sector.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);

                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update Sector set status= 4,Notes=@Notes,"+
                                     " LastUpdate=@lastupdate " +
                                     " where SectorPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _sector.Notes);
                                cmd.Parameters.AddWithValue("@PK", _sector.SectorPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _sector.HistoryPK);
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

        public void Sector_Approved(Sector _sector)
        {
             try
            {
                DateTime _datetimeNow = DateTime.Now; 
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update Sector set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,LastUpdate=@lastupdate " +
                            "where SectorPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _sector.SectorPK);
                        cmd.Parameters.AddWithValue("@historyPK", _sector.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _sector.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update Sector set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@lastupdate where SectorPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _sector.SectorPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _sector.ApprovedUsersID);
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

        public void Sector_Reject(Sector _sector)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now; 
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update Sector set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@lastupdate " +
                            "where SectorPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _sector.SectorPK);
                        cmd.Parameters.AddWithValue("@historyPK", _sector.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _sector.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update Sector set status= 2,LastUpdate=@lastupdate where SectorPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _sector.SectorPK);
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

        public void Sector_Void(Sector _sector)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now; 
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update Sector set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where SectorPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _sector.SectorPK);
                        cmd.Parameters.AddWithValue("@historyPK", _sector.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _sector.VoidUsersID);
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

        //AREA LAIN-LAIN dimulai dari sini ( untuk function diluar standart )

        public List<SectorCombo> Sector_Combo()
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<SectorCombo> L_Sector = new List<SectorCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "SELECT  SectorPK,ID + ' - ' + Name ID, Name FROM [Sector]  where status = 2 order by ID,Name";
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    SectorCombo M_Sector = new SectorCombo();
                                    M_Sector.SectorPK = Convert.ToInt32(dr["SectorPK"]);
                                    M_Sector.ID = Convert.ToString(dr["ID"]);
                                    M_Sector.Name = Convert.ToString(dr["Name"]);
                                    L_Sector.Add(M_Sector);
                                }

                            }
                            return L_Sector;
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