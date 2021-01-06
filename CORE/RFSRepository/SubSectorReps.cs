using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RFSModel;
using RFSUtility;
using System.Data.SqlClient;


namespace RFSRepository
{
    public class SubSectorReps
    {
        Host _host = new Host();


        //NEW
        public List<SubSector> SubSector_SelectBySectorPK(int _SectorPK)
        {
            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<SubSector> L_SubSector = new List<SubSector>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {

                        cmd.CommandText = @"Select case when A.status=1 then 'PENDING' else Case When A.status = 2 then 'APPROVED' else Case when A.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,B.ID SectorID,B.Name SectorName, A.* from SubSector A left join 
                        Sector B on A.SectorPK = B.SectorPK and B.status = 2
                        where A.status in (1,2) and A.SectorPK = @SectorPK ";
                        cmd.Parameters.AddWithValue("@SectorPK", _SectorPK);

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_SubSector.Add(setSubSector(dr));
                                }
                            }
                            return L_SubSector;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        //====================================================================================================================================


        //1
        string _insertCommand = "INSERT INTO [dbo].[SubSector] " +
                            "([SubSectorPK],[HistoryPK],[Status],[SectorPK],[ID],[Name],";
        string _paramaterCommand = "@SectorPK,@ID,@Name,";

        //2
        private SubSector setSubSector(SqlDataReader dr)
        {
            SubSector M_SubSector = new SubSector();
            M_SubSector.SubSectorPK = Convert.ToInt32(dr["SubSectorPK"]);
            M_SubSector.HistoryPK = Convert.ToInt32(dr["HistoryPK"]);
            M_SubSector.Status = Convert.ToInt32(dr["Status"]);
            M_SubSector.StatusDesc = Convert.ToString(dr["StatusDesc"]);
            M_SubSector.Notes = Convert.ToString(dr["Notes"]);
            M_SubSector.SectorPK = Convert.ToInt32(dr["SectorPK"]);
            M_SubSector.SectorID = Convert.ToString(dr["SectorID"]);
            M_SubSector.SectorName = Convert.ToString(dr["SectorName"]);
            M_SubSector.ID = dr["ID"].ToString();
            M_SubSector.Name = dr["Name"].ToString();
            M_SubSector.EntryUsersID = dr["EntryUsersID"].ToString();
            M_SubSector.UpdateUsersID = dr["UpdateUsersID"].ToString();
            M_SubSector.ApprovedUsersID = dr["ApprovedUsersID"].ToString();
            M_SubSector.VoidUsersID = dr["VoidUsersID"].ToString();
            M_SubSector.EntryTime = dr["EntryTime"].ToString();
            M_SubSector.UpdateTime = dr["UpdateTime"].ToString();
            M_SubSector.ApprovedTime = dr["ApprovedTime"].ToString();
            M_SubSector.VoidTime = dr["VoidTime"].ToString();
            M_SubSector.DBUserID = dr["DBUserID"].ToString();
            M_SubSector.DBTerminalID = dr["DBTerminalID"].ToString();
            M_SubSector.LastUpdate = dr["LastUpdate"].ToString();
            M_SubSector.LastUpdateDB = Convert.ToString(dr["LastUpdateDB"]);
            return M_SubSector;
        }

        public List<SubSector> SubSector_Select(int _status)
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<SubSector> L_SubSector = new List<SubSector>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        if (_status != 9)
                        {
                            cmd.CommandText = @"Select case when A.status = 1 then 'PENDING' else Case When A.status = 2 then 'APPROVED' else Case when A.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,B.ID SectorID,B.Name SectorName,A.* from SubSector A
	                                            left join Sector B on A.SectorPK = B.SectorPK and B.status = 2
	                                            where A.status = @Status ";
                            cmd.Parameters.AddWithValue("@status", _status);
                        }
                        else
                        {
                            cmd.CommandText = @"Select case when A.status = 1 then 'PENDING' else Case When A.status = 2 then 'APPROVED' else Case when A.Status = 3 then 'VOID' else 'WAITING' END END END StatusDesc,B.ID SectorID,B.Name SectorName,A.* from SubSector A
	                                            left join Sector B on A.SectorPK = B.SectorPK and B.status = 2
                                                Order by SectorPK ";
                        }
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    L_SubSector.Add(setSubSector(dr));
                                }
                            }
                            return L_SubSector;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int SubSector_Add(SubSector _SubSector, bool _havePrivillege)
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
                                 "Select isnull(max(SubSectorPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@ApprovedUsersID,@ApprovedTime,@LastUpdate from SubSector";
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _SubSector.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        }
                        else
                        {
                            cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],LastUpdate)" +
                                "Select isnull(max(SubSectorPk),0) + 1,1,@status," + _paramaterCommand + "@EntryUsersID,@EntryTime,@LastUpdate from SubSector";
                        }
                        cmd.Parameters.AddWithValue("@status", _havePrivillege ? 2 : 1);
                        cmd.Parameters.AddWithValue("@SectorPK", _SubSector.SectorPK);
                        cmd.Parameters.AddWithValue("@ID", _SubSector.ID);
                        cmd.Parameters.AddWithValue("@Name", _SubSector.Name);
                        cmd.Parameters.AddWithValue("@EntryUsersID", _SubSector.EntryUsersID);
                        cmd.Parameters.AddWithValue("@EntryTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);

                        cmd.ExecuteNonQuery();

                        return _host.Get_LastPKByLastUpate(_datetimeNow, "SubSector");
                    }

                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        public int SubSector_Update(SubSector _SubSector, bool _havePrivillege)
        {

            try
            {
                int _newHisPK;
                int status = _host.Get_Status(_SubSector.SubSectorPK, _SubSector.HistoryPK, "SubSector");
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    if (_havePrivillege)
                    {
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update SubSector set status=2, Notes=@Notes,SectorPK=@SectorPK,ID=@ID,Name=@Name," +
                                "ApprovedUsersID=@ApprovedUsersID, " +
                                "ApprovedTime=@ApprovedTime,UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@lastupdate " +
                                "where SubSectorPK = @PK and historyPK = @HistoryPK";
                            cmd.Parameters.AddWithValue("@HistoryPK", _SubSector.HistoryPK);
                            cmd.Parameters.AddWithValue("@PK", _SubSector.SubSectorPK);
                            cmd.Parameters.AddWithValue("@Notes", _SubSector.Notes);
                            cmd.Parameters.AddWithValue("@SectorPK", _SubSector.SectorPK);
                            cmd.Parameters.AddWithValue("@ID", _SubSector.ID);
                            cmd.Parameters.AddWithValue("@Name", _SubSector.Name);
                            cmd.Parameters.AddWithValue("@UpdateUsersID", _SubSector.EntryUsersID);
                            cmd.Parameters.AddWithValue("@Updatetime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@ApprovedUsersID", _SubSector.EntryUsersID);
                            cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                            cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);

                            cmd.ExecuteNonQuery();
                        }
                        using (SqlCommand cmd = DbCon.CreateCommand())
                        {
                            cmd.CommandText = "Update SubSector set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where SubSectorPK = @PK and status = 4";
                            cmd.Parameters.AddWithValue("@PK", _SubSector.SubSectorPK);
                            cmd.Parameters.AddWithValue("@VoidUsersID", _SubSector.EntryUsersID);
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
                                cmd.CommandText = "Update SubSector set Notes=@Notes,SectorPK=@SectorPK,ID=@ID,Name=@Name," +
                                "UpdateUsersID=@UpdateUsersID,Updatetime=@Updatetime,LastUpdate=@lastupdate " +
                                "where SubSectorPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@HistoryPK", _SubSector.HistoryPK);
                                cmd.Parameters.AddWithValue("@PK", _SubSector.SubSectorPK);
                                cmd.Parameters.AddWithValue("@Notes", _SubSector.Notes);
                                cmd.Parameters.AddWithValue("@SectorPK", _SubSector.SectorPK);
                                cmd.Parameters.AddWithValue("@ID", _SubSector.ID);
                                cmd.Parameters.AddWithValue("@Name", _SubSector.Name);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _SubSector.EntryUsersID);
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
                                _newHisPK = _host.Get_NewHistoryPK(_SubSector.SubSectorPK, "SubSector");
                                cmd.CommandText = _insertCommand + "[EntryUsersID],[EntryTime],[UpdateUsersID],[UpdateTime],[LastUpdate])" +
                                "Select @PK,@NewHistoryPK,1," + _paramaterCommand + "EntryUsersID,EntryTime,@UpdateUsersID,@UpdateTime,@LastUpdate  " +
                                "From SubSector where SubSectorPK =@PK and historyPK = @HistoryPK ";

                                cmd.Parameters.AddWithValue("@PK", _SubSector.SubSectorPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _SubSector.HistoryPK);
                                cmd.Parameters.AddWithValue("@NewHistoryPK", _newHisPK);
                                cmd.Parameters.AddWithValue("@SectorPK", _SubSector.SectorPK);
                                cmd.Parameters.AddWithValue("@ID", _SubSector.ID);
                                cmd.Parameters.AddWithValue("@Name", _SubSector.Name);
                                cmd.Parameters.AddWithValue("@UpdateUsersID", _SubSector.EntryUsersID);
                                cmd.Parameters.AddWithValue("@UpdateTime", _datetimeNow);
                                cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                                cmd.ExecuteNonQuery();
                            }

                            using (SqlCommand cmd = DbCon.CreateCommand())
                            {
                                cmd.CommandText = "Update SubSector set status= 4,Notes=@Notes," +
                                " LastUpdate=@lastupdate " +
                                " where SubSectorPK = @PK and historyPK = @HistoryPK";
                                cmd.Parameters.AddWithValue("@Notes", _SubSector.Notes);
                                cmd.Parameters.AddWithValue("@PK", _SubSector.SubSectorPK);
                                cmd.Parameters.AddWithValue("@HistoryPK", _SubSector.HistoryPK);
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

        public void SubSector_Approved(SubSector _SubSector)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update SubSector set status = 2,ApprovedUsersID = @ApprovedUsersID,ApprovedTime = @ApprovedTime,LastUpdate=@lastupdate " +
                            "where SubSectorPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _SubSector.SubSectorPK);
                        cmd.Parameters.AddWithValue("@historyPK", _SubSector.HistoryPK);
                        cmd.Parameters.AddWithValue("@ApprovedUsersID", _SubSector.ApprovedUsersID);
                        cmd.Parameters.AddWithValue("@ApprovedTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update SubSector set status= 3,VoidUsersID=@VoidUsersID,VoidTime=@VoidTime,LastUpdate=@LastUpdate where SubSectorPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _SubSector.SubSectorPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _SubSector.ApprovedUsersID);
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

        public void SubSector_Reject(SubSector _SubSector)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update SubSector set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where SubSectorPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _SubSector.SubSectorPK);
                        cmd.Parameters.AddWithValue("@historyPK", _SubSector.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _SubSector.VoidUsersID);
                        cmd.Parameters.AddWithValue("@VoidTime", _datetimeNow);
                        cmd.Parameters.AddWithValue("@LastUpdate", _datetimeNow);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "Update SubSector set status= 2,LastUpdate=@LastUpdate where SubSectorPK = @PK and status = 4";
                        cmd.Parameters.AddWithValue("@PK", _SubSector.SubSectorPK);
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

        public void SubSector_Void(SubSector _SubSector)
        {
            try
            {
                DateTime _datetimeNow = DateTime.Now;
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "update SubSector set status = 3,VoidUsersID = @VoidUsersID,VoidTime = @VoidTime,LastUpdate=@LastUpdate " +
                            "where SubSectorPK = @PK and historypk = @historyPK";
                        cmd.Parameters.AddWithValue("@PK", _SubSector.SubSectorPK);
                        cmd.Parameters.AddWithValue("@historyPK", _SubSector.HistoryPK);
                        cmd.Parameters.AddWithValue("@VoidUsersID", _SubSector.VoidUsersID);
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

        public List<SubSectorCombo> SubSector_Combo()
        {

            try
            {
                using (SqlConnection DbCon = new SqlConnection(Tools.conString))
                {
                    DbCon.Open();
                    List<SubSectorCombo> L_SubSector = new List<SubSectorCombo>();
                    using (SqlCommand cmd = DbCon.CreateCommand())
                    {
                        cmd.CommandText = "SELECT  SubSectorPK,ID +' - '+ Name ID, Name FROM [SubSector]  where status = 2 order by SubSectorPK";
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    SubSectorCombo M_SubSector = new SubSectorCombo();
                                    M_SubSector.SubSectorPK = Convert.ToInt32(dr["SubSectorPK"]);
                                    M_SubSector.ID = Convert.ToString(dr["ID"]);
                                    M_SubSector.Name = Convert.ToString(dr["Name"]);
                                    L_SubSector.Add(M_SubSector);
                                }

                            }
                            return L_SubSector;
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